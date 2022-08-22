using BB.Entity.Base;
using BB.Entity.Security;
using BB.HttpServices.Base;

namespace BB.HttpServices.Core.LoginLog;

public class LoginLogHttpService : BaseHttpService<LoginLogInfo>
{
    private readonly ILoginLogHttpService _loginLogHttpService;

    public LoginLogHttpService(ILoginLogHttpService loginLogHttpService) : base(loginLogHttpService)
    {
        _loginLogHttpService = loginLogHttpService;
    }

    /// <summary>
    /// 记录用户操作日志
    /// </summary>
    /// <param name="info">用户信息</param>
    /// <param name="systemType">系统类型ID</param>
    /// <param name="ip">IP地址</param>
    /// <param name="macAddr">Mac地址</param>
    /// <param name="note">备注说明</param>
    public async Task AddLoginLogAsync(LoginUserInfo info, string systemType, string ip, string macAddr, string note)
    {
        await _loginLogHttpService.AddLoginLogAsync(info, systemType, ip, macAddr, note);
    }

    /// <summary>
    /// 根据最后更新日前的数据获取数据
    /// </summary>
    /// <param name="lastUpdated">最后更新日前</param>
    /// <returns></returns>
    public async Task<List<LoginLogInfo>> GetListAsync(DateTime lastUpdated)
    {
        return (await _loginLogHttpService.GetListAsync(lastUpdated)).Data;
    }

    /// <summary>
    /// 如果目标不存在则插入，否则判断更新时间，如果目标较旧则更新
    /// </summary>
    /// <param name="infoList"></param>
    public async Task InsertOrUpdateAsync(List<LoginLogInfo> infoList)
    {
        await _loginLogHttpService.InsertOrUpdateAsync(infoList);
    }

    /// <summary>
    /// 删除一个月前的数据
    /// </summary>
    public async Task DeleteMonthLogAsync()
    {
        await _loginLogHttpService.DeleteMonthLogAsync();
    }

    /// <summary>
    /// 获取上一次（非刚刚登录）的登录日志
    /// </summary>
    /// <param name="userId">登录用户ID</param>
    /// <returns></returns>
    public async Task<LoginLogInfo> GetLastLoginInfoAsync(string userId)
    {
        return (await _loginLogHttpService.GetLastLoginInfoAsync(userId)).Data;
    }
}