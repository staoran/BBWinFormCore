using System.ComponentModel;
using System.Diagnostics;
using BB.BaseUI.Extension;
using BB.BaseUI.Other;
using BB.BaseUI.WinForm;
using BB.Security.UI;
using BB.Starter.UI.Other;
using BB.Starter.UI.Settings;
using BB.Starter.UI.SplashScreen;
using BB.Tools.Cache;
using BB.Tools.Const;
using BB.Tools.Extension;
using BB.Tools.Format;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Helpers;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraTabbedMdi;
using Furion;
using Furion.Logging.Extensions;
using Logon = BB.BaseUI.BaseUI.Logon;

namespace BB.Starter.UI.SYS;

public partial class MainForm : RibbonForm
{
    #region 属性变量
    //定义BackgroundWorker对象，并注册事件（执行线程主体、执行UI更新事件）
    private readonly BackgroundWorker _backgroundWorker;
    //全局热键
    private readonly RegisterHotKeyHelper _hotKey2 = new();
    //用来第一次创建动态菜单
    private RibbonPageHelper _ribbonHelper;

    /// <summary>
    /// 设置窗体的标题信息
    /// </summary>
    public sealed override string Text
    {
        get => base.Text;
        set => base.Text = value;
    }

    /// <summary>
    /// 获取或设置命令状态信息
    /// </summary>
    public string CommandStatus
    {
        get => lblCommandStatus.Caption;
        set => lblCommandStatus.Caption = value;
    }

    /// <summary>
    /// 获取或设置用户信息
    /// </summary>
    public string UserStatus
    {
        get => lblCurrentUser.Caption;
        set => lblCurrentUser.Caption = value;
    }

    #endregion

    #region 初始化

    public MainForm()
    {
        InitializeComponent();

        // 初始化菜单帮助类
        _ribbonHelper = new RibbonPageHelper(this, ref ribbonControl);

        #region 注册右下角显示时间

        _backgroundWorker = new BackgroundWorker();
        //设置报告进度更新
        _backgroundWorker.WorkerReportsProgress = true;
        //注册线程主体方法
        _backgroundWorker.DoWork += backgroundWorkerShowTime_DoWork;
        //注册更新UI方法
        _backgroundWorker.ProgressChanged += backgroundWorkerShowTime_ProgressChanged;
        // 允许后台取消操作
        _backgroundWorker.WorkerSupportsCancellation = true;
        _backgroundWorker.RunWorkerAsync();

        #endregion
    }
                 
    /// <summary>
    /// 初始化用户相关的系统信息
    /// </summary>
    private async Task InitUserRelated()
    {
        // 必须首先加载缓存，用户权限在其中
        // await GB.LoadCache(); // 注释掉，下面刷新菜单时会刷新缓存

        #region 初始化菜单
            
        //刷新菜单
        await _ribbonHelper.RefreshMenu();

        //根据权限屏蔽内置的静态菜单对象
        InitAuthorizedUi();

        #endregion
            
        #region 根据权限显示对象的初始化窗体(首页，启动后默认打开的窗体)

        // if (GB.HasFunction("StockSearch"))
        // {
        //     ChildWinManagement.LoadMdiForm(this, typeof(FrmMenu));
        // }

        #endregion

        #region 初始化系统名称
        try
        {
            string certificatedCompany = GB.Config.AppConfigGet("CertificatedCompany");
            string applicationName = GB.Config.AppConfigGet("ApplicationName");
            string appWholeName = $"{certificatedCompany}-{applicationName}";
            GB.AppUnit = certificatedCompany;
            GB.AppName = appWholeName;
            GB.AppWholeName = appWholeName;

            Text = appWholeName;
            notifyIcon1.BalloonTipText = appWholeName;
            notifyIcon1.BalloonTipTitle = appWholeName;
            notifyIcon1.Text = appWholeName;

            UserStatus = $"当前用户：{GB.LoginUserInfo.FullName}({GB.LoginUserInfo.Name})";
            CommandStatus = $"欢迎使用 {GB.AppWholeName}";
        }
        catch
        {
            // ignored
        }

        #endregion
    }

    #endregion

    #region 热键和托盘菜单操作

    /// <summary>
    /// 设置Alt+W的显示/隐藏窗体全局热键
    /// </summary>
    private void SetHotKey()
    {
        try
        {
            _hotKey2.Keys = Keys.W;
            _hotKey2.ModKey = RegisterHotKeyHelper.Modkey.ModAlt;
            _hotKey2.WindowHandle = Handle;
            _hotKey2.WParam = 10003;
            _hotKey2.HotKey += hotKey2_HotKey;
            _hotKey2.StarHotKey();
        }
        catch (Exception ex)
        {
            ex.ToString().LogError();
            ex.Message.ShowUxError();
        }
    }

