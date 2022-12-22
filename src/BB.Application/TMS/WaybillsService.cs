using BB.Core.DbContext;
using BB.Core.Services.Base;
using BB.Entity.TMS;
using BB.Tools.Entity;
using FluentValidation;

namespace BB.Application.TMS;

/// <summary>
/// 运单货物明细 业务逻辑类
/// </summary>
[ApiDescriptionSettings("初始区域")]
public class WaybillsService : BaseService<Waybills>, IDynamicApiController, ITransient
{
    public WaybillsService(BaseRepository<Waybills> repository, IValidator<Waybills> validator) : base(repository, validator)
    {
    }

    /// <summary>
    /// 实体动态默认值
    /// </summary>
    /// <returns></returns>
    public override Task<Waybills> SetDynamicDefaults(Waybills entity)
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
            { nameof(Waybills.ISID), 3 },
            { nameof(Waybills.WaybillNo), 3 },
            { nameof(Waybills.CargoType), 3 },
            { nameof(Waybills.CubageLength), 3 },
            { nameof(Waybills.CubageWidth), 3 },
            { nameof(Waybills.CubageHight), 3 },
            { nameof(Waybills.CreationDate), 3 },
            { nameof(Waybills.CreatedBy), 3 },
            { nameof(Waybills.LastUpdateDate), 3 },
            { nameof(Waybills.LastUpdatedBy), 3 },
            { nameof(Waybills.CancelYN), 3 },
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
    public override async Task<bool> InsertAsync(Waybills obj)
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
    public override async Task<bool> UpdateAsync(Waybills obj)
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
        return Cache.GetOrAdd($"{nameof(Waybills)}ConditionTypes",
            _ => new List<FieldConditionType>
            {
                new(Waybills.FieldCargoName, SqlOperator.Like),
                new(Waybills.FieldPackageType, SqlOperator.Equal),
                new(Waybills.FieldCargoUnit, SqlOperator.Like),
                new(Waybills.FieldQty, SqlOperator.Between),
                new(Waybills.FieldWeight, SqlOperator.Between),
                new(Waybills.FieldCubage, SqlOperator.Between),
                new(Waybills.FieldPrice, SqlOperator.Between),
                new(Waybills.FieldPriceType, SqlOperator.Equal),
                new(Waybills.FieldAmountInsured, SqlOperator.Between),
                new(Waybills.FieldCarriageCharge, SqlOperator.Between),
                new(Waybills.FieldDeliveryCharge, SqlOperator.Between),
                new(Waybills.FieldPremiumCharge, SqlOperator.Between),
                new(Waybills.FieldUnloadingCharge, SqlOperator.Between),
                new(Waybills.FieldUpstairsCharge, SqlOperator.Between),
                new(Waybills.FieldWaitNoticeCharge, SqlOperator.Between),
                new(Waybills.FieldAckRecCharge, SqlOperator.Between),
                new(Waybills.FieldInformationCharge, SqlOperator.Between),
                new(Waybills.FieldPackageCharge, SqlOperator.Between),
                new(Waybills.FieldPickupCharge, SqlOperator.Between),
                new(Waybills.FieldTransferCharge, SqlOperator.Between),
                new(Waybills.FieldOtherCharge, SqlOperator.Between),
                new(Waybills.FieldAgencyReceiveCharge, SqlOperator.Between),
                new(Waybills.FieldAgencyReceiveChargePoundage, SqlOperator.Between),
                new(Waybills.FieldBrokerage, SqlOperator.Between),
                new(Waybills.FieldPremiumRate, SqlOperator.Between),
                new(Waybills.FieldCreationDate, SqlOperator.Between),
                new(Waybills.FieldCreatedBy, SqlOperator.Equal),
                new(Waybills.FieldLastUpdateDate, SqlOperator.Between),
                new(Waybills.FieldLastUpdatedBy, SqlOperator.Equal),
                new(Waybills.FieldCancelYN, SqlOperator.Equal),
            });
    }
}