using System.Runtime.Serialization;
using BB.Entity.Base;

namespace BB.Entity.TMS;

/// <summary>
/// 预付金管理 实体类
/// </summary>
[DataContract]
[Serializable]
[Table(DBTableName)]
public sealed class CostBill : BaseEntity
{
    /// <summary>
    /// 默认构造函数（需要初始化属性的在此处理，复杂的属性值初始化通过重写 _bll.NewEntity 方法实现）
    /// </summary>
    public CostBill()
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
    /// 预付单类型
    /// </summary>
    [DataMember]
    [Column(FieldCostBillType, DisCostBillType)]
    public string CostBillType { get; set; }

    /// <summary>
    /// 预付单编号
    /// </summary>
    [DataMember]
    [Key]
    [Column(FieldCostBillNo, DisCostBillNo)]
    public string CostBillNo { get; set; }

    /// <summary>
    /// 运单编号
    /// </summary>
    [DataMember]
    [Column(FieldWaybillNo, DisWaybillNo)]
    public string WaybillNo { get; set; }

    /// <summary>
    /// 收款网点
    /// </summary>
    [DataMember]
    [Column(FieldTranNodeNO, DisTranNodeNO)]
    public string TranNodeNO { get; set; }

    /// <summary>
    /// 付款网点
    /// </summary>
    [DataMember]
    [Column(FieldTranNodeNOPay, DisTranNodeNOPay)]
    public string TranNodeNOPay { get; set; }

    /// <summary>
    /// 来源单号
    /// </summary>
    [DataMember]
    [Column(FieldSourceNo, DisSourceNo)]
    public string SourceNo { get; set; }

    /// <summary>
    /// 操作类型
    /// </summary>
    [DataMember]
    [Column(FieldCostType, DisCostType)]
    public string CostType { get; set; }

    /// <summary>
    /// 正负
    /// </summary>
    [DataMember]
    [Column(FieldCtrl, DisCtrl)]
    public short Ctrl { get; set; }

    /// <summary>
    /// 金额
    /// </summary>
    [DataMember]
    [Column(FieldCost, DisCost)]
    public decimal Cost { get; set; }

    /// <summary>
    /// 币别
    /// </summary>
    [DataMember]
    [Hide]
    [Column(FieldCurrency, DisCurrency)]
    public string Currency { get; set; }

    /// <summary>
    /// 入账
    /// </summary>
    [DataMember]
    [Column(FieldPostYN, DisPostYN)]
    public bool PostYN { get; set; }

    /// <summary>
    /// 入账时间
    /// </summary>
    [DataMember]
    [Column(FieldPostDate, DisPostDate)]
    public DateTime? PostDate { get; set; }

    /// <summary>
    /// 入账人
    /// </summary>
    [DataMember]
    [Column(FieldPostBy, DisPostBy)]
    public string PostBy { get; set; }

    /// <summary>
    /// 单据状态
    /// </summary>
    [DataMember]
    [Column(FieldStatusID, DisStatusID)]
    public string StatusID { get; set; }

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
    /// 创建网点
    /// </summary>
    [DataMember]
    [Column(FieldCreatedByNode, DisCreatedByNode)]
    public string CreatedByNode { get; set; }

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
    /// 附件
    /// </summary>
    [DataMember]
    [Hide]
    [Column(FieldAttaPath, DisAttaPath)]
    public string AttaPath { get; set; }

    /// <summary>
    /// 财务中心
    /// </summary>
    [DataMember]
    [Column(FieldFinancialCenter, DisFinancialCenter)]
    public string FinancialCenter { get; set; }

    #endregion

    #region 定义字段名称相关常量

    /// <summary>
    /// 表名
    /// </summary>
    [NonSerialized]
    public new const string DBTableName = "tb_TranCostBill";

    /// <summary>
    /// 主键名
    /// </summary>
    [NonSerialized]
    public new const string PrimaryKey = FieldCostBillNo;

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
    /// 预付单类型
    /// </summary>
    [NonSerialized]
    public const string FieldCostBillType = "CostBillType";

    /// <summary>
    /// 预付单编号
    /// </summary>
    [NonSerialized]
    public const string FieldCostBillNo = "CostBillNo";

    /// <summary>
    /// 运单编号
    /// </summary>
    [NonSerialized]
    public const string FieldWaybillNo = "WaybillNo";

