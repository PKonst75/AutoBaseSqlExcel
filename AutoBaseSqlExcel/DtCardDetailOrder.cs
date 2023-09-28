using System;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// ������ �� �������� ������� � �����-������.
	/// </summary>
	public class DtCardDetailOrder
	{
		long code;						// ��� ������ � ��������
		long card_number;				// ����� ��������
		int card_year;					// ��� ��������
		long code_storage_detail;		// ��� ��������� �������
		long code_catalogue_detail;		// ��� ������� � ��������
		bool guaranty_flag;				// ���� ����������� ����������� ������
		// ���������
		string tmp_name;				// ������������ ������

		public DtCardDetailOrder()
		{
			code					= 0L;
			card_number				= 0L;
			card_year				= 0;
			code_catalogue_detail	= 0;
			code_storage_detail		= 0;
			guaranty_flag			= false;

			tmp_name				= "";
		}

		public object GetData(string data)
		{
			switch(data)
			{
				case "���_��������_������_������":
					return (object)(long)code;
				case "�����_��������":
					return (object)(long)card_number;
				case "���_��������":
					return (object)(int)card_year;
				case "���_�������_������":
					return (object)(long)code_catalogue_detail;
				case "���_�����_������":
					return (object)(long)code_storage_detail;
				case "��������":
					return (object)(bool)guaranty_flag;
				// ���������
				case "������������":
					return (object)(string)tmp_name;
				default:
					return (object)null;
			}
		}

		public void SetData(string data, object val)
		{
			switch(data)
			{
				case "���_��������_������_������":
					code = (long)val;
					break;
				case "�����_��������":
					card_number = (long)val;
					break;
				case "���_��������":
					card_year = (int)val;
					break;
				case "���_�������_������":
					code_catalogue_detail = (long)val;
					break;
				case "���_�����_������":
					code_storage_detail = (long)val;
					break;
				case "����_��������":
					guaranty_flag = (bool)val;
					break;
				// ���������������
				case "������������":
					tmp_name = (string)val;
					break;
				default:
					break;
			}
		}

		public void SetLVItem(ListViewItem item)
		{
			item.SubItems.Clear();		// ����� ������� ���������� ���������� � ���������
			item.SubItems.Add("");

			item.Tag				= this.code;
			item.Text				= this.tmp_name;
			if(this.guaranty_flag	== true)
				item.SubItems[1].Text = "+";
			else
				item.SubItems[1].Text = "-";
		}
	}
}
