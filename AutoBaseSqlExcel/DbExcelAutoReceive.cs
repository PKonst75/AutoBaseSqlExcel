using System;
using Excel = Microsoft.Office.Interop.Excel;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbExcelAutoReceive.
	/// </summary>
	public class DbExcelAutoReceive:DbExcel
	{
		ArrayList		autos;
		string			doc_date = "";
		string			doc_sender = "";
		
		public DbExcelAutoReceive(long code_document)
		{
			autos = new ArrayList();
			DbSqlAuto.SelectInArrayReceive(autos, code_document);

			DtAutoReceive	auto_receive;
			auto_receive = DbSqlAutoReceive.Find(code_document);
			if(auto_receive != null)
			{
				DateTime date	= (DateTime)auto_receive.GetData("ДАТА_ДОКУМЕНТ");
				doc_date		= date.ToShortDateString();
				doc_sender		= (string)auto_receive.GetData("ПРИМЕЧАНИЕ_ДОКУМЕНТ");
			}
		}

		override protected void TitleFormat(Excel.Worksheet ws)
		{
			// Номер строки
			FormatColumn(ws, "A1", 3, 8, "Right");
			FormatColumnVAlign(ws, "A1", "Top");
			CellText(ws, "A5", "№", "Center", 10, true);
			// Наименование модели
			FormatColumn(ws, "B1", 18, 8, "Left");
			FormatColumnVAlign(ws, "B1", "Top");
			CellText(ws, "B5", "Модель", "Center", 10, true);
			// Вариант
			FormatColumn(ws, "C1", 12, 8, "Left");
			FormatColumnVAlign(ws, "C1", "Top");
			CellText(ws, "C5", "Исполнение", "Center", 10, true);
			// VIN
			FormatColumn(ws, "D1", 20, 8, "Left");
			FormatColumnVAlign(ws, "D1", "Top");
			CellText(ws, "D5", "VIN", "Center", 10, true);
			// Кузов
			FormatColumn(ws, "E1", 20, 8, "Left");
			FormatColumnVAlign(ws, "E1", "Top");
			FormatColumnNumberFormat(ws, "E1", "@");
			CellText(ws, "E5", "Кузов", "Center", 10, true);
			// Примечание
			FormatColumn(ws, "F1", 35, 8, "Left");
			FormatColumnVAlign(ws, "F1", "Top");
			FormatColumnWrapText(ws, "F1", true);
			CellText(ws, "F5", "Примечание", "Center", 10, true);

			// Заголовок
			CellText(ws, "A1", "Приход автомобилей", "Left", 12, true);
			CellText(ws, "B2", doc_date, "Left", 12, true);
			CellText(ws, "B3", doc_sender, "Left", 12, true);

			PrintStandart1(ws);
		}

		override protected void DataToExcel(Excel.Worksheet ws, int start)
		{
			string row_txt;
			string cell_txt;
			int row = 6;
			string txt;
			int count = 1;

			foreach(object o in autos)
			{
				// Автомобили
				DtAuto detail = (DtAuto)o;
			
				// Выставление данных в Excel
				row_txt = row.ToString();
				// Наименование
				cell_txt = "A" + row_txt;
				txt = (string)count.ToString();
				CellText(ws, cell_txt, txt);
				// Наименование
				cell_txt = "B" + row_txt;
				txt = (string)detail.GetData("МОДЕЛЬ");
				CellText(ws, cell_txt, txt);
				// Исполнение
				cell_txt = "C" + row_txt;
				txt = (string)detail.GetData("АВТОМОБИЛЬ_ИСПОЛНЕНИЕ");
				CellText(ws, cell_txt, txt);
				// VIN
				cell_txt = "D" + row_txt;
				txt = (string)detail.GetData("VIN");
				CellText(ws, cell_txt, txt);
				// Номер кузов
				cell_txt = "E" + row_txt;
				txt = (string)detail.GetData("НОМЕР_КУЗОВ");
				CellText(ws, cell_txt, txt);
				// Примечание
				cell_txt = "F" + row_txt;
				txt = (string)detail.GetData("ПОЛУЧЕНИЕ_ПРИМЕЧАНИЕ");
				CellText(ws, cell_txt, txt);
				
				count++;
				row++;
			}
		}
	}
}
