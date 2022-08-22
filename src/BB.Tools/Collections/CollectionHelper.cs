using BB.Tools.Entity;
using BB.Tools.Extension;

namespace BB.Tools.Collections;

/// <summary>
/// 把实体类树形结构的集合的名称进行缩进转化的辅助类
/// </summary>
/// <typeparam name="T">实体类</typeparam>
public class CollectionHelper<T> where T : class
{
    /// <summary>
    /// 把实体类树形结构的集合，根据层次关系对名称进行空格缩进，方便显示（如下拉列表）
    /// </summary>
    /// <param name="pId">父节点</param>
    /// <param name="level">层次等级，从0开始</param>
    /// <param name="list">实体类列表</param>
    /// <param name="pidName">父节点名称</param>
    /// <param name="idName">ID名称</param>
    /// <param name="name">树节点名称</param>
    /// <returns></returns>
    public static List<T> Fill(int pId, int level, List<T> list, string pidName, string idName, string name)
    {
        List<T> returnList = new List<T>();
        foreach (T obj in list)
        {
            int typePID = (int)ReflectionExtension.GetProperty(obj, pidName);
            int typeId = (int)ReflectionExtension.GetProperty(obj, idName);
            string typeName = ReflectionExtension.GetProperty(obj, name) as string;

            if (pId == typePID)
            {
                string newName = new string('　', level * 2) + typeName;
                ReflectionExtension.SetProperty(obj, name, newName);
                returnList.Add(obj);

                returnList.AddRange(Fill(typeId, level + 1, list, pidName, idName, name));
            }
        }
        return returnList;
    }

    /// <summary>
    /// 把实体类树形结构的集合，根据层次关系对名称进行空格缩进，方便显示（如下拉列表）
    /// </summary>
    /// <param name="pId">父节点</param>
    /// <param name="level">层次等级，从0开始</param>
    /// <param name="list">实体类列表</param>
    /// <param name="pidName">父节点名称</param>
    /// <param name="idName">ID名称</param>
    /// <param name="name">树节点名称</param>
    /// <returns></returns>
    public static List<T> Fill(string pId, int level, List<T> list, string pidName, string idName, string name)
    {
        List<T> returnList = new List<T>();
        foreach (T obj in list)
        {
            string typePID = (string)ReflectionExtension.GetProperty(obj, pidName);
            string typeId = (string)ReflectionExtension.GetProperty(obj, idName);
            string typeName = ReflectionExtension.GetProperty(obj, name) as string;

            if (pId == typePID)
            {
                string newName = new string('　', level * 2) + typeName;
                ReflectionExtension.SetProperty(obj, name, newName);
                returnList.Add(obj);

                returnList.AddRange(Fill(typeId, level + 1, list, pidName, idName, name));
            }
        }
        return returnList;
    }

    public static List<T> Fill(object pId, int level, List<T> list, string pidName, string idName, string name)
    {
        List<T> returnList = new List<T>();
        foreach (T obj in list)
        {
            string typePID = ReflectionExtension.GetProperty(obj, pidName).ToString();
            string typeId = ReflectionExtension.GetProperty(obj, idName).ToString();
            string typeName = ReflectionExtension.GetProperty(obj, name) as string;

            if (pId.ToString() == typePID)
            {
                string newName = new string('　', level * 2) + typeName;
                ReflectionExtension.SetProperty(obj, name, newName);
                returnList.Add(obj);

                returnList.AddRange(Fill(typeId, level + 1, list, pidName, idName, name));
            }
        }
        return returnList;
    }

    /// <summary>
    /// 根据集合的树形结构（以PID为索引），对集合中对象的Name进行缩进处理后返回新的集合。
    /// </summary>
    /// <param name="pId">PID的值</param>
    /// <param name="level">记录的层级，默认开始为0</param>
    /// <param name="list">对象集合</param>
    /// <param name="pidName">PID的属性名称</param>
    /// <param name="idName">ID的属性名称</param>
    /// <param name="name">Name的属性名称</param>
    /// <param name="levelChar">缩进或代替字符，默认可以用' '</param>
    /// <returns></returns>
    public static List<T> Fill(string pId, int level, List<T> list, string pidName = "PID", string idName = "ID", string name = "Name", char levelChar = ' ')
    {
        List<T> returnList = new List<T>();
        foreach (T obj in list)
        {
            string typePID = (string)ReflectionExtension.GetProperty(obj, pidName);
            string typeId = (string)ReflectionExtension.GetProperty(obj, idName);
            string typeName = ReflectionExtension.GetProperty(obj, name) as string;

            if (pId == typePID)
            {
                string newName = new string(levelChar, level * 2) + typeName;
                ReflectionExtension.SetProperty(obj, name, newName);
                returnList.Add(obj);

                returnList.AddRange(Fill(typeId, level + 1, list, pidName, idName, name));
            }
        }
        return returnList;
    }

    /// <summary>
    /// 和Fill方法类似，获取用于绑定字典的有层次的数据集合
    /// </summary>
    /// <param name="list">列表内容</param>
    /// <param name="level">层次等级，从0开始</param>
    /// <param name="childrenName">子集合的对象名</param>
    /// <param name="id">值名称，如ID</param>
    /// <param name="name">显示名称，如Name</param>
    /// <returns></returns>
    public static List<CListItem> GetIndentedItems(List<T> list, int level, string childrenName, string id = "ID", string name = "Name")
    {
        List<CListItem> result = new List<CListItem>();
        foreach (T info in list)
        {
            var objId = ReflectionExtension.GetProperty(info, id);
            string idValue = objId != null ? objId.ToString() : "";
            string nameValue = ReflectionExtension.GetProperty(info, name) as string;
            nameValue = new string('　', level * 2) + nameValue;

            result.Add(new CListItem(nameValue, idValue));

            var children = ReflectionExtension.GetProperty(info, childrenName) as List<T>;
            if (children != null)
            {
                var itemList = GetIndentedItems(children, level + 1, childrenName, id, name);
                result.AddRange(itemList);
            }
        }
        return result;
    }

    /// <summary>
    /// 根据普通列表的PID关系，转换为排序后的字典列表
    /// </summary>
    /// <param name="list">列表内容</param>
    /// <param name="pid">父节点</param>
    /// <param name="pidName">父节点名称，如PID</param>
    /// <param name="id">值名称，如ID</param>
    /// <param name="name">显示名称，如Name</param>
    /// <returns></returns>
    public static List<CListItem> GetSortedItems(List<T> list, string pid, string pidName = "PID", string id = "ID", string name = "Name")
    {
        List<CListItem> result = new List<CListItem>();
        foreach (T info in list)
        {
            var objPID = ReflectionExtension.GetProperty(info, pidName);
            string pidValue = objPID != null ? objPID.ToString() : "";

            var objId = ReflectionExtension.GetProperty(info, id);
            string idValue = objId != null ? objId.ToString() : "";
            string nameValue = ReflectionExtension.GetProperty(info, name) as string;

            result.Add(new CListItem(nameValue, idValue));

            if (pid == pidValue)
            {
                var itemList = GetSortedItems(list, pidValue, pidName, id, name);
                result.AddRange(itemList);
            }
        }
        return result;
    }
}