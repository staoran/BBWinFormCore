using System.ComponentModel;
using System.Reflection;

namespace BB.Tools.Collections;

public class SortableBindingList<T> : BindingList<T>
{
    private bool _isSortedCore = true;
    private ListSortDirection _sortDirectionCore = ListSortDirection.Ascending;
    private PropertyDescriptor _sortPropertyCore = null;
    private string _defaultSortItem;

    public SortableBindingList() : base() { }

    public SortableBindingList(IList<T> list) : base(list) { }

    protected override bool SupportsSortingCore => true;

    protected override bool SupportsSearchingCore => true;

    protected override bool IsSortedCore => _isSortedCore;

    protected override ListSortDirection SortDirectionCore => _sortDirectionCore;

    protected override PropertyDescriptor SortPropertyCore => _sortPropertyCore;

    protected override int FindCore(PropertyDescriptor prop, object key)
    {
        for (int i = 0; i < Count; i++)
        {
            if (Equals(prop.GetValue(this[i]), key)) return i;
        }
        return -1;
    }

    protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
    {
        _isSortedCore = true;
        _sortPropertyCore = prop;
        _sortDirectionCore = direction;
        Sort();
    }

    protected override void RemoveSortCore()
    {
        if (_isSortedCore)
        {
            _isSortedCore = false;
            _sortPropertyCore = null;
            _sortDirectionCore = ListSortDirection.Ascending;
            Sort();
        }
    }

    public string DefaultSortItem
    {
        get => _defaultSortItem;
        set
        {
            if (_defaultSortItem != value)
            {
                _defaultSortItem = value;
                Sort();
            }
        }
    }

    private void Sort()
    {
        List<T> list = (Items as List<T>);
        list.Sort(CompareCore);
        ResetBindings();
    }

    private int CompareCore(T o1, T o2)
    {
        int ret = 0;
        if (SortPropertyCore != null)
        {
            ret = CompareValue(SortPropertyCore.GetValue(o1), SortPropertyCore.GetValue(o2), SortPropertyCore.PropertyType);
        }
        if (ret == 0 && DefaultSortItem != null)
        {
            PropertyInfo property = typeof(T).GetProperty(DefaultSortItem, BindingFlags.Public | BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.IgnoreCase, null, null, new Type[0], null);
            if (property != null)
            {
                ret = CompareValue(property.GetValue(o1, null), property.GetValue(o2, null), property.PropertyType);
            }
        }
        if (SortDirectionCore == ListSortDirection.Descending) ret = -ret;
        return ret;
    }

    private static int CompareValue(object o1, object o2, Type type)
    {

        if (o1 == null) return o2 == null ? 0 : -1;
        else if (o2 == null) return 1;
        else if (type.IsPrimitive || type.IsEnum) return Convert.ToDouble(o1).CompareTo(Convert.ToDouble(o2));
        else if (type == typeof(DateTime)) return Convert.ToDateTime(o1).CompareTo(o2);
        else return String.Compare(o1.ToString().Trim(), o2.ToString().Trim());
    }
}