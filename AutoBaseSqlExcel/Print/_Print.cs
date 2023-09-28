using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using AutoBaseSql.Print;

namespace AutoBaseSql
{
	/// <summary>
	/// ����� ��� ����������� ������� ������������ ������ : �����-�����������
	/// </summary>
	public class _Print
	{
		const int PAGEMAX_Y = 277; // ������� ������� � �� ��������
		const int PAGEMIN_Y = 10; // ������ ������� � �� ��������


		private bool _pageHeadPrinted;		// ���� ��������� ��������� ������� ��� ��� ���
		private int _currentPageToPrint;	// �������, ����� ������� ������ ���������� � ������ ������
		private int _currentPageRun;		// ������� ����� ������� ������ ����������� ��� ����������� ������
		private float _currentPageY;		// ������� �� �������� Y � ������� ����� ������ ��������
		private float _maxPage;				// ������������ ���������� �������

		public DocumentPrintSchema _printSchema;	// ����-����� ��� ������
		public PrintDocumentBase _printDocument;	// ����� ��������������� ���������� ��������

		public _Print()
		{

		}

		protected void TestOn() // �������� ���� � ������ ������������
		{
			_printDocument.GetPrintTool().TestOn();

		}
		protected void TestOff() // ��������� ���� � ������ ������������
		{
			_printDocument.GetPrintTool().TestOff();
		}
		private float PrintSchemaElement(DocumentPrintSchemaElement element, float positionY) // ������ ���������� �������� �����
		{
			if (element.BlockHeadDelegate != null && element.FirstElementFlag)
			{
				// ������ ��������� , ���� ���� �������� ������ � ���� ������� ��� ������
				positionY = element.BlockHeadDelegate(positionY, null);
			}
			positionY = element.BlockDelegate(positionY, element.Element, element.Count);
			return positionY;
		}
		public void Print(PrintDocumentBase printing, bool previewFlag = false) // �������� ����� ��� ������� ������ ����� ������ ���������������� ���������
		{
			try
			{
				//_printBase = new PrintDrawingBase();
				_printDocument = printing;
				DocumentPrintSchema printSchema = printing.CreatePrintingSchema();
				if (printSchema == null) return; // ����� ������ �� ���������, ������ �� ��������
				// �������� ����� ������ ��������� 
				_printSchema = printSchema;

				// ��������� ���������
				_currentPageToPrint = 1;	// ��� ������� - ������ �������� � ������ ��������
				_maxPage = 1;				// ������������ �������� - ���������� ������������ ��� ����� ����

				// ��������� ��������� ������
				PrintDocument pd = new PrintDocument();
				pd.DefaultPageSettings.Landscape = false;
				pd.PrintPage += new PrintPageEventHandler(PrintPage);	// ���������� ������������ ������
				if (previewFlag)
				{
                    // ������ � ��������������
                    PrintPreviewDialog preview = new PrintPreviewDialog
                    {
                        Document = pd
                    };
                    preview.ShowDialog();
				}
				else
				{
					// ������ ����� �� ������� �� ���������
					pd.Print();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			finally
			{
			}
		}

		private void PrintPage(object sender, PrintPageEventArgs ev) // ���������������� ���������� ������� PrintPage ����������� NET ������� PrintDocument
		{
			ev.Graphics.PageUnit = GraphicsUnit.Millimeter; // ������ � ����������
			_printDocument.SetPrintToolGraphics(ev.Graphics);

			// ������
			_currentPageRun = 1;
			_currentPageY = PAGEMIN_Y;
			_pageHeadPrinted = false;

			// ������� ��������������� ������		
			foreach (DocumentPrintSchemaElement schema in _printSchema.SchemaElements)
			{
				PrintBlockHead(schema);
			}
			// ����� ����� ���������������� ������
	
			if (_currentPageToPrint == _maxPage)
			{
				ev.HasMorePages = false; // ������� ��������� ������
			}
			else
			{
				ev.HasMorePages = true;
				_currentPageToPrint++;
			}
		}

		private void PrintBlockHead(DocumentPrintSchemaElement printSchemaElement)
		{
			float y;  // ��� �������� ��������� �� ��������
			// ������ ������������ ��������� ��������
			if (_pageHeadPrinted == false)
			{
				TestOff();
				_pageHeadPrinted = true;
				if(_printSchema.PrintPageHeader != null)
					_currentPageY = _printSchema.PrintPageHeader(_currentPageY, null); // PrintStandartHead(_currentPageY);
			}
			// ������������� ������ ����� ����� ������� �������
			// ��������� ����������� ��������� ����� �� �������� �������
			TestOn(); // ��������� ������ ������������
			y = PrintSchemaElement(printSchemaElement, _currentPageY);

			if (y <= PAGEMAX_Y) // ������� ���������� �� ��������
			{
				if (_currentPageRun == _currentPageToPrint)
				{
					TestOff();
					_currentPageY = PrintSchemaElement(printSchemaElement, _currentPageY);
				}
				else
				{
					_currentPageY = y;
				}
			}
			else
			{
				// ������� ��������
				_pageHeadPrinted = false;
				_currentPageY = PAGEMIN_Y; // �������� � ����� ��������
				_currentPageRun++; // ����������� ������� ����������� �������� �� 1
				if (_currentPageRun > _maxPage) _maxPage = _currentPageRun;
				PrintBlockHead(printSchemaElement); // ����� ������� ��������
			}
		}
	
		public delegate float DelegatePrintBlock(float start_y, object print_data, int coutCounter = 0);

	}
}
