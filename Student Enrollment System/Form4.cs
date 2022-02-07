using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Student_Enrollment_System
{
    public partial class Form4 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Documents\studentinfo.mdf;Integrated Security=True;Connect Timeout=30");
        public Form4()
        {
        
            InitializeComponent();
            cmbCourseFees.Items.Add("HND - COM");
            cmbCourseFees.Items.Add("HND - BM");
            cmbCourseFees.Items.Add("Dip - IT");
            cmbCourseFees.Items.Add("Dip - English");
            cmbCourseFees.Items.Add("Dip - BM");
        }

        private void txtStudentID_Leave(object sender, EventArgs e)
        {
           con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select * from  Table1 where RegistrationNumber='" + txtStudentID.Text.ToString() + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            if (dt.Rows.Count >= 1)
            {
            }
            else
            {
                if (MessageBox.Show("Continue or Re-Enter Number ", "Not Registered Student", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                }
                else 
                {
                    btnClear_Click(sender, e);
                }
            }
            con.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
                this.txtStudentID.Text = "";
                this.txtDiscount.Text = "";
                cmbCourseFees.SelectedIndex = -1;
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                int discount = Convert.ToInt32(txtDiscount.Text);
                int discountfinal;
               
                if (cmbCourseFees.Text == "HND - COM")
                {
                    int fees = 25000;
                    discountfinal = (fees * discount) / 100;
                    Double Total = fees - discount;
                    dataGridView1.Rows.Add(txtStudentID.Text, cmbCourseFees.Text, fees, discountfinal.ToString(), Total);
                }
                else if (cmbCourseFees.Text == "HND - BM")
                {
                    int fees = 30000;
                    discountfinal = (fees * discount) / 100;
                    Double Total = fees - discount;
                    dataGridView1.Rows.Add(txtStudentID.Text, cmbCourseFees.Text, fees, discountfinal.ToString(), Total);
                }
                else if (cmbCourseFees.Text == "Dip - IT")
                {
                    int fees = 35000;
                    discountfinal = (fees * discount) / 100;
                    Double Total = fees - discount;
                    dataGridView1.Rows.Add(txtStudentID.Text, cmbCourseFees.Text, fees, discountfinal.ToString(), Total);
                }
                else if (cmbCourseFees.Text == "Dip - English")
                {
                    int fees = 40000;
                    discountfinal = (fees * discount) / 100;
                    Double Total = fees - discount;
                    dataGridView1.Rows.Add(txtStudentID.Text, cmbCourseFees.Text, fees, discountfinal.ToString(), Total);
                }
                else if (cmbCourseFees.Text == "Dip - BM")
                {
                    int fees = 45000;
                    discountfinal = (fees * discount) / 100;
                    Double Total = fees - discount;
                    dataGridView1.Rows.Add(txtStudentID.Text, cmbCourseFees.Text, fees ,  discountfinal.ToString(), Total);
                }

                btnClear_Click(sender, e);
            }
          catch (System.Exception)
            {
                MessageBox.Show("Enter Correct Data");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 from = new Form2();
            from.Show();
            this.Hide();
        }
    }
   
}
