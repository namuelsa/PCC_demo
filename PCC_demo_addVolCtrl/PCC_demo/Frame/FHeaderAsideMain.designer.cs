namespace Sunny.UI.Demo
{
    partial class FHeaderAsideMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FHeaderAsideMain));
            this.uiAvatar2 = new Sunny.UI.UIAvatar();
            this.uiLabel1 = new Sunny.UI.UILabel();
            this.uiLabel2 = new Sunny.UI.UILabel();
            this.uiLabel3 = new Sunny.UI.UILabel();
            this.uiLabel4 = new Sunny.UI.UILabel();
            this.uiStyleManager1 = new Sunny.UI.UIStyleManager(this.components);
            this.uiPanel3 = new Sunny.UI.UIPanel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.Header.SuspendLayout();
            this.SuspendLayout();
            // 
            // Aside
            // 
            this.Aside.LineColor = System.Drawing.Color.Black;
            this.Aside.Size = new System.Drawing.Size(250, 895);
            this.Aside.Style = Sunny.UI.UIStyle.DarkBlue;
            // 
            // Header
            // 
            this.Header.Controls.Add(this.uiLabel4);
            this.Header.Controls.Add(this.uiLabel3);
            this.Header.Controls.Add(this.uiLabel2);
            this.Header.Controls.Add(this.uiLabel1);
            this.Header.Controls.Add(this.uiAvatar2);
            this.Header.Size = new System.Drawing.Size(1920, 110);
            this.Header.Style = Sunny.UI.UIStyle.DarkBlue;
            // 
            // uiAvatar2
            // 
            this.uiAvatar2.AvatarSize = 80;
            this.uiAvatar2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiAvatar2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(30)))), ((int)(((byte)(63)))));
            this.uiAvatar2.Icon = Sunny.UI.UIAvatar.UIIcon.Image;
            this.uiAvatar2.Image = global::PCC_demo.Properties.Resources.浙能logo;
            this.uiAvatar2.Location = new System.Drawing.Point(17, 12);
            this.uiAvatar2.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiAvatar2.Name = "uiAvatar2";
            this.uiAvatar2.Shape = Sunny.UI.UIShape.Square;
            this.uiAvatar2.Size = new System.Drawing.Size(90, 86);
            this.uiAvatar2.Style = Sunny.UI.UIStyle.DarkBlue;
            this.uiAvatar2.TabIndex = 27;
            this.uiAvatar2.Text = "uiAvatar2";
            // 
            // uiLabel1
            // 
            this.uiLabel1.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel1.ForeColor = System.Drawing.Color.Black;
            this.uiLabel1.Location = new System.Drawing.Point(1584, 12);
            this.uiLabel1.Name = "uiLabel1";
            this.uiLabel1.Size = new System.Drawing.Size(307, 40);
            this.uiLabel1.Style = Sunny.UI.UIStyle.Custom;
            this.uiLabel1.StyleCustomMode = true;
            this.uiLabel1.TabIndex = 28;
            this.uiLabel1.Text = "微波煤粉浓度实时检测系统";
            this.uiLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiLabel2
            // 
            this.uiLabel2.BackColor = System.Drawing.Color.Transparent;
            this.uiLabel2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel2.ForeColor = System.Drawing.Color.Black;
            this.uiLabel2.Location = new System.Drawing.Point(1587, 52);
            this.uiLabel2.Name = "uiLabel2";
            this.uiLabel2.Size = new System.Drawing.Size(307, 40);
            this.uiLabel2.Style = Sunny.UI.UIStyle.Custom;
            this.uiLabel2.StyleCustomMode = true;
            this.uiLabel2.TabIndex = 29;
            this.uiLabel2.Text = "Microwave-based Real-time Pulverized Coal Concentration Detection System ";
            this.uiLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiLabel3
            // 
            this.uiLabel3.Font = new System.Drawing.Font("微软雅黑", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel3.ForeColor = System.Drawing.Color.Black;
            this.uiLabel3.Location = new System.Drawing.Point(109, 18);
            this.uiLabel3.Name = "uiLabel3";
            this.uiLabel3.Size = new System.Drawing.Size(407, 60);
            this.uiLabel3.Style = Sunny.UI.UIStyle.Custom;
            this.uiLabel3.StyleCustomMode = true;
            this.uiLabel3.TabIndex = 30;
            this.uiLabel3.Text = "浙江浙能嘉华发电有限公司";
            this.uiLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiLabel4
            // 
            this.uiLabel4.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel4.ForeColor = System.Drawing.Color.Black;
            this.uiLabel4.Location = new System.Drawing.Point(113, 65);
            this.uiLabel4.Name = "uiLabel4";
            this.uiLabel4.Size = new System.Drawing.Size(358, 27);
            this.uiLabel4.Style = Sunny.UI.UIStyle.Custom;
            this.uiLabel4.StyleCustomMode = true;
            this.uiLabel4.TabIndex = 30;
            this.uiLabel4.Text = "Zhejiang Zheneng Jiahua Power Generator Co., Ltd";
            this.uiLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiStyleManager1
            // 
            this.uiStyleManager1.DPIScale = true;
            this.uiStyleManager1.Style = Sunny.UI.UIStyle.LayuiGreen;
            // 
            // uiPanel3
            // 
            this.uiPanel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.uiPanel3.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(30)))), ((int)(((byte)(63)))));
            this.uiPanel3.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(30)))), ((int)(((byte)(63)))));
            this.uiPanel3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiPanel3.ForeColor = System.Drawing.Color.Silver;
            this.uiPanel3.Location = new System.Drawing.Point(250, 1008);
            this.uiPanel3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel3.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel3.Name = "uiPanel3";
            this.uiPanel3.Radius = 1;
            this.uiPanel3.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.uiPanel3.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(58)))), ((int)(((byte)(92)))));
            this.uiPanel3.RectSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)));
            this.uiPanel3.Size = new System.Drawing.Size(1670, 32);
            this.uiPanel3.Style = Sunny.UI.UIStyle.Custom;
            this.uiPanel3.StyleCustomMode = true;
            this.uiPanel3.TabIndex = 3;
            this.uiPanel3.Text = null;
            this.uiPanel3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FHeaderAsideMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1920, 1040);
            this.ControlBoxFillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(75)))), ((int)(((byte)(101)))));
            this.Controls.Add(this.uiPanel3);
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(1920, 1080);
            this.Name = "FHeaderAsideMain";
            this.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(58)))), ((int)(((byte)(92)))));
            this.ShowFullScreen = true;
            this.Style = Sunny.UI.UIStyle.DarkBlue;
            this.Text = "微波煤粉浓度实时检测系统";
            this.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(26)))), ((int)(((byte)(55)))));
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ZoomScaleRect = new System.Drawing.Rectangle(15, -28, 1024, 720);
            this.Controls.SetChildIndex(this.Header, 0);
            this.Controls.SetChildIndex(this.Aside, 0);
            this.Controls.SetChildIndex(this.uiPanel3, 0);
            this.Header.ResumeLayout(false);
            this.ResumeLayout(false);
        }
        #endregion

        private UIAvatar uiAvatar2;
        private UILabel uiLabel2;
        private UILabel uiLabel1;
        private UILabel uiLabel4;
        private UIStyleManager uiStyleManager1;
        private UILabel uiLabel3;
        private UIPanel uiPanel3;
        private System.Windows.Forms.Timer timer1;
    }
}