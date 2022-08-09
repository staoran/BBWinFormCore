using System.Reflection;

namespace BB.Starter.UI.SplashScreen;

public class Splasher
{
    private static Form _mSplashForm = null;
    private static ISplashForm _mSplashInterface = null;
    private static Thread _mSplashThread = null;
    private static string _mTempStatus = string.Empty;

    /// <summary>
    /// Show the SplashForm
    /// </summary>
    public static void Show(Type splashFormType)
    {
        if (_mSplashThread != null)
            return;
        if (splashFormType == null)
        {
            throw (new Exception("splashFormType is null"));
        }

        _mSplashThread = new Thread(delegate()
        {
            CreateInstance(splashFormType);
            Application.Run(_mSplashForm);
        });

        _mSplashThread.IsBackground = true;
        _mSplashThread.SetApartmentState(ApartmentState.STA);
        _mSplashThread.Start();
    }



    /// <summary>
    /// set the loading Status
    /// </summary>
    public static string Status
    {
        set
        {
            if (_mSplashInterface == null || _mSplashForm == null)
            {
                _mTempStatus = value;
                return;
            }
            _mSplashForm.Invoke(
                new SplashStatusChangedHandle(delegate(string str) { _mSplashInterface.SetStatusInfo(str); }),
                new object[] { value }
            );
        }

    }

    /// <summary>
    /// Colse the SplashForm
    /// </summary>
    public static void Close()
    {
        if (_mSplashThread == null || _mSplashForm == null) return;

        try
        {
            _mSplashForm.Invoke(new MethodInvoker(_mSplashForm.Close));
        }
        catch (Exception)
        {
        }
        _mSplashThread = null;
        _mSplashForm = null;
    }

    private static void CreateInstance(Type formType)
    {

        object obj = formType.InvokeMember(null,
            BindingFlags.DeclaredOnly |
            BindingFlags.Public | BindingFlags.NonPublic |
            BindingFlags.Instance | BindingFlags.CreateInstance, null, null, null);
        _mSplashForm = obj as Form;
        _mSplashInterface = obj as ISplashForm;
        if (_mSplashForm == null)
        {
            throw (new Exception("Splash Screen must inherit from System.Windows.Forms.Form"));
        }
        if (_mSplashInterface == null)
        {
            throw (new Exception("must implement interface ISplashForm"));
        }

        if (!string.IsNullOrEmpty(_mTempStatus))
            _mSplashInterface.SetStatusInfo(_mTempStatus);
    }


    private delegate void SplashStatusChangedHandle(string newStatusInfo);

}