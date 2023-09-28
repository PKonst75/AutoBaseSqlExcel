using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbPrintProduction.
	/// </summary>
	public class DbPrintProduction:DbPrint
	{
		struct Analiz
		{
			public float	cash_to;			// Деньги за ТО
			public float	to_count;			// Количество ТО
			public float	cash;				// Деньги за договорные работы
			public float	discount_to;		// Скидка по ТО
			public float	discount_cash;		// Скидка по договорным работам
			public float	wash;				// Выручка за мойки
			public float	discount_wash;		// Скидка за мойки
			public int		wash_count;			// Количество моек
			public float	cash_hour;			// Деньги за нормочасные работы
			public float	discount_cash_hour;	// Скидка за нормочасные работы
			public float	hour;				// Отработанные нормачасы
			public int		ppp_count;			// Количество предпродажек
			public float	guaranty_cash_to;	// На всякий случай
			public float	guaranty_cash;		// Договорные работы по гарантии
			public float	guaranty_cash_hour;	// Деньги за нормочасные работы по гарантии
			public float	guaranty_hour;		// Сколько нормочасов потрачено на гарантию
			public float	guaranty_wash_count;// Мойки по гарантии
			public int		guaranty_ppp_count;	// Количество предпродажек по гарантии
			public float[]	guaranty_cash_tos;		// Для разбивки по видам гарантии
			public float[]	guaranty_hours;			// Для разбивки по видам гарантии
			public float[]	guaranty_cashs;			// Для разбивки по видам гарантии
			public float[]	guaranty_cash_hours;	// Для разбивки по видам гарантии
		}
		Analiz				analiz_cash;
		Analiz				analiz_cashless;
		Analiz				analiz_inner;
		Analiz				analiz_ppp;
		
		SolidBrush	draw_brush;
		Font		font_print;
		Font		font_large_bold;

		// Идинтификационные данные
		string		title_period;
		// Расчетные данные
		

		public DbPrintProduction(DateTime start_date, DateTime end_date, long workshop)
		{
			// Инициализация массивов
			analiz_cash.guaranty_cash_tos			= new float[100];
			analiz_cash.guaranty_cashs				= new float[100];
			analiz_cash.guaranty_hours				= new float[100];
			analiz_cash.guaranty_cash_hours			= new float[100];
			analiz_cashless.guaranty_cash_tos		= new float[100];
			analiz_cashless.guaranty_cashs			= new float[100];
			analiz_cashless.guaranty_hours			= new float[100];
			analiz_cashless.guaranty_cash_hours		= new float[100];
			analiz_inner.guaranty_cash_tos			= new float[100];
			analiz_inner.guaranty_cashs				= new float[100];
			analiz_inner.guaranty_hours				= new float[100];
			analiz_inner.guaranty_cash_hours		= new float[100];
			analiz_ppp.guaranty_cash_tos			= new float[100];
			analiz_ppp.guaranty_cashs				= new float[100];
			analiz_ppp.guaranty_hours				= new float[100];
			analiz_ppp.guaranty_cash_hours			= new float[100];
			
			// Инструменты для печати
			draw_brush		= new SolidBrush(Color.Black);
			font_print		= new Font("Arial", 10);
			font_large_bold	= new Font("Arial", 14, FontStyle.Bold);

			// Идинтификационные данные
			title_period	= " с " + start_date.ToShortDateString() + " по " + end_date.ToShortDateString();

			// Получаем необходимые для расчета з/п данные
			ArrayList works = new ArrayList();
			DbSqlCardWork.SelectInArray(start_date, end_date, workshop, works);

			// Обработка данных по работам
			foreach(object o in works)
			{
				DtCardWork work = (DtCardWork)o;
				if((bool)work.GetData("БЕЗНАЛ") == true)
				{
					// Безнальный заказ-няряд
					analiz_cashless = AnalizeWork(analiz_cashless, work);
				}
				if((bool)work.GetData("ВНУТРЕННИЙ_КАРТОЧКА") == true)
				{
					// Внутренниый зака-наряд
					if((long)work.GetData("КАРТОЧКА_ССЫЛКА_КОД_КОНТРАГЕНТ") == 399 ||(long)work.GetData("КАРТОЧКА_ССЫЛКА_КОД_КОНТРАГЕНТ") == 1088)
					{
						// Предпродажная подготовка
						analiz_ppp = AnalizeWork(analiz_ppp, work);
					}
					else
					{
						// Обычный внутренний
						analiz_inner = AnalizeWork(analiz_inner, work);
					}
				}
				if((bool)work.GetData("ВНУТРЕННИЙ_КАРТОЧКА") == false && (bool)work.GetData("БЕЗНАЛ") == false)
				{
					// Обычный заказ-наряд за деньги
					analiz_cash = AnalizeWork(analiz_cash, work);
				}
			}
		}

		public override void  PrintPage(Graphics graph, int page)
		{
			float val_1;
			float val_2;
			int offset = 0;

			PrintText(graph, "ВЫРАБОТКА ТЕХЦЕНТРА" + title_period, 10, 10, 190, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_large_bold, draw_brush, false);	

			// В виде таблицы
			offset = 20;
			// Таблица
			PrintText(graph, "ТО", 10, 10 + offset, 100, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, "Ремонты (дог. цена)", 10, 15 + offset, 100, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, "Ремонты (н/ч)", 10, 20 + offset, 100, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, "Итого", 10, 25 + offset, 100, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, "Наличный расчет", 50, 5 + offset, 50, 5, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, "Безналичный расчет", 100, 5 + offset, 50, 5, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, "Итого", 150, 5 + offset, 50, 5, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			// Данные
			PrintText(graph, (analiz_cash.cash_to - analiz_cash.discount_to).ToString() + " / " + analiz_cash.discount_to.ToString() + " / " + analiz_cash.to_count, 50, 10 + offset, 50, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, (analiz_cash.cash - analiz_cash.discount_cash).ToString() + " / " + analiz_cash.discount_cash.ToString(), 50, 15 + offset, 50, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, (analiz_cash.cash_hour - analiz_cash.discount_cash_hour).ToString() + " / " + analiz_cash.discount_cash_hour.ToString() +  " / " + analiz_cash.hour, 50, 20 + offset, 60, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, (analiz_cash.cash_to - analiz_cash.discount_to + analiz_cash.cash - analiz_cash.discount_cash + analiz_cash.cash_hour - analiz_cash.discount_cash_hour).ToString() + " / " + (analiz_cash.discount_to + analiz_cash.discount_cash + analiz_cash.discount_cash_hour).ToString(), 50, 25 + offset, 50, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);

			PrintText(graph, (analiz_cashless.cash_to - analiz_cashless.discount_to).ToString() + " / " + analiz_cashless.discount_to.ToString() + " / " + analiz_cashless.to_count, 100, 10 + offset, 50, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, (analiz_cashless.cash - analiz_cashless.discount_cash).ToString() + " / " + analiz_cashless.discount_cash.ToString(), 100, 15 + offset, 50, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, (analiz_cashless.cash_hour - analiz_cashless.discount_cash_hour).ToString() + " / " + analiz_cashless.discount_cash_hour.ToString() +  " / " + analiz_cashless.hour, 100, 20 + offset, 50, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, (analiz_cashless.cash_to - analiz_cashless.discount_to + analiz_cashless.cash - analiz_cashless.discount_cash + analiz_cashless.cash_hour - analiz_cashless.discount_cash_hour).ToString() + " / " + (analiz_cashless.discount_to + analiz_cashless.discount_cash + analiz_cashless.discount_cash_hour).ToString(), 100, 25 + offset, 50, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);

			PrintText(graph, (analiz_cash.cash_to - analiz_cash.discount_to + analiz_cashless.cash_to - analiz_cashless.discount_to).ToString() + " / " + (analiz_cash.discount_to + analiz_cashless.discount_to).ToString() + " / " + (analiz_cash.to_count + analiz_cashless.to_count).ToString(), 150, 10 + offset, 50, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, (analiz_cash.cash - analiz_cash.discount_cash + analiz_cashless.cash - analiz_cashless.discount_cash).ToString() + " / " + (analiz_cash.discount_cash + analiz_cashless.discount_cash).ToString(), 150, 15 + offset, 50, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, (analiz_cash.cash_hour - analiz_cash.discount_cash_hour + analiz_cashless.cash_hour - analiz_cashless.discount_cash_hour).ToString() + " / " + (analiz_cash.discount_cash_hour + analiz_cashless.discount_cash_hour).ToString() +  " / " + (analiz_cash.hour + analiz_cashless.hour).ToString(), 150, 20 + offset, 60, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, (analiz_cash.cash_to - analiz_cash.discount_to + analiz_cash.cash - analiz_cash.discount_cash + analiz_cash.cash_hour - analiz_cash.discount_cash_hour + analiz_cashless.cash_to - analiz_cashless.discount_to + analiz_cashless.cash - analiz_cashless.discount_cash + analiz_cashless.cash_hour - analiz_cashless.discount_cash_hour).ToString() + " / " + (analiz_cash.discount_to + analiz_cash.discount_cash + analiz_cash.discount_cash_hour + analiz_cashless.discount_to + analiz_cashless.discount_cash + analiz_cashless.discount_cash_hour).ToString(), 150, 25 + offset, 50, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_large_bold, draw_brush, false);

			// В виде таблицы
			offset = 50;
			// Таблица
			PrintText(graph, "ТО", 10, 10 + offset, 100, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, "Ремонты (дог. цена)", 10, 15 + offset, 100, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, "Ремонты (н/ч)", 10, 20 + offset, 100, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, "Итого", 10, 25 + offset, 100, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, "Без оплаты", 50, 5 + offset, 50, 5, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, "Предпродажка", 100, 5 + offset, 50, 5, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);

			// Данные
			PrintText(graph, (analiz_inner.cash_to - analiz_inner.discount_to).ToString() + " / " + analiz_inner.discount_to.ToString() + " / " + analiz_inner.to_count, 50, 10 + offset, 50, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, (analiz_inner.cash - analiz_inner.discount_cash).ToString() + " / " + analiz_inner.discount_cash.ToString(), 50, 15 + offset, 50, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, (analiz_inner.cash_hour - analiz_inner.discount_cash_hour).ToString() + " / " + analiz_inner.discount_cash_hour.ToString() +  " / " + analiz_inner.hour, 50, 20 + offset, 50, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, (analiz_inner.cash_to - analiz_inner.discount_to + analiz_inner.cash - analiz_inner.discount_cash + analiz_inner.cash_hour - analiz_inner.discount_cash_hour).ToString() + " / " + (analiz_inner.discount_to + analiz_inner.discount_cash + analiz_inner.discount_cash_hour).ToString(), 50, 25 + offset, 50, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);

			PrintText(graph, (analiz_ppp.cash_to - analiz_ppp.discount_to).ToString() + " / " + analiz_ppp.discount_to.ToString() + " / " + analiz_ppp.to_count, 100, 10 + offset, 50, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, (analiz_ppp.cash - analiz_ppp.discount_cash).ToString() + " / " + analiz_ppp.discount_cash.ToString(), 100, 15 + offset, 50, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, (analiz_ppp.cash_hour - analiz_ppp.discount_cash_hour).ToString() + " / " + analiz_ppp.discount_cash_hour.ToString() +  " / " + analiz_ppp.hour, 100, 20 + offset, 50, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, (analiz_ppp.cash_to - analiz_ppp.discount_to + analiz_ppp.cash - analiz_ppp.discount_cash + analiz_ppp.cash_hour - analiz_ppp.discount_cash_hour).ToString() + " / " + (analiz_ppp.discount_to + analiz_ppp.discount_cash + analiz_ppp.discount_cash_hour).ToString(), 100, 25 + offset, 50, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);

			PrintText(graph, "ППП наличка " + analiz_cash.ppp_count.ToString() + " / " + analiz_cash.guaranty_ppp_count.ToString(), 150, 10 + offset, 50, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, "ППП безнал " + analiz_cashless.ppp_count.ToString() + " / " + analiz_cashless.guaranty_ppp_count.ToString(), 150, 15 + offset, 50, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, "ППП внутренний " + analiz_inner.ppp_count.ToString() + " / " + analiz_inner.guaranty_ppp_count.ToString(), 150, 20 + offset, 60, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, "ППП " + analiz_ppp.ppp_count.ToString() + " / " + analiz_ppp.guaranty_ppp_count.ToString(), 150, 25 + offset, 50, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);

			// Гарантия
			offset = 90;
			PrintText(graph, "ГАРАНТИЯ", 10, 0 + offset, 100, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_large_bold, draw_brush, false);
			// Таблица
			PrintText(graph, "ТО", 10, 10 + offset, 100, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, "Ремонты (дог. цена)", 10, 15 + offset, 100, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, "Ремонты (н/ч)", 10, 20 + offset, 100, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, "Итого", 10, 25 + offset, 100, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, "Наличный расчет", 50, 5 + offset, 50, 5, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, "Безналичный", 90, 5 + offset, 50, 5, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, "Внутренние", 130, 5 + offset, 50, 5, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, "ППП", 170, 5 + offset, 50, 5, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			// Данные
			PrintText(graph, analiz_cash.guaranty_cash_to.ToString(), 50, 10 + offset, 50, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, analiz_cash.guaranty_cash.ToString(), 50, 15 + offset, 50, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, analiz_cash.guaranty_cash_hour.ToString() + " / " + analiz_cash.guaranty_hour.ToString(), 50, 20 + offset, 60, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);

			PrintText(graph, analiz_cashless.guaranty_cash_to.ToString(), 90, 10 + offset, 50, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, analiz_cashless.guaranty_cash.ToString(), 90, 15 + offset, 50, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, analiz_cashless.guaranty_cash_hour.ToString() + " / " + analiz_cashless.guaranty_hour.ToString(), 90, 20 + offset, 60, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);

			PrintText(graph, analiz_inner.guaranty_cash_to.ToString(), 130, 10 + offset, 50, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, analiz_inner.guaranty_cash.ToString(), 130, 15 + offset, 50, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, analiz_inner.guaranty_cash_hour.ToString() + " / " + analiz_inner.guaranty_hour.ToString(), 130, 20 + offset, 60, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);

			PrintText(graph, analiz_ppp.guaranty_cash_to.ToString(), 170, 10 + offset, 50, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, analiz_ppp.guaranty_cash.ToString(), 170, 15 + offset, 50, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, analiz_ppp.guaranty_cash_hour.ToString() + " / " + analiz_ppp.guaranty_hour.ToString(), 170, 20 + offset, 60, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);

			// Разбивка гарантии по видам.
			offset = 120;
			for(int i = 0; i<100; i++)
			{
				float guaranty_cash_to		= analiz_cash.guaranty_cash_tos[i] + analiz_cashless.guaranty_cash_tos[i] + analiz_inner.guaranty_cash_tos[i] + analiz_ppp.guaranty_cash_tos[i];
				float guaranty_cash			= analiz_cash.guaranty_cashs[i] + analiz_cashless.guaranty_cashs[i] + analiz_inner.guaranty_cashs[i] + analiz_ppp.guaranty_cashs[i];
				float guaranty_cash_hour	= analiz_cash.guaranty_cash_hours[i] + analiz_cashless.guaranty_cash_hours[i] + analiz_inner.guaranty_cash_hours[i] + analiz_ppp.guaranty_cash_hours[i];
				float guaranty_hour			= analiz_cash.guaranty_hours[i] + analiz_cashless.guaranty_hours[i] + analiz_inner.guaranty_hours[i] + analiz_ppp.guaranty_hours[i];
				if(guaranty_cash_to + guaranty_cash + guaranty_cash_hour == 0.0F && guaranty_hour == 0.0F){}
				else
				{
					DtGuarantyType gt = DbSqlGuarantyType.Find((long)i);
					string txt = "";
					if(gt != null)
						txt = (string)gt.GetData("ОПИСАНИЕ_ГАРАНТИЯ");
					if(txt.Length == 0) txt = "Неизвестный = " + i.ToString();
					PrintText(graph, txt, 10, 10 + offset, 100, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
					PrintText(graph, (guaranty_cash_to + guaranty_cash +  guaranty_cash_hour).ToString() + " / " + guaranty_hour.ToString(), 80, 10 + offset, 100, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
					offset += 5;
				}
			}

			// Гарантия
			offset += 15;
			PrintText(graph, "МОЙКА", 10, 0 + offset, 100, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_large_bold, draw_brush, false);

			// Таблица
			PrintText(graph, "Заработок", 10, 10 + offset, 100, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, "Количество моек", 10, 15 + offset, 100, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			// Подсчет
			float	wash_cash	= analiz_cash.wash + analiz_cashless.wash - analiz_cash.discount_wash - analiz_cashless.discount_wash;
			int		wash_count	= (int)(float)(analiz_cash.wash_count + analiz_cashless.wash_count + analiz_inner.wash_count + analiz_ppp.wash_count - analiz_cash.guaranty_wash_count - analiz_cashless.guaranty_wash_count - analiz_inner.guaranty_wash_count - analiz_ppp.guaranty_wash_count);
			PrintText(graph, wash_cash.ToString(), 50, 10 + offset, 80, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, wash_count.ToString(), 50, 15 + offset, 80, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
		}

		private float Summ(DtCardWork wrk)
		{
			if((float)wrk.GetData("ТРУДОЕМКОСТЬ_КАРТОЧКА_РАБОТА") == 0.0F)
				return (float)wrk.GetData("НОРМАЧАС_КАРТОЧКА_РАБОТА")*(float)wrk.GetData("КОЛИЧЕСТВО_КАРТОЧКА_РАБОТА");
			else
				return (float)wrk.GetData("ТРУДОЕМКОСТЬ_КАРТОЧКА_РАБОТА")*(float)wrk.GetData("НОРМАЧАС_КАРТОЧКА_РАБОТА")*(float)wrk.GetData("КОЛИЧЕСТВО_КАРТОЧКА_РАБОТА");
		}
		private float SummDiscount(DtCardWork wrk)
		{
			if((float)wrk.GetData("ТРУДОЕМКОСТЬ_КАРТОЧКА_РАБОТА") == 0.0F)
				return (float)wrk.GetData("НОРМАЧАС_КАРТОЧКА_РАБОТА")*(float)wrk.GetData("КОЛИЧЕСТВО_КАРТОЧКА_РАБОТА") / 100.0F * (float)wrk.GetData("СКИДКА_КАРТОЧКА_РАБОТА");
			else
				return (float)wrk.GetData("ТРУДОЕМКОСТЬ_КАРТОЧКА_РАБОТА")*(float)wrk.GetData("НОРМАЧАС_КАРТОЧКА_РАБОТА")*(float)wrk.GetData("КОЛИЧЕСТВО_КАРТОЧКА_РАБОТА") / 100.0F * (float)wrk.GetData("СКИДКА_КАРТОЧКА_РАБОТА");
		}
		private float Hour(DtCardWork wrk)
		{
			if((float)wrk.GetData("ТРУДОЕМКОСТЬ_КАРТОЧКА_РАБОТА") == 0.0F)
				return (float)0.0F;
			else
				return (float)wrk.GetData("ТРУДОЕМКОСТЬ_КАРТОЧКА_РАБОТА")*(float)wrk.GetData("КОЛИЧЕСТВО_КАРТОЧКА_РАБОТА");
		}

		private Analiz AnalizeWork(Analiz analiz, DtCardWork wrk)
		{
			// Проверка на предпродажку
			if((long)wrk.GetData("ССЫЛКА_КОД_СПРАВОЧНИК_ТРУДОЕМКОСТЬ")== 188)
			{
				if((bool)wrk.GetData("ГАРАНТИЯ_КАРТОЧКА_РАБОТА") == false)
					analiz.ppp_count++;
				else
				{
					analiz.guaranty_ppp_count++;
				}
				return analiz;
			}
			// Проверка на мойку
			if((long)wrk.GetData("ССЫЛКА_КОД_СПРАВОЧНИК_ТРУДОЕМКОСТЬ")== 722)
			{
				if((bool)wrk.GetData("ГАРАНТИЯ_КАРТОЧКА_РАБОТА") == false)
				{
					analiz.wash_count++;
					analiz.wash	+= Summ(wrk);
					analiz.discount_wash += SummDiscount(wrk);
				}
				else
					analiz.guaranty_wash_count++;
				return analiz;
			}
			// Проверка на ТО
			if((long)wrk.GetData("СПЕЦИАЛЬНЫЙ_ТИП") == 1)
			{
				analiz.to_count ++;
				if((bool)wrk.GetData("ГАРАНТИЯ_КАРТОЧКА_РАБОТА") == false)
				{
					analiz.cash_to	+= Summ(wrk);
					analiz.discount_to += SummDiscount(wrk);
				}
				else
				{
					analiz.guaranty_cash_to	+= Summ(wrk);
					analiz.guaranty_cash_tos[(int)(long)wrk.GetData("ГАРАНТИЯ_ВИД_КАРТОЧКА_РАБОТА")] += Summ(wrk);
				}
				return analiz;
			}


			if((bool)wrk.GetData("ГАРАНТИЯ_КАРТОЧКА_РАБОТА") == false)
			{		
				if((float)wrk.GetData("ТРУДОЕМКОСТЬ_КАРТОЧКА_РАБОТА") == 0.0F)
				{
					analiz.cash				+= Summ(wrk);
					analiz.discount_cash	+= SummDiscount(wrk);
				}					
				else
				{
					analiz.hour					+= Hour(wrk);
					analiz.cash_hour			+= Summ(wrk);
					analiz.discount_cash_hour	+= SummDiscount(wrk);
				}
			}
			else
			{
				if((float)wrk.GetData("ТРУДОЕМКОСТЬ_КАРТОЧКА_РАБОТА") == 0.0F)
				{
					analiz.guaranty_cash	+= Summ(wrk);
					analiz.guaranty_cashs[(int)(long)wrk.GetData("ГАРАНТИЯ_ВИД_КАРТОЧКА_РАБОТА")] += Summ(wrk);
				}					
				else
				{
					analiz.guaranty_hour		+= Hour(wrk);
					analiz.guaranty_cash_hour	+= Summ(wrk);
					analiz.guaranty_hours[(int)(long)wrk.GetData("ГАРАНТИЯ_ВИД_КАРТОЧКА_РАБОТА")] += Hour(wrk);
					analiz.guaranty_cash_hours[(int)(long)wrk.GetData("ГАРАНТИЯ_ВИД_КАРТОЧКА_РАБОТА")] += Summ(wrk);
				}
			}
			return analiz;
		}
	}
}
