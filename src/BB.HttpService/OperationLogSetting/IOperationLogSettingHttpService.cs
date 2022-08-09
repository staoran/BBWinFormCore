using BB.Entity.Security;
using BB.HttpService.Base;
using Furion.UnifyResult;

namespace BB.HttpService.OperationLogSetting;

public interface IOperationLogSettingHttpService : IBaseHttpService<OperationLogSettingInfo>
{
    /// <summary>
    /// 判断指定的表名称是否需要记录操作日志（是否在配置表里面，并是有效状态）
    /// </summary>
    /// <param name="tableName">表名称</param>
    /// <returns></returns>
    Task<RESTfulResult<bool>> IsTableNeedToLogAsync(string tableName);

    /// <summary>
    /// 根据数据库表名称获取配置信息
    /// </summary>
    /// <param name="tableName">数据库表名</param>
    /// <returns></returns>
    Task<RESTfulResult<OperationLogSettingInfo>> FindByTableNameAsync(string tableName);
}