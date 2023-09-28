using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbPrintDopList.
	/// </summary>
	public class DbPrintDopList:DbPrint
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
			public ArrayList	works		= null;
			public ArrayList	details		= null;
			public float		hole_summ	= 0.0F;
			public long			card_number	= 0;
			public int			card_year	= 0;
			public bool			mark		= true;
			
			public HeaderData(long the_card_number, int the_card_year)
			{
				card_number	= the_card_number;
				card_year	= the_card_year;
				works = new ArrayList();
				details = new ArrayList();
				DbSqlCardWork.SelectInArray(card_number, card_year, works);
				DbSqlCardDetail.SelectInArray(card_number, card_year, details);
				// Анализируем данные карточки
				foreach(DtCardWork work in works)
				{
					if (work.GuaranteeFlag()) mark = false;
					hole_summ += work.WorkSummCash;
				}
				foreach(DtCardDetail detail in details)
				{
					if (detail.IsGuaranty) mark = false;
					hole_summ += detail.DetailSummCash;
				}
			}
		}
		ArrayList	header_datas = null;
	

		public DbPrintDopList(long code_auto)
		{
			// Подготовка инструментов для печати
			brush_standart		= new SolidBrush(Color.Black);
			brush_lightgray		= new SolidBrush(Color.LightGray);
			font_small_bold		= new Font("Arial", 8, FontStyle.Bold);
			font_small			= new Font("Arial", 8);
			font_small_cur		= new Font("Arial", 8, FontStyle.Italic);
			font_middle_bold	= new Font("Arial", 10, FontStyle.Bold);
			font_middle			= new Font("Arial", 10);
			pen_thin			= new Pen(brush_standart, 0.3F);

			if(code_auto != 0)
			{
				// Сумели найти карточку, получаем данные для печати
				ArrayList cards = new ArrayList();
				DbSqlCard.SelectCardOpenNumberAuto(cards, code_auto);
				header_datas = new ArrayList();
				foreach (DtCard card in cards)
				{
					long card_number	= (long)card.GetData("НОМЕР_КАРТОЧКА");
					int card_year		= (int)card.GetData("ГОД_КАРТОЧКА");
					DtCard the_card = DbSqlCard.Find(card_number, card_year);
					bool mark = false;
					// тест пригодности карточки
					if((bool)the_card.GetData("ВНУТРЕННИЙ_КАРТОЧКА") == false)
					{
						mark = true;
					}
					if (mark)
					{
						HeaderData header_data = null;
						header_data = new HeaderData(card_number, card_year);
						if (header_data.mark == true)
							header_datas.Add(header_data);
					}
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
				PrintText(graph, "ЗАКАЗ-НАРЯД №" + data.card_number + " / " + data.hole_summ.ToString(), offset_x_left, y, col1, rowheight, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, test);
				y += rowheight;
				y = PrintListWork(graph, y, test, print, data.works);
				PrintText(graph, "-----------------", offset_x_left+20, y, col1, rowheight, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, test);
				y += rowheight;
				y = PrintListDetail(graph, y, test, print, data.details);
			}
			return y;
		}

		private int PrintListWork(Graphics graph, int offset,  bool test, bool print, object o)
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
			int	rowheight	= 6;

			foreach(DtCardWork data in (ArrayList)o)
			{
				DtTxtCardWork txtCardWork = new DtTxtCardWork(data);
				PrintText(graph, txtCardWork.WorkName + " x " + data.WorkQuontity, offset_x_left, y, col1, rowheight, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, test);
				y += rowheight;
			}
			return y;
		}
		private int PrintListDetail(Graphics graph, int offset,  bool test, bool print, object o)
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
			int	rowheight	= 6;

			foreach(DtCardDetail data in (ArrayList)o)
			{
				PrintText(graph, data.DetailName + " x " + data.DetailQuontityTxt, offset_x_left, y, col1, rowheight, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, test);
				y += rowheight;
			}
			return y;
		}
	}
}
