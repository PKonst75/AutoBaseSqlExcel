using System;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DtWorkshop.
	/// </summary>
	public class DtWorkshop:Dt
	{
		long		code;
		string		name;
		string		description;
		string		pass_destination;

		public DtWorkshop()
		{
			code				= 0;
			name				= "";
			description			= "";
			pass_destination	= "";
		}

		public string Name
        {
            get { return name; }
        }

		public object GetData(string data)
		{
			switch(data)
			{
				case "ÊÎÄ_ÖÅÕ":
					return (object)(long)code;
				case "ÍÀÈÌÅÍÎÂÀÍÈÅ_ÖÅÕ":
					return (object)(string)name;
				case "ÏĞÈÌÅÍÅÍÈÅ_ÖÅÕ":
					return (object)(string)description;
				case "ÏĞÎÏÓÑÊ_ÍÀÇÍÀ×ÅÍÈÅ":
					return (object)(string)pass_destination;
				default:
					return (object)null;
			}
		}
		public void SetData(string data, object val)
		{
			switch(data)
			{
				case "ÊÎÄ_ÖÅÕ":
					code = (long)val;
					break;
				case "ÍÀÈÌÅÍÎÂÀÍÈÅ_ÖÅÕ":
					name = (string)val;
					name = name.Trim();
					break;
				case "ÏĞÈÌÅÍÅÍÈÅ_ÖÅÕ":
					description = (string)val;
					break;
				case "ÏĞÎÏÓÑÊ_ÍÀÇÍÀ×ÅÍÈÅ":
					pass_destination = (string)val;
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
		public string Txt()
		{
			return name;
		}

		public override long Code()
		{
			return code;
		}
		public override string Title()
		{
			return name;
		}

		public long CodeWorkshop
		{
			get { return code; }
		}
	}
}
