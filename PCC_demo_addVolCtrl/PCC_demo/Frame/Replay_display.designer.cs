namespace Sunny.UI.Demo
{
    partial class Replay_display
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Replay_display));
            this.uiCheckBoxGroup1 = new Sunny.UI.UICheckBoxGroup();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.StyleManager = new Sunny.UI.UIStyleManager(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.uiDatetimePicker1 = new Sunny.UI.UIDatetimePicker();
            this.uiSymbolLabel1 = new Sunny.UI.UISymbolLabel();
            this.uiLabel1 = new Sunny.UI.UILabel();
            this.uiDatetimePicker2 = new Sunny.UI.UIDatetimePicker();
            this.uiSymbolButton4 = new Sunny.UI.UISymbolButton();
            this.LineChart = new Sunny.UI.UILineChart();
            this.uiCheckBoxGroup1.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiCheckBoxGroup1
            // 
            this.uiCheckBoxGroup1.Controls.Add(this.label6);
            this.uiCheckBoxGroup1.Controls.Add(this.label5);
            this.uiCheckBoxGroup1.Controls.Add(this.label4);
            this.uiCheckBoxGroup1.Controls.Add(this.label3);
            this.uiCheckBoxGroup1.Controls.Add(this.label2);
            this.uiCheckBoxGroup1.Controls.Add(this.label1);
            this.uiCheckBoxGroup1.Dock = System.Windows.Forms.DockStyle.Right;
            this.uiCheckBoxGroup1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(30)))), ((int)(((byte)(63)))));
            this.uiCheckBoxGroup1.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(30)))), ((int)(((byte)(63)))));
            this.uiCheckBoxGroup1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiCheckBoxGroup1.ForeColor = System.Drawing.Color.White;
            this.uiCheckBoxGroup1.Items.AddRange(new object[] {
            "1#",
            "2#",
            "3#",
            "4#",
            "5#",
            "6#"});
            this.uiCheckBoxGroup1.ItemSize = new System.Drawing.Size(110, 35);
            this.uiCheckBoxGroup1.Location = new System.Drawing.Point(1526, 35);
            this.uiCheckBoxGroup1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiCheckBoxGroup1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiCheckBoxGroup1.Name = "uiCheckBoxGroup1";
            this.uiCheckBoxGroup1.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            this.uiCheckBoxGroup1.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(58)))), ((int)(((byte)(92)))));
            this.uiCheckBoxGroup1.SelectedIndexes = ((System.Collections.Generic.List<int>)(resources.GetObject("uiCheckBoxGroup1.SelectedIndexes")));
            this.uiCheckBoxGroup1.Size = new System.Drawing.Size(196, 825);
            this.uiCheckBoxGroup1.Style = Sunny.UI.UIStyle.DarkBlue;
            this.uiCheckBoxGroup1.TabIndex = 53;
            this.uiCheckBoxGroup1.Text = "通道筛选";
            this.uiCheckBoxGroup1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiCheckBoxGroup1.ValueChanged += new Sunny.UI.UICheckBoxGroup.OnValueChanged(this.uiCheckBoxGroup1_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(75, 207);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 22);
            this.label6.TabIndex = 63;
            this.label6.Text = "——";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(75, 172);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 22);
            this.label5.TabIndex = 62;
            this.label5.Text = "——";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(75, 137);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 22);
            this.label4.TabIndex = 61;
            this.label4.Text = "——";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(75, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 22);
            this.label3.TabIndex = 60;
            this.label3.Text = "——";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(75, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 22);
            this.label2.TabIndex = 59;
            this.label2.Text = "——";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(75, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 22);
            this.label1.TabIndex = 58;
            this.label1.Text = "——";
            // 
            // StyleManager
            // 
            this.StyleManager.DPIScale = true;
            this.StyleManager.Style = Sunny.UI.UIStyle.DarkBlue;
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            // 
            // uiDatetimePicker1
            // 
            this.uiDatetimePicker1.CanEmpty = true;
            this.uiDatetimePicker1.DateFormat = "yyyy-MM-dd HH:mm";
            this.uiDatetimePicker1.FillColor = System.Drawing.Color.White;
            this.uiDatetimePicker1.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(30)))), ((int)(((byte)(63)))));
            this.uiDatetimePicker1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiDatetimePicker1.Location = new System.Drawing.Point(161, 50);
            this.uiDatetimePicker1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiDatetimePicker1.MaxLength = 16;
            this.uiDatetimePicker1.MinimumSize = new System.Drawing.Size(63, 0);
            this.uiDatetimePicker1.Name = "uiDatetimePicker1";
            this.uiDatetimePicker1.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.uiDatetimePicker1.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(58)))), ((int)(((byte)(92)))));
            this.uiDatetimePicker1.ShowToday = true;
            this.uiDatetimePicker1.Size = new System.Drawing.Size(223, 29);
            this.uiDatetimePicker1.Style = Sunny.UI.UIStyle.DarkBlue;
            this.uiDatetimePicker1.SymbolDropDown = 61555;
            this.uiDatetimePicker1.SymbolNormal = 61555;
            this.uiDatetimePicker1.TabIndex = 63;
            this.uiDatetimePicker1.Text = "2020-06-02 17:57";
            this.uiDatetimePicker1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiDatetimePicker1.Value = new System.DateTime(2020, 6, 2, 17, 57, 28, 203);
            this.uiDatetimePicker1.Watermark = "";
            this.uiDatetimePicker1.ValueChanged += new Sunny.UI.UIDatetimePicker.OnDateTimeChanged(this.uiDatetimePicker1_ValueChanged);
            // 
            // uiSymbolLabel1
            // 
            this.uiSymbolLabel1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiSymbolLabel1.ForeColor = System.Drawing.Color.White;
            this.uiSymbolLabel1.Location = new System.Drawing.Point(50, 55);
            this.uiSymbolLabel1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiSymbolLabel1.Name = "uiSymbolLabel1";
            this.uiSymbolLabel1.Size = new System.Drawing.Size(91, 24);
            this.uiSymbolLabel1.Style = Sunny.UI.UIStyle.DarkBlue;
            this.uiSymbolLabel1.Symbol = 261463;
            this.uiSymbolLabel1.SymbolColor = System.Drawing.Color.White;
            this.uiSymbolLabel1.TabIndex = 40;
            this.uiSymbolLabel1.Text = "起止时间";
            this.uiSymbolLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiLabel1
            // 
            this.uiLabel1.AutoSize = true;
            this.uiLabel1.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel1.ForeColor = System.Drawing.Color.White;
            this.uiLabel1.Location = new System.Drawing.Point(402, 52);
            this.uiLabel1.Name = "uiLabel1";
            this.uiLabel1.Size = new System.Drawing.Size(32, 27);
            this.uiLabel1.Style = Sunny.UI.UIStyle.DarkBlue;
            this.uiLabel1.TabIndex = 64;
            this.uiLabel1.Text = "→";
            this.uiLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiDatetimePicker2
            // 
            this.uiDatetimePicker2.CanEmpty = true;
            this.uiDatetimePicker2.DateFormat = "yyyy-MM-dd HH:mm";
            this.uiDatetimePicker2.FillColor = System.Drawing.Color.White;
            this.uiDatetimePicker2.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(30)))), ((int)(((byte)(63)))));
            this.uiDatetimePicker2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiDatetimePicker2.Location = new System.Drawing.Point(458, 50);
            this.uiDatetimePicker2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiDatetimePicker2.MaxLength = 16;
            this.uiDatetimePicker2.MinimumSize = new System.Drawing.Size(63, 0);
            this.uiDatetimePicker2.Name = "uiDatetimePicker2";
            this.uiDatetimePicker2.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.uiDatetimePicker2.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(58)))), ((int)(((byte)(92)))));
            this.uiDatetimePicker2.ShowToday = true;
            this.uiDatetimePicker2.Size = new System.Drawing.Size(223, 29);
            this.uiDatetimePicker2.Style = Sunny.UI.UIStyle.DarkBlue;
            this.uiDatetimePicker2.SymbolDropDown = 61555;
            this.uiDatetimePicker2.SymbolNormal = 61555;
            this.uiDatetimePicker2.TabIndex = 64;
            this.uiDatetimePicker2.Text = "2020-06-02 17:57";
            this.uiDatetimePicker2.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiDatetimePicker2.Value = new System.DateTime(2020, 6, 2, 17, 57, 28, 203);
            this.uiDatetimePicker2.Watermark = "";
            this.uiDatetimePicker2.ValueChanged += new Sunny.UI.UIDatetimePicker.OnDateTimeChanged(this.uiDatetimePicker2_ValueChanged);
            // 
            // uiSymbolButton4
            // 
            this.uiSymbolButton4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiSymbolButton4.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiSymbolButton4.Location = new System.Drawing.Point(713, 50);
            this.uiSymbolButton4.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiSymbolButton4.Name = "uiSymbolButton4";
            this.uiSymbolButton4.Size = new System.Drawing.Size(91, 29);
            this.uiSymbolButton4.Style = Sunny.UI.UIStyle.DarkBlue;
            this.uiSymbolButton4.StyleCustomMode = true;
            this.uiSymbolButton4.Symbol = 61950;
            this.uiSymbolButton4.TabIndex = 80;
            this.uiSymbolButton4.Text = "查询";
            this.uiSymbolButton4.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiSymbolButton4.Click += new System.EventHandler(this.uiSymbolButton4_Click);
            // 
            // LineChart
            // 
            this.LineChart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LineChart.ChartStyleType = Sunny.UI.UIChartStyleType.LiveChart;
            this.LineChart.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(36)))), ((int)(((byte)(71)))));
            this.LineChart.Font = new System.Drawing.Font("微软雅黑", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LineChart.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            this.LineChart.LegendFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LineChart.Location = new System.Drawing.Point(50, 102);
            this.LineChart.MinimumSize = new System.Drawing.Size(1, 1);
            this.LineChart.Name = "LineChart";
            this.LineChart.Size = new System.Drawing.Size(1407, 711);
            this.LineChart.Style = Sunny.UI.UIStyle.DarkBlue;
            this.LineChart.SubFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LineChart.TabIndex = 54;
            this.LineChart.Text = "uiLineChart1";
            // 
            // Replay_display
            // 
            this.AllowShowTitle = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1722, 860);
            this.Controls.Add(this.uiSymbolButton4);
            this.Controls.Add(this.uiDatetimePicker2);
            this.Controls.Add(this.uiLabel1);
            this.Controls.Add(this.uiSymbolLabel1);
            this.Controls.Add(this.uiDatetimePicker1);
            this.Controls.Add(this.LineChart);
            this.Controls.Add(this.uiCheckBoxGroup1);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "Replay_display";
            this.Padding = new System.Windows.Forms.Padding(0, 35, 0, 0);
            this.PageIndex = 1001;
            this.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(30)))), ((int)(((byte)(63)))));
            this.ShowTitle = true;
            this.Style = Sunny.UI.UIStyle.DarkBlue;
            this.Symbol = 61950;
            this.Text = "可视化展示";
            this.Initialize += new System.EventHandler(this.FTitlePage2_Initialize);
            this.uiCheckBoxGroup1.ResumeLayout(false);
            this.uiCheckBoxGroup1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private UICheckBoxGroup uiCheckBoxGroup1;
        private System.Windows.Forms.Timer timer1;
        private UIStyleManager StyleManager;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private UIDatetimePicker uiDatetimePicker1;
        private UISymbolLabel uiSymbolLabel1;
        private UILabel uiLabel1;
        private UIDatetimePicker uiDatetimePicker2;
        private UISymbolButton uiSymbolButton4;
        private UILineChart LineChart;
    }
}