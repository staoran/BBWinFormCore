using System.Collections;
using BB.BaseUI.Extension;
using BB.BaseUI.Print;
using DevExpress.Utils.Drawing.Helpers;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;

namespace BB.BaseUI.Other;

/// <summary>
/// DataGrid打印辅助类
/// </summary>
internal class PrintGV
    {
        private static StringFormat StrFormat;  // Holds content of a TextBox Cell to write by DrawString
        private static StringFormat StrFormatComboBox; // Holds content of a Boolean Cell to write by DrawImage
        private static Button CellButton;       // Holds the Contents of Button Cell
        private static CheckBox CellCheckBox;   // Holds the Contents of CheckBox Cell 
        private static ComboBox CellComboBox;   // Holds the Contents of ComboBox Cell

        private static int TotalWidth;          // Summation of Columns widths
        private static int RowPos;              // Position of currently printing row 
        private static bool NewPage;            // Indicates if a new page reached
        private static int PageNo;              // Number of pages to print
        private static ArrayList ColumnLefts = new ArrayList();  // Left Coordinate of Columns
        private static ArrayList ColumnWidths = new ArrayList(); // Width of Columns
        private static ArrayList ColumnTypes = new ArrayList();  // DataType of Columns
        private static int CellHeight;          // Height of DataGrid Cell
        private static int RowsPerPage;         // Number of Rows per Page
        private static System.Drawing.Printing.PrintDocument printDoc =
                       new System.Drawing.Printing.PrintDocument();  // PrintDocumnet Object used for printing

        private static string PrintTitle = "";  // Header of pages
        private static GridView dgv;        // Holds GridView Object to print its contents
        private static List<string> SelectedColumns = new List<string>();   // The Columns Selected by user to print.
        private static List<string> AvailableColumns = new List<string>();  // All Columns avaiable in DataGrid 
        private static bool PrintAllRows = true;   // True = print all rows,  False = print selected rows    
        private static bool FitToPageWidth = true; // True = Fits selected columns to page width ,  False = Print columns as showed    
        private static int HeaderHeight = 0;

        public static void Print_GridView(GridView dgv1)
        {
            Print_GridView(dgv1, "");
        }

        public static void Print_GridView(GridView dgv1, string title)
        {
            CoolPrintPreviewDialog ppvw;
            try 
	        {	
                // Getting GridView object to print
                dgv = dgv1;
                PrintTitle = title;

                // Getting all Coulmns Names in the GridView
                AvailableColumns.Clear();
                foreach (GridColumn c in dgv.Columns)
                {
                    if (!c.Visible) continue;
                    AvailableColumns.Add(c.Caption);
                }

                // Showing the PrintOption Form
                PrintOptions dlg = new PrintOptions(AvailableColumns);

                //加载注册表中的内容
                string itemNameInRegister = dgv.GetType().GUID.ToString();
                string checkItems = RegistryHelper.GetValue(itemNameInRegister);
                if (!string.IsNullOrEmpty(checkItems))
                {
                    string[] items = checkItems.Split(',');
                    dlg.SetCheckedItems(items);
                }
                dlg.PrintTitle = PrintTitle;//先赋值给对话框

                if (dlg.ShowDialog() != DialogResult.OK) return;
                
                //保存选项内容
                string selectedString = "";
                List<string> selectedItems = dlg.GetCheckItems();
                foreach (string item in selectedItems)
                {
                    selectedString += string.Format("{0},", item);
                }
                selectedString = selectedString.Trim(',');
                RegistryHelper.SaveValue(itemNameInRegister, selectedString);

                PrintTitle = dlg.PrintTitle;
                PrintAllRows = dlg.PrintAllRows;
                FitToPageWidth = dlg.FitToPageWidth;
                SelectedColumns = dlg.GetSelectedColumns();

                RowsPerPage = 0;

                ppvw = new CoolPrintPreviewDialog();
                ppvw.Document = printDoc;

                // Showing the Print Preview Page
                printDoc.BeginPrint +=new System.Drawing.Printing.PrintEventHandler(PrintDoc_BeginPrint);
                printDoc.PrintPage +=new System.Drawing.Printing.PrintPageEventHandler(PrintDoc_PrintPage);
                if (ppvw.ShowDialog() != DialogResult.OK)
                {
                    printDoc.BeginPrint -= new System.Drawing.Printing.PrintEventHandler(PrintDoc_BeginPrint);
                    printDoc.PrintPage -= new System.Drawing.Printing.PrintPageEventHandler(PrintDoc_PrintPage);
                    return;
                }

                // Printing the Documnet
                printDoc.Print();
                printDoc.BeginPrint -= new System.Drawing.Printing.PrintEventHandler(PrintDoc_BeginPrint);
                printDoc.PrintPage -= new System.Drawing.Printing.PrintPageEventHandler(PrintDoc_PrintPage);
	        }
	        catch (Exception ex)
	        {
                ex.Message.ShowUxError();
	        }
            finally
            {

            }
        }

        private static void PrintDoc_BeginPrint(object sender, 
                    System.Drawing.Printing.PrintEventArgs e) 
        {
            try
	        {
                // Formatting the Content of Text Cell to print
                StrFormat = new StringFormat();
                StrFormat.Alignment = StringAlignment.Near;
                StrFormat.LineAlignment = StringAlignment.Center;
                StrFormat.Trimming = StringTrimming.EllipsisCharacter;

                // Formatting the Content of Combo Cells to print
                StrFormatComboBox = new StringFormat();
                StrFormatComboBox.LineAlignment = StringAlignment.Center;
                StrFormatComboBox.FormatFlags = StringFormatFlags.NoWrap;
                StrFormatComboBox.Trimming = StringTrimming.EllipsisCharacter;

                ColumnLefts.Clear();
                ColumnWidths.Clear();
                ColumnTypes.Clear();
                CellHeight = 0;
                RowsPerPage = 0;

                // For various column types
                CellButton = new Button();
                CellCheckBox = new CheckBox();
                CellComboBox = new ComboBox();

                // Calculating Total Widths
                TotalWidth = 0;
                foreach (GridColumn GridCol in dgv.Columns)
                {
                    if (!GridCol.Visible) continue;
                    if (!SelectedColumns.Contains(GridCol.Caption)) continue;
                    TotalWidth += GridCol.Width;
                }
                PageNo = 1;
                NewPage = true;
                RowPos = 0;        		
	        }
	        catch (Exception ex)
	        {
                ex.Message.ShowUxError();
	        }
        }

        private static bool CheckInArray(int rowHandle)
        {
            bool result = false;
            int[] selectedRow = dgv.GetSelectedRows();
            if (selectedRow != null)
            {
                foreach (int row in selectedRow)
                {
                    if (row == rowHandle)
                    {
                        return true;
                    }
                }
            }
            return result;
        }

        private static void PrintDoc_PrintPage(object sender, 
                    System.Drawing.Printing.PrintPageEventArgs e) 
        {
            int tmpWidth, i;
            int tmpTop = e.MarginBounds.Top;
            int tmpLeft = e.MarginBounds.Left;

            try 
	        {	        
                // Before starting first page, it saves Width & Height of Headers and CoulmnType
                if (PageNo == 1) 
                {
                    #region 第一页
                    foreach (GridColumn GridCol in dgv.Columns)
                    {
                        if (!GridCol.Visible) continue;
                        // Skip if the current column not selected
                        if (!SelectedColumns.Contains(GridCol.Caption)) continue;

                        // Detemining whether the columns are fitted to page or not.
                        if (FitToPageWidth)
                        {
                            tmpWidth = (int)(Math.Floor((double)((double)GridCol.Width /
                                       (double)TotalWidth * (double)TotalWidth *
                                       ((double)e.MarginBounds.Width / (double)TotalWidth))));
                        }
                        else
                        {
                            tmpWidth = GridCol.Width;
                        }

                        HeaderHeight = (int)(e.Graphics.MeasureString(GridCol.Caption,
                                    GridCol.AppearanceCell.Font, tmpWidth).Height) + 15;

                        // Save width & height of headres and ColumnType
                        ColumnLefts.Add(tmpLeft);
                        ColumnWidths.Add(tmpWidth);
                        ColumnTypes.Add(GridCol.ColumnType);
                        tmpLeft += tmpWidth;
                    } 
                    #endregion
                }

                // Printing Current Page, Row by Row
                while (RowPos <= dgv.RowCount - 1)
                {
                    if ((!PrintAllRows && !CheckInArray(RowPos)))
                    {
                        RowPos++;
                        continue;
                    }
                    
                    CellHeight = dgv.RowHeight + 40;
                    if (tmpTop + CellHeight >= e.MarginBounds.Height + e.MarginBounds.Top)
                    {
                        DrawFooter(e, RowsPerPage);
                        NewPage = true;
                        PageNo++;
                        e.HasMorePages = true;
                        return;
                    }
                    else
                    {
                        #region MyRegion
                        if (NewPage)
                        {
                            // Draw Header
                            e.Graphics.DrawString(PrintTitle, new Font(dgv.GridControl.Font, FontStyle.Bold),
                                    Brushes.Black, /*e.MarginBounds.Left*/e.MarginBounds.Width / 2, e.MarginBounds.Top -
                            e.Graphics.MeasureString(PrintTitle, new Font(dgv.GridControl.Font,
                                    FontStyle.Bold), e.MarginBounds.Width).Height - 13);

                            String s = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToShortTimeString();

                            e.Graphics.DrawString(s, new Font(dgv.GridControl.Font, FontStyle.Bold),
                                    Brushes.Black, e.MarginBounds.Left + (e.MarginBounds.Width -
                                    e.Graphics.MeasureString(s, new Font(dgv.GridControl.Font,
                                    FontStyle.Bold), e.MarginBounds.Width).Width), e.MarginBounds.Top -
                                    e.Graphics.MeasureString(PrintTitle, new Font(new Font(dgv.GridControl.Font,
                                    FontStyle.Bold), FontStyle.Bold), e.MarginBounds.Width).Height - 13);

                            // Draw Columns
                            tmpTop = e.MarginBounds.Top;
                            i = 0;
                            foreach (GridColumn GridCol in dgv.Columns)
                            {
                                if (!GridCol.Visible) continue;
                                if (!SelectedColumns.Contains(GridCol.Caption))
                                    continue;

                                e.Graphics.FillRectangle(new SolidBrush(Color.LightGray),
                                    new Rectangle((int)ColumnLefts[i], tmpTop,
                                    (int)ColumnWidths[i], HeaderHeight));

                                e.Graphics.DrawRectangle(Pens.Black,
                                    new Rectangle((int)ColumnLefts[i], tmpTop,
                                    (int)ColumnWidths[i], HeaderHeight));

                                e.Graphics.DrawString(GridCol.Caption, GridCol.AppearanceCell.Font,
                                    new SolidBrush(Color.Black),
                                    new RectangleF((int)ColumnLefts[i], tmpTop,
                                    (int)ColumnWidths[i], HeaderHeight), StrFormat);
                                i++;
                            }
                            NewPage = false;
                            tmpTop += HeaderHeight;
                        } 
                        #endregion

                        #region MyRegion
                        // Draw Columns Contents
                        i = 0;
                        string tempString = "";
                        foreach (GridColumn GridCol in dgv.Columns)
                        {
                            if (!GridCol.Visible) continue;
                            if (!SelectedColumns.Contains(GridCol.Caption))
                                continue;

                            tempString = dgv.GetRowCellDisplayText(RowPos, GridCol.FieldName);
                            e.Graphics.DrawString(tempString, dgv.GridControl.Font,
                                    new SolidBrush(dgv.GridControl.ForeColor),
                                    new RectangleF((int)ColumnLefts[i], (float)tmpTop,
                                    (int)ColumnWidths[i], (float)CellHeight), StrFormat);


                            #region MyRegion
                            //string type = ColumnTypes[i].ToString();
                            //Console.WriteLine(type);

                            //// For the TextBox Column
                            //if (((Type) ColumnTypes[i]).Name == "GridViewTextBoxColumn" || 
                            //    ((Type) ColumnTypes[i]).Name == "GridViewLinkColumn")
                            //{

                            //    e.Graphics.DrawString(tempString, Cel.InheritedStyle.Font, 
                            //            new SolidBrush(Cel.InheritedStyle.ForeColor),
                            //            new RectangleF((int)ColumnLefts[i], (float)tmpTop,
                            //            (int)ColumnWidths[i], (float)CellHeight), StrFormat);
                            //}
                            //// For the Button Column
                            //else if (((Type) ColumnTypes[i]).Name == "GridViewButtonColumn")
                            //{
                            //    CellButton.Text = Cel.Value.ToString();
                            //    CellButton.Size = new Size((int)ColumnWidths[i], CellHeight);
                            //    Bitmap bmp =new Bitmap(CellButton.Width, CellButton.Height);
                            //    CellButton.DrawToBitmap(bmp, new Rectangle(0, 0, 
                            //            bmp.Width, bmp.Height));
                            //    e.Graphics.DrawImage(bmp, new Point((int)ColumnLefts[i], tmpTop));
                            //}
                            //// For the CheckBox Column
                            //else if (((Type) ColumnTypes[i]).Name == "GridViewCheckBoxColumn")
                            //{
                            //    CellCheckBox.Size = new Size(14, 14);
                            //    CellCheckBox.Checked = (bool)Cel.Value;
                            //    Bitmap bmp = new Bitmap((int)ColumnWidths[i], CellHeight);
                            //    Graphics tmpGraphics = Graphics.FromImage(bmp);
                            //    tmpGraphics.FillRectangle(Brushes.White, new Rectangle(0, 0, 
                            //            bmp.Width, bmp.Height));
                            //    CellCheckBox.DrawToBitmap(bmp, 
                            //            new Rectangle((int)((bmp.Width - CellCheckBox.Width) / 2), 
                            //            (int)((bmp.Height - CellCheckBox.Height) / 2), 
                            //            CellCheckBox.Width, CellCheckBox.Height));
                            //    e.Graphics.DrawImage(bmp, new Point((int)ColumnLefts[i], tmpTop));
                            //}
                            //// For the ComboBox Column
                            //else if (((Type) ColumnTypes[i]).Name == "GridViewComboBoxColumn")
                            //{
                            //    CellComboBox.Size = new Size((int)ColumnWidths[i], CellHeight);
                            //    Bitmap bmp = new Bitmap(CellComboBox.Width, CellComboBox.Height);
                            //    CellComboBox.DrawToBitmap(bmp, new Rectangle(0, 0, 
                            //            bmp.Width, bmp.Height));
                            //    e.Graphics.DrawImage(bmp, new Point((int)ColumnLefts[i], tmpTop));
                            //    e.Graphics.DrawString(Cel.Value.ToString(), Cel.InheritedStyle.Font, 
                            //            new SolidBrush(Cel.InheritedStyle.ForeColor), 
                            //            new RectangleF((int)ColumnLefts[i] + 1, tmpTop, (int)ColumnWidths[i]
                            //            - 16, CellHeight), StrFormatComboBox);
                            //}
                            //// For the Image Column
                            //else if (((Type) ColumnTypes[i]).Name == "GridViewImageColumn")
                            //{
                            //    Rectangle CelSize = new Rectangle((int)ColumnLefts[i], 
                            //            tmpTop, (int)ColumnWidths[i], CellHeight);
                            //    Size ImgSize = ((Image)(Cel.FormattedValue)).Size;
                            //    e.Graphics.DrawImage((Image)Cel.FormattedValue, 
                            //            new Rectangle((int)ColumnLefts[i] + (int)((CelSize.Width - ImgSize.Width) / 2), 
                            //            tmpTop + (int)((CelSize.Height - ImgSize.Height) / 2), 
                            //            ((Image)(Cel.FormattedValue)).Width, ((Image)(Cel.FormattedValue)).Height));

                            //} 
                            #endregion

                            // Drawing Cells Borders 
                            e.Graphics.DrawRectangle(Pens.Black, new Rectangle((int)ColumnLefts[i],
                                    tmpTop, (int)ColumnWidths[i], CellHeight));

                            i++;

                        } 
                        #endregion
                        tmpTop += CellHeight;
                    }

                    RowPos++;
                    // For the first page it calculates Rows per Page
                    if (PageNo == 1) RowsPerPage++;
                }

                if (RowsPerPage == 0) return;

                // Write Footer (Page Number)
                DrawFooter(e, RowsPerPage);

                e.HasMorePages = false;
	        }
	        catch (Exception ex)
            {
                ex.Message.ShowUxError();
            }
        }

        private static void DrawFooter(System.Drawing.Printing.PrintPageEventArgs e, 
                    int RowsPerPage)
        {
            double cnt = 0; 

            // Detemining rows number to print
            if (PrintAllRows)
            {
                if (dgv.OptionsBehavior.AllowAddRows == DevExpress.Utils.DefaultBoolean.True)
                {
                    cnt = dgv.RowCount - 2; // When the GridView doesn't allow adding rows
                }
                else
                {
                    cnt = dgv.RowCount - 1; // When the GridView allows adding rows
                }
            }
            else
            {
                cnt = dgv.SelectedRowsCount;
            }

            // Writing the Page Number on the Bottom of Page
            string PageNum = PageNo.ToString() + " / " + 
                Math.Ceiling((double)(cnt / RowsPerPage)).ToString();

            e.Graphics.DrawString(PageNum, dgv.GridControl.Font, Brushes.Black, 
                e.MarginBounds.Left + (e.MarginBounds.Width -
                e.Graphics.MeasureString(PageNum, dgv.GridControl.Font, 
                e.MarginBounds.Width).Width) / 2, e.MarginBounds.Top + 
                e.MarginBounds.Height + 31);
        }
    }