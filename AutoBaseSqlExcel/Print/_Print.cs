using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using AutoBaseSql.Print;

namespace AutoBaseSql
{
	/// <summary>
	/// Класс для организации блочной постраничной печати : класс-печатальщик
	/// </summary>
	public class _Print
	{
		const int PAGEMAX_Y = 277; // Верхняя граница в мм страницы
		const int PAGEMIN_Y = 10; // Нижняя граница в мм страницы


		private bool _pageHeadPrinted;		// Флаг напечатан заголовок сраницы или еще нет
		private int _currentPageToPrint;	// Счетчик, какач траницв должна печататься в данный момент
		private int _currentPageRun;		// Счетчик какая траница сейчас пробегается при виртуальной печати
		private float _currentPageY;		// Позиция на странице Y с которой сечас должны печатать
		private float _maxPage;				// Максимальное количество страниц

		public DocumentPrintSchema _printSchema;	// Блок-Схема для печати
		public PrintDocumentBase _printDocument;	// Класс пердаставляющий печатаемый документ

		public _Print()
		{

		}

		protected void TestOn() // Включаем тест в классе печатальщике
		{
			_printDocument.GetPrintTool().TestOn();

		}
		protected void TestOff() // Выключаем тест в классе печатальщике
		{
			_printDocument.GetPrintTool().TestOff();
		}
		private float PrintSchemaElement(DocumentPrintSchemaElement element, float positionY) // Печать единичного элемента схемы
		{
			if (element.BlockHeadDelegate != null && element.FirstElementFlag)
			{
				// Печать заголовка , если флаг элемента первый и есть функция его печати
				positionY = element.BlockHeadDelegate(positionY, null);
			}
			positionY = element.BlockDelegate(positionY, element.Element, element.Count);
			return positionY;
		}
		public void Print(PrintDocumentBase printing, bool previewFlag = false) // Основной метод для запуска печати через диалог предварительного просмотра
		{
			try
			{
				//_printBase = new PrintDrawingBase();
				_printDocument = printing;
				DocumentPrintSchema printSchema = printing.CreatePrintingSchema();
				if (printSchema == null) return; // Схема печати не загружена, ничего не печатаем
				// Основная схема печати документа 
				_printSchema = printSchema;

				// Настройка счетчиков
				_currentPageToPrint = 1;	// Для страниц - печать начинаем с первой страницы
				_maxPage = 1;				// Максимальная страница - изначально предполагаем что всего одна

				// Настройка документа печати
				PrintDocument pd = new PrintDocument();
				pd.DefaultPageSettings.Landscape = false;
				pd.PrintPage += new PrintPageEventHandler(PrintPage);	// Обработчик постраничной печати
				if (previewFlag)
				{
                    // Печать с предпросмотром
                    PrintPreviewDialog preview = new PrintPreviewDialog
                    {
                        Document = pd
                    };
                    preview.ShowDialog();
				}
				else
				{
					// Печать сразу на принтер по умолчанию
					pd.Print();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			finally
			{
			}
		}

		private void PrintPage(object sender, PrintPageEventArgs ev) // Переопределенный обработчик события PrintPage стандарного NET объекта PrintDocument
		{
			ev.Graphics.PageUnit = GraphicsUnit.Millimeter; // Печать в милиметрах
			_printDocument.SetPrintToolGraphics(ev.Graphics);

			// Печать
			_currentPageRun = 1;
			_currentPageY = PAGEMIN_Y;
			_pageHeadPrinted = false;

			// Пробуем инкапсулировать печать		
			foreach (DocumentPrintSchemaElement schema in _printSchema.SchemaElements)
			{
				PrintBlockHead(schema);
			}
			// Конец пробы инкапсулирования печати
	
			if (_currentPageToPrint == _maxPage)
			{
				ev.HasMorePages = false; // Закрыли процедуру печати
			}
			else
			{
				ev.HasMorePages = true;
				_currentPageToPrint++;
			}
		}

		private void PrintBlockHead(DocumentPrintSchemaElement printSchemaElement)
		{
			float y;  // Для контроля положения на странице
			// Печать стандартного заголовка страницы
			if (_pageHeadPrinted == false)
			{
				TestOff();
				_pageHeadPrinted = true;
				if(_printSchema.PrintPageHeader != null)
					_currentPageY = _printSchema.PrintPageHeader(_currentPageY, null); // PrintStandartHead(_currentPageY);
			}
			// Осуществление печати блока через внешнюю функцию
			// Тестируем возможность попадания блока на страницу целиком
			TestOn(); // Включение режима тестирования
			y = PrintSchemaElement(printSchemaElement, _currentPageY);

			if (y <= PAGEMAX_Y) // Целиком помещается на страницу
			{
				if (_currentPageRun == _currentPageToPrint)
				{
					TestOff();
					_currentPageY = PrintSchemaElement(printSchemaElement, _currentPageY);
				}
				else
				{
					_currentPageY = y;
				}
			}
			else
			{
				// Переход страницы
				_pageHeadPrinted = false;
				_currentPageY = PAGEMIN_Y; // Начинаем в верха страницы
				_currentPageRun++; // Увеличиваем счетчик пробегаемой страцицы на 1
				if (_currentPageRun > _maxPage) _maxPage = _currentPageRun;
				PrintBlockHead(printSchemaElement); // Опять пробуем печатать
			}
		}
	
		public delegate float DelegatePrintBlock(float start_y, object print_data, int coutCounter = 0);

	}
}
