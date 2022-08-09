using BB.Tools.MultiLanuage;

namespace BB.BaseUI.WinForm;

///<summary>
///打开、保存文件对话框操作辅助类
///</summary>
public class FileDialogHelper
{
    #region 常用变量定义

    private static string _allFilter = "All File(*.*)|*.*";
    private static string _wordFilter = "Word(*.doc)|*.doc|Word(*.docx)|*.docx|All File(*.*)|*.*";
    private static string _excelFilter = "Excel(*.xls)|*.xls|Excel(*.xlsx)|*.xlsx|All File(*.*)|*.*";
    private static string _pdfFilter = "PDF(*.pdf)|*.pdf|All File(*.*)|*.*";
    private static string _imageFilter = "Image Files(*.BMP;*.bmp;*.JPG;*.jpg;*.GIF;*.gif;*.png;*.PNG)|(*.BMP;*.bmp;*.JPG;*.jpg;*.GIF;*.gif;*.png;*.PNG)|All File(*.*)|*.*";
    private static string _htmlFilter = "HTML files (*.html;*.htm)|*.html;*.htm|All files (*.*)|*.*";
    private static string _accessFilter = "Access(*.mdb)|*.mdb|All File(*.*)|*.*";
    private static string _zipFillter = "Zip(*.zip)|*.zip|Rar(*.rar)|*.rar|All files (*.*)|*.*";
    private const string ConfigFilter = "Configuration Files(*.cfg)|*.cfg|All File(*.*)|*.*";
    private static string _txtFilter = "Text (*.txt)|*.txt|All files (*.*)|*.*";
    private static string _xmlFilter = "XML Files(*.xml)|*.xml|All files (*.*)|*.*";
    private static string _rarFilter = "Rar(*.rar)|*.rar|All files (*.*)|*.*";
    private static string _sqliteFilter = "Sqlite Files(*.db)|*.db|All files (*.*)|*.*"; 

    #endregion

    ///<summary>
    ///私有构造函数
    ///</summary>
    private FileDialogHelper()
    {
    }

    #region 普通文件操作

    /// <summary>
    /// 打开所有文件
    /// </summary>
    /// <returns></returns>
    public static string OpenFile()
    {
        return Open("打开文件", _allFilter);
    }

    /// <summary>
    /// 打开所有文件
    /// </summary>
    /// <param name="multiselect">是否支持多选</param>
    /// <returns></returns>
    public static string OpenFile(bool multiselect)
    {
        return OpenFile(multiselect, "");
    }

    /// <summary>
    /// 打开所有文件
    /// </summary>
    /// <param name="multiselect">是否支持多选</param>
    /// <param name="fileName">文件名称</param>
    /// <returns></returns>
    public static string OpenFile(bool multiselect, string fileName)
    {
        return OpenFile(multiselect, fileName, null);
    }

    /// <summary>
    /// 打开所有文件
    /// </summary>
    /// <param name="multiselect">是否支持多选</param>
    /// <param name="fileName">文件名称</param>
    /// <param name="initialDirectory">初始化目录路径</param>
    /// <returns></returns>
    public static string OpenFile(bool multiselect, string fileName, string initialDirectory)
    {
        if (multiselect)
        {
            return OpenMultiselect("打开多个文件", _allFilter, fileName, initialDirectory);
        }
        else
        {
            return Open("打开文件", _allFilter, fileName, initialDirectory );
        }
    }

    /// <summary>
    /// 保存文件对话框,并返回保存全路径
    /// </summary>
    /// <returns></returns>
    public static string SaveFile()
    {
        return SaveFile(string.Empty);
    }

    /// <summary>
    /// 保存文件对话框,并返回保存全路径
    /// </summary>
    /// <param name="filename">文件名称</param>
    /// <returns></returns>
    public static string SaveFile(string filename)
    {
        return Save("保存文件", _allFilter, filename);
    }

