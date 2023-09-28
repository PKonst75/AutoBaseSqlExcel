using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbPrintGivingoutP2.
	/// </summary>
	public class DbPrintGivingoutP2:DbPrint
	{
		SolidBrush	brush_standart;
		Font		font_small_bold;
		Font		font_middle_bold;
		Font		font_small;
		Font		font_middle;
		Pen			pen_thin;

		ArrayList	array_1;

		public DbPrintGivingoutP2()
		{
			// ���������� ������������ ��� ������
										  brush_standart		= new SolidBrush(Color.Black);
			font_small_bold		= new Font("Arial", 8, FontStyle.Bold);
			font_small			= new Font("Arial", 8);
			font_middle_bold	= new Font("Arial", 10, FontStyle.Bold);
			font_middle			= new Font("Arial", 10);
			pen_thin			= new Pen(brush_standart, 0.3F);

			// ��� ������� �����
			array_1	= new ArrayList();
			array_1.Add("��������� ��������");
			array_1.Add("������� ����������");
			array_1.Add("���������� ������");
			array_1.Add("���� ��� ������� �������");
			array_1.Add("������ ����������� ���������");
			array_1.Add("����� ����������������");
			array_1.Add("��������� �����");
			array_1.Add("����� ���������������");
			array_1.Add("������");
			array_1.Add("������ ������ / ����");
			array_1.Add("������������");
			array_1.Add("���������� ������ ����������");
			array_1.Add("������� ������� ��");
			array_1.Add("������� ������� ����");
			array_1.Add("������������� / ����������");
			array_1.Add("���������");
			array_1.Add("��������� ������ ������");
		}

		#region ������ ����� ���������� � ������� �������� ����������
		private int PrintBlock1(Graphics graph, int offset,  bool test, bool print, object o)
		{
			// ��������������� �����������
			RectangleF rect;
			int y		= offset;
			string text	= "";
			// ����������� ���������
			int offset_x_left		= 10;
			int offset_x_right		= 10;
			int page_width			= 210;
			int title_height		= 4;
			int page_height			= 297 - 10;
			int row_count			= 7;
			int	col1		=	60;
			int col3		=	25;
			int col2		=	25;
			int	rowheight		= 4;
			int	titlerowheight	= 7;

			if(test == true)
			{
				// ��� ������������ ��������� ��������� �� �������� ��� � �������
				y	+= title_height;	// ���������
				y	+= titlerowheight;		// ������ ������� �������
				foreach(string txt in array_1)
				{
					y += rowheight;
				}
				return y;
			}
			if(print == false)
			{
				// ��� ������������ ��������� ��������� �� �������� ��� � �������
				y	+= title_height;	// ���������
				y	+= titlerowheight;		// ������ ������� �������
				foreach(string txt in array_1)
				{
					y += rowheight;
				}
				return y;
			}

			// ���������
			text	= "������ ���������� �� �������";
			PrintText(graph, text, offset_x_left, y, page_width - offset_x_right - offset_x_left, title_height, StringAlignment.Near, StringAlignment.Center, font_middle_bold, brush_standart, false);
			y += title_height;
			
			// ������� ��� ������ �������
			// ������ �������
			PrintTextBox(graph, "���������� ��������", offset_x_left, y, col1, titlerowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "�����", offset_x_left + col1, y, col2, titlerowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "����������", offset_x_left + col1 + col2, y, col3, titlerowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			y += titlerowheight;
			// ������ �����
			foreach(string txt in array_1)
			{
				PrintTextBox(graph, txt, offset_x_left, y, col1, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_middle, brush_standart, pen_thin, false);
				rect	= new RectangleF(offset_x_left + col1, y, col2, rowheight);
				PrintBox(graph, rect, pen_thin);
				rect	= new RectangleF(offset_x_left + col1 + col2, y, col3, rowheight);
				PrintBox(graph, rect, pen_thin);
				y += rowheight;
			}

			// �������� �������
			PrintImage(graph, "car_schema.bmp", offset_x_left + col1 + col2 + col3 + 10, offset + 2, 60, 80, false);
			return y;
		}
		#endregion

		#region ������ ����� ������� ����������� ����������
		private int PrintBlock2(Graphics graph, int offset,  bool test, bool print, object o)
		{
			// ��������������� �����������
			int y		= offset;
			// ����������� ���������
			int offset_x_left		= 10;

			y += 2;
			if(test == true || print == false)
			{
				y += 5;
				y += 5;
				return y;
			}

			// ���������
			PrintLeftCheckBoxX(graph, "- ���� �����", offset_x_left, y, 30, 5, 1, 1, 0, StringAlignment.Near, font_middle, brush_standart, pen_thin, false);
			PrintLeftCheckBoxD(graph, "- ��������", offset_x_left + 30, y, 30, 5, 1, 1, 0, StringAlignment.Near, font_middle, brush_standart, pen_thin, false);
			PrintLeftCheckBoxO(graph, "- �������", offset_x_left + 30 + 30, y, 30, 5, 1, 1, 0, StringAlignment.Near, font_middle, brush_standart, pen_thin, false);
			PrintLeftCheckBox(graph, "- ����������� ������", offset_x_left + 30 + 30 + 30, y, 45, 5, 1, 1, 0, StringAlignment.Near, font_middle, brush_standart, pen_thin, false);
			PrintLeftCheckDiamond(graph, "- �������", offset_x_left + 30 + 30 + 30 + 45, y, 30, 5, 1, 1, 0, StringAlignment.Near, font_middle, brush_standart, pen_thin, false);
			y += 5;
			PrintText(graph, "��������� ����������:", offset_x_left, y, 45, 5, StringAlignment.Near, StringAlignment.Center, font_middle, brush_standart, false);
			PrintLeftCheckBox(graph, " ������", offset_x_left + 45, y, 30, 5, 1, 1, 0, StringAlignment.Near, font_middle, brush_standart, pen_thin, false);
			PrintLeftCheckBox(graph, " ������", offset_x_left + 45 + 30, y, 30, 5, 1, 1, 0, StringAlignment.Near, font_middle, brush_standart, pen_thin, false);
			PrintLeftCheckBox(graph, " ����� �������", offset_x_left + 45 + 30 + 30, y, 30, 5, 1, 1, 0, StringAlignment.Near, font_middle, brush_standart, pen_thin, false);
			y += 5;
			return y;
		}
		#endregion

		#region ������ ����� �������� ����������� ����������
		private int PrintBlock3(Graphics graph, int offset,  bool test, bool print, object o)
		{
			// ��������������� �����������
			int y		= offset;
			// ����������� ���������
			int offset_x_left		= 10;
			int row_count			= 10;

			y += 2;
			if(test == true || print == false)
			{
				y += 8;
				for(int i = 0; i < row_count; i++)
				{
					y += 5;
				}
				return y;
			}

			// ���������
			PrintText(graph, "�������� ����������� ����������:", offset_x_left, y, 120, 8, StringAlignment.Near, StringAlignment.Center, font_middle_bold, brush_standart, false);
			y += 8;
			for(int i = 0; i < row_count; i++)
			{
				PrintLineHor(graph, offset_x_left, y, 190, pen_thin, false);
				y += 5;
			}
			return y;
		}
		#endregion

		#region ������ ����� ������ ��������� �� ����������� ������
		private int PrintBlock4(Graphics graph, int offset,  bool test, bool print, object o)
		{
			// ��������������� �����������
			int y		= offset;
			RectangleF rect;
			// ����������� ���������
			int offset_x_left		= 10;
			int h					= 5;
			int w1					= 70;
			int w2					= 20;
			int w3					= 50;
			int w4					= 20;
			int x2					= 120;
			

			y += 2;
			if(test == true || print == false)
			{
				y += h;
				y += h;
				return y;
			}

			// ���������
			PrintTextBox(graph, "������ ��������� �� ����������� ������ (�� ����������� �����)", offset_x_left, y, w1, h * 2, 1, StringAlignment.Near, StringAlignment.Center, font_middle, brush_standart, pen_thin, false);
			for (int i = 1; i <= 5; i++)
			{
				PrintTextBox(graph, i.ToString(), offset_x_left + w1 + w2 * (i - 1), y, w2, h, 1, StringAlignment.Center, StringAlignment.Center, font_middle, brush_standart, pen_thin, false);
			}
			y += h;
			for (int i = 1; i <= 5; i++)
			{
				PrintBox(graph, offset_x_left + w1 + w2 * (i - 1), y, w2, h, pen_thin);
			}
			y += h;
			return y;
		}
		#endregion

		#region ������ ����� ���������� � ��������
		private int PrintBlock5(Graphics graph, int offset,  bool test, bool print, object o)
		{
			// ��������������� �����������
			string txt;
			int y		= offset;
			// ����������� ���������
			int offset_x_left		= 10;
			int h1					= 4;
			int w1					= 190;

			txt = "-�������� �������� �������� ������, �������, ��������������� ������������ � �������������� �� ��� ���������� �����, ����������� 30 ���� ��� 1000 �� �������.";
			txt += "\n";
			txt += "-�������� �� �������� ���� ����� �������������� ���������� �������: �������������� ������ - 1000 ��; ������ �� ��������� ������� - 1000 ��. �� ������������������ ������ (����� ���������, �������, �����, ��������� �������� �����������������, ���� ���������, ��������� ������� � �.�.) �������� �� ����������������.";
			SizeF size = MeasureText(graph, txt, w1, StringAlignment.Near, StringAlignment.Near, font_small);

			y += 2;
			if(test == true || print == false)
			{
				y += h1;
				y += (int)Math.Round(size.Height) + 1;
				return y;
			}

			// ���������
			PrintText(graph, "������� ��������", offset_x_left, y, w1, h1, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			y += h1;
			PrintText(graph, txt, offset_x_left, y, w1, size.Height, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, false);
			y += (int)Math.Round(size.Height) + 1;
			return y;
		}
		#endregion

		#region ������ ����� ��������
		private int PrintBlock6(Graphics graph, int offset,  bool test, bool print, object o)
		{
			// ��������������� �����������
			string txt;
			int y		= offset;
			// ����������� ���������
			int offset_x_left		= 10;
			int h1					= 4;
			int w1					= 60;

			y += 6;
			if(test == true || print == false)
			{
				y += h1;
				return y;
			}

			// ���������
			txt	= "���� � ����� ������ ����������";
			PrintText(graph, txt, offset_x_left, y, 60, h1, StringAlignment.Near, StringAlignment.Far, font_small, brush_standart, false);
			y += h1;
			PrintLineHor(graph, offset_x_left + 60, y, 60, pen_thin, false);
			y += h1 + 4;
			PrintText(graph, "���������� ����� (������-�����������)", offset_x_left, y, w1, h1, StringAlignment.Near, StringAlignment.Far, font_small, brush_standart, false);
			y += h1;
			PrintLineHor(graph, offset_x_left + w1, y, 60, pen_thin, false);
			PrintLineHor(graph, offset_x_left + w1 + 70, y, 60, pen_thin, false);
			PrintText(graph, "(�������)", offset_x_left + w1, y, 60, h1, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
			PrintText(graph, "(���������)", offset_x_left + w1 + 70, y, 60, h1, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
			y += h1 + 4;
			PrintText(graph, "���������� ������ (��������)", offset_x_left, y, w1, h1, StringAlignment.Near, StringAlignment.Far, font_small, brush_standart, false);
			y += h1;
			PrintLineHor(graph, offset_x_left + w1, y, 60, pen_thin, false);
			PrintLineHor(graph, offset_x_left + w1 + 70, y, 60, pen_thin, false);
			PrintText(graph, "(�������)", offset_x_left + w1, y, 60, h1, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
			PrintText(graph, "(���������)", offset_x_left + w1 + 70, y, 60, h1, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
			y += h1;
			return y;
		}
		#endregion

		// �������� ��������� ������
		public override void  PrintPage(Graphics graph, int page)
		{
			// ��� ���������� �� ��������
			int offset = 0;

			offset = page_min_y;
			offset = PrintBlock(graph, offset, new DelegatePrintBlock(PrintBlock1), null);
			offset = PrintBlock(graph, offset, new DelegatePrintBlock(PrintBlock2), null);
			offset = PrintBlock(graph, offset, new DelegatePrintBlock(PrintBlock3), null);
			offset = PrintBlock(graph, offset, new DelegatePrintBlock(PrintBlock4), null);
			offset = PrintBlock(graph, offset, new DelegatePrintBlock(PrintBlock5), null);
			offset = PrintBlock(graph, offset, new DelegatePrintBlock(PrintBlock6), null);
		}
	}
}
