using NPOI.OpenXmlFormats.Dml.Diagram;
using PCC_demo;
using Sunny.UI;
using System;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.Data.SQLite;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQLiteDemo
{
    class SQLdemo
    {
        SQLiteConnection dbConn;

        // 初始化
        public void initialSQL()
        {
            connectSQL();
            if (!File.Exists("PCCdata_test.db"))
            {
                SQLiteConnection.CreateFile("PCCdata_test.db");
            }

            string dateString = DateTime.Now.ToString("yyMMdd");
            createTable(dateString, "raw");
            createTable(dateString, "PCC");
        }

        // 连接到SQLite
        public void connectSQL()
        {
            dbConn = new SQLiteConnection(systemSetting.dbConnStr);
            dbConn.Open();
        }

        // 创建表
        public void createTable(string tableName, string catName)
        {
            string table_Name = "", CommandText = "";
            switch (catName)
            {
                case "raw":
                    table_Name = "_raw_" + tableName + "_";
                    CommandText = "CREATE TABLE IF NOT EXISTS " + table_Name + " (time DATETIME, sensor INTEGER, vol REAL, data TEXT)";
                    break;
                case "PCC":
                    table_Name = "_PCC_" + tableName + "_";
                    CommandText = "CREATE TABLE IF NOT EXISTS " + table_Name + " (time DATETIME, sensor INTEGER, value REAL)";
                    break;
            }
            SQLiteCommand command = new SQLiteCommand(CommandText, dbConn);
            command.ExecuteNonQuery();

            createIndex(tableName, catName);
        }

        // 创建索引
        public void createIndex(string tableName, string catName)
        {
            string table_Name = "", CommandText = "";
            switch (catName)
            {
                case "raw":
                    table_Name = "_raw_" + tableName + "_";
                    break;
                case "PCC":
                    table_Name = "_PCC_" + tableName + "_";
                    break;
            }
            // 创建组合索引
            CommandText = $"CREATE INDEX IF NOT EXISTS {table_Name}_time_sensor_index ON {table_Name} (time, sensor)";
            SQLiteCommand command = new SQLiteCommand(CommandText, dbConn);
            command.ExecuteNonQuery();
        }

        // 插入数据
        public void insertData(string tableName, string catName, object[] values)
        {
            using (dbConn = new SQLiteConnection(systemSetting.dbConnStr))
            {
                dbConn.Open();
                SQLiteCommand command = new SQLiteCommand(dbConn);
                switch (catName)
                {
                    case "raw":
                        command.CommandText = "INSERT INTO _raw_" + tableName + "_ (time, sensor, vol, data) VALUES (@time, @sensor, @vol, @data)";
                        command.Parameters.AddWithValue("@time", values[0]);
                        command.Parameters.AddWithValue("@sensor", values[1]);
                        command.Parameters.AddWithValue("@vol", values[2]);
                        command.Parameters.AddWithValue("@data", values[3]);
                        break;
                    case "PCC":
                        command.CommandText = "INSERT INTO _PCC_" + tableName + "_ (time, sensor, value) VALUES (@time, @sensor, @value)";
                        command.Parameters.AddWithValue("@time", values[0]);
                        command.Parameters.AddWithValue("@sensor", values[1]);
                        command.Parameters.AddWithValue("@value", values[2]);
                        break;
                }
                command.ExecuteNonQuery();
            }
        }


        // 插入数据(异步方法）
        public async Task insertDataAsync(string tableName, string catName, object[] values)
        {
            dbConn = new SQLiteConnection(systemSetting.dbConnStr);
            await dbConn.OpenAsync();
            SQLiteCommand command = new SQLiteCommand(dbConn);
            switch (catName)
            {
                case "raw":
                    command.CommandText = "INSERT INTO _raw_" + tableName + "_ (time, sensor, vol, data) VALUES (@time, @sensor, @vol, @data)";
                    command.Parameters.AddWithValue("@time", values[0]);
                    command.Parameters.AddWithValue("@sensor", values[1]);
                    command.Parameters.AddWithValue("@vol", values[2]);
                    command.Parameters.AddWithValue("@data", values[3]);
                    break;
                case "PCC":
                    command.CommandText = "INSERT INTO _PCC_" + tableName + "_ (time, sensor, value) VALUES (@time, @sensor, @value)";
                    command.Parameters.AddWithValue("@time", values[0]);
                    command.Parameters.AddWithValue("@sensor", values[1]);
                    command.Parameters.AddWithValue("@value", values[2]);
                    break;
            }
            command.ExecuteNonQuery();
        }

        // 删除指定的表
        public void deleteTable(string tableName, string catName)
        {
            int result = 0;

            SQLiteCommand command = new SQLiteCommand(dbConn);
            string table_Name = "", CommandText = "";
            switch (catName)
            {
                case "raw":
                    table_Name = "_raw_" + tableName + "_";
                    break;
                case "PCC":
                    table_Name = "_PCC_" + tableName + "_";
                    break;
            }
            command.CommandText = "DROP TABLE IF EXISTS " + table_Name;
            command.ExecuteNonQuery();
            
        }


        // 查询指定日期之内的数据
        public DataTable queryTable(DateTime start, DateTime end)
        {
            connectSQL();
            StringBuilder sb = new StringBuilder();
            DateTime date = start;
            bool first = true; // 用于标记是否是第一个表
            while (date <= end)
            {
                string tableName = "_PCC_" + date.ToString("yyMMdd") + "_";
                if (Exists(tableName))
                {
                    // 如果存在，根据是否是第一个表来拼接SQL语句
                    if (first)
                    {
                        sb.Append("SELECT * FROM " + tableName);
                        first = false; // 将标记设为false
                    }
                    else
                    {
                        sb.Append(" UNION ALL SELECT * FROM " + tableName);
                    }
                }
                date = date.AddDays(1);
            }
            if (first) return new DataTable();

            sb.Append(" WHERE datetime(time) BETWEEN ");
            sb.Append("'" + start.ToString("yyyy-MM-dd HH:mm:ss") + "'");
            sb.Append(" AND ");
            sb.Append("'" + end.ToString("yyyy-MM-dd HH:mm:ss") + "'");

            string CommandText = sb.ToString();

            // 查询数据
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(CommandText, dbConn);

            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }

        // 判断表是否存在
        public bool Exists(string tableName)
        {
            connectSQL();
            string CommandText = $"SELECT name FROM sqlite_master WHERE type='table' AND name='{tableName}'";
            SQLiteCommand command = new SQLiteCommand(CommandText, dbConn);
            object result = command.ExecuteScalar();
            return result != null;
        }

    }
}