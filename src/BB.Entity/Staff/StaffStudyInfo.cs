using System.Runtime.Serialization;
using BB.Entity.Base;

namespace BB.Entity.Staff;

/// <summary>
/// 人员学习情况
/// </summary>
[DataContract]
public class StaffStudyInfo : BaseEntity
{    
    #region Field Members

    private string m_ID = System.Guid.NewGuid().ToString(); //          
    private string m_Staff_ID; //人员ID          
    private string m_StartDate; //起始年月          
    private string m_EndDate; //毕业年月          
    private string m_School; //毕业院校          
    private string m_Specialty; //所学专业          
    private string m_Education; //学历          
    private string m_Degree; //学位          
    private string m_IsFullTime; //是否全日制          
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
    /// 毕业年月
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
    /// 毕业院校
    /// </summary>
    [DataMember]
    public virtual string School
    {
        get
        {
            return m_School;
        }
        set
        {
            m_School = value;
        }
    }

    /// <summary>
    /// 所学专业
    /// </summary>
    [DataMember]
    public virtual string Specialty
    {
        get
        {
            return m_Specialty;
        }
        set
        {
            m_Specialty = value;
        }
    }

    /// <summary>
    /// 学历
    /// </summary>
    [DataMember]
    public virtual string Education
    {
        get
        {
            return m_Education;
        }
        set
        {
            m_Education = value;
        }
    }

    /// <summary>
    /// 学位
    /// </summary>
    [DataMember]
    public virtual string Degree
    {
        get
        {
            return m_Degree;
        }
        set
        {
            m_Degree = value;
        }
    }

    /// <summary>
    /// 是否全日制
    /// </summary>
    [DataMember]
    public virtual string IsFullTime
    {
        get
        {
            return m_IsFullTime;
        }
        set
        {
            m_IsFullTime = value;
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