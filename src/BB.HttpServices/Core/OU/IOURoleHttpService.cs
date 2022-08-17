using BB.Entity.Security;
using BB.HttpServices.Base;
using Furion.UnifyResult;

namespace BB.HttpServices.Core.OU;

public interface IOURoleHttpService : IBaseHttpService<OURoleEntity>
{
    /// <summary>
    /// 为角色指定新的机构列表
    /// </summary>
    /// <param name="roleId">角色ID</param>
    /// <param name="newOuList">机构列表</param>
    /// <returns></returns>
    Task<RESTfulResult<bool>> EditRoleOUsAsync(int roleId, List<string> newOuList);

    /// <summary>
    /// 根据机构的ID获取对应的角色列表
    /// </summary>
    /// <param name="ouId">机构的ID</param>
    /// <returns></returns>
    Task<RESTfulResult<List<RoleInfo>>> GetRolesByOuAsync(string ouId);

    /// <summary>
    /// 从角色机构列表中，移除指定的机构
    /// </summary>
    /// <param name="ouId">机构ID</param>
    /// <param name="roleId">角色ID</param>
    Task RemoveOuAsync(string ouId, int roleId);

    /// <summary>
    /// 判断机构是否在指定的角色中
    /// </summary>
    /// <param name="ouId">机构ID</param>
    /// <param name="roleId">角色ID</param>
    /// <returns></returns>
    Task<RESTfulResult<bool>> OuInRoleAsync(string ouId, int roleId);
}