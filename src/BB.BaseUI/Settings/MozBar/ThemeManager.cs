using System.Runtime.InteropServices;

namespace BB.BaseUI.Settings.MozBar;

[StructLayout(LayoutKind.Sequential)]
public struct Dllversioninfo
{
	public int cbSize;
	public int dwMajorVersion;
	public int dwMinorVersion;
	public int dwBuildNumber;
	public int dwPlatformID;
}
	
/// <summary>
/// Summary description for ThemeManager.
/// </summary>
public class ThemeManager
{		
	// Declare functions used in uxTheme.dll and ComCtl32.dll

	[DllImport("uxTheme.dll", EntryPoint="GetThemeColor", ExactSpelling=true, PreserveSig=false, CharSet=CharSet.Unicode )]
	private extern static void GetThemeColor (IntPtr hTheme,
		int partId,
		int stateId,
		int propId,
		out int color);

	[DllImport( "uxtheme.dll", CharSet=CharSet.Unicode )]
	private static extern IntPtr OpenThemeData( IntPtr hwnd, string classes );
		
	[DllImport( "uxtheme.dll", EntryPoint="CloseThemeData", ExactSpelling=true, PreserveSig=false, CharSet=CharSet.Unicode) ]
	private static extern int CloseThemeData( IntPtr hwnd );

	[DllImport("uxtheme.dll", EntryPoint="GetWindowTheme", ExactSpelling=true,PreserveSig=false, CharSet=CharSet.Unicode)]
	private static extern int GetWindowTheme(IntPtr hWnd);
		
	[DllImport("uxtheme.dll", EntryPoint="IsThemeActive", ExactSpelling=true,PreserveSig=false, CharSet=CharSet.Unicode)]
	private static extern bool IsThemeActive();
		
	[DllImport("Comctl32.dll", EntryPoint="DllGetVersion", ExactSpelling=true,PreserveSig=false, CharSet=CharSet.Unicode)]
	private static extern int DllGetVersion(ref Dllversioninfo s);

	public ThemeManager()
	{
			  
	}

	public bool _IsAppThemed()
	{
		try
		{
			// Check which version of ComCtl32 thats in use..
			Dllversioninfo version = new Dllversioninfo
			{
				cbSize = Marshal.SizeOf(typeof(Dllversioninfo))
			};

			int ret = DllGetVersion(ref version);
			// If MajorVersion > 5 themes are allowed.
			if (version.dwMajorVersion >= 6) 			
				return true;
			else
				return false;
		}
		catch (Exception)
		{
			return false;
		}
	}

	public void _CloseThemeData(IntPtr hwnd)
	{
		try
		{
			CloseThemeData(hwnd);
		}
		catch (Exception)
		{
				
		}
	}
		
	public IntPtr _OpenThemeData(IntPtr hwnd, string classes)
	{
		try
		{
			return OpenThemeData(hwnd, classes );
		}
		catch (Exception)
		{
			return IntPtr.Zero;
		}
	}

	public int _GetWindowTheme(IntPtr hwnd)
	{
		try
		{
			return GetWindowTheme(hwnd);
		}
		catch (Exception)
		{
			return -1;
		}

	}

	public bool _IsThemeActive()
	{
		try
		{
			return IsThemeActive();
		}
		catch (Exception)
		{
			return false;
		}
	}

	public Color _GetThemeColor ( IntPtr hTheme, int partId, int stateId,int propId )
	{
		int color;

		try 
		{
			GetThemeColor ( hTheme, partId, stateId, propId, out color );
			return ColorTranslator.FromWin32 ( color );
		}
		catch (Exception) 
		{
			return Color.Empty;
		}
	}
}