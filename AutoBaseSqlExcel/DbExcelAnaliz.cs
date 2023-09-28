using System;
using System.Collections;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbExcelAnaliz.
	/// </summary>
	public class DbExcelAnaliz:DbExcel
	{
		public ArrayList	cards;		// ������ ��������, �������� � ��������� ������
		int					year;		// ��������� �� ��� �������
		int					month;		// ��������� �� ����� �������

		DrServiceProduction	production;	// ������ �� ���������

		public DbExcelAnaliz()
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
			production			= new DrServiceProduction(cards);
		}

		void TitleFormatSheet1(Excel.Worksheet ws)
		{
			// ������ � ���������
			FormatColumn(ws, "A1", 20, 8, "Left");
			CellText(ws, "A1", "���������", "Center", 10, true);
			
			// ����� ������� - � ������ ������
			FormatColumn(ws, "B1", 8, 8, "Right");
			CellText(ws, "B1", "����", "Center", 10, true);
			// ����������
			FormatColumn(ws, "C1", 0, 8, "Right");
			FormatColumn(ws, "D1", 0, 8, "Right");
			// ����� ����� �� �����-�������
			FormatColumn(ws, "E1", 8, 8, "Right");
			CellText(ws, "E1", "�����", "Center", 10, true);
			// ����� ������ �� �����-�������
			FormatColumn(ws, "F1", 8, 8, "Right");
			CellText(ws, "F1", "������", "Center", 10, true);
			// ����� ���������� �� ��
			FormatColumn(ws, "G1", 8, 8, "Right");
			CellText(ws, "G1", "��", "Center", 10, true);
			// ����� ���������� �� �������
			FormatColumn(ws, "H1", 8, 8, "Right");
			CellText(ws, "H1", "������", "Center", 10, true);
			// ����� �� ������ ������
			FormatColumn(ws, "I1", 8, 8, "Right");
			CellText(ws, "I1", "��", "Center", 10, true);
			// ����� �� �����
			FormatColumn(ws, "J1", 8, 8, "Right");
			CellText(ws, "J1", "�����", "Center", 10, true);
			// ����� �� ������������� ����������
			FormatColumn(ws, "K1", 8, 8, "Right");
			CellText(ws, "K1", "���", "Center", 10, true);


			FormatColumn(ws, "L1", 4, 8, "Right");
			FormatColumn(ws, "M1", 0, 8, "Right");
			FormatColumn(ws, "N1", 0, 8, "Right");
			FormatColumn(ws, "O1", 0, 8, "Right");
			FormatColumn(ws, "P1", 0, 8, "Right");
			FormatColumn(ws, "Q1", 0, 8, "Right");
			FormatColumn(ws, "R1", 0, 8, "Right");
			FormatColumn(ws, "S1", 0, 8, "Right");
			FormatColumn(ws, "T1", 0, 8, "Right");
			FormatColumn(ws, "U1", 0, 8, "Right");
			FormatColumn(ws, "V1", 0, 8, "Right");
			FormatColumn(ws, "W1", 0, 8, "Right");
			FormatColumn(ws, "X1", 0, 8, "Right");
			FormatColumn(ws, "Y1", 0, 8, "Right");
			FormatColumn(ws, "Z1", 0, 8, "Right");

			// ��������� ����������
			// ����� ����������� �� �����-�����
			FormatColumn(ws, "AA1", 8, 8, "Right");
			CellText(ws, "AA1", "�����", "Center", 10, true);
			// ����� ����������� �� ��
			FormatColumn(ws, "AB1", 8, 8, "Right");
			CellText(ws, "AB1", "��", "Center", 10, true);
			// ����� ����������� �� ������
			FormatColumn(ws, "AC1", 8, 8, "Right");
			CellText(ws, "AC1", "������", "Center", 10, true);
			// ����� ����������� �� ������ ������
			FormatColumn(ws, "AD1", 8, 8, "Right");
			CellText(ws, "AD1", "��", "Center", 10, true);
			// ����� ����������� �� ���
			FormatColumn(ws, "AE1", 8, 8, "Right");
			CellText(ws, "AE1", "���", "Center", 10, true);

			FormatColumn(ws, "AF1", 2, 8, "Right");
			FormatColumn(ws, "AG1", 0, 8, "Right");
			FormatColumn(ws, "AH1", 0, 8, "Right");
			FormatColumn(ws, "AI1", 0, 8, "Right");
			FormatColumn(ws, "AJ1", 0, 8, "Right");
			FormatColumn(ws, "AK1", 0, 8, "Right");
			FormatColumn(ws, "AL1", 0, 8, "Right");
			FormatColumn(ws, "AM1", 0, 8, "Right");
			FormatColumn(ws, "AN1", 0, 8, "Right");
			FormatColumn(ws, "AO1", 0, 8, "Right");
			FormatColumn(ws, "AP1", 0, 8, "Right");
			FormatColumn(ws, "AQ1", 0, 8, "Right");
			FormatColumn(ws, "AR1", 0, 8, "Right");
			FormatColumn(ws, "AS1", 0, 8, "Right");
			FormatColumn(ws, "AT1", 0, 8, "Right");
			FormatColumn(ws, "AU1", 0, 8, "Right");
			FormatColumn(ws, "AV1", 0, 8, "Right");
			FormatColumn(ws, "AW1", 0, 8, "Right");
			FormatColumn(ws, "AX1", 0, 8, "Right");
			FormatColumn(ws, "AY1", 0, 8, "Right");
			FormatColumn(ws, "AZ1", 0, 8, "Right");

			// �������������� ����������
			// ����� ���������� ��
			FormatColumn(ws, "BC1", 6, 8, "Right");
			CellText(ws, "BC1", "��", "Center", 10, true);
		}

		protected void DataToExcelSheet1(Excel.Worksheet ws, int start)
		{
			string row_last = "1";
			string row_txt;
			string cell_txt;
			int row = start;
			int row_summ = 2;
			string txt;
	
			// �������� ����������
			// �������� �����-������
			cell_txt = "A2";
			txt = "���������� �/�";
			CellText(ws, cell_txt, txt);
			// ����� � ������ ������
			cell_txt = "B2";
			txt = "=E2-F2";
			CellText(ws, cell_txt, txt);
			// ����� �����
			cell_txt = "E2";
			txt = production.service.cash.cash_all.ToString();
			CellText(ws, cell_txt, txt);
			// ������
			cell_txt = "F2";
			txt = production.service.cash.cash_discount.ToString();
			CellText(ws, cell_txt, txt);
			// ��
			cell_txt = "G2";
			txt = production.service.cash.cash_to.ToString();
			CellText(ws, cell_txt, txt);
			// ������
			cell_txt = "H2";
			txt = production.service.cash.cash.ToString();
			CellText(ws, cell_txt, txt);
			// ������ ������
			cell_txt = "I2";
			txt = production.service.cash.cash_sp.ToString();
			CellText(ws, cell_txt, txt);
			// �����
			cell_txt = "J2";
			txt = production.service.cash.cash_wash.ToString();
			CellText(ws, cell_txt, txt);
			// ���
			cell_txt = "K2";
			txt = production.service.cash.cash_ppp.ToString();
			CellText(ws, cell_txt, txt);
			
			// ��������� ����������
			// ���ee �����
			cell_txt = "AA2";
			txt = production.service.cash.hour_all.ToString();
			CellText(ws, cell_txt, txt);
			// ����� �� ��
			cell_txt = "AB2";
			txt = production.service.cash.hour_to.ToString();
			CellText(ws, cell_txt, txt);
			// ����� �� ������
			cell_txt = "AC2";
			txt = production.service.cash.hour.ToString();
			CellText(ws, cell_txt, txt);
			// ����� �� �����������
			cell_txt = "AD2";
			txt = production.service.cash.hour_sp.ToString();
			CellText(ws, cell_txt, txt);
			// ����� �� ���
			cell_txt = "AE2";
			txt = production.service.cash.hour_ppp.ToString();
			CellText(ws, cell_txt, txt);

			// �������������� ����������
			// ���ee �����
			cell_txt = "BB2";
			txt = production.service.count_work_cash.ToString();
			CellText(ws, cell_txt, txt);
			// ���ee �����
			cell_txt = "BC2";
			txt = production.service.cash.count_to.ToString();
			CellText(ws, cell_txt, txt);


			// ����������� �����-������
			cell_txt = "A3";
			txt = "����������� �/�";
			CellText(ws, cell_txt, txt);
			// ����� � ������ ������
			cell_txt = "B3";
			txt = "=E3-F3";
			CellText(ws, cell_txt, txt);
			// ����� �����
			cell_txt = "E3";
			txt = production.service.cash_bn.cash_all.ToString();
			CellText(ws, cell_txt, txt);
			// ������
			cell_txt = "F3";
			txt = production.service.cash_bn.cash_discount.ToString();
			CellText(ws, cell_txt, txt);
			// ��
			cell_txt = "G3";
			txt = production.service.cash_bn.cash_to.ToString();
			CellText(ws, cell_txt, txt);
			// ������
			cell_txt = "H3";
			txt = production.service.cash_bn.cash.ToString();
			CellText(ws, cell_txt, txt);
			// ������ ������
			cell_txt = "I3";
			txt = production.service.cash_bn.cash_sp.ToString();
			CellText(ws, cell_txt, txt);
			// �����
			cell_txt = "J3";
			txt = production.service.cash_bn.cash_wash.ToString();
			CellText(ws, cell_txt, txt);
			// ���
			cell_txt = "K3";
			txt = production.service.cash_bn.cash_ppp.ToString();
			CellText(ws, cell_txt, txt);

			// ���ee �����
			cell_txt = "AA3";
			txt = production.service.cash_bn.hour_all.ToString();
			CellText(ws, cell_txt, txt);
			// ����� �� ��
			cell_txt = "AB3";
			txt = production.service.cash_bn.hour_to.ToString();
			CellText(ws, cell_txt, txt);
			// ����� �� ������
			cell_txt = "AC3";
			txt = production.service.cash_bn.hour.ToString();
			CellText(ws, cell_txt, txt);
			// ����� �� �����������
			cell_txt = "AD3";
			txt = production.service.cash_bn.hour_sp.ToString();
			CellText(ws, cell_txt, txt);
			// ����� �� ���
			cell_txt = "AE3";
			txt = production.service.cash_bn.hour_ppp.ToString();
			CellText(ws, cell_txt, txt);



			// ���������� �����-������
			cell_txt = "A5";
			txt = "���������� �/�";
			CellText(ws, cell_txt, txt);
			// ����� ���
			// ����� �����
			cell_txt = "E5";
			txt = production.service.inner.cash_all.ToString();
			CellText(ws, cell_txt, txt);
			// ������
			cell_txt = "F5";
			txt = production.service.inner.cash_discount.ToString();
			CellText(ws, cell_txt, txt);
			// ��
			cell_txt = "G5";
			txt = production.service.inner.cash_to.ToString();
			CellText(ws, cell_txt, txt);
			// ������
			cell_txt = "H5";
			txt = production.service.inner.cash.ToString();
			CellText(ws, cell_txt, txt);
			// ������ ������
			cell_txt = "I5";
			txt = production.service.inner.cash_sp.ToString();
			CellText(ws, cell_txt, txt);
			// �����
			cell_txt = "J5";
			txt = production.service.inner.cash_wash.ToString();
			CellText(ws, cell_txt, txt);
			// ���
			cell_txt = "K5";
			txt = production.service.inner.cash_ppp.ToString();
			CellText(ws, cell_txt, txt);

			// ���ee �����
			cell_txt = "AA5";
			txt = production.service.inner.hour_all.ToString();
			CellText(ws, cell_txt, txt);
			// ����� �� ��
			cell_txt = "AB5";
			txt = production.service.inner.hour_to.ToString();
			CellText(ws, cell_txt, txt);
			// ����� �� ������
			cell_txt = "AC5";
			txt = production.service.inner.hour.ToString();
			CellText(ws, cell_txt, txt);
			// ����� �� �����������
			cell_txt = "AD5";
			txt = production.service.inner.hour_sp.ToString();
			CellText(ws, cell_txt, txt);
			// ����� �� ���
			cell_txt = "AE5";
			txt = production.service.inner.hour_ppp.ToString();
			CellText(ws, cell_txt, txt);

			// ������������� ����������
			cell_txt = "A6";
			txt = "������������ �/�";
			CellText(ws, cell_txt, txt);
			// ����� ���
			// ����� �����
			cell_txt = "E6";
			txt = production.service.ppp.cash_all.ToString();
			CellText(ws, cell_txt, txt);
			// ������
			cell_txt = "F6";
			txt = production.service.ppp.cash_discount.ToString();
			CellText(ws, cell_txt, txt);
			// ��
			cell_txt = "G6";
			txt = production.service.ppp.cash_to.ToString();
			CellText(ws, cell_txt, txt);
			// ������
			cell_txt = "H6";
			txt = production.service.ppp.cash.ToString();
			CellText(ws, cell_txt, txt);
			// ������ ������
			cell_txt = "I6";
			txt = production.service.ppp.cash_sp.ToString();
			CellText(ws, cell_txt, txt);
			// �����
			cell_txt = "J6";
			txt = production.service.ppp.cash_wash.ToString();
			CellText(ws, cell_txt, txt);
			// ���
			cell_txt = "K6";
			txt = production.service.ppp.cash_ppp.ToString();
			CellText(ws, cell_txt, txt);

			// ���ee �����
			cell_txt = "AA6";
			txt = production.service.ppp.hour_all.ToString();
			CellText(ws, cell_txt, txt);
			// ����� �� ��
			cell_txt = "AB6";
			txt = production.service.ppp.hour_to.ToString();
			CellText(ws, cell_txt, txt);
			// ����� �� ������
			cell_txt = "AC6";
			txt = production.service.ppp.hour.ToString();
			CellText(ws, cell_txt, txt);
			// ����� �� �����������
			cell_txt = "AD6";
			txt = production.service.ppp.hour_sp.ToString();
			CellText(ws, cell_txt, txt);
			// ����� �� ���
			cell_txt = "AE6";
			txt = production.service.ppp.hour_ppp.ToString();
			CellText(ws, cell_txt, txt);

			// ������ ��������
			// �������� �����-������
			cell_txt = "A8";
			txt = "���������� - ��������";
			CellText(ws, cell_txt, txt);
			// ��� �����
			// ����� �����
			cell_txt = "E8";
			txt = production.service.guaranty_cash.cash_all.ToString();
			CellText(ws, cell_txt, txt);
			// ������
			cell_txt = "F8";
			txt = production.service.guaranty_cash.cash_discount.ToString();
			CellText(ws, cell_txt, txt);
			// ��
			cell_txt = "G8";
			txt = production.service.guaranty_cash.cash_to.ToString();
			CellText(ws, cell_txt, txt);
			// ������
			cell_txt = "H8";
			txt = production.service.guaranty_cash.cash.ToString();
			CellText(ws, cell_txt, txt);
			// ������ ������
			cell_txt = "I8";
			txt = production.service.guaranty_cash.cash_sp.ToString();
			CellText(ws, cell_txt, txt);
			// �����
			cell_txt = "J8";
			txt = production.service.guaranty_cash.cash_wash.ToString();
			CellText(ws, cell_txt, txt);
			// ���
			cell_txt = "K8";
			txt = production.service.guaranty_cash.cash_ppp.ToString();
			CellText(ws, cell_txt, txt);

			// ���ee �����
			cell_txt = "AA8";
			txt = production.service.guaranty_cash.hour_all.ToString();
			CellText(ws, cell_txt, txt);
			// ����� �� ��
			cell_txt = "AB8";
			txt = production.service.guaranty_cash.hour_to.ToString();
			CellText(ws, cell_txt, txt);
			// ����� �� ������
			cell_txt = "AC8";
			txt = production.service.guaranty_cash.hour.ToString();
			CellText(ws, cell_txt, txt);
			// ����� �� �����������
			cell_txt = "AD8";
			txt = production.service.guaranty_cash.hour_sp.ToString();
			CellText(ws, cell_txt, txt);
			// ����� �� ���
			cell_txt = "AE8";
			txt = production.service.guaranty_cash.hour_ppp.ToString();
			CellText(ws, cell_txt, txt);

			// ����������� �����-������
			cell_txt = "A9";
			txt = "����������� - ��������";
			CellText(ws, cell_txt, txt);
			// ��� �����
			// ����� �����
			cell_txt = "E9";
			txt = production.service.guaranty_cash_bn.cash_all.ToString();
			CellText(ws, cell_txt, txt);
			// ������
			cell_txt = "F9";
			txt = production.service.guaranty_cash_bn.cash_discount.ToString();
			CellText(ws, cell_txt, txt);
			// ��
			cell_txt = "G9";
			txt = production.service.guaranty_cash_bn.cash_to.ToString();
			CellText(ws, cell_txt, txt);
			// ������
			cell_txt = "H9";
			txt = production.service.guaranty_cash_bn.cash.ToString();
			CellText(ws, cell_txt, txt);
			// ������ ������
			cell_txt = "I9";
			txt = production.service.guaranty_cash_bn.cash_sp.ToString();
			CellText(ws, cell_txt, txt);
			// �����
			cell_txt = "J9";
			txt = production.service.guaranty_cash_bn.cash_wash.ToString();
			CellText(ws, cell_txt, txt);
			// ���
			cell_txt = "K9";
			txt = production.service.guaranty_cash_bn.cash_ppp.ToString();
			CellText(ws, cell_txt, txt);

			// ���ee �����
			cell_txt = "AA9";
			txt = production.service.guaranty_cash_bn.hour_all.ToString();
			CellText(ws, cell_txt, txt);
			// ����� �� ��
			cell_txt = "AB9";
			txt = production.service.guaranty_cash_bn.hour_to.ToString();
			CellText(ws, cell_txt, txt);
			// ����� �� ������
			cell_txt = "AC9";
			txt = production.service.guaranty_cash_bn.hour.ToString();
			CellText(ws, cell_txt, txt);
			// ����� �� �����������
			cell_txt = "AD9";
			txt = production.service.guaranty_cash_bn.hour_sp.ToString();
			CellText(ws, cell_txt, txt);
			// ����� �� ���
			cell_txt = "AE9";
			txt = production.service.guaranty_cash_bn.hour_ppp.ToString();
			CellText(ws, cell_txt, txt);

			// ���������� �����-������
			cell_txt = "A10";
			txt = "���������� - ��������";
			CellText(ws, cell_txt, txt);
			// ��� �����
			// ����� �����
			cell_txt = "E10";
			txt = production.service.guaranty_inner.cash_all.ToString();
			CellText(ws, cell_txt, txt);
			// ������
			cell_txt = "F10";
			txt = production.service.guaranty_inner.cash_discount.ToString();
			CellText(ws, cell_txt, txt);
			// ��
			cell_txt = "G10";
			txt = production.service.guaranty_inner.cash_to.ToString();
			CellText(ws, cell_txt, txt);
			// ������
			cell_txt = "H10";
			txt = production.service.guaranty_inner.cash.ToString();
			CellText(ws, cell_txt, txt);
			// ������ ������
			cell_txt = "I10";
			txt = production.service.guaranty_inner.cash_sp.ToString();
			CellText(ws, cell_txt, txt);
			// �����
			cell_txt = "J10";
			txt = production.service.guaranty_inner.cash_wash.ToString();
			CellText(ws, cell_txt, txt);
			// ���
			cell_txt = "K10";
			txt = production.service.guaranty_inner.cash_ppp.ToString();
			CellText(ws, cell_txt, txt);

			// ���ee �����
			cell_txt = "AA10";
			txt = production.service.guaranty_inner.hour_all.ToString();
			CellText(ws, cell_txt, txt);
			// ����� �� ��
			cell_txt = "AB10";
			txt = production.service.guaranty_inner.hour_to.ToString();
			CellText(ws, cell_txt, txt);
			// ����� �� ������
			cell_txt = "AC10";
			txt = production.service.guaranty_inner.hour.ToString();
			CellText(ws, cell_txt, txt);
			// ����� �� �����������
			cell_txt = "AD10";
			txt = production.service.guaranty_inner.hour_sp.ToString();
			CellText(ws, cell_txt, txt);
			// ����� �� ���
			cell_txt = "AE10";
			txt = production.service.guaranty_inner.hour_ppp.ToString();
			CellText(ws, cell_txt, txt);

			// ������������ �����-������
			cell_txt = "A11";
			txt = "������������ - ��������";
			CellText(ws, cell_txt, txt);
			// ��� �����
			// ����� �����
			cell_txt = "E11";
			txt = production.service.guaranty_ppp.cash_all.ToString();
			CellText(ws, cell_txt, txt);
			// ������
			cell_txt = "F11";
			txt = production.service.guaranty_ppp.cash_discount.ToString();
			CellText(ws, cell_txt, txt);
			// ��
			cell_txt = "G11";
			txt = production.service.guaranty_ppp.cash_to.ToString();
			CellText(ws, cell_txt, txt);
			// ������
			cell_txt = "H11";
			txt = production.service.guaranty_ppp.cash.ToString();
			CellText(ws, cell_txt, txt);
			// ������ ������
			cell_txt = "I11";
			txt = production.service.guaranty_ppp.cash_sp.ToString();
			CellText(ws, cell_txt, txt);
			// �����
			cell_txt = "J11";
			txt = production.service.guaranty_ppp.cash_wash.ToString();
			CellText(ws, cell_txt, txt);
			// ���
			cell_txt = "K11";
			txt = production.service.guaranty_ppp.cash_ppp.ToString();
			CellText(ws, cell_txt, txt);

			// ���ee �����
			cell_txt = "AA11";
			txt = production.service.guaranty_ppp.hour_all.ToString();
			CellText(ws, cell_txt, txt);
			// ����� �� ��
			cell_txt = "AB11";
			txt = production.service.guaranty_ppp.hour_to.ToString();
			CellText(ws, cell_txt, txt);
			// ����� �� ������
			cell_txt = "AC11";
			txt = production.service.guaranty_ppp.hour.ToString();
			CellText(ws, cell_txt, txt);
			// ����� �� �����������
			cell_txt = "AD11";
			txt = production.service.guaranty_ppp.hour_sp.ToString();
			CellText(ws, cell_txt, txt);
			// ����� �� ���
			cell_txt = "AE11";
			txt = production.service.guaranty_ppp.hour_ppp.ToString();
			CellText(ws, cell_txt, txt);
			
		}

		override protected void DataToExcelMult(Excel.Worksheet ws, int sheet, int start)
		{
			if(sheet == 1)
			{
				DataToExcelSheet1(ws, start);
				return;
			}
		/*	if(sheet == 2)
			{
				DataToExcelSheet2(ws, start);
				return;
			}
			if(sheet == 3)
			{
				DataToExcelSheet3(ws, start);
				return;
			}*/
		}
		override protected void TitleFormatMult(Excel.Worksheet ws, int sheet)
		{
			if(sheet == 1)
			{
				TitleFormatSheet1(ws);
				return;
			}
		/*	if(sheet == 2)
			{
				TitleFormatSheet2(ws);
				return;
			}
			if(sheet == 3)
			{
				TitleFormatSheet3(ws);
				return;
			}*/
		}
	}
}
