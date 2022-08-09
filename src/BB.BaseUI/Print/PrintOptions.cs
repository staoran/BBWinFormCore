using BB.BaseUI.Other;

namespace BB.BaseUI.Print;

/// <summary>
/// 打印选项
/// </summary>
public partial class PrintOptions : Form
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public PrintOptions()
    {
        InitializeComponent();
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="availableFields">可见字段列表</param>
    public PrintOptions(List<string> availableFields)
    {
        InitializeComponent();

        foreach (string field in availableFields)
            chklst.Items.Add(field, true);
    }

    private void PrintOtions_Load(object sender, EventArgs e)
    {
        // 初始化
        rdoAllRows.Checked = true;
        chkFitToPageWidth.Checked = true; 
        txtTitle.Text = PrintTitle;

        //多语言支持
        if (!DesignMode)
        {
            LanguageHelper.InitLanguage(this);
        }
    }
        
    /// <summary>
    /// 设置选定项目
    /// </summary>
    /// <param name="items"></param>
    public void SetCheckedItems(string[] items)
    {
        for (int i = 0; i < chklst.Items.Count; i++)
        {
            chklst.SetItemChecked(i, false);
            foreach (string item in items)
            {
                if (item == chklst.Items[i].ToString())
                {
                    chklst.SetItemChecked(i, true);
                }
            }
        }
    }

    /// <summary>
    /// 获取用户选定的项目内容
    /// </summary>
    /// <returns></returns>
    public List<string> GetCheckItems()
    {
        List<string> list = new List<string>();
        for (int i = 0; i < chklst.Items.Count; i++)
        {
            if (chklst.GetItemChecked(i))
            {
                list.Add(chklst.Items[i].ToString());
            }
        }
        return list;
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.OK;
        Close();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }

    /// <summary>
    /// 获取选择的列
    /// </summary>
    /// <returns></returns>
    public List<string> GetSelectedColumns()
    {
        List<string> lst = new List<string>();
        foreach (object item in chklst.CheckedItems)
            lst.Add(item.ToString());
        return lst;
    }

    /// <summary>
    /// 打印标题
    /// </summary>
    public string PrintTitle
    {
        get => txtTitle.Text;
        set => txtTitle.Text = value;
    }

    /// <summary>
    /// 是否打印所有行
    /// </summary>
    public bool PrintAllRows => rdoAllRows.Checked;

    /// <summary>
    /// 是否适宽
    /// </summary>
    public bool FitToPageWidth => chkFitToPageWidth.Checked;
}