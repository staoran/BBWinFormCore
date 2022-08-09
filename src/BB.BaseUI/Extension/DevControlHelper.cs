using DevExpress.Utils.Drawing;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace BB.BaseUI.Extension;

internal class DevControlHelper
{
    public static void DrawCheckBox(ColumnHeaderCustomDrawEventArgs e, bool chk)
    {
        if (e.Column.ColumnEdit is RepositoryItemCheckEdit repositoryCheck)
        {
            Graphics g = e.Graphics;
            Rectangle r = e.Bounds;

            CheckEditViewInfo info;
            CheckEditPainter painter;
            ControlGraphicsInfoArgs args;
            info = repositoryCheck.CreateViewInfo() as CheckEditViewInfo;

            painter = repositoryCheck.CreatePainter() as CheckEditPainter;
            info.EditValue = chk;
            info.Bounds = r;
            info.CalcViewInfo(g);
            args = new ControlGraphicsInfoArgs(info, new GraphicsCache(g), r);
            painter.Draw(args);
            args.Cache.Dispose();
        }
    }

    public static bool ClickGridCheckBox(GridView gridView, string fieldName, bool currentStatus)
    {
        if (gridView != null)
        {
            gridView.ClearSorting();//禁止排序

            gridView.PostEditor();
            GridHitInfo info;
            Point pt = gridView.GridControl.PointToClient(System.Windows.Forms.Control.MousePosition);
            info = gridView.CalcHitInfo(pt);
            if (info.InColumn && info.Column != null && info.Column.FieldName == fieldName)
            {
                for (int i = 0; i < gridView.RowCount; i++)
                {
                    gridView.SetRowCellValue(i, fieldName, !currentStatus);
                }
                return true;
            }
        }
        return false;
    }
}