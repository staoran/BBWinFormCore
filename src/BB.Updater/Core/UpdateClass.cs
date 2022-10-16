using System.ComponentModel;
using System.Net;
using System.Xml;
using System.Xml.Serialization;

namespace BB.Updater.Core;

/// <summary>
/// 程序更新事件参数
/// </summary>
public class ManifestEventArgs : EventArgs
{
    public Manifest Manifest { get; set; }
}

/// <summary>
/// 激活安装开始事件参数
/// </summary>
public class ActivationStartedEventArgs : EventArgs
{
    public Manifest Manifest { get; set; }

    public bool Cancel { get; set; }
}
    
/// <summary>
/// 安装完成事件参数
/// </summary>
public class ActivationCompletedEventArgs : AsyncCompletedEventArgs
{
    public ActivationCompletedEventArgs(Exception error, bool cancelled, object userState)
        : base(error, cancelled, userState)
    {
    }

    public Manifest Manifest { get; set; }
}

/// <summary>
/// 程序自动更新操作类，封装了文件下载、文件复制、文件解压等操作
/// </summary>
public class UpdateClass
{
    #region 变量属性
    private DownloadClass _downloader = new DownloadClass();
    private FileCopyClass _fileCopyer = new FileCopyClass();
    private UpdaterConfigurationView _updateCfgView = new UpdaterConfigurationView();
    private string _backupFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "backup");

    /// <summary>
    /// 封装的文件下载操作类
    /// </summary>
    public DownloadClass Downloader => _downloader;

    /// <summary>
    /// 封装的文件复制操作类
    /// </summary>
    public FileCopyClass FileCopyer => _fileCopyer;

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

    public event EventHandler<ManifestEventArgs> ActivationInitializing;

    public event EventHandler<ActivationCompletedEventArgs> ActivationCompleted;

    public event EventHandler<ActivationStartedEventArgs> ActivationStarted;

    public event EventHandler<FileCopyProgressChangedEventArgs> ActivationProgressChanged;

    public event EventHandler<FileCopyErrorEventArgs> ActivationError;

    #endregion

    #region 下载更新实现

    public UpdateClass()
    {
        _downloader.DownloadCompleted += downloader_DownloadCompleted;
        _downloader.DownloadError += downloader_DownloadError;
        _downloader.DownloadProgressChanged += downloader_DownloadProgressChanged;
        _fileCopyer.FileCopyError += fileCopyer_FileCopyError;
        _fileCopyer.FileCopyCompleted += fileCopyer_FileCopyCompleted;
        _fileCopyer.FileCopyProgressChanged += fileCopyer_FileCopyProgressChanged;
    }

    /// <summary>
    /// 是否有最新的版本
    /// </summary>
    public bool HasNewVersion
    {
        get
        {
            var m = CheckForUpdates();
            return m.Length > 0;
        }
    }

    /// <summary>
    /// 检查更新,返回更新清单列表
    /// </summary>
    /// <returns></returns>
    public Manifest[] CheckForUpdates()
    {
        _updateCfgView.Refresh();

        Uri uri = new Uri(_updateCfgView.ManifestUri);
        string doc = DownLoadFile(uri);
        XmlSerializer xser = new XmlSerializer(typeof(Manifest));
        if (!(xser.Deserialize(new XmlTextReader(doc, XmlNodeType.Document, null)) is Manifest manifest) ||
            manifest.Version == _updateCfgView.Version ||
            manifest.MyApplication.ApplicationId != _updateCfgView.ApplicationId)
        {
            return new Manifest[0];
        }
        return new[] { manifest };
    }

    /// <summary>
    /// 用于远程下载文件清单
    /// </summary>
    /// <param name="uri">文件清单网络路径</param>
    /// <returns></returns>
    private string DownLoadFile(Uri uri)
    {
        WebRequest request = WebRequest.Create(uri);
        request.Credentials = CredentialCache.DefaultCredentials;
        string response = String.Empty;
        using (WebResponse res = request.GetResponse())
        {
            using (StreamReader reader = new StreamReader(res.GetResponseStream(), true))
            {
                response = reader.ReadToEnd();
            }
        }
        return response;
    }

    /// <summary>
    /// 同步下载文件清单中的文件
    /// </summary>
    /// <param name="manifests">下载文件清单</param>
    public void Download(Manifest[] manifests)
    {
        foreach (var m in manifests)
        {
            _downloader.Download(m);
        }
    }

    /// <summary>
    /// 异步下载文件清单中的文件
    /// </summary>
    /// <param name="manifests">下载文件清单</param>
    public void DownloadAsync(Manifest[] manifests)
    {
        foreach (var m in manifests)
        {
            _downloader.DownloadAsync(m);
        }
    }

    /// <summary>
    /// 下载完毕后执行的启动操作
    /// </summary>
    /// <param name="manifests"></param>
    public void Activate(Manifest[] manifests)
    {
        foreach (var m in manifests)
        {
            OnActivationInitializing(new ManifestEventArgs() { Manifest = m });
            Backup(m);
            ActivationStartedEventArgs e = new ActivationStartedEventArgs() { Manifest = m };
            OnActivationStarted(e);
            if (e.Cancel)
            {
                Clear();
                break;
            }
            else
            {
                _fileCopyer.CopyAsync(m, _downloader.TempPath);
            }
        }
    }

    /// <summary>
    /// 备份操作
    /// </summary>
    /// <param name="manifest">文件清单</param>
    private void Backup(Manifest manifest)
    {
        try
        {
            string sourcePath = Path.GetFullPath(manifest.MyApplication.Location);
            string sFilename = string.Empty;
            string tFilename = string.Empty;
            if (!Directory.Exists(_backupFilePath))
            {
                Directory.CreateDirectory(_backupFilePath);
            }
            foreach (var file in manifest.ManifestFiles.Files)
            {
                tFilename = Path.Combine(_backupFilePath, file.Source);
                sFilename = Path.Combine(sourcePath, file.Source);
                if (File.Exists(sFilename))
                {
                    File.Copy(sFilename, tFilename, true);
                }
            }
        }
        catch
        {
        }
    }

    /// <summary>
    /// 回滚文件下载内容
    /// </summary>
    /// <param name="manifest"></param>
    public void Rollback(Manifest manifest)
    {
        try
        {
            string filename = string.Empty;
            foreach (var file in manifest.ManifestFiles.Files)
            {
                filename = Path.Combine(_backupFilePath, file.Source);
                File.Copy(filename, Path.Combine(Path.GetFullPath(manifest.MyApplication.Location), file.Source));
            }
            Directory.Delete(_backupFilePath, true);
        }
        catch
        {
        }
    }

    /// <summary>
    /// 清除临时文件
    /// </summary>
    private void Clear()
    {
        try
        {
            Directory.Delete(_backupFilePath, true);
            Directory.Delete(_downloader.TempPath, true);
        }
        catch
        { }
    } 

    #endregion

    #region 事件处理

    private void fileCopyer_FileCopyError(object? sender, FileCopyErrorEventArgs e)
    {
        OnActivationError(e);
    }

    private void fileCopyer_FileCopyProgressChanged(object? sender, FileCopyProgressChangedEventArgs e)
    {
        if (ActivationProgressChanged != null)
        {
            ActivationProgressChanged(sender, e);
        }
    }

    private void fileCopyer_FileCopyCompleted(object? sender, FileCopyCompletedEventArgs e)
    {
        Clear();
        try
        {
            _updateCfgView.Version = e.Manifest.Version;
        }
        catch
        {  }

        if (ActivationCompleted != null)
        {
            ActivationCompletedEventArgs evt = new ActivationCompletedEventArgs(e.Error, e.Cancelled, e.UserState)
            {
                Manifest = e.Manifest
            };
            OnActivationCompleted(evt);
        }
    }

    private void downloader_DownloadProgressChanged(object? sender, DownloadProgressEventArgs e)
    {
        if (DownloadProgressChanged != null)
        {
            DownloadProgressChanged(sender, e);
        }
    }

    private void downloader_DownloadError(object? sender, DownloadErrorEventArgs e)
    {
        if (DownloadError != null)
        {
            DownloadError(sender, e);
        }
    }

    private void downloader_DownloadCompleted(object? sender, DownloadCompleteEventArgs e)
    {
        if (DownloadCompleted != null)
        {
            DownloadCompleted(sender, e);
        }
    }

    private void OnActivationInitializing(ManifestEventArgs e)
    {
        if (ActivationInitializing != null)
        {
            ActivationInitializing(this, e);
        }
    }

    private void OnActivationStarted(ActivationStartedEventArgs e)
    {
        if (ActivationStarted != null)
        {
            ActivationStarted(this, e);
        }
    }

    private void OnActivationCompleted(ActivationCompletedEventArgs e)
    {
        if (ActivationCompleted != null)
        {
            ActivationCompleted(this, e);
        }
    }

    private void OnActivationError(FileCopyErrorEventArgs e)
    {
        if (ActivationError != null)
        {
            ActivationError(this, e);
        }
    }

    private void OnActivationProgressChanged(FileCopyProgressChangedEventArgs e)
    {
        if (ActivationProgressChanged != null)
        {
            ActivationProgressChanged(this, e);
        }
    }

    #endregion

}