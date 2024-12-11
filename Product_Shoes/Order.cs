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
    public partial class Order : Form
    {
        private string connectionString;
        private SqlConnection connection;
        public Order()
        {
            InitializeComponent();
            connectionString = @"Data Source=TUYENPRO\SQLEXPRESS01;Initial Catalog=""ShoeSalesManager"";Integrated Security=True;TrustServerCertificate=True";

            connection = new SqlConnection(connectionString);

            LoadOrderIntoGridView();
            LoadOrderDetailIntoGridView();
            dataGridViewOrder.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewOrderDetail.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }
        private void LoadOrderIntoGridView()
        {
            try
            {
                string query = "SELECT " +
                    "o.OrderID, " +
                    "o.OrderDate, " +
                    "c.CustomerName, " +
                    "e.EmployeeName, " +
                    "o.TotalAmount, " +
                    "o.Profit " +
                    "FROM " +
                    "[dbo].[Order] o " +
                    "INNER JOIN " +
                    "Customer c ON o.CustomerID = c.CustomerID " +
                    "INNER JOIN" +
                    " Employee e ON o.EmployeeID = e.EmployeeID";

                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridViewOrder.DataSource = dataTable;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error loading customer data: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadOrderDetailIntoGridView()
        {
            try
            {
                string query = "SELECT" +
                    " o.OrderDetailID," +
                    "o.OrderID, " +
                    "p.ProductName," +
                    "o.QuantitySold " +
                    "FROM  " +
                    "[dbo].[OrderDetail] o " +
                    "INNER JOIN " +
                    "[dbo].[Product_Shoes] p ON o.ProductID = p.ProductID";

                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridViewOrderDetail.DataSource = dataTable;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error loading customer data: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SearchOrder_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtSearchOrder.Text, out int orderId))
            {
                DataGridViewRow orderRow = null;
                foreach (DataGridViewRow row in dataGridViewOrder.Rows)
                {
                    if (row.Cells["OrderID"].Value != null && (int)row.Cells["OrderID"].Value == orderId)
                    {
                        orderRow = row;
                        break;
                    }
                }

                if (orderRow != null)
                {

                    DataGridViewRow orderDetailRow = null;
                    foreach (DataGridViewRow row in dataGridViewOrderDetail.Rows)
                    {
                        if (row.Cells["OrderID"].Value != null && (int)row.Cells["OrderID"].Value == orderId)
                        {
                            orderDetailRow = row;
                            break;
                        }
                    }
                    StringBuilder result = new StringBuilder();
                    result.AppendLine($"Order ID: {orderId}");
                    result.AppendLine($"Order Date: {((DateTime)orderRow.Cells["OrderDate"].Value).ToShortDateString()}");
                    result.AppendLine($"Customer Name: {orderRow.Cells["CustomerName"].Value}");
                    result.AppendLine($"Employee Name: {orderRow.Cells["EmployeeName"].Value}");
                    result.AppendLine($"Total Amount: {((decimal)orderRow.Cells["TotalAmount"].Value):C}");
                    result.AppendLine($"Profit: {((decimal)orderRow.Cells["Profit"].Value):C}");

                    if (orderDetailRow != null)
                    {
                        result.AppendLine($"Order Detail ID: {orderDetailRow.Cells["OrderDetailID"].Value}");
                        result.AppendLine($"Product Name: {orderDetailRow.Cells["ProductName"].Value}");
                        result.AppendLine($"Quantity Sold: {orderDetailRow.Cells["QuantitySold"].Value}");
                    }
                    else
                    {
                        result.AppendLine("No order detail found for this order.");
                    }
                    txtResultOrder.Text = result.ToString();
                }
                else
                {
                    txtResultOrder.Text = "Order not found.";
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid Order ID.");
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            string currentRole = UserSession.UserRole;
            switch (currentRole)
            {
                case "Admin":
                    this.Hide();
                    AdminForm adminForm = new AdminForm();
                    adminForm.ShowDialog();
                    break;
                case "Staff":
                    this.Hide();
                    StaffForm staffForm = new StaffForm();
                    staffForm.ShowDialog();
                    break;

                default:
                    MessageBox.Show("Unknown user role", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to exit the application?",
                                                 "Confirm Exit",
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void txtResultOrder_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
