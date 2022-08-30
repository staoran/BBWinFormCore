using BB.HttpServices.Base;
using Furion.RemoteRequest;

namespace BB.HttpServices.Core.FieldControlConfig;

public interface IFieldControlConfigHttpService : IHttpDispatchProxy, IBaseHttpService<Entity.Security.FieldControlConfig>
{
    /// <summary>
    /// 获取数据库的所有表名称
    /// </summary>
    /// <returns></returns>
    [Get("tableNames")]
    Task<RESTfulResultControl<List<string>>> GetTableNames();
                       
    /// <summary>
    /// 获取表的主键
    /// </summary>
    /// <param name="name">表名</param>
    /// <returns></returns>
    [Get("tableKeyList")]
    Task<RESTfulResultControl<IEnumerable<string>>> GetTableKeyList(string name);
                       
    /// <summary>
    /// 获取表的自增字段
    /// </summary>
    /// <param name="name">表名</param>
    /// <returns></returns>
    [Get("tableIdentityList")]
    Task<RESTfulResultControl<List<string>>> GetTableIdentityList(string name);
                       
    /// <summary>
    /// 获取表的注释
    /// </summary>
    /// <param name="name">表名</param>
    /// <returns></returns>
    [Get("tableComment")]
    Task<RESTfulResultControl<string>> GetTableComment(string name);
                       
    /// <summary>
    /// 获取表的控件配置模版
    /// </summary>
    /// <param name="name">表名</param>
    /// <returns></returns>
    [Get("fieldControlConfigs")]
    Task<RESTfulResultControl<IEnumerable<Entity.Security.FieldControlConfig>>> GetFieldControlConfigs(string name);

    /// <summary>
    /// 获取数据库的全部表名称和注释
    /// </summary>
    /// <returns></returns>
    [Get("tableNamesAndComments")]
    Task<RESTfulResultControl<IEnumerable<string>>> GetTableNamesAndComments();

    /// <summary>
    /// HttpClient 拦截
    /// </summary>
    /// <param name="req"></param>
    [Interceptor(InterceptorTypes.Client)]
    static void OnClientCreating(HttpClient req)
    {
        // var builder = new UriBuilder(req.BaseAddress!);
        // var path = req.BaseAddress!.AbsolutePath;
        // builder.Path = $"{path}fieldControlConfig/";
        // req.BaseAddress = builder.Uri;
    }
}