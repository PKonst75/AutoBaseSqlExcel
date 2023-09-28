using System;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Îïèñàíèå êîíòğàãåíòà - ôèçè÷åñêîãî ëèöà
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
				case "ÔÀÌÈËÈß":
					return (object)(string)surname;
				case "ÈÌß":
					return (object)(string)name;
				case "ÎÒ×ÅÑÒÂÎ":
					return (object)(string)patronymic;
				case "ÀÄĞÅÑ_ÏĞÎÏÈÑÊÀ":
					return (object)(string)registration;
				case "ÄÀÒÀ_ĞÎÆÄÅÍÈß":
					return (object)(DateTime)birthday;
				case "ÅÑÒÜ_ÄÀÒÀ_ĞÎÆÄÅÍÈß":
					return (object)(bool)is_birthday;
				case "ÀÄĞÅÑ_ÏĞÎÆÈÂÀÍÈÅ":
					return (object)(string)address_living;
				case "ÏÀĞÂÈËÜÍÎÅ_ÍÀÈÌÅÍÎÂÀÍÈÅ_ÊĞÀÒÊÎÅ":
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
				case "ÔÀÌÈËÈß":
					surname = (string)val;
					surname = surname.Trim();
					surname = DtService.FirstUpper(surname); // Ïåğâàÿ áóêâà äîëæíà áûòü çàãëàâíîé
					break;
				case "ÈÌß":
					name = (string)val;
					name = name.Trim();
					name = DtService.FirstUpper(name);	// Ïåğâàÿ áóêâà äîëæíà áûòü çàãëàâíîé
					break;
				case "ÎÒ×ÅÑÒÂÎ":
					patronymic = (string)val;
					patronymic = patronymic.Trim();
					patronymic = DtService.FirstUpper(patronymic); // Ïåğâàÿ áóêâà äîëæíà áûòü çàãëàâíîé
					break;
				case "ÀÄĞÅÑ_ÏĞÎÏÈÑÊÀ":
					registration = (string)val;
					break;
				case "ÄÀÒÀ_ĞÎÆÄÅÍÈß":
					birthday = (DateTime)val;
					break;
				case "ÅÑÒÜ_ÄÀÒÀ_ĞÎÆÄÅÍÈß":
					is_birthday = (bool)val;
					break;
				case "ÀÄĞÅÑ_ÏĞÎÆÈÂÀÍÈÅ":
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
				case "ÔÀÌÈËÈß":
					if(surname.Length == 0) return false;
					break;
				case "ÈÌß":
					if(name.Length <= 1) return false;
					break;
				case "ÎÒ×ÅÑÒÂÎ":
					if(patronymic.Length <= 1) return false;
					break;
				default:
					return false;
			}
			return true;
		}

		public void SetLVItem(ListViewItem item)
		{
			// Òîëüêî äîïîëíèòåëüíûå ïîëÿ	
			item.SubItems.Add(surname + " " + name + " " + patronymic);
		}

		public string Surname() { return this.surname; }
		public string Name() { return this.name; }
		public string Secondname() { return this.patronymic; }
	}
}
