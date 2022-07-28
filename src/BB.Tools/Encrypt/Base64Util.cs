using System.Text;

namespace BB.Tools.Encrypt;

/// <summary>
/// 基于Base64的加密编码辅助类，
/// 可以设置不同的密码表来获取不同的编码与解码
/// </summary>
public class Base64Util
{
    /// <summary>
    /// 构造函数，初始化编码表
    /// </summary>
    public Base64Util()
    {
        InitDict();
    }

    protected static Base64Util SB64 = new Base64Util();

    /// <summary>
    /// 使用默认的密码表加密字符串
    /// </summary>
    /// <param name="input">待加密字符串</param>
    /// <returns></returns>
    public static string Encrypt(string input)
    {
        return SB64.Encode(input);
    }
    /// <summary>
    /// 使用默认的密码表解密字符串
    /// </summary>
    /// <param name="input">待解密字符串</param>
    /// <returns></returns>
    public static string Decrypt(string input)
    {
        return SB64.Decode(input);
    }

    /// <summary>
    /// 获取具有标准的Base64密码表的加密类
    /// </summary>
    /// <returns></returns>
    public static Base64Util GetStandardBase64()
    {
        Base64Util b64 = new Base64Util
        {
            Pad = "=",
            CodeTable = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/"
        };
        return b64;
    }

    protected string MCodeTable = @"ABCDEFGHIJKLMNOPQRSTUVWXYZbacdefghijklmnopqrstu_wxyz0123456789*-";
    protected string MPad = "v";
    protected Dictionary<int, char> MT1 = new Dictionary<int, char>();
    protected Dictionary<char, int> MT2 = new Dictionary<char, int>();

    /// <summary>
    /// 密码表
    /// </summary>
    public string CodeTable
    {
        get => MCodeTable;
        set
        {
            if (value == null)
            {
                throw new Exception("密码表不能为null");
            }
            else if (value.Length < 64)
            {
                throw new Exception("密码表长度必须至少为64");
            }
            else
            {
                ValidateRepeat(value);
                ValidateEqualPad(value, MPad);
                MCodeTable = value;
                InitDict();
            }
        }
    }

    /// <summary>
    /// 补码
    /// </summary>
    public string Pad
    {
        get => MPad;
        set
        {
            if (value == null)
            {
                throw new Exception("密码表的补码不能为null");
            }
            else if (value.Length != 1)
            {
                throw new Exception("密码表的补码长度必须为1");
            }
            else
            {
                ValidateEqualPad(MCodeTable, value);
                MPad = value;
                InitDict();
            }
        }
    }

    /// <summary>
    /// 返回编码后的字符串
    /// </summary>
    /// <param name="source">原字符串</param>
    /// <returns></returns>
    public string Encode(string source)
    {
        if (source == null || source == "")
        {
            return "";
        }
        else
        {
            StringBuilder sb = new StringBuilder();
            byte[] tmp = Encoding.UTF8.GetBytes(source);
            int remain = tmp.Length % 3;
            int patch = 3 - remain;
            if (remain != 0)
            {
                Array.Resize(ref tmp, tmp.Length + patch);
            }
            int cnt = (int)Math.Ceiling(tmp.Length * 1.0 / 3);
            for (int i = 0; i < cnt; i++)
            {
                sb.Append(EncodeUnit(tmp[i * 3], tmp[i * 3 + 1], tmp[i * 3 + 2]));
            }
            if (remain != 0)
            {
                sb.Remove(sb.Length - patch, patch);
                for (int i = 0; i < patch; i++)
                {
                    sb.Append(MPad);
                }
            }
            return sb.ToString();
        }

    }

