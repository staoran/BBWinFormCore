using System.Collections;
using System.ComponentModel;
using System.Windows.Forms.Design;

namespace BB.BaseUI.Settings.MozBar;

#region Enumerations
public enum MozItemState {Normal = 0, Focus = 1, Selected = 2}
 
public enum MozItemStyle {Text = 0, Picture = 1, TextAndPicture = 2, Divider = 3}
public enum MozTextAlign {Bottom = 0, Right = 1, Top = 2, Left = 3}
#endregion
	
#region delegates

public delegate void MozItemEventHandler(object sender, MozItemEventArgs e);
public delegate void MozItemClickEventHandler(object sender, MozItemClickEventArgs e);
public delegate void ImageChangedEventHandler(object sender, ImageChangedEventArgs e);
	
#endregion

/// <summary>
/// Summary description for MozItem.
/// </summary>

[Designer(typeof(MozItemDesigner))]
[ToolboxItem(true)]
[DefaultEvent("Click")]
[ToolboxBitmap(typeof(MozItem),"MozItem.bmp")]
public class MozItem : DevExpress.XtraEditors.XtraUserControl
{
		
	#region EventHandler
		
	internal event MozItemEventHandler ItemGotFocus;
	internal event MozItemEventHandler ItemLostFocus;
	internal event MozItemClickEventHandler ItemClick;
	internal event MozItemClickEventHandler ItemDoubleClick;
				
	[Browsable(true)]
	[Category("Property Changed")]
	[Description("Indicates that the ItemStyle has changed.")]
	public event EventHandler ItemStyleChanged;
		
	[Browsable(true)]
	[Category("Property Changed")]
	[Description("Indicates that an item image has changed.")]
	public event ImageChangedEventHandler ImageChanged;

	[Browsable(true)]
	[Category("Property Changed")]
	[Description("Indicates that TextAlign has changed.")]
	public event EventHandler TextAlignChanged;
				
	#endregion
		
	#region private class members
				
	private MozPane _mMozPane;
	private MouseButtons _mMouseButton;
	private Image _image;

	private ImageCollection _mImageCollection;
								
	private MozTextAlign _mTextAlign;
				
	private MozItemState _mState;
	private MozItemStyle _mItemStyle;
			
	private Container _components = null;

	#endregion

	#region constructor

	public MozItem()
	{
		// This call is required by the Windows.Forms Form Designer.
		InitializeComponent();

		SetStyle(ControlStyles.DoubleBuffer, true);
		SetStyle(ControlStyles.AllPaintingInWmPaint, true);
		SetStyle(ControlStyles.UserPaint, true);
		SetStyle(ControlStyles.ResizeRedraw, true);
		SetStyle(ControlStyles.SupportsTransparentBackColor, true);

		// TODO: Add any initialization after the InitializeComponent call
			
		_mImageCollection = new ImageCollection(this); 
									
		_image = null;
								
		_mState = MozItemState.Normal;
 			
		_mItemStyle = MozItemStyle.TextAndPicture;
		_mTextAlign = MozTextAlign.Bottom; 
		DoLayout();
            			
	}

	#endregion

	#region dispose

	/// <summary> 
	/// Clean up any resources being used.
	/// </summary>
	protected override void Dispose( bool disposing )
	{
		if( disposing )
		{
			if(_components != null)
			{
				_components.Dispose();
			}

			// Delete images...
			if (_image!=null) _image.Dispose();
			_image = null;
			_mImageCollection.Dispose(); 
				
		}
		base.Dispose( disposing );
	}

	#endregion

	#region Component Designer generated code
	/// <summary> 
	/// Required method for Designer support - do not modify 
	/// the contents of this method with the code editor.
	/// </summary>
	private void InitializeComponent()
	{

	}
	#endregion
		

	#region Properties

	#region Colors
		
	private Color SelectedColor
	{
		get
		{
			if (_mMozPane!=null)
				return _mMozPane.ItemColors.SelectedBackground;
			else
				return Color.FromArgb(193,210,238);
		}	

	}

	private Color SelectedBorderColor
	{
		get
		{
			if (_mMozPane!=null)
				return _mMozPane.ItemColors.SelectedBorder;
			else
				return Color.FromArgb(49,106,197);
		}
	}

