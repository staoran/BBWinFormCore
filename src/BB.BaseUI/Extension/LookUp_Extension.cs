using System.Data;
using BB.BaseUI.Control;
using BB.BaseUI.Other;
using BB.Entity.Dictionary;
using BB.Tools.Entity;
using BB.Tools.Format;
using BB.Tools.MultiLanuage;
using BB.Tools.Utils;
using BB.Tools.Extension;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;

namespace BB.BaseUI.Extension; 

 /// <summary>
 /// LookupEdit/SearchLookupEdit/RepositoryItemLookUpEdit/RepositoryItemSearchLookUpEdit的扩展函数
 /// </summary>
 public static class LookUpExtension
 {
     #region SearchLookUpEdit和LookUpEdit控件

     /// <summary>
     /// 绑定控件的数据源
     /// </summary>
     /// <param name="lookup">控件对象</param>
     /// <param name="dataSource">数据源</param>
     /// <param name="displayMember">显示字段</param>
     /// <param name="valueMember">值字段</param>
     /// <param name="popupWidth">显示宽度</param>
     /// <param name="autoSearchColumnIndex">自动查询列位置</param>
     /// <param name="lookUpColumnInfos">显示的列</param>
     /// <returns></returns>
     public static object BindDictItems(this RepositoryItemLookUpEdit lookup, object dataSource, string displayMember,
         string valueMember, int popupWidth, int autoSearchColumnIndex, params LookUpColumnInfo[] lookUpColumnInfos)
     {
         lookup.DataSource = dataSource;
         lookup.DisplayMember = displayMember;
         lookup.ValueMember = valueMember;
         lookup.Columns.Clear();
         for (int i = 0; i < lookUpColumnInfos.Length; i++)
         {
             lookup.Columns.Add(lookUpColumnInfos[i]);
         }
         lookup.PopupWidth = popupWidth;
         lookup.AutoSearchColumnIndex = autoSearchColumnIndex;
         lookup.ImmediatePopup = true;
         lookup.SearchMode = SearchMode.AutoComplete;
         lookup.TextEditStyle = TextEditStyles.Standard;

         return dataSource;
     }

     /// <summary>
     /// 绑定控件的数据源
     /// </summary>
     /// <param name="lookup">控件对象</param>
     /// <param name="dataSource">数据源</param>
     /// <param name="displayMember">显示字段</param>
     /// <param name="valueMember">值字段</param>
     /// <param name="popupWidth">显示宽度</param>
     /// <param name="lookUpColumnInfos">显示的列</param>
     /// <returns></returns>
     public static object BindDictItems(this RepositoryItemSearchLookUpEdit lookup, object dataSource,
         string displayMember, string valueMember, int popupWidth, params LookUpColumnInfo[] lookUpColumnInfos)
     {
         lookup.DataSource = dataSource;
         lookup.DisplayMember = displayMember;
         lookup.ValueMember = valueMember;
         lookup.View.Columns.Clear();
         for (int i = 0; i < lookUpColumnInfos.Length; i++)
         {
             lookup.View.CreateColumn(lookUpColumnInfos[i].FieldName, lookUpColumnInfos[i].Caption,
                 lookUpColumnInfos[i].Width, true, UnboundColumnType.Bound, DefaultBoolean.False, FixedStyle.None);
         }
         lookup.ImmediatePopup = true;
         lookup.TextEditStyle = TextEditStyles.Standard;

         return dataSource;
     }

     /// <summary>
     /// 绑定控件的数据源
     /// </summary>
     /// <param name="lookup">控件对象</param>
     /// <param name="dataSource">数据源</param>
     /// <param name="displayMember">显示字段</param>
     /// <param name="valueMember">值字段</param>
     /// <param name="popupWidth">显示宽度</param>
     /// <param name="autoSearchColumnIndex">自动查询列位置</param>
     /// <param name="lookUpColumnInfos">显示的列</param>
     /// <returns></returns>
     public static object BindDictItems(this LookUpEdit lookup, object dataSource, string displayMember, string valueMember, int popupWidth,
         int autoSearchColumnIndex, params LookUpColumnInfo[] lookUpColumnInfos)
     {
         lookup.Properties.DataSource = dataSource;
         lookup.Properties.DisplayMember = displayMember;
         lookup.Properties.ValueMember = valueMember;
         lookup.Properties.Columns.Clear();
         for (int i = 0; i < lookUpColumnInfos.Length; i++)
         {
             lookup.Properties.Columns.Add(lookUpColumnInfos[i]);
         }
         lookup.Properties.PopupWidth = popupWidth;
         lookup.Properties.AutoSearchColumnIndex = autoSearchColumnIndex;
         lookup.Properties.ImmediatePopup = true;
         lookup.Properties.SearchMode = SearchMode.AutoComplete;
         lookup.Properties.TextEditStyle = TextEditStyles.Standard;

         lookup.KeyUp += OnRepositoryItemOnKeyUp;

         return dataSource;
     }

     /// <summary>
     /// 绑定控件的数据源
     /// </summary>
     /// <param name="lookup">控件对象</param>
     /// <param name="dataSource">数据源</param>
     /// <param name="displayMember">显示字段</param>
     /// <param name="valueMember">值字段</param>
     /// <param name="popupWidth">显示宽度</param>
     /// <param name="lookUpColumnInfos">显示的列</param>
     /// <returns></returns>
     public static object BindDictItems(this SearchLookUpEdit lookup, object dataSource, string displayMember, string valueMember,
         int popupWidth, params LookUpColumnInfo[] lookUpColumnInfos)
     {
         lookup.Properties.DataSource = dataSource;
         lookup.Properties.DisplayMember = displayMember;
         lookup.Properties.ValueMember = valueMember;
         lookup.Properties.View.Columns.Clear();
         for (int i = 0; i < lookUpColumnInfos.Length; i++)
         {
             lookup.Properties.View.CreateColumn(lookUpColumnInfos[i].FieldName, lookUpColumnInfos[i].Caption,
                 lookUpColumnInfos[i].Width, true, UnboundColumnType.Bound, DefaultBoolean.False, FixedStyle.None);
         }
         lookup.Properties.ImmediatePopup = true;
         lookup.Properties.TextEditStyle = TextEditStyles.Standard;

         lookup.KeyUp += OnRepositoryItemOnKeyUp;

         return dataSource;
     }

     /// <summary>
     /// 绑定列表对象到控件
     /// </summary>
     /// <param name="control">控件对象</param>
     /// <param name="list">列表集合</param>
     /// <param name="emptyFlag">是否插入空行</param>
     /// <returns></returns>
     public static List<CListItem> BindDictItems(this RepositoryItemLookUpEdit control, List<CListItem> list, bool emptyFlag = true)
     {
         if (emptyFlag)
         {
             list.Insert(0, new CListItem(""));
         }

         BindDictItems(control, list, "Text", "Value", 300, 2, new[]
         {
             new LookUpColumnInfo("Text", "名称", 150),
             new LookUpColumnInfo("Value", "编码", 150),
         });

         return list;
     }
        
     /// <summary>
     /// 绑定列表对象到控件
     /// </summary>
     /// <param name="control">控件对象</param>
     /// <param name="list">列表集合</param>
     /// <param name="emptyFlag">是否插入空行</param>
     /// <returns></returns>
     public static List<CListItem> BindDictItems(this RepositoryItemSearchLookUpEdit control, List<CListItem> list, bool emptyFlag = true)
     {
         if (emptyFlag)
         {
             list.Insert(0, new CListItem(""));
         }

         BindDictItems(control, list, "Text", "Value", 300, new[]
         {
             new LookUpColumnInfo("Text", "名称", 150),
             new LookUpColumnInfo("Value", "编码", 150),
         });

         return list;
     }

     #endregion

     #region CustomGridLookUpEdit控件

     /// <summary>
     /// 获取下拉列表的值
     /// </summary>
     /// <param name="combo">下拉列表</param>
     /// <returns></returns>
     public static string GetComboBoxValue(this CustomGridLookUpEdit combo)
     {
         return combo.EditValue as string;
     }

     /// <summary>
     /// 设置下拉列表选中指定的值
     /// </summary>
     /// <param name="combo">下拉列表</param>
     /// <param name="value">指定的CListItem中的值</param>
     public static void SetComboBoxItem(this CustomGridLookUpEdit combo, string value)
     {
         combo.EditValue = value;
     }
        
     /// <summary>
     /// 绑定下拉列表控件为指定的数据字典列表
     /// </summary>
     /// <param name="combo">下拉列表控件</param>
     /// <param name="itemList">数据字典列表</param>
     public static void BindDictItems(this CustomGridLookUpEdit combo, List<CListItem> itemList)
     {
         BindDictItems(combo, itemList, null);
     }

     /// <summary>
     /// 绑定下拉列表控件为指定的数据字典列表
     /// </summary>
     /// <param name="combo">下拉列表控件</param>
     /// <param name="itemList">数据字典列表</param>
     /// <param name="defaultValue">控件默认值</param>
     public static void BindDictItems(this CustomGridLookUpEdit combo, List<CListItem> itemList, string defaultValue)
     {
         string displayName = "显示内容";
         const string valueName = "值内容";
         const string pinyin = "拼音码";
         DataTable dt = DataTableHelper.CreateTable($"{displayName},{valueName},{pinyin}");

         foreach (CListItem item in itemList)
         {
             DataRow row = dt.NewRow();
             row[displayName] = item.Text;
             row[valueName] = item.Value;
             row[pinyin] = Pinyin.GetFirstPy(item.Text);
             dt.Rows.Add(row);
         }

         combo.Properties.ValueMember = valueName;
         combo.Properties.DisplayMember = displayName;
         combo.Properties.DataSource = dt;
         combo.Properties.PopulateViewColumns();
         combo.Properties.View.Columns[valueName].Visible = false;

         combo.KeyUp += OnRepositoryItemOnKeyUp;

         if (!string.IsNullOrEmpty(defaultValue))
         {
             combo.Text = defaultValue;
         }

         if (!string.IsNullOrEmpty(defaultValue))
         {
             combo.EditValue = defaultValue;
         }
     }

     #endregion

     #region 省市区SearchLookUpEdit控件

     /// <summary>
     /// 绑定下拉列表控件为省市区
     /// </summary>
     /// <param name="combo">区县下拉框</param>
     /// <param name="provinceCombo">省下拉框</param>
     /// <param name="cityCombo">市下拉框</param>
     public static void BindCityItems(this SearchLookUpEdit combo, ComboBoxEdit provinceCombo, ComboBoxEdit cityCombo)
     {
         var data = GB.AllRegion.Where(x => x.Type == 3).ToList();
         combo.BindDictItems(data, "Name", "Id", 400, new[]
         {
             new LookUpColumnInfo("Id", "地区编号", 80),
             new LookUpColumnInfo("Name", "地区名称", 120),
             new LookUpColumnInfo("FullName", "全称", 200)
         });

         provinceCombo.BindDictItems(GB.AllProvince, "", true);
         cityCombo.BindDictItems(GB.AllCity, "", true);
         provinceCombo.ReadOnly = true;
         cityCombo.ReadOnly = true;

         combo.EditValueChanged += (sender, args) =>
         {
             var comboStr = combo.EditValue.ToString();
             if (comboStr.Length < 6) return;
             string proId = comboStr.IsNullOrEmpty() ? string.Empty : comboStr.Substring(0, 2);
             string cityId = comboStr.IsNullOrEmpty() ? string.Empty : comboStr.Substring(0, 4);
             provinceCombo.SetComboBoxItem(proId);
             cityCombo.SetComboBoxItem(cityId);
             // ChangeEventArgs a = args as ChangeEventArgs;
             // string v = a?.Value.ToString();
             // if (!v.IsNullOrEmpty())
             // {
             //     string proId = v.Substring(0, 2);
             //     string cityId = v.Substring(0, 4);
             //     provinceCombo.EditValue = proId;
             //     cityCombo.EditValue = cityId;
             // }
         };
     }

     /// <summary>
     /// 绑定下拉列表控件为省市区
     /// </summary>
     /// <param name="combo">区级搜索下拉控件</param>
     /// <param name="isAreaOnly">仅限区县数据</param>
     public static void BindCityItems(this SearchLookUpEdit combo, bool isAreaOnly = true)
     {
         var data = EnumerableExtension.Where(GB.AllRegion, isAreaOnly, x => x.Type == 3).ToList();
         combo.BindDictItems(data, "FullName", "Id", 400, new[]
         {
             new LookUpColumnInfo("Id", "地区编号", 80),
             new LookUpColumnInfo("Name", "地区名称", 120),
             new LookUpColumnInfo("FullName", "全称", 200)
         });
     }

     /// <summary>
     /// 绑定下拉列表控件为省市区
     /// </summary>
     /// <param name="combo">区级搜索下拉框控件</param>
     /// <param name="gridView">所属网格控件</param>
     /// <param name="provinceCombo">省组件</param>
     /// <param name="cityCombo">市组件</param>
     public static void BindCityItems(this RepositoryItemSearchLookUpEdit combo, GridView gridView,
         RepositoryItemComboBox provinceCombo, RepositoryItemComboBox cityCombo)
     {
         var data = GB.AllRegion.Where(x => x.Type == 3).ToList();
         combo.BindDictItems(data, "Name", "Id", 400, new[]
         {
             new LookUpColumnInfo("Id", "地区编号", 80),
             new LookUpColumnInfo("Name", "地区名称", 120),
             new LookUpColumnInfo("FullName", "全称", 200)
         });

         provinceCombo.BindDictItems(GB.AllProvince);
         cityCombo.BindDictItems(GB.AllCity);
         provinceCombo.ReadOnly = true;
         cityCombo.ReadOnly = true;

         combo.EditValueChanged += (sender, args) =>
         { 
             var searchLookUpEdit = (SearchLookUpEdit)sender;
             var row = (RegionInfo)searchLookUpEdit?.GetSelectedDataRow();
             if (row != null)
             {
                 var v = row.Id.ToString();
                 if (v.Length < 6) return;
                 // gridView.SetRowCellValue(e.RowHandle, TranCustomerInfo.PRIMARY_KEY, _tempInfo.CustomerCode); //另一种赋值方式
                 gridView.SetFocusedRowCellValue(combo.Name, v); // 很奇怪用了这个事件给其他列赋值时，本单元格会被影响没有值，必须同时给自己赋值才行
                 gridView.SetFocusedRowCellValue(cityCombo.Name, v.Substring(0, 4));
                 gridView.SetFocusedRowCellValue(provinceCombo.Name, v.Substring(0, 2));
             }
         };

         // 选择上面的事件，更及时
         // gridView.CellValueChanged += (sender, args) =>
         // {
         //     if (args.Column.Name.Contains("AreaNo"))
         //     {
         //         var v = args.Value.ToString();
         //         if (!v.IsNullOrEmpty())
         //         {
         //             // gridView.SetRowCellValue(e.RowHandle, TranCustomerInfo.PRIMARY_KEY, _tempInfo.CustomerCode); //明细表的外键
         //             gridView.SetFocusedRowCellValue(cityCombo.Name, v.Substring(0, 4));
         //             gridView.SetFocusedRowCellValue(provinceCombo.Name, v.Substring(0, 2));
         //         }
         //     }
         // };
     }

     /// <summary>
     /// 绑定下拉列表控件为省市区
     /// </summary>
     /// <param name="combo">区级搜索下拉控件</param>
     /// <param name="isAreaOnly">仅限区县数据</param>
     public static void BindCityItems(this RepositoryItemSearchLookUpEdit combo, bool isAreaOnly = true)
     {
         var data = EnumerableExtension.Where(GB.AllRegion, isAreaOnly, x => x.Type == 3).ToList();
         combo.BindDictItems(data, "FullName", "Id", 400, new[]
         {
             new LookUpColumnInfo("Id", "地区编号", 80),
             new LookUpColumnInfo("Name", "地区名称", 120),
             new LookUpColumnInfo("FullName", "全称", 200)
         });
     }

     #endregion

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
     /// 为LookUpEdit添加删除按钮
     /// </summary>
     /// <param name="lookup">LookUpEdit</param>
     /// <param name="prompttext">删除按钮提示文字</param>
     public static void AddDeleteButton(this LookUpEdit lookup, string prompttext)
     {
         prompttext = string.IsNullOrEmpty(prompttext) ? "删除选中项" : prompttext;

         //多语言支持
         prompttext = JsonLanguage.Default.GetString(prompttext);
         string deleteText = JsonLanguage.Default.GetString("删除");

         lookup.Properties.Buttons.AddRange(new[]
         {
             new EditorButton(
                 ButtonPredefines.Delete,
                 deleteText, -1, true, true, false, ImageLocation.MiddleCenter,
                 null,
                 new KeyShortcut(Keys.Delete),
                 new SerializableAppearanceObject(),
                 prompttext,
                 "Delete",
                 null,
                 true)
         });

         lookup.ButtonClick += (sender, e) =>
         {
             if (e.Button.Kind == ButtonPredefines.Delete)
             {
                 LookUpEdit curLue = sender as LookUpEdit;
                 curLue.EditValue = null;
             }
         };
     }

     /// <summary>
     /// 自动完成功能绑定实现
     /// </summary>
     /// <param name="lookup">LookUpEdit</param>
     /// <param name="source">数据源</param>
     /// <param name="value">隐式字段</param>
     /// <param name="displayName">显示字段</param>
     /// <param name="prompttext">提示文字</param>
     public static void BindWithAutoCompletion(this LookUpEdit lookup, object source, string value, string displayName, string prompttext)
     {
         //多语言支持
         prompttext = JsonLanguage.Default.GetString(prompttext);

         lookup.Properties.DataSource = source;
         lookup.Properties.DisplayMember = displayName;
         lookup.Properties.ValueMember = value;
         lookup.Properties.NullText = prompttext;
         lookup.Properties.TextEditStyle = TextEditStyles.Standard;
         lookup.Properties.SearchMode = SearchMode.AutoFilter;
     }
 }