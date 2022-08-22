namespace BB.Security.UI
{
    partial class FrmEditOperationLog
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
            this.txtUser_ID = new DevExpress.XtraEditors.TextEdit();
            this.txtLoginName = new DevExpress.XtraEditors.TextEdit();
            this.txtFullName = new DevExpress.XtraEditors.TextEdit();
            this.txtCompany_ID = new DevExpress.XtraEditors.TextEdit();
            this.txtCompanyName = new DevExpress.XtraEditors.TextEdit();
            this.txtTableName = new DevExpress.XtraEditors.TextEdit();
            this.txtOperationType = new DevExpress.XtraEditors.TextEdit();
            this.txtCreationDate = new DevExpress.XtraEditors.DateEdit();
            this.txtNote = new DevExpress.XtraEditors.MemoEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.txtIPAddress = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.txtMacAddress = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.picPrint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtUser_ID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLoginName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFullName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCompany_ID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCompanyName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTableName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOperationType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreationDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreationDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNote.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIPAddress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMacAddress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(553, 476);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(652, 476);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(466, 476);
            // 
            // dataNavigator1
            // 
            this.dataNavigator1.Location = new System.Drawing.Point(12, 471);
            // 
            // picPrint
            // 
            this.picPrint.Location = new System.Drawing.Point(205, 476);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.layoutControl1.Appearance.ControlReadOnly.BackColor = System.Drawing.Color.SeaShell;
            this.layoutControl1.Appearance.ControlReadOnly.Options.UseBackColor = true;
            this.layoutControl1.Controls.Add(this.txtMacAddress);
            this.layoutControl1.Controls.Add(this.txtIPAddress);
            this.layoutControl1.Controls.Add(this.txtUser_ID);
            this.layoutControl1.Controls.Add(this.txtLoginName);
            this.layoutControl1.Controls.Add(this.txtFullName);
            this.layoutControl1.Controls.Add(this.txtCompany_ID);
            this.layoutControl1.Controls.Add(this.txtCompanyName);
            this.layoutControl1.Controls.Add(this.txtTableName);
            this.layoutControl1.Controls.Add(this.txtOperationType);
            this.layoutControl1.Controls.Add(this.txtCreationDate);
            this.layoutControl1.Controls.Add(this.txtNote);
            this.layoutControl1.Location = new System.Drawing.Point(12, 8);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(720, 447);
            this.layoutControl1.TabIndex = 6;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // txtUser_ID
            // 
            this.txtUser_ID.Location = new System.Drawing.Point(87, 12);
            this.txtUser_ID.Name = "txtUser_ID";
            this.txtUser_ID.Properties.ReadOnly = true;
            this.txtUser_ID.Size = new System.Drawing.Size(266, 20);
            this.txtUser_ID.StyleController = this.layoutControl1;
            this.txtUser_ID.TabIndex = 1;
            // 
            // txtLoginName
            // 
            this.txtLoginName.Location = new System.Drawing.Point(87, 36);
            this.txtLoginName.Name = "txtLoginName";
            this.txtLoginName.Properties.ReadOnly = true;
            this.txtLoginName.Size = new System.Drawing.Size(266, 20);
            this.txtLoginName.StyleController = this.layoutControl1;
            this.txtLoginName.TabIndex = 2;
            // 
            // txtFullName
            // 
            this.txtFullName.Location = new System.Drawing.Point(432, 36);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Properties.ReadOnly = true;
            this.txtFullName.Size = new System.Drawing.Size(276, 20);
            this.txtFullName.StyleController = this.layoutControl1;
            this.txtFullName.TabIndex = 3;
            // 
            // txtCompany_ID
            // 
            this.txtCompany_ID.Location = new System.Drawing.Point(87, 60);
            this.txtCompany_ID.Name = "txtCompany_ID";
            this.txtCompany_ID.Properties.ReadOnly = true;
            this.txtCompany_ID.Size = new System.Drawing.Size(266, 20);
            this.txtCompany_ID.StyleController = this.layoutControl1;
            this.txtCompany_ID.TabIndex = 4;
            // 
            // txtCompanyName
            // 
            this.txtCompanyName.Location = new System.Drawing.Point(432, 60);
            this.txtCompanyName.Name = "txtCompanyName";
            this.txtCompanyName.Properties.ReadOnly = true;
            this.txtCompanyName.Size = new System.Drawing.Size(276, 20);
            this.txtCompanyName.StyleController = this.layoutControl1;
            this.txtCompanyName.TabIndex = 5;
            // 
            // txtTableName
            // 
            this.txtTableName.Location = new System.Drawing.Point(87, 84);
            this.txtTableName.Name = "txtTableName";
            this.txtTableName.Properties.ReadOnly = true;
            this.txtTableName.Size = new System.Drawing.Size(266, 20);
            this.txtTableName.StyleController = this.layoutControl1;
            this.txtTableName.TabIndex = 6;
            // 
            // txtOperationType
            // 
            this.txtOperationType.Location = new System.Drawing.Point(432, 84);
            this.txtOperationType.Name = "txtOperationType";
            this.txtOperationType.Properties.ReadOnly = true;
            this.txtOperationType.Size = new System.Drawing.Size(276, 20);
            this.txtOperationType.StyleController = this.layoutControl1;
            this.txtOperationType.TabIndex = 7;
            // 
            // txtCreationDate
            // 
            this.txtCreationDate.EditValue = null;
            this.txtCreationDate.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtCreationDate.Location = new System.Drawing.Point(432, 12);
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
            this.txtCreationDate.Size = new System.Drawing.Size(276, 20);
            this.txtCreationDate.StyleController = this.layoutControl1;
            this.txtCreationDate.TabIndex = 9;
            // 
            // txtNote
            // 
            this.txtNote.Location = new System.Drawing.Point(87, 132);
            this.txtNote.Name = "txtNote";
            this.txtNote.Properties.ReadOnly = true;
            this.txtNote.Size = new System.Drawing.Size(621, 303);
            this.txtNote.StyleController = this.layoutControl1;
            this.txtNote.TabIndex = 8;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem4,
            this.layoutControlItem6,
            this.layoutControlItem8,
            this.layoutControlItem5,
            this.layoutControlItem3,
            this.layoutControlItem7,
            this.layoutControlItem9,
            this.layoutControlItem10,
            this.layoutControlItem11});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(720, 447);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.txtUser_ID;
            this.layoutControlItem1.CustomizationFormText = "登录用户ID";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(345, 24);
            this.layoutControlItem1.Text = "登录用户ID";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(72, 14);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.txtLoginName;
            this.layoutControlItem2.CustomizationFormText = "登录名";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(345, 24);
            this.layoutControlItem2.Text = "登录名";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(72, 14);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.txtCompany_ID;
            this.layoutControlItem4.CustomizationFormText = "所属公司ID";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(345, 24);
            this.layoutControlItem4.Text = "所属公司ID";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(72, 14);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.txtTableName;
            this.layoutControlItem6.CustomizationFormText = "操作表名称";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(345, 24);
            this.layoutControlItem6.Text = "操作表名称";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(72, 14);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.txtNote;
            this.layoutControlItem8.CustomizationFormText = "日志描述";
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 120);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(700, 307);
            this.layoutControlItem8.Text = "日志描述";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(72, 14);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.txtCompanyName;
            this.layoutControlItem5.CustomizationFormText = "所属公司名称";
            this.layoutControlItem5.Location = new System.Drawing.Point(345, 48);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(355, 24);
            this.layoutControlItem5.Text = "所属公司名称";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(72, 14);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.txtFullName;
            this.layoutControlItem3.CustomizationFormText = "真实名称";
            this.layoutControlItem3.Location = new System.Drawing.Point(345, 24);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(355, 24);
            this.layoutControlItem3.Text = "真实名称";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(72, 14);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.txtOperationType;
            this.layoutControlItem7.CustomizationFormText = "操作类型";
            this.layoutControlItem7.Location = new System.Drawing.Point(345, 72);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(355, 24);
            this.layoutControlItem7.Text = "操作类型";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(72, 14);
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.txtCreationDate;
            this.layoutControlItem9.CustomizationFormText = "创建时间";
            this.layoutControlItem9.Location = new System.Drawing.Point(345, 0);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(355, 24);
            this.layoutControlItem9.Text = "创建时间";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(72, 14);
            // 
            // txtIPAddress
            // 
            this.txtIPAddress.Location = new System.Drawing.Point(87, 108);
            this.txtIPAddress.Name = "txtIPAddress";
            this.txtIPAddress.Properties.ReadOnly = true;
            this.txtIPAddress.Size = new System.Drawing.Size(267, 20);
            this.txtIPAddress.StyleController = this.layoutControl1;
            this.txtIPAddress.TabIndex = 10;
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.txtIPAddress;
            this.layoutControlItem10.CustomizationFormText = "IP地址";
            this.layoutControlItem10.Location = new System.Drawing.Point(0, 96);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(346, 24);
            this.layoutControlItem10.Text = "IP地址";
            this.layoutControlItem10.TextSize = new System.Drawing.Size(72, 14);
            // 
            // txtMacAddress
            // 
            this.txtMacAddress.Location = new System.Drawing.Point(433, 108);
            this.txtMacAddress.Name = "txtMacAddress";
            this.txtMacAddress.Properties.ReadOnly = true;
            this.txtMacAddress.Size = new System.Drawing.Size(275, 20);
            this.txtMacAddress.StyleController = this.layoutControl1;
            this.txtMacAddress.TabIndex = 11;
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.txtMacAddress;
            this.layoutControlItem11.CustomizationFormText = "Mac地址";
            this.layoutControlItem11.Location = new System.Drawing.Point(346, 96);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(354, 24);
            this.layoutControlItem11.Text = "Mac地址";
            this.layoutControlItem11.TextSize = new System.Drawing.Size(72, 14);
            // 
            // FrmEditOperationLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(739, 511);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FrmEditOperationLog";
            this.Text = "用户关键操作记录";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.btnOK, 0);
            this.Controls.SetChildIndex(this.btnAdd, 0);
            this.Controls.SetChildIndex(this.dataNavigator1, 0);
            this.Controls.SetChildIndex(this.picPrint, 0);
            ((System.ComponentModel.ISupportInitialize)(this.picPrint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtUser_ID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLoginName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFullName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCompany_ID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCompanyName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTableName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOperationType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreationDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreationDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNote.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIPAddress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMacAddress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

         private DevExpress.XtraEditors.TextEdit txtUser_ID;
          private DevExpress.XtraEditors.TextEdit txtLoginName;
          private DevExpress.XtraEditors.TextEdit txtFullName;
          private DevExpress.XtraEditors.TextEdit txtCompany_ID;
          private DevExpress.XtraEditors.TextEdit txtCompanyName;
          private DevExpress.XtraEditors.TextEdit txtTableName;
          private DevExpress.XtraEditors.TextEdit txtOperationType;
          private DevExpress.XtraEditors.DateEdit txtCreationDate;
  
         private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;    
         private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;    
         private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;    
         private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;    
         private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;    
         private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;    
         private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;    
         private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;    
         private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
         private DevExpress.XtraEditors.MemoEdit txtNote;
         private DevExpress.XtraEditors.TextEdit txtIPAddress;
         private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
         private DevExpress.XtraEditors.TextEdit txtMacAddress;
         private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;    
 
    }
}