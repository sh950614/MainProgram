using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;

namespace WebGame
{
    public partial class WebForm_InBetween : System.Web.UI.Page
    {
        
        string porkpath = "~/pic/poker/";
        Random random = new Random();


        protected void Page_Load(object sender, EventArgs e)
        {
            Label_Hello.Text = "Hello! "+Convert.ToString(Session["user"]);
            Label_Hello.Font.Size = FontUnit.Larger;
            Label_Hello.Font.Bold = true;
            
            Class_Game newGame = new Class_Game();
            newGame.rank("InBetween", "score", GridView1);

            if (!IsPostBack)
            {
                porkInitial();
            }
        }

        protected void Button_Start_Click(object sender, EventArgs e)
        {
           
            RandomPork(Image1);
            RandomPork(Image2);
            Image3.ImageUrl = porkpath + "bicycle_backs.jpg";
            Button_Start.Enabled = false;
            Button_Bet.Enabled = true;
            Button_Pass.Enabled = true;

            Opening();          
                  
        }



        protected void Button_Bet_Click(object sender, EventArgs e)
        {            
            ArrayList pork = (ArrayList)Session["pork"];
            RandomPork(Image3, false);
            Bet();
            Button_Start.Enabled = true;
            Button_Bet.Enabled = false;
            Button_Pass.Enabled = false;

            if (pork.Count <= 2)
            {
                porkInitial();
                Label_Count.Text = "本輪已結束，新的一輪即將開始";
                Label_Count.Font.Size = FontUnit.Larger;
                Label_Count.Font.Bold = true;
            }           
                
        }

        protected void Button_Pass_Click(object sender, EventArgs e)
        {
            ArrayList pork = (ArrayList)Session["pork"];
            Button_Start.Enabled = true;
            Button_Bet.Enabled = false;
            Button_Pass.Enabled = false;

            if (pork.Count <= 2)
            {
                porkInitial();
                Label_Count.Text = "本輪已結束，新的一輪即將開始";
                Label_Count.Font.Size = FontUnit.Larger;
                Label_Count.Font.Bold = true;
            }
            else
            {
                RandomPork(Image1);
                RandomPork(Image2);
                Image3.ImageUrl = porkpath + "bicycle_backs.jpg";

                Button_Start.Enabled = false;
                Button_Bet.Enabled = true;
                Button_Pass.Enabled = true;
                Opening();
            }            
        }

        protected void Button_Record_Click(object sender, EventArgs e)
        {
            Class_Game newGame = new Class_Game();
            newGame.record("InBetween", Convert.ToString(Session["user"]), "score", Convert.ToInt32(Label_MaxCoinRecord.Text));
            newGame.rank("InBetween", "score", GridView1);
            porkInitial();
            coinInitial();
            Label_Count.Text = "重新開局!";
        }



        protected int ChangeToNumber(Image image)
        {
            int length = image.ImageUrl.Length;
            String pathstring = image.ImageUrl.Substring(length - 6, 2);
            int n = Convert.ToInt32(pathstring);
            int m = n % 13;

            if (m == 0)
            {
                return 13;
            }
            else
            {
                return m;
            }
        }


        protected void porkInitial()
        {
            String key;
            ArrayList pork = new ArrayList();
            for (int i = 1; i <= 52; i++)
            {
                key = i.ToString("00");
                pork.Add(porkpath + "p" + key + ".jpg");
            }
            Session["pork"] = pork;
        }

        protected void coinInitial()
        {
            Label_NowCoin.Text = "50000";
            TextBox_BetCoin.Text = "1000";
            Label_MaxCoinRecord.Text = "50000";
        }


