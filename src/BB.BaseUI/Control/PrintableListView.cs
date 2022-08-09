using System.ComponentModel;
using System.Drawing.Printing;

namespace BB.BaseUI.Control;

/// <summary>
/// 可打印ListView控件
/// </summary>
public class PrintableListView : ListView
{
	// Print fields
	private PrintDocument _mPrintDoc = new PrintDocument();
	private PrintPreviewDialog _mPreviewDlg = new PrintPreviewDialog();
	private PrintDialog        _mPrintDlg   = new PrintDialog();

	private int _mNPageNumber=1;
	private int _mNStartRow=0;
	private int _mNStartCol=0;

	private bool _mBPrintSel=false;
	private bool _mBFitToPage=false;

	private float _mFListWidth=0.0f;
	private float[] _mArColsWidth;

	private float _mFDpi=96.0f;
	private string _mStrTitle="";
	private string _mBottomTitle = "";

	/// <summary>
	/// Required designer variable.
	/// </summary>
	private Container _components = null;

	#region Properties
	/// <summary>
	///		Gets or sets whether to fit the list width on a single page
	/// </summary>
	/// <value>
	///		<c>True</c> if you want to scale the list width so it will fit on a single page.
	/// </value>
	/// <remarks>
	///		If you choose false (the default value), and the list width exceeds the page width, the list
	///		will be broken in multiple page.
	/// </remarks>
	public bool FitToPage
	{
		get => _mBFitToPage;
		set => _mBFitToPage=value;
	}

	/// <summary>
	///		Gets or sets the title to dispaly as page header in the printed list
	/// </summary>
	/// <value>
	///		A <see cref="string"/> the represents the title printed as page header.
	/// </value>
	public string Title
	{
		get => _mStrTitle;
		set => _mStrTitle=value;
	}
        
	public string BottomTitle
	{
		get => _mBottomTitle;
		set => _mBottomTitle = value;
	}

	public PrintDocument PrintDoc
	{
		get => _mPrintDoc;
		set => _mPrintDoc = value;
	}

	#endregion

	/// <summary>
	/// 构造函数
	/// </summary>
	public PrintableListView()
	{
		// This call is required by the Windows.Forms Form Designer.
		InitializeComponent();

		_mPrintDoc.BeginPrint += OnBeginPrint;
		_mPrintDoc.PrintPage += OnPrintPage;

		_mPreviewDlg.Document = _mPrintDoc;
		_mPrintDlg.Document   = _mPrintDoc;
			
		_mPrintDlg.AllowSomePages = false;
	}

	/// <summary>
	/// Clean up any resources being used.
	/// </summary>
	protected override void Dispose( bool disposing )
	{
		if( disposing )
		{
			if( _components != null )
				_components.Dispose();
		}
		base.Dispose( disposing );
	}

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

	/// <summary>
	///		Show the standard page setup dialog box that lets the user specify
	///		margins, page orientation, page sources, and paper sizes.
	/// </summary>
	public DialogResult PageSetup()
	{
		PageSetupDialog mSetupDlg = new PageSetupDialog();
		mSetupDlg.Document = _mPrintDoc;

		mSetupDlg.PageSettings.Margins = PrinterUnitConvert.Convert(mSetupDlg.PageSettings.Margins,
			PrinterUnit.ThousandthsOfAnInch, PrinterUnit.HundredthsOfAMillimeter);
		DialogResult result = mSetupDlg.ShowDialog();
		if (result == DialogResult.OK)
		{
			Print();
		}
		else
		{
			mSetupDlg.PageSettings.Margins = PrinterUnitConvert.Convert(mSetupDlg.PageSettings.Margins,
				PrinterUnit.HundredthsOfAMillimeter, PrinterUnit.ThousandthsOfAnInch);
		}
		return result;
	}

	/// <summary>
	///		Show the standard print preview dialog box.
	/// </summary>
	public void PrintPreview()
	{
		PrintDoc.DocumentName = "List View";

		_mNPageNumber = 1;
		_mBPrintSel	  = false;

		_mPreviewDlg.ShowDialog(this);
	}

	/// <summary>
	///		Start the print process.
	/// </summary>
	public void Print()
	{
		_mPrintDlg.AllowSelection = SelectedItems.Count>0;

		// Show the standard print dialog box, that lets the user select a printer
		// and change the settings for that printer.
		if (_mPrintDlg.ShowDialog(this) == DialogResult.OK)
		{
			PrintDoc.DocumentName = _mStrTitle;
			_mBPrintSel = _mPrintDlg.PrinterSettings.PrintRange==PrintRange.Selection;
			_mNPageNumber = 1;

			// Start print
			PrintDoc.Print();
		}
	}

