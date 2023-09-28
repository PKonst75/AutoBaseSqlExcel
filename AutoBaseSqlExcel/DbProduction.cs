using System;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbProduction.
	/// </summary>
	public class DbProduction
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
			public int		ppp_cash;			// Деньги за предпродажку
			public float	guaranty_cash_to;	// На всякий случай
			public float	guaranty_cash;		// Договорные работы по гарантии
			public float	guaranty_cash_hour;	// Деньги за нормочасные работы по гарантии
			public float	guaranty_hour;		// Сколько нормочасов потрачено на гарантию
			public float	guaranty_wash_count;// Мойки по гарантии
			public int		guaranty_ppp_count;	// Количество предпродажек по гарантии
			public int		guaranty_ppp_cash;	// Деньги за предпродажку отмеченную по гарантии
			public float[]	guaranty_cash_tos;		// Для разбивки по видам гарантии
			public float[]	guaranty_hours;			// Для разбивки по видам гарантии
			public float[]	guaranty_cashs;			// Для разбивки по видам гарантии
			public float[]	guaranty_cash_hours;	// Для разбивки по видам гарантии
		}
		Analiz				analiz_cash;
		Analiz				analiz_cashless;
		Analiz				analiz_inner;
		Analiz				analiz_ppp;

		public DbProduction(DateTime start_date, DateTime end_date, long workshop)
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

		public float Cash
		{
			get
			{
				float cash = analiz_cash.cash_to + analiz_cash.cash + analiz_cash.cash_hour;
				cash = cash - analiz_cash.discount_to - analiz_cash.discount_cash - analiz_cash.discount_cash_hour;
				cash = cash + analiz_cashless.cash_to + analiz_cashless.cash + analiz_cashless.cash_hour;
				cash = cash - analiz_cashless.discount_to - analiz_cashless.discount_cash - analiz_cashless.discount_cash_hour;
				return cash;
			}
		}
	}
}