        protected void Opening()
        {
            ArrayList pork = (ArrayList)Session["pork"];
            Label_Count.Text = "本輪剩下" + ((pork.Count / 2) - 1).ToString() + "場";
            Label_Count.Font.Size = FontUnit.Small;
            Label_Count.Font.Bold = false;
            if (Math.Abs(ChangeToNumber(Image1) - ChangeToNumber(Image2)) <= 1)
            {
                Label_Result.Text = "You Lose! Please start again.";
                Label_NowCoin.Text = (Convert.ToInt32(Label_NowCoin.Text) - Convert.ToInt32(TextBox_BetCoin.Text)).ToString();
                Button_Start.Enabled = true;
                Button_Bet.Enabled = false;
                Button_Pass.Enabled = false;

                if (Convert.ToInt32(Label_NowCoin.Text) <= 0)
                {
                    porkInitial();
                    coinInitial();
                    Label_Count.Text = "輸到脫褲子! 重新開局!";
                    Label_Count.Font.Size = FontUnit.Larger;
                    Label_Count.Font.Bold = true;
                }
                else if(pork.Count <= 2)
                {
                    porkInitial();
                    Label_Count.Text = "本輪已結束，新的一輪即將開始";
                    Label_Count.Font.Size = FontUnit.Larger;
                    Label_Count.Font.Bold = true;
                }                
            }
            else
            {
                Label_Result.Text = "Bet or Pass?";
            }
        }


        protected void Bet()
        {
            if ((ChangeToNumber(Image3) - ChangeToNumber(Image1)) * (ChangeToNumber(Image3) - ChangeToNumber(Image2)) == 0)
            {
                Label_Result.Text = "Bump! You Lose!";
                Label_NowCoin.Text = (Convert.ToInt32(Label_NowCoin.Text) - 2*Convert.ToInt32(TextBox_BetCoin.Text)).ToString();
                if (Convert.ToInt32(Label_NowCoin.Text) <= 0)
                {
                    porkInitial();
                    coinInitial();
                    Label_Count.Text = "輸到脫褲子! 重新開局!";
                    Label_Count.Font.Size = FontUnit.Larger;
                    Label_Count.Font.Bold = true;
                }
            }
            else if((ChangeToNumber(Image3) - ChangeToNumber(Image1)) * (ChangeToNumber(Image3) - ChangeToNumber(Image2)) > 0)
            {
                Label_Result.Text = "Out of door! You Lose!";
                Label_NowCoin.Text = (Convert.ToInt32(Label_NowCoin.Text) - Convert.ToInt32(TextBox_BetCoin.Text)).ToString();
                if (Convert.ToInt32(Label_NowCoin.Text) <= 0)
                {
                    porkInitial();
                    coinInitial();
                    Label_Count.Text = "輸到脫褲子! 重新開局!";
                    Label_Count.Font.Size = FontUnit.Larger;
                    Label_Count.Font.Bold = true;
                }
            }
            else
            {
                Label_Result.Text = "In door! You Win!";
                Label_NowCoin.Text = (Convert.ToInt32(Label_NowCoin.Text) + Convert.ToInt32(TextBox_BetCoin.Text)).ToString();
                if(Convert.ToInt32(Label_NowCoin.Text)> Convert.ToInt32(Label_MaxCoinRecord.Text))
                {
                    Label_MaxCoinRecord.Text = Label_NowCoin.Text;
                }
            }
        }

        protected void RandomPork(Image image,bool IsRemove = true)
        {
            ArrayList pork = (ArrayList)Session["pork"];
            
            int n= pork.Count;
            int m=random.Next(1, n+1) - 1;
            String picture;
            picture = pork[m].ToString();
            image.ImageUrl = picture;

            if(IsRemove)
            {
                pork.RemoveAt(m);
                Session["pork"] = pork;
            }
        }

        protected void TextBox_BetCoin_TextChanged(object sender, EventArgs e)
        {
            if(Convert.ToInt32(TextBox_BetCoin.Text)<1000)
            {
                TextBox_BetCoin.Text = "1000";
            }
            else if(Convert.ToInt32(TextBox_BetCoin.Text)> Convert.ToInt32(Label_NowCoin.Text))
            {
                TextBox_BetCoin.Text = Label_NowCoin.Text;
            }
        }

        protected void GridView1_RawDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false; //隱藏game欄位
            e.Row.Cells[3].Visible = false; //隱藏recordtype欄位          

        }
    }  

}