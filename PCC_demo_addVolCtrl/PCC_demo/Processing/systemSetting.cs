using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PCC_demo
{
    internal class systemSetting
    {
        public static readonly Color[] colors;
        public static readonly string dbConnStr = "Data Source=D:\\software\\visualstudio\\230718六通道持续采集_UIupdated\\database_test\\PCCdata_test.db;Version=3;";

        public static string projectPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        static systemSetting()
        {
            colors = new Color[6];  
            colors[0] = Color.FromArgb(078, 171, 144); // 绿色
            colors[1] = Color.FromArgb(238, 191, 101); // 肉黄色
            colors[2] = Color.FromArgb(217, 079, 051); // 红色
            colors[3] = Color.FromArgb(131, 064, 038); // 棕色
            colors[4] = Color.FromArgb(075, 116, 178); // 蓝色
            colors[5] = Color.FromArgb(069, 042, 061); // 紫色
        }
    }
}
