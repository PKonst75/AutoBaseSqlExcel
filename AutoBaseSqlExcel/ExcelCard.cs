using System;
using System.Collections;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// �������� �������� �����-������ � Excel.
	/// </summary>
	public class ExcelCard
	{
		private DbCard				card;
		private ArrayList			cardWorks;
		private ArrayList			cardDetails;
		private ArrayList			cardRecomend;
		private Excel.Workbook		workbook;
		private float				cardSumm;

		public static void DownloadList(ListView list)
		{
			Excel.Workbook		wb;
			wb					= null;
			
			foreach(ListViewItem item in list.SelectedItems)
			{
				DbCard card	= (DbCard)item.Tag;
				if(card != null)
				{
					ExcelCard excelCard	= new ExcelCard(card, wb);
					wb					= excelCard.StartDownLoad();
				}
			}

			ShowExcel(wb);
		}

		public ExcelCard(DbCard cardSrc, Excel.Workbook workbookSrc)
		{
			card		= cardSrc;
			workbook	= workbookSrc;
			// �������� ������ ����� ������ ��������
			cardWorks = new ArrayList();
			DbCardWork.FillList(cardWorks, card);
			// �������� ������ ������� ������ ��������
			cardDetails = new ArrayList();
			DbCardDetail.FillList(cardDetails, card);
			// �������� ������ ������������ ������ ��������
			cardRecomend = new ArrayList();
			DbCardRecomend.FillArray(cardRecomend, card);
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
			Excel.Worksheet ws;

			try
			{
				if(workbook == null)
				{
					app			= new Excel.Application();				// ����� ��������� ���������� Excel
					wb			= app.Workbooks.Add(Missing.Value);		// ����� ����� Excel
					workbook	= wb;
					// ������� ������������ �����
					while(wb.Worksheets.Count != 1)
						((Excel.Worksheet)wb.Worksheets[1]).Delete();
					DownloadFormatBrifTitle(wb);
					((Excel.Worksheet)wb.Worksheets[1]).Name	= "������";
				}
				else
				{
					wb			= workbook;
					app			= wb.Application;
				}
				
				// ������ ��������� ����� ����
				ws = (Excel.Worksheet)wb.Worksheets.Add(Missing.Value, wb.Worksheets[wb.Worksheets.Count], 1, Missing.Value);
				
				DownloadFormat(ws);
				DownloadTitle(ws);
				int row = DownloadWorks(ws, 10);
				DownloadDetails(ws, row + 2);
				DownloadBrifTitle(wb);
				ws.Name			= card.WarrantNumberTxt;
			}
			catch(Exception E)
			{
				Db.SetException(E);
			}
			return wb;
		}

		protected void DownloadTitle(Excel.Worksheet ws)
		{
			// ��������� ��������� �� ������������ ���� � Excel
			Excel.Range cell;
			Excel.Range rng;
			string		txt;
			try
			{
				rng							= ws.Rows;
				// ����� ��������/������
				if(card.WarrantNumber <= 0)
					txt	= "�������� " + card.NumberTxt;
				else
					txt	= "�����-����� � " + card.WarrantNumberTxt;
				cell						= rng.get_Range("A1", Missing.Value);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold				= true;
				// ���� � ����� �������� � ��������
				if(card.WarrantNumber <= 0)
					txt	= "";
				else
				{
					txt	= "������ : " + card.WarrantOpenTxt;
					if (card.ActionCode == (int)DbCardAction.ActionCodes.Close)
					{
						txt	+= " ������ : " + card.WarrantCloseTxt;
					}
				}
				cell						= rng.get_Range("H1", Missing.Value);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignRight;
				// ����������
				// �����
				cell						= rng.get_Range("A3", "C8");
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ����������
				txt							= "����������";
				cell						= rng.get_Range("A3", Missing.Value);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignLeft;
				cell.IndentLevel			= 1;
				cell.Font.Bold				= true;
				// ������
				txt							= "������ :";
				cell						= rng.get_Range("A4", Missing.Value);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignLeft;
				cell.IndentLevel			= 1;
				txt							= card.Auto.AutoModel.Model;
				cell						= rng.get_Range("C4", Missing.Value);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignLeft;
				// VIN
				txt							= "VIN :";
				cell						= rng.get_Range("A5", Missing.Value);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignLeft;
				cell.IndentLevel			= 1;
				txt							= card.Auto.Vin;
				cell						= rng.get_Range("C5", Missing.Value);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignLeft;
				// ����� �
				txt							= "����� � :";
				cell						= rng.get_Range("A6", Missing.Value);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignLeft;
				cell.IndentLevel			= 1;
				txt							= card.Auto.BodyNo;
				cell						= rng.get_Range("C6", Missing.Value);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignLeft;
				// ��������� �
				txt							= "��������� � :";
				cell						= rng.get_Range("A7", Missing.Value);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignLeft;
				cell.IndentLevel			= 1;
				txt							= card.Auto.EngineNo;
				cell						= rng.get_Range("C7", Missing.Value);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignLeft;
				// ��������������� ���� � ������
				txt							= "���. ����: " + card.Auto.SignNo + "   " + "������: " + card.RunTxt;
				cell						= rng.get_Range("A8", Missing.Value);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignLeft;
				cell.IndentLevel			= 1;

				// ������
				// �����
				cell						= rng.get_Range("E3", "H8");
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ������
				txt							= "������";
				cell						= rng.get_Range("E3", Missing.Value);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignLeft;
				cell.IndentLevel			= 1;
				cell.Font.Bold				= true;
				// ����������
				txt	= card.Partner.Title;
				if(card.Partner.Juridical)
					txt	+= "\n" + card.Partner.AddressJuridical;
				else
					txt	+= "\n" + card.Partner.AddressRegistration;
				if(card.Partner.Juridical)
				{
					if(card.Partner.ContactPhone.Length != 0)
					{
						txt	+= "; ���. " + card.Partner.ContactPhone;
					}
				}
				else
				{
					if(card.Partner.Phone.Length != 0)
					{
						txt	+= "; ���. " + card.Partner.Phone;
					}
				}
				cell						= rng.get_Range("E4", "H8");
				cell.MergeCells				= true;
				cell.Value2					= txt;
				cell.IndentLevel			= 1;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignLeft;
				cell.VerticalAlignment		= Excel.XlVAlign.xlVAlignTop;
			}
			catch(Exception E)
			{
				Db.SetException(E);
			}
		}

		protected int DownloadWorks(Excel.Worksheet ws, int row)
		{
			// ��������� ��������� �� ������������ ���� � Excel
			Excel.Range cell;
			Excel.Range rng;
			string		txt;
			string		rowTxt;
			int			rowStart;
				
			try
			{
				rng							= ws.Rows;
				// ������������ ������
				txt							= "������";
				cell						= rng.get_Range("B" + row.ToString(), "B" + row.ToString());
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold				= true;
				// ��������� ������� �����
				row++;
				rowTxt						= row.ToString();
				rowStart					= row;
				// �����
				txt							= "�";
				cell						= rng.get_Range("A" + rowTxt, "A" + rowTxt);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignCenter;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ������������ �����
				txt							= "������������ �����";
				cell						= rng.get_Range("B" + rowTxt, "D" + rowTxt);
				cell.MergeCells				= true;
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignCenter;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ����������
				txt							= "�-��";
				cell						= rng.get_Range("E" + rowTxt, "E" + rowTxt);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignCenter;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ����
				txt							= "�.";
				cell						= rng.get_Range("F" + rowTxt, "F" + rowTxt);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignCenter;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ����
				txt							= "����";
				cell						= rng.get_Range("G" + rowTxt, "G" + rowTxt);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignCenter;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// �����
				txt							= "�����";
				cell						= rng.get_Range("H" + rowTxt, "H" + rowTxt);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignCenter;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);

				row++;

				// �������� ���� �����
				int	num			= 1;
				float summ		= 0.0f;
				float summOil	= 0.0f;

				// ������� ������ ������������ ����������
				foreach(object o in cardWorks)
				{
					DbCardWork cardWork = (DbCardWork)o;
					if(cardWork != null)
					{
						if(cardWork.Oil == true)
						{
							summOil += cardWork.Summ;
						}
					}
				}
				foreach(object o in cardDetails)
				{
					DbCardDetail cardDetail = (DbCardDetail)o;
					if(cardDetail != null)
					{
						if(cardDetail.Oil == true)
						{
							summOil += cardDetail.Summ;
						}
					}
				}
				if(summOil != 0.0f)
				{
					// ��������� ������������ ����������
					rowTxt						= row.ToString();
					// �����
					txt							= num.ToString();
					cell						= rng.get_Range("A" + rowTxt, "A" + rowTxt);
					cell.Value2					= txt;
					cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignCenter;
					cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
					// ������������ �����
					txt							= "������������ ����������";
					cell						= rng.get_Range("B" + rowTxt, "D" + rowTxt);
					cell.MergeCells				= true;
					cell.Value2					= txt;
					cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignLeft;
					cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
					// ����������
					txt							= "-";
					cell						= rng.get_Range("E" + rowTxt, "E" + rowTxt);
					cell.Value2					= txt;
					cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignCenter;
					cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
					// ����
					txt							= "-";
					cell						= rng.get_Range("F" + rowTxt, "F" + rowTxt);
					cell.Value2					= txt;
					cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignCenter;
					cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
					// ����
					txt							= Db.CachToTxt(summOil);
					cell						= rng.get_Range("G" + rowTxt, "G" + rowTxt);
					cell.Value2					= txt;
					cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignCenter;
					cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
					// �����
					txt							= Db.CachToTxt(summOil);
					cell						= rng.get_Range("H" + rowTxt, "H" + rowTxt);
					cell.Value2					= txt;
					cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignCenter;
					cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);

					num++;
					row++;
					summ += summOil;
				}

				// ��� ����������� ������
				foreach(object o in cardWorks)
				{
					DbCardWork cardWork = (DbCardWork)o;
					if(cardWork != null && cardWork.Oil == false)
					{
						summ += DownloadWork(ws, row, num, cardWork);
						num++;
						row++;
					}
				}
				// ��������� �������� ����� �� �����-������
				cardSumm	+= summ;
				// �������� ����� �� �������
				rowTxt						= row.ToString();
				// ����
				txt							= "�����:";
				cell						= rng.get_Range("A" + rowTxt, "G" + rowTxt);
				cell.MergeCells				= true;
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignRight;
				cell.IndentLevel			= 1;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				cell.Font.Bold				= true;
				// �����
				txt							= Db.CachToTxt(summ);
				cell						= rng.get_Range("H" + rowTxt, "H" + rowTxt);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignCenter;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);

				// �������� �����
				cell						= rng.get_Range("A" + rowStart.ToString(), "H" + rowTxt);
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
			}
			catch(Exception E)
			{
				Db.SetException(E);
				return row;
			}
			return row;
		}

		protected float DownloadWork(Excel.Worksheet ws, int row, int num, DbCardWork work)
		{
			// ��������� ��������� �� ������������ ���� � Excel
			Excel.Range cell;
			Excel.Range rng;
			string		txt;
			string		rowTxt;
			float		ret = 0.0f;
				
			try
			{
				rng							= ws.Rows;
				// ��������� ������� �����
				rowTxt						= row.ToString();
				// �����
				txt							= num.ToString();
				cell						= rng.get_Range("A" + rowTxt, "A" + rowTxt);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignCenter;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ������������ �����
				txt							= work.Name;
				cell						= rng.get_Range("B" + rowTxt, "D" + rowTxt);
				cell.MergeCells				= true;
				cell.Value2					= txt;
				cell.WrapText				= true;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignLeft;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ����������
				txt							= work.QuontityTxt;
				cell						= rng.get_Range("E" + rowTxt, "E" + rowTxt);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignCenter;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				if(work.Guaranty == false)
				{
					// ����
					txt							= work.ValTxt;
					cell						= rng.get_Range("F" + rowTxt, "F" + rowTxt);
					cell.Value2					= txt;
					cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignCenter;
					cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
					// ����
					txt							= work.PriceTxt;
					cell						= rng.get_Range("G" + rowTxt, "G" + rowTxt);
					cell.Value2					= txt;
					cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignCenter;
					cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
					// �����
					txt							= work.SummTxt;
					cell						= rng.get_Range("H" + rowTxt, "H" + rowTxt);
					cell.Value2					= txt;
					cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignCenter;
					cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);	
				}
				else
				{
					// ����
					txt							= "��������";
					cell						= rng.get_Range("F" + rowTxt, "H" + rowTxt);
					cell.MergeCells				= true;
					cell.Value2					= txt;
					cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignCenter;
					cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				}
				// ������� �����
				ret			= work.Summ;
			}
			catch(Exception E)
			{
				Db.SetException(E);
				return ret;
			}
			return ret;
		}

		protected int DownloadDetails(Excel.Worksheet ws, int row)
		{
			// ��������� ��������� �� ������������ ���� � Excel
			Excel.Range cell;
			Excel.Range rng;
			string		txt;
			string		rowTxt;
			int			rowStart;
				
			try
			{
				rng							= ws.Rows;
				// ������������ ������
				txt							= "������";
				cell						= rng.get_Range("B" + row.ToString(), "B" + row.ToString());
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold				= true;
				// ��������� ������� �����
				row++;
				rowTxt						= row.ToString();
				rowStart					= row;
				// �����
				txt							= "�";
				cell						= rng.get_Range("A" + rowTxt, "A" + rowTxt);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignCenter;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ��� ������
				txt							= "���";
				cell						= rng.get_Range("B" + rowTxt, "B" + rowTxt);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignCenter;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ������������
				txt							= "������������";
				cell						= rng.get_Range("C" + rowTxt, "E" + rowTxt);
				cell.MergeCells				= true;
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignCenter;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ����������
				txt							= "�-��";
				cell						= rng.get_Range("F" + rowTxt, "F" + rowTxt);
				cell.MergeCells				= true;
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignCenter;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ����
				txt							= "����";
				cell						= rng.get_Range("G" + rowTxt, "G" + rowTxt);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignCenter;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// �����
				txt							= "�����";
				cell						= rng.get_Range("H" + rowTxt, "H" + rowTxt);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignCenter;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);

				row++;
				// �������� ���� �����
				int	num			= 1;
				float summ		= 0.0f;

				// ��� ����������� ������
				foreach(object o in cardDetails)
				{
					DbCardDetail cardDetail = (DbCardDetail)o;
					if(cardDetail!= null && cardDetail.Oil == false)
					{
						summ += DownloadDetail(ws, row, num, cardDetail);
						num++;
						row++;
					}
				}
				// �������� ����� �� �������
				cardSumm					+= summ;
				rowTxt						= row.ToString();
				// ����
				txt							= "�����:";
				cell						= rng.get_Range("A" + rowTxt, "G" + rowTxt);
				cell.MergeCells				= true;
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignRight;
				cell.IndentLevel			= 1;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				cell.Font.Bold				= true;
				// �����
				txt							= Db.CachToTxt(summ);
				cell						= rng.get_Range("H" + rowTxt, "H" + rowTxt);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignCenter;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);

				// �������� �����
				cell						= rng.get_Range("A" + rowStart.ToString(), "H" + rowTxt);
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
			}
			catch(Exception E)
			{
				Db.SetException(E);
				return row;
			}
			return row;
		}

		protected float DownloadDetail(Excel.Worksheet ws, int row, int num, DbCardDetail detail)
		{
			// ��������� ��������� �� ������������ ���� � Excel
			Excel.Range cell;
			Excel.Range rng;
			string		txt;
			string		rowTxt;
			float		ret = 0.0f;
				
			try
			{
				rng							= ws.Rows;
				// ��������� ������� �����
				rowTxt						= row.ToString();
				// �����
				txt							= num.ToString();
				cell						= rng.get_Range("A" + rowTxt, "A" + rowTxt);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignCenter;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ��� ������
				txt							= detail.CodeDetailTxt;
				cell						= rng.get_Range("B" + rowTxt, "B" + rowTxt);
				cell.Value2					= txt;
				cell.ShrinkToFit			= true;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignCenter;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ������������
				txt							= detail.DetailNameTxt;
				cell						= rng.get_Range("C" + rowTxt, "E" + rowTxt);
				cell.WrapText				= true;
				cell.MergeCells				= true;
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignLeft;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				// ������������
				txt							= detail.QuontityTxt;
				cell						= rng.get_Range("F" + rowTxt, "F" + rowTxt);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignCenter;
				cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				if(detail.Guaranty == false)
				{
					// ����
					txt							= detail.PriceTxt;
					cell						= rng.get_Range("G" + rowTxt, "G" + rowTxt);
					cell.Value2					= txt;
					cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignCenter;
					cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
					// �����
					txt							= detail.SummTxt;
					cell						= rng.get_Range("H" + rowTxt, "H" + rowTxt);
					cell.Value2					= txt;
					cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignCenter;
					cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);	
				}
				else
				{
					// �������� ��� �������
					txt							= "��������";
					if(detail.Outer == true)	txt = "�����������";
					cell						= rng.get_Range("G" + rowTxt, "H" + rowTxt);
					cell.MergeCells				= true;
					cell.Value2					= txt;
					cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignCenter;
					cell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				}
				// ������� �����
				ret			= detail.Summ;
			}
			catch(Exception E)
			{
				Db.SetException(E);
				return ret;
			}
			return ret;
		}

		protected void DownloadFormat(Excel.Worksheet ws)
		{
			// �������������� ����������� ������������ ���� � Excel
			Excel.Range rng;
			try
			{
				rng						= ws.get_Range("A1", "A1");
				rng.Columns.ColumnWidth	= 4;
				rng						= ws.get_Range("B1", "B1");
				rng.Columns.ColumnWidth	= 12.5;
				rng						= ws.get_Range("C1", "C1");
				rng.Columns.ColumnWidth	= 26.5;
				rng						= ws.get_Range("D1", "D1");
				rng.Columns.ColumnWidth	= 1;
				rng						= ws.get_Range("E1", "E1");
				rng.Columns.ColumnWidth	= 9;
				rng						= ws.get_Range("F1", "F1");
				rng.Columns.ColumnWidth	= 9;
				rng						= ws.get_Range("G1", "G1");
				rng.Columns.ColumnWidth	= 11;
				rng						= ws.get_Range("H1", "H1");
				rng.Columns.ColumnWidth	= 11;

				// ��������� ��������
				ws.PageSetup.LeftMargin		= ws.Application.InchesToPoints(0.393700787401575);
				ws.PageSetup.RightMargin	= ws.Application.InchesToPoints(0.393700787401575);
			}
			catch(Exception E)
			{
				Db.SetException(E);
			}
		}
		protected void DownloadBrifTitle(Excel.Workbook wb)
		{
			// ��������� ��������� �� ������������ ���� � Excel
			Excel.Range			cell;
			Excel.Worksheet		ws;
			Excel.Range			rng;
			string				txt;
			int					count;
			string				rowTxt;
			try
			{
				ws							= (Excel.Worksheet)wb.Worksheets[1];
				rng							= ws.Rows;
				rng							= rng.CurrentRegion;
				count						= rng.Rows.Count + 1;
				rng							= ws.Rows;
				rowTxt						= count.ToString();

				// ����� ��������
				txt							= card.Number.ToString();
				cell						= rng.get_Range("A" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold				= true;
				// ���� ��������
				txt							= card.DateTxt;
				cell						= rng.get_Range("B" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignLeft;
				cell.Font.Bold				= true;
				// ����� �����/������
				if(card.WarrantNumber > 0)
				{
					txt							= card.WarrantNumber.ToString();
					cell						= rng.get_Range("C" + rowTxt, Missing.Value);
					cell.Value2					= txt;
					cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignLeft;
					cell.Font.Bold				= true;
					txt							= card.WarrantDateTxt;
					cell						= rng.get_Range("D" + rowTxt, Missing.Value);
					cell.Value2					= txt;
					cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignLeft;
					cell.Font.Bold				= true;
				}
				// ������
				txt							= card.PartnerNameTxt;
				cell						= rng.get_Range("E" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignLeft;
				// ����������
				// ������
				txt							= card.Auto.ModelTxt;
				cell						= rng.get_Range("F" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignLeft;
				// �����
				txt							= card.Auto.BodyNo;
				cell						= rng.get_Range("G" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignLeft;
				// ���. �����
				txt							= card.Auto.SignNo;
				cell						= rng.get_Range("H" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignLeft;
				// ����� �� ������
				txt							= cardSumm.ToString();
				cell						= rng.get_Range("I" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignRight;
				// ������
				txt							= card.RunTxt;
				cell						= rng.get_Range("J" + rowTxt, Missing.Value);
				cell.Value2					= txt;
				cell.HorizontalAlignment	= Excel.XlHAlign.xlHAlignRight;
			}
			catch(Exception E)
			{
				Db.SetException(E);
			}
		}
		protected void DownloadFormatBrifTitle(Excel.Workbook wb)
		{
			// �������������� ����������� ������������ ���� � Excel
			Excel.Range			rng;
			Excel.Worksheet		ws;
			try
			{
				ws						= (Excel.Worksheet)wb.Worksheets[1];
				rng						= ws.get_Range("A1", "A1");
				rng.Columns.ColumnWidth	= 8;
				rng.Value2				= "� ��������";
				rng						= ws.get_Range("B1", "B1");
				rng.Columns.ColumnWidth	= 12;
				rng.Value2				= "���� ��������";
				rng						= ws.get_Range("C1", "C1");
				rng.Columns.ColumnWidth	= 8;
				rng.Value2				= "� ������";
				rng						= ws.get_Range("D1", "D1");
				rng.Columns.ColumnWidth	= 12;
				rng.Value2				= "���� ������";
				rng						= ws.get_Range("E1", "E1");
				rng.Columns.ColumnWidth	= 30;
				rng.Value2				= "������";
				rng						= ws.get_Range("F1", "F1");
				rng.Columns.ColumnWidth	= 12;
				rng.Value2				= "����������";
				rng						= ws.get_Range("G1", "G1");
				rng.Columns.ColumnWidth	= 12;
				rng.Value2				= "� ������";
				rng						= ws.get_Range("H1", "H1");
				rng.Columns.ColumnWidth	= 10;
				rng.Value2				= "���. ����";
				rng						= ws.get_Range("I1", "I1");
				rng.Columns.ColumnWidth	= 12;
				rng.Value2				= "�����";
				rng						= ws.get_Range("J1", "J1");
				rng.Columns.ColumnWidth	= 12;
				rng.Value2				= "������";

				// ��������� ��������
				ws.PageSetup.LeftMargin		= ws.Application.InchesToPoints(0.393700787401575);
				ws.PageSetup.RightMargin	= ws.Application.InchesToPoints(0.393700787401575);
				ws.PageSetup.Orientation	= Excel.XlPageOrientation.xlLandscape;
			}
			catch(Exception E)
			{
				Db.SetException(E);
			}
		}
	}
}
