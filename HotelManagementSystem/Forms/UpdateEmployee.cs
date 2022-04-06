using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelManagementSystem.Forms
{
    public partial class UpdateEmployee : Form
    {
        User employee;

        String query;


        public UpdateEmployee(User _employee)
        {
            InitializeComponent();
            employee = _employee;
        }

        private void UpdateEmployee_Load(object sender, EventArgs e)
        {
            txtID.Text = employee._id;
            txtFirstName.Text = employee._firstName;
            txtLastName.Text = employee._lastName;
            txtEmail.Text = employee._email;
            txtPhoneNumber.Text = employee._phoneNumber;
            txtDateOfBirth.Text = employee._dateOfBirth;
            txtDepartment.Text = employee._department;
            txtPermissions.Text = employee._permissions;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtLastName.Text != "" && txtFirstName.Text != "" && txtEmail.Text != "" && txtPhoneNumber.Text != "" &&
                txtDepartment.Text != "" && txtDateOfBirth.Text != "" && txtPermissions.Text != "")
            {

                string dateOfBirth = "";
                if (txtDateOfBirth.Text.Length == 9)
                {
                    string date = "0" + txtDateOfBirth.Text;
                    dateOfBirth = date.Substring(6, 4) + "-" +
               date.Substring(0, 2) + "-" +
               date.Substring(3, 2);
                }
                else if (txtDateOfBirth.Text.Length == 10)
                {
                    dateOfBirth = txtDateOfBirth.Text.Substring(6, 4) + "-" +
                txtDateOfBirth.Text.Substring(0, 2) + "-" +
                txtDateOfBirth.Text.Substring(3, 2);
                }
                else if (txtDateOfBirth.Text.Length == 8)
                {
                    string date = "0" + txtDateOfBirth.Text.Substring(0, 2) + "0" +
                        txtDateOfBirth.Text.Substring(2, 6);
                    dateOfBirth = date.Substring(6, 4) + "-" +
                       date.Substring(0, 2) + "-" +
                      date.Substring(3, 2);
                }


                String fname = txtFirstName.Text;
                String lname = txtLastName.Text;
                String email = txtEmail.Text;
                Int64 phoneNumber = Int64.Parse(txtPhoneNumber.Text);
                String permissions = txtPermissions.Text;
                String department = txtDepartment.Text;

                query = "EXEC UpdateEmployee @employeeId ='" + employee._id +
                          "', @employeeFirstName = '" + fname +
                          "', @employeeLastName ='" + lname +
                          "', @dateOfBirth = '" + dateOfBirth +
                          "',@phoneNumber = '" + phoneNumber +
                          "', @emailAddress = '" + email +
                          "', @department = '" + department +
                          "', @permission = '" + permissions + "';";

                Function.SetData(query, "Employee data has been updated successfully.");

                    this.Close();
                
                

            }
            else
            {
                MessageBox.Show("All fields are required", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
