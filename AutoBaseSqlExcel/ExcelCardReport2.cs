using System;
using System.Collections;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for ExcelCardReport2.
	/// </summary>
	public class ExcelCardReport2
	{
		private Excel.Workbook		workbook;
		private DbCardReport1		cardReport1;
		private string				file;
		private static int[]		current_row = new int[2];	// ��������� ����������� ������ ������ ��������

		public static void DownloadList(ArrayList array)
		{
			Excel.Workbook		wb;
			wb					= null;
			string				fileName;

			// ������ ����� ����� ���� ���������
			OpenFileDialog dlg	= new OpenFileDialog();
			dlg.Filter			= "���� ������ EXCEL (*.xls)|*.xls";
			dlg.CheckFileExists	= true;
			dlg.CheckPathExists	= true;
			dlg.Multiselect		= false;
			dlg.Title			= "�������� ���� ������ ��� �����������";
			dlg.ShowDialog();
			if(dlg.FileName.Length == 0)
			{
				DialogResult res = MessageBox.Show(null, "������� ����� ���� ������?", "������", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if(res == DialogResult.No) return;
				fileName = "";
			}
			else
			{
				fileName = dlg.FileName;
			}
			
			
			foreach(object o in array)
			{
				DbCard card	= (DbCard)o;
				if(card != null)
				{
					ExcelCardReport2 excelCardReport2	= new ExcelCardReport2(card, wb, fileName);
					wb		= excelCardReport2.StartDownLoad();
				}
			}

			ShowExcel(wb);
		}

		public ExcelCardReport2(DbCard cardSrc, Excel.Workbook workbookSrc, string fileName)
		{
			cardReport1		= new DbCardReport1(cardSrc);
			workbook		= workbookSrc;
			file			= fileName;
		}
		public static void ShowExcel(Excel.Workbook wb)
		{
			if(wb == null) return;
			try
			{
				wb.Application.UserControl	= true;
				wb.Application.Visible		= true;
			}
			catch(Exception E)
			{
				Db.SetErrorMessage(E.Message);
			}
		}

		public Excel.Workbook StartDownLoad()
		{
			Excel.Application app;
			Excel.Workbook wb = null;

			try
			{
				if(workbook == null)
				{
					app			= new Excel.Application();				// ����� ��������� ���������� Excel
					// ���� ����� ���� ������, ��������� ������������ �����
					if(file.Length == 0)
					{
						wb			= app.Workbooks.Add(Missing.Value);		// ����� ����� Excel
						// ����������� ����� �����
						while(wb.Worksheets.Count < 1)
							wb.Worksheets.Add(Missing.Value, wb.Worksheets[wb.Worksheets.Count], 1, Missing.Value);
						// ���� ��� ������
						((Excel.Worksheet)wb.Worksheets[1]).Name	= "��";
						DownloadTitleTO((Excel.Worksheet)wb.Worksheets[1]);			// �������� ���������						
					}
					else
					{
						wb			= app.Workbooks.Open(file, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
					}
					workbook	= wb;
				}
				else
				{
					wb			= workbook;
					app			= wb.Application;
				}
				//��������
				DownloadReport();					// �������� ���������� ��������� ������
			}
			catch(Exception E)
			{
				Db.SetException(E);
			}
			return wb;
		}

		#region ��
		protected void DownloadTitleTO(Excel.Worksheet ws)
		{
			// ��������� ��������� �� ������������ ���� � Excel
			Excel.Range cell;
			Excel.Range rng;
			string		txt;
			try
			{
				rng							= ws.Rows;
				// ����� �����-������
				txt	= "�����";
				cell							= rng.get_Range("A1", Missing.Value);
				cell.Value2						= txt;
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 8;
				cell.EntireColumn.Font.Size		= 8;
				// ���� �����-������
				txt	= "����";
				cell							= rng.get_Range("B1", Missing.Value);
				cell.Value2						= txt;
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 9;
				cell.EntireColumn.Font.Size		= 8;
				// ������ ����������
				txt	= "������";
				cell							= rng.get_Range("C1", Missing.Value);
				cell.Value2						= txt;
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 9;
				cell.EntireColumn.Font.Size		= 8;
				// VIN
				txt	= "Vin";
				cell								= rng.get_Range("D1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 14;
				cell.EntireColumn.Font.Size			= 8;
				// ������ ������� �� ������� �� ��
				txt	= "��";
				cell								= rng.get_Range("E1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 25;
				cell.EntireColumn.Font.Size			= 8;
				// ��������
				txt	= "��������";
				cell								= rng.get_Range("F1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 25;
				cell.EntireColumn.Font.Size			= 8;
				// �������
				txt	= "�������";
				cell								= rng.get_Range("G1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 18;
				cell.EntireColumn.Font.Size			= 8;
				// �������������
				txt	= "�������������";
				cell								= rng.get_Range("H1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 25;
				cell.EntireColumn.Font.Size			= 8;
				// �������
				txt	= "�������";
				cell								= rng.get_Range("I1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 18;
				cell.EntireColumn.Font.Size			= 8;

				// ��������� ������
				ws.PageSetup.Orientation	= Excel.XlPageOrientation.xlLandscape;
				ws.PageSetup.LeftMargin		= ws.Application.InchesToPoints(0.393700787401575);
				ws.PageSetup.RightMargin	= ws.Application.InchesToPoints(0.393700787401575);
				ws.PageSetup.TopMargin		= ws.Application.InchesToPoints(0.590551181102362);
				ws.PageSetup.BottomMargin	= ws.Application.InchesToPoints(0.590551181102362);
				ws.PageSetup.HeaderMargin	= ws.Application.InchesToPoints(0.511811023622047);
				ws.PageSetup.FooterMargin	= ws.Application.InchesToPoints(0.511811023622047);
				ws.PageSetup.PrintArea		= "$A:$I";
			}
			catch(Exception E)
			{
				Db.SetException(E);
			}
		}
		protected void DownloadReportTO(Excel.Worksheet ws, int row)
		{
			// ��������� ������ �� ������������ ���� � Excel
			Excel.Range cell;
			Excel.Range rng;
			string		txt;
			string		rowTxt;
				
			rowTxt		= row.ToString();
			try
			{
				rng							= ws.Rows;
				// ����� �����-������
				txt							= cardReport1.card.WarrantNumber.ToString();
				cell						= rng.get_Range("A" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ���� �����-������
				txt							= cardReport1.card.WarrantDateTxt;
				cell						= rng.get_Range("B" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ������ ���������� 
				txt							= cardReport1.card.Auto.ModelTxt;
				cell						= rng.get_Range("C" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// VIN 
				txt							= cardReport1.card.Auto.Vin;
				cell						= rng.get_Range("D" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ������ �������� �� �� 
				//txt						= cardReport1.to_text;
				if(cardReport1.card.Auto.IsSellDate)
					txt						= cardReport1.card.Auto.SellDateTxt;
				else
					txt						= "----";
				cell						= rng.get_Range("E" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ��������
				txt							= cardReport1.card.PartnerNameTxt;
				cell						= rng.get_Range("F" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ��������
				txt							= cardReport1.card.PartnerPhoneTxt;
				cell						= rng.get_Range("G" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// �������������
				txt							= cardReport1.card.RepresentNameTxt;
				cell						= rng.get_Range("H" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// �������
				txt							= cardReport1.card.RepresentPhoneTxt;
				cell						= rng.get_Range("I" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.Font.Bold				= true;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
			}
			catch(Exception E)
			{
				Db.SetException(E);
				return;
			}
			return;
		}

		protected void DownloadReportTO(Excel.Worksheet ws)
		{
			int count	= 0;
			int index	= ws.Index;
			if(ExcelCardReport2.current_row[index] == 0)
			{
				Excel.Range rng = ws.UsedRange;
				count = rng.Rows.Count;
			}
			else
			{
				count = ExcelCardReport2.current_row[index];
				ExcelCardReport2.current_row[index]++;
			}
			DownloadReportTO(ws, count + 1);
		}
		#endregion
		

		protected void DownloadReport()
		{
			// ���������� ����������� ���� �������� ��� ��������
			if(cardReport1.card.Auto.IsSellDate == false || (cardReport1.card.Auto.SellDate.Month > 1 && cardReport1.card.Auto.SellDate.Month < 5))
                DownloadReportTO((Excel.Worksheet)workbook.Worksheets[1]);
		}

		protected void DownloadReport1()
		{
			// ���������� ����������� ���� �������� ��� ��������
			if(cardReport1.to_is)
                DownloadReportTO((Excel.Worksheet)workbook.Worksheets[1]);
		}
	}
}
