using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Computer_Shop_Management_System.PAL
{
    public partial class FormMain : Form
    {
        public string name = "{?}";
        public FormMain()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        

        private void FormMain_Load(object sender, EventArgs e)
        {
            lblUsername.Text = name;
            timerDateAndTime.Start();
            
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are You Want to Log Out ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                timerDateAndTime.Stop();
                Close();
                Application.Exit();
            }
        }

        private void pnlCenter_Paint(object sender, PaintEventArgs e)
        {

        }

        private void timerDateAndTime_Tick(object sender, EventArgs e)
        {
            lblTImeAndDate.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            userControlDashboard1.Visible = true;
            userControlBrand1.Visible = false;
            userControlCategory1.Visible = false;
            userControlProduct1.Visible = false;
            userControlOrder1.Visible = false;
            userControlUser1.Visible = false;
            userControlDashboard1.count();
        }

        private void btnBrand_Click(object sender, EventArgs e)
        {
            userControlDashboard1.Visible = false;
            userControlBrand1.EmpBox();
            userControlBrand1.Visible = true;
            userControlCategory1.Visible = false;
            userControlProduct1.Visible = false;
            userControlOrder1.Visible = false;
            userControlUser1.Visible = false;
        }

        private void BtnCategory_Click(object sender, EventArgs e)
        {
            userControlDashboard1.Visible = false;
            userControlBrand1.Visible = false;
            userControlCategory1.EmpBox();
            userControlCategory1.Visible = true;
            userControlProduct1.Visible = false;
            userControlOrder1.Visible = false;
            userControlUser1.Visible = false;
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            userControlDashboard1.Visible = false;
            userControlBrand1.Visible = false;
            userControlCategory1.Visible = false;
            userControlProduct1.Visible = true;
            userControlOrder1.Visible = false;
            userControlUser1.Visible = false;
        }

        private void btnOrders_Click(object sender, EventArgs e)
        {
            userControlDashboard1.Visible = false;
            userControlBrand1.Visible = false;
            userControlCategory1.Visible = false;
            userControlProduct1.Visible = false;
            userControlOrder1.Visible = true;
            userControlUser1.Visible = false;
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            userControlDashboard1.Visible = false;
            userControlBrand1.Visible = false;
            userControlCategory1.Visible = false;
            userControlProduct1.Visible = false;
            userControlOrder1.Visible = false;
            userControlUser1.Visible = false;

        }

        private void btnUser_Click(object sender, EventArgs e)
        {
            userControlDashboard1.Visible = false;
            userControlBrand1.Visible = false;
            userControlCategory1.Visible = false;
            userControlProduct1.Visible = false;
            userControlOrder1.Visible = false;
            userControlUser1.Visible = true;
        }

        private void userControlBrand1_Load(object sender, EventArgs e)
        {
           
        }

        private void userControlProduct1_Load(object sender, EventArgs e)
        {

        }

        private void userControlUser1_Load(object sender, EventArgs e)
        {

        }
    }
}
