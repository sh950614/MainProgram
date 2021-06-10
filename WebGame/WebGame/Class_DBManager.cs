using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebGame
{
    public class Class_DBManager
    {
        //創一個變數存放從config內的資訊，其實也可不用創立這變數，直接放進SqlConnection內即可。
        private readonly string connStr = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["WebGameConnectionString"].ConnectionString;
        //private readonly string connStr = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=account;Persist Security Info=True;User ID=lccnet;Password=lccnet;Trusted_Connection=True";
          

        public List<Class_Record> getRecord(string item, string table, string condition)
        {
            List<Class_Record> games = new List<Class_Record>();
            string sqlStr = "select " + item + " from " + table + " " + condition;
            int i = 1;

            //new一個SqlConnection物件，是與資料庫連結的通道(其名為Connection)，以s_data內的連接字串連接所對應的資料庫。
            SqlConnection sqlConnection = new SqlConnection(connStr);
            //new一個SqlCommand告訴這個物件準備要執行什麼SQL指令
            SqlCommand sqlCommand = new SqlCommand(sqlStr, sqlConnection);
            //與資料庫連接的通道開啟
            sqlConnection.Open();
            //new一個DataReader接取Execute所回傳的資料。
            SqlDataReader reader = sqlCommand.ExecuteReader();
            //檢查是否有資料列
            if (reader.HasRows)
            {
                //使用Read方法把資料讀進Reader，讓Reader一筆一筆順向指向資料列，並回傳是否成功。
                while (reader.Read())
                {
                    //DataReader讀出欄位內資料的方式，通常也可寫Reader[0]、[1]...[N]代表第一個欄位到N個欄位。
                    //string results = reader[item].ToString().Trim();

                    //用Class_Record來儲存各項資料
                    Class_Record account = new Class_Record
                    {
                        rank = i,
                        game = reader.GetString(reader.GetOrdinal("game")),
                        username = reader.GetString(reader.GetOrdinal("username")),
                        recordtype = reader.GetString(reader.GetOrdinal("recordtype")),
                        recordvalue = reader.GetInt32(reader.GetOrdinal("recordvalue")),
                        recordtime = reader.GetDateTime(reader.GetOrdinal("recordtime")),
                    };

                    //用List來儲存各筆資料
                    games.Add(account);
                    i++;
                }
            }
            else
            {
                Console.WriteLine("資料庫為空");
            }
            //關閉與資料庫連接的通道
            sqlConnection.Close();
            
            //return results;
            return games;
        }


        public void newRecord(Class_Record record)
        {
            string addStr = @"insert into Record(game,username,recordtype,recordvalue,recordtime) VALUES(@game,@username,@recordtype,@recordvalue,@recordtime)";

            SqlConnection sqlConnection = new SqlConnection(connStr);
            SqlCommand sqlCommand = new SqlCommand(addStr);
            sqlCommand.Connection = sqlConnection;

            sqlCommand.Parameters.Add(new SqlParameter("@game", record.game));
            sqlCommand.Parameters.Add(new SqlParameter("@username", record.username));
            sqlCommand.Parameters.Add(new SqlParameter("@recordtype", record.recordtype));
            sqlCommand.Parameters.Add(new SqlParameter("@recordvalue", record.recordvalue));
            sqlCommand.Parameters.Add(new SqlParameter("@recordtime", record.recordtime));

            sqlConnection.Open();
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }


    }
}