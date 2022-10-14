using System.Collections;
using System.Data;
using BB.BaseUI.Other;
using BB.Tools.Entity;
using BB.Tools.Extension;
using BB.Tools.Format;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;

namespace BB.BaseUI.Extension;

/// <summary>
/// LookupEdit/SearchLookupEdit/RepositoryItemLookUpEdit/RepositoryItemSearchLookUpEdit的扩展函数
/// </summary>
public static class ComboBoxExtension
{
    /// <summary>
    /// 获取系统时间
    /// </summary>
    /// <returns></returns>
    private static DateTime GetSystemDateTime()
    {
        return DateTime.Now;
    }

    /// <summary>
    /// 绑定年的下拉列表
    /// </summary>
    /// <param name="control">ComboBoxEdit对象</param>
    /// <param name="emptyFlag">是否增加空行</param>
    /// <returns></returns>
    public static List<int> BindYear(this ComboBoxEdit control, bool emptyFlag = true)
    {
        DateTime dateTime = GetSystemDateTime();
        List<int> list = new List<int>();
        for (int y = 2011; y <= dateTime.Year; y++)
        {
            list.Add(y);
        }
        control.BindDictItems(list, dateTime.Year, emptyFlag);
        return list;
    }

    /// <summary>
    /// 绑定月份的下拉列表
    /// </summary>
    /// <param name="control">ComboBoxEdit对象</param>
    /// <param name="emptyFlag">是否增加空行</param>
    /// <returns></returns>
    public static List<string> BindMonth(this ComboBoxEdit control, bool emptyFlag = true)
    {
        DateTime dateTime = GetSystemDateTime();
        List<string> list = new List<string>();
        for (int i = 1; i <= 12; i++)
        {
            list.Add(i.ToString("00"));
        }
        control.BindDictItems(list, dateTime.Month.ToString("00"), emptyFlag);
        return list;
    }

    /// <summary>
    /// 绑定年月的下拉列表
    /// </summary>
    /// <param name="control">ComboBoxEdit对象</param>
    /// <param name="emptyFlag">是否增加空行</param>
    /// <returns></returns>
    public static List<string> BindYearMonth(this ComboBoxEdit control, bool emptyFlag = true)
    {
        DateTime dateTime = GetSystemDateTime();
        List<string> list = new List<string>();
        for (int i = 1; i <= 12; i++)
        {
            list.Add($"{dateTime.Year.ToString("0000")}{i.ToString("00")}");
        }
        control.BindDictItems(list, dateTime.Month.ToString("00"), emptyFlag);
        return list;
    }

    /// <summary>
    /// 绑定控件的字典数据
    /// </summary>
    /// <param name="control">下拉控件</param>
    /// <param name="dataSource">数据源</param>
    /// <param name="displayMember">显示字段</param>
    /// <param name="valueMember">值字段</param>
    /// <returns></returns>
    public static DataTable BindDictItems(this CheckedComboBoxEdit control, DataTable dataSource, string displayMember, string valueMember)
    {
        control.Properties.DataSource = dataSource;
        control.Properties.DisplayMember = displayMember;
        control.Properties.ValueMember = valueMember;
        control.Properties.NullText = string.Empty;

        return dataSource;
    }

    /// <summary>
    /// 绑定控件的字典数据
    /// </summary>
    /// <param name="control">下拉控件</param>
    /// <param name="dataSource">数据源</param>
    /// <param name="displayMember">显示字段</param>
    /// <param name="valueMember">值字段</param>
    /// <returns></returns>
    public static DataTable BindDictItems(this RepositoryItemCheckedComboBoxEdit control, DataTable dataSource, string displayMember, string valueMember)
    {
        control.DataSource = dataSource;
        control.DisplayMember = displayMember;
        control.ValueMember = valueMember;
        control.NullText = string.Empty;

        return dataSource;
    }
        
    /// <summary>
    /// 获取下拉列表的值
    /// </summary>
    /// <param name="combo">下拉列表</param>
    /// <param name="value">当前值</param>
    /// <returns></returns>
    public static string GetComboBoxValue(this RepositoryItemComboBox combo, object value)
    {
        foreach (object listItem in combo.Items)
            switch (listItem)
            {
                case CListItem l when l.Value == (value is DBNull ? string.Empty : (string)value):
                    return l.Text;
                case string s when s == (value is DBNull ? string.Empty : (string)value):
                    return s;
            }

        return string.Empty;
    }

