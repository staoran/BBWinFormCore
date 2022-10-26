using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using BB.Core.DbContext;
using BB.Core.Filter;
using BB.Core.Services.Base;
using BB.Entity.Security;
using FluentValidation;

namespace BB.Core.Services.Role;

[ApiDescriptionSettings("权限")]
public class RoleService : BaseService<RoleInfo>, IDynamicApiController, ITransient
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public RoleService(BaseRepository<RoleInfo> repository, IValidator<RoleInfo> validator) : base(repository, validator)
    {
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
    /// 判断Admin用户是否包含用户
    /// </summary>
    /// <returns></returns>
    internal async Task<bool> AdminHasUserAsync()
    {
        return await Repository.Db.Queryable<UserRoleEntity>()
            .Where(x => x.RoleId == RoleInfo.SUPER_ADMIN_ID).Select(x => x.UserId).AnyAsync();
    }

    public override async Task<bool> DeleteAsync([ModelBinder(typeof(ObjectModelBinder))][Required] object key)
    {
        if (Convert.ToInt32(key) == RoleInfo.SUPER_ADMIN_ID)
        {
            throw Oops.Bah("管理员角色 不能被删除！");
        }
        return await base.DeleteAsync(key);
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

    public override async Task<bool> InsertAsync(RoleInfo obj)
    {
        if (obj.ID == RoleInfo.SUPER_ADMIN_ID)
        {
            throw Oops.Bah("超级管理员为保留名称，不能新增使用！");
        }

        //检查不同ID是否还有其他相同关键字的记录
        if (await IsExistRecordAsync(x => x.Name == obj.Name && x.CompanyId == obj.CompanyId))
        {
            throw Oops.Bah("角色名称已存在！");
        }
        return await base.InsertAsync(obj);
    }

    /// <summary>
    /// 更新角色信息
    /// </summary>
    /// <param name="obj">角色对象</param>
    /// <returns></returns>
    public override async Task<bool> UpdateAsync(RoleInfo obj)
    {
        if (obj.ID == RoleInfo.SUPER_ADMIN_ID)
        {
            throw Oops.Bah("保留角色不能修改！");
        }

        //检查不同ID是否还有其他相同关键字的记录
        if (await IsExistRecordAsync(x => x.Name == obj.Name && x.ID != obj.ID))
        {
            throw Oops.Bah("角色名称已存在！");
        }
        return await base.UpdateAsync(obj);
    }

    /// <summary>
    /// 设置删除标志
    /// </summary>
    /// <param name="id">记录ID</param>
    /// <param name="deleted">是否删除</param>
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