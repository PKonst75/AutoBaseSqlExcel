using System;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DtAutoOptionVariant.
	/// </summary>
	public class DtAutoOptionVariant
	{
		public long code;
		public long code_option;
		public string name;

		public DtAutoOptionVariant()
		{
			code = 0L;
			code_option = 0L;
			name = "";
		}

		public object GetData(string data)
		{
			switch(data)
			{
				case "ÊÎÄ":
					return (object)(long)code;
				case "ÂÀĞÈÀÍÒ_ÍÀÈÌÅÍÎÂÀÍÈÅ":
					return (object)(string)name;
				case "ÑÑÛËÊÀ_ÊÎÄ_ÎÏÖÈß":
					return (object)(long)code_option;
				default:
					return (object)null;
			}
		}

		public void SetData(string data, object val)
		{
			switch(data)
			{
				case "ÊÎÄ":
					code = (long)val;
					break;
				case "ÂÀĞÈÀÍÒ_ÍÀÈÌÅÍÎÂÀÍÈÅ":
					name = (string)val;
					name.Trim();
					break;
				case "ÑÑÛËÊÀ_ÊÎÄ_ÎÏÖÈß":
					code_option = (long)val;
					break;
				default:
					break;
			}
		}

		public void SetLVItem(ListViewItem item)
		{
			item.SubItems.Clear();		// ×òîáû ñäåëàòü îäíîòèïíûì äîáàâëåíèå è èçìåíåíèå

			item.Tag				= this.code;
			item.Text				= this.name;
		}
	}
}
