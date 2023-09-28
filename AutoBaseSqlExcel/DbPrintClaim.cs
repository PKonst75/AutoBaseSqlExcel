using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// ������ ����� ������ �������, ������������.
	/// </summary>
	public class DbPrintClaim:DbPrint
	{
		// ����������� ��� ������
		SolidBrush	brush_standart;
		Font		font_small_bold;
		Font		font_middle_bold;
		Font		font_small;
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
            public string txt_mail      = "";
			public string txt_represent	= "";
			public string txt_sell		= "";
			public string txt_run		= "";
			public string txt_card		= "";
			public DIALER dialer		= DIALER.unknown;

            public string txt_recomendations = "";
            public ArrayList recomendations = null;

			public HeaderData(DtCard card)
			{
				// ������ ��������
				txt_card = card.GetData("�����_��������").ToString() + "/" + card.GetData("���_��������").ToString();
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
                    txt_mail        = owner.GetMail();
				}
				// �������������
				DtPartner represent = DbSqlPartner.Find((long)card.GetData("�������������_��������"));
				if(represent != null)
					txt_represent	= represent.GetTitle();

                // ������ ������������
                recomendations = new ArrayList();
                DbSqlCardRecomendation.SelectInArray(recomendations, (long)card.GetData("�����_��������"), (int)card.GetData("���_��������"));
                foreach (object o in recomendations)
                {
                    DtCardRecomendation recomendation = (DtCardRecomendation)o;
                    string s = (string)recomendation.GetData("������������");
                    txt_recomendations += s;
                    txt_recomendations += "; ";
                }
			}
		}
		HeaderData	header_data = null;
		#endregion

		public DbPrintClaim(long card_number, int card_year)
		{
			// ���������� ������������ ��� ������
			brush_standart		= new SolidBrush(Color.Black);
			font_small_bold		= new Font("Arial", 8, FontStyle.Bold);
			font_small			= new Font("Arial", 8);
			font_middle_bold	= new Font("Arial", 10, FontStyle.Bold);
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
			PrintText(graph, "������ �������, � �������� �" + header_data.txt_card, header_x, y, 150, 8, StringAlignment.Near, StringAlignment.Center, font_middle_bold, brush_standart, false);
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

		#region ������ ���� ������
		private int PrintBody(Graphics graph, int offset,  bool test, bool print, object o)
		{
			// ��������������� �����������
			RectangleF rect;
			int y		= offset;
			string text	= "";
            SizeF size;
			// ����������� ���������
			int offset_x_left		= 10;
			int offset_x_right		= 10;
			int page_width			= 210;
			int title_height		= 4;
			int page_height			= 297 - 10;

            int recomendations_height = 0;

			// ���������
			text	= "������ ��������� �� ���������� �����";
			PrintText(graph, text, offset_x_left, y, page_width - offset_x_right - offset_x_left, title_height, StringAlignment.Center, StringAlignment.Center, font_middle_bold, brush_standart, false);
			y += title_height;
			text	= "(����������� ����������)";
			PrintText(graph, text, offset_x_left, y, page_width - offset_x_right - offset_x_left, title_height, StringAlignment.Center, StringAlignment.Center, font_middle_bold, brush_standart, false);
			y += title_height;
			// ������� ��� ������ �������

            if (header_data.txt_recomendations != "")
            {
                size = this.MeasureText(graph, header_data.txt_recomendations, 190 - 4, StringAlignment.Near, StringAlignment.Center, font_small);
                recomendations_height = (int)Math.Ceiling(size.Height);
                size = this.MeasureText(graph, "������������ �������� �����-������ :", 190 - 4, StringAlignment.Near, StringAlignment.Center, font_small_bold);
                recomendations_height += (int)Math.Round(size.Height);
            }
            if (recomendations_height != 0)
            {
                rect = new RectangleF(offset_x_left, y, 190, recomendations_height);
                PrintTextNoBox(graph, "������������ �������� �����-������ :", rect.X, rect.Y, rect.Width, rect.Height, 2, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, pen_thin, false);
                SizeF size1 = this.MeasureText(graph, "������������ �������� �����-������ :", 190 - 4, StringAlignment.Near, StringAlignment.Near, font_small_bold);
                PrintTextNoBox(graph, header_data.txt_recomendations, rect.X, rect.Y + (int)Math.Ceiling(size1.Height), rect.Width, rect.Height, 2, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, pen_thin, false);
                PrintRoundBox(graph, rect, pen_thin, 2);
                y += recomendations_height;
                y += 1;
            }

			int	col1		=	10;
			int col2		=	page_width - offset_x_left - offset_x_right - col1;
			int	rowheight	= 8;

			// ������ �������
			PrintTextBox(graph, "� �/�", offset_x_left, y, col1, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "���������� ������������� / ������", offset_x_left + col1, y, col2, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			y += rowheight;
			// ������ �����
			int footer_height = PrintFooter(graph, 0, true, false, null);	// �������� ������ �������
			int last_valid_y = page_height - footer_height - rowheight;
			while(y < last_valid_y)
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

		#region ������ ������� ������
		private int PrintFooter(Graphics graph, int offset,  bool test, bool print, object o)
		{
			// ��������������� �����������
			int y;
			string text	= "";
			// ����������� ���������
			int offset_x_left		= 10;
			int height1				= 5;
			int pos1				= 30;
			int width1				= 30;
			int length1				= 120;
			int offset_y_1			= 10;
            int width2 = 190;
            int pos2 = 5;
            int width3 = 70;

			if(test == true || print == false)
			{
				y		= offset + offset_y_1;
				y += height1;
				y += height1;
                y += height1;   // ������ ������� ��������� ������� � �����

                // ������ �������������� ������������!
                y += height1;
                y += height1;
                y += height1;
                y += height1;
                y += height1;
                y += height1;
                y += height1;
                y += height1;
                y += height1;
                y += height1;
                y += height1;
                y += height1;
                y += height1;
                y += height1;

				return y;
			}

            // ������� � ����������� �����

			// ���������
			y		= offset + offset_y_1;
            string tmps = header_data.txt_phone;
            if (tmps == "") tmps = "__________________";
            string tmps2 = header_data.txt_mail;
            if (tmps2 == "") tmps2 = "___________________";
            text = "���������, ����������, ������� : _" + tmps + "_ � e-mail : _" + tmps2 + "_";
            PrintText(graph, text, offset_x_left, y-5, 180, height1, StringAlignment.Near, StringAlignment.Far, font_middle_bold, brush_standart, false);
            y += height1;
			text	= "��������";
			PrintText(graph, text, offset_x_left + pos1, y, width1, height1, StringAlignment.Far, StringAlignment.Far, font_middle_bold, brush_standart, false);
			PrintLineHor(graph, offset_x_left + pos1 + width1, y + height1, length1, pen_thin , false);
			y += height1;
			text	= "         (����)                        (�������)                                                        (���������)";
			PrintText(graph, text, offset_x_left + pos1 + width1, y, length1, height1, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			y += height1;

            // ������ �������������� ������������!
            PrintText(graph, "��������, ����������!", offset_x_left + pos2, y, width2, height1, StringAlignment.Near, StringAlignment.Far, font_middle_bold, brush_standart, false);
            y += height1;

            PrintText(graph, "���������� ��������� �� ��������:", offset_x_left + pos2, y, width2, height1, StringAlignment.Near, StringAlignment.Far, font_middle_bold, brush_standart, false);
            y += height1;
            PrintText(graph, "* ��, ���� ��������� ������ � ����������� �����", offset_x_left + pos1, y, width2, height1, StringAlignment.Near, StringAlignment.Far, font_middle_bold, brush_standart, false);
            y += height1;
            PrintText(graph, "* ��, �� ����������: ��������� ������ / ����������� ����� / � ������ � �����", offset_x_left + pos1, y, width2, height1, StringAlignment.Near, StringAlignment.Far, font_middle_bold, brush_standart, false);
            y += height1; 
            PrintText(graph, "* ���", offset_x_left + pos1, y, width2, height1, StringAlignment.Near, StringAlignment.Far, font_middle_bold, brush_standart, false);
            y += height1;


            PrintText(graph, "� �������� ������� ��������� �������:", offset_x_left + pos2, y, width2, height1, StringAlignment.Near, StringAlignment.Far, font_middle_bold, brush_standart, false);
            y += height1;
            PrintText(graph, "* � ����������", offset_x_left + pos1, y, width2, height1, StringAlignment.Near, StringAlignment.Far, font_middle_bold, brush_standart, false);
            y += height1;
            PrintText(graph, "* ������������ ���� �� ��������� ������� �� _______���.______ ���.", offset_x_left + pos1, y, width2, height1, StringAlignment.Near, StringAlignment.Far, font_middle_bold, brush_standart, false);
            y += height1;
           


            PrintText(graph, "���������� �������� ����� �����:", offset_x_left + pos2, y, width2, height1, StringAlignment.Near, StringAlignment.Far, font_middle_bold, brush_standart, false);
            y += height1;
            PrintText(graph, "* ������� (�� ����������� ���������� �� ��������)", offset_x_left + pos1, y, width2, height1, StringAlignment.Near, StringAlignment.Far, font_middle_bold, brush_standart, false);
            y += height1;
            PrintText(graph, "* ������������� ����� ������������", offset_x_left + pos1, y, width2, height1, StringAlignment.Near, StringAlignment.Far, font_middle_bold, brush_standart, false);
            y += height1;
            PrintText(graph, "* �������������", offset_x_left + pos1, y, width2, height1, StringAlignment.Near, StringAlignment.Far, font_middle_bold, brush_standart, false);
            y += height1;

            text = "����� �����������";
            PrintText(graph, text, offset_x_left + pos1, y, width3, height1, StringAlignment.Far, StringAlignment.Far, font_middle_bold, brush_standart, false);
            PrintLineHor(graph, offset_x_left + pos1 + width3, y + height1, width3, pen_thin, false);
            y += height1;
            text = "                                                              (�������)";
            PrintText(graph, text, offset_x_left + pos1 + width1, y, length1, height1, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
            y += height1;

			return y;
		}
		#endregion

		// �������� ��������� ������
		public override void  PrintPage(Graphics graph, int page)
		{
			// ��� ���������� �� ��������
			int offset = 0;

			offset = 10;
			offset = PrintBlock(graph, offset, new DelegatePrintBlock(PrintHead), null);
			offset = PrintBlock(graph, offset, new DelegatePrintBlock(PrintBody), null);
			offset = PrintBlock(graph, offset, new DelegatePrintBlock(PrintFooter), null);
		}
	}
}
