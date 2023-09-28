using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DtCardTimePause.
	/// </summary>
	public class DtCardTimePause
	{
		protected long		card_number;
		protected int		card_year;
		protected bool		is_begin_time;
		protected bool		is_end_time;
		protected DateTime	begin_time;
		protected DateTime	end_time;
		protected long		reason;

		public DtCardTimePause()
		{
			card_number		= 0L;
			card_year		= 0;
			is_begin_time	= false;
			is_end_time		= false;
			begin_time		= DateTime.Now;
			end_time		= DateTime.Now;
			reason			= 0L;
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
				case "ÅÑÒÜ_ÂĞÅÌß_ÍÀ×ÀËÎ":
					is_begin_time	= (bool)val;
					break;
				case "ÅÑÒÜ_ÂĞÅÌß_ÎÊÎÍ×ÀÍÈÅ":
					is_end_time	= (bool)val;
					break;
				case "ÂĞÅÌß_ÍÀ×ÀËÎ":
					begin_time	= (DateTime)val;
					break;
				case "ÂĞÅÌß_ÎÊÎÍ×ÀÍÈÅ":
					end_time	= (DateTime)val;
					break;
				case "ÏĞÈ×ÈÍÀ":
					reason	= (long)val;
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
				case "ÅÑÒÜ_ÂĞÅÌß_ÍÀ×ÀËÎ":
					return (object)(bool)is_begin_time;
				case "ÅÑÒÜ_ÂĞÅÌß_ÎÊÎÍ×ÀÍÈÅ":
					return (object)(bool)is_end_time;
				case "ÂĞÅÌß_ÍÀ×ÀËÎ":
					return (object)(DateTime)begin_time;
				case "ÂĞÅÌß_ÎÊÎÍ×ÀÍÈÅ":
					return (object)(DateTime)end_time;
				case "ÏĞÈ×ÈÍÀ":
					return (object)(long)reason;
				default:
					return (object)null;
			}
		}
	}
}
