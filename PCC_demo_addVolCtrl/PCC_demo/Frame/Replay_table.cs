using System.Drawing;
using System;
using System.Reflection;
using System.Drawing.Imaging;
using SQLiteDemo;
using System.Data;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Collections.Generic;

namespace Sunny.UI.Demo
{
    public partial class Replay_table : UIPage
    {
        //for datetimepicker
        DateTime startTime, endTime;
        public static DateTime SharedStartTime { get; set; }    //静态变量，为了统一两个历史回放的datetimepicker
        public static DateTime SharedEndTime { get; set; }    //静态变量，为了统一两个历史回放的datetimepicker

        //for sqlite
        SQLdemo sqlCall = new SQLdemo();

        //for dataGridView
        DataTable dt = new DataTable();

        public Replay_table()
        {
            InitializeComponent();
            InitializeCheckbox();
            InitializeDatetimepicker();

            sqlCall.connectSQL();
        }

        private void Replay_table_Initialize(object sender, System.EventArgs e)
        {
            uiDatetimePicker1.Value = Replay_display.SharedStartTime;
            uiDatetimePicker2.Value = Replay_display.SharedEndTime;

            if (startTime > endTime)
                UIMessageBox.Show("起始时间不能早于结束时间！");
            else
            {
                dt = sqlCall.queryTable(uiDatetimePicker1.Value, uiDatetimePicker2.Value);
                uiDataGridView1.DataSource = dt;
                uiDataGridView1.Columns[2].DefaultCellStyle.Format = "N3";  //"N2"表示使用数字格式，并保留两位小数
                uiButton1.Enabled = true;
            }

        }

        private void InitializeCheckbox()
        {
            for (int i = 0; i < uiCheckBoxGroup1.Items.Count; i++)
            {
                uiCheckBoxGroup1.SetItemCheckState(i, true);
            }

            label1.ForeColor = Color.FromArgb(078, 171, 144);   //绿色
            label2.ForeColor = Color.FromArgb(238, 191, 101);   //肉黄色
            label3.ForeColor = Color.FromArgb(217, 079, 051);   //红色
            label4.ForeColor = Color.FromArgb(131, 064, 038);   //棕色
            label5.ForeColor = Color.FromArgb(075, 116, 178);   //蓝色
            label6.ForeColor = Color.FromArgb(069, 042, 061);   //紫色

            uiButton1.Enabled = false;
        }

        private void InitializeDatetimepicker()
        {
            uiDatetimePicker1.Value = DateTime.Today.AddDays(-3);
            uiDatetimePicker2.Value = DateTime.Now;

            startTime = uiDatetimePicker1.Value;
            endTime = uiDatetimePicker2.Value;
        }

        // “应用筛选”
        private void uiButton1_Click(object sender, EventArgs e)
        {
            var item = uiCheckBoxGroup1.SelectedIndexes;

            if (item != null)
            {
                string filter = "";
                string filterStr = "";

                foreach (var index in item)
                {
                    filter += (index + 1).ToString() + ", ";
                }
                filter = filter.Substring(0, filter.Length - 2);
                filterStr = "sensor IN (" + filter + ")";

                DataRow[] rows = dt.Select(filterStr);
                DataTable dtFiltered = dt.Clone();

                foreach (DataRow row in rows)
                {
                    dtFiltered.ImportRow(row);
                }
                uiDataGridView1.DataSource = dtFiltered;
            }
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


        // “查询”
        private void uiSymbolButton4_Click(object sender, EventArgs e)
        {
            startTime = uiDatetimePicker1.Value;
            endTime = uiDatetimePicker2.Value;

            if (startTime > endTime)
                UIMessageBox.Show("起始时间不能早于结束时间！");
            else
            {
                dt = sqlCall.queryTable(startTime, endTime);
                uiDataGridView1.DataSource = dt;
                uiDataGridView1.Columns[2].DefaultCellStyle.Format = "N3";  //"N2"表示使用数字格式，并保留两位小数
                uiButton1.Enabled = true;
            }
        }
    }
}