using BB.Tools.MultiLanuage;

namespace BB.Tools.Validation;

/// <summary>
/// 该辅助类提供一些辅助类使用的键值，方便使用多语言统一处理
/// </summary>
internal class ResourceKey
{                
    /// <summary>
    ///   查找类似 查询条件组中的操作类型错误，只能为And或者Or。 的本地化字符串。
    /// </summary>
    internal static string FilterGroupOperateError => JsonLanguage.Default.GetString("查询条件组中的操作类型错误，只能为And或者Or。");

    /// <summary>
    ///   查找类似 指定的属性“{0}”在类型“{1}”中不存在。 的本地化字符串。
    /// </summary>
    internal static string FilterRuleFieldInTypeNotFound => JsonLanguage.Default.GetString("指定的属性“{0}”在类型“{1}”中不存在。");

    /// <summary>
    ///   查找类似 创建名称为“{0}”的日志实例时“{1}”返回空实例。 的本地化字符串。
    /// </summary>
    internal static string LoggingCreateLogInstanceReturnNull => JsonLanguage.Default.GetString("创建名称为“{0}”的日志实例时“{1}”返回空实例。");

    /// <summary>
    ///   查找类似 当前Http上下文中不存在Request有效范围的Mef部件容器。 的本地化字符串。
    /// </summary>
    internal static string MefHttpContextItemsNotFoundRequestContainer => JsonLanguage.Default.GetString("当前Http上下文中不存在Request有效范围的Mef部件容器。");

    /// <summary>
    ///   查找类似 指定对象中不存在名称为“{0}”的属性。 的本地化字符串。
    /// </summary>
    internal static string ObjectExtensionsPropertyNameNotExistsInType => JsonLanguage.Default.GetString("指定对象中不存在名称为“{0}”的属性。");

    /// <summary>
    ///   查找类似 指定名称“{0}”的属性类型不是“{1}”。 的本地化字符串。
    /// </summary>
    internal static string ObjectExtensionsPropertyNameNotFixedType => JsonLanguage.Default.GetString("指定名称“{0}”的属性类型不是“{1}”。");


    /// <summary>
    ///   查找类似 参数“{0}”的值必须在“{1}”与“{2}”之间。 的本地化字符串。
    /// </summary>
    internal static string ParameterCheckBetween => JsonLanguage.Default.GetString("参数“{0}”的值必须在“{1}”与“{2}”之间。");

    /// <summary>
    ///   查找类似 参数“{0}”的值必须在“{1}”与“{2}”之间，且不能等于“{3}”。 的本地化字符串。
    /// </summary>
    internal static string ParameterCheckBetweenNotEqual => JsonLanguage.Default.GetString("参数“{0}”的值必须在“{1}”与“{2}”之间，且不能等于“{3}”。");

    /// <summary>
    ///   查找类似 指定的目录路径“{0}”不存在。 的本地化字符串。
    /// </summary>
    internal static string ParameterCheckDirectoryNotExists => JsonLanguage.Default.GetString("指定的目录路径“{0}”不存在。");

    /// <summary>
    ///   查找类似 文件类型不合法，必须为“{0}”后缀文件。 的本地化字符串。
    /// </summary>
    internal static string ParameterCheckFileExtCompare => JsonLanguage.Default.GetString("文件类型不合法，必须为“{0}”后缀文件。");

    /// <summary>
    ///   查找类似 指定的文件路径“{0}”不存在。 的本地化字符串。
    /// </summary>
    internal static string ParameterCheckFileNotExists => JsonLanguage.Default.GetString("指定的文件路径“{0}”不存在。");

    /// <summary>
    ///   查找类似 参数“{0}”的值不是合法IP4地址。 的本地化字符串。
    /// </summary>
    internal static string ParameterCheckIp4Address => JsonLanguage.Default.GetString("参数“{0}”的值不是合法IP4地址。");

    /// <summary>
    ///   查找类似 指定的文件路径“{0}”非法。 的本地化字符串。
    /// </summary>
    internal static string ParameterCheckIsFilePath => JsonLanguage.Default.GetString("指定的文件路径“{0}”非法。");

    /// <summary>
    ///   查找类似 参数“{0}”不能匹配&quot;{1}&quot;格式。 的本地化字符串。
    /// </summary>
    internal static string ParameterCheckMatch => JsonLanguage.Default.GetString("参数“{0}”不能匹配&quot;{1}&quot;格式。");

    /// <summary>
    ///   查找类似 参数“{0}”格式不合法。 的本地化字符串。
    /// </summary>
    internal static string ParameterCheckMatch2 => JsonLanguage.Default.GetString("参数“{0}”格式不合法。");

    /// <summary>
    ///   查找类似 参数“{0}”的值不能为Guid.Empty 的本地化字符串。
    /// </summary>
    internal static string ParameterCheckNotEmptyGuid => JsonLanguage.Default.GetString("参数“{0}”的值不能为Guid.Empty");

