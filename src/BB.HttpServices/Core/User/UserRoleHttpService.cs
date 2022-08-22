using BB.Entity.Security;
using BB.HttpServices.Base;

namespace BB.HttpServices.Core.User;

public class UserRoleHttpService : BaseHttpService<UserRoleEntity>
{
    private readonly IUserRoleHttpService _userRoleHttpService;

    public UserRoleHttpService(IUserRoleHttpService userRoleHttpService) : base(userRoleHttpService)
    {
        _userRoleHttpService = userRoleHttpService;
    }

    /// <summary>
    /// 通过用户角色ID方式获取对应的用户基本信息列表
    /// </summary>
    /// <param name="roleId">用户角色ID</param>
    /// <returns></returns>
    public async Task<List<SimpleUserInfo>> GetSimpleUsersByRoleAsync(int roleId)
    {
        return (await _userRoleHttpService.GetSimpleUsersByRoleAsync(roleId)).Data;
    }

    /// <summary>
    /// 通过角色ID获取对应的用户列表
    /// </summary>
    /// <param name="roleId">角色ID</param>
    /// <returns></returns>
    public async Task<List<UserInfo>> GetUsersByRoleAsync(int roleId)
    {
        return (await _userRoleHttpService.GetUsersByRoleAsync(roleId)).Data;
    }

    /// <summary>
    /// 更新用户的角色列表
    /// </summary>
    /// <param name="userid">用户ID</param>
    /// <param name="roleList">角色列表</param>
    public async Task UpdateRolesAsync(int userid, List<int> roleList)
    {
        await _userRoleHttpService.UpdateRolesAsync(userid, roleList);
    }

    /// <summary>
    /// 给指定角色添加用户
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="roleId">角色ID</param>
    public async Task AddUserAsync(int userId, int roleId)
    {
        await _userRoleHttpService.AddUserAsync(userId, roleId);
    }

    /// <summary>
    /// 从角色的用户列表中移除指定的用户
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="roleId">角色ID</param>
    public async Task RemoveUserAsync(int userId, int roleId)
    {
        await _userRoleHttpService.RemoveUserAsync(userId, roleId);
    }

    /// <summary>
    /// 根据用户的ID获取对应的角色列表
    /// </summary>
    /// <param name="userId">用户的ID</param>
    /// <returns></returns>
    public async Task<List<RoleInfo>> GetRolesByUserAsync(int userId)
    {
        return (await _userRoleHttpService.GetRolesByUserAsync(userId)).Data;
    }

    /// <summary>
    /// 判断用户ID是否在指定的角色中
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="roleId">角色ID</param>
    /// <returns></returns>
    public async Task<bool> UserInRoleAsync(int userId, int roleId)
    {
        return (await _userRoleHttpService.UserInRoleAsync(userId, roleId)).Data;
    }

    /// <summary>
    /// 判断用户是否为公司管理员
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns></returns>
    public async Task<bool> UserIsCompanyAdminAsync(int userId)
    {
        return (await _userRoleHttpService.UserIsCompanyAdminAsync(userId)).Data;
    }

    /// <summary>
    /// 判断用户是否为超级管理员
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns></returns>
    public async Task<bool> UserIsSuperAdminAsync(int userId)
    {
        return (await _userRoleHttpService.UserIsSuperAdminAsync(userId)).Data;
    }

    /// <summary>
    /// 判断用户是否为管理员，超级管理员、公司级别的系统管理员均通过。
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns></returns>
    public async Task<bool> UserIsAdminAsync(int userId)
    {
        return (await _userRoleHttpService.UserIsAdminAsync(userId)).Data;
    }

    /// <summary>
    /// 为角色指定新的人员列表
    /// </summary>
    /// <param name="roleId">角色ID</param>
    /// <param name="newUserList">人员列表</param>
    /// <returns></returns>
    public async Task<bool> EditRoleUsersAsync(int roleId, List<int> newUserList)
    {
        return (await _userRoleHttpService.EditRoleUsersAsync(roleId, newUserList)).Data;
    }
}