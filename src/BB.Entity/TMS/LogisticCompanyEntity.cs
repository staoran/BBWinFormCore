using System.Runtime.Serialization;
using BB.Entity.Base;

namespace BB.Entity.TMS;

/// <summary>
/// 承运商资料 实体类
/// </summary>
[DataContract]
[Serializable]
[Table(DBTableName)]
public sealed class LogisticCompany : BaseEntity
{
    /// <summary>
    /// 默认构造函数（需要初始化属性的在此处理，复杂的属性值初始化通过重写 _bll.NewEntity 方法实现）
    /// </summary>
    public LogisticCompany()
    {
    }

    #region Property Members

    /// <summary>
    /// 自增ID
    /// </summary>
    [DataMember]
    [Hide]
    [Identity]
    [Sort(IsDesc)]
    [Column(Fieldisid, Disisid)]
    public int isid { get; set; }

    /// <summary>
    /// 网点名称
    /// </summary>
    [DataMember]
    [Column(FieldOrgCode, DisOrgCode)]
    public string OrgCode { get; set; }

    /// <summary>
    /// 承运商编号
    /// </summary>
    [DataMember]
    [Key]
    [Column(FieldLogisticCode, DisLogisticCode)]
    public string LogisticCode { get; set; }

    /// <summary>
    /// 承运商名称
    /// </summary>
    [DataMember]
    [Column(FieldLogisticName, DisLogisticName)]
    public string LogisticName { get; set; }

    /// <summary>
    /// 助记码
    /// </summary>
    [DataMember]
    [Column(FieldZJM, DisZJM)]
    public string ZJM { get; set; }

    /// <summary>
    /// 所属公司
    /// </summary>
    [DataMember]
    [Hide]
    [Column(FieldStation, DisStation)]
    public string Station { get; set; }

    /// <summary>
    /// 联系人
    /// </summary>
    [DataMember]
    [Column(FieldContacts, DisContacts)]
    public string Contacts { get; set; }

    /// <summary>
    /// 电话
    /// </summary>
    [DataMember]
    [Column(FieldTel, DisTel)]
    public string Tel { get; set; }

    /// <summary>
    /// 手机
    /// </summary>
    [DataMember]
    [Hide]
    [Column(FieldMobile, DisMobile)]
    public string Mobile { get; set; }

    /// <summary>
    /// 地址
    /// </summary>
    [DataMember]
    [Column(FieldAddress, DisAddress)]
    public string Address { get; set; }

    /// <summary>
    /// 传真
    /// </summary>
    [DataMember]
    [Hide]
    [Column(FieldFax, DisFax)]
    public string Fax { get; set; }

    /// <summary>
    /// 主营线路
    /// </summary>
    [DataMember]
    [Column(FieldMainLine, DisMainLine)]
    public string MainLine { get; set; }

    /// <summary>
    /// 信誉
    /// </summary>
    [DataMember]
    [Column(FieldTrustLevel, DisTrustLevel)]
    public string TrustLevel { get; set; }

    /// <summary>
    /// 法人
    /// </summary>
    [DataMember]
    [Column(FieldLegal, DisLegal)]
    public string Legal { get; set; }

    /// <summary>
    /// 税号
    /// </summary>
    [DataMember]
    [Column(FieldTax, DisTax)]
    public string Tax { get; set; }

    /// <summary>
    /// 开户行
    /// </summary>
    [DataMember]
    [Column(FieldBank, DisBank)]
    public string Bank { get; set; }

    /// <summary>
    /// 银行账号
    /// </summary>
    [DataMember]
    [Column(FieldBankAccount, DisBankAccount)]
    public string BankAccount { get; set; }

    /// <summary>
    /// 账期说明
    /// </summary>
    [DataMember]
    [Column(FieldPaymentTerm, DisPaymentTerm)]
    public string PaymentTerm { get; set; }

    /// <summary>
    /// 合同起始日期
    /// </summary>
    [DataMember]
    [Column(FieldContractDate1, DisContractDate1)]
    public DateTime ContractDate1 { get; set; }

