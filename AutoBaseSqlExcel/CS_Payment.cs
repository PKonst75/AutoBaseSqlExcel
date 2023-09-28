using System;
using System.Collections;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// ��������� ������ ����������� ������
	/// </summary>
	public class CS_Payment
	{
		public struct Pair
		{
			public long code;
			public int year;
		}
		// ���� ��� ����������� �������� � ���� ������
		public long		code;				// ��� �������
		public int		year;				// ���
		// ��������� �������
		public DateTime	date;				// ���� �������
		public long		code_department;	// ����� �����
		public long		code_workshop;		// ������������� �������
		public float	summ;				// ����� �������
		// �������������� ��������� �������
		public string	comment;			// ����������
		public long		card_number;		// ����� ��������
		public int		card_year;			// ��� ��������
		public long		code_auto;			// ��� ����������
		public long		code_partner;		// ��� �����������
		// ��������� ������� �������
		public long		supervisor_check;	// ������ ��������
		// ��������� ��������� ��� ����������� ������
		public string		str_warrant;		// �����-�����
		public string		str_auto;			// ����������
		public string		str_partner;		// ����������
		public string		str_department;		// ����� �����
		public string		str_workshop;		// ������������� �������
		// ��������� ������
		bool		flag_error;			// ���� ������� ������
		bool		flag_warning;		// ���� ������� ��������������
		ArrayList	list_error;			// ������ ������
		ArrayList	list_warning;		// ������ ��������������

		public void AddError(string error)
		{
			flag_error		= true;
			if(list_error == null) list_error = new ArrayList();
			list_error.Add(error);
		}

		public void AddWarning(string warning)
		{
			flag_warning	= true;
			if(list_warning == null) list_warning = new ArrayList();
			list_warning.Add(warning);
		}

		public CS_Payment()
		{
			code			= 0L;
			year			= 0;

			date			= DateTime.Now;
			code_department	= 0;
			code_workshop	= 0;
			summ			= 0.0F;
			comment			= "";
			card_number		= 0L;
			card_year		= 0;
			code_auto		= 0L;
			code_partner	= 0L;

			supervisor_check	= 0L;

			str_warrant		= "";
			str_auto		= "";
			str_partner		= "";
			str_department	= "";
			str_workshop	= "";
		}

		public CS_Payment(long department)
		{
			code			= 0L;
			year			= 0;

			date			= DateTime.Now;
			code_department	= department;
			code_workshop	= 0;
			summ			= 0.0F;
			comment			= "";
			card_number		= 0L;
			card_year		= 0;
			code_auto		= 0L;
			code_partner	= 0L;

			supervisor_check = 0L;

			str_warrant		= "";
			str_auto		= "";
			str_partner		= "";
			str_department	= "";
			str_workshop	= "";

			if(department == 1)
			{
				str_department = "��, ������, �����";
			}
			if(department == 2)
			{
				str_department = "��������";
			}
		}

		public DtCard Card
		{
			set
			{
				card_number		= value.Number;
				card_year		= value.Year;
				// ���������� ��������� ������
				DtCard card = DbSqlCard.Find(card_number, card_year);
				if(card == null)
				{
					AddError("�� ������� ��������");
					return;
				}
				str_warrant = card.WarrantNumber.ToString() + " / " + card.Year.ToString();
				// ��������� ������ �����-������
				short status = (short)card.State;
				if (status == 0)
				{
					AddError("������ ���������� ���������� ��������");
					return;
				}
				// ���� ����������
				code_auto = card.CodeAuto;// (long)card.GetData("����������_��������");
				if(code_auto != 0)
				{
					DtAuto auto = DbSqlAuto.Find(code_auto);
					if(auto == null)
					{
						AddError("�� ������ ����������");
						return;
					}
					str_auto = auto.Txt();
				}
				// ���� �����������
				code_partner = card.CodeOwner;
				if(code_partner != 0)
				{
					DtPartner partner	= DbSqlPartner.Find(code_partner);
					if(partner == null)
					{
						AddError("�� ������ ��������");
						return;
					}
					str_partner = partner.GetTitle();
				}
				// ���� �������������
				code_workshop = card.CodeWorkshop;
				if(code_workshop == 0)
				{
					AddError("�� ������� �������������");
					return;
				}
				else
				{
					DtWorkshop workshop = DbSqlWorkshop.Find(code_workshop);
					if(workshop == null)
					{
						AddError("�� ������� �������������");
						return;
					}
					str_workshop = workshop.Txt();
				}
				// ������������� ������ �������
				if(code_department == 0)
				{
					AddError("�� ������ ����� �����");
				}
				// ��������� �� ������� �����
				if(code_department == 1)
				{
					// ������ � �����
					summ = card.SummWorkPay() + card.SummDetailOilPay();
				}
				if(code_department == 2)
				{
					// ��������
					summ = card.SummDetailPay();
				}
			}
		}

		public bool CheckError()
		{
			if(flag_error == false) return true;
			foreach(object o in list_error)
			{
				string str = (string)o;
				MessageBox.Show(str);
			}
			flag_error = false;
			return false;
		}
		public void CheckElement()
		{
			if(summ == 0)
			{
				flag_error = true;
				AddError("������ ��������� ������� ������");
			}
			if(code_department == 0)
			{
				flag_error = true;
				AddError("������ ��������� ������ ��� ������ �����");
			}
			if(code_workshop == 0)
			{
				flag_error = true;
				AddError("������ ��������� ������ ��� �������������");
			}
		}

		public DtWorkshop Workshop
		{
			set
			{
				// ���� �������������
				code_workshop = (long)value.GetData("���_���");
				if(code_workshop == 0)
				{
					AddError("�� ������� �������������");
					return;
				}
				else
				{
					DtWorkshop workshop = DbSqlWorkshop.Find(code_workshop);
					if(workshop == null)
					{
						AddError("�� ������� �������������");
						return;
					}
					str_workshop = workshop.Txt();
				}
			}
		}

		public DtAuto Auto
		{
			set
			{
				// ���� ����������
				code_auto		= (long)value.GetData("���_����������");
				if(code_auto != 0)
				{
					DtAuto auto = DbSqlAuto.Find(code_auto);
					if(auto == null)
					{
						AddError("�� ������ ����������");
						return;
					}
					str_auto = auto.Txt();
				}
			}
		}

		public void SetTNode_Supervisor(TreeNode node)
		{
			string text = summ.ToString() + " (" + date.ToShortDateString() + ")";
			node.Text	= text;
		}

		public void SetLVItem(ListViewItem item)
		{
			item.SubItems.Clear();

			Pair pair = new Pair();
			pair.code		= code;
			pair.year		= year;
			item.Tag		= pair;

			item.Text		= date.ToLongTimeString();
			item.SubItems.Add(code_department.ToString());
			item.SubItems.Add(summ.ToString());
			
			// ����� �������������� ������ � ���������� �����
			DtWorkshop workshop = DbSqlWorkshop.Find(code_workshop);
			DtCard card = DbSqlCard.Find(card_number, card_year);
			DtAuto auto = DbSqlAuto.Find(code_auto);

			string workshop_txt = "";
			string card_txt = "";
			string auto_txt = "";
			if(workshop != null) workshop_txt = (string)workshop.GetData("������������_���");
			if(card != null) card_txt = card.Number.ToString() + "/" + card.Year.ToString();
			if(auto != null) auto_txt = auto.Txt();
			item.SubItems.Add(workshop_txt);
			item.SubItems.Add(card_txt);
			item.SubItems.Add(auto_txt);

			if(supervisor_check == 0)
				item.StateImageIndex = 0;
			else
				item.StateImageIndex = 1;
		}
	}
}
