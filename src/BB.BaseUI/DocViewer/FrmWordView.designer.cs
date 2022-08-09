namespace BB.BaseUI.DocViewer
{
    partial class FrmWordView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmWordView));
            this.richEditControl1 = new DevExpress.XtraRichEdit.RichEditControl();
            this.btnPreview = new DevExpress.XtraEditors.SimpleButton();
            this.btnSaveFile = new DevExpress.XtraEditors.SimpleButton();
            this.btnSaveAs = new DevExpress.XtraEditors.SimpleButton();
            this.btnOpenFile = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // richEditControl1
            // 
            this.richEditControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richEditControl1.Location = new System.Drawing.Point(3, 3);
            this.richEditControl1.Name = "richEditControl1";
            this.richEditControl1.ReadOnly = true;
            this.richEditControl1.Size = new System.Drawing.Size(810, 483);
            this.richEditControl1.TabIndex = 8;
            // 
            // btnPreview
            // 
            this.btnPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPreview.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnPreview.Image")));
            this.btnPreview.Location = new System.Drawing.Point(636, 501);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(98, 33);
            this.btnPreview.TabIndex = 12;
            this.btnPreview.Text = "打印预览";
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // btnSaveFile
            // 
            this.btnSaveFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSaveFile.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveFile.Image")));
            this.btnSaveFile.Location = new System.Drawing.Point(115, 501);
            this.btnSaveFile.Name = "btnSaveFile";
            this.btnSaveFile.Size = new System.Drawing.Size(94, 33);
            this.btnSaveFile.TabIndex = 11;
            this.btnSaveFile.Text = "保存文件";
            this.btnSaveFile.Visible = false;
            this.btnSaveFile.Click += new System.EventHandler(this.btnSaveFile_Click);
            // 
            // btnSaveAs
            // 
            this.btnSaveAs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveAs.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveAs.Image")));
            this.btnSaveAs.Location = new System.Drawing.Point(548, 501);
            this.btnSaveAs.Name = "btnSaveAs";
            this.btnSaveAs.Size = new System.Drawing.Size(82, 33);
            this.btnSaveAs.TabIndex = 10;
            this.btnSaveAs.Text = "另存为";
            this.btnSaveAs.Click += new System.EventHandler(this.btnSaveAs_Click);
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOpenFile.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenFile.Image")));
            this.btnOpenFile.Location = new System.Drawing.Point(12, 501);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(89, 33);
            this.btnOpenFile.TabIndex = 9;
            this.btnOpenFile.Text = "打开文件";
            this.btnOpenFile.Visible = false;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(740, 501);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(73, 33);
            this.btnClose.TabIndex = 13;
            this.btnClose.Text = "关闭";
            // 
            // FrmWordView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(816, 546);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnPreview);
            this.Controls.Add(this.btnSaveFile);
            this.Controls.Add(this.btnSaveAs);
            this.Controls.Add(this.btnOpenFile);
            this.Controls.Add(this.richEditControl1);
            this.KeyPreview = true;
            this.Name = "FrmWordView";
            this.Text = "Word文档查看";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FrmWordView_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FrmWordView_KeyUp);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnPreview;
        private DevExpress.XtraEditors.SimpleButton btnSaveFile;
        private DevExpress.XtraEditors.SimpleButton btnSaveAs;
        private DevExpress.XtraEditors.SimpleButton btnOpenFile;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        public DevExpress.XtraRichEdit.RichEditControl richEditControl1;
    }
}