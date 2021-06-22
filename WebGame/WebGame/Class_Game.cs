using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace WebGame
{
    public class Class_Game
    {

        public void record(String a_game_name, String a_user_name, String a_record_type, int a_record_value)
        {
            Class_Record record = new Class_Record();
            record.game = a_game_name;
            record.username = a_user_name;
            record.recordtype = a_record_type;
            record.recordvalue = a_record_value;
            record.recordtime = System.DateTime.Now;

            Class_DBManager dBManager = new Class_DBManager();
            dBManager.newRecord(record);
        }


        public void rank(String a_game_name, String a_record_type, GridView gridView)
        {
            Class_DBManager dBManager = new Class_DBManager();
            List<Class_Record> games = new List<Class_Record>();
            string item = "TOP (10) *";
            string table = "Record";
            string condition = "";


            switch (a_record_type)
            {
                case "score":
                    {
                        condition = $"where game = '{a_game_name}' order by recordvalue desc, recordtime";
                    }
                    break;
                case "passtime":
                    {
                        condition = $"where game = '{a_game_name}' order by recordvalue, recordtime";
                    }
                    break;                
            }
            games = dBManager.getRecord(item,table,condition);
            gridView.DataSource = games;           
            gridView.DataBind();
        }      


    }
}