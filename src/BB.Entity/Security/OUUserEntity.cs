using System.Runtime.Serialization;
using BB.Entity.Base;

namespace BB.Entity.Security;

[DataContract]
[Serializable]
[Table(DBTableName)]
public class OUUserEntity : BaseEntity
{
    [Key]
    [DataMember]
    [Column(FieldUserId, "用户编号")]
    public int UserId { get; set; }

    [Key]
    [DataMember]
    [Column(FieldOUId, "机构编号")]
    public string OUId { get; set; }
    
    /// <summary>
    /// 表名
    /// </summary>
    [NonSerialized]
    public new const string DBTableName = "T_ACL_OU_User";

    /// <summary>
    /// 主键名
    /// </summary>
    [NonSerialized]
    public new const string PrimaryKey = FieldUserId;

    /// <summary>
    /// 角色ID
    /// </summary>
    [NonSerialized]
    public const string FieldUserId = "User_ID";

    /// <summary>
    /// 功能ID
    /// </summary>
    [NonSerialized]
    public const string FieldOUId = "OU_ID";
}