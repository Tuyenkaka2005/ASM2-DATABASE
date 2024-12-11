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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Product_Shoes
{
    public partial class Product : Form
    {
        private string connectionString;
        private SqlConnection connection;
        public Product()
        {
            InitializeComponent();
            connectionString = @"Data Source=TUYENPRO\SQLEXPRESS01;Initial Catalog=""ShoeSalesManager"";Integrated Security=True;TrustServerCertificate=True";

            connection = new SqlConnection(connectionString);
            dataGridViewProduct.SelectionChanged += dataGridViewProduct_SelectionChanged;
            LoadDataIntoGridView();
        }
        private void LoadDataIntoGridView()
        {
            try
            {
                string query = "SELECT * FROM [dbo].[Product_Shoes]";

                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridViewProduct.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void dataGridViewProduct_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewProduct.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridViewProduct.SelectedRows[0];
                UpdateInfoPanel(selectedRow);
            }
        }
        private void UpdateInfoPanel(DataGridViewRow row)
        {
            try
            {
                txtProductID.Text = row.Cells["ProductID"].Value.ToString();
                txtProductName.Text = row.Cells["ProductName"].Value.ToString();
                txtProductCode.Text = row.Cells["ProductCode"].Value.ToString();
                txtPrice.Text = row.Cells["ProductPrice"].Value.ToString();
                txtQuantity.Text = row.Cells["InventoryQuantity"].Value.ToString();
                txtCategoryID.Text = row.Cells["CategoryID"].Value.ToString();
                txtImage.Text = row.Cells["ProductImage"].Value.ToString();
                txtDescription.Text = row.Cells["Description"].Value.ToString();
                txtBrand.Text = row.Cells["BrandID"].Value.ToString();

                string imagePath = row.Cells["ProductImage"].Value.ToString();
                if (System.IO.File.Exists(imagePath))
                {
                    pictureBox1.Image = Image.FromFile(imagePath);
                }
                else
                {
                    pictureBox1.Image = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating info panel: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (connection != null && connection.State != ConnectionState.Closed)
            {
                connection.Close();
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "SELECT * FROM [dbo].[Product_Shoes] WHERE ProductID = @id";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@id", int.Parse(txtSearch.Text));

                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtProductID.Text = reader["ProductID"].ToString();
                            txtProductName.Text = reader["ProductName"].ToString();
                            txtProductCode.Text = reader["ProductCode"].ToString();
                            txtPrice.Text = reader["ProductPrice"].ToString();
                            txtQuantity.Text = reader["InventoryQuantity"].ToString();
                            txtImage.Text = reader["ProductImage"].ToString();
                            txtCategoryID.Text = reader["CategoryID"].ToString();
                            txtDescription.Text = reader["Description"].ToString();
                            txtBrand.Text = reader["BrandID"].ToString();

                            string imgPath = reader["ProductImage"].ToString();
                            if (!string.IsNullOrEmpty(imgPath))
                            {
                                pictureBox1.ImageLocation = imgPath;
                                pictureBox1.Visible = true;
                            }
                            else
                            {
                                pictureBox1.Visible = false;
                            }

                        }
                        else
                        {
                            MessageBox.Show("Product not found!");
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching product: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (ValidateTextBoxes())
            {
                    try
                    {
                        string query = "INSERT INTO [dbo].[Product_Shoes]" +
                            " (ProductName,ProductCode, ProductPrice, InventoryQuantity,CategoryID,Description,ProductImage,BrandID)" +
                            " VALUES (@name,@code, @price, @quantity,@category, @description,@img,@brand)";
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        using (SqlCommand cmd = new SqlCommand(query, connection))
                        {

                            cmd.Parameters.AddWithValue("@name", txtProductName.Text);
                            cmd.Parameters.AddWithValue("@code", txtProductCode.Text);
                            cmd.Parameters.AddWithValue("@price", decimal.Parse(txtPrice.Text));
                            cmd.Parameters.AddWithValue("@quantity", int.Parse(txtQuantity.Text));
                            cmd.Parameters.AddWithValue("@category", txtCategoryID.Text);
                            cmd.Parameters.AddWithValue("@description", txtDescription.Text);
                            cmd.Parameters.AddWithValue("@img", txtImage.Text);
                            cmd.Parameters.AddWithValue("@brand", txtBrand.Text);

                            connection.Open();
                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show("Product added successfully!", "Added Successfully",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                        LoadDataIntoGridView();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error adding product: " + ex.Message,
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
                        string query = "UPDATE [dbo].[Product_Shoes] " +
                            "SET ProductName = @name,ProductCode = @code," +
                            " ProductPrice = @price, InventoryQuantity = @quantity," +
                            "ProductImage = @img , CategoryID = @category," +
                            " Description = @description,BrandID = @brand  " +
                            "WHERE ProductID = @id";


                        using (SqlCommand cmd = new SqlCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@id", int.Parse(txtProductID.Text));
                            cmd.Parameters.AddWithValue("@name", txtProductName.Text);
                            cmd.Parameters.AddWithValue("@code", txtProductCode.Text);
                            cmd.Parameters.AddWithValue("@price", decimal.Parse(txtPrice.Text));
                            cmd.Parameters.AddWithValue("@quantity", int.Parse(txtQuantity.Text));
                            cmd.Parameters.AddWithValue("@category", txtCategoryID.Text);
                            cmd.Parameters.AddWithValue("@description", txtDescription.Text);
                            cmd.Parameters.AddWithValue("@img", txtImage.Text);
                            cmd.Parameters.AddWithValue("@brand", txtBrand.Text);

                            connection.Open();


                            int rowsAffected = cmd.ExecuteNonQuery();
                            connection.Close();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Product updated successfully!", "Updated Success",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

                                LoadDataIntoGridView();

                            }
                            else
                            {
                                MessageBox.Show("No product found with the given ID.", "No product found",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Question);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error updating product: " + ex.Message,
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
            }
            
           
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Are you sure you want to delete this product?",
                    "Confirm Deletion",
                     MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    string query = "DELETE FROM [dbo].[Product_Shoes] WHERE ProductID = @id";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", int.Parse(txtProductID.Text));

                        connection.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        connection.Close();

                        if (rowsAffected > 0)
                        {
                            if (result == DialogResult.Yes)
                            {
                                MessageBox.Show("Product deleted successfully!",
                                    "Deletion Confirmed",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                            }
                                    LoadDataIntoGridView();
                                    txtProductID.Clear();
                                    txtProductCode.Clear();
                                    txtProductName.Clear();
                                    txtPrice.Clear();
                                    txtQuantity.Clear();
                                    txtCategoryID.Clear();
                                    txtDescription.Clear();
                                    txtImage.Clear();
                                    txtBrand.Clear();
                                    pictureBox1.Image = null;
                        }
                        else
                        {
                            MessageBox.Show("No product found with the given ID.",
                        "Deletion Failed",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                        }
                    }
                }  
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting product: " + ex.Message,
            "Error",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error);
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
        public bool ValidateTextBoxes()
        {
            TextBox[] textBoxesToValidate = new TextBox[]
            {

                txtProductCode,
                txtProductName,
                txtPrice,
                txtQuantity,
                txtCategoryID,
                txtDescription,
                txtImage,
                txtBrand,

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
