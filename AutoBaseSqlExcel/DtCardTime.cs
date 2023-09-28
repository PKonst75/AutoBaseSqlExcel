using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DtCardTime.
	/// </summary>
	public class DtCardTime
	{
		protected long		card_number;
		protected int		card_year;
		protected bool		is_goin_time;
		protected bool		is_begin_time;
		protected bool		is_end_time;
		protected bool		is_goout_time;
		protected DateTime	goin_time;
		protected DateTime	begin_time;
		protected DateTime	end_time;
		protected DateTime	goout_time;
		protected bool		notime;
		protected bool		pause;

		public DtCardTime()
		{
			card_number		= 0L;
			card_year		= 0;
			is_goin_time	= false;
			is_begin_time	= false;
			is_end_time		= false;
			is_goout_time	= false;
			goin_time		= DateTime.Now;
			begin_time		= DateTime.Now;
			end_time		= DateTime.Now;
			goout_time		= DateTime.Now;
			notime			= false;
			pause			= false;
		}

		public void SetData(string data, object val)
		{
			switch(data)
			{
				case "ÍÎÌÅĞ_ÊÀĞÒÎ×ÊÀ":
					card_number	= (long)val;
					break;
				case "ÃÎÄ_ÊÀĞÒÎ×ÊÀ":
					card_year	= (int)val;
					break;
				case "ÅÑÒÜ_ÂĞÅÌß_ÇÀÅÇÄ":
					is_goin_time	= (bool)val;
					break;
				case "ÅÑÒÜ_ÂĞÅÌß_ÍÀ×ÀËÎ":
					is_begin_time	= (bool)val;
					break;
				case "ÅÑÒÜ_ÂĞÅÌß_ÎÊÎÍ×ÀÍÈÅ":
					is_end_time	= (bool)val;
					break;
				case "ÅÑÒÜ_ÂĞÅÌß_ÂÛÅÇÄ":
					is_goout_time	= (bool)val;
					break;
				case "ÂĞÅÌß_ÇÀÅÇÄ":
					goin_time	= (DateTime)val;
					break;
				case "ÂĞÅÌß_ÍÀ×ÀËÎ":
					begin_time	= (DateTime)val;
					break;
				case "ÂĞÅÌß_ÎÊÎÍ×ÀÍÈÅ":
					end_time	= (DateTime)val;
					break;
				case "ÂĞÅÌß_ÂÛÅÇÄ":
					goout_time	= (DateTime)val;
					break;
				case "ÍÅÇÀÅÇÄ":
					notime	= (bool)val;
					break;
				case "ÏÀÓÇÀ":
					pause	= (bool)val;
					break;
				default:
					break;
			}
		}

		public object GetData(string data)
		{
			switch(data)
			{
				case "ÍÎÌÅĞ_ÊÀĞÒÎ×ÊÀ":
					return (object)(long)card_number;
				case "ÃÎÄ_ÊÀĞÒÎ×ÊÀ":
					return (object)(int)card_year;
				case "ÅÑÒÜ_ÂĞÅÌß_ÇÀÅÇÄ":
					return (object)(bool)is_goin_time;
				case "ÅÑÒÜ_ÂĞÅÌß_ÍÀ×ÀËÎ":
					return (object)(bool)is_begin_time;
				case "ÅÑÒÜ_ÂĞÅÌß_ÎÊÎÍ×ÀÍÈÅ":
					return (object)(bool)is_end_time;
				case "ÅÑÒÜ_ÂĞÅÌß_ÂÛÅÇÄ":
					return (object)(bool)is_goout_time;
				case "ÂĞÅÌß_ÇÀÅÇÄ":
					return (object)(DateTime)goin_time;
				case "ÂĞÅÌß_ÍÀ×ÀËÎ":
					return (object)(DateTime)begin_time;
				case "ÂĞÅÌß_ÎÊÎÍ×ÀÍÈÅ":
					return (object)(DateTime)end_time;
				case "ÂĞÅÌß_ÂÛÅÇÄ":
					return (object)(DateTime)goout_time;
				case "ÍÅÇÀÅÇÄ":
					return (object)(bool)notime;
				case "ÏÀÓÇÀ":
					return (object)(bool)pause;
				default:
					return (object)null;
			}
		}
	}
}
