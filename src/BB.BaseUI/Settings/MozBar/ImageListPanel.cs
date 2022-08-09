using System.ComponentModel;

namespace BB.BaseUI.Settings.MozBar;

#region Delegates

public delegate void ImageListPanelEventHandler(object sender, ImageListPanelEventArgs ilpea);

#endregion

[ToolboxItem(false)]
public class ImageListPanel : DevExpress.XtraEditors.XtraUserControl
{
		
	#region Protected Member Variables
	protected Bitmap	Bitmap = null;
	protected ImageList	ImageList = null;
	protected int		NBitmapWidth = 0;
	protected int		NBitmapHeight = 0;
	protected int		NItemWidth = 0;
	protected int		NItemHeight = 0;
	protected int		NRows = 0;
	protected int		NColumns = 0;
	protected int		NHSpace = 0;
	protected int		NVSpace = 0;
	protected int		NCoordX = -1;
	protected int		NCoordY = -1;
	protected bool		BIsMouseDown = false;
		
	protected int DefaultImage;
		
	#endregion
		
	#region Public Properties
	public Color		BackgroundColor = Color.FromArgb(255,255,255);
	public Color		BackgroundOverColor = Color.FromArgb(241,238,231);
	public Color		HLinesColor = Color.FromArgb(222,222,222);
	public Color		VLinesColor = Color.FromArgb(165,182,222);
	public Color		BorderColor = Color.FromArgb(0,16,123);
	public bool			EnableDragDrop = false;
	#endregion

	#region Events

	public event	ImageListPanelEventHandler ItemClick = null;
	#endregion


	#region Constructor
	public ImageListPanel()
	{

	}
	#endregion

	#region Public Methods
	public bool Init(ImageList imageList, int nHSpace, int nVSpace, int nColumns, int defaultImage)
	{
		int nRows;
			
		Brush bgBrush = new SolidBrush(BackgroundColor);
		Pen vPen = new Pen(VLinesColor);
		Pen hPen = new Pen(HLinesColor);
		Pen borderPen = new Pen(BorderColor);
						
		ImageList = imageList;
		NColumns = nColumns;

		DefaultImage = defaultImage;
		if (DefaultImage > ImageList.Images.Count)
			DefaultImage = ImageList.Images.Count;
		if (DefaultImage < 0) DefaultImage = -1;
			
			
		nRows = imageList.Images.Count / NColumns;
		if (imageList.Images.Count % NColumns > 0) nRows++;

		NRows = nRows;
		NHSpace = nHSpace;
		NVSpace = nVSpace;
		NItemWidth = ImageList.ImageSize.Width + nHSpace;
		NItemHeight = ImageList.ImageSize.Height + nVSpace;
		NBitmapWidth = NColumns * NItemWidth + 1;
		NBitmapHeight = NRows * NItemHeight + 1;
		Width = NBitmapWidth;
		Height = NBitmapHeight;


		Bitmap = new Bitmap(NBitmapWidth,NBitmapHeight);
		Graphics grfx =	Graphics.FromImage(Bitmap);
		grfx.FillRectangle(bgBrush, 0, 0, NBitmapWidth, NBitmapHeight);
		for (int i=0;i<NColumns;i++)
			grfx.DrawLine(vPen, i*NItemWidth, 0, i*NItemWidth, NBitmapHeight-1);
		for (int i=0;i<NRows;i++)
			grfx.DrawLine(hPen, 0, i*NItemHeight, NBitmapWidth-1, i*NItemHeight);
			
		grfx.DrawRectangle(borderPen, 0 ,0 , NBitmapWidth-1, NBitmapHeight-1);

		for (int i=0;i<NColumns;i++)
		for (int j=0;j<NRows ;j++)
		{
			if ((j*NColumns+i) < imageList.Images.Count)
				imageList.Draw(grfx,
					i*NItemWidth+NHSpace/2,
					j*NItemHeight+nVSpace/2,
					imageList.ImageSize.Width,
					imageList.ImageSize.Height,
					j*NColumns+i);
	
		}

		/*	int a = (_defaultImage / _nColumns);  // rad
			int b = (_defaultImage % _nColumns); // kolumn;

			_nCoordX = b*(_nItemWidth+_nHSpace/2)-1;
			_nCoordY = a*(_nItemHeight+nVSpace/2)-1;
		*/	
			
		// Clean up
		bgBrush.Dispose();
		vPen.Dispose();
		hPen.Dispose();
		borderPen.Dispose();
								
		Invalidate();
		return true;
	}
	
	public void Show(int x, int y)
	{
		Left = x;
		Top = y;
		base.Show();
	}
	#endregion

	
	#region Overrides
	
	protected override void OnMouseLeave(EventArgs ea)
	{
		// We repaint the popup if the mouse is no more over it
		base.OnMouseLeave(ea);
		NCoordX = -1;
		NCoordY = -1;
		Invalidate();
	}

	/*protected override void OnDeactivate(EventArgs ea)
	{
		// If the form loses focus, we hide it
		this.Hide();
	}*/

