using BB.Entity.Base;

namespace BB.Entity.WareHouse;

[Serializable]
public class PurchaseHeaderInfo : BaseEntity
{    
    #region Field Members

    private int _mId = 0;         
    private string _mHandNo = ""; //进货编号          
    private string _mOperationType = ""; //操作类型（进货还是退货）          
    private string _mManufacture = ""; //供应商          
    private string _mWareHouse = ""; //库房编号          
    private string _mCostCenter = ""; //成本中心          
    private string _mNote = ""; //备注          
    private DateTime _mCreateDate = DateTime.Now; //创建日期          
    private string _mCreator = ""; //操作员        
    private int _mCreateYear = 0; //记录年          
    private int _mCreateMonth = 0; //记录月  
    private string _mPickingPeople = "";//领料人

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
    /// 进货编号
    /// </summary>
    public virtual string HandNo
    {
        get
        {
            return _mHandNo;
        }
        set
        {
            _mHandNo = value;
        }
    }

    /// <summary>
    /// 操作类型（进货还是退货）
    /// </summary>
    public virtual string OperationType
    {
        get
        {
            return _mOperationType;
        }
        set
        {
            _mOperationType = value;
        }
    }

    /// <summary>
    /// 供应商
    /// </summary>
    public virtual string Manufacture
    {
        get
        {
            return _mManufacture;
        }
        set
        {
            _mManufacture = value;
        }
    }

    /// <summary>
    /// 库房编号
    /// </summary>
    public virtual string WareHouse
    {
        get
        {
            return _mWareHouse;
        }
        set
        {
            _mWareHouse = value;
        }
    }

    /// <summary>
    /// 成本中心
    /// </summary>
    public string CostCenter
    {
        get { return _mCostCenter; }
        set { _mCostCenter = value; }
    }

    /// <summary>
    /// 备注
    /// </summary>
    public virtual string Note
    {
        get
        {
            return _mNote;
        }
        set
        {
            _mNote = value;
        }
    }

    /// <summary>
    /// 创建日期
    /// </summary>
    public virtual DateTime CreateDate
    {
        get
        {
            return _mCreateDate;
        }
        set
        {
            _mCreateDate = value;
        }
    }

    /// <summary>
    /// 操作员
    /// </summary>
    public virtual string Creator
    {
        get
        {
            return _mCreator;
        }
        set
        {
            _mCreator = value;
        }
    }


    /// <summary>
    /// 记录年
    /// </summary>
    public virtual int CreateYear
    {
        get
        {
            return _mCreateYear;
        }
        set
        {
            _mCreateYear = value;
        }
    }

    /// <summary>
    /// 记录月
    /// </summary>
    public virtual int CreateMonth
    {
        get
        {
            return _mCreateMonth;
        }
        set
        {
            _mCreateMonth = value;
        }
    }


    /// <summary>
    /// 领料人
    /// </summary>
    public string PickingPeople
    {
        get { return _mPickingPeople; }
        set { _mPickingPeople = value; }
    }

    #endregion

}