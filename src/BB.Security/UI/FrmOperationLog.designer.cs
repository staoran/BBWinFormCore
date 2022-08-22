namespace BB.Security.UI
{
    partial class FrmOperationLog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmOperationLog));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("所有记录", 0, 0);
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("自定义分组1", 1, 1);
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("自定义分组2", 1, 1);
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("自定义分组3", 1, 1);
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("个人分组", 1, 1, new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode3,
            treeNode4});
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.btnExport = new DevExpress.XtraEditors.SimpleButton();
            this.winGridViewPager1 = new BB.BaseUI.Pager.WinGridViewPager();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.txtLoginName = new DevExpress.XtraEditors.TextEdit();
            this.txtTableName = new DevExpress.XtraEditors.TextEdit();
            this.txtCreationDate1 = new DevExpress.XtraEditors.DateEdit();
            this.txtCreationDate2 = new DevExpress.XtraEditors.DateEdit();
            this.txtOperationType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.btnSetTableLog = new DevExpress.XtraEditors.SimpleButton();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.menuTree = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuTree_ExpandAll = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTree_Clapase = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTree_Refresh = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtLoginName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTableName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreationDate1.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreationDate1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreationDate2.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreationDate2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOperationType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.menuTree.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Location = new System.Drawing.Point(528, 63);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(69, 22);
            this.btnSearch.TabIndex = 14;
            this.btnSearch.Text = "查询";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.Location = new System.Drawing.Point(616, 63);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(69, 22);
            this.btnExport.TabIndex = 15;
            this.btnExport.Text = "导出";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // winGridViewPager1
            // 
            this.winGridViewPager1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.winGridViewPager1.AppendedMenu = null;
            this.winGridViewPager1.ColumnNameAlias = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("winGridViewPager1.ColumnNameAlias")));
            this.winGridViewPager1.DataSource = null;
            this.winGridViewPager1.DisplayColumns = "";
            this.winGridViewPager1.Location = new System.Drawing.Point(8, 91);
            this.winGridViewPager1.MinimumSize = new System.Drawing.Size(540, 0);
            this.winGridViewPager1.Name = "winGridViewPager1";
            this.winGridViewPager1.PrintTitle = "";
            this.winGridViewPager1.ShowCheckBox = false;
            this.winGridViewPager1.ShowExportButton = true;
            this.winGridViewPager1.Size = new System.Drawing.Size(784, 586);
            this.winGridViewPager1.TabIndex = 11;
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
            this.layoutControl1.Controls.Add(this.txtLoginName);
            this.layoutControl1.Controls.Add(this.txtTableName);
            this.layoutControl1.Controls.Add(this.txtCreationDate1);
            this.layoutControl1.Controls.Add(this.txtCreationDate2);
            this.layoutControl1.Controls.Add(this.txtOperationType);
            this.layoutControl1.Location = new System.Drawing.Point(8, 8);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(70, 185, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(784, 47);
            this.layoutControl1.TabIndex = 12;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // txtLoginName
            // 
            this.txtLoginName.Location = new System.Drawing.Point(75, 12);
            this.txtLoginName.Name = "txtLoginName";
            this.txtLoginName.Size = new System.Drawing.Size(66, 20);
            this.txtLoginName.StyleController = this.layoutControl1;
            this.txtLoginName.TabIndex = 1;
            // 
            // txtTableName
            // 
            this.txtTableName.Location = new System.Drawing.Point(208, 12);
            this.txtTableName.Name = "txtTableName";
            this.txtTableName.Size = new System.Drawing.Size(91, 20);
            this.txtTableName.StyleController = this.layoutControl1;
            this.txtTableName.TabIndex = 2;
            // 
            // txtCreationDate1
            // 
            this.txtCreationDate1.EditValue = null;
            this.txtCreationDate1.Location = new System.Drawing.Point(533, 12);
            this.txtCreationDate1.Name = "txtCreationDate1";
            this.txtCreationDate1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtCreationDate1.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtCreationDate1.Properties.CalendarTimeProperties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.txtCreationDate1.Properties.CalendarTimeProperties.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.txtCreationDate1.Properties.CalendarTimeProperties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtCreationDate1.Properties.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.txtCreationDate1.Properties.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.txtCreationDate1.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtCreationDate1.Size = new System.Drawing.Size(105, 20);
            this.txtCreationDate1.StyleController = this.layoutControl1;
            this.txtCreationDate1.TabIndex = 4;
            // 
            // txtCreationDate2
            // 
            this.txtCreationDate2.EditValue = null;
            this.txtCreationDate2.Location = new System.Drawing.Point(656, 12);
            this.txtCreationDate2.Name = "txtCreationDate2";
            this.txtCreationDate2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtCreationDate2.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtCreationDate2.Properties.CalendarTimeProperties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.txtCreationDate2.Properties.CalendarTimeProperties.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.txtCreationDate2.Properties.CalendarTimeProperties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtCreationDate2.Properties.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.txtCreationDate2.Properties.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.txtCreationDate2.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtCreationDate2.Size = new System.Drawing.Size(116, 20);
            this.txtCreationDate2.StyleController = this.layoutControl1;
            this.txtCreationDate2.TabIndex = 5;
            // 
            // txtOperationType
            // 
            this.txtOperationType.Location = new System.Drawing.Point(366, 12);
            this.txtOperationType.Name = "txtOperationType";
            this.txtOperationType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtOperationType.Properties.Items.AddRange(new object[] {
            "增加",
            "修改",
            "删除"});
            this.txtOperationType.Size = new System.Drawing.Size(100, 20);
            this.txtOperationType.StyleController = this.layoutControl1;
            this.txtOperationType.TabIndex = 3;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(784, 47);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.txtLoginName;
            this.layoutControlItem1.CustomizationFormText = "登录名";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(217, 24);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(117, 24);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(133, 27);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "登录名";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.txtTableName;
            this.layoutControlItem2.CustomizationFormText = "操作表名称";
            this.layoutControlItem2.Location = new System.Drawing.Point(133, 0);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(217, 24);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(117, 24);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(158, 27);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "操作表名称";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.txtOperationType;
            this.layoutControlItem3.CustomizationFormText = "操作类型";
            this.layoutControlItem3.Location = new System.Drawing.Point(291, 0);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(217, 25);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(117, 25);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(167, 27);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.Text = "操作类型";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.txtCreationDate1;
            this.layoutControlItem4.CustomizationFormText = "创建时间1";
            this.layoutControlItem4.Location = new System.Drawing.Point(458, 0);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(217, 24);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(117, 24);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(172, 27);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.Text = "创建时间";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.txtCreationDate2;
            this.layoutControlItem5.CustomizationFormText = "创建时间2";
            this.layoutControlItem5.Location = new System.Drawing.Point(630, 0);
            this.layoutControlItem5.MaxSize = new System.Drawing.Size(168, 24);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(68, 24);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(134, 27);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.Text = "~";
            this.layoutControlItem5.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(9, 14);
            this.layoutControlItem5.TextToControlDistance = 5;
            // 
            // btnSetTableLog
            // 
            this.btnSetTableLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSetTableLog.Location = new System.Drawing.Point(704, 63);
            this.btnSetTableLog.Name = "btnSetTableLog";
            this.btnSetTableLog.Size = new System.Drawing.Size(88, 22);
            this.btnSetTableLog.TabIndex = 15;
            this.btnSetTableLog.Text = "参数设置";
            this.btnSetTableLog.Click += new System.EventHandler(this.btnSetTableLog_Click);
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.groupControl1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.btnSetTableLog);
            this.splitContainerControl1.Panel2.Controls.Add(this.btnExport);
            this.splitContainerControl1.Panel2.Controls.Add(this.winGridViewPager1);
            this.splitContainerControl1.Panel2.Controls.Add(this.layoutControl1);
            this.splitContainerControl1.Panel2.Controls.Add(this.btnSearch);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1008, 680);
            this.splitContainerControl1.SplitterPosition = 204;
            this.splitContainerControl1.TabIndex = 16;
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
            this.groupControl1.Size = new System.Drawing.Size(204, 680);
            this.groupControl1.TabIndex = 27;
            this.groupControl1.Text = "操作日志分类";
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
            treeNode1.ImageIndex = 0;
            treeNode1.Name = "节点73";
            treeNode1.SelectedImageIndex = 0;
            treeNode1.Text = "所有记录";
            treeNode2.ImageIndex = 1;
            treeNode2.Name = "节点11";
            treeNode2.SelectedImageIndex = 1;
            treeNode2.Text = "自定义分组1";
            treeNode3.ImageIndex = 1;
            treeNode3.Name = "节点12";
            treeNode3.SelectedImageIndex = 1;
            treeNode3.Text = "自定义分组2";
            treeNode4.ImageIndex = 1;
            treeNode4.Name = "节点13";
            treeNode4.SelectedImageIndex = 1;
            treeNode4.Text = "自定义分组3";
            treeNode5.ImageIndex = 1;
            treeNode5.Name = "节点2";
            treeNode5.SelectedImageIndex = 1;
            treeNode5.Text = "个人分组";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode5});
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(200, 656);
            this.treeView1.TabIndex = 0;
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
            // FrmOperationLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 680);
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "FrmOperationLog";
            this.Text = "用户操作日志管理";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtLoginName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTableName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreationDate1.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreationDate1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreationDate2.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreationDate2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOperationType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.menuTree.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.SimpleButton btnExport;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private BB.BaseUI.Pager.WinGridViewPager winGridViewPager1;

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;


        private DevExpress.XtraEditors.TextEdit txtLoginName;

        private DevExpress.XtraEditors.TextEdit txtTableName;     
 
        private DevExpress.XtraEditors.DateEdit txtCreationDate1;  
        private DevExpress.XtraEditors.DateEdit txtCreationDate2;  
 
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;    
         private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;    
         private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;    
         private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;    
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.SimpleButton btnSetTableLog;
        private DevExpress.XtraEditors.ComboBoxEdit txtOperationType;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ContextMenuStrip menuTree;
        private System.Windows.Forms.ToolStripMenuItem menuTree_ExpandAll;
        private System.Windows.Forms.ToolStripMenuItem menuTree_Clapase;
        private System.Windows.Forms.ToolStripMenuItem menuTree_Refresh;
        private System.Windows.Forms.ImageList imageList1;  
 
    }
}