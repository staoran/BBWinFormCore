using BB.BaseUI.Other;
using BB.Tools.Entity;
using BB.Tools.Extension;
using BB.Tools.Format;
using BB.Tools.MultiLanuage;
using BB.Tools.Validation;
using DevExpress.Images;
using DevExpress.Utils;
using DevExpress.Utils.Design;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.XtraLayout;

namespace BB.BaseUI.Extension;

/// <summary>
/// 编辑控件的扩展函数
/// </summary>
public static class ControlExtension
{
    #region 控件校验

    /// <summary>
    /// 检验输入控件
    /// </summary>
    /// <param name="baseEdit">输入控件</param>
    /// <param name="dxErrorProvider"></param>
    /// <param name="checkNull">是否验证空值</param>
    /// <param name="max">验证长度，0为不验证</param>
    /// <param name="boolFunc">验证方法</param>
    /// <param name="errorText">错误提示</param>
    /// <param name="errorType">默认错误类型</param>
    /// <returns></returns>
    public static bool ValidateBaseEdit(this BaseEdit baseEdit, DXErrorProvider dxErrorProvider, bool checkNull,
        int max, Function<bool, string> boolFunc, string errorText, ErrorType errorType = ErrorType.Critical)
    {
        if (checkNull && !baseEdit.ValidateEditNullError(dxErrorProvider)) return false;
        
        if (max > 0 && !baseEdit.ValidateRange(dxErrorProvider, max)) return false;
        
        string Func()
        {
            string editValue;
            if (baseEdit is SearchLookUpEdit && baseEdit.EditValue is long l)
                editValue = l == 0 ? string.Empty : l.ToString();
            else if (baseEdit is ComboBoxEdit && baseEdit.EditValue is CListItem item)
                editValue = item.Value;
            else
                editValue = string.Concat(baseEdit.EditValue).Trim();
            // 如果是空或是 无 直接返回空，不验证
            return !editValue.IsNullOrEmpty() && editValue is not "无" or "请选择" && !boolFunc(editValue)
                ? JsonLanguage.Default.GetString(errorText)
                : string.Empty;
        }

        if (!dxErrorProvider.ValidateEdit(baseEdit, Func, errorType, true))
        {
            errorText.ShowErrorTip(baseEdit);
            baseEdit.Focus();
            baseEdit.SelectAll();
            return false;
        }

        return true;
    }

    /// <summary>
    /// 自定义校验控件，通过返回true
    /// </summary>
    /// <param name="dxErrorProvider"></param>
    /// <param name="baseEdit">输入控件集合</param>
    /// <param name="validateFunc">匿名委托，通过返回string.Empty,否则返回错误提示</param>
    /// <param name="errorType">默认错误类型</param>
    /// <param name="realTime">实时验证</param>
    /// <returns></returns>
    public static bool ValidateEdit(this DXErrorProvider dxErrorProvider, BaseEdit baseEdit, 
        Function<string> validateFunc, ErrorType errorType = ErrorType.Critical, bool realTime = false)
    {
        string errorText = validateFunc();
        if (errorText.IsNullOrEmpty()) return true;
            
        //多语言支持
        errorText = JsonLanguage.Default.GetString(errorText);

        dxErrorProvider.SetError(baseEdit, errorText, errorType);

        if (realTime)
            baseEdit.EditValueChanged += (_, _) =>
            {
                if (validateFunc().IsNullOrEmpty()) dxErrorProvider.SetErrorType(baseEdit, ErrorType.None);
            };
        baseEdit.Focus();
        baseEdit.SelectAll();
            
        return false;
    }

    /// <summary>
    /// 校验控件是否为空，如果全部非空，则返回True
    /// </summary>
    /// <param name="dxErrorProvider"></param>
    /// <param name="baseEdits">输入控件集合</param>
    /// <returns></returns>
    public static bool ValidateEditNull(this DXErrorProvider dxErrorProvider, IEnumerable<BaseEdit> baseEdits, 
        string errorText = "不能为空", ErrorType errorType = ErrorType.Critical)
    {
        bool passed = true;
        foreach (BaseEdit edit in baseEdits)
        {
            if (!edit.ValidateEditNullError(dxErrorProvider, errorText, errorType))
                passed = false;
        }

        return passed;
    }

    /// <summary>
    /// 校验控件是否为空和长度合法性
    /// </summary>
    /// <param name="dxErrorProvider"></param>
    /// <param name="baseEdits">输入控件集合</param>
    /// <returns></returns>
    public static bool ValidateEditNullOrRange(this BaseEdit baseEdit, DXErrorProvider dxErrorProvider, bool checkNull,
        int max, int min = 0, string? errorText = null, ErrorType errorType = ErrorType.Critical)
    {
        return checkNull switch
        {
            true when max < 1 => baseEdit.ValidateEditNullError(dxErrorProvider),
            false when max > 0 => baseEdit.ValidateRange(dxErrorProvider, max, min, errorText),
            _ => baseEdit.ValidateEditNullError(dxErrorProvider) &&
                 baseEdit.ValidateRange(dxErrorProvider, max, min, errorText, errorType)
        };
    }

