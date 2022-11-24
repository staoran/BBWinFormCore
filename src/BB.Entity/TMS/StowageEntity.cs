using System.Runtime.Serialization;
using BB.Entity.Base;
using BB.Tools.Entity;
using SqlSugar;

namespace BB.Entity.TMS;

/// <summary>
/// 送货配载 实体类
/// </summary>
[DataContract]
[Serializable]
[Table(DBTableName)]
public sealed class Stowage : BaseEntity<Stowages>
{
    /// <summary>
    /// 默认构造函数（需要初始化属性的在此处理，复杂的属性值初始化通过关键字替换或重写 SetDynamicDefaults 方法实现）
    /// </summary>
    public Stowage()
    {
        StowageNo = "*自动生成*";
        TranNodeNO = "*当前机构*";
        StowageType = "01";
        TransType = "01";
        TransDate = DateTime.Now;
        TotalQty = 0;
        TotalWeight = 0;
        TotalCubage = 0;
        TotalCarriage = 0;
        TransCarriage = 0;
        CheckInYN = false;
        CreationDate = DateTime.Now;
        CreatedBy = "*当前用户*";
        LastUpdateDate = DateTime.Now;
        LastUpdatedBy = "*当前用户*";
        FlagApp = false;
        Income = 0;
    }

    #region Property Members

    /// <summary>
    /// 配载编号
    /// </summary>
    [DataMember]
    [Key]
    [Column(FieldStowageNo, DisStowageNo)]
    public string StowageNo { get; set; }

    /// <summary>
    /// 网点编号
    /// </summary>
    [DataMember]
    [Column(FieldTranNodeNO, DisTranNodeNO)]
    public string TranNodeNO { get; set; }

    /// <summary>
    /// 配载单类型
    /// </summary>
    [DataMember]
    [Column(FieldStowageType, DisStowageType)]
    public string StowageType { get; set; }

    /// <summary>
    /// 配载运输类型
    /// </summary>
    [DataMember]
    [Column(FieldTransType, DisTransType)]
    public string TransType { get; set; }

    /// <summary>
    /// 配载车标号
    /// </summary>
    [DataMember]
    [Column(FieldTransMarkNo, DisTransMarkNo)]
    public string TransMarkNo { get; set; }

    /// <summary>
    /// 车辆编号
    /// </summary>
    [DataMember]
    [Column(FieldTransNo, DisTransNo)]
    public string TransNo { get; set; }

    /// <summary>
    /// 司机
    /// </summary>
    [DataMember]
    [Column(FieldTransDriver, DisTransDriver)]
    public string TransDriver { get; set; }

    /// <summary>
    /// 司机电话
    /// </summary>
    [DataMember]
    [Column(FieldTransDriverPhone, DisTransDriverPhone)]
    public string TransDriverPhone { get; set; }

    /// <summary>
    /// 承运时间
    /// </summary>
    [DataMember]
    [Column(FieldTransDate, DisTransDate)]
    public DateTime TransDate { get; set; }

    /// <summary>
    /// 总件数
    /// </summary>
    [DataMember]
    [Column(FieldTotalQty, DisTotalQty)]
    public int TotalQty { get; set; }

    /// <summary>
    /// 总重量
    /// </summary>
    [DataMember]
    [Column(FieldTotalWeight, DisTotalWeight)]
    public decimal TotalWeight { get; set; }

    /// <summary>
    /// 总体积
    /// </summary>
    [DataMember]
    [Column(FieldTotalCubage, DisTotalCubage)]
    public decimal TotalCubage { get; set; }

    /// <summary>
    /// 总运费
    /// </summary>
    [DataMember]
    [Column(FieldTotalCarriage, DisTotalCarriage)]
    public decimal TotalCarriage { get; set; }

    /// <summary>
    /// 支付费用
    /// </summary>
    [DataMember]
    [Column(FieldTransCarriage, DisTransCarriage)]
    public decimal TransCarriage { get; set; }

    /// <summary>
    /// 核销
    /// </summary>
    [DataMember]
    [Column(FieldCheckInYN, DisCheckInYN)]
    public bool CheckInYN { get; set; }

    /// <summary>
    /// 核销人
    /// </summary>
    [DataMember]
    [Column(FieldCheckInAccount, DisCheckInAccount)]
    public string CheckInAccount { get; set; }

    /// <summary>
    /// 核销时间
    /// </summary>
    [DataMember]
    [Column(FieldCheckInDate, DisCheckInDate)]
    public DateTime? CheckInDate { get; set; }

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
    /// 收入
    /// </summary>
    [DataMember]
    [Column(FieldIncome, DisIncome)]
    public decimal Income { get; set; }

    /// <summary>
    /// 分摊类型
    /// </summary>
    [DataMember]
    [Column(FieldSharType, DisSharType)]
    public string SharType { get; set; }

    /// <summary>
    /// 配载明细表 集合
    /// </summary>
    [Ignore]
    public IEnumerable<Stowages> StowagesList { get; set; }

    #endregion

    #region 定义字段名称相关常量

    /// <summary>
    /// 表名
    /// </summary>
    [NonSerialized]
    public new const string DBTableName = "tb_TranStowage";

    /// <summary>
    /// 主键名
    /// </summary>
    [NonSerialized]
    public new const string PrimaryKey = FieldStowageNo;

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
    public new List<Stowages>? ChildTableList
    {
        get => base.ChildTableList;
        set => base.ChildTableList = value;
    }

