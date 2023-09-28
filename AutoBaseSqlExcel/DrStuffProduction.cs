using System;
using System.Collections;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DrStuffProduction.
	/// </summary>
	public class DrStuffProduction
	{
		public ArrayList		works;				// Полный список работ

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

				if (val == 0.0F)
				{
					// Требуется дополнительный анализ сервис-пакета
					double local_sp			= Math.Round(GetSPHours(card_work),2);
					cash_sp					+= Math.Round(price, 2) * Math.Round(quontity, 2) / count;
					if(local_sp == 0.0F)
					{
						cash_sp_nohours		+= Math.Round(price, 2) * Math.Round(quontity, 2) / count;
						count_sp_nohours	++;
					}
					else
					{
						hours_sp			+= local_sp * Math.Round(quontity, 2) / count;
					}
				}
				else
				{
				
					cash_hours	+= Math.Round(val, 2) * Math.Round(price, 2) * Math.Round(quontity, 2) / count;
					hours		+= Math.Round(val, 2) * Math.Round(quontity, 2) / count;
				}
			}

			public static float GetSPHours(DtCardWork element)
			{
				// Получение данных о НВ сервис пакета
				long code_work = (long)element.GetData("КОД_ТРУДОЕМКОСТЬ_КАРТОЧКА_РАБОТА");
				DtWork work = DbSqlWork.Find(code_work);

				// Для начала ищем чистое НВ
				float local_nv	= (float)work.GetData("НВ");
				if(local_nv != 0.0F)
				{
					element.SetData("СЕРВИС_ПАКЕТ_НВ", local_nv);
					return local_nv;
				}
				
				long code_collection = (long)work.GetData("ССЫЛКА_КОД_КОЛЛЕКЦИЯ");
				float local_sp = 0.0F;
				if(code_collection !=  0)
				{
					// Есть коллекция
					element.SetData("ССЫЛКА_КОД_КОЛЛЕКЦИЯ", code_collection);
							
					ArrayList array = new ArrayList();
					DbSqlWorkCollectionItem.SelectInArray(array, code_collection);
					foreach(DtWorkCollectionItem elm in array)
					{
						local_sp += (float)elm.GetData("ТРУДОЕМКОСТЬ_КОЛЛЕКЦИЯ_ЭЛЕМЕНТ");
					}
					element.SetData("СЕРВИС_ПАКЕТ_НВ", local_sp);		
				}
				return local_sp;
			}
		}

		public Production	card_cash		= new Production();	// Выработка по заработанным деньгам
		public Production	card_guaranty	= new Production();	// Выработка по гарантии
		public Production	card_inner		= new Production();	// Выработка по внутренним заказ-нарядам

		public DrStuffProduction(long code, int year, int month)
		{
			// Получаем список работ, закрытых в данном месяце на выбранного сотрудника
			works = new ArrayList();
			DbSqlCardWork.SelectInArray(code, year, month, works);

			// Анализируем полученный список
			foreach(object o in works)
			{
				DtCardWork work = (DtCardWork)o;
				DtCard card		= DbSqlCard.Find((long)work.GetData("НОМЕР_КАРТОЧКА_КАРТОЧКА_РАБОТА"), (int)work.GetData("ГОД_КАРТОЧКА_КАРТОЧКА_РАБОТА"));
				if(card == null)
				{
					MessageBox.Show("Именно это");
					MessageBox.Show(work.GetData("НОМЕР_КАРТОЧКА_КАРТОЧКА_РАБОТА").ToString());
					MessageBox.Show(work.GetData("КОД_ТРУДОЕМКОСТЬ_КАРТОЧКА_РАБОТА").ToString());
					MessageBox.Show(code.ToString());
				}
				if((bool)work.GetData("ГАРАНТИЯ_КАРТОЧКА_РАБОТА") == true)
				{
					// Гарантийная работа
					card_guaranty.AddCardWork(work);
				}
				else
				{
					// Не гарантия
					if((bool)card.GetData("ВНУТРЕННИЙ_КАРТОЧКА") == true)
					{
						// Внутренний заказ-няряд
						card_inner.AddCardWork(work);
					}
					else
					{
						// Заработанные деньги
						card_cash.AddCardWork(work);
					}
				}
			}
		}

		public DrStuffProduction(long code, DateTime start_date, DateTime end_date)
		{
			// Получаем список работ, закрытых в данном месяце на выбранного сотрудника
			works = new ArrayList();
			DbSqlCardWork.SelectInArray(code, start_date, end_date, works);

			// Анализируем полученный список
			foreach(object o in works)
			{
				DtCardWork work = (DtCardWork)o;
				DtCard card		= DbSqlCard.Find((long)work.GetData("НОМЕР_КАРТОЧКА_КАРТОЧКА_РАБОТА"), (int)work.GetData("ГОД_КАРТОЧКА_КАРТОЧКА_РАБОТА"));
				if(card == null)
				{
					MessageBox.Show("Именно это");
					MessageBox.Show(work.GetData("НОМЕР_КАРТОЧКА_КАРТОЧКА_РАБОТА").ToString());
					MessageBox.Show(work.GetData("КОД_ТРУДОЕМКОСТЬ_КАРТОЧКА_РАБОТА").ToString());
					MessageBox.Show(code.ToString());
				}
				if((bool)work.GetData("ГАРАНТИЯ_КАРТОЧКА_РАБОТА") == true)
				{
					// Гарантийная работа
					card_guaranty.AddCardWork(work);
				}
				else
				{
					// Не гарантия
					if((bool)card.GetData("ВНУТРЕННИЙ_КАРТОЧКА") == true)
					{
						// Внутренний заказ-няряд
						card_inner.AddCardWork(work);
					}
					else
					{
						// Заработанные деньги
						card_cash.AddCardWork(work);
					}
				}
			}
		}
	}
}
