using BB.Core.DbContext;
using BB.Core.Services.Base;
using BB.Entity.TMS;
using BB.Tools.Entity;
using FluentValidation;

namespace BB.Application.TMS;

/// <summary>
/// 线路表 业务逻辑类
/// </summary>
[ApiDescriptionSettings("线路与报价")]
public class SegmentService : BaseMultiService<Segment, Segments>, IDynamicApiController, ITransient
{
    public SegmentService(BaseRepository<Segment> repository, BaseRepository<Segments> childRepository, IValidator<Segment> validator, IValidator<Segments> childValidator)
        : base(repository, childRepository, validator, childValidator)
    {
    }

    /// <summary>
    /// 实体动态默认值
    /// </summary>
    /// <returns></returns>
    public override Task<Segment> SetDynamicDefaults(Segment entity)
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
            { nameof(Segment.ISID), 3 },
            // 主键不为空则只读
            { nameof(Segment.SegmentNo), 4 },
            { nameof(Segment.CreationDate), 1 },
            { nameof(Segment.CreatedBy), 1 },
            { nameof(Segment.LastUpdateDate), 1 },
            { nameof(Segment.LastUpdatedBy), 1 },
            { nameof(Segment.FlagApp), 1 },
            { nameof(Segment.AppUser), 1 },
            { nameof(Segment.AppDate), 1 },
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
    public override async Task<bool> InsertAsync(Segment obj)
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
    public override async Task<bool> UpdateAsync(Segment obj)
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
        return Cache.GetOrAdd($"{nameof(Segment)}ConditionTypes",
            _ => new List<FieldConditionType>
            {
                new(Segment.FieldSegmentNo, SqlOperator.Like),
                new(Segment.FieldSegmentType, SqlOperator.Equal),
                new(Segment.FieldSegmentName, SqlOperator.Like),
                new(Segment.FieldSegmentBeginNode, SqlOperator.Equal),
                new(Segment.FieldSegmentEndNode, SqlOperator.Equal),
                new(Segment.FieldPlanBeginTime, SqlOperator.Between),
                new(Segment.FieldExpectedHour, SqlOperator.Between),
                new(Segment.FieldExpectedDistance, SqlOperator.Between),
                new(Segment.FieldExpectedOilWear, SqlOperator.Between),
                new(Segment.FieldExpectedCharge, SqlOperator.Between),
                new(Segment.FieldExpectedPontage, SqlOperator.Between),
                new(Segment.FieldRemark, SqlOperator.Like),
                new(Segment.FieldCreationDate, SqlOperator.Between),
                new(Segment.FieldCreatedBy, SqlOperator.Equal),
                new(Segment.FieldLastUpdateDate, SqlOperator.Between),
                new(Segment.FieldLastUpdatedBy, SqlOperator.Equal),
                new(Segment.FieldFlagApp, SqlOperator.Equal),
                new(Segment.FieldAppUser, SqlOperator.Equal),
                new(Segment.FieldAppDate, SqlOperator.Between)
            });
    }
}