    /// <summary>
    /// 检验输入控件是否没有输入，为空则显示错误图标
    /// </summary>
    /// <param name="baseEdit">输入控件</param>
    /// <param name="dxErrorProvider"></param>
    /// <param name="checkNull">是否验证空值</param>
    /// <param name="max">验证长度，0为不验证</param>
    /// <param name="errorText">错误提示</param>
    /// <param name="errorType">默认错误类型</param>
    /// <returns></returns>
    public static bool ValidateEditNullError(this BaseEdit baseEdit, DXErrorProvider dxErrorProvider, 
        string errorText = "不能为空", ErrorType errorType = ErrorType.Critical)
    {
        string Func()
        {
            string editValue;
            if (baseEdit is SearchLookUpEdit && baseEdit.EditValue is long l)
                editValue = l == 0 ? string.Empty : l.ToString();
            else if (baseEdit is ComboBoxEdit && baseEdit.EditValue is CListItem item)
                editValue = item.Value;
            else
                editValue = string.Concat(baseEdit.EditValue).Trim();
            return editValue.IsNullOrEmpty() || editValue is "无" or "请选择"
                ? JsonLanguage.Default.GetString(errorText)
                : string.Empty;
        }

        if (!dxErrorProvider.ValidateEdit(baseEdit, Func, errorType, true))
        {
            errorText.ShowErrorTip(baseEdit);
            baseEdit.Focus();
            baseEdit.SelectAll();
            return false;
        }

        return true;
    }

    /// <summary>
    /// 检验输入的长度是否合法
    /// </summary>
    /// <param name="baseEdit">输入控件</param>
    /// <param name="dxErrorProvider"></param>
    /// <param name="max"></param>
    /// <param name="min"></param>
    /// <param name="errorText">错误提示</param>
    /// <param name="errorType">默认错误类型</param>
    /// <returns></returns>
    public static bool ValidateRange(this BaseEdit baseEdit, DXErrorProvider dxErrorProvider, int max, int min = 0, 
        string? errorText = null, ErrorType errorType = ErrorType.Critical)
    {
        string Func()
        {
            string editValue;
            if (baseEdit is SearchLookUpEdit && baseEdit.EditValue is long l)
                editValue = l == 0 ? string.Empty : l.ToString();
            else if (baseEdit is ComboBoxEdit && baseEdit.EditValue is CListItem item)
                editValue = item.Value;
            else if (baseEdit is ToggleSwitch && baseEdit.EditValue is bool b)
                editValue = b ? "1" : "0";
            else
                editValue = string.Concat(baseEdit.EditValue).Trim();
            return !editValue.IsNullOrEmpty() && editValue is not "无" or "请选择" && !ValidateUtil.IsRange(editValue, min, max)
                ? JsonLanguage.Default.GetString(errorText ?? $"内容长度必须在{min}-{max}之间")
                : string.Empty;
        }

        if (!dxErrorProvider.ValidateEdit(baseEdit, Func, errorType, true))
        {
            baseEdit.Focus();
            baseEdit.SelectAll();
            return false;
        }

        return true;
    }

    /// <summary>
    /// 校验控件，通过返回true【未启用】
    /// </summary>
    /// <param name="dxErrorProvider"></param>
    /// <param name="baseEdit">输入控件集合</param>
    /// <param name="validateFunc">匿名委托，通过返回string.Empty,否则返回错误提示</param>
    /// <param name="errorType">默认错误类型</param>
    /// <param name="realTime">实时验证</param>
    /// <returns></returns>
    public static bool ValidateEdit(this DXErrorProvider dxErrorProvider, BaseEdit baseEdit, ValidateType validateType, 
        bool checkNull = false, int max = 0, int min = 0, string? errorText = null, ErrorType errorType = ErrorType.Critical)
    {
        return validateType switch
        {
            ValidateType.UserName => baseEdit.ValidateUserName(dxErrorProvider,checkNull, max, errorText, errorType),
            ValidateType.Phone => baseEdit.ValidatePhone(dxErrorProvider, checkNull, max, errorText, errorType),
            ValidateType.Email => baseEdit.ValidateEmail(dxErrorProvider, checkNull, max, errorText, errorType),
            ValidateType.URL => baseEdit.ValidateURL(dxErrorProvider, checkNull, max, errorText, errorType),
            ValidateType.Number => baseEdit.ValidateNumber(dxErrorProvider, checkNull, errorText, errorType),
            ValidateType.Mobile => baseEdit.ValidateMobile(dxErrorProvider, checkNull, max, errorText, errorType),
            ValidateType.IdCard => baseEdit.ValidateIdCard(dxErrorProvider, checkNull, max, errorText, errorType),
            ValidateType.Chinese => baseEdit.ValidateChinese(dxErrorProvider, checkNull, max, errorText, errorType),
            ValidateType.Decimal => baseEdit.ValidateDecimal(dxErrorProvider, checkNull, errorText, errorType),
            ValidateType.Letter => baseEdit.ValidateLetter(dxErrorProvider, checkNull, max, errorText, errorType),
            ValidateType.Numeric => baseEdit.ValidateNumeric(dxErrorProvider, checkNull, errorText, errorType),
            ValidateType.FilePath => baseEdit.ValidateFilePath(dxErrorProvider, checkNull, max, errorText, errorType),
            ValidateType.PhoneAndMobile => baseEdit.ValidatePhoneAndMobile(dxErrorProvider, checkNull, max, errorText, errorType),
            _ => baseEdit.ValidateEditNullOrRange(dxErrorProvider, checkNull, max, min, errorText, errorType)
        };
    }

