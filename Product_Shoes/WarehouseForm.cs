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
    public partial class WarehouseForm : Form
    {
        public WarehouseForm()
        {
            InitializeComponent();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            this.Hide();
            Import Form = new Import();
            Form.ShowDialog();
            this.Close();
        }

        private void btnBrand_Click(object sender, EventArgs e)
        {
            this.Hide();
            Brand Form = new Brand();
            Form.ShowDialog();
            this.Close();
        }

        private void btnSupplier_Click(object sender, EventArgs e)
        {
            this.Hide();
            Supplier Form = new Supplier();
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
