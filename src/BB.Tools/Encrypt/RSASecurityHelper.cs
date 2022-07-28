using System.Security.Cryptography;
using System.Text;

namespace BB.Tools.Encrypt;

/// <summary>
/// 非对称加密、解密、验证辅助类
/// </summary>
public static class RsaSecurityHelper
{
    //RSA是常用的非对称加密算法。使用System.Security类库中的RSA加密算法时，出现了“不正确的长度”，这是因为待加密的数据超长所致。
    //.NET 中提供的RSA算法规定，每次加密的字节数，不能超过密钥的长度值减去11, 而每次加密得到的密文长度，却恰恰是密钥的长度。
    //所以，如果要加密较长的数据，可以采用数据截取的方法，分段进行加解密。

    /// <summary>
    /// 非对称加密生成的私钥和公钥 
    /// </summary>
    /// <param name="privateKey">私钥</param>
    /// <param name="publicKey">公钥</param>
    public static void GenerateRsaKey(out string privateKey, out string publicKey)
    {
        RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
        privateKey = rsa.ToXmlString(true);
        publicKey = rsa.ToXmlString(false);
    }
        
    #region 非对称数据加密（公钥加密）

    /// <summary>
    /// 非对称加密字符串数据，返回加密后的数据
    /// </summary>
    /// <param name="publicKey">公钥</param>
    /// <param name="originalString">待加密的字符串</param>
    /// <returns>加密后的数据</returns>
    public static string RsaEncrypt(string publicKey, string originalString)
    {
        //byte[] PlainTextBArray;
        //byte[] CypherTextBArray;
        //string Result;
        //RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
        //rsa.FromXmlString(publicKey);
        //PlainTextBArray = (new UnicodeEncoding()).GetBytes(originalString);
        //CypherTextBArray = rsa.Encrypt(PlainTextBArray, false);
        //Result = Convert.ToBase64String(CypherTextBArray);
        //return Result;

        //分段加密方法
        RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
        rsa.FromXmlString(publicKey);
        byte[] originalBytes = (new UnicodeEncoding()).GetBytes(originalString);

        //实现分段
        int keySize = rsa.KeySize / 8;
        int bufferSize = keySize - 11;
        byte[] buffer = new byte[bufferSize];
        MemoryStream msInput = new MemoryStream(originalBytes);
        MemoryStream msOutput = new MemoryStream();

        int readLen = msInput.Read(buffer, 0, bufferSize);
        while (readLen > 0)
        {
            byte[] dataToEnc = new byte[readLen];
            Array.Copy(buffer, 0, dataToEnc, 0, readLen);

            byte[] encData = rsa.Encrypt(dataToEnc, false);
            msOutput.Write(encData, 0, encData.Length);

            readLen = msInput.Read(buffer, 0, bufferSize);
        }

        msInput.Close();
        byte[] result = msOutput.ToArray();
        var strResult = Convert.ToBase64String(result);

        //得到加密结果
        msOutput.Close();
        rsa.Clear();

        return strResult;
    }

    /// <summary>
    /// 非对称加密字节数组，返回加密后的数据
    /// </summary>
    /// <param name="publicKey">公钥</param>
    /// <param name="originalBytes">待加密的字节数组</param>
    /// <returns>返回加密后的数据</returns>
    public static byte[] RsaEncrypt(string publicKey, byte[] originalBytes)
    {
        RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
        rsa.FromXmlString(publicKey);

        //实现分段
        int keySize = rsa.KeySize / 8;
        int bufferSize = keySize - 11;
        byte[] buffer = new byte[bufferSize];
        MemoryStream msInput = new MemoryStream(originalBytes);
        MemoryStream msOutput = new MemoryStream();
            
        int readLen = msInput.Read(buffer, 0,  bufferSize);
        while (readLen > 0)
        {
            byte[] dataToEnc = new byte[readLen];
            Array.Copy(buffer, 0, dataToEnc, 0, readLen);
                
            byte[] encData = rsa.Encrypt(dataToEnc, false);
            msOutput.Write(encData, 0, encData.Length);

            readLen = msInput.Read(buffer, 0, bufferSize);
        }

        msInput.Close();
        byte[] result = msOutput.ToArray();
            
        //得到加密结果
        msOutput.Close();
        rsa.Clear();

        return result;
    } 

    #endregion

    #region 非对称解密（私钥解密）

    /// <summary>
    /// 非对称解密字符串，返回解密后的数据
    /// </summary>
    /// <param name="privateKey">私钥</param>
    /// <param name="encryptedString">待解密数据</param>
    /// <returns>返回解密后的数据</returns>
    public static string RsaDecrypt(string privateKey, string encryptedString)
    {
        //byte[] PlainTextBArray;
        //byte[] DypherTextBArray;
        //string Result;
        //System.Security.Cryptography.RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
        //rsa.FromXmlString(privateKey);
        //PlainTextBArray = Convert.FromBase64String(encryptedString);
        //DypherTextBArray = rsa.Decrypt(PlainTextBArray, false);
        //Result = (new UnicodeEncoding()).GetString(DypherTextBArray);
        //return Result;

        RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
        rsa.FromXmlString(privateKey);

        int keySize = rsa.KeySize / 8;
        byte[] buffer = new byte[keySize];
            
        var dataEnc = Convert.FromBase64String(encryptedString);
        MemoryStream msInput = new MemoryStream(dataEnc);            
        MemoryStream msOutput = new MemoryStream();
            
        int readLen = msInput.Read(buffer, 0, keySize);
        while (readLen > 0)
        {
            byte[] dataToDec = new byte[readLen];                
            Array.Copy(buffer, 0, dataToDec, 0, readLen);
                
            byte[] decData = rsa.Decrypt(dataToDec, false);
            msOutput.Write(decData, 0, decData.Length);                
            readLen = msInput.Read(buffer, 0, keySize);
        }
        msInput.Close();

        byte[] result = msOutput.ToArray();
        var strResult = (new UnicodeEncoding()).GetString(result);//得到解密结果            
        msOutput.Close();
        rsa.Clear();

        return strResult;
    }

