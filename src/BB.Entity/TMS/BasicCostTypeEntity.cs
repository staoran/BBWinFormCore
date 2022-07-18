using System.Runtime.Serialization;
using BB.Entity.Base;

namespace BB.Entity.TMS;

/// <summary>
/// 费用类型 实体类
/// </summary>
[DataContract]
[Serializable]
[Table(DBTableName)]
public sealed class BasicCostType : BaseEntity
{
    /// <summary>
    /// 默认构造函数（需要初始化属性的在此处理，复杂的属性值初始化通过重写 _bll.NewEntity 方法实现）
    /// </summary>
    public BasicCostType()
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
    /// 费用类型编号
    /// </summary>
    [DataMember]
    [Key]
    [Column(FieldCostType, DisCostType)]
    public string CostType { get; set; }

    /// <summary>
    /// 费用类型
    /// </summary>
    [DataMember]
    [Column(FieldCostTypeDesc, DisCostTypeDesc)]
    public string CostTypeDesc { get; set; }

    /// <summary>
    /// 正负
    /// </summary>
    [DataMember]
    [Column(FieldCtrl, DisCtrl)]
    public short Ctrl { get; set; }

    /// <summary>
    /// 启用
    /// </summary>
    [DataMember]
    [Column(FieldUseYN, DisUseYN)]
    public bool UseYN { get; set; }

    /// <summary>
    /// 适用范围
    /// </summary>
    [DataMember]
    [Column(FieldUseType, DisUseType)]
    public string UseType { get; set; }

    /// <summary>
    /// 付款网点类型
    /// </summary>
    [DataMember]
    [Column(FieldPayNodeType, DisPayNodeType)]
    public string PayNodeType { get; set; }

    /// <summary>
    /// 收款网点类型
    /// </summary>
    [DataMember]
    [Column(FieldRecvNodeType, DisRecvNodeType)]
    public string RecvNodeType { get; set; }

    /// <summary>
    /// 付款入账类型
    /// </summary>
    [DataMember]
    [Column(FieldPayPostType, DisPayPostType)]
    public string PayPostType { get; set; }

    /// <summary>
    /// 收款入账类型
    /// </summary>
    [DataMember]
    [Column(FieldRecvPostType, DisRecvPostType)]
    public string RecvPostType { get; set; }

