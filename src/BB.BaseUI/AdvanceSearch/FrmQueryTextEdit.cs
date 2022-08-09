namespace BB.BaseUI.AdvanceSearch;

internal partial class FrmQueryTextEdit : FrmQueryBase
{
    public FrmQueryTextEdit()
    {
        InitializeComponent();

        txtContent.KeyUp += SearchControl_KeyUp;
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

    private void btnCopy_Click(object sender, EventArgs e)
    {
        if (txtContent.Text.Length > 0)
        {
            Clipboard.SetText(txtContent.Text);
        }
    }
    private void btnPaste_Click(object sender, EventArgs e)
    {
        txtContent.Text = Clipboard.GetText();
    }

    private void FrmQueryTextEdit_Load(object sender, EventArgs e)
    {
        lblFieldName.Text = FieldDisplayName;
        if (!string.IsNullOrEmpty(FieldDefaultValue))
        {
            txtContent.Text = FieldDefaultValue;
        }
        txtContent.Focus();
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
        ReturnValue = txtContent.Text.Trim();
        ReturnDisplay = txtContent.Text.Trim();

        ProcessDataSearch(null, null);
    }

}