using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Dynamic;
using System.Reflection;
using BB.Tools.Entity;
using BB.Tools.Extension;
using Furion.ClayObject;

namespace BB.Tools.Format;

/// <summary>
/// DataTable操作辅助类
/// </summary>
public class DataTableHelper
{
    /// <summary>
    /// 给DataTable增加一个自增列
    /// 如果DataTable 存在 identityid 字段  则 直接返回DataTable 不做任何处理
    /// </summary>
    /// <param name="dt">DataTable</param>
    /// <returns>返回Datatable 增加字段 identityid </returns>
    public static DataTable AddIdentityColumn(DataTable dt)
    {
        if (!dt.Columns.Contains("identityid"))
        {
            dt.Columns.Add("identityid");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["identityid"] = (i + 1).ToString();
            }
        }
        return dt;
    }

    /// <summary>
    /// 检查DataTable 是否有数据行
    /// </summary>
    /// <param name="dt">DataTable</param>
    /// <returns></returns>
    public static bool IsHaveRows(DataTable dt)
    {
        if (dt != null && dt.Rows.Count > 0)
            return true;

        return false;
    }

    /// <summary>
    /// DataTable转换成实体列表
    /// </summary>
    /// <typeparam name="T">实体 T </typeparam>
    /// <param name="table">datatable</param>
    /// <returns></returns>
    public static IList<T> DataTableToList<T>(DataTable table)
        where T : class
    {
        if (!IsHaveRows(table))
            return new List<T>();

        IList<T> list = new List<T>();
        T model = default(T);
        foreach (DataRow dr in table.Rows)
        {
            model = Activator.CreateInstance<T>();

            foreach (DataColumn dc in dr.Table.Columns)
            {
                object drValue = dr[dc.ColumnName];
                PropertyInfo pi = model.GetType().GetProperty(dc.ColumnName);

                if (pi != null && pi.CanWrite && (drValue != null && !Convert.IsDBNull(drValue)))
                {
                    pi.SetValue(model, drValue, null);
                }
            }

            list.Add(model);
        }
        return list;
    }


    /// <summary>
    /// 实体列表转换成DataTable
    /// </summary>
    /// <typeparam name="T">实体</typeparam>
    /// <param name="list"> 实体列表</param>
    /// <returns></returns>
    public static DataTable ListToDataTable<T>(IList<T> list)
        where T : class
    {
        if (list == null || list.Count <= 0)
        {
            return null;
        }
        DataTable dt = new DataTable(typeof(T).Name);
        DataColumn column;
        DataRow row;

        PropertyInfo[] myPropertyInfo = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        int length = myPropertyInfo.Length;
        bool createColumn = true;

        foreach (T t in list)
        {
            if (t == null)
            {
                continue;
            }

            row = dt.NewRow();
            for (int i = 0; i < length; i++)
            {
                PropertyInfo pi = myPropertyInfo[i];
                string name = pi.Name;
                if (createColumn)
                {
                    column = new DataColumn(name, pi.PropertyType);
                    dt.Columns.Add(column);
                }

                row[name] = pi.GetValue(t, null);
            }

            if (createColumn)
            {
                createColumn = false;
            }

            dt.Rows.Add(row);
        }
        return dt;
    }

    /// <summary>
    /// 将DataTable 转换成 List<dynamic>
    /// reverse 反转：控制返回结果中是只存在 FilterField 指定的字段,还是排除.
    /// [flase 返回FilterField 指定的字段]|[true 返回结果剔除 FilterField 指定的字段]
    /// FilterField  字段过滤，FilterField 为空 忽略 reverse 参数；返回DataTable中的全部数
    /// </summary>
    /// <param name="table">DataTable</param>
    /// <param name="reverse">
    /// 反转：控制返回结果中是只存在 FilterField 指定的字段,还是排除.
    /// [flase 返回FilterField 指定的字段]|[true 返回结果剔除 FilterField 指定的字段]
    ///</param>
    /// <param name="filterField">字段过滤，FilterField 为空 忽略 reverse 参数；返回DataTable中的全部数据</param>
    /// <returns>List<dynamic></returns>
    public static List<dynamic> ToDynamicList(DataTable table, bool reverse = true, params string[] filterField)
    {
        var modelList = new List<dynamic>();
        foreach (DataRow row in table.Rows)
        {
            dynamic model = new ExpandoObject();
            var dict = (IDictionary<string, object>)model;
            foreach (DataColumn column in table.Columns)
            {
                if (filterField.Length != 0)
                {
                    if (reverse == true)
                    {
                        if (!((IList)filterField).Contains(column.ColumnName))
                        {
                            dict[column.ColumnName] = row[column];
                        }
                    }
                    else
                    {
                        if (((IList)filterField).Contains(column.ColumnName))
                        {
                            dict[column.ColumnName] = row[column];
                        }
                    }
                }
                else
                {
                    dict[column.ColumnName] = row[column];
                }
            }

            modelList.Add(model);
        }

        return modelList;
    }

    /// <summary>
    /// 将DataReader数据转为Dynamic对象
    /// </summary>
    /// <param name="reader"></param>
    /// <returns></returns>
    public static dynamic DataFillDynamic(IDataReader reader)
    {
        var d = new DynamicModel();
        for (var i = 0; i < reader.FieldCount; i++)
        {
            try
            {
                d.Add(reader.GetName(i), reader.GetValue(i));
            }
            catch
            {
                d.Add(reader.GetName(i), null);
            }
        }

        return d;
    }

    /// <summary>
    /// 将一条DataReader数据转为Clay对象
    /// </summary>
    /// <param name="reader"></param>
    /// <returns></returns>
    public static dynamic DataFillClay(IDataReader reader)
    {
        return Clay.Parse(reader.ToJsonObject());
    }

    /// <summary>
    /// 将DataReader数据转为List<dynamic>
    /// </summary>
    /// <param name="reader"></param>
    /// <returns></returns>
    public static List<dynamic> DataFillClayList(IDataReader reader)
    {
        List<dynamic> list = new ();
        while (reader.Read())
        {
            list.Add(DataFillClay(reader));
        }

        reader.Close();
        return list;
    }

    /// <summary>
    /// 将泛型集合类转换成DataTable
    /// </summary>
    /// <typeparam name="T">集合项类型</typeparam>
    /// <param name="list">集合</param>
    /// <returns>数据集(表)</returns>
    public static DataTable ToDataTable<T>(IList<T> list)
    {
        return ToDataTable<T>(list, null);
    }

    /// <summary>
    /// 将泛型集合类转换成DataTable
    /// </summary>
    /// <typeparam name="T">集合项类型</typeparam>
    /// <param name="list">集合</param>
    /// <param name="propertyName">需要返回的列的列名</param>
    /// <returns>数据集(表)</returns>
    public static DataTable ToDataTable<T>(IList<T> list, params string[] propertyName)
    {
        List<string> propertyNameList = new List<string>();
        if (propertyName != null)
            propertyNameList.AddRange(propertyName);

        DataTable result = new DataTable();
        if (list.Count > 0)
        {
            PropertyInfo[] propertys = list[0].GetType().GetProperties();
            foreach (PropertyInfo pi in propertys)
            {
                if (propertyNameList.Count == 0)
                {
                    result.Columns.Add(pi.Name, pi.PropertyType);
                }
                else
                {
                    if (propertyNameList.Contains(pi.Name))
                    {
                        result.Columns.Add(pi.Name, pi.PropertyType);
                    }
                }
            }

            for (int i = 0; i < list.Count; i++)
            {
                ArrayList tempList = new ArrayList();
                foreach (PropertyInfo pi in propertys)
                {
                    if (propertyNameList.Count == 0)
                    {
                        object obj = pi.GetValue(list[i], null);
                        tempList.Add(obj);
                    }
                    else
                    {
                        if (propertyNameList.Contains(pi.Name))
                        {
                            object obj = pi.GetValue(list[i], null);
                            tempList.Add(obj);
                        }
                    }
                }
                object[] array = tempList.ToArray();
                result.LoadDataRow(array, true);
            }
        }
        return result;
    }

    /// <summary>
    /// 获取实体类的DataTable表头信息
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    /// <returns></returns>
    public static DataTable GetDataTableSchema<T>()
    {
        PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
        DataTable table = new DataTable();
        for (int i = 0; i < props.Count; i++)
        {
            PropertyDescriptor prop = props[i];
            Type pt = prop.PropertyType;
            if (pt.IsGenericType && pt.GetGenericTypeDefinition() == typeof(Nullable<>))
                pt = Nullable.GetUnderlyingType(pt);
            table.Columns.Add(prop.Name, pt);
        }
        return table;
    }

    /// <summary>
    /// 根据实体类的集合，转换为DataTable的记录集合
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    /// <param name="data">实体类集合</param>
    /// <returns></returns>
    public static DataTable ConvertToDataTable<T>(List<T> data)
    {
        PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
        DataTable table = GetDataTableSchema<T>();
        object[] values = new object[props.Count];
        foreach (T item in data)
        {
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = props[i].GetValue(item);
            }
            table.Rows.Add(values);
        }
        table.AcceptChanges();
        return table;
    }


    /// <summary>
    /// 根据nameList里面的字段创建一个表格,返回该表格的DataTable
    /// </summary>
    /// <param name="nameList">包含字段信息的列表</param>
    /// <returns>DataTable</returns>
    public static DataTable CreateTable(List<string> nameList)
    {
        if (nameList.Count <= 0)
            return null;

        DataTable myDataTable = new DataTable();
        myDataTable.TableName = "tableName";//增加一个默认的名字
        foreach (string columnName in nameList)
        {
            myDataTable.Columns.Add(columnName, typeof(string));
        }
        return myDataTable;
    }

    /// <summary>
    /// 通过字符列表创建表字段，字段格式可以是：
    /// 1) a,b,c,d,e
    /// 2) a|int,b|string,c|bool,d|decimal
    /// </summary>
    /// <param name="nameString"></param>
    /// <returns></returns>
    public static DataTable CreateTable(string nameString)
    {
        string[] nameArray = nameString.Split(new[] { ',', ';' });
        List<string> nameList = new List<string>();
        DataTable dt = new DataTable();
        dt.TableName = "tableName";//增加一个默认的名字
        foreach (string item in nameArray)
        {
            if (!string.IsNullOrEmpty(item))
            {
                string[] subItems = item.Split('|');
                if (subItems.Length == 2)
                {
                    dt.Columns.Add(subItems[0], ConvertType(subItems[1]));
                }
                else
                {
                    dt.Columns.Add(subItems[0]);
                }
            }
        }
        return dt;
    }

    private static Type ConvertType(string typeName)
    {
        typeName = typeName.ToLower().Replace("system.", "");
        Type newType = typeof(string);
        switch (typeName)
        {
            case "boolean":
            case "bool":
                newType = typeof(bool);
                break;
            case "int16":
            case "short":
                newType = typeof(short);
                break;
            case "int32":
            case "int":
                newType = typeof(int);
                break;
            case "long":
            case "int64":
                newType = typeof(long);
                break;
            case "uint16":
            case "ushort":
                newType = typeof(ushort);
                break;
            case "uint32":
            case "uint":
                newType = typeof(uint);
                break;
            case "uint64":
            case "ulong":
                newType = typeof(ulong);
                break;
            case "single":
            case "float":
                newType = typeof(float);
                break;

            case "string":
                newType = typeof(string);
                break;
            case "guid":
                newType = typeof(Guid);
                break;
            case "decimal":
                newType = typeof(decimal);
                break;
            case "double":
                newType = typeof(double);
                break;
            case "datetime":
                newType = typeof(DateTime);
                break;
            case "byte":
                newType = typeof(byte);
                break;
            case "char":
                newType = typeof(char);
                break;
        }
        return newType;
    }

    /// <summary>
    /// 获得从DataRowCollection转换成的DataRow数组
    /// </summary>
    /// <param name="drc">DataRowCollection</param>
    /// <returns></returns>
    public static DataRow[] GetDataRowArray(DataRowCollection drc)
    {
        int count = drc.Count;
        DataRow[] drs = new DataRow[count];
        for (int i = 0; i < count; i++)
        {
            drs[i] = drc[i];
        }
        return drs;
    }

    /// <summary>
    /// 将DataRow数组转换成DataTable，注意行数组的每个元素须具有相同的数据结构，
    /// 否则当有元素长度大于第一个元素时，抛出异常
    /// </summary>
    /// <param name="rows">行数组</param>
    /// <returns></returns>
    public static DataTable GetTableFromRows(DataRow[] rows)
    {
        if (rows.Length <= 0)
        {
            return new DataTable();
        }
        DataTable dt = rows[0].Table.Clone();
        dt.DefaultView.Sort = rows[0].Table.DefaultView.Sort;
        for (int i = 0; i < rows.Length; i++)
        {
            dt.LoadDataRow(rows[i].ItemArray, true);
        }
        return dt;
    }

    /// <summary>
    /// 排序表的视图
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="sorts"></param>
    /// <returns></returns>
    public static DataTable SortedTable(DataTable dt, params string[] sorts)
    {
        if (dt.Rows.Count > 0)
        {
            string tmp = "";
            for (int i = 0; i < sorts.Length; i++)
            {
                tmp += sorts[i] + ",";
            }
            dt.DefaultView.Sort = tmp.TrimEnd(',');
        }
        return dt;
    }

    /// <summary>
    /// 根据条件过滤表的内容
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="condition"></param>
    /// <returns></returns>
    public static DataTable FilterDataTable(DataTable dt, string condition)
    {
        if (condition.Trim() == "")
        {
            return dt;
        }
        else
        {
            DataTable newdt = new DataTable();                
            newdt = dt.Clone();
            DataRow[] dr = dt.Select(condition);
            for (int i = 0; i < dr.Length; i++)
            {
                newdt.ImportRow((DataRow)dr[i]);
            }
            return newdt;
        }
    }

    /// <summary>
    /// 转换.NET的Type到数据库参数的类型
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public static DbType TypeToDbType(Type t)
    {
        DbType dbt;
        try
        {
            dbt = (DbType)Enum.Parse(typeof(DbType), t.Name);
        }
        catch
        {
            dbt = DbType.Object;
        }
        return dbt;
    }
                
    /// <summary>
    /// 使用分隔符串联表格字段的内容,如：a,b,c
    /// </summary>
    /// <param name="dt">表格</param>
    /// <param name="columnName">字段名称</param>
    /// <param name="append">增加的字符串，无则为空</param>
    /// <param name="splitChar">分隔符，如逗号(,)</param>
    /// <returns></returns>
    public static string ConcatColumnValue(DataTable dt, string columnName, string append, char splitChar)
    {
        string result = append;
        if (dt != null && dt.Rows.Count > 0)
        {
            foreach (DataRow row in dt.Rows)
            {
                result += $"{splitChar}{row[columnName]}";
            }
        }
        return result.Trim(splitChar);
    }

    /// <summary>
    /// 使用逗号串联表格字段的内容,如：a,b,c
    /// </summary>
    /// <param name="dt">表格</param>
    /// <param name="columnName">字段名称</param>
    /// <param name="append">增加的字符串，无则为空</param>
    /// <returns></returns>
    public static string ConcatColumnValue(DataTable dt, string columnName, string append)
    {
        string result = append;
        if (dt != null && dt.Rows.Count > 0)
        {
            foreach (DataRow row in dt.Rows)
            {
                result += $",{row[columnName]}";
            }
        }
        return result.Trim(',');
    }

    /// <summary>
    /// 判断表格是否包含指定的全部字段名称,如果其中一个不符合则返回false
    /// </summary>
    /// <param name="dt">表格对象</param>
    /// <param name="columnString">字段列名称，逗号分开</param>
    /// <returns></returns>
    public static bool ContainAllColumns(DataTable dt, string columnString)
    {
        bool result = true;
        if (dt != null && !string.IsNullOrEmpty(columnString))
        {
            List<string> columnList = columnString.ToDelimitedList<string>(",");
            foreach (string columnName in columnList)
            {
                if (!string.IsNullOrEmpty(columnName) && !dt.Columns.Contains(columnName))
                {
                    result = false;
                }
            }
        }
        else
        {
            result = false;
        }
        return result;
    }


    /// <summary>
    /// 接收Dataset里面的数据修改
    /// </summary>
    /// <param name="dataSet">Dataset对象</param>
    public static void AcceptDataChanges(DataSet dataSet)
    {
        for (int i = 0; i <= dataSet.Tables.Count - 1; i++)
        {
            if (dataSet.Tables[i] != null && dataSet.Tables[i].GetChanges() != null)
            {
                dataSet.Tables[i].AcceptChanges();
            }
        }
    }

    /// <summary>
    /// 放弃Dataset里面的数据修改
    /// </summary>
    /// <param name="dataSet">Dataset对象</param>
    public static void RejectDataChanges(DataSet dataSet)
    {
        for (int i = dataSet.Tables.Count - 1; i >= 0; i--)
        {
            if (dataSet.Tables[i] != null && dataSet.Tables[i].GetChanges() != null)
            {
                dataSet.Tables[i].RejectChanges();
            }
        }
    }

    /// <summary>
    /// 在DataTable表格里面增加一个标记选中的列CHECKFLAG
    /// </summary>
    /// <param name="dataTable">DataTable数据</param>
    /// <returns></returns>
    public static DataTable AddCheckFlagColumn(DataTable dataTable)
    {
        DataTable dtReturn = dataTable.Copy();
        DataTable result;
        if (dtReturn.Columns.Contains("CHECKFLAG"))
        {
            result = dtReturn;
        }
        else
        {
            dtReturn.Columns.Add("CHECKFLAG", typeof(int));
            foreach (DataRow row in dtReturn.Rows)
            {
                row["CHECKFLAG"] = 0;
            }
            dtReturn.AcceptChanges();
            result = dtReturn;
        }
        return result;
    }
        
    /// <summary>
    /// 获取DataTable里面排序过的不重复的记录
    /// </summary>
    /// <param name="dt">DataTable</param>
    /// <param name="sort">表格行的排序列</param>
    /// <returns></returns>
    public static DataTable DistinctDataTable(DataTable dt, string sort)
    {
        DataRow[] drs = dt.Copy().Select("", sort);
        object value = null;
        DataTable d = dt.Clone();

        for (int i = 0; i < drs.Length; i++)
        {
            if (value == null || !value.Equals(drs[i][sort].ToString()))
            {
                d.ImportRow(drs[i]);
                value = drs[i][sort].ToString();
            }
            else
            {
                drs[i].Delete();
            }
        }
        dt = d;
        return dt;
    }
}