    /// <summary>
    /// 检验用户名格式是否有效
    /// </summary>
    /// <param name="baseEdit">输入控件</param>
    /// <param name="dxErrorProvider"></param>
    /// <param name="checkNull">是否验证空值</param>
    /// <param name="max">验证长度，0为不验证</param>
    /// <param name="errorText">错误提示</param>
    /// <param name="errorType">默认错误类型</param>
    /// <returns></returns>
    public static bool ValidateUserName(this BaseEdit baseEdit, DXErrorProvider dxErrorProvider, bool checkNull = false,
        int max = 0, string? errorText = null, ErrorType errorType = ErrorType.Critical)
    {
        return ValidateBaseEdit(baseEdit, dxErrorProvider, checkNull, max, ValidateUtil.IsValidUserName, errorText ?? "用户名不合法", errorType);
    }

    /// <summary>
    /// 检验输入是否是数字
    /// </summary>
    /// <param name="baseEdit">输入控件</param>
    /// <param name="dxErrorProvider"></param>
    /// <param name="checkNull">是否验证空值</param>
    /// <param name="max">验证长度，0为不验证</param>
    /// <param name="errorText">错误提示</param>
    /// <param name="errorType">默认错误类型</param>
    /// <returns></returns>
    public static bool ValidateNumeric(this BaseEdit baseEdit, DXErrorProvider dxErrorProvider, bool checkNull = false, 
        string? errorText = null, ErrorType errorType = ErrorType.Critical)
    {
        return ValidateBaseEdit(baseEdit, dxErrorProvider, checkNull, 0, ValidateUtil.IsNumeric, errorText ?? "请输入与数字", errorType);
    }

    /// <summary>
    /// 检验输入是否是整数
    /// </summary>
    /// <param name="baseEdit">输入控件</param>
    /// <param name="dxErrorProvider"></param>
    /// <param name="checkNull">是否验证空值</param>
    /// <param name="max">验证长度，0为不验证</param>
    /// <param name="errorText">错误提示</param>
    /// <param name="errorType">默认错误类型</param>
    /// <returns></returns>
    public static bool ValidateNumber(this BaseEdit baseEdit, DXErrorProvider dxErrorProvider, bool checkNull = false,
        string? errorText = null, ErrorType errorType = ErrorType.Critical)
    {
        return ValidateBaseEdit(baseEdit, dxErrorProvider, checkNull, 0, ValidateUtil.IsNumberSign, errorText ?? "请输入整数", errorType);
    }

    /// <summary>
    /// 检验输入是否是小数
    /// </summary>
    /// <param name="baseEdit">输入控件</param>
    /// <param name="dxErrorProvider"></param>
    /// <param name="checkNull">是否验证空值</param>
    /// <param name="max">验证长度，0为不验证</param>
    /// <param name="errorText">错误提示</param>
    /// <param name="errorType">默认错误类型</param>
    /// <returns></returns>
    public static bool ValidateDecimal(this BaseEdit baseEdit, DXErrorProvider dxErrorProvider, bool checkNull = false,
        string? errorText = null, ErrorType errorType = ErrorType.Critical)
    {
        return ValidateBaseEdit(baseEdit, dxErrorProvider, checkNull, 0, ValidateUtil.IsDecimal, errorText ?? "请输入小数", errorType);
    }

    /// <summary>
    /// 检验输入是否包含中文
    /// </summary>
    /// <param name="baseEdit">输入控件</param>
    /// <param name="dxErrorProvider"></param>
    /// <param name="checkNull">是否验证空值</param>
    /// <param name="max">验证长度，0为不验证</param>
    /// <param name="errorText">错误提示</param>
    /// <param name="errorType">默认错误类型</param>
    /// <returns></returns>
    public static bool ValidateChinese(this BaseEdit baseEdit, DXErrorProvider dxErrorProvider, bool checkNull = false,
        int max = 0, string? errorText = null, ErrorType errorType = ErrorType.Critical)
    {
        return ValidateBaseEdit(baseEdit, dxErrorProvider, checkNull, max, ValidateUtil.IsChinese, errorText ?? "请输入中文", errorType);
    }

