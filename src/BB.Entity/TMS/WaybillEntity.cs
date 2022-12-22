using System.Runtime.Serialization;
using BB.Entity.Base;
using BB.Tools.Entity;
using SqlSugar;

namespace BB.Entity.TMS;

/// <summary>
/// 运单管理 实体类
/// </summary>
[DataContract]
[Serializable]
[Table(DBTableName)]
public sealed class Waybill : BaseEntity<Waybills>
{
    /// <summary>
    /// 默认构造函数（需要初始化属性的在此处理，复杂的属性值初始化通过重写 _bll.NewEntity 方法实现）
    /// </summary>
    public Waybill()
    {
        WaybillNo = "*自动生成*";
        WaybillDate = DateTime.Now;
        FromNode = "*当前机构*";
        WaitNoticeYN = false;
        AbnormityType = false;
        Qty = 0;
        Weight = 0;
        Cubage = 0;
        ChargeableWeight = 0;
        AgencyReceiveCharge = 0;
        CarriagePrepaid = 0;
        CarriageForward = 0;
        CarriageMonthly = 0;
        CarriageReceipt = 0;
        CarriageOwed = 0;
        CarriageOther = 0;
        Brokerage = 0;
        AmountInsured = 0;
        UnloadYN = false;
        UpstairYN = false;
        UpstairNum = 0;
        CloseYN = false;
        CreationDate = DateTime.Now;
        CreatedBy = "*当前用户*";
        LastUpdateDate = DateTime.Now;
        LastUpdatedBy = "*当前用户*";
        FlagApp = false;
        AutoNoYN = false;
        AckRecCancelYN = false;
        AutoReceipt = false;
    }

    #region Property Members

    /// <summary>
    /// 运单号
    /// </summary>
    [DataMember]
    [Key]
    [Column(FieldWaybillNo, DisWaybillNo)]
    public string WaybillNo { get; set; }

    /// <summary>
    /// 手动编号
    /// </summary>
    [DataMember]
    [Hide]
    [Column(FieldWaybillNoDef, DisWaybillNoDef)]
    public string WaybillNoDef { get; set; }

    /// <summary>
    /// 运单时间
    /// </summary>
    [DataMember]
    [Column(FieldWaybillDate, DisWaybillDate)]
    public DateTime WaybillDate { get; set; }

    /// <summary>
    /// 转入来源
    /// </summary>
    [DataMember]
    [Hide]
    [Column(FieldTransferInType, DisTransferInType)]
    public string TransferInType { get; set; }

    /// <summary>
    /// 转入单号
    /// </summary>
    [DataMember]
    [Column(FieldTransferInNo, DisTransferInNo)]
    public string TransferInNo { get; set; }

    /// <summary>
    /// 转出类型
    /// </summary>
    [DataMember]
    [Hide]
    [Column(FieldTransferOutType, DisTransferOutType)]
    public string TransferOutType { get; set; }

    /// <summary>
    /// 转出单号
    /// </summary>
    [DataMember]
    [Hide]
    [Column(FieldTransferOutNo, DisTransferOutNo)]
    public string TransferOutNo { get; set; }

    /// <summary>
    /// 发货客户编号
    /// </summary>
    [DataMember]
    [Hide]
    [Column(FieldTranCustomer, DisTranCustomer)]
    public string TranCustomer { get; set; }

    /// <summary>
    /// 订单类型
    /// </summary>
    [DataMember]
    [Hide]
    [Column(FieldOrderType, DisOrderType)]
    public string OrderType { get; set; }

    /// <summary>
    /// 订单号
    /// </summary>
    [DataMember]
    [Column(FieldOrderNo, DisOrderNo)]
    public string OrderNo { get; set; }

    /// <summary>
    /// 起始网点编号
    /// </summary>
    [DataMember]
    [Column(FieldFromNode, DisFromNode)]
    public string FromNode { get; set; }

    /// <summary>
    /// 起始区域编号
    /// </summary>
    [DataMember]
    [Column(FieldFromNodes, DisFromNodes)]
    public int FromNodes { get; set; }

    /// <summary>
    /// 发货省
    /// </summary>
    [DataMember]
    [Column(FieldFromProvinceId, DisFromProvinceId)]
    public string FromProvinceId { get; set; }

    /// <summary>
    /// 发货市
    /// </summary>
    [DataMember]
    [Column(FieldFromCityId, DisFromCityId)]
    public string FromCityId { get; set; }

