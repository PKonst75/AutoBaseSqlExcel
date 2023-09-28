using System;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DtAutoOption.
	/// </summary>
	public class DtAutoOption
	{
		public long code;
		public string name;
		public long code_group;

		public long	tmp_option_variant;				// ������� �����, ��� �������� ������������
		public string	tmp_option_variant_name;	// ������� �����, ��� �������� ������������ ������������

		public bool tmp_active;						// ���� ����������� � ������������
		public bool tmp_change;						// ���� ��������� �������

		public long tmp_code_model;
		public long tmp_code_model_variant;

		public DtAutoOption()
		{
			code = 0L;
			name = "";
			code_group = 0L;

			tmp_option_variant = 0L;
			tmp_option_variant_name	= "";

			tmp_active	= false;
			tmp_change	= false;

			tmp_code_model = 0;
			tmp_code_model_variant = 0;;
		}

		public object GetData(string data)
		{
			switch(data)
			{
				case "���":
					return (object)(long)code;
				case "�����_������������":
					return (object)(string)name;
				case "������_���_�����_������":
					return (object)(long)code_group;
				case "���_������":
					return (object)(long)tmp_code_model;
				case "���_������_�������":
					return (object)(long)tmp_code_model_variant;
				case "�������_������������":
					return (object)(string)tmp_option_variant_name;
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
				case "�����_������������":
					name = (string)val;
					name.Trim();
					break;
				case "������_���_�����_������":
					code_group = (long)val;
					break;
				case "���_������":
					tmp_code_model = (long)val;
					break;
				case "���_������_�������":
					tmp_code_model_variant = (long)val;
					break;
				case "�������_������������":
					tmp_option_variant_name = (string)val;
					break;
				default:
					break;
			}
		}

		public void SetLVItem(ListViewItem item)
		{
			item.SubItems.Clear();		// ����� ������� ���������� ���������� � ���������

			item.Tag				= this;
			item.Text				= this.name;
			item.SubItems.Add(this.tmp_option_variant_name);
		}

		public void SetLVItemComplect(ListViewItem item)
		{
			item.SubItems.Clear();		// ����� ������� ���������� ���������� � ���������

			item.Tag				= this;
			item.Text				= this.name;
			item.SubItems.Add(this.tmp_option_variant_name);

			if(tmp_code_model != 0) item.Checked = true;
		}
	}
}
