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

namespace CarRental
{
    public partial class Return : Form
    {
        public Return()
        {
            InitializeComponent();
        }
        //connects to the database
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\gansievoges\Documents\CarRentaldb.mdf;Integrated Security=True;Connect Timeout=30");
        //method
        private void populate()
        {
            Con.Open();
            string query = "select * from RentalTbl";
            SqlDataAdapter da = new SqlDataAdapter(query, Con);
            SqlCommandBuilder build = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            RentDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void populateRet()
        {
            Con.Open();
            string query = "select * from ReturnTbl";
            SqlDataAdapter da = new SqlDataAdapter(query, Con);
            SqlCommandBuilder build = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            ReturnDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void Return_Load(object sender, EventArgs e)
        {
            populate();
            populateRet();
        }

        private void RentDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var rowIndex = e.RowIndex;
            var row = RentDGV.Rows[rowIndex];
            CarIdTb.Text = row.Cells[1].Value.ToString();
            CustNameTb.Text = row.Cells[2].Value.ToString();
            ReturnDate.Text = row.Cells[4].Value.ToString();
            DateTime d1 = ReturnDate.Value.Date;
            DateTime d2 = DateTime.Now;
            TimeSpan t = d2 - d1;
            int numOfDays = Convert.ToInt32(t.TotalDays);
            if(numOfDays <= 0)
            {
                DelayTb.Text = "No delay";
                FineTb.Text = "0";
            }
            else
            {
                DelayTb.Text = "" + numOfDays;
                FineTb.Text = "" + (numOfDays * 250);  //amount paid as fine
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm main = new MainForm();  //instantiates an object
            main.Show();
        }
        private void DeleteonReturn()
        {
            int rentId;
            rentId = Convert.ToInt32(RentDGV.SelectedRows[0].Cells[0].Value.ToString());
            Con.Open();
            string query = "delete from RentalTbl where RentId = " + rentId + ";";
            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.ExecuteNonQuery();
            //MessageBox.Show("Rental Successfully deleted");
            Con.Close();
            populate();
            //UpdateonRentDelete();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (IdTb.Text == "" || CustNameTb.Text == "" || FineTb.Text == "" || DelayTb.Text == "" || CarIdTb.Text == "")
            {
                MessageBox.Show("Missing information required");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "insert into ReturnTbl values(" + IdTb.Text + ",'" + CarIdTb.Text + "','" + CustNameTb.Text + "','" + DelayTb.Text + "','" + ReturnDate.Text + "','" + FineTb.Text + "')";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Car dully returned");
                    Con.Close();
                    DeleteonReturn();
                    populateRet();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
