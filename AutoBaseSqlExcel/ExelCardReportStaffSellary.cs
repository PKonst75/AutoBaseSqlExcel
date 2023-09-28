using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Windows.Forms;

namespace AutoBaseSql
{
    class ExelCardReportStaffSellary:DbExcel
    {
        private Excel.Workbook workbook;
        private DbCardReportStaffSellary cardReport;
        private static int[] current_row = new int[13 + 1];    // Последняя выгруженная строка отчета полистно
        private DtCard _card; // Карточка
        private DtTxtCard _cardTxt; // Текстовое отображение карточки

		public static void DownloadList(ArrayList array)
		{
			Excel.Workbook wb = null;
			//string				fileName;

			string fileName = SelectFileDialog("Выберете файл отчета для продолжения");
			if (fileName == DbExcel.CANCEL_SELECT) return; // Отказ от выбора файла

			foreach (object o in array)
			{
				DbCard card = (DbCard)o;
				if (card != null)
				{
					ExelCardReportStaffSellary excelCardReport = new ExelCardReportStaffSellary(card, wb);
					wb = excelCardReport.StartDownLoad(fileName);
				}
			}
			ShowExcel(wb);
		}

		public ExelCardReportStaffSellary(DbCard cardSrc, Excel.Workbook workbookSrc)
		{
			cardReport = new DbCardReportStaffSellary(cardSrc);
			workbook = workbookSrc;
		}

		public Excel.Workbook StartDownLoad(string file)
		{
			Excel.Application app;
			Excel.Workbook wb = null;

			try
			{
				if (workbook == null)
				{
					app = new Excel.Application();              // Новый экземпляр приложения Excel
																// Если задан файл отчета, открываем существующую книгу
					if (file.Length == 0)
					{
						wb = app.Workbooks.Add(Missing.Value);      // Новая книга Excel
																	// Форматируем новую книгу
						
						wb.Worksheets.Add(Missing.Value, wb.Worksheets[1], 1, Missing.Value);
						// Лист для ТО
						((Excel.Worksheet)wb.Worksheets[1]).Name = "ТО";
						DownloadTitleTO((Excel.Worksheet)wb.Worksheets[1]);         // Создание заголовка
																					// Лист для ТО
					}
					else
					{
						wb = app.Workbooks.Open(file, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
					}
					workbook = wb;
				}
				else
				{
					wb = workbook;
					app = wb.Application;
				}
				//Выгрузка
				DownloadReport();                   // Выгрузка параметров основного отчета
			}
			catch (Exception E)
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
				rng = ws.Rows;
				TryColumnFormatTitleBold(rng, "Закрытие", "A1", 9, 8, EXEL_HORIZONT_ALIGN.LEFT);
				TryColumnFormatTitleBold(rng, "Номер", "B1", 8, 8, EXEL_HORIZONT_ALIGN.LEFT);
				TryColumnFormatTitleBold(rng, "Дата", "C1", 9, 8, EXEL_HORIZONT_ALIGN.LEFT);

				TryColumnFormatTitleBold(rng, "Часы в ЗП", "E1", 9, 8, EXEL_HORIZONT_ALIGN.LEFT, "# ##0,00");
				TryColumnFormatTitleBold(rng, "ЗП механики", "F1", 9, 8, EXEL_HORIZONT_ALIGN.LEFT, "# ##0,00");
				TryColumnFormatTitleBold(rng, "ЗП консультант", "G1", 9, 8, EXEL_HORIZONT_ALIGN.LEFT, "# ##0,00");
				TryColumnFormatTitleBold(rng, "Оплачено", "H1", 9, 8, EXEL_HORIZONT_ALIGN.LEFT, "# ##0,00");

				TryColumnFormatTitleBold(rng, "Подразделение", "Q1", 8, 8, EXEL_HORIZONT_ALIGN.LEFT);
				TryColumnFormatTitleBold(rng, "Консультант", "R1", 8, 8, EXEL_HORIZONT_ALIGN.LEFT);

				/*
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
				*/

				// Параметры печати
				ws.PageSetup.Orientation = Excel.XlPageOrientation.xlLandscape;
				ws.PageSetup.LeftMargin = ws.Application.InchesToPoints(0.393700787401575);
				ws.PageSetup.RightMargin = ws.Application.InchesToPoints(0.393700787401575);
				ws.PageSetup.TopMargin = ws.Application.InchesToPoints(0.590551181102362);
				ws.PageSetup.BottomMargin = ws.Application.InchesToPoints(0.590551181102362);
				ws.PageSetup.HeaderMargin = ws.Application.InchesToPoints(0.511811023622047);
				ws.PageSetup.FooterMargin = ws.Application.InchesToPoints(0.511811023622047);
				ws.PageSetup.PrintArea = "$A:$M";
			}
			catch (Exception E)
			{
				Db.SetException(E);
			}
		}
		protected void DownloadReportTO(Excel.Worksheet ws, int row)
		{
			// Выгружаем данные на определенный лист в Excel
			Excel.Range cell;
			Excel.Range rng;
			string txt;
			string rowTxt;

			rowTxt = row.ToString();

			try
			{
				rng = ws.Rows;
				TrySetCellBorderText(rng, "A" + rowTxt, cardReport.CloseDate);
				TrySetCellBorderText(rng, "B" + rowTxt, cardReport.WarrantNumber);
				TrySetCellBorderText(rng, "C" + rowTxt, cardReport.WarrantDate);

				TrySetCellBorderFloat(rng, "E" + rowTxt, cardReport.SellaryHours);
				TrySetCellBorderFloat(rng, "F" + rowTxt, cardReport.SalaryServiceMechanic);
				TrySetCellBorderFloat(rng, "G" + rowTxt, cardReport.SalaryServiceManager);
				TrySetCellBorderFloat(rng, "H" + rowTxt, cardReport.PaidAmount);

				TrySetCellBorderText(rng, "Q" + rowTxt, cardReport.Department);
				TrySetCellBorderText(rng, "R" + rowTxt, cardReport.ServiceManager);
				/*
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
				
				// Гарантия ДА/НЕТ
				if (cardReport.HaveGuaranty())
					txt = "ДА";
				else
					txt = "";
				cell = rng.get_Range("O" + rowTxt, Missing.Value);
				cell.Value2 = txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// Модель 
				txt = cardReport.AutoModel;
				cell = rng.get_Range("Q" + rowTxt, Missing.Value);
				cell.Value2 = txt;
				// VIN 
				txt = cardReport.AutoVIN;
				cell = rng.get_Range("R" + rowTxt, Missing.Value);
				cell.Value2 = txt;
				// Регистрационный знак 
				txt = cardReport.AutoSignNo;
				cell = rng.get_Range("S" + rowTxt, Missing.Value);
				cell.Value2 = txt;
				*/
			}
			catch (Exception E)
			{
				Db.SetException(E);
				return;
			}
			return;
		}

		protected void DownloadReportTO(Excel.Worksheet ws)
		{
			int count;
			int index = ws.Index;
			if (ExelCardReportStaffSellary.current_row[index] == 0)
			{
				Excel.Range rng = ws.UsedRange;
				count = rng.Rows.Count;
			}
			else
			{
				count = ExelCardReportStaffSellary.current_row[index];
				ExelCardReportStaffSellary.current_row[index]++;
			}
			DownloadReportTO(ws, count + 1);
		}
		#endregion

		protected void DownloadReport()
		{
			// Определяем необходимые типы выгрузки для карточки
			this.DownloadReportTO((Excel.Worksheet)workbook.Worksheets[1]);
							
		}
	}
}