    /// <summary>
    /// 发货区
    /// </summary>
    [DataMember]
    [Column(FieldFromAreaId, DisFromAreaId)]
    public string FromAreaId { get; set; }

    /// <summary>
    /// 目的网点编号
    /// </summary>
    [DataMember]
    [Column(FieldToNode, DisToNode)]
    public string ToNode { get; set; }

    /// <summary>
    /// 目的区域编号
    /// </summary>
    [DataMember]
    [Column(FieldToNodes, DisToNodes)]
    public int ToNodes { get; set; }

    /// <summary>
    /// 目的省
    /// </summary>
    [DataMember]
    [Column(FieldToProvinceId, DisToProvinceId)]
    public string ToProvinceId { get; set; }

    /// <summary>
    /// 目的市
    /// </summary>
    [DataMember]
    [Column(FieldToCityId, DisToCityId)]
    public string ToCityId { get; set; }

    /// <summary>
    /// 目的区
    /// </summary>
    [DataMember]
    [Column(FieldToAreaId, DisToAreaId)]
    public string ToAreaId { get; set; }

    /// <summary>
    /// 发货公司
    /// </summary>
    [DataMember]
    [Column(FieldShipperCompanyName, DisShipperCompanyName)]
    public string ShipperCompanyName { get; set; }

    /// <summary>
    /// 发货地址
    /// </summary>
    [DataMember]
    [Column(FieldShipperAddress, DisShipperAddress)]
    public string ShipperAddress { get; set; }

    /// <summary>
    /// 发货电话
    /// </summary>
    [DataMember]
    [Column(FieldShipperTel, DisShipperTel)]
    public string ShipperTel { get; set; }

    /// <summary>
    /// 发货人
    /// </summary>
    [DataMember]
    [Column(FieldShipper, DisShipper)]
    public string Shipper { get; set; }

    /// <summary>
    /// 收货公司
    /// </summary>
    [DataMember]
    [Column(FieldConsigneeCompanyName, DisConsigneeCompanyName)]
    public string ConsigneeCompanyName { get; set; }

    /// <summary>
    /// 收货地址
    /// </summary>
    [DataMember]
    [Column(FieldConsigneeAddress, DisConsigneeAddress)]
    public string ConsigneeAddress { get; set; }

    /// <summary>
    /// 收货电话
    /// </summary>
    [DataMember]
    [Column(FieldConsigneeTel, DisConsigneeTel)]
    public string ConsigneeTel { get; set; }

    /// <summary>
    /// 收货人
    /// </summary>
    [DataMember]
    [Column(FieldConsignee, DisConsignee)]
    public string Consignee { get; set; }

    /// <summary>
    /// 运输方式
    /// </summary>
    [DataMember]
    [Column(FieldTransportType, DisTransportType)]
    public string TransportType { get; set; }

    /// <summary>
    /// 提货方式
    /// </summary>
    [DataMember]
    [Column(FieldPickUpType, DisPickUpType)]
    public string PickUpType { get; set; }

    /// <summary>
    /// 交货方式
    /// </summary>
    [DataMember]
    [Column(FieldDeliveryType, DisDeliveryType)]
    public string DeliveryType { get; set; }

    /// <summary>
    /// 付款方式
    /// </summary>
    [DataMember]
    [Column(FieldPaymentType, DisPaymentType)]
    public string PaymentType { get; set; }

    /// <summary>
    /// 等通知放货
    /// </summary>
    [DataMember]
    [Column(FieldWaitNoticeYN, DisWaitNoticeYN)]
    public bool WaitNoticeYN { get; set; }

    /// <summary>
    /// 通知人员
    /// </summary>
    [DataMember]
    [Column(FieldNoticeUser, DisNoticeUser)]
    public string NoticeUser { get; set; }

    /// <summary>
    /// 通知日期
    /// </summary>
    [DataMember]
    [Column(FieldNoticeDate, DisNoticeDate)]
    public DateTime? NoticeDate { get; set; }

    /// <summary>
    /// 通知备注
    /// </summary>
    [DataMember]
    [Column(FieldNoticeRemark, DisNoticeRemark)]
    public string NoticeRemark { get; set; }

    /// <summary>
    /// 回单数量
    /// </summary>
    [DataMember]
    [Hide]
    [Column(FieldAckRecQty, DisAckRecQty)]
    public int? AckRecQty { get; set; }

