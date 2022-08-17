using BB.BaseUI.Extension;
using BB.BaseUI.Print;
using BB.Tools.MultiLanuage;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;
using FluentValidation.Results;

namespace BB.BaseUI.BaseUI;

/// <summary>
/// 编辑界面基类
/// </summary>
public partial class BaseEditForm : BaseForm
{
    /// <summary>
    /// 是否使用动作前缀：编辑、新增等，默认为True
    /// </summary>
    public bool UseActionPrefix { get; set; }

    /// <summary>
    /// 错误管理器
    /// </summary>
    protected DXErrorProvider ErrorProvider;

    public BaseEditForm()
    {
        InitializeComponent();

        ErrorProvider = new DXErrorProvider();

        InitValue();
        dataNavigator1.PositionChanged += dataNavigator1_PositionChanged;

        Shown += BaseEditForm_Shown;
    }

    /// <summary>
    /// 初始化变量
    /// </summary>
    private void InitValue()
    {
        ID = string.Empty;  // 记录主键
        IdList = new List<string>();
        UseActionPrefix = true;
    }

    /// <summary>
    /// 设置 this.dataNavigator1.CurrentIndex 后触发
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void dataNavigator1_PositionChanged(object sender, EventArgs e)
    {
        ID = IdList[dataNavigator1.CurrentIndex];
        DisplayData();

        // 启动时用此方法标识必填项
        CheckInput();
    }

    /// <summary>
    /// 加载窗体的事件
    /// </summary>
    public override void FormOnLoad()
    {
        base.FormOnLoad();
        if (!DesignMode)
        {
            if (!string.IsNullOrEmpty(ID))
            {
                if (UseActionPrefix && !Text.Contains("编辑"))
                {
                    Text = $"{JsonLanguage.Default.GetString("编辑")} {JsonLanguage.Default.GetString(Text)}";
                }
                btnAdd.Visible = false;//如果是编辑，则屏蔽添加按钮
            }
            else
            {
                if (UseActionPrefix && !Text.Contains("新建"))
                {
                    Text = $"{JsonLanguage.Default.GetString("新建")} {JsonLanguage.Default.GetString(Text)}";
                }
            }
        }

        #region 表单容器设置

        layoutControl1.Appearance.ControlReadOnly.BackColor = Color.SeaShell;
        layoutControl1.Appearance.ControlReadOnly.Options.UseBackColor = true;

        #endregion
    }

    private void BaseEditForm_Shown(object sender, EventArgs e)
    {
        dataNavigator1.IdList = IdList;
        dataNavigator1.CurrentIndex = IdList.IndexOf(ID);
        if (IdList == null || IdList.Count == 0)
        {
            dataNavigator1.Visible = false;
            DisplayData(); //CurrentIndex = -1的时候需要主动调用

            // 启动时用此方法标识必填项
            CheckInput();
        }

        //由于上面设置this.dataNavigator1.CurrentIndex，导致里面触发dataNavigator1_PositionChanged
        //从而调用了DisplayData，所以下面的代码不用重复调用，否则执行了两次。
        //DisplayData();
    }

    private void BaseEditForm_Load(object sender, EventArgs e)
    {
    }
 
    /// <summary>
    /// 显示数据到控件上
    /// </summary>
    public virtual async Task DisplayData()
    {
        throw new NotSupportedException("无法直接调用基类方法，请在派生类中实现该方法");
    }
        
    /// <summary>
    /// 检查输入的有效性
    /// </summary>
    /// <returns>有效</returns>
    public virtual async Task<bool> CheckInput()
    {
        return await Task.FromResult(true);
    }
               
    /// <summary>
    /// 清除屏幕
    /// </summary>
    public virtual async Task ClearScreen()
    {
        ID = "";////需要设置为空，表示新增
        ClearControlValue(this);
        FormOnLoad();
    }

    /// <summary>
    /// 清除容器里面某些控件的值
    /// </summary>
    /// <param name="ctrl">容器类控件</param>
    public void ClearControlValue(System.Windows.Forms.Control ctrl)
    {
        ClearPanelEditValue(ctrl);
        //ClearSinglelValue(ctrl);
        //if (ctrl.Controls.Count > 0)
        //{
        //    // 如果是容器类控件，递归调用自己
        //    foreach (System.Windows.Forms.Control control in ctrl.Controls)
        //    {
        //        ClearControlValue(control);
        //    }
        //}
    }

    /// <summary>
    /// 清除容器里面某些控件的值
    /// </summary>
    /// <param name="panels">控件或者Panel</param>
    public virtual void ClearPanelEditValue(params System.Windows.Forms.Control[] panels)
    {
        for (int i = 0; i < panels.Length; i++)
        {
            System.Windows.Forms.Control panel = panels[i];
            foreach (System.Windows.Forms.Control c in panel.Controls)
            {
                if (c is BaseEdit)
                {
                    (c as BaseEdit).EditValue = null;
                }
                ClearPanelEditValue(c);
            }
        }
    }

