using System.Drawing;
using System;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using AutoBaseSql.Print;

namespace AutoBaseSql.Print
{
	// Класс описывает базовые методы для рисования в объекте Graphiscs
    public class PrintDrawingBase
    {
		const string PICTURE_PATH = ""; // "\\Pictrures\\";
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

		public PrintDrawingBase()
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

		#region Работа через устанолвенные настройки
		public TEST_LVL SetTestLvl(TEST_LVL srcTestLvl)
		{
			TEST_LVL previous = _selectedTestLevel;
			_selectedTestLevel = srcTestLvl;
			return previous;
		}
		public TEST_LVL TestOn()
		{
			return SetTestLvl(TEST_LVL.SIMPLETEST);
		}
		public TEST_LVL TestOff()
		{
			return SetTestLvl(TEST_LVL.NOTEST);
		}
		public bool TestFlag()
        {
			if (_selectedTestLevel != TEST_LVL.NOTEST)
				return true;
			else
				return false;
        }
		public bool NoTestFlag()
		{
			if (_selectedTestLevel == TEST_LVL.NOTEST)
				return true;
			else
				return false;
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
			// Внимательно изучить данный метод!!!!
			float heightCol1;
			float heightCol2;
			TEST_LVL level = TestOn();
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
			// Внимательно изучть данный метод!!!
			float heightCol1;
			float heightCol2;
			TEST_LVL level = TestOn();
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
			// Внимательно изучить данный метод!
			float heightCol;
			TEST_LVL level = TestOn();
			string txt;
			float width;
			bool tmpAlignment = false;
			foreach (object[] element in tableData)
			{
				if (element != null)
				{
					txt = (string)element[0];
					width = (float)Convert.ToDecimal(element[1]);
					heightCol = SelectedPrintTextBox(txt, x, y, width, height);
					if (heightCol > height) height = heightCol;
				}
			}
			SetTestLvl(level);
			if (_selectedTestLevel != TEST_LVL.NOTEST) return height;
			foreach (object[] element in tableData)
			{
				if (element != null)
				{
					txt = (string)element[0];
					width = (float)Convert.ToDecimal(element[1]);
					if (element.Length > 2)
					{
						tmpAlignment = true;
						SetTmpAlignmentX((ALIGN_X)element[2]);
					}
					SelectedPrintTextBox(txt, x, y, width, height);
					if (tmpAlignment) ReturnTmpAlignmentX();
					x += width;
				}
			}
			return height;
		}
		public float SelectedPrintTableGroupMerge(object[] tableData, object[] tableData2, float x, float y, float height)
		{
			// Внимательно изучить данный метод!
			float heightCol, heightCol1 = 0.0F, heightCol2 = 0.0F, heightCol1Merge = 0.0F;
			TEST_LVL level = TestOn();
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
				if (count <= count_row2)
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


		#region Вспомогательные методы по блоками
		public float PrintSign(string title, string fio, float x, float y, float width, float height)
		{
			// Формируем строку
			if (fio.Length == 0) fio = "_____________________________";
			string text = title + "_____________________" + "/" + fio + "/";
			return PrintText(text, x, y, width, height);
		}
		public float PrintSignRight(string title, string fio, float x, float y, float width, float height)
		{
			StringFormat stringFormat = _selectedStringFormat;
			_selectedStringFormat.LineAlignment = StringAlignment.Far;
			y = PrintSign(title, fio, x, y, width, height);
			_selectedStringFormat = stringFormat;
			return y;
		}
		#endregion

		#region Перереализация примитивов с переименованием
		// Измерение высоты требуемой для печати строки в заданную ширину с учетом переноса
		public SizeF MeasureText(string text, SizeF printAreaSize)
		{
			SizeF size = new SizeF(printAreaSize.Width, 240);
			size = _selectedGraphics.MeasureString(text, _selectedFont, size /*printAreaSize*/, _selectedStringFormat);
			if (size.Height > printAreaSize.Height) printAreaSize.Height = size.Height;
			return printAreaSize;
		}

		// Печать заданного текста  в заданной позиции с возвратом размера по высоте требуемого места
		public float PrintText(string text, float x, float y, float width, float height = 0)
		{
			SizeF printAreaSize = new SizeF(width - _selectedTextBoxOffsetX * 2, height);
			printAreaSize = MeasureText(text, printAreaSize);
			//if (size.Height > height) height = size.Height;

			//if (_selectedTestLevel != TEST_LVL.NOTEST) return height;
			if (NoTestFlag())
			{
				//RectangleF rect_text = new RectangleF(x + _selectedTextBoxOffsetX, y, width - _selectedTextBoxOffsetX * 2, height);
				RectangleF printArea = new RectangleF(new PointF(x, y), printAreaSize);
				_selectedGraphics.DrawString(text, _selectedFont, _selectedBrush, printArea, _selectedStringFormat);
			}
			return printAreaSize.Height;
		}

		public float PrintTextBox(string text, float x, float y, float width, float height)
		{
			height = PrintText(text, x, y, width, height);
			if (_selectedTestLevel != TEST_LVL.NOTEST) return height;
			RectangleF rect_box = new RectangleF(x, y, width, height);
			_selectedGraphics.DrawRectangle(_selectedPen, rect_box.X, rect_box.Y, rect_box.Width, rect_box.Height);
			return height;
		}

		public float PrintBox(float x, float y, float width, float height)
		{
			if (_selectedTestLevel == TEST_LVL.NOTEST)
			{
				RectangleF rect_box = new RectangleF(x, y, width, height);
				_selectedGraphics.DrawRectangle(_selectedPen, rect_box.X, rect_box.Y, rect_box.Width, rect_box.Height);
			}
			return height;
		}

		// Печать строковых данных в табличном виде
		public float PrintTableGroup(object[] tableData, float x, float y, float height)
		{
			// Внимательно изучить данный метод!
			float heightCol;
			TEST_LVL level = TestOn();
			string txt;
			float width;
			bool tmpAlignment = false;
			foreach (object[] element in tableData)
			{
				if (element != null)
				{
					txt = (string)element[0];
					width = (float)Convert.ToDecimal(element[1]);
					heightCol = PrintTextBox(txt, x, y, width, height);
					if (heightCol > height) height = heightCol;
				}
			}
			SetTestLvl(level);
			if (_selectedTestLevel != TEST_LVL.NOTEST) return height;
			foreach (object[] element in tableData)
			{
				if (element != null)
				{
					txt = (string)element[0];
					width = (float)Convert.ToDecimal(element[1]);
					if (element.Length > 2)
					{
						tmpAlignment = true;
						SetTmpAlignmentX((ALIGN_X)element[2]);
					}
					PrintTextBox(txt, x, y, width, height);
					if (tmpAlignment) ReturnTmpAlignmentX();
					x += width;
				}
			}
			return height;
		}

		public float PrintImage(string shotrFileName, float x, float y, float width, float height)
		{
			if (_selectedTestLevel != TEST_LVL.NOTEST) return height;
			RectangleF rect = new RectangleF(x, y, width, height);
			string fileName = PICTURE_PATH + shotrFileName;
			Image image;
			try
			{
				image = Image.FromFile(fileName);
			}
			catch (Exception e)
			{
				Db.SetException(e);
				Db.ShowFaults();
				return 0 ;     // Файл не найден
			}
			if (image == null) return 0; // Файл нулевой
			_selectedGraphics.DrawImage(image, rect);
			return height;
		}
		#endregion
	}
}