    /// <summary>
    /// 收款网点
    /// </summary>
    [NonSerialized]
    public const string FieldTranNodeNO = "TranNodeNO";

    /// <summary>
    /// 付款网点
    /// </summary>
    [NonSerialized]
    public const string FieldTranNodeNOPay = "TranNodeNOPay";

    /// <summary>
    /// 来源单号
    /// </summary>
    [NonSerialized]
    public const string FieldSourceNo = "SourceNo";

    /// <summary>
    /// 费用类型
    /// </summary>
    [NonSerialized]
    public const string FieldCostType = "CostType";

    /// <summary>
    /// 正负
    /// </summary>
    [NonSerialized]
    public const string FieldCtrl = "Ctrl";

    /// <summary>
    /// 金额
    /// </summary>
    [NonSerialized]
    public const string FieldCost = "Cost";

    /// <summary>
    /// 币别
    /// </summary>
    [NonSerialized]
    public const string FieldCurrency = "Currency";

    /// <summary>
    /// 入账
    /// </summary>
    [NonSerialized]
    public const string FieldPostYN = "PostYN";

    /// <summary>
    /// 入账时间
    /// </summary>
    [NonSerialized]
    public const string FieldPostDate = "PostDate";

    /// <summary>
    /// 入账人
    /// </summary>
    [NonSerialized]
    public const string FieldPostBy = "PostBy";

    /// <summary>
    /// 单据状态
    /// </summary>
    [NonSerialized]
    public const string FieldStatusID = "StatusID";

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
    /// 创建网点
    /// </summary>
    [NonSerialized]
    public const string FieldCreatedByNode = "CreatedByNode";

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
    /// 附件
    /// </summary>
    [NonSerialized]
    public const string FieldAttaPath = "AttaPath";

    /// <summary>
    /// 财务中心
    /// </summary>
    [NonSerialized]
    public const string FieldFinancialCenter = "FinancialCenter";

    #endregion

    #region 列显示名
    /// <summary>
    /// 自增ID
    /// </summary>
    [NonSerialized]
    public const string DisISID = "自增ID";

    /// <summary>
    /// 预付单类型
    /// </summary>
    [NonSerialized]
    public const string DisCostBillType = "预付单类型";

    /// <summary>
    /// 预付单编号
    /// </summary>
    [NonSerialized]
    public const string DisCostBillNo = "预付单编号";

    /// <summary>
    /// 运单编号
    /// </summary>
    [NonSerialized]
    public const string DisWaybillNo = "运单编号";

    /// <summary>
    /// 收款网点
    /// </summary>
    [NonSerialized]
    public const string DisTranNodeNO = "收款网点";

    /// <summary>
    /// 付款网点
    /// </summary>
    [NonSerialized]
    public const string DisTranNodeNOPay = "付款网点";

    /// <summary>
    /// 来源单号
    /// </summary>
    [NonSerialized]
    public const string DisSourceNo = "来源单号";

    /// <summary>
    /// 费用类型
    /// </summary>
    [NonSerialized]
    public const string DisCostType = "费用类型";

    /// <summary>
    /// 正负
    /// </summary>
    [NonSerialized]
    public const string DisCtrl = "正负";

    /// <summary>
    /// 金额
    /// </summary>
    [NonSerialized]
    public const string DisCost = "金额";

    /// <summary>
    /// 币别
    /// </summary>
    [NonSerialized]
    public const string DisCurrency = "币别";

    /// <summary>
    /// 入账
    /// </summary>
    [NonSerialized]
    public const string DisPostYN = "入账";

    /// <summary>
    /// 入账时间
    /// </summary>
    [NonSerialized]
    public const string DisPostDate = "入账时间";

    /// <summary>
    /// 入账人
    /// </summary>
    [NonSerialized]
    public const string DisPostBy = "入账人";

    /// <summary>
    /// 单据状态
    /// </summary>
    [NonSerialized]
    public const string DisStatusID = "单据状态";

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
    /// 创建网点
    /// </summary>
    [NonSerialized]
    public const string DisCreatedByNode = "创建网点";

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
    /// 附件
    /// </summary>
    [NonSerialized]
    public const string DisAttaPath = "附件";

    /// <summary>
    /// 财务中心
    /// </summary>
    [NonSerialized]
    public const string DisFinancialCenter = "财务中心";

    #endregion

    #endregion
}