using BB.Core.DbContext;
using BB.Core.Services.Base;
using BB.Entity.TMS;
using BB.Tools.Entity;
using BB.Tools.Extension;
using FluentValidation;

namespace BB.Application.TMS;

/// <summary>
/// 运单管理 业务逻辑类
/// </summary>
[ApiDescriptionSettings("初始区域")]
public class WaybillService : BaseMultiService<Waybill, Waybills>, IDynamicApiController, ITransient
{
    public WaybillService(BaseRepository<Waybill> repository, BaseRepository<Waybills> childRepository, IValidator<Waybill> validator, IValidator<Waybills> childValidator)
        : base(repository, childRepository, validator, childValidator)
    {
    }

    /// <summary>
    /// 实体动态默认值
    /// </summary>
    /// <returns></returns>
    public override Task<Waybill> SetDynamicDefaults(Waybill entity)
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
            { nameof(Waybill.WaybillNo), 1 },
            { nameof(Waybill.WaybillNoDef), 3 },
            { nameof(Waybill.TransferInType), 3 },
            { nameof(Waybill.TransferOutType), 3 },
            { nameof(Waybill.TransferOutNo), 3 },
            { nameof(Waybill.TranCustomer), 3 },
            { nameof(Waybill.OrderType), 3 },
            { nameof(Waybill.FromNode), 1 },
            { nameof(Waybill.FromNodes), 4 },
            { nameof(Waybill.FromProvinceId), 3 },
            { nameof(Waybill.FromCityId), 3 },
            { nameof(Waybill.ToProvinceId), 3 },
            { nameof(Waybill.ToCityId), 3 },
            { nameof(Waybill.NoticeUser), 3 },
            { nameof(Waybill.NoticeDate), 3 },
            { nameof(Waybill.NoticeRemark), 3 },
            { nameof(Waybill.AckRecQty), 3 },
            { nameof(Waybill.AckRecType), 3 },
            { nameof(Waybill.CargoType), 3 },
            { nameof(Waybill.CargoName), 3 },
            { nameof(Waybill.PackageType), 3 },
            { nameof(Waybill.CargoUnit), 3 },
            { nameof(Waybill.StatusNode), 1 },
            { nameof(Waybill.StatusId), 1 },
            { nameof(Waybill.CloseYN), 3 },
            { nameof(Waybill.CloseUser), 3 },
            { nameof(Waybill.CloseDate), 3 },
            { nameof(Waybill.CloseTime), 3 },
            { nameof(Waybill.CreationDate), 1 },
            { nameof(Waybill.CreatedBy), 1 },
            { nameof(Waybill.LastUpdateDate), 1 },
            { nameof(Waybill.LastUpdatedBy), 1 },
            { nameof(Waybill.FlagApp), 1 },
            { nameof(Waybill.AppUser), 1 },
            { nameof(Waybill.AppDate), 1 },
            { nameof(Waybill.AckRecCancelYN), 3 },
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
    public override async Task<bool> InsertAsync(Waybill obj)
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
    public override async Task<bool> UpdateAsync(Waybill obj)
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
        return Cache.GetOrAdd($"{nameof(Waybill)}ConditionTypes",
            _ => new List<FieldConditionType>
            {
                new(Waybill.FieldWaybillNo, SqlOperator.Like),
                new(Waybill.FieldWaybillDate, SqlOperator.Between),
                new(Waybill.FieldTransferInNo, SqlOperator.Like),
                new(Waybill.FieldOrderNo, SqlOperator.Like),
                new(Waybill.FieldFromNode, SqlOperator.Equal),
                new(Waybill.FieldFromNodes, SqlOperator.Equal),
                new(Waybill.FieldFromAreaId, SqlOperator.Equal),
                new(Waybill.FieldToNode, SqlOperator.Like),
                new(Waybill.FieldToNodes, SqlOperator.Like),
                new(Waybill.FieldToAreaId, SqlOperator.Equal),
                new(Waybill.FieldShipperCompanyName, SqlOperator.Equal),
                new(Waybill.FieldShipperAddress, SqlOperator.Like),
                new(Waybill.FieldShipperTel, SqlOperator.Like),
                new(Waybill.FieldShipper, SqlOperator.Like),
                new(Waybill.FieldConsigneeCompanyName, SqlOperator.Equal),
                new(Waybill.FieldConsigneeAddress, SqlOperator.Like),
                new(Waybill.FieldConsigneeTel, SqlOperator.Like),
                new(Waybill.FieldConsignee, SqlOperator.Like),
                new(Waybill.FieldTransportType, SqlOperator.Equal),
                new(Waybill.FieldPickUpType, SqlOperator.Equal),
                new(Waybill.FieldDeliveryType, SqlOperator.Equal),
                new(Waybill.FieldPaymentType, SqlOperator.Equal),
                new(Waybill.FieldWaitNoticeYN, SqlOperator.Equal),
                new(Waybill.FieldNoticeUser, SqlOperator.Equal),
                new(Waybill.FieldNoticeDate, SqlOperator.Between),
                new(Waybill.FieldNoticeRemark, SqlOperator.Like),
                new(Waybill.FieldAckRecNo, SqlOperator.Like),
                new(Waybill.FieldAbnormityType, SqlOperator.Equal),
                new(Waybill.FieldQty, SqlOperator.Between),
                new(Waybill.FieldWeight, SqlOperator.Between),
                new(Waybill.FieldCubage, SqlOperator.Between),
                new(Waybill.FieldChargeableWeight, SqlOperator.Between),
                new(Waybill.FieldAgencyReceiveCharge, SqlOperator.Between),
                new(Waybill.FieldCarriagePrepaid, SqlOperator.Between),
                new(Waybill.FieldCarriageForward, SqlOperator.Between),
                new(Waybill.FieldCarriageMonthly, SqlOperator.Between),
                new(Waybill.FieldCarriageReceipt, SqlOperator.Between),
                new(Waybill.FieldCarriageOwed, SqlOperator.Between),
                new(Waybill.FieldCarriageOther, SqlOperator.Between),
                new(Waybill.FieldBrokerage, SqlOperator.Between),
                new(Waybill.FieldAmountInsured, SqlOperator.Between),
                new(Waybill.FieldStatusNode, SqlOperator.Equal),
                new(Waybill.FieldStatusId, SqlOperator.Equal),
                new(Waybill.FieldUnloadYN, SqlOperator.Equal),
                new(Waybill.FieldUpstairYN, SqlOperator.Equal),
                new(Waybill.FieldUpstairNum, SqlOperator.Between),
                new(Waybill.FieldSalesMan, SqlOperator.Equal),
                new(Waybill.FieldRemark, SqlOperator.Like),
                new(Waybill.FieldCreationDate, SqlOperator.Between),
                new(Waybill.FieldCreatedBy, SqlOperator.Equal),
                new(Waybill.FieldLastUpdateDate, SqlOperator.Between),
                new(Waybill.FieldLastUpdatedBy, SqlOperator.Equal),
                new(Waybill.FieldFlagApp, SqlOperator.Equal),
                new(Waybill.FieldAppUser, SqlOperator.Equal),
                new(Waybill.FieldAppDate, SqlOperator.Between),
                new(Waybill.FieldAutoNoYN, SqlOperator.Equal),
                new(Waybill.FieldAutoReceipt, SqlOperator.Equal),
            });
    }

    /// <summary>
    /// 构造查询条件
    /// </summary>
    /// <param name="searchInfos">查询参数</param>
    /// <returns></returns>
    public override async Task<List<IConditionalModel>> GetConditionExc(Dictionary<string, string> searchInfos)
    {
        var condition = await base.GetConditionExc(searchInfos);
        bool fromAreaIdLength = !searchInfos[Waybill.FieldFromAreaId].IsNullOrEmpty() && searchInfos[Waybill.FieldFromAreaId].Length < 6;
        bool toAreaIdLength = !searchInfos[Waybill.FieldToAreaId].IsNullOrEmpty() && searchInfos[Waybill.FieldToAreaId].Length < 6;
        if (fromAreaIdLength || toAreaIdLength) // condition 用字典是不是更合适
        {
            condition.ForEach(x =>
            {
                if (x is ConditionalModel { FieldName: Waybill.FieldFromAreaId or Waybill.FieldToAreaId } c)
                {
                    c.ConditionalType = ConditionalType.LikeLeft;
                }
            });
        }

        return condition;
    }
}