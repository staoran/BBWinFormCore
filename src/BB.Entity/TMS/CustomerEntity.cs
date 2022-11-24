using System.Runtime.Serialization;
using BB.Entity.Base;
using BB.Tools.Entity;
using SqlSugar;

namespace BB.Entity.TMS;

/// <summary>
/// 客户资料 实体类
/// </summary>
[DataContract]
[Serializable]
[Table(DBTableName)]
public sealed class Customer : BaseEntity<Customers>
{
    /// <summary>
    /// 默认构造函数（需要初始化属性的在此处理，复杂的属性值初始化通过关键字替换或重写 SetDynamicDefaults 方法实现）
    /// </summary>
    public Customer()
    {
        CustomerCode = "*自动生成*";
        TranNode = "*当前机构*";
        InUse = true;
        FlagInvoice = false;
        CreationDate = DateTime.Now;
        CreatedBy = "*当前用户*";
        LastUpdateDate = DateTime.Now;
        LastUpdatedBy = "*当前用户*";
        FlagApp = false;
    }

    #region Property Members

    /// <summary>
    /// 公司编号
    /// </summary>
    [DataMember]
    [Key]
    [Column(FieldCustomerCode, DisCustomerCode)]
    public string CustomerCode { get; set; }

    /// <summary>
    /// 助记码
    /// </summary>
    [DataMember]
    [Column(FieldMnemonicCode, DisMnemonicCode)]
    public string MnemonicCode { get; set; }

    /// <summary>
    /// 网点编号
    /// </summary>
    [DataMember]
    [Column(FieldTranNode, DisTranNode)]
    public string TranNode { get; set; }

    /// <summary>
    /// 联系人
    /// </summary>
    [DataMember]
    [Column(FieldContactPerson, DisContactPerson)]
    public string ContactPerson { get; set; }

    /// <summary>
    /// 公司名称
    /// </summary>
    [DataMember]
    [Column(FieldNativeName, DisNativeName)]
    public string NativeName { get; set; }

    /// <summary>
    /// 地址
    /// </summary>
    [DataMember]
    [Column(FieldAddress, DisAddress)]
    public string Address { get; set; }

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
    /// 电话号码
    /// </summary>
    [DataMember]
    [Column(FieldTel, DisTel)]
    public string Tel { get; set; }

    /// <summary>
    /// 手机号
    /// </summary>
    [DataMember]
    [Column(FieldMobile, DisMobile)]
    public string Mobile { get; set; }

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
    /// 备注
    /// </summary>
    [DataMember]
    [Column(FieldRemark, DisRemark)]
    public string Remark { get; set; }

    /// <summary>
    /// 是否在用(Y/N）
    /// </summary>
    [DataMember]
    [Column(FieldInUse, DisInUse)]
    public bool InUse { get; set; }

    /// <summary>
    /// 付款方式
    /// </summary>
    [DataMember]
    [Column(FieldPaymentType, DisPaymentType)]
    public string PaymentType { get; set; }

    /// <summary>
    /// 税率
    /// </summary>
    [DataMember]
    [Column(FieldInvoiceFax, DisInvoiceFax)]
    public decimal InvoiceFax { get; set; }

    /// <summary>
    /// 业务员提成方式
    /// </summary>
    [DataMember]
    [Column(FieldCommissionType, DisCommissionType)]
    public string CommissionType { get; set; }

    /// <summary>
    /// 提成比例
    /// </summary>
    [DataMember]
    [Column(FieldCommissionRate, DisCommissionRate)]
    public decimal CommissionRate { get; set; }

    /// <summary>
    /// 客服
    /// </summary>
    [DataMember]
    [Column(FieldSalesDeputy, DisSalesDeputy)]
    public string SalesDeputy { get; set; }

    /// <summary>
    /// 项目主管
    /// </summary>
    [DataMember]
    [Column(FieldProjectManager, DisProjectManager)]
    public string ProjectManager { get; set; }

