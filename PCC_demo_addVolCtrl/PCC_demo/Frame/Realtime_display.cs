using System.Drawing;
using System;
using System.Reflection;
using System.Drawing.Imaging;
using SQLiteDemo;
using PCC_demo;
using System.Collections.Generic;
using PCC_demo.Processing;
using System.Threading;
using System.Windows.Forms;

namespace Sunny.UI.Demo
{
    public partial class Realtime_display : UIPage
    {
        //for drawing
        double[] data_playing = new double[6];
        double[,] rawData = new double[6, 240];
        double[] pccData = new double[6];

        //for sqlite
        SQLdemo sqlCall = new SQLdemo();

        //for voltageControl
        const int volMin = 0, volMax = 10;
        const double volAdd = 0.1;

        //for UI
        dataCollector collector = new dataCollector();

        public Realtime_display()
        {
            InitializeComponent();
            InitializeCheckbox();
           //sqlCall.initialSQL();

            StyleManager.Style = (UIStyle)101;

            UIWaitForm wait = new UIWaitForm();
            wait.ShowWaitForm("系统正在初始化...");
            
            collector.DataUpdated += displayData;
            collector.SomeMethodRunsEach30s();
            
            //collector.Start();
            wait.HideWaitForm();
        }

        private void Realtime_display_Initialize(object sender, System.EventArgs e)
        {
            index = 0;
            dt_zero = DateTime.Now;
            DateTime dt_newzero = dt_zero;
            if(dt_zero.Second % 10 != 0)
            {
                dt_newzero = dt_zero.ZeroSecond().AddSeconds(dt_zero.Second / 10 * 10);
                dt_zero.SetTime( dt_zero.Hour, dt_zero.Minute, dt_zero.Second / 10 * 10);
            }
            UILineOption option = new UILineOption();
            option.ToolTip.Visible = true;
            option.Title = new UITitle();
            option.Title.Text = "实时煤粉浓度趋势";
            //option.Title.SubText = "LineChart";
            var series1 = option.AddSeries(new UILineSeries("CH1", systemSetting.colors[0]));//绿色
            var series2 = option.AddSeries(new UILineSeries("CH2", systemSetting.colors[1]));//肉黄色
            var series3 = option.AddSeries(new UILineSeries("CH3", systemSetting.colors[2]));//红色
            var series4 = option.AddSeries(new UILineSeries("CH4", systemSetting.colors[3]));//棕色
            var series5 = option.AddSeries(new UILineSeries("CH5", systemSetting.colors[4]));//蓝色
            var series6 = option.AddSeries(new UILineSeries("CH6", systemSetting.colors[5]));//灰色

            //设置曲线显示最大点数，超过后自动清理
            series1.SetMaxCount(50);
            series2.SetMaxCount(50);
            series3.SetMaxCount(50);
            series4.SetMaxCount(50);
            series5.SetMaxCount(50);
            series6.SetMaxCount(50);

            series1.Width = 3;
            series2.Width = 3;
            series3.Width = 3;
            series4.Width = 3;
            series5.Width = 3;
            series6.Width = 3;

            series1.Symbol = UILinePointSymbol.Round;
            series2.Symbol = UILinePointSymbol.Round;
            series3.Symbol = UILinePointSymbol.Round;
            series4.Symbol = UILinePointSymbol.Round;
            series5.Symbol = UILinePointSymbol.Round;
            series6.Symbol = UILinePointSymbol.Round;

            series1.YAxisDecimalPlaces = 2;
            series2.YAxisDecimalPlaces = 2;
            series3.YAxisDecimalPlaces = 2;
            series4.YAxisDecimalPlaces = 2;
            series5.YAxisDecimalPlaces = 2;
            series6.YAxisDecimalPlaces = 2;

            option.XAxis.Name = "时间";
            option.XAxisType = UIAxisType.DateTime;
            option.XAxis.AxisLabel.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            option.YAxis.Name = "煤粉浓度";


            //坐标轴显示小数位数
            option.XAxis.AxisLabel.DecimalPlaces = 2;
            option.YAxis.AxisLabel.DecimalPlaces = 1;
            option.XAxis.SetRange(dt_zero.AddMilliseconds(-dataCollector.timer_interval), dt_zero.AddMilliseconds(50 * dataCollector.timer_interval));
            LineChart.SetOption(option);
            //timer1.Start();
            //timer2.Start();

            collector.UpdateDataDisplay();
        }


