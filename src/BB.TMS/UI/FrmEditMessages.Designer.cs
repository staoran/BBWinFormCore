namespace BB.TMS.UI
{
    partial class FrmEditMessages
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEditMessages));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutMsgNo = new DevExpress.XtraLayout.LayoutControlItem();
            this.txtMsgNo = new DevExpress.XtraEditors.TextEdit();
            this.layoutDealStatus = new DevExpress.XtraLayout.LayoutControlItem();
            this.txtDealStatus = new DevExpress.XtraEditors.ComboBoxEdit();
            this.layoutDealContent = new DevExpress.XtraLayout.LayoutControlItem();
            this.txtDealContent = new DevExpress.XtraEditors.TextEdit();
            this.layoutAttaPath = new DevExpress.XtraLayout.LayoutControlItem();
            this.txtAttaPath = new DevExpress.XtraEditors.TextEdit();
            this.layoutCreationDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.txtCreationDate = new DevExpress.XtraEditors.DateEdit();
            this.layoutCreatedBy = new DevExpress.XtraLayout.LayoutControlItem();
            this.txtCreatedBy = new DevExpress.XtraEditors.ComboBoxEdit();
            this.layoutLastUpdateDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.txtLastUpdateDate = new DevExpress.XtraEditors.DateEdit();
            this.layoutLastUpdateBy = new DevExpress.XtraLayout.LayoutControlItem();
            this.txtLastUpdateBy = new DevExpress.XtraEditors.ComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPrint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutMsgNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMsgNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutDealStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDealStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutDealContent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDealContent.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutAttaPath)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAttaPath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreationDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutCreationDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreationDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutCreatedBy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreatedBy.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLastUpdateDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutLastUpdateDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLastUpdateDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutLastUpdateBy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLastUpdateBy.Properties)).BeginInit();
            this.SuspendLayout();
            //
            // btnOK
            //
            this.btnOK.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.ImageOptions.Image")));
            this.btnOK.Location = new System.Drawing.Point(341, 315);
            //
            // btnCancel
            //
            this.btnCancel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.ImageOptions.Image")));
            this.btnCancel.Location = new System.Drawing.Point(435, 315);
            //
            // btnAdd
            //
            this.btnAdd.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.ImageOptions.Image")));
            this.btnAdd.Location = new System.Drawing.Point(247, 315);
            //
            // dataNavigator1
            //
            this.dataNavigator1.Location = new System.Drawing.Point(10, 319);
            //
            // picPrint
            //
            this.picPrint.Location = new System.Drawing.Point(207, 320);
            //
            // layoutControl1
            //
            this.layoutControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.layoutControl1.Controls.Add(this.txtMsgNo);
            this.layoutControl1.Controls.Add(this.txtDealStatus);
            this.layoutControl1.Controls.Add(this.txtDealContent);
            this.layoutControl1.Controls.Add(this.txtAttaPath);
            this.layoutControl1.Controls.Add(this.txtCreationDate);
            this.layoutControl1.Controls.Add(this.txtCreatedBy);
            this.layoutControl1.Controls.Add(this.txtLastUpdateDate);
            this.layoutControl1.Controls.Add(this.txtLastUpdateBy);
            this.layoutControl1.Location = new System.Drawing.Point(10, 8);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(515, 298);
            this.layoutControl1.TabIndex = 200;
            this.layoutControl1.Text = "layoutControl1";
            //
            // layoutControlGroup1
            //
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutMsgNo,
            this.layoutDealStatus,
            this.layoutDealContent,
            this.layoutAttaPath,
            this.layoutCreationDate,
            this.layoutCreatedBy,
            this.layoutLastUpdateDate,
            this.layoutLastUpdateBy,
            });
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(515, 298);
            this.layoutControlGroup1.TextVisible = false;
            //
            // txtMsgNo
            //
            this.txtMsgNo.Location = new System.Drawing.Point(87, 12);
            this.txtMsgNo.Name = "txtMsgNo";
            this.txtMsgNo.Size = new System.Drawing.Size(416, 20);
            this.txtMsgNo.StyleController = this.layoutControl1;
            this.txtMsgNo.TabIndex = 0;
            //
            // layoutMsgNo
            //
            this.layoutMsgNo.Control = this.txtMsgNo;
            this.layoutMsgNo.CustomizationFormText = "问题件编号";
            this.layoutMsgNo.Location = new System.Drawing.Point(0, 0);
            this.layoutMsgNo.Name = "layoutMsgNo";
            this.layoutMsgNo.Size = new System.Drawing.Size(495, 24);
            this.layoutMsgNo.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Default;
            this.layoutMsgNo.Text = "问题件编号";
            this.layoutMsgNo.TextSize = new System.Drawing.Size(72, 14);
            //
            // txtDealStatus
            //
            this.txtDealStatus.Location = new System.Drawing.Point(87, 36);
            this.txtDealStatus.Name = "txtDealStatus";
            this.txtDealStatus.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDealStatus.Size = new System.Drawing.Size(416, 20);
            this.txtDealStatus.StyleController = this.layoutControl1;
            this.txtDealStatus.TabIndex = 1;
            //
            // layoutDealStatus
            //
            this.layoutDealStatus.Control = this.txtDealStatus;
            this.layoutDealStatus.CustomizationFormText = "处理状态";
            this.layoutDealStatus.Location = new System.Drawing.Point(0, 24);
            this.layoutDealStatus.Name = "layoutDealStatus";
            this.layoutDealStatus.Size = new System.Drawing.Size(495, 24);
            this.layoutDealStatus.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Default;
            this.layoutDealStatus.Text = "处理状态";
            this.layoutDealStatus.TextSize = new System.Drawing.Size(72, 14);
            //
            // txtDealContent
            //
            this.txtDealContent.Location = new System.Drawing.Point(87, 60);
            this.txtDealContent.Name = "txtDealContent";
            this.txtDealContent.Size = new System.Drawing.Size(416, 20);
            this.txtDealContent.StyleController = this.layoutControl1;
            this.txtDealContent.TabIndex = 2;
            //
            // layoutDealContent
            //
            this.layoutDealContent.Control = this.txtDealContent;
            this.layoutDealContent.CustomizationFormText = "处理内容";
            this.layoutDealContent.Location = new System.Drawing.Point(0, 48);
            this.layoutDealContent.Name = "layoutDealContent";
            this.layoutDealContent.Size = new System.Drawing.Size(495, 24);
            this.layoutDealContent.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Default;
            this.layoutDealContent.Text = "处理内容";
            this.layoutDealContent.TextSize = new System.Drawing.Size(72, 14);
            //
            // txtAttaPath
            //
            this.txtAttaPath.Location = new System.Drawing.Point(87, 84);
            this.txtAttaPath.Name = "txtAttaPath";
            this.txtAttaPath.Size = new System.Drawing.Size(416, 20);
            this.txtAttaPath.StyleController = this.layoutControl1;
            this.txtAttaPath.TabIndex = 3;
            //
            // layoutAttaPath
            //
            this.layoutAttaPath.Control = this.txtAttaPath;
            this.layoutAttaPath.CustomizationFormText = "附件地址";
            this.layoutAttaPath.Location = new System.Drawing.Point(0, 72);
            this.layoutAttaPath.Name = "layoutAttaPath";
            this.layoutAttaPath.Size = new System.Drawing.Size(495, 24);
            this.layoutAttaPath.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Default;
            this.layoutAttaPath.Text = "附件地址";
            this.layoutAttaPath.TextSize = new System.Drawing.Size(72, 14);
            //
            // txtCreationDate
            //
            this.txtCreationDate.EditValue = null;
            this.txtCreationDate.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtCreationDate.Location = new System.Drawing.Point(87, 108);
            this.txtCreationDate.Name = "txtCreationDate";
            this.txtCreationDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtCreationDate.Properties.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            this.txtCreationDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtCreationDate.Properties.EditFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            this.txtCreationDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtCreationDate.Properties.Mask.EditMask = "yyyy-MM-dd HH:mm:ss";
            this.txtCreationDate.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.True;
            this.txtCreationDate.Properties.VistaEditTime = DevExpress.Utils.DefaultBoolean.True;
            this.txtCreationDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtCreationDate.Properties.VistaTimeProperties.DisplayFormat.FormatString = "HH:mm";
            this.txtCreationDate.Properties.VistaTimeProperties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtCreationDate.Properties.VistaTimeProperties.EditFormat.FormatString = "HH:mm";
            this.txtCreationDate.Properties.VistaTimeProperties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtCreationDate.Properties.VistaTimeProperties.Mask.EditMask = "HH:mm";
            this.txtCreationDate.Size = new System.Drawing.Size(416, 20);
            this.txtCreationDate.StyleController = this.layoutControl1;
            this.txtCreationDate.TabIndex = 4;
            //
            // layoutCreationDate
            //
            this.layoutCreationDate.Control = this.txtCreationDate;
            this.layoutCreationDate.CustomizationFormText = "创建时间";
            this.layoutCreationDate.Location = new System.Drawing.Point(0, 96);
            this.layoutCreationDate.Name = "layoutCreationDate";
            this.layoutCreationDate.Size = new System.Drawing.Size(495, 24);
            this.layoutCreationDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Default;
            this.layoutCreationDate.Text = "创建时间";
            this.layoutCreationDate.TextSize = new System.Drawing.Size(72, 14);
            //
            // txtCreatedBy
            //
            this.txtCreatedBy.Location = new System.Drawing.Point(87, 132);
            this.txtCreatedBy.Name = "txtCreatedBy";
            this.txtCreatedBy.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtCreatedBy.Size = new System.Drawing.Size(416, 20);
            this.txtCreatedBy.StyleController = this.layoutControl1;
            this.txtCreatedBy.TabIndex = 5;
            //
            // layoutCreatedBy
            //
            this.layoutCreatedBy.Control = this.txtCreatedBy;
            this.layoutCreatedBy.CustomizationFormText = "创建人";
            this.layoutCreatedBy.Location = new System.Drawing.Point(0, 120);
            this.layoutCreatedBy.Name = "layoutCreatedBy";
            this.layoutCreatedBy.Size = new System.Drawing.Size(495, 24);
            this.layoutCreatedBy.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Default;
            this.layoutCreatedBy.Text = "创建人";
            this.layoutCreatedBy.TextSize = new System.Drawing.Size(72, 14);
            //
            // txtLastUpdateDate
            //
            this.txtLastUpdateDate.EditValue = null;
            this.txtLastUpdateDate.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtLastUpdateDate.Location = new System.Drawing.Point(87, 156);
            this.txtLastUpdateDate.Name = "txtLastUpdateDate";
            this.txtLastUpdateDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtLastUpdateDate.Properties.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            this.txtLastUpdateDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtLastUpdateDate.Properties.EditFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            this.txtLastUpdateDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtLastUpdateDate.Properties.Mask.EditMask = "yyyy-MM-dd HH:mm:ss";
            this.txtLastUpdateDate.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.True;
            this.txtLastUpdateDate.Properties.VistaEditTime = DevExpress.Utils.DefaultBoolean.True;
            this.txtLastUpdateDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtLastUpdateDate.Properties.VistaTimeProperties.DisplayFormat.FormatString = "HH:mm";
            this.txtLastUpdateDate.Properties.VistaTimeProperties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtLastUpdateDate.Properties.VistaTimeProperties.EditFormat.FormatString = "HH:mm";
            this.txtLastUpdateDate.Properties.VistaTimeProperties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtLastUpdateDate.Properties.VistaTimeProperties.Mask.EditMask = "HH:mm";
            this.txtLastUpdateDate.Size = new System.Drawing.Size(416, 20);
            this.txtLastUpdateDate.StyleController = this.layoutControl1;
            this.txtLastUpdateDate.TabIndex = 6;
            //
            // layoutLastUpdateDate
            //
            this.layoutLastUpdateDate.Control = this.txtLastUpdateDate;
            this.layoutLastUpdateDate.CustomizationFormText = "修改时间";
            this.layoutLastUpdateDate.Location = new System.Drawing.Point(0, 144);
            this.layoutLastUpdateDate.Name = "layoutLastUpdateDate";
            this.layoutLastUpdateDate.Size = new System.Drawing.Size(495, 24);
            this.layoutLastUpdateDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Default;
            this.layoutLastUpdateDate.Text = "修改时间";
            this.layoutLastUpdateDate.TextSize = new System.Drawing.Size(72, 14);
            //
            // txtLastUpdateBy
            //
            this.txtLastUpdateBy.Location = new System.Drawing.Point(87, 180);
            this.txtLastUpdateBy.Name = "txtLastUpdateBy";
            this.txtLastUpdateBy.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
                new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtLastUpdateBy.Size = new System.Drawing.Size(416, 20);
            this.txtLastUpdateBy.StyleController = this.layoutControl1;
            this.txtLastUpdateBy.TabIndex = 7;
            //
            // layoutLastUpdateBy
            //
            this.layoutLastUpdateBy.Control = this.txtLastUpdateBy;
            this.layoutLastUpdateBy.CustomizationFormText = "修改人";
            this.layoutLastUpdateBy.Location = new System.Drawing.Point(0, 168);
            this.layoutLastUpdateBy.Name = "layoutLastUpdateBy";
            this.layoutLastUpdateBy.Size = new System.Drawing.Size(495, 24);
            this.layoutLastUpdateBy.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Default;
            this.layoutLastUpdateBy.Text = "修改人";
            this.layoutLastUpdateBy.TextSize = new System.Drawing.Size(72, 14);
            //
            // FrmEditMessages
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(535, 355);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FrmEditMessages";
            this.Text = "问题件回复";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.btnOK, 0);
            this.Controls.SetChildIndex(this.btnAdd, 0);
            this.Controls.SetChildIndex(this.dataNavigator1, 0);
            this.Controls.SetChildIndex(this.picPrint, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPrint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMsgNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutMsgNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDealStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutDealStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDealContent.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutDealContent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAttaPath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutAttaPath)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreationDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreationDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutCreationDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreatedBy.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutCreatedBy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLastUpdateDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLastUpdateDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutLastUpdateDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLastUpdateBy.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutLastUpdateBy)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txtMsgNo;
        private DevExpress.XtraLayout.LayoutControlItem layoutMsgNo;
        private DevExpress.XtraEditors.ComboBoxEdit txtDealStatus;
        private DevExpress.XtraLayout.LayoutControlItem layoutDealStatus;
        private DevExpress.XtraEditors.TextEdit txtDealContent;
        private DevExpress.XtraLayout.LayoutControlItem layoutDealContent;
        private DevExpress.XtraEditors.TextEdit txtAttaPath;
        private DevExpress.XtraLayout.LayoutControlItem layoutAttaPath;
        private DevExpress.XtraEditors.DateEdit txtCreationDate;
        private DevExpress.XtraLayout.LayoutControlItem layoutCreationDate;
        private DevExpress.XtraEditors.ComboBoxEdit txtCreatedBy;
        private DevExpress.XtraLayout.LayoutControlItem layoutCreatedBy;
        private DevExpress.XtraEditors.DateEdit txtLastUpdateDate;
        private DevExpress.XtraLayout.LayoutControlItem layoutLastUpdateDate;
        private DevExpress.XtraEditors.ComboBoxEdit txtLastUpdateBy;
        private DevExpress.XtraLayout.LayoutControlItem layoutLastUpdateBy;
    }
}