﻿using System.ComponentModel;
using System.Drawing.Printing;
using BB.BaseUI.Other;
using BB.Tools.MultiLanuage;

namespace BB.BaseUI.Print;

/// <summary>
/// Represents a dialog containing a <see cref="PrintDocument"/> control
/// used to preview and print <see cref="PrintPreviewDialog"/> objects.
/// </summary>
/// <remarks>
/// This dialog is similar to the standard <see cref="ToolStrip"/>
/// but provides additional options such printer and page setup buttons,
/// a better UI based on the <see cref="CoolPrintPreviewControl"/> control, and built-in
/// PDF export.
/// </remarks>
public partial class CoolPrintPreviewDialog : Form
{
    PrintDocument _doc;

    /// <summary>
    /// Initializes a new instance of a <see cref="CoolPrintPreviewDialog"/>.
    /// </summary>
    public CoolPrintPreviewDialog() : this(null)
    {
    }

    /// <summary>
    /// Initializes a new instance of a <see cref="CoolPrintPreviewDialog"/>.
    /// </summary>
    /// <param name="parentForm">Parent form that defines the initial size for this dialog.</param>
    public CoolPrintPreviewDialog(System.Windows.Forms.Control parentForm)
    {
        InitializeComponent();
        if (parentForm != null)
        {
            Size = parentForm.Size;
        }
    }

    /// <summary>
    /// Gets or sets the <see cref="PrintDocument"/> to preview.
    /// </summary>
    public PrintDocument Document
    {
        get => _doc;
        set
        {
            // unhook event handlers
            if (_doc != null)
            {
                _doc.BeginPrint -= _doc_BeginPrint;
                _doc.EndPrint -= _doc_EndPrint;
            }

            // save the value
            _doc = value;

            // hook up event handlers
            if (_doc != null)
            {
                _doc.BeginPrint += _doc_BeginPrint;
                _doc.EndPrint += _doc_EndPrint;
            }


            // don't assign document to preview until this form becomes visible
            if (Visible)
            {
                _preview.Document = Document;
            }
        }
    }

    #region ** overloads

