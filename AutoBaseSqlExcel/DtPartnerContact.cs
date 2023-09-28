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
				case "������_���_����������_�������":
					return (object)(long)code_partner;
				case "���_�������":
					return (object)(long)code;
				case "���_�������":
					return (object)(string)type;
				case "���_�������":
					return (object)(string)sort;
				case "�������":
					return (object)(string)contact;
				case "����������_�������":
					return (object)(string)comment;
				default:
					return (object)null;
			}
		}

		public void SetData(string data, object val)
		{
			switch(data)
			{
				case "������_���_����������_�������":
					code_partner = (long)val;
					break;
				case "���_�������":
					code = (long)val;
					break;
				case "���_�������":
					type = (string)val;
					break;
				case "���_�������":
					sort = (string)val;
					break;
				case "�������":
					contact = (string)val;
					break;
				case "����������_�������":
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
				case "������_���_����������_�������":
					if(code_partner <= 0) return false;
					break;
				case "�������":
					if(contact.Length <= 0) return false;
					break;
				default:
					return false;
			}
			return true;
		}

		public void SetLVItem(ListViewItem item)
		{
			item.SubItems.Clear();		// ����� ������� ���������� ���������� � ���������

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
				contact.SetData("���_�������", "�������");
				old = old.Replace("-", "");
				old = old.Replace(",", "");
				old = old.Replace(".", "");
				old = old.Replace("/", "");
				contact.contact = old;
				array.Add(contact);
				return array;
			}
			// ���� ����� ���� ������ - ����� �� ��� �����
			char[] param = new char[1];
			param[0] = ' ';
			string[] phones = old.Split(param, 2);
			for(int i = 0; i < 2; i++)
			{
				contact = new DtPartnerContact();
				contact.SetData("���_�������", "�������");
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
