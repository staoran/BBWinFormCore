using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Runtime.InteropServices;
using System.Windows.Forms.Design;

namespace BB.BaseUI.Settings.MozBar;

#region Enumerations

public enum MozPaneStyle {Vertical = 0, Horizontal = 1 }	
public enum MozSelectButton {Any = 0, Left = 1, Middle = 2, Right = 3 }
	
public enum MozItemColor 
{
	Background = 0, Border = 1, Text = 2, FocusBackground = 3, FocusBorder = 4,
	SelectedBackground = 5, SelectedBorder = 6, Divider = 7,
	SelectedText = 8, FocusText = 9 }	
	
#endregion	

#region Delegates

public delegate void ItemColorChangedEventHandler(object sender, ItemColorChangedEventArgs e);
public delegate void ItemBorderStyleChangedEventHandler(object sender, ItemBorderStyleChangedEventArgs e);

#endregion

/// <summary>
/// Summary description for MozPane.
/// </summary>

[Designer(typeof(MozPaneDesigner))]
[ToolboxItem(true)]
[DefaultEvent("ItemClick")]
[ToolboxBitmap(typeof(MozPane),"BB.BaseUI.Settings.MozPane.bmp")]
public class MozPane : ScrollableControlWithScrollEvents , ISupportInitialize	 	 	
{	
		
	#region Win32 API functions
				
	[DllImport("user32.dll")]
	private static extern int ShowScrollBar(IntPtr hWnd, int wBar, int bShow);
		
	[DllImport("user32", CharSet=CharSet.Auto)]
	private static extern int GetWindowLong(IntPtr hwnd, int nIndex);
			
	#endregion

	#region Win32 API constants
	private const int WmThemechanged = 0x031A;
	private const int WmKeydown =  0x100;
		
	private const int SbHorz = 0;
	private const int SbVert = 1;
	private const int SbCtl  = 2;
	private const int SbBoth = 3;
		
	private const int WmNccalcsize = 0x83;
		
	private const int WsHscroll = 0x100000;
	private const int WsVscroll = 0x200000;
	private const int GwlStyle = (-16);
	#endregion

	#region EventHandlers
				
	[Browsable(true)]
	[Category("Property Changed")]
	[Description("Indicates that an item color has changed.")]
	public event ItemColorChangedEventHandler ItemColorChanged;
		
	[Browsable(true)]
	[Category("Property Changed")]
	[Description("Indicates that an item borderstyle has changed.")]
	public event ItemBorderStyleChangedEventHandler ItemBorderStyleChanged;
		
	[Browsable(true)]
	[Category("Property Changed")]
	[Description("Indicates that the border color was changed.")]
	public event EventHandler BorderColorChanged;
		
	[Browsable(true)]
	[Category("Property Changed")]
	[Description("Indicates that the Theme setting was changed.")]
	public event EventHandler UseThemeChanged;
		
	[Browsable(true)]
	[Category("Property Changed")]
	[Description("Indicates that the select button was changed.")]
	public event EventHandler SelectButtonChanged;

	[Browsable(true)]		
	[Category("Property Changed")]
	[Description("Indicates that the imagelist has has been changed.")]
	public event EventHandler ImageListChanged;

	[Browsable(true)]
	[Category("Property Changed")]
	[Description("Indicates that the style has been changed.")]
	public event EventHandler PaneStyleChanged;
		
	[Browsable(true)]
	[Category("Property Changed")]
	[Description("Indicates that the toggle property has been changed.")]
	public event EventHandler ToggleChanged;
		
	[Browsable(true)]
	[Category("Property Changed")]
	[Description("Indicates the max allowed number of selected items has changed.")]
	public event EventHandler MaxSelectedItemsChanged;
		
	[Browsable(true)]
	[Category("Property Changed")]
	[Description("Indicates that the padding has has been changed.")]
	public event EventHandler PaddingChanged;
		
	[Category("Panel")]
	[Description("Indicates that an item was added to the panel.")]
	[Browsable(true)]
	public event MozItemEventHandler ItemAdded; 
		
	[Category("Panel")]
	[Description("Indicates that an item was removed from the panel.")]
	[Browsable(true)]
	public event MozItemEventHandler ItemRemoved; 
				
	[Category("Action")]
	[Description("Indicates that an item was selected.")]
	[Browsable(true)]
	public event MozItemEventHandler ItemSelected;

	[Category("Action")]
	[Description("Indicates that an item was unselected.")]
	[Browsable(true)]
	public event MozItemEventHandler ItemDeselected;
		
	[Category("Action")]
	[Description("Indicates that an item lost focus.")]
	[Browsable(true)]
	public event MozItemEventHandler ItemLostFocus;
		
	[Category("Action")]
	[Description("Indicates that an item recieved focus.")]
	[Browsable(true)]
	public event MozItemEventHandler ItemGotFocus;
		
	[Category("Action")]
	[Description("Indicates that an item has been double clicked.")]
	[Browsable(true)]
	public event MozItemClickEventHandler ItemDoubleClick;
		
	[Category("Action")]
	[Description("Indicates that an item has been clicked.")]
	[Browsable(true)]
	public event MozItemClickEventHandler ItemClick;

	#endregion

	#region private class members

	private Container _components = null;
		
			
	private bool _layout;
	private int _beginUpdateCount;
	internal bool deserializing;
	internal bool initialising;
	
	private int _mTabIndex = -1;
	private MozItem _mMouseOverItem = null;
	
	private Color _mBorderColor;
	private PaddingCollection _mPadding;
	private ColorCollection _mColorCollection;
	private BorderStyleCollection _mBorderStyleCollection;
	private MozSelectButton _mSelectButton;
	private ThemeManager _mThemeManager;
	
	private MozPaneStyle _mStyle;
	private IntPtr _mTheme;
	private bool _mUseTheme;
	private bool _mToggle;
	private int _mMaxSelectedItems;
	private int _mSelectedItems;

	private ButtonBorderStyle _mBorderStyle;
	private ImageList _mImageList = null;
		
	private MozItemCollection _mMozItemCollection;
		
	#endregion
		
	#region Constructor

