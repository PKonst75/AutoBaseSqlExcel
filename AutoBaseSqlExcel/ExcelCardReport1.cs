using System;
using System.Collections;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for ExcelCardReport1.
	/// </summary>
	public class ExcelCardReport1
	{
		private Excel.Workbook		workbook;
		private DbCardReport		cardReport;
		private string				file;
		private static int[]		current_row = new int[10];	// ��������� ����������� ������ ������ ��������

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
		


			foreach (object o in array)
			{
				DbCard card	= (DbCard)o;
				if(card != null)
				{
					ExcelCardReport1 excelCardReport	= new ExcelCardReport1(card, wb, fileName);
					wb		= excelCardReport.StartDownLoad();
				}
			}

			ShowExcel(wb);
		}

		public ExcelCardReport1(DbCard cardSrc, Excel.Workbook workbookSrc, string fileName)
		{
			cardReport		= new DbCardReport(cardSrc);
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
						while(wb.Worksheets.Count < 2)
							wb.Worksheets.Add(Missing.Value, wb.Worksheets[wb.Worksheets.Count], 1, Missing.Value);
						// ���� ��� �������
						((Excel.Worksheet)wb.Worksheets[1]).Name	= "������";
						DownloadTitleDetail((Excel.Worksheet)wb.Worksheets[1]);			// �������� ���������
						// ���� ��� ������� ��������
						((Excel.Worksheet)wb.Worksheets[2]).Name	= "������-��������";
						DownloadTitleDetail((Excel.Worksheet)wb.Worksheets[2]);			// �������� ���������
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
		protected void DownloadTitleDetail(Excel.Worksheet ws)
		{
			// ��������� ��������� �� ������������ ���� � Excel
			Excel.Range cell;
			Excel.Range rng;
			string		txt;
			try
			{
				rng							= ws.Rows;
				// ����� �������� / ����� ������
				txt	= "� / �";
				cell							= rng.get_Range("A1", Missing.Value);
				cell.Value2						= txt;
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 9;
				cell.EntireColumn.Font.Size		= 8;
				// ���� �������� �����-������
				txt	= "������";
				cell							= rng.get_Range("B1", Missing.Value);
				cell.Value2						= txt;
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 9;
				cell.EntireColumn.Font.Size		= 8;
				// ���� ��������
				txt	= "������";
				cell							= rng.get_Range("C1", Missing.Value);
				cell.Value2						= txt;
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 9;
				cell.EntireColumn.Font.Size		= 8;
				// ������
				txt	= "������";
				cell								= rng.get_Range("D1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 20;
				cell.EntireColumn.Font.Size			= 8;
				// VIN
				txt	= "VIN";
				cell								= rng.get_Range("E1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 20;
				cell.EntireColumn.Font.Size			= 8;
				// ������
				txt	= "������";
				cell								= rng.get_Range("F1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 60;
				cell.EntireColumn.Font.Size			= 8;
				// ������ ���
				txt	= "������ ���";
				cell								= rng.get_Range("G1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 20;
				cell.EntireColumn.Font.Size			= 8;
				
				// ��������� ������
				ws.PageSetup.Orientation	= Excel.XlPageOrientation.xlLandscape;
				ws.PageSetup.LeftMargin		= ws.Application.InchesToPoints(0.393700787401575);
				ws.PageSetup.RightMargin	= ws.Application.InchesToPoints(0.393700787401575);
				ws.PageSetup.TopMargin		= ws.Application.InchesToPoints(0.590551181102362);
				ws.PageSetup.BottomMargin	= ws.Application.InchesToPoints(0.590551181102362);
				ws.PageSetup.HeaderMargin	= ws.Application.InchesToPoints(0.511811023622047);
				ws.PageSetup.FooterMargin	= ws.Application.InchesToPoints(0.511811023622047);
				ws.PageSetup.PrintArea		= "$A:$M";
			}
			catch(Exception E)
			{
				Db.SetException(E);
			}
		}
		protected void DownloadReportDetail(Excel.Worksheet ws, int row, DbCardDetail detail, bool first)
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
				// ����� �������� / ����� ������
				if(first == true)
				{
					txt							= cardReport.NumberTxt + " / " + cardReport.WarrantNumber;
					cell						= rng.get_Range("A" + rowTxt, Missing.Value);
					cell.Value2					= txt;
					cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
					// ���� �������� �����-������
					txt							= cardReport.WarrantDate;
					cell						= rng.get_Range("B" + rowTxt, Missing.Value);
					cell.Value2					= txt;
					cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
					// ���� �������� �����-������ 
					txt							= cardReport.CloseDate;
					cell						= rng.get_Range("C" + rowTxt, Missing.Value);
					cell.Value2					= txt;
					cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
					// ������
					txt							= cardReport.AutoModel;
					cell						= rng.get_Range("D" + rowTxt, Missing.Value);
					cell.Value2					= txt;
					cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
					// VIN 
					txt							= cardReport.AutoVIN;
					cell						= rng.get_Range("E" + rowTxt, Missing.Value);
					cell.Value2					= txt;
					cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				}
				// ������ - ������������
				txt							= detail.DetailNameTxt;
				cell						= rng.get_Range("F" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ������ - ���
				txt							= detail.CodeDetailTxt;
				cell						= rng.get_Range("G" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
			}
			catch(Exception E)
			{
				Db.SetException(E);
				return;
			}
			return;
		}

		protected void DownloadReportDetail(Excel.Worksheet ws, bool guaranty)
		{
			bool first = true;
			int count	= 0;
			int index	= ws.Index;
			if(ExcelCardReport1.current_row[index] == 0)
			{
				Excel.Range rng = ws.UsedRange;
				count = rng.Rows.Count;
				ExcelCardReport1.current_row[index] = count + 1; // ?
			}
			else
			{
				count = ExcelCardReport1.current_row[index];
				ExcelCardReport1.current_row[index]++;
			}
			foreach(object o in cardReport.CardDetails)
			{
				DbCardDetail detail = (DbCardDetail)o;
				if (detail.Guaranty == guaranty)
				{
					DownloadReportDetail(ws, count + 1, detail, first);
					first = false;
					count++;
				}
			}
			ExcelCardReport1.current_row[index] = count;
		}
		#endregion

		protected void DownloadReport()
		{
			if(cardReport.CountDetailGuaranty > 0)
                this.DownloadReportDetail((Excel.Worksheet)workbook.Worksheets[2], true);
			if(cardReport.CountDetail > 0)
                this.DownloadReportDetail((Excel.Worksheet)workbook.Worksheets[1], false);
		}
	}
}
