using System.Collections;

namespace BB.BaseUI.Control;

public class SortableListView : ListView
{
    int _sortOrder;
    int _sortColumn;
    Bitmap _imageAscending;
    Bitmap _imageDescending;

    public SortableListView()
    {
        _sortColumn = 0;
        _sortOrder = 1;
        _imageAscending = new Bitmap(Resources.up);
        _imageAscending.MakeTransparent(Color.Magenta);
        _imageDescending = new Bitmap(Resources.down);
        _imageDescending.MakeTransparent(Color.Magenta);
        BorderStyle = BorderStyle.None;
        Dock = DockStyle.Fill;
        FullRowSelect = true;
        HideSelection = false;
        LabelEdit = true;
        LabelWrap = false;
        View = View.Details;
        Sorting = SortOrder.None;
        AllowColumnReorder = true;
        OwnerDraw = true;
        DrawColumnHeader += SortableListView_DrawColumnHeader;
        DrawItem += SortableListView_DrawItem;
        DrawSubItem += SortableListView_DrawSubItem;
        ColumnClick += SortableListView_ColumnClick;
        ColumnReordered += SortableListView_ColumnReordered;
    }

    void SortableListView_DrawSubItem(object? sender, DrawListViewSubItemEventArgs e)
    {
        e.DrawDefault = true;
    }

    void SortableListView_DrawItem(object? sender, DrawListViewItemEventArgs e)
    {
        e.DrawDefault = true;
    }

    void SortableListView_DrawColumnHeader(object? sender, DrawListViewColumnHeaderEventArgs e)
    {
        bool fSorted = (_sortColumn == e.ColumnIndex);

        if (fSorted)
        {
            e.DrawBackground();
            e.DrawText(TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);

            if (fSorted && _clickColumn)
            {
                Point ptImage = new Point(e.Bounds.Left + (int)e.Graphics.MeasureString(e.Header.Text + "XY", e.Font).Width, (e.Bounds.Top + e.Bounds.Bottom - _imageAscending.Height) / 2);
                e.Graphics.DrawImage((_sortOrder > 0) ? _imageAscending : _imageDescending, ptImage);
                //this.Refresh();

                _clickColumn = !_clickColumn;
            }
                
        }
        else
        {
            e.DrawDefault = true;
        }            
    }

    public void AddColumns(params string[] columns)
    {
        Columns.Clear();

        foreach (string columnName in columns)
        {
            Columns.Add(columnName, 120);
        }

        ListViewItemSorter = new ListViewItemComparer(_sortColumn, _sortOrder);
    }

    void SortableListView_ColumnReordered(object? sender, ColumnReorderedEventArgs e)
    {
        if (e.OldDisplayIndex == _sortColumn)
        {
            _sortColumn = e.NewDisplayIndex;
            ListViewItemSorter = new ListViewItemComparer(_sortColumn, _sortOrder);
        }
    }

    bool _clickColumn = false;
    void SortableListView_ColumnClick(object? sender, ColumnClickEventArgs e)
    {
        if (_sortColumn != e.Column)
        {
            _sortColumn = e.Column;
            _sortOrder = 1;
        }
        else
        {
            _sortOrder *= -1;
        }

        _clickColumn = true;

        // Sort by the column
        ListViewItemSorter = new ListViewItemComparer(_sortColumn, _sortOrder);
    }

    class ListViewItemComparer : IComparer
    {
        int _col;
        int _order;

        public ListViewItemComparer(int column, int directiron)
        {
            _order = directiron;
            _col = column;
        }

        public int Compare(object x, object y)
        {
            return Math.Sign(_order) * String.Compare(((ListViewItem)x).SubItems[_col].Text, ((ListViewItem)y).SubItems[_col].Text);
        }
    }
}