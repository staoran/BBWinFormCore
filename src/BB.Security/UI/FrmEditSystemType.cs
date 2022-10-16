using BB.BaseUI.BaseUI;
using BB.BaseUI.Extension;
using BB.Entity.Security;
using BB.HttpServices.Core.SystemType;
using Furion.Logging.Extensions;

namespace BB.Security.UI;

public partial class FrmEditSystemType : BaseEditForm
{
    private readonly SystemTypeHttpService _bll;

    public FrmEditSystemType(SystemTypeHttpService bll)
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

        if (txtOid.Text.Trim().Length == 0)
        {
            "请输入系统标识".ShowUxTips();
            txtOid.Focus();
            result = false;
        }
        else if (txtName.Text.Trim().Length == 0)
        {
            "请输入系统名称".ShowUxTips();
            txtName.Focus();
            result = false;
        }
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
            #region 显示客户信息
            SystemTypeInfo info = await _bll.FindByIdAsync(ID);
            if (info != null)
            {
                txtOid.Text = info.Oid;
                txtName.Text = info.Name;
                txtCustomID.Text = info.CustomId;
                txtAuthorize.Text = info.Authorize;
                txtNote.Text = info.Note;

                txtOid.Enabled = false;
            }
            #endregion
            //this.btnOK.Enabled = GB.HasFunction("SystemType/Edit");             
        }
        else
        {                
            //this.btnOK.Enabled = GB.HasFunction("SystemType/Add");  
        }
    }

    /// <summary>
    /// 编辑或者保存状态下取值函数
    /// </summary>
    /// <param name="info"></param>
    private void SetInfo(SystemTypeInfo info)
    {
        info.Name = txtName.Text;
        info.CustomId = txtCustomID.Text;
        info.Authorize = txtAuthorize.Text;
        info.Note = txtNote.Text;
    }

    /// <summary>
    /// 新增状态下的数据保存
    /// </summary>
    /// <returns></returns>
    public override async Task<bool> SaveAddNew()
    {
        SystemTypeInfo info = new SystemTypeInfo();
        SetInfo(info);

        try
        {
            #region 新增数据
            //检查是否还有其他相同关键字的记录
            bool exist = await _bll.IsExistKeyAsync("OID", info.Oid);
            if (exist)
            {
                "指定的【系统标识】已经存在，不能重复添加，请修改".ShowUxTips();
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
        SystemTypeInfo info = await _bll.FindByIdAsync(ID);
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