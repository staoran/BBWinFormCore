using System.Runtime.Serialization;
using BB.Entity.Base;
using BB.Tools.Entity;

namespace BB.Entity.TMS;

/// <summary>
/// 运单线路表 实体类
/// </summary>
[DataContract]
[Serializable]
[Table(DBTableName)]
public sealed class WaybillSegment : BaseEntity
{
    /// <summary>
    /// 默认构造函数（需要初始化属性的在此处理，复杂的属性值初始化通过重写 _bll.NewEntity 方法实现）
    /// </summary>
    public WaybillSegment()
    {
        SegmentBeginYN = false;
        SegmentEndYN = false;
        CreationDate = DateTime.Now;
        CreatedBy = "*当前用户*";
        LastUpdateDate = DateTime.Now;
        LastUpdatedBy = "*当前用户*";
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
    /// 运单编号
    /// </summary>
    [DataMember]
    [Column(FieldWaybillNo, DisWaybillNo)]
    public string WaybillNo { get; set; }

    /// <summary>
    /// 线路编号
    /// </summary>
    [DataMember]
    [Column(FieldSegmentNo, DisSegmentNo)]
    public string SegmentNo { get; set; }

    /// <summary>
    /// 车标号
    /// </summary>
    [DataMember]
    [Column(FieldCarmarkNo, DisCarmarkNo)]
    public string CarmarkNo { get; set; }

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
    /// 线路起点
    /// </summary>
    [DataMember]
    [Column(FieldSegmentBeginNode, DisSegmentBeginNode)]
    public string SegmentBeginNode { get; set; }

    /// <summary>
    /// 线路终点
    /// </summary>
    [DataMember]
    [Column(FieldSegmentEndNode, DisSegmentEndNode)]
    public string SegmentEndNode { get; set; }

    /// <summary>
    /// 预估发车时间
    /// </summary>
    [DataMember]
    [Column(FieldExpectedTime, DisExpectedTime)]
    public DateTime? ExpectedTime { get; set; }

    /// <summary>
    /// 预估时间
    /// </summary>
    [DataMember]
    [Column(FieldExpectedHour, DisExpectedHour)]
    public int? ExpectedHour { get; set; }

    /// <summary>
    /// 预估距离
    /// </summary>
    [DataMember]
    [Column(FieldExpectedDistance, DisExpectedDistance)]
    public decimal? ExpectedDistance { get; set; }

    /// <summary>
    /// 预估油耗
    /// </summary>
    [DataMember]
    [Column(FieldExpectedOilWear, DisExpectedOilWear)]
    public decimal? ExpectedOilWear { get; set; }

    /// <summary>
    /// 预估成本
    /// </summary>
    [DataMember]
    [Column(FieldExpectedCharge, DisExpectedCharge)]
    public decimal? ExpectedCharge { get; set; }

    /// <summary>
    /// 预估路桥费
    /// </summary>
    [DataMember]
    [Column(FieldExpectedPontage, DisExpectedPontage)]
    public decimal? ExpectedPontage { get; set; }

    /// <summary>
    /// 起始登记
    /// </summary>
    [DataMember]
    [Column(FieldSegmentBeginYN, DisSegmentBeginYN)]
    public bool SegmentBeginYN { get; set; }

    /// <summary>
    /// 起始登记人
    /// </summary>
    [DataMember]
    [Column(FieldSegmentBeginUser, DisSegmentBeginUser)]
    public string SegmentBeginUser { get; set; }

    /// <summary>
    /// 起始登记时间
    /// </summary>
    [DataMember]
    [Column(FieldSegmentBeginDate, DisSegmentBeginDate)]
    public DateTime? SegmentBeginDate { get; set; }

    /// <summary>
    /// 起始登记备注
    /// </summary>
    [DataMember]
    [Column(FieldSegmentBeginRemark, DisSegmentBeginRemark)]
    public string SegmentBeginRemark { get; set; }

    /// <summary>
    /// 到达登记
    /// </summary>
    [DataMember]
    [Column(FieldSegmentEndYN, DisSegmentEndYN)]
    public bool SegmentEndYN { get; set; }

    /// <summary>
    /// 到达登记人
    /// </summary>
    [DataMember]
    [Column(FieldSegmentEndUser, DisSegmentEndUser)]
    public string SegmentEndUser { get; set; }

    /// <summary>
    /// 到达登记时间
    /// </summary>
    [DataMember]
    [Column(FieldSegmentEndDate, DisSegmentEndDate)]
    public DateTime? SegmentEndDate { get; set; }

    /// <summary>
    /// 到达登记备注
    /// </summary>
    [DataMember]
    [Column(FieldSegmentEndRemark, DisSegmentEndRemark)]
    public string SegmentEndRemark { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    [DataMember]
    [Column(FieldStatusId, DisStatusId)]
    public string StatusId { get; set; }

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

    #endregion

    #region 定义字段名称相关常量

    /// <summary>
    /// 表名
    /// </summary>
    [NonSerialized]
    public new const string DBTableName = "tb_TranWaybillSegment";

    /// <summary>
    /// 主键名
    /// </summary>
    [NonSerialized]
    public new const string PrimaryKey = FieldISID;

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
    /// 运单编号
    /// </summary>
    [NonSerialized]
    public const string FieldWaybillNo = "WaybillNo";

    /// <summary>
    /// 线路编号
    /// </summary>
    [NonSerialized]
    public const string FieldSegmentNo = "SegmentNo";

    /// <summary>
    /// 车标号
    /// </summary>
    [NonSerialized]
    public const string FieldCarmarkNo = "CarmarkNo";

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
    /// 线路起点
    /// </summary>
    [NonSerialized]
    public const string FieldSegmentBeginNode = "SegmentBeginNode";

    /// <summary>
    /// 线路终点
    /// </summary>
    [NonSerialized]
    public const string FieldSegmentEndNode = "SegmentEndNode";

    /// <summary>
    /// 预估发车时间
    /// </summary>
    [NonSerialized]
    public const string FieldExpectedTime = "ExpectedTime";

    /// <summary>
    /// 预估时间
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
    /// 预估成本
    /// </summary>
    [NonSerialized]
    public const string FieldExpectedCharge = "ExpectedCharge";

    /// <summary>
    /// 预估路桥费
    /// </summary>
    [NonSerialized]
    public const string FieldExpectedPontage = "ExpectedPontage";

    /// <summary>
    /// 起始登记
    /// </summary>
    [NonSerialized]
    public const string FieldSegmentBeginYN = "SegmentBeginYN";

    /// <summary>
    /// 起始登记人
    /// </summary>
    [NonSerialized]
    public const string FieldSegmentBeginUser = "SegmentBeginUser";

    /// <summary>
    /// 起始登记时间
    /// </summary>
    [NonSerialized]
    public const string FieldSegmentBeginDate = "SegmentBeginDate";

    /// <summary>
    /// 起始登记备注
    /// </summary>
    [NonSerialized]
    public const string FieldSegmentBeginRemark = "SegmentBeginRemark";

    /// <summary>
    /// 到达登记
    /// </summary>
    [NonSerialized]
    public const string FieldSegmentEndYN = "SegmentEndYN";

    /// <summary>
    /// 到达登记人
    /// </summary>
    [NonSerialized]
    public const string FieldSegmentEndUser = "SegmentEndUser";

    /// <summary>
    /// 到达登记时间
    /// </summary>
    [NonSerialized]
    public const string FieldSegmentEndDate = "SegmentEndDate";

    /// <summary>
    /// 到达登记备注
    /// </summary>
    [NonSerialized]
    public const string FieldSegmentEndRemark = "SegmentEndRemark";

    /// <summary>
    /// 状态
    /// </summary>
    [NonSerialized]
    public const string FieldStatusId = "StatusId";

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

    #endregion

    #region 列显示名
    /// <summary>
    /// 自增ID
    /// </summary>
    [NonSerialized]
    public const string DisISID = "自增ID";

    /// <summary>
    /// 运单编号
    /// </summary>
    [NonSerialized]
    public const string DisWaybillNo = "运单编号";

    /// <summary>
    /// 线路编号
    /// </summary>
    [NonSerialized]
    public const string DisSegmentNo = "线路编号";

    /// <summary>
    /// 车标号
    /// </summary>
    [NonSerialized]
    public const string DisCarmarkNo = "车标号";

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
    /// 线路起点
    /// </summary>
    [NonSerialized]
    public const string DisSegmentBeginNode = "线路起点";

    /// <summary>
    /// 线路终点
    /// </summary>
    [NonSerialized]
    public const string DisSegmentEndNode = "线路终点";

    /// <summary>
    /// 预估发车时间
    /// </summary>
    [NonSerialized]
    public const string DisExpectedTime = "预估发车时间";

    /// <summary>
    /// 预估时间
    /// </summary>
    [NonSerialized]
    public const string DisExpectedHour = "预估时间";

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
    /// 预估成本
    /// </summary>
    [NonSerialized]
    public const string DisExpectedCharge = "预估成本";

    /// <summary>
    /// 预估路桥费
    /// </summary>
    [NonSerialized]
    public const string DisExpectedPontage = "预估路桥费";

    /// <summary>
    /// 起始登记
    /// </summary>
    [NonSerialized]
    public const string DisSegmentBeginYN = "起始登记";

    /// <summary>
    /// 起始登记人
    /// </summary>
    [NonSerialized]
    public const string DisSegmentBeginUser = "起始登记人";

    /// <summary>
    /// 起始登记时间
    /// </summary>
    [NonSerialized]
    public const string DisSegmentBeginDate = "起始登记时间";

    /// <summary>
    /// 起始登记备注
    /// </summary>
    [NonSerialized]
    public const string DisSegmentBeginRemark = "起始登记备注";

    /// <summary>
    /// 到达登记
    /// </summary>
    [NonSerialized]
    public const string DisSegmentEndYN = "到达登记";

    /// <summary>
    /// 到达登记人
    /// </summary>
    [NonSerialized]
    public const string DisSegmentEndUser = "到达登记人";

    /// <summary>
    /// 到达登记时间
    /// </summary>
    [NonSerialized]
    public const string DisSegmentEndDate = "到达登记时间";

    /// <summary>
    /// 到达登记备注
    /// </summary>
    [NonSerialized]
    public const string DisSegmentEndRemark = "到达登记备注";

    /// <summary>
    /// 状态
    /// </summary>
    [NonSerialized]
    public const string DisStatusId = "状态";

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

    #endregion

    #endregion
}