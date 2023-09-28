using System;
using System.Collections;
using Excel = Microsoft.Office.Interop.Excel;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbExcelWorkshopWorks.
	/// </summary>
	public class DbExcelWorkshopWorks:DbExcel
	{
		public ArrayList	cards;
		public ArrayList	cards_open;
		DateTime			date_start;
		DateTime			date_stop;
		long				workshop;

		public DbExcelWorkshopWorks(DateTime start, DateTime stop, long ws)
		{
			workshop	= ws;
			date_start	= start;
			date_stop	= stop;

			cards		= new ArrayList();
			DbSqlCard.SelectCardClosedNumberWorkshop(cards, start, stop, ws);

			cards_open		= new ArrayList();
			DbSqlCard.SelectCardOpenNumberWorkshop(cards_open, ws);
		}

		override protected void TitleFormat(Excel.Worksheet ws)
		{
			// Дата оплаты
			FormatColumn(ws, "A1", 12, 8, "Left");
			CellText(ws, "A1", "Дата оплаты", "Center", 10, true);
			// Сумма
			FormatColumn(ws, "B1", 8, 8, "Right", true);
			FormatColumnNumberFormat(ws, "B1", "# ##0,00");
			CellText(ws, "B1", "Сумма", "Center", 10, true);
			// Вид расчета
			FormatColumn(ws, "C1", 4, 8, "Center");
			CellText(ws, "C1", "Hасчет", "Center", 10, true);
			// Заказ-наряд
			FormatColumn(ws, "D1", 6, 8, "Left");
			CellText(ws, "D1", "З/Н", "Center", 10, true);
			// Модель
			FormatColumn(ws, "E1", 16, 8, "Left");
			CellText(ws, "E1", "Модель", "Center", 10, true);
			// Номер кузова
			FormatColumn(ws, "F1", 8, 8, "Left");
			FormatColumnNumberFormat(ws, "F1", "@");
			CellText(ws, "F1", "Кузов", "Center", 10, true);

			// Работы
			// Наименование
			FormatColumn(ws, "G1", 48, 8, "Left");
			CellText(ws, "G1", "Наименование работы", "Center", 10, true);
			// Количество
			FormatColumn(ws, "H1", 4, 8, "Right");
			CellText(ws, "H1", "К-во", "Center", 10, true);
			// Стоимость
			FormatColumn(ws, "I1", 8, 8, "Right");
			FormatColumnNumberFormat(ws, "I1", "# ##0,00");
			CellText(ws, "I1", "Стоимость", "Center", 10, true);
		}

		override protected void DataToExcel(Excel.Worksheet ws, int start)
		{
			string row_last = "1";
			string row_txt;
			string cell_txt;
			int row = start;
			int row_summ = 2;
			string txt;

			CalculatorCard calc = new CalculatorCard(CALCULATOR_TYPE.CALCULATOR_PAY, VAT_TYPE.VAT_NON, 0);


			foreach(object o in cards)
			{
				// Заказ-наряд
				DtCard card = (DtCard)o;
				// Загружаем полные данные по заказ-наряду
				DtCard card_data = DbSqlCard.Find((long)card.GetData("НОМЕР_КАРТОЧКА"), (int)card.GetData("ГОД_КАРТОЧКА"));
				// Загружаем данные по автомобилю
				DtAuto auto_data	= DbSqlAuto.Find((long)card_data.CodeAuto/*GetData("АВТОМОБИЛЬ_КАРТОЧКА")*/);
				
				// Загружаем список работ по заказ-наряду
				ArrayList works = new ArrayList();
				DbSqlCardWork.SelectInArray(card_data, works);

				// Обрабатываем список работ
				float summ	= 0.0F;
				foreach(DtCardWork o1 in works)
				{
					//DtCardWork work = (DtCardWork)o1;
					CalculatorResult res = calc.WorkCalculator.Calculate(o1);
					summ += res.SummDatabase;//summ	+= work.WorkSummCash;
				}

				// Выставление данных по заказ-наряду в Excel
				row_txt = row.ToString();
				// Дата оплаты = Дата закрытия
				cell_txt = "A" + row_txt;
				txt = card_data.GetData("ДАТА_НАРЯД_ЗАКРЫТ_КАРТОЧКА").ToString();
				CellText(ws, cell_txt, txt);
				// Сумма оплаты
				cell_txt = "B" + row_txt;
				txt = summ.ToString();
				CellText(ws, cell_txt, txt);
				// Вид расчета (1 если безналичный)
				cell_txt = "C" + row_txt;
				txt = "";
				if ((bool)card_data.GetData("БЕЗНАЛИЧНЫЙ_КАРТОЧКА"))
					txt = "БН";
				CellText(ws, cell_txt, txt);
				// Номер заказ-наряда
				cell_txt = "D" + row_txt;
				txt = card_data.GetData("НОМЕР_НАРЯД_КАРТОЧКА").ToString();
				CellText(ws, cell_txt, txt);
				// Модель автомобиля 
				cell_txt = "E" + row_txt;
				txt = auto_data.GetData("МОДЕЛЬ").ToString();
				CellText(ws, cell_txt, txt);
				// Модель автомобиля 
				cell_txt = "F" + row_txt;
				txt = auto_data.GetData("НОМЕР_КУЗОВ").ToString();
				CellText(ws, cell_txt, txt);

				row++;
				foreach(DtCardWork o1 in works)
				{
					//DtCardWork work = (DtCardWork)o1;
					DtTxtCardWork txtCardWork = new DtTxtCardWork(o1);
					CalculatorResult res = calc.WorkCalculator.Calculate(o1);

					row_txt = row.ToString();
					// Наименование работы
					cell_txt = "G" + row_txt;
					txt = txtCardWork.WorkName;//txt = work.GetData("НАИМЕНОВАНИЕ_КАРТОЧКА_РАБОТА").ToString();
					CellText(ws, cell_txt, txt);
					// Количество
					cell_txt = "H" + row_txt;
					txt = txtCardWork.OperationAmount;//txt = work.GetData("КОЛИЧЕСТВО_КАРТОЧКА_РАБОТА").ToString();
					CellText(ws, cell_txt, txt);
					// Стоимость
					cell_txt = "I" + row_txt;
					txt = res.SummDatabase.ToString();//txt = work.WorkSummCash.ToString();
					CellText(ws, cell_txt, txt);

					row++;
				}
			}
		}

		protected void TitleFormat1(Excel.Worksheet ws)
		{
			// Дата оплаты
			FormatColumn(ws, "A1", 12, 8, "Left");
			CellText(ws, "A1", "Дата оплаты", "Center", 10, true);
			// Сумма
			FormatColumn(ws, "B1", 8, 8, "Right", true);
			FormatColumnNumberFormat(ws, "B1", "# ##0,00");
			CellText(ws, "B1", "Сумма", "Center", 10, true);
			// Вид расчета
			FormatColumn(ws, "C1", 4, 8, "Center");
			CellText(ws, "C1", "Hасчет", "Center", 10, true);
			// Заказ-наряд
			FormatColumn(ws, "D1", 6, 8, "Left");
			CellText(ws, "D1", "З/Н", "Center", 10, true);
			// Модель
			FormatColumn(ws, "E1", 16, 8, "Left");
			CellText(ws, "E1", "Модель", "Center", 10, true);
			// Номер кузова
			FormatColumn(ws, "F1", 8, 8, "Left");
			FormatColumnNumberFormat(ws, "F1", "@");
			CellText(ws, "F1", "Кузов", "Center", 10, true);

			// Работы
			// Наименование
			FormatColumn(ws, "G1", 48, 8, "Left");
			CellText(ws, "G1", "Наименование работы", "Center", 10, true);
			// Количество
			FormatColumn(ws, "H1", 4, 8, "Right");
			CellText(ws, "H1", "К-во", "Center", 10, true);
			// Стоимость
			FormatColumn(ws, "I1", 8, 8, "Right");
			FormatColumnNumberFormat(ws, "I1", "# ##0,00");
			CellText(ws, "I1", "Стоимость", "Center", 10, true);
		}

		protected void DataToExcel1(Excel.Worksheet ws, int start)
		{
			string row_last = "1";
			string row_txt;
			string cell_txt;
			int row = start;
			int row_summ = 2;
			string txt;

			foreach(object o in cards)
			{
				// Заказ-наряд
				DtCard card = (DtCard)o;
				// Загружаем полные данные по заказ-наряду
				DtCard card_data = DbSqlCard.Find(card.Number, card.Year);
				// Загружаем данные по автомобилю
				DtAuto auto_data	= DbSqlAuto.Find((long)card_data.CodeAuto/*GetData("АВТОМОБИЛЬ_КАРТОЧКА")*/);
				
				// Загружаем список работ по заказ-наряду
				ArrayList works = new ArrayList();
				DbSqlCardWork.SelectInArray(card_data, works);

				// Обрабатываем список работ
				float summ	= 0.0F;
				CalculatorCard calc = new CalculatorCard(CALCULATOR_TYPE.CALCULATOR_PAY, VAT_TYPE.VAT_NON, 0);
				foreach(DtCardWork o1 in works)
				{
					DtCardWork work = o1;
					CalculatorResult res = calc.WorkCalculator.Calculate(o1);
					summ += res.SummDatabase; //summ	+= work.WorkSummCash;
				}

				// Выставление данных по заказ-наряду в Excel
				row_txt = row.ToString();
				// Дата оплаты = Дата закрытия
				cell_txt = "A" + row_txt;
				txt = card_data.GetData("ДАТА_НАРЯД_ЗАКРЫТ_КАРТОЧКА").ToString();
				CellText(ws, cell_txt, txt);
				// Сумма оплаты
				cell_txt = "B" + row_txt;
				txt = summ.ToString();
				CellText(ws, cell_txt, txt);
				// Вид расчета (1 если безналичный)
				cell_txt = "C" + row_txt;
				txt = "";
				if ((bool)card_data.GetData("БЕЗНАЛИЧНЫЙ_КАРТОЧКА"))
					txt = "БН";
				CellText(ws, cell_txt, txt);
				// Номер заказ-наряда
				cell_txt = "D" + row_txt;
				txt = card_data.GetData("НОМЕР_НАРЯД_КАРТОЧКА").ToString();
				CellText(ws, cell_txt, txt);
				// Модель автомобиля 
				cell_txt = "E" + row_txt;
				txt = auto_data.GetData("МОДЕЛЬ").ToString();
				CellText(ws, cell_txt, txt);
				// Модель автомобиля 
				cell_txt = "F" + row_txt;
				txt = auto_data.GetData("НОМЕР_КУЗОВ").ToString();
				CellText(ws, cell_txt, txt);

				row++;
				foreach(DtCardWork o1 in works)
				{
					DtCardWork work = o1;
					DtTxtCardWork txtCardWork = new DtTxtCardWork(o1);
					CalculatorResult res = calc.WorkCalculator.Calculate(work);
					row_txt = row.ToString();
					// Наименование работы
					cell_txt = "G" + row_txt;
					txt = txtCardWork.WorkName;//txt = work.GetData("НАИМЕНОВАНИЕ_КАРТОЧКА_РАБОТА").ToString();
					CellText(ws, cell_txt, txt);
					// Количество
					cell_txt = "H" + row_txt;
					txt = txtCardWork.OperationAmount;// txt = work.GetData("КОЛИЧЕСТВО_КАРТОЧКА_РАБОТА").ToString();
					CellText(ws, cell_txt, txt);
					// Стоимость
					cell_txt = "I" + row_txt;
					txt = res.SummDatabase.ToString();//txt = work.WorkSummCash.ToString();
					CellText(ws, cell_txt, txt);

					row++;
				}
			}
		}

		protected void DataToExcel2(Excel.Worksheet ws, int start)
		{
			string row_last = "1";
			string row_txt;
			string cell_txt;
			int row = start;
			int row_summ = 2;
			string txt;

			foreach(object o in cards_open)
			{
				// Заказ-наряд
				DtCard card = (DtCard)o;
				// Загружаем полные данные по заказ-наряду
				DtCard card_data = DbSqlCard.Find((long)card.GetData("НОМЕР_КАРТОЧКА"), (int)card.GetData("ГОД_КАРТОЧКА"));
				// Загружаем данные по автомобилю
				DtAuto auto_data	= DbSqlAuto.Find((long)card_data.CodeAuto/*GetData("АВТОМОБИЛЬ_КАРТОЧКА")*/);
				
				// Загружаем список работ по заказ-наряду
				ArrayList works = new ArrayList();
				DbSqlCardWork.SelectInArray(card_data, works);

				// Обрабатываем список работ
				float summ	= 0.0F;
				CalculatorCard calc = new CalculatorCard(CALCULATOR_TYPE.CALCULATOR_PAY, VAT_TYPE.VAT_NON, 0);
				foreach (DtCardWork o1 in works)
				{
					DtCardWork work = o1;
					CalculatorResult res = calc.WorkCalculator.Calculate(o1);
					summ += res.SummDatabase;//summ	+= work.WorkSummCash;
				}

				// Выставление данных по заказ-наряду в Excel
				row_txt = row.ToString();
				// Дата оплаты = Дата закрытия
				cell_txt = "A" + row_txt;
				txt = card_data.GetData("ДАТА_НАРЯД_ЗАКРЫТ_КАРТОЧКА").ToString();
				CellText(ws, cell_txt, txt);
				// Сумма оплаты
				cell_txt = "B" + row_txt;
				txt = summ.ToString();
				CellText(ws, cell_txt, txt);
				// Вид расчета (1 если безналичный)
				cell_txt = "C" + row_txt;
				txt = "";
				if ((bool)card_data.GetData("БЕЗНАЛИЧНЫЙ_КАРТОЧКА"))
					txt = "БН";
				CellText(ws, cell_txt, txt);
				// Номер заказ-наряда
				cell_txt = "D" + row_txt;
				txt = card_data.GetData("НОМЕР_НАРЯД_КАРТОЧКА").ToString();
				CellText(ws, cell_txt, txt);
				// Модель автомобиля 
				cell_txt = "E" + row_txt;
				txt = auto_data.GetData("МОДЕЛЬ").ToString();
				CellText(ws, cell_txt, txt);
				// Модель автомобиля 
				cell_txt = "F" + row_txt;
				txt = auto_data.GetData("НОМЕР_КУЗОВ").ToString();
				CellText(ws, cell_txt, txt);

				row++;
				foreach(DtCardWork o1 in works)
				{
					DtCardWork work = o1;
					DtTxtCardWork txtCardWork = new DtTxtCardWork(o1);
					CalculatorResult res = calc.WorkCalculator.Calculate(work);
					row_txt = row.ToString();
					// Наименование работы
					cell_txt = "G" + row_txt;
					txt = txtCardWork.WorkName;//txt = work.GetData("НАИМЕНОВАНИЕ_КАРТОЧКА_РАБОТА").ToString();
					CellText(ws, cell_txt, txt);
					// Количество
					cell_txt = "H" + row_txt;
					txt = txtCardWork.OperationAmount;// txt = work.GetData("КОЛИЧЕСТВО_КАРТОЧКА_РАБОТА").ToString();
					CellText(ws, cell_txt, txt);
					// Стоимость
					cell_txt = "I" + row_txt;
					txt = res.SummDatabase.ToString();//txt = work.WorkSummCash.ToString();
					CellText(ws, cell_txt, txt);

					row++;
				}
			}
		}

		override protected void TitleFormatMult(Excel.Worksheet ws, int sheet)
		{
				TitleFormat1(ws);
		}
		override protected void DataToExcelMult(Excel.Worksheet ws, int sheet, int start)
		{
			if(sheet == 1)
			{
				DataToExcel1(ws, start);
				return;
			}
			if(sheet == 2)
			{
				DataToExcel2(ws, start);
				return;
			}
		}
	}
}
