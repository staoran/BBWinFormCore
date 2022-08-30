using BB.Entity.Security;
using BB.HttpServices.Base;

namespace BB.HttpServices.Core.SystemType;

public class SystemTypeHttpService : BaseHttpService<SystemTypeInfo>
{
    private readonly ISystemTypeHttpService _systemTypeHttpService;

    public SystemTypeHttpService(ISystemTypeHttpService systemTypeHttpService) : base(systemTypeHttpService)
    {
        _systemTypeHttpService = systemTypeHttpService;
    }

    /// <summary>
    /// 根据系统OID获取系统标识信息
    /// </summary>
    /// <param name="oid">系统OID</param>
    /// <returns></returns>
    public async Task<SystemTypeInfo> FindByOidAsync(string oid)
    {
        return (await _systemTypeHttpService.FindByOidAsync(oid)).Handling();
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
        return (await _systemTypeHttpService.VerifySystemAsync(serialNumber, typeId, authorizeAmount)).Handling();
    }
}