using BB.Core.DbContext;
using BB.Core.Services.Base;
using BB.Entity.TMS;
using BB.Tools.Encrypt;
using BB.Tools.Entity;
using BB.Tools.Extension;
using BB.Tools.Utils;
using FluentValidation;

namespace BB.Application.TMS;

/// <summary>
/// 普通报价 业务逻辑类
/// </summary>
[ApiDescriptionSettings("线路与报价")]
public class QuotationService : BaseMultiService<Quotation, Quotations>, IDynamicApiController, ITransient
{
    public QuotationService(BaseRepository<Quotation> repository, BaseRepository<Quotations> childRepository, IValidator<Quotation> validator, IValidator<Quotations> childValidator)
        : base(repository, childRepository, validator, childValidator)
    {
    }

    /// <summary>
    /// 实体动态默认值
    /// </summary>
    /// <returns></returns>
    public override Task<Quotation> SetDynamicDefaults(Quotation entity)
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
            { nameof(Quotation.ISID), 3 },
            { nameof(Quotation.QuotationNo), 1 },
            { nameof(Quotation.CreationDate), 1 },
            { nameof(Quotation.CreatedBy), 1 },
            { nameof(Quotation.LastUpdateDate), 1 },
            { nameof(Quotation.LastUpdatedBy), 1 },
            { nameof(Quotation.FlagApp), 1 },
            { nameof(Quotation.AppUser), 1 },
            { nameof(Quotation.AppDate), 1 },
            { nameof(Quotation.TranNodeNO), 1 },
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
    public override async Task<bool> InsertAsync(Quotation obj)
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
    public override async Task<bool> UpdateAsync(Quotation obj)
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
    /// 获取查询参数配置
    /// </summary>
    /// <returns></returns>
    public override List<FieldConditionType> GetConditionTypes()
    {
        return Cache.Instance.GetOrCreate($"{nameof(Quotation)}ConditionTypes",
            () => new List<FieldConditionType>
            {
                new(Quotation.FieldQuotationNo, SqlOperator.Like),
                new(Quotation.FieldQuotationDesc, SqlOperator.Like),
                new(Quotation.FieldCostType, SqlOperator.Equal),
                new(Quotation.FieldCargoTypePerYN, SqlOperator.Equal),
                new(Quotation.FieldRemark, SqlOperator.Like),
                new(Quotation.FieldCreationDate, SqlOperator.Between),
                new(Quotation.FieldCreatedBy, SqlOperator.Equal),
                new(Quotation.FieldLastUpdateDate, SqlOperator.Between),
                new(Quotation.FieldLastUpdatedBy, SqlOperator.Equal),
                new(Quotation.FieldFlagApp, SqlOperator.Equal),
                new(Quotation.FieldAppUser, SqlOperator.Equal),
                new(Quotation.FieldAppDate, SqlOperator.Between)
            });
    }
}