using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using BB.Core.DbContext;
using BB.Core.Services.Base;
using BB.Core.Services.OU;
using BB.Core.Services.User;
using BB.Entity.Security;

namespace BB.Core.Services.Role;

public class RoleService : BaseService<RoleInfo>, IDynamicApiController, ITransient
{
	/// <summary>
	/// 该ID(-99)实际为一个无效ID，当调用 FillAdminID 会初始化为真是的管理员ID，以后以该实际ID作为管理员的凭证
	/// </summary>
	private static int _mAdminId = -99;
	private readonly IUserService _userService;
	private readonly IOUService _ouService;

	/// <summary>
	/// 构造函数
	/// </summary>
	public RoleService(BaseRepository<RoleInfo> repository, IUserService userService, IOUService ouService) : base(repository)
	{
		_userService = userService;
		_ouService = ouService;
	}

	/// <summary>
	/// 根据公司ID（机构ID）获取对应的角色列表
	/// </summary>
	/// <param name="companyId">公司ID（机构ID）</param>
	/// <returns></returns>
	public async Task<List<RoleInfo>> GetRolesByCompanyAsync(string companyId)
	{
		string condition = $"Company_ID='{companyId}' and Deleted = 0 ";
		return await FindAsync(condition);
	}

	/// <summary>
	/// 为角色添加操作权限
	/// </summary>
	/// <param name="functionId">功能ID</param>
	/// <param name="roleId">角色ID</param>
	public async Task AddFunctionAsync(string functionId, int roleId)
	{
		var targetTable = "T_ACL_Role_Function";
		string checkSql =
			$"Select count(*) from {targetTable} Where Function_ID='{functionId}' AND Role_ID={roleId}";
		string insertSql = $"INSERT INTO {targetTable}(Function_ID, Role_ID) VALUES('{functionId}',{roleId})";

		await InsertCheckDuplicatedAsync(checkSql, insertSql);
	}

	/// <summary>
	/// 为角色添加机构
	/// </summary>
	/// <param name="ouId">机构ID</param>
	/// <param name="roleId">角色ID</param>
	public async Task AddOuAsync(string ouId, int roleId)
	{
		var targetTable = "T_ACL_OU_Role";
		string checkSql = $"Select count(*) from {targetTable} Where OU_ID='{ouId}' AND Role_ID={roleId}";
		string insertSql = $"INSERT INTO {targetTable}(OU_ID, Role_ID) VALUES('{ouId}',{roleId})";

		await InsertCheckDuplicatedAsync(checkSql, insertSql);
	}

	/// <summary>
	/// 为角色添加用户
	/// </summary>
	/// <param name="userId">用户ID</param>
	/// <param name="roleId">角色ID</param>
	public async Task AddUserAsync(int userId, int roleId)
	{
		await FillAdminIdAsync();
		if (roleId == _mAdminId)
		{
			await _userService.CancelExpireAsync(userId);
		}

		
		var targetTable = "T_ACL_User_Role";
		string checkSql = $"Select count(*) from {targetTable} Where User_ID={userId} AND Role_ID={roleId}";
		string insertSql = $"INSERT INTO {targetTable}(User_ID, Role_ID) VALUES({userId},{roleId})";

		await InsertCheckDuplicatedAsync(checkSql, insertSql);
	}

	/// <summary>
	/// 通用的插入检查处理
	/// </summary>
	/// <param name="checkSql">检查是否存在语句</param>
	/// <param name="insertSql">插入数据的语句</param>
	private async Task InsertCheckDuplicatedAsync(string checkSql, string insertSql)
	{
		if (await Repository.Db.Ado.GetIntAsync(checkSql) == 0)
		{
			await Repository.Db.Ado.ExecuteCommandAsync(insertSql);
		}
	}
                      
	/// <summary>
	/// 为角色指定新的人员列表
	/// </summary>
	/// <param name="roleId">角色ID</param>
	/// <param name="newUserList">人员列表</param>
	/// <returns></returns>
	public async Task<bool> EditRoleUsersAsync(int roleId, List<int> newUserList)
	{
		string sql = $"Delete from T_ACL_User_Role where Role_ID = {roleId} ";
		await Repository.SqlExecuteAsync(sql);

		foreach (int userId in newUserList)
		{
			await AddUserAsync(userId, roleId);
		}
		return true;
	}
               
