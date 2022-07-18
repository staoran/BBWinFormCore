using System.Runtime.Serialization;
using BB.Entity.Base;

namespace BB.Entity.Attachment;

/// <summary>
/// 上传附件信息
/// </summary>
[DataContract]
[Serializable]
[Table(DBTableName)]
public class FileUploadInfo : BaseEntity
{
    /// <summary>
    /// 默认构造函数（需要初始化属性的在此处理）
    /// </summary>
    public FileUploadInfo()
    {
        Id = Guid.NewGuid().ToString();
        FileSize = 0;//文件大小   
        DeleteFlag = 0;//删除标志，1为删除，0为正常
        AddTime = DateTime.Now; //添加时间    
    }

    #region Property Members

    /// <summary>
    /// ID
    /// </summary>
    [DataMember]
    [Key]
    [Column(FieldId)]
    public virtual string Id { get; set; }

    /// <summary>
    /// 附件组所属记录ID
    /// </summary>
    [DataMember]
    [Column(FieldOwnerId)]
    public virtual string OwnerId { get; set; }

    /// <summary>
    /// 附件组GUID
    /// </summary>
    [DataMember]
    [Column(FieldAttachmentGuid)]
    public virtual string AttachmentGuid { get; set; }

    /// <summary>
    /// 文件名
    /// </summary>
    [DataMember]
    [Column(FieldFileName)]
    public virtual string FileName { get; set; }

    /// <summary>
    /// 基础路径，在单机版的情况下，路径为本地物理路径
    /// </summary>
    [DataMember]
    [Column(FieldBasePath)]
    public virtual string BasePath { get; set; }

    /// <summary>
    /// 文件保存相对路径
    /// </summary>
    [DataMember]
    [Column(FieldSavePath)]
    public virtual string SavePath { get; set; }

    /// <summary>
    /// 文件分类
    /// </summary>
    [DataMember]
    [Column(FieldCategory)]
    public virtual string Category { get; set; }

    /// <summary>
    /// 文件大小
    /// </summary>
    [DataMember]
    [Column(FieldFileSize)]
    public virtual int FileSize { get; set; }

    /// <summary>
    /// 文件扩展名
    /// </summary>
    [DataMember]
    [Column(FieldFileExtend)]
    public virtual string FileExtend { get; set; }

    /// <summary>
    /// 所属用户
    /// </summary>
    [DataMember]
    [Column(FieldEditor)]
    public virtual string Editor { get; set; }

    /// <summary>
    /// 添加时间
    /// </summary>
    [DataMember]
    [Sort(IsDesc)]
    [Column(FieldAddTime)]
    public virtual DateTime AddTime { get; set; }

    /// <summary>
    /// 删除标志，1为删除，0为正常
    /// </summary>
    [DataMember]
    [Column(FieldDeleteFlag)]
    public virtual int DeleteFlag { get; set; }

    /// <summary>
    /// 文件流数据
    /// </summary>
    [DataMember]
    [Ignore]
    public byte[] FileData { get; set; }

    #endregion

    #region 定义字段名称相关常量

    /// <summary>
    /// 表名
    /// </summary>
    [NonSerialized]
    public new const string DBTableName = "TB_FileUpload";

    /// <summary>
    /// 主键名
    /// </summary>
    [NonSerialized]
    public new const string PrimaryKey = FieldId;

    /// <summary>
    /// 排序字段
    /// </summary>
    [NonSerialized]
    public new const string SortKey = FieldAddTime;

    /// <summary>
    /// 排序方式
    /// </summary>
    [NonSerialized]
    public new const bool IsDesc = true;

    [NonSerialized]
    public const string FieldId = "ID";

    /// <summary>
    /// 附件组所属记录ID
    /// </summary>
    [NonSerialized]
    public const string FieldOwnerId = "Owner_ID";

    /// <summary>
    /// 附件组GUID
    /// </summary>
    [NonSerialized]
    public const string FieldAttachmentGuid = "AttachmentGUID";

    /// <summary>
    /// 文件名
    /// </summary>
    [NonSerialized]
    public const string FieldFileName = "FileName";

    /// <summary>
    /// 基础路径
    /// </summary>
    [NonSerialized]
    public const string FieldBasePath = "BasePath";

    /// <summary>
    /// 文件保存相对路径
    /// </summary>
    [NonSerialized]
    public const string FieldSavePath = "SavePath";

    /// <summary>
    /// 文件分类
    /// </summary>
    [NonSerialized]
    public const string FieldCategory = "Category";

    /// <summary>
    /// 文件大小
    /// </summary>
    [NonSerialized]
    public const string FieldFileSize = "FileSize";

    /// <summary>
    /// 文件扩展名
    /// </summary>
    [NonSerialized]
    public const string FieldFileExtend = "FileExtend";

    /// <summary>
    /// 所属用户
    /// </summary>
    [NonSerialized]
    public const string FieldEditor = "Editor";

    /// <summary>
    /// 添加时间
    /// </summary>
    [NonSerialized]
    public const string FieldAddTime = "AddTime";

    /// <summary>
    /// 删除标志，1为删除，0为正常
    /// </summary>
    [NonSerialized]
    public const string FieldDeleteFlag = "DeleteFlag";

    #endregion
}

/// <summary>
/// FTP配置信息
/// </summary>
[DataContract]
[Serializable]
public class FtpInfo
{
    /// <summary>
    /// 默认构造函数
    /// </summary>
    public FtpInfo()
    {

    }

    /// <summary>
    /// 参数化构造函数
    /// </summary>
    /// <param name="server"></param>
    /// <param name="user"></param>
    /// <param name="password"></param>
    public FtpInfo(string server, string user, string password, string baseUrl)
    {
        Server = server;
        User = user;
        Password = password;
        BaseUrl = baseUrl;
    }

    /// <summary>
    /// FTP服务地址
    /// </summary>
    [DataMember]
    public string Server { get; set; }

    /// <summary>
    /// FTP用户名
    /// </summary>
    [DataMember]
    public string User { get; set; }

    /// <summary>
    /// FTP密码
    /// </summary>
    [DataMember]
    public string Password { get; set; }

    /// <summary>
    /// FTP的基础路径，如可以指定为IIS的路径：http://www.base.com:8000 ,方便下载打开
    /// </summary>
    [DataMember]
    public string BaseUrl { get; set; }
}