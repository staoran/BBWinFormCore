using System.Runtime.Serialization;
using BB.Entity.Base;

namespace BB.Entity.Staff;

/// <summary>
/// 人员家庭情况
/// </summary>
[DataContract]
public class StaffFamilyInfo : BaseEntity
{    
    #region Field Members

    private string m_ID = Guid.NewGuid().ToString(); //          
    private string m_Staff_ID; //人员ID          
    private string m_Name; //姓名          
    private string m_Relation; //关系          
    private string m_Sex; //性别          
    private DateTime m_BirthDate; //出生时间          
    private string m_Political; //政治面貌          
    private string m_ServeUnit; //工作（学习）单位          
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
    /// 姓名
    /// </summary>
    [DataMember]
    public virtual string Name
    {
        get
        {
            return m_Name;
        }
        set
        {
            m_Name = value;
        }
    }

    /// <summary>
    /// 关系
    /// </summary>
    [DataMember]
    public virtual string Relation
    {
        get
        {
            return m_Relation;
        }
        set
        {
            m_Relation = value;
        }
    }

    /// <summary>
    /// 性别
    /// </summary>
    [DataMember]
    public virtual string Sex
    {
        get
        {
            return m_Sex;
        }
        set
        {
            m_Sex = value;
        }
    }

    /// <summary>
    /// 出生时间
    /// </summary>
    [DataMember]
    public virtual DateTime BirthDate
    {
        get
        {
            return m_BirthDate;
        }
        set
        {
            m_BirthDate = value;
        }
    }

    /// <summary>
    /// 政治面貌
    /// </summary>
    [DataMember]
    public virtual string Political
    {
        get
        {
            return m_Political;
        }
        set
        {
            m_Political = value;
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