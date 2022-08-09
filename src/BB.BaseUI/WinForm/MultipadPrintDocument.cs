using System.Drawing.Printing;
using System.Text;

namespace BB.BaseUI.WinForm;

/// <summary>
/// USB方式POS打印辅助类
/// </summary>
public class MultipadPrintDocument : PrintDocument
{
    String _text = "";
    Font _font = null;
    Int32 _offset = 0;
    Int32 _pageno = 0;

    /// <summary>
    /// 构造函数
    /// </summary>
    public MultipadPrintDocument()
    {
    }

    /// <summary>
    /// 打印的文本内容
    /// </summary>
    public String Text
    {
        get => _text;
        set => _text = value;
    }

    /// <summary>
    /// 打印的字体
    /// </summary>
    public Font Font
    {
        get => _font;
        set => _font = value;
    }

    protected override void OnBeginPrint(PrintEventArgs e)
    {
        _offset = 0;
        _pageno = 1;

        base.OnBeginPrint(e);
    }

    Boolean NextCharIsNewLine()
    {
        Int32 nl = Environment.NewLine.Length;
        Int32 tl = _text.Length - _offset;

        if (tl < nl) return false;

        String newline = Environment.NewLine;

        for (Int32 i = 0; i < nl; i++)
        {
            if (_text[_offset + i] != newline[i])
                return false;
        }

        return true;
    }

    const Int32 Eos = -1;
    const Int32 NewLine = -2;

    Int32 NextChar()
    {
        if (_offset >= _text.Length)
            return -1;

        if (NextCharIsNewLine())
        {
            _offset += Environment.NewLine.Length;
            return -2;
        }

        return (Int32)_text[_offset++];
    }

    /// <summary>
    /// 打印页
    /// </summary>
    /// <param name="e"></param>
    protected override void OnPrintPage(PrintPageEventArgs e)
    {
        base.OnPrintPage(e);

        Single pagewidth = e.MarginBounds.Width * 3.0f;
        Single pageheight = e.MarginBounds.Height * 3.0f;

        Single textwidth = 0.0f;
        Single textheight = 0.0f;

        Single offsetx = e.MarginBounds.Left * 3.0f;
        Single offsety = e.MarginBounds.Top * 3.0f;

        Single x = offsetx;
        Single y = offsety;

        StringBuilder line = new StringBuilder(256);
        StringFormat sf = StringFormat.GenericTypographic;
        sf.FormatFlags = StringFormatFlags.DisplayFormatControl;
        sf.SetTabStops(0.0f, new[] { 300.0f });

        RectangleF r;

        Graphics g = e.Graphics;
        g.PageUnit = GraphicsUnit.Document;

        SizeF size = g.MeasureString("X", _font, 1, sf);
        Single lineheight = size.Height;

        // make sure we can print at least 1 line (font too big?)
        if (lineheight + (lineheight * 3) > pageheight)
        {
            // cannot print at least 1 line and footer
            g.Dispose();

            e.HasMorePages = false;

            return;
        }

        // don't include footer
        //pageheight -= lineheight * 3;

        // last whitespace in line buffer
        Int32 lastws = -1;

        // next character
        Int32 c = Eos;

        for (; ; )
        {

            // get next character
            c = NextChar();

            // append c to line if not NewLine or Eos
            if ((c != NewLine) && (c != Eos))
            {
                Char ch = Convert.ToChar(c);
                line.Append(ch);

                // if ch is whitespace, remember pos and continue
                if (ch == ' ' || ch == '\t')
                {
                    lastws = line.Length - 1;
                    continue;
                }
            }

            // measure string if line is not empty
            if (line.Length > 0)
            {
                size = g.MeasureString(line.ToString(), _font, Int32.MaxValue,
                    StringFormat.GenericTypographic);
                textwidth = size.Width;
            }

            // draw line if line is full, if NewLine or if last line
            if (c == Eos || (textwidth > pagewidth) || (c == NewLine))
            {
                if (textwidth > pagewidth)
                {
                    if (lastws != -1)
                    {
                        _offset -= line.Length - lastws - 1;
                        line.Length = lastws + 1;
                    }
                    else
                    {
                        line.Length--;
                        _offset--;
                    }
                }

                // there's something to draw
                if (line.Length > 0)
                {
                    r = new RectangleF(x, y, pagewidth, lineheight);
                    sf.Alignment = StringAlignment.Near;
                    g.DrawString(line.ToString(), _font, Brushes.Black, r, sf);
                }

                // increase ypos
                y += lineheight;
                textheight += lineheight;

                // empty line buffer
                line.Length = 0;
                textwidth = 0.0f;
                lastws = -1;
            }

            // if next line doesn't fit on page anymore, exit loop
            if (textheight > (pageheight - lineheight))
                break;

            if (c == Eos)
                break;
        }

        // print footer
        //x = offsetx;
        //y = offsety + pageheight + (lineheight * 2);
        //r = new RectangleF(x, y, pagewidth, lineheight);
        //sf.Alignment = StringAlignment.Center;
        //g.DrawString(_pageno.ToString(), _font, Brushes.Black, r, sf);

        g.Dispose();

        _pageno++;

        e.HasMorePages = (c != Eos);
    }

    protected override void OnEndPrint(PrintEventArgs e)
    {
        base.OnEndPrint(e);
    }

}