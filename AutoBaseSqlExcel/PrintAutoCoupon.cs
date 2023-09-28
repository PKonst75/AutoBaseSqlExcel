using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// ������ �������� ����������
	/// </summary>
	/// 
	public class PrintAutoCoupon
	{
		DbAuto		auto		= null;

		Pen			thinPen		= null;
		SolidBrush	drawBrush	= null;
		Font		printFont	= null;
		Font		boldFont	= null;
		Font		boldFont12	= null;

		public PrintAutoCoupon(DbAuto src)
		{
			auto	= new DbAuto(src);	// ����� ����� ����������
		}

		// ���������� ������������ ������, �������, �����
		private void PrepareTools()
		{
			// �����
			thinPen			= new Pen(Color.Black, (float)0.02);
			// �����
			drawBrush		= new SolidBrush(Color.Black);
			// ������
			printFont		= new Font("Arial", 12);
			boldFont		= new Font("Arial", 16, FontStyle.Bold);
			boldFont12		= new Font("Arial", 12, FontStyle.Bold);
		}
		// ����������� ��� ������ ������ ��������
		private void pd_PrintPage(object sender, PrintPageEventArgs ev) 
		{
			// ��������� ���������� ������
			ev.Graphics.PageUnit = GraphicsUnit.Millimeter;

			PrintMainTitle(ev.Graphics);
			PrintCouponTitle(ev.Graphics);
			PrintAutoTitle(ev.Graphics);
			PrintAutoPrice(ev.Graphics);

			ev.HasMorePages = false;		// � ����� ������ �������� ������ ����
		}
		// �������� ���������� ������� ������
		public void Print() 
		{
			try 
			{
				try 
				{
					PrepareTools();				// ������� �����������

					PrintDocument pd = new PrintDocument();
					// �������� ��������
					pd.DefaultPageSettings.Landscape = false;
					pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);
					PrintPreviewDialog preview = new PrintPreviewDialog();
					preview.Document = pd;
					preview.ShowDialog();
					//pd.Print();
				}  
				finally 
				{
				}
			}  
			catch(Exception ex) 
			{
				MessageBox.Show(ex.Message);
			}
		}

		#region ������ ������
		private void PrintMainTitle(Graphics graph)
		{
			// ������ ��������� ��������� ��������
			string text;
			RectangleF rect = new RectangleF(0, 0, 0, 0);

			// ��������� �������� � ������������� ���������
			rect.X = 10;
			rect.Y = 10;
			rect.Width = 60;
			rect.Height = 20;
			Image carImage = Image.FromFile("avto1.bmp");
			graph.DrawImage(carImage, rect);

			rect.Y = 30;
			rect.X = 10;
			rect.Width = 90;
			rect.Height = 10;
			text = "�.�����������, ��.�������,48";
			PrintBoxFixedNoBorder(graph, text, rect, StringAlignment.Near);
			rect.Y = 35;
			text = "�.(383)330-03-03 (��������������)";
			PrintBoxFixedNoBorder(graph, text, rect, StringAlignment.Near);
			rect.Y = 40;
			text = "www.avto-1.ru";
			PrintBoxFixedNoBorder(graph, text, rect, StringAlignment.Near);
		}
		private void PrintAutoTitle(Graphics graph)
		{
			// ������ ���������� �� ����������
			string txt;
			RectangleF rect = new RectangleF(0, 0, 0, 0);
			int posY	= 50;

			// ��������� �������
			rect.Y = posY;
			rect.X = 10;
			rect.Width = 90;
			rect.Height = 10;
			txt = "������ ����������";
			PrintBoxFixed(graph, txt, rect, true, StringAlignment.Near, StringAlignment.Center, boldFont);
			rect.Y = rect.Y + 10;
			txt = "�������, ������������";
			PrintBoxFixed(graph, txt, rect, true, StringAlignment.Near, StringAlignment.Center, boldFont);
			rect.Y = rect.Y + 10;
			txt = "����";
			PrintBoxFixed(graph, txt, rect, true, StringAlignment.Near, StringAlignment.Center, boldFont);
			rect.Y = rect.Y + 10;
			txt = "VIN";
			PrintBoxFixed(graph, txt, rect, true, StringAlignment.Near, StringAlignment.Center, boldFont);
			rect.Y = rect.Y + 10;
			txt = "� ���������";
			PrintBoxFixed(graph, txt, rect, true, StringAlignment.Near, StringAlignment.Center, boldFont);
			// ���������� �������
			rect.Y = posY;
			rect.X = 100;
			rect.Width = 90;
			rect.Height = 10;
			txt = auto.ModelTxt;
			PrintBoxFixed(graph, txt, rect, true, StringAlignment.Near, StringAlignment.Center, boldFont);
			rect.Y = rect.Y + 10;
			txt = auto.AutoSubModelTxt + auto.AutoComplectTxt;
			PrintBoxFixed(graph, txt, rect, true, StringAlignment.Near, StringAlignment.Center, boldFont);
			rect.Y = rect.Y + 10;
			txt = auto.AutoColorsTxt;
			PrintBoxFixed(graph, txt, rect, true, StringAlignment.Near, StringAlignment.Center, boldFont);
			rect.Y = rect.Y + 10;
			txt = auto.Vin;
			PrintBoxFixed(graph, txt, rect, true, StringAlignment.Near, StringAlignment.Center, boldFont);
			rect.Y = rect.Y + 10;
			txt = auto.EngineNo;
			PrintBoxFixed(graph, txt, rect, true, StringAlignment.Near, StringAlignment.Center, boldFont);
		}

		private void PrintAutoPrice(Graphics graph)
		{
			// ������ ���������� �� ����������
			string txt;
			RectangleF rect = new RectangleF(0, 0, 0, 0);
			int posY	= 110;
			float y		= 0.0f;

			// ��������� �������
			rect.Y = posY;
			rect.X = 10;
			rect.Width = 140;
			rect.Height = 10;
			txt = "������� ���� ����������";
			PrintBoxFixed(graph, txt, rect, true, StringAlignment.Near, StringAlignment.Center, boldFont);
			txt	= "";
			rect.X = 150;
			rect.Width = 40;
			PrintBoxFixed(graph, txt, rect, true);
			y	= rect.Y + 10;
			PrintOptionTitle(graph, "�������������� �����", y);		y += 5;
			PrintOption(graph, "������", y);						y += 5;
			PrintOption(graph, "����� �����", y);					y += 5;
			PrintOption(graph, "�����", y);							y += 5;
			PrintOption(graph, "���������� ���������", y);			y += 5;
			PrintOption(graph, "������� ������", y);				y += 5;
			PrintOption(graph, "", y);								y += 5;
			PrintOption(graph, "", y);								y += 5;
			PrintOption(graph, "", y);								y += 5;
			PrintOption(graph, "�������", y);						y += 5;
			PrintOption(graph, "������ �������", y);				y += 5;
			PrintOption(graph, "���������", y);						y += 5;
			PrintOption(graph, "", y);								y += 5;
			PrintOption(graph, "", y);								y += 5;
			PrintOptionTitle(graph, "������", y);					y += 5;
			PrintOption(graph, "�������������", y);					y += 5;
			PrintOption(graph, "�������� ���������", y);			y += 5;
			PrintOption(graph, "������ ���������", y);				y += 5;
			PrintOption(graph, "������", y);						y += 5;
			PrintOption(graph, "", y);								y += 5;
			PrintOption(graph, "", y);								y += 5;
			PrintOption(graph, "������������", y);					y += 5;
			PrintOption(graph, "", y);								y += 5;
			PrintOption(graph, "", y);								y += 5;
			PrintOption(graph, "", y);								y += 5;
			PrintOption(graph, "", y);								y += 5;
			PrintOption(graph, "", y);								y += 5;
			PrintOptionTitle(graph, "����������", y);				y += 5;
			PrintOption(graph, "���������� �����", y);				y += 5;
			rect.Y = y;
			rect.X = 10;
			rect.Width = 140;
			rect.Height = 10;
			txt = "����� ���������";
			PrintBoxFixed(graph, txt, rect, true, StringAlignment.Near, StringAlignment.Center, boldFont);
			txt	= "";
			rect.X = 150;
			rect.Width = 40;
			PrintBoxFixed(graph, txt, rect, true);
		}
		private void PrintOption(Graphics graph, string txt, float posY)
		{
			// ������ ������� �����
			RectangleF rect = new RectangleF(0, 0, 0, 0);
			rect.X = 10;
			rect.Y = posY;
			rect.Width = 140;
			rect.Height = 5;
			PrintBoxFixed(graph, txt, rect, true, StringAlignment.Near, StringAlignment.Center, boldFont12);
			rect.X = 150;
			rect.Y = posY;
			rect.Width = 40;
			rect.Height = 5;
			PrintBoxFixed(graph, "", rect, true, StringAlignment.Near, StringAlignment.Center, boldFont12);
		}
		private void PrintOptionTitle(Graphics graph, string txt, float posY)
		{
			// ������ ��������� ������� �����
			RectangleF rect = new RectangleF(0, 0, 0, 0);
			rect.X = 10;
			rect.Y = posY;
			rect.Width = 180;
			rect.Height = 5;
			PrintBoxFixed(graph, txt, rect, true, StringAlignment.Center, StringAlignment.Center, boldFont12);
		}
		private void PrintCouponTitle(Graphics graph)
		{
			// ������ ���������� �� ����������
			string txt;
			RectangleF rect = new RectangleF(0, 0, 0, 0);
			int posY	= 10;

			// ��������� �������
			rect.Y = posY;
			rect.X = 100;
			rect.Width = 50;
			rect.Height = 10;
			txt = "����";
			PrintBoxFixed(graph, txt, rect, false, StringAlignment.Near, StringAlignment.Center, boldFont);
			rect.Y = rect.Y + 10;
			txt = "��������";
			PrintBoxFixed(graph, txt, rect, false, StringAlignment.Near, StringAlignment.Center, boldFont);
			rect.Y = rect.Y + 10;
			txt = "������� ���";
			PrintBoxFixed(graph, txt, rect, false, StringAlignment.Near, StringAlignment.Center, boldFont);
			rect.X		= 170;
			rect.Width	= 10;
			txt = "";
			PrintBoxFixed(graph, txt, rect, true);
		}
		#endregion

		#region ������ ��������
		public void PrintBoxFixed(Graphics graph, string text, Rectangle rect)
		{
			StringFormat strFormat = new StringFormat();
			strFormat.Alignment = StringAlignment.Center;
			strFormat.LineAlignment = StringAlignment.Center;
			strFormat.Trimming = StringTrimming.Word;
			graph.DrawString(text, printFont, drawBrush, rect, strFormat);
			graph.DrawRectangle(thinPen, rect);
		}
		public void PrintBoxFixed(Graphics graph, string text, RectangleF rect)
		{
			StringFormat strFormat = new StringFormat();
			strFormat.Alignment = StringAlignment.Center;
			strFormat.LineAlignment = StringAlignment.Center;
			strFormat.Trimming = StringTrimming.Word;
			graph.DrawString(text, printFont, drawBrush, rect, strFormat);
			graph.DrawRectangle(thinPen, rect.X, rect.Y, rect.Width, rect.Height);
		}
		public void PrintBoxFixedNoBorder(Graphics graph, string text, RectangleF rect, StringAlignment alignment)
		{
			StringFormat strFormat = new StringFormat();
			strFormat.Alignment = alignment;
			strFormat.LineAlignment = StringAlignment.Center;
			strFormat.Trimming = StringTrimming.Word;
			graph.DrawString(text, printFont, drawBrush, rect, strFormat);
		}
		public void PrintBoxFixed(Graphics graph, string text, RectangleF rect, bool border)
		{
			StringFormat strFormat = new StringFormat();
			strFormat.Alignment = StringAlignment.Center;
			strFormat.LineAlignment = StringAlignment.Center;
			strFormat.Trimming = StringTrimming.Word;
			graph.DrawString(text, printFont, drawBrush, rect, strFormat);
			if(border == true)
				graph.DrawRectangle(thinPen, rect.X, rect.Y, rect.Width, rect.Height);
		}
		public void PrintBoxFixed(Graphics graph, string text, RectangleF rect, bool border, StringAlignment alignment, StringAlignment lineAlignment)
		{
			StringFormat strFormat = new StringFormat();
			strFormat.Alignment = alignment;
			strFormat.LineAlignment = lineAlignment;
			strFormat.Trimming = StringTrimming.Word;
			graph.DrawString(text, printFont, drawBrush, rect, strFormat);
			if(border == true)
				graph.DrawRectangle(thinPen, rect.X, rect.Y, rect.Width, rect.Height);
		}
		public void PrintBoxFixed(Graphics graph, string text, RectangleF rect, bool border, StringAlignment alignment, StringAlignment lineAlignment, Font font)
		{
			StringFormat strFormat = new StringFormat();
			strFormat.Alignment = alignment;
			strFormat.LineAlignment = lineAlignment;
			strFormat.Trimming = StringTrimming.Word;
			graph.DrawString(text, font, drawBrush, rect, strFormat);
			if(border == true)
				graph.DrawRectangle(thinPen, rect.X, rect.Y, rect.Width, rect.Height);
		}
		#endregion
	}
}
