using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PunchCard
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            lblCompany.Text = ConfigurationManager.AppSettings["CompanyName"];
        }

        private void btnON_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                try
                {
                    WebCommModel model = new WebCommModel();
                    model.WBC_SERNO = My.GenSerNo();
                    model.WBC_DATE = DateTime.Now.AddMinutes(-1).ToString("yyyy-MM-dd"); ;
                    model.WBC_CLOCKIN = DateTime.Now.ToString("HH:mm");

                    if(WebCommDAL.GetSernoByDate(connection , DateTime.Now.ToString("yyyy-MM-dd")) != string.Empty)
                    {
                        MessageBox.Show("今日已打卡過!");
                        return;
                    }


                    if( WebCommDAL.Add(connection, model, PunchCard.ON)>0)
                    {
                        MessageBox.Show(string.Format("打卡上班 時間:{0} {1}", model.WBC_DATE, model.WBC_CLOCKIN));
                    }
                    else
                    {
                        MessageBox.Show("打卡發生異常!");
                    }
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnOFF_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                try
                {
                    string date = DateTime.Now.ToString("yyyy-MM-dd");
                    string time = DateTime.Now.ToString("HH:mm");
                    string serno = WebCommDAL.GetSernoByDate(connection, date);

                    if (string.IsNullOrEmpty(serno))
                    {
                        MessageBox.Show("今日上班尚未打卡!");
                        return;
                    }
                    else
                    {
                        string strSql = @" UPDATE [dbo].[WEBCOMM]
                                        SET [WBC_CLOCKOUT] = @WBC_CLOCKOUT
                                        WHERE WBC_SERNO = @WBC_SERNO    ";

                        SqlCommand command = new SqlCommand(strSql, connection);
                        if (connection.State != System.Data.ConnectionState.Open)
                            connection.Open();

                        command.Parameters.AddWithValue("@WBC_CLOCKOUT", time);
                        command.Parameters.AddWithValue("@WBC_SERNO", serno);
                        if(command.ExecuteNonQuery()>0)
                        {
                            MessageBox.Show(string.Format("打卡下班 時間:{0} {1}", date, time));
                        }
                        else
                        {
                            MessageBox.Show("打卡發生異常!");
                        }

                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
