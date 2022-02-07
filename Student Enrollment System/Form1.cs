using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Student_Enrollment_System
{
    public partial class Form1 : Form
    {
        private int attemptsLeft = 5;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnloging_Click(object sender, EventArgs e)
        {
            string username, password;
            username = txtuserName.Text;
            password = txtpassword.Text;

            if ((username == "admin") && (password == "admin"))
            {

                Form2 from = new Form2();
                from.Show();
                this.Hide();

            }
            else
            {
                attemptsLeft--;
                MessageBox.Show("You have  " + attemptsLeft + "  attempts left !");
                lblerrors.Text = ("User name or Password is wrong");
                lblattempts.Text = (+attemptsLeft + " attempts left !");
                txtpassword.Clear();
                txtuserName.Clear();

            }
            if (attemptsLeft == 0)
            {
                MessageBox.Show("You are not granted with access");
                Application.Exit();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
