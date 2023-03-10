using BB.Entity.Security;
using BB.HttpServices.Base;
using Furion.RemoteRequest;

namespace BB.HttpServices.Core.OU;

public interface IOUUserHttpService : IHttpDispatchProxy, IBaseHttpService<OUUserEntity>
{
    /// <summary>
    /// 通过用户机构ID方式获取对应的用户基本信息列表
    /// </summary>
    /// <param name="ouId">用户机构ID方式</param>
    /// <returns></returns>
    [Get("simpleUsersByOu")]
    Task<RESTfulResultControl<List<SimpleUserInfo>>> GetSimpleUsersByOuAsync([QueryString]string ouId);

    /// <summary>
    /// 通过机构ID获取对应的用户列表
    /// </summary>
    /// <param name="ouId">机构ID</param>
    /// <returns></returns>
    [Get("usersByOu")]
    Task<RESTfulResultControl<List<UserInfo>>> GetUsersByOuAsync([QueryString]string ouId);

    /// <summary>
    /// 通过用户ID获取对应的机构列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns></returns>
    [Get("ousByUserId")]
    Task<RESTfulResultControl<List<OUInfo>>> GetOusByUserIdAsync(int userId);

    /// <summary>
    /// 通过用户ID获取对应的机构数据权限列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns></returns>
    [Get("ouIdsByUserId")]
    Task<RESTfulResultControl<List<string>>> GetOuIdsByUserIdAsync(int userId);

    /// <summary>
    /// 在机构中移除指定的用户
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="ouId">机构ID</param>
    [Delete("user")]
    Task RemoveUserAsync([QueryString]int userId, [QueryString]string ouId);

    /// <summary>
    /// HttpClient 拦截
    /// </summary>
    /// <param name="req"></param>
    [Interceptor(InterceptorTypes.Client)]
    static void OnClientCreating(HttpClient req)
    {
        // var builder = new UriBuilder(req.BaseAddress!);
        // var path = req.BaseAddress!.AbsolutePath;
        // builder.Path = $"{path}oUUser/";
        // req.BaseAddress = builder.Uri;
    }
}