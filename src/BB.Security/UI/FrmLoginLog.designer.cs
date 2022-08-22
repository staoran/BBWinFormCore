namespace BB.Security.UI
{
    partial class FrmLoginLog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLoginLog));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("所有记录", 0, 0);
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("自定义分组1", 1, 1);
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("自定义分组2", 1, 1);
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("自定义分组3", 1, 1);
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("个人分组", 1, 1, new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode3,
            treeNode4});
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.txtSystemType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtMacAddress = new DevExpress.XtraEditors.TextEdit();
            this.txtIPAddress = new DevExpress.XtraEditors.TextEdit();
            this.txtNote = new DevExpress.XtraEditors.TextEdit();
            this.dateTimePicker2 = new DevExpress.XtraEditors.DateEdit();
            this.dateTimePicker1 = new DevExpress.XtraEditors.DateEdit();
            this.txtRealName = new DevExpress.XtraEditors.TextEdit();
            this.txtLoginName = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.btnDeleteMonthLog = new DevExpress.XtraEditors.SimpleButton();
            this.winGridViewPager1 = new BB.BaseUI.Pager.WinGridViewPager();
            this.btnExport = new DevExpress.XtraEditors.SimpleButton();
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
            ((System.ComponentModel.ISupportInitialize)(this.txtSystemType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMacAddress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIPAddress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNote.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTimePicker2.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTimePicker2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTimePicker1.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTimePicker1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRealName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLoginName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.menuTree.SuspendLayout();
            this.SuspendLayout();
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
            this.layoutControl1.Controls.Add(this.txtSystemType);
            this.layoutControl1.Controls.Add(this.txtMacAddress);
            this.layoutControl1.Controls.Add(this.txtIPAddress);
            this.layoutControl1.Controls.Add(this.txtNote);
            this.layoutControl1.Controls.Add(this.dateTimePicker2);
            this.layoutControl1.Controls.Add(this.dateTimePicker1);
            this.layoutControl1.Controls.Add(this.txtRealName);
            this.layoutControl1.Controls.Add(this.txtLoginName);
            this.layoutControl1.Location = new System.Drawing.Point(1, 8);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(767, 70);
            this.layoutControl1.TabIndex = 11;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // txtSystemType
            // 
            this.txtSystemType.Location = new System.Drawing.Point(87, 12);
            this.txtSystemType.Name = "txtSystemType";
            this.txtSystemType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.DropDown)});
            this.txtSystemType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.txtSystemType.Size = new System.Drawing.Size(96, 20);
            this.txtSystemType.StyleController = this.layoutControl1;
            this.txtSystemType.TabIndex = 12;
            // 
            // txtMacAddress
            // 
            this.txtMacAddress.Location = new System.Drawing.Point(262, 37);
            this.txtMacAddress.Name = "txtMacAddress";
            this.txtMacAddress.Size = new System.Drawing.Size(112, 20);
            this.txtMacAddress.StyleController = this.layoutControl1;
            this.txtMacAddress.TabIndex = 11;
            // 
            // txtIPAddress
            // 
            this.txtIPAddress.Location = new System.Drawing.Point(87, 37);
            this.txtIPAddress.Name = "txtIPAddress";
            this.txtIPAddress.Size = new System.Drawing.Size(96, 20);
            this.txtIPAddress.StyleController = this.layoutControl1;
            this.txtIPAddress.TabIndex = 10;
            // 
            // txtNote
            // 
            this.txtNote.Location = new System.Drawing.Point(643, 12);
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(112, 20);
            this.txtNote.StyleController = this.layoutControl1;
            this.txtNote.TabIndex = 9;
            this.txtNote.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SearchControl_KeyUp);
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.EditValue = null;
            this.dateTimePicker2.Location = new System.Drawing.Point(643, 37);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateTimePicker2.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateTimePicker2.Properties.CalendarTimeProperties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.dateTimePicker2.Properties.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.dateTimePicker2.Size = new System.Drawing.Size(112, 20);
            this.dateTimePicker2.StyleController = this.layoutControl1;
            this.dateTimePicker2.TabIndex = 7;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.EditValue = null;
            this.dateTimePicker1.Location = new System.Drawing.Point(453, 37);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateTimePicker1.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateTimePicker1.Properties.CalendarTimeProperties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.dateTimePicker1.Properties.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.dateTimePicker1.Size = new System.Drawing.Size(111, 20);
            this.dateTimePicker1.StyleController = this.layoutControl1;
            this.dateTimePicker1.TabIndex = 6;
            // 
            // txtRealName
            // 
            this.txtRealName.Location = new System.Drawing.Point(453, 12);
            this.txtRealName.Name = "txtRealName";
            this.txtRealName.Size = new System.Drawing.Size(111, 20);
            this.txtRealName.StyleController = this.layoutControl1;
            this.txtRealName.TabIndex = 5;
            this.txtRealName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SearchControl_KeyUp);
            // 
            // txtLoginName
            // 
            this.txtLoginName.Location = new System.Drawing.Point(262, 12);
            this.txtLoginName.Name = "txtLoginName";
            this.txtLoginName.Size = new System.Drawing.Size(112, 20);
            this.txtLoginName.StyleController = this.layoutControl1;
            this.txtLoginName.TabIndex = 4;
            this.txtLoginName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SearchControl_KeyUp);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem9,
            this.layoutControlItem11,
            this.layoutControlItem6,
            this.layoutControlItem10,
            this.layoutControlItem4,
            this.layoutControlItem3});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(767, 70);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.txtLoginName;
            this.layoutControlItem1.CustomizationFormText = "登录名称";
            this.layoutControlItem1.Location = new System.Drawing.Point(175, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(199, 25);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(105, 24);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(191, 25);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "登录名称";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(72, 14);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.txtRealName;
            this.layoutControlItem2.CustomizationFormText = "真实名称";
            this.layoutControlItem2.Location = new System.Drawing.Point(366, 0);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(199, 25);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(105, 24);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(190, 25);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "真实名称";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(72, 14);
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.txtIPAddress;
            this.layoutControlItem9.CustomizationFormText = "IP地址";
            this.layoutControlItem9.Location = new System.Drawing.Point(0, 25);
            this.layoutControlItem9.MaxSize = new System.Drawing.Size(199, 25);
            this.layoutControlItem9.MinSize = new System.Drawing.Size(105, 24);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(175, 25);
            this.layoutControlItem9.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem9.Text = "IP地址";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(72, 14);
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.txtSystemType;
            this.layoutControlItem11.CustomizationFormText = "系统类型编号";
            this.layoutControlItem11.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem11.MaxSize = new System.Drawing.Size(199, 25);
            this.layoutControlItem11.MinSize = new System.Drawing.Size(129, 24);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(175, 25);
            this.layoutControlItem11.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem11.Text = "系统类型编号";
            this.layoutControlItem11.TextSize = new System.Drawing.Size(72, 14);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.txtNote;
            this.layoutControlItem6.CustomizationFormText = "日志信息";
            this.layoutControlItem6.Location = new System.Drawing.Point(556, 0);
            this.layoutControlItem6.MaxSize = new System.Drawing.Size(199, 25);
            this.layoutControlItem6.MinSize = new System.Drawing.Size(105, 24);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(191, 25);
            this.layoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem6.Text = "日志信息";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(72, 14);
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.txtMacAddress;
            this.layoutControlItem10.CustomizationFormText = "Mac地址";
            this.layoutControlItem10.Location = new System.Drawing.Point(175, 25);
            this.layoutControlItem10.MaxSize = new System.Drawing.Size(199, 25);
            this.layoutControlItem10.MinSize = new System.Drawing.Size(105, 24);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(191, 25);
            this.layoutControlItem10.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem10.Text = "Mac地址";
            this.layoutControlItem10.TextSize = new System.Drawing.Size(72, 14);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.dateTimePicker2;
            this.layoutControlItem4.CustomizationFormText = "结束日期";
            this.layoutControlItem4.Location = new System.Drawing.Point(556, 25);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(199, 25);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(105, 24);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(191, 25);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.Text = "结束日期";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(72, 14);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.dateTimePicker1;
            this.layoutControlItem3.CustomizationFormText = "开始日期";
            this.layoutControlItem3.Location = new System.Drawing.Point(366, 25);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(199, 25);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(105, 24);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(190, 25);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.Text = "开始日期";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(72, 14);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Location = new System.Drawing.Point(479, 84);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 22);
            this.btnSearch.TabIndex = 5;
            this.btnSearch.Text = "查询";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnDeleteMonthLog
            // 
            this.btnDeleteMonthLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteMonthLog.Location = new System.Drawing.Point(560, 84);
            this.btnDeleteMonthLog.Name = "btnDeleteMonthLog";
            this.btnDeleteMonthLog.Size = new System.Drawing.Size(120, 22);
            this.btnDeleteMonthLog.TabIndex = 6;
            this.btnDeleteMonthLog.Text = "删除30天前的日志";
            this.btnDeleteMonthLog.Click += new System.EventHandler(this.btnDeleteMonthLog_Click);
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
            this.winGridViewPager1.Location = new System.Drawing.Point(1, 112);
            this.winGridViewPager1.MinimumSize = new System.Drawing.Size(540, 0);
            this.winGridViewPager1.Name = "winGridViewPager1";
            this.winGridViewPager1.PrintTitle = "";
            this.winGridViewPager1.ShowCheckBox = false;
            this.winGridViewPager1.ShowExportButton = true;
            this.winGridViewPager1.Size = new System.Drawing.Size(770, 565);
            this.winGridViewPager1.TabIndex = 10;
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.Location = new System.Drawing.Point(686, 84);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 22);
            this.btnExport.TabIndex = 5;
            this.btnExport.Text = "导出";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.groupControl1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.btnExport);
            this.splitContainerControl1.Panel2.Controls.Add(this.btnDeleteMonthLog);
            this.splitContainerControl1.Panel2.Controls.Add(this.winGridViewPager1);
            this.splitContainerControl1.Panel2.Controls.Add(this.layoutControl1);
            this.splitContainerControl1.Panel2.Controls.Add(this.btnSearch);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1008, 680);
            this.splitContainerControl1.SplitterPosition = 229;
            this.splitContainerControl1.TabIndex = 12;
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
            this.groupControl1.Size = new System.Drawing.Size(229, 680);
            this.groupControl1.TabIndex = 28;
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
            this.treeView1.Size = new System.Drawing.Size(225, 656);
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
            // FrmLoginLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 680);
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "FrmLoginLog";
            this.Text = "用户登录日志";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtSystemType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMacAddress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIPAddress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNote.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTimePicker2.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTimePicker2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTimePicker1.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTimePicker1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRealName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLoginName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.menuTree.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private BB.BaseUI.Pager.WinGridViewPager winGridViewPager1;
        private DevExpress.XtraEditors.SimpleButton btnDeleteMonthLog;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.DateEdit dateTimePicker2;
        private DevExpress.XtraEditors.DateEdit dateTimePicker1;
        private DevExpress.XtraEditors.TextEdit txtRealName;
        private DevExpress.XtraEditors.TextEdit txtLoginName;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.TextEdit txtNote;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraEditors.TextEdit txtMacAddress;
        private DevExpress.XtraEditors.TextEdit txtIPAddress;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraEditors.ComboBoxEdit txtSystemType;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private DevExpress.XtraEditors.SimpleButton btnExport;
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