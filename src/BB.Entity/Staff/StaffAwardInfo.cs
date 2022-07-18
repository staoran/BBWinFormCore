using System.Runtime.Serialization;
using BB.Entity.Base;

namespace BB.Entity.Staff;

/// <summary>
/// 人员受奖情况
/// </summary>
[DataContract]
public class StaffAwardInfo : BaseEntity
{    
    #region Field Members

    private string m_ID = System.Guid.NewGuid().ToString(); //          
    private string m_Staff_ID; //人员ID          
    private string m_Note; //受奖情况          
    private string m_AttachGUID = System.Guid.NewGuid().ToString(); //相关资料          
    private int m_Seq = 0; //序号  

    #endregion

    #region Property Members
        
    [DataMember]
    public virtual string ID
    {
        get
        {
            return m_ID;
        }
        set
        {
            m_ID = value;
        }
    }

    /// <summary>
    /// 人员ID
    /// </summary>
    [DataMember]
    public virtual string Staff_ID
    {
        get
        {
            return m_Staff_ID;
        }
        set
        {
            m_Staff_ID = value;
        }
    }

    /// <summary>
    /// 受奖情况
    /// </summary>
    [DataMember]
    public virtual string Note
    {
        get
        {
            return m_Note;
        }
        set
        {
            m_Note = value;
        }
    }

    /// <summary>
    /// 相关资料
    /// </summary>
    [DataMember]
    public virtual string AttachGUID
    {
        get
        {
            return m_AttachGUID;
        }
        set
        {
            m_AttachGUID = value;
        }
    }

    /// <summary>
    /// 序号
    /// </summary>
    [DataMember]
    public virtual int Seq
    {
        get
        {
            return m_Seq;
        }
        set
        {
            m_Seq = value;
        }
    }

    #endregion

}