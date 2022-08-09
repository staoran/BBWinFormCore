using System.Collections;
using System.IO.Compression;
using System.Text;

namespace BB.Updater.Core;

/// <summary>
/// 压缩文件类
/// </summary>
public class GZip
{
    #region MyRegion
    /*
        //Compress三个参数分别是“要压缩的目标目录”，“保存压缩文件的目录”，压缩文件名
        GZip.Compress(@"E:\much\", @"E:\much\", "ziptest");
        
        //Decompress三个参数分别是"压缩包所在目录","要解压到的目录",“压缩包名”
        //如果压缩包所在目录不存在则解压不会成功
        GZip.Decompress(@"E:\much\zip\", @"E:\much\zip\", "ziptest"); 
    */
    #endregion

    /// <summary>
    /// Compress
    /// </summary>
    /// <param name="lpSourceFolder">The location of the files to include in the zip file, all files including files in subfolders will be included.</param>
    /// <param name="lpDestFolder">Folder to write the zip file into</param>
    /// <param name="zipFileName">Name of the zip file to write</param>
    public static GZipResult Compress(string lpSourceFolder, string lpDestFolder, string zipFileName)
    {
        return Compress(lpSourceFolder, "*.*", SearchOption.AllDirectories, lpDestFolder, zipFileName, true);
    }

    /// <summary>
    /// Compress
    /// </summary>
    /// <param name="lpSourceFolder">The location of the files to include in the zip file</param>
    /// <param name="searchPattern">Search pattern (ie "*.*" or "*.txt" or "*.gif") to idendify what files in lpSourceFolder to include in the zip file</param>
    /// <param name="searchOption">Only files in lpSourceFolder or include files in subfolders also</param>
    /// <param name="lpDestFolder">Folder to write the zip file into</param>
    /// <param name="zipFileName">Name of the zip file to write</param>
    /// <param name="deleteTempFile">Boolean, true deleted the intermediate temp file, false leaves the temp file in lpDestFolder (for debugging)</param>
    public static GZipResult Compress(string lpSourceFolder, string searchPattern, SearchOption searchOption, string lpDestFolder, string zipFileName, bool deleteTempFile)
    {
        DirectoryInfo di = new DirectoryInfo(lpSourceFolder);
        FileInfo[] files = di.GetFiles("*.*", searchOption);
        return Compress(files, lpSourceFolder, lpDestFolder, zipFileName, deleteTempFile);
    }

    /// <summary>
    /// Compress
    /// </summary>
    /// <param name="files">Array of FileInfo objects to be included in the zip file</param>
    /// <param name="lpBaseFolder">Base folder to use when creating relative paths for the files 
    /// stored in the zip file. For example, if lpBaseFolder is 'C:\zipTest\Files\', and there is a file 
    /// 'C:\zipTest\Files\folder1\sample.txt' in the 'files' array, the relative path for sample.txt 
    /// will be 'folder1/sample.txt'</param>
    /// <param name="lpDestFolder">Folder to write the zip file into</param>
    /// <param name="zipFileName">Name of the zip file to write</param>
    public static GZipResult Compress(FileInfo[] files, string lpBaseFolder, string lpDestFolder, string zipFileName)
    {
        return Compress(files, lpBaseFolder, lpDestFolder, zipFileName, true);
    }

    /// <summary>
    /// Compress
    /// </summary>
    /// <param name="files">Array of FileInfo objects to be included in the zip file</param>
    /// <param name="lpBaseFolder">Base folder to use when creating relative paths for the files 
    /// stored in the zip file. For example, if lpBaseFolder is 'C:\zipTest\Files\', and there is a file 
    /// 'C:\zipTest\Files\folder1\sample.txt' in the 'files' array, the relative path for sample.txt 
    /// will be 'folder1/sample.txt'</param>
    /// <param name="lpDestFolder">Folder to write the zip file into</param>
    /// <param name="zipFileName">Name of the zip file to write</param>
    /// <param name="deleteTempFile">Boolean, true deleted the intermediate temp file, false leaves the temp file in lpDestFolder (for debugging)</param>
    public static GZipResult Compress(FileInfo[] files, string lpBaseFolder, string lpDestFolder, string zipFileName, bool deleteTempFile)
    {
        GZipResult result = new GZipResult();

        try
        {
            if (!lpDestFolder.EndsWith("\\"))
            {
                lpDestFolder += "\\";
            }

            string lpTempFile = lpDestFolder + zipFileName + ".tmp";
            string lpZipFile = lpDestFolder + zipFileName;

            result.TempFile = lpTempFile;
            result.ZipFile = lpZipFile;

            if (files != null && files.Length > 0)
            {
                CreateTempFile(files, lpBaseFolder, lpTempFile, result);

                if (result.FileCount > 0)
                {
                    CreateZipFile(lpTempFile, lpZipFile, result);
                }

                // delete the temp file
                if (deleteTempFile)
                {
                    File.Delete(lpTempFile);
                    result.TempFileDeleted = true;
                }
            }
        }
        catch //(Exception ex4)
        {
            result.Errors = true;
        }
        return result;
    }

