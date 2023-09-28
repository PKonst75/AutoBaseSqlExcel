using System;
using System.Collections;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for ExcelCardReport.
	/// </summary>
	public class ExcelCardReport:DbExcel
	{
		private Excel.Workbook		workbook;
		private DbCardReport		cardReport;
		private static int[]		current_row = new int[13+1];    // ��������� ����������� ������ ������ ��������
		private DtCard _card; // ��������
		private DtTxtCard _cardTxt; // ��������� ����������� ��������

		public static void DownloadList(ArrayList array)
		{
			Excel.Workbook wb = null;
			//string				fileName;

			string fileName = SelectFileDialog("�������� ���� ������ ��� �����������");
			if (fileName == DbExcel.CANCEL_SELECT) return; // ����� �� ������ �����
			
			foreach(object o in array)
			{
				DbCard card	= (DbCard)o;
				if(card != null)
				{
					ExcelCardReport excelCardReport	= new ExcelCardReport(card, wb);
					wb		= excelCardReport.StartDownLoad(fileName);
				}
			}
			ShowExcel(wb);
		}

		public ExcelCardReport(DbCard cardSrc, Excel.Workbook workbookSrc)
		{
			cardReport		= new DbCardReport(cardSrc);
			workbook		= workbookSrc;
		}

		public Excel.Workbook StartDownLoad(string file)
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
						while(wb.Worksheets.Count != 12+1)
							wb.Worksheets.Add(Missing.Value, wb.Worksheets[wb.Worksheets.Count], 1, Missing.Value);
						// ���� ��� ��
						((Excel.Worksheet)wb.Worksheets[1]).Name	= "��";
						DownloadTitleTO((Excel.Worksheet)wb.Worksheets[1]);			// �������� ���������
						// ���� ��� ��
						((Excel.Worksheet)wb.Worksheets[2]).Name	= "��-��";
						DownloadTitleTO((Excel.Worksheet)wb.Worksheets[2]);			// �������� ���������
						// ���� ��� ��������
						((Excel.Worksheet)wb.Worksheets[3]).Name	= "��������";
						DownloadTitleGuaranty((Excel.Worksheet)wb.Worksheets[3]);	// �������� ���������
						// ���� ��� ���
						((Excel.Worksheet)wb.Worksheets[4]).Name	= "���";
						DownloadTitlePPP((Excel.Worksheet)wb.Worksheets[4]);		// �������� ���������
						// ���� ��� �������
						((Excel.Worksheet)wb.Worksheets[5]).Name	= "�������"; //���������� 2018
						DownloadTitleWork((Excel.Worksheet)wb.Worksheets[5]);		// �������� ���������
						// ���� ��� ������
                        ((Excel.Worksheet)wb.Worksheets[6]).Name = "������ ������"; //���������� 2018
						DownloadTitleWork((Excel.Worksheet)wb.Worksheets[6]);		// �������� ���������
						// ���� ��� �����
						((Excel.Worksheet)wb.Worksheets[7]).Name	= "����";
						//DownloadTitleWork((Excel.Worksheet)wb.Worksheets[7]);		// �������� ���������
						DownloadTitleTO((Excel.Worksheet)wb.Worksheets[7]);		// �������� ���������
						// ���� ��� �������
						((Excel.Worksheet)wb.Worksheets[7+1]).Name	= "�������";
						DownloadTitleWork((Excel.Worksheet)wb.Worksheets[7+1]);		// �������� ���������
						// ���� ��� �����
						((Excel.Worksheet)wb.Worksheets[8+1]).Name	= "�����";
						DownloadTitleWork((Excel.Worksheet)wb.Worksheets[8+1]);		// �������� ���������
						// ���� ��� �����
						((Excel.Worksheet)wb.Worksheets[9+1]).Name	= "������";
						DownloadTitleReport((Excel.Worksheet)wb.Worksheets[9+1]);		// �������� ���������
						// ����������� ��������
						((Excel.Worksheet)wb.Worksheets[10+1]).Name	= "�������� ����������";
						DownloadTitleGuarantyExtend((Excel.Worksheet)wb.Worksheets[10+1]);		// �������� ���������
                        // ����������� ��������
                        ((Excel.Worksheet)wb.Worksheets[11 + 1]).Name = "������� �����-��";
                        DownloadTitleTO((Excel.Worksheet)wb.Worksheets[11 + 1]);        // �������� ���������	
						// ����������� ��������
						((Excel.Worksheet)wb.Worksheets[12 + 1]).Name = "���� ����������";
						DownloadTitleWork((Excel.Worksheet)wb.Worksheets[12 + 1]);        // �������� ���������

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
			Excel.Range rng;	
			try
			{
				rng							= ws.Rows;			
				TryColumnFormatTitleBold(rng, "��������", "A1", 9, 8, EXEL_HORIZONT_ALIGN.LEFT);	
				TryColumnFormatTitleBold(rng, "�����", "B1", 8, 8, EXEL_HORIZONT_ALIGN.LEFT);		
				TryColumnFormatTitleBold(rng, "����", "C1", 9, 8, EXEL_HORIZONT_ALIGN.LEFT);		
				TryColumnFormatTitleBold(rng, "��", "D1", 9, 8, EXEL_HORIZONT_ALIGN.LEFT, "# ##0,00");	
				TryColumnFormatTitleBold(rng, "��������", "E1", 9, 8, EXEL_HORIZONT_ALIGN.LEFT, "# ##0,00");
				TryColumnFormatTitleBold(rng, "������.", "F1", 9, 8, EXEL_HORIZONT_ALIGN.LEFT, "# ##0,00");
				TryColumnFormatTitleBold(rng, "������", "G1", 9, 8, EXEL_HORIZONT_ALIGN.LEFT, "# ##0,00");
				TryColumnFormatTitleBold(rng, "�����", "H1", 9, 8, EXEL_HORIZONT_ALIGN.LEFT, "# ##0,00");
				TryColumnFormatTitleBold(rng, "����", "I1", 9, 8, EXEL_HORIZONT_ALIGN.LEFT, "# ##0,00");
				TryColumnFormatTitleBold(rng, "������", "J1", 9, 8, EXEL_HORIZONT_ALIGN.LEFT, "# ##0,00");
				TryColumnFormatTitleBold(rng, "� ������", "K1", 9, 8, EXEL_HORIZONT_ALIGN.LEFT, "# ##0,00");
				TryColumnFormatTitleBold(rng, "������", "L1", 9, 8, EXEL_HORIZONT_ALIGN.LEFT, "# ##0,00");
				TryColumnFormatTitleBold(rng, "������", "M1", 9, 8, EXEL_HORIZONT_ALIGN.LEFT, "# ##0,00");
				TryColumnFormatTitleBold(rng, "������.", "N1", 9, 8, EXEL_HORIZONT_ALIGN.LEFT, "# ##0,00");
				TryColumnFormatTitleBold(rng, "���.", "O1", 4, 8, EXEL_HORIZONT_ALIGN.LEFT);
				TryColumnFormatTitleBold(rng, "����������", "P1", 15, 8, EXEL_HORIZONT_ALIGN.LEFT);
				TryColumnFormatTitleBold(rng, "������", "Q1", 15, 8, EXEL_HORIZONT_ALIGN.LEFT);
				TryColumnFormatTitleBold(rng, "VIN", "R1", 20, 8, EXEL_HORIZONT_ALIGN.LEFT);
				TryColumnFormatTitleBold(rng, "���.����", "S1", 9, 8, EXEL_HORIZONT_ALIGN.LEFT);

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
				rng	= ws.Rows;
				TrySetCellBorderText(rng, "A" + rowTxt, cardReport.CloseDate);
				TrySetCellBorderText(rng, "B" + rowTxt, cardReport.WarrantNumber);
				TrySetCellBorderText(rng, "C" + rowTxt, cardReport.WarrantDate);
				TrySetCellBorderFloat(rng, "D" + rowTxt, cardReport.CardTOD);
				TrySetCellBorderFloat(rng, "E" + rowTxt, cardReport.CardOilD);
				TrySetCellBorderFloat(rng, "F" + rowTxt, cardReport.CardOilInputD);
				TrySetCellBorderFloat(rng, "G" + rowTxt, cardReport.CardWorkNoWashD);
				TrySetCellBorderFloat(rng, "H" + rowTxt, cardReport.CardWashD);
				TrySetCellBorderFloat(rng, "I" + rowTxt, cardReport.WorkD);
				TrySetCellBorderFloat(rng, "J" + rowTxt, cardReport.CardDiscountD, EXEL_TEXT_TYPE.BOLD);
				TrySetCellBorderFloat(rng, "K" + rowTxt, cardReport.CardPayD, EXEL_TEXT_TYPE.BOLD);
				TrySetCellBorderText(rng, "L" + rowTxt, "");
				TrySetCellBorderFloat(rng, "M" + rowTxt, cardReport.CardDetailD, EXEL_TEXT_TYPE.BOLD);
				TrySetCellBorderFloat(rng, "N" + rowTxt, cardReport.CardDetailInputD);
				// ���� �������� ������
				//txt							= cardReport.CloseDate;
				//cell						= rng.get_Range("A" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				//cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ����� ��������
				//txt							= cardReport.WarrantNumber;
				//cell						= rng.get_Range("B" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				//cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ���� 
				//txt							= cardReport.WarrantDate;
				//cell						= rng.get_Range("C" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				//cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// �� 
				//		cell						= rng.get_Range("D" + rowTxt, Missing.Value);
				//		cell.Value2 = cardReport.CardTOD;
				//		cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ����� 			
				//cell						= rng.get_Range("E" + rowTxt, Missing.Value);
				//cell.Value2 = cardReport.CardOilD;
				//cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ����� - �������������

				//	cell						= rng.get_Range("F" + rowTxt, Missing.Value);
				//	cell.Value2 = cardReport.CardOilInputD;
				//	cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ������ - ��� �����
				//	cell						= rng.get_Range("G" + rowTxt, Missing.Value);
				//	cell.Value2 = cardReport.CardWorkNoWashD;
				//	cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// �����

				//	cell						= rng.get_Range("H" + rowTxt, Missing.Value);
				//	cell.Value2 = cardReport.CardWashD;
				//	cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ������ ����

				//cell						= rng.get_Range("I" + rowTxt, Missing.Value);
				//cell.Value2 = cardReport.WorkD;
				//cell.Font.Bold				= true;
				//cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ����� ������

				//	cell						= rng.get_Range("J" + rowTxt, Missing.Value);
				//	cell.Value2 = cardReport.CardDiscountD;
				//	cell.Font.Bold				= true;
				//	cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// � ������

				//cell						= rng.get_Range("K" + rowTxt, Missing.Value);
				//cell.Value2 = cardReport.CardPayD;
				//cell.Font.Bold				= true;
				//cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ������
				//	cell						= rng.get_Range("L" + rowTxt, Missing.Value);
				//	cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ������ 

				//	cell						= rng.get_Range("M" + rowTxt, Missing.Value);
				//	cell.Value2 = cardReport.CardDetailD;
				//	cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ������ - �������������

			//	cell						= rng.get_Range("N" + rowTxt, Missing.Value);
			//	cell.Value2 = cardReport.CardDetailInputD;
			//	cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// �������� ��/���
				if(cardReport.HaveGuaranty())
					txt							= "��";
				else
					txt							= "";
				cell						= rng.get_Range("O" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ������ 
				txt							= cardReport.AutoModel;
				cell						= rng.get_Range("Q" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				// VIN 
				txt							= cardReport.AutoVIN;
				cell						= rng.get_Range("R" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				// ��������������� ���� 
				txt							= cardReport.AutoSignNo;
				cell						= rng.get_Range("S" + rowTxt, Missing.Value);
				cell.Value2					= txt;
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
			int count;
			int index	= ws.Index;
			if(ExcelCardReport.current_row[index] == 0)
			{
				Excel.Range rng = ws.UsedRange;
				count = rng.Rows.Count;
			}
			else
			{
				count = ExcelCardReport.current_row[index];
				ExcelCardReport.current_row[index]++;
			}
			DownloadReportTO(ws, count + 1);
		}
		#endregion
		
		#region ��������
		protected void DownloadTitleGuaranty(Excel.Worksheet ws)
		{
			// ��������� ��������� �� ������������ ���� � Excel
			Excel.Range cell;
			Excel.Range rng;
			string		txt;
			try
			{
				rng							= ws.Rows;
				// ���� ��������
				txt	= "��������";
				cell							= rng.get_Range("A1", Missing.Value);
				cell.Value2						= txt;
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 9;
				cell.EntireColumn.Font.Size		= 8;
				// ����� �����-������
				txt	= "�����";
				cell							= rng.get_Range("B1", Missing.Value);
				cell.Value2						= txt;
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 8;
				cell.EntireColumn.Font.Size		= 8;
				// ���� �����-������
				txt	= "����";
				cell							= rng.get_Range("C1", Missing.Value);
				cell.Value2						= txt;
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 9;
				cell.EntireColumn.Font.Size		= 8;
				// ������ ��
				txt	= "��";
				cell								= rng.get_Range("D1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 9;
				cell.EntireColumn.Font.Size			= 8;
				cell.EntireColumn.NumberFormatLocal	= "# ##0,00";
				// �������� � �����-������
				txt	= "��������";
				cell								= rng.get_Range("E1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 9;
				cell.EntireColumn.Font.Size			= 8;
				cell.EntireColumn.NumberFormatLocal	= "# ##0,00";
				// ������ ��� �����
				txt	= "������";
				cell								= rng.get_Range("F1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 9;
				cell.EntireColumn.Font.Size			= 8;
				cell.EntireColumn.NumberFormatLocal	= "# ##0,00";
				// ������-��������� ������
				txt	= "�����";
				cell								= rng.get_Range("G1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 9;
				cell.EntireColumn.Font.Size			= 8;
				cell.EntireColumn.NumberFormatLocal	= "# ##0,00";
				// ����� �������
				txt	= "������";
				cell								= rng.get_Range("H1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 9;
				cell.EntireColumn.Font.Size			= 8;
				cell.EntireColumn.NumberFormatLocal	= "# ##0,00";
				// �������� ���
				txt	= "���.";
				cell							= rng.get_Range("I1", Missing.Value);
				cell.Value2						= txt;
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 22;
				cell.EntireColumn.Font.Size		= 8;
				// �������������
				cell							= rng.get_Range("J1", Missing.Value);
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 10;
				cell.EntireColumn.Font.Size		= 8;
				// ������ ������� ��� ��� ����������
				// 1
				cell							= rng.get_Range("K1", Missing.Value);
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 5;
				cell.EntireColumn.Font.Size		= 8;
				// ������
				txt	= "������";
				cell							= rng.get_Range("L1", Missing.Value);
				cell.Value2						= txt;
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 15;
				cell.EntireColumn.Font.Size		= 8;
				// VIN
				txt	= "VIN";
				cell							= rng.get_Range("M1", Missing.Value);
				cell.Value2						= txt;
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 20;
				cell.EntireColumn.Font.Size		= 8;		
				// ��������������� ����
				txt	= "���.����";
				cell							= rng.get_Range("N1", Missing.Value);
				cell.Value2						= txt;
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 9;
				cell.EntireColumn.Font.Size		= 8;

				// ��������� ������
				ws.PageSetup.Orientation	= Excel.XlPageOrientation.xlLandscape;
				ws.PageSetup.LeftMargin		= ws.Application.InchesToPoints(0.393700787401575);
				ws.PageSetup.RightMargin	= ws.Application.InchesToPoints(0.393700787401575);
				ws.PageSetup.TopMargin		= ws.Application.InchesToPoints(0.590551181102362);
				ws.PageSetup.BottomMargin	= ws.Application.InchesToPoints(0.590551181102362);
				ws.PageSetup.HeaderMargin	= ws.Application.InchesToPoints(0.511811023622047);
				ws.PageSetup.FooterMargin	= ws.Application.InchesToPoints(0.511811023622047);
				ws.PageSetup.PrintArea		= "$A:$K";
			}
			catch(Exception E)
			{
				Db.SetException(E);
			}
		}
		protected void DownloadReportGuaranty(Excel.Worksheet ws, int row)
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
				// ���� �������� ������
				txt							= cardReport.CloseDate;
				cell						= rng.get_Range("A" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ����� ��������
				txt							= cardReport.WarrantNumber;
				cell						= rng.get_Range("B" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ���� 
				txt							= cardReport.WarrantDate;
				cell						= rng.get_Range("C" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// �� 
				//txt							= cardReport.CardTOGuaranty;
				cell						= rng.get_Range("D" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2 = cardReport.CardTOGuarantyD;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ����� 
				//txt							= cardReport.CardOilGuaranty;
				cell						= rng.get_Range("E" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2 = cardReport.CardOilGuarantyD;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ������ - ��� �����
				//txt							= cardReport.CardWorkNoWashGuaranty;
				cell						= rng.get_Range("F" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2 = cardReport.CardWorkNoWashGuarantyD;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// �����
				//txt							= cardReport.CardWashGuaranty;
				cell						= rng.get_Range("G" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2 = cardReport.CardWashGuarantyD;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ������ 
				//txt							= cardReport.CardDetailGuaranty;
				//txt							= cardReport.CardDetailGuarantyInput;
				cell						= rng.get_Range("H" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2 = cardReport.CardDetailGuarantyInputD;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ��� ���������
				txt							= cardReport.CardGuarantyType;
				cell						= rng.get_Range("I" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// �������������
				txt							= cardReport.CardWorkshopTxt;
				cell						= rng.get_Range("J" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ������ 
				txt							= cardReport.AutoModel;
				cell						= rng.get_Range("L" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// VIN 
				txt							= cardReport.AutoVIN;
				cell						= rng.get_Range("M" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ��������������� ���� 
				txt							= cardReport.AutoSignNo;
				cell						= rng.get_Range("N" + rowTxt, Missing.Value);
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

		protected void DownloadReportGuaranty(Excel.Worksheet ws)
		{
			//Excel.Range rng = ws.UsedRange;
			//int count = rng.Rows.Count;
			int count	= 0;
			int index	= ws.Index;
			if(ExcelCardReport.current_row[index] == 0)
			{
				Excel.Range rng = ws.UsedRange;
				count = rng.Rows.Count;
			}
			else
			{
				count = ExcelCardReport.current_row[index];
				ExcelCardReport.current_row[index]++;
			}
			DownloadReportGuaranty(ws, count + 1);
		}
		#endregion

		#region ���� ���
		protected void DownloadTitlePPP(Excel.Worksheet ws)
		{
			// ��������� ��������� �� ������������ ���� � Excel
			Excel.Range cell;
			Excel.Range rng;
			string		txt;
			try
			{
				rng							= ws.Rows;
				// ���� ��������
				txt	= "��������";
				cell							= rng.get_Range("A1", Missing.Value);
				cell.Value2						= txt;
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 9;
				cell.EntireColumn.Font.Size		= 8;
				// ����� �����-������
				txt	= "�����";
				cell							= rng.get_Range("B1", Missing.Value);
				cell.Value2						= txt;
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 8;
				cell.EntireColumn.Font.Size		= 8;
				// ���� �����-������
				txt	= "����";
				cell							= rng.get_Range("C1", Missing.Value);
				cell.Value2						= txt;
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 9;
				cell.EntireColumn.Font.Size		= 8;
				// ������ ��
				txt	= "��";
				cell								= rng.get_Range("D1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 9;
				cell.EntireColumn.Font.Size			= 8;
				cell.EntireColumn.NumberFormatLocal	= "# ##0,00";
				// ������ �� - ��������
				txt	= "�� ���.";
				cell								= rng.get_Range("E1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 9;
				cell.EntireColumn.Font.Size			= 8;
				cell.EntireColumn.NumberFormatLocal	= "# ##0,00";
				// �������� � �����-������
				txt	= "��������";
				cell								= rng.get_Range("F1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 9;
				cell.EntireColumn.Font.Size			= 8;
				cell.EntireColumn.NumberFormatLocal	= "# ##0,00";
				// �������� � �����-������ - ��������
				txt	= "�������� - ���.";
				cell								= rng.get_Range("G1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 9;
				cell.EntireColumn.Font.Size			= 8;
				cell.EntireColumn.NumberFormatLocal	= "# ##0,00";
				// ������ ��� �����
				txt	= "������";
				cell								= rng.get_Range("H1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 9;
				cell.EntireColumn.Font.Size			= 8;
				cell.EntireColumn.NumberFormatLocal	= "# ##0,00";
				// ������-��������� ������
				txt	= "�����";
				cell								= rng.get_Range("I1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 9;
				cell.EntireColumn.Font.Size			= 8;
				cell.EntireColumn.NumberFormatLocal	= "# ##0,00";
				// ������ ��� ����� - ��������
				txt	= "������ ���.";
				cell								= rng.get_Range("J1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 9;
				cell.EntireColumn.Font.Size			= 8;
				cell.EntireColumn.NumberFormatLocal	= "# ##0,00";
				// ������-��������� ������ - ��������
				txt	= "����� ���.";
				cell								= rng.get_Range("K1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 9;
				cell.EntireColumn.Font.Size			= 8;
				cell.EntireColumn.NumberFormatLocal	= "# ##0,00";
				// ����� �������
				txt	= "������";
				cell								= rng.get_Range("L1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 9;
				cell.EntireColumn.Font.Size			= 8;
				cell.EntireColumn.NumberFormatLocal	= "# ##0,00";
				// ����� ������� - ��������
				txt	= "������ - ���.";
				cell								= rng.get_Range("M1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 9;
				cell.EntireColumn.Font.Size			= 8;
				cell.EntireColumn.NumberFormatLocal	= "# ##0,00";
				
				// ������ ������� ��� ��� ����������
				// 1
				cell							= rng.get_Range("N1", Missing.Value);
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 5;
				cell.EntireColumn.Font.Size		= 8;
				// ������
				txt	= "������";
				cell							= rng.get_Range("O1", Missing.Value);
				cell.Value2						= txt;
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 15;
				cell.EntireColumn.Font.Size		= 8;
				// VIN
				txt	= "VIN";
				cell							= rng.get_Range("P1", Missing.Value);
				cell.Value2						= txt;
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 20;
				cell.EntireColumn.Font.Size		= 8;

				// ��������� ������
				ws.PageSetup.Orientation	= Excel.XlPageOrientation.xlLandscape;
				ws.PageSetup.LeftMargin		= ws.Application.InchesToPoints(0.393700787401575);
				ws.PageSetup.RightMargin	= ws.Application.InchesToPoints(0.393700787401575);
				ws.PageSetup.TopMargin		= ws.Application.InchesToPoints(0.590551181102362);
				ws.PageSetup.BottomMargin	= ws.Application.InchesToPoints(0.590551181102362);
				ws.PageSetup.HeaderMargin	= ws.Application.InchesToPoints(0.511811023622047);
				ws.PageSetup.FooterMargin	= ws.Application.InchesToPoints(0.511811023622047);
				ws.PageSetup.PrintArea		= "$A:$N";
			}
			catch(Exception E)
			{
				Db.SetException(E);
			}
		}
		protected void DownloadReportPPP(Excel.Worksheet ws, int row)
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
				// ���� �������� ������
				txt							= cardReport.CloseDate;
				cell						= rng.get_Range("A" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ����� ��������
				txt							= cardReport.WarrantNumber;
				cell						= rng.get_Range("B" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ���� 
				txt							= cardReport.WarrantDate;
				cell						= rng.get_Range("C" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// �� 
				//txt							= cardReport.CardTO;
				cell						= rng.get_Range("D" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2 = cardReport.CardTOD;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// �� - ��������
				//txt							= cardReport.CardTOGuaranty;
				cell						= rng.get_Range("E" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2 = cardReport.CardTOGuarantyD;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ����� 
				//txt							= cardReport.CardOil;
				cell						= rng.get_Range("F" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2 = cardReport.CardOilD;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ����� - ��������
				//txt							= cardReport.CardOilGuaranty;
				cell						= rng.get_Range("G" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2 = cardReport.CardOilGuarantyD;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ������ - ��� �����
				//txt							= cardReport.CardWorkNoWash;
				cell						= rng.get_Range("H" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2 = cardReport.CardWorkNoWashD;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// �����
				//txt							= cardReport.CardWash;
				cell						= rng.get_Range("I" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2 = cardReport.CardWashD;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ������ - ��� ����� - ��������
				//txt							= cardReport.CardWorkNoWashGuaranty;
				cell						= rng.get_Range("J" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2 = cardReport.CardWorkNoWashGuarantyD;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ����� - ��������
				//txt							= cardReport.CardWashGuaranty;
				cell						= rng.get_Range("K" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2 = cardReport.CardWashGuarantyD;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ������ 
				//txt							= cardReport.CardDetail;
				cell						= rng.get_Range("L" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2 = cardReport.CardDetailD;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ������ - ��������
				//txt							= cardReport.CardDetailGuaranty;
				cell						= rng.get_Range("M" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2 = cardReport.CardDetailGuarantyD;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ������ 
				txt							= cardReport.AutoModel;
				cell						= rng.get_Range("O" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// VIN 
				txt							= cardReport.AutoVIN;
				cell						= rng.get_Range("P" + rowTxt, Missing.Value);
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
		protected void DownloadReportPPP(Excel.Worksheet ws)
		{
			//Excel.Range rng = ws.UsedRange;
			//int count = rng.Rows.Count;
			int count	= 0;
			int index	= ws.Index;
			if(ExcelCardReport.current_row[index] == 0)
			{
				Excel.Range rng = ws.UsedRange;
				count = rng.Rows.Count;
			}
			else
			{
				count = ExcelCardReport.current_row[index];
				ExcelCardReport.current_row[index]++;
			}
			DownloadReportPPP(ws, count + 1);
		}
		#endregion
		
		#region ������ ��� �����, �� � ��������
		protected void DownloadTitleWork(Excel.Worksheet ws)
		{
			// ��������� ��������� �� ������������ ���� � Excel
			Excel.Range cell;
			Excel.Range rng;
			string		txt;
			try
			{
				rng							= ws.Rows;
				// ���� ��������
				txt	= "��������";
				cell							= rng.get_Range("A1", Missing.Value);
				cell.Value2						= txt;
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 9;
				cell.EntireColumn.Font.Size		= 8;
				// ����� �����-������
				txt	= "�����";
				cell							= rng.get_Range("B1", Missing.Value);
				cell.Value2						= txt;
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 8;
				cell.EntireColumn.Font.Size		= 8;
				// ���� �����-������
				txt	= "����";
				cell							= rng.get_Range("C1", Missing.Value);
				cell.Value2						= txt;
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 9;
				cell.EntireColumn.Font.Size		= 8;
				// ������ ��� �����
				txt	= "������";
				cell								= rng.get_Range("D1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 9;
				cell.EntireColumn.Font.Size			= 8;
				cell.EntireColumn.NumberFormatLocal	= "# ##0,00";
				// ������-��������� ������
				txt	= "�����";
				cell								= rng.get_Range("E1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 9;
				cell.EntireColumn.Font.Size			= 8;
				cell.EntireColumn.NumberFormatLocal	= "# ##0,00";
				// ����� �������
				txt	= "������";
				cell								= rng.get_Range("F1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 9;
				cell.EntireColumn.Font.Size			= 8;
				cell.EntireColumn.NumberFormatLocal	= "# ##0,00";
				// �������� ��/���
				txt	= "���.";
				cell							= rng.get_Range("G1", Missing.Value);
				cell.Value2						= txt;
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignCenter;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 4;
				cell.EntireColumn.Font.Size		= 8;
				// ������ ������� ��� ��� ����������
				// 1
				cell							= rng.get_Range("H1", Missing.Value);
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 15;
				cell.EntireColumn.Font.Size		= 8;
				// 1
				cell							= rng.get_Range("I1", Missing.Value);
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 15;
				cell.EntireColumn.Font.Size		= 8;
				// ������
				txt	= "������";
				cell							= rng.get_Range("J1", Missing.Value);
				cell.Value2						= txt;
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 15;
				cell.EntireColumn.Font.Size		= 8;
				// VIN
				txt	= "VIN";
				cell							= rng.get_Range("K1", Missing.Value);
				cell.Value2						= txt;
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 20;
				cell.EntireColumn.Font.Size		= 8;		
				// ��������������� ����
				txt	= "���.����";
				cell							= rng.get_Range("L1", Missing.Value);
				cell.Value2						= txt;
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 9;
				cell.EntireColumn.Font.Size		= 8;

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
		protected void DownloadReportWork(Excel.Worksheet ws, int row)
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
				// ���� �������� ������
				txt							= cardReport.CloseDate;
				cell						= rng.get_Range("A" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ����� ��������
				txt							= cardReport.WarrantNumber;
				cell						= rng.get_Range("B" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ���� 
				txt							= cardReport.WarrantDate;
				cell						= rng.get_Range("C" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ������ - ��� �����
				//txt							= cardReport.CardWorkNoWash;
				cell						= rng.get_Range("D" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2 = cardReport.CardWorkNoWashD;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// �����
				//txt							= cardReport.CardWash;
				cell						= rng.get_Range("E" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2 = cardReport.CardWashD;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ������ 
				//txt							= cardReport.CardDetail;
				cell						= rng.get_Range("F" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2 = cardReport.CardDetailD;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// �������� ��/���
				if(cardReport.HaveGuaranty())
					txt							= "��";
				else
					txt							= "";
				cell						= rng.get_Range("G" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ������ 
				txt							= cardReport.AutoModel;
				cell						= rng.get_Range("J" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// VIN 
				txt							= cardReport.AutoVIN;
				cell						= rng.get_Range("K" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ��������������� ���� 
				txt							= cardReport.AutoSignNo;
				cell						= rng.get_Range("L" + rowTxt, Missing.Value);
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

		protected void DownloadReportWork(Excel.Worksheet ws)
		{
			//Excel.Range rng = ws.UsedRange;
			//int count = rng.Rows.Count;
			int count	= 0;
			int index	= ws.Index;
			if(ExcelCardReport.current_row[index] == 0)
			{
				Excel.Range rng = ws.UsedRange;
				count = rng.Rows.Count;
			}
			else
			{
				count = ExcelCardReport.current_row[index];
				ExcelCardReport.current_row[index]++;
			}
			DownloadReportWork(ws, count + 1);
		}
		#endregion

		#region ������� ������� �����
		protected void DownloadTitleReport(Excel.Worksheet ws)
		{
			// ��������� ��������� �� ������������ ���� � Excel
			Excel.Range cell;
			Excel.Range rng;
			string		txt;
			try
			{
				rng							= ws.Rows;
				// ���� ��������
				txt	= "��������";
				cell							= rng.get_Range("A1", Missing.Value);
				cell.Value2						= txt;
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 9;
				cell.EntireColumn.Font.Size		= 8;
				// ����� �����-������
				txt	= "�����";
				cell							= rng.get_Range("B1", Missing.Value);
				cell.Value2						= txt;
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 8;
				cell.EntireColumn.Font.Size		= 8;
				// ���� �����-������
				txt	= "����";
				cell							= rng.get_Range("C1", Missing.Value);
				cell.Value2						= txt;
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 9;
				cell.EntireColumn.Font.Size		= 8;
				// ������ ��
				txt	= "��";
				cell								= rng.get_Range("D1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 9;
				cell.EntireColumn.Font.Size			= 8;
				cell.EntireColumn.NumberFormatLocal	= "# ##0,00";
				// �������� � �����-������
				txt	= "��������";
				cell								= rng.get_Range("E1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 9;
				cell.EntireColumn.Font.Size			= 8;
				cell.EntireColumn.NumberFormatLocal	= "# ##0,00";
				// �������������
				txt	= "������.";
				cell								= rng.get_Range("F1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 9;
				cell.EntireColumn.Font.Size			= 8;
				cell.EntireColumn.NumberFormatLocal	= "# ##0,00";
				// ������ ��� �����
				txt	= "������";
				cell								= rng.get_Range("G1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 9;
				cell.EntireColumn.Font.Size			= 8;
				cell.EntireColumn.NumberFormatLocal	= "# ##0,00";
				// ������-��������� ������
				txt	= "�����";
				cell								= rng.get_Range("H1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 9;
				cell.EntireColumn.Font.Size			= 8;
				cell.EntireColumn.NumberFormatLocal	= "# ##0,00";
				// ���� - ������
				txt	= "����";
				cell								= rng.get_Range("I1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 9;
				cell.EntireColumn.Font.Size			= 8;
				cell.EntireColumn.NumberFormatLocal	= "# ##0,00";
				cell.EntireColumn.Font.Bold			= true;
				// ����� �������
				txt	= "������";
				cell								= rng.get_Range("J1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 9;
				cell.EntireColumn.Font.Size			= 8;
				cell.EntireColumn.NumberFormatLocal	= "# ##0,00";
				// ������������� �������
				txt	= "������.";
				cell								= rng.get_Range("K1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 9;
				cell.EntireColumn.Font.Size			= 8;
				cell.EntireColumn.NumberFormatLocal	= "# ##0,00";

				// ������ �� - ��������
				txt	= "�� ���.";
				cell								= rng.get_Range("L1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 9;
				cell.EntireColumn.Font.Size			= 8;
				cell.EntireColumn.NumberFormatLocal	= "# ##0,00";
				// �������� � �����-������ - ��������
				txt	= "����.-���.";
				cell								= rng.get_Range("M1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 9;
				cell.EntireColumn.Font.Size			= 8;
				cell.EntireColumn.NumberFormatLocal	= "# ##0,00";
				// ������������� �������� - ���.
				txt	= "������.";
				cell								= rng.get_Range("N1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 9;
				cell.EntireColumn.Font.Size			= 8;
				cell.EntireColumn.NumberFormatLocal	= "# ##0,00";
				// ������ ��� ����� - ��������
				txt	= "������ ���.";
				cell								= rng.get_Range("O1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 9;
				cell.EntireColumn.Font.Size			= 8;
				cell.EntireColumn.NumberFormatLocal	= "# ##0,00";
				// ������-��������� ������ - ��������
				txt	= "����� ���.";
				cell								= rng.get_Range("P1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 9;
				cell.EntireColumn.Font.Size			= 8;
				cell.EntireColumn.NumberFormatLocal	= "# ##0,00";
				// ����� ������� - ��������
				txt	= "���.-���.";
				cell								= rng.get_Range("Q1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 9;
				cell.EntireColumn.Font.Size			= 8;
				cell.EntireColumn.NumberFormatLocal	= "# ##0,00";
				// ������������� ������� - ��������
				txt	= "������.";
				cell								= rng.get_Range("R1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 9;
				cell.EntireColumn.Font.Size			= 8;
				cell.EntireColumn.NumberFormatLocal	= "# ##0,00";
				// �������� ������/������
				cell							= rng.get_Range("S1", Missing.Value);
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 8;
				cell.EntireColumn.Font.Size		= 8;
				// �������� ���
				cell							= rng.get_Range("T1", Missing.Value);
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 20;
				cell.EntireColumn.Font.Size		= 8;
				// �������������
				cell							= rng.get_Range("U1", Missing.Value);
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 20;
				cell.EntireColumn.Font.Size		= 8;
				// ������
				txt	= "������";
				cell							= rng.get_Range("V1", Missing.Value);
				cell.Value2						= txt;
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 15;
				cell.EntireColumn.Font.Size		= 8;
				// VIN
				txt	= "VIN";
				cell							= rng.get_Range("W1", Missing.Value);
				cell.Value2						= txt;
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 20;
				cell.EntireColumn.Font.Size		= 8;

				// ��������� ������
				ws.PageSetup.Orientation	= Excel.XlPageOrientation.xlLandscape;
				ws.PageSetup.LeftMargin		= ws.Application.InchesToPoints(0.393700787401575);
				ws.PageSetup.RightMargin	= ws.Application.InchesToPoints(0.393700787401575);
				ws.PageSetup.TopMargin		= ws.Application.InchesToPoints(0.590551181102362);
				ws.PageSetup.BottomMargin	= ws.Application.InchesToPoints(0.590551181102362);
				ws.PageSetup.HeaderMargin	= ws.Application.InchesToPoints(0.511811023622047);
				ws.PageSetup.FooterMargin	= ws.Application.InchesToPoints(0.511811023622047);
				ws.PageSetup.PrintArea		= "$A:$X";
			}
			catch(Exception E)
			{
				Db.SetException(E);
			}
		}
		protected void DownloadReportReport(Excel.Worksheet ws, int row)
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
				// ���� �������� ������
				txt							= cardReport.CloseDate;
				cell						= rng.get_Range("A" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ����� ��������
				txt							= cardReport.WarrantNumber;
				cell						= rng.get_Range("B" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ���� 
				txt							= cardReport.WarrantDate;
				cell						= rng.get_Range("C" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// �� 
				//txt							= cardReport.CardTO;
				cell						= rng.get_Range("D" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2 = cardReport.CardTOD;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ����� 
				//txt							= cardReport.CardOil;
				cell						= rng.get_Range("E" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2 = cardReport.CardOilD;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ����� 
				//txt							= cardReport.CardOilInput;
				cell						= rng.get_Range("F" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2 = cardReport.CardOilInputD;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ������ - ��� �����
				//txt							= cardReport.CardWorkNoWash;
				cell						= rng.get_Range("G" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2 = cardReport.CardWorkNoWash;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// �����
				//txt							= cardReport.CardWash;
				cell						= rng.get_Range("H" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2 = cardReport.CardWashD;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ���� �����
				//txt							= cardReport.Work;
				cell						= rng.get_Range("I" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.Value2 = cardReport.WorkD;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ������ 
				//txt							= cardReport.CardDetail;
				cell						= rng.get_Range("J" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2 = cardReport.CardDetailD;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ������������� - ������ 
				//txt							= cardReport.CardDetailInput;
				cell						= rng.get_Range("K" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2 = cardReport.CardDetailInputD;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);

				// �� - ��������
				//txt							= cardReport.CardTOGuaranty;
				cell							= rng.get_Range("L" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2						= cardReport.CardTOGuarantyD;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ����� - ��������
				//txt							= cardReport.CardOilGuaranty;
				cell						= rng.get_Range("M" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2					= cardReport.CardOilGuarantyD;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ������������� ����� - ��������
				//txt							= cardReport.CardOilGuarantyInput;
				cell						= rng.get_Range("N" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2					= cardReport.CardOilGuarantyInputD;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ������ - ��� ����� - ��������
				//txt							= cardReport.CardWorkNoWashGuaranty;
				cell						= rng.get_Range("O" + rowTxt, Missing.Value);
				//cell.Value2				= txt;
				cell.Value2					= cardReport.CardWorkNoWashGuarantyD;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ����� - ��������
				//txt							= cardReport.CardWashGuaranty;
				cell						= rng.get_Range("P" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2					= cardReport.CardWashGuarantyD;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ������ - ��������
				//txt							= cardReport.CardDetailGuaranty;
				cell						= rng.get_Range("Q" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2					= cardReport.CardDetailGuarantyD;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ������������� ������ - ��������
				//txt							= cardReport.CardDetailGuarantyInput;
				cell						= rng.get_Range("R" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2					= cardReport.CardDetailGuarantyInputD;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);

				// �������� ������/������
				txt							= cardReport.IsGuarantyWorkDetailTxt;
				cell						= rng.get_Range("S" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// �������� ���
				if(cardReport.IsGuarantyWorkDetail)
					txt							= cardReport.CardGuarantyType;
				else
					txt							= "";
				cell						= rng.get_Range("T" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// �������������
				txt							= cardReport.CardWorkshopTxt;
				cell						= rng.get_Range("U" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ������ 
				txt							= cardReport.AutoModel;
				cell						= rng.get_Range("V" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// VIN 
				txt							= cardReport.AutoVIN;
				cell						= rng.get_Range("W" + rowTxt, Missing.Value);
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
		protected void DownloadReportReport(Excel.Worksheet ws)
		{
			//Excel.Range rng = ws.UsedRange;
			//int count = rng.Rows.Count;
			int count	= 0;
			int index	= ws.Index;
			if(ExcelCardReport.current_row[index] == 0)
			{
				Excel.Range rng = ws.UsedRange;
				count = rng.Rows.Count;
			}
			else
			{
				count = ExcelCardReport.current_row[index];
				ExcelCardReport.current_row[index]++;
			}
			DownloadReportReport(ws, count + 1);
		}
		#endregion

		#region �������� - ����������
		protected void DownloadTitleGuarantyExtend(Excel.Worksheet ws)
		{
			// ��������� ��������� �� ������������ ���� � Excel
			Excel.Range cell;
			Excel.Range rng;
			string		txt;
			try
			{
				rng							= ws.Rows;
				// ���� ��������
				txt	= "��������";
				cell							= rng.get_Range("A1", Missing.Value);
				cell.Value2						= txt;
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 9;
				cell.EntireColumn.Font.Size		= 8;
				// ����� �����-������
				txt	= "�����";
				cell							= rng.get_Range("B1", Missing.Value);
				cell.Value2						= txt;
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 8;
				cell.EntireColumn.Font.Size		= 8;
				// ���� �����-������
				txt	= "����";
				cell							= rng.get_Range("C1", Missing.Value);
				cell.Value2						= txt;
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 9;
				cell.EntireColumn.Font.Size		= 8;
				// ������ ��
				txt	= "��";
				cell								= rng.get_Range("D1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 9;
				cell.EntireColumn.Font.Size			= 8;
				cell.EntireColumn.NumberFormatLocal	= "# ##0,00";
				// �������� � �����-������
				txt	= "��������";
				cell								= rng.get_Range("E1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 9;
				cell.EntireColumn.Font.Size			= 8;
				cell.EntireColumn.NumberFormatLocal	= "# ##0,00";
				// ������ ��� �����
				txt	= "������";
				cell								= rng.get_Range("F1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 9;
				cell.EntireColumn.Font.Size			= 8;
				cell.EntireColumn.NumberFormatLocal	= "# ##0,00";
				// ������-��������� ������
				txt	= "�����";
				cell								= rng.get_Range("G1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 9;
				cell.EntireColumn.Font.Size			= 8;
				cell.EntireColumn.NumberFormatLocal	= "# ##0,00";
				// ����� �������
				txt	= "������";
				cell								= rng.get_Range("H1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 9;
				cell.EntireColumn.Font.Size			= 8;
				cell.EntireColumn.NumberFormatLocal	= "# ##0,00";
				// �������� ���
				txt	= "���.";
				cell							= rng.get_Range("I1", Missing.Value);
				cell.Value2						= txt;
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 22;
				cell.EntireColumn.Font.Size		= 8;
				// �������������
				cell							= rng.get_Range("J1", Missing.Value);
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 10;
				cell.EntireColumn.Font.Size		= 8;
				// ������ ������� ��� ��� ����������
				// 1
				cell							= rng.get_Range("K1", Missing.Value);
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 5;
				cell.EntireColumn.Font.Size		= 8;
				// ������
				txt	= "������";
				cell							= rng.get_Range("L1", Missing.Value);
				cell.Value2						= txt;
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 15;
				cell.EntireColumn.Font.Size		= 8;
				// VIN
				txt	= "VIN";
				cell							= rng.get_Range("M1", Missing.Value);
				cell.Value2						= txt;
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 20;
				cell.EntireColumn.Font.Size		= 8;		
				// ��������������� ����
				txt	= "���.����";
				cell							= rng.get_Range("N1", Missing.Value);
				cell.Value2						= txt;
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 9;
				cell.EntireColumn.Font.Size		= 8;

				// ��������� ������
				ws.PageSetup.Orientation	= Excel.XlPageOrientation.xlLandscape;
				ws.PageSetup.LeftMargin		= ws.Application.InchesToPoints(0.393700787401575);
				ws.PageSetup.RightMargin	= ws.Application.InchesToPoints(0.393700787401575);
				ws.PageSetup.TopMargin		= ws.Application.InchesToPoints(0.590551181102362);
				ws.PageSetup.BottomMargin	= ws.Application.InchesToPoints(0.590551181102362);
				ws.PageSetup.HeaderMargin	= ws.Application.InchesToPoints(0.511811023622047);
				ws.PageSetup.FooterMargin	= ws.Application.InchesToPoints(0.511811023622047);
				ws.PageSetup.PrintArea		= "$A:$K";
			}
			catch(Exception E)
			{
				Db.SetException(E);
			}
		}
		protected void DownloadReportGuarantyExtend(Excel.Worksheet ws, int row)
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
				// ���� �������� ������
				txt							= cardReport.CloseDate;
				cell						= rng.get_Range("A" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ����� ��������
				txt							= cardReport.WarrantNumber;
				cell						= rng.get_Range("B" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ���� 
				txt							= cardReport.WarrantDate;
				cell						= rng.get_Range("C" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// �� 
				//txt							= cardReport.CardTOGuaranty;
				cell						= rng.get_Range("D" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2					= cardReport.CardTOGuarantyD;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ����� 
				//txt							= cardReport.CardOilGuaranty;
				cell						= rng.get_Range("E" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2					= cardReport.CardOilGuarantyD;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ������ - ��� �����
				//txt							= cardReport.CardWorkNoWashGuaranty;
				cell						= rng.get_Range("F" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2 = cardReport.CardWorkNoWashGuarantyD;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// �����
				//txt							= cardReport.CardWashGuaranty;
				cell						= rng.get_Range("G" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2 = cardReport.CardWashGuarantyD;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ������ 
				//txt							= cardReport.CardDetailGuaranty;
				cell						= rng.get_Range("H" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2 = cardReport.CardDetailGuarantyD;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ��� ���������
				txt							= cardReport.CardGuarantyType;
				cell						= rng.get_Range("I" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// �������������
				txt							= cardReport.CardWorkshopTxt;
				cell						= rng.get_Range("J" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ������ 
				txt							= cardReport.AutoModel;
				cell						= rng.get_Range("L" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// VIN 
				txt							= cardReport.AutoVIN;
				cell						= rng.get_Range("M" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ��������������� ���� 
				txt							= cardReport.AutoSignNo;
				cell						= rng.get_Range("N" + rowTxt, Missing.Value);
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

		protected bool DownloadReportGuarantyExtend(Excel.Worksheet ws, int row, int position)
		{
			// ��������� ������ �� ������������ ���� � Excel
			Excel.Range cell;
			Excel.Range rng;
			string		txt;
			string		rowTxt;
				
			rowTxt		= row.ToString();

			if(cardReport.GuarantyArray.Count <= position) return false;
			DbCardReport.Guaranty guaranty = (DbCardReport.Guaranty)cardReport.GuarantyArray[position];
			try
			{
				rng							= ws.Rows;
				// ���� �������� ������
				txt							= cardReport.CloseDate;
				cell						= rng.get_Range("A" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ����� ��������
				txt							= cardReport.WarrantNumber;
				cell						= rng.get_Range("B" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ���� 
				txt							= cardReport.WarrantDate;
				cell						= rng.get_Range("C" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ��
				if(guaranty.summ_to_guaranty != 0.0F)
					//txt							= guaranty.summ_to_guaranty.ToString();//cardReport.CardTOGuaranty;
					txt							= guaranty.summ_to_guaranty.ToString();//cardReport.CardTOGuaranty;
				else
					txt = "";
				cell						= rng.get_Range("D" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2 = guaranty.summ_to_guaranty;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ����� 
				if(guaranty.summ_oil_guaranty_input != 0.0F)
					txt							= guaranty.summ_oil_guaranty_input.ToString();//cardReport.CardOilGuaranty;
				else
					txt = "";
				cell						= rng.get_Range("E" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2 = guaranty.summ_oil_guaranty_input;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ������ - ��� �����
				if(guaranty.summ_work_guaranty - guaranty.summ_wash_guaranty != 0.0F)
					txt							= (guaranty.summ_work_guaranty - guaranty.summ_wash_guaranty).ToString();//cardReport.CardWorkNoWashGuaranty;
				else
					txt = "";
				cell						= rng.get_Range("F" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2 = (guaranty.summ_work_guaranty - guaranty.summ_wash_guaranty);
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// �����
				if(guaranty.summ_wash_guaranty != 0.0F)
					txt							= guaranty.summ_wash_guaranty.ToString();//cardReport.CardWashGuaranty;
				else
					txt = "";
				cell						= rng.get_Range("G" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2 = guaranty.summ_wash_guaranty;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ������
				if(guaranty.is_detail_guaranty == true)
					txt							= guaranty.summ_detail_guaranty_input.ToString();// cardReport.CardDetailGuaranty;
				else
					txt = "";
				cell						= rng.get_Range("H" + rowTxt, Missing.Value);
				//cell.Value2					= txt;
				cell.Value2 = guaranty.summ_detail_guaranty_input;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ��� ���������
				txt							= guaranty.guaranty_name;//cardReport.CardGuarantyType;
				cell						= rng.get_Range("I" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// �������������
				txt							= cardReport.CardWorkshopTxt;
				cell						= rng.get_Range("J" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ������ 
				txt							= cardReport.AutoModel;
				cell						= rng.get_Range("L" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// VIN 
				txt							= cardReport.AutoVIN;
				cell						= rng.get_Range("M" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ��������������� ���� 
				txt							= cardReport.AutoSignNo;
				cell						= rng.get_Range("N" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
			}
			catch(Exception E)
			{
				Db.SetException(E);
				return true;
			}
			return true;
		}

		protected void DownloadReportGuarantyExtend(Excel.Worksheet ws)
		{
			//Excel.Range rng = ws.UsedRange;
			//int count = rng.Rows.Count;
			int count	= 0;
			int index	= ws.Index;
			if(ExcelCardReport.current_row[index] == 0)
			{
				Excel.Range rng = ws.UsedRange;
				count = rng.Rows.Count;
			}
			else
			{
				count = ExcelCardReport.current_row[index];
				//ExcelCardReport.current_row[index]++;
			}
			//DownloadReportGuarantyExtend(ws, count + 1);
			int i = 0;
			//count++;
			while(DownloadReportGuarantyExtend(ws, count + 1, i) != false)
			{
				i++;
				count++;
				//ExcelCardReport.current_row[index]++;
			}
			//ExcelCardReport.current_row[index] = ExcelCardReport.current_row[index] + i;
		}
		#endregion

		protected void DownloadReport()
		{
			// ���������� ����������� ���� �������� ��� ��������
			switch(cardReport.CardWorkshop)
			{
				case 1:			// ������
					if(cardReport.HavePPP() == true)
						this.DownloadReportPPP((Excel.Worksheet)workbook.Worksheets[4]);
					else
					{
						if(cardReport.HaveTO() == true)
						{
							if(cardReport.Is_Cashless() == false)
							{
								this.DownloadReportTO((Excel.Worksheet)workbook.Worksheets[1]);
							}
							else
							{
								this.DownloadReportTO((Excel.Worksheet)workbook.Worksheets[2]);
							}
						}
					}
					break;
				case 2:			// ������
					this.DownloadReportWork((Excel.Worksheet)workbook.Worksheets[6]); // 5 �� 6 ����������� 2018
					break;
				case 3:			// �������
					this.DownloadReportWork((Excel.Worksheet)workbook.Worksheets[7+1]);
					break;
				case 5:			// �����
                    this.DownloadReportWork((Excel.Worksheet)workbook.Worksheets[8 + 1]);
					break;
				case 7:			// ������
                    this.DownloadReportWork((Excel.Worksheet)workbook.Worksheets[6]);
					break;
				case 11:		// ������ ������
                    this.DownloadReportWork((Excel.Worksheet)workbook.Worksheets[6]);
					break;
				case 12:		// ������ ����������
                    this.DownloadReportWork((Excel.Worksheet)workbook.Worksheets[6]);
					break;
				case 9:			// ���� g��� ������� ����������� 2018
					//this.DownloadReportWork((Worksheet)workbook.Worksheets[7]);
                    //this.DownloadReportTO((Excel.Worksheet)workbook.Worksheets[7]);
                    this.DownloadReportWork((Excel.Worksheet)workbook.Worksheets[5]); // �������� ����������� 2018
					break;
				case 13:			// ���� �������
					//this.DownloadReportWork((Worksheet)workbook.Worksheets[7]);
                    this.DownloadReportTO((Excel.Worksheet)workbook.Worksheets[7]);
					break;
                case 14:			// �����-��
                    //this.DownloadReportWork((Worksheet)workbook.Worksheets[7]);
                    this.DownloadReportTO((Excel.Worksheet)workbook.Worksheets[11+1]);
                    break;
				case 16:
					this.DownloadReportWork((Excel.Worksheet)workbook.Worksheets[12+1]); // �������� ����������� 2018
					break;
				default:
					break;
			}
			// ��� ������ ������������� ��������� ��������
			if(cardReport.HaveGuaranty() == true)
                this.DownloadReportGuaranty((Excel.Worksheet)workbook.Worksheets[3]);

			// ����� ������ �������
            this.DownloadReportReport((Excel.Worksheet)workbook.Worksheets[9 + 1]);

			// ��� ������ ������������� ��������� ��������
			if(cardReport.HaveGuaranty() == true)
                this.DownloadReportGuarantyExtend((Excel.Worksheet)workbook.Worksheets[10 + 1]);
		}
	}
}
