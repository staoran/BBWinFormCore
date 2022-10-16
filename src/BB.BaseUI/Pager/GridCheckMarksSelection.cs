using System.Collections;
using DevExpress.Utils.Drawing;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace BB.BaseUI.Pager;

/// <summary>
/// GridView选择变化的委托定义
/// </summary>
public delegate void SelectionChangedEventHandler(object? sender, EventArgs e);

/// <summary>
/// GridView复选框选择的辅助类
/// </summary>
public class GridCheckMarksSelection
{
    protected GridView _view;
    protected ArrayList selection;
    GridColumn _column;
    RepositoryItemCheckEdit _edit;
    const int CheckboxIndent = 4;
    GridLookUpEdit _gridLookUpEdit;

    /// <summary>
    /// 以GridView为参数的构造函数
    /// </summary>
    /// <param name="view">GridView</param>
    public GridCheckMarksSelection(GridView view) : this()
    {
        View = view;
    }

    /// <summary>
    /// 以GridLookUpEdit为参数的构造函数
    /// </summary>
    /// <param name="control"></param>
    public GridCheckMarksSelection(GridLookUpEdit control) : this()
    {
        View = control.Properties.View;
        _gridLookUpEdit = control;
    }

    /// <summary>
    /// 表格视图对象
    /// </summary>
    public GridView View
    {
        get => _view;
        set
        {
            if (_view != value)
            {
                Detach();
                Attach(value);
            }
        }
    }

    public GridColumn CheckMarkColumn => _column;

    public GridLookUpEdit GridCheckLookUpEdit => _gridLookUpEdit;

    public GridCheckMarksSelection()
    {
        selection = new ArrayList();
        OnSelectionChanged();
    }

    public ArrayList Selection
    {
        get => selection;
        set => selection = value;
    }

    public int SelectedCount => selection.Count;

    public object GetSelectedRow(int index)
    { 
        return selection[index];
    }

    public int GetSelectedIndex(object row)
    { 
        return selection.IndexOf(row);
    }

    public void ClearSelection()
    {
        selection.Clear();
        Invalidate();
        OnSelectionChanged();
    }

    public void SelectAll()
    { 
        SelectAll(null); 
    }

    public void SelectAll(object dataSource)
    {
        selection.Clear();
        if (dataSource != null)
        {
            if (dataSource is ICollection)
                selection.AddRange(((ICollection)dataSource));
        }
        else
        {
            for (int i = 0; i < _view.DataRowCount; i++)
                selection.Add(_view.GetRow(i));
        }
        Invalidate();
        OnSelectionChanged();
    }

    /// <summary>
    /// 处理选择变化的事件
    /// </summary>
    public event SelectionChangedEventHandler SelectionChanged;

    /// <summary>
    /// 选择变化事件的处理
    /// </summary>
    public void OnSelectionChanged()
    {
        if (SelectionChanged != null)
        {
            EventArgs e = EventArgs.Empty;
            SelectionChanged(this, e);
        }
    }

    public void SelectGroup(int rowHandle, bool select)
    {
        if (IsGroupRowSelected(rowHandle) && select) return;
        for (int i = 0; i < _view.GetChildRowCount(rowHandle); i++)
        {
            int childRowHandle = _view.GetChildRowHandle(rowHandle, i);
            if (_view.IsGroupRow(childRowHandle))
                SelectGroup(childRowHandle, select);
            else
                SelectRow(childRowHandle, select, false);
        }
        Invalidate();
    }

    public void SelectRow(int rowHandle, bool select)
    { SelectRow(rowHandle, select, true); }

    public void InvertRowSelection(int rowHandle)
    {
        if (View.IsDataRow(rowHandle))
            SelectRow(rowHandle, !IsRowSelected(rowHandle));
        if (View.IsGroupRow(rowHandle))
            SelectGroup(rowHandle, !IsGroupRowSelected(rowHandle));
    }
    public bool IsGroupRowSelected(int rowHandle)
    {
        for (int i = 0; i < _view.GetChildRowCount(rowHandle); i++)
        {
            int row = _view.GetChildRowHandle(rowHandle, i);
            if (_view.IsGroupRow(row))
            {
                if (!IsGroupRowSelected(row)) return false;
            }
            else
            if (!IsRowSelected(row)) return false;
        }
        return true;
    }

    /// <summary>
    /// 检查指定的行是否选中
    /// </summary>
    /// <param name="rowHandle"></param>
    /// <returns></returns>
    public bool IsRowSelected(int rowHandle)
    {
        if (_view.IsGroupRow(rowHandle))
            return IsGroupRowSelected(rowHandle);

        object row = _view.GetRow(rowHandle);
        return GetSelectedIndex(row) != -1;
    }

