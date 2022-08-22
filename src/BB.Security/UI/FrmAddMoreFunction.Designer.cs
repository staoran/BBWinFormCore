using BB.BaseUI.Control.Security;

namespace BB.Security.UI
{
    partial class FrmAddMoreFunction
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.functionControl1 = new FunctionControl();
            this.chkExport = new DevExpress.XtraEditors.CheckEdit();
            this.chkImport = new DevExpress.XtraEditors.CheckEdit();
            this.chkView = new DevExpress.XtraEditors.CheckEdit();
            this.chkDelete = new DevExpress.XtraEditors.CheckEdit();
            this.chkModify = new DevExpress.XtraEditors.CheckEdit();
            this.chkAdd = new DevExpress.XtraEditors.CheckEdit();
            this.txtSystemType = new DevExpress.XtraEditors.TextEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.lblSystemType = new System.Windows.Forms.Label();
            this.txtFunctionID = new DevExpress.XtraEditors.TextEdit();
            this.label5 = new System.Windows.Forms.Label();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkExport.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkImport.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkView.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDelete.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkModify.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAdd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSystemType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFunctionID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.functionControl1);
            this.groupBox2.Controls.Add(this.chkExport);
            this.groupBox2.Controls.Add(this.chkImport);
            this.groupBox2.Controls.Add(this.chkView);
            this.groupBox2.Controls.Add(this.chkDelete);
            this.groupBox2.Controls.Add(this.chkModify);
            this.groupBox2.Controls.Add(this.chkAdd);
            this.groupBox2.Controls.Add(this.txtSystemType);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.lblSystemType);
            this.groupBox2.Controls.Add(this.txtFunctionID);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtName);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(417, 231);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "主功能信息";
            // 
            // functionControl1
            // 
            this.functionControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.functionControl1.Location = new System.Drawing.Point(113, 52);
            this.functionControl1.Name = "functionControl1";
            this.functionControl1.Size = new System.Drawing.Size(291, 23);
            this.functionControl1.TabIndex = 7;
            this.functionControl1.Value = "-1";
            // 
            // chkExport
            // 
            this.chkExport.EditValue = true;
            this.chkExport.Location = new System.Drawing.Point(190, 201);
            this.chkExport.Name = "chkExport";
            this.chkExport.Properties.Caption = "导出";
            this.chkExport.Size = new System.Drawing.Size(50, 19);
            this.chkExport.TabIndex = 3;
            // 
            // chkImport
            // 
            this.chkImport.EditValue = true;
            this.chkImport.Location = new System.Drawing.Point(113, 201);
            this.chkImport.Name = "chkImport";
            this.chkImport.Properties.Caption = "导入";
            this.chkImport.Size = new System.Drawing.Size(50, 19);
            this.chkImport.TabIndex = 3;
            // 
            // chkView
            // 
            this.chkView.EditValue = true;
            this.chkView.Location = new System.Drawing.Point(344, 170);
            this.chkView.Name = "chkView";
            this.chkView.Properties.Caption = "查看";
            this.chkView.Size = new System.Drawing.Size(50, 19);
            this.chkView.TabIndex = 3;
            // 
            // chkDelete
            // 
            this.chkDelete.EditValue = true;
            this.chkDelete.Location = new System.Drawing.Point(190, 170);
            this.chkDelete.Name = "chkDelete";
            this.chkDelete.Properties.Caption = "删除";
            this.chkDelete.Size = new System.Drawing.Size(50, 19);
            this.chkDelete.TabIndex = 3;
            // 
            // chkModify
            // 
            this.chkModify.EditValue = true;
            this.chkModify.Location = new System.Drawing.Point(267, 170);
            this.chkModify.Name = "chkModify";
            this.chkModify.Properties.Caption = "修改";
            this.chkModify.Size = new System.Drawing.Size(50, 19);
            this.chkModify.TabIndex = 3;
            // 
            // chkAdd
            // 
            this.chkAdd.EditValue = true;
            this.chkAdd.Location = new System.Drawing.Point(113, 170);
            this.chkAdd.Name = "chkAdd";
            this.chkAdd.Properties.Caption = "添加";
            this.chkAdd.Size = new System.Drawing.Size(50, 19);
            this.chkAdd.TabIndex = 3;
            // 
            // txtSystemType
            // 
            this.txtSystemType.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSystemType.Location = new System.Drawing.Point(113, 124);
            this.txtSystemType.Name = "txtSystemType";
            this.txtSystemType.Size = new System.Drawing.Size(291, 20);
            this.txtSystemType.TabIndex = 2;
            this.txtSystemType.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 171);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 14);
            this.label3.TabIndex = 0;
            this.label3.Text = "其他子功能：";
            // 
            // lblSystemType
            // 
            this.lblSystemType.AutoSize = true;
            this.lblSystemType.Location = new System.Drawing.Point(10, 129);
            this.lblSystemType.Name = "lblSystemType";
            this.lblSystemType.Size = new System.Drawing.Size(108, 14);
            this.lblSystemType.TabIndex = 0;
            this.lblSystemType.Text = "系统类型编号(*)：";
            this.lblSystemType.Visible = false;
            // 
            // txtFunctionID
            // 
            this.txtFunctionID.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFunctionID.Location = new System.Drawing.Point(113, 92);
            this.txtFunctionID.Name = "txtFunctionID";
            this.txtFunctionID.Size = new System.Drawing.Size(291, 20);
            this.txtFunctionID.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 97);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 14);
            this.label5.TabIndex = 0;
            this.label5.Text = "功能控件ID(*)：";
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(113, 21);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(291, 20);
            this.txtName.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 14);
            this.label2.TabIndex = 0;
            this.label2.Text = "上层功能(*)：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "主功能名称(*)：";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(263, 265);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "保存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(354, 265);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "关闭";
            // 
            // FrmAddMoreFunction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(441, 300);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBox2);
            this.Name = "FrmAddMoreFunction";
            this.Text = "批量添加多个功能";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkExport.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkImport.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkView.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDelete.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkModify.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAdd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSystemType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFunctionID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraEditors.TextEdit txtSystemType;
        private System.Windows.Forms.Label lblSystemType;
        private DevExpress.XtraEditors.TextEdit txtFunctionID;
        private System.Windows.Forms.Label label5;
        private DevExpress.XtraEditors.TextEdit txtName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.CheckEdit chkExport;
        private DevExpress.XtraEditors.CheckEdit chkImport;
        private DevExpress.XtraEditors.CheckEdit chkView;
        private DevExpress.XtraEditors.CheckEdit chkDelete;
        private DevExpress.XtraEditors.CheckEdit chkModify;
        private DevExpress.XtraEditors.CheckEdit chkAdd;
        private System.Windows.Forms.Label label3;
        private FunctionControl functionControl1;
    }
}