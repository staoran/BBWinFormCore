using System.Runtime.Serialization;
using BB.Entity.Base;
using BB.Tools.Entity;

namespace BB.Entity.Security;

/// <summary>
/// 系统标识信息
/// </summary>
[Serializable]
[DataContract]
[Table(DBTableName)]
public class SystemTypeInfo : BaseEntity
{
    /// <summary>
    /// 默认构造函数（需要初始化属性的在此处理）
    /// </summary>
    public SystemTypeInfo()
    {
        Oid = Guid.NewGuid().ToString(); //系统标识
    }

    #region Property Members

    /// <summary>
    /// 系统标识
    /// </summary>
    [DataMember]
    [Key]
    [Column(FieldOid)]
    public virtual string Oid { get; set; }

    /// <summary>
    /// 系统名称
    /// </summary>
    [DataMember]
    [Column(FieldName)]
    public virtual string Name { get; set; }

    /// <summary>
    /// 客户编码
    /// </summary>
    [DataMember]
    [Column(FieldCustomId)]
    public virtual string CustomId { get; set; }

    /// <summary>
    /// 授权编码
    /// </summary>
    [DataMember]
    [Column(FieldAuthorize)]
    public virtual string Authorize { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [DataMember]
    [Column(FieldNote)]
    public virtual string Note { get; set; }

    #endregion

    #region 定义字段名称相关常量

    /// <summary>
    /// 表名
    /// </summary>
    [NonSerialized]
    public new const string DBTableName = "T_ACL_SystemType";

    /// <summary>
    /// 主键名
    /// </summary>
    [NonSerialized]
    public new const string PrimaryKey = FieldOid;

    /// <summary>
    /// 系统标识
    /// </summary>
    [NonSerialized]
    public const string FieldOid = "OID";

    /// <summary>
    /// 系统名称
    /// </summary>
    [NonSerialized]
    public const string FieldName = "Name";

    /// <summary>
    /// 客户编码
    /// </summary>
    [NonSerialized]
    public const string FieldCustomId = "CustomID";

    /// <summary>
    /// 授权编码
    /// </summary>
    [NonSerialized]
    public const string FieldAuthorize = "Authorize";

    /// <summary>
    /// 备注
    /// </summary>
    [NonSerialized]
    public const string FieldNote = "Note";

    #endregion
}