using System.Runtime.Serialization;
using BB.Entity.Base;

namespace BB.Entity.Staff;

/// <summary>
/// 人员出国情况
/// </summary>
[DataContract]
public class StaffAbroadInfo : BaseEntity
{    
    #region Field Members

    private string m_ID = System.Guid.NewGuid().ToString(); //          
    private string m_Staff_ID; //人员ID          
    private string m_StartDate; //起始日期        
    private string m_EndDate; //毕业日期   
    private string m_Country; //国别          
    private string m_ServeUnit; //工作（学习）单位          
    private string m_AbroadType; //出国类型          
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
    /// 结束日期
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
    /// 工作（学习）单位
    /// </summary>
    [DataMember]
    public virtual string ServeUnit
    {
        get
        {
            return m_ServeUnit;
        }
        set
        {
            m_ServeUnit = value;
        }
    }

    /// <summary>
    /// 出国类型
    /// </summary>
    [DataMember]
    public virtual string AbroadType
    {
        get
        {
            return m_AbroadType;
        }
        set
        {
            m_AbroadType = value;
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