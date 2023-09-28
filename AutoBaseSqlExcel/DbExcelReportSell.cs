using System;
using System.Collections;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbExcelReportSell.
	/// </summary>
	public class DbExcelReportSell:DbExcel
	{
		ArrayList array = new ArrayList();		// Список продаж
		struct INFO
		{
			public DtAutoSell sell;
			public DtAuto	auto;
			public CS_SellInfo info;
			public DtAutoSellServ info_serv;
		} 

		public DbExcelReportSell()
		{
			// Запрос даты начала отсчета и конца отсчета
			FormSelectDate dlg = new FormSelectDate();
			dlg.ShowDialog();
			if(dlg.DialogResult != DialogResult.OK) return;
			DateTime date_start = dlg.SelectedDate;

			dlg.ShowDialog();
			if(dlg.DialogResult != DialogResult.OK) return;
			DateTime date_end = dlg.SelectedDate;

			// Выбираем все продажи за заданный период времени
			ArrayList sells = new ArrayList();
			DbSqlAutoSell.SearchMask mask = new DbSqlAutoSell.SearchMask();
			mask.timeon = true;

			DateTime date_start_0 = new DateTime(date_start.Year, date_start.Month, date_start.Day, 0, 0, 0, 0);
			DateTime date_end_0 = new DateTime(date_end.Year, date_end.Month, date_end.Day, 23, 59, 59, 0);

			mask.date_start = date_start_0;
			mask.date_stop = date_end_0;
			DbSqlAutoSell.SelectInArray(sells, mask);

			FormInfoTable info = new FormInfoTable("Начало отсчета");
			info.Show();
			foreach(DtAutoSell element in sells)
			{		
				array.Add(element);
				info.SetText(element.GetData("ДАТА_АВТОМОБИЛЬ_ПРОДАЖА").ToString());
			}
			info.SetText("Конец отсчета");
			info.Close();
		}

		protected void TitleFormatSheet1(Excel.Worksheet ws)
		{
			// Дата продажи
			FormatColumn(ws, "A1", 12, 8, "Left");
			CellText(ws, "A1", "Дата продажи", "Center", 10, true);
			// VIN автомобиля
			FormatColumn(ws, "B1", 18, 8, "Right", true);
			CellText(ws, "B1", "VIN", "Center", 10, true);
			// МОДЕЛЬ
			FormatColumn(ws, "C1", 25, 8, "Right");
			CellText(ws, "C1", "Модель", "Center", 10, true);
			// ДАТА ПОЛУЧЕНИЯ АВТО
			FormatColumn(ws, "D1", 12, 8, "Right");
			CellText(ws, "D1", "Дата получения", "Center", 10, true);
			// КРЕДИТ
			FormatColumn(ws, "E1", 8, 8, "Right");
			CellText(ws, "E1", "Кредит", "Center", 10, true);
			// Менеджер
			FormatColumn(ws, "F1", 16, 8, "Right");
			CellText(ws, "F1", "Менеджер", "Center", 10, true);
			// СУММА АВТО
			FormatColumn(ws, "G1", 8, 8, "Right");
			CellText(ws, "G1", "Авто сум", "Center", 10, true);
			// СКИДКА АВТО
			FormatColumn(ws, "H1", 8, 8, "Right");
			CellText(ws, "H1", "Авто скид", "Center", 10, true);

			FormatColumn(ws, "I1", 1, 8, "Right");
			FormatColumn(ws, "J1", 1, 8, "Right");
			FormatColumn(ws, "K1", 1, 8, "Right");

			// МУЗЫКА
			FormatColumn(ws, "L1", 4, 8, "Right");
			CellText(ws, "L1", "М", "Center", 10, true);
			// СИГНАЛИЗАЦИЯ
			FormatColumn(ws, "M1", 4, 8, "Right");
			CellText(ws, "M1", "С", "Center", 10, true);
			// ТЮНИНГ
			FormatColumn(ws, "N1", 4, 8, "Right");
			CellText(ws, "N1", "Т", "Center", 10, true);
			// ДОПЫ
			FormatColumn(ws, "O1", 4, 8, "Right");
			CellText(ws, "O1", "Д", "Center", 10, true);
			// СУММА ДОПОВ
			FormatColumn(ws, "P1", 8, 8, "Right");
			CellText(ws, "P1", "Допы сум", "Center", 10, true);
			// СУММА СКИДКИ + ПОДАРОК
			FormatColumn(ws, "Q1", 8, 8, "Right");
			CellText(ws, "Q1", "Скид+Под", "Center", 10, true);

			FormatColumn(ws, "R1", 1, 8, "Right");

			// АНТИКОР
			FormatColumn(ws, "S1", 4, 8, "Right");
			CellText(ws, "S1", "А", "Center", 10, true);
			// ЗАЩИТА
			FormatColumn(ws, "T1", 4, 8, "Right");
			CellText(ws, "T1", "З", "Center", 10, true);
			// ПОДКРЫЛКИ
			FormatColumn(ws, "U1", 4, 8, "Right");
			CellText(ws, "U1", "П", "Center", 10, true);
			// СУММА АНТИКОР
			FormatColumn(ws, "V1", 8, 8, "Right");
			CellText(ws, "V1", "Ант сумм", "Center", 10, true);
			// СУММА СКИДКИ АНТИКОР
			FormatColumn(ws, "W1", 8, 8, "Right");
			CellText(ws, "W1", "Скид ант", "Center", 10, true);

			FormatColumn(ws, "X1", 1, 8, "Right");

			// ГИБДД РЕГИСТРАЦИЯ
			FormatColumn(ws, "Y1", 4, 8, "Right");
			CellText(ws, "Y1", "ГИБДД", "Center", 10, true);
			// ДКП СУММА
			FormatColumn(ws, "Z1", 8, 8, "Right");
			CellText(ws, "Z1", "ДКП сум", "Center", 10, true);

			FormatColumn(ws, "AA1", 1, 8, "Right");

			// КАСКО
			FormatColumn(ws, "AB1", 4, 8, "Right");
			CellText(ws, "AB1", "Каско", "Center", 10, true);
			// ОСАГО
			FormatColumn(ws, "AC1", 4, 8, "Right");
			CellText(ws, "AC1", "Осаго", "Center", 10, true);
		}

		protected void DataToExcelSheet1(Excel.Worksheet ws, int start)
		{
			string row_last = "1";
			string row_txt;
			string cell_txt;
			int row = start;
			int row_summ = 2;
			string txt;

			DtAuto auto = null;
			DtAutoReceive receive = null;
			CS_SellInfo info = null;
			DtAutoSellServ serv = null;

			foreach(object o in array)
			{
				// Картички
				DtAutoSell sell = (DtAutoSell)o;

				// Загружаем дополнительные данные
				auto = DbSqlAuto.Find((long)sell.GetData("ССЫЛКА_КОД_АВТОМОБИЛЬ"));
				receive = DbSqlAutoReceive.FindAuto((long)sell.GetData("ССЫЛКА_КОД_АВТОМОБИЛЬ"));
				info = DbSqlSellInfo.Find((long)sell.GetData("КОД_АВТОМОБИЛЬ_ПРОДАЖА"));
				serv = DbSqlAutoSellServ.Find((long)sell.GetData("КОД_АВТОМОБИЛЬ_ПРОДАЖА"));
				
				// Выставление данных в Excel
				row_txt = row.ToString();
				// Дата продажи
				cell_txt = "A" + row_txt;
				txt = ((DateTime)sell.GetData("ДАТА_АВТОМОБИЛЬ_ПРОДАЖА")).ToShortDateString();
				CellText(ws, cell_txt, txt);
				// VIN
				txt = "";
				if (auto != null)
					txt = (string)auto.GetData("VIN");
				cell_txt = "B" + row_txt;
				CellText(ws, cell_txt, txt);
				// МОДЕЛЬ
				cell_txt = "C" + row_txt;
				txt = "";
				if (auto != null)
					txt = (string)auto.GetData("МОДЕЛЬ");
				CellText(ws, cell_txt, txt);
				// ДАТА ПОЛУЧЕНИЯ АВТО
				cell_txt = "D" + row_txt;
				txt = "";
				if (receive != null)
					txt = ((DateTime)receive.GetData("ДАТА_ДОКУМЕНТ")).ToShortDateString();
				CellText(ws, cell_txt, txt);
				// КРЕДИТ
				cell_txt = "E" + row_txt;
				txt = "";
				if (info != null)
					if(info.flag_credit_inner == true || info.flag_credit_outer == true)
						txt = "ДА";
				CellText(ws, cell_txt, txt);

				// Расширенная информация
				if (serv != null)
				{
					// Менеджер
					cell_txt = "F" + row_txt;
					DtStaff staff = DbSqlStaff.Find(serv.code_manager);
					if (staff != null)
						txt = staff.Title;
					else
						txt = "";
					CellText(ws, cell_txt, txt);
					// СУММА АВТО
					cell_txt = "G" + row_txt;
					txt = (serv.auto_summ).ToString();
					CellText(ws, cell_txt, txt);
					// СКИДКА АВТО
					cell_txt = "H" + row_txt;
					txt = (serv.auto_discount_money).ToString();
					CellText(ws, cell_txt, txt);

					// МУЗЫКА
					cell_txt = "L" + row_txt;
					if (serv.flag_music == true)
						txt = "1";
					else
						txt = "0";
					CellText(ws, cell_txt, txt);
					// СИГНАЛИЗАЦИЯ
					cell_txt = "M" + row_txt;
					if (serv.flag_alarm == true)
						txt = "1";
					else
						txt = "0";
					CellText(ws, cell_txt, txt);
					// ТЮНИНГ
					cell_txt = "N" + row_txt;
					if (serv.flag_tune == true)
						txt = "1";
					else
						txt = "0";
					CellText(ws, cell_txt, txt);
					// ДОПЫ
					cell_txt = "O" + row_txt;
					if (serv.flag_other == true)
						txt = "1";
					else
						txt = "0";
					CellText(ws, cell_txt, txt);
					// СУММА ДОПОВ
					cell_txt = "P" + row_txt;
					txt = (serv.summ_whole).ToString();
					CellText(ws, cell_txt, txt);
					// СКИДКА С ДОПОВ + ПОДАРОК
					cell_txt = "Q" + row_txt;
					txt = (serv.auto_discount_tunemus + serv.auto_discount_other).ToString();
					CellText(ws, cell_txt, txt);

					// АНТИКОР
					cell_txt = "S" + row_txt;
					if (serv.flag_anti == true)
						txt = "1";
					else
						txt = "0";
					CellText(ws, cell_txt, txt);
					// ЗАЩИТА
					cell_txt = "T" + row_txt;
					if (serv.flag_anti == true)
						txt = "1";
					else
						txt = "0";
					CellText(ws, cell_txt, txt);
					// ПОДКРЫЛКИ
					cell_txt = "U" + row_txt;
					if (serv.flag_anti == true)
						txt = "1";
					else
						txt = "0";
					CellText(ws, cell_txt, txt);
					// СУММА АНТИКОР
					cell_txt = "V" + row_txt;
					txt = (serv.summ_anti).ToString();
					CellText(ws, cell_txt, txt);
					// СКИДКА АНТИКОР
					cell_txt = "W" + row_txt;
					txt = (serv.auto_discount_anti).ToString();
					CellText(ws, cell_txt, txt);

					// ГИБДД
					cell_txt = "Y" + row_txt;
					if (serv.flag_gibdd == true)
						txt = "1";
					else
						txt = "0";
					CellText(ws, cell_txt, txt);
					// СУММА ДКП
					cell_txt = "Z" + row_txt;
					txt = (serv.summ_sprav).ToString();
					CellText(ws, cell_txt, txt);

					// КАСКО
					cell_txt = "AB" + row_txt;
					if (serv.flag_kasko == true)
						txt = "1";
					else
						txt = "0";
					CellText(ws, cell_txt, txt);
					// ОСАГО
					cell_txt = "AC" + row_txt;
					if (serv.flag_osago == true)
						txt = "1";
					else
						txt = "0";
					CellText(ws, cell_txt, txt);

					/*
					// Антикор
					cell_txt = "J" + row_txt;
					if (serv.flag_anti == true)
						txt = "+";
					else
						txt = "-";
					if (serv.flag_anti1 == true)
						txt += "+";
					else
						txt += "-";
					if (serv.flag_anti2 == true)
						txt += "+";
					else
						txt += "-";
					CellText(ws, cell_txt, txt);
					
					// Гаи/Договор
					cell_txt = "K" + row_txt;
					if (serv.flag_gibdd == true)
						txt = "+";
					else
						txt = "-";
					if (serv.flag_sprav == true)
						txt += "+";
					else
						txt += "-";
					CellText(ws, cell_txt, txt);
					// Каско/Осаго
					cell_txt = "L" + row_txt;
					if (serv.flag_kasko == true)
						txt = "+";
					else
						txt = "-";
					if (serv.flag_osago == true)
						txt += "+";
					else
						txt += "-";
					CellText(ws, cell_txt, txt);

					// Цена АВТО
					cell_txt = "M" + row_txt;
					txt = serv.auto_summ.ToString();
					CellText(ws, cell_txt, txt);
					// Скидка АВТО
					cell_txt = "N" + row_txt;
					txt = serv.auto_discount_money.ToString();
					CellText(ws, cell_txt, txt);
					// Допы АВТО
					cell_txt = "O" + row_txt;
					txt = serv.summ_whole.ToString();
					CellText(ws, cell_txt, txt);
					// Скидка с допов+подарок
					cell_txt = "P" + row_txt;
					txt = (serv.auto_discount_tunemus + serv.auto_discount_other).ToString();
					CellText(ws, cell_txt, txt);
					// Антикор
					cell_txt = "Q" + row_txt;
					txt = serv.summ_anti.ToString();
					CellText(ws, cell_txt, txt);
					// Антикор скидка
					cell_txt = "R" + row_txt;
					txt = serv.auto_discount_anti.ToString();
					CellText(ws, cell_txt, txt);
					*/

				}

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
