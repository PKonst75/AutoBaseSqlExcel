using System;
using System.Windows.Forms;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DtPartnerContact.
	/// </summary>
	public class DtPartnerContact
	{
		long	code_partner;
		long	code;
		string	type;
		string	sort;
		string	contact;
		string	comment;

		public DtPartnerContact()
		{
			code_partner	= 0;
			code			= 0;
			type			= "";
			sort			= "";
			contact			= "";
			comment			= "";
		}

		public object GetData(string data)
		{
			switch(data)
			{
				case "ÑÑÛËÊÀ_ÊÎÄ_ÊÎÍÒĞÀÃÅÍÒ_ÊÎÍÒÀÊÒ":
					return (object)(long)code_partner;
				case "ÊÎÄ_ÊÎÍÒÀÊÒ":
					return (object)(long)code;
				case "ÒÈÏ_ÊÎÍÒÀÊÒ":
					return (object)(string)type;
				case "ÂÈÄ_ÊÎÍÒÀÊÒ":
					return (object)(string)sort;
				case "ÊÎÍÒÀÊÒ":
					return (object)(string)contact;
				case "ÏĞÈÌÅ×ÀÍÈÅ_ÊÎÍÒÀÊÒ":
					return (object)(string)comment;
				default:
					return (object)null;
			}
		}

		public void SetData(string data, object val)
		{
			switch(data)
			{
				case "ÑÑÛËÊÀ_ÊÎÄ_ÊÎÍÒĞÀÃÅÍÒ_ÊÎÍÒÀÊÒ":
					code_partner = (long)val;
					break;
				case "ÊÎÄ_ÊÎÍÒÀÊÒ":
					code = (long)val;
					break;
				case "ÒÈÏ_ÊÎÍÒÀÊÒ":
					type = (string)val;
					break;
				case "ÂÈÄ_ÊÎÍÒÀÊÒ":
					sort = (string)val;
					break;
				case "ÊÎÍÒÀÊÒ":
					contact = (string)val;
					break;
				case "ÏĞÈÌÅ×ÀÍÈÅ_ÊÎÍÒÀÊÒ":
					comment = (string)val;
					break;
				default:
					break;
			}
		}

		public bool CheckData(string data)
		{
			switch(data)
			{
				case "ÑÑÛËÊÀ_ÊÎÄ_ÊÎÍÒĞÀÃÅÍÒ_ÊÎÍÒÀÊÒ":
					if(code_partner <= 0) return false;
					break;
				case "ÊÎÍÒÀÊÒ":
					if(contact.Length <= 0) return false;
					break;
				default:
					return false;
			}
			return true;
		}

		public void SetLVItem(ListViewItem item)
		{
			item.SubItems.Clear();		// ×òîáû ñäåëàòü îäíîòèïíûì äîáàâëåíèå è èçìåíåíèå

			item.Tag				= this.code;
			item.Text				= this.contact;
			item.SubItems.Add(this.type);
			item.SubItems.Add(this.sort);
			item.SubItems.Add(this.comment);
		}

		public string ContactTxt
		{
			get
			{
				return this.sort +"/" + this.type + "   " + this.contact + "  " + this.comment;
			}
		}

		public static ArrayList MakeContacts(string old)
		{
			DtPartnerContact contact;
			ArrayList array;
			old = old.Trim();
			if(old.Length == 0) return null;
			int count = 0;
			int index = 0;
			index = old.IndexOf(" ", 0);
			while(index != -1)
			{
				index = old.IndexOf(" ", index + 1);
				count++;
			}
			array = new ArrayList();
			if(count == 0 || count > 1)
			{
				contact = new DtPartnerContact();
				contact.SetData("ÒÈÏ_ÊÎÍÒÀÊÒ", "òåëåôîí");
				old = old.Replace("-", "");
				old = old.Replace(",", "");
				old = old.Replace(".", "");
				old = old.Replace("/", "");
				contact.contact = old;
				array.Add(contact);
				return array;
			}
			// Åñëè ğîâíî îäèí ïğîáåë - äåëèì íà äâå ÷àñòè
			char[] param = new char[1];
			param[0] = ' ';
			string[] phones = old.Split(param, 2);
			for(int i = 0; i < 2; i++)
			{
				contact = new DtPartnerContact();
				contact.SetData("ÒÈÏ_ÊÎÍÒÀÊÒ", "òåëåôîí");
				phones[i] = phones[i].Replace("-", "");
				phones[i] = phones[i].Replace(",", "");
				phones[i] = phones[i].Replace(".", "");
				phones[i] = phones[i].Replace("/", "");
				contact.contact = phones[i];
				array.Add(contact);
			}
			return array;
		}
	}
}
