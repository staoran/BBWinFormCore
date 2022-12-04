using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using BB.BaseUI.DocViewer;
using BB.BaseUI.Extension;
using BB.BaseUI.WinForm;
using BB.Tools.File;
using BB.Tools.MultiLanuage;
using Furion.Logging.Extensions;

namespace BB.BaseUI.BaseUI;

/// <summary>
/// 通用Excel数据导入操作
/// </summary>
public partial class FrmImportExcelData : BaseForm
{
    private AppConfig _config = new AppConfig();
    private DataSet _myDs = new DataSet();
    private BackgroundWorker _worker = null;

    /// <summary>
    /// 数据保存的代理定义
    /// </summary>
    /// <param name="dr"></param>
    /// <returns></returns>
    public delegate Task<bool> SaveDataHandler(DataRow dr);

    /// <summary>
    /// 数据保存的事件
    /// </summary>
    public event SaveDataHandler OnDataSave;
    /// <summary>
    /// 数据刷新的事件
    /// </summary>
    public event EventHandler OnRefreshData;

    /// <summary>
    /// 默认构造函数
    /// </summary>
    public FrmImportExcelData()
    {
        InitializeComponent();

        gridView1.OptionsBehavior.AutoPopulateColumns = true;

        _worker = new BackgroundWorker();
        _worker.WorkerReportsProgress = true;
        _worker.ProgressChanged += worker_ProgressChanged;
        _worker.DoWork += worker_DoWork;
        _worker.RunWorkerCompleted += worker_RunWorkerCompleted;
    }

    void worker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
    {
        progressBar1.Value = e.ProgressPercentage;
    }

    void worker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
    {
        progressBar1.Visible = false;
        progressBar1.Value = 0;

        if (OnRefreshData != null)
        {
            OnRefreshData(null, null);
        }

        string tips = e.Result as string;
        if (!string.IsNullOrEmpty(tips))
        {
            tips.ShowUxTips();
            if (tips == "操作成功")
            {
                gridControl1.DataSource = null;
            }
        }
    }

    async void worker_DoWork(object? sender, DoWorkEventArgs e)
    {
        int itemCount = 0;
        int errorCount = 0;
        if (_myDs != null && _myDs.Tables[0].Rows.Count > 0)
        {
            //定义步长
            double step = 50 * (1.0 / _myDs.Tables[0].Rows.Count);
            DataTable dt = _myDs.Tables[0];
            int i = 1;
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (OnDataSave != null)
                    {
                        #region 保存操作，如果有错误，则记录并处理
                        try
                        {
                            bool success = await OnDataSave(dr);
                            if (success)
                            {
                                itemCount++;
                            }
                        }
                        catch (Exception ex)
                        {
                            errorCount++;
                            ex.ToString().LogError();
                            ex.Message.ShowUxError();
                        }

                        if (errorCount >= 3)
                        {
                            var format = "记录导入已经连续出错超过[{0}]条，您是否确定退出导入操作？\r\n单击【是】退出导入，单击【否】忽略错误，继续导入下一记录。";
                            format = JsonLanguage.Default.GetString(format);
                            string message = string.Format(format, errorCount);

                            if (message.ShowYesNoAndUxWarning() == DialogResult.Yes)
                            {
                                break;
                            }
                            else
                            {
                                errorCount = 0;//置零重新计算
                            }
                        } 
                        #endregion
                    }

                    int currentStep = Convert.ToInt32(step * i);
                    _worker.ReportProgress(currentStep);
                    i++;
                }

                if (itemCount == dt.Rows.Count)
                {
                    e.Result = "操作成功";
                }
                else
                {
                    e.Result = "操作完成，有错误可能未导入全部";
                }
            }
            catch (Exception ex)
            {
                e.Result = ex.Message;
                ex.ToString().LogError();
                ex.ToString().ShowUxError();
            }
        }
        else
        {
            e.Result = "请检查数据记录是否存在";
        }
    }

    /// <summary>
    /// 设置导入模板标题，及文件路径
    /// </summary>
    /// <param name="title">模板标题</param>
    /// <param name="filePath">模板文件路径</param>
    public void SetTemplate(string title, string filePath)
    {
        //多语言处理
        title = JsonLanguage.Default.GetString(title);

        lnkExcel.Text = title;
        lnkExcel.Tag = filePath;
    }

    private void lnkExcel_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e)
    {
        try
        {
            string templateFile = lnkExcel.Tag.ToString();
            if (string.IsNullOrEmpty(templateFile))
            {
                "导入操作未指定模板文件".ShowUxTips();
                return;
            }
            if (!File.Exists(templateFile))
            {
                (templateFile + " 不存在该模板文件！").ShowUxTips();
                return;
            }
            Process.Start(templateFile);
        }
        catch (Exception)
        {
            "文件打开失败".ShowUxWarning();
        }
    }

    private void FrmImportExcelData_Load(object? sender, EventArgs e)
    {
    }

    private void btnBrowse_Click(object? sender, EventArgs e)
    {
        string file = FileDialogHelper.OpenExcel();
        if (!string.IsNullOrEmpty(file))
        {
            txtFilePath.Text = file;

            ViewData();
        }
    }

    /// <summary>
    /// 查看Excel文件并显示在界面上操作
    /// </summary>
    private void ViewData()
    {
        if (txtFilePath.Text == "")
        {
            "请选择指定的Excel文件".ShowUxTips();
            return;
        }
            
        try
        { 
            _myDs.Tables.Clear();
            _myDs.Clear();
            gridControl1.DataSource = null;   

            string error = "";
            AsposeExcelTools.ExcelFileToDataSet(txtFilePath.Text, out _myDs, out error);
            gridControl1.DataSource = _myDs.Tables[0];
            gridView1.PopulateColumns();
        }
        catch (Exception ex)
        {
            ex.ToString().LogError();
            ex.Message.ShowUxError();
        }
    }

    private void btnSaveData_Click(object? sender, EventArgs e)
    {
        if (_worker.IsBusy)
            return;

        if (txtFilePath.Text == "")
        {
            "请选择指定的Excel文件".ShowUxTips();
            return;
        }

        if ("该操作将把数据导入到系统数据库中，您确定是否继续？".ShowYesNoAndUxWarning() == DialogResult.Yes)
        {
            if (_myDs != null && _myDs.Tables[0].Rows.Count > 0)
            {
                DataTable dt = _myDs.Tables[0];
                progressBar1.Visible = true;
                _worker.RunWorkerAsync();  
            }     
        }
    }

    private DateTime? GetDateTime(TextBox tb)
    {
        DateTime? dt = null;
        if (tb.Text.Length > 0)
        {
            try
            {
                dt = Convert.ToDateTime(tb.Text);
            }
            catch
            {
                // ignored
            }
        }
        return dt;
    }

    private DateTime? GetDateTime(string text)
    {
        DateTime? dt = null;
        if (!string.IsNullOrEmpty(text))
        {
            try
            {
                dt = Convert.ToDateTime(text);
            }
            catch
            {
                // ignored
            }
        }
        return dt;
    }

    private void btnCancel_Click(object? sender, EventArgs e)
    {
        Close();
    }

    /// <summary>
    /// 判断字符是否为中午
    /// </summary>
    /// <param name="str_chinese"></param>
    /// <returns></returns>
    public bool IsChinese(string strChinese)
    {
        return System.Text.RegularExpressions.Regex.IsMatch(strChinese, @"[\u4e00-\u9fa5]");
    }
}