    /// <summary>
    /// 检验输入是否纯字母
    /// </summary>
    /// <param name="baseEdit">输入控件</param>
    /// <param name="dxErrorProvider"></param>
    /// <param name="checkNull">是否验证空值</param>
    /// <param name="max">验证长度，0为不验证</param>
    /// <param name="errorText">错误提示</param>
    /// <param name="errorType">默认错误类型</param>
    /// <returns></returns>
    public static bool ValidateLetter(this BaseEdit baseEdit, DXErrorProvider dxErrorProvider, bool checkNull = false,
        int max = 0, string? errorText = null, ErrorType errorType = ErrorType.Critical)
    {
        return ValidateBaseEdit(baseEdit, dxErrorProvider, checkNull, max, ValidateUtil.IsLetter, errorText ?? "请输入字母", errorType);
    }

    /// <summary>
    /// 检验输入是否是身份证
    /// </summary>
    /// <param name="baseEdit">输入控件</param>
    /// <param name="dxErrorProvider"></param>
    /// <param name="checkNull">是否验证空值</param>
    /// <param name="max">验证长度，0为不验证</param>
    /// <param name="errorText">错误提示</param>
    /// <param name="errorType">默认错误类型</param>
    /// <returns></returns>
    public static bool ValidateIdCard(this BaseEdit baseEdit, DXErrorProvider dxErrorProvider, bool checkNull = false,
        int max = 0, string? errorText = null, ErrorType errorType = ErrorType.Critical)
    {
        return ValidateBaseEdit(baseEdit, dxErrorProvider, checkNull, max, ValidateUtil.IsIdCard, errorText ?? "身份证不合法", errorType);
    }

    /// <summary>
    /// 检验输入是否是邮件地址
    /// </summary>
    /// <param name="baseEdit">输入控件</param>
    /// <param name="dxErrorProvider"></param>
    /// <param name="checkNull">是否验证空值</param>
    /// <param name="max">验证长度，0为不验证</param>
    /// <param name="errorText">错误提示</param>
    /// <param name="errorType">默认错误类型</param>
    /// <returns></returns>
    public static bool ValidateEmail(this BaseEdit baseEdit, DXErrorProvider dxErrorProvider, bool checkNull = false,
        int max = 0, string? errorText = null, ErrorType errorType = ErrorType.Critical)
    {
        return ValidateBaseEdit(baseEdit, dxErrorProvider, checkNull, max, ValidateUtil.IsEmail, errorText ?? "邮箱地址不合法", errorType);
    }

    /// <summary>
    /// 检验输入是否是固定电话
    /// </summary>
    /// <param name="baseEdit">输入控件</param>
    /// <param name="dxErrorProvider"></param>
    /// <param name="checkNull">是否验证空值</param>
    /// <param name="max">验证长度，0为不验证</param>
    /// <param name="errorText">错误提示</param>
    /// <param name="errorType">默认错误类型</param>
    /// <returns></returns>
    public static bool ValidatePhone(this BaseEdit baseEdit, DXErrorProvider dxErrorProvider, bool checkNull = false,
        int max = 0, string? errorText = null, ErrorType errorType = ErrorType.Critical)
    {
        return ValidateBaseEdit(baseEdit, dxErrorProvider, checkNull, max, ValidateUtil.IsPhone, errorText ?? "请输入正确的电话号码", errorType);
    }

    /// <summary>
    /// 检验输入是否是手机
    /// </summary>
    /// <param name="baseEdit">输入控件</param>
    /// <param name="dxErrorProvider"></param>
    /// <param name="checkNull">是否验证空值</param>
    /// <param name="max">验证长度，0为不验证</param>
    /// <param name="errorText">错误提示</param>
    /// <param name="errorType">默认错误类型</param>
    /// <returns></returns>
    public static bool ValidateMobile(this BaseEdit baseEdit, DXErrorProvider dxErrorProvider, bool checkNull = false,
        int max = 0, string? errorText = null, ErrorType errorType = ErrorType.Critical)
    {
        return ValidateBaseEdit(baseEdit, dxErrorProvider, checkNull, max, ValidateUtil.IsMobile, errorText ?? "请输入正确的手机号码", errorType);
    }

