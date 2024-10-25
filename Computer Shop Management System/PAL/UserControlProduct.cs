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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace Computer_Shop_Management_System.PAL
{
    public partial class UserControlProduct : UserControl
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataSet ds;

        String s = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DELL\source\repos\Computer Shop Management System\Computer Shop Management System\PAL\CSMS.mdf;Integrated Security=True";

        String i, d;
        int id;

        private CrystalDecisions.CrystalReports.Engine.ReportDocument cr = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
        static string Crypath = "";

        void connection()
        {
            con = new SqlConnection(s);
            con.Open();
        }

        public UserControlProduct()
        {
            InitializeComponent();
        }



        private void picSearch_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(picSearch, "Search");
        }

        // Fill Brand ComboBox
        // Fill Brand ComboBox
        private void fillBrandComboBox()
        {
            /* try
             {
                 if (con.State == ConnectionState.Closed)
                     con.Open();

                 SqlCommand cmdBrand = new SqlCommand("SELECT Brand_Id, Brand_Name FROM Brand", con);
                 SqlDataReader readerBrand = cmdBrand.ExecuteReader();

                 DataTable brandTable = new DataTable();
                 brandTable.Load(readerBrand);

                 cmbBrand.DataSource = brandTable;
                 cmbBrand.DisplayMember = "Brand_Name";
                 cmbBrand.ValueMember = "Brand_Id";

                 readerBrand.Close();
             }
             finally
             {
                 con.Close();
             }*/

            // Ensure the connection is established
            connection(); // Open the connection

            try
            {
                SqlCommand cmd = new SqlCommand("SELECT Brand_Id, Brand_Name, Brand_Status FROM Brand", con);

                SqlDataReader reader = cmd.ExecuteReader();

                Dictionary<int, string> brands = new Dictionary<int, string>();

                while (reader.Read())
                {
                    // Add Id and Name to the dictionary
                    brands.Add(reader.GetInt32(0), reader.GetString(1));
                }

                // Close the reader after reading the data
                reader.Close();

                // Bind the data to the ComboBox
                cmbBrand.DataSource = new BindingSource(brands, null);
                cmbBrand.DisplayMember = "Value"; // What you want to display (Brand Name)
                cmbBrand.ValueMember = "Key"; // The underlying value (Brand ID)
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while filling the Brand ComboBox: " + ex.Message);
            }
            finally
            {
                // Ensure the connection is closed
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        // Fill Category ComboBox
        private void fillCategoryComboBox()
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                SqlCommand cmdCategory = new SqlCommand("SELECT Category_Id, Category_Name FROM Category", con);
                SqlDataReader readerCategory = cmdCategory.ExecuteReader();

                DataTable categoryTable = new DataTable();
                categoryTable.Load(readerCategory);

                cmbCategory.DataSource = categoryTable;
                cmbCategory.DisplayMember = "Category_Name";
                cmbCategory.ValueMember = "Category_Id";

                readerCategory.Close();
            }
            finally
            {
                con.Close();
            }
        }


        private void UserControlProduct_Load(object sender, EventArgs e)
        {

            connection();
            fillgrid();
            fillBrandComboBox();
            fillCategoryComboBox();
            con.Close();
        }




        private void btnBrowser_Click(object sender, EventArgs e)
        {



        }

        private void txtSeachProductName_TextChanged(object sender, EventArgs e)
        {
            connection();
            string query = "SELECT * FROM Product WHERE Product_Name LIKE @Search";
            da = new SqlDataAdapter(query, con);
            da.SelectCommand.Parameters.AddWithValue("@Search", "%" + txtSeachProductName.Text + "%");

            ds = new DataSet();
            da.Fill(ds);
            dgvProduct.DataSource = ds.Tables[0];
            con.Close();
        }

        void fillgrid()
        {
            connection();
            da = new SqlDataAdapter("SELECT * FROM Product", con);
            ds = new DataSet();
            da.Fill(ds);
            dgvProduct.DataSource = ds.Tables[0];
            lblTotal.Text = dgvProduct.Rows.Count.ToString();
            con.Close();
        }

        private void dgvProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {


            if (e.RowIndex < 0) return; // Ignore header row clicks

            // If the clicked column is for the "Update" button
            if (dgvProduct.Columns[e.ColumnIndex].HeaderText == "Update")
            {
                btnAdd.Text = "Update";
                id = Convert.ToInt16(dgvProduct.Rows[e.RowIndex].Cells["Product_Id"].Value); // Get Product ID
                txtProductName.Text = dgvProduct.Rows[e.RowIndex].Cells["Product_Name"].Value.ToString();

                // Convert and set Rate and Quantity values
                nudRate.Value = Convert.ToDecimal(dgvProduct.Rows[e.RowIndex].Cells["Product_Rate"].Value);
                nudQuantity.Value = Convert.ToDecimal(dgvProduct.Rows[e.RowIndex].Cells["Product_Qty"].Value);

                // Safely handle Product Brand
                object brandValue = dgvProduct.Rows[e.RowIndex].Cells["Product_Brand"].Value;
                if (brandValue != null && brandValue != DBNull.Value)
                {
                    if (int.TryParse(brandValue.ToString(), out int brandId))
                    {
                        cmbBrand.SelectedValue = brandId; // Set the selected Brand by ID
                    }
                }

                // Safely handle Product Category
                object categoryValue = dgvProduct.Rows[e.RowIndex].Cells["Product_Category"].Value;
                if (categoryValue != null && categoryValue != DBNull.Value)
                {
                    if (int.TryParse(categoryValue.ToString(), out int categoryId))
                    {
                        cmbCategory.SelectedValue = categoryId; // Set the selected Category by ID
                    }
                }

                cmbStatus.Text = dgvProduct.Rows[e.RowIndex].Cells["Product_Status"].Value.ToString();
            }
            else
            {
                // Handle the Delete case
                connection();
                id = Convert.ToInt16(dgvProduct.Rows[e.RowIndex].Cells["Product_Id"].Value);
                cmd = new SqlCommand("DELETE FROM Product WHERE Product_Id = @ProductId", con);
                cmd.Parameters.AddWithValue("@ProductId", id);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Deleted successfully.");
               // fillgrid(); // Refresh grid after deletion
            }

        }

        private void cmbBrand_SelectedIndexChanged(object sender, EventArgs e)
        {
          /*  if (cmbBrand.SelectedItem != null)
            {
                KeyValuePair<int, string> selectedBrand = (KeyValuePair<int, string>)cmbBrand.SelectedItem;
                MessageBox.Show($"Selected Brand Name: {selectedBrand.Value}"); // Show Brand Name
            }*/
        }

        private void ProductReport_Click(object sender, EventArgs e)
        {
            connection();
            da = new SqlDataAdapter("select * from Product", con);
            ds = new DataSet();
            da.Fill(ds);
            string xml = @"C:/Users/DELL/source/repos/Computer Shop Management System/Computer Shop Management System/PAL/Product.xml";
            ds.WriteXmlSchema(xml);

            Product.Visible = true;

            Crypath = @"C:/Users/DELL/source/repos/Computer Shop Management System/Computer Shop Management System/PAL/Product.rpt";
            cr.Load(Crypath);
            cr.SetDataSource(ds);
            cr.Database.Tables[0].SetDataSource(ds);
            cr.Refresh();
            Product.ReportSource = cr;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {


            try
            {
                connection();

                if (btnAdd.Text == "Add")
                {
                    cmd = new SqlCommand("INSERT INTO Product(Product_Name, Product_Rate, Product_Qty, Product_Brand, Product_Category, Product_Status) VALUES (@ProductName, @ProductRate, @ProductQty, @ProductBrand, @ProductCategory, @ProductStatus)", con);
                }
                else if (btnAdd.Text == "Update")
                {
                    cmd = new SqlCommand("UPDATE Product SET Product_Name=@ProductName, Product_Rate=@ProductRate, Product_Qty=@ProductQty, Product_Brand=@ProductBrand, Product_Category=@ProductCategory, Product_Status=@ProductStatus WHERE Product_Id=@ProductId", con);
                    cmd.Parameters.AddWithValue("@ProductId", id);  // Product_Id for Update
                }

                // Add parameters
                cmd.Parameters.AddWithValue("@ProductName", txtProductName.Text);
                cmd.Parameters.AddWithValue("@ProductRate", nudRate.Value);
                cmd.Parameters.AddWithValue("@ProductQty", (int)nudQuantity.Value); // Ensure quantity is an integer

                // Use IDs for Brand and Category (from combo box)
                cmd.Parameters.AddWithValue("@ProductBrand", (int)cmbBrand.SelectedValue);
                cmd.Parameters.AddWithValue("@ProductCategory", (int)cmbCategory.SelectedValue);
                cmd.Parameters.AddWithValue("@ProductStatus", cmbStatus.Text);

                cmd.ExecuteNonQuery();  // Execute the query
                MessageBox.Show(btnAdd.Text == "Add" ? "Product added successfully!" : "Product updated successfully!");
                fillgrid(); // Refresh the grid after Add/Update
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                con.Close();  // Ensure the connection is closed
            }
        }
    }
}
