using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbPrintCard1.
	/// </summary>
	public class DbPrintCard1:DbPrint
	{
		SolidBrush	draw_brush;
		Font		font_print;
		Font		font_large_bold;

		public DbPrintCard1()
		{
			draw_brush		= new SolidBrush(Color.Black);
			font_print		= new Font("Arial", 10);
			font_large_bold	= new Font("Arial", 14, FontStyle.Bold);
		}

		public override void  PrintPage(Graphics graph, int page)
		{
			PrintImage(graph, "auto1.jpg", 10, 10, 40, 16, false);
			PrintText(graph, "Академгородок, ул. Русская, 48", 55, 10, 100, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, "(383)330-03-03 (отдел продаж)", 55, 14, 100, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, "(383)306-63-53 (сервисный центр)", 55, 18, 100, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, "(383)333-81-02 (тюнинг)", 55, 22, 100, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_print, draw_brush, false);
			PrintText(graph, "ЗАКАЗ-НАРЯД №10235/2006/10234", 115, 10, 90, 20, System.Drawing.StringAlignment.Center, System.Drawing.StringAlignment.Near, font_large_bold, draw_brush, false);
		}
	}
}
