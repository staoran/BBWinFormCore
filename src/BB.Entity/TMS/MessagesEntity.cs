using System.Runtime.Serialization;
using BB.Entity.Base;
using BB.Tools.Entity;

namespace BB.Entity.TMS;

/// <summary>
/// 问题件回复 实体类
/// </summary>
[DataContract]
[Serializable]
[Table(DBTableName)]
public sealed class Messages : BaseEntity
{
    /// <summary>
    /// 默认构造函数（需要初始化属性的在此处理）
    /// </summary>
    public Messages()
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
    /// 问题件编号
    /// </summary>
    [DataMember]
    [Column(FieldMsgNo, DisMsgNo)]
    public string MsgNo { get; set; }

    /// <summary>
    /// 处理状态
    /// </summary>
    [DataMember]
    [Column(FieldDealStatus, DisDealStatus)]
    public string DealStatus { get; set; }

    /// <summary>
    /// 处理内容
    /// </summary>
    [DataMember]
    [Column(FieldDealContent, DisDealContent)]
    public string DealContent { get; set; }

    /// <summary>
    /// 附件地址
    /// </summary>
    [DataMember]
    [Column(FieldAttaPath, DisAttaPath)]
    public string AttaPath { get; set; }

    /// <summary>
    /// 已读时间
    /// </summary>
    [DataMember]
    [Hide]
    [Column(FieldLastReadTime, DisLastReadTime)]
    public DateTime LastReadTime { get; set; }

    /// <summary>
    /// 已读帐号
    /// </summary>
    [DataMember]
    [Hide]
    [Column(FieldLaseRealAccount, DisLaseRealAccount)]
    public string LaseRealAccount { get; set; }

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
    [Column(FieldLastUpdateBy, DisLastUpdateBy)]
    public string LastUpdateBy { get; set; }

    #endregion

    #region 定义字段名称相关常量

    /// <summary>
    /// 表名
    /// </summary>
    [NonSerialized]
    public new const string DBTableName = "tb_TranMessages";

    /// <summary>
    /// 主键名
    /// </summary>
    [NonSerialized]
    public new const string PrimaryKey = FieldISID;

    /// <summary>
    /// 外键名
    /// </summary>
    [NonSerialized]
    public new const string ForeignKey = FieldMsgNo;

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
    /// 处理状态
    /// </summary>
    [NonSerialized]
    public const string FieldDealStatus = "DealStatus";

    /// <summary>
    /// 处理内容
    /// </summary>
    [NonSerialized]
    public const string FieldDealContent = "DealContent";

    /// <summary>
    /// 附件地址
    /// </summary>
    [NonSerialized]
    public const string FieldAttaPath = "AttaPath";

    /// <summary>
    /// 已读时间
    /// </summary>
    [NonSerialized]
    public const string FieldLastReadTime = "LastReadTime";

    /// <summary>
    /// 已读帐号
    /// </summary>
    [NonSerialized]
    public const string FieldLaseRealAccount = "LaseRealAccount";

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
    public const string FieldLastUpdateBy = "LastUpdateBy";

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
    /// 处理状态
    /// </summary>
    [NonSerialized]
    public const string DisDealStatus = "处理状态";

    /// <summary>
    /// 处理内容
    /// </summary>
    [NonSerialized]
    public const string DisDealContent = "处理内容";

    /// <summary>
    /// 附件地址
    /// </summary>
    [NonSerialized]
    public const string DisAttaPath = "附件地址";

    /// <summary>
    /// 已读时间
    /// </summary>
    [NonSerialized]
    public const string DisLastReadTime = "已读时间";

    /// <summary>
    /// 已读帐号
    /// </summary>
    [NonSerialized]
    public const string DisLaseRealAccount = "已读帐号";

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
    public const string DisLastUpdateBy = "修改人";

    #endregion

    #endregion
}