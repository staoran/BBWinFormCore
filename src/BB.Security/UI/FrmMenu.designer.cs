namespace BB.Security.UI
{
    partial class FrmMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMenu));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("菜单1");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("系统1", new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.btnAddNew = new DevExpress.XtraEditors.SimpleButton();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.btnExport = new DevExpress.XtraEditors.SimpleButton();
            this.winGridViewPager1 = new BB.BaseUI.Pager.WinGridViewPager();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.txtFunctionId = new DevExpress.XtraEditors.TextEdit();
            this.txtWinformType = new DevExpress.XtraEditors.TextEdit();
            this.txtUrl = new DevExpress.XtraEditors.TextEdit();
            this.txtVisible = new DevExpress.XtraEditors.CheckEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.ctxMenuTree = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctxMenuTree_Refresh = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFunctionId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWinformType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUrl.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVisible.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.ctxMenuTree.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAddNew
            // 
            this.btnAddNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddNew.Location = new System.Drawing.Point(851, 59);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(69, 22);
            this.btnAddNew.TabIndex = 15;
            this.btnAddNew.Text = "新建";
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Location = new System.Drawing.Point(776, 59);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(69, 22);
            this.btnSearch.TabIndex = 14;
            this.btnSearch.Text = "查询";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.Location = new System.Drawing.Point(926, 59);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(69, 22);
            this.btnExport.TabIndex = 15;
            this.btnExport.Text = "导出";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // winGridViewPager1
            // 
            this.winGridViewPager1.AppendedMenu = null;
            this.winGridViewPager1.ColumnNameAlias = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("winGridViewPager1.ColumnNameAlias")));
            this.winGridViewPager1.DataSource = null;
            this.winGridViewPager1.DisplayColumns = "";
            this.winGridViewPager1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.winGridViewPager1.Location = new System.Drawing.Point(0, 0);
            this.winGridViewPager1.MinimumSize = new System.Drawing.Size(540, 0);
            this.winGridViewPager1.Name = "winGridViewPager1";
            this.winGridViewPager1.PrintTitle = "";
            this.winGridViewPager1.ShowCheckBox = false;
            this.winGridViewPager1.ShowExportButton = true;
            this.winGridViewPager1.Size = new System.Drawing.Size(756, 588);
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
            this.layoutControl1.Controls.Add(this.txtName);
            this.layoutControl1.Controls.Add(this.txtFunctionId);
            this.layoutControl1.Controls.Add(this.txtWinformType);
            this.layoutControl1.Controls.Add(this.txtUrl);
            this.layoutControl1.Controls.Add(this.txtVisible);
            this.layoutControl1.Location = new System.Drawing.Point(12, 8);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(70, 185, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(980, 45);
            this.layoutControl1.TabIndex = 12;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(109, 12);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(117, 20);
            this.txtName.StyleController = this.layoutControl1;
            this.txtName.TabIndex = 1;
            // 
            // txtFunctionId
            // 
            this.txtFunctionId.Location = new System.Drawing.Point(327, 12);
            this.txtFunctionId.Name = "txtFunctionId";
            this.txtFunctionId.Size = new System.Drawing.Size(117, 20);
            this.txtFunctionId.StyleController = this.layoutControl1;
            this.txtFunctionId.TabIndex = 4;
            // 
            // txtWinformType
            // 
            this.txtWinformType.Location = new System.Drawing.Point(545, 12);
            this.txtWinformType.Name = "txtWinformType";
            this.txtWinformType.Size = new System.Drawing.Size(132, 20);
            this.txtWinformType.StyleController = this.layoutControl1;
            this.txtWinformType.TabIndex = 6;
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(778, 12);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(106, 20);
            this.txtUrl.StyleController = this.layoutControl1;
            this.txtUrl.TabIndex = 7;
            // 
            // txtVisible
            // 
            this.txtVisible.EditValue = true;
            this.txtVisible.Location = new System.Drawing.Point(888, 12);
            this.txtVisible.Name = "txtVisible";
            this.txtVisible.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.txtVisible.Properties.Caption = "菜单可见";
            this.txtVisible.Size = new System.Drawing.Size(80, 19);
            this.txtVisible.StyleController = this.layoutControl1;
            this.txtVisible.TabIndex = 5;
            this.txtVisible.CheckedChanged += new System.EventHandler(this.txtVisible_CheckedChanged);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem4,
            this.layoutControlItem6,
            this.layoutControlItem7,
            this.layoutControlItem5});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(980, 45);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.txtName;
            this.layoutControlItem1.CustomizationFormText = "显示名称";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(233, 25);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(163, 25);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(218, 25);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "显示名称";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(94, 14);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.txtFunctionId;
            this.layoutControlItem4.CustomizationFormText = "功能ID";
            this.layoutControlItem4.Location = new System.Drawing.Point(218, 0);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(233, 25);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(163, 25);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(218, 25);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.Text = "功能ID";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(94, 14);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.txtWinformType;
            this.layoutControlItem6.CustomizationFormText = "Winform窗体类型";
            this.layoutControlItem6.Location = new System.Drawing.Point(436, 0);
            this.layoutControlItem6.MaxSize = new System.Drawing.Size(233, 25);
            this.layoutControlItem6.MinSize = new System.Drawing.Size(163, 25);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(233, 25);
            this.layoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem6.Text = "Winform窗体类型";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(94, 14);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.txtUrl;
            this.layoutControlItem7.CustomizationFormText = "Web界面Url地址";
            this.layoutControlItem7.Location = new System.Drawing.Point(669, 0);
            this.layoutControlItem7.MaxSize = new System.Drawing.Size(233, 25);
            this.layoutControlItem7.MinSize = new System.Drawing.Size(163, 25);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(207, 25);
            this.layoutControlItem7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem7.Text = "Web界面Url地址";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(94, 14);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.txtVisible;
            this.layoutControlItem5.CustomizationFormText = "是否可见";
            this.layoutControlItem5.Location = new System.Drawing.Point(876, 0);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(84, 25);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(84, 25);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.Text = "是否可见";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerControl1.Location = new System.Drawing.Point(12, 87);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.groupControl1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.winGridViewPager1);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(983, 588);
            this.splitContainerControl1.SplitterPosition = 222;
            this.splitContainerControl1.TabIndex = 16;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.treeView1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(222, 588);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "菜单管理";
            // 
            // treeView1
            // 
            this.treeView1.ContextMenuStrip = this.ctxMenuTree;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.FullRowSelect = true;
            this.treeView1.HideSelection = false;
            this.treeView1.HotTracking = true;
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Indent = 22;
            this.treeView1.ItemHeight = 24;
            this.treeView1.Location = new System.Drawing.Point(2, 22);
            this.treeView1.Name = "treeView1";
            treeNode1.ImageIndex = 1;
            treeNode1.Name = "节点1";
            treeNode1.Text = "菜单1";
            treeNode2.Name = "节点0";
            treeNode2.Text = "系统1";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2});
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(218, 564);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // ctxMenuTree
            // 
            this.ctxMenuTree.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxMenuTree_Refresh});
            this.ctxMenuTree.Name = "ctxMenuTree";
            this.ctxMenuTree.Size = new System.Drawing.Size(141, 26);
            // 
            // ctxMenuTree_Refresh
            // 
            this.ctxMenuTree_Refresh.Name = "ctxMenuTree_Refresh";
            this.ctxMenuTree_Refresh.Size = new System.Drawing.Size(140, 22);
            this.ctxMenuTree_Refresh.Text = "刷新列表(&R)";
            this.ctxMenuTree_Refresh.Click += new System.EventHandler(this.ctxMenuTree_Refresh_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "0036.ICO");
            this.imageList1.Images.SetKeyName(1, "(00,13).png");
            this.imageList1.Images.SetKeyName(2, "(03,00).png");
            this.imageList1.Images.SetKeyName(3, "(07,11).png");
            // 
            // FrmMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1004, 680);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnAddNew);
            this.Controls.Add(this.btnExport);
            this.Name = "FrmMenu";
            this.Text = "菜单管理";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFunctionId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWinformType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUrl.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVisible.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.ctxMenuTree.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.SimpleButton btnAddNew;
        private DevExpress.XtraEditors.SimpleButton btnExport;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private BB.BaseUI.Pager.WinGridViewPager winGridViewPager1;

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;

        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.TextEdit txtFunctionId;     
         private DevExpress.XtraEditors.TextEdit txtWinformType;     
         private DevExpress.XtraEditors.TextEdit txtUrl;

         private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;    
         private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;    
         private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;    
         private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;    
         private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
         private DevExpress.XtraEditors.CheckEdit txtVisible;
         private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
         private System.Windows.Forms.TreeView treeView1;
         private DevExpress.XtraEditors.GroupControl groupControl1;
         private System.Windows.Forms.ContextMenuStrip ctxMenuTree;
         private System.Windows.Forms.ToolStripMenuItem ctxMenuTree_Refresh;
         private System.Windows.Forms.ImageList imageList1;    
 
    }
}