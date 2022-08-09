namespace BB.Starter.UI.Settings
{
    partial class PageReport
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.radReport = new DevExpress.XtraEditors.RadioGroup();
            this.txtOtherReport = new DevExpress.XtraEditors.TextEdit();
            this.lblOtherReport = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.radReport.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOtherReport.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Location = new System.Drawing.Point(26, 23);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(91, 14);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "派车单报表设置";
            // 
            // radReport
            // 
            this.radReport.EditValue = "BB.CarDispatch.CarSendBill2.rdlc";
            this.radReport.Location = new System.Drawing.Point(60, 43);
            this.radReport.Name = "radReport";
            this.radReport.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("BB.CarDispatch.CarSendBill2.rdlc", "部队派车单"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("BB.CarDispatch.CarSendBill.rdlc", "普通派车单"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "指定其他")});
            this.radReport.Size = new System.Drawing.Size(366, 74);
            this.radReport.TabIndex = 2;
            this.radReport.SelectedIndexChanged += new System.EventHandler(this.radReport_SelectedIndexChanged);
            // 
            // txtOtherReport
            // 
            this.txtOtherReport.Location = new System.Drawing.Point(116, 123);
            this.txtOtherReport.Name = "txtOtherReport";
            this.txtOtherReport.Properties.NullValuePrompt = "指定存放在Resource\\Report的报表文件";
            this.txtOtherReport.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtOtherReport.Size = new System.Drawing.Size(310, 20);
            this.txtOtherReport.TabIndex = 3;
            // 
            // lblOtherReport
            // 
            this.lblOtherReport.Location = new System.Drawing.Point(62, 126);
            this.lblOtherReport.Name = "lblOtherReport";
            this.lblOtherReport.Size = new System.Drawing.Size(48, 14);
            this.lblOtherReport.TabIndex = 4;
            this.lblOtherReport.Text = "其他地址";
            // 
            // PageReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblOtherReport);
            this.Controls.Add(this.txtOtherReport);
            this.Controls.Add(this.radReport);
            this.Controls.Add(this.labelControl1);
            this.Name = "PageReport";
            this.Size = new System.Drawing.Size(522, 228);
            this.Load += new System.EventHandler(this.PageSetting_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radReport.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOtherReport.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.RadioGroup radReport;
        private DevExpress.XtraEditors.TextEdit txtOtherReport;
        private DevExpress.XtraEditors.LabelControl lblOtherReport;
    }
}