	private Color SelectedText
	{
		get
		{
			if (_mMozPane!=null)
				return _mMozPane.ItemColors.SelectedText;
			else
				return Color.Black;
		}
	}

	private Color FocusText
	{
		get
		{
			if (_mMozPane!=null)
				return _mMozPane.ItemColors.FocusText;
			else
				return Color.Black;
		}
	}

	private Color FocusColor
	{
		get
		{
			if (_mMozPane!=null)
				return _mMozPane.ItemColors.FocusBackground;
			else
				return Color.FromArgb(224,232,246);
		}
	}

	private Color FocusBorderColor
	{
		get
		{
			if (_mMozPane!=null)
				return _mMozPane.ItemColors.FocusBorder;
			else
				return Color.White;
		}
	}

	private Color TextColor
	{
		get
		{
			if (_mMozPane!=null)
				return _mMozPane.ItemColors.Text;
			else
				return Color.Black;

		}
	}

	private Color BackgroundColor
	{
		get
		{
			if (_mMozPane!=null)
				return _mMozPane.ItemColors.Background;
			else
				return Color.White;
		}
	}

	private Color BorderColor
	{
		get
		{
			if (_mMozPane!=null)
				return _mMozPane.ItemColors.Border;
			else
				return Color.FromArgb(152,180,226);
		}
	}

	private Color DividerColor
	{
		get
		{
			if (_mMozPane!=null)
				return _mMozPane.ItemColors.Divider;
			else
				return Color.FromArgb(127,157,185);
		}
	}

	#endregion

	#region BorderStyles

	private ButtonBorderStyle SelectedBorderStyle
	{
		get
		{
			if (_mMozPane!=null)
				return _mMozPane.ItemBorderStyles.Selected;
			else
				return ButtonBorderStyle.Solid; 
		}
	}

	private ButtonBorderStyle NormalBorderStyle
	{
		get
		{
			if (_mMozPane!=null)
				return _mMozPane.ItemBorderStyles.Normal;
			else
				return ButtonBorderStyle.None; 
		}
	}

	private ButtonBorderStyle FocusBorderStyle
	{
		get
		{
			if (_mMozPane!=null)
				return _mMozPane.ItemBorderStyles.Focus;
			else
				return ButtonBorderStyle.Solid; 
		}
	}


	#endregion
				
