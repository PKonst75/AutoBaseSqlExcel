using System;
using System.Windows.Forms;
using System.Drawing;

namespace AutoBaseSql
{
	/// <summary>
	/// ������ �� �����
	/// </summary>
	public class DtStorageRequest
	{
		public struct CodeYear
		{
			public long code;
			public int year;
		};
		// �������� ������ �� �����
		private long		code;				// ��� ������
		private int			year;				// ��� ������
		private DateTime	date;				// ���� ������
		private long		code_storage;		// ��� ��������� ������� ��� ������
		private float		quontity;			// ���������� ��� ������
		private bool		guaranty;			// ������� � ������ �� ��������
		private DateTime	date_perfomance;	// �������� ���� ���������� ������
		private long		code_requester;		// ��� ������������ ������
		private long		card_number;		// ����� ��������
		private int			card_year;			// ��� ��������
		private long		code_partner;		// ��� �����������
		private DateTime	date_give;			// ���� ������ ������
		private long		code_giver;			// ��� ��������� ������
		private DateTime	date_execute;		// ���� ���������� ������
		private long		code_execute;		// ��� ������������ ���������� ������
		private long		code_archive;		// ��� ������������ ���������
		private DateTime	date_supply;		// �������������� ���� ��������

		private string		tmp_detail_name;		// ������������ ���������� ������
		private string		tmp_requester_name;		// ������ ������������ ������
		private bool		tmp_date_perfomance_is;	// ������������ �� �������� ����
		private bool		tmp_date_give_is;		// ������������ ���� ������ ������
		private bool		tmp_date_execute_is;	// ������������ ���� ���������� ������
		private bool		tmp_date_supply_is;		// ������������ �� ���� �������������� ��������
		private string		tmp_partner_name;		// ������������ �����������


		public DtStorageRequest()
		{
			code			= 0;
			year			= 0;
			date			= DateTime.Now;
			code_storage	= 0;
			quontity		= 0.0F;
			guaranty		= false;
			date_perfomance	= DateTime.Now;
			code_requester	= 0;
			card_number		= 0;
			card_year		= 0;
			code_partner	= 0;
			date_give		= DateTime.Now;
			code_giver		= 0;
			date_execute	= DateTime.Now;
			code_execute	= 0;
			code_archive	= 0;
			date_supply		= DateTime.Now;

			tmp_detail_name			= "";
			tmp_requester_name		= "";
			tmp_date_perfomance_is	= false;
			tmp_date_give_is		= false;
			tmp_date_execute_is		= false;
			tmp_date_supply_is		= false;
			tmp_partner_name		= "";
		}

		public DtStorageRequest(DtStorageRequest element)
		{
			code			= element.code;
			year			= element.year;
			date			= element.date;
			code_storage	= element.code_storage;
			quontity		= element.quontity;
			guaranty		= element.guaranty;
			date_perfomance	= element.date_perfomance;
			code_requester	= element.code_requester;
			card_number		= element.card_number;
			card_year		= element.card_year;
			code_partner	= element.code_partner;
			date_give		= element.date_give;
			code_giver		= element.code_giver;
			date_execute	= element.date_execute;
			code_execute	= element.code_execute;
			code_archive	= element.code_archive;
			date_supply		= element.date_supply;

			tmp_detail_name			= element.tmp_detail_name;
			tmp_requester_name		= element.tmp_requester_name;
			tmp_date_perfomance_is	= element.tmp_date_perfomance_is;
			tmp_date_give_is		= element.tmp_date_give_is;
			tmp_date_execute_is		= element.tmp_date_execute_is;
			tmp_date_supply_is		= element.tmp_date_supply_is;
			tmp_partner_name		= element.tmp_partner_name;
		}

