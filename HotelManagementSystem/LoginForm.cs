using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;


namespace HotelManagementSystem
{
    public partial class LoginForm : Form
    {

        public User user = new User();
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            String password = Function.EncryptPassword(txtPassword.Text);

            String query = "EXEC LoginToSystem @username = '" + txtUsername.Text + "', @passwd = '"+ password + "'";

            DataSet ds = Function.GetData(query);

            if (ds.Tables[0].Rows.Count != 0)
            {
                labelError.Visible = false;

                SetUserInfo(ds);

                Dashboard db = new Dashboard(user);
                this.Hide();
                db.Show();
            }
            else
            {
                labelError.Visible = true;
                txtUsername.Clear();
                txtPassword.Clear();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void SetUserInfo(DataSet ds)
        {
            user._id = ds.Tables[0].Rows[0][0].ToString();
            user._firstName = ds.Tables[0].Rows[0][1].ToString();
            user._lastName = ds.Tables[0].Rows[0][2].ToString();
            user._dateOfBirth = ds.Tables[0].Rows[0][3].ToString();
            user._phoneNumber = ds.Tables[0].Rows[0][4].ToString();
            user._email = ds.Tables[0].Rows[0][5].ToString();
            user._permissions = ds.Tables[0].Rows[0][6].ToString();
            user._department = ds.Tables[0].Rows[0][7].ToString();
            user._username = ds.Tables[0].Rows[0][8].ToString();
            user._password = ds.Tables[0].Rows[0][9].ToString();
        }

        
    }

    public class User
    {
        public String _id;
        public String _lastName;
        public String _firstName;
        public String _email;
        public String _phoneNumber;
        public String _department;
        public String _permissions;
        public String _dateOfBirth;
        public String _username;
        public String _password;
    }
}
