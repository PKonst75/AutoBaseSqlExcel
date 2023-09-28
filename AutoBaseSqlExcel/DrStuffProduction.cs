using System;
using System.Collections;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DrStuffProduction.
	/// </summary>
	public class DrStuffProduction
	{
		public ArrayList		works;				// ������ ������ �����

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

				if (val == 0.0F)
				{
					// ��������� �������������� ������ ������-������
					double local_sp			= Math.Round(GetSPHours(card_work),2);
					cash_sp					+= Math.Round(price, 2) * Math.Round(quontity, 2) / count;
					if(local_sp == 0.0F)
					{
						cash_sp_nohours		+= Math.Round(price, 2) * Math.Round(quontity, 2) / count;
						count_sp_nohours	++;
					}
					else
					{
						hours_sp			+= local_sp * Math.Round(quontity, 2) / count;
					}
				}
				else
				{
				
					cash_hours	+= Math.Round(val, 2) * Math.Round(price, 2) * Math.Round(quontity, 2) / count;
					hours		+= Math.Round(val, 2) * Math.Round(quontity, 2) / count;
				}
			}

			public static float GetSPHours(DtCardWork element)
			{
				// ��������� ������ � �� ������ ������
				long code_work = (long)element.GetData("���_������������_��������_������");
				DtWork work = DbSqlWork.Find(code_work);

				// ��� ������ ���� ������ ��
				float local_nv	= (float)work.GetData("��");
				if(local_nv != 0.0F)
				{
					element.SetData("������_�����_��", local_nv);
					return local_nv;
				}
				
				long code_collection = (long)work.GetData("������_���_���������");
				float local_sp = 0.0F;
				if(code_collection !=  0)
				{
					// ���� ���������
					element.SetData("������_���_���������", code_collection);
							
					ArrayList array = new ArrayList();
					DbSqlWorkCollectionItem.SelectInArray(array, code_collection);
					foreach(DtWorkCollectionItem elm in array)
					{
						local_sp += (float)elm.GetData("������������_���������_�������");
					}
					element.SetData("������_�����_��", local_sp);		
				}
				return local_sp;
			}
		}

		public Production	card_cash		= new Production();	// ��������� �� ������������ �������
		public Production	card_guaranty	= new Production();	// ��������� �� ��������
		public Production	card_inner		= new Production();	// ��������� �� ���������� �����-�������

		public DrStuffProduction(long code, int year, int month)
		{
			// �������� ������ �����, �������� � ������ ������ �� ���������� ����������
			works = new ArrayList();
			DbSqlCardWork.SelectInArray(code, year, month, works);

			// ����������� ���������� ������
			foreach(object o in works)
			{
				DtCardWork work = (DtCardWork)o;
				DtCard card		= DbSqlCard.Find((long)work.GetData("�����_��������_��������_������"), (int)work.GetData("���_��������_��������_������"));
				if(card == null)
				{
					MessageBox.Show("������ ���");
					MessageBox.Show(work.GetData("�����_��������_��������_������").ToString());
					MessageBox.Show(work.GetData("���_������������_��������_������").ToString());
					MessageBox.Show(code.ToString());
				}
				if((bool)work.GetData("��������_��������_������") == true)
				{
					// ����������� ������
					card_guaranty.AddCardWork(work);
				}
				else
				{
					// �� ��������
					if((bool)card.GetData("����������_��������") == true)
					{
						// ���������� �����-�����
						card_inner.AddCardWork(work);
					}
					else
					{
						// ������������ ������
						card_cash.AddCardWork(work);
					}
				}
			}
		}

		public DrStuffProduction(long code, DateTime start_date, DateTime end_date)
		{
			// �������� ������ �����, �������� � ������ ������ �� ���������� ����������
			works = new ArrayList();
			DbSqlCardWork.SelectInArray(code, start_date, end_date, works);

			// ����������� ���������� ������
			foreach(object o in works)
			{
				DtCardWork work = (DtCardWork)o;
				DtCard card		= DbSqlCard.Find((long)work.GetData("�����_��������_��������_������"), (int)work.GetData("���_��������_��������_������"));
				if(card == null)
				{
					MessageBox.Show("������ ���");
					MessageBox.Show(work.GetData("�����_��������_��������_������").ToString());
					MessageBox.Show(work.GetData("���_������������_��������_������").ToString());
					MessageBox.Show(code.ToString());
				}
				if((bool)work.GetData("��������_��������_������") == true)
				{
					// ����������� ������
					card_guaranty.AddCardWork(work);
				}
				else
				{
					// �� ��������
					if((bool)card.GetData("����������_��������") == true)
					{
						// ���������� �����-�����
						card_inner.AddCardWork(work);
					}
					else
					{
						// ������������ ������
						card_cash.AddCardWork(work);
					}
				}
			}
		}
	}
}
