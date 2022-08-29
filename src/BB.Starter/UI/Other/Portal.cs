using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Text;
using BB.BaseUI.BaseUI;
using BB.BaseUI.Extension;
using BB.BaseUI.Other;
using BB.BaseUI.WinForm;
using BB.Starter.UI.SYS;
using BB.Tools.Const;
using BB.Tools.Encrypt;
using BB.Tools.Format;
using BB.Tools.Utils;
using BB.Tools.Validation;
using BB.Updater.Core;
using FluentValidation;
using Furion;
using Furion.Logging.Extensions;
using Microsoft.Extensions.DependencyInjection;

#if DEBUG
// 控制台输出，需加入此库
#endif

namespace BB.Starter.UI.Other;

public class Portal
{

#if DEBUG
    [DllImport("kernel32.dll")]
    public static extern bool AllocConsole();
    [DllImport("kernel32.dll")]
    static extern bool FreeConsole();
#endif
    
    private static BackgroundWorker _updateWorker;

    /// <summary>
    /// 应用程序的主入口点。
    /// </summary>
    [STAThread]
    private static Task Main(string[] args)
    {
        Serve.Run(GenericRunOptions.DefaultSilence.ConfigureBuilder(builder =>
            builder.ConfigureServices((_, collection) =>
             collection.AddRemoteRequest(o=>
                 o.AddHttpClient(string.Empty, client =>
                     {
                         client.BaseAddress = new Uri("https://localhost:5001/api/");
                     }
                )))));

        GlobalExceptionCapture(() =>
        {
#if DEBUG
            // 允许调用控制台输出
            AllocConsole();
#endif

            //DateTime dtEnd = Convert.ToDateTime("6/1/2009");
            //if (DateTime.Now.CompareTo(dtEnd) > 0)
            //{
            //    string message = "使用期限已到，请联系作者bubing@bb.com";
            //    LogTextHelper.Error(message);
            //    MessageDxUtil.ShowUxError(message);
            //    Application.Exit();
            //    return;
            //}

            // 设置UI常量，后台检测更新
            SetUiConstants();
            // 全局互斥检测
            GlobalMutex();
            
            //界面汉化
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("zh-CN");
            DevExpress.UserSkins.BonusSkins.Register();
            DevExpress.Skins.SkinManager.EnableFormSkins();
        
            // 配置验证程序
            ValidatorOptions.Global.DisplayNameResolver = CustomValidatorExtensions.DisplayNameResolver;
            // ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Stop;

            ApplicationConfiguration.Initialize();

            if (args.Length >= 1)
            {
                LoginByArgs(args);
            }
            else
            {
                LoginNormal(args);
            }

#if DEBUG
            // 释放控制台
            FreeConsole();
#endif
        });
        return Task.CompletedTask;
    }

    /// <summary>
    /// 常规登陆方式
    /// </summary>
    /// <param name="args"></param>
    private static void LoginNormal(string[] args)
    {
        //登录界面
        var dlg = new Logon();
        dlg.StartPosition = FormStartPosition.CenterScreen;
        var result = dlg.ShowDialog();
        if (DialogResult.OK == result)
        {
            if (dlg.BLogin)
            {
                SplashScreen.Splasher.Show(typeof(SplashScreen.FrmSplash));

                GB.MainDialog = new MainForm();
                GB.MainDialog.StartPosition = FormStartPosition.CenterScreen;
                Application.Run(GB.MainDialog);
            }

        }else if(DialogResult.Yes == result)
        {
            var form = App.GetService<FrmSelectDataBase>();
            form.MaximizeBox = true;
            form.WindowState = FormWindowState.Maximized;
            Application.Run(form);
        }
        dlg.Dispose();
    }

