using System;

namespace AutoBaseSql
{
	/// <summary>
	/// Догполнительная информация о продаже автомобиля.
	/// </summary>
	public class CS_SellInfo
	{
		public long code_sell;				// Код продажи
		public long code_position;			// Код местоположения покупателя
		public long code_reklama;			// Код рекламного источника
		public bool flag_credit_inner;		// Флаг внутреннего кредита
		public bool flag_credit_outer;		// Флаг внешнего кредита
		public bool flag_lising;			// Флаг продажи в лизинг
		public bool flag_cashless;			// Флаг безналичной продажи
		public bool flag_partner;			// Флаг продажи через партнеров
		public bool flag_util;				// Флаг утилизации

        public bool flag_tin;				// Флаг trade-in
        public long tinprice;               // Стоимость сдаваемого в зачет автомобиля

		public CS_SellInfo()
		{
			code_sell			= 0;
			code_position		= 0;
			code_reklama		= 0;
			flag_credit_inner	= false;
			flag_credit_outer	= false;
			flag_lising			= false;
			flag_cashless		= false;
			flag_partner		= false;
			flag_util			= false;

            flag_tin = false;
            tinprice = 0;
		}

		public CS_SellInfo(long sell)
		{
			code_sell			= sell;
			code_position		= 0;
			code_reklama		= 0;
			flag_credit_inner	= false;
			flag_credit_outer	= false;
			flag_lising			= false;
			flag_cashless		= false;
			flag_partner		= false;
			flag_util			= false;

            flag_tin = false;
            tinprice = 0;
		}
	}
}
