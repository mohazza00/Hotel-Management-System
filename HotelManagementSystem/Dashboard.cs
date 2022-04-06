using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelManagementSystem
{
    public partial class Dashboard : Form
    {
        public static User user = new User();
        public Dashboard(User _user)
        {
            InitializeComponent();
            user = _user;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnReservations_Click(object sender, EventArgs e)
        {
            uC_Reservations1.Visible = true;
            uC_Reservations1.BringToFront();
            uC_Reservations1.ReloadData();
        }

        private void btnCheckIn_Click(object sender, EventArgs e)
        {
            uC_CheckIn1.Visible = true;
            uC_CheckIn1.BringToFront();
        }

        private void btnGuests_Click(object sender, EventArgs e)
        {
            uC_CustomersDetails1.Visible = true;
            uC_CustomersDetails1.BringToFront();
            uC_CustomersDetails1.ReloadData();

        }

        private void btnRooms_Click(object sender, EventArgs e)
        {
            uC_Rooms1.Visible = true;
            uC_Rooms1.BringToFront();
            uC_Rooms1.ReloadData();
        }

        private void btnEmployees_Click(object sender, EventArgs e)
        {
            uC_Staff1.Visible = true;
            uC_Staff1.BringToFront();
            uC_Staff1.ReloadData();

        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            uC_Rooms1.Visible = false;
            btnRooms.PerformClick();
            date.Text = DateTime.Now.ToLongDateString();
            time.Text = DateTime.Now.ToLongTimeString();
            txtUsername.Text = user._username;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            time.Text = DateTime.Now.ToLongTimeString();
        }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            uC_CustomerCheckOut1.Visible = true;
            uC_CustomerCheckOut1.BringToFront();
            uC_CustomerCheckOut1.ReloadData();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            uC_Reports1.Visible = true;
            uC_Reports1.BringToFront();
            //uC_Reports1.ReloadData();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to Log out", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                user = null;
                LoginForm loginForm = new LoginForm();
                this.Close();
                loginForm.Show();
            }
        }

        private void Dashboard_Activated(object sender, EventArgs e)
        {
            uC_Reservations1.ReloadData();
            uC_Staff1.ReloadData();
        }
    }
}
