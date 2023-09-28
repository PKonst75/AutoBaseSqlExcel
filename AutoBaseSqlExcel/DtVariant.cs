using System;
using System.Windows.Forms;
using System.Drawing;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DtVariant.
	/// </summary>
	public class DtVariant
	{
		long		code;					// ���������� ���
		long		code_model;				// ��� ������ ��� ������� ��� ����������
		string		variant_name;			// ������������ ���������� (���, ��������, ��������)
		string		variant_description;	// ����������� �������� ����������
		bool		variant_canceled;		// ������� �� ������ ����������

		public DtVariant()
		{
			code					= 0;
			code_model				= 0;
			variant_name			= "";
			variant_description		= "";
			variant_canceled		= false;
		}

		public DtVariant(DtVariant element)
		{
			code					= element.code;
			code_model				= element.code_model;
			variant_name			= element.variant_name;
			variant_description		= element.variant_description;
			variant_canceled		= element.variant_canceled;
		}

		public object GetData(string data)
		{
			switch(data)
			{
				case "���_����������_����������":
					return (object)(long)code;
				case "������_���_����������_������":
					return (object)(long)code_model;
				case "����������_������������":
					return (object)(string)variant_name;
				case "����������_��������":
					return (object)(string)variant_description;
				case "����������_�������":
					return (object)(bool)variant_canceled;
				default:
					return (object)null;
			}
		}

		public void SetData(string data, object val)
		{
			switch(data)
			{
				case "���_����������_����������":
					code = (long)val;
					break;
				case "������_���_����������_������":
					code_model = (long)val;
					break;
				case "����������_������������":
					variant_name = (string)val;
					variant_name.Trim();
					break;
				case "����������_��������":
					variant_description = (string)val;
					variant_description.Trim();
					break;
				case "����������_�������":
					variant_canceled = (bool)val;
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
				case "����������_������������":
					if(variant_name.Length <= 0) return false;
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
			item.Text				= this.variant_name;
			if(this.variant_canceled == true)
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
		public override string ToString()
		{
			return this.variant_name;
		}
	}
}
