using System.Runtime.Serialization;
using BB.Entity.Base;
using BB.Tools.Entity;

namespace BB.Entity.TMS;

/// <summary>
/// 运单货物明细 实体类
/// </summary>
[DataContract]
[Serializable]
[Table(DBTableName)]
public sealed class Waybills : BaseEntity
{
    /// <summary>
    /// 默认构造函数（需要初始化属性的在此处理）
    /// </summary>
    public Waybills()
    {
        Qty = 0;
        Weight = 0;
        Cubage = 0;
        Price = 0;
        AmountInsured = 0;
        CarriageCharge = 0;
        DeliveryCharge = 0;
        PremiumCharge = 0;
        UnloadingCharge = 0;
        UpstairsCharge = 0;
        WaitNoticeCharge = 0;
        AckRecCharge = 0;
        InformationCharge = 0;
        PackageCharge = 0;
        PickupCharge = 0;
        TransferCharge = 0;
        OtherCharge = 0;
        AgencyReceiveCharge = 0;
        AgencyReceiveChargePoundage = 0;
        Brokerage = 0;
        PremiumRate = 0;
        CreationDate = DateTime.Now;
        CreatedBy = "*当前用户*";
        LastUpdateDate = DateTime.Now;
        LastUpdatedBy = "*当前用户*";
        CancelYN = false;
    }

    #region Property Members

    /// <summary>
    /// 自增ID
    /// </summary>
    [DataMember]
    [Key]
    [Identity]
    [Sort(IsDesc)]
    [Column(FieldISID, DisISID)]
    public int ISID { get; set; }

    /// <summary>
    /// 运单号
    /// </summary>
    [DataMember]
    [Hide]
    [Column(FieldWaybillNo, DisWaybillNo)]
    public string WaybillNo { get; set; }

    /// <summary>
    /// 货物类型
    /// </summary>
    [DataMember]
    [Hide]
    [Column(FieldCargoType, DisCargoType)]
    public string CargoType { get; set; }

    /// <summary>
    /// 货物名称
    /// </summary>
    [DataMember]
    [Column(FieldCargoName, DisCargoName)]
    public string CargoName { get; set; }

    /// <summary>
    /// 包装类型
    /// </summary>
    [DataMember]
    [Column(FieldPackageType, DisPackageType)]
    public string PackageType { get; set; }

    /// <summary>
    /// 货物单位
    /// </summary>
    [DataMember]
    [Column(FieldCargoUnit, DisCargoUnit)]
    public string CargoUnit { get; set; }

    /// <summary>
    /// 件数
    /// </summary>
    [DataMember]
    [Column(FieldQty, DisQty)]
    public int Qty { get; set; }

    /// <summary>
    /// 重量
    /// </summary>
    [DataMember]
    [Column(FieldWeight, DisWeight)]
    public decimal Weight { get; set; }

    /// <summary>
    /// 体积
    /// </summary>
    [DataMember]
    [Column(FieldCubage, DisCubage)]
    public decimal Cubage { get; set; }

    /// <summary>
    /// 长
    /// </summary>
    [DataMember]
    [Hide]
    [Column(FieldCubageLength, DisCubageLength)]
    public decimal? CubageLength { get; set; }

    /// <summary>
    /// 宽
    /// </summary>
    [DataMember]
    [Hide]
    [Column(FieldCubageWidth, DisCubageWidth)]
    public decimal? CubageWidth { get; set; }

    /// <summary>
    /// 高
    /// </summary>
    [DataMember]
    [Hide]
    [Column(FieldCubageHight, DisCubageHight)]
    public decimal? CubageHight { get; set; }

    /// <summary>
    /// 单价
    /// </summary>
    [DataMember]
    [Column(FieldPrice, DisPrice)]
    public decimal Price { get; set; }

    /// <summary>
    /// 报价类型
    /// </summary>
    [DataMember]
    [Column(FieldPriceType, DisPriceType)]
    public string PriceType { get; set; }

