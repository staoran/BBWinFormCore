using System.Runtime.Serialization;
using BB.Entity.Base;

namespace BB.Entity.TMS;

/// <summary>
/// 公式报价 实体类
/// </summary>
[DataContract]
[Serializable]
[IsChildListNull]
[Table(DBTableName)]
public sealed class BasicQuotation : BaseEntity
{
    /// <summary>
    /// 默认构造函数（需要初始化属性的在此处理，复杂的属性值初始化通过重写 _bll.NewEntity 方法实现）
    /// </summary>
    public BasicQuotation()
    {
    }

    #region Property Members

    /// <summary>
    /// 自增ID
    /// </summary>
    [DataMember]
    [Identity]
    [Hide]
    [Sort(IsDesc)]
    [Column(FieldISID, DisISID)]
    public int ISID { get; set; }

    /// <summary>
    /// 报价编号
    /// </summary>
    [DataMember]
    [Key]
    [Column(FieldQuotationNo, DisQuotationNo)]
    public string QuotationNo { get; set; }

    /// <summary>
    /// 报价名称
    /// </summary>
    [DataMember]
    [Column(FieldQuotationDesc, DisQuotationDesc)]
    public string QuotationDesc { get; set; }

    /// <summary>
    /// 所属网点
    /// </summary>
    [DataMember]
    [Column(FieldTranNodeNO, DisTranNodeNO)]
    public string TranNodeNO { get; set; }

    /// <summary>
    /// 费用类型
    /// </summary>
    [DataMember]
    [Column(FieldCostType, DisCostType)]
    public string CostType { get; set; }

    /// <summary>
    /// 货物类型
    /// </summary>
    [DataMember]
    [Column(FieldCargoType, DisCargoType)]
    public string CargoType { get; set; }

    /// <summary>
    /// 收货方式
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
    /// 运输方式
    /// </summary>
    [DataMember]
    [Column(FieldTransportType, DisTransportType)]
    public string TransportType { get; set; }

    /// <summary>
    /// 起始区域
    /// </summary>
    [DataMember]
    [Column(FieldFroms, DisFroms)]
    public string Froms { get; set; }

    /// <summary>
    /// 目的区域
    /// </summary>
    [DataMember]
    [Column(FieldTos, DisTos)]
    public string Tos { get; set; }

    /// <summary>
    /// 生效时间
    /// </summary>
    [DataMember]
    [Column(FieldBeginTime, DisBeginTime)]
    public DateTime BeginTime { get; set; }

