using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Printing;
using System.Drawing;
using System.Windows.Forms;

namespace AutoBaseSql
{
	public class PrintCommon
	{
		const int PAGEMAX_Y = 287;
		const int PAGEMIN_Y = 10;

		public enum TEST_LVL : short { NOTEST = 0, SIMPLETEST = 1 };
		public enum ALIGN_X : short { LEFT = 1, CENTER = 2, RIGHT = 3 };

		private Font _selectedFont; // Выбранный в данный момент Font
		private Brush _selectedBrush; // выбранный в данный Brush
		private Pen _selectedPen; // Выбранный в данный момент Pen
		private StringFormat _selectedStringFormat; // Выбранный в данный момент StringFormat
		private Graphics _selectedGraphics; // Выбранный в данный момент элемент Graphics
		private float _selectedTextBoxOffsetX; // Выбранный оступ от границы окружающего тест прямоугольника по горизонтали
		private TEST_LVL _selectedTestLevel; // Выбранный уровеь тестирования

		private StringAlignment _tmpAlignmentX;

		private bool _pageHeadPrinted;
		private int _currentPageToPrint;
		private int _currentPageRun;
		private float _currentPageY;
		private float _maxPage;

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
		public Graphics GetGraph()
		{
			return _selectedGraphics;
		}
		public void SelectTextBoxOffsetX(float srcTextBoxOffset)
		{
			_selectedTextBoxOffsetX = srcTextBoxOffset;
		}
		public void SetStringFormat(StringAlignment srcAlignment, StringAlignment srcLineAlignment)
		{
			if (_selectedStringFormat == null)
				_selectedStringFormat = new StringFormat();
			_selectedStringFormat.Alignment = srcAlignment;
			_selectedStringFormat.LineAlignment = srcLineAlignment;
			_selectedStringFormat.Trimming = StringTrimming.Word;
		}
		public void SetTmpAlignmentX(ALIGN_X srcAlign)
		{
			if (_selectedStringFormat == null) return;
			_tmpAlignmentX = _selectedStringFormat.Alignment;
            switch (srcAlign)
            {
				case ALIGN_X.LEFT:
					_selectedStringFormat.Alignment = StringAlignment.Near;
					return;
				case ALIGN_X.RIGHT:
					_selectedStringFormat.Alignment = StringAlignment.Far;
					return;
				case ALIGN_X.CENTER:
					_selectedStringFormat.Alignment = StringAlignment.Center;
					return;
				default:
					break;

			}
		}
		public void ReturnTmpAlignmentX()
        {
			if (_selectedStringFormat == null) return;
			_selectedStringFormat.Alignment = _tmpAlignmentX;
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
			RectangleF rect_text = new RectangleF(x + _selectedTextBoxOffsetX, y, width - _selectedTextBoxOffsetX * 2, height);
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
			bool tmpAlignment = false;
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
				width = (float)Convert.ToDecimal(element[1]);
				if(element.Length > 2)
				{
					tmpAlignment = true;
					SetTmpAlignmentX((ALIGN_X)element[2]);
				}
				SelectedPrintTextBox(txt, x, y, width, height);
				if (tmpAlignment) ReturnTmpAlignmentX();
				x += width;
			}
			return height;
		}
		public float SelectedPrintTableGroupMerge(object[] tableData, object[] tableData2,  float x, float y, float height)
		{
			float heightCol,heightCol1 = 0.0F, heightCol2 = 0.0F, heightCol1Merge= 0.0F;
			TEST_LVL level = SimpleTestOn();
			string txt;
			float width;
			bool tmpAlignment = false;
			int count_row1 = 0, count_row2 = 0;
			float tmpX = x;
			foreach (object[] element in tableData2) // Вторая строка -измерение
			{
				txt = (string)element[0];
				width = (float)Convert.ToDecimal(element[1]);
				heightCol = SelectedPrintTextBox(txt, x, y, width, height);
				if (heightCol > height) height = heightCol;
				if (heightCol > heightCol2) heightCol2 = heightCol;
				count_row2++;
			}
			foreach (object[] element in tableData) // Первая строка - измерение
			{
				txt = (string)element[0];
				width = (float)Convert.ToDecimal(element[1]);
				heightCol = SelectedPrintTextBox(txt, x, y, width, height);
				if (heightCol > height) height = heightCol;
				count_row1++;
				if (count_row2 >= count_row1)
                {
					if (heightCol > heightCol1) heightCol1 = heightCol;
				}
                else
                {
					if (heightCol > heightCol1Merge) heightCol1Merge = heightCol;
				}
			}
			height = heightCol1 + heightCol2;
			if (heightCol1Merge > height)
			{
				height = heightCol1Merge;
				heightCol2 = height - heightCol1;
			}
			else
				heightCol1Merge = height;
			SetTestLvl(level);
			if (_selectedTestLevel != TEST_LVL.NOTEST) return height;
			int count = 0;
			foreach (object[] element in tableData)
			{
				count++;
				txt = (string)element[0];
				width = (float)Convert.ToDecimal(element[1]);
				if (element.Length > 2)
				{
					tmpAlignment = true;
					SetTmpAlignmentX((ALIGN_X)element[2]);
				}
				if(count <= count_row2)
					SelectedPrintTextBox(txt, tmpX, y, width, heightCol1);
				else
					SelectedPrintTextBox(txt, tmpX, y, width, heightCol1Merge);
				if (tmpAlignment) ReturnTmpAlignmentX();
				tmpX += width;
			}
			y += heightCol1;
			tmpX = x;
			foreach (object[] element in tableData2)
			{
				count++;
				txt = (string)element[0];
				width = (float)Convert.ToDecimal(element[1]);
				if (element.Length > 2)
				{
					tmpAlignment = true;
					SetTmpAlignmentX((ALIGN_X)element[2]);
				}
				SelectedPrintTextBox(txt, tmpX, y, width, heightCol2);
				if (tmpAlignment) ReturnTmpAlignmentX();
				tmpX += width;
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
			if (_selectedTestLevel != TEST_LVL.NOTEST) return;
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

		public PrintCommon()
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

		#region Новая организация печати
		public void PrintPreview(bool previewFlag = false) // Основной метод для запуска печати через диалог предварительного просмотра
		{
			try
			{
				try
				{
					// Настройка счетчиков для страниц
					_currentPageToPrint = 1;
					_pageHeadPrinted = false;
					_currentPageY = PAGEMIN_Y;
					_maxPage = 1;
					// Свойтсва принтера
					PrintDocument pd = new PrintDocument();
					pd.DefaultPageSettings.Landscape = false;
					pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);
					if (previewFlag)
					{
						// Аечать с предпросмотром
						PrintPreviewDialog preview = new PrintPreviewDialog();
						preview.Document = pd;
						preview.ShowDialog();
					}
					else
					{
						// Печать сразу на принтер по умолчанию
						pd.Print();
					}
				}
				finally
				{
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void pd_PrintPage(object sender, PrintPageEventArgs ev)
		{
			_selectedGraphics = ev.Graphics;
			_selectedGraphics.PageUnit = GraphicsUnit.Millimeter; // Печать в милиметрах
		
			// Печать
			_currentPageRun = 1;
			_currentPageY = PAGEMIN_Y;
			_pageHeadPrinted = false;
			PrintDocument();
			// Конец печать
			if(_currentPageToPrint == _maxPage)
            {
				ev.HasMorePages = false; // Закрыли процедуру печати
            }
            else
            {
				ev.HasMorePages = true;
				_currentPageToPrint++;
            }
		}
		public virtual void PrintDocument() // Виртуальная функция печати всего документа целиком переопределяется в наследуемых классах печати
		{

		}
		protected void PrintBlockHead(DelegatePrintBlock printing, object print_data, DelegatePrintBlock printingHead = null, bool firstRow = false, int outCounter = 0)
		{
			float y;  // Для контроля положения на странице
					  // Печать стандартного заголовка страницы
			if (_pageHeadPrinted == false)
			{
				TestOff();
				_pageHeadPrinted = true;
				_currentPageY = PrintStandartHead(_currentPageY);
			}
			// Осуществление печати блока через внешнюю функцию
			// Тестируем возможность попадания блока на страницу целиком
			SimpleTestOn();
			// Печать заголовка
			y = _currentPageY;
			if (printingHead != null && firstRow)
            {
				y = printingHead(y, null);
            }
			y = printing(y, print_data);    // Получаем виртуальное окончание блока на странице
			if (y <= PAGEMAX_Y) // Целиком помещается на страницу
			{
				if (_currentPageRun == _currentPageToPrint)
				{
					TestOff();
					if (printingHead != null && firstRow)
					{
						_currentPageY = printingHead(_currentPageY, null);
					}
					_currentPageY = printing(_currentPageY, print_data, outCounter);
				}
				else
				{
					_currentPageY = y;
				}
				return;
			}
			// Переход страницы
			_pageHeadPrinted = false;
			_currentPageY = PAGEMIN_Y; // Начинаем в верха страницы
			_currentPageRun++; // Увеличиваем счетчик пробегаемой страцицы на 1
			if (_currentPageRun > _maxPage) _maxPage = _currentPageRun;
			PrintBlockHead(printing, print_data, printingHead, true, outCounter); // Опять пробуем печатать
		}
		public delegate float DelegatePrintBlock(float start_y, object print_data, int coutCounter = 0);
		public virtual float PrintStandartHead(float y) { return y; }
		public virtual float PrintDocumentTitle(float y) { return y; }
		#endregion

	}
}
