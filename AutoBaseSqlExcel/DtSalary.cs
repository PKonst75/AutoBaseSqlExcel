using System;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DtSalary.
	/// </summary>
	public class DtSalary
	{
		// ��������� ������
		public float		cash;
		public float		cash_hour;
		public float		cash_hour_count;
		public float		hour;
		public int			ppp_count;
		public int			to_count;
		public float		cash_to;
		public int			guarantee_ppp_count;
		public float		guarantee_hour;
		public float		guarantee_cash;
		public int			guarantee_to_count;
		public int			wash_count;

		// ��� ��������
		public float		salary_cash;
		public float		salary_cash_hour;
		public float		salary_hour;
		public float		salary_guarantee;
		public float		salary_ppp;
		public float		salary;

		public DtSalary(long code, DateTime start_date, DateTime end_date)
		{
			ArrayList works = new ArrayList();
			DbSqlCardWork.SelectInArray(code, start_date, end_date, works);

			cash							= 0.0F;		// ���������� ��������� ������ (� ������ ������ � ����������� �� ���������� ������������)
			cash_hour						= 0.0F;		// ���������� ������ �� ���������
			cash_hour_count					= 0.0F;		// ���������� ����������� ����� �� ���������
			hour							= 0.0F;		// ���������� ������������� ����������
			ppp_count						= 0;		// ���������� ����������� ������������� ����������
			to_count						= 0;		// ���������� ����������� ��
			cash_to							= 0.0F;		// ������ ���������� ������ �� ��
			guarantee_ppp_count				= 0;		// ���������� ����������� ������������, ���������� ��� ��������
			guarantee_cash					= 0.0F;		// ���������� ����������� ���������� ����� (����������� �� ���������� ������������)
			guarantee_hour					= 0.0F;		// ���������� ������������ ����������� ���������� (����������� �� ���������� ������������)
			guarantee_to_count				= 0;		// ���������� ����������� �� (���������� ��� ��������)

			wash_count						= 0;		// ���������� ����������� ����
			

			foreach(object o in works)
			{
				DtCardWork work = (DtCardWork)o;
				if (work.WorkType() == WORK_TYPE.WASH) wash_count++;
				//	if((long)work.GetData("������_���_����������_������������")== 722)
				//		{
				//			wash_count++;	// ����� ���������� ������������� ����
				//	}
				//if((bool)work.GetData("��������_��������_������") == false)
				if (work.GuaranteeFlag() == false)
				{
					//if((long)work.GetData("�����������_���")== 1)
					if((bool)work.IsTo() == true)
					{
						to_count++;
						if((float)work.GetData("������������_��������_������") == 0.0F)
						{
							float	summ		= (float)work.GetData("��������_��������_������")*(float)work.GetData("����������_��������_������");
							float	discount	= (float)work.GetData("��������_��������_������")*(float)work.GetData("����������_��������_������") / 100.0F * (float)work.GetData("������_��������_������");
							float	summ_person	= (summ - discount) / (float)(int)work.GetData("����������_������������");
							cash_to				+= summ_person;
						}
						else
						{
							float	summ		= (float)work.GetData("������������_��������_������")*(float)work.GetData("��������_��������_������")*(float)work.GetData("����������_��������_������");
							float	discount	= (float)work.GetData("������������_��������_������")*(float)work.GetData("��������_��������_������")*(float)work.GetData("����������_��������_������") / 100.0F * (float)work.GetData("������_��������_������");
							float	summ_person	= (summ - discount) / (float)(int)work.GetData("����������_������������");
							cash_to				+= summ_person;
						}
					}
					if((long)work.GetData("������_���_����������_������������")== 188)
					{
						ppp_count++;
					}
					else
					{
						if((float)work.GetData("������������_��������_������") == 0.0F)
						{
							float	summ		= (float)work.GetData("��������_��������_������")*(float)work.GetData("����������_��������_������");
							float	discount	= (float)work.GetData("��������_��������_������")*(float)work.GetData("����������_��������_������") / 100.0F * (float)work.GetData("������_��������_������");
							float	summ_person	= (summ - discount) / (float)(int)work.GetData("����������_������������");
							cash				+= summ_person;
						}
						else
						{
							float	summ		= (float)work.GetData("������������_��������_������")*(float)work.GetData("��������_��������_������")*(float)work.GetData("����������_��������_������");
							float	discount	= (float)work.GetData("������������_��������_������")*(float)work.GetData("��������_��������_������")*(float)work.GetData("����������_��������_������") / 100.0F * (float)work.GetData("������_��������_������");
							float	summ_person	= (summ - discount) / (float)(int)work.GetData("����������_������������");
							cash_hour			+= summ_person;
							if((float)work.GetData("��������_��������_������") == 0.0F)
							{
								// ������������� ���������, � ������ ������
								summ			= (float)work.GetData("������������_��������_������")*(float)work.GetData("����������_��������_������");
								discount		= (float)work.GetData("������������_��������_������")*(float)work.GetData("����������_��������_������") / 100.0F * (float)work.GetData("������_��������_������");
								hour			+= (summ - discount) /(float)(int)work.GetData("����������_������������");
							}
							else
							{
								cash_hour_count += (float)work.GetData("������������_��������_������")*(float)work.GetData("����������_��������_������")/(float)(int)work.GetData("����������_������������");
							}
						}
					}
				}
				else
				{
					//if((long)work.GetData("�����������_���")== 1)
					if((bool)work.IsTo() == true)
					{
						guarantee_to_count++;
					}
					if((long)work.GetData("������_���_����������_������������")== 188)
					{
						guarantee_ppp_count++;
					}
					else
					{
						if((float)work.GetData("������������_��������_������") == 0.0F)
						{
							float	summ		= (float)work.GetData("��������_��������_������")*(float)work.GetData("����������_��������_������");
							float	summ_person	= summ / (float)(int)work.GetData("����������_������������");
							guarantee_cash		+= summ_person;
						}
						else
						{
							guarantee_hour		+= (float)work.GetData("������������_��������_������")*(float)work.GetData("����������_��������_������")/(float)(int)work.GetData("����������_������������");
						}
					}
				}
			}

			// ������ ���������� �����
			salary_cash			= 0.0F;
			salary_cash_hour	= 0.0F;
			salary_hour			= 0.0F;
			salary_guarantee	= 0.0F;
			salary_ppp			= 0.0F;
			salary				= 0.0F;

			float coef_cash			= 0.16F;
			float coef_cash_hour	= 0.3F;
			float coef_hour			= 80.0F;
			float coef_guaranty		= 160.0F;
			float coef_ppp			= 150.0F;

			salary_cash			= cash * coef_cash;
			salary_cash_hour	= cash_hour * coef_cash_hour;
			salary_hour			= hour * coef_hour;
			salary_guarantee	= guarantee_hour * coef_guaranty;
			salary_ppp			= (ppp_count + guarantee_ppp_count) * coef_ppp;
			salary				= salary_cash + salary_cash_hour + salary_hour + salary_guarantee + salary_ppp;
		}

		public DtSalary(long code, int year, int month)
		{
			ArrayList works = new ArrayList();
			DbSqlCardWork.SelectInArray(code, year, month, works);

			cash							= 0.0F;		// ���������� ��������� ������ (� ������ ������ � ����������� �� ���������� ������������)
			cash_hour						= 0.0F;		// ���������� ������ �� ���������
			cash_hour_count					= 0.0F;		// ���������� ����������� ����� �� ���������
			hour							= 0.0F;		// ���������� ������������� ����������
			ppp_count						= 0;		// ���������� ����������� ������������� ����������
			to_count						= 0;		// ���������� ����������� ��
			cash_to							= 0.0F;		// ������ ���������� ������ �� ��
			guarantee_ppp_count				= 0;		// ���������� ����������� ������������, ���������� ��� ��������
			guarantee_cash					= 0.0F;		// ���������� ����������� ���������� ����� (����������� �� ���������� ������������)
			guarantee_hour					= 0.0F;		// ���������� ������������ ����������� ���������� (����������� �� ���������� ������������)
			guarantee_to_count				= 0;		// ���������� ����������� �� (���������� ��� ��������)

			wash_count						= 0;		// ���������� ����������� ����
			

			foreach(object o in works)
			{
				DtCardWork work = (DtCardWork)o;
				if((long)work.GetData("������_���_����������_������������")== 722)
				{
					wash_count++;	// ����� ���������� ������������� ����
				}
				if((bool)work.GetData("��������_��������_������") == false)
				{
					//if((long)work.GetData("�����������_���")== 1)
					if((bool)work.IsTo() == true)
					{
						to_count++;
						if((float)work.GetData("������������_��������_������") == 0.0F)
						{
							float	summ		= (float)work.GetData("��������_��������_������")*(float)work.GetData("����������_��������_������");
							float	discount	= (float)work.GetData("��������_��������_������")*(float)work.GetData("����������_��������_������") / 100.0F * (float)work.GetData("������_��������_������");
							float	summ_person	= (summ - discount) / (float)(int)work.GetData("����������_������������");
							cash_to				+= summ_person;
						}
						else
						{
							float	summ		= (float)work.GetData("������������_��������_������")*(float)work.GetData("��������_��������_������")*(float)work.GetData("����������_��������_������");
							float	discount	= (float)work.GetData("������������_��������_������")*(float)work.GetData("��������_��������_������")*(float)work.GetData("����������_��������_������") / 100.0F * (float)work.GetData("������_��������_������");
							float	summ_person	= (summ - discount) / (float)(int)work.GetData("����������_������������");
							cash_to				+= summ_person;
						}
					}
					if((long)work.GetData("������_���_����������_������������")== 188)
					{
						ppp_count++;
					}
					else
					{
						if((float)work.GetData("������������_��������_������") == 0.0F)
						{
							float	summ		= (float)work.GetData("��������_��������_������")*(float)work.GetData("����������_��������_������");
							float	discount	= (float)work.GetData("��������_��������_������")*(float)work.GetData("����������_��������_������") / 100.0F * (float)work.GetData("������_��������_������");
							float	summ_person	= (summ - discount) / (float)(int)work.GetData("����������_������������");
							cash				+= summ_person;
						}
						else
						{
							float	summ		= (float)work.GetData("������������_��������_������")*(float)work.GetData("��������_��������_������")*(float)work.GetData("����������_��������_������");
							float	discount	= (float)work.GetData("������������_��������_������")*(float)work.GetData("��������_��������_������")*(float)work.GetData("����������_��������_������") / 100.0F * (float)work.GetData("������_��������_������");
							float	summ_person	= (summ - discount) / (float)(int)work.GetData("����������_������������");
							cash_hour			+= summ_person;
							if((float)work.GetData("��������_��������_������") == 0.0F)
							{
								// ������������� ���������, � ������ ������
								summ			= (float)work.GetData("������������_��������_������")*(float)work.GetData("����������_��������_������");
								discount		= (float)work.GetData("������������_��������_������")*(float)work.GetData("����������_��������_������") / 100.0F * (float)work.GetData("������_��������_������");
								hour			+= (summ - discount) /(float)(int)work.GetData("����������_������������");
							}
							else
							{
								cash_hour_count += (float)work.GetData("������������_��������_������")*(float)work.GetData("����������_��������_������")/(float)(int)work.GetData("����������_������������");
							}
						}
					}
				}
				else
				{
					//if((long)work.GetData("�����������_���")== 1)
					if((bool)work.IsTo() == true)
					{
						guarantee_to_count++;
					}
					if((long)work.GetData("������_���_����������_������������")== 188)
					{
						guarantee_ppp_count++;
					}
					else
					{
						if((float)work.GetData("������������_��������_������") == 0.0F)
						{
							float	summ		= (float)work.GetData("��������_��������_������")*(float)work.GetData("����������_��������_������");
							float	summ_person	= summ / (float)(int)work.GetData("����������_������������");
							guarantee_cash		+= summ_person;
						}
						else
						{
							guarantee_hour		+= (float)work.GetData("������������_��������_������")*(float)work.GetData("����������_��������_������")/(float)(int)work.GetData("����������_������������");
						}
					}
				}
			}

			// ������ ���������� �����
			salary_cash			= 0.0F;
			salary_cash_hour	= 0.0F;
			salary_hour			= 0.0F;
			salary_guarantee	= 0.0F;
			salary_ppp			= 0.0F;
			salary				= 0.0F;

			float coef_cash			= 0.16F;
			float coef_cash_hour	= 0.3F;
			float coef_hour			= 80.0F;
			float coef_guaranty		= 160.0F;
			float coef_ppp			= 150.0F;

			salary_cash			= cash * coef_cash;
			salary_cash_hour	= cash_hour * coef_cash_hour;
			salary_hour			= hour * coef_hour;
			salary_guarantee	= guarantee_hour * coef_guaranty;
			salary_ppp			= (ppp_count + guarantee_ppp_count) * coef_ppp;
			salary				= salary_cash + salary_cash_hour + salary_hour + salary_guarantee + salary_ppp;
		}
	}
}
