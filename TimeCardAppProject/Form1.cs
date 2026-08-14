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



namespace TimeCardAppProject
    
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string ConnectionString = "Data Source=DESKTOP-L9G3OCN\\SQLEXPRESS;Initial Catalog=TimeCardDB;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;";

            SqlConnection con = new SqlConnection(ConnectionString);

            con.Open();

            string EmployeeFirstName = this.textBox1.Text;
            string EmployeeLastName = this.textBox2.Text;
            string ClockOut = this.Clock.Text;
            string Query = "UPDATE EmployeeTimeSlots SET ClockOut = '" + ClockOut + "' WHERE EmployeeFirstName ='" + EmployeeFirstName + "' AND EmployeeSecondName='" + EmployeeLastName + "';";

            SqlCommand cmd = new SqlCommand(Query, con);
            cmd.ExecuteNonQuery();

            con.Close();

            MessageBox.Show("Employee Clock Out Time Has Been Recorded!");

        }


        private void textBox3_TextChanged(object sender, EventArgs e)
        {


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Clock.Text = DateTime.Now.ToString("hh:mm:ss");
            this.Date.Text = DateTime.Now.ToString("M, d, yyyy");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Employee Time Card Page";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ConnectionString = "Data Source=DESKTOP-L9G3OCN\\SQLEXPRESS;Initial Catalog=TimeCardDB;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;";

            SqlConnection con = new SqlConnection(ConnectionString);

            con.Open();

            string EmployeeFirstName = this.textBox1.Text;
            string EmployeeLastName = this.textBox2.Text;
            string ClockIn = this.Clock.Text;
            string Date = this.Date.Text;
            string Query = "INSERT INTO EmployeeTimeSlots (EmployeeFirstName, EmployeeSecondName, ClockIn, Date ) VALUES('" + EmployeeFirstName + "', '" + EmployeeLastName + "', '" + ClockIn + "', '" + Date + "')";

            SqlCommand cmd = new SqlCommand(Query, con);
            cmd.ExecuteNonQuery();

            con.Close();

            MessageBox.Show("Employee Clock In Time Has Been Entered!");
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 f2 = new Form2();
            f2.ShowDialog();
            this.Close();
        }
    }
}
