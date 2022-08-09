using System.Text;
using BB.Tools.Extension;
using BB.Tools.File;
using Furion.Logging.Extensions;
using NVelocity;
using NVelocity.App;
using NVelocity.Exception;
using NVelocity.Runtime;

namespace BB.Tools.Format;

/// <summary>
/// 基于NVelocity的模板文件生成辅助类
/// </summary>
public sealed class NVelocityHelper
{
    private const string NVELOCITY_PROPERTY = "nvelocity.properties";
    private VelocityContext _context;
    private string _templateDir;
    private Dictionary<string, Template> _templateDictionary = new();

    /// <summary>
    /// 存放键值的字典内容
    /// </summary>
    private Dictionary<string, object> _keyObjDict = new();

    private string _directoryOfOutput = ""; //输出文件的文件夹名称
    private string _fileNameOfOutput; //输出的文件名称
    private string _fileNamePrefixOfOutput = ""; //输出的文件名称前缀
    private string _childFileNameOfOutput; //输出的文件名称
    private string _childFileNamePrefixOfOutput = ""; //输出的文件名称前缀

    /// <summary>
    /// 输出文件的文件夹名称, 如"Entity","Common"等
    /// 支持绝对路径和相对路径，相对路径时默认在本程序根目录下
    /// </summary>
    public string DirectoryOfOutput
    {
        set => _directoryOfOutput = value;
        get => _directoryOfOutput;
    }

    /// <summary>
    /// 输出的完整文件名称. 要带后缀名.
    /// 默认的文件名称为模板文件的名称,没有后缀名.
    /// 所以建议的模版文件名应是A.cs.vm
    /// </summary>
    public string FileNameOfOutput
    {
        set => _fileNameOfOutput = value;
        get => _fileNameOfOutput;
    }

    /// <summary>
    /// 输出的文件名称前缀
    /// </summary>
    public string FileNamePrefixOfOutput
    {
        set => _fileNamePrefixOfOutput = value;
        get => _fileNamePrefixOfOutput;
    }

    /// <summary>
    /// 输出的完整子模块文件名称. 要带后缀名.
    /// 默认的文件名称为模板文件的名称,没有后缀名.
    /// 所以建议的模版文件名应是A.cs.vm
    /// </summary>
    public string ChildFileNameOfOutput
    {
        set => _childFileNameOfOutput = value;
        get => _childFileNameOfOutput;
    }

    /// <summary>
    /// 输出的子模块文件名称前缀
    /// </summary>
    public string ChildFileNamePrefixOfOutput
    {
        set => _childFileNamePrefixOfOutput = value;
        get => _childFileNamePrefixOfOutput;
    }

    /// <summary>
    /// 添加一个键值对象
    /// </summary>
    /// <param name="key">键，不可重复</param>
    /// <param name="value">值</param>
    /// <returns></returns>
    public NVelocityHelper AddKeyValue(string key, object value)
    {
        if (!_keyObjDict.ContainsKey(key))
        {
            _keyObjDict.Add(key, value);
        }
        return this;
    }

    /// <summary>
    /// 参数化构造函数，根据模板文件/文件夹构造相应的对象，并初始化
    /// </summary>
    /// <param name="templateDir">模板文件</param>
    /// <param name="directory">输出目录</param>
    /// <param name="fileNamePrefixOfOutput">输出文件的前缀</param>
    public NVelocityHelper(string templateDir, string directory = "", string fileNamePrefixOfOutput = "",
        string childFileNamePrefixOfOutput = "")
    {
        _templateDir = templateDir;
        
        _directoryOfOutput = Path.IsPathRooted(directory) ? directory :
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, directory); // 默认情况下,放到当前目录下面
        DirectoryUtil.CreateDirectory(_directoryOfOutput);
        
        _fileNamePrefixOfOutput = fileNamePrefixOfOutput;
        _childFileNamePrefixOfOutput = childFileNamePrefixOfOutput;

