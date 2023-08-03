using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dotNetDMS.Class
{
    public partial class TimedMessageBox : Form
    {
        Timer t;
        public TimedMessageBox()
        {
            InitializeComponent();
        }

        public TimedMessageBox(string title, string message, int timeout)
        {
            InitializeComponent();

            this.Text = title;

            lblMessage = new Label();
            lblMessage.AutoSize = false;

            //Dock the label to fill, this will keep it in the center even if form is resized.
            lblMessage.Dock = DockStyle.Fill;

            lblMessage.Text = message;
            lblMessage.TextAlign = ContentAlignment.MiddleCenter;

            // Timer set up
            t = new Timer();
            t.Interval = timeout; // Set your time in milliseconds
            t.Tick += new EventHandler(t_Tick);
            t.Start();

            this.Controls.Add(lblMessage);
        }

        void t_Tick(object sender, EventArgs e)
        {
            t.Stop();
            this.Close();
        }
        private void TimedMessageBox_Load(object sender, EventArgs e)
        {

        }
    }
}
