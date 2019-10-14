using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using MySql.Data.MySqlClient;

namespace Student_Login.Models
{
    public class Stud_log
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }




        public string encryptPass(string word)
        {
            string result = null;

            using (MD5CryptoServiceProvider sha = new MD5CryptoServiceProvider())
            {
                byte[] bs = sha.ComputeHash(Encoding.ASCII.GetBytes(word));
                using (TripleDESCryptoServiceProvider triDes = new TripleDESCryptoServiceProvider() { Key = bs, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = triDes.CreateEncryptor();
                    byte[] results = transform.TransformFinalBlock(bs, 0, bs.Length);
                    result = Convert.ToBase64String(results, 0, results.Length);
                }
            }




            return result;
        }
        public void saveToDB(string FirstName, string LastName, string Email, string UserName, string Password)
        {

            string cryp_pass = null;

            cryp_pass = encryptPass(Password);

            string s = null;

            string query = @"INSERT INTO student_reg_details
           (Student_FirstName,Student_LastName,Student_Email,Student_UserName,Student_Password)     VALUES
           (@Student_FirstName,@Student_LastName,@Student_Email,@Student_UserName,@Student_Password)";

            string connection = "server=localhost;user id=root;database=student;password=myPHP@dmin2019!";

            using (MySql.Data.MySqlClient.MySqlConnection sql = new MySql.Data.MySqlClient.MySqlConnection(connection))
            using (MySql.Data.MySqlClient.MySqlCommand com = new MySql.Data.MySqlClient.MySqlCommand(query, sql))
            {
                com.Parameters.AddWithValue("@Student_FirstName", FirstName);
                com.Parameters.AddWithValue("@Student_LastName", LastName);
                com.Parameters.AddWithValue("@Student_Email", Email);
                com.Parameters.AddWithValue("@Student_UserName", UserName);
                com.Parameters.AddWithValue("@Student_Password", cryp_pass);

                foreach (MySql.Data.MySqlClient.MySqlParameter parameter in com.Parameters)
                {
                    if (parameter.Value == null)
                    {
                        parameter.Value = DBNull.Value;
                    }
                }

                sql.Open();
                com.ExecuteNonQuery();
                sql.Close();
            }

        }





    }
}