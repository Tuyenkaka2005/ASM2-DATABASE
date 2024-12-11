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
    public partial class Category : Form
    {
        private string connectionString;
        private SqlConnection connection;
        public Category(string role)
        {
            InitializeComponent();
            connectionString = @"Data Source=TUYENPRO\SQLEXPRESS01;Initial Catalog=""ShoeSalesManager"";Integrated Security=True;TrustServerCertificate=True";

            connection = new SqlConnection(connectionString);
            LoadCategoryIntoGridView();
            dataGridViewCategory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }
        private void LoadCategoryIntoGridView()
        {
            try
            {
                string query = "SELECT * FROM [dbo].[Category]";
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridViewCategory.DataSource = dataTable;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error loading category data: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}
