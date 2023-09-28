using System;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DrReprot.
	/// </summary>
	public class DrReprot
	{
		public class Report
		{
			public int card_count;				// ������� ��������
			public float spare_parts;			// ����� ��������� ��������� �� �����-�������
			public float spare_parts_input;		// ����� ����������� �� ������� ��������� ��������� �� �����-�������
			public float spare_oil;				// ����� ����� ��������� �� �����-�������
			public float spare_oil_input;		// ����� ����������� �� ������� ����� ��������� �� �����-�������
			public float works_to;				// ����� ����� �� ������������ ������������
			public float works_labor;			// ����� �����
			public float works_wash;			// ����� ����

			public float work_labor_time;		// ��������� �������� �� �������
			public float work_to_time;			// ��������� �������� ��
			public float work_sp_time;			// ��������� ������� �� ������ ������� (�����)


			public float[] works_gar_cost;		// ����� ��������� ����� �� ��������
			public float[] works_gar_time;		// ����� ���������, ����������� �� ������ �� ��������


			public Report()
			{
				works_gar_time = new float[100];
				works_gar_cost = new float[100];

				card_count			= 0;
				spare_parts			= 0.0F;
				spare_parts_input	= 0.0F;
				spare_oil			= 0.0F;
				spare_oil_input		= 0.0F;
				works_to			= 0.0F;
				works_labor			= 0.0F;
				works_wash			= 0.0F;

				work_labor_time		= 0.0F;
				work_to_time		= 0.0F;
				work_sp_time		= 0.0F;
			}
		}

		public Report report;

		public DrReprot()
		{
			report = new Report();
		}

		public bool FillReport(ArrayList array)
		{
			foreach (object o in array)
			{
				DtCard card = (DtCard)o;
				AddCard(card);
			}
			return true;
		}

		public void AddCard(DtCard card)
		{
			long card_number = (long)card.GetData("�����_��������");
			int card_year = (int)card.GetData("���_��������");
			// ��������� �������� � ������
			report.card_count++;		// ����������� ������� ��������
			// �������� ��������� � �������� �������� ������
			ArrayList parts = new ArrayList();
			DbSqlCardDetail.SelectInArray(card, parts);
			AnalizeParts(parts);
			// �������� ����������� �� �������� �����
			ArrayList works = new ArrayList();
			DbSqlCardWork.SelectInArray(card, works);
			AnalizeWorks(works);
		}

		public void AnalizeParts(ArrayList array)
		{
			foreach (object o in array)
			{
				AddDetail((DtCardDetail)o);
			}
		}
		public void AddDetail(DtCardDetail detail)
		{
			report.spare_parts			+= detail.DetailSummCash;
			report.spare_parts_input	+= detail.DetailSummCashInput;
			report.spare_oil			+= detail.DetailSummOilCash;
			report.spare_oil_input		+= detail.DetailSummOilCashInput;
		}
		public void AnalizeWorks(ArrayList array)
		{
			foreach (object o in array)
			{
				AddWork((DtCardWork)o);
			}
		}
		public void AddWork(DtCardWork work)
		{
			long type;
			report.works_labor				+= work.WorkLaborSummCash;
			report.works_to					+= work.WorkToSummCash;
			report.works_wash				+= work.WorkWashSummCash;

			if((bool)work.GetData("��������_��������_������")!=true)
			{
				// ��������� ������ ���� �� ��������
				report.work_labor_time			+= work.WorkNV;
			}

			if((bool)work.GetData("��������_��������_������")==true)
			{
				type = (long)work.GetData("��������_���_��������_������");
				report.works_gar_time[type] += work.WorkNV;
				report.works_gar_cost[type] += work.WorkSumm;
			}
		}
	}
}
