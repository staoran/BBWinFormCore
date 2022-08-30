using BB.Entity.Security;
using BB.HttpServices.Base;

namespace BB.HttpServices.Core.RoleData;

public class RoleDataHttpService : BaseHttpService<RoleDataInfo>
{
    private readonly IRoleDataHttpService _roleDataHttpService;

    public RoleDataHttpService(IRoleDataHttpService roleDataHttpService) : base(roleDataHttpService)
    {
        _roleDataHttpService = roleDataHttpService;
    }

    /// <summary>
    /// 获取用户所属角色对应的管理公司列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns></returns>
    public async Task<List<string>> GetBelongCompanysByUserAsync(int userId)
    {
        return (await _roleDataHttpService.GetBelongCompanysByUserAsync(userId)).Handling();
    }

    /// <summary>
    /// 获取用户所属角色对应的管理公司列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns></returns>
    public async Task<List<string>> GetBelongDeptsByUserAsync(int userId)
    {
        return (await _roleDataHttpService.GetBelongDeptsByUserAsync(userId)).Handling();
    }


    /// <summary>
    /// 获取用户所属角色对应的数据权限集合
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns></returns>
    public async Task<List<RoleDataInfo>> FindByUserAsync(int userId)
    {
        return (await _roleDataHttpService.FindByUserAsync(userId)).Handling();
    }

    /// <summary>
    /// 根据角色ID获取对应的记录对象
    /// </summary>
    /// <param name="roleId">角色ID</param>
    /// <returns></returns>
    public async Task<RoleDataInfo> FindByRoleIdAsync(int roleId)
    {
        return (await _roleDataHttpService.FindByRoleIdAsync(roleId)).Handling();
    }

    /// <summary>
    /// 保存角色的数据权限
    /// </summary>
    /// <param name="roleId">角色ID</param>
    /// <param name="belongCompanys">包含公司</param>
    /// <param name="belongDepts">包含部门</param>
    /// <returns></returns>
    public async Task<bool> UpdateRoleDataAsync(int roleId, string belongCompanys, string belongDepts)
    {
        return (await _roleDataHttpService.UpdateRoleDataAsync(roleId, belongCompanys, belongDepts)).Handling();
    }

    /// <summary>
    /// 获取数据库的配置，角色数据权限(不对所在公司，所在部门转义）
    /// </summary>
    /// <param name="roleId">角色ID</param>
    /// <returns></returns>
    public async Task<Dictionary<string, string>> GetRoleDataDictAsync(int roleId)
    {
        return (await _roleDataHttpService.GetRoleDataDictAsync(roleId)).Handling();
    }
}