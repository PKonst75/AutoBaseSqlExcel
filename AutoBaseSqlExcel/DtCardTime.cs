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
				case "�����_��������":
					card_number	= (long)val;
					break;
				case "���_��������":
					card_year	= (int)val;
					break;
				case "����_�����_�����":
					is_goin_time	= (bool)val;
					break;
				case "����_�����_������":
					is_begin_time	= (bool)val;
					break;
				case "����_�����_���������":
					is_end_time	= (bool)val;
					break;
				case "����_�����_�����":
					is_goout_time	= (bool)val;
					break;
				case "�����_�����":
					goin_time	= (DateTime)val;
					break;
				case "�����_������":
					begin_time	= (DateTime)val;
					break;
				case "�����_���������":
					end_time	= (DateTime)val;
					break;
				case "�����_�����":
					goout_time	= (DateTime)val;
					break;
				case "�������":
					notime	= (bool)val;
					break;
				case "�����":
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
				case "�����_��������":
					return (object)(long)card_number;
				case "���_��������":
					return (object)(int)card_year;
				case "����_�����_�����":
					return (object)(bool)is_goin_time;
				case "����_�����_������":
					return (object)(bool)is_begin_time;
				case "����_�����_���������":
					return (object)(bool)is_end_time;
				case "����_�����_�����":
					return (object)(bool)is_goout_time;
				case "�����_�����":
					return (object)(DateTime)goin_time;
				case "�����_������":
					return (object)(DateTime)begin_time;
				case "�����_���������":
					return (object)(DateTime)end_time;
				case "�����_�����":
					return (object)(DateTime)goout_time;
				case "�������":
					return (object)(bool)notime;
				case "�����":
					return (object)(bool)pause;
				default:
					return (object)null;
			}
		}
	}
}
