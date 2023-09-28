using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DBPrintReportCallService.
	/// </summary>
	public class DBPrintReportCallService:DbPrint
	{
		// ����������� ��� ������
		SolidBrush	brush_standart;
		SolidBrush	brush_lightgray;
		Font		font_small_bold;
		Font		font_middle_bold;
		Font		font_middle;
		Font		font_small;
		Pen			pen_thin;

		#region ������ ��� ������
		protected class HeaderData
		{
			public struct CardInfo
			{
				public long warrant_number;
				public DateTime warrant_date;
				public string txt_auto;
				public string txt_client;
				public string client_name;
				public string represent_name;
				public string client_contact;
				public string client_old_contact;
				public string txt_warrant_number;
				public string txt_warrant_date;
			};
			
			public string txt_date;
			public string txt_service_manager;
			public ArrayList closed_cards = null;
			
			public HeaderData(DateTime date, long service_manager)
			{
				ArrayList cards = null;
				ArrayList tmp = null;
				
				// ������ ������
				txt_date = date.ToShortDateString();
				DtStaff staff = DbSqlStaff.Find(service_manager);
				if (staff == null)
					txt_service_manager = "������";
				else
					txt_service_manager = staff.Title;


				#region ������
				// ����� �� �������������: ������
				cards = new ArrayList();
				DbSqlCard.SelectCardClosedNumberWorkshopNal(cards, date, date, 1);
				foreach(object o in cards)
				{
					DtCard card = (DtCard)o;
					card = DbSqlCard.Find((long)card.GetData("�����_��������"), (int)card.GetData("���_��������"));
					if(card != null)
					{
						DtTxtCard txtCard = new DtTxtCard(card);
						string txt_auto = "";
						string card_date = txtCard.Date;//(string)card.GetDataTxt("����");
						string card_number = txtCard.WarrantNumber;// (string)card.GetDataTxt("�����_�����_��������");
						// ��������� ������ ������������
						long card_service_manager = (long)card.GetData("������_�����������");
						if(card_service_manager == service_manager)
						{
							// ���������� ������������� ��������!
							// �������� ��� ������ ��������
							tmp = new ArrayList();
							DbSqlCardWork.SelectInArray(card, tmp);
							bool garant = false;
							bool to = false;
							foreach(DtCardWork work in tmp)
							{
								// ��������� ��������
								DtCardWork fwork = DbSqlCardWork.Find(card, (int)work.GetData("�������_��������_������"));
								if (fwork != null)
								{
									to = to | fwork.IsTo();
									garant = garant | fwork.GuaranteeFlag();
								}
							}
							if (to == true || garant == true)
							{
								// �������� ������ �������� � ������ �� � ������
								//��������_��������
								//�������������_��������
								string txt_client = "";
								string client_old_contact = "";
								string client_contact = "";

								long client_code = (long)card.GetData("��������_��������");
								DtPartner client = DbSqlPartner.Find(client_code);
								long represent_code = (long)card.GetData("�������������_��������");
								DtPartner represent = DbSqlPartner.Find(represent_code);
								if (represent != null)
								{
									txt_client = represent.GetTitle();
									client_old_contact = represent.GetPhone();
								}
								else
								{
									if(client != null)
									{
										txt_client = client.GetTitle();
										client_old_contact = client.GetPhone();
									}
								}
								//����������_��������
								long auto_code = (long)card.CodeAuto;// GetData("����������_��������");
								DtAuto auto = DbSqlAuto.Find(auto_code);
								if (auto == null)
									txt_auto = "������";
								else
								{
									txt_auto = (string)auto.GetData("VIN");
								}
								// ��������
								ArrayList contacts = new ArrayList();
								if(represent_code != 0)
								{
									DbSqlPartnerContact.SelectInArray(contacts, represent_code);
									foreach (DtPartnerContact contact in contacts)
									{
										client_contact += "; " + contact.ContactTxt;
									}
								}
								else
								{
									if(client_code != 0)
									{
										DbSqlPartnerContact.SelectInArray(contacts, client_code);
										foreach (DtPartnerContact contact in contacts)
										{
											client_contact += "; " + contact.ContactTxt;
										}
									}
								}

								// ����������� ����?
								bool juridical = true;
								if(client != null)
								{
									juridical = (bool)client.GetData("�����������_����");
								}

								// ��������� ������, ���� ���� ����������
								if (juridical == false)
								{
									CardInfo card_info = new CardInfo();
									card_info.client_contact = client_contact;
									card_info.client_old_contact = client_old_contact;
									card_info.txt_client = txt_client;
									card_info.txt_auto = txt_auto;
									card_info.txt_warrant_number = card_number;
									card_info.txt_warrant_date = card_date;
									if (closed_cards == null)
										closed_cards = new ArrayList();
									closed_cards.Add(card_info);
								}
							}
						}
					}
				}
				#endregion
			}
		}
		HeaderData	header_data = null;
		#endregion

		public DBPrintReportCallService(DateTime date, long code_service_manager)
		{
			// ���������� ������������ ��� ������
			brush_standart		= new SolidBrush(Color.Black);
			brush_lightgray		= new SolidBrush(Color.LightGray);
			font_small_bold		= new Font("Arial", 8, FontStyle.Bold);
			font_small			= new Font("Arial", 8);
			font_middle_bold	= new Font("Arial", 10, FontStyle.Bold);
			font_middle			= new Font("Arial", 10);
			pen_thin			= new Pen(brush_standart, 0.3F);

			header_data = new HeaderData(date, code_service_manager);
		}

		#region ������ ����� ���������
		private int PrintMain(Graphics graph, int offset,  bool test, bool print, object o)
		{
			// ��������� ���������� ������ ���������
			int	y;
			int offset_x		= 10;
			int page_width		= 190;
			int title_height	= 8;

			if(test == true || print == false)
			{
				// ������ ������������ ������
				y = offset;
				y += title_height*3;
				return y ;
			}

			y	= offset;
			PrintText(graph, "���� ������� �� " + header_data.txt_date, offset_x, y, page_width, title_height, StringAlignment.Near, StringAlignment.Center, font_middle_bold, brush_standart, false);
			y += title_height;
			PrintText(graph, "������ ����������� " + header_data.txt_service_manager, offset_x, y, page_width, title_height, StringAlignment.Near, StringAlignment.Center, font_middle_bold, brush_standart, false);
			y += title_height;
			PrintText(graph, "������ " + DateTime.Now.ToLongDateString()+ " " + DateTime.Now.ToLongTimeString(), offset_x, y, page_width, title_height, StringAlignment.Near, StringAlignment.Center, font_middle_bold, brush_standart, false);
			y += title_height;
			return y;
		}
		private int PrintServiceBlock(Graphics graph, int offset,  bool test, bool print, object o)
		{
			// ��������� ���������� ������ ���������
			SizeF size;
			int	height;
			int	y;
			string txt	= "";
		

			int offset_x		= 10;
			int page_width		= 190;
			int height1	= 5;
			int height2	= 12;
			int col1			= 30;
			int col2			= 60;
			int col3			= 20;
			int col4			= 35;
			int col5			= 30;
			int col6			= 15;

			int txt_offset		= 1;
			int interval		= 4;

			if(test == true || print == false || o == null)
			{
				// ������ ������������ ������
				y = offset;
				y += height1*4;
				y += interval;
				return y ;
			}

			HeaderData.CardInfo data;
			data = (HeaderData.CardInfo)o;

			y	= offset;
			PrintTextBox(graph, "��������", offset_x, y, col1, height1, txt_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, data.txt_warrant_number + "/" + data.txt_warrant_date, offset_x + col1, y, col2, height1, txt_offset, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
			PrintTextBox(graph, "�� ������", offset_x + col1 + col2, y, col3, height1, txt_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "������ ������ �/�", offset_x + col1 + col2 + col3, y, col4, height1, txt_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_lightgray, pen_thin, false);
			PrintTextBox(graph, "����������", offset_x + col1 + col2 + + col3 + col4, y, col5, height1, txt_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "��  ���", offset_x + col1 + col2 + col3 + col4 + col5, y, col6, height1, txt_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_lightgray, pen_thin, false);
			y += height1;

			PrintTextBox(graph, "���������� ", offset_x, y, col1, height1, txt_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, data.txt_auto, offset_x + col1, y, col2, height1, txt_offset, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
			PrintTextBox(graph, "��������", offset_x + col1 + col2, y, col3, height1, txt_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "��  ��� ��������", offset_x + col1 + col2 + col3, y, col4, height1, txt_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_lightgray, pen_thin, false);
			PrintTextBox(graph, "����� ������", offset_x + col1 + col2 + + col3 + col4, y, col5, height1, txt_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "��  ���", offset_x + col1 + col2 + col3 + col4 + col5, y, col6, height1, txt_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_lightgray, pen_thin, false);
			y += height1;

			PrintTextBox(graph, "������ ", offset_x, y, col1, height1, txt_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, data.txt_client, offset_x + col1, y, col2, height1, txt_offset, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
			PrintTextBox(graph, "��� ��� ����.���� ����� ����� ������ ������ ������", offset_x + col1 + col2, y, col3 + col4 + col5 + col6, height1, txt_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_lightgray, pen_thin, false);
			y += height1;

			size = MeasureTextBox(graph, data.client_contact + " / " + data.client_old_contact, col2, txt_offset, StringAlignment.Near, StringAlignment.Center, font_small);
			height2 = (int)Math.Ceiling(size.Height);

			PrintTextBox(graph, "������� ", offset_x, y, col1, height2, txt_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, data.client_contact + " / " + data.client_old_contact, offset_x + col1, y, col2, height2, txt_offset, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
			PrintTextBox(graph, "", offset_x + col1 + col2, y, col3 + col4 + col5 + col6, height2, txt_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_lightgray, pen_thin, false);
			y += height2;
			y += interval;
			return y;
		}
		private int PrintServiceBlockHead(Graphics graph, int offset,  bool test, bool print, object o)
		{
			// ��������������� �����������
			RectangleF rect;
			int y		= offset;
			int y1		= 0;
			string text	= "";
			

			// ����������� ���������
			int offset_x_left		= 10;
			int offset_x_right		= 10;
			int page_width			= 210;
			int title_height		= 4;
			int	col1				= 190;
			int offset1				= 12;
			int offset2				= 80;
			int x1					= 30;
			int x2					= 20;

			
			if(test == true || print == false || o == null)
			{	
				y += title_height * 5;
				return y;
			}

			HeaderData.CardInfo data;
			data = (HeaderData.CardInfo)o;

			// ������ �������
			PrintTextBox(graph, "�����������. �� ���� � ��� �� ������������ � (��, ��������). ��������� �� ���������� �������?", offset_x_left, y, col1, title_height * 5, 1, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, pen_thin, false);
			y += title_height;
			PrintText(graph, "���������� �� ����� ����������? ���� ����� ����������� � ��������� �������?", offset_x_left, y, col1, title_height * 5, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, false);
			y += title_height;
			PrintText(graph, "��������� ��� ��������� �� ��������� ������?", offset_x_left, y, col1, title_height * 5, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, false);
			y += title_height;
			PrintText(graph, "������ - �������� ������ ������ �� �������.", offset_x_left, y, col1, title_height * 5, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, false);
			y1 = y;
			y += title_height * 2;
			return y;
		}
		#endregion

		// �������� ��������� ������
		public override void  PrintPage(Graphics graph, int page)
		{
			// ��� ���������� �� ��������
			int offset = 0;
			offset = 10;
			offset = PrintBlock(graph, offset, new DelegatePrintBlock(PrintMain), null);
			// ������ ��� ����� ������� �� ������������� ������
			bool first = true;
			if(header_data.closed_cards != null && header_data.closed_cards.Count > 0)
			{
				foreach(HeaderData.CardInfo element in header_data.closed_cards)
				{
					offset = PrintBlockWithHeader(graph, offset, new DelegatePrintBlock(PrintServiceBlock), element, new DelegatePrintBlock(PrintServiceBlockHead), first);
					first = false;
				}
			}
			else
			{
				offset = PrintBlockWithHeader(graph, offset, new DelegatePrintBlock(PrintServiceBlock), null, new DelegatePrintBlock(PrintServiceBlockHead), first);
			}
		}
	}
}
