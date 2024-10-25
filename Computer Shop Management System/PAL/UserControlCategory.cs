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
    public partial class UserControlCategory : UserControl
    {

        string s = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DELL\source\repos\Computer Shop Management System\Computer Shop Management System\PAL\CSMS.mdf;Integrated Security=True";
        SqlConnection con;
        SqlDataAdapter da;
        DataSet ds;
        SqlCommand cmd;
        int id;

        private CrystalDecisions.CrystalReports.Engine.ReportDocument cr = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
        static string Crypath = "";

        public UserControlCategory()
        {
            InitializeComponent();
        }

        public void EmpBox()
        {
            txtCategoryName.Clear();
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
            da = new SqlDataAdapter("select * from Category", con);
            ds = new DataSet();
            da.Fill(ds);
            dgvCategory.DataSource = ds.Tables[0];
            lblTotal.Text = dgvCategory.Rows.Count.ToString();
        }

        private void picSearch_Click(object sender, EventArgs e)
        {

        }

        private void picSearch_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(picSearch,"Search");
        }

        private void UserControlCategory_Load(object sender, EventArgs e)
        {
            connection();
            fillgrid();
        }

        private void dgvCategory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return; // Ignore header row clicks

            if (dgvCategory.Columns[e.ColumnIndex].HeaderText == "Update")
            {
                id = Convert.ToInt16(dgvCategory.Rows[e.RowIndex].Cells["Category_Id"]?.Value ?? -1);
                txtCategoryName.Text = (dgvCategory.Rows[e.RowIndex].Cells["Category_Name"]?.Value?.ToString() ?? string.Empty);
                cmbStatus.Text = (dgvCategory.Rows[e.RowIndex].Cells["Category_Status"]?.Value?.ToString() ?? string.Empty);
                btnAdd.Text = "Update"; // Change button text to update
            }
            
            else
            {
                connection();
                id = Convert.ToInt16(dgvCategory.Rows[e.RowIndex].Cells["Category_Id"].Value);
                cmd = new SqlCommand("delete from Category where Category_Id ='" + id + "'", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Deleted..");
               // fillgrid();

            }
        }

        private void txtSeachCategoryName_TextChanged(object sender, EventArgs e)
        {
            connection();
            string query = "SELECT * FROM Category WHERE Category_Name LIKE @Search";
            da = new SqlDataAdapter(query, con);
            da.SelectCommand.Parameters.AddWithValue("@Search", "%" + txtSeachCategoryName.Text + "%");
            ds = new DataSet();
            da.Fill(ds);
            dgvCategory.DataSource = ds.Tables[0];
            con.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (btnAdd.Text == "Add")
            {
                connection();
                cmd = new SqlCommand("insert into Category(Category_Name,Category_Status)values('" + txtCategoryName.Text + "','" + cmbStatus.Text + "')", con);
                cmd.ExecuteNonQuery();
            }
            else
            {
                connection();
                cmd = new SqlCommand("update Category set Category_Name='" + txtCategoryName.Text + "', Category_Status='" + cmbStatus.Text + "' where Category_Id='" + id + "'", con);
                cmd.ExecuteNonQuery();
            }
            fillgrid();
        }

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CategoryReport_Click(object sender, EventArgs e)
        {
            connection();
            da = new SqlDataAdapter("select * from Category", con);
            ds = new DataSet();
            da.Fill(ds);
            string xml = @"C:/Users/DELL/source/repos/Computer Shop Management System/Computer Shop Management System/PAL/Category.xml";
            ds.WriteXmlSchema(xml);

            Category.Visible = true;

            Crypath = @"C:/Users/DELL/source/repos/Computer Shop Management System/Computer Shop Management System/PAL/Category.rpt";
            cr.Load(Crypath);
            cr.SetDataSource(ds);
            cr.Database.Tables[0].SetDataSource(ds);
            cr.Refresh();
            Category.ReportSource = cr;
        }
    }
}
