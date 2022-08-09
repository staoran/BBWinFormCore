using System.Collections.Specialized;
using System.ComponentModel;
using System.Text;

namespace BB.Updater.Core;

/// <summary>
/// 文件复制进度报告事件参数
/// </summary>
public class FileCopyProgressChangedEventArgs : ProgressChangedEventArgs
{
    public FileCopyProgressChangedEventArgs(int progressPercentage, object userState)
        : base(progressPercentage, userState)
    {
    }

    /// <summary>
    /// 当前复制的字节数
    /// </summary>
    public double BytesToCopy { get; set; }

    /// <summary>
    /// 当前复制操作中的字节总数
    /// </summary>
    public double TotalBytesToCopy { get; set; }

    /// <summary>
    /// 当前复制的源文件名
    /// </summary>
    public string SourceFileName { get; set; }

    /// <summary>
    /// 当前复制的目标文件名
    /// </summary>
    public string TargetFileName { get; set; }

    public Manifest Manifest { get; set; }
}

/// <summary>
/// 文件复制完成事件参数
/// </summary>
public class FileCopyCompletedEventArgs : AsyncCompletedEventArgs
{
    public FileCopyCompletedEventArgs(Exception error, bool cancelled, object userState)
        : base(error, cancelled, userState)
    {
    }

    public Manifest Manifest { get; set; }
}

/// <summary>
/// 文件复制错误事件参数
/// </summary>
public class FileCopyErrorEventArgs : EventArgs
{
    public Exception Error { get; set; }

    public Manifest Manifest { get; set; }
}

/// <summary>
/// 文件复制组件类
/// </summary>
public class FileCopyClass : Component
{
    #region 变量定义
    private object _defaultTaskId = new object();
    private int _writeFileLength = 1024 * 64;

    private delegate void WorkerEventHandler(Manifest manifest, string sourcePath, AsyncOperation asyncOp);

    private SendOrPostCallback _onProgressReportDelegate;
    private SendOrPostCallback _onCompletedDelegate;

    private HybridDictionary _userStateToLifetime = new HybridDictionary();

    private Container _components = null; 
    #endregion

    #region 事件

    /// <summary>
    /// 文件复制进度事件
    /// </summary>
    public event EventHandler<FileCopyProgressChangedEventArgs> FileCopyProgressChanged;

    /// <summary>
    /// 文件复制完成事件
    /// </summary>
    public event EventHandler<FileCopyCompletedEventArgs> FileCopyCompleted;

    /// <summary>
    /// 文件复制错误事件
    /// </summary>
    public event EventHandler<FileCopyErrorEventArgs> FileCopyError;

    #endregion

    #region 构造及析构

    public FileCopyClass(IContainer container)
    {
        container.Add(this);
        InitializeComponent();
        InitializeDelegates();
    }

    public FileCopyClass()
    {
        InitializeComponent();
        InitializeDelegates();
    }