        private void InitializeCheckbox()
        {
            for (int i = 0; i < uiCheckBoxGroup1.Items.Count; i++)
            {
                uiCheckBoxGroup1.SetItemCheckState(i, true);
            }
            label1.ForeColor = systemSetting.colors[0];   //绿色
            label2.ForeColor = systemSetting.colors[1];   //肉黄色
            label3.ForeColor = systemSetting.colors[2];   //红色
            label4.ForeColor = systemSetting.colors[3];   //棕色
            label5.ForeColor = systemSetting.colors[4];   //蓝色
            label6.ForeColor = systemSetting.colors[5];   //紫色
        }

        int index = 0;
        DateTime dt_zero = new DateTime(2023, 10, 4);
        Random random = new Random();

        public void displayData(double[] data_playing)
        {
            DateTime dt_now = dataCollector.time_playing;

            foreach (int indexChecked in uiCheckBoxGroup1.SelectedIndexes)
            {
                int i = indexChecked + 1;
                LineChart.Option.AddData("CH" + i.ToString(), dt_now, dataCollector.data_playing[indexChecked]);
            }
            index++;

            if (index > 30)
            {
                LineChart.Option.XAxis.SetRange(dt_now.AddMilliseconds(-30 * dataCollector.timer_interval), dt_now.AddMilliseconds(20 * dataCollector.timer_interval));
                index--;
            }

            LineChart.Refresh();
        }


        private void timer2_Tick(object sender, EventArgs e)
        {   
            double volNo = 0;
            int sensorNo = 0;
            DateTime time = DateTime.Now.AddSeconds(-DateTime.Now.Second).AddMilliseconds(-DateTime.Now.Millisecond);//当前的年月日时分

            double temp = random.NextDouble() * 0.2 + 0.3;
            string rawDataString = "";  // 用于把240个原始值连成字符串

            // rawData get & insert
            for (volNo = volMin; volNo <= volMax; volNo += volAdd)
            {
                for (sensorNo = 0; sensorNo < rawData.GetLength(0); sensorNo++)
                {
                    rawDataString = "";
                    for (int i = 0; i < rawData.GetLength(1); i++)
                    {
                        rawData[sensorNo, i] = random.NextDouble() * 40 - 60;  // 随机赋一个-60~-20之间的值
                        rawDataString += rawData[sensorNo, i].ToString("0.0000") + ", ";
                    }
                    rawDataString = rawDataString.Substring(0, rawDataString.Length - 2);
                    object[] values = new object[] { time, sensorNo, Math.Round(volNo, 1), rawDataString };
                    sqlCall.insertData(time.ToString("yyMMdd"), "raw", values);
                }
            }

            // pccData get & insert
            for (sensorNo = 0; sensorNo < data_playing.Length; sensorNo++)
            {
                data_playing[sensorNo] = temp + random.NextDouble() * 0.05;
                pccData[sensorNo] = temp + random.NextDouble() * 0.05;
                object[] values = new object[] { time, sensorNo, pccData[sensorNo] };
                sqlCall.insertData(time.ToString("yyMMdd"), "PCC", values);
            }
        }

        private void uiCheckBoxGroup1_ValueChanged(object sender, int index, string text, bool isChecked)
        {
            if(isChecked == false)
            {
                LineChart.Option.Clear("CH" + (index + 1).ToString());
            }
        }

        private void Realtime_display_Shown(object sender, EventArgs e)
        {
            this.Activate();
        }

        private void uiSwitch1_ValueChanged(object sender, bool value)
        {
            if(uiSwitch1.Active == true)
            {
                collector.timer.Start();
            }
            else
            {
                collector.timer.Stop();
            }
        }

    }
}