using System;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// ������� ����������
	/// </summary>
	public class DtAutoSell
	{
		long				code;
		long				code_auto;
		long				code_customer;
		DateTime			date;
		string				comment;

		DtPartner			tmp_partner;
		DtAuto				tmp_auto;
		string				tmp_customer_name;
		string				tmp_auto_model;
		string				tmp_auto_color;
		string				tmp_auto_variant;
		string				tmp_auto_vin;

		public DtAutoSell()
		{
			code			= 0;
			code_auto		= 0;
			code_customer	= 0;
			date			= DateTime.Now;
			comment			= "";

			tmp_partner		= null;
			tmp_auto		= null;

			tmp_customer_name	= "";
			tmp_auto_model		= "";
			tmp_auto_color		= "";
			tmp_auto_variant	= "";
			tmp_auto_vin		= "";
		}

		public object GetData(string data)
		{
			switch(data)
			{
				case "���_����������_�������":
					return (object)(long)code;
				case "������_���_����������":
					return (object)(long)code_auto;
				case "������_���_����������":
					return (object)(long)code_customer;
				case "����_����������_�������":
					return (object)(DateTime)date;
				case "����������_����������_�������":
					return (object)(string)comment;
				// ��������������
				case "����������":
					return (object)(DtPartner)tmp_partner;
				case "����������":
					return (object)(DtAuto)tmp_auto;
				case "����������_������������":
					return (object)(string)tmp_customer_name;
				case "����������_������":
					return (object)(string)tmp_auto_model;
				case "����������_����":
					return (object)(string)tmp_auto_color;
				case "����������_����������":
					return (object)(string)tmp_auto_variant;
				case "����������_VIN":
					return (object)(string)tmp_auto_vin;
				default:
					return (object)null;
			}
		}

		public void SetData(string data, object val)
		{
			switch(data)
			{
				case "���_����������_�������":
					code = (long)val;
					break;
				case "������_���_����������":
					code_auto = (long)val;
					break;
				case "������_���_����������":
					code_customer = (long)val;
					break;
				case "����_����������_�������":
					date = (DateTime)val;
					break;
				case "����������_����������_�������":
					comment = (string)val;
					comment = comment.Trim();
					break;
				// ��������������
				case "����������":
					tmp_partner			= (DtPartner)val;
					code_customer		= (long)tmp_partner.GetData("���_����������");
					tmp_customer_name	= (string)tmp_partner.GetData("������������_�������");
					break;
				case "����������":
					tmp_auto		= (DtAuto)val;
					code_auto		= (long)tmp_auto.GetData("���_����������");
					tmp_auto_model	= (string)tmp_auto.GetData("������");
					tmp_auto_color	= (string)tmp_auto.GetData("����������_����");
					tmp_auto_variant= (string)tmp_auto.GetData("����������_����������");
					tmp_auto_vin	= (string)tmp_auto.GetData("VIN");
					break;
				case "����������_������������":
					tmp_customer_name	= (string)val;
					break;
				case "����������_������":
					tmp_auto_model		= (string)val;
					break;
				case "����������_����":
					tmp_auto_color		= (string)val;
					break;
				case "����������_����������":
					tmp_auto_variant	= (string)val;
					break;
				case "����������_VIN":
					tmp_auto_vin		= (string)val;
					break;
				default:
					break;
			}
		}

		public bool CheckData(string data)
		{
			switch(data)
			{
				case "������_���_����������":
					if(code_auto == 0) return false;
					break;
				case "������_���_����������":
					if(code_customer == 0) return false;
					break;
				default:
					return false;
			}
			return true;
		}

		public void SetLVItem(ListViewItem item)
		{
			item.SubItems.Clear();		// ����� ������� ���������� ���������� � ���������

			item.Tag				= code;
			item.Text				= this.date.ToShortDateString();
			item.SubItems.Add(this.tmp_customer_name);
			item.SubItems.Add(this.tmp_auto_model);
			item.SubItems.Add(this.tmp_auto_color);
			item.SubItems.Add(this.tmp_auto_variant);
			item.SubItems.Add(this.tmp_auto_vin);
		}
	}
}