    /// <summary>
    /// Overridden to assign document to preview control only after the 
    /// initial activation.
    /// </summary>
    /// <param name="e"><see cref="EventArgs"/> that contains the event data.</param>
    protected override void OnShown(EventArgs e)
    {
        base.OnShown(e);
        _preview.Document = Document;
    }
    /// <summary>
    /// Overridden to cancel any ongoing previews when closing form.
    /// </summary>
    /// <param name="e"><see cref="FormClosingEventArgs"/> that contains the event data.</param>
    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        base.OnFormClosing(e);
        if (_preview.IsRendering && !e.Cancel)
        {
            _preview.Cancel();
        }
    }

    #endregion

    #region ** main commands

    void _btnPrint_Click(object sender, EventArgs e)
    {
        using (var dlg = new PrintDialog())
        {
            // configure dialog
            dlg.AllowSomePages = true;
            dlg.AllowSelection = true;
            dlg.UseEXDialog = true;
            dlg.Document = Document;

            // show allowed page range
            var ps = dlg.PrinterSettings;
            ps.MinimumPage = ps.FromPage = 1;
            ps.MaximumPage = ps.ToPage = _preview.PageCount;

            // show dialog
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                // print selected page range
                _preview.Print();
            }
        }
    }
    void _btnPageSetup_Click(object sender, EventArgs e)
    {
        using (var dlg = new PageSetupDialog())
        {
            dlg.Document = Document;
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                // to show new page layout
                _preview.RefreshPreview();
            }
        }
    }

    #endregion

    #region ** zoom

    void _btnZoom_ButtonClick(object sender, EventArgs e)
    {
        _preview.ZoomMode = _preview.ZoomMode == ZoomMode.ActualSize
            ? ZoomMode.FullPage
            : ZoomMode.ActualSize;
    }
    void _btnZoom_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
    {
        if (e.ClickedItem == _itemActualSize)
        {
            _preview.ZoomMode = ZoomMode.ActualSize;
        }
        else if (e.ClickedItem == _itemFullPage)
        {
            _preview.ZoomMode = ZoomMode.FullPage;
        }
        else if (e.ClickedItem == _itemPageWidth)
        {
            _preview.ZoomMode = ZoomMode.PageWidth;
        }
        else if (e.ClickedItem == _itemTwoPages)
        {
            _preview.ZoomMode = ZoomMode.TwoPages;
        }
        if (e.ClickedItem == _item10)
        {
            _preview.Zoom = .1;
        }
        else if (e.ClickedItem == _item100)
        {
            _preview.Zoom = 1;
        }
        else if (e.ClickedItem == _item150)
        {
            _preview.Zoom = 1.5;
        }
        else if (e.ClickedItem == _item200)
        {
            _preview.Zoom = 2;
        }
        else if (e.ClickedItem == _item25)
        {
            _preview.Zoom = .25;
        }
        else if (e.ClickedItem == _item50)
        {
            _preview.Zoom = .5;
        }
        else if (e.ClickedItem == _item500)
        {
            _preview.Zoom = 5;
        }
        else if (e.ClickedItem == _item75)
        {
            _preview.Zoom = .75;
        }
    }
    #endregion

    #region ** page navigation

    void _btnFirst_Click(object sender, EventArgs e)
    {
        _preview.StartPage = 0;
    }
    void _btnPrev_Click(object sender, EventArgs e)
    {
        _preview.StartPage--;
    }
    void _btnNext_Click(object sender, EventArgs e)
    {
        _preview.StartPage++;
    }
    void _btnLast_Click(object sender, EventArgs e)
    {
        _preview.StartPage = _preview.PageCount - 1;
    }
    void _txtStartPage_Enter(object sender, EventArgs e)
    {
        _txtStartPage.SelectAll();
    }
    void _txtStartPage_Validating(object sender, CancelEventArgs e)
    {
        CommitPageNumber();
    }
    void _txtStartPage_KeyPress(object sender, KeyPressEventArgs e)
    {
        var c = e.KeyChar;
        if (c == (char)13)
        {
            CommitPageNumber();
            e.Handled = true;
        }
        else if (c > ' ' && !char.IsDigit(c))
        {
            e.Handled = true;
        }
    }
    void CommitPageNumber()
    {
        int page;
        if (int.TryParse(_txtStartPage.Text, out page))
        {
            _preview.StartPage = page - 1;
        }
    }
    void _preview_StartPageChanged(object sender, EventArgs e)
    {
        var page = _preview.StartPage + 1;
        _txtStartPage.Text = page.ToString();
    }
    private void _preview_PageCountChanged(object sender, EventArgs e)
    {
        Update();
        Application.DoEvents();
        _lblPageCount.Text = $"/ {_preview.PageCount}";
    }

    #endregion

    #region ** job control

    void _btnCancel_Click(object sender, EventArgs e)
    {
        if (_preview.IsRendering)
        {
            _preview.Cancel();
        }
        else
        {
            Close();
        }
    }
    void _doc_BeginPrint(object sender, PrintEventArgs e)
    {
        _btnCancel.Text = JsonLanguage.Default.GetString("取消(&C)");
        _btnPrint.Enabled = _btnPageSetup.Enabled = false;
    }
    void _doc_EndPrint(object sender, PrintEventArgs e)
    {
        _btnCancel.Text = JsonLanguage.Default.GetString("关闭(&C)");
        _btnPrint.Enabled = _btnPageSetup.Enabled = true;
    }

    #endregion

    private void CoolPrintPreviewDialog_Shown(object sender, EventArgs e)
    {
        if (!DesignMode)
        {
            LanguageHelper.InitLanguage(this);
        }
    }
}

/// <summary>
/// This version of the PageImageList is a simple List<Image>. It is simple,
/// but caches one image (GDI object) per preview page.
/// </summary>
public class PageImageList : List<System.Drawing.Image>
{
}