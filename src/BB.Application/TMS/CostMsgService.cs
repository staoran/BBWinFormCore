using BB.Core.DbContext;
using BB.Core.Services.Base;
using BB.Entity.TMS;
using BB.Tools.Entity;
using FluentValidation;

namespace BB.Application.TMS;

/// <summary>
/// 费用调整 业务逻辑类
/// </summary>
[ApiDescriptionSettings("费用与账单")]
public class CostMsgService : BaseMultiService<CostMsg, CostMsgs>, IDynamicApiController, ITransient
{
    public CostMsgService(BaseRepository<CostMsg> repository, BaseRepository<CostMsgs> childRepository, IValidator<CostMsg> validator, IValidator<CostMsgs> childValidator)
        : base(repository, childRepository, validator, childValidator)
    {
    }

    /// <summary>
    /// 实体动态默认值
    /// </summary>
    /// <returns></returns>
    public override Task<CostMsg> SetDynamicDefaults(CostMsg entity)
    {
        return Task.FromResult(entity);
    }

    /// <summary>
    /// 获取字段显示权限，0可读写 1只读 2隐藏值 3不显示 4 可新增不可编辑
    /// </summary>
    /// <returns></returns>
    public override async Task<Dictionary<string, int>> GetPermitDictAsync()
    {
        var permitDict = new Dictionary<string, int>
        {
            { nameof(CostMsg.ISID), 3 },
            { nameof(CostMsg.CostMsgNo), 1 },
            { nameof(CostMsg.SourceISID), 3 },
            { nameof(CostMsg.WaybillNo), 4 },
            { nameof(CostMsg.SendMsgNode), 1 },
            { nameof(CostMsg.RecvMsgType), 1 },
            { nameof(CostMsg.SourceValue), 1 },
            { nameof(CostMsg.StatusID), 1 },
            { nameof(CostMsg.CreationDate), 1 },
            { nameof(CostMsg.CreatedBy), 1 },
            { nameof(CostMsg.LastUpdateDate), 1 },
            { nameof(CostMsg.LastUpdatedBy), 1 },
            { nameof(CostMsg.FlagApp), 1 },
            { nameof(CostMsg.AppUser), 1 },
            { nameof(CostMsg.AppDate), 1 },
            { nameof(CostMsg.FinancialCenter), 1 },
        };
        // 后端权限控制
        // var permitDict = BllFactory<FieldPermit>.Instance.GetColumnsPermit(typeof(Test1Car).FullName, LoginUserInfo.ID.ToInt32());
        return await Task.FromResult(permitDict);
    }

    /// <summary>
    /// 新增一条数据
    /// </summary>
    /// <param name="obj">指定的对象</param>
    /// <returns>执行操作是否成功。</returns>
    public override async Task<bool> InsertAsync(CostMsg obj)
    {
        #region 新增前

        #endregion

        bool succeed = await base.InsertAsync(obj);

        #region 新增后

        if (!succeed) return false;

        //可添加其他关联操作

        return true;

        #endregion
    }

    /// <summary>
    /// 更新一条数据
    /// </summary>
    /// <param name="obj">指定的对象</param>
    /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
    public override async Task<bool> UpdateAsync(CostMsg obj)
    {
        #region 修改前

        #endregion

        bool succeed = await base.UpdateAsync(obj);

        #region 修改后

        if (!succeed) return false;

        //可添加其他关联操作

        return true;

        #endregion
    }

    /// <summary>
    /// 删除一条数据
    /// </summary>
    /// <param name="key">指定对象的ID</param>
    /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
    public override async Task<bool> DeleteAsync(object key)
    {
        #region 删除前

        #endregion

        bool succeed = await base.DeleteAsync(key);

        #region 删除后

        if (!succeed) return false;

        //可添加其他关联操作

        return true;

        #endregion
    }

    /// <summary>
    /// 获取查询参数配置
    /// </summary>
    /// <returns></returns>
    public override List<FieldConditionType> GetConditionTypes()
    {
        return Cache.GetOrAdd($"{nameof(CostMsg)}ConditionTypes",
            _ => new List<FieldConditionType>
            {
                new(CostMsg.FieldCostMsgNo, SqlOperator.Like),
                new(CostMsg.FieldSourceType, SqlOperator.Equal),
                new(CostMsg.FieldWaybillNo, SqlOperator.Like),
                new(CostMsg.FieldSendMsgNode, SqlOperator.Equal),
                new(CostMsg.FieldSendMsgContent, SqlOperator.Like),
                new(CostMsg.FieldAttaPath, SqlOperator.Like),
                new(CostMsg.FieldRecvMsgType, SqlOperator.Equal),
                new(CostMsg.FieldRecvMsgAccount, SqlOperator.Equal),
                new(CostMsg.FieldValueType, SqlOperator.Equal),
                new(CostMsg.FieldSourceValue, SqlOperator.Between),
                new(CostMsg.FieldActiveValue, SqlOperator.Between),
                new(CostMsg.FieldStatusID, SqlOperator.Equal),
                new(CostMsg.FieldCreationDate, SqlOperator.Between),
                new(CostMsg.FieldCreatedBy, SqlOperator.Equal),
                new(CostMsg.FieldLastUpdateDate, SqlOperator.Between),
                new(CostMsg.FieldLastUpdatedBy, SqlOperator.Equal),
                new(CostMsg.FieldFlagApp, SqlOperator.Equal),
                new(CostMsg.FieldAppUser, SqlOperator.Equal),
                new(CostMsg.FieldAppDate, SqlOperator.Between),
                new(CostMsg.FieldFinancialCenter, SqlOperator.Equal)
            });
    }
}