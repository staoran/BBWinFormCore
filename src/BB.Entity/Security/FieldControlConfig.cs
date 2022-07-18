using System.Runtime.Serialization;
using BB.Entity.Base;

namespace BB.Entity.Security;

[Serializable]
[DataContract]
public class FieldControlConfig : BaseEntity
{
    /// <summary>
    /// 表名
    /// </summary>
    public string TableName { set; get; }
    
    /// <summary>
    /// 表说明
    /// </summary>
    public string TableDes { set; get; }
    
    /// <summary>
    /// 数据库字段名称
    /// </summary>
    public string DataBaseFieldName { set; get; }
    
    /// <summary>
    /// 数据库字段类型
    /// </summary>
    public string DataBaseFieldType { set; get; }
    
    /// <summary>
    /// 数据库字段长度
    /// </summary>
    public int DataBaseFieldLong { set; get; }

    /// <summary>
    /// 数据库字段说明
    /// </summary>
    public string DataBaseFieldDes { set; get; }
    
    /// <summary>
    /// c#字段类型
    /// </summary>
    public string CSharpFieldType { set; get; }
    
    /// <summary>
    /// c#字段全类型
    /// </summary>
    public string CSharpFieldFullType { set; get; }
    
    /// <summary>
    /// c#字段长度
    /// </summary>
    public int CSharpFieldLong { set; get; }
    
    /// <summary>
    /// C#字段名称
    /// </summary>
    public string CSharpFieldName { set; get; }
    
    /// <summary>
    /// C#字段说明
    /// </summary>
    public string CSharpFieldDes { set; get; }
    
    /// <summary>
    /// 控件类型
    /// </summary>
    public string ControlType { set; get; }
    
    /// <summary>
    /// 数据源
    /// </summary>
    public string DataTableName { set; get; }

    /// <summary>
    /// 控件labelname
    /// </summary>
    public string ControlLabelName { set; get; }
    
    /// <summary>
    /// 控件name
    /// </summary>
    public string ControlName { set; get; }

    /// <summary>
    /// 是否可见
    /// </summary>
    public bool IsVisible { set; get; } = true;

    /// <summary>
    /// 默认值
    /// </summary>
    public string Defaults { set; get; }

    /// <summary>
    /// 验证规则
    /// </summary>
    public string Validation { set; get; }

    /// <summary>
    /// 排序
    /// </summary>
    public int Sort { set; get; }

    /// <summary>
    /// 是否可新增
    /// </summary>
    public bool IsAdd { set; get; } = true;

    /// <summary>
    /// 是否可编辑
    /// </summary>
    public bool IsEdit { set; get; } = true;

    /// <summary>
    /// 是否只读
    /// </summary>
    public bool IsReadonly { set; get; } = false;
    
    /// <summary>
    /// 是否可为空
    /// </summary>
    public bool IsNull { set; get; }
    
    /// <summary>
    /// 是否验证长度
    /// </summary>
    public bool IsCheckLong { set; get; }
    
    /// <summary>
    ///是否主键
    /// </summary>
    public bool IsKey { set; get; }
    
    /// <summary>
    /// 是否自增字段
    /// </summary>
    public bool IsIdentity{ set; get; }

    /// <summary>
    /// 是否搜索列
    /// </summary>
    public bool IsSearch { set; get; } = false;

    /// <summary>
    /// 是否高级搜索列
    /// </summary>
    public bool IsAdvSearch { set; get; } = true;

    /// <summary>
    /// 是否网格列
    /// </summary>
    public bool IsColumn { set; get; } = true;

    /// <summary>
    /// 是否为空检验
    /// </summary>
    public bool IsCheckNull { set; get; }

    /// <summary>
    /// 验证是否重复
    /// </summary>
    public bool IsCheckDuplicate { set; get; } = false;

    /// <summary>
    /// 排序字段和排序方式
    /// </summary>
    public string OrderBy { set; get; }

    /// <summary>
    /// 乐观锁字段
    /// </summary>
    public bool OptimisticLock { set; get; } = false;

    /// <summary>
    /// 汇总方式
    /// </summary>
    public string SummaryItemType { set; get; } = "None";

    /// <summary>
    /// 汇总显示格式
    /// </summary>
    public string SummaryItemDisplayFormat { set; get; }
}