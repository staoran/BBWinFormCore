namespace BB.BaseUI.Device
{
    partial class PortraitControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PortraitControl));
            this.picPortrait = new DevExpress.XtraEditors.PictureEdit();
            this.pnlTool = new DevExpress.XtraEditors.PanelControl();
            this.btnRestore = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnLoad = new DevExpress.XtraEditors.SimpleButton();
            this.btnCapture = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.picPortrait.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTool)).BeginInit();
            this.pnlTool.SuspendLayout();
            this.SuspendLayout();
            // 
            // picPortrait
            // 
            this.picPortrait.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picPortrait.EditValue = ((object)(resources.GetObject("picPortrait.EditValue")));
            this.picPortrait.Location = new System.Drawing.Point(3, 32);
            this.picPortrait.Name = "picPortrait";
            this.picPortrait.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.picPortrait.Size = new System.Drawing.Size(170, 245);
            this.picPortrait.TabIndex = 0;
            this.picPortrait.ToolTip = "双击放大图片进行查看";
            this.picPortrait.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.picPortrait_MouseDoubleClick);
            // 
            // pnlTool
            // 
            this.pnlTool.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlTool.Controls.Add(this.btnCapture);
            this.pnlTool.Controls.Add(this.btnRestore);
            this.pnlTool.Controls.Add(this.btnSave);
            this.pnlTool.Controls.Add(this.btnLoad);
            this.pnlTool.Location = new System.Drawing.Point(3, 3);
            this.pnlTool.Name = "pnlTool";
            this.pnlTool.Size = new System.Drawing.Size(170, 23);
            this.pnlTool.TabIndex = 1;
            // 
            // btnRestore
            // 
            this.btnRestore.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.btnRestore.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnRestore.Image")));
            this.btnRestore.Location = new System.Drawing.Point(75, 0);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(24, 23);
            this.btnRestore.TabIndex = 0;
            this.btnRestore.ToolTip = "使用默认图片";
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
            // 
            // btnSave
            // 
            this.btnSave.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.btnSave.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.Location = new System.Drawing.Point(41, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(24, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.ToolTip = "保存图片";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.btnLoad.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnLoad.Image")));
            this.btnLoad.Location = new System.Drawing.Point(5, 0);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(24, 23);
            this.btnLoad.TabIndex = 0;
            this.btnLoad.ToolTip = "导入图片";
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnCapture
            // 
            this.btnCapture.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.btnCapture.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnCapture.Image")));
            this.btnCapture.Location = new System.Drawing.Point(115, 0);
            this.btnCapture.Name = "btnCapture";
            this.btnCapture.Size = new System.Drawing.Size(24, 23);
            this.btnCapture.TabIndex = 0;
            this.btnCapture.ToolTip = "拍照";
            this.btnCapture.Click += new System.EventHandler(this.btnCapture_Click);
            // 
            // PortraitControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.pnlTool);
            this.Controls.Add(this.picPortrait);
            this.Name = "PortraitControl";
            this.Size = new System.Drawing.Size(176, 278);
            this.Load += new System.EventHandler(this.PortraitControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picPortrait.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTool)).EndInit();
            this.pnlTool.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PictureEdit picPortrait;
        private DevExpress.XtraEditors.PanelControl pnlTool;
        private DevExpress.XtraEditors.SimpleButton btnRestore;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnLoad;
        private DevExpress.XtraEditors.SimpleButton btnCapture;
    }
}
