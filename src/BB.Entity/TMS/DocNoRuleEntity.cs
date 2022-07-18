using System.Runtime.Serialization;
using BB.Entity.Base;

namespace BB.Entity.TMS;

/// <summary>
/// 单号规则 实体类
/// </summary>
[DataContract]
[Serializable]
[Table(DBTableName)]
public class DocNoRule : BaseEntity
{
    /// <summary>
    /// 默认构造函数（需要初始化属性的在此处理，复杂的属性值初始化通过重写 _bll.NewEntity 方法实现）
    /// </summary>
    public DocNoRule()
    {
    }

    #region Property Members

    /// <summary>
    /// 自增ID
    /// </summary>
    [DataMember]
    [Key]
    [Hide]
    [Identity]
    [Sort(IsDesc)]
    [Column(FieldISID, DisISID)]
    public int ISID { get; set; }

    /// <summary>
    /// 单据字头
    /// </summary>
    [DataMember]
    [Column(FieldDocCode, DisDocCode)]
    public string DocCode { get; set; }

    /// <summary>
    /// 编码规则
    /// </summary>
    [DataMember]
    [Column(FieldRuleFormat, DisRuleFormat)]
    public string RuleFormat { get; set; }

    /// <summary>
    /// 自增数长度
    /// </summary>
    [DataMember]
    [Column(FieldNoLength, DisNoLength)]
    public int NoLength { get; set; }

    /// <summary>
    /// 自动归零
    /// </summary>
    [DataMember]
    [Column(FieldResetZero, DisResetZero)]
    public bool ResetZero { get; set; }

    /// <summary>
    /// 加间隔符
    /// </summary>
    [DataMember]
    [Column(FieldFlagSpilitNo, DisFlagSpilitNo)]
    public bool FlagSpilitNo { get; set; }

    /// <summary>
    /// 加单据字头
    /// </summary>
    [DataMember]
    [Column(FieldFlagIncludeDocCode, DisFlagIncludeDocCode)]
    public bool FlagIncludeDocCode { get; set; }

    /// <summary>
    /// 末尾加毫秒
    /// </summary>
    [DataMember]
    [Column(FieldFlagLastMillisecond, DisFlagLastMillisecond)]
    public bool FlagLastMillisecond { get; set; }

    /// <summary>
    /// 当前自增数
    /// </summary>
    [DataMember]
    [Column(FieldCurrentValue, DisCurrentValue)]
    [Ignore]
    public int CurrentValue { get; set; }

    /// <summary>
    /// 当前年月日
    /// </summary>
    [DataMember]
    [Column(FieldCurrentYMD, DisCurrentYMD)]
    [Ignore]
    public int CurrentYMD { get; set; }

    /// <summary>
    /// 创建日期
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

    #endregion

    #region 定义字段名称相关常量

    /// <summary>
    /// 表名
    /// </summary>
    [NonSerialized]
    public new const string DBTableName = "sys_DocNoRule";

    /// <summary>
    /// 主键名
    /// </summary>
    [NonSerialized]
    public new const string PrimaryKey = FieldISID;

    /// <summary>
    /// 排序字段
    /// </summary>
    [NonSerialized]
    public new const string SortKey = FieldISID;

    /// <summary>
    /// 排序方式
    /// </summary>
    [NonSerialized]
    public new const bool IsDesc = false;

    #region 列名

    /// <summary>
    /// 自增ID
    /// </summary>
    [NonSerialized]
    public const string FieldISID = "ISID";

    /// <summary>
    /// 单据字头
    /// </summary>
    [NonSerialized]
    public const string FieldDocCode = "DocCode";

    /// <summary>
    /// 编码规则
    /// </summary>
    [NonSerialized]
    public const string FieldRuleFormat = "RuleFormat";

    /// <summary>
    /// 自增数长度
    /// </summary>
    [NonSerialized]
    public const string FieldNoLength = "NoLength";

    /// <summary>
    /// 自动归零
    /// </summary>
    [NonSerialized]
    public const string FieldResetZero = "ResetZero";

    /// <summary>
    /// 加间隔符
    /// </summary>
    [NonSerialized]
    public const string FieldFlagSpilitNo = "FlagSpilitNo";

    /// <summary>
    /// 加单据字头
    /// </summary>
    [NonSerialized]
    public const string FieldFlagIncludeDocCode = "FlagIncludeDocCode";

    /// <summary>
    /// 末尾加毫秒
    /// </summary>
    [NonSerialized]
    public const string FieldFlagLastMillisecond = "FlagLastMillisecond";

    /// <summary>
    /// 当前自增数
    /// </summary>
    [NonSerialized]
    public const string FieldCurrentValue = "CurrentValue";

    /// <summary>
    /// 当前年月日
    /// </summary>
    [NonSerialized]
    public const string FieldCurrentYMD = "CurrentYMD";

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

    #endregion

    #region 列显示名

    /// <summary>
    /// 自增ID
    /// </summary>
    [NonSerialized]
    public const string DisISID = "自增ID";

    /// <summary>
    /// 单据字头
    /// </summary>
    [NonSerialized]
    public const string DisDocCode = "单据字头";

    /// <summary>
    /// 编码规则
    /// </summary>
    [NonSerialized]
    public const string DisRuleFormat = "编码规则";

    /// <summary>
    /// 自增数长度
    /// </summary>
    [NonSerialized]
    public const string DisNoLength = "自增数长度";

    /// <summary>
    /// 自动归零
    /// </summary>
    [NonSerialized]
    public const string DisResetZero = "自动归零";

    /// <summary>
    /// 加单据字头
    /// </summary>
    [NonSerialized]
    public const string DisFlagSpilitNo = "加单据字头";

    /// <summary>
    /// 加间隔符
    /// </summary>
    [NonSerialized]
    public const string DisFlagIncludeDocCode = "加间隔符";

    /// <summary>
    /// 末尾加毫秒
    /// </summary>
    [NonSerialized]
    public const string DisFlagLastMillisecond = "末尾加毫秒";

    /// <summary>
    /// 当前自增数
    /// </summary>
    [NonSerialized]
    public const string DisCurrentValue = "当前自增数";

    /// <summary>
    /// 当前年月日
    /// </summary>
    [NonSerialized]
    public const string DisCurrentYMD = "当前年月日";

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

    #endregion

    #endregion
}