    private static void CreateZipFile(string lpSourceFile, string lpZipFile, GZipResult result)
    {
        byte[] buffer;
        int count = 0;
        FileStream fsOut = null;
        FileStream fsIn = null;
        GZipStream gzip = null;

        // compress the file into the zip file
        try
        {
            fsOut = new FileStream(lpZipFile, FileMode.Create, FileAccess.Write, FileShare.None);
            gzip = new GZipStream(fsOut, CompressionMode.Compress, true);

            fsIn = new FileStream(lpSourceFile, FileMode.Open, FileAccess.Read, FileShare.Read);
            buffer = new byte[fsIn.Length];
            count = fsIn.Read(buffer, 0, buffer.Length);
            fsIn.Close();
            fsIn = null;

            // compress to the zip file
            gzip.Write(buffer, 0, buffer.Length);

            result.ZipFileSize = fsOut.Length;
            result.CompressionPercent = GetCompressionPercent(result.TempFileSize, result.ZipFileSize);
        }
        catch //(Exception ex1)
        {
            result.Errors = true;
        }
        finally
        {
            if (gzip != null)
            {
                gzip.Close();
                gzip = null;
            }
            if (fsOut != null)
            {
                fsOut.Close();
                fsOut = null;
            }
            if (fsIn != null)
            {
                fsIn.Close();
                fsIn = null;
            }
        }
    }

    private static void CreateTempFile(FileInfo[] files, string lpBaseFolder, string lpTempFile, GZipResult result)
    {
        byte[] buffer;
        int count = 0;
        byte[] header;
        string fileHeader = null;
        string fileModDate = null;
        string lpFolder = null;
        int fileIndex = 0;
        string lpSourceFile = null;
        string vpSourceFile = null;
        GZipFileInfo gzf = null;
        FileStream fsOut = null;
        FileStream fsIn = null;

        if (files != null && files.Length > 0)
        {
            try
            {
                result.Files = new GZipFileInfo[files.Length];

                // open the temp file for writing
                fsOut = new FileStream(lpTempFile, FileMode.Create, FileAccess.Write, FileShare.None);

                foreach (FileInfo fi in files)
                {
                    lpFolder = fi.DirectoryName + "\\";
                    try
                    {
                        gzf = new GZipFileInfo
                        {
                            Index = fileIndex
                        };

                        // read the source file, get its virtual path within the source folder
                        lpSourceFile = fi.FullName;
                        gzf.LocalPath = lpSourceFile;
                        vpSourceFile = lpSourceFile.Replace(lpBaseFolder, string.Empty);
                        vpSourceFile = vpSourceFile.Replace("\\", "/");
                        gzf.RelativePath = vpSourceFile;

                        fsIn = new FileStream(lpSourceFile, FileMode.Open, FileAccess.Read, FileShare.Read);
                        buffer = new byte[fsIn.Length];
                        count = fsIn.Read(buffer, 0, buffer.Length);
                        fsIn.Close();
                        fsIn = null;

                        fileModDate = fi.LastWriteTimeUtc.ToString();
                        gzf.ModifiedDate = fi.LastWriteTimeUtc;
                        gzf.Length = buffer.Length;

                        fileHeader = fileIndex + "," + vpSourceFile + "," + fileModDate + "," + buffer.Length + "\n";
                        header = Encoding.Default.GetBytes(fileHeader);

                        fsOut.Write(header, 0, header.Length);
                        fsOut.Write(buffer, 0, buffer.Length);
                        fsOut.WriteByte(10); // linefeed

                        gzf.AddedToTempFile = true;

                        // update the result object
                        result.Files[fileIndex] = gzf;

                        // increment the fileIndex
                        fileIndex++;
                    }
                    catch //(Exception ex1)
                    {
                        result.Errors = true;
                    }
                    finally
                    {
                        if (fsIn != null)
                        {
                            fsIn.Close();
                            fsIn = null;
                        }
                    }
                    if (fsOut != null)
                    {
                        result.TempFileSize = fsOut.Length;
                    }
                }
            }
            catch //(Exception ex2)
            {
                result.Errors = true;
            }
            finally
            {
                if (fsOut != null)
                {
                    fsOut.Close();
                    fsOut = null;
                }
            }
        }

        result.FileCount = fileIndex;
    }

