using System;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DrReprot.
	/// </summary>
	public class DrReprot
	{
		public class Report
		{
			public int card_count;				// Счетчик карточек
			public float spare_parts;			// Сумма запчастей проданных по заказ-нарядам
			public float spare_parts_input;		// Сумма затраченная на покупку запчастей проданных по заказ-нарядам
			public float spare_oil;				// Сумма масел проданных по заказ-нарядам
			public float spare_oil_input;		// Сумма затраченная на покупку масел проданных по заказ-нарядам
			public float works_to;				// Сумма работ по Техническому осблуживанию
			public float works_labor;			// Сумма работ
			public float works_wash;			// Сумма моек

			public float work_labor_time;		// Нормочасы закрытые по работам
			public float work_to_time;			// Нормочасы закрытые ТО
			public float work_sp_time;			// Нормочасы закрыте по сервис пакетам (ВСЕГО)


			public float[] works_gar_cost;		// Сумма стоимости работ по гарантии
			public float[] works_gar_time;		// Время механиков, затраченное на работы по гарантии


			public Report()
			{
				works_gar_time = new float[100];
				works_gar_cost = new float[100];

				card_count			= 0;
				spare_parts			= 0.0F;
				spare_parts_input	= 0.0F;
				spare_oil			= 0.0F;
				spare_oil_input		= 0.0F;
				works_to			= 0.0F;
				works_labor			= 0.0F;
				works_wash			= 0.0F;

				work_labor_time		= 0.0F;
				work_to_time		= 0.0F;
				work_sp_time		= 0.0F;
			}
		}

		public Report report;

		public DrReprot()
		{
			report = new Report();
		}

		public bool FillReport(ArrayList array)
		{
			foreach (object o in array)
			{
				DtCard card = (DtCard)o;
				AddCard(card);
			}
			return true;
		}

		public void AddCard(DtCard card)
		{
			long card_number = (long)card.GetData("НОМЕР_КАРТОЧКА");
			int card_year = (int)card.GetData("ГОД_КАРТОЧКА");
			// Добавляем карточку к данным
			report.card_count++;		// Увеличиваем счетчик карточек
			// Загрузка проданных в карточке запасных частей
			ArrayList parts = new ArrayList();
			DbSqlCardDetail.SelectInArray(card, parts);
			AnalizeParts(parts);
			// Загрузка выполненных по карточке работ
			ArrayList works = new ArrayList();
			DbSqlCardWork.SelectInArray(card, works);
			AnalizeWorks(works);
		}

		public void AnalizeParts(ArrayList array)
		{
			foreach (object o in array)
			{
				AddDetail((DtCardDetail)o);
			}
		}
		public void AddDetail(DtCardDetail detail)
		{
			report.spare_parts			+= detail.DetailSummCash;
			report.spare_parts_input	+= detail.DetailSummCashInput;
			report.spare_oil			+= detail.DetailSummOilCash;
			report.spare_oil_input		+= detail.DetailSummOilCashInput;
		}
		public void AnalizeWorks(ArrayList array)
		{
			foreach (object o in array)
			{
				AddWork((DtCardWork)o);
			}
		}
		public void AddWork(DtCardWork work)
		{
			long type;
			report.works_labor				+= work.WorkLaborSummCash;
			report.works_to					+= work.WorkToSummCash;
			report.works_wash				+= work.WorkWashSummCash;

			if((bool)work.GetData("ГАРАНТИЯ_КАРТОЧКА_РАБОТА")!=true)
			{
				// Суммируем только если не гарантия
				report.work_labor_time			+= work.WorkNV;
			}

			if((bool)work.GetData("ГАРАНТИЯ_КАРТОЧКА_РАБОТА")==true)
			{
				type = (long)work.GetData("ГАРАНТИЯ_ВИД_КАРТОЧКА_РАБОТА");
				report.works_gar_time[type] += work.WorkNV;
				report.works_gar_cost[type] += work.WorkSumm;
			}
		}
	}
}
