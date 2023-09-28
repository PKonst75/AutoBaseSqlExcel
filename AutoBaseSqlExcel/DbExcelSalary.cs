using System;
using System.Collections;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbExcelSalary.
	/// </summary>
	public class DbExcelSalary:DbExcel
	{
		ArrayList	staffs;
		ArrayList	washers;
		int			year;
		int			month;

		DateTime	start_date1;
		DateTime	end_date1;
		int			variant;

		public DbExcelSalary()
		{
			staffs = new ArrayList();
			DbSqlStaff.SelectInArray(staffs, 1);

			washers = new ArrayList();
			DbSqlStaff.SelectInArray(washers, 3);

			// Запрос месяца выгрузки
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
				start_date1 = DateTime.Now;
				end_date1 = DateTime.Now;
				FormSelectDate dialog = new FormSelectDate();
				if(dialog.ShowDialog() == DialogResult.OK)
					start_date1 = dialog.SelectedDate;
				if(dialog.ShowDialog() == DialogResult.OK)
					end_date1 = dialog.SelectedDate;
				variant = 2;
			}
		}
		override protected void TitleFormat(Excel.Worksheet ws)
		{
			// Данные о персонале
			FormatColumn(ws, "A1", 27, 8, "Left");
			CellText(ws, "A1", "Фамилия Имя Отчество", "Center", 10, true);
			// Данные о зарплате - ИТОГ С БОНУСАМИ и ВЫЧЕТАМИ
			FormatColumn(ws, "B1", 12, 8, "Right", true);
			CellText(ws, "B1", "ИТОГ", "Center", 10, true);
			// Данные о зарплате
			FormatColumn(ws, "C1", 12, 8, "Right");
			CellText(ws, "C1", "Расчет", "Center", 10, true);
			// Данные о зарплате (c учетом коэффициента)
			FormatColumn(ws, "D1", 12, 8, "Right");
			CellText(ws, "D1", "Коэф.", "Center", 10, true);

			// Для бонусов и вычетов
			// Стандартные вычеты за косяки
			FormatColumn(ws, "E1", 12, 8, "Right");
			CellText(ws, "E1", "Штраф", "Center", 10, true);
			// Ручные бонусы
			FormatColumn(ws, "F1", 12, 8, "Right");
			CellText(ws, "F1", "Бонус", "Center", 10, true);

			// Часть за договорные работы
			FormatColumn(ws, "I1", 8, 8, "Right");
			CellText(ws, "I1", "За $", "Center", 10, true);
			// Часть за работы по нормачасам
			FormatColumn(ws, "J1", 8, 8, "Right");
			CellText(ws, "J1", "За н/ч", "Center", 10, true);
			// Часть за нерасцененные нормачасы
			FormatColumn(ws, "K1", 8, 8, "Right");
			CellText(ws, "K1", "За часы", "Center", 10, true);
			// Часть за предпродажную подготовку
			FormatColumn(ws, "L1", 8, 8, "Right");
			CellText(ws, "L1", "За ППП", "Center", 10, true);
			// Часть за гарантийные работы
			FormatColumn(ws, "M1", 8, 8, "Right");
			CellText(ws, "M1", "За гар.", "Center", 10, true);

			// По Договоным
			FormatColumn(ws, "O1", 8, 8, "Right");
			CellText(ws, "O1", "$", "Center", 10, true);
			// Количество ТО
			FormatColumn(ws, "P1", 8, 8, "Right");
			CellText(ws, "P1", "TO", "Center", 10, true);
			// По ТО
			FormatColumn(ws, "Q1", 8, 8, "Right");
			CellText(ws, "Q1", "TO$", "Center", 10, true);
			// ПО нормачасам
			FormatColumn(ws, "R1", 8, 8, "Right");
			CellText(ws, "R1", "ПО Н/Ч $", "Center", 10, true);
			// Нормачасы - за деньги
			FormatColumn(ws, "S1", 8, 8, "Right");
			CellText(ws, "S1", "Н/Ч", "Center", 10, true);
			// Нормачасы - нерасцененные
			FormatColumn(ws, "T1", 8, 8, "Right");
			CellText(ws, "T1", "Н/Ч 0", "Center", 10, true);
			// Нормачасы - гарантийные
			FormatColumn(ws, "U1", 8, 8, "Right");
			CellText(ws, "U1", "Н/Ч гар", "Center", 10, true);
			// Количество предпродажек
			FormatColumn(ws, "W1", 8, 8, "Right");
			CellText(ws, "W1", "ППП", "Center", 10, true);
		}
		override protected void DataToExcel(Excel.Worksheet ws, int start)
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
				// Получение данных о зарплате
				//DtSalary salary = new DtSalary((long)staff.GetData("КОД_ПЕРСОНАЛ"), year, month);
				DtSalary salary;
				if (variant == 1)
					salary = new DtSalary((long)staff.GetData("КОД_ПЕРСОНАЛ"), year, month);
				else
					salary = new DtSalary((long)staff.GetData("КОД_ПЕРСОНАЛ"), start_date1, end_date1);

				// Выставление данных в Excel
				row_txt = row.ToString();
				// Данные о персонале
				cell_txt = "A" + row_txt;
				txt = staff.GetData("ФАМИЛИЯ_ПЕРСОНАЛ") + " " +staff.GetData("ИМЯ_ПЕРСОНАЛ") + " " + staff.GetData("ОТЧЕСТВО_ПЕРСОНАЛ");
				CellText(ws, cell_txt, txt);
				// ИТОГ ПО ЗАРПЛАТЕ
				cell_txt = "B" + row_txt;
				txt = "=D" + row_txt + "-" + "E" + row_txt + "+" + "F" + row_txt;
				CellText(ws, cell_txt, txt);
				// Данные о зарплате
				cell_txt = "C" + row_txt;
				txt = salary.salary.ToString();
				CellText(ws, cell_txt, txt);
				// Данные о зарплате с учетом разрадного коэффициента
				cell_txt = "D" + row_txt;
				txt = (salary.salary*(float)staff.GetData("РАЗРЯД_КОЭФФИЦИЕНТ")).ToString();
				CellText(ws, cell_txt, txt);
				// Часть от договорных работ
				cell_txt = "I" + row_txt;
				txt = salary.salary_cash.ToString();
				CellText(ws, cell_txt, txt);
				// Часть от слесарных работ
				cell_txt = "J" + row_txt;
				txt = salary.salary_cash_hour.ToString();
				CellText(ws, cell_txt, txt);
				// Часть от нерацененных нормачасов
				cell_txt = "K" + row_txt;
				txt = salary.salary_hour.ToString();
				CellText(ws, cell_txt, txt);
				// Часть за ППП
				cell_txt = "L" + row_txt;
				txt = salary.salary_ppp.ToString();
				CellText(ws, cell_txt, txt);
				// Часть за гарантию
				cell_txt = "M" + row_txt;
				txt = salary.salary_guarantee.ToString();
				CellText(ws, cell_txt, txt);

				// Договорные
				cell_txt = "O" + row_txt;
				txt = salary.cash.ToString();
				CellText(ws, cell_txt, txt);
				// Количество ТО
				cell_txt = "P" + row_txt;
				txt = salary.to_count.ToString();
				CellText(ws, cell_txt, txt);
				// По ТО
				cell_txt = "Q" + row_txt;
				txt = salary.cash_to.ToString();
				CellText(ws, cell_txt, txt);
				// По Нормачасам
				cell_txt = "R" + row_txt;
				txt = salary.cash_hour.ToString();
				CellText(ws, cell_txt, txt);
				// Нормачасы
				cell_txt = "S" + row_txt;
				txt = salary.cash_hour_count.ToString();
				CellText(ws, cell_txt, txt);
				// Нерасцененные нормачасы
				cell_txt = "T" + row_txt;
				txt = salary.hour.ToString();
				CellText(ws, cell_txt, txt);
				// Гарантийные нормачасы
				cell_txt = "U" + row_txt;
				txt = salary.guarantee_hour.ToString();
				CellText(ws, cell_txt, txt);
				// Предпродажки
				cell_txt = "V" + row_txt;
				txt = (salary.ppp_count + salary.guarantee_ppp_count).ToString();
				CellText(ws, cell_txt, txt);

				// Переход к следующей строке
				row_last = row.ToString();
				row++;
			}

			// Завершение документа
			// Сумма итоговой З/П
			row_summ	= row;
			row_txt = row.ToString();
			cell_txt = "B" + row_txt;
			txt = "=СУММ(B2:B" + row_last + ")";
			CellText(ws, cell_txt, txt);
			// Сумма З/П окладников
			row_txt = (row + 1).ToString();
			cell_txt = "A" + row_txt;
			txt = "Окладники";
			CellText(ws, cell_txt, txt);
			cell_txt = "B" + row_txt;
			txt = "80000";
			CellText(ws, cell_txt, txt);
			// Сумма расчетной З/П
			row_txt = row.ToString();
			cell_txt = "C" + row_txt;
			txt = "=СУММ(C2:C" + row_last + ")";
			CellText(ws, cell_txt, txt);
			// Сумма расчетной с коэффициентом З/П
			row_txt = row.ToString();
			cell_txt = "D" + row_txt;
			txt = "=СУММ(D2:D" + row_last + ")";
			CellText(ws, cell_txt, txt);
			// Сумма штрафов
			row_txt = row.ToString();
			cell_txt = "E" + row_txt;
			txt = "=СУММ(E2:E" + row_last + ")";
			CellText(ws, cell_txt, txt);
			// Сумма бонусов
			row_txt = row.ToString();
			cell_txt = "F" + row_txt;
			txt = "=СУММ(F2:F" + row_last + ")";
			CellText(ws, cell_txt, txt);
			// Сумма за ППП
			row_txt = row.ToString();
			cell_txt = "L" + row_txt;
			txt = "=СУММ(L2:L" + row_last + ")";
			CellText(ws, cell_txt, txt);
			// Сумма за Гарантию
			row_txt = row.ToString();
			cell_txt = "M" + row_txt;
			txt = "=СУММ(M2:M" + row_last + ")";
			CellText(ws, cell_txt, txt);
			// Сумма за Гарантию и ППП - Дополнительная!
			row_txt = (row + 1).ToString();
			cell_txt = "L" + row_txt;
			txt = "=L" + row_summ.ToString() + "+" + "M" + row_summ.ToString();
			CellText(ws, cell_txt, txt, "Center", 8, true);


			row = row + 4;
			// Данные по мойщикам
			foreach(object o in washers)
			{
				// Персонал
				DtStaff staff = (DtStaff)o;
				// Получение данных о зарплате
				//DtSalary salary = new DtSalary((long)staff.GetData("КОД_ПЕРСОНАЛ"), year, month);
				DtSalary salary;
				if (variant == 1)
					salary = new DtSalary((long)staff.GetData("КОД_ПЕРСОНАЛ"), year, month);
				else
					salary = new DtSalary((long)staff.GetData("КОД_ПЕРСОНАЛ"), start_date1, end_date1);

				// Выставление данных в Excel
				row_txt = row.ToString();
				// Данные о персонале
				cell_txt = "A" + row_txt;
				txt = staff.GetData("ФАМИЛИЯ_ПЕРСОНАЛ") + " " +staff.GetData("ИМЯ_ПЕРСОНАЛ") + " " + staff.GetData("ОТЧЕСТВО_ПЕРСОНАЛ");
				CellText(ws, cell_txt, txt);
				// Количество моек
				cell_txt = "B" + row_txt;
				txt = salary.wash_count.ToString();
				CellText(ws, cell_txt, txt);

				row++;
			}


			// Получение данных о выручке сервиса
			DateTime start_date;
			DateTime end_date;
			if(variant == 1)
			{
				start_date = new DateTime(year, month, 1);
				end_date = new DateTime(year, month, 1);
				end_date = end_date.AddMonths(1);
				end_date = end_date.AddDays(-1);
			}
			else
			{
				start_date	= start_date1;
				end_date	= end_date1;
			}
			//end_date = end_date.AddDays(-2);
			DbProduction product = new DbProduction(start_date, end_date, 1);
			row += 5;
			row_txt = row.ToString();
			// Денежный заработок сервиса
			cell_txt = "A" + row_txt;
			txt = "Выручка сервиса";
			CellText(ws, cell_txt, txt);
			cell_txt = "B" + row_txt;
			txt = product.Cash.ToString();
			CellText(ws, cell_txt, txt);
			cell_txt = "C" + row_txt;
			txt = product.Cash.ToString();
			CellText(ws, cell_txt, txt);
			// Процент зарплаты слесарей от выручки сервиса
			row++;
			row_txt = row.ToString();
			cell_txt = "A" + row_txt;
			txt = "Процент зарплаты";
			CellText(ws, cell_txt, txt);
			cell_txt = "B" + row_txt;
			txt = "=B" + row_summ.ToString() + "/B" + (row-1).ToString() +"*100";
			CellText(ws, cell_txt, txt);
			// Процент зарплаты всего сервиса от выручки сервиса
			row++;
			row_txt = row.ToString();
			cell_txt = "A" + row_txt;
			txt = "Процент зарплаты сервиса";
			CellText(ws, cell_txt, txt);
			cell_txt = "B" + row_txt;
			txt = "=(B" + row_summ.ToString()+ "+" + "B" + (row_summ+1).ToString()+ ")/B" + (row-2).ToString() +"*100";
			CellText(ws, cell_txt, txt);
		}
	}
}
