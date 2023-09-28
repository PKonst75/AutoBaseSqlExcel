using System;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DtServiceOuter.
	/// </summary>
	public class DtServiceOuter
	{
		long	code;
		string	name;
		string	address;

		public DtServiceOuter()
		{
			code		= 0;
			name		= "";
			address		= "";
		}

		public object GetData(string data)
		{
			switch(data)
			{
				case "���_������":
					return (object)(long)code;
				case "������������":
					return (object)(string)name;
				case "�����":
					return (object)(string)address;
				default:
					return (object)null;
			}
		}

		public void SetData(string data, object val)
		{
			switch(data)
			{
				case "���_������":
					code = (long)val;
					break;
				case "������������":
					name = (string)val;
					name.Trim();
					break;
				case "�����":
					address = (string)val;
					address.Trim();
					break;
				default:
					break;
			}
		}

		public void SetLVItem(ListViewItem item)
		{
			string txt = "";
			item.SubItems.Clear();		// ����� ������� ���������� ���������� � ���������

			item.Tag				= this.code;
			item.Text				= this.name;
			item.SubItems.Add(this.address);
		}
	}
}
