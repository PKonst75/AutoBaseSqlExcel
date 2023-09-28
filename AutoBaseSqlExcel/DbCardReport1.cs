using System;
using System.Collections;


namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbCardReport1.
	/// </summary>
	public class DbCardReport1
	{
		// Исходные параметры
		public DbCard				card;
		public ArrayList			cardWorks;
		public ArrayList			cardDetails;
		private ArrayList			cardRecomend;

		// Отслеживаемые параметры по работам
		public bool		to_is;			// Присутсвует ли в заказ-наряде плановое ТО
		public float	to_summ;		// Сумма работ по плановому ТО
		public bool		norm_is;		// Присутсвие работ по нормачасам
		public float	norm_summ;		// Сумма работ по нормачасам
		public float	norm_time;		// Время работ по нормачасам
		public bool		work_is;		// Присутсвие работ договорных
		public float	work_summ;		// Сумма работ договорных
		public bool		wash_is;		// Присутсвие работ моечных
		public float	wash_summ;		// Сумма работ моечных
		public bool		ppp_is;			// Пристутсвие ППП
		public float	ppp_summ;		// Сумма ППП
		public float	work_whole;		// Сумма всех работ вместе

		public string	to_text;		// Работа принятая нами за ТО

		// Отслеживаемые параметры по работам - гарантии
		public bool		g_to_is;		// Присутсвует ли в заказ-наряде плановое ТО
		public float	g_to_summ;		// Сумма работ по плановому ТО
		public bool		g_norm_is;		// Присутсвие работ по нормачасам
		public float	g_norm_summ;	// Сумма работ по нормачасам
		public float	g_norm_time;	// Время работ по нормачасам
		public bool		g_work_is;		// Присутсвие работ договорных
		public float	g_work_summ;	// Сумма работ договорных
		public bool		g_wash_is;		// Присутсвие работ моечных
		public float	g_wash_summ;	// Сумма работ моечных
		public bool		g_ppp_is;		// Пристутсвие ППП
		public float	g_ppp_summ;		// Сумма ППП
		public float	g_work_whole;	// Сумма всех работ вместе

		// Отслеживаемые параметры по деталям
		public bool		liquid_is;			// Писутсвие жидкостей
		public float	liquid_summ;		// Сумма жидкостей
		public bool		liquid_bad_is;		// Присутсвие нерасцененных жидкостей
		public float	liquid_input;		// Вход жидкостей
		public bool		detail_is;			// Писутсвие деталей
		public float	detail_summ;		// Сумма сумма деталей
		public bool		detail_bad_is;		// Присутсвие нерасцененных деталей
		public float	detail_input;		// Вход деталей

		// Отслеживаемые параметры по гарантийным деталям 
		public bool		g_liquid_is;		// Писутсвие жидкостей
		public float	g_liquid_summ;		// Сумма жидкостей
		public bool		g_liquid_bad_is;	// Присутсвие нерасцененных жидкостей
		public float	g_liquid_input;		// Вход жидкостей
		public bool		g_detail_is;		// Писутсвие деталей
		public float	g_detail_summ;		// Сумма сумма деталей
		public bool		g_detail_bad_is;	// Присутсвие нерасцененных деталей
		public float	g_detail_input;		// Вход деталей


		public DbCardReport1(long card_number, int card_year)
		{
			card = new DbCard();
			card.Number	= card_number;
			card.Year	= card_year;

			// Получаем список работ данной карточки
			cardWorks = new ArrayList();
			DbCardWork.FillList(cardWorks, card);
			// Получаем список деталей данной карточки
			cardDetails = new ArrayList();
			DbCardDetail.FillList(cardDetails, card);

			// Делаем отчет
			Report_Work();
			Report_Work_Guaranty();
			Report_Detail();
			Report_Detail_Guaranty();
		}

		public DbCardReport1(DbCard cardSrc)
		{
			card = cardSrc;
			
			// Получаем список работ данной карточки
			cardWorks = new ArrayList();
			DbCardWork.FillList(cardWorks, card);
			// Получаем список деталей данной карточки
			cardDetails = new ArrayList();
			DbCardDetail.FillList(cardDetails, card);

			// Делаем отчет
			Report_Work();
			Report_Work_Guaranty();
			Report_Detail();
			Report_Detail_Guaranty();
		}

		protected void Report_Work()
		{
			to_is		= false;
			to_summ		= 0.0F;
			ppp_is		= false;
			ppp_summ	= 0.0F;
			wash_is		= false;
			wash_summ	= 0.0F;
			work_is		= false;
			work_summ	= 0.0F;
			norm_is		= false;
			norm_summ	= 0.0F;
			norm_time	= 0.0F;
			work_whole	= 0.0F;
			// Обработка информации о работах
			foreach(object o in cardWorks)
			{
				DbCardWork wrk = (DbCardWork)o;
				if(wrk.Guaranty == false)
				{
					if(IsTO(wrk) == true)
					{
						to_is	= true;
						to_summ += wrk.Summ;
						to_text	= wrk.WorkName;
					}
					else
					{ 
						if(IsPPP(wrk))
						{
							ppp_summ	+= wrk.Summ;
							ppp_is		= true;
						}
						else
						{
							if(IsWash(wrk))
							{
								wash_is		= true;
								wash_summ	+= wrk.Summ;
							}
							else
							{
								// Все остальное - просто работы
								if(wrk.Val == 0)
								{
									// Договорная работа
									work_is		= true;
									work_summ	+= wrk.Summ;
								}
								else
								{
									// Работа по нормачасам
									norm_is		= true;
									norm_summ	+= wrk.Summ;
									norm_time	+= wrk.Val;
								}
							}
						}
					}
					work_whole += wrk.Summ;
				}
			}
		}

		protected void Report_Work_Guaranty()
		{
			g_to_is			= false;
			g_to_summ		= 0.0F;
			g_ppp_is		= false;
			g_ppp_summ		= 0.0F;
			g_wash_is		= false;
			g_wash_summ		= 0.0F;
			g_work_is		= false;
			g_work_summ		= 0.0F;
			g_norm_is		= false;
			g_norm_summ		= 0.0F;
			g_norm_time		= 0.0F;
			g_work_whole	= 0.0F;
			// Обработка информации о работах
			foreach(object o in cardWorks)
			{
				DbCardWork wrk = (DbCardWork)o;
				if(wrk.Guaranty == true)
				{
					if(IsTO(wrk) == true)
					{
						g_to_is	= true;
						g_to_summ += wrk.SummFull;
					}
					else
					{ 
						if(IsPPP(wrk))
						{
							g_ppp_summ	+= wrk.SummFull;
							g_ppp_is	= true;
						}
						else
						{
							if(IsWash(wrk))
							{
								g_wash_is	= true;
								g_wash_summ	+= wrk.SummFull;
							}
							else
							{
								// Все остальное - просто работы
								if(wrk.Val == 0)
								{
									// Договорная работа
									g_work_is	= true;
									g_work_summ	+= wrk.SummFull;
								}
								else
								{
									// Работа по нормачасам
									g_norm_is	= true;
									g_norm_summ	+= wrk.SummFull;
									g_norm_time	+= wrk.Val;
								}
							}
						}
					}
					g_work_whole += wrk.SummFull;
				}
			}
		}


		protected void Report_Detail()
		{
			liquid_is		= false;
			liquid_summ		= 0.0F;
			liquid_bad_is	= false;
			detail_is		= false;
			detail_summ		= 0.0F;
			detail_bad_is		= false;
			// Обработка информации о деталях
			foreach(object o in cardDetails)
			{
				DbCardDetail dtl = (DbCardDetail)o;
				if(dtl.Guaranty == false)
				{
					if(dtl.Oil == true)
					{
						liquid_is	= true;
						liquid_summ += dtl.Summ;
						liquid_input+= dtl.InputSumm;
						if(dtl.InputSumm == 0) liquid_bad_is	= true;
					}
					else
					{ 
						detail_is	= true;
						detail_summ += dtl.Summ;
						detail_input+= dtl.InputSumm;
						if(dtl.InputSumm == 0) detail_bad_is	= true;
					}
				}
			}
		}

		protected void Report_Detail_Guaranty()
		{
			g_liquid_is			= false;
			g_liquid_summ		= 0.0F;
			g_liquid_bad_is		= false;
			g_detail_is			= false;
			g_detail_summ		= 0.0F;
			g_detail_bad_is		= false;
			// Обработка информации о деталях
			foreach(object o in cardDetails)
			{
				DbCardDetail dtl = (DbCardDetail)o;
				if(dtl.Guaranty == true)
				{
					if(dtl.Oil == true)
					{
						g_liquid_is	= true;
						g_liquid_summ += dtl.SummWhole;
						g_liquid_input+= dtl.InputSumm;
						if(dtl.InputSumm == 0) g_liquid_bad_is	= true;
					}
					else
					{ 
						g_detail_is	= true;
						g_detail_summ += dtl.SummWhole;
						g_detail_input+= dtl.InputSumm;
						if(dtl.InputSumm == 0) g_detail_bad_is	= true;
					}
				}
			}
		}


		// Является ли работа регламентным ТО
		protected bool IsTO(DbCardWork work)
		{
			if(work.CodeDirectoryWork == 1) return true;
			if(work.CodeDirectoryWork == 2) return true;
			if(work.CodeDirectoryWork == 3) return true;
			if(work.CodeDirectoryWork == 4) return true;
			if(work.CodeDirectoryWork == 5) return true;
			if(work.CodeDirectoryWork == 6) return true;
			if(work.CodeDirectoryWork == 725) return true;
			if(work.CodeDirectoryWork == 737) return true;
			if(work.CodeDirectoryWork == 738) return true;
			if(work.CodeDirectoryWork == 739) return true;
			if(work.CodeDirectoryWork == 740) return true;
			if(work.CodeDirectoryWork == 741) return true;
			if(work.CodeDirectoryWork == 9) return true;
			if(work.CodeDirectoryWork == 460) return true;
			if(work.CodeDirectoryWork == 517) return true;
			if(work.CodeDirectoryWork == 727) return true;
			if(work.CodeDirectoryWork == 518) return true;
			if(work.CodeDirectoryWork == 728) return true;
			if(work.CodeDirectoryWork == 730) return true;
			if(work.CodeDirectoryWork == 729) return true;
			if(work.CodeDirectoryWork == 732) return true;
			if(work.CodeDirectoryWork == 731) return true;
			if(work.CodeDirectoryWork == 742) return true;
			if(work.CodeDirectoryWork == 733) return true;
			if(work.CodeDirectoryWork == 734) return true;
			if(work.CodeDirectoryWork == 736) return true;
			if(work.CodeDirectoryWork == 735) return true;
			return false;
		}
		// Является ли оабота предпродажной подготовкой
		public bool IsPPP(DbCardWork work)
		{
			if(work.CodeDirectoryWork == 188) return true;
			return false;
		}
		// Является ли работа моечно-уборочной
		public bool IsWash(DbCardWork work)
		{
			if(work.CodeDirectoryWork == 722) return true;
			return false;
		}
	}
}
