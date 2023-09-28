using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbPrintInspectionUndercarriage.
	/// </summary>
	public class DbPrintInspectionUndercarriage:DbPrint
	{
		// Инструменты для печати
		SolidBrush	brush_standart;
		SolidBrush	brush_lightgray;
		Font		font_small_bold;
		Font		font_middle_bold;
		Font		font_middle;
		Font		font_small;
		Pen			pen_thin;

		private DtCard		card;

		#region Данные для печати
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
				// Данные карточки
				txt_card = card.GetData("НОМЕР_КАРТОЧКА").ToString() + "/" + card.GetData("ГОД_КАРТОЧКА").ToString();
				if((long)card.GetData("НОМЕР_НАРЯД_КАРТОЧКА") != 0)
				{
					txt_warrant = card.GetData("НОМЕР_НАРЯД_КАРТОЧКА").ToString() + " от " + card.GetData("ДАТА_НАРЯД_ОТКРЫТ_КАРТОЧКА").ToString();
				}
				if((short)card.GetData("СТАТУС_КАРТОЧКА") == 2)
				{
					txt_warrant_close = card.GetData("ДАТА_НАРЯД_ЗАКРЫТ_КАРТОЧКА").ToString();
				}
				if((bool)card.GetData("БЕЗНАЛИЧНЫЙ_КАРТОЧКА") == true)
				{
					cashless = true;
				}
				if((bool)card.GetData("ВНУТРЕННИЙ_КАРТОЧКА") == true)
				{
					inner = true;
				}
				int run = (int)card.GetData("ПРОБЕГ_КАРТОЧКА");
				if(run != 0)
				{
					txt_run		= run.ToString();
				}
				// Загрузка отображаемых данных заголовка
				DtAuto auto = DbSqlAuto.Find((long)card.CodeAuto/*GetData("АВТОМОБИЛЬ_КАРТОЧКА")*/);
				if(auto != null)
				{
					txt_sign	= (string)auto.GetData("НОМЕР_ЗНАК");
					txt_vin		= (string)auto.GetData("VIN");
					// Данные по автомобилю
					// Модель
					DtModel	model = DbSqlModel.Find((long)auto.GetData("ССЫЛКА_КОД_АВТОМОБИЛЬ_МОДЕЛЬ"));
					if(model != null)
						txt_model	= (string)model.GetData("МОДЕЛЬ");
					// Дата продажи
					if((bool)auto.GetData("ЕСТЬ_ПРОДАЖА_ДАТА") == true)
						txt_sell = ((DateTime)auto.GetData("ПРОДАЖА_ДАТА")).ToShortDateString();
					else
						txt_sell = "---------";

					// Тип официального дилера
					DtBrand brand = DbSqlBrand.FindModel((long)model.GetData("КОД_АВТОМОБИЛЬ_МОДЕЛЬ"));
					string txt_brand = "";
					if(brand != null)
						txt_brand = (string)brand.GetData("НАИМЕНОВАНИЕ_АВТОМОБИЛЬ_БРЕНД");
					if(txt_brand == "ШЕВРОЛЕ") dialer = DIALER.chevrolet;
					if(txt_brand == "LADA") dialer = DIALER.lada;
					if(txt_brand == "KIA") dialer = DIALER.kia;

					// АНАЛИЗ ПРОФИЛЬНОГО ДИЛЕРА
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
				// Владелец
				DtPartner owner = DbSqlPartner.Find((long)card.GetData("ВЛАДЕЛЕЦ_КАРТОЧКА"));
				if(owner != null)
				{
					txt_owner		= owner.GetTitle();
					txt_address		= owner.GetAddress();
					txt_phone		= owner.GetPhone();
				}
				// Представитель
				DtPartner represent = DbSqlPartner.Find((long)card.GetData("ПРЕДСТАВИТЕЛЬ_КАРТОЧКА"));
				if(represent != null)
					txt_represent	= represent.GetTitle();

				// Список работ в заказ-наряде
				works = new ArrayList();
				DbSqlCardWork.SelectInArray(card, works);
				float summ_work = 0.0F;
				foreach(DtCardWork element in works)
				{
					if((bool)element.GetData("ГАРАНТИЯ_КАРТОЧКА_РАБОТА") == false)
					{
						float nv = (float)element.GetData("ТРУДОЕМКОСТЬ_КАРТОЧКА_РАБОТА");
						if(nv != 0.0F)
						{
							summ_work += nv * (float)element.GetData("НОРМАЧАС_КАРТОЧКА_РАБОТА") * (float)element.GetData("КОЛИЧЕСТВО_КАРТОЧКА_РАБОТА");
						}
						else
						{
							summ_work += (float)element.GetData("НОРМАЧАС_КАРТОЧКА_РАБОТА") * (float)element.GetData("КОЛИЧЕСТВО_КАРТОЧКА_РАБОТА");
						}
					}
				}

				// Список деталей в заказ-наряде
				details = new ArrayList();
				DbSqlCardDetail.SelectInArray(card, details);
				float summ_oil = 0.0F;
				float summ_detail = 0.0F;
				foreach(DtCardDetail element in details)
				{
					if((bool)element.GetData("ЖИДКОСТЬ_КАРТОЧКА_ДЕТАЛЬ") == true)
					{
						if((bool)element.GetData("ГАРАНТИЯ_КАРТОЧКА_ДЕТАЛЬ") == false)
						{
							summ_oil += (float)element.GetData("КОЛИЧЕСТВО_КАРТОЧКА_ДЕТАЛЬ") * (float)element.GetData("ЦЕНА_КАРТОЧКА_ДЕТАЛЬ");
						}
					}
					else
					{
						if((bool)element.GetData("ГАРАНТИЯ_КАРТОЧКА_ДЕТАЛЬ") == false)
						{
							summ_detail += (float)element.GetData("КОЛИЧЕСТВО_КАРТОЧКА_ДЕТАЛЬ") * (float)element.GetData("ЦЕНА_КАРТОЧКА_ДЕТАЛЬ");
						}
					}
				}

				// Список рекомендаций
				recomendations = new ArrayList();
				DbSqlCardRecomendation.SelectInArray(recomendations, (long)card.GetData("НОМЕР_КАРТОЧКА"), (int)card.GetData("ГОД_КАРТОЧКА"));
				foreach(object o in recomendations)
				{
					DtCardRecomendation recomendation = (DtCardRecomendation)o;
					string s = (string)recomendation.GetData("РЕКОМЕНДАЦИЯ");
					txt_recomendations += s;
					txt_recomendations += "; ";
				}

				// Данные по сумме работ, с учетом скидки
				float discount_work		= 0.0F;
				float summ_work_pay		= 0.0F;
				float summ_work_oil		= 0.0F;
				float discount_val		= 0.0F;
				discount_val	= (float)card.GetData("СКИДКА_РАБОТА_КАРТОЧКА");
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

		public DbPrintInspectionUndercarriage(long card_number, int card_year)
		{
			// Подготовка инструментов для печати
			brush_standart		= new SolidBrush(Color.Black);
			brush_lightgray		= new SolidBrush(Color.LightGray);
			font_small_bold		= new Font("Arial", 8, FontStyle.Bold);
			font_small			= new Font("Arial", 8);
			font_middle_bold	= new Font("Arial", 10, FontStyle.Bold);
			font_middle			= new Font("Arial", 10);
			pen_thin			= new Pen(brush_standart, 0.3F);

			// Заявка клиента печатается по карточке (даже по нрулевой)
			if(card_number != 0 && card_year != 0)
				// Поиск карточки по входным данным
				card =	DbSqlCard.Find(card_number, card_year);
			else
				// Карточка нулевая
				card = null;

			if(card != null)
			{
				// Сумели найти карточку, получаем данные для печати
				header_data = new HeaderData(card);
			}
		}

		#region Печать заголовка
		private int PrintHead(Graphics graph, int offset,  bool test, bool print, object o)
		{
			// Настройка параметров печати заголовка
			SizeF size;
			int	height;
			int	y;
			string txt	= "";

			int	header_height_real	= 0;		// Реально получившаяся высота заголовка
			int header_height		= 50;		// Фиксированная высота заголовка
			int text_offset			= 2;		// Отсуп текста внутри оконтовки
			int header_line_height	= 4;		// Высота линии
			int	header_x			= 10;		// Смещение заголовка по х
			int	header_title_width	= 40;		// Ширина блока заголовка для наименований
			int	header_data_width	= 75;		// Ширина блока заголовка для данных
			if(test == true || print == false)
			{
				// Просто рассчитываем высоту
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
				return y + 3;	// Отсуп от заголовка
			}

			y	= offset;
			// НАЗВАНИЕ ДОКУМЕНТА
			PrintText(graph, "АКТ ВИЗУАЛЬНОГО ОСМОТРА ХОДОВОЙ ЧАСТИ", header_x, y, 150, 5, StringAlignment.Near, StringAlignment.Center, font_middle_bold, brush_standart, false);
			y += 5;
			PrintText(graph, DateTime.Now.ToShortDateString(), header_x, y, 150, 5, StringAlignment.Near, StringAlignment.Center, font_middle_bold, brush_standart, false);
			y += 5;
			// ДАННЫЕ ЗАГОЛОВКА
			// Модель
			PrintTextNoBox(graph, "Модель:", header_x, y, header_title_width, header_line_height, text_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextNoBox(graph, header_data.txt_model, header_x + header_title_width, y, header_data_width, header_line_height, text_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			y += header_line_height;
			// Регистрационный знак
			PrintTextBox(graph, "Гос. номер:", header_x, y, header_title_width, header_line_height, text_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, header_data.txt_sign, header_x + header_title_width, y, header_data_width, header_line_height, text_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			y += header_line_height;
			// VIN
			PrintTextBox(graph, "VIN:", header_x, y, header_title_width, header_line_height, text_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, header_data.txt_vin, header_x + header_title_width, y, header_data_width, header_line_height, text_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			y += header_line_height;
			// Дата продажи
			PrintTextBox(graph, "Дата продажи:", header_x, y, header_title_width, header_line_height, text_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, header_data.txt_sell, header_x + header_title_width, y, header_data_width, header_line_height, text_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			y += header_line_height;
			// Пробег
			PrintTextBox(graph, "Пробег:", header_x, y, header_title_width, header_line_height, text_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, header_data.txt_run, header_x + header_title_width, y, header_data_width, header_line_height, text_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			y += header_line_height;
			// Владелец
			// Сначала измеряем строчку, чтобы определить истинный размер
			size	= MeasureTextBox(graph, header_data.txt_owner, header_data_width, text_offset, StringAlignment.Near, StringAlignment.Near, font_small_bold);
			height	= (int)Math.Ceiling(size.Height);
			if(height < header_line_height) height = header_line_height;
			PrintTextBox(graph, "Владелец:", header_x, y, header_title_width, height, text_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, header_data.txt_owner, header_x + header_title_width, y, header_data_width, height, text_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			y += height;
			// Адрес
			size	= MeasureTextBox(graph, header_data.txt_address, header_data_width, text_offset, StringAlignment.Near, StringAlignment.Near, font_small_bold);
			height	= (int)Math.Ceiling(size.Height);
			if(height < header_line_height) height = header_line_height;
			PrintTextBox(graph, "Адрес:", header_x, y, header_title_width, height, text_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, header_data.txt_address, header_x + header_title_width, y, header_data_width, height, text_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			y += height;
			// Телефон
			PrintTextBox(graph, "Телефон:", header_x, y, header_title_width, header_line_height, text_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, header_data.txt_phone, header_x + header_title_width, y, header_data_width, header_line_height, text_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			y += header_line_height;
			// Доверенное лицо
			PrintTextNoBox(graph, "Доверенное лицо:", header_x, y, header_title_width, header_line_height, text_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextNoBox(graph, header_data.txt_represent, header_x + header_title_width, y, header_data_width, header_line_height, text_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			y += header_line_height;
			// Внешний закругленный прямоугольник и вертикальное дополнение
			RectangleF rect1	= new RectangleF(header_x, offset + 10, header_data_width + header_title_width, y - (offset + 10));
			PrintRoundBox(graph, rect1, pen_thin, 4);
			PrintLineVrt(graph, header_x + header_title_width, offset + 10, y - (offset + 10), pen_thin, false);


			header_height_real = y;
			// ЛОГОТИП ОФИЦИАЛЬНОГО ДИЛЕРА (ДЛЯ ПРОФИЛЬНЫХ АВТОМОБИЛЕЙ)
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
				PrintText(graph, "ОФИЦИАЛЬНЫЙ ДИЛЕР", image_x, offset + 20, 39, 4, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, false);
			// БЛОК РЕКВИЗИТОВ ТЕХНИЧЕСКОГО ЦЕНТРА
			int radius					= 3;
			int addr_block_x			= header_title_width + header_data_width + header_x + 5;
			int addr_block_y			= offset + 20 + 4;
			int addr_block_width		= 200 - addr_block_x;
			int addr_block_height		= header_height_real - addr_block_y;
			int addr_block_line_height	= 4;
			RectangleF rect = new RectangleF(addr_block_x, addr_block_y , addr_block_width, addr_block_height);
			PrintRoundBox(graph, rect, pen_thin, radius);

			// Зачитываем данные из настроечного файла
			txt = FileIni.GetParameter("print.ini", "#ADDRESS_BLOCK");
			this.PrintTextNoBox(graph, txt, rect, 1, StringAlignment.Center, StringAlignment.Center, font_small, brush_standart, false);
			y += addr_block_height;

			//		y = addr_block_y + (addr_block_height - addr_block_line_height * 5) / 2;
			//		PrintText(graph, "Сервисный центр \"Авто-1\"", addr_block_x + radius, y, addr_block_width - radius * 2, addr_block_line_height, StringAlignment.Center, StringAlignment.Center, font_small, brush_standart, false);
			//		y += addr_block_line_height;
			//		PrintText(graph, "Тел. (383) 332-02-92 Факс (383) 333-87-08", addr_block_x + radius, y, addr_block_width - radius * 2, addr_block_line_height, StringAlignment.Center, StringAlignment.Center, font_small, brush_standart, false);
			//		y += addr_block_line_height;
			//		PrintText(graph, "630058, г.Новосибирск", addr_block_x + radius, y, addr_block_width - radius * 2, addr_block_line_height, StringAlignment.Center, StringAlignment.Center, font_small, brush_standart, false);
			//		y += addr_block_line_height;
			//		PrintText(graph, "Академгородок, ул. Русская, 48", addr_block_x + radius, y, addr_block_width - radius * 2, addr_block_line_height, StringAlignment.Center, StringAlignment.Center, font_small, brush_standart, false);
			//		y += addr_block_line_height;
			//		PrintText(graph, "www.avto-1.ru", addr_block_x + radius, y, addr_block_width - radius * 2, addr_block_line_height, StringAlignment.Center, StringAlignment.Center, font_small, brush_standart, false);


			return header_height_real + 3;
		}
		#endregion

		#region Печать завершающего блока
		private int PrintFooter(Graphics graph, int offset,  bool test, bool print, object o)
		{
			// Вспомогательные инструменты
			RectangleF rect;
			SizeF size;
			int y		= offset;
			string txt	= "";
		
			// Настроечные параметры
			int offset_x_left_ini		= 15;
			int offset_x_right			= 10;
			int page_width				= 210;
			int w_page					= page_width - offset_x_right - offset_x_left_ini;

			int offset_x_left	= 0;
			int	title_height	= 5;
			int	row_height	= 4;
			int w1			= 30;
			int w11			= 35;
			int w2			= 10;
			int w3			= 5;
			int box_width	= 2;
			int box_height	= 2;
			
			y += 3;

			if(test == true || print == false)
			{	
				// Заголовок документа
				//y += title_height;
				//y += title_height;

				// Блок рулевого управления
				y += row_height;
				y += row_height;
				y += row_height;
				y += row_height;
				y += row_height;

				return y;
			}
			
			// Заголовок документа
			//txt = "АКТ ВИЗУАЛЬНОГО ОСМОТРА ХОДОВОЙ ЧАСТИ";
			//PrintText(graph, txt, offset_x_left, y, w_page, title_height, StringAlignment.Center, StringAlignment.Near, font_middle_bold, brush_standart, false);
			//y += title_height;
			//y += title_height;

			// Блок рулевого управления
			RectangleF block_rect = new RectangleF(offset_x_left_ini - 5, y - 2, 0, 0);
			// Блок рулевой рейки
			int y0 = y;
			offset_x_left	= offset_x_left_ini;
			PrintText(graph, "Рулевая рейка", offset_x_left, y, w1, row_height, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, false);
			y += row_height;
			PrintText(graph, "Да", offset_x_left + w1, y, w2, row_height, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
			PrintText(graph, "Нет", offset_x_left + w1 + w2, y, w2, row_height, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
			y += row_height;
			rect = new RectangleF(offset_x_left + w1 + (w2 - box_width) / 2, y + (row_height - box_height) / 2, box_width, box_height);
			PrintText(graph, "Требуется замена", offset_x_left, y, w1, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			rect.X += w2;
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			rect.X -= w2;
			PrintText(graph, "Требуется ремонт", offset_x_left, y, w1, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			rect.X += w2;
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			rect.X -= w2;
			PrintText(graph, "Замена пыльников", offset_x_left, y, w1, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			rect.X += w2;
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			rect.X -= w2;
			PrintText(graph, "Обнаружен люфт", offset_x_left, y, w1, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			rect.X += w2;
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			rect.X -= w2;
			PrintText(graph, "Обнаружен стук", offset_x_left, y, w1, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			rect.X += w2;
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			// Блок рулевого редуктора
			y = y0;
			offset_x_left	= offset_x_left_ini + w1 + w2 * 2;
			PrintText(graph, "Рулевой редуктор", offset_x_left, y, w1, row_height, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, false);
			y += row_height;
			PrintText(graph, "Да", offset_x_left + w1, y, w2, row_height, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
			PrintText(graph, "Нет", offset_x_left + w1 + w2, y, w2, row_height, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
			y += row_height;
			rect = new RectangleF(offset_x_left + w1 + (w2 - box_width) / 2, y + (row_height - box_height) / 2, box_width, box_height);
			PrintText(graph, "Требуется замена", offset_x_left, y, w1, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			rect.X += w2;
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			rect.X -= w2;
			PrintText(graph, "Требуется ремонт", offset_x_left, y, w1, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			rect.X += w2;
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			rect.X -= w2;
			PrintText(graph, "Течь масла", offset_x_left, y, w1, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			rect.X += w2;
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			rect.X -= w2;
			PrintText(graph, "Обнаружен люфт", offset_x_left, y, w1, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			rect.X += w2;
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			rect.X -= w2;
			PrintText(graph, "Обнаружен стук", offset_x_left, y, w1, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			rect.X += w2;
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			// Наконечник рулевой тяги левый
			y = y0;
			offset_x_left	= offset_x_left_ini + w1*2 + w2 * 4;
			PrintText(graph, "Наконечник рулевой тяги", offset_x_left, y, w1, row_height * 2, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, false);
			PrintText(graph, "Левый", offset_x_left + w1, y, w2 * 2, row_height, StringAlignment.Center, StringAlignment.Near, font_small_bold, brush_standart, false);
			PrintText(graph, "Правый", offset_x_left + w1 + w2 * 2, y, w2 * 2, row_height, StringAlignment.Center, StringAlignment.Near, font_small_bold, brush_standart, false);
			y += row_height;
			PrintText(graph, "Да", offset_x_left + w1, y, w2, row_height, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
			PrintText(graph, "Нет", offset_x_left + w1 + w2, y, w2, row_height, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
			PrintText(graph, "Да", offset_x_left + w1 + w2 * 2, y, w2, row_height, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
			PrintText(graph, "Нет", offset_x_left + w1 + w2 * 2 + w2, y, w2, row_height, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
			y += row_height;
			rect = new RectangleF(offset_x_left + w1 + (w2 - box_width) / 2, y + (row_height - box_height) / 2, box_width, box_height);
			PrintText(graph, "Замена чехла", offset_x_left, y, w1, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			rect.X += w2;
			PrintBox(graph, rect, pen_thin);
			rect.X += w2;
			PrintBox(graph, rect, pen_thin);
			rect.X += w2;
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			rect.X -= w2 * 3;
			PrintText(graph, "Состояние", offset_x_left, y, w1, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			rect.Y	+= row_height;
			y		+= row_height;
			PrintText(graph, "Норма", offset_x_left, y, w1, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			rect.X += w2 / 2;
			PrintBox(graph, rect, pen_thin);
			rect.X += w2 * 2;
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			rect.X -= w2 * 2;
			PrintText(graph, "Удовлетворительное", offset_x_left, y, w1, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			rect.X += w2 * 2;
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			rect.X -= w2 * 2;
			PrintText(graph, "Требуется замена", offset_x_left, y, w1, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			rect.X += w2 * 2;
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			// Тяга рулевой трапеции
			y0 = y;
			offset_x_left	= offset_x_left_ini;
			PrintText(graph, "Тяга рулевой трапеции", offset_x_left, y, w1, row_height * 2, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, false);
			PrintText(graph, "Левая", offset_x_left + w1, y, w2 * 2, row_height, StringAlignment.Center, StringAlignment.Near, font_small_bold, brush_standart, false);
			PrintText(graph, "Правая", offset_x_left + w1 + w2 * 2, y, w2 * 2, row_height, StringAlignment.Center, StringAlignment.Near, font_small_bold, brush_standart, false);
			y += row_height;
			PrintText(graph, "Да", offset_x_left + w1, y, w2, row_height, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
			PrintText(graph, "Нет", offset_x_left + w1 + w2, y, w2, row_height, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
			PrintText(graph, "Да", offset_x_left + w1 + w2 * 2, y, w2, row_height, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
			PrintText(graph, "Нет", offset_x_left + w1 + w2 * 2 + w2, y, w2, row_height, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
			y += row_height;
			rect = new RectangleF(offset_x_left + w1 + (w2 - box_width) / 2, y + (row_height - box_height) / 2, box_width, box_height);
			PrintText(graph, "Замена чехла", offset_x_left, y, w1, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			rect.X += w2;
			PrintBox(graph, rect, pen_thin);
			rect.X += w2;
			PrintBox(graph, rect, pen_thin);
			rect.X += w2;
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			rect.X -= w2 * 3;
			PrintText(graph, "Состояние", offset_x_left, y, w1, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			rect.Y	+= row_height;
			y		+= row_height;
			PrintText(graph, "Норма", offset_x_left, y, w1, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			rect.X += w2 / 2;
			PrintBox(graph, rect, pen_thin);
			rect.X += w2 * 2;
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			rect.X -= w2 * 2;
			PrintText(graph, "Удовлетворительное", offset_x_left, y, w1, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			rect.X += w2 * 2;
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			rect.X -= w2 * 2;
			PrintText(graph, "Требуется замена", offset_x_left, y, w1, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			rect.X += w2 * 2;
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			// Тяга рулевая средняя
			y = y0;
			offset_x_left	= offset_x_left_ini + w1 + w2 * 4;
			PrintText(graph, "Тяга рулевая средняя", offset_x_left, y, w1, row_height * 2, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, false);
			y += row_height;
			y += row_height;
			rect = new RectangleF(offset_x_left + w1 + (w2 - box_width) / 2, y + (row_height - box_height) / 2, box_width, box_height);
			PrintText(graph, "Состояние", offset_x_left, y, w1, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			rect.Y	+= row_height;
			y		+= row_height;
			PrintText(graph, "Норма", offset_x_left, y, w1, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			PrintText(graph, "Удовлетворительное", offset_x_left, y, w1, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			PrintText(graph, "Требуется замена", offset_x_left, y, w1, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			// Маятник
			y = y0;
			offset_x_left	= offset_x_left_ini + w1 + w2 * 4 + w1 + w2;
			PrintText(graph, "Маятник", offset_x_left, y, w1, row_height * 2, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, false);
			y += row_height;
			y += row_height;
			rect = new RectangleF(offset_x_left + w1 + (w2 - box_width) / 2, y + (row_height - box_height) / 2, box_width, box_height);
			PrintText(graph, "Состояние", offset_x_left, y, w1, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			rect.Y	+= row_height;
			y		+= row_height;
			PrintText(graph, "Норма", offset_x_left, y, w1, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			PrintText(graph, "Удовлетворительное", offset_x_left, y, w1, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			PrintText(graph, "Требуется замена", offset_x_left, y, w1, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;

			block_rect.Height = y - block_rect.Y + 4;
			block_rect.Width  = offset_x_left_ini + w1 + w2 * 4 + w1 + w2 + w1 + w2 * 2 + 5 + 5;
			PrintBlockSigned(graph, "Рулевое управление", block_rect, 10, font_middle_bold, brush_standart, pen_thin, false);

			y += row_height;
			y += row_height;
			y += row_height;

			// БЛОК ХОДОВОЙ ЧАСТИ
			block_rect = new RectangleF(offset_x_left_ini - 5, y - 2, 0, 0);
			// Шаровые опоры
			// Левая верхняя
			y0 = y;
			offset_x_left	= offset_x_left_ini;
			PrintText(graph, "Опора шаровая левая верхняя", offset_x_left, y, w1, row_height * 2, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, false);
			y += row_height;
			y += row_height;
			rect = new RectangleF(offset_x_left + w1 + (w2 - box_width) / 2, y + (row_height - box_height) / 2, box_width, box_height);
			PrintText(graph, "Норма", offset_x_left, y, w1, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			PrintText(graph, "Требуется замена", offset_x_left, y, w1, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			// Левая нижняя
			y = y0;
			offset_x_left	= offset_x_left_ini + w1 + w2;
			PrintText(graph, "Опора шаровая левая нижняя", offset_x_left, y, w1, row_height * 2, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, false);
			y += row_height;
			y += row_height;
			rect = new RectangleF(offset_x_left + w1 + (w2 - box_width) / 2, y + (row_height - box_height) / 2, box_width, box_height);
			PrintText(graph, "Норма", offset_x_left, y, w1, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			PrintText(graph, "Требуется замена", offset_x_left, y, w1, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			// Правая верхняя
			y = y0;
			offset_x_left	= offset_x_left_ini + w1*2 + w2*2;
			PrintText(graph, "Опора шаровая правая верхняя", offset_x_left, y, w1, row_height * 2, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, false);
			y += row_height;
			y += row_height;
			rect = new RectangleF(offset_x_left + w1 + (w2 - box_width) / 2, y + (row_height - box_height) / 2, box_width, box_height);
			PrintText(graph, "Норма", offset_x_left, y, w1, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			PrintText(graph, "Требуется замена", offset_x_left, y, w1, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			// Правая нижняя
			y = y0;
			offset_x_left	= offset_x_left_ini + w1*3 + w2*3;
			PrintText(graph, "Опора шаровая правая нижняя", offset_x_left, y, w1, row_height * 2, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, false);
			y += row_height;
			y += row_height;
			rect = new RectangleF(offset_x_left + w1 + (w2 - box_width) / 2, y + (row_height - box_height) / 2, box_width, box_height);
			PrintText(graph, "Норма", offset_x_left, y, w1, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			PrintText(graph, "Требуется замена", offset_x_left, y, w1, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			// Амортизаторы
			// Передний левый
			y0 = y;
			offset_x_left	= offset_x_left_ini;
			PrintText(graph, "Стойка/амортизатор передний левый", offset_x_left, y, w11, row_height * 2, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, false);
			y += row_height;
			y += row_height;
			rect = new RectangleF(offset_x_left + w11 + (w2 - box_width) / 2, y + (row_height - box_height) / 2, box_width, box_height);
			PrintText(graph, "Норма", offset_x_left, y, w1, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			PrintText(graph, "Течь масла", offset_x_left, y, w11, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			PrintText(graph, "Повреждение", offset_x_left, y, w11, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			PrintText(graph, "Требуется диагностика", offset_x_left, y, w11, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			// Передний правый
			y = y0;
			offset_x_left	= offset_x_left_ini + w11 + w2;
			PrintText(graph, "Стойка/амортизатор передний правый", offset_x_left, y, w11, row_height * 2, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, false);
			y += row_height;
			y += row_height;
			rect = new RectangleF(offset_x_left + w11 + (w2 - box_width) / 2, y + (row_height - box_height) / 2, box_width, box_height);
			PrintText(graph, "Норма", offset_x_left, y, w1, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			PrintText(graph, "Течь масла", offset_x_left, y, w11, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			PrintText(graph, "Повреждение", offset_x_left, y, w11, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			PrintText(graph, "Требуется диагностика", offset_x_left, y, w11, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			// Задний левый
			y = y0;
			offset_x_left	= offset_x_left_ini + w11*2 + w2*2;
			PrintText(graph, "Стойка/амортизатор задний левый", offset_x_left, y, w11, row_height * 2, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, false);
			y += row_height;
			y += row_height;
			rect = new RectangleF(offset_x_left + w11 + (w2 - box_width) / 2, y + (row_height - box_height) / 2, box_width, box_height);
			PrintText(graph, "Норма", offset_x_left, y, w1, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			PrintText(graph, "Течь масла", offset_x_left, y, w11, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			PrintText(graph, "Повреждение", offset_x_left, y, w11, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			PrintText(graph, "Требуется диагностика", offset_x_left, y, w11, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			// Задний правый
			y = y0;
			offset_x_left	= offset_x_left_ini + w11*3 + w2*3;
			PrintText(graph, "Стойка/амортизатор задний правый", offset_x_left, y, w11, row_height * 2, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, false);
			y += row_height;
			y += row_height;
			rect = new RectangleF(offset_x_left + w11 + (w2 - box_width) / 2, y + (row_height - box_height) / 2, box_width, box_height);
			PrintText(graph, "Норма", offset_x_left, y, w1, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			PrintText(graph, "Течь масла", offset_x_left, y, w11, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			PrintText(graph, "Повреждение", offset_x_left, y, w11, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			PrintText(graph, "Требуется диагностика", offset_x_left, y, w11, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			// Колесные подшипники
			// Передний левый
			y0 = y;
			offset_x_left	= offset_x_left_ini;
			PrintText(graph, "Колесный подшипник передний левый", offset_x_left, y, w11, row_height * 2, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, false);
			y += row_height;
			y += row_height;
			rect = new RectangleF(offset_x_left + w11 + (w2 - box_width) / 2, y + (row_height - box_height) / 2, box_width, box_height);
			PrintText(graph, "Норма", offset_x_left, y, w1, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			PrintText(graph, "Люфт", offset_x_left, y, w11, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			PrintText(graph, "Требуется замена", offset_x_left, y, w11, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			// Передний правый
			y = y0;
			offset_x_left	= offset_x_left_ini + w11 + w2;
			PrintText(graph, "Колесный подшипник передний правый", offset_x_left, y, w11, row_height * 2, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, false);
			y += row_height;
			y += row_height;
			rect = new RectangleF(offset_x_left + w11 + (w2 - box_width) / 2, y + (row_height - box_height) / 2, box_width, box_height);
			PrintText(graph, "Норма", offset_x_left, y, w1, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			PrintText(graph, "Люфт", offset_x_left, y, w11, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			PrintText(graph, "Требуется замена", offset_x_left, y, w11, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			// Задний левый
			y = y0;
			offset_x_left	= offset_x_left_ini + w11*2 + w2*2;
			PrintText(graph, "Колесный подшипник задний левый", offset_x_left, y, w11, row_height * 2, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, false);
			y += row_height;
			y += row_height;
			rect = new RectangleF(offset_x_left + w11 + (w2 - box_width) / 2, y + (row_height - box_height) / 2, box_width, box_height);
			PrintText(graph, "Норма", offset_x_left, y, w1, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			PrintText(graph, "Люфт", offset_x_left, y, w11, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			PrintText(graph, "Требуется замена", offset_x_left, y, w11, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			// Задний правый
			y = y0;
			offset_x_left	= offset_x_left_ini + w11*3 + w2*3;
			PrintText(graph, "Колесный подшипник задний правый", offset_x_left, y, w11, row_height * 2, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, false);
			y += row_height;
			y += row_height;
			rect = new RectangleF(offset_x_left + w11 + (w2 - box_width) / 2, y + (row_height - box_height) / 2, box_width, box_height);
			PrintText(graph, "Норма", offset_x_left, y, w1, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			PrintText(graph, "Люфт", offset_x_left, y, w11, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			PrintText(graph, "Требуется замена", offset_x_left, y, w11, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			// Рычаг подвески
			// Левый верхний
			y0 = y;
			offset_x_left	= offset_x_left_ini;
			PrintText(graph, "Рычаг подвески левый верхний", offset_x_left, y, w11, row_height * 2, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, false);
			y += row_height;
			y += row_height;
			rect = new RectangleF(offset_x_left + w11 + (w2 - box_width) / 2, y + (row_height - box_height) / 2, box_width, box_height);
			PrintText(graph, "Норма", offset_x_left, y, w1, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			PrintText(graph, "Следы повреждений", offset_x_left, y, w11, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			PrintText(graph, "Требуется замена", offset_x_left, y, w11, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			// Левый нижний
			y = y0;
			offset_x_left	= offset_x_left_ini + w11 + w2;
			PrintText(graph, "Рычаг подвески левый нижний", offset_x_left, y, w11, row_height * 2, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, false);
			y += row_height;
			y += row_height;
			rect = new RectangleF(offset_x_left + w11 + (w2 - box_width) / 2, y + (row_height - box_height) / 2, box_width, box_height);
			PrintText(graph, "Норма", offset_x_left, y, w1, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			PrintText(graph, "Следы повреждений", offset_x_left, y, w11, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			PrintText(graph, "Требуется замена", offset_x_left, y, w11, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			// Правый верхний
			y = y0;
			offset_x_left	= offset_x_left_ini + w11*2 + w2*2;
			PrintText(graph, "Рычаг подвески правый верхний", offset_x_left, y, w11, row_height * 2, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, false);
			y += row_height;
			y += row_height;
			rect = new RectangleF(offset_x_left + w11 + (w2 - box_width) / 2, y + (row_height - box_height) / 2, box_width, box_height);
			PrintText(graph, "Норма", offset_x_left, y, w1, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			PrintText(graph, "Следы повреждений", offset_x_left, y, w11, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			PrintText(graph, "Требуется замена", offset_x_left, y, w11, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			// Правый нижний
			y = y0;
			offset_x_left	= offset_x_left_ini + w11*3 + w2*3;
			PrintText(graph, "Рычаг подвески правый нижний", offset_x_left, y, w11, row_height * 2, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, false);
			y += row_height;
			y += row_height;
			rect = new RectangleF(offset_x_left + w11 + (w2 - box_width) / 2, y + (row_height - box_height) / 2, box_width, box_height);
			PrintText(graph, "Норма", offset_x_left, y, w1, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			PrintText(graph, "Следы повреждений", offset_x_left, y, w11, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			PrintText(graph, "Требуется замена", offset_x_left, y, w11, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			// Стабилизатор поперечной устойчивости
			// Передний
			y0 = y;
			offset_x_left	= offset_x_left_ini;
			PrintText(graph, "Стабилизатор \n передний", offset_x_left, y, w11, row_height * 2, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, false);
			y += row_height;
			y += row_height;
			rect = new RectangleF(offset_x_left + w11 + (w2 - box_width) / 2, y + (row_height - box_height) / 2, box_width, box_height);
			PrintText(graph, "Норма", offset_x_left, y, w1, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			PrintText(graph, "Следы повреждений", offset_x_left, y, w11, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			PrintText(graph, "Требуется замена", offset_x_left, y, w11, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			// Задний
			y = y0;
			offset_x_left	= offset_x_left_ini + w11 + w2;
			PrintText(graph, "Стабилизатор \n задний", offset_x_left, y, w11, row_height * 2, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, false);
			y += row_height;
			y += row_height;
			rect = new RectangleF(offset_x_left + w11 + (w2 - box_width) / 2, y + (row_height - box_height) / 2, box_width, box_height);
			PrintText(graph, "Норма", offset_x_left, y, w1, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			PrintText(graph, "Следы повреждений", offset_x_left, y, w11, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			PrintText(graph, "Требуется замена", offset_x_left, y, w11, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			// Реактивные тяги
			// Левая
			y = y0;
			offset_x_left	= offset_x_left_ini + w11*2 + w2*2;
			PrintText(graph, "Реактивная тяга \n левая", offset_x_left, y, w11, row_height * 2, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, false);
			y += row_height;
			y += row_height;
			rect = new RectangleF(offset_x_left + w11 + (w2 - box_width) / 2, y + (row_height - box_height) / 2, box_width, box_height);
			PrintText(graph, "Норма", offset_x_left, y, w1, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			PrintText(graph, "Следы повреждений", offset_x_left, y, w11, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			PrintText(graph, "Требуется замена", offset_x_left, y, w11, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			// Правая
			y = y0;
			offset_x_left	= offset_x_left_ini + w11*3 + w2*3;
			PrintText(graph, "Реактиваная тяга \n правая", offset_x_left, y, w11, row_height * 2, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, false);
			y += row_height;
			y += row_height;
			rect = new RectangleF(offset_x_left + w11 + (w2 - box_width) / 2, y + (row_height - box_height) / 2, box_width, box_height);
			PrintText(graph, "Норма", offset_x_left, y, w1, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			PrintText(graph, "Следы повреждений", offset_x_left, y, w11, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			PrintText(graph, "Требуется замена", offset_x_left, y, w11, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			// Привод передний левый
			// Передний левый
			y0 = y;
			offset_x_left	= offset_x_left_ini;
			PrintText(graph, "Привод передний левый", offset_x_left, y, w11, row_height * 2, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, false);
			y += row_height;
			y += row_height;
			rect = new RectangleF(offset_x_left + w11 + (w2 - box_width) / 2, y + (row_height - box_height) / 2, box_width, box_height);
			PrintText(graph, "Норма", offset_x_left, y, w1, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			PrintText(graph, "Следы повреждений", offset_x_left, y, w11, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			PrintText(graph, "Требуется замена", offset_x_left, y, w11, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			PrintText(graph, "Замена чехла", offset_x_left, y, w11, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			// Передний правый
			y = y0;
			offset_x_left	= offset_x_left_ini + w11*1 + w2*1;
			PrintText(graph, "Привод передний правый", offset_x_left, y, w11, row_height * 2, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, false);
			y += row_height;
			y += row_height;
			rect = new RectangleF(offset_x_left + w11 + (w2 - box_width) / 2, y + (row_height - box_height) / 2, box_width, box_height);
			PrintText(graph, "Норма", offset_x_left, y, w1, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			PrintText(graph, "Следы повреждений", offset_x_left, y, w11, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			PrintText(graph, "Требуется замена", offset_x_left, y, w11, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			PrintText(graph, "Замена чехла", offset_x_left, y, w11, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			// Задний левый
			y = y0;
			offset_x_left	= offset_x_left_ini + w11*2 + w2*2;
			PrintText(graph, "Привод задний левый", offset_x_left, y, w11, row_height * 2, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, false);
			y += row_height;
			y += row_height;
			rect = new RectangleF(offset_x_left + w11 + (w2 - box_width) / 2, y + (row_height - box_height) / 2, box_width, box_height);
			PrintText(graph, "Норма", offset_x_left, y, w1, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			PrintText(graph, "Следы повреждений", offset_x_left, y, w11, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			PrintText(graph, "Требуется замена", offset_x_left, y, w11, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			PrintText(graph, "Замена чехла", offset_x_left, y, w11, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			// Задний правый
			y = y0;
			offset_x_left	= offset_x_left_ini + w11*3 + w2*3;
			PrintText(graph, "Привод задний правый", offset_x_left, y, w11, row_height * 2, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, false);
			y += row_height;
			y += row_height;
			rect = new RectangleF(offset_x_left + w11 + (w2 - box_width) / 2, y + (row_height - box_height) / 2, box_width, box_height);
			PrintText(graph, "Норма", offset_x_left, y, w1, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			PrintText(graph, "Следы повреждений", offset_x_left, y, w11, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			PrintText(graph, "Требуется замена", offset_x_left, y, w11, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			PrintText(graph, "Замена чехла", offset_x_left, y, w11, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			// Карданный вал
			// Передний
			y0 = y;
			offset_x_left	= offset_x_left_ini;
			PrintText(graph, "Карданный вал передний", offset_x_left, y, w11, row_height * 2, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, false);
			y += row_height;
			y += row_height;
			rect = new RectangleF(offset_x_left + w11 + (w2 - box_width) / 2, y + (row_height - box_height) / 2, box_width, box_height);
			PrintText(graph, "Норма", offset_x_left, y, w1, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			PrintText(graph, "Стук/люфт", offset_x_left, y, w11, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			PrintText(graph, "Требуется ремонт", offset_x_left, y, w11, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			PrintText(graph, "Требуется замена", offset_x_left, y, w11, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			// Передний правый
			y = y0;
			offset_x_left	= offset_x_left_ini + w11*1 + w2*1;
			PrintText(graph, "Карданный вал задний", offset_x_left, y, w11, row_height * 2, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, false);
			y += row_height;
			y += row_height;
			rect = new RectangleF(offset_x_left + w11 + (w2 - box_width) / 2, y + (row_height - box_height) / 2, box_width, box_height);
			PrintText(graph, "Норма", offset_x_left, y, w1, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			PrintText(graph, "Стук/люфт", offset_x_left, y, w11, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			PrintText(graph, "Требуется ремонт", offset_x_left, y, w11, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			PrintText(graph, "Требуется замена", offset_x_left, y, w11, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;

			// Печать завершающего блока
			block_rect.Height = y - block_rect.Y + 4;
			block_rect.Width  = offset_x_left_ini + w1 + w2 * 4 + w1 + w2 + w1 + w2 * 2 + 5 + 5;
			PrintBlockSigned(graph, "Ходовая часть", block_rect, 10, font_middle_bold, brush_standart, pen_thin, false);

			return y;
		}
		#endregion

		// Основная процедура печати
		public override void  PrintPage(Graphics graph, int page)
		{
			// Для ориентации на странице
			int offset = 0;

			offset = 10;
			offset = PrintBlock(graph, offset, new DelegatePrintBlock(PrintHead), null);
			offset = PrintBlock(graph, offset, new DelegatePrintBlock(PrintFooter), null);
		}
	}
}
