using System.Text;

namespace BB.Tools.File;

/// <summary>
/// 数据库字符串解析操作辅助类
/// </summary>
public class DatabaseInfo
{
	/// <summary>
	/// 构造函数
	/// </summary>
	public DatabaseInfo()
	{
	}

	/// <summary>
	/// 可以接受三种格式的数据库连接字符串
	/// 1. 服务名称=(local);数据库名称=EDNSM;用户名称=sa;用户密码=123456
	/// 2. Data Source=(local);Initial Catalog=EDNSM;User ID=sa;Password=123456
	/// 3. server=(local);uid=sa;pwd=;
	/// </summary>
	/// <param name="connectionString">连接字符串</param>
	public DatabaseInfo(string connectionString)
	{
		#region 服务器名

		_server = GetSubItemValue(connectionString, "服务名称");
		if (_server == null)
		{
			_server = GetSubItemValue(connectionString, "Data Source");
		}
		if (_server == null)
		{
			_server = GetSubItemValue(connectionString, "server");
		}

		#endregion

		#region 数据库名

		_database = GetSubItemValue(connectionString, "数据库名称");
		if (_database == null)
		{
			_database = GetSubItemValue(connectionString, "Initial Catalog");
		}
		if (_database == null)
		{
			_database = GetSubItemValue(connectionString, "database");
		}

		#endregion

		#region 用户名称

		_userId = GetSubItemValue(connectionString, "用户名称");
		if (_userId == null)
		{
			_userId = GetSubItemValue(connectionString, "User ID");
		}
		if (_userId == null)
		{
			_userId = GetSubItemValue(connectionString, "uid");
		}

		#endregion

		#region 用户密码

		_password = GetSubItemValue(connectionString, "用户密码");
		if (_password == null)
		{
			_password = GetSubItemValue(connectionString, "Password");
		}
		if (_password == null)
		{
			_password = GetSubItemValue(connectionString, "pwd");
		}

		#endregion
	}

	#region 变量及属性

	/// <summary>
	/// 数据库服务器
	/// </summary>
	public string Server
	{
		get => _server;
		set => _server = value;
	}

	/// <summary>
	/// 数据库
	/// </summary>
	public string Database
	{
		get => _database;
		set => _database = value;
	}

	/// <summary>
	/// 访问用户名
	/// </summary>
	public string UserId
	{
		get => _userId;
		set => _userId = value;
	}

	/// <summary>
	/// 访问密码
	/// </summary>
	public string Password
	{
		get => _password;
		set => _password = value;
	}

	private string _server;
	private string _database;
	private string _userId;
	private string _password;

	#endregion

	/// <summary>
	/// 加密后的连接字符串
	/// </summary>
	public string EncryptConnectionString => EncodeBase64(ConnectionString);


	/// <summary>
	/// 没有加密的字符串
	/// </summary>
	public string ConnectionString
	{
		get
		{
			string connString = "";
			if (!string.IsNullOrEmpty(UserId) && !string.IsNullOrEmpty(Password))
			{
				connString =
					$"Persist Security Info=False;Data Source={_server};Initial Catalog={_database};User ID={_userId};Password={_password};Packet Size=4096;Pooling=true;Max Pool Size=100;Min Pool Size=1";
			}
			else
			{
				connString =
					$"Persist Security Info=False;Data Source={_server};Initial Catalog={_database};Integrated Security=SSPI;Packet Size=4096;Pooling=true;Max Pool Size=100;Min Pool Size=1";
			}
			return connString;
		}
	}

	/// <summary>
	/// 提供OLEDB数据源的链接字符串
	/// </summary>
	public string OleDbConnectionString
	{
		get
		{
			string connectionPrefix = "Driver={SQL Server};";
			return connectionPrefix + ConnectionString;
		}
	}

	#region 辅助函数

	/// <summary>
	/// 获取给定字符串中的子节点的值, 如果不存在返回Null
	/// </summary>
	/// <param name="itemValueString">多个值的字符串</param>
	/// <param name="subKeyName"></param>
	/// <returns></returns>
	private string GetSubItemValue(string itemValueString, string subKeyName)
	{
		string[] item = itemValueString.Split(new[] {';'});
		for (int i = 0; i < item.Length; i++)
		{
			string itemValue = item[i].ToLower();
			if (itemValue.IndexOf(subKeyName.ToLower()) >= 0) //如果含有指定的关键字
			{
				int startIndex = item[i].IndexOf("="); //等号开始的位置
				return item[i].Substring(startIndex + 1); //获取等号后面的值即为Value
			}
		}
		return null;
	}


	private string EncodeBase64(string source)
	{
		byte[] buffer1 = Encoding.UTF8.GetBytes(source);
		return Convert.ToBase64String(buffer1, 0, buffer1.Length);
	}

	#endregion
}