    /// <summary>
    /// 过期时间
    /// </summary>
    [DataMember]
    [Column(FieldEndTime, DisEndTime)]
    public DateTime EndTime { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [DataMember]
    [Column(FieldRemark, DisRemark)]
    public string Remark { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    [DataMember]
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
    /// 审批
    /// </summary>
    [DataMember]
    [Column(FieldFlagApp, DisFlagApp)]
    public bool FlagApp { get; set; }

    /// <summary>
    /// 审批人
    /// </summary>
    [DataMember]
    [Column(FieldAppUser, DisAppUser)]
    public string AppUser { get; set; }

    /// <summary>
    /// 审批时间
    /// </summary>
    [DataMember]
    [Column(FieldAppDate, DisAppDate)]
    public DateTime? AppDate { get; set; }

    /// <summary>
    /// 仅用于抽成
    /// </summary>
    [DataMember]
    [Column(FieldRakeMarkYN, DisRakeMarkYN)]
    public bool RakeMarkYN { get; set; }

    /// <summary>
    /// 公式报价明细 集合
    /// </summary>
    [Ignore]
    public IEnumerable<BasicQuotations> BasicQuotationsList { get; set; }

    #endregion

    #region 定义字段名称相关常量

    /// <summary>
    /// 表名
    /// </summary>
    [NonSerialized]
    public new const string DBTableName = "tb_BasicQuotation";

    /// <summary>
    /// 主键名
    /// </summary>
    [NonSerialized]
    public new const string PrimaryKey = FieldQuotationNo;

    /// <summary>
    /// 排序字段
    /// </summary>
    [NonSerialized]
    public new const string SortKey = FieldISID;

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
    /// 报价编号
    /// </summary>
    [NonSerialized]
    public const string FieldQuotationNo = "QuotationNo";

    /// <summary>
    /// 报价名称
    /// </summary>
    [NonSerialized]
    public const string FieldQuotationDesc = "QuotationDesc";

    /// <summary>
    /// 所属网点
    /// </summary>
    [NonSerialized]
    public const string FieldTranNodeNO = "TranNodeNO";

    /// <summary>
    /// 费用类型
    /// </summary>
    [NonSerialized]
    public const string FieldCostType = "CostType";

    /// <summary>
    /// 货物类型
    /// </summary>
    [NonSerialized]
    public const string FieldCargoType = "CargoType";

    /// <summary>
    /// 收货方式
    /// </summary>
    [NonSerialized]
    public const string FieldPickUpType = "PickUpType";

    /// <summary>
    /// 交货方式
    /// </summary>
    [NonSerialized]
    public const string FieldDeliveryType = "DeliveryType";

    /// <summary>
    /// 运输方式
    /// </summary>
    [NonSerialized]
    public const string FieldTransportType = "TransportType";

    /// <summary>
    /// 起始区域
    /// </summary>
    [NonSerialized]
    public const string FieldFroms = "Froms";

    /// <summary>
    /// 目的区域
    /// </summary>
    [NonSerialized]
    public const string FieldTos = "Tos";

    /// <summary>
    /// 生效时间
    /// </summary>
    [NonSerialized]
    public const string FieldBeginTime = "BeginTime";

    /// <summary>
    /// 过期时间
    /// </summary>
    [NonSerialized]
    public const string FieldEndTime = "EndTime";

    /// <summary>
    /// 备注
    /// </summary>
    [NonSerialized]
    public const string FieldRemark = "Remark";

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
    /// 审批
    /// </summary>
    [NonSerialized]
    public const string FieldFlagApp = "FlagApp";

    /// <summary>
    /// 审批人
    /// </summary>
    [NonSerialized]
    public const string FieldAppUser = "AppUser";

    /// <summary>
    /// 审批时间
    /// </summary>
    [NonSerialized]
    public const string FieldAppDate = "AppDate";

    /// <summary>
    /// 仅用于抽成
    /// </summary>
    [NonSerialized]
    public const string FieldRakeMarkYN = "RakeMarkYN";

    #endregion

    #region 列显示名
    /// <summary>
    /// 自增ID
    /// </summary>
    [NonSerialized]
    public const string DisISID = "自增ID";

    /// <summary>
    /// 报价编号
    /// </summary>
    [NonSerialized]
    public const string DisQuotationNo = "报价编号";

    /// <summary>
    /// 报价名称
    /// </summary>
    [NonSerialized]
    public const string DisQuotationDesc = "报价名称";

    /// <summary>
    /// 所属网点
    /// </summary>
    [NonSerialized]
    public const string DisTranNodeNO = "所属网点";

    /// <summary>
    /// 费用类型
    /// </summary>
    [NonSerialized]
    public const string DisCostType = "费用类型";

    /// <summary>
    /// 货物类型
    /// </summary>
    [NonSerialized]
    public const string DisCargoType = "货物类型";

    /// <summary>
    /// 收货方式
    /// </summary>
    [NonSerialized]
    public const string DisPickUpType = "收货方式";

    /// <summary>
    /// 交货方式
    /// </summary>
    [NonSerialized]
    public const string DisDeliveryType = "交货方式";

    /// <summary>
    /// 运输方式
    /// </summary>
    [NonSerialized]
    public const string DisTransportType = "运输方式";

    /// <summary>
    /// 起始区域
    /// </summary>
    [NonSerialized]
    public const string DisFroms = "起始区域";

    /// <summary>
    /// 目的区域
    /// </summary>
    [NonSerialized]
    public const string DisTos = "目的区域";

    /// <summary>
    /// 生效时间
    /// </summary>
    [NonSerialized]
    public const string DisBeginTime = "生效时间";

    /// <summary>
    /// 过期时间
    /// </summary>
    [NonSerialized]
    public const string DisEndTime = "过期时间";

    /// <summary>
    /// 备注
    /// </summary>
    [NonSerialized]
    public const string DisRemark = "备注";

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
    /// 审批
    /// </summary>
    [NonSerialized]
    public const string DisFlagApp = "审批";

    /// <summary>
    /// 审批人
    /// </summary>
    [NonSerialized]
    public const string DisAppUser = "审批人";

    /// <summary>
    /// 审批时间
    /// </summary>
    [NonSerialized]
    public const string DisAppDate = "审批时间";

    /// <summary>
    /// 仅用于抽成
    /// </summary>
    [NonSerialized]
    public const string DisRakeMarkYN = "仅用于抽成";

    #endregion

    #endregion
}