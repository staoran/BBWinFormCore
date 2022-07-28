using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BB.Core.Services.Base;
using BB.Entity.Security;

namespace BB.Core.Services.LoginLog;

public interface ILoginLogService : IBaseService<LoginLogInfo>
{
    /// <summary>
    /// 记录用户操作日志
    /// </summary>
    /// <param name="info">用户信息</param>
    /// <param name="systemType">系统类型ID</param>
    /// <param name="ip">IP地址</param>
    /// <param name="macAddr">Mac地址</param>
    /// <param name="note">备注说明</param>
    Task AddLoginLogAsync(LoginLogInfo info, string systemType, string ip, string macAddr, string note);

    /// <summary>
    /// 根据最后更新日前的数据获取数据
    /// </summary>
    /// <param name="lastUpdated">最后更新日前</param>
    /// <returns></returns>
    Task<List<LoginLogInfo>> GetListAsync(DateTime lastUpdated);

    /// <summary>
    /// 如果目标不存在则插入，否则判断更新时间，如果目标较旧则更新
    /// </summary>
    /// <param name="infoList"></param>
    Task InsertOrUpdateAsync(List<LoginLogInfo> infoList);

    /// <summary>
    /// 删除一个月前的数据
    /// </summary>
    Task DeleteMonthLogAsync();

    /// <summary>
    /// 获取上一次（非刚刚登录）的登录日志
    /// </summary>
    /// <param name="userId">登录用户ID</param>
    /// <returns></returns>
    Task<LoginLogInfo> GetLastLoginInfoAsync(string userId);
}