    /// <summary>
    /// 保存文件对话框,并返回保存全路径
    /// </summary>
    /// <param name="filename">文件名称</param>
    /// <param name="initialDirectory">初始化目录路径</param>
    /// <returns></returns>
    public static string SaveFile(string filename, string initialDirectory)
    {
        return Save("保存文件", _allFilter, filename, initialDirectory);
    } 
    #endregion

    #region Txt相关对话框

    /// <summary>
    /// 打开Txt对话框
    /// </summary>
    /// <returns></returns>
    public static string OpenText()
    {
        return Open("选择文本文件选择", _txtFilter);
    }

    /// <summary>
    /// 打开Txt对话框
    /// </summary>
    /// <param name="multiselect">是否支持多选</param>
    /// <returns></returns>
    public static string OpenText(bool multiselect)
    {
        return OpenText(multiselect, "");
    }

    /// <summary>
    /// 打开Txt对话框
    /// </summary>
    /// <param name="multiselect">是否支持多选</param>
    /// <param name="fileName">文件名称</param>
    /// <returns></returns>
    public static string OpenText(bool multiselect, string fileName)
    {
        return OpenText(multiselect, fileName, null);
    }

    /// <summary>
    /// 打开Txt对话框
    /// </summary>
    /// <param name="multiselect">是否支持多选</param>
    /// <param name="fileName">文件名称</param>
    /// <param name="initialDirectory">初始化目录路径</param>
    /// <returns></returns>
    public static string OpenText(bool multiselect, string fileName, string initialDirectory)
    {
        if (multiselect)
        {
            return OpenMultiselect("选择多个文本文件", _txtFilter, fileName);
        }
        else
        {
            return Open("选择文本文件", _txtFilter, fileName);
        }
    }

    /// <summary>
    /// 保存Excel对话框,并返回保存全路径
    /// </summary>
    /// <returns></returns>
    public static string SaveText()
    {
        return SaveText(string.Empty);
    }

    /// <summary>
    /// 保存Excel对话框,并返回保存全路径
    /// </summary>
    /// <param name="filename">文件名称</param>
    /// <returns></returns>
    public static string SaveText(string filename)
    {
        return Save("保存文本文件", _txtFilter, filename);
    } 

    /// <summary>
    /// 保存Excel对话框,并返回保存全路径
    /// </summary>
    /// <param name="filename">文件名称</param>
    /// <param name="initialDirectory">初始化目录</param>
    /// <returns></returns>
    public static string SaveText(string filename, string initialDirectory)
    {
        return Save("保存文本文件", _txtFilter, filename, initialDirectory);
    }

    #endregion

    #region Excel相关对话框

    /// <summary>
    /// 打开Excel对话框
    /// </summary>
    /// <returns></returns>
    public static string OpenExcel()
    {
        return Open("选择Excel文件", _excelFilter);
    }
        
    /// <summary>
    /// 打开Excel对话框
    /// </summary>
    /// <param name="multiselect">是否支持多选</param>
    /// <returns></returns>
    public static string OpenExcel(bool multiselect)
    {
        return OpenExcel(multiselect, "");
    }
    /// <summary>
    /// 打开Excel对话框
    /// </summary>
    /// <param name="multiselect">是否支持多选</param>
    /// <param name="fileName">文件名称</param>
    /// <returns></returns>
    public static string OpenExcel(bool multiselect, string fileName)
    {
        return OpenExcel(multiselect, fileName, null);
    }

    /// <summary>
    /// 打开Excel对话框
    /// </summary>
    /// <returns></returns>
    public static string OpenExcel(bool multiselect, string fileName, string initialDirectory)
    {
        if (multiselect)
        {
            return OpenMultiselect("选择多个Excel文件", _excelFilter, fileName, initialDirectory);
        }
        else
        {
            return Open("选择Excel文件", _excelFilter, fileName, initialDirectory);
        }
    }

    /// <summary>
    /// 保存Excel对话框,并返回保存全路径
    /// </summary>
    /// <returns></returns>
    public static string SaveExcel()
    {
        return SaveExcel(string.Empty);
    }

