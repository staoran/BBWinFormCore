using System.IO;

namespace BB.BaseUI.LocalReport;

/// <summary>
/// 文件管理包装类
/// </summary>
internal class FileManagerWrapper
{
    private static Type _fileManagerType;

    private object _fileManager;
    private Action<FileManagerStatus> _setStatusDelegate;
    private Func<FileManagerStatus> _getStatusDelegate;
    private Func<int> _getCountDelegate;
    private Func<bool, Stream> _createPageDelegate;

    /// <summary>
    /// 静态构造函数
    /// </summary>
    static FileManagerWrapper()
    {
        _fileManagerType = typeof(Microsoft.Reporting.WinForms.LocalReport).Assembly.GetType("Microsoft.Reporting.WinForms.FileManager");
    }

    /// <summary>
    /// 默认构造函数
    /// </summary>
    public FileManagerWrapper()
    {
        _fileManager = Activator.CreateInstance(_fileManagerType);
        _getStatusDelegate = (Func<FileManagerStatus>)Delegate.CreateDelegate(
            typeof(Func<FileManagerStatus>), _fileManager, "get_Status");
        _setStatusDelegate = (Action<FileManagerStatus>)Delegate.CreateDelegate(
            typeof(Action<FileManagerStatus>), _fileManager, "set_Status");
        _getCountDelegate = (Func<int>)Delegate.CreateDelegate(
            typeof(Func<int>), _fileManager, "get_Count");

        _createPageDelegate = (Func<bool, Stream>)Delegate.CreateDelegate(
            typeof(Func<bool, Stream>), _fileManager, "CreatePage");
    }

    /// <summary>
    /// 文件管理对象
    /// </summary>
    public object FileManager => _fileManager;

    /// <summary>
    /// 创建页面
    /// </summary>
    /// <param name="register"></param>
    /// <returns></returns>
    public Stream CreatePage(bool register)
    {
        return _createPageDelegate(register);
    }

    /// <summary>
    /// 文件数量
    /// </summary>
    public int Count => _getCountDelegate();

    /// <summary>
    /// 状态
    /// </summary>
    public FileManagerStatus Status
    {
        get => _getStatusDelegate();
        set => _setStatusDelegate(value);
    }
}