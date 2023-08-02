using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using dotNetDMS.Class;
    
namespace dotNetDMS
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dND_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/mESmaC/dotNetDMS");
        }

        private void login_Click(object sender, EventArgs e)
        {
            try
            {
                string jsonFilePath = "Data/accounts.json";
                string jsonData = File.ReadAllText(jsonFilePath);
                var account = JsonConvert.DeserializeObject<Account>(jsonData);

                if (Username.Text == account.Username && Password.Text == account.Password)
                {
                    MessageBox.Show("Login successful!");
                    // Redirect to the next form or do something else
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Incorrect username or password");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
