using System.Collections.Specialized;
using System.ComponentModel;
using System.Net;
using System.Text;

namespace BB.Updater.Core;

/// <summary>
/// 下载错误事件数据
/// </summary>
public class DownloadErrorEventArgs : EventArgs
{
    public Exception Error { get; set; }

    public Manifest Manifest { get; set; }
}

/// <summary>
/// 下载进度事件数据
/// </summary>
public class DownloadProgressEventArgs : ProgressChangedEventArgs
{
    public DownloadProgressEventArgs(int progressPercentage, object userState)
        : base(progressPercentage,userState)
    { }

    /// <summary>
    /// 当前下载的文件名
    /// </summary>
    public string FileName { get; set; }

    /// <summary>
    /// 获取收到的字节数。
    /// </summary>
    public long BytesReceived { get; set; }
    /// <summary>
    /// 获取 System.Net.WebClient 数据下载操作中的字节总数。
    /// </summary>
    public long TotalBytesToReceive { get; set; }
}

/// <summary>
/// 下载完成事件数据
/// </summary>
public class DownloadCompleteEventArgs : AsyncCompletedEventArgs
{
    public DownloadCompleteEventArgs(Exception error, bool cancelled, object userState)
        : base(error, cancelled, userState)
    { 
    }

    public Manifest Manifest { get; set; }
}

