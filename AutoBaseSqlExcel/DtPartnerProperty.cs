using System;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DtPartnerProperty.
	/// </summary>
	public class DtPartnerProperty
	{
		private long	code_partner;
		private bool	cashless;
		private float	discount;
		private string	comment;
		private long	card_number;

		private string	tmp_partner_name;

		public DtPartnerProperty()
		{
			code_partner		= 0;
			cashless			= false;
			discount			= 0.0f;
			comment				= "";
			card_number			= 0;

			tmp_partner_name	= "";
		}

		public DtPartnerProperty(DtPartnerProperty element)
		{
			code_partner		= element.code_partner;
			cashless			= element.cashless;
			discount			= element.discount;
			comment				= element.comment;
			card_number			= element.card_number;

			tmp_partner_name	= element.tmp_partner_name;
		}

		#region ������ � ���������� - ���������
		public object GetData(string data)
		{
			switch(data)
			{
				case "���_����������":
					return (object)(long)this.code_partner;
				case "������":
					return (object)(bool)this.cashless;
				case "������":
					return (object)(float)this.discount;
				case "����������":
					return (object)(string)this.comment;
				case "�����_�����":
					return (object)(long)this.card_number;
				default:
					return (object)null;
			}
		}
		public void SetData(string data, object val)
		{
			switch(data)
			{
				case "���_����������":
					this.code_partner = (long)val;
					break;
				case "����������":
					this.code_partner = (long)((DbPartner)val).Code;
					this.tmp_partner_name	= (string)((DbPartner)val).NameShort;
					break;
				case "������":
					this.cashless	= (bool)val;
					break;
				case "������":
					this.discount	= (float)val;
					break;
				case "����������":
					this.comment	= (string)val;
					break;
				case "�����_�����":
					this.card_number	= (long)val;
					break;
				default:
					break;
			}
		}
		public long CodePartner
		{
			get
			{
				return code_partner;
			}
			set
			{
				code_partner = value;
			}
		}
		public float Discount
		{
			get
			{
				return discount;
			}
			set
			{
				discount = value;
			}
		}
		public long CardNumber
		{
			get
			{
				return card_number;
			}
			set
			{
				card_number = value;
			}
		}
		public bool Cashless
		{
			get
			{
				return cashless;
			}
			set
			{
				cashless = value;
			}
		}
		public string PartnerName
		{
			get
			{
				return tmp_partner_name;
			}
			set
			{
				tmp_partner_name = value;
			}
		}
		public string Comment
		{
			get
			{
				return comment;
			}
			set
			{
				comment = value;
			}
		}
		#endregion

		#region ����������� ���������� � �����
		public string CashlessTxt
		{
			get
			{
				return DbSql.BoolTxt(cashless);
			}
		}
		public string DiscountTxt
		{
			get
			{
				return DbSql.FloatTxt(discount);
			}
		}
		public string CardNumberTxt
		{
			get
			{
				return DbSql.LongTxt(card_number);
			}
		}
		#endregion

		// �������� ������������ ������
		public bool Check(string data)
		{
			switch(data)
			{
				case "���_����������":		// ������ ����
					if(code_partner <= 0) return false;
					return true;
				case "������":				// ������ ��� ����� ����, ������ 30
					if(discount < 0 || discount > 30) return false;
					return true;
				case "�����_�����":			// ������ ��� ����� ���� 
					if(card_number < 0) return false;
					return true;
				case "����������":			// ����� ������ 1024, ��������
					if(comment.Length > 1024) return false;
					if(comment.Length == 0) return false;
					return true;
				default:
					return false;
			}
		}
		// ���������� ������ � ����������� ����
		public bool Update(string data)
		{
			string txt;
			switch(data)
			{
				case "������":		// �� 0 �� 30
					if(discount < 0)
					{
						discount = 0;
						return true;
					}
					if(discount > 30)
					{
						discount = 30;
						return true;
					}
					return false;
				case "�����_�����":		// ������ ��� ����� ����
					if(card_number < 0)
					{
						card_number = 0;
						return true;
					}
					return false;
				case "����������":	// ����� ������ 1024
					txt = comment;
					if(comment.Length > 1024)
						txt = DtService.TrancateString(txt, 1024);
					txt = DtService.Trim(txt);
					if(txt == comment) return false;
					comment = txt;
					return true;
				default:
					return true;
			}
		}
		// ���������� �������� �����
		public void SetLVItem(ListViewItem item)
		{
			item.SubItems.Clear();		// ����� ������� ���������� ���������� � ���������
			item.Tag				= this.CodePartner;
			item.Text				= this.PartnerName;
			item.SubItems.Add(this.CashlessTxt);
			item.SubItems.Add(this.DiscountTxt);
			item.SubItems.Add(this.CardNumberTxt);
			item.SubItems.Add(this.Comment);
		}
	}
}
