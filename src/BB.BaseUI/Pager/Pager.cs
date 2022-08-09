using System.ComponentModel;
using BB.Tools.Entity;
using BB.Tools.MultiLanuage;
using DevExpress.XtraEditors;

namespace BB.BaseUI.Pager;

public delegate void PageChangedEventHandler(object sender, EventArgs e);
public delegate void ExportCurrentEventHandler(object sender, EventArgs e);
public delegate void ExportAllEventHandler(object sender, EventArgs e);

/// <summary>
/// 分页工具条用户控件，仅提供分页信息显示及改变页码操作
/// </summary>
public class Pager : XtraUserControl
{
    /// <summary>
    /// 页面切换的时候触发的时间
    /// </summary>
    public event PageChangedEventHandler PageChanged;
    public event ExportCurrentEventHandler ExportCurrent;
    public event ExportAllEventHandler ExportAll;

    private int _mPageSize;
    private int _mPageCount;
    private int _mRecordCount;
    private int _mCurrentPageIndex;
    private bool _mShowExportButton = true;//是否显示导出按钮

    private LabelControl _lblPageInfo;
    private TextEdit _txtCurrentPage;
    private SimpleButton _btnFirst;
    private SimpleButton _btnPrevious;
    private SimpleButton _btnNext;
    private SimpleButton _btnLast;
    private SimpleButton _btnExport;
    private SimpleButton _btnExportCurrent;

    private PagerInfo? _pagerInfo;

    /// <summary>
    /// 分页信息
    /// </summary>
    public PagerInfo PagerInfo
    {
        get
        {
            if (_pagerInfo == null)
            {
                _pagerInfo = new PagerInfo
                {
                    TotalRows = RecordCount,
                    PageNo = CurrentPageIndex,
                    PageSize = PageSize
                };

                _pagerInfo.OnPageInfoChanged += pagerInfo_OnPageInfoChanged;
            }
            else
            {
                _pagerInfo.PageNo = CurrentPageIndex;
            }

            return _pagerInfo;
        }
    }

    void pagerInfo_OnPageInfoChanged(PagerInfo info)
    {
        RecordCount = info.TotalRows;
        CurrentPageIndex = info.PageNo;
        PageSize = info.PageSize;

        InitPageInfo();
    }

    /// <summary> 
    /// 必需的设计器变量。
    /// </summary>
    private Container _components = null;
        
    /// <summary> 
    /// 默认构造函数，设置分页初始信息
    /// </summary>
    public Pager()
    {
        InitializeComponent();

        _mPageSize = 50;
        _mRecordCount = 0;
        _mCurrentPageIndex = 1; //默认为第一页
        InitPageInfo();
    }

    /// <summary> 
    /// 带参数的构造函数
    /// <param name="pageSize">每页记录数</param>
    /// <param name="recordCount">总记录数</param>
    /// </summary>
    public Pager(int recordCount, int pageSize)
    {
        InitializeComponent();

        _mPageSize = pageSize;
        _mRecordCount = recordCount;
        _mCurrentPageIndex = 1; //默认为第一页
        InitPageInfo();
    }

