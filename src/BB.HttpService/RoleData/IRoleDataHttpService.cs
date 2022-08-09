using BB.Entity.Security;
using BB.HttpService.Base;
using Furion.UnifyResult;

namespace BB.HttpService.RoleData;

public interface IRoleDataHttpService : IBaseHttpService<RoleDataInfo>
{
    /// <summary>
    /// 获取用户所属角色对应的管理公司列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns></returns>
    Task<RESTfulResult<List<string>>> GetBelongCompanysByUserAsync(int userId);

    /// <summary>
    /// 获取用户所属角色对应的管理公司列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns></returns>
    Task<RESTfulResult<List<string>>> GetBelongDeptsByUserAsync(int userId);

    /// <summary>
    /// 获取用户所属角色对应的数据权限集合
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns></returns>
    Task<RESTfulResult<List<RoleDataInfo>>> FindByUserAsync(int userId);

    /// <summary>
    /// 根据角色ID获取对应的记录对象
    /// </summary>
    /// <param name="roleId">角色ID</param>
    /// <returns></returns>
    Task<RESTfulResult<RoleDataInfo>> FindByRoleIdAsync(int roleId);

    /// <summary>
    /// 保存角色的数据权限
    /// </summary>
    /// <param name="roleId">角色ID</param>
    /// <param name="belongCompanys">包含公司</param>
    /// <param name="belongDepts">包含部门</param>
    /// <returns></returns>
    Task<RESTfulResult<bool>> UpdateRoleDataAsync(int roleId, string belongCompanys, string belongDepts);

    /// <summary>
    /// 获取数据库的配置，角色数据权限(不对所在公司，所在部门转义）
    /// </summary>
    /// <param name="roleId">角色ID</param>
    /// <returns></returns>
    Task<RESTfulResult<Dictionary<string, string>>> GetRoleDataDictAsync(int roleId);
}