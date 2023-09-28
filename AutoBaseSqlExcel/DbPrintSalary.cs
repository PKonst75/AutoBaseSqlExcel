using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// ������ �������� �� ��������.
	/// </summary>
	public class DbPrintSalary:DbPrint
	{
		SolidBrush	draw_brush;
		Font		font_print;
		Font		font_large_bold;

		DtStaff		staff;
		// ����������������� ������
		string		title_staff;
		string		title_period;
		// ��������� ������
		float		cash;
		float		cash_hour;
		float		cash_hour_count;
		float		hour;
		int			ppp_count;
		int			guarantee_ppp_count;
		float		guarantee_hour;
		float		guarantee_cash;


		// ��� ��������
		float		salary_cash;
		float		salary_cash_hour;
		float		salary_hour;
		float		salary_guarantee;
		float		salary_ppp;
		float		salary;


		public DbPrintSalary(long code, int year, int month)
		{
			// ����������� ��� ������
			draw_brush		= new SolidBrush(Color.Black);
			font_print		= new Font("Arial", 10);
			font_large_bold	= new Font("Arial", 14, FontStyle.Bold);

			// ��������� ������ ���������
			staff = DbSqlStaff.Find(code);
			if(staff == null) return;

			// ����������������� ������
			title_staff		= staff.GetData("�������_��������") + " " + staff.GetData("���_��������") + " " + staff.GetData("��������_��������");
			title_period	= Month(month) + " " + year.ToString();

			// �������� ����������� ��� ������� �/� ������
			ArrayList works = new ArrayList();
			DbSqlCardWork.SelectInArray(code, year, month, works);

			cash							= 0.0F;		// ���������� ��������� ������ (� ������ ������ � ����������� �� ���������� ������������)
			cash_hour						= 0.0F;		// ���������� ������ �� ���������
			cash_hour_count					= 0.0F;		// ���������� ����������� ����� �� ���������
			hour							= 0.0F;		// ���������� ������������� ����������
			ppp_count						= 0;		// ���������� ����������� ������������� ����������
			guarantee_ppp_count				= 0;		// ���������� ����������� ������������, ���������� ��� ��������
			guarantee_cash					= 0.0F;		// ���������� ����������� ���������� ����� (����������� �� ���������� ������������)
			guarantee_hour					= 0.0F;		// ���������� ������������ ����������� ���������� (����������� �� ���������� ������������)
			

			foreach(object o in works)
			{
				DtCardWork work = (DtCardWork)o;
				if((bool)work.GetData("��������_��������_������") == false)
				{
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

		public override void  PrintPage(Graphics graph, int page)
		{
			// ��� ���������� �� ��������
			int offset = 0;

			offset = 10;
			PrintText(graph, "������ �� " + title_period, 10, 0 + offset, 100, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_large_bold, draw_brush, false);
			PrintText(graph, title_staff, 70, 0 + offset, 100, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_large_bold, draw_brush, false);
			PrintText(graph, "���������� ������", 10, 10 + offset, 100, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, "�� ���������", 10, 15 + offset, 100, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, "�� ����������� ���������", 10, 20 + offset, 100, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, "��������", 20, 25 + offset, 100, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, "���������� ������", 10, 30 + offset, 100, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, "���������", 10, 35 + offset, 100, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, "������������� ����������", 20, 40 + offset, 100, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, "����������", 10, 45 + offset, 100, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);

			PrintText(graph, cash.ToString(), 60, 10 + offset, 100, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, cash_hour.ToString() + " ( " + cash_hour_count.ToString() + " ) ", 60, 15 + offset, 100, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, hour.ToString(), 60, 20 + offset, 100, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, guarantee_cash.ToString(), 60, 30 + offset, 100, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, guarantee_hour.ToString(), 60, 35 + offset, 100, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, (ppp_count + guarantee_ppp_count).ToString(), 60, 45 + offset, 100, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);

			PrintText(graph, salary_cash.ToString(), 120, 10 + offset, 100, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, salary_cash_hour.ToString(), 120, 15 + offset, 100, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, salary_hour.ToString(), 120, 20 + offset, 100, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, salary_guarantee.ToString(), 120, 25 + offset, 100, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, salary_ppp.ToString(), 120, 40 + offset, 100, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, salary.ToString(),120, 50 + offset, 100, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_large_bold, draw_brush, false);

		}

		private string Month(int month)
		{
			switch(month)
			{
				case 1:
					return "������";
				case 2:
					return "�������";
				case 3:
					return "����";
				case 4:
					return "������";
				case 5:
					return "���";
				case 6:
					return "����";
				case 7:
					return "����";
				case 8:
					return "������";
				case 9:
					return "��������";
				case 10:
					return "�������";
				case 11:
					return "������";
				case 12:
					return "�������";
				default:
					return "������";
			}
		}
	}
}
