using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BB.Core.DbContext;
using BB.Core.Services.Base;
using BB.Entity.Base;
using BB.Entity.Security;

namespace BB.Core.Services.LoginLog;

public class LoginLogService : BaseService<LoginLogInfo>, IDynamicApiController, ITransient
{
    public LoginLogService(BaseRepository<LoginLogInfo> repository) : base(repository)
    {
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
        if (info == null) return;

        #region 记录用户登录操作
        try
        {
            var logInfo = new LoginLogInfo
            {
                IPAddress = ip,
                MacAddress = macAddr,
                LastUpdated = DateTime.Now,
                Note = note,
                SystemTypeId = systemType,
                UserId = info.ID.ToString(),
                FullName = info.FullName,
                LoginName = info.Name,
                CompanyId = info.CompanyId,
                CompanyName = info.CompanyName
            };

            await InsertAsync(logInfo);
        }
        catch (Exception ex)
        {
            ex.ToString().LogError();
        }
        #endregion
    }

    /// <summary>
    /// 根据最后更新日前的数据获取数据
    /// </summary>
    /// <param name="lastUpdated">最后更新日前</param>
    /// <returns></returns>
    public async Task<List<LoginLogInfo>> GetListAsync(DateTime lastUpdated)
    {
        return await Repository.GetListAsync(x => x.LastUpdated >= lastUpdated);
    }

    /// <summary>
    /// 如果目标不存在则插入，否则判断更新时间，如果目标较旧则更新
    /// </summary>
    /// <param name="infoList"></param>
    public async Task InsertOrUpdateAsync(List<LoginLogInfo> infoList)
    {
        if (infoList is { Count: > 0 })
        {
            foreach (LoginLogInfo info in infoList)
            {
                LoginLogInfo tempInfo = await FindByIdAsync(info.ID);
                if (tempInfo != null)
                {
                    if (tempInfo.LastUpdated < info.LastUpdated)
                    {
                        await UpdateAsync(info);
                    }
                }
                else
                {
                    await InsertAsync(info);
                }
            }
        }
    }

    /// <summary>
    /// 删除一个月前的数据
    /// </summary>
    public async Task DeleteMonthLogAsync()
    {
        await Repository.DeleteAsync(x => x.LastUpdated <= DateTime.Now.AddDays(-30));
    }

    /// <summary>
    /// 获取上一次（非刚刚登录）的登录日志
    /// </summary>
    /// <param name="userId">登录用户ID</param>
    /// <returns></returns>
    public async Task<LoginLogInfo> GetLastLoginInfoAsync(string userId)
    {
        List<LoginLogInfo> list = await Repository.AsQueryable()
            .Take(2)
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.LastUpdated)
            .ToListAsync();

        if (list.Count == 2)
        {
            return list[1];
        }

        return null;
    }
}