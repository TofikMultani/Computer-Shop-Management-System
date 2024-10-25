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
    public partial class UserControlDashboard : UserControl
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataSet ds;

        String s = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DELL\source\repos\Computer Shop Management System\Computer Shop Management System\PAL\CSMS.mdf;Integrated Security=True";

        String i, d;
        int id;

        void connection()
        {
            con = new SqlConnection(s);
            con.Open();
        }
        public UserControlDashboard()
        {
            InitializeComponent();
        }

        private void UserControlDashboard_Load(object sender, EventArgs e)
        {
            count();
        }

        public void count()
        {
            connection(); // Ensure connection is open

            // Count total products
            SqlCommand cmdCount = new SqlCommand("SELECT COUNT(*) FROM Product", con);
            int productCount = (int)cmdCount.ExecuteScalar();
            lblTotalProduct.Text = productCount.ToString();

            // Count total unpaid orders
            SqlCommand cmdOrders = new SqlCommand("select count(*) from Orders where Payment_Status = 'Not Paid';", con);
            int totalOrders = (int)cmdOrders.ExecuteScalar();
            lblTotalOrders.Text = totalOrders.ToString();

            // Count products with low stock
            SqlCommand cmdLowStock = new SqlCommand("select count(*) from Product where Product_Status = 'Not Available';", con);
            int lowStockCount = (int)cmdLowStock.ExecuteScalar();
            lblLowStock.Text = lowStockCount.ToString();

            // Calculate total revenue
            SqlCommand cmdRevenue = new SqlCommand("select SUM(Grand_Total) From Orders", con);
            object totalRevenue = cmdRevenue.ExecuteScalar();
            lblTotalRevenue.Text = (totalRevenue != DBNull.Value) ? totalRevenue.ToString() : "0";

            con.Close(); // Close connection after use
        }

    }
}
