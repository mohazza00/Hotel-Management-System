using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HotelManagementSystem.All_User_Control;
using System.Data.SqlClient;


namespace HotelManagementSystem.Forms
{
    public partial class UpdateReservation : Form
    {
        String query;

        Reservation reservation;
        int reservationId;
        double totalPrice;
        int roomId;
        int guestId;

        String pricePerNight;
        
        bool Initialize = true;

        public UpdateReservation(Reservation _reservation)
        {
            InitializeComponent();
            reservation = _reservation;
        }

        private void UpdateReservation_Load(object sender, EventArgs e)
        {
            query = "select guestLastName from guests where guestLastName like '" + txtLastName.Text + "%'";
            setComboBox(query, txtLastName);

            query = "select roomNumber from rooms where roomType = '" + txtRoomType.Text + "' and roomStatus = 'Not Reserved' or roomNumber = '" + reservation.RoomNumber + "'";
            setComboBox(query, txtRoomNumber);

            reservationId = int.Parse(reservation.Id);
            txtFirstName.Text = reservation.FirstName;
            txtLastName.Text = reservation.LastName;
            txtRoomNumber.Text = reservation.RoomNumber;
            txtAdults.Text = reservation.NoOfAdults;
            txtChildren.Text = reservation.NoOfChildren;
            txtCheckIn.Text = reservation.CheckInDate;
            txtCheckOut.Text = reservation.CheckOutDate;
            txtReservationForm.Text = reservation.ReservationForm;
            txtPaymentOption.Text = reservation.PaymentOption;
            txtAdvancePayment.Text = reservation.AdvacnePayment;
            txtPrice.Text = reservation.TotalPrice + "$";
            txtStatus.Text = reservation.Status;
            totalPrice = int.Parse(reservation.TotalPrice);
            txtPendingBalance.Text = (totalPrice - int.Parse(txtAdvancePayment.Text)).ToString() + "$";

            DateTime d1 = txtCheckIn.Value;
            DateTime d2 = txtCheckOut.Value;
            String datesDifference = Math.Ceiling((d2 - d1).TotalDays).ToString();

            txtNights.Text = datesDifference;

            query = "select price, roomId, occupancy, roomType from rooms where roomNumber = '" + reservation.RoomNumber + "'";
            DataSet ds = Function.GetData(query);
            txtRoomPrice.Text = ds.Tables[0].Rows[0][0].ToString();
            roomId = int.Parse(ds.Tables[0].Rows[0][1].ToString());
            txtRoomOccupancy.Text = ds.Tables[0].Rows[0][2].ToString();
            txtRoomType.Text = ds.Tables[0].Rows[0][3].ToString();


            Initialize = false;

        }

