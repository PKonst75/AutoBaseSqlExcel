using System;
using System.Windows.Forms;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Описание контрагента
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
		long			code;			// Код контрагента
		bool			juridical;		// Признак юридического лица
		string			title;			// Наименование
		string			comment;		// Примечание
		string			inn;			// ИНН

		DtPartnerPerson		partner_person;		// Описание физического лица
		DtPartnerJuridical	partner_juridical;  // Описание юридического лица
		DtPartnerProperty extDtpartnerProperty; // Внешние временные данные - свойства контрагента

		// Будем отказываться
		string			del_phone;			// Описание телефонов, будем отказываться
		string			del_contact_phone;	// Описание телефонов, будем отказываться

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
				case "КОД_КОНТРАГЕНТ":
					return (object)(long)code;
				case "НАИМЕНОВАНИЕ_КРАТКОЕ":
					return (object)(string)title;
				case "ЮРИДИЧЕСКОЕ_ЛИЦО":
					return (object)(bool)juridical;
				case "КОМЕНТАРИЙ":
					return (object)(string)comment;
				case "ИНН":
					return (object)(string)inn;
				case "ФИЗИЧЕСКОЕ":
					return (object)(DtPartnerPerson)partner_person;
				case "ЮРИДИЧЕСКОЕ":
					return (object)(DtPartnerJuridical)partner_juridical;
				// Будем отказываться
				case "ТЕЛЕФОН":
					return (object)(string)del_phone;
				case "КОНТАКТ_ТЕЛЕФОН":
					return (object)(string)del_contact_phone;
				default:
					return (object)null;
			}
		}

		public void SetData(string data, object val)
		{
			switch(data)
			{
				case "КОД_КОНТРАГЕНТ":
					code = (long)val;
					break;
				case "НАИМЕНОВАНИЕ_КРАТКОЕ":
					title = (string)val;
					title = title.Trim();
					break;
				case "ЮРИДИЧЕСКОЕ_ЛИЦО":
					juridical = (bool)val;
					break;
				case "КОМЕНТАРИЙ":
					comment = (string)val;
					comment = comment.Trim();
					break;
				case "ИНН":
					inn = (string)val;
					inn = inn.Trim();
					break;
				case "ФИЗИЧЕСКОЕ":
					partner_person = (DtPartnerPerson)val;
					break;
				case "ЮРИДИЧЕСКОЕ":
					partner_juridical = (DtPartnerJuridical)val;
					break;
				// Будем отказываться
				case "ТЕЛЕФОН":
					del_phone = (string)val;
					break;
				case "КОНТАКТ_ТЕЛЕФОН":
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
				case "НАИМЕНОВАНИЕ_КРАТКОЕ":
					if(title.Length == 0) return false;
					break;
				default:
					return false;
			}
			return true;
		}

		public void SetLVItem(ListViewItem item)
		{
			item.SubItems.Clear();		// Чтобы сделать однотипным добавление и изменение

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
			// Получение данных о ФИО или полном наименовании
			if(juridical == false)
			{
				title = (string)partner_person.GetData("ФАМИЛИЯ") + " " + partner_person.GetData("ИМЯ") + " " + (string)partner_person.GetData("ОТЧЕСТВО");
			}
			else
			{
				title = (string)partner_juridical.GetData("НАИМЕНОВАНИЕ_ЮРИДИЧЕСКОЕ");
			}
			return title;
		}
        public string GetTitleShort()
        {
            string title;
            
            // Получение данных о ФИО или полном наименовании
            if (juridical == false)
            {
                string name = (string)partner_person.GetData("ИМЯ");
                string surname = (string)partner_person.GetData("ОТЧЕСТВО");
                if (name.Length > 0) name = name.Substring(0, 1);
                if (surname.Length > 0) surname = surname.Substring(0, 1);
                if (name.Length == 1) name = name + ".";
                if (surname.Length == 1) surname = surname + ".";
                title = (string)partner_person.GetData("ФАМИЛИЯ") + " " + name + surname;
            }
            else
            {
                title = (string)partner_juridical.GetData("НАИМЕНОВАНИЕ_ЮРИДИЧЕСКОЕ");
            }
            return title;
        }
		public string GetAddress()
		{
			string txt;
			// Получение данных о прописке или юр. адресе
			if(juridical == false)
			{
				txt = (string)partner_person.GetData("АДРЕС_ПРОПИСКА");
			}
			else
			{
				txt = (string)partner_juridical.GetData("АДРЕС_ЮРИДИЧЕСКИЙ");
			}
			return txt;
		}
		public string GetPhone()
		{
			string txt = "";
			// Получение данных о контактном телефоне
			// Для начала попытка получения данных по новой схеме
			ArrayList contacts = new ArrayList();
			DbSqlPartnerContact.SelectInArray(contacts, this.code);
			foreach(DtPartnerContact contact in contacts)
			{
				// Берем только телефоны
				if((string)contact.GetData("ТИП_КОНТАКТ") == "ТЕЛЕФОН")
				{
					if (txt != "") txt	+= "; ";
					txt += contact.GetData("КОНТАКТ");
				}
			}
			if (txt != "") return txt;

			// Если нет новой версии - работаем со старой
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
            // Получение данных о контактном телефоне
            // Для начала попытка получения данных по новой схеме
            ArrayList contacts = new ArrayList();
            DbSqlPartnerContact.SelectInArray(contacts, this.code);
            foreach (DtPartnerContact contact in contacts)
            {
                // Берем только e-mail
                if ((string)contact.GetData("ТИП_КОНТАКТ") == "E-MAIL")
                {
                    if (txt != "") txt += "; ";
                    txt += contact.GetData("КОНТАКТ");
                }
            }
            return txt;
        }

		public string GetTitleName()
		{
			string title;
			// Получение данных о ФИО или полном наименовании
			if(juridical == false)
			{
				title = partner_person.GetData("ИМЯ") + " " + (string)partner_person.GetData("ОТЧЕСТВО");
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

		// Получение текстовых данных физического лица
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
				MessageBox.Show("Уточните e-mail клиента!");
			return true;
		}
		public void PreselectInfoShow()
        {
			if (Comment.Length != 0)
				MessageBox.Show(Comment, "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
