using System.Drawing;
using System;
using System.Reflection;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Collections.Generic;
using SQLiteDemo;
using System.Data;
using PCC_demo;

namespace Sunny.UI.Demo
{
    public partial class Replay_display : UIPage
    {
        //for datetimepicker
        DateTime startTime, endTime;
        public static DateTime SharedStartTime { get; set; }    //静态变量，为了统一两个历史回放的datetimepicker
        public static DateTime SharedEndTime { get; set; }    //静态变量，为了统一两个历史回放的datetimepicker

        //for sqlite
        SQLdemo sqlCall = new SQLdemo();

        //for dataGridView
        DataTable dt = new DataTable();

        public Replay_display()
        {
            InitializeComponent();
            InitializeCheckbox();
            InitializeDatetimepicker();
        }

        private void FTitlePage2_Initialize(object sender, System.EventArgs e)
        {
            //uiDatetimePicker1.Value = Replay_table.SharedStartTime;
            //uiDatetimePicker2.Value = Replay_table.SharedEndTime;

            uiDatetimePicker1.Value = DateTime.Today.AddDays(-3);
            uiDatetimePicker2.Value = DateTime.Now;

            startTime = uiDatetimePicker1.Value;
            endTime = uiDatetimePicker2.Value;

            if (startTime > endTime)
                UIMessageBox.Show("起始时间不能早于结束时间！");
            else
            {
                dt = sqlCall.queryTable(startTime, endTime);
                useDTtodraw();
            }
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

        private void InitializeDatetimepicker()
        {
            uiDatetimePicker1.Value = DateTime.Today.AddDays(-3);
            uiDatetimePicker2.Value = DateTime.Now;

            startTime = uiDatetimePicker1.Value;
            endTime = uiDatetimePicker2.Value;
        }

        private void uiDatetimePicker1_ValueChanged(object sender, DateTime value)
        {
            DateTime newValue = uiDatetimePicker1.Value;
            SharedStartTime = newValue;
        }

        private void uiDatetimePicker2_ValueChanged(object sender, DateTime value)
        {
            DateTime newValue = uiDatetimePicker2.Value;
            SharedEndTime = newValue;
        }

        private void uiSymbolButton4_Click(object sender, EventArgs e)
        {
            startTime = uiDatetimePicker1.Value;
            endTime = uiDatetimePicker2.Value;

            if (startTime > endTime)
                UIMessageBox.Show("起始时间不能早于结束时间！");
            else
            {
                dt = sqlCall.queryTable(startTime, endTime);
                useDTtodraw();
            }
        }

        private void uiCheckBoxGroup1_ValueChanged(object sender, int index, string text, bool isChecked)
        {
            if (isChecked)
            {
                if(LineChart.Option.ExistsSeries("CH" + (index + 1).ToString()))
                {
                    LineChart.Option.Series["CH" + (index + 1).ToString()].Color = systemSetting.colors[index];
                    LineChart.Refresh();
                }
            }
            else
            {
                if (LineChart.Option.ExistsSeries("CH" + (index + 1).ToString()))
                {
                    LineChart.Option.Series["CH" + (index + 1).ToString()].Color = Color.Transparent;
                    LineChart.Refresh();
                }
            }
        }

        private void useDTtodraw()
        {
            UILineOption option = new UILineOption();
            option.ToolTip.Visible = true;
            option.Title = new UITitle();
            option.Title.Text = "历史煤粉浓度趋势";

            option.XAxisType = UIAxisType.DateTime;

            var series1 = option.AddSeries(new UILineSeries("CH1", systemSetting.colors[0]));//绿色
            var series2 = option.AddSeries(new UILineSeries("CH2", systemSetting.colors[1]));//肉黄色
            var series3 = option.AddSeries(new UILineSeries("CH3", systemSetting.colors[2]));//红色
            var series4 = option.AddSeries(new UILineSeries("CH4", systemSetting.colors[3]));//棕色
            var series5 = option.AddSeries(new UILineSeries("CH5", systemSetting.colors[4]));//蓝色
            var series6 = option.AddSeries(new UILineSeries("CH6", systemSetting.colors[5]));//灰色

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

            option.XAxis.AxisLabel.DecimalPlaces = 2;
            option.YAxis.AxisLabel.DecimalPlaces = 1;

            foreach (DataRow row in dt.Rows)
            {
                DateTime time = Convert.ToDateTime(row["time"]);
                int sensor = Convert.ToInt32(row["sensor"]);
                double value = Convert.ToDouble(row["value"]);
                option.AddData("CH" + sensor.ToString(), time, value);
            }

            option.XAxis.SetRange(startTime, endTime);

            LineChart.SetOption(option);
        }
    }
}