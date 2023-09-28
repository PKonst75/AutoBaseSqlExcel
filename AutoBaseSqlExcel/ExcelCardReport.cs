using System;
using System.Collections;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for ExcelCardReport.
	/// </summary>
	public class ExcelCardReport:DbExcel
	{
		private Excel.Workbook		workbook;
		private DbCardReport		cardReport;
		private static int[]		current_row = new int[13+1];    // Последняя выгруженная строка отчета полистно
		private DtCard _card; // Карточка
		private DtTxtCard _cardTxt; // Текстовое отображение карточки

		public static void DownloadList(ArrayList array)
		{
			Excel.Workbook wb = null;
			//string				fileName;

			string fileName = SelectFileDialog("Выберете файл отчета для продолжения");
			if (fileName == DbExcel.CANCEL_SELECT) return; // Отказ от выбора файла
			
			foreach(object o in array)
			{
				DbCard card	= (DbCard)o;
				if(card != null)
				{
					ExcelCardReport excelCardReport	= new ExcelCardReport(card, wb);
					wb		= excelCardReport.StartDownLoad(fileName);
				}
			}
			ShowExcel(wb);
		}

		public ExcelCardReport(DbCard cardSrc, Excel.Workbook workbookSrc)
		{
			cardReport		= new DbCardReport(cardSrc);
			workbook		= workbookSrc;
		}

		public Excel.Workbook StartDownLoad(string file)
		{
			Excel.Application app;
			Excel.Workbook wb = null;

			try
			{
				if(workbook == null)
				{
					app			= new Excel.Application();				// Новый экземпляр приложения Excel
					// Если задан файл отчета, открываем существующую книгу
					if(file.Length == 0)
					{
						wb			= app.Workbooks.Add(Missing.Value);		// Новая книга Excel
						// Форматируем новую книгу
						while(wb.Worksheets.Count != 12+1)
							wb.Worksheets.Add(Missing.Value, wb.Worksheets[wb.Worksheets.Count], 1, Missing.Value);
						// Лист для ТО
						((Excel.Worksheet)wb.Worksheets[1]).Name	= "ТО";
						DownloadTitleTO((Excel.Worksheet)wb.Worksheets[1]);			// Создание заголовка
						// Лист для ТО
						((Excel.Worksheet)wb.Worksheets[2]).Name	= "ТО-бн";
						DownloadTitleTO((Excel.Worksheet)wb.Worksheets[2]);			// Создание заголовка
						// Лист для Гарантия
						((Excel.Worksheet)wb.Worksheets[3]).Name	= "Гарантия";
						DownloadTitleGuaranty((Excel.Worksheet)wb.Worksheets[3]);	// Создание заголовка
						// Лист для ППП
						((Excel.Worksheet)wb.Worksheets[4]).Name	= "ППП";
						DownloadTitlePPP((Excel.Worksheet)wb.Worksheets[4]);		// Создание заголовка
						// Лист для Тюнинга
						((Excel.Worksheet)wb.Worksheets[5]).Name	= "Продажи"; //ИСПАВЛЕНИЕ 2018
						DownloadTitleWork((Excel.Worksheet)wb.Worksheets[5]);		// Создание заголовка
						// Лист для Музыки
                        ((Excel.Worksheet)wb.Worksheets[6]).Name = "Тюнинг Музыка"; //ИСПАВЛЕНИЕ 2018
						DownloadTitleWork((Excel.Worksheet)wb.Worksheets[6]);		// Создание заголовка
						// Лист для допов
						((Excel.Worksheet)wb.Worksheets[7]).Name	= "Допы";
						//DownloadTitleWork((Excel.Worksheet)wb.Worksheets[7]);		// Создание заголовка
						DownloadTitleTO((Excel.Worksheet)wb.Worksheets[7]);		// Создание заголовка
						// Лист для Малярки
						((Excel.Worksheet)wb.Worksheets[7+1]).Name	= "Малярка";
						DownloadTitleWork((Excel.Worksheet)wb.Worksheets[7+1]);		// Создание заголовка
						// Лист для Мойки
						((Excel.Worksheet)wb.Worksheets[8+1]).Name	= "Мойка";
						DownloadTitleWork((Excel.Worksheet)wb.Worksheets[8+1]);		// Создание заголовка
						// Лист для Мойки
						((Excel.Worksheet)wb.Worksheets[9+1]).Name	= "Список";
						DownloadTitleReport((Excel.Worksheet)wb.Worksheets[9+1]);		// Создание заголовка
						// Расширенная гарантия
						((Excel.Worksheet)wb.Worksheets[10+1]).Name	= "Гарантия расширенно";
						DownloadTitleGuarantyExtend((Excel.Worksheet)wb.Worksheets[10+1]);		// Создание заголовка
                        // Расширенная гарантия
                        ((Excel.Worksheet)wb.Worksheets[11 + 1]).Name = "Затраты трейд-ин";
                        DownloadTitleTO((Excel.Worksheet)wb.Worksheets[11 + 1]);        // Создание заголовка	
						// Расширенная гарантия
						((Excel.Worksheet)wb.Worksheets[12 + 1]).Name = "ЛАДА Аксессуары";
						DownloadTitleWork((Excel.Worksheet)wb.Worksheets[12 + 1]);        // Создание заголовка

					}
					else
					{
						wb			= app.Workbooks.Open(file, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
					}
					workbook	= wb;
				}
				else
				{
					wb			= workbook;
					app			= wb.Application;
				}
				//Выгрузка
				DownloadReport();					// Выгрузка параметров основного отчета
			}
			catch(Exception E)
			{
				Db.SetException(E);
			}
			return wb;
		}

		#region ТО
		protected void DownloadTitleTO(Excel.Worksheet ws)
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
			Excel.Range cell;
			Excel.Range rng;
			string		txt;
			string		rowTxt;
				
			rowTxt		= row.ToString();

			try
			{
				rng	= ws.Rows;
				TrySetCellBorderText(rng, "A" + rowTxt, cardReport.CloseDate);
				TrySetCellBorderText(rng, "B" + rowTxt, cardReport.WarrantNumber);
				TrySetCellBorderText(rng, "C" + rowTxt, cardReport.WarrantDate);
				TrySetCellBorderFloat(rng, "D" + rowTxt, cardReport.CardTOD);
				TrySetCellBorderFloat(rng, "E" + rowTxt, cardReport.CardOilD);
				TrySetCellBorderFloat(rng, "F" + rowTxt, cardReport.CardOilInputD);
				TrySetCellBorderFloat(rng, "G" + rowTxt, cardReport.CardWorkNoWashD);
				TrySetCellBorderFloat(rng, "H" + rowTxt, cardReport.CardWashD);
				TrySetCellBorderFloat(rng, "I" + rowTxt, cardReport.WorkD);
				TrySetCellBorderFloat(rng, "J" + rowTxt, cardReport.CardDiscountD, EXEL_TEXT_TYPE.BOLD);
				TrySetCellBorderFloat(rng, "K" + rowTxt, cardReport.CardPayD, EXEL_TEXT_TYPE.BOLD);
				TrySetCellBorderText(rng, "L" + rowTxt, "");
				TrySetCellBorderFloat(rng, "M" + rowTxt, cardReport.CardDetailD, EXEL_TEXT_TYPE.BOLD);
				TrySetCellBorderFloat(rng, "N" + rowTxt, cardReport.CardDetailInputD);
				// Дата закрытия наряда
				//txt							= cardReport.CloseDate;
				//cell						= rng.get_Range("A" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				//cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Номер карточки
				//txt							= cardReport.WarrantNumber;
				//cell						= rng.get_Range("B" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				//cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Дата 
				//txt							= cardReport.WarrantDate;
				//cell						= rng.get_Range("C" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				//cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ТО 
				//		cell						= rng.get_Range("D" + rowTxt, Missing.Value);
				//		cell.Value2 = cardReport.CardTOD;
				//		cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Масла 			
				//cell						= rng.get_Range("E" + rowTxt, Missing.Value);
				//cell.Value2 = cardReport.CardOilD;
				//cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Масла - Себестоимость

				//	cell						= rng.get_Range("F" + rowTxt, Missing.Value);
				//	cell.Value2 = cardReport.CardOilInputD;
				//	cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Работы - без мойки
				//	cell						= rng.get_Range("G" + rowTxt, Missing.Value);
				//	cell.Value2 = cardReport.CardWorkNoWashD;
				//	cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Мойки

				//	cell						= rng.get_Range("H" + rowTxt, Missing.Value);
				//	cell.Value2 = cardReport.CardWashD;
				//	cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Работа ИТОГ

				//cell						= rng.get_Range("I" + rowTxt, Missing.Value);
				//cell.Value2 = cardReport.WorkD;
				//cell.Font.Bold				= true;
				//cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Сумма скидки

				//	cell						= rng.get_Range("J" + rowTxt, Missing.Value);
				//	cell.Value2 = cardReport.CardDiscountD;
				//	cell.Font.Bold				= true;
				//	cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// К оплате

				//cell						= rng.get_Range("K" + rowTxt, Missing.Value);
				//cell.Value2 = cardReport.CardPayD;
				//cell.Font.Bold				= true;
				//cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Оплата
				//	cell						= rng.get_Range("L" + rowTxt, Missing.Value);
				//	cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Детали 

				//	cell						= rng.get_Range("M" + rowTxt, Missing.Value);
				//	cell.Value2 = cardReport.CardDetailD;
				//	cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Детали - себестоимость

			//	cell						= rng.get_Range("N" + rowTxt, Missing.Value);
			//	cell.Value2 = cardReport.CardDetailInputD;
			//	cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Гарантия ДА/НЕТ
				if(cardReport.HaveGuaranty())
					txt							= "ДА";
				else
					txt							= "";
				cell						= rng.get_Range("O" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Модель 
				txt							= cardReport.AutoModel;
				cell						= rng.get_Range("Q" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				// VIN 
				txt							= cardReport.AutoVIN;
				cell						= rng.get_Range("R" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				// Регистрационный знак 
				txt							= cardReport.AutoSignNo;
				cell						= rng.get_Range("S" + rowTxt, Missing.Value);
				cell.Value2					= txt;
			}
			catch(Exception E)
			{
				Db.SetException(E);
				return;
			}
			return;
		}

		protected void DownloadReportTO(Excel.Worksheet ws)
		{
			int count;
			int index	= ws.Index;
			if(ExcelCardReport.current_row[index] == 0)
			{
				Excel.Range rng = ws.UsedRange;
				count = rng.Rows.Count;
			}
			else
			{
				count = ExcelCardReport.current_row[index];
				ExcelCardReport.current_row[index]++;
			}
			DownloadReportTO(ws, count + 1);
		}
		#endregion
		
		#region Гарантия
		protected void DownloadTitleGuaranty(Excel.Worksheet ws)
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
			if(ExcelCardReport.current_row[index] == 0)
			{
				Excel.Range rng = ws.UsedRange;
				count = rng.Rows.Count;
			}
			else
			{
				count = ExcelCardReport.current_row[index];
				ExcelCardReport.current_row[index]++;
			}
			DownloadReportGuaranty(ws, count + 1);
		}
		#endregion

		#region Лист ППП
		protected void DownloadTitlePPP(Excel.Worksheet ws)
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
			if(ExcelCardReport.current_row[index] == 0)
			{
				Excel.Range rng = ws.UsedRange;
				count = rng.Rows.Count;
			}
			else
			{
				count = ExcelCardReport.current_row[index];
				ExcelCardReport.current_row[index]++;
			}
			DownloadReportPPP(ws, count + 1);
		}
		#endregion
		
		#region Работы без масел, ТО и гарантии
		protected void DownloadTitleWork(Excel.Worksheet ws)
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
			if(ExcelCardReport.current_row[index] == 0)
			{
				Excel.Range rng = ws.UsedRange;
				count = rng.Rows.Count;
			}
			else
			{
				count = ExcelCardReport.current_row[index];
				ExcelCardReport.current_row[index]++;
			}
			DownloadReportWork(ws, count + 1);
		}
		#endregion

		#region Большой сводный отчет
		protected void DownloadTitleReport(Excel.Worksheet ws)
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
			if(ExcelCardReport.current_row[index] == 0)
			{
				Excel.Range rng = ws.UsedRange;
				count = rng.Rows.Count;
			}
			else
			{
				count = ExcelCardReport.current_row[index];
				ExcelCardReport.current_row[index]++;
			}
			DownloadReportReport(ws, count + 1);
		}
		#endregion

		#region Гарантия - Расширенно
		protected void DownloadTitleGuarantyExtend(Excel.Worksheet ws)
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
			DbCardReport.Guaranty guaranty = (DbCardReport.Guaranty)cardReport.GuarantyArray[position];
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
			if(ExcelCardReport.current_row[index] == 0)
			{
				Excel.Range rng = ws.UsedRange;
				count = rng.Rows.Count;
			}
			else
			{
				count = ExcelCardReport.current_row[index];
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
						this.DownloadReportPPP((Excel.Worksheet)workbook.Worksheets[4]);
					else
					{
						if(cardReport.HaveTO() == true)
						{
							if(cardReport.Is_Cashless() == false)
							{
								this.DownloadReportTO((Excel.Worksheet)workbook.Worksheets[1]);
							}
							else
							{
								this.DownloadReportTO((Excel.Worksheet)workbook.Worksheets[2]);
							}
						}
					}
					break;
				case 2:			// Тюнинг
					this.DownloadReportWork((Excel.Worksheet)workbook.Worksheets[6]); // 5 на 6 ИСПРАВЛЕНИЕ 2018
					break;
				case 3:			// Малярка
					this.DownloadReportWork((Excel.Worksheet)workbook.Worksheets[7+1]);
					break;
				case 5:			// Мойка
                    this.DownloadReportWork((Excel.Worksheet)workbook.Worksheets[8 + 1]);
					break;
				case 7:			// Музыка
                    this.DownloadReportWork((Excel.Worksheet)workbook.Worksheets[6]);
					break;
				case 11:		// Музыка сервис
                    this.DownloadReportWork((Excel.Worksheet)workbook.Worksheets[6]);
					break;
				case 12:		// Музыка предоплата
                    this.DownloadReportWork((Excel.Worksheet)workbook.Worksheets[6]);
					break;
				case 9:			// Допы gпри продаже ИСПРАВЛЕНИЕ 2018
					//this.DownloadReportWork((Worksheet)workbook.Worksheets[7]);
                    //this.DownloadReportTO((Excel.Worksheet)workbook.Worksheets[7]);
                    this.DownloadReportWork((Excel.Worksheet)workbook.Worksheets[5]); // Добавили ИСПРАВЛЕНИЕ 2018
					break;
				case 13:			// Допы сервиса
					//this.DownloadReportWork((Worksheet)workbook.Worksheets[7]);
                    this.DownloadReportTO((Excel.Worksheet)workbook.Worksheets[7]);
					break;
                case 14:			// Трейд-ин
                    //this.DownloadReportWork((Worksheet)workbook.Worksheets[7]);
                    this.DownloadReportTO((Excel.Worksheet)workbook.Worksheets[11+1]);
                    break;
				case 16:
					this.DownloadReportWork((Excel.Worksheet)workbook.Worksheets[12+1]); // Добавили ИСПРАВЛЕНИЕ 2018
					break;
				default:
					break;
			}
			// Для любого подразделения вычленяем гарантию
			if(cardReport.HaveGuaranty() == true)
                this.DownloadReportGuaranty((Excel.Worksheet)workbook.Worksheets[3]);

			// Общий список нарядов
            this.DownloadReportReport((Excel.Worksheet)workbook.Worksheets[9 + 1]);

			// Для любого подразделения вычленяем гарантию
			if(cardReport.HaveGuaranty() == true)
                this.DownloadReportGuarantyExtend((Excel.Worksheet)workbook.Worksheets[10 + 1]);
		}
	}
}
