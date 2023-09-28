using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Печать карточки автомобиля
	/// </summary>
	/// 
	public class PrintAutoCoupon
	{
		DbAuto		auto		= null;

		Pen			thinPen		= null;
		SolidBrush	drawBrush	= null;
		Font		printFont	= null;
		Font		boldFont	= null;
		Font		boldFont12	= null;

		public PrintAutoCoupon(DbAuto src)
		{
			auto	= new DbAuto(src);	// Новая копия автомобиля
		}

		// Подготовка инструментов печати, шрифтов, ручек
		private void PrepareTools()
		{
			// Ручки
			thinPen			= new Pen(Color.Black, (float)0.02);
			// Кисти
			drawBrush		= new SolidBrush(Color.Black);
			// Шрифты
			printFont		= new Font("Arial", 12);
			boldFont		= new Font("Arial", 16, FontStyle.Bold);
			boldFont12		= new Font("Arial", 12, FontStyle.Bold);
		}
		// Выполняется при печати каждой страницы
		private void pd_PrintPage(object sender, PrintPageEventArgs ev) 
		{
			// Настройка параметров печати
			ev.Graphics.PageUnit = GraphicsUnit.Millimeter;

			PrintMainTitle(ev.Graphics);
			PrintCouponTitle(ev.Graphics);
			PrintAutoTitle(ev.Graphics);
			PrintAutoPrice(ev.Graphics);

			ev.HasMorePages = false;		// В нашем случае страница только одна
		}
		// Основной обработчик события печати
		public void Print() 
		{
			try 
			{
				try 
				{
					PrepareTools();				// Готовим инструменты

					PrintDocument pd = new PrintDocument();
					// Свойтсва принтера
					pd.DefaultPageSettings.Landscape = false;
					pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);
					PrintPreviewDialog preview = new PrintPreviewDialog();
					preview.Document = pd;
					preview.ShowDialog();
					//pd.Print();
				}  
				finally 
				{
				}
			}  
			catch(Exception ex) 
			{
				MessageBox.Show(ex.Message);
			}
		}

		#region Печать Блоков
		private void PrintMainTitle(Graphics graph)
		{
			// Печать основного заголовка карточки
			string text;
			RectangleF rect = new RectangleF(0, 0, 0, 0);

			// Отрисовка картинки в фиксированном положении
			rect.X = 10;
			rect.Y = 10;
			rect.Width = 60;
			rect.Height = 20;
			Image carImage = Image.FromFile("avto1.bmp");
			graph.DrawImage(carImage, rect);

			rect.Y = 30;
			rect.X = 10;
			rect.Width = 90;
			rect.Height = 10;
			text = "г.Новосибирск, ул.Русская,48";
			PrintBoxFixedNoBorder(graph, text, rect, StringAlignment.Near);
			rect.Y = 35;
			text = "т.(383)330-03-03 (многоканальный)";
			PrintBoxFixedNoBorder(graph, text, rect, StringAlignment.Near);
			rect.Y = 40;
			text = "www.avto-1.ru";
			PrintBoxFixedNoBorder(graph, text, rect, StringAlignment.Near);
		}
		private void PrintAutoTitle(Graphics graph)
		{
			// Печать Информации по автомобилю
			string txt;
			RectangleF rect = new RectangleF(0, 0, 0, 0);
			int posY	= 50;

			// Заголовок таблицы
			rect.Y = posY;
			rect.X = 10;
			rect.Width = 90;
			rect.Height = 10;
			txt = "МОДЕЛЬ АВТОМОБИЛЯ";
			PrintBoxFixed(graph, txt, rect, true, StringAlignment.Near, StringAlignment.Center, boldFont);
			rect.Y = rect.Y + 10;
			txt = "ВАРИАНТ, КОМПЛЕКТАЦИЯ";
			PrintBoxFixed(graph, txt, rect, true, StringAlignment.Near, StringAlignment.Center, boldFont);
			rect.Y = rect.Y + 10;
			txt = "ЦВЕТ";
			PrintBoxFixed(graph, txt, rect, true, StringAlignment.Near, StringAlignment.Center, boldFont);
			rect.Y = rect.Y + 10;
			txt = "VIN";
			PrintBoxFixed(graph, txt, rect, true, StringAlignment.Near, StringAlignment.Center, boldFont);
			rect.Y = rect.Y + 10;
			txt = "№ ДВИГАТЕЛЯ";
			PrintBoxFixed(graph, txt, rect, true, StringAlignment.Near, StringAlignment.Center, boldFont);
			// Заполнение таблицы
			rect.Y = posY;
			rect.X = 100;
			rect.Width = 90;
			rect.Height = 10;
			txt = auto.ModelTxt;
			PrintBoxFixed(graph, txt, rect, true, StringAlignment.Near, StringAlignment.Center, boldFont);
			rect.Y = rect.Y + 10;
			txt = auto.AutoSubModelTxt + auto.AutoComplectTxt;
			PrintBoxFixed(graph, txt, rect, true, StringAlignment.Near, StringAlignment.Center, boldFont);
			rect.Y = rect.Y + 10;
			txt = auto.AutoColorsTxt;
			PrintBoxFixed(graph, txt, rect, true, StringAlignment.Near, StringAlignment.Center, boldFont);
			rect.Y = rect.Y + 10;
			txt = auto.Vin;
			PrintBoxFixed(graph, txt, rect, true, StringAlignment.Near, StringAlignment.Center, boldFont);
			rect.Y = rect.Y + 10;
			txt = auto.EngineNo;
			PrintBoxFixed(graph, txt, rect, true, StringAlignment.Near, StringAlignment.Center, boldFont);
		}

		private void PrintAutoPrice(Graphics graph)
		{
			// Печать Информации по автомобилю
			string txt;
			RectangleF rect = new RectangleF(0, 0, 0, 0);
			int posY	= 110;
			float y		= 0.0f;

			// Заголовок таблицы
			rect.Y = posY;
			rect.X = 10;
			rect.Width = 140;
			rect.Height = 10;
			txt = "БАЗОВАЯ ЦЕНА АВТОМОБИЛЯ";
			PrintBoxFixed(graph, txt, rect, true, StringAlignment.Near, StringAlignment.Center, boldFont);
			txt	= "";
			rect.X = 150;
			rect.Width = 40;
			PrintBoxFixed(graph, txt, rect, true);
			y	= rect.Y + 10;
			PrintOptionTitle(graph, "Дополнительнын опции", y);		y += 5;
			PrintOption(graph, "Резина", y);						y += 5;
			PrintOption(graph, "Литые диски", y);					y += 5;
			PrintOption(graph, "Свечи", y);							y += 5;
			PrintOption(graph, "Маршрутный компьютер", y);			y += 5;
			PrintOption(graph, "Коврики салона", y);				y += 5;
			PrintOption(graph, "", y);								y += 5;
			PrintOption(graph, "", y);								y += 5;
			PrintOption(graph, "", y);								y += 5;
			PrintOption(graph, "Антикор", y);						y += 5;
			PrintOption(graph, "Защита картера", y);				y += 5;
			PrintOption(graph, "Проставки", y);						y += 5;
			PrintOption(graph, "", y);								y += 5;
			PrintOption(graph, "", y);								y += 5;
			PrintOptionTitle(graph, "Тюнинг", y);					y += 5;
			PrintOption(graph, "Автомагнитола", y);					y += 5;
			PrintOption(graph, "Передняя аккустика", y);			y += 5;
			PrintOption(graph, "Задняя аккустика", y);				y += 5;
			PrintOption(graph, "Антена", y);						y += 5;
			PrintOption(graph, "", y);								y += 5;
			PrintOption(graph, "", y);								y += 5;
			PrintOption(graph, "Сигнализация", y);					y += 5;
			PrintOption(graph, "", y);								y += 5;
			PrintOption(graph, "", y);								y += 5;
			PrintOption(graph, "", y);								y += 5;
			PrintOption(graph, "", y);								y += 5;
			PrintOption(graph, "", y);								y += 5;
			PrintOptionTitle(graph, "Оформление", y);				y += 5;
			PrintOption(graph, "Транзитный номер", y);				y += 5;
			rect.Y = y;
			rect.X = 10;
			rect.Width = 140;
			rect.Height = 10;
			txt = "ОБЩАЯ СТОИМОСТЬ";
			PrintBoxFixed(graph, txt, rect, true, StringAlignment.Near, StringAlignment.Center, boldFont);
			txt	= "";
			rect.X = 150;
			rect.Width = 40;
			PrintBoxFixed(graph, txt, rect, true);
		}
		private void PrintOption(Graphics graph, string txt, float posY)
		{
			// Печать позиции опции
			RectangleF rect = new RectangleF(0, 0, 0, 0);
			rect.X = 10;
			rect.Y = posY;
			rect.Width = 140;
			rect.Height = 5;
			PrintBoxFixed(graph, txt, rect, true, StringAlignment.Near, StringAlignment.Center, boldFont12);
			rect.X = 150;
			rect.Y = posY;
			rect.Width = 40;
			rect.Height = 5;
			PrintBoxFixed(graph, "", rect, true, StringAlignment.Near, StringAlignment.Center, boldFont12);
		}
		private void PrintOptionTitle(Graphics graph, string txt, float posY)
		{
			// Печать заголовка позиций опции
			RectangleF rect = new RectangleF(0, 0, 0, 0);
			rect.X = 10;
			rect.Y = posY;
			rect.Width = 180;
			rect.Height = 5;
			PrintBoxFixed(graph, txt, rect, true, StringAlignment.Center, StringAlignment.Center, boldFont12);
		}
		private void PrintCouponTitle(Graphics graph)
		{
			// Печать Информации по автомобилю
			string txt;
			RectangleF rect = new RectangleF(0, 0, 0, 0);
			int posY	= 10;

			// Заголовок таблицы
			rect.Y = posY;
			rect.X = 100;
			rect.Width = 50;
			rect.Height = 10;
			txt = "ДАТА";
			PrintBoxFixed(graph, txt, rect, false, StringAlignment.Near, StringAlignment.Center, boldFont);
			rect.Y = rect.Y + 10;
			txt = "МЕНЕДЖЕР";
			PrintBoxFixed(graph, txt, rect, false, StringAlignment.Near, StringAlignment.Center, boldFont);
			rect.Y = rect.Y + 10;
			txt = "НАЛИЧИЕ ПТС";
			PrintBoxFixed(graph, txt, rect, false, StringAlignment.Near, StringAlignment.Center, boldFont);
			rect.X		= 170;
			rect.Width	= 10;
			txt = "";
			PrintBoxFixed(graph, txt, rect, true);
		}
		#endregion

		#region Печать шаблонов
		public void PrintBoxFixed(Graphics graph, string text, Rectangle rect)
		{
			StringFormat strFormat = new StringFormat();
			strFormat.Alignment = StringAlignment.Center;
			strFormat.LineAlignment = StringAlignment.Center;
			strFormat.Trimming = StringTrimming.Word;
			graph.DrawString(text, printFont, drawBrush, rect, strFormat);
			graph.DrawRectangle(thinPen, rect);
		}
		public void PrintBoxFixed(Graphics graph, string text, RectangleF rect)
		{
			StringFormat strFormat = new StringFormat();
			strFormat.Alignment = StringAlignment.Center;
			strFormat.LineAlignment = StringAlignment.Center;
			strFormat.Trimming = StringTrimming.Word;
			graph.DrawString(text, printFont, drawBrush, rect, strFormat);
			graph.DrawRectangle(thinPen, rect.X, rect.Y, rect.Width, rect.Height);
		}
		public void PrintBoxFixedNoBorder(Graphics graph, string text, RectangleF rect, StringAlignment alignment)
		{
			StringFormat strFormat = new StringFormat();
			strFormat.Alignment = alignment;
			strFormat.LineAlignment = StringAlignment.Center;
			strFormat.Trimming = StringTrimming.Word;
			graph.DrawString(text, printFont, drawBrush, rect, strFormat);
		}
		public void PrintBoxFixed(Graphics graph, string text, RectangleF rect, bool border)
		{
			StringFormat strFormat = new StringFormat();
			strFormat.Alignment = StringAlignment.Center;
			strFormat.LineAlignment = StringAlignment.Center;
			strFormat.Trimming = StringTrimming.Word;
			graph.DrawString(text, printFont, drawBrush, rect, strFormat);
			if(border == true)
				graph.DrawRectangle(thinPen, rect.X, rect.Y, rect.Width, rect.Height);
		}
		public void PrintBoxFixed(Graphics graph, string text, RectangleF rect, bool border, StringAlignment alignment, StringAlignment lineAlignment)
		{
			StringFormat strFormat = new StringFormat();
			strFormat.Alignment = alignment;
			strFormat.LineAlignment = lineAlignment;
			strFormat.Trimming = StringTrimming.Word;
			graph.DrawString(text, printFont, drawBrush, rect, strFormat);
			if(border == true)
				graph.DrawRectangle(thinPen, rect.X, rect.Y, rect.Width, rect.Height);
		}
		public void PrintBoxFixed(Graphics graph, string text, RectangleF rect, bool border, StringAlignment alignment, StringAlignment lineAlignment, Font font)
		{
			StringFormat strFormat = new StringFormat();
			strFormat.Alignment = alignment;
			strFormat.LineAlignment = lineAlignment;
			strFormat.Trimming = StringTrimming.Word;
			graph.DrawString(text, font, drawBrush, rect, strFormat);
			if(border == true)
				graph.DrawRectangle(thinPen, rect.X, rect.Y, rect.Width, rect.Height);
		}
		#endregion
	}
}
