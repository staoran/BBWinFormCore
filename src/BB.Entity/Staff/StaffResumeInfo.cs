using System.Runtime.Serialization;
using BB.Entity.Base;

namespace BB.Entity.Staff;

/// <summary>
/// 人员履历情况
/// </summary>
[DataContract]
public class StaffResumeInfo : BaseEntity
{    
    #region Field Members

    private string m_ID = System.Guid.NewGuid().ToString(); //          
    private string m_Staff_ID; //人员ID          
    private string m_StartDate; //起始年月          
    private string m_EndDate; //截止年月          
    private string m_ServeCompany; //工作单位          
    private string m_OfficeRank; //职务   
    private string m_Titles; //职称          
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
    /// 起始年月
    /// </summary>
    [DataMember]
    public virtual string StartDate
    {
        get
        {
            return m_StartDate;
        }
        set
        {
            m_StartDate = value;
        }
    }

    /// <summary>
    /// 截止年月
    /// </summary>
    [DataMember]
    public virtual string EndDate
    {
        get
        {
            return m_EndDate;
        }
        set
        {
            m_EndDate = value;
        }
    }

    /// <summary>
    /// 工作单位
    /// </summary>
    [DataMember]
    public virtual string ServeCompany
    {
        get
        {
            return m_ServeCompany;
        }
        set
        {
            m_ServeCompany = value;
        }
    }

    /// <summary>
    /// 职务
    /// </summary>
    [DataMember]
    public virtual string OfficeRank
    {
        get
        {
            return m_OfficeRank;
        }
        set
        {
            m_OfficeRank = value;
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