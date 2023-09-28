using System;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// ���������� �������� �����
	/// </summary>
	public class DtWorkCollection
	{
		long	code;		// ���������� ��� ������
		string	name;		// ������������ ������

		public DtWorkCollection()
		{
			code	= 0;
			name	= "";
		}

		public DtWorkCollection(string collection_name)
		{
			code	= 0;
			name	= collection_name;
		}

		public object GetData(string data)
		{
			switch(data)
			{
				case "���_���������":
					return (object)(long)code;
				case "������������_���������":
					return (object)(string)name;
				default:
					return (object)null;
			}
		}

		public void SetData(string data, object val)
		{
			switch(data)
			{
				case "���_���������":
					code = (long)val;
					break;
				case "������������_���������":
					name = (string)val;
					name = name.Trim();
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