    /// <summary>
    ///   查找类似 参数“{0}”不能等于&quot;{1}&quot;。 的本地化字符串。
    /// </summary>
    internal static string ParameterCheckNotEqual => JsonLanguage.Default.GetString("参数“{0}”不能等于&quot;{1}&quot;。");

    /// <summary>
    ///   查找类似 参数“{0}”的值必须大于“{1}”。 的本地化字符串。
    /// </summary>
    internal static string ParameterCheckNotGreaterThan => JsonLanguage.Default.GetString("参数“{0}”的值必须大于“{1}”。");

    /// <summary>
    ///   查找类似 参数“{0}”的值必须大于或等于“{1}”。 的本地化字符串。
    /// </summary>
    internal static string ParameterCheckNotGreaterThanOrEqual => JsonLanguage.Default.GetString("参数“{0}”的值必须大于或等于“{1}”。");

    /// <summary>
    ///   查找类似 参数“{0}”的值必须小于“{1}”。 的本地化字符串。
    /// </summary>
    internal static string ParameterCheckNotLessThan => JsonLanguage.Default.GetString("参数“{0}”的值必须小于“{1}”。");

    /// <summary>
    ///   查找类似 参数“{0}”的值必须小于或等于“{1}”。 的本地化字符串。
    /// </summary>
    internal static string ParameterCheckNotLessThanOrEqual => JsonLanguage.Default.GetString("参数“{0}”的值必须小于或等于“{1}”。");

    /// <summary>
    ///   查找类似 参数“{0}”不能为空引用。 的本地化字符串。
    /// </summary>
    internal static string ParameterCheckNotNull => JsonLanguage.Default.GetString("参数“{0}”不能为空引用。");

    /// <summary>
    ///   查找类似 参数“{0}”不能为空引用或空集合。 的本地化字符串。
    /// </summary>
    internal static string ParameterCheckNotNullOrEmptyCollection => JsonLanguage.Default.GetString("参数“{0}”不能为空引用或空集合。");

    /// <summary>
    ///   查找类似 参数“{0}”不能为空引用或空字符串。 的本地化字符串。
    /// </summary>
    internal static string ParameterCheckNotNullOrEmptyString => JsonLanguage.Default.GetString("参数“{0}”不能为空引用或空字符串。");

    /// <summary>
    ///   查找类似 参数“{0}”的值不是合法端口。 的本地化字符串。
    /// </summary>
    internal static string ParameterCheckPort => JsonLanguage.Default.GetString("参数“{0}”的值不是合法端口。");

    /// <summary>
    ///   查找类似 该参数类型{0}不能序列化！ 的本地化字符串。
    /// </summary>
    internal static string ParameterCheckSerializable => JsonLanguage.Default.GetString("该参数类型{0}不能序列化！");

    /// <summary>
    ///   查找类似 参数“{0}”长度需为&quot;{1}&quot;个长度。 的本地化字符串。
    /// </summary>
    internal static string ParameterCheckStringLength => JsonLanguage.Default.GetString("参数“{0}”长度需为&quot;{1}&quot;个长度。");

    /// <summary>
    ///   查找类似 参数“{0}”的值不是合法URL。 的本地化字符串。
    /// </summary>
    internal static string ParameterCheckUrl => JsonLanguage.Default.GetString("参数“{0}”的值不是合法URL。");

    /// <summary>
    ///   查找类似 参数key的长度必须为8或24，当前为{0}。 的本地化字符串。
    /// </summary>
    internal static string SecurityDesKeyLenght => JsonLanguage.Default.GetString("参数key的长度必须为8或24，当前为{0}。");

    /// <summary>
    ///   查找类似 参数hashType必须为MD5或SHA1 的本地化字符串。
    /// </summary>
    internal static string SecurityRsaSignHashType => JsonLanguage.Default.GetString("参数hashType必须为MD5或SHA1");


    /// <summary>
    /// 参数'{0}'的值不能为空字符串。
    /// </summary>
    internal static string ExceptionEmptyString => JsonLanguage.Default.GetString("参数'{0}'的值不能为空字符串。");

    /// <summary>
    /// 参数'{0}'的名称不能为空引用或空字符串。
    /// </summary>
    internal static string ExceptionInvalidNullNameArgument => JsonLanguage.Default.GetString("参数'{0}'的名称不能为空引用或空字符串。");

    /// <summary>
    /// 数值必须大于0字节。
    /// </summary>
    internal static string ExceptionMustBeGreaterThanZeroBytes => JsonLanguage.Default.GetString("数值必须大于0字节。");

    /// <summary>
    /// 无效的类型，期待的类型必须为'{0}'。
    /// </summary>
    internal static string ExceptionExpectedType => JsonLanguage.Default.GetString("无效的类型，期待的类型必须为'{0}'。");

    /// <summary>
    /// {0}不是{1}的一个有效值
    /// </summary>
    internal static string ExceptionEnumerationNotDefined => JsonLanguage.Default.GetString("{0}不是{1}的一个有效值");
}