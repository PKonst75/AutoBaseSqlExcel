using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbPrintAcceptanceReportP2.
	/// </summary>
	public class DbPrintAcceptanceReportP2:DbPrint
	{
		SolidBrush	brush_standart;
		Font		font_small_bold;
		Font		font_middle_bold;
		Font		font_small;
		Font		font_middle;
		Pen			pen_thin;

		ArrayList	array_1;
		string		txt_service_name = "";

		public DbPrintAcceptanceReportP2()
		{
			// Подготовка инструментов для печати
			brush_standart		= new SolidBrush(Color.Black);
			font_small_bold		= new Font("Arial", 8, FontStyle.Bold);
			font_small			= new Font("Arial", 8);
			font_middle_bold	= new Font("Arial", 10, FontStyle.Bold);
			font_middle			= new Font("Arial", 10);
			pen_thin			= new Pen(brush_standart, 0.3F);

			// Для первого блока
			array_1	= new ArrayList();
			array_1.Add("Подсветка приборов");
			array_1.Add("Рулевое управление");
			array_1.Add("Стояночный тормоз");
			array_1.Add("Шумы при нажатии педалей");
			array_1.Add("Уровни технических жидкостей");
			array_1.Add("Щетки стеклоочистителя");
			array_1.Add("Приводные ремни");
			array_1.Add("Явные негермитичности");
			array_1.Add("Стекла");
			array_1.Add("Задние фонари / фары");
			array_1.Add("Амортизаторы");
			array_1.Add("Остаточная высота протектора");
			array_1.Add("Система выпуска ОГ");
			array_1.Add("Зеркала заднего вида");
			array_1.Add("Прикуриватель / пепельницы");
			array_1.Add("Магнитола");
			array_1.Add("Состояние обивок салона");


			txt_service_name	= FileIni.GetParameter("print.ini", "#SERVICE_BLOCK");
		}

		#region Печать блока внутренней и внешней проверки автомобиля
		private int PrintBlock1(Graphics graph, int offset,  bool test, bool print, object o)
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
			int row_count			= 7;
			int	col1		=	60;
			int col3		=	25;
			int col2		=	25;
			int	rowheight		= 4;
			int	titlerowheight	= 7;

			if(test == true)
			{
				// При тестировании проверяем попадание на страницу еще и подвала
				y	+= title_height;	// Заголовок
				y	+= titlerowheight;		// Первая строчка таблицы
				foreach(string txt in array_1)
				{
					y += rowheight;
				}
				return y;
			}
			if(print == false)
			{
				// При тестировании проверяем попадание на страницу еще и подвала
				y	+= title_height;	// Заголовок
				y	+= titlerowheight;		// Первая строчка таблицы
				foreach(string txt in array_1)
				{
					y += rowheight;
				}
				return y;
			}

			// Заголовок
			text	= "Прием автомобиля в ремонт";
			PrintText(graph, text, offset_x_left, y, page_width - offset_x_right - offset_x_left, title_height, StringAlignment.Near, StringAlignment.Center, font_middle_bold, brush_standart, false);
			y += title_height;
			
			// Таблица под заявку клиента
			// Первая строчка
			PrintTextBox(graph, "Внутренняя проверка", offset_x_left, y, col1, titlerowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "Норма", offset_x_left + col1, y, col2, titlerowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "Отклонение", offset_x_left + col1 + col2, y, col3, titlerowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			y += titlerowheight;
			// Массив строк
			foreach(string txt in array_1)
			{
				PrintTextBox(graph, txt, offset_x_left, y, col1, rowheight, 1, StringAlignment.Near, StringAlignment.Center, font_middle, brush_standart, pen_thin, false);
				rect	= new RectangleF(offset_x_left + col1, y, col2, rowheight);
				PrintBox(graph, rect, pen_thin);
				rect	= new RectangleF(offset_x_left + col1 + col2, y, col3, rowheight);
				PrintBox(graph, rect, pen_thin);
				y += rowheight;
			}

			// Картинка машинки
			PrintImage(graph, "car_schema.bmp", offset_x_left + col1 + col2 + col3 + 10, offset + 2, 60, 80, false);
			return y;
		}
		#endregion

		#region Печать блока значков повреждений автомобиля
		private int PrintBlock2(Graphics graph, int offset,  bool test, bool print, object o)
		{
			// Вспомогательные инструменты
			int y		= offset;
			// Настроечные параметры
			int offset_x_left		= 10;

			y += 2;
			if(test == true || print == false)
			{
				y += 5;
				y += 5;
				return y;
			}

			// Заголовок
			PrintLeftCheckBoxX(graph, "- удар камня", offset_x_left, y, 30, 5, 1, 1, 0, StringAlignment.Near, font_middle, brush_standart, pen_thin, false);
			PrintLeftCheckBoxD(graph, "- царапины", offset_x_left + 30, y, 30, 5, 1, 1, 0, StringAlignment.Near, font_middle, brush_standart, pen_thin, false);
			PrintLeftCheckBoxO(graph, "- вмятины", offset_x_left + 30 + 30, y, 30, 5, 1, 1, 0, StringAlignment.Near, font_middle, brush_standart, pen_thin, false);
			PrintLeftCheckBox(graph, "- повреждения кузова", offset_x_left + 30 + 30 + 30, y, 45, 5, 1, 1, 0, StringAlignment.Near, font_middle, brush_standart, pen_thin, false);
			PrintLeftCheckDiamond(graph, "- трещины", offset_x_left + 30 + 30 + 30 + 45, y, 30, 5, 1, 1, 0, StringAlignment.Near, font_middle, brush_standart, pen_thin, false);
			y += 5;
			PrintText(graph, "Состояние автомобиля:", offset_x_left, y, 45, 5, StringAlignment.Near, StringAlignment.Center, font_middle, brush_standart, false);
			PrintLeftCheckBox(graph, " чистый", offset_x_left + 45, y, 30, 5, 1, 1, 0, StringAlignment.Near, font_middle, brush_standart, pen_thin, false);
			PrintLeftCheckBox(graph, " грязый", offset_x_left + 45 + 30, y, 30, 5, 1, 1, 0, StringAlignment.Near, font_middle, brush_standart, pen_thin, false);
			PrintLeftCheckBox(graph, " очень грязный", offset_x_left + 45 + 30 + 30, y, 30, 5, 1, 1, 0, StringAlignment.Near, font_middle, brush_standart, pen_thin, false);
			y += 5;
			return y;
		}
		#endregion

		#region Печать блока описаний повреждений автомобиля - версия 0
		/*
		private int PrintBlock3(Graphics graph, int offset,  bool test, bool print, object o)
		{
			// Вспомогательные инструменты
			int y		= offset;
			// Настроечные параметры
			int offset_x_left		= 10;
			int row_count			= 10;

			y += 2;
			if(test == true || print == false)
			{
				y += 8;
				for(int i = 0; i < row_count; i++)
				{
					y += 5;
				}
				return y;
			}

			// Заголовок
			PrintText(graph, "Описание повреждений автомобиля:", offset_x_left, y, 120, 8, StringAlignment.Near, StringAlignment.Center, font_middle_bold, brush_standart, false);
			y += 8;
			for(int i = 0; i < row_count; i++)
			{
				PrintLineHor(graph, offset_x_left, y, 190, pen_thin, false);
				y += 5;
			}
			return y;
		}
		*/
		#endregion

		#region Печать блока описаний повреждений автомобиля
		private int PrintBlock3(Graphics graph, int offset,  bool test, bool print, object o)
		{
			// Вспомогательные инструменты
			int y		= offset;
			// Настроечные параметры
			int offset_x_left		= 10;
			int row_count			= 6;

			y += 2;
			if(test == true || print == false)
			{
				y += 8;
				for(int i = 0; i < row_count; i++)
				{
					y += 5;
				}
				return y;
			}

			// Заголовок
			PrintText(graph, "Описание повреждений автомобиля:", offset_x_left, y, 120, 8, StringAlignment.Near, StringAlignment.Center, font_middle_bold, brush_standart, false);
			y += 8;
			for(int i = 0; i < row_count; i++)
			{
				PrintLineHor(graph, offset_x_left, y, 190, pen_thin, false);
				y += 5;
			}
			return y;
		}
		#endregion

		#region Печать блока оговоренных сумм и направлений на мойку - версия 0
		/*
		private int PrintBlock4(Graphics graph, int offset,  bool test, bool print, object o)
		{
			// Вспомогательные инструменты
			int y		= offset;
			RectangleF rect;
			// Настроечные параметры
			int offset_x_left		= 10;
			int h					= 5;
			int w1					= 40;
			int w2					= 10;
			int w3					= 50;
			int w4					= 20;
			int x2					= 120;
			

			y += 2;
			if(test == true || print == false)
			{
				y += h;
				y += h;
				y += h;
				return y;
			}

			// Заголовок
			PrintTextBox(graph, "Мойка автомобиля", offset_x_left, y, w1, h, 1, StringAlignment.Near, StringAlignment.Center, font_middle, brush_standart, pen_thin, false);
			rect = new RectangleF(offset_x_left + w1, y, w2, h);
			PrintBox(graph, rect, pen_thin);
			PrintTextBox(graph, "Чистка салона", offset_x_left + w1 + w2, y, w1, h, 1, StringAlignment.Near, StringAlignment.Center, font_middle, brush_standart, pen_thin, false);
			rect = new RectangleF(offset_x_left + w1 + w1 + w2, y, w2, h);
			PrintBox(graph, rect, pen_thin);

			PrintTextBox(graph, "Вид оплаты", x2, y, w3, h, 1, StringAlignment.Near, StringAlignment.Center, font_middle, brush_standart, pen_thin, false);
			rect = new RectangleF(x2 + w3, y, w4, h);
			PrintBox(graph, rect, pen_thin);

			y += h;

			PrintTextBox(graph, "Мойка двигателя", offset_x_left, y, w1, h, 1, StringAlignment.Near, StringAlignment.Center, font_middle, brush_standart, pen_thin, false);
			rect = new RectangleF(offset_x_left + w1, y, w2, h);
			PrintBox(graph, rect, pen_thin);
			PrintTextBox(graph, "Чистка дисков", offset_x_left + w1 + w2, y, w1, h, 1, StringAlignment.Near, StringAlignment.Center, font_middle, brush_standart, pen_thin, false);
			rect = new RectangleF(offset_x_left + w1 + w1 + w2, y, w2, h);
			PrintBox(graph, rect, pen_thin);

			PrintTextBox(graph, "Предварительная сумма работ", x2, y, w3, h, 1, StringAlignment.Near, StringAlignment.Center, font_middle, brush_standart, pen_thin, false);
			rect = new RectangleF(x2 + w3, y, w4, h);
			PrintBox(graph, rect, pen_thin);

			y += h;

			PrintTextBox(graph, "Сверх оговоренная сумма", x2, y, w3, h, 1, StringAlignment.Near, StringAlignment.Center, font_middle, brush_standart, pen_thin, false);
			rect = new RectangleF(x2 + w3, y, w4, h);
			PrintBox(graph, rect, pen_thin);

			y += h;
			return y;
		}
		*/
		#endregion

		#region Печать блока оговоренных сумм и направлений на мойку
		private int PrintBlock4(Graphics graph, int offset,  bool test, bool print, object o)
		{
			// Вспомогательные инструменты
			int y		= offset;
			string txt	= "";
			RectangleF rect;
			// Настроечные параметры
			int offset_x_left		= 10;
			int h					= 4;
			int w1					= 20;
			int w2					= 5;
			int w3					= 30;
			int w4					= 30;
			int w5					= 40;

			int w6					= 145;
			int w7					= 40;

			int x2					= 120;
			

			y += 2;
			if(test == true || print == false)
			{
				y += h;
				y += h;
				y += h;
				y += 2;
				return y;
			}

			// Подпись мойщика, выполнившего работу
			// Подпись мойщика, выполнившего работу
			txt	= "Мойку выполнил";
			PrintText(graph, txt, offset_x_left + w6, y - 6, w7, h, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
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

			y += 2;
			if(test == true || print == false)
			{
				y += h1 * 3;
				y += h1;
				y += h1;
				return y;
			}

			// Заголовок
			//txt	= "   В случае проведения ремонта не в сервисном центре автосалона \"АВТО-1\" претензии по диагностике приниматься не будут.";
			txt	= "   В случае проведения ремонта не в сервисном центре автосалона " + txt_service_name + " претензии по диагностике приниматься не будут.";
			//txt	+= "\n" + "   В случае простоя автомобиля на территории сервисного центра автосалона \"АВТО-1\" по вине заказчика (в ожидании запасных частей, предоставляемых заказчиком, в ожидании решения заказчика о дальнейшем проведении ремонта, в ожидании оплаты и т.д.) ЗАКЗАЧИК оплачивает стянку автомобиля автомобиля на территории сервисного центра из расчета 100 рублей сутки.";
			txt	+= "\n" + "   В случае простоя автомобиля на территории сервисного центра автосалона " + txt_service_name + " по вине заказчика (в ожидании запасных частей, предоставляемых заказчиком, в ожидании решения заказчика о дальнейшем проведении ремонта, в ожидании оплаты и т.д.) ЗАКЗАЧИК оплачивает стянку автомобиля автомобиля на территории сервисного центра из расчета 100 рублей сутки.";
			PrintText(graph, txt, offset_x_left, y, 190, h1 * 4, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, false);
			y += h1 * 3;
			txt	= "   За оставленные в автомобиле вещи автосалон ответственности не несет.";
			PrintText(graph, txt, offset_x_left, y, 190, h1 * 3, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			y += h1;
			txt	= "   За скрытые дефекты загрязненного кузова, ответственность на автосалон не возлагается.";
			PrintText(graph, txt, offset_x_left, y, 190, h1 * 3, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, false);
			y += h1;
			return y;
		}
		#endregion

		#region Печать блока подписей
		private int PrintBlock6(Graphics graph, int offset,  bool test, bool print, object o)
		{
			// Вспомогательные инструменты
			string txt;
			int y		= offset;
			// Настроечные параметры
			int offset_x_left		= 10;
			int h1					= 4;

			y += 6;
			if(test == true || print == false)
			{
				y += h1;
				return y;
			}

			// Заголовок
			txt	= "Дата и время приема автомобиля";
			PrintText(graph, txt, offset_x_left, y, 60, h1, StringAlignment.Near, StringAlignment.Far, font_small, brush_standart, false);
			y += h1;
			PrintLineHor(graph, offset_x_left + 60, y, 60, pen_thin, false);
			y += h1 + 4;
			PrintText(graph, "Заказчик", offset_x_left, y, 40, h1, StringAlignment.Near, StringAlignment.Far, font_small, brush_standart, false);
			y += h1;
			PrintLineHor(graph, offset_x_left + 40, y, 60, pen_thin, false);
			PrintLineHor(graph, offset_x_left + 40 + 70, y, 60, pen_thin, false);
			PrintText(graph, "(подпись)", offset_x_left + 40, y, 60, h1, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
			PrintText(graph, "(ФамилияИО)", offset_x_left + 40 + 70, y, 60, h1, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
			y += h1 + 4;
			PrintText(graph, "Сервис-консультант", offset_x_left, y, 40, h1, StringAlignment.Near, StringAlignment.Far, font_small, brush_standart, false);
			y += h1;
			PrintLineHor(graph, offset_x_left + 40, y, 60, pen_thin, false);
			PrintLineHor(graph, offset_x_left + 40 + 70, y, 60, pen_thin, false);
			PrintText(graph, "(подпись)", offset_x_left + 40, y, 60, h1, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
			PrintText(graph, "(ФамилияИО)", offset_x_left + 40 + 70, y, 60, h1, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
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
			offset = PrintBlock(graph, offset, new DelegatePrintBlock(PrintBlock1), null);
			offset = PrintBlock(graph, offset, new DelegatePrintBlock(PrintBlock2), null);
			offset = PrintBlock(graph, offset, new DelegatePrintBlock(PrintBlock3), null);
			offset = PrintBlock(graph, offset, new DelegatePrintBlock(PrintBlock4), null);
			offset = PrintBlock(graph, offset, new DelegatePrintBlock(PrintBlock5), null);
			offset = PrintBlock(graph, offset, new DelegatePrintBlock(PrintBlock6), null);
		}
	}
}
