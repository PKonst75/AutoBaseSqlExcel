using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbPrintDiagnosticList.
	/// </summary>
	public class DbPrintDiagnosticList:DbPrint
	{
		// ����������� ��� ������
		SolidBrush	brush_standart;
		SolidBrush	brush_lightgray;
		Font		font_small_bold;
		Font		font_middle_bold;
		Font		font_middle;
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
			public string txt_represent	= "";
			public string txt_sell		= "";
			public string txt_run		= "";
			public string txt_card		= "";
			public string txt_warrant	= "";
			public string txt_warrant_close		= "";
			public string txt_summ_work			= "";
			public string txt_summ_work_pay		= "";
			public string txt_discount_work		= "";
			public string txt_summ_detail		= "";
			public string txt_summ_detail_pay	= "";
			public string txt_summ_pay			= "";
			public string txt_recomendations	= "";
			public DIALER dialer		= DIALER.unknown;
	
			public ArrayList	works			= null;
			public ArrayList	details			= null;
			public ArrayList	recomendations	= null;
			public bool			cashless		= false;
			public bool			inner			= false;

			public float	count_nv	= 0.0F;
			public int		count_sp	= 0;

			public HeaderData(DtCard card)
			{
				// ������ ��������
				txt_card = card.GetData("�����_��������").ToString() + "/" + card.GetData("���_��������").ToString();
				if((long)card.GetData("�����_�����_��������") != 0)
				{
					txt_warrant = card.GetData("�����_�����_��������").ToString() + " �� " + card.GetData("����_�����_������_��������").ToString();
				}
				if((short)card.GetData("������_��������") == 2)
				{
					txt_warrant_close = card.GetData("����_�����_������_��������").ToString();
				}
				if((bool)card.GetData("�����������_��������") == true)
				{
					cashless = true;
				}
				if((bool)card.GetData("����������_��������") == true)
				{
					inner = true;
				}
				int run = (int)card.GetData("������_��������");
				if(run != 0)
				{
					txt_run		= run.ToString();
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

				// ������ ����� � �����-������
				works = new ArrayList();
				DbSqlCardWork.SelectInArray(card, works);
				float summ_work = 0.0F;
				foreach(DtCardWork element in works)
				{
					if((bool)element.GetData("��������_��������_������") == false)
					{
						float nv = (float)element.GetData("������������_��������_������");
						if(nv != 0.0F)
						{
							summ_work += nv * (float)element.GetData("��������_��������_������") * (float)element.GetData("����������_��������_������");
						}
						else
						{
							summ_work += (float)element.GetData("��������_��������_������") * (float)element.GetData("����������_��������_������");
						}
					}
				}

				// ������ ������� � �����-������
				details = new ArrayList();
				DbSqlCardDetail.SelectInArray(card, details);
				float summ_oil = 0.0F;
				float summ_detail = 0.0F;
				foreach(DtCardDetail element in details)
				{
					if((bool)element.GetData("��������_��������_������") == true)
					{
						if((bool)element.GetData("��������_��������_������") == false)
						{
							summ_oil += (float)element.GetData("����������_��������_������") * (float)element.GetData("����_��������_������");
						}
					}
					else
					{
						if((bool)element.GetData("��������_��������_������") == false)
						{
							summ_detail += (float)element.GetData("����������_��������_������") * (float)element.GetData("����_��������_������");
						}
					}
				}

				// ������ ������������
				recomendations = new ArrayList();
				DbSqlCardRecomendation.SelectInArray(recomendations, (long)card.GetData("�����_��������"), (int)card.GetData("���_��������"));
				foreach(object o in recomendations)
				{
					DtCardRecomendation recomendation = (DtCardRecomendation)o;
					string s = (string)recomendation.GetData("������������");
					txt_recomendations += s;
					txt_recomendations += "; ";
				}

				// ������ �� ����� �����, � ������ ������
				float discount_work		= 0.0F;
				float summ_work_pay		= 0.0F;
				float summ_work_oil		= 0.0F;
				float discount_val		= 0.0F;
				discount_val	= (float)card.GetData("������_������_��������");
				if(discount_val != 0.0F)
				{
					discount_work = (float)Math.Round((float)(summ_work / 100 * discount_val), 0);
				}
				summ_work_pay		= summ_oil + summ_work - discount_work;
				summ_work_oil		= summ_oil + summ_work;
				txt_summ_work		= Db.CachToTxt(Math.Round(summ_work_oil, 2));
				txt_discount_work	= Db.CachToTxt(Math.Round(discount_work, 2));
				txt_summ_work_pay	= Db.CachToTxt(Math.Round(summ_work_pay, 2));
				txt_summ_detail		= Db.CachToTxt(Math.Round(summ_detail, 2));
				txt_summ_detail_pay	= Db.CachToTxt(Math.Round(summ_detail, 2));
				txt_summ_pay		= Db.CachToTxt(Math.Round(summ_detail + summ_work_pay, 2));
			}
		}
		HeaderData	header_data = null;
		#endregion

		public DbPrintDiagnosticList(long card_number, int card_year)
		{
			// ���������� ������������ ��� ������
			brush_standart		= new SolidBrush(Color.Black);
			brush_lightgray		= new SolidBrush(Color.LightGray);
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
				y += 6;
				y += 6;
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
			PrintText(graph, "���� ����������� ���������", header_x, y, 150, 5, StringAlignment.Near, StringAlignment.Center, font_middle_bold, brush_standart, false);
			y += 5;
			PrintText(graph, DateTime.Now.ToShortDateString(), header_x, y, 150, 5, StringAlignment.Near, StringAlignment.Center, font_middle_bold, brush_standart, false);
			y += 5;
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

		#region ������ ������������ �����
		private int PrintFooter(Graphics graph, int offset,  bool test, bool print, object o)
		{
			// ��������������� �����������
			RectangleF rect;
			int y		= offset;
		
			// ����������� ���������
			int offset_x_left_ini		= 15;
			int offset_x_right			= 10;
			int page_width				= 210;
			int w_page					= page_width - offset_x_right - offset_x_left_ini;

			int offset_x_left	= 0;
			int	row_height0	= 4;
			int	row_height	= 8;
			int w1			= 40;
			int w2			= 10;
			int w3			= 15;
			int interval	= 10;
			int offset_x_left_col2	= w1 + w2 * 4 + interval;

			int box_width	= 2;
			int box_height	= 2;
			
			y += 3;

			if(test == true || print == false)
			{	
				// ��������� ���������
				//y += title_height;
				//y += title_height;

				// ���� ��������
				y += row_height0;
				y += row_height0;
				y += row_height;
				y += row_height;

				y += row_height;

				// ���� ����������
				y += row_height0;
				y += row_height0;
				y += row_height;

				y += row_height;

				// ���� ����
				y += row_height0;
				y += row_height * 10;

				return y;
			}
			

			#region ���� ����������� ��������
			RectangleF block_rect = new RectangleF(offset_x_left_ini - 5, y - 2, 0, 0);
			// ���� ������� �����
			int y0 = y;
			offset_x_left		= offset_x_left_ini;
			offset_x_left_col2	= offset_x_left_ini + offset_x_left_col2;
			PrintTextBox(graph, "�� �������", offset_x_left, y, w1 + w2 * 4, row_height0, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "����� �������", offset_x_left_col2, y, w1 + w2 * 4, row_height0, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			y += row_height0;
			PrintTextBox(graph, "������ / �������", offset_x_left, y, w1, row_height0, 1, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "1", offset_x_left + w1, y, w2, row_height0, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "2", offset_x_left + w1 + w2, y, w2, row_height0, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "3", offset_x_left + w1 + w2 + w2, y, w2, row_height0, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "4", offset_x_left + w1 + w2 + w2 + w2, y, w2, row_height0, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "������ / �������", offset_x_left_col2, y, w1, row_height0, 1, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "1", offset_x_left_col2 + w1, y, w2, row_height0, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "2", offset_x_left_col2 + w1 + w2, y, w2, row_height0, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "3", offset_x_left_col2 + w1 + w2 + w2, y, w2, row_height0, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "4", offset_x_left_col2 + w1 + w2 + w2 + w2, y, w2, row_height0, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			y += row_height0;
			PrintTextBox(graph, "�����", offset_x_left, y, w1, row_height, 1, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "", offset_x_left + w1, y, w2, row_height, 1, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "", offset_x_left + w1 + w2, y, w2, row_height, 1, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "", offset_x_left + w1 + w2 + w2, y, w2, row_height, 1, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "", offset_x_left + w1 + w2 + w2 + w2, y, w2, row_height, 1, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "�����", offset_x_left_col2, y, w1, row_height, 1, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "", offset_x_left_col2 + w1, y, w2, row_height, 1, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "", offset_x_left_col2 + w1 + w2, y, w2, row_height, 1, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "", offset_x_left_col2 + w1 + w2 + w2, y, w2, row_height, 1, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "", offset_x_left_col2 + w1 + w2 + w2 + w2, y, w2, row_height, 1, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, pen_thin, false);
			y += row_height;
			PrintTextBox(graph, "������", offset_x_left, y, w1, row_height, 1, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "", offset_x_left + w1, y, w2, row_height, 1, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "", offset_x_left + w1 + w2, y, w2, row_height, 1, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "", offset_x_left + w1 + w2 + w2, y, w2, row_height, 1, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "", offset_x_left + w1 + w2 + w2 + w2, y, w2, row_height, 1, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "������", offset_x_left_col2, y, w1, row_height, 1, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "", offset_x_left_col2 + w1, y, w2, row_height, 1, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "", offset_x_left_col2 + w1 + w2, y, w2, row_height, 1, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "", offset_x_left_col2 + w1 + w2 + w2, y, w2, row_height, 1, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "", offset_x_left_col2 + w1 + w2 + w2 + w2, y, w2, row_height, 1, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, pen_thin, false);
			y += row_height;
			block_rect.Height = y - block_rect.Y + 4;
			block_rect.Width  = offset_x_left_ini + w1 + w2 * 4 + interval + w1 + w2*4;
			PrintBlockSigned(graph, "����������� ��������", block_rect, 10, font_middle_bold, brush_standart, pen_thin, false);
			
			y += row_height;
			#endregion

			#region ���� ����������
			block_rect = new RectangleF(offset_x_left_ini - 5, y - 2, 0, 0);
			// ���� ������� �����
			PrintTextBox(graph, "�� �������", offset_x_left, y, w1 + w2 * 4, row_height0, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "����� �������", offset_x_left_col2, y, w1 + w2 * 4, row_height0, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			y += row_height0;
			PrintTextBox(graph, "�������", offset_x_left, y, w1, row_height0, 1, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "1", offset_x_left + w1, y, w2, row_height0, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "2", offset_x_left + w1 + w2, y, w2, row_height0, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "3", offset_x_left + w1 + w2 + w2, y, w2, row_height0, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "4", offset_x_left + w1 + w2 + w2 + w2, y, w2, row_height0, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "�������", offset_x_left_col2, y, w1, row_height0, 1, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "1", offset_x_left_col2 + w1, y, w2, row_height0, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "2", offset_x_left_col2 + w1 + w2, y, w2, row_height0, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "3", offset_x_left_col2 + w1 + w2 + w2, y, w2, row_height0, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "4", offset_x_left_col2 + w1 + w2 + w2 + w2, y, w2, row_height0, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			y += row_height0;
			PrintTextBox(graph, "����������", offset_x_left, y, w1, row_height, 1, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "", offset_x_left + w1, y, w2, row_height, 1, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "", offset_x_left + w1 + w2, y, w2, row_height, 1, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "", offset_x_left + w1 + w2 + w2, y, w2, row_height, 1, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "", offset_x_left + w1 + w2 + w2 + w2, y, w2, row_height, 1, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "����������", offset_x_left_col2, y, w1, row_height, 1, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "", offset_x_left_col2 + w1, y, w2, row_height, 1, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "", offset_x_left_col2 + w1 + w2, y, w2, row_height, 1, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "", offset_x_left_col2 + w1 + w2 + w2, y, w2, row_height, 1, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "", offset_x_left_col2 + w1 + w2 + w2 + w2, y, w2, row_height, 1, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, pen_thin, false);
			y += row_height;

			block_rect.Height = y - block_rect.Y + 4;
			block_rect.Width  = offset_x_left_ini + w1 + w2 * 4 + interval + w1 + w2*4;
			PrintBlockSigned(graph, "���������� � ���������", block_rect, 10, font_middle_bold, brush_standart, pen_thin, false);

			y += row_height;
			#endregion

			#region ���� ������ ����
			block_rect = new RectangleF(offset_x_left_ini - 5, y - 2, 0, 0);
			// ���� ������� �����
			PrintTextBox(graph, "���", offset_x_left, y, w3, row_height0, 1, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "���", offset_x_left + w3, y, w3, row_height0, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "�����", offset_x_left + w3 + w3, y, w3, row_height0, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "�����", offset_x_left + w3 + w3 + w3, y, w3, row_height0, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "�������� ��������", offset_x_left + w3 + w3 + w3 + w3, y, w3*3, row_height0, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "�������", offset_x_left + w3 + w3 + w3 + w3 + w3 * 3, y, w3*4, row_height0, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			y += row_height0;

			for(int i = 0; i< 10; i++)
			{
				PrintTextBox(graph, "", offset_x_left, y, w3, row_height, 1, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
				PrintTextBox(graph, "", offset_x_left + w3, y, w3, row_height, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
				PrintTextBox(graph, "", offset_x_left + w3 + w3, y, w3, row_height, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
				PrintTextBox(graph, "", offset_x_left + w3 + w3 + w3, y, w3, row_height, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
				PrintTextBox(graph, "", offset_x_left + w3 + w3 + w3 + w3, y, w3*3, row_height, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
				PrintTextBox(graph, "", offset_x_left + w3 + w3 + w3 + w3 + w3 * 3, y, w3*4, row_height, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
				y += row_height;
			}


			block_rect.Height = y - block_rect.Y + 4;
			block_rect.Width  = offset_x_left_ini + w3 + w3 * 10;
			PrintBlockSigned(graph, "������ ����", block_rect, 10, font_middle_bold, brush_standart, pen_thin, false);
			#endregion

			y += row_height;
			y += row_height;
			y += row_height;

			
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
			offset = PrintBlock(graph, offset, new DelegatePrintBlock(PrintFooter), null);
		}
	}
}