	/// <summary>
	/// 为角色指定新的操作功能列表
	/// </summary>
	/// <param name="roleId">角色ID</param>
	/// <param name="newFunctionList">功能列表</param>
	/// <param name="systemType">系统类型</param>
	/// <returns></returns>
	public async Task<bool> EditRoleFunctionsAsync(int roleId, List<string> newFunctionList, string systemType)
	{
		string sql = string.Format(@"delete from T_ACL_Role_Function Where Function_ID in ");
            
		string condition = $@"Select t.Function_ID from T_ACL_Role_Function t inner join T_ACL_Function f 
            on t.Function_ID = f.ID where Role_ID ={roleId}";

		if(!string.IsNullOrEmpty(systemType))
		{
			condition += $" AND SystemType_ID='{systemType}'";
		}
		sql += $"({condition})";

		await Repository.SqlExecuteAsync(sql);

		foreach (string functionId in newFunctionList)
		{
			await AddFunctionAsync(functionId, roleId);
		}
		return true;
	}

	/// <summary>
	/// 为角色指定新的机构列表
	/// </summary>
	/// <param name="roleId">角色ID</param>
	/// <param name="newOuList">机构列表</param>
	/// <returns></returns>
	public async Task<bool> EditRoleOUsAsync(int roleId, List<string> newOuList)
	{
		string sql = $"Delete from T_ACL_OU_Role where Role_ID = {roleId} ";
		await Repository.SqlExecuteAsync(sql);

		foreach (string ouId in newOuList)
		{
			await AddOuAsync(ouId, roleId);
		}
		return true;
	}

	/// <summary>
	/// 判断Admin用户是否包含用户
	/// </summary>
	/// <returns></returns>
	internal async Task<bool> AdminHasUserAsync()
	{
		await FillAdminIdAsync();

		return (await _userService.GetSimpleUsersByRoleAsync(_mAdminId)).Count > 0;
	}

	/// <summary>
	/// 检查管理员角色不被移除
	/// </summary>
	/// <param name="roleId"></param>
	private async Task CanRemoveFromAdminAsync(int roleId)
	{
		await FillAdminIdAsync();

		if ((roleId == _mAdminId) && ((await GetAdminSimpleUsersAsync()).Count <= 1))
		{
			throw Oops.Bah("管理员角色 至少需要包含一个用户！");
		}
	}

	public override async Task<bool> DeleteAsync(object key)
	{
		await FillAdminIdAsync();

		if (Convert.ToInt32(key) == _mAdminId)
		{
			throw Oops.Bah("管理员角色 不能被删除！");
		}
		return await base.DeleteAsync(key);
	}

	/// <summary>
	/// 找到对应的角色名称（管理员），获取其对应的ID作为今后比较
	/// </summary>
	private async Task FillAdminIdAsync()
	{
		if (_mAdminId == -99)
		{
			string condition = $"Name='{RoleInfo.SUPER_ADMIN_NAME}' ";//超级管理员唯一性，不用公司区分
			RoleInfo roleByName = await FindSingleAsync(condition);
			if (roleByName != null)
			{
				_mAdminId = roleByName.ID;//保存ID作为管理员角色参考
			}
		}
	}

	/// <summary>
	/// 获取管理员包含的机构ID列表
	/// </summary>
	/// <returns></returns>
	internal async Task<List<string>> GetAdminOuiDsAsync()
	{
		await FillAdminIdAsync();

		List<OUInfo> oUsByRole = await _ouService.GetOUsByRoleAsync(_mAdminId);
		List<string> list = new();
		foreach (OUInfo info in oUsByRole)
		{
			list.Add(info.HandNo);
		}
		return list;
	}

	/// <summary>
	/// 获取管理员包含的用户基础信息列表
	/// </summary>
	/// <returns></returns>
	public async Task<List<SimpleUserInfo>> GetAdminSimpleUsersAsync()
	{
		await FillAdminIdAsync();

		List<SimpleUserInfo> simpleUsersByRole = await _userService.GetSimpleUsersByRoleAsync(_mAdminId);
		int count = simpleUsersByRole.Count;
		if (count <= 1)
		{
			foreach (OUInfo info in await _ouService.GetOUsByRoleAsync(_mAdminId))
			{
				List<SimpleUserInfo> simpleUsersByOu = await _userService.GetSimpleUsersByOuAsync(info.HandNo);
				if (simpleUsersByOu.Count > 0)
				{
					simpleUsersByRole.Add(simpleUsersByOu[0]);
					count++;
					if (simpleUsersByOu.Count > 1)
					{
						simpleUsersByRole.Add(simpleUsersByOu[1]);
						count++;
					}
					if (count > 1)
					{
						return simpleUsersByRole;
					}
				}
			}
		}
		return simpleUsersByRole;
	}

