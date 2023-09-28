using System;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for V1_DtCardRateCall.
	/// </summary>
	public class V1_DtCardRateCall
	{
		public long card_number;
		public int card_year;
		public DateTime call_date;
		public bool work_done;
		public short rate;
		public string comment;

		public V1_DtCardRateCall()
		{
			card_number = 0L;
			card_year = 0;;
			call_date = DateTime.Now;
			work_done = false;
			rate = 0;
			comment = "";
		}
	}
}
