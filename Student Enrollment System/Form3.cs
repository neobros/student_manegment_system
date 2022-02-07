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
    public partial class Form3 : Form
    {
     
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Documents\studentinfo.mdf;Integrated Security=True;Connect Timeout=30");
        int years;
      
        public Form3()
        {
            InitializeComponent();
            cmbCourse.Items.Add("HND - COM");
            cmbCourse.Items.Add("HND - BM");
            cmbCourse.Items.Add("Dip - IT");
            cmbCourse.Items.Add("Dip - English");
            cmbCourse.Items.Add("Dip - BM");
            btnDelete.Visible = false;
        }

        private void dateTimePicker1_CloseUp(object sender, EventArgs e)
        {
           years = DateTime.Now.Year - dateTimePicker1.Value.Year;

            if (dateTimePicker1.Value.AddYears(years) > DateTime.Now) years--;

            if (years > 0 && years < 18)
            {
                MessageBox.Show("Cannot Enroll - Below 18 years");
                label1.Text = "Re-enter";
                //txtAge.Text = years.ToString();
            }
           
               txtAge.Text = years.ToString();

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.txtRegistrationNumber.Text = "";
            this.txtStudentName.Text = "";
            txtContractNumber.Text = "";
            txtAge.Text = "";
            cmbCourse.SelectedIndex = -1;
            radioMale.Checked = false;
            radioFemale.Checked = false;
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
           

            if (!(!(cmbCourse.SelectedItem != null) || (txtStudentName.Text == "" || txtRegistrationNumber.Text == "" || txtContractNumber.Text == "")
                  || (!radioMale.Checked == true && !radioFemale.Checked == true)))
            {
              try
              {
                string name = txtStudentName.Text;
                string studentNumber = txtRegistrationNumber.Text;
                DateTime date = dateTimePicker1.Value;
                string age1 = txtAge.Text;
                int age = Convert.ToInt32(age1);
                string male = "";
                string female = "";
                string ContractNumber = txtContractNumber.Text;
                string Course = cmbCourse.SelectedItem.ToString();

                if (radioMale.Checked == true)
                {
                    male = " Male";
                }
                else if (radioFemale.Checked == true)
                {
                    female = " Female";
                }
             
                con.Open();

                    string qry = " insert into Table1 values('" + studentNumber + "','" + name + "','" + date + "' ,'" + age + "','" + male+female + "'" +
                        " ,'" + ContractNumber + "' ,'" + Course + "')";

                           SqlCommand sc = new SqlCommand(qry, con);

               
                int i = sc.ExecuteNonQuery();

                if (i >= 1)
                    MessageBox.Show(i +""+ "Student Insert Successfully");
                else
                    MessageBox.Show("Registered Failed Try again");

                    btnClear_Click(sender, e);
                    show();

              }
                catch (System.Exception ) 
              {
                    MessageBox.Show(" registed allredy");
              }
                con.Close();
            }

            else
            { 
                MessageBox.Show("Complete the Missing Data");
            }
        }

       

        void show()
        {
            SqlDataAdapter sda = new SqlDataAdapter("select * from Table1", con);

            DataTable dt = new DataTable();
            sda.Fill(dt);

            dataGridView2.Rows.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                int n = dataGridView2.Rows.Add();
                dataGridView2.Rows[n].Cells[0].Value = dr[0].ToString();
                dataGridView2.Rows[n].Cells[1].Value = dr[1].ToString();
                dataGridView2.Rows[n].Cells[2].Value = dr[2].ToString();
                dataGridView2.Rows[n].Cells[3].Value = dr[3].ToString();
                dataGridView2.Rows[n].Cells[4].Value = dr[4].ToString();
                dataGridView2.Rows[n].Cells[5].Value = dr[5].ToString();
                dataGridView2.Rows[n].Cells[6].Value = dr[6].ToString();
            }
        }

        private void dataGridView2_MouseClick(object sender, MouseEventArgs e)
        { 
            try
            {
                btnDelete.Visible = true;

                txtRegistrationNumber.Text = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
                txtStudentName.Text = dataGridView2.SelectedRows[0].Cells[1].Value.ToString();
                dateTimePicker1.Text = dataGridView2.SelectedRows[0].Cells[2].Value.ToString();
                txtAge.Text = dataGridView2.SelectedRows[0].Cells[3].Value.ToString();

                txtContractNumber.Text = dataGridView2.SelectedRows[0].Cells[5].Value.ToString();
                cmbCourse.Text = dataGridView2.SelectedRows[0].Cells[6].Value.ToString();
            }
            catch (System.Exception)
            {       
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Confirm Delete ", "Warning !!", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
               
                try
                {

                    con.Open();
                    string studentNumber = txtRegistrationNumber.Text;
                    string qry = "delete from Table1 where RegistrationNumber ='" + studentNumber + "'";

                    SqlCommand sc = new SqlCommand(qry, con);
                    int i = sc.ExecuteNonQuery();

                    if (i >= 1)
                        MessageBox.Show( "Complete ");
                  

                    btnClear_Click(sender, e);
                    show();
                    con.Close();
                }

                catch (System.Exception exp)
                {
                    MessageBox.Show("error is " + exp.ToString());
                }
                con.Close();
            }
        }

       
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            btnDelete.Visible = true;
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select * from Table1 where RegistrationNumber like '%" + txtSearch.Text.ToString()+"%'",  con);

            DataTable dt = new DataTable();
            sda.Fill(dt);

            dataGridView2.Rows.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                int n = dataGridView2.Rows.Add();
                dataGridView2.Rows[n].Cells[0].Value = dr[0].ToString();
                dataGridView2.Rows[n].Cells[1].Value = dr[1].ToString();
                dataGridView2.Rows[n].Cells[2].Value = dr[2].ToString();
                dataGridView2.Rows[n].Cells[3].Value = dr[3].ToString();
                dataGridView2.Rows[n].Cells[4].Value = dr[4].ToString();
                dataGridView2.Rows[n].Cells[5].Value = dr[5].ToString();
                dataGridView2.Rows[n].Cells[6].Value = dr[6].ToString();
            }
            con.Close();
        }

        private void txtRegistrationNumber_Leave(object sender, EventArgs e)
        {
            con.Open();
            SqlDataAdapter sda= new SqlDataAdapter("select * from  Table1 where RegistrationNumber='" +txtRegistrationNumber.Text.ToString()+ "'" ,con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
        
           if (dt.Rows.Count>=1)
           {
                btnInsert.Visible = false;
                btnDelete.Visible = false;
                btnClear_Click(sender, e);
                MessageBox.Show("Already registered. Enter a New Number");
                show();
           }
           else
           {
                btnInsert.Visible = true;
                btnDelete.Visible = true;
            }
            con.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
          //show(); 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 from = new Form2();
            from.Show();
            this.Hide();
        }
    }
    
    
}
