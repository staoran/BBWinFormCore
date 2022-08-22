namespace BB.Security.UI
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.txtUserName = new DevExpress.XtraEditors.TextEdit();
            this.txtPassword = new DevExpress.XtraEditors.TextEdit();
            this.btnLogin = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lblTips = new DevExpress.XtraEditors.LabelControl();
            this.btnExit = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(98, 332);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(181)))), ((int)(((byte)(214)))), ((int)(((byte)(233)))));
            this.txtUserName.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.txtUserName.Properties.Appearance.Options.UseBackColor = true;
            this.txtUserName.Properties.Appearance.Options.UseFont = true;
            this.txtUserName.Properties.AutoHeight = false;
            this.txtUserName.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtUserName.Size = new System.Drawing.Size(110, 25);
            this.txtUserName.TabIndex = 0;
            this.txtUserName.ToolTip = "首次登陆的默认账号为:admin";
            this.txtUserName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtUserName_KeyUp);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(303, 332);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(181)))), ((int)(((byte)(214)))), ((int)(((byte)(233)))));
            this.txtPassword.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.txtPassword.Properties.Appearance.Options.UseBackColor = true;
            this.txtPassword.Properties.Appearance.Options.UseFont = true;
            this.txtPassword.Properties.AutoHeight = false;
            this.txtPassword.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtPassword.Properties.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(110, 25);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.ToolTip = "首次登陆的默认密码为空,  以后请更改默认密码！";
            this.txtPassword.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtPassword_KeyUp);
            // 
            // btnLogin
            // 
            this.btnLogin.Appearance.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.btnLogin.Appearance.Image = ((System.Drawing.Image)(resources.GetObject("btnLogin.Appearance.Image")));
            this.btnLogin.Appearance.Options.UseFont = true;
            this.btnLogin.Appearance.Options.UseImage = true;
            this.btnLogin.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.btnLogin.Location = new System.Drawing.Point(437, 331);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(87, 26);
            this.btnLogin.TabIndex = 3;
            this.btnLogin.ToolTip = "登录系统";
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Image = ((System.Drawing.Image)(resources.GetObject("labelControl1.Appearance.Image")));
            this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelControl1.Location = new System.Drawing.Point(569, 1);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(29, 30);
            this.labelControl1.TabIndex = 3;
            this.labelControl1.Visible = false;
            this.labelControl1.Click += new System.EventHandler(this.labelControl1_Click);
            // 
            // lblTips
            // 
            this.lblTips.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTips.Appearance.ForeColor = System.Drawing.Color.Yellow;
            this.lblTips.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Horizontal;
            this.lblTips.Location = new System.Drawing.Point(85, 219);
            this.lblTips.Name = "lblTips";
            this.lblTips.Size = new System.Drawing.Size(240, 21);
            this.lblTips.TabIndex = 5;
            this.lblTips.Text = "                                                ";
            // 
            // btnExit
            // 
            this.btnExit.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(232)))), ((int)(((byte)(245)))));
            this.btnExit.Appearance.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Appearance.Image")));
            this.btnExit.Appearance.Options.UseBackColor = true;
            this.btnExit.Appearance.Options.UseImage = true;
            this.btnExit.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.btnExit.Location = new System.Drawing.Point(548, 332);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(21, 23);
            this.btnExit.TabIndex = 6;
            this.btnExit.ToolTip = "退出系统";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayoutStore = System.Windows.Forms.ImageLayout.Tile;
            this.BackgroundImageStore = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImageStore")));
            this.ClientSize = new System.Drawing.Size(600, 371);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.lblTips);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUserName);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Login";
            this.Text = "企业信息化权限管理系统";
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Login_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.txtUserName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txtUserName;
        private DevExpress.XtraEditors.TextEdit txtPassword;
        private DevExpress.XtraEditors.SimpleButton btnLogin;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl lblTips;
        private DevExpress.XtraEditors.SimpleButton btnExit;

    }
}