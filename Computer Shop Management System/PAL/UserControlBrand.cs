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

    public partial class UserControlBrand : UserControl
    {


        string s = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DELL\source\repos\Computer Shop Management System\Computer Shop Management System\PAL\CSMS.mdf;Integrated Security=True";
       

        SqlConnection con;
        SqlDataAdapter da;
        DataSet ds;
        SqlCommand cmd;
        int id;

        private CrystalDecisions.CrystalReports.Engine.ReportDocument cr = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
        static string Crypath = "";

        public UserControlBrand()
        {
            InitializeComponent();
        }

        public void EmpBox()
        {
            txtBrandName.Clear();
            cmbStatus.SelectedIndex = 0;
        }
        void connection()
        {
            con = new SqlConnection(s);
            con.Open();
        }

        void fillgrid()
        {
            connection();
            da = new SqlDataAdapter("select * from Brand", con);
            ds = new DataSet();
            da.Fill(ds);
            dgvBrand.DataSource = ds.Tables[0];
            lblTotal.Text = dgvBrand.Rows.Count.ToString();
        }

        private void picSearch_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(picSearch, "Search");
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }
       
        private void tpAddBrand_Enter(object sender, EventArgs e)
        {
           
        }

        private void txtSeachBrandName_TextChanged(object sender, EventArgs e)
        {
            connection();
            string query = "SELECT * FROM Brand WHERE Brand_Name LIKE @Search";
            da = new SqlDataAdapter(query, con);
            da.SelectCommand.Parameters.AddWithValue("@Search", "%" + txtSeachBrandName.Text + "%");
            ds = new DataSet();
            da.Fill(ds);
            dgvBrand.DataSource = ds.Tables[0];
            con.Close();
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            //// Check if the row is selected
            //if (Id == "")
            //{
            //    MessageBox.Show("First Select Row From the table.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}

            //// Check if the Brand Name is entered
            //else if (txtBrandName1.Text.Trim() == string.Empty)
            //{
            //    MessageBox.Show("Please Enter Brand Name.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}

            //// Check if the Status is selected
            //else if (cmbStatus1.SelectedIndex == 0)
            //{
            //    MessageBox.Show("Please Select Status.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            //else
            //{
            //    // Create a new Brand object and assign name and status
            //    Brand brand = new Brand
            //    {
            //        Name = txtBrandName1.Text.Trim(),
            //        Status = cmbStatus1.SelectedItem.ToString()
            //    };

            //    // Validate the brand details
            //    bool isValid = Brand.IsValidNamePass(brand.Name, brand.Status);

            //    if (isValid)
            //    {
            //        try
            //        {
            //            // Update the brand in the database
            //            connection(); // Ensure that the connection is open

            //            string query = "UPDATE Brand SET Brand_Name = @Name, Status = @Status WHERE Id = @Id";

            //            using (SqlCommand cmd = new SqlCommand(query, con))
            //            {
            //                cmd.Parameters.AddWithValue("@Name", brand.Name);
            //                cmd.Parameters.AddWithValue("@Status", brand.Status);
            //                cmd.Parameters.AddWithValue("@Id", Id); // Use the selected Id to update the correct row

            //                cmd.ExecuteNonQuery(); // Execute the update query
            //            }

            //            MessageBox.Show("Brand updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            EmptyBox1(); // Clear the input fields after successful update
            //            fillgrid();  // Refresh the grid to display the updated data
            //            tcBrand.SelectedTab = tpAddBrand;
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        }
                   
            //    }
            //    else
            //    {
            //        MessageBox.Show("Brand validation failed. Please check inputs.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            //// Check if a brand is selected
            //if (Id == "")
            //{
            //    MessageBox.Show("Please select a brand to remove.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}

            //// Confirm the brand deletion with the user
            //DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this brand?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //if (dialogResult == DialogResult.Yes)
            //{
            //    try
            //    {
            //        // Establish the connection to the database
            //        connection();

            //        // SQL DELETE query to remove the selected brand
            //        string query = "DELETE FROM Brand WHERE Id = @Id";

            //        using (SqlCommand cmd = new SqlCommand(query, con))
            //        {
            //            // Add the selected brand Id as a parameter
            //            cmd.Parameters.AddWithValue("@Id", Id);

            //            // Execute the DELETE query
            //            cmd.ExecuteNonQuery();
            //        }

            //        MessageBox.Show("Brand deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //        // Clear the form and refresh the grid after deletion
            //        EmptyBox1();
            //        fillgrid();
            //        tcBrand.SelectedTab = tpAddBrand;  // Switch back to the 'Add Brand' tab
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
               
            //}
        }

        private void tpEdit_Enter(object sender, EventArgs e)
        {
            //if(Id=="")
            //    tcBrand.SelectedTab = tpAddBrand;
        }

        private void tpEdit_Leave(object sender, EventArgs e)
        {
          //  EmptyBox1();
        }

        private void UserControlBrand_Load(object sender, EventArgs e)
        {
            connection();
            fillgrid();
        }

        private void txtSeachBrandName_TextChanged_1(object sender, EventArgs e)
        {
            connection();
            string query = "SELECT * FROM Brand WHERE Brand_Name LIKE @Search";
            da = new SqlDataAdapter(query, con);
            da.SelectCommand.Parameters.AddWithValue("@Search", "%" + txtSeachBrandName.Text + "%");

            ds = new DataSet();
            da.Fill(ds);
            dgvBrand.DataSource = ds.Tables[0];
            con.Close();
        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            if (btnAdd.Text == "Add")
            {
                connection();
                cmd = new SqlCommand("insert into Brand(Brand_Name,Brand_Status)values('" + txtBrandName.Text + "','" + cmbStatus.Text + "')", con);
                cmd.ExecuteNonQuery();
            }
            else
            {
                connection();
                cmd = new SqlCommand("update Brand set Brand_Name='" + txtBrandName.Text + "', Brand_Status='" + cmbStatus.Text + "' where Brand_Id='" + id + "'", con);
                cmd.ExecuteNonQuery();
            }
            fillgrid();
        }

        private void dgvBrand_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvBrand_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvBrand.Columns[e.ColumnIndex].HeaderText == "Update")
            {
                btnAdd.Text = "Update";
                id = Convert.ToInt16(dgvBrand.Rows[e.RowIndex].Cells["Brand_Id"].Value);
                txtBrandName.Text = (dgvBrand.Rows[e.RowIndex].Cells["Brand_Name"].Value).ToString();
                cmbStatus.Text = (dgvBrand.Rows[e.RowIndex].Cells["Brand_Status"].Value).ToString();
            }
            else
            {
                connection();
                id = Convert.ToInt16(dgvBrand.Rows[e.RowIndex].Cells["Brand_Id"].Value);
                cmd = new SqlCommand("delete from Brand where Brand_Id ='" + id + "'", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Deleted..");
                //fillgrid();
            }
        }

        private void BrandReport_Click(object sender, EventArgs e)
        {
            connection();
            da = new SqlDataAdapter("select * from Brand", con);
            ds = new DataSet();
            da.Fill(ds);
            string xml = @"C:/Users/DELL/source/repos/Computer Shop Management System/Computer Shop Management System/PAL/Brand.xml";
            ds.WriteXmlSchema(xml);

            Brand.Visible = true;

            Crypath = @"C:/Users/DELL/source/repos/Computer Shop Management System/Computer Shop Management System/PAL/Brand.rpt";
            cr.Load(Crypath);
            cr.SetDataSource(ds);
            cr.Database.Tables[0].SetDataSource(ds);
            cr.Refresh();
            Brand.ReportSource = cr;
        }

        private void UserControlBrand_Load_1(object sender, EventArgs e)
        {

        }
    }
}
