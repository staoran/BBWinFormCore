using BB.Core.DbContext;
using BB.Core.Services.Base;
using BB.Entity.TMS;
using BB.Tools.Entity;
using FluentValidation;

namespace BB.Application.TMS;

/// <summary>
/// 运单线路表 业务逻辑类
/// </summary>
[ApiDescriptionSettings("初始区域")]
public class WaybillSegmentService : BaseService<WaybillSegment>, IDynamicApiController, ITransient
{
    public WaybillSegmentService(BaseRepository<WaybillSegment> repository, IValidator<WaybillSegment> validator)
        : base(repository, validator)
    {
    }

    /// <summary>
    /// 实体动态默认值
    /// </summary>
    /// <returns></returns>
    public override Task<WaybillSegment> SetDynamicDefaults(WaybillSegment entity)
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
            { nameof(WaybillSegment.ISID), 3 },
            { nameof(WaybillSegment.CreationDate), 1 },
            { nameof(WaybillSegment.CreatedBy), 1 },
            { nameof(WaybillSegment.LastUpdateDate), 1 },
            { nameof(WaybillSegment.LastUpdatedBy), 1 },
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
    public override async Task<bool> InsertAsync(WaybillSegment obj)
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
    public override async Task<bool> UpdateAsync(WaybillSegment obj)
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
        return Cache.GetOrAdd($"{nameof(WaybillSegment)}ConditionTypes",
            _ => new List<FieldConditionType>
            {
                new(WaybillSegment.FieldWaybillNo, SqlOperator.Like),
                new(WaybillSegment.FieldSegmentNo, SqlOperator.Like),
                new(WaybillSegment.FieldCarmarkNo, SqlOperator.Like),
                new(WaybillSegment.FieldSegmentType, SqlOperator.Equal),
                new(WaybillSegment.FieldSegmentName, SqlOperator.Like),
                new(WaybillSegment.FieldSegmentBeginNode, SqlOperator.Equal),
                new(WaybillSegment.FieldSegmentEndNode, SqlOperator.Equal),
                new(WaybillSegment.FieldExpectedTime, SqlOperator.Between),
                new(WaybillSegment.FieldExpectedHour, SqlOperator.Like),
                new(WaybillSegment.FieldExpectedDistance, SqlOperator.Between),
                new(WaybillSegment.FieldExpectedOilWear, SqlOperator.Between),
                new(WaybillSegment.FieldExpectedCharge, SqlOperator.Between),
                new(WaybillSegment.FieldExpectedPontage, SqlOperator.Between),
                new(WaybillSegment.FieldSegmentBeginYN, SqlOperator.Equal),
                new(WaybillSegment.FieldSegmentBeginUser, SqlOperator.Equal),
                new(WaybillSegment.FieldSegmentBeginDate, SqlOperator.Between),
                new(WaybillSegment.FieldSegmentBeginRemark, SqlOperator.Like),
                new(WaybillSegment.FieldSegmentEndYN, SqlOperator.Equal),
                new(WaybillSegment.FieldSegmentEndUser, SqlOperator.Equal),
                new(WaybillSegment.FieldSegmentEndDate, SqlOperator.Between),
                new(WaybillSegment.FieldSegmentEndRemark, SqlOperator.Like),
                new(WaybillSegment.FieldStatusId, SqlOperator.Like),
                new(WaybillSegment.FieldRemark, SqlOperator.Like),
                new(WaybillSegment.FieldCreationDate, SqlOperator.Between),
                new(WaybillSegment.FieldCreatedBy, SqlOperator.Equal),
                new(WaybillSegment.FieldLastUpdateDate, SqlOperator.Between),
                new(WaybillSegment.FieldLastUpdatedBy, SqlOperator.Equal),
            });
    }
}