    /// <summary>
    /// 保存Excel对话框,并返回保存全路径
    /// </summary>
    /// <returns></returns>
    public static string SaveExcel(string filename)
    {
        return Save("保存Excel", _excelFilter, filename);
    }

    /// <summary>
    /// 保存Excel对话框,并返回保存全路径
    /// </summary>
    /// <returns></returns>
    public static string SaveExcel(string filename, string initialDirectory)
    {
        return Save("保存Excel", _excelFilter, filename, initialDirectory);
    } 

    #endregion

    #region Word相关对话框

    /// <summary>
    /// 打开Word对话框
    /// </summary>
    /// <returns></returns>
    public static string OpenWord()
    {
        return Open("选择Word文件", _wordFilter);
    }
        
    /// <summary>
    /// 打开Word对话框
    /// </summary>
    /// <param name="multiselect">是否支持多选</param>
    /// <returns></returns>
    public static string OpenWord(bool multiselect)
    {
        return OpenWord(multiselect, "");
    }
    /// <summary>
    /// 打开Word对话框
    /// </summary>
    /// <param name="multiselect">是否支持多选</param>
    /// <param name="fileName">文件名称</param>
    /// <returns></returns>
    public static string OpenWord(bool multiselect, string fileName)
    {
        return OpenWord(multiselect, fileName, null);
    }

    /// <summary>
    /// 打开Word对话框
    /// </summary>
    /// <returns></returns>
    public static string OpenWord(bool multiselect, string fileName, string initialDirectory)
    {
        if (multiselect)
        {
            return OpenMultiselect("选择多个Word文件", _wordFilter, fileName, initialDirectory);
        }
        else
        {
            return Open("选择Word文件", _wordFilter, fileName, initialDirectory);
        }
    }

    /// <summary>
    /// 保存Word对话框,并返回保存全路径
    /// </summary>
    /// <returns></returns>
    public static string SaveWord()
    {
        return SaveWord(string.Empty);
    }

    /// <summary>
    /// 保存Word对话框,并返回保存全路径
    /// </summary>
    /// <returns></returns>
    public static string SaveWord(string filename)
    {
        return Save("保存Word", _wordFilter, filename);
    }

    /// <summary>
    /// 保存Word对话框,并返回保存全路径
    /// </summary>
    /// <returns></returns>
    public static string SaveWord(string filename, string initialDirectory)
    {
        return Save("保存Word", _wordFilter, filename, initialDirectory);
    } 

    #endregion

    #region PDF相关对话框

    /// <summary>
    /// 打开Pdf对话框
    /// </summary>
    /// <returns></returns>
    public static string OpenPdf()
    {
        return Open("选择PDF文件", _pdfFilter);
    }
        
    /// <summary>
    /// 打开Pdf对话框
    /// </summary>
    /// <param name="multiselect">是否支持多选</param>
    /// <returns></returns>
    public static string OpenPdf(bool multiselect)
    {
        return OpenPdf(multiselect, "");
    }
    /// <summary>
    /// 打开Pdf对话框
    /// </summary>
    /// <param name="multiselect">是否支持多选</param>
    /// <param name="fileName">文件名称</param>
    /// <returns></returns>
    public static string OpenPdf(bool multiselect, string fileName)
    {
        return OpenPdf(multiselect, fileName, null);
    }

    /// <summary>
    /// 打开Pdf对话框
    /// </summary>
    /// <returns></returns>
    public static string OpenPdf(bool multiselect, string fileName, string initialDirectory)
    {
        if (multiselect)
        {
            return OpenMultiselect("选择多个PDF文件", _pdfFilter, fileName, initialDirectory);
        }
        else
        {
            return Open("选择PDF文件", _pdfFilter, fileName, initialDirectory);
        }
    }

    /// <summary>
    /// 保存Pdf对话框,并返回保存全路径
    /// </summary>
    /// <returns></returns>
    public static string SavePdf()
    {
        return SavePdf(string.Empty);
    }

