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
		// Инструменты для печати
		SolidBrush	brush_standart;
		Font		font_small_bold;
		Font		font_middle_bold;
		Font		font_small;
		Font		font_middle;
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
			public string txt_workshop	= "";
			public string txt_warrant	= "";
			public DIALER dialer		= DIALER.unknown;

			public ArrayList	recomendations	= null;
			public bool cashless				= false;
			public bool inner					= false;

			public HeaderData(DtCard card)
			{
				// Данные карточки
				txt_card = card.GetData("НОМЕР_КАРТОЧКА").ToString() + "/" + card.GetData("ГОД_КАРТОЧКА").ToString();
				if((long)card.GetData("НОМЕР_НАРЯД_КАРТОЧКА") != 0)
				{
					txt_warrant = card.GetData("НОМЕР_НАРЯД_КАРТОЧКА").ToString() + " от " + card.GetData("ДАТА_НАРЯД_ОТКРЫТ_КАРТОЧКА").ToString();
				}
				if((bool)card.GetData("БЕЗНАЛИЧНЫЙ_КАРТОЧКА") == true)
				{
					cashless = true;
				}
				if((bool)card.GetData("ВНУТРЕННИЙ_КАРТОЧКА") == true)
				{
					inner = true;
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

				// Подразделение
				DtWorkshop workshop =  DbSqlWorkshop.Find((long)card.GetData("ПОДРАЗДЕЛЕНИЕ_КАРТОЧКА"));
				if(workshop != null)
					txt_workshop = (string)workshop.GetData("НАИМЕНОВАНИЕ_ЦЕХ");

				// Список заявленных неисправностей/работ
				recomendations = new ArrayList();
				DbSqlCardRecomendation.SelectInArray(recomendations, (long)card.GetData("НОМЕР_КАРТОЧКА"), (int)card.GetData("ГОД_КАРТОЧКА"));
			}
		}
		HeaderData	header_data = null;
		#endregion

		public DbPrintGivingout(long card_number, int card_year)
		{
			// Подготовка инструментов для печати
			brush_standart		= new SolidBrush(Color.Black);
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
				return y + 3;	// Отсуп от заголовка
			}

			y	= offset;
			// НАЗВАНИЕ ДОКУМЕНТА
			PrintText(graph, "АКТ ВЫДАЧИ АВТОМОБИЛЯ, К ЗАКАЗ-НАРЯДУ №" + header_data.txt_warrant, header_x, y, 150, 8, StringAlignment.Near, StringAlignment.Center, font_middle_bold, brush_standart, false);
			y += 8;
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
			RectangleF rect1	= new RectangleF(header_x, offset + 8, header_data_width + header_title_width, y - (offset + 8));
			PrintRoundBox(graph, rect1, pen_thin, 4);
			PrintLineVrt(graph, header_x + header_title_width, offset + 8, y - (offset + 8), pen_thin, false);


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

		#region Печать статичного блока с галочками
		private int PrintBlock1(Graphics graph, int offset,  bool test, bool print, object o)
		{
			// Вспомогательные инструменты
			int y;
			int height_real;
			// Настроечные параметры
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

			// Блок 1
			y		= offset;
			PrintText(graph, "ДА", b1x + b1w - 2, y, 8, b1h, StringAlignment.Center, StringAlignment.Center, font_middle, brush_standart, false);
			PrintText(graph, "НЕТ", b1x + b1w + b1h + b1d - 3, y, 12, b1h, StringAlignment.Center, StringAlignment.Center, font_middle, brush_standart, false);
			y += b1h;
			PrintCheckBox(graph, "Сервисная книжка", b1x, y, b1w, b1h, b1to, 2, b1d, StringAlignment.Near, font_middle, brush_standart, pen_thin, false);
			y += b1h;
			PrintCheckBox(graph, "Гарантийный талон", b1x, y, b1w, b1h, b1to, 2, b1d, StringAlignment.Near, font_middle, brush_standart, pen_thin, false);
			y += b1h;
			PrintCheckBox(graph, "Свидетельство ТС", b1x, y, b1w, b1h, b1to, 2, b1d, StringAlignment.Near, font_middle, brush_standart, pen_thin, false);
			y += b1h;
			PrintCheckBox(graph, "Паспорт ТС", b1x, y, b1w, b1h, b1to, 2, b1d, StringAlignment.Near, font_middle, brush_standart, pen_thin, false);
			y += b1h;
			y += 2;
			y += b1h;
			height_real = y;
			// Блок 2
			y = offset;
			PrintText(graph, "ДА", b2x + b1w - 2, y, 8, b1h, StringAlignment.Center, StringAlignment.Center, font_middle, brush_standart, false);
			PrintText(graph, "НЕТ", b2x + b1w + b1h + b1d - 3, y, 12, b1h, StringAlignment.Center, StringAlignment.Center, font_middle, brush_standart, false);
			y += b1h;
			PrintCheckBox(graph, "Автомагнитола", b2x, y, b1w, b1h, b1to, 2, b1d, StringAlignment.Near, font_middle, brush_standart, pen_thin, false);
			y += b1h;
			PrintCheckBox(graph, "Панель магнитолы", b2x, y, b1w, b1h, b1to, 2, b1d, StringAlignment.Near, font_middle, brush_standart, pen_thin, false);
			y += b1h;
			PrintCheckBox(graph, "Секретки на диски", b2x, y, b1w, b1h, b1to, 2, b1d, StringAlignment.Near, font_middle, brush_standart, pen_thin, false);
			y += b1h;
			PrintCheckBox(graph, "Брелок сигнализации", b2x, y, b1w, b1h, b1to, 2, b1d, StringAlignment.Near, font_middle, brush_standart, pen_thin, false);
			y += b1h;

			// Блок 3
			y = offset;
			PrintText(graph, "Уровень топлива 0", b3x, y, 34, b1h, StringAlignment.Near, StringAlignment.Center, font_middle, brush_standart, false);
			RectangleF rect = new RectangleF(b3x + 34, y, 15, b1h);
			PrintBox(graph, rect, pen_thin);
			PrintText(graph, "1", b3x + 34 + 15, y, 8, b1h, StringAlignment.Near, StringAlignment.Center, font_middle, brush_standart, false);
			y += b1h;
			PrintText(graph, "Штатная сигнализация:", b3x, y, 42, b1h, StringAlignment.Near, StringAlignment.Center, font_middle, brush_standart, false);
			PrintCheckBox(graph, "ДА", b3x + 42, y, 10, b1h, 1, 1, 0, StringAlignment.Far, font_middle, brush_standart, pen_thin, false);
			PrintCheckBox(graph, "НЕТ", b3x + 42 + 15, y, 10, b1h, 1, 1, 0, StringAlignment.Far, font_middle, brush_standart, pen_thin, false);
			y += b1h;
			PrintText(graph, "Модель нештатной", b3x, y, 35, b1h, StringAlignment.Near, StringAlignment.Center, font_middle, brush_standart, false);
			y += b1h;
			PrintLineHor(graph, b3x + 35, y, 40, pen_thin, false);

			PrintText(graph, "Последнее ТО", b3x, y, 35, b1h, StringAlignment.Near, StringAlignment.Center, font_middle, brush_standart, false);
			y += b1h;
			PrintLineHor(graph, b3x + 35, y, 40, pen_thin, false);
			PrintText(graph, "(номер     дата      пробег)", b3x + 35, y, 40, b1h, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
			y += b1h;
			PrintText(graph, "Возврат демонтированных деталей:", b3x - 20, y, 85, b1h, StringAlignment.Near, StringAlignment.Center, font_middle, brush_standart, false);
			PrintCheckBox(graph, "ДА", b3x + 40, y, 10, b1h, 1, 1, 0, StringAlignment.Far, font_middle, brush_standart, pen_thin, false);
			PrintCheckBox(graph, "НЕТ", b3x + 40 + 15, y, 10, b1h, 1, 1, 0, StringAlignment.Far, font_middle, brush_standart, pen_thin, false);
			y += b1h;

			return height_real;
		}
		#endregion

		#region Печать блока чистовой заявки
		private int PrintClaimBlockHead(Graphics graph, int offset,  bool test, bool print, object o)
		{
			// Вспомогательные инструменты
			RectangleF rect;
			int y		= offset;
			string text	= "";
			// Настроечные параметры
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

			// Заголовок
			text	= "Выполнили работы согласованные с заказчиком";
			PrintText(graph, text, offset_x_left, y, page_width - offset_x_right - offset_x_left, title_height, StringAlignment.Near, StringAlignment.Center, font_middle_bold, brush_standart, false);
			y += title_height;
			// Таблица под заявку клиента

			// Первая строчка
			PrintTextBox(graph, "1", offset_x_left, y, col1, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "Выполнили работы согласно заказ-наряду №" + header_data.txt_warrant, offset_x_left + col1, y, col2, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
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
			// Вспомогательные инструменты
			RectangleF rect;
			int y		= offset;
			string text	= "";
			// Настроечные параметры
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
			txt		= (string)claim.GetData("НАИМЕНОВАНИЕ_ЗАЯВКА");
			txt1	= claim.GetData("ПОЗИЦИЯ").ToString();
			// Массив строк
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

		#region Дополнение блока чистовой заявки пустыми строчками
		private int PrintClaimBlank(Graphics graph, int offset,  bool test, bool print, object o)
		{
			// Вспомогательные инструменты
			RectangleF rect;
			int y		= offset;
			string text	= "";
			// Настроечные параметры
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

			// Проводим измерения последующих блоков
			int h = 0;
	//		h += this.PrintClaimAgreement(graph, 0, false, false, null);	// Работы согласованные с заказчиком
			h += this.PrintControl(graph, 0, false, false, null);		// Показания приборов
			h += PrintFooter(graph, 0, false, false, null);				// Подвал

			// Первичное тестирование типа печати
			int max_y = 0;
			if(y + h > page_max_y)
			{
				// Окончание не влезет
				// Добиваем страницу до конца пустыми строчками
				max_y = page_max_y;
			}
			else
			{
				// Добиваем сколько можно, до влезания окончания
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

			// Массив строк
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


		#region Печать блока рекомендаций
		private int PrintRecomendationBlockHead(Graphics graph, int offset,  bool test, bool print, object o)
		{
			// Вспомогательные инструменты
			RectangleF rect;
			int y		= offset;
			string text	= "";
			// Настроечные параметры
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

			// Заголовок
			text	= "РЕКОМЕНДАЦИИ";
			PrintText(graph, text, offset_x_left, y, page_width - offset_x_right - offset_x_left, title_height, StringAlignment.Center, StringAlignment.Center, font_middle_bold, brush_standart, false);
			y += title_height;
			// Таблица под заявку клиента

			

			// Первая строчка
			PrintTextBox(graph, "№ п/п", offset_x_left, y, col1, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "Рекомендации заказчику", offset_x_left + col1, y, col2, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			y += rowheight;

			return y;
		}
		private int PrintRecomendationBlock(Graphics graph, int offset,  bool test, bool print, object o)
		{
			// Вспомогательные инструменты
			RectangleF rect;
			int y		= offset;
			string text	= "";
			// Настроечные параметры
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
			txt		= (string)recomendation.GetData("РЕКОМЕНДАЦИЯ");
			txt1	= recomendation.GetData("НОМЕР_РЕКОМЕНДАЦИЯ").ToString();
			// Массив строк
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

		#region Печать блока показаний панели приборов
		private int PrintControl(Graphics graph, int offset,  bool test, bool print, object o)
		{
			// Вспомогательные инструменты
			RectangleF rect;
			int y		= offset;
			string text	= "";
			// Настроечные параметры
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
				// При тестировании проверяем попадание на страницу еще и подвала
				y	+= title_height;	// Заголовок
				for(int j = 0; j < row_count; j++)
				{
					y += rowheight;
				}
				y	+= PrintFooter(graph, 0, true, false, null);	// Подвал
				return y;
			}
			if(print == false)
			{
				// При тестировании проверяем попадание на страницу еще и подвала
				y	+= title_height;	// Заголовок
				for(int j = 0; j < row_count; j++)
				{
					y += rowheight;
				}
				return y;
			}

			// Заголовок
			text	= "ПОКАЗАНИЯ ИНДИКАТОРОВ ПАНЕЛИ ПРИБОРОВ / ОШИБКИ КОНТРОЛЛЕРА";
			PrintText(graph, text, offset_x_left, y, page_width - offset_x_right - offset_x_left, title_height, StringAlignment.Center, StringAlignment.Center, font_middle_bold, brush_standart, false);
			y += title_height;
			// Таблица
			// Массив строк
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


		#region Печать Завершающего блока
		private int PrintFooter(Graphics graph, int offset,  bool test, bool print, object o)
		{
			// Вспомогательные инструменты
			int y		= offset;
			string text	= "";
			// Настроечные параметры
			int h1					= 6;	// Высота под первую строку
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

			// Квадрат для отметок кассы
			RectangleF rect = new RectangleF(w6, y + 6 + h1, w7, h1 * 4);
			text = "Наличный";
			if (header_data.inner == true)
				text = "Внутренний";
			if (header_data.cashless == true)
				text = "Безаналичный";
			PrintTextNoBox(graph, text, w6, y + 6 + h1, w7, 4, 2, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintRoundBox(graph, rect, pen_thin, 5);

			// ПРОПУСК НА ВЪЕЗД
			y += 3;
			PrintLineHor(graph, offset_x_left, y, page_width - offset_x_left -  offset_x_right, pen_thin, false);
			text	= "линия отрыва                   линия отрыва              линия отрыва";
			PrintText(graph, text, offset_x_left, y - 3, page_width - offset_x_left - offset_x_right, 6, StringAlignment.Center, StringAlignment.Center, font_small, brush_standart, false);
			y += 3;

			text	= "ПРОПУСК НА ВЫЕЗД ПО КАРТОЧКЕ: " + header_data.txt_card + "  (" + header_data.txt_workshop + ")";
			PrintText(graph, text, offset_x_left, y, page_width, h1, StringAlignment.Near, StringAlignment.Center, font_middle_bold, brush_standart, false);
			text	= DateTime.Now.ToString();
			PrintText(graph, text, offset_x_left, y, page_width - offset_x_left - offset_x_right, h1, StringAlignment.Far, StringAlignment.Center, font_middle_bold, brush_standart, false);
			y += h1;
			text	= "ЗАКАЗ-НАРЯД №: " + header_data.txt_warrant;
			PrintText(graph, text, offset_x_left, y, page_width, h1, StringAlignment.Near, StringAlignment.Center, font_middle_bold, brush_standart, false);
			y += h1;
			text	= "АВТОМОБИЛЬ " + header_data.txt_model + " VIN: " + header_data.txt_vin + " РЕГ. ЗНАК: " + header_data.txt_sign;
			PrintText(graph, text, offset_x_left, y, page_width, h1, StringAlignment.Near, StringAlignment.Center, font_middle_bold, brush_standart, false);
			y += h1;
			text	= "Cервис-консультант";
			PrintText(graph, text, offset_x_left, y, w1, h1, StringAlignment.Near, StringAlignment.Far, font_middle, brush_standart, false);
			y += h1;
			PrintLineHor(graph, offset_x_left + w1, y, w2, pen_thin, false);
			PrintLineHor(graph, offset_x_left + w1 + w2 + d1, y, w3, pen_thin, false);
			text	= "(подпись)";
			PrintText(graph, text, offset_x_left + w1, y, w2, h1, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
			text	= "(ФамилияИО)";
			PrintText(graph, text, offset_x_left + w1 + w2 + d1, y, w3, h1, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
			y += h1;
			return y;
		}
		#endregion

		// Основная процедура печати
		public override void  PrintPage(Graphics graph, int page)
		{
			// Для ориентации на странице
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
					element.SetData("НОМЕР_РЕКОМЕНДАЦИЯ", counter);
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
