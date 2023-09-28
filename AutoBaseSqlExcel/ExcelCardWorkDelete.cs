using System;
using System.Collections;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for ExcelCardWorkDelete.
	/// </summary>
	public class ExcelCardWorkDelete
	{
		private ArrayList			report;
		private DateTime			date;
		private Excel.Workbook		workbook;

		public static void DownloadList(ListView list)
		{
			Excel.Workbook		wb;
			wb					= null;
			
			foreach(ListViewItem item in list.SelectedItems)
			{
				DbCard card	= (DbCard)item.Tag;
				if(card != null)
				{
					ExcelCard excelCard	= new ExcelCard(card, wb);
					wb					= excelCard.StartDownLoad();
				}
			}

			ShowExcel(wb);
		}

		public ExcelCardWorkDelete(DateTime dateSrc, Excel.Workbook workbookSrc)
		{
			workbook	= workbookSrc;
			// Получаем список работ данной карточки
			report = new ArrayList();
			DbReportCardWorkDelete.FillArray(report, dateSrc);
			date	= dateSrc;
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

		public Excel.Workbook StartDownLoad()
		{
			Excel.Application app;
			Excel.Workbook wb = null;
			Excel.Worksheet ws;

			try
			{
				if(workbook == null)
				{
					app			= new Excel.Application();				// Новый экземпляр приложения Excel
					wb			= app.Workbooks.Add(Missing.Value);		// Новая книга Excel
					workbook	= wb;
					// Удаляем существующие листы (все кроме одного)
					while(wb.Worksheets.Count != 1)
						((Excel.Worksheet)wb.Worksheets[1]).Delete();
					((Excel.Worksheet)wb.Worksheets[1]).Name	= "Список";
				}
				else
				{
					wb			= workbook;
					app			= wb.Application;
				}
				
				ws = (Excel.Worksheet)wb.Worksheets[1];
				DownloadTitle(ws);
				DownloadReport(ws, 4);
			}
			catch(Exception E)
			{
				Db.SetException(E);
			}
			return wb;
		}

		protected void DownloadTitle(Excel.Worksheet ws)
		{
			// Выгружаем заголовок на определенный лист в Excel
			Excel.Range cell;
			Excel.Range rng;
			string		txt;
			try
			{
				rng							= ws.Rows;
				// Заголовок  списка
				txt	= "Список удаленных работ за " + date.ToShortDateString();
				cell						= rng.get_Range("A1", Missing.Value);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold				= true;
				// Номер карточки
				txt	= "КАРТОЧКА №";
				cell						= rng.get_Range("A2", Missing.Value);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold				= true;
				cell.ColumnWidth			= 14;
				// год карточки
				txt = "ГОД";
				cell						= rng.get_Range("B2", Missing.Value);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold				= true;
				cell.ColumnWidth			= 8;
				// АВТОМОБИЛЬ
				txt							= "РАБОТА";
				cell						= rng.get_Range("C2", Missing.Value);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold				= true;
				cell.ColumnWidth			= 50;
				// Модель
				txt							= "Приемщик";
				cell						= rng.get_Range("D2", Missing.Value);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold				= true;
				cell.ColumnWidth			= 15;
			}
			catch(Exception E)
			{
				Db.SetException(E);
			}
		}

		protected void DownloadReport(Excel.Worksheet ws, int row)
		{
			// Выгружаем заголовок на определенный лист в Excel
			// Все немаслянные работы
			foreach(object o in report)
			{
				DbReportCardWorkDelete element = (DbReportCardWorkDelete)o;
				if(element != null)
				{
					DownloadWork(ws, row, element);
					row++;
				}
			}
		}

		protected void DownloadWork(Excel.Worksheet ws, int row, DbReportCardWorkDelete element)
		{
			// Выгружаем заголовок на определенный лист в Excel
			// Выгружаем заголовок на определенный лист в Excel
			Excel.Range cell;
			Excel.Range rng;
			string		txt;
			string		cellTxt;
			try
			{
				rng							= ws.Rows;
				// Номер карточки
				txt	= element.CardNumberTxt;
				cellTxt = "A" + row.ToString();
				cell						= rng.get_Range(cellTxt, Missing.Value);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold				= true;
				// год карточки
				txt	= element.CardYearTxt;
				cellTxt = "B" + row.ToString();
				cell						= rng.get_Range(cellTxt, Missing.Value);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold				= true;
				// АВТОМОБИЛЬ
				txt	= element.WorkName;
				cellTxt = "C" + row.ToString();
				cell						= rng.get_Range(cellTxt, Missing.Value);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignLeft;
				// Модель
				txt	= element.User;
				cellTxt = "D" + row.ToString();
				cell						= rng.get_Range(cellTxt, Missing.Value);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignLeft;
			}
			catch(Exception E)
			{
				Db.SetException(E);
			}
		}

		public void Download()
		{
			Excel.Workbook		wb;
			wb					= null;
			
			wb					= (Excel.Workbook)this.StartDownLoad();
			ShowExcel(wb);
		}
	}
}
