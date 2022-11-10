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
using DevExpress.XtraBars;
using Furion.Logging.Extensions;

namespace BB.TMS.UI;

/// <summary>
/// 单号规则
/// </summary>
public partial class FrmDocNoRule : BaseViewDock<DocNoRule, DocNoRuleHttpService, FrmEditDocNoRule>
{
    public FrmDocNoRule(DocNoRuleHttpService bll, LazilyResolved<FrmEditDocNoRule> baseForm) : base(bll, baseForm)
    {
        moduleName = "单号规则";

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
    private void FrmDocNoRule_Shown(object? sender, EventArgs e)
    {
    }

    /// <summary>
    /// 快查树初始化
    /// </summary>
    protected override void InitTree()
    {
        treeView1.InitTree(x =>
        {
            x.AddNode(DocNoRule.FieldResetZero, "自动归零", GB.YesOrNo);
            x.AddNode(DocNoRule.FieldFlagSpilitNo, "包含单据字头", GB.YesOrNo);
            x.AddNode(DocNoRule.FieldFlagIncludeDocCode, "序号前加间隔符", GB.YesOrNo);
            x.AddNode(DocNoRule.FieldFlagLastMillisecond, "末尾增加毫秒", GB.YesOrNo);
        });
    }

    /// <summary>
    /// 初始化字典列表内容
    /// </summary>
    protected sealed override async Task InitDictItem()
    {
        #region 查询表单初始化

        txtResetZero.BindDictItems(GB.YesOrNo, null, true, false);
        txtFlagSpilitNo.BindDictItems(GB.YesOrNo, null, true, false);
        txtFlagIncludeDocCode.BindDictItems(GB.YesOrNo, null, true, false);
        txtFlagLastMillisecond.BindDictItems(GB.YesOrNo, null, true, false);

        #endregion

        #region Grid初始化

        await base.InitDictItem();

        #region 主表列

        // 显示列的内容和排序，如不设置则默认按照别名解析排列
        // winGridViewPager1.DisplayColumns = "isid,DocCode,RuleFormat,NoLength,ResetZero,FlagSpilitNo,FlagIncludeDocCode,FlagLastMillisecond,CurrentValue,CurrentYMD,CreationDate,CreatedBy";

        #region 添加别名解析，两种方式均可，尽量从后台取

        #region 前端直接定义

        // winGridViewPager1.AddColumnAlias(DocNoRule.Fieldisid, "自增ID");
        // winGridViewPager1.AddColumnAlias(DocNoRule.FieldDocCode, "单据编码");
        // winGridViewPager1.AddColumnAlias(DocNoRule.FieldRuleFormat, "编码组合");
        // winGridViewPager1.AddColumnAlias(DocNoRule.FieldNoLength, "自增数长度");
        // winGridViewPager1.AddColumnAlias(DocNoRule.FieldResetZero, "自动归零");
        // winGridViewPager1.AddColumnAlias(DocNoRule.FieldFlagSpilitNo, "包含单据字头");
        // winGridViewPager1.AddColumnAlias(DocNoRule.FieldFlagIncludeDocCode, "序号前加间隔符");
        // winGridViewPager1.AddColumnAlias(DocNoRule.FieldFlagLastMillisecond, "末尾增加毫秒");
        // winGridViewPager1.AddColumnAlias(DocNoRule.FieldCurrentValue, "当前序号");
        // winGridViewPager1.AddColumnAlias(DocNoRule.FieldCurrentYMD, "当前年月日");
        // winGridViewPager1.AddColumnAlias(DocNoRule.FieldCreationDate, "创建日期");
        // winGridViewPager1.AddColumnAlias(DocNoRule.FieldCreatedBy, "创建人");

        #endregion

        // 获取字段显示权限，并设置(默认不使用字段权限)
        // winGridViewPager1.gridView1.SetColumnsPermit(permitDict);

        #endregion

        #region 添加列字段数据源

        winGridViewPager1.SetColumnDataSource(DocNoRule.FieldCreatedBy, GB.AllUserDict);

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
            //     gridView.SetGridColumWidth(DocNoRule.Fieldisid, 200);
            //     gridView.SetGridColumWidth(DocNoRule.FieldDocCode, 200);
            //     gridView.SetGridColumWidth(DocNoRule.FieldRuleFormat, 200);
            //     gridView.SetGridColumWidth(DocNoRule.FieldNoLength, 200);
            //     gridView.SetGridColumWidth(DocNoRule.FieldResetZero, 200);
            //     gridView.SetGridColumWidth(DocNoRule.FieldFlagSpilitNo, 200);
            //     gridView.SetGridColumWidth(DocNoRule.FieldFlagIncludeDocCode, 200);
            //     gridView.SetGridColumWidth(DocNoRule.FieldFlagLastMillisecond, 200);
            //     gridView.SetGridColumWidth(DocNoRule.FieldCurrentValue, 200);
            //     gridView.SetGridColumWidth(DocNoRule.FieldCurrentYMD, 200);
            //     gridView.SetGridColumWidth(DocNoRule.FieldCreationDate, 200);
            //     gridView.SetGridColumWidth(DocNoRule.FieldCreatedBy, 200);
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
            { DocNoRule.FieldDocCode, txtDocCode.Text.Trim() },
            { DocNoRule.FieldResetZero, txtResetZero.GetComboBoxValue() },
            { DocNoRule.FieldFlagSpilitNo, txtFlagSpilitNo.GetComboBoxValue() },
            { DocNoRule.FieldFlagIncludeDocCode, txtFlagIncludeDocCode.GetComboBoxValue() },
            { DocNoRule.FieldFlagLastMillisecond, txtFlagLastMillisecond.GetComboBoxValue() },
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
        var info = new DocNoRule
        {
            ISID = GetRowData(dr, "自增ID").ObjToInt(),
            DocCode = GetRowData(dr, "单据编码"),
            RuleFormat = GetRowData(dr, "编码组合"),
            NoLength = GetRowData(dr, "自增数长度").ObjToInt(),
            ResetZero = GetRowData(dr, "自动归零") == "是",
            FlagSpilitNo = GetRowData(dr, "包含单据字头") == "是",
            FlagIncludeDocCode = GetRowData(dr, "序号前加间隔符") == "是",
            FlagLastMillisecond = GetRowData(dr, "末尾增加毫秒") == "是",
            CurrentValue = GetRowData(dr, "当前序号").ObjToInt(),
            CurrentYMD = GetRowData(dr, "当前年月日").ObjToInt(),
            CreationDate = GetRowData(dr, "创建日期").ToDateTime(dtNow),
            CreatedBy = GetRowData(dr, "创建人"),
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
        List<DocNoRule> list = await _bll.FindAsync(condition);
        DataTable dtNew = DataTableHelper.CreateTable(
            "自增ID,单据编码,编码组合,自增数长度,自动归零,包含单据字头,序号前加间隔符,末尾增加毫秒,当前序号,当前年月日,创建日期,创建人");
        var j = 1;
        foreach (DocNoRule t in list)
        {
            DataRow dr = dtNew.NewRow();
            dr["序号"] = j++;
            dr["自增ID"] = t.ISID;
            dr["单据编码"] = t.DocCode;
            dr["编码组合"] = t.RuleFormat;
            dr["自增数长度"] = t.NoLength;
            dr["自动归零"] = t.ResetZero ? "是" : "否";
            dr["包含单据字头"] = t.FlagSpilitNo ? "是" : "否";
            dr["序号前加间隔符"] = t.FlagIncludeDocCode ? "是" : "否";
            dr["末尾增加毫秒"] = t.FlagLastMillisecond ? "是" : "否";
            dr["当前序号"] = t.CurrentValue;
            dr["当前年月日"] = t.CurrentYMD;
            dr["创建日期"] = t.CreationDate;
            dr["创建人"] = t.CreatedBy;
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
            AdvDlg?.AddColumnListItem(DocNoRule.FieldCreatedBy, GB.AllUserDict);

            #endregion

        AdvDlg?.ShowDialog();
    }

    #endregion
}