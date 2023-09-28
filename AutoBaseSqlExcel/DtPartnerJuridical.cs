using System;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DtPartnerJuridical.
	/// </summary>
	public class DtPartnerJuridical
	{
		public class DtPartnerJuridicalTxt
		{
			public readonly string name;
			public readonly string address;
			public DtPartnerJuridicalTxt(DtPartnerJuridical partner)
            {
				name = partner.name_juridical;
				address = partner.address_juridical;
            }
		}
		string		name_juridical;
		string		address_juridical;

		string		address_fact;
		string		contact;
		
		public DtPartnerJuridical()
		{
			name_juridical			= "";
			address_juridical		= "";

			address_fact			= "";
			contact					= "";
		}

		public object GetData(string data)
		{
			switch(data)
			{
				case "ÍÀÈÌÅÍÎÂÀÍÈÅ_ŞĞÈÄÈ×ÅÑÊÎÅ":
					return (object)(string)name_juridical;
				case "ÀÄĞÅÑ_ŞĞÈÄÈ×ÅÑÊÈÉ":
					return (object)(string)address_juridical;
				case "ÀÄĞÅÑ_ÔÀÊÒÈ×ÅÑÊÈÉ":
					return (object)(string)address_fact;
				case "ÊÎÍÒÀÊÒ":
					return (object)(string)contact;
				default:
					return (object)null;
			}
		}

		public void SetData(string data, object val)
		{
			switch(data)
			{
				case "ÍÀÈÌÅÍÎÂÀÍÈÅ_ŞĞÈÄÈ×ÅÑÊÎÅ":
					name_juridical = (string)val;
					name_juridical = name_juridical.Trim();
					break;
				case "ÀÄĞÅÑ_ŞĞÈÄÈ×ÅÑÊÈÉ":
					address_juridical = (string)val;
					break;
				case "ÀÄĞÅÑ_ÔÀÊÒÈ×ÅÑÊÈÉ":
					address_fact = (string)val;
					break;
				case "ÊÎÍÒÀÊÒ":
					contact = (string)val;
					break;
				default:
					break;
			}
		}

		public bool CheckData(string data)
		{
			switch(data)
			{
				case "ÍÀÈÌÅÍÎÂÀÍÈÅ_ŞĞÈÄÈ×ÅÑÊÎÅ":
					if(name_juridical.Length == 0) return false;
					break;
				default:
					return false;
			}
			return true;
		}

		public void SetLVItem(ListViewItem item)
		{
			// Òîëüêî äîïîëíèòåëüíûå ïîëÿ	
			item.SubItems.Add(name_juridical);
		}
	}
}
