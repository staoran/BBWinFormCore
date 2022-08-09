using System.ComponentModel;
using System.Runtime.InteropServices;

namespace BB.BaseUI.Settings.MozBar;

#region Delegates

public delegate void MozScrollEventHandler(object sender, MozScrollEventArgs e);
	
#endregion
	
[StructLayout(LayoutKind.Sequential)]
public struct Scrollinfo
{
	public int cbSize;
	public int fMask;
	public int nMin;
	public int nMax;
	public int nPage;
	public int nPos;
	public int nTrackPos;
}
		
/// <summary>
/// Adds the missing scroll events to the scrollable control!
/// Written by Martin Randall - Thursday 17th June, 2004
///
///
/// Modified by Patrik Bohman , May 2005
/// 
/// </summary>
[ToolboxItem(false)]
public class ScrollableControlWithScrollEvents : ScrollableControl
{
		
	#region Win32 API Constants

	private const int WsHscroll = 0x100000;

	private const int WmHscroll = 0x114;
	private const int WmVscroll = 0x115;
	private const int SbHorz = 0;
	private const int SbVert = 1;
	private const int SifRange =0x1;
	private const int SifPage = 0x2;
	private const int SifPos = 0x4;
	private const int SifDisablenoscroll = 0x8;
	private const int SifTrackpos = 0x10;
	private const int SifAll = SifRange | SifPage | SifPos | SifDisablenoscroll | SifTrackpos;
		
	#endregion

	#region Win32 API Functions

	[DllImport("User32", EntryPoint="GetScrollInfo")]
	private static extern bool GetScrollInfo (IntPtr hWnd, int fnBar, ref Scrollinfo info);
		
	#endregion
		
	#region Events

	[Browsable(true)]
	[Description("Indicates that the control has been scrolled horizontally.")]
	[Category("Panel")]
	public event MozScrollEventHandler HorizontalScroll;

		

	[Browsable(true)]
	[Description("Indicates that the control has been scrolled vertically.")]
	[Category("Panel")]
	public event MozScrollEventHandler VerticalScroll;
		
	#endregion

	#region Overrides

	protected override CreateParams CreateParams
	{
		get
		{
				
			CreateParams p = base.CreateParams;
			//p.Style= p.Style & ~WS_HSCROLL;
			return p; //base.CreateParams;
		}
	}

	/// <summary>
	/// Intercept scroll messages to send notifications
	/// </summary>
	/// <param name="m">Message parameters</param>
	protected override void WndProc(ref Message m)
	{
		// Let the control process the message
		base.WndProc (ref m);

		// Was this a horizontal scroll message?
		if ( m.Msg == WmHscroll ) 
		{
			if ( HorizontalScroll != null ) 
			{
				uint wParam = (uint)m.WParam.ToInt32();
				Scrollinfo si = new Scrollinfo();
				si.cbSize = Marshal.SizeOf(si);
				si.fMask = SifAll;
				bool ret = GetScrollInfo(Handle,SbHorz,ref si);
				HorizontalScroll( this, 
					new MozScrollEventArgs( 
						GetEventType( wParam & 0xffff), (int)(wParam >> 16),si ) );
			}
		} 
		// or a vertical scroll message?
		else if ( m.Msg == WmVscroll )
		{
				
			if ( VerticalScroll != null )
			{
				uint wParam = (uint)m.WParam.ToInt32();
				Scrollinfo si = new Scrollinfo();
				si.cbSize = Marshal.SizeOf(si);
				si.fMask = SifAll;
				bool ret = GetScrollInfo(Handle,SbVert,ref si);
				VerticalScroll( this, 
					new MozScrollEventArgs( 
						GetEventType( wParam & 0xffff), (int)(wParam >> 16),si ) );
							
			}
		}
	}

	#endregion

	#region Methods

	// Based on SB_* constants
	private static ScrollEventType [] _events =
		new[] {
			ScrollEventType.SmallDecrement,
			ScrollEventType.SmallIncrement,
			ScrollEventType.LargeDecrement,
			ScrollEventType.LargeIncrement,
			ScrollEventType.ThumbPosition,
			ScrollEventType.ThumbTrack,
			ScrollEventType.First,
			ScrollEventType.Last,
			ScrollEventType.EndScroll
		};
	/// <summary>
	/// Decode the type of scroll message
	/// </summary>
	/// <param name="wParam">Lower word of scroll notification</param>
	/// <returns></returns>
	private ScrollEventType GetEventType( uint wParam )
	{
		if ( wParam < _events.Length )
			return _events[wParam];
		else
			return ScrollEventType.EndScroll;
	}

	#endregion
		
}

#region MozScrollEventArgs
	
public class MozScrollEventArgs
{
	#region Class Data

	/// <summary>
	/// The color that has changed
	/// </summary>
	private ScrollEventType _mType;
	private int _mNewValue;
	private Scrollinfo _mInfo;

	#endregion

	#region Constructor

	/// <summary>
	/// Initializes a new instance of the MozItemEventArgs class with default settings
	/// </summary>
		
	public MozScrollEventArgs(ScrollEventType type , int newValue, Scrollinfo info)
	{
		_mType = type;
		_mNewValue = newValue; 
		_mInfo = info;
	}

	#endregion


	#region Properties

	public Scrollinfo ScrollInfo => _mInfo;

	public ScrollEventType Type => _mType;

	public int NewValue => _mNewValue;

	#endregion
}


#endregion