	public MozPane()
	{
		// This call is required by the Windows.Forms Form Designer.
		InitializeComponent();
			
		// TODO: remove the following lines after you know the resource names
				
		// This call is required by the Windows.Forms Form Designer.
		_components = new Container();

		SetStyle(ControlStyles.DoubleBuffer, true);
		SetStyle(ControlStyles.AllPaintingInWmPaint, true);
		SetStyle(ControlStyles.UserPaint, true);
		SetStyle(ControlStyles.ResizeRedraw, true);
		SetStyle(ControlStyles.SupportsTransparentBackColor, true);
		SetStyle(ControlStyles.ContainerControl,true);  
	
		_mMozItemCollection = new  MozItemCollection(this);
			
		// Enable Autoscroll
		AutoScroll = true;
	
		_mPadding = new PaddingCollection(this);
		_mColorCollection = new ColorCollection(this);
		_mBorderStyleCollection = new BorderStyleCollection(this);
		_mThemeManager = new ThemeManager(); 
		_mSelectButton = MozSelectButton.Left; 
		_mStyle = MozPaneStyle.Vertical;
		_mToggle = false;
		_mMaxSelectedItems = 1;
		_mSelectedItems = 0;
			
		_mUseTheme = false;
		_mTheme = IntPtr.Zero; 
  	    										
		// Listen for changes to the parent
		ParentChanged += OnParentChanged;
			

		_beginUpdateCount = 0;

		deserializing = false;
		initialising = false;
			
		_mBorderColor = Color.FromArgb(127,157,185);
		BackColor = Color.White;
		_mBorderStyle = ButtonBorderStyle.Solid; 
	}

	#endregion

	#region Dispose

	/// <summary>
	/// Clean up any resources being used.
	/// </summary>
	protected override void Dispose( bool disposing )
	{
		if( disposing )
		{
			if( _components != null )
				_components.Dispose();

			_mColorCollection.Dispose();
			_mBorderStyleCollection.Dispose();
		}
		base.Dispose( disposing );
	}

	#endregion


	#region properties
		
	[DefaultValue(typeof(Color),"White")]
	public override Color BackColor
	{
		get => base.BackColor;
		set => base.BackColor = value;
	}

	[Category("Behavior")]
	[Description("Mouse button used to select items.")]	
	[DefaultValue(typeof(MozSelectButton),"Left")]
	public MozSelectButton SelectButton
	{
		get => _mSelectButton;
		set
		{
			if (_mSelectButton!=value)
			{
				_mSelectButton = value;
				if (SelectButtonChanged !=null)
					SelectButtonChanged(this,EventArgs.Empty); 
			}
		}
	}

	[Category("Behavior")]
	[Description("Indicates wether the control should use the current theme.")]	
	[DefaultValue(false)]
	public bool Theme
	{
		get => _mUseTheme;
		set
		{
			if (_mUseTheme!=value)
			{
				_mUseTheme = value;
				if (UseThemeChanged !=null)
					UseThemeChanged(this,EventArgs.Empty); 
				if (_mUseTheme)
					GetThemeColors();
					
			}
		}
	}

	[Browsable(true)]
	[Category("Behavior")]
	[RefreshProperties(RefreshProperties.All)]
	[Description("Indicates the possibility to toggle items i.e. unselect selected items.")]
	[DefaultValue(false)]
	public bool Toggle
	{
		get => _mToggle;
		set
		{
			// Check if new value differs from old one
			if (value!=_mToggle)
			{
				_mToggle = value;
				if (value == false) MaxSelectedItems = 1;
				if (ToggleChanged!=null) ToggleChanged(this,EventArgs.Empty);
			}
		}
	}

	[Browsable(true)]
	[Category("Behavior")]
	[Description("Max number of selected items.")]
	[DefaultValue(1)]
	public int MaxSelectedItems
	{
		get => _mMaxSelectedItems;
		set
		{
			if (value!=_mMaxSelectedItems)
			{
				if (value<1) value = 1;
				if (value>Items.Count) value = Items.Count;
					
				_mMaxSelectedItems = value;
				if (MaxSelectedItemsChanged!=null) MaxSelectedItemsChanged(this,EventArgs.Empty);
			}
		}
	}

	// Returns number of selected item in the panel
	[Browsable(false)]	
	public int SelectedItems => _mSelectedItems;

