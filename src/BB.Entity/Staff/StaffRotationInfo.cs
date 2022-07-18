using System.Runtime.Serialization;
using BB.Entity.Base;

namespace BB.Entity.Staff;

/// <summary>
/// 人员轮转况
/// </summary>
[DataContract]
public class StaffRotationInfo : BaseEntity
{    
    #region Field Members

    private string m_ID = System.Guid.NewGuid().ToString(); //          
    private string m_Staff_ID; //人员ID          
    private string m_StartDate; //起始日期          
    private string m_EndDate; //截止日期  
    private int m_Months = 0;//轮转月数
    private string m_Department; //轮转科室          
    private string m_SubSpecialty; //轮转亚专业          
    private string m_Witness; //证明人          
    private string m_WitnessPhone; //证明人电话          
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
    /// 轮转月数
    /// </summary>
    [DataMember]
    public virtual int Months
    {
        get
        {
            return m_Months;
        }
        set
        {
            m_Months = value;
        }
    }
    /// <summary>
    /// 轮转科室
    /// </summary>
    [DataMember]
    public virtual string Department
    {
        get
        {
            return m_Department;
        }
        set
        {
            m_Department = value;
        }
    }

    /// <summary>
    /// 轮转亚专业
    /// </summary>
    [DataMember]
    public virtual string SubSpecialty
    {
        get
        {
            return m_SubSpecialty;
        }
        set
        {
            m_SubSpecialty = value;
        }
    }

    /// <summary>
    /// 证明人
    /// </summary>
    [DataMember]
    public virtual string Witness
    {
        get
        {
            return m_Witness;
        }
        set
        {
            m_Witness = value;
        }
    }

    /// <summary>
    /// 证明人电话
    /// </summary>
    [DataMember]
    public virtual string WitnessPhone
    {
        get
        {
            return m_WitnessPhone;
        }
        set
        {
            m_WitnessPhone = value;
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