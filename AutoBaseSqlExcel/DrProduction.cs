using System;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DrProduction.
	/// </summary>
	public class DrProduction
	{
		public ArrayList service_consultants = new ArrayList();

		public class Production
		{
			public double	hours;				// Отработанные нормочасы
			public double	hours_sp;			// Отработанные нормачасы (по сервис-пакетам)
			public double	cash_sp;			// Заработаные по сервис-пакетам деньги
			public double	cash_hours;			// Заработанные деньги
			public double	cash_sp_nohours;	// Заработанные деньги по сервис пакетам, без расшифровки по нормочасам
			public int		count_sp_nohours;	// Количество сервис пакетов, без расшифровки по нормочасам

			public Production()
			{
				hours				= 0.0;
				hours_sp			= 0.0;
				cash_sp				= 0.0;
				count_sp_nohours	= 0;
				cash_hours			= 0.0;
			}

			public void AddCardWork(DtCardWork card_work)
			{
				// Анализируем работу, раскидываем по подгруппам
				float quontity	= (float)card_work.GetData("КОЛИЧЕСТВО_КАРТОЧКА_РАБОТА");	// Количество повторений работы
				float val		= (float)card_work.GetData("ТРУДОЕМКОСТЬ_КАРТОЧКА_РАБОТА");	// Количество повторений работы
				float price		= (float)card_work.GetData("НОРМАЧАС_КАРТОЧКА_РАБОТА");		// Количество стоимость работы/нормачаса
				float count		= (int)card_work.GetData("КОЛИЧЕСТВО_ИСПОЛНИТЕЛЕЙ");		// Количество исполнителей работы
				count			= 1.0F;	// Для сохранения общности оставим

				if (val == 0.0F)
				{
					// Требуется дополнительный анализ сервис-пакета
					double local_sp			= Math.Round(GetSPHours(card_work), 2);
					cash_sp					+= Math.Round(price, 2) * Math.Round(quontity, 2);// / count;
					if(local_sp == 0.0F)
					{
						cash_sp_nohours		+= Math.Round(price, 2) * Math.Round(quontity, 2);// / count;
						count_sp_nohours	++;
					}
					else
					{
						hours_sp			+= local_sp * Math.Round(quontity, 2);// / count;
					}
				}
				else
				{
					cash_hours	+= Math.Round(val, 2) * Math.Round(price, 2) * Math.Round(quontity, 2);// / count;
					hours		+= Math.Round(val, 2) * Math.Round(quontity, 2);// / count;
				}
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
		}

		public class ServiceConsultant
		{
			// Выработка разбитая по сервис-консультантам
			public long code;				// Код сервис-консультанта
			public string name;				// Наименование сервис консультанта (отображаемое)
			public Production production;	// Выработка сервис-консультанта
			
			public ServiceConsultant(DtStaff staff)
			{
				code	= (long)staff.GetData("КОД_ПЕРСОНАЛ");
				name	= (string)staff.GetData("ФАМИЛИЯ_ПЕРСОНАЛ") + " " + (string)staff.GetData("ИМЯ_ПЕРСОНАЛ") + " " + (string)staff.GetData("ОТЧЕСТВО_ПЕРСОНАЛ");
				production = new Production();
			}			
		}

		public class CardWorkEx
		{
			public string txt_card;					// Номер карточки и наряда, дата наряда
			public string txt_card_close_date;		// Дата закрытия наряда
			public string txt_service_consultant;	// Табельный номер сервис-консультанта
			public string txt_inner;				// Указатель внутреннего заказ-наряда
			public string txt_cashless;				// Указатель безналичного заказ-наряда
			public string txt_work_guaranty;		// Указатель Гарантийной работы
			public string txt_work_code;			// Код работы	
			public string txt_work_name;			// Наименование работы
			public string txt_work_count;			// Количество работ
			public string txt_work_nv;				// Норма времени работы
			public string txt_work_price;			// Стоимость нормачаса
			public string txt_work_sp;				// Для сервис пакета - раскрутка по нормам времени
			public string txt_work_nvsum;			// Итоговая сумма норм времени по работам
			
			public CardWorkEx()
			{
				txt_card				= "";
				txt_card_close_date		= "";
				txt_service_consultant	= "";
				txt_inner				= "";
				txt_cashless			= "";
				txt_work_guaranty		= "";
				txt_work_code			= "";
				txt_work_name			= "";
				txt_work_count			= "";
				txt_work_nv				= "";
				txt_work_price			= "";
				txt_work_sp				= "";
				txt_work_nvsum			= "";
			}
			public void SetCard(DtCard card)
			{
				txt_card				= card.GetData("НОМЕР_НАРЯД_КАРТОЧКА").ToString() + "/" + card.GetData("НОМЕР_КАРТОЧКА").ToString() + "/" + ((DateTime)card.GetData("ДАТА_НАРЯД_ОТКРЫТ_КАРТОЧКА")).ToShortDateString();
				txt_card_close_date		= ((DateTime)card.GetData("ДАТА_НАРЯД_ЗАКРЫТ_КАРТОЧКА")).ToShortDateString();
				txt_service_consultant	= card.GetData("МАСТЕР_КОНТРОЛЕР_КАРТОЧКА").ToString();
				if((bool)card.GetData("ВНУТРЕННИЙ_КАРТОЧКА") == true) txt_inner = "+";
				if((bool)card.GetData("БЕЗНАЛИЧНЫЙ_КАРТОЧКА") == true) txt_cashless = "+";
			}
			public void SetCardWork(DtCardWork card_work)
			{
				if((bool)card_work.GetData("ГАРАНТИЯ_КАРТОЧКА_РАБОТА") == true)txt_work_guaranty = "+";
				txt_work_code		= (string)card_work.GetData("НОМЕР_ПОЗИЦИЯ_КАРТОЧКА_РАБОТА");
				txt_work_name		= (string)card_work.GetData("НАИМЕНОВАНИЕ_КАРТОЧКА_РАБОТА");
				txt_work_count		= card_work.GetData("КОЛИЧЕСТВО_КАРТОЧКА_РАБОТА").ToString();
				txt_work_nv			= card_work.GetData("ТРУДОЕМКОСТЬ_КАРТОЧКА_РАБОТА").ToString();
				txt_work_price		= card_work.GetData("НОРМАЧАС_КАРТОЧКА_РАБОТА").ToString();
				if((float)card_work.GetData("ТРУДОЕМКОСТЬ_КАРТОЧКА_РАБОТА") == 0.0F)
				{
					txt_work_nvsum	= ((float)card_work.GetData("СЕРВИС_ПАКЕТ_НВ") * (float)card_work.GetData("КОЛИЧЕСТВО_КАРТОЧКА_РАБОТА")).ToString();
					txt_work_sp		= card_work.GetData("СЕРВИС_ПАКЕТ_НВ").ToString();
				}
				else
				{
					txt_work_nvsum	= ((float)card_work.GetData("ТРУДОЕМКОСТЬ_КАРТОЧКА_РАБОТА") * (float)card_work.GetData("КОЛИЧЕСТВО_КАРТОЧКА_РАБОТА")).ToString();
				}
			}
		}

		public Production	card_cash		= new Production();	// Выработка по заработанным деньгам
		public Production	card_cashless	= new Production();	// Выработка по заработанным деньгам, по безналу
		public Production	card_guaranty	= new Production();	// Выработка по гарантии
		public Production	card_inner		= new Production();	// Выработка по внутренним заказ-нарядам
		public ArrayList	works_ex		= new ArrayList();

		public DrProduction(ArrayList cards)
		{

			// Производим анализ работ по списку карточек
			foreach(DtCard element in cards)
			{
				DtCard card								= DbSqlCard.Find((long)element.GetData("НОМЕР_КАРТОЧКА"), (int)element.GetData("ГОД_КАРТОЧКА"));
				ServiceConsultant service_consultant	= null;
				long service_consultant_code			= (long)card.GetData("МАСТЕР_КОНТРОЛЕР_КАРТОЧКА");
				bool cashless							= (bool)card.GetData("БЕЗНАЛИЧНЫЙ_КАРТОЧКА");
				bool inner								= (bool)card.GetData("ВНУТРЕННИЙ_КАРТОЧКА");
				// Проверяем наличие данных по сервис консультанту
				foreach(ServiceConsultant o in service_consultants)
				{
					if(service_consultant_code == o.code)
						service_consultant = o;
				}
				if(service_consultant == null)
				{
					DtStaff staff			= DbSqlStaff.Find(service_consultant_code);
					service_consultant		= new ServiceConsultant(staff);
					service_consultants.Add(service_consultant);
				}
				// Получили данные сервис консультанта, закрывшего наряд
				ArrayList works = new ArrayList();
				DbSqlCardWork.SelectInArray(card, works);
				// Анализируем полученный список работ по карточке
				foreach(DtCardWork work in works)
				{
					service_consultant.production.AddCardWork(work);		// Данные по сервис-консультанту
					if((bool)work.GetData("ГАРАНТИЯ_КАРТОЧКА_РАБОТА") == false)
					{
						if(inner == true)
						{
							card_inner.AddCardWork(work);
						}
						else
						{
							if(cashless == true)
							{
								card_cashless.AddCardWork(work);
							}
							else
							{
								card_cash.AddCardWork(work);
							}
						}
					}
					else
					{
						card_guaranty.AddCardWork(work);
					}

					// Также добавляем работу в список
					CardWorkEx work_ex = new CardWorkEx();
					work_ex.SetCard(card);
					work_ex.SetCardWork(work);
					works_ex.Add(work_ex);
				}
			}
		}
	}
}