    /// <summary>
    /// 检验输入是否是固话和手机
    /// </summary>
    /// <param name="baseEdit">输入控件</param>
    /// <param name="dxErrorProvider"></param>
    /// <param name="checkNull">是否验证空值</param>
    /// <param name="max">验证长度，0为不验证</param>
    /// <param name="errorText">错误提示</param>
    /// <param name="errorType">默认错误类型</param>
    /// <returns></returns>
    public static bool ValidatePhoneAndMobile(this BaseEdit baseEdit, DXErrorProvider dxErrorProvider, bool checkNull = false,
        int max = 0, string? errorText = null, ErrorType errorType = ErrorType.Critical)
    {
        return ValidateBaseEdit(baseEdit, dxErrorProvider, checkNull, max, ValidateUtil.IsPhoneAndMobile, errorText ?? "请输入正确的联系方式", errorType);
    }

    /// <summary>
    /// 检验输入是否是URL
    /// </summary>
    /// <param name="baseEdit">输入控件</param>
    /// <param name="dxErrorProvider"></param>
    /// <param name="checkNull">是否验证空值</param>
    /// <param name="max">验证长度，0为不验证</param>
    /// <param name="errorText">错误提示</param>
    /// <param name="errorType">默认错误类型</param>
    /// <returns></returns>
    public static bool ValidateURL(this BaseEdit baseEdit, DXErrorProvider dxErrorProvider, bool checkNull = false,
        int max = 0, string? errorText = null, ErrorType errorType = ErrorType.Critical)
    {
        return ValidateBaseEdit(baseEdit, dxErrorProvider, checkNull, max, ValidateUtil.IsUrl, errorText ?? "请输入正确的联系方式", errorType);
    }

    /// <summary>
    /// 检验输入是否是文件路径
    /// </summary>
    /// <param name="baseEdit">输入控件</param>
    /// <param name="dxErrorProvider"></param>
    /// <param name="checkNull">是否验证空值</param>
    /// <param name="max">验证长度，0为不验证</param>
    /// <param name="errorText">错误提示</param>
    /// <param name="errorType">默认错误类型</param>
    /// <returns></returns>
    public static bool ValidateFilePath(this BaseEdit baseEdit, DXErrorProvider dxErrorProvider, bool checkNull = false,
        int max = 0, string? errorText = null, ErrorType errorType = ErrorType.Critical)
    {
        return ValidateBaseEdit(baseEdit, dxErrorProvider, checkNull, max, ValidateUtil.IsFilePath, errorText ?? "请输入正确的联系方式", errorType);
    }

    /// <summary>
    /// 校验输入控件是否为空，为空则提示对话框
    /// </summary>
    /// <param name="baseEdit">输入控件</param>
    /// <returns></returns>
    public static bool ValidateEditNull(this BaseEdit baseEdit)
    {
        bool result;
        if (string.IsNullOrEmpty(baseEdit.Text) || baseEdit.Text == "无" || baseEdit.Text == "请选择")
        {
            $"{baseEdit.Tag}不能为空".ShowErrorTip(baseEdit);
            baseEdit.Focus();
            result = true;
        }
        else
        {
            result = false;
        }
        return result;
    }
    
    #endregion

    #region Panel只读和清空操作

    /// <summary>
    /// 设置控件的可见、读写权限显示
    /// </summary>
    /// <param name="panel">控件对象</param>
    /// <param name="permitDict">字段和权限字典，字典值为权限控制：0可读写，1只读，2隐藏值，3不显示</param>
    /// <param name="layoutControl">如果存在布局，则使用布局控件，否则为空</param>
    public static void SetControlPermit(this System.Windows.Forms.Control panel, Dictionary<string, int> permitDict, LayoutControl layoutControl = null)
    {
        foreach (System.Windows.Forms.Control ctrl in panel.Controls)
        {
            BaseEdit baseCtrl = ctrl as BaseEdit;
            if (baseCtrl != null)
            {
                var tag = string.Concat(baseCtrl.Tag);
                if (!string.IsNullOrEmpty(tag) && permitDict.ContainsKey(tag))
                {
                    var permit = permitDict[tag];
                    var visible = (permit == 0 || permit == 1);//2、3不可见

                    if (layoutControl != null)
                    {
                        var layoutItem = layoutControl.GetItemByControl(baseCtrl);
                        if (layoutItem != null)
                        {
                            layoutItem.ToVisibility(visible);
                        }
                    }
                    baseCtrl.Visible = visible;
                    baseCtrl.ReadOnly = permit == 1;
                }
            }
            ctrl.SetControlPermit(permitDict, layoutControl);
        }
    }

    /// <summary>
    /// 设置Edit控件或指定Panel内的Edit控件为只读
    /// </summary>
    /// <param name="panel">Edit控件或控件面板</param>
    /// <param name="readOnly">是否只读，true为只读</param>
    public static void SetPanelReadOnly(this System.Windows.Forms.Control panel, bool readOnly = true)
    {
        if (panel is BaseEdit e)
        {
            e.Properties.ReadOnly = readOnly;
        }
        else
        {
            foreach (System.Windows.Forms.Control c in panel.Controls)
            {
                if (c is BaseEdit edit)
                {
                    edit.Properties.ReadOnly = readOnly;
                }
                c.SetPanelReadOnly(readOnly);
            }
        }
    }

