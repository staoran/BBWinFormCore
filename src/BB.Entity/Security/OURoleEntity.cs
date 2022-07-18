using System.Runtime.Serialization;
using BB.Entity.Base;

namespace BB.Entity.Security;

[DataContract]
[Serializable]
[Table("T_ACL_OU_Role")]
public class OURoleEntity
{
    [Key]
    [DataMember]
    [Column("Role_ID", "角色编号")]
    public int RoleId { get; set; }
    [Key]
    [DataMember]
    [Column("OU_ID", "机构编号")]
    public string OUId { get; set; }
}