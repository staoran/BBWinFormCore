using System.Runtime.Serialization;
using BB.Entity.Base;

namespace BB.Entity.TMS;

/// <summary>
/// 公式定义 实体类
/// </summary>
[DataContract]
[Serializable]
[Table(DBTableName)]
public sealed class BasicTranslateWords : BaseEntity
{
    /// <summary>
    /// 默认构造函数（需要初始化属性的在此处理，复杂的属性值初始化通过重写 _bll.NewEntity 方法实现）
    /// </summary>
    public BasicTranslateWords()
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
    /// 关键字
    /// </summary>
    [DataMember]
    [Column(FieldWordsInFront, DisWordsInFront)]
    public string WordsInFront { get; set; }

    /// <summary>
    /// 代码
    /// </summary>
    [DataMember]
    [Column(FieldWordsBehind, DisWordsBehind)]
    public string WordsBehind { get; set; }

    /// <summary>
    /// 代码类型
    /// </summary>
    [DataMember]
    [Column(FieldTranslateType, DisTranslateType)]
    public string TranslateType { get; set; }

    /// <summary>
    /// 代码类型
    /// </summary>
    [DataMember]
    [Hide]
    [Column(FieldTranslateTypeDesc, DisTranslateTypeDesc)]
    public string TranslateTypeDesc { get; set; }

    /// <summary>
    /// 可选
    /// </summary>
    [DataMember]
    [Column(FieldCanSelectYN, DisCanSelectYN)]
    public bool CanSelectYN { get; set; }

    /// <summary>
    /// 说明
    /// </summary>
    [DataMember]
    [Column(FieldExampleStr, DisExampleStr)]
    public string ExampleStr { get; set; }

    /// <summary>
    /// 禁用
    /// </summary>
    [DataMember]
    [Column(FieldCancelYN, DisCancelYN)]
    public bool CancelYN { get; set; }

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
    public new const string DBTableName = "tb_BasicTranslateWords";

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
    /// 关键字
    /// </summary>
    [NonSerialized]
    public const string FieldWordsInFront = "WordsInFront";

    /// <summary>
    /// 代码
    /// </summary>
    [NonSerialized]
    public const string FieldWordsBehind = "WordsBehind";

    /// <summary>
    /// 代码类型编号
    /// </summary>
    [NonSerialized]
    public const string FieldTranslateType = "TranslateType";

    /// <summary>
    /// 代码类型
    /// </summary>
    [NonSerialized]
    public const string FieldTranslateTypeDesc = "TranslateTypeDesc";

    /// <summary>
    /// 是否可选
    /// </summary>
    [NonSerialized]
    public const string FieldCanSelectYN = "CanSelectYN";

    /// <summary>
    /// 说明
    /// </summary>
    [NonSerialized]
    public const string FieldExampleStr = "ExampleStr";

    /// <summary>
    /// 是否禁用
    /// </summary>
    [NonSerialized]
    public const string FieldCancelYN = "CancelYN";

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

    #endregion

    #region 列显示名
    /// <summary>
    /// 自增ID
    /// </summary>
    [NonSerialized]
    public const string DisISID = "自增ID";

    /// <summary>
    /// 关键字
    /// </summary>
    [NonSerialized]
    public const string DisWordsInFront = "关键字";

    /// <summary>
    /// 代码
    /// </summary>
    [NonSerialized]
    public const string DisWordsBehind = "代码";

    /// <summary>
    /// 代码类型编号
    /// </summary>
    [NonSerialized]
    public const string DisTranslateType = "代码类型编号";

    /// <summary>
    /// 代码类型
    /// </summary>
    [NonSerialized]
    public const string DisTranslateTypeDesc = "代码类型";

    /// <summary>
    /// 是否可选
    /// </summary>
    [NonSerialized]
    public const string DisCanSelectYN = "是否可选";

    /// <summary>
    /// 说明
    /// </summary>
    [NonSerialized]
    public const string DisExampleStr = "说明";

    /// <summary>
    /// 是否禁用
    /// </summary>
    [NonSerialized]
    public const string DisCancelYN = "是否禁用";

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

    #endregion

    #endregion
}