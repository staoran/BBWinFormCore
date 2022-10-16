using BB.BaseUI.BaseUI;

namespace BB.BaseUI.DocViewer;

/// <summary>
/// 图片显示窗体
/// </summary>
public partial class FrmImageView : BaseForm
{
    /// <summary>
    /// 设置显示的图片内容
    /// </summary>
    public Image Image { get; set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    public FrmImageView()
    {
        InitializeComponent();
    }

    private void pictureEdit1_Properties_MouseUp(object? sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Right)
        {
            Close();
        }
    }

    private void FrmImageView_Load(object? sender, EventArgs e)
    {
        pictureEdit1.Image = Image;
    }

    private void FrmImageView_KeyUp(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            Close();
        }
    }
}