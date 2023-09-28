using System;
using System.Collections;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbExcelProduction.
	/// </summary>
	public class DbExcelProduction:DbExcel
	{
		public ArrayList	cards;		// Массив карточек, закрытых в указанный период
		int					year;		// Указатель на год периода
		int					month;		// Указатель на месяц периода
		DrProduction		production;	// Данные по выработке

		public DbExcelProduction()
		{
			cards = new ArrayList();

			// Запрос месяца выгрузки
			DateTime date = DateTime.Now;
			FormSelectDate dialog = new FormSelectDate();
			if(dialog.ShowDialog() != DialogResult.OK)
			{
				year = date.Year;
				month = date.Month;
			}
			else
			{
				year = dialog.SelectedDate.Year;
				month = dialog.SelectedDate.Month;
			}
			DateTime date_start = new DateTime(year, month, 1, 0, 0, 0, 0);
			DateTime date_end	= date_start.AddMonths(1);
			date_end			= date_end.AddDays(-1);
			DbSqlCard.SelectCardClosedNumberWorkshop(cards, date_start, date_end, 1);
			production			= new DrProduction(cards);

		}

		#region Постраничное форматирование страниц
		void TitleFormatSheet1(Excel.Worksheet ws)
		{
			// Данные о персонале
			FormatColumn(ws, "A1", 27, 8, "Left");
			CellText(ws, "A1", "Фамилия Имя Отчество", "Center", 10, true);
			
			// Данные по денежной выработке
			MergeCells(ws, "C1", "F1");
			CellText(ws, "C1", "Закрытые з/н", "Center", 10, true);
			// Выработка в нормачасах
			FormatColumn(ws, "C2", 10, 8, "Right");
			CellText(ws, "C2", "НВ", "Center", 10, true);
			// Выработка в нормачасах по сервис пакетам
			FormatColumn(ws, "D2", 10, 8, "Right");
			CellText(ws, "D2", "СП НВ", "Center", 10, true);
			// Сумма не раскрученных в нормачасах сервис пакетов
			FormatColumn(ws, "E2", 10, 8, "Right");
			CellText(ws, "E2", "СП сумма", "Center", 10, true);
			// Сумма не раскрученных в нормачасах сервис пакетов
			FormatColumn(ws, "F2", 10, 8, "Right");
			CellText(ws, "F2", "СП кол-во", "Center", 10, true);
		}
		void TitleFormatSheet2(Excel.Worksheet ws)
		{
			// Данные о персонале
			FormatColumn(ws, "A1", 27, 8, "Left");
			CellText(ws, "A1", "Анализ выработки", "Center", 10, true);
			
			// Выработка в нормачасах
			FormatColumn(ws, "B2", 10, 8, "Right");
			CellText(ws, "B2", "Сумма", "Center", 10, true);
			// Данные по денежной выработке
			MergeCells(ws, "C1", "F1");
			CellText(ws, "C1", "Клиентские З/Н наличные", "Center", 10, true);
			// Выработка в нормачасах
			FormatColumn(ws, "C2", 10, 8, "Right");
			CellText(ws, "C2", "НВ", "Center", 10, true);
			// Выработка в нормачасах по сервис пакетам
			FormatColumn(ws, "D2", 10, 8, "Right");
			CellText(ws, "D2", "СП НВ", "Center", 10, true);
			// Сумма не раскрученных в нормачасах сервис пакетов
			FormatColumn(ws, "E2", 10, 8, "Right");
			CellText(ws, "E2", "СП сумма", "Center", 10, true);
			// Сумма не раскрученных в нормачасах сервис пакетов
			FormatColumn(ws, "F2", 10, 8, "Right");
			CellText(ws, "F2", "СП кол-во", "Center", 10, true);

			// Данные по гарантии
			MergeCells(ws, "G1", "J1");
			CellText(ws, "G1", "Клиентские З/Н безналичные ", "Center", 10, true);
			// Выработка в нормачасах
			FormatColumn(ws, "G2", 10, 8, "Right");
			CellText(ws, "G2", "НВ", "Center", 10, true);
			// Выработка в нормачасах по сервис пакетам
			FormatColumn(ws, "H2", 10, 8, "Right");
			CellText(ws, "H2", "СП НВ", "Center", 10, true);
			// Сумма не раскрученных в нормачасах сервис пакетов
			FormatColumn(ws, "I2", 10, 8, "Right");
			CellText(ws, "I2", "СП сумма", "Center", 10, true);
			// Сумма не раскрученных в нормачасах сервис пакетов
			FormatColumn(ws, "J2", 10, 8, "Right");
			CellText(ws, "J2", "СП кол-во", "Center", 10, true);

			// Данные по внутренним заказ-нарядам
			MergeCells(ws, "K1", "N1");
			CellText(ws, "K1", "Гарантийные З/Н", "Center", 10, true);
			// Выработка в нормачасах
			FormatColumn(ws, "K2", 10, 8, "Right");
			CellText(ws, "K2", "НВ", "Center", 10, true);
			// Выработка в нормачасах по сервис пакетам
			FormatColumn(ws, "L2", 10, 8, "Right");
			CellText(ws, "L2", "СП НВ", "Center", 10, true);
			// Сумма не раскрученных в нормачасах сервис пакетов
			FormatColumn(ws, "M2", 10, 8, "Right");
			CellText(ws, "M2", "СП сумма", "Center", 10, true);
			// Сумма не раскрученных в нормачасах сервис пакетов
			FormatColumn(ws, "N2", 10, 8, "Right");
			CellText(ws, "N2", "СП кол-во", "Center", 10, true);

			// Данные по внутренним заказ-нарядам
			MergeCells(ws, "O1", "R1");
			CellText(ws, "O1", "Внутренние З/Н", "Center", 10, true);
			// Выработка в нормачасах
			FormatColumn(ws, "O2", 10, 8, "Right");
			CellText(ws, "O2", "НВ", "Center", 10, true);
			// Выработка в нормачасах по сервис пакетам
			FormatColumn(ws, "P2", 10, 8, "Right");
			CellText(ws, "P2", "СП НВ", "Center", 10, true);
			// Сумма не раскрученных в нормачасах сервис пакетов
			FormatColumn(ws, "Q2", 10, 8, "Right");
			CellText(ws, "Q2", "СП сумма", "Center", 10, true);
			// Сумма не раскрученных в нормачасах сервис пакетов
			FormatColumn(ws, "R2", 10, 8, "Right");
			CellText(ws, "R2", "СП кол-во", "Center", 10, true);
		}
		void TitleFormatSheet3(Excel.Worksheet ws)
		{
			// Данные о заказ-наряде
			FormatColumn(ws, "A1", 16, 8, "Left");
			CellText(ws, "A1", "Заказ-няряд", "Center", 10, true);
			FormatColumn(ws, "B1", 10, 8, "Left");
			CellText(ws, "B1", "закрытие", "Center", 10, true);
			FormatColumn(ws, "C1", 4, 8, "Center");
			CellVertical(ws, "C1");
			CellText(ws, "C1", "Сервис-консультант", "Center", 10, true);
			FormatColumn(ws, "D1", 3, 8, "Center");
			CellVertical(ws, "D1");
			CellText(ws, "D1", "Внутренний", "Center", 10, true);
			FormatColumn(ws, "E1", 3, 8, "Center");
			CellVertical(ws, "E1");
			CellText(ws, "E1", "Безналичный", "Center", 10, true);
			FormatColumn(ws, "F1", 3, 8, "Center");
			CellVertical(ws, "F1");
			CellText(ws, "F1", "Гарантия", "Center", 10, true);
			FormatColumn(ws, "G1", 10, 8, "Left");
			CellText(ws, "G1", "Код", "Center", 10, true);
			FormatColumn(ws, "H1", 25, 8, "Left");
			CellText(ws, "H1", "Наименование", "Center", 10, true);
			FormatColumn(ws, "I1", 3, 8, "Center");
			CellVertical(ws, "I1");
			CellText(ws, "I1", "Количество", "Center", 10, true);
			FormatColumn(ws, "J1", 6, 8, "Center");
			CellText(ws, "J1", "НВ", "Center", 10, true);
			FormatColumn(ws, "K1", 8, 8, "Center");
			CellText(ws, "K1", "Цена", "Center", 10, true);
			FormatColumn(ws, "L1", 6, 8, "Center");
			CellText(ws, "L1", "СП", "Center", 10, true);
			FormatColumn(ws, "M1", 8, 8, "Center");
			CellText(ws, "M1", "Сумма НВ", "Center", 10, true);
		}
		#endregion

		#region Постраничная выгрузка данных
		protected void DataToExcelSheet1(Excel.Worksheet ws, int start)
		{
			string row_last = "1";
			string row_txt;
			string cell_txt;
			int row = start;
			int row_summ = 2;
			string txt;

			foreach(object o in production.service_consultants)
			{
				// Персонал
				DrProduction.ServiceConsultant consultant = (DrProduction.ServiceConsultant)o;
			
				// Выставление данных в Excel
				row_txt = row.ToString();
				// Данные о персонале
				cell_txt = "A" + row_txt;
				txt = consultant.name;
				CellText(ws, cell_txt, txt);
				
				// Данные по денежной выработке
				// Количество отработанных нормочасов
				cell_txt = "C" + row_txt;
				txt = consultant.production.hours.ToString();
				CellText(ws, cell_txt, txt);
				// Количество отработанных нормочасов по сервиспакетам
				cell_txt = "D" + row_txt;
				txt = consultant.production.hours_sp.ToString();
				CellText(ws, cell_txt, txt);
				// Сумма не раскрученных по нормаам времени сервис пакетов
				cell_txt = "E" + row_txt;
				txt = consultant.production.cash_sp_nohours.ToString();
				CellText(ws, cell_txt, txt);
				// Количество не раскрученных по нормаам времени сервис пакетов
				cell_txt = "F" + row_txt;
				txt = consultant.production.count_sp_nohours.ToString();
				CellText(ws, cell_txt, txt);

				// Переход к следующей строке
				row_last = row.ToString();
				row++;
			}
		}
		protected void DataToExcelSheet2(Excel.Worksheet ws, int start)
		{
			string row_last = "1";
			string row_txt;
			string cell_txt;
			int row = start;
			int row_summ = 2;
			string txt;

			
			// Выставление данных в Excel
			row_txt = row.ToString();
			// Данные о персонале
			cell_txt = "A" + row_txt;
			txt = "Выработка";
			CellText(ws, cell_txt, txt);

			// Данные по суммарной выработке
			cell_txt = "B" + row_txt;
			txt = "=C" + row_txt + "+D" + row_txt + "+G" + row_txt + "+H" + row_txt + "+K" + row_txt + "+L" + row_txt + "+O" + row_txt + "+P" + row_txt;
			CellText(ws, cell_txt, txt);
				
			// Данные по денежной выработке - наличка
			// Количество отработанных нормочасов
			cell_txt = "C" + row_txt;
			txt = production.card_cash.hours.ToString();
			CellText(ws, cell_txt, txt);
			// Количество отработанных нормочасов по сервиспакетам
			cell_txt = "D" + row_txt;
			txt = production.card_cash.hours_sp.ToString();
			CellText(ws, cell_txt, txt);
			// Сумма не раскрученных по нормаам времени сервис пакетов
			cell_txt = "E" + row_txt;
			txt = production.card_cash.cash_sp_nohours.ToString();
			CellText(ws, cell_txt, txt);
			// Количество не раскрученных по нормаам времени сервис пакетов
			cell_txt = "F" + row_txt;
			txt = production.card_cash.count_sp_nohours.ToString();
			CellText(ws, cell_txt, txt);

			// Данные по денежной выработке - безнал
			// Количество отработанных нормочасов
			cell_txt = "G" + row_txt;
			txt = production.card_cashless.hours.ToString();
			CellText(ws, cell_txt, txt);
			// Количество отработанных нормочасов по сервиспакетам
			cell_txt = "H" + row_txt;
			txt = production.card_cashless.hours_sp.ToString();
			CellText(ws, cell_txt, txt);
			// Сумма не раскрученных по нормаам времени сервис пакетов
			cell_txt = "I" + row_txt;
			txt = production.card_cashless.cash_sp_nohours.ToString();
			CellText(ws, cell_txt, txt);
			// Количество не раскрученных по нормаам времени сервис пакетов
			cell_txt = "J" + row_txt;
			txt = production.card_cashless.count_sp_nohours.ToString();
			CellText(ws, cell_txt, txt);

			// Данные по гарантии
			// Количество отработанных нормочасов
			cell_txt = "K" + row_txt;
			txt = production.card_guaranty.hours.ToString();
			CellText(ws, cell_txt, txt);
			// Количество отработанных нормочасов по сервиспакетам
			cell_txt = "L" + row_txt;
			txt = production.card_guaranty.hours_sp.ToString();
			CellText(ws, cell_txt, txt);
			// Сумма не раскрученных по нормаам времени сервис пакетов
			cell_txt = "M" + row_txt;
			txt = production.card_guaranty.cash_sp_nohours.ToString();
			CellText(ws, cell_txt, txt);
			// Количество не раскрученных по нормаам времени сервис пакетов
			cell_txt = "N" + row_txt;
			txt = production.card_guaranty.count_sp_nohours.ToString();
			CellText(ws, cell_txt, txt);

			// Данные по внутренним з/н
			// Количество отработанных нормочасов
			cell_txt = "O" + row_txt;
			txt = production.card_inner.hours.ToString();
			CellText(ws, cell_txt, txt);
			// Количество отработанных нормочасов по сервиспакетам
			cell_txt = "P" + row_txt;
			txt = production.card_inner.hours_sp.ToString();
			CellText(ws, cell_txt, txt);
			// Сумма не раскрученных по нормаам времени сервис пакетов
			cell_txt = "Q" + row_txt;
			txt = production.card_inner.cash_sp_nohours.ToString();
			CellText(ws, cell_txt, txt);
			// Количество не раскрученных по нормаам времени сервис пакетов
			cell_txt = "R" + row_txt;
			txt = production.card_inner.count_sp_nohours.ToString();
			CellText(ws, cell_txt, txt);

			// Переход к следующей строке
			row_last = row.ToString();
			row++;
		}
		protected void DataToExcelSheet3(Excel.Worksheet ws, int start)
		{
			string row_last = "1";
			string row_txt;
			string cell_txt;
			int row = start;
			int row_summ = 2;
			string txt;

			foreach(DrProduction.CardWorkEx work_ex in production.works_ex)
			{
				// Получение данных о заказ/наряде
				// Выставление данных в Excel
				row_txt = row.ToString();

				// Данные о заказ-наряде
				cell_txt = "A" + row_txt;
				CellText(ws, cell_txt, work_ex.txt_card);
				// Данные о закрытии заказ-наряда
				cell_txt = "B" + row_txt;
				CellText(ws, cell_txt, work_ex.txt_card_close_date);
				// Данные о сервис-консультанте
				cell_txt = "C" + row_txt;
				CellText(ws, cell_txt, work_ex.txt_service_consultant);
				// Данные о внутреннем заказ-наряде
				cell_txt = "D" + row_txt;
				CellText(ws, cell_txt, work_ex.txt_inner);
				// Данные о безналичном заказ-наряде
				cell_txt = "E" + row_txt;
				CellText(ws, cell_txt, work_ex.txt_cashless);
				// Данные о гарантийной работе
				cell_txt = "F" + row_txt;
				CellText(ws, cell_txt, work_ex.txt_work_guaranty);
				// Номер позиции работы
				cell_txt = "G" + row_txt;
				CellText(ws, cell_txt, work_ex.txt_work_code);
				// Наименование работы
				cell_txt = "H" + row_txt;
				CellText(ws, cell_txt, work_ex.txt_work_name);
				// Количество выполненных работ
				cell_txt = "I" + row_txt;
				CellText(ws, cell_txt, work_ex.txt_work_count);
				// НВ работы
				cell_txt = "J" + row_txt;
				CellText(ws, cell_txt, work_ex.txt_work_nv);
				// Стоимость нормачаса работы
				cell_txt = "K" + row_txt;
				CellText(ws, cell_txt, work_ex.txt_work_price);
				// Раскручивание сервис пакета
				cell_txt = "L" + row_txt;
				CellText(ws, cell_txt, work_ex.txt_work_sp);
				// Итоговые НВ по данной работе
				cell_txt = "M" + row_txt;
				CellText(ws, cell_txt, work_ex.txt_work_nvsum);
				
				
				// Переход к следующей строке
				row_last = row.ToString();
				row++;
			}	
		}
		#endregion

		override protected void DataToExcelMult(Excel.Worksheet ws, int sheet, int start)
		{
			if(sheet == 1)
			{
				DataToExcelSheet1(ws, start);
				return;
			}
			if(sheet == 2)
			{
				DataToExcelSheet2(ws, start);
				return;
			}
			if(sheet == 3)
			{
				DataToExcelSheet3(ws, start);
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
			if(sheet == 2)
			{
				TitleFormatSheet2(ws);
				return;
			}
			if(sheet == 3)
			{
				TitleFormatSheet3(ws);
				return;
			}
		}
	}
}
