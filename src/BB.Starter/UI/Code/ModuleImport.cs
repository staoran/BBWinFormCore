using BB.Entity.Security;

namespace BB.Starter.UI.Code;

/// <summary>
/// 模块生成
/// </summary>
public class ModuleImport
{
        /// <summary>
        /// 命名空间
        /// </summary>
        public string NameSpace { get; set; }
        
        /// <summary>
        /// 模块名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string Display { get; set; }

        /// <summary>
        /// 模块对应的物理表名或视图名
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 实体继承基类
        /// </summary>
        public string EntityBaseType { get; set; }

        /// <summary>
        /// 子模块外键字段名称
        /// </summary>
        public string ForeignKeyName { get; set; }
        
        /// <summary>
        /// 子模块名称
        /// </summary>
        public string ChildName { get; set; }

        /// <summary>
        /// 子模块显示名称
        /// </summary>
        public string ChildDisplay { get; set; }

        /// <summary>
        /// 子模块对应的物理表名或视图名
        /// </summary>
        public string ChildTableName { get; set; }

        /// <summary>
        /// 子实体继承基类
        /// </summary>
        public string ChildEntityBaseType { get; set; }

        #region 模块控制

        /// <summary>
        /// 是否允许查询
        /// </summary>
        public bool IsQuery { get; set; }
        
        /// <summary>
        /// 是否允许新增
        /// </summary>
        public bool IsAdd { get; set; }

        /// <summary>
        /// 是否允许编辑
        /// </summary>
        public bool IsEdit { get; set; }

        /// <summary>
        /// 是否允许删除
        /// </summary>
        public bool IsDelete { get; set; }

        /// <summary>
        /// 是否允许审核
        /// </summary>
        public bool IsCheck { get; set; }

        /// <summary>
        /// 是否允许导入
        /// </summary>
        public bool IsImport { get; set; }

        /// <summary>
        /// 是否允许导出
        /// </summary>
        public bool IsExport { get; set; }

        /// <summary>
        /// 启用打印
        /// </summary>
        public bool IsPrint { get; set; }

        /// <summary>
        /// 启用快查树
        /// </summary>
        public bool IsTree { get; set; }

        /// <summary>
        /// 子表可空
        /// </summary>
        public bool IsChildListNull { get; set; }

        #endregion

        /// <summary>
        /// 主表字段数据
        /// </summary>
        public IEnumerable<FieldControlConfig> MetadataImports { get; set; }

        /// <summary>
        /// 子表字段数据
        /// </summary>
        public IEnumerable<FieldControlConfig> ChildMetadataImports { get; set; }
}