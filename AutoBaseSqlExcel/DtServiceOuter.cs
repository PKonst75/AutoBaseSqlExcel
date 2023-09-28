using System;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DtServiceOuter.
	/// </summary>
	public class DtServiceOuter
	{
		long	code;
		string	name;
		string	address;

		public DtServiceOuter()
		{
			code		= 0;
			name		= "";
			address		= "";
		}

		public object GetData(string data)
		{
			switch(data)
			{
				case "ÊÎÄ_ÑÅĞÂÈÑ":
					return (object)(long)code;
				case "ÍÀÈÌÅÍÎÂÀÍÈÅ":
					return (object)(string)name;
				case "ÀÄĞÅÑ":
					return (object)(string)address;
				default:
					return (object)null;
			}
		}

		public void SetData(string data, object val)
		{
			switch(data)
			{
				case "ÊÎÄ_ÑÅĞÂÈÑ":
					code = (long)val;
					break;
				case "ÍÀÈÌÅÍÎÂÀÍÈÅ":
					name = (string)val;
					name.Trim();
					break;
				case "ÀÄĞÅÑ":
					address = (string)val;
					address.Trim();
					break;
				default:
					break;
			}
		}

		public void SetLVItem(ListViewItem item)
		{
			string txt = "";
			item.SubItems.Clear();		// ×òîáû ñäåëàòü îäíîòèïíûì äîáàâëåíèå è èçìåíåíèå

			item.Tag				= this.code;
			item.Text				= this.name;
			item.SubItems.Add(this.address);
		}
	}
}
