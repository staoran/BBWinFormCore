using BB.Core.DbContext;
using BB.Core.Services.Base;
using BB.Entity.TMS;
using BB.Tools.Entity;
using FluentValidation;

namespace BB.Application.TMS;

/// <summary>
/// 费用调整确认 业务逻辑类
/// </summary>
[ApiDescriptionSettings("费用与账单")]
public class CostMsgsService : BaseService<CostMsgs>, IDynamicApiController, ITransient
{
    public CostMsgsService(BaseRepository<CostMsgs> repository, IValidator<CostMsgs> validator) : base(repository, validator)
    {
    }

    /// <summary>
    /// 实体动态默认值
    /// </summary>
    /// <returns></returns>
    public override Task<CostMsgs> SetDynamicDefaults(CostMsgs entity)
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
            { nameof(CostMsgs.ISID), 3 },
            { nameof(CostMsgs.CostMsgNo), 3 },
            { nameof(CostMsgs.StatusID), 4 },
            { nameof(CostMsgs.RecvMsgNode), 1 },
            { nameof(CostMsgs.CreationDate), 3 },
            { nameof(CostMsgs.CreatedBy), 3 },
            { nameof(CostMsgs.LastUpdateDate), 3 },
            { nameof(CostMsgs.LastUpdatedBy), 3 },
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
    public override async Task<bool> InsertAsync(CostMsgs obj)
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
    public override async Task<bool> UpdateAsync(CostMsgs obj)
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
        if (!succeed) return false;

        #region 删除后

        //可添加其他关联操作

        #endregion

        return true;
    }

    /// <summary>
    /// 获取查询参数配置
    /// </summary>
    /// <returns></returns>
    public override List<FieldConditionType> GetConditionTypes()
    {
        return Cache.GetOrAdd($"{nameof(CostMsgs)}ConditionTypes",
            _ => new List<FieldConditionType>
            {
                new(CostMsgs.FieldCostMsgNo, SqlOperator.Like),
                new(CostMsgs.FieldStatusID, SqlOperator.Equal),
                new(CostMsgs.FieldRecvMsgNode, SqlOperator.Equal),
                new(CostMsgs.FieldRecvMsgContent, SqlOperator.Like),
                new(CostMsgs.FieldAttaPath, SqlOperator.Like),
                new(CostMsgs.FieldCreationDate, SqlOperator.Between),
                new(CostMsgs.FieldCreatedBy, SqlOperator.Equal),
                new(CostMsgs.FieldLastUpdateDate, SqlOperator.Between),
                new(CostMsgs.FieldLastUpdatedBy, SqlOperator.Equal)
            });
    }
}