using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Windows.Forms;


namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbPrintGuarantyKIA.
	/// </summary>
	public class DbPrintGuarantyKIA:DbPrint
	{
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
			public string txt_engine	= "";
			public string txt_owner		= "";
			public string txt_address	= "";
			public string txt_phone		= "";
			public string txt_represent	= "";
			public string txt_sell		= "";
			public string txt_run		= "";
			public string txt_card		= "";
			public string txt_workshop	= "";
			public string txt_card_number		= "";
			public string txt_warrant_number	= "";
			public string txt_warrant_close		= "";
			public string txt_warrant_open		= "";
			public string txt_card_open			= "";
			public DIALER dialer		= DIALER.unknown;

			public ArrayList	claims	= null;

			public HeaderData(DtCard card)
			{
				// ������ ��������
				txt_card = card.GetData("�����_��������").ToString() + "/" + card.GetData("���_��������").ToString();
				txt_card_number = card.GetData("�����_��������").ToString();

				// ������ ��������
				txt_card_open			= card.GetData("����").ToString();
				if((long)card.GetData("�����_�����_��������") != 0)
				{
					txt_warrant_open	= card.GetData("����_�����_������_��������").ToString();
					txt_warrant_number	= card.GetData("�����_�����_��������").ToString();
				}
				if((short)card.GetData("������_��������") == 2)
				{
					txt_warrant_close = card.GetData("����_�����_������_��������").ToString();
				}
				int run = (int)card.GetData("������_��������");
				if(run != 0)
				{
					txt_run		= run.ToString();
				}
				// ������ �� ��������� ������ ��������
				if(MessageBox.Show("������ ������ �������?", "������", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					// ��������� ������
					FormSelectDataGuaranty dialog = new FormSelectDataGuaranty();
					// ��������� ��������� �������
					dialog.number		= txt_card_number;
					dialog.time_request	= (DateTime)card.GetData("����");
					dialog.time_begin	= (DateTime)card.GetData("����_�����_������_��������");
					dialog.time_end		= (DateTime)card.GetData("����_�����_������_��������");
					if(dialog.ShowDialog() == DialogResult.OK)
					{
						txt_card_number			= dialog.number;
						txt_card_open			= dialog.TimeRequest.ToString();
						txt_warrant_open		= dialog.TimeBegin.ToString();
						txt_warrant_close		= dialog.TimeEnd.ToString();
					}
				}
				// �������� ������������ ������ ���������
				DtAuto auto = DbSqlAuto.Find((long)card.CodeAuto/*GetData("����������_��������")*/);
				if(auto != null)
				{
					txt_sign	= (string)auto.GetData("�����_����");
					txt_vin		= (string)auto.GetData("VIN");

					// ���� ���� VIN ������-������������, �������� ��� � �������
					string txt_vin_origin = (string)auto.GetData("VIN_�������������");
					if (txt_vin_origin != "")
						txt_vin	+= " (" + txt_vin_origin + ")";

					txt_engine	= (string)auto.GetData("�����_���������");
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
				claims = new ArrayList();
				DbSqlCardClaim.SelectInArray(claims, (long)card.GetData("�����_��������"), (int)card.GetData("���_��������"));
			}
		}
		HeaderData	header_data = null;
		#endregion

		public DbPrintGuarantyKIA(long card_number, int card_year)
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
			int x1					= 50;
			int x2					= 30;
			if(test == true || print == false)
			{
				// ������ ������������ ������
				y = offset;
				y += 10;
				y += header_line_height;
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
			PrintText(graph, "�����-����� �" + header_data.txt_card_number, header_x, y, 150, 5, StringAlignment.Near, StringAlignment.Center, font_middle_bold, brush_standart, false);
			PrintText(graph, "���� ���������:", header_x + x1 - 50, y, 70, 5, StringAlignment.Far, StringAlignment.Center, font_small, brush_standart, false);
			PrintText(graph, header_data.txt_card_open, header_x + x1 + x2, y, 150, 5, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, false);
			y += 3;
			PrintText(graph, "������/���� �������� ���������� � ������:", header_x + x1 - 50, y, 70, 5, StringAlignment.Far, StringAlignment.Center, font_small, brush_standart, false);
			PrintText(graph, header_data.txt_warrant_open, header_x + x1 + x2, y, 150, 5, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, false);
			y += 3;
			PrintText(graph, "������/���� ���������� �������:", header_x + x1 - 50, y, 70, 5, StringAlignment.Far, StringAlignment.Center, font_small, brush_standart, false);
			PrintText(graph, header_data.txt_warrant_close, header_x + x1 + x2, y, 150, 5, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, false);
			y += 4;
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
			// ����� ���������
			PrintTextBox(graph, "��������� �:", header_x, y, header_title_width, header_line_height, text_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, header_data.txt_engine, header_x + header_title_width, y, header_data_width, header_line_height, text_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
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
			RectangleF rect1	= new RectangleF(header_x, offset + 10, header_data_width + header_title_width, y - (offset + 10));
			PrintRoundBox(graph, rect1, pen_thin, 4);
			PrintLineVrt(graph, header_x + header_title_width, offset + 10, y - (offset + 10), pen_thin, false);


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
			txt = FileIni.GetParameter("print.ini", "#ADDRESS_BLOCK_KIA");
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

		#region ������ ������������ �����
		private int PrintFooter(Graphics graph, int offset,  bool test, bool print, object o)
		{
			// ��������������� �����������
			RectangleF rect;
			int y		= offset;
			string text	= "";
			string txt	= "";
			SizeF size;
			// ����������� ���������
			int offset_x_left			= 10;
			int offset_x_right			= 10;
			int page_width				= 210;
			int title_height			= 4;
			int page_height				= 297 - 10;
			int recomendations_height	= 0;

			int	rowheight	= 4;
			int w1			= 190;
			int w2			= 46;
			int w3			= 15;
			int w4			= 25;
			int w5			= 15;
			int w6			= 105;
			int w7			= 37;
			int w8			= 15;
			int w9			= 25;

			y += 3;

			txt = "�� �������� ����� �������������� ��� ����������� ������� ���������� ���������������� �������� ������-������������, ������� �������� ������������ � ���������� ������������ ����� �� ����������. ����� � ������� �������� �� ���������� ��������� � ����������� ������.";
			size = this.MeasureText(graph, txt, w1 - 4, StringAlignment.Center, StringAlignment.Center, font_small);

			if(test == true || print == false)
			{
				y += 2;
				y += rowheight;
				y += rowheight;
				y += rowheight;
				y += rowheight;
				y += rowheight;
				y += 1;
				y += (int)Math.Ceiling(size.Height);
				y += 1;
				y += rowheight * 2;
				y += rowheight;
				y += rowheight;
				y += rowheight * 2;
				y += rowheight;

				y += 10;
				return y;
			}

			// ������������ ������ �������
			rect = new RectangleF(offset_x_left, y, w1, rowheight * 5 + 2);
			y += 2;
			txt = "���� ������� ������� �� _________ (���-�� ����) �� ������� ___________________________________";
			PrintText(graph, txt, offset_x_left, y, w1, rowheight, StringAlignment.Near, StringAlignment.Center, font_middle, brush_standart, false);
			y += rowheight;
			PrintText(graph, "������������ ���� ������� ���������� 45 ����", offset_x_left, y, w1, rowheight, StringAlignment.Near, StringAlignment.Center, font_middle, brush_standart, false);
			y += rowheight * 2;
			PrintText(graph, "��������/�������������", offset_x_left, y, w2, rowheight, StringAlignment.Near, StringAlignment.Center, font_middle, brush_standart, false);
			PrintText(graph, "������-�����������", offset_x_left + w6, y, w7, rowheight, StringAlignment.Near, StringAlignment.Center, font_middle, brush_standart, false);
			y += rowheight;
			PrintLineHor(graph, offset_x_left + w2, y, w3 + w4 + w5, pen_thin, false);
			PrintText(graph, "�������", offset_x_left + w2, y, w3, rowheight, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
			PrintText(graph, "�����������", offset_x_left + w2 + w3, y, w4, rowheight, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
			PrintText(graph, "����", offset_x_left + w2 + w3 + w4, y, w5, rowheight, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
			PrintLineHor(graph, offset_x_left + w6 + w7, y, w8 + w9, pen_thin, false);
			PrintText(graph, "�������", offset_x_left + w6 + w7, y, w8, rowheight, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
			PrintText(graph, "�����������", offset_x_left + w6 + w7 + w8, y, w9, rowheight, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
			PrintRoundBox(graph, rect, pen_thin, 2);
			y += rowheight;
			y += 1;
			txt = "�� �������� ����� �������������� ��� ����������� ������� ���������� ���������������� �������� ������-������������, ������� �������� ������������ � ���������� ������������ ����� �� ����������. ����� � ������� �������� �� ���������� ��������� � ����������� ������.";
			rect = new RectangleF(offset_x_left, y, w1, (int)Math.Ceiling(size.Height));
			PrintTextNoBox(graph, txt, rect.X, rect.Y, rect.Width, rect.Height, 2, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, pen_thin, false);
			PrintRoundBox(graph, rect, pen_thin, 2);
			y += (int)Math.Ceiling(size.Height);
			y += 1;
			txt = "���������� ������� ���������/�������������, ����� � �������� ����������� ����� ���������.";
			PrintText(graph, txt, offset_x_left, y, w1, rowheight, StringAlignment.Near, StringAlignment.Center, font_middle, brush_standart, false);
			y += rowheight * 2;
			PrintText(graph, "������-�����������", offset_x_left, y, w7, rowheight, StringAlignment.Near, StringAlignment.Center, font_middle, brush_standart, false);
			PrintText(graph, "������� �� ��������", offset_x_left + w6 - 10, y, w2, rowheight, StringAlignment.Near, StringAlignment.Center, font_middle, brush_standart, false);
			y += rowheight;
			PrintLineHor(graph, offset_x_left + w7, y, w3 + w4, pen_thin, false);
			PrintText(graph, "�������", offset_x_left + w7, y, w3, rowheight, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
			PrintText(graph, "�����������", offset_x_left + w7 + w3, y, w4, rowheight, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
			PrintLineHor(graph, offset_x_left + w6 - 10 + w7, y, w3 + w4, pen_thin, false);
			PrintText(graph, "�������", offset_x_left + w6 - 10 + w7, y, w3, rowheight, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
			PrintText(graph, "�����������", offset_x_left + w6 - 10 + w7 + w3, y, w4, rowheight, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
			y += rowheight;
			txt = "���������� ����� ������� ������� � ���������� ��������� ���������, ��������� �� ����.";
			PrintText(graph, txt, offset_x_left, y, w1, rowheight, StringAlignment.Near, StringAlignment.Center, font_middle, brush_standart, false);
			y += rowheight * 2;
			PrintText(graph, "��������/�������������", offset_x_left, y, w2, rowheight, StringAlignment.Near, StringAlignment.Center, font_middle, brush_standart, false);
			y += rowheight;
			PrintLineHor(graph, offset_x_left + w2, y, w3 + w4 + w5, pen_thin, false);
			PrintText(graph, "�������", offset_x_left + w2, y, w3, rowheight, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
			PrintText(graph, "�����������", offset_x_left + w2 + w3, y, w4, rowheight, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
			PrintText(graph, "����", offset_x_left + w2 + w3 + w4, y, w5, rowheight, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);

			y += 10;
			return y;
		}
		#endregion

		public override void  PrintPage(Graphics graph, int page)
		{
			// ��� ���������� �� ��������
			int offset = 0;

			offset = page_min_y;
			offset = PrintBlock(graph, offset, new DelegatePrintBlock(PrintHead), null);
			offset = PrintFooter(graph, offset, new DelegatePrintBlock(PrintFooter), null);
		}
	}
}
