namespace BB.BaseUI.Pager
{
    partial class WinListViewPager
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menu_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Refresh = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Print = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.listView1 = new System.Windows.Forms.ListView();
            this.pager = new Pager();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.menu_Delete,
            this.menu_Refresh,
            this.menu_Print,
            this.toolStripSeparator2});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(149, 82);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(145, 6);
            // 
            // menu_Delete
            // 
            this.menu_Delete.Name = "menu_Delete";
            this.menu_Delete.Size = new System.Drawing.Size(148, 22);
            this.menu_Delete.Text = "删除选定项(&D)";
            this.menu_Delete.Click += new System.EventHandler(this.menu_Delete_Click);
            // 
            // menu_Refresh
            // 
            this.menu_Refresh.Name = "menu_Refresh";
            this.menu_Refresh.Size = new System.Drawing.Size(148, 22);
            this.menu_Refresh.Text = "刷新列表(&R)";
            this.menu_Refresh.Click += new System.EventHandler(this.menu_Refresh_Click);
            // 
            // menu_Print
            // 
            this.menu_Print.Name = "menu_Print";
            this.menu_Print.Size = new System.Drawing.Size(148, 22);
            this.menu_Print.Text = "打印列表(&P)";
            this.menu_Print.Click += new System.EventHandler(this.menu_Print_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(145, 6);
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.ContextMenuStrip = this.contextMenuStrip1;
            this.listView1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(578, 179);
            this.listView1.TabIndex = 6;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // pager
            // 
            this.pager.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pager.CurrentPageIndex = 1;
            this.pager.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pager.Location = new System.Drawing.Point(3, 178);
            this.pager.Name = "pager";
            this.pager.PageSize = 20;
            this.pager.RecordCount = 0;
            this.pager.Size = new System.Drawing.Size(572, 31);
            this.pager.TabIndex = 4;
            // 
            // WinListViewPager
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.pager);
            this.MinimumSize = new System.Drawing.Size(540, 0);
            this.Name = "WinListViewPager";
            this.Size = new System.Drawing.Size(578, 212);
            this.Load += new System.EventHandler(this.WinListViewPager_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Pager pager;
        private System.Windows.Forms.ToolStripMenuItem menu_Delete;
        private System.Windows.Forms.ToolStripMenuItem menu_Refresh;
        private System.Windows.Forms.ToolStripMenuItem menu_Print;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        public System.Windows.Forms.ListView listView1;
        public System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    }
}
