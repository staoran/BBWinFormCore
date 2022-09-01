using BB.Entity.Security;
using BB.HttpServices.Base;
using Furion.RemoteRequest;

namespace BB.HttpServices.Core.OperationLog;

public interface IOperationLogHttpService : IHttpDispatchProxy, IBaseHttpService<OperationLogInfo>
{

    /// <summary>
    /// 根据相关信息，写入用户的操作日志记录
    /// </summary>
    /// <param name="userId">操作用户</param>
    /// <param name="tableName">操作表名称</param>
    /// <param name="operationType">操作类型</param>
    /// <param name="note">操作详细表述</param>
    /// <returns></returns>
    [Post("onOperationLog")]
    Task<RESTfulResultControl<bool>> OnOperationLog([QueryString]string userId, [QueryString]string tableName, [QueryString]string operationType, [QueryString]string note);

    /// <summary>
    /// HttpClient 拦截
    /// </summary>
    /// <param name="req"></param>
    [Interceptor(InterceptorTypes.Client)]
    static void OnClientCreating(HttpClient req)
    {
        // var builder = new UriBuilder(req.BaseAddress!);
        // var path = req.BaseAddress!.AbsolutePath;
        // builder.Path = $"{path}operationLog/";
        // req.BaseAddress = builder.Uri;
    }
}