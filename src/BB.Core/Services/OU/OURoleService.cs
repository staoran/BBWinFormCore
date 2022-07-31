using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BB.Core.DbContext;
using BB.Core.Services.Base;
using BB.Entity.Security;

namespace BB.Core.Services.OU;

public class OURoleService : BaseService<OURoleEntity>, ITransient
{
    public OURoleService(BaseRepository<OURoleEntity> repository) : base(repository)
    {
    }

    /// <summary>
    /// 为角色指定新的机构列表
    /// </summary>
    /// <param name="roleId">角色ID</param>
    /// <param name="newOuList">机构列表</param>
    /// <returns></returns>
    public async Task<bool> EditRoleOUsAsync(int roleId, List<string> newOuList)
    {
	    var ouRoleList = newOuList.Select(x => new OURoleEntity() { RoleId = roleId, OUId = x }).ToList();
	    return await UseTransactionAsync(async ()=>
	    {
		    await Repository.DeleteAsync(x => x.RoleId == roleId);
		    await Repository.InsertRangeAsync(ouRoleList);
	    });
    }

	/// <summary>
	/// 根据机构的ID获取对应的角色列表
	/// </summary>
	/// <param name="ouId">机构的ID</param>
	/// <returns></returns>
	public async Task<List<RoleInfo>> GetRolesByOuAsync(string ouId)
	{
		return await Repository.Db.Queryable<RoleInfo, OURoleEntity>((u, ou) => u.ID == ou.RoleId)
			.Where((_, ou) => ou.OUId == ouId).ToListAsync();
	}

	/// <summary>
	/// 从角色机构列表中，移除指定的机构
	/// </summary>
	/// <param name="ouId">机构ID</param>
	/// <param name="roleId">角色ID</param>
	public async Task RemoveOuAsync(string ouId, int roleId)
	{
		await DeleteAsync(x => x.OUId == ouId && x.RoleId == roleId);
	}

	/// <summary>
	/// 获取管理员包含的机构ID列表
	/// </summary>
	/// <returns></returns>
	internal async Task<List<string>> GetAdminOuiDsAsync()
	{
		return await Repository.Db.Queryable<OURoleEntity>()
			.Where(x => x.RoleId == RoleInfo.SUPER_ADMIN_ID)
			.Select(x => x.OUId)
			.ToListAsync();
	}

	/// <summary>
	/// 判断机构是否在指定的角色中
	/// </summary>
	/// <param name="ouId">机构ID</param>
	/// <param name="roleId">角色ID</param>
	/// <returns></returns>
	public async Task<bool> OuInRoleAsync(string ouId, int roleId)
	{
		return await Repository.IsAnyAsync(x => x.RoleId == roleId && x.OUId == ouId);
	}
}