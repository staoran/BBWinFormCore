using BB.Tools.MultiLanuage;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using DevExpress.XtraNavBar;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;

namespace BB.BaseUI.Other
{
    /// <summary>
    /// 对界面控件进行多语言的处理辅助类
    /// </summary>
    public class LanguageHelper
    {             
        /// <summary>
        /// 初始化语言
        /// </summary>
        public static void InitLanguage(System.Windows.Forms.Control control)
        {
            //如果没有资源，那么不必遍历控件，提高速度
            if (!JsonLanguage.Default.HasResource)
                return;

            //使用递归的方式对控件及其子控件进行处理
            SetControlLanguage(control);
            foreach (System.Windows.Forms.Control ctrl in control.Controls)
            {
                InitLanguage(ctrl);
            }

            //工具栏或者菜单动态构建窗体或者控件的时候，重新对子控件进行处理
            control.ControlAdded += (sender, e) =>
            {
                InitLanguage(e.Control);
            };
        }

        /// <summary>
        /// 对不同的控件类型，转换为对应的对象进行处理
        /// </summary>
        /// <param name="control"></param>
        private static void SetControlLanguage(System.Windows.Forms.Control control)
        {
            control.Text = JsonLanguage.Default.GetString(control.Text);

            //设置状态提示信息
            var baseControl = control as BaseControl;
            if (baseControl != null)
            {
                //对基础控件，设置它的提示信息和标题
                if (!string.IsNullOrEmpty(baseControl.ToolTip))
                {
                    baseControl.ToolTip = JsonLanguage.Default.GetString(baseControl.ToolTip);
                }
                if (!string.IsNullOrEmpty(baseControl.ToolTipTitle))
                {
                    baseControl.ToolTipTitle = JsonLanguage.Default.GetString(baseControl.ToolTipTitle);
                }
            }

            
            //对不同的控件进行不同的处理
            if (control is LayoutControl layout)
            {
                //对布局控件，设置布局项的文本
                foreach (BaseLayoutItem item in layout.Items)
                {
                    item.Text = JsonLanguage.Default.GetString(item.Text);
                }
            }
            else if (control is GridControl grid)
            {
                //对GridControl,设置列的表头以及右键菜单
                foreach (GridView view in grid.Views)
                {
                    foreach (GridColumn col in view.Columns)
                    {
                        col.Caption = JsonLanguage.Default.GetString(col.Caption);
                    }
                }
                if(grid.ContextMenuStrip != null)
                {
                    SetMenuText(grid.ContextMenuStrip);
                }
            }
            else if(control is NavBarControl bar)
            {
                //对NavBarControl，设置项目的标题和分组的标题
                foreach(NavBarItem item in bar.Items)
                {
                    item.Caption = JsonLanguage.Default.GetString(item.Caption);
                }
                foreach (NavBarGroup group in bar.Groups)
                {
                    group.Caption = JsonLanguage.Default.GetString(group.Caption);
                }
            }
            else if (control is RibbonControl ribbon)
            {
                //对RibbonControl，设置RibbonPage、RibbonPageGroup、BarItem几级的显示内容
                foreach(BarButtonItem rbitem in ribbon.Items)
                {
                    rbitem.Caption = JsonLanguage.Default.GetString(rbitem.Caption);
                    rbitem.Description = JsonLanguage.Default.GetString(rbitem.Description);
                    rbitem.Hint = JsonLanguage.Default.GetString(rbitem.Hint);
                }

                foreach (RibbonPage page in ribbon.Pages)
                {
                    page.Text = JsonLanguage.Default.GetString(page.Text);
                    foreach(RibbonPageGroup group in page.Groups)
                    {
                        group.Text = JsonLanguage.Default.GetString(group.Text);
                        foreach (BarButtonItemLink item in group.ItemLinks)
                        {
                            item.Caption = JsonLanguage.Default.GetString(item.Caption);
                        }
                    }
                }
            }
            else if(control is TreeList list)
            {
                //对TreeList控件，设置列表列表头和右键菜单
                foreach (TreeListColumn column in list.Columns)
                {
                    column.Caption = JsonLanguage.Default.GetString(column.Caption);
                }

                if (list.ContextMenuStrip != null)
                {
                    SetMenuText(list.ContextMenuStrip);
                }
            }
            else if (control is ListView listView)
            {
                //对列表控件，设置表头和右键菜单
                foreach (ColumnHeader column in listView.Columns)
                {
                    column.Text = JsonLanguage.Default.GetString(column.Text);
                }

                if (listView.ContextMenuStrip != null)
                {
                    SetMenuText(listView.ContextMenuStrip);
                }
            }
            else if(control is ToolStrip tool)
            {
                //对工具条，设置项目的标题和下拉项目的标题
                foreach(ToolStripItem item in tool.Items)
                {
                    item.Text = JsonLanguage.Default.GetString(item.Text);

                    //针对下拉列表的处理
                    if (item is ToolStripSplitButton splitButton)
                    {
                        foreach (ToolStripItem dropItem in splitButton.DropDownItems)
                        {
                            dropItem.Text = JsonLanguage.Default.GetString(dropItem.Text);
                        }
                    }
                }
            }
            else if(control is BarDockControl dock)
            {
                //对DevExpress的bar控件，设置项目的标题
                if(dock.Manager != null)
                {
                    foreach(BarItem item in dock.Manager.Items)
                    {
                        item.Caption = JsonLanguage.Default.GetString(item.Caption);
                    }
                }
            }
            else if (control is TreeView view)
            {
                //对传统树列表，设置节点的文本和菜单
                foreach (TreeNode node in view.Nodes)
                {
                    SetNodeText(node);
                }

                if (view.ContextMenuStrip != null)
                {
                    SetMenuText(view.ContextMenuStrip);
                }
            }
            else if (control is TreeListLookUpEdit treeview)
            {
                //对TreeList控件，设置列表列表头和右键菜单
                if (treeview.Properties.TreeList is { Columns: { } })
                {
                    foreach (TreeListColumn column in treeview.Properties.TreeList.Columns)
                    {
                        column.Caption = JsonLanguage.Default.GetString(column.Caption);
                    }
                }

                //提示信息
                treeview.Properties.NullValuePrompt = JsonLanguage.Default.GetString(treeview.Properties.NullValuePrompt);

                if (treeview.ContextMenuStrip != null)
                {
                    SetMenuText(treeview.ContextMenuStrip);
                }
            }
            else if (control is TextEdit baseEdit)
            {
                //显示占位符
                baseEdit.Properties.NullText = JsonLanguage.Default.GetString(baseEdit.Properties.NullText);
                baseEdit.Properties.NullValuePrompt = JsonLanguage.Default.GetString(baseEdit.Properties.NullValuePrompt);
            }
        }

        /// <summary>
        /// 在需要时候（一般为DataSourceChanged函数中）动态设置列的多语言信息
        /// </summary>
        /// <param name="gridView"></param>
        public static void SetGridViewColumns(GridView gridView)
        {
            foreach (GridColumn col in gridView.Columns)
            {
                col.Caption = JsonLanguage.Default.GetString(col.Caption);
            }
        }

        /// <summary>
        /// 设置菜单项的显示文本
        /// </summary>
        private static void SetMenuText(ContextMenuStrip menuStrip)
        {
            if (menuStrip != null)
            {
                foreach (ToolStripItem item in menuStrip.Items)
                {
                    item.Text = JsonLanguage.Default.GetString(item.Text);
                }
            }
        }

        /// <summary>
        /// 设置树形列表的Node的文本（可以不用，由数据展示）
        /// </summary>
        /// <param name="node"></param>
        private static void SetNodeText(TreeNode node)
        {
            node.Text = JsonLanguage.Default.GetString(node.Text);
            foreach (TreeNode subNode in node.Nodes)
            {
                SetNodeText(subNode);
            }
        }

    }
}
