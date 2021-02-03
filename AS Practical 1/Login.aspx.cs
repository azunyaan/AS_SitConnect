using System;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;

namespace AS_Practical_1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        string ASDBConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ASDBConnection"].ConnectionString;
        static int failedattempt = 0;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public class MyObject
        {
            public string success { get; set; }
            public List<string> ErrorMessage { get; set; }
        }

        public bool ValidateCaptcha()
        {
            bool result = true;
            string captchaRes = Request.Form["g-recaptcha-response"];
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create
                (" https://www.google.com/recaptcha/api/siteverify?secret=6LeKvzkaAAAAAFofuh-jtUaIlOTWJlvVeHoNV4ch &response=" + captchaRes);

            try
            {
                using (WebResponse wRes = req.GetResponse())
                {
                    using (StreamReader readStream = new StreamReader(wRes.GetResponseStream()))
                    {
                        string jsonRes = readStream.ReadToEnd();
                        //lbl_gscore.Text = jsonRes.ToString();
                        JavaScriptSerializer js = new JavaScriptSerializer();
                        MyObject jsonObj = js.Deserialize<MyObject>(jsonRes);
                        result = Convert.ToBoolean(jsonObj.success);
                    }
                }
                return result;
            }
            catch (WebException ex)
            {
                throw ex;
            }
        }

        protected void Logmein(object sender, EventArgs e)
        {
            string pwd = tb_pwd.Text.ToString().Trim();
            string userid = tb_user.Text.ToString().Trim();
            SHA512Managed hashing = new SHA512Managed();
            string dbHash = getDBHash(userid);
            string dbSalt = getDBSalt(userid);


            if (ValidateCaptcha())
            {
                if (failedattempt >= 3)
                {
                    var timer = (DateTime.Now - Convert.ToDateTime(GetTimecheck(userid))).TotalSeconds;
                    Console.WriteLine(timer);
                    if (timer < 1)
                    {
                        lblMessage.Text = "Your Account is still locked out, please wait for 1 minute before trying again";
                        lblMessage.ForeColor = Color.Red;
                        Timer.Text = timer.ToString();
                        tb_user.Enabled = false;
                        tb_pwd.Enabled = false;
                    }
                    else
                    {
                        UpdateLockout(userid, 0);
                    }
                }
                else
                {
                    try
                    {
                        if (dbSalt != null && dbSalt.Length > 0 && dbHash != null && dbHash.Length > 0)
                        {
                            string pwdwithSalt = pwd + dbSalt;
                            byte[] hashwithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwdwithSalt));
                            string userHash = Convert.ToBase64String(hashwithSalt);
                            if (userHash.Equals(dbHash))
                            {
                                Session["LoggedIn"] = tb_user.Text.Trim();
                                string guid = Guid.NewGuid().ToString();
                                Session["AuthToken"] = guid;

                                Response.Cookies.Add(new HttpCookie("AuthToken", guid));
                                Response.Redirect("Homepage.aspx?Comment=" + HttpUtility.UrlEncode(tb_pwd.Text) + HttpUtility.UrlEncode(tb_user.Text), false);
                            }
                            else
                            {
                                lblMessage.ForeColor = Color.Red;
                                lblMessage.Text = "Incorrect username or password";
                                failedattempt += 1;
                                lbl_gscore.Text = failedattempt.ToString();
                                if (failedattempt == 3)
                                {
                                    updateTimecheck(userid, DateTime.Now.ToString());
                                    lblMessage.ForeColor = Color.Red;
                                    lblMessage.Text = "Your account has been locked out";
                                  

                                }
                            }
                        }


                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.ToString());
                    }
                    finally { }
                }
            }
        }

        protected string getDBHash(string userid)
        {
            string h = null;
            SqlConnection connection = new SqlConnection(ASDBConnectionString);
            string sql = "select PasswordHash FROM Account WHERE Email = @USERID";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@USERID", userid);

            try
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["PasswordHash"] != null)
                        {
                            if (reader["PasswordHash"] != DBNull.Value)
                            {
                                h = reader["PasswordHash"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception exe)
            {
                throw new Exception(exe.ToString());
            }
            finally { connection.Close(); }
            return h;
        }

        protected string getDBSalt(string userid)
        {
            string s = null;
            SqlConnection connection = new SqlConnection(ASDBConnectionString);
            string sql = "Select PASSWORDSALT FROM ACCOUNT WHERE Email=@USERID";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@USERID", userid);

            try
            {
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["PASSWORDSALT"] != null)
                        {
                            if (reader["PASSWORDSALT"] != DBNull.Value)
                            {
                                s = reader["PASSWORDSALT"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception exe)
            {
                throw new Exception(exe.ToString());
            }
            finally { connection.Close(); }
            return s;
        }

        protected string GetTimecheck(string userid)
        {
            string t = null;
            SqlConnection connection = new SqlConnection(ASDBConnectionString);
            string sql = "Select Timecheck FROM ACCOUNT WHERE Email=@EMAIL";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@EMAIL", userid);
            try
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if(reader["Timecheck"] != null)
                        {
                            if(reader["Timecheck"] != DBNull.Value)
                            {
                                t = reader["Timecheck"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { connection.Close(); }
            return t;
        }

        protected void updateTimecheck(string email, string timecheck)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ASDBConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("UPDATE Account SET Timecheck = @Timecheck where Email = @Email"))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@Email", email);
                            cmd.Parameters.AddWithValue("@Timecheck", timecheck);
                            cmd.Connection = connection;
                            connection.Open();
                            cmd.ExecuteNonQuery();
                            connection.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        protected void UpdateLockout(string email, int lockout)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ASDBConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("UPDATE Account SET Lockout= @lockout where Email = @Email"))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@Email", email);
                            cmd.Parameters.AddWithValue("@lockout", lockout);
                            cmd.Connection = connection;
                            connection.Open();
                            cmd.ExecuteNonQuery();
                            connection.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        protected void Register(object sender, EventArgs e)
        {
            Response.Redirect("Registration.aspx", false);
        }
    }
}
