using BB.BaseUI.BaseUI;
using BB.BaseUI.Extension;
using BB.Entity.Security;
using BB.HttpServices.Core.OperationLog;
using Furion.Logging.Extensions;

namespace BB.Security.UI;

public partial class FrmEditOperationLog : BaseEditForm
{
    /// <summary>
    /// 创建一个临时对象，方便在附件管理中获取存在的GUID
    /// </summary>
    private OperationLogInfo _tempInfo = new OperationLogInfo();

    private readonly OperationLogHttpService _bll;

    public FrmEditOperationLog(OperationLogHttpService bll)
    {
        InitializeComponent();
        _bll = bll;
    }

    /// <summary>
    /// 实现控件输入检查的函数
    /// </summary>
    /// <returns></returns>
    public override Task<bool> CheckInput()
    {
        bool result = true;//默认是可以通过

        #region MyRegion
        #endregion

        return Task.FromResult(result);
    }

    /// <summary>
    /// 初始化数据字典
    /// </summary>
    private void InitDictItem()
    {
        //初始化代码
    }

    /// <summary>
    /// 数据显示的函数
    /// </summary>
    public override async Task DisplayData()
    {
        InitDictItem();//数据字典加载（公用）

        if (!string.IsNullOrEmpty(ID))
        {
            #region 显示信息
            OperationLogInfo info = await _bll.FindByIdAsync(ID);
            if (info != null)
            {
                _tempInfo = info;//重新给临时对象赋值，使之指向存在的记录对象
                txtUser_ID.Text = info.UserId;
                txtLoginName.Text = info.LoginName;
                txtFullName.Text = info.FullName;
                txtCompany_ID.Text = info.CompanyId;
                txtCompanyName.Text = info.CompanyName;
                txtTableName.Text = info.TableName;
                txtOperationType.Text = info.OperationType;
                txtNote.Text = info.Note;
                txtIPAddress.Text = info.IPAddress;
                txtMacAddress.Text = info.MacAddress;
                txtCreationDate.SetDateTime(info.CreationDate);
            }
            #endregion
            //this.btnOK.Enabled = GB.HasFunction("OperationLog/Edit");             
        }
        else
        {
            //this.btnOK.Enabled = GB.HasFunction("OperationLog/Add");  
        }
    }

    public override async Task ClearScreen()
    {
        _tempInfo = new OperationLogInfo();
        await base.ClearScreen();
    }

    /// <summary>
    /// 编辑或者保存状态下取值函数
    /// </summary>
    /// <param name="info"></param>
    private void SetInfo(OperationLogInfo info)
    {
        info.LoginName = txtLoginName.Text;
        info.FullName = txtFullName.Text;
        info.CompanyName = txtCompanyName.Text;
        info.TableName = txtTableName.Text;
        info.OperationType = txtOperationType.Text;
        info.Note = txtNote.Text;
        info.MacAddress = txtMacAddress.Text;
        info.IPAddress = txtIPAddress.Text;
        info.CreationDate = txtCreationDate.DateTime;
    }

    /// <summary>
    /// 新增状态下的数据保存
    /// </summary>
    /// <returns></returns>
    public override async Task<bool> SaveAddNew()
    {
        OperationLogInfo info = _tempInfo;//必须使用存在的局部变量，因为部分信息可能被附件使用
        SetInfo(info);

        try
        {
            #region 新增数据

            bool succeed = await _bll.InsertAsync(info);
            if (succeed)
            {
                //可添加其他关联操作

                return true;
            }
            #endregion
        }
        catch (Exception ex)
        {
            ex.ToString().LogError();
            ex.Message.ShowUxError();
        }
        return false;
    }

    /// <summary>
    /// 编辑状态下的数据保存
    /// </summary>
    /// <returns></returns>
    public override async Task<bool> SaveUpdated()
    {

        OperationLogInfo info = await _bll.FindByIdAsync(ID);
        if (info != null)
        {
            SetInfo(info);

            try
            {
                #region 更新数据
                bool succeed = await _bll.UpdateAsync(info);
                if (succeed)
                {
                    //可添加其他关联操作

                    return true;
                }
                #endregion
            }
            catch (Exception ex)
            {
                ex.ToString().LogError();
                ex.Message.ShowUxError();
            }
        }
        return false;
    }
}