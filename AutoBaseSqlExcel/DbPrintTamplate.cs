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
	/// ����� ��� ���������� ������ �������� ��������.
	/// </summary>
	public class DbPrintTamplate:DbPrint
	{
		string file_name = "";

		#region ��������� ������� ��� �������� � ������
		protected class TemplateStruct
		{
			string	block_name			= "";		// ��� �����
			int		block_type			= 0;		// ��� �����
			float	block_height		= 0.0F;		// ������ �����, ���� ���� �����������
			bool	flag_block_height	= false;	// ����, ��� ������ ����� ����������

			public TemplateStruct(string name, int type)
			{
				block_name	= name;
				block_type	= type;
			}
		}
		ArrayList template_struct = null;
		// ���������� ������ �� ��������
		// ������ �������
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
		// ������ �������� �����
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
		// ������ ������
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
		// ������ ������
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

		// ���������� ������ � ���������� ��������� ������� �� �����
		protected void ReadTemplate(string file)
		{
			// ������ ������� �� �����
			StreamReader reader = null;
			reader = new StreamReader(file, System.Text.Encoding.GetEncoding(1251));	// ��������� ���� ��� ������
			

			string s = reader.ReadLine();		// ������ �� ����� ������
			while(s != null)
			{
				char[] separators = new Char[] {'\t'};
				string[] ssplit = s.Split(separators);
				bool block = AnalizeTemplateLine(ssplit);
				if(block == true)
				{
					// ���������� �������� �� ����� ���� ������ � ����������� ���
					// ������ ����
					ArrayList strs = null;
					string str = reader.ReadLine();
					while (str != null && str != "#BLOCKEND")
					{
						if(strs == null) strs = new ArrayList();
						strs.Add(str);
						str = reader.ReadLine();
					}
					// ��������� ���� � ������ � ��������� ��������� ���������
					MakeTemplateBlock(ssplit, strs);
				}
				s = reader.ReadLine();
			}
			reader.Close();						// ��������� ����
		}

		// ��������� ������� - ��� ������ ������� �������
		public override void  PrintPage(Graphics graph, int page)
		{
			// ��� ���������� �� ��������
			PrintTemplate(graph, file_name);
		}
		// ���������� ����� ������ �� ���� �� ��������
		protected void PrintTemplate(Graphics graph, string file)
		{
			// ������ ������� �� �����
			StreamReader reader = null;
			int offset			= 20;			// �������������� ������
			reader = new StreamReader(file, System.Text.Encoding.GetEncoding(1251));	// ��������� ���� ��� ������
			

			string s = reader.ReadLine();		// ������ �� ����� ������
			while(s != null)
			{
				char[] separators = new Char[] {'\t'};
				string[] ssplit = s.Split(separators);
				bool block = AnalizeTemplateLine(ssplit);
				if(block == true)
				{
					// ���������� �������� �� ����� ���� ������ � ����������� ���
					// ������ ����
					ArrayList strs = null;
					string str = reader.ReadLine();
					while (str != null && str != "#BLOCKEND")
					{
						if(strs == null) strs = new ArrayList();
						strs.Add(str);
						str = reader.ReadLine();
					}
					// ��������� ��������� ������ �����
					offset = PrintBlock(graph, offset, new DelegatePrintBlock(PrintTemplateBlock), (object)strs);
				}
				s = reader.ReadLine();
			}
			reader.Close();						// ��������� ����
		}
		protected int PrintTemplateBlock(Graphics graph, int offset, bool test, bool print, object o)
		{
			ArrayList list = (ArrayList)o;
			int width = 0;
			foreach(string s in list)
			{
				// ����������� � ������������ ������ ������� �����
				char[] separators = new Char[] {'\t'};
				string[] ssplit = s.Split(separators);
				if(test)
				{
					// ��������� ������ ����� �����
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
			if(line.Length < 1) return 0;		// ������������ ������� ��������� �� ��������
			string mask	= line[0];				// ������ ������� ������ ������� - ��� ������
			switch(mask)
			{
				case "#TEXT":
					// ������������ ������
					return PrintTemplateText(graph, 0, false, line);
					break;
				default:
					break;
			}
			return 0;
		}
		protected int PrintTemplateBlockLine(Graphics graph, int offset, string[] line)
		{
			if(line.Length < 1) return 0;		// ������������ ������� ��������� �� ��������
			string mask	= line[0];				// ������ ������� ������ ������� - ��� ������
			switch(mask)
			{
				case "#TEXT":
					// ������������ ������
					return PrintTemplateText(graph, offset, true, line);
					break;
				default:
					break;
			}
			return 0;
		}
		protected int PrintTemplateText(Graphics graph, int offset, bool print, string[] line)
		{
			// ������ ����� �������������� ��� �������� �������������
			if(line.Length < 2) return 0;		// ������������ ������� ��������� �� ��������
			string mask	= line[0];
			if(mask != "#TEXT") return 0;		// �������������� ��������
			string version	= line[1];		// ������ ������� ������

			// ���������� �������� ������
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
					// ������ ������
					if(line.Length < 8) return 0;
					template_text_text		= line[2];
					template_text_x			= line[3];
					template_text_width		= line[4];
					template_text_format	= line[5];
					template_text_font		= line[6];
					template_text_brush		= line[7];
					// �������������� ������
					text_text				= template_text_text;
					text_x					= (int)Convert.ToInt32(template_text_x);
					text_width				= (int)Convert.ToInt32(template_text_width);
					text_format				= (StringFormat)template_stringformats[template_text_format];
					text_font				= (Font)tamplate_fonts[template_text_font];
					text_brush				= (Brush)template_brushes[template_text_brush];
					// � ��� ��� ��������������� ������
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
			// ����������� ������� �������
			if(line.Length < 1) return false;		// ������������ ������� ��������� �� ��������
			string mask	= line[0];					// ������ ������� ������ ������� - ��� ������
			switch(mask)
			{
				case "#FONT":
					// ���������� ������ ������ � ������
					MakeTemplateFont(line);
					break;
				case "#STRINGFORMAT":
					// ���������� ������ ������ � ������
					MakeTemplateStringFormat(line);
					break;
				case "#BRUSH":
					// ���������� ������ ������ � ������
					MakeTemplateBrush(line);
					break;
				case "#BLOCK":
					// ��� ����, ��������� ������ �����������, � ����������� �������
					return true;
					break;
				default:
					break;
			}
			return false;
		}
		#region ������ ������� ���������� �������
		protected void MakeTemplateFont(string[] line)
		{
			// ������ ����� �������������� ��� �������� �������������
			if(line.Length < 2) return;		// ������������ ������� ��������� �� ��������
			string mask	= line[0];
			if(mask != "#FONT") return;		// �������������� ��������
			string version	= line[1];		// ������ ������� ������

			// ���������� �������� ������
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
					// ������ ������
					if(line.Length < 6) return;
					template_font_name		= line[2];
					template_font_family	= line[3];
					template_font_size		= line[4];
					template_font_style		= line[5];
					// �������������� ������
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
					// �������� ������
					if(font_name == "") return;
					if(font_family == "") return;
					if(font_size < 0.0F) return;
					// �������� � ����������� ������
					if(tamplate_fonts.Contains(font_name)) return;				// ����� ��� ���� � ������ ������� �������
					Font font = new Font(font_family, font_size, font_style);
					tamplate_fonts.Add(font_name, font);						// ��������� ����� � ������ ������� �������
					break;
				default:
					break;
			}
		}
		protected void MakeTemplateStringFormat(string[] line)
		{
			// ������ ����� �������������� ��� �������� �������������
			if(line.Length < 2) return;		// ������������ ������� ��������� �� ��������
			string mask	= line[0];
			if(mask != "#STRINGFORMAT") return;		// �������������� ��������
			string version	= line[1];		// ������ ������� ������

			// ���������� �������� ������
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
					// ������ ������
					if(line.Length < 6) return;
					template_stringformat_name			= line[2];
					template_stringformat_alignment		= line[3];
					template_stringformat_linealignment	= line[4];
					template_stringformat_trimming		= line[5];
					// �������� �������
					stringformat_format	= new StringFormat();
					// �������������� ������ � ���������� �������
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
					// �������� ������
					if(stringformat_name == "") return;
					if(stringformat_format == null) return;

					// ���������� ������� � ������
					StringFormat stringformat = new StringFormat(stringformat_format);
					if(template_stringformats.Contains(stringformat_name)) return;		// ������ ������ ��� ���� � ������ �������� �������
					template_stringformats.Add(stringformat_name, stringformat);						// ��������� ������ � ������ �������� �������
					break;
				default:
					break;
			}
		}
		protected void MakeTemplateBrush(string[] line)
		{
			// ������ ����� �������������� ��� �������� �������������
			if(line.Length < 2) return;		// ������������ ������� ��������� �� ��������
			string mask	= line[0];
			if(mask != "#BRUSH") return;	// �������������� ��������
			string version	= line[1];		// ������ �������

			// ���������� �������� ������
			string	template_brush_name;
			string	template_brush_color;

			string		brush_name;
			Color		brush_color;

			if(template_brushes == null)
				template_brushes = new TemplateBrushes();

			switch(version)
			{
				case "1":
					// ������ ������
					if(line.Length < 4) return;
					template_brush_name		= line[2];
					template_brush_color	= line[3];
					// �������������� ������
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
					// �������� ������
					if(brush_name == "") return;
					// �������� � ����������� ������
					if(template_brushes.Contains(brush_name)) return;				// ����� ��� ���� � ������ ������� �������
					Brush brush = new SolidBrush(brush_color);
					template_brushes.Add(brush_name, brush);						// ��������� ����� � ������ ������� �������
					break;
				default:
					break;
			}
		}
		protected void MakeTemplateBlock(string[] line, ArrayList strings)
		{
			// ������ ����� �������������� ��� �������� �������������
			if(line.Length < 2) return;		// ������������ ������� ��������� �� ��������
			string mask	= line[0];
			if(mask != "#FONT") return;		// �������������� ��������
			string version	= line[1];		// ������ ������� ������

			// ���������� �������� ������
			string	template_block_name;
			string	template_block_type;

			string		block_name;
			int			block_type;

			if(template_blocks == null)
				template_blocks = new TemplateBlocks();

			switch(version)
			{
				case "1":
					// ������ ������
					if(line.Length < 3) return;
					template_block_type		= line[1];
					template_block_name		= line[2];
					// �������������� ������
					block_name				= template_block_name;
					block_type				= (int)Convert.ToInt32(template_block_type);
					// �������� ������
					if(block_name == "") return;
					if(block_type <= 0) return;
					// �������� � ����������� ������
					if(tamplate_fonts.Contains(block_name)) return;				// ���� ��� ���� � ������ ������ �������
					template_blocks.Add(block_name, strings);						// ��������� ���� � ������ ������ �������
					// ��������� ��������� ���������
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
