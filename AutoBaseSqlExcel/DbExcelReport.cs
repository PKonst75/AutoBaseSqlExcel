using System;
using System.Collections;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbExcelReport.
	/// </summary>
	public class DbExcelReport:DbExcel
	{
		ArrayList array = new ArrayList();
		ArrayList array_service_manager_1 = new ArrayList();
		ArrayList array_service_manager_2 = new ArrayList();
		ArrayList array_service_manager_3 = new ArrayList();
		DrReprot report;
		DrReprot report_service_manager_1;
		DrReprot report_service_manager_2;
		DrReprot report_service_manager_3;

		public DbExcelReport()
		{
			// Запрос даты начала отсчета и конца отсчета
			FormSelectDate dlg = new FormSelectDate();
			dlg.ShowDialog();
			if(dlg.DialogResult != DialogResult.OK) return;
			DateTime date_start = dlg.SelectedDate;

			dlg.ShowDialog();
			if(dlg.DialogResult != DialogResult.OK) return;
			DateTime date_end = dlg.SelectedDate;

			//Выбор желаемого подразделения
			FormWorkshopList form = new FormWorkshopList();
			if (form.ShowDialog() != DialogResult.OK) return;
			DbWorkshop workshop = form.SelectedWorkshop;
			if(workshop == null) return;
			long workshop_code = workshop.Code;

			// Новый способ получения списка карточке закрытых на указанную дату
			// Считаем, что работаем в одном году
			ArrayList	number_array	= new ArrayList();
			DbSqlCard.SelectCardClosedNumberWorkshop(number_array, date_start, date_end, workshop_code);	
			FormInfoTable info = new FormInfoTable("Начало отсчета");
			info.Show();
			foreach(DtCard element in number_array)
			{		
				//DbCard card = DbCard.Find((long)element.GetData("НОМЕР_КАРТОЧКА"), (int)element.GetData("ГОД_КАРТОЧКА"));
				DtCard card = DbSqlCard.Find((long)element.GetData("НОМЕР_КАРТОЧКА"), (int)element.GetData("ГОД_КАРТОЧКА"));
				if ((bool)card.GetData("ВНУТРЕННИЙ_КАРТОЧКА") == false)
				{
					array.Add(card);
					if(card.ServiceManagerCode == 66)
						array_service_manager_1.Add(card);
					if(card.ServiceManagerCode == 149)
						array_service_manager_2.Add(card);
					if(card.ServiceManagerCode == 157)
						array_service_manager_3.Add(card);
				}
				else
				{
					// Анализ внутренних карточек
				}
				info.SetText(card.GetData("НОМЕР_НАРЯД_КАРТОЧКА").ToString());
			}
			info.SetText("Конец отсчета");
			info.Close();

			// Получен список заказ-нарядов, закрытых в указанный период времени
			report = new DrReprot();
			report.FillReport(array);

			report_service_manager_1 = new DrReprot();
			report_service_manager_1.FillReport(array_service_manager_1);
			report_service_manager_2 = new DrReprot();
			report_service_manager_2.FillReport(array_service_manager_2);
			report_service_manager_3 = new DrReprot();
			report_service_manager_3.FillReport(array_service_manager_3);
		
		}

		protected void TitleFormatSheet1(Excel.Worksheet ws)
		{
			// Дата закрытия заказ/наряда
			FormatColumn(ws, "A1", 27, 8, "Left");
			CellText(ws, "A1", "Закрытие", "Center", 10, true);
			// Номер заказ/наряда
			FormatColumn(ws, "B1", 12, 8, "Right", true);
			CellText(ws, "B1", "Номер ЗН", "Center", 10, true);
			// Дата заказ/наряда
			FormatColumn(ws, "C1", 12, 8, "Right");
			CellText(ws, "C1", "Дата ЗН", "Center", 10, true);
			// Данные о мастере-консультанте
			FormatColumn(ws, "D1", 12, 8, "Right");
			CellText(ws, "D1", "Мастер-консультант", "Center", 10, true);
		}
		protected void TitleFormatSheet2(Excel.Worksheet ws)
		{
			// Наименование показателя
			FormatColumn(ws, "A1", 27, 8, "Left");
			CellText(ws, "A1", "Показатель", "Center", 10, true);
			// Значение показателя
			FormatColumn(ws, "B1", 12, 8, "Right", true);
			CellText(ws, "B1", "Сумма", "Center", 10, true);
			// Себистоимость показателя
			FormatColumn(ws, "C1", 12, 8, "Right", true);
			CellText(ws, "C1", "Себистоимость", "Center", 10, true);
			// Прибыль показателя
			FormatColumn(ws, "D1", 12, 8, "Right", true);
			CellText(ws, "D1", "Прибыль", "Center", 10, true);
		}

		protected void DataToExcelSheet1(Excel.Worksheet ws, int start)
		{
			string row_last = "1";
			string row_txt;
			string cell_txt;
			int row = start;
			int row_summ = 2;
			string txt;

			DtStaff master = null;

			foreach(object o in array)
			{
				// Картички
				DtCard card = (DtCard)o;

				// Загружаем дополнительные данные
				master = DbSqlStaff.Find((long)card.GetData("СЕРВИС_КОНСУЛЬТАНТ"));
				
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
				if (master != null)
				{
					cell_txt = "D" + row_txt;
					txt = (string)master.Title;
					CellText(ws, cell_txt, txt);
				}

				row++;	// переход на следующую стоку
			}
		}

		protected void DataToExcelReport(Excel.Worksheet ws, int start, DrReprot report_data)
		{
			string row_last = "1";
			string row_txt;
			string cell_txt;
			int row = start;
			string txt;

			// Дополнительные данные
			// Количество заказ/нарядов
			row_txt = row.ToString();
			cell_txt = "A" + row_txt;
			txt = "Количество ЗН";
			CellText(ws, cell_txt, txt);
			cell_txt = "B" + row_txt;
			txt = report_data.report.card_count.ToString();
			CellText(ws, cell_txt, txt);

			// Запчасти
			row++;
			row_txt = row.ToString();
			// Сумма запчастей по заказ-нарядам и вход
			cell_txt = "A" + row_txt;
			txt = "ЗАПЧАСТИ";
			CellText(ws, cell_txt, txt);
			cell_txt = "B" + row_txt;
			txt = report_data.report.spare_parts.ToString();
			CellText(ws, cell_txt, txt);
			cell_txt = "C" + row_txt;
			txt = report_data.report.spare_parts_input.ToString();
			CellText(ws, cell_txt, txt);
			cell_txt = "D" + row_txt;
			txt = "=B" + row_txt + "-C" + row_txt;
			CellText(ws, cell_txt, txt);

			row++;
			row_txt = row.ToString();
			// Сумма масел по заказ-нарядам и вход
			cell_txt = "A" + row_txt;
			txt = "МАСЛА";
			CellText(ws, cell_txt, txt);
			cell_txt = "B" + row_txt;
			txt = report_data.report.spare_oil.ToString();
			CellText(ws, cell_txt, txt);
			cell_txt = "C" + row_txt;
			txt = report_data.report.spare_oil_input.ToString();
			CellText(ws, cell_txt, txt);
			cell_txt = "D" + row_txt;
			txt = "=B" + row_txt + "-C" + row_txt;
			CellText(ws, cell_txt, txt);

			row++;
			row_txt = row.ToString();
			// Сумма ТО по заказ-нарядам
			cell_txt = "A" + row_txt;
			txt = "ТО";
			CellText(ws, cell_txt, txt);
			cell_txt = "B" + row_txt;
			txt = report_data.report.works_to.ToString();
			CellText(ws, cell_txt, txt);

			row++;
			row_txt = row.ToString();
			// Сумма работ по заказ-нарядам
			cell_txt = "A" + row_txt;
			txt = "Работы";
			CellText(ws, cell_txt, txt);
			cell_txt = "B" + row_txt;
			txt = report_data.report.works_labor.ToString();
			CellText(ws, cell_txt, txt);

			row++;
			row_txt = row.ToString();
			// Сумма моек по заказ-нарядам
			cell_txt = "A" + row_txt;
			txt = "Мойка";
			CellText(ws, cell_txt, txt);
			cell_txt = "B" + row_txt;
			txt = report_data.report.works_wash.ToString();
			CellText(ws, cell_txt, txt);

			row++;
			row_txt = row.ToString();
			// Нормочасы закрыте по негарантии
			cell_txt = "A" + row_txt;
			txt = "Н/Ч деньги";
			CellText(ws, cell_txt, txt);
			cell_txt = "B" + row_txt;
			txt = report_data.report.work_labor_time.ToString();
			CellText(ws, cell_txt, txt);

			for(int i = 0; i <100; i++)
			{
				if((report_data.report.works_gar_time[i] > 0) || (report_data.report.works_gar_cost[i] > 0))
				{
					DtGuarantyType gt = DbSqlGuarantyType.Find(i);
					string gname = i.ToString();
					if(gt != null)
					{
						gname = gt.GetData("ОПИСАНИЕ_ГАРАНТИЯ").ToString();
					}
					row++;
					row_txt = row.ToString();
					// Гарантии
					cell_txt = "A" + row_txt;
					txt = "Гарантия " + gname;
					CellText(ws, cell_txt, txt);
					cell_txt = "B" + row_txt;
					txt = report_data.report.works_gar_time[i].ToString();
					CellText(ws, cell_txt, txt);
					cell_txt = "C" + row_txt;
					txt = report_data.report.works_gar_cost[i].ToString();
					CellText(ws, cell_txt, txt);
				}
			}
		}

		protected void DataToExcelSheet2(Excel.Worksheet ws, int start)
		{
			string row_last = "1";
			string row_txt;
			string cell_txt;
			int row = start;
			string txt;

			// Дополнительные данные
			// Количество заказ/нарядов
			row_txt = row.ToString();
			cell_txt = "A" + row_txt;
			txt = "Количество ЗН";
			CellText(ws, cell_txt, txt);
			cell_txt = "B" + row_txt;
			txt = report.report.card_count.ToString();
			CellText(ws, cell_txt, txt);

			// Запчасти
			row++;
			row_txt = row.ToString();
			// Сумма запчастей по заказ-нарядам и вход
			cell_txt = "A" + row_txt;
			txt = "ЗАПЧАСТИ";
			CellText(ws, cell_txt, txt);
			cell_txt = "B" + row_txt;
			txt = report.report.spare_parts.ToString();
			CellText(ws, cell_txt, txt);
			cell_txt = "C" + row_txt;
			txt = report.report.spare_parts_input.ToString();
			CellText(ws, cell_txt, txt);
			cell_txt = "D" + row_txt;
			txt = "=B" + row_txt + "-C" + row_txt;
			CellText(ws, cell_txt, txt);

			row++;
			row_txt = row.ToString();
			// Сумма масел по заказ-нарядам и вход
			cell_txt = "A" + row_txt;
			txt = "МАСЛА";
			CellText(ws, cell_txt, txt);
			cell_txt = "B" + row_txt;
			txt = report.report.spare_oil.ToString();
			CellText(ws, cell_txt, txt);
			cell_txt = "C" + row_txt;
			txt = report.report.spare_oil_input.ToString();
			CellText(ws, cell_txt, txt);
			cell_txt = "D" + row_txt;
			txt = "=B" + row_txt + "-C" + row_txt;
			CellText(ws, cell_txt, txt);

			row++;
			row_txt = row.ToString();
			// Сумма ТО по заказ-нарядам
			cell_txt = "A" + row_txt;
			txt = "ТО";
			CellText(ws, cell_txt, txt);
			cell_txt = "B" + row_txt;
			txt = report.report.works_to.ToString();
			CellText(ws, cell_txt, txt);

			row++;
			row_txt = row.ToString();
			// Сумма работ по заказ-нарядам
			cell_txt = "A" + row_txt;
			txt = "Работы";
			CellText(ws, cell_txt, txt);
			cell_txt = "B" + row_txt;
			txt = report.report.works_labor.ToString();
			CellText(ws, cell_txt, txt);

			row++;
			row_txt = row.ToString();
			// Сумма моек по заказ-нарядам
			cell_txt = "A" + row_txt;
			txt = "Мойка";
			CellText(ws, cell_txt, txt);
			cell_txt = "B" + row_txt;
			txt = report.report.works_wash.ToString();
			CellText(ws, cell_txt, txt);

			row++;
			row_txt = row.ToString();
			// Нормочасы закрыте по негарантии
			cell_txt = "A" + row_txt;
			txt = "Н/Ч деньги";
			CellText(ws, cell_txt, txt);
			cell_txt = "B" + row_txt;
			txt = report.report.work_labor_time.ToString();
			CellText(ws, cell_txt, txt);


			for(int i = 0; i <100; i++)
			{
				if((report.report.works_gar_time[i] > 0) || (report.report.works_gar_cost[i] > 0))
				{
					DtGuarantyType gt = DbSqlGuarantyType.Find(i);
					string gname = i.ToString();
					if(gt != null)
					{
						gname = gt.GetData("ОПИСАНИЕ_ГАРАНТИЯ").ToString();
					}
					row++;
					row_txt = row.ToString();
					// Гарантии
					cell_txt = "A" + row_txt;
					txt = "Гарантия " + gname;
					CellText(ws, cell_txt, txt);
					cell_txt = "B" + row_txt;
					txt = report.report.works_gar_time[i].ToString();
					CellText(ws, cell_txt, txt);
					cell_txt = "C" + row_txt;
					txt = report.report.works_gar_cost[i].ToString();
					CellText(ws, cell_txt, txt);
				}
			}
		}

		override protected void DataToExcelMult(Excel.Worksheet ws, int sheet, int start)
		{
			if(sheet == 1)
			{
				DataToExcelSheet1(ws, start);
				return;
			}
			if(sheet == 2)
			{
				DataToExcelReport(ws, start, report);
				return;
			}
			if(sheet == 3)
			{
				DataToExcelReport(ws, start, report_service_manager_1);
				return;
			}
			if(sheet == 4)
			{
				DataToExcelReport(ws, start, report_service_manager_2);
				return;
			}
			if(sheet == 5)
			{
				DataToExcelReport(ws, start, report_service_manager_3);
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
				TitleFormatSheet2(ws);
				return;
			}
			if(sheet == 4)
			{
				TitleFormatSheet2(ws);
				return;
			}
			if(sheet == 5)
			{
				TitleFormatSheet2(ws);
				return;
			}
		}
	}
}
