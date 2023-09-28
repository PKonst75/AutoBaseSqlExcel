using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbPrintOptionsList.
	/// </summary>
	public class DbPrintOptionsList:DbPrint
	{
		// Инструменты для печати
		SolidBrush	brush_standart;
		SolidBrush	brush_lightgray;
		Font		font_small_bold;
		Font		font_middle_bold;
		Font		font_middle;
		Font		font_small;
		Font		font_small_cur;
		Pen			pen_thin;

		
		protected class HeaderData
		{
			public ArrayList	options		= null;
			public string		name		= "";
			
			public HeaderData(string group_name, ArrayList group_options)
			{
				if (group_options.Count > 0)
					options = group_options;
				name = group_name;
			}
		}
		ArrayList	header_datas = null;

		public DbPrintOptionsList(long code_auto)
		{
			// Подготовка инструментов для печати
			brush_standart		= new SolidBrush(Color.Black);
			brush_lightgray		= new SolidBrush(Color.LightGray);
			font_small_bold		= new Font("Arial", 8, FontStyle.Bold);
			font_small			= new Font("Arial", 6);
			font_small_cur		= new Font("Arial", 8, FontStyle.Italic);
			font_middle_bold	= new Font("Arial", 10, FontStyle.Bold);
			font_middle			= new Font("Arial", 10);
			pen_thin			= new Pen(brush_standart, 0.3F);

			if(code_auto != 0)
			{
				// Найден автомобиль. Теперь ищем его комплектацию, если есть.
				DtAuto auto = DbSqlAuto.Find(code_auto);
				long code_model	= (long)auto.GetData("ССЫЛКА_КОД_АВТОМОБИЛЬ_МОДЕЛЬ");
				long code_variant = (long)auto.GetData("ССЫЛКА_КОД_АВТОМОБИЛЬ_ИСПОЛНЕНИЕ");

				ArrayList groups = new ArrayList();
				DbSqlAutoOptions.SelectInArrayGroup(groups);

				header_datas = new ArrayList();
				foreach (DtAutoOptionGroup group in groups)
				{
					ArrayList array = new ArrayList();
					DbSqlAutoOptions.SelectInArrayOptionGroupCpmplect(array, group.code, code_model, code_variant);
					HeaderData header_data = null;
					header_data = new HeaderData(group.name, array);
					header_datas.Add(header_data);
				}
			}
		}

		// Основная процедура печати
		public override void  PrintPage(Graphics graph, int page)
		{
			// Для ориентации на странице
			int offset = 0;

			offset = 10;
			offset = PrintBlock(graph, offset, new DelegatePrintBlock(PrintList), null);
		}

	
		private int PrintList(Graphics graph, int offset,  bool test, bool print, object o)
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

			int	col1		=	200;
			int col3		=	15;
			int col4		=	20;
			int col2		=	page_width - offset_x_left - offset_x_right - col1 - col3 - col4;
			int	rowheight	= 6;

			foreach(HeaderData data in header_datas)
			{
				PrintText(graph, " ОПЦИИ - " + data.name, offset_x_left, y, col1, rowheight, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, test);
				y += rowheight;
				y = PrintListOptions(graph, y, test, print, data.options);
			}
			return y;
		}

		private int PrintListOptions(Graphics graph, int offset,  bool test, bool print, object o)
		{
			// Вспомогательные инструменты
			RectangleF rect;
			int y		= offset;
			string text	= "";
			// Настроечные параметры
			int offset_x_left		= 30;
			int offset_x_right		= 10;
			int page_width			= 210;
			int title_height		= 4;
			int page_height			= 297 - 10;

			int	col1		=	200;
			int col3		=	15;
			int col4		=	20;
			int col2		=	page_width - offset_x_left - offset_x_right - col1 - col3 - col4;
			int	rowheight	= 3;

			if(o == null)
			{
				y += rowheight;
				return y;
			}
			foreach(DtAutoOption data in (ArrayList)o)
			{
				string txt = data.name;
				if (data.tmp_option_variant_name != "") txt += " / " + data.tmp_option_variant_name;
				PrintText(graph, txt, offset_x_left, y, col1, rowheight, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, test);
				y += rowheight;
			}
			return y;
		}
	}
}
