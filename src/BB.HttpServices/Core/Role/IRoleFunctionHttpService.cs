using BB.Entity.Security;
using BB.HttpServices.Base;
using Furion.UnifyResult;

namespace BB.HttpServices.Core.Role;

public interface IRoleFunctionHttpService : IBaseHttpService<RoleFunction>
{
    /// <summary>
    /// 获取对应功能的相关角色列表
    /// </summary>
    /// <param name="functionId">对应功能ID</param>
    /// <returns></returns>
    Task<RESTfulResult<List<RoleInfo>>> GetRolesByFunctionAsync(string functionId);

    /// <summary>
    /// 给指定角色添加功能点
    /// </summary>
    /// <param name="functionId">功能ID</param>
    /// <param name="roleId">角色ID</param>
    Task AddFunctionAsync(string functionId, int roleId);

    /// <summary>
    /// 从角色操作功能列表中，移除对应的功能
    /// </summary>
    /// <param name="functionId">功能ID</param>
    /// <param name="roleId">角色ID</param>
    Task RemoveFunctionAsync(string functionId, int roleId);

    /// <summary>
    /// 为角色指定新的操作功能列表
    /// </summary>
    /// <param name="roleId">角色ID</param>
    /// <param name="newFunctionList">功能列表</param>
    /// <returns></returns>
    Task<RESTfulResult<bool>> EditRoleFunctionsAsync(int roleId, List<string> newFunctionList);
}