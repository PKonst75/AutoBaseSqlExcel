using System;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// �������� ������� ��������� �� �����
	/// </summary>
	public class DtStorageIncomDoc
	{
		long code;					// ���������� ��� ���������
		string number;				// ����� ���������
		DateTime date;				// ���� ���������

		public DtStorageIncomDoc()
		{
			code		= 0;
			number		= "";
			date		= DateTime.Now;
		}

		public object GetData(string data)
		{
			switch(data)
			{
				case "���":
					return (object)(long)code;
				case "�����":
					return (object)(string)number;
				case "����":
					return (object)(DateTime)date;
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
				case "�����":
					number = (string)val;
					break;
				case "����":
					date = (DateTime)val;
					break;
				default:
					break;
			}
		}

		public void SetLVItem(ListViewItem item)
		{
			item.SubItems.Clear();		// ����� ������� ���������� ���������� � ���������

			item.Tag				= this.code;
			item.Text				= this.number;
			item.SubItems.Add(this.date.ToString());
		}
	}
}
