using BB.Entity.Security;
using BB.HttpServices.Base;
using Furion.RemoteRequest;

namespace BB.HttpServices.Core.OU;

public interface IOURoleHttpService : IHttpDispatchProxy, IBaseHttpService<OURoleEntity>
{
    /// <summary>
    /// 为角色指定新的机构列表
    /// </summary>
    /// <param name="roleId">角色ID</param>
    /// <param name="newOuList">机构列表</param>
    /// <returns></returns>
    [Post("editRoleOUs")]
    Task<RESTfulResultControl<bool>> EditRoleOUsAsync(int roleId, List<string> newOuList);

    /// <summary>
    /// 根据机构的ID获取对应的角色列表
    /// </summary>
    /// <param name="ouId">机构的ID</param>
    /// <returns></returns>
    [Get("rolesByOu")]
    Task<RESTfulResultControl<List<RoleInfo>>> GetRolesByOuAsync(string ouId);

    /// <summary>
    /// 给指定角色添加机构
    /// </summary>
    /// <param name="ouId">机构ID</param>
    /// <param name="roleId">角色ID</param>
    [Post("oU")]
    Task AddOUAsync(string ouId, int roleId);

    /// <summary>
    /// 从角色机构列表中，移除指定的机构
    /// </summary>
    /// <param name="ouId">机构ID</param>
    /// <param name="roleId">角色ID</param>
    [Delete("ou")]
    Task RemoveOuAsync(string ouId, int roleId);

    /// <summary>
    /// 判断机构是否在指定的角色中
    /// </summary>
    /// <param name="ouId">机构ID</param>
    /// <param name="roleId">角色ID</param>
    /// <returns></returns>
    [Post("ouInRole")]
    Task<RESTfulResultControl<bool>> OuInRoleAsync(string ouId, int roleId);

    /// <summary>
    /// HttpClient 拦截
    /// </summary>
    /// <param name="req"></param>
    [Interceptor(InterceptorTypes.Client)]
    static void OnClientCreating(HttpClient req)
    {
        // var builder = new UriBuilder(req.BaseAddress!);
        // var path = req.BaseAddress!.AbsolutePath;
        // builder.Path = $"{path}oURole/";
        // req.BaseAddress = builder.Uri;
    }
}