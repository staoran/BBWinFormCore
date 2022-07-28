using System.Collections.Generic;
using System.Threading.Tasks;
using BB.Core.Services.Base;
using BB.Entity.Security;

namespace BB.Core.Services.Role;

public interface IRoleService : IBaseService<RoleInfo>
{
    /// <summary>
    /// 根据公司ID（机构ID）获取对应的角色列表
    /// </summary>
    /// <param name="companyId">公司ID（机构ID）</param>
    /// <returns></returns>
    Task<List<RoleInfo>> GetRolesByCompanyAsync(string companyId);

    /// <summary>
    /// 为角色添加操作权限
    /// </summary>
    /// <param name="functionId">功能ID</param>
    /// <param name="roleId">角色ID</param>
    Task AddFunctionAsync(string functionId, int roleId);

    /// <summary>
    /// 为角色添加机构
    /// </summary>
    /// <param name="ouId">机构ID</param>
    /// <param name="roleId">角色ID</param>
    Task AddOuAsync(string ouId, int roleId);

    /// <summary>
    /// 为角色添加用户
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="roleId">角色ID</param>
    Task AddUserAsync(int userId, int roleId);

    /// <summary>
    /// 为角色指定新的人员列表
    /// </summary>
    /// <param name="roleId">角色ID</param>
    /// <param name="newUserList">人员列表</param>
    /// <returns></returns>
    Task<bool> EditRoleUsersAsync(int roleId, List<int> newUserList);

    /// <summary>
    /// 为角色指定新的操作功能列表
    /// </summary>
    /// <param name="roleId">角色ID</param>
    /// <param name="newFunctionList">功能列表</param>
    /// <param name="systemType">系统类型</param>
    /// <returns></returns>
    Task<bool> EditRoleFunctionsAsync(int roleId, List<string> newFunctionList, string systemType);

    /// <summary>
    /// 为角色指定新的机构列表
    /// </summary>
    /// <param name="roleId">角色ID</param>
    /// <param name="newOuList">机构列表</param>
    /// <returns></returns>
    Task<bool> EditRoleOUsAsync(int roleId, List<string> newOuList);

    /// <summary>
    /// 获取管理员包含的用户基础信息列表
    /// </summary>
    /// <returns></returns>
    Task<List<SimpleUserInfo>> GetAdminSimpleUsersAsync();

    /// <summary>
    /// 根据角色名称查找角色对象
    /// </summary>
    /// <param name="roleName">角色名称</param>
    /// <param name="companyId">公司ID</param>
    /// <returns></returns>
    Task<RoleInfo> GetRoleByNameAsync(string roleName, string companyId = null);

    /// <summary>
    /// 获取对应功能的相关角色列表
    /// </summary>
    /// <param name="functionId">对应功能ID</param>
    /// <returns></returns>
    Task<List<RoleInfo>> GetRolesByFunctionAsync(string functionId);

    /// <summary>
    /// 根据机构的ID获取对应的角色列表
    /// </summary>
    /// <param name="ouId">机构的ID</param>
    /// <returns></returns>
    Task<List<RoleInfo>> GetRolesByOuAsync(string ouId);

    /// <summary>
    /// 根据用户的ID获取对应的角色列表
    /// </summary>
    /// <param name="userId">用户的ID</param>
    /// <returns></returns>
    Task<List<RoleInfo>> GetRolesByUserAsync(int userId);

    /// <summary>
    /// 从角色操作功能列表中，移除对应的功能
    /// </summary>
    /// <param name="functionId">功能ID</param>
    /// <param name="roleId">角色ID</param>
    Task RemoveFunctionAsync(string functionId, int roleId);

    /// <summary>
    /// 从角色机构列表中，移除指定的机构
    /// </summary>
    /// <param name="ouId">机构ID</param>
    /// <param name="roleId">角色ID</param>
    Task RemoveOuAsync(string ouId, int roleId);

    /// <summary>
    /// 从角色的用户列表中移除指定的用户
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="roleId">角色ID</param>
    Task RemoveUserAsync(int userId, int roleId);

    /// <summary>
    /// 设置删除标志
    /// </summary>
    /// <param name="id">记录ID</param>
    /// <param name="deleted">是否删除</param>
    /// <param name="trans">事务对象</param>
    /// <returns></returns>
    Task<bool> SetDeletedFlagAsync(object id, bool deleted = true);

    /// <summary>
    /// 给指定角色添加菜单
    /// </summary>
    /// <param name="menuId">菜单ID</param>
    /// <param name="roleId">角色ID</param>
    Task AddMenuAsync(string menuId, int roleId);

    /// <summary>
    /// 为指定角色移除对应菜单
    /// </summary>
    /// <param name="menuId">菜单ID</param>
    /// <param name="roleId">角色ID</param>
    Task RemoveMenuAsync(string menuId, int roleId);

    /// <summary>
    /// 为角色指定新的菜单列表
    /// </summary>
    /// <param name="roleId">角色ID</param>
    /// <param name="newList">菜单列表</param>
    /// <param name="systemType">系统类型</param>
    /// <returns></returns>
    Task<bool> EditRoleMenusAsync(int roleId, List<string> newList, string systemType);
}