using System;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DtCardWorkend.
	/// </summary>
	public class DtCardWorkend
	{
		long		card_number;	// Íîìåğ êàğòî÷êè
		int			card_year;		// Ãîä êàğòî÷êè
		DateTime	date;			// Äàòà è âğåìÿ îêîí÷àíèÿ ğåìîíòà

		public DtCardWorkend(long the_card_number, int the_card_year, DateTime the_date)
		{
			card_number = the_card_number;
			card_year	= the_card_year;
			date		= the_date;
		}

		public DtCardWorkend()
		{
			card_number = 0L;
			card_year	= 0;
			date		= DateTime.Now;
		}

		public object GetData(string data)
		{
			switch(data)
			{
				case "ÑÑÛËÊÀ_ÊÀĞÒÎ×ÊÀ_ÍÎÌÅĞ":
					return (object)(long)card_number;
				case "ÑÑÛËÊÀ_ÊÀĞÒÎ×ÊÀ_ÃÎÄ":
					return (object)(int)card_year;
				case "ÄÀÒÀ_ÎÊÎÍ×ÀÍÈß_ĞÅÌÎÍÒÀ":
					return (object)(DateTime)date;
				default:
					return (object)null;
			}
		}

		public void SetData(string data, object val)
		{
			switch(data)
			{
				case "ÑÑÛËÊÀ_ÊÀĞÒÎ×ÊÀ_ÍÎÌÅĞ":
					card_number = (long)val;
					break;
				case "ÑÑÛËÊÀ_ÊÀĞÒÎ×ÊÀ_ÃÎÄ":
					card_year = (int)val;
					break;
				case "ÄÀÒÀ_ÎÊÎÍ×ÀÍÈß_ĞÅÌÎÍÒÀ":
					date = (DateTime)val;
					break;
				default:
					break;
			}
		}
	}
}
