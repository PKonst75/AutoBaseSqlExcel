using System;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DtListAlarm.
	/// </summary>
	public class DtListAlarm
	{
		long		code;
		string		name;

		public DtListAlarm()
		{
			code		= 0;
			name	= "";
		}

		public object GetData(string data)
		{
			switch(data)
			{
				case "ÊÎÄ_ÑÈÃÍÀËÈÇÀÖÈß":
					return (object)(long)code;
				case "ÍÀÈÌÅÍÎÂÀÍÈÅ":
					return (object)(string)name;
				default:
					return (object)null;
			}
		}

		public void SetData(string data, object val)
		{
			switch(data)
			{
				case "ÊÎÄ_ÑÈÃÍÀËÈÇÀÖÈß":
					code = (long)val;
					break;
				case "ÍÀÈÌÅÍÎÂÀÍÈÅ":
					name = (string)val;
					name.Trim();
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
		}
	}
}
