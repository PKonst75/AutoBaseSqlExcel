using System;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DtCardMarkEndWork.
	/// </summary>
	public class DtCardMarkEndWork
	{
		long		card_number;	// Íîìåğ êàğòî÷êè
		int			card_year;		// Ãîä êàğòî÷êè
		DateTime	date;			// Äàòà è âğåìÿ îêîí÷àíèÿ ğåìîíòà

		public DtCardMarkEndWork(long the_card_number, int the_card_year)
		{
			card_number = the_card_number;
			card_year	= the_card_year;
			date		= DateTime.Now;
		}

		public DtCardMarkEndWork()
		{
			card_number = 0L;
			card_year	= 0;
			date		= DateTime.Now;
		}

		public object GetData(string data)
		{
			switch(data)
			{
				case "ÍÎÌÅĞ_ÊÀĞÒÎ×ÊÀ":
					return (object)(long)card_number;
				case "ÃÎÄ_ÊÀĞÒÎ×ÊÀ":
					return (object)(int)card_year;
				case "ÄÀÒÀ":
					return (object)(DateTime)date;
				default:
					return (object)null;
			}
		}

		public void SetData(string data, object val)
		{
			switch(data)
			{
				case "ÍÎÌÅĞ_ÊÀĞÒÎ×ÊÀ":
					card_number = (long)val;
					break;
				case "ÃÎÄ_ÊÀĞÒÎ×ÊÀ":
					card_year = (int)val;
					break;
				case "ÄÀÒÀ":
					date = (DateTime)val;
					break;
				default:
					break;
			}
		}
	}
}
