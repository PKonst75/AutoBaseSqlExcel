using System;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DtFactory.
	/// </summary>
	public class DtFactory
	{
		private long		code;
		private string		name;
		private string		prefix;

		public DtFactory()
		{
			code		= 0;
			name		= "";
			prefix		= "";
		}
		public DtFactory(DtFactory data)
		{
			code		= data.code;
			name		= data.name;
			prefix		= data.prefix;
		}

		public object GetData(string data)
		{
			switch(data)
			{
				case "���_����������_�������������":
					return (object)(long)code;
				case "������������_����������_�������������":
					return (object)(string)name;
				case "�������_����������_�������������":
					return (object)(string)prefix;
				default:
					return (object)null;
			}
		}

		public void SetData(string data, object val)
		{
			switch(data)
			{
				case "���_����������_�������������":
					code = (long)val;
					break;
				case "������������_����������_�������������":
					name = (string)val;
					break;
				case "�������_����������_�������������":
					prefix = (string)val;
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
			item.SubItems.Add(this.prefix);
		}
	}
}