	[Browsable(true)]
	[Category("Appearance")]
	[Description("Colors for various states.")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	public ColorCollection ItemColors
	{
		get => _mColorCollection;
		set
		{
			if (value != null)
				_mColorCollection = value;
		}
	}

	[Browsable(true)]
	[Category("Appearance")]
	[Description("Various border styles.")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	public BorderStyleCollection ItemBorderStyles
	{
		get => _mBorderStyleCollection;
		set
		{
			if ((value != null) && (value !=_mBorderStyleCollection))
				_mBorderStyleCollection = value;
		}
	}

	[Browsable(true)]
	[Category("Appearance")]
	[Description("Padding (Horizontal, Vertical)")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	public PaddingCollection Padding
	{
		get => _mPadding;
		set
		{
			if (value!=_mPadding)
			{
				if (value != null)	_mPadding = value;
				DoLayout();
				Invalidate();
				if (PaddingChanged!=null) PaddingChanged(this,EventArgs.Empty);
			}
		}
	}


	[Browsable(true)]
	[Category("Appearance")]
	[DefaultValue(typeof(MozPaneStyle),"Vertical")]
	[Description("Pane style.")]
	public MozPaneStyle Style
	{
		get => _mStyle;
		set
		{
			if (_mStyle !=value)
			{
				_mStyle = value;

				// for each control in our list...
				for (int i=0; i<Items.Count; i++)
				{
					// force layout change
					Items[i].DoLayout();
					Invalidate();
						 
				}
				DoLayout();
				Invalidate();
				if (PaneStyleChanged!=null) PaneStyleChanged(this,EventArgs.Empty);
			}
		}
	}

	[Browsable(true)]
	[Category("Behavior")]
	[Description("The imagelist that contains images used by the MozBar.")]
	public ImageList ImageList
	{
		get => _mImageList;
		set
		{
			if (_mImageList != value)
			{
				_mImageList = value;
				// for each control in our list...
				for (int i=0; i<Items.Count; i++)
				{
					// reset all images
					Items[i].Images.NormalImage = null; 
					Items[i].Images.FocusImage = null;
					Items[i].Images.SelectedImage = null;
					//redo layout (size might have changed..)
					Items[i].DoLayout();
					Invalidate(); 
				}
					
				if (ImageListChanged!=null) ImageListChanged(this,EventArgs.Empty);
				DoLayout(); 
				Invalidate();
			}
		}
	}
		
	[Browsable(true)]
	[Category("Appearance")]
	[DefaultValue(typeof(ButtonBorderStyle),"Solid")]
	[Description("Border color for panel.")]
	public ButtonBorderStyle BorderStyle
	{
		get => _mBorderStyle;
		set
		{
			if (_mBorderStyle!=value)
			{
				_mBorderStyle = value;
				Invalidate();
			}
		}
	}
		
	[Browsable(true)]
	[Category("Appearance")]
	[DefaultValue(typeof(Color),"127,157,185")]
	[Description("Border color for panel.")]
	public Color BorderColor
	{
		get => _mBorderColor;
		set
		{
			if (value!=_mBorderColor)
			{
				_mBorderColor = value;
				if (BorderColorChanged!=null) BorderColorChanged(this,EventArgs.Empty);
				Invalidate();
			}
		}

	}
		
	[Category("Behavior")]
	[DefaultValue(null)]
	[Description("The Items contained in the Panel."),]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)] 
	[Editor(typeof(MozItemCollectionEditor), typeof(UITypeEditor))]
	public MozItemCollection Items => _mMozItemCollection;

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public new ControlCollection Controls => base.Controls;

	// obsolete properties

	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Obsolete("This property is not supported",true)]
	public override Image BackgroundImage
	{
		get => base.BackgroundImage;
		set => base.BackgroundImage = value;
	}
		
		
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Obsolete("This property is not supported",true)]
	public override RightToLeft RightToLeft
	{
		get => base.RightToLeft;
		set => base.RightToLeft = value;
	}
		
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Obsolete("This property is not supported",true)]
	public override string Text
	{
		get => base.Text;
		set => base.Text = value;
	}
		
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Obsolete("This property is not supported",true)]
	public override Color ForeColor
	{
		get => base.ForeColor;
		set => base.ForeColor = value;
	}

	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	// [Obsolete("This property is not supported",true)]
	public override bool AutoScroll
	{
		get => base.AutoScroll;
		set => base.AutoScroll = true;
	}
				
	#endregion
		
	#region Events
		
		

	protected override void OnGotFocus(EventArgs e)
	{
		base.OnGotFocus (e);
		if (_mTabIndex==-1)
			_mTabIndex = 0;
		RemoveFocus();
		if (Items.Count>=1)
			if (Items[_mTabIndex].State!=MozItemState.Selected)
			{
				Items[_mTabIndex].State = MozItemState.Focus;  
				ScrollControlIntoView(Items[_mTabIndex]);
			}
	}
		
		

	protected override void OnLostFocus(EventArgs e)
	{
		base.OnLostFocus (e);
		RemoveFocus();
		_mTabIndex = -1;
	}
		
	//protected override onp
				
	protected override void OnKeyDown(KeyEventArgs e)
	{
		base.OnKeyDown (e);
		switch (e.KeyCode)
		{
				 
			case Keys.Enter:  // Enter
			case Keys.Space: 
			{
				if (_mTabIndex!=-1)
					SelectItem(_mTabIndex);   
				break;
			}
			case Keys.Down:
			case Keys.Right:
			case Keys.Tab: 
			{
				// Move tabindex one step forward.
				_mTabIndex++;
				if ((_mTabIndex < Items.Count) && (_mTabIndex >0))
				{
					RemoveFocus();
					if (Items[_mTabIndex].State!=MozItemState.Selected)
						Items[_mTabIndex].State = MozItemState.Focus;
					ScrollControlIntoView(Items[_mTabIndex]);
				}
				else SelectNextControl(this,true,true,true,true); 
						
				break;
			}
			case Keys.Up:
			case Keys.Left:
			{
				// Move tabindex one step backward
				_mTabIndex--;
				if ((_mTabIndex >= 0) && (_mTabIndex <Items.Count))
				{
					RemoveFocus();
					if (Items[_mTabIndex].State!=MozItemState.Selected)
						Items[_mTabIndex].State = MozItemState.Focus;
					ScrollControlIntoView(Items[_mTabIndex]);
				}
				else SelectNextControl(this,false,true,true,true);
					
				break;
			}

		}
	}

	public override bool PreProcessMessage(ref Message msg)
	{
			
		// Check if message is KEY_DOWN
		if (msg.Msg == WmKeydown)
		{
			Keys keyData = ((Keys) (int) msg.WParam) |ModifierKeys;
			Keys keyCode = ((Keys) (int) msg.WParam);
			// Make sure we handle certain keys
			switch(keyCode)
			{
				// Keys used to move forward i list
				case Keys.Down:
				case Keys.Right:
				case Keys.Tab: 
				{
					// If not at the end handle message
					if (_mTabIndex < Items.Count-1)
						return false;
					//Cant go any further backwards , do not handle message;
					_mTabIndex = -1;
					break;
				}
				// Keys used to move backwards in list
				case Keys.Up:
				case Keys.Left:
				{
					// If not at the end handle message
					if (_mTabIndex >0)
						return false;
					//Cant go any further foreward , do not handle message;
					_mTabIndex = -1;
					break;
				}
				default:
					break;
			}    
		}
			
		return base.PreProcessMessage (ref msg);
	}
		
	protected override void OnSystemColorsChanged(EventArgs e)
	{
		base.OnSystemColorsChanged (e);
		if (Theme)
			GetThemeColors();
	}


	protected override void WndProc(ref Message m)
	{
		base.WndProc (ref m);
		switch (m.Msg)
		{
			case WmThemechanged:
			{
				// Theme has changed , get new colors if Theme = true
				if (Theme)
					GetThemeColors();
				break;
			}
			case WmNccalcsize:
			{
					
				if (Style == MozPaneStyle.Vertical)
				{
					// hide horizontal scrollbar
					ShowScrollBar(m.HWnd, SbHorz, 0);
				}
				else
				{
					// hide vertical scrollbar
					ShowScrollBar(m.HWnd, SbVert, 0);
				}
					
				break;
			}
		}
		
	}
			

	protected override void OnSizeChanged(EventArgs e)
	{
		base.OnSizeChanged (e);
		AutoScroll = false; 
		AutoScroll = true;
	}


	protected override void OnPaint(PaintEventArgs e)
	{
				
		ButtonBorderStyle leftStyle = _mBorderStyle;
		ButtonBorderStyle topStyle =  _mBorderStyle;
		ButtonBorderStyle rightStyle = _mBorderStyle;
		ButtonBorderStyle bottomStyle = _mBorderStyle;
	
		base.OnPaint(e); 
			
		Rectangle borderRect = new Rectangle();

		borderRect = DisplayRectangle;
		Brush bgBrush = new SolidBrush(BackColor);
			 
		// Draw background and border
						
		e.Graphics.FillRectangle(bgBrush,DisplayRectangle); 
		if (Style == MozPaneStyle.Vertical)
		{
												
			if (IsVerticalScrollBarVisible())
			{
				topStyle = ButtonBorderStyle.None;
				bottomStyle = ButtonBorderStyle.None;
									
			}	
		}
		else
		{
			if (IsHorizontalScrollBarVisible())
			{
				leftStyle = ButtonBorderStyle.None;
				rightStyle = ButtonBorderStyle.None;
			}
		}
		ControlPaint.DrawBorder(e.Graphics,borderRect , 
			_mBorderColor,1,leftStyle, // left
			_mBorderColor,1,topStyle,  // top
			_mBorderColor,1,rightStyle,	// right
			_mBorderColor,1,bottomStyle); //bottom
						
		// clean up
		bgBrush.Dispose();
	}

	private void OnParentChanged(object sender, EventArgs e)
	{
		if (Parent != null)
		{
			Parent.VisibleChanged += OnParentVisibleChanged;
		}
	}

	private void OnParentVisibleChanged(object sender, EventArgs e)
	{
		if (sender != Parent)
		{
			((System.Windows.Forms.Control) sender).VisibleChanged -= OnParentVisibleChanged;
				
			return;
		}

		if (Parent.Visible)
		{
			DoLayout();
		}
	}
		

	protected override void OnResize(EventArgs e)
	{
		// size has changed , force layout change
				
		DoLayout();
		Invalidate(true);
			
	}

					
	private void MozItem_GotFocus(object sender, MozItemEventArgs e)
	{
		//Check if item is selected
		if (e.MozItem.State != MozItemState.Selected)
		{
			// if not set its state to focus
			e.MozItem.State = MozItemState.Focus;
			_mMouseOverItem = e.MozItem;
			if (ItemGotFocus != null) ItemGotFocus(this,e);
		}
	}

	private void MozItem_LostFocus(object sender, MozItemEventArgs e)
	{
		// check if item is selected
		if (e.MozItem.State != MozItemState.Selected)
		{
			// if not set its state to normal
			e.MozItem.State = MozItemState.Normal;
			_mMouseOverItem = null;
			if (ItemLostFocus != null) ItemLostFocus(this,e);
		}
	}

	private void MozItem_Click(object sender, MozItemClickEventArgs e)
	{
			
		// Fire click event and then ...
		if (ItemClick != null) ItemClick(this,e);
		// Try to select item if the proper button was used.
		if ((e.Button.ToString() == SelectButton.ToString()) ||
		    (SelectButton == MozSelectButton.Any))
		{
			_mTabIndex = Items.IndexOf(e.MozItem); 
			Focus(); 
			SelectItem(Items.IndexOf(e.MozItem));  
		}	
	}

	private void MozItem_DoubleClick(object sender, MozItemClickEventArgs e)
	{
		if (ItemDoubleClick != null) ItemDoubleClick(this,e);
	}
		
	protected override void OnControlAdded(ControlEventArgs e)
	{
		// check if control is a MozItem	
		if ((e.Control as MozItem) == null)
		{
			// If not remove and...
			Controls.Remove(e.Control);
			// throw exception
			throw new InvalidCastException("Only MozItem's can be added to the MozPane");
		}
			
		base.OnControlAdded(e);

		// Check if item exists in collection
		if (!Items.Contains((MozItem) e.Control))
		{
			// if not add it
			Items.Add((MozItem) e.Control);
		}

		// Refresh
		Invalidate();
	}

	protected override void OnControlRemoved(ControlEventArgs e)
	{
		base.OnControlRemoved (e);
			
		// Check if item is in collection
		if (Items.Contains(e.Control))
		{
			// If it is , remove it
			Items.Remove((MozItem) e.Control);
		}
			
		// Refresh
		Invalidate();
			
	}

	protected virtual void OnMozItemAdded(MozItemEventArgs e)
	{
		if (!Controls.Contains(e.MozItem))
		{
			Controls.Add(e.MozItem);
		}
									 
		// tell the MozItem who's its daddy...
		e.MozItem.MozPane = this;
				
		// listen for events
		e.MozItem.ItemGotFocus +=MozItem_GotFocus;    
		e.MozItem.ItemLostFocus +=MozItem_LostFocus;  
		e.MozItem.ItemClick +=MozItem_Click;
		e.MozItem.ItemDoubleClick += MozItem_DoubleClick; 

		// update the layout of the controls
			
		DoLayout();
						
		if (ItemAdded != null)
		{
			ItemAdded(this, e);
		}
	}


	protected virtual void OnMozItemRemoved(MozItemEventArgs e)
	{
		if (Controls.Contains(e.MozItem))
		{
			Controls.Remove(e.MozItem);
		}

		// remove event listeners
		e.MozItem.ItemGotFocus -=MozItem_GotFocus; 
		e.MozItem.ItemLostFocus -= MozItem_LostFocus; 
		e.MozItem.ItemClick -=MozItem_Click;
		e.MozItem.ItemDoubleClick -= MozItem_DoubleClick; 


		// update the layout of the controls
		DoLayout();

		if (ItemRemoved != null)
		{
			ItemRemoved(this, e);
		}
	}

	#endregion
		
	#region Methods
		
	#region ISupportInitialize Members

	/// <summary>
	/// Signals the MozPane that initialization is starting
	/// </summary>
	public void BeginInit()
	{
		initialising = true;
	}


	/// <summary>
	/// Signals the MozPane that initialization is complete
	/// </summary>
	public void EndInit()
	{
		initialising = false;

		//this.DoLayout();
	}
	#endregion

	#region Layout

	public void DoLayout()
	{
		DoLayout(false);
	}

	public void DoLayout(bool performRealLayout)
	{
		if (_layout || _beginUpdateCount > 0 || deserializing)
		{
			return;
		}

		_layout = true;
			
		SuspendLayout();
			
		MozItem e;
		Point p;
		
		switch (_mStyle)
		{
			case MozPaneStyle.Vertical:  // Vertical
			{
				// work out how wide to make the controls, and where
				// the top of the first control should be
				int y = DisplayRectangle.Y + _mPadding.Vertical;
				int width = ClientRectangle.Width - (2*_mPadding.Horizontal);
				// for each control in our list...
				for (int i=0; i<Items.Count; i++)
				{
					e = Items[i];
					// go to the next mozitem if this one is invisible and 
					// it's parent is visible
					if (!e.Visible && e.Parent != null && e.Parent.Visible)
					{
						continue;
					}
					p = new Point(_mPadding.Horizontal, y);
					// set the width and location of the control
					e.Location = p;
					e.Width = Width;
					// update the next starting point
					y += e.Height + _mPadding.Vertical;
				}
				break;
			}
			case MozPaneStyle.Horizontal:  // Horizontal
			{
				int x = DisplayRectangle.X + _mPadding.Horizontal;
				int height = ClientRectangle.Height - (2*_mPadding.Vertical);
				for (int i=0; i<Items.Count; i++)
				{
					e = Items[i];
					if (!e.Visible && e.Parent != null && e.Parent.Visible)
					{
						continue;
					}
					p = new Point(x,_mPadding.Vertical);
					e.Location = p;
					e.Height = height;
					x += e.Width + _mPadding.Horizontal;
				}
				break;
			}
		}
			
		// restart the layout engine
		ResumeLayout(performRealLayout);

		_layout = false;
	}

	internal bool IsVerticalScrollBarVisible()
	{
		return (GetWindowLong(Handle, GwlStyle) & WsVscroll) != 0;
	}

	internal bool IsHorizontalScrollBarVisible()
	{
		return (GetWindowLong(Handle, GwlStyle) & WsHscroll) != 0;
	}
		
	internal void UpdateMozItems()
	{
		if (Items.Count == Controls.Count)
		{
			MatchControlCollToMozItemsColl();				
				
			return;
		}

		if (Items.Count > Controls.Count)
		{
			for (int i=0; i<Items.Count; i++)
			{
				if (!Controls.Contains(Items[i]))
				{
					OnMozItemAdded(new MozItemEventArgs(Items[i]));
				}
			}
		}
		else
		{
			int i = 0;
			MozItem mozItem;

			while (i < Controls.Count)
			{
				mozItem = (MozItem) Controls[i];
					
				if (!Items.Contains(mozItem))
				{
					OnMozItemRemoved(new MozItemEventArgs(mozItem));
				}
				else
				{
					i++;
				}
			}
		}
	}
	
	internal void MatchControlCollToMozItemsColl()
	{
		SuspendLayout();
				
		for (int i=0; i<Items.Count; i++)
		{
			Controls.SetChildIndex(Items[i], i);
		}

		ResumeLayout(false);
				
		DoLayout(true);

		Invalidate(true);
	}

	#endregion
		
	private void RemoveFocus()
	{
		for (int i = 0;i<Items.Count;i++)
		{
			if ((Items[i].State == MozItemState.Focus) && (Items[i] != _mMouseOverItem))
			{
				Items[i].State = MozItemState.Normal; 
			}
		}
	}
	
	private void GetThemeColors()
	{
		int epbHeaderbackground = 1;
		int epbNormalgroupbackground = 5;
			
		int tmtGradientcolor1 = 3810;
		int tmtGradientcolor2 = 3811;

		Color selectColor = new Color(); 
		Color focusColor = new Color();
		Color borderColor = new Color();
		bool useSystemColors = false;

		// Check if themes are available
		if (_mThemeManager._IsAppThemed())
		{
			if (_mTheme!=IntPtr.Zero)
				_mThemeManager._CloseThemeData(_mTheme); 
								 
			// Open themes for "ExplorerBar"
			_mTheme = _mThemeManager._OpenThemeData(Handle,"EXPLORERBAR");  
			if (_mTheme!=IntPtr.Zero)
			{							
						
				// Get Theme colors..
				selectColor = _mThemeManager._GetThemeColor(_mTheme,epbHeaderbackground,1,tmtGradientcolor2); 		
				focusColor = _mThemeManager._GetThemeColor(_mTheme,epbNormalgroupbackground,1,tmtGradientcolor1); 		
					
				borderColor = ControlPaint.Light(selectColor);	
				selectColor = ControlPaint.LightLight(selectColor);
				focusColor = ControlPaint.LightLight(selectColor);
			}
			else
			{
				useSystemColors = true;
			}
		}
		else
		{
			useSystemColors = true;
		}

		if (useSystemColors)
		{
			// Get System colors
			selectColor = SystemColors.ActiveCaption;  		
			focusColor = ControlPaint.Light(selectColor); 		
			borderColor = SystemColors.ActiveBorder;
		}

		// apply colors..
		ItemColors.SelectedBorder = ControlPaint.Light(ControlPaint.Dark(selectColor));
		BorderColor = borderColor;
		ItemColors.Divider = borderColor;
													 
		ItemColors.SelectedBackground = selectColor;
		ItemColors.FocusBackground = focusColor;
		ItemColors.FocusBorder = selectColor;
			
		Invalidate();

	}

	public void SelectItem(int index)
	{
		// Check if index is valid
		if (index >=0 && index <=Items.Count-1)
		{
			// Check if item is selected
			if (Items[index].State != MozItemState.Selected) 
			{
				// Is it a divider ?
				if (Items[index].ItemStyle!=MozItemStyle.Divider)
				{
					// Check if toggle is enabled
					if (!Toggle)
					{
						// for each control in our list...
						for (int i=0; i<Items.Count; i++)
						{
							// set all items to not selected
							if ((Items[i]!=_mMouseOverItem) || (Items[i].State == MozItemState.Selected)) 
								Items[i].State = MozItemState.Normal;
						}
						// No item is selected
						_mSelectedItems=0;
					}
					// Check that the allowed number of selected items isnt exceeded
					if (_mMaxSelectedItems >= _mSelectedItems + 1)
					{
						// set our item to selected
						Items[index].State = MozItemState.Selected;
						_mSelectedItems++;
						// Scroll selected item into view
						ScrollControlIntoView(Items[index]);
						if (ItemSelected != null) ItemSelected(this,new MozItemEventArgs(Items[index])); 
					}
				}
			}
			else
			{
				if (Toggle)
				{
					//unselect selected item by setting its state to Focus
					Items[index].State = MozItemState.Focus;
					_mSelectedItems--;
					if (ItemDeselected != null) ItemDeselected(this,new MozItemEventArgs(Items[index])); 
				}
			}
		}
	}
		
	public void SelectItem(string tag)
	{
		// loop through item collection
		for (int i=0; i<Items.Count; i++)
		{
			// if matching tag is found try to select item
			if (Items[i].Tag.ToString()  == tag)
				SelectItem(i);
		}
			
	}

	public void SelectByName(string name)
	{
		for (int i = 0; i < Items.Count; i++)
		{
			if (Items[i].Name == name)
				SelectItem(i);
		}
	}

	public void SelectByText(string text)
	{
		for (int i = 0; i < Items.Count; i++)
		{
			if (Items[i].Text == text)
				SelectItem(i);
		}
	}

	#endregion

	#region Component Designer generated code
	/// <summary>
	/// Required method for Designer support - do not modify 
	/// the contents of this method with the code editor.
	/// </summary>
	private void InitializeComponent()
	{
		_components = new System.ComponentModel.Container();
	}
		
	#endregion
		
	#region MozItemsCollection

	/// <summary>
	/// Represents a collection of MozItem objects
	/// </summary>
	public class MozItemCollection : CollectionBase 
	{
		#region Class Data

		/// <summary>
		/// The MozPane that owns this MozItemsCollection
		/// </summary>
		private MozPane _owner;

		#endregion

		#region Constructor

		public MozItemCollection(MozPane owner) : base()
		{
			if (owner == null)
			{
				throw new ArgumentNullException("owner");
			}
				
			_owner = owner;
		}
			
		public MozItemCollection(MozPane owner, MozItemCollection mozItems) : this(owner)
		{
			Add(mozItems);
		}

		#endregion

		#region Methods
			
		public void Add(MozItem value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			List.Add(value);

			if (_owner != null && !_owner.deserializing)
			{
				_owner.Controls.Add(value);

				_owner.OnMozItemAdded(new MozItemEventArgs(value));
			}
		}

		public void AddRange(MozItem[] mozItems)
		{
			if (mozItems == null)
			{
				throw new ArgumentNullException("mozItems");
			}

			for (int i=0; i<mozItems.Length; i++)
			{
				Add(mozItems[i]);
			}
		}

		public void Add(MozItemCollection mozItems)
		{
			if (mozItems == null)
			{
				throw new ArgumentNullException("mozItems");
			}

			for (int i=0; i<mozItems.Count; i++)
			{
				Add(mozItems[i]);
			}
		}
			
			
		public new void Clear()
		{
			while (Count > 0)
			{
				RemoveAt(0);
			}
		}


		public bool Contains(MozItem mozItem)
		{
			if (mozItem == null)
			{
				throw new ArgumentNullException("mozItem");
			}

			return (IndexOf(mozItem) != -1);
		}

		public bool Contains(System.Windows.Forms.Control control)
		{
			if (!(control is MozItem))
			{
				return false;
			}

			return Contains((MozItem) control);
		}

		public int IndexOf(MozItem mozItem)
		{
			if (mozItem == null)
			{
				throw new ArgumentNullException("mozItem");
			}
				
			for (int i=0; i<Count; i++)
			{
				if (this[i] == mozItem)
				{
					return i;
				}
			}

			return -1;
		}
			
		public void Remove(MozItem value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			List.Remove(value);

			if (_owner != null && !_owner.deserializing)
			{
				_owner.Controls.Remove(value);

				_owner.OnMozItemRemoved(new MozItemEventArgs(value));
			}
		}
			
		public new void RemoveAt(int index)
		{
			Remove(this[index]);
		}


		public void Move(MozItem value, int index)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			if (index < 0)
			{
				index = 0;
			}
			else if (index > Count)
			{
				index = Count;
			}

			if (!Contains(value) || IndexOf(value) == index)
			{
				return;
			}

			List.Remove(value);

			if (index > Count)
			{
				List.Add(value);
			}
			else
			{
				List.Insert(index, value);
			}

			if (_owner != null && !_owner.deserializing)
			{
				_owner.MatchControlCollToMozItemsColl();
			}
		}

		public void MoveToTop(MozItem value)
		{
			Move(value, 0);
		}


		public void MoveToBottom(MozItem value)
		{
			Move(value, Count);
		}

		#endregion

		#region Properties

		public virtual MozItem this[int index] => List[index] as MozItem;

		#endregion
	}

	#endregion

	#region MozItemCollectionEditor
	/// <summary>
	/// A custom CollectionEditor for editing MozItemCollections
	/// </summary>
	internal class MozItemCollectionEditor : CollectionEditor
	{
		public MozItemCollectionEditor(Type type) : base(type)
		{
			
		}
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider isp, object value)
		{
			MozPane originalControl = (MozPane) context.Instance;

			object returnObject = base.EditValue(context, isp, value);

			originalControl.UpdateMozItems();

			return returnObject;
		}
			

		protected override object CreateInstance(Type itemType)
		{
			object mozItem = base.CreateInstance(itemType);
			
			((MozItem) mozItem).Name = "MozItem";
			
			return mozItem;
		}
	}

	#endregion

	#region  PaddingCollection

	[TypeConverter(typeof(PaddingCollectionTypeConverter))]		
	public class PaddingCollection
	{
		private MozPane _mPane;
		private int _mHorizontal;
		private int _mVertical;
			
		public PaddingCollection(MozPane pane)
		{
			// set the control to which the collection belong
			_mPane = pane;
			// Default values
			_mHorizontal = 2;
			_mVertical = 2;
		}
			
		[RefreshProperties(RefreshProperties.All)]
		[Description("Horizontal padding.")]
		[DefaultValue(2)]
		public int Horizontal
		{
			get => _mHorizontal;
			set
			{
				if (_mHorizontal!=value)
				{
					_mHorizontal = value;
					if (_mPane!=null)
					{
						// padding has changed , force DoLayout
						_mPane.DoLayout();
						_mPane.Invalidate();
						if (_mPane.PaddingChanged!=null) _mPane.PaddingChanged(this,EventArgs.Empty);
					}
				}
			}
		}
			
		[RefreshProperties(RefreshProperties.All)]
		[Description("Vertical padding.")]
		[DefaultValue(2)]
		public int Vertical
		{
			get => _mVertical;
			set
			{
				if (_mVertical!=value)
				{
					_mVertical = value;
					if (_mPane!=null)
					{						
						_mPane.DoLayout();
						_mPane.Invalidate();
						if (_mPane.PaddingChanged!=null) _mPane.PaddingChanged(this,EventArgs.Empty);
					}
				}
			}
		}

	}
		
	#endregion
		
	#region PaddingCollectionTypeConverter

	public class PaddingCollectionTypeConverter : ExpandableObjectConverter
	{
			        	
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			if(sourceType == typeof(string))
				return true;
			return base.CanConvertFrom (context, sourceType);
		}

		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			if(destinationType == typeof(string))
				return true;
			return base.CanConvertTo (context, destinationType);
		}

		public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
		{
				
			if(value.GetType() == typeof(string))
			{
				// Parse property string
				string[] ss = value.ToString().Split(new[] {';'}, 2);
				if (ss.Length==2)
				{
					// Create new PaddingCollection
					PaddingCollection item = new PaddingCollection((MozPane)context.Instance)
					{
						// Set properties
						Horizontal = int.Parse(ss[0]),
						Vertical = int.Parse(ss[1])
					};
					return item;				
				}
			}
			return base.ConvertFrom (context, culture, value);
		}

		public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
		{
								
			if(destinationType == typeof(string) && (value is PaddingCollection) )
			{
				// cast value to paddingCollection
				PaddingCollection dest = (PaddingCollection)value;  
				// create property string
				return dest.Horizontal+"; "+dest.Vertical;
			}
			return base.ConvertTo (context, culture, value, destinationType);
		}

	}


	#endregion
		
	#region  ColorCollection
		

	[TypeConverter(typeof(ItemCollectionTypeConverter))]
	public class ColorCollection
	{
		private Color _mSelected;
		private Color _mSelectedBorder;
		private Color _mFocus;
		private Color _mFocusBorder;
		private Color _mText;
		private Color _mBack;
		private Color _mBorder;
		private Color _mDivider;
		private Color _mSelectedText;
		private Color _mFocusText;
				
		public MozPane MPane;
			
		public ColorCollection(MozPane pane)
		{
			MPane = pane;
			// Default values
			_mSelected = Color.FromArgb(193,210,238);
			_mSelectedBorder = Color.FromArgb(49,106,197); 
			_mFocus = Color.FromArgb(224,232,246); 
			_mFocusBorder = Color.FromArgb(152,180,226); 
			_mBack = Color.White;
			_mBorder = Color.Black;
			_mText = Color.Black;
			_mSelectedText = Color.Black;
			_mFocusText = Color.Black;
			_mDivider = Color.FromArgb(127,157,185);
		}
			
		public void Dispose()
		{
				
		}

		private void UpdateItems()
		{
			// for each item contained in the panel
			for (int i=0; i<MPane.Items.Count; i++)
			{
				// Refresh item
				MPane.Items[i].Invalidate();
			}
				
		}

		[Description("Color used for item text.")]
		[DefaultValue(typeof(Color),"Black")]
		public Color Text
		{
			get => _mText;
			set
			{
				if (_mText!=value)
				{
					_mText = value;
					UpdateItems();
					if (MPane.ItemColorChanged!=null) MPane.ItemColorChanged(this,new ItemColorChangedEventArgs(MozItemColor.Text));
				}
			}
		}

		[Description("Text color when item is selected.")]
		[DefaultValue(typeof(Color),"Black")]
		public Color SelectedText
		{
			get => _mSelectedText;
			set
			{
				if (_mSelectedText!=value)
				{
					_mSelectedText = value;
					UpdateItems();
					if (MPane.ItemColorChanged!=null) MPane.ItemColorChanged(this,new ItemColorChangedEventArgs(MozItemColor.SelectedText));
				}
			}
		}

		[Description("Text color when item has focus.")]
		[DefaultValue(typeof(Color),"Black")]
		public Color FocusText
		{
			get => _mFocusText;
			set
			{
				if (_mFocusText!=value)
				{
					_mFocusText = value;
					UpdateItems();
					if (MPane.ItemColorChanged!=null) MPane.ItemColorChanged(this,new ItemColorChangedEventArgs(MozItemColor.FocusText));
				}
			}
		}



		[Description("Background color when item is not selected or has focus.")]
		[DefaultValue(typeof(Color),"White")]
		public Color Background
		{
			get => _mBack;
			set
			{
				if (_mBack!=value)
				{
					_mBack = value;
					UpdateItems();
					if (MPane.ItemColorChanged!=null) MPane.ItemColorChanged(this,new ItemColorChangedEventArgs(MozItemColor.Background));
				}
			}
		}

		[Description("Border color when item is not selected or has focus.")]
		[DefaultValue(typeof(Color),"Black")]
		public Color Border
		{
			get => _mBorder;
			set
			{
				if (_mBorder!=value)
				{
					_mBorder = value;
					UpdateItems();
					if (MPane.ItemColorChanged!=null) MPane.ItemColorChanged(this,new ItemColorChangedEventArgs(MozItemColor.Border));
				}
			}
		}
			
		[Description("Color used when item style is set to divider.")]
		[DefaultValue(typeof(Color),"127,157,185")]
		public Color Divider
		{
			get => _mDivider;
			set
			{
				if (_mDivider!=value)
				{
					_mDivider = value;
					UpdateItems();
					if (MPane.ItemColorChanged!=null) MPane.ItemColorChanged(this,new ItemColorChangedEventArgs(MozItemColor.Divider));
				}
			}
		}

		[Description("Background color when item is selected.")]
		[DefaultValue(typeof(Color),"193,210,238")]
		public Color SelectedBackground
		{
			get => _mSelected;
			set
			{
				if (_mSelected!=value)
				{
					_mSelected = value;
					UpdateItems();
					if (MPane.ItemColorChanged!=null) MPane.ItemColorChanged(this,new ItemColorChangedEventArgs(MozItemColor.SelectedBackground));
				}
			}
		}
		
		[Description("Border color when item is selected.")]
		[DefaultValue(typeof(Color),"49,106,197")]
		public Color SelectedBorder
		{
			get => _mSelectedBorder;
			set
			{
				if (_mSelectedBorder!=value)
				{
					_mSelectedBorder = value;
					UpdateItems();
					if (MPane.ItemColorChanged!=null) MPane.ItemColorChanged(this,new ItemColorChangedEventArgs(MozItemColor.SelectedBorder)); 
				}
			}
		}
		
		[Description("Background color when item has focus.")]
		[DefaultValue(typeof(Color),"224,232,246")]
		public Color FocusBackground
		{
			get => _mFocus;
			set
			{
				if (_mFocus!=value)
				{
					_mFocus = value;
					UpdateItems();
					if (MPane.ItemColorChanged!=null) MPane.ItemColorChanged(this,new ItemColorChangedEventArgs(MozItemColor.FocusBackground));
				}
			}
		}
		
		[Description("Border color when item has focus.")]
		[DefaultValue(typeof(Color),"152,180,226")]
		public Color FocusBorder
		{
			get => _mFocusBorder;
			set
			{
				if (_mFocusBorder!=value)
				{
					_mFocusBorder = value;
					UpdateItems();
					if (MPane.ItemColorChanged!=null) MPane.ItemColorChanged(this,new ItemColorChangedEventArgs(MozItemColor.FocusBorder));
				}
			}
		}
		
	}
		
	#endregion

	#region  BorderStyleCollection
	

	[TypeConverter(typeof(ItemCollectionTypeConverter))]
	public class BorderStyleCollection
	{
		public MozPane MPane;
			
		private ButtonBorderStyle _mBorderStyle;
		private ButtonBorderStyle _mFocusBorderStyle;
		private ButtonBorderStyle _mSelectedBorderStyle;


		public BorderStyleCollection(MozPane pane)
		{
			MPane = pane;
			_mBorderStyle = ButtonBorderStyle.None;
			_mFocusBorderStyle = ButtonBorderStyle.Solid;
			_mSelectedBorderStyle = ButtonBorderStyle.Solid;
		}
	
		public void Dispose()
		{
				
						
		}

		private void UpdateItems()
		{
			// for each item contained in the panel
			for (int i=0; i<MPane.Items.Count; i++)
			{
				// Refresh item
				MPane.Items[i].Invalidate();
			}
				
		}
			
		[Description("Border style when item has no focus.")]
		[DefaultValue(typeof(ButtonBorderStyle),"None")]
		public ButtonBorderStyle Normal
		{
			get => _mBorderStyle;
			set
			{
				if (_mBorderStyle!=value)
				{
					_mBorderStyle = value;
					UpdateItems();
					if (MPane.ItemBorderStyleChanged!=null) MPane.ItemBorderStyleChanged(this,new ItemBorderStyleChangedEventArgs(MozItemState.Normal));  
				}
			}
				
		}
			
		[Description("Border style when item has focus.")]
		[DefaultValue(typeof(ButtonBorderStyle),"Solid")]
		public ButtonBorderStyle Focus
		{
			get => _mFocusBorderStyle;
			set
			{
				if (_mFocusBorderStyle!=value)
				{
					_mFocusBorderStyle = value;
					UpdateItems();
					if (MPane.ItemBorderStyleChanged!=null) MPane.ItemBorderStyleChanged(this,new ItemBorderStyleChangedEventArgs(MozItemState.Focus));    
				}
			
			}
				
		}
			
		[Description("Border style when item is selected.")]
		[DefaultValue(typeof(ButtonBorderStyle),"Solid")]
		public ButtonBorderStyle Selected
		{
			get => _mSelectedBorderStyle;
			set
			{
				if (_mSelectedBorderStyle!=value)
				{
					_mSelectedBorderStyle = value;
					UpdateItems();
					if (MPane.ItemBorderStyleChanged!=null) MPane.ItemBorderStyleChanged(this,new ItemBorderStyleChangedEventArgs(MozItemState.Selected));    
				}
			}
	
		}
		
	}
		

	#endregion


}