    public static GZipResult Decompress(string lpSourceFolder, string lpDestFolder, string zipFileName)
    {
        return Decompress(lpSourceFolder, lpDestFolder, zipFileName, true, true, null, null, 4096);
    }

    public static GZipResult Decompress(string lpSourceFolder, string lpDestFolder, string zipFileName, bool writeFiles, string addExtension)
    {
        return Decompress(lpSourceFolder, lpDestFolder, zipFileName, true, writeFiles, addExtension, null, 4096);
    }

    public static GZipResult Decompress(string lpSrcFolder, string lpDestFolder, string zipFileName,
        bool deleteTempFile, bool writeFiles, string addExtension, Hashtable htFiles, int bufferSize)
    {
        GZipResult result = new GZipResult();

        if (!lpDestFolder.EndsWith("\\"))
        {
            lpDestFolder += "\\";
        }

        string lpTempFile = lpSrcFolder + zipFileName + ".tmp";
        string lpZipFile = lpSrcFolder + zipFileName;

        result.TempFile = lpTempFile;
        result.ZipFile = lpZipFile;

        string line = null;
        string lpFilePath = null;
        string lpFolder = null;
        GZipFileInfo gzf = null;
        FileStream fsTemp = null;
        ArrayList gzfs = new ArrayList();
        bool write = false;

        if (string.IsNullOrEmpty(addExtension))
        {
            addExtension = string.Empty;
        }
        else if (!addExtension.StartsWith("."))
        {
            addExtension = "." + addExtension;
        }

        // extract the files from the temp file
        try
        {
            fsTemp = UnzipToTempFile(lpZipFile, lpTempFile, result);
            if (fsTemp != null)
            {
                while (fsTemp.Position != fsTemp.Length)
                {
                    line = null;
                    while (string.IsNullOrEmpty(line) && fsTemp.Position != fsTemp.Length)
                    {
                        line = ReadLine(fsTemp);
                    }

                    if (!string.IsNullOrEmpty(line))
                    {
                        gzf = new GZipFileInfo();
                        if (gzf.ParseFileInfo(line) && gzf.Length > 0)
                        {
                            gzfs.Add(gzf);
                            lpFilePath = lpDestFolder + gzf.RelativePath;
                            lpFolder = GetFolder(lpFilePath);
                            gzf.LocalPath = lpFilePath;

                            write = false;
                            if (htFiles == null || htFiles.ContainsKey(gzf.RelativePath))
                            {
                                gzf.RestoreRequested = true;
                                write = writeFiles;
                            }

                            if (write)
                            {
                                // make sure the folder exists
                                if (!Directory.Exists(lpFolder))
                                {
                                    Directory.CreateDirectory(lpFolder);
                                }

                                // read from fsTemp and write out the file
                                gzf.Restored = WriteFile(fsTemp, gzf.Length, lpFilePath + addExtension, bufferSize);
                            }
                            else
                            {
                                // need to advance fsTemp
                                fsTemp.Position += gzf.Length;
                            }
                        }
                    }
                }
            }
        }
        catch //(Exception ex3)
        {
            result.Errors = true;
        }
        finally
        {
            if (fsTemp != null)
            {
                fsTemp.Close();
                fsTemp = null;
            }
        }

        // delete the temp file
        try
        {
            if (deleteTempFile)
            {
                File.Delete(lpTempFile);
                result.TempFileDeleted = true;
            }
        }
        catch //(Exception ex4)
        {
            result.Errors = true;
        }

        result.FileCount = gzfs.Count;
        result.Files = new GZipFileInfo[gzfs.Count];
        gzfs.CopyTo(result.Files);
        return result;
    }

    private static string ReadLine(FileStream fs)
    {
        string line = string.Empty;

        const int bufferSize = 4096;
        byte[] buffer = new byte[bufferSize];
        byte b = 0;
        byte lf = 10;
        int i = 0;

        while (b != lf)
        {
            b = (byte)fs.ReadByte();
            buffer[i] = b;
            i++;
        }

        line = Encoding.Default.GetString(buffer, 0, i - 1);

        return line;
    }

    private static bool WriteFile(FileStream fs, int fileLength, string lpFile, int bufferSize)
    {
        bool success = false;
        FileStream fsFile = null;

        if (bufferSize == 0 || fileLength < bufferSize)
        {
            bufferSize = fileLength;
        }

        int count = 0;
        int remaining = fileLength;
        int readSize = 0;

        try
        {
            byte[] buffer = new byte[bufferSize];
            fsFile = new FileStream(lpFile, FileMode.Create, FileAccess.Write, FileShare.None);

            while (remaining > 0)
            {
                if (remaining > bufferSize)
                {
                    readSize = bufferSize;
                }
                else
                {
                    readSize = remaining;
                }

                count = fs.Read(buffer, 0, readSize);
                remaining -= count;

                if (count == 0)
                {
                    break;
                }

                fsFile.Write(buffer, 0, count);
                fsFile.Flush();

            }
            fsFile.Flush();
            fsFile.Close();
            fsFile = null;

            success = true;
        }
        catch //(Exception ex2)
        {
            success = false;
        }
        finally
        {
            if (fsFile != null)
            {
                fsFile.Flush();
                fsFile.Close();
                fsFile = null;
            }
        }
        return success;
    }

