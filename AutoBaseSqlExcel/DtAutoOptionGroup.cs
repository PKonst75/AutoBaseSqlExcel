using System;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DtAutoOptionGroup.
	/// </summary>
	public class DtAutoOptionGroup
	{
		public long code;
		public string name;
		

		public DtAutoOptionGroup()
		{
			code = 0L;
			name = "";
		}

		public object GetData(string data)
		{
			switch(data)
			{
				case "���_������":
					return (object)(long)code;
				case "������������_������":
					return (object)(string)name;
				default:
					return (object)null;
			}
		}

		public void SetData(string data, object val)
		{
			switch(data)
			{
				case "���_������":
					code = (long)val;
					break;
				case "������������_������":
					name = (string)val;
					name.Trim();
					break;
				default:
					break;
			}
		}

		public override string ToString()
		{
			return this.name;
		}

	}
}
