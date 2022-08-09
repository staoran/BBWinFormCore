namespace BB.BaseUI.BaseUI;

/// <summary>
/// 位置变化代理
/// </summary>
public delegate void PostionChangedEventHandler(object sender, EventArgs e);

/// <summary>
/// 记录导航控件
/// </summary>
public partial class DataNavigator : DevExpress.XtraEditors.XtraUserControl
{
    /// <summary>
    /// 位置变化事件处理
    /// </summary>
    public event PostionChangedEventHandler PositionChanged;
    private int _mCurrentIndex = 0;//当前的位置
    /// <summary>
    /// 用来导航的当前页面的ID列表
    /// </summary>
    public List<string> IdList = new List<string>();

    /// <summary>
    /// 获取或设置索引值
    /// </summary>
    public int CurrentIndex
    {
        get => _mCurrentIndex;
        set 
        {
            _mCurrentIndex = value;
            ChangePosition(value);
        }
    }
    /// <summary>
    /// 默认构造函数
    /// </summary>
    public DataNavigator()
    {
        InitializeComponent();            
    }

    private void btnFirst_Click(object sender, EventArgs e)
    {
        ChangePosition(0);
    }

    private void btnPrevious_Click(object sender, EventArgs e)
    {
        ChangePosition(_mCurrentIndex - 1);
    }

    private void btnNext_Click(object sender, EventArgs e)
    {
        ChangePosition(_mCurrentIndex + 1);
    }

    private void btnLast_Click(object sender, EventArgs e)
    {
        ChangePosition(IdList.Count - 1);
    }

    private void EnableControl(bool enable)
    {
        btnFirst.Enabled = enable;
        btnLast.Enabled = enable;
        btnNext.Enabled = enable;
        btnPrevious.Enabled = enable;
    }

    private void ChangePosition(int newPos)
    {
        int count = IdList.Count;
        if (count == 0)
        {
            EnableControl(false);
            txtInfo.Text = "";
        }
        else
        {
            EnableControl(true);

            newPos = (newPos < 0) ? 0 : newPos;
            _mCurrentIndex = ((count - 1) > newPos) ? newPos : (count - 1);
            btnPrevious.Enabled = (_mCurrentIndex > 0);
            btnNext.Enabled = _mCurrentIndex < (count - 1);
            txtInfo.Text = $"{_mCurrentIndex + 1}/{count}";

            if (PositionChanged != null)
            {
                PositionChanged(this, EventArgs.Empty);
            }
        }
    }

    private void DataNavigator_Load(object sender, EventArgs e)
    {
    }
}