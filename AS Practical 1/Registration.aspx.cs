using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Security.Cryptography;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

using System.Text.RegularExpressions;
using System.Drawing;

namespace AS_Practical_1
{
    public partial class Registration : System.Web.UI.Page
    {
        string ASDBConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ASDBConnection"].ConnectionString;
        static string finalHash;
        static string salt;
        byte[] Key;
        byte[] IV;
        int lockout = 0;
        


        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //regex validation
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

        protected void Submit_Click(object sender, EventArgs e)
        {
            string email = TB_email.Text.ToString().Trim();
            if (checkemail(email) == null)
            {
                //password protection
                string pwd = TB_pwd.Text.ToString().Trim();
                RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                byte[] saltByte = new byte[8];
                rng.GetBytes(saltByte);
                salt = Convert.ToBase64String(saltByte);
                SHA512Managed hashing = new SHA512Managed();
                string pwdWithSalt = pwd + salt;
                byte[] plainHash = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwd));
                byte[] hashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwdWithSalt));

                finalHash = Convert.ToBase64String(hashWithSalt);

                RijndaelManaged cipher = new RijndaelManaged();
                cipher.GenerateKey();
                Key = cipher.Key;
                IV = cipher.IV;

                //xss = HttpUtility.HtmlEncode(TB_Fname.Text);
                //xss = HttpUtility.HtmlEncode(TB_Lname.Text);
                //xss = HttpUtility.HtmlEncode(TB_email.Text);
                //xss = HttpUtility.HtmlEncode(TB_pwd.Text);

                int scores = checkPassword(TB_pwd.Text);
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
                pwd_checker.Text = "Status : " + status;
                if (scores < 4)
                {
                    pwd_checker.ForeColor = Color.Red;
                    return;
                }
                else
                {
                    pwd_checker.ForeColor = Color.Green;

                    createAccount();
                    Response.Redirect("Login.aspx?Comment=" + HttpUtility.UrlEncode(TB_email.Text) + HttpUtility.UrlEncode(TB_pwd.Text) 
                        + HttpUtility.UrlEncode(TB_DoB.Text) + HttpUtility.UrlEncode(TB_Fname.Text) + HttpUtility.UrlEncode(TB_Lname.Text)
                        + HttpUtility.UrlEncode(TB_cardnum.Text), false);
                }
            }
            else
            {
                lbl_message.ForeColor = Color.Red;
                lbl_message.Text = "This Email already Exists!";
            }
        }

        protected void createAccount()
        {
                try
                {
                    using (SqlConnection myconn = new SqlConnection(ASDBConnectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("INSERT INTO Account VALUES(@Fname, @Lname, @Dob, @CCnum, @Email, @PasswordHash, @PasswordSalt, @IV, @Key, @Lockout, @Timer)"))
                        {
                            using (SqlDataAdapter sda = new SqlDataAdapter())
                            {
                                cmd.CommandType = CommandType.Text;
                                cmd.Parameters.AddWithValue("@Fname", TB_Fname.Text.Trim());
                                cmd.Parameters.AddWithValue("@Lname", TB_Lname.Text.Trim());
                                cmd.Parameters.AddWithValue("@Dob", TB_DoB.Text.Trim());
                                cmd.Parameters.AddWithValue("@CCnum", Convert.ToBase64String(encryptData(TB_cardnum.Text.Trim())));
                                cmd.Parameters.AddWithValue("@Email", TB_email.Text.Trim());
                                cmd.Parameters.AddWithValue("@PasswordHash", finalHash);
                                cmd.Parameters.AddWithValue("@PasswordSalt", salt);
                                cmd.Parameters.AddWithValue("@IV", Convert.ToBase64String(IV));
                                cmd.Parameters.AddWithValue("@Key", Convert.ToBase64String(Key));
                                cmd.Parameters.AddWithValue("@Lockout", lockout.ToString());
                                cmd.Parameters.AddWithValue("@Timer", DateTime.Now);
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

        protected byte[] encryptData(string data)
        {
            byte[] cipherText = null;
            try
            {
                RijndaelManaged cipher = new RijndaelManaged();
                cipher.IV = IV;
                cipher.Key = Key;
                ICryptoTransform encryptTransform = cipher.CreateEncryptor();
                //ICryptoTransform decryptTransform = cipher.CreateDecryptor();
                byte[] plainText = Encoding.UTF8.GetBytes(data);
                cipherText = encryptTransform.TransformFinalBlock(plainText, 0, plainText.Length);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally 
            { }
            return cipherText;
        }

        protected string checkemail (string email)
        {
            string s = null;
            SqlConnection connection = new SqlConnection(ASDBConnectionString);
            string sql = "Select Email FROM ACCOUNT WHERE Email=@EMAIL";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@EMAIL", email);
            try
            {
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["Email"] != null)
                        {
                            if (reader["Email"] != DBNull.Value)
                            {
                                s = reader["Email"].ToString();
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
    }
}
