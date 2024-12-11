using Sales_Management;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Product_Shoes
{
    public partial class StaffForm : Form
    {
        
        public StaffForm()
        {
            InitializeComponent();
           
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            this.Hide();
            Product Form = new Product();
            Form.ShowDialog();
            this.Close();
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            this.Hide();
            Customer Form = new Customer();
            Form.ShowDialog();
            this.Close();
        }

        private void btnOder_Click(object sender, EventArgs e)
        {
            this.Hide();
            Order Form = new Order();
            Form.ShowDialog();
            this.Close();
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            this.Hide();
            Category Form = new Category("Staff");
            Form.ShowDialog();
            this.Close();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm loginForm = new LoginForm();
            loginForm.ShowDialog();
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
