using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace BB.Starter.UI.SplashScreen;

/// <summary>
/// Represents a Bordered label.
/// </summary>
public partial class BorderLabel : Label
{
    private float _borderSize;
    private Color _borderColor;

    private PointF _point;
    private SizeF _drawSize;
    private Pen _drawPen;
    private GraphicsPath _drawPath;
    private SolidBrush _forecolorBrush;

    #region Constructor
    /// <summary>
    ///   Constructs a new BorderLabel object.
    /// </summary>
    public BorderLabel()
    {
        _borderSize = 1f;
        _borderColor = Color.White;
        _drawPath = new GraphicsPath();
        _drawPen = new Pen(new SolidBrush(_borderColor), _borderSize);
        _forecolorBrush = new SolidBrush(ForeColor);

        Invalidate();
    }
    #endregion

    #region Public Properties

    /// <summary>
    ///   The border's thickness
    /// </summary>
    [Browsable(true)]
    [Category("Appearance")]
    [Description("The border's thickness")]
    [DefaultValue(1f)]
    public float BorderSize
    {
        get { return _borderSize; }
        set
        {
            _borderSize = value;
            if (value == 0)
            {
                //If border size equals zero, disable the
                // border by setting it as transparent
                _drawPen.Color = Color.Transparent;
            }
            else
            {
                _drawPen.Color = BorderColor;
                _drawPen.Width = value;
            }

            OnTextChanged(EventArgs.Empty);
        }
    }


    /// <summary>
    ///   The border color of this component
    /// </summary>
    [Browsable(true)]
    [Category("Appearance")]
    [DefaultValue(typeof(Color), "White")]
    [Description("The border color of this component")]
    public Color BorderColor
    {
        get { return _borderColor; }
        set
        {
            _borderColor = value;

            if (BorderSize != 0)
                _drawPen.Color = value;

            Invalidate();
        }
    }

    #endregion

    #region Public Methods
    /// <summary>
    ///   Releases all resources used by this control
    /// </summary>
    /// <param name="disposing">True to release both managed and unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {

            if (_forecolorBrush != null)
                _forecolorBrush.Dispose();

            if (_drawPath != null)
                _drawPath.Dispose();

            if (_drawPen != null)
                _drawPen.Dispose();

        }
        base.Dispose(disposing);
    }
    #endregion

    #region Event Handling
    protected override void OnFontChanged(EventArgs e)
    {
        base.OnFontChanged(e);
        Invalidate();
    }

    protected override void OnTextAlignChanged(EventArgs e)
    {
        base.OnTextAlignChanged(e);
        Invalidate();
    }

    protected override void OnTextChanged(EventArgs e)
    {
        base.OnTextChanged(e);
    }

    protected override void OnForeColorChanged(EventArgs e)
    {
        _forecolorBrush.Color = base.ForeColor;
        base.OnForeColorChanged(e);
        Invalidate();
    }
    #endregion

    #region Drawning
    protected override void OnPaint(PaintEventArgs e)
    {

        // First lets check if we indeed have text to draw.
        //  if we have no text, then we have nothing to do.
        if (Text.Length == 0)
            return;


        // Secondly, lets begin setting the smoothing mode to AntiAlias, to
        // reduce image sharpening and compositing quality to HighQuality,
        // to improve our drawnings and produce a better looking image.

        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        e.Graphics.CompositingQuality = CompositingQuality.HighQuality;



        // Next, we measure how much space our drawning will use on the control.
        //  this is important so we can determine the correct position for our text.
        _drawSize = e.Graphics.MeasureString(Text, Font, new PointF(), StringFormat.GenericTypographic);


            
        // Now, we can determine how we should align our text in the control
        //  area, both horizontally and vertically. If the control is set to auto
        //  size itselft, then it should be automatically drawn to the standard position.
            
        if (AutoSize)
        {
            _point.X = Padding.Left;
            _point.Y = Padding.Top;
        }
        else
        {
            // Text is Left-Aligned:
            if (TextAlign == ContentAlignment.TopLeft ||
                TextAlign == ContentAlignment.MiddleLeft ||
                TextAlign == ContentAlignment.BottomLeft)
                _point.X = Padding.Left;

            // Text is Center-Aligned
            else if (TextAlign == ContentAlignment.TopCenter ||
                     TextAlign == ContentAlignment.MiddleCenter ||
                     TextAlign == ContentAlignment.BottomCenter)
                _point.X = (Width - _drawSize.Width) / 2;

            // Text is Right-Aligned
            else _point.X = Width - (Padding.Right + _drawSize.Width);


            // Text is Top-Aligned
            if (TextAlign == ContentAlignment.TopLeft ||
                TextAlign == ContentAlignment.TopCenter ||
                TextAlign == ContentAlignment.TopRight)
                _point.Y = Padding.Top;

            // Text is Middle-Aligned
            else if (TextAlign == ContentAlignment.MiddleLeft ||
                     TextAlign == ContentAlignment.MiddleCenter ||
                     TextAlign == ContentAlignment.MiddleRight)
                _point.Y = (Height - _drawSize.Height) / 2;

            // Text is Bottom-Aligned
            else _point.Y = Height - (Padding.Bottom + _drawSize.Height);
        }



        // Now we can draw our text to a graphics path.
        //  
        //   PS: this is a tricky part: AddString() expects float emSize in pixel, but Font.Size
        //   measures it as points. So, we need to convert between points and pixels, which in
        //   turn requires detailed knowledge of the DPI of the device we are drawing on. 
        //
        //   The solution was to get the last value returned by the Graphics.DpiY property and
        //   divide by 72, since point is 1/72 of an inch, no matter on what device we draw.
        //
        //   The source of this solution can be seen on CodeProject's article
        //   'OSD window with animation effect' - http://www.codeproject.com/csharp/OSDwindow.asp 

        float fontSize = e.Graphics.DpiY * Font.SizeInPoints / 72;
            
        _drawPath.Reset();                                                                    
        _drawPath.AddString(Text, Font.FontFamily, (int)Font.Style, fontSize,
            _point, StringFormat.GenericTypographic);


        // And finally, using our pen, all we have to do now
        //  is draw our graphics path to the screen. Voilla!
        e.Graphics.FillPath(_forecolorBrush, _drawPath);
        e.Graphics.DrawPath(_drawPen, _drawPath);

    }
    #endregion
}