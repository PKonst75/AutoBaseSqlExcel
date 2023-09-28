using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbPrintGivingout.
	/// </summary>
	public class DbPrintGivingout:DbPrint
	{
		// ����������� ��� ������
		SolidBrush	brush_standart;
		Font		font_small_bold;
		Font		font_middle_bold;
		Font		font_small;
		Font		font_middle;
		Pen			pen_thin;

		private DtCard		card;

		#region ������ ��� ������
		protected class HeaderData
		{
			public enum DIALER:long{unknown=0, chevrolet=1, lada=2, kia=3}
			public string txt_model		= "";
			public string txt_sign		= "";
			public string txt_vin		= "";
			public string txt_owner		= "";
			public string txt_address	= "";
			public string txt_phone		= "";
			public string txt_represent	= "";
			public string txt_sell		= "";
			public string txt_run		= "";
			public string txt_card		= "";
			public string txt_workshop	= "";
			public string txt_warrant	= "";
			public DIALER dialer		= DIALER.unknown;

			public ArrayList	recomendations	= null;
			public bool cashless				= false;
			public bool inner					= false;

			public HeaderData(DtCard card)
			{
				// ������ ��������
				txt_card = card.GetData("�����_��������").ToString() + "/" + card.GetData("���_��������").ToString();
				if((long)card.GetData("�����_�����_��������") != 0)
				{
					txt_warrant = card.GetData("�����_�����_��������").ToString() + " �� " + card.GetData("����_�����_������_��������").ToString();
				}
				if((bool)card.GetData("�����������_��������") == true)
				{
					cashless = true;
				}
				if((bool)card.GetData("����������_��������") == true)
				{
					inner = true;
				}
				// �������� ������������ ������ ���������
				DtAuto auto = DbSqlAuto.Find((long)card.CodeAuto/*GetData("����������_��������")*/);
				if(auto != null)
				{
					txt_sign	= (string)auto.GetData("�����_����");
					txt_vin		= (string)auto.GetData("VIN");
					// ������ �� ����������
					// ������
					DtModel	model = DbSqlModel.Find((long)auto.GetData("������_���_����������_������"));
					if(model != null)
						txt_model	= (string)model.GetData("������");
					// ���� �������
					if((bool)auto.GetData("����_�������_����") == true)
						txt_sell = ((DateTime)auto.GetData("�������_����")).ToShortDateString();
					else
						txt_sell = "---------";

					// ��� ������������ ������
					DtBrand brand = DbSqlBrand.FindModel((long)model.GetData("���_����������_������"));
					string txt_brand = "";
					if(brand != null)
						txt_brand = (string)brand.GetData("������������_����������_�����");
					if(txt_brand == "�������") dialer = DIALER.chevrolet;
					if(txt_brand == "LADA") dialer = DIALER.lada;
					if(txt_brand == "KIA") dialer = DIALER.kia;

					// ������ ����������� ������
					string txt_dealer		= FileIni.GetParameter("print.ini", "#DEALER_BLOCK");
					char[] separators		= new Char[] {'\t'};
					string[] txt_dealers	= txt_dealer.Split(separators);
					bool flag				= false;
					foreach(string s in txt_dealers)
					{
						if(s == ((long)dialer).ToString()) flag = true;
					}
					if(!flag) dialer = DIALER.unknown;
				}
				else
				{

				}
				// ��������
				DtPartner owner = DbSqlPartner.Find((long)card.GetData("��������_��������"));
				if(owner != null)
				{
					txt_owner		= owner.GetTitle();
					txt_address		= owner.GetAddress();
					txt_phone		= owner.GetPhone();
				}
				// �������������
				DtPartner represent = DbSqlPartner.Find((long)card.GetData("�������������_��������"));
				if(represent != null)
					txt_represent	= represent.GetTitle();

				// �������������
				DtWorkshop workshop =  DbSqlWorkshop.Find((long)card.GetData("�������������_��������"));
				if(workshop != null)
					txt_workshop = (string)workshop.GetData("������������_���");

				// ������ ���������� ��������������/�����
				recomendations = new ArrayList();
				DbSqlCardRecomendation.SelectInArray(recomendations, (long)card.GetData("�����_��������"), (int)card.GetData("���_��������"));
			}
		}
		HeaderData	header_data = null;
		#endregion

		public DbPrintGivingout(long card_number, int card_year)
		{
			// ���������� ������������ ��� ������
			brush_standart		= new SolidBrush(Color.Black);
			font_small_bold		= new Font("Arial", 8, FontStyle.Bold);
			font_small			= new Font("Arial", 8);
			font_middle_bold	= new Font("Arial", 10, FontStyle.Bold);
			font_middle			= new Font("Arial", 10);
			pen_thin			= new Pen(brush_standart, 0.3F);

			// ������ ������� ���������� �� �������� (���� �� ��������)
			if(card_number != 0 && card_year != 0)
				// ����� �������� �� ������� ������
				card =	DbSqlCard.Find(card_number, card_year);
			else
				// �������� �������
				card = null;

			if(card != null)
			{
				// ������ ����� ��������, �������� ������ ��� ������
				header_data = new HeaderData(card);
			}
		}

		#region ������ ���������
		private int PrintHead(Graphics graph, int offset,  bool test, bool print, object o)
		{
			// ��������� ���������� ������ ���������
			SizeF size;
			int	height;
			int	y;
			string txt	= "";

			int	header_height_real	= 0;		// ������� ������������ ������ ���������
			int header_height		= 50;		// ������������� ������ ���������
			int text_offset			= 2;		// ����� ������ ������ ���������
			int header_line_height	= 4;		// ������ �����
			int	header_x			= 10;		// �������� ��������� �� �
			int	header_title_width	= 40;		// ������ ����� ��������� ��� ������������
			int	header_data_width	= 75;		// ������ ����� ��������� ��� ������
			if(test == true || print == false)
			{
				// ������ ������������ ������
				y = offset;
				y += 8;
				y += header_line_height;
				y += header_line_height;
				y += header_line_height;
				y += header_line_height;
				y += header_line_height;
				size	= MeasureTextBox(graph, header_data.txt_owner, header_data_width, text_offset, StringAlignment.Near, StringAlignment.Near, font_small_bold);
				height	= (int)Math.Ceiling(size.Height);
				if(height < header_line_height) height = header_line_height;
				y += height;
				size	= MeasureTextBox(graph, header_data.txt_address, header_data_width, text_offset, StringAlignment.Near, StringAlignment.Near, font_small_bold);
				height	= (int)Math.Ceiling(size.Height);
				if(height < header_line_height) height = header_line_height;
				y += height;
				y += header_line_height;
				y += header_line_height;
				return y + 3;	// ����� �� ���������
			}

			y	= offset;
			// �������� ���������
			PrintText(graph, "��� ������ ����������, � �����-������ �" + header_data.txt_warrant, header_x, y, 150, 8, StringAlignment.Near, StringAlignment.Center, font_middle_bold, brush_standart, false);
			y += 8;
			// ������ ���������
			// ������
			PrintTextNoBox(graph, "������:", header_x, y, header_title_width, header_line_height, text_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextNoBox(graph, header_data.txt_model, header_x + header_title_width, y, header_data_width, header_line_height, text_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			y += header_line_height;
			// ��������������� ����
			PrintTextBox(graph, "���. �����:", header_x, y, header_title_width, header_line_height, text_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, header_data.txt_sign, header_x + header_title_width, y, header_data_width, header_line_height, text_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			y += header_line_height;
			// VIN
			PrintTextBox(graph, "VIN:", header_x, y, header_title_width, header_line_height, text_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, header_data.txt_vin, header_x + header_title_width, y, header_data_width, header_line_height, text_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			y += header_line_height;
			// ���� �������
			PrintTextBox(graph, "���� �������:", header_x, y, header_title_width, header_line_height, text_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, header_data.txt_sell, header_x + header_title_width, y, header_data_width, header_line_height, text_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			y += header_line_height;
			// ������
			PrintTextBox(graph, "������:", header_x, y, header_title_width, header_line_height, text_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, header_data.txt_run, header_x + header_title_width, y, header_data_width, header_line_height, text_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			y += header_line_height;
			// ��������
			// ������� �������� �������, ����� ���������� �������� ������
			size	= MeasureTextBox(graph, header_data.txt_owner, header_data_width, text_offset, StringAlignment.Near, StringAlignment.Near, font_small_bold);
			height	= (int)Math.Ceiling(size.Height);
			if(height < header_line_height) height = header_line_height;
			PrintTextBox(graph, "��������:", header_x, y, header_title_width, height, text_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, header_data.txt_owner, header_x + header_title_width, y, header_data_width, height, text_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			y += height;
			// �����
			size	= MeasureTextBox(graph, header_data.txt_address, header_data_width, text_offset, StringAlignment.Near, StringAlignment.Near, font_small_bold);
			height	= (int)Math.Ceiling(size.Height);
			if(height < header_line_height) height = header_line_height;
			PrintTextBox(graph, "�����:", header_x, y, header_title_width, height, text_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, header_data.txt_address, header_x + header_title_width, y, header_data_width, height, text_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			y += height;
			// �������
			PrintTextBox(graph, "�������:", header_x, y, header_title_width, header_line_height, text_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, header_data.txt_phone, header_x + header_title_width, y, header_data_width, header_line_height, text_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			y += header_line_height;
			// ���������� ����
			PrintTextNoBox(graph, "���������� ����:", header_x, y, header_title_width, header_line_height, text_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextNoBox(graph, header_data.txt_represent, header_x + header_title_width, y, header_data_width, header_line_height, text_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			y += header_line_height;
			// ������� ������������ ������������� � ������������ ����������
			RectangleF rect1	= new RectangleF(header_x, offset + 8, header_data_width + header_title_width, y - (offset + 8));
			PrintRoundBox(graph, rect1, pen_thin, 4);
			PrintLineVrt(graph, header_x + header_title_width, offset + 8, y - (offset + 8), pen_thin, false);


			header_height_real = y;
			// ������� ������������ ������ (��� ���������� �����������)
			string file_name	= "";
			bool official		= false;
			switch(header_data.dialer)
			{
				case HeaderData.DIALER.kia:
					file_name	= "logo_kia.bmp";
					official	= true;
					break;
				case HeaderData.DIALER.lada:
					file_name	= "logo_lada.bmp";
					official	= true;
					break;
				case HeaderData.DIALER.chevrolet:
					file_name	= "logo_chevrolet.bmp";
					official	= true;
					break;
				case HeaderData.DIALER.unknown:
					file_name	= "logo_avto.bmp";
					break;
				default:
					file_name	= "logo_avto.bmp";
					break;
			}
			int image_x = header_title_width + header_data_width + header_x + 5;
			image_x = image_x + (200 - image_x) / 2;
			image_x = image_x - 39 / 2;
			PrintImage(graph, file_name, image_x, offset, 39, 20, false);
			if(official)
				PrintText(graph, "����������� �����", image_x, offset + 20, 39, 4, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, false);
			// ���� ���������� ������������ ������
			int radius					= 3;
			int addr_block_x			= header_title_width + header_data_width + header_x + 5;
			int addr_block_y			= offset + 20 + 4;
			int addr_block_width		= 200 - addr_block_x;
			int addr_block_height		= header_height_real - addr_block_y;
			int addr_block_line_height	= 4;
			RectangleF rect = new RectangleF(addr_block_x, addr_block_y , addr_block_width, addr_block_height);
			PrintRoundBox(graph, rect, pen_thin, radius);

			// ���������� ������ �� ������������ �����
			txt = FileIni.GetParameter("print.ini", "#ADDRESS_BLOCK");
			this.PrintTextNoBox(graph, txt, rect, 1, StringAlignment.Center, StringAlignment.Center, font_small, brush_standart, false);
			y += addr_block_height;

	//		y = addr_block_y + (addr_block_height - addr_block_line_height * 5) / 2;
	//		PrintText(graph, "��������� ����� \"����-1\"", addr_block_x + radius, y, addr_block_width - radius * 2, addr_block_line_height, StringAlignment.Center, StringAlignment.Center, font_small, brush_standart, false);
	//		y += addr_block_line_height;
	//		PrintText(graph, "���. (383) 332-02-92 ���� (383) 333-87-08", addr_block_x + radius, y, addr_block_width - radius * 2, addr_block_line_height, StringAlignment.Center, StringAlignment.Center, font_small, brush_standart, false);
	//		y += addr_block_line_height;
	//		PrintText(graph, "630058, �.�����������", addr_block_x + radius, y, addr_block_width - radius * 2, addr_block_line_height, StringAlignment.Center, StringAlignment.Center, font_small, brush_standart, false);
	//		y += addr_block_line_height;
	//		PrintText(graph, "�������������, ��. �������, 48", addr_block_x + radius, y, addr_block_width - radius * 2, addr_block_line_height, StringAlignment.Center, StringAlignment.Center, font_small, brush_standart, false);
	//		y += addr_block_line_height;
	//		PrintText(graph, "www.avto-1.ru", addr_block_x + radius, y, addr_block_width - radius * 2, addr_block_line_height, StringAlignment.Center, StringAlignment.Center, font_small, brush_standart, false);


			return header_height_real + 3;
		}
		#endregion

		#region ������ ���������� ����� � ���������
		private int PrintBlock1(Graphics graph, int offset,  bool test, bool print, object o)
		{
			// ��������������� �����������
			int y;
			int height_real;
			// ����������� ���������
			int b1x					= 10;
			int b1w					= 38;
			int b1h					= 4;
			int b1d					= 4;
			int b1to				= 1;
			int b2x					= 64;
			int b3x					= 120;

			if(test == true || print == false)
			{
				y = offset;
				y += b1h;
				y += b1h;
				y += b1h;
				y += b1h;
				y += b1h;
				y += 2;
				return y;
			}

			// ���� 1
			y		= offset;
			PrintText(graph, "��", b1x + b1w - 2, y, 8, b1h, StringAlignment.Center, StringAlignment.Center, font_middle, brush_standart, false);
			PrintText(graph, "���", b1x + b1w + b1h + b1d - 3, y, 12, b1h, StringAlignment.Center, StringAlignment.Center, font_middle, brush_standart, false);
			y += b1h;
			PrintCheckBox(graph, "��������� ������", b1x, y, b1w, b1h, b1to, 2, b1d, StringAlignment.Near, font_middle, brush_standart, pen_thin, false);
			y += b1h;
			PrintCheckBox(graph, "����������� �����", b1x, y, b1w, b1h, b1to, 2, b1d, StringAlignment.Near, font_middle, brush_standart, pen_thin, false);
			y += b1h;
			PrintCheckBox(graph, "������������� ��", b1x, y, b1w, b1h, b1to, 2, b1d, StringAlignment.Near, font_middle, brush_standart, pen_thin, false);
			y += b1h;
			PrintCheckBox(graph, "������� ��", b1x, y, b1w, b1h, b1to, 2, b1d, StringAlignment.Near, font_middle, brush_standart, pen_thin, false);
			y += b1h;
			y += 2;
			y += b1h;
			height_real = y;
			// ���� 2
			y = offset;
			PrintText(graph, "��", b2x + b1w - 2, y, 8, b1h, StringAlignment.Center, StringAlignment.Center, font_middle, brush_standart, false);
			PrintText(graph, "���", b2x + b1w + b1h + b1d - 3, y, 12, b1h, StringAlignment.Center, StringAlignment.Center, font_middle, brush_standart, false);
			y += b1h;
			PrintCheckBox(graph, "�������������", b2x, y, b1w, b1h, b1to, 2, b1d, StringAlignment.Near, font_middle, brush_standart, pen_thin, false);
			y += b1h;
			PrintCheckBox(graph, "������ ���������", b2x, y, b1w, b1h, b1to, 2, b1d, StringAlignment.Near, font_middle, brush_standart, pen_thin, false);
			y += b1h;
			PrintCheckBox(graph, "�������� �� �����", b2x, y, b1w, b1h, b1to, 2, b1d, StringAlignment.Near, font_middle, brush_standart, pen_thin, false);
			y += b1h;
			PrintCheckBox(graph, "������ ������������", b2x, y, b1w, b1h, b1to, 2, b1d, StringAlignment.Near, font_middle, brush_standart, pen_thin, false);
			y += b1h;

			// ���� 3
			y = offset;
			PrintText(graph, "������� ������� 0", b3x, y, 34, b1h, StringAlignment.Near, StringAlignment.Center, font_middle, brush_standart, false);
			RectangleF rect = new RectangleF(b3x + 34, y, 15, b1h);
			PrintBox(graph, rect, pen_thin);
			PrintText(graph, "1", b3x + 34 + 15, y, 8, b1h, StringAlignment.Near, StringAlignment.Center, font_middle, brush_standart, false);
			y += b1h;
			PrintText(graph, "������� ������������:", b3x, y, 42, b1h, StringAlignment.Near, StringAlignment.Center, font_middle, brush_standart, false);
			PrintCheckBox(graph, "��", b3x + 42, y, 10, b1h, 1, 1, 0, StringAlignment.Far, font_middle, brush_standart, pen_thin, false);
			PrintCheckBox(graph, "���", b3x + 42 + 15, y, 10, b1h, 1, 1, 0, StringAlignment.Far, font_middle, brush_standart, pen_thin, false);
			y += b1h;
			PrintText(graph, "������ ���������", b3x, y, 35, b1h, StringAlignment.Near, StringAlignment.Center, font_middle, brush_standart, false);
			y += b1h;
			PrintLineHor(graph, b3x + 35, y, 40, pen_thin, false);

			PrintText(graph, "��������� ��", b3x, y, 35, b1h, StringAlignment.Near, StringAlignment.Center, font_middle, brush_standart, false);
			y += b1h;
			PrintLineHor(graph, b3x + 35, y, 40, pen_thin, false);
			PrintText(graph, "(�����     ����      ������)", b3x + 35, y, 40, b1h, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
			y += b1h;
			PrintText(graph, "������� ��������������� �������:", b3x - 20, y, 85, b1h, StringAlignment.Near, StringAlignment.Center, font_middle, brush_standart, false);
			PrintCheckBox(graph, "��", b3x + 40, y, 10, b1h, 1, 1, 0, StringAlignment.Far, font_middle, brush_standart, pen_thin, false);
			PrintCheckBox(graph, "���", b3x + 40 + 15, y, 10, b1h, 1, 1, 0, StringAlignment.Far, font_middle, brush_standart, pen_thin, false);
			y += b1h;

			return height_real;
		}
		#endregion

		#region ������ ����� �������� ������
		private int PrintClaimBlockHead(Graphics graph, int offset,  bool test, bool print, object o)
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

			int	col1		=	10;
			int col2		=	page_width - offset_x_left - offset_x_right - col1;
			int	rowheight	= 6;

			if(test == true || print == false)
			{	
				y += title_height;
				y += rowheight;
				y += rowheight;
				y += rowheight;
				y += rowheight;
				return y;
			}

			// ���������
			text	= "��������� ������ ������������� � ����������";
			PrintText(graph, text, offset_x_left, y, page_width - offset_x_right - offset_x_left, title_height, StringAlignment.Near, StringAlignment.Center, font_middle_bold, brush_standart, false);
			y += title_height;
			// ������� ��� ������ �������

			// ������ �������
			PrintTextBox(graph, "1", offset_x_left, y, col1, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "��������� ������ �������� �����-������ �" + header_data.txt_warrant, offset_x_left + col1, y, col2, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			y += rowheight;
			PrintBox(graph, offset_x_left, y, col1, rowheight, pen_thin);
			PrintBox(graph, offset_x_left + col1, y, col2, rowheight, pen_thin);
			y += rowheight;
			PrintBox(graph, offset_x_left, y, col1, rowheight, pen_thin);
			PrintBox(graph, offset_x_left + col1, y, col2, rowheight, pen_thin);
			y += rowheight;
			PrintBox(graph, offset_x_left, y, col1, rowheight, pen_thin);
			PrintBox(graph, offset_x_left + col1, y, col2, rowheight, pen_thin);
			y += rowheight;

			return y;
		}
		private int PrintClaimBlock(Graphics graph, int offset,  bool test, bool print, object o)
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

			int	col1		=	10;
			int col2		=	page_width - offset_x_left - offset_x_right - col1;
			int	rowheight	= 8;
			
			if(o == null) return y;
			string txt;
			string txt1;
			DtCardClaim claim = (DtCardClaim)o;
			txt		= (string)claim.GetData("������������_������");
			txt1	= claim.GetData("�������").ToString();
			// ������ �����
			SizeF size = MeasureText(graph, txt, col2 - 2, StringAlignment.Near, StringAlignment.Center, font_middle);
			rowheight	= (int)Math.Ceiling(size.Height);
			if(test == true || print == false)
			{	
				return y + rowheight;
			}
			rect	= new RectangleF(offset_x_left, y, col1, rowheight);
			PrintTextBox(graph, txt1, offset_x_left, y, col1, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_middle, brush_standart, pen_thin, false);
			rect	= new RectangleF(offset_x_left + col1, y, col2, rowheight);
			PrintTextBox(graph, txt, offset_x_left + col1, y, col2, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_middle, brush_standart, pen_thin, false);
			y += rowheight;
			return y;
		}
		#endregion

		#region ���������� ����� �������� ������ ������� ���������
		private int PrintClaimBlank(Graphics graph, int offset,  bool test, bool print, object o)
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
			int row_count			= 5;
			int	col1		=	10;
			int col2		=	page_width - offset_x_left - offset_x_right - col1;
			int	rowheight		= 5;
			int	titlerowheight	= 7;

			// �������� ��������� ����������� ������
			int h = 0;
	//		h += this.PrintClaimAgreement(graph, 0, false, false, null);	// ������ ������������� � ����������
			h += this.PrintControl(graph, 0, false, false, null);		// ��������� ��������
			h += PrintFooter(graph, 0, false, false, null);				// ������

			// ��������� ������������ ���� ������
			int max_y = 0;
			if(y + h > page_max_y)
			{
				// ��������� �� ������
				// �������� �������� �� ����� ������� ���������
				max_y = page_max_y;
			}
			else
			{
				// �������� ������� �����, �� �������� ���������
				max_y = page_max_y - h;
			}
			if(test == true)
			{
				while(y + rowheight < max_y)
				{
					y += rowheight;
				}
				return y;
			}
			if(print == false)
			{
				while(y + rowheight < max_y)
				{
					y += rowheight;
				}
				return y;
			}

			// ������ �����
			while(y + rowheight < max_y)
			{
				rect	= new RectangleF(offset_x_left, y, col1, rowheight);
				PrintBox(graph, rect, pen_thin);
				rect	= new RectangleF(offset_x_left + col1, y, col2, rowheight);
				PrintBox(graph, rect, pen_thin);
				y += rowheight;
			}
			return y;
		}
		#endregion


		#region ������ ����� ������������
		private int PrintRecomendationBlockHead(Graphics graph, int offset,  bool test, bool print, object o)
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

			int	col1		=	10;
			int col2		=	page_width - offset_x_left - offset_x_right - col1;
			int	rowheight	= 8;

			if(test == true || print == false)
			{	
				y += title_height;
				y += rowheight;
				return y;
			}

			// ���������
			text	= "������������";
			PrintText(graph, text, offset_x_left, y, page_width - offset_x_right - offset_x_left, title_height, StringAlignment.Center, StringAlignment.Center, font_middle_bold, brush_standart, false);
			y += title_height;
			// ������� ��� ������ �������

			

			// ������ �������
			PrintTextBox(graph, "� �/�", offset_x_left, y, col1, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "������������ ���������", offset_x_left + col1, y, col2, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			y += rowheight;

			return y;
		}
		private int PrintRecomendationBlock(Graphics graph, int offset,  bool test, bool print, object o)
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

			int	col1		=	10;
			int col2		=	page_width - offset_x_left - offset_x_right - col1;
			int	rowheight	= 8;
			
			if(o == null) return y;
			string txt;
			string txt1;
			DtCardRecomendation recomendation = (DtCardRecomendation)o;
			txt		= (string)recomendation.GetData("������������");
			txt1	= recomendation.GetData("�����_������������").ToString();
			// ������ �����
			SizeF size = MeasureText(graph, txt, col2 - 2, StringAlignment.Near, StringAlignment.Center, font_middle);
			rowheight	= (int)Math.Ceiling(size.Height);
			if(test == true || print == false)
			{	
				return y + rowheight;
			}
			rect	= new RectangleF(offset_x_left, y, col1, rowheight);
			PrintTextBox(graph, txt1, offset_x_left, y, col1, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_middle, brush_standart, pen_thin, false);
			rect	= new RectangleF(offset_x_left + col1, y, col2, rowheight);
			PrintTextBox(graph, txt, offset_x_left + col1, y, col2, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_middle, brush_standart, pen_thin, false);
			y += rowheight;
			return y;
		}
		#endregion

		#region ������ ����� ��������� ������ ��������
		private int PrintControl(Graphics graph, int offset,  bool test, bool print, object o)
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
			int row_count			= 5;
			int	col1		=	10;
			int col2		=	page_width - offset_x_left - offset_x_right - col1;
			int	rowheight		= 5;
			int	titlerowheight	= 7;

			if(test == true)
			{
				// ��� ������������ ��������� ��������� �� �������� ��� � �������
				y	+= title_height;	// ���������
				for(int j = 0; j < row_count; j++)
				{
					y += rowheight;
				}
				y	+= PrintFooter(graph, 0, true, false, null);	// ������
				return y;
			}
			if(print == false)
			{
				// ��� ������������ ��������� ��������� �� �������� ��� � �������
				y	+= title_height;	// ���������
				for(int j = 0; j < row_count; j++)
				{
					y += rowheight;
				}
				return y;
			}

			// ���������
			text	= "��������� ����������� ������ �������� / ������ �����������";
			PrintText(graph, text, offset_x_left, y, page_width - offset_x_right - offset_x_left, title_height, StringAlignment.Center, StringAlignment.Center, font_middle_bold, brush_standart, false);
			y += title_height;
			// �������
			// ������ �����
			for(int j = 0; j < row_count; j++)
			{
				rect	= new RectangleF(offset_x_left, y, col1, rowheight);
				PrintBox(graph, rect, pen_thin);
				rect	= new RectangleF(offset_x_left + col1, y, col2, rowheight);
				PrintBox(graph, rect, pen_thin);
				y += rowheight;
			}
			return y;
		}
		#endregion


		#region ������ ������������ �����
		private int PrintFooter(Graphics graph, int offset,  bool test, bool print, object o)
		{
			// ��������������� �����������
			int y		= offset;
			string text	= "";
			// ����������� ���������
			int h1					= 6;	// ������ ��� ������ ������
			int w1					= 40;
			int w2					= 30;
			int w3					= 30;
			int d1					= 5;
			int w6					= 140;
			int w7					= 60;

			int offset_x_left		= 10;
			int offset_x_right		= 10;
			int page_width			= 210;
			

			if(test == true || print == false)
			{	
				y += 3;
				y += 3;
				y += h1;
				y += h1;
				y += h1;
				y += h1;
				y += h1;
				return y;
			}

			// ������� ��� ������� �����
			RectangleF rect = new RectangleF(w6, y + 6 + h1, w7, h1 * 4);
			text = "��������";
			if (header_data.inner == true)
				text = "����������";
			if (header_data.cashless == true)
				text = "������������";
			PrintTextNoBox(graph, text, w6, y + 6 + h1, w7, 4, 2, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintRoundBox(graph, rect, pen_thin, 5);

			// ������� �� �����
			y += 3;
			PrintLineHor(graph, offset_x_left, y, page_width - offset_x_left -  offset_x_right, pen_thin, false);
			text	= "����� ������                   ����� ������              ����� ������";
			PrintText(graph, text, offset_x_left, y - 3, page_width - offset_x_left - offset_x_right, 6, StringAlignment.Center, StringAlignment.Center, font_small, brush_standart, false);
			y += 3;

			text	= "������� �� ����� �� ��������: " + header_data.txt_card + "  (" + header_data.txt_workshop + ")";
			PrintText(graph, text, offset_x_left, y, page_width, h1, StringAlignment.Near, StringAlignment.Center, font_middle_bold, brush_standart, false);
			text	= DateTime.Now.ToString();
			PrintText(graph, text, offset_x_left, y, page_width - offset_x_left - offset_x_right, h1, StringAlignment.Far, StringAlignment.Center, font_middle_bold, brush_standart, false);
			y += h1;
			text	= "�����-����� �: " + header_data.txt_warrant;
			PrintText(graph, text, offset_x_left, y, page_width, h1, StringAlignment.Near, StringAlignment.Center, font_middle_bold, brush_standart, false);
			y += h1;
			text	= "���������� " + header_data.txt_model + " VIN: " + header_data.txt_vin + " ���. ����: " + header_data.txt_sign;
			PrintText(graph, text, offset_x_left, y, page_width, h1, StringAlignment.Near, StringAlignment.Center, font_middle_bold, brush_standart, false);
			y += h1;
			text	= "C�����-�����������";
			PrintText(graph, text, offset_x_left, y, w1, h1, StringAlignment.Near, StringAlignment.Far, font_middle, brush_standart, false);
			y += h1;
			PrintLineHor(graph, offset_x_left + w1, y, w2, pen_thin, false);
			PrintLineHor(graph, offset_x_left + w1 + w2 + d1, y, w3, pen_thin, false);
			text	= "(�������)";
			PrintText(graph, text, offset_x_left + w1, y, w2, h1, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
			text	= "(���������)";
			PrintText(graph, text, offset_x_left + w1 + w2 + d1, y, w3, h1, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
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
			offset = PrintBlock(graph, offset, new DelegatePrintBlock(PrintHead), null);
			offset = PrintBlock(graph, offset, new DelegatePrintBlock(PrintBlock1), null);
			offset = PrintBlock(graph, offset, new DelegatePrintBlock(PrintClaimBlockHead), null);
			bool first = true;
			long counter = 1;
			if(header_data.recomendations.Count > 0)
			{
				foreach(DtCardRecomendation element in header_data.recomendations)
				{
					element.SetData("�����_������������", counter);
					offset = PrintBlockWithHeader(graph, offset, new DelegatePrintBlock(PrintRecomendationBlock), element, new DelegatePrintBlock(PrintRecomendationBlockHead), first);
					first = false;
					counter++;
				}
			}
			else
			{
				offset = PrintBlockWithHeader(graph, offset, new DelegatePrintBlock(PrintClaimBlock), null, new DelegatePrintBlock(PrintRecomendationBlockHead), first);
			}
//			offset = PrintBlock(graph, offset, new DelegatePrintBlock(PrintClaimBlank), null);
//			offset = PrintBlock(graph, offset, new DelegatePrintBlock(PrintClaimAgreement), null);
			offset = PrintBlock(graph, offset, new DelegatePrintBlock(PrintControl), null);
			offset = PrintFooter(graph, offset, new DelegatePrintBlock(PrintFooter), null);
		}
	}
}