    /// <summary>
    /// 保存Pdf对话框,并返回保存全路径
    /// </summary>
    /// <returns></returns>
    public static string SavePdf(string filename)
    {
        return Save("保存PDF", _pdfFilter, filename);
    }

    /// <summary>
    /// 保存Pdf对话框,并返回保存全路径
    /// </summary>
    /// <returns></returns>
    public static string SavePdf(string filename, string initialDirectory)
    {
        return Save("保存PDF", _pdfFilter, filename, initialDirectory);
    } 

    #endregion

    #region HTML相关对话框

    /// <summary>
    /// 打开Html对话框
    /// </summary>
    /// <returns></returns>
    public static string OpenHtml()
    {
        return Open("选择Html文件", _htmlFilter);
    }

    /// <summary>
    /// 打开Html对话框
    /// </summary>
    /// <returns></returns>
    public static string OpenHtml(bool multiselect)
    {
        if (multiselect)
        {
            return OpenMultiselect("选择多个Html文件", _htmlFilter, "");
        }
        else
        {
            return Open("选择Html文件", _htmlFilter);
        }
    }

    /// <summary>
    /// 保存Html对话框,并返回保存全路径
    /// </summary>
    /// <returns></returns>
    public static string SaveHtml()
    {
        return SaveHtml(string.Empty);
    }

    /// <summary>
    /// 保存Html对话框,并返回保存全路径
    /// </summary>
    /// <returns></returns>
    public static string SaveHtml(string filename)
    {
        return Save("保存Html", _htmlFilter, filename);
    } 
        
    /// <summary>
    /// 保存Html对话框,并返回保存全路径
    /// </summary>
    /// <returns></returns>
    public static string SaveHtml(string filename, string initialDirectory)
    {
        return Save("保存Html", _htmlFilter, filename, initialDirectory);
    } 

    #endregion

    #region 压缩文件相关

    /// <summary>
    /// 压缩文件选择
    /// </summary>
    /// <returns></returns>
    public static string OpenZip()
    {
        return Open("选择压缩文件", _zipFillter);
    }
    /// <summary>
    /// 压缩文件选择
    /// </summary>
    /// <returns></returns>
    public static string OpenZip(string filename)
    {
        return Open("选择压缩文件", _zipFillter, filename);
    }                        
    /// <summary>
    /// 选择多个压缩文件
    /// </summary>
    /// <returns></returns>
    public static string OpenZip(bool multiselect)
    {
        return OpenZip(multiselect, "");
    }   
    /// <summary>
    /// 选择多个压缩文件
    /// </summary>
    /// <returns></returns>
    public static string OpenZip(bool multiselect, string fileName)
    {
        return OpenZip(multiselect, fileName, null);
    }
    /// <summary>
    /// 选择多个压缩文件
    /// </summary>
    /// <returns></returns>
    public static string OpenZip(bool multiselect, string fileName, string initialDirectory)
    {
        if (multiselect)
        {
            return OpenMultiselect("选择多个压缩文件", _zipFillter, fileName, initialDirectory);
        }
        else
        {
            return Open("选择压缩文件", _zipFillter, fileName, initialDirectory);
        }
    }


    /// <summary>
    /// 保存Zip压缩文件
    /// </summary>
    /// <returns></returns>
    public static string SaveZip()
    {
        return SaveZip(string.Empty);
    }

    /// <summary>
    /// 保存Zip压缩文件
    /// </summary>
    /// <returns></returns>
    public static string SaveZip(string filename)
    {
        return Save("保存压缩文件", _zipFillter, filename);
    } 

    /// <summary>
    /// 保存Zip压缩文件
    /// </summary>
    /// <returns></returns>
    public static string SaveZip(string filename, string initialDirectory)
    {
        return Save("保存压缩文件", _zipFillter, filename, initialDirectory);
    } 

    #endregion

    #region Rar压缩文件相关

