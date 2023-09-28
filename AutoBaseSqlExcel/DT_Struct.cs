using System;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DT_Struct.
	/// </summary>
	public class DT_Struct
	{
		public class DT
		{
			public string Database_Name;
			public string Database_Parameter;
			public string Data_Type;
			public object data;

			public DT()
			{
			}

			public void SetData(object val)
			{
				data = val;
			}
		}

		public ArrayList datas;

		public DT_Struct()
		{
			datas = new ArrayList();
		}

		public void AddLong(string name, string parameter, long data)
		{
			DT element = new DT_Struct.DT();
			element.Data_Type			= "LONG";
			element.Database_Name		= name;
			element.Database_Parameter	= parameter;
			element.data				= (object)data;

			datas.Add(element);
		}

		public void AddString(string name, string parameter, string data)
		{
			DT element = new DT_Struct.DT();
			element.Data_Type			= "LONG";
			element.Database_Name		= name;
			element.Database_Parameter	= parameter;
			element.data				= (object)data;

			datas.Add(element);
		}

		public long FindLong(string name)
		{
			DT match = new DT();
			match.Data_Type = "NULL";
			foreach(object o in datas)
			{
				DT element = (DT)o;
				if (element.Database_Name == name)
				{
					match = element;
				}
			}

			if(match.Data_Type == "NULL") return 0;
			if(match.Data_Type != "LONG") return 0;
			if(match.data == null) return 0;
			return (long)match.data;
		}

		public string FindString(string name)
		{
			DT match = new DT();
			match.Data_Type = "NULL";
			foreach(object o in datas)
			{
				DT element = (DT)o;
				if (element.Database_Name == name)
				{
					match = element;
				}
			}

			if(match.Data_Type == "NULL") return "";
			if(match.Data_Type != "STRING") return "";
			if(match.data == null) return "";
			return (string)match.data;
		}

		public void ChangeLong(string name, long data)
		{
			DT match = new DT();
			match.Data_Type = "NULL";
			object matcho = null;
			foreach(object o in datas)
			{
				DT element = (DT)o;
				if (element.Database_Name == name)
				{
					match = element;
					matcho = o;
				}
			}

			datas.Remove(matcho);

			if(match.Data_Type == "NULL") return;
			if(match.Data_Type != "LONG") return;
			if(match.data == null) return;
			match.data = data;
			
			datas.Add(match);

		}

		public void ChangeLong1(string name, long data)
		{
			foreach(object o in datas)
			{
				DT element = (DT)o;
				if (element.Database_Name == name)
				{
					int i = datas.IndexOf(o);
					if( ((DT)datas[i]).Data_Type != "LONG" ) return;
					((DT)datas[i]).SetData(data);
				}
			}
		}
	}
}