    /// <summary>
    /// 绑定控件的字典数据
    /// </summary>
    /// <param name="control">下拉控件</param>
    /// <param name="list">数据列表</param>
    /// <param name="emptyFlag">是否增加空值</param>
    /// <returns></returns>
    public static ICollection BindDictItems(this RepositoryItemComboBox control, ICollection list, bool emptyFlag = true)
    {

        control.Items.Clear();
        try
        {
            control.BeginUpdate();
            if (emptyFlag)
            {
                control.Items.Add(string.Empty);
            }
            control.Items.AddRange(list);
            control.ParseEditValue += (sender, e) =>
            {
                e.Value = e.Value?.ToString();
                e.Handled = true;
            };
        }
        finally
        {
            control.EndUpdate();
        }

        return list;
    }

    /// <summary>
    /// 绑定控件的字典数据
    /// </summary>
    /// <param name="control">下拉控件</param>
    /// <param name="list">数据列表</param>
    /// <param name="emptyFlag">是否增加空值</param>
    /// <returns></returns>
    public static List<string> BindDictItems(this RepositoryItemComboBox control, List<string> list, bool emptyFlag = true)
    {
        control.Items.Clear();
        if (emptyFlag)
        {
            control.Items.Add(string.Empty);
        }
        control.Items.AddRange(list);

        return list;
    }

    /// <summary>
    /// 绑定下拉列表控件为指定的数据字典列表
    /// </summary>
    /// <param name="combo">下拉列表控件</param>
    /// <param name="dictTypeName">数据字典类型名称</param>
    /// <param name="emptyFlag">是否增加空值</param>
    public static void BindDictItems(this RepositoryItemComboBox combo, string dictTypeName, bool emptyFlag = true)
    {
        BindDictItems(combo, GB.GetDictByName(dictTypeName), emptyFlag);
    }

    /// <summary>
    /// 绑定控件的字典数据
    /// </summary>
    /// <param name="control">下拉列表控件</param>
    /// <param name="list">数据列表</param>
    /// <param name="emptyFlag">是否增加空值</param>
    /// <returns></returns>
    public static List<CListItem> BindDictItems(this RepositoryItemComboBox control, List<CListItem> list,
        bool emptyFlag = true)
    {
        control.Items.Clear();
        try
        {
            control.BeginUpdate();
            if (emptyFlag)
            {
                control.Items.Add(new CListItem(string.Empty));
            }

            control.Items.AddRange(list);

            control.ParseEditValue += (sender, e) =>
            {
                if (e.Value is CListItem item)
                {
                    e.Value = item.Value;
                    e.Handled = true;
                }
            };
        }
        finally
        {
            control.EndUpdate();
        }

        return list;
    }

    /// <summary>
    /// 绑定控件的字典数据
    /// </summary>
    /// <param name="control">下拉控件</param>
    /// <param name="list">数据列表</param>
    /// <param name="emptyFlag">是否增加空值</param>
    /// <returns></returns>
    public static List<string> BindDictItems(this RepositoryItemCheckedComboBoxEdit control, List<string> list, bool emptyFlag = true)
    {
        if (emptyFlag)
        {
            list.Insert(0, string.Empty);
        }

        control.Items.Clear();
        control.Items.AddRange(list.ToArray());
        return list;
    }

    /// <summary>
    /// 绑定控件的字典数据
    /// </summary>
    /// <param name="control">下拉控件</param>
    /// <param name="list">数据列表</param>
    /// <param name="emptyFlag">是否增加空值</param>
    /// <returns></returns>
    public static List<CListItem> BindDictItems(this RepositoryItemCheckedComboBoxEdit control, List<CListItem> list, bool emptyFlag = true)
    {
        List<CheckedListBoxItem> checkList = new();
        foreach (CListItem item in list)
        {
            checkList.Add(new CheckedListBoxItem(item.Value, item.Text));
        }

        control.Items.Clear();
        if (emptyFlag)
        {
            control.Items.Add(new CListItem(string.Empty));
        }
        control.Items.AddRange(checkList.ToArray());
        return list;
    }

    #region ComboBoxEdit控件

    /// <summary>
    /// 下拉控件，键盘按下，自动弹出选框
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    static void OnRepositoryItemOnKeyUp(object? sender, KeyEventArgs args)
    {
        if (args.KeyCode == Keys.Tab && sender is PopupBaseEdit popupBaseEdit)
        {
            popupBaseEdit.ShowPopup();
        }
    }

