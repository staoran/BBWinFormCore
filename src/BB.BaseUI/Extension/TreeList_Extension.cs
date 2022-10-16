using System.Drawing.Drawing2D;
using BB.Tools.MultiLanuage;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;

namespace BB.BaseUI.Extension;

/// <summary>
/// TreeList控件的扩展函数
/// </summary>
public static class TreeListExtension
{
    #region 创建列编辑控件

    public static RepositoryItemLookUpEdit CreateTreeLookUpEdit(this TreeListColumn treeColumn)
    {
        RepositoryItemLookUpEdit repositoryItem = new RepositoryItemLookUpEdit
        {
            AutoHeight = false
        };
        treeColumn.TreeList.RepositoryItems.Add(repositoryItem);
        treeColumn.ColumnEdit = repositoryItem;
        return repositoryItem;
    }

    public static RepositoryItemGridLookUpEdit CreateTreeGridLookUpEdit(this TreeListColumn treeColumn)
    {
        RepositoryItemGridLookUpEdit repositoryItem = new RepositoryItemGridLookUpEdit
        {
            AutoHeight = false
        };
        GridView repositoryItemGridLookUpEditView = new GridView
        {
            FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        };
        repositoryItemGridLookUpEditView.OptionsSelection.EnableAppearanceFocusedCell = false;
        repositoryItemGridLookUpEditView.OptionsView.ShowGroupPanel = false;
        repositoryItem.View = repositoryItemGridLookUpEditView;
        treeColumn.TreeList.RepositoryItems.Add(repositoryItem);
        treeColumn.ColumnEdit = repositoryItem;
        return repositoryItem;
    }

    public static RepositoryItemComboBox CreateTreeComboBox(this TreeListColumn treeColumn)
    {
        RepositoryItemComboBox repositoryItem = new RepositoryItemComboBox
        {
            AutoHeight = false
        };
        treeColumn.TreeList.RepositoryItems.Add(repositoryItem);
        treeColumn.ColumnEdit = repositoryItem;
        return repositoryItem;
    }

    public static RepositoryItemImageComboBox CreateTreeImageComboBox(this TreeListColumn treeColumn)
    {
        RepositoryItemImageComboBox repositoryItem = new RepositoryItemImageComboBox
        {
            AutoHeight = false
        };
        treeColumn.TreeList.RepositoryItems.Add(repositoryItem);
        treeColumn.ColumnEdit = repositoryItem;
        return repositoryItem;
    }

    public static RepositoryItemCheckedComboBoxEdit CreateTreeCheckedComboBoxEdit(this TreeListColumn treeColumn)
    {
        RepositoryItemCheckedComboBoxEdit repositoryItem = new RepositoryItemCheckedComboBoxEdit
        {
            AutoHeight = false
        };
        treeColumn.TreeList.RepositoryItems.Add(repositoryItem);
        treeColumn.ColumnEdit = repositoryItem;
        return repositoryItem;
    }

    public static RepositoryItemPopupContainerEdit CreateTreePopupContainerEdit(this TreeListColumn treeColumn, PopupContainerControl popupContainerControl)
    {
        RepositoryItemPopupContainerEdit repositoryItem = new RepositoryItemPopupContainerEdit
        {
            AutoHeight = false
        };
        treeColumn.TreeList.RepositoryItems.Add(repositoryItem);
        treeColumn.ColumnEdit = repositoryItem;
        repositoryItem.CloseUpKey = new KeyShortcut(Keys.Space);
        repositoryItem.PopupControl = popupContainerControl;
        return repositoryItem;
    }

    public static RepositoryItemPopupContainerEdit CreateTreePopupContainerEdit(this TreeListColumn treeColumn)
    {
        RepositoryItemPopupContainerEdit repositoryItem = new RepositoryItemPopupContainerEdit
        {
            AutoHeight = false
        };
        treeColumn.TreeList.RepositoryItems.Add(repositoryItem);
        treeColumn.ColumnEdit = repositoryItem;
        repositoryItem.CloseUpKey = new KeyShortcut(Keys.Space);
        return repositoryItem;
    }

