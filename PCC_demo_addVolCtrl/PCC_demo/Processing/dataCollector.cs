using Sunny.UI.Win32;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using SQLiteDemo;
using System.Reflection;
using NPOI.POIFS.Crypt.Dsig;
using PCC_demo.Processing;
using Accord.Math;
using Sunny.UI.Demo;
using System.Windows.Forms;
using NationalInstruments.DAQmx;
using System.IO;

namespace PCC_demo
{
    internal class dataCollector
    {
        private Thread collectThread;
        private bool running;

        Random random = new Random();
        
        //for collecting
        double[,] rawData = new double[6, 240];
        double[] pccData = new double[6];

        //for display
        public static double[] data_playing = new double[6];
        public static DateTime time_playing;
        DateTime time_last;
        public delegate void DataHandler(double[] data_playing); // 定义一个委托类型
        public event DataHandler DataUpdated; // 定义一个事件

        //for voltageControl
        const int volMin = 0, volMax = 10;
        const double volAdd = 0.1;
        const int NoLoopDelay = 10000;

        public static int timer_interval = 60000;

        //for sqlite
        SQLdemo sqlCall = new SQLdemo();
        DateTime DTlast = DateTime.Now;
        DateTime DTnow = DateTime.Now;

        //for NI card
        bool[,] PortsArray = new bool[3, 8];
        //private Task digitalWriteTaskAll = new Task();
        //private Task AnalogTask = new Task();
        //private DigitalMultiChannelWriter writerAll;
        //private AnalogSingleChannelReader myAnalogReader;
        //private AnalogMultiChannelReader readerAll;
        private const double minimumValue = -10;
        private const double maximumValue = 10;
        const int ChannelNumber = 6;
        const int sampleNumber = 2;

        public System.Timers.Timer timer;

        public dataCollector()
        {
            initNI();
            collectThread = new Thread(new ThreadStart(dataCollect));
            running = false;
            sqlCall.initialSQL();
            SetUpTimer(TimeSpan.FromMilliseconds(10)); // 设置定时器为每30s触发，包括采集数据、处理数据
        }

        private void SetUpTimer(TimeSpan alertTime)
        {
            DateTime current = DateTime.Now; 
            TimeSpan timeToGo = alertTime; 
            this.timer = new System.Timers.Timer(timeToGo.TotalMilliseconds);
            this.timer.Elapsed += timer_Elapsed;
            this.timer.AutoReset = true; 
            this.timer.Interval = timer_interval;
            this.timer.Enabled = true;
        }

        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e) // 定义事件处理方法
        {
            this.SomeMethodRunsEach30s(); // 调用要执行的方法
            UpdateDataDisplay(); // 调用要执行更新数据显示的方法
        }

        public async void SomeMethodRunsEach30s()
        {
            double volNo = 0;
            int sensorNo = 0;
            DateTime time = DateTime.Now.AddSeconds(-DateTime.Now.Second).AddMilliseconds(-DateTime.Now.Millisecond);//当前的年月日时分
            time_playing = DateTime.Now;
            double[] data_display = new double[6];
            DTnow = DateTime.Now;

            double temp = random.NextDouble() * 0.2 + 0.3;
            string rawDataString = "";  // 用于把240个原始值连成字符串

            getParameters.afterPCA_dict.Clear();

            //  Create new table and delete old table(once a day)
            if (DTnow != DTlast)
            {
                string dateString = DTnow.ToString("yyMMdd");
                sqlCall.createTable(dateString, "raw");
                sqlCall.createTable(dateString, "PCC");
                string delete_raw = DTnow.AddDays(-2).ToString("yyMMdd");//删除三天前的表
                string delete_PCC = DTnow.AddYears(-1).ToString("yyMMdd");//删除一年前的表
                sqlCall.deleteTable(delete_raw, "raw");
                sqlCall.deleteTable(delete_PCC, "PCC");
            }
            DTlast = DTnow;

            // rawData get & insert
            for (volNo = volMin; volNo <= volMax; volNo += volAdd)
            {
                rawData.Clear();
                volNo = Math.Round(volNo, 1);
                //GetData(volNo); //实际使用时rawData用这个函数赋值
                GetHistoryData(volNo);  //for test



                for (sensorNo = 0; sensorNo < rawData.GetLength(0); sensorNo++)
                {
                    rawDataString = "";
                    for (int i = 0; i < rawData.GetLength(1); i++)
                    {
                        //rawData[sensorNo, i] = random.NextDouble() * 40 - 60;  // 测试时随机赋一个-60~-20之间的值
                        rawDataString += rawData[sensorNo, i].ToString("0.0000") + ", ";
                    }
                    rawDataString = rawDataString.Substring(0, rawDataString.Length - 2);
                    object[] values = new object[] { time, sensorNo + 1, Math.Round(volNo, 1), rawDataString };
                    await sqlCall.insertDataAsync(time.ToString("yyMMdd"), "raw", values);
                }
                //  PCA降维
                getParameters.PCAprocess(rawData, Convert.ToInt16(volNo * 10), getParameters.PCAMatrix, getParameters.PCAMean);
            }
            getParameters.transPCAdict_toArray(getParameters.afterPCA_dict);
            //  OMP预测
            pccData = getParameters.PredictValues(getParameters.afterPCA_array, getParameters.formulaDict);

            // pccData get & insert
            for (sensorNo = 0; sensorNo < data_playing.Length; sensorNo++)
            {
                //pccData[sensorNo] = temp + random.NextDouble() * 0.05;
                object[] values = new object[] { time, sensorNo + 1, pccData[sensorNo] };
                await sqlCall.insertDataAsync(time.ToString("yyMMdd"), "PCC", values);
            }
        }

