namespace BB.TMS.UI
{
    partial class FrmDocNoRule
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDocNoRule));
            this.winGridViewPager1 = new BB.BaseUI.Pager.WinGridViewPager();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.menuTree = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuTree_ExpandAll = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTree_Clapase = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTree_Refresh = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.txtDocCode = new DevExpress.XtraEditors.TextEdit();
            this.txtResetZero = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtFlagSpilitNo = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtFlagIncludeDocCode = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtFlagLastMillisecond = new DevExpress.XtraEditors.ComboBoxEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutDocCode = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutResetZero = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutFlagSpilitNo = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutFlagIncludeDocCode = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutFlagLastMillisecond = new DevExpress.XtraLayout.LayoutControlItem();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.standaloneBarDockControl1 = new DevExpress.XtraBars.StandaloneBarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.menuTree.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDocCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtResetZero.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFlagSpilitNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFlagIncludeDocCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFlagLastMillisecond.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutDocCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutResetZero)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutFlagSpilitNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutFlagIncludeDocCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutFlagLastMillisecond)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            //
            // winGridViewPager1
            //
            this.winGridViewPager1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.winGridViewPager1.AppendedMenu = null;
            this.winGridViewPager1.DataSource = null;
            this.winGridViewPager1.DisplayColumns = "";
            this.winGridViewPager1.FixedColumns = null;
            this.winGridViewPager1.IsExportAllPage = false;
            this.winGridViewPager1.Location = new System.Drawing.Point(0, 0);
            this.winGridViewPager1.MinimumSize = new System.Drawing.Size(540, 0);
            this.winGridViewPager1.Name = "winGridViewPager1";
            this.winGridViewPager1.PrintTitle = "";
            this.winGridViewPager1.ShowAddMenu = true;
            this.winGridViewPager1.ShowCheckBox = false;
            this.winGridViewPager1.ShowDeleteMenu = true;
            this.winGridViewPager1.ShowEditMenu = true;
            this.winGridViewPager1.ShowExportButton = true;
            this.winGridViewPager1.Size = new System.Drawing.Size(775, 596);
            this.winGridViewPager1.TabIndex = 11;
            //
            // splitContainerControl1
            //
            this.splitContainerControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerControl1.Location = new System.Drawing.Point(12, 81);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.groupControl1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.winGridViewPager1);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(980, 596);
            this.splitContainerControl1.SplitterPosition = 200;
            this.splitContainerControl1.TabIndex = 102;
            this.splitContainerControl1.Text = "splitContainerControl1";
            //
            // groupControl1
            //
            this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.AppearanceCaption.Options.UseTextOptions = true;
            this.groupControl1.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.groupControl1.Controls.Add(this.treeView1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(200, 524);
            this.groupControl1.TabIndex = 101;
            this.groupControl1.Text = "单号规则分类快查";
            //
            // treeView1
            //
            this.treeView1.ContextMenuStrip = this.menuTree;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.FullRowSelect = true;
            this.treeView1.HideSelection = false;
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.ItemHeight = 27;
            this.treeView1.Location = new System.Drawing.Point(2, 22);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(196, 524);
            this.treeView1.TabIndex = 97;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            //
            // menuTree
            //
            this.menuTree.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuTree_ExpandAll,
            this.menuTree_Clapase,
            this.menuTree_Refresh});
            this.menuTree.Name = "menuTree";
            this.menuTree.Size = new System.Drawing.Size(141, 70);
            //
            // menuTree_ExpandAll
            //
            this.menuTree_ExpandAll.Image = ((System.Drawing.Image)(resources.GetObject("menuTree_ExpandAll.Image")));
            this.menuTree_ExpandAll.Name = "menuTree_ExpandAll";
            this.menuTree_ExpandAll.Size = new System.Drawing.Size(140, 22);
            this.menuTree_ExpandAll.Text = "全部展开(&E)";
            this.menuTree_ExpandAll.Click += new System.EventHandler(this.menuTree_ExpandAll_Click);
            //
            // menuTree_Clapase
            //
            this.menuTree_Clapase.Image = ((System.Drawing.Image)(resources.GetObject("menuTree_Clapase.Image")));
            this.menuTree_Clapase.Name = "menuTree_Clapase";
            this.menuTree_Clapase.Size = new System.Drawing.Size(140, 22);
            this.menuTree_Clapase.Text = "全部折叠(&C)";
            this.menuTree_Clapase.Click += new System.EventHandler(this.menuTree_Clapase_Click);
            //
            // menuTree_Refresh
            //
            this.menuTree_Refresh.Image = ((System.Drawing.Image)(resources.GetObject("menuTree_Refresh.Image")));
            this.menuTree_Refresh.Name = "menuTree_Refresh";
            this.menuTree_Refresh.Size = new System.Drawing.Size(140, 22);
            this.menuTree_Refresh.Text = "刷新列表(&R)";
            this.menuTree_Refresh.Click += new System.EventHandler(this.menuTree_Refresh_Click);
            //
            // imageList1
            //
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Tip.png");
            this.imageList1.Images.SetKeyName(1, "Organ.ico");
            this.imageList1.Images.SetKeyName(2, "Search2.ICO");
            this.imageList1.Images.SetKeyName(3, "tableInfo.ico");
            this.imageList1.Images.SetKeyName(4, "accept.ico");
            this.imageList1.Images.SetKeyName(5, "ima.ico");
            this.imageList1.Images.SetKeyName(6, "sm.ico");
            this.imageList1.Images.SetKeyName(7, "tra.ico");
            //
            // contextMenuStrip1
            //
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            //
            // layoutControl1
            //
            this.layoutControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.layoutControl1.Controls.Add(this.txtDocCode);
            this.layoutControl1.Controls.Add(this.txtResetZero);
            this.layoutControl1.Controls.Add(this.txtFlagSpilitNo);
            this.layoutControl1.Controls.Add(this.txtFlagIncludeDocCode);
            this.layoutControl1.Controls.Add(this.txtFlagLastMillisecond);
            this.layoutControl1.Location = new System.Drawing.Point(12, 8);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(70, 185, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(980, 44);
            this.layoutControl1.TabIndex = 12;
            this.layoutControl1.Text = "layoutControl1";
            //
            // layoutControlGroup1
            //
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutDocCode,
            this.layoutResetZero,
            this.layoutFlagSpilitNo,
            this.layoutFlagIncludeDocCode,
            this.layoutFlagLastMillisecond,
            });
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(980, 44);
            this.layoutControlGroup1.TextVisible = false;
            //
            // layoutDocCode
            //
            this.layoutDocCode.Control = this.txtDocCode;
            this.layoutDocCode.CustomizationFormText = "单据编码";
            this.layoutDocCode.Location = new System.Drawing.Point(0, 0);
            this.layoutDocCode.Name = "layoutDocCode";
            this.layoutDocCode.Size = new System.Drawing.Size(160, 24);
            this.layoutDocCode.Text = "单据编码";
            this.layoutDocCode.TextSize = new System.Drawing.Size(48, 14);
            //
            // txtDocCode
            //
            this.txtDocCode.Location = new System.Drawing.Point(64, 12);
            this.txtDocCode.Name = "txtDocCode";
            this.txtDocCode.Size = new System.Drawing.Size(105, 20);
            this.txtDocCode.StyleController = this.layoutControl1;
            this.txtDocCode.TabIndex = 0;
            //
            // layoutResetZero
            //
            this.layoutResetZero.Control = this.txtResetZero;
            this.layoutResetZero.CustomizationFormText = "自动归零";
            this.layoutResetZero.Location = new System.Drawing.Point(160, 0);
            this.layoutResetZero.Name = "layoutResetZero";
            this.layoutResetZero.Size = new System.Drawing.Size(160, 24);
            this.layoutResetZero.Text = "自动归零";
            this.layoutResetZero.TextSize = new System.Drawing.Size(48, 14);
            //
            // txtResetZero
            //
            this.txtResetZero.Location = new System.Drawing.Point(224, 12);
            this.txtResetZero.Name = "txtResetZero";
            this.txtResetZero.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtResetZero.Size = new System.Drawing.Size(105, 20);
            this.txtResetZero.StyleController = this.layoutControl1;
            this.txtResetZero.TabIndex = 1;
            //
            // layoutFlagSpilitNo
            //
            this.layoutFlagSpilitNo.Control = this.txtFlagSpilitNo;
            this.layoutFlagSpilitNo.CustomizationFormText = "包含单据字头";
            this.layoutFlagSpilitNo.Location = new System.Drawing.Point(320, 0);
            this.layoutFlagSpilitNo.Name = "layoutFlagSpilitNo";
            this.layoutFlagSpilitNo.Size = new System.Drawing.Size(160, 24);
            this.layoutFlagSpilitNo.Text = "包含单据字头";
            this.layoutFlagSpilitNo.TextSize = new System.Drawing.Size(48, 14);
            //
            // txtFlagSpilitNo
            //
            this.txtFlagSpilitNo.Location = new System.Drawing.Point(384, 12);
            this.txtFlagSpilitNo.Name = "txtFlagSpilitNo";
            this.txtFlagSpilitNo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtFlagSpilitNo.Size = new System.Drawing.Size(105, 20);
            this.txtFlagSpilitNo.StyleController = this.layoutControl1;
            this.txtFlagSpilitNo.TabIndex = 2;
            //
            // layoutFlagIncludeDocCode
            //
            this.layoutFlagIncludeDocCode.Control = this.txtFlagIncludeDocCode;
            this.layoutFlagIncludeDocCode.CustomizationFormText = "序号前加间隔符";
            this.layoutFlagIncludeDocCode.Location = new System.Drawing.Point(480, 0);
            this.layoutFlagIncludeDocCode.Name = "layoutFlagIncludeDocCode";
            this.layoutFlagIncludeDocCode.Size = new System.Drawing.Size(160, 24);
            this.layoutFlagIncludeDocCode.Text = "序号前加间隔符";
            this.layoutFlagIncludeDocCode.TextSize = new System.Drawing.Size(48, 14);
            //
            // txtFlagIncludeDocCode
            //
            this.txtFlagIncludeDocCode.Location = new System.Drawing.Point(544, 12);
            this.txtFlagIncludeDocCode.Name = "txtFlagIncludeDocCode";
            this.txtFlagIncludeDocCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtFlagIncludeDocCode.Size = new System.Drawing.Size(105, 20);
            this.txtFlagIncludeDocCode.StyleController = this.layoutControl1;
            this.txtFlagIncludeDocCode.TabIndex = 3;
            //
            // layoutFlagLastMillisecond
            //
            this.layoutFlagLastMillisecond.Control = this.txtFlagLastMillisecond;
            this.layoutFlagLastMillisecond.CustomizationFormText = "末尾增加毫秒";
            this.layoutFlagLastMillisecond.Location = new System.Drawing.Point(640, 0);
            this.layoutFlagLastMillisecond.Name = "layoutFlagLastMillisecond";
            this.layoutFlagLastMillisecond.Size = new System.Drawing.Size(320, 24);
            this.layoutFlagLastMillisecond.Text = "末尾增加毫秒";
            this.layoutFlagLastMillisecond.TextSize = new System.Drawing.Size(48, 14);
            //
            // txtFlagLastMillisecond
            //
            this.txtFlagLastMillisecond.Location = new System.Drawing.Point(704, 12);
            this.txtFlagLastMillisecond.Name = "txtFlagLastMillisecond";
            this.txtFlagLastMillisecond.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtFlagLastMillisecond.Size = new System.Drawing.Size(105, 20);
            this.txtFlagLastMillisecond.StyleController = this.layoutControl1;
            this.txtFlagLastMillisecond.TabIndex = 4;
            //
            // barManager1
            //
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager1.DockControls.Add(this.standaloneBarDockControl1);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.MaxItemId = 0;
            //
            // bar1
            //
            this.bar1.BarName = "Tools";
            this.bar1.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Standalone;
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Standalone;
            this.bar1.FloatLocation = new System.Drawing.Point(274, 193);
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.OptionsBar.DisableClose = true;
            this.bar1.StandaloneBarDockControl = this.standaloneBarDockControl1;
            this.bar1.Text = "Tools";
            //
            // standaloneBarDockControl1
            //
            this.standaloneBarDockControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.standaloneBarDockControl1.CausesValidation = false;
            this.standaloneBarDockControl1.Location = new System.Drawing.Point(12, 52);
            this.standaloneBarDockControl1.Manager = this.barManager1;
            this.standaloneBarDockControl1.Name = "standaloneBarDockControl1";
            this.standaloneBarDockControl1.Size = new System.Drawing.Size(980, 29);
            this.standaloneBarDockControl1.Text = "standaloneBarDockControl1";
            //
            // barDockControlBottom
            //
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 719);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1020, 0);
            //
            // barDockControlLeft
            //
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 719);
            //
            // barDockControlRight
            //
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1004, 0);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 719);
            //
            // FrmDocNoRule
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1004, 680);
            this.Controls.Add(this.standaloneBarDockControl1);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "FrmDocNoRule";
            this.Text = "单号规则";
            this.Shown += new System.EventHandler(this.FrmDocNoRule_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.menuTree.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtDocCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtResetZero.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFlagSpilitNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFlagIncludeDocCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFlagLastMillisecond.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutDocCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutResetZero)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutFlagSpilitNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutFlagIncludeDocCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutFlagLastMillisecond)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.ContextMenuStrip menuTree;
        private System.Windows.Forms.ToolStripMenuItem menuTree_ExpandAll;
        private System.Windows.Forms.ToolStripMenuItem menuTree_Clapase;
        private System.Windows.Forms.ToolStripMenuItem menuTree_Refresh;
        private System.Windows.Forms.ImageList imageList1;

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.StandaloneBarDockControl standaloneBarDockControl1;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;

        private DevExpress.XtraEditors.TextEdit txtDocCode;
        private DevExpress.XtraLayout.LayoutControlItem layoutDocCode;
        private DevExpress.XtraEditors.ComboBoxEdit txtResetZero;
        private DevExpress.XtraLayout.LayoutControlItem layoutResetZero;
        private DevExpress.XtraEditors.ComboBoxEdit txtFlagSpilitNo;
        private DevExpress.XtraLayout.LayoutControlItem layoutFlagSpilitNo;
        private DevExpress.XtraEditors.ComboBoxEdit txtFlagIncludeDocCode;
        private DevExpress.XtraLayout.LayoutControlItem layoutFlagIncludeDocCode;
        private DevExpress.XtraEditors.ComboBoxEdit txtFlagLastMillisecond;
        private DevExpress.XtraLayout.LayoutControlItem layoutFlagLastMillisecond;
    }
}