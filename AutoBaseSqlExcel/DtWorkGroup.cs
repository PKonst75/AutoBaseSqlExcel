using System;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// ������ ������������� �� ���������
	/// </summary>
	public class DtWorkGroup
	{
		long		code;			// ���������� ���
		string		name;			// ������������ ������ �������������

		public DtWorkGroup()
		{
			code		= 0;
			name		= "";
		}

		public object GetData(string data)
		{
			switch(data)
			{
				case "���_����������_���":
					return (object)(long)code;
				case "������������":
					return (object)(string)name;
				default:
					return (object)null;
			}
		}

		public void SetData(string data, object val)
		{
			switch(data)
			{
				case "���_����������_���":
					code = (long)val;
					break;
				case "������������":
					name = (string)val;
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
