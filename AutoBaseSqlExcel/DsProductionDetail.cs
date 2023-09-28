using System;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DsProductionDetail.
	/// </summary>
	public class DsProductionDetail
	{
		// Данные составляющие компоненты выручки сервиса
		public float cash_all;			// Сумма полученных за детали/расходники наличных
		public float cash_input;		// Сумма входа за проданные детали/расхдные материалы
		public float cash_oil;			// Сумма полученных за расходники наличных
		public float cash_detail;		// Сумма полученных за детали наличных
		public float cash_input_oil;	// Сумма входа за проданные расхдные материалы
		public float cash_input_detail;	// Сумма входа за проданные детали материалы

		public DsProductionDetail()
		{
			//
			// TODO: Add constructor logic here
			//
		}
	}
}
