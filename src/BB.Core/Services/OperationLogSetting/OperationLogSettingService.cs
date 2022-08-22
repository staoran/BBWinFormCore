using System.Threading.Tasks;
using BB.Core.DbContext;
using BB.Core.Services.Base;
using BB.Entity.Security;
using FluentValidation;

namespace BB.Core.Services.OperationLogSetting;

public class OperationLogSettingService : BaseService<OperationLogSettingInfo>, IDynamicApiController, ITransient
{
    public OperationLogSettingService(BaseRepository<OperationLogSettingInfo> repository, IValidator<OperationLogSettingInfo> validator) : base(repository, validator)
    {
    }

    public override async Task<bool> UpdateAsync(OperationLogSettingInfo obj)
    {
        //检查不同ID是否还有其他相同关键字的记录
        if (await IsExistRecordAsync(x => x.TableName == obj.TableName && x.ID != obj.ID))
        {
            throw Oops.Bah("指定的【数据库表】已经存在，不能重复添加，请修改");
        }

        return await base.UpdateAsync(obj);
    }

    /// <summary>
    /// 判断指定的表名称是否需要记录操作日志（是否在配置表里面，并是有效状态）
    /// </summary>
    /// <param name="tableName">表名称</param>
    /// <returns></returns>
    public async Task<bool> IsTableNeedToLogAsync(string tableName)
    {
        string condition = $"TableName = '{tableName}' and Forbid = 0 ";
        return await IsExistRecordAsync(condition);
    }

    /// <summary>
    /// 根据数据库表名称获取配置信息
    /// </summary>
    /// <param name="tableName">数据库表名</param>
    /// <returns></returns>
    public async Task<OperationLogSettingInfo> FindByTableNameAsync(string tableName)
    {
        string condition = $"TableName = '{tableName}' and Forbid = 0 ";
        return await FindSingleAsync(condition);
    }
}