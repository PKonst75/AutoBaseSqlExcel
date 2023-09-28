using System;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// ����� ���������� ����� ��������� - ������� - ������, ��������� � ������ �����, � �.�.
	/// </summary>
	public class DtCommonReason
	{
		long		code;
		string		description;

		public DtCommonReason()
		{
			code		= 0;
			description	= "";
		}

		public object GetData(string data)
		{
			switch(data)
			{
				case "���_�������":
					return (object)(long)code;
				case "��������":
					return (object)(string)description;
				default:
					return (object)null;
			}
		}

		public void SetData(string data, object val)
		{
			switch(data)
			{
				case "���_�������":
					code = (long)val;
					break;
				case "��������":
					description = (string)val;
					description.Trim();
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
			item.Text				= this.description;
		}
	}
}
