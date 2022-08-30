using BB.Entity.Security;
using BB.HttpServices.Base;
using Furion.RemoteRequest;

namespace BB.HttpServices.Core.BlackIP;

public interface IBlackIPHttpService : IHttpDispatchProxy, IBaseHttpService<BlackIpInfo>
{
    /// <summary>
    /// 根据黑名单ID获取对应的用户ID列表
    /// </summary>
    /// <param name="id">黑名单ID</param>
    /// <returns></returns>
    [Get("userIdList")]
    Task<RESTfulResultControl<string>> GetUserIdListAsync(string id);

    /// <summary>
    /// 根据名单ID获取对应的用户列表
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Get("simpleUserByBlackIp")]
    Task<RESTfulResultControl<List<SimpleUserInfo>>> GetSimpleUserByBlackIpAsync(string id);

    /// <summary>
    /// 新增黑用户
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="blackId"></param>
    /// <returns></returns>
    [Post("user")]
    Task AddUserAsync(int userId, string blackId);

    /// <summary>
    /// 移除黑用户
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="blackId"></param>
    /// <returns></returns>
    [Delete("user")]
    Task RemoveUserAsync(int userId, string blackId);
        
    /// <summary>
    /// 根据用户ID和授权类型获取列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="type">授权类型</param>
    /// <returns></returns>
    [Get("byUser")]
    Task<RESTfulResultControl<List<BlackIpInfo>>> FindByUserAsync(int userId, AuthrizeType type);

    /// <summary>
    /// 检验IP的可访问性(白名单优先于黑名单），如果同时白名单、黑名名单都有同一IP，则也允许访问。
    /// </summary>
    /// <param name="ipAddress"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    [Post("validateIpAccess")]
    Task<RESTfulResultControl<bool>> ValidateIpAccessAsync(string ipAddress, int userId);

    /// <summary>
    /// HttpClient 拦截
    /// </summary>
    /// <param name="req"></param>
    [Interceptor(InterceptorTypes.Client)]
    static void OnClientCreating(HttpClient req)
    {
        // var builder = new UriBuilder(req.BaseAddress!);
        // var path = req.BaseAddress!.AbsolutePath;
        // builder.Path = $"{path}blackIP/";
        // req.BaseAddress = builder.Uri;
    }
}