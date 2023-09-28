using System;
using System.Collections;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbExcelStuffProduction.
	/// </summary>
	public class DbExcelStuffProduction:DbExcel
	{
		public ArrayList	staffs;
		readonly int			year;
		int			month;
		DateTime	start_date;
		DateTime	end_date;
		int			variant;

		public DbExcelStuffProduction(long codeJob = 1)
		{
			staffs = new ArrayList();
			DbSqlStaff.SelectInArray(staffs, codeJob);
			//DbSqlStaff.SelectInArray(staffs, 1);
			//DbSqlStaff.SelectInArray(staffs);

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
				start_date = DateTime.Now;
				end_date = DateTime.Now;
				FormSelectDate dialog = new FormSelectDate();
				if(dialog.ShowDialog() == DialogResult.OK)
					start_date = dialog.SelectedDate;
				if(dialog.ShowDialog() == DialogResult.OK)
					end_date = dialog.SelectedDate;
				variant = 2;
			}
		}

		#region ������������ �������������� �������
		void TitleFormatSheet1(Excel.Worksheet ws)
		{
			// ��������������
			Excel.Range rng = ws.Rows;
			Excel.Range cell = rng.get_Range("C1", Missing.Value);
			cell.EntireColumn.NumberFormatLocal = "# ##0,00";

			// ������ � ���������
			FormatColumn(ws, "A1", 27, 8, "Left");
			CellText(ws, "A1", "������� ��� ��������", "Center", 10, true);
			
			// ��������� � ����������
			FormatColumn(ws, "B2", 10, 8, "Right");
		//	FormatColumnNumberFormat(ws, "B2", "0.00");
			CellText(ws, "B2", "�����", "Center", 10, true);
			
			// ������ �� �������� ���������
			MergeCells(ws, "C1", "F1");
		//	FormatColumnNumberFormat(ws, "C1", "0.00");
			CellText(ws, "C1", "���������� �/�", "Center", 10, true);
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
			CellText(ws, "G1", "����������� �/�", "Center", 10, true);
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
			CellText(ws, "K1", "���������� �/�", "Center", 10, true);
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
		}
		void TitleFormatSheet2(Excel.Worksheet ws, int pos)
		{
						// ������ � ���������
			DtStaff	staff	= (DtStaff)this.staffs[pos];
			string txt = staff.GetData("�������_��������") + " " + staff.GetData("���_��������") + " " + staff.GetData("��������_��������");
			CellText(ws, "A1", txt, "Left", 10, true);
			
			// ��������� ������ �� ���������
			// ����� �����/������ (��������) � ��� ��������, ���� ��������
			FormatColumn(ws, "A2", 22, 8, "Left");
			CellText(ws, "A2", "��������", "Center", 10, true);
			// ��� ����������� ������
			FormatColumn(ws, "B2", 10, 8, "Left");
			CellText(ws, "B2", "��� ������", "Center", 10, true);
			// ������������ ����������� ������
			FormatColumn(ws, "C2", 30, 8, "Left");
			CellText(ws, "C2", "������������ ������", "Center", 10, true);
			// ���������� ����������� �����
			FormatColumn(ws, "D2", 7, 8, "Center");
			CellText(ws, "D2", "���-��", "Center", 10, true);
			// ����� ������� �� ����������� ������
			FormatColumn(ws, "E2", 7, 8, "Center");
			CellText(ws, "E2", "��", "Center", 10, true);
			// ��������� ���������
			FormatColumn(ws, "F2", 7, 8, "Center");
			CellText(ws, "F2", "��", "Center", 10, true);
			// ���������� ������������
			FormatColumn(ws, "G2", 7, 8, "Center");
			CellText(ws, "G2", "���.", "Center", 10, true);
			// ��������� �� ������� ������ ������
			FormatColumn(ws, "H2", 7, 8, "Center");
			CellText(ws, "H2", "��", "Center", 10, true);
			// ��������
			FormatColumn(ws, "I2", 7, 8, "Center");
			CellText(ws, "I2", "���.", "Center", 10, true);
			// ��� ������
			FormatColumn(ws, "J2", 8, 8, "Center");
			CellText(ws, "J2", "���", "Center", 10, true);
			// ����������� ����� ������������ �����
			FormatColumn(ws, "K2", 10, 8, "Center");
			CellText(ws, "K2", "����� ��", "Center", 10, true);
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

			foreach(object o in staffs)
			{
				// ��������
				DtStaff staff = (DtStaff)o;
				// ��������� ������ � ���������
				// DrStuffProduction production = new DrStuffProduction((long)staff.GetData("���_��������"), year, month);
				DrStuffProduction production;
				if (variant == 1)
					production = new DrStuffProduction((long)staff.GetData("���_��������"), year, month);
				else
					production = new DrStuffProduction((long)staff.GetData("���_��������"), start_date, end_date);

				// ����������� ������ � Excel
				row_txt = row.ToString();
				// ������ � ���������
				cell_txt = "A" + row_txt;
				txt = staff.GetData("�������_��������") + " " + staff.GetData("���_��������") + " " + staff.GetData("��������_��������");
				CellText(ws, cell_txt, txt);

				// ������ �� ��������� ���������
				cell_txt = "B" + row_txt;
				txt = "=C" + row_txt + "+D" + row_txt + "+G" + row_txt + "+H" + row_txt + "+K" + row_txt + "+L" + row_txt;
				CellText(ws, cell_txt, txt);
				
				// ������ �� �������� ���������
				// ���������� ������������ ����������
				cell_txt = "C" + row_txt;
				txt = production.card_cash.hours.ToString();
				//if (txt.Length > 8) txt = txt.Substring(0, 8);
				txt = txt.Replace(",", ".");
				//txt = ToString(production.card_cash.hours, "0000.0000");
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

				// ������ �� ��������
				// ���������� ������������ ����������
				cell_txt = "G" + row_txt;
				txt = production.card_guaranty.hours.ToString();
				CellText(ws, cell_txt, txt);
				// ���������� ������������ ���������� �� �������������
				cell_txt = "H" + row_txt;
				txt = production.card_guaranty.hours_sp.ToString();
				CellText(ws, cell_txt, txt);
				// ����� �� ������������ �� ������� ������� ������ �������
				cell_txt = "I" + row_txt;
				txt = production.card_guaranty.cash_sp_nohours.ToString();
				CellText(ws, cell_txt, txt);
				// ���������� �� ������������ �� ������� ������� ������ �������
				cell_txt = "J" + row_txt;
				txt = production.card_guaranty.count_sp_nohours.ToString();
				CellText(ws, cell_txt, txt);

				// ������ �� ���������� �/�
				// ���������� ������������ ����������
				cell_txt = "K" + row_txt;
				txt = production.card_inner.hours.ToString();
				CellText(ws, cell_txt, txt);
				// ���������� ������������ ���������� �� �������������
				cell_txt = "L" + row_txt;
				txt = production.card_inner.hours_sp.ToString();
				CellText(ws, cell_txt, txt);
				// ����� �� ������������ �� ������� ������� ������ �������
				cell_txt = "M" + row_txt;
				txt = production.card_inner.cash_sp_nohours.ToString();
				CellText(ws, cell_txt, txt);
				// ���������� �� ������������ �� ������� ������� ������ �������
				cell_txt = "N" + row_txt;
				txt = production.card_inner.count_sp_nohours.ToString();
				CellText(ws, cell_txt, txt);

				// ������� � ��������� ������
				row_last = row.ToString();
				row++;
			}
		}
		protected void DataToExcelSheet2(Excel.Worksheet ws, int start, int pos)
		{
			string row_last = "1";
			string row_txt;
			string cell_txt;
			int row = start;
			int row_summ = 2;
			string txt;

			long card_number		= 0;
			int card_year			= 0;
			long card_number_old	= 0;
			int card_year_old		= 0;
			DtCard card				= null;
			DtAuto auto				= null;

			// ��������
			DtStaff staff = (DtStaff)staffs[pos];
			// ��������� ������ � ���������
			// DrStuffProduction production = new DrStuffProduction((long)staff.GetData("���_��������"), year, month);
			DrStuffProduction production;
			if (variant == 1)
				production = new DrStuffProduction((long)staff.GetData("���_��������"), year, month);
			else
				production = new DrStuffProduction((long)staff.GetData("���_��������"), start_date, end_date);

			foreach(DtCardWork work in production.works)
			{
				DtTxtCardWork txtCardWork = new DtTxtCardWork(work);
				// ��������� ������ � �����/������
				bool	is_sp		= false;
				float	local_sp	= 0.0F;
				card_number = work.CardNumber;// (long)work.GetData("�����_��������_��������_������");
				card_year = work.CardYear;// (int)work.GetData("���_��������_��������_������");
				if(card_year_old != card_year || card_number_old != card_number)
				{
					card = DbSqlCard.Find(card_number, card_year);
					card_number_old	= card_number;
					card_year_old	= card_year;

					// �������������� ���� �� ����������
					auto = DbSqlAuto.Find((long)card.CodeAuto/*GetData("����������_��������")*/);
				}
				// �������������� ������ � ������
				if (work.WorkNV == 0.0F)//if((float)work.GetData("������������_��������_������") == 0.0F)
				{
					is_sp	= true;
					local_sp	= DrStuffProduction.Production.GetSPHours(work);
				}
				else
				{
					is_sp	= false;
				}

				// ����������� ������ � Excel
				row_txt = row.ToString();
				// ������ � �����-������
				cell_txt = "A" + row_txt;
				if(card != null)
				{
					txt = card.GetData("�����_�����_��������").ToString() + "(" + card_number.ToString() + ")" + " / " + card_year.ToString() + " " + ((DateTime)card.GetData("����_�����_������_��������")).ToShortDateString();
					if (auto != null)
						txt += " " + auto.GetData("������").ToString();
				}
				else
					txt = "";
				CellText(ws, cell_txt, txt);
				// ��� ������
				cell_txt = "B" + row_txt;
				txt = txtCardWork.CatalogueNumber; //txt = (string)work.GetData("�����_�������_��������_������");
				CellText(ws, cell_txt, txt);
				// ������������ ������
				cell_txt = "C" + row_txt;
				txt = txtCardWork.WorkName; //txt = (string)work.GetData("������������_��������_������");
				CellText(ws, cell_txt, txt);
				// ����������
				cell_txt = "D" + row_txt;
				txt = txtCardWork.OperationAmount; // txt = work.GetData("����������_��������_������").ToString();
				CellText(ws, cell_txt, txt);
				// ������������ (��)
				cell_txt = "E" + row_txt;
				txt = txtCardWork.Amount; // txt = work.GetData("������������_��������_������").ToString();
				CellText(ws, cell_txt, txt);
				// ��������� ���������
				cell_txt = "F" + row_txt;
				txt = txtCardWork.Price; // txt = work.GetData("��������_��������_������").ToString();
				CellText(ws, cell_txt, txt);
				// ���������� ������������
				cell_txt = "G" + row_txt;
				txt = txtCardWork.ExecutorsCount;//txt = work.GetData("����������_������������").ToString();
				CellText(ws, cell_txt, txt);
				// ��������� �� ������� ������ ������
				cell_txt = "H" + row_txt;
				if(is_sp == true)
					txt = local_sp.ToString();
				else
					txt = "";
				CellText(ws, cell_txt, txt);
				// �������� ��� ���
				cell_txt = "I" + row_txt;
				if(work.GuaranteeFlag() == true)
					txt = "+";
				else
					txt = "";
				CellText(ws, cell_txt, txt);
				// ��� �����-������
				if((bool)card.GetData("����������_��������") == true)
					txt = "�����";
				else
					txt = "������";
				cell_txt = "J" + row_txt;
				CellText(ws, cell_txt, txt);
				// ����� ����������� ���������� ���� �������
				cell_txt = "K" + row_txt;
				txt = "=(E" + row_txt + "+H" + row_txt + ")*D" + row_txt + "/G" + row_txt;
				CellText(ws, cell_txt, txt);
				
				// ������� � ��������� ������
				row_last = row.ToString();
				row++;
			}	
		}
		#endregion

		override protected void TitleFormatMult(Excel.Worksheet ws, int sheet)
		{
			if(sheet == 1)
			{
				TitleFormatSheet1(ws);
				return;
			}
			if(sheet > 1 && sheet < this.staffs.Count + 2)
			{
				TitleFormatSheet2(ws, sheet - 2);
				return;
			}
		}
		override protected void DataToExcelMult(Excel.Worksheet ws, int sheet, int start)
		{
			if(sheet == 1)
			{
				DataToExcelSheet1(ws, start);
				return;
			}
			if(sheet > 1 && sheet < this.staffs.Count + 2)
			{
				DataToExcelSheet2(ws, start, sheet - 2);
				return;
			}
		}
	}
}