    protected string EncodeUnit(params byte[] unit)
    {
        int[] obj = new int[4];
        obj[0] = (unit[0] & 0xfc) >> 2;
        obj[1] = ((unit[0] & 0x03) << 4) + ((unit[1] & 0xf0) >> 4);
        obj[2] = ((unit[1] & 0x0f) << 2) + ((unit[2] & 0xc0) >> 6);
        obj[3] = unit[2] & 0x3f;
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < obj.Length; i++)
        {
            sb.Append(GetEc((int)obj[i]));
        }
        return sb.ToString();
    }

    protected char GetEc(int code)
    {
        return MT1[code];//m_codeTable[code];
    }

    /// <summary>
    /// 获得解码字符串
    /// </summary>
    /// <param name="source">原字符串</param>
    /// <returns></returns>
    public string Decode(string source)
    {
        if (source == null || source == "")
        {
            return "";
        }
        else
        {
            List<byte> list = new List<byte>();
            char[] tmp = source.ToCharArray();
            int remain = tmp.Length % 4;
            if (remain != 0)
            {
                Array.Resize(ref tmp, tmp.Length - remain);
            }
            int patch = source.IndexOf(MPad);
            if (patch != -1)
            {
                patch = source.Length - patch;
            }
            int cnt = tmp.Length / 4;
            for (int i = 0; i < cnt; i++)
            {
                DecodeUnit(list, tmp[i * 4], tmp[i * 4 + 1], tmp[i * 4 + 2], tmp[i * 4 + 3]);
            }
            for (int i = 0; i < patch; i++)
            {
                list.RemoveAt(list.Count - 1);
            }
            return Encoding.UTF8.GetString(list.ToArray());
        }
    }

    protected void DecodeUnit(List<byte> byteArr, params char[] chArray)
    {
        int[] res = new int[3];
        byte[] unit = new byte[chArray.Length];
        for (int i = 0; i < chArray.Length; i++)
        {
            unit[i] = FindChar(chArray[i]);
        }
        res[0] = (unit[0] << 2) + ((unit[1] & 0x30) >> 4);
        res[1] = ((unit[1] & 0xf) << 4) + ((unit[2] & 0x3c) >> 2);
        res[2] = ((unit[2] & 0x3) << 6) + unit[3];
        for (int i = 0; i < res.Length; i++)
        {
            byteArr.Add((byte)res[i]);
        }
    }

    protected byte FindChar(char ch)
    {
        int pos = MT2[ch];//m_codeTable.IndexOf(ch);
        return (byte)pos;
    }

    /// <summary>
    /// 初始化双向哈希字典
    /// </summary>
    protected void InitDict()
    {
        MT1.Clear();
        MT2.Clear();
        MT2.Add(MPad[0], -1);
        for (int i = 0; i < MCodeTable.Length; i++)
        {
            MT1.Add(i, MCodeTable[i]);
            MT2.Add(MCodeTable[i], i);
        }
    }

    /// <summary>
    /// 检查字符串中的字符是否有重复
    /// </summary>
    /// <param name="input">待检查字符串</param>
    /// <returns></returns>
    protected void ValidateRepeat(string input)
    {
        for (int i = 0; i < input.Length; i++)
        {
            if (input.LastIndexOf(input[i]) > i)
            {
                throw new Exception("密码表中含有重复字符：" + input[i]);
            }
        }
    }

    /// <summary>
    /// 检查字符串是否包含补码字符
    /// </summary>
    /// <param name="input">待检查字符串</param>
    /// <param name="pad"></param>
    protected void ValidateEqualPad(string input, string pad)
    {
        if (input.IndexOf(pad) > -1)
        {
            throw new Exception("密码表中包含了补码字符：" + pad);
        }
    }

    /// <summary>
    /// 测试
    /// </summary>
    protected void Test()
    {
        //m_codeTable = @"STUVWXYZbacdefghivklABCDEFGHIJKLMNOPQRmnopqrstu!wxyz0123456789+/";
        //m_pad = "j";

        InitDict();

        string test = "abc ABC 你好！◎＃￥％……!@#$%^";
        string encode = Encode("false");
        string decode = Decode(encode);
        Console.WriteLine(encode);
        Console.WriteLine(test == decode);
    }
}