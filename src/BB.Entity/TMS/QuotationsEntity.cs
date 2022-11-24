using System.Runtime.Serialization;
using BB.Entity.Base;
using BB.Tools.Entity;

namespace BB.Entity.TMS;

/// <summary>
/// 普通报价明细 实体类
/// </summary>
[DataContract]
[Serializable]
[Table(DBTableName)]
public sealed class Quotations : BaseEntity
{
    /// <summary>
    /// 默认构造函数（需要初始化属性的在此处理）
    /// </summary>
    public Quotations()
    {
        MinCost = 0;
        MaxCost = 999999;
        FirstCost = 0;
        FirstValue = 0;
        MinValue = 0;
        MaxValue = 99999;
        UnitPrice = 0;
        UnitPricePer = 1;
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
    /// 报价编号
    /// </summary>
    [DataMember]
    [Hide]
    [Column(FieldQuotationNo, DisQuotationNo)]
    public string QuotationNo { get; set; }

    /// <summary>
    /// 报价类型
    /// </summary>
    [DataMember]
    [Column(FieldQuotationType, DisQuotationType)]
    public string QuotationType { get; set; }

    /// <summary>
    /// 起始组
    /// </summary>
    [DataMember]
    [Column(FieldFromGroups, DisFromGroups)]
    public string FromGroups { get; set; }

    /// <summary>
    /// 到达组
    /// </summary>
    [DataMember]
    [Column(FieldToGroups, DisToGroups)]
    public string ToGroups { get; set; }

    /// <summary>
    /// 最小金额
    /// </summary>
    [DataMember]
    [Column(FieldMinCost, DisMinCost)]
    public decimal MinCost { get; set; }

    /// <summary>
    /// 最大金额
    /// </summary>
    [DataMember]
    [Column(FieldMaxCost, DisMaxCost)]
    public decimal MaxCost { get; set; }

    /// <summary>
    /// 首值金额
    /// </summary>
    [DataMember]
    [Column(FieldFirstCost, DisFirstCost)]
    public decimal FirstCost { get; set; }

    /// <summary>
    /// 首值
    /// </summary>
    [DataMember]
    [Column(FieldFirstValue, DisFirstValue)]
    public decimal FirstValue { get; set; }

    /// <summary>
    /// 最小值
    /// </summary>
    [DataMember]
    [Column(FieldMinValue, DisMinValue)]
    public decimal MinValue { get; set; }

    /// <summary>
    /// 最大值
    /// </summary>
    [DataMember]
    [Column(FieldMaxValue, DisMaxValue)]
    public decimal MaxValue { get; set; }

    /// <summary>
    /// 单价
    /// </summary>
    [DataMember]
    [Column(FieldUnitPrice, DisUnitPrice)]
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// 单价加成
    /// </summary>
    [DataMember]
    [Column(FieldUnitPricePer, DisUnitPricePer)]
    public decimal UnitPricePer { get; set; }

    /// <summary>
    /// 条件范围
    /// </summary>
    [DataMember]
    [Hide]
    [Column(FieldMathConditional, DisMathConditional)]
    public string MathConditional { get; set; }

    /// <summary>
    /// 条件公式
    /// </summary>
    [DataMember]
    [Hide]
    [Column(FieldMathContent, DisMathContent)]
    public string MathContent { get; set; }

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
    [Hide]
    [Column(FieldCreationDate, DisCreationDate)]
    public DateTime CreationDate { get; set; }

    /// <summary>
    /// 创建人
    /// </summary>
    [DataMember]
    [Hide]
    [Column(FieldCreatedBy, DisCreatedBy)]
    public string CreatedBy { get; set; }

    /// <summary>
    /// 修改时间
    /// </summary>
    [DataMember]
    [Hide]
    [OptimisticLock]
    [Column(FieldLastUpdateDate, DisLastUpdateDate)]
    public DateTime LastUpdateDate { get; set; }

    /// <summary>
    /// 修改人
    /// </summary>
    [DataMember]
    [Hide]
    [Column(FieldLastUpdatedBy, DisLastUpdatedBy)]
    public string LastUpdatedBy { get; set; }

    /// <summary>
    /// 起始组ID
    /// </summary>
    [DataMember]
    [Hide]
    [Column(FieldFromGroupsID, DisFromGroupsID)]
    public string FromGroupsID { get; set; }

    /// <summary>
    /// 到达组ID
    /// </summary>
    [DataMember]
    [Hide]
    [Column(FieldToGroupsID, DisToGroupsID)]
    public string ToGroupsID { get; set; }

    #endregion

    #region 定义字段名称相关常量

    /// <summary>
    /// 表名
    /// </summary>
    [NonSerialized]
    public new const string DBTableName = "tb_TranQuotations";

    /// <summary>
    /// 主键名
    /// </summary>
    [NonSerialized]
    public new const string PrimaryKey = FieldISID;

    /// <summary>
    /// 外键名
    /// </summary>
    [NonSerialized]
    public new const string ForeignKey = FieldQuotationNo;

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
    /// 报价编号
    /// </summary>
    [NonSerialized]
    public const string FieldQuotationNo = "QuotationNo";

    /// <summary>
    /// 报价类型
    /// </summary>
    [NonSerialized]
    public const string FieldQuotationType = "QuotationType";

    /// <summary>
    /// 起始组
    /// </summary>
    [NonSerialized]
    public const string FieldFromGroups = "FromGroups";

    /// <summary>
    /// 到达组
    /// </summary>
    [NonSerialized]
    public const string FieldToGroups = "ToGroups";

    /// <summary>
    /// 最小金额
    /// </summary>
    [NonSerialized]
    public const string FieldMinCost = "MinCost";

    /// <summary>
    /// 最大金额
    /// </summary>
    [NonSerialized]
    public const string FieldMaxCost = "MaxCost";

    /// <summary>
    /// 首值金额
    /// </summary>
    [NonSerialized]
    public const string FieldFirstCost = "FirstCost";

    /// <summary>
    /// 首值
    /// </summary>
    [NonSerialized]
    public const string FieldFirstValue = "FirstValue";

    /// <summary>
    /// 最小值
    /// </summary>
    [NonSerialized]
    public const string FieldMinValue = "MinValue";

    /// <summary>
    /// 最大值
    /// </summary>
    [NonSerialized]
    public const string FieldMaxValue = "MaxValue";

    /// <summary>
    /// 单价
    /// </summary>
    [NonSerialized]
    public const string FieldUnitPrice = "UnitPrice";

    /// <summary>
    /// 单价加成
    /// </summary>
    [NonSerialized]
    public const string FieldUnitPricePer = "UnitPricePer";

    /// <summary>
    /// 条件范围
    /// </summary>
    [NonSerialized]
    public const string FieldMathConditional = "MathConditional";

    /// <summary>
    /// 条件公式
    /// </summary>
    [NonSerialized]
    public const string FieldMathContent = "MathContent";

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
    /// 起始组ID
    /// </summary>
    [NonSerialized]
    public const string FieldFromGroupsID = "FromGroupsID";

    /// <summary>
    /// 到达组ID
    /// </summary>
    [NonSerialized]
    public const string FieldToGroupsID = "ToGroupsID";

    #endregion

    #region 列显示名
    /// <summary>
    /// 自增ID
    /// </summary>
    [NonSerialized]
    public const string DisISID = "自增ID";

    /// <summary>
    /// 报价编号
    /// </summary>
    [NonSerialized]
    public const string DisQuotationNo = "报价编号";

    /// <summary>
    /// 报价类型
    /// </summary>
    [NonSerialized]
    public const string DisQuotationType = "报价类型";

    /// <summary>
    /// 起始组
    /// </summary>
    [NonSerialized]
    public const string DisFromGroups = "起始组";

    /// <summary>
    /// 到达组
    /// </summary>
    [NonSerialized]
    public const string DisToGroups = "到达组";

    /// <summary>
    /// 最小金额
    /// </summary>
    [NonSerialized]
    public const string DisMinCost = "最小金额";

    /// <summary>
    /// 最大金额
    /// </summary>
    [NonSerialized]
    public const string DisMaxCost = "最大金额";

    /// <summary>
    /// 首值金额
    /// </summary>
    [NonSerialized]
    public const string DisFirstCost = "首值金额";

    /// <summary>
    /// 首值
    /// </summary>
    [NonSerialized]
    public const string DisFirstValue = "首值";

    /// <summary>
    /// 最小值
    /// </summary>
    [NonSerialized]
    public const string DisMinValue = "最小值";

    /// <summary>
    /// 最大值
    /// </summary>
    [NonSerialized]
    public const string DisMaxValue = "最大值";

    /// <summary>
    /// 单价
    /// </summary>
    [NonSerialized]
    public const string DisUnitPrice = "单价";

    /// <summary>
    /// 单价加成
    /// </summary>
    [NonSerialized]
    public const string DisUnitPricePer = "单价加成";

    /// <summary>
    /// 条件范围
    /// </summary>
    [NonSerialized]
    public const string DisMathConditional = "条件范围";

    /// <summary>
    /// 条件公式
    /// </summary>
    [NonSerialized]
    public const string DisMathContent = "条件公式";

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
    /// 起始组ID
    /// </summary>
    [NonSerialized]
    public const string DisFromGroupsID = "起始组ID";

    /// <summary>
    /// 到达组ID
    /// </summary>
    [NonSerialized]
    public const string DisToGroupsID = "到达组ID";

    #endregion

    #endregion
}