	/// <summary>
	/// 根据角色名称查找角色对象
	/// </summary>
	/// <param name="roleName">角色名称</param>
	/// <param name="companyId">公司ID</param>
	/// <returns></returns>
	public async Task<RoleInfo> GetRoleByNameAsync(string roleName, string companyId = null)
	{
		string condition = $"Name='{roleName}' ";
		if (!string.IsNullOrEmpty(companyId))
		{
			condition += $" And Company_ID='{companyId}' ";
		}
		return await FindSingleAsync(condition);
	}

	/// <summary>
	/// 获取对应功能的相关角色列表
	/// </summary>
	/// <param name="functionId">对应功能ID</param>
	/// <returns></returns>
	public async Task<List<RoleInfo>> GetRolesByFunctionAsync(string functionId)
	{
		string sql = $@"SELECT * FROM T_ACL_Role 
            INNER JOIN T_ACL_Role_Function On T_ACL_Role.ID=T_ACL_Role_Function.Role_ID WHERE Function_ID ='{functionId}' ";
		return await Repository.GetListAsync(sql);
	}

	/// <summary>
	/// 根据机构的ID获取对应的角色列表
	/// </summary>
	/// <param name="ouId">机构的ID</param>
	/// <returns></returns>
	public async Task<List<RoleInfo>> GetRolesByOuAsync(string ouId)
	{
		string sql = $"SELECT * FROM T_ACL_Role INNER JOIN T_ACL_OU_Role ON T_ACL_Role.ID=Role_ID WHERE OU_ID = '{ouId}'";
		return await Repository.GetListAsync(sql);
	}

	/// <summary>
	/// 根据用户的ID获取对应的角色列表
	/// </summary>
	/// <param name="userId">用户的ID</param>
	/// <returns></returns>
	public async Task<List<RoleInfo>> GetRolesByUserAsync(int userId)
	{
		string sql = "SELECT * FROM T_ACL_Role INNER JOIN T_ACL_User_Role On T_ACL_Role.ID=T_ACL_User_Role.Role_ID WHERE User_ID = " + userId;
		List<RoleInfo> rolesByUser = await Repository.GetListAsync(sql);

		List<int> list = new List<int>();
		foreach (RoleInfo info in rolesByUser)
		{
			list.Add(info.ID);
		}

		//包含部门中间表的角色
		foreach (OUInfo ouInfo in await _ouService.GetOUsByUserAsync(userId))
		{
			foreach (RoleInfo roleInfo in await GetRolesByOuAsync(ouInfo.HandNo))
			{
				if (!list.Contains(roleInfo.ID))
				{
					rolesByUser.Add(roleInfo);
					list.Add(roleInfo.ID);
				}
			}
		}

		//包含默认所属部门的角色
		UserInfo userInfo = await _userService.FindByIdAsync(userId);
		if (userInfo != null)
		{
			foreach (RoleInfo roleInfo in await GetRolesByOuAsync(userInfo.DeptId))
			{
				if (!list.Contains(roleInfo.ID))
				{
					rolesByUser.Add(roleInfo);
					list.Add(roleInfo.ID);
				}
			}
		}

		return rolesByUser;
	}

	/// <summary>
	/// 从角色操作功能列表中，移除对应的功能
	/// </summary>
	/// <param name="functionId">功能ID</param>
	/// <param name="roleId">角色ID</param>
	public async Task RemoveFunctionAsync(string functionId, int roleId)
	{
		string sql = $"DELETE FROM T_ACL_Role_Function WHERE Function_ID='{functionId}' AND Role_ID={roleId}";
		await Repository.SqlExecuteAsync(sql);
	}

