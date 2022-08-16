using System.Runtime.Serialization;
using BB.Entity.Base;
using BB.Tools.Entity;

namespace BB.Entity.Dictionary;

/// <summary>
/// 字典数据
/// </summary>
[Serializable]
[DataContract]
[Table(DBTableName)]
public class DictDataInfo : BaseEntity
{
    /// <summary>
    /// 默认构造函数（需要初始化属性的在此处理）
    /// </summary>
    public DictDataInfo()
    {
        ID = Guid.NewGuid().ToString();
        LastUpdated = DateTime.Now;
    }

    #region Property Members

    /// <summary>
    /// 编号
    /// </summary>
    [DataMember]
    [Key]
    [Column(FieldID)]
    public virtual string ID { get; set; }

    /// <summary>
    /// 字典大类
    /// </summary>
    [DataMember]
    [Column(FieldDictTypeId)]
    public virtual string DictTypeId { get; set; }

    /// <summary>
    /// 字典名称
    /// </summary>
    [DataMember]
    [Column(FieldName)]
    public virtual string Name { get; set; }

    /// <summary>
    /// 字典值
    /// </summary>
    [DataMember]
    [Column(FieldValue)]
    public virtual string Value { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [DataMember]
    [Column(FieldRemark)]
    public virtual string Remark { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    [DataMember]
    [Sort(IsDesc)]
    [Column(FieldSEQ)]
    public virtual string SEQ { get; set; }

    /// <summary>
    /// 编辑者
    /// </summary>
    [DataMember]
    [Column(FieldEditor)]
    public virtual string Editor { get; set; }

    /// <summary>
    /// 编辑时间
    /// </summary>
    [DataMember]
    [OptimisticLock]
    [Column(FieldLastUpdated)]
    public virtual DateTime LastUpdated { get; set; }

    #endregion

    #region 定义字段名称相关常量

    /// <summary>
    /// 表名
    /// </summary>
    [NonSerialized]
    public new const string DBTableName = "TB_DictData";

    /// <summary>
    /// 主键名
    /// </summary>
    [NonSerialized]
    public new const string PrimaryKey = FieldID;

    /// <summary>
    /// 排序字段
    /// </summary>
    [NonSerialized]
    public new const string SortKey = FieldSEQ;

    /// <summary>
    /// 排序方式
    /// </summary>
    [NonSerialized]
    public new const bool IsDesc = false;

    /// <summary>
    /// 乐观锁字段
    /// </summary>
    [NonSerialized]
    public new const string OptimisticLockKey = FieldLastUpdated;

    /// <summary>
    /// 编号
    /// </summary>
    [NonSerialized]
    public const string FieldID = "ID";

    /// <summary>
    /// 字典大类
    /// </summary>
    [NonSerialized]
    public const string FieldDictTypeId = "DictType_ID";

    /// <summary>
    /// 字典名称
    /// </summary>
    [NonSerialized]
    public const string FieldName = "Name";

    /// <summary>
    /// 字典值
    /// </summary>
    [NonSerialized]
    public const string FieldValue = "Value";

    /// <summary>
    /// 备注
    /// </summary>
    [NonSerialized]
    public const string FieldRemark = "Remark";

    /// <summary>
    /// 排序
    /// </summary>
    [NonSerialized]
    public const string FieldSEQ = "Seq";

    /// <summary>
    /// 编辑者
    /// </summary>
    [NonSerialized]
    public const string FieldEditor = "Editor";

    /// <summary>
    /// 编辑时间
    /// </summary>
    [NonSerialized]
    public const string FieldLastUpdated = "LastUpdated";

    #endregion
}