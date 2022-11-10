using BB.BaseUI.Extension;
using BB.BaseUI.Other;
using BB.Entity.Base;
using BB.Tools.Entity;
using BB.Tools.MultiLanuage;
using DevExpress.XtraBars.Alerter;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using Furion.Logging.Extensions;

namespace BB.BaseUI.BaseUI;

/// <summary>
/// 自定义控件基类
/// </summary>
public partial class BaseUserControl : XtraUserControl//, IFunction
{
    /// <summary>
    /// 子窗体数据保存的触发
    /// </summary>
    public event EventHandler OnDataSaved;

    /// <summary>
    /// 进行数据过滤的Sql条件，默认通过 Cache.Instance["DataFilterCondition"]获取
    /// </summary>
    public string DataFilterCondition { get; set; }

    /// <summary>
    /// 选择查看的公司ID
    /// </summary>
    public string SelectedCompanyId { get; set; }

    /// <summary>
    /// 默认构造函数
    /// </summary>
    public BaseUserControl()
    {
        //为了保证一些界面控件的权限控制和身份确认，以及简化操作，在界面初始化的时候，从缓存里面内容（如果存在的话）
        //继承的子模块，也可以通过InitFunction()进行指定用户相关信息
        LoginUserInfo = GB.LoginUserInfo;
        FunctionDict = GB.FunctionDict;

        //此处放最后，防止部分控件使用上面缓存内容出现问题
        InitializeComponent();
    }

    /// <summary>
    /// 处理数据保存后的事件触发
    /// </summary>
    public virtual void ProcessDataSaved(object? sender, EventArgs e)
    {
        if (OnDataSaved != null)
        {
            OnDataSaved(sender, e);
        }
    }

    /// <summary>
    /// 记录异常
    /// </summary>
    /// <param name="ex"></param>
    public void WriteException(Exception ex)
    {
        // 在本地记录异常
        ex.ToString().LogError();
        ex.Message.ShowUxError();
    }

    /// <summary>
    /// 处理异常信息
    /// </summary>
    /// <param name="ex">异常</param>
    public void ProcessException(Exception ex)
    {
        WriteException(ex);

        // 显示异常页面
        //FrmException frmException = new FrmException(this.UserInfo, ex);
        //frmException.ShowDialog();

        ex.Message.ShowUxError();//临时处理
    }

    // /// <summary>
    // /// 初始化权限控制信息
    // /// </summary>
    // public void InitFunction(LoginUserInfo userInfo, Dictionary<string, string> functionDict)
    // {
    //     if (userInfo != null)
    //     {
    //         LoginUserInfo = userInfo;
    //     }
    //     if (functionDict != null && functionDict.Count > 0)
    //     {
    //         FunctionDict = functionDict;
    //     }
    // }
    //
    // /// <summary>
    // /// 是否具有访问指定控制ID的权限
    // /// </summary>
    // /// <param name="controlId">功能控制ID</param>
    // /// <returns></returns>
    // public bool HasFunction(string controlId)
    // {
    //     return !FunctionDict.Any() || GB.DataCanManage(LoginUserInfo.CompanyId) || (!string.IsNullOrEmpty(controlId) &&
    //         FunctionDict.ContainsKey(controlId));
    // }


    /// <summary>
    /// 登陆用户基础信息
    /// </summary>
    public LoginUserInfo LoginUserInfo { get; set; }

    /// <summary>
    /// 登录用户具有的功能字典集合
    /// </summary>
    public Dictionary<string, string> FunctionDict { get; set; }

    private AppInfo _mAppInfo = new AppInfo();
    /// <summary>
    /// 应用程序基础信息
    /// </summary>
    public AppInfo AppInfo
    {
        get => _mAppInfo;
        set => _mAppInfo = value;
    }


    private static SplashScreenManager _waitForm;
    /// <summary>
    /// 等待窗体管理对象
    /// </summary>
    protected SplashScreenManager WaitForm
    {
        get
        {
            if (_waitForm == null)
            {
                _waitForm = new SplashScreenManager(ParentForm, typeof(FrmWaitForm), true, true);
                // _waitForm.ClosingDelay = 0;
            }
            return _waitForm;
        }
    }
    /// <summary>
    /// 显示等待窗体
    /// </summary>
    public void ShowWaitForm()
    {
        if (!WaitForm.IsSplashFormVisible)
        {
            WaitForm.ShowWaitForm();
        }
    }
    /// <summary>
    /// 关闭等待窗体
    /// </summary>
    public void HideWaitForm()
    {
        if (WaitForm.IsSplashFormVisible)
        {
            WaitForm.CloseWaitForm();
        }
    }
    /// <summary>
    /// 显示自定义并自动关闭的信息
    /// </summary>
    /// <param name="message">自定义消息，默认为：操作成功</param>
    /// <param name="description">正文内容</param>
    /// <param name="during">显示时间（毫秒）</param>
    public void ShowMessageAutoHide(string message = "操作成功", string description = "", int during = 1000)
    {
        message = JsonLanguage.Default.GetString(message);
        description = JsonLanguage.Default.GetString(description);

        new Thread(() =>
        {
            ShowWaitForm();
            WaitForm.SetWaitFormCaption(message);
            WaitForm.SetWaitFormDescription(description);
            Thread.Sleep(during);
            HideWaitForm();
        }).Start();
    }

    /// <summary>
    /// 使用AlertControl弹出显示提示内容
    /// </summary>
    /// <param name="message">标题内容</param>
    /// <param name="description">正文内容</param>
    /// <param name="owner"></param>
    /// <param name="autoFormDelay">延迟</param>
    /// <param name="formLocation">显示位置</param>
    public void ShowAlertControl(string message = "操作成功", string description = "", Form? owner = null, int autoFormDelay = 1000, AlertFormLocation formLocation = AlertFormLocation.TopRight)
    {
        message = JsonLanguage.Default.GetString(message);
        description = JsonLanguage.Default.GetString(description);

        AlertControl alert = new AlertControl();
        alert.FormLocation = formLocation;
        alert.AutoFormDelay = autoFormDelay;
        alert.Show(owner ?? ParentForm, message, string.IsNullOrEmpty(description) ? message : description);
    }
}