    public static RepositoryItemTextEdit CreateTreeTextEdit(this TreeListColumn treeColumn)
    {
        RepositoryItemTextEdit repositoryItem = new RepositoryItemTextEdit
        {
            AutoHeight = false
        };
        treeColumn.TreeList.RepositoryItems.Add(repositoryItem);
        treeColumn.ColumnEdit = repositoryItem;
        return repositoryItem;
    }

    public static RepositoryItemSpinEdit CreateTreeSpinEdit(this TreeListColumn treeColumn)
    {
        RepositoryItemSpinEdit repositoryItem = new RepositoryItemSpinEdit
        {
            Increment = decimal.One,
            AutoHeight = false
        };
        treeColumn.TreeList.RepositoryItems.Add(repositoryItem);
        treeColumn.ColumnEdit = repositoryItem;
        return repositoryItem;
    }

    public static RepositoryItemCheckEdit CreateTreeCheckEdit(this TreeListColumn treeColumn)
    {
        RepositoryItemCheckEdit repositoryItem = new RepositoryItemCheckEdit
        {
            AutoHeight = false,
            ValueChecked = 1,
            ValueUnchecked = 0
        };
        treeColumn.TreeList.RepositoryItems.Add(repositoryItem);
        treeColumn.ColumnEdit = repositoryItem;
        return repositoryItem;
    }

    public static RepositoryItemDateEdit CreateTreeDateEdit(this TreeListColumn treeColumn)
    {
        RepositoryItemDateEdit repositoryItem = new RepositoryItemDateEdit
        {
            AutoHeight = false
        };
        treeColumn.TreeList.RepositoryItems.Add(repositoryItem);
        treeColumn.ColumnEdit = repositoryItem;
        return repositoryItem;
    }

    public static RepositoryItemTimeEdit CreateTreeTimeEdit(this TreeListColumn treeColumn)
    {
        RepositoryItemTimeEdit repositoryItem = new RepositoryItemTimeEdit
        {
            AutoHeight = false,
            EditMask = "yyyy-MM-dd HH:mm:ss"
        };
        treeColumn.TreeList.RepositoryItems.Add(repositoryItem);
        treeColumn.ColumnEdit = repositoryItem;
        return repositoryItem;
    }

    public static RepositoryItemMemoEdit CreateTreeMemoEdit(this TreeListColumn treeColumn)
    {
        RepositoryItemMemoEdit repositoryItem = new RepositoryItemMemoEdit
        {
            AutoHeight = false,
            LinesCount = 0
        };
        treeColumn.TreeList.RepositoryItems.Add(repositoryItem);
        treeColumn.ColumnEdit = repositoryItem;
        return repositoryItem;
    }

    public static RepositoryItemMemoExEdit CreateTreeMemoExEdit(this TreeListColumn treeColumn)
    {
        RepositoryItemMemoExEdit repositoryItem = new RepositoryItemMemoExEdit
        {
            AutoHeight = false,
            ShowIcon = false,
            PopupFormSize = new Size(400, 200)
        };
        treeColumn.TreeList.RepositoryItems.Add(repositoryItem);
        treeColumn.ColumnEdit = repositoryItem;
        return repositoryItem;
    }

    public static RepositoryItemButtonEdit CreateTreeButtonEdit(this TreeListColumn treeColumn)
    {
        RepositoryItemButtonEdit repositoryItem = new RepositoryItemButtonEdit
        {
            AutoHeight = false
        };
        treeColumn.TreeList.RepositoryItems.Add(repositoryItem);
        treeColumn.ColumnEdit = repositoryItem;
        return repositoryItem;
    }

    public static RepositoryItemMRUEdit CreateTreeMruEdit(this TreeListColumn treeColumn)
    {
        RepositoryItemMRUEdit repositoryItem = new RepositoryItemMRUEdit
        {
            AutoHeight = false
        };
        treeColumn.TreeList.RepositoryItems.Add(repositoryItem);
        treeColumn.ColumnEdit = repositoryItem;
        return repositoryItem;
    }