    /// <summary>
    /// 使用参数化登录
    /// </summary>
    /// <param name="args"></param>
    private static async void LoginByArgs(string[] args)
    {
        CommandArgs commandArgs = CommandLine.Parse(args);
        // 获取用户参数
        string loginName = commandArgs.ArgPairs["U"];
        string password = commandArgs.ArgPairs.ContainsKey("P") ? commandArgs.ArgPairs["P"] : "";

        if (!string.IsNullOrEmpty(loginName))
        {
            // LoginUserInfo loginUser = await App.GetService<IUserHttpService>().VerifyUser(loginName, password, GB.SystemType);
            if (await SecurityHelper.Login(loginName, password, GB.SystemType))
            {
                SplashScreen.Splasher.Show(typeof(SplashScreen.FrmSplash));

                GB.MainDialog = App.GetService<MainForm>();
                GB.MainDialog.StartPosition = FormStartPosition.CenterScreen;
                Application.Run(GB.MainDialog);
            }
            else
            {
                "参数快捷登陆失败，您可以使用常规登陆方式重试。".ShowTips();
                LoginNormal(args);
            }
        }
        else
        {
            "命令格式有误".ShowTips();
            LoginNormal(args);
        }
    }

    private static Mutex _mutex = null;
        
    /// <summary>
    /// 程序启动标识，互斥检测
    /// </summary>
    private static void GlobalMutex()
    {
        // 是否第一次创建mutex
        bool newMutexCreated = false;
        string mutexName = "Global\\" + "HussarFramework";
        try
        {
            _mutex = new Mutex(false, mutexName, out newMutexCreated);
        }
        catch (Exception ex)
        {
            Console.Write(ex.Message);
            Thread.Sleep(1000);
            Environment.Exit(1);
        }

        // 第一次创建mutex
        if (newMutexCreated)
        {
            Console.WriteLine("程序已启动");
            // 此处为要执行的任务
        }
        else
        {
            "另一个窗口已在运行，不能重复运行。".ShowUxTips();
            Thread.Sleep(1000);
            Environment.Exit(1);//退出程序
        }
    }

    /// <summary>
    /// 发生未捕获的线程异常时
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="ex"></param>
    private static void Application_ThreadException(object sender, ThreadExceptionEventArgs ex)
    {
        GetExceptionMsg(ex.Exception, ex.ToString()).LogError();

        string message = $"{ex.Exception.Message}\r\n操作发生错误，您需要退出系统么？";
        if (DialogResult.Yes == message.ShowYesNoAndUxError())
        {
            Application.ExitThread();
        }
    }

    /// <summary>
    /// 当前域异常
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        if (e.ExceptionObject is not Exception ex) return;
        var es = e.ToString();
        if (ex is AggregateException exception)
        {
            var sb = new System.Text.StringBuilder();
            foreach (Exception innerEx in exception.Flatten().InnerExceptions)
            {
                GetExceptionMsg(innerEx, es).LogError();
                sb.AppendLine(innerEx.Message);
            }
            sb.ToString().ShowUxError();
        }
        else
        {
            GetExceptionMsg(ex, es).LogError();
        }
        
