namespace BB.Starter.UI.SplashScreen
{
    partial class FrmSplash
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSplash));
            this.lbStatusInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbStatusInfo
            // 
            this.lbStatusInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbStatusInfo.AutoSize = true;
            this.lbStatusInfo.BackColor = System.Drawing.Color.Transparent;
            this.lbStatusInfo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbStatusInfo.ForeColor = System.Drawing.Color.Yellow;
            this.lbStatusInfo.Location = new System.Drawing.Point(35, 185);
            this.lbStatusInfo.Name = "lbStatusInfo";
            this.lbStatusInfo.Size = new System.Drawing.Size(164, 12);
            this.lbStatusInfo.TabIndex = 0;
            this.lbStatusInfo.Text = "正在初始化应用程序......";
            // 
            // frmSplash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(500, 250);
            this.Controls.Add(this.lbStatusInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmSplash";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmSplash";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmSplash_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbStatusInfo;
    }
}