using System;
using Excel = Microsoft.Office.Interop.Excel;
using System.Collections;
using System.Reflection;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Выгрузка в EXCEL списка работ, выполненных сотрудником
	/// </summary>
	public class ExcelCardWorkStaff
	{
		protected DbStaff		staff;
		protected DateTime		startDate;
		protected DateTime		endDate;
		protected float			summ = 0;
		protected float			summWork = 0;

		ArrayList				cardWorks;

		private Excel.Workbook		workbook;

		// Структура для анализа выполненнывх работ
		public struct WorkAnaliz
		{
			public float	summ_cash;
			public float	summ_cash_fix;
			public float	summ_cash_hours;
			public float	summ_guaranty;
			public float	summ_ppp;
			public float	count_ppp;
			public float	summ_wash;
			public float	count_wash;
			public float[]	summ_guaranty_type;
			// Новые части анализа
			public float	summ_hours;				// Отработанные нормачасы + гарантия
			public float	summ_work_cash;			// Отработанные договорные работы + гарантия
			public float	summ_guaranty_fix;		// Работы по гарантии с фиксированной стоимостью
			public float	guaranty_hours;			// Гарантийные нормочасы
		};

		public WorkAnaliz analiz;

		public ExcelCardWorkStaff(DbStaff staffSrc, DateTime startDateSrc, DateTime endDateSrc, Excel.Workbook workbookSrc)
		{
			staff		= new DbStaff(staffSrc);
			startDate	= startDateSrc;
			endDate		= endDateSrc;

			workbook	= workbookSrc;

			// Получение списка работ
			cardWorks		= new ArrayList();
			DbCardWork.FillList(cardWorks, staff, startDate, endDate);
			analiz.summ_guaranty_type = new float[20];
		}

		public static void DownloadList(ListView list, DateTime startDate, DateTime endDate)
		{
			Excel.Workbook		wb;
			wb					= null;
			
			foreach(ListViewItem item in list.SelectedItems)
			{
				DbStaff staff	= (DbStaff)item.Tag;
				if(staff != null)
				{
					ExcelCardWorkStaff excelCardWorkStaff	= new ExcelCardWorkStaff(staff, startDate, endDate, wb);
					wb					= (Excel.Workbook)excelCardWorkStaff.StartDownload();
				}
			}

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
		public Excel.Workbook StartDownload()
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
					// Удаляем существующие листы
					while(wb.Worksheets.Count != 1)
						((Excel.Worksheet)wb.Worksheets[1]).Delete();
					((Excel.Worksheet)wb.Worksheets[1]).Name	= "Список";
				}
				else
				{
					wb			= workbook;
					app			= wb.Application;
				}
				
				// Всегда вставляем новый лист
				ws = (Excel.Worksheet)wb.Worksheets.Add(Missing.Value, wb.Worksheets[wb.Worksheets.Count], 1, Missing.Value);
				
				DownloadWorks(ws);
				DownloadBrifTitle(wb);
				ws.Name			= staff.Title;
			}
			catch(Exception E)
			{
				Db.SetException(E);
			}
			return wb;
		}

		protected void DownloadWorks(Excel.Worksheet ws)
		{
			// Выгружаем заголовок на определенный лист в Excel
			Excel.Range cell;
			Excel.Range rng;
			string		txt;
			string		rowTxt;
			int			row;
				
			try
			{
				rng							= ws.Rows;
				// Подзаголовок РАБОТЫ
				txt							= staff.Title + " работы выполненные c " + Db.DateToTxt(startDate) + " по " + Db.DateToTxt(endDate);
				cell						= rng.get_Range("A1", Missing.Value);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold				= true;
				// ЗАГОЛОВОК ТАБЛИЦЫ РАБОТ
				// Номер
				txt							= "Номер заказа";
				cell						= rng.get_Range("A2", Missing.Value);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignCenter;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				cell.ColumnWidth			= 12;
				// Наименование Работ
				txt							= "Наименование работ";
				cell						= rng.get_Range("B2", Missing.Value);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignCenter;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				cell.ColumnWidth			= 25;
				// Количество
				txt							= "К-во";
				cell						= rng.get_Range("C2", Missing.Value);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignCenter;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				cell.ColumnWidth			= 8;
				// Часы
				txt							= "Ч.";
				cell						= rng.get_Range("D2", Missing.Value);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignCenter;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				cell.ColumnWidth			= 8;
				// Цена
				txt							= "Цена";
				cell						= rng.get_Range("E2", Missing.Value);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignCenter;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				cell.ColumnWidth			= 12;
				// Сумма
				txt							= "Сумма";
				cell						= rng.get_Range("F2", Missing.Value);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignCenter;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				cell.ColumnWidth			= 12;
				// Количество исполнителей
				txt							= "Исполнителей";
				cell						= rng.get_Range("G2", Missing.Value);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignCenter;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				cell.ColumnWidth			= 12;
				// Гарантия
				txt							= "Гарантия";
				cell						= rng.get_Range("H2", Missing.Value);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignCenter;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				cell.ColumnWidth			= 8;

				row		= 3;
				foreach(object o in cardWorks)
				{
					DbCardWork cardWork = (DbCardWork)o;
					if(cardWork != null)
					{
						DownloadWork(ws, row, cardWork);
						row++;
					}
				}
			}
			catch(Exception E)
			{
				Db.SetException(E);
			}
		}

		protected void DownloadWork(Excel.Worksheet ws, int row, DbCardWork work)
		{
			// Выгружаем заголовок на определенный лист в Excel
			Excel.Range cell;
			Excel.Range rng;
			string		txt;
			string		rowTxt;
		
			try
			{
				rowTxt		= row.ToString();
				rng							= ws.Rows;
				// ЗАГОЛОВОК ТАБЛИЦЫ РАБОТ
				// Номер
				txt							= work.CardNumber.ToString() + " / " + work.CardYear.ToString();
				cell						= rng.get_Range("A" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignCenter;
				// Наименование Работ
				txt							= work.Name;
				cell						= rng.get_Range("B" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignLeft;
				// Количество
				txt							= work.QuontityTxt;
				cell						= rng.get_Range("C" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignCenter;
				// Часы
				txt							= work.ValTxt;
				cell						= rng.get_Range("D" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignCenter;
				// Цена
				txt							= work.PriceFullTxt;
				cell						= rng.get_Range("E" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignCenter;
				// Сумма
				txt							= work.SummFull.ToString();
				cell						= rng.get_Range("F" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignCenter;
				// Количество исполнителей
				txt							= work.DonePersonalQuontityTxt;
				cell						= rng.get_Range("G" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignCenter;
				// Гарантия
				txt							= work.Guaranty.ToString();
				cell						= rng.get_Range("H" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignCenter;

				summ		+= work.SummFull / work.DonePersonalQuontity;
				summWork	+= work.Val / work.DonePersonalQuontity;
				if(work.Val == 0)
					analiz.summ_work_cash  += work.Quontity * work.Price / work.DonePersonalQuontity;
				else
					analiz.summ_hours  += work.Quontity * work.Val / work.DonePersonalQuontity;

				// Использование структуры анализа
				
				if(work.Guaranty == false)
				{
					// ППП
					if(work.CodeDirectoryWork == 188)
					{
						analiz.count_ppp ++;
						analiz.summ_ppp += work.Summ / work.DonePersonalQuontity;
					}
					// Мойка
					if(work.CodeDirectoryWork == 722)
					{
						analiz.count_wash ++;
						analiz.summ_wash += work.Summ / work.DonePersonalQuontity;
					}
					if((work.CodeDirectoryWork != 722)&&(work.CodeDirectoryWork != 188))
					{
						analiz.summ_cash += work.Summ / work.DonePersonalQuontity;
						if(work.Val == 0)
							analiz.summ_cash_fix += work.Summ / work.DonePersonalQuontity;
						else
							analiz.summ_cash_hours += work.Summ / work.DonePersonalQuontity;
					}
				}
				else
				{
					// Гарантию просто рассовываем по видам
					// Мойка
					if(work.CodeDirectoryWork == 722)
					{
						analiz.count_wash ++;
						analiz.summ_wash += work.SummFull / work.DonePersonalQuontity;
					}
					else
					{
						DtCardWork cardWork = DbSqlCardWork.Find(work.CardNumber, work.CardYear, work.Number);
						analiz.summ_guaranty += work.SummFull / work.DonePersonalQuontity;
						long posl = (long)cardWork.GetData("ГАРАНТИЯ_ВИД_КАРТОЧКА_РАБОТА");
						analiz.summ_guaranty_type[posl] += work.SummFull / work.DonePersonalQuontity;
						if(work.Val == 0)
							analiz.summ_guaranty_fix += work.SummFull / work.DonePersonalQuontity;
						else
							analiz.guaranty_hours += work.Val * work.Quontity / work.DonePersonalQuontity;
					}
				}
			}
			catch(Exception E)
			{
				Db.SetException(E);
			}
		}

		protected void DownloadBrifTitle(Excel.Workbook wb)
		{
			// Выгружаем заголовок на определенный лист в Excel
			Excel.Range			cell;
			Excel.Worksheet		ws;
			Excel.Range			rng;
			string				txt;
			int					count;
			string				rowTxt;
			try
			{
				ws							= (Excel.Worksheet)wb.Worksheets[1];
				rng							= ws.Rows;
				rng							= rng.CurrentRegion;
				count						= rng.Rows.Count + 1;
				rng							= ws.Rows;
				rowTxt						= count.ToString();

				// Персонал
				txt							= staff.Title;
				cell						= rng.get_Range("A" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold				= true;
				cell.ColumnWidth			= 35;
				// Сумма
				txt							= summ.ToString();;
				cell						= rng.get_Range("B" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignLeft;
				cell.ColumnWidth			= 12;
				// Затраченные нормочасы
				txt							= summWork.ToString();;
				cell						= rng.get_Range("C" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignLeft;
				cell.ColumnWidth			= 12;

				// Дополнительный анализ
				txt							= analiz.summ_cash.ToString();
				cell						= rng.get_Range("AA" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignLeft;
				cell.ColumnWidth			= 12;
				txt							= analiz.summ_cash_fix.ToString();
				cell						= rng.get_Range("AB" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignLeft;
				cell.ColumnWidth			= 12;
				txt							= analiz.summ_cash_hours.ToString();
				cell						= rng.get_Range("AC" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignLeft;
				cell.ColumnWidth			= 12;
				txt							= analiz.summ_ppp.ToString();
				cell						= rng.get_Range("AD" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignLeft;
				cell.ColumnWidth			= 12;
				txt							= analiz.count_ppp.ToString();
				cell						= rng.get_Range("AE" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignLeft;
				cell.ColumnWidth			= 12;
				txt							= analiz.summ_wash.ToString();
				cell						= rng.get_Range("AF" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignLeft;
				cell.ColumnWidth			= 12;
				txt							= analiz.count_wash.ToString();
				cell						= rng.get_Range("AG" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignLeft;
				cell.ColumnWidth			= 12;
				txt							= analiz.summ_guaranty.ToString();
				cell						= rng.get_Range("AH" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignLeft;
				cell.ColumnWidth			= 12;
				txt							= analiz.summ_work_cash.ToString();
				cell						= rng.get_Range("AI" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignLeft;
				cell.ColumnWidth			= 12;
				txt							= analiz.summ_hours.ToString();
				cell						= rng.get_Range("AJ" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignLeft;
				cell.ColumnWidth			= 12;
				// По видам гарантии
				for(int i=1;i<20;i++)
				{
					string cell_txt = "B" + ItoA(i);
					txt							= analiz.summ_guaranty_type[i].ToString();
					cell						= rng.get_Range(cell_txt + rowTxt, Missing.Value);
					cell.Value2					= txt;
					cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignLeft;
					cell.ColumnWidth			= 12;
				}
				// Новый отчет
				// Сумма заработанных денег по договорным работам
				txt							= analiz.summ_cash_fix.ToString();
				cell						= rng.get_Range("CA" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignLeft;
				cell.ColumnWidth			= 12;
				// Сумма заработанных денег по нормачасам
				txt							= analiz.summ_cash_hours.ToString();
				cell						= rng.get_Range("CB" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignLeft;
				cell.ColumnWidth			= 12;
				// Количество выполненных предпродажек
				txt							= analiz.count_ppp.ToString();
				cell						= rng.get_Range("CD" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignLeft;
				cell.ColumnWidth			= 12;
				// Сумма выполненных гарантийных работ (договорные)
				txt							= analiz.summ_guaranty_fix.ToString();
				cell						= rng.get_Range("CE" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignLeft;
				cell.ColumnWidth			= 12;
				// Сумма нормочасов по гарантийным работам
				txt							= analiz.guaranty_hours.ToString();
				cell						= rng.get_Range("CF" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignLeft;
				cell.ColumnWidth			= 12;

			}
			catch(Exception E)
			{
				Db.SetException(E);
			}
		}

		protected string ItoA(int i)
		{
			switch (i)
			{
				case 1: return "A";
				case 2: return "B";
				case 3: return "C";
				case 4: return "D";
				case 5: return "E";
				case 6: return "F";
				case 7: return "G";
				case 8: return "H";
				case 9: return "I";
				case 10: return "J";
				case 11: return "K";
				case 12: return "L";
				case 13: return "M";
				case 14: return "N";
				case 15: return "O";
				case 16: return "P";
				case 17: return "Q";
				case 18: return "R";
				case 19: return "S";
				default: return "Z";
			}
		}
	}
}
