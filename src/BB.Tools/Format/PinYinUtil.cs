using System.Collections;
using System.Globalization;
using System.Text;

namespace BB.Tools.Format;

/// <summary>
/// 中文字符串转换为拼音或者拼音首字母的辅助类，另一个辅助类可以使用Pinyin类
/// </summary>
public class PinYinUtil
{
    #region 私有字段
    /// <SUMMARY>
    /// 包含字符 ASC 码的整形数组。
    /// </SUMMARY>
    private static int[] _pinyinValues = new[]
    {
        -20319, -20317, -20304, -20295, -20292, -20283, -20265, -20257, -20242, -20230, -20051, -20036, -20032, -20026,
        -20002, -19990, -19986, -19982, -19976, -19805, -19784, -19775, -19774, -19763, -19756, -19751, -19746, -19741,
        -19739, -19728, -19725, -19715, -19540, -19531, -19525, -19515, -19500, -19484, -19479, -19467, -19289, -19288,
        -19281, -19275, -19270, -19263, -19261, -19249, -19243, -19242, -19238, -19235, -19227, -19224, -19218, -19212,
        -19038, -19023, -19018, -19006, -19003, -18996, -18977, -18961, -18952, -18783, -18774, -18773, -18763, -18756,
        -18741, -18735, -18731, -18722, -18710, -18697, -18696, -18526, -18518, -18501, -18490, -18478, -18463, -18448,
        -18447, -18446, -18239, -18237, -18231, -18220, -18211, -18201, -18184, -18183, -18181, -18012, -17997, -17988,
        -17970, -17964, -17961, -17950, -17947, -17931, -17928, -17922, -17759, -17752, -17733, -17730, -17721, -17703,
        -17701, -17697, -17692, -17683, -17676, -17496, -17487, -17482, -17468, -17454, -17433, -17427, -17417, -17202,
        -17185, -16983, -16970, -16942, -16915, -16733, -16708, -16706, -16689, -16664, -16657, -16647, -16474, -16470,
        -16465, -16459, -16452, -16448, -16433, -16429, -16427, -16423, -16419, -16412, -16407, -16403, -16401, -16393,
        -16220, -16216, -16212, -16205, -16202, -16187, -16180, -16171, -16169, -16158, -16155, -15959, -15958, -15944,
        -15933, -15920, -15915, -15903, -15889, -15878, -15707, -15701, -15681, -15667, -15661, -15659, -15652, -15640,
        -15631, -15625, -15454, -15448, -15436, -15435, -15419, -15416, -15408, -15394, -15385, -15377, -15375, -15369,
        -15363, -15362, -15183, -15180, -15165, -15158, -15153, -15150, -15149, -15144, -15143, -15141, -15140, -15139,
        -15128, -15121, -15119, -15117, -15110, -15109, -14941, -14937, -14933, -14930, -14929, -14928, -14926, -14922,
        -14921, -14914, -14908, -14902, -14894, -14889, -14882, -14873, -14871, -14857, -14678, -14674, -14670, -14668,
        -14663, -14654, -14645, -14630, -14594, -14429, -14407, -14399, -14384, -14379, -14368, -14355, -14353, -14345,
        -14170, -14159, -14151, -14149, -14145, -14140, -14137, -14135, -14125, -14123, -14122, -14112, -14109, -14099,
        -14097, -14094, -14092, -14090, -14087, -14083, -13917, -13914, -13910, -13907, -13906, -13905, -13896, -13894,
        -13878, -13870, -13859, -13847, -13831, -13658, -13611, -13601, -13406, -13404, -13400, -13398, -13395, -13391,
        -13387, -13383, -13367, -13359, -13356, -13343, -13340, -13329, -13326, -13318, -13147, -13138, -13120, -13107,
        -13096, -13095, -13091, -13076, -13068, -13063, -13060, -12888, -12875, -12871, -12860, -12858, -12852, -12849,
        -12838, -12831, -12829, -12812, -12802, -12607, -12597, -12594, -12585, -12556, -12359, -12346, -12320, -12300,
        -12120, -12099, -12089, -12074, -12067, -12058, -12039, -11867, -11861, -11847, -11831, -11798, -11781, -11604,
        -11589, -11536, -11358, -11340, -11339, -11324, -11303, -11097, -11077, -11067, -11055, -11052, -11045, -11041,
        -11038, -11024, -11020, -11019, -11018, -11014, -10838, -10832, -10815, -10800, -10790, -10780, -10764, -10587,
        -10544, -10533, -10519, -10331, -10329, -10328, -10322, -10315, -10309, -10307, -10296, -10281, -10274, -10270,
        -10262, -10260, -10256, -10254
    };



