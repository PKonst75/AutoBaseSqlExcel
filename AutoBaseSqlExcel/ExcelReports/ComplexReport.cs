using System;
using System.Collections;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Windows.Forms;
using System.Collections.Generic;

using AutoBaseSql;

namespace ExcelReports
{
	/// <summary>
	/// Summary description for ExcelCardReport.
	/// </summary>
	/// 
	public class ComplexReport:DbExcel
	{
		delegate void FormatWorkbookList(Excel.Worksheet ws);

		readonly private Excel.Workbook		workbook_;
		readonly private ComplexReportData		cardReport;
		private static int[]		current_row = new int[13+1];    // Последняя выгруженная строка отчета полистно

		private struct WorkBookSchemaList
		{
			string name;
			FormatWorkbookList make_title;
			public static WorkBookSchemaList MakeSchema(string name, FormatWorkbookList make_title)
			{
				WorkBookSchemaList list;
				list.name = name;
				list.make_title = make_title;
				return list;
			}
			public string Name
			{
				get { return name; }
			}
			public FormatWorkbookList FormatList
            {
                get { return make_title;  }
            }
		};
		private static void  FormatNewExcelWorkBook(Excel.Workbook workbook)
        {
            List<WorkBookSchemaList> schema = new List<WorkBookSchemaList>
            {
                WorkBookSchemaList.MakeSchema("ТО", new FormatWorkbookList(DownloadTitleTO)),
                WorkBookSchemaList.MakeSchema("ТО-бн", new FormatWorkbookList(DownloadTitleTO)),
                WorkBookSchemaList.MakeSchema("Гарантия", new FormatWorkbookList(DownloadTitleGuaranty)),
                WorkBookSchemaList.MakeSchema("ППП", new FormatWorkbookList(DownloadTitlePPP)),
                WorkBookSchemaList.MakeSchema("Продажи", new FormatWorkbookList(DownloadTitleWork)),
                WorkBookSchemaList.MakeSchema("Тюнинг-Музыка", new FormatWorkbookList(DownloadTitleWork)),
                WorkBookSchemaList.MakeSchema("Допы", new FormatWorkbookList(DownloadTitleTO)),
                WorkBookSchemaList.MakeSchema("Малярка", new FormatWorkbookList(DownloadTitleWork)),
                WorkBookSchemaList.MakeSchema("Мойка", new FormatWorkbookList(DownloadTitleWork)),
                WorkBookSchemaList.MakeSchema("Список", new FormatWorkbookList(DownloadTitleReport)),
                WorkBookSchemaList.MakeSchema("Гарантия Расширенно", new FormatWorkbookList(DownloadTitleGuarantyExtend)),
                WorkBookSchemaList.MakeSchema("Затраты трейжин", new FormatWorkbookList(DownloadTitleTO)),
                WorkBookSchemaList.MakeSchema("ЛАДА Аксессуары", new FormatWorkbookList(DownloadTitleWork))
            };

            int list_count = 1;
			foreach(WorkBookSchemaList list in schema)
            {
				if(workbook.Worksheets.Count < list_count)
                {
					workbook.Worksheets.Add(Missing.Value, workbook.Worksheets[workbook.Worksheets.Count], 1, Missing.Value);
				}
				((Excel.Worksheet)workbook.Worksheets[list_count]).Name = list.Name;
				list.FormatList((Excel.Worksheet)workbook.Worksheets[list_count]);
				++list_count;
			}
		}
		private static Excel.Workbook OpenOrCreateWorkbook(string existing_file_name)
        {
			Excel.Workbook wb = null;
			try
			{
				Excel.Application app = new Excel.Application(); // Новый экземпляр приложения Excel															  
				if (String.IsNullOrEmpty(existing_file_name))
				{
					wb = app.Workbooks.Add(Missing.Value);      // Новая книга Excel
					FormatNewExcelWorkBook(wb);					// Форматируем новую книгу
				}
				else
				{
					// Если задан файл отчета, открываем существующую книгу
					wb = app.Workbooks.Open(existing_file_name, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
				}
			}
			catch (Exception E)
			{
				Db.SetException(E);
				return null;
			}
			return wb;
		}
		public static void Downloadreport(ArrayList array)
		{
			string fileName = SelectFileDialog("Выберете файл отчета для продолжения");
			if (fileName == DbExcel.CANCEL_SELECT) return; // Отказ от выбора файла

			// Создание и открытие книги
			Excel.Workbook workbook;
			if( (workbook = OpenOrCreateWorkbook(fileName)) == null) return;

			foreach (object o in array)
			{
				DbCard card	= (DbCard)o;
				if(card != null)
				{
					new ComplexReport(card, workbook).StartDownLoad();
				}
			}
			ShowExcel(workbook);
		}

		public ComplexReport(DbCard cardSrc, Excel.Workbook workbookSrc)
		{
			cardReport		= new ComplexReportData(cardSrc);
			workbook_		= workbookSrc;
		}

		public void StartDownLoad()
		{
			try
			{
				DownloadReport();					// Выгрузка параметров основного отчета
			}
			catch(Exception E)
			{
				Db.SetException(E);
			}
		}

		#region ТО
		protected static void DownloadTitleTO(Excel.Worksheet ws)
		{
			// Выгружаем заголовок на определенный лист в Excel
			Excel.Range rng;	
			try
			{
				rng							= ws.Rows;			
				TryColumnFormatTitleBold(rng, "Закрытие", "A1", 9, 8, EXEL_HORIZONT_ALIGN.LEFT);	
				TryColumnFormatTitleBold(rng, "Номер", "B1", 8, 8, EXEL_HORIZONT_ALIGN.LEFT);		
				TryColumnFormatTitleBold(rng, "Дата", "C1", 9, 8, EXEL_HORIZONT_ALIGN.LEFT);		
				TryColumnFormatTitleBold(rng, "ТО", "D1", 9, 8, EXEL_HORIZONT_ALIGN.LEFT, "# ##0,00");	
				TryColumnFormatTitleBold(rng, "Жидкости", "E1", 9, 8, EXEL_HORIZONT_ALIGN.LEFT, "# ##0,00");
				TryColumnFormatTitleBold(rng, "Себест.", "F1", 9, 8, EXEL_HORIZONT_ALIGN.LEFT, "# ##0,00");
				TryColumnFormatTitleBold(rng, "Работы", "G1", 9, 8, EXEL_HORIZONT_ALIGN.LEFT, "# ##0,00");
				TryColumnFormatTitleBold(rng, "Мойка", "H1", 9, 8, EXEL_HORIZONT_ALIGN.LEFT, "# ##0,00");
				TryColumnFormatTitleBold(rng, "Итог", "I1", 9, 8, EXEL_HORIZONT_ALIGN.LEFT, "# ##0,00");
				TryColumnFormatTitleBold(rng, "Скидка", "J1", 9, 8, EXEL_HORIZONT_ALIGN.LEFT, "# ##0,00");
				TryColumnFormatTitleBold(rng, "К оплате", "K1", 9, 8, EXEL_HORIZONT_ALIGN.LEFT, "# ##0,00");
				TryColumnFormatTitleBold(rng, "Оплата", "L1", 9, 8, EXEL_HORIZONT_ALIGN.LEFT, "# ##0,00");
				TryColumnFormatTitleBold(rng, "Детали", "M1", 9, 8, EXEL_HORIZONT_ALIGN.LEFT, "# ##0,00");
				TryColumnFormatTitleBold(rng, "Себест.", "N1", 9, 8, EXEL_HORIZONT_ALIGN.LEFT, "# ##0,00");
				TryColumnFormatTitleBold(rng, "Гар.", "O1", 4, 8, EXEL_HORIZONT_ALIGN.LEFT);
				TryColumnFormatTitleBold(rng, "Примечание", "P1", 15, 8, EXEL_HORIZONT_ALIGN.LEFT);
				TryColumnFormatTitleBold(rng, "Модель", "Q1", 15, 8, EXEL_HORIZONT_ALIGN.LEFT);
				TryColumnFormatTitleBold(rng, "VIN", "R1", 20, 8, EXEL_HORIZONT_ALIGN.LEFT);
				TryColumnFormatTitleBold(rng, "Рег.Знак", "S1", 9, 8, EXEL_HORIZONT_ALIGN.LEFT);
				TryColumnFormatTitleBold(rng, "Консультант", "W1", 12, 8, EXEL_HORIZONT_ALIGN.LEFT);
				TryColumnFormatTitleBold(rng, "Часы", "X1", 9, 8, EXEL_HORIZONT_ALIGN.LEFT, "# ##0,00");
				TryColumnFormatTitleBold(rng, "Часы Полные", "Y1", 9, 8, EXEL_HORIZONT_ALIGN.LEFT, "# ##0,00");

				// Параметры печати
				ws.PageSetup.Orientation	= Excel.XlPageOrientation.xlLandscape;
				ws.PageSetup.LeftMargin		= ws.Application.InchesToPoints(0.393700787401575);
				ws.PageSetup.RightMargin	= ws.Application.InchesToPoints(0.393700787401575);
				ws.PageSetup.TopMargin		= ws.Application.InchesToPoints(0.590551181102362);
				ws.PageSetup.BottomMargin	= ws.Application.InchesToPoints(0.590551181102362);
				ws.PageSetup.HeaderMargin	= ws.Application.InchesToPoints(0.511811023622047);
				ws.PageSetup.FooterMargin	= ws.Application.InchesToPoints(0.511811023622047);
				ws.PageSetup.PrintArea		= "$A:$M";
			}
			catch(Exception E)
			{
				Db.SetException(E);
			}
		}
		protected void DownloadReportTO(Excel.Worksheet ws, int row)
		{
			// Выгружаем данные на определенный лист в Excel
			Excel.Range rng;
			try
			{
				rng	= ws.Rows;
				TrySetCellBorderText(rng, "A", row, cardReport.CloseDate);
				TrySetCellBorderText(rng, "B", row, cardReport.WarrantNumber);
				TrySetCellBorderText(rng, "C", row, cardReport.WarrantDate);
				TrySetCellBorderFloat(rng, "D", row, cardReport.CardTOD);
				TrySetCellBorderFloat(rng, "E", row, cardReport.CardOilD);
				TrySetCellBorderFloat(rng, "F", row, cardReport.CardOilInputD);
				TrySetCellBorderFloat(rng, "G", row, cardReport.CardWorkNoWashD);
				TrySetCellBorderFloat(rng, "H", row, cardReport.CardWashD);
				TrySetCellBorderFloat(rng, "I", row, cardReport.WorkD);
				TrySetCellBorderFloat(rng, "J", row, cardReport.CardDiscountD, EXEL_TEXT_TYPE.BOLD);
				TrySetCellBorderFloat(rng, "K", row, cardReport.CardPayD, EXEL_TEXT_TYPE.BOLD);
				TrySetCellBorderText(rng, "L", row, "");
				TrySetCellBorderFloat(rng, "M", row, cardReport.CardDetailD, EXEL_TEXT_TYPE.BOLD);
				TrySetCellBorderFloat(rng, "N", row, cardReport.CardDetailInputD);
				TrySetCellBorderBool(rng, "O", row, cardReport.HaveGuaranty());
				TrySetCellBorderText(rng, "Q", row, cardReport.AutoModel);
				TrySetCellBorderText(rng, "R", row, cardReport.AutoVIN);
				TrySetCellBorderText(rng, "S", row, cardReport.AutoSignNo);
				// Блок дополнительных даных для рассчета зарплаты
				TrySetCellBorderText(rng, "W", row, cardReport.ServiceManager);
				TrySetCellBorderFloat(rng, "X", row, cardReport.PayedHoursMinusDiscount);
				TrySetCellBorderFloat(rng, "Y", row, cardReport.PayedHours);
			}
			catch(Exception E)
			{
				Db.SetException(E);
			}
		}

		protected void DownloadReportTO(Excel.Worksheet ws)
		{
			int count;
			int index	= ws.Index;
			if(ComplexReport.current_row[index] == 0)
			{
				Excel.Range rng = ws.UsedRange;
				count = rng.Rows.Count;
			}
			else
			{
				count = ComplexReport.current_row[index];
				ComplexReport.current_row[index]++;
			}
			DownloadReportTO(ws, count + 1);
		}
		#endregion
		
		#region Гарантия
		protected static void DownloadTitleGuaranty(Excel.Worksheet ws)
		{
			// Выгружаем заголовок на определенный лист в Excel
			Excel.Range cell;
			Excel.Range rng;
			string		txt;
			try
			{
				rng							= ws.Rows;
				// Дата закрытия
				txt	= "Закрытие";
				cell							= rng.get_Range("A1", Missing.Value);
				cell.Value2						= txt;
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 9;
				cell.EntireColumn.Font.Size		= 8;
				// Номер Заказ-наряда
				txt	= "Номер";
				cell							= rng.get_Range("B1", Missing.Value);
				cell.Value2						= txt;
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 8;
				cell.EntireColumn.Font.Size		= 8;
				// Дата заказ-наряда
				txt	= "Дата";
				cell							= rng.get_Range("C1", Missing.Value);
				cell.Value2						= txt;
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 9;
				cell.EntireColumn.Font.Size		= 8;
				// Работы ТО
				txt	= "ТО";
				cell								= rng.get_Range("D1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 9;
				cell.EntireColumn.Font.Size			= 8;
				cell.EntireColumn.NumberFormatLocal	= "# ##0,00";
				// Жидкости в заказ-наряде
				txt	= "Жидкости";
				cell								= rng.get_Range("E1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 9;
				cell.EntireColumn.Font.Size			= 8;
				cell.EntireColumn.NumberFormatLocal	= "# ##0,00";
				// Работы без мойки
				txt	= "Работы";
				cell								= rng.get_Range("F1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 9;
				cell.EntireColumn.Font.Size			= 8;
				cell.EntireColumn.NumberFormatLocal	= "# ##0,00";
				// Моечно-уборочные работы
				txt	= "Мойка";
				cell								= rng.get_Range("G1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 9;
				cell.EntireColumn.Font.Size			= 8;
				cell.EntireColumn.NumberFormatLocal	= "# ##0,00";
				// Сумма деталей
				txt	= "Детали";
				cell								= rng.get_Range("H1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 9;
				cell.EntireColumn.Font.Size			= 8;
				cell.EntireColumn.NumberFormatLocal	= "# ##0,00";
				// Гарантия Вид
				txt	= "Гар.";
				cell							= rng.get_Range("I1", Missing.Value);
				cell.Value2						= txt;
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 22;
				cell.EntireColumn.Font.Size		= 8;
				// Подразделение
				cell							= rng.get_Range("J1", Missing.Value);
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 10;
				cell.EntireColumn.Font.Size		= 8;
				// Пустые строчки для доп информации
				// 1
				cell							= rng.get_Range("K1", Missing.Value);
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 5;
				cell.EntireColumn.Font.Size		= 8;
				// Модель
				txt	= "Модель";
				cell							= rng.get_Range("L1", Missing.Value);
				cell.Value2						= txt;
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 15;
				cell.EntireColumn.Font.Size		= 8;
				// VIN
				txt	= "VIN";
				cell							= rng.get_Range("M1", Missing.Value);
				cell.Value2						= txt;
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 20;
				cell.EntireColumn.Font.Size		= 8;		
				// Регистрационный знак
				txt	= "Рег.Знак";
				cell							= rng.get_Range("N1", Missing.Value);
				cell.Value2						= txt;
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 9;
				cell.EntireColumn.Font.Size		= 8;

				// Параметры печати
				ws.PageSetup.Orientation	= Excel.XlPageOrientation.xlLandscape;
				ws.PageSetup.LeftMargin		= ws.Application.InchesToPoints(0.393700787401575);
				ws.PageSetup.RightMargin	= ws.Application.InchesToPoints(0.393700787401575);
				ws.PageSetup.TopMargin		= ws.Application.InchesToPoints(0.590551181102362);
				ws.PageSetup.BottomMargin	= ws.Application.InchesToPoints(0.590551181102362);
				ws.PageSetup.HeaderMargin	= ws.Application.InchesToPoints(0.511811023622047);
				ws.PageSetup.FooterMargin	= ws.Application.InchesToPoints(0.511811023622047);
				ws.PageSetup.PrintArea		= "$A:$K";
			}
			catch(Exception E)
			{
				Db.SetException(E);
			}
		}
		protected void DownloadReportGuaranty(Excel.Worksheet ws, int row)
		{
			// Выгружаем данные на определенный лист в Excel
			Excel.Range cell;
			Excel.Range rng;
			string		txt;
			string		rowTxt;
				
			rowTxt		= row.ToString();
			try
			{
				rng							= ws.Rows;
				// Дата закрытия наряда
				txt							= cardReport.CloseDate;
				cell						= rng.get_Range("A" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Номер карточки
				txt							= cardReport.WarrantNumber;
				cell						= rng.get_Range("B" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Дата 
				txt							= cardReport.WarrantDate;
				cell						= rng.get_Range("C" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ТО 
				//txt							= cardReport.CardTOGuaranty;
				cell						= rng.get_Range("D" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2 = cardReport.CardTOGuarantyD;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Масла 
				//txt							= cardReport.CardOilGuaranty;
				cell						= rng.get_Range("E" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2 = cardReport.CardOilGuarantyD;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Работы - без мойки
				//txt							= cardReport.CardWorkNoWashGuaranty;
				cell						= rng.get_Range("F" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2 = cardReport.CardWorkNoWashGuarantyD;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Мойки
				//txt							= cardReport.CardWashGuaranty;
				cell						= rng.get_Range("G" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2 = cardReport.CardWashGuarantyD;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Детали 
				//txt							= cardReport.CardDetailGuaranty;
				//txt							= cardReport.CardDetailGuarantyInput;
				cell						= rng.get_Range("H" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2 = cardReport.CardDetailGuarantyInputD;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Вид Гарантиии
				txt							= cardReport.CardGuarantyType;
				cell						= rng.get_Range("I" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Подразделение
				txt							= cardReport.CardWorkshopTxt;
				cell						= rng.get_Range("J" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Модель 
				txt							= cardReport.AutoModel;
				cell						= rng.get_Range("L" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// VIN 
				txt							= cardReport.AutoVIN;
				cell						= rng.get_Range("M" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Регистрационный знак 
				txt							= cardReport.AutoSignNo;
				cell						= rng.get_Range("N" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
			}
			catch(Exception E)
			{
				Db.SetException(E);
				return;
			}
			return;
		}

		protected void DownloadReportGuaranty(Excel.Worksheet ws)
		{
			//Excel.Range rng = ws.UsedRange;
			//int count = rng.Rows.Count;
			int count	= 0;
			int index	= ws.Index;
			if(ComplexReport.current_row[index] == 0)
			{
				Excel.Range rng = ws.UsedRange;
				count = rng.Rows.Count;
			}
			else
			{
				count = ComplexReport.current_row[index];
				ComplexReport.current_row[index]++;
			}
			DownloadReportGuaranty(ws, count + 1);
		}
		#endregion

		#region Лист ППП
		protected static void DownloadTitlePPP(Excel.Worksheet ws)
		{
			// Выгружаем заголовок на определенный лист в Excel
			Excel.Range cell;
			Excel.Range rng;
			string		txt;
			try
			{
				rng							= ws.Rows;
				// Дата закрытия
				txt	= "Закрытие";
				cell							= rng.get_Range("A1", Missing.Value);
				cell.Value2						= txt;
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 9;
				cell.EntireColumn.Font.Size		= 8;
				// Номер Заказ-наряда
				txt	= "Номер";
				cell							= rng.get_Range("B1", Missing.Value);
				cell.Value2						= txt;
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 8;
				cell.EntireColumn.Font.Size		= 8;
				// Дата заказ-наряда
				txt	= "Дата";
				cell							= rng.get_Range("C1", Missing.Value);
				cell.Value2						= txt;
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 9;
				cell.EntireColumn.Font.Size		= 8;
				// Работы ТО
				txt	= "ТО";
				cell								= rng.get_Range("D1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 9;
				cell.EntireColumn.Font.Size			= 8;
				cell.EntireColumn.NumberFormatLocal	= "# ##0,00";
				// Работы ТО - Гарантия
				txt	= "ТО Гар.";
				cell								= rng.get_Range("E1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 9;
				cell.EntireColumn.Font.Size			= 8;
				cell.EntireColumn.NumberFormatLocal	= "# ##0,00";
				// Жидкости в заказ-наряде
				txt	= "Жидкости";
				cell								= rng.get_Range("F1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 9;
				cell.EntireColumn.Font.Size			= 8;
				cell.EntireColumn.NumberFormatLocal	= "# ##0,00";
				// Жидкости в заказ-наряде - Гарантия
				txt	= "Жидкости - Гар.";
				cell								= rng.get_Range("G1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 9;
				cell.EntireColumn.Font.Size			= 8;
				cell.EntireColumn.NumberFormatLocal	= "# ##0,00";
				// Работы без мойки
				txt	= "Работы";
				cell								= rng.get_Range("H1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 9;
				cell.EntireColumn.Font.Size			= 8;
				cell.EntireColumn.NumberFormatLocal	= "# ##0,00";
				// Моечно-уборочные работы
				txt	= "Мойка";
				cell								= rng.get_Range("I1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 9;
				cell.EntireColumn.Font.Size			= 8;
				cell.EntireColumn.NumberFormatLocal	= "# ##0,00";
				// Работы без мойки - Гарантия
				txt	= "Работы Гар.";
				cell								= rng.get_Range("J1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 9;
				cell.EntireColumn.Font.Size			= 8;
				cell.EntireColumn.NumberFormatLocal	= "# ##0,00";
				// Моечно-уборочные работы - Гарантия
				txt	= "Мойка Гар.";
				cell								= rng.get_Range("K1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 9;
				cell.EntireColumn.Font.Size			= 8;
				cell.EntireColumn.NumberFormatLocal	= "# ##0,00";
				// Сумма деталей
				txt	= "Детали";
				cell								= rng.get_Range("L1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 9;
				cell.EntireColumn.Font.Size			= 8;
				cell.EntireColumn.NumberFormatLocal	= "# ##0,00";
				// Сумма деталей - Гарантия
				txt	= "Детали - Гар.";
				cell								= rng.get_Range("M1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 9;
				cell.EntireColumn.Font.Size			= 8;
				cell.EntireColumn.NumberFormatLocal	= "# ##0,00";
				
				// Пустые строчки для доп информации
				// 1
				cell							= rng.get_Range("N1", Missing.Value);
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 5;
				cell.EntireColumn.Font.Size		= 8;
				// Модель
				txt	= "Модель";
				cell							= rng.get_Range("O1", Missing.Value);
				cell.Value2						= txt;
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 15;
				cell.EntireColumn.Font.Size		= 8;
				// VIN
				txt	= "VIN";
				cell							= rng.get_Range("P1", Missing.Value);
				cell.Value2						= txt;
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 20;
				cell.EntireColumn.Font.Size		= 8;

				// Параметры печати
				ws.PageSetup.Orientation	= Excel.XlPageOrientation.xlLandscape;
				ws.PageSetup.LeftMargin		= ws.Application.InchesToPoints(0.393700787401575);
				ws.PageSetup.RightMargin	= ws.Application.InchesToPoints(0.393700787401575);
				ws.PageSetup.TopMargin		= ws.Application.InchesToPoints(0.590551181102362);
				ws.PageSetup.BottomMargin	= ws.Application.InchesToPoints(0.590551181102362);
				ws.PageSetup.HeaderMargin	= ws.Application.InchesToPoints(0.511811023622047);
				ws.PageSetup.FooterMargin	= ws.Application.InchesToPoints(0.511811023622047);
				ws.PageSetup.PrintArea		= "$A:$N";
			}
			catch(Exception E)
			{
				Db.SetException(E);
			}
		}
		protected void DownloadReportPPP(Excel.Worksheet ws, int row)
		{
			// Выгружаем данные на определенный лист в Excel
			Excel.Range cell;
			Excel.Range rng;
			string		txt;
			string		rowTxt;
				
			rowTxt		= row.ToString();
			try
			{
				rng							= ws.Rows;
				// Дата закрытия наряда
				txt							= cardReport.CloseDate;
				cell						= rng.get_Range("A" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Номер карточки
				txt							= cardReport.WarrantNumber;
				cell						= rng.get_Range("B" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Дата 
				txt							= cardReport.WarrantDate;
				cell						= rng.get_Range("C" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ТО 
				//txt							= cardReport.CardTO;
				cell						= rng.get_Range("D" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2 = cardReport.CardTOD;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ТО - Гарантия
				//txt							= cardReport.CardTOGuaranty;
				cell						= rng.get_Range("E" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2 = cardReport.CardTOGuarantyD;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Масла 
				//txt							= cardReport.CardOil;
				cell						= rng.get_Range("F" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2 = cardReport.CardOilD;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Масла - Гарантия
				//txt							= cardReport.CardOilGuaranty;
				cell						= rng.get_Range("G" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2 = cardReport.CardOilGuarantyD;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Работы - без мойки
				//txt							= cardReport.CardWorkNoWash;
				cell						= rng.get_Range("H" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2 = cardReport.CardWorkNoWashD;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Мойки
				//txt							= cardReport.CardWash;
				cell						= rng.get_Range("I" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2 = cardReport.CardWashD;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Работы - без мойки - Гарантия
				//txt							= cardReport.CardWorkNoWashGuaranty;
				cell						= rng.get_Range("J" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2 = cardReport.CardWorkNoWashGuarantyD;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Мойки - Гарантия
				//txt							= cardReport.CardWashGuaranty;
				cell						= rng.get_Range("K" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2 = cardReport.CardWashGuarantyD;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Детали 
				//txt							= cardReport.CardDetail;
				cell						= rng.get_Range("L" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2 = cardReport.CardDetailD;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Детали - Гарантия
				//txt							= cardReport.CardDetailGuaranty;
				cell						= rng.get_Range("M" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2 = cardReport.CardDetailGuarantyD;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Модель 
				txt							= cardReport.AutoModel;
				cell						= rng.get_Range("O" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// VIN 
				txt							= cardReport.AutoVIN;
				cell						= rng.get_Range("P" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
			}
			catch(Exception E)
			{
				Db.SetException(E);
				return;
			}
			return;
		}
		protected void DownloadReportPPP(Excel.Worksheet ws)
		{
			//Excel.Range rng = ws.UsedRange;
			//int count = rng.Rows.Count;
			int count	= 0;
			int index	= ws.Index;
			if(ComplexReport.current_row[index] == 0)
			{
				Excel.Range rng = ws.UsedRange;
				count = rng.Rows.Count;
			}
			else
			{
				count = ComplexReport.current_row[index];
				ComplexReport.current_row[index]++;
			}
			DownloadReportPPP(ws, count + 1);
		}
		#endregion
		
		#region Работы без масел, ТО и гарантии
		protected static void DownloadTitleWork(Excel.Worksheet ws)
		{
			// Выгружаем заголовок на определенный лист в Excel
			Excel.Range cell;
			Excel.Range rng;
			string		txt;
			try
			{
				rng							= ws.Rows;
				// Дата закрытия
				txt	= "Закрытие";
				cell							= rng.get_Range("A1", Missing.Value);
				cell.Value2						= txt;
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 9;
				cell.EntireColumn.Font.Size		= 8;
				// Номер Заказ-наряда
				txt	= "Номер";
				cell							= rng.get_Range("B1", Missing.Value);
				cell.Value2						= txt;
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 8;
				cell.EntireColumn.Font.Size		= 8;
				// Дата заказ-наряда
				txt	= "Дата";
				cell							= rng.get_Range("C1", Missing.Value);
				cell.Value2						= txt;
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 9;
				cell.EntireColumn.Font.Size		= 8;
				// Работы без мойки
				txt	= "Работы";
				cell								= rng.get_Range("D1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 9;
				cell.EntireColumn.Font.Size			= 8;
				cell.EntireColumn.NumberFormatLocal	= "# ##0,00";
				// Моечно-уборочные работы
				txt	= "Мойка";
				cell								= rng.get_Range("E1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 9;
				cell.EntireColumn.Font.Size			= 8;
				cell.EntireColumn.NumberFormatLocal	= "# ##0,00";
				// Сумма деталей
				txt	= "Детали";
				cell								= rng.get_Range("F1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 9;
				cell.EntireColumn.Font.Size			= 8;
				cell.EntireColumn.NumberFormatLocal	= "# ##0,00";
				// Гарантия Да/Нет
				txt	= "Гар.";
				cell							= rng.get_Range("G1", Missing.Value);
				cell.Value2						= txt;
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignCenter;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 4;
				cell.EntireColumn.Font.Size		= 8;
				// Пустые строчки для доп информации
				// 1
				cell							= rng.get_Range("H1", Missing.Value);
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 15;
				cell.EntireColumn.Font.Size		= 8;
				// 1
				cell							= rng.get_Range("I1", Missing.Value);
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 15;
				cell.EntireColumn.Font.Size		= 8;
				// Модель
				txt	= "Модель";
				cell							= rng.get_Range("J1", Missing.Value);
				cell.Value2						= txt;
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 15;
				cell.EntireColumn.Font.Size		= 8;
				// VIN
				txt	= "VIN";
				cell							= rng.get_Range("K1", Missing.Value);
				cell.Value2						= txt;
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 20;
				cell.EntireColumn.Font.Size		= 8;		
				// Регистрационный знак
				txt	= "Рег.Знак";
				cell							= rng.get_Range("L1", Missing.Value);
				cell.Value2						= txt;
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 9;
				cell.EntireColumn.Font.Size		= 8;

				// Параметры печати
				ws.PageSetup.Orientation	= Excel.XlPageOrientation.xlLandscape;
				ws.PageSetup.LeftMargin		= ws.Application.InchesToPoints(0.393700787401575);
				ws.PageSetup.RightMargin	= ws.Application.InchesToPoints(0.393700787401575);
				ws.PageSetup.TopMargin		= ws.Application.InchesToPoints(0.590551181102362);
				ws.PageSetup.BottomMargin	= ws.Application.InchesToPoints(0.590551181102362);
				ws.PageSetup.HeaderMargin	= ws.Application.InchesToPoints(0.511811023622047);
				ws.PageSetup.FooterMargin	= ws.Application.InchesToPoints(0.511811023622047);
				ws.PageSetup.PrintArea		= "$A:$I";
			}
			catch(Exception E)
			{
				Db.SetException(E);
			}
		}
		protected void DownloadReportWork(Excel.Worksheet ws, int row)
		{
			// Выгружаем данные на определенный лист в Excel
			Excel.Range cell;
			Excel.Range rng;
			string		txt;
			string		rowTxt;
				
			rowTxt		= row.ToString();
			try
			{
				rng							= ws.Rows;
				// Дата закрытия наряда
				txt							= cardReport.CloseDate;
				cell						= rng.get_Range("A" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Номер карточки
				txt							= cardReport.WarrantNumber;
				cell						= rng.get_Range("B" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Дата 
				txt							= cardReport.WarrantDate;
				cell						= rng.get_Range("C" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Работы - без мойки
				//txt							= cardReport.CardWorkNoWash;
				cell						= rng.get_Range("D" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2 = cardReport.CardWorkNoWashD;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Мойки
				//txt							= cardReport.CardWash;
				cell						= rng.get_Range("E" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2 = cardReport.CardWashD;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Детали 
				//txt							= cardReport.CardDetail;
				cell						= rng.get_Range("F" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2 = cardReport.CardDetailD;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Гарантия ДА/НЕТ
				if(cardReport.HaveGuaranty())
					txt							= "ДА";
				else
					txt							= "";
				cell						= rng.get_Range("G" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Модель 
				txt							= cardReport.AutoModel;
				cell						= rng.get_Range("J" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// VIN 
				txt							= cardReport.AutoVIN;
				cell						= rng.get_Range("K" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Регистрационный знак 
				txt							= cardReport.AutoSignNo;
				cell						= rng.get_Range("L" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
			}
			catch(Exception E)
			{
				Db.SetException(E);
				return;
			}
			return;
		}

		protected void DownloadReportWork(Excel.Worksheet ws)
		{
			//Excel.Range rng = ws.UsedRange;
			//int count = rng.Rows.Count;
			int count	= 0;
			int index	= ws.Index;
			if(ComplexReport.current_row[index] == 0)
			{
				Excel.Range rng = ws.UsedRange;
				count = rng.Rows.Count;
			}
			else
			{
				count = ComplexReport.current_row[index];
				ComplexReport.current_row[index]++;
			}
			DownloadReportWork(ws, count + 1);
		}
		#endregion

		#region Большой сводный отчет
		protected static void DownloadTitleReport(Excel.Worksheet ws)
		{
			// Выгружаем заголовок на определенный лист в Excel
			Excel.Range cell;
			Excel.Range rng;
			string		txt;
			try
			{
				rng							= ws.Rows;
				// Дата закрытия
				txt	= "Закрытие";
				cell							= rng.get_Range("A1", Missing.Value);
				cell.Value2						= txt;
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 9;
				cell.EntireColumn.Font.Size		= 8;
				// Номер Заказ-наряда
				txt	= "Номер";
				cell							= rng.get_Range("B1", Missing.Value);
				cell.Value2						= txt;
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 8;
				cell.EntireColumn.Font.Size		= 8;
				// Дата заказ-наряда
				txt	= "Дата";
				cell							= rng.get_Range("C1", Missing.Value);
				cell.Value2						= txt;
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 9;
				cell.EntireColumn.Font.Size		= 8;
				// Работы ТО
				txt	= "ТО";
				cell								= rng.get_Range("D1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 9;
				cell.EntireColumn.Font.Size			= 8;
				cell.EntireColumn.NumberFormatLocal	= "# ##0,00";
				// Жидкости в заказ-наряде
				txt	= "Жидкости";
				cell								= rng.get_Range("E1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 9;
				cell.EntireColumn.Font.Size			= 8;
				cell.EntireColumn.NumberFormatLocal	= "# ##0,00";
				// Себестоимость
				txt	= "Себест.";
				cell								= rng.get_Range("F1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 9;
				cell.EntireColumn.Font.Size			= 8;
				cell.EntireColumn.NumberFormatLocal	= "# ##0,00";
				// Работы без мойки
				txt	= "Работы";
				cell								= rng.get_Range("G1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 9;
				cell.EntireColumn.Font.Size			= 8;
				cell.EntireColumn.NumberFormatLocal	= "# ##0,00";
				// Моечно-уборочные работы
				txt	= "Мойка";
				cell								= rng.get_Range("H1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 9;
				cell.EntireColumn.Font.Size			= 8;
				cell.EntireColumn.NumberFormatLocal	= "# ##0,00";
				// ИТОГ - работы
				txt	= "Итог";
				cell								= rng.get_Range("I1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 9;
				cell.EntireColumn.Font.Size			= 8;
				cell.EntireColumn.NumberFormatLocal	= "# ##0,00";
				cell.EntireColumn.Font.Bold			= true;
				// Сумма деталей
				txt	= "Детали";
				cell								= rng.get_Range("J1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 9;
				cell.EntireColumn.Font.Size			= 8;
				cell.EntireColumn.NumberFormatLocal	= "# ##0,00";
				// Себестоимость деталей
				txt	= "Себест.";
				cell								= rng.get_Range("K1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 9;
				cell.EntireColumn.Font.Size			= 8;
				cell.EntireColumn.NumberFormatLocal	= "# ##0,00";

				// Работы ТО - Гарантия
				txt	= "ТО Гар.";
				cell								= rng.get_Range("L1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 9;
				cell.EntireColumn.Font.Size			= 8;
				cell.EntireColumn.NumberFormatLocal	= "# ##0,00";
				// Жидкости в заказ-наряде - Гарантия
				txt	= "Жидк.-Гар.";
				cell								= rng.get_Range("M1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 9;
				cell.EntireColumn.Font.Size			= 8;
				cell.EntireColumn.NumberFormatLocal	= "# ##0,00";
				// Себестоимость жидкости - Гар.
				txt	= "Себест.";
				cell								= rng.get_Range("N1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 9;
				cell.EntireColumn.Font.Size			= 8;
				cell.EntireColumn.NumberFormatLocal	= "# ##0,00";
				// Работы без мойки - Гарантия
				txt	= "Работы Гар.";
				cell								= rng.get_Range("O1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 9;
				cell.EntireColumn.Font.Size			= 8;
				cell.EntireColumn.NumberFormatLocal	= "# ##0,00";
				// Моечно-уборочные работы - Гарантия
				txt	= "Мойка Гар.";
				cell								= rng.get_Range("P1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 9;
				cell.EntireColumn.Font.Size			= 8;
				cell.EntireColumn.NumberFormatLocal	= "# ##0,00";
				// Сумма деталей - Гарантия
				txt	= "Дет.-Гар.";
				cell								= rng.get_Range("Q1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 9;
				cell.EntireColumn.Font.Size			= 8;
				cell.EntireColumn.NumberFormatLocal	= "# ##0,00";
				// Себестоимость деталей - Гарантия
				txt	= "Себест.";
				cell								= rng.get_Range("R1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 9;
				cell.EntireColumn.Font.Size			= 8;
				cell.EntireColumn.NumberFormatLocal	= "# ##0,00";
				// Гарантия Работы/Детали
				cell							= rng.get_Range("S1", Missing.Value);
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 8;
				cell.EntireColumn.Font.Size		= 8;
				// Гарантия Вид
				cell							= rng.get_Range("T1", Missing.Value);
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 20;
				cell.EntireColumn.Font.Size		= 8;
				// Подразделение
				cell							= rng.get_Range("U1", Missing.Value);
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 20;
				cell.EntireColumn.Font.Size		= 8;
				// Модель
				txt	= "Модель";
				cell							= rng.get_Range("V1", Missing.Value);
				cell.Value2						= txt;
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 15;
				cell.EntireColumn.Font.Size		= 8;
				// VIN
				txt	= "VIN";
				cell							= rng.get_Range("W1", Missing.Value);
				cell.Value2						= txt;
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 20;
				cell.EntireColumn.Font.Size		= 8;

				// Параметры печати
				ws.PageSetup.Orientation	= Excel.XlPageOrientation.xlLandscape;
				ws.PageSetup.LeftMargin		= ws.Application.InchesToPoints(0.393700787401575);
				ws.PageSetup.RightMargin	= ws.Application.InchesToPoints(0.393700787401575);
				ws.PageSetup.TopMargin		= ws.Application.InchesToPoints(0.590551181102362);
				ws.PageSetup.BottomMargin	= ws.Application.InchesToPoints(0.590551181102362);
				ws.PageSetup.HeaderMargin	= ws.Application.InchesToPoints(0.511811023622047);
				ws.PageSetup.FooterMargin	= ws.Application.InchesToPoints(0.511811023622047);
				ws.PageSetup.PrintArea		= "$A:$X";
			}
			catch(Exception E)
			{
				Db.SetException(E);
			}
		}
		protected void DownloadReportReport(Excel.Worksheet ws, int row)
		{
			// Выгружаем данные на определенный лист в Excel
			Excel.Range cell;
			Excel.Range rng;
			string		txt;
			string		rowTxt;
				
			rowTxt		= row.ToString();
			try
			{
				rng							= ws.Rows;
				// Дата закрытия наряда
				txt							= cardReport.CloseDate;
				cell						= rng.get_Range("A" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Номер карточки
				txt							= cardReport.WarrantNumber;
				cell						= rng.get_Range("B" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Дата 
				txt							= cardReport.WarrantDate;
				cell						= rng.get_Range("C" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ТО 
				//txt							= cardReport.CardTO;
				cell						= rng.get_Range("D" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2 = cardReport.CardTOD;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Масла 
				//txt							= cardReport.CardOil;
				cell						= rng.get_Range("E" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2 = cardReport.CardOilD;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Масла 
				//txt							= cardReport.CardOilInput;
				cell						= rng.get_Range("F" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2 = cardReport.CardOilInputD;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Работы - без мойки
				//txt							= cardReport.CardWorkNoWash;
				cell						= rng.get_Range("G" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2 = cardReport.CardWorkNoWash;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Мойки
				//txt							= cardReport.CardWash;
				cell						= rng.get_Range("H" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2 = cardReport.CardWashD;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Итог работ
				//txt							= cardReport.Work;
				cell						= rng.get_Range("I" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.Value2 = cardReport.WorkD;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Детали 
				//txt							= cardReport.CardDetail;
				cell						= rng.get_Range("J" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2 = cardReport.CardDetailD;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Себестоимость - Детали 
				//txt							= cardReport.CardDetailInput;
				cell						= rng.get_Range("K" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2 = cardReport.CardDetailInputD;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);

				// ТО - Гарантия
				//txt							= cardReport.CardTOGuaranty;
				cell							= rng.get_Range("L" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2						= cardReport.CardTOGuarantyD;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Масла - Гарантия
				//txt							= cardReport.CardOilGuaranty;
				cell						= rng.get_Range("M" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2					= cardReport.CardOilGuarantyD;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Себестоимость Масла - Гарантия
				//txt							= cardReport.CardOilGuarantyInput;
				cell						= rng.get_Range("N" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2					= cardReport.CardOilGuarantyInputD;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Работы - без мойки - Гарантия
				//txt							= cardReport.CardWorkNoWashGuaranty;
				cell						= rng.get_Range("O" + rowTxt, Missing.Value);
				//cell.Value2				= txt;
				cell.Value2					= cardReport.CardWorkNoWashGuarantyD;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Мойки - Гарантия
				//txt							= cardReport.CardWashGuaranty;
				cell						= rng.get_Range("P" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2					= cardReport.CardWashGuarantyD;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Детали - Гарантия
				//txt							= cardReport.CardDetailGuaranty;
				cell						= rng.get_Range("Q" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2					= cardReport.CardDetailGuarantyD;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Себестоимость Детали - Гарантия
				//txt							= cardReport.CardDetailGuarantyInput;
				cell						= rng.get_Range("R" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2					= cardReport.CardDetailGuarantyInputD;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);

				// Гарантия Работы/Детали
				txt							= cardReport.IsGuarantyWorkDetailTxt;
				cell						= rng.get_Range("S" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Гарантия вид
				if(cardReport.IsGuarantyWorkDetail)
					txt							= cardReport.CardGuarantyType;
				else
					txt							= "";
				cell						= rng.get_Range("T" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Подразделение
				txt							= cardReport.CardWorkshopTxt;
				cell						= rng.get_Range("U" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Модель 
				txt							= cardReport.AutoModel;
				cell						= rng.get_Range("V" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// VIN 
				txt							= cardReport.AutoVIN;
				cell						= rng.get_Range("W" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
			}
			catch(Exception E)
			{
				Db.SetException(E);
				return;
			}
			return;
		}
		protected void DownloadReportReport(Excel.Worksheet ws)
		{
			//Excel.Range rng = ws.UsedRange;
			//int count = rng.Rows.Count;
			int count	= 0;
			int index	= ws.Index;
			if(ComplexReport.current_row[index] == 0)
			{
				Excel.Range rng = ws.UsedRange;
				count = rng.Rows.Count;
			}
			else
			{
				count = ComplexReport.current_row[index];
				ComplexReport.current_row[index]++;
			}
			DownloadReportReport(ws, count + 1);
		}
		#endregion

		#region Гарантия - Расширенно
		protected static void DownloadTitleGuarantyExtend(Excel.Worksheet ws)
		{
			// Выгружаем заголовок на определенный лист в Excel
			Excel.Range cell;
			Excel.Range rng;
			string		txt;
			try
			{
				rng							= ws.Rows;
				// Дата закрытия
				txt	= "Закрытие";
				cell							= rng.get_Range("A1", Missing.Value);
				cell.Value2						= txt;
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 9;
				cell.EntireColumn.Font.Size		= 8;
				// Номер Заказ-наряда
				txt	= "Номер";
				cell							= rng.get_Range("B1", Missing.Value);
				cell.Value2						= txt;
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 8;
				cell.EntireColumn.Font.Size		= 8;
				// Дата заказ-наряда
				txt	= "Дата";
				cell							= rng.get_Range("C1", Missing.Value);
				cell.Value2						= txt;
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 9;
				cell.EntireColumn.Font.Size		= 8;
				// Работы ТО
				txt	= "ТО";
				cell								= rng.get_Range("D1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 9;
				cell.EntireColumn.Font.Size			= 8;
				cell.EntireColumn.NumberFormatLocal	= "# ##0,00";
				// Жидкости в заказ-наряде
				txt	= "Жидкости";
				cell								= rng.get_Range("E1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 9;
				cell.EntireColumn.Font.Size			= 8;
				cell.EntireColumn.NumberFormatLocal	= "# ##0,00";
				// Работы без мойки
				txt	= "Работы";
				cell								= rng.get_Range("F1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 9;
				cell.EntireColumn.Font.Size			= 8;
				cell.EntireColumn.NumberFormatLocal	= "# ##0,00";
				// Моечно-уборочные работы
				txt	= "Мойка";
				cell								= rng.get_Range("G1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 9;
				cell.EntireColumn.Font.Size			= 8;
				cell.EntireColumn.NumberFormatLocal	= "# ##0,00";
				// Сумма деталей
				txt	= "Детали";
				cell								= rng.get_Range("H1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 9;
				cell.EntireColumn.Font.Size			= 8;
				cell.EntireColumn.NumberFormatLocal	= "# ##0,00";
				// Гарантия Вид
				txt	= "Гар.";
				cell							= rng.get_Range("I1", Missing.Value);
				cell.Value2						= txt;
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 22;
				cell.EntireColumn.Font.Size		= 8;
				// Подразделение
				cell							= rng.get_Range("J1", Missing.Value);
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 10;
				cell.EntireColumn.Font.Size		= 8;
				// Пустые строчки для доп информации
				// 1
				cell							= rng.get_Range("K1", Missing.Value);
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 5;
				cell.EntireColumn.Font.Size		= 8;
				// Модель
				txt	= "Модель";
				cell							= rng.get_Range("L1", Missing.Value);
				cell.Value2						= txt;
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 15;
				cell.EntireColumn.Font.Size		= 8;
				// VIN
				txt	= "VIN";
				cell							= rng.get_Range("M1", Missing.Value);
				cell.Value2						= txt;
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 20;
				cell.EntireColumn.Font.Size		= 8;		
				// Регистрационный знак
				txt	= "Рег.Знак";
				cell							= rng.get_Range("N1", Missing.Value);
				cell.Value2						= txt;
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 9;
				cell.EntireColumn.Font.Size		= 8;

				// Параметры печати
				ws.PageSetup.Orientation	= Excel.XlPageOrientation.xlLandscape;
				ws.PageSetup.LeftMargin		= ws.Application.InchesToPoints(0.393700787401575);
				ws.PageSetup.RightMargin	= ws.Application.InchesToPoints(0.393700787401575);
				ws.PageSetup.TopMargin		= ws.Application.InchesToPoints(0.590551181102362);
				ws.PageSetup.BottomMargin	= ws.Application.InchesToPoints(0.590551181102362);
				ws.PageSetup.HeaderMargin	= ws.Application.InchesToPoints(0.511811023622047);
				ws.PageSetup.FooterMargin	= ws.Application.InchesToPoints(0.511811023622047);
				ws.PageSetup.PrintArea		= "$A:$K";
			}
			catch(Exception E)
			{
				Db.SetException(E);
			}
		}
		protected void DownloadReportGuarantyExtend(Excel.Worksheet ws, int row)
		{
			// Выгружаем данные на определенный лист в Excel
			Excel.Range cell;
			Excel.Range rng;
			string		txt;
			string		rowTxt;
				
			rowTxt		= row.ToString();
			try
			{
				rng							= ws.Rows;
				// Дата закрытия наряда
				txt							= cardReport.CloseDate;
				cell						= rng.get_Range("A" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Номер карточки
				txt							= cardReport.WarrantNumber;
				cell						= rng.get_Range("B" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Дата 
				txt							= cardReport.WarrantDate;
				cell						= rng.get_Range("C" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ТО 
				//txt							= cardReport.CardTOGuaranty;
				cell						= rng.get_Range("D" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2					= cardReport.CardTOGuarantyD;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Масла 
				//txt							= cardReport.CardOilGuaranty;
				cell						= rng.get_Range("E" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2					= cardReport.CardOilGuarantyD;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Работы - без мойки
				//txt							= cardReport.CardWorkNoWashGuaranty;
				cell						= rng.get_Range("F" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2 = cardReport.CardWorkNoWashGuarantyD;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Мойки
				//txt							= cardReport.CardWashGuaranty;
				cell						= rng.get_Range("G" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2 = cardReport.CardWashGuarantyD;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Детали 
				//txt							= cardReport.CardDetailGuaranty;
				cell						= rng.get_Range("H" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2 = cardReport.CardDetailGuarantyD;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);

				
				// Вид Гарантиии
				txt							= cardReport.CardGuarantyType;
				cell						= rng.get_Range("I" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Подразделение
				txt							= cardReport.CardWorkshopTxt;
				cell						= rng.get_Range("J" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Модель 
				txt							= cardReport.AutoModel;
				cell						= rng.get_Range("L" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// VIN 
				txt							= cardReport.AutoVIN;
				cell						= rng.get_Range("M" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Регистрационный знак 
				txt							= cardReport.AutoSignNo;
				cell						= rng.get_Range("N" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
			}
			catch(Exception E)
			{
				Db.SetException(E);
				return;
			}
			return;
		}

		protected bool DownloadReportGuarantyExtend(Excel.Worksheet ws, int row, int position)
		{
			// Выгружаем данные на определенный лист в Excel
			Excel.Range cell;
			Excel.Range rng;
			string		txt;
			string		rowTxt;
				
			rowTxt		= row.ToString();

			if(cardReport.GuarantyArray.Count <= position) return false;
			ComplexReportData.Guaranty guaranty = (ComplexReportData.Guaranty)cardReport.GuarantyArray[position];
			try
			{
				rng							= ws.Rows;
				// Дата закрытия наряда
				txt							= cardReport.CloseDate;
				cell						= rng.get_Range("A" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Номер карточки
				txt							= cardReport.WarrantNumber;
				cell						= rng.get_Range("B" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Дата 
				txt							= cardReport.WarrantDate;
				cell						= rng.get_Range("C" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ТО
				if(guaranty.summ_to_guaranty != 0.0F)
					//txt							= guaranty.summ_to_guaranty.ToString();//cardReport.CardTOGuaranty;
					txt							= guaranty.summ_to_guaranty.ToString();//cardReport.CardTOGuaranty;
				else
					txt = "";
				cell						= rng.get_Range("D" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2 = guaranty.summ_to_guaranty;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Масла 
				if(guaranty.summ_oil_guaranty_input != 0.0F)
					txt							= guaranty.summ_oil_guaranty_input.ToString();//cardReport.CardOilGuaranty;
				else
					txt = "";
				cell						= rng.get_Range("E" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2 = guaranty.summ_oil_guaranty_input;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Работы - без мойки
				if(guaranty.summ_work_guaranty - guaranty.summ_wash_guaranty != 0.0F)
					txt							= (guaranty.summ_work_guaranty - guaranty.summ_wash_guaranty).ToString();//cardReport.CardWorkNoWashGuaranty;
				else
					txt = "";
				cell						= rng.get_Range("F" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2 = (guaranty.summ_work_guaranty - guaranty.summ_wash_guaranty);
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Мойки
				if(guaranty.summ_wash_guaranty != 0.0F)
					txt							= guaranty.summ_wash_guaranty.ToString();//cardReport.CardWashGuaranty;
				else
					txt = "";
				cell						= rng.get_Range("G" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2 = guaranty.summ_wash_guaranty;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Детали
				if(guaranty.is_detail_guaranty == true)
					txt							= guaranty.summ_detail_guaranty_input.ToString();// cardReport.CardDetailGuaranty;
				else
					txt = "";
				cell						= rng.get_Range("H" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2 = guaranty.summ_detail_guaranty_input;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Вид Гарантиии
				txt							= guaranty.guaranty_name;//cardReport.CardGuarantyType;
				cell						= rng.get_Range("I" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Подразделение
				txt							= cardReport.CardWorkshopTxt;
				cell						= rng.get_Range("J" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Модель 
				txt							= cardReport.AutoModel;
				cell						= rng.get_Range("L" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// VIN 
				txt							= cardReport.AutoVIN;
				cell						= rng.get_Range("M" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Регистрационный знак 
				txt							= cardReport.AutoSignNo;
				cell						= rng.get_Range("N" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
			}
			catch(Exception E)
			{
				Db.SetException(E);
				return true;
			}
			return true;
		}

		protected void DownloadReportGuarantyExtend(Excel.Worksheet ws)
		{
			//Excel.Range rng = ws.UsedRange;
			//int count = rng.Rows.Count;
			int count	= 0;
			int index	= ws.Index;
			if(ComplexReport.current_row[index] == 0)
			{
				Excel.Range rng = ws.UsedRange;
				count = rng.Rows.Count;
			}
			else
			{
				count = ComplexReport.current_row[index];
				//ExcelCardReport.current_row[index]++;
			}
			//DownloadReportGuarantyExtend(ws, count + 1);
			int i = 0;
			//count++;
			while(DownloadReportGuarantyExtend(ws, count + 1, i) != false)
			{
				i++;
				count++;
				//ExcelCardReport.current_row[index]++;
			}
			//ExcelCardReport.current_row[index] = ExcelCardReport.current_row[index] + i;
		}
		#endregion

		protected void DownloadReport()
		{
			// Определяем необходимые типы выгрузки для карточки
			switch(cardReport.CardWorkshop)
			{
				case 1:			// Сервис
					if(cardReport.HavePPP() == true)
						this.DownloadReportPPP((Excel.Worksheet)workbook_.Worksheets[4]);
					else
					{
						if(cardReport.HaveTO() == true)
						{
							if(cardReport.Is_Cashless() == false)
							{
								this.DownloadReportTO((Excel.Worksheet)workbook_.Worksheets[1]);
							}
							else
							{
								this.DownloadReportTO((Excel.Worksheet)workbook_.Worksheets[2]);
							}
						}
					}
					break;
				case 2:			// Тюнинг
					this.DownloadReportWork((Excel.Worksheet)workbook_.Worksheets[6]); // 5 на 6 ИСПРАВЛЕНИЕ 2018
					break;
				case 3:			// Малярка
					this.DownloadReportWork((Excel.Worksheet)workbook_.Worksheets[7+1]);
					break;
				case 5:			// Мойка
                    this.DownloadReportWork((Excel.Worksheet)workbook_.Worksheets[8 + 1]);
					break;
				case 7:			// Музыка
                    this.DownloadReportWork((Excel.Worksheet)workbook_.Worksheets[6]);
					break;
				case 11:		// Музыка сервис
                    this.DownloadReportWork((Excel.Worksheet)workbook_.Worksheets[6]);
					break;
				case 12:		// Музыка предоплата
                    this.DownloadReportWork((Excel.Worksheet)workbook_.Worksheets[6]);
					break;
				case 9:			// Допы gпри продаже ИСПРАВЛЕНИЕ 2018
					//this.DownloadReportWork((Worksheet)workbook.Worksheets[7]);
                    //this.DownloadReportTO((Excel.Worksheet)workbook.Worksheets[7]);
                    this.DownloadReportWork((Excel.Worksheet)workbook_.Worksheets[5]); // Добавили ИСПРАВЛЕНИЕ 2018
					break;
				case 13:			// Допы сервиса
					//this.DownloadReportWork((Worksheet)workbook.Worksheets[7]);
                    this.DownloadReportTO((Excel.Worksheet)workbook_.Worksheets[7]);
					break;
                case 14:			// Трейд-ин
                    //this.DownloadReportWork((Worksheet)workbook.Worksheets[7]);
                    this.DownloadReportTO((Excel.Worksheet)workbook_.Worksheets[11+1]);
                    break;
				case 16:
					this.DownloadReportWork((Excel.Worksheet)workbook_.Worksheets[12+1]); // Добавили ИСПРАВЛЕНИЕ 2018
					break;
				default:
					break;
			}
			// Для любого подразделения вычленяем гарантию
			if(cardReport.HaveGuaranty() == true)
                this.DownloadReportGuaranty((Excel.Worksheet)workbook_.Worksheets[3]);

			// Общий список нарядов
            this.DownloadReportReport((Excel.Worksheet)workbook_.Worksheets[9 + 1]);

			// Для любого подразделения вычленяем гарантию
			if(cardReport.HaveGuaranty() == true)
                this.DownloadReportGuarantyExtend((Excel.Worksheet)workbook_.Worksheets[10 + 1]);
		}
	}
}
