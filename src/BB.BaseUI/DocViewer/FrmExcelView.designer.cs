namespace BB.BaseUI.DocViewer
{
    partial class FrmExcelView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmExcelView));
            this.spreadsheetControl1 = new DevExpress.XtraSpreadsheet.SpreadsheetControl();
            this.btnPreview = new DevExpress.XtraEditors.SimpleButton();
            this.btnSaveAs = new DevExpress.XtraEditors.SimpleButton();
            this.btnOpenFile = new DevExpress.XtraEditors.SimpleButton();
            this.btnSaveFile = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // spreadsheetControl1
            // 
            this.spreadsheetControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.spreadsheetControl1.Location = new System.Drawing.Point(3, 3);
            this.spreadsheetControl1.Name = "spreadsheetControl1";
            this.spreadsheetControl1.Options.Import.Csv.Encoding = ((System.Text.Encoding)(resources.GetObject("spreadsheetControl1.Options.Import.Csv.Encoding")));
            this.spreadsheetControl1.Options.Import.Txt.Encoding = ((System.Text.Encoding)(resources.GetObject("spreadsheetControl1.Options.Import.Txt.Encoding")));
            this.spreadsheetControl1.ReadOnly = true;
            this.spreadsheetControl1.Size = new System.Drawing.Size(810, 482);
            this.spreadsheetControl1.TabIndex = 0;
            this.spreadsheetControl1.Text = "spreadsheetControl1";
            // 
            // btnPreview
            // 
            this.btnPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPreview.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnPreview.Image")));
            this.btnPreview.Location = new System.Drawing.Point(617, 505);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(101, 32);
            this.btnPreview.TabIndex = 4;
            this.btnPreview.Text = "打印预览";
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // btnSaveAs
            // 
            this.btnSaveAs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveAs.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveAs.Image")));
            this.btnSaveAs.Location = new System.Drawing.Point(510, 505);
            this.btnSaveAs.Name = "btnSaveAs";
            this.btnSaveAs.Size = new System.Drawing.Size(101, 32);
            this.btnSaveAs.TabIndex = 3;
            this.btnSaveAs.Text = "另存为";
            this.btnSaveAs.Click += new System.EventHandler(this.btnSaveAs_Click);
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOpenFile.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenFile.Image")));
            this.btnOpenFile.Location = new System.Drawing.Point(12, 505);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(93, 32);
            this.btnOpenFile.TabIndex = 2;
            this.btnOpenFile.Text = "打开文件";
            this.btnOpenFile.Visible = false;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // btnSaveFile
            // 
            this.btnSaveFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSaveFile.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveFile.Image")));
            this.btnSaveFile.Location = new System.Drawing.Point(119, 505);
            this.btnSaveFile.Name = "btnSaveFile";
            this.btnSaveFile.Size = new System.Drawing.Size(96, 32);
            this.btnSaveFile.TabIndex = 3;
            this.btnSaveFile.Text = "保存文件";
            this.btnSaveFile.Visible = false;
            this.btnSaveFile.Click += new System.EventHandler(this.btnSaveFile_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(731, 505);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(82, 32);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "关闭";
            // 
            // FrmExcelView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(816, 546);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnPreview);
            this.Controls.Add(this.btnSaveFile);
            this.Controls.Add(this.btnSaveAs);
            this.Controls.Add(this.btnOpenFile);
            this.Controls.Add(this.spreadsheetControl1);
            this.KeyPreview = true;
            this.Name = "FrmExcelView";
            this.Text = "Excel文档查看";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FrmExcelView_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FrmExcelView_KeyUp);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnPreview;
        private DevExpress.XtraEditors.SimpleButton btnSaveAs;
        private DevExpress.XtraEditors.SimpleButton btnOpenFile;
        private DevExpress.XtraEditors.SimpleButton btnSaveFile;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        public DevExpress.XtraSpreadsheet.SpreadsheetControl spreadsheetControl1;
    }
}