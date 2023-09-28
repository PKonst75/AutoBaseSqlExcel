using System;
using System.Collections;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbExcelReportSell.
	/// </summary>
	public class DbExcelReportSell:DbExcel
	{
		ArrayList array = new ArrayList();		// ������ ������
		struct INFO
		{
			public DtAutoSell sell;
			public DtAuto	auto;
			public CS_SellInfo info;
			public DtAutoSellServ info_serv;
		} 

		public DbExcelReportSell()
		{
			// ������ ���� ������ ������� � ����� �������
			FormSelectDate dlg = new FormSelectDate();
			dlg.ShowDialog();
			if(dlg.DialogResult != DialogResult.OK) return;
			DateTime date_start = dlg.SelectedDate;

			dlg.ShowDialog();
			if(dlg.DialogResult != DialogResult.OK) return;
			DateTime date_end = dlg.SelectedDate;

			// �������� ��� ������� �� �������� ������ �������
			ArrayList sells = new ArrayList();
			DbSqlAutoSell.SearchMask mask = new DbSqlAutoSell.SearchMask();
			mask.timeon = true;

			DateTime date_start_0 = new DateTime(date_start.Year, date_start.Month, date_start.Day, 0, 0, 0, 0);
			DateTime date_end_0 = new DateTime(date_end.Year, date_end.Month, date_end.Day, 23, 59, 59, 0);

			mask.date_start = date_start_0;
			mask.date_stop = date_end_0;
			DbSqlAutoSell.SelectInArray(sells, mask);

			FormInfoTable info = new FormInfoTable("������ �������");
			info.Show();
			foreach(DtAutoSell element in sells)
			{		
				array.Add(element);
				info.SetText(element.GetData("����_����������_�������").ToString());
			}
			info.SetText("����� �������");
			info.Close();
		}

		protected void TitleFormatSheet1(Excel.Worksheet ws)
		{
			// ���� �������
			FormatColumn(ws, "A1", 12, 8, "Left");
			CellText(ws, "A1", "���� �������", "Center", 10, true);
			// VIN ����������
			FormatColumn(ws, "B1", 18, 8, "Right", true);
			CellText(ws, "B1", "VIN", "Center", 10, true);
			// ������
			FormatColumn(ws, "C1", 25, 8, "Right");
			CellText(ws, "C1", "������", "Center", 10, true);
			// ���� ��������� ����
			FormatColumn(ws, "D1", 12, 8, "Right");
			CellText(ws, "D1", "���� ���������", "Center", 10, true);
			// ������
			FormatColumn(ws, "E1", 8, 8, "Right");
			CellText(ws, "E1", "������", "Center", 10, true);
			// ��������
			FormatColumn(ws, "F1", 16, 8, "Right");
			CellText(ws, "F1", "��������", "Center", 10, true);
			// ����� ����
			FormatColumn(ws, "G1", 8, 8, "Right");
			CellText(ws, "G1", "���� ���", "Center", 10, true);
			// ������ ����
			FormatColumn(ws, "H1", 8, 8, "Right");
			CellText(ws, "H1", "���� ����", "Center", 10, true);

			FormatColumn(ws, "I1", 1, 8, "Right");
			FormatColumn(ws, "J1", 1, 8, "Right");
			FormatColumn(ws, "K1", 1, 8, "Right");

			// ������
			FormatColumn(ws, "L1", 4, 8, "Right");
			CellText(ws, "L1", "�", "Center", 10, true);
			// ������������
			FormatColumn(ws, "M1", 4, 8, "Right");
			CellText(ws, "M1", "�", "Center", 10, true);
			// ������
			FormatColumn(ws, "N1", 4, 8, "Right");
			CellText(ws, "N1", "�", "Center", 10, true);
			// ����
			FormatColumn(ws, "O1", 4, 8, "Right");
			CellText(ws, "O1", "�", "Center", 10, true);
			// ����� �����
			FormatColumn(ws, "P1", 8, 8, "Right");
			CellText(ws, "P1", "���� ���", "Center", 10, true);
			// ����� ������ + �������
			FormatColumn(ws, "Q1", 8, 8, "Right");
			CellText(ws, "Q1", "����+���", "Center", 10, true);

			FormatColumn(ws, "R1", 1, 8, "Right");

			// �������
			FormatColumn(ws, "S1", 4, 8, "Right");
			CellText(ws, "S1", "�", "Center", 10, true);
			// ������
			FormatColumn(ws, "T1", 4, 8, "Right");
			CellText(ws, "T1", "�", "Center", 10, true);
			// ���������
			FormatColumn(ws, "U1", 4, 8, "Right");
			CellText(ws, "U1", "�", "Center", 10, true);
			// ����� �������
			FormatColumn(ws, "V1", 8, 8, "Right");
			CellText(ws, "V1", "��� ����", "Center", 10, true);
			// ����� ������ �������
			FormatColumn(ws, "W1", 8, 8, "Right");
			CellText(ws, "W1", "���� ���", "Center", 10, true);

			FormatColumn(ws, "X1", 1, 8, "Right");

			// ����� �����������
			FormatColumn(ws, "Y1", 4, 8, "Right");
			CellText(ws, "Y1", "�����", "Center", 10, true);
			// ��� �����
			FormatColumn(ws, "Z1", 8, 8, "Right");
			CellText(ws, "Z1", "��� ���", "Center", 10, true);

			FormatColumn(ws, "AA1", 1, 8, "Right");

			// �����
			FormatColumn(ws, "AB1", 4, 8, "Right");
			CellText(ws, "AB1", "�����", "Center", 10, true);
			// �����
			FormatColumn(ws, "AC1", 4, 8, "Right");
			CellText(ws, "AC1", "�����", "Center", 10, true);
		}

		protected void DataToExcelSheet1(Excel.Worksheet ws, int start)
		{
			string row_last = "1";
			string row_txt;
			string cell_txt;
			int row = start;
			int row_summ = 2;
			string txt;

			DtAuto auto = null;
			DtAutoReceive receive = null;
			CS_SellInfo info = null;
			DtAutoSellServ serv = null;

			foreach(object o in array)
			{
				// ��������
				DtAutoSell sell = (DtAutoSell)o;

				// ��������� �������������� ������
				auto = DbSqlAuto.Find((long)sell.GetData("������_���_����������"));
				receive = DbSqlAutoReceive.FindAuto((long)sell.GetData("������_���_����������"));
				info = DbSqlSellInfo.Find((long)sell.GetData("���_����������_�������"));
				serv = DbSqlAutoSellServ.Find((long)sell.GetData("���_����������_�������"));
				
				// ����������� ������ � Excel
				row_txt = row.ToString();
				// ���� �������
				cell_txt = "A" + row_txt;
				txt = ((DateTime)sell.GetData("����_����������_�������")).ToShortDateString();
				CellText(ws, cell_txt, txt);
				// VIN
				txt = "";
				if (auto != null)
					txt = (string)auto.GetData("VIN");
				cell_txt = "B" + row_txt;
				CellText(ws, cell_txt, txt);
				// ������
				cell_txt = "C" + row_txt;
				txt = "";
				if (auto != null)
					txt = (string)auto.GetData("������");
				CellText(ws, cell_txt, txt);
				// ���� ��������� ����
				cell_txt = "D" + row_txt;
				txt = "";
				if (receive != null)
					txt = ((DateTime)receive.GetData("����_��������")).ToShortDateString();
				CellText(ws, cell_txt, txt);
				// ������
				cell_txt = "E" + row_txt;
				txt = "";
				if (info != null)
					if(info.flag_credit_inner == true || info.flag_credit_outer == true)
						txt = "��";
				CellText(ws, cell_txt, txt);

				// ����������� ����������
				if (serv != null)
				{
					// ��������
					cell_txt = "F" + row_txt;
					DtStaff staff = DbSqlStaff.Find(serv.code_manager);
					if (staff != null)
						txt = staff.Title;
					else
						txt = "";
					CellText(ws, cell_txt, txt);
					// ����� ����
					cell_txt = "G" + row_txt;
					txt = (serv.auto_summ).ToString();
					CellText(ws, cell_txt, txt);
					// ������ ����
					cell_txt = "H" + row_txt;
					txt = (serv.auto_discount_money).ToString();
					CellText(ws, cell_txt, txt);

					// ������
					cell_txt = "L" + row_txt;
					if (serv.flag_music == true)
						txt = "1";
					else
						txt = "0";
					CellText(ws, cell_txt, txt);
					// ������������
					cell_txt = "M" + row_txt;
					if (serv.flag_alarm == true)
						txt = "1";
					else
						txt = "0";
					CellText(ws, cell_txt, txt);
					// ������
					cell_txt = "N" + row_txt;
					if (serv.flag_tune == true)
						txt = "1";
					else
						txt = "0";
					CellText(ws, cell_txt, txt);
					// ����
					cell_txt = "O" + row_txt;
					if (serv.flag_other == true)
						txt = "1";
					else
						txt = "0";
					CellText(ws, cell_txt, txt);
					// ����� �����
					cell_txt = "P" + row_txt;
					txt = (serv.summ_whole).ToString();
					CellText(ws, cell_txt, txt);
					// ������ � ����� + �������
					cell_txt = "Q" + row_txt;
					txt = (serv.auto_discount_tunemus + serv.auto_discount_other).ToString();
					CellText(ws, cell_txt, txt);

					// �������
					cell_txt = "S" + row_txt;
					if (serv.flag_anti == true)
						txt = "1";
					else
						txt = "0";
					CellText(ws, cell_txt, txt);
					// ������
					cell_txt = "T" + row_txt;
					if (serv.flag_anti == true)
						txt = "1";
					else
						txt = "0";
					CellText(ws, cell_txt, txt);
					// ���������
					cell_txt = "U" + row_txt;
					if (serv.flag_anti == true)
						txt = "1";
					else
						txt = "0";
					CellText(ws, cell_txt, txt);
					// ����� �������
					cell_txt = "V" + row_txt;
					txt = (serv.summ_anti).ToString();
					CellText(ws, cell_txt, txt);
					// ������ �������
					cell_txt = "W" + row_txt;
					txt = (serv.auto_discount_anti).ToString();
					CellText(ws, cell_txt, txt);

					// �����
					cell_txt = "Y" + row_txt;
					if (serv.flag_gibdd == true)
						txt = "1";
					else
						txt = "0";
					CellText(ws, cell_txt, txt);
					// ����� ���
					cell_txt = "Z" + row_txt;
					txt = (serv.summ_sprav).ToString();
					CellText(ws, cell_txt, txt);

					// �����
					cell_txt = "AB" + row_txt;
					if (serv.flag_kasko == true)
						txt = "1";
					else
						txt = "0";
					CellText(ws, cell_txt, txt);
					// �����
					cell_txt = "AC" + row_txt;
					if (serv.flag_osago == true)
						txt = "1";
					else
						txt = "0";
					CellText(ws, cell_txt, txt);

					/*
					// �������
					cell_txt = "J" + row_txt;
					if (serv.flag_anti == true)
						txt = "+";
					else
						txt = "-";
					if (serv.flag_anti1 == true)
						txt += "+";
					else
						txt += "-";
					if (serv.flag_anti2 == true)
						txt += "+";
					else
						txt += "-";
					CellText(ws, cell_txt, txt);
					
					// ���/�������
					cell_txt = "K" + row_txt;
					if (serv.flag_gibdd == true)
						txt = "+";
					else
						txt = "-";
					if (serv.flag_sprav == true)
						txt += "+";
					else
						txt += "-";
					CellText(ws, cell_txt, txt);
					// �����/�����
					cell_txt = "L" + row_txt;
					if (serv.flag_kasko == true)
						txt = "+";
					else
						txt = "-";
					if (serv.flag_osago == true)
						txt += "+";
					else
						txt += "-";
					CellText(ws, cell_txt, txt);

					// ���� ����
					cell_txt = "M" + row_txt;
					txt = serv.auto_summ.ToString();
					CellText(ws, cell_txt, txt);
					// ������ ����
					cell_txt = "N" + row_txt;
					txt = serv.auto_discount_money.ToString();
					CellText(ws, cell_txt, txt);
					// ���� ����
					cell_txt = "O" + row_txt;
					txt = serv.summ_whole.ToString();
					CellText(ws, cell_txt, txt);
					// ������ � �����+�������
					cell_txt = "P" + row_txt;
					txt = (serv.auto_discount_tunemus + serv.auto_discount_other).ToString();
					CellText(ws, cell_txt, txt);
					// �������
					cell_txt = "Q" + row_txt;
					txt = serv.summ_anti.ToString();
					CellText(ws, cell_txt, txt);
					// ������� ������
					cell_txt = "R" + row_txt;
					txt = serv.auto_discount_anti.ToString();
					CellText(ws, cell_txt, txt);
					*/

				}

				row++;	// ������� �� ��������� �����
			}
		}

		override protected void DataToExcelMult(Excel.Worksheet ws, int sheet, int start)
		{
			if(sheet == 1)
			{
				DataToExcelSheet1(ws, start);
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
		}
	}
}
