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
    public partial class Customer : Form
    {
        private string connectionString;
        private SqlConnection connection;
        public Customer()
        {
            InitializeComponent();
            connectionString = @"Data Source=TUYENPRO\SQLEXPRESS01;Initial Catalog=""ShoeSalesManager"";Integrated Security=True;TrustServerCertificate=True";

            connection = new SqlConnection(connectionString);
            dataGridViewCustomer.SelectionChanged += dataGridViewCustomer_SelectionChanged;
            LoadCustomerIntoGridView();
        }
        private void dataGridViewCustomer_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewCustomer.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridViewCustomer.SelectedRows[0];
                txtCustomerID.Text = selectedRow.Cells["CustomerID"].Value.ToString();
                txtCustomerCode.Text = selectedRow.Cells["CustomerCode"].Value.ToString();
                txtCustomerName.Text = selectedRow.Cells["CustomerName"].Value.ToString();
                txtPhone.Text = selectedRow.Cells["PhoneNumber"].Value.ToString();
                txtEmail.Text = selectedRow.Cells["Email"].Value.ToString();
                txtAddress.Text = selectedRow.Cells["Address"].Value.ToString();
            }
        }
        private void LoadCustomerIntoGridView()
        {
            try
            {
                string query = "SELECT * FROM [dbo].[Customer]";
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridViewCustomer.DataSource = dataTable;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error loading customer data: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SearchCustomer_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtSearchInfo.Text, out int customerId))
            {
                DataGridViewRow customerRow = null;
                foreach (DataGridViewRow row in dataGridViewCustomer.Rows)
                {
                    if (row.Cells["CustomerID"].Value != null && (int)row.Cells["CustomerID"].Value == customerId)
                    {
                        customerRow = row;
                        break;
                    }
                }

                if (customerRow != null)
                {
                    StringBuilder result = new StringBuilder();
                    result.AppendLine($"Customer ID: {customerId}");
                    result.AppendLine($"Customer Code: {customerRow.Cells["CustomerCode"].Value}");
                    result.AppendLine($"Customer Name: {customerRow.Cells["CustomerName"].Value}");
                    result.AppendLine($"Phone Number: {customerRow.Cells["PhoneNumber"].Value}");
                    result.AppendLine($"Address: {customerRow.Cells["Address"].Value}");
                    result.AppendLine($"Email: {customerRow.Cells["Email"].Value}");
                    txtResultCustomer.Text = result.ToString();
                }
                else
                {
                    txtResultCustomer.Text = "Customer not found.";
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid Customer ID.");
            }
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            string code = txtCustomerCode.Text;
            string fullname = txtCustomerName.Text;
            string email = txtEmail.Text;
            string address = txtAddress.Text;
            string phone = txtPhone.Text;

            if (string.IsNullOrEmpty(fullname) || string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(address) || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(code))
            {
                MessageBox.Show("All fields are required. Please fill in all the information.",
                    "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string checkPhoneQuery = "SELECT COUNT(*) FROM [dbo].[Customer] WHERE PhoneNumber = @phone";
                string checkEmailQuery = "SELECT COUNT(*) FROM [dbo].[Customer] WHERE Email = @email";
                bool phoneExists = false;
                bool emailExists = false;

                using (SqlCommand checkPhoneCmd = new SqlCommand(checkPhoneQuery, connection))
                {
                    checkPhoneCmd.Parameters.AddWithValue("@phone", txtPhone.Text);
                    connection.Open();
                    int existingPhoneCount = (int)checkPhoneCmd.ExecuteScalar();
                    connection.Close();

                    if (existingPhoneCount > 0)
                    {
                        phoneExists = true;
                        MessageBox.Show("Phone Number already exists. Please choose a different Phone Number.",
                            "Phone Number Conflict",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        txtPhone.Focus();
                    }
                }
                using (SqlCommand checkEmailCmd = new SqlCommand(checkEmailQuery, connection))
                {
                    checkEmailCmd.Parameters.AddWithValue("@email", txtEmail.Text);
                    connection.Open();
                    int existingEmailCount = (int)checkEmailCmd.ExecuteScalar();
                    connection.Close();

                    if (existingEmailCount > 0)
                    {
                        emailExists = true;
                        MessageBox.Show("Email already exists. Please choose a different Email.",
                            "Email Conflict",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        txtEmail.Focus();
                    }
                }
                if (phoneExists || emailExists)
                {
                    return;
                }
                string insertCustomerQuery = "INSERT INTO Customer (CustomerCode, CustomerName, PhoneNumber,Email,Address) " +
                "VALUES (@code, @name, @phone, @email, @address)";
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(insertCustomerQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@code", code);
                    cmd.Parameters.AddWithValue("@name", fullname);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@phone", phone);
                    cmd.Parameters.AddWithValue("@address", address);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }

             MessageBox.Show("Registration successful!", "Registration Success",
             MessageBoxButtons.OK,
             MessageBoxIcon.Information);
             RefreshDataGridView();

             dataGridViewCustomer.Refresh();
             txtCustomerCode.Clear();
             txtCustomerName.Clear();
             txtPhone.Clear();
             txtEmail.Clear();
             txtAddress.Clear();

                
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private int GenerateCustomerID(SqlConnection connection)
        {
            string query = "SELECT ISNULL(MAX(CustomerID), 0) + 1 FROM Customer";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        private string GenerateCustomerID()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmss");
        }
        private void RefreshDataGridView()
        {
            if (dataGridViewCustomer.DataSource is DataTable dataTable)
            {
                dataTable.Clear();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string selectQuery = "SELECT * FROM Customer";
                    using (SqlDataAdapter adapter = new SqlDataAdapter(selectQuery, connection))
                    {
                        adapter.Fill(dataTable);
                    }
                }
                dataGridViewCustomer.Refresh();
            }
        }

        private void btnEditCustomer_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs())
            {
                return;
            }

            try
            {
                string checkPhoneQuery = "SELECT COUNT(*) FROM [dbo].[Customer] WHERE PhoneNumber = @phone";
                string checkEmailQuery = "SELECT COUNT(*) FROM [dbo].[Customer] WHERE Email = @email";
                bool phoneExists = false;
                bool emailExists = false;
                using (SqlCommand checkPhoneCmd = new SqlCommand(checkPhoneQuery, connection))
                {
                    checkPhoneCmd.Parameters.AddWithValue("@phone", txtPhone.Text);
                    connection.Open();
                    int existingPhoneCount = (int)checkPhoneCmd.ExecuteScalar();
                    connection.Close();

                    if (existingPhoneCount > 0)
                    {
                        phoneExists = true;
                        MessageBox.Show("Phone Number already exists. Please choose a different Phone Number.",
                            "Phone Number Conflict",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        txtPhone.Focus();
                    }
                }

                using (SqlCommand checkEmailCmd = new SqlCommand(checkEmailQuery, connection))
                {
                    checkEmailCmd.Parameters.AddWithValue("@email", txtEmail.Text);
                    connection.Open();
                    int existingEmailCount = (int)checkEmailCmd.ExecuteScalar();
                    connection.Close();

                    if (existingEmailCount > 0)
                    {
                        emailExists = true;
                        MessageBox.Show("Email already exists. Please choose a different Email.",
                            "Email Conflict",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        txtEmail.Focus();
                    }
                }

                if (phoneExists || emailExists)
                {
                    return;
                }
                string query = "UPDATE [dbo].[Customer] " +
                    "SET CustomerCode =@code, CustomerName = @name, PhoneNumber = @phone," +
                    " Address = @address, Email = @email WHERE CustomerID = @id";
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@id", int.Parse(txtCustomerID.Text));
                    cmd.Parameters.AddWithValue("@code", txtCustomerCode.Text);
                    cmd.Parameters.AddWithValue("@name", txtCustomerName.Text);
                    cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
                    cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text);

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Customer updated successfully!", "Updated Success",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        LoadCustomerIntoGridView();
                    }
                    else
                    {
                        MessageBox.Show("No customer found with the given ID.", 
                            "Update Failed", 
                            MessageBoxButtons.OK, 
                            MessageBoxIcon.Error);
                    }
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter valid numeric values for Customer ID and Phone.");
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating customer: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtCustomerCode.Text) ||
                string.IsNullOrWhiteSpace(txtCustomerName.Text) ||
                string.IsNullOrWhiteSpace(txtPhone.Text) ||
                string.IsNullOrWhiteSpace(txtAddress.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("All fields are required. Please fill in all the information.");
                return false;
            }
            return true;
        }

        private void btnDeleteCustomer_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCustomerID.Text))
            {
                MessageBox.Show("Please enter a valid Customer ID.");
                return;
            }

            try
            {
                DialogResult result = MessageBox.Show("Are you sure you want to delete this customer?",
                    "Confirm Deletion",
                     MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        using (SqlTransaction transaction = connection.BeginTransaction())
                        {
                            try
                            {
                                int customerID = int.Parse(txtCustomerID.Text);

                                string deleteCustomerQuery = "DELETE FROM [dbo].[Customer] WHERE CustomerID = @id";
                                using (SqlCommand deleteCustomerCmd = new SqlCommand(deleteCustomerQuery, connection, transaction))
                                {
                                    deleteCustomerCmd.Parameters.AddWithValue("@id", customerID);
                                    int customerDeleted = deleteCustomerCmd.ExecuteNonQuery();

                                    if (customerDeleted > 0)
                                    {
                                        transaction.Commit();
                                        MessageBox.Show("Customer deleted successfully!",
                                        "Deletion Confirmed",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);

                                        LoadCustomerIntoGridView();
                                        ClearInputFields();
                                    }
                                    else
                                    {
                                        transaction.Rollback();
                                        MessageBox.Show("No Customer found with the given ID.");
                                    }
                                }
                            }
                            catch (Exception)
                            {
                                transaction.Rollback();
                                throw;
                            }
                        }
                    }
                }
            }
                
            catch (FormatException)
            {
                MessageBox.Show("Please enter a valid numeric value for Customer ID.");
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting customer: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ClearInputFields()
        {
            txtCustomerID.Clear();
            txtCustomerName.Clear();
            txtEmail.Clear();
            txtAddress.Clear();
            txtPhone.Clear();
            
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