    /// <summary>
    /// 压缩文件选择
    /// </summary>
    /// <returns></returns>
    public static string OpenRar()
    {
        return Open("选择RAR压缩文件", _rarFilter);
    }
    /// <summary>
    /// Rar压缩文件选择
    /// </summary>
    /// <returns></returns>
    public static string OpenRar(string filename)
    {
        return Open("选择RAR压缩文件", _rarFilter, filename);
    }
    /// <summary>
    /// 选择多个Rar压缩文件
    /// </summary>
    /// <returns></returns>
    public static string OpenRar(bool multiselect)
    {
        return OpenRar(multiselect, "");
    }
    /// <summary>
    /// 选择多个Rar压缩文件
    /// </summary>
    /// <returns></returns>
    public static string OpenRar(bool multiselect, string fileName)
    {
        return OpenRar(multiselect, fileName, null);
    }
    /// <summary>
    /// 选择多个Rar压缩文件
    /// </summary>
    /// <returns></returns>
    public static string OpenRar(bool multiselect, string fileName, string initialDirectory)
    {
        if (multiselect)
        {
            return OpenMultiselect("选择多个压缩文件", _rarFilter, fileName, initialDirectory);
        }
        else
        {
            return Open("选择压缩文件", _rarFilter, fileName, initialDirectory);
        }
    }


    /// <summary>
    /// 保存Rar压缩文件
    /// </summary>
    /// <returns></returns>
    public static string SaveRar()
    {
        return SaveRar(string.Empty);
    }

    /// <summary>
    /// 保存Rar压缩文件
    /// </summary>
    /// <returns></returns>
    public static string SaveRar(string filename)
    {
        return Save("保存压缩文件", _rarFilter, filename);
    }

    /// <summary>
    /// 保存Rar压缩文件
    /// </summary>
    /// <returns></returns>
    public static string SaveRar(string filename, string initialDirectory)
    {
        return Save("保存RAR压缩文件", _rarFilter, filename, initialDirectory);
    }

    #endregion

    #region Sqlite数据库文件相关

    /// <summary>
    /// Sqlite数据库文件选择
    /// </summary>
    /// <returns></returns>
    public static string OpenSqlite()
    {
        return Open("选择Sqlite数据库文件", _sqliteFilter);
    }
    /// <summary>
    /// Sqlite数据库选择
    /// </summary>
    /// <returns></returns>
    public static string OpenSqlite(string filename)
    {
        return Open("选择Sqlite数据库文件", _sqliteFilter, filename);
    }
    /// <summary>
    /// 选择多个Sqlite数据库
    /// </summary>
    /// <returns></returns>
    public static string OpenSqlite(bool multiselect)
    {
        return OpenSqlite(multiselect, "");
    }
    /// <summary>
    /// 选择多个Sqlite数据库
    /// </summary>
    /// <returns></returns>
    public static string OpenSqlite(bool multiselect, string fileName)
    {
        return OpenSqlite(multiselect, fileName, null);
    }
    /// <summary>
    /// 选择多个Sqlite数据库
    /// </summary>
    /// <returns></returns>
    public static string OpenSqlite(bool multiselect, string fileName, string initialDirectory)
    {
        if (multiselect)
        {
            return OpenMultiselect("选择多个Sqlite数据库文件", _sqliteFilter, fileName, initialDirectory);
        }
        else
        {
            return Open("选择Sqlite数据库文件", _sqliteFilter, fileName, initialDirectory);
        }
    }


    /// <summary>
    /// 保存Sqlite数据库
    /// </summary>
    /// <returns></returns>
    public static string SaveSqlite()
    {
        return SaveSqlite(string.Empty);
    }

    /// <summary>
    /// 保存Sqlite数据库
    /// </summary>
    /// <returns></returns>
    public static string SaveSqlite(string filename)
    {
        return Save("保存Sqlite数据库文件", _sqliteFilter, filename);
    }

    /// <summary>
    /// 保存Sqlite数据库
    /// </summary>
    /// <returns></returns>
    public static string SaveSqlite(string filename, string initialDirectory)
    {
        return Save("保存Sqlite数据库文件", _sqliteFilter, filename, initialDirectory);
    }

