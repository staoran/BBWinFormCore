using BB.Entity.Security;
using BB.HttpServices.Base;
using Furion.RemoteRequest;
using Furion.UnifyResult;

namespace BB.HttpServices.Core.SystemType;

public interface ISystemTypeHttpService : IHttpDispatchProxy, IBaseHttpService<SystemTypeInfo>
{
    /// <summary>
    /// 根据系统OID获取系统标识信息
    /// </summary>
    /// <param name="oid">系统OID</param>
    /// <returns></returns>
    [Get("byOid")]
    Task<RESTfulResult<SystemTypeInfo>> FindByOidAsync(string oid);

    /// <summary>
    /// 验证系统是否被授权注册
    /// </summary>
    /// <param name="serialNumber">序列号</param>
    /// <param name="typeId">类型ID</param>
    /// <param name="authorizeAmount">授权数量</param>
    /// <returns></returns>
    [Post("verifySystem")]
    Task<RESTfulResult<bool>> VerifySystemAsync(string serialNumber, string typeId, int authorizeAmount);

    /// <summary>
    /// HttpClient 拦截
    /// </summary>
    /// <param name="req"></param>
    [Interceptor(InterceptorTypes.Client)]
    static void OnClientCreating(HttpClient req)
    {
        var builder = new UriBuilder(req.BaseAddress!);
        var path = req.BaseAddress!.AbsolutePath;
        builder.Path = $"{path}systemType/";
        req.BaseAddress = builder.Uri;
    }
}