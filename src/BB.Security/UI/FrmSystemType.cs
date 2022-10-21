using System.Windows.Forms;
using BB.BaseUI.BaseUI;
using BB.BaseUI.Extension;
using BB.BaseUI.Other;
using BB.Entity.Security;
using BB.HttpServices.Core.SystemType;
using Furion;

namespace BB.Security.UI;

public partial class FrmSystemType : BaseForm
{
    private readonly SystemTypeHttpService _bll;

    public FrmSystemType(SystemTypeHttpService bll)
    {
        InitializeComponent();
        _bll = bll;

        InitDictItem();

        winGridView1.OnEditSelected += winGridView1_OnEditSelected;
        winGridView1.OnDeleteSelected += winGridView1_OnDeleteSelected;
        winGridView1.OnRefresh += winGridView1_OnRefresh;
        winGridView1.OnAddNew += winGridView1_OnAddNew;
        winGridView1.AppendedMenu = contextMenuStrip1;
        winGridView1.BestFitColumnWith = false;
        winGridView1.gridView1.DataSourceChanged += gridView1_DataSourceChanged;
    }

    /// <summary>
    /// 编写初始化窗体的实现，可以用于刷新
    /// </summary>
    public override async Task FormOnLoad()
    {
        await BindData();
    }

    /// <summary>
    /// 初始化字典列表内容
    /// </summary>
    private void InitDictItem()
    {
        //初始化代码
    }

    private void gridView1_DataSourceChanged(object? sender, EventArgs e)
    {
        if (winGridView1.gridView1.Columns.Count > 0 && winGridView1.gridView1.RowCount > 0)
        {
            winGridView1.gridView1.Columns[nameof(SystemTypeInfo.Oid)].Width = 100;
            winGridView1.gridView1.Columns[nameof(SystemTypeInfo.Name)].Width = 100;
            winGridView1.gridView1.Columns[nameof(SystemTypeInfo.CustomId)].Width = 100;
            winGridView1.gridView1.Columns[nameof(SystemTypeInfo.Authorize)].Width = 100;
            winGridView1.gridView1.Columns[nameof(SystemTypeInfo.Note)].Width = 200;
        }
    }

    private async void winGridView1_OnRefresh(object? sender, EventArgs e)
    {
        await BindData();
    }

    private async void winGridView1_OnDeleteSelected(object? sender, EventArgs e)
    {
        if ("您确定删除选定的记录么？".ShowYesNoAndUxTips() == DialogResult.No)
        {
            return;
        }

        int[] rowSelected = winGridView1.GridView1.GetSelectedRows();
        if (rowSelected != null)
        {
            foreach (int iRow in rowSelected)
            {
                string id = winGridView1.GridView1.GetRowCellDisplayText(iRow, "OID");
                await _bll.DeleteAsync(id);
            }
            await BindData();
        }
    }

    private async void winGridView1_OnEditSelected(object? sender, EventArgs e)
    {
        int[] rowSelected = winGridView1.GridView1.GetSelectedRows();
        foreach (int iRow in rowSelected)
        {
            FrmEditSystemType dlg = new FrmEditSystemType(_bll);
            dlg.ID = winGridView1.GridView1.GetRowCellDisplayText(iRow, "OID");
            if (DialogResult.OK == dlg.ShowDialog())
            {
                await BindData();
            }

            break;
        }
    }

    private async void winGridView1_OnAddNew(object? sender, EventArgs e)
    {
        FrmEditSystemType dlg = App.GetService<FrmEditSystemType>();
        if (DialogResult.OK == dlg.ShowDialog())
        {
            await BindData();
        }
    }

    private async Task BindData()
    {
        winGridView1.DisplayColumns = $"{nameof(SystemTypeInfo.Oid)},{nameof(SystemTypeInfo.Name)},{nameof(SystemTypeInfo.CustomId)},{nameof(SystemTypeInfo.Authorize)},{nameof(SystemTypeInfo.Note)}";
        #region 添加别名解析
        winGridView1.AddColumnAlias(nameof(SystemTypeInfo.Oid), "系统标识");
        winGridView1.AddColumnAlias(nameof(SystemTypeInfo.Name), "系统名称");
        winGridView1.AddColumnAlias(nameof(SystemTypeInfo.CustomId), "客户编码");
        winGridView1.AddColumnAlias(nameof(SystemTypeInfo.Authorize), "授权编码");
        winGridView1.AddColumnAlias(nameof(SystemTypeInfo.Note), "备注");
        #endregion

        winGridView1.DataSource = await _bll.GetAllAsync();
        winGridView1.PrintTitle = GB.AppUnit + " -- " + "系统类型信息报表";
    }

    private void tsbNew_Click(object? sender, EventArgs e)
    {
        winGridView1_OnAddNew(sender, e);
    }

    private void tsbEdit_Click(object? sender, EventArgs e)
    {
        winGridView1_OnEditSelected(sender, e);
    }

    private void tsbDelete_Click(object? sender, EventArgs e)
    {
        winGridView1_OnDeleteSelected(sender, e);
    }
    private async void tsbRefresh_Click(object? sender, EventArgs e)
    {
        await BindData();
    }

    private void tsbClose_Click(object? sender, EventArgs e)
    {
        Close();
    }


}