	[Browsable(true)]
	[Category("Appearance")]
	[Description("Images for various states.")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	public ImageCollection Images
	{
		get => _mImageCollection;
		set
		{
			if ((value != null) && (value!=_mImageCollection))
			{
				_mImageCollection = value;
			}
		}
	}
				
	[Browsable(true)]
	[Category("Appearance")]
	[Description("The alignment of the text that will be displayed.")]
	[DefaultValue(typeof(MozTextAlign),"Bottom")]
	public MozTextAlign TextAlign
	{
		get => _mTextAlign;
		set
		{
			if (_mTextAlign!=value)
			{
				_mTextAlign = value;
				DoLayout();
				if (MozPane != null)
				{
					MozPane.DoLayout();
				}
				if (TextAlignChanged!=null) TextAlignChanged(this,EventArgs.Empty);
				
				Invalidate();
			}
		}
	}

	public override string Text
	{
		get => base.Text;
		set
		{
			if (value!=base.Text)
			{
				base.Text = value;
				DoLayout();
				Invalidate();
			}
		}
	}
		
	[Browsable(true)]
	[Category("Appearance")]
	[Description("The visual appearance of the item.")]
	[DefaultValue(typeof(MozItemStyle),"TextAndPicture")]
	public MozItemStyle ItemStyle
	{
		get => _mItemStyle;
		set
		{
			if (value!=_mItemStyle)
			{
				_mItemStyle = value;
				DoLayout();
				if (MozPane != null)
				{
					MozPane.DoLayout();
				}
				if (ItemStyleChanged!=null) ItemStyleChanged(this,EventArgs.Empty);
				Invalidate();
			}
		}
	}

	// obsolete properties
		
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	// [Obsolete("This property is not supported",true)]
	public override Color BackColor
	{
		get => base.BackColor;
		set => base.BackColor = value;
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
		
	protected internal MozPane MozPane
	{
		get => _mMozPane;

		set => _mMozPane = value;
	}


	internal MozItemState State
	{
		get => _mState;
		set
		{
			_mState = value;
			Invalidate();
		}
	}
		
	#endregion

	#region methods
		
	public void DoLayout()
	{
		int imageHeight;
		int imageWidth;
		MozPaneStyle mode;

		ImageList list = GetImageList();			
			
		if (list!=null)
		{
			imageHeight = list.ImageSize.Height;
			imageWidth = list.ImageSize.Width;
		}
		else
		{
			imageHeight = 32;
			imageWidth = 32;
		}
			
		if (_mMozPane!=null)
			mode = _mMozPane.Style;
		else
			mode = MozPaneStyle.Vertical; 
						
		switch (mode)
		{
			case MozPaneStyle.Vertical:
			{
				if (_mMozPane!=null)
				{
					if (!_mMozPane.IsVerticalScrollBarVisible()) 
						Width = _mMozPane.Width -(2*_mMozPane.Padding.Horizontal);
					else
						Width = _mMozPane.Width -(2*_mMozPane.Padding.Horizontal)-3 - (SystemInformation.VerticalScrollBarWidth-2);
				}
				else
					Width = 40;

				switch (_mItemStyle)
				{
											
					case MozItemStyle.Divider:
					{
						Height = 2*4;
						break;
					}
				
					case MozItemStyle.Picture:
					{
						Height = imageHeight + (2*4);
						break;
					}
					case MozItemStyle.Text:
					{
						Height = base.Font.Height + (2*4);
						break;
					}
					case MozItemStyle.TextAndPicture:
					{
						switch (_mTextAlign)
						{
							case MozTextAlign.Bottom:
							case MozTextAlign.Top:
							{
								Height = imageHeight + (3*4) + base.Font.Height;
								break;
							}
							case MozTextAlign.Right:
							case MozTextAlign.Left:
							{
								Height = imageHeight + (2*4);
								break;
							}
						
						}
						break;
					}
				}
				break;
			}
			case MozPaneStyle.Horizontal:
			{
				if (_mMozPane!=null)
					if (!_mMozPane.IsHorizontalScrollBarVisible())
						Height = _mMozPane.Height -(2*_mMozPane.Padding.Vertical);
					else
						Height = _mMozPane.Height -(2*_mMozPane.Padding.Vertical)-3 - (SystemInformation.HorizontalScrollBarHeight-2);

				else
					Height = 40;
					
				switch (_mItemStyle)
				{
					case MozItemStyle.Divider:
					{
						Width = 2*4;
						break;
					}
					case MozItemStyle.Picture:
					{
						Width = imageWidth + (2*4);
						break;
					}
					case MozItemStyle.Text:
					{
							  
						Width = (2*4)+ (int)MeasureString(Text);
						break;
					}
					case MozItemStyle.TextAndPicture:
					{
						switch (_mTextAlign)
						{
							case MozTextAlign.Bottom:
							case MozTextAlign.Top:
							{
								int minWidth = 2 * 4 + imageWidth;
								int stringWidth = (2*4)+ (int)MeasureString(Text); 
								if (stringWidth > minWidth)
									Width = stringWidth;
								else
									Width = minWidth;
								break;
							}
							case MozTextAlign.Right:
							case MozTextAlign.Left:
							{
								Width  = (3*4)+ (int)MeasureString(Text) + imageWidth;
								break;
							}
						
						}
						break;
					}
				}
				break;
			}
		}
			
	}

	private float MeasureString(string str)
	{
			
		SizeF f = new SizeF();
		Graphics g;
		g = CreateGraphics();
		f = g.MeasureString(str,Font);  
		g.Dispose();
		g = null;
			
		return f.Width; 
	}

	public ImageList GetImageList()
	{
		if (_mMozPane == null)
			return null;
		else
			return _mMozPane.ImageList;
	}

	public bool IsSelected()
	{
		if (_mState == MozItemState.Selected)
			return true;
		else
			return false;
	}

	public void SelectItem()
	{
		if (Enabled)
		{
			if (ItemClick!=null) ItemClick(this,new MozItemClickEventArgs(this,MouseButtons.Left));
			Invalidate();
		}
	}

	#endregion


	#region Events
		
	protected override void OnEnabledChanged(EventArgs e)
	{
		base.OnEnabledChanged (e);
		// Enabled has changed, Invalidate..
		Invalidate();
	}

	protected override void OnFontChanged(EventArgs e)
	{
		base.OnFontChanged (e);
		DoLayout();
		Invalidate();
	}
		
	protected override void OnSizeChanged(EventArgs e)
	{
		base.OnSizeChanged (e);
						
		if (_mMozPane!=null)
		{
			_mMozPane.DoLayout();
			_mMozPane.Invalidate();
		}
		DoLayout();
		Invalidate();

	}
		
	protected override void OnMouseEnter(EventArgs e)
	{
		if (ItemGotFocus != null) ItemGotFocus(this,new MozItemEventArgs(this));		
	}
		
	protected override void OnMouseLeave(EventArgs e)
	{
		if (ItemLostFocus!=null) ItemLostFocus(this,new MozItemEventArgs(this));
	}

	protected override void OnMouseDown(MouseEventArgs e)
	{
		_mMouseButton = e.Button;
		if (ItemClick!=null) ItemClick(this,new MozItemClickEventArgs(this,_mMouseButton));
	}

	protected override void OnDoubleClick(EventArgs e)
	{
		if (ItemDoubleClick!=null) ItemDoubleClick(this,new MozItemClickEventArgs(this,_mMouseButton));	
	}

	protected override void OnClick(EventArgs e)
	{
		base.OnClick (e);
	}

	protected override void OnPaint(PaintEventArgs e)
	{
			
		int imageHeight;
		int imageWidth;
		int paddingX;
		int paddingY;
						
			
		Pen dividerPen = new Pen(DividerColor,0);
		Brush textBrush = new SolidBrush(TextColor);
		Brush disabledTextBrush = new SolidBrush(Color.Gray);
		Brush bgBrush = new SolidBrush(Color.Black); 
		Color borderColor = Color.Black;
		ButtonBorderStyle btnBorderStyle = ButtonBorderStyle.None;
 
		// Check if a ImageList exist
		ImageList list = GetImageList();
		if (list!=null)
		{
			// if so get Height and Width
			imageHeight = list.ImageSize.Height;
			imageWidth = list.ImageSize.Width;
		}
		else
		{
			// if not use default values
			imageHeight = 32;
			imageWidth = 32;
		}

		// Check if the item has belongs to a MozPane
		if (_mMozPane!=null)
		{
			// If so get the padding
			paddingX = _mMozPane.Padding.Horizontal;
			paddingY = _mMozPane.Padding.Vertical;
		}
		else
		{
			// use some kind of padding if no daddy is found
			paddingX = 1;
			paddingY = 1;
		}

		Rectangle textRect = new Rectangle();
		Rectangle imageRect = new Rectangle(0,0,imageWidth,imageHeight);
		Rectangle borderRect = new Rectangle();
											
		StringFormat f = new StringFormat();

		Point borderLocation = new Point
		{
			X = 0,
			Y = 0
		};

		borderRect.Location = borderLocation;
		borderRect.Width = Width;
		borderRect.Height = Height; 
			
		// Draw background
		e.Graphics.FillRectangle(new SolidBrush(BackgroundColor),DisplayRectangle);

		// Use Normal image for disabled state
		if (!Enabled) _mState = MozItemState.Normal;
		// A divider should not be able to be selected or recieve focus
		if (ItemStyle == MozItemStyle.Divider) _mState = MozItemState.Normal;  

		// Check state for item, to decide image
		switch (_mState)
		{
			case MozItemState.Focus:
			{
					
				textBrush = new SolidBrush(FocusText);
				bgBrush = new SolidBrush(FocusColor);
				borderColor = FocusBorderColor; 
				btnBorderStyle = FocusBorderStyle; 		
				if (_mImageCollection.FocusImage!= null)
					_image = _mImageCollection.FocusImage;
				else
					// if focusimage isnt set use Normal image
					_image = _mImageCollection.NormalImage;
				break;
			}
			case MozItemState.Selected:
			{
				textBrush = new SolidBrush(SelectedText);
				bgBrush = new SolidBrush(SelectedColor);
				borderColor = SelectedBorderColor; 
				btnBorderStyle = SelectedBorderStyle;
				if (_mImageCollection.SelectedImage!= null)
					_image = _mImageCollection.SelectedImage;
				else
					_image = _mImageCollection.NormalImage;
				break;
			}
			case MozItemState.Normal:
			{
				_image = _mImageCollection.NormalImage;
				bgBrush = new SolidBrush(BackgroundColor);
				btnBorderStyle = NormalBorderStyle;
				borderColor = BorderColor; 
				break;
			}
		}

		e.Graphics.FillRectangle(bgBrush,borderRect);
		ControlPaint.DrawBorder(e.Graphics,borderRect,borderColor,btnBorderStyle);
									
		// check for itemStyle
		switch (_mItemStyle)
		{
			case MozItemStyle.Divider:
			{
				float ptY;
				float ptX;

				if (_mMozPane!=null)
				{
					// Check MozPane orientation
					if (_mMozPane.Style == MozPaneStyle.Vertical) 
					{
						ptY = borderRect.Top + (borderRect.Height / 2);
						e.Graphics.DrawLine(dividerPen,borderRect.Left,ptY,borderRect.Right,ptY);
					}
					else
					{
						ptX = borderRect.Left + (borderRect.Width / 2);
						e.Graphics.DrawLine(dividerPen,ptX,borderRect.Top,ptX,borderRect.Bottom);
					}
				}
				else
				{
					ptY = borderRect.Top + (borderRect.Height / 2);
					e.Graphics.DrawLine(dividerPen,borderRect.Left,ptY,borderRect.Right,ptY);
				}
					
				break;
			}
			case MozItemStyle.Text:
			{	
				f.Alignment = StringAlignment.Center;
				f.LineAlignment = StringAlignment.Center;
				textRect = borderRect;
				if (_mState == MozItemState.Selected)
				{
					textRect.X+=1;
					textRect.Y+=1;
				}
				if (Enabled)
					e.Graphics.DrawString(Text,Font,textBrush,textRect,f);   
				else
					e.Graphics.DrawString(Text,Font,disabledTextBrush,textRect,f);
				break;
			}
			case MozItemStyle.Picture:
			{
				if (_image!=null)
				{							
					// center image
					imageRect.X = ((borderRect.Width/2) - (imageRect.Width/2));
					imageRect.Y = ((borderRect.Height/2) - (imageRect.Height/2));
					if (_mState == MozItemState.Selected)
					{
						imageRect.X+=1;
						imageRect.Y+=1;
					}
					
					if (Enabled) 
						if (_image!=null)
							e.Graphics.DrawImage(_image,imageRect);
						else
						if (_image!=null)
							ControlPaint.DrawImageDisabled(e.Graphics,_image,imageRect.X,imageRect.Y,BackgroundColor);   
				}
				break;
			}
			case MozItemStyle.TextAndPicture:
			{
				f.LineAlignment = StringAlignment.Center;
										
				switch (_mTextAlign)
				{
					case MozTextAlign.Bottom:
					{
							
						f.Alignment = StringAlignment.Center;
						textRect.Height = Font.Height + (2*4);
						textRect.Y = borderRect.Bottom - textRect.Height;
						textRect.X = borderRect.X;
						textRect.Width = borderRect.Width;

						imageRect.Y = borderRect.Top +2;
						imageRect.X = ((borderRect.Width/2) - imageRect.Width/2);
						break;
					}
					case MozTextAlign.Top:
					{
						f.Alignment = StringAlignment.Center;
						textRect.Height = Font.Height + (2*4);
						textRect.Y = borderRect.Top; 
						textRect.X = borderRect.X;
						textRect.Width = borderRect.Width;
							
						imageRect.Y =  borderRect.Bottom - 2 - imageRect.Height;
						imageRect.X = ((borderRect.Width/2) - imageRect.Width/2);
						break;
					}
					case MozTextAlign.Right:
					{
							
						f.Alignment = StringAlignment.Near;
						textRect.Height = borderRect.Height - 2 * 4;
						textRect.Y = borderRect.Top +4;
						textRect.X = borderRect.X + 4 + imageRect.Width+ 4;
						textRect.Width = borderRect.Width - 4 - imageRect.Width;

						imageRect.X = 4;
						imageRect.Y = ((borderRect.Height/2) - (imageRect.Height/2));
						break;
					}
					case MozTextAlign.Left:
					{
						f.Alignment = StringAlignment.Near;
						textRect.Height = borderRect.Height - 2 * 4;
						textRect.Y = borderRect.Top +4;
						textRect.X = borderRect.X + 4; 
						textRect.Width = borderRect.Width - 4 - imageRect.Width;
							
						imageRect.X = borderRect.Right - 4 - imageRect.Width; 
						imageRect.Y = ((borderRect.Height/2) - (imageRect.Height/2));
						break;
					}
				}
					
				// Check if enabled
				if (Enabled)
				{
					if (_mState == MozItemState.Selected)
					{
						imageRect.X+=1;
						imageRect.Y+=1;
						textRect.X+=1;
						textRect.Y+=1;
					}
					// draw image and text
					if (_image!=null)
						e.Graphics.DrawImage(_image,imageRect);
					e.Graphics.DrawString(Text,Font,textBrush,textRect,f);
				}
				else
				{
					// Draw disabled image and text
					if (_image!=null)
						ControlPaint.DrawImageDisabled(e.Graphics,_image,imageRect.X,imageRect.Y,BackColor);
					e.Graphics.DrawString(Text,Font,disabledTextBrush,textRect,f);
				}
								
				break;
			}
		}
			
		// tidy up
		dividerPen.Dispose();
		textBrush.Dispose();
		disabledTextBrush.Dispose();
		bgBrush.Dispose();
	
	}

	protected override void OnResize(EventArgs e)
	{
		if (_mMozPane!=null)
		{
			_mMozPane.DoLayout();
			_mMozPane.Invalidate();
		}
		DoLayout();
		Invalidate();
			
	}

	protected override void OnMove(EventArgs e)
	{
		Invalidate();
	}


	#endregion
				

	#region ImageCollection


	[TypeConverter(typeof(ItemCollectionTypeConverter))]
	public class ImageCollection
	{
			
		private MozItem _mItem;
			
		private int _mImageIndex;
		private int _mFocusImageIndex;
		private int _mSelectedImageIndex;
			
		// used for setting image without using an imagelist..
		private Image _mImage;
		private Image _mFocusImage;
		private Image _mSelectedImage;
			
		public MozItem Item;
			

		public ImageCollection(MozItem item)
		{
				
			_mItem = item;
			_mImageIndex = -1;
			_mFocusImageIndex = -1;
			_mSelectedImageIndex = -1;

			_mImage = null;
			_mFocusImage = null;
			_mSelectedImage = null;
		}
			
		public void Dispose()
		{
			_mImageIndex = -1;
			_mFocusImageIndex = -1;
			_mSelectedImageIndex = -1;
			if (_mImage!=null) _mImage.Dispose();
			if (_mFocusImage!=null) _mFocusImage.Dispose();
			if (_mSelectedImage!=null) _mSelectedImage.Dispose();
		}

		public ImageList GetImageList()
		{
			if (_mItem == null)
				return null;
			else
				return _mItem.GetImageList();
		}

		[Browsable(false)]	
		public Image NormalImage
		{
			get
			{
				// If image already has been set (without imagelist) return it.
				if (_mImage!=null) return _mImage;

				// Check that an ImageList exists and that index is valid
				if ((GetImageList()!=null) && (_mImageIndex!=-1))
				{
					// make sure we catch any exceptions that might occur here
					try
					{
						// return image from panels ImageList
						return GetImageList().Images[_mImageIndex];
					}
					catch(Exception)
					{
						// An exception occured , the imagelist (or image) might have been disposed
						// or removed in the designer , return null
						return null;
					}
				}
				else return null;
			}
			set
			{
				_mImage = value;
				if (_mItem.ImageChanged!=null) _mItem.ImageChanged(this,new ImageChangedEventArgs(MozItemState.Normal));
				_mItem.Invalidate();
					
			}

		}
			
		[TypeConverter(typeof(ImageTypeConverter))]
		[Editor(typeof(ImageMapEditor),typeof(System.Drawing.Design.UITypeEditor))]
		[Description("Image for normal state.")]		
		public int Normal
		{
			get => _mImageIndex;
			set
			{
				if (value != _mImageIndex)
				{
					_mImageIndex = value;
					if (_mItem.ImageChanged!=null) _mItem.ImageChanged(this,new ImageChangedEventArgs(MozItemState.Normal));
					_mItem.Invalidate();
				}
			}
		}
			
		[Browsable(false)]	
		public Image FocusImage
		{
			get
			{
				if (_mFocusImage!=null) return _mFocusImage;

				if ((GetImageList()!=null) && (_mFocusImageIndex!=-1))
				{
					try
					{
						return GetImageList().Images[_mFocusImageIndex];
					}
					catch (Exception)
					{
						return null;
					}
				}
				else return null;
			}
			set
			{
				_mFocusImage = value;
				if (_mItem.ImageChanged!=null) _mItem.ImageChanged(this,new ImageChangedEventArgs(MozItemState.Focus));
				_mItem.Invalidate();
			}

		}

		[TypeConverter(typeof(ImageTypeConverter))]
		[Editor(typeof(ImageMapEditor),typeof(System.Drawing.Design.UITypeEditor))]
		[Description("Image for has focus state.")]
		public int Focus
		{
			get => _mFocusImageIndex;
			set
			{
				if (value != _mFocusImageIndex)
				{
						
					_mFocusImageIndex = value;
																
					if (_mItem.ImageChanged!=null) _mItem.ImageChanged(this,new ImageChangedEventArgs(MozItemState.Focus));
					_mItem.Invalidate();
				}

			}
		}

		[Browsable(false)]	
		public Image SelectedImage
		{
			get
			{
				if (_mSelectedImage!=null) return _mSelectedImage;

				if ((GetImageList()!=null) && (_mSelectedImageIndex!=-1))
				{
					try
					{
						return GetImageList().Images[_mSelectedImageIndex];
					}
					catch (Exception)
					{
						return null;
					}
				}
				else return null;
			}
			set
			{
				_mSelectedImage = value;
				if (_mItem.ImageChanged!=null) _mItem.ImageChanged(this,new ImageChangedEventArgs(MozItemState.Selected));
				_mItem.Invalidate();
					
			}

		}
	
		[TypeConverter(typeof(ImageTypeConverter))]
		[Editor(typeof(ImageMapEditor),typeof(System.Drawing.Design.UITypeEditor))]
		[Description("Image for selected state.")]
		public int Selected
		{
			get => _mSelectedImageIndex;
			set
			{
				if (value != _mSelectedImageIndex)
				{
						
					_mSelectedImageIndex = value;

					if (_mItem.ImageChanged!=null) _mItem.ImageChanged(this,new ImageChangedEventArgs(MozItemState.Selected));
					_mItem.Invalidate();
				}
			}
		}
		
	}

	#endregion
		
	#region ImageTypeConverter

	public class ImageTypeConverter : TypeConverter
	{
		public override object ConvertTo(ITypeDescriptorContext context,System.Globalization.CultureInfo culture,object value,Type destinationType)
		{
			if (value.ToString() == "-1")
			{
				return "(none)";
			}
			else
			{
				return value.ToString();
			}
			
		}
        	
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{

			if(destinationType == typeof(string))
				return true;
			return base.CanConvertTo (context, destinationType);

		}

		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			if(sourceType == typeof(string))
				return true;
			return base.CanConvertFrom (context, sourceType);

		}

		public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
		{
			if(value.GetType() == typeof(string))
			{
				// none = -1 = no image
				if (( value.ToString().ToUpper().IndexOf("NONE") >=0) || (value.ToString()==""))
					return -1;
				return
					Convert.ToInt16(value.ToString()); 
			}
			return base.ConvertFrom (context, culture, value);
			
		}

	}

	#endregion

}

#region ItemCollectionTypeConverter

public class ItemCollectionTypeConverter : ExpandableObjectConverter
{
	// override stuff here
					
