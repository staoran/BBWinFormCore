using BB.Entity.Base;

namespace BB.Entity.WareHouse;

[Serializable]
public class PurchaseDetailInfo : BaseEntity
{    
    #region Field Members

    private int _mId = 0;         
    private int _mPurchaseHeadId = 0; //进货表单头          
    private string _mOperationType = ""; //操作类型(进货还是退货)          
    private string _mItemNo = ""; //备件编号          
    private string _mItemName = ""; //备件名称          
    private string _mMapNo = ""; //图号          
    private string _mSpecification = ""; //规格型号          
    private string _mMaterial = ""; //材质          
    private string _mItemBigType = ""; //备件属类          
    private string _mItemType = ""; //备件类别          
    private string _mUnit = ""; //单位          
    private decimal _mPrice = 0; //单价          
    private double _mQuantity = 0; //数量          
    private decimal _mAmount = 0; //金额          
    private string _mSource = ""; //来源          
    private string _mStoragePos = ""; //库位          
    private string _mUsagePos = ""; //使用位置          
    private string _mWareHouse = ""; //库房编号          
    private string _mDept = ""; //部门   

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
    /// 进货表单头
    /// </summary>
    public virtual int PurchaseHeadId
    {
        get
        {
            return _mPurchaseHeadId;
        }
        set
        {
            _mPurchaseHeadId = value;
        }
    }

    /// <summary>
    /// 操作类型(进货还是退货)
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
    /// 备件编号
    /// </summary>
    public virtual string ItemNo
    {
        get
        {
            return _mItemNo;
        }
        set
        {
            _mItemNo = value;
        }
    }

    /// <summary>
    /// 备件名称
    /// </summary>
    public virtual string ItemName
    {
        get
        {
            return _mItemName;
        }
        set
        {
            _mItemName = value;
        }
    }

    /// <summary>
    /// 图号
    /// </summary>
    public virtual string MapNo
    {
        get
        {
            return _mMapNo;
        }
        set
        {
            _mMapNo = value;
        }
    }

    /// <summary>
    /// 规格型号
    /// </summary>
    public virtual string Specification
    {
        get
        {
            return _mSpecification;
        }
        set
        {
            _mSpecification = value;
        }
    }

    /// <summary>
    /// 材质
    /// </summary>
    public virtual string Material
    {
        get
        {
            return _mMaterial;
        }
        set
        {
            _mMaterial = value;
        }
    }

    /// <summary>
    /// 备件属类
    /// </summary>
    public virtual string ItemBigType
    {
        get
        {
            return _mItemBigType;
        }
        set
        {
            _mItemBigType = value;
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
    /// 单位
    /// </summary>
    public virtual string Unit
    {
        get
        {
            return _mUnit;
        }
        set
        {
            _mUnit = value;
        }
    }

    /// <summary>
    /// 单价
    /// </summary>
    public virtual decimal Price
    {
        get
        {
            return _mPrice;
        }
        set
        {
            _mPrice = value;
        }
    }

    /// <summary>
    /// 数量
    /// </summary>
    public virtual double Quantity
    {
        get
        {
            return _mQuantity;
        }
        set
        {
            _mQuantity = value;
        }
    }

    /// <summary>
    /// 金额
    /// </summary>
    public virtual decimal Amount
    {
        get
        {
            return _mAmount;
        }
        set
        {
            _mAmount = value;
        }
    }

    /// <summary>
    /// 来源
    /// </summary>
    public virtual string Source
    {
        get
        {
            return _mSource;
        }
        set
        {
            _mSource = value;
        }
    }

    /// <summary>
    /// 库位
    /// </summary>
    public virtual string StoragePos
    {
        get
        {
            return _mStoragePos;
        }
        set
        {
            _mStoragePos = value;
        }
    }

    /// <summary>
    /// 使用位置
    /// </summary>
    public virtual string UsagePos
    {
        get
        {
            return _mUsagePos;
        }
        set
        {
            _mUsagePos = value;
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
    /// 部门
    /// </summary>
    public virtual string Dept
    {
        get
        {
            return _mDept;
        }
        set
        {
            _mDept = value;
        }
    }

    #endregion

}