    public static RepositoryItemPictureEdit CreateTreePictureEdit(this TreeListColumn treeColumn)
    {
        RepositoryItemPictureEdit repositoryItem = new RepositoryItemPictureEdit
        {
            SizeMode = PictureSizeMode.Zoom,
            PictureInterpolationMode = InterpolationMode.High,
            NullText = " "
        };
        treeColumn.TreeList.RepositoryItems.Add(repositoryItem);
        treeColumn.ColumnEdit = repositoryItem;
        return repositoryItem;
    }

    public static RepositoryItemRadioGroup CreateTreeRadioGroup(this TreeListColumn treeColumn)
    {
        RepositoryItemRadioGroup repositoryItem = new RepositoryItemRadioGroup();
        treeColumn.TreeList.RepositoryItems.Add(repositoryItem);
        treeColumn.ColumnEdit = repositoryItem;
        return repositoryItem;
    }

    public static RepositoryItemHyperLinkEdit CreateTreeHyperLinkEdit(this TreeListColumn treeColumn)
    {
        RepositoryItemHyperLinkEdit repositoryItem = new RepositoryItemHyperLinkEdit
        {
            AutoHeight = false
        };
        treeColumn.TreeList.RepositoryItems.Add(repositoryItem);
        treeColumn.ColumnEdit = repositoryItem;
        return repositoryItem;
    }

    public static RepositoryItemImageEdit CreateTreeImageEdit(this TreeListColumn treeColumn)
    {
        RepositoryItemImageEdit repositoryItem = new RepositoryItemImageEdit
        {
            AutoHeight = false,
            SizeMode = PictureSizeMode.Zoom
        };
        treeColumn.TreeList.RepositoryItems.Add(repositoryItem);
        treeColumn.ColumnEdit = repositoryItem;
        return repositoryItem;
    }

    public static RepositoryItemCalcEdit CreateTreeCalcEdit(this TreeListColumn treeColumn)
    {
        RepositoryItemCalcEdit repositoryItem = new RepositoryItemCalcEdit
        {
            AutoHeight = false
        };
        treeColumn.TreeList.RepositoryItems.Add(repositoryItem);
        treeColumn.ColumnEdit = repositoryItem;
        return repositoryItem;
    }

    public static RepositoryItemColorEdit CreateTreeColorEdit(this TreeListColumn treeColumn)
    {
        RepositoryItemColorEdit repositoryItem = new RepositoryItemColorEdit
        {
            AutoHeight = false
        };
        treeColumn.TreeList.RepositoryItems.Add(repositoryItem);
        treeColumn.ColumnEdit = repositoryItem;
        return repositoryItem;
    }

    public static RepositoryItemFontEdit CreateTreeFontEdit(this TreeListColumn treeColumn)
    {
        RepositoryItemFontEdit repositoryItem = new RepositoryItemFontEdit
        {
            AutoHeight = false
        };
        treeColumn.TreeList.RepositoryItems.Add(repositoryItem);
        treeColumn.ColumnEdit = repositoryItem;
        return repositoryItem;
    }

    #endregion

    /// <summary>
    /// 初始化TreeList对象
    /// </summary>
    public static void InitTree(this TreeList treeList, string keyFieldName, string parentFieldName, string rootValue = "0",
        bool editable = true, bool showColumnHeader = false, bool oddEvenRowColor = true, bool allowDrop = false)
    {
        treeList.KeyFieldName = keyFieldName;
        treeList.ParentFieldName = parentFieldName;
        treeList.OptionsBehavior.Editable = editable;
        treeList.RootValue = editable;
        treeList.AllowDrop = allowDrop;
        treeList.OptionsView.ShowColumns = showColumnHeader;
        treeList.OptionsView.AutoWidth = true;
        treeList.OptionsNavigation.EnterMovesNextColumn = true;
        treeList.OptionsSelection.MultiSelect = true;

        if (oddEvenRowColor)
        {
            treeList.OptionsView.EnableAppearanceOddRow = true;
            treeList.OptionsView.EnableAppearanceEvenRow = true;
            treeList.Appearance.OddRow.BackColor = Color.Transparent;
            treeList.Appearance.OddRow.BorderColor = Color.FromArgb(199, 209, 228);
            treeList.Appearance.EvenRow.BackColor = Color.FromArgb(239, 243, 250);
            treeList.Appearance.EvenRow.BorderColor = Color.FromArgb(199, 209, 228);
        }
    }

