using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Reflection;
using BB.BaseUI.Other;
using BB.BaseUI.Pager;
using BB.BaseUI.WinForm;
using BB.Entity.Base;
using BB.Tools.Entity;
using BB.Tools.Extension;
using BB.Tools.Format;
using BB.Tools.MultiLanuage;
using BB.Tools.Validation;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using FluentValidation.Results;

namespace BB.BaseUI.Extension;

/// <summary>
/// GridView及其RepositoryItem编辑控件的扩展类
/// </summary>
public static class GridExtension
{
    #region GridView 初始化

    /// <summary>
    /// 初始化GridView对象
    /// </summary>
    /// <param name="gridView">GridView对象</param>
    /// <param name="gridType">Grid类型，默认为不显示新行</param>
    /// <param name="checkBoxSelect">是否出现勾选列</param>
    /// <param name="editorShowMode">编辑器显示模式</param>
    /// <param name="viewCaption">GridView标题</param>
    public static void InitGridView(this GridView gridView, GridType gridType = GridType.EditOnly, bool checkBoxSelect = false, 
        EditorShowMode editorShowMode = EditorShowMode.Default, string viewCaption = "")
    {
        // gridView.OptionsDetail.AllowOnlyOneMasterRowExpanded = true; // 只允许展开一行
        // gridView.OptionsDetail.AllowExpandEmptyDetails = true; // 允许展开空白的明细行
        // gridView.OptionsDetail.SmartDetailExpandButtonMode = DetailExpandButtonMode.AlwaysEnabled; // 可以展开
        gridView.OptionsView.ShowGroupPanel = false; // 是否出现分组面板
        gridView.OptionsCustomization.AllowFilter = false; // 禁用过滤器
        gridView.OptionsCustomization.AllowSort = false; // 禁用排序
        gridView.OptionsNavigation.AutoFocusNewRow = true; // 自动获取焦点
        gridView.OptionsNavigation.EnterMoveNextColumn = true; // 光标移到下一列时，自动跳到下一列
        gridView.OptionsView.ShowViewCaption = false; // 显示标题
        gridView.OptionsView.ColumnAutoWidth = false; // 自动调整列宽
        gridView.OptionsView.RowAutoHeight = true; // 自动调整行高
        gridView.OptionsSelection.MultiSelect = false; // 允许多选
        gridView.OptionsBehavior.EditorShowMode = editorShowMode; // 编辑器显示模式，默认单击直接可编辑。MouseDownFocused 为双击可编辑
        gridView.OptionsView.EnableAppearanceOddRow = true; // 奇数行颜色
        gridView.OptionsView.EnableAppearanceEvenRow = true; // 偶数行颜色
        gridView.Appearance.OddRow.BackColor = Color.Transparent; // 奇数行背景色
        gridView.Appearance.OddRow.BorderColor = Color.FromArgb(199, 209, 228); // 奇数行边框颜色
        gridView.Appearance.EvenRow.BackColor = Color.FromArgb(239, 243, 250); // 偶数行背景色
        gridView.Appearance.EvenRow.BorderColor = Color.FromArgb(199, 209, 228); // 偶数行边框颜色

        if (gridType == GridType.ReadOnly)
        {
            gridView.OptionsBehavior.Editable = false;
        }
        else
        {
            if (gridType == GridType.NewItem)
            {
                gridView.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;
            }
            else if (gridType == GridType.EditOnly)
            {
                gridView.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
            }

            gridView.GridControl.Enter += (sender, args) =>
            {
                if (gridView.RowCount == 0)
                {
                    gridView.AddNewRow();
                }
            };
        }

        if (checkBoxSelect)
        {
            gridView.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
            gridView.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DefaultBoolean.True;
            gridView.OptionsSelection.ShowCheckBoxSelectorInGroupRow = DefaultBoolean.True;
            gridView.OptionsSelection.ShowCheckBoxSelectorInPrintExport = DefaultBoolean.True;
        }

        if (!string.IsNullOrEmpty(viewCaption))
        {
            //多语言支持
            viewCaption = JsonLanguage.Default.GetString(viewCaption);

            gridView.ViewCaption = viewCaption;
            gridView.OptionsView.ShowViewCaption = true;
        }

        gridView.CustomColumnDisplayText += gridView_CustomColumnDisplayText;
        gridView.InvalidRowException += gridView_InvalidRowException;
    }

    /// <summary>
    /// 初始化 GridView Columns
    /// </summary>
    /// <param name="gridView">GridView对象</param>
    /// <param name="buttonClick"></param>
    public static void InitGridColumns(this GridView gridView, ButtonPressedEventHandler buttonClick)
    {
        gridView.Columns.Clear();

        gridView.CreateColumn("Operate", "操作", 60).CreateButtonEdit(new[]
        {
            new EditorButton()
            {
                Kind = ButtonPredefines.Plus,
                Caption = "新增",
                Tag = "add"
            },
            new EditorButton()
            {
                Kind = ButtonPredefines.Delete,
                Caption = "删除",
                Tag = "delete"
            }
        }, buttonClick);
    }

    /// <summary>
    /// 自定义列显示文本
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    static void gridView_CustomColumnDisplayText(object sender, CustomColumnDisplayTextEventArgs e)
    {
        if (e.Column.ColumnType == typeof(DateTime))
        {
            if (e.Value != null)
            {
                if (e.Value != null && (e.Value == DBNull.Value || Convert.ToDateTime(e.Value) <= Convert.ToDateTime("1900-1-1")))
                {
                    e.DisplayText = "";
                }
                else
                {
                    e.DisplayText = Convert.ToDateTime(e.Value).ToString("yyyy-MM-dd HH:mm"); //yyyy-MM-dd
                }
            }
        }
        
        if (e.Column.ColumnEdit != null && e.Column.ColumnEdit.GetType() == typeof(RepositoryItemComboBox))
        {
            var list = e.Column.ColumnEdit as RepositoryItemComboBox;
            string value = list?.GetComboBoxValue(e.Value);
            e.DisplayText = value.IsNullOrEmpty() ? e.Value.ObjToStr() : value;
        }
        
        if (e.Column.ColumnEdit != null && e.Column.ColumnEdit.GetType() == typeof(RepositoryItemSpinEdit))
        {
            var list = e.Column.ColumnEdit as RepositoryItemSpinEdit;
            e.DisplayText = list?.GetDisplayText(e.Value);
        }
    }

    /// <summary>
    /// 资料行的数据检查失败时
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    static void gridView_InvalidRowException(object sender, InvalidRowExceptionEventArgs e)
    {
        e.ErrorText.ShowUxError();
        // 禁止显示错误消息对话框
        e.ExceptionMode = ExceptionMode.NoAction;
    }

    #endregion

    #region GridColumn 创建和设置项扩展方法

    #region 创建空白列和基础属性

    /// <summary>
    /// 创建GridView的列
    /// </summary>
    public static GridColumn CreateColumn(this GridView gridView, string fieldName, string caption, int width = 80, bool allowEdit = true,
        UnboundColumnType unboundColumnType = UnboundColumnType.Bound, DefaultBoolean allowMerge = DefaultBoolean.False,
        FixedStyle fixedStyle = FixedStyle.None)
    {
        //使用多语言处理标题
        caption = JsonLanguage.Default.GetString(caption);

        GridColumn gridColumn = new GridColumn
        {
            FieldName = fieldName,
            Caption = caption,
            Width = width,
            UnboundType = unboundColumnType
        };

        gridView.Columns.Add(gridColumn);
        gridColumn.AbsoluteIndex = gridView.Columns.Count;
        gridColumn.VisibleIndex = gridView.Columns.Count;
        gridColumn.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
        gridColumn.AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;
        gridColumn.OptionsColumn.AllowEdit = allowEdit;

        if (!allowEdit)
        {
            gridColumn.AppearanceHeader.ForeColor = Color.Gray;
        }

        bool allowCellMerge = !gridView.OptionsView.AllowCellMerge && allowMerge == DefaultBoolean.True;
        if (allowCellMerge)
        {
            gridView.OptionsView.AllowCellMerge = true;
        }
        gridColumn.OptionsColumn.AllowMerge = allowMerge;
        gridColumn.Fixed = fixedStyle;

        bool isTime = caption.Contains("时间");
        if (isTime)
        {
            gridColumn.DisplayFormat.FormatType = FormatType.DateTime;
            gridColumn.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
        }
        else
        {
            bool isDate = caption.Contains("日期");
            if (isDate)
            {
                gridColumn.DisplayFormat.FormatType = FormatType.DateTime;
                gridColumn.DisplayFormat.FormatString = "yyyy-MM-dd";
            }
            else
            {
                bool isPercent = caption.Contains("百分比") || caption.Contains("率");
                if (isPercent)
                {
                    gridColumn.DisplayFormat.FormatType = FormatType.Numeric;
                    gridColumn.DisplayFormat.FormatString = "P";
                }
                else
                {
                    if (caption.Contains("费") || caption.Contains("款"))
                    {
                        gridColumn.DisplayFormat.FormatType = FormatType.Numeric;
                        gridColumn.DisplayFormat.FormatString = "0.00";
                    }
                }
            }
        }
        return gridColumn;
    }