    /// <summary>
    /// 获取下拉列表的值
    /// </summary>
    /// <param name="combo">下拉列表</param>
    /// <returns></returns>
    public static string GetComboBoxValue(this ComboBoxEdit combo)
    {
        if (combo?.SelectedItem is CListItem item)
        {
            return item.Value;
        }

        if(combo?.SelectedItem is string v && combo?.Properties.Items.Count > 0 && combo?.Properties.Items[0] is CListItem)
        {
            foreach (CListItem propertiesItem in combo?.Properties.Items)
            {
                if (propertiesItem.Value == v || propertiesItem.Text == v)
                {
                    return propertiesItem.Value;
                }
            }
        }
        else
        {
            return combo?.Text;
        }

        return string.Empty;
    }

    /// <summary>
    /// 设置下拉列表选中指定的普通值
    /// </summary>
    /// <param name="combo">下拉列表</param>
    /// <param name="value">指定的CListItem中的值</param>
    public static int SetDropDownValue(this ComboBoxEdit combo, string value)
    {
        int result = -1;
        for (int i = 0; i < combo.Properties.Items.Count; i++)
        {
            if (combo.Properties.Items[i].ToString() == value)
            {
                combo.SelectedIndex = i;
                result = i;
                break;
            }
        }
        return result;
    }

    /// <summary>
    /// 设置下拉列表选中指定的CListItem值
    /// </summary>
    /// <param name="combo">下拉列表</param>
    /// <param name="value">指定的CListItem中的值</param>
    public static void SetComboBoxItem(this ComboBoxEdit combo, object value)
    {
        for (int i = 0; i < combo.Properties.Items.Count; i++)
        {
            CListItem item = combo.Properties.Items[i] as CListItem;
            if (item != null && value != null && item.Value == string.Concat(value))
            {
                combo.SelectedIndex = i;
                combo.SelectedText = item.Text;
            }
        }
    }

    /// <summary>
    /// 绑定下拉列表控件为指定的数据字典列表
    /// </summary>
    /// <param name="combo">下拉列表控件</param>
    /// <param name="dict">数据字典列表</param>
    /// <param name="emptyFlag">是否加入空值选项</param>
    /// <param name="freeInput">是否允许自由输入</param>
    /// <param name="limitedContent">是否限定输入内容</param>
    public static void BindDictItems(this ComboBoxEdit combo, Dictionary<string, string> dict, bool emptyFlag = true, bool freeInput = true, bool limitedContent = true)
    {
        List<CListItem> itemList = new List<CListItem>();
        foreach (string key in dict.Keys)
        {
            itemList.Add(new CListItem(dict[key], key));
        }
        BindDictItems(combo, itemList, null, emptyFlag, freeInput, limitedContent);
    }

    /// <summary>
    /// 绑定下拉列表控件为指定的数据字典列表
    /// </summary>
    /// <param name="combo">下拉列表控件</param>
    /// <param name="dictTypeName">字典名</param>
    /// <param name="defaultValue">默认值</param>
    /// <param name="emptyFlag">是否加入空值选项</param>
    /// <param name="freeInput">是否允许自由输入</param>
    /// <param name="limitedContent">是否限定输入内容</param>
    public static void BindDictItems(this ComboBoxEdit combo, string dictTypeName, string defaultValue = null, bool emptyFlag = true, bool freeInput = true, bool limitedContent = true)
    {
        BindDictItems(combo, GB.GetDictByName(dictTypeName), defaultValue, emptyFlag, freeInput, limitedContent);
    }

    /// <summary>
    /// 绑定下拉列表控件为指定的数据字典列表
    /// </summary>
    /// <param name="combo">下拉列表控件</param>
    /// <param name="itemList">数据字典列表</param>
    /// <param name="emptyFlag">是否加入空值选项</param>
    /// <param name="freeInput">是否允许自由输入</param>
    /// <param name="limitedContent">是否限定输入内容</param>
    public static void BindDictItems(this ComboBoxEdit combo, List<CListItem> itemList, bool emptyFlag = true, bool freeInput = true, bool limitedContent = true)
    {
        BindDictItems(combo, itemList, null, emptyFlag, freeInput, limitedContent);
    }

