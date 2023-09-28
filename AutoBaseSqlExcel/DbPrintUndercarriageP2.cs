using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Windows.Forms;


namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbPrintUndercarriageP2.
	/// </summary>
	public class DbPrintUndercarriageP2:DbPrint
	{
		SolidBrush	brush_standart;
		SolidBrush	brush_lightgray;
		Font		font_small_bold;
		Font		font_middle_bold;
		Font		font_middle;
		Font		font_small;
		Pen			pen_thin;

		public DbPrintUndercarriageP2()
		{
			// Подготовка инструментов для печати
			brush_standart		= new SolidBrush(Color.Black);
			brush_lightgray		= new SolidBrush(Color.LightGray);
			font_small_bold		= new Font("Arial", 8, FontStyle.Bold);
			font_small			= new Font("Arial", 8);
			font_middle_bold	= new Font("Arial", 10, FontStyle.Bold);
			font_middle			= new Font("Arial", 10);
			pen_thin			= new Pen(brush_standart, 0.3F);
		}

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
			int w12			= 145;
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

			// БЛОК ОТМЕТКИ ЗАМЕНЯЕМЫХ САЙЛЕНТБЛОКОВ
			RectangleF block_rect = new RectangleF(offset_x_left_ini - 5, y - 2, 0, 0);
			y += 40;
			block_rect.Height = y - block_rect.Y + 4;
			block_rect.Width  = offset_x_left_ini + w1 + w2 * 4 + w1 + w2 + w1 + w2 * 2 + 5 + 5;
			PrintBlockSigned(graph, "Сайлентблоки требующие замену", block_rect, 10, font_middle_bold, brush_standart, pen_thin, false);

			y += row_height;
			y += row_height;
			y += row_height;
			// БЛОК ТОРМОЗНОЙ СИСТЕМЫ
			block_rect = new RectangleF(offset_x_left_ini - 5, y - 2, 0, 0);
			// Тормозные шланги
			// Передний левый
			int y0 = y;
			offset_x_left	= offset_x_left_ini;
			PrintText(graph, "Шланг тормозной передний левый", offset_x_left, y, w1, row_height * 2, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, false);
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
			// Передний правый
			y = y0;
			offset_x_left	= offset_x_left_ini + w1 + w2;
			PrintText(graph, "Шланг тормозной передний правый", offset_x_left, y, w1, row_height * 2, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, false);
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
			// Задний левый
			y = y0;
			offset_x_left	= offset_x_left_ini + w1*2 + w2*2;
			PrintText(graph, "Шланг тормозной задний левый", offset_x_left, y, w1, row_height * 2, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, false);
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
			// Задний правый
			y = y0;
			offset_x_left	= offset_x_left_ini + w1*3 + w2*3;
			PrintText(graph, "Шланг тормозной задний правый", offset_x_left, y, w1, row_height * 2, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, false);
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
			// Тормозные колодки
			// Передние
			y0 = y;
			offset_x_left	= offset_x_left_ini;
			PrintText(graph, "Колодки тормозные передние", offset_x_left, y, w11, row_height * 2, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, false);
			y += row_height;
			y += row_height;
			rect = new RectangleF(offset_x_left + w11 + (w2 - box_width) / 2, y + (row_height - box_height) / 2, box_width, box_height);
			PrintText(graph, "Норма", offset_x_left, y, w11, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			PrintText(graph, "Рекомендуется замена", offset_x_left, y, w11, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			PrintText(graph, "Требуется замена", offset_x_left, y, w11, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			// Задние
			y = y0;
			offset_x_left	= offset_x_left_ini + w11 + w2;
			PrintText(graph, "Колодки тормозные задние", offset_x_left, y, w11, row_height * 2, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, false);
			y += row_height;
			y += row_height;
			rect = new RectangleF(offset_x_left + w11 + (w2 - box_width) / 2, y + (row_height - box_height) / 2, box_width, box_height);
			PrintText(graph, "Норма", offset_x_left, y, w11, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			PrintText(graph, "Рекомендуется замена", offset_x_left, y, w11, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			PrintText(graph, "Требуется замена", offset_x_left, y, w11, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			// Ручной тормоз
			y = y0;
			offset_x_left	= offset_x_left_ini + w11*2 + w2*2;
			PrintText(graph, "Ручной тормоз", offset_x_left, y, w11, row_height * 2, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, false);
			y += row_height;
			y += row_height;
			rect = new RectangleF(offset_x_left + w11 + (w2 - box_width) / 2, y + (row_height - box_height) / 2, box_width, box_height);
			PrintText(graph, "Норма", offset_x_left, y, w11, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			PrintText(graph, "Регулировка", offset_x_left, y, w11, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			PrintText(graph, "Следы повреждений", offset_x_left, y, w11, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			
			
			// Печать завершающего блока
			block_rect.Height = y - block_rect.Y + 4;
			block_rect.Width  = offset_x_left_ini + w1 + w2 * 4 + w1 + w2 + w1 + w2 * 2 + 5 + 5;
			PrintBlockSigned(graph, "Тормозная система", block_rect, 10, font_middle_bold, brush_standart, pen_thin, false);
			y += row_height;
			y += row_height;

			// Другие замечания
			block_rect = new RectangleF(offset_x_left_ini - 5, y - 2, 0, 0);
			y += 40;
			block_rect.Height = y - block_rect.Y + 4;
			block_rect.Width  = offset_x_left_ini + w1 + w2 * 4 + w1 + w2 + w1 + w2 * 2 + 5 + 5;
			PrintBlockSigned(graph, "Другие недостатки выявленные в процессе осмотра", block_rect, 10, font_middle_bold, brush_standart, pen_thin, false);
			y += row_height;
			y += row_height;

			// БЛОК ВЫВОДОВ
			// Ручной тормоз
			y0 = y;
			offset_x_left	= offset_x_left_ini;
			PrintText(graph, "ВЫВОДЫ по результатам осмотра", offset_x_left, y, w11, row_height * 2, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, false);
			y += row_height;
			y += row_height;
			rect = new RectangleF(offset_x_left + w12 + (w2 - box_width) / 2, y + (row_height - box_height) / 2, box_width, box_height);
			PrintText(graph, "Состояние автомобиля - норма", offset_x_left, y, w12, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			PrintText(graph, "Требуется диагностика с применением технических средств", offset_x_left, y, w12, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			PrintText(graph, "Рекомендуем устранить выявленные неисправности", offset_x_left, y, w12, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			rect.Y += row_height;
			PrintText(graph, "Эксплуатация автомобиля должна быть прекращена до устранения выявленных неисправностей", offset_x_left, y, w12, row_height, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			PrintBox(graph, rect, pen_thin);
			y += row_height;
			y += row_height;
			y += row_height;

			// БЛОК ПОДПИСЕЙ
			int h1					= 4;
			int w14					= 60;
			PrintText(graph, "Осмотр проводил сервис консультант", offset_x_left, y, w14, h1, StringAlignment.Near, StringAlignment.Far, font_small, brush_standart, false);
			y += h1;
			PrintLineHor(graph, offset_x_left + w14, y, 40, pen_thin, false);
			PrintLineHor(graph, offset_x_left + w14 + 50, y, 60, pen_thin, false);
			PrintText(graph, "(подпись)", offset_x_left + w14, y, 40, h1, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
			PrintText(graph, "(ФамилияИО)", offset_x_left + w14 + 50, y, 60, h1, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
			y += h1 + 4;
			PrintText(graph, "С резултатами осмотра ознакомлен", offset_x_left, y, w14, h1, StringAlignment.Near, StringAlignment.Far, font_small, brush_standart, false);
			y += h1;
			PrintLineHor(graph, offset_x_left + w14, y, 40, pen_thin, false);
			PrintLineHor(graph, offset_x_left + w14 + 50, y, 60, pen_thin, false);
			PrintText(graph, "(подпись)", offset_x_left + w14, y, 40, h1, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
			PrintText(graph, "(ФамилияИО)", offset_x_left + w14 + 50, y, 60, h1, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
			y += h1;

			return y;
		}
		#endregion

		// Основная процедура печати
		public override void  PrintPage(Graphics graph, int page)
		{
			// Для ориентации на странице
			int offset = 0;

			offset = 10;
			offset = PrintBlock(graph, offset, new DelegatePrintBlock(PrintFooter), null);
		}
	}
}