    /// <SUMMARY>
    /// 包含汉字拼音的字符串数组。
    /// </SUMMARY>
    private static string[] _pinyinNames = new[]
    {
        "a", "ai", "an", "ang", "ao", "ba", "bai", "ban", "bang", "bao", "bei", "ben", "beng", "bi", "bian", "biao", "bie",
        "bin", "bing", "bo", "bu", "ca", "cai", "can", "cang", "cao", "ce", "ceng", "cha", "chai", "chan", "chang", "chao",
        "che", "chen", "cheng", "chi", "chong", "chou", "chu", "chuai", "chuan", "chuang", "chui", "chun", "chuo", "ci",
        "cong", "cou", "cu", "cuan", "cui", "cun", "cuo", "da", "dai", "dan", "dang", "dao", "de", "deng", "di", "dian",
        "diao", "die", "ding", "diu", "dong", "dou", "du", "duan", "dui", "dun", "duo", "e", "en", "er", "fa", "fan", "fang",
        "fei", "fen", "feng", "fo", "fou", "fu", "ga", "gai", "gan", "gang", "gao", "ge", "gei", "gen", "geng", "gong", "gou",
        "gu", "gua", "guai", "guan", "guang", "gui", "gun", "guo", "ha", "hai", "han", "hang", "hao", "he", "hei", "hen", "heng",
        "hong", "hou", "hu", "hua", "huai", "huan", "huang", "hui", "hun", "huo", "ji", "jia", "jian", "jiang", "jiao", "jie",
        "jin", "jing", "jiong", "jiu", "ju", "juan", "jue", "jun", "ka", "kai", "kan", "kang", "kao", "ke", "ken", "keng",
        "kong", "kou", "ku", "kua", "kuai", "kuan", "kuang", "kui", "kun", "kuo", "la", "lai", "lan", "lang", "lao", "le",
        "lei", "leng", "li", "lia", "lian", "liang", "liao", "lie", "lin", "ling", "liu", "long", "lou", "lu", "lv", "luan",
        "lue", "lun", "luo", "ma", "mai", "man", "mang", "mao", "me", "mei", "men", "meng", "mi", "mian", "miao", "mie", "min",
        "ming", "miu", "mo", "mou", "mu", "na", "nai", "nan", "nang", "nao", "ne", "nei", "nen", "neng", "ni", "nian", "niang",
        "niao", "nie", "nin", "ning", "niu", "nong", "nu", "nv", "nuan", "nue", "nuo", "o", "ou", "pa", "pai", "pan", "pang",
        "pao", "pei", "pen", "peng", "pi", "pian", "piao", "pie", "pin", "ping", "po", "pu", "qi", "qia", "qian", "qiang",
        "qiao", "qie", "qin", "qing", "qiong", "qiu", "qu", "quan", "que", "qun", "ran", "rang", "rao", "re", "ren", "reng",
        "ri", "rong", "rou", "ru", "ruan", "rui", "run", "ruo", "sa", "sai", "san", "sang", "sao", "se", "sen", "seng", "sha",
        "shai", "shan", "shang", "shao", "she", "shen", "sheng", "shi", "shou", "shu", "shua", "shuai", "shuan", "shuang",
        "shui", "shun", "shuo", "si", "song", "sou", "su", "suan", "sui", "sun", "suo", "ta", "tai", "tan", "tang", "tao",
        "te", "teng", "ti", "tian", "tiao", "tie", "ting", "tong", "tou", "tu", "tuan", "tui", "tun", "tuo", "wa", "wai", "wan",
        "wang", "wei", "wen", "weng", "wo", "wu", "xi", "xia", "xian", "xiang", "xiao", "xie", "xin", "xing", "xiong", "xiu",
        "xu", "xuan", "xue", "xun", "ya", "yan", "yang", "yao", "ye", "yi", "yin", "ying", "yo", "yong", "you", "yu", "yuan",
        "yue", "yun", "za", "zai", "zan", "zang", "zao", "ze", "zei", "zen", "zeng", "zha", "zhai", "zhan", "zhang", "zhao",
        "zhe", "zhen", "zheng", "zhi", "zhong", "zhou", "zhu", "zhua", "zhuai", "zhuan", "zhuang", "zhui", "zhun", "zhuo", "zi",
        "zong", "zou", "zu", "zuan", "zui", "zun", "zuo"
    };
    private static Hashtable _phraseSpecial;
    #endregion

