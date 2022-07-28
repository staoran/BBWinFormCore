using System.Text;

namespace BB.Tools.Encrypt;

public sealed class EncryptHelper
{
	public static string ComputeHash(string source, string key)
	{
		if (source == null)
		{
			return "";
		}
		string text = "abcdefghjklmnopqrstuvwxyz";
		if (source.Length < 0x1a)
		{
			source = source + text.Substring(source.Length);
		}

		byte[] inArray = Encoding.Unicode.GetBytes(source);
		int length = inArray.Length;
		if ((key == null) || (key.Length == 0))
		{
			key = "Encrypthejinhua";
		}

		byte[] bytes = Encoding.Unicode.GetBytes(key);
		byte num2 = Convert.ToByte(bytes.Length);
		byte num3 = 2;
		byte index = 0;
		for (int i = 0; i < length; i++)
		{
			byte[] buffer3;
			IntPtr ptr;
			byte num5 = (byte) (bytes[index] | num2);
			num5 = (byte) (num5 & num3);
			(buffer3 = inArray)[(int) (ptr = (IntPtr) i)] = (byte) (buffer3[(int) ptr] ^ num5);
			num3 = (byte) (num3 + 1);
			if (num3 > 0xfd)
			{
				num3 = 2;
			}
			index = (byte) (index + 1);
			if (index >= num2)
			{
				index = 0;
			}
		}
		return Convert.ToBase64String(inArray, 0, inArray.Length);
	}

	public static string EncryptStr(string source, string key)
	{
		key = key.PadLeft(8, 'x');
		return EncodeHelper.DesEncrypt(source, key);
	}

	public static string UnEncryptStr(string source, string key)
	{
		key = key.PadLeft(8, 'x');
		return EncodeHelper.DesDecrypt(source, key);
	}
}