    /// <summary>
    /// 清除指定Panel内的控件的值
    /// </summary>
    /// <param name="panel">控件面板</param>
    public static void ClearPanelEditValue(this System.Windows.Forms.Control panel)
    {
        foreach (System.Windows.Forms.Control c in panel.Controls)
        {
            if (c is BaseEdit)
            {
                (c as BaseEdit).EditValue = null;
            }
            ClearPanelEditValue(c);
        }
    } 
    #endregion

    #region 日期控件

    /// <summary>
    /// 设置时间格式有效显示，如果大于默认时间，赋值给控件；否则不赋值
    /// </summary>
    /// <param name="control">DateEdit控件对象</param>
    /// <param name="dateTime">日期对象</param>
    public static void SetDateTime(this DateEdit control, DateTime dateTime)
    {
        if (dateTime > Convert.ToDateTime("1900-1-1"))
        {
            control.DateTime = dateTime;
        }
        else
        {
            control.Text = "";
        }
    }

    /// <summary>
    /// 设置时间格式有效显示，如果大于默认时间，赋值给控件；否则不赋值
    /// </summary>
    /// <param name="control">DateEdit控件对象</param>
    /// <param name="dateTime">日期对象</param>
    public static void SetDateTime(this DateEdit control, DateTime? dateTime)
    {
        if(dateTime.HasValue)
        {
            if (dateTime > Convert.ToDateTime("1900-1-1"))
            {
                control.DateTime = dateTime.Value;
            }
            else
            {
                control.Text = "";
            }
        }
    }

    #endregion

    #region 控件布局显示

    /// <summary>
    /// 设置控件组是否显示
    /// </summary>
    /// <returns></returns>
    public static void ToVisibility(this LayoutControlGroup control, bool visible)
    {
        if (visible)
        {
            control.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
        }
        else
        {
            control.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }
    }

    /// <summary>
    /// 获取控件组是否为显示状态
    /// </summary>
    /// <param name="control"></param>
    /// <returns></returns>
    public static bool GetVisibility(this LayoutControlGroup control)
    {
        return control.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
    }

    /// <summary>
    /// 设置控件组是否显示
    /// </summary>
    /// <returns></returns>
    public static void ToVisibility(this LayoutControlItem control, bool visible)
    {
        if (visible)
        {
            control.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
        }
        else
        {
            control.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }
    }

    /// <summary>
    /// 获取控件组是否为显示状态
    /// </summary>
    /// <param name="control"></param>
    /// <returns></returns>
    public static bool GetVisibility(this LayoutControlItem control)
    {
        return control.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
    }
    /// <summary>
    /// 设置控件组是否显示
    /// </summary>
    /// <returns></returns>
    public static void ToVisibility(this EmptySpaceItem control, bool visible)
    {
        if (visible)
        {
            control.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
        }
        else
        {
            control.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }
    }

    /// <summary>
    /// 获取控件组是否为显示状态
    /// </summary>
    /// <param name="control"></param>
    /// <returns></returns>
    public static bool GetVisibility(this EmptySpaceItem control)
    {
        return control.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
    }

    /// <summary>
    /// 设置控件组是否显示
    /// </summary>
    /// <returns></returns>
    public static void ToVisibility(this BarButtonItem control, bool visible)
    {
        if (visible)
        {
            control.Visibility = BarItemVisibility.Always;
        }
        else
        {
            control.Visibility = BarItemVisibility.Never;
        }
    }

    #endregion

    #region CheckEdit控件默认设置

    /// <summary>
    /// 绑定Check控件为YN
    /// </summary>
    /// <param name="combo">Check控件</param>
    public static void BindDictItems(this CheckEdit combo)
    {
        BindDictItems(combo, "Y", "N", "N");
    }

    /// <summary>
    /// 绑定Check控件
    /// </summary>
    /// <param name="combo">Check控件</param>
    /// <param name="valueChecked">勾选时的值</param>
    /// <param name="valueGrayed">不确定时的值</param>
    /// <param name="valueUnchecked">取消勾选时的值</param>
    /// <param name="defaultValue">默认状态</param>
    public static void BindDictItems(this CheckEdit combo, object valueChecked, object valueGrayed, object valueUnchecked, CheckState defaultValue = CheckState.Unchecked)
    {
        combo.Properties.ValueChecked = valueChecked;
        combo.Properties.ValueGrayed = valueGrayed;
        combo.Properties.ValueUnchecked = valueUnchecked;
        combo.CheckState = defaultValue;
    }

    #endregion
        
    #region ToggleSwitch控件默认设置

