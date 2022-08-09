using BB.BaseUI.BaseUI;

namespace BB.BaseUI.Device;

/// <summary>
/// 摄像头图形显示窗体
/// </summary>
public partial class CameraDialog : BaseForm
{
    private Camera _camera;

    /// <summary>
    /// 构造函数
    /// </summary>
    public CameraDialog()
    {
        InitializeComponent();
        Disposed += CameraDialog_Disposed;
    }

    void CameraDialog_Disposed(object sender, EventArgs e)
    {
        _camera.Close();
    }

    private void CameraDialog_Load(object sender, EventArgs e)
    {
        const int photoWidth = 226;
        const int photoHeight = 280;
        Width = (Width - panel1.ClientSize.Width) + photoWidth;
        Height = (Height - panel1.ClientSize.Height) + photoHeight;

        _camera = new Camera(panel1);
        try
        {
            _camera.Open();
        }
        catch (Exception ex)
        {
            lblError.Parent = this;
            lblError.Top = panel1.Top;
            lblError.Left = panel1.Left;
            lblError.Width = panel1.Width;
            lblError.Height = panel1.Height;
            lblError.Text = ex.Message;
            lblError.Visible = true;
            panel1.Visible = false;
            btnOK.Enabled = false;
        }
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
        Photo = _camera.GrabImage();
    }

    /// <summary>
    /// 摄像头图片对象
    /// </summary>
    public Bitmap Photo { get; private set; }
}