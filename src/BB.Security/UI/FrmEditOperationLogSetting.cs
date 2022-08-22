using BB.BaseUI.BaseUI;
using BB.BaseUI.Extension;
using BB.BaseUI.Other;
using BB.Entity.Security;
using BB.HttpServices.Core.FieldControlConfig;
using BB.HttpServices.Core.OperationLogSetting;
using Furion.Logging.Extensions;

namespace BB.Security.UI;

public partial class FrmEditOperationLogSetting : BaseEditForm
{
    /// <summary>
    /// 创建一个临时对象，方便在附件管理中获取存在的GUID
    /// </summary>
    private OperationLogSettingInfo _tempInfo = new OperationLogSettingInfo();

    private readonly OperationLogSettingHttpService _bll;
    private readonly FieldControlConfigHttpService _fieldControlConfigBll;

    public FrmEditOperationLogSetting(OperationLogSettingHttpService bll, FieldControlConfigHttpService fieldControlConfigBll)
    {
        InitializeComponent();
        _bll = bll;
        _fieldControlConfigBll = fieldControlConfigBll;
    }

    /// <summary>
    /// 实现控件输入检查的函数
    /// </summary>
    /// <returns></returns>
    public override Task<bool> CheckInput()
    {
        bool result = true;//默认是可以通过

        #region MyRegion
        if (txtTableName.Text.Trim().Length == 0)
        {
            "请输入数据库表".ShowUxTips();
            txtTableName.Focus();
            result = false;
        }
        #endregion

        return Task.FromResult(result);
    }

    /// <summary>
    /// 初始化数据字典
    /// </summary>
    private async Task InitDictItem()
    {
        //初始化代码
        txtTableName.Properties.BeginUpdate();
        txtTableName.Properties.Items.Clear();

        List<string> tableList = await _fieldControlConfigBll.GetTableNames();
        txtTableName.Properties.Items.AddRange(tableList.ToArray());
        txtTableName.Properties.EndUpdate();
    }

    /// <summary>
    /// 数据显示的函数
    /// </summary>
    public override async Task DisplayData()
    {
        await InitDictItem();//数据字典加载（公用）

        if (!string.IsNullOrEmpty(ID))
        {
            #region 显示信息
            OperationLogSettingInfo info = await _bll.FindByIdAsync(ID);
            if (info != null)
            {
                _tempInfo = info;//重新给临时对象赋值，使之指向存在的记录对象

                txtForbid.Checked = info.Forbid;
                txtTableName.Text = info.TableName;
                txtInsertLog.Checked = info.InsertLog;
                txtDeleteLog.Checked = info.DeleteLog;
                txtUpdateLog.Checked = info.UpdateLog;
                txtNote.Text = info.Note;
                txtCreator.Text = info.Creator;
                txtCreationDate.SetDateTime(info.CreationDate);
                txtEditor.Text = info.Editor;
                txtLastUpdateDate.SetDateTime(info.LastUpdateDate);
            }
            #endregion
            //this.btnOK.Enabled = GB.HasFunction("OperationLogSetting/Edit");             
        }
        else
        {
            txtCreationDate.DateTime = DateTime.Now; //默认当前时间
            txtCreator.Text = GB.LoginUserInfo.FullName;//默认为当前登录用户
            txtEditor.Text = GB.LoginUserInfo.FullName;//默认为当前登录用户
            txtLastUpdateDate.DateTime = DateTime.Now; //默认当前时间
            //this.btnOK.Enabled = GB.HasFunction("OperationLogSetting/Add");  
        }
    }


    public override async Task ClearScreen()
    {
        _tempInfo = new OperationLogSettingInfo();
        await base.ClearScreen();
    }

    /// <summary>
    /// 编辑或者保存状态下取值函数
    /// </summary>
    /// <param name="info"></param>
    private void SetInfo(OperationLogSettingInfo info)
    {
        info.Forbid = txtForbid.Checked;
        info.TableName = txtTableName.Text;
        info.InsertLog = txtInsertLog.Checked;
        info.DeleteLog = txtDeleteLog.Checked;
        info.UpdateLog = txtUpdateLog.Checked;
        info.Note = txtNote.Text;
        info.Editor = GB.LoginUserInfo.FullName;
        info.LastUpdatedBy = GB.LoginUserInfo.ID.ToString();
        info.LastUpdateDate = txtCreationDate.DateTime;
    }

    /// <summary>
    /// 新增状态下的数据保存
    /// </summary>
    /// <returns></returns>
    public override async Task<bool> SaveAddNew()
    {
        OperationLogSettingInfo info = _tempInfo;//必须使用存在的局部变量，因为部分信息可能被附件使用
        SetInfo(info);
        info.Creator = GB.LoginUserInfo.FullName;
        info.CreatedBy = GB.LoginUserInfo.ID.ToString();
        info.CreationDate = txtCreationDate.DateTime;

        try
        {
            #region 新增数据
            //检查是否还有其他相同关键字的记录
            bool exist = await _bll.IsExistKeyAsync("TableName", info.TableName);
            if (exist)
            {
                "指定的【数据库表】已经存在，不能重复添加，请修改".ShowUxTips();
                return false;
            }

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
        OperationLogSettingInfo info = await _bll.FindByIdAsync(ID);
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

    private void FrmEditOperationLogSetting_Load(object sender, EventArgs e)
    {

    }
}