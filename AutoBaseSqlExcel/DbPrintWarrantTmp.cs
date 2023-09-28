using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbPrintWarrantTmp.
	/// </summary>
	public class DbPrintWarrantTmp:DbPrint
	{
		// ����������� ��� ������
		SolidBrush	brush_standart;
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
			public string txt_summ_work	= "";
			public string txt_summ_detail		= "";
			public string txt_summ_detail_oil	= "";
			public string txt_discount_work	= "";
			public string txt_pay			= "";
			public string txt_we			= "";
			public DIALER dialer		= DIALER.unknown;
			public bool inner			= false;
			public bool cashless		= false;
	
			public ArrayList	works	= null;
			public ArrayList	details	= null;

			public ArrayList	works_comments	= null;

			
			public float	count_nv	= 0.0F;
			public float	count_sp	= 0.0F;

			public HeaderData(DtCard card)
			{
				// ������ ��������
				txt_card = card.GetData("�����_��������").ToString() + "/" + card.GetData("���_��������").ToString();
				if((long)card.GetData("�����_�����_��������") != 0)
				{
					txt_warrant = card.GetData("�����_�����_��������").ToString() + " �� " + card.GetData("����_�����_������_��������").ToString();
				}
				// �������� �����-������
				if((short)card.GetData("������_��������") == 2)
				{
					txt_warrant_close = card.GetData("����_�����_������_��������").ToString();
				}

				/*
				// ���� ����� ��������� �������!!!!
				DtCardWorkend we = DbSqlCardWorkend.Find((long)card.GetData("�����_��������"), (int)card.GetData("���_��������"));
				if (we != null)
					txt_we = we.GetData("����_���������_�������").ToString();
				*/

				// ��� ������
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

				works_comments = new ArrayList();

				float summ_work = 0.0F;
				foreach(DtCardWork element in works)
				{

					// ��� ������ ������ ���� ��� ����������
					ArrayList comments = new ArrayList();
					int pos = (int)element.GetData("�������_��������_������");
					DbSqlCardWorkComment.SelectInArrayConnection(comments, (long)card.GetData("�����_��������"), (int)card.GetData("���_��������"), pos);
					if(comments.Count > 0)
					{
						while(pos > works_comments.Count)  works_comments.Add(null);
						works_comments.Insert(pos, comments);
					}

					float nv = (float)element.GetData("������������_��������_������");
					if(nv != 0.0F)
						count_nv += nv * (float)element.GetData("����������_��������_������");
					else
					{
						// ������ �����
						// ��������� ������ � �� ������ ������
						long code_work = (long)element.GetData("���_������������_��������_������");
						DtWork work = DbSqlWork.Find(code_work);
						// ������� ��������� ��
						float local_nv	= (float)work.GetData("��");
						if(local_nv == 0.0F)
						{
							long code_collection = (long)work.GetData("������_���_���������");
							float local_sp = 0.0F;
							if(code_collection !=  0)
							{
								// ���� ���������
								element.SetData("������_���_���������", code_collection);
							
								ArrayList array = new ArrayList();
								DbSqlWorkCollectionItem.SelectInArray(array, code_collection);
								foreach(DtWorkCollectionItem elm in array)
								{
									local_sp += (float)elm.GetData("������������_���������_�������");
								}
								element.SetData("������_�����_��", local_sp);
							
							}
							count_sp += local_sp * (float)element.GetData("����������_��������_������");
						}
						else
						{
							count_sp += local_nv * (float)element.GetData("����������_��������_������");
							element.SetData("������_�����_��", local_nv);
							element.SetData("����_��", true);
						}
					}
					if((bool)element.GetData("��������_��������_������") == false)
					{
						if(nv != 0.0F)
							summ_work += (float)element.GetData("������������_��������_������") * (float)element.GetData("����������_��������_������")*(float)element.GetData("��������_��������_������");
						else
							summ_work += (float)element.GetData("����������_��������_������")*(float)element.GetData("��������_��������_������");
					}
					// ��������� ������ �� ������������, ��������� ������
					ArrayList array1 = new ArrayList();
					DbSqlStaff.SelectInArrayExecutor(array1, (long)card.GetData("�����_��������"), (int)card.GetData("���_��������"), (int)element.GetData("�������_��������_������"));
					string tx = "";
					foreach (DtStaff staff in array1)
					{
						if(tx != "") tx += ", ";
						tx += staff.GetData("���_��������").ToString(); 
					}
					element.SetData("�����_���������_�����_�����������", tx);
				}

				// ������ ������� � �����-������
				details = new ArrayList();
				DbSqlCardDetail.SelectInArray(card, details);
				float summ_detail = 0.0F;
				float summ_detail_oil = 0.0F;
				foreach(DtCardDetail element in details)
				{
					if((bool)element.GetData("��������_��������_������") == false)
					{
						if((bool)element.GetData("��������_��������_������") == false)
						{
							// summ_detail += (float)element.GetData("����������_��������_������") * (float)element.GetData("����_��������_������");
                            summ_detail += (float)element.DetailSummDiscount;
						}
						else
						{
							summ_detail_oil += (float)element.GetData("����������_��������_������") * (float)element.GetData("����_��������_������");
						}
					}
				}

				// ������ �� ����� �����, � ������ ������
				txt_summ_work			= Db.CachToTxt(Math.Round(summ_work, 2));
				txt_summ_detail			= Db.CachToTxt(Math.Round(summ_detail, 2));
				txt_summ_detail_oil		= Db.CachToTxt(Math.Round(summ_detail_oil, 2));
				float discount_val	= (float)card.GetData("������_������_��������");
				float discount_work = 0.0F;
				if(discount_val != 0.0F)
				{
					discount_work = (float)Math.Round((float)(summ_work / 100 * discount_val), 0);
				}
				txt_discount_work	= Db.CachToTxt(Math.Round(discount_work, 2));
				txt_pay				= Db.CachToTxt(Math.Round(summ_work - discount_work + summ_detail + summ_detail_oil, 2));
			}
		}
		HeaderData	header_data = null;
		#endregion

		public DbPrintWarrantTmp(long card_number, int card_year)
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
			if(header_data.txt_warrant != "")
				PrintText(graph, "��������������� �����-����� �" + header_data.txt_warrant, header_x, y, 150, 5, StringAlignment.Near, StringAlignment.Center, font_middle_bold, brush_standart, false);
			else
				//PrintText(graph, "�����-����� (�� ������)" + header_data.txt_warrant, header_x, y, 150, 5, StringAlignment.Near, StringAlignment.Center, font_middle_bold, brush_standart, false);
				PrintText(graph, "������ �� ������ � " + header_data.txt_card, header_x, y, 150, 5, StringAlignment.Near, StringAlignment.Center, font_middle_bold, brush_standart, false);
			y += 5;
			if(header_data.txt_warrant_close != "")
				PrintText(graph, "������ : " + header_data.txt_warrant_close, header_x, y, 150, 5, StringAlignment.Near, StringAlignment.Center, font_middle_bold, brush_standart, false);
			else
				//PrintText(graph, "�� ������", header_x, y, 150, 5, StringAlignment.Near, StringAlignment.Center, font_middle_bold, brush_standart, false);
				PrintText(graph, "����� ������ ����������: " + header_data.txt_we, header_x, y, 150, 5, StringAlignment.Near, StringAlignment.Center, font_middle_bold, brush_standart, false);
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

		#region ������ ����� ��� ����� ����
		private int PrintHours(Graphics graph, int offset,  bool test, bool print, object o)
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

			int	rowheight	= 5;
			int w1			= 100;
			int w2			= 20;
			int w3			= 40;
			int w4			= 40;
			int w5			= 50;
			

			y += 1;
			if(test == true || print == false)
			{	
				y += rowheight;
				y += rowheight;
				y += 1;
				return y;
			}

			// ������ �������
			PrintText(graph, "����� ����", w1, y, w2, rowheight * 2, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			PrintText(graph, "������ �����:", w1 + w2, y, w3, rowheight, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			PrintText(graph, "������������ �����:", w4, y, w5, rowheight, StringAlignment.Far, StringAlignment.Center, font_small_bold, brush_standart, false);
			y += rowheight;
			PrintText(graph, "��������� �����:", w1 + w2, y, w3, rowheight, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			PrintText(graph, "������������� �����:", w4, y, w5, rowheight, StringAlignment.Far, StringAlignment.Center, font_small_bold, brush_standart, false);
			y += rowheight;
			y += 1;
			return y;
		}
		#endregion

		#region ������ ����� �����  � �������
		private int PrintWorkBlockHead(Graphics graph, int offset,  bool test, bool print, object o)
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

			int	col1		=	20;
			int col3		=	13;
			int col4		=	15;
			int col5		=	8;
			int col6		=	25;
			int col7		=	0;
			int col8		=	0;
			int col2		=	page_width - offset_x_left - offset_x_right - col1 - col3 - col4 - col5 - col6 - col7 - col8;
			int	rowheight	= 8;

			if(test == true || print == false)
			{	
				y += rowheight;
				return y;
			}

			// ������ �������
			PrintTextBox(graph, "��� ������ ��� �/�����", offset_x_left, y, col1, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "������������ ������ (�������� �����)", offset_x_left + col1, y, col2, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "���.�", offset_x_left + col1 + col2, y, col3, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "���-�� �� (��)", offset_x_left + col1 + col2 + col3, y, col4, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "���.", offset_x_left + col1 + col2 + col3 + col4, y, col5, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "�����", offset_x_left + col1 + col2 + col3 + col4 + col5, y, col6, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
		//	PrintTextBox(graph, "����� �������", offset_x_left + col1 + col2 + col3 + col4 + col5 + col6, y, col7 + col8, rowheight / 2, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
		//	y += rowheight / 2;
		//	PrintTextBox(graph, "������", offset_x_left + col1 + col2 + col3 + col4 + col5 + col6, y, col7, rowheight / 2, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
		//	PrintTextBox(graph, "���������", offset_x_left + col1 + col2 + col3 + col4 + col5 + col6 + col7, y, col8, rowheight / 2, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			y += rowheight;
			return y;
		}
		private int PrintWorkBlock(Graphics graph, int offset,  bool test, bool print, object o)
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

			int	col1		=	20;
			int col3		=	13;
			int col4		=	15;
			int col5		=	8;
			int col6		=	25;
			int col7		=	0;
			int col8		=	0;
			int col2		=	page_width - offset_x_left - offset_x_right - col1 - col3 - col4 - col5 - col6 - col7 - col8;
			int	rowheight	= 8;
			
			if(o == null) return y;
			string txt;
			string txt1;
			string txt2;
			string txt3 = "";
			string txt4 = "";
			string txt5 = "";
			string txt_ex = "";
			DtCardWork work = (DtCardWork)o;
			txt		= (string)work.GetData("�����_�������_��������_������");
			txt1	= (string)work.GetData("������������_��������_������");

			// ��� �������������, ��������� ���������� � ����������� ������
			int pos = (int)work.GetData("�������_��������_������");
			if(pos < header_data.works_comments.Count && header_data.works_comments[pos] != null)
			{
				ArrayList arr = (ArrayList)header_data.works_comments[pos];
				foreach(DtCardWorkComment cmnt in arr)
				{
					txt_ex += cmnt.text + "\n";
				}
				txt1 += "\n" + txt_ex;
			}


			if ((float)work.GetData("������������_��������_������") != 0.0F)
			{
				// ������� ������������
				txt2	= work.GetData("������������_��������_������").ToString();
				txt4	= Db.CachToTxt((float)work.GetData("����������_��������_������")*(float)work.GetData("������������_��������_������")*(float)work.GetData("��������_��������_������"));
				
			}
			else
			{	
				// ������ �����
				txt2	= "��";
				txt4	= Db.CachToTxt((float)work.GetData("����������_��������_������")*(float)work.GetData("��������_��������_������"));
				// ������ ������ ������ ������
				// ������ �����
				
				if((bool)work.GetData("����_��") == true)
				{
					float nv = (float)work.GetData("������_�����_��");
					if (nv != 0.0F)
						txt2 += "[" + Math.Round(nv, 2).ToString() + "]";
				}
				else
				{
					if((long)work.GetData("������_���_���������") != 0)
					{
						float nv = (float)work.GetData("������_�����_��");
						txt2 += "(" + Math.Round(nv, 2).ToString() + ")";
					}
				}	
			}
			if ((float)work.GetData("����������_��������_������") > 1.0F)
			{
				txt2 = work.GetData("����������_��������_������").ToString() + "*" + txt2;
			}
			
			if ((bool)work.GetData("��������_��������_������") == true)
			{
				txt3	= "+";
				txt4	= "��������";
			}
			// ��� �����������
			txt5 = (string)work.GetData("�����_���������_�����_�����������");
			
			
			// ������ �����
			SizeF size = MeasureText(graph, txt1, col2 - 2, StringAlignment.Near, StringAlignment.Center, font_small);
			rowheight	= (int)Math.Ceiling(size.Height);
			if(test == true || print == false)
			{	
				return y + rowheight;
			}
			PrintTextBox(graph, txt, offset_x_left, y, col1, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
			PrintTextBox(graph, txt1, offset_x_left + col1, y, col2, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
			PrintTextBox(graph, txt5, offset_x_left + col1 + col2, y, col3, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
		//	PrintBox(graph, offset_x_left + col1 + col2, y, col3, rowheight, pen_thin);
			PrintTextBox(graph, txt2, offset_x_left + col1 + col2 + col3, y, col4, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
			PrintTextBox(graph, txt3, offset_x_left + col1 + col2 + col3 + col4, y, col5, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
			PrintTextBox(graph, txt4, offset_x_left + col1 + col2 + col3 + col4 + col5, y, col6, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
		//	PrintBox(graph, offset_x_left + col1 + col2 + col3 + col4 + col5, y, col6, rowheight, pen_thin);
		//	PrintBox(graph, offset_x_left + col1 + col2 + col3 + col4 + col5 + col6, y, col7, rowheight, pen_thin);
		//	PrintBox(graph, offset_x_left + col1 + col2 + col3 + col4 + col5 + col6 + col7, y, col8, rowheight, pen_thin);
			y += rowheight;
			return y;
		}
		private int PrintDetailOilBlockHead(Graphics graph, int offset,  bool test, bool print, object o)
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

			int	col1		=	20;
			int col3		=	13;
			int col4		=	15;
			int col5		=	8;
			int col6		=	25;
			int col7		=	0;
			int col8		=	0;
			int col2		=	page_width - offset_x_left - offset_x_right - col1 - col3 - col4 - col5 - col6 - col7 - col8;
			int	rowheight	= 8;

			if(test == true || print == false)
			{	
				y += rowheight;
				return y;
			}

			// ������ �������
			PrintTextBox(graph, "��� �/�����", offset_x_left, y, col1, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "������������ ���������� ���������", offset_x_left + col1, y, col2, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "-", offset_x_left + col1 + col2, y, col3, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "���-��", offset_x_left + col1 + col2 + col3, y, col4, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "���.", offset_x_left + col1 + col2 + col3 + col4, y, col5, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "�����", offset_x_left + col1 + col2 + col3 + col4 + col5, y, col6, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			//	PrintTextBox(graph, "����� �������", offset_x_left + col1 + col2 + col3 + col4 + col5 + col6, y, col7 + col8, rowheight / 2, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			//	y += rowheight / 2;
			//	PrintTextBox(graph, "������", offset_x_left + col1 + col2 + col3 + col4 + col5 + col6, y, col7, rowheight / 2, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			//	PrintTextBox(graph, "���������", offset_x_left + col1 + col2 + col3 + col4 + col5 + col6 + col7, y, col8, rowheight / 2, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			y += rowheight;
			return y;
		}
		private int PrintDetailBlockHead(Graphics graph, int offset,  bool test, bool print, object o)
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

			int	col1		=	20;
			int col3		=	13;
			int col4		=	15;
			int col5		=	8;
			int col6		=	25;
			int col7		=	0;
			int col8		=	0;
			int col2		=	page_width - offset_x_left - offset_x_right - col1 - col3 - col4 - col5 - col6 - col7 - col8;
			int	rowheight	= 8;

			if(test == true || print == false)
			{	
				y += rowheight;
				return y;
			}

			// ������ �������
			PrintTextBox(graph, "��� �/�����", offset_x_left, y, col1, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "������������ �������� �����", offset_x_left + col1, y, col2, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "-", offset_x_left + col1 + col2, y, col3, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "���-��", offset_x_left + col1 + col2 + col3, y, col4, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "���.", offset_x_left + col1 + col2 + col3 + col4, y, col5, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "�����", offset_x_left + col1 + col2 + col3 + col4 + col5, y, col6, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			//	PrintTextBox(graph, "����� �������", offset_x_left + col1 + col2 + col3 + col4 + col5 + col6, y, col7 + col8, rowheight / 2, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			//	y += rowheight / 2;
			//	PrintTextBox(graph, "������", offset_x_left + col1 + col2 + col3 + col4 + col5 + col6, y, col7, rowheight / 2, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			//	PrintTextBox(graph, "���������", offset_x_left + col1 + col2 + col3 + col4 + col5 + col6 + col7, y, col8, rowheight / 2, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			y += rowheight;
			return y;
		}
		private int PrintDetailBlock(Graphics graph, int offset,  bool test, bool print, object o)
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

			int	col1		=	20;
			int col3		=	13;
			int col4		=	15;
			int col5		=	8;
			int col6		=	25;
			int col7		=	0;
			int col8		=	0;
			int col2		=	page_width - offset_x_left - offset_x_right - col1 - col3 - col4 - col5 - col6 - col7 - col8;
			int	rowheight	= 8;
			
			if(o == null) return y;
			string txt;
			string txt1;
			string txt2;
			string txt3 = "";
			string txt4 = "";
			DtCardDetail detail = (DtCardDetail)o;
			txt		= (string)detail.GetData("�������_�����_��������_������");
			txt1	= (string)detail.GetData("������������_��������_������");
			txt2	= detail.GetData("����������_��������_������").ToString() + " " + (string)detail.GetData("�������_���������_��������_������");
			txt4	= Db.CachToTxt((float)detail.GetData("����������_��������_������")*(float)detail.GetData("����_��������_������"));
            txt4    = Db.CachToTxt((float)detail.DetailSummDiscount); 
			
			if ((bool)detail.GetData("��������_��������_������") == true)
			{
				txt3	= "+";
				txt4	= "��������";
			}
			
			
			// ������ �����
			SizeF size = MeasureText(graph, txt1, col2 - 2, StringAlignment.Near, StringAlignment.Center, font_small);
			SizeF size1 = MeasureText(graph, txt, col1 - 2, StringAlignment.Near, StringAlignment.Center, font_small);
			if(size1.Height > size.Height) size = size1;
			rowheight	= (int)Math.Ceiling(size.Height);
			if(test == true || print == false)
			{	
				return y + rowheight;
			}
			PrintTextBox(graph, txt, offset_x_left, y, col1, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
			PrintTextBox(graph, txt1, offset_x_left + col1, y, col2, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
			PrintBox(graph, offset_x_left + col1 + col2, y, col3, rowheight, pen_thin);
			PrintTextBox(graph, txt2, offset_x_left + col1 + col2 + col3, y, col4, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
			PrintTextBox(graph, txt3, offset_x_left + col1 + col2 + col3 + col4, y, col5, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
			PrintTextBox(graph, txt4, offset_x_left + col1 + col2 + col3 + col4 + col5, y, col6, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
		//	PrintBox(graph, offset_x_left + col1 + col2 + col3 + col4 + col5, y, col6, rowheight, pen_thin);
		//	PrintBox(graph, offset_x_left + col1 + col2 + col3 + col4 + col5 + col6, y, col7, rowheight, pen_thin);
		//	PrintBox(graph, offset_x_left + col1 + col2 + col3 + col4 + col5 + col6 + col7, y, col8, rowheight, pen_thin);
			y += rowheight;
			return y;
		}
		private int PrintWorkBlockFooter(Graphics graph, int offset,  bool test, bool print, object o)
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

			int	col1		=	20;
			int col2		=	50;
			int col3		=	25;
			int x1			=	200 - 95 - offset_x_left;			
			int	rowheight	= 4;

			int h2			= 4;
			int w10			= 10;
			int w11			= 20;
			int w12			= 35;

			if(test == true || print == false)
			{	
				y += rowheight;
				y += rowheight;
				y += rowheight;
				y += rowheight;
				y += rowheight;
				y += rowheight;
				y += rowheight;
				y += rowheight;
				y += rowheight;
				return y;
			}

           
			// ���� �������� ����� ������
            /*
			y += 4;
			rect = new RectangleF(w10, y, w12, h2 + rowheight * 6 + 1);
			PrintRoundBox(graph, rect, pen_thin, 3);
			PrintTextNoBox(graph, "����� ������", w10 + 1, y, w12 - 1, h2, 2, StringAlignment.Center, StringAlignment.Near, font_small_bold, brush_standart, pen_thin, false);
			if(header_data.cashless == false && header_data.inner == true)
			{
				PrintRightCheckBoxO(graph, "��������", w10 + 1, y + 5, w11, 6, 1, 1, 0, StringAlignment.Near, font_small, brush_standart, pen_thin, false);
				PrintRightCheckBoxO(graph, "������", w10 + 1, y + 12, w11, 6, 1, 1, 0, StringAlignment.Near, font_small, brush_standart, pen_thin, false);
				PrintRightCheckBoxOChecked(graph, "�����.", w10 + 1, y + 19, w11, 6, 1, 1, 0, StringAlignment.Near, font_small, brush_standart, pen_thin, false);
			}
			if(header_data.cashless == true && header_data.inner == false)
			{
				PrintRightCheckBoxO(graph, "��������", w10 + 1, y + 5, w11, 6, 1, 1, 0, StringAlignment.Near, font_small, brush_standart, pen_thin, false);
				PrintRightCheckBoxOChecked(graph, "������", w10 + 1, y + 12, w11, 6, 1, 1, 0, StringAlignment.Near, font_small, brush_standart, pen_thin, false);
				PrintRightCheckBoxO(graph, "�����.", w10 + 1, y + 19, w11, 6, 1, 1, 0, StringAlignment.Near, font_small, brush_standart, pen_thin, false);
			}
			if(header_data.cashless == false && header_data.inner == false)
			{
				PrintRightCheckBoxOChecked(graph, "��������", w10 + 1, y + 5, w11, 6, 1, 1, 0, StringAlignment.Near, font_small, brush_standart, pen_thin, false);
				PrintRightCheckBoxO(graph, "������", w10 + 1, y + 12, w11, 6, 1, 1, 0, StringAlignment.Near, font_small, brush_standart, pen_thin, false);
				PrintRightCheckBoxO(graph, "�����.", w10 + 1, y + 19, w11, 6, 1, 1, 0, StringAlignment.Near, font_small, brush_standart, pen_thin, false);
			}
			y -= 4;
            */
            // �������� ������� ������������� ����������� ���������
            y += 4;
            rect = new RectangleF(offset_x_left, y, 90, rowheight * 6);
            PrintTextNoBox(graph, "��������� �� �������� ������� �������� �������� ������ � ��������� ����������, �������������� ���������� �� �������� � ����������� ����������. � ����� � ���, �� ������������� �������� �� ������, ����������� � �������������� ���� �������� ������ � ��������� ����������.", offset_x_left, y, 90, rowheight * 6, 4, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, pen_thin, false);
            PrintRoundBox(graph, rect, pen_thin, 2);
            y -= 4;

			// ������ �������
			PrintTextBox(graph, "�����:", offset_x_left + x1, y, col1, rowheight * 7, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "��", offset_x_left + x1 + col1, y, col2, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, Math.Round(header_data.count_nv, 2).ToString(), offset_x_left + x1 + col1 + col2, y, col3, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			y += rowheight;
			PrintTextBox(graph, "��", offset_x_left + x1 + col1, y, col2, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, header_data.count_sp.ToString(), offset_x_left + x1 + col1 + col2, y, col3, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			y += rowheight;
			PrintTextBox(graph, "������", offset_x_left + x1 + col1, y, col2, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, header_data.txt_summ_work, offset_x_left + x1 + col1 + col2, y, col3, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			y += rowheight;
			PrintTextBox(graph, "������ ������", offset_x_left + x1 + col1, y, col2, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, header_data.txt_discount_work, offset_x_left + x1 + col1 + col2, y, col3, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			y += rowheight;
			PrintTextBox(graph, "��������� ���������", offset_x_left + x1 + col1, y, col2, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, header_data.txt_summ_detail_oil, offset_x_left + x1 + col1 + col2, y, col3, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			y += rowheight;
			PrintTextBox(graph, "��������", offset_x_left + x1 + col1, y, col2, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, header_data.txt_summ_detail, offset_x_left + x1 + col1 + col2, y, col3, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			y += rowheight;
			PrintTextBox(graph, "� ������", offset_x_left + x1 + col1, y, col2, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, header_data.txt_pay, offset_x_left + x1 + col1 + col2, y, col3, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			y += rowheight;
			PrintTextBox(graph, "���� ������", offset_x_left + x1, y,  col1 + col2, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintBox(graph, offset_x_left + x1 + col1 + col2, y,  col3, rowheight, pen_thin);
			y += rowheight;
			PrintTextBox(graph, "���� ����������� �����", offset_x_left + x1, y,  col1 + col2, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintBox(graph, offset_x_left + x1 + col1 + col2, y,  col3, rowheight, pen_thin);
			y += rowheight;
			return y;
		}
		#endregion

		#region ���������� ����� ����� ������� ���������
		private int PrintWorkBlank(Graphics graph, int offset,  bool test, bool print, object o)
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


			int	col1		=	20;
			int col3		=	13;
			int col4		=	15;
			int col5		=	8;
			int col6		=	25;
			int col7		=	0;
			int col8		=	0;
			int col2		=	page_width - offset_x_left - offset_x_right - col1 - col3 - col4 - col5 - col6 - col7 - col8;
			int	rowheight	= 8;

			// �������� ��������� ����������� ������
			int h = 0;
			h += this.PrintWorkBlockFooter(graph, 0, false, false, null);	// ���������� �������
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
				PrintBox(graph, offset_x_left, y, col1, rowheight, pen_thin);
				PrintBox(graph, offset_x_left + col1, y, col2, rowheight, pen_thin);
				PrintBox(graph, offset_x_left + col1 + col2, y, col3, rowheight, pen_thin);
				PrintBox(graph, offset_x_left + col1 + col2 + col3, y, col4, rowheight, pen_thin);
				PrintBox(graph, offset_x_left + col1 + col2 + col3 + col4, y, col5, rowheight, pen_thin);
				PrintBox(graph, offset_x_left + col1 + col2 + col3 + col4 + col5, y, col6, rowheight, pen_thin);
			//	PrintBox(graph, offset_x_left + col1 + col2 + col3 + col4 + col5 + col6, y, col7, rowheight, pen_thin);
			//	PrintBox(graph, offset_x_left + col1 + col2 + col3 + col4 + col5 + col6 + col7, y, col8, rowheight, pen_thin);
				y += rowheight;
			}
			return y;
		}
		#endregion

		#region ������ ������������ �����
		private int PrintFooter(Graphics graph, int offset,  bool test, bool print, object o)
		{
			// ��������������� �����������
			RectangleF rect;
			int y		= offset;
			string txt	= "";
			SizeF size;
			int h_guar = 0;
			// ����������� ���������
			int offset_x_left		= 10;
			int offset_x_right		= 10;
			int page_width			= 210;
			int title_height		= 4;
			int page_height			= 297 - 10;

			int	rowheight	= 4;
			int w1			= 40;
			int w2			= 20;
			int w3			= 30;
			int w4			= 30;
			int d1			= 5;
			int d2			= 100;
			int w5			= 100;


			txt = "-�������� �������� �������� ������, �������, ��������������� ������������ � �������������� �� ��� ���������� �����, ����������� 30 ���� ��� 1000 �� �������.";
			txt += "\n";
			txt += "-�������� �� �������� ���� ����� �������������� ���������� �������: �������������� ������ - 1000 ��; ������ �� ��������� ������� - 1000 ��. �� ������������������ ������ (����� ���������, �������, �����, ��������� �������� �����������������, ���� ���������, ��������� ������� � �.�.) �������� �� ����������������.";
			size = this.MeasureText(graph, txt, 190, StringAlignment.Center, StringAlignment.Center, font_small);
			h_guar = 4 + (int)Math.Round(size.Height) + 1;

			y += 3;
			if(test == true || print == false)
			{	
				y += rowheight;
				y += rowheight;
				y += 3;
				y += rowheight * 2 + 1;
				y += h_guar;
				y += 3;
				y += rowheight;
				y += rowheight;
				y += rowheight;
			//	y += 3;
			//	y += rowheight;
			//	y += rowheight;
				return y;
			}

			// ������ �������
			PrintText(graph, "������-�����������", offset_x_left, y, w1, rowheight, StringAlignment.Near, StringAlignment.Center, font_middle, brush_standart, false);
			//PrintText(graph, "������ �����", offset_x_left + d2, y, w4, rowheight, StringAlignment.Near, StringAlignment.Center, font_middle, brush_standart, false);
			y += rowheight;
			PrintLineHor(graph, offset_x_left + w1, y, w2, pen_thin, false);
			PrintLineHor(graph, offset_x_left + w1 + w2 + d1 , y, w3, pen_thin, false);
			PrintText(graph, "(�������)", offset_x_left + w1, y, w2, rowheight, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
			PrintText(graph, "(���������)", offset_x_left + w1 + w2 + d1, y, w3, rowheight, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
			//PrintLineHor(graph, offset_x_left + w4 + d2, y, w2, pen_thin, false);
			//PrintLineHor(graph, offset_x_left + w4 + w2 + d1 + d2, y, w3, pen_thin, false);
			//PrintText(graph, "(�������)", offset_x_left + w4 + d2, y, w2, rowheight, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
			//PrintText(graph, "(���������)", offset_x_left + w4 + w2 + d1 + d2, y, w3, rowheight, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
			y += rowheight;
			y += 3;

			// �������������� ���������� � ��������
			rect = new RectangleF(offset_x_left, y, 190, rowheight * 2);
			PrintTextNoBox(graph, "������������ ���� ������� ��� ���������� �������� ������ ���������� 45 ����. ��� ������� �������� ������ ������ ����� ���������� � ����������� �������� �����.", offset_x_left, y, 190, rowheight * 2, 4, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, pen_thin, false);
			PrintRoundBox(graph, rect, pen_thin, 2);
			y += rowheight * 2 + 1;

			// ���� ��������
			rect = new RectangleF(10, y, 190, h_guar);
			PrintTextNoBox(graph, "������� ��������", 10, y, 190, 4, 2, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextNoBox(graph, txt, 10, y + 4, 190, size.Height, 2, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
			PrintRoundBox(graph, rect, pen_thin, 5);
			y += h_guar;
			y += 3;

			PrintText(graph, "� ������� � ������ ���������� �����, ��������� � ���������� ��������. � ��������� �������� ����� ���������� � ��������. (��������)", offset_x_left, y, w5, rowheight * 3, StringAlignment.Near, StringAlignment.Center, font_middle, brush_standart, false);
			y += rowheight;
			y += rowheight;
			PrintLineHor(graph, offset_x_left + w5, y, w2, pen_thin, false);
			PrintLineHor(graph, offset_x_left + w5 + w2 + d1 , y, w3, pen_thin, false);
			PrintText(graph, "(�������)", offset_x_left + w5, y, w2, rowheight, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
			PrintText(graph, "(���������)", offset_x_left + w5 + w2 + d1, y, w3, rowheight, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
			y += rowheight;
	//		y += 3;
	//		PrintText(graph, "������ �������� ����������� � � ���� (��������)", offset_x_left, y, w5, rowheight, StringAlignment.Near, StringAlignment.Center, font_middle, brush_standart, false);
	//		y += rowheight;
	//		PrintLineHor(graph, offset_x_left + w5, y, w2, pen_thin, false);
	//		PrintLineHor(graph, offset_x_left + w5 + w2 + d1 , y, w3, pen_thin, false);
	//		PrintText(graph, "(�������)", offset_x_left + w5, y, w2, rowheight, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
	//		PrintText(graph, "(���������)", offset_x_left + w5 + w2 + d1, y, w3, rowheight, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
	//		y += rowheight;
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
			//offset = PrintBlock(graph, offset, new DelegatePrintBlock(PrintHours), null);
			bool first = true;
			if(header_data.works.Count > 0)
			{
				foreach(DtCardWork element in header_data.works)
				{
					offset = PrintBlockWithHeader(graph, offset, new DelegatePrintBlock(PrintWorkBlock), element, new DelegatePrintBlock(PrintWorkBlockHead), first);
					first = false;
				}
			}
			else
			{
				offset = PrintBlockWithHeader(graph, offset, new DelegatePrintBlock(PrintWorkBlock), null, new DelegatePrintBlock(PrintWorkBlockHead), first);
			}
			first = true;
			if(header_data.details.Count > 0)
			{
				foreach(DtCardDetail element in header_data.details)
				{
					if((bool)element.GetData("��������_��������_������") == true)
					{
						offset = PrintBlockWithHeader(graph, offset, new DelegatePrintBlock(PrintDetailBlock), element, new DelegatePrintBlock(PrintDetailOilBlockHead), first);
						first = false;
					}
				}
			}
			first = true;
			if(header_data.details.Count > 0)
			{
				foreach(DtCardDetail element in header_data.details)
				{
					if((bool)element.GetData("��������_��������_������") == false)
					{
						offset = PrintBlockWithHeader(graph, offset, new DelegatePrintBlock(PrintDetailBlock), element, new DelegatePrintBlock(PrintDetailBlockHead), first);
						first = false;
					}
				}
			}
			offset = PrintBlock(graph, offset, new DelegatePrintBlock(PrintWorkBlank), null);
			offset = PrintBlock(graph, offset, new DelegatePrintBlock(PrintWorkBlockFooter), null);
			offset = PrintBlock(graph, offset, new DelegatePrintBlock(PrintFooter), null);
		}
	}
}