/// <summary>
/// 服务器文件下载类
/// </summary>
public class DownloadClass : Component
{
    #region 变量定义
    private WebClient _webClient = new WebClient();
    private Manifest _manifest;
    private int _fileCount = 0;
    private bool _cancel = false;
    private string _tempPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "temp");

    private HybridDictionary _userStateToLifetime = new HybridDictionary();
    private object _defaultTaskId = new object();
    private delegate void WorkerEventHandler(AsyncOperation asyncOp);
    private Container _components = null;
    private SendOrPostCallback _onProgressReportDelegate;
    private SendOrPostCallback _onCompletedDelegate;
    private AsyncOperation _current; 
    #endregion

    #region 事件
    /// <summary>
    /// 下载进度
    /// </summary>
    public event EventHandler<DownloadProgressEventArgs> DownloadProgressChanged;

    /// <summary>
    /// 下载完成事件
    /// </summary>
    public event EventHandler<DownloadCompleteEventArgs> DownloadCompleted;

    /// <summary>
    /// 下载错误触发的事件
    /// </summary>
    public event EventHandler<DownloadErrorEventArgs> DownloadError; 
    #endregion

    #region 构造及析构
    public DownloadClass(IContainer container)
    {
        container.Add(this);
        InitializeComponent();
        InitializeDelegates();
    }

    public DownloadClass()
    {
        InitializeComponent();
        InitializeDelegates();
    }

    /// <summary>
    /// 初始化代理
    /// </summary>
    protected virtual void InitializeDelegates()
    {
        _onProgressReportDelegate = ReportProgress;
        _onCompletedDelegate = DoDownloadCompleted;
    }

    private void InitializeComponent()
    {
        _components = new Container();
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (_components != null)
            {
                _components.Dispose();
            }
        }
        base.Dispose(disposing);
    } 
    #endregion

    /// <summary>
    /// 触发下载进度事件
    /// </summary>
    /// <param name="e"></param>
    protected virtual void OnDownloadProgressChanged(DownloadProgressEventArgs e)
    {
        if (DownloadProgressChanged != null)
        {
            DownloadProgressChanged(this, e);
        }
    }

    /// <summary>
    /// 触发下载完成事件
    /// </summary>
    /// <param name="e"></param>
    protected virtual void OnDownloadCompleted(DownloadCompleteEventArgs e)
    {
        if (DownloadCompleted != null)
        {
            DownloadCompleted(this, e);
        }
    }

    /// <summary>
    /// 触发下载错误事件
    /// </summary>
    /// <param name="e"></param>
    protected virtual void OnDownloadError(DownloadErrorEventArgs e)
    {
        if (DownloadError != null)
        {
            DownloadError(this, e);
        }
    }

    /// <summary>
    /// 下载文字保存的临时目录
    /// </summary>
    public string TempPath
    {
        get => _tempPath;
        set => _tempPath = value;
    }

    /// <summary>
    /// 同步下载
    /// </summary>
    /// <param name="manifest">文件下载清单</param>
    public void Download(Manifest manifest)
    {
        Init(manifest);
        foreach (var file in manifest.ManifestFiles.Files)
        {
            string serverFileName = Path.Combine(manifest.ManifestFiles.BaseUrl, file.Source);
            string clientFileName = Path.Combine(_tempPath, file.Source);
            Uri uri = new Uri(serverFileName);
            if (!Directory.Exists(Path.GetDirectoryName(clientFileName)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(clientFileName));
            }
            _webClient.DownloadFile(uri, clientFileName);
        }
    }

    /// <summary>
    /// 异步下载
    /// </summary>
    /// <param name="manifest">文件下载清单</param>
    public void DownloadAsync(Manifest manifest)
    {
        Init(manifest);
        DownloadAsync(manifest,_defaultTaskId);
    }

    /// <summary>
    /// 异步下载并指定任务Id
    /// </summary>
    /// <param name="manifest">文件下载清单</param>
    /// <param name="taskId">任务Id</param>
    public void DownloadAsync(Manifest manifest,object taskId)
    {
        AsyncOperation asyncOp = AsyncOperationManager.CreateOperation(taskId);
        lock (_userStateToLifetime.SyncRoot)
        {
            if (_userStateToLifetime.Contains(taskId))
            {
                throw new ArgumentException("参数taskId必须是唯一的", "taskId");
            }
            _userStateToLifetime[taskId] = asyncOp;
        }
        WorkerEventHandler workerDelegate = DownloadWorker;
        workerDelegate.BeginInvoke(asyncOp, null, null);
    }

    private void Init(Manifest manifest)
    {
        _manifest = manifest;
        _webClient.BaseAddress = manifest.ManifestFiles.BaseUrl;
        _webClient.Credentials = CredentialCache.DefaultCredentials;
        _webClient.Encoding = Encoding.UTF8;
    }

    /// <summary>
    /// 异步下载方法
    /// </summary>
    /// <param name="asyncOp"></param>
    private void DownloadWorker(AsyncOperation asyncOp)
    {
        _current = asyncOp;
        if (!TaskCanceled(asyncOp.UserSuppliedState))
        {
            try
            {
                _webClient.DownloadFileCompleted += webClient_DownloadFileCompleted;
                _webClient.DownloadProgressChanged += webClient_DownloadProgressChanged;
                foreach (var file in _manifest.ManifestFiles.Files)
                {
                    string serverFileName = Path.Combine(_manifest.ManifestFiles.BaseUrl, file.Source);
                    string clientFileName = Path.Combine(_tempPath, file.Source);
                    Uri uri = new Uri(serverFileName);
                    if (!Directory.Exists(Path.GetDirectoryName(clientFileName)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(clientFileName));
                    }
                    while (_webClient.IsBusy)
                    {
                        //阻塞异步下载
                    }
                    if (!_cancel)
                    {
                        _webClient.DownloadFileAsync(uri, clientFileName, file.Source);
                    }
                }
            }
            catch (Exception ex)
            {
                DownloadErrorEventArgs e = new DownloadErrorEventArgs
                {
                    Error = ex,
                    Manifest = _manifest
                };
                OnDownloadError(e);
            }
        }
    }

    /// <summary>
    /// 异步完成方法
    /// </summary>
    /// <param name="exception">异常数据</param>
    /// <param name="canceled">是否取消</param>
    /// <param name="asyncOp"></param>
    private void CompletionMethod(Exception exception, bool canceled, AsyncOperation asyncOp)
    {
        if (!canceled)
        {
            lock (_userStateToLifetime.SyncRoot)
            {
                _userStateToLifetime.Remove(asyncOp.UserSuppliedState);
            }
        }

        DownloadCompleteEventArgs e = new DownloadCompleteEventArgs(exception, canceled, asyncOp.UserSuppliedState)
        {
            Manifest = _manifest
        };
        asyncOp.PostOperationCompleted(_onCompletedDelegate, e);
        _current = null;
    }

    /// <summary>
    /// 异步下载进度事件（仅对于单个文件）
    /// </summary>
    void webClient_DownloadProgressChanged(object? sender, DownloadProgressChangedEventArgs e)
    {
        DownloadProgressEventArgs args = new DownloadProgressEventArgs(e.ProgressPercentage, e.UserState)
        {
            BytesReceived = e.BytesReceived,
            FileName = e.UserState.ToString(),
            TotalBytesToReceive = e.TotalBytesToReceive
        };
        if (_current != null)
        {
            _current.Post(_onProgressReportDelegate, args);
        }
    }

    /// <summary>
    /// 异步下载完成事件（仅对于单个文件）
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void webClient_DownloadFileCompleted(object? sender, AsyncCompletedEventArgs e)
    {
        _fileCount++;
        if (_fileCount == _manifest.ManifestFiles.Files.Length)
        {
            CompletionMethod(e.Error, TaskCanceled(_current.UserSuppliedState), _current);
        }
    }

    /// <summary>
    /// 取消异步下载
    /// </summary>
    public void CancelAsync()
    {
        CancelAsync(_defaultTaskId);
    }

    /// <summary>
    /// 取消异步下载
    /// </summary>
    public void CancelAsync(object taskId)
    {
        _webClient.CancelAsync();
        _cancel = true;
        _current = null;
        if (_userStateToLifetime[taskId] is AsyncOperation asyncOp)
        {
            lock (_userStateToLifetime.SyncRoot)
            {
                _userStateToLifetime.Remove(taskId);
            }
        }
    }

    private bool TaskCanceled(object taskId)
    {
        return _cancel || (_userStateToLifetime[taskId] == null);
    }

    private void DoDownloadCompleted(object? operationState)
    {
        DownloadCompleteEventArgs e = operationState as DownloadCompleteEventArgs;
        OnDownloadCompleted(e);
    }

    private void ReportProgress(object? state)
    {
        DownloadProgressEventArgs e = state as DownloadProgressEventArgs;
        OnDownloadProgressChanged(e);
    }
}