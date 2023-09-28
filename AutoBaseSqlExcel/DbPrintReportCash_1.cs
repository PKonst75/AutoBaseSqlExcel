using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbPrintReportCash_1.
	/// </summary>
	public class DbPrintReportCash_1:DbPrint
	{
		// Инструменты для печати
		SolidBrush	brush_standart;
		SolidBrush	brush_lightgray;
		Font		font_small_bold;
		Font		font_middle_bold;
		Font		font_middle;
		Font		font_small;
		Pen			pen_thin;

		#region Данные для печати
		protected class HeaderData
		{
			public struct CardInfo
			{
				public long warrant_number;
				public DateTime warrant_date;

				public float summ_work;
				public float summ_oil;
				public float summ_detail;
				public float summ_discount;
				public float summ_pay_work;
				public float summ_pay_detail;
				public float summ_pay;
			};
			public struct Info
			{
				public float summ_work;
				public float summ_oil;
				public float summ_detail;
				public float summ_discount;
				public float summ_pay_work;
				public float summ_pay_detail;
				public float summ_pay;
			};
			public class InfoWork
			{
				public long code;
				public string name;
				public int quontity;

				public InfoWork ()
				{
					code		= 0;
					name		= "";
					quontity	= 0;
				}

				public void Add()
				{
					quontity++;
				}
			}
			public string txt_date;
			public ArrayList cards_service_info = null;
			public Info service_info = new Info();
			public ArrayList cards_music_info = null;
			public Info music_info;
			public ArrayList cards_tuning_info = null;
			public Info tuning_info;
			public ArrayList cards_antikor_info = null;
			public Info antikor_info;
			public ArrayList antikor_info_works;
			public ArrayList cards_paint_info = null;
			public Info paint_info;
			public ArrayList cards_wash_info = null;
			public Info wash_info;
			public ArrayList cards_shop_info = null;
			public Info shop_info;

			public HeaderData(DateTime date)
			{
				ArrayList cards = null;

				ArrayList payments = null;
				ArrayList tmp = null;
				float tmp_sum	= 0.0F;
				float tmp_sum1	= 0.0F;
				// Данные отчета
				txt_date = date.ToShortDateString();

				#region Сервис
				// Отчет по подразделению: сервис
				payments = new ArrayList();
				cards_service_info = new ArrayList();
				DbSqlPayment.SelectInArrayWorkshop(payments, date, 1);
				foreach(object o in payments)
				{
					CS_Payment peyment = (CS_Payment)o;
					// Разбираем данные платежа
					DtCard card = (DtCard)o;
					card = DbSqlCard.Find((long)card.GetData("НОМЕР_КАРТОЧКА"), (int)card.GetData("ГОД_КАРТОЧКА"));
					CardInfo card_info = new CardInfo();
					card_info.warrant_number	= (long)card.GetData("НОМЕР_НАРЯД_КАРТОЧКА");
					card_info.warrant_date		= (DateTime)card.GetData("ДАТА_НАРЯД_ОТКРЫТ_КАРТОЧКА");
					// Получаем все работы карточки
					tmp = new ArrayList();
					DbSqlCardWork.SelectInArray(card, tmp);
					tmp_sum		= 0.0F;
					foreach(DtCardWork work in tmp)
					{
						tmp_sum += work.WorkSummCash;
					}
					card_info.summ_work	= tmp_sum;
					// Дисконт
					tmp_sum	= (float)card.GetData("СКИДКА_РАБОТА_КАРТОЧКА");
					if(tmp_sum != 0.0F)
					{
						card_info.summ_discount = (float)Math.Round((float)(card_info.summ_work / 100 * tmp_sum), 0);
					}
					else
					{
						card_info.summ_discount = 0.0F;
					}
					// Получаем все детали в карточке
					tmp = new ArrayList();
					DbSqlCardDetail.SelectInArray(card, tmp);
					tmp_sum	= 0.0F;
					tmp_sum1	= 0.0F;
					foreach(DtCardDetail detail in tmp)
					{
						
						tmp_sum += detail.DetailSummCash;
						tmp_sum1 += detail.DetailSummOilCash;
					}
					card_info.summ_detail	= tmp_sum;
					card_info.summ_oil		= tmp_sum1;
					// Оплаты
					card_info.summ_pay_work		= card_info.summ_work + card_info.summ_oil - card_info.summ_discount;
					card_info.summ_pay_detail	= card_info.summ_detail;
					card_info.summ_pay			= card_info.summ_pay_work + card_info.summ_pay_detail;
					// Добавляем информацию о карточке в список, если было что оплачивать
					//	if(card_info.summ_pay > 0.0F)
					//	{
					cards_service_info.Add(card_info);
					service_info.summ_detail	+= card_info.summ_detail;
					service_info.summ_work		+= card_info.summ_work;
					service_info.summ_oil		+= card_info.summ_oil;
					service_info.summ_discount	+= card_info.summ_discount;
					service_info.summ_pay_work	+= card_info.summ_pay_work;
					service_info.summ_pay_detail+= card_info.summ_pay_detail;
					service_info.summ_pay		+= card_info.summ_pay;
					//	}
				}
				#endregion	

				#region Музыка
				cards = new ArrayList();
				DbSqlCard.SelectCardClosedNumberWorkshopNal(cards, date, date, 7);
				cards_music_info = CollectData(cards);
				music_info			= AnalizeData(cards_music_info);
				#endregion

				#region Тюнинг
				cards = new ArrayList();
				DbSqlCard.SelectCardClosedNumberWorkshopNal(cards, date, date, 2);
				cards_tuning_info	= CollectData(cards);
				tuning_info			= AnalizeData(cards_tuning_info);
				#endregion

				#region Антикор
				cards = new ArrayList();
				DbSqlCard.SelectCardClosedNumberWorkshopNal(cards, date, date, 6);
				cards_antikor_info	= CollectData(cards);
				antikor_info		= AnalizeData(cards_antikor_info);
				antikor_info_works	= CollectDataWork(cards);
				#endregion

				#region Малярка
				cards = new ArrayList();
				DbSqlCard.SelectCardClosedNumberWorkshopNal(cards, date, date, 3);
				cards_paint_info	= CollectData(cards);
				paint_info		= AnalizeData(cards_paint_info);
				#endregion

				#region Мойка
				cards = new ArrayList();
				DbSqlCard.SelectCardClosedNumberWorkshopNal(cards, date, date, 5);
				cards_wash_info	= CollectData(cards);
				wash_info		= AnalizeData(cards_wash_info);
				#endregion

				#region Магазин
				cards = new ArrayList();
				DbSqlCard.SelectCardClosedNumberWorkshopNal(cards, date, date, 8);
				cards_shop_info	= CollectData(cards);
				shop_info		= AnalizeData(cards_wash_info);
				#endregion
			}

			#region Вспомогательные функции
			public ArrayList CollectDataWork(ArrayList cards)
			{
				ArrayList tmp	= null;
				ArrayList result = new ArrayList();
				foreach(object o in cards)
				{
					// Получаем все работы карточки
					tmp = new ArrayList();
					DtCard card = (DtCard)o;
					DbSqlCardWork.SelectInArray(card, tmp);
					foreach(DtCardWork work in tmp)
					{
						long code = (long)work.GetData("КОД_ТРУДОЕМКОСТЬ_КАРТОЧКА_РАБОТА");
						// Ищем в списке
						object mach = null;
						bool find		= false;
						foreach(object obj in result)
						{
							InfoWork iw = (InfoWork)obj;
							if(iw.code == code)
							{
								mach	= (object)iw;
								find	= true;
							}
						}
						if(find == false)
						{
							InfoWork iw = new InfoWork();
							iw.code		= code;
							iw.name		= (string)work.GetData("НАИМЕНОВАНИЕ_КАРТОЧКА_РАБОТА");
							iw.quontity	= 1;
							result.Add(iw);
						}
						else
						{
							((InfoWork)mach).Add();
						}
					}
				}
				return result;
			}
			public ArrayList CollectData(ArrayList cards)
			{
				float tmp_sum	= 0.0F;
				float tmp_sum1	= 0.0F;
				ArrayList tmp	= null;
				// Отчет по подразделению: сервис
				ArrayList result = new ArrayList();
				foreach(object o in cards)
				{
					DtCard card = (DtCard)o;
					card = DbSqlCard.Find((long)card.GetData("НОМЕР_КАРТОЧКА"), (int)card.GetData("ГОД_КАРТОЧКА"));
					CardInfo card_info = new CardInfo();
					card_info.warrant_number	= (long)card.GetData("НОМЕР_НАРЯД_КАРТОЧКА");
					card_info.warrant_date		= (DateTime)card.GetData("ДАТА_НАРЯД_ОТКРЫТ_КАРТОЧКА");
					// Получаем все работы карточки
					tmp = new ArrayList();
					DbSqlCardWork.SelectInArray(card, tmp);
					tmp_sum	= 0.0F;
					foreach(DtCardWork work in tmp)
					{
						tmp_sum += work.WorkSummCash;
					}
					card_info.summ_work	= tmp_sum;
					// Дисконт
					tmp_sum	= (float)card.GetData("СКИДКА_РАБОТА_КАРТОЧКА");
					if(tmp_sum != 0.0F)
					{
						card_info.summ_discount = (float)Math.Round((float)(card_info.summ_work / 100 * tmp_sum), 0);
					}
					else
					{
						card_info.summ_discount = 0.0F;
					}
					// Получаем все детали в карточке
					tmp = new ArrayList();
					DbSqlCardDetail.SelectInArray(card, tmp);
					tmp_sum	= 0.0F;
					tmp_sum1	= 0.0F;
					foreach(DtCardDetail detail in tmp)
					{
						
						tmp_sum += detail.DetailSummCash;
						tmp_sum1 += detail.DetailSummOilCash;
					}
					card_info.summ_detail	= tmp_sum;
					card_info.summ_oil		= tmp_sum1;
					// Оплаты
					card_info.summ_pay_work		= card_info.summ_work + card_info.summ_oil - card_info.summ_discount;
					card_info.summ_pay_detail	= card_info.summ_detail;
					card_info.summ_pay			= card_info.summ_pay_work + card_info.summ_pay_detail;
					// Добавляем информацию о карточке в список, если было что оплачивать
					//	if(card_info.summ_pay > 0.0F)
					//	{
					result.Add(card_info);
					//	}
				}
				return result;
			}
			public Info AnalizeData(ArrayList cards_info)
			{
				Info info = new Info();
				foreach(CardInfo card_info in cards_info)
				{
					info.summ_detail	+= card_info.summ_detail;
					info.summ_work		+= card_info.summ_work;
					info.summ_oil		+= card_info.summ_oil;
					info.summ_discount	+= card_info.summ_discount;
					info.summ_pay_work	+= card_info.summ_pay_work;
					info.summ_pay_detail+= card_info.summ_pay_detail;
					info.summ_pay		+= card_info.summ_pay;
				}
				return info;
			}
			#endregion
		}
		HeaderData	header_data = null;
		#endregion

		public DbPrintReportCash_1(DateTime date)
		{
			// Подготовка инструментов для печати
			brush_standart		= new SolidBrush(Color.Black);
			brush_lightgray		= new SolidBrush(Color.LightGray);
			font_small_bold		= new Font("Arial", 8, FontStyle.Bold);
			font_small			= new Font("Arial", 8);
			font_middle_bold	= new Font("Arial", 10, FontStyle.Bold);
			font_middle			= new Font("Arial", 10);
			pen_thin			= new Pen(brush_standart, 0.3F);

			// Получение данных кассового отчета на определенную дату
			header_data = new HeaderData(date);
		}

		#region Печать Основного блока
		private int PrintMain(Graphics graph, int offset,  bool test, bool print, object o)
		{
			// Настройка параметров печати заголовка
			SizeF size;
			int	height;
			int	y;
			string txt	= "";

			int offset_x		= 10;
			int page_width		= 190;
			int title_height	= 8;

			if(test == true || print == false)
			{
				// Просто рассчитываем высоту
				y = offset;
				y += title_height;
				return y ;
			}

			float summ	= header_data.service_info.summ_pay_work;
			summ		+= header_data.music_info.summ_pay_work;
			summ		+= header_data.tuning_info.summ_pay_work;
			summ		+= header_data.antikor_info.summ_pay_work;
			summ		+= header_data.paint_info.summ_pay_work;
			summ		+= header_data.wash_info.summ_pay_work;
			summ		+= header_data.shop_info.summ_pay_work;

			float summ2	= header_data.service_info.summ_pay_detail;
			summ2		+= header_data.music_info.summ_pay_detail;
			summ2		+= header_data.tuning_info.summ_pay_detail;
			summ2		+= header_data.antikor_info.summ_pay_detail;
			summ2		+= header_data.paint_info.summ_pay_detail;
			summ2		+= header_data.wash_info.summ_pay_detail;
			summ2		+= header_data.shop_info.summ_pay_detail;

			y	= offset;
			PrintText(graph, "КАССОВЫЙ ОТЧЕТ НА " + header_data.txt_date + "     КАССА(ТО, МАСЛА) " + summ.ToString() + "     КАССА(ЗАПЧАСТИ) " + summ2.ToString(), offset_x, y, page_width, title_height, StringAlignment.Near, StringAlignment.Center, font_middle_bold, brush_standart, false);
			y += title_height;
			return y;
		}
		#endregion

		#region Печать блока сервиса
		private int PrintServiceBlockHead(Graphics graph, int offset,  bool test, bool print, object o)
		{
			// Вспомогательные инструменты
			RectangleF rect;
			int y		= offset;
			int y1		= 0;
			string text	= "";

			// Настроечные параметры
			int offset_x_left		= 10;
			int offset_x_right		= 10;
			int page_width			= 210;
			int title_height		= 4;
			int	col1				= 190;
			int offset1				= 12;
			int offset2				= 80;
			int x1					= 30;
			int x2					= 20;

			
			if(test == true || print == false)
			{	
				y += title_height * 5;
				return y;
			}

			// Первая строчка
			PrintTextBox(graph, "Подразделение - сервис", offset_x_left, y, col1, title_height * 5, 1, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, pen_thin, false);
			y += title_height;
			y1 = y;
			PrintText(graph, "ТО, ремонт", offset1, y1, x1, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			PrintText(graph, header_data.service_info.summ_work.ToString(), offset1 + x1, y1, x2, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			y1 += title_height;
			PrintText(graph, "Масла", offset1, y1, x1, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			PrintText(graph, header_data.service_info.summ_oil.ToString(), offset1 + x1, y1, x2, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			y1 += title_height;
			PrintText(graph, "Запчасти", offset1, y1, x1, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			PrintText(graph, header_data.service_info.summ_detail.ToString(), offset1 + x1, y1, x2, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			y1 += title_height;
			PrintText(graph, "Скидки", offset1, y1, x1, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			PrintText(graph, header_data.service_info.summ_discount.ToString(), offset1 + x1, y1, x2, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			y1 = offset;
			PrintText(graph, "ОПЛАТА", offset2, y1, x1, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			y1 += title_height;
			PrintText(graph, "ТО, ремонт, масла", offset2, y1, x1, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			PrintText(graph, header_data.service_info.summ_pay_work.ToString(), offset2 + x1, y1, x2, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			y1 += title_height;
			PrintText(graph, "Запчасти", offset2, y1, x1, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			PrintText(graph, header_data.service_info.summ_pay_detail.ToString(), offset2 + x1, y1, x2, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			y1 += title_height;
			PrintText(graph, "ВСЕГО", offset2, y1, x1, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			PrintText(graph, header_data.service_info.summ_pay.ToString(), offset2 + x1, y1, x2, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);

			y += title_height * 4;
			return y;
		}

		private int PrintServiceBlockHeadData(Graphics graph, int offset,  bool test, bool print, object o)
		{
			// Вспомогательные инструменты
			RectangleF rect;
			int y		= offset;
			int y1		= 0;
			string text	= "";

			// Настроечные параметры
			int offset_x_left		= 10;
			int offset_x_right		= 10;
			int page_width			= 210;
			int title_height		= 4;
			int	col1				= 190;
			int offset1				= 150;
			int x1					= 30;
			int x2					= 20;

			
			if(test == true || print == false)
			{	
				y += title_height;
				y += title_height;
				y += title_height;
				y += title_height;
				y += title_height;
				y += title_height;
				y += title_height;
				y += title_height;
				y += title_height;
				y += title_height;
				return y;
			}

			y += title_height;

			// Агоригированные данные
			PrintText(graph, "ТО, ремонт", offset1, y1, x1, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			PrintText(graph, header_data.service_info.summ_work.ToString(), offset1 + x1, y1, x2, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			y += title_height;
			PrintText(graph, "Масла", offset1, y1, x1, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			PrintText(graph, header_data.service_info.summ_oil.ToString(), offset1 + x1, y1, x2, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			y += title_height;
			PrintText(graph, "Запчасти", offset1, y1, x1, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			PrintText(graph, header_data.service_info.summ_detail.ToString(), offset1 + x1, y1, x2, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			y += title_height;
			PrintText(graph, "Скидки", offset1, y1, x1, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			PrintText(graph, header_data.service_info.summ_discount.ToString(), offset1 + x1, y1, x2, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			y += title_height;
			y += title_height;
			y += title_height;
			PrintText(graph, "ОПЛАТА", offset1, y1, x1, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			y += title_height;
			PrintText(graph, "ТО, ремонт, масла", offset1, y1, x1, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			PrintText(graph, header_data.service_info.summ_pay_work.ToString(), offset1 + x1, y1, x2, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			y += title_height;
			PrintText(graph, "Запчасти", offset1, y1, x1, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			PrintText(graph, header_data.service_info.summ_pay_detail.ToString(), offset1 + x1, y1, x2, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			y += title_height;
			PrintText(graph, "ВСЕГО", offset1, y1, x1, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			PrintText(graph, header_data.service_info.summ_pay.ToString(), offset1 + x1, y1, x2, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);

			return y;
		}
		private int PrintServiceBlock(Graphics graph, int offset,  bool test, bool print, object o)
		{
			// Вспомогательные инструменты
			RectangleF rect;
			int y		= offset;
			string text	= "";

			// Проверка логичности данных
			if(o == null) return y;

			// Настроечные параметры
			int offset_x_left		= 10;
			int offset_x_right		= 10;
			int page_width			= 210;
			int rowheight			= 4;
			int	col1				= 30;
			int	col2				= 15;
			int	col3				= 15;
			int	col4				= 15;
			int	col5				= 15;
			int	col6				= 15;
			int	col7				= 15;
			int	col8				= 15;
			int	col9				= 190 - col1 - col2 - col3 - col4 - col5 - col6 - col7 - col8;
			
			// Получение данных для печати
			HeaderData.CardInfo info = (HeaderData.CardInfo)o;

			string txt_warrant			= (string)info.warrant_number.ToString() + " / " + (string)info.warrant_date.ToShortDateString();
			string txt_summ_work		= (string)info.summ_work.ToString();
			string txt_summ_detail		= (string)info.summ_detail.ToString();
			string txt_summ_oil			= (string)info.summ_oil.ToString();
			string txt_summ_discount	= (string)info.summ_discount.ToString();
			string txt_summ_pay_work	= (string)info.summ_pay_work.ToString();
			string txt_summ_pay_detail	= (string)info.summ_pay_detail.ToString();
			string txt_summ_pay			= (string)info.summ_pay.ToString();

			if(test == true || print == false)
			{	
				return y + rowheight;
			}
			if(info.summ_pay > 0)
			{
				PrintTextBox(graph, txt_warrant, offset_x_left, y, col1, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
				PrintTextBox(graph, txt_summ_work, offset_x_left + col1, y, col2, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
				PrintTextBox(graph, txt_summ_oil, offset_x_left + col1 + col2, y, col3, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
				PrintTextBox(graph, txt_summ_detail, offset_x_left + col1 + col2 + col3, y, col4, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
				PrintTextBox(graph, txt_summ_discount, offset_x_left + col1 + col2 + col3 + col4, y, col5, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
				PrintTextBox(graph, txt_summ_pay_work, offset_x_left + col1 + col2 + col3 + col4 + col5, y, col6, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
				PrintTextBox(graph, txt_summ_pay_detail, offset_x_left + col1 + col2 + col3 + col4 + col5 + col6, y, col7, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
				PrintTextBox(graph, txt_summ_pay, offset_x_left + col1 + col2 + col3 + col4 + col5 + col6 + col7, y, col8, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			}
			else
			{
				PrintTextBoxColor(graph, txt_warrant, offset_x_left, y, col1, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, brush_lightgray, false);
				PrintTextBoxColor(graph, txt_summ_work, offset_x_left + col1, y, col2, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, brush_lightgray, false);
				PrintTextBoxColor(graph, txt_summ_oil, offset_x_left + col1 + col2, y, col3, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, brush_lightgray, false);
				PrintTextBoxColor(graph, txt_summ_detail, offset_x_left + col1 + col2 + col3, y, col4, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, brush_lightgray, false);
				PrintTextBoxColor(graph, txt_summ_discount, offset_x_left + col1 + col2 + col3 + col4, y, col5, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, brush_lightgray, false);
				PrintTextBoxColor(graph, txt_summ_pay_work, offset_x_left + col1 + col2 + col3 + col4 + col5, y, col6, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, brush_lightgray, false);
				PrintTextBoxColor(graph, txt_summ_pay_detail, offset_x_left + col1 + col2 + col3 + col4 + col5 + col6, y, col7, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, brush_lightgray, false);
				PrintTextBoxColor(graph, txt_summ_pay, offset_x_left + col1 + col2 + col3 + col4 + col5 + col6 + col7, y, col8, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, brush_lightgray, false);
			}

			y += rowheight;
			return y;
		}
		#endregion

		#region Печать блока музыки
		private int PrintMusicBlockHead(Graphics graph, int offset,  bool test, bool print, object o)
		{
			// Вспомогательные инструменты
			RectangleF rect;
			int y		= offset + 3;
			int y1		= 0;
			string text	= "";

			// Настроечные параметры
			int offset_x_left		= 10;
			int offset_x_right		= 10;
			int page_width			= 210;
			int title_height		= 4;
			int	col1				= 190;
			int offset1				= 12;
			int offset2				= 80;
			int x1					= 30;
			int x2					= 20;

			
			// Сбор данных
			string txt_summ_work			= header_data.music_info.summ_work.ToString();
			string txt_summ_oil				= header_data.music_info.summ_oil.ToString();
			string txt_summ_detail			= header_data.music_info.summ_detail.ToString();
			string txt_summ_discount		= header_data.music_info.summ_discount.ToString();
			string txt_summ_pay_work		= header_data.music_info.summ_pay_work.ToString();
			string txt_summ_pay_detail		= header_data.music_info.summ_pay_detail.ToString();
			string txt_summ_pay				= header_data.music_info.summ_pay.ToString();

			if(test == true || print == false)
			{	
				y += 3 + title_height * 2;
				return y;
			}

			// Первая строчка
			PrintTextBox(graph, "Подразделение - Музыка", offset_x_left, y, col1, title_height * 2, 1, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, pen_thin, false);
			y += title_height;
			y1 = y;
			PrintText(graph, "Работы", offset1, y1, x1, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			PrintText(graph, txt_summ_pay_work, offset1 + x1, y1, x2, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			y1 = y;
			PrintText(graph, "Запчасти", offset2, y1, x1, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			PrintText(graph, txt_summ_pay_detail, offset2 + x1, y1, x2, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			y += title_height;

			//	y1 = y;
			//	PrintText(graph, "Работы", offset1, y1, x1, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			//	PrintText(graph, txt_summ_work, offset1 + x1, y1, x2, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			//	y1 += title_height;
			//	PrintText(graph, "Масла", offset1, y1, x1, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			//	PrintText(graph, txt_summ_oil, offset1 + x1, y1, x2, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			//	y1 += title_height;
			//	PrintText(graph, "Запчасти", offset1, y1, x1, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			//	PrintText(graph, txt_summ_detail, offset1 + x1, y1, x2, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			//	y1 += title_height;
			//	PrintText(graph, "Скидки", offset1, y1, x1, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			//	PrintText(graph, txt_summ_discount, offset1 + x1, y1, x2, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			//	y1 = offset + 3;
			//	PrintText(graph, "ОПЛАТА", offset2, y1, x1, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			//	y1 += title_height;
			//	PrintText(graph, "ТО, ремонт, масла", offset2, y1, x1, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			//	PrintText(graph, txt_summ_pay_work, offset2 + x1, y1, x2, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			//	y1 += title_height;
			//	PrintText(graph, "Запчасти", offset2, y1, x1, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			//	PrintText(graph, txt_summ_pay_detail, offset2 + x1, y1, x2, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			//	y1 += title_height;
			//	PrintText(graph, "ВСЕГО", offset2, y1, x1, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			//	PrintText(graph, txt_summ_pay, offset2 + x1, y1, x2, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			//	y += title_height * 4;


			return y;
		}

		private int PrintMusicBlock(Graphics graph, int offset,  bool test, bool print, object o)
		{
			// Вспомогательные инструменты
			RectangleF rect;
			int y		= offset;
			string text	= "";

			// Проверка логичности данных
			if(o == null) return y;

			// Настроечные параметры
			int offset_x_left		= 10;
			int offset_x_right		= 10;
			int page_width			= 210;
			int rowheight			= 4;
			int	col1				= 30;
			int	col2				= 15;
			int	col3				= 15;
			int	col4				= 15;
			int	col5				= 15;
			int	col6				= 15;
			int	col7				= 15;
			int	col8				= 15;
			int	col9				= 190 - col1 - col2 - col3 - col4 - col5 - col6 - col7 - col8;
			
			// Получение данных для печати
			HeaderData.CardInfo info = (HeaderData.CardInfo)o;

			string txt_warrant			= (string)info.warrant_number.ToString() + " / " + (string)info.warrant_date.ToShortDateString();
			string txt_summ_work		= (string)info.summ_work.ToString();
			string txt_summ_detail		= (string)info.summ_detail.ToString();
			string txt_summ_oil			= (string)info.summ_oil.ToString();
			string txt_summ_discount	= (string)info.summ_discount.ToString();
			string txt_summ_pay_work	= (string)info.summ_pay_work.ToString();
			string txt_summ_pay_detail	= (string)info.summ_pay_detail.ToString();
			string txt_summ_pay			= (string)info.summ_pay.ToString();

			if(test == true || print == false)
			{	
				return y + rowheight;
			}
			if(info.summ_pay > 0)
			{
				PrintTextBox(graph, txt_warrant, offset_x_left, y, col1, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
				PrintTextBox(graph, txt_summ_work, offset_x_left + col1, y, col2, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
				PrintTextBox(graph, txt_summ_oil, offset_x_left + col1 + col2, y, col3, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
				PrintTextBox(graph, txt_summ_detail, offset_x_left + col1 + col2 + col3, y, col4, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
				PrintTextBox(graph, txt_summ_discount, offset_x_left + col1 + col2 + col3 + col4, y, col5, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
				PrintTextBox(graph, txt_summ_pay_work, offset_x_left + col1 + col2 + col3 + col4 + col5, y, col6, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
				PrintTextBox(graph, txt_summ_pay_detail, offset_x_left + col1 + col2 + col3 + col4 + col5 + col6, y, col7, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
				PrintTextBox(graph, txt_summ_pay, offset_x_left + col1 + col2 + col3 + col4 + col5 + col6 + col7, y, col8, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			}
			else
			{
				PrintTextBoxColor(graph, txt_warrant, offset_x_left, y, col1, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, brush_lightgray, false);
				PrintTextBoxColor(graph, txt_summ_work, offset_x_left + col1, y, col2, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, brush_lightgray, false);
				PrintTextBoxColor(graph, txt_summ_oil, offset_x_left + col1 + col2, y, col3, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, brush_lightgray, false);
				PrintTextBoxColor(graph, txt_summ_detail, offset_x_left + col1 + col2 + col3, y, col4, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, brush_lightgray, false);
				PrintTextBoxColor(graph, txt_summ_discount, offset_x_left + col1 + col2 + col3 + col4, y, col5, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, brush_lightgray, false);
				PrintTextBoxColor(graph, txt_summ_pay_work, offset_x_left + col1 + col2 + col3 + col4 + col5, y, col6, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, brush_lightgray, false);
				PrintTextBoxColor(graph, txt_summ_pay_detail, offset_x_left + col1 + col2 + col3 + col4 + col5 + col6, y, col7, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, brush_lightgray, false);
				PrintTextBoxColor(graph, txt_summ_pay, offset_x_left + col1 + col2 + col3 + col4 + col5 + col6 + col7, y, col8, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, brush_lightgray, false);
			}

			y += rowheight;
			return y;
		}
		#endregion

		#region Печать блока тюнинга
		private int PrintTuningBlockHead(Graphics graph, int offset,  bool test, bool print, object o)
		{
			// Вспомогательные инструменты
			RectangleF rect;
			int y		= offset + 3;
			int y1		= 0;
			string text	= "";

			// Настроечные параметры
			int offset_x_left		= 10;
			int offset_x_right		= 10;
			int page_width			= 210;
			int title_height		= 4;
			int	col1				= 190;
			int offset1				= 12;
			int offset2				= 80;
			int x1					= 30;
			int x2					= 20;

			
			// Сбор данных
			string txt_summ_work			= header_data.tuning_info.summ_work.ToString();
			string txt_summ_oil				= header_data.tuning_info.summ_oil.ToString();
			string txt_summ_detail			= header_data.tuning_info.summ_detail.ToString();
			string txt_summ_discount		= header_data.tuning_info.summ_discount.ToString();
			string txt_summ_pay_work		= header_data.tuning_info.summ_pay_work.ToString();
			string txt_summ_pay_detail		= header_data.tuning_info.summ_pay_detail.ToString();
			string txt_summ_pay				= header_data.tuning_info.summ_pay.ToString();

			if(test == true || print == false)
			{	
				y += 3 + title_height * 5;
				return y;
			}

			// Первая строчка
			PrintTextBox(graph, "Подразделение - Тюнинг", offset_x_left, y, col1, title_height * 2, 1, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, pen_thin, false);
			y += title_height;

			y1 = y;
			PrintText(graph, "Работы", offset1, y1, x1, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			PrintText(graph, txt_summ_pay_work, offset1 + x1, y1, x2, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			y1 = y;
			PrintText(graph, "Запчасти", offset2, y1, x1, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			PrintText(graph, txt_summ_pay_detail, offset2 + x1, y1, x2, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			y += title_height;

			//	y1 = y;
			//	PrintText(graph, "ТО, ремонт", offset1, y1, x1, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			//	PrintText(graph, txt_summ_work, offset1 + x1, y1, x2, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			//	y1 += title_height;
			//	PrintText(graph, "Масла", offset1, y1, x1, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			//	PrintText(graph, txt_summ_oil, offset1 + x1, y1, x2, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			//	y1 += title_height;
			//	PrintText(graph, "Запчасти", offset1, y1, x1, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			//	PrintText(graph, txt_summ_detail, offset1 + x1, y1, x2, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			//	y1 += title_height;
			//	PrintText(graph, "Скидки", offset1, y1, x1, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			//	PrintText(graph, txt_summ_discount, offset1 + x1, y1, x2, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			//	y1 = offset + 3;
			//	PrintText(graph, "ОПЛАТА", offset2, y1, x1, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			//	y1 += title_height;
			//	PrintText(graph, "ТО, ремонт, масла", offset2, y1, x1, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			//	PrintText(graph, txt_summ_pay_work, offset2 + x1, y1, x2, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			//	y1 += title_height;
			//	PrintText(graph, "Запчасти", offset2, y1, x1, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			//	PrintText(graph, txt_summ_pay_detail, offset2 + x1, y1, x2, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			//	y1 += title_height;
			//	PrintText(graph, "ВСЕГО", offset2, y1, x1, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			//	PrintText(graph, txt_summ_pay, offset2 + x1, y1, x2, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			//	y += title_height * 4;

			return y;
		}

		private int PrintTuningBlock(Graphics graph, int offset,  bool test, bool print, object o)
		{
			// Вспомогательные инструменты
			RectangleF rect;
			int y		= offset;
			string text	= "";

			// Проверка логичности данных
			if(o == null) return y;

			// Настроечные параметры
			int offset_x_left		= 10;
			int offset_x_right		= 10;
			int page_width			= 210;
			int rowheight			= 4;
			int	col1				= 30;
			int	col2				= 15;
			int	col3				= 15;
			int	col4				= 15;
			int	col5				= 15;
			int	col6				= 15;
			int	col7				= 15;
			int	col8				= 15;
			int	col9				= 190 - col1 - col2 - col3 - col4 - col5 - col6 - col7 - col8;
			
			// Получение данных для печати
			HeaderData.CardInfo info = (HeaderData.CardInfo)o;

			string txt_warrant			= (string)info.warrant_number.ToString() + " / " + (string)info.warrant_date.ToShortDateString();
			string txt_summ_work		= (string)info.summ_work.ToString();
			string txt_summ_detail		= (string)info.summ_detail.ToString();
			string txt_summ_oil			= (string)info.summ_oil.ToString();
			string txt_summ_discount	= (string)info.summ_discount.ToString();
			string txt_summ_pay_work	= (string)info.summ_pay_work.ToString();
			string txt_summ_pay_detail	= (string)info.summ_pay_detail.ToString();
			string txt_summ_pay			= (string)info.summ_pay.ToString();

			if(test == true || print == false)
			{	
				return y + rowheight;
			}
			if(info.summ_pay > 0)
			{
				PrintTextBox(graph, txt_warrant, offset_x_left, y, col1, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
				PrintTextBox(graph, txt_summ_work, offset_x_left + col1, y, col2, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
				PrintTextBox(graph, txt_summ_oil, offset_x_left + col1 + col2, y, col3, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
				PrintTextBox(graph, txt_summ_detail, offset_x_left + col1 + col2 + col3, y, col4, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
				PrintTextBox(graph, txt_summ_discount, offset_x_left + col1 + col2 + col3 + col4, y, col5, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
				PrintTextBox(graph, txt_summ_pay_work, offset_x_left + col1 + col2 + col3 + col4 + col5, y, col6, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
				PrintTextBox(graph, txt_summ_pay_detail, offset_x_left + col1 + col2 + col3 + col4 + col5 + col6, y, col7, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
				PrintTextBox(graph, txt_summ_pay, offset_x_left + col1 + col2 + col3 + col4 + col5 + col6 + col7, y, col8, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			}
			else
			{
				PrintTextBoxColor(graph, txt_warrant, offset_x_left, y, col1, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, brush_lightgray, false);
				PrintTextBoxColor(graph, txt_summ_work, offset_x_left + col1, y, col2, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, brush_lightgray, false);
				PrintTextBoxColor(graph, txt_summ_oil, offset_x_left + col1 + col2, y, col3, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, brush_lightgray, false);
				PrintTextBoxColor(graph, txt_summ_detail, offset_x_left + col1 + col2 + col3, y, col4, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, brush_lightgray, false);
				PrintTextBoxColor(graph, txt_summ_discount, offset_x_left + col1 + col2 + col3 + col4, y, col5, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, brush_lightgray, false);
				PrintTextBoxColor(graph, txt_summ_pay_work, offset_x_left + col1 + col2 + col3 + col4 + col5, y, col6, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, brush_lightgray, false);
				PrintTextBoxColor(graph, txt_summ_pay_detail, offset_x_left + col1 + col2 + col3 + col4 + col5 + col6, y, col7, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, brush_lightgray, false);
				PrintTextBoxColor(graph, txt_summ_pay, offset_x_left + col1 + col2 + col3 + col4 + col5 + col6 + col7, y, col8, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, brush_lightgray, false);
			}
			y += rowheight;
			return y;
		}
		#endregion

		#region Печать блока антикора
		private int PrintAntikorBlockHead(Graphics graph, int offset,  bool test, bool print, object o)
		{
			// Вспомогательные инструменты
			RectangleF rect;
			int y		= offset + 3;
			int y1		= 0;
			string text	= "";

			// Настроечные параметры
			int offset_x_left		= 10;
			int offset_x_right		= 10;
			int page_width			= 210;
			int title_height		= 4;
			int	col1				= 190;
			int offset1				= 12;
			int offset2				= 60;
			int x1					= 30;
			int x2					= 20;
			int x3					= 100;

			
			// Сбор данных
			string txt_summ_work			= header_data.antikor_info.summ_work.ToString();
			string txt_summ_oil				= header_data.antikor_info.summ_oil.ToString();
			string txt_summ_detail			= header_data.antikor_info.summ_detail.ToString();
			string txt_summ_discount		= header_data.antikor_info.summ_discount.ToString();
			string txt_summ_pay_work		= header_data.antikor_info.summ_pay_work.ToString();
			string txt_summ_pay_detail		= header_data.antikor_info.summ_pay_detail.ToString();
			string txt_summ_pay				= header_data.antikor_info.summ_pay.ToString();

			// Подсчитываем количество работ
			int count	= header_data.antikor_info_works.Count;
			if(count < 5) count	= 5;

			if(test == true || print == false)
			{	
				y += title_height * count;
				return y;
			}

			// Первая строчка
			PrintTextBox(graph, "Подразделение - Антикор", offset_x_left, y, col1, title_height * count, 1, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, pen_thin, false);
			y1 = y + title_height;
			PrintText(graph, "Работы", offset1, y1, x1, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			PrintText(graph, txt_summ_pay_work, offset1 + x1, y1, x2, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			y1 += title_height;
			PrintText(graph, "Детали", offset1, y1, x1, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			PrintText(graph, txt_summ_pay_detail, offset1 + x1, y1, x2, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			y1 += title_height;
			PrintText(graph, "ВСЕГО", offset1, y1, x1, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			PrintText(graph, txt_summ_pay, offset1 + x1, y1, x2, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);

			// Расписываем работы
			y1 = y;
			foreach(HeaderData.InfoWork iw in header_data.antikor_info_works)
			{
				PrintText(graph, iw.name, offset2, y1, x3, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
				PrintText(graph, iw.quontity.ToString(), offset2 + x3, y1, x2, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
				y1 += title_height;
			}
			
			y += title_height * count;
			return y;
		}

		private int PrintAntikorBlock(Graphics graph, int offset,  bool test, bool print, object o)
		{
			// Вспомогательные инструменты
			RectangleF rect;
			int y		= offset;
			string text	= "";

			// Проверка логичности данных
			if(o == null) return y;

			// Настроечные параметры
			int offset_x_left		= 10;
			int offset_x_right		= 10;
			int page_width			= 210;
			int rowheight			= 4;
			int	col1				= 30;
			int	col2				= 15;
			int	col3				= 15;
			int	col4				= 15;
			int	col5				= 15;
			int	col6				= 15;
			int	col7				= 15;
			int	col8				= 15;
			int	col9				= 190 - col1 - col2 - col3 - col4 - col5 - col6 - col7 - col8;
			
			// Получение данных для печати
			HeaderData.CardInfo info = (HeaderData.CardInfo)o;

			string txt_warrant			= (string)info.warrant_number.ToString() + " / " + (string)info.warrant_date.ToShortDateString();
			string txt_summ_work		= (string)info.summ_work.ToString();
			string txt_summ_detail		= (string)info.summ_detail.ToString();
			string txt_summ_oil			= (string)info.summ_oil.ToString();
			string txt_summ_discount	= (string)info.summ_discount.ToString();
			string txt_summ_pay_work	= (string)info.summ_pay_work.ToString();
			string txt_summ_pay_detail	= (string)info.summ_pay_detail.ToString();
			string txt_summ_pay			= (string)info.summ_pay.ToString();

			if(test == true || print == false)
			{	
				return y + rowheight;
			}
			if(info.summ_pay > 0)
			{
				PrintTextBox(graph, txt_warrant, offset_x_left, y, col1, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
				PrintTextBox(graph, txt_summ_work, offset_x_left + col1, y, col2, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
				PrintTextBox(graph, txt_summ_oil, offset_x_left + col1 + col2, y, col3, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
				PrintTextBox(graph, txt_summ_detail, offset_x_left + col1 + col2 + col3, y, col4, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
				PrintTextBox(graph, txt_summ_discount, offset_x_left + col1 + col2 + col3 + col4, y, col5, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
				PrintTextBox(graph, txt_summ_pay_work, offset_x_left + col1 + col2 + col3 + col4 + col5, y, col6, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
				PrintTextBox(graph, txt_summ_pay_detail, offset_x_left + col1 + col2 + col3 + col4 + col5 + col6, y, col7, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
				PrintTextBox(graph, txt_summ_pay, offset_x_left + col1 + col2 + col3 + col4 + col5 + col6 + col7, y, col8, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			}
			else
			{
				PrintTextBoxColor(graph, txt_warrant, offset_x_left, y, col1, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, brush_lightgray, false);
				PrintTextBoxColor(graph, txt_summ_work, offset_x_left + col1, y, col2, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, brush_lightgray, false);
				PrintTextBoxColor(graph, txt_summ_oil, offset_x_left + col1 + col2, y, col3, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, brush_lightgray, false);
				PrintTextBoxColor(graph, txt_summ_detail, offset_x_left + col1 + col2 + col3, y, col4, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, brush_lightgray, false);
				PrintTextBoxColor(graph, txt_summ_discount, offset_x_left + col1 + col2 + col3 + col4, y, col5, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, brush_lightgray, false);
				PrintTextBoxColor(graph, txt_summ_pay_work, offset_x_left + col1 + col2 + col3 + col4 + col5, y, col6, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, brush_lightgray, false);
				PrintTextBoxColor(graph, txt_summ_pay_detail, offset_x_left + col1 + col2 + col3 + col4 + col5 + col6, y, col7, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, brush_lightgray, false);
				PrintTextBoxColor(graph, txt_summ_pay, offset_x_left + col1 + col2 + col3 + col4 + col5 + col6 + col7, y, col8, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, brush_lightgray, false);
			}

			y += rowheight;
			return y;
		}
		#endregion

		#region Печать блока Малярки
		private int PrintPaintBlockHead(Graphics graph, int offset,  bool test, bool print, object o)
		{
			// Вспомогательные инструменты
			RectangleF rect;
			int y		= offset + 3;
			int y1		= 0;
			string text	= "";

			// Настроечные параметры
			int offset_x_left		= 10;
			int offset_x_right		= 10;
			int page_width			= 210;
			int title_height		= 4;
			int	col1				= 190;
			int offset1				= 12;
			int offset2				= 80;
			int x1					= 30;
			int x2					= 20;

			
			// Сбор данных
			string txt_summ_work			= header_data.paint_info.summ_work.ToString();
			string txt_summ_oil				= header_data.paint_info.summ_oil.ToString();
			string txt_summ_detail			= header_data.paint_info.summ_detail.ToString();
			string txt_summ_discount		= header_data.paint_info.summ_discount.ToString();
			string txt_summ_pay_work		= header_data.paint_info.summ_pay_work.ToString();
			string txt_summ_pay_detail		= header_data.paint_info.summ_pay_detail.ToString();
			string txt_summ_pay				= header_data.paint_info.summ_pay.ToString();

			if(test == true || print == false)
			{	
				y += 3 + title_height * 5;
				return y;
			}

			// Первая строчка
			PrintTextBox(graph, "Подразделение - Малярка", offset_x_left, y, col1, title_height * 2, 1, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, pen_thin, false);
			y += title_height;

			y1 = y;
			PrintText(graph, "Работы", offset1, y1, x1, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			PrintText(graph, txt_summ_pay_work, offset1 + x1, y1, x2, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			y1 = y;
			PrintText(graph, "Запчасти", offset2, y1, x1, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			PrintText(graph, txt_summ_pay_detail, offset2 + x1, y1, x2, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			y += title_height;

			//	y1 = y;
			//	PrintText(graph, "ТО, ремонт", offset1, y1, x1, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			//	PrintText(graph, txt_summ_work, offset1 + x1, y1, x2, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			//	y1 += title_height;
			//	PrintText(graph, "Масла", offset1, y1, x1, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			//	PrintText(graph, txt_summ_oil, offset1 + x1, y1, x2, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			//	y1 += title_height;
			//	PrintText(graph, "Запчасти", offset1, y1, x1, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			//	PrintText(graph, txt_summ_detail, offset1 + x1, y1, x2, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			//	y1 += title_height;
			//	PrintText(graph, "Скидки", offset1, y1, x1, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			//	PrintText(graph, txt_summ_discount, offset1 + x1, y1, x2, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			//	y1 = offset + 3;
			//	PrintText(graph, "ОПЛАТА", offset2, y1, x1, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			//	y1 += title_height;
			//	PrintText(graph, "ТО, ремонт, масла", offset2, y1, x1, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			//	PrintText(graph, txt_summ_pay_work, offset2 + x1, y1, x2, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			//	y1 += title_height;
			//	PrintText(graph, "Запчасти", offset2, y1, x1, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			//	PrintText(graph, txt_summ_pay_detail, offset2 + x1, y1, x2, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			//	y1 += title_height;
			//	PrintText(graph, "ВСЕГО", offset2, y1, x1, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			//	PrintText(graph, txt_summ_pay, offset2 + x1, y1, x2, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			//	y += title_height * 4;

			return y;
		}

		private int PrintPaintBlock(Graphics graph, int offset,  bool test, bool print, object o)
		{
			// Вспомогательные инструменты
			RectangleF rect;
			int y		= offset;
			string text	= "";

			// Проверка логичности данных
			if(o == null) return y;

			// Настроечные параметры
			int offset_x_left		= 10;
			int offset_x_right		= 10;
			int page_width			= 210;
			int rowheight			= 4;
			int	col1				= 30;
			int	col2				= 15;
			int	col3				= 15;
			int	col4				= 15;
			int	col5				= 15;
			int	col6				= 15;
			int	col7				= 15;
			int	col8				= 15;
			int	col9				= 190 - col1 - col2 - col3 - col4 - col5 - col6 - col7 - col8;
			
			// Получение данных для печати
			HeaderData.CardInfo info = (HeaderData.CardInfo)o;

			string txt_warrant			= (string)info.warrant_number.ToString() + " / " + (string)info.warrant_date.ToShortDateString();
			string txt_summ_work		= (string)info.summ_work.ToString();
			string txt_summ_detail		= (string)info.summ_detail.ToString();
			string txt_summ_oil			= (string)info.summ_oil.ToString();
			string txt_summ_discount	= (string)info.summ_discount.ToString();
			string txt_summ_pay_work	= (string)info.summ_pay_work.ToString();
			string txt_summ_pay_detail	= (string)info.summ_pay_detail.ToString();
			string txt_summ_pay			= (string)info.summ_pay.ToString();

			if(test == true || print == false)
			{	
				return y + rowheight;
			}
			if(info.summ_pay > 0)
			{
				PrintTextBox(graph, txt_warrant, offset_x_left, y, col1, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
				PrintTextBox(graph, txt_summ_work, offset_x_left + col1, y, col2, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
				PrintTextBox(graph, txt_summ_oil, offset_x_left + col1 + col2, y, col3, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
				PrintTextBox(graph, txt_summ_detail, offset_x_left + col1 + col2 + col3, y, col4, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
				PrintTextBox(graph, txt_summ_discount, offset_x_left + col1 + col2 + col3 + col4, y, col5, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
				PrintTextBox(graph, txt_summ_pay_work, offset_x_left + col1 + col2 + col3 + col4 + col5, y, col6, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
				PrintTextBox(graph, txt_summ_pay_detail, offset_x_left + col1 + col2 + col3 + col4 + col5 + col6, y, col7, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
				PrintTextBox(graph, txt_summ_pay, offset_x_left + col1 + col2 + col3 + col4 + col5 + col6 + col7, y, col8, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			}
			else
			{
				PrintTextBoxColor(graph, txt_warrant, offset_x_left, y, col1, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, brush_lightgray, false);
				PrintTextBoxColor(graph, txt_summ_work, offset_x_left + col1, y, col2, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, brush_lightgray, false);
				PrintTextBoxColor(graph, txt_summ_oil, offset_x_left + col1 + col2, y, col3, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, brush_lightgray, false);
				PrintTextBoxColor(graph, txt_summ_detail, offset_x_left + col1 + col2 + col3, y, col4, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, brush_lightgray, false);
				PrintTextBoxColor(graph, txt_summ_discount, offset_x_left + col1 + col2 + col3 + col4, y, col5, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, brush_lightgray, false);
				PrintTextBoxColor(graph, txt_summ_pay_work, offset_x_left + col1 + col2 + col3 + col4 + col5, y, col6, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, brush_lightgray, false);
				PrintTextBoxColor(graph, txt_summ_pay_detail, offset_x_left + col1 + col2 + col3 + col4 + col5 + col6, y, col7, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, brush_lightgray, false);
				PrintTextBoxColor(graph, txt_summ_pay, offset_x_left + col1 + col2 + col3 + col4 + col5 + col6 + col7, y, col8, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, brush_lightgray, false);
			}

			y += rowheight;
			return y;
		}
		#endregion

		#region Печать блока Мойки
		private int PrintWashBlockHead(Graphics graph, int offset,  bool test, bool print, object o)
		{
			// Вспомогательные инструменты
			RectangleF rect;
			int y		= offset + 3;
			int y1		= 0;
			string text	= "";

			// Настроечные параметры
			int offset_x_left		= 10;
			int offset_x_right		= 10;
			int page_width			= 210;
			int title_height		= 4;
			int	col1				= 190;
			int offset1				= 12;
			int offset2				= 80;
			int x1					= 30;
			int x2					= 20;

			
			// Сбор данных
			string txt_summ_work			= header_data.wash_info.summ_work.ToString();
			string txt_summ_oil				= header_data.wash_info.summ_oil.ToString();
			string txt_summ_detail			= header_data.wash_info.summ_detail.ToString();
			string txt_summ_discount		= header_data.wash_info.summ_discount.ToString();
			string txt_summ_pay_work		= header_data.wash_info.summ_pay_work.ToString();
			string txt_summ_pay_detail		= header_data.wash_info.summ_pay_detail.ToString();
			string txt_summ_pay				= header_data.wash_info.summ_pay.ToString();

			if(test == true || print == false)
			{	
				y += 3 + title_height * 5;
				return y;
			}

			// Первая строчка
			PrintTextBox(graph, "Подразделение - Мойка", offset_x_left, y, col1, title_height * 2, 1, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, pen_thin, false);
			y += title_height;

			y1 = y;
			PrintText(graph, "Работы", offset1, y1, x1, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			PrintText(graph, txt_summ_pay_work, offset1 + x1, y1, x2, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			y1 = y;
			PrintText(graph, "Запчасти", offset2, y1, x1, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			PrintText(graph, txt_summ_pay_detail, offset2 + x1, y1, x2, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			y += title_height;

			//	y1 = y;
			//	PrintText(graph, "ТО, ремонт", offset1, y1, x1, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			//	PrintText(graph, txt_summ_work, offset1 + x1, y1, x2, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			//	y1 += title_height;
			//	PrintText(graph, "Масла", offset1, y1, x1, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			//	PrintText(graph, txt_summ_oil, offset1 + x1, y1, x2, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			//	y1 += title_height;
			//	PrintText(graph, "Запчасти", offset1, y1, x1, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			//	PrintText(graph, txt_summ_detail, offset1 + x1, y1, x2, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			//	y1 += title_height;
			//	PrintText(graph, "Скидки", offset1, y1, x1, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			//	PrintText(graph, txt_summ_discount, offset1 + x1, y1, x2, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			//	y1 = offset + 3;
			//	PrintText(graph, "ОПЛАТА", offset2, y1, x1, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			//	y1 += title_height;
			//	PrintText(graph, "ТО, ремонт, масла", offset2, y1, x1, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			//	PrintText(graph, txt_summ_pay_work, offset2 + x1, y1, x2, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			//	y1 += title_height;
			//	PrintText(graph, "Запчасти", offset2, y1, x1, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			//	PrintText(graph, txt_summ_pay_detail, offset2 + x1, y1, x2, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			//	y1 += title_height;
			//	PrintText(graph, "ВСЕГО", offset2, y1, x1, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			//	PrintText(graph, txt_summ_pay, offset2 + x1, y1, x2, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			//	y += title_height * 4;

			return y;
		}

		private int PrintWashBlock(Graphics graph, int offset,  bool test, bool print, object o)
		{
			// Вспомогательные инструменты
			RectangleF rect;
			int y		= offset;
			string text	= "";

			// Проверка логичности данных
			if(o == null) return y;

			// Настроечные параметры
			int offset_x_left		= 10;
			int offset_x_right		= 10;
			int page_width			= 210;
			int rowheight			= 4;
			int	col1				= 30;
			int	col2				= 15;
			int	col3				= 15;
			int	col4				= 15;
			int	col5				= 15;
			int	col6				= 15;
			int	col7				= 15;
			int	col8				= 15;
			int	col9				= 190 - col1 - col2 - col3 - col4 - col5 - col6 - col7 - col8;
			
			// Получение данных для печати
			HeaderData.CardInfo info = (HeaderData.CardInfo)o;

			string txt_warrant			= (string)info.warrant_number.ToString() + " / " + (string)info.warrant_date.ToShortDateString();
			string txt_summ_work		= (string)info.summ_work.ToString();
			string txt_summ_detail		= (string)info.summ_detail.ToString();
			string txt_summ_oil			= (string)info.summ_oil.ToString();
			string txt_summ_discount	= (string)info.summ_discount.ToString();
			string txt_summ_pay_work	= (string)info.summ_pay_work.ToString();
			string txt_summ_pay_detail	= (string)info.summ_pay_detail.ToString();
			string txt_summ_pay			= (string)info.summ_pay.ToString();

			if(test == true || print == false)
			{	
				return y + rowheight;
			}
			if(info.summ_pay > 0)
			{
				PrintTextBox(graph, txt_warrant, offset_x_left, y, col1, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
				PrintTextBox(graph, txt_summ_work, offset_x_left + col1, y, col2, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
				PrintTextBox(graph, txt_summ_oil, offset_x_left + col1 + col2, y, col3, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
				PrintTextBox(graph, txt_summ_detail, offset_x_left + col1 + col2 + col3, y, col4, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
				PrintTextBox(graph, txt_summ_discount, offset_x_left + col1 + col2 + col3 + col4, y, col5, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
				PrintTextBox(graph, txt_summ_pay_work, offset_x_left + col1 + col2 + col3 + col4 + col5, y, col6, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
				PrintTextBox(graph, txt_summ_pay_detail, offset_x_left + col1 + col2 + col3 + col4 + col5 + col6, y, col7, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
				PrintTextBox(graph, txt_summ_pay, offset_x_left + col1 + col2 + col3 + col4 + col5 + col6 + col7, y, col8, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			}
			else
			{
				PrintTextBoxColor(graph, txt_warrant, offset_x_left, y, col1, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, brush_lightgray, false);
				PrintTextBoxColor(graph, txt_summ_work, offset_x_left + col1, y, col2, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, brush_lightgray, false);
				PrintTextBoxColor(graph, txt_summ_oil, offset_x_left + col1 + col2, y, col3, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, brush_lightgray, false);
				PrintTextBoxColor(graph, txt_summ_detail, offset_x_left + col1 + col2 + col3, y, col4, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, brush_lightgray, false);
				PrintTextBoxColor(graph, txt_summ_discount, offset_x_left + col1 + col2 + col3 + col4, y, col5, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, brush_lightgray, false);
				PrintTextBoxColor(graph, txt_summ_pay_work, offset_x_left + col1 + col2 + col3 + col4 + col5, y, col6, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, brush_lightgray, false);
				PrintTextBoxColor(graph, txt_summ_pay_detail, offset_x_left + col1 + col2 + col3 + col4 + col5 + col6, y, col7, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, brush_lightgray, false);
				PrintTextBoxColor(graph, txt_summ_pay, offset_x_left + col1 + col2 + col3 + col4 + col5 + col6 + col7, y, col8, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, brush_lightgray, false);
			}

			y += rowheight;
			return y;
		}
		#endregion

		#region Печать блока Магазина
		private int PrintShopBlockHead(Graphics graph, int offset,  bool test, bool print, object o)
		{
			// Вспомогательные инструменты
			RectangleF rect;
			int y		= offset + 3;
			int y1		= 0;
			string text	= "";

			// Настроечные параметры
			int offset_x_left		= 10;
			int offset_x_right		= 10;
			int page_width			= 210;
			int title_height		= 4;
			int	col1				= 190;
			int offset1				= 12;
			int offset2				= 80;
			int x1					= 30;
			int x2					= 20;

			
			// Сбор данных
			string txt_summ_work			= header_data.shop_info.summ_work.ToString();
			string txt_summ_oil				= header_data.shop_info.summ_oil.ToString();
			string txt_summ_detail			= header_data.shop_info.summ_detail.ToString();
			string txt_summ_discount		= header_data.shop_info.summ_discount.ToString();
			string txt_summ_pay_work		= header_data.shop_info.summ_pay_work.ToString();
			string txt_summ_pay_detail		= header_data.shop_info.summ_pay_detail.ToString();
			string txt_summ_pay				= header_data.shop_info.summ_pay.ToString();

			if(test == true || print == false)
			{	
				y += 3 + title_height * 5;
				return y;
			}

			// Первая строчка
			PrintTextBox(graph, "Подразделение - Магазин", offset_x_left, y, col1, title_height * 2, 1, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, pen_thin, false);
			y += title_height;
			
			y1 = y;
			PrintText(graph, "Работы", offset1, y1, x1, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			PrintText(graph, txt_summ_pay_work, offset1 + x1, y1, x2, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			y1 = y;
			PrintText(graph, "Запчасти", offset2, y1, x1, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			PrintText(graph, txt_summ_pay_detail, offset2 + x1, y1, x2, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			y += title_height;


			//	y1 = y;
			//	PrintText(graph, "ТО, ремонт", offset1, y1, x1, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			//	PrintText(graph, txt_summ_work, offset1 + x1, y1, x2, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			//	y1 += title_height;
			//	PrintText(graph, "Масла", offset1, y1, x1, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			//	PrintText(graph, txt_summ_oil, offset1 + x1, y1, x2, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			//	y1 += title_height;
			//	PrintText(graph, "Запчасти", offset1, y1, x1, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			//	PrintText(graph, txt_summ_detail, offset1 + x1, y1, x2, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			//	y1 += title_height;
			//	PrintText(graph, "Скидки", offset1, y1, x1, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			//	PrintText(graph, txt_summ_discount, offset1 + x1, y1, x2, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			//	y1 = offset + 3;
			//	PrintText(graph, "ОПЛАТА", offset2, y1, x1, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			//	y1 += title_height;
			//	PrintText(graph, "ТО, ремонт, масла", offset2, y1, x1, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			//	PrintText(graph, txt_summ_pay_work, offset2 + x1, y1, x2, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			//	y1 += title_height;
			//	PrintText(graph, "Запчасти", offset2, y1, x1, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			//	PrintText(graph, txt_summ_pay_detail, offset2 + x1, y1, x2, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			//	y1 += title_height;
			//	PrintText(graph, "ВСЕГО", offset2, y1, x1, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			//	PrintText(graph, txt_summ_pay, offset2 + x1, y1, x2, title_height,StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			//	y += title_height * 4;

			return y;
		}

		private int PrintShopBlock(Graphics graph, int offset,  bool test, bool print, object o)
		{
			// Вспомогательные инструменты
			RectangleF rect;
			int y		= offset;
			string text	= "";

			// Проверка логичности данных
			if(o == null) return y;

			// Настроечные параметры
			int offset_x_left		= 10;
			int offset_x_right		= 10;
			int page_width			= 210;
			int rowheight			= 4;
			int	col1				= 30;
			int	col2				= 15;
			int	col3				= 15;
			int	col4				= 15;
			int	col5				= 15;
			int	col6				= 15;
			int	col7				= 15;
			int	col8				= 15;
			int	col9				= 190 - col1 - col2 - col3 - col4 - col5 - col6 - col7 - col8;
			
			// Получение данных для печати
			HeaderData.CardInfo info = (HeaderData.CardInfo)o;

			string txt_warrant			= (string)info.warrant_number.ToString() + " / " + (string)info.warrant_date.ToShortDateString();
			string txt_summ_work		= (string)info.summ_work.ToString();
			string txt_summ_detail		= (string)info.summ_detail.ToString();
			string txt_summ_oil			= (string)info.summ_oil.ToString();
			string txt_summ_discount	= (string)info.summ_discount.ToString();
			string txt_summ_pay_work	= (string)info.summ_pay_work.ToString();
			string txt_summ_pay_detail	= (string)info.summ_pay_detail.ToString();
			string txt_summ_pay			= (string)info.summ_pay.ToString();

			if(test == true || print == false)
			{	
				return y + rowheight;
			}
			if(info.summ_pay > 0)
			{
				PrintTextBox(graph, txt_warrant, offset_x_left, y, col1, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
				PrintTextBox(graph, txt_summ_work, offset_x_left + col1, y, col2, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
				PrintTextBox(graph, txt_summ_oil, offset_x_left + col1 + col2, y, col3, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
				PrintTextBox(graph, txt_summ_detail, offset_x_left + col1 + col2 + col3, y, col4, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
				PrintTextBox(graph, txt_summ_discount, offset_x_left + col1 + col2 + col3 + col4, y, col5, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
				PrintTextBox(graph, txt_summ_pay_work, offset_x_left + col1 + col2 + col3 + col4 + col5, y, col6, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
				PrintTextBox(graph, txt_summ_pay_detail, offset_x_left + col1 + col2 + col3 + col4 + col5 + col6, y, col7, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
				PrintTextBox(graph, txt_summ_pay, offset_x_left + col1 + col2 + col3 + col4 + col5 + col6 + col7, y, col8, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			}
			else
			{
				PrintTextBoxColor(graph, txt_warrant, offset_x_left, y, col1, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, brush_lightgray, false);
				PrintTextBoxColor(graph, txt_summ_work, offset_x_left + col1, y, col2, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, brush_lightgray, false);
				PrintTextBoxColor(graph, txt_summ_oil, offset_x_left + col1 + col2, y, col3, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, brush_lightgray, false);
				PrintTextBoxColor(graph, txt_summ_detail, offset_x_left + col1 + col2 + col3, y, col4, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, brush_lightgray, false);
				PrintTextBoxColor(graph, txt_summ_discount, offset_x_left + col1 + col2 + col3 + col4, y, col5, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, brush_lightgray, false);
				PrintTextBoxColor(graph, txt_summ_pay_work, offset_x_left + col1 + col2 + col3 + col4 + col5, y, col6, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, brush_lightgray, false);
				PrintTextBoxColor(graph, txt_summ_pay_detail, offset_x_left + col1 + col2 + col3 + col4 + col5 + col6, y, col7, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, brush_lightgray, false);
				PrintTextBoxColor(graph, txt_summ_pay, offset_x_left + col1 + col2 + col3 + col4 + col5 + col6 + col7, y, col8, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, brush_lightgray, false);
			}

			y += rowheight;
			return y;
		}
		#endregion

		// Основная процедура печати
		public override void  PrintPage(Graphics graph, int page)
		{
			// Для ориентации на странице
			int offset = 0;
			offset = 10;
			offset = PrintBlock(graph, offset, new DelegatePrintBlock(PrintMain), null);
			// Печать все заказ нарядов по подразделению сервис
			bool first = true;
			if(header_data.cards_service_info.Count > 0)
			{
				foreach(HeaderData.CardInfo element in header_data.cards_service_info)
				{
					offset = PrintBlockWithHeader(graph, offset, new DelegatePrintBlock(PrintServiceBlock), element, new DelegatePrintBlock(PrintServiceBlockHead), first);
					first = false;
				}
			}
			else
			{
				offset = PrintBlockWithHeader(graph, offset, new DelegatePrintBlock(PrintServiceBlock), null, new DelegatePrintBlock(PrintServiceBlockHead), first);
			}
			// Печать все заказ нарядов по подразделению музыка
			first = true;
			if(header_data.cards_music_info.Count > 0)
			{
				foreach(HeaderData.CardInfo element in header_data.cards_music_info)
				{
					offset = PrintBlockWithHeader(graph, offset, new DelegatePrintBlock(PrintMusicBlock), element, new DelegatePrintBlock(PrintMusicBlockHead), first);
					first = false;
				}
			}
			else
			{
				offset = PrintBlockWithHeader(graph, offset, new DelegatePrintBlock(PrintMusicBlock), null, new DelegatePrintBlock(PrintMusicBlockHead), first);
			}
			// Печать все заказ нарядов по подразделению тюнинг
			first = true;
			if(header_data.cards_tuning_info.Count > 0)
			{
				foreach(HeaderData.CardInfo element in header_data.cards_tuning_info)
				{
					offset = PrintBlockWithHeader(graph, offset, new DelegatePrintBlock(PrintTuningBlock), element, new DelegatePrintBlock(PrintTuningBlockHead), first);
					first = false;
				}
			}
			else
			{
				offset = PrintBlockWithHeader(graph, offset, new DelegatePrintBlock(PrintTuningBlock), null, new DelegatePrintBlock(PrintTuningBlockHead), first);
			}
			// Печать все заказ нарядов по подразделению антикор
			first = true;
			if(header_data.cards_antikor_info.Count > 0)
			{
				foreach(HeaderData.CardInfo element in header_data.cards_antikor_info)
				{
					offset = PrintBlockWithHeader(graph, offset, new DelegatePrintBlock(PrintAntikorBlock), element, new DelegatePrintBlock(PrintAntikorBlockHead), first);
					first = false;
				}
			}
			else
			{
				offset = PrintBlockWithHeader(graph, offset, new DelegatePrintBlock(PrintAntikorBlock), null, new DelegatePrintBlock(PrintAntikorBlockHead), first);
			}
			// Печать все заказ нарядов по подразделению мялярка
			first = true;
			if(header_data.cards_paint_info.Count > 0)
			{
				foreach(HeaderData.CardInfo element in header_data.cards_paint_info)
				{
					offset = PrintBlockWithHeader(graph, offset, new DelegatePrintBlock(PrintPaintBlock), element, new DelegatePrintBlock(PrintPaintBlockHead), first);
					first = false;
				}
			}
			else
			{
				offset = PrintBlockWithHeader(graph, offset, new DelegatePrintBlock(PrintPaintBlock), null, new DelegatePrintBlock(PrintPaintBlockHead), first);
			}
			// Печать все заказ нарядов по подразделению мойка
			first = true;
			if(header_data.cards_wash_info.Count > 0)
			{
				foreach(HeaderData.CardInfo element in header_data.cards_wash_info)
				{
					offset = PrintBlockWithHeader(graph, offset, new DelegatePrintBlock(PrintWashBlock), element, new DelegatePrintBlock(PrintWashBlockHead), first);
					first = false;
				}
			}
			else
			{
				offset = PrintBlockWithHeader(graph, offset, new DelegatePrintBlock(PrintWashBlock), null, new DelegatePrintBlock(PrintWashBlockHead), first);
			}
			// Печать все заказ нарядов по подразделению магазин
			first = true;
			if(header_data.cards_shop_info.Count > 0)
			{
				foreach(HeaderData.CardInfo element in header_data.cards_shop_info)
				{
					offset = PrintBlockWithHeader(graph, offset, new DelegatePrintBlock(PrintShopBlock), element, new DelegatePrintBlock(PrintShopBlockHead), first);
					first = false;
				}
			}
			else
			{
				offset = PrintBlockWithHeader(graph, offset, new DelegatePrintBlock(PrintShopBlock), null, new DelegatePrintBlock(PrintShopBlockHead), first);
			}
		}
	}
}