    /// <summary>
    /// 回单类型
    /// </summary>
    [DataMember]
    [Hide]
    [Column(FieldAckRecType, DisAckRecType)]
    public string AckRecType { get; set; }

    /// <summary>
    /// 回单号
    /// </summary>
    [DataMember]
    [Column(FieldAckRecNo, DisAckRecNo)]
    public string AckRecNo { get; set; }

    /// <summary>
    /// 异形件
    /// </summary>
    [DataMember]
    [Column(FieldAbnormityType, DisAbnormityType)]
    public bool AbnormityType { get; set; }

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
    [Hide]
    [Column(FieldCargoName, DisCargoName)]
    public string CargoName { get; set; }

    /// <summary>
    /// 包装类型
    /// </summary>
    [DataMember]
    [Hide]
    [Column(FieldPackageType, DisPackageType)]
    public string PackageType { get; set; }

    /// <summary>
    /// 货物单位
    /// </summary>
    [DataMember]
    [Hide]
    [Column(FieldCargoUnit, DisCargoUnit)]
    public string CargoUnit { get; set; }

    /// <summary>
    /// 数量
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
    /// 计费重量
    /// </summary>
    [DataMember]
    [Column(FieldChargeableWeight, DisChargeableWeight)]
    public decimal ChargeableWeight { get; set; }

    /// <summary>
    /// 代收款
    /// </summary>
    [DataMember]
    [Column(FieldAgencyReceiveCharge, DisAgencyReceiveCharge)]
    public decimal AgencyReceiveCharge { get; set; }

    /// <summary>
    /// 现付金额
    /// </summary>
    [DataMember]
    [Column(FieldCarriagePrepaid, DisCarriagePrepaid)]
    public decimal CarriagePrepaid { get; set; }

    /// <summary>
    /// 到付金额
    /// </summary>
    [DataMember]
    [Column(FieldCarriageForward, DisCarriageForward)]
    public decimal CarriageForward { get; set; }

    /// <summary>
    /// 月结金额
    /// </summary>
    [DataMember]
    [Column(FieldCarriageMonthly, DisCarriageMonthly)]
    public decimal CarriageMonthly { get; set; }

    /// <summary>
    /// 回付金额
    /// </summary>
    [DataMember]
    [Column(FieldCarriageReceipt, DisCarriageReceipt)]
    public decimal CarriageReceipt { get; set; }

    /// <summary>
    /// 欠付金额
    /// </summary>
    [DataMember]
    [Column(FieldCarriageOwed, DisCarriageOwed)]
    public decimal CarriageOwed { get; set; }

    /// <summary>
    /// 其他金额
    /// </summary>
    [DataMember]
    [Column(FieldCarriageOther, DisCarriageOther)]
    public decimal CarriageOther { get; set; }

    /// <summary>
    /// 回扣金额
    /// </summary>
    [DataMember]
    [Column(FieldBrokerage, DisBrokerage)]
    public decimal Brokerage { get; set; }

    /// <summary>
    /// 保价金额
    /// </summary>
    [DataMember]
    [Column(FieldAmountInsured, DisAmountInsured)]
    public decimal AmountInsured { get; set; }

    /// <summary>
    /// 状态网点
    /// </summary>
    [DataMember]
    [Column(FieldStatusNode, DisStatusNode)]
    public string StatusNode { get; set; }

    /// <summary>
    /// 运单状态
    /// </summary>
    [DataMember]
    [Column(FieldStatusId, DisStatusId)]
    public string StatusId { get; set; }

    /// <summary>
    /// 卸货
    /// </summary>
    [DataMember]
    [Column(FieldUnloadYN, DisUnloadYN)]
    public bool UnloadYN { get; set; }

    /// <summary>
    /// 上楼
    /// </summary>
    [DataMember]
    [Column(FieldUpstairYN, DisUpstairYN)]
    public bool UpstairYN { get; set; }

    /// <summary>
    /// 上楼层数
    /// </summary>
    [DataMember]
    [Column(FieldUpstairNum, DisUpstairNum)]
    public byte? UpstairNum { get; set; }

