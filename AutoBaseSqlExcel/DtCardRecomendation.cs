using System;
using System.Windows.Forms;
using System.Drawing;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DtCardRecomendation.
	/// </summary>
	public class DtCardRecomendation
	{

		long		card_number;		// Íîìåğ êàğòî÷êè
		int			card_year;			// Ãîä êàğòî÷êè
		long		code;				// Ïîğÿäêîâûé íîìåğ ğåêîìåíäàöèè
		string		recomendation;		// Òåêñò ğåêîìåíäàöèè

		public DtCardRecomendation()
		{
			card_number			= 0;
			card_year			= 0;
			code				= 0;
			recomendation		= "";
		}
		public object GetData(string data)
		{
			switch(data)
			{
				case "ÑÑÛËÊÀ_ÍÎÌÅĞ_ÊÀĞÒÎ×ÊÀ":
					return (object)(long)card_number;
				case "ÑÑÛËÊÀ_ÃÎÄ_ÊÀĞÒÎ×ÊÀ":
					return (object)(int)card_year;
				case "ÍÎÌÅĞ_ĞÅÊÎÌÅÍÄÀÖÈß":
					return (object)(long)code;
				case "ĞÅÊÎÌÅÍÄÀÖÈß":
					return (object)(string)recomendation;
				default:
					return (object)null;
			}
		}

		public void SetData(string data, object val)
		{
			switch(data)
			{
				case "ÑÑÛËÊÀ_ÍÎÌÅĞ_ÊÀĞÒÎ×ÊÀ":
					card_number = (long)val;
					break;
				case "ÑÑÛËÊÀ_ÃÎÄ_ÊÀĞÒÎ×ÊÀ":
					card_year = (int)val;
					break;
				case "ÍÎÌÅĞ_ĞÅÊÎÌÅÍÄÀÖÈß":
					code = (long)val;
					break;
				case "ĞÅÊÎÌÅÍÄÀÖÈß":
					recomendation = (string)val;
					break;
				default:
					break;
			}
		}

		public string RecomendationTxt
        {
            get { return recomendation; }
        }
	}
}