    /// <summary>
    /// 绑定下拉列表控件为指定的数据字典列表
    /// </summary>
    /// <param name="combo">下拉列表控件</param>
    /// <param name="itemList">数据字典列表</param>
    /// <param name="defaultValue">控件默认值</param>
    /// <param name="emptyFlag">是否加入空值选项</param>
    /// <param name="freeInput">是否允许自由输入</param>
    /// <param name="limitedContent">是否限定输入内容</param>
    public static void BindDictItems(this ComboBoxEdit combo, List<CListItem> itemList, string? defaultValue, bool emptyFlag = true, bool freeInput = true, bool limitedContent = true)
    {
        combo.Properties.BeginUpdate();//可以加快
        combo.Properties.NullValuePromptShowForEmptyValue = true;
        combo.Properties.NullValuePrompt = "请选择";
        combo.Properties.Items.Clear();
        combo.Properties.Items.AddRange(itemList);
        if (emptyFlag)
        {
            combo.Properties.Items.Insert(0, new CListItem("无", string.Empty));
        }

        if (itemList.Count > 0)
        {
            if (!string.IsNullOrEmpty(defaultValue))
            {
                combo.SetComboBoxItem(defaultValue);
            }
            else
            {
                combo.SelectedIndex = 0;
            }
        }

        if (!freeInput)
            combo.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
        
        combo.KeyUp += OnRepositoryItemOnKeyUp;
            
        combo.Properties.EndUpdate();//可以加快

        combo.CustomDisplayText += (sender, args) =>
        {
            var com = (ComboBoxEdit)sender;
            com.SetComboBoxItem(args.Value);
        };

        if (limitedContent)
            combo.LostFocus += (sender, args) =>
            {
                var com = (ComboBoxEdit)sender;
                bool contains = itemList.Contains(com.EditValue);
                if (!contains)
                    com.EditValue = null;
            };
    }

    /// <summary>
    /// 绑定下拉列表控件为指定的数据字典列表
    /// </summary>
    /// <param name="combo">下拉列表控件</param>
    /// <param name="itemList">数据字典列表</param>
    /// <param name="defaultValue">控件默认值</param>
    /// <param name="emptyFlag">是否加入空值选项</param>
    /// <param name="freeInput">是否允许自由输入</param>
    /// <param name="limitedContent">是否限定输入内容</param>
    public static void BindDictItems(this ComboBoxEdit combo, List<string> itemList, string defaultValue, bool emptyFlag = true, bool freeInput = false, bool limitedContent = true)
    {
        combo.Properties.BeginUpdate();//可以加快
        combo.Properties.NullValuePromptShowForEmptyValue = true;
        combo.Properties.NullValuePrompt = "请选择";
        combo.Properties.Items.Clear();
        combo.Properties.Items.AddRange(itemList);
        if (emptyFlag)
        {
            combo.Properties.Items.Insert(0, string.Empty);
        }

        if (itemList.Count > 0)
        {
            combo.SetDropDownValue(defaultValue);
        }

        if (!freeInput)
            combo.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
        
        combo.KeyUp += OnRepositoryItemOnKeyUp;

        combo.Properties.EndUpdate();//可以加快

        if (!limitedContent)
            combo.LostFocus += (sender, args) =>
            {
                var com = (ComboBoxEdit)sender;
                bool contains = itemList.Contains(com.EditValue);
                if (!contains)
                    com.EditValue = null;
            };
    }

    /// <summary>
    /// 绑定下拉列表控件为指定的数据字典列表
    /// </summary>
    /// <param name="combo">下拉列表控件</param>
    /// <param name="itemList">数据字典列表</param>
    /// <param name="defaultValue">控件默认值</param>
    /// <param name="emptyFlag">是否加入空值选项</param>
    /// <param name="freeInput">是否允许自由输入</param>
    public static void BindDictItems(this ComboBoxEdit combo, List<int> itemList, int defaultValue, bool emptyFlag = true, bool freeInput = false)
    {
        combo.Properties.BeginUpdate();//可以加快
        combo.Properties.NullValuePromptShowForEmptyValue = true;
        combo.Properties.NullValuePrompt = "请选择";
        combo.Properties.Items.Clear();
        combo.Properties.Items.AddRange(itemList);
        if (emptyFlag)
        {
            combo.Properties.Items.Insert(0, string.Empty);
        }

        if (itemList.Count > 0)
        {
            combo.SetDropDownValue(defaultValue.ToString());
        }

        if (!freeInput)
            combo.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
        
        combo.KeyUp += OnRepositoryItemOnKeyUp;

        combo.Properties.EndUpdate();//可以加快
    }

    /// <summary>
    /// 绑定枚举类型
    /// </summary>
    /// <param name="combo">下拉列表控件</param>
    /// <param name="freeInput">是否允许自由输入</param>
    public static void BindDictItems<T>(this ComboBoxEdit combo, bool freeInput = false)
    {
        Dictionary<string, object> dict = EnumHelper.GetMemberKeyValue<T>();
        combo.Properties.BeginUpdate();//可以加快
        combo.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
        combo.Properties.Items.Clear();
        foreach (string key in dict.Keys)
        {
            combo.Properties.Items.Add(new CListItem(dict[key].ToString(), key));
        }

        if (!freeInput)
            combo.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
        
        combo.KeyUp += OnRepositoryItemOnKeyUp;
            
        combo.Properties.EndUpdate();//可以加快
    }

