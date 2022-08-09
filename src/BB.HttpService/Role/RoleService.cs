using BB.Entity.Security;
using BB.HttpService.Base;

namespace BB.HttpService.Role;

public class RoleHttpService : BaseHttpService<RoleInfo>
{
	private readonly IRoleHttpService _roleHttpService;

	/// <summary>
	/// 构造函数
	/// </summary>
	public RoleHttpService(IRoleHttpService roleHttpService) : base(roleHttpService)
	{
		_roleHttpService = roleHttpService;
	}

	/// <summary>
	/// 根据公司ID（机构ID）获取对应的角色列表
	/// </summary>
	/// <param name="companyId">公司ID（机构ID）</param>
	/// <returns></returns>
	public async Task<List<RoleInfo>> GetRolesByCompanyAsync(string companyId)
	{
		return (await _roleHttpService.GetRolesByCompanyAsync(companyId)).Data;
	}

	/// <summary>
	/// 根据角色名称查找角色对象
	/// </summary>
	/// <param name="roleName">角色名称</param>
	/// <param name="companyId">公司ID</param>
	/// <returns></returns>
	public async Task<RoleInfo> GetRoleByNameAsync(string roleName, string companyId = null)
	{
		return (await _roleHttpService.GetRoleByNameAsync(roleName, companyId)).Data;
	}
                       
	/// <summary>
	/// 设置删除标志
	/// </summary>
	/// <param name="id">记录ID</param>
	/// <param name="deleted">是否删除</param>
	/// <returns></returns>
	public async Task<bool> SetDeletedFlagAsync(object id, bool deleted = true)
	{
		return (await _roleHttpService.SetDeletedFlagAsync(id, deleted)).Data;
	}

              
	/// <summary>
	/// 给指定角色添加菜单
	/// </summary>
	/// <param name="menuId">菜单ID</param>
	/// <param name="roleId">角色ID</param>
	public async Task AddMenuAsync(string menuId, int roleId)
	{
		await _roleHttpService.AddMenuAsync(menuId, roleId);
	}

	/// <summary>
	/// 为指定角色移除对应菜单
	/// </summary>
	/// <param name="menuId">菜单ID</param>
	/// <param name="roleId">角色ID</param>
	public async Task RemoveMenuAsync(string menuId, int roleId)
	{
		await _roleHttpService.RemoveMenuAsync(menuId, roleId);
	}

	/// <summary>
	/// 为角色指定新的菜单列表
	/// </summary>
	/// <param name="roleId">角色ID</param>
	/// <param name="newList">菜单列表</param>
	/// <param name="systemType">系统类型</param>
	/// <returns></returns>
	public async Task<bool> EditRoleMenusAsync(int roleId, List<string> newList, string systemType)
	{
		return (await _roleHttpService.EditRoleMenusAsync(roleId, newList, systemType)).Data;
	}
}