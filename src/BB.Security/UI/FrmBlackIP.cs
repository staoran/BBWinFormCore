using System.Collections.Specialized;
using System.Drawing;
using System.Windows.Forms;
using BB.BaseUI.BaseUI;
using BB.BaseUI.Extension;
using BB.Tools.Entity;
using BB.Tools.Format;
using BB.Entity.Security;
using BB.HttpServices.Core.BlackIP;
using BB.Tools.Collections;
using BB.Tools.Extension;

namespace BB.Security.UI;

/// <summary>
/// 登陆系统的黑白名单列表
/// </summary>	
public partial class FrmBlackIp : BaseDock
{
    private readonly BlackIPHttpService _bll;

    public FrmBlackIp(BlackIPHttpService bll)
    {
        InitializeComponent();

        _bll = bll;

        InitDictItem();

        winGridViewPager1.OnStartExport += winGridViewPager1_OnStartExport;
        winGridViewPager1.OnEditSelected += winGridViewPager1_OnEditSelected;
        winGridViewPager1.OnAddNew += winGridViewPager1_OnAddNew;
        winGridViewPager1.OnDeleteSelected += winGridViewPager1_OnDeleteSelected;
        winGridViewPager1.OnRefresh += winGridViewPager1_OnRefresh;
        winGridViewPager1.AppendedMenu = contextMenuStrip1;
        winGridViewPager1.ShowLineNumber = true;
        winGridViewPager1.BestFitColumnWith = false;//是否设置为自动调整宽度，false为不设置
        winGridViewPager1.gridView1.DataSourceChanged +=gridView1_DataSourceChanged;
        winGridViewPager1.gridView1.CustomColumnDisplayText += gridView1_CustomColumnDisplayText;
        winGridViewPager1.gridView1.RowCellStyle += gridView1_RowCellStyle;

        //关联回车键进行查询
        foreach (Control control in layoutControl1.Controls)
        {
            control.KeyUp += SearchControl_KeyUp;
        }
    }
    void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
    {
        //AuthorizeType,Forbid
        if (e.Column.FieldName == "AuthorizeType")
        {
            Color color = Color.White;
            if (e.CellValue.ToString() == "0")
            {
                e.Appearance.BackColor = Color.Black;
                e.Appearance.BackColor2 = Color.LightCyan;
            }
            else
            {
                e.Appearance.BackColor = Color.White;
            }
        }
        if (e.Column.FieldName == "Forbid")
        {
            Color color = Color.White;
            if (e.CellValue.ObjToBool())
            {
                e.Appearance.BackColor = Color.Red;
                e.Appearance.BackColor2 = Color.LightCyan;
            }
        }      
    }
    void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
    {
        if (e.Column.ColumnType == typeof(DateTime))
        {
            string columnName = e.Column.FieldName;
            if (e.Value != null)
            {
                if (Convert.ToDateTime(e.Value) <= Convert.ToDateTime("1900-1-1"))
                {
                    e.DisplayText = "";
                }
                else
                {
                    e.DisplayText = Convert.ToDateTime(e.Value).ToString("yyyy-MM-dd HH:mm");//yyyy-MM-dd
                }
            }
        }
        else if (e.Column.FieldName == "AuthorizeType")
        {
            if (e.Value != null)
            {
                e.DisplayText = ((AuthrizeType)e.Value).ToString();
            }
        }
    }
        
    /// <summary>
    /// 绑定数据后，分配各列的宽度
    /// </summary>
    private void gridView1_DataSourceChanged(object sender, EventArgs e)
    {
        if (winGridViewPager1.gridView1.Columns.Count > 0 && winGridViewPager1.gridView1.RowCount > 0)
        {
            //统一设置100宽度
            foreach (DevExpress.XtraGrid.Columns.GridColumn column in winGridViewPager1.gridView1.Columns)
            {
                column.Width = 100;
            }

            //可特殊设置特别的宽度 
            SetGridColumWidth("Name", 200);
            SetGridColumWidth("Note", 200);
        }
    }

    private void SetGridColumWidth(string columnName, int width)
    {
        DevExpress.XtraGrid.Columns.GridColumn column = winGridViewPager1.gridView1.Columns.ColumnByFieldName(columnName);
        if (column != null)
        {
            column.Width = width;
        }
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
        //初始化分类
        Dictionary<string, object> dictEnum = EnumHelper.GetMemberKeyValue<AuthrizeType>();
        txtAuthorizeType.Properties.Items.Clear();
        foreach (string item in dictEnum.Keys)
        {
            txtAuthorizeType.Properties.Items.Add(new CListItem(dictEnum[item].ToString(), item));
        }
    }
        
    /// <summary>
    /// 分页控件刷新操作
    /// </summary>
    private async void winGridViewPager1_OnRefresh(object sender, EventArgs e)
    {
        await BindData();
    }
        