    /// <summary>
    /// 收入费用
    /// </summary>
    [DataMember]
    [Column(FieldCostYN, DisCostYN)]
    public bool CostYN { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [DataMember]
    [Column(FieldRemark, DisRemark)]
    public string Remark { get; set; }

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
    /// 创建人
    /// </summary>
    [DataMember]
    [Column(FieldCreatedBy, DisCreatedBy)]
    public string CreatedBy { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    [DataMember]
    [Column(FieldCreationDate, DisCreationDate)]
    public DateTime CreationDate { get; set; }

    /// <summary>
    /// 修改人
    /// </summary>
    [DataMember]
    [Column(FieldLastUpdatedBy, DisLastUpdatedBy)]
    public string LastUpdatedBy { get; set; }

    /// <summary>
    /// 修改时间
    /// </summary>
    [DataMember]
    [OptimisticLock]
    [Column(FieldLastUpdateDate, DisLastUpdateDate)]
    public DateTime LastUpdateDate { get; set; }

    #endregion

    #region 定义字段名称相关常量

    /// <summary>
    /// 表名
    /// </summary>
    [NonSerialized]
    public new const string DBTableName = "tb_BasicCostType";

    /// <summary>
    /// 主键名
    /// </summary>
    [NonSerialized]
    public new const string PrimaryKey = FieldCostType;

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
    /// 费用类型编号
    /// </summary>
    [NonSerialized]
    public const string FieldCostType = "CostType";

    /// <summary>
    /// 费用类型
    /// </summary>
    [NonSerialized]
    public const string FieldCostTypeDesc = "CostTypeDesc";

    /// <summary>
    /// 正负
    /// </summary>
    [NonSerialized]
    public const string FieldCtrl = "Ctrl";

    /// <summary>
    /// 启用
    /// </summary>
    [NonSerialized]
    public const string FieldUseYN = "UseYN";

    /// <summary>
    /// 适用范围
    /// </summary>
    [NonSerialized]
    public const string FieldUseType = "UseType";

    /// <summary>
    /// 付款网点类型
    /// </summary>
    [NonSerialized]
    public const string FieldPayNodeType = "PayNodeType";

    /// <summary>
    /// 收款网点类型
    /// </summary>
    [NonSerialized]
    public const string FieldRecvNodeType = "RecvNodeType";

    /// <summary>
    /// 付款入账类型
    /// </summary>
    [NonSerialized]
    public const string FieldPayPostType = "PayPostType";

    /// <summary>
    /// 收款入账类型
    /// </summary>
    [NonSerialized]
    public const string FieldRecvPostType = "RecvPostType";

    /// <summary>
    /// 收入费用
    /// </summary>
    [NonSerialized]
    public const string FieldCostYN = "CostYN";

    /// <summary>
    /// 备注
    /// </summary>
    [NonSerialized]
    public const string FieldRemark = "Remark";

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
    /// 创建人
    /// </summary>
    [NonSerialized]
    public const string FieldCreatedBy = "CreatedBy";

    /// <summary>
    /// 创建时间
    /// </summary>
    [NonSerialized]
    public const string FieldCreationDate = "CreationDate";

    /// <summary>
    /// 修改人
    /// </summary>
    [NonSerialized]
    public const string FieldLastUpdatedBy = "LastUpdatedBy";

    /// <summary>
    /// 修改时间
    /// </summary>
    [NonSerialized]
    public const string FieldLastUpdateDate = "LastUpdateDate";

    #endregion

    #region 列显示名
    /// <summary>
    /// 自增ID
    /// </summary>
    [NonSerialized]
    public const string DisISID = "自增ID";

    /// <summary>
    /// 费用类型编号
    /// </summary>
    [NonSerialized]
    public const string DisCostType = "费用类型编号";

    /// <summary>
    /// 费用类型
    /// </summary>
    [NonSerialized]
    public const string DisCostTypeDesc = "费用类型";

    /// <summary>
    /// 正负
    /// </summary>
    [NonSerialized]
    public const string DisCtrl = "正负";

    /// <summary>
    /// 启用
    /// </summary>
    [NonSerialized]
    public const string DisUseYN = "启用";

    /// <summary>
    /// 适用范围
    /// </summary>
    [NonSerialized]
    public const string DisUseType = "适用范围";

    /// <summary>
    /// 付款网点类型
    /// </summary>
    [NonSerialized]
    public const string DisPayNodeType = "付款网点类型";

    /// <summary>
    /// 收款网点类型
    /// </summary>
    [NonSerialized]
    public const string DisRecvNodeType = "收款网点类型";

    /// <summary>
    /// 付款入账类型
    /// </summary>
    [NonSerialized]
    public const string DisPayPostType = "付款入账类型";

    /// <summary>
    /// 收款入账类型
    /// </summary>
    [NonSerialized]
    public const string DisRecvPostType = "收款入账类型";

    /// <summary>
    /// 收入费用
    /// </summary>
    [NonSerialized]
    public const string DisCostYN = "收入费用";

    /// <summary>
    /// 备注
    /// </summary>
    [NonSerialized]
    public const string DisRemark = "备注";

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
    /// 创建人
    /// </summary>
    [NonSerialized]
    public const string DisCreatedBy = "创建人";

    /// <summary>
    /// 创建时间
    /// </summary>
    [NonSerialized]
    public const string DisCreationDate = "创建时间";

    /// <summary>
    /// 修改人
    /// </summary>
    [NonSerialized]
    public const string DisLastUpdatedBy = "修改人";

    /// <summary>
    /// 修改时间
    /// </summary>
    [NonSerialized]
    public const string DisLastUpdateDate = "修改时间";

    #endregion

    #endregion
}