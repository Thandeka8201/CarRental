using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRental
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();    //closes the whole application
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Car car = new Car();  //instantiates an object
            car.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Customer customer = new Customer();  //instantiates an object
            customer.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Rental rent = new Rental();  //instantiates an object
            rent.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Return ret = new Return();  //instantiates an object
            ret.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Users user = new Users();  //instantiates an object
            user.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            Dashboard dash = new Dashboard();    //instantiates an object
            dash.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login log = new Login();   //instantiates an object
            log.Show();
        }
    }
}