		public object GetData(string data)
		{
			switch(data)
			{
				case "���_������":
					return (object)(long)code;
				case "���_������":
					return (object)(int)year;
				case "����_������":
					return (object)(DateTime)date;
				case "������_���_�����_������":
					return (object)(long)code_storage;
				case "����������_�����_������":
					return (object)(float)quontity;
				case "��������_������":
					return (object)(bool)guaranty;
				case "���������_����_����������":
					return (object)(DateTime)date_perfomance;
				case "����_���������_����_����������":
					return (object)(bool)tmp_date_perfomance_is;
				case "���_��������_������":
					return (object)(long)code_requester;
				case "������_�����_��������":
					return (object)(long)card_number;
				case "������_���_��������":
					return (object)(int)card_year;
				case "������_���_����������":
					return (object)(long)code_partner;
				case "����_������_������":
					return (object)(DateTime)date_give;
				case "����_����_������_������":
					return (object)(bool)tmp_date_give_is;
				case "���_��������_������_������":
					return (object)(long)code_giver;
				case "����_������_����������":
					return (object)(DateTime)date_execute;
				case "����_����_������_����������":
					return (object)(bool)tmp_date_execute_is;
				case "���_��������_����������_������":
					return (object)(long)code_execute;
				case "���_��������_���������":
					return (object)(long)code_archive;
				case "����_��������":
					return (object)(DateTime)date_supply;
				case "����_����_��������":
					return (object)(bool)tmp_date_supply_is;
				// ���������������
				case "������������_�����_������":
					return (object)(string)tmp_detail_name;
				case "��������_������":
					return (object)(string)tmp_requester_name;
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
				case "���_������":
					code = (long)val;
					break;
				case "���_������":
					year = (int)val;
					break;
				case "����_������":
					date = (DateTime)val;
					break;
				case "������_���_�����_������":
					code_storage = (long)val;
					break;
				case "����������_�����_������":
					quontity = (float)val;
					break;
				case "��������_������":
					guaranty = (bool)val;
					break;
				case "���������_����_����������":
					date_perfomance = (DateTime)val;
					tmp_date_perfomance_is	= true;
					break;
				case "���_��������_������":
					code_requester = (long)val;
					break;
				case "������_�����_��������":
					card_number = (long)val;
					break;
				case "������_���_��������":
					card_year = (int)val;
					break;
				case "������_���_����������":
					code_partner = (long)val;
					break;
				case "����_������_������":
					date_give = (DateTime)val;
					tmp_date_give_is	= true;
					break;
				case "���_��������_������_������":
					code_giver = (long)val;
					break;
				case "����_������_����������":
					date_execute = (DateTime)val;
					tmp_date_execute_is	= true;
					break;
				case "���_��������_����������_������":
					code_execute = (long)val;
					break;
				case "���_��������_���������":
					code_archive = (long)val;
					break;
				case "����_��������":
					date_supply = (DateTime)val;
					tmp_date_supply_is	= true;
					break;
				// ���������������
				case "������������_�����_������":
					tmp_detail_name = (string)val;
					break;
				case "��������_������":
					tmp_requester_name = (string)val;
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

			CodeYear code_year;
			code_year.code = this.code;
			code_year.year = this.year;
			item.Tag				= code_year;
			item.Text				= this.code.ToString();
			item.SubItems.Add(this.date.ToString());
			item.SubItems.Add(this.tmp_detail_name);
			item.SubItems.Add(this.quontity.ToString());
			if(this.tmp_date_perfomance_is == true)
				item.SubItems.Add(this.date_perfomance.ToShortDateString());
			else
				item.SubItems.Add("");
			item.SubItems.Add(this.tmp_partner_name);
			if(this.tmp_date_supply_is == true)
				item.SubItems.Add(this.date_supply.ToShortDateString());
			else
				item.SubItems.Add("");
			item.SubItems.Add(this.tmp_requester_name);
			if(this.guaranty == false)
				item.StateImageIndex = 0;
			else
				item.StateImageIndex = 1;
			item.BackColor = Color.Red;
			if(tmp_date_give_is == true)
			{
				item.BackColor = Color.Blue;
			}
			if(tmp_date_execute_is == true)
			{
				item.BackColor = Color.Green;
			}
		}
	}
}
