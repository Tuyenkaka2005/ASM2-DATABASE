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
    public partial class Import : Form
    {
        private string connectionString;
        private SqlConnection connection;
        public Import()
        {
            InitializeComponent();
            connectionString = @"Data Source=TUYENPRO\SQLEXPRESS01;Initial Catalog=""ShoeSalesManager"";Integrated Security=True;TrustServerCertificate=True";

            connection = new SqlConnection(connectionString);
            dataGridViewImport.SelectionChanged += dataGridViewImport_SelectionChanged;
            dataGridViewImportDetail.SelectionChanged += dataGridViewImportDetail_SelectionChanged;

            LoadImportIntoGridView();
            LoadImportDetailIntoGridView();

            dataGridViewImport.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewImport.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

        }
        private void LoadImportIntoGridView()
        {
            try
            {
                string query = "SELECT * FROM [dbo].[Import]";

                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridViewImport.DataSource = dataTable;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error loading import data: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void dataGridViewImport_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewImport.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridViewImport.SelectedRows[0];
                UpdateInfoPanel(selectedRow);
            }
        }
        private void UpdateInfoPanel(DataGridViewRow row)
        {
            try
            {
                txtImportID.Text = row.Cells["ImportID"].Value.ToString();
                txtImportDate.Text = row.Cells["ImportDate"].Value.ToString();
                txtTotal.Text = row.Cells["TotalQuantity"].Value.ToString();
                txtEmployeeID.Text = row.Cells["EmployeeID"].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating info panel: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        //private void btnSearchImport_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string query = "SELECT * FROM [dbo].[Import] WHERE ImportID = @id";
        //        using (SqlCommand cmd = new SqlCommand(query, connection))
        //        {
        //            cmd.Parameters.AddWithValue("@id", int.Parse(txtSearch.Text));

        //            connection.Open();
        //            using (SqlDataReader reader = cmd.ExecuteReader())
        //            {
        //                if (reader.Read())
        //                {
        //                    txtImportID.Text = reader["ImportID"].ToString();
        //                    txtImportDate.Text = reader["ImportDate"].ToString();
        //                    txtTotal.Text = reader["TotalQuantity"].ToString();
        //                    txtEmployeeName.Text = reader["EmployeeName"].ToString();
        //                }
        //                else
        //                {
        //                    MessageBox.Show("Import not found!");
        //                }
        //            }
        //            connection.Close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error searching import: " + ex.Message,
        //            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        private void btnEditImport_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "UPDATE [dbo].[Import] SET ImportDate = @date,EmployeeID = @employee, TotalQuantity = @quantity WHERE ImportID = @id";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@id", int.Parse(txtImportID.Text));
                    cmd.Parameters.AddWithValue("@date", txtImportDate.Text);
                    cmd.Parameters.AddWithValue("@employee", int.Parse(txtEmployeeID.Text));
                    cmd.Parameters.AddWithValue("@quantity", int.Parse(txtTotal.Text));

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    connection.Close();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Import updated successfully!");
                        LoadImportIntoGridView();
                    }
                    else
                    {
                        MessageBox.Show("No Import found with the given ID.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating Import: " + ex.Message,
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
                case "WareHouse":
                    this.Hide();
                    WarehouseForm warehouseForm = new WarehouseForm();
                    warehouseForm.ShowDialog();
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
        ///////////////////
        ///
        private void LoadImportDetailIntoGridView()
        {
            try
            {
                string query = "SELECT * FROM [dbo].[ImportDetail]";

                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridViewImportDetail.DataSource = dataTable;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error loading import detail data: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void dataGridViewImportDetail_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewImportDetail.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridViewImportDetail.SelectedRows[0];
                UpdateDetailPanel(selectedRow);
            }
        }
        private void UpdateDetailPanel(DataGridViewRow row)
        {
            try
            {
                txtDetailID.Text = row.Cells["ImportDetailID"].Value.ToString();
                txtImportID1.Text = row.Cells["ImportID"].Value.ToString();
                txtProductID.Text = row.Cells["ProductID"].Value.ToString();
                txtQuantityI.Text = row.Cells["QuantityImported"].Value.ToString();
                txtCost.Text = row.Cells["ImportCost"].Value.ToString();
                txtSupplierID.Text = row.Cells["SupplierID"].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating info panel: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

       

        //private void btnSearchDetail_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string query = "SELECT * FROM [dbo].[ImportDetail] WHERE ImportDetailID = @id";
        //        using (SqlCommand cmd = new SqlCommand(query, connection))
        //        {
        //            cmd.Parameters.AddWithValue("@id", int.Parse(txtSearchDetail.Text));

        //            connection.Open();
        //            using (SqlDataReader reader = cmd.ExecuteReader())
        //            {
        //                if (reader.Read())
        //                {
        //                    txtDetailID.Text = reader["ImportDetailID"].ToString();
        //                    txtImportID1.Text = reader["ImportID"].ToString();
        //                    txtProductID.Text = reader["ProductID"].ToString();
        //                    txtQuantityI.Text = reader["QuantityImported"].ToString();
        //                    txtCost.Text = reader["ImportCost"].ToString();
        //                }
        //                else
        //                {
        //                    MessageBox.Show("Import Detail not found!");
        //                }
        //            }
        //            connection.Close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error searching import detail: " + ex.Message,
        //            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        private void btnEditDetail_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "UPDATE [dbo].[ImportDetail] SET ImportID = @import,ProductID = @product, QuantityImported = @quantity ,ImportCost = @cost,SupplierID = @supplier WHERE ImportDetailID = @id";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@id", int.Parse(txtDetailID.Text));
                    cmd.Parameters.AddWithValue("@import", int.Parse(txtImportID.Text));
                    cmd.Parameters.AddWithValue("@product", int.Parse(txtProductID.Text));
                    cmd.Parameters.AddWithValue("@quantity", int.Parse(txtQuantityI.Text));
                    cmd.Parameters.AddWithValue("@cost", decimal.Parse(txtCost.Text));
                    cmd.Parameters.AddWithValue("@supplier", int.Parse(txtSupplierID.Text));


                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    connection.Close();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Import Detail updated successfully!");
                        LoadImportDetailIntoGridView();
                    }
                    else
                    {
                        MessageBox.Show("No Import Detail found with the given ID.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating Import Detail: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