	protected override void OnKeyDown(KeyEventArgs kea)
	{
		if (NCoordX==-1 || NCoordY==-1)
		{
			NCoordX = 0;
			NCoordY = 0;
			Invalidate();
		}
		else
		{
			switch(kea.KeyCode)
			{
				case Keys.Down:
					if (NCoordY<NRows-1)
					{
						NCoordY++;
						Invalidate();
					}
					break;
				case Keys.Up:
					if (NCoordY>0)
					{
						NCoordY--;
						Invalidate();
					}
					break;
				case Keys.Right:
					if (NCoordX<NColumns-1)
					{
						NCoordX++;
						Invalidate();
					}
					break;
				case Keys.Left:
					if (NCoordX>0)
					{
						NCoordX--;
						Invalidate();
					}
					break;
				case Keys.Enter:
				case Keys.Space:
					// We fire the event only when the mouse is released
					int nImageId = NCoordY*NColumns + NCoordX;
					if (ItemClick != null && nImageId>=0 && nImageId<ImageList.Images.Count)
					{
						ItemClick(this, new ImageListPanelEventArgs(nImageId));
						NCoordX = -1;
						NCoordY = -1;
						Hide();
					}
					break;
				case Keys.Escape:
					NCoordX = -1;
					NCoordY = -1;
					Hide();
					break;
			}
		}
	}

	protected override void OnMouseMove(MouseEventArgs mea)
	{
		// Update the popup only if the image selection has changed
		if (ClientRectangle.Contains(new Point(mea.X,mea.Y)))
		{
			if (EnableDragDrop && BIsMouseDown)
			{
				int nImage = NCoordY*NColumns+NCoordX;
				if (nImage <=ImageList.Images.Count-1)
				{
					DataObject data = new DataObject();
					data.SetData(DataFormats.Text,nImage.ToString());
					data.SetData(DataFormats.Bitmap,ImageList.Images[nImage]);
					try
					{
						DragDropEffects dde = DoDragDrop(data, DragDropEffects.Copy | DragDropEffects.Move);
					}
					catch
					{

					}
					BIsMouseDown = false;
				}
			}

			if ( ((mea.X/NItemWidth)!=NCoordX) || ((mea.Y/NItemHeight)!=NCoordY) )
			{
				NCoordX = mea.X/NItemWidth;
				NCoordY = mea.Y/NItemHeight;
				Invalidate();
			}
		}
		else
		{
			NCoordX = -1;
			NCoordY = -1;
			Invalidate();
		}
		base.OnMouseMove(mea);
	}

	protected override void OnMouseDown(MouseEventArgs mea)
	{
		base.OnMouseDown(mea);
		BIsMouseDown = true;
		Invalidate();
	}

	protected override void OnMouseUp(MouseEventArgs mea)
	{
		base.OnMouseDown(mea);
		BIsMouseDown = false;
			
		// We fire the event only when the mouse is released
		int nImageId = NCoordY*NColumns + NCoordX;
		// check that imageID is a valid image
		if (ItemClick != null && nImageId>=0 && nImageId<ImageList.Images.Count)
		{
			ItemClick(this, new ImageListPanelEventArgs(nImageId));
			Hide();
		}
	}
	

	protected override void OnPaintBackground(PaintEventArgs pea)
	{
		Graphics grfx = pea.Graphics;
		grfx.PageUnit = GraphicsUnit.Pixel;
			
		// Basic double buffering technique
		Bitmap offscreenBitmap = new Bitmap(NBitmapWidth, NBitmapHeight);
		Graphics offscreenGrfx = Graphics.FromImage(offscreenBitmap);
		// We blit the precalculated bitmap on the offscreen Graphics
		offscreenGrfx.DrawImage(Bitmap, 0, 0);

		if (NCoordX!=-1 && NCoordY!=-1 && (NCoordY*NColumns+NCoordX)<ImageList.Images.Count)
		{
			// We draw the selection rectangle
			offscreenGrfx.FillRectangle(new SolidBrush(BackgroundOverColor), NCoordX*NItemWidth + 1, NCoordY*NItemHeight + 1, NItemWidth-1, NItemHeight-1);
			if (BIsMouseDown)
			{
				// Mouse Down aspect for the image
				ImageList.Draw(offscreenGrfx,
					NCoordX*NItemWidth + NHSpace/2 + 1,
					NCoordY*NItemHeight + NVSpace/2 + 1,
					ImageList.ImageSize.Width,
					ImageList.ImageSize.Height,
					NCoordY*NColumns + NCoordX);
			}
			else
			{
				// Normal aspect for the image
				ImageList.Draw(offscreenGrfx,
					NCoordX*NItemWidth + NHSpace/2,
					NCoordY*NItemHeight + NVSpace/2,
					ImageList.ImageSize.Width,
					ImageList.ImageSize.Height,
					NCoordY*NColumns + NCoordX);
			}
			// Border selection Rectangle
			offscreenGrfx.DrawRectangle(new Pen(BorderColor), NCoordX*NItemWidth, NCoordY*NItemHeight, NItemWidth, NItemHeight);
		}

		// We blit the offscreen image on the screen
		grfx.DrawImage(offscreenBitmap, 0, 0);
			
		// Clean up
		offscreenGrfx.Dispose ();
	}
	#endregion
}

#region ImageListPanelEventArgs

public class ImageListPanelEventArgs : EventArgs
{      
	public int SelectedItem;
		
	public ImageListPanelEventArgs(int selectedItem)
	{
		SelectedItem = selectedItem;
	}
}

#endregion