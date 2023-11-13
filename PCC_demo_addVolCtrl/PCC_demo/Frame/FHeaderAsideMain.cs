using System;
using System.Drawing;
using System.Windows.Forms;

namespace Sunny.UI.Demo
{
    public partial class FHeaderAsideMain : UIHeaderAsideMainFrame
    {
        public FHeaderAsideMain()
        {
            InitializeComponent();

            //设置关联
            Aside.TabControl = MainTabControl;

            //增加页面到Main
            AddPage(new Realtime_display(), 1001);
            AddPage(new Replay_display(), 1002);

            //设置Header节点索引
            Aside.CreateNode("煤粉实时浓度", 1001);
            Aside.CreateNode("历史回放", 1002);

            //显示默认界面
            Aside.SelectFirst();

            uiStyleManager1.Style = (UIStyle)101;
        }

        private void timer1_Tick(object sender, System.EventArgs e)
        {
            uiPanel3.Text = DateTime.Now.DateTimeString();
        }
    }
}
