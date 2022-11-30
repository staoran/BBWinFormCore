using System.Data;
using BB.BaseUI.Other;
using BB.Tools.Const;
using BB.Tools.Extension;
using BB.Tools.Format;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Card;
using DevExpress.XtraLayout;
using DevExpress.XtraTreeList;

namespace BB.BaseUI.Extension;

/// <summary>
/// 双向绑定工具
/// </summary>
public static class DataBinderTools
{
    public static void DoBindingEditorPanel(this LayoutControlGroup editorGroup, object dataSource, string head)
    {
        List<string> propertyInfos =  ReflectionExtension.GetProperties(dataSource).Select(x=>x.Name).ToList();
        editorGroup.Items.ForEach(x =>
        {
            if (x is not LayoutControlItem ctl || ctl.Name.IsNullOrEmpty()) return;
            DoBindingEditorPanel(ctl.Control, dataSource, head, propertyInfos);
        });
    }
        
    public static void DoBindingEditorPanel(System.Windows.Forms.Control editorPanel, object dataSource,
        string head, List<string> propertyNames)
    {
        string editorName = editorPanel.Name;
        int length = head.Length;
        try
        {
            if (editorName.Substring(0, length) == head)
            {
                string fieldName = editorName.Substring(length, editorName.Length - length);
                if (!propertyNames.Contains(fieldName)) return;
                switch (editorPanel)
                {
                    case CheckEdit checkEdit:
                        BindingCheckEdit(checkEdit, dataSource, fieldName);
                        break;
                    case ComboBoxEdit comboBoxEdit:
                        BindingComboEdit(comboBoxEdit, dataSource, fieldName);
                        break;
                    case RadioGroup radioGroup:
                        BindingRadioEdit(radioGroup, dataSource, fieldName);
                        break;
                    case CheckedListBoxControl boxControl:
                        BindingCheckedListBox(boxControl, dataSource, fieldName);
                        break;
                    case PictureEdit pictureEdit:
                        BindingImageEdit(pictureEdit, dataSource, fieldName);
                        break;
                    case ToggleSwitch toggleSwitch:
                        BindingToggleSwitch(toggleSwitch, dataSource, fieldName);
                        break;
                    case DateEdit dateEdit:
                        BindingDateEdit(dateEdit, dataSource, fieldName);
                        break;
                    case TimeEdit dateEdit:
                        BindingTimeEdit(dateEdit, dataSource, fieldName);
                        break;
                    case TextEdit textEdit:
                        BindingTextEdit(textEdit, dataSource, fieldName);
                        break;
                    case BaseEdit baseEdit:
                        BindingTextEditBase(baseEdit, dataSource, fieldName);
                        break;
                }
            }
            else if (editorPanel.Controls.Count > 0)
            {
                foreach (System.Windows.Forms.Control editorPanelControl in editorPanel.Controls)
                {
                    DoBindingEditorPanel(editorPanelControl, dataSource, head, propertyNames);
                }
            }
        }
        catch (Exception ex)
        {
            $"控件:{editorName}\r\n{ex.Message}".ShowUxWarning();
        }
    }