#region Designer

// ControlDesigner
	
public class MozPaneDesigner  : ScrollableControlDesigner
{

	public MozPaneDesigner()
	{
	}

	public override void OnSetComponentDefaults()
	{
		base.OnSetComponentDefaults(); 
			
	}

		
	public override SelectionRules SelectionRules
	{
		get
		{
			// Remove all manual resizing of the control
			SelectionRules selectionRules = base.SelectionRules;
			selectionRules = SelectionRules.Visible |SelectionRules.AllSizeable | SelectionRules.Moveable;
			return selectionRules;
		}
	}

	protected override void PreFilterProperties(IDictionary properties)
	{
		base.PreFilterProperties(properties);
			
		// Remove obsolete properties
		properties.Remove("BackgroundImage");
		properties.Remove("ForeColor");
		properties.Remove("Text");
		properties.Remove("RightToLeft");
		properties.Remove("ImeMode");
		properties.Remove("AutoScroll");
	}
        
}

#endregion
	
#region ColorChangedEventArgs
	
public class ItemColorChangedEventArgs : EventArgs
{
	#region Class Data

	/// <summary>
	/// The color that has changed
	/// </summary>
	private MozItemColor _mColor;

	#endregion

	#region Constructor

	/// <summary>
	/// Initializes a new instance of the MozItemEventArgs class with default settings
	/// </summary>
	public ItemColorChangedEventArgs()
	{
		_mColor = 0;
	}


	public ItemColorChangedEventArgs(MozItemColor color)
	{
		_mColor = color;
	}

	#endregion


	#region Properties

	public MozItemColor Color => _mColor;

	#endregion
}


#endregion

	
#region ItemBorderStyleChangedEventArgs
	
public class ItemBorderStyleChangedEventArgs : EventArgs
{
	#region Class Data

	/// <summary>
	/// The MozItem that generated the event
	/// </summary>
	private MozItemState _mState;

	#endregion

	#region Constructor

	/// <summary>
	/// Initializes a new instance of the MozItemEventArgs class with default settings
	/// </summary>
	public ItemBorderStyleChangedEventArgs()
	{
		_mState = 0;
	}


	public ItemBorderStyleChangedEventArgs(MozItemState state)
	{
		_mState = state;
	}

	#endregion


	#region Properties

	public MozItemState State => _mState;

	#endregion
}

#endregion