using System.Runtime.Serialization;

namespace BB.Entity.Base;

/// <summary>
/// 登陆用户的基础信息
/// </summary>
[Serializable]
[DataContract]
public class LoginUserInfo
{
    /// <summary>
    /// 用户ID
    /// </summary>
    [DataMember]
    public int ID { get; set; }

    /// <summary>
    /// 所属部门ID
    /// </summary>
    [DataMember]
    public string DeptId { get; set; }

    /// <summary>
    /// 部门名称
    /// </summary>
    [DataMember]
    public string DeptName { get; set; }

    /// <summary>
    /// 所属公司ID
    /// </summary>
    [DataMember]
    public string CompanyId { get; set; }

    /// <summary>
    /// 公司名称
    /// </summary>
    [DataMember]
    public string CompanyName { get; set; }

    /// <summary>
    /// 角色名称集合
    /// </summary>
    [DataMember]
    public List<string> RoleNameList { get; set; }
              
    /// <summary>
    /// 角色ID集合
    /// </summary>
    [DataMember]
    public List<string> RoleIdList { get; set; }

    /// <summary>
    /// 用户登陆名称
    /// </summary>
    [DataMember]
    public string Name { get; set; }

    /// <summary>
    /// 用户全名
    /// </summary>
    [DataMember]
    public string FullName { get; set; }

    /// <summary>
    /// 用户性别
    /// </summary>
    [DataMember]
    public string Gender { get; set; }

    /// <summary>
    /// 移动电话   
    /// </summary>
    [DataMember]
    public string MobilePhone { get; set; }

    /// <summary>
    /// Email
    /// </summary>
    [DataMember]
    public string Email { get; set; }

    /// <summary>
    /// 是否为管理员身份
    /// </summary>
    [DataMember]
    public bool IsAdmin { get; set; }

    /// <summary>
    /// 用来传递的内容
    /// </summary>
    [DataMember]
    public string Data1 { get; set; }

    /// <summary>
    /// 用来传递的内容
    /// </summary>
    [DataMember]
    public string Data2 { get; set; }

    /// <summary>
    /// 用来传递的内容
    /// </summary>
    [DataMember]
    public string Data3 { get; set; }

}