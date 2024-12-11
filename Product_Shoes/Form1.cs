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
using Product_Shoes;

namespace Sales_Management
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }
        private string placeholderUsername = "Username";
        private string placeholderPassword = "Password";
        private void Form1_Load(object sender, EventArgs e)
        {
            txtUserName.Text = placeholderUsername;
            txtUserName.ForeColor = Color.Gray;
            txtPassword.Text = placeholderPassword;
            txtPassword.ForeColor = Color.Gray;

        }

        private void txtUsername_Enter(object sender, EventArgs e)
        {
            if (txtUserName.Text == placeholderUsername)
            {
                txtUserName.Text = "";
                txtUserName.ForeColor = Color.Black;
            }
        }
        private void txtUsername_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUserName.Text))
            {
                txtUserName.Text = placeholderUsername;
                txtUserName.ForeColor = Color.Gray;
            }
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            if (txtPassword.Text == placeholderPassword)
            {
                txtPassword.Text = "";
                txtPassword.ForeColor = Color.Black;
            }
        }
        private void txtPassword_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                txtPassword.Text = placeholderPassword;
                txtPassword.ForeColor = Color.Gray;
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



        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUserName.Text;
            string password = txtPassword.Text;

            if (AuthenticateUser(username, password, out string userRole))
            {
                MessageBox.Show($"Welcome, {username}! Login successful.",
                "Login Success",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
                OpenAppropriateInterface(userRole, username);
            }
            else
            {
                MessageBox.Show("Invalid username or password.",
                    "Login Failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        private bool AuthenticateUser(string username, string password, out string userRole)
        {
            userRole = string.Empty;
            string connectionString = @"Data Source=TUYENPRO\SQLEXPRESS01;Initial Catalog=""ShoeSalesManager"";Integrated Security=True;TrustServerCertificate=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"SELECT AuthorityLevel, PasswordChanged FROM [dbo].[Employee] WHERE Username = @UserName AND Password = @Password";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserName", username);
                        command.Parameters.AddWithValue("@Password", password);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                userRole = reader["AuthorityLevel"].ToString();
                                int passwordChange = Convert.ToInt32(reader["PasswordChanged"]);

                                if (passwordChange == 0)
                                {
                                    MessageBox.Show("Change password first time login","", MessageBoxButtons.OK,
                                         MessageBoxIcon.Error);
                                    this.Hide();
                                    ChangedPassword changePasswordForm = new ChangedPassword();
                                    changePasswordForm.ShowDialog();
                                    this.Close();
                                    return false;
                                }
                                return true;
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Database error: {ex.Message}",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            return false;
        }

        private void OpenAppropriateInterface(string userRole, string username)
        {
            UserSession.UserRole = userRole;
            Form interfaceForm = null;

            switch (userRole)
            {
                case "Admin":
                    interfaceForm = new AdminForm();
                    break;
                case "Staff":
                    interfaceForm = new StaffForm();
                    break;
                case "WareHouse":
                    interfaceForm = new WarehouseForm();
                    break;
            }
            if (interfaceForm != null)
            {
                this.Hide();
                interfaceForm.ShowDialog();
                this.Close();
            }

        }


    }
}
