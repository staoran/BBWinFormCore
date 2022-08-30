using BB.Entity.Security;
using BB.HttpServices.Base;
using Furion.RemoteRequest;

namespace BB.HttpServices.Core.OperationLogSetting;

public interface IOperationLogSettingHttpService : IHttpDispatchProxy, IBaseHttpService<OperationLogSettingInfo>
{
    /// <summary>
    /// 判断指定的表名称是否需要记录操作日志（是否在配置表里面，并是有效状态）
    /// </summary>
    /// <param name="tableName">表名称</param>
    /// <returns></returns>
    [Post("isTableNeedToLog")]
    Task<RESTfulResultControl<bool>> IsTableNeedToLogAsync(string tableName);

    /// <summary>
    /// 根据数据库表名称获取配置信息
    /// </summary>
    /// <param name="tableName">数据库表名</param>
    /// <returns></returns>
    [Get("byTableName")]
    Task<RESTfulResultControl<OperationLogSettingInfo>> FindByTableNameAsync(string tableName);

    /// <summary>
    /// HttpClient 拦截
    /// </summary>
    /// <param name="req"></param>
    [Interceptor(InterceptorTypes.Client)]
    static void OnClientCreating(HttpClient req)
    {
        // var builder = new UriBuilder(req.BaseAddress!);
        // var path = req.BaseAddress!.AbsolutePath;
        // builder.Path = $"{path}operationLogSetting/";
        // req.BaseAddress = builder.Uri;
    }
}