using System;
using System.Windows.Forms;
using System.Drawing;

namespace AutoBaseSql
{
	/// <summary>
	/// ������ � ������� ����������
	/// </summary>
	public class DtColor
	{
		long		code;				// ���������� ��� ����� � ���� ������
		long		code_model;			// ������ � ������� ��������� ����	
		string		color_code;			// ��� ���������� ����� ��������������
		string		color_name;			// ������������ ����� (�������������)
		string		color_description;	// �������� ����� 

		bool		color_canceled;		// �� � ����������� � ����������� ������ ������

		public DtColor()
		{
			code				= 0;
			code_model			= 0;
			color_code			= "";
			color_name			= "";
			color_description	= "";

			color_canceled		= false;
		}

		public DtColor(DtColor element)
		{
			code				= element.code;
			code_model			= element.code_model;
			color_code			= element.color_code;
			color_name			= element.color_name;
			color_description	= element.color_description;

			color_canceled		= element.color_canceled;
		}

		public object GetData(string data)
		{
			switch(data)
			{
				case "���_����������_����":
					return (object)(long)code;
				case "������_���_����������_������":
					return (object)(long)code_model;
				case "����_���":
					return (object)(string)color_code;
				case "����_������������":
					return (object)(string)color_name;
				case "����_��������":
					return (object)(string)color_description;
				case "����_�������":
					return (object)(bool)color_canceled;
				default:
					return (object)null;
			}
		}

		public void SetData(string data, object val)
		{
			switch(data)
			{
				case "���_����������_����":
					code = (long)val;
					break;
				case "������_���_����������_������":
					code_model = (long)val;
					break;
				case "����_���":
					color_code = (string)val;
					color_code.Trim();
					break;
				case "����_������������":
					color_name = (string)val;
					color_name.Trim();
					break;
				case "����_��������":
					color_description = (string)val;
					color_description.Trim();
					break;
				case "����_�������":
					color_canceled = (bool)val;
					break;
				default:
					break;
			}
		}

		public bool CheckData(string data)
		{
			switch(data)
			{
				case "������_���_����������_������":
					if(code_model <= 0) return false;
					break;
				case "����_������������":
					if(color_name.Length <= 0) return false;
					break;
				default:
					return false;
			}
			return true;
		}

		public void SetLVItem(ListViewItem item)
		{
			item.SubItems.Clear();		// ����� ������� ���������� ���������� � ���������

			item.Tag				= this.code;
			item.Text				= this.color_name + "(" + this.color_code + ")";
			if(this.color_canceled == true)
				SetLVItemCancel(item);
		}

		public static void SetLVItemCancel(ListViewItem item)
		{
			item.BackColor = Color.Gray;
		}
		public static void SetLVItemRestore(ListViewItem item)
		{
			item.BackColor = Color.White;
		}
	}
}