        InitTemplateEngine(); //初始化模板引擎，尽量放最后
    }

    /// <summary>
    /// 默认构造函数，用于字符串对象的合并
    /// </summary>
    public NVelocityHelper()
    {
    }

    /// <summary>
    ///根据模板创建输出的文件
    /// </summary>
    public void ExecuteFile()
    {
        if (!_templateDictionary.Any()) return;
        
        InitContext();
        foreach (KeyValuePair<string, Template> keyValuePair in _templateDictionary)
        {
            string templatePath = Path.GetFullPath(keyValuePair.Key);
            // 如果文件不存在则创建文件
            FileUtil.CreateFile(templatePath);

            //LogTextHelper.Debug(string.Format("Class file output path:{0}", fileName));

            using var writer = new StreamWriter(keyValuePair.Key, false);
            keyValuePair.Value.Merge(_context, writer);
        }
    }

    /// <summary>
    /// 根据模板输出字符串内容
    /// </summary>
    /// <returns></returns>
    public string ExecuteString()
    {
        if (!_templateDictionary.Any()) return string.Empty;
        
        InitContext();
        var template = _templateDictionary.FirstOrDefault();
        var writer = new StringWriter();
        template.Value.Merge(_context, writer);
        return writer.GetStringBuilder().ToString();
    }

    /// <summary>
    /// 合并字符串的内容
    /// </summary>
    /// <returns></returns>
    public string ExecuteMergeString(string inputString)
    {
        VelocityEngine templateEngine = new VelocityEngine();
        templateEngine.Init();

        InitContext();

        StringWriter writer = new StringWriter();
        templateEngine.Evaluate(_context, writer, "mystring", inputString);

        return writer.GetStringBuilder().ToString();
    }

    /// <summary>
    /// 初始化模板引擎
    /// </summary>
    private void InitTemplateEngine()
    {
        try
        {
            // Velocity.Init(NVELOCITY_PROPERTY);
            VelocityEngine templateEngine = new VelocityEngine();
            templateEngine.SetProperty(RuntimeConstants.RESOURCE_LOADER, "file");

            templateEngine.SetProperty(RuntimeConstants.INPUT_ENCODING, "utf-8");
            templateEngine.SetProperty(RuntimeConstants.OUTPUT_ENCODING, "utf-8");

            //模板的缓存设置
            templateEngine.SetProperty(RuntimeConstants.FILE_RESOURCE_LOADER_CACHE, true);              //是否缓存
            templateEngine.SetProperty("file.resource.loader.modificationCheckInterval", (long)30);    //缓存时间(秒)
            
            //如果设置了FILE_RESOURCE_LOADER_PATH属性，那么模板文件的基础路径就是基于这个设置的目录，否则默认当前运行目录
            templateEngine.SetProperty(RuntimeConstants.FILE_RESOURCE_LOADER_PATH, AppDomain.CurrentDomain.BaseDirectory);

            templateEngine.Init();

            // 传入的模版是单个文件路径还是文件夹路径
            if (FileUtil.IsExistFile(_templateDir))
            {
                // 拼接生成文件的输出路径
                string fileOutput = Path.Combine(_directoryOfOutput, GetFileNameFromTemplate(_templateDir));
                var template = templateEngine.GetTemplate(_templateDir);
                _templateDictionary.Add(fileOutput, template);
            }
            else if (DirectoryUtil.IsExistDirectory(_templateDir))
            {
                var filePathList = DirectoryUtil.GetFileNames(_templateDir, "*.vm", true);
                foreach (string filePath in filePathList)
                {
                    if (!_childFileNamePrefixOfOutput.IsNullOrEmpty() && filePath.Contains("Frm") && !filePath.Contains("Master"))
                        continue;
                    if (_childFileNamePrefixOfOutput.IsNullOrEmpty() && (filePath.Contains("{CN}") || filePath.Contains("Master")))
                        continue;
                    string fileOutput = Path.Combine(_directoryOfOutput, GetFileNameFromTemplate(filePath));
                    Template template = templateEngine.GetTemplate(filePath);
                    _templateDictionary.Add(fileOutput, template);
                }
            }
            else
            {
                throw new Exception($"非法的模版文件/文件夹{_templateDir}");
            }
        }
        catch (ResourceNotFoundException re)
        {
            string error = string.Format("Cannot find template " + _templateDir);

            error.LogError();
            throw new Exception(error, re);
        }
        catch (ParseErrorException pee)
        {
            string error = string.Format("Syntax error in template " + _templateDir + ":" + pee.StackTrace);
            error.LogError();
            throw new Exception(error, pee);
        }
    }

    /// <summary>
    /// 初始化上下文的内容
    /// </summary>
    private void InitContext()
    {
        _context = new VelocityContext();
        foreach (string key in _keyObjDict.Keys)
        {
            _context.Put(key, _keyObjDict[key]);
        }
    }

    #region 辅助方法

    /// <summary>
    /// 从模板文件名称获取输出文件名的方法
    /// </summary>
    /// <param name="templateFileName">带后缀名的模板文件名</param>
    /// <returns>输出的文件名(无后缀名)</returns>
    private string GetFileNameFromTemplate(string templateFileName)
    {
        return !_fileNameOfOutput.IsNullOrEmpty()
            ? _fileNameOfOutput
            : Path.GetFileName(templateFileName)
                .Replace("{N}", _fileNamePrefixOfOutput)
                .Replace("{CN}", _childFileNamePrefixOfOutput)
                .Replace("Master", "")
                .Replace(".vm", "");
    }

    /// <summary>
    /// Gets the sourcefile path according the mainSetting values. End by slash char("/").
    /// </summary>
    /// <param name="outputDir">The output directory.</param>
    /// <param name="rootNameSpace">The root namespace of the project.</param>
    /// <returns>The valid directory path end by slash("/").</returns>
    public static string GetFilePath(string outputDir, string rootNameSpace)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(CheckEndBySlash(outputDir));
        sb.Append(CheckEndBySlash(rootNameSpace.Replace(StringConstants.DOT, StringConstants.SLASH)));

        return sb.ToString();
    }

    /// <summary>
    /// 确保路径以"/"结束
    /// </summary>
    /// <param name="pathName">路径名称</param>
    /// <returns>以"/"结束德路径名称</returns>
    public static string CheckEndBySlash(string pathName)
    {
        if (!string.IsNullOrEmpty(pathName) && !pathName.EndsWith(StringConstants.SLASH))
        {
            return pathName + StringConstants.SLASH;
        }
        return pathName;
    }

    /// <summary>
    /// 读取模版内容
    /// </summary>
    /// <param name="path">路径</param>
    /// <returns>模版内容</returns>
    public static string GetTemplateContent(string path)
    {
        var sr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + path);
        string templateString = sr.ReadToEnd();
        sr.Close();
        return templateString;
    }

    #endregion
}