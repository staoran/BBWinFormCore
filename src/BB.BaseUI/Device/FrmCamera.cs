using BB.BaseUI.BaseUI;

namespace BB.BaseUI.Device;

/// <summary>
/// 摄像头采集图片
/// </summary>
public partial class FrmCamera : BaseForm
{
    /// <summary>
    /// 采集图片
    /// </summary>
    public Image CameraImage { get; set; }

    public FrmCamera()
    {
        InitializeComponent();
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.OK;
    }

    private void btnCapture_Click(object sender, EventArgs e)
    {
        CameraImage = cameraControl1.TakeSnapshot();
        pictureEdit1.Image = CameraImage;
    }

    private void FrmCamera_FormClosing(object sender, FormClosingEventArgs e)
    {
        //释放设备资源
        cameraControl1.Dispose();
    }
}