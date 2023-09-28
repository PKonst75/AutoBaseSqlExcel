using System;
using System.Collections;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbExcelReportTo.
	/// </summary>
	public class DbExcelReportTo:DbExcel
	{
		readonly ArrayList array = new ArrayList();

		public DbExcelReportTo()
		{
			// ������ ���� ������ ������� � ����� �������
			FormSelectDate dlg = new FormSelectDate();
			dlg.ShowDialog();
			if(dlg.DialogResult != DialogResult.OK) return;
			DateTime date_start = dlg.SelectedDate;

			dlg.ShowDialog();
			if(dlg.DialogResult != DialogResult.OK) return;
			DateTime date_end = dlg.SelectedDate;

			// ����� ������ ��������� ������ �������� �������� �� ��������� ����
			// �������, ��� �������� � ����� ����
			// ������������� - ������������� ������
			ArrayList	number_array	= new ArrayList();
			DbSqlCard.SelectCardClosedNumberWorkshop(number_array, date_start, date_end, 1);	
			FormInfoTable info = new FormInfoTable("������ �������");
			info.Show();
			foreach(DtCard element in number_array)
			{		
				DtCard card = DbSqlCard.Find((long)element.GetData("�����_��������"), (int)element.GetData("���_��������"));
				DtAuto  auto = DbSqlAuto.Find((long)card.CodeAuto);

				bool lada = false;
				if (auto != null) lada = auto.IsBrandLada();

				// ���������, �������� �� �������� ��
				if ((card.IsToWork() == true) && (lada == true))
				{
					array.Add(card);
				}
				else
				{
					// ������ ���������� ��������
				}
				info.SetText(card.GetData("�����_�����_��������").ToString());
			}
			info.SetText("����� �������");
			info.Close();
		}

		protected void TitleFormatSheet1(Excel.Worksheet ws)
		{
			// ���� �������� �����/������
			FormatColumn(ws, "A1", 12, 8, "Left");
			CellText(ws, "A1", "��������", "Center", 10, true);
			// ����� �����/������
			FormatColumn(ws, "B1", 12, 8, "Right", true);
			CellText(ws, "B1", "����� ��", "Center", 10, true);
			// ���� �����/������
			FormatColumn(ws, "C1", 12, 8, "Right");
			CellText(ws, "C1", "���� ��", "Center", 10, true);
			// ������ � �������-������������
			FormatColumn(ws, "D1", 27, 8, "Right");
			CellText(ws, "D1", "VIN", "Center", 10, true);
			// ������ � �������
			FormatColumn(ws, "E1", 12, 8, "Right");
			CellText(ws, "E1", "������", "Center", 10, true);
			// ������ � ������ ��
			FormatColumn(ws, "F1", 40, 8, "Right");
			CellText(ws, "F1", "��", "Center", 10, true);

		}

		protected void DataToExcelSheet1(Excel.Worksheet ws, int start)
		{
			//string row_last = "1";
			string row_txt;
			string cell_txt;
			int row = start;
			//int row_summ = 2;
			string txt;

			DtAuto auto;// = null;

			foreach(object o in array)
			{
				// ��������
				DtCard card = (DtCard)o;

				// ��������� �������������� ������
				auto = DbSqlAuto.Find((long)card.CodeAuto);
				
				
				// ����������� ������ � Excel
				row_txt = row.ToString();
				// ���� ��������
				cell_txt = "A" + row_txt;
				txt = ((DateTime)card.GetData("����_�����_������_��������")).ToShortDateString();
				CellText(ws, cell_txt, txt);
				// ����� ��
				cell_txt = "B" + row_txt;
				txt = card.GetData("�����_�����_��������").ToString();
				CellText(ws, cell_txt, txt);
				// ���� ��
				cell_txt = "C" + row_txt;
				txt = ((DateTime)card.GetData("����")).ToShortDateString();
				CellText(ws, cell_txt, txt);
				// ������ �����������
				if (auto != null)
				{
					cell_txt = "D" + row_txt;
					txt = (string)auto.GetData("VIN");
					CellText(ws, cell_txt, txt);
				}
				// ������
				cell_txt = "E" + row_txt;
				txt = card.GetData("������_��������").ToString();
				CellText(ws, cell_txt, txt);
				// ������������ ��
				cell_txt = "F" + row_txt;
				txt = card.NameToWork();
				CellText(ws, cell_txt, txt);

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