    #endregion

    #region 列增加编辑控件

    /// <summary>
    /// 创建GridView的列编辑为LookUpEdit
    /// </summary>
    /// <param name="gridColumn">GridColumn列对象</param>
    /// <returns></returns>
    public static RepositoryItemLookUpEdit CreateLookUpEdit(this GridColumn gridColumn)
    {
        RepositoryItemLookUpEdit repositoryItem = new RepositoryItemLookUpEdit
        {
            Name = gridColumn.FieldName,
            AutoHeight = false,
            NullText = ""
        };
        repositoryItem.KeyUp += OnRepositoryItemOnKeyUp;
        gridColumn.View.GridControl.RepositoryItems.Add(repositoryItem);
        gridColumn.ColumnEdit = repositoryItem;
        return repositoryItem;
    }

    /// <summary>
    /// 创建GridView的列编辑为SearchLookUpEdit
    /// </summary>
    /// <param name="gridColumn">GridColumn列对象</param>
    /// <returns></returns>
    public static RepositoryItemSearchLookUpEdit CreateSearchLookUpEdit(this GridColumn gridColumn)
    {
        RepositoryItemSearchLookUpEdit repositoryItem = new RepositoryItemSearchLookUpEdit
        {
            Name = gridColumn.FieldName,
            AutoHeight = false,
            NullText = ""
        };
        repositoryItem.KeyUp += OnRepositoryItemOnKeyUp;
        gridColumn.View.GridControl.RepositoryItems.Add(repositoryItem);
        gridColumn.ColumnEdit = repositoryItem;
        return repositoryItem;
    }

    /// <summary>
    /// 创建GridView的列编辑为GridLookUpEdit
    /// </summary>
    /// <param name="gridColumn">GridColumn列对象</param>
    /// <returns></returns>
    public static RepositoryItemGridLookUpEdit CreateGridLookUpEdit(this GridColumn gridColumn)
    {
        RepositoryItemGridLookUpEdit repositoryItem = new RepositoryItemGridLookUpEdit
        {
            Name = gridColumn.FieldName,
            AutoHeight = false
        };
        repositoryItem.KeyUp += OnRepositoryItemOnKeyUp;
        GridView repositoryItemGridLookUpEditView = new GridView
        {
            FocusRectStyle = DrawFocusRectStyle.RowFocus
        };
        repositoryItemGridLookUpEditView.OptionsSelection.EnableAppearanceFocusedCell = false;
        repositoryItemGridLookUpEditView.OptionsView.ShowGroupPanel = false;
        repositoryItem.View = repositoryItemGridLookUpEditView;
        gridColumn.View.GridControl.RepositoryItems.Add(repositoryItem);
        gridColumn.ColumnEdit = repositoryItem;
        return repositoryItem;
    }

    /// <summary>
    /// 创建GridView的列编辑为ComboBox
    /// </summary>
    /// <param name="gridColumn">GridColumn列对象</param>
    /// <param name="textEditStyles">文本编辑样式</param>
    /// <returns></returns>
    public static RepositoryItemComboBox CreateComboBoxEdit(this GridColumn gridColumn, TextEditStyles textEditStyles = TextEditStyles.DisableTextEditor)
    {
        RepositoryItemComboBox repositoryItem = new RepositoryItemComboBox
        {
            Name = gridColumn.FieldName,
            AutoHeight = false,
            // 禁用文本输入
            TextEditStyle = textEditStyles
        };
        repositoryItem.KeyUp += OnRepositoryItemOnKeyUp;
        gridColumn.View.GridControl.RepositoryItems.Add(repositoryItem);
        gridColumn.ColumnEdit = repositoryItem;
        return repositoryItem;
    }

    /// <summary>
    /// 创建GridView的列编辑为ImageComboBox
    /// </summary>
    /// <param name="gridColumn">GridColumn列对象</param>
    /// <returns></returns>
    public static RepositoryItemImageComboBox CreateImageComboBoxEdit(this GridColumn gridColumn)
    {
        RepositoryItemImageComboBox repositoryItem = new RepositoryItemImageComboBox
        {
            Name = gridColumn.FieldName,
            AutoHeight = false,
            TextEditStyle = TextEditStyles.DisableTextEditor
        };
        repositoryItem.KeyUp += OnRepositoryItemOnKeyUp;
        gridColumn.View.GridControl.RepositoryItems.Add(repositoryItem);
        gridColumn.ColumnEdit = repositoryItem;
        return repositoryItem;
    }

    /// <summary>
    /// 创建GridView的列编辑为CheckedComboBoxEdit
    /// </summary>
    /// <param name="gridColumn">GridColumn列对象</param>
    /// <returns></returns>
    public static RepositoryItemCheckedComboBoxEdit CreateCheckedComboBoxEdit(this GridColumn gridColumn)
    {
        RepositoryItemCheckedComboBoxEdit repositoryItem = new RepositoryItemCheckedComboBoxEdit
        {
            Name = gridColumn.FieldName,
            AutoHeight = false,
            TextEditStyle = TextEditStyles.DisableTextEditor
        };
        repositoryItem.KeyUp += OnRepositoryItemOnKeyUp;
        gridColumn.View.GridControl.RepositoryItems.Add(repositoryItem);
        gridColumn.ColumnEdit = repositoryItem;
        return repositoryItem;
    }

    /// <summary>
    /// 创建GridView的列编辑为PopupContainerEdit
    /// </summary>
    /// <param name="gridColumn">GridColumn列对象</param>
    /// <param name="popupContainerControl">弹出的容器控件</param>
    /// <returns></returns>
    public static RepositoryItemPopupContainerEdit CreatePopupContainerEdit(this GridColumn gridColumn, PopupContainerControl popupContainerControl)
    {
        RepositoryItemPopupContainerEdit repositoryItem = new RepositoryItemPopupContainerEdit
        {
            Name = gridColumn.FieldName,
            AutoHeight = false
        };
        repositoryItem.KeyUp += OnRepositoryItemOnKeyUp;
        gridColumn.View.GridControl.RepositoryItems.Add(repositoryItem);
        gridColumn.ColumnEdit = repositoryItem;
        repositoryItem.CloseUpKey = new KeyShortcut(Keys.Space);
        repositoryItem.PopupControl = popupContainerControl;
        return repositoryItem;
    }

    /// <summary>
    /// 创建GridView的列编辑为PopupContainerEdit
    /// </summary>
    /// <param name="gridColumn">GridColumn列对象</param>
    /// <returns></returns>
    public static RepositoryItemPopupContainerEdit CreatePopupContainerEdit(this GridColumn gridColumn)
    {
        RepositoryItemPopupContainerEdit repositoryItem = new RepositoryItemPopupContainerEdit
        {
            Name = gridColumn.FieldName,
            AutoHeight = false
        };
        repositoryItem.KeyUp += OnRepositoryItemOnKeyUp;
        gridColumn.View.GridControl.RepositoryItems.Add(repositoryItem);
        gridColumn.ColumnEdit = repositoryItem;
        repositoryItem.CloseUpKey = new KeyShortcut(Keys.Space);
        return repositoryItem;
    }

    /// <summary>
    /// 创建GridView的列编辑为TextEdit
    /// </summary>
    /// <param name="gridColumn">GridColumn列对象</param>
    /// <returns></returns>
    public static RepositoryItemTextEdit CreateTextEdit(this GridColumn gridColumn)
    {
        RepositoryItemTextEdit repositoryItem = new RepositoryItemTextEdit
        {
            Name = gridColumn.FieldName,
            AutoHeight = false
        };
        gridColumn.View.GridControl.RepositoryItems.Add(repositoryItem);
        gridColumn.ColumnEdit = repositoryItem;
        return repositoryItem;
    }

    /// <summary>
    /// 创建GridView的列编辑为SpinEdit
    /// </summary>
    /// <param name="gridColumn">GridColumn列对象</param>
    /// <returns></returns>
    public static RepositoryItemSpinEdit CreateSpinEdit(this GridColumn gridColumn)
    {
        RepositoryItemSpinEdit repositoryItem = new RepositoryItemSpinEdit
        {
            Name = gridColumn.FieldName,
            Increment = decimal.One,
            AutoHeight = false
        };
        gridColumn.View.GridControl.RepositoryItems.Add(repositoryItem);
        gridColumn.ColumnEdit = repositoryItem;
        return repositoryItem;
    }

    /// <summary>
    /// 创建GridView的列编辑为CheckEdit
    /// </summary>
    /// <param name="gridColumn">GridColumn列对象</param>
    /// <returns></returns>
    public static RepositoryItemCheckEdit CreateCheckEdit(this GridColumn gridColumn)
    {
        RepositoryItemCheckEdit repositoryItem = new RepositoryItemCheckEdit
        {
            Name = gridColumn.FieldName,
            AutoHeight = false,
            ValueChecked = 1,
            ValueUnchecked = 0
        };
        gridColumn.View.GridControl.RepositoryItems.Add(repositoryItem);
        gridColumn.ColumnEdit = repositoryItem;
        return repositoryItem;
    }

