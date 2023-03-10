using System.Runtime.Serialization;
using BB.Entity.Base;

namespace BB.Entity.Staff;

/// <summary>
/// 人员休假情况
/// </summary>
[DataContract]
public class StaffVacationInfo : BaseEntity
{    
    #region Field Members

    private string m_ID = System.Guid.NewGuid().ToString(); //          
    private string m_Staff_ID; //人员ID          
    private string m_StartDate; //起始日期      
    private int m_Days = 0; //天数          
    private string m_EndDate; //截止日期          
    private string m_VacationLocation; //休假地点          
    private string m_Country; //国别          
    private string m_EmergencyPhone; //紧急联系电话          
    private string m_Note; //备注          
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
    /// 起始日期
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
    /// 截止日期
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
    /// 天数
    /// </summary>
    [DataMember]
    public virtual int Days
    {
        get
        {
            return m_Days;
        }
        set
        {
            m_Days = value;
        }
    }
    /// <summary>
    /// 休假地点
    /// </summary>
    [DataMember]
    public virtual string VacationLocation
    {
        get
        {
            return m_VacationLocation;
        }
        set
        {
            m_VacationLocation = value;
        }
    }

    /// <summary>
    /// 国别
    /// </summary>
    [DataMember]
    public virtual string Country
    {
        get
        {
            return m_Country;
        }
        set
        {
            m_Country = value;
        }
    }

    /// <summary>
    /// 紧急联系电话
    /// </summary>
    [DataMember]
    public virtual string EmergencyPhone
    {
        get
        {
            return m_EmergencyPhone;
        }
        set
        {
            m_EmergencyPhone = value;
        }
    }

    /// <summary>
    /// 备注
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