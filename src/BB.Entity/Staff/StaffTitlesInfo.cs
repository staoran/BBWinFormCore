using System.Runtime.Serialization;
using BB.Entity.Base;

namespace BB.Entity.Staff;

/// <summary>
/// 人员职称情况
/// </summary>
[DataContract]
public class StaffTitlesInfo : BaseEntity
{    
    #region Field Members

    private string m_ID = System.Guid.NewGuid().ToString(); //          
    private string m_Staff_ID; //人员ID          
    private string m_Titles; //职称          
    private string m_ObtainDate; //取得资格年月          
    private string m_AppointDate; //任命年月          
    private int m_Seq; //序号          

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
    /// 职称
    /// </summary>
    [DataMember]
    public virtual string Titles
    {
        get
        {
            return m_Titles;
        }
        set
        {
            m_Titles = value;
        }
    }

    /// <summary>
    /// 取得资格年月
    /// </summary>
    [DataMember]
    public virtual string ObtainDate
    {
        get
        {
            return m_ObtainDate;
        }
        set
        {
            m_ObtainDate = value;
        }
    }

    /// <summary>
    /// 任命年月
    /// </summary>
    [DataMember]
    public virtual string AppointDate
    {
        get
        {
            return m_AppointDate;
        }
        set
        {
            m_AppointDate = value;
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