using BB.Core.DbContext;
using BB.Core.Services.Base;
using BB.Entity.TMS;
using BB.Tools.Entity;
using BB.Tools.Extension;
using BB.Tools.Utils;
using FluentValidation;

namespace BB.Application.TMS;

/// <summary>
/// 公式报价 业务逻辑类
/// </summary>
[ApiDescriptionSettings("线路与报价")]
public class BasicQuotationService : BaseMultiService<BasicQuotation, BasicQuotations>, IDynamicApiController, ITransient
{
    public BasicQuotationService(BaseRepository<BasicQuotation> repository, BaseRepository<BasicQuotations> childRepository, IValidator<BasicQuotation> validator, IValidator<BasicQuotations> childValidator)
        : base(repository, childRepository, validator, childValidator)
    {
    }

    /// <summary>
    /// 实体动态默认值
    /// </summary>
    /// <returns></returns>
    public override Task<BasicQuotation> SetDynamicDefaults(BasicQuotation entity)
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
            { nameof(BasicQuotation.ISID), 3 },
            // 主键不为空则只读
            { nameof(BasicQuotation.QuotationNo), 4 },
            { nameof(BasicQuotation.TranNodeNO), 1 },
            { nameof(BasicQuotation.CreationDate), 1 },
            { nameof(BasicQuotation.CreatedBy), 1 },
            { nameof(BasicQuotation.LastUpdateDate), 1 },
            { nameof(BasicQuotation.LastUpdatedBy), 1 },
            { nameof(BasicQuotation.FlagApp), 1 },
            { nameof(BasicQuotation.AppUser), 1 },
            { nameof(BasicQuotation.AppDate), 1 },
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
    public override async Task<bool> InsertAsync(BasicQuotation obj)
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
    public override async Task<bool> UpdateAsync(BasicQuotation obj)
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
        return Cache.Instance.GetOrCreate($"{nameof(BasicQuotation)}ConditionTypes",
            () => new List<FieldConditionType>
            {
                new(BasicQuotation.FieldQuotationNo, SqlOperator.Like),
                new(BasicQuotation.FieldQuotationDesc, SqlOperator.Like),
                new(BasicQuotation.FieldTranNodeNO, SqlOperator.Equal),
                new(BasicQuotation.FieldCostType, SqlOperator.Equal),
                new(BasicQuotation.FieldCargoType, SqlOperator.Equal),
                new(BasicQuotation.FieldPickUpType, SqlOperator.Equal),
                new(BasicQuotation.FieldDeliveryType, SqlOperator.Equal),
                new(BasicQuotation.FieldTransportType, SqlOperator.Equal),
                new(BasicQuotation.FieldFroms, SqlOperator.Like),
                new(BasicQuotation.FieldTos, SqlOperator.Like),
                new(BasicQuotation.FieldBeginTime, SqlOperator.Between),
                new(BasicQuotation.FieldEndTime, SqlOperator.Between),
                new(BasicQuotation.FieldRemark, SqlOperator.Like),
                new(BasicQuotation.FieldCreationDate, SqlOperator.Between),
                new(BasicQuotation.FieldCreatedBy, SqlOperator.Equal),
                new(BasicQuotation.FieldLastUpdateDate, SqlOperator.Between),
                new(BasicQuotation.FieldLastUpdatedBy, SqlOperator.Equal),
                new(BasicQuotation.FieldFlagApp, SqlOperator.Equal),
                new(BasicQuotation.FieldAppUser, SqlOperator.Equal),
                new(BasicQuotation.FieldAppDate, SqlOperator.Between),
                new(BasicQuotation.FieldRakeMarkYN, SqlOperator.Equal)
            });
    }
}