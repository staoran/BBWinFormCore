using BB.Entity.Base;
using BB.Tools.Entity;

namespace BB.BaseUI.PlugInInterface;

/// <summary>
/// 父窗体实现的权限控制接口
/// </summary>
public interface IFunction
{
    /// <summary>
    /// 初始化权限控制信息
    /// </summary>
    void InitFunction(LoginUserInfo userInfo, Dictionary<string, string> functionDict);

    /// <summary>
    /// 是否具有访问指定控制ID的权限
    /// </summary>
    /// <param name="controlId">功能控制ID</param>
    /// <returns></returns>
    bool HasFunction(string controlId);

    /// <summary>
    /// 登陆用户基础信息
    /// </summary>
    LoginUserInfo LoginUserInfo { get; set; }

    /// <summary>
    /// 登录用户具有的功能字典集合
    /// </summary>
    Dictionary<string, string> FunctionDict { get; set; }

    /// <summary>
    /// 应用程序基础信息
    /// </summary>
    AppInfo AppInfo { get; set; }

}