    /// <summary>
    /// 创建GridView的列编辑为DateEdit
    /// </summary>
    /// <param name="gridColumn">GridColumn列对象</param>
    /// <returns></returns>
    public static RepositoryItemDateEdit CreateDateEdit(this GridColumn gridColumn)
    {
        RepositoryItemDateEdit repositoryItem = new RepositoryItemDateEdit
        {
            Name = gridColumn.FieldName,
            AutoHeight = false
        };
        gridColumn.View.GridControl.RepositoryItems.Add(repositoryItem);
        gridColumn.ColumnEdit = repositoryItem;
        return repositoryItem;
    }

    /// <summary>
    /// 创建GridView的列编辑为TimeEdit
    /// </summary>
    /// <param name="gridColumn">GridColumn列对象</param>
    /// <returns></returns>
    public static RepositoryItemTimeEdit CreationDateEdit(this GridColumn gridColumn)
    {
        RepositoryItemTimeEdit repositoryItem = new RepositoryItemTimeEdit
        {
            Name = gridColumn.FieldName,
            AutoHeight = false,
            EditMask = "yyyy-MM-dd HH:mm:ss"
        };
        gridColumn.View.GridControl.RepositoryItems.Add(repositoryItem);
        gridColumn.ColumnEdit = repositoryItem;
        return repositoryItem;
    }

    /// <summary>
    /// 创建GridView的列编辑为MemoEdit
    /// </summary>
    /// <param name="gridColumn">GridColumn列对象</param>
    /// <returns></returns>
    public static RepositoryItemMemoEdit CreateMemoEdit(this GridColumn gridColumn)
    {
        RepositoryItemMemoEdit repositoryItem = new RepositoryItemMemoEdit
        {
            Name = gridColumn.FieldName,
            AutoHeight = false,
            LinesCount = 0
        };
        gridColumn.View.GridControl.RepositoryItems.Add(repositoryItem);
        gridColumn.ColumnEdit = repositoryItem;
        return repositoryItem;
    }

    /// <summary>
    /// 创建GridView的列编辑为MemoExEdit
    /// </summary>
    /// <param name="gridColumn">GridColumn列对象</param>
    /// <returns></returns>
    public static RepositoryItemMemoExEdit CreateMemoExEdit(this GridColumn gridColumn)
    {
        RepositoryItemMemoExEdit repositoryItem = new RepositoryItemMemoExEdit
        {
            Name = gridColumn.FieldName,
            AutoHeight = false,
            ShowIcon = false,
            PopupFormSize = new Size(400, 200)
        };
        gridColumn.View.GridControl.RepositoryItems.Add(repositoryItem);
        gridColumn.ColumnEdit = repositoryItem;
        return repositoryItem;
    }

    #region ButtonEdit

    /// <summary>
    /// 创建GridView的列编辑为ButtonEdit
    /// </summary>
    /// <param name="gridColumn">GridColumn列对象</param>
    /// <param name="buttonClick"></param>
    /// <returns></returns>
    public static RepositoryItemButtonEdit CreateButtonEdit(this GridColumn gridColumn, ButtonPressedEventHandler buttonClick)
    {
        RepositoryItemButtonEdit repositoryBtn = new RepositoryItemButtonEdit();
        repositoryBtn.Name = gridColumn.FieldName;
        repositoryBtn.AppearanceDisabled.Options.UseTextOptions = true;
        repositoryBtn.AppearanceDisabled.TextOptions.HAlignment = HorzAlignment.Near;
        repositoryBtn.AutoHeight = false;
        repositoryBtn.TextEditStyle = TextEditStyles.HideTextEditor;
        repositoryBtn.ButtonsStyle = BorderStyles.UltraFlat;
        repositoryBtn.Buttons.Clear();
        repositoryBtn.Buttons.Add(
            new EditorButton()
            {
                Kind = ButtonPredefines.Delete,
                Caption = "删除",
                Tag = "delete"
            });
        repositoryBtn.ButtonClick += buttonClick;
        gridColumn.View.GridControl.RepositoryItems.Add(repositoryBtn);
        gridColumn.ColumnEdit = repositoryBtn;
        return repositoryBtn;
    }

    /// <summary>
    /// 创建GridView的列编辑为ButtonEdit
    /// </summary>
    /// <param name="gridColumn">GridColumn列对象</param>
    /// <param name="buttons"></param>
    /// <param name="buttonClick"></param>
    /// <returns></returns>
    public static RepositoryItemButtonEdit CreateButtonEdit(this GridColumn gridColumn, EditorButton[] buttons, ButtonPressedEventHandler buttonClick)
    {
        RepositoryItemButtonEdit repositoryBtn = new RepositoryItemButtonEdit();
        repositoryBtn.Name = gridColumn.FieldName;
        repositoryBtn.AppearanceDisabled.Options.UseTextOptions = true;
        repositoryBtn.AppearanceDisabled.TextOptions.HAlignment = HorzAlignment.Near;
        repositoryBtn.AutoHeight = false;
        repositoryBtn.TextEditStyle = TextEditStyles.HideTextEditor;
        repositoryBtn.ButtonsStyle = BorderStyles.UltraFlat;
        repositoryBtn.Buttons.Clear();
        repositoryBtn.Buttons.AddRange(buttons);
        repositoryBtn.ButtonClick += buttonClick;
        gridColumn.View.GridControl.RepositoryItems.Add(repositoryBtn);
        gridColumn.ColumnEdit = repositoryBtn;
        return repositoryBtn;
    }

    /// <summary>
    /// 创建GridView的列编辑为ButtonEdit
    /// 默认搜索图标
    /// </summary>
    /// <param name="gridColumn">GridColumn列对象</param>
    /// <returns></returns>
    public static RepositoryItemButtonEdit CreateButtonEdit(this GridColumn gridColumn, ButtonPredefines buttonPredefines = ButtonPredefines.Search)
    {
        RepositoryItemButtonEdit repositoryItem = new RepositoryItemButtonEdit
        {
            Name = gridColumn.FieldName,
            AutoHeight = false
        };
        repositoryItem.Buttons[0].Kind = buttonPredefines;
        gridColumn.View.GridControl.RepositoryItems.Add(repositoryItem);
        gridColumn.ColumnEdit = repositoryItem;
        repositoryItem.Buttons[0].Tag = gridColumn;
        return repositoryItem;
    }

    #endregion

    /// <summary>
    /// 创建GridView的列编辑为MRUEdit
    /// </summary>
    /// <param name="gridColumn">GridColumn列对象</param>
    /// <returns></returns>
    public static RepositoryItemMRUEdit CreateMruEdit(this GridColumn gridColumn)
    {
        RepositoryItemMRUEdit repositoryItem = new RepositoryItemMRUEdit
        {
            Name = gridColumn.FieldName,
            AutoHeight = false
        };
        gridColumn.View.GridControl.RepositoryItems.Add(repositoryItem);
        gridColumn.ColumnEdit = repositoryItem;
        return repositoryItem;
    }

    /// <summary>
    /// 创建GridView的列编辑为PictureEdit
    /// </summary>
    /// <param name="gridColumn">GridColumn列对象</param>
    /// <returns></returns>
    public static RepositoryItemPictureEdit CreatePictureEdit(this GridColumn gridColumn)
    {
        RepositoryItemPictureEdit repositoryItem = new RepositoryItemPictureEdit
        {
            Name = gridColumn.FieldName,
            SizeMode = PictureSizeMode.Zoom,
            PictureInterpolationMode = InterpolationMode.High,
            NullText = " "
        };
        gridColumn.View.GridControl.RepositoryItems.Add(repositoryItem);
        gridColumn.ColumnEdit = repositoryItem;
        return repositoryItem;
    }

    /// <summary>
    /// 创建GridView的列编辑为RadioGroup
    /// </summary>
    /// <param name="gridColumn">GridColumn列对象</param>
    /// <returns></returns>
    public static RepositoryItemRadioGroup CreateRadioGroup(this GridColumn gridColumn)
    {
        RepositoryItemRadioGroup repositoryItem = new RepositoryItemRadioGroup();
        gridColumn.View.GridControl.RepositoryItems.Add(repositoryItem);
        gridColumn.ColumnEdit = repositoryItem;
        return repositoryItem;
    }

    /// <summary>
    /// 创建GridView的列编辑为HyperLinkEdit
    /// </summary>
    /// <param name="gridColumn">GridColumn列对象</param>
    /// <returns></returns>
    public static RepositoryItemHyperLinkEdit CreateHyperLinkEdit(this GridColumn gridColumn)
    {
        RepositoryItemHyperLinkEdit repositoryItem = new RepositoryItemHyperLinkEdit
        {
            Name = gridColumn.FieldName,
            AutoHeight = false
        };
        gridColumn.View.GridControl.RepositoryItems.Add(repositoryItem);
        gridColumn.ColumnEdit = repositoryItem;
        return repositoryItem;
    }