    /// <summary>
    /// 业务员
    /// </summary>
    [DataMember]
    [Column(FieldSalesMan, DisSalesMan)]
    public string SalesMan { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [DataMember]
    [Column(FieldRemark, DisRemark)]
    public string Remark { get; set; }

    /// <summary>
    /// 取消
    /// </summary>
    [DataMember]
    [Hide]
    [Column(FieldCloseYN, DisCloseYN)]
    public bool CloseYN { get; set; }

    /// <summary>
    /// 取消人
    /// </summary>
    [DataMember]
    [Hide]
    [Column(FieldCloseUser, DisCloseUser)]
    public string CloseUser { get; set; }

    /// <summary>
    /// 取消日期
    /// </summary>
    [DataMember]
    [Hide]
    [Column(FieldCloseDate, DisCloseDate)]
    public DateTime? CloseDate { get; set; }

    /// <summary>
    /// 取消时间
    /// </summary>
    [DataMember]
    [Hide]
    [Column(FieldCloseTime, DisCloseTime)]
    public DateTime? CloseTime { get; set; }

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
    /// 审核
    /// </summary>
    [DataMember]
    [Column(FieldFlagApp, DisFlagApp)]
    public bool FlagApp { get; set; }

    /// <summary>
    /// 审核人
    /// </summary>
    [DataMember]
    [Column(FieldAppUser, DisAppUser)]
    public string AppUser { get; set; }

    /// <summary>
    /// 审核时间
    /// </summary>
    [DataMember]
    [Column(FieldAppDate, DisAppDate)]
    public DateTime? AppDate { get; set; }

    /// <summary>
    /// 自动单号
    /// </summary>
    [DataMember]
    [Column(FieldAutoNoYN, DisAutoNoYN)]
    public bool AutoNoYN { get; set; }

    /// <summary>
    /// 回单撤销状态
    /// </summary>
    [DataMember]
    [Hide]
    [Column(FieldAckRecCancelYN, DisAckRecCancelYN)]
    public bool AckRecCancelYN { get; set; }

    /// <summary>
    /// 自动回单
    /// </summary>
    [DataMember]
    [Column(FieldAutoReceipt, DisAutoReceipt)]
    public bool AutoReceipt { get; set; }

    /// <summary>
    /// 运单货物明细 集合
    /// </summary>
    [Ignore]
    public IEnumerable<Waybills> WaybillsList { get; set; }

    #endregion

    #region 定义字段名称相关常量

    /// <summary>
    /// 表名
    /// </summary>
    [NonSerialized]
    public new const string DBTableName = "tb_TranWaybill";

    /// <summary>
    /// 主键名
    /// </summary>
    [NonSerialized]
    public new const string PrimaryKey = FieldWaybillNo;

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

    /// <summary>
    /// 子表数据
    /// </summary>
    [DataMember]
    [Ignore]
    [Navigate(NavigateType.OneToMany, ChildForeignKey)]
    public new List<Waybills>? ChildTableList
    {
        get => base.ChildTableList;
        set => base.ChildTableList = value;
    }

    /// <summary>
    /// 子表外键
    /// </summary>
    [NonSerialized]
    public new const string ChildForeignKey = Waybills.ForeignKey;

    #region 列名
    /// <summary>
    /// 运单号
    /// </summary>
    [NonSerialized]
    public const string FieldWaybillNo = "WaybillNo";

    /// <summary>
    /// 手动编号
    /// </summary>
    [NonSerialized]
    public const string FieldWaybillNoDef = "WaybillNoDef";

    /// <summary>
    /// 运单时间
    /// </summary>
    [NonSerialized]
    public const string FieldWaybillDate = "WaybillDate";

    /// <summary>
    /// 转入来源
    /// </summary>
    [NonSerialized]
    public const string FieldTransferInType = "TransferInType";

    /// <summary>
    /// 转入单号
    /// </summary>
    [NonSerialized]
    public const string FieldTransferInNo = "TransferInNo";

    /// <summary>
    /// 转出类型
    /// </summary>
    [NonSerialized]
    public const string FieldTransferOutType = "TransferOutType";

    /// <summary>
    /// 转出单号
    /// </summary>
    [NonSerialized]
    public const string FieldTransferOutNo = "TransferOutNo";

    /// <summary>
    /// 发货客户编号
    /// </summary>
    [NonSerialized]
    public const string FieldTranCustomer = "TranCustomer";

    /// <summary>
    /// 订单类型
    /// </summary>
    [NonSerialized]
    public const string FieldOrderType = "OrderType";

    /// <summary>
    /// 订单号
    /// </summary>
    [NonSerialized]
    public const string FieldOrderNo = "OrderNo";

    /// <summary>
    /// 起始网点编号
    /// </summary>
    [NonSerialized]
    public const string FieldFromNode = "FromNode";

    /// <summary>
    /// 起始区域编号
    /// </summary>
    [NonSerialized]
    public const string FieldFromNodes = "FromNodes";

    /// <summary>
    /// 发货省
    /// </summary>
    [NonSerialized]
    public const string FieldFromProvinceId = "FromProvinceId";

    /// <summary>
    /// 发货市
    /// </summary>
    [NonSerialized]
    public const string FieldFromCityId = "FromCityId";

    /// <summary>
    /// 发货区
    /// </summary>
    [NonSerialized]
    public const string FieldFromAreaId = "FromAreaId";

    /// <summary>
    /// 目的网点编号
    /// </summary>
    [NonSerialized]
    public const string FieldToNode = "ToNode";

    /// <summary>
    /// 目的区域编号
    /// </summary>
    [NonSerialized]
    public const string FieldToNodes = "ToNodes";

    /// <summary>
    /// 目的省
    /// </summary>
    [NonSerialized]
    public const string FieldToProvinceId = "ToProvinceId";

    /// <summary>
    /// 目的市
    /// </summary>
    [NonSerialized]
    public const string FieldToCityId = "ToCityId";

    /// <summary>
    /// 目的区
    /// </summary>
    [NonSerialized]
    public const string FieldToAreaId = "ToAreaId";

    /// <summary>
    /// 发货公司
    /// </summary>
    [NonSerialized]
    public const string FieldShipperCompanyName = "ShipperCompanyName";

    /// <summary>
    /// 发货地址
    /// </summary>
    [NonSerialized]
    public const string FieldShipperAddress = "ShipperAddress";

    /// <summary>
    /// 发货电话
    /// </summary>
    [NonSerialized]
    public const string FieldShipperTel = "ShipperTel";

    /// <summary>
    /// 发货人
    /// </summary>
    [NonSerialized]
    public const string FieldShipper = "Shipper";

    /// <summary>
    /// 收货公司
    /// </summary>
    [NonSerialized]
    public const string FieldConsigneeCompanyName = "ConsigneeCompanyName";

    /// <summary>
    /// 收货地址
    /// </summary>
    [NonSerialized]
    public const string FieldConsigneeAddress = "ConsigneeAddress";

    /// <summary>
    /// 收货电话
    /// </summary>
    [NonSerialized]
    public const string FieldConsigneeTel = "ConsigneeTel";

    /// <summary>
    /// 收货人
    /// </summary>
    [NonSerialized]
    public const string FieldConsignee = "Consignee";

    /// <summary>
    /// 运输方式
    /// </summary>
    [NonSerialized]
    public const string FieldTransportType = "TransportType";

    /// <summary>
    /// 提货方式
    /// </summary>
    [NonSerialized]
    public const string FieldPickUpType = "PickUpType";

    /// <summary>
    /// 交货方式
    /// </summary>
    [NonSerialized]
    public const string FieldDeliveryType = "DeliveryType";

    /// <summary>
    /// 付款方式
    /// </summary>
    [NonSerialized]
    public const string FieldPaymentType = "PaymentType";

    /// <summary>
    /// 等通知放货
    /// </summary>
    [NonSerialized]
    public const string FieldWaitNoticeYN = "WaitNoticeYN";

    /// <summary>
    /// 通知人员
    /// </summary>
    [NonSerialized]
    public const string FieldNoticeUser = "NoticeUser";

    /// <summary>
    /// 通知日期
    /// </summary>
    [NonSerialized]
    public const string FieldNoticeDate = "NoticeDate";

    /// <summary>
    /// 通知备注
    /// </summary>
    [NonSerialized]
    public const string FieldNoticeRemark = "NoticeRemark";

    /// <summary>
    /// 回单数量
    /// </summary>
    [NonSerialized]
    public const string FieldAckRecQty = "AckRecQty";

    /// <summary>
    /// 回单类型
    /// </summary>
    [NonSerialized]
    public const string FieldAckRecType = "AckRecType";

    /// <summary>
    /// 回单号
    /// </summary>
    [NonSerialized]
    public const string FieldAckRecNo = "AckRecNo";

    /// <summary>
    /// 异形件
    /// </summary>
    [NonSerialized]
    public const string FieldAbnormityType = "AbnormityType";

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
    /// 数量
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
    /// 计费重量
    /// </summary>
    [NonSerialized]
    public const string FieldChargeableWeight = "ChargeableWeight";

    /// <summary>
    /// 代收款
    /// </summary>
    [NonSerialized]
    public const string FieldAgencyReceiveCharge = "AgencyReceiveCharge";

    /// <summary>
    /// 现付金额
    /// </summary>
    [NonSerialized]
    public const string FieldCarriagePrepaid = "CarriagePrepaid";

    /// <summary>
    /// 到付金额
    /// </summary>
    [NonSerialized]
    public const string FieldCarriageForward = "CarriageForward";

    /// <summary>
    /// 月结金额
    /// </summary>
    [NonSerialized]
    public const string FieldCarriageMonthly = "CarriageMonthly";

    /// <summary>
    /// 回付金额
    /// </summary>
    [NonSerialized]
    public const string FieldCarriageReceipt = "CarriageReceipt";

    /// <summary>
    /// 欠付金额
    /// </summary>
    [NonSerialized]
    public const string FieldCarriageOwed = "CarriageOwed";

    /// <summary>
    /// 其他金额
    /// </summary>
    [NonSerialized]
    public const string FieldCarriageOther = "CarriageOther";

    /// <summary>
    /// 回扣金额
    /// </summary>
    [NonSerialized]
    public const string FieldBrokerage = "Brokerage";

    /// <summary>
    /// 保价金额
    /// </summary>
    [NonSerialized]
    public const string FieldAmountInsured = "AmountInsured";

    /// <summary>
    /// 状态网点
    /// </summary>
    [NonSerialized]
    public const string FieldStatusNode = "StatusNode";

    /// <summary>
    /// 运单状态
    /// </summary>
    [NonSerialized]
    public const string FieldStatusId = "StatusId";

    /// <summary>
    /// 卸货
    /// </summary>
    [NonSerialized]
    public const string FieldUnloadYN = "UnloadYN";

    /// <summary>
    /// 上楼
    /// </summary>
    [NonSerialized]
    public const string FieldUpstairYN = "UpstairYN";

    /// <summary>
    /// 上楼层数
    /// </summary>
    [NonSerialized]
    public const string FieldUpstairNum = "UpstairNum";

    /// <summary>
    /// 业务员
    /// </summary>
    [NonSerialized]
    public const string FieldSalesMan = "SalesMan";

    /// <summary>
    /// 备注
    /// </summary>
    [NonSerialized]
    public const string FieldRemark = "Remark";

    /// <summary>
    /// 取消
    /// </summary>
    [NonSerialized]
    public const string FieldCloseYN = "CloseYN";

    /// <summary>
    /// 取消人
    /// </summary>
    [NonSerialized]
    public const string FieldCloseUser = "CloseUser";

    /// <summary>
    /// 取消日期
    /// </summary>
    [NonSerialized]
    public const string FieldCloseDate = "CloseDate";

    /// <summary>
    /// 取消时间
    /// </summary>
    [NonSerialized]
    public const string FieldCloseTime = "CloseTime";

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
    /// 审核
    /// </summary>
    [NonSerialized]
    public const string FieldFlagApp = "FlagApp";

    /// <summary>
    /// 审核人
    /// </summary>
    [NonSerialized]
    public const string FieldAppUser = "AppUser";

    /// <summary>
    /// 审核时间
    /// </summary>
    [NonSerialized]
    public const string FieldAppDate = "AppDate";

    /// <summary>
    /// 自动单号
    /// </summary>
    [NonSerialized]
    public const string FieldAutoNoYN = "AutoNoYN";

    /// <summary>
    /// 回单撤销状态
    /// </summary>
    [NonSerialized]
    public const string FieldAckRecCancelYN = "AckRecCancelYN";

    /// <summary>
    /// 自动回单
    /// </summary>
    [NonSerialized]
    public const string FieldAutoReceipt = "AutoReceipt";

    #endregion

    #region 列显示名
    /// <summary>
    /// 运单号
    /// </summary>
    [NonSerialized]
    public const string DisWaybillNo = "运单号";

    /// <summary>
    /// 手动编号
    /// </summary>
    [NonSerialized]
    public const string DisWaybillNoDef = "手动编号";

    /// <summary>
    /// 运单时间
    /// </summary>
    [NonSerialized]
    public const string DisWaybillDate = "运单时间";

    /// <summary>
    /// 转入来源
    /// </summary>
    [NonSerialized]
    public const string DisTransferInType = "转入来源";

    /// <summary>
    /// 转入单号
    /// </summary>
    [NonSerialized]
    public const string DisTransferInNo = "转入单号";

    /// <summary>
    /// 转出类型
    /// </summary>
    [NonSerialized]
    public const string DisTransferOutType = "转出类型";

    /// <summary>
    /// 转出单号
    /// </summary>
    [NonSerialized]
    public const string DisTransferOutNo = "转出单号";

    /// <summary>
    /// 发货客户编号
    /// </summary>
    [NonSerialized]
    public const string DisTranCustomer = "发货客户编号";

    /// <summary>
    /// 订单类型
    /// </summary>
    [NonSerialized]
    public const string DisOrderType = "订单类型";

    /// <summary>
    /// 订单号
    /// </summary>
    [NonSerialized]
    public const string DisOrderNo = "订单号";

    /// <summary>
    /// 起始网点编号
    /// </summary>
    [NonSerialized]
    public const string DisFromNode = "起始网点编号";

    /// <summary>
    /// 起始区域编号
    /// </summary>
    [NonSerialized]
    public const string DisFromNodes = "起始区域编号";

    /// <summary>
    /// 发货省
    /// </summary>
    [NonSerialized]
    public const string DisFromProvinceId = "发货省";

    /// <summary>
    /// 发货市
    /// </summary>
    [NonSerialized]
    public const string DisFromCityId = "发货市";

    /// <summary>
    /// 发货区
    /// </summary>
    [NonSerialized]
    public const string DisFromAreaId = "发货区";

    /// <summary>
    /// 目的网点编号
    /// </summary>
    [NonSerialized]
    public const string DisToNode = "目的网点编号";

    /// <summary>
    /// 目的区域编号
    /// </summary>
    [NonSerialized]
    public const string DisToNodes = "目的区域编号";

    /// <summary>
    /// 目的省
    /// </summary>
    [NonSerialized]
    public const string DisToProvinceId = "目的省";

    /// <summary>
    /// 目的市
    /// </summary>
    [NonSerialized]
    public const string DisToCityId = "目的市";

    /// <summary>
    /// 目的区
    /// </summary>
    [NonSerialized]
    public const string DisToAreaId = "目的区";

    /// <summary>
    /// 发货公司
    /// </summary>
    [NonSerialized]
    public const string DisShipperCompanyName = "发货公司";

    /// <summary>
    /// 发货地址
    /// </summary>
    [NonSerialized]
    public const string DisShipperAddress = "发货地址";

    /// <summary>
    /// 发货电话
    /// </summary>
    [NonSerialized]
    public const string DisShipperTel = "发货电话";

    /// <summary>
    /// 发货人
    /// </summary>
    [NonSerialized]
    public const string DisShipper = "发货人";

    /// <summary>
    /// 收货公司
    /// </summary>
    [NonSerialized]
    public const string DisConsigneeCompanyName = "收货公司";

    /// <summary>
    /// 收货地址
    /// </summary>
    [NonSerialized]
    public const string DisConsigneeAddress = "收货地址";

    /// <summary>
    /// 收货电话
    /// </summary>
    [NonSerialized]
    public const string DisConsigneeTel = "收货电话";

    /// <summary>
    /// 收货人
    /// </summary>
    [NonSerialized]
    public const string DisConsignee = "收货人";

    /// <summary>
    /// 运输方式
    /// </summary>
    [NonSerialized]
    public const string DisTransportType = "运输方式";

    /// <summary>
    /// 提货方式
    /// </summary>
    [NonSerialized]
    public const string DisPickUpType = "提货方式";

    /// <summary>
    /// 交货方式
    /// </summary>
    [NonSerialized]
    public const string DisDeliveryType = "交货方式";

    /// <summary>
    /// 付款方式
    /// </summary>
    [NonSerialized]
    public const string DisPaymentType = "付款方式";

    /// <summary>
    /// 等通知放货
    /// </summary>
    [NonSerialized]
    public const string DisWaitNoticeYN = "等通知放货";

    /// <summary>
    /// 通知人员
    /// </summary>
    [NonSerialized]
    public const string DisNoticeUser = "通知人员";

    /// <summary>
    /// 通知日期
    /// </summary>
    [NonSerialized]
    public const string DisNoticeDate = "通知日期";

    /// <summary>
    /// 通知备注
    /// </summary>
    [NonSerialized]
    public const string DisNoticeRemark = "通知备注";

    /// <summary>
    /// 回单数量
    /// </summary>
    [NonSerialized]
    public const string DisAckRecQty = "回单数量";

    /// <summary>
    /// 回单类型
    /// </summary>
    [NonSerialized]
    public const string DisAckRecType = "回单类型";

    /// <summary>
    /// 回单号
    /// </summary>
    [NonSerialized]
    public const string DisAckRecNo = "回单号";

    /// <summary>
    /// 异形件
    /// </summary>
    [NonSerialized]
    public const string DisAbnormityType = "异形件";

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
    /// 数量
    /// </summary>
    [NonSerialized]
    public const string DisQty = "数量";

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
    /// 计费重量
    /// </summary>
    [NonSerialized]
    public const string DisChargeableWeight = "计费重量";

    /// <summary>
    /// 代收款
    /// </summary>
    [NonSerialized]
    public const string DisAgencyReceiveCharge = "代收款";

    /// <summary>
    /// 现付金额
    /// </summary>
    [NonSerialized]
    public const string DisCarriagePrepaid = "现付金额";

    /// <summary>
    /// 到付金额
    /// </summary>
    [NonSerialized]
    public const string DisCarriageForward = "到付金额";

    /// <summary>
    /// 月结金额
    /// </summary>
    [NonSerialized]
    public const string DisCarriageMonthly = "月结金额";

    /// <summary>
    /// 回付金额
    /// </summary>
    [NonSerialized]
    public const string DisCarriageReceipt = "回付金额";

    /// <summary>
    /// 欠付金额
    /// </summary>
    [NonSerialized]
    public const string DisCarriageOwed = "欠付金额";

    /// <summary>
    /// 其他金额
    /// </summary>
    [NonSerialized]
    public const string DisCarriageOther = "其他金额";

    /// <summary>
    /// 回扣金额
    /// </summary>
    [NonSerialized]
    public const string DisBrokerage = "回扣金额";

    /// <summary>
    /// 保价金额
    /// </summary>
    [NonSerialized]
    public const string DisAmountInsured = "保价金额";

    /// <summary>
    /// 状态网点
    /// </summary>
    [NonSerialized]
    public const string DisStatusNode = "状态网点";

    /// <summary>
    /// 运单状态
    /// </summary>
    [NonSerialized]
    public const string DisStatusId = "运单状态";

    /// <summary>
    /// 卸货
    /// </summary>
    [NonSerialized]
    public const string DisUnloadYN = "卸货";

    /// <summary>
    /// 上楼
    /// </summary>
    [NonSerialized]
    public const string DisUpstairYN = "上楼";

    /// <summary>
    /// 上楼层数
    /// </summary>
    [NonSerialized]
    public const string DisUpstairNum = "上楼层数";

    /// <summary>
    /// 业务员
    /// </summary>
    [NonSerialized]
    public const string DisSalesMan = "业务员";

    /// <summary>
    /// 备注
    /// </summary>
    [NonSerialized]
    public const string DisRemark = "备注";

    /// <summary>
    /// 取消
    /// </summary>
    [NonSerialized]
    public const string DisCloseYN = "取消";

    /// <summary>
    /// 取消人
    /// </summary>
    [NonSerialized]
    public const string DisCloseUser = "取消人";

    /// <summary>
    /// 取消日期
    /// </summary>
    [NonSerialized]
    public const string DisCloseDate = "取消日期";

    /// <summary>
    /// 取消时间
    /// </summary>
    [NonSerialized]
    public const string DisCloseTime = "取消时间";

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
    /// 审核
    /// </summary>
    [NonSerialized]
    public const string DisFlagApp = "审核";

    /// <summary>
    /// 审核人
    /// </summary>
    [NonSerialized]
    public const string DisAppUser = "审核人";

    /// <summary>
    /// 审核时间
    /// </summary>
    [NonSerialized]
    public const string DisAppDate = "审核时间";

    /// <summary>
    /// 自动单号
    /// </summary>
    [NonSerialized]
    public const string DisAutoNoYN = "自动单号";

    /// <summary>
    /// 回单撤销状态
    /// </summary>
    [NonSerialized]
    public const string DisAckRecCancelYN = "回单撤销状态";

    /// <summary>
    /// 自动回单
    /// </summary>
    [NonSerialized]
    public const string DisAutoReceipt = "自动回单";

    #endregion

    #endregion
}