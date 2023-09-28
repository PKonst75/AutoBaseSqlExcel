using System;
using System.Collections;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbExcel.
	/// </summary>
	/// 
	public enum EXEL_HORIZONT_ALIGN : short { DEFAULT=0, LEFT=1, MIDDLE=2, RIGHT=3}
	public enum EXEL_TEXT_TYPE : short { NONE = 0, BOLD = 1, ITALIC = 2, BOLDITALIC=3 }

	public class DbExcel
	{
		protected static string CANCEL_SELECT = "%CANCEL";
		Excel.Workbook				workbook;
		public bool file_continue	= false;

		public DbExcel()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		protected static string SelectFileDialog(string srcDialogName)
        {
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.Filter = "Файл EXCEL (*.xls)|*.xls|Файл EXCEL NEW (*.xlsx)|*.xlsx";
			dlg.CheckFileExists = true;
			dlg.CheckPathExists = true;
			dlg.Multiselect = false;
			dlg.Title = srcDialogName;
			dlg.ShowDialog();
			if (dlg.FileName.Length == 0)
			{
				DialogResult res = MessageBox.Show(null, "Создать новый файл отчета?", "Запрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (res == DialogResult.No) return CANCEL_SELECT;
				else return "";
			}
			else
			{
				return dlg.FileName;
			}
		}

		public void DownloadData(bool file, int worksheet)
		{
			Excel.Workbook		wb;
			string				fileName;
			int					start_row;
			if(file == true)
			{
				// Запрос имени файла куда выгружать
				/*	OpenFileDialog dlg	= new OpenFileDialog();
					dlg.Filter			= "Файл отчета EXCEL (*.xls)|*.xls|Файл отчета EXCEL NEW (*.xlsx)|*.xlsx";
					dlg.CheckFileExists	= true;
					dlg.CheckPathExists	= true;
					dlg.Multiselect		= false;
					dlg.Title			= "Выберете файл отчета для продолжения";
					dlg.ShowDialog();
					if(dlg.FileName.Length == 0)
					{
						DialogResult res = MessageBox.Show(null, "Создать новый файл отчета?", "Запрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
						if(res == DialogResult.No) return;
						fileName = "";
					}
					else
					{
						fileName = dlg.FileName;
						file_continue = true;
					}
				*/
				fileName = SelectFileDialog("Выберете файл отчета для продолжения");
				if (fileName == CANCEL_SELECT) return;
				if (fileName.Length > 0) file_continue = true;
			}
			else
			{
				fileName = "";
			}	
			// Подготовка документа EXCEL для выгрузки
			wb = WorkbookPrepare(fileName, worksheet);
			start_row	= GetUsedRows(wb, worksheet) + 1;
			DataToExcel(wb, worksheet, start_row);
			ShowExcel(wb);
		}

		public static void ShowExcel(Excel.Workbook wb)
		{
			if(wb == null) return;
			try
			{
				wb.Application.UserControl	= true;
				wb.Application.Visible		= true;
			}
			catch(Exception E)
			{
				Db.SetErrorMessage(E.Message);
			}
		}
		public Excel.Workbook WorkbookPrepare(string file, int sheet)
		{
			Excel.Application app;
			Excel.Workbook wb = null;

			try
			{
				app			= new Excel.Application();
				// Если задан файл отчета, открываем существующую книгу
				if(file.Length == 0)
				{
					wb			= app.Workbooks.Add(Missing.Value);		// Новая книга Excel
					// Форматируем новую книгу
					while(wb.Worksheets.Count < sheet)
						wb.Worksheets.Add(Missing.Value, wb.Worksheets[wb.Worksheets.Count], 1, Missing.Value);
					TitleFormat((Excel.Worksheet)wb.Worksheets[sheet]);		
				}
				else
				{
					wb			= app.Workbooks.Open(file, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
				}
			}
			catch(Exception E)
			{
				Db.SetException(E);
			}
			return wb;
		}

		#region Многостраничный вариант
		public void DownloadDataMult(bool file, int worksheets_count)
		{
			Excel.Workbook		wb;
			string				fileName;
			int					start_row;

			wb					= null;
			fileName			= "";
			start_row			= 0;

			if(file == true)
			{
				// Запрос имени файла куда выгружать
			/*	OpenFileDialog dlg	= new OpenFileDialog();
				dlg.Filter = "Файл отчета EXCEL (*.xls)|*.xls|Файл отчета EXCEL NEW (*.xlsx)|*.xlsx";
				dlg.CheckFileExists	= true;
				dlg.CheckPathExists	= true;
				dlg.Multiselect		= false;
				dlg.Title			= "Выберете файл отчета для продолжения";
				dlg.ShowDialog();
				if(dlg.FileName.Length == 0)
				{
					DialogResult res = MessageBox.Show(null, "Создать новый файл отчета?", "Запрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
					if(res == DialogResult.No) return;
					fileName = "";
				}
				else
				{
					fileName = dlg.FileName;
					file_continue = true;
				}
			*/
				fileName = SelectFileDialog("Выберете файл отчета для продолжения");
				if (fileName == CANCEL_SELECT) return;
				if (fileName.Length > 0) file_continue = true;
			}
			else
			{
				fileName = "";
			}
			
			// Подготовка документа EXCEL для выгрузки
			wb = WorkbookPrepareMult(fileName, worksheets_count);
			for(int worksheet = 1; worksheet <= worksheets_count; worksheet++)
			{
				start_row	= GetUsedRows(wb, worksheet) + 1;
				DataToExcelMult(wb, worksheet, start_row);
			}
			ShowExcel(wb);
		}
		public Excel.Workbook WorkbookPrepareMult(string file, int sheets_count)
		{
			Excel.Application app;
			Excel.Workbook wb = null;

			try
			{
				app			= new Excel.Application();
				// Если задан файл отчета, открываем существующую книгу
				if(file.Length == 0)
				{
					wb			= app.Workbooks.Add(Missing.Value);		// Новая книга Excel
					// Форматируем новую книгу
					// Удаляем лишние листы
					while(wb.Worksheets.Count != 1)
					{
						Excel.Worksheet ws = (Excel.Worksheet)wb.Worksheets[wb.Worksheets.Count];
						ws.Delete();
					}
					// Добавляем и форматируем нужное количество листов
					
					while(wb.Worksheets.Count < sheets_count)
						wb.Worksheets.Add(Missing.Value, wb.Worksheets[wb.Worksheets.Count], 1, Missing.Value);
					for(int sheet = 1; sheet <= sheets_count; sheet ++)
					{
						TitleFormatMult((Excel.Worksheet)wb.Worksheets[sheet], sheet);
					}
				}
				else
				{
					wb			= app.Workbooks.Open(file, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
				}
			}
			catch(Exception E)
			{
				Db.SetException(E);
			}
			return wb;
		}
		protected void DataToExcelMult(Excel.Workbook workbook, int sheet, int start)
		{
			Excel.Worksheet ws = (Excel.Worksheet)workbook.Worksheets[sheet];
			DataToExcelMult(ws, sheet, start);
		}
		virtual protected void TitleFormatMult(Excel.Worksheet ws, int sheet){}
		virtual protected void DataToExcelMult(Excel.Worksheet ws, int sheet, int start){}
		#endregion

		protected int GetUsedRows(Excel.Workbook workbook, int sheet)
		{
			Excel.Worksheet ws = (Excel.Worksheet)workbook.Worksheets[sheet];
			Excel.Range rng = ws.UsedRange;
			return rng.Rows.Count;
		}

		protected void DataToExcel(Excel.Workbook workbook, int sheet, int start)
		{
			Excel.Worksheet ws = (Excel.Worksheet)workbook.Worksheets[sheet];
			DataToExcel(ws, start);
		}

		virtual protected void TitleFormat(Excel.Worksheet ws){}
		virtual protected void DataToExcel(Excel.Worksheet ws, int start){}

		#region Методы для упрощения работы
		protected void FormatColumnNumberFormat(Excel.Worksheet ws, string col, string format)
		{
			Excel.Range	rng					= ws.Rows;
			Excel.Range cell				= rng.get_Range(col, Missing.Value);
			cell.EntireColumn.NumberFormatLocal	= format;
		}
		protected void FormatColumnWrapText(Excel.Worksheet ws, string col, bool wrap)
		{
			Excel.Range	rng					= ws.Rows;
			Excel.Range cell				= rng.get_Range(col, Missing.Value);
			cell.EntireColumn.WrapText		= wrap;
		}

		protected void FormatColumnVAlign(Excel.Worksheet ws, string col, string align_v)
		{
			Excel.Range	rng					= ws.Rows;
			Excel.Range cell				= rng.get_Range(col, Missing.Value);
			switch(align_v)
			{
				case "Top":
					cell.EntireColumn.VerticalAlignment		= Excel.XlVAlign.xlVAlignTop;
					break;
				case "Center":
					cell.EntireColumn.VerticalAlignment		= Excel.XlVAlign.xlVAlignCenter;
					break;
				case "Bottom":
					cell.EntireColumn.VerticalAlignment		= Excel.XlVAlign.xlVAlignBottom;
					break;
				default:
					cell.EntireColumn.VerticalAlignment		= Excel.XlVAlign.xlVAlignTop;
					break;
			}
		}

        protected void FormatCellVAlign(Excel.Worksheet ws, string cell_name, string align_v)
        {
            Excel.Range rng = ws.Rows;
            Excel.Range cell = rng.get_Range(cell_name, Missing.Value);
            switch (align_v)
            {
                case "Top":
                    cell.EntireColumn.VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    break;
                case "Center":
                    cell.EntireColumn.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                    break;
                case "Bottom":
                    cell.EntireColumn.VerticalAlignment = Excel.XlVAlign.xlVAlignBottom;
                    break;
                default:
                    cell.EntireColumn.VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    break;
            }
        }

        protected void FormatCellsVAlign(Excel.Worksheet ws, string cell_start, string cell_end, string align_v)
        {
            Excel.Range rng = ws.Rows;
            Excel.Range cell = rng.get_Range(cell_start, cell_end);
            switch (align_v)
            {
                case "Top":
                    cell.EntireColumn.VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    break;
                case "Center":
                    cell.EntireColumn.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                    break;
                case "Bottom":
                    cell.EntireColumn.VerticalAlignment = Excel.XlVAlign.xlVAlignBottom;
                    break;
                default:
                    cell.EntireColumn.VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    break;
            }
        }

		protected void FormatColumn(Excel.Worksheet ws, string col, int width, int font_size, string align_h)
		{
			Excel.Range	rng					= ws.Rows;
			Excel.Range cell				= rng.get_Range(col, Missing.Value);
			cell.Columns.ColumnWidth		= width;
			switch(align_h)
			{
				case "Left":
					cell.EntireColumn.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
					break;
				case "Center":
					cell.EntireColumn.HorizontalAlignment		= Excel.XlHAlign.xlHAlignCenter;
					break;
				case "Right":
					cell.EntireColumn.HorizontalAlignment		= Excel.XlHAlign.xlHAlignRight;
					break;
				default:
					cell.EntireColumn.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
					break;
			}
			cell.EntireColumn.Font.Size		= font_size;
		}
		protected void FormatColumn(Excel.Worksheet ws, string col, int width, int font_size, string align_h, bool bold)
		{
			Excel.Range	rng					= ws.Rows;
			Excel.Range cell				= rng.get_Range(col, Missing.Value);
			cell.Columns.ColumnWidth		= width;
			switch(align_h)
			{
				case "Left":
					cell.EntireColumn.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
					break;
				case "Center":
					cell.EntireColumn.HorizontalAlignment		= Excel.XlHAlign.xlHAlignCenter;
					break;
				case "Right":
					cell.EntireColumn.HorizontalAlignment		= Excel.XlHAlign.xlHAlignRight;
					break;
				default:
					cell.EntireColumn.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
					break;
			}
			cell.EntireColumn.Font.Size		= font_size;
			cell.EntireColumn.Font.Bold		= bold;
		}
		protected void CellText(Excel.Worksheet ws, string cell_name, string text, string align_h, int font_size, bool bold)
		{
			Excel.Range	rng					= ws.Rows;
			Excel.Range cell				= rng.get_Range(cell_name, Missing.Value);
			cell.Value2						= text;
			switch(align_h)
			{
				case "Left":
					cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
					break;
				case "Center":
					cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignCenter;
					break;
				case "Right":
					cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignRight;
					break;
				default:
					cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
					break;
			}
			if(bold == true)
				cell.Font.Bold					= true;
			cell.Font.Size					= font_size;
		}
		protected void CellText(Excel.Worksheet ws, string cell_name, string text)
		{
			Excel.Range	rng					= ws.Rows;
			Excel.Range cell				= rng.get_Range(cell_name, Missing.Value);
			cell.Value2						= text;
		}
		protected void MergeCells(Excel.Worksheet ws, string cell_start, string cell_end)
		{
			Excel.Range	rng					= ws.Rows;
			Excel.Range cell				= rng.get_Range(cell_start, cell_end);
			cell.Merge(Missing.Value);
		}
        protected void UnMergeCells(Excel.Worksheet ws, string cell_start, string cell_end)
        {
            Excel.Range rng = ws.Rows;
            Excel.Range cell = rng.get_Range(cell_start, cell_end);
            cell.UnMerge();
        }
		protected void CellVertical(Excel.Worksheet ws, string cell_name)
		{
			Excel.Range	rng					= ws.Rows;
			Excel.Range cell				= rng.get_Range(cell_name, Missing.Value);
			cell.Orientation				= 90;
		}
		protected void PrintStandart1(Excel.Worksheet ws)
		{
			ws.PageSetup.LeftMargin		= ws.Application.InchesToPoints(0.196850393700787);
			ws.PageSetup.RightMargin	= ws.Application.InchesToPoints(0.196850393700787);
			ws.PageSetup.TopMargin		= ws.Application.InchesToPoints(0.393700787401575);
			ws.PageSetup.BottomMargin	= ws.Application.InchesToPoints(0.393700787401575);
			ws.PageSetup.FitToPagesWide	= 1;
			ws.PageSetup.FitToPagesTall	= false;
			ws.PageSetup.Zoom			= false;
		}
		protected void CellsColor(Excel.Worksheet ws, string cell_start, string cell_end, int color_index)
		{
			Excel.Range	rng					= ws.Rows;
			Excel.Range cell				= rng.get_Range(cell_start, cell_end);
			cell.Interior.ColorIndex		= color_index;
			cell.Interior.Pattern			= Excel.XlPattern.xlPatternSolid;
		}
		protected void CellsBorderOuter(Excel.Worksheet ws, string cell_start, string cell_end, int color_index)
		{
			Excel.Range	rng					= ws.Rows;
			Excel.Border border;
			Excel.Range cell				= rng.get_Range(cell_start, cell_end);
			border = cell.Borders.get_Item(Excel.XlBordersIndex.xlEdgeBottom);
			border.ColorIndex	= Excel.XlColorIndex.xlColorIndexAutomatic;
			border.LineStyle	= Excel.XlLineStyle.xlContinuous;
			border.Weight		= Excel.XlBorderWeight.xlThin;

			border = cell.Borders.get_Item(Excel.XlBordersIndex.xlEdgeTop);
			border.ColorIndex	= Excel.XlColorIndex.xlColorIndexAutomatic;
			border.LineStyle	= Excel.XlLineStyle.xlContinuous;
			border.Weight		= Excel.XlBorderWeight.xlThin;

			border = cell.Borders.get_Item(Excel.XlBordersIndex.xlEdgeLeft);
			border.ColorIndex	= Excel.XlColorIndex.xlColorIndexAutomatic;
			border.LineStyle	= Excel.XlLineStyle.xlContinuous;
			border.Weight		= Excel.XlBorderWeight.xlThin;

			border = cell.Borders.get_Item(Excel.XlBordersIndex.xlEdgeRight);
			border.ColorIndex	= Excel.XlColorIndex.xlColorIndexAutomatic;
			border.LineStyle	= Excel.XlLineStyle.xlContinuous;
			border.Weight		= Excel.XlBorderWeight.xlThin;

			border = cell.Borders.get_Item(Excel.XlBordersIndex.xlInsideVertical);
			border.ColorIndex	= Excel.XlColorIndex.xlColorIndexAutomatic;
			border.LineStyle	= Excel.XlLineStyle.xlContinuous;
			border.Weight		= Excel.XlBorderWeight.xlThin;
		}

		protected void CellTextAdd(Excel.Worksheet ws, string cell_name, string text)
		{
			Excel.Range rng = ws.Rows;
			Excel.Range cell = rng.get_Range(cell_name, Missing.Value);
			string txt = "";
			object o = cell.Value2;
			if (o != null)
				txt = o.ToString();
			if (txt != "") txt += " ";
			txt += text;
			
			cell.Value2 = txt;
		}
		protected string GET_CellText(Excel.Worksheet ws, string cell_name)
		{
			Excel.Range	rng					= ws.Rows;
			Excel.Range cell				= rng.get_Range(cell_name, Missing.Value);
			string text						= "";
			object o						= cell.Value2;
			if(o != null)
				text						= o.ToString();
			return text;
		}
		protected string GET_CellTextDateShort(Excel.Worksheet ws, string cell_name)
		{
			Excel.Range	rng					= ws.Rows;
			Excel.Range cell				= rng.get_Range(cell_name, Missing.Value);
			string text						= "";
			object o						= cell.Text;
			if(o != null)
			{
				text						= o.ToString();
			}
			return text;
		}
		protected void RowInsert(Excel.Worksheet ws, string cell_from_1, string cell_from_2, string cell_to_1, string cell_to_2)
		{
			Excel.Range	rng					= ws.Rows;
			//Excel.Range from				= rng.get_Range(cell_from_1, cell_from_2);
			//Excel.Range to					= rng.get_Range(cell_to_1, cell_to_2);
			Excel.Range from				= rng.get_Range("97:97", Missing.Value);
			Excel.Range to					= rng.get_Range("95:95", Missing.Value);
			to.Insert(Excel.XlInsertShiftDirection.xlShiftDown, from);
		}
		protected void RowInsertCopy(Excel.Worksheet ws, long row)
		{
			Excel.Range cpy = ws.Rows[row];
			Excel.Range cells = ws.Rows[row + 1];
			cells.Insert(Excel.XlInsertShiftDirection.xlShiftDown, Missing.Value);
			cells = ws.Rows[row + 1];
			cpy.Copy(cells);
		}
		protected void RowResize(Excel.Worksheet ws, long row)
		{
			Excel.Range cells = ws.Rows[row];
			cells.AutoFit() ;
		}
		protected void CellTextFormula(Excel.Worksheet ws, string cell_name, string text)
		{
			Excel.Range rng = ws.Rows;
			Excel.Range cell = rng.get_Range(cell_name, Missing.Value);
			cell.Formula = text;
		}
		protected void CellTextNumber(Excel.Worksheet ws, string cell_name, string text)
		{
			Excel.Range rng = ws.Rows;
			Excel.Range cell = rng.get_Range(cell_name, Missing.Value);
			cell.Value2 = text;
			cell.NumberFormat = "#0";
			cell.Value2 = cell.Value2;
		}
		protected void CellDouble(Excel.Worksheet ws, string cell_name, double data)
		{
			Excel.Range rng = ws.Rows;
			Excel.Range cell = rng.get_Range(cell_name, Missing.Value);
			cell.Value2 = data;
		}
		#endregion

		#region Новые вспомогательные методы
		// Самый общий метод устновки значений ячейки
		protected void TrySetCellBorderText(Excel.Range srcRange, string column_name, int row, string srcCellTxt)
		{
			TrySetCellBorderText(srcRange, column_name + row.ToString(), srcCellTxt);
		}
		protected void TrySetCellBorderText(Excel.Range srcRange, string srcCellName, string srcCellTxt) // Текст в ячейку и сетка по границе
		{
			Excel.Range cell;
			try
			{
				cell = srcRange.get_Range(srcCellName, Missing.Value);
				cell.Value2 = srcCellTxt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
			}
			catch (Exception E)
			{
				Db.SetException(E);
			}
		}

		protected void TrySetCellBorderFloat(Excel.Range srcRange, string column_name, int row, float srcCellFloat, EXEL_TEXT_TYPE srcTextType = EXEL_TEXT_TYPE.NONE)
		{
			TrySetCellBorderFloat(srcRange, column_name + row.ToString(), srcCellFloat, srcTextType);
		}																									
		protected void TrySetCellBorderFloat(Excel.Range srcRange, string srcCellName, float srcCellFloat, EXEL_TEXT_TYPE srcTextType = EXEL_TEXT_TYPE.NONE) // Число Float в ячейку и сетка по границе
		{
			Excel.Range cell;
			try
			{
				cell = srcRange.get_Range(srcCellName, Missing.Value);
				cell.Value2 = srcCellFloat;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				if (srcTextType == EXEL_TEXT_TYPE.BOLD || srcTextType == EXEL_TEXT_TYPE.BOLDITALIC)
					cell.Font.Bold = true;
				if (srcTextType == EXEL_TEXT_TYPE.ITALIC || srcTextType == EXEL_TEXT_TYPE.BOLDITALIC)
					cell.Font.Italic = true;
			}
			catch (Exception E)
			{
				Db.SetException(E);
			}
		}
		protected void TrySetCellText1(Excel.Range srcRange, string srcCellName, string srcCellTxt, float srcColumnWidth = 0F, float srcFontSize = 0F, EXEL_HORIZONT_ALIGN srcHorizontAlign = EXEL_HORIZONT_ALIGN.DEFAULT, string srcNumberFormat = "", EXEL_TEXT_TYPE srcTextType = EXEL_TEXT_TYPE.NONE)
		{
			Excel.Range cell;
			try
			{
				cell = srcRange.get_Range(srcCellName, Missing.Value);
				cell.Value2 = srcCellTxt;
				switch (srcHorizontAlign)
				{
					case EXEL_HORIZONT_ALIGN.LEFT:
						cell.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
						break;
					case EXEL_HORIZONT_ALIGN.RIGHT:
						cell.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
						break;
					case EXEL_HORIZONT_ALIGN.MIDDLE:
						cell.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
						break;
					case EXEL_HORIZONT_ALIGN.DEFAULT:
					default:
						break;
				}
				if(srcTextType == EXEL_TEXT_TYPE.BOLD || srcTextType == EXEL_TEXT_TYPE.BOLDITALIC)
					cell.Font.Bold = true;
				if (srcTextType == EXEL_TEXT_TYPE.ITALIC || srcTextType == EXEL_TEXT_TYPE.BOLDITALIC)
					cell.Font.Italic = true;
				cell.Columns.ColumnWidth = srcColumnWidth;
				cell.EntireColumn.Font.Size = srcFontSize;
				if(srcNumberFormat != "")
					cell.EntireColumn.NumberFormatLocal = srcNumberFormat;
			}
			catch (Exception E)
			{
				Db.SetException(E);
			}
		}
		protected void SetCellTextLeft(Excel.Range srcRange, string srcColumnName, string srcTitle, float srcColumnWidth, float srcFontSize, EXEL_HORIZONT_ALIGN srcHorizontAlign)
        {

        }

		protected static void TryColumnFormatTitleBold(Excel.Range srcRange, string srcColumnTitle, string srcColumn,
			float srcColumnWidth, float srcColumnFontSize, EXEL_HORIZONT_ALIGN srcHorizontalAlign = EXEL_HORIZONT_ALIGN.MIDDLE, string srcNumberFormat = "") // Текст в ячейку и сетка по границе
		{
			Excel.Range cell;
			try
			{
				cell = srcRange.get_Range(srcColumn, Missing.Value);
				cell.Value2 = srcColumnTitle;
				cell.Font.Bold = true; // Заголовок всегда BOLD
				cell.Columns.ColumnWidth = srcColumnWidth;
				cell.EntireColumn.Font.Size = srcColumnFontSize;
				switch (srcHorizontalAlign)
				{
					case EXEL_HORIZONT_ALIGN.LEFT:
						cell.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
						break;
					case EXEL_HORIZONT_ALIGN.RIGHT:
						cell.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
						break;
					case EXEL_HORIZONT_ALIGN.MIDDLE:
						cell.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
						break;
					case EXEL_HORIZONT_ALIGN.DEFAULT:
					default:
						cell.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
						break;
				}
				if (srcNumberFormat != "")
						cell.EntireColumn.NumberFormatLocal = srcNumberFormat;
			}
			catch (Exception E)
			{
				Db.SetException(E);
			}
		}

		protected void TrySetCellBorderBool(Excel.Range srcRange, string column_name, int row, bool value)
		{
			TrySetCellBorderBool(srcRange, column_name + row.ToString(), value);
		}
		protected void TrySetCellBorderBool(Excel.Range srcRange, string srcCellName, bool value) 
		{
			string bool_string = "";
			if (value) bool_string = "ДА";
			TrySetCellBorderText(srcRange, srcCellName, bool_string);
		}
		#endregion
	}
}