    protected virtual void InitializeDelegates()
    {
        _onProgressReportDelegate = ReportProgress;
        _onCompletedDelegate = CopyCompleted;
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

    #region 实现

    public int WriteFileLength
    {
        set => _writeFileLength = value;
    }

    public void Copy(Manifest manifest, string sourcePath)
    {
        string[] sourceFiles = null;
        string[] targetFiles = null;
        GetFiles(manifest, sourcePath, out sourceFiles, out targetFiles);
        for (int i = 0; i < sourceFiles.Length; i++)
        {
            if (!Directory.Exists(Path.GetDirectoryName(targetFiles[i])))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(targetFiles[i]));
            }
            File.Copy(sourceFiles[i], targetFiles[i], true);
        }
    }

    public void CopyAsync(Manifest manifest, string sourcePath)
    {
        CopyAsync(manifest, sourcePath, _defaultTaskId);
    }

    public void CopyAsync(Manifest manifest, string sourcePath, object taskId)
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

        WorkerEventHandler workerDelegate = FileCopyWorker;
        workerDelegate.BeginInvoke(manifest, sourcePath, asyncOp, null, null);
    }

    private bool TaskCanceled(object taskId)
    {
        return (_userStateToLifetime[taskId] == null);
    }

    public void CancelAsync()
    {
        CancelAsync(_defaultTaskId);
    }

    public void CancelAsync(object taskId)
    {
        if (_userStateToLifetime[taskId] is AsyncOperation asyncOp)
        {
            lock (_userStateToLifetime.SyncRoot)
            {
                _userStateToLifetime.Remove(taskId);
            }
        }
    }

    private void FileCopyWorker(Manifest manifest, string sourcePath, AsyncOperation asyncOp)
    {
        Exception exception = null;
        FileCopyProgressChangedEventArgs e = null;
        Stream rStream = null;
        Stream wStream = null;
        double writeBytes = 0;
        string[] sourceFiles = null; 
        string[] targetFiles = null;
        GetFiles(manifest, sourcePath,out sourceFiles,out targetFiles);

        if (!TaskCanceled(asyncOp.UserSuppliedState))
        {
            try
            {
                double totalBytes = GetFileLength(sourceFiles);
                byte[] buffer = new byte[_writeFileLength];
                int len = 0;
                int offset = 0;
                for (int i = 0; i < sourceFiles.Length; i++)
                {
                    try
                    {
                        if (!Directory.Exists(Path.GetDirectoryName(targetFiles[i])))
                        {
                            Directory.CreateDirectory(Path.GetDirectoryName(targetFiles[i]));
                        } 

                        rStream = new FileStream(sourceFiles[i], FileMode.Open, FileAccess.Read, FileShare.None);
                        wStream = new FileStream(targetFiles[i], FileMode.Create, FileAccess.Write, FileShare.None);
                        while ((len = rStream.Read(buffer, offset, _writeFileLength)) > 0)
                        {
                            wStream.Write(buffer, offset, len);
                            writeBytes += len;
                            e = new FileCopyProgressChangedEventArgs((int)(writeBytes / totalBytes * 100), asyncOp.UserSuppliedState)
                            {
                                SourceFileName = sourceFiles[i],
                                TargetFileName = targetFiles[i],
                                TotalBytesToCopy = totalBytes,
                                BytesToCopy = len,
                                Manifest = manifest
                            };
                            asyncOp.Post(_onProgressReportDelegate, e);
                            Thread.Sleep(1);
                        }
                    }
                    finally
                    {
                        DisposeStream(wStream);
                        DisposeStream(rStream);
                    }
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                OnFileCopyError(new FileCopyErrorEventArgs() { Error = ex, Manifest = manifest });
            }
        }

        CompletionMethod(manifest, exception, TaskCanceled(asyncOp.UserSuppliedState), asyncOp);
            
        //如果文件是压缩文件，则解压这些文件
        ZipFiles(e.Manifest);
    }

    private void GetFiles(Manifest manifest, string sourcePath,out string[] sourceFiles,out string[] targetFiles)
    {
        sourceFiles = new string[manifest.ManifestFiles.Files.Length];
        targetFiles = new string[manifest.ManifestFiles.Files.Length];
        string path = Path.GetFullPath(manifest.MyApplication.Location);
        for (int i = 0; i < manifest.ManifestFiles.Files.Length; i++)
        {
            sourceFiles[i] = Path.Combine(sourcePath, manifest.ManifestFiles.Files[i].Source);
            targetFiles[i] = Path.Combine(path, manifest.ManifestFiles.Files[i].Source);
        }
    }

    private void DisposeStream(Stream stream)
    {
        if (stream != null)
        {
            stream.Flush();
            stream.Close();
            stream.Dispose();
        }
    }

    private double GetFileLength(string[] sourceFiles)
    {
        double bytes = 0;
        foreach (var file in sourceFiles)
        {
            FileInfo fileInfo = new FileInfo(file);
            bytes += fileInfo.Length;
        }
        return bytes;
    }

    private void CopyCompleted(object operationState)
    {
        FileCopyCompletedEventArgs e = operationState as FileCopyCompletedEventArgs;

        OnFileCopyCompleted(e);
    }

    private void ReportProgress(object state)
    {
        FileCopyProgressChangedEventArgs e = state as FileCopyProgressChangedEventArgs;

        OnProgressChanged(e);
    }

    protected void OnFileCopyCompleted(FileCopyCompletedEventArgs e)
    {
        if (FileCopyCompleted != null)
        {
            FileCopyCompleted(this, e);
        }
    }

    /// <summary>
    /// 如果文件是压缩文件，则解压这些文件
    /// </summary>
    /// <param name="manifest"></param>
    private void ZipFiles(Manifest manifest)
    {
        if (manifest != null)
        {
            string path = Path.GetFullPath(manifest.MyApplication.Location);
            foreach (ManifestFile file in manifest.ManifestFiles.Files)
            {                    
                bool unzip = false;
                bool.TryParse(file.Unzip, out unzip);

                if (file.Source.EndsWith(".zip", StringComparison.OrdinalIgnoreCase) && unzip)
                {
                    string zipFile = Path.Combine(path, file.Source);
                    try
                    {
                        ZipUtility.UnZipFiles(zipFile, path, null, true);
                    }
                    catch (Exception ex)
                    {
                        WriteLine(ex.ToString());
                    }
                }
            }
        }
    }

    public static void WriteLine(string message)
    {
        string temp = DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss]    ") + message + "\r\n\r\n";
        string fileName = DateTime.Now.ToString("yyyyMMdd") + ".log";
        try
        {
            File.AppendAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName), temp, Encoding.GetEncoding("GB2312"));
        }
        catch
        {
        }
    }

    protected void OnProgressChanged(FileCopyProgressChangedEventArgs e)
    {
        if (FileCopyProgressChanged != null)
        {
            FileCopyProgressChanged(this, e);
        }
    }

    protected void OnFileCopyError(FileCopyErrorEventArgs e)
    {
        if (FileCopyError != null)
        {
            FileCopyError(this, e);
        }
    }

    private void CompletionMethod(Manifest manifest, Exception exception, bool canceled, AsyncOperation asyncOp)
    {
        if (!canceled)
        {
            lock (_userStateToLifetime.SyncRoot)
            {
                _userStateToLifetime.Remove(asyncOp.UserSuppliedState);
            }
        }

        FileCopyCompletedEventArgs e = new FileCopyCompletedEventArgs(exception, canceled, asyncOp.UserSuppliedState)
        {
            Manifest = manifest
        };
        asyncOp.PostOperationCompleted(_onCompletedDelegate, e);
    }

    #endregion

}