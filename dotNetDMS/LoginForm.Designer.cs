namespace dotNetDMS
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.Username = new System.Windows.Forms.TextBox();
            this.Password = new System.Windows.Forms.TextBox();
            this.user = new System.Windows.Forms.Label();
            this.pass = new System.Windows.Forms.Label();
            this.login = new System.Windows.Forms.Button();
            this.loginBox = new System.Windows.Forms.GroupBox();
            this.dND = new System.Windows.Forms.PictureBox();
            this.loginBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dND)).BeginInit();
            this.SuspendLayout();
            // 
            // Username
            // 
            this.Username.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Username.Location = new System.Drawing.Point(16, 39);
            this.Username.Name = "Username";
            this.Username.Size = new System.Drawing.Size(229, 26);
            this.Username.TabIndex = 0;
            this.Username.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Password
            // 
            this.Password.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Password.Location = new System.Drawing.Point(16, 92);
            this.Password.Name = "Password";
            this.Password.PasswordChar = '*';
            this.Password.Size = new System.Drawing.Size(229, 26);
            this.Password.TabIndex = 1;
            this.Password.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Password.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Password_KeyUp);
            // 
            // user
            // 
            this.user.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.user.AutoSize = true;
            this.user.Location = new System.Drawing.Point(12, 16);
            this.user.Name = "user";
            this.user.Size = new System.Drawing.Size(87, 20);
            this.user.TabIndex = 2;
            this.user.Text = "Username:";
            // 
            // pass
            // 
            this.pass.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pass.AutoSize = true;
            this.pass.Location = new System.Drawing.Point(12, 69);
            this.pass.Name = "pass";
            this.pass.Size = new System.Drawing.Size(82, 20);
            this.pass.TabIndex = 3;
            this.pass.Text = "Password:";
            // 
            // login
            // 
            this.login.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.login.Location = new System.Drawing.Point(16, 134);
            this.login.Name = "login";
            this.login.Size = new System.Drawing.Size(229, 35);
            this.login.TabIndex = 4;
            this.login.Text = "Login";
            this.login.UseVisualStyleBackColor = true;
            this.login.Click += new System.EventHandler(this.login_Click);
            // 
            // loginBox
            // 
            this.loginBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loginBox.Controls.Add(this.login);
            this.loginBox.Controls.Add(this.pass);
            this.loginBox.Controls.Add(this.Username);
            this.loginBox.Controls.Add(this.user);
            this.loginBox.Controls.Add(this.Password);
            this.loginBox.Location = new System.Drawing.Point(83, 95);
            this.loginBox.Name = "loginBox";
            this.loginBox.Size = new System.Drawing.Size(260, 185);
            this.loginBox.TabIndex = 5;
            this.loginBox.TabStop = false;
            this.loginBox.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // dND
            // 
            this.dND.Image = ((System.Drawing.Image)(resources.GetObject("dND.Image")));
            this.dND.Location = new System.Drawing.Point(12, 12);
            this.dND.Name = "dND";
            this.dND.Size = new System.Drawing.Size(400, 77);
            this.dND.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.dND.TabIndex = 6;
            this.dND.TabStop = false;
            this.dND.Click += new System.EventHandler(this.dND_Click);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 310);
            this.Controls.Add(this.dND);
            this.Controls.Add(this.loginBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "dotNetDMS: Login";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.loginBox.ResumeLayout(false);
            this.loginBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dND)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox Username;
        private System.Windows.Forms.TextBox Password;
        private System.Windows.Forms.Label user;
        private System.Windows.Forms.Label pass;
        private System.Windows.Forms.Button login;
        private System.Windows.Forms.GroupBox loginBox;
        private System.Windows.Forms.PictureBox dND;
    }
}