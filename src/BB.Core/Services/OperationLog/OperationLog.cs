using System;
using System.Threading.Tasks;
using BB.Core.DbContext;
using BB.Core.Services.Base;
using BB.Core.Services.OperationLogSetting;
using BB.Core.Services.User;
using BB.Entity.Security;
using FluentValidation;

namespace BB.Core.Services.OperationLog;

public class OperationLog : BaseService<OperationLogInfo>, IDynamicApiController, ITransient
{
    private readonly UserService _userService;
    private readonly OperationLogSettingService _operationLogSettingService;

    public OperationLog(BaseRepository<OperationLogInfo> repository, IValidator<OperationLogInfo> validator, UserService userService,
        OperationLogSettingService operationLogSettingService) : base(repository, validator)
    {
        _userService = userService;
        _operationLogSettingService = operationLogSettingService;
    }

    /// <summary>
    /// 根据相关信息，写入用户的操作日志记录
    /// </summary>
    /// <param name="userId">操作用户</param>
    /// <param name="tableName">操作表名称</param>
    /// <param name="operationType">操作类型</param>
    /// <param name="note">操作详细表述</param>
    /// <returns></returns>
    public async Task<bool> OnOperationLog(string userId, string tableName, string operationType, string note)
    {
        //虽然实现了这个事件，但是我们还需要判断该表是否在配置表里面，如果不在，则不记录操作日志。
        OperationLogSettingInfo settingInfo = await _operationLogSettingService.FindByTableNameAsync(tableName);
        if (settingInfo != null)
        {
            bool insert = operationType == "增加" && settingInfo.InsertLog;
            bool update = operationType == "修改" && settingInfo.UpdateLog;
            bool delete = operationType == "删除" && settingInfo.DeleteLog;
            if (insert || update || delete)
            {
                OperationLogInfo info = new OperationLogInfo
                {
                    TableName = tableName,
                    OperationType = operationType,
                    Note = note,
                    CreationDate = DateTime.Now
                };

                if (!string.IsNullOrEmpty(userId))
                {
                    UserInfo userInfo = await _userService.FindByIdAsync(userId);
                    if (userInfo != null)
                    {
                        info.UserId = userId;
                        info.LoginName = userInfo.Name;
                        info.FullName = userInfo.FullName;
                        info.CompanyId = userInfo.CompanyId;
                        info.CompanyName = userInfo.CompanyName;
                        info.MacAddress = userInfo.CurrentMacAddress;
                        info.IPAddress = userInfo.CurrentLoginIP;
                    }
                }

                return await InsertAsync(info);
            }
        }
        return false;
    }
}