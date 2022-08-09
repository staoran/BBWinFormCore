using System.Diagnostics;
using System.Runtime.InteropServices;
using BB.Updater.Core;

namespace BB.Updater;

public partial class MainForm : Form
{
    //主程序传入的参数，系统标示 是否需要重新启动主程序
    private string[] _args;
    //表示主程序打开时传入的参数
    private readonly static string OpenFlag = "121";
    private bool _isComplete = true;

    private UpdateClass _updater;
    private List<Manifest> _mList = new List<Manifest>();
    private int _mLen = 0;

    public MainForm()
    {
        InitializeComponent();
    }

    public MainForm(string[] args)
    {
        InitializeComponent();
        _args = args;
    }

    private void MainForm_Load(object sender, EventArgs e)
    {
        try
        {
            _updater = new UpdateClass();
            _updater.ActivationCompleted += ActivationCompleted;
            _updater.ActivationError += ActivationError;
            _updater.ActivationInitializing += ActivationInitializing;
            _updater.ActivationProgressChanged += ActivationProgressChanged;
            _updater.ActivationStarted += ActivationStarted;
            _updater.DownloadCompleted += DownloadCompleted;
            _updater.DownloadError += DownloadError;
            _updater.DownloadProgressChanged += DownloadProgressChanged;

            InitUpdater();
        }
        catch(Exception ex)
        {
            Log.Write("更新错误：" + ex.Message);
            MessageBox.Show("更新错误", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    private void InitUpdater()
    {
        //从配置文件动态设置更新标题
        UpdaterConfigurationView updateCfgView = new UpdaterConfigurationView();
        lblTitle.Text = updateCfgView.Title;

        var manifests = _updater.CheckForUpdates();
        _mLen = manifests.Length;
        if (_updater.HasNewVersion)
        {
            //显示本次更新内容
            string updateDescription = manifests[0].Description;
            lblUpdateLog.Text = $"更新说明：{updateDescription}";
            lblTitle.Update();

            if (_args != null && _args.Length > 0)
            {
                #region 关闭主程序
                try
                {
                    string entryPoint = manifests[0].MyApplication.EntryPoint.File;
                    KillProcessDos(entryPoint);
                }
                catch (Exception ex)
                {
                    Log.Write(ex.ToString());
                } 
                #endregion
            }
            _isComplete = false;
            _updater.DownloadAsync(manifests);
        }
        else
        {
            lab_filename.Text = "";                
            MessageBox.Show("您当前的版本已经是最新，不需要更新。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Exit();
        }
    }

    /// <summary>
    /// 利用C#的Process对象Kill方法关闭进程
    /// </summary>
    /// <param name="processName"></param>
    private void KillProcessNormal(string processName)
    {
        processName = processName.ToUpper().Replace(".EXE", "");
        if (!string.IsNullOrEmpty(processName))
        {
            foreach (Process thisproc in Process.GetProcesses())
            {
                if (processName.Equals(thisproc.ProcessName, StringComparison.OrdinalIgnoreCase))
                {
                    thisproc.Kill();
                    //Log.Write("kill " + ProcessName);
                }
            }
        }
    }

    /// <summary>
    /// 使用DOS关闭进程
    /// </summary>
    /// <param name="processName">进程名称</param>
    private void KillProcessDos(string processName)
    {
        RunCmd("taskkill /im " + processName + " /f ");
    }

    /// <summary>
    /// 运行DOS命令
    /// DOS关闭进程命令(ntsd -c q -p PID )PID为进程的ID
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    private void RunCmd(string command)
    {
        //實例一個Process類，啟動一個獨立進程
        Process p = new Process();

        //Process類有一個StartInfo屬性，這個是ProcessStartInfo類，包括了一些屬性和方法，下面我們用到了他的幾個屬性：
        p.StartInfo.FileName = "cmd.exe";           //設定程序名
        p.StartInfo.Arguments = "/c " + command;    //設定程式執行參數
        p.StartInfo.UseShellExecute = false;        //關閉Shell的使用
        p.StartInfo.RedirectStandardInput = true;   //重定向標準輸入
        p.StartInfo.RedirectStandardOutput = true;  //重定向標準輸出
        p.StartInfo.RedirectStandardError = true;   //重定向錯誤輸出
        p.StartInfo.CreateNoWindow = true;          //設置不顯示窗口
        p.Start();   //啟動

        //p.StandardInput.WriteLine(command);       //也可以用這種方式輸入要執行的命令
        //p.StandardInput.WriteLine("exit");        //不過要記得加上Exit要不然下一行程式執行的時候會當機
        p.StandardOutput.ReadToEnd();        //從輸出流取得命令執行結果

        while (!p.HasExited)
        {
            p.WaitForExit(1000);
        }
    }

    /// <summary>
    /// 系统退出
    /// </summary>
    private void Exit()
    {
        notifyIcon1.Visible = false;
        notifyIcon1.Dispose();
        Close();
        Environment.Exit(0);
    }

    /// <summary>
    /// 带参数启动指定的应用程序
    /// </summary>
    /// <param name="entryPoint">入口的应用程序</param>
    /// <param name="parameters">程序启动参数</param>
    private void Startup(string entryPoint, string parameters)
    {
        try
        {
            if (_args != null && _args.Length > 0)
            {
                if (_args[0] == OpenFlag)
                {
                    //关闭主程序
                    ExeCommand("taskkill /im " + Path.GetFileName(entryPoint) + " /f ");
                    //启动主程序
                    System.Threading.Thread.Sleep(1500);
                    Process.Start(entryPoint, parameters);
                }
            }
        }
        catch (Exception ex)
        {
            Log.Write(ex);
        }
    }

    /// <summary>
    /// DOS命令运行函数
    /// </summary>
    /// <param name="commandText"></param>
    private void ExeCommand(string commandText)
    {
        Process p = new Process();
        p.StartInfo.FileName = "cmd.exe";
        p.StartInfo.UseShellExecute = false;
        p.StartInfo.RedirectStandardInput = true;
        p.StartInfo.RedirectStandardOutput = true;
        p.StartInfo.RedirectStandardError = true;
        p.StartInfo.CreateNoWindow = true;
        try
        {
            p.Start();
            p.StandardInput.WriteLine(commandText);
            p.StandardInput.WriteLine("exit");
            //p.StandardOutput.ReadToEnd();
        }
        catch
        {

        }
    }

    private void pictureBox1_Click(object sender, EventArgs e)
    {
        if (_isComplete)
        {
            Exit();
        }
        else
        {
            Hide();
            notifyIcon1.Visible = true;
        }
    }

    private void MenuItem_exit_Click(object sender, EventArgs e)
    {
        Exit();
    }

    private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            if (Visible)
            {
                Hide();
                notifyIcon1.Visible = true;
            }
            else
            {
                Show();
                notifyIcon1.Visible = false;
            }
        }
    }

    #region 鼠标选中移动窗体

    [DllImport("user32.dll")]
    public static extern bool ReleaseCapture();
    [DllImport("user32.dll")]
    public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

    public const int WM_SYSCOMMAND = 0x0112;
    public const int SC_MOVE = 0xF010;
    public const int HTCAPTION = 0x0002;

    Point _mouseOff;//鼠标移动位置变量
    bool _leftFlag;//标签是否为左键
    private void Form_MouseDown(object sender, MouseEventArgs e)
    {
        ReleaseCapture();
        SendMessage(Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
    }

    private void Form_MouseMove(object sender, MouseEventArgs e)
    {
        if (_leftFlag)
        {
            Point mouseSet = MousePosition;
            mouseSet.Offset(_mouseOff.X, _mouseOff.Y); //设置移动后的位置
            Location = mouseSet;
        }
    }

    private void Form_MouseUp(object sender, MouseEventArgs e)
    {
        if (_leftFlag)
        {
            _leftFlag = false;//释放鼠标后标注为false;
        }
    }
    #endregion

    #region 事件处理

    void DownloadProgressChanged(object sender, DownloadProgressEventArgs e)
    {
        progressBar1.Value = e.ProgressPercentage;
        lab_percent.Text = e.ProgressPercentage + "%";
        lab_percent.Update();
        lab_fileinfo.Text = $"字节数:{e.BytesReceived}/{e.TotalBytesToReceive}";
        lab_fileinfo.Update();
        lab_filename.Text = "正在下载文件：" + e.FileName;
        lab_filename.Update();
    }

    void DownloadError(object sender, DownloadErrorEventArgs e)
    {
        Log.Write("下载过程中出现错误，错误描述：" + e.Error.Message + Environment.NewLine + "Version:" + e.Manifest.Version);
        MessageBox.Show("下载出错：" + e.Error.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    /// <summary>
    /// 文件下载完毕执行的操作
    /// </summary>
    void DownloadCompleted(object sender, DownloadCompleteEventArgs e)
    {
        _mList.Add(e.Manifest);
        if (_mList.Count == _mLen)
        {
            _updater.Activate(_mList.ToArray());
            _mList.Clear();
        }
    }

    void ActivationStarted(object sender, ActivationStartedEventArgs e)
    {
        lab_filename.Text = "开始安装，请稍后......";
        lab_filename.Update();
        e.Cancel = CheckActivation();
        if (e.Cancel)
        {
            lab_filename.Text = "安装已被取消";
            _isComplete = true;
        }
    }

    private bool CheckActivation()
    {
        bool cancel = false;
        //检查主程序（进程名称）是否打开，如果打开则提示
        string[] processName = { "Client", "Server" };
        foreach (string name in processName)
        {
            Process[] processes = Process.GetProcessesByName(name);
            if (processes != null && processes.Length != 0)
            {
                if (MessageBox.Show($"进程{name}正在运行中，请关闭后重试。", "系统提示",
                        MessageBoxButtons.RetryCancel, MessageBoxIcon.Information) == DialogResult.Cancel)
                {
                    cancel = true;
                    break;
                }
                else
                {
                    return CheckActivation();
                }
            }
        }
        return cancel;
    }

    void ActivationProgressChanged(object sender, FileCopyProgressChangedEventArgs e)
    {
        progressBar1.Value = e.ProgressPercentage;
        lab_percent.Text = e.ProgressPercentage + "%";
        lab_percent.Update();
        lab_fileinfo.Text = $"字节数:{e.BytesToCopy}/{e.TotalBytesToCopy}";
        lab_fileinfo.Update();
        lab_filename.Text = "正在安装：" + e.SourceFileName;
        lab_filename.Update();
    }

    void ActivationInitializing(object sender, ManifestEventArgs e)
    {
        lab_filename.Text = "正在初始化安装，请稍后......";
        lab_filename.Update();
        lab_percent.Text = "0%";
        lab_percent.Update();
        lab_fileinfo.Text = "";
        lab_fileinfo.Update();
        progressBar1.Value = 0;
    }

    void ActivationError(object sender, FileCopyErrorEventArgs e)
    {
        Log.Write("安装过程中出现错误，错误描述：" + e.Error.Message + Environment.NewLine + "Version:" + e.Manifest.Version);
        MessageBox.Show(this, "安装错误：" + e.Error.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        lab_filename.Text = "系统正在回滚";
        _updater.Rollback(e.Manifest);
    }

    void ActivationCompleted(object sender, ActivationCompletedEventArgs e)
    {
        //安装完成
        _isComplete = true;
        lab_filename.Text = "安装完成";
        lab_percent.Text = "100%";
        if (progressBar1.Value != progressBar1.Maximum)
        {
            progressBar1.Value = progressBar1.Maximum;
        }
        if (e.Error != null)
        {
            lab_filename.Text += "，但出现错误";
            lab_filename.Update();
        }
        else
        {
            lab_filename.Update();
            System.Threading.Thread.Sleep(1000);
            string filename = GetFileName(e.Manifest.MyApplication.Location, e.Manifest.MyApplication.EntryPoint.File);
            Startup(filename, e.Manifest.MyApplication.EntryPoint.Parameters);
            if (_args != null && _args.Length > 0)
            {
                Exit();
            }
        }
    }

    private string GetFileName(string location, string file)
    {
        return Path.Combine(Path.GetFullPath(location), file);
    }

    #endregion

}