    protected virtual void Attach(GridView view)
    {
        if (view == null) return;
        selection.Clear();
        _view = view;
        view.BeginUpdate();
        try
        {
            _edit = view.GridControl.RepositoryItems.Add("CheckEdit") as RepositoryItemCheckEdit;

            _column = view.Columns.Add();
            _column.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            _column.Visible = true;
            _column.VisibleIndex = 0;
            _column.FieldName = "CheckMarkSelection";
            _column.Caption = "Mark";
            _column.OptionsColumn.ShowCaption = false;
            _column.OptionsColumn.AllowEdit = false;
            _column.OptionsColumn.AllowSize = false;
            _column.UnboundType = DevExpress.Data.UnboundColumnType.Boolean;
            _column.Width = GetCheckBoxWidth();
            _column.ColumnEdit = _edit;

            view.Click += View_Click;
            view.CustomDrawColumnHeader += View_CustomDrawColumnHeader;
            view.CustomDrawGroupRow += View_CustomDrawGroupRow;
            view.CustomUnboundColumnData += view_CustomUnboundColumnData;
            view.KeyDown += view_KeyDown;
        }
        finally
        {
            view.EndUpdate();
        }
    }
    protected virtual void Detach()
    {
        if (_view == null) return;
        if (_column != null)
            _column.Dispose();
        if (_edit != null)
        {
            _view.GridControl.RepositoryItems.Remove(_edit);
            _edit.Dispose();
        }
        _view.Click -= View_Click;
        _view.CustomDrawColumnHeader -= View_CustomDrawColumnHeader;
        _view.CustomDrawGroupRow -= View_CustomDrawGroupRow;
        _view.CustomUnboundColumnData -= view_CustomUnboundColumnData;
        _view.KeyDown -= view_KeyDown;
        _view = null;
    }
    protected int GetCheckBoxWidth()
    {
        DevExpress.XtraEditors.ViewInfo.CheckEditViewInfo info = _edit.CreateViewInfo() as DevExpress.XtraEditors.ViewInfo.CheckEditViewInfo;
        int width = 0;
        GraphicsInfo.Default.AddGraphics(null);
        try
        {
            width = info.CalcBestFit(GraphicsInfo.Default.Graphics).Width;
        }
        finally
        {
            GraphicsInfo.Default.ReleaseGraphics();
        }
        return width + CheckboxIndent * 2;
    }
    protected void DrawCheckBox(Graphics g, Rectangle r, bool @checked)
    {
        DevExpress.XtraEditors.ViewInfo.CheckEditViewInfo info;
        DevExpress.XtraEditors.Drawing.CheckEditPainter painter;
        DevExpress.XtraEditors.Drawing.ControlGraphicsInfoArgs args;
        info = _edit.CreateViewInfo() as DevExpress.XtraEditors.ViewInfo.CheckEditViewInfo;
        painter = _edit.CreatePainter() as DevExpress.XtraEditors.Drawing.CheckEditPainter;
        info.EditValue = @checked;
        info.Bounds = r;
        info.CalcViewInfo(g);
        args = new DevExpress.XtraEditors.Drawing.ControlGraphicsInfoArgs(info, new GraphicsCache(g), r);
        painter.Draw(args);
        args.Cache.Dispose();
    }
    void Invalidate()
    {
        _view.BeginUpdate();
        _view.EndUpdate();
    }
    void SelectRow(int rowHandle, bool select, bool invalidate)
    {
        if (IsRowSelected(rowHandle) == select) return;
        object row = _view.GetRow(rowHandle);
        if (select)
            selection.Add(row);
        else
            selection.Remove(row);
        if (invalidate)
            Invalidate();
        OnSelectionChanged();
    }
    void view_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
    {
        if (e.Column != CheckMarkColumn) return;
        if (e.IsGetData)
            e.Value = IsRowSelected(View.GetRowHandle(e.ListSourceRowIndex));
        else
            SelectRow(View.GetRowHandle(e.ListSourceRowIndex), (bool)e.Value);
    }
    void view_KeyDown(object? sender, KeyEventArgs e)
    {
        if (View.FocusedColumn != _column || e.KeyCode != Keys.Space) return;
        InvertRowSelection(View.FocusedRowHandle);
    }
    void View_Click(object? sender, EventArgs e)
    {
        GridHitInfo info;
        Point pt = _view.GridControl.PointToClient(System.Windows.Forms.Control.MousePosition);
        info = _view.CalcHitInfo(pt);
        if (info.Column == _column)
        {
            if (info.InColumn)
            {
                if (SelectedCount == _view.DataRowCount)
                    ClearSelection();
                else
                    SelectAll();
            }
            if (info.InRowCell)
                InvertRowSelection(info.RowHandle);
        }
        if (info.InRow && _view.IsGroupRow(info.RowHandle) && info.HitTest != GridHitTest.RowGroupButton)
            InvertRowSelection(info.RowHandle);
    }
    void View_CustomDrawColumnHeader(object? sender, ColumnHeaderCustomDrawEventArgs e)
    {
        if (e.Column == _column)
        {
            e.Info.InnerElements.Clear();
            e.Painter.DrawObject(e.Info);
            DrawCheckBox(e.Graphics, e.Bounds, SelectedCount == _view.DataRowCount);
            e.Handled = true;
        }
    }
    void View_CustomDrawGroupRow(object sender, RowObjectCustomDrawEventArgs e)
    {
        GridGroupRowInfo info;
        info = e.Info as GridGroupRowInfo;

        info.GroupText = "         " + info.GroupText.TrimStart();
        e.Info.Paint.FillRectangle(e.Graphics, e.Appearance.GetBackBrush(e.Cache), e.Bounds);
        e.Painter.DrawObject(e.Info);

        Rectangle r = info.ButtonBounds;
        r.Offset(r.Width + CheckboxIndent * 2 - 1, 0);
        DrawCheckBox(e.Graphics, r, IsGroupRowSelected(e.RowHandle));
        e.Handled = true;
    }
}