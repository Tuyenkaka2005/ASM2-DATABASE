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
    
    public partial class AdminForm : Form
    {
        public AdminForm()
        {
            InitializeComponent();    
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            this.Hide();
            Product productForm = new Product();
            productForm.ShowDialog();
            this.Close();
        }

        private void btnEmployee_Click(object sender, EventArgs e)
        {
            this.Hide();
            Employee employeeForm = new Employee();
            employeeForm.ShowDialog();
            this.Close();
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            this.Hide();
            Customer customerForm = new Customer();
            customerForm.ShowDialog();
            this.Close();
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            this.Hide();
            Order orderForm = new Order();
            orderForm.ShowDialog();
            this.Close();

        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            this.Hide();
            Import importForm = new Import();
            importForm.ShowDialog();
            this.Close();
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            this.Hide();
            Category categoryForm = new Category("Admin");
            categoryForm.ShowDialog();
            this.Close();
        }

        private void btnBrand_Click(object sender, EventArgs e)
        {
            this.Hide();
            Brand brandForm = new Brand();
            brandForm.ShowDialog();
            this.Close();
        }

        private void btnSupplier_Click(object sender, EventArgs e)
        {
            this.Hide();
            Supplier supplierForm = new Supplier();
            supplierForm.ShowDialog();
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
