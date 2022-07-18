using System.Runtime.Serialization;
using BB.Entity.Base;

namespace BB.Entity.Staff;

/// <summary>
/// 人员基本信息
/// </summary>
[DataContract]
public class StaffInfo : BaseEntity
{
    #region Field Members

    private string m_ID = Guid.NewGuid().ToString(); //          
    private string m_Name; //姓名          
    private string m_Sex; //性别          
    private DateTime m_BirthDate; //出生时间          
    private string m_Political; //政治面貌          
    private DateTime m_PartyDate; //党团时间          
    private string m_Nationality; //民族          
    private string m_NativePlace; //籍贯          
    private string m_OfficialRank; //职务          
    private string m_ServingDate; //任职时间          
    private string m_WorkingDate; //工作时间          
    private string m_HighestEducation; //最高学历          
    private string m_EducationDate; //获学历时间          
    private string m_HighestDegree; //最高学位          
    private string m_DegreeDate; //获学位时间          
    private string m_MarriageStatus; //婚否          
    private string m_Titles; //职称          
    private string m_TitlesDate; //职称时间          
    private string m_ChildStatus; //是否独生子女          
    private string m_UserIdentity; //身份          
    private string m_Email; //电子邮箱          
    private string m_Mobile; //手机          
    private string m_OfficePhone; //办公电话          
    private string m_HomePhone; //家庭电话          
    private string m_Academic; //学术任职          
    private string m_Research; //研究方向          
    private string m_Introduce; //个人介绍          
    private string m_Note; //备注信息          
    private byte[] m_Portraint; //个人照片          
    private string m_AttachGUID; //个人资料          
    private string m_CheckUser; //资料核对          
    private string m_Creator; //创建人          
    private DateTime m_CreateTime; //创建时间          
    private string m_Editor; //编辑人          
    private DateTime m_EditTime; //编辑时间          
    private string m_Dept_ID; //所属部门
    private string m_Company_ID; //所属公司        

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
    /// 党团时间
    /// </summary>
    [DataMember]
    public virtual DateTime PartyDate
    {
        get
        {
            return m_PartyDate;
        }
        set
        {
            m_PartyDate = value;
        }
    }

    /// <summary>
    /// 民族
    /// </summary>
    [DataMember]
    public virtual string Nationality
    {
        get
        {
            return m_Nationality;
        }
        set
        {
            m_Nationality = value;
        }
    }

    /// <summary>
    /// 籍贯
    /// </summary>
    [DataMember]
    public virtual string NativePlace
    {
        get
        {
            return m_NativePlace;
        }
        set
        {
            m_NativePlace = value;
        }
    }

    /// <summary>
    /// 职务
    /// </summary>
    [DataMember]
    public virtual string OfficialRank
    {
        get
        {
            return m_OfficialRank;
        }
        set
        {
            m_OfficialRank = value;
        }
    }

    /// <summary>
    /// 任职时间
    /// </summary>
    [DataMember]
    public virtual string ServingDate
    {
        get
        {
            return m_ServingDate;
        }
        set
        {
            m_ServingDate = value;
        }
    }

    /// <summary>
    /// 工作时间
    /// </summary>
    [DataMember]
    public virtual string WorkingDate
    {
        get
        {
            return m_WorkingDate;
        }
        set
        {
            m_WorkingDate = value;
        }
    }

    /// <summary>
    /// 最高学历
    /// </summary>
    [DataMember]
    public virtual string HighestEducation
    {
        get
        {
            return m_HighestEducation;
        }
        set
        {
            m_HighestEducation = value;
        }
    }

    /// <summary>
    /// 获学历时间
    /// </summary>
    [DataMember]
    public virtual string EducationDate
    {
        get
        {
            return m_EducationDate;
        }
        set
        {
            m_EducationDate = value;
        }
    }

    /// <summary>
    /// 最高学位
    /// </summary>
    [DataMember]
    public virtual string HighestDegree
    {
        get
        {
            return m_HighestDegree;
        }
        set
        {
            m_HighestDegree = value;
        }
    }

    /// <summary>
    /// 获学位时间
    /// </summary>
    [DataMember]
    public virtual string DegreeDate
    {
        get
        {
            return m_DegreeDate;
        }
        set
        {
            m_DegreeDate = value;
        }
    }

