using System.Security.Cryptography;
using System.Text;

namespace BB.Tools.Encrypt;

/// <summary>
/// DES对称加解密、AES RijndaelManaged加解密、Base64加密解密、MD5加密等操作辅助类
/// </summary>
public sealed class EncodeHelper
{
    #region DES对称加密解密
        
    /// <summary>
    /// 注意DEFAULT_ENCRYPT_KEY的长度为8位(如果要增加或者减少key长度,调整IV的长度就是了) 
    /// </summary>
    public const string DEFAULT_ENCRYPT_KEY = "12345678";

    /// <summary>
    /// 使用默认加密
    /// </summary>
    /// <param name="strText"></param>
    /// <returns></returns>
    public static string DesEncrypt(string strText)
    {
        try
        {
            return DesEncrypt(strText, DEFAULT_ENCRYPT_KEY);
        }
        catch
        {
            return "";
        }
    }

    /// <summary>
    /// 使用默认解密
    /// </summary>
    /// <param name="strText">解密字符串</param>
    /// <returns></returns>
    public static string DesDecrypt(string strText)
    {
        try
        {
            return DesDecrypt(strText, DEFAULT_ENCRYPT_KEY);
        }
        catch
        {
            return "";
        }
    }

    /// <summary> 
    /// 加密字符串,注意strEncrKey的长度为8位
    /// </summary> 
    /// <param name="strText">待加密字符串</param> 
    /// <param name="strEncrKey">加密键</param> 
    /// <returns></returns> 
    public static string DesEncrypt(string strText, string strEncrKey)
    {
        byte[] byKey = null;
        byte[] iv = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

        byKey = Encoding.UTF8.GetBytes(strEncrKey.Substring(0, 8));
        DESCryptoServiceProvider des = new DESCryptoServiceProvider();
        byte[] inputByteArray = Encoding.UTF8.GetBytes(strText);
        MemoryStream ms = new MemoryStream();
        CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(byKey, iv), CryptoStreamMode.Write);
        cs.Write(inputByteArray, 0, inputByteArray.Length);
        cs.FlushFinalBlock();
        return Convert.ToBase64String(ms.ToArray());
    }

    /// <summary> 
    /// 解密字符串,注意strEncrKey的长度为8位
    /// </summary> 
    /// <param name="strText">待解密的字符串</param> 
    /// <param name="sDecrKey">解密键</param> 
    /// <returns>解密后的字符串</returns> 
    public static string DesDecrypt(string strText, string sDecrKey)
    {
        byte[] byKey = null;
        byte[] iv = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        byte[] inputByteArray = new Byte[strText.Length];

        byKey = Encoding.UTF8.GetBytes(sDecrKey.Substring(0, 8));
        DESCryptoServiceProvider des = new DESCryptoServiceProvider();
        inputByteArray = Convert.FromBase64String(strText);
        MemoryStream ms = new MemoryStream();
        CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(byKey, iv), CryptoStreamMode.Write);
        cs.Write(inputByteArray, 0, inputByteArray.Length);
        cs.FlushFinalBlock();
        Encoding encoding = new UTF8Encoding();
        return encoding.GetString(ms.ToArray());
    }

    /// <summary> 
    /// 加密数据文件,注意strEncrKey的长度为8位
    /// </summary> 
    /// <param name="m_InFilePath">待加密的文件路径</param> 
    /// <param name="m_OutFilePath">输出文件路径</param> 
    /// <param name="strEncrKey">加密键</param> 
    public static void DesEncrypt(string mInFilePath, string mOutFilePath, string strEncrKey)
    {
        byte[] byKey = null;
        byte[] iv = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

        byKey = Encoding.UTF8.GetBytes(strEncrKey.Substring(0, 8));
        FileStream fin = new FileStream(mInFilePath, FileMode.Open, FileAccess.Read);
        FileStream fout = new FileStream(mOutFilePath, FileMode.OpenOrCreate, FileAccess.Write);
        fout.SetLength(0);
        //Create variables to help with read and write. 
        byte[] bin = new byte[100]; //This is intermediate storage for the encryption. 
        long rdlen = 0; //This is the total number of bytes written. 
        long totlen = fin.Length; //This is the total length of the input file. 
        int len; //This is the number of bytes to be written at a time. 

        DES des = new DESCryptoServiceProvider();
        CryptoStream encStream = new CryptoStream(fout, des.CreateEncryptor(byKey, iv), CryptoStreamMode.Write);

        //Read from the input file, then encrypt and write to the output file. 
        while (rdlen < totlen)
        {
            len = fin.Read(bin, 0, 100);
            encStream.Write(bin, 0, len);
            rdlen = rdlen + len;
        }
        encStream.Close();
        fout.Close();
        fin.Close();
    }

    /// <summary> 
    /// 解密数据文件,注意strEncrKey的长度为8位
    /// </summary> 
    /// <param name="m_InFilePath">待解密的文件路径</param> 
    /// <param name="m_OutFilePath">输出路径</param> 
    /// <param name="sDecrKey">解密键</param> 
    public static void DesDecrypt(string mInFilePath, string mOutFilePath, string sDecrKey)
    {
        byte[] byKey = null;
        byte[] iv = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

        byKey = Encoding.UTF8.GetBytes(sDecrKey.Substring(0, 8));
        FileStream fin = new FileStream(mInFilePath, FileMode.Open, FileAccess.Read);
        FileStream fout = new FileStream(mOutFilePath, FileMode.OpenOrCreate, FileAccess.Write);
        fout.SetLength(0);
        //Create variables to help with read and write. 
        byte[] bin = new byte[100]; //This is intermediate storage for the encryption. 
        long rdlen = 0; //This is the total number of bytes written. 
        long totlen = fin.Length; //This is the total length of the input file. 
        int len; //This is the number of bytes to be written at a time. 

        DES des = new DESCryptoServiceProvider();
        CryptoStream encStream = new CryptoStream(fout, des.CreateDecryptor(byKey, iv), CryptoStreamMode.Write);

        //Read from the input file, then encrypt and write to the output file. 
        while (rdlen < totlen)
        {
            len = fin.Read(bin, 0, 100);
            encStream.Write(bin, 0, len);
            rdlen = rdlen + len;
        }
        encStream.Close();
        fout.Close();
        fin.Close();
    } 
    #endregion

    #region 对称加密算法AES RijndaelManaged加密解密
    private static readonly string DefaultAesKey = "@#hussar@bb#11223";
    private static byte[] _keys = { 0x41, 0x72, 0x65, 0x79, 0x6F, 0x75, 0x6D, 0x79,
        0x53,0x6E, 0x6F, 0x77, 0x6D, 0x61, 0x6E, 0x3F };

    /// <summary>
    /// 对称加密算法AES RijndaelManaged加密(RijndaelManaged（AES）算法是块式加密算法)
    /// </summary>
    /// <param name="encryptString">待加密字符串</param>
    /// <returns>加密结果字符串</returns>
    public static string AES_Encrypt(string encryptString)
    {
        return AES_Encrypt(encryptString, DefaultAesKey);
    }

    /// <summary>
    /// 对称加密算法AES RijndaelManaged加密(RijndaelManaged（AES）算法是块式加密算法)
    /// </summary>
    /// <param name="encryptString">待加密字符串</param>
    /// <param name="encryptKey">加密密钥，须半角字符</param>
    /// <returns>加密结果字符串</returns>
    public static string AES_Encrypt(string encryptString, string encryptKey)
    {
        encryptKey = GetSubString(encryptKey, 32, "");
        encryptKey = encryptKey.PadRight(32, ' ');

        RijndaelManaged rijndaelProvider = new RijndaelManaged();
        rijndaelProvider.Key = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 32));
        rijndaelProvider.IV = _keys;
        ICryptoTransform rijndaelEncrypt = rijndaelProvider.CreateEncryptor();

        byte[] inputData = Encoding.UTF8.GetBytes(encryptString);
        byte[] encryptedData = rijndaelEncrypt.TransformFinalBlock(inputData, 0, inputData.Length);

        return Convert.ToBase64String(encryptedData);
    }

    /// <summary>
    /// 对称加密算法AES RijndaelManaged解密字符串
    /// </summary>
    /// <param name="decryptString">待解密的字符串</param>
    /// <returns>解密成功返回解密后的字符串,失败返源串</returns>
    public static string AES_Decrypt(string decryptString)
    {
        return AES_Decrypt(decryptString, DefaultAesKey);
    }

    /// <summary>
    /// 对称加密算法AES RijndaelManaged解密字符串
    /// </summary>
    /// <param name="decryptString">待解密的字符串</param>
    /// <param name="decryptKey">解密密钥,和加密密钥相同</param>
    /// <returns>解密成功返回解密后的字符串,失败返回空</returns>
    public static string AES_Decrypt(string decryptString, string decryptKey)
    {
        try
        {
            decryptKey = GetSubString(decryptKey, 32, "");
            decryptKey = decryptKey.PadRight(32, ' ');

            RijndaelManaged rijndaelProvider = new RijndaelManaged();
            rijndaelProvider.Key = Encoding.UTF8.GetBytes(decryptKey);
            rijndaelProvider.IV = _keys;
            ICryptoTransform rijndaelDecrypt = rijndaelProvider.CreateDecryptor();

            byte[] inputData = Convert.FromBase64String(decryptString);
            byte[] decryptedData = rijndaelDecrypt.TransformFinalBlock(inputData, 0, inputData.Length);

            return Encoding.UTF8.GetString(decryptedData);
        }
        catch
        {
            return string.Empty;
        }
    }

    /// <summary>
    /// 按字节长度(按字节,一个汉字为2个字节)取得某字符串的一部分
    /// </summary>
    /// <param name="sourceString">源字符串</param>
    /// <param name="length">所取字符串字节长度</param>
    /// <param name="tailString">附加字符串(当字符串不够长时，尾部所添加的字符串，一般为"...")</param>
    /// <returns>某字符串的一部分</returns>
    private static string GetSubString(string sourceString, int length, string tailString)
    {
        return GetSubString(sourceString, 0, length, tailString);
    }

    /// <summary>
    /// 按字节长度(按字节,一个汉字为2个字节)取得某字符串的一部分
    /// </summary>
    /// <param name="sourceString">源字符串</param>
    /// <param name="startIndex">索引位置，以0开始</param>
    /// <param name="length">所取字符串字节长度</param>
    /// <param name="tailString">附加字符串(当字符串不够长时，尾部所添加的字符串，一般为"...")</param>
    /// <returns>某字符串的一部分</returns>
    private static string GetSubString(string sourceString, int startIndex, int length, string tailString)
    {
        string myResult = sourceString;

        //当是日文或韩文时(注:中文的范围:\u4e00 - \u9fa5, 日文在\u0800 - \u4e00, 韩文为\xAC00-\xD7A3)
        if (System.Text.RegularExpressions.Regex.IsMatch(sourceString, "[\u0800-\u4e00]+") ||
            System.Text.RegularExpressions.Regex.IsMatch(sourceString, "[\xAC00-\xD7A3]+"))
        {
            //当截取的起始位置超出字段串长度时
            if (startIndex >= sourceString.Length)
            {
                return string.Empty;
            }
            else
            {
                return sourceString.Substring(startIndex,
                    ((length + startIndex) > sourceString.Length) ? (sourceString.Length - startIndex) : length);
            }
        }

        //中文字符，如"中国人民abcd123"
        if (length <= 0)
        {
            return string.Empty;
        }
        byte[] bytesSource = Encoding.Default.GetBytes(sourceString);

        //当字符串长度大于起始位置
        if (bytesSource.Length > startIndex)
        {
            int endIndex = bytesSource.Length;

            //当要截取的长度在字符串的有效长度范围内
            if (bytesSource.Length > (startIndex + length))
            {
                endIndex = length + startIndex;
            }
            else
            {   //当不在有效范围内时,只取到字符串的结尾
                length = bytesSource.Length - startIndex;
                tailString = "";
            }

            int[] anResultFlag = new int[length];
            int nFlag = 0;
            //字节大于127为双字节字符
            for (int i = startIndex; i < endIndex; i++)
            {
                if (bytesSource[i] > 127)
                {
                    nFlag++;
                    if (nFlag == 3)
                    {
                        nFlag = 1;
                    }
                }
                else
                {
                    nFlag = 0;
                }
                anResultFlag[i] = nFlag;
            }
            //最后一个字节为双字节字符的一半
            if ((bytesSource[endIndex - 1] > 127) && (anResultFlag[length - 1] == 1))
            {
                length = length + 1;
            }

            byte[] bsResult = new byte[length];
            Array.Copy(bytesSource, startIndex, bsResult, 0, length);
            myResult = Encoding.Default.GetString(bsResult);
            myResult = myResult + tailString;

            return myResult;
        }

        return string.Empty;

    }

    /// <summary>
    /// 加密文件流
    /// </summary>
    /// <param name="fs">文件流对象</param>
    /// <param name="encryptKey">加密键</param>
    /// <returns></returns>
    public static CryptoStream AES_EncryptStrream(FileStream fs, string encryptKey)
    {
        encryptKey = GetSubString(encryptKey, 32, "");
        encryptKey = encryptKey.PadRight(32, ' ');

        RijndaelManaged rijndaelProvider = new RijndaelManaged();
        rijndaelProvider.Key = Encoding.UTF8.GetBytes(encryptKey);
        rijndaelProvider.IV = _keys;

        ICryptoTransform encrypto = rijndaelProvider.CreateEncryptor();
        CryptoStream cytptostreamEncr = new CryptoStream(fs, encrypto, CryptoStreamMode.Write);
        return cytptostreamEncr;
    }

    /// <summary>
    /// 解密文件流
    /// </summary>
    /// <param name="fs">文件流对象</param>
    /// <param name="decryptKey">解密键</param>
    /// <returns></returns>
    public static CryptoStream AES_DecryptStream(FileStream fs, string decryptKey)
    {
        decryptKey = GetSubString(decryptKey, 32, "");
        decryptKey = decryptKey.PadRight(32, ' ');

        RijndaelManaged rijndaelProvider = new RijndaelManaged();
        rijndaelProvider.Key = Encoding.UTF8.GetBytes(decryptKey);
        rijndaelProvider.IV = _keys;
        ICryptoTransform decrypto = rijndaelProvider.CreateDecryptor();
        CryptoStream cytptostreamDecr = new CryptoStream(fs, decrypto, CryptoStreamMode.Read);
        return cytptostreamDecr;
    }

    /// <summary>
    /// 对指定文件加密
    /// </summary>
    /// <param name="inputFile">输入文件</param>
    /// <param name="outputFile">输出文件</param>
    /// <returns></returns>
    public static bool AES_EncryptFile(string inputFile, string outputFile)
    {
        try
        {
            string decryptKey = "bbwinform";

            FileStream fr = new FileStream(inputFile, FileMode.Open);
            FileStream fren = new FileStream(outputFile, FileMode.Create);
            CryptoStream enfr = AES_EncryptStrream(fren, decryptKey);
            byte[] bytearrayinput = new byte[fr.Length];
            fr.Read(bytearrayinput, 0, bytearrayinput.Length);
            enfr.Write(bytearrayinput, 0, bytearrayinput.Length);
            enfr.Close();
            fr.Close();
            fren.Close();
        }
        catch
        {
            //文件异常
            return false;
        }
        return true;
    }

    /// <summary>
    /// 对指定的文件解压缩
    /// </summary>
    /// <param name="inputFile">输入文件</param>
    /// <param name="outputFile">输出文件</param>
    /// <returns></returns>
    public static bool AES_DecryptFile(string inputFile, string outputFile)
    {
        try
        {
            string decryptKey = "bbwinform";
            FileStream fr = new FileStream(inputFile, FileMode.Open);
            FileStream frde = new FileStream(outputFile, FileMode.Create);
            CryptoStream defr = AES_DecryptStream(fr, decryptKey);
            byte[] bytearrayoutput = new byte[1024];
            int mCount = 0;

            do
            {
                mCount = defr.Read(bytearrayoutput, 0, bytearrayoutput.Length);
                frde.Write(bytearrayoutput, 0, mCount);
                if (mCount < bytearrayoutput.Length)
                    break;
            } while (true);

            defr.Close();
            fr.Close();
            frde.Close();
        }
        catch
        {
            //文件异常
            return false;
        }
        return true;
    }
        
    #endregion

    #region Base64加密解密
    /// <summary>
    /// Base64是一種使用64基的位置計數法。它使用2的最大次方來代表僅可列印的ASCII 字元。
    /// 這使它可用來作為電子郵件的傳輸編碼。在Base64中的變數使用字元A-Z、a-z和0-9 ，
    /// 這樣共有62個字元，用來作為開始的64個數字，最後兩個用來作為數字的符號在不同的
    /// 系統中而不同。
    /// Base64加密
    /// </summary>
    /// <param name="str">Base64方式加密字符串</param>
    /// <returns></returns>
    public static string Base64Encrypt(string str)
    {
        byte[] encbuff = Encoding.UTF8.GetBytes(str);
        return Convert.ToBase64String(encbuff);
    }

    /// <summary>
    /// Base64解密字符串
    /// </summary>
    /// <param name="str">待解密的字符串</param>
    /// <returns></returns>
    public static string Base64Decrypt(string str)
    {
        byte[] decbuff = Convert.FromBase64String(str);
        return Encoding.UTF8.GetString(decbuff);
    } 
    #endregion

    #region MD5加密

    /// <summary> 
    /// 使用MD5加密字符串
    /// </summary> 
    /// <param name="strText">待加密的字符串</param> 
    /// <returns>MD5加密后的字符串</returns> 
    public static string Md5Encrypt(string strText)
    {
        MD5 md5 = new MD5CryptoServiceProvider();
        byte[] result = md5.ComputeHash(Encoding.Default.GetBytes(strText));
        return Encoding.Default.GetString(result);
    }

    /// <summary>
    /// 使用MD5加密的Hash表
    /// </summary>
    /// <param name="input">待加密字符串</param>
    /// <returns></returns>
    public static string Md5EncryptHash(String input)
    {
        MD5 md5 = new MD5CryptoServiceProvider();
        //the GetBytes method returns byte array equavalent of a string
        byte[] res = md5.ComputeHash(Encoding.Default.GetBytes(input), 0, input.Length);
        char[] temp = new char[res.Length];
        //copy to a char array which can be passed to a String constructor
        Array.Copy(res, temp, res.Length);
        //return the result as a string
        return new String(temp);
    }

    /// <summary>
    /// 使用Md5加密为16进制字符串
    /// </summary>
    /// <param name="input">待加密字符串</param>
    /// <returns></returns>
    public static string Md5EncryptHashHex(String input)
    {
        MD5 md5 = new MD5CryptoServiceProvider();
        //the GetBytes method returns byte array equavalent of a string
        byte[] res = md5.ComputeHash(Encoding.Default.GetBytes(input), 0, input.Length);

        String returnThis = string.Empty;

        for (int i = 0; i < res.Length; i++)
        {
            returnThis += Uri.HexEscape((char)res[i]);
        }
        returnThis = returnThis.Replace("%", "");
        returnThis = returnThis.ToLower();

        return returnThis;
    }

    /// <summary>
    /// MD5 三次加密算法.计算过程: (QQ使用)
    /// 1. 验证码转为大写
    /// 2. 将密码使用这个方法进行三次加密后,与验证码进行叠加
    /// 3. 然后将叠加后的内容再次MD5一下,得到最终验证码的值
    /// </summary>
    /// <param name="s">待加密字符串</param>
    /// <returns></returns>
    public static string EncyptMD5_3_16(string s)
    {
        MD5 md5 = MD5.Create();
        byte[] bytes = Encoding.ASCII.GetBytes(s);
        byte[] bytes1 = md5.ComputeHash(bytes);
        byte[] bytes2 = md5.ComputeHash(bytes1);
        byte[] bytes3 = md5.ComputeHash(bytes2);

        StringBuilder sb = new StringBuilder();
        foreach (var item in bytes3)
        {
            sb.Append(item.ToString("x").PadLeft(2, '0'));
        }
        return sb.ToString().ToUpper();
    }
    #endregion

    /// <summary>
    /// SHA256函数
    /// </summary>
    /// <param name="str">原始字符串</param>
    /// <returns>SHA256结果(返回长度为44字节的字符串)</returns>
    public static string Sha256(string str)
    {
        byte[] sha256Data = Encoding.UTF8.GetBytes(str);
        SHA256Managed sha256 = new SHA256Managed();
        byte[] result = sha256.ComputeHash(sha256Data);
        return Convert.ToBase64String(result);  //返回长度为44字节的字符串
    }

    /// <summary>
    /// 加密字符串
    /// </summary>
    /// <param name="input">待加密的字符串</param>
    /// <returns></returns>
    public static string EncryptString(string input)
    {
        return Md5Util.AddMd5Profix(Base64Util.Encrypt(Md5Util.AddMd5Profix(input)));
        //return Base64.Encrypt(MD5.AddMD5Profix(Base64.Encrypt(input)));
    }

    /// <summary>
    /// 解密加过密的字符串
    /// </summary>
    /// <param name="input">待解密的字符串</param>
    /// <param name="throwException">解密失败是否抛异常</param>
    /// <returns></returns>
    public static string DecryptString(string input, bool throwException)
    {
        string res = "";
        try
        {
            res = input;// Base64.Decrypt(input);
            if (Md5Util.ValidateValue(res))
            {
                return Md5Util.RemoveMd5Profix(Base64Util.Decrypt(Md5Util.RemoveMd5Profix(res)));
            }
            else
            {
                throw new Exception("字符串无法转换成功！");
            }
        }
        catch
        {
            if (throwException)
            {
                throw;
            }
            else
            {
                return "";
            }
        }
    }
}