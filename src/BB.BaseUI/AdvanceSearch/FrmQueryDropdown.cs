using BB.Tools.Entity;
using DevExpress.XtraEditors;

namespace BB.BaseUI.AdvanceSearch;

internal partial class FrmQueryDropdown : FrmQueryBase
{
    public FrmQueryDropdown()
    {
        InitializeComponent();

        ddlContent.KeyUp += SearchControl_KeyUp;
    }

    /// <summary>
    /// 提供给控件回车执行查询的操作
    /// </summary>
    private void SearchControl_KeyUp(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            DialogResult = DialogResult.OK;
            btnOK_Click(null, null!);
        }
    }

    private void btnClear_Click(object? sender, EventArgs e)
    {
        ProcessDataClear(FieldName);
    }

    private void FrmQueryDropdown_Load(object? sender, EventArgs e)
    {
        lblFieldName.Text = FieldDisplayName;
        if (DropDownItems != null)
        {
            ddlContent.Properties.Items.Clear();
            foreach (CListItem item in DropDownItems)
            {
                ddlContent.Properties.Items.Add(item);
            }
        }
        if (!string.IsNullOrEmpty(FieldDefaultValue))
        {
            SetComboBoxItem(ddlContent, FieldDefaultValue);
        }

        ddlContent.Focus();
    }

    private void btnOK_Click(object? sender, EventArgs e)
    {
        CListItem item = ddlContent.SelectedItem as CListItem;
        if (item != null)
        {
            ReturnValue = item.Value;
            ReturnDisplay = item.Text;
        }

        ProcessDataSearch(null, null);
    }

    /// <summary>
    /// 设置下拉列表选中指定的值
    /// </summary>
    /// <param name="combo">下拉列表</param>
    /// <param name="value">指定的CListItem中的值</param>
    private void SetComboBoxItem(ComboBoxEdit combo, string value)
    {
        for (int i = 0; i < combo.Properties.Items.Count; i++)
        {
            CListItem item = combo.Properties.Items[i] as CListItem;
            if (item != null && item.Value == value)
            {
                combo.SelectedIndex = i;
            }
        }
    }
}