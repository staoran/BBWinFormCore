using BB.Entity.Security;
using BB.HttpServices.Base;
using Furion.UnifyResult;

namespace BB.HttpServices.Core.SystemType;

public interface ISystemTypeHttpService : IBaseHttpService<SystemTypeInfo>
{
    /// <summary>
    /// 根据系统OID获取系统标识信息
    /// </summary>
    /// <param name="oid">系统OID</param>
    /// <returns></returns>
    Task<RESTfulResult<SystemTypeInfo>> FindByOidAsync(string oid);

    /// <summary>
    /// 验证系统是否被授权注册
    /// </summary>
    /// <param name="serialNumber">序列号</param>
    /// <param name="typeId">类型ID</param>
    /// <param name="authorizeAmount">授权数量</param>
    /// <returns></returns>
    Task<RESTfulResult<bool>> VerifySystemAsync(string serialNumber, string typeId, int authorizeAmount);
}