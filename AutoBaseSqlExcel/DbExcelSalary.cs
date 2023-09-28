using System;
using System.Collections;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbExcelSalary.
	/// </summary>
	public class DbExcelSalary:DbExcel
	{
		ArrayList	staffs;
		ArrayList	washers;
		int			year;
		int			month;

		DateTime	start_date1;
		DateTime	end_date1;
		int			variant;

		public DbExcelSalary()
		{
			staffs = new ArrayList();
			DbSqlStaff.SelectInArray(staffs, 1);

			washers = new ArrayList();
			DbSqlStaff.SelectInArray(washers, 3);

			// ������ ������ ��������
			// ������ ���� ��������......
			if (MessageBox.Show("��������� ������ �����", "������", MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
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
				variant = 1;
			}
			else
			{
				// ������ ��������� ��������
				start_date1 = DateTime.Now;
				end_date1 = DateTime.Now;
				FormSelectDate dialog = new FormSelectDate();
				if(dialog.ShowDialog() == DialogResult.OK)
					start_date1 = dialog.SelectedDate;
				if(dialog.ShowDialog() == DialogResult.OK)
					end_date1 = dialog.SelectedDate;
				variant = 2;
			}
		}
		override protected void TitleFormat(Excel.Worksheet ws)
		{
			// ������ � ���������
			FormatColumn(ws, "A1", 27, 8, "Left");
			CellText(ws, "A1", "������� ��� ��������", "Center", 10, true);
			// ������ � �������� - ���� � �������� � ��������
			FormatColumn(ws, "B1", 12, 8, "Right", true);
			CellText(ws, "B1", "����", "Center", 10, true);
			// ������ � ��������
			FormatColumn(ws, "C1", 12, 8, "Right");
			CellText(ws, "C1", "������", "Center", 10, true);
			// ������ � �������� (c ������ ������������)
			FormatColumn(ws, "D1", 12, 8, "Right");
			CellText(ws, "D1", "����.", "Center", 10, true);

			// ��� ������� � �������
			// ����������� ������ �� ������
			FormatColumn(ws, "E1", 12, 8, "Right");
			CellText(ws, "E1", "�����", "Center", 10, true);
			// ������ ������
			FormatColumn(ws, "F1", 12, 8, "Right");
			CellText(ws, "F1", "�����", "Center", 10, true);

			// ����� �� ���������� ������
			FormatColumn(ws, "I1", 8, 8, "Right");
			CellText(ws, "I1", "�� $", "Center", 10, true);
			// ����� �� ������ �� ����������
			FormatColumn(ws, "J1", 8, 8, "Right");
			CellText(ws, "J1", "�� �/�", "Center", 10, true);
			// ����� �� ������������� ���������
			FormatColumn(ws, "K1", 8, 8, "Right");
			CellText(ws, "K1", "�� ����", "Center", 10, true);
			// ����� �� ������������� ����������
			FormatColumn(ws, "L1", 8, 8, "Right");
			CellText(ws, "L1", "�� ���", "Center", 10, true);
			// ����� �� ����������� ������
			FormatColumn(ws, "M1", 8, 8, "Right");
			CellText(ws, "M1", "�� ���.", "Center", 10, true);

			// �� ���������
			FormatColumn(ws, "O1", 8, 8, "Right");
			CellText(ws, "O1", "$", "Center", 10, true);
			// ���������� ��
			FormatColumn(ws, "P1", 8, 8, "Right");
			CellText(ws, "P1", "TO", "Center", 10, true);
			// �� ��
			FormatColumn(ws, "Q1", 8, 8, "Right");
			CellText(ws, "Q1", "TO$", "Center", 10, true);
			// �� ����������
			FormatColumn(ws, "R1", 8, 8, "Right");
			CellText(ws, "R1", "�� �/� $", "Center", 10, true);
			// ��������� - �� ������
			FormatColumn(ws, "S1", 8, 8, "Right");
			CellText(ws, "S1", "�/�", "Center", 10, true);
			// ��������� - �������������
			FormatColumn(ws, "T1", 8, 8, "Right");
			CellText(ws, "T1", "�/� 0", "Center", 10, true);
			// ��������� - �����������
			FormatColumn(ws, "U1", 8, 8, "Right");
			CellText(ws, "U1", "�/� ���", "Center", 10, true);
			// ���������� ������������
			FormatColumn(ws, "W1", 8, 8, "Right");
			CellText(ws, "W1", "���", "Center", 10, true);
		}
		override protected void DataToExcel(Excel.Worksheet ws, int start)
		{
			string row_last = "1";
			string row_txt;
			string cell_txt;
			int row = start;
			int row_summ = 2;
			string txt;

			foreach(object o in staffs)
			{
				// ��������
				DtStaff staff = (DtStaff)o;
				// ��������� ������ � ��������
				//DtSalary salary = new DtSalary((long)staff.GetData("���_��������"), year, month);
				DtSalary salary;
				if (variant == 1)
					salary = new DtSalary((long)staff.GetData("���_��������"), year, month);
				else
					salary = new DtSalary((long)staff.GetData("���_��������"), start_date1, end_date1);

				// ����������� ������ � Excel
				row_txt = row.ToString();
				// ������ � ���������
				cell_txt = "A" + row_txt;
				txt = staff.GetData("�������_��������") + " " +staff.GetData("���_��������") + " " + staff.GetData("��������_��������");
				CellText(ws, cell_txt, txt);
				// ���� �� ��������
				cell_txt = "B" + row_txt;
				txt = "=D" + row_txt + "-" + "E" + row_txt + "+" + "F" + row_txt;
				CellText(ws, cell_txt, txt);
				// ������ � ��������
				cell_txt = "C" + row_txt;
				txt = salary.salary.ToString();
				CellText(ws, cell_txt, txt);
				// ������ � �������� � ������ ���������� ������������
				cell_txt = "D" + row_txt;
				txt = (salary.salary*(float)staff.GetData("������_�����������")).ToString();
				CellText(ws, cell_txt, txt);
				// ����� �� ���������� �����
				cell_txt = "I" + row_txt;
				txt = salary.salary_cash.ToString();
				CellText(ws, cell_txt, txt);
				// ����� �� ��������� �����
				cell_txt = "J" + row_txt;
				txt = salary.salary_cash_hour.ToString();
				CellText(ws, cell_txt, txt);
				// ����� �� ������������ ����������
				cell_txt = "K" + row_txt;
				txt = salary.salary_hour.ToString();
				CellText(ws, cell_txt, txt);
				// ����� �� ���
				cell_txt = "L" + row_txt;
				txt = salary.salary_ppp.ToString();
				CellText(ws, cell_txt, txt);
				// ����� �� ��������
				cell_txt = "M" + row_txt;
				txt = salary.salary_guarantee.ToString();
				CellText(ws, cell_txt, txt);

				// ����������
				cell_txt = "O" + row_txt;
				txt = salary.cash.ToString();
				CellText(ws, cell_txt, txt);
				// ���������� ��
				cell_txt = "P" + row_txt;
				txt = salary.to_count.ToString();
				CellText(ws, cell_txt, txt);
				// �� ��
				cell_txt = "Q" + row_txt;
				txt = salary.cash_to.ToString();
				CellText(ws, cell_txt, txt);
				// �� ����������
				cell_txt = "R" + row_txt;
				txt = salary.cash_hour.ToString();
				CellText(ws, cell_txt, txt);
				// ���������
				cell_txt = "S" + row_txt;
				txt = salary.cash_hour_count.ToString();
				CellText(ws, cell_txt, txt);
				// ������������� ���������
				cell_txt = "T" + row_txt;
				txt = salary.hour.ToString();
				CellText(ws, cell_txt, txt);
				// ����������� ���������
				cell_txt = "U" + row_txt;
				txt = salary.guarantee_hour.ToString();
				CellText(ws, cell_txt, txt);
				// ������������
				cell_txt = "V" + row_txt;
				txt = (salary.ppp_count + salary.guarantee_ppp_count).ToString();
				CellText(ws, cell_txt, txt);

				// ������� � ��������� ������
				row_last = row.ToString();
				row++;
			}

			// ���������� ���������
			// ����� �������� �/�
			row_summ	= row;
			row_txt = row.ToString();
			cell_txt = "B" + row_txt;
			txt = "=����(B2:B" + row_last + ")";
			CellText(ws, cell_txt, txt);
			// ����� �/� ����������
			row_txt = (row + 1).ToString();
			cell_txt = "A" + row_txt;
			txt = "���������";
			CellText(ws, cell_txt, txt);
			cell_txt = "B" + row_txt;
			txt = "80000";
			CellText(ws, cell_txt, txt);
			// ����� ��������� �/�
			row_txt = row.ToString();
			cell_txt = "C" + row_txt;
			txt = "=����(C2:C" + row_last + ")";
			CellText(ws, cell_txt, txt);
			// ����� ��������� � ������������� �/�
			row_txt = row.ToString();
			cell_txt = "D" + row_txt;
			txt = "=����(D2:D" + row_last + ")";
			CellText(ws, cell_txt, txt);
			// ����� �������
			row_txt = row.ToString();
			cell_txt = "E" + row_txt;
			txt = "=����(E2:E" + row_last + ")";
			CellText(ws, cell_txt, txt);
			// ����� �������
			row_txt = row.ToString();
			cell_txt = "F" + row_txt;
			txt = "=����(F2:F" + row_last + ")";
			CellText(ws, cell_txt, txt);
			// ����� �� ���
			row_txt = row.ToString();
			cell_txt = "L" + row_txt;
			txt = "=����(L2:L" + row_last + ")";
			CellText(ws, cell_txt, txt);
			// ����� �� ��������
			row_txt = row.ToString();
			cell_txt = "M" + row_txt;
			txt = "=����(M2:M" + row_last + ")";
			CellText(ws, cell_txt, txt);
			// ����� �� �������� � ��� - ��������������!
			row_txt = (row + 1).ToString();
			cell_txt = "L" + row_txt;
			txt = "=L" + row_summ.ToString() + "+" + "M" + row_summ.ToString();
			CellText(ws, cell_txt, txt, "Center", 8, true);


			row = row + 4;
			// ������ �� ��������
			foreach(object o in washers)
			{
				// ��������
				DtStaff staff = (DtStaff)o;
				// ��������� ������ � ��������
				//DtSalary salary = new DtSalary((long)staff.GetData("���_��������"), year, month);
				DtSalary salary;
				if (variant == 1)
					salary = new DtSalary((long)staff.GetData("���_��������"), year, month);
				else
					salary = new DtSalary((long)staff.GetData("���_��������"), start_date1, end_date1);

				// ����������� ������ � Excel
				row_txt = row.ToString();
				// ������ � ���������
				cell_txt = "A" + row_txt;
				txt = staff.GetData("�������_��������") + " " +staff.GetData("���_��������") + " " + staff.GetData("��������_��������");
				CellText(ws, cell_txt, txt);
				// ���������� ����
				cell_txt = "B" + row_txt;
				txt = salary.wash_count.ToString();
				CellText(ws, cell_txt, txt);

				row++;
			}


			// ��������� ������ � ������� �������
			DateTime start_date;
			DateTime end_date;
			if(variant == 1)
			{
				start_date = new DateTime(year, month, 1);
				end_date = new DateTime(year, month, 1);
				end_date = end_date.AddMonths(1);
				end_date = end_date.AddDays(-1);
			}
			else
			{
				start_date	= start_date1;
				end_date	= end_date1;
			}
			//end_date = end_date.AddDays(-2);
			DbProduction product = new DbProduction(start_date, end_date, 1);
			row += 5;
			row_txt = row.ToString();
			// �������� ��������� �������
			cell_txt = "A" + row_txt;
			txt = "������� �������";
			CellText(ws, cell_txt, txt);
			cell_txt = "B" + row_txt;
			txt = product.Cash.ToString();
			CellText(ws, cell_txt, txt);
			cell_txt = "C" + row_txt;
			txt = product.Cash.ToString();
			CellText(ws, cell_txt, txt);
			// ������� �������� �������� �� ������� �������
			row++;
			row_txt = row.ToString();
			cell_txt = "A" + row_txt;
			txt = "������� ��������";
			CellText(ws, cell_txt, txt);
			cell_txt = "B" + row_txt;
			txt = "=B" + row_summ.ToString() + "/B" + (row-1).ToString() +"*100";
			CellText(ws, cell_txt, txt);
			// ������� �������� ����� ������� �� ������� �������
			row++;
			row_txt = row.ToString();
			cell_txt = "A" + row_txt;
			txt = "������� �������� �������";
			CellText(ws, cell_txt, txt);
			cell_txt = "B" + row_txt;
			txt = "=(B" + row_summ.ToString()+ "+" + "B" + (row_summ+1).ToString()+ ")/B" + (row-2).ToString() +"*100";
			CellText(ws, cell_txt, txt);
		}
	}
}
