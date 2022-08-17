using BB.Entity.Security;
using BB.HttpServices.Base;
using Furion.UnifyResult;

namespace BB.HttpServices.Core.User;

public interface IUserRoleHttpService : IBaseHttpService<UserRoleEntity>
{
    /// <summary>
    /// 通过用户角色ID方式获取对应的用户基本信息列表
    /// </summary>
    /// <param name="roleId">用户角色ID</param>
    /// <returns></returns>
    Task<RESTfulResult<List<SimpleUserInfo>>> GetSimpleUsersByRoleAsync(int roleId);

    /// <summary>
    /// 通过角色ID获取对应的用户列表
    /// </summary>
    /// <param name="roleId">角色ID</param>
    /// <returns></returns>
    Task<RESTfulResult<List<UserInfo>>> GetUsersByRoleAsync(int roleId);

    /// <summary>
    /// 更新用户的角色列表
    /// </summary>
    /// <param name="userid">用户ID</param>
    /// <param name="roleList">角色列表</param>
    Task UpdateRolesAsync(int userid, List<int> roleList);

    /// <summary>
    /// 从角色的用户列表中移除指定的用户
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="roleId">角色ID</param>
    Task RemoveUserAsync(int userId, int roleId);

    /// <summary>
    /// 根据用户的ID获取对应的角色列表
    /// </summary>
    /// <param name="userId">用户的ID</param>
    /// <returns></returns>
    Task<RESTfulResult<List<RoleInfo>>> GetRolesByUserAsync(int userId);

    /// <summary>
    /// 判断用户ID是否在指定的角色中
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="roleId">角色ID</param>
    /// <returns></returns>
    Task<RESTfulResult<bool>> UserInRoleAsync(int userId, int roleId);

    /// <summary>
    /// 判断用户是否为公司管理员
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns></returns>
    Task<RESTfulResult<bool>> UserIsCompanyAdminAsync(int userId);

    /// <summary>
    /// 判断用户是否为超级管理员
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns></returns>
    Task<RESTfulResult<bool>> UserIsSuperAdminAsync(int userId);

    /// <summary>
    /// 判断用户是否为管理员，超级管理员、公司级别的系统管理员均通过。
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns></returns>
    Task<RESTfulResult<bool>> UserIsAdminAsync(int userId);

    /// <summary>
    /// 为角色指定新的人员列表
    /// </summary>
    /// <param name="roleId">角色ID</param>
    /// <param name="newUserList">人员列表</param>
    /// <returns></returns>
    Task<RESTfulResult<bool>> EditRoleUsersAsync(int roleId, List<int> newUserList);
}