using BB.BaseUI.BaseUI;
using BB.BaseUI.Extension;
using BB.Entity.Security;
using BB.HttpServices.Core.Function;
using Furion.LinqBuilder;

namespace BB.Security.UI;

public partial class FrmAddMoreFunction : BaseForm
{
    private readonly FunctionHttpService _bll;

    public FrmAddMoreFunction(FunctionHttpService bll)
    {
        InitializeComponent();

        _bll = bll;

        functionControl1.EditValueChanged += functionControl1_EditValueChanged;

        InitDictItem();
    }

    void functionControl1_EditValueChanged(object? sender, EventArgs e)
    {
        string item = functionControl1.Value;
        if (!string.IsNullOrEmpty(item) && item == "-1")
        {
            SetSystemTypeVisible(true);
        }
        else
        {
            SetSystemTypeVisible(false);
        }
    }


    private void SetSystemTypeVisible(bool visible)
    {
        txtSystemType.Visible = visible;
        lblSystemType.Visible = visible;
    }

    private void InitDictItem()
    {

    }

    public void SetUpperFunction(string value)
    {
        functionControl1.Value = value;
    }

    private async void btnSave_Click(object? sender, EventArgs e)
    {
        if (functionControl1.Text.Length == 0)
            return;

        #region 验证用户输入

        if (txtName.Text == "")
        {
            "功能名称不能为空".ShowUxTips();
            txtName.Focus();
            return;
        }
        else if (txtFunctionID.Text == "")
        {
            "功能ID不能为空".ShowUxTips();
            txtFunctionID.Focus();
            return;
        }
        else if (txtSystemType.Visible && txtSystemType.Text.Length == 0)
        {
            "系统类型编号不能为空".ShowUxTips();
            txtSystemType.Focus();
            return;
        }

        #endregion

        string pid = functionControl1.Value;
        FunctionInfo functionInfo = await _bll.FindByIdAsync(pid);

        if (functionInfo.SystemTypeId.IsNullOrEmpty())
        {
            functionInfo.SystemTypeId = txtSystemType.Text;
        }

        FunctionInfo mainInfo = new FunctionInfo();
        mainInfo = SetFunction(mainInfo);
        mainInfo.SystemTypeId = functionInfo.SystemTypeId; //和父节点的SystemType_ID一样。

        if (await _bll.AddMore(mainInfo, chkAdd.Checked, chkModify.Checked, chkDelete.Checked, chkExport.Checked,
                chkImport.Checked, chkView.Checked))
        {
            ProcessDataSaved(btnSave, EventArgs.Empty);

            //this.DialogResult = System.Windows.Forms.DialogResult.OK;
            "保存成功".ShowUxTips();
        }
        else
        {
            "保存失败".ShowUxTips();
        }
    }

    private FunctionInfo SetFunction(FunctionInfo info)
    {
        info.Name = txtName.Text;
        info.PID = functionControl1.Value;
        info.ControlId = txtFunctionID.Text;
        return info;
    }
}