    #endregion

    #region Xml文件相关

    /// <summary>
    /// Xml文件选择
    /// </summary>
    /// <returns></returns>
    public static string OpenXml()
    {
        return Open("选择Xml文件", _xmlFilter);
    }
    /// <summary>
    /// Xml文件选择
    /// </summary>
    /// <returns></returns>
    public static string OpenXml(string filename)
    {
        return Open("选择Xml文件", _xmlFilter, filename);
    }
    /// <summary>
    /// 选择多个Xml文件
    /// </summary>
    /// <returns></returns>
    public static string OpenXml(bool multiselect)
    {
        return OpenXml(multiselect, "");
    }
    /// <summary>
    /// 选择多个Xml文件
    /// </summary>
    /// <returns></returns>
    public static string OpenXml(bool multiselect, string fileName)
    {
        return OpenXml(multiselect, fileName, null);
    }
    /// <summary>
    /// 选择多个Xml文件
    /// </summary>
    /// <returns></returns>
    public static string OpenXml(bool multiselect, string fileName, string initialDirectory)
    {
        if (multiselect)
        {
            return OpenMultiselect("选择多个Xml文件", _xmlFilter, fileName, initialDirectory);
        }
        else
        {
            return Open("选择Xml文件", _xmlFilter, fileName, initialDirectory);
        }
    }

    /// <summary>
    /// 保存Xml文件
    /// </summary>
    /// <returns></returns>
    public static string SaveXml()
    {
        return SaveXml(string.Empty);
    }

    /// <summary>
    /// 保存Xml数据库
    /// </summary>
    /// <returns></returns>
    public static string SaveXml(string filename)
    {
        return Save("保存Xml文件", _xmlFilter, filename);
    }

    /// <summary>
    /// 保存Xml数据库
    /// </summary>
    /// <returns></returns>
    public static string SaveXml(string filename, string initialDirectory)
    {
        return Save("保存Xml文件", _xmlFilter, filename, initialDirectory);
    }

    #endregion

    #region 图片相关
    /// <summary>
    /// 打开图片文件
    /// </summary>
    /// <returns></returns>
    public static string OpenImage()
    {
        return Open("选择图片文件", _imageFilter);
    }

    /// <summary>
    /// 打开图片文件
    /// </summary>
    /// <returns></returns>
    public static string OpenImage(bool multiselect)
    {
        return OpenImage(multiselect, "");
    }
    /// <summary>
    /// 打开图片文件
    /// </summary>
    /// <returns></returns>
    public static string OpenImage(bool multiselect, string fileName)
    {
        return OpenImage(multiselect, fileName, null);
    }

    /// <summary>
    /// 打开图片文件
    /// </summary>
    /// <returns></returns>
    public static string OpenImage(bool multiselect, string fileName, string initialDirectory)
    {
        if (multiselect)
        {
            return OpenMultiselect("选择多个图片文件", _imageFilter, fileName, initialDirectory);
        }
        else
        {
            return Open("选择图片文件", _imageFilter, fileName, initialDirectory);
        }
    }

    /// <summary>
    /// 保存图片对话框,并返回保存全路径
    /// </summary>
    /// <returns></returns>
    public static string SaveImage()
    {
        return SaveImage(string.Empty);
    } 

    /// <summary>
    /// 保存图片对话框并设置默认文件名,并返回保存全路径
    /// </summary>
    /// <returns></returns>
    public static string SaveImage(string filename)
    {
        return Save("保存图片", _imageFilter, filename);
    }

    /// <summary>
    /// 保存图片对话框并设置默认文件名,并返回保存全路径
    /// </summary>
    /// <returns></returns>
    public static string SaveImage(string filename, string initialDirectory)
    {
        return Save("保存图片", _imageFilter, filename, initialDirectory);
    }

    /// <summary>
    /// 是否图片文件
    /// </summary>
    /// <param name="extension"></param>
    /// <returns></returns>
    public static bool IsImageFile(string extension)
    {
        List<string> imageExList = new List<string>() { ".bmp", ".jpg", ".gif", ".png", ".jpeg", ".tif" };
        return imageExList.Contains(extension.ToLower());
    }

