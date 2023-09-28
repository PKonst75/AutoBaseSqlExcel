using System;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DtAutoAlarm.
	/// </summary>
	public class DtAutoAlarm
	{
		long		code_auto;
		long		flag;
		long		code_alarm;
		long		code_service;
		long		code_reason;
		DateTime	date;
		long		version;

		public DtAutoAlarm()
		{
			code_auto		= 0;
			flag			= 0;
			code_alarm		= 0;
			code_service	= 0;
			code_reason		= 0;
			date			= DateTime.Now;
			version			= 0;
		}

		public object GetData(string data)
		{
			switch(data)
			{
				case "���_����������":
					return (object)(long)code_auto;
				case "����_���������":
					return (object)(long)flag;
				case "���_������������":
					return (object)(long)code_alarm;
				case "���_������":
					return (object)(long)code_service;
				case "���_�������":
					return (object)(long)code_reason;
				case "����_���������":
					return (object)(DateTime)date;
				case "������":
					return (object)(long)version;
				default:
					return (object)null;
			}
		}

		public void SetData(string data, object val)
		{
			switch(data)
			{
				case "���_����������":
					code_auto = (long)val;
					break;
				case "����_���������":
					flag = (long)val;
					break;
				case "���_������������":
					code_alarm = (long)val;
					break;
				case "���_������":
					code_service = (long)val;
					break;
				case "���_�������":
					code_reason = (long)val;
					break;
				case "����_���������":
					date = (DateTime)val;
					break;
				case "������":
					version = (long)val;
					break;
				default:
					break;
			}
		}
	}
}
