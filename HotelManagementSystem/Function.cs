using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace HotelManagementSystem
{
    static class Function
    {
        private static SqlConnection GetConnection()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "data source = MOHAZZA\\MOHAZZA;" +
                "database = HotelManagementSystem;" +
                "integrated security = True";
            return con;
        }

        public static DataSet GetData(String query)   // Get data from database
        {
            SqlConnection con = GetConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = query;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        public static void SetData(String query, String message)   // insert delete update
        {
            SqlConnection con = GetConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();
            cmd.CommandText = query;

            try
            {
                cmd.ExecuteNonQuery();

                if (message != "")
                {
                    MessageBox.Show("  " + message + "   ", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("  " + ex.Message + "   ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            con.Close();
        }

        public static SqlDataReader GetDataForComboBox(String query)
        {
            SqlConnection con = GetConnection();
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Connection = con;
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            return sdr;
        }

        public static String EncryptPassword(String value)
        {
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                UTF32Encoding utf8 = new UTF32Encoding();
                byte[] data = md5.ComputeHash(utf8.GetBytes(value));
                return Convert.ToBase64String(data);
            }
        }
    }
}
