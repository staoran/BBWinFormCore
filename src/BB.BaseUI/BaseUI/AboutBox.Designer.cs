namespace BB.BaseUI.BaseUI
{
    partial class AboutBox
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutBox));
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.logoPictureBox = new System.Windows.Forms.PictureBox();
            this.lblProductName = new DevExpress.XtraEditors.LabelControl();
            this.lblVersion = new DevExpress.XtraEditors.LabelControl();
            this.lblCopyright = new DevExpress.XtraEditors.LabelControl();
            this.lblCertificated = new DevExpress.XtraEditors.LabelControl();
            this.lblContact = new DevExpress.XtraEditors.LabelControl();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.okButton = new DevExpress.XtraEditors.SimpleButton();
            this.tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32.08138F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 67.91862F));
            this.tableLayoutPanel.Controls.Add(this.logoPictureBox, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.lblProductName, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.lblVersion, 1, 1);
            this.tableLayoutPanel.Controls.Add(this.lblCopyright, 1, 2);
            this.tableLayoutPanel.Controls.Add(this.lblCertificated, 1, 3);
            this.tableLayoutPanel.Controls.Add(this.lblContact, 1, 4);
            this.tableLayoutPanel.Controls.Add(this.txtDescription, 1, 5);
            this.tableLayoutPanel.Controls.Add(this.okButton, 1, 6);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(10, 9);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 7;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(611, 340);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // logoPictureBox
            // 
            this.logoPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logoPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("logoPictureBox.Image")));
            this.logoPictureBox.Location = new System.Drawing.Point(3, 3);
            this.logoPictureBox.Name = "logoPictureBox";
            this.tableLayoutPanel.SetRowSpan(this.logoPictureBox, 7);
            this.logoPictureBox.Size = new System.Drawing.Size(190, 334);
            this.logoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.logoPictureBox.TabIndex = 12;
            this.logoPictureBox.TabStop = false;
            // 
            // lblProductName
            // 
            this.lblProductName.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblProductName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblProductName.Location = new System.Drawing.Point(203, 0);
            this.lblProductName.Margin = new System.Windows.Forms.Padding(7, 0, 3, 0);
            this.lblProductName.MaximumSize = new System.Drawing.Size(0, 19);
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.Size = new System.Drawing.Size(52, 14);
            this.lblProductName.TabIndex = 19;
            this.lblProductName.Text = "产品名称";
            // 
            // lblVersion
            // 
            this.lblVersion.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblVersion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblVersion.Location = new System.Drawing.Point(203, 28);
            this.lblVersion.Margin = new System.Windows.Forms.Padding(7, 0, 3, 0);
            this.lblVersion.MaximumSize = new System.Drawing.Size(0, 19);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(26, 14);
            this.lblVersion.TabIndex = 0;
            this.lblVersion.Text = "版本";
            // 
            // lblCopyright
            // 
            this.lblCopyright.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblCopyright.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCopyright.Location = new System.Drawing.Point(203, 56);
            this.lblCopyright.Margin = new System.Windows.Forms.Padding(7, 0, 3, 0);
            this.lblCopyright.MaximumSize = new System.Drawing.Size(0, 19);
            this.lblCopyright.Name = "lblCopyright";
            this.lblCopyright.Size = new System.Drawing.Size(26, 14);
            this.lblCopyright.TabIndex = 21;
            this.lblCopyright.Text = "版权";
            // 
            // lblCertificated
            // 
            this.lblCertificated.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblCertificated.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCertificated.Location = new System.Drawing.Point(203, 84);
            this.lblCertificated.Margin = new System.Windows.Forms.Padding(7, 0, 3, 0);
            this.lblCertificated.MaximumSize = new System.Drawing.Size(0, 19);
            this.lblCertificated.Name = "lblCertificated";
            this.lblCertificated.Size = new System.Drawing.Size(52, 14);
            this.lblCertificated.TabIndex = 22;
            this.lblCertificated.Text = "授权使用";
            // 
            // lblContact
            // 
            this.lblContact.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblContact.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.lblContact.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblContact.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblContact.Location = new System.Drawing.Point(199, 115);
            this.lblContact.Name = "lblContact";
            this.lblContact.Size = new System.Drawing.Size(52, 14);
            this.lblContact.TabIndex = 25;
            this.lblContact.Text = "联系方式";
            this.lblContact.ToolTip = "双击可复制联系方式到剪贴板上";
            this.lblContact.Click += new System.EventHandler(this.lblContact_Click);
            // 
            // txtDescription
            // 
            this.txtDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDescription.Location = new System.Drawing.Point(203, 175);
            this.txtDescription.Margin = new System.Windows.Forms.Padding(7, 3, 3, 3);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ReadOnly = true;
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtDescription.Size = new System.Drawing.Size(405, 134);
            this.txtDescription.TabIndex = 23;
            this.txtDescription.TabStop = false;
            this.txtDescription.Text = "说明";
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.okButton.Location = new System.Drawing.Point(521, 315);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(87, 22);
            this.okButton.TabIndex = 24;
            this.okButton.Text = "确定(&O)";
            // 
            // AboutBox
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(631, 358);
            this.Controls.Add(this.tableLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutBox";
            this.Padding = new System.Windows.Forms.Padding(10, 9, 10, 9);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "关于";
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.AboutBox_KeyUp);
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.PictureBox logoPictureBox;
        private DevExpress.XtraEditors.LabelControl lblProductName;
        private DevExpress.XtraEditors.LabelControl lblVersion;
        private DevExpress.XtraEditors.LabelControl lblCopyright;
        private DevExpress.XtraEditors.LabelControl lblCertificated;
        private System.Windows.Forms.TextBox txtDescription;
        private DevExpress.XtraEditors.SimpleButton okButton;
        private DevExpress.XtraEditors.LabelControl lblContact;
    }
}
