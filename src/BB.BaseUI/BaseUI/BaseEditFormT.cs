using BB.BaseUI.Extension;
using BB.Entity.Base;
using BB.HttpServices.Base;
using BB.Tools.Extension;
using FluentValidation;

namespace BB.BaseUI.BaseUI;

/// <summary>
/// 编辑界面基类
/// </summary>
public partial class BaseEditForm<T, IT> : BaseEditForm
    where T : BaseEntity, new()
    where IT : BaseHttpService<T>
{
    /// <summary>
    /// 业务逻辑
    /// </summary>
    protected readonly IT Bll;

    private readonly IValidator<T> _validator;

    /// <summary>
    /// 创建一个临时对象，方便在附件管理中获取存在的GUID
    /// </summary>
    protected T? TempInfo;

    public BaseEditForm(IT bll, IValidator<T> validator)
    {
        InitializeComponent();

        Bll = bll;
        _validator = validator;
    }

    /// <summary>
    /// 初始化数据字典
    /// </summary>
    protected virtual Task InitDictItem()
    {
        throw new NotSupportedException("无法直接调用基类方法，请在派生类中实现该方法");
    }

    /// <summary>
    /// 显示数据到控件上
    /// </summary>
    public override async Task DisplayData()
    {
        await InitDictItem(); //数据字典加载（公用）

        if (!string.IsNullOrEmpty(ID))
        {
            #region 编辑时的显示信息

            TempInfo = await Bll.FindByIdAsync(ID);

            #endregion

            #region 按钮权限

            btnOK.Enabled = HasFunction($"{ParentForm}/Edit");
            btnAdd.Enabled = HasFunction($"{ParentForm}/Add") && HasFunction($"{ParentForm}/Edit");

            #endregion

            #region 默认值

            // _tempInfo.LastUpdatedBy = LoginUserInfo.ID.ToString();
            // _tempInfo.LastUpdateDate = DateTime.Now;

            #endregion
        }
        else
        {
            // 新增时
            TempInfo = await Bll.NewEntityAsync();

            #region 按钮权限

            btnOK.Enabled = HasFunction($"{ParentForm}/Add");
            btnAdd.Enabled = HasFunction($"{ParentForm}/Add") && HasFunction($"{ParentForm}/Edit");

            #endregion

            #region 默认值

            // 以保存前的赋值为准，新增时一般不用调整，都已在_bll.NewEntity()中处理
            // _tempInfo.OrgCode = LoginUserInfo.CompanyId;
            // _tempInfo.CreatedBy = LoginUserInfo.ID.ToString();
            // _tempInfo.CreationDate = DateTime.Now;

            #endregion
        }

        // 初始化表单
        DisplayInfo(TempInfo);

        //_tempInfo在对象存在则为指定对象，新建则是全新的对象，但有一些初始化的GUID用于附件上传
        //SetAttachInfo(_tempInfo);

        // 字段权限相关
        await SetPermit();
    }

    /// <summary>
    /// 初始化控件的显示信息
    /// </summary>
    /// <param name="info"></param>
    protected virtual void DisplayInfo(T info)
    {
        if (TempInfo == null || !TempInfo.Equals(info))
            TempInfo = info; //重新给临时对象赋值，使之指向存在的记录对象

        layoutControlGroup1.DoBindingEditorPanel(TempInfo, "txt"); //绑定编辑面板
    }

    /// <summary>
    /// 设置控件字段的权限显示或者隐藏(默认不使用字段权限)
    /// </summary>
    protected virtual async Task SetPermit()
    {
        // 获取表单权限的列表
        Dictionary<string, int> permitDict = await Bll.GetPermitDictAsync();

        // 调整可新增不可编辑的字段
        if (permitDict.ContainsValue(4))
        {
            permitDict.ForEach(x=>
            {
                if (x.Value==4)
                {
                    permitDict[x.Key] = ID.IsNullOrEmpty() ? 0 : 3;
                }
            });
        }

        // 设置控件的读写可见
        this.SetControlPermit(permitDict, layoutControl1);
    }

    /// <summary>
    /// 检查输入的有效性
    /// </summary>
    /// <returns>有效</returns>
    public override async Task<bool> CheckInput()
    {
        // SetInfo(_tempInfo);
        return await ProcessValidationResults(await _validator.ValidateAsync(TempInfo!));
    }

    /// <summary>
    /// 清除屏幕
    /// </summary>
    public override async Task ClearScreen()
    {
        TempInfo = await Bll.NewEntityAsync();

        ID = ""; ////需要设置为空，表示新增
        ClearControlValue(this);
        await FormOnLoad();
    }

    /// <summary>
    /// 编辑状态下的数据保存
    /// </summary>
    /// <returns></returns>
    public override async Task<bool> SaveUpdated()
    {
        if (TempInfo == null) return false;
        // SetInfo(_tempInfo);

        #region 更新数据

        bool succeed = await Bll.UpdateAsync(TempInfo);
        if (succeed)
        {
            //可添加其他关联操作

            return true;
        }

        #endregion

        return false;
    }

    protected virtual void SetInfo(T tempInfo)
    {
        throw new NotSupportedException("无法直接调用基类方法，请在派生类中实现该方法");
    }


    /// <summary>
    /// 新增状态下的数据保存
    /// </summary>
    /// <returns></returns>
    public override async Task<bool> SaveAddNew()
    {
        //必须使用存在的局部变量，因为部分信息可能被附件使用
        if (TempInfo == null) return false;
        // SetInfo(_tempInfo);

        #region 新增数据

        bool succeed = await Bll.InsertAsync(TempInfo);
        if (succeed)
        {
            //可添加其他关联操作

            return true;
        }

        #endregion

        return false;
    }
}