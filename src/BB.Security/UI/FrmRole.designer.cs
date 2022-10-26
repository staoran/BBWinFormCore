using BB.BaseUI.Control.Security;

namespace BB.Security.UI
{
    partial class FrmRole
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("节点0");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("节点2");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("节点1", new System.Windows.Forms.TreeNode[] {
            treeNode2});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRole));
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("节点0");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("节点2");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("节点1", new System.Windows.Forms.TreeNode[] {
            treeNode5});
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("节点0");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("节点2");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("节点1", new System.Windows.Forms.TreeNode[] {
            treeNode8});
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("节点0");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("节点2");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("节点1", new System.Windows.Forms.TreeNode[] {
            treeNode11});
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.roleList_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.roleList_Add = new System.Windows.Forms.ToolStripMenuItem();
            this.roleList_Update = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.roleList_ExpandAll = new System.Windows.Forms.ToolStripMenuItem();
            this.roleList_Collapse = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.txtNote = new DevExpress.XtraEditors.MemoEdit();
            this.label12 = new System.Windows.Forms.Label();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRemoveUser = new DevExpress.XtraEditors.SimpleButton();
            this.btnEditUser = new DevExpress.XtraEditors.SimpleButton();
            this.lvwUser = new System.Windows.Forms.ListBox();
            this.btnEditOU = new DevExpress.XtraEditors.SimpleButton();
            this.btnRemoveOU = new DevExpress.XtraEditors.SimpleButton();
            this.lvwOU = new System.Windows.Forms.ListBox();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.txtCompany = new BB.BaseUI.Control.Security.CompanyControl();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCategory = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSortCode = new DevExpress.XtraEditors.TextEdit();
            this.txtHandNo = new DevExpress.XtraEditors.TextEdit();
            this.groupControl4 = new DevExpress.XtraEditors.GroupControl();
            this.groupControl5 = new DevExpress.XtraEditors.GroupControl();
            this.imageFunction = new System.Windows.Forms.ImageList(this.components);
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.tabBasic = new DevExpress.XtraTab.XtraTabPage();
            this.tabMenu = new DevExpress.XtraTab.XtraTabPage();
            this.chkMenuSelectAll = new DevExpress.XtraEditors.CheckEdit();
            this.btnRefreshMenu = new DevExpress.XtraEditors.SimpleButton();
            this.btnSaveMenu = new DevExpress.XtraEditors.SimpleButton();
            this.treeMenu = new Sunny.UI.UITreeView();
            this.contextMenuStrip3 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menu_Refresh = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_ExpandAll = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Collapse = new System.Windows.Forms.ToolStripMenuItem();
            this.tabFunction = new DevExpress.XtraTab.XtraTabPage();
            this.chkFunctionSelectAll = new DevExpress.XtraEditors.CheckEdit();
            this.btnRefreshFunction = new DevExpress.XtraEditors.SimpleButton();
            this.btnSaveFunction = new DevExpress.XtraEditors.SimpleButton();
            this.treeFunction = new System.Windows.Forms.TreeView();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.function_Refresh = new System.Windows.Forms.ToolStripMenuItem();
            this.function_ExpandAll = new System.Windows.Forms.ToolStripMenuItem();
            this.function_Collapse = new System.Windows.Forms.ToolStripMenuItem();
            this.tabRoleData = new DevExpress.XtraTab.XtraTabPage();
            this.btnRefreshRoleData = new DevExpress.XtraEditors.SimpleButton();
            this.btnSaveRoleData = new DevExpress.XtraEditors.SimpleButton();
            this.chkAllRoleData = new DevExpress.XtraEditors.CheckEdit();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.treeRoleData = new System.Windows.Forms.TreeView();
            this.imageMenu = new System.Windows.Forms.ImageList(this.components);
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNote.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCategory.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSortCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHandNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).BeginInit();
            this.groupControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl5)).BeginInit();
            this.groupControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).BeginInit();
            this.splitContainerControl1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).BeginInit();
            this.splitContainerControl1.Panel2.SuspendLayout();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.tabBasic.SuspendLayout();
            this.tabMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkMenuSelectAll.Properties)).BeginInit();
            this.contextMenuStrip3.SuspendLayout();
            this.tabFunction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkFunctionSelectAll.Properties)).BeginInit();
            this.contextMenuStrip2.SuspendLayout();
            this.tabRoleData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkAllRoleData.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.ContextMenuStrip = this.contextMenuStrip1;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.HideSelection = false;
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.ItemHeight = 24;
            this.treeView1.Location = new System.Drawing.Point(2, 23);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "节点0";
            treeNode1.Text = "节点0";
            treeNode2.Name = "节点2";
            treeNode2.Text = "节点2";
            treeNode3.Name = "节点1";
            treeNode3.Text = "节点1";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode3});
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(223, 648);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.treeView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.roleList_Delete,
            this.roleList_Add,
            this.roleList_Update,
            this.toolStripSeparator1,
            this.roleList_ExpandAll,
            this.roleList_Collapse});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(165, 120);
            // 
            // roleList_Delete
            // 
            this.roleList_Delete.Name = "roleList_Delete";
            this.roleList_Delete.Size = new System.Drawing.Size(164, 22);
            this.roleList_Delete.Text = "删除(&D)";
            this.roleList_Delete.Click += new System.EventHandler(this.roleList_Delete_Click);
            // 
            // roleList_Add
            // 
            this.roleList_Add.Name = "roleList_Add";
            this.roleList_Add.Size = new System.Drawing.Size(164, 22);
            this.roleList_Add.Text = "添加(&A)";
            this.roleList_Add.Click += new System.EventHandler(this.roleList_Add_Click);
            // 
            // roleList_Update
            // 
            this.roleList_Update.Name = "roleList_Update";
            this.roleList_Update.Size = new System.Drawing.Size(164, 22);
            this.roleList_Update.Text = "刷新列表(&U)";
            this.roleList_Update.Click += new System.EventHandler(this.roleList_Update_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(161, 6);
            // 
            // roleList_ExpandAll
            // 
            this.roleList_ExpandAll.Name = "roleList_ExpandAll";
            this.roleList_ExpandAll.Size = new System.Drawing.Size(164, 22);
            this.roleList_ExpandAll.Text = "展开全部子节点";
            this.roleList_ExpandAll.Click += new System.EventHandler(this.roleList_ExpandAll_Click);
            // 
            // roleList_Collapse
            // 
            this.roleList_Collapse.Name = "roleList_Collapse";
            this.roleList_Collapse.Size = new System.Drawing.Size(164, 22);
            this.roleList_Collapse.Text = "折叠全部节点(&C)";
            this.roleList_Collapse.Click += new System.EventHandler(this.roleList_Collapse_Click);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "star.ico");
            this.imageList1.Images.SetKeyName(1, "Organ.ico");
            this.imageList1.Images.SetKeyName(2, "usergroup6.ico");
            this.imageList1.Images.SetKeyName(3, "rolekey.ico");
            this.imageList1.Images.SetKeyName(4, "user.ico");
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(395, 219);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "保存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtNote
            // 
            this.txtNote.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNote.Location = new System.Drawing.Point(92, 164);
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(378, 45);
            this.txtNote.TabIndex = 1;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(46, 171);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(43, 14);
            this.label12.TabIndex = 0;
            this.label12.Text = "描述：";
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(92, 29);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(378, 20);
            this.txtName.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "名称(*)：";
            // 
            // btnRemoveUser
            // 
            this.btnRemoveUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveUser.Location = new System.Drawing.Point(126, 643);
            this.btnRemoveUser.Name = "btnRemoveUser";
            this.btnRemoveUser.Size = new System.Drawing.Size(75, 23);
            this.btnRemoveUser.TabIndex = 1;
            this.btnRemoveUser.Text = "移除";
            this.btnRemoveUser.Click += new System.EventHandler(this.btnRemoveUser_Click);
            // 
            // btnEditUser
            // 
            this.btnEditUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditUser.Location = new System.Drawing.Point(29, 643);
            this.btnEditUser.Name = "btnEditUser";
            this.btnEditUser.Size = new System.Drawing.Size(75, 23);
            this.btnEditUser.TabIndex = 0;
            this.btnEditUser.Text = "添加";
            this.btnEditUser.Click += new System.EventHandler(this.btnEditUser_Click);
            // 
            // lvwUser
            // 
            this.lvwUser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwUser.FormattingEnabled = true;
            this.lvwUser.ItemHeight = 14;
            this.lvwUser.Location = new System.Drawing.Point(3, 24);
            this.lvwUser.Name = "lvwUser";
            this.lvwUser.Size = new System.Drawing.Size(214, 592);
            this.lvwUser.TabIndex = 2;
            // 
            // btnEditOU
            // 
            this.btnEditOU.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditOU.Location = new System.Drawing.Point(305, 387);
            this.btnEditOU.Name = "btnEditOU";
            this.btnEditOU.Size = new System.Drawing.Size(75, 23);
            this.btnEditOU.TabIndex = 0;
            this.btnEditOU.Text = "编辑";
            this.btnEditOU.Click += new System.EventHandler(this.btnEditOU_Click);
            // 
            // btnRemoveOU
            // 
            this.btnRemoveOU.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveOU.Location = new System.Drawing.Point(402, 387);
            this.btnRemoveOU.Name = "btnRemoveOU";
            this.btnRemoveOU.Size = new System.Drawing.Size(75, 23);
            this.btnRemoveOU.TabIndex = 1;
            this.btnRemoveOU.Text = "移除";
            this.btnRemoveOU.Click += new System.EventHandler(this.btnRemoveOU_Click);
            // 
            // lvwOU
            // 
            this.lvwOU.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwOU.FormattingEnabled = true;
            this.lvwOU.ItemHeight = 14;
            this.lvwOU.Location = new System.Drawing.Point(3, 23);
            this.lvwOU.Name = "lvwOU";
            this.lvwOU.Size = new System.Drawing.Size(490, 354);
            this.lvwOU.TabIndex = 3;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(84, 11);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(61, 23);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.Text = "删除";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // groupControl1
            // 
            this.groupControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl1.Controls.Add(this.treeView1);
            this.groupControl1.Location = new System.Drawing.Point(3, 45);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(227, 673);
            this.groupControl1.TabIndex = 2;
            this.groupControl1.Text = "角色列表";
            // 
            // groupControl2
            // 
            this.groupControl2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl2.Controls.Add(this.txtCompany);
            this.groupControl2.Controls.Add(this.label5);
            this.groupControl2.Controls.Add(this.txtCategory);
            this.groupControl2.Controls.Add(this.btnSave);
            this.groupControl2.Controls.Add(this.txtNote);
            this.groupControl2.Controls.Add(this.label4);
            this.groupControl2.Controls.Add(this.label3);
            this.groupControl2.Controls.Add(this.label2);
            this.groupControl2.Controls.Add(this.label1);
            this.groupControl2.Controls.Add(this.label12);
            this.groupControl2.Controls.Add(this.txtSortCode);
            this.groupControl2.Controls.Add(this.txtHandNo);
            this.groupControl2.Controls.Add(this.txtName);
            this.groupControl2.Location = new System.Drawing.Point(15, 12);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(496, 253);
            this.groupControl2.TabIndex = 3;
            this.groupControl2.Text = "角色详细信息";
            // 
            // txtCompany
            // 
            this.txtCompany.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCompany.Location = new System.Drawing.Point(92, 57);
            this.txtCompany.Name = "txtCompany";
            this.txtCompany.Size = new System.Drawing.Size(378, 20);
            this.txtCompany.TabIndex = 9;
            this.txtCompany.Value = "-1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 59);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 14);
            this.label5.TabIndex = 7;
            this.label5.Text = "所属公司(*)：";
            // 
            // txtCategory
            // 
            this.txtCategory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCategory.Location = new System.Drawing.Point(92, 137);
            this.txtCategory.Name = "txtCategory";
            this.txtCategory.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtCategory.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.txtCategory.Size = new System.Drawing.Size(378, 20);
            this.txtCategory.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 140);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 14);
            this.label4.TabIndex = 0;
            this.label4.Text = "角色分类：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 14);
            this.label3.TabIndex = 0;
            this.label3.Text = "排序码：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 14);
            this.label2.TabIndex = 0;
            this.label2.Text = "角色编码：";
            // 
            // txtSortCode
            // 
            this.txtSortCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSortCode.Location = new System.Drawing.Point(92, 110);
            this.txtSortCode.Name = "txtSortCode";
            this.txtSortCode.Size = new System.Drawing.Size(378, 20);
            this.txtSortCode.TabIndex = 0;
            // 
            // txtHandNo
            // 
            this.txtHandNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHandNo.Location = new System.Drawing.Point(92, 83);
            this.txtHandNo.Name = "txtHandNo";
            this.txtHandNo.Size = new System.Drawing.Size(378, 20);
            this.txtHandNo.TabIndex = 0;
            // 
            // groupControl4
            // 
            this.groupControl4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl4.Controls.Add(this.btnRemoveUser);
            this.groupControl4.Controls.Add(this.lvwUser);
            this.groupControl4.Controls.Add(this.btnEditUser);
            this.groupControl4.Location = new System.Drawing.Point(527, 10);
            this.groupControl4.Name = "groupControl4";
            this.groupControl4.Size = new System.Drawing.Size(220, 679);
            this.groupControl4.TabIndex = 5;
            this.groupControl4.Text = "包含用户";
            // 
            // groupControl5
            // 
            this.groupControl5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl5.Controls.Add(this.btnEditOU);
            this.groupControl5.Controls.Add(this.lvwOU);
            this.groupControl5.Controls.Add(this.btnRemoveOU);
            this.groupControl5.Location = new System.Drawing.Point(15, 271);
            this.groupControl5.Name = "groupControl5";
            this.groupControl5.Size = new System.Drawing.Size(496, 418);
            this.groupControl5.TabIndex = 6;
            this.groupControl5.Text = "包含机构";
            // 
            // imageFunction
            // 
            this.imageFunction.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageFunction.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageFunction.ImageStream")));
            this.imageFunction.TransparentColor = System.Drawing.Color.Transparent;
            this.imageFunction.Images.SetKeyName(0, "0036.ICO");
            this.imageFunction.Images.SetKeyName(1, "key2.ico");
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            // 
            // splitContainerControl1.Panel1
            // 
            this.splitContainerControl1.Panel1.Controls.Add(this.groupControl1);
            this.splitContainerControl1.Panel1.Controls.Add(this.btnAdd);
            this.splitContainerControl1.Panel1.Controls.Add(this.btnDelete);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            // 
            // splitContainerControl1.Panel2
            // 
            this.splitContainerControl1.Panel2.Controls.Add(this.xtraTabControl1);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1008, 730);
            this.splitContainerControl1.SplitterPosition = 241;
            this.splitContainerControl1.TabIndex = 7;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.tabBasic;
            this.xtraTabControl1.Size = new System.Drawing.Size(757, 730);
            this.xtraTabControl1.TabIndex = 0;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabBasic,
            this.tabMenu,
            this.tabFunction,
            this.tabRoleData});
            // 
            // tabBasic
            // 
            this.tabBasic.Controls.Add(this.groupControl5);
            this.tabBasic.Controls.Add(this.groupControl2);
            this.tabBasic.Controls.Add(this.groupControl4);
            this.tabBasic.Name = "tabBasic";
            this.tabBasic.Size = new System.Drawing.Size(755, 704);
            this.tabBasic.Text = "角色基础信息";
            // 
            // tabMenu
            // 
            this.tabMenu.Controls.Add(this.chkMenuSelectAll);
            this.tabMenu.Controls.Add(this.btnRefreshMenu);
            this.tabMenu.Controls.Add(this.btnSaveMenu);
            this.tabMenu.Controls.Add(this.treeMenu);
            this.tabMenu.Name = "tabMenu";
            this.tabMenu.Size = new System.Drawing.Size(755, 704);
            this.tabMenu.Text = "可操作的菜单和按钮";
            // 
            // chkMenuSelectAll
            // 
            this.chkMenuSelectAll.Location = new System.Drawing.Point(6, 18);
            this.chkMenuSelectAll.Name = "chkMenuSelectAll";
            this.chkMenuSelectAll.Properties.Caption = "全选/反选";
            this.chkMenuSelectAll.Size = new System.Drawing.Size(79, 20);
            this.chkMenuSelectAll.TabIndex = 9;
            this.chkMenuSelectAll.CheckedChanged += new System.EventHandler(this.chkMenuSelectAll_CheckedChanged);
            // 
            // btnRefreshMenu
            // 
            this.btnRefreshMenu.Location = new System.Drawing.Point(169, 14);
            this.btnRefreshMenu.Name = "btnRefreshMenu";
            this.btnRefreshMenu.Size = new System.Drawing.Size(79, 23);
            this.btnRefreshMenu.TabIndex = 7;
            this.btnRefreshMenu.Text = "刷新列表";
            this.btnRefreshMenu.Click += new System.EventHandler(this.btnRefreshMenu_Click);
            // 
            // btnSaveMenu
            // 
            this.btnSaveMenu.Location = new System.Drawing.Point(275, 14);
            this.btnSaveMenu.Name = "btnSaveMenu";
            this.btnSaveMenu.Size = new System.Drawing.Size(141, 23);
            this.btnSaveMenu.TabIndex = 6;
            this.btnSaveMenu.Text = "保存可操作的菜单和按钮";
            this.btnSaveMenu.Click += new System.EventHandler(this.btnSaveMenu_Click);
            // 
            // treeMenu
            // 
            this.treeMenu.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeMenu.CheckBoxes = true;
            this.treeMenu.ContextMenuStrip = this.contextMenuStrip3;
            this.treeMenu.FillColor = System.Drawing.Color.White;
            this.treeMenu.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.treeMenu.ImageIndex = 0;
            this.treeMenu.ImageList = this.imageMenu;
            this.treeMenu.Indent = 27;
            this.treeMenu.ItemHeight = 24;
            this.treeMenu.Location = new System.Drawing.Point(8, 43);
            this.treeMenu.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.treeMenu.MinimumSize = new System.Drawing.Size(1, 1);
            this.treeMenu.Name = "treeMenu";
            treeNode4.Name = "节点0";
            treeNode4.Text = "节点0";
            treeNode5.Name = "节点2";
            treeNode5.Text = "节点2";
            treeNode6.Name = "节点1";
            treeNode6.Text = "节点1";
            this.treeMenu.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode6});
            this.treeMenu.SelectedImageIndex = 0;
            this.treeMenu.ShowText = false;
            this.treeMenu.Size = new System.Drawing.Size(741, 651);
            this.treeMenu.Style = Sunny.UI.UIStyle.Custom;
            this.treeMenu.TabIndex = 5;
            this.treeMenu.Text = null;
            this.treeMenu.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.treeMenu.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.treeMenu.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeMenu_AfterCheck);
            // 
            // contextMenuStrip3
            // 
            this.contextMenuStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_Refresh,
            this.menu_ExpandAll,
            this.menu_Collapse});
            this.contextMenuStrip3.Name = "contextMenuStrip3";
            this.contextMenuStrip3.Size = new System.Drawing.Size(165, 70);
            // 
            // menu_Refresh
            // 
            this.menu_Refresh.Name = "menu_Refresh";
            this.menu_Refresh.Size = new System.Drawing.Size(164, 22);
            this.menu_Refresh.Text = "刷新列表(&U)";
            this.menu_Refresh.Click += new System.EventHandler(this.menu_Refresh_Click);
            // 
            // menu_ExpandAll
            // 
            this.menu_ExpandAll.Name = "menu_ExpandAll";
            this.menu_ExpandAll.Size = new System.Drawing.Size(164, 22);
            this.menu_ExpandAll.Text = "展开全部子节点";
            this.menu_ExpandAll.Click += new System.EventHandler(this.menu_ExpandAll_Click);
            // 
            // menu_Collapse
            // 
            this.menu_Collapse.Name = "menu_Collapse";
            this.menu_Collapse.Size = new System.Drawing.Size(164, 22);
            this.menu_Collapse.Text = "折叠全部节点(&C)";
            this.menu_Collapse.Click += new System.EventHandler(this.menu_Collapse_Click);
            // 
            // tabFunction
            // 
            this.tabFunction.Controls.Add(this.chkFunctionSelectAll);
            this.tabFunction.Controls.Add(this.btnRefreshFunction);
            this.tabFunction.Controls.Add(this.btnSaveFunction);
            this.tabFunction.Controls.Add(this.treeFunction);
            this.tabFunction.Name = "tabFunction";
            this.tabFunction.Size = new System.Drawing.Size(755, 704);
            this.tabFunction.Text = "可操作功能(废弃)";
            // 
            // chkFunctionSelectAll
            // 
            this.chkFunctionSelectAll.Location = new System.Drawing.Point(6, 18);
            this.chkFunctionSelectAll.Name = "chkFunctionSelectAll";
            this.chkFunctionSelectAll.Properties.Caption = "全选/反选";
            this.chkFunctionSelectAll.Size = new System.Drawing.Size(79, 20);
            this.chkFunctionSelectAll.TabIndex = 9;
            this.chkFunctionSelectAll.CheckedChanged += new System.EventHandler(this.chkFunctionSelectAll_CheckedChanged);
            // 
            // btnRefreshFunction
            // 
            this.btnRefreshFunction.Location = new System.Drawing.Point(169, 14);
            this.btnRefreshFunction.Name = "btnRefreshFunction";
            this.btnRefreshFunction.Size = new System.Drawing.Size(79, 23);
            this.btnRefreshFunction.TabIndex = 7;
            this.btnRefreshFunction.Text = "刷新列表";
            this.btnRefreshFunction.Click += new System.EventHandler(this.btnRefreshFunction_Click);
            // 
            // btnSaveFunction
            // 
            this.btnSaveFunction.Location = new System.Drawing.Point(275, 14);
            this.btnSaveFunction.Name = "btnSaveFunction";
            this.btnSaveFunction.Size = new System.Drawing.Size(111, 23);
            this.btnSaveFunction.TabIndex = 6;
            this.btnSaveFunction.Text = "保存操作功能";
            this.btnSaveFunction.Click += new System.EventHandler(this.btnSaveFunction_Click);
            // 
            // treeFunction
            // 
            this.treeFunction.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeFunction.CheckBoxes = true;
            this.treeFunction.ContextMenuStrip = this.contextMenuStrip2;
            this.treeFunction.ImageIndex = 0;
            this.treeFunction.ImageList = this.imageFunction;
            this.treeFunction.Indent = 27;
            this.treeFunction.ItemHeight = 24;
            this.treeFunction.Location = new System.Drawing.Point(8, 43);
            this.treeFunction.Name = "treeFunction";
            treeNode7.Name = "节点0";
            treeNode7.Text = "节点0";
            treeNode8.Name = "节点2";
            treeNode8.Text = "节点2";
            treeNode9.Name = "节点1";
            treeNode9.Text = "节点1";
            this.treeFunction.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode7,
            treeNode9});
            this.treeFunction.SelectedImageIndex = 0;
            this.treeFunction.Size = new System.Drawing.Size(741, 651);
            this.treeFunction.TabIndex = 5;
            this.treeFunction.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeFunction_AfterCheck);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.function_Refresh,
            this.function_ExpandAll,
            this.function_Collapse});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(165, 70);
            // 
            // function_Refresh
            // 
            this.function_Refresh.Image = ((System.Drawing.Image)(resources.GetObject("function_Refresh.Image")));
            this.function_Refresh.Name = "function_Refresh";
            this.function_Refresh.Size = new System.Drawing.Size(164, 22);
            this.function_Refresh.Text = "刷新列表(&U)";
            this.function_Refresh.Click += new System.EventHandler(this.function_Refresh_Click);
            // 
            // function_ExpandAll
            // 
            this.function_ExpandAll.Image = ((System.Drawing.Image)(resources.GetObject("function_ExpandAll.Image")));
            this.function_ExpandAll.Name = "function_ExpandAll";
            this.function_ExpandAll.Size = new System.Drawing.Size(164, 22);
            this.function_ExpandAll.Text = "展开全部子节点";
            this.function_ExpandAll.Click += new System.EventHandler(this.function_ExpandAll_Click);
            // 
            // function_Collapse
            // 
            this.function_Collapse.Image = ((System.Drawing.Image)(resources.GetObject("function_Collapse.Image")));
            this.function_Collapse.Name = "function_Collapse";
            this.function_Collapse.Size = new System.Drawing.Size(164, 22);
            this.function_Collapse.Text = "折叠全部节点(&C)";
            this.function_Collapse.Click += new System.EventHandler(this.function_Collapse_Click);
            // 
            // tabRoleData
            // 
            this.tabRoleData.Controls.Add(this.btnRefreshRoleData);
            this.tabRoleData.Controls.Add(this.btnSaveRoleData);
            this.tabRoleData.Controls.Add(this.chkAllRoleData);
            this.tabRoleData.Controls.Add(this.groupControl3);
            this.tabRoleData.Name = "tabRoleData";
            this.tabRoleData.Size = new System.Drawing.Size(755, 704);
            this.tabRoleData.Text = "可访问数据";
            // 
            // btnRefreshRoleData
            // 
            this.btnRefreshRoleData.Location = new System.Drawing.Point(166, 14);
            this.btnRefreshRoleData.Name = "btnRefreshRoleData";
            this.btnRefreshRoleData.Size = new System.Drawing.Size(79, 23);
            this.btnRefreshRoleData.TabIndex = 10;
            this.btnRefreshRoleData.Text = "刷新列表";
            this.btnRefreshRoleData.Click += new System.EventHandler(this.btnRefreshRoleData_Click);
            // 
            // btnSaveRoleData
            // 
            this.btnSaveRoleData.Location = new System.Drawing.Point(262, 14);
            this.btnSaveRoleData.Name = "btnSaveRoleData";
            this.btnSaveRoleData.Size = new System.Drawing.Size(115, 23);
            this.btnSaveRoleData.TabIndex = 9;
            this.btnSaveRoleData.Text = "保存数据权限";
            this.btnSaveRoleData.Click += new System.EventHandler(this.btnSaveRoleData_Click);
            // 
            // chkAllRoleData
            // 
            this.chkAllRoleData.Location = new System.Drawing.Point(14, 18);
            this.chkAllRoleData.Name = "chkAllRoleData";
            this.chkAllRoleData.Properties.Caption = "全选/反选";
            this.chkAllRoleData.Size = new System.Drawing.Size(89, 20);
            this.chkAllRoleData.TabIndex = 8;
            this.chkAllRoleData.CheckedChanged += new System.EventHandler(this.chkAllRoleData_CheckedChanged);
            // 
            // groupControl3
            // 
            this.groupControl3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl3.Controls.Add(this.treeRoleData);
            this.groupControl3.Location = new System.Drawing.Point(14, 43);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(734, 649);
            this.groupControl3.TabIndex = 7;
            this.groupControl3.Text = "组织机构列表";
            // 
            // treeRoleData
            // 
            this.treeRoleData.CheckBoxes = true;
            this.treeRoleData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeRoleData.HideSelection = false;
            this.treeRoleData.ImageIndex = 0;
            this.treeRoleData.ImageList = this.imageList1;
            this.treeRoleData.ItemHeight = 24;
            this.treeRoleData.Location = new System.Drawing.Point(2, 23);
            this.treeRoleData.Name = "treeRoleData";
            treeNode10.Name = "节点0";
            treeNode10.Text = "节点0";
            treeNode11.Name = "节点2";
            treeNode11.Text = "节点2";
            treeNode12.Name = "节点1";
            treeNode12.Text = "节点1";
            this.treeRoleData.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode10,
            treeNode12});
            this.treeRoleData.SelectedImageIndex = 0;
            this.treeRoleData.Size = new System.Drawing.Size(730, 624);
            this.treeRoleData.TabIndex = 0;
            // 
            // imageMenu
            // 
            this.imageMenu.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageMenu.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageMenu.ImageStream")));
            this.imageMenu.TransparentColor = System.Drawing.Color.Transparent;
            this.imageMenu.Images.SetKeyName(0, "底层架构.png");
            this.imageMenu.Images.SetKeyName(1, "分支.png");
            this.imageMenu.Images.SetKeyName(2, "联盟链.png");
            this.imageMenu.Images.SetKeyName(3, "灵活扩展.png");
            this.imageMenu.Images.SetKeyName(4, "全领域规模.png");
            // 
            // FrmRole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "FrmRole";
            this.Text = "角色管理";
            this.Load += new System.EventHandler(this.FrmRole_Load);
            this.Controls.SetChildIndex(this.splitContainerControl1, 0);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtNote.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCategory.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSortCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHandNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).EndInit();
            this.groupControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl5)).EndInit();
            this.groupControl5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).EndInit();
            this.splitContainerControl1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).EndInit();
            this.splitContainerControl1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.tabBasic.ResumeLayout(false);
            this.tabMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkMenuSelectAll.Properties)).EndInit();
            this.contextMenuStrip3.ResumeLayout(false);
            this.tabFunction.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkFunctionSelectAll.Properties)).EndInit();
            this.contextMenuStrip2.ResumeLayout(false);
            this.tabRoleData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkAllRoleData.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ImageList imageList1;
        private DevExpress.XtraEditors.TextEdit txtName;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.MemoEdit txtNote;
        private System.Windows.Forms.Label label12;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem roleList_Delete;
        private System.Windows.Forms.ToolStripMenuItem roleList_Add;
        private System.Windows.Forms.ToolStripMenuItem roleList_Update;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem roleList_ExpandAll;
        private System.Windows.Forms.ListBox lvwUser;
        private System.Windows.Forms.ListBox lvwOU;
        private DevExpress.XtraEditors.SimpleButton btnRemoveUser;
        private DevExpress.XtraEditors.SimpleButton btnEditUser;
        private DevExpress.XtraEditors.SimpleButton btnEditOU;
        private DevExpress.XtraEditors.SimpleButton btnRemoveOU;
        private DevExpress.XtraEditors.SimpleButton btnAdd;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.GroupControl groupControl4;
        private DevExpress.XtraEditors.GroupControl groupControl5;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.TextEdit txtHandNo;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.TextEdit txtSortCode;
        private DevExpress.XtraEditors.ComboBoxEdit txtCategory;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ImageList imageFunction;
        private CompanyControl txtCompany;
        private System.Windows.Forms.ToolStripMenuItem roleList_Collapse;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage tabBasic;
        private DevExpress.XtraTab.XtraTabPage tabMenu;
        private Sunny.UI.UITreeView treeMenu;
        private DevExpress.XtraEditors.SimpleButton btnSaveMenu;
        private DevExpress.XtraEditors.SimpleButton btnRefreshMenu;
        private DevExpress.XtraTab.XtraTabPage tabFunction;
        private System.Windows.Forms.TreeView treeFunction;
        private DevExpress.XtraEditors.SimpleButton btnSaveFunction;
        private DevExpress.XtraEditors.SimpleButton btnRefreshFunction;
        private DevExpress.XtraTab.XtraTabPage tabRoleData;
        private DevExpress.XtraEditors.CheckEdit chkAllRoleData;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private System.Windows.Forms.TreeView treeRoleData;
        private DevExpress.XtraEditors.SimpleButton btnSaveRoleData;
        private DevExpress.XtraEditors.SimpleButton btnRefreshRoleData;
        private DevExpress.XtraEditors.CheckEdit chkMenuSelectAll;
        private DevExpress.XtraEditors.CheckEdit chkFunctionSelectAll;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip3;
        private System.Windows.Forms.ToolStripMenuItem menu_Refresh;
        private System.Windows.Forms.ToolStripMenuItem menu_ExpandAll;
        private System.Windows.Forms.ToolStripMenuItem menu_Collapse;
        private System.Windows.Forms.ToolStripMenuItem function_Refresh;
        private System.Windows.Forms.ToolStripMenuItem function_ExpandAll;
        private System.Windows.Forms.ToolStripMenuItem function_Collapse;
        private System.Windows.Forms.ImageList imageMenu;
    }
}