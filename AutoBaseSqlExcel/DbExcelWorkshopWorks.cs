using System;
using System.Collections;
using Excel = Microsoft.Office.Interop.Excel;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbExcelWorkshopWorks.
	/// </summary>
	public class DbExcelWorkshopWorks:DbExcel
	{
		public ArrayList	cards;
		public ArrayList	cards_open;
		DateTime			date_start;
		DateTime			date_stop;
		long				workshop;

		public DbExcelWorkshopWorks(DateTime start, DateTime stop, long ws)
		{
			workshop	= ws;
			date_start	= start;
			date_stop	= stop;

			cards		= new ArrayList();
			DbSqlCard.SelectCardClosedNumberWorkshop(cards, start, stop, ws);

			cards_open		= new ArrayList();
			DbSqlCard.SelectCardOpenNumberWorkshop(cards_open, ws);
		}

		override protected void TitleFormat(Excel.Worksheet ws)
		{
			// ���� ������
			FormatColumn(ws, "A1", 12, 8, "Left");
			CellText(ws, "A1", "���� ������", "Center", 10, true);
			// �����
			FormatColumn(ws, "B1", 8, 8, "Right", true);
			FormatColumnNumberFormat(ws, "B1", "# ##0,00");
			CellText(ws, "B1", "�����", "Center", 10, true);
			// ��� �������
			FormatColumn(ws, "C1", 4, 8, "Center");
			CellText(ws, "C1", "H�����", "Center", 10, true);
			// �����-�����
			FormatColumn(ws, "D1", 6, 8, "Left");
			CellText(ws, "D1", "�/�", "Center", 10, true);
			// ������
			FormatColumn(ws, "E1", 16, 8, "Left");
			CellText(ws, "E1", "������", "Center", 10, true);
			// ����� ������
			FormatColumn(ws, "F1", 8, 8, "Left");
			FormatColumnNumberFormat(ws, "F1", "@");
			CellText(ws, "F1", "�����", "Center", 10, true);

			// ������
			// ������������
			FormatColumn(ws, "G1", 48, 8, "Left");
			CellText(ws, "G1", "������������ ������", "Center", 10, true);
			// ����������
			FormatColumn(ws, "H1", 4, 8, "Right");
			CellText(ws, "H1", "�-��", "Center", 10, true);
			// ���������
			FormatColumn(ws, "I1", 8, 8, "Right");
			FormatColumnNumberFormat(ws, "I1", "# ##0,00");
			CellText(ws, "I1", "���������", "Center", 10, true);
		}

		override protected void DataToExcel(Excel.Worksheet ws, int start)
		{
			string row_last = "1";
			string row_txt;
			string cell_txt;
			int row = start;
			int row_summ = 2;
			string txt;

			CalculatorCard calc = new CalculatorCard(CALCULATOR_TYPE.CALCULATOR_PAY, VAT_TYPE.VAT_NON, 0);


			foreach(object o in cards)
			{
				// �����-�����
				DtCard card = (DtCard)o;
				// ��������� ������ ������ �� �����-������
				DtCard card_data = DbSqlCard.Find((long)card.GetData("�����_��������"), (int)card.GetData("���_��������"));
				// ��������� ������ �� ����������
				DtAuto auto_data	= DbSqlAuto.Find((long)card_data.CodeAuto/*GetData("����������_��������")*/);
				
				// ��������� ������ ����� �� �����-������
				ArrayList works = new ArrayList();
				DbSqlCardWork.SelectInArray(card_data, works);

				// ������������ ������ �����
				float summ	= 0.0F;
				foreach(DtCardWork o1 in works)
				{
					//DtCardWork work = (DtCardWork)o1;
					CalculatorResult res = calc.WorkCalculator.Calculate(o1);
					summ += res.SummDatabase;//summ	+= work.WorkSummCash;
				}

				// ����������� ������ �� �����-������ � Excel
				row_txt = row.ToString();
				// ���� ������ = ���� ��������
				cell_txt = "A" + row_txt;
				txt = card_data.GetData("����_�����_������_��������").ToString();
				CellText(ws, cell_txt, txt);
				// ����� ������
				cell_txt = "B" + row_txt;
				txt = summ.ToString();
				CellText(ws, cell_txt, txt);
				// ��� ������� (1 ���� �����������)
				cell_txt = "C" + row_txt;
				txt = "";
				if ((bool)card_data.GetData("�����������_��������"))
					txt = "��";
				CellText(ws, cell_txt, txt);
				// ����� �����-������
				cell_txt = "D" + row_txt;
				txt = card_data.GetData("�����_�����_��������").ToString();
				CellText(ws, cell_txt, txt);
				// ������ ���������� 
				cell_txt = "E" + row_txt;
				txt = auto_data.GetData("������").ToString();
				CellText(ws, cell_txt, txt);
				// ������ ���������� 
				cell_txt = "F" + row_txt;
				txt = auto_data.GetData("�����_�����").ToString();
				CellText(ws, cell_txt, txt);

				row++;
				foreach(DtCardWork o1 in works)
				{
					//DtCardWork work = (DtCardWork)o1;
					DtTxtCardWork txtCardWork = new DtTxtCardWork(o1);
					CalculatorResult res = calc.WorkCalculator.Calculate(o1);

					row_txt = row.ToString();
					// ������������ ������
					cell_txt = "G" + row_txt;
					txt = txtCardWork.WorkName;//txt = work.GetData("������������_��������_������").ToString();
					CellText(ws, cell_txt, txt);
					// ����������
					cell_txt = "H" + row_txt;
					txt = txtCardWork.OperationAmount;//txt = work.GetData("����������_��������_������").ToString();
					CellText(ws, cell_txt, txt);
					// ���������
					cell_txt = "I" + row_txt;
					txt = res.SummDatabase.ToString();//txt = work.WorkSummCash.ToString();
					CellText(ws, cell_txt, txt);

					row++;
				}
			}
		}

		protected void TitleFormat1(Excel.Worksheet ws)
		{
			// ���� ������
			FormatColumn(ws, "A1", 12, 8, "Left");
			CellText(ws, "A1", "���� ������", "Center", 10, true);
			// �����
			FormatColumn(ws, "B1", 8, 8, "Right", true);
			FormatColumnNumberFormat(ws, "B1", "# ##0,00");
			CellText(ws, "B1", "�����", "Center", 10, true);
			// ��� �������
			FormatColumn(ws, "C1", 4, 8, "Center");
			CellText(ws, "C1", "H�����", "Center", 10, true);
			// �����-�����
			FormatColumn(ws, "D1", 6, 8, "Left");
			CellText(ws, "D1", "�/�", "Center", 10, true);
			// ������
			FormatColumn(ws, "E1", 16, 8, "Left");
			CellText(ws, "E1", "������", "Center", 10, true);
			// ����� ������
			FormatColumn(ws, "F1", 8, 8, "Left");
			FormatColumnNumberFormat(ws, "F1", "@");
			CellText(ws, "F1", "�����", "Center", 10, true);

			// ������
			// ������������
			FormatColumn(ws, "G1", 48, 8, "Left");
			CellText(ws, "G1", "������������ ������", "Center", 10, true);
			// ����������
			FormatColumn(ws, "H1", 4, 8, "Right");
			CellText(ws, "H1", "�-��", "Center", 10, true);
			// ���������
			FormatColumn(ws, "I1", 8, 8, "Right");
			FormatColumnNumberFormat(ws, "I1", "# ##0,00");
			CellText(ws, "I1", "���������", "Center", 10, true);
		}

		protected void DataToExcel1(Excel.Worksheet ws, int start)
		{
			string row_last = "1";
			string row_txt;
			string cell_txt;
			int row = start;
			int row_summ = 2;
			string txt;

			foreach(object o in cards)
			{
				// �����-�����
				DtCard card = (DtCard)o;
				// ��������� ������ ������ �� �����-������
				DtCard card_data = DbSqlCard.Find(card.Number, card.Year);
				// ��������� ������ �� ����������
				DtAuto auto_data	= DbSqlAuto.Find((long)card_data.CodeAuto/*GetData("����������_��������")*/);
				
				// ��������� ������ ����� �� �����-������
				ArrayList works = new ArrayList();
				DbSqlCardWork.SelectInArray(card_data, works);

				// ������������ ������ �����
				float summ	= 0.0F;
				CalculatorCard calc = new CalculatorCard(CALCULATOR_TYPE.CALCULATOR_PAY, VAT_TYPE.VAT_NON, 0);
				foreach(DtCardWork o1 in works)
				{
					DtCardWork work = o1;
					CalculatorResult res = calc.WorkCalculator.Calculate(o1);
					summ += res.SummDatabase; //summ	+= work.WorkSummCash;
				}

				// ����������� ������ �� �����-������ � Excel
				row_txt = row.ToString();
				// ���� ������ = ���� ��������
				cell_txt = "A" + row_txt;
				txt = card_data.GetData("����_�����_������_��������").ToString();
				CellText(ws, cell_txt, txt);
				// ����� ������
				cell_txt = "B" + row_txt;
				txt = summ.ToString();
				CellText(ws, cell_txt, txt);
				// ��� ������� (1 ���� �����������)
				cell_txt = "C" + row_txt;
				txt = "";
				if ((bool)card_data.GetData("�����������_��������"))
					txt = "��";
				CellText(ws, cell_txt, txt);
				// ����� �����-������
				cell_txt = "D" + row_txt;
				txt = card_data.GetData("�����_�����_��������").ToString();
				CellText(ws, cell_txt, txt);
				// ������ ���������� 
				cell_txt = "E" + row_txt;
				txt = auto_data.GetData("������").ToString();
				CellText(ws, cell_txt, txt);
				// ������ ���������� 
				cell_txt = "F" + row_txt;
				txt = auto_data.GetData("�����_�����").ToString();
				CellText(ws, cell_txt, txt);

				row++;
				foreach(DtCardWork o1 in works)
				{
					DtCardWork work = o1;
					DtTxtCardWork txtCardWork = new DtTxtCardWork(o1);
					CalculatorResult res = calc.WorkCalculator.Calculate(work);
					row_txt = row.ToString();
					// ������������ ������
					cell_txt = "G" + row_txt;
					txt = txtCardWork.WorkName;//txt = work.GetData("������������_��������_������").ToString();
					CellText(ws, cell_txt, txt);
					// ����������
					cell_txt = "H" + row_txt;
					txt = txtCardWork.OperationAmount;// txt = work.GetData("����������_��������_������").ToString();
					CellText(ws, cell_txt, txt);
					// ���������
					cell_txt = "I" + row_txt;
					txt = res.SummDatabase.ToString();//txt = work.WorkSummCash.ToString();
					CellText(ws, cell_txt, txt);

					row++;
				}
			}
		}

		protected void DataToExcel2(Excel.Worksheet ws, int start)
		{
			string row_last = "1";
			string row_txt;
			string cell_txt;
			int row = start;
			int row_summ = 2;
			string txt;

			foreach(object o in cards_open)
			{
				// �����-�����
				DtCard card = (DtCard)o;
				// ��������� ������ ������ �� �����-������
				DtCard card_data = DbSqlCard.Find((long)card.GetData("�����_��������"), (int)card.GetData("���_��������"));
				// ��������� ������ �� ����������
				DtAuto auto_data	= DbSqlAuto.Find((long)card_data.CodeAuto/*GetData("����������_��������")*/);
				
				// ��������� ������ ����� �� �����-������
				ArrayList works = new ArrayList();
				DbSqlCardWork.SelectInArray(card_data, works);

				// ������������ ������ �����
				float summ	= 0.0F;
				CalculatorCard calc = new CalculatorCard(CALCULATOR_TYPE.CALCULATOR_PAY, VAT_TYPE.VAT_NON, 0);
				foreach (DtCardWork o1 in works)
				{
					DtCardWork work = o1;
					CalculatorResult res = calc.WorkCalculator.Calculate(o1);
					summ += res.SummDatabase;//summ	+= work.WorkSummCash;
				}

				// ����������� ������ �� �����-������ � Excel
				row_txt = row.ToString();
				// ���� ������ = ���� ��������
				cell_txt = "A" + row_txt;
				txt = card_data.GetData("����_�����_������_��������").ToString();
				CellText(ws, cell_txt, txt);
				// ����� ������
				cell_txt = "B" + row_txt;
				txt = summ.ToString();
				CellText(ws, cell_txt, txt);
				// ��� ������� (1 ���� �����������)
				cell_txt = "C" + row_txt;
				txt = "";
				if ((bool)card_data.GetData("�����������_��������"))
					txt = "��";
				CellText(ws, cell_txt, txt);
				// ����� �����-������
				cell_txt = "D" + row_txt;
				txt = card_data.GetData("�����_�����_��������").ToString();
				CellText(ws, cell_txt, txt);
				// ������ ���������� 
				cell_txt = "E" + row_txt;
				txt = auto_data.GetData("������").ToString();
				CellText(ws, cell_txt, txt);
				// ������ ���������� 
				cell_txt = "F" + row_txt;
				txt = auto_data.GetData("�����_�����").ToString();
				CellText(ws, cell_txt, txt);

				row++;
				foreach(DtCardWork o1 in works)
				{
					DtCardWork work = o1;
					DtTxtCardWork txtCardWork = new DtTxtCardWork(o1);
					CalculatorResult res = calc.WorkCalculator.Calculate(work);
					row_txt = row.ToString();
					// ������������ ������
					cell_txt = "G" + row_txt;
					txt = txtCardWork.WorkName;//txt = work.GetData("������������_��������_������").ToString();
					CellText(ws, cell_txt, txt);
					// ����������
					cell_txt = "H" + row_txt;
					txt = txtCardWork.OperationAmount;// txt = work.GetData("����������_��������_������").ToString();
					CellText(ws, cell_txt, txt);
					// ���������
					cell_txt = "I" + row_txt;
					txt = res.SummDatabase.ToString();//txt = work.WorkSummCash.ToString();
					CellText(ws, cell_txt, txt);

					row++;
				}
			}
		}

		override protected void TitleFormatMult(Excel.Worksheet ws, int sheet)
		{
				TitleFormat1(ws);
		}
		override protected void DataToExcelMult(Excel.Worksheet ws, int sheet, int start)
		{
			if(sheet == 1)
			{
				DataToExcel1(ws, start);
				return;
			}
			if(sheet == 2)
			{
				DataToExcel2(ws, start);
				return;
			}
		}
	}
}
