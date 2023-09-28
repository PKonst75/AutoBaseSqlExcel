using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbPrintGuarantee.
	/// </summary>
	public class DbPrintGuarantee:DbPrint
	{
		SolidBrush	draw_brush;
		Font		font_print;
		Font		font_large_bold;
		Pen			pen_bold;

		DateTime	date_close;		// Дата закрытия заказ-наряда
		bool		date_close_is;	// Флаг присутсвия даты закрытия
		long		warrant_number;	// Номер заказ-няряда
		ArrayList	works;			// Список работ по карточке

		public DbPrintGuarantee(DtCard card)
		{
			// Подготовка инструментов к печати
			draw_brush		= new SolidBrush(Color.Black);
			font_print		= new Font("Arial", 10);
			font_large_bold	= new Font("Arial", 14, FontStyle.Bold);
			pen_bold		= new Pen(draw_brush, 1);

			// Определение необходимых данных
			// Дата закрытия заказ-наряда
			DtCardAction close = DbSqlCardAction.FindClose(card);
			if(close != null)
			{
				date_close		= (DateTime)close.Date;
				date_close_is	= true;
			}
			else
			{
				date_close		= DateTime.Now;
				date_close_is	= false;
			}
			// Номер заказ-наряда
			warrant_number	= card.WarrantNumber;
			// Список работ по карточке
			works = new ArrayList();
			DbSqlCardWork.SelectInArray(card, works);
		}

		public override void  PrintPage(Graphics graph, int page)
		{
			PrintTitle(graph);
			float y = 40;
			foreach(object o in works)
			{
				DtCardWork work = (DtCardWork)o;
				y += PrintWork(graph, y, work, false);
			}
			PrintGuaranteeConditions(graph, 100, false);
		}

		private void PrintTitle(Graphics graph)
		{
			// Обязательные реквизиты
			PrintImage(graph, "auto1.jpg", 10, 10, 40, 16, false);
			PrintText(graph, "Академгородок, ул. Русская, 48", 55, 10, 100, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, "(383)330-03-03 (отдел продаж)", 55, 14, 100, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, "(383)332-02-92 (сервисный центр)", 55, 18, 100, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, "(383)333-81-02 (тюнинг)", 55, 22, 100, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);

			// Данные гарантийного талона
			PrintText(graph, "ГАРАНТИЙНЫЙ ТАЛОН №" + warrant_number.ToString(), 115, 10, 90, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_large_bold, draw_brush, false);
			if(date_close_is)
				PrintText(graph, "ОТ " + date_close.ToShortDateString(), 115, 15, 90, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_large_bold, draw_brush, false);
			// Отчерк
			PrintLineHor(graph, 10, 30, 190, pen_bold, false);
		}

		private void PrintGuaranteeConditions(Graphics graph, float y, bool test)
		{
			// Условия гарантии
			string text;
			text = "Ремонт производиться на территории станции технического обслуживания. Расходы по доставке на СТО и ответственность за возможные повреждения при транспортировке несет Покупатель. Ремонт осуществляется в срок до 20 дней. При отсутствии запчастей, срок ремонта может быть продлен до 45 дней.";
			text += "\n" + "В случае нестабильного проявления неисправности, началом ремонта считается момент проявления неисправности на СТО.";

			if(!test)PrintText(graph, text, 10, y, 190, 40, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
		}

		private float PrintWork(Graphics graph, float y, DtCardWork work, bool test)
		{
			// Печать одной работы
			string text = "";
			text = (string)work.GetData("НАИМЕНОВАНИЕ_КАРТОЧКА_РАБОТА");
			SizeF size = MeasureText(graph, text, 190, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print);
			if(!test)PrintText(graph, text, 10, y, 190, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			return size.Height;
		}
	}
}
