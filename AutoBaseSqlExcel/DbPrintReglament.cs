using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Печать списка регламентных работ
	/// </summary>
	public class DbPrintReglament:DbPrint
	{
		SolidBrush	draw_brush;
		Font		font_print;
		Font		font_large_bold;
		Pen			thin_pen;

		private ArrayList	cardWorks;
		private ArrayList	cardWorkCollections;
		private ArrayList	works;
		private DbCard		the_card;

		bool	is_collection	= false;

		public DbPrintReglament(DbCard card)
		{
			// Инструменты для печати
			draw_brush		= new SolidBrush(Color.Black);
			font_print		= new Font("Arial", 10);
			font_large_bold	= new Font("Arial", 14, FontStyle.Bold);
			thin_pen		= new Pen(draw_brush, 0.3F);

			the_card	= card;
			// Список работ карточки
			cardWorks = new ArrayList();
			DbCardWork.FillList(cardWorks, card);

			// Для каждой работы загружаем ее набор
			// И создаем список работ с набором!
			cardWorkCollections = new ArrayList();
			works				= new ArrayList();
			foreach(DbCardWork card_wrk in cardWorks)
			{
				DtWork wrk = DbSqlWork.Find(card_wrk.CodeWork);
				if(wrk != null)
				{
					long code_collection = (long)wrk.GetData("ССЫЛКА_КОД_КОЛЛЕКЦИЯ");
					if(code_collection != 0)
					{
						works.Add(wrk);
						ArrayList arr = new ArrayList();
						DbSqlWorkCollectionItem.SelectInArray(arr, code_collection);
						cardWorkCollections.Add(arr);
					}
				}
			}

			// Устанавливаем флаг - есть ли регламентные работы
			if(works.Count > 0) is_collection = true;
		}

		public bool IsCollection
		{
			get
			{
				return is_collection;
			}
		}

		public override void  PrintPage(Graphics graph, int page)
		{
			// Для ориентации на странице
			int offset = 0;

			offset = 10;
			//offset = PrintHeader(graph, offset);
			offset = PrintBlock(graph, offset, new DelegatePrintBlock(PrintHeader), null);
			offset = PrintBody(graph, offset);
		}

		protected int PrintHeader(Graphics graph, int offset)
		{
			string text = the_card.WarrantNumberTxt;// + " от " + the_card.WarrantDateTxt;
			PrintText(graph, "РЕГЛАМЕНТНЫЕ РАБОТЫ К ЗАКАЗ-НАРЯДУ №" + text, 10, 0 + offset, 190, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_large_bold, draw_brush, false);
			return offset + 10;
		}

		protected int PrintBody(Graphics graph, int offset)
		{
			int count = 0;
			foreach(DtWork element in works)
			{
			//	SizeF size = MeasureText(graph, element.GetData("НАИМЕНОВАНИЕ").ToString(), 150F, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print);
			//	PrintText(graph, element.GetData("НАИМЕНОВАНИЕ").ToString(), 10, offset, 150, size.Height, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			//	offset += (int)Math.Ceiling(size.Height);
				offset = PrintBlock(graph, offset, new DelegatePrintBlock(PrintCollectionHead), element);
				ArrayList array = (ArrayList)cardWorkCollections[count];
				offset = PrintCollectionItems(graph, offset, array);
				count++;
			}
			return offset;
		}

		protected int PrintCollectionItems(Graphics graph, int offset, ArrayList collection)
		{
			foreach(DtWorkCollectionItem element in collection)
			{
				DtWorkCollectionItem item = (DtWorkCollectionItem)element;
				//offset = PrintCollectionItem(graph, offset, item);
				offset = PrintBlock(graph, offset, new DelegatePrintBlock(PrintCollectionItem), (object)item);
			}
			return offset;
		}
		protected int PrintCollectionItem(Graphics graph, int offset, DtWorkCollectionItem item)
		{
			RectangleF rect = new RectangleF(20, offset, 3, 3);
			PrintBox(graph, rect, thin_pen);

			SizeF size = MeasureText(graph, item.GetData("НАИМЕНОВАНИЕ_КОЛЛЕКЦИЯ_ЭЛЕМЕНТ").ToString(), 120F, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print);
			PrintText(graph, item.GetData("НАИМЕНОВАНИЕ_КОЛЛЕКЦИЯ_ЭЛЕМЕНТ").ToString(), 30, offset, 120, size.Height, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);

			int height = (int)Math.Ceiling(size.Height);
			if(height < 6) height = 6;
			offset += height;

			PrintLineHor(graph, 155, offset, 40, thin_pen, false);

			return offset;

		}

		// Для реализации многостраничной печати
		protected int PrintCollectionItem(Graphics graph, int offset, bool test, bool print, object o)
		{
			DtWorkCollectionItem item = (DtWorkCollectionItem)o;
			SizeF size;
			int height;

			if(test == true)
			{
				// Производим тест размера неделимого блока
				size = MeasureText(graph, item.GetData("НАИМЕНОВАНИЕ_КОЛЛЕКЦИЯ_ЭЛЕМЕНТ").ToString(), 120F, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print);
				height = (int)Math.Ceiling(size.Height);
				if(height < 6) height = 6;
				offset += height;
				return offset;
			}

			RectangleF rect = new RectangleF(20, offset, 3, 3);
			if(print) PrintBox(graph, rect, thin_pen);

			size = MeasureText(graph, item.GetData("НАИМЕНОВАНИЕ_КОЛЛЕКЦИЯ_ЭЛЕМЕНТ").ToString(), 120F, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print);
			if(print) PrintText(graph, item.GetData("НАИМЕНОВАНИЕ_КОЛЛЕКЦИЯ_ЭЛЕМЕНТ").ToString(), 30, offset, 120, size.Height, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);

			string txt;
			if((float)item.GetData("ТРУДОЕМКОСТЬ_КОЛЛЕКЦИЯ_ЭЛЕМЕНТ") != 0.0F)
				txt = item.GetData("ТРУДОЕМКОСТЬ_КОЛЛЕКЦИЯ_ЭЛЕМЕНТ").ToString();
			else
				txt = "-";
			if(print) PrintText(graph, txt, 120 + 30, offset, 15, size.Height, System.Drawing.StringAlignment.Center, System.Drawing.StringAlignment.Center, font_print, draw_brush, false);

			height = (int)Math.Ceiling(size.Height);
			if(print) PrintLineHor(graph, 120 + 30 + 15, offset + height, 30, thin_pen, false);

			if(height < 6) height = 6;
			offset += height;

			return offset;
		}

		protected int PrintCollectionHead(Graphics graph, int offset, bool test, bool print, object o)
		{
			DtWork element	= (DtWork)o;
			SizeF size;
			int height;

			if(test == true)
			{
				// Производим тест размера неделимого блока
				size = MeasureText(graph, element.GetData("НАИМЕНОВАНИЕ").ToString(), 150F, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print);
				height = (int)Math.Ceiling(size.Height);
				offset += height;
				return offset;
			}

			size = MeasureText(graph, element.GetData("НАИМЕНОВАНИЕ").ToString(), 150F, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print);
			if(print) PrintText(graph, element.GetData("НАИМЕНОВАНИЕ").ToString(), 10, offset, 150, size.Height, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);

			height = (int)Math.Ceiling(size.Height);
			offset += height;

			return offset;
		}
		protected int PrintHeader(Graphics graph, int offset,  bool test, bool print, object o)
		{
			if(test == true)
			{
				return offset + 10;
			}
			string text = the_card.WarrantNumberTxt;// + " от " + the_card.WarrantDateTxt;
			if(print)PrintText(graph, "РЕГЛАМЕНТНЫЕ РАБОТЫ К ЗАКАЗ-НАРЯДУ №" + text, 10, 0 + offset, 190, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_large_bold, draw_brush, false);
			return offset + 10;
		}
	}
}
