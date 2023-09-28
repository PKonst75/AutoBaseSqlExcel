using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbPrintWarrant.
	/// </summary>
	public class DbPrintWarrant:DbPrint
	{
		// ����������� ��� ������
		SolidBrush	brush_standart;
		SolidBrush	brush_lightgray;
		Font		font_small_bold;
		Font		font_middle_bold;
		Font		font_middle;
		Font		font_small;
		Font		font_small_cur;
		Pen			pen_thin;

		private DtCard		card;
        public static int thetype = 0;
       

		#region ������ ��� ������
		protected class HeaderData
		{
            public int thetype = 0;
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
			public string txt_warrant	= "";
			public string txt_warrant_close		= "";
			public string txt_summ_work			= "";
			public string txt_summ_work_pay		= "";
			public string txt_discount_work		= "";
			public string txt_summ_detail		= "";
            // �����1000!!!!!!!
            public string txt_summ_parts_pay = "";
            public string txt_summ_pay2 = "";
            public string txt_discount_parts = "";
            public bool is_discount_parts         = false;
            // ����� �����1000
			public string txt_summ_detail_pay	= "";
			public string txt_summ_pay			= "";
			public string txt_recomendations	= "";
			public DIALER dialer		= DIALER.unknown;

			public string txt_warrant_number	= "";
			public string txt_date_request		= "";
			public string txt_date_workbegin	= "";
			public string txt_date_workend		= "";
			public string txt_date_givinout		= "";
	
			public ArrayList	works			= null;
			public ArrayList	details			= null;
			public ArrayList	recomendations	= null;
			public bool			cashless		= false;
			public bool			inner			= false;
			public bool			creditcard		= false;

            

			public ArrayList	works_comments	= null;


			public float	count_nv	= 0.0F;
			public int		count_sp	= 0;

			public bool		juridical	= false;

			public HeaderData(DtCard card, int thetype1)
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
				if((bool)card.GetData("���������_�����_��������") == true)
				{
					creditcard = true;
				}
				int run = (int)card.GetData("������_��������");
				if(run != 0)
				{
					txt_run		= run.ToString();
				}

				// ��������� ������ �� �������� �����-������
				txt_date_request	= card.GetData("����").ToString();	// ���� ��������� �����-������
				if((long)card.GetData("�����_�����_��������") != 0)
				{
					txt_warrant_number	= card.GetData("�����_�����_��������").ToString() + "/" + card.GetData("���_��������").ToString();
					txt_date_workbegin	= card.GetData("����_�����_������_��������").ToString(); // ���� ������ �����
				}
				DtCardMarkEndWork mark_workend = DbSqlCardMarkEndWork.Find(card);
				if(mark_workend != null)
					txt_date_workend = mark_workend.GetData("����").ToString();
				if((short)card.GetData("������_��������") == 2)
					txt_date_givinout = card.GetData("����_�����_������_��������").ToString();	// ���� ������ ����������
				// ��������� ��������� ����� �� ��������

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
					juridical		= (bool)owner.GetData("�����������_����");
				}
				// �������������
				DtPartner represent = DbSqlPartner.Find((long)card.GetData("�������������_��������"));
				if(represent != null)
					txt_represent	= represent.GetTitle();

				// ������ ����� � �����-������
				works = new ArrayList();
				DbSqlCardWork.SelectInArray(card, works);

				works_comments = new ArrayList();

                float discount_val		= 0.0F;
                float discount_work		= 0.0F;
                discount_val	        = (float)card.GetData("������_������_��������");

				float summ_work = 0.0F;
                float summ_work_for_common_discount = 0.0F;
				foreach(DtCardWork element in works)
				{
					// ��� ������ ������ ���� ������� ����������
					ArrayList comments = new ArrayList();
					int pos = (int)element.GetData("�������_��������_������");
					DbSqlCardWorkComment.SelectInArrayConnectionVisible(comments, (long)card.GetData("�����_��������"), (int)card.GetData("���_��������"), pos);
					if(comments.Count > 0)
					{
						while(pos > works_comments.Count)  works_comments.Add(null);
						works_comments.Insert(pos, comments);
					}

					if((bool)element.GetData("��������_��������_������") == false)
					{
                        float summ_work_local = 0;
                        float discount_work_local = 0;
						float nv = (float)element.GetData("������������_��������_������");
                        float local_discount = (float)element.GetData("������_��������_������");
						if(nv != 0.0F)
						{
                            summ_work_local = nv * (float)element.GetData("��������_��������_������") * (float)element.GetData("����������_��������_������");
							//float summ_work_local = nv * (float)element.GetData("��������_��������_������") * (float)element.GetData("����������_��������_������");
							if (thetype1 != 1) summ_work_local = (float)Math.Round(summ_work_local, 0);
							//summ_work += summ_work_local;
							//summ_work += nv * (float)element.GetData("��������_��������_������") * (float)element.GetData("����������_��������_������");
						}
						else
						{
                            summ_work_local = (float)element.GetData("��������_��������_������") * (float)element.GetData("����������_��������_������");
							//float summ_work_local = (float)element.GetData("��������_��������_������") * (float)element.GetData("����������_��������_������");
                            if (thetype1 != 1) summ_work_local = (float)Math.Round(summ_work_local, 0);
							//summ_work += summ_work_local;
							//summ_work += (float)element.GetData("��������_��������_������") * (float)element.GetData("����������_��������_������");
						}
                        if (local_discount != 0.0F)
                        {
                            summ_work_local = (float)Math.Round((float)(summ_work_local - summ_work_local / 100 * local_discount), 0);
                            
                        }
                        else
                        {
                            summ_work_for_common_discount += summ_work_local;
                        }
                        summ_work += summ_work_local;
					}
				}
                
				// ������ ������� � �����-������
				details = new ArrayList();
				DbSqlCardDetail.SelectInArray(card, details);
				float summ_oil = 0.0F;
				float summ_detail = 0.0F;
                float summ_detail_for_common_discount = 0.0F;
				foreach(DtCardDetail element in details)
				{
					if((bool)element.GetData("��������_��������_������") == true)
					{
                        if ((bool)element.GetData("��������_��������_������") == false && (bool)element.GetData("�������") == false)
						{
							summ_oil += (float)element.GetData("����������_��������_������") * (float)element.GetData("����_��������_������");
						}
					}
					else
					{
						if((bool)element.GetData("��������_��������_������") == false && (bool)element.GetData("�������") == false)
						{
							//summ_detail += (float)element.GetData("����������_��������_������") * (float)element.GetData("����_��������_������");
                            summ_detail += element.DetailSummDiscount;
                            summ_detail_for_common_discount += element.DetailSummForCommonDiscount;
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
				float summ_work_pay		= 0.0F;
				float summ_work_oil		= 0.0F;
                //float discount_work		= 0.0F;
				//float discount_val		= 0.0F;
                
				discount_val	= (float)card.GetData("������_������_��������");
				if(discount_val != 0.0F)
				{
					discount_work = (float)Math.Round((float)(summ_work_for_common_discount / 100 * discount_val), 0);
				}
                // �����1000!!!!!!
                float summ_parts_pay = 0.0F;
                float discount_parts_val = 0.0F;
                float discount_parts = 0.0F;
                discount_parts_val = (float)card.GetData("������_������_��������");
                if (discount_parts_val > 0.0F) is_discount_parts = true;
                if (discount_parts_val != 0.0F)
                {
                    //discount_parts = (float)Math.Round((float)(summ_detail / 100 * discount_parts_val), 0);
                    discount_parts = (float)Math.Round((float)(summ_detail_for_common_discount / 100 * discount_parts_val), 0);
                }
                summ_parts_pay = summ_detail - discount_parts;
                txt_discount_parts = Db.CachToTxt(Math.Round(discount_parts, 2));
                txt_summ_parts_pay = Db.CachToTxt(Math.Round(summ_parts_pay, 2));
                // ����� �����1000!!!!
				summ_work_pay		= summ_oil + summ_work - discount_work;
				summ_work_oil		= summ_oil + summ_work;
                txt_summ_work =     Db.CachToTxt(Math.Round(summ_work_oil, 2));
                txt_discount_work = Db.CachToTxt(Math.Round(discount_work, 2));
                txt_summ_work_pay =  Db.CachToTxt(Math.Round(summ_work_pay, 2));
				txt_summ_detail		= Db.CachToTxt(Math.Round(summ_detail, 2));
				txt_summ_detail_pay	= Db.CachToTxt(Math.Round(summ_detail, 2));
				txt_summ_pay		= Db.CachToTxt(Math.Round(summ_detail + summ_work_pay, 2));
                // �����1000!!!!!!
                txt_summ_pay2 = Db.CachToTxt(Math.Round(summ_parts_pay + summ_work_pay, 2));
                // ����� �����10000!!!!

                if (thetype1 == 1)
                {
                    txt_summ_work = Db.CachToTxt(summ_work_oil);
                    txt_discount_work = Db.CachToTxt(discount_work);
                    txt_summ_work_pay = Db.CachToTxt(summ_work_pay);
                    txt_summ_detail = Db.CachToTxt(summ_detail);
                    txt_summ_detail_pay = Db.CachToTxt(summ_detail);
                    txt_summ_pay = Db.CachToTxt(summ_detail + summ_work_pay);
                    // �����1000!!!!!!
                    txt_summ_pay2 = Db.CachToTxt(summ_parts_pay + summ_work_pay);
                }
			}
		}
		HeaderData	header_data = null;
		#endregion

		public DbPrintWarrant(long card_number, int card_year, int type)
		{
            thetype = type;
			// ���������� ������������ ��� ������
			brush_standart		= new SolidBrush(Color.Black);
			brush_lightgray		= new SolidBrush(Color.LightGray);
			font_small_bold		= new Font("Arial", 8, FontStyle.Bold);
			font_small			= new Font("Arial", 8);
			font_small_cur		= new Font("Arial", 8, FontStyle.Italic);
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
				header_data = new HeaderData(card, type);
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
			int x1					= 55;
			int x2					= 25;
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
			if (header_data.txt_date_workend != "")
			{
				PrintText(graph, "�����-����� �" + header_data.txt_warrant_number, header_x, y, 150, 5, StringAlignment.Near, StringAlignment.Center, font_middle_bold, brush_standart, false);
				PrintText(graph, "���� ���������:", header_x + x1 - 50, y, 70, 5, StringAlignment.Far, StringAlignment.Center, font_small, brush_standart, false);
				PrintText(graph, header_data.txt_date_request, header_x + x1 + x2, y, 150, 5, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, false);
				y += 3;
				PrintText(graph, "������ �����:", header_x, y, 70, 5, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, false);
				PrintText(graph, header_data.txt_date_workbegin, header_x + x2 - 5, y, 150, 5, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, false);
				PrintText(graph, "���������� �����:", header_x + x1 - 45, y, 70, 5, StringAlignment.Far, StringAlignment.Center, font_small, brush_standart, false);
				PrintText(graph, header_data.txt_date_workend, header_x + x1 + x2, y, 150, 5, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, false);
				y += 3;
				PrintText(graph, "���� ������ ����������:", header_x, y, 70, 5, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, false);
				PrintText(graph, header_data.txt_date_givinout, header_x + x2 + 20, y, 150, 5, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, false);
				y += 4;
			}
			else
			{
				if(header_data.txt_warrant != "")
					PrintText(graph, "�����-����� �" + header_data.txt_warrant, header_x, y, 150, 5, StringAlignment.Near, StringAlignment.Center, font_middle_bold, brush_standart, false);
				else
					PrintText(graph, "�����-����� (�� ������)" + header_data.txt_warrant, header_x, y, 150, 5, StringAlignment.Near, StringAlignment.Center, font_middle_bold, brush_standart, false);
				y += 5;
				if(header_data.txt_warrant_close != "")
					PrintText(graph, "������ : " + header_data.txt_warrant_close, header_x, y, 150, 5, StringAlignment.Near, StringAlignment.Center, font_middle_bold, brush_standart, false);
				else
					PrintText(graph, "�� ������", header_x, y, 150, 5, StringAlignment.Near, StringAlignment.Center, font_middle_bold, brush_standart, false);
				y += 5;
			}
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
			size = MeasureTextBox(graph, header_data.txt_phone + " | " + header_data.txt_mail, header_data_width, text_offset, StringAlignment.Near, StringAlignment.Near, font_small_bold);
			height = (int)Math.Ceiling(size.Height);
			PrintTextBox(graph, "������� | e-mail:", header_x, y, header_title_width, height/*header_line_height*/, text_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, header_data.txt_phone + " | " + header_data.txt_mail, header_x + header_title_width, y, header_data_width, height/*header_line_height*/, text_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			y += height /*header_line_height*/;
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
			/* ������ �������
			if(header_data.juridical == true)
			{
				txt = FileIni.GetParameter("print.ini", "#ADDRESS_BLOCK_J");
			}
			else
			{
				txt = FileIni.GetParameter("print.ini", "#ADDRESS_BLOCK");
			}
			*/
			txt = FileIni.GetParameter("print.ini", "#ADDRESS_BLOCK");
			if(header_data.cashless == true)
			{
				txt = FileIni.GetParameter("print.ini", "#ADDRESS_BLOCK_J");
			}
			if(header_data.creditcard == true)
			{
				txt = FileIni.GetParameter("print.ini", "#ADDRESS_BLOCK_C");
			}
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

		#region ������ ����� �����
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
			int col3		=	15;
			int col4		=	20;
			int col2		=	page_width - offset_x_left - offset_x_right - col1 - col3 - col4;
			int	rowheight	= 6;

			if(test == true || print == false)
			{	
				y += rowheight;
				return y;
			}

			// ������ �������
			PrintTextBox(graph, "��� ������", offset_x_left, y, col1, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "������������ ������", offset_x_left + col1, y, col2, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "��", offset_x_left + col1 + col2, y, col3, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBoxColor(graph, "�����", offset_x_left + col1 + col2 + col3, y, col4, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, brush_lightgray, false);
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
			int col3		=	15;
			int col4		=	20;
			int col2		=	page_width - offset_x_left - offset_x_right - col1 - col3 - col4;
			int	rowheight	= 8;
			
			if(o == null) return y;
			string txt;
			string txt1;
			string txt2;
			string txt3 = "";
			string txt_ex = "";
			DtCardWork work = (DtCardWork)o;
			txt		= (string)work.GetData("�����_�������_��������_������");
			txt1	= (string)work.GetData("������������_��������_������");
            if (work.IsDiscount)
                txt1 += "/������ " + work.GetData("������_��������_������") + "%";

			// ��� �������������, ��������� ���������� � ����������� ������
			int pos = (int)work.GetData("�������_��������_������");
			if(pos < header_data.works_comments.Count && header_data.works_comments[pos] != null)
			{
				ArrayList arr = (ArrayList)header_data.works_comments[pos];
				foreach(DtCardWorkComment cmnt in arr)
				{
					txt_ex += cmnt.text + "\n";
				}
				//txt1 += "\n" + txt_ex;
			}

			if ((float)work.GetData("������������_��������_������") != 0.0F)
				txt2	= work.GetData("������������_��������_������").ToString();
			else	
				txt2	= "��";
			if ((float)work.GetData("����������_��������_������") > 1.0F)
			{
				txt2 = work.GetData("����������_��������_������").ToString() + "*" + txt2;
			}
			
			if ((bool)work.GetData("��������_��������_������") == true)
				txt3	= "��������";
			else
			{
				// ����������� ����� ���������� �����, � ���������� �����������
				float work_summ		= 0.0F;
				float work_val		= (float)work.GetData("������������_��������_������");
				float work_cost		= (float)work.GetData("��������_��������_������");
				float work_quontity	= (float)work.GetData("����������_��������_������");
                float work_discount = (float)work.GetData("������_��������_������");
				if(work_val == 0.0F)
				{
					work_summ		= work_cost * work_quontity;
				}
				else
				{
					work_summ		= work_cost * work_quontity * work_val;
				}

                if (work_discount != 0.0F)
                {
                    work_summ = work_summ -  work_summ * work_discount / 100;
                }
                
                if(thetype != 1)
				    work_summ			= (float)Math.Round(work_summ, 0);
                
				txt3				= Db.CachToTxt(work_summ);
                if (work_discount == 100.0F) txt3 = "�����";

				//if ((float)work.GetData("������������_��������_������") == 0.0F)
				//{
				//	txt3 = Db.CachToTxt((float)work.GetData("��������_��������_������")*(float)work.GetData("����������_��������_������")); 
				//}
				//else
				//{
				//	txt3 = Db.CachToTxt((float)work.GetData("������������_��������_������")*(float)work.GetData("����������_��������_������") * (float)work.GetData("��������_��������_������")); 
				//}
			}
			
			
			// ������ �����
			SizeF size1 = MeasureText(graph, txt, col1 - 2, StringAlignment.Near, StringAlignment.Center, font_small);
			SizeF size = MeasureText(graph, txt1, col2 - 2, StringAlignment.Near, StringAlignment.Center, font_small);
			// ���
			SizeF size_ex;
			size_ex = MeasureText(graph, txt_ex, col2 - 7, StringAlignment.Near, StringAlignment.Center, font_small_cur);
			if(txt_ex != "")
				size.Height = size.Height + size_ex.Height;
			
			int rowheight_ex = 0;
			if(txt_ex != "")
				rowheight_ex = (int)Math.Ceiling(size_ex.Height);

			if(size.Height < size1.Height) size = size1;
			rowheight	= (int)Math.Ceiling(size.Height);
			if(test == true || print == false)
			{	
				return y + rowheight;
			}
			PrintTextBox(graph, txt, offset_x_left, y, col1, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
			//PrintTextBox(graph, txt1, offset_x_left + col1, y , col2, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
			// ���
			PrintTextBox(graph, "", offset_x_left + col1, y , col2, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
			PrintTextNoBox(graph, txt1, offset_x_left + col1, y , col2, rowheight  - rowheight_ex, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
			PrintTextNoBox(graph, txt_ex, offset_x_left + col1 + 5, y + rowheight - rowheight_ex, col2, rowheight_ex, 1, StringAlignment.Near, StringAlignment.Center, font_small_cur, brush_standart, pen_thin, false);

			PrintTextBox(graph, txt2, offset_x_left + col1 + col2, y, col3, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
			PrintTextBoxColor(graph, txt3, offset_x_left + col1 + col2 + col3 , y, col4, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small, brush_standart, pen_thin, brush_lightgray, false);
			y += rowheight;
			return y;
		}
		private int PrintWorkGoodsBlock(Graphics graph, int offset,  bool test, bool print, object o)
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
			int col3		=	15;
			int col4		=	20;
			int col2		=	page_width - offset_x_left - offset_x_right - col1 - col3 - col4;
			int	rowheight		= 8;
			int	add_rowheight	= 0;
			bool have_goods		= false;
			
			
			string txt;
			string txt1 = "";
			string txt2;
			string txt3 = "";
			
			// ����������� �� ������� ������� ������� �����!
			float summ = 0.0F;
			foreach(DtCardDetail element in header_data.details)
			{
				if((bool)element.GetData("��������_��������_������") == true)
				{
					if(txt1 != "") txt1 += "\n";
					txt1 += element.GetData("����������_��������_������").ToString() + " * " + element.GetData("������������_��������_������");
					have_goods = true;
					if((bool)element.GetData("��������_��������_������") == false)
					{
						summ += (float)element.GetData("����������_��������_������") * (float)element.GetData("����_��������_������");
					}
				}
			}
			txt		= "��������� ���������";
			txt3	= Db.CachToTxt(Math.Round(summ, 2));
			if(have_goods == false) return y;
			
			// ������ �����
			SizeF size = MeasureText(graph, txt, col2 - 2, StringAlignment.Near, StringAlignment.Center, font_small);
			SizeF size1 = MeasureText(graph, txt1, col2 - 2, StringAlignment.Near, StringAlignment.Center, font_small);
			rowheight	= (int)Math.Ceiling(size.Height);
			add_rowheight	= (int)Math.Ceiling(size1.Height);
			if(test == true || print == false)
			{	
				return y + rowheight + add_rowheight;
			}
			PrintBox(graph, offset_x_left, y, col1, rowheight + add_rowheight, pen_thin);
			PrintBox(graph, offset_x_left + col1, y, col2, rowheight + add_rowheight, pen_thin);
			PrintBox(graph, offset_x_left + col1 + col2, y, col3, rowheight + add_rowheight, pen_thin);
			PrintBox(graph, offset_x_left + col1 + col2 + col3, y, col4, rowheight + add_rowheight, pen_thin);
			PrintTextBoxColor(graph, "", offset_x_left + col1 + col2 + col3, y, col4, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small, brush_standart, pen_thin, brush_lightgray, false);

			PrintTextNoBox(graph, "", offset_x_left, y, col1, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
			PrintTextNoBox(graph, txt, offset_x_left + col1, y, col2, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
			PrintTextNoBox(graph, "-", offset_x_left + col1 + col2, y, col3, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
			PrintTextNoBox(graph, txt3, offset_x_left + col1 + col2 + col3 , y, col4, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
			y += rowheight;			
			PrintTextNoBox(graph, txt1, offset_x_left + col1, y, col2, add_rowheight, 1, StringAlignment.Far, StringAlignment.Near, font_small, brush_standart, pen_thin, false);
			y += add_rowheight;			
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

			int w1			=	120;
			int w2			=	40;
			int	col1		=	20;
			int col3		=	15;
			int col4		=	20;
			int col2		=	page_width - offset_x_left - offset_x_right - col1 - col3 - col4;
			int	rowheight	= 5;

			y += 2;
			if(test == true || print == false)
			{	
				y += rowheight;
				y += rowheight;
				y += rowheight;
				return y;
			}

			// ������ �������
			PrintText(graph, "����� � ������:", offset_x_left + w1, y, w2, rowheight, StringAlignment.Far, StringAlignment.Center, font_small_bold, brush_standart, false);
			PrintTextBox(graph, header_data.txt_summ_work, offset_x_left + col1 + col2 + col3, y, col4, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			y += rowheight;
			PrintText(graph, "������:", offset_x_left + w1, y, w2, rowheight, StringAlignment.Far, StringAlignment.Center, font_small_bold, brush_standart, false);
			PrintTextBox(graph, header_data.txt_discount_work, offset_x_left + col1 + col2 + col3, y, col4, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			y += rowheight;
			PrintText(graph, "� ������:", offset_x_left + w1, y, w2, rowheight, StringAlignment.Far, StringAlignment.Center, font_small_bold, brush_standart, false);
			PrintTextBox(graph, header_data.txt_summ_work_pay, offset_x_left + col1 + col2 + col3, y, col4, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			y += rowheight;
			return y;
		}
		#endregion

		#region ������ ����� �������
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

			int	col1		=	30;
			int col3		=	15;
			int col4		=	20;
			int col5		=	20;
			int col6		=	20;
			int col2		=	page_width - offset_x_left - offset_x_right - col1 - col3 - col4 - col5 - col6;
			int	rowheight	= 6;

			y += 3;
			if(test == true || print == false)
			{	
				y += rowheight;
				return y;
			}

			// ������ �������
			PrintTextBox(graph, "��� ������", offset_x_left, y, col1, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "������������ ������", offset_x_left + col1, y, col2, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "���-��", offset_x_left + col1 + col2, y, col3, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "��. ���.", offset_x_left + col1 + col2 + col3, y, col4, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "����", offset_x_left + col1 + col2 + col3 + col4, y, col5, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBoxColor(graph, "�����", offset_x_left + col1 + col2 + col3 + col4 + col5, y, col6, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, brush_lightgray, false);
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

			int	col1		=	30;
			int col3		=	15;
			int col4		=	20;
			int col5		=	20;
			int col6		=	20;
			int col2		=	page_width - offset_x_left - offset_x_right - col1 - col3 - col4 - col5 - col6;
			int	rowheight	= 6;
			
			if(o == null) return y;
			string txt;
			string txt1;
			string txt2;
			string txt3 = "";
			string txt4 = "";
			string txt5 = "";
			DtCardDetail detail = (DtCardDetail)o;
			if((bool)detail.GetData("��������_��������_������") == true) return y; // ��� ���������� � ��������� ����������
			txt		= (string)detail.GetData("�������_�����_��������_������");
			txt1	= (string)detail.GetData("������������_��������_������");
            if (detail.IsDiscount)
                txt1 += "/������ " + detail.GetData("������") + "%";
			txt2	= detail.GetData("����������_��������_������").ToString();
			txt3	= detail.GetData("�������_���������_��������_������").ToString();
			txt4	= Db.CachToTxt((float)detail.GetData("����_��������_������"));
			if ((bool)detail.GetData("��������_��������_������") == true)
			{
				txt5	= "��������";
				txt4	= "-";
			}
			else
			{
                if ((bool)detail.GetData("�������") == true)
                {
                    txt5 = "�����";
                }
                else
                {
                    //txt5 = Db.CachToTxt((float)detail.GetData("����������_��������_������")*(float)detail.GetData("����_��������_������")); 
                    txt5 = Db.CachToTxt((float)detail.DetailSummDiscount);
                }
			}
			
			// ������ �����
			SizeF size = MeasureText(graph, txt, col1 - 2, StringAlignment.Near, StringAlignment.Center, font_small);
			SizeF size1 = MeasureText(graph, txt1, col2 - 2, StringAlignment.Near, StringAlignment.Center, font_small);
			if(size.Height < size1.Height) size = size1;
			rowheight	= (int)Math.Ceiling(size.Height);
			if(test == true || print == false)
			{	
				return y + rowheight;
			}
			PrintTextBox(graph, txt, offset_x_left, y, col1, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
			PrintTextBox(graph, txt1, offset_x_left + col1, y, col2, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
			PrintTextBox(graph, txt2, offset_x_left + col1 + col2, y, col3, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
			PrintTextBox(graph, txt3, offset_x_left + col1 + col2 + col3 , y, col4, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
			PrintTextBox(graph, txt4, offset_x_left + col1 + col2 + col3 + col4 , y, col5, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
			PrintTextBoxColor(graph, txt5, offset_x_left + col1 + col2 + col3 + col4 + col5, y, col6, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small, brush_standart, pen_thin, brush_lightgray, false);
			y += rowheight;
			return y;
		}
		private int PrintDetailBlockFooter(Graphics graph, int offset,  bool test, bool print, object o)
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
			int col3		=	15;
			int col4		=	20;
			int col5		=	20;
			int col6		=	20;
			int col2		=	page_width - offset_x_left - offset_x_right - col1 - col3 - col4 - col5 - col6;
			int	rowheight	= 5;
			int w1			=	120;
			int w2			=	40;

			y += 2;
			if(test == true || print == false)
			{	
				y += rowheight;
                // �����1000!!!!!
                if(header_data.is_discount_parts == true)
                    y += rowheight;
                // ����� �����1000
				y += rowheight + 2;
				y += rowheight;
				return y;
			}

			// ������ �������
			PrintText(graph, "����� � ������:", offset_x_left + w1, y, w2, rowheight, StringAlignment.Far, StringAlignment.Center, font_small_bold, brush_standart, false);
			PrintTextBox(graph, header_data.txt_summ_detail, offset_x_left + col1 + col2 + col3 + col4 + col5, y, col6, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
            // �����1000!!!!!
            if (header_data.is_discount_parts == true)
            {
                y += rowheight;
                PrintText(graph, "������:", offset_x_left + w1, y, w2, rowheight, StringAlignment.Far, StringAlignment.Center, font_small_bold, brush_standart, false);
                PrintTextBox(graph, header_data.txt_discount_parts, offset_x_left + col1 + col2 + col3 + col4 + col5, y, col6, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);

                y += rowheight;
                PrintText(graph, "� ������:", offset_x_left + w1, y, w2, rowheight, StringAlignment.Far, StringAlignment.Center, font_small_bold, brush_standart, false);
                PrintTextBox(graph, header_data.txt_summ_parts_pay, offset_x_left + col1 + col2 + col3 + col4 + col5, y, col6, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
                y += rowheight + 2;
                //PrintText(graph, "����� � ������ (��� ���):", offset_x_left + w1, y, w2, rowheight, StringAlignment.Far, StringAlignment.Center, font_small_bold, brush_standart, false);
                PrintText(graph, "����� � ������:", offset_x_left + w1, y, w2, rowheight, StringAlignment.Far, StringAlignment.Center, font_small_bold, brush_standart, false);
                PrintTextBox(graph, header_data.txt_summ_pay2, offset_x_left + col1 + col2 + col3 + col4 + col5, y, col6, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
                y += rowheight;
            }
            // ����������1000!!!!!
            else
            {
                y += rowheight;
                PrintText(graph, "� ������:", offset_x_left + w1, y, w2, rowheight, StringAlignment.Far, StringAlignment.Center, font_small_bold, brush_standart, false);
                PrintTextBox(graph, header_data.txt_summ_detail_pay, offset_x_left + col1 + col2 + col3 + col4 + col5, y, col6, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
                y += rowheight + 2;
                //PrintText(graph, "����� � ������ (��� ���):", offset_x_left + w1, y, w2, rowheight, StringAlignment.Far, StringAlignment.Center, font_small_bold, brush_standart, false);
                PrintText(graph, "����� � ������:", offset_x_left + w1, y, w2, rowheight, StringAlignment.Far, StringAlignment.Center, font_small_bold, brush_standart, false);
                PrintTextBox(graph, header_data.txt_summ_pay, offset_x_left + col1 + col2 + col3 + col4 + col5, y, col6, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
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
			int w1			= 70;
			int w2			= 20;
			int w3			= 30;
			int w4			= 30;
			int d1			= 5;
			int d2			= 100;
			int w5			= 100;
			int w6			= 140;
			int w7			= 60;
			int h1			= 10;
			int w8			= 50;
			int w9			= 150;
			int h2			= 4;
			int w10			= 10;
			int w11			= 20;
			int w12			= 35;

			y += 3;

			if(header_data.txt_recomendations != "")
			{
				size = this.MeasureText(graph, header_data.txt_recomendations, 190 - 4, StringAlignment.Near, StringAlignment.Center, font_small);
				recomendations_height	= (int)Math.Ceiling(size.Height);
				size = this.MeasureText(graph, "������������ :", 190 - 4, StringAlignment.Near, StringAlignment.Center, font_small_bold);
				recomendations_height	+= (int)Math.Round(size.Height);
			}

			txt = "-�������� �������� �������� ������, �������, ��������������� ������������ � �������������� �� ��� ���������� �����, ����������� 30 ���� ��� 1000 �� �������.";
			txt += "\n";
			txt += "-�������� �� �������� ���� ����� �������������� ���������� �������: �������������� ������ - 1000 ��; ������ �� ��������� ������� - 1000 ��. �� ������������������ ������ (����� ���������, �������, �����, ��������� �������� �����������������, ���� ���������, ��������� ������� � �.�.) �������� �� ����������������.";
			size = this.MeasureText(graph, txt, w9 - 4, StringAlignment.Center, StringAlignment.Center, font_small);

			if(test == true || print == false)
			{	
				y += recomendations_height + 1;
				// �������������� ���������� � ��������
				y += rowheight * 2 + 1;

				y += h1;
				y += rowheight;
				y += rowheight;
				y += 3;
				y += rowheight;
				y += rowheight;
				y += 3;
				y += h2 + (int)Math.Round(size.Height) + 1;
				return y;
			}
			
			if(recomendations_height != 0)
			{
				rect = new RectangleF(offset_x_left, y, 190, recomendations_height);
				PrintTextNoBox(graph, "������������ :", rect.X, rect.Y, rect.Width, rect.Height, 2, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, pen_thin, false);
				SizeF size1 = this.MeasureText(graph, "������������ :", 190 - 4, StringAlignment.Near, StringAlignment.Near, font_small_bold);
				PrintTextNoBox(graph, header_data.txt_recomendations, rect.X, rect.Y + (int)Math.Ceiling(size1.Height), rect.Width, rect.Height, 2, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, pen_thin, false);
				PrintRoundBox(graph, rect, pen_thin, 2);
				y += recomendations_height;
				y += 1;
			}

			// �������������� ���������� � ��������
			//rect = new RectangleF(offset_x_left, y, 190, rowheight * 2);
			//PrintTextNoBox(graph, "������������ ���� ������� ��� ���������� �������� ������ ���������� 45 ����. ��� ������� �������� ������ ������ ����� ���������� � ����������� �������� �����.", offset_x_left, y, 190, rowheight * 2, 4, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, pen_thin, false);
			//PrintRoundBox(graph, rect, pen_thin, 2);
			//y += rowheight * 2 + 1;
            // ������� �������� �� �������� ��������������� ����������
            rect = new RectangleF(offset_x_left, y, 190, rowheight * 3);
            PrintTextNoBox(graph, "��������� �� �������� ������� �������� �������� ������ � ��������� ����������, �������������� ���������� �� �������� � ����������� ����������. � ����� � ���, �� ������������� �������� �� ������, ����������� � �������������� ���� �������� ������ � ��������� ����������.", offset_x_left, y, 190, rowheight * 3, 4, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, pen_thin, false);
            PrintRoundBox(graph, rect, pen_thin, 2);
            y += rowheight * 3 + 1;

			// ������� ��� ������� �����
			rect = new RectangleF(w6, y, w7, h1 + rowheight * 2 + rowheight * 2 + 3);
			PrintTextNoBox(graph, "���� ������ / ������ �����", w6, y, w7, 4, 2, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintRoundBox(graph, rect, pen_thin, 5);

			// ������ �������
			y += h1;
			PrintText(graph, "������-�����������", offset_x_left, y, w1, rowheight, StringAlignment.Near, StringAlignment.Center, font_middle, brush_standart, false);
			y += rowheight;
			PrintLineHor(graph, offset_x_left + w1, y, w2, pen_thin, false);
			PrintLineHor(graph, offset_x_left + w1 + w2 + d1 , y, w3, pen_thin, false);
			PrintText(graph, "(�������)", offset_x_left + w1, y, w2, rowheight, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
			PrintText(graph, "(���������)", offset_x_left + w1 + w2 + d1, y, w3, rowheight, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
			y += rowheight;
			y += 3;
			//PrintText(graph, "� ������� ���������� ����� ��������", offset_x_left, y, w5, rowheight, StringAlignment.Near, StringAlignment.Center, font_middle, brush_standart, false);
			string txt_01 = "� ������� ���������� ����� ��������";
            //string txt_01 = "� ������� �������������������� ����� ��������";
			if(header_data.txt_warrant_close != "")
				txt_01 = "���������� �� ������� �������. � ������� ����������� ����� ��������. ���������� ������ ��� �����������. ��������� � �������� ������� �� ����.";
			PrintText(graph, txt_01, offset_x_left, y, w5 - 25, rowheight*5, StringAlignment.Near, StringAlignment.Center, font_middle, brush_standart, false);
			y += rowheight*4;
			PrintLineHor(graph, offset_x_left + w1, y, w2, pen_thin, false);
			PrintLineHor(graph, offset_x_left + w1 + w2 + d1 , y, w3, pen_thin, false);
			PrintText(graph, "(�������)", offset_x_left + w1, y, w2, rowheight, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
			PrintText(graph, "(���������)", offset_x_left + w1 + w2 + d1, y, w3, rowheight, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
			y += rowheight;
			y += 3;
			// ���� �������� �������
			
			rect = new RectangleF(w8, y, w9, h2 + (int)Math.Round(size.Height) + 1);
			PrintTextNoBox(graph, "������� ��������", w8, y, w9, h2, 2, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextNoBox(graph, txt, w8, y + h2, w9, size.Height, 2, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
			PrintRoundBox(graph, rect, pen_thin, 5);

			// ���� �������� ����� ������
			rect = new RectangleF(w10, y, w12, h2 + (int)Math.Round(size.Height) + 1);
			PrintRoundBox(graph, rect, pen_thin, 3);
			PrintTextNoBox(graph, "����� ������", w10 + 1, y, w12 - 1, h2, 2, StringAlignment.Center, StringAlignment.Near, font_small_bold, brush_standart, pen_thin, false);
			/* ������ �������
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
			*/
			if(header_data.cashless == false && header_data.inner == false && header_data.creditcard == false)
			{
				PrintRightCheckBoxOChecked(graph, "��������", w10 + 1, y + 4, w11, 6, 1, 1, 0, StringAlignment.Near, font_small, brush_standart, pen_thin, false);
				PrintRightCheckBoxO(graph,			 "�����", w10 + 1, y + 9, w11, 6, 1, 1, 0, StringAlignment.Near, font_small, brush_standart, pen_thin, false);
				PrintRightCheckBoxO(graph,          "������", w10 + 1, y + 14, w11, 6, 1, 1, 0, StringAlignment.Near, font_small, brush_standart, pen_thin, false);
				PrintRightCheckBoxO(graph,          "�����.", w10 + 1, y + 19, w11, 6, 1, 1, 0, StringAlignment.Near, font_small, brush_standart, pen_thin, false);
			}
			if(header_data.cashless == false && header_data.inner == false && header_data.creditcard == true)
			{
				PrintRightCheckBoxO(graph,               "��������", w10 + 1, y + 4, w11, 6, 1, 1, 0, StringAlignment.Near, font_small, brush_standart, pen_thin, false);
				PrintRightCheckBoxOChecked(graph,			"�����", w10 + 1, y + 9, w11, 6, 1, 1, 0, StringAlignment.Near, font_small, brush_standart, pen_thin, false);
				PrintRightCheckBoxO(graph,                 "������", w10 + 1, y + 14, w11, 6, 1, 1, 0, StringAlignment.Near, font_small, brush_standart, pen_thin, false);
				PrintRightCheckBoxO(graph,                 "�����.", w10 + 1, y + 19, w11, 6, 1, 1, 0, StringAlignment.Near, font_small, brush_standart, pen_thin, false);
			}
			if(header_data.cashless == true && header_data.inner == false && header_data.creditcard == false)
			{
				PrintRightCheckBoxO(graph,        "��������", w10 + 1, y + 4, w11, 6, 1, 1, 0, StringAlignment.Near, font_small, brush_standart, pen_thin, false);
				PrintRightCheckBoxO(graph,			 "�����", w10 + 1, y + 9, w11, 6, 1, 1, 0, StringAlignment.Near, font_small, brush_standart, pen_thin, false);
				PrintRightCheckBoxOChecked(graph,   "������", w10 + 1, y + 14, w11, 6, 1, 1, 0, StringAlignment.Near, font_small, brush_standart, pen_thin, false);
				PrintRightCheckBoxO(graph,          "�����.", w10 + 1, y + 19, w11, 6, 1, 1, 0, StringAlignment.Near, font_small, brush_standart, pen_thin, false);
			}
			if(header_data.cashless == false && header_data.inner == true && header_data.creditcard == false)
			{
				PrintRightCheckBoxO(graph,        "��������", w10 + 1, y + 4, w11, 6, 1, 1, 0, StringAlignment.Near, font_small, brush_standart, pen_thin, false);
				PrintRightCheckBoxO(graph,			 "�����", w10 + 1, y + 9, w11, 6, 1, 1, 0, StringAlignment.Near, font_small, brush_standart, pen_thin, false);
				PrintRightCheckBoxO(graph,          "������", w10 + 1, y + 14, w11, 6, 1, 1, 0, StringAlignment.Near, font_small, brush_standart, pen_thin, false);
				PrintRightCheckBoxOChecked(graph,   "�����.", w10 + 1, y + 19, w11, 6, 1, 1, 0, StringAlignment.Near, font_small, brush_standart, pen_thin, false);
			}

			y += h2 + (int)Math.Round(size.Height) + 1;
			return y;
		}
		#endregion

		#region ����������� ���������
		override public int PrintStandartHead(Graphics graph, int offset, bool test, bool print, object print_data)
		{
			// ������ ��������� �� ������ ����������� ��������
			return PrintHead(graph, offset,  test, print, null);
		}
		#endregion

		// �������� ��������� ������
		public override void  PrintPage(Graphics graph, int page)
		{
			// ��� ���������� �� ��������
			int offset = 0;

			offset = 10;
			offset = PrintBlock(graph, offset, new DelegatePrintBlock(PrintHead), null);
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
			offset = PrintBlock(graph, offset, new DelegatePrintBlock(PrintWorkGoodsBlock), null);
			offset = PrintBlock(graph, offset, new DelegatePrintBlock(PrintWorkBlockFooter), null);
			first = true;
			if(header_data.details.Count > 0)
			{
				foreach(DtCardDetail element in header_data.details)
				{
					offset = PrintBlockWithHeader(graph, offset, new DelegatePrintBlock(PrintDetailBlock), element, new DelegatePrintBlock(PrintDetailBlockHead), first);
					first = false;
				}
			}
			else
			{
				offset = PrintBlockWithHeader(graph, offset, new DelegatePrintBlock(PrintDetailBlock), null, new DelegatePrintBlock(PrintDetailBlockHead), first);
			}
			offset = PrintBlock(graph, offset, new DelegatePrintBlock(PrintDetailBlockFooter), null);
			offset = PrintBlock(graph, offset, new DelegatePrintBlock(PrintFooter), null);
		}
	}
}
