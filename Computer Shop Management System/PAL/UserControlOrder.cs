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

    public partial class UserControlOrder : UserControl
    {
        string s = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DELL\source\repos\Computer Shop Management System\Computer Shop Management System\PAL\CSMS.mdf;Integrated Security=True";
        SqlConnection con;
        SqlDataAdapter da;
        DataSet ds;
        SqlCommand cmd;
        int id;

        private CrystalDecisions.CrystalReports.Engine.ReportDocument cr = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
        static string Crypath = "";


        private void fillProductComboBox()
        {
           

            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                SqlCommand cmdProduct = new SqlCommand("SELECT Product_Id, Product_Name FROM Product", con);
                SqlDataReader readerProduct = cmdProduct.ExecuteReader();

                DataTable ProductTable = new DataTable();
                ProductTable.Load(readerProduct);

                cmbProduct.DataSource = ProductTable;
                cmbProduct.DisplayMember = "Product_Name";
                cmbProduct.ValueMember = "Product_Id";

                readerProduct.Close();
            }
            finally
            {
                con.Close();
            }
        }

        private void fillRateComboBox()
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                SqlCommand cmdRate = new SqlCommand("SELECT Product_Id, Product_Name, Product_Rate FROM Product", con);
                SqlDataReader readerRate = cmdRate.ExecuteReader();

                DataTable RateTable = new DataTable();
                RateTable.Load(readerRate);

                cmbProduct.DataSource = RateTable;
                cmbProduct.DisplayMember = "Product_Rate";
                cmbProduct.ValueMember = "Product_Name";
                cmbProduct.ValueMember = "Product_Id";

                readerRate.Close();
            }
            finally
            {
                con.Close();
            }
        }


        //  private String id = "";
        public UserControlOrder()
        {
            InitializeComponent();
            con = new SqlConnection(s);
        }
        void connection()
        {
            con = new SqlConnection(s);
            con.Open();
        }

        private void UserControlOrder_Load(object sender, EventArgs e)
        {
            connection();
            fillProductComboBox();
        }
        public void EmpBox()
        {
            dtpDate.Value = DateTime.Now;
            txtCustomerName.Clear();
            mtbCustomerNumber.Clear();
            AddClear();
            dgvOrders.Rows.Clear();
            txtTotalAmount.Text = "0";
            nudPaidAmount.Value = 0;
            txtDueAmount.Text = "0";
            nudDiscount.Value = 0;
            txtGrandTotal.Text = "0";
            cmbStatus.SelectedIndex = 0;
        }

        void fillgrid()
        {
           

            try
            {
                connection();
                // Update the query to select only the required columns
              //  string query = "SELECT Product_Name, Rate, Qty, Total FROM Orders"; // Adjust the column names based on your actual database structure
                da = new SqlDataAdapter();
                ds = new DataSet();
                da.Fill(ds);

                dgvOrders.DataSource = ds.Tables[0]; // Bind the DataGridView to the DataSet
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }


        private void AddClear()
        {
            
            try
            {
                cmbProduct.Items.Clear(); // Clear existing items
                fillProductComboBox(); // Refill the ComboBox with products
                cmbProduct.SelectedIndex = 0; // Reset selection

                txtRate.Clear(); // Clear other fields
                nudQuantity.Value = 0;
                txtTotal.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }
        private void btnAdd_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(btnAdd, "Add");
        }

        int oTotal = 0;
        private void btnAdd_Click(object sender, EventArgs e)
        {

            if (cmbProduct.SelectedValue != null && nudQuantity.Value > 0)
            {
                AddItemToGrid();
            }
            else
            {
                MessageBox.Show("Please select a product and enter a quantity.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

/*
            if (nudQuantity.Value > 0)
            {
                int rate, total;
                Int32.TryParse(txtRate.Text, out rate);
                Int32.TryParse(txtTotal.Text, out total);
                int itemCount = (int)nudQuantity.Value; // Count of items being added

                if (dgvOrders.Rows.Count != 0)
                {
                    bool itemExists = false;
                    foreach (DataGridViewRow rows in dgvOrders.Rows)
                    {
                        if (rows.Cells[0].Value.ToString() == cmbProduct.SelectedItem.ToString())
                        {
                            int quantity = Convert.ToInt32(rows.Cells[2].Value.ToString());
                            int total1 = Convert.ToInt32(rows.Cells[3].Value.ToString());

                            quantity += itemCount; // Add new quantity
                            rows.Cells[2].Value = quantity;
                            rows.Cells[3].Value = total1 + total; // Update total price
                            itemExists = true;
                            break;
                        }
                    }
                    if (!itemExists)
                    {
                        // New item to be added
                        txtTotal.Text = (rate * itemCount).ToString();
                        String[] row =
                        {
                     cmbProduct.SelectedItem.ToString(), txtRate.Text, itemCount.ToString(), txtTotal.Text
                };
                        dgvOrders.Rows.Add(row);
                    }
                }
                else
                {
                    // First item to be added
                    txtTotal.Text = (rate * itemCount).ToString();
                    string[] row =
                    {
                cmbProduct.SelectedItem.ToString(), txtRate.Text, itemCount.ToString(), txtTotal.Text
            };
                    dgvOrders.Rows.Add(row);
                }
            }

            // Calculate total amount and item count
            int oTotal = 0;
            int totalItems = 0; // Variable to keep track of total items

            foreach (DataGridViewRow rows in dgvOrders.Rows)
            {
                oTotal += Convert.ToInt32(rows.Cells[3].Value.ToString());
                totalItems += Convert.ToInt32(rows.Cells[2].Value.ToString()); // Count each item
            }

            txtTotalAmount.Text = oTotal.ToString();
            // You might want to use this variable to save the total item count in your Orders table
            // Save this value somewhere, like totalItems*/
        }

        private void cmbProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                // Safely convert SelectedValue to int
                if (cmbProduct.SelectedValue != null && int.TryParse(cmbProduct.SelectedValue.ToString(), out int productId))
                {
                    SqlCommand cmdRate = new SqlCommand("SELECT Product_Rate FROM Product WHERE Product_Id = @ProductId", con);
                    cmdRate.Parameters.AddWithValue("@ProductId", productId);
                    object rate = cmdRate.ExecuteScalar();

                    if (rate != null)
                        txtRate.Text = rate.ToString(); // Display the rate in the textbox
                }
            }
            finally
            {
                con.Close();
            }
        }

        private void nudQuantity_ValueChanged(object sender, EventArgs e)
        {
            
            int rate;
            if (int.TryParse(txtRate.Text, out rate))
            {
                txtTotal.Text = (rate * (int)nudQuantity.Value).ToString();
            }
        }

        private void dgvOrders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4) // Assuming the 5th column is for removing items
            {
                int rowIndex = e.RowIndex;
                if (rowIndex >= 0 && rowIndex < dgvOrders.Rows.Count)
                {
                    dgvOrders.Rows.RemoveAt(rowIndex);
                    UpdateTotalAmount();
                }
            }

        }
       

        private void AddItemToGrid()
        {
            if (nudQuantity.Value > 0)
            {
                int rate;
                if (!int.TryParse(txtRate.Text, out rate))
                {
                    MessageBox.Show("Rate must be a valid integer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int quantityOrdered = (int)nudQuantity.Value;
                int total = rate * quantityOrdered;

                int productId = (int)cmbProduct.SelectedValue;
                int availableQuantity = GetAvailableQuantity(productId); // Check available stock

                if (quantityOrdered > availableQuantity)
                {
                    MessageBox.Show($"Cannot add more than {availableQuantity} of this product to the order.", "Insufficient Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool itemExists = false;
                foreach (DataGridViewRow row in dgvOrders.Rows)
                {
                    if (row.Cells[0].Value.ToString() == cmbProduct.Text)
                    {
                        int currentQuantity;
                        if (!int.TryParse(row.Cells[2].Value.ToString(), out currentQuantity))
                        {
                            MessageBox.Show("Error reading current quantity.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        int newQuantity = currentQuantity + quantityOrdered;
                        row.Cells[2].Value = newQuantity;
                        row.Cells[3].Value = rate * newQuantity; // Update total
                        itemExists = true;
                        break;
                    }
                }

                if (!itemExists)
                {
                    dgvOrders.Rows.Add(cmbProduct.Text, rate.ToString(), quantityOrdered.ToString(), total.ToString());
                }

                UpdateTotalAmount();
                UpdateInventory(productId, quantityOrdered); // Reduce stock after adding to order
                AddClear();
            }
        }

        private int GetAvailableQuantity(int productId)
        {
            int availableQuantity = 0;

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT Product_Qty FROM Product WHERE Product_Id = @ProductId", con); // Updated column name
                cmd.Parameters.AddWithValue("@ProductId", productId);
                object result = cmd.ExecuteScalar();

                if (result != null)
                {
                    availableQuantity = Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching available quantity: " + ex.Message);
            }
            finally
            {
                con.Close();
            }

            return availableQuantity;
        }

        private void UpdateInventory(int productId, int quantityOrdered)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Product SET Product_Qty = Product_Qty - @QuantityOrdered WHERE Product_Id = @ProductId", con); // Updated column name
                cmd.Parameters.AddWithValue("@QuantityOrdered", quantityOrdered);
                cmd.Parameters.AddWithValue("@ProductId", productId);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating inventory: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        private void UpdateTotalAmount()
        {
            int oTotal = 0;
            foreach (DataGridViewRow row in dgvOrders.Rows)
            {
                if (row.Cells[3].Value != null) // Check if the cell is not null
                {
                    oTotal += Convert.ToInt32(row.Cells[3].Value); // Add all total column values
                }
            }
            txtTotalAmount.Text = oTotal.ToString();
        }
        private void nudPaidAmount_ValueChanged(object sender, EventArgs e)
        {
            txtDueAmount.Text = (Convert.ToInt32(nudPaidAmount.Value) - Convert.ToInt32(txtTotalAmount.Text)).ToString();

        }

        private void nudDiscount_ValueChanged(object sender, EventArgs e)
        {
            txtGrandTotal.Text = (Convert.ToInt32(txtTotalAmount.Text) - Convert.ToInt32(nudDiscount.Value)).ToString();
        }

        private void txtTotalAmount_TextChanged(object sender, EventArgs e)
        {
            nudPaidAmount_ValueChanged(sender, e);
            nudDiscount_ValueChanged(sender, e);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
           
    int totalItems = 0; // Initialize total items count
    foreach (DataGridViewRow rows in dgvOrders.Rows)
    {
        totalItems += Convert.ToInt32(rows.Cells[2].Value.ToString()); // Get total items from the grid
    }

    // Now, save the order with totalItems
    try
    {
        connection();

        cmd = new SqlCommand("INSERT INTO Orders (Orders_Date, Customer_Name, Custmore_Number, Total_Amount, Paid_Amount, Due_Amount, Discount, Grand_Total, Payment_Status) VALUES (@Date, @CustomerName, @CustomerNumber, @TotalAmount, @PaidAmount, @DueAmount, @Discount, @GrandTotal, @PaymentStatus)", con);
        cmd.Parameters.AddWithValue("@Date", dtpDate.Value);
        cmd.Parameters.AddWithValue("@CustomerName", txtCustomerName.Text);
        cmd.Parameters.AddWithValue("@CustomerNumber", mtbCustomerNumber.Text);
        cmd.Parameters.AddWithValue("@TotalAmount", Convert.ToInt32(txtTotalAmount.Text));
        cmd.Parameters.AddWithValue("@PaidAmount", nudPaidAmount.Value);
        cmd.Parameters.AddWithValue("@DueAmount", Convert.ToInt32(txtDueAmount.Text));
        cmd.Parameters.AddWithValue("@Discount", nudDiscount.Value);
        cmd.Parameters.AddWithValue("@GrandTotal", Convert.ToInt32(txtGrandTotal.Text));
        cmd.Parameters.AddWithValue("@PaymentStatus", cmbStatus.SelectedItem.ToString());
       // cmd.Parameters.AddWithValue("@TotalItems", totalItems); // Pass total item count

        cmd.ExecuteNonQuery();

        MessageBox.Show("Order saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

        fillgrid(); // Refresh the grid to show the latest orders
    }

    catch (Exception ex)
    {
        MessageBox.Show("Error saving order: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
    finally
    {
        con.Close(); // Ensure the connection is always closed
    }
        }

        private void btnReceipt_Click(object sender, EventArgs e)
        {
            connection();
            da = new SqlDataAdapter("select * from Orders", con);
            ds = new DataSet();
            da.Fill(ds);
            string xml = @"C:/Users/DELL/source/repos/Computer Shop Management System/Computer Shop Management System/PAL/Orders.xml";
            ds.WriteXmlSchema(xml);

            Orders.Visible = true;

            Crypath = @"C:/Users/DELL/source/repos/Computer Shop Management System/Computer Shop Management System/PAL/Orders.rpt";
            cr.Load(Crypath);
            cr.SetDataSource(ds);
            cr.Database.Tables[0].SetDataSource(ds);
            cr.Refresh();
            Orders.ReportSource = cr;
        }



       

        private void txtRate_TextChanged(object sender, EventArgs e)
        {
            // You might want to update the rate when a product is selected
            if (cmbProduct.SelectedValue != null)
            {
                // Update the rate based on the selected product
                // Assuming you have a method to get the product rate
                UpdateRate(); // You need to implement this method
            }
        }
        private void UpdateRate()
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                SqlCommand cmdRate = new SqlCommand("SELECT Product_Rate FROM Product WHERE Product_Id = @ProductId", con);
                cmdRate.Parameters.AddWithValue("@ProductId", cmbProduct.SelectedValue);
                var rate = cmdRate.ExecuteScalar();

                if (rate != null)
                {
                    txtRate.Text = rate.ToString();
                }
            }
            finally
            {
                con.Close();
            }
        }

        private void Orders_Load(object sender, EventArgs e)
        {

        }

       
    }
} 