        private void txtRoomType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!Initialize)
            {
                txtRoomNumber.Items.Clear();
                txtRoomPrice.Clear();
                txtRoomOccupancy.Clear();
                txtPrice.Clear();

                query = "select roomNumber from rooms where roomType = '" + txtRoomType.Text + "' and roomStatus = 'Not Reserved' or roomNumber = '" + reservation.RoomNumber + "'";
                setComboBox(query, txtRoomNumber);
                txtPrice.Text = "0$";
            }

        }

        private void txtRoomNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            query = "select price, roomId, occupancy from rooms where roomNumber = '" + txtRoomNumber.Text + "'";
            DataSet ds = Function.GetData(query);
            pricePerNight = ds.Tables[0].Rows[0][0].ToString();
            txtRoomPrice.Text = pricePerNight;

            DateTime d1 = txtCheckIn.Value;
            DateTime d2 = txtCheckOut.Value;
            String datesDifference = (d2 - d1).TotalDays.ToString();
            double nights = Math.Ceiling(Convert.ToDouble(datesDifference));

            totalPrice = nights * Int32.Parse(pricePerNight);
            txtPrice.Text = totalPrice.ToString() + "$";
            txtPendingBalance.Text = totalPrice.ToString() + "$";

            roomId = int.Parse(ds.Tables[0].Rows[0][1].ToString());

            txtRoomOccupancy.Text = ds.Tables[0].Rows[0][2].ToString();


        }

        private void txtCheckOut_ValueChanged(object sender, EventArgs e)
        {
            DateTime d1 = txtCheckIn.Value;
            DateTime d2 = txtCheckOut.Value;
            String datesDifference = Math.Ceiling((d2 - d1).TotalDays).ToString();
            
            txtNights.Text = datesDifference;

            if (pricePerNight != null)
            {
                double nights = Math.Ceiling(Convert.ToDouble(datesDifference));
                double price = nights * Int32.Parse(pricePerNight);
                txtPrice.Text = price.ToString();
                txtPendingBalance.Text = price.ToString();
                totalPrice = price;

            }

        }

        private void txtCheckIn_ValueChanged(object sender, EventArgs e)
        {
            DateTime d1 = txtCheckIn.Value;
            DateTime d2 = txtCheckOut.Value;
            String datesDifference = Math.Ceiling((d2 - d1).TotalDays).ToString();

            txtNights.Text = datesDifference;

            if (pricePerNight != null)
            {
                double nights = Math.Ceiling(Convert.ToDouble(datesDifference));

                double price = nights * Int32.Parse(pricePerNight);
                txtPrice.Text = price.ToString();
                txtPendingBalance.Text = price.ToString();
                totalPrice = price;
            }

        }

        private void txtLastName_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFirstName.Clear();

            query = "select guestId, guestFirstName from guests where guestLastName like '" + txtLastName.Text + "'";
            DataSet ds = Function.GetData(query);

            if (txtLastName.SelectedIndex != -1)
            {
                guestId = int.Parse(ds.Tables[0].Rows[0][0].ToString());
                txtFirstName.Text = ds.Tables[0].Rows[0][1].ToString();
            }

        }

        private void txtAdvancePayment_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(txtAdvancePayment.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                txtAdvancePayment.Text = txtAdvancePayment.Text.Remove(txtAdvancePayment.Text.Length - 1);
            }
            else
            {
                if (txtAdvancePayment.Text != "")
                {
                    txtPendingBalance.Text = (totalPrice - int.Parse(txtAdvancePayment.Text)).ToString() + "$";
                }
                else
                {
                    txtPendingBalance.Text = totalPrice + "$";
                }
            }


        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtLastName.Text != "" && txtFirstName.Text != "" && txtCheckIn.Text != "" && txtCheckOut.Text != "" &&
                txtReservationForm.Text != "" && txtRoomType.Text != "" && txtRoomNumber.Text != "" &&
                txtAdults.Text != "" && txtPaymentOption.Text != "" && txtChildren.Text != "" && txtAdvancePayment.Text != ""
                && txtPrice.Text != "" && txtStatus.Text != "")
            {
                if (int.Parse(txtAdults.Text) + int.Parse(txtChildren.Text) <= int.Parse(txtRoomOccupancy.Text))
                {
                    string checkInDate = "";
                    if (txtCheckIn.Text.Length == 9)
                    {
                        string date = "0" + txtCheckIn.Text;
                        checkInDate = date.Substring(6, 4) + "-" +
                   date.Substring(0, 2) + "-" +
                   date.Substring(3, 2);
                    }
                    else if (txtCheckIn.Text.Length == 10)
                    {
                        checkInDate = txtCheckIn.Text.Substring(6, 4) + "-" +
                    txtCheckIn.Text.Substring(0, 2) + "-" +
                    txtCheckIn.Text.Substring(3, 2);
                    }

                    else if (txtCheckIn.Text.Length == 8)
                    {
                        string date = "0" + txtCheckIn.Text.Substring(0, 2) + "0" +
                            txtCheckIn.Text.Substring(2, 6);
                        checkInDate = date.Substring(6, 4) + "-" +
                           date.Substring(0, 2) + "-" +
                          date.Substring(3, 2);
                    }

                    string checkOutDate = "";
                    if (txtCheckOut.Text.Length == 9)
                    {
                        string date = "0" + txtCheckOut.Text;
                        checkOutDate = date.Substring(6, 4) + "-" +
                   date.Substring(0, 2) + "-" +
                   date.Substring(3, 2);
                    }
                    else if (txtCheckOut.Text.Length == 10)
                    {
                        checkOutDate = txtCheckOut.Text.Substring(6, 4) + "-" +
                    txtCheckOut.Text.Substring(0, 2) + "-" +
                    txtCheckOut.Text.Substring(3, 2);
                    }
                    else if (txtCheckOut.Text.Length == 8)
                    {
                        string date = "0" + txtCheckOut.Text.Substring(0, 2) + "0" +
                            txtCheckOut.Text.Substring(2, 6);
                        checkOutDate = date.Substring(6, 4) + "-" +
                           date.Substring(0, 2) + "-" +
                          date.Substring(3, 2);
                    }






                    var checkIn = checkInDate;
                    var checkOut = checkOutDate;
                    String reservationForm = txtReservationForm.Text;
                    //String roomType = txtRoomType.Text;
                    String roomNumber = txtRoomNumber.Text;
                    int noOfAdults = int.Parse(txtAdults.Text);
                    int noOfChildren = int.Parse(txtChildren.Text);
                    String paymentOption = txtPaymentOption.Text;
                    String advancePayment = txtAdvancePayment.Text;
                    String reservationStatus = txtStatus.Text;

                    query = "EXEC UpdateReservation @reservationId = '" + reservationId +
                       "', @checkInDate ='" + checkInDate +
                       "', @checkOutDate ='" + checkOutDate +
                       "', @noOfAdults = '" + noOfAdults +
                       "', @noOfChildren = '" + noOfChildren +
                       "', @paymentOption = '" + paymentOption +
                       "', @advancePayment = '" + advancePayment +
                       "', @reservationForm = '" + reservationForm +
                       "', @totalPrice = '" + totalPrice +
                       "', @roomId = '" + roomId + "', @guestId = '" + guestId +
                       "', @reservationStatus = '" + reservationStatus + "';";

                    Function.SetData(query, "Reservation data has been updated successfully.");


                    this.Close();
                }
                else
                {
                    MessageBox.Show("The number of occupants is more than the available occupancy", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("All fields are required", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        public void setComboBox(String query, ComboBox combo)
        {
            SqlDataReader sdr = Function.GetDataForComboBox(query);
            while (sdr.Read())
            {
                for (int i = 0; i < sdr.FieldCount; i++)
                {
                    combo.Items.Add(sdr.GetString(i));
                }
            }
            sdr.Close();
        }

       
    }
}
