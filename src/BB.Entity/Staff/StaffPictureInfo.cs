using System.Runtime.Serialization;
using BB.Entity.Base;

namespace BB.Entity.Staff;

/// <summary>
/// 人员图片信息
/// </summary>
[DataContract]
public class StaffPictureInfo : BaseEntity
{    
    #region Field Members

    private string m_ID = System.Guid.NewGuid().ToString(); //          
    private string m_Staff_ID; //人员ID          
    private string m_Category; //类别          
    private string m_Note; //说明信息          
    private string m_AttachGUID; //相关资料          

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
    /// 类别
    /// </summary>
    [DataMember]
    public virtual string Category
    {
        get
        {
            return m_Category;
        }
        set
        {
            m_Category = value;
        }
    }

    /// <summary>
    /// 说明信息
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


    #endregion

}