    /// <summary>
    /// 设置控件状态.ReadOnly or Enable = false/true
    /// </summary>
    public static void SetControlAccessable(System.Windows.Forms.Control control, bool value)
    {
        try
        {
            switch (control)
            {
                case Label _:
                case LabelControl _:
                case ControlNavigator _:
                    return;
                case UserControl userControl:
                    userControl.Enabled = value;
                    return;
                case BaseButton button:
                    button.Enabled = value;
                    return;
                case BaseEdit edit:
                    edit.Properties.ReadOnly = !value;
                    return;
                case TreeList list:
                    list.OptionsBehavior.Editable = value;
                    return;
                case GridControl gridControl:
                {
                    foreach (BaseView v in gridControl.Views)
                    {
                        if (v is ColumnView)
                        {
                            (v as ColumnView).OptionsBehavior.Editable = value;
                        }

                        if (v is CardView)
                            (v as CardView).OptionsBehavior.Editable = value;
                    }
                    break;
                }
            }

            if (control.Controls.Count > 0)
            {
                foreach (System.Windows.Forms.Control c in control.Controls)
                    SetControlAccessable(c, value);
            }

            Type type = control.GetType();

            //PropertyInfo info = null;

            #region Old

            //PropertyInfo[] infos = type.GetProperties();
            //foreach (PropertyInfo info in infos)
            //{
            //    if (info.Name == "ReadOnly")//ReadOnly
            //    {
            //        info.SetValue(control, !value, null);
            //        return;
            //    }
            //    if (info.Name == "Properties")//Properties.ReadOnly
            //    {
            //        object o = info.GetValue(control, null);
            //        if (o is RepositoryItem)
            //        {
            //            ((RepositoryItem)o).ReadOnly = !value;
            //        }
            //        if ((o is RepositoryItemButtonEdit) && (((RepositoryItemButtonEdit)o).Buttons.Count > 0))
            //            ((RepositoryItemButtonEdit)o).Buttons[0].Enabled = value;
            //        if ((o is RepositoryItemDateEdit) && (((RepositoryItemDateEdit)o).Buttons.Count > 0))
            //            ((RepositoryItemDateEdit)o).Buttons[0].Enabled = value;
            //        return;
            //    }
            //    if (info.Name == "Views")//OptionsBehavior.Editable
            //    {
            //        object o = info.GetValue(control, null);
            //        if (null == o) return;
            //        foreach (object view in (GridControlViewCollection)o)
            //        {
            //            if (view is ColumnView)
            //                ((ColumnView)view).OptionsBehavior.Editable = value;
            //        }
            //        return;
            //    }

            //}

            #endregion
        }
        catch (Exception ex)
        {
            FrmException.ShowBug(ex);
        }
    }

    public static void SetViewEditable(ColumnView view, bool value)
    {
        view.OptionsBehavior.Editable = value;
    }

    /// <summary>
    /// 设置Grid自定义按钮(Add,Insert,Delete)状态
    /// </summary>
    public static void SetNavigatorButtonsEnable(GridControl gridControl, bool value)
    {
        //设置自带按钮
        foreach (NavigatorButton btn in gridControl.EmbeddedNavigator.Buttons.ButtonCollection) btn.Enabled = value;

        //设置按钮
        foreach (NavigatorCustomButton btn in gridControl.EmbeddedNavigator.Buttons.CustomButtons)
            btn.Enabled = value;
    }

    /// <summary>
    /// 设置GridNavigatorButtons可见
    /// </summary>
    public static void SetNavigatorButtonsVisable(GridControl gridControl, bool value)
    {
        //设置自带按钮
        foreach (NavigatorButton btn in gridControl.EmbeddedNavigator.Buttons.ButtonCollection) btn.Visible = value;

        //设置按钮
        foreach (NavigatorCustomButton btn in gridControl.EmbeddedNavigator.Buttons.CustomButtons)
            btn.Visible = value;
    }

    // public static void SetEditorBindingValue(System.Windows.Forms.Control edit, object value)
    // {
    //     ////edit.EditValue = value;
    //     if (edit.DataBindings.Count > 0)
    //     {
    //         DataSourceUpdateMode bk = edit.DataBindings[0].DataSourceUpdateMode;
    //         if (bk != DataSourceUpdateMode.OnPropertyChanged)
    //         {
    //
    //             edit.DataBindings[0].DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged;
    //             edit.GetType().GetProperty(edit.DataBindings[0].PropertyName)?.SetValue(edit, value, null);
    //             edit.DataBindings[0].DataSourceUpdateMode = bk;
    //
    //         }
    //         else
    //             edit.GetType().GetProperty(edit.DataBindings[0].PropertyName)?.SetValue(edit, value, null);
    //     }
    // }

    public static void DoClearPanel(System.Windows.Forms.Control editorPanel)
    {
        try
        {
            for (int i = 0; i <= editorPanel.Controls.Count - 1; i++)
            {

                if (editorPanel.Controls[i] is BaseEdit)
                    ((BaseEdit)editorPanel.Controls[i]).EditValue = "";

                else if (editorPanel.Controls[i].Controls.Count > 0)
                    DoClearPanel(editorPanel.Controls[i]);
            }
        }
        catch (Exception ex)
        {
            ex.Message.ShowUxWarning();
        }
    }

    public enum IntRuler
    {
        不判断,
        大于0,
        小于0,
        不能等于0
    }

