using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using BB.Tools.Format;

namespace BB.Tools.File;

/// <summary>
/// 常用的文件操作辅助类FileUtil
/// </summary>
public class FileUtil
{
    #region Stream、byte[] 和 文件之间的转换

    /// <summary>
    /// 将流读取到缓冲区中
    /// </summary>
    /// <param name="stream">原始流</param>
    public static byte[] StreamToBytes(Stream stream)
    {
        try
        {
            //创建缓冲区
            byte[] buffer = new byte[stream.Length];

            //读取流
            stream.Read(buffer, 0, Convert.ToInt32(stream.Length));
            stream.Seek(0, SeekOrigin.Begin);

            //返回流
            return buffer;
        }
        catch (IOException ex)
        {
            throw;
        }
        finally
        {
            //关闭流
            stream.Close();
        }
    }

    /// <summary>
    /// 将 byte[] 转成 Stream
    /// </summary>
    public static Stream BytesToStream(byte[] bytes)
    {
        Stream stream = new MemoryStream(bytes);
        return stream;
    }

    /// <summary>
    /// 从byte[]转换为文件
    /// </summary>
    /// <param name="bytes">文件字节数组</param>
    /// <param name="fileName">文件名称</param>
    public static void BytesToFile(byte[] bytes, string fileName)
    {
        MemoryStream memoryStream = new MemoryStream(bytes);
        FileStream fileStream = new FileStream(fileName, FileMode.OpenOrCreate);
        memoryStream.WriteTo(fileStream);
        memoryStream.Close();
        fileStream.Close();
    }

    /// <summary>
    /// 转换byte[]数组为Image对象
    /// </summary>
    /// <param name="bytes">byte[]数组</param>
    /// <returns></returns>
    public static Image BytesToImage(byte[] bytes)
    {
        MemoryStream memoryStream = new MemoryStream(bytes)
        {
            Position = 0L
        };
        Image image = Image.FromStream(memoryStream);
        memoryStream.Close();
        return image;
    }

    /// <summary>
    /// 把Image对象转换为Byte数组
    /// </summary>
    /// <param name="image">图片Image对象</param>
    /// <returns>字节集合</returns>
    public static byte[] ImageToBytes(Image image)
    {
        byte[] bytes = null;
        if (image != null)
        {
            lock (image)
            {
                using MemoryStream ms = new MemoryStream();
                image.Save(ms, ImageFormat.Png);
                bytes = ms.GetBuffer();
            }
        }
        return bytes;
    }

    /// <summary>
    /// 把Image对象转换为Byte数组
    /// </summary>
    /// <param name="image">image对象</param>
    /// <param name="imageFormat">图片格式（后缀名）</param>
    /// <returns></returns>
    public static byte[] ImageToBytes(Image image, ImageFormat imageFormat)
    {
        if (image == null) { return null; }
        byte[] data = null;
        using MemoryStream ms = new MemoryStream();
        using Bitmap bitmap = new Bitmap(image);
        bitmap.Save(ms, imageFormat);
        ms.Position = 0;
        data = new byte[ms.Length];
        ms.Read(data, 0, Convert.ToInt32(ms.Length));
        ms.Flush();

        return data;
    }

    /// <summary>
    /// 将 Stream 写入文件
    /// </summary>
    public static void StreamToFile(Stream stream, string fileName)
    {
        // 把 Stream 转换成 byte[]
        byte[] bytes = new byte[stream.Length];
        stream.Read(bytes, 0, bytes.Length);
        // 设置当前流的位置为流的开始
        stream.Seek(0, SeekOrigin.Begin);
        // 把 byte[] 写入文件
        FileStream fs = new FileStream(fileName, FileMode.Create);
        BinaryWriter bw = new BinaryWriter(fs);
        bw.Write(bytes);
        bw.Close();
        fs.Close();
    }