    /// <summary>
    /// 开票
    /// </summary>
    [DataMember]
    [Column(FieldFlagInvoice, DisFlagInvoice)]
    public bool FlagInvoice { get; set; }

    /// <summary>
    /// 业务员
    /// </summary>
    [DataMember]
    [Column(FieldSalesPerson, DisSalesPerson)]
    public string SalesPerson { get; set; }

    /// <summary>
    /// 创建日期
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
    /// 更新日期
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
    /// 审核日期
    /// </summary>
    [DataMember]
    [Column(FieldAppDate, DisAppDate)]
    public DateTime? AppDate { get; set; }

    /// <summary>
    /// 客户收货人 集合
    /// </summary>
    [Ignore]
    public IEnumerable<Customers> CustomersList { get; set; }

    #endregion

    #region 定义字段名称相关常量

    /// <summary>
    /// 表名
    /// </summary>
    [NonSerialized]
    public new const string DBTableName = "tb_TranCustomer";

    /// <summary>
    /// 主键名
    /// </summary>
    [NonSerialized]
    public new const string PrimaryKey = FieldCustomerCode;

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

    /// <summary>
    /// 子表数据
    /// </summary>
    [DataMember]
    [Ignore]
    [Navigate(NavigateType.OneToMany, ChildForeignKey)]
    public new List<Customers>? ChildTableList
    {
        get => base.ChildTableList;
        set => base.ChildTableList = value;
    }

    /// <summary>
    /// 子表外键
    /// </summary>
    [NonSerialized]
    public new const string ChildForeignKey = Customers.ForeignKey;

    #region 列名
    /// <summary>
    /// 公司编号
    /// </summary>
    [NonSerialized]
    public const string FieldCustomerCode = "CustomerCode";

    /// <summary>
    /// 助记码
    /// </summary>
    [NonSerialized]
    public const string FieldMnemonicCode = "MnemonicCode";

    /// <summary>
    /// 网点编号
    /// </summary>
    [NonSerialized]
    public const string FieldTranNode = "TranNode";

    /// <summary>
    /// 联系人
    /// </summary>
    [NonSerialized]
    public const string FieldContactPerson = "ContactPerson";

    /// <summary>
    /// 公司名称
    /// </summary>
    [NonSerialized]
    public const string FieldNativeName = "NativeName";

    /// <summary>
    /// 地址
    /// </summary>
    [NonSerialized]
    public const string FieldAddress = "Address";

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
    /// 电话号码
    /// </summary>
    [NonSerialized]
    public const string FieldTel = "Tel";

    /// <summary>
    /// 手机号
    /// </summary>
    [NonSerialized]
    public const string FieldMobile = "Mobile";

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
    /// 备注
    /// </summary>
    [NonSerialized]
    public const string FieldRemark = "Remark";

    /// <summary>
    /// 是否在用
    /// </summary>
    [NonSerialized]
    public const string FieldInUse = "InUse";

    /// <summary>
    /// 付款方式
    /// </summary>
    [NonSerialized]
    public const string FieldPaymentType = "PaymentType";

    /// <summary>
    /// 税率
    /// </summary>
    [NonSerialized]
    public const string FieldInvoiceFax = "InvoiceFax";

    /// <summary>
    /// 业务员提成方式
    /// </summary>
    [NonSerialized]
    public const string FieldCommissionType = "CommissionType";

    /// <summary>
    /// 提成比例
    /// </summary>
    [NonSerialized]
    public const string FieldCommissionRate = "CommissionRate";

    /// <summary>
    /// 客服
    /// </summary>
    [NonSerialized]
    public const string FieldSalesDeputy = "SalesDeputy";

    /// <summary>
    /// 项目主管
    /// </summary>
    [NonSerialized]
    public const string FieldProjectManager = "ProjectManager";

    /// <summary>
    /// 开票
    /// </summary>
    [NonSerialized]
    public const string FieldFlagInvoice = "FlagInvoice";

