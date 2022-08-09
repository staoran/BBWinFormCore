using DevExpress.XtraSplashScreen;

namespace BB.BaseUI.BaseUI;

/// <summary>
/// 通用的程序闪屏窗体
/// </summary>
public partial class FrmSplashScreen : SplashScreen
{
    /// <summary>
    /// 闪屏图片
    /// </summary>
    public Image SplashImage { get; set; }

    /// <summary>
    /// 默认构造函数
    /// </summary>
    public FrmSplashScreen()
    {
        InitializeComponent();
    }

    #region Overrides

    public override void ProcessCommand(Enum cmd, object arg)
    {
        base.ProcessCommand(cmd, arg);
    }

    #endregion

    public enum SplashScreenCommand
    {
    }

    private void FrmSplashScreen_Load(object sender, EventArgs e)
    {
        if(!DesignMode)
        {
            if(SplashImage != null)
            {
                pictureEdit2.Image = SplashImage;
            }
        }
    }    
}