    /// <summary>
    /// 从文件读取 Stream
    /// </summary>
    public static Stream FileToStream(string fileName)
    {
        // 打开文件
        FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
        // 读取文件的 byte[]
        byte[] bytes = new byte[fileStream.Length];
        fileStream.Read(bytes, 0, bytes.Length);
        fileStream.Close();
        // 把 byte[] 转换成 Stream
        Stream stream = new MemoryStream(bytes);
        return stream;
    }

    /// <summary>
    /// 将文件读取到缓冲区中
    /// </summary>
    /// <param name="filePath">文件的绝对路径</param>
    public static byte[] FileToBytes(string filePath)
    {
        //获取文件的大小
        int fileSize = GetFileSize(filePath);

        //创建一个临时缓冲区
        byte[] buffer = new byte[fileSize];

        //创建一个文件流
        FileInfo fi = new FileInfo(filePath);
        FileStream fs = fi.Open(FileMode.Open);

        try
        {
            //将文件流读入缓冲区
            fs.Read(buffer, 0, fileSize);

            return buffer;
        }
        catch (IOException ex)
        {
            throw;
        }
        finally
        {
            //关闭文件流
            fs.Close();
        }
    }

    /// <summary>
    /// 将文件读取到字符串中
    /// </summary>
    /// <param name="filePath">文件的绝对路径</param>
    public static string FileToString(string filePath)
    {
        return FileToString(filePath, Encoding.Default);
    }

    /// <summary>
    /// 将文件读取到字符串中
    /// </summary>
    /// <param name="filePath">文件的绝对路径</param>
    /// <param name="encoding">字符编码</param>
    public static string FileToString(string filePath, Encoding encoding)
    {
        try
        {
            //创建流读取器
            using StreamReader reader = new StreamReader(filePath, encoding);
            //读取流
            return reader.ReadToEnd();
        }
        catch (IOException ex)
        {
            throw;
        }

    }

    /// <summary>
    /// 从嵌入资源中读取文件内容(e.g: xml).
    /// </summary>
    /// <param name="fileWholeName">嵌入资源文件名，包括项目的命名空间.</param>
    /// <returns>资源中的文件内容.</returns>
    public static string ReadFileFromEmbedded(string fileWholeName)
    {
        string result = string.Empty;

        using TextReader reader = new StreamReader(
            Assembly.GetExecutingAssembly().GetManifestResourceStream(fileWholeName));
        result = reader.ReadToEnd();
        return result;
    }

    #endregion

    #region 获取文件的编码类型

    /// <summary>
    /// 获取文件编码
    /// </summary>
    /// <param name="filePath">文件绝对路径</param>
    /// <returns></returns>
    public static Encoding GetEncoding(string filePath)
    {
        return GetEncoding(filePath, Encoding.Default);
    }

    /// <summary>
    /// 获取文件编码
    /// </summary>
    /// <param name="filePath">文件绝对路径</param>
    /// <param name="defaultEncoding">找不到则返回这个默认编码</param>
    /// <returns></returns>
    public static Encoding GetEncoding(string filePath, Encoding defaultEncoding)
    {
        Encoding targetEncoding = defaultEncoding;
        using FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4);
        if (fs.Length >= 2)
        {
            long pos = fs.Position;
            fs.Position = 0;
            int[] buffer = new int[4];
            //long x = fs.Seek(0, SeekOrigin.Begin);
            //fs.Read(buffer,0,4);
            buffer[0] = fs.ReadByte();
            buffer[1] = fs.ReadByte();
            buffer[2] = fs.ReadByte();
            buffer[3] = fs.ReadByte();

            fs.Position = pos;

            if (buffer[0] == 0xFE && buffer[1] == 0xFF)//UnicodeBe
            {
                targetEncoding = Encoding.BigEndianUnicode;
            }
            if (buffer[0] == 0xFF && buffer[1] == 0xFE)//Unicode
            {
                targetEncoding = Encoding.Unicode;
            }
            if (buffer[0] == 0xEF && buffer[1] == 0xBB && buffer[2] == 0xBF)//UTF8
            {
                targetEncoding = Encoding.UTF8;
            }
        }

