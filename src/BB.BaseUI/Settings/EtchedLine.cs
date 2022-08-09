using System.ComponentModel;

namespace BB.BaseUI.Settings;

/// <summary>
/// Summary description for EtchedLine.
/// </summary>
//[ToolboxItem(false)]
public class EtchedLine : DevExpress.XtraEditors.XtraUserControl
{
	/// <summary> 
	/// Required designer variable.
	/// </summary>
	private Container _components = null;

	public EtchedLine()
	{
		InitializeComponent();
	}

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

	protected override void OnPaint(PaintEventArgs e)
	{
		Graphics g = e.Graphics;
		Rectangle rect = ClientRectangle;

		Pen lightPen = new Pen(LightColor, 1.0F);
		Pen darkPen = new Pen(DarkColor, 1.0F);

		if (Dock == DockStyle.Top)
		{
			int y0 = rect.Top;
			int y1 = rect.Top + 1;

			g.DrawLine(darkPen, rect.Left, y0, rect.Right, y0);
			g.DrawLine(lightPen, rect.Left, y1, rect.Right, y1);
		}
		else if (Dock == DockStyle.Bottom)
		{
			int y0 = rect.Bottom - 2;
			int y1 = rect.Bottom - 1;

			g.DrawLine(darkPen, rect.Left, y0, rect.Right, y0);
			g.DrawLine(lightPen, rect.Left, y1, rect.Right, y1);
		}

		base.OnPaint(e);
	}

	Color _lightColor = SystemColors.ControlLightLight;
	Color _darkColor = SystemColors.ControlDark;

	[ Category("Appearance") ]
	public Color LightColor
	{
		get => _lightColor;
		set => _lightColor = value;
	}

	[ Category("Appearance") ]
	public Color DarkColor
	{
		get => _darkColor;
		set => _darkColor = value;
	}
}