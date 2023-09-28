using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Вторая сторона листа заявки клиента.
	/// </summary>
	public class DbPrintClaimP2:DbPrint
	{
		// Инструменты для печати
		SolidBrush	brush_standart;
		Font		font_small_bold;
		Font		font_middle_bold;
		Font		font_small;
		Pen			pen_thin;

		public DbPrintClaimP2()
		{
			// Подготовка инструментов для печати
			brush_standart		= new SolidBrush(Color.Black);
			font_small_bold		= new Font("Arial", 8, FontStyle.Bold);
			font_small			= new Font("Arial", 8);
			font_middle_bold	= new Font("Arial", 10, FontStyle.Bold);
			pen_thin			= new Pen(brush_standart, 0.3F);
		}

		#region Печать первую часть тела заявки
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
			int block_height		= (page_max_y - page_min_y) / 2 - 5;

			// Заголовок
			text	= "АКТ ПРОБНОЙ ПОЕЗДКИ ПРИ ПРИЕМЕ В РЕМОНТ";
			PrintText(graph, text, offset_x_left, y, page_width - offset_x_right - offset_x_left, title_height, StringAlignment.Center, StringAlignment.Center, font_middle_bold, brush_standart, false);
			y += title_height;
			// Таблица под заявку клиента

			int	col1		=	10;
			int col2		=	page_width - offset_x_left - offset_x_right - col1;
			int	rowheight	= 8;

			// Массив строк
			int footer_height = PrintBlockFooter(graph, 0, true, false, null);	// Замеряем размер подвала
			int last_valid_y = block_height - footer_height - 3;
			while(y < last_valid_y)
			{
				rect	= new RectangleF(offset_x_left, y, col1, rowheight);
				PrintBox(graph, rect, pen_thin);
				rect	= new RectangleF(offset_x_left + col1, y, col2, rowheight);
				PrintBox(graph, rect, pen_thin);
				y += rowheight;
			}
			y = PrintBlockFooter(graph, y, false, true, null);
			y += 3;
			return y;
		}
		#endregion
		#region Печать вторую часть тела заявки
		private int PrintBlock2(Graphics graph, int offset,  bool test, bool print, object o)
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
			int block_height		= (page_max_y - page_min_y) / 2 - 5;

			// Заголовок
			text	= "АКТ ПРОБНОЙ ПОЕЗДКИ ПОСЛЕ ВЫПОЛНЕНИЯ РЕМОНТА";
			PrintText(graph, text, offset_x_left, y, page_width - offset_x_right - offset_x_left, title_height, StringAlignment.Center, StringAlignment.Center, font_middle_bold, brush_standart, false);
			y += title_height;
			// Таблица под заявку клиента

			int	col1		=	10;
			int col2		=	page_width - offset_x_left - offset_x_right - col1;
			int	rowheight	= 8;

			// Массив строк
			int footer_height = PrintBlockFooter(graph, 0, true, false, null);	// Замеряем размер подвала
			int last_valid_y = page_max_y - footer_height - 10;
			while(y < last_valid_y)
			{
				rect	= new RectangleF(offset_x_left, y, col1, rowheight);
				PrintBox(graph, rect, pen_thin);
				rect	= new RectangleF(offset_x_left + col1, y, col2, rowheight);
				PrintBox(graph, rect, pen_thin);
				y += rowheight;
			}
			y = PrintBlockFooter(graph, y, false, true, null);
			return y;
		}
		#endregion
		#region Печать подвала блока
		private int PrintBlockFooter(Graphics graph, int offset,  bool test, bool print, object o)
		{
			// Вспомогательные инструменты
			int y;
			string text	= "";
			// Настроечные параметры
			int offset_x_left		= 10;
			int height1				= 8;
			int pos1				= 0;
			int width1				= 60;
			int length1				= 40;
			int length2				= 60;
			int offset_y_1			= 0;
			int offset_y_2			= 0;

			if(test == true || print == false)
			{
				y = offset + offset_y_1;
				y += height1 + offset_y_2;
				y += height1;
				y += 3;
				y += height1;
				y += 3;
				return y;
			}

			// Заголовок
			y		= offset + offset_y_1;
			text	= "Дата и время пробной поездки";
			PrintText(graph, text, offset_x_left + pos1, y, width1, height1, StringAlignment.Near, StringAlignment.Far, font_middle_bold, brush_standart, false);
			PrintLineHor(graph, offset_x_left + pos1 + width1, y + height1, length1, pen_thin , false);
			y += height1 + offset_y_2;
			text	= "Заказчик";
			PrintText(graph, text, offset_x_left + pos1, y, width1, height1, StringAlignment.Near, StringAlignment.Far, font_middle_bold, brush_standart, false);
			PrintLineHor(graph, offset_x_left + pos1 + width1, y + height1, length1, pen_thin , false);
			PrintLineHor(graph, offset_x_left + pos1 + width1 + length1 + 5, y + height1, length2, pen_thin , false);
			y += height1;
			text	= "(подпись)";
			PrintText(graph, text, offset_x_left + pos1 + width1, y, length1, height1, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
			text	= "(ФамилияИО)";
			PrintText(graph, text, offset_x_left + pos1 + width1 + length1 + 5, y, length2, height1, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
			y += 3;
			text	= "Сервис-консультант";
			PrintText(graph, text, offset_x_left + pos1, y, width1, height1, StringAlignment.Near, StringAlignment.Far, font_middle_bold, brush_standart, false);
			PrintLineHor(graph, offset_x_left + pos1 + width1, y + height1, length1, pen_thin , false);
			PrintLineHor(graph, offset_x_left + pos1 + width1 + length1 + 5, y + height1, length2, pen_thin , false);
			y += height1;
			text	= "(подпись)";
			PrintText(graph, text, offset_x_left + pos1 + width1, y, length1, height1, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
			text	= "(ФамилияИО)";
			PrintText(graph, text, offset_x_left + pos1 + width1 + length1 + 5, y, length2, height1, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
			y += 3;
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
		}
	}
}