        return targetEncoding;
    }

    #endregion

    #region 文件操作

    #region 获取一个文件的长度
    /// <summary>
    /// 获取一个文件的长度,单位为Byte
    /// </summary>
    /// <param name="filePath">文件的绝对路径</param>
    public static int GetFileSize(string filePath)
    {
        //创建一个文件对象
        FileInfo fi = new FileInfo(filePath);

        //获取文件的大小
        return (int)fi.Length;
    }

    /// <summary>
    /// 获取一个文件的长度,单位为KB
    /// </summary>
    /// <param name="filePath">文件的路径</param>
    public static double GetFileSizeKb(string filePath)
    {
        //创建一个文件对象
        FileInfo fi = new FileInfo(filePath);

        //获取文件的大小
        return (Convert.ToDouble(fi.Length) / 1024).ToDouble(1);
    }

    /// <summary>
    /// 获取一个文件的长度,单位为MB
    /// </summary>
    /// <param name="filePath">文件的路径</param>
    public static double GetFileSizeMb(string filePath)
    {
        //创建一个文件对象
        FileInfo fi = new FileInfo(filePath);

        //获取文件的大小
        return (Convert.ToDouble(fi.Length) / 1024 / 1024).ToDouble(1);
    }
    #endregion

    /// <summary>
    /// 向文本文件中写入内容
    /// </summary>
    /// <param name="filePath">文件的绝对路径</param>
    /// <param name="content">写入的内容</param>
    public static void WriteText(string filePath, string content)
    {
        //向文件写入内容
        System.IO.File.WriteAllText(filePath, content, Encoding.Default);
    }

    /// <summary>
    /// 向文本文件的尾部追加内容
    /// </summary>
    /// <param name="filePath">文件的绝对路径</param>
    /// <param name="content">写入的内容</param>
    public static void AppendText(string filePath, string content)
    {
        System.IO.File.AppendAllText(filePath, content, Encoding.Default);
    }

    /// <summary>
    /// 将源文件的内容复制到目标文件中
    /// </summary>
    /// <param name="sourceFilePath">源文件的绝对路径</param>
    /// <param name="destFilePath">目标文件的绝对路径</param>
    public static void Copy(string sourceFilePath, string destFilePath)
    {
        System.IO.File.Copy(sourceFilePath, destFilePath, true);
    }

    /// <summary>
    /// 将文件移动到指定目录
    /// </summary>
    /// <param name="sourceFilePath">需要移动的源文件的绝对路径</param>
    /// <param name="descDirectoryPath">移动到的目录的绝对路径</param>
    public static void Move(string sourceFilePath, string descDirectoryPath)
    {
        //获取源文件的名称
        string sourceFileName = GetFileName(sourceFilePath);

        if (Directory.Exists(descDirectoryPath))
        {
            //如果目标中存在同名文件,则删除
            if (IsExistFile(descDirectoryPath + "\\" + sourceFileName))
            {
                DeleteFile(descDirectoryPath + "\\" + sourceFileName);
            }
            //将文件移动到指定目录
            System.IO.File.Move(sourceFilePath, descDirectoryPath + "\\" + sourceFileName);
        }
    }

    /// <summary>
    /// 检测指定文件是否存在,如果存在则返回true。
    /// </summary>
    /// <param name="filePath">文件的绝对路径</param>
    public static bool IsExistFile(string filePath)
    {
        return System.IO.File.Exists(filePath);
    }

    /// <summary>
    /// 如果文件不存在则创建一个文件。
    /// </summary>
    /// <param name="filePath">文件的绝对路径</param>
    public static void CreateFile(string filePath)
    {
        try
        {
            //如果文件不存在则创建该文件
            if (!IsExistFile(filePath))
            {
                System.IO.File.Create(filePath).Close();
            }
        }
        catch (IOException ex)
        {
            throw;
        }
    }

    /// <summary>
    /// 创建一个文件,并将字节流写入文件。
    /// </summary>
    /// <param name="filePath">文件的绝对路径</param>
    /// <param name="buffer">二进制流数据</param>
    public static void CreateFile(string filePath, byte[] buffer)
    {
        try
        {
            //如果文件不存在则创建该文件
            if (!IsExistFile(filePath))
            {
                //创建文件
                using FileStream fs = System.IO.File.Create(filePath);
                //写入二进制流
                fs.Write(buffer, 0, buffer.Length);
            }
        }
        catch (IOException ex)
        {
            throw;
        }
    }

    /// <summary>
    /// 获取文本文件的行数
    /// </summary>
    /// <param name="filePath">文件的绝对路径</param>
    public static int GetLineCount(string filePath)
    {
        //将文本文件的各行读到一个字符串数组中
        string[] rows = System.IO.File.ReadAllLines(filePath);

        //返回行数
        return rows.Length;
    }

    /// <summary>
    /// 从文件的绝对路径中获取文件名( 包含扩展名 )
    /// </summary>
    /// <param name="filePath">文件的绝对路径</param>
    public static string GetFileName(string filePath)
    {
        //获取文件的名称
        FileInfo fi = new FileInfo(filePath);
        return fi.Name;
    }

    /// <summary>
    /// 从文件的绝对路径中获取文件名( 不包含扩展名 )
    /// </summary>
    /// <param name="filePath">文件的绝对路径</param>
    public static string GetFileNameNoExtension(string filePath)
    {
        //获取文件的名称
        FileInfo fi = new FileInfo(filePath);
        return fi.Name.Substring(0, fi.Name.LastIndexOf('.'));
    }

    /// <summary>
    /// 从文件的绝对路径中获取扩展名
    /// </summary>
    /// <param name="filePath">文件的绝对路径</param>
    public static string GetExtension(string filePath)
    {
        //获取文件的名称
        FileInfo fi = new FileInfo(filePath);
        return fi.Extension;
    }

    /// <summary>
    /// 清空文件内容
    /// </summary>
    /// <param name="filePath">文件的绝对路径</param>
    public static void ClearFile(string filePath)
    {
        //删除文件
        System.IO.File.Delete(filePath);

        //重新创建该文件
        CreateFile(filePath);
    }

    /// <summary>
    /// 删除指定文件
    /// </summary>
    /// <param name="filePath">文件的绝对路径</param>
    public static void DeleteFile(string filePath)
    {
        if (IsExistFile(filePath))
        {
            System.IO.File.Delete(filePath);
        }
    }

    /// <summary>
    /// 文件是否存在或无权访问
    /// </summary>
    /// <param name="path">相对路径或绝对路径</param>
    /// <returns>如果是目录也返回false</returns>
    public static bool FileIsExist(string path)
    {
        return System.IO.File.Exists(path);
    }

    /// <summary>
    /// 文件是否只读
    /// </summary>
    /// <param name="fullpath"></param>
    /// <returns></returns>
    public static bool FileIsReadOnly(string fullpath)
    {
        FileInfo file = new FileInfo(fullpath);
        if ((file.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 设置文件是否只读
    /// </summary>
    /// <param name="fullpath"></param>
    /// <param name="flag">true表示只读，反之</param>
    public static void SetFileReadonly(string fullpath, bool flag)
    {
        FileInfo file = new FileInfo(fullpath);

        if (flag)
        {
            // 添加只读属性
            file.Attributes |= FileAttributes.ReadOnly;
        }
        else
        {
            // 移除只读属性
            file.Attributes &= ~FileAttributes.ReadOnly;
        }
    }

    /// <summary>
    /// 取文件名
    /// </summary>
    /// <param name="fullpath">文件路径</param>
    /// <param name="removeExt">是否移除后缀名</param>
    /// <returns></returns>
    public static string GetFileName(string fullpath, bool removeExt)
    {
        FileInfo fi = new FileInfo(fullpath);
        string name = fi.Name;
        if (removeExt)
        {
            name = name.Remove(name.LastIndexOf('.'));
        }
        return name;
    }

    /// <summary>
    /// 取文件创建时间
    /// </summary>
    /// <param name="fullpath"></param>
    /// <returns></returns>
    public static DateTime GetFileCreateTime(string fullpath)
    {
        FileInfo fi = new FileInfo(fullpath);
        return fi.CreationTime;
    }

    /// <summary>
    /// 取文件最后存储时间
    /// </summary>
    /// <param name="fullpath"></param>
    /// <returns></returns>
    public static DateTime GetLastWriteTime(string fullpath)
    {
        FileInfo fi = new FileInfo(fullpath);
        return fi.LastWriteTime;
    }

    /// <summary>
    /// 创建一个零字节临时文件
    /// </summary>
    /// <returns></returns>
    public static string CreateTempZeroByteFile()
    {
        return Path.GetTempFileName();
    }

    /// <summary>
    /// 创建一个随机文件名，不创建文件本身
    /// </summary>
    /// <returns></returns>
    public static string GetRandomFileName()
    {
        return Path.GetRandomFileName();
    }

    /// <summary>
    /// 判断两个文件的哈希值是否一致
    /// </summary>
    /// <param name="fileName1"></param>
    /// <param name="fileName2"></param>
    /// <returns></returns>
    public static bool CompareFilesHash(string fileName1, string fileName2)
    {
        using HashAlgorithm hashAlg = HashAlgorithm.Create();
        using FileStream fs1 = new FileStream(fileName1, FileMode.Open), fs2 = new FileStream(fileName2, FileMode.Open);
        byte[] hashBytes1 = hashAlg.ComputeHash(fs1);
        byte[] hashBytes2 = hashAlg.ComputeHash(fs2);

        // 比较哈希码
        return (BitConverter.ToString(hashBytes1) == BitConverter.ToString(hashBytes2));
    }

    #endregion

    #region XML文件操作
    /// <summary>
    /// 从XML文件转换为Object对象类型.
    /// </summary>
    /// <param name="path">XML文件路径</param>
    /// <param name="type">Object对象类型</param>
    /// <returns></returns>
    public static object LoadObjectFromXml(string path, Type type)
    {
        object obj = null;
        using StreamReader reader = new StreamReader(path);
        string content = reader.ReadToEnd();
        obj = XmlConvertor.XmlToObject(content, type);
        return obj;
    }

    /// <summary>
    /// 保存对象到特定格式的XML文件
    /// </summary>
    /// <param name="path">XML文件路径.</param>
    /// <param name="obj">待保存的对象</param>
    public static void SaveObjectToXml(string path, object obj)
    {
        string xml = XmlConvertor.ObjectToXml(obj, true);
        using StreamWriter writer = new StreamWriter(path);
        writer.Write(xml);
    } 
    #endregion

    /// <summary>
    /// 通过在临时文件中创建文件，并在本地进程中打开，退出后删除临时文件。
    /// </summary>
    /// <param name="fileName">显示的文件名称</param>
    /// <param name="bytes">文件字节</param>
    public static void OpenFileInProcess(string fileName, byte[] bytes)
    {
        string tempFilePath = Path.Combine(DirectoryUtil.GetTempPath(), fileName);
        if (System.IO.File.Exists(tempFilePath))
        {
            System.IO.File.Delete(tempFilePath);
        }
        BytesToFile(bytes, tempFilePath);

        string fileExtension = GetExtension(tempFilePath);
        string showFileName = GetFileName(tempFilePath);

        Thread thread = new Thread(delegate()
        {
            Process process = Process.Start(tempFilePath);
            process.WaitForExit();

            if (System.IO.File.Exists(tempFilePath))
            {
                System.IO.File.Delete(tempFilePath);
            }
            process.Close();
        });
        thread.Start();
    }
}