    /// <summary>
    /// 保价金额
    /// </summary>
    [DataMember]
    [Column(FieldAmountInsured, DisAmountInsured)]
    public decimal AmountInsured { get; set; }

    /// <summary>
    /// 运费
    /// </summary>
    [DataMember]
    [Column(FieldCarriageCharge, DisCarriageCharge)]
    public decimal CarriageCharge { get; set; }

    /// <summary>
    /// 送货费
    /// </summary>
    [DataMember]
    [Column(FieldDeliveryCharge, DisDeliveryCharge)]
    public decimal DeliveryCharge { get; set; }

    /// <summary>
    /// 保险费
    /// </summary>
    [DataMember]
    [Column(FieldPremiumCharge, DisPremiumCharge)]
    public decimal PremiumCharge { get; set; }

    /// <summary>
    /// 装卸费
    /// </summary>
    [DataMember]
    [Column(FieldUnloadingCharge, DisUnloadingCharge)]
    public decimal UnloadingCharge { get; set; }

    /// <summary>
    /// 上楼费
    /// </summary>
    [DataMember]
    [Column(FieldUpstairsCharge, DisUpstairsCharge)]
    public decimal UpstairsCharge { get; set; }

    /// <summary>
    /// 通知费
    /// </summary>
    [DataMember]
    [Column(FieldWaitNoticeCharge, DisWaitNoticeCharge)]
    public decimal WaitNoticeCharge { get; set; }

    /// <summary>
    /// 回单费
    /// </summary>
    [DataMember]
    [Column(FieldAckRecCharge, DisAckRecCharge)]
    public decimal AckRecCharge { get; set; }

    /// <summary>
    /// 信息费
    /// </summary>
    [DataMember]
    [Column(FieldInformationCharge, DisInformationCharge)]
    public decimal InformationCharge { get; set; }

    /// <summary>
    /// 包装费
    /// </summary>
    [DataMember]
    [Column(FieldPackageCharge, DisPackageCharge)]
    public decimal PackageCharge { get; set; }

    /// <summary>
    /// 提货费
    /// </summary>
    [DataMember]
    [Column(FieldPickupCharge, DisPickupCharge)]
    public decimal PickupCharge { get; set; }

    /// <summary>
    /// 中转费
    /// </summary>
    [DataMember]
    [Column(FieldTransferCharge, DisTransferCharge)]
    public decimal TransferCharge { get; set; }

    /// <summary>
    /// 其他费
    /// </summary>
    [DataMember]
    [Column(FieldOtherCharge, DisOtherCharge)]
    public decimal OtherCharge { get; set; }

    /// <summary>
    /// 代收款
    /// </summary>
    [DataMember]
    [Column(FieldAgencyReceiveCharge, DisAgencyReceiveCharge)]
    public decimal AgencyReceiveCharge { get; set; }

    /// <summary>
    /// 代收款手续费
    /// </summary>
    [DataMember]
    [Column(FieldAgencyReceiveChargePoundage, DisAgencyReceiveChargePoundage)]
    public decimal AgencyReceiveChargePoundage { get; set; }

    /// <summary>
    /// 回扣
    /// </summary>
    [DataMember]
    [Column(FieldBrokerage, DisBrokerage)]
    public decimal Brokerage { get; set; }

