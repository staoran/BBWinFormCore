using System.Runtime.Serialization;
using BB.Entity.Base;
using BB.Tools.Entity;

namespace BB.Entity.TMS;

/// <summary>
/// 运单操作记录 实体类
/// </summary>
[DataContract]
[Serializable]
[Table(DBTableName)]
public sealed class WaybillRecords : BaseEntity
{
    /// <summary>
    /// 默认构造函数（需要初始化属性的在此处理，复杂的属性值初始化通过重写 _bll.NewEntity 方法实现）
    /// </summary>
    public WaybillRecords()
    {
        TranNode = "*当前机构*";
        CancelYN = false;
        CreationDate = DateTime.Now;
        CreatedBy = "*当前用户*";
        LastUpdateDate = DateTime.Now;
        LastUpdatedBy = "*当前用户*";
        NotPublic = true;
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
    /// 当前网点
    /// </summary>
    [DataMember]
    [Column(FieldTranNode, DisTranNode)]
    public string TranNode { get; set; }

    /// <summary>
    /// 上一/下一网点
    /// </summary>
    [DataMember]
    [Column(FieldTranNodePN, DisTranNodePN)]
    public string TranNodePN { get; set; }

    /// <summary>
    /// 当前状态
    /// </summary>
    [DataMember]
    [Column(FieldStatusID, DisStatusID)]
    public string StatusID { get; set; }

    /// <summary>
    /// 相关人员
    /// </summary>
    [DataMember]
    [Column(FieldRelatedUser, DisRelatedUser)]
    public string RelatedUser { get; set; }

    /// <summary>
    /// 车标号
    /// </summary>
    [DataMember]
    [Column(FieldCarMarkNo, DisCarMarkNo)]
    public string CarMarkNo { get; set; }

    /// <summary>
    /// 配载编号
    /// </summary>
    [DataMember]
    [Column(FieldSegmentNo, DisSegmentNo)]
    public string SegmentNo { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [DataMember]
    [Column(FieldRemark, DisRemark)]
    public string Remark { get; set; }

    /// <summary>
    /// 作废
    /// </summary>
    [DataMember]
    [Column(FieldCancelYN, DisCancelYN)]
    public bool CancelYN { get; set; }

    /// <summary>
    /// 作废时间
    /// </summary>
    [DataMember]
    [Column(FieldCancelDate, DisCancelDate)]
    public DateTime? CancelDate { get; set; }

    /// <summary>
    /// 作废人
    /// </summary>
    [DataMember]
    [Column(FieldCancelBy, DisCancelBy)]
    public string CancelBy { get; set; }

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
    /// 对外公开
    /// </summary>
    [DataMember]
    [Column(FieldNotPublic, DisNotPublic)]
    public bool NotPublic { get; set; }

    #endregion

    #region 定义字段名称相关常量

    /// <summary>
    /// 表名
    /// </summary>
    [NonSerialized]
    public new const string DBTableName = "tb_TranWaybillRecords";

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
    /// 当前网点
    /// </summary>
    [NonSerialized]
    public const string FieldTranNode = "TranNode";

    /// <summary>
    /// 上一/下一网点
    /// </summary>
    [NonSerialized]
    public const string FieldTranNodePN = "TranNodePN";

    /// <summary>
    /// 当前状态
    /// </summary>
    [NonSerialized]
    public const string FieldStatusID = "StatusID";

    /// <summary>
    /// 相关人员
    /// </summary>
    [NonSerialized]
    public const string FieldRelatedUser = "RelatedUser";

    /// <summary>
    /// 车标号
    /// </summary>
    [NonSerialized]
    public const string FieldCarMarkNo = "CarMarkNo";

    /// <summary>
    /// 配载编号
    /// </summary>
    [NonSerialized]
    public const string FieldSegmentNo = "SegmentNo";

    /// <summary>
    /// 备注
    /// </summary>
    [NonSerialized]
    public const string FieldRemark = "Remark";

    /// <summary>
    /// 作废
    /// </summary>
    [NonSerialized]
    public const string FieldCancelYN = "CancelYN";

    /// <summary>
    /// 作废时间
    /// </summary>
    [NonSerialized]
    public const string FieldCancelDate = "CancelDate";

    /// <summary>
    /// 作废人
    /// </summary>
    [NonSerialized]
    public const string FieldCancelBy = "CancelBy";

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
    /// 对外公开
    /// </summary>
    [NonSerialized]
    public const string FieldNotPublic = "NotPublic";

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
    /// 当前网点
    /// </summary>
    [NonSerialized]
    public const string DisTranNode = "当前网点";

    /// <summary>
    /// 上一/下一网点
    /// </summary>
    [NonSerialized]
    public const string DisTranNodePN = "上一/下一网点";

    /// <summary>
    /// 当前状态
    /// </summary>
    [NonSerialized]
    public const string DisStatusID = "当前状态";

    /// <summary>
    /// 相关人员
    /// </summary>
    [NonSerialized]
    public const string DisRelatedUser = "相关人员";

    /// <summary>
    /// 车标号
    /// </summary>
    [NonSerialized]
    public const string DisCarMarkNo = "车标号";

    /// <summary>
    /// 配载编号
    /// </summary>
    [NonSerialized]
    public const string DisSegmentNo = "配载编号";

    /// <summary>
    /// 备注
    /// </summary>
    [NonSerialized]
    public const string DisRemark = "备注";

    /// <summary>
    /// 作废
    /// </summary>
    [NonSerialized]
    public const string DisCancelYN = "作废";

    /// <summary>
    /// 作废时间
    /// </summary>
    [NonSerialized]
    public const string DisCancelDate = "作废时间";

    /// <summary>
    /// 作废人
    /// </summary>
    [NonSerialized]
    public const string DisCancelBy = "作废人";

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
    /// 对外公开
    /// </summary>
    [NonSerialized]
    public const string DisNotPublic = "对外公开";

    #endregion

    #endregion
}