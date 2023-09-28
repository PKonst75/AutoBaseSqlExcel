using System;
using System.Collections;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbExcelProduction.
	/// </summary>
	public class DbExcelProduction:DbExcel
	{
		public ArrayList	cards;		// ������ ��������, �������� � ��������� ������
		int					year;		// ��������� �� ��� �������
		int					month;		// ��������� �� ����� �������
		DrProduction		production;	// ������ �� ���������

		public DbExcelProduction()
		{
			cards = new ArrayList();

			// ������ ������ ��������
			DateTime date = DateTime.Now;
			FormSelectDate dialog = new FormSelectDate();
			if(dialog.ShowDialog() != DialogResult.OK)
			{
				year = date.Year;
				month = date.Month;
			}
			else
			{
				year = dialog.SelectedDate.Year;
				month = dialog.SelectedDate.Month;
			}
			DateTime date_start = new DateTime(year, month, 1, 0, 0, 0, 0);
			DateTime date_end	= date_start.AddMonths(1);
			date_end			= date_end.AddDays(-1);
			DbSqlCard.SelectCardClosedNumberWorkshop(cards, date_start, date_end, 1);
			production			= new DrProduction(cards);

		}

		#region ������������ �������������� �������
		void TitleFormatSheet1(Excel.Worksheet ws)
		{
			// ������ � ���������
			FormatColumn(ws, "A1", 27, 8, "Left");
			CellText(ws, "A1", "������� ��� ��������", "Center", 10, true);
			
			// ������ �� �������� ���������
			MergeCells(ws, "C1", "F1");
			CellText(ws, "C1", "�������� �/�", "Center", 10, true);
			// ��������� � ����������
			FormatColumn(ws, "C2", 10, 8, "Right");
			CellText(ws, "C2", "��", "Center", 10, true);
			// ��������� � ���������� �� ������ �������
			FormatColumn(ws, "D2", 10, 8, "Right");
			CellText(ws, "D2", "�� ��", "Center", 10, true);
			// ����� �� ������������ � ���������� ������ �������
			FormatColumn(ws, "E2", 10, 8, "Right");
			CellText(ws, "E2", "�� �����", "Center", 10, true);
			// ����� �� ������������ � ���������� ������ �������
			FormatColumn(ws, "F2", 10, 8, "Right");
			CellText(ws, "F2", "�� ���-��", "Center", 10, true);
		}
		void TitleFormatSheet2(Excel.Worksheet ws)
		{
			// ������ � ���������
			FormatColumn(ws, "A1", 27, 8, "Left");
			CellText(ws, "A1", "������ ���������", "Center", 10, true);
			
			// ��������� � ����������
			FormatColumn(ws, "B2", 10, 8, "Right");
			CellText(ws, "B2", "�����", "Center", 10, true);
			// ������ �� �������� ���������
			MergeCells(ws, "C1", "F1");
			CellText(ws, "C1", "���������� �/� ��������", "Center", 10, true);
			// ��������� � ����������
			FormatColumn(ws, "C2", 10, 8, "Right");
			CellText(ws, "C2", "��", "Center", 10, true);
			// ��������� � ���������� �� ������ �������
			FormatColumn(ws, "D2", 10, 8, "Right");
			CellText(ws, "D2", "�� ��", "Center", 10, true);
			// ����� �� ������������ � ���������� ������ �������
			FormatColumn(ws, "E2", 10, 8, "Right");
			CellText(ws, "E2", "�� �����", "Center", 10, true);
			// ����� �� ������������ � ���������� ������ �������
			FormatColumn(ws, "F2", 10, 8, "Right");
			CellText(ws, "F2", "�� ���-��", "Center", 10, true);

			// ������ �� ��������
			MergeCells(ws, "G1", "J1");
			CellText(ws, "G1", "���������� �/� ����������� ", "Center", 10, true);
			// ��������� � ����������
			FormatColumn(ws, "G2", 10, 8, "Right");
			CellText(ws, "G2", "��", "Center", 10, true);
			// ��������� � ���������� �� ������ �������
			FormatColumn(ws, "H2", 10, 8, "Right");
			CellText(ws, "H2", "�� ��", "Center", 10, true);
			// ����� �� ������������ � ���������� ������ �������
			FormatColumn(ws, "I2", 10, 8, "Right");
			CellText(ws, "I2", "�� �����", "Center", 10, true);
			// ����� �� ������������ � ���������� ������ �������
			FormatColumn(ws, "J2", 10, 8, "Right");
			CellText(ws, "J2", "�� ���-��", "Center", 10, true);

			// ������ �� ���������� �����-�������
			MergeCells(ws, "K1", "N1");
			CellText(ws, "K1", "����������� �/�", "Center", 10, true);
			// ��������� � ����������
			FormatColumn(ws, "K2", 10, 8, "Right");
			CellText(ws, "K2", "��", "Center", 10, true);
			// ��������� � ���������� �� ������ �������
			FormatColumn(ws, "L2", 10, 8, "Right");
			CellText(ws, "L2", "�� ��", "Center", 10, true);
			// ����� �� ������������ � ���������� ������ �������
			FormatColumn(ws, "M2", 10, 8, "Right");
			CellText(ws, "M2", "�� �����", "Center", 10, true);
			// ����� �� ������������ � ���������� ������ �������
			FormatColumn(ws, "N2", 10, 8, "Right");
			CellText(ws, "N2", "�� ���-��", "Center", 10, true);

			// ������ �� ���������� �����-�������
			MergeCells(ws, "O1", "R1");
			CellText(ws, "O1", "���������� �/�", "Center", 10, true);
			// ��������� � ����������
			FormatColumn(ws, "O2", 10, 8, "Right");
			CellText(ws, "O2", "��", "Center", 10, true);
			// ��������� � ���������� �� ������ �������
			FormatColumn(ws, "P2", 10, 8, "Right");
			CellText(ws, "P2", "�� ��", "Center", 10, true);
			// ����� �� ������������ � ���������� ������ �������
			FormatColumn(ws, "Q2", 10, 8, "Right");
			CellText(ws, "Q2", "�� �����", "Center", 10, true);
			// ����� �� ������������ � ���������� ������ �������
			FormatColumn(ws, "R2", 10, 8, "Right");
			CellText(ws, "R2", "�� ���-��", "Center", 10, true);
		}
		void TitleFormatSheet3(Excel.Worksheet ws)
		{
			// ������ � �����-������
			FormatColumn(ws, "A1", 16, 8, "Left");
			CellText(ws, "A1", "�����-�����", "Center", 10, true);
			FormatColumn(ws, "B1", 10, 8, "Left");
			CellText(ws, "B1", "��������", "Center", 10, true);
			FormatColumn(ws, "C1", 4, 8, "Center");
			CellVertical(ws, "C1");
			CellText(ws, "C1", "������-�����������", "Center", 10, true);
			FormatColumn(ws, "D1", 3, 8, "Center");
			CellVertical(ws, "D1");
			CellText(ws, "D1", "����������", "Center", 10, true);
			FormatColumn(ws, "E1", 3, 8, "Center");
			CellVertical(ws, "E1");
			CellText(ws, "E1", "�����������", "Center", 10, true);
			FormatColumn(ws, "F1", 3, 8, "Center");
			CellVertical(ws, "F1");
			CellText(ws, "F1", "��������", "Center", 10, true);
			FormatColumn(ws, "G1", 10, 8, "Left");
			CellText(ws, "G1", "���", "Center", 10, true);
			FormatColumn(ws, "H1", 25, 8, "Left");
			CellText(ws, "H1", "������������", "Center", 10, true);
			FormatColumn(ws, "I1", 3, 8, "Center");
			CellVertical(ws, "I1");
			CellText(ws, "I1", "����������", "Center", 10, true);
			FormatColumn(ws, "J1", 6, 8, "Center");
			CellText(ws, "J1", "��", "Center", 10, true);
			FormatColumn(ws, "K1", 8, 8, "Center");
			CellText(ws, "K1", "����", "Center", 10, true);
			FormatColumn(ws, "L1", 6, 8, "Center");
			CellText(ws, "L1", "��", "Center", 10, true);
			FormatColumn(ws, "M1", 8, 8, "Center");
			CellText(ws, "M1", "����� ��", "Center", 10, true);
		}
		#endregion

		#region ������������ �������� ������
		protected void DataToExcelSheet1(Excel.Worksheet ws, int start)
		{
			string row_last = "1";
			string row_txt;
			string cell_txt;
			int row = start;
			int row_summ = 2;
			string txt;

			foreach(object o in production.service_consultants)
			{
				// ��������
				DrProduction.ServiceConsultant consultant = (DrProduction.ServiceConsultant)o;
			
				// ����������� ������ � Excel
				row_txt = row.ToString();
				// ������ � ���������
				cell_txt = "A" + row_txt;
				txt = consultant.name;
				CellText(ws, cell_txt, txt);
				
				// ������ �� �������� ���������
				// ���������� ������������ ����������
				cell_txt = "C" + row_txt;
				txt = consultant.production.hours.ToString();
				CellText(ws, cell_txt, txt);
				// ���������� ������������ ���������� �� �������������
				cell_txt = "D" + row_txt;
				txt = consultant.production.hours_sp.ToString();
				CellText(ws, cell_txt, txt);
				// ����� �� ������������ �� ������� ������� ������ �������
				cell_txt = "E" + row_txt;
				txt = consultant.production.cash_sp_nohours.ToString();
				CellText(ws, cell_txt, txt);
				// ���������� �� ������������ �� ������� ������� ������ �������
				cell_txt = "F" + row_txt;
				txt = consultant.production.count_sp_nohours.ToString();
				CellText(ws, cell_txt, txt);

				// ������� � ��������� ������
				row_last = row.ToString();
				row++;
			}
		}
		protected void DataToExcelSheet2(Excel.Worksheet ws, int start)
		{
			string row_last = "1";
			string row_txt;
			string cell_txt;
			int row = start;
			int row_summ = 2;
			string txt;

			
			// ����������� ������ � Excel
			row_txt = row.ToString();
			// ������ � ���������
			cell_txt = "A" + row_txt;
			txt = "���������";
			CellText(ws, cell_txt, txt);

			// ������ �� ��������� ���������
			cell_txt = "B" + row_txt;
			txt = "=C" + row_txt + "+D" + row_txt + "+G" + row_txt + "+H" + row_txt + "+K" + row_txt + "+L" + row_txt + "+O" + row_txt + "+P" + row_txt;
			CellText(ws, cell_txt, txt);
				
			// ������ �� �������� ��������� - �������
			// ���������� ������������ ����������
			cell_txt = "C" + row_txt;
			txt = production.card_cash.hours.ToString();
			CellText(ws, cell_txt, txt);
			// ���������� ������������ ���������� �� �������������
			cell_txt = "D" + row_txt;
			txt = production.card_cash.hours_sp.ToString();
			CellText(ws, cell_txt, txt);
			// ����� �� ������������ �� ������� ������� ������ �������
			cell_txt = "E" + row_txt;
			txt = production.card_cash.cash_sp_nohours.ToString();
			CellText(ws, cell_txt, txt);
			// ���������� �� ������������ �� ������� ������� ������ �������
			cell_txt = "F" + row_txt;
			txt = production.card_cash.count_sp_nohours.ToString();
			CellText(ws, cell_txt, txt);

			// ������ �� �������� ��������� - ������
			// ���������� ������������ ����������
			cell_txt = "G" + row_txt;
			txt = production.card_cashless.hours.ToString();
			CellText(ws, cell_txt, txt);
			// ���������� ������������ ���������� �� �������������
			cell_txt = "H" + row_txt;
			txt = production.card_cashless.hours_sp.ToString();
			CellText(ws, cell_txt, txt);
			// ����� �� ������������ �� ������� ������� ������ �������
			cell_txt = "I" + row_txt;
			txt = production.card_cashless.cash_sp_nohours.ToString();
			CellText(ws, cell_txt, txt);
			// ���������� �� ������������ �� ������� ������� ������ �������
			cell_txt = "J" + row_txt;
			txt = production.card_cashless.count_sp_nohours.ToString();
			CellText(ws, cell_txt, txt);

			// ������ �� ��������
			// ���������� ������������ ����������
			cell_txt = "K" + row_txt;
			txt = production.card_guaranty.hours.ToString();
			CellText(ws, cell_txt, txt);
			// ���������� ������������ ���������� �� �������������
			cell_txt = "L" + row_txt;
			txt = production.card_guaranty.hours_sp.ToString();
			CellText(ws, cell_txt, txt);
			// ����� �� ������������ �� ������� ������� ������ �������
			cell_txt = "M" + row_txt;
			txt = production.card_guaranty.cash_sp_nohours.ToString();
			CellText(ws, cell_txt, txt);
			// ���������� �� ������������ �� ������� ������� ������ �������
			cell_txt = "N" + row_txt;
			txt = production.card_guaranty.count_sp_nohours.ToString();
			CellText(ws, cell_txt, txt);

			// ������ �� ���������� �/�
			// ���������� ������������ ����������
			cell_txt = "O" + row_txt;
			txt = production.card_inner.hours.ToString();
			CellText(ws, cell_txt, txt);
			// ���������� ������������ ���������� �� �������������
			cell_txt = "P" + row_txt;
			txt = production.card_inner.hours_sp.ToString();
			CellText(ws, cell_txt, txt);
			// ����� �� ������������ �� ������� ������� ������ �������
			cell_txt = "Q" + row_txt;
			txt = production.card_inner.cash_sp_nohours.ToString();
			CellText(ws, cell_txt, txt);
			// ���������� �� ������������ �� ������� ������� ������ �������
			cell_txt = "R" + row_txt;
			txt = production.card_inner.count_sp_nohours.ToString();
			CellText(ws, cell_txt, txt);

			// ������� � ��������� ������
			row_last = row.ToString();
			row++;
		}
		protected void DataToExcelSheet3(Excel.Worksheet ws, int start)
		{
			string row_last = "1";
			string row_txt;
			string cell_txt;
			int row = start;
			int row_summ = 2;
			string txt;

			foreach(DrProduction.CardWorkEx work_ex in production.works_ex)
			{
				// ��������� ������ � �����/������
				// ����������� ������ � Excel
				row_txt = row.ToString();

				// ������ � �����-������
				cell_txt = "A" + row_txt;
				CellText(ws, cell_txt, work_ex.txt_card);
				// ������ � �������� �����-������
				cell_txt = "B" + row_txt;
				CellText(ws, cell_txt, work_ex.txt_card_close_date);
				// ������ � ������-������������
				cell_txt = "C" + row_txt;
				CellText(ws, cell_txt, work_ex.txt_service_consultant);
				// ������ � ���������� �����-������
				cell_txt = "D" + row_txt;
				CellText(ws, cell_txt, work_ex.txt_inner);
				// ������ � ����������� �����-������
				cell_txt = "E" + row_txt;
				CellText(ws, cell_txt, work_ex.txt_cashless);
				// ������ � ����������� ������
				cell_txt = "F" + row_txt;
				CellText(ws, cell_txt, work_ex.txt_work_guaranty);
				// ����� ������� ������
				cell_txt = "G" + row_txt;
				CellText(ws, cell_txt, work_ex.txt_work_code);
				// ������������ ������
				cell_txt = "H" + row_txt;
				CellText(ws, cell_txt, work_ex.txt_work_name);
				// ���������� ����������� �����
				cell_txt = "I" + row_txt;
				CellText(ws, cell_txt, work_ex.txt_work_count);
				// �� ������
				cell_txt = "J" + row_txt;
				CellText(ws, cell_txt, work_ex.txt_work_nv);
				// ��������� ��������� ������
				cell_txt = "K" + row_txt;
				CellText(ws, cell_txt, work_ex.txt_work_price);
				// ������������� ������ ������
				cell_txt = "L" + row_txt;
				CellText(ws, cell_txt, work_ex.txt_work_sp);
				// �������� �� �� ������ ������
				cell_txt = "M" + row_txt;
				CellText(ws, cell_txt, work_ex.txt_work_nvsum);
				
				
				// ������� � ��������� ������
				row_last = row.ToString();
				row++;
			}	
		}
		#endregion

		override protected void DataToExcelMult(Excel.Worksheet ws, int sheet, int start)
		{
			if(sheet == 1)
			{
				DataToExcelSheet1(ws, start);
				return;
			}
			if(sheet == 2)
			{
				DataToExcelSheet2(ws, start);
				return;
			}
			if(sheet == 3)
			{
				DataToExcelSheet3(ws, start);
				return;
			}
		}
		override protected void TitleFormatMult(Excel.Worksheet ws, int sheet)
		{
			if(sheet == 1)
			{
				TitleFormatSheet1(ws);
				return;
			}
			if(sheet == 2)
			{
				TitleFormatSheet2(ws);
				return;
			}
			if(sheet == 3)
			{
				TitleFormatSheet3(ws);
				return;
			}
		}
	}
}
