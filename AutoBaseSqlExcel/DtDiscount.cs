using System;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// ���������� ��������
	/// </summary>
	public class DtDiscount
	{
		private long			code;					// ��� ���������� �������� (����� �� ��������)
		private float			discount_service_work;	// ������ �� ������ �������
		private long			code_partner;			// ��� ����������� �� ������ ��������
		private bool			flag;					// ���� ���������� �������� �����������
		private string			comment;				// ���������� � ���������� ��������

		private string			tmp_partner_name;		// ������������ ��������� ��������

		public DtDiscount()
		{
			code					= 0;
			discount_service_work	= 0.0F;
			code_partner			= 0;
			flag					= false;
			comment					= "";

			tmp_partner_name		= "";
		}

		public DtDiscount(DtDiscount element)
		{
			code					= element.code;
			discount_service_work	= element.discount_service_work;
			code_partner			= element.code_partner;
			flag					= element.flag;
			comment					= element.comment;

			tmp_partner_name		= element.tmp_partner_name;
		}

		public bool IsEqual(DtDiscount element)
		{
			if(code					 != element.code) return false;
			if(discount_service_work !=	element.discount_service_work) return false;
			if(code_partner			 != element.code_partner) return false;
			if(flag					 != element.flag) return false;
			if(comment				 != element.comment) return false;
			
			return true;
		}

		public object GetData(string data)
		{
			switch(data)
			{
				case "���_�������":
					return (object)(long)code;
				case "������_������_������_�������":
					return (object)(float)discount_service_work;
				case "���_����������_�������":
					return (object)(long)code_partner;
				case "����_������_�������":
					return (object)(bool)flag;
				case "����������_�������":
					return (object)(string)comment;
				case "����������_������������":
					return (object)(string)tmp_partner_name;
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
				case "������_������_������_�������":
					discount_service_work = (float)val;
					break;
				case "���_����������_�������":
					code_partner = (long)val;
					break;
				case "����_������_�������":
					flag = (bool)val;
					break;
				case "����������_�������":
					comment = (string)val;
					break;
				case "����������_������������":
					tmp_partner_name = (string)val;
					break;
				default:
					break;
			}
		}

		public void SetLVItem(ListViewItem item)
		{
			item.SubItems.Clear();		// ����� ������� ���������� ���������� � ���������

			item.Tag				= this.code;
			item.Text				= this.code.ToString();
			item.SubItems.Add(this.discount_service_work.ToString());
			if(this.code_partner > 0)
				item.SubItems.Add(this.tmp_partner_name);
			else
				item.SubItems.Add("");
			item.SubItems.Add(this.comment);
			if(this.code_partner != 0 && this.flag == false)
				item.BackColor = System.Drawing.Color.Red;
			if(this.flag == true)
				item.BackColor = System.Drawing.Color.LightGreen;
		}
	}
}