    /// <summary>
    /// 创建GridView的列编辑为ImageEdit
    /// </summary>
    /// <param name="gridColumn">GridColumn列对象</param>
    /// <returns></returns>
    public static RepositoryItemImageEdit CreateImageEdit(this GridColumn gridColumn)
    {
        RepositoryItemImageEdit repositoryItem = new RepositoryItemImageEdit
        {
            Name = gridColumn.FieldName,
            AutoHeight = false,
            SizeMode = PictureSizeMode.Zoom
        };
        gridColumn.View.GridControl.RepositoryItems.Add(repositoryItem);
        gridColumn.ColumnEdit = repositoryItem;
        return repositoryItem;
    }

    /// <summary>
    /// 创建GridView的列编辑为CalcEdit
    /// </summary>
    /// <param name="gridColumn">GridColumn列对象</param>
    /// <returns></returns>
    public static RepositoryItemCalcEdit CreateCalcEdit(this GridColumn gridColumn)
    {
        RepositoryItemCalcEdit repositoryItem = new RepositoryItemCalcEdit
        {
            Name = gridColumn.FieldName,
            AutoHeight = false
        };
        gridColumn.View.GridControl.RepositoryItems.Add(repositoryItem);
        gridColumn.ColumnEdit = repositoryItem;
        return repositoryItem;
    }

    /// <summary>
    /// 创建GridView的列编辑为ColorEdit
    /// </summary>
    /// <param name="gridColumn">GridColumn列对象</param>
    /// <returns></returns>
    public static RepositoryItemColorEdit CreateColorEdit(this GridColumn gridColumn)
    {
        RepositoryItemColorEdit repositoryItem = new RepositoryItemColorEdit
        {
            Name = gridColumn.FieldName,
            AutoHeight = false
        };
        gridColumn.View.GridControl.RepositoryItems.Add(repositoryItem);
        gridColumn.ColumnEdit = repositoryItem;
        return repositoryItem;
    }

    /// <summary>
    /// 创建GridView的列编辑为FontEdit
    /// </summary>
    /// <param name="gridColumn">GridColumn列对象</param>
    /// <returns></returns>
    public static RepositoryItemFontEdit CreateFontEdit(this GridColumn gridColumn)
    {
        RepositoryItemFontEdit repositoryItem = new RepositoryItemFontEdit
        {
            Name = gridColumn.FieldName,
            AutoHeight = false
        };
        gridColumn.View.GridControl.RepositoryItems.Add(repositoryItem);
        gridColumn.ColumnEdit = repositoryItem;
        return repositoryItem;
    }

    /// <summary>
    /// 创建GridView的列编辑为ToggleSwitch
    /// </summary>
    /// <param name="gridColumn">GridColumn列对象</param>
    /// <returns></returns>
    public static RepositoryItemToggleSwitch CreateToggleSwitch(this GridColumn gridColumn)
    {
        RepositoryItemToggleSwitch repositoryItem = new RepositoryItemToggleSwitch
        {
            Name = gridColumn.FieldName,
            AutoHeight = false,
            OnText = "",
            OffText = ""
        };
        gridColumn.View.GridControl.RepositoryItems.Add(repositoryItem);
        gridColumn.ColumnEdit = repositoryItem;
        return repositoryItem;
    }

    #endregion

    /// <summary>
    /// 设置GridView的列为只读
    /// </summary>
    /// <param name="gridColumn">GridColumn列对象</param>
    /// <param name="allowEdit">是否允许编辑，默认为false，只读</param>
    public static GridColumn SetReadOnly(this GridColumn gridColumn, bool allowEdit = false)
    {
        gridColumn.OptionsColumn.AllowEdit = allowEdit;
        gridColumn.OptionsColumn.ReadOnly = !allowEdit;
        if (!allowEdit)
        {
            gridColumn.AppearanceHeader.ForeColor = Color.Gray;
        }
        else
        {
            gridColumn.AppearanceHeader.ForeColor = Color.Black;
        }
        return gridColumn;
    }

    /// <summary>
    /// 是否使用密码字符（如*号）来隐藏内容
    /// </summary>
    /// <param name="gridColumn">GridColumn列对象</param>
    /// <param name="masked">是否使用隐藏</param>
    /// <param name="passwordChar">密码字符，默认为*</param>
    public static GridColumn SetPasswordChar(this GridColumn gridColumn, bool masked = true, char passwordChar = '*')
    {
        if(masked)
        {
            gridColumn.CreateTextEdit().PasswordChar = passwordChar;
        }
        return gridColumn;
    }

    /// <summary>
    /// 键盘按下，自动弹出选框
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

    #endregion

    #region GridControl 校验 和 设置 扩展

    /// <summary>
    /// 校验GridControl控件对象
    /// </summary>
    /// <param name="gridControl">GridControl控件对象</param>
    /// <returns></returns>
    public static bool ValidateGridEditor(this GridControl gridControl)
    {
        bool result;
        if (gridControl == null)
        {
            result = true;
        }
        else
        {
            GridView gridView = gridControl.FocusedView as GridView;
            if (gridView == null)
            {
                result = true;
            }
            else
            {
                if (!gridView.OptionsBehavior.Editable)
                {
                    result = true;
                }
                else
                {
                    gridView.PostEditor();
                    if (!gridView.ValidateEditor())
                    {
                        result = false;
                    }
                    else
                    {
                        result = gridView.UpdateCurrentRow();
                    }
                }
            }
        }
        return result;
    }

    /// <summary>
    /// 设置GridControl是否可以编辑
    /// </summary>
    /// <param name="gridControl">GridControl控件对象</param>
    /// <param name="editable">是否可以编辑</param>
    public static void SetGridEditable(this GridControl gridControl, bool editable)
    {
        if (gridControl != null)
        {
            foreach (GridView gridView in gridControl.ViewCollection)
            {
                gridView.OptionsBehavior.Editable = editable;
            }
        }
    }

    #endregion

    #region GridView 控件设置扩展方法

    /// <summary>
    /// 设置统计列内容
    /// </summary>
    /// <param name="gridView">GridView对象</param>
    /// <param name="fieldName">统计字段</param>
    /// <param name="summaryItemType">统计类型</param>
    /// <param name="prefix">显示前缀</param>
    public static void SetSummaryColumn(this GridView gridView, string fieldName, SummaryItemType summaryItemType = SummaryItemType.Sum,
        string prefix = "")
    {
        if (!gridView.OptionsView.ShowFooter)
        {
            gridView.OptionsView.ShowFooter = true;
        }

        string upperFieldName = fieldName;
        gridView.Columns[upperFieldName].SummaryItem.FieldName = upperFieldName;
        gridView.Columns[upperFieldName].SummaryItem.DisplayFormat = gridView.Columns[upperFieldName].DisplayFormat.FormatString;
        gridView.Columns[upperFieldName].SummaryItem.SummaryType = summaryItemType;
        gridView.Columns[upperFieldName].SummaryItem.DisplayFormat = prefix + "{0}";
    }

    /// <summary>
    /// 设置GridView的列为只读
    /// </summary>
    /// <param name="gridView">GridView对象</param>
    /// <param name="fieldName">操作字段名</param>
    /// <param name="allowEdit">是否允许编辑，默认为false，只读</param>
    public static void SetReadOnly(this GridView gridView, string fieldName, bool allowEdit = false)
    {
        var gridColumn = gridView.Columns[fieldName];
        if (gridColumn != null)
        {
            gridColumn.OptionsColumn.AllowEdit = allowEdit;
            gridColumn.OptionsColumn.ReadOnly = !allowEdit;
            if (!allowEdit)
            {
                gridColumn.AppearanceHeader.ForeColor = Color.Gray;
            }
            else
            {
                gridColumn.AppearanceHeader.ForeColor = Color.Black;
            }
        }
    }

