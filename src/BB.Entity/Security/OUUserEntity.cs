using System.Runtime.Serialization;
using BB.Entity.Base;

namespace BB.Entity.Security;

[DataContract]
[Serializable]
[Table("T_ACL_OU_User")]
public class OUUserEntity
{
    [Key]
    [DataMember]
    [Column("User_ID", "用户编号")]
    public int UserId { get; set; }
    [Key]
    [DataMember]
    [Column("OU_ID", "机构编号")]
    public string OUId { get; set; }
}