	#region Events Handlers
	private void OnBeginPrint(object sender, PrintEventArgs e)
	{
		PreparePrint();
	}

	private void OnPrintPage(object sender, PrintPageEventArgs e)
	{
		int nNumItems = GetItemsCount();  // Number of items to print

		if (nNumItems==0 || _mNStartRow>=nNumItems)
		{
			e.HasMorePages = false;
			return;
		}

		int nNextStartCol = 0; 			  // First column exeeding the page width
		float x = 0.0f;					  // Current horizontal coordinate
		float y = 0.0f;					  // Current vertical coordinate
		float cx = 4.0f;                  // The horizontal space, in hundredths of an inch,
		// of the padding between items text and
		// their cell boundaries.
		float fScale = 1.0f;              // Scale factor when fit to page is enabled
		float fRowHeight = 0.0f;		  // The height of the current row
		float fColWidth  = 0.0f;		  // The width of the current column

		RectangleF rectFull;			  // The full available space
		RectangleF rectBody;			  // Area for the list items

		bool bUnprintable = false;

		Graphics g = e.Graphics;

		if (g.VisibleClipBounds.X<0)	// Print preview
		{
			rectFull = e.MarginBounds;

			// Convert to hundredths of an inch
			rectFull = new RectangleF(rectFull.X/_mFDpi*100.0f,
				rectFull.Y/_mFDpi*100.0f,
				rectFull.Width/_mFDpi*100.0f,
				rectFull.Height/_mFDpi*100.0f);
		}
		else							// Print
		{
			// Printable area (approximately) of the page, taking into account the user margins
			rectFull = new RectangleF(
				e.MarginBounds.Left - (e.PageBounds.Width  - g.VisibleClipBounds.Width)/2,
				e.MarginBounds.Top  - (e.PageBounds.Height - g.VisibleClipBounds.Height)/2,
				e.MarginBounds.Width,
				e.MarginBounds.Height);
		}

		rectBody = RectangleF.Inflate(rectFull, 0, -3*Font.GetHeight(g));

		// Display title at top
		StringFormat sfmt = new StringFormat();
		sfmt.Alignment = StringAlignment.Center;
		g.DrawString(_mStrTitle, Font, Brushes.Black, rectFull, sfmt);

		// Display page number at bottom
		sfmt.LineAlignment = StringAlignment.Far;
		g.DrawString("ҳ " + _mNPageNumber, Font, Brushes.Black, rectFull, sfmt);

		if (_mNStartCol==0 && _mBFitToPage && _mFListWidth>rectBody.Width)
		{
			// Calculate scale factor
			fScale = rectBody.Width / _mFListWidth;
		}

		// Scale the printable area
		rectFull = new RectangleF(rectFull.X/fScale, 
			rectFull.Y/fScale,
			rectFull.Width/fScale, 
			rectFull.Height/fScale);

		rectBody = new RectangleF(rectBody.X/fScale, 
			rectBody.Y/fScale,
			rectBody.Width/fScale, 
			rectBody.Height/fScale);

		// Setting scale factor and unit of measure
		g.ScaleTransform(fScale, fScale);
		g.PageUnit = GraphicsUnit.Inch;
		g.PageScale = 0.01f;

		// Start print
		nNextStartCol=0;
		y = rectBody.Top;

		// Columns headers ----------------------------------------
		Brush brushHeader = new SolidBrush(Color.LightGray);
		Font fontHeader = new Font(Font, FontStyle.Bold);
		fRowHeight = fontHeader.GetHeight(g)*3.0f;
		x = rectBody.Left;

		for (int i=_mNStartCol; i<Columns.Count; i++)
		{
			ColumnHeader ch = Columns[i];
			fColWidth = _mArColsWidth[i];

			if ( (x + fColWidth) <= rectBody.Right)
			{
				// Rectangle
				g.FillRectangle(brushHeader, x, y, fColWidth, fRowHeight);
				g.DrawRectangle(Pens.Black, x, y, fColWidth, fRowHeight);

				// Text
				StringFormat sf = new StringFormat();
				if (ch.TextAlign == HorizontalAlignment.Left)
					sf.Alignment = StringAlignment.Near;
				else if (ch.TextAlign == HorizontalAlignment.Center)
					sf.Alignment = StringAlignment.Center;
				else
					sf.Alignment = StringAlignment.Far;

				sf.LineAlignment = StringAlignment.Center;
				sf.FormatFlags = StringFormatFlags.NoWrap;
				sf.Trimming = StringTrimming.EllipsisCharacter;

				RectangleF rectText = new RectangleF(x+cx, y, fColWidth-1-2*cx, fRowHeight);
				g.DrawString(ch.Text, fontHeader, Brushes.Black, rectText, sf);
				x += fColWidth;
			}
			else
			{
				if (i==_mNStartCol)
					bUnprintable=true;

				nNextStartCol=i;
				break;
			}
		}
		y += fRowHeight;

		// Rows ---------------------------------------------------
		int nRow = _mNStartRow;
		bool bEndOfPage = false;
		while (!bEndOfPage && nRow<nNumItems)
		{
			ListViewItem item = GetItem(nRow);

			fRowHeight = item.Bounds.Height/_mFDpi*100.0f + 5.0f;

			if (y+fRowHeight>rectBody.Bottom)
			{
				bEndOfPage=true;
			}
			else
			{
				x = rectBody.Left;

				for (int i=_mNStartCol; i<Columns.Count; i++)
				{
					ColumnHeader ch = Columns[i];
					fColWidth = _mArColsWidth[i];

					if ( (x + fColWidth) <= rectBody.Right)
					{
						// Rectangle
						g.DrawRectangle(Pens.Black, x, y, fColWidth, fRowHeight);

						// Text
						StringFormat sf = new StringFormat();
						if (ch.TextAlign == HorizontalAlignment.Left)
							sf.Alignment = StringAlignment.Near;
						else if (ch.TextAlign == HorizontalAlignment.Center)
							sf.Alignment = StringAlignment.Center;
						else
							sf.Alignment = StringAlignment.Far;

						sf.LineAlignment = StringAlignment.Center;
						sf.FormatFlags = StringFormatFlags.NoWrap;
						sf.Trimming = StringTrimming.EllipsisCharacter;

						// Text
						string strText = i==0 ? item.Text : item.SubItems[i].Text;
						Font font = i==0 ? item.Font : item.SubItems[i].Font;

						RectangleF rectText = new RectangleF(x+cx, y, fColWidth-1-2*cx, fRowHeight);
						g.DrawString(strText, font, Brushes.Black, rectText, sf);
						x += fColWidth;
					}
					else
					{
						nNextStartCol=i;
						break;
					}
				}

				y += fRowHeight;
				nRow++;
			}
		}

		RectangleF rectText2 = new RectangleF(rectBody.Left, y + 20, rectFull.Width, fRowHeight*4);
		StringFormat sf2 = new StringFormat();
		sf2.Alignment = StringAlignment.Near;
		sf2.LineAlignment = StringAlignment.Center;
		sf2.FormatFlags = StringFormatFlags.NoWrap;
		g.DrawString(BottomTitle, Font, Brushes.Black, rectText2, sf2);

		if (nNextStartCol==0)
			_mNStartRow = nRow;

		_mNStartCol = nNextStartCol;

		_mNPageNumber++;

		e.HasMorePages = (!bUnprintable && (_mNStartRow>0 && _mNStartRow<nNumItems) || _mNStartCol>0);

		if (!e.HasMorePages)
		{
			_mNPageNumber=1;
			_mNStartRow=0;
			_mNStartCol=0;
		}

		brushHeader.Dispose();
	}
	#endregion

	private int GetItemsCount()
	{
		return _mBPrintSel ? SelectedItems.Count : Items.Count;
	}

	private ListViewItem GetItem(int index)
	{
		return _mBPrintSel ? SelectedItems[index] : Items[index];
	}

	private void PreparePrint()
	{
		// Gets the list width and the columns width in units of hundredths of an inch.
		_mFListWidth = 0.0f;
		_mArColsWidth = new float[Columns.Count];

		Graphics g = CreateGraphics();
		_mFDpi = g.DpiX;
		g.Dispose();

		for (int i=0; i<Columns.Count; i++)
		{
			ColumnHeader ch = Columns[i];
			float fWidth = ch.Width/_mFDpi*100 + 1; // Column width + separator
			_mFListWidth += fWidth;
			_mArColsWidth[i] = fWidth;
		}
		_mFListWidth += 1; // separator
	}

}