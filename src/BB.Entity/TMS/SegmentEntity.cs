using System.Runtime.Serialization;
using BB.Entity.Base;
using BB.Tools.Entity;

namespace BB.Entity.TMS;

/// <summary>
/// 线路管理 实体类
/// </summary>
[DataContract]
[Serializable]
[Table(DBTableName)]
public sealed class Segment : BaseEntity<Segments>
{
    /// <summary>
    /// 默认构造函数（需要初始化属性的在此处理，复杂的属性值初始化通过重写 _bll.NewEntity 方法实现）
    /// </summary>
    public Segment()
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
    /// 线路编号
    /// </summary>
    [DataMember]
    [Key]
    [Column(FieldSegmentNo, DisSegmentNo)]
    public string SegmentNo { get; set; }

    /// <summary>
    /// 线路类型
    /// </summary>
    [DataMember]
    [Column(FieldSegmentType, DisSegmentType)]
    public string SegmentType { get; set; }

    /// <summary>
    /// 线路名称
    /// </summary>
    [DataMember]
    [Column(FieldSegmentName, DisSegmentName)]
    public string SegmentName { get; set; }

    /// <summary>
    /// 起始网点
    /// </summary>
    [DataMember]
    [Column(FieldSegmentBeginNode, DisSegmentBeginNode)]
    public string SegmentBeginNode { get; set; }

    /// <summary>
    /// 结束网点
    /// </summary>
    [DataMember]
    [Column(FieldSegmentEndNode, DisSegmentEndNode)]
    public string SegmentEndNode { get; set; }

    /// <summary>
    /// 起始时间
    /// </summary>
    [DataMember]
    [Column(FieldPlanBeginTime, DisPlanBeginTime)]
    public DateTime PlanBeginTime { get; set; }

    /// <summary>
    /// 预估时间（小时）
    /// </summary>
    [DataMember]
    [Column(FieldExpectedHour, DisExpectedHour)]
    public decimal ExpectedHour { get; set; }

    /// <summary>
    /// 预估距离
    /// </summary>
    [DataMember]
    [Column(FieldExpectedDistance, DisExpectedDistance)]
    public decimal ExpectedDistance { get; set; }

    /// <summary>
    /// 预估油耗
    /// </summary>
    [DataMember]
    [Column(FieldExpectedOilWear, DisExpectedOilWear)]
    public decimal ExpectedOilWear { get; set; }

    /// <summary>
    /// 预估开支
    /// </summary>
    [DataMember]
    [Column(FieldExpectedCharge, DisExpectedCharge)]
    public decimal ExpectedCharge { get; set; }

    /// <summary>
    /// 预估路桥费
    /// </summary>
    [DataMember]
    [Column(FieldExpectedPontage, DisExpectedPontage)]
    public decimal ExpectedPontage { get; set; }

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
    /// 线路报价 集合
    /// </summary>
    [Ignore]
    public IEnumerable<Segments> SegmentsList { get; set; }

    #endregion

    #region 定义字段名称相关常量

    /// <summary>
    /// 表名
    /// </summary>
    [NonSerialized]
    public new const string DBTableName = "tb_TranSegment";

    /// <summary>
    /// 主键名
    /// </summary>
    [NonSerialized]
    public new const string PrimaryKey = FieldSegmentNo;

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
    /// 线路编号
    /// </summary>
    [NonSerialized]
    public const string FieldSegmentNo = "SegmentNo";

    /// <summary>
    /// 线路类型
    /// </summary>
    [NonSerialized]
    public const string FieldSegmentType = "SegmentType";

    /// <summary>
    /// 线路名称
    /// </summary>
    [NonSerialized]
    public const string FieldSegmentName = "SegmentName";

    /// <summary>
    /// 起始网点
    /// </summary>
    [NonSerialized]
    public const string FieldSegmentBeginNode = "SegmentBeginNode";

    /// <summary>
    /// 结束网点
    /// </summary>
    [NonSerialized]
    public const string FieldSegmentEndNode = "SegmentEndNode";

    /// <summary>
    /// 起始时间
    /// </summary>
    [NonSerialized]
    public const string FieldPlanBeginTime = "PlanBeginTime";

    /// <summary>
    /// 预估时间（小时）
    /// </summary>
    [NonSerialized]
    public const string FieldExpectedHour = "ExpectedHour";

    /// <summary>
    /// 预估距离
    /// </summary>
    [NonSerialized]
    public const string FieldExpectedDistance = "ExpectedDistance";

    /// <summary>
    /// 预估油耗
    /// </summary>
    [NonSerialized]
    public const string FieldExpectedOilWear = "ExpectedOilWear";

    /// <summary>
    /// 预估开支
    /// </summary>
    [NonSerialized]
    public const string FieldExpectedCharge = "ExpectedCharge";

    /// <summary>
    /// 预估路桥费
    /// </summary>
    [NonSerialized]
    public const string FieldExpectedPontage = "ExpectedPontage";

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

    #endregion

    #region 列显示名
    /// <summary>
    /// 自增ID
    /// </summary>
    [NonSerialized]
    public const string DisISID = "自增ID";

    /// <summary>
    /// 线路编号
    /// </summary>
    [NonSerialized]
    public const string DisSegmentNo = "线路编号";

    /// <summary>
    /// 线路类型
    /// </summary>
    [NonSerialized]
    public const string DisSegmentType = "线路类型";

    /// <summary>
    /// 线路名称
    /// </summary>
    [NonSerialized]
    public const string DisSegmentName = "线路名称";

    /// <summary>
    /// 起始网点
    /// </summary>
    [NonSerialized]
    public const string DisSegmentBeginNode = "起始网点";

    /// <summary>
    /// 结束网点
    /// </summary>
    [NonSerialized]
    public const string DisSegmentEndNode = "结束网点";

    /// <summary>
    /// 起始时间
    /// </summary>
    [NonSerialized]
    public const string DisPlanBeginTime = "起始时间";

    /// <summary>
    /// 预估时间（小时）
    /// </summary>
    [NonSerialized]
    public const string DisExpectedHour = "预估时间（小时）";

    /// <summary>
    /// 预估距离
    /// </summary>
    [NonSerialized]
    public const string DisExpectedDistance = "预估距离";

    /// <summary>
    /// 预估油耗
    /// </summary>
    [NonSerialized]
    public const string DisExpectedOilWear = "预估油耗";

    /// <summary>
    /// 预估开支
    /// </summary>
    [NonSerialized]
    public const string DisExpectedCharge = "预估开支";

    /// <summary>
    /// 预估路桥费
    /// </summary>
    [NonSerialized]
    public const string DisExpectedPontage = "预估路桥费";

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

    #endregion

    #endregion
}