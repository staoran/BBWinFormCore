using System.Runtime.Serialization;
using BB.Entity.Base;
using BB.Tools.Entity;
using SqlSugar;

namespace BB.Entity.TMS;

/// <summary>
/// 网点资料 实体类
/// </summary>
[DataContract]
[Serializable]
[Table(DBTableName)]
public sealed class Node : BaseEntity<Nodes>
{
    /// <summary>
    /// 默认构造函数（需要初始化属性的在此处理，复杂的属性值初始化通过重写 _bll.NewEntity 方法实现）
    /// </summary>
    public Node()
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
    /// 网点ID
    /// </summary>
    [DataMember]
    [Key]
    [Column(FieldTranNodeNO, DisTranNodeNO)]
    public string TranNodeNO { get; set; }

    /// <summary>
    /// 结算网点编号
    /// </summary>
    [DataMember]
    [Column(FieldTranNodeCostNo, DisTranNodeCostNo)]
    public string TranNodeCostNo { get; set; }

    /// <summary>
    /// 网点名称
    /// </summary>
    [DataMember]
    [Column(FieldTranNodeName, DisTranNodeName)]
    public string TranNodeName { get; set; }

    /// <summary>
    /// 网点类型
    /// </summary>
    [DataMember]
    [Column(FieldTranNodeType, DisTranNodeType)]
    public string TranNodeType { get; set; }

    /// <summary>
    /// 合同开始时间
    /// </summary>
    [DataMember]
    [Column(FieldTranNodeBeginDate, DisTranNodeBeginDate)]
    public DateTime TranNodeBeginDate { get; set; }

    /// <summary>
    /// 合同终止时间
    /// </summary>
    [DataMember]
    [Column(FieldTranNodeEndDate, DisTranNodeEndDate)]
    public DateTime TranNodeEndDate { get; set; }

    /// <summary>
    /// 上级网点ID
    /// </summary>
    [DataMember]
    [Column(FieldParentNo, DisParentNo)]
    public string ParentNo { get; set; }

    /// <summary>
    /// 所属公司
    /// </summary>
    [DataMember]
    [Hide]
    [Column(FieldCompanyNo, DisCompanyNo)]
    public string CompanyNo { get; set; }

    /// <summary>
    /// 网点负责人
    /// </summary>
    [DataMember]
    [Column(FieldTranNodePerson, DisTranNodePerson)]
    public string TranNodePerson { get; set; }

    /// <summary>
    /// 负责人证件号码
    /// </summary>
    [DataMember]
    [Column(FieldTranNodePersonID, DisTranNodePersonID)]
    public string TranNodePersonID { get; set; }

    /// <summary>
    /// 网点联系方式
    /// </summary>
    [DataMember]
    [Column(FieldTranNodeMobile, DisTranNodeMobile)]
    public string TranNodeMobile { get; set; }

    /// <summary>
    /// 网点地址
    /// </summary>
    [DataMember]
    [Column(FieldTranNodeAddress, DisTranNodeAddress)]
    public string TranNodeAddress { get; set; }

    /// <summary>
    /// 锁机限制
    /// </summary>
    [DataMember]
    [Column(FieldLockLimit, DisLockLimit)]
    public bool LockLimit { get; set; }

    /// <summary>
    /// 锁机金额
    /// </summary>
    [DataMember]
    [Column(FieldLockLimitAmt, DisLockLimitAmt)]
    public decimal LockLimitAmt { get; set; }

    /// <summary>
    /// 警戒金额
    /// </summary>
    [DataMember]
    [Column(FieldWarningLimitAmt, DisWarningLimitAmt)]
    public decimal WarningLimitAmt { get; set; }

    /// <summary>
    /// 开通短信
    /// </summary>
    [DataMember]
    [Column(FieldSendSMS, DisSendSMS)]
    public bool SendSMS { get; set; }

    /// <summary>
    /// 封锁
    /// </summary>
    [DataMember]
    [Column(FieldISLocked, DisISLocked)]
    public bool ISLocked { get; set; }

    /// <summary>
    /// 开通回单
    /// </summary>
    [DataMember]
    [Column(FieldAckRec, DisAckRec)]
    public bool AckRec { get; set; }

    /// <summary>
    /// 代收款限额
    /// </summary>
    [DataMember]
    [Column(FieldAgencyRecLimitAmt, DisAgencyRecLimitAmt)]
    public decimal AgencyRecLimitAmt { get; set; }

