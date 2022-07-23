using System.Runtime.Serialization;
using BB.Entity.Base;

namespace BB.Entity.Dictionary;

/// <summary>
/// 用户参数配置
/// </summary>
[DataContract]
[Table(DBTableName)]
public class UserParameterInfo : BaseEntity
{
    /// <summary>
    /// 默认构造函数（需要初始化属性的在此处理）
    /// </summary>
    public UserParameterInfo()
    {
        ID = Guid.NewGuid().ToString();
        CreationDate = DateTime.Now;
    }

    #region Property Members

    /// <summary>
    /// ID
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
    /// 参数文本内容
    /// </summary>
    [DataMember]
    [Column(FieldContent)]
    public virtual string Content { get; set; }

    /// <summary>
    /// 创建人
    /// </summary>
    [DataMember]
    [Column(FieldCreator)]
    public virtual string Creator { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    [DataMember]
    [Sort(IsDesc)]
    [Column(FieldCreationDate)]
    public virtual DateTime CreationDate { get; set; }

    #endregion

    #region 定义字段名称相关常量

    /// <summary>
    /// 表名
    /// </summary>
    [NonSerialized]
    public new const string DBTableName = "TB_UserParameter";

    /// <summary>
    /// 主键名
    /// </summary>
    [NonSerialized]
    public new const string PrimaryKey = FieldID;

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

    [NonSerialized]
    public const string FieldID = "ID";

    /// <summary>
    /// 类型名称
    /// </summary>
    [NonSerialized]
    public const string FieldName = "Name";

    /// <summary>
    /// 参数文本内容
    /// </summary>
    [NonSerialized]
    public const string FieldContent = "Content";

    /// <summary>
    /// 创建人
    /// </summary>
    [NonSerialized]
    public const string FieldCreator = "Creator";

    /// <summary>
    /// 创建时间
    /// </summary>
    [NonSerialized]
    public const string FieldCreationDate = "CreationDate";

    #endregion
}