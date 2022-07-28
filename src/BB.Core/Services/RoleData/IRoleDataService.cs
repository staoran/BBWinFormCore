using System.Collections.Generic;
using System.Threading.Tasks;
using BB.Core.Services.Base;
using BB.Entity.Security;

namespace BB.Core.Services.RoleData;

public interface IRoleDataService : IBaseService<RoleDataInfo>
{
    /// <summary>
    /// 获取用户所属角色对应的管理公司列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns></returns>
    Task<List<string>> GetBelongCompanysByUserAsync(int userId);

    /// <summary>
    /// 获取用户所属角色对应的管理公司列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns></returns>
    Task<List<string>> GetBelongDeptsByUserAsync(int userId);

    /// <summary>
    /// 获取用户所属角色对应的数据权限集合
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns></returns>
    Task<List<RoleDataInfo>> FindByUserAsync(int userId);

    /// <summary>
    /// 根据角色ID获取对应的记录对象
    /// </summary>
    /// <param name="roleId">角色ID</param>
    /// <returns></returns>
    Task<RoleDataInfo> FindByRoleIdAsync(int roleId);

    /// <summary>
    /// 保存角色的数据权限
    /// </summary>
    /// <param name="roleId">角色ID</param>
    /// <param name="belongCompanys">包含公司</param>
    /// <param name="belongDepts">包含部门</param>
    /// <returns></returns>
    Task<bool> UpdateRoleDataAsync(int roleId, string belongCompanys, string belongDepts);

    /// <summary>
    /// 获取数据库的配置，角色数据权限(不对所在公司，所在部门转义）
    /// </summary>
    /// <param name="roleId">角色ID</param>
    /// <returns></returns>
    Task<Dictionary<string, string>> GetRoleDataDictAsync(int roleId);
}