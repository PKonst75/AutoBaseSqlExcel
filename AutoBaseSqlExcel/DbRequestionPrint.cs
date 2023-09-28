using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbRequestionPrint.
	/// </summary>
	public class DbRequestionPrint
	{
		private Font printFont;
		SolidBrush drawBrush;
		//Pen boldPen;
		Pen thinPen;

		private DbDetailOutcomDoc requestion;
		private DbDetailIncomDoc debitDoc;
		private ArrayList requestionDetails;
	
		private int col1;
		private int col2;
		private int col3;
		private int col4;
		private int col5;
		private int col6;
		private int col7;
		private int col8;
		private int col9;
		private int col10;
		private int paperWidth = 270;
		private int leftMargin = 10;
		private int topMargin = 10;

		public DbRequestionPrint(DbDetailOutcomDoc document)
		{
			// Расстановка размеров
			col1 = 11;
			col2 = 40;
			col3 = 70;
			col4 = 9;
			col5 = 17;
			col6 = 15;
			col7 = 20;
			col8 = 22;
			col9 = 20;
			col10 = 40;

			requestion = document;
			// Получаем список работ данной карточки
			requestionDetails = new ArrayList();
			DbDetailOutcom.FillList(requestionDetails, requestion);
		}

		// Вызов диалога печати
		public void Print() 
		{
			try 
			{
				try 
				{
					thinPen = new Pen(Color.Black, (float)0.02);
					drawBrush = new SolidBrush(Color.Black);
					printFont = new Font("Arial", 10);
					PrintDocument pd = new PrintDocument();
					// Свойтсва принтера
					pd.DefaultPageSettings.Landscape = true;
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

		// The PrintPage event is raised for each page to be printed.
		// Пробуем печатать текст!
		private void pd_PrintPage(object sender, PrintPageEventArgs ev) 
		{
			// Настройка
			ev.Graphics.PageUnit = GraphicsUnit.Millimeter;

			// Печать
			int yPos = topMargin;
			yPos = PrintTitle(ev.Graphics, yPos);
			yPos = PrintDetails(ev.Graphics, yPos);
			yPos = PrintEnd(ev.Graphics, yPos);

			ev.HasMorePages = false;
		}

		private int PrintTitle(Graphics graph, int yPos)
		{
			StringFormat strFormat = new StringFormat();
			string text = "";
			int y = yPos;
			Rectangle rect = new Rectangle(leftMargin, y, paperWidth, 8);
			text = "ТРЕБОВАНИЕ №" + requestion.NumberTxt + " от " + requestion.DateTxt;
			y += 8;
			strFormat.Alignment = StringAlignment.Near;
			graph.DrawString(text, printFont, drawBrush, rect, strFormat);
			rect = new Rectangle(leftMargin, y, paperWidth, 8);
			text = "На основании:" + " " + requestion.Based;
			strFormat.Alignment = StringAlignment.Near;
			graph.DrawString(text, printFont, drawBrush, rect, strFormat);
			y += 8;
			return y;
		}

		private int PrintEnd(Graphics graph, int yPos)
		{
			StringFormat strFormat = new StringFormat();
			string text = "";
			int y = yPos;
			Rectangle rect = new Rectangle(leftMargin, y, paperWidth, 8);
			text = "Отпустил:" + requestion.GiveTxt;
			strFormat.Alignment = StringAlignment.Near;
			graph.DrawString(text, printFont, drawBrush, rect, strFormat);
			y += 8;
			rect.Y = y;
			text = "Получил:" + " " + requestion.GetTxt;
			graph.DrawString(text, printFont, drawBrush, rect, strFormat);
			y += 8;
			return y;
		}

		private int PrintDetails(Graphics graph, int yPos)
		{
			float summ = 0;
			int y = yPos;
			int count = 1;
			string text = "";
			string debitDetail = "";
			int height = 0;
			Rectangle rect = new Rectangle(leftMargin, y, 0, 15);
			StringFormat strFormat = new StringFormat();

			text = "№ п/п";
			rect.Width = col1;
			PrintBoxFixed(graph, text, rect, true);
			text = "Код";
			rect.X += rect.Width;
			rect.Width = col2;
			PrintBoxFixed(graph, text, rect, true);
			text = "Наименование";
			rect.X += rect.Width;
			rect.Width = col3;
			PrintBoxFixed(graph, text, rect, true);
			text = "Количество";
			rect.X += rect.Width;
			rect.Width = col4;
			PrintBoxFixed(graph, text, rect, true);
			text = "Цена, руб";
			rect.X += rect.Width;
			rect.Width = col5;
			PrintBoxFixed(graph, text, rect, true);
			text = "НДС, %";
			rect.X += rect.Width;
			rect.Width = col6;
			PrintBoxFixed(graph, text, rect, true);
			text = "Цена с НДС, руб";
			rect.X += rect.Width;
			rect.Width = col7;
			PrintBoxFixed(graph, text, rect, true);
			text = "Сумма, руб";
			rect.X += rect.Width;
			rect.Width = col8;
			PrintBoxFixed(graph, text, rect, true);
			text = "Цена входн., руб";
			rect.X += rect.Width;
			rect.Width = col9;
			PrintBoxFixed(graph, text, rect, true);
			text = "Документ поставки";
			rect.X += rect.Width;
			rect.Width = col10;
			PrintBoxFixed(graph, text, rect, true);
			y += 15;

			foreach(object o in requestionDetails)
			{
				DbDetailOutcom element = (DbDetailOutcom)o;
				text = element.DetailStorageName;
				strFormat.Trimming = StringTrimming.Word;
				strFormat.Alignment = StringAlignment.Near;
				SizeF size = graph.MeasureString(text, printFont, col3, strFormat);
				height = (int)size.Height;

				// Детали по поставщику 
				debitDoc = null;
				if(element.DetailIncom != null)
				{
					debitDoc = DbDetailIncomDoc.Find(element.DetailIncom.CodeDetailIncomDoc);
				}
				if(debitDoc != null)
				{
					debitDetail = debitDoc.FullNumberTxt + " / " + debitDoc.PartnerName + " / " + debitDoc.Document;
					text = debitDetail;
					strFormat.Trimming = StringTrimming.Word;
					strFormat.Alignment = StringAlignment.Near;
					size = graph.MeasureString(text, printFont, col10, strFormat);
					if(height < (int)size.Height) height = (int)size.Height;
				}
				else
				{
					debitDetail = "";
				}

				rect = new Rectangle(0, y, 0, height);

				text = count.ToString();
				rect.X = leftMargin;
				rect.Width = col1;
				PrintBoxFixed(graph, text, rect, true);
				text = element.CodeDetailTxt;
				rect.X += rect.Width;
				rect.Width = col2;
				strFormat.Alignment = StringAlignment.Near;
				graph.DrawString(text, printFont, drawBrush, rect, strFormat);
				graph.DrawRectangle(thinPen, rect);
				text = element.DetailStorageName;
				rect.X += rect.Width;
				rect.Width = col3;
				strFormat.Alignment = StringAlignment.Near;
				graph.DrawString(text, printFont, drawBrush, rect, strFormat);
				graph.DrawRectangle(thinPen, rect);
				text = element.QuontityTxt;
				rect.X += rect.Width;
				rect.Width = col4;
				PrintBoxFixed(graph, text, rect, true);
				text = element.PriceNoNdsTxt;
				rect.X += rect.Width;
				rect.Width = col5;
				PrintBoxFixed(graph, text, rect, true);
				text = element.NdsTxt;
				rect.X += rect.Width;
				rect.Width = col6;
				PrintBoxFixed(graph, text, rect, true);
				text = element.PriceTxt;
				rect.X += rect.Width;
				rect.Width = col7;
				PrintBoxFixed(graph, text, rect, true);
				text = element.SummTxt;
				rect.X += rect.Width;
				rect.Width = col8;
				PrintBoxFixed(graph, text, rect, true);
				if (element.CodeDetailIncom != 0)
				{
					text = element.DetailIncom.PriceTxt;
				}
				else
				{
					text = "---";
				}
				rect.X += rect.Width;
				rect.Width = col9;
				PrintBoxFixed(graph, text, rect, true);
				text = debitDetail;
				rect.X += rect.Width;
				rect.Width = col10;
				PrintBoxFixed(graph, text, rect, true);
				

				y += (int)size.Height;

				summ += element.Summ;
				count++;
			}
			text = "ИТОГО:";
			rect.X = leftMargin;
			rect.Y = y;
			rect.Width = col1 + col2 + col3 + col4 + col5 + col6 + col7;
			rect.Height = 6;
			strFormat.Alignment = StringAlignment.Far;
			graph.DrawString(text, printFont, drawBrush, rect, strFormat);
			text = Db.CachToTxt(summ);
			rect.X += rect.Width;
			rect.Width = col8;
			strFormat.Alignment = StringAlignment.Center;
			graph.DrawString(text, printFont, drawBrush, rect, strFormat);
			graph.DrawRectangle(thinPen, rect);

			y += 8;
			return y;
		}

		public void PrintBoxFixed(Graphics graph, string text, Rectangle rect, bool border)
		{
			StringFormat strFormat = new StringFormat();
			strFormat.Alignment = StringAlignment.Center;
			strFormat.LineAlignment = StringAlignment.Center;
			strFormat.Trimming = StringTrimming.Word;
			graph.DrawString(text, printFont, drawBrush, rect, strFormat);
			if(border == true)
				graph.DrawRectangle(thinPen, rect);
		}
	}
}
