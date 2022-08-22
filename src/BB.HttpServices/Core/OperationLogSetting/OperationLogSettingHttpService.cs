using BB.Entity.Security;
using BB.HttpServices.Base;

namespace BB.HttpServices.Core.OperationLogSetting;

public class OperationLogSettingHttpService : BaseHttpService<OperationLogSettingInfo>
{
    private readonly IOperationLogSettingHttpService _operationLogSettingHttpService;

    public OperationLogSettingHttpService(IOperationLogSettingHttpService operationLogSettingHttpService) : base(operationLogSettingHttpService)
    {
        _operationLogSettingHttpService = operationLogSettingHttpService;
    }

    /// <summary>
    /// 判断指定的表名称是否需要记录操作日志（是否在配置表里面，并是有效状态）
    /// </summary>
    /// <param name="tableName">表名称</param>
    /// <returns></returns>
    public async Task<bool> IsTableNeedToLogAsync(string tableName)
    {
        return (await _operationLogSettingHttpService.IsTableNeedToLogAsync(tableName)).Data;
    }

    /// <summary>
    /// 根据数据库表名称获取配置信息
    /// </summary>
    /// <param name="tableName">数据库表名</param>
    /// <returns></returns>
    public async Task<OperationLogSettingInfo> FindByTableNameAsync(string tableName)
    {
        return (await _operationLogSettingHttpService.FindByTableNameAsync(tableName)).Data;
    }
}