    public static bool IsNotEmpBaseEdit(BaseEdit edit, string errorText = "", IntRuler ruler = IntRuler.大于0)
    {
        if (edit.Visible == false) return true;

        if ((edit.EditValue is Int32 || edit.EditValue is Decimal) && ruler != IntRuler.不判断)
        {
            bool tmp = false;
            switch (ruler)
            {
                case IntRuler.大于0:
                    tmp = Convert.ToDecimal(edit.EditValue) > 0;
                    break;
                case IntRuler.不能等于0:
                    tmp = Convert.ToDecimal(edit.EditValue) != 0;
                    break;
                case IntRuler.小于0:
                    tmp = Convert.ToDecimal(edit.EditValue) < 0;
                    break;
            }

            if (tmp == false)
            {
                edit.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                edit.ErrorText = errorText;

                edit.EditValueChanged -= edit_EditValueChanged;

                edit.EditValueChanged += edit_EditValueChanged;
                return false;
            }
        }

        if (string.IsNullOrEmpty(edit.EditValue.ToString()))
        {
            if (errorText != "")
            {
                edit.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                edit.ErrorText = errorText;

                edit.EditValueChanged -= edit_EditValueChanged;

                edit.EditValueChanged += edit_EditValueChanged;
            }
            return false;
        }

        return true;
    }

    static void edit_EditValueChanged(object? sender, EventArgs e)
    {
        ((BaseEdit)sender).ErrorText = string.Empty;
    }

    /// <summary>
    /// 统一更新配置项
    /// OnValidation 验证控件属性时，数据源将更新。验证后，控件属性中的值也会重新格式化。
    /// OnPropertyChanged 每当控件属性的值更改时，数据源都会更新。
    /// </summary>
    private const DataSourceUpdateMode CustomerDataSourceUpdateMode = DataSourceUpdateMode.OnValidation;

    /// <summary>
    /// 绑定RadioGroup组控件的数据源
    /// </summary>
    /// <param name="edit">RadioGroup组控件</param>
    /// <param name="dataSource">数据源</param>
    /// <param name="bindField">取值字段</param>
    public static void BindingRadioEdit(RadioGroup edit, object dataSource, string bindField)
    {
        try
        {
            edit.DataBindings.Clear();

            var b = new Binding("EditValue", dataSource, bindField)
            {
                DataSourceUpdateMode = CustomerDataSourceUpdateMode
            };
            edit.DataBindings.Add(b);
        }
        catch (Exception ex)
        {
            FrmException.ShowBug(ex);
        }
    }

    /// <summary>
    /// 绑定输入控件的数据源
    /// </summary>
    /// <param name="ctl">支持输入功能的控件</param>
    /// <param name="dataSource">数据源</param>
    /// <param name="bindField">取值字段</param>
    /// <param name="propertyName">控件的取值属性</param>
    public static void BindingControl(System.Windows.Forms.Control ctl, object dataSource, string bindField,
        string propertyName)
    {
        try
        {
            ctl.DataBindings.Clear();
            var b = new Binding(propertyName, dataSource, bindField)
            {
                DataSourceUpdateMode = CustomerDataSourceUpdateMode
            };
            ctl.DataBindings.Add(b);
        }
        catch (Exception ex)
        {
            FrmException.ShowBug(ex);
        }
    }

    /// <summary>
    /// 绑定输入控件的数据源
    /// </summary>
    /// <param name="edit">控件框</param>
    /// <param name="dataSource">数据源</param>
    /// <param name="bindField">取值字段</param>
    public static void BindingTextEditBase(BaseEdit edit, object dataSource, string bindField)
    {
        try
        {
            edit.DataBindings.Clear();
            var b = new Binding("EditValue", dataSource, bindField, true)
            {
                DataSourceUpdateMode = CustomerDataSourceUpdateMode
            };
            edit.DataBindings.Add(b);
            b.ReadValue();
        }
        catch (Exception ex)
        {
            FrmException.ShowBug(ex);
        }
    }

    /// <summary>
    /// 绑定输入控件的数据源
    /// </summary>
    /// <param name="edit">控件框</param>
    /// <param name="dataSource">数据源</param>
    /// <param name="bindField">取值字段</param>
    public static void BindingTextEdit(TextEdit edit, object dataSource, string bindField)
    {
        try
        {
            edit.DataBindings.Clear();
            var b = new Binding("EditValue", dataSource, bindField)
            {
                DataSourceUpdateMode = CustomerDataSourceUpdateMode
            };
            edit.DataBindings.Add(b);
        }
        catch (Exception ex)
        {
            FrmException.ShowBug(ex);
        }
    }

