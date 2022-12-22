using BB.Core.DbContext;
using BB.Core.Services.Base;
using BB.Entity.TMS;
using BB.Tools.Entity;
using FluentValidation;

namespace BB.Application.TMS;

/// <summary>
/// 预付金操作类型 业务逻辑类
/// </summary>
[ApiDescriptionSettings("费用与账单")]
public class BasicCostBillTypeService : BaseService<BasicCostBillType>, IDynamicApiController, ITransient
{
    public BasicCostBillTypeService(BaseRepository<BasicCostBillType> repository, IValidator<BasicCostBillType> validator) : base(repository, validator)
    {
    }

    /// <summary>
    /// 实体动态默认值
    /// </summary>
    /// <returns></returns>
    public override Task<BasicCostBillType> SetDynamicDefaults(BasicCostBillType entity)
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
            { nameof(BasicCostBillType.ISID), 3 },
            // 主键不为空则只读
            { nameof(BasicCostBillType.CostType), 4 },
            { nameof(BasicCostBillType.CreatedBy), 1 },
            { nameof(BasicCostBillType.CreationDate), 1 },
            { nameof(BasicCostBillType.FlagApp), 1 },
            { nameof(BasicCostBillType.AppUser), 1 },
            { nameof(BasicCostBillType.AppDate), 1 },
            { nameof(BasicCostBillType.LastUpdatedBy), 1 },
            { nameof(BasicCostBillType.LastUpdateDate), 1 },
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
    public override async Task<bool> InsertAsync(BasicCostBillType obj)
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
    public override async Task<bool> UpdateAsync(BasicCostBillType obj)
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
        return Cache.GetOrAdd($"{nameof(BasicCostBillType)}ConditionTypes",
            _ => new List<FieldConditionType>
            {
                new(BasicCostBillType.FieldCostType, SqlOperator.Like),
                new(BasicCostBillType.FieldCostDesc, SqlOperator.Like),
                new(BasicCostBillType.FieldCtrl, SqlOperator.Like),
                new(BasicCostBillType.FieldRemark, SqlOperator.Like),
                new(BasicCostBillType.FieldUseType, SqlOperator.Equal),
                new(BasicCostBillType.FieldCreatedBy, SqlOperator.Equal),
                new(BasicCostBillType.FieldCreationDate, SqlOperator.Between),
                new(BasicCostBillType.FieldFlagApp, SqlOperator.Equal),
                new(BasicCostBillType.FieldAppUser, SqlOperator.Equal),
                new(BasicCostBillType.FieldAppDate, SqlOperator.Between),
                new(BasicCostBillType.FieldLastUpdatedBy, SqlOperator.Equal),
                new(BasicCostBillType.FieldLastUpdateDate, SqlOperator.Between)
            });
    }
}