using BB.Entity.Security;
using BB.HttpServices.Base;

namespace BB.HttpServices.Core.OU;

public class OURoleHttpService : BaseHttpService<OURoleEntity>
{
	private readonly IOURoleHttpService _ouRoleHttpService;

	public OURoleHttpService(IOURoleHttpService ouRoleHttpService) : base(ouRoleHttpService)
	{
		_ouRoleHttpService = ouRoleHttpService;
	}

    /// <summary>
    /// 为角色指定新的机构列表
    /// </summary>
    /// <param name="roleId">角色ID</param>
    /// <param name="newOuList">机构列表</param>
    /// <returns></returns>
    public async Task<bool> EditRoleOUsAsync(int roleId, List<string> newOuList)
    {
	    return (await _ouRoleHttpService.EditRoleOUsAsync(roleId, newOuList)).Data;
    }

	/// <summary>
	/// 根据机构的ID获取对应的角色列表
	/// </summary>
	/// <param name="ouId">机构的ID</param>
	/// <returns></returns>
	public async Task<List<RoleInfo>> GetRolesByOuAsync(string ouId)
	{
		return (await _ouRoleHttpService.GetRolesByOuAsync(ouId)).Data;
	}

	/// <summary>
	/// 给指定角色添加机构
	/// </summary>
	/// <param name="ouId">机构ID</param>
	/// <param name="roleId">角色ID</param>
	public async Task AddOUAsync(string ouId, int roleId)
	{
		await _ouRoleHttpService.AddOUAsync(ouId, roleId);
	}

	/// <summary>
	/// 从角色机构列表中，移除指定的机构
	/// </summary>
	/// <param name="ouId">机构ID</param>
	/// <param name="roleId">角色ID</param>
	public async Task RemoveOuAsync(string ouId, int roleId)
	{
		await _ouRoleHttpService.RemoveOuAsync(ouId, roleId);
	}

	/// <summary>
	/// 判断机构是否在指定的角色中
	/// </summary>
	/// <param name="ouId">机构ID</param>
	/// <param name="roleId">角色ID</param>
	/// <returns></returns>
	public async Task<bool> OuInRoleAsync(string ouId, int roleId)
	{
		return (await _ouRoleHttpService.OuInRoleAsync(ouId, roleId)).Data;
	}
}