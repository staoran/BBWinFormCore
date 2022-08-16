using System.Runtime.Serialization;
using BB.Entity.Base;
using BB.Tools.Entity;

namespace BB.Entity.TMS;

/// <summary>
/// 问题件 实体类
/// </summary>
[DataContract]
[Serializable]
[Table(DBTableName)]
public sealed class Message : BaseEntity<Messages>
{
    /// <summary>
    /// 默认构造函数（需要初始化属性的在此处理，复杂的属性值初始化通过重写 _bll.NewEntity 方法实现）
    /// </summary>
    public Message()
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
    /// 问题件编号
    /// </summary>
    [DataMember]
    [Key]
    [Column(FieldMsgNo, DisMsgNo)]
    public string MsgNo { get; set; }

    /// <summary>
    /// 问题件类型
    /// </summary>
    [DataMember]
    [Column(FieldMsgType, DisMsgType)]
    public string MsgType { get; set; }

    /// <summary>
    /// 运单号
    /// </summary>
    [DataMember]
    [Column(FieldWaybillNo, DisWaybillNo)]
    public string WaybillNo { get; set; }

    /// <summary>
    /// 发送方网点
    /// </summary>
    [DataMember]
    [Column(FieldSendMsgNode, DisSendMsgNode)]
    public string SendMsgNode { get; set; }

    /// <summary>
    /// 问题件内容
    /// </summary>
    [DataMember]
    [Column(FieldSendMsgContent, DisSendMsgContent)]
    public string SendMsgContent { get; set; }

    /// <summary>
    /// 接收方网点
    /// </summary>
    [DataMember]
    [Column(FieldRecvMsgNode, DisRecvMsgNode)]
    public string RecvMsgNode { get; set; }

    /// <summary>
    /// 处理状态
    /// </summary>
    [DataMember]
    [Column(FieldDealStatus, DisDealStatus)]
    public string DealStatus { get; set; }

    /// <summary>
    /// 附件地址
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
    /// 问题件回复 集合
    /// </summary>
    [Ignore]
    public IEnumerable<Messages> MessagesList { get; set; }

    #endregion

    #region 定义字段名称相关常量

    /// <summary>
    /// 表名
    /// </summary>
    [NonSerialized]
    public new const string DBTableName = "tb_TranMessage";

    /// <summary>
    /// 主键名
    /// </summary>
    [NonSerialized]
    public new const string PrimaryKey = FieldMsgNo;

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
    /// 问题件编号
    /// </summary>
    [NonSerialized]
    public const string FieldMsgNo = "MsgNo";

    /// <summary>
    /// 问题件类型
    /// </summary>
    [NonSerialized]
    public const string FieldMsgType = "MsgType";

    /// <summary>
    /// 运单号
    /// </summary>
    [NonSerialized]
    public const string FieldWaybillNo = "WaybillNo";

    /// <summary>
    /// 发送方网点编号
    /// </summary>
    [NonSerialized]
    public const string FieldSendMsgNode = "SendMsgNode";

    /// <summary>
    /// 问题件内容
    /// </summary>
    [NonSerialized]
    public const string FieldSendMsgContent = "SendMsgContent";

    /// <summary>
    /// 接收方网点编号
    /// </summary>
    [NonSerialized]
    public const string FieldRecvMsgNode = "RecvMsgNode";

    /// <summary>
    /// 处理状态
    /// </summary>
    [NonSerialized]
    public const string FieldDealStatus = "DealStatus";

    /// <summary>
    /// 附件地址
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
    /// 问题件编号
    /// </summary>
    [NonSerialized]
    public const string DisMsgNo = "问题件编号";

    /// <summary>
    /// 问题件类型
    /// </summary>
    [NonSerialized]
    public const string DisMsgType = "问题件类型";

    /// <summary>
    /// 运单号
    /// </summary>
    [NonSerialized]
    public const string DisWaybillNo = "运单号";

    /// <summary>
    /// 发送方网点编号
    /// </summary>
    [NonSerialized]
    public const string DisSendMsgNode = "发送方网点编号";

    /// <summary>
    /// 问题件内容
    /// </summary>
    [NonSerialized]
    public const string DisSendMsgContent = "问题件内容";

    /// <summary>
    /// 接收方网点编号
    /// </summary>
    [NonSerialized]
    public const string DisRecvMsgNode = "接收方网点编号";

    /// <summary>
    /// 处理状态
    /// </summary>
    [NonSerialized]
    public const string DisDealStatus = "处理状态";

    /// <summary>
    /// 附件地址
    /// </summary>
    [NonSerialized]
    public const string DisAttaPath = "附件地址";

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