using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbPrintAcceptanceReportEmpty.
	/// </summary>
	public class DbPrintAcceptanceReportEmpty:DbPrint
	{
		// Инструменты для печати
		SolidBrush	brush_standart;
		Font		font_small_bold;
		Font		font_middle_bold;
		Font		font_small;
		Font		font_middle;
		Pen			pen_thin;
		Pen			pen_thin_dot;

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
			public DIALER dialer		= DIALER.unknown;

			public ArrayList	claims	= null;

			public HeaderData(DtCard card)
			{			
				dialer = DIALER.unknown;
			}
		}
		HeaderData	header_data = null;
		#endregion

		public DbPrintAcceptanceReportEmpty()
		{
			// Подготовка инструментов для печати
			brush_standart		= new SolidBrush(Color.Black);
			font_small_bold		= new Font("Arial", 8, FontStyle.Bold);
			font_small			= new Font("Arial", 8);
			font_middle_bold	= new Font("Arial", 10, FontStyle.Bold);
			font_middle			= new Font("Arial", 10);
			pen_thin			= new Pen(brush_standart, 0.3F);
			pen_thin_dot		= new Pen(brush_standart, 0.4F);
			pen_thin_dot.DashStyle = DashStyle.Dot;

			header_data = new HeaderData(null);
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
			// PrintText(graph, "К КАРТОЧКЕ №" + header_data.txt_card, header_x, y, 150, 8, StringAlignment.Near, StringAlignment.Center, font_middle_bold, brush_standart, false);
			// Данные о приемке и отдаче автомобиля
			PrintText(graph, "К КАРТОЧКЕ №_______", header_x, y, 150, 8, StringAlignment.Near, StringAlignment.Center, font_middle_bold, brush_standart, false);
			PrintText(graph, "ПРИЕМКА__________", header_x + 40, y, 150, 8, StringAlignment.Near, StringAlignment.Center, font_middle_bold, brush_standart, false);
			PrintText(graph, "ОКОНЧАНИЕ__________", header_x + 40 + 40, y, 150, 8, StringAlignment.Near, StringAlignment.Center, font_middle_bold, brush_standart, false);
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

		#region Печать блока работ, заявленных клиентом
		private int PrintClaim(Graphics graph, int offset,  bool test, bool print, object o)
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
			int row_count			= 4;
			int	col1		=	10;
			int col3		=	25;
			int col2		=	page_width - offset_x_left - offset_x_right - col1 - col3;
			int	rowheight		= 4;
			int	titlerowheight	= 7;

			if(test == true)
			{
				// При тестировании проверяем попадание на страницу еще и подвала
				y	+= title_height;	// Заголовок
				y	+= title_height;	//		таблицы
				y	+= titlerowheight;		// Первая строчка таблицы
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
				y	+= titlerowheight;		// Первая строчка таблицы
				for(int j = 0; j < row_count; j++)
				{
					y += rowheight;
				}
				return y;
			}

			// Заголовок
			text	= "РАБОТЫ, ЗАЯВЛЕННЫЕ ЗАКАЗЧИКОМ";
			PrintText(graph, text, offset_x_left, y, page_width - offset_x_right - offset_x_left, title_height, StringAlignment.Center, StringAlignment.Center, font_middle_bold, brush_standart, false);
			y += title_height;
			// Таблица под заявку клиента

			// Первая строчка
			PrintTextBox(graph, "№ п/п", offset_x_left, y, col1, titlerowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "Наименование работ", offset_x_left + col1, y, col2, titlerowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "Примерная стоимость", offset_x_left + col1 + col2, y, col3, titlerowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			y += titlerowheight;
			// Массив строк
			for(int j = 0; j < row_count; j++)
			{
				rect	= new RectangleF(offset_x_left, y, col1, rowheight);
				PrintBox(graph, rect, pen_thin);
				rect	= new RectangleF(offset_x_left + col1, y, col2, rowheight);
				PrintBox(graph, rect, pen_thin);
				rect	= new RectangleF(offset_x_left + col1 + col2, y, col3, rowheight);
				PrintBox(graph, rect, pen_thin);
				y += rowheight;
			}
			return y;
		}
		#endregion

		#region Печать блока деталей, к заявке клиента
		private int PrintClaimDetails(Graphics graph, int offset,  bool test, bool print, object o)
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
			int row_count			= 4;
			int	col1		=	10;
			int col3		=	25;
			int col2		=	page_width - offset_x_left - offset_x_right - col1 - col3;
			int	rowheight		= 4;
			int	titlerowheight	= 7;

			if(test == true)
			{
				// При тестировании проверяем попадание на страницу еще и подвала
				y	+= title_height;	// Заголовок
				y	+= titlerowheight;		// Первая строчка таблицы
				for(int j = 0; j < row_count; j++)
				{
					y += rowheight;
				}
				y -= rowheight;
				y	+= PrintFooter(graph, 0, true, false, null);	// Подвал
				return y;
			}
			if(print == false)
			{
				// При тестировании проверяем попадание на страницу еще и подвала
				y	+= title_height;	// Заголовок
				y	+= titlerowheight;		// Первая строчка таблицы
				for(int j = 0; j < row_count; j++)
				{
					y += rowheight;
				}
				y -= rowheight;
				return y;
			}

			// Заголовок
			text	= "ЗАПЧАСТИ И РАСХОДНЫЕ МАТЕРИАЛЫ К ЗАЯВКЕ";
			PrintText(graph, text, offset_x_left, y, page_width - offset_x_right - offset_x_left, title_height, StringAlignment.Center, StringAlignment.Center, font_middle_bold, brush_standart, false);
			y += title_height;
			// Таблица под заявку клиента

			// Первая строчка
			PrintTextBox(graph, "№ п/п", offset_x_left, y, col1, titlerowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "Наименование", offset_x_left + col1, y, col2, titlerowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "Примерная стоимость", offset_x_left + col1 + col2, y, col3, titlerowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			y += titlerowheight;
			// Массив строк
			for(int j = 0; j < row_count; j++)
			{
				rect	= new RectangleF(offset_x_left, y, col1, rowheight);
				PrintBox(graph, rect, pen_thin);
				rect	= new RectangleF(offset_x_left + col1, y, col2, rowheight);
				PrintBox(graph, rect, pen_thin);
				rect	= new RectangleF(offset_x_left + col1 + col2, y, col3, rowheight);
				PrintBox(graph, rect, pen_thin);
				y += rowheight;
			}
			y -= rowheight;
			return y;
		}
		#endregion

		#region Печать блока соглашений с клиентом
		private int PrintBlock5(Graphics graph, int offset,  bool test, bool print, object o)
		{
			// Вспомогательные инструменты
			string txt;
			int y		= offset;
			// Настроечные параметры
			int offset_x_left		= 10;
			int h1					= 4;

			if(test == true || print == false)
			{
				y += h1 * 5;
				y += h1;
				y += h1 + 2;
				return y;
			}

			// Заголовок
			txt	= "   С предварительной стоимостью работ, запчастей и расходных материалов ознакомлен.";
			txt	+= "\n" + "   Согласен на увеличение суммы заказ-наряда на 10% без дополнительного согласования.";
			txt	+= "\n" + "   Максимальный срок ремонта при отсутствии запасных частей составляет 45 дней. Прин наличии запасных частей ремонт будет произведен в максимально короткий срок.";
			PrintText(graph, txt, offset_x_left, y, 190, h1 * 6, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, false);
			y += h1 * 5;
			PrintText(graph, "Заказчик", offset_x_left, y, 40, h1, StringAlignment.Near, StringAlignment.Far, font_small, brush_standart, false);
			y += h1;
			PrintLineHor(graph, offset_x_left + 40, y, 60, pen_thin, false);
			PrintLineHor(graph, offset_x_left + 40 + 70, y, 60, pen_thin, false);
			PrintText(graph, "(подпись)", offset_x_left + 40, y, 60, h1, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
			PrintText(graph, "(ФамилияИО)", offset_x_left + 40 + 70, y, 60, h1, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
			y += h1 + 2;
			return y;
		}
		#endregion

		#region Печать блока дефектов
		private int PrintDefects(Graphics graph, int offset,  bool test, bool print, object o)
		{
			// Вспомогательные инструменты
			RectangleF rect;
			int y		= offset;
			string text	= "";
			// Настроечные параметры
			int offset_x_left		= 10;
			int offset_x_right		= 10;
			int page_width			= 210;
			int title_height		= 7;
			int page_height			= 297 - 10;
			int row_count			= 6;
			int	col1		=	10;
			int col2		=	10;
			int col4		=	15;
			int col5		=	15;
			int col6		=	15;
			int col7		=	20;
			int col3		=	page_width - offset_x_left - offset_x_right - col1 - col2 - col4 - col5 - col6 - col7;
			int	rowheight		= 4;
			int	titlerowheight	= 10;

			if(test == true)
			{
				// При тестировании проверяем попадание на страницу еще и подвала
				y	+= title_height;	// Заголовок
				y	+= titlerowheight;		// Первая строчка таблицы
				for(int j = 0; j < row_count; j++)
				{
					y += rowheight;
				}
				y -= rowheight;
				y	+= PrintFooter(graph, 0, true, false, null);	// Подвал
				return y;
			}
			if(print == false)
			{
				// При тестировании проверяем попадание на страницу еще и подвала
				y	+= title_height;	// Заголовок
				y	+= titlerowheight;		// Первая строчка таблицы
				for(int j = 0; j < row_count; j++)
				{
					y += rowheight;
				}
				y -= rowheight;
				return y;
			}

			// Заголовок
			text	= "ЗАЯВЛЕННЫЕ ДЕФЕКТЫ";
			PrintText(graph, text, offset_x_left, y, page_width - offset_x_right - offset_x_left, title_height, StringAlignment.Center, StringAlignment.Center, font_middle_bold, brush_standart, false);
			y += title_height;
			// Таблица под заявку клиента

			// Первая строчка
			PrintTextBox(graph, "Деф ект", offset_x_left, y, col1, titlerowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "Раб. /Дет.", offset_x_left + col1, y, col2, titlerowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "Наименование", offset_x_left + col1 + col2, y, col3, titlerowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "Подтвержд. да / нет", offset_x_left + col1 + col2 + col3, y, col4, titlerowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "Диагн. да/нет", offset_x_left + col1 + col2 + col3 + col4, y, col5, titlerowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "Гарант. да/нет /диагн.", offset_x_left + col1 + col2 + col3 + col4 + col5, y, col6, titlerowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "Примерная стоимость", offset_x_left + col1 + col2 + col3 + col4 + col5 + col6, y, col7, titlerowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			y += titlerowheight;
			// Массив строк
			for(int j = 0; j < row_count; j++)
			{
				rect	= new RectangleF(offset_x_left, y, col1, rowheight);
				PrintBox(graph, rect, pen_thin);
				rect	= new RectangleF(offset_x_left + col1, y, col2, rowheight);
				PrintBox(graph, rect, pen_thin);
				rect	= new RectangleF(offset_x_left + col1 + col2, y, col3, rowheight);
				PrintBox(graph, rect, pen_thin);
				rect	= new RectangleF(offset_x_left + col1 + col2 + col3, y, col4, rowheight);
				PrintBox(graph, rect, pen_thin);
				rect	= new RectangleF(offset_x_left + col1 + col2 + col3 + col4, y, col5, rowheight);
				PrintBox(graph, rect, pen_thin);
				rect	= new RectangleF(offset_x_left + col1 + col2 + col3 + col4 + col5, y, col6, rowheight);
				PrintBox(graph, rect, pen_thin);
				rect	= new RectangleF(offset_x_left + col1 + col2 + col3 + col4 + col5 + col6, y, col7, rowheight);
				PrintBox(graph, rect, pen_thin);
				y += rowheight;
			}
			y -= rowheight;
			return y;
		}
		#endregion

		#region Печать блока соглашений с клиентом 2
		private int PrintBlock6(Graphics graph, int offset,  bool test, bool print, object o)
		{
			// Вспомогательные инструменты
			string txt;
			int y		= offset;
			// Настроечные параметры
			int offset_x_left		= 10;
			int h1					= 4;

			if(test == true || print == false)
			{
				y += h1 * 3;
				y += h1;
				y += h1 + 2;
				return y;
			}

			// Заголовок
			txt	= "   С предварительной диагностикой заявленных дефектов согласен.";
			txt	+= "\n" + "   Согласен на увеличение суммы заказ-наряда на 10% без дополнительного согласования.";
			PrintText(graph, txt, offset_x_left, y, 190, h1 * 4, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, false);
			y += h1 * 3;
			PrintText(graph, "Заказчик", offset_x_left, y, 40, h1, StringAlignment.Near, StringAlignment.Far, font_small, brush_standart, false);
			y += h1;
			PrintLineHor(graph, offset_x_left + 40, y, 60, pen_thin, false);
			PrintLineHor(graph, offset_x_left + 40 + 70, y, 60, pen_thin, false);
			PrintText(graph, "(подпись)", offset_x_left + 40, y, 60, h1, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
			PrintText(graph, "(ФамилияИО)", offset_x_left + 40 + 70, y, 60, h1, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
			y += h1 + 2;
			return y;
		}
		#endregion


		#region Печать Завершающего блока
		private int PrintFooter(Graphics graph, int offset,  bool test, bool print, object o)
		{
			// Вспомогательные инструменты
			int y		= offset;
			string text	= "";
			string txt = "";
			RectangleF rect;
			// Настроечные параметры
			int h1					= 6;	// Высота под первую строку
			int w11					= 70;
			int w21					= 90;

			int h					= 4;
			int w1					= 20;
			int w2					= 5;
			int w3					= 30;
			int w4					= 30;
			int w5					= 40;

			int w6					= 145;
			int w7					= 40;

			int offset_x_left		= 10;
			int offset_x_right		= 10;
			int page_width			= 210;
			

			y += 3;
			if(test == true || print == false)
			{	

				y += 3;
				y += h1;
				y += h1;
				y += h;
				y += h;
				y += h;
				y += 2;

				y += 3;
				y += 3;
				y += h1;
				y += h1;
				y += h1;
				y += h1;
				return y;
			}

			// НАПРАВЛЕНИЕ НА МОЙКУ
			PrintLineHor(graph, offset_x_left, y, page_width - offset_x_left -  offset_x_right, pen_thin_dot, false);
			text	= "линия отрыва                   линия отрыва              линия отрыва";
			PrintText(graph, text, offset_x_left, y - 3, page_width - offset_x_left - offset_x_right, 6, StringAlignment.Center, StringAlignment.Center, font_small, brush_standart, false);
			y += 3;

			text	= "НАПРАВЛЕНИЕ НА МОЙКУ:";// ПО КАРТОЧКЕ: " + header_data.txt_card + "  (" + header_data.txt_workshop + ")";
			PrintText(graph, text, offset_x_left, y, page_width, h1, StringAlignment.Near, StringAlignment.Center, font_middle_bold, brush_standart, false);
			text	= "Дата и время:";//DateTime.Now.ToString();
			PrintText(graph, text, offset_x_left + 100, y, page_width - offset_x_left - offset_x_right, h1, StringAlignment.Near, StringAlignment.Center, font_middle_bold, brush_standart, false);
			y += h1;
			//text	= "АВТОМОБИЛЬ " + header_data.txt_model + " VIN: " + header_data.txt_vin + " РЕГ. ЗНАК: " + header_data.txt_sign;
			text	= "АВТОМОБИЛЬ " + "                       " + " VIN: " + "                          " + " РЕГ. ЗНАК: ";
			PrintText(graph, text, offset_x_left, y, page_width, h1, StringAlignment.Near, StringAlignment.Center, font_middle_bold, brush_standart, false);
			y += h1;

			// Подпись мойщика, выполнившего работу
			txt	= "Мойку выполнил";
			PrintText(graph, txt, offset_x_left + w6, y - h1, w7, h, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
			PrintLineHor(graph, offset_x_left + w6, y + h, w7, pen_thin, false);
			PrintText(graph, "(подпись)", offset_x_left + w6, y + h, w7, h, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
			PrintLineHor(graph, offset_x_left + w6, y + h*3, w7, pen_thin, false);
			PrintText(graph, "(ФамилияИО)", offset_x_left + w6, y + h*3, w7, h, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);

			// Заголовок
			PrintTextBox(graph, "Мойка низ", offset_x_left, y, w1, h, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
			rect = new RectangleF(offset_x_left + w1, y, w2, h);
			PrintBox(graph, rect, pen_thin);
			PrintTextBox(graph, "Мойка коврики", offset_x_left + w1 + w2, y, w3, h, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
			rect = new RectangleF(offset_x_left + w1 + w2 + w3, y, w2, h);
			PrintBox(graph, rect, pen_thin);
			PrintTextBox(graph, "Мойка ДВС (химия)", offset_x_left + w1 + w2 + w3 + w2, y, w4, h, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
			rect = new RectangleF(offset_x_left + w1 + w2 + w3 + w2 + w4, y, w2, h);
			PrintBox(graph, rect, pen_thin);
			PrintTextBox(graph, "Мойка верх (химия)", offset_x_left + w1 + w2 + w3 + w2 + w4 + w2, y, w5, h, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
			rect = new RectangleF(offset_x_left + w1 + w2 + w3 + w2 + w4 + w2 + w5, y, w2, h);
			PrintBox(graph, rect, pen_thin);

			y += h;

			PrintTextBox(graph, "Мойка техн.", offset_x_left, y, w1, h, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
			rect = new RectangleF(offset_x_left + w1, y, w2, h);
			PrintBox(graph, rect, pen_thin);
			PrintTextBox(graph, "Мойка ДВС", offset_x_left + w1 + w2, y, w3, h, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
			rect = new RectangleF(offset_x_left + w1 + w2 + w3, y, w2, h);
			PrintBox(graph, rect, pen_thin);
			PrintTextBox(graph, "Чистка стекол", offset_x_left + w1 + w2 + w3 + w2, y, w4, h, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
			rect = new RectangleF(offset_x_left + w1 + w2 + w3 + w2 + w4, y, w2, h);
			PrintBox(graph, rect, pen_thin);
			PrintTextBox(graph, "Чистка салона (пылесос)", offset_x_left + w1 + w2 + w3 + w2 + w4 + w2, y, w5, h, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
			rect = new RectangleF(offset_x_left + w1 + w2 + w3 + w2 + w4 + w2 + w5, y, w2, h);
			PrintBox(graph, rect, pen_thin);

			y += h;

			PrintTextBox(graph, "Мойка ТО", offset_x_left, y, w1, h, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
			rect = new RectangleF(offset_x_left + w1, y, w2, h);
			PrintBox(graph, rect, pen_thin);
			PrintTextBox(graph, "Мойка антикор", offset_x_left + w1 + w2, y, w3, h, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
			rect = new RectangleF(offset_x_left + w1 + w2 + w3, y, w2, h);
			PrintBox(graph, rect, pen_thin);
			PrintTextBox(graph, "Протирка кузова", offset_x_left + w1 + w2 + w3 + w2, y, w4, h, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
			rect = new RectangleF(offset_x_left + w1 + w2 + w3 + w2 + w4, y, w2, h);
			PrintBox(graph, rect, pen_thin);
			PrintTextBox(graph, "Чистка салона (влажная)", offset_x_left + w1 + w2 + w3 + w2 + w4 + w2, y, w5, h, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
			rect = new RectangleF(offset_x_left + w1 + w2 + w3 + w2 + w4 + w2 + w5, y, w2, h);
			PrintBox(graph, rect, pen_thin);

			y += h;
			y += 2;

			// ПРОПУСК НА ВЪЕЗД
			y += 3;
			PrintLineHor(graph, offset_x_left, y, page_width - offset_x_left -  offset_x_right, pen_thin_dot, false);
			text	= "линия отрыва                   линия отрыва              линия отрыва";
			PrintText(graph, text, offset_x_left, y - 3, page_width - offset_x_left - offset_x_right, 6, StringAlignment.Center, StringAlignment.Center, font_small, brush_standart, false);
			y += 3;

			text	= "ПРОПУСК НА ВЪЕЗД";// ПО КАРТОЧКЕ: " + header_data.txt_card + "  (" + header_data.txt_workshop + ")";
			PrintText(graph, text, offset_x_left, y, page_width, h1, StringAlignment.Near, StringAlignment.Center, font_middle_bold, brush_standart, false);
			text	= "Дата и время:";//DateTime.Now.ToString();
			PrintText(graph, text, offset_x_left + 100, y, page_width - offset_x_left - offset_x_right, h1, StringAlignment.Near, StringAlignment.Center, font_middle_bold, brush_standart, false);
			y += h1;
			//text	= "АВТОМОБИЛЬ " + header_data.txt_model + " VIN: " + header_data.txt_vin + " РЕГ. ЗНАК: " + header_data.txt_sign;
			text	= "АВТОМОБИЛЬ " + "                       " + " VIN: " + "                          " + " РЕГ. ЗНАК: ";
			PrintText(graph, text, offset_x_left, y, page_width, h1, StringAlignment.Near, StringAlignment.Center, font_middle_bold, brush_standart, false);
			y += h1;
			text	= "Въезд разрешил (сервис-консультант)";
			PrintText(graph, text, offset_x_left, y, w11, h1, StringAlignment.Near, StringAlignment.Far, font_middle, brush_standart, false);
			y += h1;
			PrintLineHor(graph, offset_x_left + w11, y, w21, pen_thin, false);
			text	= "(подпись)                    (ФамилияИО)";
			PrintText(graph, text, offset_x_left + w11, y, w21, h1, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
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
			
			offset = PrintBlock(graph, offset, new DelegatePrintBlock(PrintClaim), null);
			offset = PrintBlock(graph, offset, new DelegatePrintBlock(PrintClaimDetails), null);
			offset = PrintBlock(graph, offset, new DelegatePrintBlock(PrintBlock5), null);
			offset = PrintBlock(graph, offset, new DelegatePrintBlock(PrintDefects), null);
			offset = PrintBlock(graph, offset, new DelegatePrintBlock(PrintBlock6), null);
			offset = PrintFooter(graph, offset, new DelegatePrintBlock(PrintFooter), null);
		}
	}
}
