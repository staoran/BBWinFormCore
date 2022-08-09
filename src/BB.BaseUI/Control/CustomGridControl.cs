using DevExpress.Data.Filtering;
using DevExpress.Data.Filtering.Helpers;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Registrator;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.Drawing;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace BB.BaseUI.Control;

/// <summary>
/// 自定义表格控件
/// </summary>
public class CustomGridView : GridView
{
    public CustomGridView() : base() { }

    protected internal virtual void SetGridControlAccessMetod(GridControl newControl)
    {
        SetGridControl(newControl);
    }

    protected override string OnCreateLookupDisplayFilter(string text, string displayMember)
    {
        List<CriteriaOperator> subStringOperators = new List<CriteriaOperator>();
        foreach (string sString in text.Split(' '))
        {
            string exp = LikeData.CreateContainsPattern(sString);
            List<CriteriaOperator> columnsOperators = new List<CriteriaOperator>();
            foreach (GridColumn col in Columns)
            {
                if (col.Visible && col.ColumnType == typeof(string))
                    columnsOperators.Add(new BinaryOperator(col.FieldName, exp, BinaryOperatorType.Like));
            }
            subStringOperators.Add(new GroupOperator(GroupOperatorType.Or, columnsOperators));
        }
        return new GroupOperator(GroupOperatorType.And, subStringOperators).ToString();
    }

    protected override string ViewName => "CustomGridView";
    protected virtual internal string GetExtraFilterText => ExtraFilterText;
}

public class CustomGridControl : GridControl
{
    public CustomGridControl() : base() { }

    protected override void RegisterAvailableViewsCore(InfoCollection collection)
    {
        base.RegisterAvailableViewsCore(collection);
        collection.Add(new CustomGridInfoRegistrator());
    }

    protected override BaseView CreateDefaultView()
    {
        return CreateView("CustomGridView");
    }        
}

public class CustomGridPainter : GridPainter
{
    public CustomGridPainter(GridView view) : base(view) { }

    public virtual new CustomGridView View => (CustomGridView)base.View;

    protected override void DrawRowCell(GridViewDrawArgs e, GridCellInfo cell)
    {
        cell.ViewInfo.MatchedStringUseContains = true;
        cell.ViewInfo.MatchedString = View.GetExtraFilterText;
        cell.State = GridRowCellState.Dirty;
        e.ViewInfo.UpdateCellAppearance(cell);
        base.DrawRowCell(e, cell);
    }
}

public class CustomGridInfoRegistrator : GridInfoRegistrator
{
    public CustomGridInfoRegistrator() : base() { }
    public override BaseViewPainter CreatePainter(BaseView view)
    {
        return new CustomGridPainter(view
            as GridView);
    }
    public override string ViewName => "CustomGridView";

    public override BaseView CreateView(GridControl grid)
    {
        CustomGridView view = new CustomGridView();
        view.SetGridControlAccessMetod(grid);
        return view;
    }

}