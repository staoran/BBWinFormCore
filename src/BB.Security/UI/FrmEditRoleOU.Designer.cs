namespace BB.Security.UI
{
    partial class FrmEditRoleOU
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEditRoleOU));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.chkAll = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkAll.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl1.Controls.Add(this.treeView1);
            this.groupControl1.Location = new System.Drawing.Point(12, 3);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(460, 609);
            this.groupControl1.TabIndex = 3;
            this.groupControl1.Text = "组织机构列表";
            // 
            // treeView1
            // 
            this.treeView1.CheckBoxes = true;
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
            this.treeView1.Size = new System.Drawing.Size(456, 585);
            this.treeView1.TabIndex = 0;
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
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(392, 627);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取消";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(311, 627);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // chkAll
            // 
            this.chkAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkAll.Location = new System.Drawing.Point(12, 625);
            this.chkAll.Name = "chkAll";
            this.chkAll.Properties.Caption = "全选";
            this.chkAll.Size = new System.Drawing.Size(50, 19);
            this.chkAll.TabIndex = 6;
            this.chkAll.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
            // 
            // FrmEditRoleOU
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(481, 662);
            this.Controls.Add(this.chkAll);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupControl1);
            this.Name = "FrmEditRoleOU";
            this.Text = "角色包含机构";
            this.Load += new System.EventHandler(this.FrmEditRoleOU_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkAll.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ImageList imageList1;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        public DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.CheckEdit chkAll;
    }
}