        FrmException.ShowBug(ex); 
        // 捕捉异常图片
        GetScreenshot();
    }
 
    static void GlobalExceptionCapture(Action mainContent) 
    {
        try
        {
            //设置应用程序处理异常方式：ThreadException处理
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            //处理UI线程异常
            Application.ThreadException += Application_ThreadException;
            //处理非UI线程异常
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
 
            #region 应用程序Main()内容包装的委托
            mainContent();
            #endregion
        }
        catch (Exception ex)
        {
            string str = GetExceptionMsg(ex, string.Empty);
            str.LogError();
            str.ShowUxError();
        }
    }
        
    /// <summary>
    /// 捕捉异常图片
    /// </summary>
    private static void GetScreenshot()
    {
        Thread.Sleep(Const.SLEEP_TIME); 
        int width = Screen.PrimaryScreen.Bounds.Width;
        int height = Screen.PrimaryScreen.Bounds.Height;
        Bitmap bmp = new (width, height);
        using Graphics g = Graphics.FromImage(bmp);
        g.CopyFromScreen(0, 0, 0, 0, new Size(width, height));
        bmp.Save("Screen\\" + DateTimeHelper.GetServerDateTime2().ToString("yyyyMMdd_hhmmss") + ".png", ImageFormat.Png);
    }

    /// <summary>
    /// 生成自定义异常消息
    /// </summary>
    /// <param name="ex">异常对象</param>
    /// <param name="backStr">备用异常消息：当ex为null时有效</param>
    /// <returns>异常字符串文本</returns>
    static string GetExceptionMsg(Exception ex, string backStr)
    {
        StringBuilder sbr = new ();
        sbr.AppendLine("****************************异常文本****************************");
        sbr.AppendLine("【出现时间】：" + DateTime.Now);
        if (ex != null)
        {
            sbr.AppendLine("【异常类型】：" + ex.GetType().Name);
            sbr.AppendLine("【异常信息】：" + ex.Message);
            sbr.AppendLine("【堆栈调用】：" + ex.StackTrace);
            sbr.AppendLine("【异常对象】：" + ex.Source);
            sbr.AppendLine("【触发方法】：" + ex.TargetSite);
        }
        else
        {
            sbr.AppendLine("【未处理异常】：" + backStr);
        }
        sbr.AppendLine("***************************************************************");
        return sbr.ToString();
    }
    
    #region 更新提示线程处理

    private static void updateWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
        //MessageDxUtil.ShowUxTips("版本更新完成");
    }

    private static void updateWorker_DoWork(object sender, DoWorkEventArgs e)
    {
        try
        {
            var update = new UpdateClass();
            bool newVersion = update.HasNewVersion;
            if (newVersion)
            {
                if ("有新的版本，是否需要更新".ShowYesNoAndUxTips() == DialogResult.Yes)
                {
                    Process.Start(Path.Combine(Application.StartupPath, "Updater.exe"), "121");
                    Application.Exit();
                }
            }
        }
        catch (Exception ex)
        {
            ex.Message.ShowUxError();
        }
    }

    #endregion
    

    /// <summary>
    /// 设置UI常量
    /// </summary>
    private static void SetUiConstants()
    {
        //代码设置授权码
        MyConstants.License = "397cV0hDLlNlY3VybXR5fOS8jeWNjuiBqnzlua-lt57niLHlkK-o_6rmioDmnK-mnInpmZDlhbzlj7h8RmFsc2Uv";
        // Security.MyConstants.License = "397cV0hDLlNlY3VybXR5fOS8jeWNjuiBqnzlua-lt57niLHlkK-o_6rmioDmnK-mnInpmZDlhbzlj7h8RmFsc2Uv";
        // Dictionary.Other.MyConstants.License = "37c6V0hDLkRpY3Rpa25hcnl85LyN5Y2O6IGqfOW5_*W3nueIseWQr*i-qubKgObcr*bciemZkOWFrOWPuHxGYWxzZQvv";
        // Pager.WinControl.MyConstants.License = "070eV0hDLlBhZ2VyfOS8jeWNjuiBqnx8RmFsc2Uv";

        //设置软件信息
        string expiredDate = "12/29/2013";//标识，并不起作用
        string softName = "StaffData";//软件名称，确定存储位置
        string version = "5.0";//软件版本
        string publicKey = "<RSAKeyValue><Modulus>mtDtu679/0quhftVyOc6/cBov/i534Dkh3AB8RwrpC9Vq2RIFB3uvjRUuaAEPR8vMcijQjVzqLZgMM7jFKclzbh21rWTM+YlOeraKz5FPCC7rSLnv6Tfbzia9VI/r5cfM8ogVMuUKCZeU+PTEmVviasCl8nPYyqOQchlf/MftMM=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
        UiConstants.SetValue(expiredDate, version, softName, publicKey);

        #region 更新提示/判断是否自动更新

        _updateWorker = new BackgroundWorker();
        _updateWorker.DoWork += updateWorker_DoWork;
        _updateWorker.RunWorkerCompleted += updateWorker_RunWorkerCompleted;

        string strUpdate = GB.Config.AppConfigGet("AutoUpdate");
        if (!string.IsNullOrEmpty(strUpdate))
        {
            bool.TryParse(strUpdate, out bool autoUpdate);
            if (autoUpdate)
            {
                _updateWorker.RunWorkerAsync();
            }
        }

        #endregion
    }
}