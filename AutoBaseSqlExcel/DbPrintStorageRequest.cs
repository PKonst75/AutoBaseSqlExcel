using System;
using System.Collections;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;


namespace AutoBaseSql
{
	/// <summary>
	/// ������ ������ �� ���������� ������.
	/// </summary>
	public class DbPrintStorageRequest:DbPrint
	{
		ArrayList elements;

		SolidBrush	draw_brush;
		Font		font_print;
		Font		font_print_large;
		Pen			pen_thin;

		string request_date_txt;

		public DbPrintStorageRequest(ListView list)
		{
			// ������������� ������ ������, �� ������� �������� � �����
			elements = new ArrayList();	
			foreach(ListViewItem itm in list.SelectedItems)
			{
				DtStorageRequest.CodeYear code_year = (DtStorageRequest.CodeYear)itm.Tag;
				DtStorageRequest element = DbSqlStorageRequest.Find(code_year.code, code_year.year);
				elements.Add(element);
			}
			// ����������� ��� ������
			draw_brush			= new SolidBrush(Color.Black);
			font_print			= new Font("Arial", 10);
			font_print_large	= new Font("Arial", 10, FontStyle.Bold);
			pen_thin			= new Pen(draw_brush, 0.3F);

			// ���� � ����� ������������ ������
			request_date_txt	= DateTime.Now.ToString();
		}

		public override void  PrintPage(Graphics graph, int page)
		{
			int offset = 10;
			offset = PrintHeader(graph, offset, "(��������� ������)");
			offset += 4;
			offset = PrintBody(graph, offset);
			offset += 4;
			offset = PrintFooter(graph, offset, "������");
			offset += 10;
			offset = PrintHeader(graph, offset, "(��������� ����������)");
			offset += 4;
			offset = PrintBody(graph, offset);
			offset += 4;
			offset = PrintFooter(graph, offset, "�������");
		}

		protected int PrintHeader(Graphics graph, int offset, string txt)
		{
			PrintText(graph, "������ �� �������� ��������� " + txt + " �� " + request_date_txt, 10, offset, 190, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print_large, draw_brush, false);
			return offset + 10;
		}

		protected int PrintBody(Graphics graph, int offset)
		{
			foreach(DtStorageRequest element in elements)
			{
				PrintText(graph, element.GetData("������������_�����_������").ToString(), 10, offset, 150, 5, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
				PrintText(graph, element.GetData("����������_�����_������").ToString(), 180, offset, 20, 5, System.Drawing.StringAlignment.Far, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
				DbPrint.PrintLineHor(graph, 10, offset + 5, 190, pen_thin, false);
				offset += 5;
			}
			return offset;
		}

		protected int PrintFooter(Graphics graph, int offset, string txt)
		{
			PrintText(graph, "������ �� �������� " + txt, 10, offset, 90, 5, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			DbPrint.PrintLineHor(graph, 60, offset + 5, 60, pen_thin, false);
			return offset + 10;
		}

	}
}
