using System;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DtAutoOptionVariant.
	/// </summary>
	public class DtAutoOptionVariant
	{
		public long code;
		public long code_option;
		public string name;

		public DtAutoOptionVariant()
		{
			code = 0L;
			code_option = 0L;
			name = "";
		}

		public object GetData(string data)
		{
			switch(data)
			{
				case "���":
					return (object)(long)code;
				case "�������_������������":
					return (object)(string)name;
				case "������_���_�����":
					return (object)(long)code_option;
				default:
					return (object)null;
			}
		}

		public void SetData(string data, object val)
		{
			switch(data)
			{
				case "���":
					code = (long)val;
					break;
				case "�������_������������":
					name = (string)val;
					name.Trim();
					break;
				case "������_���_�����":
					code_option = (long)val;
					break;
				default:
					break;
			}
		}

		public void SetLVItem(ListViewItem item)
		{
			item.SubItems.Clear();		// ����� ������� ���������� ���������� � ���������

			item.Tag				= this.code;
			item.Text				= this.name;
		}
	}
}