	/// <summary>
	/// 从角色机构列表中，移除指定的机构
	/// </summary>
	/// <param name="ouId">机构ID</param>
	/// <param name="roleId">角色ID</param>
	public async Task RemoveOuAsync(string ouId, int roleId)
	{
		await FillAdminIdAsync();
		if (roleId == _mAdminId)
		{
			List<SimpleUserInfo> simpleUsersByRole = await _userService.GetSimpleUsersByRoleAsync(_mAdminId);
			if (simpleUsersByRole.Count < 1)
			{
				simpleUsersByRole.Clear();
				List<UserInfo> usersByOu = await _userService.GetUsersByOuAsync(ouId);
				if (usersByOu.Count > 0)
				{
					usersByOu.Clear();
					bool flag = false;
					List<OUInfo> oUsByRole = await _ouService.GetOUsByRoleAsync(_mAdminId);
					foreach (OUInfo info in oUsByRole)
					{
						if ((info.HandNo != ouId) && ((await _userService.GetSimpleUsersByOuAsync(info.HandNo)).Count > 0))
						{
							flag = true;
							break;
						}
					}
					oUsByRole.Clear();
					if (!flag)
					{
						throw Oops.Bah("管理员角色至少需要包含一个用户！");
					}
				}
			}
		}
		string sql = $"DELETE FROM T_ACL_OU_Role WHERE OU_ID='{ouId}' AND Role_ID={roleId}";
		await Repository.SqlExecuteAsync(sql);
	}

	/// <summary>
	/// 从角色的用户列表中移除指定的用户
	/// </summary>
	/// <param name="userId">用户ID</param>
	/// <param name="roleId">角色ID</param>
	public async Task RemoveUserAsync(int userId, int roleId)
	{
		await CanRemoveFromAdminAsync(roleId);
		string sql = $"DELETE FROM T_ACL_User_Role WHERE User_ID={userId} AND Role_ID={roleId}";
		await Repository.SqlExecuteAsync(sql);
	}

	/// <summary>
	/// 更新角色信息
	/// </summary>
	/// <param name="obj">角色对象</param>
	/// <param name="trans"></param>
	/// <returns></returns>
	public override async Task<bool> UpdateAsync(RoleInfo obj)
	{
		if (obj.ID == _mAdminId)
		{
			obj.Name = RoleInfo.SUPER_ADMIN_NAME;
		}
		return await UpdateAsync(obj);
	}
                       
	/// <summary>
	/// 设置删除标志
	/// </summary>
	/// <param name="id">记录ID</param>
	/// <param name="deleted">是否删除</param>
	/// <param name="trans">事务对象</param>
	/// <returns></returns>
	public async Task<bool> SetDeletedFlagAsync(object id, bool deleted = true)
	{
		Hashtable ht = new Hashtable()
		{
			{ RoleInfo.FieldDeleted, deleted },
			{ RoleInfo.PrimaryKey, id }
		};
		return await UpdateFieldsAsync(ht);
	}

              
	/// <summary>
	/// 给指定角色添加菜单
	/// </summary>
	/// <param name="menuId">菜单ID</param>
	/// <param name="roleId">角色ID</param>
	public async Task AddMenuAsync(string menuId, int roleId)
	{
		string sql = $"INSERT INTO T_ACL_Role_Menu(Menu_ID, Role_ID) VALUES('{menuId}',{roleId})";
		await Repository.SqlExecuteAsync(sql);
	}

	/// <summary>
	/// 为指定角色移除对应菜单
	/// </summary>
	/// <param name="menuId">菜单ID</param>
	/// <param name="roleId">角色ID</param>
	public async Task RemoveMenuAsync(string menuId, int roleId)
	{
		string sql = $"DELETE FROM T_ACL_Role_Menu WHERE Menu_ID='{menuId}' AND Role_ID={roleId}";
		await Repository.SqlExecuteAsync(sql);
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
		string sql = string.Format(@"delete from T_ACL_Role_Menu Where Menu_ID in ");

		string condition = $@"Select t.Menu_ID from T_ACL_Role_Menu t inner join T_ACL_Menu f 
            on t.Menu_ID = f.ID where Role_ID ={roleId}";

		if (!string.IsNullOrEmpty(systemType))
		{
			condition += $" AND SystemType_ID='{systemType}'";
		}
		sql += $"({condition})";

		await Repository.SqlExecuteAsync(sql);

		foreach (string menuId in newList)
		{
			await AddMenuAsync(menuId, roleId);
		}
		return true;
	}
}