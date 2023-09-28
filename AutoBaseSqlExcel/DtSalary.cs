using System;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DtSalary.
	/// </summary>
	public class DtSalary
	{
		// Расчетные данные
		public float		cash;
		public float		cash_hour;
		public float		cash_hour_count;
		public float		hour;
		public int			ppp_count;
		public int			to_count;
		public float		cash_to;
		public int			guarantee_ppp_count;
		public float		guarantee_hour;
		public float		guarantee_cash;
		public int			guarantee_to_count;
		public int			wash_count;

		// Для зарплаты
		public float		salary_cash;
		public float		salary_cash_hour;
		public float		salary_hour;
		public float		salary_guarantee;
		public float		salary_ppp;
		public float		salary;

		public DtSalary(long code, DateTime start_date, DateTime end_date)
		{
			ArrayList works = new ArrayList();
			DbSqlCardWork.SelectInArray(code, start_date, end_date, works);

			cash							= 0.0F;		// Выполненые договрные работы (с учетом скидки и приведенные на количество исполнителей)
			cash_hour						= 0.0F;		// Выполненые работы по нормачасу
			cash_hour_count					= 0.0F;		// Количество выполненных работ по нормачасу
			hour							= 0.0F;		// Количество нерасцененных нормачасов
			ppp_count						= 0;		// Количество выполненных предпродажных подготовок
			to_count						= 0;		// Количество выполненных ТО
			cash_to							= 0.0F;		// Деньги полученные именно за ТО
			guarantee_ppp_count				= 0;		// Количество выполненных предпродажек, отмеченных как гарантия
			guarantee_cash					= 0.0F;		// Количество гарантийных договорных работ (приведенные на количество исполнителей)
			guarantee_hour					= 0.0F;		// Количество отработанных гарантийных нормачасов (приведенные на количество исполнителей)
			guarantee_to_count				= 0;		// Количество выполненных ТО (отмеченных как гарантия)

			wash_count						= 0;		// Количество выполненных моек
			

			foreach(object o in works)
			{
				DtCardWork work = (DtCardWork)o;
				if (work.WorkType() == WORK_TYPE.WASH) wash_count++;
				//	if((long)work.GetData("ССЫЛКА_КОД_СПРАВОЧНИК_ТРУДОЕМКОСТЬ")== 722)
				//		{
				//			wash_count++;	// Общее количество произведенных моек
				//	}
				//if((bool)work.GetData("ГАРАНТИЯ_КАРТОЧКА_РАБОТА") == false)
				if (work.GuaranteeFlag() == false)
				{
					//if((long)work.GetData("СПЕЦИАЛЬНЫЙ_ТИП")== 1)
					if((bool)work.IsTo() == true)
					{
						to_count++;
						if((float)work.GetData("ТРУДОЕМКОСТЬ_КАРТОЧКА_РАБОТА") == 0.0F)
						{
							float	summ		= (float)work.GetData("НОРМАЧАС_КАРТОЧКА_РАБОТА")*(float)work.GetData("КОЛИЧЕСТВО_КАРТОЧКА_РАБОТА");
							float	discount	= (float)work.GetData("НОРМАЧАС_КАРТОЧКА_РАБОТА")*(float)work.GetData("КОЛИЧЕСТВО_КАРТОЧКА_РАБОТА") / 100.0F * (float)work.GetData("СКИДКА_КАРТОЧКА_РАБОТА");
							float	summ_person	= (summ - discount) / (float)(int)work.GetData("КОЛИЧЕСТВО_ИСПОЛНИТЕЛЕЙ");
							cash_to				+= summ_person;
						}
						else
						{
							float	summ		= (float)work.GetData("ТРУДОЕМКОСТЬ_КАРТОЧКА_РАБОТА")*(float)work.GetData("НОРМАЧАС_КАРТОЧКА_РАБОТА")*(float)work.GetData("КОЛИЧЕСТВО_КАРТОЧКА_РАБОТА");
							float	discount	= (float)work.GetData("ТРУДОЕМКОСТЬ_КАРТОЧКА_РАБОТА")*(float)work.GetData("НОРМАЧАС_КАРТОЧКА_РАБОТА")*(float)work.GetData("КОЛИЧЕСТВО_КАРТОЧКА_РАБОТА") / 100.0F * (float)work.GetData("СКИДКА_КАРТОЧКА_РАБОТА");
							float	summ_person	= (summ - discount) / (float)(int)work.GetData("КОЛИЧЕСТВО_ИСПОЛНИТЕЛЕЙ");
							cash_to				+= summ_person;
						}
					}
					if((long)work.GetData("ССЫЛКА_КОД_СПРАВОЧНИК_ТРУДОЕМКОСТЬ")== 188)
					{
						ppp_count++;
					}
					else
					{
						if((float)work.GetData("ТРУДОЕМКОСТЬ_КАРТОЧКА_РАБОТА") == 0.0F)
						{
							float	summ		= (float)work.GetData("НОРМАЧАС_КАРТОЧКА_РАБОТА")*(float)work.GetData("КОЛИЧЕСТВО_КАРТОЧКА_РАБОТА");
							float	discount	= (float)work.GetData("НОРМАЧАС_КАРТОЧКА_РАБОТА")*(float)work.GetData("КОЛИЧЕСТВО_КАРТОЧКА_РАБОТА") / 100.0F * (float)work.GetData("СКИДКА_КАРТОЧКА_РАБОТА");
							float	summ_person	= (summ - discount) / (float)(int)work.GetData("КОЛИЧЕСТВО_ИСПОЛНИТЕЛЕЙ");
							cash				+= summ_person;
						}
						else
						{
							float	summ		= (float)work.GetData("ТРУДОЕМКОСТЬ_КАРТОЧКА_РАБОТА")*(float)work.GetData("НОРМАЧАС_КАРТОЧКА_РАБОТА")*(float)work.GetData("КОЛИЧЕСТВО_КАРТОЧКА_РАБОТА");
							float	discount	= (float)work.GetData("ТРУДОЕМКОСТЬ_КАРТОЧКА_РАБОТА")*(float)work.GetData("НОРМАЧАС_КАРТОЧКА_РАБОТА")*(float)work.GetData("КОЛИЧЕСТВО_КАРТОЧКА_РАБОТА") / 100.0F * (float)work.GetData("СКИДКА_КАРТОЧКА_РАБОТА");
							float	summ_person	= (summ - discount) / (float)(int)work.GetData("КОЛИЧЕСТВО_ИСПОЛНИТЕЛЕЙ");
							cash_hour			+= summ_person;
							if((float)work.GetData("НОРМАЧАС_КАРТОЧКА_РАБОТА") == 0.0F)
							{
								// Нерасцененные нормачасы, с учетом скидки
								summ			= (float)work.GetData("ТРУДОЕМКОСТЬ_КАРТОЧКА_РАБОТА")*(float)work.GetData("КОЛИЧЕСТВО_КАРТОЧКА_РАБОТА");
								discount		= (float)work.GetData("ТРУДОЕМКОСТЬ_КАРТОЧКА_РАБОТА")*(float)work.GetData("КОЛИЧЕСТВО_КАРТОЧКА_РАБОТА") / 100.0F * (float)work.GetData("СКИДКА_КАРТОЧКА_РАБОТА");
								hour			+= (summ - discount) /(float)(int)work.GetData("КОЛИЧЕСТВО_ИСПОЛНИТЕЛЕЙ");
							}
							else
							{
								cash_hour_count += (float)work.GetData("ТРУДОЕМКОСТЬ_КАРТОЧКА_РАБОТА")*(float)work.GetData("КОЛИЧЕСТВО_КАРТОЧКА_РАБОТА")/(float)(int)work.GetData("КОЛИЧЕСТВО_ИСПОЛНИТЕЛЕЙ");
							}
						}
					}
				}
				else
				{
					//if((long)work.GetData("СПЕЦИАЛЬНЫЙ_ТИП")== 1)
					if((bool)work.IsTo() == true)
					{
						guarantee_to_count++;
					}
					if((long)work.GetData("ССЫЛКА_КОД_СПРАВОЧНИК_ТРУДОЕМКОСТЬ")== 188)
					{
						guarantee_ppp_count++;
					}
					else
					{
						if((float)work.GetData("ТРУДОЕМКОСТЬ_КАРТОЧКА_РАБОТА") == 0.0F)
						{
							float	summ		= (float)work.GetData("НОРМАЧАС_КАРТОЧКА_РАБОТА")*(float)work.GetData("КОЛИЧЕСТВО_КАРТОЧКА_РАБОТА");
							float	summ_person	= summ / (float)(int)work.GetData("КОЛИЧЕСТВО_ИСПОЛНИТЕЛЕЙ");
							guarantee_cash		+= summ_person;
						}
						else
						{
							guarantee_hour		+= (float)work.GetData("ТРУДОЕМКОСТЬ_КАРТОЧКА_РАБОТА")*(float)work.GetData("КОЛИЧЕСТВО_КАРТОЧКА_РАБОТА")/(float)(int)work.GetData("КОЛИЧЕСТВО_ИСПОЛНИТЕЛЕЙ");
						}
					}
				}
			}

			// Расчет заработной платы
			salary_cash			= 0.0F;
			salary_cash_hour	= 0.0F;
			salary_hour			= 0.0F;
			salary_guarantee	= 0.0F;
			salary_ppp			= 0.0F;
			salary				= 0.0F;

			float coef_cash			= 0.16F;
			float coef_cash_hour	= 0.3F;
			float coef_hour			= 80.0F;
			float coef_guaranty		= 160.0F;
			float coef_ppp			= 150.0F;

			salary_cash			= cash * coef_cash;
			salary_cash_hour	= cash_hour * coef_cash_hour;
			salary_hour			= hour * coef_hour;
			salary_guarantee	= guarantee_hour * coef_guaranty;
			salary_ppp			= (ppp_count + guarantee_ppp_count) * coef_ppp;
			salary				= salary_cash + salary_cash_hour + salary_hour + salary_guarantee + salary_ppp;
		}

		public DtSalary(long code, int year, int month)
		{
			ArrayList works = new ArrayList();
			DbSqlCardWork.SelectInArray(code, year, month, works);

			cash							= 0.0F;		// Выполненые договрные работы (с учетом скидки и приведенные на количество исполнителей)
			cash_hour						= 0.0F;		// Выполненые работы по нормачасу
			cash_hour_count					= 0.0F;		// Количество выполненных работ по нормачасу
			hour							= 0.0F;		// Количество нерасцененных нормачасов
			ppp_count						= 0;		// Количество выполненных предпродажных подготовок
			to_count						= 0;		// Количество выполненных ТО
			cash_to							= 0.0F;		// Деньги полученные именно за ТО
			guarantee_ppp_count				= 0;		// Количество выполненных предпродажек, отмеченных как гарантия
			guarantee_cash					= 0.0F;		// Количество гарантийных договорных работ (приведенные на количество исполнителей)
			guarantee_hour					= 0.0F;		// Количество отработанных гарантийных нормачасов (приведенные на количество исполнителей)
			guarantee_to_count				= 0;		// Количество выполненных ТО (отмеченных как гарантия)

			wash_count						= 0;		// Количество выполненных моек
			

			foreach(object o in works)
			{
				DtCardWork work = (DtCardWork)o;
				if((long)work.GetData("ССЫЛКА_КОД_СПРАВОЧНИК_ТРУДОЕМКОСТЬ")== 722)
				{
					wash_count++;	// Общее количество произведенных моек
				}
				if((bool)work.GetData("ГАРАНТИЯ_КАРТОЧКА_РАБОТА") == false)
				{
					//if((long)work.GetData("СПЕЦИАЛЬНЫЙ_ТИП")== 1)
					if((bool)work.IsTo() == true)
					{
						to_count++;
						if((float)work.GetData("ТРУДОЕМКОСТЬ_КАРТОЧКА_РАБОТА") == 0.0F)
						{
							float	summ		= (float)work.GetData("НОРМАЧАС_КАРТОЧКА_РАБОТА")*(float)work.GetData("КОЛИЧЕСТВО_КАРТОЧКА_РАБОТА");
							float	discount	= (float)work.GetData("НОРМАЧАС_КАРТОЧКА_РАБОТА")*(float)work.GetData("КОЛИЧЕСТВО_КАРТОЧКА_РАБОТА") / 100.0F * (float)work.GetData("СКИДКА_КАРТОЧКА_РАБОТА");
							float	summ_person	= (summ - discount) / (float)(int)work.GetData("КОЛИЧЕСТВО_ИСПОЛНИТЕЛЕЙ");
							cash_to				+= summ_person;
						}
						else
						{
							float	summ		= (float)work.GetData("ТРУДОЕМКОСТЬ_КАРТОЧКА_РАБОТА")*(float)work.GetData("НОРМАЧАС_КАРТОЧКА_РАБОТА")*(float)work.GetData("КОЛИЧЕСТВО_КАРТОЧКА_РАБОТА");
							float	discount	= (float)work.GetData("ТРУДОЕМКОСТЬ_КАРТОЧКА_РАБОТА")*(float)work.GetData("НОРМАЧАС_КАРТОЧКА_РАБОТА")*(float)work.GetData("КОЛИЧЕСТВО_КАРТОЧКА_РАБОТА") / 100.0F * (float)work.GetData("СКИДКА_КАРТОЧКА_РАБОТА");
							float	summ_person	= (summ - discount) / (float)(int)work.GetData("КОЛИЧЕСТВО_ИСПОЛНИТЕЛЕЙ");
							cash_to				+= summ_person;
						}
					}
					if((long)work.GetData("ССЫЛКА_КОД_СПРАВОЧНИК_ТРУДОЕМКОСТЬ")== 188)
					{
						ppp_count++;
					}
					else
					{
						if((float)work.GetData("ТРУДОЕМКОСТЬ_КАРТОЧКА_РАБОТА") == 0.0F)
						{
							float	summ		= (float)work.GetData("НОРМАЧАС_КАРТОЧКА_РАБОТА")*(float)work.GetData("КОЛИЧЕСТВО_КАРТОЧКА_РАБОТА");
							float	discount	= (float)work.GetData("НОРМАЧАС_КАРТОЧКА_РАБОТА")*(float)work.GetData("КОЛИЧЕСТВО_КАРТОЧКА_РАБОТА") / 100.0F * (float)work.GetData("СКИДКА_КАРТОЧКА_РАБОТА");
							float	summ_person	= (summ - discount) / (float)(int)work.GetData("КОЛИЧЕСТВО_ИСПОЛНИТЕЛЕЙ");
							cash				+= summ_person;
						}
						else
						{
							float	summ		= (float)work.GetData("ТРУДОЕМКОСТЬ_КАРТОЧКА_РАБОТА")*(float)work.GetData("НОРМАЧАС_КАРТОЧКА_РАБОТА")*(float)work.GetData("КОЛИЧЕСТВО_КАРТОЧКА_РАБОТА");
							float	discount	= (float)work.GetData("ТРУДОЕМКОСТЬ_КАРТОЧКА_РАБОТА")*(float)work.GetData("НОРМАЧАС_КАРТОЧКА_РАБОТА")*(float)work.GetData("КОЛИЧЕСТВО_КАРТОЧКА_РАБОТА") / 100.0F * (float)work.GetData("СКИДКА_КАРТОЧКА_РАБОТА");
							float	summ_person	= (summ - discount) / (float)(int)work.GetData("КОЛИЧЕСТВО_ИСПОЛНИТЕЛЕЙ");
							cash_hour			+= summ_person;
							if((float)work.GetData("НОРМАЧАС_КАРТОЧКА_РАБОТА") == 0.0F)
							{
								// Нерасцененные нормачасы, с учетом скидки
								summ			= (float)work.GetData("ТРУДОЕМКОСТЬ_КАРТОЧКА_РАБОТА")*(float)work.GetData("КОЛИЧЕСТВО_КАРТОЧКА_РАБОТА");
								discount		= (float)work.GetData("ТРУДОЕМКОСТЬ_КАРТОЧКА_РАБОТА")*(float)work.GetData("КОЛИЧЕСТВО_КАРТОЧКА_РАБОТА") / 100.0F * (float)work.GetData("СКИДКА_КАРТОЧКА_РАБОТА");
								hour			+= (summ - discount) /(float)(int)work.GetData("КОЛИЧЕСТВО_ИСПОЛНИТЕЛЕЙ");
							}
							else
							{
								cash_hour_count += (float)work.GetData("ТРУДОЕМКОСТЬ_КАРТОЧКА_РАБОТА")*(float)work.GetData("КОЛИЧЕСТВО_КАРТОЧКА_РАБОТА")/(float)(int)work.GetData("КОЛИЧЕСТВО_ИСПОЛНИТЕЛЕЙ");
							}
						}
					}
				}
				else
				{
					//if((long)work.GetData("СПЕЦИАЛЬНЫЙ_ТИП")== 1)
					if((bool)work.IsTo() == true)
					{
						guarantee_to_count++;
					}
					if((long)work.GetData("ССЫЛКА_КОД_СПРАВОЧНИК_ТРУДОЕМКОСТЬ")== 188)
					{
						guarantee_ppp_count++;
					}
					else
					{
						if((float)work.GetData("ТРУДОЕМКОСТЬ_КАРТОЧКА_РАБОТА") == 0.0F)
						{
							float	summ		= (float)work.GetData("НОРМАЧАС_КАРТОЧКА_РАБОТА")*(float)work.GetData("КОЛИЧЕСТВО_КАРТОЧКА_РАБОТА");
							float	summ_person	= summ / (float)(int)work.GetData("КОЛИЧЕСТВО_ИСПОЛНИТЕЛЕЙ");
							guarantee_cash		+= summ_person;
						}
						else
						{
							guarantee_hour		+= (float)work.GetData("ТРУДОЕМКОСТЬ_КАРТОЧКА_РАБОТА")*(float)work.GetData("КОЛИЧЕСТВО_КАРТОЧКА_РАБОТА")/(float)(int)work.GetData("КОЛИЧЕСТВО_ИСПОЛНИТЕЛЕЙ");
						}
					}
				}
			}

			// Расчет заработной платы
			salary_cash			= 0.0F;
			salary_cash_hour	= 0.0F;
			salary_hour			= 0.0F;
			salary_guarantee	= 0.0F;
			salary_ppp			= 0.0F;
			salary				= 0.0F;

			float coef_cash			= 0.16F;
			float coef_cash_hour	= 0.3F;
			float coef_hour			= 80.0F;
			float coef_guaranty		= 160.0F;
			float coef_ppp			= 150.0F;

			salary_cash			= cash * coef_cash;
			salary_cash_hour	= cash_hour * coef_cash_hour;
			salary_hour			= hour * coef_hour;
			salary_guarantee	= guarantee_hour * coef_guaranty;
			salary_ppp			= (ppp_count + guarantee_ppp_count) * coef_ppp;
			salary				= salary_cash + salary_cash_hour + salary_hour + salary_guarantee + salary_ppp;
		}
	}
}
