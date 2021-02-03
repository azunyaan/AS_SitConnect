using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Text;
using System.Drawing;
using System.Data;

namespace AS_Practical_1
{
    public partial class Changepwd : System.Web.UI.Page
    {
        string ASDBConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ASDBConnection"].ConnectionString;
        static string finalHash;
        static string salt;
        

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_newpwd_Click(object sender, EventArgs e)
        {
            string oldpwd = tb_oldpwd.Text.ToString().Trim();
            string newpwd = tb_newpassword.Text.ToString().Trim();
            string retype = tb_newpwdretype.Text.ToString().Trim();
            Session["email"] = tb_user.Text.ToString().Trim();
            

            if (newpwd == oldpwd)
            {
                lbl_error.Text = "Your new password cannot be the same as your old password";
            }
            else
            {
                if (newpwd == retype)
                {
                    RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                    byte[] saltByte = new byte[8];
                    rng.GetBytes(saltByte);
                    salt = Convert.ToBase64String(saltByte);
                    SHA512Managed hashing = new SHA512Managed();
                    string pwdWithSalt = newpwd + salt;
                    byte[] plainHash = hashing.ComputeHash(Encoding.UTF8.GetBytes(newpwd));
                    byte[] hashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwdWithSalt));
                    finalHash = Convert.ToBase64String(hashWithSalt);

                    int scores = checkPassword(tb_newpassword.Text);
                    string status = "";
                    switch (scores)
                    {
                        case 1:
                            status = "Very Weak";
                            break;
                        case 2:
                            status = "Weak";
                            break;
                        case 3:
                            status = "Medium";
                            break;
                        case 4:
                            status = "Strong";
                            break;
                        case 5:
                            status = "Excellent";
                            break;
                        default:
                            break;
                    }
                    lbl_error.Text = "Status : " + status;
                    if (scores < 4)
                    {
                        lbl_error.ForeColor = Color.Red;
                        return;
                    }
                    else
                    {
                        lbl_error.ForeColor = Color.Green;
                        updatepwd(Session["email"].ToString(), finalHash, salt);
                        Response.Redirect("Homepage.aspx", false);
                    }
                }
                else
                {
                    lbl_error.Text = "New passwords don't match!";
                }
            }
        }

        private int checkPassword(string password)
        {
            int score = 0;
            if (password.Length < 8)
            {
                return 1;
            }
            else
            {
                score = 1;
            }
            if (Regex.IsMatch(password, "[a-z]"))
            {
                score++;
            }
            if (Regex.IsMatch(password, "[A-Z]"))
            {
                score++;
            }
            if (Regex.IsMatch(password, "[0-9]"))
            {
                score++;
            }
            if (Regex.IsMatch(password, "[^A-Za-z0-9]"))
            {
                score++;
            }
            return score;
        }

        protected void updatepwd(string email, string pwd, string salt)
        {
            try
            {
                using (SqlConnection myconn = new SqlConnection(ASDBConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("UPDATE Account SET PasswordHash=@PasswordHash, PasswordSalt=@PasswordSalt  WHERE Email = @EMAIL"))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@EMAIL", Session["email"].ToString());
                            cmd.Parameters.AddWithValue("@PasswordHash", finalHash);
                            cmd.Parameters.AddWithValue("@PasswordSalt", salt);
                            cmd.Connection = myconn;
                            myconn.Open();
                            cmd.ExecuteNonQuery();
                            myconn.Close();


                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}