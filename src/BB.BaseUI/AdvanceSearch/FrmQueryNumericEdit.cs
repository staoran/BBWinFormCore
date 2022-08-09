namespace BB.BaseUI.AdvanceSearch;

internal partial class FrmQueryNumericEdit : FrmQueryBase
{
    public FrmQueryNumericEdit()
    {
        InitializeComponent();

        txtStart.KeyUp += SearchControl_KeyUp;
        txtEnd.KeyUp += SearchControl_KeyUp;

        //设置最大
        txtStart.Maximum = Decimal.MaxValue;
        txtEnd.Maximum = Decimal.MaxValue;
    }

    /// <summary>
    /// 提供给控件回车执行查询的操作
    /// </summary>
    private void SearchControl_KeyUp(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            DialogResult = DialogResult.OK;
            btnOK_Click(null, null);
        }
    }

    private void btnClear_Click(object sender, EventArgs e)
    {
        ProcessDataClear(FieldName);
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
        //判断输入的内容是否为空，来决定是否匹配日期
        if (txtStart.Text.Length > 0)
        {
            ReturnDisplay = $"{txtStart.Value}";
            ReturnValue = $"{txtStart.Value}";
        }
        if (txtEnd.Text.Length > 0)
        {
            ReturnDisplay += $" ~ {txtEnd.Value}";
            ReturnValue += $" ~ {txtEnd.Value}";
        }

        ProcessDataSearch(null, null);
    }

    private void FrmQueryNumericEdit_Load(object sender, EventArgs e)
    {
        txtStart.Text = "";
        txtEnd.Text = "";

        lblFieldName.Text = FieldDisplayName;
        if (!string.IsNullOrEmpty(FieldDefaultValue))
        {
            string[] itemArray = FieldDefaultValue.Split('~');
            if (itemArray != null)
            {   
                decimal value = 0M;
                bool result = false;

                if (itemArray.Length > 0)
                {
                    result = decimal.TryParse(itemArray[0].Trim(), out value);
                    if (result)
                    {
                        txtStart.Value = value;
                    }
                }
                if (itemArray.Length > 1)
                {
                    result = decimal.TryParse(itemArray[1].Trim(), out value);
                    if (result)
                    {
                        txtEnd.Value = value;
                    }
                }
            }
        }

        txtStart.Focus();
    }
}