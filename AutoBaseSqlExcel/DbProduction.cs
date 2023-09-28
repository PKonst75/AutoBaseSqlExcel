using System;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbProduction.
	/// </summary>
	public class DbProduction
	{
		struct Analiz
		{
			public float	cash_to;			// ������ �� ��
			public float	to_count;			// ���������� ��
			public float	cash;				// ������ �� ���������� ������
			public float	discount_to;		// ������ �� ��
			public float	discount_cash;		// ������ �� ���������� �������
			public float	wash;				// ������� �� �����
			public float	discount_wash;		// ������ �� �����
			public int		wash_count;			// ���������� ����
			public float	cash_hour;			// ������ �� ����������� ������
			public float	discount_cash_hour;	// ������ �� ����������� ������
			public float	hour;				// ������������ ���������
			public int		ppp_count;			// ���������� ������������
			public int		ppp_cash;			// ������ �� ������������
			public float	guaranty_cash_to;	// �� ������ ������
			public float	guaranty_cash;		// ���������� ������ �� ��������
			public float	guaranty_cash_hour;	// ������ �� ����������� ������ �� ��������
			public float	guaranty_hour;		// ������� ���������� ��������� �� ��������
			public float	guaranty_wash_count;// ����� �� ��������
			public int		guaranty_ppp_count;	// ���������� ������������ �� ��������
			public int		guaranty_ppp_cash;	// ������ �� ������������ ���������� �� ��������
			public float[]	guaranty_cash_tos;		// ��� �������� �� ����� ��������
			public float[]	guaranty_hours;			// ��� �������� �� ����� ��������
			public float[]	guaranty_cashs;			// ��� �������� �� ����� ��������
			public float[]	guaranty_cash_hours;	// ��� �������� �� ����� ��������
		}
		Analiz				analiz_cash;
		Analiz				analiz_cashless;
		Analiz				analiz_inner;
		Analiz				analiz_ppp;

		public DbProduction(DateTime start_date, DateTime end_date, long workshop)
		{
			// ������������� ��������
			analiz_cash.guaranty_cash_tos			= new float[100];
			analiz_cash.guaranty_cashs				= new float[100];
			analiz_cash.guaranty_hours				= new float[100];
			analiz_cash.guaranty_cash_hours			= new float[100];
			analiz_cashless.guaranty_cash_tos		= new float[100];
			analiz_cashless.guaranty_cashs			= new float[100];
			analiz_cashless.guaranty_hours			= new float[100];
			analiz_cashless.guaranty_cash_hours		= new float[100];
			analiz_inner.guaranty_cash_tos			= new float[100];
			analiz_inner.guaranty_cashs				= new float[100];
			analiz_inner.guaranty_hours				= new float[100];
			analiz_inner.guaranty_cash_hours		= new float[100];
			analiz_ppp.guaranty_cash_tos			= new float[100];
			analiz_ppp.guaranty_cashs				= new float[100];
			analiz_ppp.guaranty_hours				= new float[100];
			analiz_ppp.guaranty_cash_hours			= new float[100];

			// �������� ����������� ��� ������� �/� ������
			ArrayList works = new ArrayList();
			DbSqlCardWork.SelectInArray(start_date, end_date, workshop, works);

			// ��������� ������ �� �������
			foreach(object o in works)
			{
				DtCardWork work = (DtCardWork)o;
				if((bool)work.GetData("������") == true)
				{
					// ���������� �����-�����
					analiz_cashless = AnalizeWork(analiz_cashless, work);
				}
				if((bool)work.GetData("����������_��������") == true)
				{
					// ����������� ����-�����
					if((long)work.GetData("��������_������_���_����������") == 399 ||(long)work.GetData("��������_������_���_����������") == 1088)
					{
						// ������������� ����������
						analiz_ppp = AnalizeWork(analiz_ppp, work);
					}
					else
					{
						// ������� ����������
						analiz_inner = AnalizeWork(analiz_inner, work);
					}
				}
				if((bool)work.GetData("����������_��������") == false && (bool)work.GetData("������") == false)
				{
					// ������� �����-����� �� ������
					analiz_cash = AnalizeWork(analiz_cash, work);
				}
			}
		}
		private float Summ(DtCardWork wrk)
		{
			if((float)wrk.GetData("������������_��������_������") == 0.0F)
				return (float)wrk.GetData("��������_��������_������")*(float)wrk.GetData("����������_��������_������");
			else
				return (float)wrk.GetData("������������_��������_������")*(float)wrk.GetData("��������_��������_������")*(float)wrk.GetData("����������_��������_������");
		}
		private float SummDiscount(DtCardWork wrk)
		{
			if((float)wrk.GetData("������������_��������_������") == 0.0F)
				return (float)wrk.GetData("��������_��������_������")*(float)wrk.GetData("����������_��������_������") / 100.0F * (float)wrk.GetData("������_��������_������");
			else
				return (float)wrk.GetData("������������_��������_������")*(float)wrk.GetData("��������_��������_������")*(float)wrk.GetData("����������_��������_������") / 100.0F * (float)wrk.GetData("������_��������_������");
		}
		private float Hour(DtCardWork wrk)
		{
			if((float)wrk.GetData("������������_��������_������") == 0.0F)
				return (float)0.0F;
			else
				return (float)wrk.GetData("������������_��������_������")*(float)wrk.GetData("����������_��������_������");
		}

		private Analiz AnalizeWork(Analiz analiz, DtCardWork wrk)
		{
			// �������� �� ������������
			if((long)wrk.GetData("������_���_����������_������������")== 188)
			{
				if((bool)wrk.GetData("��������_��������_������") == false)
					analiz.ppp_count++;
				else
				{
					analiz.guaranty_ppp_count++;
				}
				return analiz;
			}
			// �������� �� �����
			if((long)wrk.GetData("������_���_����������_������������")== 722)
			{
				if((bool)wrk.GetData("��������_��������_������") == false)
				{
					analiz.wash_count++;
					analiz.wash	+= Summ(wrk);
					analiz.discount_wash += SummDiscount(wrk);
				}
				else
					analiz.guaranty_wash_count++;
				return analiz;
			}
			// �������� �� ��
			if((long)wrk.GetData("�����������_���") == 1)
			{
				analiz.to_count ++;
				if((bool)wrk.GetData("��������_��������_������") == false)
				{
					analiz.cash_to	+= Summ(wrk);
					analiz.discount_to += SummDiscount(wrk);
				}
				else
				{
					analiz.guaranty_cash_to	+= Summ(wrk);
					analiz.guaranty_cash_tos[(int)(long)wrk.GetData("��������_���_��������_������")] += Summ(wrk);
				}
				return analiz;
			}


			if((bool)wrk.GetData("��������_��������_������") == false)
			{		
				if((float)wrk.GetData("������������_��������_������") == 0.0F)
				{
					analiz.cash				+= Summ(wrk);
					analiz.discount_cash	+= SummDiscount(wrk);
				}					
				else
				{
					analiz.hour					+= Hour(wrk);
					analiz.cash_hour			+= Summ(wrk);
					analiz.discount_cash_hour	+= SummDiscount(wrk);
				}
			}
			else
			{
				if((float)wrk.GetData("������������_��������_������") == 0.0F)
				{
					analiz.guaranty_cash	+= Summ(wrk);
					analiz.guaranty_cashs[(int)(long)wrk.GetData("��������_���_��������_������")] += Summ(wrk);
				}					
				else
				{
					analiz.guaranty_hour		+= Hour(wrk);
					analiz.guaranty_cash_hour	+= Summ(wrk);
					analiz.guaranty_hours[(int)(long)wrk.GetData("��������_���_��������_������")] += Hour(wrk);
					analiz.guaranty_cash_hours[(int)(long)wrk.GetData("��������_���_��������_������")] += Summ(wrk);
				}
			}
			return analiz;
		}

		public float Cash
		{
			get
			{
				float cash = analiz_cash.cash_to + analiz_cash.cash + analiz_cash.cash_hour;
				cash = cash - analiz_cash.discount_to - analiz_cash.discount_cash - analiz_cash.discount_cash_hour;
				cash = cash + analiz_cashless.cash_to + analiz_cashless.cash + analiz_cashless.cash_hour;
				cash = cash - analiz_cashless.discount_to - analiz_cashless.discount_cash - analiz_cashless.discount_cash_hour;
				return cash;
			}
		}
	}
}
