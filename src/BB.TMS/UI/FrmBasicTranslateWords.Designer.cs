namespace BB.TMS.UI
{
    partial class FrmBasicTranslateWords
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBasicTranslateWords));
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
            this.txtWordsInFront = new DevExpress.XtraEditors.TextEdit();
            this.txtWordsBehind = new DevExpress.XtraEditors.TextEdit();
            this.txtTranslateType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtCanSelectYN = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtExampleStr = new DevExpress.XtraEditors.TextEdit();
            this.txtCancelYN = new DevExpress.XtraEditors.ComboBoxEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutWordsInFront = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutWordsBehind = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutTranslateType = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutCanSelectYN = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutExampleStr = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutCancelYN = new DevExpress.XtraLayout.LayoutControlItem();
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
            ((System.ComponentModel.ISupportInitialize)(this.txtWordsInFront.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWordsBehind.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTranslateType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCanSelectYN.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtExampleStr.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCancelYN.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutWordsInFront)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutWordsBehind)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutTranslateType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutCanSelectYN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutExampleStr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutCancelYN)).BeginInit();
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
            this.winGridViewPager1.Size = new System.Drawing.Size(775, 580);
            this.winGridViewPager1.TabIndex = 11;
            //
            // splitContainerControl1
            //
            this.splitContainerControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerControl1.Location = new System.Drawing.Point(12, 97);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.groupControl1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.winGridViewPager1);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(980, 580);
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
            this.groupControl1.Text = "公式定义分类快查";
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
            this.layoutControl1.Controls.Add(this.txtWordsInFront);
            this.layoutControl1.Controls.Add(this.txtWordsBehind);
            this.layoutControl1.Controls.Add(this.txtTranslateType);
            this.layoutControl1.Controls.Add(this.txtCanSelectYN);
            this.layoutControl1.Controls.Add(this.txtExampleStr);
            this.layoutControl1.Controls.Add(this.txtCancelYN);
            this.layoutControl1.Location = new System.Drawing.Point(12, 8);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(70, 185, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(980, 60);
            this.layoutControl1.TabIndex = 12;
            this.layoutControl1.Text = "layoutControl1";
            //
            // layoutControlGroup1
            //
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutWordsInFront,
            this.layoutWordsBehind,
            this.layoutTranslateType,
            this.layoutCanSelectYN,
            this.layoutExampleStr,
            this.layoutCancelYN,
            });
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(980, 60);
            this.layoutControlGroup1.TextVisible = false;
            //
            // layoutWordsInFront
            //
            this.layoutWordsInFront.Control = this.txtWordsInFront;
            this.layoutWordsInFront.CustomizationFormText = "关键字";
            this.layoutWordsInFront.Location = new System.Drawing.Point(0, 0);
            this.layoutWordsInFront.Name = "layoutWordsInFront";
            this.layoutWordsInFront.Size = new System.Drawing.Size(160, 34);
            this.layoutWordsInFront.Text = "关键字";
            this.layoutWordsInFront.TextSize = new System.Drawing.Size(48, 14);
            //
            // txtWordsInFront
            //
            this.txtWordsInFront.Location = new System.Drawing.Point(64, 16);
            this.txtWordsInFront.Name = "txtWordsInFront";
            this.txtWordsInFront.Size = new System.Drawing.Size(105, 28);
            this.txtWordsInFront.StyleController = this.layoutControl1;
            this.txtWordsInFront.TabIndex = 0;
            //
            // layoutWordsBehind
            //
            this.layoutWordsBehind.Control = this.txtWordsBehind;
            this.layoutWordsBehind.CustomizationFormText = "代码";
            this.layoutWordsBehind.Location = new System.Drawing.Point(160, 0);
            this.layoutWordsBehind.Name = "layoutWordsBehind";
            this.layoutWordsBehind.Size = new System.Drawing.Size(160, 34);
            this.layoutWordsBehind.Text = "代码";
            this.layoutWordsBehind.TextSize = new System.Drawing.Size(48, 14);
            //
            // txtWordsBehind
            //
            this.txtWordsBehind.Location = new System.Drawing.Point(224, 16);
            this.txtWordsBehind.Name = "txtWordsBehind";
            this.txtWordsBehind.Size = new System.Drawing.Size(105, 28);
            this.txtWordsBehind.StyleController = this.layoutControl1;
            this.txtWordsBehind.TabIndex = 1;
            //
            // layoutTranslateType
            //
            this.layoutTranslateType.Control = this.txtTranslateType;
            this.layoutTranslateType.CustomizationFormText = "代码类型编号";
            this.layoutTranslateType.Location = new System.Drawing.Point(320, 0);
            this.layoutTranslateType.Name = "layoutTranslateType";
            this.layoutTranslateType.Size = new System.Drawing.Size(160, 34);
            this.layoutTranslateType.Text = "代码类型编号";
            this.layoutTranslateType.TextSize = new System.Drawing.Size(48, 14);
            //
            // txtTranslateType
            //
            this.txtTranslateType.Location = new System.Drawing.Point(384, 16);
            this.txtTranslateType.Name = "txtTranslateType";
            this.txtTranslateType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtTranslateType.Size = new System.Drawing.Size(105, 28);
            this.txtTranslateType.StyleController = this.layoutControl1;
            this.txtTranslateType.TabIndex = 2;
            //
            // layoutCanSelectYN
            //
            this.layoutCanSelectYN.Control = this.txtCanSelectYN;
            this.layoutCanSelectYN.CustomizationFormText = "是否可选";
            this.layoutCanSelectYN.Location = new System.Drawing.Point(480, 0);
            this.layoutCanSelectYN.Name = "layoutCanSelectYN";
            this.layoutCanSelectYN.Size = new System.Drawing.Size(160, 34);
            this.layoutCanSelectYN.Text = "是否可选";
            this.layoutCanSelectYN.TextSize = new System.Drawing.Size(48, 14);
            //
            // txtCanSelectYN
            //
            this.txtCanSelectYN.Location = new System.Drawing.Point(544, 16);
            this.txtCanSelectYN.Name = "txtCanSelectYN";
            this.txtCanSelectYN.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtCanSelectYN.Size = new System.Drawing.Size(105, 28);
            this.txtCanSelectYN.StyleController = this.layoutControl1;
            this.txtCanSelectYN.TabIndex = 3;
            //
            // layoutExampleStr
            //
            this.layoutExampleStr.Control = this.txtExampleStr;
            this.layoutExampleStr.CustomizationFormText = "说明";
            this.layoutExampleStr.Location = new System.Drawing.Point(640, 0);
            this.layoutExampleStr.Name = "layoutExampleStr";
            this.layoutExampleStr.Size = new System.Drawing.Size(160, 34);
            this.layoutExampleStr.Text = "说明";
            this.layoutExampleStr.TextSize = new System.Drawing.Size(48, 14);
            //
            // txtExampleStr
            //
            this.txtExampleStr.Location = new System.Drawing.Point(704, 16);
            this.txtExampleStr.Name = "txtExampleStr";
            this.txtExampleStr.Size = new System.Drawing.Size(105, 28);
            this.txtExampleStr.StyleController = this.layoutControl1;
            this.txtExampleStr.TabIndex = 4;
            //
            // layoutCancelYN
            //
            this.layoutCancelYN.Control = this.txtCancelYN;
            this.layoutCancelYN.CustomizationFormText = "是否禁用";
            this.layoutCancelYN.Location = new System.Drawing.Point(800, 0);
            this.layoutCancelYN.Name = "layoutCancelYN";
            this.layoutCancelYN.Size = new System.Drawing.Size(160, 34);
            this.layoutCancelYN.Text = "是否禁用";
            this.layoutCancelYN.TextSize = new System.Drawing.Size(48, 14);
            //
            // txtCancelYN
            //
            this.txtCancelYN.Location = new System.Drawing.Point(864, 16);
            this.txtCancelYN.Name = "txtCancelYN";
            this.txtCancelYN.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtCancelYN.Size = new System.Drawing.Size(105, 28);
            this.txtCancelYN.StyleController = this.layoutControl1;
            this.txtCancelYN.TabIndex = 5;
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
            this.standaloneBarDockControl1.Location = new System.Drawing.Point(12, 68);
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
            // FrmBasicTranslateWords
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
            this.Name = "FrmBasicTranslateWords";
            this.Text = "公式定义";
            this.Shown += new System.EventHandler(this.FrmBasicTranslateWords_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.menuTree.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtWordsInFront.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWordsBehind.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTranslateType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCanSelectYN.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtExampleStr.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCancelYN.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutWordsInFront)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutWordsBehind)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutTranslateType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutCanSelectYN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutExampleStr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutCancelYN)).EndInit();
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

        private DevExpress.XtraEditors.TextEdit txtWordsInFront;
        private DevExpress.XtraLayout.LayoutControlItem layoutWordsInFront;
        private DevExpress.XtraEditors.TextEdit txtWordsBehind;
        private DevExpress.XtraLayout.LayoutControlItem layoutWordsBehind;
        private DevExpress.XtraEditors.ComboBoxEdit txtTranslateType;
        private DevExpress.XtraLayout.LayoutControlItem layoutTranslateType;
        private DevExpress.XtraEditors.ComboBoxEdit txtCanSelectYN;
        private DevExpress.XtraLayout.LayoutControlItem layoutCanSelectYN;
        private DevExpress.XtraEditors.TextEdit txtExampleStr;
        private DevExpress.XtraLayout.LayoutControlItem layoutExampleStr;
        private DevExpress.XtraEditors.ComboBoxEdit txtCancelYN;
        private DevExpress.XtraLayout.LayoutControlItem layoutCancelYN;
    }
}