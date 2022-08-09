namespace BB.BaseUI.Other
{
    partial class FrmException
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmException));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtBugInfo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkSendBug = new System.Windows.Forms.CheckBox();
            this.chkReboot = new System.Windows.Forms.CheckBox();
            this.chkCanHelp = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtContentInfo = new System.Windows.Forms.TextBox();
            this.btnDetailsInfo = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.lblErrorCode = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("logoPictureBox.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(18, 18);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(121, 120);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // txtBugInfo
            // 
            this.txtBugInfo.BackColor = System.Drawing.Color.White;
            this.txtBugInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBugInfo.Location = new System.Drawing.Point(147, 18);
            this.txtBugInfo.Margin = new System.Windows.Forms.Padding(4);
            this.txtBugInfo.Multiline = true;
            this.txtBugInfo.Name = "txtBugInfo";
            this.txtBugInfo.ReadOnly = true;
            this.txtBugInfo.Size = new System.Drawing.Size(526, 174);
            this.txtBugInfo.TabIndex = 7;
            this.txtBugInfo.Text = "BugInfo";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 224);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 18);
            this.label1.TabIndex = 2;
            this.label1.Text = "错误号：";
            // 
            // chkSendBug
            // 
            this.chkSendBug.AutoSize = true;
            this.chkSendBug.Checked = true;
            this.chkSendBug.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSendBug.Location = new System.Drawing.Point(18, 272);
            this.chkSendBug.Margin = new System.Windows.Forms.Padding(4);
            this.chkSendBug.Name = "chkSendBug";
            this.chkSendBug.Size = new System.Drawing.Size(169, 22);
            this.chkSendBug.TabIndex = 3;
            this.chkSendBug.Text = "发送错误报告(&S)";
            this.chkSendBug.UseVisualStyleBackColor = true;
            // 
            // chkReboot
            // 
            this.chkReboot.AutoSize = true;
            this.chkReboot.Location = new System.Drawing.Point(18, 304);
            this.chkReboot.Margin = new System.Windows.Forms.Padding(4);
            this.chkReboot.Name = "chkReboot";
            this.chkReboot.Size = new System.Drawing.Size(133, 22);
            this.chkReboot.TabIndex = 4;
            this.chkReboot.Text = "重启程序(&R)";
            this.chkReboot.UseVisualStyleBackColor = true;
            // 
            // chkCanHelp
            // 
            this.chkCanHelp.AutoSize = true;
            this.chkCanHelp.Location = new System.Drawing.Point(18, 338);
            this.chkCanHelp.Margin = new System.Windows.Forms.Padding(4);
            this.chkCanHelp.Name = "chkCanHelp";
            this.chkCanHelp.Size = new System.Drawing.Size(241, 22);
            this.chkCanHelp.TabIndex = 5;
            this.chkCanHelp.Text = "我愿意协助解决此问题(&A)";
            this.chkCanHelp.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 396);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(206, 18);
            this.label2.TabIndex = 6;
            this.label2.Text = "这个问题是如何出现的？";
            // 
            // txtContentInfo
            // 
            this.txtContentInfo.Location = new System.Drawing.Point(21, 428);
            this.txtContentInfo.Margin = new System.Windows.Forms.Padding(4);
            this.txtContentInfo.Multiline = true;
            this.txtContentInfo.Name = "txtContentInfo";
            this.txtContentInfo.Size = new System.Drawing.Size(654, 118);
            this.txtContentInfo.TabIndex = 1;
            // 
            // btnDetailsInfo
            // 
            this.btnDetailsInfo.Location = new System.Drawing.Point(21, 574);
            this.btnDetailsInfo.Margin = new System.Windows.Forms.Padding(4);
            this.btnDetailsInfo.Name = "btnDetailsInfo";
            this.btnDetailsInfo.Size = new System.Drawing.Size(202, 34);
            this.btnDetailsInfo.TabIndex = 8;
            this.btnDetailsInfo.Text = ">>错误详细信息";
            this.btnDetailsInfo.UseVisualStyleBackColor = true;
            this.btnDetailsInfo.Click += new System.EventHandler(this.btnDetailsInfo_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(561, 574);
            this.btnOk.Margin = new System.Windows.Forms.Padding(4);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(112, 34);
            this.btnOk.TabIndex = 9;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // lblErrorCode
            // 
            this.lblErrorCode.AutoSize = true;
            this.lblErrorCode.Location = new System.Drawing.Point(106, 224);
            this.lblErrorCode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblErrorCode.Name = "lblErrorCode";
            this.lblErrorCode.Size = new System.Drawing.Size(53, 18);
            this.lblErrorCode.TabIndex = 10;
            this.lblErrorCode.Text = "号...";
            // 
            // FrmException
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(699, 627);
            this.Controls.Add(this.lblErrorCode);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnDetailsInfo);
            this.Controls.Add(this.txtContentInfo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chkCanHelp);
            this.Controls.Add(this.chkReboot);
            this.Controls.Add(this.chkSendBug);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBugInfo);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmException";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "错误报送";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtBugInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkSendBug;
        private System.Windows.Forms.CheckBox chkReboot;
        private System.Windows.Forms.CheckBox chkCanHelp;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtContentInfo;
        private System.Windows.Forms.Button btnDetailsInfo;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label lblErrorCode;
    }
}