    /// <summary>
    /// 合同结束日期
    /// </summary>
    [DataMember]
    [Column(FieldContractDate2, DisContractDate2)]
    public DateTime ContractDate2 { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    [DataMember]
    [Column(FieldInUse, DisInUse)]
    public bool InUse { get; set; }

    /// <summary>
    /// 是否开票
    /// </summary>
    [DataMember]
    [Column(FieldFlagInvoice, DisFlagInvoice)]
    public bool FlagInvoice { get; set; }

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
    /// 数据来源
    /// </summary>
    [DataMember]
    [Hide]
    [Column(FieldDataType, DisDataType)]
    public string DataType { get; set; }

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
    /// 附件地址
    /// </summary>
    [DataMember]
    [Hide]
    [Column(FieldAttaPath, DisAttaPath)]
    public string AttaPath { get; set; }

    /// <summary>
    /// 是否作废
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
    [Column(FieldCancelUser, DisCancelUser)]
    public string CancelUser { get; set; }

    #endregion

    #region 定义字段名称相关常量

    /// <summary>
    /// 表名
    /// </summary>
    [NonSerialized]
    public new const string DBTableName = "dt_LogisticCompany";

    /// <summary>
    /// 主键名
    /// </summary>
    [NonSerialized]
    public new const string PrimaryKey = FieldLogisticCode;

    /// <summary>
    /// 排序字段
    /// </summary>
    [NonSerialized]
    public new const string SortKey = Fieldisid;

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
    public const string Fieldisid = "isid";

    /// <summary>
    /// 乐观锁
    /// </summary>
    [NonSerialized]
    public const string FieldTS = "TS";

    /// <summary>
    /// 网点名称
    /// </summary>
    [NonSerialized]
    public const string FieldOrgCode = "OrgCode";

    /// <summary>
    /// 承运商编号
    /// </summary>
    [NonSerialized]
    public const string FieldLogisticCode = "LogisticCode";

    /// <summary>
    /// 承运商名称
    /// </summary>
    [NonSerialized]
    public const string FieldLogisticName = "LogisticName";

    /// <summary>
    /// 助记码
    /// </summary>
    [NonSerialized]
    public const string FieldZJM = "ZJM";

    /// <summary>
    /// 所属公司
    /// </summary>
    [NonSerialized]
    public const string FieldStation = "Station";

    /// <summary>
    /// 联系人
    /// </summary>
    [NonSerialized]
    public const string FieldContacts = "Contacts";

    /// <summary>
    /// 电话
    /// </summary>
    [NonSerialized]
    public const string FieldTel = "Tel";

    /// <summary>
    /// 手机
    /// </summary>
    [NonSerialized]
    public const string FieldMobile = "Mobile";

    /// <summary>
    /// 地址
    /// </summary>
    [NonSerialized]
    public const string FieldAddress = "Address";

    /// <summary>
    /// 传真
    /// </summary>
    [NonSerialized]
    public const string FieldFax = "Fax";

    /// <summary>
    /// 主营线路
    /// </summary>
    [NonSerialized]
    public const string FieldMainLine = "MainLine";

    /// <summary>
    /// 信誉
    /// </summary>
    [NonSerialized]
    public const string FieldTrustLevel = "TrustLevel";

    /// <summary>
    /// 法人
    /// </summary>
    [NonSerialized]
    public const string FieldLegal = "Legal";

    /// <summary>
    /// 税号
    /// </summary>
    [NonSerialized]
    public const string FieldTax = "Tax";

    /// <summary>
    /// 开户行
    /// </summary>
    [NonSerialized]
    public const string FieldBank = "Bank";

    /// <summary>
    /// 银行账号
    /// </summary>
    [NonSerialized]
    public const string FieldBankAccount = "BankAccount";

    /// <summary>
    /// 账期说明
    /// </summary>
    [NonSerialized]
    public const string FieldPaymentTerm = "PaymentTerm";

    /// <summary>
    /// 合同起始日期
    /// </summary>
    [NonSerialized]
    public const string FieldContractDate1 = "ContractDate1";

    /// <summary>
    /// 合同结束日期
    /// </summary>
    [NonSerialized]
    public const string FieldContractDate2 = "ContractDate2";

    /// <summary>
    /// 是否启用
    /// </summary>
    [NonSerialized]
    public const string FieldInUse = "InUse";

    /// <summary>
    /// 是否开票
    /// </summary>
    [NonSerialized]
    public const string FieldFlagInvoice = "FlagInvoice";

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
    /// 数据来源
    /// </summary>
    [NonSerialized]
    public const string FieldDataType = "DataType";

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
    /// 附件地址
    /// </summary>
    [NonSerialized]
    public const string FieldAttaPath = "AttaPath";

    /// <summary>
    /// 是否作废
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
    public const string FieldCancelUser = "CancelUser";

    #endregion

    #region 列显示名
    /// <summary>
    /// 自增ID
    /// </summary>
    [NonSerialized]
    public const string Disisid = "自增ID";

    /// <summary>
    /// 乐观锁
    /// </summary>
    [NonSerialized]
    public const string DisTS = "乐观锁";

    /// <summary>
    /// 网点名称
    /// </summary>
    [NonSerialized]
    public const string DisOrgCode = "网点名称";

    /// <summary>
    /// 承运商编号
    /// </summary>
    [NonSerialized]
    public const string DisLogisticCode = "承运商编号";

    /// <summary>
    /// 承运商名称
    /// </summary>
    [NonSerialized]
    public const string DisLogisticName = "承运商名称";

    /// <summary>
    /// 助记码
    /// </summary>
    [NonSerialized]
    public const string DisZJM = "助记码";

    /// <summary>
    /// 所属公司
    /// </summary>
    [NonSerialized]
    public const string DisStation = "所属公司";

    /// <summary>
    /// 联系人
    /// </summary>
    [NonSerialized]
    public const string DisContacts = "联系人";

    /// <summary>
    /// 电话
    /// </summary>
    [NonSerialized]
    public const string DisTel = "电话";

    /// <summary>
    /// 手机
    /// </summary>
    [NonSerialized]
    public const string DisMobile = "手机";

    /// <summary>
    /// 地址
    /// </summary>
    [NonSerialized]
    public const string DisAddress = "地址";

    /// <summary>
    /// 传真
    /// </summary>
    [NonSerialized]
    public const string DisFax = "传真";

    /// <summary>
    /// 主营线路
    /// </summary>
    [NonSerialized]
    public const string DisMainLine = "主营线路";

    /// <summary>
    /// 信誉
    /// </summary>
    [NonSerialized]
    public const string DisTrustLevel = "信誉";

    /// <summary>
    /// 法人
    /// </summary>
    [NonSerialized]
    public const string DisLegal = "法人";

    /// <summary>
    /// 税号
    /// </summary>
    [NonSerialized]
    public const string DisTax = "税号";

    /// <summary>
    /// 开户行
    /// </summary>
    [NonSerialized]
    public const string DisBank = "开户行";

    /// <summary>
    /// 银行账号
    /// </summary>
    [NonSerialized]
    public const string DisBankAccount = "银行账号";

    /// <summary>
    /// 账期说明
    /// </summary>
    [NonSerialized]
    public const string DisPaymentTerm = "账期说明";

    /// <summary>
    /// 合同起始日期
    /// </summary>
    [NonSerialized]
    public const string DisContractDate1 = "合同起始日期";

    /// <summary>
    /// 合同结束日期
    /// </summary>
    [NonSerialized]
    public const string DisContractDate2 = "合同结束日期";

    /// <summary>
    /// 是否启用
    /// </summary>
    [NonSerialized]
    public const string DisInUse = "是否启用";

    /// <summary>
    /// 是否开票
    /// </summary>
    [NonSerialized]
    public const string DisFlagInvoice = "是否开票";

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
    /// 数据来源
    /// </summary>
    [NonSerialized]
    public const string DisDataType = "数据来源";

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
    /// 附件地址
    /// </summary>
    [NonSerialized]
    public const string DisAttaPath = "附件地址";

    /// <summary>
    /// 是否作废
    /// </summary>
    [NonSerialized]
    public const string DisCancelYN = "是否作废";

    /// <summary>
    /// 作废时间
    /// </summary>
    [NonSerialized]
    public const string DisCancelDate = "作废时间";

    /// <summary>
    /// 作废人
    /// </summary>
    [NonSerialized]
    public const string DisCancelUser = "作废人";

    #endregion

    #endregion
}