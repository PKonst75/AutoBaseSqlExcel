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
		// Èíñòðóìåíòû äëÿ ïå÷àòè
		SolidBrush	brush_standart;
		SolidBrush	brush_lightgray;
		Font		font_small_bold;
		Font		font_middle_bold;
		Font		font_middle;
		Font		font_small;
		Pen			pen_thin;

		#region Äàííûå äëÿ ïå÷àòè
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
				
				// Äàííûå îò÷åòà
				txt_date = date.ToShortDateString();
				DtStaff staff = DbSqlStaff.Find(service_manager);
				if (staff == null)
					txt_service_manager = "ÎØÈÁÊÀ";
				else
					txt_service_manager = staff.Title;


				#region Ñåðâèñ
				// Îò÷åò ïî ïîäðàçäåëåíèþ: ñåðâèñ
				cards = new ArrayList();
				DbSqlCard.SelectCardClosedNumberWorkshopNal(cards, date, date, 1);
				foreach(object o in cards)
				{
					DtCard card = (DtCard)o;
					card = DbSqlCard.Find((long)card.GetData("ÍÎÌÅÐ_ÊÀÐÒÎ×ÊÀ"), (int)card.GetData("ÃÎÄ_ÊÀÐÒÎ×ÊÀ"));
					if(card != null)
					{
						DtTxtCard txtCard = new DtTxtCard(card);
						string txt_auto = "";
						string card_date = txtCard.Date;//(string)card.GetDataTxt("ÄÀÒÀ");
						string card_number = txtCard.WarrantNumber;// (string)card.GetDataTxt("ÍÎÌÅÐ_ÍÀÐßÄ_ÊÀÐÒÎ×ÊÀ");
						// Ïðîâåðÿåì ñåðâèñ êîíñóëüòàíòà
						long card_service_manager = (long)card.GetData("ÑÅÐÂÈÑ_ÊÎÍÑÓËÜÒÀÍÒ");
						if(card_service_manager == service_manager)
						{
							// Ïðîäàëæàåì àíàëèçèðîâàòü êàðòî÷êó!
							// Ïîëó÷àåì âñå ðàáîòû êàðòî÷êè
							tmp = new ArrayList();
							DbSqlCardWork.SelectInArray(card, tmp);
							bool garant = false;
							bool to = false;
							foreach(DtCardWork work in tmp)
							{
								// Ïðîâåðÿåì ãàðàíòèþ
								DtCardWork fwork = DbSqlCardWork.Find(card, (int)work.GetData("ÏÎÇÈÖÈß_ÊÀÐÒÎ×ÊÀ_ÐÀÁÎÒÀ"));
								if (fwork != null)
								{
									to = to | fwork.IsTo();
									garant = garant | fwork.GuaranteeFlag();
								}
							}
							if (to == true || garant == true)
							{
								// Ñîáèðàåì äàííûå êàðòî÷êè è âíîñèì åå â ñïèñîê
								//ÂËÀÄÅËÅÖ_ÊÀÐÒÎ×ÊÀ
								//ÏÐÅÄÑÒÀÂÈÒÅËÜ_ÊÀÐÒÎ×ÊÀ
								string txt_client = "";
								string client_old_contact = "";
								string client_contact = "";

								long client_code = (long)card.GetData("ÂËÀÄÅËÅÖ_ÊÀÐÒÎ×ÊÀ");
								DtPartner client = DbSqlPartner.Find(client_code);
								long represent_code = (long)card.GetData("ÏÐÅÄÑÒÀÂÈÒÅËÜ_ÊÀÐÒÎ×ÊÀ");
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
								//ÀÂÒÎÌÎÁÈËÜ_ÊÀÐÒÎ×ÊÀ
								long auto_code = (long)card.CodeAuto;// GetData("ÀÂÒÎÌÎÁÈËÜ_ÊÀÐÒÎ×ÊÀ");
								DtAuto auto = DbSqlAuto.Find(auto_code);
								if (auto == null)
									txt_auto = "ÎØÈÁÊÀ";
								else
								{
									txt_auto = (string)auto.GetData("VIN");
								}
								// ÊÎÍÒÀÊÒÛ
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

								// ÞÐÈÄÈ×ÅÑÊÎÅ ËÈÖÎ?
								bool juridical = true;
								if(client != null)
								{
									juridical = (bool)client.GetData("ÞÐÈÄÈ×ÅÑÊÎÅ_ËÈÖÎ");
								}

								// Çàïîëíÿåì äàííûå, åñëè ëèöî ôèçè÷åñêîå
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
			// Ïîäãîòîâêà èíñòðóìåíòîâ äëÿ ïå÷àòè
			brush_standart		= new SolidBrush(Color.Black);
			brush_lightgray		= new SolidBrush(Color.LightGray);
			font_small_bold		= new Font("Arial", 8, FontStyle.Bold);
			font_small			= new Font("Arial", 8);
			font_middle_bold	= new Font("Arial", 10, FontStyle.Bold);
			font_middle			= new Font("Arial", 10);
			pen_thin			= new Pen(brush_standart, 0.3F);

			header_data = new HeaderData(date, code_service_manager);
		}

		#region Ïå÷àòü Áëîêà çàãîëîâêà
		private int PrintMain(Graphics graph, int offset,  bool test, bool print, object o)
		{
			// Íàñòðîéêà ïàðàìåòðîâ ïå÷àòè çàãîëîâêà
			int	y;
			int offset_x		= 10;
			int page_width		= 190;
			int title_height	= 8;

			if(test == true || print == false)
			{
				// Ïðîñòî ðàññ÷èòûâàåì âûñîòó
				y = offset;
				y += title_height*3;
				return y ;
			}

			y	= offset;
			PrintText(graph, "ËÈÑÒ ÎÁÇÂÎÍÀ íà " + header_data.txt_date, offset_x, y, page_width, title_height, StringAlignment.Near, StringAlignment.Center, font_middle_bold, brush_standart, false);
			y += title_height;
			PrintText(graph, "ÑÅÐÂÈÑ ÊÎÍÑÓËÜÒÀÍÒ " + header_data.txt_service_manager, offset_x, y, page_width, title_height, StringAlignment.Near, StringAlignment.Center, font_middle_bold, brush_standart, false);
			y += title_height;
			PrintText(graph, "ÑÎÇÄÀÍ " + DateTime.Now.ToLongDateString()+ " " + DateTime.Now.ToLongTimeString(), offset_x, y, page_width, title_height, StringAlignment.Near, StringAlignment.Center, font_middle_bold, brush_standart, false);
			y += title_height;
			return y;
		}
		private int PrintServiceBlock(Graphics graph, int offset,  bool test, bool print, object o)
		{
			// Íàñòðîéêà ïàðàìåòðîâ ïå÷àòè çàãîëîâêà
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
				// Ïðîñòî ðàññ÷èòûâàåì âûñîòó
				y = offset;
				y += height1*4;
				y += interval;
				return y ;
			}

			HeaderData.CardInfo data;
			data = (HeaderData.CardInfo)o;

			y	= offset;
			PrintTextBox(graph, "ÊÀÐÒÎ×ÊÀ", offset_x, y, col1, height1, txt_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, data.txt_warrant_number + "/" + data.txt_warrant_date, offset_x + col1, y, col2, height1, txt_offset, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
			PrintTextBox(graph, "ÍÅ ÇÂÎÍÈË", offset_x + col1 + col2, y, col3, height1, txt_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "ÌÎÄÅËÜ ÊËÈÅÍÒ Ç/×", offset_x + col1 + col2 + col3, y, col4, height1, txt_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_lightgray, pen_thin, false);
			PrintTextBox(graph, "ÄÎÇÂÎÍÈËÑß", offset_x + col1 + col2 + + col3 + col4, y, col5, height1, txt_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "ÄÀ  ÍÅÒ", offset_x + col1 + col2 + col3 + col4 + col5, y, col6, height1, txt_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_lightgray, pen_thin, false);
			y += height1;

			PrintTextBox(graph, "ÀÂÒÎÌÎÁÈËÜ ", offset_x, y, col1, height1, txt_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, data.txt_auto, offset_x + col1, y, col2, height1, txt_offset, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
			PrintTextBox(graph, "ÓÑÒÐÀÍÅÍ", offset_x + col1 + col2, y, col3, height1, txt_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "ÄÀ  ÍÅÒ ×ÀÑÒÈ×ÍÎ", offset_x + col1 + col2 + col3, y, col4, height1, txt_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_lightgray, pen_thin, false);
			PrintTextBox(graph, "ÍÎÂÛÅ ÆÀËÎÁÛ", offset_x + col1 + col2 + + col3 + col4, y, col5, height1, txt_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "ÄÀ  ÍÅÒ", offset_x + col1 + col2 + col3 + col4 + col5, y, col6, height1, txt_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_lightgray, pen_thin, false);
			y += height1;

			PrintTextBox(graph, "ÊËÈÅÍÒ ", offset_x, y, col1, height1, txt_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, data.txt_client, offset_x + col1, y, col2, height1, txt_offset, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
			PrintTextBox(graph, "ÎÒË ÕÎÐ ÌÎÃË.ËÓ×Ø ÓÄÎÂË ÏËÎÕÎ ÓÆÀÑÍÎ ÍÅÓÃÎÄ ÇÀÒÐÓÄ", offset_x + col1 + col2, y, col3 + col4 + col5 + col6, height1, txt_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_lightgray, pen_thin, false);
			y += height1;

			size = MeasureTextBox(graph, data.client_contact + " / " + data.client_old_contact, col2, txt_offset, StringAlignment.Near, StringAlignment.Center, font_small);
			height2 = (int)Math.Ceiling(size.Height);

			PrintTextBox(graph, "ÊÎÍÒÀÊÒ ", offset_x, y, col1, height2, txt_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, data.client_contact + " / " + data.client_old_contact, offset_x + col1, y, col2, height2, txt_offset, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
			PrintTextBox(graph, "", offset_x + col1 + col2, y, col3 + col4 + col5 + col6, height2, txt_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_lightgray, pen_thin, false);
			y += height2;
			y += interval;
			return y;
		}
		private int PrintServiceBlockHead(Graphics graph, int offset,  bool test, bool print, object o)
		{
			// Âñïîìîãàòåëüíûå èíñòðóìåíòû
			RectangleF rect;
			int y		= offset;
			int y1		= 0;
			string text	= "";
			

			// Íàñòðîå÷íûå ïàðàìåòðû
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

			// Ïåðâàÿ ñòðî÷êà
			PrintTextBox(graph, "Ïðèâåòñòâèå. Âû áûëè ó íàñ íà îáñëóæèâàíèè ñ (ÒÎ, ÃÀÐÀÍÒÈß). Óñòðàíåíû ëè çàÿâëåííûå äåôåêòû?", offset_x_left, y, col1, title_height * 5, 1, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, pen_thin, false);
			y += title_height;
			PrintText(graph, "Ïîÿâëèëèñü ëè íîâûå íåäîñòàòêè? Âàøå îáùåå âïå÷àòëåíèå î ïîñåùåíèå ñòàíöèè?", offset_x_left, y, col1, title_height * 5, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, false);
			y += title_height;
			PrintText(graph, "Çàìå÷àíèÿ èëè ïîæåëàíèÿ ïî óëó÷øåíèþ ðàáîòû?", offset_x_left, y, col1, title_height * 5, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, false);
			y += title_height;
			PrintText(graph, "ÓÆÀÑÍÎ - îçíà÷àåò êëèåíò áîëüøå íå ïðèåäåò.", offset_x_left, y, col1, title_height * 5, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, false);
			y1 = y;
			y += title_height * 2;
			return y;
		}
		#endregion

		// Îñíîâíàÿ ïðîöåäóðà ïå÷àòè
		public override void  PrintPage(Graphics graph, int page)
		{
			// Äëÿ îðèåíòàöèè íà ñòðàíèöå
			int offset = 0;
			offset = 10;
			offset = PrintBlock(graph, offset, new DelegatePrintBlock(PrintMain), null);
			// Ïå÷àòü âñå çàêàç íàðÿäîâ ïî ïîäðàçäåëåíèþ ñåðâèñ
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
