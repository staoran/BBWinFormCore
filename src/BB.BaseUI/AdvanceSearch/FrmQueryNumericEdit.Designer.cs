namespace BB.BaseUI.AdvanceSearch
{
    partial class FrmQueryNumericEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmQueryNumericEdit));
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.lblFieldName = new DevExpress.XtraEditors.LabelControl();
            this.txtStart = new System.Windows.Forms.NumericUpDown();
            this.label1 = new DevExpress.XtraEditors.LabelControl();
            this.txtEnd = new System.Windows.Forms.NumericUpDown();
            this.label2 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEnd)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
            this.btnOK.Location = new System.Drawing.Point(146, 131);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(87, 34);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClear.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClear.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnClear.Image")));
            this.btnClear.Location = new System.Drawing.Point(246, 131);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(87, 34);
            this.btnClear.TabIndex = 0;
            this.btnClear.Text = "清除";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.Location = new System.Drawing.Point(346, 131);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(87, 34);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "取消";
            // 
            // lblFieldName
            // 
            this.lblFieldName.Appearance.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblFieldName.Appearance.Options.UseFont = true;
            this.lblFieldName.Location = new System.Drawing.Point(15, 15);
            this.lblFieldName.Name = "lblFieldName";
            this.lblFieldName.Size = new System.Drawing.Size(68, 16);
            this.lblFieldName.TabIndex = 1;
            this.lblFieldName.Text = "查询字段";
            // 
            // txtStart
            // 
            this.txtStart.Location = new System.Drawing.Point(104, 64);
            this.txtStart.Maximum = new decimal(new int[] {
            -402653185,
            -1613725636,
            54210108,
            0});
            this.txtStart.Name = "txtStart";
            this.txtStart.Size = new System.Drawing.Size(140, 22);
            this.txtStart.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Appearance.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Appearance.Options.UseFont = true;
            this.label1.Location = new System.Drawing.Point(68, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "从";
            // 
            // txtEnd
            // 
            this.txtEnd.Location = new System.Drawing.Point(294, 66);
            this.txtEnd.Maximum = new decimal(new int[] {
            -1593835521,
            466537709,
            54210,
            0});
            this.txtEnd.Name = "txtEnd";
            this.txtEnd.Size = new System.Drawing.Size(140, 22);
            this.txtEnd.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.Appearance.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Appearance.Options.UseFont = true;
            this.label2.Location = new System.Drawing.Point(258, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "到";
            // 
            // FrmQueryNumericEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 194);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtEnd);
            this.Controls.Add(this.txtStart);
            this.Controls.Add(this.lblFieldName);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnOK);
            this.Name = "FrmQueryNumericEdit";
            this.Text = "数值内容查询";
            this.Load += new System.EventHandler(this.FrmQueryNumericEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtStart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEnd)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.LabelControl lblFieldName;
        private System.Windows.Forms.NumericUpDown txtStart;
        private DevExpress.XtraEditors.LabelControl label1;
        private System.Windows.Forms.NumericUpDown txtEnd;
        private DevExpress.XtraEditors.LabelControl label2;
    }
}