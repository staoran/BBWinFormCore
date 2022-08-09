using DevExpress.XtraWaitForm;

namespace BB.BaseUI.BaseUI;

/// <summary>
/// 等待提示窗体
/// </summary>
public partial class FrmWaitForm : WaitForm
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public FrmWaitForm()
    {
        InitializeComponent();
        progressPanel1.AutoHeight = true;
    }

    #region Overrides

    /// <summary>
    /// 设置标题
    /// </summary>
    /// <param name="caption"></param>
    public override void SetCaption(string caption)
    {
        base.SetCaption(caption);
        progressPanel1.Caption = caption;
    }

    /// <summary>
    /// 设置正文内容
    /// </summary>
    /// <param name="description"></param>
    public override void SetDescription(string description)
    {
        base.SetDescription(description);
        progressPanel1.Description = description;
    }

    /// <summary>
    /// 处理命令
    /// </summary>
    /// <param name="cmd"></param>
    /// <param name="arg"></param>
    public override void ProcessCommand(Enum cmd, object arg)
    {
        base.ProcessCommand(cmd, arg);
    }

    #endregion

    public enum WaitFormCommand
    {
    }
}