    /// <summary>
    /// 创建TreeList列
    /// </summary>
    public static TreeListColumn CreateColumn(this TreeList tree, string fieldName, string caption, int width = 80, bool allowEdit = true)
    {
        //多语言支持
        caption = JsonLanguage.Default.GetString(caption);

        TreeListColumn treeListColumn = new TreeListColumn
        {
            FieldName = fieldName,
            Caption = caption,
            Width = width
        };
        tree.Columns.AddRange(new[]
        {
            treeListColumn
        });

        treeListColumn.AbsoluteIndex = tree.Columns.Count;
        treeListColumn.VisibleIndex = tree.Columns.Count;
        treeListColumn.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
        treeListColumn.AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;
        treeListColumn.OptionsColumn.AllowEdit = allowEdit;

        if (!allowEdit)
        {
            treeListColumn.AppearanceHeader.ForeColor = Color.Gray;
        }

        bool isTime = caption.Contains("时间");
        if (isTime)
        {
            treeListColumn.Format.FormatType = FormatType.DateTime;
            treeListColumn.Format.FormatString = "yyyy-MM-dd HH:mm:ss";
        }
        else
        {
            bool isDate = caption.Contains("日期");
            if (isDate)
            {
                treeListColumn.Format.FormatType = FormatType.DateTime;
                treeListColumn.Format.FormatString = "yyyy-MM-dd";
            }
        }
        return treeListColumn;
    }

    /// <summary>
    /// 注册TreeList选择事件
    /// </summary>
    public static void RegisterTreeCheckEditClickEvent(TreeList treeList, RepositoryItemCheckEdit checkCtrl, BarButtonItem btnSave = null)
    {
        checkCtrl.Click += delegate(object? sender, EventArgs e)
        {
            TreeListNode focusedNode = treeList.FocusedNode;
            int nodeValue = ConvertToInt(focusedNode.GetValue("CHECKFLAG"));
            if (nodeValue == 1)
            {
                focusedNode.SetValue("CHECKFLAG", 0);
                SetParentChildValue(focusedNode, 0);
            }
            else
            {
                focusedNode.SetValue("CHECKFLAG", 1);
                SetParentChildValue(focusedNode, 1);
            }

            if (btnSave != null)
            {
                btnSave.Enabled = true;
            }
        };
    }

    private static void SetParentChildValue(TreeListNode node, int checkValue)
    {
        SetChildValue(node, checkValue);
        SetParentValue(node, checkValue);
    }

    private static void SetChildValue(TreeListNode node, int checkValue)
    {
        for (int i = 0; i < node.Nodes.Count; i++)
        {
            int nodeValue = ConvertToInt(node.Nodes[i].GetValue("CHECKFLAG"));
            if (!checkValue.Equals(nodeValue))
            {
                node.Nodes[i].SetValue("CHECKFLAG", checkValue);
            }
            SetChildValue(node.Nodes[i], checkValue);
        }
    }

    private static void SetParentValue(TreeListNode node, int checkValue)
    {
        if (node.ParentNode != null)
        {
            if (checkValue == 0)
            {
                for (int i = 0; i < node.ParentNode.Nodes.Count; i++)
                {
                    bool flag3 = ConvertToInt(node.ParentNode.Nodes[i].GetValue("CHECKFLAG")) == 1;
                    if (flag3)
                    {
                        checkValue = 1;
                        break;
                    }
                }
            }

            bool flag4 = !checkValue.Equals(ConvertToInt(node.ParentNode.GetValue("CHECKFLAG")));
            if (flag4)
            {
                node.ParentNode.SetValue("CHECKFLAG", checkValue);
            }
            SetParentValue(node.ParentNode, checkValue);
        }
    }

    private static int ConvertToInt(object o)
    {
        int result;
        try
        {
            result = Convert.ToInt32(o);
        }
        catch
        {
            result = 0;
        }
        return result;
    }


}