    #region 公有方法
    /// <summary>
    /// 将指定中文字符串转换为拼音形式
    /// </summary>
    /// <param name="chs">要转换的中文字符串</param>
    /// <param name="separator">连接拼音之间的分隔符</param>
    /// <param name="initialCap">指定是否将首字母大写</param>
    /// <returns>包含中文字符串的拼音的字符串</returns>
    public static string ChsToPinyin(string chs, string separator, bool initialCap)
    {

        if (chs == null || chs.Length == 0) return "";
        if (separator == null || separator.Length == 0) separator = "";
        // 例外词组
        foreach (DictionaryEntry de in ChsPhraseSpecial)
        {
            chs = chs.Replace(de.Key.ToString(), $"\r\r\r{de.Value.ToString().Replace(" ", separator)}\r\r\r");
        }
        byte[] array = new byte[2];
        string returnstr = "";
        int chrasc = 0;
        int i1 = 0;
        int i2 = 0;
        bool b = false, d = false;
        char[] nowchar = chs.ToCharArray();
        CultureInfo ci = Thread.CurrentThread.CurrentCulture;
        TextInfo ti = ci.TextInfo;

        for (int j = 0; j < nowchar.Length; j++)
        {
            array = Encoding.Default.GetBytes(nowchar[j].ToString());
            string s = nowchar[j].ToString(); ;
            if (array.Length == 1)
            {
                b = true;
                returnstr += s;
            }
            else
            {
                i1 = (short)(array[0]);
                i2 = (short)(array[1]);
                chrasc = i1 * 256 + i2 - 65536;
                for (int i = (_pinyinValues.Length - 1); i >= 0; i--)
                {
                    if (_pinyinValues[i] <= chrasc)
                    {
                        d = true;
                        s = _pinyinNames[i];
                        if (initialCap == true) s = ti.ToTitleCase(s);
                        if (returnstr == "" || b == true)
                            returnstr += s;
                        else
                            returnstr += separator + s;

                        break;
                    }
                    else
                    {
                        d = false;
                    }
                }
                if (d == false) returnstr += s;
                b = false;
            }
        }

        returnstr = returnstr.Replace("\r\r\r", separator);
        return returnstr;
    }

    /// <summary>
    /// 将指定中文字符串转换为拼音形式
    /// </summary>
    /// <param name="chs">要转换的中文字符串</param>
    /// <param name="separator">连接拼音之间的分隔符</param>
    /// <returns>包含中文字符串的拼音的字符串</returns>
    public static string ChsToPinyin(string chs, string separator)
    {
        return ChsToPinyin(chs, separator, false);
    }

    /// <summary>
    /// 将指定中文字符串转换为拼音形式
    /// </summary>
    /// <param name="chs">要转换的中文字符串</param>
    /// <param name="initialCap">指定是否将首字母大写</param>
    /// <returns>包含中文字符串的拼音的字符串</returns>
    public static string ChsToPinyin(string chs, bool initialCap)
    {
        return ChsToPinyin(chs, "", initialCap);
    }

    /// <summary>
    /// 将指定中文字符串转换为拼音形式
    /// </summary>
    /// <param name="chs">要转换的中文字符串</param>
    /// <returns>包含中文字符串的拼音的字符串</returns>
    public static string ChsToPinyin(string chs)
    {
        return ChsToPinyin(chs, "");
    }


    /// <summary>
    /// 这个方法是用来获得一个字符串的每个字的拼音首字母组成所需的字符串
    /// </summary>
    /// <param name="chsStr">要转换的中文或字母字符串</param>
    /// <returns></returns>
    public static string GetFirstPy(string chsStr)
    {
        int len = chsStr.Length;
        string myStr = "";
        for (int i = 0; i < len; i++)
        {
            myStr += GetSpell(chsStr.Substring(i, 1));
        }

        return myStr.ToUpper();
    }

    /// <summary>
    /// 用来获得一个字的拼音首字母
    /// </summary>
    /// <param name="cnChar"></param>
    /// <returns></returns>
    private static string GetSpell(string cnChar)
    {
        //将汉字转化为ASNI码,二进制序列
        byte[] arrCn = Encoding.Default.GetBytes(cnChar);
        if (arrCn.Length > 1)
        {
            int area = (short)arrCn[0];
            int pos = (short)arrCn[1];
            int code = (area << 8) + pos;
            int[] areacode = {45217,45253,45761,46318,46826,47010,47297,47614,48119,48119,49062,
                49324,49896,50371,50614,50622,50906,51387,51446,52218,52698,52698,
                52698,52980,53689,54481};
            for (int i = 0; i < 26; i++)
            {
                int max = 55290;
                if (i != 25) max = areacode[i + 1];
                if (areacode[i] <= code && code < max)
                {
                    return Encoding.Default.GetString(new[] { (byte)(65 + i) });
                }
            }
            return "*";
        }
        else
        {
            return cnChar;
        }
    }

    #endregion

    #region 公有属性
    /// <summary>
    /// 设置或获取包含例外词组读音的键/值对的组合
    /// </summary>
    public static Hashtable ChsPhraseSpecial
    {
        get
        {

            if (_phraseSpecial == null)
            {
                _phraseSpecial = new Hashtable();
                _phraseSpecial.Add("重庆", "Chong Qing");
                _phraseSpecial.Add("深圳", "Shen Zhen");
                _phraseSpecial.Add("什么", "Shen Me");
            }
            return _phraseSpecial;
        }
        set => _phraseSpecial = value;
    }
    #endregion

}