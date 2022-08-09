namespace BB.Tools.Entity;

/// <summary>
/// 应用程序信息
/// </summary>
public class AppInfo
{
    /// <summary>
    /// 单位名称
    /// </summary>
    public string AppUnit { get; set; }

    /// <summary>
    /// 程序名称
    /// </summary>
    public string AppName { get; set; }

    /// <summary>
    /// 应用程序全部名称(单位名称+程序名称)
    /// </summary>
    public string AppWholeName { get; set; }

    /// <summary>
    /// 系统类型
    /// </summary>
    public string SystemType { get; set; }

    /// <summary>
    /// 默认构造函数
    /// </summary>
    public AppInfo()
    { }

    /// <summary>
    /// 参数化构造函数
    /// </summary>
    /// <param name="appUnit">生产单位</param>
    /// <param name="appName">程序名称</param>
    /// <param name="appWholeName">程序全名</param>
    /// <param name="systemType">系统类型</param>
    public AppInfo(string appUnit, string appName, string appWholeName, string systemType)
    {
        AppUnit = appUnit;
        AppName = appName;
        AppWholeName = appWholeName;
        SystemType = systemType;
    }
}