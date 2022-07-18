using System.Runtime.Serialization;

namespace BB.Entity.Security;

/// <summary>
/// 用来传递一般操作的信息
/// </summary>
[DataContract]
[Serializable]
public class LoginResult
{
    /// <summary>
    /// 是否成功
    /// </summary>
    [DataMember]
    public bool Success { get; set; }

    /// <summary>
    /// 如果有错误，则显示错误信息
    /// </summary>
    [DataMember]
    public string ErrorMessage { get; set; }

    /// <summary>
    /// 成功后，返回的用户信息
    /// </summary>
    [DataMember]
    public UserInfo UserInfo { get; set; }
}