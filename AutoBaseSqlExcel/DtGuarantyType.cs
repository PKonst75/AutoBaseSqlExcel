using System;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// ���� ��������
	/// </summary>
	public class DtGuarantyType:Dt
	{
		long		code;			// ��� ���� �������� � ��
		string		name;			// ������������ ���� ��������

		bool		is_responsible;	// ���� ������� �������������

		public DtGuarantyType()
		{
			code		= 0;
			name		= "";

			is_responsible	= false;
		}

		public object GetData(string data)
		{
			switch(data)
			{
				case "���_��������":
					return (object)(long)code;
				case "��������_��������":
					return (object)(string)name;
				case "�������������":
					return (object)(bool)is_responsible;
				default:
					return (object)null;
			}
		}

		public void SetData(string data, object val)
		{
			switch(data)
			{
				case "���_��������":
					code = (long)val;
					break;
				case "��������_��������":
					name = (string)val;
					break;
				case "�������������":
					is_responsible = (bool)val;
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

		public override string Title()
		{
			return name;
		}

		public override long Code()
		{
			return code;
		}
	}
}
