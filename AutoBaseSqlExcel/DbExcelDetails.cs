using System;
using System.Collections;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbExcelDetails.
	/// </summary>
	public class DbExcelDetails:DbExcel
	{
		ArrayList	details;

		public DbExcelDetails(long workshop, bool liquid, bool guaranty, bool cashless, bool inner)
		{
			// ������� ��������
			FormSelectDateInterval dialog = new FormSelectDateInterval();
			if(dialog.ShowDialog() != DialogResult.OK) return;
			details = new ArrayList();
			DbSqlCardDetail.SelectInArray(details, dialog.StartDate, dialog.EndDate, workshop, liquid, guaranty, cashless, inner);
		}

		public DbExcelDetails()
		{
			// ������� ��������
			FormSelectDateInterval dialog = new FormSelectDateInterval();
			if(dialog.ShowDialog() != DialogResult.OK) return;
			details = new ArrayList();
			DbSqlCardDetail.SelectInArraySpec1(details, dialog.StartDate, dialog.EndDate);
		}

		override protected void TitleFormat(Excel.Worksheet ws)
		{
			// ������������ ������
			FormatColumn(ws, "A1", 32, 8, "Left");
			CellText(ws, "A1", "������������", "Center", 10, true);
			// ����������
			FormatColumn(ws, "B1", 12, 8, "Right", true);
			CellText(ws, "B1", "����������", "Center", 10, true);
			// ����
			FormatColumn(ws, "C1", 12, 8, "Right");
			CellText(ws, "C1", "����", "Center", 10, true);
			// �����
			FormatColumn(ws, "D1", 12, 8, "Right");
			CellText(ws, "D1", "�����", "Center", 10, true);
			// ���� ��������
			//FormatColumn(ws, "E1", 12, 8, "Right");
			//CellText(ws, "E1", "��������", "Center", 10, true);
			// ���� �����
			//FormatColumn(ws, "F1", 12, 8, "Right");
			//CellText(ws, "F1", "�����", "Center", 10, true);
		}

		override protected void DataToExcel(Excel.Worksheet ws, int start)
		{
			string row_txt;
			string cell_txt;
			int row = start;
			string txt;

			foreach(object o in details)
			{
				// ��������
				DtCardDetail detail = (DtCardDetail)o;
			
				// ����������� ������ � Excel
				row_txt = row.ToString();
				// ������������
				cell_txt = "A" + row_txt;
				if(((string)detail.GetData("�������_�����_��������_������")).Length != 0)
					txt = (string)detail.GetData("������������_��������_������") + " (" + (string)detail.GetData("�������_�����_��������_������") + ")";
				else
					txt = (string)detail.GetData("������������_��������_������");
				CellText(ws, cell_txt, txt);
				// ����������
				cell_txt = "B" + row_txt;
				txt = detail.GetData("����������_��������_������").ToString();
				CellText(ws, cell_txt, txt);
				// ����
				cell_txt = "C" + row_txt;
				txt = detail.GetData("����_��������_������").ToString();
				CellText(ws, cell_txt, txt);
				// �����
				cell_txt = "D" + row_txt;
				txt = ((float)detail.GetData("����������_��������_������") * (float)detail.GetData("����_��������_������")).ToString();
				CellText(ws, cell_txt, txt);
				// ���� ��������
				//cell_txt = "E" + row_txt;
				//txt = detail.GetData("��������_��������_������").ToString();
				//CellText(ws, cell_txt, txt);
				// ���� �����
				//cell_txt = "F" + row_txt;
				//txt = detail.GetData("��������_��������_������").ToString();
				//CellText(ws, cell_txt, txt);

				row++;
			}
		}
	}
}