    // /// <summary>
    // /// 代收款限额BKP
    // /// </summary>
    // [DataMember]
    // [Column(FieldAgencyRecLimitAmtBKP, DisAgencyRecLimitAmtBKP)]
    // public decimal AgencyRecLimitAmtBKP { get; set; }

    /// <summary>
    /// 到付款限额
    /// </summary>
    [DataMember]
    [Column(FieldCarriageForwardLimitAmt, DisCarriageForwardLimitAmt)]
    public decimal CarriageForwardLimitAmt { get; set; }

    // /// <summary>
    // /// 到付款限额BKP
    // /// </summary>
    // [DataMember]
    // [Column(FieldCarriageForwardLimitAmtBKP, DisCarriageForwardLimitAmtBKP)]
    // public decimal CarriageForwardLimitAmtBKP { get; set; }

    /// <summary>
    /// 省
    /// </summary>
    [DataMember]
    [Hide]
    [Column(FieldProvinceNo, DisProvinceNo)]
    public string ProvinceNo { get; set; }

    /// <summary>
    /// 市
    /// </summary>
    [DataMember]
    [Hide]
    [Column(FieldCityNo, DisCityNo)]
    public string CityNo { get; set; }

    /// <summary>
    /// 区
    /// </summary>
    [DataMember]
    [Column(FieldAreaNo, DisAreaNo)]
    public string AreaNo { get; set; }

    /// <summary>
    /// 进港时间
    /// </summary>
    [DataMember]
    [Column(FieldInTime, DisInTime)]
    public DateTime InTime { get; set; }

    /// <summary>
    /// 出港时间
    /// </summary>
    [DataMember]
    [Column(FieldOutTime, DisOutTime)]
    public DateTime OutTime { get; set; }

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
    /// 更新时间
    /// </summary>
    [DataMember]
    [OptimisticLock]
    [Column(FieldLastUpdateDate, DisLastUpdateDate)]
    public DateTime LastUpdateDate { get; set; }

    /// <summary>
    /// 更新人
    /// </summary>
    [DataMember]
    [Column(FieldLastUpdatedBy, DisLastUpdatedBy)]
    public string LastUpdatedBy { get; set; }

    /// <summary>
    /// 网点状态
    /// </summary>
    [DataMember]
    [Column(FieldTranNodeStatus, DisTranNodeStatus)]
    public string TranNodeStatus { get; set; }

    /// <summary>
    /// 是否开放
    /// </summary>
    [DataMember]
    [Column(FieldPublicYN, DisPublicYN)]
    public bool PublicYN { get; set; }

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
    /// 签收周期起算时间
    /// </summary>
    [DataMember]
    [Column(FieldSignLoopEndTime, DisSignLoopEndTime)]
    public DateTime SignLoopEndTime { get; set; }

    /// <summary>
    /// 签收最晚时间
    /// </summary>
    [DataMember]
    [Column(FieldSignLimitTime, DisSignLimitTime)]
    public DateTime SignLimitTime { get; set; }

    /// <summary>
    /// 签收天数
    /// </summary>
    [DataMember]
    [Column(FieldSignDays, DisSignDays)]
    public int SignDays { get; set; }

    /// <summary>
    /// 回单返回天数
    /// </summary>
    [DataMember]
    [Column(FieldAckRecDays, DisAckRecDays)]
    public int AckRecDays { get; set; }

    /// <summary>
    /// 跨平台结算主体
    /// </summary>
    [DataMember]
    [Column(FieldCostMasterYN, DisCostMasterYN)]
    public bool CostMasterYN { get; set; }

    /// <summary>
    /// 管理费
    /// </summary>
    [DataMember]
    [Column(FieldManagementFee, DisManagementFee)]
    public decimal ManagementFee { get; set; }

    /// <summary>
    /// 系统使用费
    /// </summary>
    [DataMember]
    [Column(FieldUsageFee, DisUsageFee)]
    public decimal UsageFee { get; set; }

    /// <summary>
    /// 押金
    /// </summary>
    [DataMember]
    [Column(FieldDeposit, DisDeposit)]
    public decimal Deposit { get; set; }

    /// <summary>
    /// 合同备注
    /// </summary>
    [DataMember]
    [Column(FieldContractNote, DisContractNote)]
    public string ContractNote { get; set; }

