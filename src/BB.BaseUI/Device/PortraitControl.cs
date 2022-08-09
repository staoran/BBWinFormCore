using System.ComponentModel;
using BB.BaseUI.DocViewer;
using BB.BaseUI.Extension;
using BB.BaseUI.Other;
using DevExpress.XtraEditors;
using Furion.Logging.Extensions;

namespace BB.BaseUI.Device;

/// <summary>
/// 保存图片的处理代理定义
/// </summary>
/// <param name="id">记录ID</param>
/// <param name="imageBytes">图片数据</param>
/// <param name="imageType">图片类型</param>
/// <returns></returns>
public delegate bool SavePortraitHandler(string id, byte[] imageBytes, string imageType);

/// <summary>
/// 绑定图片数据的代理定义
/// </summary>
/// <param name="id">记录ID</param>
/// <param name="imageType">图片类型</param>
/// <returns></returns>
public delegate byte[] BindPortraitHandler(string id, string imageType);

/// <summary>
/// 图片数据显示和采集控件
/// </summary>
public partial class PortraitControl : XtraUserControl
{
    #region 事件及属性定义
    /// <summary>
    /// 保存图片操作的事件
    /// </summary>
    public event SavePortraitHandler OnSavePortrait;

    /// <summary>
    /// 绑定图片数据的事件
    /// </summary>
    public event BindPortraitHandler OnBindPortait;

    /// <summary>
    /// 记录ID
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// 图片是否被修改过
    /// </summary>
    public bool ImageIsDirty { get; set; }

    private string _imageType = "个人肖像";
    /// <summary>
    /// 图片类型
    /// </summary>
    public string ImageType
    {
        get => _imageType;
        set
        {
            _imageType = value;
            ResetDefaultImage(_imageType);
        }
    }

    private bool _mShowEditTool = true;
    /// <summary>
    /// 是否显示编辑工具条
    /// </summary>
    public bool ShowEditTool
    {
        get => _mShowEditTool;
        set
        {
            _mShowEditTool = value;
            pnlTool.Visible = value;
            if (!value)
            {
                picPortrait.Dock = DockStyle.Fill;
            }
            else
            {
                picPortrait.Dock = DockStyle.None;
            }
            picPortrait.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
        }
    }
    #endregion

    /// <summary>
    /// 默认构造函数
    /// </summary>
    public PortraitControl()
    {
        InitializeComponent();
    }

    private void btnLoad_Click(object sender, EventArgs e)
    {
        picPortrait.LoadImage();

        //看是否被修改
        ImageIsDirty = true;
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        bool sucess = SavePicture(Id);

        (sucess ? "图片保存成功！" : "保存失败！").ShowUxTips();
    }

    /// <summary>
    /// 保存图片到服务器
    /// </summary>
    public bool SavePicture(string id)
    {
        Id = id;//设置控件的ID值

        if (string.IsNullOrEmpty(id))
        {
            "记录ID未指定，无法保存，请先保存数据！".ShowUxTips();
            return false;
        }
        if (OnSavePortrait == null)
        {
            "控件未指定OnSavePortrait处理事件！".ShowUxTips();
            return false;
        }

        if (picPortrait.Image != null)
        {
            try
            {
                byte[] imageBytes = ImageHelper.ImageToBytes(picPortrait.Image);

                bool sucess = false;
                if (OnSavePortrait != null)
                {
                    sucess = OnSavePortrait(Id, imageBytes, ImageType);
                }
                return sucess;

            }
            catch (Exception ex)
            {
                ex.Message.ShowUxError();
                ex.ToString().LogError();;
            }
        }
        return false;
    }

    private void btnRestore_Click(object sender, EventArgs e)
    {
        ResetDefaultImage(ImageType);
    }

    /// <summary>
    /// 重置默认图片，可以选择“个人肖像”、“车辆图片”，其他值将显示空白
    /// </summary>
    /// <param name="imageType"></param>
    private void ResetDefaultImage(string imageType)
    {
        ComponentResourceManager resources = new ComponentResourceManager(typeof(PortraitControl));
        switch (imageType)
        {
            case "个人肖像":
                picPortrait.EditValue = ((object)(resources.GetObject("picPortrait.EditValue")));
                break;
            case "车辆照片":
                picPortrait.EditValue = ((object)(resources.GetObject("CabrioletRed")));
                break;
            default:
                picPortrait.EditValue = null;
                break;
        }
        ImageIsDirty = true;
    }

    /// <summary>
    /// 绑定图片的操作，触发绑定事件处理
    /// </summary>
    /// <param name="id">记录ID</param>
    public void BindPicture(string id)
    {
        try
        {
            Id = id; //设置控件的ID值

            #region 更新图片显示操作

            if (OnBindPortait == null)
            {
                "控件未指定OnBindPortait事件处理".ShowUxTips();
                return;
            }

            byte[] imageBytes = OnBindPortait(id, ImageType);
            if (imageBytes != null)
            {
                picPortrait.Image = ImageHelper.ImageFromBytes(imageBytes);
            }
            else
            {
                ResetDefaultImage(ImageType);
            }

            #endregion

            //this.lblPictureTips.Text = "全部图片更新成功！";
        }
        catch (Exception ex)
        {
            ex.Message.ShowUxError();
            ex.ToString().LogError();;
        }
    }

    private void PortraitControl_Load(object sender, EventArgs e)
    {
    }

    private void picPortrait_MouseDoubleClick(object sender, MouseEventArgs e)
    {
        PictureEdit picture = sender as PictureEdit;
        if (picture != null && picture.Image != null)
        {
            FrmImageView dlg = new FrmImageView();
            dlg.pictureEdit1.Image = picture.Image;
            dlg.ShowDialog();
        }
    }

    private void btnCapture_Click(object sender, EventArgs e)
    {
        CameraDialog dlg = new CameraDialog();
        if (dlg.ShowDialog() == DialogResult.OK)
        {
            picPortrait.Image = dlg.Photo;
            ImageIsDirty = true;
        }
    }
}