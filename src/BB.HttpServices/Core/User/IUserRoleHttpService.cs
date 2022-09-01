using BB.Entity.Security;
using BB.HttpServices.Base;
using Furion.RemoteRequest;

namespace BB.HttpServices.Core.User;

public interface IUserRoleHttpService : IHttpDispatchProxy, IBaseHttpService<UserRoleEntity>
{
    /// <summary>
    /// 通过用户角色ID方式获取对应的用户基本信息列表
    /// </summary>
    /// <param name="roleId">用户角色ID</param>
    /// <returns></returns>
    [Get("simpleUsersByRole")]
    Task<RESTfulResultControl<List<SimpleUserInfo>>> GetSimpleUsersByRoleAsync([QueryString]int roleId);

    /// <summary>
    /// 通过角色ID获取对应的用户列表
    /// </summary>
    /// <param name="roleId">角色ID</param>
    /// <returns></returns>
    [Get("usersByRole")]
    Task<RESTfulResultControl<List<UserInfo>>> GetUsersByRoleAsync([QueryString]int roleId);

    /// <summary>
    /// 更新用户的角色列表
    /// </summary>
    /// <param name="userid">用户ID</param>
    /// <param name="roleList">角色列表</param>
    [Put("roles")]
    Task UpdateRolesAsync([QueryString]int userid, [Body]List<int> roleList);

    /// <summary>
    /// 给指定角色添加用户
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="roleId">角色ID</param>
    [Post("user")]
    Task AddUserAsync([QueryString]int userId, [QueryString]int roleId);

    /// <summary>
    /// 从角色的用户列表中移除指定的用户
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="roleId">角色ID</param>
    [Delete("user")]
    Task RemoveUserAsync([QueryString]int userId, [QueryString]int roleId);

    /// <summary>
    /// 根据用户的ID获取对应的角色列表
    /// </summary>
    /// <param name="userId">用户的ID</param>
    /// <returns></returns>
    [Get("rolesByUser")]
    Task<RESTfulResultControl<List<RoleInfo>>> GetRolesByUserAsync([QueryString]int userId);

    /// <summary>
    /// 判断用户ID是否在指定的角色中
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="roleId">角色ID</param>
    /// <returns></returns>
    [Post("userInRole")]
    Task<RESTfulResultControl<bool>> UserInRoleAsync([QueryString]int userId, [QueryString]int roleId);

    /// <summary>
    /// 判断用户是否为公司管理员
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns></returns>
    [Post("userIsCompanyAdmin")]
    Task<RESTfulResultControl<bool>> UserIsCompanyAdminAsync([QueryString]int userId);

    /// <summary>
    /// 判断用户是否为超级管理员
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns></returns>
    [Post("userIsSuperAdmin")]
    Task<RESTfulResultControl<bool>> UserIsSuperAdminAsync([QueryString]int userId);

    /// <summary>
    /// 判断用户是否为管理员，超级管理员、公司级别的系统管理员均通过。
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns></returns>
    [Post("userIsAdmin")]
    Task<RESTfulResultControl<bool>> UserIsAdminAsync([QueryString]int userId);

    /// <summary>
    /// 获取管理员包含的用户基础信息列表
    /// </summary>
    /// <returns></returns>
    [Get("adminSimpleUsers")]
    Task<RESTfulResultControl<List<SimpleUserInfo>>> GetAdminSimpleUsersAsync();

    /// <summary>
    /// 为角色指定新的人员列表
    /// </summary>
    /// <param name="roleId">角色ID</param>
    /// <param name="newUserList">人员列表</param>
    /// <returns></returns>
    [Post("editRoleUsers")]
    Task<RESTfulResultControl<bool>> EditRoleUsersAsync([QueryString]int roleId, [Body]List<int> newUserList);

    /// <summary>
    /// HttpClient 拦截
    /// </summary>
    /// <param name="req"></param>
    [Interceptor(InterceptorTypes.Client)]
    static void OnClientCreating(HttpClient req)
    {
        // var builder = new UriBuilder(req.BaseAddress!);
        // var path = req.BaseAddress!.AbsolutePath;
        // builder.Path = $"{path}userRole/";
        // req.BaseAddress = builder.Uri;
    }
}