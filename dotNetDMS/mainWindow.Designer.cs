namespace dotNetDMS
{
    partial class mainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainWindow));
            this.previewList = new System.Windows.Forms.ImageList(this.components);
            this.docuView = new System.Windows.Forms.ListView();
            this.menuBar = new System.Windows.Forms.MenuStrip();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusOut = new System.Windows.Forms.ToolStripStatusLabel();
            this.previewControl = new System.Windows.Forms.WebBrowser();
            this.menuBar.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // previewList
            // 
            this.previewList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.previewList.ImageSize = new System.Drawing.Size(128, 128);
            this.previewList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // docuView
            // 
            this.docuView.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.docuView.HideSelection = false;
            this.docuView.LargeImageList = this.previewList;
            this.docuView.Location = new System.Drawing.Point(12, 36);
            this.docuView.Name = "docuView";
            this.docuView.Size = new System.Drawing.Size(813, 904);
            this.docuView.TabIndex = 0;
            this.docuView.UseCompatibleStateImageBehavior = false;
            this.docuView.SelectedIndexChanged += new System.EventHandler(this.docuView_SelectedIndexChanged);
            // 
            // menuBar
            // 
            this.menuBar.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuBar.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testToolStripMenuItem});
            this.menuBar.Location = new System.Drawing.Point(0, 0);
            this.menuBar.Name = "menuBar";
            this.menuBar.Size = new System.Drawing.Size(1628, 33);
            this.menuBar.TabIndex = 1;
            this.menuBar.Text = "menuStrip1";
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(58, 29);
            this.testToolStripMenuItem.Text = "Test";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusOut});
            this.statusStrip1.Location = new System.Drawing.Point(0, 1012);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1628, 32);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusOut
            // 
            this.statusOut.Name = "statusOut";
            this.statusOut.Size = new System.Drawing.Size(179, 25);
            this.statusOut.Text = "toolStripStatusLabel1";
            // 
            // previewControl
            // 
            this.previewControl.Location = new System.Drawing.Point(869, 36);
            this.previewControl.MinimumSize = new System.Drawing.Size(20, 20);
            this.previewControl.Name = "previewControl";
            this.previewControl.Size = new System.Drawing.Size(747, 967);
            this.previewControl.TabIndex = 2;
            // 
            // mainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1628, 1044);
            this.Controls.Add(this.previewControl);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.docuView);
            this.Controls.Add(this.menuBar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuBar;
            this.Name = "mainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "dotNetDMS";
            this.Load += new System.EventHandler(this.mainWindow_Load);
            this.menuBar.ResumeLayout(false);
            this.menuBar.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList previewList;
        private System.Windows.Forms.ListView docuView;
        private System.Windows.Forms.MenuStrip menuBar;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusOut;
        private System.Windows.Forms.WebBrowser previewControl;
    }
}