    #endregion

    #region 数据库备份还原

    /// <summary>
    /// 保存数据库备份对话框
    /// </summary>
    /// <returns>数据库备份路径</returns>
    public static string SaveAccessDb()
    {
        return Save("数据库备份", _accessFilter);
    }

    /// <summary>
    /// 保存Access备份目录
    /// </summary>
    /// <returns></returns>
    public static string SaveBakDb()
    {
        return Save("数据库备份", "Access(*.bak)|*.bak");
    }

    /// <summary>
    /// 打开Access备份目录
    /// </summary>
    /// <param name="file">备份文件名</param>
    /// <returns></returns>
    public static string OpenBakDb(string file)
    {
        return Open("数据库还原", "Access(*.bak)|*.bak", file);
    }

    /// <summary>
    /// 数据库还原对话框
    /// </summary>
    /// <returns>数据库还原路径</returns>
    public static string OpenAccessDb()
    {
        return Open("数据库还原", _accessFilter);
    } 
    #endregion

    #region 配置文件
    /// <summary>
    /// 保存配置文件备份对话框
    /// </summary>
    /// <returns>配置文件备份路径</returns>
    public static string SaveConfig()
    {
        return Save("配置文件备份", ConfigFilter);
    }

    /// <summary>
    /// 配置文件还原对话框
    /// </summary>
    /// <returns>配置文件还原路径</returns>
    public static string OpenConfig()
    {
        return Open("配置文件还原", ConfigFilter);
    } 
    #endregion

    #region 通用函数

    /// <summary>
    /// 打开文件夹浏览对话框
    /// </summary>
    /// <returns></returns>
    public static string OpenDir()
    {
        return OpenDir(string.Empty);
    }

    /// <summary>
    /// 以指定目录打开文件夹浏览对话框
    /// </summary>
    /// <param name="selectedPath">指定目录</param>
    /// <returns></returns>
    public static string OpenDir(string selectedPath)
    {
        FolderBrowserDialog dialog = new FolderBrowserDialog();
        dialog.Description = JsonLanguage.Default.GetString("请选择路径");
        dialog.SelectedPath = selectedPath;
        if (dialog.ShowDialog() == DialogResult.OK)
        {
            return dialog.SelectedPath;
        }
        else
        {
            return string.Empty;
        }
    }
    /// <summary>
    /// 打开多个文件列表
    /// </summary>
    /// <param name="title">对话框标题</param>
    /// <param name="filter">后缀名过滤</param>
    /// <param name="filename">默认文件名</param>
    /// <returns></returns>
    public static string OpenMultiselect(string title, string filter, string filename)
    {
        return OpenMultiselect(title, filter, filename, null);
    }

    /// <summary>
    /// 打开多个文件列表
    /// </summary>
    /// <param name="title">对话框标题</param>
    /// <param name="filter">后缀名过滤</param>
    /// <param name="filename">默认文件名</param>
    /// <param name="initialDirectory">初始化目录</param>
    /// <returns></returns>
    public static string OpenMultiselect(string title, string filter, string filename, string initialDirectory)
    {
        //多语言支持
        title = JsonLanguage.Default.GetString(title);

        OpenFileDialog dialog = new OpenFileDialog();
        dialog.Filter = filter;
        dialog.Title = title;
        dialog.RestoreDirectory = true;
        dialog.FileName = filename;
        dialog.Multiselect = true;
        if (!string.IsNullOrEmpty(initialDirectory))
        {
            dialog.InitialDirectory = initialDirectory;
        }

        string result = "";
        if (dialog.ShowDialog() == DialogResult.OK)
        {
            foreach (string strFile in dialog.FileNames)
            {
                result += $"{strFile},";
            }
        }

        return result.Trim(',');
    }

