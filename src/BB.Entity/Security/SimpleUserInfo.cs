using System.Runtime.Serialization;
using BB.Entity.Base;

namespace BB.Entity.Security;

/// <summary>
/// 可用于传递的用户简单信息
/// </summary>
[Serializable]
[DataContract]
public class SimpleUserInfo : BaseEntity
{     
    /// <summary>
    /// 默认构造函数
    /// </summary>
    public SimpleUserInfo()
    {

    }

    /// <summary>
    /// ID
    /// </summary>
    [DataMember]
    public virtual int ID { get; set; }

    /// <summary>
    /// 用户编码
    /// </summary>
    [DataMember]
    public virtual string HandNo { get; set; }

    /// <summary>
    /// 用户名/登录名
    /// </summary>
    [DataMember]
    public virtual string Name { get; set; }

    /// <summary>
    /// 用户密码
    /// </summary>
    [DataMember]
    public virtual string Password { get; set; }

    /// <summary>
    /// 用户全名
    /// </summary>
    [DataMember]
    public virtual string FullName { get; set; }

    /// <summary>
    /// 移动电话
    /// </summary>
    [DataMember]
    public virtual string MobilePhone { get; set; }

    /// <summary>
    /// 邮件地址
    /// </summary>
    [DataMember]
    public virtual string Email { get; set; }

    /// <summary>
    /// 默认部门ID
    /// </summary>
    [DataMember]
    public virtual string DeptId { get; set; }

    /// <summary>
    /// 所属机构ID
    /// </summary>
    [DataMember]
    public virtual string CompanyId { get; set; }

    /// <summary>
    /// 当前登录IP
    /// </summary>
    [DataMember]
    public virtual string CurrentLoginIP { get; set; }

    /// <summary>
    /// 当前登录时间
    /// </summary>
    [DataMember]
    public virtual DateTime CurrentLoginTime { get; set; }

    /// <summary>
    /// 当前Mac地址
    /// </summary>
    [DataMember]
    public virtual string CurrentMacAddress { get; set; }
}