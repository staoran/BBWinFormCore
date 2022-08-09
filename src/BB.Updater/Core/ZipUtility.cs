using System.Collections;
using ICSharpCode.SharpZipLib.Zip;

namespace BB.Updater.Core;

public static class ZipUtility
{
    /// <summary>
    /// 压缩文件中的文件，可设置密码
    /// </summary>
    /// <param name="inputFolderPath">输入的文件夹</param>
    /// <param name="outputPathAndFile">输出的压缩文件全名</param>
    /// <param name="password">压缩密码</param>
    public static void ZipFiles(string inputFolderPath, string outputPathAndFile, string password)
    {
        ArrayList ar = GenerateFileList(inputFolderPath);
        int trimLength = (Directory.GetParent(inputFolderPath)).ToString().Length;
        // find number of chars to remove   // from orginal file path
        trimLength += 1; //remove '\'
        FileStream ostream;
        byte[] obuffer;
        string outPath = inputFolderPath + @"\" + outputPathAndFile;
        ZipOutputStream oZipStream = new ZipOutputStream(File.Create(outPath));
        if (!string.IsNullOrEmpty(password))
        {
            oZipStream.Password = password;
        }
        oZipStream.SetLevel(9); // 设置最大压缩率

        ZipEntry oZipEntry;
        foreach (string fil in ar)
        {
            oZipEntry = new ZipEntry(fil.Remove(0, trimLength));
            oZipStream.PutNextEntry(oZipEntry);

            if (!fil.EndsWith(@"/")) // 如果文件以 '/' 结束，则是目录
            {
                ostream = File.OpenRead(fil);
                obuffer = new byte[ostream.Length];
                ostream.Read(obuffer, 0, obuffer.Length);
                oZipStream.Write(obuffer, 0, obuffer.Length);
            }
        }
        oZipStream.Finish();
        oZipStream.Close();
    }

    /// <summary>
    /// 根据文件夹生成文件列表
    /// </summary>
    /// <param name="dir"></param>
    /// <returns></returns>
    private static ArrayList GenerateFileList(string dir)
    {
        ArrayList fils = new ArrayList();
        bool empty = true;
        foreach (string file in Directory.GetFiles(dir))
        {
            fils.Add(file);
            empty = false;
        }

        if (empty)
        {
            //加入完全为空的目录
            if (Directory.GetDirectories(dir).Length == 0)
            {
                fils.Add(dir + @"/");
            }
        }

        foreach (string dirs in Directory.GetDirectories(dir)) // 递归目录
        {
            foreach (object obj in GenerateFileList(dirs))
            {
                fils.Add(obj);
            }
        }
        return fils; 
    }

    /// <summary>
    /// 解压文件到指定的目录，可设置密码、删除原文件等
    /// </summary>
    /// <param name="zipPathAndFile">压缩文件全名</param>
    /// <param name="outputFolder">解压输出文件目录</param>
    /// <param name="password">解压密码</param>
    /// <param name="deleteZipFile">是否删除原文件（压缩文件）</param>
    public static void UnZipFiles(string zipPathAndFile, string outputFolder, string password, bool deleteZipFile)
    {
        using (ZipInputStream s = new ZipInputStream(File.OpenRead(zipPathAndFile)))
        {
            if (password != null && password != String.Empty)
            {
                s.Password = password;
            }

            ZipEntry theEntry;
            string tmpEntry = String.Empty;
            while ((theEntry = s.GetNextEntry()) != null)
            {
                #region 遍历每个Entry对象进行解压处理
                string directoryName = outputFolder;
                string fileName = Path.GetFileName(theEntry.Name);
                if (directoryName != "")
                {
                    Directory.CreateDirectory(directoryName);
                }

                if (fileName != String.Empty)
                {
                    if (theEntry.Name.IndexOf(".ini") < 0)
                    {
                        string fullPath = directoryName + "\\" + theEntry.Name;
                        fullPath = fullPath.Replace("\\ ", "\\");
                        string fullDirPath = Path.GetDirectoryName(fullPath);
                        if (!Directory.Exists(fullDirPath)) Directory.CreateDirectory(fullDirPath);
                        using (FileStream streamWriter = File.Create(fullPath))
                        {
                            #region 写入文件流
                            int size = 2048;
                            byte[] data = new byte[2048];
                            while (true)
                            {
                                size = s.Read(data, 0, data.Length);
                                if (size > 0)
                                {
                                    streamWriter.Write(data, 0, size);
                                }
                                else
                                {
                                    break;
                                }
                            }
                            #endregion
                        }
                    }
                } 
                #endregion
            }
        }

        if (deleteZipFile)
        {
            File.Delete(zipPathAndFile);
        }
    }
}