    /// <summary>
    /// 以指定标题打开文件对话框
    /// </summary>
    /// <param name="title">对话框标题</param>
    /// <param name="filter">后缀名过滤</param>
    /// <param name="filename">默认文件名</param>
    /// <returns></returns>
    public static string Open(string title, string filter, string filename)
    {
        return Open(title, filter, filename, null);
    }

    /// <summary>
    /// 以指定标题打开文件对话框
    /// </summary>
    /// <param name="title">对话框标题</param>
    /// <param name="filter">后缀名过滤</param>
    /// <param name="filename">默认文件名</param>
    /// <param name="initialDirectory">初始化目录</param>
    /// <returns></returns>
    public static string Open(string title, string filter, string filename, string initialDirectory)
    {
        //多语言支持
        title = JsonLanguage.Default.GetString(title);

        OpenFileDialog dialog = new OpenFileDialog();
        dialog.Filter = filter;
        dialog.Title = title;
        dialog.RestoreDirectory = true;
        dialog.FileName = filename;
        if (!string.IsNullOrEmpty(initialDirectory))
        {
            dialog.InitialDirectory = initialDirectory;
        }

        if (dialog.ShowDialog() == DialogResult.OK)
        {
            return dialog.FileName;
        }
        else
        {
            return string.Empty;
        }
    }

    /// <summary>
    /// 以指定标题打开文件对话框
    /// </summary>
    /// <param name="title">对话框标题</param>
    /// <param name="filter">后缀名过滤</param>
    /// <returns></returns>
    public static string Open(string title, string filter)
    {
        return Open(title, filter, string.Empty);
    }

    /// <summary>
    /// 以指定的标题弹出保存文件对话框
    /// </summary>
    /// <param name="title">对话框标题</param>
    /// <param name="filter">后缀名过滤</param>
    /// <param name="filename">默认文件名</param>
    /// <returns></returns>
    public static string Save(string title, string filter, string filename)
    {
        return Save(title, filter, filename, "");
    }

    /// <summary>
    /// 以指定的标题弹出保存文件对话框
    /// </summary>
    /// <param name="title">对话框标题</param>
    /// <param name="filter">后缀名过滤</param>
    /// <param name="filename">默认文件名</param>
    /// <param name="initialDirectory">初始化目录</param>
    /// <returns></returns>
    public static string Save(string title, string filter, string filename, string initialDirectory)
    {
        //多语言支持
        title = JsonLanguage.Default.GetString(title);

        SaveFileDialog dialog = new SaveFileDialog();
        dialog.Filter = filter;
        dialog.Title = title;
        dialog.FileName = filename;
        dialog.RestoreDirectory = true;
        if (!string.IsNullOrEmpty(initialDirectory))
        {
            dialog.InitialDirectory = initialDirectory;
        }

        if (dialog.ShowDialog() == DialogResult.OK)
        {
            return dialog.FileName;
        }
        return string.Empty;
    }

    /// <summary>
    /// 以指定的标题弹出保存文件对话框
    /// </summary>
    /// <param name="title">对话框标题</param>
    /// <param name="filter">后缀名过滤</param>
    /// <returns></returns>
    public static string Save(string title, string filter)
    {
        return Save(title, filter, string.Empty);
    }

    #endregion

    #region 获取颜色对话框的颜色
    /// <summary>
    /// 获取颜色对话框的值
    /// </summary>
    /// <returns></returns>
    public static Color PickColor()
    {
        Color result = SystemColors.Control;
        ColorDialog form = new ColorDialog();
        if (DialogResult.OK == form.ShowDialog())
        {
            result = form.Color;
        }
        return result;
    }

    /// <summary>
    /// 获取颜色对话框的值
    /// </summary>
    /// <param name="color">默认颜色</param>
    /// <returns></returns>
    public static Color PickColor(Color color)
    {
        Color result = SystemColors.Control;
        ColorDialog form = new ColorDialog();
        form.Color = color;
        if (DialogResult.OK == form.ShowDialog())
        {
            result = form.Color;
        }
        return result;
    } 
    #endregion
}