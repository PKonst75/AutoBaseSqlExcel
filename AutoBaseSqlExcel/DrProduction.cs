using System;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DrProduction.
	/// </summary>
	public class DrProduction
	{
		public ArrayList service_consultants = new ArrayList();

		public class Production
		{
			public double	hours;				// ������������ ���������
			public double	hours_sp;			// ������������ ��������� (�� ������-�������)
			public double	cash_sp;			// ����������� �� ������-������� ������
			public double	cash_hours;			// ������������ ������
			public double	cash_sp_nohours;	// ������������ ������ �� ������ �������, ��� ����������� �� ����������
			public int		count_sp_nohours;	// ���������� ������ �������, ��� ����������� �� ����������

			public Production()
			{
				hours				= 0.0;
				hours_sp			= 0.0;
				cash_sp				= 0.0;
				count_sp_nohours	= 0;
				cash_hours			= 0.0;
			}

			public void AddCardWork(DtCardWork card_work)
			{
				// ����������� ������, ����������� �� ����������
				float quontity	= (float)card_work.GetData("����������_��������_������");	// ���������� ���������� ������
				float val		= (float)card_work.GetData("������������_��������_������");	// ���������� ���������� ������
				float price		= (float)card_work.GetData("��������_��������_������");		// ���������� ��������� ������/���������
				float count		= (int)card_work.GetData("����������_������������");		// ���������� ������������ ������
				count			= 1.0F;	// ��� ���������� �������� �������

				if (val == 0.0F)
				{
					// ��������� �������������� ������ ������-������
					double local_sp			= Math.Round(GetSPHours(card_work), 2);
					cash_sp					+= Math.Round(price, 2) * Math.Round(quontity, 2);// / count;
					if(local_sp == 0.0F)
					{
						cash_sp_nohours		+= Math.Round(price, 2) * Math.Round(quontity, 2);// / count;
						count_sp_nohours	++;
					}
					else
					{
						hours_sp			+= local_sp * Math.Round(quontity, 2);// / count;
					}
				}
				else
				{
					cash_hours	+= Math.Round(val, 2) * Math.Round(price, 2) * Math.Round(quontity, 2);// / count;
					hours		+= Math.Round(val, 2) * Math.Round(quontity, 2);// / count;
				}
			}

			public static double GetSPHours(DtCardWork element)
			{
				// ��������� ������ � �� ������ ������
				long code_work = (long)element.GetData("���_������������_��������_������");
				DtWork work = DbSqlWork.Find(code_work);

				// ��� ������ ���� ������ ��
				double local_nv	= Math.Round((float)work.GetData("��"), 2);
				if(local_nv != 0.0F)
				{
					element.SetData("������_�����_��", (float)local_nv);
					return local_nv;
				}
				
				long code_collection = (long)work.GetData("������_���_���������");
				double local_sp = 0.0F;
				if(code_collection !=  0)
				{
					// ���� ���������
					element.SetData("������_���_���������", code_collection);
							
					ArrayList array = new ArrayList();
					DbSqlWorkCollectionItem.SelectInArray(array, code_collection);
					foreach(DtWorkCollectionItem elm in array)
					{
						local_sp += Math.Round((float)elm.GetData("������������_���������_�������"), 2);
					}
					element.SetData("������_�����_��", (float)local_sp);		
				}
				return local_sp;
			}
		}

		public class ServiceConsultant
		{
			// ��������� �������� �� ������-�������������
			public long code;				// ��� ������-������������
			public string name;				// ������������ ������ ������������ (������������)
			public Production production;	// ��������� ������-������������
			
			public ServiceConsultant(DtStaff staff)
			{
				code	= (long)staff.GetData("���_��������");
				name	= (string)staff.GetData("�������_��������") + " " + (string)staff.GetData("���_��������") + " " + (string)staff.GetData("��������_��������");
				production = new Production();
			}			
		}

		public class CardWorkEx
		{
			public string txt_card;					// ����� �������� � ������, ���� ������
			public string txt_card_close_date;		// ���� �������� ������
			public string txt_service_consultant;	// ��������� ����� ������-������������
			public string txt_inner;				// ��������� ����������� �����-������
			public string txt_cashless;				// ��������� ������������ �����-������
			public string txt_work_guaranty;		// ��������� ����������� ������
			public string txt_work_code;			// ��� ������	
			public string txt_work_name;			// ������������ ������
			public string txt_work_count;			// ���������� �����
			public string txt_work_nv;				// ����� ������� ������
			public string txt_work_price;			// ��������� ���������
			public string txt_work_sp;				// ��� ������ ������ - ��������� �� ������ �������
			public string txt_work_nvsum;			// �������� ����� ���� ������� �� �������
			
			public CardWorkEx()
			{
				txt_card				= "";
				txt_card_close_date		= "";
				txt_service_consultant	= "";
				txt_inner				= "";
				txt_cashless			= "";
				txt_work_guaranty		= "";
				txt_work_code			= "";
				txt_work_name			= "";
				txt_work_count			= "";
				txt_work_nv				= "";
				txt_work_price			= "";
				txt_work_sp				= "";
				txt_work_nvsum			= "";
			}
			public void SetCard(DtCard card)
			{
				txt_card				= card.GetData("�����_�����_��������").ToString() + "/" + card.GetData("�����_��������").ToString() + "/" + ((DateTime)card.GetData("����_�����_������_��������")).ToShortDateString();
				txt_card_close_date		= ((DateTime)card.GetData("����_�����_������_��������")).ToShortDateString();
				txt_service_consultant	= card.GetData("������_���������_��������").ToString();
				if((bool)card.GetData("����������_��������") == true) txt_inner = "+";
				if((bool)card.GetData("�����������_��������") == true) txt_cashless = "+";
			}
			public void SetCardWork(DtCardWork card_work)
			{
				if((bool)card_work.GetData("��������_��������_������") == true)txt_work_guaranty = "+";
				txt_work_code		= (string)card_work.GetData("�����_�������_��������_������");
				txt_work_name		= (string)card_work.GetData("������������_��������_������");
				txt_work_count		= card_work.GetData("����������_��������_������").ToString();
				txt_work_nv			= card_work.GetData("������������_��������_������").ToString();
				txt_work_price		= card_work.GetData("��������_��������_������").ToString();
				if((float)card_work.GetData("������������_��������_������") == 0.0F)
				{
					txt_work_nvsum	= ((float)card_work.GetData("������_�����_��") * (float)card_work.GetData("����������_��������_������")).ToString();
					txt_work_sp		= card_work.GetData("������_�����_��").ToString();
				}
				else
				{
					txt_work_nvsum	= ((float)card_work.GetData("������������_��������_������") * (float)card_work.GetData("����������_��������_������")).ToString();
				}
			}
		}

		public Production	card_cash		= new Production();	// ��������� �� ������������ �������
		public Production	card_cashless	= new Production();	// ��������� �� ������������ �������, �� �������
		public Production	card_guaranty	= new Production();	// ��������� �� ��������
		public Production	card_inner		= new Production();	// ��������� �� ���������� �����-�������
		public ArrayList	works_ex		= new ArrayList();

		public DrProduction(ArrayList cards)
		{

			// ���������� ������ ����� �� ������ ��������
			foreach(DtCard element in cards)
			{
				DtCard card								= DbSqlCard.Find((long)element.GetData("�����_��������"), (int)element.GetData("���_��������"));
				ServiceConsultant service_consultant	= null;
				long service_consultant_code			= (long)card.GetData("������_���������_��������");
				bool cashless							= (bool)card.GetData("�����������_��������");
				bool inner								= (bool)card.GetData("����������_��������");
				// ��������� ������� ������ �� ������ ������������
				foreach(ServiceConsultant o in service_consultants)
				{
					if(service_consultant_code == o.code)
						service_consultant = o;
				}
				if(service_consultant == null)
				{
					DtStaff staff			= DbSqlStaff.Find(service_consultant_code);
					service_consultant		= new ServiceConsultant(staff);
					service_consultants.Add(service_consultant);
				}
				// �������� ������ ������ ������������, ���������� �����
				ArrayList works = new ArrayList();
				DbSqlCardWork.SelectInArray(card, works);
				// ����������� ���������� ������ ����� �� ��������
				foreach(DtCardWork work in works)
				{
					service_consultant.production.AddCardWork(work);		// ������ �� ������-������������
					if((bool)work.GetData("��������_��������_������") == false)
					{
						if(inner == true)
						{
							card_inner.AddCardWork(work);
						}
						else
						{
							if(cashless == true)
							{
								card_cashless.AddCardWork(work);
							}
							else
							{
								card_cash.AddCardWork(work);
							}
						}
					}
					else
					{
						card_guaranty.AddCardWork(work);
					}

					// ����� ��������� ������ � ������
					CardWorkEx work_ex = new CardWorkEx();
					work_ex.SetCard(card);
					work_ex.SetCardWork(work);
					works_ex.Add(work_ex);
				}
			}
		}
	}
}
