using System;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DtListAlarm.
	/// </summary>
	public class DtListAlarm
	{
		long		code;
		string		name;

		public DtListAlarm()
		{
			code		= 0;
			name	= "";
		}

		public object GetData(string data)
		{
			switch(data)
			{
				case "���_������������":
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
				case "���_������������":
					code = (long)val;
					break;
				case "������������":
					name = (string)val;
					name.Trim();
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
		}
	}
}
