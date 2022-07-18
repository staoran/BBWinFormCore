using System.Runtime.Serialization;
using BB.Entity.Base;

namespace BB.Entity.TMS;

/// <summary>
/// 费用调整 实体类
/// </summary>
[DataContract]
[Serializable]
[Table(DBTableName)]
public sealed class CostMsg : BaseEntity
{
    /// <summary>
    /// 默认构造函数（需要初始化属性的在此处理，复杂的属性值初始化通过重写 _bll.NewEntity 方法实现）
    /// </summary>
    public CostMsg()
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
    /// 费用调整编号
    /// </summary>
    [DataMember]
    [Key]
    [Column(FieldCostMsgNo, DisCostMsgNo)]
    public string CostMsgNo { get; set; }

    /// <summary>
    /// 来源类型
    /// </summary>
    [DataMember]
    [Column(FieldSourceType, DisSourceType)]
    public string SourceType { get; set; }

    /// <summary>
    /// 来源ID
    /// </summary>
    [DataMember]
    [Hide]
    [Column(FieldSourceISID, DisSourceISID)]
    public int? SourceISID { get; set; }

    /// <summary>
    /// 运单编号
    /// </summary>
    [DataMember]
    [Column(FieldWaybillNo, DisWaybillNo)]
    public string WaybillNo { get; set; }

    /// <summary>
    /// 申请网点
    /// </summary>
    [DataMember]
    [Column(FieldSendMsgNode, DisSendMsgNode)]
    public string SendMsgNode { get; set; }

    /// <summary>
    /// 内容描述
    /// </summary>
    [DataMember]
    [Column(FieldSendMsgContent, DisSendMsgContent)]
    public string SendMsgContent { get; set; }

    /// <summary>
    /// 附件
    /// </summary>
    [DataMember]
    [Column(FieldAttaPath, DisAttaPath)]
    public string AttaPath { get; set; }

    /// <summary>
    /// 接收类型
    /// </summary>
    [DataMember]
    [Column(FieldRecvMsgType, DisRecvMsgType)]
    public string RecvMsgType { get; set; }

    /// <summary>
    /// 接收网点
    /// </summary>
    [DataMember]
    [Column(FieldRecvMsgAccount, DisRecvMsgAccount)]
    public string RecvMsgAccount { get; set; }

    /// <summary>
    /// 费用类型
    /// </summary>
    [DataMember]
    [Column(FieldValueType, DisValueType)]
    public string ValueType { get; set; }

    /// <summary>
    /// 原始值
    /// </summary>
    [DataMember]
    [Column(FieldSourceValue, DisSourceValue)]
    public decimal SourceValue { get; set; }

    /// <summary>
    /// 修改值
    /// </summary>
    [DataMember]
    [Column(FieldActiveValue, DisActiveValue)]
    public decimal ActiveValue { get; set; }

    /// <summary>
    /// 单据状态
    /// </summary>
    [DataMember]
    [Column(FieldStatusID, DisStatusID)]
    public string StatusID { get; set; }

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
    /// 财务中心
    /// </summary>
    [DataMember]
    [Column(FieldFinancialCenter, DisFinancialCenter)]
    public string FinancialCenter { get; set; }

    /// <summary>
    /// 费用调整确认 集合
    /// </summary>
    [Ignore]
    public IEnumerable<CostMsgs> CostMsgsList { get; set; }

    #endregion

    #region 定义字段名称相关常量

    /// <summary>
    /// 表名
    /// </summary>
    [NonSerialized]
    public new const string DBTableName = "tb_TranCostMsg";

    /// <summary>
    /// 主键名
    /// </summary>
    [NonSerialized]
    public new const string PrimaryKey = FieldCostMsgNo;

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
    /// 来源类型
    /// </summary>
    [NonSerialized]
    public const string FieldSourceType = "SourceType";

    /// <summary>
    /// 来源ID
    /// </summary>
    [NonSerialized]
    public const string FieldSourceISID = "SourceISID";

    /// <summary>
    /// 运单编号
    /// </summary>
    [NonSerialized]
    public const string FieldWaybillNo = "WaybillNo";

    /// <summary>
    /// 申请网点
    /// </summary>
    [NonSerialized]
    public const string FieldSendMsgNode = "SendMsgNode";

    /// <summary>
    /// 内容描述
    /// </summary>
    [NonSerialized]
    public const string FieldSendMsgContent = "SendMsgContent";

    /// <summary>
    /// 附件
    /// </summary>
    [NonSerialized]
    public const string FieldAttaPath = "AttaPath";

    /// <summary>
    /// 接收类型
    /// </summary>
    [NonSerialized]
    public const string FieldRecvMsgType = "RecvMsgType";

    /// <summary>
    /// 接收网点
    /// </summary>
    [NonSerialized]
    public const string FieldRecvMsgAccount = "RecvMsgAccount";

    /// <summary>
    /// 费用类型
    /// </summary>
    [NonSerialized]
    public const string FieldValueType = "ValueType";

    /// <summary>
    /// 原始值
    /// </summary>
    [NonSerialized]
    public const string FieldSourceValue = "SourceValue";

    /// <summary>
    /// 修改值
    /// </summary>
    [NonSerialized]
    public const string FieldActiveValue = "ActiveValue";

    /// <summary>
    /// 单据状态
    /// </summary>
    [NonSerialized]
    public const string FieldStatusID = "StatusID";

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
    /// 费用调整编号
    /// </summary>
    [NonSerialized]
    public const string DisCostMsgNo = "费用调整编号";

    /// <summary>
    /// 来源类型
    /// </summary>
    [NonSerialized]
    public const string DisSourceType = "来源类型";

    /// <summary>
    /// 来源ID
    /// </summary>
    [NonSerialized]
    public const string DisSourceISID = "来源ID";

    /// <summary>
    /// 运单编号
    /// </summary>
    [NonSerialized]
    public const string DisWaybillNo = "运单编号";

    /// <summary>
    /// 申请网点
    /// </summary>
    [NonSerialized]
    public const string DisSendMsgNode = "申请网点";

    /// <summary>
    /// 内容描述
    /// </summary>
    [NonSerialized]
    public const string DisSendMsgContent = "内容描述";

    /// <summary>
    /// 附件
    /// </summary>
    [NonSerialized]
    public const string DisAttaPath = "附件";

    /// <summary>
    /// 接收类型
    /// </summary>
    [NonSerialized]
    public const string DisRecvMsgType = "接收类型";

    /// <summary>
    /// 接收网点
    /// </summary>
    [NonSerialized]
    public const string DisRecvMsgAccount = "接收网点";

    /// <summary>
    /// 费用类型
    /// </summary>
    [NonSerialized]
    public const string DisValueType = "费用类型";

    /// <summary>
    /// 原始值
    /// </summary>
    [NonSerialized]
    public const string DisSourceValue = "原始值";

    /// <summary>
    /// 修改值
    /// </summary>
    [NonSerialized]
    public const string DisActiveValue = "修改值";

    /// <summary>
    /// 单据状态
    /// </summary>
    [NonSerialized]
    public const string DisStatusID = "单据状态";

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
    /// 财务中心
    /// </summary>
    [NonSerialized]
    public const string DisFinancialCenter = "财务中心";

    #endregion

    #endregion
}