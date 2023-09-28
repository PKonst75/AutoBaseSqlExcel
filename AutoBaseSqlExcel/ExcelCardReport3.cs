using System;
using System.Collections;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for ExcelCardReport3.
	/// </summary>
	public class ExcelCardReport3
	{
		private Excel.Workbook		workbook;
		private DbCardReport1		cardReport1;
		private string				file;
		private static int[]		current_row = new int[2];	// ��������� ����������� ������ ������ ��������
		private static bool			download_works;

		public static void DownloadList(ArrayList array, bool works)
		{
			Excel.Workbook		wb;
			wb					= null;
			string				fileName;

			download_works		= works;

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
					ExcelCardReport3 excelCardReport3	= new ExcelCardReport3(card, wb, fileName);
					wb		= excelCardReport3.StartDownLoad();
				}
			}

			ShowExcel(wb);
		}

		public ExcelCardReport3(DbCard cardSrc, Excel.Workbook workbookSrc, string fileName)
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
						((Excel.Worksheet)wb.Worksheets[1]).Name	= "�������";
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
				// ��������������� �������
				cell							= rng.get_Range("A1", Missing.Value);
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Columns.ColumnWidth		= 5;
				cell.EntireColumn.Font.Size		= 8;
				cell.EntireColumn.Font.Bold		= true;
				// ������ / ������
				txt	= "������ / ������";
				cell							= rng.get_Range("B1", Missing.Value);
				cell.Value2						= txt;
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 50;
				cell.EntireColumn.Font.Size		= 8;
				cell.EntireColumn.WrapText		= true;
				// ���� �������
				txt	= "��";
				cell							= rng.get_Range("C1", Missing.Value);
				cell.Value2						= txt;
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignCenter;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 10;
				cell.EntireColumn.Font.Size		= 8;
				cell.EntireColumn.Font.Bold		= true;
				// ����������� ����
				txt	= "���-����";
				cell							= rng.get_Range("D1", Missing.Value);
				cell.Value2						= txt;
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignCenter;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 10;
				cell.EntireColumn.Font.Size		= 8;
				cell.EntireColumn.Font.Bold		= true;
				// �������� ����������� ����
				txt	= "��������";
				cell							= rng.get_Range("E1", Missing.Value);
				cell.Value2						= txt;
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignCenter;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 10;
				cell.EntireColumn.Font.Size		= 8;
				cell.EntireColumn.Font.Bold		= true;
				// ������ ���� ��������
				txt	= "������";
				cell							= rng.get_Range("F1", Missing.Value);
				cell.Value2						= txt;
				cell.HorizontalAlignment		= Excel.XlHAlign.xlHAlignCenter;
				cell.Font.Bold					= true;
				cell.Columns.ColumnWidth		= 10;
				cell.EntireColumn.Font.Size		= 8;
				cell.EntireColumn.Font.Bold		= true;
				// �������� ���� ��������
				txt	= "��� ��������";
				cell								= rng.get_Range("G1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 25;
				cell.EntireColumn.Font.Size			= 8;
				// ��������/����������
				txt	= "�������� / ����������";
				cell								= rng.get_Range("H1", Missing.Value);
				cell.Value2							= txt;
				cell.HorizontalAlignment			= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold						= true;
				cell.Columns.ColumnWidth			= 35;
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
			string letter;
			string guaranty_txt;
				
			rowTxt		= row.ToString();
			try
			{
				rng							= ws.Rows;
				// �����-�����, ����������
				txt							= "�" + cardReport1.card.WarrantNumber.ToString() + "(" + cardReport1.card.Number.ToString() + ") �� " + cardReport1.card.WarrantDateTxt + " " + cardReport1.card.Auto.ModelTxt + " " + cardReport1.card.Auto.Vin + " ���.����.: " + cardReport1.card.Auto.SignNo + " ������: " + cardReport1.card.RunTxt;
				cell						= rng.get_Range("A" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				// ������������� ���� �������
				foreach(object o in cardReport1.cardDetails)
				{
					DbCardDetail dtl	= (DbCardDetail)o;
					if(dtl.Guaranty == true)
					{
						row++;
						rowTxt						= row.ToString();
						txt							= dtl.DetailNameTxt;
						cell						= rng.get_Range("B" + rowTxt, Missing.Value);
						cell.Value2					= txt;
						cell.Interior.ColorIndex	= 6;
						// ����� ����� ������ �� ���� ��������
						DtCardDetail dtl1 = DbSqlCardDetail.Find(cardReport1.card, dtl.Number);
						switch((long)dtl1.GetData("��������_���_��������_������"))
						{
							case 3:
								letter	= "C";
								txt		= dtl.InputSumm.ToString();
								break;
							case 16:
								letter	= "D";
								txt		= dtl.SummWhole.ToString();
								break;
							case 17:
								letter	= "E";
								//txt		= dtl.SummWhole.ToString();
								txt		= dtl.InputSumm.ToString();
								break;
							default:
								letter	= "F";
								txt		= dtl.InputSumm.ToString();
								break;
						}
						guaranty_txt = (string)dtl1.GetData("��������_���_������������_��������_������");

						//txt							= dtl.InputSumm.ToString();
						cell						= rng.get_Range(letter + rowTxt, Missing.Value);
						cell.Value2					= txt;

						txt							= guaranty_txt;
						cell						= rng.get_Range("G" + rowTxt, Missing.Value);
						cell.Value2					= txt;

						txt							= dtl1.GetData("���������_������������_��������_������") + " / " + dtl1.GetData("�����_��������_������");
						cell						= rng.get_Range("H" + rowTxt, Missing.Value);
						cell.Value2					= txt;
					}
				}
				// ������������� ���� �����
				if(download_works == true)
				{
					foreach(object o in cardReport1.cardWorks)
					{
						DbCardWork	wrk = (DbCardWork)o;
						if(wrk.Guaranty == true)
						{
							row++;
							rowTxt						= row.ToString();
							txt							= wrk.WorkName;
							cell						= rng.get_Range("B" + rowTxt, Missing.Value);
							cell.Value2					= txt;
							cell.Interior.ColorIndex	= 4;
							// ����� ����� ������ �� ���� ��������
							DtCardWork wrk1 = DbSqlCardWork.Find(cardReport1.card, wrk.Number);
							switch((long)wrk1.GetData("��������_���_��������_������"))
							{
								case 3:
									letter = "C";
									break;
								case 16:
									letter = "D";
									break;
								case 17:
									letter = "E";
									break;
								default:
									letter = "F";
									break;
							}
							guaranty_txt = (string)wrk1.GetData("��������_���_������������_��������_������");

							txt							= wrk.SummFull.ToString();
							cell						= rng.get_Range(letter + rowTxt, Missing.Value);
							cell.Value2					= txt;

							txt							= guaranty_txt;
							cell						= rng.get_Range("G" + rowTxt, Missing.Value);
							cell.Value2					= txt;
						}
					}
				}
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
		//	if(ExcelCardReport3.current_row[index] == 0)
		//	{
				Excel.Range rng = ws.UsedRange;
				count = rng.Rows.Count;
		//	}
		//	else
		//	{
		//		count = ExcelCardReport3.current_row[index];
		//		ExcelCardReport3.current_row[index]++;
		//	}
			DownloadReportTO(ws, count + 1);
		}
		#endregion
		

		protected void DownloadReport()
		{
			// ���������� ����������� ���� �������� ��� ��������
			if(download_works == true)
			{
				if(cardReport1.g_detail_is || cardReport1.g_liquid_is || cardReport1.g_work_is || cardReport1.g_norm_is || cardReport1.g_ppp_is || cardReport1.g_to_is || cardReport1.g_wash_is)
                    DownloadReportTO((Excel.Worksheet)workbook.Worksheets[1]);
			}
			else
			{
				if(cardReport1.g_detail_is || cardReport1.g_liquid_is)
                    DownloadReportTO((Excel.Worksheet)workbook.Worksheets[1]);
			}
		}

		protected void DownloadReport1()
		{
			// ���������� ����������� ���� �������� ��� ��������
			if(cardReport1.to_is)
                DownloadReportTO((Excel.Worksheet)workbook.Worksheets[1]);
		}
	}
}
