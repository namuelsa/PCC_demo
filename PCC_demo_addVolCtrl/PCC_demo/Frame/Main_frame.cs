using System;
using System.Threading;
using System.Windows.Forms;

namespace Sunny.UI.Demo
{
    public partial class Main_frame : UIHeaderAsideMainFooterFrame
    {
        public Main_frame()
        {
            InitializeComponent();

            //设置关联
            Aside.TabControl = MainTabControl;

            int pageIndex = 1000;
            //左侧导航主节点关联页面
            Aside.CreateNode(AddPage(new Realtime_display(), pageIndex));

            pageIndex = 2000;
            TreeNode parent = Aside.CreateNode("历史回放", 61451, 24, pageIndex);

            //通过设置PageIndex关联，节点文字、图标由相应的Page的Text、Symbol提供
            Aside.CreateChildNode(parent, AddPage(new Replay_display(), ++pageIndex));
            Aside.CreateChildNode(parent, AddPage(new Replay_table(), ++pageIndex));

            //显示默认界面
            Aside.SelectFirst();

            uiStyleManager1.Style = (UIStyle)101;

        }

        private void Aside_MenuItemClick(System.Windows.Forms.TreeNode node, NavMenuItem item, int pageIndex)
        {
        }

        private void timer1_Tick(object sender, System.EventArgs e)
        {
            Footer.Text = DateTime.Now.DateTimeString();
        }
    }
}