    public static void BindingTextEdit(TextEdit edit, object dataSource, string bindField, int rowPosition)
    {
        try
        {
            edit.DataBindings.Clear();
            var b = new Binding("EditValue", dataSource, bindField)
            {
                DataSourceUpdateMode = CustomerDataSourceUpdateMode
            };
            edit.DataBindings.Add(b);

            //指定资料行序号
            if (rowPosition >= 0) edit.BindingContext[dataSource].Position = rowPosition;
        }
        catch (Exception ex)
        {
            FrmException.ShowBug(ex);
        }
    }

    /// <summary>
    ///  绑定CheckedListBox的数据源
    /// </summary>
    /// <param name="control">CheckedListBox</param>
    /// <param name="dataSource">数据源</param>
    /// <param name="bindField">取值字段</param>
    public static void BindingCheckedListBox(CheckedListBoxControl control, object dataSource,
        string bindField)
    {
        try
        {
            control.DataBindings.Clear();
            var b = new Binding("SelectedValue", dataSource, bindField)
            {
                DataSourceUpdateMode = CustomerDataSourceUpdateMode
            };
            control.DataBindings.Add(b);
        }
        catch (Exception ex)
        {
            FrmException.ShowBug(ex);
        }
    }

    /// <summary>
    /// 绑定ComboBoxEdit的数据源
    /// </summary>
    /// <param name="edit">ComboBoxEdit</param>
    /// <param name="dataSource">数据源</param>
    /// <param name="bindField">取值字段</param>
    public static void BindingComboEdit(ComboBoxEdit edit, object dataSource, string bindField)
    {
        try
        {
            edit.DataBindings.Clear();
            var b = new Binding("EditValue", dataSource, bindField)
            {
                DataSourceNullValue = string.Empty,
                NullValue = string.Empty,
                DataSourceUpdateMode = CustomerDataSourceUpdateMode
            };
            edit.DataBindings.Add(b);
            
            edit.SelectedValueChanged += OnBindingEditValueChange;
        }
        catch (Exception ex)
        {
            FrmException.ShowBug(ex);
        }
    }

    /// <summary>
    /// 绑定CheckEdit的数据源
    /// </summary>
    /// <param name="edit">CheckEdit</param>
    /// <param name="dataSource">数据源</param>
    /// <param name="bindField">取值字段</param>
    public static void BindingCheckEdit(CheckEdit edit, object dataSource, string bindField)
    {
        try
        {
            edit.DataBindings.Clear();
            var b = new Binding("EditValue", dataSource, bindField, true)
            {
                DataSourceUpdateMode = CustomerDataSourceUpdateMode,
                DataSourceNullValue = string.Empty,
                NullValue = "N"
            };
            edit.DataBindings.Add(b);
        }
        catch (Exception ex)
        {
            FrmException.ShowBug(ex);
        }
    }

    /// <summary>
    /// 绑定CheckEdit的数据源
    /// </summary>
    /// <param name="edit">CheckEdit</param>
    /// <param name="dataSource">数据源</param>
    /// <param name="bindField">取值字段</param>
    public static void BindingCheckedListBox(CheckedListBox edit, object dataSource, string bindField)
    {
        try
        {
            edit.DataBindings.Clear();
            var b = new Binding("SelectedValue", dataSource, bindField)
            {
                DataSourceUpdateMode = CustomerDataSourceUpdateMode,
                NullValue = string.Empty,
                DataSourceNullValue = string.Empty,
            };
            edit.DataBindings.Add(b);
        }
        catch (Exception ex)
        {
            FrmException.ShowBug(ex);
        }
    }

    /// <summary>
    /// 绑定图像控件的数据源
    /// </summary>
    /// <param name="edit">PictureEdit</param>
    /// <param name="dataSource">数据源</param>
    /// <param name="bindField">取值字段</param>
    public static void BindingImageEdit(PictureEdit edit, object dataSource, string bindField)
    {
        try
        {
            edit.DataBindings.Clear();
            var b = new Binding("EditValue", dataSource, bindField)
            {
                NullValue = string.Empty,
                DataSourceNullValue = string.Empty,
                DataSourceUpdateMode = CustomerDataSourceUpdateMode
            };
            edit.DataBindings.Add(b);
        }
        catch (Exception ex)
        {
            FrmException.ShowBug(ex);
        }
    }

