using System;
using System.Collections;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbExcelDetails.
	/// </summary>
	public class DbExcelDetails:DbExcel
	{
		ArrayList	details;

		public DbExcelDetails(long workshop, bool liquid, bool guaranty, bool cashless, bool inner)
		{
			// Периода выгрузки
			FormSelectDateInterval dialog = new FormSelectDateInterval();
			if(dialog.ShowDialog() != DialogResult.OK) return;
			details = new ArrayList();
			DbSqlCardDetail.SelectInArray(details, dialog.StartDate, dialog.EndDate, workshop, liquid, guaranty, cashless, inner);
		}

		public DbExcelDetails()
		{
			// Периода выгрузки
			FormSelectDateInterval dialog = new FormSelectDateInterval();
			if(dialog.ShowDialog() != DialogResult.OK) return;
			details = new ArrayList();
			DbSqlCardDetail.SelectInArraySpec1(details, dialog.StartDate, dialog.EndDate);
		}

		override protected void TitleFormat(Excel.Worksheet ws)
		{
			// Наименование детали
			FormatColumn(ws, "A1", 32, 8, "Left");
			CellText(ws, "A1", "Наименование", "Center", 10, true);
			// Количество
			FormatColumn(ws, "B1", 12, 8, "Right", true);
			CellText(ws, "B1", "Количество", "Center", 10, true);
			// Цена
			FormatColumn(ws, "C1", 12, 8, "Right");
			CellText(ws, "C1", "Цена", "Center", 10, true);
			// Сумма
			FormatColumn(ws, "D1", 12, 8, "Right");
			CellText(ws, "D1", "Сумма", "Center", 10, true);
			// Фдаг гарантии
			//FormatColumn(ws, "E1", 12, 8, "Right");
			//CellText(ws, "E1", "Гарантия", "Center", 10, true);
			// Флаг масел
			//FormatColumn(ws, "F1", 12, 8, "Right");
			//CellText(ws, "F1", "Масла", "Center", 10, true);
		}

		override protected void DataToExcel(Excel.Worksheet ws, int start)
		{
			string row_txt;
			string cell_txt;
			int row = start;
			string txt;

			foreach(object o in details)
			{
				// Персонал
				DtCardDetail detail = (DtCardDetail)o;
			
				// Выставление данных в Excel
				row_txt = row.ToString();
				// Наименование
				cell_txt = "A" + row_txt;
				if(((string)detail.GetData("КАТАЛОГ_НОМЕР_КАРТОЧКА_ДЕТАЛЬ")).Length != 0)
					txt = (string)detail.GetData("НАИМЕНОВАНИЕ_КАРТОЧКА_ДЕТАЛЬ") + " (" + (string)detail.GetData("КАТАЛОГ_НОМЕР_КАРТОЧКА_ДЕТАЛЬ") + ")";
				else
					txt = (string)detail.GetData("НАИМЕНОВАНИЕ_КАРТОЧКА_ДЕТАЛЬ");
				CellText(ws, cell_txt, txt);
				// Количество
				cell_txt = "B" + row_txt;
				txt = detail.GetData("КОЛИЧЕСТВО_КАРТОЧКА_ДЕТАЛЬ").ToString();
				CellText(ws, cell_txt, txt);
				// Цена
				cell_txt = "C" + row_txt;
				txt = detail.GetData("ЦЕНА_КАРТОЧКА_ДЕТАЛЬ").ToString();
				CellText(ws, cell_txt, txt);
				// Сумма
				cell_txt = "D" + row_txt;
				txt = ((float)detail.GetData("КОЛИЧЕСТВО_КАРТОЧКА_ДЕТАЛЬ") * (float)detail.GetData("ЦЕНА_КАРТОЧКА_ДЕТАЛЬ")).ToString();
				CellText(ws, cell_txt, txt);
				// Флаг Гарантии
				//cell_txt = "E" + row_txt;
				//txt = detail.GetData("ГАРАНТИЯ_КАРТОЧКА_ДЕТАЛЬ").ToString();
				//CellText(ws, cell_txt, txt);
				// Флаг Масел
				//cell_txt = "F" + row_txt;
				//txt = detail.GetData("ЖИДКОСТЬ_КАРТОЧКА_ДЕТАЛЬ").ToString();
				//CellText(ws, cell_txt, txt);

				row++;
			}
		}
	}
}
