using System.Runtime.Serialization;
using BB.Entity.Base;
using BB.Tools.Entity;

namespace BB.Entity.TMS;

/// <summary>
/// 预付金操作类型 实体类
/// </summary>
[DataContract]
[Serializable]
[Table(DBTableName)]
public sealed class BasicCostBillType : BaseEntity
{
    /// <summary>
    /// 默认构造函数（需要初始化属性的在此处理，复杂的属性值初始化通过关键字替换或重写 SetDynamicDefaults 方法实现）
    /// </summary>
    public BasicCostBillType()
    {
        CreatedBy = "*当前用户*";
        CreationDate = DateTime.Now;
        FlagApp = false;
        LastUpdatedBy = "*当前用户*";
        LastUpdateDate = DateTime.Now;
    }

    #region Property Members

    /// <summary>
    /// 自增ID
    /// </summary>
    [DataMember]
    [Identity]
    [Sort(IsDesc)]
    [Column(FieldISID, DisISID)]
    public int ISID { get; set; }

    /// <summary>
    /// 类型编号
    /// </summary>
    [DataMember]
    [Key]
    [Column(FieldCostType, DisCostType)]
    public string CostType { get; set; }

    /// <summary>
    /// 类型名称
    /// </summary>
    [DataMember]
    [Column(FieldCostDesc, DisCostDesc)]
    public string CostDesc { get; set; }

    /// <summary>
    /// 正负1/-1
    /// </summary>
    [DataMember]
    [Column(FieldCtrl, DisCtrl)]
    public short Ctrl { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [DataMember]
    [Column(FieldRemark, DisRemark)]
    public string Remark { get; set; }

    /// <summary>
    /// 适用范围
    /// </summary>
    [DataMember]
    [Column(FieldUseType, DisUseType)]
    public string UseType { get; set; }

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
    /// 审核
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
    public new const string DBTableName = "tb_BasicCostBillType";

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
    /// 类型编号
    /// </summary>
    [NonSerialized]
    public const string FieldCostType = "CostType";

    /// <summary>
    /// 类型名称
    /// </summary>
    [NonSerialized]
    public const string FieldCostDesc = "CostDesc";

    /// <summary>
    /// 正负1/-1
    /// </summary>
    [NonSerialized]
    public const string FieldCtrl = "Ctrl";

    /// <summary>
    /// 备注
    /// </summary>
    [NonSerialized]
    public const string FieldRemark = "Remark";

    /// <summary>
    /// 适用范围
    /// </summary>
    [NonSerialized]
    public const string FieldUseType = "UseType";

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
    /// 审核
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
    /// 类型编号
    /// </summary>
    [NonSerialized]
    public const string DisCostType = "类型编号";

    /// <summary>
    /// 类型名称
    /// </summary>
    [NonSerialized]
    public const string DisCostDesc = "类型名称";

    /// <summary>
    /// 正负1/-1
    /// </summary>
    [NonSerialized]
    public const string DisCtrl = "正负1/-1";

    /// <summary>
    /// 备注
    /// </summary>
    [NonSerialized]
    public const string DisRemark = "备注";

    /// <summary>
    /// 适用范围
    /// </summary>
    [NonSerialized]
    public const string DisUseType = "适用范围";

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
    /// 审核
    /// </summary>
    [NonSerialized]
    public const string DisFlagApp = "审核";

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