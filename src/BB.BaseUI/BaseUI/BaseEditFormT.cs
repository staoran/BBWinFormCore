using BB.BaseUI.Extension;
using BB.Entity.Base;
using BB.HttpService.Base;
using BB.Tools.Extension;
using FluentValidation;
using Furion;

namespace BB.BaseUI.BaseUI;

/// <summary>
/// 编辑界面基类
/// </summary>
public partial class BaseEditForm<T, IT, TV> : BaseEditForm
    where T : BaseEntity
    where IT : BaseHttpService<T>
    where TV : AbstractValidator<T>, new()
{
    /// <summary>
    /// 业务逻辑
    /// </summary>
    protected readonly IT _bll;

    /// <summary>
    /// 创建一个临时对象，方便在附件管理中获取存在的GUID
    /// </summary>
    protected T? _tempInfo;

    public BaseEditForm(IT bll)
    {
        InitializeComponent();

        _bll = bll;
    }

    /// <summary>
    /// 初始化数据字典
    /// </summary>
    protected virtual void InitDictItem()
    {
        throw new NotSupportedException("无法直接调用基类方法，请在派生类中实现该方法");
    }

    /// <summary>
    /// 显示数据到控件上
    /// </summary>
    public override async Task DisplayData()
    {
        InitDictItem(); //数据字典加载（公用）

        if (!string.IsNullOrEmpty(ID))
        {
            #region 编辑时的显示信息

            _tempInfo = await _bll.FindByIdAsync(ID);

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
            _tempInfo = await _bll.NewEntityAsync();

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
        DisplayInfo(_tempInfo);

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
        if (!_tempInfo.Equals(info))
            _tempInfo = info; //重新给临时对象赋值，使之指向存在的记录对象

        layoutControlGroup1.DoBindingEditorPanel(_tempInfo, "txt"); //绑定编辑面板
    }

    /// <summary>
    /// 设置控件字段的权限显示或者隐藏(默认不使用字段权限)
    /// </summary>
    protected virtual async Task SetPermit()
    {
        // 获取表单权限的列表
        Dictionary<string, int> permitDict = await _bll.GetPermitDictAsync();

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
        var validator = new TV();
        return await ProcessValidationResults(await validator.ValidateAsync(_tempInfo));
    }

    /// <summary>
    /// 清除屏幕
    /// </summary>
    public override async Task ClearScreen()
    {
        _tempInfo = await _bll.NewEntityAsync();

        ID = ""; ////需要设置为空，表示新增
        ClearControlValue(this);
        FormOnLoad();
    }

    /// <summary>
    /// 编辑状态下的数据保存
    /// </summary>
    /// <returns></returns>
    public override async Task<bool> SaveUpdated()
    {
        if (_tempInfo == null) return false;
        // SetInfo(_tempInfo);

        #region 更新数据

        bool succeed = await _bll.UpdateAsync(_tempInfo);
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
        if (_tempInfo == null) return false;
        // SetInfo(_tempInfo);

        #region 新增数据

        bool succeed = await _bll.InsertAsync(_tempInfo);
        if (succeed)
        {
            //可添加其他关联操作

            return true;
        }

        #endregion

        return false;
    }
}