    #endregion

    #region CheckedComboBoxEdit

    /// <summary>
    /// 设置下拉列表选中指定的值
    /// </summary>
    /// <param name="combo">下拉列表</param>
    /// <param name="value">指定的CListItem中的值</param>
    public static void SetComboBoxItem(this CheckedComboBoxEdit combo, string value)
    {
        for (int i = 0; i < combo.Properties.Items.Count; i++)
        {
            CheckedListBoxItem item = combo.Properties.Items[i] as CheckedListBoxItem;
            if (item != null && value != null)
            {
                List<string> list = value.ToDelimitedList<string>(",");
                foreach (string valueItem in list)
                {
                    if (string.Concat(item.Value) == valueItem.Trim())
                    {
                        combo.Properties.Items[i].CheckState = CheckState.Checked;
                    }
                }
            }
        }
    }



    /// <summary>
    /// 绑定下拉列表控件为指定的数据字典列表
    /// </summary>
    /// <param name="combo">下拉列表控件</param>
    /// <param name="itemList">数据字典列表</param>
    public static void BindDictItems(this CheckedComboBoxEdit combo, Dictionary<string, string> dict)
    {
        List<CListItem> itemList = new List<CListItem>();
        foreach (string key in dict.Keys)
        {
            itemList.Add(new CListItem(dict[key], key));
        }

        BindDictItems(combo, itemList);
    }
        
    /// <summary>
    /// 绑定下拉列表控件为指定的数据字典列表
    /// </summary>
    /// <param name="combo">下拉列表控件</param>
    /// <param name="itemList">数据字典列表</param>
    public static void BindDictItems(this CheckedComboBoxEdit combo, List<CListItem> itemList)
    {
        BindDictItems(combo, itemList, null);
    }

    /// <summary>
    /// 绑定下拉列表控件为指定的数据字典列表
    /// </summary>
    /// <param name="combo">下拉列表控件</param>
    /// <param name="itemList">数据字典列表</param>
    /// <param name="defaultValue">控件默认值</param>
    public static void BindDictItems(this CheckedComboBoxEdit combo, List<CListItem> itemList, string defaultValue)
    {
        List<CheckedListBoxItem> checkList = new List<CheckedListBoxItem>();
        foreach (CListItem item in itemList)
        {
            checkList.Add(new CheckedListBoxItem(item.Value, item.Text));
        }

        combo.Properties.BeginUpdate();//可以加快
        combo.Properties.Items.Clear();
        combo.Properties.Items.AddRange(checkList.ToArray());//可以加快

        if (!string.IsNullOrEmpty(defaultValue))
        {
            combo.SetComboBoxItem(defaultValue);
        }
        
        combo.KeyUp += OnRepositoryItemOnKeyUp;

        combo.Properties.EndUpdate();//可以加快
    }

    #endregion

    #region 单选框组RadioGroup
    /// <summary>
    /// 设置单选框组的选定内容
    /// </summary>
    /// <param name="radGroup">单选框组</param>
    /// <param name="value">选定内容</param>
    public static void SetRaidioGroupItem(this RadioGroup radGroup, string value)
    {
        radGroup.SelectedIndex = radGroup.Properties.Items.GetItemIndexByValue(value);
    }

    /// <summary>
    /// 绑定单选框组为指定的数据字典列表
    /// </summary>
    /// <param name="radGroup">单选框组</param>
    /// <param name="itemList">字典列表</param>
    public static void BindDictItems(this RadioGroup radGroup, List<CListItem> itemList)
    {
        BindDictItems(radGroup, itemList, null);
    }

    /// <summary>
    /// 绑定单选框组为指定的数据字典列表
    /// </summary>
    /// <param name="radGroup">单选框组</param>
    /// <param name="itemList">字典列表</param>
    /// <param name="defaultValue">控件默认值</param>
    public static void BindDictItems(this RadioGroup radGroup, List<CListItem> itemList, string defaultValue)
    {
        List<RadioGroupItem> groupList = new List<RadioGroupItem>();
        foreach (CListItem item in itemList)
        {
            groupList.Add(new RadioGroupItem(item.Value, item.Text));
        }

        radGroup.Properties.BeginUpdate();//可以加快
        radGroup.Properties.Items.Clear();
        radGroup.Properties.Items.AddRange(groupList.ToArray());//可以加快

        if (!string.IsNullOrEmpty(defaultValue))
        {
            SetRaidioGroupItem(radGroup, defaultValue);
        }
        radGroup.Properties.EndUpdate();//可以加快
    }


    #endregion
}