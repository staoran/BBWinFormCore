using BB.Core.DbContext;
using BB.Core.Services.Base;
using BB.Entity.TMS;
using BB.Tools.Entity;
using FluentValidation;

namespace BB.Application.TMS;

/// <summary>
/// 回单操作记录 业务逻辑类
/// </summary>
[ApiDescriptionSettings("初始区域")]
public class WaybillAckRecsService : BaseService<WaybillAckRecs>, IDynamicApiController, ITransient
{
    public WaybillAckRecsService(BaseRepository<WaybillAckRecs> repository, IValidator<WaybillAckRecs> validator)
        : base(repository, validator)
    {
    }

    /// <summary>
    /// 实体动态默认值
    /// </summary>
    /// <returns></returns>
    public override Task<WaybillAckRecs> SetDynamicDefaults(WaybillAckRecs entity)
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
            { nameof(WaybillAckRecs.ISID), 3 },
            { nameof(WaybillAckRecs.TranNode), 1 },
            { nameof(WaybillAckRecs.CancelYN), 1 },
            { nameof(WaybillAckRecs.CancelDate), 1 },
            { nameof(WaybillAckRecs.CancelBy), 1 },
            { nameof(WaybillAckRecs.CreationDate), 1 },
            { nameof(WaybillAckRecs.CreatedBy), 1 },
            { nameof(WaybillAckRecs.LastUpdateDate), 1 },
            { nameof(WaybillAckRecs.LastUpdatedBy), 1 },
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
    public override async Task<bool> InsertAsync(WaybillAckRecs obj)
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
    public override async Task<bool> UpdateAsync(WaybillAckRecs obj)
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
        return Cache.GetOrAdd($"{nameof(WaybillAckRecs)}ConditionTypes",
            _ => new List<FieldConditionType>
            {
                new(WaybillAckRecs.FieldAckRecNo, SqlOperator.Like),
                new(WaybillAckRecs.FieldTranNode, SqlOperator.Equal),
                new(WaybillAckRecs.FieldTranNodePN, SqlOperator.Like),
                new(WaybillAckRecs.FieldStatusID, SqlOperator.Equal),
                new(WaybillAckRecs.FieldRelatedUser, SqlOperator.Equal),
                new(WaybillAckRecs.FieldCarMarkNo, SqlOperator.Like),
                new(WaybillAckRecs.FieldRemark, SqlOperator.Like),
                new(WaybillAckRecs.FieldCancelYN, SqlOperator.Equal),
                new(WaybillAckRecs.FieldCancelDate, SqlOperator.Between),
                new(WaybillAckRecs.FieldCancelBy, SqlOperator.Like),
                new(WaybillAckRecs.FieldCreationDate, SqlOperator.Between),
                new(WaybillAckRecs.FieldCreatedBy, SqlOperator.Equal),
                new(WaybillAckRecs.FieldLastUpdateDate, SqlOperator.Between),
                new(WaybillAckRecs.FieldLastUpdatedBy, SqlOperator.Equal),
            });
    }
}