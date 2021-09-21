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
    public partial class Rental : Form
    {
        public Rental()
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
        private void fillcombo()
        {
            Con.Open();
            string query = "select RegNum from CarTbl where Available='" +"Yes"+"'";
            SqlCommand cmd = new SqlCommand(query,Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("RegNum",typeof(string));
            dt.Load(rdr);
            CarRegBx.ValueMember = "RegNum";
            CarRegBx.DataSource = dt;
            Con.Close();
        }
        private void fillCustomer()
        {
            Con.Open();
            string query = "select CustId from CustomerTbl";
            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CustId", typeof(int));
            dt.Load(rdr);
            CustIdBx.ValueMember = "CustId";
            CustIdBx.DataSource = dt;
            Con.Close();
        }
        private void fetchCustName()
        {
            Con.Open();
            string query = "select * from CustomerTbl where CustId = " + CustIdBx.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach(DataRow dr in dt.Rows)
            {
                CustNameTb.Text = dr["CustName"].ToString();
            }
            Con.Close();
        }
        private void UpdateonRent()
        {
            Con.Open();
            string query = "update CarTbl set Available = '" + "No"+ "' where RegNum= '" + CarRegBx.SelectedValue.ToString() + "';";

            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.ExecuteNonQuery();
            //MessageBox.Show("Car Successfully updated");
            Con.Close();
        }
        private void UpdateonRentDelete()
        {
            Con.Open();
            string query = "update CarTbl set Available = '" + "Yes" + "' where RegNum= '" + CarRegBx.SelectedValue.ToString() + "';";

            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.ExecuteNonQuery();
            //MessageBox.Show("Car Successfully updated");
            Con.Close();
        }
        private void Rental_Load(object sender, EventArgs e)
        {
            fillcombo();
            fillCustomer();
            populate();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void CarRegBx_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }

        private void CustIdBx_SelectionChangeCommitted(object sender, EventArgs e)
        {
            fetchCustName();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (IdTb.Text == "" || CustNameTb.Text == "" || FeesTb.Text == "")
            {
                MessageBox.Show("Missing information required");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "insert into RentalTbl values(" + IdTb.Text + ", '" + CarRegBx.SelectedValue.ToString() + "','" + CustNameTb.Text + "','" + RentDate.Text + "','" + ReturnDate.Text + "','" + FeesTb.Text + "')";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Car Successfully rented");
                    Con.Close();
                    UpdateonRent();
                    populate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void RentDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var rowIndex = e.RowIndex;
            var row = RentDGV.Rows[rowIndex];
            IdTb.Text = row.Cells[0].Value.ToString();
            CarRegBx.SelectedValue = row.Cells[1].Value.ToString();
            FeesTb.Text = row.Cells[5].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm main = new MainForm();  //instantiates an object
            main.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (IdTb.Text == "")
            {
                MessageBox.Show("Missing Information required");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "delete from RentalTbl where RentId = '" + IdTb.Text + "';";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Rental Successfully deleted");
                    Con.Close();
                    populate();
                    UpdateonRentDelete();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