    /// <summary>
    /// 清除容器里面某些控件的值（不用）
    /// </summary>
    /// <param name="ctrl">容器类控件</param>
    public virtual void ClearSinglelValue(System.Windows.Forms.Control ctrl)
    {
        switch (ctrl.GetType().Name)
        {
            case "TextEdit":
            case "MemoEdit":
            case "MemoExEdit":
                ctrl.Text = "";
                break;

            case "SpinEdit":
                ((SpinEdit)ctrl).Value = 0M;
                break;

            case "CheckEdit":
                ((CheckEdit)ctrl).Checked = false;
                break;

            case "ComboBoxEdit":
                ((ComboBoxEdit)ctrl).Text = "";
                break;
            case "SearchLookUpEdit":
                ((SearchLookUpEdit)ctrl).Text = "";
                break;
            case "GridLookUpEdit":
                ((GridLookUpEdit)ctrl).Text = "";
                break;
        }
    }
                
    /// <summary>
    /// 保存数据（新增和编辑的保存）
    /// </summary>
    public virtual async Task<bool> SaveEntity()
    {
        bool result = false;
        if(!string.IsNullOrEmpty(ID))
        {
            //编辑的保存
            result = await SaveUpdated();
        }
        else
        {
            //新增的保存
            result = await SaveAddNew();
        }

        return result;
    }

    /// <summary>
    /// 更新已有的数据
    /// </summary>
    /// <returns></returns>
    public virtual async Task<bool> SaveUpdated()
    {
        return await Task.FromResult(true);
    }

    /// <summary>
    /// 保存新增的数据
    /// </summary>
    /// <returns></returns>
    public virtual async Task<bool> SaveAddNew()
    {
        return true;
    }

    /// <summary>
    /// 保存操作的统一入口
    /// </summary>
    /// <param name="close">关闭窗体</param>
    public virtual async Task SaveEntity(bool close)
    {
        ShowWaitForm();
        WaitForm.SetWaitFormDescription("校验数据...");
        // 检查输入的有效性
        if (!await CheckInput())
        {
            "输入的数据无效，请检查！".ShowErrorTip(this);
            HideWaitForm();
            // "输入的数据无效，请检查！".ShowUxError();
            return;
        }

        // 设置鼠标繁忙状态
        Cursor = Cursors.WaitCursor;
        WaitForm.SetWaitFormDescription("保存数据...");
        try
        {
            if (await SaveEntity())
            {
                "保存成功！".ShowSuccessTip(this);
                // "保存成功".ShowUxTips();
                if (close)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    await ClearScreen();
                }
                
                // 数据保存后事件
                ProcessDataSaved(btnOK, EventArgs.Empty);
            }
            else
            {
                "保存失败！".ShowErrorTip(this);
                // "保存失败！".ShowUxError();
            }
        }
        catch (Exception ex)
        {
            HideWaitForm();
            ProcessException(ex);
        }
        finally
        {
            // 设置鼠标默认状态
            Cursor = Cursors.Default;
            HideWaitForm();
        }
    }

    /// <summary>
    /// 统一处理验证结果
    /// </summary>
    /// <param name="result"></param>
    /// <returns></returns>
    public virtual async Task<bool> ProcessValidationResults(ValidationResult result)
    {
        ErrorProvider.ClearErrors();
        if (!result.IsValid)
        {
            result.Errors.ForEach(x =>
            {
                var c = Controls.Find($"txt{x.PropertyName}", true);
                if (c.Length > 0)
                {
                    x.ErrorMessage.ShowErrorTip(c[0]);
                    ErrorProvider.SetError(c[0], x.ErrorMessage);
                }
            });
        }

        return await Task.FromResult(result.IsValid);
    }
    private async void btnAdd_Click(object sender, EventArgs e)
    {
        await SaveEntity(false);
    }

    private async void btnOK_Click(object sender, EventArgs e)
    {
        await SaveEntity(true);
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }

    private void picPrint_Click(object sender, EventArgs e)
    {
        PrintFormHelper.Print(this);
    }

    /// <summary>
    /// 处理输入的按键
    /// </summary>
    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
        if (keyData == Keys.F5)
        {
            FormOnLoad();
        }

        if (!(ActiveControl is Button))
        {
            if (keyData == Keys.Down || keyData == Keys.Enter)
            {
                return SelectNextControl(ActiveControl, true, true, true, true);
            }
            else if (keyData == Keys.Up)
            {
                return SelectNextControl(ActiveControl, false, true, true, true);
            }
            //if (keyData == Keys.Enter)
            //{
            //    System.Windows.Forms.SendKeys.Send("{TAB}");
            //    return true;
            //}
            //if (keyData == Keys.Down)
            //{
            //    System.Windows.Forms.SendKeys.Send("{TAB}");
            //}
            //else
            //{
            //    SendKeys.Send("+{Tab}");
            //}

            return false;
        }
        else
        {
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}