    /// <summary>
    /// 子表外键
    /// </summary>
    [NonSerialized]
    public new const string ChildForeignKey = Stowages.ForeignKey;

    #region 列名
    /// <summary>
    /// 配载编号
    /// </summary>
    [NonSerialized]
    public const string FieldStowageNo = "StowageNo";

    /// <summary>
    /// 网点编号
    /// </summary>
    [NonSerialized]
    public const string FieldTranNodeNO = "TranNodeNO";

    /// <summary>
    /// 配载单类型
    /// </summary>
    [NonSerialized]
    public const string FieldStowageType = "StowageType";

    /// <summary>
    /// 配载运输类型
    /// </summary>
    [NonSerialized]
    public const string FieldTransType = "TransType";

    /// <summary>
    /// 配载车标号
    /// </summary>
    [NonSerialized]
    public const string FieldTransMarkNo = "TransMarkNo";

    /// <summary>
    /// 车辆编号
    /// </summary>
    [NonSerialized]
    public const string FieldTransNo = "TransNo";

    /// <summary>
    /// 司机
    /// </summary>
    [NonSerialized]
    public const string FieldTransDriver = "TransDriver";

    /// <summary>
    /// 司机电话
    /// </summary>
    [NonSerialized]
    public const string FieldTransDriverPhone = "TransDriverPhone";

    /// <summary>
    /// 承运时间
    /// </summary>
    [NonSerialized]
    public const string FieldTransDate = "TransDate";

    /// <summary>
    /// 总件数
    /// </summary>
    [NonSerialized]
    public const string FieldTotalQty = "TotalQty";

    /// <summary>
    /// 总重量
    /// </summary>
    [NonSerialized]
    public const string FieldTotalWeight = "TotalWeight";

    /// <summary>
    /// 总体积
    /// </summary>
    [NonSerialized]
    public const string FieldTotalCubage = "TotalCubage";

    /// <summary>
    /// 总运费
    /// </summary>
    [NonSerialized]
    public const string FieldTotalCarriage = "TotalCarriage";

    /// <summary>
    /// 支付费用
    /// </summary>
    [NonSerialized]
    public const string FieldTransCarriage = "TransCarriage";

    /// <summary>
    /// 核销
    /// </summary>
    [NonSerialized]
    public const string FieldCheckInYN = "CheckInYN";

    /// <summary>
    /// 核销人
    /// </summary>
    [NonSerialized]
    public const string FieldCheckInAccount = "CheckInAccount";

    /// <summary>
    /// 核销时间
    /// </summary>
    [NonSerialized]
    public const string FieldCheckInDate = "CheckInDate";

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

    /// <summary>
    /// 收入
    /// </summary>
    [NonSerialized]
    public const string FieldIncome = "Income";

    /// <summary>
    /// 分摊类型
    /// </summary>
    [NonSerialized]
    public const string FieldSharType = "SharType";

    #endregion

    #region 列显示名
    /// <summary>
    /// 配载编号
    /// </summary>
    [NonSerialized]
    public const string DisStowageNo = "配载编号";

    /// <summary>
    /// 网点编号
    /// </summary>
    [NonSerialized]
    public const string DisTranNodeNO = "网点编号";

    /// <summary>
    /// 配载单类型
    /// </summary>
    [NonSerialized]
    public const string DisStowageType = "配载单类型";

    /// <summary>
    /// 配载运输类型
    /// </summary>
    [NonSerialized]
    public const string DisTransType = "配载运输类型";

    /// <summary>
    /// 配载车标号
    /// </summary>
    [NonSerialized]
    public const string DisTransMarkNo = "配载车标号";

    /// <summary>
    /// 车辆编号
    /// </summary>
    [NonSerialized]
    public const string DisTransNo = "车辆编号";

    /// <summary>
    /// 司机
    /// </summary>
    [NonSerialized]
    public const string DisTransDriver = "司机";

    /// <summary>
    /// 司机电话
    /// </summary>
    [NonSerialized]
    public const string DisTransDriverPhone = "司机电话";

    /// <summary>
    /// 承运时间
    /// </summary>
    [NonSerialized]
    public const string DisTransDate = "承运时间";

    /// <summary>
    /// 总件数
    /// </summary>
    [NonSerialized]
    public const string DisTotalQty = "总件数";

    /// <summary>
    /// 总重量
    /// </summary>
    [NonSerialized]
    public const string DisTotalWeight = "总重量";

    /// <summary>
    /// 总体积
    /// </summary>
    [NonSerialized]
    public const string DisTotalCubage = "总体积";

    /// <summary>
    /// 总运费
    /// </summary>
    [NonSerialized]
    public const string DisTotalCarriage = "总运费";

    /// <summary>
    /// 支付费用
    /// </summary>
    [NonSerialized]
    public const string DisTransCarriage = "支付费用";

    /// <summary>
    /// 核销
    /// </summary>
    [NonSerialized]
    public const string DisCheckInYN = "核销";

    /// <summary>
    /// 核销人
    /// </summary>
    [NonSerialized]
    public const string DisCheckInAccount = "核销人";

    /// <summary>
    /// 核销时间
    /// </summary>
    [NonSerialized]
    public const string DisCheckInDate = "核销时间";

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

    /// <summary>
    /// 收入
    /// </summary>
    [NonSerialized]
    public const string DisIncome = "收入";

    /// <summary>
    /// 分摊类型
    /// </summary>
    [NonSerialized]
    public const string DisSharType = "分摊类型";

    #endregion

    #endregion
}