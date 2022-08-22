using BB.BaseUI.Control.Security;

namespace BB.Security.UI
{
    partial class FrmOu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmOu));
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menu_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Add = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Update = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menu_ExpandAll = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Collapse = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.txtNote = new DevExpress.XtraEditors.MemoEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.label12 = new System.Windows.Forms.Label();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAddress = new DevExpress.XtraEditors.MemoEdit();
            this.btnRemoveUser = new DevExpress.XtraEditors.SimpleButton();
            this.btnEditUser = new DevExpress.XtraEditors.SimpleButton();
            this.lvwUser = new System.Windows.Forms.ListBox();
            this.lvwRole = new System.Windows.Forms.ListBox();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.cmbUpperOU = new DeptControl();
            this.txtCreationDate = new DevExpress.XtraEditors.TextEdit();
            this.txtInnerPhone = new DevExpress.XtraEditors.TextEdit();
            this.txtCreator = new DevExpress.XtraEditors.TextEdit();
            this.txtOuterPhone = new DevExpress.XtraEditors.TextEdit();
            this.txtSortCode = new DevExpress.XtraEditors.TextEdit();
            this.txtHandNo = new DevExpress.XtraEditors.TextEdit();
            this.label11 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCategory = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label6 = new System.Windows.Forms.Label();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.groupControl4 = new DevExpress.XtraEditors.GroupControl();
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNote.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreationDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInnerPhone.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreator.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOuterPhone.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSortCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHandNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCategory.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).BeginInit();
            this.groupControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
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
            this.treeView1.Location = new System.Drawing.Point(2, 22);
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
            this.treeView1.Size = new System.Drawing.Size(272, 646);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.treeView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_Delete,
            this.menu_Add,
            this.menu_Update,
            this.toolStripSeparator1,
            this.menu_ExpandAll,
            this.menu_Collapse});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(176, 120);
            // 
            // menu_Delete
            // 
            this.menu_Delete.Image = ((System.Drawing.Image)(resources.GetObject("menu_Delete.Image")));
            this.menu_Delete.Name = "menu_Delete";
            this.menu_Delete.Size = new System.Drawing.Size(175, 22);
            this.menu_Delete.Text = "删除(&D)";
            this.menu_Delete.Click += new System.EventHandler(this.menu_Delete_Click);
            // 
            // menu_Add
            // 
            this.menu_Add.Image = ((System.Drawing.Image)(resources.GetObject("menu_Add.Image")));
            this.menu_Add.Name = "menu_Add";
            this.menu_Add.Size = new System.Drawing.Size(175, 22);
            this.menu_Add.Text = "添加(&A)";
            this.menu_Add.Click += new System.EventHandler(this.menu_Add_Click);
            // 
            // menu_Update
            // 
            this.menu_Update.Image = ((System.Drawing.Image)(resources.GetObject("menu_Update.Image")));
            this.menu_Update.Name = "menu_Update";
            this.menu_Update.Size = new System.Drawing.Size(175, 22);
            this.menu_Update.Text = "刷新列表(&U)";
            this.menu_Update.Click += new System.EventHandler(this.menu_Update_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(172, 6);
            // 
            // menu_ExpandAll
            // 
            this.menu_ExpandAll.Image = ((System.Drawing.Image)(resources.GetObject("menu_ExpandAll.Image")));
            this.menu_ExpandAll.Name = "menu_ExpandAll";
            this.menu_ExpandAll.Size = new System.Drawing.Size(175, 22);
            this.menu_ExpandAll.Text = "展开全部子节点(&E)";
            this.menu_ExpandAll.Click += new System.EventHandler(this.menu_ExpandAll_Click);
            // 
            // menu_Collapse
            // 
            this.menu_Collapse.Image = ((System.Drawing.Image)(resources.GetObject("menu_Collapse.Image")));
            this.menu_Collapse.Name = "menu_Collapse";
            this.menu_Collapse.Size = new System.Drawing.Size(175, 22);
            this.menu_Collapse.Text = "折叠全部节点(&C)";
            this.menu_Collapse.Click += new System.EventHandler(this.menu_Collapse_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "star.ico");
            this.imageList1.Images.SetKeyName(1, "Organ.ico");
            this.imageList1.Images.SetKeyName(2, "usergroup6.ico");
            this.imageList1.Images.SetKeyName(3, "usergroup5.ico");
            this.imageList1.Images.SetKeyName(4, "user005.ico");
            // 
            // txtNote
            // 
            this.txtNote.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNote.Location = new System.Drawing.Point(93, 435);
            this.txtNote.MinimumSize = new System.Drawing.Size(0, 50);
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(301, 225);
            this.txtNote.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(44, 449);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 14);
            this.label3.TabIndex = 5;
            this.label3.Text = "描述：";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(319, 666);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "保存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(25, 372);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(67, 14);
            this.label12.TabIndex = 0;
            this.label12.Text = "机构地址：";
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(93, 31);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(301, 20);
            this.txtName.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 14);
            this.label2.TabIndex = 0;
            this.label2.Text = "上级机构(*)：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "机构名(*)：";
            // 
            // txtAddress
            // 
            this.txtAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAddress.Location = new System.Drawing.Point(93, 368);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(301, 50);
            this.txtAddress.TabIndex = 2;
            // 
            // btnRemoveUser
            // 
            this.btnRemoveUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveUser.Location = new System.Drawing.Point(185, 293);
            this.btnRemoveUser.Name = "btnRemoveUser";
            this.btnRemoveUser.Size = new System.Drawing.Size(75, 23);
            this.btnRemoveUser.TabIndex = 1;
            this.btnRemoveUser.Text = "移除";
            this.btnRemoveUser.Click += new System.EventHandler(this.btnRemoveUser_Click);
            // 
            // btnEditUser
            // 
            this.btnEditUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditUser.Location = new System.Drawing.Point(88, 293);
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
            this.lvwUser.Location = new System.Drawing.Point(3, 25);
            this.lvwUser.Name = "lvwUser";
            this.lvwUser.Size = new System.Drawing.Size(259, 256);
            this.lvwUser.TabIndex = 3;
            // 
            // lvwRole
            // 
            this.lvwRole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwRole.FormattingEnabled = true;
            this.lvwRole.ItemHeight = 14;
            this.lvwRole.Location = new System.Drawing.Point(2, 22);
            this.lvwRole.Name = "lvwRole";
            this.lvwRole.Size = new System.Drawing.Size(261, 334);
            this.lvwRole.TabIndex = 4;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(13, 14);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(61, 23);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "添加";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(87, 14);
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
            this.groupControl1.Location = new System.Drawing.Point(3, 48);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(276, 670);
            this.groupControl1.TabIndex = 2;
            this.groupControl1.Text = "组织机构列表";
            // 
            // groupControl2
            // 
            this.groupControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl2.Controls.Add(this.cmbUpperOU);
            this.groupControl2.Controls.Add(this.txtNote);
            this.groupControl2.Controls.Add(this.txtCreationDate);
            this.groupControl2.Controls.Add(this.txtInnerPhone);
            this.groupControl2.Controls.Add(this.txtCreator);
            this.groupControl2.Controls.Add(this.txtOuterPhone);
            this.groupControl2.Controls.Add(this.txtSortCode);
            this.groupControl2.Controls.Add(this.txtHandNo);
            this.groupControl2.Controls.Add(this.label11);
            this.groupControl2.Controls.Add(this.txtName);
            this.groupControl2.Controls.Add(this.label8);
            this.groupControl2.Controls.Add(this.label3);
            this.groupControl2.Controls.Add(this.label10);
            this.groupControl2.Controls.Add(this.label5);
            this.groupControl2.Controls.Add(this.label7);
            this.groupControl2.Controls.Add(this.txtAddress);
            this.groupControl2.Controls.Add(this.label4);
            this.groupControl2.Controls.Add(this.btnSave);
            this.groupControl2.Controls.Add(this.label1);
            this.groupControl2.Controls.Add(this.txtCategory);
            this.groupControl2.Controls.Add(this.label6);
            this.groupControl2.Controls.Add(this.label2);
            this.groupControl2.Controls.Add(this.label12);
            this.groupControl2.Location = new System.Drawing.Point(7, 14);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(418, 704);
            this.groupControl2.TabIndex = 3;
            this.groupControl2.Text = "组织机构详细信息";
            // 
            // cmbUpperOU
            // 
            this.cmbUpperOU.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbUpperOU.Location = new System.Drawing.Point(93, 67);
            this.cmbUpperOU.Name = "cmbUpperOU";
            this.cmbUpperOU.Size = new System.Drawing.Size(301, 20);
            this.cmbUpperOU.TabIndex = 7;
            this.cmbUpperOU.Value = "-1";
            // 
            // txtCreationDate
            // 
            this.txtCreationDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCreationDate.Location = new System.Drawing.Point(93, 331);
            this.txtCreationDate.Name = "txtCreationDate";
            this.txtCreationDate.Properties.Appearance.BackColor = System.Drawing.Color.SeaShell;
            this.txtCreationDate.Properties.Appearance.Options.UseBackColor = true;
            this.txtCreationDate.Properties.ReadOnly = true;
            this.txtCreationDate.Size = new System.Drawing.Size(301, 20);
            this.txtCreationDate.TabIndex = 0;
            // 
            // txtInnerPhone
            // 
            this.txtInnerPhone.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInnerPhone.Location = new System.Drawing.Point(93, 257);
            this.txtInnerPhone.Name = "txtInnerPhone";
            this.txtInnerPhone.Size = new System.Drawing.Size(301, 20);
            this.txtInnerPhone.TabIndex = 0;
            // 
            // txtCreator
            // 
            this.txtCreator.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCreator.Location = new System.Drawing.Point(93, 294);
            this.txtCreator.Name = "txtCreator";
            this.txtCreator.Properties.Appearance.BackColor = System.Drawing.Color.SeaShell;
            this.txtCreator.Properties.Appearance.Options.UseBackColor = true;
            this.txtCreator.Properties.ReadOnly = true;
            this.txtCreator.Size = new System.Drawing.Size(301, 20);
            this.txtCreator.TabIndex = 0;
            // 
            // txtOuterPhone
            // 
            this.txtOuterPhone.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOuterPhone.Location = new System.Drawing.Point(93, 220);
            this.txtOuterPhone.Name = "txtOuterPhone";
            this.txtOuterPhone.Size = new System.Drawing.Size(301, 20);
            this.txtOuterPhone.TabIndex = 0;
            // 
            // txtSortCode
            // 
            this.txtSortCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSortCode.Location = new System.Drawing.Point(93, 144);
            this.txtSortCode.Name = "txtSortCode";
            this.txtSortCode.Size = new System.Drawing.Size(301, 20);
            this.txtSortCode.TabIndex = 0;
            // 
            // txtHandNo
            // 
            this.txtHandNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHandNo.Location = new System.Drawing.Point(93, 107);
            this.txtHandNo.Name = "txtHandNo";
            this.txtHandNo.Size = new System.Drawing.Size(301, 20);
            this.txtHandNo.TabIndex = 0;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(25, 333);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(67, 14);
            this.label11.TabIndex = 0;
            this.label11.Text = "创建时间：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(25, 259);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 14);
            this.label8.TabIndex = 0;
            this.label8.Text = "内线电话：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(37, 297);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(55, 14);
            this.label10.TabIndex = 0;
            this.label10.Text = "创建人：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(37, 146);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 14);
            this.label5.TabIndex = 0;
            this.label5.Text = "排序码：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(25, 221);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 14);
            this.label7.TabIndex = 0;
            this.label7.Text = "外线电话：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 14);
            this.label4.TabIndex = 0;
            this.label4.Text = "机构编码：";
            // 
            // txtCategory
            // 
            this.txtCategory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCategory.Location = new System.Drawing.Point(93, 181);
            this.txtCategory.Name = "txtCategory";
            this.txtCategory.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.DropDown)});
            this.txtCategory.Properties.Items.AddRange(new object[] {
            "集团",
            "公司",
            "部门",
            "工作组"});
            this.txtCategory.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.txtCategory.Size = new System.Drawing.Size(301, 20);
            this.txtCategory.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(25, 185);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 14);
            this.label6.TabIndex = 0;
            this.label6.Text = "机构分类：";
            // 
            // groupControl3
            // 
            this.groupControl3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl3.Controls.Add(this.btnRemoveUser);
            this.groupControl3.Controls.Add(this.lvwUser);
            this.groupControl3.Controls.Add(this.btnEditUser);
            this.groupControl3.Location = new System.Drawing.Point(444, 14);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(265, 330);
            this.groupControl3.TabIndex = 4;
            this.groupControl3.Text = "包含用户";
            // 
            // groupControl4
            // 
            this.groupControl4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl4.Controls.Add(this.lvwRole);
            this.groupControl4.Location = new System.Drawing.Point(444, 360);
            this.groupControl4.Name = "groupControl4";
            this.groupControl4.Size = new System.Drawing.Size(265, 358);
            this.groupControl4.TabIndex = 5;
            this.groupControl4.Text = "所属角色";
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "star.ico");
            this.imageList2.Images.SetKeyName(1, "Organ.ico");
            this.imageList2.Images.SetKeyName(2, "usergroup6.ico");
            this.imageList2.Images.SetKeyName(3, "usergroup5.ico");
            this.imageList2.Images.SetKeyName(4, "user005.ico");
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.btnAdd);
            this.splitContainerControl1.Panel1.Controls.Add(this.btnDelete);
            this.splitContainerControl1.Panel1.Controls.Add(this.groupControl1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.groupControl2);
            this.splitContainerControl1.Panel2.Controls.Add(this.groupControl3);
            this.splitContainerControl1.Panel2.Controls.Add(this.groupControl4);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1008, 730);
            this.splitContainerControl1.SplitterPosition = 282;
            this.splitContainerControl1.TabIndex = 6;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // FrmOU
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.splitContainerControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(16, 664);
            this.Name = "FrmOu";
            this.Text = "组织机构管理";
            this.Load += new System.EventHandler(this.FrmOU_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtNote.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreationDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInnerPhone.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreator.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOuterPhone.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSortCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHandNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCategory.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).EndInit();
            this.groupControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ImageList imageList1;
        private DevExpress.XtraEditors.TextEdit txtName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label12;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menu_Delete;
        private System.Windows.Forms.ToolStripMenuItem menu_Add;
        private DevExpress.XtraEditors.MemoEdit txtNote;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripMenuItem menu_Update;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem menu_ExpandAll;
        private System.Windows.Forms.ListBox lvwUser;
        private System.Windows.Forms.ListBox lvwRole;
        private DevExpress.XtraEditors.SimpleButton btnRemoveUser;
        private DevExpress.XtraEditors.SimpleButton btnEditUser;
        private DevExpress.XtraEditors.SimpleButton btnAdd;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private DevExpress.XtraEditors.MemoEdit txtAddress;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private DevExpress.XtraEditors.GroupControl groupControl4;
        private DevExpress.XtraEditors.TextEdit txtHandNo;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.TextEdit txtSortCode;
        private System.Windows.Forms.Label label5;
        private DevExpress.XtraEditors.ComboBoxEdit txtCategory;
        private System.Windows.Forms.Label label6;
        private DevExpress.XtraEditors.TextEdit txtInnerPhone;
        private DevExpress.XtraEditors.TextEdit txtOuterPhone;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private DevExpress.XtraEditors.TextEdit txtCreator;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private DevExpress.XtraEditors.TextEdit txtCreationDate;
        private System.Windows.Forms.ImageList imageList2;
        private System.Windows.Forms.ToolStripMenuItem menu_Collapse;
        private DeptControl cmbUpperOU;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
    }
}