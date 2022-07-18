using BB.Entity.Base;

namespace BB.Entity.WareHouse;

[Serializable]
public class ReportMonthlyCostDetailInfo : BaseEntity
{    
    #region Field Members

    private int _mId = 0;         
    private int _mHeaderId = 0; //报表头ID          
    private int _mReportYear = 0; //报表年份          
    private int _mReportMonth = 0; //报表月份          
    private string _mYearMonth = ""; //报表年月          
    private string _mDeptName = ""; //部门项目          
    private string _mItemType = ""; //备件类别          
    private decimal _mTotalMoney = 0; //总金额          
    private string _mReportCode = ""; //额外的数据分类码          

    #endregion

    #region Property Members
        
    public virtual int Id
    {
        get
        {
            return _mId;
        }
        set
        {
            _mId = value;
        }
    }

    /// <summary>
    /// 报表头ID
    /// </summary>
    public virtual int HeaderId
    {
        get
        {
            return _mHeaderId;
        }
        set
        {
            _mHeaderId = value;
        }
    }

    /// <summary>
    /// 报表年份
    /// </summary>
    public virtual int ReportYear
    {
        get
        {
            return _mReportYear;
        }
        set
        {
            _mReportYear = value;
        }
    }

    /// <summary>
    /// 报表月份
    /// </summary>
    public virtual int ReportMonth
    {
        get
        {
            return _mReportMonth;
        }
        set
        {
            _mReportMonth = value;
        }
    }

    /// <summary>
    /// 报表年月
    /// </summary>
    public virtual string YearMonth
    {
        get
        {
            return _mYearMonth;
        }
        set
        {
            _mYearMonth = value;
        }
    }

    /// <summary>
    /// 部门项目
    /// </summary>
    public virtual string DeptName
    {
        get
        {
            return _mDeptName;
        }
        set
        {
            _mDeptName = value;
        }
    }

    /// <summary>
    /// 备件类别
    /// </summary>
    public virtual string ItemType
    {
        get
        {
            return _mItemType;
        }
        set
        {
            _mItemType = value;
        }
    }

    /// <summary>
    /// 总金额
    /// </summary>
    public virtual decimal TotalMoney
    {
        get
        {
            return _mTotalMoney;
        }
        set
        {
            _mTotalMoney = value;
        }
    }

    /// <summary>
    /// 额外的数据分类码
    /// </summary>
    public virtual string ReportCode
    {
        get
        {
            return _mReportCode;
        }
        set
        {
            _mReportCode = value;
        }
    }


    #endregion

}