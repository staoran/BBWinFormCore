using System.Runtime.Serialization;
using BB.Entity.Base;

namespace BB.Entity.TMS;

/// <summary>
/// 费用调整确认 实体类
/// </summary>
[DataContract]
[Serializable]
[Table(DBTableName)]
public sealed class CostMsgs : BaseEntity
{
    /// <summary>
    /// 默认构造函数（需要初始化属性的在此处理）
    /// </summary>
    public CostMsgs()
    {
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
    /// 费用调整编号
    /// </summary>
    [DataMember]
    [Column(FieldCostMsgNo, DisCostMsgNo)]
    public string CostMsgNo { get; set; }

    /// <summary>
    /// 单据状态
    /// </summary>
    [DataMember]
    [Column(FieldStatusID, DisStatusID)]
    public string StatusID { get; set; }

    /// <summary>
    /// 回复网点
    /// </summary>
    [DataMember]
    [Column(FieldRecvMsgNode, DisRecvMsgNode)]
    public string RecvMsgNode { get; set; }

    /// <summary>
    /// 回复内容
    /// </summary>
    [DataMember]
    [Column(FieldRecvMsgContent, DisRecvMsgContent)]
    public string RecvMsgContent { get; set; }

    /// <summary>
    /// 附件
    /// </summary>
    [DataMember]
    [Column(FieldAttaPath, DisAttaPath)]
    public string AttaPath { get; set; }

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

    #endregion

    #region 定义字段名称相关常量

    /// <summary>
    /// 表名
    /// </summary>
    [NonSerialized]
    public new const string DBTableName = "tb_TranCostMsgs";

    /// <summary>
    /// 主键名
    /// </summary>
    [NonSerialized]
    public new const string PrimaryKey = FieldISID;

    /// <summary>
    /// 外键名
    /// </summary>
    [NonSerialized]
    public new const string ForeignKey = FieldCostMsgNo;

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
    /// 费用调整编号
    /// </summary>
    [NonSerialized]
    public const string FieldCostMsgNo = "CostMsgNo";

    /// <summary>
    /// 单据状态
    /// </summary>
    [NonSerialized]
    public const string FieldStatusID = "StatusID";

    /// <summary>
    /// 回复网点
    /// </summary>
    [NonSerialized]
    public const string FieldRecvMsgNode = "RecvMsgNode";

    /// <summary>
    /// 回复内容
    /// </summary>
    [NonSerialized]
    public const string FieldRecvMsgContent = "RecvMsgContent";

    /// <summary>
    /// 附件
    /// </summary>
    [NonSerialized]
    public const string FieldAttaPath = "AttaPath";

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
    /// 费用调整编号
    /// </summary>
    [NonSerialized]
    public const string DisCostMsgNo = "费用调整编号";

    /// <summary>
    /// 单据状态
    /// </summary>
    [NonSerialized]
    public const string DisStatusID = "单据状态";

    /// <summary>
    /// 回复网点
    /// </summary>
    [NonSerialized]
    public const string DisRecvMsgNode = "回复网点";

    /// <summary>
    /// 回复内容
    /// </summary>
    [NonSerialized]
    public const string DisRecvMsgContent = "回复内容";

    /// <summary>
    /// 附件
    /// </summary>
    [NonSerialized]
    public const string DisAttaPath = "附件";

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