using System.ComponentModel;
using BB.BaseUI.Pager;

namespace BB.BaseUI.BaseUI;

partial class BaseViewDesigner
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
        this.components = new System.ComponentModel.Container();
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(800, 450);
        this.Text = "BaseViewDesigner";
    }

    #endregion
    
    protected DevExpress.XtraEditors.SplitContainerControl splitContainer1;
    protected WinGridViewPager winGridViewPager1;
    protected WinGridView winGridView2;    
    protected System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    protected DevExpress.XtraLayout.LayoutControl layoutControl1;
    protected DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
    protected DevExpress.XtraBars.Bar bar1;
    protected System.Windows.Forms.TreeView treeView1;
}