    /// <summary> 
    /// 清理所有正在使用的资源。
    /// </summary>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (_components != null)
            {
                _components.Dispose();
            }
        }
        base.Dispose(disposing);
    }
       
    #region 组件设计器生成的代码
    /// <summary> 
    /// 设计器支持所需的方法 - 不要使用代码编辑器 
    /// 修改此方法的内容。
    /// </summary>
    private void InitializeComponent()
    {
        ComponentResourceManager resources = new ComponentResourceManager(typeof(Pager));
        _lblPageInfo = new LabelControl();
        _txtCurrentPage = new TextEdit();
        _btnNext = new SimpleButton();
        _btnFirst = new SimpleButton();
        _btnPrevious = new SimpleButton();
        _btnLast = new SimpleButton();
        _btnExport = new SimpleButton();
        _btnExportCurrent = new SimpleButton();
        ((ISupportInitialize)(_txtCurrentPage.Properties)).BeginInit();
        SuspendLayout();
        // 
        // lblPageInfo
        // 
        _lblPageInfo.Location = new Point(18, 9);
        _lblPageInfo.Name = "_lblPageInfo";
        _lblPageInfo.Size = new Size(213, 14);
        _lblPageInfo.TabIndex = 0;
        _lblPageInfo.Text = "共 {0} 条记录，每页 {1} 条，共 {2} 页";
        // 
        // txtCurrentPage
        // 
        _txtCurrentPage.Anchor = ((AnchorStyles)((AnchorStyles.Top | AnchorStyles.Right)));
        _txtCurrentPage.EditValue = "1";
        _txtCurrentPage.Location = new Point(431, 6);
        _txtCurrentPage.Name = "_txtCurrentPage";
        _txtCurrentPage.Size = new Size(25, 20);
        _txtCurrentPage.TabIndex = 5;
        _txtCurrentPage.KeyDown += txtCurrentPage_KeyDown;
        // 
        // btnNext
        // 
        _btnNext.Anchor = ((AnchorStyles)((AnchorStyles.Top | AnchorStyles.Right)));
        _btnNext.Location = new Point(459, 6);
        _btnNext.Name = "_btnNext";
        _btnNext.Size = new Size(30, 20);
        _btnNext.TabIndex = 6;
        _btnNext.Text = ">";
        _btnNext.Click += btnNext_Click;
        // 
        // btnFirst
        // 
        _btnFirst.Anchor = ((AnchorStyles)((AnchorStyles.Top | AnchorStyles.Right)));
        _btnFirst.Location = new Point(367, 6);
        _btnFirst.Name = "_btnFirst";
        _btnFirst.Size = new Size(30, 20);
        _btnFirst.TabIndex = 7;
        _btnFirst.Text = "|<";
        _btnFirst.Click += btnFirst_Click;
        // 
        // btnPrevious
        // 
        _btnPrevious.Anchor = ((AnchorStyles)((AnchorStyles.Top | AnchorStyles.Right)));
        _btnPrevious.Location = new Point(399, 6);
        _btnPrevious.Name = "_btnPrevious";
        _btnPrevious.Size = new Size(30, 20);
        _btnPrevious.TabIndex = 8;
        _btnPrevious.Text = "<";
        _btnPrevious.Click += btnPrevious_Click;
        // 
        // btnLast
        // 
        _btnLast.Anchor = ((AnchorStyles)((AnchorStyles.Top | AnchorStyles.Right)));
        _btnLast.Location = new Point(491, 6);
        _btnLast.Name = "_btnLast";
        _btnLast.Size = new Size(30, 20);
        _btnLast.TabIndex = 9;
        _btnLast.Text = ">|";
        _btnLast.Click += btnLast_Click;
        // 
        // btnExport
        // 
        _btnExport.Anchor = ((AnchorStyles)((AnchorStyles.Top | AnchorStyles.Right)));
        _btnExport.ImageOptions.Image = ((Image)(resources.GetObject("btnExport.Image")));
        _btnExport.Location = new Point(569, 5);
        _btnExport.Name = "_btnExport";
        _btnExport.Size = new Size(25, 23);
        _btnExport.TabIndex = 11;
        _btnExport.ToolTip = "导出全部页";
        _btnExport.Click += btnExport_Click;
        // 
        // btnExportCurrent
        // 
        _btnExportCurrent.Anchor = ((AnchorStyles)((AnchorStyles.Top | AnchorStyles.Right)));
        _btnExportCurrent.ImageOptions.Image = ((Image)(resources.GetObject("btnExportCurrent.Image")));
        _btnExportCurrent.Location = new Point(539, 5);
        _btnExportCurrent.Name = "_btnExportCurrent";
        _btnExportCurrent.Size = new Size(24, 23);
        _btnExportCurrent.TabIndex = 10;
        _btnExportCurrent.ToolTip = "导出当前页";
        _btnExportCurrent.Click += btnExportCurrent_Click;
        // 
        // Pager
        // 
        Controls.Add(_btnExport);
        Controls.Add(_btnExportCurrent);
        Controls.Add(_btnLast);
        Controls.Add(_btnPrevious);
        Controls.Add(_btnFirst);
        Controls.Add(_btnNext);
        Controls.Add(_txtCurrentPage);
        Controls.Add(_lblPageInfo);
        Cursor = Cursors.Hand;
        Name = "Pager";
        Size = new Size(606, 32);
        ((ISupportInitialize)(_txtCurrentPage.Properties)).EndInit();
        ResumeLayout(false);
        PerformLayout();

    }
    #endregion

    /// <summary>
    /// 页面变化处理
    /// </summary>
    /// <param name="e"></param>
    protected virtual void OnPageChanged(EventArgs e)
    {
        if (PageChanged != null)
        {
            PageChanged(this, e);
        }
    }

    /// <summary>
    /// 是否显示导出按钮
    /// </summary>
    [Description("是否显示导出按钮。"), DefaultValue(true), Category("分页")]
    public bool ShowExportButton
    {
        get => _mShowExportButton;
        set
        {
            _mShowExportButton = value;
            _btnExport.Visible = value;
            _btnExportCurrent.Visible = value;
        }
    }

    /// <summary>
    /// 设置或获取一页中显示的记录数目
    /// </summary>
    [Description("设置或获取一页中显示的记录数目"), DefaultValue(50), Category("分页")]
    public int PageSize
    {
        set => _mPageSize = value;
        get => _mPageSize;
    }
        
    /// <summary>
    /// 获取记录总页数
    /// </summary>
    [Description("获取记录总页数"), DefaultValue(0), Category("分页")]
    public int PageCount => _mPageCount;

    /// <summary>
    /// 设置或获取记录总数
    /// </summary>
    [Description("设置或获取记录总数"), Category("分页")]
    public int RecordCount
    {
        set => _mRecordCount = value;
        get => _mRecordCount;
    }
        
    /// <summary>
    /// 当前的页面索引, 开始为1
    /// </summary>
    [Description("当前的页面索引, 开始为1"), DefaultValue(0), Category("分页")]
    [Browsable(false)]
    public int CurrentPageIndex
    {
        set => _mCurrentPageIndex = value;
        get => _mCurrentPageIndex;
    }

    /// <summary>
    /// 初始化分页信息
    /// </summary>
    /// <param name="info"></param>
    public void InitPageInfo(PagerInfo info)
    {
        _mRecordCount = info.TotalRows;
        _mPageSize = info.PageSize;
        InitPageInfo();
    }
        
    /// <summary> 
    /// 初始化分页信息
    /// <param name="pageSize">每页记录数</param>
    /// <param name="recordCount">总记录数</param>
    /// </summary>
    public void InitPageInfo(int recordCount, int pageSize)
    {
        _mRecordCount = recordCount;
        _mPageSize = pageSize;
        InitPageInfo();
    }
        
    /// <summary> 
    /// 初始化分页信息
    /// <param name="recordCount">总记录数</param>
    /// </summary>
    public void InitPageInfo(int recordCount)
    {
        _mRecordCount = recordCount;
        InitPageInfo();
    }
        
    /// <summary> 
    /// 初始化分页信息
    /// </summary>
    public void InitPageInfo()
    {
        if (_mPageSize < 1)
            _mPageSize = 10; //如果每页记录数不正确，即更改为10
        if (_mRecordCount < 0)
            _mRecordCount = 0; //如果记录总数不正确，即更改为0

        //取得总页数
        if (_mRecordCount % _mPageSize == 0)
        {
            _mPageCount = _mRecordCount / _mPageSize;
        }
        else
        {
            _mPageCount = _mRecordCount / _mPageSize + 1;
        }

        //设置当前页
        if (_mCurrentPageIndex > _mPageCount)
        {
            _mCurrentPageIndex = _mPageCount;
        }
        if (_mCurrentPageIndex < 1)
        {
            _mCurrentPageIndex = 1;
        }

        //设置按钮的可用性
        bool enable = (CurrentPageIndex > 1);
        _btnPrevious.Enabled = enable;

        enable = (CurrentPageIndex < PageCount);
        _btnNext.Enabled = enable;

        _txtCurrentPage.Text = _mCurrentPageIndex.ToString();
        var format = JsonLanguage.Default.GetString("第 {3} 页，每页 {1} 条，共 {2} 页， {0} 条记录");
        _lblPageInfo.Text = string.Format(format, _mRecordCount, _mPageSize, _mPageCount, _mCurrentPageIndex);

        _btnExport.Enabled = ShowExportButton;
        _btnExportCurrent.Enabled = ShowExportButton;
    }

    /// <summary>
    /// 刷新页面数据
    /// </summary>
    /// <param name="page">页码</param>
    public void RefreshData(int page)
    {
        _mCurrentPageIndex = page;
        EventArgs e = EventArgs.Empty;
        OnPageChanged(e);
    }
        
    private void btnFirst_Click(object sender, EventArgs e)
    {
        RefreshData(1);
    }
        
    private void btnPrevious_Click(object sender, EventArgs e)
    {
        if (_mCurrentPageIndex > 1)
        {
            RefreshData(_mCurrentPageIndex - 1);
        }
        else
        {
            RefreshData(1);
        }
    }

    private void btnNext_Click(object sender, EventArgs e)
    {
        if (_mCurrentPageIndex < _mPageCount)
        {
            RefreshData(_mCurrentPageIndex + 1);
        }
        else if (_mPageCount < 1)
        {
            RefreshData(1);
        }
        else
        {
            RefreshData(_mPageCount);
        }
    }
        
    private void btnLast_Click(object sender, EventArgs e)
    {
        if (_mPageCount > 0)
        {
            RefreshData(_mPageCount);
        }
        else
        {
            RefreshData(1);
        }
    }
        
    private void txtCurrentPage_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            int num;
            try
            {
                num = Convert.ToInt16(_txtCurrentPage.Text);
            }
            catch (Exception ex)
            {
                num = 1;
            }

            if (num > _mPageCount)
                num = _mPageCount;
            if (num < 1)
                num = 1;

            RefreshData(num);
        }
    }

    private void btnExportCurrent_Click(object sender, EventArgs e)
    {
        if (ExportCurrent != null)
        {
            ExportCurrent(sender, e);
        }
    }

    private void btnExport_Click(object sender, EventArgs e)
    {
        if (ExportAll != null)
        {
            ExportAll(sender, e);
        }
    }
}