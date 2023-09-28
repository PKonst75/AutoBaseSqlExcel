using System;
using System.Windows.Forms;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// �������� �����������
	/// </summary>
	public class DtPartner:Dt, IDt
	{
		public class DtPartnerTxt
        {
			public readonly string name;
			public readonly string short_name;
			public readonly string address;
			public readonly string phone;
			public readonly string email;
			public DtPartnerTxt(DtPartner partner)
            {
                if (partner.IsJuridical())
                {
					DtPartnerJuridical.DtPartnerJuridicalTxt juridicalTxt = new DtPartnerJuridical.DtPartnerJuridicalTxt(partner.partner_juridical);
					name = juridicalTxt.name;
					address = juridicalTxt.address;

                }
				else
                {
					DtPartnerPerson.DtPartnerPersonTxt personTxt = new DtPartnerPerson.DtPartnerPersonTxt(partner.partner_person);
					name = personTxt.full_name;
					short_name = personTxt.short_name;
					address = personTxt.address;
                }
				phone = partner.GetPhone();
				email = partner.GetMail();
            }
        }
		long			code;			// ��� �����������
		bool			juridical;		// ������� ������������ ����
		string			title;			// ������������
		string			comment;		// ����������
		string			inn;			// ���

		DtPartnerPerson		partner_person;		// �������� ����������� ����
		DtPartnerJuridical	partner_juridical;  // �������� ������������ ����
		DtPartnerProperty extDtpartnerProperty; // ������� ��������� ������ - �������� �����������

		// ����� ������������
		string			del_phone;			// �������� ���������, ����� ������������
		string			del_contact_phone;	// �������� ���������, ����� ������������

		public DtPartner()
		{
			code				= 0;
			juridical			= false;
			title				= "";
			comment				= "";
			inn					= "";

			partner_person		= null;
			partner_juridical	= null;

			del_phone			= "";
			del_contact_phone	= "";

			extDtpartnerProperty = null;
		}

		public object GetData(string data)
		{
			switch(data)
			{
				case "���_����������":
					return (object)(long)code;
				case "������������_�������":
					return (object)(string)title;
				case "�����������_����":
					return (object)(bool)juridical;
				case "����������":
					return (object)(string)comment;
				case "���":
					return (object)(string)inn;
				case "����������":
					return (object)(DtPartnerPerson)partner_person;
				case "�����������":
					return (object)(DtPartnerJuridical)partner_juridical;
				// ����� ������������
				case "�������":
					return (object)(string)del_phone;
				case "�������_�������":
					return (object)(string)del_contact_phone;
				default:
					return (object)null;
			}
		}

		public void SetData(string data, object val)
		{
			switch(data)
			{
				case "���_����������":
					code = (long)val;
					break;
				case "������������_�������":
					title = (string)val;
					title = title.Trim();
					break;
				case "�����������_����":
					juridical = (bool)val;
					break;
				case "����������":
					comment = (string)val;
					comment = comment.Trim();
					break;
				case "���":
					inn = (string)val;
					inn = inn.Trim();
					break;
				case "����������":
					partner_person = (DtPartnerPerson)val;
					break;
				case "�����������":
					partner_juridical = (DtPartnerJuridical)val;
					break;
				// ����� ������������
				case "�������":
					del_phone = (string)val;
					break;
				case "�������_�������":
					del_contact_phone = (string)val;
					break;
				default:
					break;
			}
		}

		public bool CheckData(string data)
		{
			switch(data)
			{
				case "������������_�������":
					if(title.Length == 0) return false;
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
			item.Text				= this.title;
			if(juridical == false)
			{
				partner_person.SetLVItem(item);
			}
			else
			{
				partner_juridical.SetLVItem(item);
			}
		}

		public string GetTitle()
		{
			string title;
			// ��������� ������ � ��� ��� ������ ������������
			if(juridical == false)
			{
				title = (string)partner_person.GetData("�������") + " " + partner_person.GetData("���") + " " + (string)partner_person.GetData("��������");
			}
			else
			{
				title = (string)partner_juridical.GetData("������������_�����������");
			}
			return title;
		}
        public string GetTitleShort()
        {
            string title;
            
            // ��������� ������ � ��� ��� ������ ������������
            if (juridical == false)
            {
                string name = (string)partner_person.GetData("���");
                string surname = (string)partner_person.GetData("��������");
                if (name.Length > 0) name = name.Substring(0, 1);
                if (surname.Length > 0) surname = surname.Substring(0, 1);
                if (name.Length == 1) name = name + ".";
                if (surname.Length == 1) surname = surname + ".";
                title = (string)partner_person.GetData("�������") + " " + name + surname;
            }
            else
            {
                title = (string)partner_juridical.GetData("������������_�����������");
            }
            return title;
        }
		public string GetAddress()
		{
			string txt;
			// ��������� ������ � �������� ��� ��. ������
			if(juridical == false)
			{
				txt = (string)partner_person.GetData("�����_��������");
			}
			else
			{
				txt = (string)partner_juridical.GetData("�����_�����������");
			}
			return txt;
		}
		public string GetPhone()
		{
			string txt = "";
			// ��������� ������ � ���������� ��������
			// ��� ������ ������� ��������� ������ �� ����� �����
			ArrayList contacts = new ArrayList();
			DbSqlPartnerContact.SelectInArray(contacts, this.code);
			foreach(DtPartnerContact contact in contacts)
			{
				// ����� ������ ��������
				if((string)contact.GetData("���_�������") == "�������")
				{
					if (txt != "") txt	+= "; ";
					txt += contact.GetData("�������");
				}
			}
			if (txt != "") return txt;

			// ���� ��� ����� ������ - �������� �� ������
			if(juridical == false)
			{
				txt = del_phone;
			}
			else
			{
				txt = del_contact_phone;
			}
			return txt;
		}

        public string GetMail()
        {
            string txt = "";
            // ��������� ������ � ���������� ��������
            // ��� ������ ������� ��������� ������ �� ����� �����
            ArrayList contacts = new ArrayList();
            DbSqlPartnerContact.SelectInArray(contacts, this.code);
            foreach (DtPartnerContact contact in contacts)
            {
                // ����� ������ e-mail
                if ((string)contact.GetData("���_�������") == "E-MAIL")
                {
                    if (txt != "") txt += "; ";
                    txt += contact.GetData("�������");
                }
            }
            return txt;
        }

		public string GetTitleName()
		{
			string title;
			// ��������� ������ � ��� ��� ������ ������������
			if(juridical == false)
			{
				title = partner_person.GetData("���") + " " + (string)partner_person.GetData("��������");
			}
			else
			{
				title = "";
			}
			return title;
		}

		public bool IsJuridical()
		{
			return juridical;
		}

		// ��������� ��������� ������ ����������� ����
		public string PersonSurname()
		{
			string surname = "";
			if (!IsJuridical())
            {
				surname = this.partner_person.Surname();
            }
			return surname;
		}

		public string PersonName()
		{
			string name = "";
			if (!IsJuridical())
			{
				name = this.partner_person.Name();
			}
			return name;
		}
		public string PersonSecondname()
		{
			string secondname = "";
			if (!IsJuridical())
			{
				secondname = this.partner_person.Secondname();
			}
			return secondname;
		}
		public string Comment
        {
            get { return comment; }
        }

		public long Code
        {
            get { return code; }
        }

		public bool DataComplicityCheck()
        {
			if(GetMail() == "")
				MessageBox.Show("�������� e-mail �������!");
			return true;
		}
		public void PreselectInfoShow()
        {
			if (Comment.Length != 0)
				MessageBox.Show(Comment, "��������!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}

		public DtPartnerProperty Properties
        {
            get
            {
				if (code == 0) return null;
				if (extDtpartnerProperty == null) 
					extDtpartnerProperty =  DbSqlPartnerProperty.Find(code);
				return extDtpartnerProperty;
			}
        }

		public void ReloadDataFromDatabase()
        {
			DbSqlPartner.LoadFromDatabase(this);
        }
	}
}
