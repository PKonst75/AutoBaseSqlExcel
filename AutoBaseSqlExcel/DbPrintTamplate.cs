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
	/// Класс для реализации печати файловых шаблонов.
	/// </summary>
	public class DbPrintTamplate:DbPrint
	{
		string file_name = "";

		#region Структура шаблона для хранения в памяти
		protected class TemplateStruct
		{
			string	block_name			= "";		// Имя блока
			int		block_type			= 0;		// Тип блока
			float	block_height		= 0.0F;		// Высота блока, если блок статический
			bool	flag_block_height	= false;	// Флаг, что высота блока рассчитана

			public TemplateStruct(string name, int type)
			{
				block_name	= name;
				block_type	= type;
			}
		}
		ArrayList template_struct = null;
		// Реализация печати из шаблонов
		// Список шрифтов
		protected class TemplateFonts:DictionaryBase
		{
			public void Add(string key, Font font)
			{
				this.Dictionary.Add(key, (object)font); 
			}
			public Font this[string key]
			{
				get {return (Font)this.Dictionary[key];}
			}
			public bool Contains(string key)
			{
				return this.Dictionary.Contains(key);
			}
		}
		TemplateFonts	tamplate_fonts		= null;
		// Список форматов строк
		protected class TemplateStringFormats:DictionaryBase
		{
			public void Add(string key, StringFormat stringformat)
			{
				this.Dictionary.Add(key, (object)stringformat); 
			}
			public StringFormat this[string key]
			{
				get {return (StringFormat)this.Dictionary[key];}
			}
			public bool Contains(string key)
			{
				return this.Dictionary.Contains(key);
			}
		}
		TemplateStringFormats	template_stringformats		= null;
		// Список кистей
		protected class TemplateBrushes:DictionaryBase
		{
			public void Add(string key, Brush brush)
			{
				this.Dictionary.Add(key, (object)brush); 
			}
			public Brush this[string key]
			{
				get {return (Brush)this.Dictionary[key];}
			}
			public bool Contains(string key)
			{
				return this.Dictionary.Contains(key);
			}
		}
		TemplateBrushes	template_brushes		= null;
		// Список блоков
		protected class TemplateBlocks:DictionaryBase
		{
			public void Add(string key, ArrayList strings)
			{
				this.Dictionary.Add(key, (object)strings); 
			}
			public ArrayList this[string key]
			{
				get {return (ArrayList)this.Dictionary[key];}
			}
			public bool Contains(string key)
			{
				return this.Dictionary.Contains(key);
			}
		}
		TemplateBlocks	template_blocks		= null;
		#endregion

		public DbPrintTamplate(string file)
		{
			file_name	= file;
		}

		// Реализация чтения и заполнения структуры шаблона из файла
		protected void ReadTemplate(string file)
		{
			// Чтение шаблона из файла
			StreamReader reader = null;
			reader = new StreamReader(file, System.Text.Encoding.GetEncoding(1251));	// Открываем файл для чтения
			

			string s = reader.ReadLine();		// Читаем из файла строку
			while(s != null)
			{
				char[] separators = new Char[] {'\t'};
				string[] ssplit = s.Split(separators);
				bool block = AnalizeTemplateLine(ssplit);
				if(block == true)
				{
					// Необходимо зачитать из файла блок печати и распечатать его
					// Читаем блок
					ArrayList strs = null;
					string str = reader.ReadLine();
					while (str != null && str != "#BLOCKEND")
					{
						if(strs == null) strs = new ArrayList();
						strs.Add(str);
						str = reader.ReadLine();
					}
					// Добавляем блок в список и дополняем структуру документа
					MakeTemplateBlock(ssplit, strs);
				}
				s = reader.ReadLine();
			}
			reader.Close();						// Закрываем файл
		}

		// Основаная функция - для печати чистого шаблона
		public override void  PrintPage(Graphics graph, int page)
		{
			// Для ориентации на странице
			PrintTemplate(graph, file_name);
		}
		// Реализация схемы печати из фала по шаблонам
		protected void PrintTemplate(Graphics graph, string file)
		{
			// Чтение шаблона из файла
			StreamReader reader = null;
			int offset			= 20;			// Певроначальный отступ
			reader = new StreamReader(file, System.Text.Encoding.GetEncoding(1251));	// Открываем файл для чтения
			

			string s = reader.ReadLine();		// Читаем из файла строку
			while(s != null)
			{
				char[] separators = new Char[] {'\t'};
				string[] ssplit = s.Split(separators);
				bool block = AnalizeTemplateLine(ssplit);
				if(block == true)
				{
					// Необходимо зачитать из файла блок печати и распечатать его
					// Читаем блок
					ArrayList strs = null;
					string str = reader.ReadLine();
					while (str != null && str != "#BLOCKEND")
					{
						if(strs == null) strs = new ArrayList();
						strs.Add(str);
						str = reader.ReadLine();
					}
					// Запускаем процедуру печати блока
					offset = PrintBlock(graph, offset, new DelegatePrintBlock(PrintTemplateBlock), (object)strs);
				}
				s = reader.ReadLine();
			}
			reader.Close();						// Закрываем файл
		}
		protected int PrintTemplateBlock(Graphics graph, int offset, bool test, bool print, object o)
		{
			ArrayList list = (ArrayList)o;
			int width = 0;
			foreach(string s in list)
			{
				// Анализируем и обрабатываем каждый элемент блока
				char[] separators = new Char[] {'\t'};
				string[] ssplit = s.Split(separators);
				if(test)
				{
					// Тестируем каждую линию блока
					width += TestTemplateBlockLine(graph, ssplit);
				}
				else
				{
					if(print == false)
					{
						width += TestTemplateBlockLine(graph, ssplit);
					}
					else
					{
						offset += PrintTemplateBlockLine(graph, offset, ssplit);
					}
				}
			}
			return offset + width;
		}
		protected int TestTemplateBlockLine(Graphics graph, string[] line)
		{
			if(line.Length < 1) return 0;		// Некорректная строчка обработке не подлежит
			string mask	= line[0];				// Первый элемент строки шаблона - тип записи
			switch(mask)
			{
				case "#TEXT":
					// Тестирование текста
					return PrintTemplateText(graph, 0, false, line);
					break;
				default:
					break;
			}
			return 0;
		}
		protected int PrintTemplateBlockLine(Graphics graph, int offset, string[] line)
		{
			if(line.Length < 1) return 0;		// Некорректная строчка обработке не подлежит
			string mask	= line[0];				// Первый элемент строки шаблона - тип записи
			switch(mask)
			{
				case "#TEXT":
					// Тестирование текста
					return PrintTemplateText(graph, offset, true, line);
					break;
				default:
					break;
			}
			return 0;
		}
		protected int PrintTemplateText(Graphics graph, int offset, bool print, string[] line)
		{
			// Версии будут использоваться для обратной совместимости
			if(line.Length < 2) return 0;		// Некорректная строчка обработке не подлежит
			string mask	= line[0];
			if(mask != "#TEXT") return 0;		// Дополнительная проверка
			string version	= line[1];		// Версия шаблона шрифта

			// Переменные описания шрифта
			string	template_text_text;
			string	template_text_x;
			string	template_text_width;
			string	template_text_font;
			string	template_text_brush;
			string	template_text_format;

			string			text_text;
			int				text_x;
			int				text_width;
			Font			text_font;
			Brush			text_brush;
			StringFormat	text_format;

			switch(version)
			{
				case "1":
					// Первая версия
					if(line.Length < 8) return 0;
					template_text_text		= line[2];
					template_text_x			= line[3];
					template_text_width		= line[4];
					template_text_format	= line[5];
					template_text_font		= line[6];
					template_text_brush		= line[7];
					// Преобразование данных
					text_text				= template_text_text;
					text_x					= (int)Convert.ToInt32(template_text_x);
					text_width				= (int)Convert.ToInt32(template_text_width);
					text_format				= (StringFormat)template_stringformats[template_text_format];
					text_font				= (Font)tamplate_fonts[template_text_font];
					text_brush				= (Brush)template_brushes[template_text_brush];
					// И вот уже непосредственно печать
					SizeF size				= MeasureText(graph, text_text, text_width, text_format.Alignment, text_format.LineAlignment, text_font);
					if(print == false) return (int)size.Height;
					PrintText(graph, text_text, text_x, offset, text_width, size.Height, text_format.Alignment, text_format.LineAlignment, text_font, text_brush, false);
					return (int)size.Height;
					break;
				default:
					break;
			}
			return 0;
		}
		protected bool AnalizeTemplateLine(string[] line)
		{
			// Налазириуем строчку шаблона
			if(line.Length < 1) return false;		// Некорректная строчка обработке не подлежит
			string mask	= line[0];					// Первый элемент строки шаблона - тип записи
			switch(mask)
			{
				case "#FONT":
					// Добавление нового шрифта в печать
					MakeTemplateFont(line);
					break;
				case "#STRINGFORMAT":
					// Добавление нового шрифта в печать
					MakeTemplateStringFormat(line);
					break;
				case "#BRUSH":
					// Добавление нового шрифта в печать
					MakeTemplateBrush(line);
					break;
				case "#BLOCK":
					// Это блок, требуется анализ содержимого, с последующей печатью
					return true;
					break;
				default:
					break;
			}
			return false;
		}
		#region Методы анализа примитивов шаблона
		protected void MakeTemplateFont(string[] line)
		{
			// Версии будут использоваться для обратной совместимости
			if(line.Length < 2) return;		// Некорректная строчка обработке не подлежит
			string mask	= line[0];
			if(mask != "#FONT") return;		// Дополнительная проверка
			string version	= line[1];		// Версия шаблона шрифта

			// Переменные описания шрифта
			string	template_font_name;
			string	template_font_family;
			string	template_font_size;
			string	template_font_style;

			string		font_name;
			string		font_family;
			float		font_size;
			FontStyle	font_style;

			if(tamplate_fonts == null)
				tamplate_fonts = new TemplateFonts();

			switch(version)
			{
				case "1":
					// Первая версия
					if(line.Length < 6) return;
					template_font_name		= line[2];
					template_font_family	= line[3];
					template_font_size		= line[4];
					template_font_style		= line[5];
					// Преобразование данных
					font_name				= template_font_name;
					font_family				= template_font_family;
					font_size				= (float)Convert.ToDecimal(template_font_size);
					switch(template_font_style)
					{
						case "bold":
							font_style		= FontStyle.Bold;
							break;
						default:
							font_style		= FontStyle.Regular;
							break;
					}
					// Проверка данных
					if(font_name == "") return;
					if(font_family == "") return;
					if(font_size < 0.0F) return;
					// Создание и запоминание шрифта
					if(tamplate_fonts.Contains(font_name)) return;				// Шрифт уже есть в списке шрифтов шаблона
					Font font = new Font(font_family, font_size, font_style);
					tamplate_fonts.Add(font_name, font);						// Добавляем шрифт в список шрифтов шаблона
					break;
				default:
					break;
			}
		}
		protected void MakeTemplateStringFormat(string[] line)
		{
			// Версии будут использоваться для обратной совместимости
			if(line.Length < 2) return;		// Некорректная строчка обработке не подлежит
			string mask	= line[0];
			if(mask != "#STRINGFORMAT") return;		// Дополнительная проверка
			string version	= line[1];		// Версия шаблона шрифта

			// Переменные описания шрифта
			string	template_stringformat_name;
			string	template_stringformat_alignment;
			string	template_stringformat_linealignment;
			string	template_stringformat_trimming;

			string			stringformat_name;
			StringFormat	stringformat_format;

			if(template_stringformats == null)
				template_stringformats = new TemplateStringFormats();

			switch(version)
			{
				case "1":
					// Первая версия
					if(line.Length < 6) return;
					template_stringformat_name			= line[2];
					template_stringformat_alignment		= line[3];
					template_stringformat_linealignment	= line[4];
					template_stringformat_trimming		= line[5];
					// Создание формата
					stringformat_format	= new StringFormat();
					// Преобразование данных и заполнение формата
					switch(template_stringformat_alignment)
					{
						case "near":
							stringformat_format.Alignment		= System.Drawing.StringAlignment.Near;
							break;
						default:
							stringformat_format.Alignment		= System.Drawing.StringAlignment.Near;
							break;
					}
					switch(template_stringformat_linealignment)
					{
						case "near":
							stringformat_format.LineAlignment		= System.Drawing.StringAlignment.Near;
							break;
						default:
							stringformat_format.LineAlignment		= System.Drawing.StringAlignment.Near;
							break;
					}
					switch(template_stringformat_trimming)
					{
						case "word":
							stringformat_format.Trimming		= StringTrimming.Word;
							break;
						default:
							stringformat_format.Trimming		= StringTrimming.Word;
							break;
					}
					stringformat_name	= template_stringformat_name;
					// Проверка данных
					if(stringformat_name == "") return;
					if(stringformat_format == null) return;

					// Добавление формата в список
					StringFormat stringformat = new StringFormat(stringformat_format);
					if(template_stringformats.Contains(stringformat_name)) return;		// Формат строки уже есть в списке форматов шаблона
					template_stringformats.Add(stringformat_name, stringformat);						// Добавляем формат в список форматов шаблона
					break;
				default:
					break;
			}
		}
		protected void MakeTemplateBrush(string[] line)
		{
			// Версии будут использоваться для обратной совместимости
			if(line.Length < 2) return;		// Некорректная строчка обработке не подлежит
			string mask	= line[0];
			if(mask != "#BRUSH") return;	// Дополнительная проверка
			string version	= line[1];		// Версия шаблона

			// Переменные описания шрифта
			string	template_brush_name;
			string	template_brush_color;

			string		brush_name;
			Color		brush_color;

			if(template_brushes == null)
				template_brushes = new TemplateBrushes();

			switch(version)
			{
				case "1":
					// Первая версия
					if(line.Length < 4) return;
					template_brush_name		= line[2];
					template_brush_color	= line[3];
					// Преобразование данных
					brush_name				= template_brush_name;
					switch(template_brush_color)
					{
						case "black":
							brush_color		= Color.Black;
							break;
						default:
							brush_color		= Color.Black;
							break;
					}
					// Проверка данных
					if(brush_name == "") return;
					// Создание и запоминание шрифта
					if(template_brushes.Contains(brush_name)) return;				// Шрифт уже есть в списке шрифтов шаблона
					Brush brush = new SolidBrush(brush_color);
					template_brushes.Add(brush_name, brush);						// Добавляем шрифт в список шрифтов шаблона
					break;
				default:
					break;
			}
		}
		protected void MakeTemplateBlock(string[] line, ArrayList strings)
		{
			// Версии будут использоваться для обратной совместимости
			if(line.Length < 2) return;		// Некорректная строчка обработке не подлежит
			string mask	= line[0];
			if(mask != "#FONT") return;		// Дополнительная проверка
			string version	= line[1];		// Версия шаблона шрифта

			// Переменные описания шрифта
			string	template_block_name;
			string	template_block_type;

			string		block_name;
			int			block_type;

			if(template_blocks == null)
				template_blocks = new TemplateBlocks();

			switch(version)
			{
				case "1":
					// Первая версия
					if(line.Length < 3) return;
					template_block_type		= line[1];
					template_block_name		= line[2];
					// Преобразование данных
					block_name				= template_block_name;
					block_type				= (int)Convert.ToInt32(template_block_type);
					// Проверка данных
					if(block_name == "") return;
					if(block_type <= 0) return;
					// Создание и запоминание шрифта
					if(tamplate_fonts.Contains(block_name)) return;				// Блок уже есть в списке блоков шаблона
					template_blocks.Add(block_name, strings);						// Добавляем блок в список блоков шаблона
					// Дополняем структуру документа
					if(template_struct == null)
						template_struct = new ArrayList();
					TemplateStruct template_struct_element = new TemplateStruct(block_name, block_type);
					template_struct.Add(template_struct_element);
					break;
				default:
					break;
			}
		}
		#endregion
	}
}
