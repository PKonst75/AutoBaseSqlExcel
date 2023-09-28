using System;
using System.Collections;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbExcelReportTo.
	/// </summary>
	public class DbExcelReportTo:DbExcel
	{
		readonly ArrayList array = new ArrayList();

		public DbExcelReportTo()
		{
			// Запрос даты начала отсчета и конца отсчета
			FormSelectDate dlg = new FormSelectDate();
			dlg.ShowDialog();
			if(dlg.DialogResult != DialogResult.OK) return;
			DateTime date_start = dlg.SelectedDate;

			dlg.ShowDialog();
			if(dlg.DialogResult != DialogResult.OK) return;
			DateTime date_end = dlg.SelectedDate;

			// Новый способ получения списка карточке закрытых на указанную дату
			// Считаем, что работаем в одном году
			// Подразделение - исключительно СЕРВИС
			ArrayList	number_array	= new ArrayList();
			DbSqlCard.SelectCardClosedNumberWorkshop(number_array, date_start, date_end, 1);	
			FormInfoTable info = new FormInfoTable("Начало отсчета");
			info.Show();
			foreach(DtCard element in number_array)
			{		
				DtCard card = DbSqlCard.Find((long)element.GetData("НОМЕР_КАРТОЧКА"), (int)element.GetData("ГОД_КАРТОЧКА"));
				DtAuto  auto = DbSqlAuto.Find((long)card.CodeAuto);

				bool lada = false;
				if (auto != null) lada = auto.IsBrandLada();

				// Проверяем, содержит ли карточка ТО
				if ((card.IsToWork() == true) && (lada == true))
				{
					array.Add(card);
				}
				else
				{
					// Анализ внутренних карточек
				}
				info.SetText(card.GetData("НОМЕР_НАРЯД_КАРТОЧКА").ToString());
			}
			info.SetText("Конец отсчета");
			info.Close();
		}

		protected void TitleFormatSheet1(Excel.Worksheet ws)
		{
			// Дата закрытия заказ/наряда
			FormatColumn(ws, "A1", 12, 8, "Left");
			CellText(ws, "A1", "Закрытие", "Center", 10, true);
			// Номер заказ/наряда
			FormatColumn(ws, "B1", 12, 8, "Right", true);
			CellText(ws, "B1", "Номер ЗН", "Center", 10, true);
			// Дата заказ/наряда
			FormatColumn(ws, "C1", 12, 8, "Right");
			CellText(ws, "C1", "Дата ЗН", "Center", 10, true);
			// Данные о мастере-консультанте
			FormatColumn(ws, "D1", 27, 8, "Right");
			CellText(ws, "D1", "VIN", "Center", 10, true);
			// Данные о пробеге
			FormatColumn(ws, "E1", 12, 8, "Right");
			CellText(ws, "E1", "Пробег", "Center", 10, true);
			// Данные о номере ТО
			FormatColumn(ws, "F1", 40, 8, "Right");
			CellText(ws, "F1", "ТО", "Center", 10, true);

		}

		protected void DataToExcelSheet1(Excel.Worksheet ws, int start)
		{
			//string row_last = "1";
			string row_txt;
			string cell_txt;
			int row = start;
			//int row_summ = 2;
			string txt;

			DtAuto auto;// = null;

			foreach(object o in array)
			{
				// Картички
				DtCard card = (DtCard)o;

				// Загружаем дополнительные данные
				auto = DbSqlAuto.Find((long)card.CodeAuto);
				
				
				// Выставление данных в Excel
				row_txt = row.ToString();
				// Дата закрытия
				cell_txt = "A" + row_txt;
				txt = ((DateTime)card.GetData("ДАТА_НАРЯД_ЗАКРЫТ_КАРТОЧКА")).ToShortDateString();
				CellText(ws, cell_txt, txt);
				// Номер ЗН
				cell_txt = "B" + row_txt;
				txt = card.GetData("НОМЕР_НАРЯД_КАРТОЧКА").ToString();
				CellText(ws, cell_txt, txt);
				// Дата ЗН
				cell_txt = "C" + row_txt;
				txt = ((DateTime)card.GetData("ДАТА")).ToShortDateString();
				CellText(ws, cell_txt, txt);
				// Мастер консультант
				if (auto != null)
				{
					cell_txt = "D" + row_txt;
					txt = (string)auto.GetData("VIN");
					CellText(ws, cell_txt, txt);
				}
				// Пробег
				cell_txt = "E" + row_txt;
				txt = card.GetData("ПРОБЕГ_КАРТОЧКА").ToString();
				CellText(ws, cell_txt, txt);
				// Наименование ТО
				cell_txt = "F" + row_txt;
				txt = card.NameToWork();
				CellText(ws, cell_txt, txt);

				row++;	// переход на следующую стоку
			}
		}

		override protected void DataToExcelMult(Excel.Worksheet ws, int sheet, int start)
		{
			if(sheet == 1)
			{
				DataToExcelSheet1(ws, start);
				return;
			}
		}

		override protected void TitleFormatMult(Excel.Worksheet ws, int sheet)
		{
			if(sheet == 1)
			{
				TitleFormatSheet1(ws);
				return;
			}
		}
	}
}
