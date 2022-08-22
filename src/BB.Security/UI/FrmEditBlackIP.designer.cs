namespace BB.Security.UI
{
    partial class FrmEditBlackIp
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.txtIPStart = new DevExpress.XtraEditors.TextEdit();
            this.txtIPEnd = new DevExpress.XtraEditors.TextEdit();
            this.txtCreator = new DevExpress.XtraEditors.TextEdit();
            this.txtCreationDate = new DevExpress.XtraEditors.DateEdit();
            this.txtAuthorizeType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtNote = new DevExpress.XtraEditors.MemoEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.txtForbid = new DevExpress.XtraEditors.CheckEdit();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.btnRemoveUser = new DevExpress.XtraEditors.SimpleButton();
            this.lvwUser = new System.Windows.Forms.ListBox();
            this.btnEditUser = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.picPrint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIPStart.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIPEnd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreator.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreationDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreationDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAuthorizeType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNote.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtForbid.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(585, 478);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(684, 478);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(498, 478);
            // 
            // dataNavigator1
            // 
            this.dataNavigator1.Location = new System.Drawing.Point(12, 473);
            // 
            // picPrint
            // 
            this.picPrint.Location = new System.Drawing.Point(205, 478);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.layoutControl1.Appearance.ControlReadOnly.BackColor = System.Drawing.Color.SeaShell;
            this.layoutControl1.Appearance.ControlReadOnly.Options.UseBackColor = true;
            this.layoutControl1.Controls.Add(this.txtName);
            this.layoutControl1.Controls.Add(this.txtIPStart);
            this.layoutControl1.Controls.Add(this.txtIPEnd);
            this.layoutControl1.Controls.Add(this.txtCreator);
            this.layoutControl1.Controls.Add(this.txtCreationDate);
            this.layoutControl1.Controls.Add(this.txtAuthorizeType);
            this.layoutControl1.Controls.Add(this.txtNote);
            this.layoutControl1.Location = new System.Drawing.Point(12, 8);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(459, 419);
            this.layoutControl1.TabIndex = 6;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(91, 12);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(356, 20);
            this.txtName.StyleController = this.layoutControl1;
            this.txtName.TabIndex = 1;
            // 
            // txtIPStart
            // 
            this.txtIPStart.Location = new System.Drawing.Point(91, 60);
            this.txtIPStart.Name = "txtIPStart";
            this.txtIPStart.Size = new System.Drawing.Size(356, 20);
            this.txtIPStart.StyleController = this.layoutControl1;
            this.txtIPStart.TabIndex = 6;
            // 
            // txtIPEnd
            // 
            this.txtIPEnd.Location = new System.Drawing.Point(91, 84);
            this.txtIPEnd.Name = "txtIPEnd";
            this.txtIPEnd.Size = new System.Drawing.Size(356, 20);
            this.txtIPEnd.StyleController = this.layoutControl1;
            this.txtIPEnd.TabIndex = 7;
            // 
            // txtCreator
            // 
            this.txtCreator.Location = new System.Drawing.Point(91, 387);
            this.txtCreator.Name = "txtCreator";
            this.txtCreator.Properties.ReadOnly = true;
            this.txtCreator.Size = new System.Drawing.Size(137, 20);
            this.txtCreator.StyleController = this.layoutControl1;
            this.txtCreator.TabIndex = 9;
            // 
            // txtCreationDate
            // 
            this.txtCreationDate.EditValue = null;
            this.txtCreationDate.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtCreationDate.Location = new System.Drawing.Point(311, 387);
            this.txtCreationDate.Name = "txtCreationDate";
            this.txtCreationDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtCreationDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtCreationDate.Properties.CalendarTimeProperties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.txtCreationDate.Properties.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.txtCreationDate.Properties.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm";
            this.txtCreationDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtCreationDate.Properties.EditFormat.FormatString = "yyyy-MM-dd HH:mm";
            this.txtCreationDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtCreationDate.Properties.Mask.EditMask = "yyyy-MM-dd HH:mm";
            this.txtCreationDate.Properties.ReadOnly = true;
            this.txtCreationDate.Size = new System.Drawing.Size(136, 20);
            this.txtCreationDate.StyleController = this.layoutControl1;
            this.txtCreationDate.TabIndex = 11;
            // 
            // txtAuthorizeType
            // 
            this.txtAuthorizeType.EditValue = "";
            this.txtAuthorizeType.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtAuthorizeType.Location = new System.Drawing.Point(91, 36);
            this.txtAuthorizeType.Name = "txtAuthorizeType";
            this.txtAuthorizeType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.DropDown)});
            this.txtAuthorizeType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.txtAuthorizeType.Size = new System.Drawing.Size(356, 20);
            this.txtAuthorizeType.StyleController = this.layoutControl1;
            this.txtAuthorizeType.TabIndex = 4;
            // 
            // txtNote
            // 
            this.txtNote.Location = new System.Drawing.Point(91, 108);
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(356, 275);
            this.txtNote.StyleController = this.layoutControl1;
            this.txtNote.TabIndex = 8;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem6,
            this.layoutControlItem7,
            this.layoutControlItem8,
            this.layoutControlItem9,
            this.layoutControlItem11,
            this.layoutControlItem4});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(459, 419);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.txtName;
            this.layoutControlItem1.CustomizationFormText = "显示名称";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(439, 24);
            this.layoutControlItem1.Text = "显示名称(*)";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(76, 14);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.txtIPStart;
            this.layoutControlItem6.CustomizationFormText = "IP起始地址";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(439, 24);
            this.layoutControlItem6.Text = "IP起始地址(*)";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(76, 14);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.txtIPEnd;
            this.layoutControlItem7.CustomizationFormText = "IP结束地址";
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(439, 24);
            this.layoutControlItem7.Text = "IP结束地址(*)";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(76, 14);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.txtNote;
            this.layoutControlItem8.CustomizationFormText = "备注";
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 96);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(439, 279);
            this.layoutControlItem8.Text = "备注说明";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(76, 14);
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.txtCreator;
            this.layoutControlItem9.CustomizationFormText = "创建人";
            this.layoutControlItem9.Location = new System.Drawing.Point(0, 375);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(220, 24);
            this.layoutControlItem9.Text = "创建人";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(76, 14);
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.txtCreationDate;
            this.layoutControlItem11.CustomizationFormText = "创建时间";
            this.layoutControlItem11.Location = new System.Drawing.Point(220, 375);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(219, 24);
            this.layoutControlItem11.Text = "创建时间";
            this.layoutControlItem11.TextSize = new System.Drawing.Size(76, 14);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.txtAuthorizeType;
            this.layoutControlItem4.CustomizationFormText = "授权类型";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(439, 24);
            this.layoutControlItem4.Text = "授权类型(*)";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(76, 14);
            // 
            // txtForbid
            // 
            this.txtForbid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtForbid.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtForbid.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtForbid.Location = new System.Drawing.Point(12, 435);
            this.txtForbid.Name = "txtForbid";
            this.txtForbid.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.txtForbid.Properties.Appearance.ForeColor = System.Drawing.Color.Red;
            this.txtForbid.Properties.Appearance.Options.UseFont = true;
            this.txtForbid.Properties.Appearance.Options.UseForeColor = true;
            this.txtForbid.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.txtForbid.Properties.Caption = "是否禁用";
            this.txtForbid.Size = new System.Drawing.Size(93, 19);
            this.txtForbid.TabIndex = 5;
            // 
            // groupControl3
            // 
            this.groupControl3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl3.Controls.Add(this.btnRemoveUser);
            this.groupControl3.Controls.Add(this.lvwUser);
            this.groupControl3.Controls.Add(this.btnEditUser);
            this.groupControl3.Location = new System.Drawing.Point(494, 8);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(265, 419);
            this.groupControl3.TabIndex = 7;
            this.groupControl3.Text = "包含用户";
            // 
            // btnRemoveUser
            // 
            this.btnRemoveUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveUser.Location = new System.Drawing.Point(172, 384);
            this.btnRemoveUser.Name = "btnRemoveUser";
            this.btnRemoveUser.Size = new System.Drawing.Size(75, 23);
            this.btnRemoveUser.TabIndex = 1;
            this.btnRemoveUser.Text = "移除用户";
            this.btnRemoveUser.Click += new System.EventHandler(this.btnRemoveUser_Click);
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
            this.lvwUser.Size = new System.Drawing.Size(259, 340);
            this.lvwUser.TabIndex = 3;
            // 
            // btnEditUser
            // 
            this.btnEditUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditUser.Location = new System.Drawing.Point(75, 384);
            this.btnEditUser.Name = "btnEditUser";
            this.btnEditUser.Size = new System.Drawing.Size(75, 23);
            this.btnEditUser.TabIndex = 0;
            this.btnEditUser.Text = "添加用户";
            this.btnEditUser.Click += new System.EventHandler(this.btnEditUser_Click);
            // 
            // FrmEditBlackIP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(771, 513);
            this.Controls.Add(this.groupControl3);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.txtForbid);
            this.Name = "FrmEditBlackIp";
            this.Text = "登陆系统的黑白名单列表";
            this.Load += new System.EventHandler(this.FrmEditBlackIP_Load);
            this.Controls.SetChildIndex(this.txtForbid, 0);
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.btnOK, 0);
            this.Controls.SetChildIndex(this.btnAdd, 0);
            this.Controls.SetChildIndex(this.dataNavigator1, 0);
            this.Controls.SetChildIndex(this.picPrint, 0);
            this.Controls.SetChildIndex(this.groupControl3, 0);
            ((System.ComponentModel.ISupportInitialize)(this.picPrint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIPStart.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIPEnd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreator.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreationDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreationDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAuthorizeType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNote.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtForbid.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txtName;
          private DevExpress.XtraEditors.TextEdit txtIPStart;
          private DevExpress.XtraEditors.TextEdit txtIPEnd;
          private DevExpress.XtraEditors.TextEdit txtCreator;

          private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
         private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;    
         private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;    
         private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;    
         private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
         private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
         private DevExpress.XtraEditors.ComboBoxEdit txtAuthorizeType;
         private DevExpress.XtraEditors.DateEdit txtCreationDate;
         private DevExpress.XtraEditors.MemoEdit txtNote;
         private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
         private DevExpress.XtraEditors.CheckEdit txtForbid;
         private DevExpress.XtraEditors.GroupControl groupControl3;
         private DevExpress.XtraEditors.SimpleButton btnRemoveUser;
         private System.Windows.Forms.ListBox lvwUser;
         private DevExpress.XtraEditors.SimpleButton btnEditUser;    
 
    }
}