	public override object ConvertTo(ITypeDescriptorContext context,System.Globalization.CultureInfo culture,object value,Type destinationType)
	{
		return "";
	}        	

}

#endregion

#region Designer
// Designer

public class MozItemDesigner  : ControlDesigner
{

	public MozItemDesigner()
	{
	}

	public override void OnSetComponentDefaults()
	{
		base.OnSetComponentDefaults(); 
	}

	protected override void OnPaintAdornments(PaintEventArgs pe)
	{
		base.OnPaintAdornments (pe);
		System.Windows.Forms.Control item = base.Control;
		Rectangle rect = item.ClientRectangle;
		ControlPaint.DrawBorder(pe.Graphics,rect,Color.Black,ButtonBorderStyle.Dashed);  
			
	}


	public override bool CanBeParentedTo(System.ComponentModel.Design.IDesigner parentDesigner)
	{
		if (parentDesigner.Component is MozPane)
			return true;
		else
			return false;
	}
		
	public override SelectionRules SelectionRules
	{
		get
		{
							
			SelectionRules selectionRules = base.SelectionRules;
			selectionRules = SelectionRules.Visible;   
			return selectionRules;
		}
	}

	protected override void PreFilterProperties(IDictionary properties)
	{
		base.PreFilterProperties(properties);
			
		properties.Remove("BackgroundImage");
		properties.Remove("RightToLeft");
		properties.Remove("Imemode");
			
	}	

