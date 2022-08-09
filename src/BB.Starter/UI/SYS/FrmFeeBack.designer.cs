namespace BB.Starter.UI.SYS
{
    partial class FrmFeeBack
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFeeBack));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.txtEmail = new DevExpress.XtraEditors.TextEdit();
            this.txtAdvise = new WinHtmlControl.Editor();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.lblContact = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmail.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.layoutControl1.Controls.Add(this.txtEmail);
            this.layoutControl1.Controls.Add(this.txtAdvise);
            this.layoutControl1.Location = new System.Drawing.Point(4, 12);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(690, 318);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(63, 286);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(615, 20);
            this.txtEmail.StyleController = this.layoutControl1;
            this.txtEmail.TabIndex = 5;
            // 
            // txtAdvise
            // 
            this.txtAdvise.BodyHtml = null;
            this.txtAdvise.BodyText = null;
            this.txtAdvise.DocumentText = resources.GetString("txtAdvise.DocumentText");
            this.txtAdvise.EditorBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtAdvise.EditorForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtAdvise.FontSize = WinHtmlControl.FontSize.Three;
            this.txtAdvise.Location = new System.Drawing.Point(63, 12);
            this.txtAdvise.Name = "txtAdvise";
            this.txtAdvise.Size = new System.Drawing.Size(615, 270);
            this.txtAdvise.TabIndex = 4;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(690, 318);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.txtAdvise;
            this.layoutControlItem1.CustomizationFormText = "软件建议";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(670, 274);
            this.layoutControlItem1.Text = "软件建议";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(48, 14);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.txtEmail;
            this.layoutControlItem2.CustomizationFormText = "您的邮箱";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 274);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(670, 24);
            this.layoutControlItem2.Text = "您的邮箱";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(48, 14);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(596, 340);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(98, 30);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "发送";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lblContact
            // 
            this.lblContact.Appearance.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblContact.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.lblContact.Location = new System.Drawing.Point(12, 340);
            this.lblContact.Name = "lblContact";
            this.lblContact.Size = new System.Drawing.Size(354, 20);
            this.lblContact.TabIndex = 2;
            this.lblContact.Text = "您也可以通过发邮件给 bubing@bb.com 与我联系";
            // 
            // FrmFeeBack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 382);
            this.Controls.Add(this.lblContact);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FrmFeeBack";
            this.Text = "意见反馈";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmFeeBack_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtEmail.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private WinHtmlControl.Editor txtAdvise;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.TextEdit txtEmail;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.LabelControl lblContact;
    }
}