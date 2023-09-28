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
				case "йнд_юбрнлнахкэ":
					return (object)(long)code_auto;
				case "ткюц_сярюмнбйю":
					return (object)(long)flag;
				case "йнд_яхцмюкхгюжхъ":
					return (object)(long)code_alarm;
				case "йнд_яепбхя":
					return (object)(long)code_service;
				case "йнд_опхвхмю":
					return (object)(long)code_reason;
				case "дюрю_сярюмнбйю":
					return (object)(DateTime)date;
				case "бепяхъ":
					return (object)(long)version;
				default:
					return (object)null;
			}
		}

		public void SetData(string data, object val)
		{
			switch(data)
			{
				case "йнд_юбрнлнахкэ":
					code_auto = (long)val;
					break;
				case "ткюц_сярюмнбйю":
					flag = (long)val;
					break;
				case "йнд_яхцмюкхгюжхъ":
					code_alarm = (long)val;
					break;
				case "йнд_яепбхя":
					code_service = (long)val;
					break;
				case "йнд_опхвхмю":
					code_reason = (long)val;
					break;
				case "дюрю_сярюмнбйю":
					date = (DateTime)val;
					break;
				case "бепяхъ":
					version = (long)val;
					break;
				default:
					break;
			}
		}
	}
}
