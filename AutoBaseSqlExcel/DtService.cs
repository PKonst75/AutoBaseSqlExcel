using System;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DtService.
	/// </summary>
	public class DtService
	{
		public DtService()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static string TrancateString(string data, int length)
		{
			return data.Substring(0, 1024);
		}
		public static string Trim(string data)
		{
			return data.Trim();
		}
		public static long ToLong(string val, string message)
		{
			long data;
			string dataTxt = val.Trim();
			if(dataTxt.Length != 0)
			{
				try
				{
					data = (long)Convert.ToInt64(dataTxt);
				}
				catch(Exception)
				{
					Db.SetDataWarning(message);
					return 0;	
				}
			}
			else data = 0;
			return data;
		}
		public static int ToInt(string val, string message)
		{
			int data;
			string dataTxt = val.Trim();
			if(dataTxt.Length != 0)
			{
				try
				{
					data = (int)Convert.ToInt32(dataTxt);
				}
				catch(Exception)
				{
					Db.SetDataWarning(message);
					return 0;	
				}
			}
			else data = 0;
			return data;
		}
		public static string FirstUpper(string val)
		{
			if(val == null) return "";
			string res		= val;
			if(res.Length == 0) return res;
			string first		= res.Substring(0, 1);
			string first_upper	= first.ToUpper();
			if(first == first_upper) return res;
			if(res.Length == 1) return first_upper;
			res = first_upper + res.Substring(1, res.Length -1);
			return res;
		}
	}
}
