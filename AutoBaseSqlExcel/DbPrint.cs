using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.IO;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbPrint.
	/// </summary>
	public class DbPrint
	{
		// Реализация многостраничности
		protected bool have_more_pages	= false;
		int	pages						= 0;
		int page_to_print				= 0;
		int page_current				= 0;
		int page_offset					= 0;
		bool block_head_print			= false;	// Отметка, для печати блоков с заголовком

		// Зависимость от настроек страницы
		protected int page_max_y					= 287;
		protected int page_min_y					= 10;

		public enum TEST_LVL : short { NOTEST = 0, SIMPLETEST = 1 };

		private Font _selectedFont; // Выбранный в данный момент Font
		private Brush _selectedBrush; // выбранный в данный Brush
		private Pen _selectedPen; // Выбранный в данный момент Pen
		private StringFormat _selectedStringFormat; // Выбранный в данный момент StringFormat
		private Graphics _selectedGraphics; // Выбранный в данный момент элемент Graphics
		private float _selectedTextBoxOffsetX; // Выбранный оступ от границы окружающего тест прямоугольника по горизонтали
		private TEST_LVL _selectedTestLevel; // Выбранный уровеь тестирования

		#region Работа через устанолвенные настройки
		public TEST_LVL SetTestLvl(TEST_LVL srcTestLvl)
		{
			TEST_LVL previous = _selectedTestLevel;
			_selectedTestLevel = srcTestLvl;
			return previous;
		}
		public TEST_LVL SimpleTestOn()
        {
			return SetTestLvl(TEST_LVL.SIMPLETEST);
        }
		public TEST_LVL TestOff()
		{
			return SetTestLvl(TEST_LVL.NOTEST);
		}
		public void SelectBrush(Brush srcBrush)
        {
			_selectedBrush = srcBrush;
        }

		public void SelectPen(Pen srcPen)
		{
			_selectedPen = srcPen;
		}
		public void SelectFont(Font srcFont)
		{
			_selectedFont = srcFont;
		}
		public void SelectGraph(Graphics srcGraphics)
		{
			_selectedGraphics = srcGraphics;
		}
		public void SelectTextBoxOffsetX(float srcTextBoxOffset)
		{
			_selectedTextBoxOffsetX = srcTextBoxOffset;
		}
		public void SetStringFormat(StringAlignment srcAlignment, StringAlignment srcLineAlignment)
		{
			if(_selectedStringFormat == null)
				_selectedStringFormat = new StringFormat();
			_selectedStringFormat.Alignment = srcAlignment;
			_selectedStringFormat.LineAlignment = srcLineAlignment;
			_selectedStringFormat.Trimming = StringTrimming.Word;
		}
		public float SelectedPrintTextBox(string text, float x, float y, float width, float height)
		{
			height = SelectedPrintTextNoBox(text, x, y, width, height);
			if (_selectedTestLevel != TEST_LVL.NOTEST) return height;
			RectangleF rect_box = new RectangleF(x, y, width, height);
			_selectedGraphics.DrawRectangle(_selectedPen, rect_box.X, rect_box.Y, rect_box.Width, rect_box.Height);
			return height;
		}
		public float SelectedPrintTextNoBox(string text, float x, float y, float width, float height)
		{
			SizeF size = SelectedMeasureTextBox(text, width - _selectedTextBoxOffsetX * 2);
			if (size.Height > height) height = size.Height;
			if (_selectedTestLevel != TEST_LVL.NOTEST) return height;
			RectangleF rect_text = new RectangleF(x + _selectedTextBoxOffsetX, y, width - _selectedTextBoxOffsetX, height);
			_selectedGraphics.DrawString(text, _selectedFont, _selectedBrush, rect_text, _selectedStringFormat);
			return height;
		}
		public SizeF SelectedMeasureTextBox(string text, float width)
		{
			return _selectedGraphics.MeasureString(text, _selectedFont, (int)width, _selectedStringFormat);
		}
		public float SelectedPrintTextNoMesure(string text, float x, float y, float width, float height)
		{
			RectangleF rect = new RectangleF(x, y, width, height);
			if (_selectedTestLevel != TEST_LVL.NOTEST) return height;
			_selectedGraphics.DrawString(text, _selectedFont, _selectedBrush, rect, _selectedStringFormat);
			return height;
		}
		public float SelectedPrintTextNoBoxNoMeasure(string text, RectangleF rect)
		{
			if (_selectedTestLevel != TEST_LVL.NOTEST) return rect.Height;
			RectangleF rect_text = new RectangleF(rect.X + _selectedTextBoxOffsetX, rect.Y, rect.Width - _selectedTextBoxOffsetX, rect.Height);
			_selectedGraphics.DrawString(text, _selectedFont, _selectedBrush, rect_text, _selectedStringFormat);
			return rect.Height;
		}
		public float SelectedPrintTextNoBoxPair(string textCol1, string textCol2, float x, float y, float width1, float width2, float height)
        {
			float heightCol1;
			float heightCol2;
			TEST_LVL level = SimpleTestOn();
			heightCol1 = SelectedPrintTextNoBox(textCol1, x, y, width1, height);
			heightCol2 = SelectedPrintTextNoBox(textCol2, x + width1, y, width2, height);
			SetTestLvl(level);
			if (heightCol1 > height) height = heightCol1;
			if (heightCol2 > height) height = heightCol2;

			if (_selectedTestLevel != TEST_LVL.NOTEST) return height;
			SelectedPrintTextNoBox(textCol1, x, y, width1, height);
			SelectedPrintTextNoBox(textCol2, x + width1, y, width2, height);
			return height;
		}
		public float SelectedPrintTextBoxPair(string textCol1, string textCol2, float x, float y, float width1, float width2, float height)
		{
			float heightCol1;
			float heightCol2;
			TEST_LVL level = SimpleTestOn();
			heightCol1 = SelectedPrintTextNoBox(textCol1, x, y, width1, height);
			heightCol2 = SelectedPrintTextNoBox(textCol2, x + width1, y, width2, height);
			SetTestLvl(level);
			if (heightCol1 > height) height = heightCol1;
			if (heightCol2 > height) height = heightCol2;

			if (_selectedTestLevel != TEST_LVL.NOTEST) return height;
			SelectedPrintTextBox(textCol1, x, y, width1, height);
			SelectedPrintTextBox(textCol2, x + width1, y, width2, height);
			return height;
		}
		public float SelectedPrintTableGroup(object[] tableData, float x, float y, float height)
		{
			float heightCol;
			TEST_LVL level = SimpleTestOn();
			string txt;
			float width;
			foreach (object[] element in tableData)
            {
				txt = (string)element[0];
				width = (float)Convert.ToDecimal(element[1]);
				heightCol = SelectedPrintTextBox(txt, x, y, width, height);
				if (heightCol > height) height = heightCol;
			}
			SetTestLvl(level);
			if (_selectedTestLevel != TEST_LVL.NOTEST) return height;

			foreach (object[] element in tableData)
			{
				txt = (string)element[0];
				width = (float)Convert.ToDecimal(element[1]); ;
				SelectedPrintTextBox(txt, x, y, width, height);
				x += width;
			}
			return height;
		}
		
		public void SelectedPrintRoundBox(RectangleF rect, int radius)
		{
			if (_selectedTestLevel != TEST_LVL.NOTEST) return;
			int diameter = radius * 2;
			_selectedGraphics.DrawLine(_selectedPen, rect.X + radius, rect.Y, rect.X + rect.Width - radius, rect.Y);
			_selectedGraphics.DrawLine(_selectedPen, rect.X + radius, rect.Y + rect.Height, rect.X + rect.Width - radius, rect.Y + rect.Height);
			_selectedGraphics.DrawLine(_selectedPen, rect.X, rect.Y + radius, rect.X, rect.Y + rect.Height - radius);
			_selectedGraphics.DrawLine(_selectedPen, rect.X + rect.Width, rect.Y + radius, rect.X + rect.Width, rect.Y + rect.Height - radius);
			_selectedGraphics.DrawArc(_selectedPen, rect.X, rect.Y, diameter, diameter, 180, 90);
			_selectedGraphics.DrawArc(_selectedPen, rect.X, rect.Y + rect.Height - diameter, diameter, diameter, 90, 90);
			_selectedGraphics.DrawArc(_selectedPen, rect.X + rect.Width - diameter, rect.Y + rect.Height - diameter, diameter, diameter, 0, 90);
			_selectedGraphics.DrawArc(_selectedPen, rect.X + rect.Width - diameter, rect.Y, diameter, diameter, 270, 90);
		}
		public void SelectedPrintLineVrt(float x, float y, float length)
		{
			if(_selectedTestLevel != TEST_LVL.NOTEST) return;
			Point p1 = new Point((int)x, (int)y);
			Point p2 = new Point((int)x, (int)y + (int)length);
			_selectedGraphics.DrawLine(_selectedPen, p1, p2);
		}
		public void SelectedPrintImage(string fileName, float x, float y, float width, float height)
		{
			RectangleF rect = new RectangleF(x, y, width, height);
			SelectedPrintImage(fileName, rect);
		}
		public void SelectedPrintImage(string fileName, RectangleF rect)
		{
			if (_selectedTestLevel != TEST_LVL.NOTEST) return;
			Image image;
			try
			{
				image = Image.FromFile(fileName);
			}
			catch (Exception e)
			{
				Db.SetException(e);
				Db.ShowFaults();
				return;     // Файл не найден
			}
			if (image == null) return; // Файл нулевой
			_selectedGraphics.DrawImage(image, rect);
		}
		#endregion

		#region DRY код
		public string LogoFileName(DtBrand.DIALER srcDialer) // Возвращает логитип для печати на карточке в зависимости от бренда
        {
		
			switch (srcDialer)
			{
				case DtBrand.DIALER.lada:
					return "logo_lada.bmp";
				case DtBrand.DIALER.chevrolet:
					return "logo_chevrolet.bmp";
				case DtBrand.DIALER.unknown:
				default:
					return "logo_avto.bmp";
			}
		}
		public bool OfficialMark(DtBrand.DIALER srcDialer) // Возвращает логитип для печати на карточке в зависимости от бренда
		{
			switch (srcDialer)
			{
				case DtBrand.DIALER.lada:
				case DtBrand.DIALER.chevrolet:
					return true;
				case DtBrand.DIALER.unknown:
				default:
					return false;
			}
		}
		#endregion

		public DbPrint()
		{
			// Первичные настройки, чтобы все работала и без дополнительной инициализации
			_selectedBrush = new SolidBrush(Color.Black);
			_selectedFont = new Font("Arial", 8, FontStyle.Bold);
			_selectedPen = new Pen(_selectedBrush, 0.3F);
			_selectedStringFormat = new StringFormat();
			_selectedStringFormat.Alignment = StringAlignment.Near;
			_selectedStringFormat.LineAlignment = StringAlignment.Center;
			_selectedStringFormat.Trimming = StringTrimming.Word;
			_selectedTextBoxOffsetX = 0;
			_selectedTestLevel = TEST_LVL.NOTEST;
		}

		// The PrintPage event is raised for each page to be printed.
		// Пробуем печатать текст!
		private void pd_PrintPage(object sender, PrintPageEventArgs ev) 
		{
			ev.Graphics.PageUnit	= GraphicsUnit.Millimeter;
			have_more_pages			= false;

			// Печать
			PrintPage(ev.Graphics, 0);
			// Конец печать

			// Организация многостраничности
			if(page_current == page_to_print)
			{
				// Завершаем процесс печати, так как напечатали последнюю страницу
				ev.HasMorePages = false;
				page_current	= 0;
				page_to_print	= 0;
			}
			else
			{
				ev.HasMorePages = true;
				page_to_print++;			// Переход на следующую страницу
				page_current	= 0;		// Счетчик страниц опять с нуля
			}
		}

		// The Click event is raised when the user clicks the Print button.
		public void Print() 
		{
			try 
			{
				try 
				{
					// Подготовка ресурсов
			
					
					// Свойтсва принтера
					PrintDocument pd = new PrintDocument();
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

		public static void PrintImage(Graphics graph, string fileName, float x, float y, float width, float height, bool test)
		{
			RectangleF rect;
			Image image;
			
			try
			{
				image		= Image.FromFile(fileName);
			}
			catch(Exception e)
			{
				// Файл не найден
				Db.SetException(e);
				Db.ShowFaults();
				return;
			}
			if(image != null)
			{
				rect		= new RectangleF(x, y, width, height);
				if(!test) graph.DrawImage(image, rect);
			}
		}

		public void PrintText(Graphics graph, string text, float x, float y, float width, float height, StringAlignment alignment, StringAlignment lineAlignment, Font font, Brush brush, bool test)
		{
			RectangleF rect = new RectangleF(x, y, width, height);
			StringFormat strFormat = new StringFormat();
			strFormat.Alignment		= alignment;
			strFormat.LineAlignment = lineAlignment;
			strFormat.Trimming		= StringTrimming.Word;
			if(!test) graph.DrawString(text, font, brush, rect, strFormat);
		}
		public void PrintTextV(Graphics graph, string text, float x, float y, float width, float height, StringAlignment alignment, StringAlignment lineAlignment, Font font, Brush brush, bool test)
		{
			RectangleF rect = new RectangleF(x, y, width, height);
			StringFormat strFormat = new StringFormat();
			strFormat.Alignment		= alignment;
			strFormat.LineAlignment = lineAlignment;
			strFormat.Trimming		= StringTrimming.Word;
			strFormat.FormatFlags	= strFormat.FormatFlags | StringFormatFlags.DirectionVertical;
			if(!test) graph.DrawString(text, font, brush, rect, strFormat);
		}

		public SizeF MeasureText(Graphics graph, string text, float width, StringAlignment alignment, StringAlignment lineAlignment, Font font)
		{
			StringFormat strFormat = new StringFormat();
			strFormat.Alignment		= alignment;
			strFormat.LineAlignment = lineAlignment;
			strFormat.Trimming		= StringTrimming.Word;
			return graph.MeasureString(text, font, (int)width, strFormat);
		}

		public SizeF MeasureTextBox(Graphics graph, string text, float width, float text_offset, StringAlignment alignment, StringAlignment lineAlignment, Font font)
		{
			StringFormat strFormat = new StringFormat();
			strFormat.Alignment		= alignment;
			strFormat.LineAlignment = lineAlignment;
			strFormat.Trimming		= StringTrimming.Word;
			return graph.MeasureString(text, font, (int)(width - text_offset * 2), strFormat);
		}

		public static void PrintLineHor(Graphics graph, float x, float y, float length, Pen pen, bool test)
		{
			Point p1 = new Point((int)x, (int)y);
			Point p2 = new Point((int)x + (int)length, (int)y);
			if(!test) graph.DrawLine(pen, p1, p2);
		}

		public static void PrintLineVrt(Graphics graph, float x, float y, float length, Pen pen, bool test)
		{
			Point p1 = new Point((int)x, (int)y);
			Point p2 = new Point((int)x, (int)y + (int)length);
			if(!test) graph.DrawLine(pen, p1, p2);
		}

		public void PrintBox(Graphics graph, RectangleF rect, Pen pen)
		{
			graph.DrawRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height);
		}

		public void PrintBox(Graphics graph, int x, int y, int width, int height, Pen pen)
		{
			RectangleF rect = new RectangleF(x, y, width, height);
			graph.DrawRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height);
		}

		public void PrintRoundBox(Graphics graph, RectangleF rect, Pen pen, int radius)
		{
			int diameter = radius * 2;
			graph.DrawLine(pen, rect.X + radius, rect.Y, rect.X + rect.Width - radius, rect.Y);
			graph.DrawLine(pen, rect.X + radius, rect.Y + rect.Height, rect.X + rect.Width - radius, rect.Y + rect.Height);
			graph.DrawLine(pen, rect.X, rect.Y  + radius, rect.X, rect.Y + rect.Height - radius);
			graph.DrawLine(pen, rect.X + rect.Width, rect.Y  + radius, rect.X + rect.Width, rect.Y + rect.Height - radius);
			graph.DrawArc(pen, rect.X, rect.Y, diameter, diameter,180, 90);
			graph.DrawArc(pen, rect.X, rect.Y + rect.Height - diameter, diameter, diameter, 90, 90);
			graph.DrawArc(pen, rect.X + rect.Width - diameter, rect.Y + rect.Height - diameter, diameter, diameter, 0, 90);
			graph.DrawArc(pen, rect.X + rect.Width - diameter, rect.Y, diameter, diameter, 270, 90);
		}

		

		public void PrintTextBoxColor(Graphics graph, string text, float x, float y, float width, float height, float text_offset, StringAlignment alignment, StringAlignment lineAlignment, Font font, Brush brush, Pen pen, Brush fill_brush, bool test)
		{
			RectangleF rect_box = new RectangleF(x, y, width, height);
			RectangleF rect_text = new RectangleF(x + text_offset, y, width - text_offset, height);
			StringFormat strFormat = new StringFormat();
			strFormat.Alignment		= alignment;
			strFormat.LineAlignment = lineAlignment;
			strFormat.Trimming		= StringTrimming.Word;
			if(!test) graph.FillRectangle(fill_brush, rect_box.X, rect_box.Y, rect_box.Width, rect_box.Height);
			if(!test) graph.DrawString(text, font, brush, rect_text, strFormat);
			if(!test) graph.DrawRectangle(pen, rect_box.X, rect_box.Y, rect_box.Width, rect_box.Height);		
		}

		public void PrintCheckBox(Graphics graph, string text, float x, float y, float width, float height, float text_offset, int box_count, int distance, StringAlignment alignment, Font font, Brush brush, Pen pen, bool test)
		{
			RectangleF rect_box = new RectangleF(x + width  + text_offset, y + text_offset, height - text_offset * 2, height - text_offset * 2);
			RectangleF rect_text = new RectangleF(x, y, width, height);
			StringFormat strFormat = new StringFormat();
			strFormat.Alignment		= alignment;
			strFormat.LineAlignment = StringAlignment.Center;
			strFormat.Trimming		= StringTrimming.Word;
			if(!test) graph.DrawString(text, font, brush, rect_text, strFormat);
			for(int i = 0 ; i < box_count; i++)
			{
				if(!test) graph.DrawRectangle(pen, rect_box.X + i * (distance + height), rect_box.Y, rect_box.Width, rect_box.Height);
			}
		}

		public void PrintLeftCheckBox(Graphics graph, string text, float x, float y, float width, float height, float text_offset, int box_count, int distance, StringAlignment alignment, Font font, Brush brush, Pen pen, bool test)
		{
			RectangleF rect_box = new RectangleF(x + text_offset, y + text_offset, height - text_offset * 2, height - text_offset * 2);
			RectangleF rect_text = new RectangleF(x + height, y, width, height);
			StringFormat strFormat = new StringFormat();
			strFormat.Alignment		= alignment;
			strFormat.LineAlignment = StringAlignment.Center;
			strFormat.Trimming		= StringTrimming.Word;
			if(!test) graph.DrawString(text, font, brush, rect_text, strFormat);
			for(int i = 0 ; i < box_count; i++)
			{
				if(!test)
				{
					if(!test) graph.DrawRectangle(pen, rect_box.X + i * (distance + height), rect_box.Y, rect_box.Width, rect_box.Height);
				}
			}
		}

		public void PrintRectSigned(Graphics graph, string text, RectangleF rect, int text_offset, int text_width, Font font, Brush brush, Pen pen, bool test)
		{
			RectangleF rect_text = new RectangleF(rect.X + rect.Width + text_offset, rect.Y, text_width, rect.Height);
			StringFormat strFormat = new StringFormat();
			strFormat.Alignment		= StringAlignment.Near;
			strFormat.LineAlignment = StringAlignment.Center;
			strFormat.Trimming		= StringTrimming.Word;
			if(!test) graph.DrawString(text, font, brush, rect_text, strFormat);
			if(!test) graph.DrawRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height);
		}

		public void PrintBlockSigned(Graphics graph, string text, RectangleF rect, int text_offset, Font font, Brush brush, Pen pen, bool test)
		{
			SizeF size = graph.MeasureString(text, font);
			RectangleF rect_text = new RectangleF(rect.X + text_offset, rect.Y - size.Height / 2, size.Width, size.Height);
			StringFormat strFormat = new StringFormat();
			strFormat.Alignment		= StringAlignment.Near;
			strFormat.LineAlignment = StringAlignment.Center;
			strFormat.Trimming		= StringTrimming.Word;
			
			if(!test) graph.DrawString(text, font, brush, rect_text, strFormat);
			if(!test) graph.DrawLine(pen, rect_text.X, rect.Y, rect.X, rect.Y);
			if(!test) graph.DrawLine(pen, rect.X, rect.Y, rect.X, rect.Y + rect.Height);
			if(!test) graph.DrawLine(pen, rect.X, rect.Y + rect.Height, rect.X + rect.Width, rect.Y + rect.Height);
			if(!test) graph.DrawLine(pen, rect.X  + rect.Width, rect.Y + rect.Height, rect.X + rect.Width, rect.Y);
			if(!test) graph.DrawLine(pen, rect.X  + rect.Width, rect.Y, rect_text.X + rect_text.Width, rect.Y);
		}

		public void PrintLeftCheckDiamond(Graphics graph, string text, float x, float y, float width, float height, float text_offset, int box_count, int distance, StringAlignment alignment, Font font, Brush brush, Pen pen, bool test)
		{
			RectangleF rect_box = new RectangleF(x + text_offset, y + text_offset, height - text_offset * 2, height - text_offset * 2);
			RectangleF rect_text = new RectangleF(x + height, y, width, height);
			StringFormat strFormat = new StringFormat();
			strFormat.Alignment		= alignment;
			strFormat.LineAlignment = StringAlignment.Center;
			strFormat.Trimming		= StringTrimming.Word;
			if(!test) graph.DrawString(text, font, brush, rect_text, strFormat);
			for(int i = 0 ; i < box_count; i++)
			{
				if(!test)
				{
					graph.DrawLine(pen, rect_box.X + rect_box.Width / 2, rect_box.Y, rect_box.X, rect_box.Y + rect_box.Height / 2);
					graph.DrawLine(pen, rect_box.X + rect_box.Width / 2, rect_box.Y, rect_box.X + rect_box.Width, rect_box.Y + rect_box.Height / 2);
					graph.DrawLine(pen, rect_box.X + rect_box.Width / 2, rect_box.Y + rect_box.Height, rect_box.X, rect_box.Y + rect_box.Height / 2);
					graph.DrawLine(pen, rect_box.X + rect_box.Width / 2, rect_box.Y + rect_box.Height, rect_box.X + rect_box.Width, rect_box.Y + rect_box.Height / 2);
				}
			}
		}

		public void PrintLeftCheckBoxX(Graphics graph, string text, float x, float y, float width, float height, float text_offset, int box_count, int distance, StringAlignment alignment, Font font, Brush brush, Pen pen, bool test)
		{
			RectangleF rect_box = new RectangleF(x + text_offset, y + text_offset, height - text_offset * 2, height - text_offset * 2);
			RectangleF rect_text = new RectangleF(x + height, y, width, height);
			StringFormat strFormat = new StringFormat();
			strFormat.Alignment		= alignment;
			strFormat.LineAlignment = StringAlignment.Center;
			strFormat.Trimming		= StringTrimming.Word;
			if(!test) graph.DrawString(text, font, brush, rect_text, strFormat);
			for(int i = 0 ; i < box_count; i++)
			{
				if(!test)
				{
					graph.DrawLine(pen, rect_box.X, rect_box.Y, rect_box.X + rect_box.Width, rect_box.Y + rect_box.Height);
					graph.DrawLine(pen, rect_box.X + rect_box.Width, rect_box.Y, rect_box.X, rect_box.Y + rect_box.Height);
				}
			}
		}
		public void PrintLeftCheckBoxD(Graphics graph, string text, float x, float y, float width, float height, float text_offset, int box_count, int distance, StringAlignment alignment, Font font, Brush brush, Pen pen, bool test)
		{
			RectangleF rect_box = new RectangleF(x + text_offset, y + text_offset, height - text_offset * 2, height - text_offset * 2);
			RectangleF rect_text = new RectangleF(x + height, y, width, height);
			StringFormat strFormat = new StringFormat();
			strFormat.Alignment		= alignment;
			strFormat.LineAlignment = StringAlignment.Center;
			strFormat.Trimming		= StringTrimming.Word;
			if(!test) graph.DrawString(text, font, brush, rect_text, strFormat);
			for(int i = 0 ; i < box_count; i++)
			{
				if(!test)
				{
					graph.DrawLine(pen, rect_box.X + rect_box.Width / 2, rect_box.Y, rect_box.X + rect_box.Width, rect_box.Y + rect_box.Height);
					graph.DrawLine(pen, rect_box.X + rect_box.Width, rect_box.Y + rect_box.Height, rect_box.X, rect_box.Y + rect_box.Height);
					graph.DrawLine(pen, rect_box.X + rect_box.Width / 2, rect_box.Y, rect_box.X, rect_box.Y + rect_box.Height);
				}
			}
		}
		public void PrintLeftCheckBoxO(Graphics graph, string text, float x, float y, float width, float height, float text_offset, int box_count, int distance, StringAlignment alignment, Font font, Brush brush, Pen pen, bool test)
		{
			RectangleF rect_box = new RectangleF(x + text_offset, y + text_offset, height - text_offset * 2, height - text_offset * 2);
			RectangleF rect_text = new RectangleF(x + height, y, width, height);
			StringFormat strFormat = new StringFormat();
			strFormat.Alignment		= alignment;
			strFormat.LineAlignment = StringAlignment.Center;
			strFormat.Trimming		= StringTrimming.Word;
			if(!test) graph.DrawString(text, font, brush, rect_text, strFormat);
			for(int i = 0 ; i < box_count; i++)
			{
				if(!test)
				{
					graph.DrawEllipse(pen, rect_box);
				}
			}
		}
		public void PrintRightCheckBoxO(Graphics graph, string text, float x, float y, float width, float height, float text_offset, int box_count, int distance, StringAlignment alignment, Font font, Brush brush, Pen pen, bool test)
		{
			RectangleF rect_box = new RectangleF(x + text_offset + width, y + text_offset, height - text_offset * 2, height - text_offset * 2);
			RectangleF rect_text = new RectangleF(x + height, y, width, height);
			StringFormat strFormat = new StringFormat();
			strFormat.Alignment		= alignment;
			strFormat.LineAlignment = StringAlignment.Center;
			strFormat.Trimming		= StringTrimming.Word;
			if(!test) graph.DrawString(text, font, brush, rect_text, strFormat);
			for(int i = 0 ; i < box_count; i++)
			{
				if(!test)
				{
					graph.DrawEllipse(pen, rect_box);
				}
			}
		}
		public void PrintRightCheckBoxOChecked(Graphics graph, string text, float x, float y, float width, float height, float text_offset, int box_count, int distance, StringAlignment alignment, Font font, Brush brush, Pen pen, bool test)
		{
			RectangleF rect_box = new RectangleF(x + text_offset + width, y + text_offset, height - text_offset * 2, height - text_offset * 2);
			RectangleF rect_box_inner = new RectangleF(x + text_offset * 2 + width, y + text_offset * 2, height - text_offset * 4, height - text_offset * 4);
			RectangleF rect_text = new RectangleF(x + height, y, width, height);
			StringFormat strFormat = new StringFormat();
			strFormat.Alignment		= alignment;
			strFormat.LineAlignment = StringAlignment.Center;
			strFormat.Trimming		= StringTrimming.Word;
			if(!test) graph.DrawString(text, font, brush, rect_text, strFormat);
			for(int i = 0 ; i < box_count; i++)
			{
				if(!test)
				{
					graph.DrawEllipse(pen, rect_box);
					graph.FillEllipse(brush, rect_box_inner);
				}
			}
		}

		public void PrintTextBox(Graphics graph, string text, float x, float y, float width, float height, float text_offset, StringAlignment alignment, StringAlignment lineAlignment, Font font, Brush brush, Pen pen, bool test)
		{
			RectangleF rect_box = new RectangleF(x, y, width, height);
			PrintTextNoBox(graph, text, x, y, width, height, text_offset, alignment, lineAlignment, font, brush, pen, test);
			if (!test) graph.DrawRectangle(pen, rect_box.X, rect_box.Y, rect_box.Width, rect_box.Height);
		}

		public void PrintTextNoBox(Graphics graph, string text, float x, float y, float width, float height, float text_offset, StringAlignment alignment, StringAlignment lineAlignment, Font font, Brush brush, Pen pen, bool test)
		{
			RectangleF rect_text = new RectangleF(x + text_offset, y, width - text_offset, height);
			StringFormat strFormat = new StringFormat();
			strFormat.Alignment		= alignment;
			strFormat.LineAlignment = lineAlignment;
			strFormat.Trimming		= StringTrimming.Word;
			if(!test) graph.DrawString(text, font, brush, rect_text, strFormat);
		}

		public void PrintTextNoBox(Graphics graph, string text, RectangleF rect, float text_offset, StringAlignment alignment, StringAlignment lineAlignment, Font font, Brush brush, bool test)
		{
			RectangleF rect_text = new RectangleF(rect.X + text_offset, rect.Y, rect.Width - text_offset, rect.Height);
			StringFormat strFormat = new StringFormat();
			strFormat.Alignment		= alignment;
			strFormat.LineAlignment = lineAlignment;
			strFormat.Trimming		= StringTrimming.Word;
			if(!test) graph.DrawString(text, font, brush, rect_text, strFormat);
		}

		public virtual void  PrintPage(Graphics graph, int page)
		{

		}

 		#region Многостраничная печать
		// Реализация многостраничной печати
		// Путем разбиения документа на неделимые блоки, с реализацией их печати и измерения
		public delegate int DelegatePrintBlock(Graphics graph, int offset, bool test, bool print, object print_data);
		public virtual int PrintStandartHead(Graphics graph, int offset, bool test, bool print, object print_data)
		{
			// Печать заголовка на каждой последующей странице
			return offset;
		}
		protected int PrintBlock(Graphics graph, int offset, DelegatePrintBlock printing, object print_data)
		{
			int y;	// Для контроля положения на странице

			// Осуществление печати блока через внешнюю функцию
			// Тестируем возможность попадания блока на страницу целиком
			y		= offset;
			y		= printing(graph, y, true, false, print_data);	// Получаем виртуальное окончание блока на странице
			if(y > page_max_y)
			{
				// Блок на страницу целиком не помещается, начинаем печать со следующей
				y = page_min_y;		// Положение в начало страницы
				page_current ++;	// Текущая страница увеличивается на единицу
				
				if(page_current == page_to_print)
				{
					// Текущая страница совпадает с печатаемой в данный момент
					// Производим печать
					y		= PrintStandartHead(graph, y, false, true, null);
					y		= printing(graph, y, false, true, print_data);
				}
				else
				{
					// Текущая страница не совпадает с печатаемой в данный момент
					// Производим невидимую печать, давая реальное смещение
					y		= PrintStandartHead(graph, y, false, false, null);
					y		= printing(graph, y, false, false, print_data);
				}
				return y;	// Возврат текущего смещения на странице
			}
			// Осуществляем печать на той-же странице
			y		= offset;
			if(page_current == page_to_print)
			{
				// Текущая страница совпадает с печатаемой в данный момент
				// Производим печать
				y		= printing(graph, y, false, true, print_data);
			}
			else
			{
				// Текущая страница не совпадает с печатаемой в данный момент
				// Производим невидимую печать, давая реальное смещение
				y		= printing(graph, y, false, false, print_data);
			}
			return y;
		}
		protected int PrintFooter(Graphics graph, int offset, DelegatePrintBlock printing, object print_data)
		{
			int y;	// Для контроля положения на странице

			// Осуществление печати блока через внешнюю функцию
			// Тестируем возможность попадания блока на страницу целиком
			y		= offset;
			y		= printing(graph, y, true, false, print_data);	// Получаем виртуальное окончание блока на странице
			if(y > page_max_y)
			{
				// Блок на страницу целиком не помещается, начинаем печать со следующей
				y = page_min_y;		// Положение в начало страницы
				page_current ++;	// Текущая страница увеличивается на единицу
				
				if(page_current == page_to_print)
				{
					// Текущая страница совпадает с печатаемой в данный момент
					// Производим печать
					y		= printing(graph, y, false, true, print_data);
				}
				else
				{
					// Текущая страница не совпадает с печатаемой в данный момент
					// Производим невидимую печать, давая реальное смещение
					y		= printing(graph, y, false, false, print_data);
				}
				return y;	// Возврат текущего смещения на странице
			}
			else
			{
				offset += page_max_y - y;
			}
			// Осуществляем печать на той-же странице
			y		= offset;
			if(page_current == page_to_print)
			{
				// Текущая страница совпадает с печатаемой в данный момент
				// Производим печать
				y		= printing(graph, y, false, true, print_data);
			}
			else
			{
				// Текущая страница не совпадает с печатаемой в данный момент
				// Производим невидимую печать, давая реальное смещение
				y		= printing(graph, y, false, false, print_data);
			}
			return y;
		}
		protected int PrintBlockWithHeader(Graphics graph, int offset, DelegatePrintBlock printing, object print_data, DelegatePrintBlock printing_head, bool first)
		{
			int y;	// Для контроля положения на странице
			if(first == true) block_head_print = false;

			// Осуществление печати блока через внешнюю функцию
			// Тестируем возможность попадания блока на страницу целиком
			y		= offset;
			if(block_head_print == false)
			{
				y = printing_head(graph, y, true, false, print_data);// Осуществляем печать заголовка, сли он не был напечатан
			}
			y		= printing(graph, y, true, false, print_data);	// Получаем виртуальное окончание блока на странице
			if(y > page_max_y)
			{
				// Блок на страницу целиком не помещается, начинаем печать со следующей
				y = page_min_y;				// Положение в начало страницы
				page_current ++;			// Текущая страница увеличивается на единицу
				block_head_print	= false;// В начале следующей странице нужен заголовок блока
				
				if(page_current == page_to_print)
				{
					// Текущая страница совпадает с печатаемой в данный момент
					// Производим печать
					y	= PrintStandartHead(graph, y, false, true, null);
					if(block_head_print == false)
					{
						y = printing_head(graph, y, false, true, print_data);// Осуществляем печать заголовка, сли он не был напечатан
						block_head_print = true;
					}
					y		= printing(graph, y, false, true, print_data);
				}
				else
				{
					// Текущая страница не совпадает с печатаемой в данный момент
					// Производим невидимую печать, давая реальное смещение
					y	= PrintStandartHead(graph, y, false, false, null);
					if(block_head_print == false)
					{
						y = printing_head(graph, y, false, false, print_data);// Осуществляем печать заголовка, сли он не был напечатан
						block_head_print = true;
					}
					y		= printing(graph, y, false, false, print_data);
				}
				return y;	// Возврат текущего смещения на странице
			}
			// Осуществляем печать на той-же странице
			y		= offset;
			if(page_current == page_to_print)
			{
				// Текущая страница совпадает с печатаемой в данный момент
				// Производим печать
				if(block_head_print == false)
				{
					y = printing_head(graph, y, false, true, print_data);// Осуществляем печать заголовка, сли он не был напечатан
					block_head_print = true;
				}
				y		= printing(graph, y, false, true, print_data);
			}
			else
			{
				// Текущая страница не совпадает с печатаемой в данный момент
				// Производим невидимую печать, давая реальное смещение
				if(block_head_print == false)
				{
					y = printing_head(graph, y, false, false, print_data);// Осуществляем печать заголовка, сли он не был напечатан
					block_head_print = true;
				}
				y		= printing(graph, y, false, false, print_data);
			}
			return y;
		}
		#endregion
	}
}
