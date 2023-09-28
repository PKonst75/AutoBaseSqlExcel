using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbPrintGuarantee.
	/// </summary>
	public class DbPrintGuarantee:DbPrint
	{
		SolidBrush	draw_brush;
		Font		font_print;
		Font		font_large_bold;
		Pen			pen_bold;

		DateTime	date_close;		// ���� �������� �����-������
		bool		date_close_is;	// ���� ���������� ���� ��������
		long		warrant_number;	// ����� �����-������
		ArrayList	works;			// ������ ����� �� ��������

		public DbPrintGuarantee(DtCard card)
		{
			// ���������� ������������ � ������
			draw_brush		= new SolidBrush(Color.Black);
			font_print		= new Font("Arial", 10);
			font_large_bold	= new Font("Arial", 14, FontStyle.Bold);
			pen_bold		= new Pen(draw_brush, 1);

			// ����������� ����������� ������
			// ���� �������� �����-������
			DtCardAction close = DbSqlCardAction.FindClose(card);
			if(close != null)
			{
				date_close		= (DateTime)close.Date;
				date_close_is	= true;
			}
			else
			{
				date_close		= DateTime.Now;
				date_close_is	= false;
			}
			// ����� �����-������
			warrant_number	= card.WarrantNumber;
			// ������ ����� �� ��������
			works = new ArrayList();
			DbSqlCardWork.SelectInArray(card, works);
		}

		public override void  PrintPage(Graphics graph, int page)
		{
			PrintTitle(graph);
			float y = 40;
			foreach(object o in works)
			{
				DtCardWork work = (DtCardWork)o;
				y += PrintWork(graph, y, work, false);
			}
			PrintGuaranteeConditions(graph, 100, false);
		}

		private void PrintTitle(Graphics graph)
		{
			// ������������ ���������
			PrintImage(graph, "auto1.jpg", 10, 10, 40, 16, false);
			PrintText(graph, "�������������, ��. �������, 48", 55, 10, 100, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, "(383)330-03-03 (����� ������)", 55, 14, 100, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, "(383)332-02-92 (��������� �����)", 55, 18, 100, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, "(383)333-81-02 (������)", 55, 22, 100, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);

			// ������ ������������ ������
			PrintText(graph, "����������� ����� �" + warrant_number.ToString(), 115, 10, 90, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_large_bold, draw_brush, false);
			if(date_close_is)
				PrintText(graph, "�� " + date_close.ToShortDateString(), 115, 15, 90, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_large_bold, draw_brush, false);
			// ������
			PrintLineHor(graph, 10, 30, 190, pen_bold, false);
		}

		private void PrintGuaranteeConditions(Graphics graph, float y, bool test)
		{
			// ������� ��������
			string text;
			text = "������ ������������� �� ���������� ������� ������������ ������������. ������� �� �������� �� ��� � ��������������� �� ��������� ����������� ��� ��������������� ����� ����������. ������ �������������� � ���� �� 20 ����. ��� ���������� ���������, ���� ������� ����� ���� ������� �� 45 ����.";
			text += "\n" + "� ������ ������������� ���������� �������������, ������� ������� ��������� ������ ���������� ������������� �� ���.";

			if(!test)PrintText(graph, text, 10, y, 190, 40, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
		}

		private float PrintWork(Graphics graph, float y, DtCardWork work, bool test)
		{
			// ������ ����� ������
			string text = "";
			text = (string)work.GetData("������������_��������_������");
			SizeF size = MeasureText(graph, text, 190, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print);
			if(!test)PrintText(graph, text, 10, y, 190, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			return size.Height;
		}
	}
}
