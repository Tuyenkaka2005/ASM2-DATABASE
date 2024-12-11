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
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace Product_Shoes
{
    public partial class Employee : Form
    {
        private string connectionString;
        private SqlConnection connection;
        public Employee()
        {
            InitializeComponent();
            connectionString = @"Data Source=TUYENPRO\SQLEXPRESS01;Initial Catalog=""ShoeSalesManager"";Integrated Security=True;TrustServerCertificate=True";

            connection = new SqlConnection(connectionString);
            dataGridViewEmployee.SelectionChanged += dataGridViewEmployee_SelectionChanged;

            LoadEmployeeIntoGridView();
        }
        private void LoadEmployeeIntoGridView()
        {
            try
            {
                string query = "SELECT * FROM [dbo].[Employee]";
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridViewEmployee.DataSource = dataTable;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void dataGridViewEmployee_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewEmployee.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridViewEmployee.SelectedRows[0];
                UpdateEmployeePanel(selectedRow);
            }
        }
        private void UpdateEmployeePanel(DataGridViewRow row)
        {
            try
            {
                txtEmployeeID.Text = row.Cells["EmployeeID"].Value.ToString();
                txtCode.Text = row.Cells["EmployeeCode"].Value.ToString();
                txtName.Text = row.Cells["EmployeeName"].Value.ToString();
                txtPosition.Text = row.Cells["Position"].Value.ToString();
                txtLevel.Text = row.Cells["AuthorityLevel"].Value.ToString();
                txtUserName.Text = row.Cells["Username"].Value.ToString();
                txtPassword.Text = row.Cells["Password"].Value.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating info panel: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtSearch.Text, out int employeeId))
            {
                DataGridViewRow employeeRow = null;
                foreach (DataGridViewRow row in dataGridViewEmployee.Rows)
                {
                    if (row.Cells["EmployeeID"].Value != null && (int)row.Cells["EmployeeID"].Value == employeeId)
                    {
                        employeeRow = row;
                        break;
                    }
                }

                if (employeeRow != null)
                {
                    StringBuilder result = new StringBuilder();
                    result.AppendLine($"Employee ID: {employeeId}");
                    result.AppendLine($"Employee Code: {employeeRow.Cells["EmployeeCode"].Value}");
                    result.AppendLine($"Employee Name: {employeeRow.Cells["EmployeeName"].Value}");
                    result.AppendLine($"Position: {employeeRow.Cells["Position"].Value}");
                    result.AppendLine($"Authority Level: {employeeRow.Cells["AuthorityLevel"].Value}");
                    result.AppendLine($"User Name: {employeeRow.Cells["Username"].Value}");
                    result.AppendLine($"Password: {employeeRow.Cells["Password"].Value}");
                    txtResult.Text = result.ToString();
                }
                else
                {
                    txtResult.Text = "Employee not found.";
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid Employee ID.");
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (ValidateTextBoxes())
            {
                try
                {
                    string checkUsernameQuery = "SELECT COUNT(*) FROM [dbo].[Employee] WHERE Username = @us";
                    using (SqlCommand checkCmd = new SqlCommand(checkUsernameQuery, connection))
                    {
                        checkCmd.Parameters.AddWithValue("@us", txtUserName.Text);

                        connection.Open();
                        int existingUserCount = (int)checkCmd.ExecuteScalar();
                        connection.Close();

                        if (existingUserCount > 0)
                        {
                            MessageBox.Show("Username already exists. Please choose a different username.",
                                "Username Conflict",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                            txtUserName.Focus();
                            return;
                        }
                    }
                    string query = "INSERT INTO [dbo].[Employee] " +
                        "(EmployeeCode,EmployeeName, Position, AuthorityLevel,Username,Password) " +
                        "VALUES (@code,@name, @pos, @AL,@us, @ps)";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@code", txtCode.Text);
                        cmd.Parameters.AddWithValue("@name", txtName.Text);
                        cmd.Parameters.AddWithValue("@pos", txtPosition.Text);
                        cmd.Parameters.AddWithValue("@AL", txtLevel.Text);
                        cmd.Parameters.AddWithValue("@us", txtUserName.Text); ;
                        cmd.Parameters.AddWithValue("@ps", txtPassword.Text);

                        connection.Open();
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Employee added successfully!", "Added Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    LoadEmployeeIntoGridView();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error adding Employee: " + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (ValidateTextBoxes())
            {
                try
                {
                    string checkUsernameQuery = "SELECT COUNT(*) FROM[dbo].[Employee] WHERE Username = @us";
                    using (SqlCommand checkCmd = new SqlCommand(checkUsernameQuery, connection))
                    {
                        checkCmd.Parameters.AddWithValue("@us", txtUserName.Text);
                        connection.Open();
                        int existingUserCount = (int)checkCmd.ExecuteScalar();
                        connection.Close();

                        if (existingUserCount > 0)
                        {
                            MessageBox.Show("Username already exists. Please choose a different username.",
                                "Username Conflict",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                            txtUserName.Focus();
                            return;
                        }
                    }
                    string query = "UPDATE [dbo].[Employee]" +
                        " SET EmployeeCode = @code," +
                        "EmployeeName = @name, Position = @pos, AuthorityLevel = @AL," +
                        "Username = @us , Password = @ps WHERE EmployeeID = @id";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", int.Parse(txtEmployeeID.Text));
                        cmd.Parameters.AddWithValue("@name", txtName.Text);
                        cmd.Parameters.AddWithValue("@code", txtCode.Text);
                        cmd.Parameters.AddWithValue("@pos", txtPosition.Text);
                        cmd.Parameters.AddWithValue("@AL", txtLevel.Text);
                        cmd.Parameters.AddWithValue("@us", txtUserName.Text);
                        cmd.Parameters.AddWithValue("@ps", txtPassword.Text);

                        connection.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        connection.Close();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Employee updated successfully!", "Updated Success",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                            LoadEmployeeIntoGridView();
                        }
                        else
                        {
                            MessageBox.Show("No Employee found with the given ID.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating Employee: " + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Are you sure you want to delete this employee?",
                    "Confirm Deletion",
                     MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    string query = "DELETE FROM [dbo].[Employee] WHERE EmployeeID = @id";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", int.Parse(txtEmployeeID.Text));

                        connection.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        connection.Close();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Employee deleted successfully!",
                                "Deteted Success",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                            LoadEmployeeIntoGridView();
                            txtEmployeeID.Clear();
                            txtCode.Clear();
                            txtName.Clear();
                            txtLevel.Clear();
                            txtPosition.Clear();
                            txtUserName.Clear();
                            txtPassword.Clear();

                        }
                        else
                        {
                            MessageBox.Show("No Employee found with the given ID.");
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting Employee: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            AdminForm adminForm = new AdminForm();
            adminForm.ShowDialog();
            this.Close();
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
        public bool ValidateTextBoxes()
        {
            TextBox[] textBoxesToValidate = new TextBox[]
            {
                txtCode,
                txtName,
                txtLevel,
                txtPosition, 
                txtUserName,
                txtPassword,

        };
            foreach (TextBox textBox in textBoxesToValidate)
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    MessageBox.Show(
                        $"Please fill in the {textBox.Name.Replace("textBox", "")} field.",
                        "Validation Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    textBox.Focus();
                    return false;
                }
            }
            return true;
        }
    }
}
