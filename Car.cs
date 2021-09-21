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
    public partial class Car : Form
    {
        public Car()
        {
            InitializeComponent();
        }
        //connects to the database
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\gansievoges\Documents\CarRentaldb.mdf;Integrated Security=True;Connect Timeout=30");
        
        //method
        private void populate()
        {
            Con.Open();
            string query = "select * from CarTbl";
            SqlDataAdapter da = new SqlDataAdapter(query, Con);
            SqlCommandBuilder build = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            CarsDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (RegTb.Text == "" || BrandTb.Text == "" || ModelTb.Text == "" || PriceTb.Text == "" || MileageBx.Text == "")
            {
                MessageBox.Show("Missing information required");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "insert into CarTbl values('" + RegTb.Text + "', '" + BrandTb.Text + "','" + ModelTb.Text + "','" + MileageBx.Text + "','" + YearBx.SelectedItem.ToString() +"','" + AvailableBx.SelectedItem.ToString()+"',"+PriceTb.Text+")";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Car Successfully added");
                    Con.Close();
                    populate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Car_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void CarsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var rowIndex = e.RowIndex;
            var row = CarsDGV.Rows[rowIndex];
            RegTb.Text = row.Cells[0].Value.ToString();
            BrandTb.Text = row.Cells[1].Value.ToString();
            ModelTb.Text = row.Cells[2].Value.ToString();
            PriceTb.Text = row.Cells[3].Value.ToString();
            MileageBx.Text = row.Cells[4].Value.ToString();
            YearBx.SelectedItem = row.Cells[5].Value.ToString();
            AvailableBx.SelectedItem = row.Cells[6].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (RegTb.Text == "" || BrandTb.Text == "" || ModelTb.Text == "" || PriceTb.Text == "" || MileageBx.Text == "")
            {
                MessageBox.Show("Missing information required");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "update CarTbl set Brand ='" + BrandTb.Text + "',Model='" + ModelTb.Text + "', Available = '" + AvailableBx.SelectedItem.ToString() + "', Mileage= '" + MileageBx.Text + "',Year= '" + YearBx.SelectedItem.ToString() + "', Price= '" + PriceTb.Text + "' where RegNum='" + RegTb.Text + "';";
                        
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Car Successfully updated");
                    Con.Close();
                    populate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (RegTb.Text == "")
            {
                MessageBox.Show("Missing Information required");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "delete from CarTbl where RegNum = '" + RegTb.Text + "';";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Car Successfully deleted");
                    Con.Close();
                    populate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm main = new MainForm();  //instantiates an object
            main.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            populate();
        }

        private void Search_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string flag = "";
            if (Search.SelectedItem.ToString() == "Available")
            {
                flag = "Yes";
            }
            else
            {
                flag = "No";
            }
            Con.Open();
            string query = "select * from CarTbl where Available = '" + flag+"'";
            SqlDataAdapter da = new SqlDataAdapter(query, Con);
            SqlCommandBuilder build = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            CarsDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
    }
}
