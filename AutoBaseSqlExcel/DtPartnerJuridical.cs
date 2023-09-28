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
				case "������������_�����������":
					return (object)(string)name_juridical;
				case "�����_�����������":
					return (object)(string)address_juridical;
				case "�����_�����������":
					return (object)(string)address_fact;
				case "�������":
					return (object)(string)contact;
				default:
					return (object)null;
			}
		}

		public void SetData(string data, object val)
		{
			switch(data)
			{
				case "������������_�����������":
					name_juridical = (string)val;
					name_juridical = name_juridical.Trim();
					break;
				case "�����_�����������":
					address_juridical = (string)val;
					break;
				case "�����_�����������":
					address_fact = (string)val;
					break;
				case "�������":
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
				case "������������_�����������":
					if(name_juridical.Length == 0) return false;
					break;
				default:
					return false;
			}
			return true;
		}

		public void SetLVItem(ListViewItem item)
		{
			// ������ �������������� ����	
			item.SubItems.Add(name_juridical);
		}
	}
}
