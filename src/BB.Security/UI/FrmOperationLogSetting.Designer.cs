namespace BB.Security.UI
{
    partial class FrmOperationLogSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmOperationLogSetting));
            this.btnAddNew = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.chkForbid = new DevExpress.XtraEditors.CheckEdit();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.txtTableName = new DevExpress.XtraEditors.TextEdit();
            this.txtNote = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.winGridViewPager1 = new BB.BaseUI.Pager.WinGridViewPager();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkForbid.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTableName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNote.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAddNew
            // 
            this.btnAddNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddNew.Location = new System.Drawing.Point(679, 12);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(69, 22);
            this.btnAddNew.StyleController = this.layoutControl1;
            this.btnAddNew.TabIndex = 15;
            this.btnAddNew.Text = "新建";
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.layoutControl1.Controls.Add(this.chkForbid);
            this.layoutControl1.Controls.Add(this.btnSearch);
            this.layoutControl1.Controls.Add(this.btnAddNew);
            this.layoutControl1.Controls.Add(this.txtTableName);
            this.layoutControl1.Controls.Add(this.txtNote);
            this.layoutControl1.Location = new System.Drawing.Point(12, 8);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(70, 185, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(980, 47);
            this.layoutControl1.TabIndex = 12;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // chkForbid
            // 
            this.chkForbid.Location = new System.Drawing.Point(522, 12);
            this.chkForbid.Name = "chkForbid";
            this.chkForbid.Properties.Caption = "禁用";
            this.chkForbid.Size = new System.Drawing.Size(80, 19);
            this.chkForbid.StyleController = this.layoutControl1;
            this.chkForbid.TabIndex = 4;
            this.chkForbid.CheckedChanged += new System.EventHandler(this.chkForbid_CheckedChanged);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Location = new System.Drawing.Point(606, 12);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(69, 22);
            this.btnSearch.StyleController = this.layoutControl1;
            this.btnSearch.TabIndex = 14;
            this.btnSearch.Text = "查询";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtTableName
            // 
            this.txtTableName.Location = new System.Drawing.Point(63, 12);
            this.txtTableName.Name = "txtTableName";
            this.txtTableName.Size = new System.Drawing.Size(200, 20);
            this.txtTableName.StyleController = this.layoutControl1;
            this.txtTableName.TabIndex = 1;
            // 
            // txtNote
            // 
            this.txtNote.Location = new System.Drawing.Point(318, 12);
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(200, 20);
            this.txtNote.StyleController = this.layoutControl1;
            this.txtNote.TabIndex = 2;
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
            this.layoutControlGroup1.Size = new System.Drawing.Size(980, 47);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.txtTableName;
            this.layoutControlItem1.CustomizationFormText = "数据库表";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(255, 24);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(105, 24);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(255, 27);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "数据库表";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(48, 14);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.txtNote;
            this.layoutControlItem2.CustomizationFormText = "备注";
            this.layoutControlItem2.Location = new System.Drawing.Point(255, 0);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(255, 24);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(105, 24);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(255, 27);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "备注";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(48, 14);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.chkForbid;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(510, 0);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(84, 23);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(51, 23);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(84, 27);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnSearch;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(594, 0);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(73, 26);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(39, 26);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(73, 27);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnAddNew;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(667, 0);
            this.layoutControlItem5.MaxSize = new System.Drawing.Size(73, 26);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(39, 26);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(293, 27);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
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
            this.winGridViewPager1.Location = new System.Drawing.Point(12, 61);
            this.winGridViewPager1.MinimumSize = new System.Drawing.Size(540, 0);
            this.winGridViewPager1.Name = "winGridViewPager1";
            this.winGridViewPager1.PrintTitle = "";
            this.winGridViewPager1.ShowCheckBox = false;
            this.winGridViewPager1.ShowExportButton = true;
            this.winGridViewPager1.Size = new System.Drawing.Size(980, 614);
            this.winGridViewPager1.TabIndex = 11;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // FrmOperationLogSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1004, 680);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.winGridViewPager1);
            this.Name = "FrmOperationLogSetting";
            this.Text = "用户操作日志的数据表配置";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkForbid.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTableName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNote.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.SimpleButton btnAddNew;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private BB.BaseUI.Pager.WinGridViewPager winGridViewPager1;

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;


        private DevExpress.XtraEditors.TextEdit txtTableName;     
 
        private DevExpress.XtraEditors.TextEdit txtNote;     
 
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;    
         private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
         private DevExpress.XtraEditors.CheckEdit chkForbid;
         private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
         private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
         private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;    
 
    }
}