using BB.Entity.Security;
using BB.HttpServices.Base;
using Furion.RemoteRequest;
using Furion.UnifyResult;

namespace BB.HttpServices.Core.Role;

public interface IRoleHttpService : IHttpDispatchProxy, IBaseHttpService<RoleInfo>
{
	/// <summary>
	/// 根据公司ID（机构ID）获取对应的角色列表
	/// </summary>
	/// <param name="companyId">公司ID（机构ID）</param>
	/// <returns></returns>
	Task<RESTfulResult<List<RoleInfo>>> GetRolesByCompanyAsync(string companyId);

	/// <summary>
	/// 根据角色名称查找角色对象
	/// </summary>
	/// <param name="roleName">角色名称</param>
	/// <param name="companyId">公司ID</param>
	/// <returns></returns>
	Task<RESTfulResult<RoleInfo>> GetRoleByNameAsync(string roleName, string companyId = null);

	/// <summary>
	/// 设置删除标志
	/// </summary>
	/// <param name="id">记录ID</param>
	/// <param name="deleted">是否删除</param>
	/// <returns></returns>
	Task<RESTfulResult<bool>> SetDeletedFlagAsync(object id, bool deleted = true);


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
	Task<RESTfulResult<bool>> EditRoleMenusAsync(int roleId, List<string> newList, string systemType);
}