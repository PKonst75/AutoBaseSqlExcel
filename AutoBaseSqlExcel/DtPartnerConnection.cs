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
			code_partner	= (long)partner_contact.GetData("������_���_����������_�������");
			date			= DateTime.Now;
			contact			= (string)partner_contact.GetData("�������");
			comment			= "";
			// ��������� �����������
			DtPartner	partner = DbSqlPartner.Find(code_partner);
			if(partner != null)
				tmp_partner_title	= (string)partner.GetData("������������_�������");
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
				case "������_���_����������":
					return (object)(long)code_partner;
				case "����":
					return (object)(DateTime)date;
				case "�������":
					return (object)(string)contact;
				case "����":
					return (object)(string)comment;
				
				case "����������":
					return (object)(string)tmp_partner_title;
				default:
					return (object)null;
			}
		}

		public void SetData(string data, object val)
		{
			switch(data)
			{
				case "������_���_����������":
					code_partner = (long)val;
					break;
				case "����":
					date = (DateTime)val;
					break;
				case "�������":
					contact = (string)val;
					break;
				case "����":
					comment = (string)val;
					break;

				case "����������":
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
			item.SubItems.Clear();		// ����� ������� ���������� ���������� � ���������

			item.Tag				= this.date;
			item.Text				= this.date.ToString();
			item.SubItems.Add(this.contact);
			item.SubItems.Add(this.tmp_partner_title);
			item.SubItems.Add(this.comment);
		}
	}
}
