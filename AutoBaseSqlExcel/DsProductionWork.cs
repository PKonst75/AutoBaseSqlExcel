using System;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DsProductionWork.
	/// </summary>
	public class DsProductionWork
	{
		// Данные составляющие компоненты выручки сервиса
		public float cash_all;			// Сумма полученных по работе денег (все операции)
		public float cash_discount;		// Сумма сделанных скидок
		public float hour_all;			// Общее время потраченное на заказ наряд (все операции)

		// Деление на виды операций
		public float cash;				// Сумма за выполненную работу - по нормачасам
		public float cash_sp;			// Сумма за выполненную работу по сервис пакету (Не мойка, не ТО, не ППП)
		public float cash_ppp;			// Сумма за выполненную операцию - предпродажная подготовка
		public float cash_to;			// Сумма за выполненную операцию - ТО
		public float cash_wash;			// Сумма за оплаченные операции по мойке

		// Счетчики количества операций
		public int count_to;			// Общее количество произведенных ТО
		public int count_wash;			// Количество произведенных операций по мойке
		public int count_ppp;			// Количество произведенных предпродажных подготовок
		public int count_sp;			// Количество сервис пакетов - (Не мойка, не ТО, не ППП)

		// Счетчики норм времени
		public float hour;				// Количество часов, по выполненной работе
		public float hour_sp;			// Количество часов, по выполненной работе по сервис пакету
		public float hour_to;			// Количество часов, по выполненной работе по ТО
		public float hour_ppp;			// Количество часов, по выполненной работе по ТО

		// Вспомагательные счетчики
		public int count_work;			// Счетчик работ  не мойка, не по гарантии, не ТО, не ППП, не сервис пакет
		
		public int to11_count;			// Количество произведенных ТО 11-...
		public int to0_count;			// Количество произведенных ТО 0
		public int to1_count;			// Количество произведенных ТО 1
		public int to2_count;			// Количество произведенных ТО 2
		public int to3_count;			// Количество произведенных ТО 3
		public int to4_count;			// Количество произведенных ТО 4
		public int to5_count;			// Количество произведенных ТО 5
		public int to6_count;			// Количество произведенных ТО 6
		public int to7_count;			// Количество произведенных ТО 7
		public int to8_count;			// Количество произведенных ТО 8
		public int to9_count;			// Количество произведенных ТО 9
		public int to10_count;			// Количество произведенных ТО 10
		
		public int man_count;			// Количество исполнителей
		public float cash_byman;		// Сумма за выполненную работу - приведенная на количество исполнителей
		public float hour_byman;		// Количество часов, по выполненной работе  - приведенная на количество исполнителей
		public float cash_sp_byman;		// Сумма за выполненную работу по сервис пакету - приведенная на количество исполнителей
		public float hour_sp_byman;		// Количество часов, по выполненной работе по сервис пакету - приведенная на количество исполнителей
		public float count_sp_byman;	// Количество выполненных сервис пакетов - приведенная на количество исполнителей
		public float count_ppp_byman;	// Количество выполненных предпродажных подготовок - приведенная на количество исполнителей
		public float count_to_byman;	// Количество выполненных предпродажных подготовок - приведенная на количество исполнителей
		public bool is_guaranty;		// Флаг отмечающий гарантию
		public long type_guaranty;		// Вид гарантии, которому принадлежит работа
		public ArrayList man_array;		// Список исполнителей по работе

		public DsProductionWork()
		{
			SetInitial();
		}

		public DsProductionWork(DtCardWork card_work)
		{
			SetInitial();
			// Задача - проанализировать работу, выдернуть все данные
			float quontity	= (float)card_work.GetData("КОЛИЧЕСТВО_КАРТОЧКА_РАБОТА");	// Количество повторений работы
			float val		= (float)card_work.GetData("ТРУДОЕМКОСТЬ_КАРТОЧКА_РАБОТА");	// Количество повторений работы
			float price		= (float)card_work.GetData("НОРМАЧАС_КАРТОЧКА_РАБОТА");		// Количество стоимость работы/нормачаса

			// Получаем суммарные развесовки работ/часов
			float summ		= 0.0F;
			float time		= 0.0F;
			if (val == 0.0F)
			{
				summ		= (float)Math.Round(Math.Round(price, 2) * Math.Round(quontity, 2));
				time		= (float)Math.Round((Math.Round(GetSPHours(card_work), 2) * Math.Round(quontity, 2)), 2);
			}
			else
			{
				summ		= (float)Math.Round((Math.Round(val, 2) * Math.Round(price, 2) * Math.Round(quontity, 2)));
				time		= (float)Math.Round((Math.Round(val, 2) * Math.Round(quontity, 2)), 2);
			}
			
			// Рассматриваем гарантию
			is_guaranty		= (bool)card_work.GetData("ГАРАНТИЯ_КАРТОЧКА_РАБОТА");
			type_guaranty	= (long)card_work.GetData("ГАРАНТИЯ_ВИД_КАРТОЧКА_РАБОТА");

			// Получение количества и списка исполнителей
			man_count		= (int)GetManCount(card_work);		// Количество исполнителей работы

			// Определяем специальные типы
			bool is_wash	= IsWash(card_work);
			bool is_to		= IsTO(card_work);
			bool is_ppp		= IsPPP(card_work);

			// Общая сумма прошедшая по работе
			cash_all		= summ;

			// Если это мойка - выделяем в отделное рассмотрение
			if(is_wash == true)
			{
				count_wash	= 1;
				cash_wash	= summ;
				return;
			}
			hour_all		= time;		// Чтобы не учесть мойку

			// Если это предпродажка - выделяем в отделное рассмотрение
			if(is_ppp == true)
			{
				count_ppp	= 1;
				cash_ppp	= summ;
				hour_ppp	= time;
				return;
			}
			// Если это предпродажка - выделяем в отделное рассмотрение
			if(is_to == true)
			{
				count_to	= 1;
				cash_to		= summ;
				hour_to		= time;
				return;
			}

			// Дальнейший анализ
			if (val == 0.0F)
			{
				// Это сервис пакет
				hour_sp				= time;
				cash_sp				= summ;
				count_sp			= 1;
			}
			else
			{
				// Это работа по нормачасу
				cash		= summ;
				hour		= time;
			}
			if(is_guaranty == false) count_work		= 1;	// Отмечаем наличие платной работы

			// Делаем привидение на количество исполнителей
			cash_byman		= cash / (float)man_count;
			cash_sp_byman	= cash_sp / (float)man_count;
			hour_byman		= hour / (float)man_count;
			hour_sp_byman	= hour_sp / (float)man_count;
			count_sp_byman	= count_sp / (float)man_count;
		
			
			// Делаем привидение на количество исполнителей
			count_to_byman	= count_to / (float)man_count;
			count_ppp_byman	= count_ppp / (float)man_count;
		}

		public static double GetSPHours(DtCardWork element)
		{
			// Получение данных о НВ сервис пакета
			long code_work = (long)element.GetData("КОД_ТРУДОЕМКОСТЬ_КАРТОЧКА_РАБОТА");
			DtWork work = DbSqlWork.Find(code_work);

			// Для начала ищем чистое НВ
			double local_nv	= Math.Round((float)work.GetData("НВ"), 2);
			if(local_nv != 0.0F)
			{
				element.SetData("СЕРВИС_ПАКЕТ_НВ", (float)local_nv);
				return local_nv;
			}
				
			long code_collection = (long)work.GetData("ССЫЛКА_КОД_КОЛЛЕКЦИЯ");
			double local_sp = 0.0F;
			if(code_collection !=  0)
			{
				// Есть коллекция
				element.SetData("ССЫЛКА_КОД_КОЛЛЕКЦИЯ", code_collection);
							
				ArrayList array = new ArrayList();
				DbSqlWorkCollectionItem.SelectInArray(array, code_collection);
				foreach(DtWorkCollectionItem elm in array)
				{
					local_sp += Math.Round((float)elm.GetData("ТРУДОЕМКОСТЬ_КОЛЛЕКЦИЯ_ЭЛЕМЕНТ"), 2);
				}
				element.SetData("СЕРВИС_ПАКЕТ_НВ", (float)local_sp);		
			}
			return local_sp;
		}

		public int GetManCount(DtCardWork element)
		{
			// Подсчет количества исполнителей, с заполнением списка
			ArrayList array		= new ArrayList();
			long card_number	= (long)element.GetData("НОМЕР_КАРТОЧКА_КАРТОЧКА_РАБОТА");
			int card_year		= (int)element.GetData("ГОД_КАРТОЧКА_КАРТОЧКА_РАБОТА");
			int number			= (int)element.GetData("ПОЗИЦИЯ_КАРТОЧКА_РАБОТА");
			DbSqlStaff.SelectInArrayExecutor(array, card_number, card_year, number);

			// Обработка ошибки, когда нет исполнителей
			if(array.Count < 1)
			{
				Db.SetErrorMessage("РАБОТА БЕЗ ИСПОЛНИТЕЛЯ!!!!");
				Db.ShowFaults();
				return 1;
			}
			if(man_array == null)man_array = new ArrayList();
			foreach(object o in array)
			{
				DtStaff staff = (DtStaff)o;
				long code = (long)staff.GetData("КОД_ПЕРСОНАЛ");
				man_array.Add(code);
			}
			return man_array.Count;
		}

		#region Определение вида работы
		public bool IsTO(DtCardWork element)
		{
			// Получить ссылку на справочник
			long code_work	= (long)element.GetData("КОД_ТРУДОЕМКОСТЬ_КАРТОЧКА_РАБОТА");
			DtWork work		= DbSqlWork.Find(code_work);
			long code_directorywork = (long)work.GetData("ССЫЛКА_КОД_СПРАВОЧНИК_ТРУДОЕМКОСТЬ");
			long to_type			= -1;
			// Определяем, является ли данная работа спекциальным типом
			
			if(code_directorywork  == 9) to_type = 0;
			if(code_directorywork  == 460) to_type = 0;
			if(code_directorywork  == 1) to_type = 1;
			if(code_directorywork  == 2) to_type = 2;
			if(code_directorywork  == 3) to_type = 3;
			if(code_directorywork  == 4) to_type = 4;
			if(code_directorywork  == 5) to_type = 5;
			if(code_directorywork  == 6) to_type = 6;
			if(code_directorywork  == 725) to_type = 7;
			if(code_directorywork  == 737) to_type = 8;
			if(code_directorywork  == 738) to_type = 9;
			if(code_directorywork  == 739) to_type = 10;

			if(code_directorywork  == 740) to_type = 11;
			if(code_directorywork  == 741) to_type = 11;
			if(code_directorywork  == 517) to_type = 11;
			if(code_directorywork  == 727) to_type = 11;
			if(code_directorywork  == 518) to_type = 11;
			if(code_directorywork  == 728) to_type = 11;
			if(code_directorywork  == 730) to_type = 11;
			if(code_directorywork  == 729) to_type = 11;
			if(code_directorywork  == 732) to_type = 11;
			if(code_directorywork  == 731) to_type = 11;
			if(code_directorywork  == 742) to_type = 11;
			if(code_directorywork  == 733) to_type = 11;
			if(code_directorywork  == 734) to_type = 11;
			if(code_directorywork  == 736) to_type = 11;
			if(code_directorywork  == 735) to_type = 11;
				
			if(to_type == -1) return false;
			if(to_type == 0) to0_count = 1;
			if(to_type == 1) to1_count = 1;
			if(to_type == 2) to2_count = 1;
			if(to_type == 3) to3_count = 1;
			if(to_type == 4) to4_count = 1;
			if(to_type == 5) to5_count = 1;
			if(to_type == 6) to6_count = 1;
			if(to_type == 7) to7_count = 1;
			if(to_type == 8) to8_count = 1;
			if(to_type == 9) to9_count = 1;
			if(to_type == 10) to10_count = 1;
			if(to_type == 11) to11_count = 1;
			return true;
		}

		public bool IsWash(DtCardWork element)
		{
			// Получить ссылку на справочник
			long code_work	= (long)element.GetData("КОД_ТРУДОЕМКОСТЬ_КАРТОЧКА_РАБОТА");
			DtWork work		= DbSqlWork.Find(code_work);
			long code_directorywork = (long)work.GetData("ССЫЛКА_КОД_СПРАВОЧНИК_ТРУДОЕМКОСТЬ");
			if(code_directorywork  == 722) return true;
			return false;
		}

		public bool IsPPP(DtCardWork element)
		{
			// Получить ссылку на справочник
			long code_work	= (long)element.GetData("КОД_ТРУДОЕМКОСТЬ_КАРТОЧКА_РАБОТА");
			DtWork work		= DbSqlWork.Find(code_work);
			long code_directorywork = (long)work.GetData("ССЫЛКА_КОД_СПРАВОЧНИК_ТРУДОЕМКОСТЬ");
			if(code_directorywork  == 188) return true;
			return false;
		}
		#endregion

		public void SetInitial()
		{
			cash_all			= 0.0F;
			cash_discount		= 0.0F;
			hour_all			= 0.0F;

			cash				= 0.0F;
			cash_sp				= 0.0F;
			cash_to				= 0.0F;
			cash_ppp			= 0.0F;
			cash_wash			= 0.0F;

			count_sp			= 0;
			count_wash			= 0;
			count_to			= 0;
			count_ppp			= 0;

			hour_to				= 0.0F;
			hour				= 0.0F;
			hour_sp				= 0.0F;
			hour_ppp			= 0.0F;

			count_work			= 0;
				
			to11_count			= 0;
			to0_count			= 0;
			to1_count			= 0;
			to2_count			= 0;
			to3_count			= 0;
			to4_count			= 0;
			to5_count			= 0;
			to6_count			= 0;
			to7_count			= 0;
			to8_count			= 0;
			to9_count			= 0;
			to10_count			= 0;
			
			man_count			= 0;
			cash_byman			= 0.0F;
			hour_byman			= 0.0F;
			cash_sp_byman		= 0.0F;
			hour_sp_byman		= 0.0F;
			count_sp_byman		= 0.0F;
			count_ppp_byman		= 0.0F;
			count_to_byman		= 0.0F;
			is_guaranty			= false;
			type_guaranty		= 0L;
			man_array			= null;
		}

		public void AddProduction(DsProductionWork product)
		{
			cash_all			+= product.cash_all;
			cash_discount		+= product.cash_discount;
			hour_all			+= product.hour_all;

			cash				+= product.cash;
			cash_sp				+= product.cash_sp;
			cash_to				+= product.cash_to;
			cash_ppp			+= product.cash_ppp;
			cash_wash			+= product.cash_wash;

			count_sp			+= product.count_sp;
			count_wash			+= product.count_wash;
			count_to			+= product.count_to;
			count_ppp			+= product.count_ppp;

			hour				+= product.hour;
			hour_sp				+= product.hour_sp;
			hour_to				+= product.hour_to;
			hour_ppp			+= product.hour_ppp;

			count_work			+= product.count_work;
	
			to11_count			+= product.to11_count;
			to0_count			+= product.to0_count;
			to1_count			+= product.to1_count;
			to2_count			+= product.to2_count;
			to3_count			+= product.to3_count;
			to4_count			+= product.to4_count;
			to5_count			+= product.to5_count;
			to6_count			+= product.to6_count;
			to7_count			+= product.to7_count;
			to8_count			+= product.to8_count;
			to9_count			+= product.to9_count;
			to10_count			+= product.to10_count;
			
			man_count			+= product.man_count;
			cash_byman			+= product.cash_byman;
			hour_byman			+= product.hour_byman;
			cash_sp_byman		+= product.cash_sp_byman;
			hour_sp_byman		+= product.hour_sp_byman;
			count_sp_byman		+= product.count_sp_byman;
			count_ppp_byman		+= product.count_ppp_byman;
			count_to_byman		+= product.count_to_byman;
			is_guaranty			= false;
			type_guaranty		= 0L;
			man_array			= null;
		}

		public void SetDiscount(float discount)
		{
			float summ = cash + cash_sp + cash_wash;
			float summ_discount	= summ / 100 * discount;
			cash_discount	= (float)Math.Round(summ_discount);
		}
	}
}