    /// <summary>
    /// 绑定ToggleSwitch的数据源
    /// </summary>
    /// <param name="edit">PictureEdit</param>
    /// <param name="dataSource">数据源</param>
    /// <param name="bindField">取值字段</param>
    public static void BindingToggleSwitch(ToggleSwitch edit, object dataSource, string bindField)
    {
        try
        {
            edit.DataBindings.Clear();
            var b = new Binding("EditValue", dataSource, bindField)
            {
                DataSourceUpdateMode = CustomerDataSourceUpdateMode,
                NullValue = "N",
                DataSourceNullValue = string.Empty,
            };
            edit.DataBindings.Add(b);
        }
        catch (Exception ex)
        {
            FrmException.ShowBug(ex);
        }
    }

    /// <summary>
    /// 绑定日期控件的数据源
    /// </summary>
    /// <param name="edit">控件框</param>
    /// <param name="dataSource">数据源</param>
    /// <param name="bindField">取值字段</param>
    public static void BindingDateEdit(DateEdit edit, object dataSource, string bindField)
    {
        try
        {
            edit.DataBindings.Clear();
            var b = new Binding("EditValue", dataSource, bindField, true)
            {
                NullValue = Const.DEFAULT_MINIMUM_TIME,
                DataSourceNullValue = string.Empty,
                DataSourceUpdateMode = CustomerDataSourceUpdateMode
            };
            edit.DataBindings.Add(b);
            b.ReadValue();
            edit.EditValueChanged += OnBindingEditValueChange;
            // DateEdit 控件没必要这样，DateTime 类型的 必填 字段，务必设置默认值。 非必填 字段，使用 DateTime? 类型即可。
            // 不处理0001，让验证类可以正常报空值错误
            // edit.ParseEditValue += (sender, args) =>
            // {
            //     if (args.Value is DateTime value && value < Const.DEFAULT_MINIMUM_TIME)
            //         args.Value = Const.DEFAULT_MINIMUM_TIME;
            // };
            edit.CustomDisplayText += (sender, args) =>
            {
                if (args.Value is DateTime value && value <= Const.DEFAULT_MINIMUM_TIME)
                    args.DisplayText = string.Empty;
            };
        }
        catch (Exception ex)
        {
            FrmException.ShowBug(ex);
        }
    }

    /// <summary>
    /// 绑定时间控件的数据源
    /// </summary>
    /// <param name="edit">控件框</param>
    /// <param name="dataSource">数据源</param>
    /// <param name="bindField">取值字段</param>
    public static void BindingTimeEdit(TimeEdit edit, object dataSource, string bindField)
    {
        try
        {
            edit.DataBindings.Clear();
            var b = new Binding("EditValue", dataSource, bindField, true)
            {
                NullValue = Const.DEFAULT_MINIMUM_TIME,
                DataSourceNullValue = string.Empty,
                DataSourceUpdateMode = CustomerDataSourceUpdateMode
            };
            edit.DataBindings.Add(b);
            b.ReadValue();
            // 防止 0001 被判定为空值
            edit.ParseEditValue += (sender, args) =>
            {
                if (args.Value is DateTime value && value < Const.DEFAULT_MINIMUM_TIME)
                    args.Value = Const.DEFAULT_MINIMUM_TIME;
            };
            edit.EditValueChanged += OnBindingEditValueChange;
        }
        catch (Exception ex)
        {
            FrmException.ShowBug(ex);
        }
    }

