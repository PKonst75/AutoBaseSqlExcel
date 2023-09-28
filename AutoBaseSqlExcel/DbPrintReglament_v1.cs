using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbPrintReglament_v1.
	/// </summary>
	public class DbPrintReglament_v1:DbPrint
	{
		// ����������� ��� ������
		SolidBrush	brush_standart;
		SolidBrush	brush_lightgray;
		Font		font_small_bold;
		Font		font_middle_bold;
		Font		font_middle;
		Font		font_small;
		Pen			pen_thin;

		private DtCard		card;

		#region ������ ��� ������
		public class PrintData
		{
			public bool			is_reglament	= false;	// ���� ������� ������������ �����
			public ArrayList	collections		= null;		// ���������
			public string		txt_warrant		= "";		// ����� � ��� �����-������

			public class Collection
			{
				public DtCardWork				card_work;
				public DtWorkCollection			work_collection;
				public DtWorkCollectionItem		collection_item;
				public ArrayList				collection_items;
				public string					txt_summ_hours	= "";		// ����� �� ���� �����

				public Collection()
				{
				}
			}
			public PrintData(DtCard card)
			{
				// ������ ����� � �����-������
				if((long)card.GetData("�����_�����_��������") != 0)
				{
					txt_warrant = card.GetData("�����_�����_��������").ToString() + " �� " + card.GetData("����_�����_������_��������").ToString();
				}
				else
					txt_warrant = "�� ������";

				ArrayList card_works = new ArrayList();
				DbSqlCardWork.SelectInArray(card, card_works);
				foreach(DtCardWork element in card_works)
				{
					long code_work			= (long)element.GetData("���_������������_��������_������");
					DtWork work				= DbSqlWork.Find(code_work);
					long code_collection	= (long)work.GetData("������_���_���������");
					if(code_collection != 0)
					{
						// ���������� ������������ ������!!!!
						is_reglament	= true;
						if (collections == null)
							collections = new ArrayList();
						// ��������� ��������� ��� ������� ���� ������
						ArrayList arr = new ArrayList();
						DbSqlWorkCollectionItem.SelectInArray(arr, code_collection);
						Collection item			= new Collection();
						item.card_work			= element;
						item.collection_items	= arr;
						collections.Add(item);
						// ����������� ����� �� ����� ����� � ������ ���������
						float hours = 0.0F;
						foreach(DtWorkCollectionItem itm in arr)
						{
							hours += (float)itm.GetData("������������_���������_�������");
						}
						item.txt_summ_hours = hours.ToString() + "\n";
						if (hours >= 1.0) item.txt_summ_hours += (((int)Math.Floor(hours)).ToString()) + "�";
						item.txt_summ_hours += ((hours - (int)Math.Floor(hours))*60).ToString() + "���";
					}
				}
			}
				
		}
		public PrintData	print_data = null;
		#endregion

		public DbPrintReglament_v1(long card_number, int card_year)
		{
			// ���������� ������������ ��� ������
			brush_standart		= new SolidBrush(Color.Black);
			brush_lightgray		= new SolidBrush(Color.LightGray);
			font_small_bold		= new Font("Arial", 8, FontStyle.Bold);
			font_small			= new Font("Arial", 8);
			font_middle_bold	= new Font("Arial", 10, FontStyle.Bold);
			font_middle			= new Font("Arial", 10);
			pen_thin			= new Pen(brush_standart, 0.3F);

			// ������ ������� ���������� �� �������� (���� �� ��������)
			if(card_number != 0 && card_year != 0)
				// ����� �������� �� ������� ������
				card =	DbSqlCard.Find(card_number, card_year);
			else
				// �������� �������
				card = null;

			if(card != null)
			{
				// ������ ����� ��������, �������� ������ ��� ������
				print_data = new PrintData(card);
			}
		}

		#region ������ ���������
		protected int PrintHead(Graphics graph, int offset,  bool test, bool print, object o)
		{
			if(test == true || print == false)
			{
				return offset + 10;
			}
			

			string text = print_data.txt_warrant;
			if(print)PrintText(graph, "�������� ������������ ����� � �����-������ �" + text, 10, 0 + offset, 190, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_middle, brush_standart, false);
			return offset + 10;
		}
		#endregion

		#region ������ ����� ���������
		private int PrintCollectionBlockOverHead(Graphics graph, int offset,  bool test, bool print, object o)
		{
			// ��������������� �����������
			RectangleF rect;
			int y		= offset;
			string text	= "";
			// ����������� ���������
			int offset_x_left		= 10;
			int offset_x_right		= 10;
			int page_width			= 210;
			int title_height		= 4;
			int page_height			= 297 - 10;

			int	col1		=	10;
			int col3		=	20;
			int col4		=	10;
			int col2		=	page_width - offset_x_left - offset_x_right - col1 - col3 - col4;
			int	rowheight	= 6;

			if(test == true || print == false)
			{	
				y += rowheight;
				return y;
			}

			// ������ �������
			if(o == null) return y;
			PrintData.Collection collection	= (PrintData.Collection)o;
			DtCardWork item = (DtCardWork)collection.card_work;

			text = (string)item.GetData("������������_��������_������");
			if(print)PrintText(graph, text, 10, 0 + offset, 190, 10, System.Drawing.StringAlignment.Near, System.Drawing.StringAlignment.Near, font_middle, brush_standart, false);
			y += rowheight;
			return y;
		}
		private int PrintCollectionBlockHead(Graphics graph, int offset,  bool test, bool print, object o)
		{
			// ��������������� �����������
			RectangleF rect;
			int y		= offset;
			string text	= "";
			// ����������� ���������
			int offset_x_left		= 10;
			int offset_x_right		= 10;
			int page_width			= 210;
			int title_height		= 4;
			int page_height			= 297 - 10;

			int	col1		=	10;
			int col3		=	20;
			int col4		=	10;
			int col2		=	page_width - offset_x_left - offset_x_right - col1 - col3 - col4;
			int	rowheight	= 6;

			if(test == true || print == false)
			{	
				y += rowheight;
				return y;
			}

			// ������ �������
			PrintTextBox(graph, "�", offset_x_left, y, col1, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "������������ ������", offset_x_left + col1, y, col2, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "�����", offset_x_left + col1 + col2, y, col3, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, "V", offset_x_left + col1 + col2 + col3, y, col4, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			y += rowheight;
			return y;
		}
		private int PrintCollectionBlock(Graphics graph, int offset,  bool test, bool print, object o)
		{
			// ��������������� �����������
			RectangleF			rect;
			SizeF				size;
			int y				= offset;
			string text			= "";
			int	real_height		= 0;
			int	real_height_all	= 0;
			// ����������� ���������
			int offset_x_left		= 10;
			int offset_x_right		= 10;
			int page_width			= 210;
			int title_height		= 4;
			int page_height			= 297 - 10;

			int	col1		=	10;
			int col3		=	20;
			int col4		=	10;
			int col2		=	page_width - offset_x_left - offset_x_right - col1 - col3 - col4;
			int	rowheight	=	8;
			
			if(o == null) return y;
			PrintData.Collection collection	= (PrintData.Collection)o;
			DtWorkCollectionItem item = (DtWorkCollectionItem)collection.collection_item;
			if((int)item.GetData("�����_������_���������_�������") != 0) return y;
			int number = (int)item.GetData("�����_���������_�������");

			string txt		= "";
			// ��� ��������� �������, ����� �������� ���� ��� � ���� ������ � ��� ������
			txt	= (string)item.GetData("������������_���������_�������");
			size = MeasureText(graph, txt, col2, StringAlignment.Near, StringAlignment.Near, font_small);
			real_height	= (int)Math.Ceiling(size.Height);
			if(number != 0)
			{
				foreach(DtWorkCollectionItem itm in collection.collection_items)
				{
					if((int)itm.GetData("�����_������_���������_�������") == number)
					{
						txt	= (string)itm.GetData("������������_���������_�������");
						size = MeasureText(graph, txt, col2, StringAlignment.Near, StringAlignment.Near, font_small);
						real_height_all	+= (int)Math.Ceiling(size.Height);
					}
				}
			}

			if(test == true || print == false)
			{	
				return y + real_height_all + real_height;
			}
			
			string txt1		= "";
			string txt2		= "";
			string txt3		= "";
			
			if(number != 0)
				txt = number.ToString();
			else
				txt	= "";
			PrintTextBox(graph, txt, offset_x_left, y, col1, real_height_all + real_height, 1, StringAlignment.Center, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
			if(number != 0 && real_height_all != 0)
			{
				// ��������� �������, � �������� �� ������ ������
				txt	= (string)item.GetData("������������_���������_�������");
				PrintTextBox(graph, txt, offset_x_left + col1, y, col2 + col3 + col4, real_height, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
				y += real_height;
				float val = (float)item.GetData("������������_���������_�������");
				txt = val.ToString() + "\n" + (val * 60).ToString() + "���";
				PrintTextBox(graph, txt, offset_x_left + col1 + col2, y, col3, real_height_all, 1, StringAlignment.Center, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
				foreach(DtWorkCollectionItem itm in collection.collection_items)
				{
					if((int)itm.GetData("�����_������_���������_�������") == number)
					{
						txt	= (string)itm.GetData("������������_���������_�������");
						size = MeasureText(graph, txt, col2, StringAlignment.Near, StringAlignment.Near, font_small);
						PrintTextBox(graph, txt, offset_x_left + col1, y, col2, (int)Math.Ceiling(size.Height), 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
						PrintTextBox(graph, "", offset_x_left + col1 + col2 + col3, y, col4, (int)Math.Ceiling(size.Height), 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
						y += (int)Math.Ceiling(size.Height);
					}
				}
			}
			else
			{
				txt	= (string)item.GetData("������������_���������_�������");
				PrintTextBox(graph, txt, offset_x_left + col1, y, col2, real_height, 1, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
				float val = (float)item.GetData("������������_���������_�������");
				if(val != 0.0F)
					txt = val.ToString() + " " + (val * 60).ToString() + "���";
				else
					txt = "";
				PrintTextBox(graph, txt, offset_x_left + col1 + col2, y, col3, real_height, 1, StringAlignment.Center, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
				PrintTextBox(graph, "", offset_x_left + col1 + col2 + col3, y, col4, real_height, 1, StringAlignment.Center, StringAlignment.Center, font_small, brush_standart, pen_thin, false);
				y += real_height;
			}
			
			return y;
		}
		private int PrintCollectionBlockFooter(Graphics graph, int offset,  bool test, bool print, object o)
		{
			// ��������������� �����������
			RectangleF rect;
			int y		= offset;
			string txt	= "";
			// ����������� ���������
			int offset_x_left		= 10;
			int offset_x_right		= 10;
			int page_width			= 210;
			int title_height		= 4;
			int page_height			= 297 - 10;

			int	col1		=	10;
			int col3		=	20;
			int col4		=	10;
			int col2		=	page_width - offset_x_left - offset_x_right - col1 - col3 - col4;
			int	rowheight	=	10;

			if(test == true || print == false)
			{	
				y += rowheight;
				return y;
			}

			// ������ �������
			if(o == null) return y;
			PrintData.Collection collection	= (PrintData.Collection)o;
			txt = collection.txt_summ_hours;
			PrintTextBox(graph, "�����:", offset_x_left, y, col1 + col2, rowheight, 1, StringAlignment.Far, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, txt, offset_x_left + col1 + col2, y, col4 + col3, rowheight, 1, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			y += rowheight;
			return y;
		}
		#endregion

		#region ������ ����� ��������
		private int PrintBlock6(Graphics graph, int offset,  bool test, bool print, object o)
		{
			// ��������������� �����������
			string txt;
			int y		= offset;
			// ����������� ���������
			int offset_x_left		= 10;
			int h1					= 4;
			int	w1					= 60;

			y += 6;
			if(test == true || print == false)
			{
				y += h1;
				y += h1 + 4;
				y += h1;
				y += h1;
				return y;
			}

			// ���������
			PrintText(graph, "�������� ������� ���. �", offset_x_left, y, w1, h1, StringAlignment.Near, StringAlignment.Far, font_small, brush_standart, false);
			y += h1;
			PrintLineHor(graph, offset_x_left + w1, y, 60, pen_thin, false);
			PrintLineHor(graph, offset_x_left + w1 + 70, y, 60, pen_thin, false);
			PrintText(graph, "(�������)", offset_x_left + w1, y, 60, h1, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
			PrintText(graph, "(���������)", offset_x_left + w1 + 70, y, 60, h1, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
			y += h1 + 4;
			PrintText(graph, "�������� �������� ������ �����", offset_x_left, y, w1, h1, StringAlignment.Near, StringAlignment.Far, font_small, brush_standart, false);
			y += h1;
			PrintLineHor(graph, offset_x_left + w1, y, 60, pen_thin, false);
			PrintLineHor(graph, offset_x_left + w1 + 70, y, 60, pen_thin, false);
			PrintText(graph, "(�������)", offset_x_left + w1, y, 60, h1, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
			PrintText(graph, "(���������)", offset_x_left + w1 + 70, y, 60, h1, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
			y += h1;
			return y;
		}
		#endregion

		// �������� ��������� ������
		public override void  PrintPage(Graphics graph, int page)
		{
			// ��� ���������� �� ��������
			int offset = 0;

			offset = 10;
			offset = PrintBlock(graph, offset, new DelegatePrintBlock(PrintHead), null);
			bool first = true;
			if(print_data.collections.Count > 0)
			{
				foreach(PrintData.Collection element in print_data.collections)
				{
					// ������ ����� ���������
					first = true;
					if(element.collection_items.Count > 0)
					{
						offset = PrintBlock(graph, offset, new DelegatePrintBlock(PrintCollectionBlockOverHead), element);
						foreach(DtWorkCollectionItem item in element.collection_items)
						{
							element.collection_item = item;
							offset = PrintBlockWithHeader(graph, offset, new DelegatePrintBlock(PrintCollectionBlock), element, new DelegatePrintBlock(PrintCollectionBlockHead), first);
							first = false;
						}
						offset = PrintBlock(graph, offset, new DelegatePrintBlock(PrintCollectionBlockFooter), element);
					}
				}
			}
			else
			{
		//		offset = PrintBlockWithHeader(graph, offset, new DelegatePrintBlock(PrintWorkBlock), null, new DelegatePrintBlock(PrintWorkBlockHead), first);
			}
			offset = PrintBlock(graph, offset, new DelegatePrintBlock(PrintBlock6), null);
		//	offset = PrintBlock(graph, offset, new DelegatePrintBlock(PrintWorkBlockFooter), null);
		//	
		//	offset = PrintBlock(graph, offset, new DelegatePrintBlock(PrintDetailBlockFooter), null);
		//	offset = PrintBlock(graph, offset, new DelegatePrintBlock(PrintFooter), null);
		}
	}
}