    private static string GetFolder(string lpFilePath)
    {
        string lpFolder = lpFilePath;
        int index = lpFolder.LastIndexOf("\\");
        if (index != -1)
        {
            lpFolder = lpFolder.Substring(0, index + 1);
        }
        return lpFolder;
    }
    private static FileStream UnzipToTempFile(string lpZipFile, string lpTempFile, GZipResult result)
    {
        FileStream fsIn = null;
        GZipStream gzip = null;
        FileStream fsOut = null;
        FileStream fsTemp = null;

        const int bufferSize = 4096;
        byte[] buffer = new byte[bufferSize];
        int count = 0;

        try
        {
            fsIn = new FileStream(lpZipFile, FileMode.Open, FileAccess.Read, FileShare.Read);
            result.ZipFileSize = fsIn.Length;

            fsOut = new FileStream(lpTempFile, FileMode.Create, FileAccess.Write, FileShare.None);
            gzip = new GZipStream(fsIn, CompressionMode.Decompress, true);
            while (true)
            {
                count = gzip.Read(buffer, 0, bufferSize);
                if (count != 0)
                {
                    fsOut.Write(buffer, 0, count);
                }
                if (count != bufferSize)
                {
                    break;
                }
            }
        }
        catch (Exception ex1)
        {
            result.Errors = true;
        }
        finally
        {
            if (gzip != null)
            {
                gzip.Close();
                gzip = null;
            }
            if (fsOut != null)
            {
                fsOut.Close();
                fsOut = null;
            }
            if (fsIn != null)
            {
                fsIn.Close();
                fsIn = null;
            }
        }

        fsTemp = new FileStream(lpTempFile, FileMode.Open, FileAccess.Read, FileShare.None);
        if (fsTemp != null)
        {
            result.TempFileSize = fsTemp.Length;
        }
        return fsTemp;
    }

    private static int GetCompressionPercent(long tempLen, long zipLen)
    {
        double tmp = (double)tempLen;
        double zip = (double)zipLen;
        double hundred = 100;

        double ratio = (tmp - zip) / tmp;
        double pcnt = ratio * hundred;

        return (int)pcnt;
    }
}

/// <summary>
/// 要压缩的文件信息
/// </summary>
public class GZipFileInfo
{
    /// <summary>
    /// 文件索引
    /// </summary>
    public int Index = 0;
    /// <summary>
    /// 文件相对路径，'/'
    /// </summary>
    public string RelativePath = null;
    public DateTime ModifiedDate;
    /// <summary>
    /// 文件内容长度
    /// </summary>
    public int Length = 0;
    public bool AddedToTempFile = false;
    public bool RestoreRequested = false;
    public bool Restored = false;
    /// <summary>
    /// 文件绝对路径,'\'
    /// </summary>
    public string LocalPath = null;
    public string Folder = null;

    public bool ParseFileInfo(string fileInfo)
    {
        bool success = false;
        try
        {
            if (!string.IsNullOrEmpty(fileInfo))
            {
                // get the file information
                string[] info = fileInfo.Split(',');
                if (info != null && info.Length == 4)
                {
                    Index = Convert.ToInt32(info[0]);
                    RelativePath = info[1].Replace("/", "\\");
                    ModifiedDate = Convert.ToDateTime(info[2]);
                    Length = Convert.ToInt32(info[3]);
                    success = true;
                }
            }
        }
        catch
        {
            success = false;
        }
        return success;
    }
}

/// <summary>
/// 文件压缩后的压缩包类
/// </summary>
public class GZipResult
{
    /// <summary>
    /// 压缩包中包含的所有文件,包括子目录下的文件
    /// </summary>
    public GZipFileInfo[] Files = null;
    /// <summary>
    /// 要压缩的文件数
    /// </summary>
    public int FileCount = 0;
    public long TempFileSize = 0;
    public long ZipFileSize = 0;
    /// <summary>
    /// 压缩百分比
    /// </summary>
    public int CompressionPercent = 0;
    /// <summary>
    /// 临时文件
    /// </summary>
    public string TempFile = null;
    /// <summary>
    /// 压缩文件
    /// </summary>
    public string ZipFile = null;
    /// <summary>
    /// 是否删除临时文件
    /// </summary>
    public bool TempFileDeleted = false;
    public bool Errors = false;
}