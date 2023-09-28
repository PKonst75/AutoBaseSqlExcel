using System;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for V1_DtCardRate.
	/// </summary>
	public class V1_DtCardRate
	{
		public long card_number;
		public int card_year;
		public short rate;
		public string comment;
		public bool rate_change;

		public V1_DtCardRate()
		{
			card_number = 0;
			card_year = 0;
			rate = 0;
			comment = "";
			rate_change = false;
		}
	}
}
