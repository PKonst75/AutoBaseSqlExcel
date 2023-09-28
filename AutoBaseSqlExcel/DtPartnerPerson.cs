using System;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// �������� ����������� - ����������� ����
	/// </summary>
	public class DtPartnerPerson
	{

		public class DtPartnerPersonTxt
        {
			public readonly string surname;
			public readonly string name;
			public readonly string patronymic;
			public readonly string short_name;
			public readonly string full_name;
			public readonly string address;

			public DtPartnerPersonTxt(DtPartnerPerson person)
			{
				surname = person.surname;
				name = person.name;
				patronymic = person.patronymic;
				short_name = surname;
				full_name = surname;
				if (name != "")
				{
					short_name += " " + name.Substring(0, 1) + ".";
					full_name += " " + name;
				}
				if (patronymic != "")
				{
					short_name += patronymic.Substring(0, 1);
					full_name += " " + patronymic;
				}
				address = person.registration;
				
			}

			
        }
		string		surname;
		string		name;
		string		patronymic;
		string		registration;
		DateTime	birthday;
		bool		is_birthday;
		
		string		address_living;

		public DtPartnerPerson()
		{
			surname			= "";
			name			= "";
			patronymic		= "";
			registration	= "";
			birthday		= DateTime.Now;
			is_birthday		= false;

			address_living	= "";
		}

		public object GetData(string data)
		{
			switch(data)
			{
				case "�������":
					return (object)(string)surname;
				case "���":
					return (object)(string)name;
				case "��������":
					return (object)(string)patronymic;
				case "�����_��������":
					return (object)(string)registration;
				case "����_��������":
					return (object)(DateTime)birthday;
				case "����_����_��������":
					return (object)(bool)is_birthday;
				case "�����_����������":
					return (object)(string)address_living;
				case "����������_������������_�������":
					string tmp_name = "";
					string tmp_patronymic = "";
					string tmp_title = "";
					if(name.Length > 0)
						tmp_name = name.Substring(0, 1).ToUpper() + ".";
					if(patronymic.Length > 0)
						tmp_patronymic = patronymic.Substring(0, 1).ToUpper() + ".";
					tmp_title = surname + " " +  tmp_name + tmp_patronymic;
					return (string)tmp_title;
				default:
					return (object)null;
			}
		}

		public void SetData(string data, object val)
		{
			switch(data)
			{
				case "�������":
					surname = (string)val;
					surname = surname.Trim();
					surname = DtService.FirstUpper(surname); // ������ ����� ������ ���� ���������
					break;
				case "���":
					name = (string)val;
					name = name.Trim();
					name = DtService.FirstUpper(name);	// ������ ����� ������ ���� ���������
					break;
				case "��������":
					patronymic = (string)val;
					patronymic = patronymic.Trim();
					patronymic = DtService.FirstUpper(patronymic); // ������ ����� ������ ���� ���������
					break;
				case "�����_��������":
					registration = (string)val;
					break;
				case "����_��������":
					birthday = (DateTime)val;
					break;
				case "����_����_��������":
					is_birthday = (bool)val;
					break;
				case "�����_����������":
					address_living = (string)val;
					break;
				default:
					break;
			}
		}

		public bool CheckData(string data)
		{
			switch(data)
			{
				case "�������":
					if(surname.Length == 0) return false;
					break;
				case "���":
					if(name.Length <= 1) return false;
					break;
				case "��������":
					if(patronymic.Length <= 1) return false;
					break;
				default:
					return false;
			}
			return true;
		}

		public void SetLVItem(ListViewItem item)
		{
			// ������ �������������� ����	
			item.SubItems.Add(surname + " " + name + " " + patronymic);
		}

		public string Surname() { return this.surname; }
		public string Name() { return this.name; }
		public string Secondname() { return this.patronymic; }
	}
}
