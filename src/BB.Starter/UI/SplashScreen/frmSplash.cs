using BB.BaseUI.Other;

namespace BB.Starter.UI.SplashScreen;

public partial class FrmSplash : Form,ISplashForm
{
    public FrmSplash()
    {
        InitializeComponent();
    }

    #region ISplashForm

    void ISplashForm.SetStatusInfo(string newStatusInfo)
    {
        lbStatusInfo.Text = newStatusInfo;
    }

    #endregion

    private void frmSplash_Load(object sender, EventArgs e)
    {
        if (!DesignMode)
        {
            string picturePath = GB.Config.AppConfigGet("SplashScreen");
            if (!string.IsNullOrEmpty(picturePath))
            {
                string realPath = Path.Combine(Application.StartupPath, picturePath);
                if (File.Exists(realPath))
                {
                    BackgroundImage = Image.FromFile(realPath);
                    Size newSize = BackgroundImage.Size;
                    if(BackgroundImage.Size.Width > 800)
                    {
                        newSize = new Size(BackgroundImage.Size.Width / 2, BackgroundImage.Size.Height / 2);
                    }
                    Size = newSize;
                    Invalidate();                        
                }
            }
        }
    }
}