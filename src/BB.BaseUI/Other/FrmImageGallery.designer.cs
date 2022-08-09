namespace BB.BaseUI.Other
{
    partial class FrmImageGallery
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmImageGallery));
            this.galleryControl1 = new DevExpress.XtraBars.Ribbon.GalleryControl();
            this.galleryControlClient1 = new DevExpress.XtraBars.Ribbon.GalleryControlClient();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lstCollection = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.lstSize = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.lstCategory = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtFileName = new DevExpress.XtraEditors.ButtonEdit();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtEmbedIcon = new System.Windows.Forms.PictureBox();
            this.txtFilePath = new DevExpress.XtraEditors.ButtonEdit();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.galleryControl1)).BeginInit();
            this.galleryControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lstCollection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstCategory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFileName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmbedIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFilePath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            this.SuspendLayout();
            // 
            // galleryControl1
            // 
            this.galleryControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.galleryControl1.Controls.Add(this.galleryControlClient1);
            this.galleryControl1.DesignGalleryGroupIndex = 0;
            this.galleryControl1.DesignGalleryItemIndex = 0;
            // 
            // 
            // 
            this.galleryControl1.Gallery.ImageSize = new System.Drawing.Size(32, 32);
            this.galleryControl1.Gallery.ItemSize = new System.Drawing.Size(32, 32);
            this.galleryControl1.Location = new System.Drawing.Point(13, 28);
            this.galleryControl1.Name = "galleryControl1";
            this.galleryControl1.Size = new System.Drawing.Size(518, 519);
            this.galleryControl1.TabIndex = 0;
            this.galleryControl1.Text = "galleryControl1";
            // 
            // galleryControlClient1
            // 
            this.galleryControlClient1.GalleryControl = this.galleryControl1;
            this.galleryControlClient1.Location = new System.Drawing.Point(2, 2);
            this.galleryControlClient1.Size = new System.Drawing.Size(497, 515);
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.labelControl3);
            this.splitContainerControl1.Panel1.Controls.Add(this.labelControl2);
            this.splitContainerControl1.Panel1.Controls.Add(this.labelControl1);
            this.splitContainerControl1.Panel1.Controls.Add(this.lstCollection);
            this.splitContainerControl1.Panel1.Controls.Add(this.lstSize);
            this.splitContainerControl1.Panel1.Controls.Add(this.lstCategory);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.galleryControl1);
            this.splitContainerControl1.Panel2.Controls.Add(this.labelControl4);
            this.splitContainerControl1.Panel2.Controls.Add(this.txtFileName);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(794, 547);
            this.splitContainerControl1.SplitterPosition = 258;
            this.splitContainerControl1.TabIndex = 1;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // labelControl3
            // 
            this.labelControl3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelControl3.Location = new System.Drawing.Point(12, 418);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(48, 14);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "图标集合";
            // 
            // labelControl2
            // 
            this.labelControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelControl2.Location = new System.Drawing.Point(12, 321);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(48, 14);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "图标大小";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(13, 8);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 14);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "图标类别";
            // 
            // lstCollection
            // 
            this.lstCollection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstCollection.CheckOnClick = true;
            this.lstCollection.Location = new System.Drawing.Point(12, 438);
            this.lstCollection.Name = "lstCollection";
            this.lstCollection.Size = new System.Drawing.Size(237, 96);
            this.lstCollection.TabIndex = 1;
            // 
            // lstSize
            // 
            this.lstSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstSize.CheckOnClick = true;
            this.lstSize.Location = new System.Drawing.Point(12, 341);
            this.lstSize.Name = "lstSize";
            this.lstSize.Size = new System.Drawing.Size(237, 59);
            this.lstSize.TabIndex = 1;
            // 
            // lstCategory
            // 
            this.lstCategory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstCategory.CheckOnClick = true;
            this.lstCategory.Location = new System.Drawing.Point(12, 28);
            this.lstCategory.Name = "lstCategory";
            this.lstCategory.Size = new System.Drawing.Size(237, 275);
            this.lstCategory.TabIndex = 0;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(14, 8);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(24, 14);
            this.labelControl4.TabIndex = 2;
            this.labelControl4.Text = "名称";
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(44, 5);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Search)});
            this.txtFileName.Properties.Click += new System.EventHandler(this.btnSearch_Click);
            this.txtFileName.Size = new System.Drawing.Size(143, 20);
            this.txtFileName.TabIndex = 3;
            this.txtFileName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtFileName_KeyUp);
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(800, 578);
            this.xtraTabControl1.TabIndex = 2;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.splitContainerControl1);
            this.xtraTabPage1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("xtraTabPage1.ImageOptions.Image")));
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(794, 547);
            this.xtraTabPage1.Text = "DevExpress 图标库选择";
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Appearance.Header.Image = ((System.Drawing.Image)(resources.GetObject("xtraTabPage2.Appearance.Header.Image")));
            this.xtraTabPage2.Appearance.Header.Options.UseImage = true;
            this.xtraTabPage2.Controls.Add(this.labelControl6);
            this.xtraTabPage2.Controls.Add(this.labelControl5);
            this.xtraTabPage2.Controls.Add(this.txtEmbedIcon);
            this.xtraTabPage2.Controls.Add(this.txtFilePath);
            this.xtraTabPage2.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("xtraTabPage2.ImageOptions.Image")));
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(794, 547);
            this.xtraTabPage2.Text = "系统文件选择";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(22, 83);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(48, 14);
            this.labelControl6.TabIndex = 24;
            this.labelControl6.Text = "图标显示";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(22, 26);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(48, 14);
            this.labelControl5.TabIndex = 24;
            this.labelControl5.Text = "文件路径";
            // 
            // txtEmbedIcon
            // 
            this.txtEmbedIcon.Location = new System.Drawing.Point(94, 58);
            this.txtEmbedIcon.MaximumSize = new System.Drawing.Size(64, 64);
            this.txtEmbedIcon.MinimumSize = new System.Drawing.Size(64, 64);
            this.txtEmbedIcon.Name = "txtEmbedIcon";
            this.txtEmbedIcon.Size = new System.Drawing.Size(64, 64);
            this.txtEmbedIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.txtEmbedIcon.TabIndex = 22;
            this.txtEmbedIcon.TabStop = false;
            this.txtEmbedIcon.Click += new System.EventHandler(this.txtEmbedIcon_Click);
            // 
            // txtFilePath
            // 
            this.txtFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilePath.Location = new System.Drawing.Point(94, 23);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtFilePath.Properties.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.txtFilePath_Properties_ButtonClick);
            this.txtFilePath.Size = new System.Drawing.Size(366, 20);
            this.txtFilePath.TabIndex = 23;
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageSize = new System.Drawing.Size(32, 32);
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.InsertGalleryImage("localcolorscheme_32x32.png", "images/appearance/localcolorscheme_32x32.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/appearance/localcolorscheme_32x32.png"), 0);
            this.imageCollection1.Images.SetKeyName(0, "localcolorscheme_32x32.png");
            // 
            // FrmImageGallery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 578);
            this.Controls.Add(this.xtraTabControl1);
            this.Name = "FrmImageGallery";
            this.Text = "图标选择";
            this.Load += new System.EventHandler(this.FrmImageGallery_Load);
            ((System.ComponentModel.ISupportInitialize)(this.galleryControl1)).EndInit();
            this.galleryControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lstCollection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstCategory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFileName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            this.xtraTabPage2.ResumeLayout(false);
            this.xtraTabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmbedIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFilePath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.GalleryControl galleryControl1;
        private DevExpress.XtraBars.Ribbon.GalleryControlClient galleryControlClient1;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.CheckedListBoxControl lstCategory;
        private DevExpress.XtraEditors.CheckedListBoxControl lstCollection;
        private DevExpress.XtraEditors.CheckedListBoxControl lstSize;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.ButtonEdit txtFileName;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private System.Windows.Forms.PictureBox txtEmbedIcon;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.ButtonEdit txtFilePath;
        private DevExpress.Utils.ImageCollection imageCollection1;
    }
}