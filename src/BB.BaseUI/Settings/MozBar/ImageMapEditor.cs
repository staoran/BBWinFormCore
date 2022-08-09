using System.Drawing.Design;
using System.Windows.Forms.Design;

namespace BB.BaseUI.Settings.MozBar;

/// <summary>
/// Summary description for ImageEditor.
/// </summary>
public class ImageMapEditor : UITypeEditor   
{
		
	#region properties
		
	private IWindowsFormsEditorService _wfes = null ;
	private int _mSelectedIndex = -1 ;
	private ImageListPanel _mImagePanel = null ;
	
	#endregion
		
	#region constructor

	public ImageMapEditor()
	{
			
	}

	#endregion

	#region Methods

	protected virtual ImageList GetImageList(object component) 
	{
		if (component is MozItem.ImageCollection) 
		{
			return ((MozItem.ImageCollection) component).GetImageList();
		}

		return null ;
	}

	#endregion
		
	#region overrides

	public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
	{
		_wfes = (IWindowsFormsEditorService)	provider.GetService(typeof(IWindowsFormsEditorService));
		if((_wfes == null) || (context == null))
			return null ;
			
		ImageList imageList = GetImageList(context.Instance) ;
		if ((imageList == null) || (imageList.Images.Count==0))
			return -1 ;

		_mImagePanel = new ImageListPanel(); 
						
		_mImagePanel.BackgroundColor = Color.FromArgb(241,241,241);
		_mImagePanel.BackgroundOverColor = Color.FromArgb(102,154,204);
		_mImagePanel.HLinesColor = Color.FromArgb(182,189,210);
		_mImagePanel.VLinesColor = Color.FromArgb(182,189,210);
		_mImagePanel.BorderColor = Color.FromArgb(0,0,0);
		_mImagePanel.EnableDragDrop = true;
		_mImagePanel.Init(imageList,12,12,6,(int)value);
			
		// add listner for event
		_mImagePanel.ItemClick += OnItemClicked;
			
		// set m_selectedIndex to -1 in case the dropdown is closed without selection
		_mSelectedIndex = -1;
		// show the popup as a drop-down
		_wfes.DropDownControl(_mImagePanel) ;
			
		// return the selection (or the original value if none selected)
		return (_mSelectedIndex != -1) ? _mSelectedIndex : (int) value ;
	}

	public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
	{
		if(context != null && context.Instance != null ) 
		{
			return UITypeEditorEditStyle.DropDown ;
		}
		return base.GetEditStyle (context);
	}
		

	public override bool GetPaintValueSupported(System.ComponentModel.ITypeDescriptorContext context)
	{
		return true;
	}

	public override void PaintValue(PaintValueEventArgs pe)
	{
		int imageIndex = -1 ;	
		// value is the image index
		if(pe.Value != null) 
		{
			try 
			{
				imageIndex = (int)Convert.ToUInt16( pe.Value.ToString() ) ;
			}
			catch
			{
			}
		}
		// no instance, or the instance represents an undefined image
		if((pe.Context.Instance == null) || (imageIndex < 0))
			return ;
		// get the image set
		ImageList imageList = GetImageList(pe.Context.Instance) ;
		// make sure everything is valid
		if((imageList == null) || (imageList.Images.Count == 0) || (imageIndex >= imageList.Images.Count))
			return ;
		// Draw the preview image
		pe.Graphics.DrawImage(imageList.Images[imageIndex],pe.Bounds);
	}

	#endregion

	#region EventHandlers

	public void OnItemClicked(object sender, ImageListPanelEventArgs e)
	{
		_mSelectedIndex = ((ImageListPanelEventArgs) e).SelectedItem;
			
		//remove listner
		_mImagePanel.ItemClick -= OnItemClicked;
			
		// close the drop-dwon, we are done
		_wfes.CloseDropDown() ;

		_mImagePanel.Dispose() ;
		_mImagePanel = null ;
	}

	#endregion
	
}