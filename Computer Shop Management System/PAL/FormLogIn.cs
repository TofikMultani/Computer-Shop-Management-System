using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Computer_Shop_Management_System.PAL; // Ensure correct namespace for Computer class



namespace Computer_Shop_Management_System.PAL
{
    public partial class FormLogIn : Form
    {
        public class Computer : ComputerBase
        {

            // Static method to validate username and password
            public static bool IsValidNamePass(string username, string password)
            {
                // Example validation logic (replace this with actual database logic)
                // Right now, it's hard-coded for demonstration
                if (username == "Tofik" && password == "123456789")
                {
                    return true; // Valid credentials
                }
                else
                {
                    return false; // Invalid credentials
                }
            }
        }
        public FormLogIn()
        {
            InitializeComponent();
        }

    

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void PicClose_Click(object sender, EventArgs e)
        {
            Close();
            Application.Exit();
        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            //if (textUsername.Text == "Tofik" && textPassword.Text == "123456789")
            //{
            //    new FormMain().Show();
            //    this.Hide();
            //}
            //else
            //{
            //    MessageBox.Show("Valid Insert Data");
            //    textUsername.Clear();
            //    textPassword.Clear();
            //    textUsername.Focus();

            //}
            if (textUsername.Text.Trim() == String.Empty)
            {
                MessageBox.Show("Please enter username.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Check if the password is empty
            else if (textPassword.Text.Trim() == String.Empty)
            {
                MessageBox.Show("Please enter password.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Validate credentials using the Computer class
            else
            {
                bool isValid = Computer.IsValidNamePass(textUsername.Text.Trim(), textPassword.Text.Trim());

                // If the credentials are valid, open the main form
                if (isValid)
                {
                    FormMain formMain = new FormMain();
                    formMain.name = textUsername.Text;
                    formMain.Show(); // Show the main form
                    this.Hide(); // Hide the login form
                }
                else
                {
                    // If invalid, show error message and clear password field
                    MessageBox.Show("Invalid username or password. Please try again.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textPassword.Clear(); // Clear password field
                    textPassword.Focus();  // Set focus back to password field
                }
            }

        }

        private void textUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void FormLogIn_Load(object sender, EventArgs e)
        {

        }
    }
}