    /// <summary>
    /// 设置ToggleSwitch控件的开关提示信息
    /// </summary>
    /// <param name="combo">ToggleSwitch控件</param>
    /// <param name="onText">开启时显示</param>
    /// <param name="offText">关闭时显示</param>
    /// <param name="defaultValue">默认值</param>
    public static void BindDictItems(this ToggleSwitch combo, string onText, string offText, bool defaultValue = false)
    {
        combo.Properties.OffText = offText;
        combo.Properties.OnText = onText;
        combo.IsOn = defaultValue;
    }

    /// <summary>
    /// 设置ToggleSwitch控件的开关提示信息
    /// </summary>
    /// <param name="combo">ToggleSwitch控件</param>
    /// <param name="switchText">开关文本，英文逗号分隔</param>
    /// <param name="defaultValue">默认值</param>
    public static void BindDictItems(this ToggleSwitch combo, string switchText, bool defaultValue = false)
    {
        string[] switchTexts = switchText.Split(',');
        combo.Properties.OnText = switchTexts[0];
        combo.Properties.OffText = switchTexts[1];
        combo.IsOn = defaultValue;
    }

    #endregion
        
    #region ListBoxControl控件默认设置

    /// <summary>
    /// 绑定列表控件为指定的数据字典列表
    /// </summary>
    /// <param name="control"></param>
    /// <param name="dictTypeName"></param>
    public static void BindDictItems(this ListBoxControl control, string dictTypeName)
    {
        control.Items.Clear();
        control.Items.AddRange(GB.GetDictByName(dictTypeName).ToArray());
    }

    #endregion

    #region BarAdd

    /// <summary>
    /// 新增按钮
    /// </summary>
    /// <param name="bar">工具条</param>
    /// <param name="name">按钮名称</param>
    /// <param name="caption">按钮显式名称</param>
    /// <param name="imageId">按钮图标</param>
    /// <param name="itemClick">按钮点击事件委托</param>
    /// <param name="barShortcut">按钮快捷键</param>
    /// <param name="beginGroup">新分组</param>
    /// <param name="alignment">按钮对齐方式</param>
    /// <param name="verifyPermissions">验证权限</param>
    /// <param name="tag">标签</param>
    /// <returns></returns>
    public static BarButtonItem AddBarButtonItem(this Bar bar, string name, string caption, 
        string imageId, ItemClickEventHandler itemClick, BarShortcut? barShortcut = null, bool beginGroup = false,
        BarItemLinkAlignment alignment = BarItemLinkAlignment.Default, bool verifyPermissions = true,
        object? tag = null)
    {
        var barButton = new BarButtonItem()
        {
            Caption = caption,
            Name = name,
            ItemShortcut = barShortcut ?? BarShortcut.Empty,
            LargeGlyph = ImageResourceCache.Default.GetImageById(imageId, ImageSize.Size32x32, ImageType.Colored),
            Glyph = ImageResourceCache.Default.GetImageById(imageId, ImageSize.Size16x16, ImageType.Colored),
            PaintStyle = BarItemPaintStyle.CaptionGlyph,
            Visibility = verifyPermissions ? BarItemVisibility.Never : BarItemVisibility.Always,
            Alignment = alignment,
            Tag = tag
        };
        barButton.ItemClick += itemClick;
        bar.AddItem(barButton).BeginGroup = beginGroup;

        if (verifyPermissions) GB.HasFunction(barButton);
        return barButton;
    }

    /// <summary>
    /// 新增勾选框
    /// </summary>
    /// <param name="bar">工具条</param>
    /// <param name="name">按钮名称</param>
    /// <param name="caption">按钮显式名称</param>
    /// <param name="imageId">按钮图标</param>
    /// <param name="itemClick">按钮点击事件委托</param>
    /// <param name="barShortcut">按钮快捷键</param>
    /// <param name="beginGroup">新分组</param>
    /// <param name="alignment">按钮对齐方式</param>
    /// <param name="verifyPermissions">验证权限</param>
    /// <param name="tag">标签</param>
    /// <returns></returns>
    public static BarCheckItem AddBarCheckItem(this Bar bar, string name, string caption, 
        string imageId, ItemClickEventHandler itemClick, BarShortcut? barShortcut = null, bool beginGroup = false,
        BarItemLinkAlignment alignment = BarItemLinkAlignment.Default, bool verifyPermissions = true,
        object? tag = null)
    {
        var barCheck = new BarCheckItem()
        {
            Caption = caption,
            Name = name,
            ItemShortcut = barShortcut ?? BarShortcut.Empty,
            LargeGlyph = ImageResourceCache.Default.GetImageById(imageId, ImageSize.Size32x32, ImageType.Colored),
            Glyph = ImageResourceCache.Default.GetImageById(imageId, ImageSize.Size16x16, ImageType.Colored),
            PaintStyle = BarItemPaintStyle.CaptionGlyph,
            Visibility = verifyPermissions ? BarItemVisibility.Never : BarItemVisibility.Always,
            Alignment = alignment,
            Tag = tag
        };
        barCheck.CheckedChanged += itemClick;
        bar.AddItem(barCheck).BeginGroup = beginGroup;