    /// <summary>
    /// 分页控件删除操作
    /// </summary>
    private async void winGridViewPager1_OnDeleteSelected(object sender, EventArgs e)
    {
        if ("您确定删除选定的记录么？".ShowYesNoAndUxTips() == DialogResult.No)
        {
            return;
        }

        int[] rowSelected = winGridViewPager1.GridView1.GetSelectedRows();
        foreach (int iRow in rowSelected)
        {
            string id = winGridViewPager1.GridView1.GetRowCellDisplayText(iRow, "ID");
            await _bll.DeleteAsync(id);
        }
             
        await BindData();
    }
        
    /// <summary>
    /// 分页控件编辑项操作
    /// </summary>
    private async void winGridViewPager1_OnEditSelected(object sender, EventArgs e)
    {
        string id = winGridViewPager1.gridView1.GetFocusedRowCellDisplayText("ID");
        List<string> idList = new List<string>();
        for (int i = 0; i < winGridViewPager1.gridView1.RowCount; i++)
        {
            string strTemp = winGridViewPager1.GridView1.GetRowCellDisplayText(i, "ID");
            idList.Add(strTemp);
        }

        if (!string.IsNullOrEmpty(id))
        {
            FrmEditBlackIp dlg = new FrmEditBlackIp(_bll);
            dlg.ID = id;
            dlg.IdList = idList;
            dlg.OnDataSaved += dlg_OnDataSaved;
                
            if (DialogResult.OK == dlg.ShowDialog())
            {
                await BindData();
            }
        }
    }        
        
    async void dlg_OnDataSaved(object sender, EventArgs e)
    {
        await BindData();
    }
        
    /// <summary>
    /// 分页控件新增操作
    /// </summary>        
    private void winGridViewPager1_OnAddNew(object sender, EventArgs e)
    {
        btnAddNew_Click(sender, e);
    }
        
    /// <summary>
    /// 分页控件全部导出操作前的操作
    /// </summary> 
    private async void winGridViewPager1_OnStartExport(object sender, EventArgs e)
    {
        Dictionary<string,string> condition = GetConditionSql();
        winGridViewPager1.AllToExport = await _bll.FindAsync(condition);
    }

    /// <summary>
    /// 分页控件翻页的操作
    /// </summary> 
    private async void winGridViewPager1_OnPageChanged(object sender, EventArgs e)
    {
        await BindData();
    }
 
        
    /// <summary>
    /// 根据查询条件构造查询语句
    /// </summary> 
    private Dictionary<string,string> GetConditionSql()
    {
        var condition = new NameValueCollection
        {
            { BlackIpInfo.FieldName, txtName.Text },
            { BlackIpInfo.FieldAuthorizeType, txtAuthorizeType.Text.Trim() }
        };

        if (txtForbid.Checked)
        {
            condition.Add(BlackIpInfo.FieldForbid, "1");
        }

        return condition.ToDicString();
    }
        
    /// <summary>
    /// 绑定列表数据
    /// </summary>
    private async Task BindData()
    {
        //entity
        winGridViewPager1.DisplayColumns = "Name,AuthorizeType,Forbid,IPStart,IPEnd,Note,Creator,CreationDate";
        winGridViewPager1.ColumnNameAlias = await _bll.GetColumnNameAliasAsync();//字段列显示名称转义

        #region 添加别名解析

        //this.winGridViewPager1.AddColumnAlias("Name", "显示名称");
        //this.winGridViewPager1.AddColumnAlias("AuthorizeType", "授权类型");
        //this.winGridViewPager1.AddColumnAlias("Forbid", "是否禁用");
        //this.winGridViewPager1.AddColumnAlias("IPStart", "IP起始地址");
        //this.winGridViewPager1.AddColumnAlias("IPEnd", "IP结束地址");
        //this.winGridViewPager1.AddColumnAlias("Note", "备注");
        //this.winGridViewPager1.AddColumnAlias("Creator", "创建人");
        //this.winGridViewPager1.AddColumnAlias("CreationDate", "创建时间");

        #endregion

        Dictionary<string,string> condition = GetConditionSql();
        List<BlackIpInfo> list = await _bll.FindAsync(condition);
        winGridViewPager1.DataSource = new SortableBindingList<BlackIpInfo>(list);
        winGridViewPager1.PrintTitle = "登陆系统的黑白名单列表报表";
    }
        
    /// <summary>
    /// 查询数据操作
    /// </summary>
    private async void btnSearch_Click(object sender, EventArgs e)
    {
        await BindData();
    }
        
    /// <summary>
    /// 新增数据操作
    /// </summary>
    private async void btnAddNew_Click(object sender, EventArgs e)
    {
        FrmEditBlackIp dlg = new FrmEditBlackIp(_bll);
        dlg.OnDataSaved += dlg_OnDataSaved;
            
        if (DialogResult.OK == dlg.ShowDialog())
        {
            await BindData();
        }
    }
        
    /// <summary>
    /// 提供给控件回车执行查询的操作
    /// </summary>
    private void SearchControl_KeyUp(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            btnSearch_Click(sender, e);
        }
    }

    private void txtForbid_CheckedChanged(object sender, EventArgs e)
    {
        //BindData();
    }        					 						 						 						 						 						 						 						 						 						 						 	 						 	 
}