    /// <summary>
    /// 非对称解密字节数组，返回解密后的数据
    /// </summary>
    /// <param name="privateKey">私钥</param>
    /// <param name="encryptedBytes">待解密数据</param>
    /// <returns></returns>
    public static byte[] RsaDecrypt(string privateKey, byte[] encryptedBytes)
    {
        RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
        rsa.FromXmlString(privateKey);

        int keySize = rsa.KeySize / 8;
        byte[] buffer = new byte[keySize];

        MemoryStream msInput = new MemoryStream(encryptedBytes);
        MemoryStream msOutput = new MemoryStream();

        int readLen = msInput.Read(buffer, 0, keySize);
        while (readLen > 0)
        {
            byte[] dataToDec = new byte[readLen];
            Array.Copy(buffer, 0, dataToDec, 0, readLen);

            byte[] decData = rsa.Decrypt(dataToDec, false);
            msOutput.Write(decData, 0, decData.Length);
            readLen = msInput.Read(buffer, 0, keySize);
        }
        msInput.Close();

        byte[] result = msOutput.ToArray();//得到解密结果            
        msOutput.Close();
        rsa.Clear();

        return result;
    } 
    #endregion

    #region 非对称加密签名、验证

    /// <summary>
    /// 使用非对称加密签名数据
    /// </summary>
    /// <param name="originalString">待加密的字符串</param>
    /// <param name="privateKey">私钥</param>
    /// <returns>加密后的数据</returns>
    public static string RsaEncryptSignature(string originalString, string privateKey = "password")
    {
        using RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
        rsa.FromXmlString(privateKey); //私钥
        // 加密对象 
        var f = new RSAPKCS1SignatureFormatter(rsa);
        f.SetHashAlgorithm("SHA512");
        byte[] source = Encoding.ASCII.GetBytes(originalString);
        using var sha = SHA512.Create();
        byte[] result = sha.ComputeHash(source);
        byte[] b = f.CreateSignature(result);
        return Convert.ToBase64String(b);
    }
              
    /// <summary>
    /// 对私钥加密签名的字符串，使用公钥对其进行验证
    /// </summary>
    /// <param name="originalString">未加密的文本，如机器码</param>
    /// <param name="encryptedString">加密后的文本，如注册序列号</param>
    /// <returns>如果验证成功返回True，否则为False</returns>
    public static bool Validate(string originalString, string encryptedString)
    {
        return Validate(originalString, encryptedString, UiConstants.PublicKey);
    }

    /// <summary>
    /// 对私钥加密的字符串，使用公钥对其进行验证
    /// </summary>
    /// <param name="originalString">未加密的文本，如机器码</param>
    /// <param name="encryptedString">加密后的文本，如注册序列号</param>
    /// <param name="publicKey">非对称加密的公钥</param>
    /// <returns>如果验证成功返回True，否则为False</returns>
    public static bool Validate(string originalString, string encryptedString, string publicKey)
    {
        bool bPassed = false;
        using var rsa = new RSACryptoServiceProvider();
        try
        {
            rsa.FromXmlString(publicKey); //公钥
            var formatter = new RSAPKCS1SignatureDeformatter(rsa);
            formatter.SetHashAlgorithm("SHA512");

            byte[] key = Convert.FromBase64String(encryptedString); //验证
            using var sha = SHA512.Create();
            byte[] name = sha.ComputeHash(Encoding.ASCII.GetBytes(originalString));
            if (formatter.VerifySignature(name, key))
            {
                bPassed = true;
            }
        }
        catch
        {
            // ignored
        }

        return bPassed;
    }

    #endregion

    #region Hash 加密

    /// <summary> Hash 加密 </summary>
    /// <param name="str2Hash"></param>
    /// <returns></returns>
    public static int HashEncrypt(string str2Hash)
    {
        const int salt = 100716;    // 盐值
        str2Hash += "Commons";       // 增加一个常量字符串

        int len = str2Hash.Length;
        int result = (str2Hash[len - 1] - 31) * 95 + salt;
        for (int i = 0; i < len - 1; i++)
        {
            result = (result * 95) + (str2Hash[i] - 32);
        }

        return result;
    }

    /// <summary>
    /// MD5加密
    /// </summary>
    /// <param name="str">待加密字串</param>
    /// <returns>加密后的字串</returns>
    public static string ComputeMd5(string str)
    {
        byte[] hashValue = ComputeMd5Data(str);
        return BitConverter.ToString(hashValue).Replace("-", "");
    }

    /// <summary>
    /// MD5加密
    /// </summary>
    /// <param name="input">待加密字串</param>
    /// <returns>加密后的字串</returns>
    public static byte[] ComputeMd5Data(string input)
    {
        byte[] buffer = Encoding.UTF8.GetBytes(input);
        return MD5.Create().ComputeHash(buffer);
    }

    /// <summary>
    /// MD5加密
    /// </summary>
    /// <param name="data">待加密数据</param>
    /// <returns>加密后的字串</returns>
    public static byte[] ComputeMd5Data(byte[] data)
    {
        return MD5.Create().ComputeHash(data);
    }

    /// <summary>
    /// MD5加密
    /// </summary>
    /// <param name="stream">待加密流</param>
    /// <returns>加密后的字串</returns>
    public static byte[] ComputeMd5Data(Stream stream)
    {
        return MD5.Create().ComputeHash(stream);
    }
    #endregion
}