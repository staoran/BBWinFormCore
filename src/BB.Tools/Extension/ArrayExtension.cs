using BB.Tools.Validation;

namespace BB.Tools.Extension;

/// <summary>
/// Array 帮助类
/// </summary>
public static class ArrayExtension
{
    #region Methods
        
    /// <summary>
    /// 向数组中中添加新元素
    /// <para>eg: CollectionAssert.AreEqual(new int[6] { 1, 2, 3, 4, 5, 6 }, ArrayHelper.Add(new int[5] { 1, 2, 3, 4, 5 }, 6));</para>
    /// </summary>
    /// <typeparam name="T">泛型</typeparam>
    /// <param name="sourceArray">需要操作的数组</param>
    /// <param name="item">需要添加数组项</param>
    /// <returns>数组</returns>
    public static T[] Add<T>(T[] sourceArray, T item)
    {
        ArgumentCheck.Begin(sourceArray, "需要操作的数组").NotNull();
        ArgumentCheck.Begin(item, "需要添加数组项").NotNull();
        int count = sourceArray.Length;
        Array.Resize<T>(ref sourceArray, count + 1);
        sourceArray[count] = item;
        return sourceArray;
    }
        
    /// <summary>
    /// 向数组中添加新数组；
    /// <para>
    /// eg: CollectionAssert.AreEqual(new int[7] { 1, 2, 3, 4, 5, 6, 7 },
    ///     ArrayHelper.AddRange(new int[5] { 1, 2, 3, 4, 5 }, new int[2] { 6, 7 }));
    /// </para>
    /// </summary>
    /// <typeparam name="T">泛型</typeparam>
    /// <param name="sourceArray">需要操作的数组</param>
    /// <param name="addArray">被添加的数组</param>
    /// <returns>数组</returns>
    public static T[] AddRange<T>(T[] sourceArray, T[] addArray)
    {
        ArgumentCheck.Begin(sourceArray, "需要操作的数组").NotNull();
        ArgumentCheck.Begin(addArray, "被添加的数组").NotNull();
        int count = sourceArray.Length;
        int addCount = addArray.Length;
        Array.Resize<T>(ref sourceArray, count + addCount);
        addArray.CopyTo(sourceArray, count);
        return sourceArray;
    }
        
    /// <summary>
    /// 清空数组
    /// <para>
    /// eg:
    /// int[] _test = new int[5] { 1, 2, 3, 4, 5 };
    /// _test.ClearAll();
    /// CollectionAssert.AreEqual(new int[5] { 0, 0, 0, 0, 0 }, _test);
    /// </para>
    /// </summary>
    /// <param name="sourceArray">数组</param>
    public static void ClearAll(Array sourceArray)
    {
        ArgumentCheck.Begin(sourceArray, "需要操作的数组").NotNull();
        Array.Clear(sourceArray, 0, sourceArray.Length);
    }
        
    /// <summary>
    /// 判断数组的值是否相等
    /// <para> eg: Assert.IsTrue(ArrayHelper.CompletelyEqual(new int[5] { 1, 2, 3, 4, 5 }, new int[5] { 1, 2, 3, 4, 5 }));
    /// </para>
    /// </summary>
    /// <typeparam name="T">泛型</typeparam>
    /// <param name="sourceArray">数组一</param>
    /// <param name="compareArray">数组二</param>
    /// <returns>是否相等</returns>
    public static bool CompletelyEqual<T>(this T[] sourceArray, T[] compareArray)
    {
        ArgumentCheck.Begin(sourceArray, "需要操作的数组").NotNull();
        ArgumentCheck.Begin(compareArray, "被比较的数组").NotNull();
            
        if(sourceArray == null || compareArray == null)
        {
            return false;
        }
            
        if(sourceArray.Length != compareArray.Length)
        {
            return false;
        }
            
        for(int i = 0; i < sourceArray.Length; i++)
        {
            if(!sourceArray[i].Equals(compareArray[i]))
            {
                return false;
            }
        }
            
        return true;
    }
        
    /// <summary>
    /// 字符串数值忽略大小写包含判断
    /// </summary>
    /// <param name="sourceArray">需要操作的数组</param>
    /// <param name="compareStringItem">包含判断的字符串</param>
    /// <returns>是否包含在内</returns>
    public static bool ContainIgnoreCase(this string[] sourceArray, string compareStringItem)
    {
        bool result = false;
            
        foreach(string item in sourceArray)
        {
            if(string.Equals(item, compareStringItem, StringComparison.OrdinalIgnoreCase))
            {
                result = true;
                break;
            }
        }
            
        return result;
    }
        
    /// <summary>
    /// 复制数组
    /// <para>
    /// eg: CollectionAssert.AreEqual(new int[3] { 1, 2, 3 }, ArrayHelper.Copy(new int[5] { 1,
    ///     2, 3, 4, 5 }, 0, 3));
    /// </para>
    /// </summary>
    /// <typeparam name="T">泛型</typeparam>
    /// <param name="sourceArray">需要操作数组</param>
    /// <param name="startIndex">复制起始索引，从零开始</param>
    /// <param name="endIndex">复制结束索引</param>
    /// <returns>数组</returns>
    public static T[] Copy<T>(T[] sourceArray, int startIndex, int endIndex)
    {
        ArgumentCheck.Begin(sourceArray, "需要操作的数组").NotNull()
            .CheckGreaterThan(startIndex, "复制起始索引", 0, true)
            .CheckGreaterThan(endIndex, "复制结束索引", startIndex, false)
            .CheckLessThan(endIndex, "复制结束索引", sourceArray.Length, true);
        int len = endIndex - startIndex;
        T[] destination = new T[len];
        Array.Copy(sourceArray, startIndex, destination, 0, len);
        return destination;
    }
        
    /// <summary>
    /// 判断数组是否是空还是NULL
    /// <para>eg:Assert.IsTrue(ArrayHelper.IsEmpty(new int[0]));</para>
    /// </summary>
    /// <param name="data">数组</param>
    /// <returns>是否是空或者NULL</returns>
    public static bool IsEmpty(this Array data)
    {
        if(data == null || data.Length == 0)
        {
            return true;
        }
            
        return false;
    }
        
    /// <summary>
    /// 扩大或者缩小数组
    /// </summary>
    /// <typeparam name="T">泛型</typeparam>
    /// <param name="array">数组</param>
    /// <param name="newSizes">新数组大小</param>
    /// 时间：2016/8/30 16:57
    /// 备注：
    public static T[] Resize<T>(this T[] array, int newSizes)
    {
        Array.Resize<T>(ref array, newSizes);
        return array;
    }
        
    #endregion Methods
}