    /// <summary>
    /// 数据绑定的控件的值改变事件
    /// </summary>
    private static void OnBindingEditValueChange(object? sender, EventArgs e)
    {
        try
        {
            object bindingObj;
            switch (sender)
            {
                // 日期控件(DateEdit)的EditValueChanged事件.
                // 对象内定义为Nullable<DateTime>数据类型的属性,进行数据绑定后不能将值
                // 保存在对象内,所以实现这个方法做特殊处理
                case DateEdit dateEdit:
                    if (dateEdit.DataBindings.Count <= 0) return; //无绑定数据源.
                    bindingObj = dateEdit.DataBindings[0].DataSource; //取绑定的对象.
                    if (bindingObj != null)
                    {
                        string bindingField =
                            dateEdit.DataBindings[0].BindingMemberInfo.BindingField; //取绑定的成员字段.                
                        SetValueOfObject(bindingObj, bindingField, dateEdit.EditValue); //给对象的字段赋值
                    }
                    break;
                // 时间控件(TimeEdit)的EditValueChanged事件.
                // 对象内定义为Nullable<DateTime>数据类型的属性,进行数据绑定后不能将值
                // 保存在对象内,所以实现这个方法做特殊处理
                case TimeEdit dateEdit:
                    if (dateEdit.DataBindings.Count <= 0) return; //无绑定数据源.
                    bindingObj = dateEdit.DataBindings[0].DataSource; //取绑定的对象.
                    if (bindingObj != null)
                    {
                        string bindingField =
                            dateEdit.DataBindings[0].BindingMemberInfo.BindingField; //取绑定的成员字段.                
                        SetValueOfObject(bindingObj, bindingField, dateEdit.EditValue ); //给对象的字段赋值
                    }
                    break;
                // ComboEdit 的EditValueChanged事件.
                // 用来处理绑定对象的 CListItem 类型取值
                case ComboBoxEdit comboBoxEdit:
                    if (comboBoxEdit.DataBindings.Count <= 0) return; //无绑定数据源.
                    bindingObj = comboBoxEdit.DataBindings[0].DataSource; //取绑定的对象.
                    // if(comboBoxEdit.DataBindings[0].DataSourceNullValue is DBNull)
                    //     comboBoxEdit.DataBindings[0].DataSourceNullValue = null;
                    
                    if (bindingObj != null)
                    {
                        string bindingField =
                            comboBoxEdit.DataBindings[0].BindingMemberInfo.BindingField; //取绑定的成员字段.  
                        var value = comboBoxEdit.GetComboBoxValue();
                        SetValueOfObject(bindingObj, bindingField, value); //给对象的字段赋值
                    }
                    break;
            }
        }
        catch (Exception ex)
        {
            FrmException.ShowBug(ex);
        }
    }

    /// <summary>
    /// 绑定日期选择控件(DateEdit)的EditValueChanged事件.
    /// 原因请叁考OnDateEditValueChange方法描述.
    /// </summary>        
    public static void BindingDateEditValueChangeEvent(DateEdit dateEdit)
    {
        dateEdit.EditValueChanged += OnBindingEditValueChange;
    }

    /// <summary>
    /// 给绑定数据源的输入控件赋值
    /// </summary>
    public static void SetEditorBindingValue(System.Windows.Forms.Control bindingControl, object value)
    {
        SetEditorBindingValue(bindingControl, value, false);
    }

    public static void SetEditorBindingValue(System.Windows.Forms.Control bindingControl, object value,
        bool setEditorValue)
    {
        try
        {
            if (bindingControl.DataBindings.Count > 0)
            {
                object dataSource = bindingControl.DataBindings[0].DataSource;
                string field = bindingControl.DataBindings[0].BindingMemberInfo.BindingField;
                if (dataSource is DataTable table)
                {
                    table.Rows[0][field] = value;
                }
                else
                {
                    SetValueOfObject(dataSource, field, value);
                }
            }

            if (setEditorValue)
                SetValueOfObject(bindingControl, "EditValue", value);

            bindingControl.Refresh();

        }
        catch
        {
            // ignored
        } //这里不用显示异常信息. 
    }

    /// <summary>
    /// 设置对象某个属性的值
    /// </summary>
    private static void SetValueOfObject(object obj, string property, object value)
    {
        try
        {
            Type type = obj.GetType();
            System.Reflection.PropertyInfo[] pinfo = type.GetProperties();
            foreach (System.Reflection.PropertyInfo info in pinfo)
            {
                if (info.Name.ToUpper() == property.ToUpper())
                {
                    SetPropertyValue(obj, info, value);
                    break;
                }
            }
        }
        catch
        {
            // ignored
        }
    }

    /// <summary>
    /// 给对象的属性赋值
    /// </summary>
    /// <param name="instance">对象实例</param>
    /// <param name="prop">属性</param>
    /// <param name="value">值</param>
    private static void SetPropertyValue(object instance, System.Reflection.PropertyInfo prop, object value)
    {
        try
        {
            if (prop.PropertyType.ToString() == "System.String")
            {
            }
            else if (prop.PropertyType.ToString() == "System.Decimal")
                value = value.ObjToDecimal();
            else if (prop.PropertyType.ToString() == "System.Int32")
                value = value.ObjToInt();
            else if (prop.PropertyType.ToString() == "System.Single")
                value = float.Parse(value.ObjToStr());
            else if (prop.PropertyType.ToString() == "System.DateTime" && value is not DateTime)
            {
                value = value is DBNull ? DateTime.MinValue : DateTime.Parse(value.ObjToStr());
            }

            prop.SetValue(instance, value, null);
        }
        catch
        {
            // ignored
        }
    }
}