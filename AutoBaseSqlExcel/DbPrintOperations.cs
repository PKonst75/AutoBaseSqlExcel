using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbPrintOperations.
	/// </summary>
	public class DbPrintOperations:DbPrint
	{
		// Инструменты для печати
		SolidBrush	brush_standart;
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
			public DIALER dialer		= DIALER.unknown;
	
			public ArrayList	works	= null;
			public ArrayList	details	= null;

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
				foreach(DtCardWork element in works)
				{
					float nv = (float)element.GetData("ТРУДОЕМКОСТЬ_КАРТОЧКА_РАБОТА");
					if(nv != 0.0F)
						count_nv += nv;
					else
						count_sp += 1;
				}

				// Список деталей в заказ-наряде
				details = new ArrayList();
				DbSqlCardDetail.SelectInArray(card, details);
			}
		}
		HeaderData	header_data = null;
		#endregion

		public DbPrintOperations(long card_number, int card_year)
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
			if(header_data.txt_warrant != "")
				PrintText(graph, "ОПЕРАЦИОННАЯ КАРТА К ЗАКАЗ-НАРЯДУ №" + header_data.txt_warrant, header_x, y, 150, 8, StringAlignment.Near, StringAlignment.Center, font_middle_bold, brush_standart, false);
			else
				PrintText(graph, "ОПЕРАЦИОННАЯ КАРТА" + header_data.txt_warrant, header_x, y, 150, 8, StringAlignment.Near, StringAlignment.Center, font_middle_bold, brush_standart, false);
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
			switch(header_data.dialer)
			{
				case HeaderData.DIALER.lada:
					txt = FileIni.GetParameter("print.ini", "#ADDRESS_BLOCK");
					break;
				case HeaderData.DIALER.chevrolet:
					txt = FileIni.GetParameter("print.ini", "#ADDRESS_BLOCK_GM");
					break;
				case HeaderData.DIALER.unknown:
					txt = FileIni.GetParameter("print.ini", "#ADDRESS_BLOCK");
					break;
				default:
					txt = FileIni.GetParameter("print.ini", "#ADDRESS_BLOCK");
					break;
			}
			// Старая версия
			//txt = FileIni.GetParameter("print.ini", "#ADDRESS_BLOCK");
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

		#region Печать блока пустых строчек под работы
		private int PrintWorkBlockHead(Graphics graph, int offset,  bool test, bool print, object o)
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

			int	col1		=	8;
			int col3		=	28;
			int col4		=	20;
			int col5		=	20;
			int col6		=	18;
			int col2		=	page_width - offset_x_left - offset_x_right - col1 - col3 - col4 - col5 - col6;
			int	rowheight	= 10;

			if(test == true || print == false)
			{	
				y += rowheight;
				return y;
			}

			// Первая строчка
			PrintTextBox(graph, "№ п/п", offset_x_left, y, col1, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "Наименование произведенной работы (комментарии)", offset_x_left + col1, y, col2, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "ФИО исполнителя", offset_x_left + col1 + col2, y, col3, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "Подпись", offset_x_left + col1 + col2 + col3 + col4 + col5, y, col6, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "Время работ", offset_x_left + col1 + col2 + col3, y,  col4 + col5, rowheight / 2, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			y += rowheight / 2;
			PrintTextBox(graph, "Начало", offset_x_left + col1 + col2 + col3, y, col4, rowheight / 2, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "Окончание", offset_x_left + col1 + col2 + col3 + col4, y, col5, rowheight / 2, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			y += rowheight / 2;
			return y;
		}
		private int PrintWorkBlock(Graphics graph, int offset,  bool test, bool print, object o)
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

			int	col1		=	8;
			int col3		=	28;
			int col4		=	20;
			int col5		=	20;
			int col6		=	18;
			int col2		=	page_width - offset_x_left - offset_x_right - col1 - col3 - col4 - col5 - col6;
			int	rowheight	= 14;
			
			if(test == true || print == false)
			{	
				return y + rowheight;
			}
			PrintBox(graph, offset_x_left, y, col1, rowheight, pen_thin);
			PrintBox(graph, offset_x_left + col1, y, col2, rowheight, pen_thin);
			PrintBox(graph, offset_x_left + col1 + col2, y, col3, rowheight, pen_thin);
			PrintBox(graph, offset_x_left + col1 + col2 + col3 + col4 + col5, y, col6, rowheight, pen_thin);
			//PrintBox(graph, offset_x_left + col1 + col2 + col3, y, col4, rowheight, pen_thin);
			//PrintBox(graph, offset_x_left + col1 + col2 + col3 + col4, y, col5, rowheight, pen_thin);
			// Вариант с объединением
			PrintTextBox(graph, "Н:", offset_x_left + col1 + col2 + col3, y, col4 + col5, rowheight / 2, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
			PrintTextBox(graph, "О:", offset_x_left + col1 + col2 + col3, y + rowheight / 2, col4  + col5, rowheight / 2, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
			
			
			y += rowheight;
			return y;
		}
		#endregion

		#region Дополнение блока работ пустыми строчками
		private int PrintWorkBlank(Graphics graph, int offset,  bool test, bool print, object o)
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


			int	col1		=	8;
			int col3		=	28;
			int col4		=	20;
			int col5		=	20;
			int col6		=	18;
			int col2		=	page_width - offset_x_left - offset_x_right - col1 - col3 - col4 - col5 - col6;
			int	rowheight	= 14;

			// Проводим измерения последующих блоков
			int h = 0;
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
				PrintBox(graph, offset_x_left, y, col1, rowheight, pen_thin);
				PrintBox(graph, offset_x_left + col1, y, col2, rowheight, pen_thin);
				PrintBox(graph, offset_x_left + col1 + col2, y, col3, rowheight, pen_thin);
				PrintBox(graph, offset_x_left + col1 + col2 + col3 + col4 + col5, y, col6, rowheight, pen_thin);
				//PrintBox(graph, offset_x_left + col1 + col2 + col3, y, col4, rowheight, pen_thin);
				//PrintBox(graph, offset_x_left + col1 + col2 + col3 + col4, y, col5, rowheight, pen_thin);
				// Вариант с объединением
				PrintTextBox(graph, "Н:", offset_x_left + col1 + col2 + col3, y, col4 + col5, rowheight / 2, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
				PrintTextBox(graph, "О:", offset_x_left + col1 + col2 + col3, y + rowheight / 2, col4 + col5, rowheight / 2, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
				
				y += rowheight;
			}
			return y;
		}
		#endregion

		#region Печать завершающего блока
		private int PrintFooter(Graphics graph, int offset,  bool test, bool print, object o)
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

			int	rowheight	= 4;
			int w1			= 70;
			int w2			= 20;
			int w3			= 30;
			int w4			= 30;
			int d1			= 5;
			int d2			= 100;
			int w5			= 100;

			y += 3;
			if(test == true || print == false)
			{	
				y += rowheight;
				y += rowheight;
				y += 3;
				y += rowheight;
				y += rowheight;
				return y;
			}

			// Первая строчка
			PrintText(graph, "Подпись мастера смены", offset_x_left, y, w5, rowheight, StringAlignment.Near, StringAlignment.Center, font_middle, brush_standart, false);
			y += rowheight;
			PrintLineHor(graph, offset_x_left + w1, y, w2, pen_thin, false);
			PrintLineHor(graph, offset_x_left + w1 + w2 + d1 , y, w3, pen_thin, false);
			PrintText(graph, "(подпись)", offset_x_left + w1, y, w2, rowheight, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
			PrintText(graph, "(ФамилияИО)", offset_x_left + w1 + w2 + d1, y, w3, rowheight, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
			y += rowheight;
			y += 3;
			PrintText(graph, "Подпись сервис-консультанта", offset_x_left, y, w5, rowheight, StringAlignment.Near, StringAlignment.Center, font_middle, brush_standart, false);
			y += rowheight;
			PrintLineHor(graph, offset_x_left + w1, y, w2, pen_thin, false);
			PrintLineHor(graph, offset_x_left + w1 + w2 + d1 , y, w3, pen_thin, false);
			PrintText(graph, "(подпись)", offset_x_left + w1, y, w2, rowheight, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
			PrintText(graph, "(ФамилияИО)", offset_x_left + w1 + w2 + d1, y, w3, rowheight, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
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
			offset = PrintBlock(graph, offset, new DelegatePrintBlock(PrintWorkBlank), null);
			offset = PrintBlock(graph, offset, new DelegatePrintBlock(PrintFooter), null);
		}

	}
}
