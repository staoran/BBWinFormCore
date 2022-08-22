namespace BB.TMS.UI
{
    partial class FrmEditCostMsgs
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEditCostMsgs));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutCostMsgNo = new DevExpress.XtraLayout.LayoutControlItem();
            this.txtCostMsgNo = new DevExpress.XtraEditors.TextEdit();
            this.layoutStatusID = new DevExpress.XtraLayout.LayoutControlItem();
            this.txtStatusID = new DevExpress.XtraEditors.ComboBoxEdit();
            this.layoutRecvMsgNode = new DevExpress.XtraLayout.LayoutControlItem();
            this.txtRecvMsgNode = new DevExpress.XtraEditors.ComboBoxEdit();
            this.layoutRecvMsgContent = new DevExpress.XtraLayout.LayoutControlItem();
            this.txtRecvMsgContent = new DevExpress.XtraEditors.TextEdit();
            this.layoutAttaPath = new DevExpress.XtraLayout.LayoutControlItem();
            this.txtAttaPath = new DevExpress.XtraEditors.MemoEdit();
            this.layoutCreationDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.txtCreationDate = new DevExpress.XtraEditors.DateEdit();
            this.layoutCreatedBy = new DevExpress.XtraLayout.LayoutControlItem();
            this.txtCreatedBy = new DevExpress.XtraEditors.ComboBoxEdit();
            this.layoutLastUpdateDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.txtLastUpdateDate = new DevExpress.XtraEditors.DateEdit();
            this.layoutLastUpdatedBy = new DevExpress.XtraLayout.LayoutControlItem();
            this.txtLastUpdatedBy = new DevExpress.XtraEditors.ComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPrint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutCostMsgNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCostMsgNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutStatusID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStatusID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutRecvMsgNode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRecvMsgNode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutRecvMsgContent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRecvMsgContent.Properties)).BeginInit();
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
            ((System.ComponentModel.ISupportInitialize)(this.layoutLastUpdatedBy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLastUpdatedBy.Properties)).BeginInit();
            this.SuspendLayout();
            //
            // btnOK
            //
            this.btnOK.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.ImageOptions.Image")));
            this.btnOK.Location = new System.Drawing.Point(341, 258);
            //
            // btnCancel
            //
            this.btnCancel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.ImageOptions.Image")));
            this.btnCancel.Location = new System.Drawing.Point(435, 258);
            //
            // btnAdd
            //
            this.btnAdd.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.ImageOptions.Image")));
            this.btnAdd.Location = new System.Drawing.Point(247, 258);
            //
            // dataNavigator1
            //
            this.dataNavigator1.Location = new System.Drawing.Point(10, 262);
            //
            // picPrint
            //
            this.picPrint.Location = new System.Drawing.Point(207, 263);
            //
            // layoutControl1
            //
            this.layoutControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.layoutControl1.Controls.Add(this.txtCostMsgNo);
            this.layoutControl1.Controls.Add(this.txtStatusID);
            this.layoutControl1.Controls.Add(this.txtRecvMsgNode);
            this.layoutControl1.Controls.Add(this.txtRecvMsgContent);
            this.layoutControl1.Controls.Add(this.txtAttaPath);
            this.layoutControl1.Controls.Add(this.txtCreationDate);
            this.layoutControl1.Controls.Add(this.txtCreatedBy);
            this.layoutControl1.Controls.Add(this.txtLastUpdateDate);
            this.layoutControl1.Controls.Add(this.txtLastUpdatedBy);
            this.layoutControl1.Location = new System.Drawing.Point(10, 8);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(515, 241);
            this.layoutControl1.TabIndex = 200;
            this.layoutControl1.Text = "layoutControl1";
            //
            // layoutControlGroup1
            //
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutCostMsgNo,
            this.layoutStatusID,
            this.layoutRecvMsgNode,
            this.layoutRecvMsgContent,
            this.layoutAttaPath,
            this.layoutCreationDate,
            this.layoutCreatedBy,
            this.layoutLastUpdateDate,
            this.layoutLastUpdatedBy,
            });
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(515, 241);
            this.layoutControlGroup1.TextVisible = false;
            //
            // txtCostMsgNo
            //
            this.txtCostMsgNo.Location = new System.Drawing.Point(87, 12);
            this.txtCostMsgNo.Name = "txtCostMsgNo";
            this.txtCostMsgNo.Size = new System.Drawing.Size(416, 20);
            this.txtCostMsgNo.StyleController = this.layoutControl1;
            this.txtCostMsgNo.TabIndex = 0;
            //
            // layoutCostMsgNo
            //
            this.layoutCostMsgNo.Control = this.txtCostMsgNo;
            this.layoutCostMsgNo.CustomizationFormText = "费用调整编号";
            this.layoutCostMsgNo.Location = new System.Drawing.Point(0, 0);
            this.layoutCostMsgNo.Name = "layoutCostMsgNo";
            this.layoutCostMsgNo.Size = new System.Drawing.Size(495, 24);
            this.layoutCostMsgNo.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Default;
            this.layoutCostMsgNo.Text = "费用调整编号";
            this.layoutCostMsgNo.TextSize = new System.Drawing.Size(72, 14);
            //
            // txtStatusID
            //
            this.txtStatusID.Location = new System.Drawing.Point(87, 36);
            this.txtStatusID.Name = "txtStatusID";
            this.txtStatusID.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtStatusID.Size = new System.Drawing.Size(416, 20);
            this.txtStatusID.StyleController = this.layoutControl1;
            this.txtStatusID.TabIndex = 1;
            //
            // layoutStatusID
            //
            this.layoutStatusID.Control = this.txtStatusID;
            this.layoutStatusID.CustomizationFormText = "单据状态";
            this.layoutStatusID.Location = new System.Drawing.Point(0, 24);
            this.layoutStatusID.Name = "layoutStatusID";
            this.layoutStatusID.Size = new System.Drawing.Size(495, 24);
            this.layoutStatusID.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Default;
            this.layoutStatusID.Text = "单据状态";
            this.layoutStatusID.TextSize = new System.Drawing.Size(72, 14);
            //
            // txtRecvMsgNode
            //
            this.txtRecvMsgNode.Location = new System.Drawing.Point(87, 60);
            this.txtRecvMsgNode.Name = "txtRecvMsgNode";
            this.txtRecvMsgNode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtRecvMsgNode.Size = new System.Drawing.Size(416, 20);
            this.txtRecvMsgNode.StyleController = this.layoutControl1;
            this.txtRecvMsgNode.TabIndex = 2;
            //
            // layoutRecvMsgNode
            //
            this.layoutRecvMsgNode.Control = this.txtRecvMsgNode;
            this.layoutRecvMsgNode.CustomizationFormText = "回复网点";
            this.layoutRecvMsgNode.Location = new System.Drawing.Point(0, 48);
            this.layoutRecvMsgNode.Name = "layoutRecvMsgNode";
            this.layoutRecvMsgNode.Size = new System.Drawing.Size(495, 24);
            this.layoutRecvMsgNode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Default;
            this.layoutRecvMsgNode.Text = "回复网点";
            this.layoutRecvMsgNode.TextSize = new System.Drawing.Size(72, 14);
            //
            // txtRecvMsgContent
            //
            this.txtRecvMsgContent.Location = new System.Drawing.Point(87, 84);
            this.txtRecvMsgContent.Name = "txtRecvMsgContent";
            this.txtRecvMsgContent.Size = new System.Drawing.Size(416, 20);
            this.txtRecvMsgContent.StyleController = this.layoutControl1;
            this.txtRecvMsgContent.TabIndex = 3;
            //
            // layoutRecvMsgContent
            //
            this.layoutRecvMsgContent.Control = this.txtRecvMsgContent;
            this.layoutRecvMsgContent.CustomizationFormText = "回复内容";
            this.layoutRecvMsgContent.Location = new System.Drawing.Point(0, 72);
            this.layoutRecvMsgContent.Name = "layoutRecvMsgContent";
            this.layoutRecvMsgContent.Size = new System.Drawing.Size(495, 24);
            this.layoutRecvMsgContent.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Default;
            this.layoutRecvMsgContent.Text = "回复内容";
            this.layoutRecvMsgContent.TextSize = new System.Drawing.Size(72, 14);
            //
            // txtAttaPath
            //
            this.txtAttaPath.Location = new System.Drawing.Point(87, 108);
            this.txtAttaPath.Name = "txtAttaPath";
            this.txtAttaPath.Size = new System.Drawing.Size(416, 20);
            this.txtAttaPath.StyleController = this.layoutControl1;
            this.txtAttaPath.TabIndex = 4;
            //
            // layoutAttaPath
            //
            this.layoutAttaPath.Control = this.txtAttaPath;
            this.layoutAttaPath.CustomizationFormText = "附件";
            this.layoutAttaPath.Location = new System.Drawing.Point(0, 96);
            this.layoutAttaPath.Name = "layoutAttaPath";
            this.layoutAttaPath.Size = new System.Drawing.Size(495, 24);
            this.layoutAttaPath.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Default;
            this.layoutAttaPath.Text = "附件";
            this.layoutAttaPath.TextSize = new System.Drawing.Size(72, 14);
            //
            // txtCreationDate
            //
            this.txtCreationDate.EditValue = null;
            this.txtCreationDate.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtCreationDate.Location = new System.Drawing.Point(87, 132);
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
            this.txtCreationDate.TabIndex = 5;
            //
            // layoutCreationDate
            //
            this.layoutCreationDate.Control = this.txtCreationDate;
            this.layoutCreationDate.CustomizationFormText = "创建时间";
            this.layoutCreationDate.Location = new System.Drawing.Point(0, 120);
            this.layoutCreationDate.Name = "layoutCreationDate";
            this.layoutCreationDate.Size = new System.Drawing.Size(495, 24);
            this.layoutCreationDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Default;
            this.layoutCreationDate.Text = "创建时间";
            this.layoutCreationDate.TextSize = new System.Drawing.Size(72, 14);
            //
            // txtCreatedBy
            //
            this.txtCreatedBy.Location = new System.Drawing.Point(87, 156);
            this.txtCreatedBy.Name = "txtCreatedBy";
            this.txtCreatedBy.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtCreatedBy.Size = new System.Drawing.Size(416, 20);
            this.txtCreatedBy.StyleController = this.layoutControl1;
            this.txtCreatedBy.TabIndex = 6;
            //
            // layoutCreatedBy
            //
            this.layoutCreatedBy.Control = this.txtCreatedBy;
            this.layoutCreatedBy.CustomizationFormText = "创建人";
            this.layoutCreatedBy.Location = new System.Drawing.Point(0, 144);
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
            this.txtLastUpdateDate.Location = new System.Drawing.Point(87, 180);
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
            this.txtLastUpdateDate.TabIndex = 7;
            //
            // layoutLastUpdateDate
            //
            this.layoutLastUpdateDate.Control = this.txtLastUpdateDate;
            this.layoutLastUpdateDate.CustomizationFormText = "修改时间";
            this.layoutLastUpdateDate.Location = new System.Drawing.Point(0, 168);
            this.layoutLastUpdateDate.Name = "layoutLastUpdateDate";
            this.layoutLastUpdateDate.Size = new System.Drawing.Size(495, 24);
            this.layoutLastUpdateDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Default;
            this.layoutLastUpdateDate.Text = "修改时间";
            this.layoutLastUpdateDate.TextSize = new System.Drawing.Size(72, 14);
            //
            // txtLastUpdatedBy
            //
            this.txtLastUpdatedBy.Location = new System.Drawing.Point(87, 204);
            this.txtLastUpdatedBy.Name = "txtLastUpdatedBy";
            this.txtLastUpdatedBy.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtLastUpdatedBy.Size = new System.Drawing.Size(416, 20);
            this.txtLastUpdatedBy.StyleController = this.layoutControl1;
            this.txtLastUpdatedBy.TabIndex = 8;
            //
            // layoutLastUpdatedBy
            //
            this.layoutLastUpdatedBy.Control = this.txtLastUpdatedBy;
            this.layoutLastUpdatedBy.CustomizationFormText = "修改人";
            this.layoutLastUpdatedBy.Location = new System.Drawing.Point(0, 192);
            this.layoutLastUpdatedBy.Name = "layoutLastUpdatedBy";
            this.layoutLastUpdatedBy.Size = new System.Drawing.Size(495, 24);
            this.layoutLastUpdatedBy.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Default;
            this.layoutLastUpdatedBy.Text = "修改人";
            this.layoutLastUpdatedBy.TextSize = new System.Drawing.Size(72, 14);
            //
            // FrmEditCostMsgs
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(535, 298);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FrmEditCostMsgs";
            this.Text = "费用调整确认";
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
            ((System.ComponentModel.ISupportInitialize)(this.txtCostMsgNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutCostMsgNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStatusID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutStatusID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRecvMsgNode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutRecvMsgNode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRecvMsgContent.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutRecvMsgContent)).EndInit();
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
            ((System.ComponentModel.ISupportInitialize)(this.txtLastUpdatedBy.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutLastUpdatedBy)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txtCostMsgNo;
        private DevExpress.XtraLayout.LayoutControlItem layoutCostMsgNo;
        private DevExpress.XtraEditors.ComboBoxEdit txtStatusID;
        private DevExpress.XtraLayout.LayoutControlItem layoutStatusID;
        private DevExpress.XtraEditors.ComboBoxEdit txtRecvMsgNode;
        private DevExpress.XtraLayout.LayoutControlItem layoutRecvMsgNode;
        private DevExpress.XtraEditors.TextEdit txtRecvMsgContent;
        private DevExpress.XtraLayout.LayoutControlItem layoutRecvMsgContent;
        private DevExpress.XtraEditors.MemoEdit txtAttaPath;
        private DevExpress.XtraLayout.LayoutControlItem layoutAttaPath;
        private DevExpress.XtraEditors.DateEdit txtCreationDate;
        private DevExpress.XtraLayout.LayoutControlItem layoutCreationDate;
        private DevExpress.XtraEditors.ComboBoxEdit txtCreatedBy;
        private DevExpress.XtraLayout.LayoutControlItem layoutCreatedBy;
        private DevExpress.XtraEditors.DateEdit txtLastUpdateDate;
        private DevExpress.XtraLayout.LayoutControlItem layoutLastUpdateDate;
        private DevExpress.XtraEditors.ComboBoxEdit txtLastUpdatedBy;
        private DevExpress.XtraLayout.LayoutControlItem layoutLastUpdatedBy;
    }
}