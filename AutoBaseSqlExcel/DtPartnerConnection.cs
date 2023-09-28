using System;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DtPartnerConnection.
	/// </summary>
	public class DtPartnerConnection
	{
		long		code_partner;
		DateTime	date;
		string		contact;
		string		comment;

		string		tmp_partner_title;

		public DtPartnerConnection(DtPartnerContact partner_contact)
		{
			code_partner	= (long)partner_contact.GetData("ÑÑÛËÊÀ_ÊÎÄ_ÊÎÍÒĞÀÃÅÍÒ_ÊÎÍÒÀÊÒ");
			date			= DateTime.Now;
			contact			= (string)partner_contact.GetData("ÊÎÍÒÀÊÒ");
			comment			= "";
			// Äîãğóæàåì êîíòğàãåíòà
			DtPartner	partner = DbSqlPartner.Find(code_partner);
			if(partner != null)
				tmp_partner_title	= (string)partner.GetData("ÍÀÈÌÅÍÎÂÀÍÈÅ_ÊĞÀÒÊÎÅ");
		}

		public DtPartnerConnection()
		{
			code_partner	= 0;
			date			= DateTime.Now;
			contact			= "";
			comment			= "";
			
			
			tmp_partner_title	= "";
		}

		public object GetData(string data)
		{
			switch(data)
			{
				case "ÑÑÛËÊÀ_ÊÎÄ_ÊÎÍÒĞÀÃÅÍÒ":
					return (object)(long)code_partner;
				case "ÄÀÒÀ":
					return (object)(DateTime)date;
				case "ÊÎÍÒÀÊÒ":
					return (object)(string)contact;
				case "ÖÅËÜ":
					return (object)(string)comment;
				
				case "ÊÎÍÒĞÀÃÅÍÒ":
					return (object)(string)tmp_partner_title;
				default:
					return (object)null;
			}
		}

		public void SetData(string data, object val)
		{
			switch(data)
			{
				case "ÑÑÛËÊÀ_ÊÎÄ_ÊÎÍÒĞÀÃÅÍÒ":
					code_partner = (long)val;
					break;
				case "ÄÀÒÀ":
					date = (DateTime)val;
					break;
				case "ÊÎÍÒÀÊÒ":
					contact = (string)val;
					break;
				case "ÖÅËÜ":
					comment = (string)val;
					break;

				case "ÊÎÍÒĞÀÃÅÍÒ":
					tmp_partner_title = (string)val;
					break;
				default:
					break;
			}
		}

		public bool CheckData(string data)
		{
			return true;
		}

		public void SetLVItem(ListViewItem item)
		{
			item.SubItems.Clear();		// ×òîáû ñäåëàòü îäíîòèïíûì äîáàâëåíèå è èçìåíåíèå

			item.Tag				= this.date;
			item.Text				= this.date.ToString();
			item.SubItems.Add(this.contact);
			item.SubItems.Add(this.tmp_partner_title);
			item.SubItems.Add(this.comment);
		}
	}
}
