using BB.Tools.Entity;
using Furion;

namespace BB.Tools.Format;

/// <summary> 
/// 根据各种不同数据库生成不同分页语句的辅助类 PagerHelper
/// </summary> 
public class PagerHelper
{
    #region 属性对象

    /// <summary>
    /// 待查询表或自定义查询语句
    /// </summary>
    public string TableName { get; set; }

    /// <summary>
    /// 需要返回的列
    /// </summary>
    public string FieldsToReturn { get; set; }

    /// <summary>
    /// 排序字段名称
    /// </summary>
    public string FieldNameToSort { get; set; }

    /// <summary>
    /// 页尺寸,就是一页显示多少条记录
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// 当前的页码
    /// </summary>
    public int PageIndex { get; set; }

    /// <summary>
    /// 是否以降序排列结果
    /// </summary>
    public bool IsDescending { get; set; }

    /// <summary>
    /// 检索条件(注意: 不要加 where)
    /// </summary>
    public string StrWhere { get; set; }

    /// <summary>
    /// 表或Sql语句包装属性
    /// </summary>
    internal string TableOrSqlWrapper
    {
        get
        {
            bool isSql = TableName.ToLower().Contains("from");
            if (isSql)
            {
                return $"({TableName}) AA ";//如果是Sql语句，则加括号后再使用
            }
            else
            {
                return TableName;//如果是表名，则直接使用
            }
        }
    }

    #endregion

    #region 构造函数

    /// <summary>
    /// 默认构造函数，其他通过属性设置
    /// </summary>
    public PagerHelper()
    {
        FieldsToReturn = "*";//需要返回的列
        FieldNameToSort = string.Empty;//排序字段名称
        PageSize = 10;//页尺寸,就是一页显示多少条记录
        PageIndex = 1;//当前的页码
        IsDescending = false;//是否以降序排列
        StrWhere = string.Empty;//检索条件(注意: 不要加 where)
    }

    /// <summary>
    /// 完整的构造函数,可以包含条件,返回记录字段等条件
    /// </summary>
    /// <param name="tableName">自定义查询语句</param>
    /// <param name="fieldsToReturn">需要返回的列</param>
    /// <param name="fieldNameToSort">排序字段名称</param>
    /// <param name="pageSize">页尺寸</param>
    /// <param name="pageIndex">当前的页码</param>
    /// <param name="isDescending">是否以降序排列</param>
    /// <param name="strwhere">检索条件</param>
    public PagerHelper(string tableName, string fieldsToReturn, string fieldNameToSort,
        int pageSize, int pageIndex, bool isDescending, string strwhere) : this()
    {
        TableName = tableName;
        FieldsToReturn = fieldsToReturn;
        FieldNameToSort = fieldNameToSort;
        PageSize = pageSize;
        PageIndex = pageIndex;
        IsDescending = isDescending;
        StrWhere = strwhere;
    }

    #endregion

