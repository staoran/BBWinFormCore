using BB.Core.DbContext;
using BB.Core.Services.Base;
using BB.Entity.TMS;
using BB.Tools.Entity;
using FluentValidation;

namespace BB.Application.TMS;

/// <summary>
/// 普通报价明细 业务逻辑类
/// </summary>
[ApiDescriptionSettings("线路与报价")]
public class QuotationsService : BaseService<Quotations>, IDynamicApiController, ITransient
{
    public QuotationsService(BaseRepository<Quotations> repository, IValidator<Quotations> validator) : base(repository, validator)
    {
    }

    /// <summary>
    /// 实体动态默认值
    /// </summary>
    /// <returns></returns>
    public override Task<Quotations> SetDynamicDefaults(Quotations entity)
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
            { nameof(Quotations.ISID), 3 },
            { nameof(Quotations.QuotationNo), 3 },
            { nameof(Quotations.MathConditional), 3 },
            { nameof(Quotations.MathContent), 3 },
            { nameof(Quotations.CreationDate), 3 },
            { nameof(Quotations.CreatedBy), 3 },
            { nameof(Quotations.LastUpdateDate), 3 },
            { nameof(Quotations.LastUpdatedBy), 3 },
            { nameof(Quotations.FromGroupsID), 3 },
            { nameof(Quotations.ToGroupsID), 3 },
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
    public override async Task<bool> InsertAsync(Quotations obj)
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
    public override async Task<bool> UpdateAsync(Quotations obj)
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
        return Cache.GetOrAdd($"{nameof(Quotations)}ConditionTypes",
            _ => new List<FieldConditionType>
            {
                new(Quotations.FieldQuotationType, SqlOperator.Equal),
                new(Quotations.FieldFromGroups, SqlOperator.Like),
                new(Quotations.FieldToGroups, SqlOperator.Like),
                new(Quotations.FieldMinCost, SqlOperator.Between),
                new(Quotations.FieldMaxCost, SqlOperator.Between),
                new(Quotations.FieldFirstCost, SqlOperator.Between),
                new(Quotations.FieldFirstValue, SqlOperator.Between),
                new(Quotations.FieldMinValue, SqlOperator.Between),
                new(Quotations.FieldMaxValue, SqlOperator.Between),
                new(Quotations.FieldUnitPrice, SqlOperator.Between),
                new(Quotations.FieldUnitPricePer, SqlOperator.Between),
                new(Quotations.FieldMathConditional, SqlOperator.Like),
                new(Quotations.FieldMathContent, SqlOperator.Like),
                new(Quotations.FieldRemark, SqlOperator.Like),
                new(Quotations.FieldCreationDate, SqlOperator.Between),
                new(Quotations.FieldCreatedBy, SqlOperator.Equal),
                new(Quotations.FieldLastUpdateDate, SqlOperator.Between),
                new(Quotations.FieldLastUpdatedBy, SqlOperator.Equal)
            });
    }
}