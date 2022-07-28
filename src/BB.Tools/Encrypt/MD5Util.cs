﻿using System.Text;

namespace BB.Tools.Encrypt;

/// <summary>
/// MD5各种长度加密字符、验证MD5等操作辅助类
/// </summary>
public class Md5Util
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public Md5Util()
    {
    }

    /// <summary>
    /// 获得32位的MD5加密
    /// </summary>
    public static string GetMD5_32(string input)
    {
        System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
        byte[] data = md5.ComputeHash(Encoding.Default.GetBytes(input));
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < data.Length; i++)
        {
            sb.Append(data[i].ToString("x2"));
        }
        return sb.ToString();
    }

    /// <summary>
    /// 获得16位的MD5加密
    /// </summary>
    public static string GetMD5_16(string input)
    {
        return GetMD5_32(input).Substring(8, 16);
    }

    /// <summary>
    /// 获得8位的MD5加密
    /// </summary>
    public static string GetMD5_8(string input)
    {
        return GetMD5_32(input).Substring(8, 8);
    }

    /// <summary>
    /// 获得4位的MD5加密
    /// </summary>
    public static string GetMD5_4(string input)
    {
        return GetMD5_32(input).Substring(8, 4);
    }

    /// <summary>
    /// 添加MD5的前缀，便于检查有无篡改
    /// </summary>
    public static string AddMd5Profix(string input)
    {
        return GetMD5_4(input) + input;
    }

    /// <summary>
    /// 移除MD5的前缀
    /// </summary>
    public static string RemoveMd5Profix(string input)
    {
        return input.Substring(4);
    }

    /// <summary>
    /// 验证MD5前缀处理的字符串有无被篡改
    /// </summary>
    public static bool ValidateValue(string input)
    {
        bool res = false;
        if (input.Length >= 4)
        {
            string tmp = input.Substring(4);
            if (input.Substring(0, 4) == GetMD5_4(tmp))
            {
                res = true;
            }
        }
        return res;
    }

    #region MD5签名验证

    /// <summary>
    /// 对给定文件路径的文件加上标签
    /// </summary>
    /// <param name="path">要加密的文件的路径</param>
    /// <returns>标签的值</returns>
    public static bool AddMd5(string path)
    {
        bool isNeed = true;

        if (CheckMd5(path)) //已进行MD5处理
            isNeed = false;

        try
        {
            FileStream fsread = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            byte[] md5File = new byte[fsread.Length];
            fsread.Read(md5File, 0, (int)fsread.Length);                               // 将文件流读取到Buffer中
            fsread.Close();

            if (isNeed)
            {
                string result = Md5Buffer(md5File, 0, md5File.Length);             // 对Buffer中的字节内容算MD5
                byte[] md5 = Encoding.ASCII.GetBytes(result);       // 将字符串转换成字节数组以便写人到文件中
                FileStream fsWrite = new FileStream(path, FileMode.Open, FileAccess.ReadWrite);
                fsWrite.Write(md5File, 0, md5File.Length);                               // 将文件，MD5值 重新写入到文件中。
                fsWrite.Write(md5, 0, md5.Length);
                fsWrite.Close();
            }
            else
            {
                FileStream fsWrite = new FileStream(path, FileMode.Open, FileAccess.ReadWrite);
                fsWrite.Write(md5File, 0, md5File.Length);
                fsWrite.Close();
            }
        }
        catch
        {
            return false;
        }

        return true;
    }

    /// <summary>
    /// 对给定路径的文件进行验证，如果一致返回True，否则返回False
    /// </summary>
    /// <param name="path"></param>
    /// <returns>是否加了标签或是否标签值与内容值一致</returns>
    public static bool CheckMd5(string path)
    {
        try
        {
            FileStream getFile = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            byte[] md5File = new byte[getFile.Length];                                      // 读入文件
            getFile.Read(md5File, 0, (int)getFile.Length);
            getFile.Close();

            string result = Md5Buffer(md5File, 0, md5File.Length - 32);             // 对文件除最后32位以外的字节计算MD5，这个32是因为标签位为32位。
            string md5 = Encoding.ASCII.GetString(md5File, md5File.Length - 32, 32);   //读取文件最后32位，其中保存的就是MD5值
            return result == md5;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// 计算文件的MD5值
    /// </summary>
    /// <param name="md5File">MD5签名文件字符数组</param>
    /// <param name="index">计算起始位置</param>
    /// <param name="count">计算终止位置</param>
    /// <returns>计算结果</returns>
    private static string Md5Buffer(byte[] md5File, int index, int count)
    {
        System.Security.Cryptography.MD5CryptoServiceProvider getMd5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
        byte[] hashByte = getMd5.ComputeHash(md5File, index, count);
        string result = BitConverter.ToString(hashByte);

        result = result.Replace("-", "");
        return result;
    } 
    #endregion

    private void Test()
    {
        string o = "i love u";
        o = AddMd5Profix(o);
        //o += " ";
        Console.WriteLine(o);
        Console.WriteLine(ValidateValue(o));

        o = RemoveMd5Profix(o);
        Console.WriteLine(o);

    }
}