    /// <summary>
    /// 设置GridView的列为只读与否
    /// </summary>
    /// <param name="gridView">GridView对象</param>
    /// <param name="fieldNameString">字段列表，逗号分开,如果需要设置所有，那么用*代替</param>
    /// <param name="allowEdit">是否允许编辑</param>
    public static void SetColumnsReadOnly(this GridView gridView, string fieldNameString, bool allowEdit = false)
    {
        if (!string.IsNullOrEmpty(fieldNameString))
        {
            List<string> includeList = fieldNameString.ToDelimitedList<string>(",");
            foreach (GridColumn col in gridView.Columns)
            {
                if (fieldNameString == "*")
                {
                    col.OptionsColumn.AllowEdit = allowEdit;
                    col.OptionsColumn.ReadOnly = !allowEdit;
                    if (!allowEdit)
                    {
                        col.AppearanceHeader.ForeColor = Color.Gray;
                    }
                    else
                    {
                        col.AppearanceHeader.ForeColor = Color.Black;
                    }
                }
                else
                {
                    var include = includeList.Contains(col.FieldName);
                    if (include)
                    {
                        col.OptionsColumn.AllowEdit = allowEdit;
                        col.OptionsColumn.ReadOnly = !allowEdit;
                        if (!allowEdit)
                        {
                            col.AppearanceHeader.ForeColor = Color.Gray;
                        }
                        else
                        {
                            col.AppearanceHeader.ForeColor = Color.Black;
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// 设置GridView的列为显示与否
    /// </summary>
    /// <param name="gridView">GridView对象</param>
    /// <param name="fieldNameString">字段列表，逗号分开,如果需要设置所有，那么用*代替</param>
    /// <param name="visible">是否允许编辑</param>
    public static void SetColumnsVisible(this GridView gridView, string fieldNameString, bool visible = false)
    {
        if (!string.IsNullOrEmpty(fieldNameString))
        {
            List<string> includeList = fieldNameString.ToDelimitedList<string>(",");
            foreach (GridColumn col in gridView.Columns)
            {
                if (fieldNameString == "*")
                {
                    col.Visible = visible;
                }
                else
                {
                    var include = includeList.Contains(col.FieldName);
                    if (include)
                    {
                        col.Visible = visible;
                    }
                }
            }
        }
    }

    /// <summary>
    /// 是否使用密码字符（如*号）来隐藏内容
    /// </summary>
    /// <param name="gridView">GridView对象</param>
    /// <param name="fieldNameString">字段列表，逗号分开,如果需要设置所有，那么用*代替</param>
    /// <param name="masked">是否遮挡</param>
    /// <param name="passwordChar">遮挡字符，默认为*字符</param>
    public static void SetColumnsPasswordChar(this GridView gridView, string fieldNameString, bool masked = true, char passwordChar='*')
    {
        if (!string.IsNullOrEmpty(fieldNameString) && masked)
        {
            List<string> includeList = fieldNameString.ToDelimitedList<string>(",");
            foreach (GridColumn col in gridView.Columns)
            {
                if (fieldNameString == "*")
                {
                    col.CreateTextEdit().PasswordChar = passwordChar;
                }
                else
                {
                    var include = includeList.Contains(col.FieldName);
                    if (include)
                    {
                        col.CreateTextEdit().PasswordChar = passwordChar;
                    }
                }
            }
        }
    }
        
    /// <summary>
    /// 根据参数权限字典的值：0可读写，1只读，2隐藏值，3不显示，设置列的权限。
    /// </summary>
    /// <param name="gridView">GridView对象</param>
    /// <param name="fieNamePermitDict">字段和权限字典，字典值为权限控制：0可读写，1只读，2隐藏值，3不显示</param>
    public static void SetColumnsPermit(this GridView gridView, Dictionary<string,int> fieNamePermitDict)
    {
        char passwordChar = '*';
        foreach (GridColumn col in gridView.Columns)
        {
            var include = fieNamePermitDict.ContainsKey(col.FieldName);
            if (include)
            {
                int permit = fieNamePermitDict[col.FieldName];
                switch (permit)
                {
                    case 0://正常可见、可读写
                        col.OptionsColumn.AllowEdit = true;
                        col.OptionsColumn.ReadOnly = false;
                        col.AppearanceHeader.ForeColor = Color.Black;

                        col.Visible = true;
                        break;

                    case 1:
                        //只读
                        col.OptionsColumn.AllowEdit = false;
                        col.OptionsColumn.ReadOnly = true;
                        col.AppearanceHeader.ForeColor = Color.Gray;

                        col.Visible = true;
                        break;

                    case 2:
                        //隐藏值
                        var edit = col.CreateTextEdit();                            
                        col.Tag = string.Concat(passwordChar);//用来在界面端进行判断，避免设置DisplayText
                        edit.PasswordChar = passwordChar;
                        col.Visible = true;
                        break;

                    case 3:
                        //不可见
                        col.Visible = false;
                        break;
                }
            }
        }            
    }
        
    /// <summary>
    /// 设置GridView的标题显示
    /// </summary>
    /// <param name="gridView">GridView对象</param>
    /// <param name="caption">显示标题</param>
    public static void SetGridViewCaption(this GridView gridView, string caption)
    {
        //使用多语言处理标题
        caption = JsonLanguage.Default.GetString(caption);

        if (!gridView.OptionsView.ShowViewCaption)
        {
            gridView.OptionsView.ShowViewCaption = true;
        }
        gridView.ViewCaption = caption;
    }
        
    /// <summary>
    /// 设置GridView列的宽度
    /// </summary>
    /// <param name="gridView">gridView对象</param>
    /// <param name="columnName">列名称，大小写要注意</param>
    /// <param name="width">宽度，默认我100</param>
    public static GridColumn SetGridColumWidth(this GridView gridView, string columnName, int width = 100)
    {
        GridColumn column = gridView.Columns.ColumnByFieldName(columnName);
        if (column != null)
        {
            column.Width = width;
        }
        else
        {
            column = gridView.Columns.ColumnByFieldName(columnName.ToUpper());
            if (column != null)
            {
                column.Width = width;
            }
        }
        return column;
    }

    #endregion

    #region 控件验证

    /// <summary>
    /// 统一处理验证结果
    /// </summary>
    /// <param name="result"></param>
    /// <returns></returns>
    public static bool ProcessValidationResults(this ColumnView cv, ValidateRowEventArgs e, ValidationResult result)
    {
        if (!result.IsValid)
        {
            result.Errors.ForEach(x =>
            {
                e.Valid = false;
                e.ErrorText = x.ErrorMessage;
                var column = cv.Columns[x.PropertyName];
                cv.FocusedColumn = column;
                cv.SetColumnError(column, e.ErrorText);
            });
        }

        return result.IsValid;
    }

    /// <summary>
    /// 检查指定行的字段
    /// </summary>
    /// <param name="cv">ColumnView</param>
    /// <param name="e">ValidateRowEventArgs</param>
    /// <param name="fieldName">字段名</param>
    /// <param name="validateType">验证类型</param>
    /// <param name="checkNull">是否可空</param>
    /// <param name="max">最大长度</param>
    /// <param name="min">最小长度</param>
    /// <param name="errorText">自定义错误提示</param>
    /// <returns></returns>
    public static bool ValidateRowField(this ColumnView cv, ValidateRowEventArgs e, string fieldName,
        ValidateType validateType = ValidateType.Null, bool checkNull = false, int max = 0, int min = 0, string errorText = null)
    {
        if ((checkNull || validateType == ValidateType.Null) && !cv.ValidateRowNull(e, fieldName)) return false;
        if (max > 0 && !cv.ValidateRange(e, fieldName, max, min, errorText)) return false;

        return validateType switch
        {
            ValidateType.UserName => ValidateUserName(cv, e, errorText, fieldName),
            ValidateType.Phone => ValidatePhone(cv, e, errorText, fieldName),
            ValidateType.Email => ValidateEmail(cv, e, errorText, fieldName),
            ValidateType.URL => ValidateURL(cv, e, errorText, fieldName),
            ValidateType.Number => ValidateNumber(cv, e, errorText, fieldName),
            ValidateType.Mobile => ValidateMobile(cv, e, errorText, fieldName),
            ValidateType.IdCard => ValidateIdCard(cv, e, errorText, fieldName),
            ValidateType.Chinese => ValidateChinese(cv, e, errorText, fieldName),
            ValidateType.Decimal => ValidateDecimal(cv, e, errorText, fieldName),
            ValidateType.Letter => ValidateLetter(cv, e, errorText, fieldName),
            ValidateType.Numeric => ValidateNumeric(cv, e, errorText, fieldName),
            ValidateType.FilePath => ValidateFilePath(cv, e, errorText, fieldName),
            ValidateType.PhoneAndMobile => ValidatePhoneAndMobile(cv, e, errorText, fieldName),
            _ => true
        };
    }

    /// <summary>
    /// 检验输入的长度是否合法
    /// </summary>
    /// <param name="cv">ColumnView</param>
    /// <param name="e">ValidateRowEventArgs</param>
    /// <param name="fieldName">字段名</param>
    /// <param name="max">最大长度</param>
    /// <param name="min">最小长度</param>
    /// <param name="errorText">自定义错误提示</param>
    /// <returns></returns>
    public static bool ValidateRange(this ColumnView cv, ValidateRowEventArgs e, string fieldName, int max, int min = 0, 
        string errorText = null)
    {
        GridColumn column = cv.Columns[fieldName];
        RepositoryItem baseEdit = column.ColumnEdit;
        object editObject = cv.GetRowCellValue(e.RowHandle, column);
        string editValue;
        if (baseEdit is RepositoryItemSearchLookUpEdit && editObject is long l)
            editValue = l == 0 ? string.Empty : l.ToString();
        else if (baseEdit is RepositoryItemComboBox && editObject is CListItem item)
            editValue = item.Value;
        else
            editValue = string.Concat(editObject).Trim();
        // 如果是空或是 无 直接返回空，不验证
        if (!editValue.IsNullOrEmpty() && editValue is not "无" or "请选择" && !ValidateUtil.IsRange(editValue, min, max))
        {
            e.Valid = false;
            e.ErrorText = errorText ?? $"内容长度必须在{min}-{max}之间";
            cv.FocusedColumn = column;
            cv.SetColumnError(column, e.ErrorText);
        }
        return e.Valid;
    }

    /// <summary>
    /// 检查某行字段是否为空
    /// </summary>
    /// <param name="cv">ColumnView</param>
    /// <param name="e">ValidateRowEventArgs</param>
    /// <param name="errorText">自定义错误提示</param>
    /// <param name="fieldNames">字段名数组</param>
    /// <returns></returns>
    public static bool ValidateRowNull(this ColumnView cv, ValidateRowEventArgs e, string errorText = null, params string[] fieldNames)
    {
        foreach (string fieldName in fieldNames)
        {
            GridColumn column = cv.Columns[fieldName];
            RepositoryItem baseEdit = column.ColumnEdit;

            object editObject = cv.GetRowCellValue(e.RowHandle, column);
            string editValue;
            if (baseEdit is RepositoryItemSearchLookUpEdit && editObject is long l)
                editValue = l == 0 ? string.Empty : l.ToString();
            else if (baseEdit is RepositoryItemComboBox && editObject is CListItem item)
                editValue = item.Value;
            else
                editValue = string.Concat(editObject).Trim();
            if (editValue.IsNullOrEmpty() || editValue is "无" or "请选择")
            {
                e.Valid = false;
                e.ErrorText = errorText ?? $"{column.Caption}不能为空";
                cv.FocusedColumn = column;
                cv.SetColumnError(column, e.ErrorText);
            }
        }
        return e.Valid;
    }

    /// <summary>
    /// 检查指定行的字段是否为重复
    /// </summary>
    /// <param name="cv">ColumnView</param>
    /// <param name="e">BaseContainerValidateEditorEventArgs</param>
    /// <param name="primaryKey">主键</param>
    /// <param name="errorText">自定义错误提示</param>
    /// <param name="fieldNames">字段名数组</param>
    /// <returns></returns>
    public static bool ValidateRowRepeat(this ColumnView cv, BaseContainerValidateEditorEventArgs e, string primaryKey, string errorText = null, params string[] fieldNames)
    {
        string focuseKeyValue = string.Concat(cv.GetFocusedRowCellValue(primaryKey));
        foreach (string fieldName in fieldNames)
        {
            if (cv.FocusedColumn.FieldName == fieldName)
            {
                GridColumn column = cv.Columns[fieldName];
                for (int r = 0; r < cv.RowCount; r++)
                {
                    string keyValue = string.Concat(cv.GetRowCellValue(r, primaryKey));
                    if (!string.IsNullOrEmpty(keyValue) && focuseKeyValue != keyValue
                                                        && string.Concat(e.Value) == string.Concat(cv.GetRowCellValue(r, fieldName)))
                    {
                        e.Valid = false;
                        e.ErrorText = errorText ?? $"{column.Caption}不能重复";
                        cv.FocusedColumn = column;
                        cv.SetColumnError(column, e.ErrorText);
                    }
                }
            }
        }
        return e.Valid;
    }

    /// <summary>
    /// 检查指定行的字段
    /// </summary>
    /// <param name="cv">ColumnView</param>
    /// <param name="e">ValidateRowEventArgs</param>
    /// <param name="boolFunc">验证委托</param>
    /// <param name="errorText">自定义错误提示</param>
    /// <param name="fieldNames">字段名数组</param>
    /// <returns></returns>
    public static bool ValidateRow(this ColumnView cv, ValidateRowEventArgs e, Function<bool, string> boolFunc,
        string errorText, params string[] fieldNames)
    {
        foreach (string fieldName in fieldNames)
        {
            GridColumn column = cv.Columns[fieldName];
            RepositoryItem baseEdit = column.ColumnEdit;
            object editObject = cv.GetRowCellValue(e.RowHandle, column);
            string editValue;
            if (baseEdit is RepositoryItemSearchLookUpEdit && editObject is long l)
                editValue = l == 0 ? string.Empty : l.ToString();
            else if (baseEdit is RepositoryItemComboBox && editObject is CListItem item)
                editValue = item.Value;
            else
                editValue = string.Concat(editObject).Trim();
            // 如果是空或是 无 直接返回空，不验证
            if (!editValue.IsNullOrEmpty() && editValue is not "无" or "请选择" && !boolFunc(editValue))
            {
                e.Valid = false;
                e.ErrorText = $"{column.Caption}，{errorText}";
                cv.FocusedColumn = column;
                cv.SetColumnError(column, e.ErrorText);
            }
        }
        return e.Valid;
    }

    /// <summary>
    /// 检验用户名格式是否有效
    /// </summary>
    /// <param name="cv">ColumnView</param>
    /// <param name="e">ValidateRowEventArgs</param>
    /// <param name="errorText">自定义错误提示</param>
    /// <param name="fieldNames">字段名数组</param>
    /// <returns></returns>
    public static bool ValidateUserName(this ColumnView cv, ValidateRowEventArgs e, 
        string errorText = null, params string[] fieldNames)
    {
        return ValidateRow(cv, e, ValidateUtil.IsValidUserName, errorText ?? "用户名不合法", fieldNames);
    }

    /// <summary>
    /// 检验输入是否是数字
    /// </summary>
    /// <param name="cv">ColumnView</param>
    /// <param name="e">ValidateRowEventArgs</param>
    /// <param name="errorText">自定义错误提示</param>
    /// <param name="fieldNames">字段名数组</param>
    /// <returns></returns>
    public static bool ValidateNumeric(this ColumnView cv, ValidateRowEventArgs e, 
        string errorText = null, params string[] fieldNames)
    {
        return ValidateRow(cv, e, ValidateUtil.IsNumeric, errorText ?? "请输入数字", fieldNames);
    }

    /// <summary>
    /// 检验输入是否是整数
    /// </summary>
    /// <param name="cv">ColumnView</param>
    /// <param name="e">ValidateRowEventArgs</param>
    /// <param name="errorText">自定义错误提示</param>
    /// <param name="fieldNames">字段名数组</param>
    /// <returns></returns>
    public static bool ValidateNumber(this ColumnView cv, ValidateRowEventArgs e, 
        string errorText = null, params string[] fieldNames)
    {
        return ValidateRow(cv, e, ValidateUtil.IsNumberSign, errorText ?? "请输入整数", fieldNames);
    }

    /// <summary>
    /// 检验输入是否是小数
    /// </summary>
    /// <param name="cv">ColumnView</param>
    /// <param name="e">ValidateRowEventArgs</param>
    /// <param name="errorText">自定义错误提示</param>
    /// <param name="fieldNames">字段名数组</param>
    /// <returns></returns>
    public static bool ValidateDecimal(this ColumnView cv, ValidateRowEventArgs e, 
        string errorText = null, params string[] fieldNames)
    {
        return ValidateRow(cv, e, ValidateUtil.IsDecimal, errorText ?? "请输入小数", fieldNames);
    }

    /// <summary>
    /// 检验输入是否包含中文
    /// </summary>
    /// <param name="cv">ColumnView</param>
    /// <param name="e">ValidateRowEventArgs</param>
    /// <param name="errorText">自定义错误提示</param>
    /// <param name="fieldNames">字段名数组</param>
    /// <returns></returns>
    public static bool ValidateChinese(this ColumnView cv, ValidateRowEventArgs e, 
        string errorText = null, params string[] fieldNames)
    {
        return ValidateRow(cv, e, ValidateUtil.IsChinese, errorText ?? "请输入中文", fieldNames);
    }

    /// <summary>
    /// 检验输入是否纯字母
    /// </summary>
    /// <param name="cv">ColumnView</param>
    /// <param name="e">ValidateRowEventArgs</param>
    /// <param name="errorText">自定义错误提示</param>
    /// <param name="fieldNames">字段名数组</param>
    /// <returns></returns>
    public static bool ValidateLetter(this ColumnView cv, ValidateRowEventArgs e, 
        string errorText = null, params string[] fieldNames)
    {
        return ValidateRow(cv, e, ValidateUtil.IsLetter, errorText ?? "请输入字母", fieldNames);
    }

    /// <summary>
    /// 检验输入是否是身份证
    /// </summary>
    /// <param name="cv">ColumnView</param>
    /// <param name="e">ValidateRowEventArgs</param>
    /// <param name="errorText">自定义错误提示</param>
    /// <param name="fieldNames">字段名数组</param>
    /// <returns></returns>
    public static bool ValidateIdCard(this ColumnView cv, ValidateRowEventArgs e, 
        string errorText = null, params string[] fieldNames)
    {
        return ValidateRow(cv, e, ValidateUtil.IsIdCard, errorText ?? "身份证不合法", fieldNames);
    }

    /// <summary>
    /// 检验输入是否是邮件地址
    /// </summary>
    /// <param name="cv">ColumnView</param>
    /// <param name="e">ValidateRowEventArgs</param>
    /// <param name="errorText">自定义错误提示</param>
    /// <param name="fieldNames">字段名数组</param>
    /// <returns></returns>
    public static bool ValidateEmail(this ColumnView cv, ValidateRowEventArgs e, 
        string errorText = null, params string[] fieldNames)
    {
        return ValidateRow(cv, e, ValidateUtil.IsEmail, errorText ?? "邮箱地址不合法", fieldNames);
    }

    /// <summary>
    /// 检验输入是否是固定电话
    /// </summary>
    /// <param name="cv">ColumnView</param>
    /// <param name="e">ValidateRowEventArgs</param>
    /// <param name="errorText">自定义错误提示</param>
    /// <param name="fieldNames">字段名数组</param>
    /// <returns></returns>
    public static bool ValidatePhone(this ColumnView cv, ValidateRowEventArgs e, 
        string errorText = null, params string[] fieldNames)
    {
        return ValidateRow(cv, e, ValidateUtil.IsPhone, errorText ?? "请输入正确的电话号码", fieldNames);
    }

    /// <summary>
    /// 检验输入是否是手机
    /// </summary>
    /// <param name="cv">ColumnView</param>
    /// <param name="e">ValidateRowEventArgs</param>
    /// <param name="errorText">自定义错误提示</param>
    /// <param name="fieldNames">字段名数组</param>
    /// <returns></returns>
    public static bool ValidateMobile(this ColumnView cv, ValidateRowEventArgs e, 
        string errorText = null, params string[] fieldNames)
    {
        return ValidateRow(cv, e, ValidateUtil.IsMobile, errorText ?? "请输入正确的手机号码", fieldNames);
    }

    /// <summary>
    /// 检验输入是否是固话和手机
    /// </summary>
    /// <param name="cv">ColumnView</param>
    /// <param name="e">ValidateRowEventArgs</param>
    /// <param name="errorText">自定义错误提示</param>
    /// <param name="fieldNames">字段名数组</param>
    /// <returns></returns>
    public static bool ValidatePhoneAndMobile(this ColumnView cv, ValidateRowEventArgs e, 
        string errorText = null, params string[] fieldNames)
    {
        return ValidateRow(cv, e, ValidateUtil.IsPhoneAndMobile, errorText ?? "请输入正确的联系方式", fieldNames);
    }

    /// <summary>
    /// 检验输入是否是URL
    /// </summary>
    /// <param name="cv">ColumnView</param>
    /// <param name="e">ValidateRowEventArgs</param>
    /// <param name="errorText">自定义错误提示</param>
    /// <param name="fieldNames">字段名数组</param>
    /// <returns></returns>
    public static bool ValidateURL(this ColumnView cv, ValidateRowEventArgs e, 
        string errorText = null, params string[] fieldNames)
    {
        return ValidateRow(cv, e, ValidateUtil.IsUrl, errorText ?? "请输入正确的URL", fieldNames);
    }

    /// <summary>
    /// 检验输入是否是文件路径
    /// </summary>
    /// <param name="cv">ColumnView</param>
    /// <param name="e">ValidateRowEventArgs</param>
    /// <param name="errorText">自定义错误提示</param>
    /// <param name="fieldNames">字段名数组</param>
    /// <returns></returns>
    public static bool ValidateFilePath(this ColumnView cv, ValidateRowEventArgs e, 
        string errorText = null, params string[] fieldNames)
    {
        return ValidateRow(cv, e, ValidateUtil.IsFilePath, errorText ?? "请输入正确的文件路径", fieldNames);
    }

    #endregion  
        
    #region Excel导出操作

    /// <summary>
    /// 从GridView导出到Excel文件，并可以打开文件
    /// </summary>
    /// <param name="grv">GridView</param>
    /// <param name="fileName">保存的文件名称</param>
    /// <param name="open">是否打开</param>
    /// <param name="printSelectedRowsOnly">是否打印选定行</param>
    public static void ExportToExcel(this GridView grv, string fileName = "", bool open = true, bool printSelectedRowsOnly = false)
    {
        string filePath = FileDialogHelper.SaveExcel(fileName);
        if (!string.IsNullOrEmpty(filePath))
        {
            SetOptionPrint(grv, printSelectedRowsOnly);
            grv.ExportToXls(filePath);
            ShowOpenFileDialog(open, filePath);
        }
    }

    /// <summary>
    /// 从GridView集合导出到Excel文件，并可以打开文件
    /// </summary>        
    /// <param name="grd">GridControl对象</param>
    /// <param name="fileName">保存的文件名称</param>
    /// <param name="open">是否打开</param>
    public static void ExportToExcel(this GridControl grd, string fileName = "", bool open = true)
    {
        string filePath = FileDialogHelper.SaveExcel(fileName);
        if (!string.IsNullOrEmpty(filePath))
        {
            for (int i = 0; i < grd.ViewCollection.Count; i++)
            {
                if (grd.ViewCollection[i] != null)
                {
                    GridView grv = grd.ViewCollection[i] as GridView;
                    SetOptionPrint(grv, false);
                }
            }
            grd.ExportToXls(filePath);
            ShowOpenFileDialog(open, filePath);
        }
    }

    /// <summary>
    /// 设置打印选项
    /// </summary>
    /// <param name="grv">GridView对象</param>
    /// <param name="printSelectedRowsOnly">是否只打印选中行，默认为false</param>
    private static void SetOptionPrint(this GridView grv, bool printSelectedRowsOnly = false)
    {
        grv.OptionsPrint.AutoWidth = false;
        grv.OptionsPrint.ExpandAllDetails = true;
        grv.OptionsPrint.ExpandAllGroups = true;
        grv.OptionsPrint.PrintDetails = true;
        grv.OptionsPrint.PrintHorzLines = true;
        grv.OptionsPrint.PrintVertLines = true;
        grv.OptionsPrint.EnableAppearanceEvenRow = true;
        grv.OptionsPrint.EnableAppearanceOddRow = true;
        grv.OptionsPrint.PrintSelectedRowsOnly = printSelectedRowsOnly;
    }

    private static void ShowOpenFileDialog(bool open, string fileName)
    {
        if (open)
        {
            bool result = "是否打开文件?".ShowYesNoAndUxTips() == DialogResult.Yes;
            if (result)
            {
                using (Process process = new Process())
                {
                    process.StartInfo.FileName = fileName;
                    process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                    process.Start();
                }
            }
        }
    }

    #endregion

    #region GridView 数据获取扩展

    /// <summary>
    /// 根据行，列索引来获取RepositoryItem
    /// </summary>
    /// <param name="view">GridView</param>
    /// <param name="rowIndex">行索引</param>
    /// <param name="columnIndex">列索引</param>
    /// <returns>RepositoryItem</returns>
    public static RepositoryItem GetRepositoryItem(this GridView view, int rowIndex, int columnIndex)
    {
        GridViewInfo viewInfo = view.GetViewInfo() as GridViewInfo;
        GridDataRowInfo viewRowInfo = viewInfo.RowsInfo.FindRow(rowIndex) as GridDataRowInfo;
        return viewRowInfo.Cells[columnIndex].Editor;
    }

    /// <summary>
    /// 根据行索引和列名来获取RepositoryItem
    /// </summary>
    /// <param name="view">GridView</param>
    /// <param name="rowIndex">行索引</param>
    /// <param name="columnName">列名</param>
    /// <returns>RepositoryItem</returns>
    public static RepositoryItem GetRepositoryItem(this GridView view, int rowIndex, string columnName)
    {
        GridViewInfo viewInfo = view.GetViewInfo() as GridViewInfo;
        GridDataRowInfo viewRowInfo = viewInfo.RowsInfo.FindRow(rowIndex) as GridDataRowInfo;
        RepositoryItem item = null;
        for (var i = 0; i < viewRowInfo.Cells.Count; i++)
        {
            var cell = viewRowInfo.Cells[i];
            if (cell.Column.Name == columnName)
            {
                item = cell.Editor;
            }
        }
        return item;
    }

    /// <summary>
    /// 根据行索引和列名来获取单元格值文本
    /// </summary>
    /// <param name="view">GridView</param>
    /// <param name="rowIndex">行索引</param>
    /// <param name="columnName">列名</param>
    /// <returns>ValueText</returns>
    public static string GetRowCellValueText(this GridView view, int rowIndex, string columnName)
    {
        object value = view.GetRowCellValue(rowIndex, columnName);
        string displayText = view.GetRowCellDisplayText(rowIndex, columnName);
        string editValue = string.Empty;
        if (displayText is not "无" or "请选择")
        {
            editValue = value switch
            {
                long l => l.ToString(),
                CListItem item => item.Value,
                bool b => b ? "1" : "0",
                _ => value.ObjToStr()
            };
        }
        return editValue;
    }

    /// <summary>
    /// 从GridView里面获取可见列并转换为DataTable对象
    /// </summary>
    /// <param name="gridView">GridView对象</param>
    /// <returns></returns>
    public static DataTable GetDataTableFromGridView(this GridView gridView)
    {
        DataTable dataTable = new DataTable();
        for (int c = 0; c < gridView.Columns.Count; c++)
        {
            if (gridView.Columns[c].Visible)
            {
                dataTable.Columns.Add(gridView.Columns[c].FieldName);
            }
        }

        for (int r = 0; r < gridView.RowCount; r++)
        {
            DataRow drNew = dataTable.NewRow();
            for (int c2 = 0; c2 < dataTable.Columns.Count; c2++)
            {
                drNew[dataTable.Columns[c2].ColumnName] = gridView.GetRowCellDisplayText(r, dataTable.Columns[c2].ColumnName);
            }
            dataTable.Rows.Add(drNew);
        }
        return dataTable;
    }
    
    /// <summary>
    /// 数据行转实体
    /// </summary>
    /// <param name="dr">列视图</param>
    /// <param name="rowHandle">行号</param>
    /// <typeparam name="T">实体类型</typeparam>
    /// <returns></returns>
    public static T RowToModel<T>(this ColumnView dr, int rowHandle) where T : BaseEntity, new()
    {
        T model = new T();
        foreach (PropertyInfo p in (typeof(T)).GetProperties())
        {
            var value = dr.GetRowCellValue(rowHandle, p.Name);
            if (value is DBNull) { continue; }
            p.SetValue(model, value, null);
        }
        return model;
    }
    
    /// <summary>
    /// 实体转数据行
    /// </summary>
    /// <param name="dr">列视图</param>
    /// <param name="rowHandle">行号</param>
    /// <param name="entity">实体对象</param>
    /// <returns></returns>
    public static void EntityToRow(this ColumnView dr, int rowHandle, object entity)
    {
        dr.Columns.ForEach(x =>
        {
            object value =  ReflectionExtension.GetProperty(entity, x.FieldName);
            if (value != null)
            {
                dr.SetRowCellValue(rowHandle, x.FieldName, value);
            }
        });
    }

    #endregion

    #region WinGridViewPager 设置项扩展方法

    /// <summary>
    /// 根据字典增加列的数据源
    /// </summary>
    /// <param name="pager">分页控件</param>
    /// <param name="key">列名</param>
    /// <param name="dict">字典数据</param>
    public static void SetColumnDataSource(this WinGridViewPager pager, string key, Dictionary<string, string> dict)
    {
        List<CListItem> itemList = new List<CListItem>();
        foreach (string dictKey in dict.Keys)
        {
            itemList.Add(new CListItem(dict[dictKey], dictKey));
        }
        pager.AddColumnDataSource(key, itemList);
    }

    /// <summary>
    /// 根据键值列表增加列的数据源
    /// </summary>
    /// <param name="pager">分页控件</param>
    /// <param name="key">列名</param>
    /// <param name="list">字典数据</param>
    public static void SetColumnDataSource(this WinGridViewPager pager, string key, List<CListItem> list)
    {
        pager.AddColumnDataSource(key, list);
    }
        
    /// <summary>
    /// 根据字典类型名称增加列的数据源
    /// </summary>
    /// <param name="pager">分页控件</param>
    /// <param name="key">列名</param>
    /// <param name="dictTypeName">字典名</param>
    public static void SetColumnDataSource(this WinGridViewPager pager, string key, string dictTypeName)
    {
        var dictData = GB.GetDictByName(dictTypeName);
        pager.AddColumnDataSource(key, dictData);
    }

    /// <summary>
    /// 根据字典类型名称增加列的数据源
    /// </summary>
    /// <param name="pager">不分页控件</param>
    /// <param name="key">列名</param>
    /// <param name="dictTypeName">字典名</param>
    public static void SetColumnDataSource(this WinGridView pager, string key, string dictTypeName)
    {
        var dictData = GB.GetDictByName(dictTypeName);
        pager.AddColumnDataSource(key, dictData);
    }

    /// <summary>
    /// 根据键值列表增加列的数据源
    /// </summary>
    /// <param name="pager">分页控件</param>
    /// <param name="key">列名</param>
    /// <param name="list">字典数据</param>
    public static void SetColumnDataSource(this WinGridView pager, string key, List<CListItem> list)
    {
        pager.AddColumnDataSource(key, list);
    }

    #endregion

    #region DataTable与实体转换

    /// <summary>
    /// 编辑后的数据表转实体集合
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="dt"></param>
    public static (List<T>, List<T>, List<T>) GetDataTableData<T>(this DataTable dt)where T:class,new()
    {
        List<T> listAdd = new List<T>();
        List<T> listEdit = new List<T>();
        List<T> listDel = new List<T>();
        foreach (DataRow item in dt.Rows)
        {
            if (item.RowState == DataRowState.Added )
            {
                T t = item.RowToModel<T>();
                if (t != null)
                {
                    listAdd.Add(t);
                }
            }else if (item.RowState == DataRowState.Modified)
            {
                T t = item.RowToModel<T>();
                if (t != null)
                {
                    listEdit.Add(t);
                }
            }
            //listDel = dt.GetChanges(DataRowState.Deleted).DataTableToList<T>();
        }
            
        DataTable dtDeleted = dt.GetChanges(DataRowState.Deleted);
        if (dtDeleted != null)
        {
            foreach (DataRow row in dtDeleted.Rows)
            {
                T t = new T();
                foreach (PropertyInfo fieldInfo in t.GetType().GetProperties())
                {
                    var value = row[fieldInfo.Name, DataRowVersion.Original];
                    fieldInfo.SetValue(t, value is DBNull ? string.Empty : value, null);
                }
                listDel.Add(t);
            }
        }
            
        return (listAdd, listEdit, listDel);
    }

    /// <summary>
    /// 编辑后的数据表转实体集合
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="dt"></param>
    public static EditedList<T> GetEditedList<T>(this DataTable dt)where T:class,new()
    {
        var(listAdd, listEdit, listDel) = GetDataTableData<T>(dt);
        return new EditedList<T>(listAdd, listEdit, listDel);
    }

    /// <summary>
    /// 数据行转实体
    /// </summary>
    /// <param name="dr"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T RowToModel<T>(this DataRow dr) where T : new()
    {
        T model = new T();
        foreach (PropertyInfo p in (typeof(T)).GetProperties())
        {
            if (dr[p.Name] is DBNull) { continue; }
            p.SetValue(model, dr[p.Name.ToUpper()], null);
        }
        return model;
    }
        
    /// <summary>
    /// list转datatable
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="collection"></param>
    /// <returns></returns>
    public static DataTable ToDataTable<T>(this IEnumerable<T> collection)
    {
        var props = typeof(T).GetProperties();
        var dt = new DataTable();
        // Nullable.GetUnderlyingType，避免DataSet 不支持 System.Nullable<>的报错
        dt.Columns.AddRange(props.Select(p => new DataColumn(p.Name, Nullable.GetUnderlyingType(p.PropertyType) ?? p.PropertyType)).ToArray());
        if (collection.Count() > 0)
        {
            for (int i = 0; i < collection.Count(); i++)
            {
                ArrayList tempList = new ArrayList();
                foreach (PropertyInfo pi in props)
                {
                    object obj = pi.GetValue(collection.ElementAt(i), null);
                    tempList.Add(obj);
                }
                object[] array = tempList.ToArray();
                dt.LoadDataRow(array, true);
            }
        }
        return dt;
    }

    /// <summary> 
    /// 利用反射将DataTable转换为List<T>对象
    /// </summary> 
    /// <param name="dt">DataTable 对象</param> 
    /// <returns>List<T>集合</returns> 
    public static List<T> ToList<T>(this DataTable dt) where T : class, new()
    {
        // 定义集合 
        List<T> ts = new List<T>();
        //遍历DataTable中所有的数据行 
        foreach (DataRow dr in dt.Rows)
        {
            T t = new T();
            // 获得此模型的公共属性 
            PropertyInfo[] propertys = t.GetType().GetProperties();
            //遍历该对象的所有属性 
            foreach (PropertyInfo pi in propertys)
            {
                //检查DataTable是否包含此列（列名==对象的属性名）  
                if (pi.CanWrite && dt.Columns.Contains(pi.Name))
                {
                    //取值 
                    object value = dr[pi.Name];
                    //如果非空，则赋给对象的属性 
                    if (value != DBNull.Value)
                    {
                        pi.SetValue(t, value, null);
                    }
                }
            }
            //对象添加到泛型集合中 
            ts.Add(t);
        }
        return ts;
    }

    #endregion
}

/// <summary>
/// GridView的显示类型
/// </summary>
public enum GridType { NewItem, EditOnly, ReadOnly }