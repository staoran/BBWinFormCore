using System.Data;
using System.Windows.Forms;
using System.Collections.Specialized;
using System.Diagnostics;
using BB.BaseUI.BaseUI;
using BB.BaseUI.DocViewer;
using BB.BaseUI.Extension;
using BB.BaseUI.Other;
using BB.BaseUI.WinForm;
using BB.Tools.Format;
using BB.Entity.TMS;
using BB.HttpServices.TMS;
using BB.Tools.Entity;
using BB.Tools.Extension;
using Furion.Logging.Extensions;

namespace BB.TMS.UI;

/// <summary>
/// 公式定义
/// </summary>
#if DESIGNER
public partial class FrmBasicTranslateWords : BaseViewDesigner
#else
public partial class FrmBasicTranslateWords : BaseViewDock<BasicTranslateWords, BasicTranslateWordsHttpService, FrmEditBasicTranslateWords>
#endif
{
    public FrmBasicTranslateWords(BasicTranslateWordsHttpService bll, LazilyResolved<FrmEditBasicTranslateWords> baseForm) : base(bll, baseForm)
    {
        moduleName = "公式定义";

        InitializeComponent();
    }

    /// <summary>
    /// 编写初始化窗体的实现
    /// </summary>
    public override async Task FormOnLoad()
    {
        await base.FormOnLoad();

        await InitDictItem();

        #region 查询表单


        #endregion

        #region 按钮和按钮权限

        #endregion
    }

    /// <summary>
    /// 窗体首次打开后
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void FrmBasicTranslateWords_Shown(object? sender, EventArgs e)
    {
    }

    /// <summary>
    /// 快查树初始化
    /// </summary>
    protected override void InitTree()
    {
        treeView1.InitTree(x =>
        {
            x.AddNode(BasicTranslateWords.FieldTranslateType, "公式关键字类型");
            x.AddNode(BasicTranslateWords.FieldCanSelectYN, "可选", GB.YesOrNo);
            x.AddNode(BasicTranslateWords.FieldCancelYN, "禁用", GB.YesOrNo);
        });
    }

    /// <summary>
    /// 初始化字典列表内容
    /// </summary>
    protected sealed override async Task InitDictItem()
    {
        #region 查询表单初始化

        txtTranslateType.BindDictItems("公式关键字类型", "", true, false);
        txtCanSelectYN.BindDictItems(GB.YesOrNo, null, true, false);
        txtCancelYN.BindDictItems(GB.YesOrNo, null, true, false);

        #endregion

        #region Grid初始化

        await base.InitDictItem();

        #region 主表列

        // 显示列的内容和排序，如不设置则默认按照别名解析排列
        // winGridViewPager1.DisplayColumns = "ISID,WordsInFront,WordsBehind,TranslateType,CanSelectYN,ExampleStr,CancelYN,Remark,CreationDate,CreatedBy,LastUpdateDate,LastUpdatedBy";

        #region 添加别名解析，两种方式均可，尽量从后台取

        #region 前端直接定义

        // winGridViewPager1.AddColumnAlias(BasicTranslateWords.FieldISID, "自增ID");
        // winGridViewPager1.AddColumnAlias(BasicTranslateWords.FieldWordsInFront, "关键字");
        // winGridViewPager1.AddColumnAlias(BasicTranslateWords.FieldWordsBehind, "代码");
        // winGridViewPager1.AddColumnAlias(BasicTranslateWords.FieldTranslateType, "代码类型");
        // winGridViewPager1.AddColumnAlias(BasicTranslateWords.FieldCanSelectYN, "可选");
        // winGridViewPager1.AddColumnAlias(BasicTranslateWords.FieldExampleStr, "说明");
        // winGridViewPager1.AddColumnAlias(BasicTranslateWords.FieldCancelYN, "禁用");
        // winGridViewPager1.AddColumnAlias(BasicTranslateWords.FieldRemark, "备注");
        // winGridViewPager1.AddColumnAlias(BasicTranslateWords.FieldCreationDate, "创建时间");
        // winGridViewPager1.AddColumnAlias(BasicTranslateWords.FieldCreatedBy, "创建人");
        // winGridViewPager1.AddColumnAlias(BasicTranslateWords.FieldLastUpdateDate, "修改时间");
        // winGridViewPager1.AddColumnAlias(BasicTranslateWords.FieldLastUpdatedBy, "修改人");

        #endregion

        // 获取字段显示权限，并设置(默认不使用字段权限)
        // winGridViewPager1.gridView1.SetColumnsPermit(permitDict);

        #endregion

        #region 添加列字段数据源

        winGridViewPager1.SetColumnDataSource(BasicTranslateWords.FieldTranslateType, "公式关键字类型");
        winGridViewPager1.SetColumnDataSource(BasicTranslateWords.FieldCreatedBy, GB.AllUserDict);
        winGridViewPager1.SetColumnDataSource(BasicTranslateWords.FieldLastUpdatedBy, GB.AllUserDict);

        #endregion

        #region 添加列字段汇总行


        #endregion

        #endregion

        #endregion
    }

    #region 网格列表信息

    /// <summary>
    /// 数据源变更时，分配各列的宽度
    /// </summary>
    protected override void gridView1_DataSourceChanged(object? sender, EventArgs e)
    {
        base.gridView1_DataSourceChanged(sender, e);
        if (winGridViewPager1.gridView1.Columns.Count > 0 && winGridViewPager1.gridView1.RowCount > 0)
        {
            #region 单独设置列宽

            // 可特殊设置特别的宽度
            // GridView gridView = winGridViewPager1.gridView1;
            // if (gridView != null)
            // {
            //     gridView.SetGridColumWidth(BasicTranslateWords.FieldISID, 200);
            //     gridView.SetGridColumWidth(BasicTranslateWords.FieldWordsInFront, 200);
            //     gridView.SetGridColumWidth(BasicTranslateWords.FieldWordsBehind, 200);
            //     gridView.SetGridColumWidth(BasicTranslateWords.FieldTranslateType, 200);
            //     gridView.SetGridColumWidth(BasicTranslateWords.FieldCanSelectYN, 200);
            //     gridView.SetGridColumWidth(BasicTranslateWords.FieldExampleStr, 200);
            //     gridView.SetGridColumWidth(BasicTranslateWords.FieldCancelYN, 200);
            //     gridView.SetGridColumWidth(BasicTranslateWords.FieldRemark, 200);
            //     gridView.SetGridColumWidth(BasicTranslateWords.FieldCreationDate, 200);
            //     gridView.SetGridColumWidth(BasicTranslateWords.FieldCreatedBy, 200);
            //     gridView.SetGridColumWidth(BasicTranslateWords.FieldLastUpdateDate, 200);
            //     gridView.SetGridColumWidth(BasicTranslateWords.FieldLastUpdatedBy, 200);
            // }

            #endregion
        }
    }

    #endregion

    #region 快捷查询条件

    /// <summary>
    /// 根据查询条件构造查询条件对象
    /// </summary>
    protected override Dictionary<string,string> GetQueryCondition()
    {
        // 如果存在高级查询对象信息，则使用高级查询条件，否则使用主表条件查询
        return (_treeCondition ?? _advanceCondition ?? new NameValueCollection
        {
            { BasicTranslateWords.FieldWordsInFront, txtWordsInFront.Text.Trim() },
            { BasicTranslateWords.FieldWordsBehind, txtWordsBehind.Text.Trim() },
            { BasicTranslateWords.FieldTranslateType, txtTranslateType.GetComboBoxValue() },
            { BasicTranslateWords.FieldCanSelectYN, txtCanSelectYN.GetComboBoxValue() },
            { BasicTranslateWords.FieldExampleStr, txtExampleStr.Text.Trim() },
            { BasicTranslateWords.FieldCancelYN, txtCancelYN.GetComboBoxValue() },
        }).ToDicString();
    }

    #endregion

    #region 导入导出

    /// <summary>
    /// 导入数据保存事件
    /// </summary>
    /// <param name="dr"></param>
    /// <returns></returns>
    protected override async Task<bool> ExcelData_OnDataSave(DataRow dr)
    {
        DateTime dtNow = DateTime.Now;
        var info = new BasicTranslateWords
        {
            ISID = GetRowData(dr, "自增ID").ObjToInt(),
            WordsInFront = GetRowData(dr, "关键字"),
            WordsBehind = GetRowData(dr, "代码"),
            TranslateType = GetRowData(dr, "代码类型"),
            CanSelectYN = GetRowData(dr, "可选") == "是",
            ExampleStr = GetRowData(dr, "说明"),
            CancelYN = GetRowData(dr, "禁用") == "是",
            Remark = GetRowData(dr, "备注"),
            CreationDate = GetRowData(dr, "创建时间").ToDateTime(dtNow),
            CreatedBy = GetRowData(dr, "创建人"),
            LastUpdateDate = GetRowData(dr, "修改时间").ToDateTime(dtNow),
            LastUpdatedBy = GetRowData(dr, "修改人"),
        };

        return await _bll.InsertAsync(info);
    }

    /// <summary>
    /// 导出的操作
    /// </summary>
    protected override async Task ExportData()
    {
        string file = FileDialogHelper.SaveExcel($"{moduleName}.xls");
                if (string.IsNullOrEmpty(file)) return;
        Dictionary<string,string> condition = GetQueryCondition();
        List<BasicTranslateWords> list = await _bll.FindAsync(condition);
        DataTable dtNew = DataTableHelper.CreateTable(
            "自增ID,关键字,代码,代码类型,可选,说明,禁用,备注,创建时间,创建人,修改时间,修改人");
        var j = 1;
        foreach (BasicTranslateWords t in list)
        {
            DataRow dr = dtNew.NewRow();
            dr["序号"] = j++;
            dr["自增ID"] = t.ISID;
            dr["关键字"] = t.WordsInFront;
            dr["代码"] = t.WordsBehind;
            dr["代码类型"] = t.TranslateType;
            dr["可选"] = t.CanSelectYN ? "是" : "否";
            dr["说明"] = t.ExampleStr;
            dr["禁用"] = t.CancelYN ? "是" : "否";
            dr["备注"] = t.Remark;
            dr["创建时间"] = t.CreationDate;
            dr["创建人"] = t.CreatedBy;
            dr["修改时间"] = t.LastUpdateDate;
            dr["修改人"] = t.LastUpdatedBy;
            dtNew.Rows.Add(dr);
        }

        try
        {
            AsposeExcelTools.DataTableToExcel2(dtNew, file, out string error);
            if (!string.IsNullOrEmpty(error))
            {
                $"导出Excel出现错误：{error}".ShowUxError();
            }
            else
            {
                if ("导出成功，是否打开文件？".ShowYesNoAndUxTips() == DialogResult.Yes)
                {
                    Process.Start(file);
                }
            }
        }
        catch (Exception ex)
        {
            ex.ToString().LogError();
            ex.Message.ShowUxError();
        }
    }

    #endregion

    #region 高级查询

    /// <summary>
    /// 高级查询的操作
    /// </summary>
    protected override async Task AdvanceSearch()
    {
            await base.AdvanceSearch();

            #region 下拉列表数据

            // _advDlg.AddColumnListItem("UserType", Portal.gc.GetDictData("人员类型"));// 字典列表
            // _advDlg.AddColumnListItem("Sex", "男,女");// 固定列表
            // _advDlg.AddColumnListItem("Credit", _bll.GetFieldList("Credit"));// 动态列表
            AdvDlg?.AddColumnListItem(BasicTranslateWords.FieldTranslateType, "公式关键字类型");
            AdvDlg?.AddColumnListItem(BasicTranslateWords.FieldCreatedBy, GB.AllUserDict);
            AdvDlg?.AddColumnListItem(BasicTranslateWords.FieldLastUpdatedBy, GB.AllUserDict);

            #endregion

        AdvDlg?.ShowDialog();
    }

    #endregion
}