	protected override void PreFilterEvents(IDictionary events)
	{
		base.PreFilterEvents (events);
		events.Remove("ForeColorChanged");
		events.Remove("BackColorChanged");
		events.Remove("BorderStyleChanged");
	}
        
}

#endregion
	
#region MozItemEventArgs

public class MozItemEventArgs : EventArgs
{
	#region Class Data

	/// <summary>
	/// The MozItem that generated the event
	/// </summary>
	private MozItem _mMozItem;

	#endregion

	#region Constructor

	/// <summary>
	/// Initializes a new instance of the MozItemEventArgs class with default settings
	/// </summary>
	public MozItemEventArgs()
	{
		_mMozItem = null;
	}


	public MozItemEventArgs(MozItem mozItem)
	{
		_mMozItem = mozItem;
	}

	#endregion


	#region Properties

	public MozItem MozItem => _mMozItem;

	#endregion
}

#endregion

#region MozItemClickEventArgs
	
public class MozItemClickEventArgs : EventArgs
{
	#region Class Data

	/// <summary>
	/// The MozItem that generated the event
	/// </summary>
	private MozItem _mMozItem;
	private MouseButtons _mButton;

	#endregion

	#region Constructor

	/// <summary>
	/// Initializes a new instance of the MozItemEventArgs class with default settings
	/// </summary>
	public MozItemClickEventArgs()
	{
		_mMozItem = null;
		_mButton = MouseButtons.Left;
	}

	public MozItemClickEventArgs(MozItem mozItem, MouseButtons button)
	{
		_mMozItem = mozItem;
		_mButton = button;
	}

	#endregion


	#region Properties

	public MozItem MozItem => _mMozItem;

	public MouseButtons Button => _mButton;

	#endregion
}


#endregion
	
#region ImageChangedEventArgs
	
public class ImageChangedEventArgs : EventArgs
{
	#region Class Data

	/// <summary>
	/// The image that has changed
	/// </summary>
	private MozItemState _mImage;

	#endregion

	#region Constructor

	/// <summary>
	/// Initializes a new instance of the MozItemEventArgs class with default settings
	/// </summary>
	public ImageChangedEventArgs()
	{
		_mImage = 0;
	}


	public ImageChangedEventArgs(MozItemState image)
	{
		_mImage = image;
	}

	#endregion


	#region Properties

	public MozItemState Image => _mImage;

	#endregion
}


#endregion