    /// <summary>
    /// 业务员
    /// </summary>
    [NonSerialized]
    public const string FieldSalesPerson = "SalesPerson";

    /// <summary>
    /// 创建日期
    /// </summary>
    [NonSerialized]
    public const string FieldCreationDate = "CreationDate";

    /// <summary>
    /// 创建人
    /// </summary>
    [NonSerialized]
    public const string FieldCreatedBy = "CreatedBy";

    /// <summary>
    /// 更新日期
    /// </summary>
    [NonSerialized]
    public const string FieldLastUpdateDate = "LastUpdateDate";

    /// <summary>
    /// 更新人
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
    /// 审核日期
    /// </summary>
    [NonSerialized]
    public const string FieldAppDate = "AppDate";

    #endregion

    #region 列显示名
    /// <summary>
    /// 公司编号
    /// </summary>
    [NonSerialized]
    public const string DisCustomerCode = "公司编号";

    /// <summary>
    /// 助记码
    /// </summary>
    [NonSerialized]
    public const string DisMnemonicCode = "助记码";

    /// <summary>
    /// 网点编号
    /// </summary>
    [NonSerialized]
    public const string DisTranNode = "网点编号";

    /// <summary>
    /// 联系人
    /// </summary>
    [NonSerialized]
    public const string DisContactPerson = "联系人";

    /// <summary>
    /// 公司名称
    /// </summary>
    [NonSerialized]
    public const string DisNativeName = "公司名称";

    /// <summary>
    /// 地址
    /// </summary>
    [NonSerialized]
    public const string DisAddress = "地址";

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
    /// 电话号码
    /// </summary>
    [NonSerialized]
    public const string DisTel = "电话号码";

    /// <summary>
    /// 手机号
    /// </summary>
    [NonSerialized]
    public const string DisMobile = "手机号";

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
    /// 备注
    /// </summary>
    [NonSerialized]
    public const string DisRemark = "备注";

    /// <summary>
    /// 是否在用
    /// </summary>
    [NonSerialized]
    public const string DisInUse = "是否在用";

    /// <summary>
    /// 付款方式
    /// </summary>
    [NonSerialized]
    public const string DisPaymentType = "付款方式";

    /// <summary>
    /// 税率
    /// </summary>
    [NonSerialized]
    public const string DisInvoiceFax = "税率";

    /// <summary>
    /// 业务员提成方式
    /// </summary>
    [NonSerialized]
    public const string DisCommissionType = "业务员提成方式";

    /// <summary>
    /// 提成比例
    /// </summary>
    [NonSerialized]
    public const string DisCommissionRate = "提成比例";

    /// <summary>
    /// 客服
    /// </summary>
    [NonSerialized]
    public const string DisSalesDeputy = "客服";

    /// <summary>
    /// 项目主管
    /// </summary>
    [NonSerialized]
    public const string DisProjectManager = "项目主管";

    /// <summary>
    /// 开票
    /// </summary>
    [NonSerialized]
    public const string DisFlagInvoice = "开票";

    /// <summary>
    /// 业务员
    /// </summary>
    [NonSerialized]
    public const string DisSalesPerson = "业务员";

    /// <summary>
    /// 创建日期
    /// </summary>
    [NonSerialized]
    public const string DisCreationDate = "创建日期";

    /// <summary>
    /// 创建人
    /// </summary>
    [NonSerialized]
    public const string DisCreatedBy = "创建人";

    /// <summary>
    /// 更新日期
    /// </summary>
    [NonSerialized]
    public const string DisLastUpdateDate = "更新日期";

    /// <summary>
    /// 更新人
    /// </summary>
    [NonSerialized]
    public const string DisLastUpdatedBy = "更新人";

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
    /// 审核日期
    /// </summary>
    [NonSerialized]
    public const string DisAppDate = "审核日期";

    #endregion

    #endregion
}