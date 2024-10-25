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

namespace Computer_Shop_Management_System.PAL
{
    public partial class UserControlUser : UserControl
    {
        string s = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DELL\source\repos\Computer Shop Management System\Computer Shop Management System\PAL\CSMS.mdf;Integrated Security=True";
        SqlConnection con;
        SqlDataAdapter da;
        DataSet ds;
        SqlCommand cmd;
        int id;

        public UserControlUser()
        {
            InitializeComponent();
        }

        private void EmptyBox()
        {
           /* txtUserName.Clear();
            txtEmail.Clear();
            txtPassword.Clear();*/
        }
        void connection()
        {
            con = new SqlConnection(s);
            con.Open();
        }

        void fillgrid()
        {
            /* connection();
             da = new SqlDataAdapter("select * from Users", con);
             ds = new DataSet();
             da.Fill(ds);
             dgvUser.DataSource = ds.Tables[0];
             lblTotal.Text = dgvUser.Rows.Count.ToString();*/
            connection();
            da = new SqlDataAdapter("select Product_Name ,Product_Rate, Product_Qty from Orders ", con);
            da = new SqlDataAdapter("select * from Orders", con);
            ds = new DataSet();
            da.Fill(ds);
            dgvUser.DataSource = ds.Tables[0];
        }




        private void picSearch_MouseHover(object sender, EventArgs e)
        {
           // toolTip1.SetToolTip(picSearch, "Search");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            /* if (txtUserName.Text.Trim() == string.Empty)
             {
                 MessageBox.Show("Please Enter User Name", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                 return;
             }
             else if (txtEmail.Text.Trim() == string.Empty)
             {
                 MessageBox.Show("Please Enter Email", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                 return;
             }
             else if (txtPassword.Text.Trim() == string.Empty)
             {
                 MessageBox.Show("Please Enter Password", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                 return;
             }
             else
             {

             }
            */
            /*if (btnAdd.Text == "Add")
            {
               *//* connection();
                cmd = new SqlCommand("insert into Users(Users_Name,Users_Email,Users_Password)values('" + txtUserName.Text + "','" + txtEmail.Text + "','" + txtPassword.Text + "')", con);
                cmd.ExecuteNonQuery();*//*
            }
            else
            {
               *//* connection();
                cmd = new SqlCommand("update Users set Users_Name='" + txtUserName.Text + "', Users_Email='" + txtEmail.Text + "', Users_Password='" + txtPassword.Text + "' where Users_Id='" + id + "'", con);
                cmd.ExecuteNonQuery();*//*
            }*/
          //  fillgrid();


        }

        private void txtSeachUserName_TextChanged(object sender, EventArgs e)
        {
           /* connection();
            string query = "SELECT * FROM Orders WHERE Customer_Name LIKE @Search";
            da = new SqlDataAdapter(query, con);
            da.SelectCommand.Parameters.AddWithValue("@Search", "%" + txtSeachUserName.Text + "%");
            ds = new DataSet();
            da.Fill(ds);
            dgvUser.DataSource = ds.Tables[0];
            con.Close();*/
        }

        private void UserControlUser_Load(object sender, EventArgs e)
        {
            connection();
            fillgrid();
        }

        private void dgvUser_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           /* if (dgvUser.Columns[e.ColumnIndex].HeaderText == "Update")
            {
                btnAdd.Text = "Update";
                id = Convert.ToInt16(dgvUser.Rows[e.RowIndex].Cells["Users_Id"].Value);
                txtUserName.Text = (dgvUser.Rows[e.RowIndex].Cells["Users_Name"].Value).ToString();
                txtEmail.Text = (dgvUser.Rows[e.RowIndex].Cells["Users_Email"].Value).ToString();
                txtPassword.Text = (dgvUser.Rows[e.RowIndex].Cells["Users_Password"].Value).ToString();
            }
            else
            {
                connection();
                id = Convert.ToInt16(dgvUser.Rows[e.RowIndex].Cells["Users_Id"].Value);
                cmd = new SqlCommand("delete from Users where Users_Id ='" + id + "'", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Deleted..");
                fillgrid();

            }*/
        }
    }
}
