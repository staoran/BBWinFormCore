namespace BB.Tools.Utils;

/// <summary>
/// 包含解析的命令行参数。这包括两个列表，一个是参数对，一个是独立的参数。
/// 作为ArgsParser辅助类相同功能的补充。
/// </summary>
public class CommandArgs
{
    /// <summary>
    /// 返回参数/参数值的键值字典
    /// </summary>
    public Dictionary<string, string> ArgPairs => _mArgPairs;

    /// <summary>
    /// 返回独立的参数列表
    /// </summary>
    public List<string> Params => _mParams;

    Dictionary<string, string> _mArgPairs = new Dictionary<string,string>();
    List<string> _mParams = new List<string>();
}

/// <summary>
/// 实现命令行解析的辅助类。
/// 基本上，独立的参数可以在命令行上的任何地方出现。
/// 参数是定义为键/值对。参数键必须以'-', '--'或者'\'开始，在参数和值之间必须有
/// 一个空格或者字符'='。多余的空格将被忽略。
/// 参数后面必须跟着一个值，如果没有指定值，那么字符串'true'将被指定。
/// 如果值有空格，必须使用双引号来包含字符，否则字符不能被正确解析。  
/// </summary>
public class CommandLine
{
    /// <summary>
    /// 解析传递的命令行参数，并返回结果到一个CommandArgs对象。
    /// 假设命令行格式: CMD [param] [[-|--|\]&lt;arg&gt;[[=]&lt;value&gt;]] [param]
    /// 例如：cmd first -o outfile.txt --compile second \errors=errors.txt third fourth --test = "the value" fifth
    /// </summary>
    /// <param name="args">命令行参数数组</param>
    /// <returns>包含转换后的命令行对象CommandArgs</returns>
    public static CommandArgs Parse(string[] args)
    {
        char[] kEqual = new[] { '=' };
        char[] kArgStart = new[] { '-', '\\' };

        CommandArgs ca = new CommandArgs();
        int ii = -1;
        string token = NextToken( args, ref ii );
        while ( token != null )
        {
            if (IsArg(token))
            {
                string arg = token.TrimStart(kArgStart).TrimEnd(kEqual);

                string value = null;

                if (arg.Contains("="))
                {
                    // arg was specified with an '=' sign, so we need
                    // to split the string into the arg and value, but only
                    // if there is no space between the '=' and the arg and value.
                    string[] r = arg.Split(kEqual, 2);
                    if ( r.Length == 2 && r[1] != string.Empty)
                    {
                        arg = r[0];
                        value = r[1];
                    }
                }
                    
                while ( value == null )
                {
                    string next = NextToken(args, ref ii);
                    if (next != null)
                    {
                        if (IsArg(next))
                        {
                            // push the token back onto the stack so
                            // it gets picked up on next pass as an Arg
                            ii--;
                            value = "true";
                        }
                        else if (next != "=")
                        {
                            // save the value (trimming any '=' from the start)
                            value = next.TrimStart(kEqual);
                        }
                    }
                }

                // save the pair
                ca.ArgPairs.Add(arg, value);
            }
            else if (token != string.Empty)
            {
                // this is a stand-alone parameter. 
                ca.Params.Add(token);
            }

            token = NextToken(args, ref ii);
        }

        return ca;
    }

    /// <summary>
    /// 返回true如果传递的字符串是一个参数（以'-'，'--',或'\'开始）
    /// </summary>
    /// <param name="arg">测试的字符串标记</param>
    /// <returns>如果传递的字符串是一个参数返回true，否则false</returns>
    static bool IsArg(string arg)
    {
        return (arg.StartsWith("-") || arg.StartsWith("\\"));
    }

    /// <summary>
    /// 返回参数列表中的下一个字符串标记
    /// </summary>
    /// <param name="args">字符串标记列表</param>
    /// <param name="ii">当前字符串标记在列表数组中的索引位置</param>
    /// <returns>下一个字符串标记，如果不存在则返回null</returns>
    static string NextToken(string[] args, ref int ii)
    {
        ii++; // move to next token
        while ( ii < args.Length )
        {
            string cur = args[ii].Trim();
            if (cur != string.Empty)
            {
                // found valid token
                return cur;
            }
            ii++;
        }

        // failed to get another token
        return null;
    }

}