    /// <summary>
    /// 不依赖于存储过程的分页(Oracle)
    /// </summary>
    /// <param name="isDoCount">如果isDoCount为True，返回总数统计Sql；否则返回分页语句Sql</param>
    /// <returns></returns>
    private string GetOracleSql(bool isDoCount)
    {
        string sql = "";
        if (string.IsNullOrEmpty(StrWhere))
        {
            StrWhere = " (1=1) ";
        }

        if (isDoCount)//执行总数统计
        {
            sql = $"select count(*) as Total from {TableOrSqlWrapper} Where {StrWhere} ";
        }
        else
        {
            string strOrder = $" order by {FieldNameToSort} {(IsDescending ? "DESC" : "ASC")}";

            int minRow = PageSize * (PageIndex - 1);
            int maxRow = PageSize * PageIndex;
            string selectSql = $"select {FieldsToReturn} from {TableOrSqlWrapper} Where {StrWhere} {strOrder}";
            sql = string.Format(@"select b.* from
                           (select a.*, rownum as rowIndex from({2}) a) b
                           where b.rowIndex > {0} and b.rowIndex <= {1}", minRow, maxRow, selectSql);
        }

        return sql;
    }

    /// <summary>
    /// 不依赖于存储过程的分页(SqlServer)
    /// </summary>
    /// <param name="isDoCount">如果isDoCount为True，返回总数统计Sql；否则返回分页语句Sql</param>
    /// <returns></returns>
    private string GetSqlServerSql(bool isDoCount)
    {
        string sql = "";
        if (string.IsNullOrEmpty(StrWhere))
        {
            StrWhere = " (1=1) ";
        }

        if (isDoCount)//执行总数统计
        {
            sql = $"select count(*) as Total from {TableOrSqlWrapper} Where {StrWhere} ";
        }
        else
        {
            // With Paging AS
            // ( SELECT ROW_NUMBER() OVER (order by SortCode desc) as RowNumber, * FROM T_ACL_User )
            // SELECT * from Paging Where RowNumber Between 1 and 20
            string strOrder = $" order by {FieldNameToSort} {(IsDescending ? "DESC" : "ASC")}";
            int minRow = PageSize * (PageIndex - 1) + 1;
            int maxRow = PageSize * PageIndex;

            sql = $@"With Paging AS
                ( SELECT ROW_NUMBER() OVER ({strOrder}) as RowNumber, {FieldsToReturn} FROM {TableOrSqlWrapper} Where {StrWhere})
                SELECT * FROM Paging WHERE RowNumber Between {minRow} and {maxRow}";
        }

        return sql;
    }

    /// <summary>
    /// 不依赖于存储过程的分页(Access)
    /// </summary>
    /// <param name="isDoCount">如果isDoCount为True，返回总数统计Sql；否则返回分页语句Sql</param>
    /// <returns></returns>
    private string GetAccessSql(bool isDoCount)
    {
        string sql = "";
        if (string.IsNullOrEmpty(StrWhere))
        {
            StrWhere = " (1=1) ";
        }

        if (isDoCount)//执行总数统计
        {
            sql = $"select count(*) as Total from {TableOrSqlWrapper} Where {StrWhere} ";
        }
        else
        {
            string strTemp = string.Empty;
            string strOrder = string.Empty;
            if (IsDescending)
            {
                strTemp = "<(select min";
                strOrder = $" order by [{FieldNameToSort}] desc";
            }
            else
            {
                strTemp = ">(select max";
                strOrder = $" order by [{FieldNameToSort}] asc";
            }

            sql = $"select top {PageSize} {FieldsToReturn} from {TableOrSqlWrapper} ";

            //如果是第一页就执行以上代码，这样会加快执行速度
            if (PageIndex == 1)
            {
                sql += $" Where {StrWhere} ";
                sql += strOrder;
            }
            else
            {
                sql += string.Format(" Where [{0}] {1} ([{0}]) from (select top {2} [{0}] from {3} where {5} {4} ) as tblTmp) and {5} {4}",
                    FieldNameToSort, strTemp, (PageIndex - 1) * PageSize, TableOrSqlWrapper, strOrder, StrWhere);
            }
        }

        return sql;
    }

    /// <summary>
    /// 不依赖于存储过程的分页(MySql)
    /// </summary>
    /// <param name="isDoCount">如果isDoCount为True，返回总数统计Sql；否则返回分页语句Sql</param>
    /// <returns></returns>
    private string GetMySqlSql(bool isDoCount)
    {
        string sql = "";
        if (string.IsNullOrEmpty(StrWhere))
        {
            StrWhere = " (1=1) ";
        }

        if (isDoCount)//执行总数统计
        {
            sql = $"select count(*) as Total from {TableOrSqlWrapper} Where {StrWhere} ";
        }
        else
        {
            //SELECT * FROM 表名称 LIMIT M,N 
            string strOrder = $" order by {FieldNameToSort} {(IsDescending ? "DESC" : "ASC")}";

            int minRow = PageSize * (PageIndex - 1);
            int maxRow = PageSize * PageIndex;
            sql =
                $"select {FieldsToReturn} from {TableOrSqlWrapper} Where {StrWhere} {strOrder} LIMIT {minRow},{PageSize}";
        }

        return sql;
    }

    /// <summary>
    /// 不依赖于存储过程的分页(SQLite)
    /// </summary>
    /// <param name="isDoCount">如果isDoCount为True，返回总数统计Sql；否则返回分页语句Sql</param>
    /// <returns></returns>
    private string GetSqLiteSql(bool isDoCount)
    {
        string sql = "";
        if (string.IsNullOrEmpty(StrWhere))
        {
            StrWhere = " (1=1) ";
        }

        if (isDoCount)//执行总数统计
        {
            sql = $"select count(*) as Total from {TableOrSqlWrapper} Where {StrWhere} ";
        }
        else
        {
            //SELECT * FROM 表名称 LIMIT M,N 
            string strOrder = $" order by {FieldNameToSort} {(IsDescending ? "DESC" : "ASC")}";

            int minRow = PageSize * (PageIndex - 1);
            int maxRow = PageSize * PageIndex;
            sql =
                $"select {FieldsToReturn} from {TableOrSqlWrapper} Where {StrWhere} {strOrder} LIMIT {minRow},{PageSize}";
        }

        return sql;
    }

    /// <summary>
    /// 不依赖于存储过程的分页(PostgreSQL)
    /// </summary>
    /// <param name="isDoCount">如果isDoCount为True，返回总数统计Sql；否则返回分页语句Sql</param>
    /// <returns></returns>
    private string GetPostgreSqlSql(bool isDoCount)
    {
        string sql = "";
        if (string.IsNullOrEmpty(StrWhere))
        {
            StrWhere = " (1=1) ";
        }

        if (isDoCount)//执行总数统计
        {
            sql = $"select count(*) as Total from {TableOrSqlWrapper} Where {StrWhere} ";
        }
        else
        {
            //SELECT * FROM 表名称 LIMIT M,N 
            string strOrder = $" order by {FieldNameToSort} {(IsDescending ? "DESC" : "ASC")}";

            int minRow = PageSize * (PageIndex - 1);
            int maxRow = PageSize * PageIndex;
            sql = string.Format("select {0} from {1} Where {2} {3} LIMIT {5} OFFSET {4}",
                FieldsToReturn, TableOrSqlWrapper, StrWhere, strOrder, minRow, PageSize);
        }

        return sql;
    }

    /// <summary>
    /// 不依赖于存储过程的分页(达梦数据库)
    /// </summary>
    /// <param name="isDoCount">如果isDoCount为True，返回总数统计Sql；否则返回分页语句Sql</param>
    /// <returns></returns>
    private string GetDmSql(bool isDoCount)
    {
        string sql = "";
        if (string.IsNullOrEmpty(StrWhere))
        {
            StrWhere = " (1=1) ";
        }

        if (isDoCount)//执行总数统计
        {
            sql = $"select count(*) as Total from {TableOrSqlWrapper} Where {StrWhere} ";
        }
        else
        {
            string strTemp = string.Empty;
            string strOrder = string.Empty;
            if (IsDescending)
            {
                strTemp = "<(select min";
                strOrder = $" order by {FieldNameToSort} desc";
            }
            else
            {
                strTemp = ">(select max";
                strOrder = $" order by {FieldNameToSort} asc";
            }

            sql = $"select top {PageSize} {FieldsToReturn} from {TableOrSqlWrapper} ";

            //如果是第一页就执行以上代码，这样会加快执行速度
            if (PageIndex == 1)
            {
                sql += $" Where {StrWhere} ";
                sql += strOrder;
            }
            else
            {
                sql += string.Format(" Where {0} {1} ({0}) from (select top {2} {0} from {3} where {5} {4} ) as tblTmp) and {5} {4}",
                    FieldNameToSort, strTemp, (PageIndex - 1) * PageSize, TableOrSqlWrapper, strOrder, StrWhere);
            }
        }

        return sql;
    }

    /// <summary>
    /// 获取对应数据库的分页语句（指定数据库类型）
    /// </summary>
    /// <param name="dbType">数据库类型枚举</param>
    /// <param name="isDoCount">如果isDoCount为True，返回总数统计Sql；否则返回分页语句Sql</param>
    public string GetPagingSql(bool isDoCount, DatabaseType dbType)
    {
        string sql = "";
        switch (dbType)
        {
            case DatabaseType.Access:
                sql = GetAccessSql(isDoCount);
                break;
            case DatabaseType.SqlServer:
                sql = GetSqlServerSql(isDoCount);
                break;
            case DatabaseType.Oracle:
                sql = GetOracleSql(isDoCount);
                break;
            case DatabaseType.MySql:
                sql = GetMySqlSql(isDoCount);
                break;
            case DatabaseType.SqLite:
                sql = GetSqLiteSql(isDoCount);
                break;
            case DatabaseType.PostgreSql:
                sql = GetPostgreSqlSql(isDoCount);
                break;
            case DatabaseType.Dm:
                sql = GetDmSql(isDoCount);
                break;
        }
        return sql;
    }

    /// <summary>
    /// 获取对应数据库的分页语句(从配置文件读取数据库类型：ComponentDbType）
    /// </summary>
    /// <param name="isDoCount">如果isDoCount为True，返回总数统计Sql；否则返回分页语句Sql</param>
    public string GetPagingSql(bool isDoCount, string databaseType)
    {
        DatabaseType dbType = GetDataBaseType(databaseType);
        return GetPagingSql(isDoCount, dbType);
    }

    public string GetPagingSql(bool isDoCount)
    {
        string dbType = App.Configuration["ConnectionConfigs:0:DbType"];
        dbType = string.IsNullOrEmpty(dbType) ? "SqlServer" : dbType;
        return GetPagingSql(isDoCount, dbType);
    }

    private DatabaseType GetDataBaseType(string databaseType)
    {
        DatabaseType returnValue = DatabaseType.SqlServer;
        foreach (DatabaseType dbType in Enum.GetValues(typeof(DatabaseType)))
        {
            if (dbType.ToString().Equals(databaseType, StringComparison.OrdinalIgnoreCase))
            {
                returnValue = dbType;
                break;
            }
        }
        return returnValue;
    }
}