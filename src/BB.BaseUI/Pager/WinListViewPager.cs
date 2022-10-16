using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using BB.BaseUI.DocViewer;
using BB.Tools.Entity;

namespace BB.BaseUI.Pager;

public partial class WinListViewPager : DevExpress.XtraEditors.XtraUserControl
{
    private DataTable _dataSource = new DataTable();
    private PagerInfo? _pagerInfo;
    //private Export2Excel export2XLS = new Export2Excel();
    private SaveFileDialog _saveFileDialog = new SaveFileDialog();
    private bool _isExportAllPage = false;

    public DataTable TableToExport = new DataTable();
    public ProgressBar ProgressBar;
    public event EventHandler OnStartExport;
    public event EventHandler OnEndExport;
    public event EventHandler OnPageChanged;
    public event EventHandler OnDeleteSelected;
    public event EventHandler OnRefresh;

    public WinListViewPager()
    {
        InitializeComponent();

        pager.PageChanged += pager_PageChanged;
    }

    private void pager_PageChanged(object? sender, EventArgs e)
    {
        if (OnPageChanged != null)
        {
            OnPageChanged(this, EventArgs.Empty);
        }
    }
        
    public DataTable DataSource
    {
        get => _dataSource;
        set
        {
            _dataSource = value;
            listView1.Columns.Clear();
            foreach (DataColumn col in value.Columns)
            {
                listView1.Columns.Add(col.ColumnName, 120);
            }

            ListViewItem item;
            listView1.Items.Clear();
            foreach (DataRow row in value.Rows)
            {
                item = new ListViewItem();
                item.SubItems.Clear();

                item.SubItems[0].Text = row[0].ToString();
                for (int i = 1; i < value.Columns.Count; i++)
                {
                    item.SubItems.Add(row[i].ToString());
                }
                listView1.Items.Add(item);
            }

            pager.InitPageInfo(PagerInfo.TotalRows);
        }
    }

    public PagerInfo PagerInfo
    {
        get
        {
            if (_pagerInfo == null)
            {
                _pagerInfo = new PagerInfo
                {
                    TotalRows = pager.RecordCount,
                    PageNo = pager.CurrentPageIndex,
                    PageSize = pager.PageSize
                };
            }
            else
            {
                _pagerInfo.PageNo = pager.CurrentPageIndex;
            }

            return _pagerInfo;
        }
    }

    private void btnExport_Click(object? sender, EventArgs e)
    {
        _isExportAllPage = true;
        ExportToExcel();
    }

    private void btnExportCurrent_Click(object? sender, EventArgs e)
    {
        _isExportAllPage = false;
        ExportToExcel();
    }        

    #region 导出Excel操作

    private void ExportToExcel()
    {
        _saveFileDialog = new SaveFileDialog();
        _saveFileDialog.Filter = "Excel (*.xls)|*.xls";

        if (_saveFileDialog.ShowDialog() == DialogResult.OK)
        {
            if (!_saveFileDialog.FileName.Equals(String.Empty))
            {
                FileInfo f = new FileInfo(_saveFileDialog.FileName);
                if (f.Extension.ToLower().Equals(".xls"))
                {
                    StartExport(_saveFileDialog.FileName);
                }
                else
                {
                    MessageBox.Show("文件格式不正确");
                }
            }
            else
            {
                MessageBox.Show("需要指定一个保存的目录");
            }
        }
    }

    /// <summary>
    /// starts the export to new excel document
    /// </summary>
    /// <param name="filepath">the file to export to</param>
    private void StartExport(String filepath)
    {
        if (OnStartExport != null)
        {
            OnStartExport(this, EventArgs.Empty);
        }

        //create a new background worker, to do the exporting
        BackgroundWorker bg = new BackgroundWorker();
        bg.DoWork += bg_DoWork;
        bg.RunWorkerCompleted += bg_RunWorkerCompleted;
        bg.RunWorkerAsync(filepath);
    }

    //do the new excel document work using the background worker
    private void bg_DoWork(object? sender, DoWorkEventArgs e)
    {
        DataTable dtExport = _dataSource;
        if (TableToExport != null && _isExportAllPage)
        {
            dtExport = TableToExport;
        }

        string outError = "";
        AsposeExcelTools.DataTableToExcel2(dtExport, (String)e.Argument, out outError);
    }

    //show a message to the user when the background worker has finished
    //and re-enable the export buttons
    private void bg_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
    {
        if (OnEndExport != null)
        {
            OnEndExport(this, EventArgs.Empty);
        }

        if (MessageBox.Show("导出操作完成, 您想打开该Excel文件么?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
        {
            Process.Start(_saveFileDialog.FileName);
        }
    }
                
    #endregion

    private void menu_Delete_Click(object? sender, EventArgs e)
    {
        if (OnDeleteSelected != null)
        {
            OnDeleteSelected(listView1, EventArgs.Empty);
        }
    }

    private void menu_Refresh_Click(object? sender, EventArgs e)
    {
        if (OnRefresh != null)
        {
            OnRefresh(listView1, EventArgs.Empty);
        }
    }

    private void menu_Print_Click(object? sender, EventArgs e)
    {

    }

    private void WinListViewPager_Load(object? sender, EventArgs e)
    {
        if (ContextMenuStrip == null)
        {
            ContextMenuStrip = new ContextMenuStrip();
        }

        for (int i = 0; i < contextMenuStrip1.Items.Count; i++)
        {
            ToolStripItem item = contextMenuStrip1.Items[i];
            ContextMenuStrip.Items.Add(item);
        }
    }
}