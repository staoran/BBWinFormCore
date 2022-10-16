namespace BB.BaseUI.AdvanceSearch;

internal partial class FrmQueryDateEdit : FrmQueryBase
{
    public FrmQueryDateEdit()
    {
        InitializeComponent();

        dtStart.KeyUp += SearchControl_KeyUp;
        dtStart.KeyUp += SearchControl_KeyUp;
    }

    /// <summary>
    /// 提供给控件回车执行查询的操作
    /// </summary>
    private void SearchControl_KeyUp(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            DialogResult = DialogResult.OK;
            btnOK_Click(null, null);
        }
    }

    private void btnClear_Click(object? sender, EventArgs e)
    {
        ProcessDataClear(FieldName);
    }

    private void btnOK_Click(object? sender, EventArgs e)
    {
        //判断输入的内容是否为空，来决定是否匹配日期
        if (dtStart.Text.Length > 0)
        {
            ReturnDisplay = $"{dtStart.DateTime.ToString("yyyy-MM-dd")}";
            ReturnValue = $"{dtStart.DateTime.ToString("yyyy-MM-dd")}";
        }
        if(dtEnd.Text.Length > 0)
        {
            ReturnDisplay += $" ~ {dtEnd.DateTime.ToString("yyyy-MM-dd")}";
            ReturnValue += $" ~ {dtEnd.DateTime.ToString("yyyy-MM-dd")}";
        }            

        ProcessDataSearch(null, null);
    }

    private void FrmQueryDateEdit_Load(object? sender, EventArgs e)
    {
        lblFieldName.Text = FieldDisplayName;
        if (!string.IsNullOrEmpty(FieldDefaultValue))
        {
            string[] itemArray = FieldDefaultValue.Split('~');
            if (itemArray != null)
            {
                DateTime value;
                bool result = false;

                if (itemArray.Length > 0)
                {
                    result = DateTime.TryParse(itemArray[0].Trim(), out value);
                    if (result)
                    {
                        dtStart.DateTime = value;
                    }
                }
                if (itemArray.Length > 1)
                {
                    result = DateTime.TryParse(itemArray[1].Trim(), out value);
                    if (result)
                    {
                        dtEnd.DateTime = value;
                    }
                }
            }
        }
        dtStart.Focus();
    }
}