        if (verifyPermissions) GB.HasFunction(barCheck);
        return barCheck;
    }

    /// <summary>
    /// 新增条形开关
    /// </summary>
    /// <param name="bar">工具条</param>
    /// <param name="name">按钮名称</param>
    /// <param name="caption">按钮显式名称</param>
    /// <param name="imageId">按钮图标</param>
    /// <param name="itemClick">按钮点击事件委托</param>
    /// <param name="barShortcut">按钮快捷键</param>
    /// <param name="beginGroup">新分组</param>
    /// <param name="alignment">按钮对齐方式</param>
    /// <param name="verifyPermissions">验证权限</param>
    /// <param name="tag">标签</param>
    /// <returns></returns>
    public static BarToggleSwitchItem AddBarSwitchItem(this Bar bar, string name, string caption, 
        string imageId, ItemClickEventHandler itemClick, BarShortcut? barShortcut = null, bool beginGroup = false,
        BarItemLinkAlignment alignment = BarItemLinkAlignment.Default, bool verifyPermissions = true,
        object? tag = null)
    {
        var barSwitch = new BarToggleSwitchItem()
        {
            Caption = caption,
            Name = name,
            ItemShortcut = barShortcut ?? BarShortcut.Empty,
            LargeGlyph = ImageResourceCache.Default.GetImageById(imageId, ImageSize.Size32x32, ImageType.Colored),
            Glyph = ImageResourceCache.Default.GetImageById(imageId, ImageSize.Size16x16, ImageType.Colored),
            PaintStyle = BarItemPaintStyle.CaptionGlyph,
            Visibility = verifyPermissions ? BarItemVisibility.Never : BarItemVisibility.Always,
            Alignment = alignment,
            Tag = tag
        };
        barSwitch.CheckedChanged += itemClick;
        bar.AddItem(barSwitch).BeginGroup = beginGroup;

        if (verifyPermissions) GB.HasFunction(barSwitch);
        return barSwitch;
    }

    #endregion
    
    #region 查询区间
    /// <summary>
    /// 添加数值区间的查询操作
    /// </summary>
    /// <param name="condition">SearchCondition对象</param>
    /// <param name="fieldName">字段名称</param>
    /// <param name="startCtrl">开始范围控件</param>
    /// <param name="endCtrl">结束范围控件</param>
    /// <returns></returns>
    public static SearchCondition AddNumericCondition(this SearchCondition condition, string fieldName, SpinEdit startCtrl, SpinEdit endCtrl)
    {
        if (startCtrl.Text.Length > 0)
        {
            condition.AddCondition(fieldName, startCtrl.Value, SqlOperator.MoreThanOrEqual);
        }
        if (endCtrl.Text.Length > 0)
        {
            condition.AddCondition(fieldName, endCtrl.Value, SqlOperator.LessThanOrEqual);
        }
        return condition;
    }

    public static SearchCondition AddNumericCondition(this SearchCondition condition, string fieldName, TextEdit startCtrl, TextEdit endCtrl)
    {
        decimal value = 0;
        if (decimal.TryParse(startCtrl.Text.Trim(), out value))
        {
            condition.AddCondition(fieldName, value, SqlOperator.MoreThanOrEqual);
        }
        if (decimal.TryParse(endCtrl.Text.Trim(), out value))
        {
            condition.AddCondition(fieldName, value, SqlOperator.LessThanOrEqual);
        }
        return condition;
    }

    public static SearchCondition AddNumericCondition2(this SearchCondition condition, string fieldName, TextEdit startCtrl, TextEdit endCtrl)
    {
        decimal value = 0;
        int hour = 0;
        int minute = 0;
        decimal hourMinute = 0;
        if (decimal.TryParse(startCtrl.Text.Trim(), out value))
        {
            hour = (int)value;
            hourMinute = hour * 60;
            string[] startValue = startCtrl.Text.Split('.');
            if (int.TryParse(startValue[1].Trim(), out minute))
            {
                hourMinute += minute;
            }
            condition.AddCondition(fieldName, hourMinute, SqlOperator.MoreThanOrEqual);
        }
        if (decimal.TryParse(endCtrl.Text.Trim(), out value))
        {
            hour = (int)value;
            hourMinute = hour * 60;
            string[] endValue = endCtrl.Text.Split('.');
            if (int.TryParse(endValue[1].Trim(), out minute))
            {
                hourMinute += minute;
            }
            condition.AddCondition(fieldName, hourMinute, SqlOperator.LessThanOrEqual);
        }
        return condition;
    }
    #endregion
}