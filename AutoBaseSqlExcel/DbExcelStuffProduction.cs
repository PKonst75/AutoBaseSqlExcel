using System;
using System.Collections;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbExcelStuffProduction.
	/// </summary>
	public class DbExcelStuffProduction:DbExcel
	{
		public ArrayList	staffs;
		readonly int			year;
		int			month;
		DateTime	start_date;
		DateTime	end_date;
		int			variant;

		public DbExcelStuffProduction(long codeJob = 1)
		{
			staffs = new ArrayList();
			DbSqlStaff.SelectInArray(staffs, codeJob);
			//DbSqlStaff.SelectInArray(staffs, 1);
			//DbSqlStaff.SelectInArray(staffs);

			// Запрос типа выгрузки......
			if (MessageBox.Show("Запросить полный месяц", "Запрос", MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
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
				variant = 1;
			}
			else
			{
				// Запрос интервала выгрузки
				start_date = DateTime.Now;
				end_date = DateTime.Now;
				FormSelectDate dialog = new FormSelectDate();
				if(dialog.ShowDialog() == DialogResult.OK)
					start_date = dialog.SelectedDate;
				if(dialog.ShowDialog() == DialogResult.OK)
					end_date = dialog.SelectedDate;
				variant = 2;
			}
		}

		#region Постраничное форматирование страниц
		void TitleFormatSheet1(Excel.Worksheet ws)
		{
			// Форматирование
			Excel.Range rng = ws.Rows;
			Excel.Range cell = rng.get_Range("C1", Missing.Value);
			cell.EntireColumn.NumberFormatLocal = "# ##0,00";

			// Данные о персонале
			FormatColumn(ws, "A1", 27, 8, "Left");
			CellText(ws, "A1", "Фамилия Имя Отчество", "Center", 10, true);
			
			// Выработка в нормачасах
			FormatColumn(ws, "B2", 10, 8, "Right");
		//	FormatColumnNumberFormat(ws, "B2", "0.00");
			CellText(ws, "B2", "Сумма", "Center", 10, true);
			
			// Данные по денежной выработке
			MergeCells(ws, "C1", "F1");
		//	FormatColumnNumberFormat(ws, "C1", "0.00");
			CellText(ws, "C1", "Клиентские З/Н", "Center", 10, true);
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
			CellText(ws, "G1", "Гарантийные З/Н", "Center", 10, true);
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
			CellText(ws, "K1", "Внутренние З/Н", "Center", 10, true);
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
		}
		void TitleFormatSheet2(Excel.Worksheet ws, int pos)
		{
						// Данные о персонале
			DtStaff	staff	= (DtStaff)this.staffs[pos];
			string txt = staff.GetData("ФАМИЛИЯ_ПЕРСОНАЛ") + " " + staff.GetData("ИМЯ_ПЕРСОНАЛ") + " " + staff.GetData("ОТЧЕСТВО_ПЕРСОНАЛ");
			CellText(ws, "A1", txt, "Left", 10, true);
			
			// Подробные данные по выработке
			// Номер заказ/наряда (карточки) и год карточки, дата закрытия
			FormatColumn(ws, "A2", 22, 8, "Left");
			CellText(ws, "A2", "Карточка", "Center", 10, true);
			// Код выполненной работы
			FormatColumn(ws, "B2", 10, 8, "Left");
			CellText(ws, "B2", "Код работы", "Center", 10, true);
			// Наименование выполненной работы
			FormatColumn(ws, "C2", 30, 8, "Left");
			CellText(ws, "C2", "Наименование работы", "Center", 10, true);
			// Количество выполненных работ
			FormatColumn(ws, "D2", 7, 8, "Center");
			CellText(ws, "D2", "Кол-во", "Center", 10, true);
			// Норма времени по выполненной работе
			FormatColumn(ws, "E2", 7, 8, "Center");
			CellText(ws, "E2", "НВ", "Center", 10, true);
			// Стоимость нормачаса
			FormatColumn(ws, "F2", 7, 8, "Center");
			CellText(ws, "F2", "НЧ", "Center", 10, true);
			// Количество исполнителей
			FormatColumn(ws, "G2", 7, 8, "Center");
			CellText(ws, "G2", "Исп.", "Center", 10, true);
			// Раскрутка по времени сервис пакета
			FormatColumn(ws, "H2", 7, 8, "Center");
			CellText(ws, "H2", "СП", "Center", 10, true);
			// Гарантия
			FormatColumn(ws, "I2", 7, 8, "Center");
			CellText(ws, "I2", "Гар.", "Center", 10, true);
			// Тип наряда
			FormatColumn(ws, "J2", 8, 8, "Center");
			CellText(ws, "J2", "Тип", "Center", 10, true);
			// Приведенное общее наработанное время
			FormatColumn(ws, "K2", 10, 8, "Center");
			CellText(ws, "K2", "Сумма НВ", "Center", 10, true);
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

			foreach(object o in staffs)
			{
				// Персонал
				DtStaff staff = (DtStaff)o;
				// Получение данных о выработке
				// DrStuffProduction production = new DrStuffProduction((long)staff.GetData("КОД_ПЕРСОНАЛ"), year, month);
				DrStuffProduction production;
				if (variant == 1)
					production = new DrStuffProduction((long)staff.GetData("КОД_ПЕРСОНАЛ"), year, month);
				else
					production = new DrStuffProduction((long)staff.GetData("КОД_ПЕРСОНАЛ"), start_date, end_date);

				// Выставление данных в Excel
				row_txt = row.ToString();
				// Данные о персонале
				cell_txt = "A" + row_txt;
				txt = staff.GetData("ФАМИЛИЯ_ПЕРСОНАЛ") + " " + staff.GetData("ИМЯ_ПЕРСОНАЛ") + " " + staff.GetData("ОТЧЕСТВО_ПЕРСОНАЛ");
				CellText(ws, cell_txt, txt);

				// Данные по суммарной выработке
				cell_txt = "B" + row_txt;
				txt = "=C" + row_txt + "+D" + row_txt + "+G" + row_txt + "+H" + row_txt + "+K" + row_txt + "+L" + row_txt;
				CellText(ws, cell_txt, txt);
				
				// Данные по денежной выработке
				// Количество отработанных нормочасов
				cell_txt = "C" + row_txt;
				txt = production.card_cash.hours.ToString();
				//if (txt.Length > 8) txt = txt.Substring(0, 8);
				txt = txt.Replace(",", ".");
				//txt = ToString(production.card_cash.hours, "0000.0000");
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

				// Данные по гарантии
				// Количество отработанных нормочасов
				cell_txt = "G" + row_txt;
				txt = production.card_guaranty.hours.ToString();
				CellText(ws, cell_txt, txt);
				// Количество отработанных нормочасов по сервиспакетам
				cell_txt = "H" + row_txt;
				txt = production.card_guaranty.hours_sp.ToString();
				CellText(ws, cell_txt, txt);
				// Сумма не раскрученных по нормаам времени сервис пакетов
				cell_txt = "I" + row_txt;
				txt = production.card_guaranty.cash_sp_nohours.ToString();
				CellText(ws, cell_txt, txt);
				// Количество не раскрученных по нормаам времени сервис пакетов
				cell_txt = "J" + row_txt;
				txt = production.card_guaranty.count_sp_nohours.ToString();
				CellText(ws, cell_txt, txt);

				// Данные по внутренним з/н
				// Количество отработанных нормочасов
				cell_txt = "K" + row_txt;
				txt = production.card_inner.hours.ToString();
				CellText(ws, cell_txt, txt);
				// Количество отработанных нормочасов по сервиспакетам
				cell_txt = "L" + row_txt;
				txt = production.card_inner.hours_sp.ToString();
				CellText(ws, cell_txt, txt);
				// Сумма не раскрученных по нормаам времени сервис пакетов
				cell_txt = "M" + row_txt;
				txt = production.card_inner.cash_sp_nohours.ToString();
				CellText(ws, cell_txt, txt);
				// Количество не раскрученных по нормаам времени сервис пакетов
				cell_txt = "N" + row_txt;
				txt = production.card_inner.count_sp_nohours.ToString();
				CellText(ws, cell_txt, txt);

				// Переход к следующей строке
				row_last = row.ToString();
				row++;
			}
		}
		protected void DataToExcelSheet2(Excel.Worksheet ws, int start, int pos)
		{
			string row_last = "1";
			string row_txt;
			string cell_txt;
			int row = start;
			int row_summ = 2;
			string txt;

			long card_number		= 0;
			int card_year			= 0;
			long card_number_old	= 0;
			int card_year_old		= 0;
			DtCard card				= null;
			DtAuto auto				= null;

			// Персонал
			DtStaff staff = (DtStaff)staffs[pos];
			// Получение данных о выработке
			// DrStuffProduction production = new DrStuffProduction((long)staff.GetData("КОД_ПЕРСОНАЛ"), year, month);
			DrStuffProduction production;
			if (variant == 1)
				production = new DrStuffProduction((long)staff.GetData("КОД_ПЕРСОНАЛ"), year, month);
			else
				production = new DrStuffProduction((long)staff.GetData("КОД_ПЕРСОНАЛ"), start_date, end_date);

			foreach(DtCardWork work in production.works)
			{
				DtTxtCardWork txtCardWork = new DtTxtCardWork(work);
				// Получение данных о заказ/наряде
				bool	is_sp		= false;
				float	local_sp	= 0.0F;
				card_number = work.CardNumber;// (long)work.GetData("НОМЕР_КАРТОЧКА_КАРТОЧКА_РАБОТА");
				card_year = work.CardYear;// (int)work.GetData("ГОД_КАРТОЧКА_КАРТОЧКА_РАБОТА");
				if(card_year_old != card_year || card_number_old != card_number)
				{
					card = DbSqlCard.Find(card_number, card_year);
					card_number_old	= card_number;
					card_year_old	= card_year;

					// Дополнительное инфо по автомобилю
					auto = DbSqlAuto.Find((long)card.CodeAuto/*GetData("АВТОМОБИЛЬ_КАРТОЧКА")*/);
				}
				// Дополнительные данные о работе
				if (work.WorkNV == 0.0F)//if((float)work.GetData("ТРУДОЕМКОСТЬ_КАРТОЧКА_РАБОТА") == 0.0F)
				{
					is_sp	= true;
					local_sp	= DrStuffProduction.Production.GetSPHours(work);
				}
				else
				{
					is_sp	= false;
				}

				// Выставление данных в Excel
				row_txt = row.ToString();
				// Данные о заказ-наряде
				cell_txt = "A" + row_txt;
				if(card != null)
				{
					txt = card.GetData("НОМЕР_НАРЯД_КАРТОЧКА").ToString() + "(" + card_number.ToString() + ")" + " / " + card_year.ToString() + " " + ((DateTime)card.GetData("ДАТА_НАРЯД_ЗАКРЫТ_КАРТОЧКА")).ToShortDateString();
					if (auto != null)
						txt += " " + auto.GetData("МОДЕЛЬ").ToString();
				}
				else
					txt = "";
				CellText(ws, cell_txt, txt);
				// Код работы
				cell_txt = "B" + row_txt;
				txt = txtCardWork.CatalogueNumber; //txt = (string)work.GetData("НОМЕР_ПОЗИЦИЯ_КАРТОЧКА_РАБОТА");
				CellText(ws, cell_txt, txt);
				// Наименование работы
				cell_txt = "C" + row_txt;
				txt = txtCardWork.WorkName; //txt = (string)work.GetData("НАИМЕНОВАНИЕ_КАРТОЧКА_РАБОТА");
				CellText(ws, cell_txt, txt);
				// Количество
				cell_txt = "D" + row_txt;
				txt = txtCardWork.OperationAmount; // txt = work.GetData("КОЛИЧЕСТВО_КАРТОЧКА_РАБОТА").ToString();
				CellText(ws, cell_txt, txt);
				// Трудоемкость (НВ)
				cell_txt = "E" + row_txt;
				txt = txtCardWork.Amount; // txt = work.GetData("ТРУДОЕМКОСТЬ_КАРТОЧКА_РАБОТА").ToString();
				CellText(ws, cell_txt, txt);
				// Стоимость нормачаса
				cell_txt = "F" + row_txt;
				txt = txtCardWork.Price; // txt = work.GetData("НОРМАЧАС_КАРТОЧКА_РАБОТА").ToString();
				CellText(ws, cell_txt, txt);
				// Количество исполнителей
				cell_txt = "G" + row_txt;
				txt = txtCardWork.ExecutorsCount;//txt = work.GetData("КОЛИЧЕСТВО_ИСПОЛНИТЕЛЕЙ").ToString();
				CellText(ws, cell_txt, txt);
				// Раскрутка по времени сервис пакета
				cell_txt = "H" + row_txt;
				if(is_sp == true)
					txt = local_sp.ToString();
				else
					txt = "";
				CellText(ws, cell_txt, txt);
				// Гарантия или нет
				cell_txt = "I" + row_txt;
				if(work.GuaranteeFlag() == true)
					txt = "+";
				else
					txt = "";
				CellText(ws, cell_txt, txt);
				// Тип заказ-наряда
				if((bool)card.GetData("ВНУТРЕННИЙ_КАРТОЧКА") == true)
					txt = "Внутр";
				else
					txt = "Клиент";
				cell_txt = "J" + row_txt;
				CellText(ws, cell_txt, txt);
				// Общее приведенное количество норм времени
				cell_txt = "K" + row_txt;
				txt = "=(E" + row_txt + "+H" + row_txt + ")*D" + row_txt + "/G" + row_txt;
				CellText(ws, cell_txt, txt);
				
				// Переход к следующей строке
				row_last = row.ToString();
				row++;
			}	
		}
		#endregion

		override protected void TitleFormatMult(Excel.Worksheet ws, int sheet)
		{
			if(sheet == 1)
			{
				TitleFormatSheet1(ws);
				return;
			}
			if(sheet > 1 && sheet < this.staffs.Count + 2)
			{
				TitleFormatSheet2(ws, sheet - 2);
				return;
			}
		}
		override protected void DataToExcelMult(Excel.Worksheet ws, int sheet, int start)
		{
			if(sheet == 1)
			{
				DataToExcelSheet1(ws, start);
				return;
			}
			if(sheet > 1 && sheet < this.staffs.Count + 2)
			{
				DataToExcelSheet2(ws, start, sheet - 2);
				return;
			}
		}
	}
}
