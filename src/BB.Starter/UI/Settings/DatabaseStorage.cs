using BB.Entity.Dictionary;
using Furion;
using SettingsProviderNet;

namespace BB.Starter.UI.Settings;

/// <summary>
/// 数据库参数存储设置
/// </summary>
public class DatabaseStorage : JsonSettingsStoreBase
{
    /// <summary>
    /// 保存的用户标识
    /// </summary>
    private readonly string _creator;

    /// <summary>
    /// 构造函数
    /// </summary>
    public DatabaseStorage()
    {
    }

    /// <summary>
    /// 参数构造函数
    /// </summary>
    /// <param name="creator">用户标识</param>
    public DatabaseStorage(string creator)
    {
        _creator = creator;
    }

    /// <summary>
    /// 保存到数据库
    /// </summary>
    /// <param name="filename">文件名称（类型名称）</param>
    /// <param name="fileContents">参数内容</param>
    protected override void WriteTextFile(string filename, string fileContents)
    {
        UserParameterInfo info = new UserParameterInfo
        {
            Name = filename,
            Content = fileContents,
            Creator = _creator
        };

        await App.GetService<IUserParameterHttpService>().SaveParamater(info);
    }

    /// <summary>
    /// 从数据库读取
    /// </summary>
    /// <param name="filename">文件名称（类型名称）</param>
    /// <returns></returns>
    protected override string ReadTextFile(string filename)
    {
        return await App.GetService<IUserParameterHttpService>().LoadParameter(filename, _creator);
    }
}