    void hotKey2_HotKey()
    {
        notifyMenu_Show_Click(null!, null!);
    }

    /// <summary>
    /// 托盘关于
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void notifyMenu_About_Click(object? sender, EventArgs e)
    {
        GB.About();
    }

    /// <summary>
    /// 热键事件处理
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void notifyMenu_Show_Click(object? sender, EventArgs e)
    {
        if (WindowState == FormWindowState.Minimized)
        {
            WindowState = FormWindowState.Maximized;
            Show();
            BringToFront();
            Activate();
            Focus();
        }
        else
        {
            WindowState = FormWindowState.Minimized;
            Hide();
        }
    }

    /// <summary>
    /// 托盘退出
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void notifyMenu_Exit_Click(object? sender, EventArgs e)
    {
        Exit(sender, e);
    }

    /// <summary>
    /// 托盘双击显示主界面
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void notifyIcon1_MouseDoubleClick(object? sender, MouseEventArgs e)
    {
        notifyMenu_Show_Click(sender, e);
    }

    private void MainForm_MaximizedBoundsChanged(object? sender, EventArgs e)
    {
        Hide();
    }

    #endregion
        
    #region Window 窗体事件
        
    /// <summary>
    /// 缩小到托盘中，不退出
    /// </summary>
    private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        //如果我们操作【×】按钮，那么不关闭程序而是缩小化到托盘，并提示用户.
        if (WindowState != FormWindowState.Minimized)
        {
            e.Cancel = true;//不关闭程序

            //最小化到托盘的时候显示图标提示信息，提示用户并未关闭程序
            WindowState = FormWindowState.Minimized;
            notifyIcon1.ShowBalloonTip(3000, "程序最小化提示",
                "图标已经缩小到托盘，打开窗口请双击图标即可。也可以使用Alt+W键来显示/隐藏窗体。",
                ToolTipIcon.Info);
        }
    }
        
    /// <summary>
    /// 窗体移动事件处理
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void MainForm_Move(object? sender, EventArgs e)
    {
        //最小化到托盘的时候显示图标提示信息
        if (ShowInTaskbar && WindowState == FormWindowState.Minimized)
        {
            Hide();
            notifyIcon1.ShowBalloonTip(3000, "程序最小化提示",
                "图标已经缩小到托盘，打开窗口请双击图标即可。也可以使用Alt+W键来显示/隐藏窗体。",
                ToolTipIcon.Info);
        }
    }

    /// <summary>
    /// 退出系统
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    private void Exit(object sender, EventArgs args)
    {
        DialogResult dr = "点击“Yes”退出系统，点击“No”返回".ShowYesNoAndUxTips();

        if (dr != DialogResult.Yes) return;
        notifyIcon1.Visible = false;
        _backgroundWorker.CancelAsync();
        // 等待结束
        while(_backgroundWorker.IsBusy)
        {
            Application.DoEvents();
        }
        Portal.Exit();
    }

    /// <summary>
    /// 窗体控件加载完毕
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void MainForm_Load(object? sender, EventArgs e)
    {
        try
        {
            #region 加载皮肤

            Splasher.Status = "正在展示相关的内容...";
            Thread.Sleep(Const.SLEEP_TIME);
            Application.DoEvents();
            SkinHelper.InitSkinGallery(rgbiSkins, true);
            ribbonControl.Toolbar.ItemLinks.Clear();
            ribbonControl.Toolbar.ItemLinks.Add(rgbiSkins);

            #endregion

            #region 初始化菜单及界面数据

            Splasher.Status = "初始化用户缓存、菜单及界面数据...";
            Thread.Sleep(Const.SLEEP_TIME);
            Application.DoEvents();
            await InitUserRelated();

            #endregion

            Splasher.Status = "初始化完毕...";
            Thread.Sleep(Const.SLEEP_TIME);
            Application.DoEvents();

            Splasher.Close();
            // 注册热键
            // SetHotKey();

            Init();
        }
        catch (Exception)
        {
            Splasher.Close();
            throw;
        }
        finally
        {
            ShowInTaskbar = true;
            WindowState = FormWindowState.Maximized;
        }
    }
    
    /// <summary>
    /// 手动自定义根据权限屏蔽功能
    /// </summary>
    private void InitAuthorizedUi()
    {
        tool_Dict.Visibility = GB.IsVisibility("Dictionary");
        tool_Settings.Visibility = GB.IsVisibility("Parameters");
    }

    /// <summary>
    /// 初始化界面信息
    /// </summary>
    private void Init()
    {
        //其他初始化工作
    }
        
    /// <summary>
    /// 手工刷新缓存
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
    {
        SplashScreenHelper.Show();
        GB.LoadCache();
        "内存刷新成功".ShowSuccessTip(this);
        SplashScreenHelper.Close();
    }
        
    #endregion
        
    #region Tab顶部右键菜单

    private void xtraTabbedMdiManager1_MouseDown(object? sender, MouseEventArgs e)
    {
        if (e.Button != MouseButtons.Right)
            return;

        DevExpress.XtraTab.ViewInfo.BaseTabHitInfo hi = xtraTabbedMdiManager1.CalcHitInfo(new Point(e.X, e.Y));
        if (hi.HitTest == DevExpress.XtraTab.ViewInfo.XtraTabHitTest.PageHeader)
        {
            //Form f = (hi.Page as DevExpress.XtraTabbedMdi.XtraMdiTabPage).MdiChild;
            //do something
            popupMenu1.ShowPopup(Cursor.Position);
        }
    }

    private void popMenuCloseCurrent_ItemClick(object sender, ItemClickEventArgs e)
    {
        XtraMdiTabPage page = xtraTabbedMdiManager1.SelectedPage;
        if (page != null && page.MdiChild != null)
        {
            page.MdiChild.Close();
        }
    }

    private void popMenuCloseAll_ItemClick(object sender, ItemClickEventArgs e)
    {
        CloseAllDocuments();
    }

    private void popMenuCloseOther_ItemClick(object sender, ItemClickEventArgs e)
    {
        XtraMdiTabPage selectedPage = xtraTabbedMdiManager1.SelectedPage;
        Type currentType = selectedPage.MdiChild.GetType();

        for (int i = xtraTabbedMdiManager1.Pages.Count - 1; i >= 0; i--)
        {
            XtraMdiTabPage page = xtraTabbedMdiManager1.Pages[i];
            if (page != null && page.MdiChild != null)
            {
                Form form = page.MdiChild;
                if (form.GetType() != currentType)
                {
                    form.Close();
                    if (form != null && !form.IsDisposed)
                    {
                        form.Dispose();
                    }
                }
            }
        }
    }

    public void CloseAllDocuments()
    {
        foreach (Form form in MdiChildren)
        {
            form.Close();
            if (!form.IsDisposed)
            {
                form.Dispose();
            }
        }
    }
        
    #endregion

    #region 工具条操作
        
    /// <summary>
    /// 重新登录
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void btnRelogin_ItemClick(object sender, ItemClickEventArgs e)
    {
        if ("您确定需要重新登录吗？".ShowYesNoAndUxWarning() != DialogResult.Yes) return;

        GB.MainDialog.Hide();
        await Cache.Instance.FlushAllAsync();
        
        Logon dlg = new Logon();
        dlg.StartPosition = FormStartPosition.CenterScreen;
        if (DialogResult.OK == dlg.ShowDialog())
        {
            if (dlg.BLogin)
            {
                CloseAllDocuments();
                await InitUserRelated();
            }
        }
        dlg.Dispose();
        GB.MainDialog.Show();
    }

    /// <summary>
    /// 修改密码
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void btnModPwd_ItemClick(object sender, ItemClickEventArgs e)
    {
        ChildWinManagement.PopDialogForm(typeof(FrmModifyPassword));
    }

    /// <summary>
    /// 退出 applicationMenu1
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void barItemExit_ItemClick(object sender, ItemClickEventArgs e)
    {
        Exit(sender, e);
    }

    private void btnHelp_ItemClick(object sender, ItemClickEventArgs e)
    {
        GB.Help();
    }

    private void btnAbout_ItemClick(object sender, ItemClickEventArgs e)
    {
        GB.About();
    }

    private void btnRegister_ItemClick(object sender, ItemClickEventArgs e)
    {
        GB.ShowRegDlg();
    }

    private void btnBug_ItemClick(object sender, ItemClickEventArgs e)
    {
        FrmFeeBack dlg = new FrmFeeBack();
        dlg.ShowDialog();
    }

    private void btnMyWeb_ItemClick(object sender, ItemClickEventArgs e)
    {
        Process.Start(Const.SYSTEM_WEB_URL);
    }

    private void menuLogo_ItemClick(object sender, ItemClickEventArgs e)
    {
        try
        {
            Process.Start(Const.SYSTEM_WEB_URL);
        }
        catch (Exception)
        {
            "打开浏览器失败".ShowUxError();
        }
    }


    private void tool_Settings_ItemClick(object sender, ItemClickEventArgs e)
    {
        ChildWinManagement.PopDialogForm(typeof(FrmSettings));
    }

    private void tool_Dict_ItemClick(object sender, ItemClickEventArgs e)
    {
        // ChildWinManagement.PopDialogForm(typeof(FrmDictionary));
    }

    private void tool_City_ItemClick(object sender, ItemClickEventArgs e)
    {
        // ChildWinManagement.PopDialogForm(typeof(FrmCityDistrict));
    }

    private void tool_Security_ItemClick(object sender, ItemClickEventArgs e)
    {
        // Security.UI.Portal.StartLogin();
    }

    private void tool_ModifyPass_ItemClick(object sender, ItemClickEventArgs e)
    {
        FrmModifyPassword dlg = App.GetService<FrmModifyPassword>();
        // dlg.InitFunction(GB.LoginUserInfo, GB.FunctionDict);//初始化权限控制信息
        dlg.ShowDialog();
    }

    private void tool_CurrentUserInfo_ItemClick(object sender, ItemClickEventArgs e)
    {
        FrmEditUser dlg = App.GetService<FrmEditUser>();
        dlg.ID = GB.LoginUserInfo.ID.ToString();
        dlg.ShowDialog();
    }

    private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
    {
        Process.Start(Const.FEEDBACK_MAIL);
    }
        
    private void lblCurrentUser_ItemClick(object sender, ItemClickEventArgs e)
    {
        tool_CurrentUserInfo_ItemClick(sender, e);
    }

    private void tool_OperationLog_ItemClick(object sender, ItemClickEventArgs e)
    {
        ChildWinManagement.LoadMdiForm(this, typeof(FrmOperationLog));
    }

    private void tool_LoginLog_ItemClick(object sender, ItemClickEventArgs e)
    {
        ChildWinManagement.LoadMdiForm(this, typeof(FrmLoginLog));
    }

    private void tool_BlackIP_ItemClick(object sender, ItemClickEventArgs e)
    {
        ChildWinManagement.LoadMdiForm(this, typeof(FrmBlackIp));
    }

    private void tool_SystemType_ItemClick(object sender, ItemClickEventArgs e)
    {
        ChildWinManagement.PopDialogForm(typeof(FrmSystemType));
    }

    private void tool_SysMenu_ItemClick(object sender, ItemClickEventArgs e)
    {
        ChildWinManagement.LoadMdiForm(this, typeof(FrmMenu));
    }

    private void tool_Function_ItemClick(object sender, ItemClickEventArgs e)
    {
        ChildWinManagement.LoadMdiForm(this, typeof(FrmFunction));
    }

    private void tool_Role_ItemClick(object sender, ItemClickEventArgs e)
    {
        ChildWinManagement.LoadMdiForm(this, typeof(FrmRole));
    }

    private void tool_OU_ItemClick(object sender, ItemClickEventArgs e)
    {
        ChildWinManagement.LoadMdiForm(this, typeof(FrmOu));
    }

    private void tool_User_ItemClick(object sender, ItemClickEventArgs e)
    {
        ChildWinManagement.LoadMdiForm(this, typeof(FrmUser));
    }

    private void tool_Code_ItemClick(object sender, ItemClickEventArgs e)
    {
        ChildWinManagement.LoadMdiForm(this, typeof(FrmSelectDataBase));
    }

    private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
    {
        ChildWinManagement.LoadMdiForm(this, typeof(BindTest));
    }

    private async void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
    {
        SplashScreenHelper.Show();
        barButtonItem2_ItemClick(sender, e);
        await _ribbonHelper.RefreshMenu();
        //根据权限屏蔽静态构建的菜单对象
        InitAuthorizedUi();
        "菜单刷新成功！".ShowSuccessTip(this);
        SplashScreenHelper.Close();
    }
    
    #endregion
        
    #region 异步更新时间
        
    //第二步：定义执行线程主体事件
    //线程主体方法
    private void backgroundWorkerShowTime_DoWork(object? sender, DoWorkEventArgs e)
    {
        while(!_backgroundWorker.CancellationPending){
            Thread.Sleep(1000);
            // 执行ProgressChanged事件，发送消息到主线程
            _backgroundWorker.ReportProgress(0, DateTimeHelper.GetServerDateTime());
        }
    }

    //第三步：定义执行UI更新事件
    //UI更新方法
    private void backgroundWorkerShowTime_ProgressChanged(object? sender, ProgressChangedEventArgs e)
    {
        lblCalendar.Caption = e.UserState.ToString();
    }

    #endregion
}