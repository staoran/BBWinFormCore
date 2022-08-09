namespace BB.BaseUI.Device
{
    partial class FrmPosPrintPreview
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPosPrintPreview));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtContent = new DevExpress.XtraEditors.MemoEdit();
            this.btnPrintSetup = new DevExpress.XtraEditors.SimpleButton();
            this.btnPreview = new DevExpress.XtraEditors.SimpleButton();
            this.btnPrint = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnSetPrinterName = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtContent.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtContent);
            this.groupBox1.Location = new System.Drawing.Point(3, 60);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(510, 475);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "打印内容";
            // 
            // txtContent
            // 
            this.txtContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtContent.Location = new System.Drawing.Point(3, 18);
            this.txtContent.Name = "txtContent";
            this.txtContent.Size = new System.Drawing.Size(504, 454);
            this.txtContent.TabIndex = 0;
            // 
            // btnPrintSetup
            // 
            this.btnPrintSetup.Location = new System.Drawing.Point(128, 12);
            this.btnPrintSetup.Name = "btnPrintSetup";
            this.btnPrintSetup.Size = new System.Drawing.Size(95, 32);
            this.btnPrintSetup.TabIndex = 2;
            this.btnPrintSetup.Text = "打印设置";
            this.btnPrintSetup.Click += new System.EventHandler(this.btnPrintSetup_Click);
            // 
            // btnPreview
            // 
            this.btnPreview.Location = new System.Drawing.Point(232, 12);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(95, 32);
            this.btnPreview.TabIndex = 2;
            this.btnPreview.Text = "打印预览";
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.Image")));
            this.btnPrint.Location = new System.Drawing.Point(336, 12);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(87, 32);
            this.btnPrint.TabIndex = 2;
            this.btnPrint.Text = "打印";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(432, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(76, 32);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "关闭";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSetPrinterName
            // 
            this.btnSetPrinterName.Location = new System.Drawing.Point(6, 12);
            this.btnSetPrinterName.Name = "btnSetPrinterName";
            this.btnSetPrinterName.Size = new System.Drawing.Size(113, 32);
            this.btnSetPrinterName.TabIndex = 2;
            this.btnSetPrinterName.Text = "设置打印机名称";
            this.btnSetPrinterName.Click += new System.EventHandler(this.btnSetPrinterName_Click);
            // 
            // FrmPosPrintPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(517, 539);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnPreview);
            this.Controls.Add(this.btnSetPrinterName);
            this.Controls.Add(this.btnPrintSetup);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmPosPrintPreview";
            this.Text = "USB口打印管理界面";
            this.Load += new System.EventHandler(this.FrmPosPrintPreview_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtContent.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.SimpleButton btnPrintSetup;
        private DevExpress.XtraEditors.SimpleButton btnPreview;
        private DevExpress.XtraEditors.SimpleButton btnPrint;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.MemoEdit txtContent;
        private DevExpress.XtraEditors.SimpleButton btnSetPrinterName;
    }
}