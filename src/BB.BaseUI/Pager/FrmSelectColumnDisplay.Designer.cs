namespace BB.BaseUI.Pager
{
    partial class FrmSelectColumnDisplay
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSelectColumnDisplay));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.chkSelectAll = new System.Windows.Forms.CheckBox();
            this.chkInverse = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Location = new System.Drawing.Point(14, 14);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(306, 309);
            this.panel1.TabIndex = 0;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.ImageIndex = 0;
            this.btnOK.ImageList = this.imageCollection1;
            this.btnOK.Location = new System.Drawing.Point(241, 341);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(79, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "确定(&O)";
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "check.ico");
            this.imageCollection1.Images.SetKeyName(1, "close.ico");
            // 
            // chkSelectAll
            // 
            this.chkSelectAll.AutoSize = true;
            this.chkSelectAll.Location = new System.Drawing.Point(15, 330);
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.Size = new System.Drawing.Size(91, 18);
            this.chkSelectAll.TabIndex = 2;
            this.chkSelectAll.Text = "全选/全不选";
            this.chkSelectAll.UseVisualStyleBackColor = true;
            this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
            // 
            // chkInverse
            // 
            this.chkInverse.AutoSize = true;
            this.chkInverse.Location = new System.Drawing.Point(140, 330);
            this.chkInverse.Name = "chkInverse";
            this.chkInverse.Size = new System.Drawing.Size(50, 18);
            this.chkInverse.TabIndex = 3;
            this.chkInverse.Text = "反选";
            this.chkInverse.UseVisualStyleBackColor = true;
            this.chkInverse.CheckedChanged += new System.EventHandler(this.chkInverse_CheckedChanged);
            // 
            // FrmSelectColumnDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 387);
            this.Controls.Add(this.chkInverse);
            this.Controls.Add(this.chkSelectAll);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSelectColumnDisplay";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "设置列的可见性";
            this.Load += new System.EventHandler(this.FrmSelectColumnDisplay_Load);
            this.Shown += new System.EventHandler(this.FrmSelectColumnDisplay_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private System.Windows.Forms.CheckBox chkSelectAll;
        private System.Windows.Forms.CheckBox chkInverse;
        private DevExpress.Utils.ImageCollection imageCollection1;
    }
}