    /// <summary>
    /// 婚否
    /// </summary>
    [DataMember]
    public virtual string MarriageStatus
    {
        get
        {
            return m_MarriageStatus;
        }
        set
        {
            m_MarriageStatus = value;
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
    /// 职称时间
    /// </summary>
    [DataMember]
    public virtual string TitlesDate
    {
        get
        {
            return m_TitlesDate;
        }
        set
        {
            m_TitlesDate = value;
        }
    }

    /// <summary>
    /// 是否独生子女
    /// </summary>
    [DataMember]
    public virtual string ChildStatus
    {
        get
        {
            return m_ChildStatus;
        }
        set
        {
            m_ChildStatus = value;
        }
    }

    /// <summary>
    /// 身份
    /// </summary>
    [DataMember]
    public virtual string UserIdentity
    {
        get
        {
            return m_UserIdentity;
        }
        set
        {
            m_UserIdentity = value;
        }
    }

    /// <summary>
    /// 电子邮箱
    /// </summary>
    [DataMember]
    public virtual string Email
    {
        get
        {
            return m_Email;
        }
        set
        {
            m_Email = value;
        }
    }

    /// <summary>
    /// 手机
    /// </summary>
    [DataMember]
    public virtual string Mobile
    {
        get
        {
            return m_Mobile;
        }
        set
        {
            m_Mobile = value;
        }
    }

    /// <summary>
    /// 办公电话
    /// </summary>
    [DataMember]
    public virtual string OfficePhone
    {
        get
        {
            return m_OfficePhone;
        }
        set
        {
            m_OfficePhone = value;
        }
    }

    /// <summary>
    /// 家庭电话
    /// </summary>
    [DataMember]
    public virtual string HomePhone
    {
        get
        {
            return m_HomePhone;
        }
        set
        {
            m_HomePhone = value;
        }
    }

    /// <summary>
    /// 学术任职
    /// </summary>
    [DataMember]
    public virtual string Academic
    {
        get
        {
            return m_Academic;
        }
        set
        {
            m_Academic = value;
        }
    }

    /// <summary>
    /// 研究方向
    /// </summary>
    [DataMember]
    public virtual string Research
    {
        get
        {
            return m_Research;
        }
        set
        {
            m_Research = value;
        }
    }

    /// <summary>
    /// 个人介绍
    /// </summary>
    [DataMember]
    public virtual string Introduce
    {
        get
        {
            return m_Introduce;
        }
        set
        {
            m_Introduce = value;
        }
    }

    /// <summary>
    /// 备注信息
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
    /// 个人照片
    /// </summary>
    [DataMember]
    public virtual byte[] Portraint
    {
        get
        {
            return m_Portraint;
        }
        set
        {
            m_Portraint = value;
        }
    }

    /// <summary>
    /// 个人资料
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
    /// 资料核对
    /// </summary>
    [DataMember]
    public virtual string CheckUser
    {
        get
        {
            return m_CheckUser;
        }
        set
        {
            m_CheckUser = value;
        }
    }

    /// <summary>
    /// 创建人
    /// </summary>
    [DataMember]
    public virtual string Creator
    {
        get
        {
            return m_Creator;
        }
        set
        {
            m_Creator = value;
        }
    }

    /// <summary>
    /// 创建时间
    /// </summary>
    [DataMember]
    public virtual DateTime CreateTime
    {
        get
        {
            return m_CreateTime;
        }
        set
        {
            m_CreateTime = value;
        }
    }

    /// <summary>
    /// 编辑人
    /// </summary>
    [DataMember]
    public virtual string Editor
    {
        get
        {
            return m_Editor;
        }
        set
        {
            m_Editor = value;
        }
    }

    /// <summary>
    /// 编辑时间
    /// </summary>
    [DataMember]
    public virtual DateTime EditTime
    {
        get
        {
            return m_EditTime;
        }
        set
        {
            m_EditTime = value;
        }
    }

    /// <summary>
    /// 所属部门
    /// </summary>
    [DataMember]
    public virtual string Dept_ID
    {
        get
        {
            return m_Dept_ID;
        }
        set
        {
            m_Dept_ID = value;
        }
    }

    /// <summary>
    /// 所属公司
    /// </summary>
    [DataMember]
    public virtual string Company_ID
    {
        get
        {
            return m_Company_ID;
        }
        set
        {
            m_Company_ID = value;
        }
    }

    #endregion

}