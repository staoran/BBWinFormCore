using System.Threading.Tasks;
using BB.Core.Services.Base;
using BB.Entity.Security;

namespace BB.Core.Services.SystemType;

public interface ISystemTypeService : IBaseService<SystemTypeInfo>
{
    /// <summary>
    /// 根据系统OID获取系统标识信息
    /// </summary>
    /// <param name="oid">系统OID</param>
    /// <returns></returns>
    Task<SystemTypeInfo> FindByOidAsync(string oid);

    /// <summary>
    /// 验证系统是否被授权注册
    /// </summary>
    /// <param name="serialNumber">序列号</param>
    /// <param name="typeId">类型ID</param>
    /// <param name="authorizeAmount">授权数量</param>
    /// <returns></returns>
    Task<bool> VerifySystemAsync(string serialNumber, string typeId, int authorizeAmount);
}