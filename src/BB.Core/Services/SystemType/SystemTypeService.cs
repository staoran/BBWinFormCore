using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using BB.Core.DbContext;
using BB.Core.Filter;
using BB.Core.Services.Base;
using BB.Core.Services.Role;
using BB.Entity.Security;
using FluentValidation;

namespace BB.Core.Services.SystemType;

[ApiDescriptionSettings("基础资料")]
public class SystemTypeService : BaseService<SystemTypeInfo>, IDynamicApiController, ITransient
{
    private readonly RoleService _roleService;

    public SystemTypeService(BaseRepository<SystemTypeInfo> repository, IValidator<SystemTypeInfo> validator, RoleService roleService) : base(repository, validator)
    {
        _roleService = roleService;
    }

    /// <summary>
    /// 重载删除处理
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public override async Task<bool> DeleteAsync([ModelBinder(typeof(ObjectModelBinder))][Required]object key)
    {
        int count = await _roleService.GetRecordCountAsync();
        if (count == 1)
        {
            throw Oops.Bah("系统至少需要保留一个记录！");
        }
        return await base.DeleteAsync(key);
    }

    /// <summary>
    /// 根据系统OID获取系统标识信息
    /// </summary>
    /// <param name="oid">系统OID</param>
    /// <returns></returns>
    public async Task<SystemTypeInfo> FindByOidAsync(string oid)
    {
        string condition = $"OID='{oid}'";
        return await FindSingleAsync(condition);
    }

    /// <summary>
    /// 验证系统是否被授权注册
    /// </summary>
    /// <param name="serialNumber">序列号</param>
    /// <param name="typeId">类型ID</param>
    /// <param name="authorizeAmount">授权数量</param>
    /// <returns></returns>
    public async Task<bool> VerifySystemAsync(string serialNumber, string typeId, int authorizeAmount)
    {
        // Db.Queryable<int>().AS("T_ACL_SystemAuthorize").Where($"SystemType_OID='{typeId}' ").Select("ID").Count();
        var flag = false;
        var sql = $"SELECT Count(ID) As Records FROM T_ACL_SystemAuthorize WHERE SystemType_OID='{typeId}' ";
        int num = await Repository.Db.Ado.GetIntAsync(sql);
        if (num <= authorizeAmount)
        {
            sql =
                $"SELECT * FROM T_ACL_SystemAuthorize WHERE Content='{serialNumber}'  And SystemType_OID='{typeId}' ";

            flag = (await Repository.Db.Ado.GetDataReaderAsync(sql)).Read();

            if (!flag)
            {
                flag = num < authorizeAmount;
                if (flag)
                {
                    sql =
                        $"INSERT INTO T_ACL_SystemAuthorize (SystemType_OID,Content) VALUES ('{typeId}', '{serialNumber}') ";
                    await Repository.Db.Ado.ExecuteCommandAsync(sql);
                }
            }
        }

        return flag;
    }

    public override async Task<bool> UpdateAsync(SystemTypeInfo obj)
    {
        //检查不同ID是否还有其他相同关键字的记录
        if (await IsExistRecordAsync(x => x.Name == obj.Name && x.Oid != obj.Oid))
        {
            throw Oops.Bah("指定的【系统名称】已经存在，不能重复添加，请修改");
        }

        return await base.UpdateAsync(obj);
    }
}