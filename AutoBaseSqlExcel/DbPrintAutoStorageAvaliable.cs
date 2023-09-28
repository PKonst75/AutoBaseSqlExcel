using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbPrintAutoStorageAvaliable.
	/// </summary>
	public class DbPrintAutoStorageAvaliable:DbPrint
	{
		// ����������� ��� ������
		SolidBrush	brush_standart;
		SolidBrush	brush_lightgray;
		Font		font_small_bold;
		Font		font_middle_bold;
		Font		font_middle;
		Font		font_small;
		Pen			pen_thin;

		#region ������ ��� ������
		protected class HeaderData
		{
			public struct AutoInfo
			{
				public long number;
				public string txt_date_income;
				public string txt_auto;
			};
			
			public string txt_date;
			public ArrayList auto_avaliable = null;
			
			public HeaderData()
			{
				ArrayList autos = null;
				ArrayList tmp = null;
				
				// ������ ������
				txt_date = DateTime.Now.ToShortDateString();

				
				// ����� �� ����������� � �������
				autos = new ArrayList();
				DbSqlAuto.SelectInArrayStorageAvaliable(autos);
				long last_number = 0;
				foreach(object o in autos)
				{
					DtAuto tmp_auto = (DtAuto)o;
					DtAuto auto = DbSqlAuto.Find((long)tmp_auto.GetData("���_����������"));
					if(auto != null)
					{
						
						AutoInfo auto_info = new AutoInfo();
						auto_info.txt_date_income = "";
						auto_info.txt_auto = auto.GetData("VIN").ToString() + " / " + auto.GetData("������").ToString() + " / " + auto.GetData("����������_����������").ToString() + " / " + auto.GetData("����������_����").ToString();
						auto_info.number = last_number + 1;
						last_number++;
						if (auto_avaliable == null)
							auto_avaliable = new ArrayList();
						auto_avaliable.Add(auto_info);
																	
					}
				}
			}
		}
		HeaderData	header_data = null;
		#endregion

		public DbPrintAutoStorageAvaliable()
		{
			// ���������� ������������ ��� ������
			brush_standart		= new SolidBrush(Color.Black);
			brush_lightgray		= new SolidBrush(Color.LightGray);
			font_small_bold		= new Font("Arial", 8, FontStyle.Bold);
			font_small			= new Font("Arial", 8);
			font_middle_bold	= new Font("Arial", 10, FontStyle.Bold);
			font_middle			= new Font("Arial", 10);
			pen_thin			= new Pen(brush_standart, 0.3F);

			header_data = new HeaderData();
		}
		#region ������ ����� ���������
		private int PrintMain(Graphics graph, int offset,  bool test, bool print, object o)
		{
			// ��������� ���������� ������ ���������
			int	y;
			int offset_x		= 10;
			int page_width		= 190;
			int title_height	= 8;

			if(test == true || print == false)
			{
				// ������ ������������ ������
				y = offset;
				y += title_height*1;
				return y ;
			}

			y	= offset;
			PrintText(graph, "����� ����������� �� " + header_data.txt_date, offset_x, y, page_width, title_height, StringAlignment.Near, StringAlignment.Center, font_middle_bold, brush_standart, false);
			y += title_height;
			return y;
		}
		private int PrintServiceBlock(Graphics graph, int offset,  bool test, bool print, object o)
		{
			// ��������� ���������� ������ ���������
			SizeF size;
			int	height;
			int	y;
			string txt	= "";
		

			int offset_x		= 10;
			int page_width		= 190;
			int title_height	= 5;

			if(test == true || print == false || o == null)
			{
				// ������ ������������ ������
				y = offset;
				y += title_height*1;
				return y ;
			}

			HeaderData.AutoInfo data;
			data = (HeaderData.AutoInfo)o;

			y	= offset;
			PrintText(graph, "� " + data.number.ToString()  + " ���������� " + data.txt_auto, offset_x, y, page_width, title_height, StringAlignment.Near, StringAlignment.Center, font_middle_bold, brush_standart, false);
			y += title_height;
			
			return y;
		}
		private int PrintServiceBlockHead(Graphics graph, int offset,  bool test, bool print, object o)
		{
			// ��������������� �����������
			RectangleF rect;
			int y		= offset;
			int y1		= 0;
			string text	= "";
			

			// ����������� ���������
			int offset_x_left		= 10;
			int offset_x_right		= 10;
			int page_width			= 210;
			int title_height		= 4;
			int	col1				= 190;
			int offset1				= 12;
			int offset2				= 80;
			int x1					= 30;
			int x2					= 20;

			
			if(test == true || print == false || o == null)
			{	
				y += title_height * 1;
				return y;
			}

			HeaderData.AutoInfo data;
			data = (HeaderData.AutoInfo)o;

			// ������ �������
			PrintTextBox(graph, "� �/�" + "               " + "����������", offset_x_left, y, col1, title_height * 1, 1, StringAlignment.Near, StringAlignment.Near, font_small_bold, brush_standart, pen_thin, false);
			y += title_height;
			y1 = y;
			return y;
		}
		#endregion

		// �������� ��������� ������
		public override void  PrintPage(Graphics graph, int page)
		{
			// ��� ���������� �� ��������
			int offset = 0;
			offset = 10;
			offset = PrintBlock(graph, offset, new DelegatePrintBlock(PrintMain), null);
			// ������ ��� ����� ������� �� ������������� ������
			bool first = true;
			if(header_data.auto_avaliable != null && header_data.auto_avaliable.Count > 0)
			{
				foreach(HeaderData.AutoInfo element in header_data.auto_avaliable)
				{
					offset = PrintBlockWithHeader(graph, offset, new DelegatePrintBlock(PrintServiceBlock), element, new DelegatePrintBlock(PrintServiceBlockHead), first);
					first = false;
				}
			}
			else
			{
				offset = PrintBlockWithHeader(graph, offset, new DelegatePrintBlock(PrintServiceBlock), null, new DelegatePrintBlock(PrintServiceBlockHead), first);
			}
		}
	}
}