    /// <summary>
    /// 仅送货
    /// </summary>
    [DataMember]
    [Column(FieldDispatchOnly, DisDispatchOnly)]
    public bool DispatchOnly { get; set; }

    /// <summary>
    /// 自提限重
    /// </summary>
    [DataMember]
    [Column(FieldPickupWeightLimit, DisPickupWeightLimit)]
    public decimal PickupWeightLimit { get; set; }

    /// <summary>
    /// 自提限方
    /// </summary>
    [DataMember]
    [Column(FieldPickupVolumeLimit, DisPickupVolumeLimit)]
    public decimal PickupVolumeLimit { get; set; }

    /// <summary>
    /// 网点坐标
    /// </summary>
    [DataMember]
    [Column(FieldTranNodeAxes, DisTranNodeAxes)]
    public string TranNodeAxes { get; set; }

    /// <summary>
    /// 网点锁机kpi是否执行
    /// </summary>
    [DataMember]
    [Column(FieldIsLockLimitKPI, DisIsLockLimitKPI)]
    public bool IsLockLimitKPI { get; set; }

    /// <summary>
    /// 所属财务中心
    /// </summary>
    [DataMember]
    [Column(FieldFinancialCenter, DisFinancialCenter)]
    public string FinancialCenter { get; set; }

    /// <summary>
    /// 白名单
    /// </summary>
    [DataMember]
    [Column(FieldWhiteList, DisWhiteList)]
    public string WhiteList { get; set; }

    /// <summary>
    /// 黑名单
    /// </summary>
    [DataMember]
    [Column(FieldBlackList, DisBlackList)]
    public string BlackList { get; set; }

