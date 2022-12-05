using System.ComponentModel;

namespace BB.BaseUI.BaseUI;

partial class BaseEditDesigner
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private IContainer components = null;

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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaseEditDesigner));
        this.btnOK = new DevExpress.XtraEditors.SimpleButton();
        this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
        this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
        this.dataNavigator1 = new DataNavigator();
        this.picPrint = new System.Windows.Forms.PictureBox();
        ((System.ComponentModel.ISupportInitialize)(this.picPrint)).BeginInit();
        this.SuspendLayout();
        // 
        // btnOK
        // 
        this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        this.btnOK.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
        this.btnOK.Location = new System.Drawing.Point(333, 407);
        this.btnOK.Name = "btnOK";
        this.btnOK.Size = new System.Drawing.Size(88, 32);
        this.btnOK.TabIndex = 1;
        this.btnOK.Text = "保存(&S)";
        this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
        // 
        // btnCancel
        // 
        this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        this.btnCancel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
        this.btnCancel.Location = new System.Drawing.Point(432, 407);
        this.btnCancel.Name = "btnCancel";
        this.btnCancel.Size = new System.Drawing.Size(88, 32);
        this.btnCancel.TabIndex = 2;
        this.btnCancel.Text = "关闭";
        this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
        // 
        // btnAdd
        // 
        this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        this.btnAdd.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
        this.btnAdd.Location = new System.Drawing.Point(234, 407);
        this.btnAdd.Name = "btnAdd";
        this.btnAdd.Size = new System.Drawing.Size(88, 32);
        this.btnAdd.TabIndex = 0;
        this.btnAdd.Text = "添加(&A)";
        this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
        // 
        // dataNavigator1
        // 
        this.dataNavigator1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        this.dataNavigator1.AutoSize = true;
        this.dataNavigator1.CurrentIndex = 0;
        this.dataNavigator1.Location = new System.Drawing.Point(12, 410);
        this.dataNavigator1.Name = "dataNavigator1";
        this.dataNavigator1.Size = new System.Drawing.Size(191, 28);
        this.dataNavigator1.TabIndex = 3;
        // 
        // picPrint
        // 
        this.picPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        this.picPrint.Image = ((System.Drawing.Image)(resources.GetObject("picPrint.Image")));
        this.picPrint.Location = new System.Drawing.Point(202, 412);
        this.picPrint.Name = "picPrint";
        this.picPrint.Size = new System.Drawing.Size(25, 23);
        this.picPrint.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
        this.picPrint.TabIndex = 5;
        this.picPrint.TabStop = false;
        this.picPrint.Click += new System.EventHandler(this.picPrint_Click);
        // 
        // BaseEditDesigner
        // 
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
        this.ClientSize = new System.Drawing.Size(535, 456);
        this.Controls.Add(this.picPrint);
        this.Controls.Add(this.dataNavigator1);
        this.Controls.Add(this.btnAdd);
        this.Controls.Add(this.btnOK);
        this.Controls.Add(this.btnCancel);
        this.Name = "BaseEditDesigner";
        this.ShowInTaskbar = false;
        this.Text = "BaseEditDesigner";
        this.Load += new System.EventHandler(this.BaseEditDesigner_Load);
        ((System.ComponentModel.ISupportInitialize)(this.picPrint)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    #endregion

    protected DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
    protected DevExpress.XtraLayout.LayoutControl layoutControl1;
    protected DevExpress.XtraEditors.SimpleButton btnOK;
    protected DevExpress.XtraEditors.SimpleButton btnCancel;
    protected DevExpress.XtraEditors.SimpleButton btnAdd;
    protected DevExpress.XtraGrid.GridControl gridControl1;
    protected DevExpress.XtraGrid.Views.Grid.GridView gridView1;
    protected DataNavigator dataNavigator1;
    protected PictureBox picPrint;
}