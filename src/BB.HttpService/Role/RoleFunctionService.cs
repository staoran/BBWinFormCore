using BB.Entity.Security;
using BB.HttpService.Base;

namespace BB.HttpService.Role;

public class RoleFunctionHttpService : BaseHttpService<RoleFunction>
{
    private readonly IRoleFunctionHttpService _roleFunctionHttpService;

    public RoleFunctionHttpService(IRoleFunctionHttpService roleFunctionHttpService) : base(roleFunctionHttpService)
    {
        _roleFunctionHttpService = roleFunctionHttpService;
    }

    /// <summary>
    /// 获取对应功能的相关角色列表
    /// </summary>
    /// <param name="functionId">对应功能ID</param>
    /// <returns></returns>
    public async Task<List<RoleInfo>> GetRolesByFunctionAsync(string functionId)
    {
        return (await _roleFunctionHttpService.GetRolesByFunctionAsync(functionId)).Data;
    }

    /// <summary>
    /// 从角色操作功能列表中，移除对应的功能
    /// </summary>
    /// <param name="functionId">功能ID</param>
    /// <param name="roleId">角色ID</param>
    public async Task RemoveFunctionAsync(string functionId, int roleId)
    {
        await _roleFunctionHttpService.RemoveFunctionAsync(functionId, roleId);
    }

    /// <summary>
    /// 为角色指定新的操作功能列表
    /// </summary>
    /// <param name="roleId">角色ID</param>
    /// <param name="newFunctionList">功能列表</param>
    /// <returns></returns>
    public async Task<bool> EditRoleFunctionsAsync(int roleId, List<string> newFunctionList)
    {
        return (await _roleFunctionHttpService.EditRoleFunctionsAsync(roleId, newFunctionList)).Data;
    }
}