    /// <summary>
    /// 网点区域 集合
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(Nodes.TranNodeNO))]
    public List<Nodes> NodesList { get; set; }

    #endregion

    #region 定义字段名称相关常量

    /// <summary>
    /// 表名
    /// </summary>
    [NonSerialized]
    public new const string DBTableName = "tb_TranNode";

    /// <summary>
    /// 主键名
    /// </summary>
    [NonSerialized]
    public new const string PrimaryKey = FieldTranNodeNO;

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

    /// <summary>
    /// 子表数据
    /// </summary>
    [DataMember]
    [Ignore]
    [Navigate(NavigateType.OneToMany, ChildForeignKey)]
    public new List<Nodes>? ChildTableList
    {
        get => base.ChildTableList;
        set => base.ChildTableList = value;
    }

    /// <summary>
    /// 子表外键
    /// </summary>
    [NonSerialized]
    public new const string ChildForeignKey = Nodes.ForeignKey;

    #region 列名
    /// <summary>
    /// 自增ID
    /// </summary>
    [NonSerialized]
    public const string FieldISID = "ISID";

    /// <summary>
    /// 网点ID
    /// </summary>
    [NonSerialized]
    public const string FieldTranNodeNO = "TranNodeNO";

    /// <summary>
    /// 结算网点编号
    /// </summary>
    [NonSerialized]
    public const string FieldTranNodeCostNo = "TranNodeCostNo";

    /// <summary>
    /// 网点名称
    /// </summary>
    [NonSerialized]
    public const string FieldTranNodeName = "TranNodeName";

    /// <summary>
    /// 网点类型
    /// </summary>
    [NonSerialized]
    public const string FieldTranNodeType = "TranNodeType";

    /// <summary>
    /// 合同开始时间
    /// </summary>
    [NonSerialized]
    public const string FieldTranNodeBeginDate = "TranNodeBeginDate";

    /// <summary>
    /// 合同终止时间
    /// </summary>
    [NonSerialized]
    public const string FieldTranNodeEndDate = "TranNodeEndDate";

    /// <summary>
    /// 上级网点ID
    /// </summary>
    [NonSerialized]
    public const string FieldParentNo = "ParentNo";

    /// <summary>
    /// 所属公司
    /// </summary>
    [NonSerialized]
    public const string FieldCompanyNo = "CompanyNo";

    /// <summary>
    /// 网点负责人
    /// </summary>
    [NonSerialized]
    public const string FieldTranNodePerson = "TranNodePerson";

    /// <summary>
    /// 负责人证件号码
    /// </summary>
    [NonSerialized]
    public const string FieldTranNodePersonID = "TranNodePersonID";

    /// <summary>
    /// 网点联系方式
    /// </summary>
    [NonSerialized]
    public const string FieldTranNodeMobile = "TranNodeMobile";

    /// <summary>
    /// 网点地址
    /// </summary>
    [NonSerialized]
    public const string FieldTranNodeAddress = "TranNodeAddress";

    /// <summary>
    /// 锁机限制
    /// </summary>
    [NonSerialized]
    public const string FieldLockLimit = "LockLimit";

    /// <summary>
    /// 锁机金额
    /// </summary>
    [NonSerialized]
    public const string FieldLockLimitAmt = "LockLimitAmt";

    /// <summary>
    /// 警戒金额
    /// </summary>
    [NonSerialized]
    public const string FieldWarningLimitAmt = "WarningLimitAmt";

    /// <summary>
    /// 开通短信
    /// </summary>
    [NonSerialized]
    public const string FieldSendSMS = "SendSMS";

    /// <summary>
    /// 封锁
    /// </summary>
    [NonSerialized]
    public const string FieldISLocked = "ISLocked";

    /// <summary>
    /// 开通回单
    /// </summary>
    [NonSerialized]
    public const string FieldAckRec = "AckRec";

    /// <summary>
    /// 代收款限额
    /// </summary>
    [NonSerialized]
    public const string FieldAgencyRecLimitAmt = "AgencyRecLimitAmt";

    // /// <summary>
    // /// 代收款限额BKP
    // /// </summary>
    // [NonSerialized]
    // public const string FieldAgencyRecLimitAmtBKP = "AgencyRecLimitAmtBKP";

    /// <summary>
    /// 到付款限额
    /// </summary>
    [NonSerialized]
    public const string FieldCarriageForwardLimitAmt = "CarriageForwardLimitAmt";

    // /// <summary>
    // /// 到付款限额BKP
    // /// </summary>
    // [NonSerialized]
    // public const string FieldCarriageForwardLimitAmtBKP = "CarriageForwardLimitAmtBKP";

    /// <summary>
    /// 省
    /// </summary>
    [NonSerialized]
    public const string FieldProvinceNo = "ProvinceNo";

    /// <summary>
    /// 市
    /// </summary>
    [NonSerialized]
    public const string FieldCityNo = "CityNo";

    /// <summary>
    /// 区
    /// </summary>
    [NonSerialized]
    public const string FieldAreaNo = "AreaNo";

    /// <summary>
    /// 进港时间
    /// </summary>
    [NonSerialized]
    public const string FieldInTime = "InTime";

    /// <summary>
    /// 出港时间
    /// </summary>
    [NonSerialized]
    public const string FieldOutTime = "OutTime";

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
    /// 更新时间
    /// </summary>
    [NonSerialized]
    public const string FieldLastUpdateDate = "LastUpdateDate";

    /// <summary>
    /// 更新人
    /// </summary>
    [NonSerialized]
    public const string FieldLastUpdatedBy = "LastUpdatedBy";

    /// <summary>
    /// 网点状态
    /// </summary>
    [NonSerialized]
    public const string FieldTranNodeStatus = "TranNodeStatus";

    /// <summary>
    /// 是否开放
    /// </summary>
    [NonSerialized]
    public const string FieldPublicYN = "PublicYN";

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
    /// 签收周期起算时间
    /// </summary>
    [NonSerialized]
    public const string FieldSignLoopEndTime = "SignLoopEndTime";

    /// <summary>
    /// 签收最晚时间
    /// </summary>
    [NonSerialized]
    public const string FieldSignLimitTime = "SignLimitTime";

    /// <summary>
    /// 签收天数
    /// </summary>
    [NonSerialized]
    public const string FieldSignDays = "SignDays";

    /// <summary>
    /// 回单返回天数
    /// </summary>
    [NonSerialized]
    public const string FieldAckRecDays = "AckRecDays";

    /// <summary>
    /// 跨平台结算主体
    /// </summary>
    [NonSerialized]
    public const string FieldCostMasterYN = "CostMasterYN";

    /// <summary>
    /// 管理费
    /// </summary>
    [NonSerialized]
    public const string FieldManagementFee = "ManagementFee";

    /// <summary>
    /// 系统使用费
    /// </summary>
    [NonSerialized]
    public const string FieldUsageFee = "UsageFee";

    /// <summary>
    /// 押金
    /// </summary>
    [NonSerialized]
    public const string FieldDeposit = "Deposit";

    /// <summary>
    /// 合同备注
    /// </summary>
    [NonSerialized]
    public const string FieldContractNote = "ContractNote";

    /// <summary>
    /// 仅送货
    /// </summary>
    [NonSerialized]
    public const string FieldDispatchOnly = "DispatchOnly";

    /// <summary>
    /// 自提限重
    /// </summary>
    [NonSerialized]
    public const string FieldPickupWeightLimit = "PickupWeightLimit";

    /// <summary>
    /// 自提限方
    /// </summary>
    [NonSerialized]
    public const string FieldPickupVolumeLimit = "PickupVolumeLimit";

    /// <summary>
    /// 网点坐标(无用)
    /// </summary>
    [NonSerialized]
    public const string FieldTranNodeAxes = "TranNodeAxes";

    /// <summary>
    /// 网点锁机kpi是否执行
    /// </summary>
    [NonSerialized]
    public const string FieldIsLockLimitKPI = "IsLockLimitKPI";

    /// <summary>
    /// 所属财务中心
    /// </summary>
    [NonSerialized]
    public const string FieldFinancialCenter = "FinancialCenter";

    /// <summary>
    /// 白名单
    /// </summary>
    [NonSerialized]
    public const string FieldWhiteList = "WhiteList";

    /// <summary>
    /// 黑名单
    /// </summary>
    [NonSerialized]
    public const string FieldBlackList = "BlackList";

    #endregion

    #region 列显示名
    /// <summary>
    /// 自增ID
    /// </summary>
    [NonSerialized]
    public const string DisISID = "自增ID";

    /// <summary>
    /// 网点ID
    /// </summary>
    [NonSerialized]
    public const string DisTranNodeNO = "网点ID";

    /// <summary>
    /// 结算网点编号
    /// </summary>
    [NonSerialized]
    public const string DisTranNodeCostNo = "结算网点编号";

    /// <summary>
    /// 网点名称
    /// </summary>
    [NonSerialized]
    public const string DisTranNodeName = "网点名称";

    /// <summary>
    /// 网点类型
    /// </summary>
    [NonSerialized]
    public const string DisTranNodeType = "网点类型";

    /// <summary>
    /// 合同开始时间
    /// </summary>
    [NonSerialized]
    public const string DisTranNodeBeginDate = "合同开始时间";

    /// <summary>
    /// 合同终止时间
    /// </summary>
    [NonSerialized]
    public const string DisTranNodeEndDate = "合同终止时间";

    /// <summary>
    /// 上级网点ID
    /// </summary>
    [NonSerialized]
    public const string DisParentNo = "上级网点ID";

    /// <summary>
    /// 所属公司
    /// </summary>
    [NonSerialized]
    public const string DisCompanyNo = "所属公司";

    /// <summary>
    /// 网点负责人
    /// </summary>
    [NonSerialized]
    public const string DisTranNodePerson = "网点负责人";

    /// <summary>
    /// 负责人证件号码
    /// </summary>
    [NonSerialized]
    public const string DisTranNodePersonID = "负责人证件号码";

    /// <summary>
    /// 网点联系方式
    /// </summary>
    [NonSerialized]
    public const string DisTranNodeMobile = "网点联系方式";

    /// <summary>
    /// 网点地址
    /// </summary>
    [NonSerialized]
    public const string DisTranNodeAddress = "网点地址";

    /// <summary>
    /// 锁机限制
    /// </summary>
    [NonSerialized]
    public const string DisLockLimit = "锁机限制";

    /// <summary>
    /// 锁机金额
    /// </summary>
    [NonSerialized]
    public const string DisLockLimitAmt = "锁机金额";

    /// <summary>
    /// 警戒金额
    /// </summary>
    [NonSerialized]
    public const string DisWarningLimitAmt = "警戒金额";

    /// <summary>
    /// 开通短信
    /// </summary>
    [NonSerialized]
    public const string DisSendSMS = "开通短信";

    /// <summary>
    /// 封锁
    /// </summary>
    [NonSerialized]
    public const string DisISLocked = "封锁";

    /// <summary>
    /// 开通回单
    /// </summary>
    [NonSerialized]
    public const string DisAckRec = "开通回单";

    /// <summary>
    /// 代收款限额
    /// </summary>
    [NonSerialized]
    public const string DisAgencyRecLimitAmt = "代收款限额";

    // /// <summary>
    // /// 代收款限额BKP
    // /// </summary>
    // [NonSerialized]
    // public const string DisAgencyRecLimitAmtBKP = "代收款限额BKP";

    /// <summary>
    /// 到付款限额
    /// </summary>
    [NonSerialized]
    public const string DisCarriageForwardLimitAmt = "到付款限额";

    // /// <summary>
    // /// 到付款限额BKP
    // /// </summary>
    // [NonSerialized]
    // public const string DisCarriageForwardLimitAmtBKP = "到付款限额BKP";

    /// <summary>
    /// 省
    /// </summary>
    [NonSerialized]
    public const string DisProvinceNo = "省";

    /// <summary>
    /// 市
    /// </summary>
    [NonSerialized]
    public const string DisCityNo = "市";

    /// <summary>
    /// 区
    /// </summary>
    [NonSerialized]
    public const string DisAreaNo = "区";

    /// <summary>
    /// 进港时间
    /// </summary>
    [NonSerialized]
    public const string DisInTime = "进港时间";

    /// <summary>
    /// 出港时间
    /// </summary>
    [NonSerialized]
    public const string DisOutTime = "出港时间";

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
    /// 更新时间
    /// </summary>
    [NonSerialized]
    public const string DisLastUpdateDate = "更新时间";

    /// <summary>
    /// 更新人
    /// </summary>
    [NonSerialized]
    public const string DisLastUpdatedBy = "更新人";

    /// <summary>
    /// 网点状态
    /// </summary>
    [NonSerialized]
    public const string DisTranNodeStatus = "网点状态";

    /// <summary>
    /// 是否开放
    /// </summary>
    [NonSerialized]
    public const string DisPublicYN = "是否开放";

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
    /// 签收周期起算时间
    /// </summary>
    [NonSerialized]
    public const string DisSignLoopEndTime = "签收周期起算时间";

    /// <summary>
    /// 签收最晚时间
    /// </summary>
    [NonSerialized]
    public const string DisSignLimitTime = "签收最晚时间";

    /// <summary>
    /// 签收天数
    /// </summary>
    [NonSerialized]
    public const string DisSignDays = "签收天数";

    /// <summary>
    /// 回单返回天数
    /// </summary>
    [NonSerialized]
    public const string DisAckRecDays = "回单返回天数";

    /// <summary>
    /// 跨平台结算主体
    /// </summary>
    [NonSerialized]
    public const string DisCostMasterYN = "跨平台结算主体";

    /// <summary>
    /// 管理费
    /// </summary>
    [NonSerialized]
    public const string DisManagementFee = "管理费";

    /// <summary>
    /// 系统使用费
    /// </summary>
    [NonSerialized]
    public const string DisUsageFee = "系统使用费";

    /// <summary>
    /// 押金
    /// </summary>
    [NonSerialized]
    public const string DisDeposit = "押金";

    /// <summary>
    /// 合同备注
    /// </summary>
    [NonSerialized]
    public const string DisContractNote = "合同备注";

    /// <summary>
    /// 仅送货
    /// </summary>
    [NonSerialized]
    public const string DisDispatchOnly = "仅送货";

    /// <summary>
    /// 自提限重
    /// </summary>
    [NonSerialized]
    public const string DisPickupWeightLimit = "自提限重";

    /// <summary>
    /// 自提限方
    /// </summary>
    [NonSerialized]
    public const string DisPickupVolumeLimit = "自提限方";

    /// <summary>
    /// 网点坐标(无用)
    /// </summary>
    [NonSerialized]
    public const string DisTranNodeAxes = "网点坐标(无用)";

    /// <summary>
    /// 网点锁机kpi是否执行
    /// </summary>
    [NonSerialized]
    public const string DisIsLockLimitKPI = "网点锁机kpi是否执行";

    /// <summary>
    /// 所属财务中心
    /// </summary>
    [NonSerialized]
    public const string DisFinancialCenter = "所属财务中心";

    /// <summary>
    /// 白名单
    /// </summary>
    [NonSerialized]
    public const string DisWhiteList = "白名单";

    /// <summary>
    /// 黑名单
    /// </summary>
    [NonSerialized]
    public const string DisBlackList = "黑名单";

    #endregion

    #endregion
}