namespace BB.BaseUI.Pager
{
    partial class WinGridView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WinGridView));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menu_Add = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Edit = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Refresh = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menu_CopyInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_SetColumn = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Print = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Export = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_ColumnWidth = new System.Windows.Forms.ToolStripMenuItem();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.toolTipController1 = new DevExpress.Utils.ToolTipController(this.components);
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.menu_Add,
            this.menu_Edit,
            this.menu_Delete,
            this.menu_Refresh,
            this.toolStripSeparator2,
            this.menu_CopyInfo,
            this.menu_SetColumn,
            this.menu_Print,
            this.menu_Export,
            this.menu_ColumnWidth});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(228, 278);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(224, 6);
            // 
            // menu_Add
            // 
            this.menu_Add.Image = ((System.Drawing.Image)(resources.GetObject("menu_Add.Image")));
            this.menu_Add.Name = "menu_Add";
            this.menu_Add.Size = new System.Drawing.Size(227, 26);
            this.menu_Add.Text = "新建(&N)";
            this.menu_Add.Click += new System.EventHandler(this.menu_Add_Click);
            // 
            // menu_Edit
            // 
            this.menu_Edit.Image = ((System.Drawing.Image)(resources.GetObject("menu_Edit.Image")));
            this.menu_Edit.Name = "menu_Edit";
            this.menu_Edit.Size = new System.Drawing.Size(227, 26);
            this.menu_Edit.Text = "编辑选定项(&E)";
            this.menu_Edit.Click += new System.EventHandler(this.menu_Edit_Click);
            // 
            // menu_Delete
            // 
            this.menu_Delete.Image = ((System.Drawing.Image)(resources.GetObject("menu_Delete.Image")));
            this.menu_Delete.Name = "menu_Delete";
            this.menu_Delete.Size = new System.Drawing.Size(227, 26);
            this.menu_Delete.Text = "删除选定项(&D)";
            this.menu_Delete.Click += new System.EventHandler(this.menu_Delete_Click);
            // 
            // menu_Refresh
            // 
            this.menu_Refresh.Image = ((System.Drawing.Image)(resources.GetObject("menu_Refresh.Image")));
            this.menu_Refresh.Name = "menu_Refresh";
            this.menu_Refresh.Size = new System.Drawing.Size(227, 26);
            this.menu_Refresh.Text = "刷新列表(&R)";
            this.menu_Refresh.Click += new System.EventHandler(this.menu_Refresh_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(224, 6);
            // 
            // menu_CopyInfo
            // 
            this.menu_CopyInfo.Image = ((System.Drawing.Image)(resources.GetObject("menu_CopyInfo.Image")));
            this.menu_CopyInfo.Name = "menu_CopyInfo";
            this.menu_CopyInfo.Size = new System.Drawing.Size(227, 26);
            this.menu_CopyInfo.Text = "复制选定行信息(&C)";
            this.menu_CopyInfo.Click += new System.EventHandler(this.menu_CopyInfo_Click);
            // 
            // menu_SetColumn
            // 
            this.menu_SetColumn.Image = ((System.Drawing.Image)(resources.GetObject("menu_SetColumn.Image")));
            this.menu_SetColumn.Name = "menu_SetColumn";
            this.menu_SetColumn.Size = new System.Drawing.Size(227, 26);
            this.menu_SetColumn.Text = "设置表格显示列(&S)";
            this.menu_SetColumn.Click += new System.EventHandler(this.menu_SetColumn_Click);
            // 
            // menu_Print
            // 
            this.menu_Print.Image = ((System.Drawing.Image)(resources.GetObject("menu_Print.Image")));
            this.menu_Print.Name = "menu_Print";
            this.menu_Print.Size = new System.Drawing.Size(227, 26);
            this.menu_Print.Text = "打印列表(&P)";
            this.menu_Print.Click += new System.EventHandler(this.menu_Print_Click);
            // 
            // menu_Export
            // 
            this.menu_Export.Image = ((System.Drawing.Image)(resources.GetObject("menu_Export.Image")));
            this.menu_Export.Name = "menu_Export";
            this.menu_Export.Size = new System.Drawing.Size(227, 26);
            this.menu_Export.Text = "导出数据(&E)";
            this.menu_Export.Click += new System.EventHandler(this.menu_Export_Click);
            // 
            // menu_ColumnWidth
            // 
            this.menu_ColumnWidth.Name = "menu_ColumnWidth";
            this.menu_ColumnWidth.Size = new System.Drawing.Size(227, 26);
            this.menu_ColumnWidth.Text = "设置固定宽度显示(&W)";
            this.menu_ColumnWidth.Click += new System.EventHandler(this.menu_ColumnWidth_Click);
            // 
            // gridControl1
            // 
            this.gridControl1.ContextMenuStrip = this.contextMenuStrip1;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(540, 212);
            this.gridControl1.TabIndex = 11;
            this.gridControl1.ToolTipController = this.toolTipController1;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControl1.DataSourceChanged += new System.EventHandler(this.gridView1_DataSourceChanged);
            // 
            // gridView1
            // 
            this.gridView1.Appearance.EvenRow.BackColor = System.Drawing.Color.LightCyan;
            this.gridView1.Appearance.EvenRow.Options.UseBackColor = true;
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.ReadOnly = true;
            this.gridView1.OptionsCustomization.AllowFilter = false;
            this.gridView1.OptionsCustomization.AllowGroup = false;
            this.gridView1.OptionsMenu.EnableColumnMenu = false;
            this.gridView1.OptionsMenu.EnableFooterMenu = false;
            this.gridView1.OptionsMenu.EnableGroupPanelMenu = false;
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView1.OptionsView.EnableAppearanceOddRow = true;
            this.gridView1.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gridView1_CustomDrawRowIndicator);
            // 
            // toolTipController1
            // 
            this.toolTipController1.GetActiveObjectInfo += new DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventHandler(this.toolTipController1_GetActiveObjectInfo);
            // 
            // WinGridView
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.gridControl1);
            this.Name = "WinGridView";
            this.Size = new System.Drawing.Size(540, 212);
            this.Load += new System.EventHandler(this.WinGridView_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem menu_Print;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menu_Export;
        public DevExpress.XtraGrid.GridControl gridControl1;
        public DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.Utils.ToolTipController toolTipController1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem menu_CopyInfo;
        private System.Windows.Forms.ToolStripMenuItem menu_SetColumn;
        public System.Windows.Forms.ToolStripMenuItem menu_Delete;
        public System.Windows.Forms.ToolStripMenuItem menu_Refresh;
        public System.Windows.Forms.ToolStripMenuItem menu_Edit;
        public System.Windows.Forms.ToolStripMenuItem menu_Add;
        private System.Windows.Forms.ToolStripMenuItem menu_ColumnWidth;
    }
}