    /// <summary>
    /// 保险费率
    /// </summary>
    [DataMember]
    [Column(FieldPremiumRate, DisPremiumRate)]
    public decimal PremiumRate { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    [DataMember]
    [Sort(IsDesc)]
    [Column(FieldCreationDate, DisCreationDate)]
    public DateTime CreationDate { get; set; }

    /// <summary>
    /// 创建人
    /// </summary>
    [DataMember]
    [Column(FieldCreatedBy, DisCreatedBy)]
    public string CreatedBy { get; set; }

    /// <summary>
    /// 修改时间
    /// </summary>
    [DataMember]
    [OptimisticLock]
    [Column(FieldLastUpdateDate, DisLastUpdateDate)]
    public DateTime LastUpdateDate { get; set; }

    /// <summary>
    /// 修改人
    /// </summary>
    [DataMember]
    [Column(FieldLastUpdatedBy, DisLastUpdatedBy)]
    public string LastUpdatedBy { get; set; }

    /// <summary>
    /// 作废
    /// </summary>
    [DataMember]
    [Column(FieldCancelYN, DisCancelYN)]
    public bool CancelYN { get; set; }

    #endregion

    #region 定义字段名称相关常量

    /// <summary>
    /// 表名
    /// </summary>
    [NonSerialized]
    public new const string DBTableName = "tb_TranWaybills";

    /// <summary>
    /// 主键名
    /// </summary>
    [NonSerialized]
    public new const string PrimaryKey = FieldISID;

    /// <summary>
    /// 外键名
    /// </summary>
    [NonSerialized]
    public new const string ForeignKey = FieldWaybillNo;

    /// <summary>
    /// 排序字段
    /// </summary>
    [NonSerialized]
    public new const string SortKey = FieldCreationDate;

    /// <summary>
    /// 排序方式
    /// </summary>
    [NonSerialized]
    public new const bool IsDesc = true;

    /// <summary>
    /// 乐观锁字段
    /// </summary>
    [NonSerialized]
    public new const string OptimisticLockKey = FieldLastUpdateDate;

    #region 列名
    /// <summary>
    /// 自增ID
    /// </summary>
    [NonSerialized]
    public const string FieldISID = "ISID";

    /// <summary>
    /// 运单号
    /// </summary>
    [NonSerialized]
    public const string FieldWaybillNo = "WaybillNo";

    /// <summary>
    /// 货物类型
    /// </summary>
    [NonSerialized]
    public const string FieldCargoType = "CargoType";

    /// <summary>
    /// 货物名称
    /// </summary>
    [NonSerialized]
    public const string FieldCargoName = "CargoName";

    /// <summary>
    /// 包装类型
    /// </summary>
    [NonSerialized]
    public const string FieldPackageType = "PackageType";

    /// <summary>
    /// 货物单位
    /// </summary>
    [NonSerialized]
    public const string FieldCargoUnit = "CargoUnit";

    /// <summary>
    /// 件数
    /// </summary>
    [NonSerialized]
    public const string FieldQty = "Qty";

    /// <summary>
    /// 重量
    /// </summary>
    [NonSerialized]
    public const string FieldWeight = "Weight";

    /// <summary>
    /// 体积
    /// </summary>
    [NonSerialized]
    public const string FieldCubage = "Cubage";

    /// <summary>
    /// 长
    /// </summary>
    [NonSerialized]
    public const string FieldCubageLength = "CubageLength";

    /// <summary>
    /// 宽
    /// </summary>
    [NonSerialized]
    public const string FieldCubageWidth = "CubageWidth";

    /// <summary>
    /// 高
    /// </summary>
    [NonSerialized]
    public const string FieldCubageHight = "CubageHight";

    /// <summary>
    /// 单价
    /// </summary>
    [NonSerialized]
    public const string FieldPrice = "Price";

    /// <summary>
    /// 报价类型
    /// </summary>
    [NonSerialized]
    public const string FieldPriceType = "PriceType";

    /// <summary>
    /// 保价金额
    /// </summary>
    [NonSerialized]
    public const string FieldAmountInsured = "AmountInsured";

    /// <summary>
    /// 运费
    /// </summary>
    [NonSerialized]
    public const string FieldCarriageCharge = "CarriageCharge";

    /// <summary>
    /// 送货费
    /// </summary>
    [NonSerialized]
    public const string FieldDeliveryCharge = "DeliveryCharge";

    /// <summary>
    /// 保险费
    /// </summary>
    [NonSerialized]
    public const string FieldPremiumCharge = "PremiumCharge";

    /// <summary>
    /// 装卸费
    /// </summary>
    [NonSerialized]
    public const string FieldUnloadingCharge = "UnloadingCharge";

    /// <summary>
    /// 上楼费
    /// </summary>
    [NonSerialized]
    public const string FieldUpstairsCharge = "UpstairsCharge";

    /// <summary>
    /// 通知费
    /// </summary>
    [NonSerialized]
    public const string FieldWaitNoticeCharge = "WaitNoticeCharge";

    /// <summary>
    /// 回单费
    /// </summary>
    [NonSerialized]
    public const string FieldAckRecCharge = "AckRecCharge";

    /// <summary>
    /// 信息费
    /// </summary>
    [NonSerialized]
    public const string FieldInformationCharge = "InformationCharge";

    /// <summary>
    /// 包装费
    /// </summary>
    [NonSerialized]
    public const string FieldPackageCharge = "PackageCharge";

    /// <summary>
    /// 提货费
    /// </summary>
    [NonSerialized]
    public const string FieldPickupCharge = "PickupCharge";

    /// <summary>
    /// 中转费
    /// </summary>
    [NonSerialized]
    public const string FieldTransferCharge = "TransferCharge";

    /// <summary>
    /// 其他费
    /// </summary>
    [NonSerialized]
    public const string FieldOtherCharge = "OtherCharge";

    /// <summary>
    /// 代收款
    /// </summary>
    [NonSerialized]
    public const string FieldAgencyReceiveCharge = "AgencyReceiveCharge";

    /// <summary>
    /// 代收款手续费
    /// </summary>
    [NonSerialized]
    public const string FieldAgencyReceiveChargePoundage = "AgencyReceiveChargePoundage";

    /// <summary>
    /// 回扣
    /// </summary>
    [NonSerialized]
    public const string FieldBrokerage = "Brokerage";

    /// <summary>
    /// 保险费率
    /// </summary>
    [NonSerialized]
    public const string FieldPremiumRate = "PremiumRate";

    /// <summary>
    /// 创建时间
    /// </summary>
    [NonSerialized]
    public const string FieldCreationDate = "CreationDate";

    /// <summary>
    /// 创建人
    /// </summary>
    [NonSerialized]
    public const string FieldCreatedBy = "CreatedBy";

    /// <summary>
    /// 修改时间
    /// </summary>
    [NonSerialized]
    public const string FieldLastUpdateDate = "LastUpdateDate";

    /// <summary>
    /// 修改人
    /// </summary>
    [NonSerialized]
    public const string FieldLastUpdatedBy = "LastUpdatedBy";

    /// <summary>
    /// 作废
    /// </summary>
    [NonSerialized]
    public const string FieldCancelYN = "CancelYN";

    #endregion

    #region 列显示名
    /// <summary>
    /// 自增ID
    /// </summary>
    [NonSerialized]
    public const string DisISID = "自增ID";

    /// <summary>
    /// 运单号
    /// </summary>
    [NonSerialized]
    public const string DisWaybillNo = "运单号";

    /// <summary>
    /// 货物类型
    /// </summary>
    [NonSerialized]
    public const string DisCargoType = "货物类型";

    /// <summary>
    /// 货物名称
    /// </summary>
    [NonSerialized]
    public const string DisCargoName = "货物名称";

    /// <summary>
    /// 包装类型
    /// </summary>
    [NonSerialized]
    public const string DisPackageType = "包装类型";

    /// <summary>
    /// 货物单位
    /// </summary>
    [NonSerialized]
    public const string DisCargoUnit = "货物单位";

    /// <summary>
    /// 件数
    /// </summary>
    [NonSerialized]
    public const string DisQty = "件数";

    /// <summary>
    /// 重量
    /// </summary>
    [NonSerialized]
    public const string DisWeight = "重量";

    /// <summary>
    /// 体积
    /// </summary>
    [NonSerialized]
    public const string DisCubage = "体积";

    /// <summary>
    /// 长
    /// </summary>
    [NonSerialized]
    public const string DisCubageLength = "长";

    /// <summary>
    /// 宽
    /// </summary>
    [NonSerialized]
    public const string DisCubageWidth = "宽";

    /// <summary>
    /// 高
    /// </summary>
    [NonSerialized]
    public const string DisCubageHight = "高";

    /// <summary>
    /// 单价
    /// </summary>
    [NonSerialized]
    public const string DisPrice = "单价";

    /// <summary>
    /// 报价类型
    /// </summary>
    [NonSerialized]
    public const string DisPriceType = "报价类型";

    /// <summary>
    /// 保价金额
    /// </summary>
    [NonSerialized]
    public const string DisAmountInsured = "保价金额";

    /// <summary>
    /// 运费
    /// </summary>
    [NonSerialized]
    public const string DisCarriageCharge = "运费";

    /// <summary>
    /// 送货费
    /// </summary>
    [NonSerialized]
    public const string DisDeliveryCharge = "送货费";

    /// <summary>
    /// 保险费
    /// </summary>
    [NonSerialized]
    public const string DisPremiumCharge = "保险费";

    /// <summary>
    /// 装卸费
    /// </summary>
    [NonSerialized]
    public const string DisUnloadingCharge = "装卸费";

    /// <summary>
    /// 上楼费
    /// </summary>
    [NonSerialized]
    public const string DisUpstairsCharge = "上楼费";

    /// <summary>
    /// 通知费
    /// </summary>
    [NonSerialized]
    public const string DisWaitNoticeCharge = "通知费";

    /// <summary>
    /// 回单费
    /// </summary>
    [NonSerialized]
    public const string DisAckRecCharge = "回单费";

    /// <summary>
    /// 信息费
    /// </summary>
    [NonSerialized]
    public const string DisInformationCharge = "信息费";

    /// <summary>
    /// 包装费
    /// </summary>
    [NonSerialized]
    public const string DisPackageCharge = "包装费";

    /// <summary>
    /// 提货费
    /// </summary>
    [NonSerialized]
    public const string DisPickupCharge = "提货费";

    /// <summary>
    /// 中转费
    /// </summary>
    [NonSerialized]
    public const string DisTransferCharge = "中转费";

    /// <summary>
    /// 其他费
    /// </summary>
    [NonSerialized]
    public const string DisOtherCharge = "其他费";

    /// <summary>
    /// 代收款
    /// </summary>
    [NonSerialized]
    public const string DisAgencyReceiveCharge = "代收款";

    /// <summary>
    /// 代收款手续费
    /// </summary>
    [NonSerialized]
    public const string DisAgencyReceiveChargePoundage = "代收款手续费";

    /// <summary>
    /// 回扣
    /// </summary>
    [NonSerialized]
    public const string DisBrokerage = "回扣";

    /// <summary>
    /// 保险费率
    /// </summary>
    [NonSerialized]
    public const string DisPremiumRate = "保险费率";

    /// <summary>
    /// 创建时间
    /// </summary>
    [NonSerialized]
    public const string DisCreationDate = "创建时间";

    /// <summary>
    /// 创建人
    /// </summary>
    [NonSerialized]
    public const string DisCreatedBy = "创建人";

    /// <summary>
    /// 修改时间
    /// </summary>
    [NonSerialized]
    public const string DisLastUpdateDate = "修改时间";

    /// <summary>
    /// 修改人
    /// </summary>
    [NonSerialized]
    public const string DisLastUpdatedBy = "修改人";

    /// <summary>
    /// 作废
    /// </summary>
    [NonSerialized]
    public const string DisCancelYN = "作废";

    #endregion

    #endregion
}