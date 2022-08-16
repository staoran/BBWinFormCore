using System.Runtime.Serialization;
using BB.Entity.Base;
using BB.Tools.Entity;

namespace BB.Entity.Dictionary;

/// <summary>
/// 字典类型
/// </summary>
[Serializable]
[DataContract]
[Table(DBTableName)]
public class DictTypeInfo : BaseEntity
{
    /// <summary>
    /// 默认构造函数（需要初始化属性的在此处理）
    /// </summary>
    public DictTypeInfo()
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
    /// 类型名称
    /// </summary>
    [DataMember]
    [Column(FieldName)]
    public virtual string Name { get; set; }

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

    /// <summary>
    /// 字典父类
    /// </summary>
    [DataMember]
    [Column(FieldPID)]
    public virtual string PID { get; set; }

    /// <summary>
    /// 类型编号
    /// </summary>
    [DataMember]
    [Column(FieldCode)]
    public virtual string Code { get; set; }

    /// <summary>
    /// SQL数据源
    /// </summary>
    [DataMember]
    [Column(FieldDbSQL)]
    public virtual string DbSQL { get; set; }

    #endregion

    #region 定义字段名称相关常量

    /// <summary>
    /// 表名
    /// </summary>
    [NonSerialized]
    public new const string DBTableName = "TB_DictType";

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

    [NonSerialized]
    public const string FieldID = "ID";

    /// <summary>
    /// 类型名称
    /// </summary>
    [NonSerialized]
    public const string FieldName = "Name";

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

    /// <summary>
    /// 父类ID
    /// </summary>
    [NonSerialized]
    public const string FieldPID = "PID";

    /// <summary>
    /// 类型编号
    /// </summary>
    [NonSerialized]
    public const string FieldCode = "Code";

    /// <summary>
    /// SQL语句
    /// </summary>
    [NonSerialized]
    public const string FieldDbSQL = "DbSQL";

    #endregion
}