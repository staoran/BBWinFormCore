using BB.BaseUI.Control.Security;

namespace BB.Security.UI
{
    partial class FrmFunction
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("节点2", 1, 1);
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("节点1", 1, 1, new System.Windows.Forms.TreeNode[] {
            treeNode1});
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("节点0", new System.Windows.Forms.TreeNode[] {
            treeNode2});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFunction));
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menu_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_DeletAll = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Add = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Update = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menu_ExpandAll = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Collapse = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.lblSystemType = new System.Windows.Forms.Label();
            this.txtFunctionID = new DevExpress.XtraEditors.TextEdit();
            this.label5 = new System.Windows.Forms.Label();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lvwRole = new System.Windows.Forms.ListBox();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.functionControl1 = new FunctionControl();
            this.txtSortCode = new DevExpress.XtraEditors.TextEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSystemType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.btnBatchAdd = new DevExpress.XtraEditors.SimpleButton();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFunctionID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSortCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSystemType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
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
            this.treeView1.Location = new System.Drawing.Point(2, 22);
            this.treeView1.Name = "treeView1";
            treeNode1.ImageIndex = 1;
            treeNode1.Name = "节点2";
            treeNode1.SelectedImageIndex = 1;
            treeNode1.Text = "节点2";
            treeNode2.ImageIndex = 1;
            treeNode2.Name = "节点1";
            treeNode2.SelectedImageIndex = 1;
            treeNode2.Text = "节点1";
            treeNode3.Name = "节点0";
            treeNode3.Text = "节点0";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode3});
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(313, 648);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.treeView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_Delete,
            this.menu_DeletAll,
            this.menu_Add,
            this.menu_Update,
            this.toolStripSeparator1,
            this.menu_ExpandAll,
            this.menu_Collapse});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(165, 142);
            // 
            // menu_Delete
            // 
            this.menu_Delete.Image = ((System.Drawing.Image)(resources.GetObject("menu_Delete.Image")));
            this.menu_Delete.Name = "menu_Delete";
            this.menu_Delete.Size = new System.Drawing.Size(164, 22);
            this.menu_Delete.Text = "删除(&D)";
            this.menu_Delete.Click += new System.EventHandler(this.menu_Delete_Click);
            // 
            // menu_DeletAll
            // 
            this.menu_DeletAll.Image = ((System.Drawing.Image)(resources.GetObject("menu_DeletAll.Image")));
            this.menu_DeletAll.Name = "menu_DeletAll";
            this.menu_DeletAll.Size = new System.Drawing.Size(164, 22);
            this.menu_DeletAll.Text = "级联删除(&R)";
            this.menu_DeletAll.Click += new System.EventHandler(this.menu_DeletAll_Click);
            // 
            // menu_Add
            // 
            this.menu_Add.Image = ((System.Drawing.Image)(resources.GetObject("menu_Add.Image")));
            this.menu_Add.Name = "menu_Add";
            this.menu_Add.Size = new System.Drawing.Size(164, 22);
            this.menu_Add.Text = "添加(&A)";
            this.menu_Add.Click += new System.EventHandler(this.menu_Add_Click);
            // 
            // menu_Update
            // 
            this.menu_Update.Image = ((System.Drawing.Image)(resources.GetObject("menu_Update.Image")));
            this.menu_Update.Name = "menu_Update";
            this.menu_Update.Size = new System.Drawing.Size(164, 22);
            this.menu_Update.Text = "刷新列表(&U)";
            this.menu_Update.Click += new System.EventHandler(this.menu_Update_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(161, 6);
            // 
            // menu_ExpandAll
            // 
            this.menu_ExpandAll.Image = ((System.Drawing.Image)(resources.GetObject("menu_ExpandAll.Image")));
            this.menu_ExpandAll.Name = "menu_ExpandAll";
            this.menu_ExpandAll.Size = new System.Drawing.Size(164, 22);
            this.menu_ExpandAll.Text = "展开全部子节点";
            this.menu_ExpandAll.Click += new System.EventHandler(this.menu_ExpandAll_Click);
            // 
            // menu_Collapse
            // 
            this.menu_Collapse.Image = ((System.Drawing.Image)(resources.GetObject("menu_Collapse.Image")));
            this.menu_Collapse.Name = "menu_Collapse";
            this.menu_Collapse.Size = new System.Drawing.Size(164, 22);
            this.menu_Collapse.Text = "折叠全部节点(&C)";
            this.menu_Collapse.Click += new System.EventHandler(this.menu_Collapse_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "0036.ICO");
            this.imageList1.Images.SetKeyName(1, "key.ico");
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(568, 172);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "保存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblSystemType
            // 
            this.lblSystemType.AutoSize = true;
            this.lblSystemType.Location = new System.Drawing.Point(14, 150);
            this.lblSystemType.Name = "lblSystemType";
            this.lblSystemType.Size = new System.Drawing.Size(108, 14);
            this.lblSystemType.TabIndex = 0;
            this.lblSystemType.Text = "系统类型编号(*)：";
            this.lblSystemType.Visible = false;
            // 
            // txtFunctionID
            // 
            this.txtFunctionID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFunctionID.Location = new System.Drawing.Point(128, 92);
            this.txtFunctionID.Name = "txtFunctionID";
            this.txtFunctionID.Size = new System.Drawing.Size(515, 20);
            this.txtFunctionID.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(26, 96);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 14);
            this.label5.TabIndex = 0;
            this.label5.Text = "功能控件ID(*)：";
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(128, 36);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(515, 20);
            this.txtName.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 14);
            this.label2.TabIndex = 0;
            this.label2.Text = "上层功能(*)：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "功能名称(*)：";
            // 
            // lvwRole
            // 
            this.lvwRole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwRole.FormattingEnabled = true;
            this.lvwRole.ItemHeight = 14;
            this.lvwRole.Location = new System.Drawing.Point(2, 22);
            this.lvwRole.Name = "lvwRole";
            this.lvwRole.Size = new System.Drawing.Size(653, 451);
            this.lvwRole.TabIndex = 4;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(16, 14);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(61, 23);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "添加";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(90, 14);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(61, 23);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.Text = "删除";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // groupControl1
            // 
            this.groupControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl1.Controls.Add(this.functionControl1);
            this.groupControl1.Controls.Add(this.txtSortCode);
            this.groupControl1.Controls.Add(this.label3);
            this.groupControl1.Controls.Add(this.btnSave);
            this.groupControl1.Controls.Add(this.label1);
            this.groupControl1.Controls.Add(this.label2);
            this.groupControl1.Controls.Add(this.txtName);
            this.groupControl1.Controls.Add(this.lblSystemType);
            this.groupControl1.Controls.Add(this.label5);
            this.groupControl1.Controls.Add(this.txtFunctionID);
            this.groupControl1.Controls.Add(this.txtSystemType);
            this.groupControl1.Location = new System.Drawing.Point(8, 14);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(657, 213);
            this.groupControl1.TabIndex = 2;
            this.groupControl1.Text = "功能详细信息";
            // 
            // functionControl1
            // 
            this.functionControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.functionControl1.Location = new System.Drawing.Point(128, 62);
            this.functionControl1.Name = "functionControl1";
            this.functionControl1.Size = new System.Drawing.Size(515, 20);
            this.functionControl1.TabIndex = 6;
            this.functionControl1.Value = "-1";
            // 
            // txtSortCode
            // 
            this.txtSortCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSortCode.Location = new System.Drawing.Point(128, 119);
            this.txtSortCode.Name = "txtSortCode";
            this.txtSortCode.Size = new System.Drawing.Size(515, 20);
            this.txtSortCode.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(67, 122);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 14);
            this.label3.TabIndex = 4;
            this.label3.Text = "排序码：";
            // 
            // txtSystemType
            // 
            this.txtSystemType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSystemType.Location = new System.Drawing.Point(128, 146);
            this.txtSystemType.Name = "txtSystemType";
            this.txtSystemType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.DropDown)});
            this.txtSystemType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.txtSystemType.Size = new System.Drawing.Size(515, 20);
            this.txtSystemType.TabIndex = 2;
            this.txtSystemType.Visible = false;
            // 
            // groupControl2
            // 
            this.groupControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl2.Controls.Add(this.treeView1);
            this.groupControl2.Location = new System.Drawing.Point(3, 53);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(317, 672);
            this.groupControl2.TabIndex = 3;
            this.groupControl2.Text = "功能列表";
            // 
            // groupControl3
            // 
            this.groupControl3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl3.Controls.Add(this.lvwRole);
            this.groupControl3.Location = new System.Drawing.Point(8, 248);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(657, 475);
            this.groupControl3.TabIndex = 4;
            this.groupControl3.Text = "可操作角色";
            // 
            // btnBatchAdd
            // 
            this.btnBatchAdd.Location = new System.Drawing.Point(167, 14);
            this.btnBatchAdd.Name = "btnBatchAdd";
            this.btnBatchAdd.Size = new System.Drawing.Size(75, 23);
            this.btnBatchAdd.TabIndex = 5;
            this.btnBatchAdd.Text = "批量添加";
            this.btnBatchAdd.Click += new System.EventHandler(this.btnBatchAdd_Click);
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.groupControl2);
            this.splitContainerControl1.Panel1.Controls.Add(this.btnAdd);
            this.splitContainerControl1.Panel1.Controls.Add(this.btnBatchAdd);
            this.splitContainerControl1.Panel1.Controls.Add(this.btnDelete);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.groupControl1);
            this.splitContainerControl1.Panel2.Controls.Add(this.groupControl3);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1008, 730);
            this.splitContainerControl1.SplitterPosition = 326;
            this.splitContainerControl1.TabIndex = 6;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // FrmFunction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "FrmFunction";
            this.Text = "功能管理";
            this.Load += new System.EventHandler(this.FrmFunction_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtFunctionID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSortCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSystemType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
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
        private DevExpress.XtraEditors.TextEdit txtFunctionID;
        private System.Windows.Forms.Label label5;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menu_Delete;
        private System.Windows.Forms.ToolStripMenuItem menu_Add;
        private System.Windows.Forms.ToolStripMenuItem menu_Update;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem menu_ExpandAll;
        private System.Windows.Forms.ListBox lvwRole;
        private DevExpress.XtraEditors.SimpleButton btnAdd;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private System.Windows.Forms.Label lblSystemType;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private DevExpress.XtraEditors.SimpleButton btnBatchAdd;
        private DevExpress.XtraEditors.ComboBoxEdit txtSystemType;
        private DevExpress.XtraEditors.TextEdit txtSortCode;
        private System.Windows.Forms.Label label3;
        private FunctionControl functionControl1;
        private System.Windows.Forms.ToolStripMenuItem menu_Collapse;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private System.Windows.Forms.ToolStripMenuItem menu_DeletAll;
    }
}