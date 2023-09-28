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
				case "�����_��������":
					card_number	= (long)val;
					break;
				case "���_��������":
					card_year	= (int)val;
					break;
				case "����_�����_������":
					is_begin_time	= (bool)val;
					break;
				case "����_�����_���������":
					is_end_time	= (bool)val;
					break;
				case "�����_������":
					begin_time	= (DateTime)val;
					break;
				case "�����_���������":
					end_time	= (DateTime)val;
					break;
				case "�������":
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
				case "�����_��������":
					return (object)(long)card_number;
				case "���_��������":
					return (object)(int)card_year;
				case "����_�����_������":
					return (object)(bool)is_begin_time;
				case "����_�����_���������":
					return (object)(bool)is_end_time;
				case "�����_������":
					return (object)(DateTime)begin_time;
				case "�����_���������":
					return (object)(DateTime)end_time;
				case "�������":
					return (object)(long)reason;
				default:
					return (object)null;
			}
		}
	}
}
