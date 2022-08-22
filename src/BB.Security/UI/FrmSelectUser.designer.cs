using BB.BaseUI.Pager;

namespace BB.Security.UI
{
    partial class FrmSelectUser
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("张三", 1, 1);
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("李四", 1, 1);
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("王五", 1, 1);
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("公司本部", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3});
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("总经办");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("综合部");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("财务部");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("工程部");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("采购部");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("运行维护部");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("客户服务部");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("人力资源部");
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("市场营销部");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSelectUser));
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("张三", 1, 1);
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("李四", 1, 1);
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("王五", 1, 1);
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("系统管理员", new System.Windows.Forms.TreeNode[] {
            treeNode14,
            treeNode15,
            treeNode16});
            System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("主管");
            System.Windows.Forms.TreeNode treeNode19 = new System.Windows.Forms.TreeNode("总经理");
            System.Windows.Forms.TreeNode treeNode20 = new System.Windows.Forms.TreeNode("部门经理");
            System.Windows.Forms.TreeNode treeNode21 = new System.Windows.Forms.TreeNode("部门副经理");
            System.Windows.Forms.TreeNode treeNode22 = new System.Windows.Forms.TreeNode("会计");
            System.Windows.Forms.TreeNode treeNode23 = new System.Windows.Forms.TreeNode("技术总监");
            System.Windows.Forms.TreeNode treeNode24 = new System.Windows.Forms.TreeNode("财务部经理");
            System.Windows.Forms.TreeNode treeNode25 = new System.Windows.Forms.TreeNode("运维部经理");
            System.Windows.Forms.TreeNode treeNode26 = new System.Windows.Forms.TreeNode("市场经理");
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.tabDept = new DevExpress.XtraTab.XtraTabPage();
            this.treeDept = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tabRole = new DevExpress.XtraTab.XtraTabPage();
            this.treeRole = new System.Windows.Forms.TreeView();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnClearAll = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.btnAddUser = new DevExpress.XtraEditors.SimpleButton();
            this.winGridView1 = new WinGridView();
            this.lblItemCount = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.tabDept.SuspendLayout();
            this.tabRole.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.tabDept;
            this.xtraTabControl1.Size = new System.Drawing.Size(275, 530);
            this.xtraTabControl1.TabIndex = 0;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabDept,
            this.tabRole});
            // 
            // tabDept
            // 
            this.tabDept.Controls.Add(this.treeDept);
            this.tabDept.Name = "tabDept";
            this.tabDept.Size = new System.Drawing.Size(269, 501);
            this.tabDept.Text = "按部门";
            // 
            // treeDept
            // 
            this.treeDept.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeDept.HideSelection = false;
            this.treeDept.ImageIndex = 0;
            this.treeDept.ImageList = this.imageList1;
            this.treeDept.Location = new System.Drawing.Point(0, 0);
            this.treeDept.Name = "treeDept";
            treeNode1.ImageIndex = 1;
            treeNode1.Name = "节点11";
            treeNode1.SelectedImageIndex = 1;
            treeNode1.Text = "张三";
            treeNode2.ImageIndex = 1;
            treeNode2.Name = "节点12";
            treeNode2.SelectedImageIndex = 1;
            treeNode2.Text = "李四";
            treeNode3.ImageIndex = 1;
            treeNode3.Name = "节点13";
            treeNode3.SelectedImageIndex = 1;
            treeNode3.Text = "王五";
            treeNode4.Name = "节点0";
            treeNode4.Text = "公司本部";
            treeNode5.Name = "节点1";
            treeNode5.Text = "总经办";
            treeNode6.Name = "节点2";
            treeNode6.Text = "综合部";
            treeNode7.Name = "节点3";
            treeNode7.Text = "财务部";
            treeNode8.Name = "节点4";
            treeNode8.Text = "工程部";
            treeNode9.Name = "节点5";
            treeNode9.Text = "采购部";
            treeNode10.Name = "节点6";
            treeNode10.Text = "运行维护部";
            treeNode11.Name = "节点7";
            treeNode11.Text = "客户服务部";
            treeNode12.Name = "节点8";
            treeNode12.Text = "人力资源部";
            treeNode13.Name = "节点9";
            treeNode13.Text = "市场营销部";
            this.treeDept.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode5,
            treeNode6,
            treeNode7,
            treeNode8,
            treeNode9,
            treeNode10,
            treeNode11,
            treeNode12,
            treeNode13});
            this.treeDept.SelectedImageIndex = 0;
            this.treeDept.Size = new System.Drawing.Size(269, 501);
            this.treeDept.TabIndex = 0;
            this.treeDept.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeDept_AfterSelect);
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
            this.imageList1.Images.SetKeyName(5, "rolekey.ico");
            // 
            // tabRole
            // 
            this.tabRole.Controls.Add(this.treeRole);
            this.tabRole.Name = "tabRole";
            this.tabRole.Size = new System.Drawing.Size(269, 501);
            this.tabRole.Text = "按角色";
            // 
            // treeRole
            // 
            this.treeRole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeRole.HideSelection = false;
            this.treeRole.ImageIndex = 0;
            this.treeRole.ImageList = this.imageList1;
            this.treeRole.Location = new System.Drawing.Point(0, 0);
            this.treeRole.Name = "treeRole";
            treeNode14.ImageIndex = 1;
            treeNode14.Name = "节点11";
            treeNode14.SelectedImageIndex = 1;
            treeNode14.Text = "张三";
            treeNode15.ImageIndex = 1;
            treeNode15.Name = "节点12";
            treeNode15.SelectedImageIndex = 1;
            treeNode15.Text = "李四";
            treeNode16.ImageIndex = 1;
            treeNode16.Name = "节点13";
            treeNode16.SelectedImageIndex = 1;
            treeNode16.Text = "王五";
            treeNode17.Name = "节点0";
            treeNode17.Text = "系统管理员";
            treeNode18.Name = "节点1";
            treeNode18.Text = "主管";
            treeNode19.Name = "节点2";
            treeNode19.Text = "总经理";
            treeNode20.Name = "节点3";
            treeNode20.Text = "部门经理";
            treeNode21.Name = "节点4";
            treeNode21.Text = "部门副经理";
            treeNode22.Name = "节点5";
            treeNode22.Text = "会计";
            treeNode23.Name = "节点6";
            treeNode23.Text = "技术总监";
            treeNode24.Name = "节点7";
            treeNode24.Text = "财务部经理";
            treeNode25.Name = "节点8";
            treeNode25.Text = "运维部经理";
            treeNode26.Name = "节点9";
            treeNode26.Text = "市场经理";
            this.treeRole.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode17,
            treeNode18,
            treeNode19,
            treeNode20,
            treeNode21,
            treeNode22,
            treeNode23,
            treeNode24,
            treeNode25,
            treeNode26});
            this.treeRole.SelectedImageIndex = 0;
            this.treeRole.Size = new System.Drawing.Size(269, 501);
            this.treeRole.TabIndex = 1;
            this.treeRole.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeRole_AfterSelect);
            // 
            // panelControl1
            // 
            this.panelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl1.Controls.Add(this.flowLayoutPanel1);
            this.panelControl1.Location = new System.Drawing.Point(4, 542);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(886, 99);
            this.panelControl1.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(2, 2);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(882, 95);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(625, 647);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "完成选择";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnClearAll
            // 
            this.btnClearAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearAll.Location = new System.Drawing.Point(717, 647);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new System.Drawing.Size(75, 23);
            this.btnClearAll.TabIndex = 2;
            this.btnClearAll.Text = "清空";
            this.btnClearAll.Click += new System.EventHandler(this.btnClearAll_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(815, 647);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "关闭";
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerControl1.Location = new System.Drawing.Point(4, 6);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.xtraTabControl1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.btnAddUser);
            this.splitContainerControl1.Panel2.Controls.Add(this.winGridView1);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(886, 530);
            this.splitContainerControl1.SplitterPosition = 275;
            this.splitContainerControl1.TabIndex = 3;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // btnAddUser
            // 
            this.btnAddUser.Location = new System.Drawing.Point(15, 6);
            this.btnAddUser.Name = "btnAddUser";
            this.btnAddUser.Size = new System.Drawing.Size(75, 23);
            this.btnAddUser.TabIndex = 1;
            this.btnAddUser.Text = "添加选择";
            this.btnAddUser.Click += new System.EventHandler(this.btnAddUser_Click);
            // 
            // winGridView1
            // 
            this.winGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.winGridView1.AppendedMenu = null;
            this.winGridView1.ColumnNameAlias = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("winGridView1.ColumnNameAlias")));
            this.winGridView1.DataSource = null;
            this.winGridView1.DisplayColumns = "";
            this.winGridView1.Location = new System.Drawing.Point(1, 35);
            this.winGridView1.Name = "winGridView1";
            this.winGridView1.PrintTitle = "";
            this.winGridView1.ShowCheckBox = false;
            this.winGridView1.ShowExportButton = true;
            this.winGridView1.Size = new System.Drawing.Size(605, 492);
            this.winGridView1.TabIndex = 0;
            // 
            // lblItemCount
            // 
            this.lblItemCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblItemCount.Location = new System.Drawing.Point(6, 648);
            this.lblItemCount.Name = "lblItemCount";
            this.lblItemCount.Size = new System.Drawing.Size(103, 14);
            this.lblItemCount.TabIndex = 4;
            this.lblItemCount.Text = "当前选择【0】项目";
            // 
            // FrmSelectUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(897, 682);
            this.Controls.Add(this.lblItemCount);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnClearAll);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.panelControl1);
            this.Name = "FrmSelectUser";
            this.Text = "选择人员";
            this.Load += new System.EventHandler(this.FrmSelectFlowUser_Load);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.tabDept.ResumeLayout(false);
            this.tabRole.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage tabDept;
        private DevExpress.XtraTab.XtraTabPage tabRole;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnClearAll;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private System.Windows.Forms.TreeView treeDept;
        private System.Windows.Forms.TreeView treeRole;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private WinGridView winGridView1;
        private DevExpress.XtraEditors.SimpleButton btnAddUser;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private DevExpress.XtraEditors.LabelControl lblItemCount;
    }
}