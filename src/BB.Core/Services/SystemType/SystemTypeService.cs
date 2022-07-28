using System.Threading.Tasks;
using BB.Core.DbContext;
using BB.Core.Services.Base;
using BB.Core.Services.Role;
using BB.Entity.Security;

namespace BB.Core.Services.SystemType;

public class SystemTypeService : BaseService<SystemTypeInfo>, ISystemTypeService
{
    private readonly IRoleService _roleService;

    public SystemTypeService(BaseRepository<SystemTypeInfo> repository, IRoleService roleService) : base(repository)
    {
        _roleService = roleService;
    }

    /// <summary>
    /// 重载删除处理
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public override async Task<bool> DeleteAsync(object key)
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
}