        public void UpdateDataDisplay() // 定义更新数据显示的方法
        {
            double[] data_display = new double[6];
            // data_playing get & update
            for (int sensorNo = 0; sensorNo < data_playing.Length; sensorNo++)
            {
                data_display[sensorNo] = pccData[sensorNo];
                data_playing[sensorNo] = data_display[sensorNo];
            }

            if (DataUpdated != null)
            {
                DataUpdated(data_display); // 触发事件
            }
        }

        // GetData from txt
        private void GetHistoryData(double stepVol)
        {
            string folderPath = @"G:\230718test\煤粉浓度采集数据\python_pca\231020_withoutscaler\temp\2307201130\SUM";
     
            Dictionary<Tuple<int, int>, List<double>> dict = new Dictionary<Tuple<int, int>, List<double>>();
            string[] files = Directory.GetFiles(folderPath);
            foreach (string file in files)
            {
                string fileName = Path.GetFileName(file);
                string[] parts = fileName.Split('_');
                int a = int.Parse(parts[0]);
                int b = int.Parse(parts[1].Substring(3));
                int c = int.Parse(parts[2].Substring(0, parts[2].Length - 4));
                if (a == stepVol * 10)
                {
                    using (StreamReader sr = File.OpenText(file))
                    {
                        int count = 0;
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            double value = double.Parse(line);
                            if (!dict.ContainsKey(Tuple.Create(a, b)))
                            {
                                dict[Tuple.Create(a, b)] = new List<double>();
                            }
                            dict[Tuple.Create(a, b)].Add(value);
                            count++;
                        }
                    }
                }
            }
            foreach (var item in dict)
            {
                int a = item.Key.Item1;
                int b = item.Key.Item2;
                double[] averages = new double[240];
                for (int i = 0; i < 240; i++)
                {
                    double sum = 0;
                    double sumOfSquares = 0;
                    int count = 0;
                    for (int j = 0; j < item.Value.Count; j++)
                    {
                        if (j % 240 == i)
                        {
                            sum += item.Value[j];
                            sumOfSquares += item.Value[j] * item.Value[j];
                            count++;
                        }
                    }
                    double mean = sum / count;
                    double standardDeviation = Math.Sqrt((sumOfSquares - sum * sum / count) / (count - 1));
                    standardDeviation = standardDeviation == 0 ? 0.00001 : standardDeviation;
                    List<double> validValues = new List<double>();
                    for (int j = 0; j < item.Value.Count; j++)
                    {
                        if (j % 240 == i)
                        {
                            double zScore = (item.Value[j] - mean) / standardDeviation;
                            if (Math.Abs(zScore) <= 2)
                            {
                                validValues.Add(item.Value[j]);
                            }
                        }
                    }
                    averages[i] = validValues.Average();
                }
                for (int i = 0; i < 240; i++)
                {
                    rawData[b - 1, i] = averages[i];
                }
            }

        }




        //  采集函数
        private void GetData(double stepVol)
        {
            generateVoltage(stepVol);
            System.Threading.Thread.SpinWait(NoLoopDelay);

            rawData = get6ChannelObjectArray_240();
        }

        /// <summary>
        /// to generate voltage for VCO tuning
        /// Analog Output0, 0-10V
        /// </summary>
        /// <param name="volt"></param>
        private void generateVoltage(double volt)
        {
            //try
            //{
            //    using (Task analogOutTask = new Task())
            //    {
            //        analogOutTask.AOChannels.CreateVoltageChannel("Dev1/ao0", "aoChannel",
            //            0, 10, AOVoltageUnits.Volts);
            //        AnalogSingleChannelWriter writer = new AnalogSingleChannelWriter(analogOutTask.Stream);
            //        writer.WriteSingleSample(true, volt);
            //    }
            //}
            //catch (DaqException ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        /// <summary>
        /// Measure 240 data
        /// </summary>
        private double[,] get6ChannelObjectArray_240()
        {
            int T, R;
            int i = 0, j = 0;
            int ChannelNo = 0;
            double[,] AllData_6CH_240 = new double[ChannelNumber, 240];
            double[] OneData_6CH_240 = new double[ChannelNumber];
            for (T = 1; T <= 16; T++)
            {
                for (j = 1; j < 16; j++)
                {
                    if ((T + j) <= 16)
                    {
                        R = T + j;
                    }
                    else
                    {
                        R = T + j - 16;
                    }
                    Channel_Change(T, R);
                    System.Threading.Thread.SpinWait(NoLoopDelay);
                    OneData_6CH_240 = MeasureMultiCH();
                    for (ChannelNo = 0; ChannelNo < ChannelNumber; ChannelNo++)
                    {
                        AllData_6CH_240[ChannelNo, i] = OneData_6CH_240[ChannelNo];
                    }
                    //voltMag[i] = data;
                    i++;
                }
            }
            return AllData_6CH_240;
        } //end of getObjectArray()

        private void Channel_Change(int transmitter, int reciever)
        {
            // int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8 }; //port0.0 - 0.7 => Ctrl 1 - 8
            if (transmitter == reciever)
            {
                //MessageBox.Show("Reviever cannot be same as Transmitter!", "ERROR");
            }
            else
            {
                //ctrl1 = bit 0
                //ctrl2 = bit 1
                //ctrl3 = bit 2
                // .....
                //ctrl8 = bit 7

                PortsArray[2, 2] = false;
                PortsArray[2, 3] = false;

                if (transmitter <= 8)
                {
                    PortsArray[2, 1] = false;
                    //PortsArray[2, 2] = false;
                    //PortsArray[2, 3] = true;
                }
                else if (transmitter >= 9)
                {
                    PortsArray[2, 1] = true;
                    //PortsArray[2, 2] = true;
                    //PortsArray[2, 3] = false;
                }

                if (reciever <= 8)
                {
                    PortsArray[2, 4] = false;
                }
                else
                {
                    PortsArray[2, 4] = true;
                }

                if (transmitter - reciever == -8)
                {
                    PortsArray[2, 2] = true;
                }
                else if (transmitter - reciever == 8)
                {
                    PortsArray[2, 3] = true;
                }

                switch (transmitter)
                {
                    case 1:
                    case 9:
                        PortsArray[0, 0] = false;
                        PortsArray[0, 1] = false;
                        PortsArray[0, 2] = false;
                        break;
                    case 2:
                    case 10:
                        PortsArray[0, 0] = false;
                        PortsArray[0, 1] = false;
                        PortsArray[0, 2] = true;
                        break;
                    case 3:
                    case 11:
                        PortsArray[0, 0] = false;
                        PortsArray[0, 1] = true;
                        PortsArray[0, 2] = false;
                        break;
                    case 4:
                    case 12:
                        PortsArray[0, 0] = false;
                        PortsArray[0, 1] = true;
                        PortsArray[0, 2] = true;
                        break;
                    case 5:
                    case 13:
                        PortsArray[0, 0] = true;
                        PortsArray[0, 1] = false;
                        PortsArray[0, 2] = false;
                        break;
                    case 6:
                    case 14:
                        PortsArray[0, 0] = true;
                        PortsArray[0, 1] = false;
                        PortsArray[0, 2] = true;
                        break;
                    case 7:
                    case 15:
                        PortsArray[0, 0] = true;
                        PortsArray[0, 1] = true;
                        PortsArray[0, 2] = false;
                        break;
                    case 8:
                    case 16:
                        PortsArray[0, 0] = true;
                        PortsArray[0, 1] = true;
                        PortsArray[0, 2] = true;
                        break;
                }

                switch (reciever)
                {
                    case 1:
                    case 9:
                        PortsArray[0, 3] = true;
                        PortsArray[0, 4] = true;
                        PortsArray[0, 5] = true;
                        PortsArray[0, 6] = false;
                        PortsArray[0, 7] = false;
                        PortsArray[1, 6] = false;
                        break;
                    case 2:
                    case 10:
                        PortsArray[0, 3] = true;
                        PortsArray[0, 4] = true;
                        PortsArray[0, 5] = false;
                        PortsArray[0, 6] = true;  // false;
                        PortsArray[0, 7] = false;
                        PortsArray[1, 6] = false;  // true;
                        break;
                    case 3:
                    case 11:
                        PortsArray[0, 3] = true;
                        PortsArray[0, 4] = false;
                        PortsArray[0, 5] = true;
                        PortsArray[0, 6] = false;
                        PortsArray[0, 7] = true;
                        PortsArray[1, 6] = false;
                        break;
                    case 4:
                    case 12:
                        PortsArray[0, 3] = true;
                        PortsArray[0, 4] = false;
                        PortsArray[0, 5] = false;
                        PortsArray[0, 6] = true;  // false;
                        PortsArray[0, 7] = true;
                        PortsArray[1, 6] = false; // true;
                        break;
                    case 5:
                    case 13:
                        PortsArray[0, 3] = false;
                        PortsArray[0, 4] = true;
                        PortsArray[0, 5] = true;
                        PortsArray[0, 6] = false;  // true;
                        PortsArray[0, 7] = false;
                        PortsArray[1, 6] = true;  // false;
                        break;
                    case 6:
                    case 14:
                        PortsArray[0, 3] = false;
                        PortsArray[0, 4] = true;
                        PortsArray[0, 5] = false;
                        PortsArray[0, 6] = true;
                        PortsArray[0, 7] = false;
                        PortsArray[1, 6] = true;
                        break;
                    case 7:
                    case 15:
                        PortsArray[0, 3] = false;
                        PortsArray[0, 4] = false;
                        PortsArray[0, 5] = true;
                        PortsArray[0, 6] = false;  // true;
                        PortsArray[0, 7] = true;
                        PortsArray[1, 6] = true; // false;
                        break;
                    case 8:
                    case 16:
                        PortsArray[0, 3] = false;
                        PortsArray[0, 4] = false;
                        PortsArray[0, 5] = false;
                        PortsArray[0, 6] = true;
                        PortsArray[0, 7] = true;
                        PortsArray[1, 6] = true;
                        break;
                }
                //writerAll.WriteSingleSampleMultiLine(true, PortsArray);
            }
        }//end of ChannelChange

        /// <summary>
        /// measure MultiChannel
        /// </summary>
        private double[] MeasureMultiCH()
        {
            double[,] Tdata = new double[ChannelNumber, sampleNumber];
            double[] sumTemp = new double[ChannelNumber];

            //Tdata = readerAll.ReadMultiSample(sampleNumber);
            //for (int ChannelNo = 0; ChannelNo < ChannelNumber; ChannelNo++)
            //{
            //    for (int j = 0; j < sampleNumber; j++)
            //    {
            //        sumTemp[ChannelNo] = sumTemp[ChannelNo] + Tdata[ChannelNo, j];
            //    }
            //    sumTemp[ChannelNo] = sumTemp[ChannelNo] / sampleNumber;
            //    //double result = 19.2626 * data - 63.4532 + 8;
            //    sumTemp[ChannelNo] = 20 * sumTemp[ChannelNo] - 60;
            //}
            return sumTemp;
        } //end of measureCal

        /// <summary>
        /// init NI card tasks
        /// </summary>
        private void initNI()
        {
            //digitalWriteTaskAll.DOChannels.CreateChannel("Dev1/port0", "",
            //        ChannelLineGrouping.OneChannelForAllLines);
            //digitalWriteTaskAll.DOChannels.CreateChannel("Dev1/port1", "",
            //        ChannelLineGrouping.OneChannelForAllLines);
            //digitalWriteTaskAll.DOChannels.CreateChannel("Dev1/port2", "",
            //        ChannelLineGrouping.OneChannelForAllLines);
            //digitalWriteTaskAll.Control(TaskAction.Verify);
            //writerAll = new DigitalMultiChannelWriter(digitalWriteTaskAll.Stream);
            //digitalWriteTaskAll.Start();

            ////create 6 virtual channel
            //AnalogTask.AIChannels.CreateVoltageChannel("Dev1/ai1:6", "",
            //        AITerminalConfiguration.Rse, minimumValue, maximumValue, AIVoltageUnits.Volts);
            ////AnalogTask.Timing.ConfigureSampleClock("", SampleRate, SampleClockActiveEdge.Rising, SampleQuantityMode.HardwareTimedSinglePoint);
            //AnalogTask.Control(TaskAction.Verify);
            //readerAll = new AnalogMultiChannelReader(AnalogTask.Stream);
            //AnalogTask.Start();
        }//end of iniNI







        // 启动线程
        public void Start()
        {
            if (running) return;
            running = true;
            time_last = DateTime.Now;
            collectThread.Start();
        }

        // 停止线程
        public void Stop()
        {
            if (!running) return;
            running = false;
            collectThread.Join();
        }

        // 定义线程
        private void dataCollect()
        {
        }
    }
}
