namespace BB.BaseUI.BaseUI
{
    partial class DataNavigator
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataNavigator));
            this.btnLast = new DevExpress.XtraEditors.SimpleButton();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.btnPrevious = new DevExpress.XtraEditors.SimpleButton();
            this.btnFirst = new DevExpress.XtraEditors.SimpleButton();
            this.btnNext = new DevExpress.XtraEditors.SimpleButton();
            this.txtInfo = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInfo.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLast
            // 
            this.btnLast.ImageIndex = 1;
            this.btnLast.ImageList = this.imageCollection1;
            this.btnLast.Location = new System.Drawing.Point(163, 2);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(25, 23);
            this.btnLast.TabIndex = 14;
            this.btnLast.ToolTip = "最后一页";
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "first.ico");
            this.imageCollection1.Images.SetKeyName(1, "last.ico");
            this.imageCollection1.Images.SetKeyName(2, "next.ico");
            this.imageCollection1.Images.SetKeyName(3, "previous.ico");
            // 
            // btnPrevious
            // 
            this.btnPrevious.ImageIndex = 3;
            this.btnPrevious.ImageList = this.imageCollection1;
            this.btnPrevious.Location = new System.Drawing.Point(32, 2);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(22, 23);
            this.btnPrevious.TabIndex = 13;
            this.btnPrevious.ToolTip = "上一页";
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // btnFirst
            // 
            this.btnFirst.ImageIndex = 0;
            this.btnFirst.ImageList = this.imageCollection1;
            this.btnFirst.Location = new System.Drawing.Point(3, 2);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(23, 23);
            this.btnFirst.TabIndex = 12;
            this.btnFirst.ToolTip = "首页";
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // btnNext
            // 
            this.btnNext.ImageIndex = 2;
            this.btnNext.ImageList = this.imageCollection1;
            this.btnNext.Location = new System.Drawing.Point(139, 2);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(19, 23);
            this.btnNext.TabIndex = 11;
            this.btnNext.ToolTip = "下一页";
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // txtInfo
            // 
            this.txtInfo.EditValue = "0/0";
            this.txtInfo.Location = new System.Drawing.Point(60, 3);
            this.txtInfo.Name = "txtInfo";
            this.txtInfo.Properties.ReadOnly = true;
            this.txtInfo.Size = new System.Drawing.Size(76, 20);
            this.txtInfo.TabIndex = 10;
            // 
            // DataNavigator
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoSize = true;
            this.Controls.Add(this.btnLast);
            this.Controls.Add(this.btnPrevious);
            this.Controls.Add(this.btnFirst);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.txtInfo);
            this.Name = "DataNavigator";
            this.Size = new System.Drawing.Size(192, 28);
            this.Load += new System.EventHandler(this.DataNavigator_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInfo.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected DevExpress.XtraEditors.SimpleButton btnLast;
        protected DevExpress.XtraEditors.SimpleButton btnPrevious;
        protected DevExpress.XtraEditors.SimpleButton btnFirst;
        protected DevExpress.XtraEditors.SimpleButton btnNext;
        protected DevExpress.XtraEditors.TextEdit txtInfo;
        private DevExpress.Utils.ImageCollection imageCollection1;

    }
}
