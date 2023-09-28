using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbCardPrint.
	/// </summary>
	public class DbCardPrint
	{
		private Font printFont;
		SolidBrush drawBrush;
		//Pen boldPen;
		Pen thinPen;

		bool	it_preview;

		private Font		fontBoldTitle;
		private Font		fontDigitTitle;
		private Font		fontTitle;
		private Font		fontPrint;
		private Font		fontPrintSmall;
		private Font		fontTitleMiddle;
		private Pen			dotPen;

		private DbCard card;
		private DtCard card_dt;
		private ArrayList cardWorks;
		private ArrayList cardDetails;
		private ArrayList cardRecomend;
		private DbAuto.TO				lastTo;
		private DbCard.REP				rep;
		private DbCardAction.Analize	analize;
		private string master_name;

		// Для работы с наборами
		private ArrayList cardWorkCollections;
		private ArrayList cardWorkCodes;

		private float col1;		// Колонки для отображения работ
		private float col2;
		private float col3;
		private float col4;
		private float col5;
		private float col6;
		private float col7;

		private int col1_d;
		private int col2_d;
		private int col3_d;
		private int col4_d;
		private int col5_d;
		private int col6_d;

		private float pageHeight;				// Высота печатаемой страницы
		private float pageWidth;				// Ширина печатаемой страницы
		private float pageOffsetX;				// Отсуп слева от начала листа
		private float pageOffsetY;				// Отсуп сверху от начала листа
		private float rowIntervalSection;		// Интервал между секциями
		private float rowIntervalTitle;			// Интервал между строками, заголовок
		private float innerOffsetTitleX;		// Отсуп от границы, слева
		private float rowHeightWork;			// Высота одной строки работ
		private float autoOffsetX;				// Отсуп параметров автомобиля от названий
		private float autoRectX;				// Ширина прямоугольника с описанием автомобиля
		private float clientOffsetX;			// Отсуп параметров клиента от названий
		private float rowIntervalTitleAuto;		// Интервал между строчками в заголовке автомобиля

		// Переменные конфигурации страницы
		bool testPrint;							// Флаг, что тестируется печать
		int pages;
		int page;
		bool nextPage;
		bool worksDone;
		int workNum;
		bool worksDoneGuaranty;
		int workNumGuaranty;
		int detailNum;
		bool detailsDone;
		int detailNumGuaranty;
		bool detailsDoneGuaranty;
		int detailNumOil;
		bool detailsDoneOil;
		bool summDone;
		bool	workGuaranty	= false;
		bool	detailGuaranty	= false;

		public void CardParameters(Graphics graph)
		{
			float yPos		= 0;
			bool flag		= true;
			
			if(page != 0) return;

			pages	= 1;
			page	= 1;
			testPrint = true;
			while(flag)
			{
				graph.PageUnit = GraphicsUnit.Millimeter;
				if(!nextPage) yPos	= PrintTitle(graph, this.pageOffsetX, this.pageOffsetY);
				if(!nextPage) yPos	+= rowIntervalSection;
				if(!nextPage) yPos	= PrintWorks(graph, this.pageOffsetX, yPos);
				if(!nextPage) yPos	+= rowIntervalSection;
				if(!nextPage) yPos	= PrintDetails(graph, this.pageOffsetX, yPos);
				if(!nextPage) yPos	+= rowIntervalSection;
				if(!nextPage) yPos	= PrintCardSumm(graph, this.pageOffsetX, yPos);

				if(nextPage)
				{
					nextPage = false;
					pages ++;
					page++;
				}
				else
				{
					flag = false;
				}
			}
			page = 1;
			detailNum = 0;
			workNum = 0;
			worksDone = false;
			workNumGuaranty = 0;
			worksDoneGuaranty = false;
			detailsDone = false;
			summDone = false;
			testPrint = false;
			detailNumGuaranty = 0;
			detailsDoneGuaranty = false;
			detailNumOil = 0;
			detailsDoneOil = false;
		}

		public DbCardPrint(DbCard cardSource)
		{
			// Расстановка размеров
			col1 = 11;			// Колонки для отображения работ
			col2 = 85;
			col3 = 19;
			col4 = 19;
			col5 = 22;
			col6 = 30;
			col7 = 20;

			col1_d = 8;
			col2_d = 35;
			col3_d = 85;
			col4_d = 9;
			col5_d = 18;
			col6_d = 22;

			pageWidth				= 185.0F;
			pageHeight				= 280.0F;
			pageOffsetX				= 10.0F;
			pageOffsetY				= 15.0F;
			rowIntervalTitle		= 6.0F;
			rowIntervalSection		= 2.0F;
			innerOffsetTitleX		= 3.0F;
			rowHeightWork			= 8.0F;
			autoOffsetX				= 35.0F;
			autoRectX				= 90.0F;
			clientOffsetX			= 25.0F;
			rowIntervalTitleAuto	= 5.0F;

			card = cardSource;
			// Получаем список работ данной карточки
			cardWorks = new ArrayList();
			DbCardWork.FillList(cardWorks, card);

			// Для каждой работы загружаем ее набор
			// И создаем список работ с набором!
			cardWorkCollections = new ArrayList();
			cardWorkCodes		= new ArrayList();
			foreach(DbCardWork card_wrk in cardWorks)
			{
				DtWork wrk = DbSqlWork.Find(card_wrk.CodeWork);
				if(wrk != null)
				{
					long code_collection = (long)wrk.GetData("ССЫЛКА_КОД_КОЛЛЕКЦИЯ");
					if(code_collection != 0)
					{
						cardWorkCodes.Add(wrk.GetData("КОД_ТРУДОЕМКОСТЬ"));
						ArrayList arr = new ArrayList();
						DbSqlWorkCollectionItem.SelectInArray(arr, code_collection);
						cardWorkCollections.Add(arr);
					}
				}
			}
			// Конец загрузки набора!

			// Получаем список деталей данной карточки
			cardDetails = new ArrayList();
			DbCardDetail.FillList(cardDetails, card);

			// Получаем список рекомендаций данной карточки
			cardRecomend = new ArrayList();
			DbCardRecomend.FillArray(cardRecomend, card);

			// Догружаем представителя
			if(card.CodeRepresent != 0)
				card.Represent	= DbPartner.Find(card.CodeRepresent);

			// Догружаем мастера закрывшего заказ/наряд
			master_name = "";
			card_dt = DbSqlCard.Find(card.Number, card.Year);
			if(card_dt != null)
			{
				long code_master = (long)card_dt.GetData("МАСТЕР_КОНТРОЛЕР_КАРТОЧКА");
				DbStaff master = DbStaff.Find(code_master);
				if(master != null) master_name = master.Title;
			}
			
		
			// Проверяем по списку наличие гарантийных работ или деталей
			foreach(object o in cardWorks)
			{
				DbCardWork tmpWork = (DbCardWork)o;
				if(tmpWork.Guaranty == true) workGuaranty = true;
			}
			foreach(object o in cardDetails)
			{
				DbCardDetail tmpDetail = (DbCardDetail)o;
				if(tmpDetail.Guaranty == true) detailGuaranty = true;
			}

			// Определяем последнее ТО ремонтируемого автомобиля
			//lastTo = card.Auto.LastTo();
			bool guaranty = detailGuaranty | workGuaranty;
			if(guaranty)
			{
				rep	= card.Report1();
			}
			// Определяем время работы по наряду
			analize	= DbCardAction.AnalizeIt(card);

		}
		// The PrintPage event is raised for each page to be printed.
		// Пробуем печатать текст!
		private void pd_PrintPage(object sender, PrintPageEventArgs ev) 
		{
			float yPos		= 0;

			CardParameters(ev.Graphics);

			ev.Graphics.PageUnit = GraphicsUnit.Millimeter;
			if(card.IsWarrantOpened == false)
			{
				// if(!nextPage) yPos = PrintTitleDocument(ev.Graphics, this.pageOffsetX, this.pageOffsetY - 5);
				if(!nextPage) yPos	= PrintTitle(ev.Graphics, this.pageOffsetX, this.pageOffsetY);
				if(!nextPage) yPos	+= rowIntervalSection;
				if(!nextPage) yPos	= PrintWorks(ev.Graphics, this.pageOffsetX, yPos);
				if(!nextPage) yPos	+= rowIntervalSection;
				if(!nextPage) yPos	= PrintDetails(ev.Graphics, this.pageOffsetX, yPos);
			}
			else
			{
				if(!nextPage) yPos	= PrintTitle(ev.Graphics, this.pageOffsetX, this.pageOffsetY);
				if(!nextPage) yPos	+= rowIntervalSection;
				if(!nextPage) yPos	= PrintWorks(ev.Graphics, this.pageOffsetX, yPos);
				if(!nextPage) yPos	+= rowIntervalSection;
				if(!nextPage) yPos	= PrintDetails(ev.Graphics, this.pageOffsetX, yPos);
				if(!nextPage) yPos	+= rowIntervalSection;
				if(!nextPage) yPos	= PrintCardSumm(ev.Graphics, this.pageOffsetX, yPos);
				if(!nextPage) yPos	+= rowIntervalSection;
				if(!nextPage) yPos	= PrintTitleEnd(ev.Graphics, this.pageOffsetX, yPos);
			}

			if(nextPage)
			{
				page++;
				nextPage = false;
				ev.HasMorePages = true;
			}
			else
			{
				ev.HasMorePages = false;
				// Для удачной печати!
				page = 1;
				workNum = 0;
				detailNum = 0;
				worksDone = false;
				detailsDone = false;
				testPrint = false;
				summDone = false;
				detailNumGuaranty = 0;
				detailsDoneGuaranty = false;
				// Отметка о количестве напечатанных заказ-нярядов
				if(it_preview == false)
				{
					if(card.IsWarrantClosed == true)
						DbSqlCard.SetPrint(card.Number, card.Year);
				}
				else
					it_preview = false;
			}
		}

		// The Click event is raised when the user clicks the Print button.
		public void Print() 
		{
			try 
			{
				try 
				{
					// Подготовка всяких фонтов
					fontBoldTitle = new Font("Arial", 8, FontStyle.Bold);
					fontDigitTitle = new Font("Letter Gothic", 6);
					fontTitle = new Font("Arial", 8);
					fontPrint = new Font("Arial", 8);
					dotPen = new Pen(Color.Black, (float)1);
					dotPen.DashStyle = DashStyle.Dot;
					fontPrintSmall = new Font("Arial", 7);
					fontTitleMiddle = new Font("Arial", 8);

					thinPen = new Pen(Color.Black, (float)0.02);
					drawBrush = new SolidBrush(Color.Black);
					printFont = new Font("Arial", 8);
					PrintDocument pd = new PrintDocument();
					// Свойтсва принтера
					pd.DefaultPageSettings.Landscape = false;
					pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);
					PrintPreviewDialog preview = new PrintPreviewDialog();
					preview.Document = pd;
					it_preview	= true;
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

		private float PrintWorks(Graphics graph, float xPos, float yPos)
		{
			float y		= yPos + rowIntervalSection;
			int num		= 1;
			bool testPrintOld;
			float testY;

			if(worksDone) return yPos;
			if(nextPage) return yPos;
			if(cardWorks.Count == 0) return yPos;

			y = PrintCardWorkTitle(graph, xPos, y);


			float oilSumm = GetCardSummOil();
			if(card.IsWarrantClosed == true)
			{
				if(oilSumm != 0.0F)
				{
					if(workNum <= 1)
					{
						y = PrintCardWorkOil(graph, xPos, y, 1, oilSumm);
						num = 2;
					}
				}
			}

			float summ = 0.0F;
			foreach(object o in cardWorks)
			{
				DbCardWork wrk = (DbCardWork)o;
				if((wrk.Oil == true)&&(card.IsWarrantClosed == true)){}
				else
				{
					if(num >= workNum)
					{
						// Тестирование
						testPrintOld = testPrint;
						testPrint = true;
						testY = PrintCardWork(graph, wrk, xPos, y, num);
						if(num == cardDetails.Count)testY = PrintSummWork(graph, xPos, testY, 0);
						testPrint = testPrintOld;
						// Конец теститрование

						if(testY > pageHeight)
						{
							workNum = num;
							nextPage = true;
							return yPos;
						}

						y = PrintCardWork(graph, wrk, xPos, y, num);
						workNum = num;
					}
					summ += wrk.Summ;
					num++;
				}
			}
			if(card.IsWarrantClosed==true)
			{
				// Печать с учетом дисконта
				if(card.IsDiscount)
					y = PrintSummWorkDiscount(graph, xPos, y, GetCardSummWork(), GetCardSummDetailOil());
				else
					y = PrintSummWork(graph, xPos, y, summ + oilSumm);
			}
			else
				y = PrintSummWork(graph, xPos, y, summ);
			worksDone = true;
			return y;
		}
		public float PrintCardWorkTitle(Graphics graph, float xPos, float yPos)
		{
			string text;
			StringFormat strFormat	= new StringFormat();
			RectangleF rect			= new RectangleF();
			float x;
			float y;
			bool	tmpFlag;

			// Если заказ-наряд не закрыт (временный)
			if (card.IsWarrantClosed) tmpFlag = false;
			else
			{
				col2 = col2 - col7;
				tmpFlag = true;
			}
			
			y			= yPos;
			x			= xPos;
			rect.Y		= yPos;
			rect.X		= x;
			rect.Width	= pageWidth;
			rect.Height	= rowIntervalTitle;
			if(tmpFlag)
				text		= "РАБОТЫ                   ПОСТ №№";
			else
				text		= "РАБОТЫ";
			if(!testPrint) PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontBoldTitle);
			y			+= rowIntervalTitle;

			// Первая колонка
			x			= xPos;
			rect.X		= x;
			rect.Y		= y;
			rect.Width	= col1;
			rect.Height	= rowHeightWork;
			text		= "№";
			if(!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Center, StringAlignment.Center, fontTitle);
			// Вторая колонка
			x			+= col1;
			rect.X		= x;
			rect.Width	= col2;
			text		= "Наименование работ";
			if(!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Center, StringAlignment.Center, fontTitle);
			// Третья колонка
			x			+= col2;
			rect.X		= x;
			rect.Width	= col3;
			text		= "К-во";
			if(!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Center, StringAlignment.Center, fontTitle);
			// Четвертая колонка
			x			+= col3;
			rect.X		= x;
			rect.Width	= col4;
			text		= "ч.";
			if(!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Center, StringAlignment.Center, fontTitle);
			// Пятая колонка
			x			+= col4;
			rect.X		= x;
			rect.Width	= col5;
			text		= "Цена";
			if(!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Center, StringAlignment.Center, fontTitle);
			// Шестая колонка
			x			+= col5;
			rect.X		= x;
			rect.Width	= col6;
			text		= "Сумма";
			if(!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Center, StringAlignment.Center, fontTitle);

			// Седьмая колонка
			if(tmpFlag)
			{
				x			+= col6;
				rect.X		= x;
				rect.Width	= col7;
				text		= "Вып.";
				if(!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Center, StringAlignment.Center, fontTitle);
			}

			if (card.IsWarrantClosed) tmpFlag = false;
			else
			{
				col2 = col2 + col7;
				tmpFlag = false;
			}
			y += rowHeightWork;
			return y;
			
		}
		public float PrintCardWorkOil(Graphics graph, float xPos, float yPos, int num, float summ)
		{
			string text;
			StringFormat strFormat	= new StringFormat();
			RectangleF rect			= new RectangleF();
			RectangleF rect_add		= new RectangleF();
			float x;
			float y;
			bool	tmpFlag;

			// Если заказ-наряд не закрыт (временный)
			if (card.IsWarrantClosed) tmpFlag = false;
			else
			{
				col2 = col2 - col7;
				tmpFlag = true;
			}

			// Печатаем колонки
			text				= "Обслуживание автомобиля";
			strFormat.Trimming	= StringTrimming.Word;
			strFormat.Alignment = StringAlignment.Near;
			SizeF size			= graph.MeasureString(text, printFont, (int)col2, strFormat);
			y					= yPos;
			x					= xPos;
			rect.Y				= y;
			rect.X				= x;
			rect.Height			= size.Height;

			// Для печати дополнительного списка - дополнительное измерение
			string add;
			if(false)
			{
				add = GetWorkCollectionOil(cardWorks);
				if(add.Length > 0)
				{
					// Измеряем дополнительный список
					SizeF size_add		= graph.MeasureString(add, fontPrintSmall, (int)(col2 - 15), strFormat);
					rect.Height			+= size_add.Height;
				}
			}

			// Первая
			rect.X				= x;
			rect.Width			= col1;
			text				= num.ToString();
			strFormat.Alignment = StringAlignment.Center;
			if(!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Center, StringAlignment.Center, fontTitle);
			// Вторая колонка
			x					+= col1;
			rect.X				= x;
			rect.Width			= col2;
			text				= "Обслуживание автомобиля";
			if(!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Near, StringAlignment.Near, printFont);

			if(false)
			{
				// Дополнительный текст
				if(add.Length > 0)
				{
					rect_add.Y			= rect.Y + size.Height;
					rect_add.X			= x + 15;
					rect_add.Width		= col2 - 15;
					text				= add;
					if(!testPrint) PrintBoxFixed(graph, text, rect_add, true, StringAlignment.Near, StringAlignment.Near, fontPrintSmall);
				}
			}

			// Третья колонка
			x					+= col2;
			rect.X				= x;
			rect.Width			= col3;
			text				= "1";
			if(!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Center, StringAlignment.Center, fontTitle);
			
			// Четвертая колонка
			x					+= col3;
			rect.X				= x;
			rect.Width			= col4;
			text				= "-";
			if(!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Center, StringAlignment.Center, fontTitle);
			// Пятая колонка
			x					+= col4;
			rect.X				= x;
			rect.Width			= col5;
			text				= Db.CachToTxt(summ);
			if(!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Center, StringAlignment.Center, fontTitle);
			// Шестая колонка
			x					+= col5;
			rect.X				= x;
			rect.Width			= col6;
			text				= Db.CachToTxt(summ);
			if(!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Center, StringAlignment.Center, fontTitle);
			
			if(tmpFlag)
			{
				// Седьмая колонка
				x					+= rect.Width;
				rect.X				= x;
				rect.Width			= col7;
				text				= "";
				if(!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Center, StringAlignment.Center, fontTitle);
			}

			// Если заказ-наряд не закрыт (временный)
			if (card.IsWarrantClosed) tmpFlag = false;
			else
			{
				col2 = col2 + col7;
				tmpFlag = false;
			}

			y	+= rect.Height;
			return y;
		}
		public float PrintCardWork(Graphics graph, DbCardWork work, float xPos, float yPos, int num)
		{
			string text;
			StringFormat strFormat	= new StringFormat();
			RectangleF rect			= new RectangleF();
			RectangleF rect_add		= new RectangleF();
			float x;
			float y;

			if(work == null)
			{
				return yPos;
			}		
			bool	tmpFlag;

			// Если заказ-наряд не закрыт (временный)
			if (card.IsWarrantClosed) tmpFlag = false;
			else
			{
				col2 = col2 - col7;
				tmpFlag = true;
			}

			// Печатаем колонки
			text				= work.Name;

			// Измерение высоты строки
			strFormat.Trimming	= StringTrimming.Word;
			strFormat.Alignment = StringAlignment.Near;
			SizeF size			= graph.MeasureString(text, printFont, (int)col2, strFormat);
			y					= yPos;
			x					= xPos;
			rect.Y				= y;
			rect.X				= x;
			rect.Height			= size.Height;

			string add;
			if(false)
			{
				// Для печати дополнительного списка - дополнительное измерение
				add = GetWorkCollection(work);
				if(add.Length > 0)
				{
					// Измеряем дополнительный список
					SizeF size_add		= graph.MeasureString(add, fontPrintSmall, (int)(col2 - 15), strFormat);
					rect.Height			+= size_add.Height;
				}
			}


			// Первая
			rect.X				= x;
			rect.Width			= col1;
			text				= num.ToString();
			strFormat.Alignment = StringAlignment.Center;
			if(!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Center, StringAlignment.Center, fontTitle);
			// Вторая колонка
			x					+= col1;
			rect.X				= x;
			rect.Width			= col2;
			text				= work.Name;
			if(!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Near, StringAlignment.Near, printFont);

			if(false)
			{
				// Дополнительный текст
				if(add.Length > 0)
				{
					rect_add.Y			= rect.Y + size.Height;
					rect_add.X			= x + 15;
					rect_add.Width		= col2 - 15;
					text				= add;
					if(!testPrint) PrintBoxFixed(graph, text, rect_add, true, StringAlignment.Near, StringAlignment.Near, fontPrintSmall);
				}
			}

			// Третья колонка
			x					+= col2;
			rect.X				= x;
			rect.Width			= col3;
			text				= work.QuontityTxt;
			if(!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Center, StringAlignment.Center, fontTitle);
			if(work.Guaranty)
			{
				x					+= col3;
				rect.X				= x;
				rect.Width			= col4 + col5 + col6;
				text				= "Гарантия";
				if(!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Center, StringAlignment.Center, fontTitle);
			}
			else
			{
				// Четвертая колонка
				x					+= col3;
				rect.X				= x;
				rect.Width			= col4;
				text				= work.ValTxt;
				if(!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Center, StringAlignment.Center, fontTitle);
				// Пятая колонка
				x					+= col4;
				rect.X				= x;
				rect.Width			= col5;
				text				= work.PriceTxt;
				if(!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Center, StringAlignment.Center, fontTitle);
				// Шестая колонка
				x					+= col5;
				rect.X				= x;
				rect.Width			= col6;
				text				= work.SummTxt;
				if(!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Center, StringAlignment.Center, fontTitle);
			}
			if(tmpFlag)
			{
				// Седьмая колонка
				x					+= rect.Width;
				rect.X				= x;
				rect.Width			= col7;
				text				= "";
				if(!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Center, StringAlignment.Center, fontTitle);
			}

			// Если заказ-наряд не закрыт (временный)
			if (card.IsWarrantClosed) tmpFlag = false;
			else
			{
				col2 = col2 + col7;
				tmpFlag = false;
			}

			y	+= rect.Height;
			return y;
		}

		public float PrintSummWork(Graphics graph, float xPos, float yPos, float summ)
		{
			string text;
			RectangleF rect = new RectangleF();
			// Первая колонка
			rect.Y		= yPos;
			rect.X		= xPos;
			rect.Height	= rowHeightWork;
			rect.Width	= col1 + col2 + col3 + col4 + col5;
			text		= "К ОПЛАТЕ:";
			if(!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Far, StringAlignment.Center, fontBoldTitle);
			// Вторая колонка
			rect.X		+= col1 + col2 + col3 + col4 + col5;
			rect.Width	= col6;
			text		= Db.CachToTxt(summ);
			if(!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Center, StringAlignment.Center, fontTitle);

			return yPos + rowHeightWork;
		}

		public float PrintSummWorkDiscount(Graphics graph, float xPos, float yPos, float summ, float oilSumm)
		{
			float summ_work			= summ;
			float summ_liquid		= oilSumm;
			float discount			= card.DiscountWork;
			float summ_discount		= (float)Math.Round(summ_work / 100 * discount);
			float summ_whole		= summ_work + summ_liquid;
			float summ_pay			= summ_whole - summ_discount;
			string text;
			RectangleF rect = new RectangleF();
			// Нулевая колонка
			rect.Y		= yPos;
			rect.X		= xPos;
			rect.Height	= rowHeightWork;

			rect.Width	= col1 + col2 / 2;// + col3 + col4 + col5;
			text		= "СУММА: " + Db.CachToTxt(summ_whole)/* + "    СКИДКА: " + Db.CachToTxt(summ_discount) */;
			if(!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Near, StringAlignment.Center, fontBoldTitle);

			rect.X		= xPos + rect.Width;
			rect.Width	= /*col1 + */ col2 / 2;// + col3 + col4 + col5;
			text		= /*"СУММА: " + Db.CachToTxt(summ_whole) + */ "СКИДКА: " + Db.CachToTxt(summ_discount) ;
			if(!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Near, StringAlignment.Center, fontBoldTitle);

			// Первая колонка
			rect.Y		= yPos;
			rect.X		= xPos;
			rect.Height	= rowHeightWork;
			rect.Width	= col1 + col2 + col3 + col4 + col5;
			text		= "К ОПЛАТЕ:";
			if(!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Far, StringAlignment.Center, fontBoldTitle);
			// Вторая колонка
			rect.X		+= col1 + col2 + col3 + col4 + col5;
			rect.Width	= col6;
			text		= Db.CachToTxt(summ_pay);
			if(!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Center, StringAlignment.Center, fontTitle);

			return yPos + rowHeightWork;
		}

		private float PrintDetails(Graphics graph, float xPos, float yPos)
		{
			float y		= yPos + rowIntervalSection;
			int num		= 1;
			float		testY;
			bool		testPrintOld;

			if(nextPage) return yPos;
			if(cardDetails.Count == 0) return yPos;
			if(detailsDone) return yPos;

			// Тестирование
			testPrintOld = testPrint;
			testPrint = true;
			testY = PrintCardDetailTitle(graph, xPos, y);
			testY = PrintCardDetail(graph, (DbCardDetail)cardDetails[0], xPos, testY, num);
			if (cardDetails.Count == 1) testY = PrintSummDetail(graph, xPos, testY, 0);
			testPrint = testPrintOld;
			// Конец теститрование
			if(testY > pageHeight)
			{
				detailNum = num;
				nextPage = true;
				return yPos;
			}

			y = PrintCardDetailTitle(graph, xPos, y);
			float summ = 0.0F;
			foreach(object o in cardDetails)
			{
				DbCardDetail dtl = (DbCardDetail)o;
				if((dtl.Oil == true)&&(card.IsWarrantClosed == true)){}
				else
				{

					if(num >=detailNum)
					{
						// Тестирование
						testPrintOld = testPrint;
						testPrint = true;
						testY = PrintCardDetail(graph, dtl, xPos, y, num);
						if(num == cardDetails.Count)testY = PrintSummDetail(graph, xPos, testY, 0);
						testPrint = testPrintOld;
						// Конец теститрование

						if(testY > pageHeight)
						{
							detailNum = num;
							nextPage = true;
							return yPos;
						}
						y = PrintCardDetail(graph, dtl, xPos, y, num);
						detailNum = num;
					}
					summ += dtl.Summ;
					num++;
				}
			}
			y = PrintSummDetail(graph, xPos, y, summ);
			detailsDone = true;
			return y;
		}
		public float PrintCardDetailTitle(Graphics graph, float xPos, float yPos)
		{
			string text;
			StringFormat strFormat	= new StringFormat();
			RectangleF rect			= new RectangleF();
			float x;
			float y;

			y			= yPos;
			x			= xPos;
			rect.Y		= yPos;
			rect.X		= x;
			rect.Width	= pageWidth;
			rect.Height	= rowIntervalTitle;

			text		= "ДЕТАЛИ";
			if(!testPrint) PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontBoldTitle);
			y			+= rowIntervalTitle;

			// Первая колонка
			x			= xPos;
			rect.X		= x;
			rect.Y		= y;
			rect.Width	= col1_d;
			rect.Height	= rowHeightWork;
			text		= "№";
			if(!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Center, StringAlignment.Center, fontTitle);
			// Вторая колонка
			x			+= col1_d;
			rect.X		= x;
			rect.Width	= col2_d;
			text		= "Код";
			if(!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Center, StringAlignment.Center, fontTitle);
			// Третья колонка
			x			+= col2_d;
			rect.X		= x;
			rect.Width	= col3_d;
			text		= "Наименование";
			if(!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Center, StringAlignment.Center, fontTitle);
			// Четвертая колонка
			x			+= col3_d;
			rect.X		= x;
			rect.Width	= col4_d;
			text		= "Кол-во";
			if(!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Center, StringAlignment.Center, fontTitle);
			// Пятая колонка
			x			+= col4_d;
			rect.X		= x;
			rect.Width	= col5_d;
			text		= "Цена";
			if(!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Center, StringAlignment.Center, fontTitle);
			// Шестая колонка
			x			+= col5_d;
			rect.X		= x;
			rect.Width	= col6_d;
			text		= "Сумма";
			if(!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Center, StringAlignment.Center, fontTitle);

			y += rowHeightWork;
			return y;
		}
		public float PrintCardDetail(Graphics graph, DbCardDetail detail, float xPos, float yPos, int num)
		{
			string text;
			StringFormat strFormat	= new StringFormat();
			RectangleF rect			= new RectangleF();
			float x;
			float y;

			if(detail == null)
			{
				return yPos;
			}
			// Печатаем колонки
			text				= detail.DetailNameTxt; 
			// Отбивка пустого
			if(text.Length == 0) text = "???";
			// Конец отбивка пустой детали
			strFormat.Trimming	= StringTrimming.Word;
			strFormat.Alignment = StringAlignment.Near;
			SizeF size			= graph.MeasureString(text, printFont, (int)col3_d, strFormat);
			y					= yPos;
			x					= xPos;
			rect.Y				= y;
			rect.X				= x;
			rect.Height			= size.Height;

			// Первая
			rect.X				= x;
			rect.Width			= col1_d;
			text				= num.ToString();
			strFormat.Alignment = StringAlignment.Center;
			if(!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Center, StringAlignment.Center, fontTitle);
			// Вторая колонка
			x					+= col1_d;
			rect.X				= x;
			rect.Width			= col2_d;
			text				= detail.CodeDetailTxt;
			if(!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Center, StringAlignment.Center, printFont);
			// Третья колонка
			x					+= col2_d;
			rect.X				= x;
			rect.Width			= col3_d;
			text				= detail.DetailNameTxt;
			if(!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Near, StringAlignment.Center, printFont);
			// Четвертая колонка
			x					+= col3_d;
			rect.X				= x;
			rect.Width			= col4_d;
			text				= detail.QuontityTxt;
			if(!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Center, StringAlignment.Center, fontTitle);
			if(detail.Guaranty || detail.Outer)
			{
				x					+= col4_d;
				rect.X				= x;
				rect.Width			= col5_d + col6_d;
				if(detail.Guaranty) text = "Гарантия";
				else	text = "Клиент";
				if(!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Center, StringAlignment.Center, fontTitle);
			}
			else
			{
				// Пятая колонка
				x					+= col4_d;
				rect.X				= x;
				rect.Width			= col5_d;
				text				= detail.PriceTxt;
				if(!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Center, StringAlignment.Center, fontTitle);
				// Шестая колонка
				x					+= col5_d;
				rect.X				= x;
				rect.Width			= col6_d;
				text				= detail.SummTxt;
				if(!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Center, StringAlignment.Center, fontTitle);
			}

			y	+= rect.Height;
			return y;
		}
		public float PrintSummDetail(Graphics graph, float xPos, float yPos, float summ)
		{
			string text;
			RectangleF rect = new RectangleF();
			// Первая колонка
			rect.Y		= yPos;
			rect.X		= xPos;
			rect.Height	= rowHeightWork;
			rect.Width	= col1_d + col2_d + col3_d + col4_d + col5_d;

			text		= "К ОПЛАТЕ:";
			if(!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Far, StringAlignment.Center, fontBoldTitle);
			// Вторая колонка
			rect.X		+= col1_d + col2_d + col3_d + col4_d + col5_d;
			rect.Width	= col6_d;
			text		= Db.CachToTxt(summ);
			if(!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Center, StringAlignment.Center, fontTitle);

			return yPos + rowHeightWork;
		}

		private float PrintCardSumm(Graphics graph, float xPos, float yPos)
		{
			float y		= yPos;// + rowIntervalSection;
			bool testPrintOld;

			if(summDone) return yPos;

			// Тестирование
			testPrintOld = testPrint;
			testPrint = true;
			y = PrintSumm(graph, xPos, y, 0);
			testPrint = testPrintOld;
			// Конец тестирование
			if(y > pageHeight)
			{
				nextPage = true;
				return yPos;
			}

			y		= yPos;
			// Подсчет суммы к оплате по заказ-наряду
			float oilSumm		= GetCardSummOil();	// Оплата за масла
			float summ_detail	= 0.0F;				// Сумма за детали
			float summ_work		= 0.0F;				// Сумма за работы
			float summ			= 0.0F;				// Общая сумма
			foreach(object o in cardDetails)
			{
				DbCardDetail dtl = (DbCardDetail)o;
				if(card.IsWarrantClosed == false)
				{
					summ_detail += dtl.Summ;
				}
				else
				{
					
					summ_detail += dtl.Summ;
				}
			}
			foreach(object o in cardWorks)
			{
				DbCardWork wrk = (DbCardWork)o;
				summ_work += wrk.Summ;
			}
			if(card.IsWarrantClosed == false)
			{
				summ = summ_detail + summ_work;
			}
			else
			{
				if(card.DiscountWork > 0)
				{
					float discount			= card.DiscountWork;
					float summ_discount		= (float)Math.Round(summ_work / 100 * discount);
					summ					= summ_work + summ_detail - summ_discount;
				}
				else
				{
					summ = summ_detail + summ_work;
				}
			}
			y = PrintSumm(graph, xPos, y, summ);
			summDone = true;
			return y;
		}

		private float GetCardSummOil()
		{
			float summ = 0.0F;
			foreach(object o in cardDetails)
			{
				DbCardDetail dtl = (DbCardDetail)o;
				if(dtl.Oil)
					summ += dtl.Summ;
			}
			foreach(object o in cardWorks)
			{
				DbCardWork wrk = (DbCardWork)o;
				if(wrk.Oil)
					summ += wrk.Summ;
			}
			return summ;
		}

		private float GetCardSummWork()
		{
			float summ = 0.0F;
			foreach(object o in cardWorks)
			{
				DbCardWork wrk = (DbCardWork)o;
				summ += wrk.Summ;
			}
			return summ;
		}

		private float GetCardSummDetailOil()
		{
			float summ = 0.0F;
			foreach(object o in cardDetails)
			{
				DbCardDetail dtl = (DbCardDetail)o;
				if(dtl.Oil)
					summ += dtl.Summ;
			}
			return summ;
		}

		public float PrintSumm(Graphics graph, float xPos, float yPos, float summ)
		{
			string text;
			RectangleF rect = new RectangleF();
			// Первая колонка
			rect.Y		= yPos;
			rect.X		= xPos;
			rect.Height	= rowHeightWork;
			rect.Width	= col1_d + col2_d + col3_d + col4_d;
			text		= "ВСЕГО К ОПЛАТЕ:";
			if(!testPrint)PrintBoxFixed(graph, text, rect, true, StringAlignment.Far, StringAlignment.Center, fontBoldTitle);
			// Вторая колонка
			rect.X		+= col1_d + col2_d + col3_d + col4_d;
			rect.Width	=  col5_d + col6_d;
			text		= Db.CachToTxt(summ);
			if(!testPrint)PrintBoxFixed(graph, text, rect, true, StringAlignment.Center, StringAlignment.Center, fontTitle);

			return yPos + rowHeightWork;
		}

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

		// Шапка заказ наряда - Автомобиль
		public float PrintTitleAuto(Graphics graph, float xPos, float yPos)
		{
			RectangleF	rect = new RectangleF();
			string		text;
			float		y;

			rect.X		= xPos + innerOffsetTitleX;
			rect.Y		= yPos;
			rect.Height = rowIntervalTitle;
			rect.Width	= pageWidth;
			y			= yPos;

			if(card.Auto == null)
			{
				text	= "АВТОМОБИЛЬ : ДАННЫХ НЕТ";
				if(!testPrint) PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontBoldTitle);
				return yPos + 10;
			}
			text	= "АВТОМОБИЛЬ";
			if(!testPrint) PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontBoldTitle);
			// Дата продажи
			if(card.Auto.IsSellDate)
			{
				rect.Width	= autoRectX - 5;
				text	= "д/п " + Db.DateToTxt(card.Auto.SellDate);
				if(!testPrint) PrintBoxFixed(graph, text, rect, false, StringAlignment.Far, StringAlignment.Center, fontTitle);
				rect.Width	= pageWidth;
			}
			// Модель
			rect.Y = rect.Y + rowIntervalTitle;
			text	= "МОДЕЛЬ : ";
			if(!testPrint) PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontTitleMiddle);
			rect.X = xPos + innerOffsetTitleX + autoOffsetX;
			text	= card.Auto.AutoModel.Model;
			if(!testPrint) PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontDigitTitle);
			// VIN
			//rect.Y = rect.Y + rowIntervalTitle;
			rect.Y = rect.Y + rowIntervalTitleAuto;
			rect.X = xPos + innerOffsetTitleX;
			text	= "	VIN : ";
			if(!testPrint) PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontTitleMiddle);
			rect.X = xPos + innerOffsetTitleX + autoOffsetX;
			text	= card.Auto.Vin;
			if(!testPrint) PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontDigitTitle);
			//  КУЗОВ
			//rect.Y = rect.Y + rowIntervalTitle;
			rect.Y = rect.Y + rowIntervalTitleAuto;
			rect.X = xPos + innerOffsetTitleX;
			text	= "	КУЗОВ № : ";
			if(!testPrint) PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontTitleMiddle);
			rect.X = xPos + innerOffsetTitleX + autoOffsetX;
			text	= card.Auto.BodyNo;
			if(!testPrint) PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontDigitTitle);
			// Двигатель
			//rect.Y = rect.Y + rowIntervalTitle;
			rect.Y = rect.Y + rowIntervalTitleAuto;
			rect.X = xPos + innerOffsetTitleX;
			text	= "	ДВИГАТЕЛЬ № : ";
			if(!testPrint) PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontTitleMiddle);
			rect.X = xPos + innerOffsetTitleX + autoOffsetX;
			text	= card.Auto.EngineNo;
			if(!testPrint) PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontDigitTitle);
			// Номерной знак
			//rect.Y = rect.Y + rowIntervalTitle;
			rect.Y = rect.Y + rowIntervalTitleAuto;
			rect.X = xPos + innerOffsetTitleX;
			text	= "	РЕГ. ЗНАК: ";
			if(!testPrint) PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontTitleMiddle);
			rect.X = xPos + innerOffsetTitleX + autoOffsetX - 10;
			text	= card.Auto.SignNo;
			if(!testPrint) PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontDigitTitle);
			rect.X = xPos + innerOffsetTitleX + autoOffsetX - 15 + 30;
			text	= "	ПРОБЕГ: ";
			if(!testPrint) PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontTitleMiddle);
			rect.X = xPos + innerOffsetTitleX + autoOffsetX - 15 + 35 + 15;
			if(card.IsWarrantOpened)
				text	= card.RunTxt;
			else
				text	= "";
			if(!testPrint) PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontDigitTitle);

			// Форма оплаты
			rect.Y = rect.Y + rowIntervalTitleAuto - 1;
			rect.X = xPos + innerOffsetTitleX;
			if(card.Cashless == true)
			{
				text	= "Форма оплаты: безналичный расчет";
				if(!testPrint) PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontTitleMiddle);
			}
			else
			{
				if(card.InnerWarranty == true)
				{
					text	= "Внутренний - расчет не предусмотрен";
					if(!testPrint) PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontTitleMiddle);
				}
			}

			/*
			// Дописываем ТО
			if (lastTo.to == 0)
				text	= "ТО НЕТ";
			else
				text	= "ТО №" + lastTo.to.ToString();
			rect.Y = rect.Y + rowIntervalTitle;
			rect.X = xPos + innerOffsetTitleX;
			if(!testPrint) PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontTitle);
			*/

			y = rect.Y + rowIntervalTitle;
			rect.Y = yPos;
			rect.X = xPos;
			rect.Height	= y - rect.Y;
			//rect.Width	= pageWidth;
			rect.Width	= autoRectX;
			if(!testPrint) graph.DrawRectangle(thinPen, rect.X, rect.Y, rect.Width, rect.Height);

			return y;
		}

		// Шапка заказ наряда - Клиент
		public float PrintTitleClient(Graphics graph, float xPos, float yPos)
		{
			RectangleF	rect = new RectangleF();
			string		text;
			float		y;

			rect.X		= xPos + innerOffsetTitleX;
			rect.Y		= yPos;
			rect.Height = rowIntervalTitle;
			rect.Width	= pageWidth - autoRectX - 5;
			y			= yPos;

			if(card.Partner == null)
			{
				text	= "ВЛАДЕЛЕЦ : ДАННЫХ НЕТ";
				if(!testPrint) PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontTitle);
				return yPos + 10;
			}
			// Клиент печатается  - наименование/ФИО, адрес, телефон 
			text	= "ВЛАДЕЛЕЦ";
			if(!testPrint) PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontTitle);
			// НАИМЕНОВАНИЕ/ФИО
			rect.Y = rect.Y + rowIntervalTitle;
			//if(card.Partner.Juridical)
			//	text	= "НАИМЕНОВАНИЕ : ";
			//else
			//	text	= "Ф.И.О. : ";
			//if(!testPrint) PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontTitle);
			//rect.X = xPos + innerOffsetTitleX + clientOffsetX;
			text	= card.Partner.Title;
			//rect.Width	= pageWidth - autoRectX - 5 - 35;
			//rect.Height	= rowIntervalTitle * 4;
			//if(!testPrint) y = PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontDigitTitle);
			// АДРЕС
			//rect.Y = rect.Y + rowIntervalTitle * 2;
			//rect.Height	= rowIntervalTitle;
			//rect.X = xPos + innerOffsetTitleX;
			//rect.Width	= pageWidth - autoRectX - 5;
			//text	= "АДРЕС : ";
			//if(!testPrint) PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontTitle);
			//rect.X = xPos + innerOffsetTitleX + clientOffsetX;
			//rect.Y	= y;
			if(card.Partner.Juridical)
				text	+= "\n" + card.Partner.AddressJuridical;
			else
				text	+= "\n" + card.Partner.AddressRegistration;
			//rect.Width	= pageWidth - autoRectX - 5 - 35;
			//rect.Height	= rowIntervalTitle * 2;
			//if(!testPrint) y = PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontDigitTitle);
			//  ТЕЛЕФОН
			//rect.Y = rect.Y + rowIntervalTitle * 2;
			//rect.X = xPos + innerOffsetTitleX;
			//rect.Width	= pageWidth - autoRectX - 5;
			//rect.Height	= rowIntervalTitle;
			//text	= "	ТЕЛЕФОН : ";
			//if(!testPrint) PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontTitle);
			//rect.X = xPos + innerOffsetTitleX + clientOffsetX;
			if(card.Partner.Juridical)
			{
				if(card.Partner.ContactPhone.Length != 0)
				{
					text	+= "; Тел. " + card.Partner.ContactPhone;
				}
			}
			else
			{
				if(card.Partner.Phone.Length != 0)
				{
					text	+= "; Тел. " + card.Partner.Phone;
				}
			}
			//rect.Y	= y;
			rect.Height	= rowIntervalTitle * 5;

			// Дополнение для представителя
			if(card.CodeRepresent != 0)
			{
				text	+= "\nПРЕДСТАВИТЕЛЬ:\n";
				text	+= card.Represent.Title;
				text	+= "\n" + card.Represent.AddressRegistration;
				if(card.Represent.Phone.Length != 0)
					text	+= "; Тел. " + card.Represent.Phone;
				text	+= "\nДОКУМЕНТ : " + card.RepresentDocument;
			}

			if(!testPrint) PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Near, fontPrint /*fontDigitTitle*/);

			//y = rect.Y + rowIntervalTitle;
			rect.Y = yPos;
			rect.X = xPos;
			y = rect.Y + rowIntervalTitle * 6;
			rect.Height	= y - rect.Y;
			//rect.Width	= pageWidth;
			rect.Width	= pageWidth - autoRectX - 5;
			if(!testPrint) graph.DrawRectangle(thinPen, rect.X, rect.Y, rect.Width, rect.Height);

			return y;
		}

		// Шапка заказ наряда - Карточка
		public float PrintTitleCard(Graphics graph, float xPos, float yPos)
		{
			RectangleF	rect = new RectangleF();
			string		text;
			float		y;

			rect.X		= xPos + innerOffsetTitleX;
			rect.Y		= yPos;
			rect.Height = rowIntervalTitle;
			rect.Width	= pageWidth;
			y			= yPos;

			//  НОМЕР ЗАКАЗ НАРЯДА, ДАТЫ ОТКРЫТИЯ И ЗАКРЫТИЯ
			if(card.WarrantNumber <= 0)
				text	= "";
			else
				text	= "ЗАКАЗ-НАРЯД № " + card.WarrantNumberTxt;
			if(!testPrint)PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontBoldTitle);

			rect.X = xPos;
			rect.Width = pageWidth;// - innerOffsetTitleX;
			if(card.WarrantNumber <= 0)
				text	= "";
			else
			{
				text	= "Открыт : " + card.WarrantOpenTxt;
				if (card.ActionCode == (int)DbCardAction.ActionCodes.Close)
				{
					text	+= " Закрыт : " + card.WarrantCloseTxt;
				}
			}
			if(!testPrint) PrintBoxFixed(graph, text, rect, false, StringAlignment.Far, StringAlignment.Center, fontTitle);

			// Теперь печатаем во всех случаях
			rect.Y = rect.Y + rowIntervalTitle;

			// Дописываем строчку про гарантию, если она есть
			text = "";
			bool guaranty = detailGuaranty | workGuaranty;
			if(guaranty || card.ActionCode != (int)DbCardAction.ActionCodes.Close)
			{
				//rect.Y = rect.Y + rowIntervalTitle;
				text = "    ГАРАНТИЯ : " + card.GuarantyTypeTxt;
				// ПО ТО
				/*
				// Дописываем ТО
				if (lastTo.to == 0)
					text	+= "    " + "ТО НЕТ";
				else
					text	+= "    " + "ТО №" + lastTo.to.ToString() + "(" + lastTo.run.ToString() + " ," + lastTo.date.ToShortDateString() + ")";
				*/
				// Дописываем ТО
				if(rep.error == true)
				{
					text += "   *";
				}
				else
				{
					if (rep.to == 0)
						text	+= "    " + "ТО НЕТ";
					else
						text	+= "    " + "ТО №" + rep.to.ToString() + "(" + rep.run.ToString() + " ," + rep.date.ToShortDateString() + ")";

					text += "    90:(" + rep.count + ", " + rep.evrg_run + ")";
				}

			//	if(!testPrint) PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontPrintSmall);
				
			}

			// Информация по эффективности работы
			if(text.Length != 0) text += " / ";
			text += analize.stop_count.ToString() + "." + Db.SpanToShortTxt(analize.first_stop_span) + "." + Db.SpanToShortTxt(analize.first_start_span) + "." + Db.SpanToShortTxt(analize.work_time_span);

			// Теперь печатаем во всех случаях
			if(!testPrint) PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontPrintSmall);

			y = rect.Y + rowIntervalTitle;
			rect.Y = yPos;
			rect.X = xPos;
			rect.Height	= y - rect.Y;
			rect.Width	= pageWidth;
			if(!testPrint) graph.DrawRectangle(thinPen, rect.X, rect.Y, rect.Width, rect.Height);

			return y;
		}

		// Печать шапки
		public float PrintTitle(Graphics graph, float xPos, float yPos)
		{
			string text;
			float x;
			float y;
			RectangleF rect = new RectangleF(0, 0, 0, 0);

			rect.X = xPos;
			rect.Y = yPos;
			rect.Height = 10;
			rect.Width = 60;

			rect.X = pageWidth - 60;
			rect.Y = yPos - 10;
			text = "Страница " + page.ToString() + " из " + pages.ToString();
			if(!testPrint)	PrintBoxFixed(graph, text, rect, false);

			// Рисуем картинку
			//rect.X = xPos;
			//rect.Y = yPos;
			//rect.Width = 40;
			//text = "ООО \"АВТО-1\"";
			//if(!testPrint)	PrintBoxFixed(graph, text, rect, false);

			rect.X = xPos;
			rect.Y = yPos - 10;
			rect.Width = 40;
			rect.Height = 20;
			Image carImage = Image.FromFile("car.bmp");
			if(!testPrint) graph.DrawImage(carImage, rect);

			rect.Height = 10;
			rect.Y = yPos;
			rect.X = xPos;
			rect.Width = 60;
			rect.X = rect.X + rect.Width;
			rect.Width = 120;

			// Загрузка данных из INI файла
			text = FileIni.GetParameter("print.ini", "#ADDRESS_BLOCK_OLD");
			//text = "г.Новосибирск ул.Русская, 48 т. (383)332-02-92";

			if(!testPrint)	PrintBoxFixed(graph, text, rect, false);
			//if(!testPrint)	graph.DrawLine(thinPen, xPos, rect.Bottom, xPos + 60 + 120, rect.Bottom);
			//y = yPos + rect.Height;
			y = yPos + rect.Height;

			//if(!testPrint)	graph.DrawLine(thinPen, xPos, rect.Bottom, xPos + 60 + 120, rect.Bottom);
			//y = yPos + rect.Height;

			y = PrintTitleCard(graph, this.pageOffsetX, y);
			y += rowIntervalSection;
			
			if(page > 1) return y;

			PrintTitleClient(graph, this.pageOffsetX + autoRectX + 5, y);
			y = PrintTitleAuto(graph, this.pageOffsetX, y);
			
			//y += rowIntervalSection;
			//y = PrintTitleClient(graph, this.pageOffsetX, y);

			return y;
		}

		// Завершение заказ няряда
		public float PrintTitleEnd(Graphics graph, float xPos, float yPos)
		{
			float y;
			float testY;
			bool testPrintOld;
			y = yPos;

			if(card.IsWarrantClosed != true)
			{
				// Временная печать
				while( y < pageHeight)
				{
					if(!testPrint)graph.DrawLine(thinPen, xPos, y, xPos + pageWidth, y);
					y += 8F;
				}
				return y;
			}

			// Тестирование
			testPrintOld = testPrint;
			testPrint = true;
			testY = PrintTitleEnd1(graph, xPos, y);
			testPrint = testPrintOld;
			if(testY > pageHeight)
			{
				nextPage = true;
				return 0;
			}
			// Конец тестирование

			if(pageHeight - testY > 20)
			{
				if(!testPrint)graph.DrawLine(thinPen, xPos, yPos + 5, xPos + pageWidth, yPos + 5);
				if(!testPrint)graph.DrawLine(thinPen, xPos + pageWidth, yPos + 5, xPos, yPos + pageHeight - testY - 5);
				if(!testPrint)graph.DrawLine(thinPen, xPos + pageWidth, yPos + pageHeight - testY - 5, xPos, yPos + pageHeight - testY - 5);
				if(!testPrint)graph.DrawLine(thinPen, xPos + pageWidth / 2 - 50, yPos + (pageHeight - testY) / 2, xPos + pageWidth / 2 + 50, yPos + (pageHeight - testY) / 2);
			}
			y = PrintTitleEnd1(graph, xPos, yPos + pageHeight - testY);

			return y;
		}
		public float PrintTitleEnd1(Graphics graph, float xPos, float yPos)
		{
			RectangleF	rect = new RectangleF();
			string		text;
			float		y;
			SizeF		size;
			StringFormat strFormat	= new StringFormat();
			float offset = 0;


			// Забиваем строку/несколько строк рекомендаций, если они есть
			rect.X		= xPos;
			rect.Y		= yPos;
			rect.Width	= pageWidth;
			y			= yPos;
			if(cardRecomend.Count > 0)
			{
				// есть рекомендации
				text = "Рекомендации: ";
				foreach(object o in cardRecomend)
				{
					DbCardRecomend element = (DbCardRecomend)o;
					text += element.Recomendation + ";";
				}
				strFormat.Trimming	= StringTrimming.Word;
				strFormat.Alignment = StringAlignment.Near;
				size			= graph.MeasureString(text, fontPrint, (int)pageWidth, strFormat);
				offset			= size.Height;
				rect.Height		= offset;
				if(!testPrint) PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontPrint);
			}

			rect.X		= xPos;
			rect.Y		= yPos + offset;
			rect.Height = rowIntervalTitle;
			rect.Width	= pageWidth;
			y			= yPos + offset;

			//text	+= "Качество ремонта проверил, автомобиль технически исправен";
			text	= "Качество ремонта проверил, автомобиль технически исправен";
			if(!testPrint) PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontPrint);

			// Подпечатка расшифровки фамилии мастера
			if(master_name.Length != 0)
			{
				text	= master_name;
				if(!testPrint) PrintBoxFixed(graph, text, rect, false, StringAlignment.Far, StringAlignment.Center, fontPrint);
			}

			if(!testPrint) graph.DrawLine(thinPen, rect.X, rect.Y + rect.Height, rect.X + rect.Width, rect.Y + rect.Height);
			rect.Y		= rect.Y + rect.Height;
			text	= "(Подпись)";
			rect.X		= 120;
			if(!testPrint) PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontPrint);
			text	= "(ФИО)";
			rect.X		= 160;
			if(!testPrint) PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontPrint);

			rect.X = xPos;
			rect.Y		= rect.Y + rect.Height;
			text	= "Автомобиль из ремонта принял, претензий не имею";
			if(!testPrint) PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontPrint);
			if(!testPrint) graph.DrawLine(thinPen, rect.X, rect.Y + rect.Height, rect.X + rect.Width, rect.Y + rect.Height);
			rect.Y		= rect.Y + rect.Height;
			text	= "(Подпись клиента)";
			rect.X		= 120;
			if(!testPrint) PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontPrint);
			text	= "(Дата)";
			rect.X		= 160;
			if(!testPrint) PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontPrint);

			rect.Y = rect.Y + rect.Height;
			rect.X = xPos;
			rect.Height = rowIntervalTitle * 3;
			text	=  "Гарантия на слесарные, электрические, контрольно-осмотровые(диагностические), регулировочные работы - 30 дней или 1000 км пробега.";
			text	+=  " Все устные договоренности и обязательства, взятые на себя кем-либо из сотрудников технического центра и не отмеченные в данном заказ-наряде являются недействительными.";
			if(!testPrint) PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontPrint);

			rect.Height = rowIntervalTitle;
			rect.Y = rect.Y + rect.Height;
			rect.Y = rect.Y + rect.Height;
			rect.X = xPos;

			if(!testPrint) graph.DrawLine(dotPen, rect.X, rect.Y + rect.Height, rect.X + rect.Width, rect.Y + rect.Height);
			rect.X	= xPos;
			rect.Y	= rect.Y + rect.Height;

			// Дополнительные данные для пропуска
			string destination = "";
			if(card.CodeWorkshop != 0)
			{
				DtWorkshop workshop = DbSqlWorkshop.Find(card.CodeWorkshop);
				if(workshop != null)
				{
					destination = (string)workshop.GetData("ПРОПУСК_НАЗНАЧЕНИЕ");
				}
			}
			text	= "ЗАКАЗ-НАРЯД " + card.WarrantNumberTxt;
			if(destination != "") text += " (Для " + destination + ")";

			if(!testPrint) PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontBoldTitle);
			rect.Y	= rect.Y + rect.Height;
			text	= card.Auto.ModelTxt + " VIN " + card.Auto.Vin + "            Рег. знак : " + card.Auto.SignNo;
			if(!testPrint) PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontBoldTitle);

			/*
			float summ = 0.0F;
			foreach(object o in cardDetails)
			{
				DbCardDetail dtl = (DbCardDetail)o;
				summ += dtl.Summ;
			}
			foreach(object o in cardWorks)
			{
				DbCardWork wrk = (DbCardWork)o;
				summ += wrk.Summ;
			}
			rect.Y	= rect.Y + rect.Height;
			text	= "Оплата произведена " + Db.CachToTxt(summ) + " руб";
			*/
			rect.Y	= rect.Y + rect.Height;
			text	= "Оплата произведена полностью" + card.PayTypeTxt;
			if(!testPrint) PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontTitle);
			rect.Y		= rect.Y + rect.Height;
			if(!testPrint) graph.DrawLine(thinPen, rect.X, rect.Y + rect.Height, rect.X + rect.Width, rect.Y + rect.Height);
			rect.Y		= rect.Y + rect.Height;
			text	= "(Дата)";
			rect.X		= 40;
			if(!testPrint) PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontPrint);
			text	= "(Подпись)";
			rect.X		= 160;
			if(!testPrint) PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontPrint);

			return rect.Y + rect.Height;
		}


		public float PrintBoxedText(Graphics graph, string text, float xPos, float yPos, float height, float width, int length)
		{
			RectangleF rect = new Rectangle(0, 0, 0, 0);
			rect.X = xPos;
			rect.Y = yPos;
			rect.Height = height;
			rect.Width = width;

			int count = 1;
			foreach(char ch in text.ToCharArray())
			{
				if(count <= length)
				{
					string chStr = (string)ch.ToString();
					PrintBoxFixed(graph, chStr, rect);
					rect.X += width;
				}
				count++;
			}
			while(count <= length)
			{
				PrintBoxFixed(graph, "", rect);
				rect.X += width;
				count++;
			}
			return rect.X;
		}

		// The Click event is raised when the user clicks the Print button.
		public void PrintBuhg() 
		{
			try 
			{
				try 
				{
					// Подготовка всяких фонтов
					fontBoldTitle = new Font("Arial", 12, FontStyle.Bold);
					fontDigitTitle = new Font("Letter Gothic", 12);
					fontTitle = new Font("Arial", 12);
					fontPrint = new Font("Arial", 10);
					fontTitleMiddle = new Font("Arial", 10);
					dotPen = new Pen(Color.Black, (float)1);
					dotPen.DashStyle = DashStyle.Dot;

					thinPen = new Pen(Color.Black, (float)0.02);
					drawBrush = new SolidBrush(Color.Black);
					printFont = new Font("Arial", 10);
					PrintDocument pd = new PrintDocument();
					// Свойтсва принтера
					pd.DefaultPageSettings.Landscape = false;
					pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPageBuhg);
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
		private void pd_PrintPageBuhg(object sender, PrintPageEventArgs ev) 
		{
			float yPos		= 0;

			CardParameters(ev.Graphics);

			ev.Graphics.PageUnit = GraphicsUnit.Millimeter;
			
			if(CountSummWorkBuhg(false)>0)
			{
				if(!nextPage) yPos	= PrintWorksBuhg(ev.Graphics, this.pageOffsetX, yPos);
				if(!nextPage) yPos	+= rowIntervalSection;
			}
			if(CountSummDetailBuhg(false, true)>0)
			{
				if(!nextPage) yPos	= PrintDetailsBuhgOil(ev.Graphics, this.pageOffsetX, yPos);
				if(!nextPage) yPos	+= rowIntervalSection;
			}
			if(CountSummDetailBuhg(false, false)>0)
			{
				if(!nextPage) yPos	= PrintDetailsBuhg(ev.Graphics, this.pageOffsetX, yPos);
				if(!nextPage) yPos	+= rowIntervalSection;
			}	
			if(CountSummWorkBuhg(true)>0)
			{
				if(!nextPage) yPos	= PrintWorksBuhgGuaranty(ev.Graphics, this.pageOffsetX, yPos);
				if(!nextPage) yPos	+= rowIntervalSection;
			}
			if(CountSummDetailBuhg(true, false)>0)
			{
				if(!nextPage) yPos	= PrintDetailsBuhgGuaranty(ev.Graphics, this.pageOffsetX, yPos);
				if(!nextPage) yPos	+= rowIntervalSection;
			}

			if(!nextPage)
			{
				// Добиваем набор пустых строчек
				if(this.card.IsWarrantClosed == false)
					yPos = PrintDetailsBlank(ev.Graphics, this.pageOffsetX, yPos);

			}

			if(nextPage)
			{
				page++;
				nextPage = false;
				ev.HasMorePages = true;
			}
			else
			{
				ev.HasMorePages = false;
				// Для удачной печати!
				page = 1;
				nextPage = false;
				workNum = 0;
				worksDone = false;
				page = 1;
				workNumGuaranty = 0;
				worksDoneGuaranty = false;
				detailNum = 0;
				detailsDone = false;
				detailNumGuaranty = 0;
				detailsDoneGuaranty = false;
				detailNumOil = 0;
				detailsDoneOil = false;
				testPrint = false;
				summDone = false;
			}
		}
		private float PrintWorksBuhg(Graphics graph, float xPos, float yPos)
		{
			float y		= yPos + rowIntervalSection;
			int num		= 1;
			bool testPrintOld;
			float testY;

			if(worksDone) return yPos;
			if(nextPage) return yPos;
			if(cardWorks.Count == 0) return yPos;

			// Печать гарантии
			y = PrintCardWorkTitleBuhg(graph, xPos, y, false);

			float summ = 0.0F;
			foreach(object o in cardWorks)
			{
				DbCardWork wrk = (DbCardWork)o;

				if(wrk.Guaranty == false)
				{
					if(num >= workNum)
					{
						// Тестирование
						testPrintOld = testPrint;
						testPrint = true;
						testY = PrintCardWorkBuhg(graph, wrk, xPos, y, num);
						if(num == cardDetails.Count)testY = PrintSummWork(graph, xPos, testY, 0);
						testPrint = testPrintOld;
						// Конец теститрование

						if(testY > pageHeight)
						{
							workNum = num;
							nextPage = true;
							return yPos;
						}

						y = PrintCardWorkBuhg(graph, wrk, xPos, y, num);
						workNum = num;
					}
					summ += wrk.Summ;
					num++;
				}
			}
			y = PrintSummWork(graph, xPos, y, summ);
			worksDone = true;
			return y + 4;
		}
		private float PrintWorksBuhgGuaranty(Graphics graph, float xPos, float yPos)
		{
			float y		= yPos + rowIntervalSection;
			int num		= 1;
			bool testPrintOld;
			float testY;

			if(worksDoneGuaranty) return yPos;
			if(nextPage) return yPos;
			if(cardWorks.Count == 0) return yPos;

			// Печать гарантии
			y = PrintCardWorkTitleBuhg(graph, xPos, y, true);

			float summ = 0.0F;
			foreach(object o in cardWorks)
			{
				DbCardWork wrk = (DbCardWork)o;

				if(wrk.Guaranty == true)
				{
					if(num >= workNumGuaranty)
					{
						// Тестирование
						testPrintOld = testPrint;
						testPrint = true;
						testY = PrintCardWorkBuhg(graph, wrk, xPos, y, num);
						if(num == cardDetails.Count)testY = PrintSummWork(graph, xPos, testY, 0);
						testPrint = testPrintOld;
						// Конец теститрование

						if(testY > pageHeight)
						{
							workNumGuaranty = num;
							nextPage = true;
							return yPos;
						}

						y = PrintCardWorkBuhg(graph, wrk, xPos, y, num);
						workNumGuaranty = num;
					}
					summ += wrk.SummFull;
					num++;
				}
			}
			y = PrintSummWork(graph, xPos, y, summ);
			worksDoneGuaranty = true;
			return y + 4;
		}
		public float PrintCardWorkTitleBuhg(Graphics graph, float xPos, float yPos, bool guaranty)
		{
			string text;
			StringFormat strFormat	= new StringFormat();
			RectangleF rect			= new RectangleF();
			float x;
			float y;
			
			y			= yPos;
			x			= xPos;
			rect.Y		= yPos;
			rect.X		= x;
			rect.Width	= pageWidth;
			rect.Height	= rowIntervalTitle;
			
			text		= "СПРАВКА К НАРЯДУ № " + card.WarrantNumberTxt + " - РАБОТЫ";
			if(guaranty)
				text += " (Гарантия - " + card.GuarantyTypeTxt + ")";
			else
			{
				if(card.Cashless == true)
					text += " (Без/нал.)";
				else
					text += " (Нал.)";
			}

			if(!testPrint) PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontBoldTitle);
			y			+= rowIntervalTitle;

			// Первая колонка
			x			= xPos;
			rect.X		= x;
			rect.Y		= y;
			rect.Width	= col1;
			rect.Height	= rowHeightWork;
			text		= "№";
			if(!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Center, StringAlignment.Center, fontTitle);
			// Вторая колонка
			x			+= col1;
			rect.X		= x;
			rect.Width	= col2;
			text		= "Наименоване работ";
			if(!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Center, StringAlignment.Center, fontTitle);
			// Третья колонка
			x			+= col2;
			rect.X		= x;
			rect.Width	= col3;
			text		= "Кол-во";
			if(!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Center, StringAlignment.Center, fontTitle);
			// Четвертая колонка
			x			+= col3;
			rect.X		= x;
			rect.Width	= col4;
			text		= "ч.";
			if(!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Center, StringAlignment.Center, fontTitle);
			// Пятая колонка
			x			+= col4;
			rect.X		= x;
			rect.Width	= col5;
			text		= "Цена";
			if(!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Center, StringAlignment.Center, fontTitle);
			// Шестая колонка
			x			+= col5;
			rect.X		= x;
			rect.Width	= col6;
			text		= "Сумма";
			if(!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Center, StringAlignment.Center, fontTitle);
			
			y += rowHeightWork;
			return y;
		}
		public float PrintCardWorkBuhg(Graphics graph, DbCardWork work, float xPos, float yPos, int num)
		{
			string text;
			StringFormat strFormat	= new StringFormat();
			RectangleF rect			= new RectangleF();
			float x;
			float y;

			if(work == null)
			{
				return yPos;
			}		
			
			// Печатаем колонки
			text				= work.Name;
			strFormat.Trimming	= StringTrimming.Word;
			strFormat.Alignment = StringAlignment.Near;
			SizeF size			= graph.MeasureString(text, printFont, (int)col2, strFormat);
			y					= yPos;
			x					= xPos;
			rect.Y				= y;
			rect.X				= x;
			rect.Height			= size.Height;
			// Первая
			rect.X				= x;
			rect.Width			= col1;
			text				= num.ToString();
			strFormat.Alignment = StringAlignment.Center;
			if(!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Center, StringAlignment.Center, fontTitle);
			// Вторая колонка
			x					+= col1;
			rect.X				= x;
			rect.Width			= col2;
			text				= work.Name;
			if(!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Near, StringAlignment.Center, printFont);
			// Третья колонка
			x					+= col2;
			rect.X				= x;
			rect.Width			= col3;
			text				= work.QuontityTxt;
			if(!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Center, StringAlignment.Center, fontTitle);
			
			// Четвертая колонка
			x					+= col3;
			rect.X				= x;
			rect.Width			= col4;
			text				= work.ValTxt;
			if(!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Center, StringAlignment.Center, fontTitle);
			// Пятая колонка
			x					+= col4;
			rect.X				= x;
			rect.Width			= col5;
			text				= work.PriceFullTxt;
			if(!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Center, StringAlignment.Center, fontTitle);
			// Шестая колонка
			x					+= col5;
			rect.X				= x;
			rect.Width			= col6;
			text				= work.SummFullTxt;
			if(!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Center, StringAlignment.Center, fontTitle);
			
			y	+= rect.Height;
			return y;
		}
		private float PrintDetailsBuhg(Graphics graph, float xPos, float yPos)
		{
			float y		= yPos + rowIntervalSection;
			int num		= 1;
			float		testY;
			bool		testPrintOld;

			if(nextPage) return yPos;
			if(cardDetails.Count == 0) return yPos;
			if(detailsDone) return yPos;

			// Тестирование
			testPrintOld = testPrint;
			testPrint = true;
			testY = PrintCardDetailTitleBuhg(graph, xPos, y, false, false);
			testY = PrintCardDetailBuhg(graph, (DbCardDetail)cardDetails[0], xPos, testY, num);
			if (cardDetails.Count == 1) testY = PrintSummDetail(graph, xPos, testY, 0);
			testPrint = testPrintOld;
			// Конец теститрование
			if(testY > pageHeight)
			{
				detailNum = num;
				nextPage = true;
				return yPos;
			}

			y = PrintCardDetailTitleBuhg(graph, xPos, y, false, false);
			float summ = 0.0F;
			foreach(object o in cardDetails)
			{
				DbCardDetail dtl = (DbCardDetail)o;
				if(dtl.Guaranty == false && dtl.Oil == false)
				{
					if(num >=detailNum)
					{
						// Тестирование
						testPrintOld = testPrint;
						testPrint = true;
						testY = PrintCardDetailBuhg(graph, dtl, xPos, y, num);
						if(num == cardDetails.Count)testY = PrintSummDetail(graph, xPos, testY, 0);
						testPrint = testPrintOld;
						// Конец теститрование

						if(testY > pageHeight)
						{
							detailNum = num;
							nextPage = true;
							return yPos;
						}
						y = PrintCardDetailBuhg(graph, dtl, xPos, y, num);
						detailNum = num;
					}
					summ += dtl.Summ;
					num++;
				}
			}
			y = PrintSummDetailBuhg(graph, xPos, y, false, false);
			detailsDone = true;
			return y;
		}
		private float PrintDetailsBuhgGuaranty(Graphics graph, float xPos, float yPos)
		{
			float y		= yPos + rowIntervalSection;
			int num		= 1;
			float		testY;
			bool		testPrintOld;

			if(nextPage) return yPos;
			if(cardDetails.Count == 0) return yPos;
			if(detailsDoneGuaranty) return yPos;

			// Тестирование
			testPrintOld = testPrint;
			testPrint = true;
			testY = PrintCardDetailTitleBuhg(graph, xPos, y, true, false);
			testY = PrintCardDetailBuhg(graph, (DbCardDetail)cardDetails[0], xPos, testY, num);
			if (cardDetails.Count == 1) testY = PrintSummDetail(graph, xPos, testY, 0);
			testPrint = testPrintOld;
			// Конец теститрование
			if(testY > pageHeight)
			{
				detailNumGuaranty = num;
				nextPage = true;
				return yPos;
			}

			y = PrintCardDetailTitleBuhg(graph, xPos, y, true, false);
			float summ = 0.0F;
			foreach(object o in cardDetails)
			{
				DbCardDetail dtl = (DbCardDetail)o;
				if(dtl.Guaranty == true)
				{
					if(num >=detailNumGuaranty)
					{
						// Тестирование
						testPrintOld = testPrint;
						testPrint = true;
						testY = PrintCardDetailBuhg(graph, dtl, xPos, y, num);
						if(num == cardDetails.Count)testY = PrintSummDetail(graph, xPos, testY, 0);
						testPrint = testPrintOld;
						// Конец теститрование

						if(testY > pageHeight)
						{
							detailNumGuaranty = num;
							nextPage = true;
							return yPos;
						}
						y = PrintCardDetailBuhg(graph, dtl, xPos, y, num);
						detailNumGuaranty = num;
					}
					summ += dtl.Summ;
					num++;
				}
			}
			y = PrintSummDetailBuhg(graph, xPos, y, true, false);
			detailsDoneGuaranty = true;
			return y;
		}
		private float PrintDetailsBuhgOil(Graphics graph, float xPos, float yPos)
		{
			float y		= yPos + rowIntervalSection;
			int num		= 1;
			float		testY;
			bool		testPrintOld;

			if(nextPage) return yPos;
			if(cardDetails.Count == 0) return yPos;
			if(detailsDoneOil) return yPos;

			// Тестирование
			testPrintOld = testPrint;
			testPrint = true;
			testY = PrintCardDetailTitleBuhg(graph, xPos, y, false, true);
			testY = PrintCardDetailBuhg(graph, (DbCardDetail)cardDetails[0], xPos, testY, num);
			if (cardDetails.Count == 1) testY = PrintSummDetail(graph, xPos, testY, 0);
			testPrint = testPrintOld;
			// Конец теститрование
			if(testY > pageHeight)
			{
				detailNumOil = num;
				nextPage = true;
				return yPos;
			}

			y = PrintCardDetailTitleBuhg(graph, xPos, y, false, true);
			float summ = 0.0F;
			foreach(object o in cardDetails)
			{
				DbCardDetail dtl = (DbCardDetail)o;
				if(dtl.Oil == true && dtl.Guaranty == false)
				{
					if(num >=detailNumOil)
					{
						// Тестирование
						testPrintOld = testPrint;
						testPrint = true;
						testY = PrintCardDetailBuhg(graph, dtl, xPos, y, num);
						if(num == cardDetails.Count)testY = PrintSummDetail(graph, xPos, testY, 0);
						testPrint = testPrintOld;
						// Конец теститрование

						if(testY > pageHeight)
						{
							detailNumOil = num;
							nextPage = true;
							return yPos;
						}
						y = PrintCardDetailBuhg(graph, dtl, xPos, y, num);
						detailNumOil = num;
					}
					summ += dtl.Summ;
					num++;
				}
			}
			y = PrintSummDetailBuhg(graph, xPos, y, false, true);
			detailsDoneOil = true;
			return y;
		}
		public float PrintCardDetailTitleBuhg(Graphics graph, float xPos, float yPos, bool guaranty, bool oil)
		{
			string text;
			StringFormat strFormat	= new StringFormat();
			RectangleF rect			= new RectangleF();
			float x;
			float y;

			y			= yPos;
			x			= xPos;
			rect.Y		= yPos;
			rect.X		= x;
			rect.Width	= pageWidth;
			rect.Height	= rowIntervalTitle;

			text		= "Требование №______ к з/н №" + card.WarrantNumberTxt;
			if(guaranty == true)
			{
				text += " (Гарантия - " + card.GuarantyTypeTxt + ")";
			}
			else
			{
				if(oil)
				{
					if(card.Cashless) 
						text += " Масла(Б/Н) " + card.Partner.NameShort;
					else
						text += " Масла(Нал.)";
				}
				else
				{
					if(card.Cashless) 
						text += " (Б/Н) " + card.Partner.NameShort;
					else
						text += " (Нал.)";
				}
			}
			
			if(!testPrint) PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontBoldTitle);
			y			+= rowIntervalTitle;

			rect.Y		= y;
			text = "На  " + card.Auto.ModelTxt + " " + card.Auto.SignNo + " VIN : " + card.Auto.Vin;
			if(!testPrint) PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, printFont/*, fontTitle*/);
			y			+= rowIntervalTitle;

			// Первая колонка
			x			= xPos;
			rect.X		= x;
			rect.Y		= y;
			rect.Width	= col1_d;
			rect.Height	= rowHeightWork;
			text		= "№";
            if (!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Center, StringAlignment.Center, printFont/*, fontTitle*/);
			// Вторая колонка
			x			+= col1_d;
			rect.X		= x;
			rect.Width	= col2_d;
			text		= "Код";
            if (!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Center, StringAlignment.Center, printFont/*, fontTitle*/);
			// Третья колонка
			x			+= col2_d;
			rect.X		= x;
			rect.Width	= col3_d - col5_d - col6_d;
			text		= "Наименование";
            if (!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Center, StringAlignment.Center, printFont/*, fontTitle*/);
			// Четвертая колонка
			x			+= col3_d - col5_d - col6_d;
			rect.X		= x;
			rect.Width	= col4_d;
			text		= "К-во";
            if (!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Center, StringAlignment.Center, printFont/*, fontTitle*/);
			// Пятая колонка
			x			+= col4_d;
			rect.X		= x;
			rect.Width	= col5_d;
			text		= "Цена";
            if (!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Center, StringAlignment.Center, printFont/*, fontTitle*/);
			// Шестая колонка
			x			+= col5_d;
			rect.X		= x;
			rect.Width	= col6_d;
			text		= "Сумма";
            if (!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Center, StringAlignment.Center, printFont/*, fontTitle*/);
			// Седьмая колонка
			x			+= col6_d;
			rect.X		= x;
			rect.Width	= col5_d;
			text		= "Цена Вх.";
            if (!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Center, StringAlignment.Center, printFont/*, fontTitle*/);
			// Восьмая колонка
			x			+= col5_d;
			rect.X		= x;
			rect.Width	= col6_d;
			text		= "Сумма Вх.";
            if (!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Center, StringAlignment.Center, printFont/*, fontTitle*/);

			y += rowHeightWork;
			return y;
		}
		public float PrintCardDetailBuhg(Graphics graph, DbCardDetail detail, float xPos, float yPos, int num)
		{
			string text;
			StringFormat strFormat	= new StringFormat();
			RectangleF rect			= new RectangleF();
			float x;
			float y;

			if(detail == null)
			{
				return yPos;
			}
			if (detail.Outer) return yPos;

			// Печатаем колонки
			text				= detail.DetailNameTxt;
			// Отбивка пустого
			if(text.Length == 0) text = "???";
			// Конец отбивка пустой детали
			strFormat.Trimming	= StringTrimming.Word;
			strFormat.Alignment = StringAlignment.Near;
			SizeF size			= graph.MeasureString(text, printFont, (int)col3_d - col5_d - col6_d, strFormat);
			y					= yPos;
			x					= xPos;
			rect.Y				= y;
			rect.X				= x;
			rect.Height			= size.Height;

			// Первая
			rect.X				= x;
			rect.Width			= col1_d;
			text				= num.ToString();
			strFormat.Alignment = StringAlignment.Center;
            if (!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Center, StringAlignment.Center, printFont/*, fontTitle*/);
			// Вторая колонка
			x					+= col1_d;
			rect.X				= x;
			rect.Width			= col2_d;
			text				= detail.CodeDetailTxt;
			if(!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Center, StringAlignment.Center, printFont);
			// Третья колонка
			x					+= col2_d;
			rect.X				= x;
			rect.Width			= col3_d - col5_d - col6_d;
			text				= detail.DetailNameTxt;
			if(!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Near, StringAlignment.Center, printFont);
			// Четвертая колонка
			x					+= col3_d - col5_d - col6_d;
			rect.X				= x;
			rect.Width			= col4_d;
			text				= detail.QuontityTxt;
            if (!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Center, StringAlignment.Center, printFont/*, fontTitle*/);
			// Пятая колонка
			x					+= col4_d;
			rect.X				= x;
			rect.Width			= col5_d;
			if(detail.Guaranty)
				text				= detail.PriceTxt;
			else
			text				= detail.PriceTxt;
            if (!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Center, StringAlignment.Center, printFont/*, fontTitle*/);
			// Шестая колонка
			x					+= col5_d;
			rect.X				= x;
			rect.Width			= col6_d;
			text				= detail.SummTxt;
            if (!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Center, StringAlignment.Center, printFont/*, fontTitle*/);
			
			// Седьмая
			x					+= col6_d;
			rect.X				= x;
			rect.Width			= col5_d;
			text				= detail.InputTxt;
            if (!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Center, StringAlignment.Center, printFont/*, fontTitle*/);
			// Восьмая
			x					+= col5_d;
			rect.X				= x;
			rect.Width			= col6_d;
			text				= detail.InputSummTxt;
            if (!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Center, StringAlignment.Center, printFont/*, fontTitle*/);
			

			y	+= rect.Height;
			return y;
		}
		public float PrintSummDetailBuhg(Graphics graph, float xPos, float yPos, bool guaranty, bool oil)
		{
			string text;
			RectangleF rect = new RectangleF();
			// Первая колонка
			rect.Y		= yPos;
			rect.X		= xPos;
			rect.Height	= rowHeightWork;
			rect.Width	= col1_d + col2_d + col3_d + col4_d + col5_d - col5_d - col6_d;

			float summ = 0.0F;
            float summ_input = 0.0F;
			foreach(object o in cardDetails)
			{
				DbCardDetail dtl = (DbCardDetail)o;
				if(guaranty)
				{
					if(dtl.Guaranty == guaranty) summ += dtl.SummWhole;
				}
				else
				{
                    if (dtl.Oil == oil && dtl.Guaranty == false)
                    {
                        summ += dtl.SummWhole;
                        summ_input += dtl.InputSumm;
                    }
                    
				}
			}

			text		= "ИТОГО:";
			if(!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Far, StringAlignment.Center, fontBoldTitle);
			// Вторая колонка
			rect.X		+= col1_d + col2_d + col3_d + col4_d + col5_d - col5_d - col6_d;
			rect.Width	= col6_d;
			text		= Db.CachToTxt(summ);
			if(!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Center, StringAlignment.Center, fontTitle);

			rect.X		+= col6_d;
			rect.Width	= col5_d;
			text		= "---";
			if(!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Center, StringAlignment.Center, fontTitle);
			rect.X		+= col5_d;
			rect.Width	= col6_d;
            text = ""; text = Db.CachToTxt(summ_input);
			if(!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Center, StringAlignment.Center, fontTitle);

			rect.X = pageOffsetX;
			rect.Y = rect.Y + rect.Height + 3;
			rect.Width	= pageWidth;
			text = "Разрешил________________Отпустил_________________Получил__________________";
			if(!testPrint) PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontTitle);

			return rect.Y + rect.Height + 3;
		}

		public int CountSummDetailBuhg(bool guaranty, bool oil)
		{
			int count = 0;
			foreach(object o in cardDetails)
			{
				DbCardDetail dtl = (DbCardDetail)o;
				if(guaranty)
				{
					if(dtl.Guaranty == guaranty) count++;
				}
				else
				{
					if(dtl.Oil == oil && dtl.Guaranty == false) count++;
				}
			}
			return count;
		}

		public int CountSummWorkBuhg(bool guaranty)
		{
			int count = 0;
			foreach(object o in cardWorks)
			{
				DbCardWork wrk = (DbCardWork)o;
				if(wrk.Guaranty == guaranty) count++;
			}
			return count;
		}

		public string GetWorkCollection(DbCardWork work)
		{
			string text = "";
			long code = work.CodeWork;
			// Ищем работу в списке работ с наборами
			int count = 0;
			foreach(long l in cardWorkCodes)
			{
				if(l == code)
				{
					// Добавляем по списку все элементы набора
					foreach(object o in (ArrayList)cardWorkCollections[count])
					{
						if (text.Length != 0) text += "\n";
						DtWorkCollectionItem elm = (DtWorkCollectionItem)o;
						string name = (string)elm.GetData("НАИМЕНОВАНИЕ_КОЛЛЕКЦИЯ_ЭЛЕМЕНТ");
						text += "- " + name;
					}
				}
				count++;
			}
			return text;
		}

		public string GetWorkCollectionOil(ArrayList works)
		{
			string text = "";
			foreach(DbCardWork wrk in works)
			{
				if(wrk.Oil == true)
				{
					if(text.Length != 0) text += "\n";
					text += GetWorkCollection(wrk);
				}
			}
			return text;
		}
		

		private float PrintDetailsBlank(Graphics graph, float xPos, float yPos)
		{
			float y		= yPos + rowIntervalSection;
			
			while(y < pageHeight - 10)
			{
				y = PrintCardDetailBlank(graph, xPos, y);
			}
			return y;
		}

		public float PrintCardDetailBlank(Graphics graph, float xPos, float yPos)
		{	
			RectangleF rect			= new RectangleF();
			float x;
			float y;
			float height = 10;
			string text = "";

			// Печатаем колонки
			
			y					= yPos;
			x					= xPos;
			rect.Y				= y;
			rect.X				= x;
			rect.Height			= height;

			int col1_v = 11;
			int col2_v = 25;
			int col3_v = 95;
			int col4_v = 9;
			int col5_v = 20;
			int col6_v = 25;

			// Первая
			rect.X				= x;
			rect.Width			= col1_v;
			if(!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Center, StringAlignment.Center, fontTitle);
			// Вторая колонка
			x					+= col1_v;
			rect.X				= x;
			rect.Width			= col2_v;
			if(!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Center, StringAlignment.Center, printFont);
			// Третья колонка
			x					+= col2_v;
			rect.X				= x;
			rect.Width			= col3_v;;
			if(!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Near, StringAlignment.Center, printFont);
			// Четвертая колонка
			x					+= col3_v;
			rect.X				= x;
			rect.Width			= col4_v;
			if(!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Center, StringAlignment.Center, fontTitle);
			// Пятая колонка
//			x					+= col4_d;
//			rect.X				= x;
//			rect.Width			= col5_d;
//			if(!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Center, StringAlignment.Center, fontTitle);
			// Шестая колонка
//			x					+= col5_d;
//			rect.X				= x;
//			rect.Width			= col6_d;
//			if(!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Center, StringAlignment.Center, fontTitle);
			
			// Седьмая
			x					+= col4_v;
			rect.X				= x;
			rect.Width			= col5_v;
			if(!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Center, StringAlignment.Center, fontTitle);
			// Восьмая
			x					+= col5_v;
			rect.X				= x;
			rect.Width			= col6_v;
			if(!testPrint) PrintBoxFixed(graph, text, rect, true, StringAlignment.Center, StringAlignment.Center, fontTitle);
			

			y	+= rect.Height;
			return y;
		}

		#region  Печать заголовка
		public void PrintRequest() 
		{
			try 
			{
				try 
				{
					// Подготовка всяких фонтов
					fontBoldTitle = new Font("Arial", 12, FontStyle.Bold);
					fontDigitTitle = new Font("Letter Gothic", 12);
					fontTitle = new Font("Arial", 12);
					fontPrint = new Font("Arial", 10);
					fontTitleMiddle = new Font("Arial", 10);
					dotPen = new Pen(Color.Black, (float)1);
					dotPen.DashStyle = DashStyle.Dot;

					thinPen = new Pen(Color.Black, (float)0.02);
					drawBrush = new SolidBrush(Color.Black);
					printFont = new Font("Arial", 10);
					PrintDocument pd = new PrintDocument();
					// Свойтсва принтера
					pd.DefaultPageSettings.Landscape = false;
					pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPageRequest);
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
		private void pd_PrintPageRequest(object sender, PrintPageEventArgs ev) 
		{
			CardParameters(ev.Graphics);
			ev.Graphics.PageUnit = GraphicsUnit.Millimeter;
			
			// Вверху печатаем заголовок
			PrintTitleDocument(ev.Graphics, this.pageOffsetX, this.pageOffsetY - 5);

			// Только одна страница печати
			ev.HasMorePages = false;
		}
		#endregion

		#region Печать документа на Въезд
		// The Click event is raised when the user clicks the Print button.
		public void PrintDocument() 
		{
			try 
			{
				try 
				{
					// Подготовка всяких фонтов
					fontBoldTitle = new Font("Arial", 12, FontStyle.Bold);
					fontDigitTitle = new Font("Letter Gothic", 12);
					fontTitle = new Font("Arial", 12);
					fontPrint = new Font("Arial", 10);
					fontTitleMiddle = new Font("Arial", 10);
					dotPen = new Pen(Color.Black, (float)1);
					dotPen.DashStyle = DashStyle.Dot;

					thinPen = new Pen(Color.Black, (float)0.02);
					drawBrush = new SolidBrush(Color.Black);
					printFont = new Font("Arial", 10);
					PrintDocument pd = new PrintDocument();
					// Свойтсва принтера
					pd.DefaultPageSettings.Landscape = false;
					pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPageDocument);
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
		private void pd_PrintPageDocument(object sender, PrintPageEventArgs ev) 
		{
			CardParameters(ev.Graphics);
			ev.Graphics.PageUnit = GraphicsUnit.Millimeter;
			
			// Вверху печатаем заголовок
			PrintTitleDocument(ev.Graphics, this.pageOffsetX, this.pageOffsetY - 5);
			// Внизу печатаем завершение
			//PrintTitleEndDocument(ev.Graphics, this.pageOffsetX, 180);
			PrintTitleEndDocument(ev.Graphics, this.pageOffsetX, 150);

			// Только одна страница печати
			ev.HasMorePages = false;
		}
		
		public float PrintTitleEndDocument(Graphics graph, float xPos, float yPos)
		{
			RectangleF	rect = new RectangleF();
			string		text;
			float		y;

			rect.X		= xPos;
			rect.Y		= yPos;
			rect.Height = rowIntervalTitle;
			rect.Width	= pageWidth;
			y			= yPos;

			// Мойка на приемном акте
			graph.DrawLine(thinPen, rect.X, rect.Y + rect.Height, rect.X + rect.Width, rect.Y + rect.Height);
			rect.Y	= rect.Y + rect.Height;
			text	= "МОЙКА";
			PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontBoldTitle);
			rect.Y		= rect.Y + rect.Height;
			// Виды мойки строчка 1
			// Позиция 1
			graph.DrawRectangle(thinPen, rect.X, rect.Y, 5, 5);
			rect.X		= rect.X + 7;
			text	= "Технологическая";
			PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontPrint);
			rect.X		= rect.X + 33;
			// Позиция 2
			graph.DrawRectangle(thinPen, rect.X, rect.Y, 5, 5);
			rect.X		= rect.X + 7;
			text	= "Верх(Шампунь)";
			PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontPrint);
			rect.X		= rect.X + 33;
			// Позиция 3
			graph.DrawRectangle(thinPen, rect.X, rect.Y, 5, 5);
			rect.X		= rect.X + 7;
			text	= "Низ";
			PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontPrint);
			rect.X		= rect.X + 23;
			// Позиция 4
			graph.DrawRectangle(thinPen, rect.X, rect.Y, 5, 5);
			rect.X		= rect.X + 7;
			text	= "ДВС";
			PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontPrint);
			rect.X		= rect.X + 23;
			// Позиция 5
			graph.DrawRectangle(thinPen, rect.X, rect.Y, 5, 5);
			rect.X		= rect.X + 7;
			text	= "ДВС(Химия)";
			PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontPrint);
			rect.X		= rect.X + 33;
			// Мойка строчка 2
			rect.X		= xPos;
			rect.Y		= rect.Y + rect.Height;
			// Позиция 1
			graph.DrawRectangle(thinPen, rect.X, rect.Y, 5, 5);
			rect.X		= rect.X + 7;
			text	= "Коврики";
			PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontPrint);
			rect.X		= rect.X + 33;
			// Позиция 2
			graph.DrawRectangle(thinPen, rect.X, rect.Y, 5, 5);
			rect.X		= rect.X + 7;
			text	= "Салон(Пылесос)";
			PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontPrint);
			rect.X		= rect.X + 33;
			// Позиция 3
			graph.DrawRectangle(thinPen, rect.X, rect.Y, 5, 5);
			rect.X		= rect.X + 7;
			text	= "Протирка";
			PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontPrint);
			rect.X		= rect.X + 23;	
			// Позиция 4
			graph.DrawRectangle(thinPen, rect.X, rect.Y, 5, 5);
			rect.X		= rect.X + 7;
			text	= "Мойка ТО";
			PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontPrint);
			rect.X		= rect.X + 23;	
			// Позиция 5
			graph.DrawRectangle(thinPen, rect.X, rect.Y, 5, 5);
			rect.X		= rect.X + 7;
			text	= "Мойка(Бесконтактная)";
			PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontPrint);
			rect.X		= rect.X + 33;	
			// Подпись мойщика
			rect.X		= xPos + 90;
			rect.Y		= rect.Y + rect.Height;
			text	= "Мойщик";
			PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontPrint);
			graph.DrawLine(thinPen, rect.X, rect.Y + rect.Height, rect.X + rect.Width - 90, rect.Y + rect.Height);
			rect.Y		= rect.Y + rect.Height;
			text	= "(Подпись)";
			rect.X		= 120;
			PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontPrint);
			text	= "(ФИО)";
			rect.X		= 160;
			PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontPrint);

			// Нулевая линия отреза
			rect.X		= xPos;
			graph.DrawLine(dotPen, rect.X, rect.Y + rect.Height, rect.X + rect.Width, rect.Y + rect.Height);
			rect.Y	= rect.Y + rect.Height;
			text	= "ДОВЕРЕННОСТЬ";
			PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontBoldTitle);
			rect.Y	= rect.Y + rect.Height;
			rect.Height = rowIntervalTitle * 3;
			text	= "Я, " + card.PartnerNameTxt + ", доверяю ООО \"АВТО-1\" в лице Кинозерова И.Н. проводить все необходимые действия по оформлению гарантийного заказ-наряда и рекламационного акта на зарекламированные по гарантии изделя на моем автомобиле. Доверенность действительна на время нахождения автомобиля в ремонте";
			PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontPrint);
			rect.Y		= rect.Y + rect.Height;
			rect.Height = rowIntervalTitle;
			// Подпись клиента
			rect.X		= xPos + 10;
			text	= "Документ";
			PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontPrint);
			rect.X		= xPos + 90;
			text	= "Заказчик";
			PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontPrint);
			graph.DrawLine(thinPen, xPos + 10, rect.Y + rect.Height, xPos + 60, rect.Y + rect.Height);
			graph.DrawLine(thinPen, rect.X, rect.Y + rect.Height, rect.X + rect.Width - 90, rect.Y + rect.Height);
			rect.Y		= rect.Y + rect.Height;
			text	= "(Подпись)";
			rect.X		= 120;
			PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontPrint);
			text	= "(ФИО)";
			rect.X		= 160;
			PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontPrint);

			// Первая линия отреза
			rect.X		= xPos;
			graph.DrawLine(dotPen, rect.X, rect.Y + rect.Height, rect.X + rect.Width, rect.Y + rect.Height);
			rect.Y	= rect.Y + rect.Height;
			text	= "МОЙКА по заявке № " + card.NumberTxt;
			PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontBoldTitle);
			text	= DateTime.Now.ToString();
			PrintBoxFixed(graph, text, rect, false, StringAlignment.Far, StringAlignment.Center, fontBoldTitle);
			rect.Y	= rect.Y + rect.Height;
			text	= "Автомобиль " + card.Auto.ModelTxt + ": VIN " + card.Auto.Vin + "  Рег. знак : " + card.Auto.SignNo;
			PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontBoldTitle);
			rect.Y		= rect.Y + rect.Height;
			// Виды мойки строчка 1
			// Позиция 1
			graph.DrawRectangle(thinPen, rect.X, rect.Y, 5, 5);
			rect.X		= rect.X + 7;
			text	= "Технологическая";
			PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontPrint);
			rect.X		= rect.X + 33;
			// Позиция 2
			graph.DrawRectangle(thinPen, rect.X, rect.Y, 5, 5);
			rect.X		= rect.X + 7;
			text	= "Верх(Шампунь)";
			PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontPrint);
			rect.X		= rect.X + 33;
			// Позиция 3
			graph.DrawRectangle(thinPen, rect.X, rect.Y, 5, 5);
			rect.X		= rect.X + 7;
			text	= "Низ";
			PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontPrint);
			rect.X		= rect.X + 23;
			// Позиция 4
			graph.DrawRectangle(thinPen, rect.X, rect.Y, 5, 5);
			rect.X		= rect.X + 7;
			text	= "ДВС";
			PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontPrint);
			rect.X		= rect.X + 23;
			// Позиция 5
			graph.DrawRectangle(thinPen, rect.X, rect.Y, 5, 5);
			rect.X		= rect.X + 7;
			text	= "ДВС(Химия)";
			PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontPrint);
			rect.X		= rect.X + 33;
			// Мойка строчка 2
			rect.X		= xPos;
			rect.Y		= rect.Y + rect.Height;
			// Позиция 1
			graph.DrawRectangle(thinPen, rect.X, rect.Y, 5, 5);
			rect.X		= rect.X + 7;
			text	= "Коврики";
			PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontPrint);
			rect.X		= rect.X + 33;
			// Позиция 2
			graph.DrawRectangle(thinPen, rect.X, rect.Y, 5, 5);
			rect.X		= rect.X + 7;
			text	= "Салон(Пылесос)";
			PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontPrint);
			rect.X		= rect.X + 33;
			// Позиция 3
			graph.DrawRectangle(thinPen, rect.X, rect.Y, 5, 5);
			rect.X		= rect.X + 7;
			text	= "Протирка";
			PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontPrint);
			rect.X		= rect.X + 23;
			// Позиция 4
			graph.DrawRectangle(thinPen, rect.X, rect.Y, 5, 5);
			rect.X		= rect.X + 7;
			text	= "Мойка ТО";
			PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontPrint);
			rect.X		= rect.X + 23;	
			// Позиция 5
			graph.DrawRectangle(thinPen, rect.X, rect.Y, 5, 5);
			rect.X		= rect.X + 7;
			text	= "Мойка(Бесконтактная)";
			PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontPrint);
			rect.X		= rect.X + 23;	
			// Подпись мастера
			rect.X		= xPos + 90;
			rect.Y		= rect.Y + rect.Height;
			text	= "Мастер";
			PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontPrint);
			graph.DrawLine(thinPen, rect.X, rect.Y + rect.Height, rect.X + rect.Width - 90, rect.Y + rect.Height);
			rect.Y		= rect.Y + rect.Height;
			text	= "(Подпись)";
			rect.X		= 120;
			PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontPrint);
			text	= "(ФИО)";
			rect.X		= 160;
			PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontPrint);

			// Вторая линия отреза
			rect.X		= xPos;
			graph.DrawLine(dotPen, rect.X, rect.Y + rect.Height, rect.X + rect.Width, rect.Y + rect.Height);
			rect.Y	= rect.Y + rect.Height;
			// Дополнительный анализ, расширенная информация
			string destination = "";
			if(card.CodeWorkshop != 0)
			{
				DtWorkshop workshop = DbSqlWorkshop.Find(card.CodeWorkshop);
				if(workshop != null)
				{
					destination = (string)workshop.GetData("ПРОПУСК_НАЗНАЧЕНИЕ");
				}
			}
			text	= "Пропуск на въезд по заявке №" + card.NumberTxt;
			if(destination != "") text += " (Для " + destination + ")";
			PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontBoldTitle);
			text	= DateTime.Now.ToString();
			PrintBoxFixed(graph, text, rect, false, StringAlignment.Far, StringAlignment.Center, fontBoldTitle);
			rect.Y	= rect.Y + rect.Height;
			text	= " " + card.Auto.ModelTxt + ": VIN " + card.Auto.Vin + "  Рег. знак : " + card.Auto.SignNo;
			PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontBoldTitle);
			// Назначение въезда
			// Позиция 1
			rect.Y	= rect.Y + rect.Height;
			graph.DrawRectangle(thinPen, rect.X, rect.Y, 5, 5);
			rect.X		= rect.X + 7;
			text	= "Сервис";
			PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontPrint);
			rect.X		= rect.X + 33;
			// Позиция 2
			graph.DrawRectangle(thinPen, rect.X, rect.Y, 5, 5);
			rect.X		= rect.X + 7;
			text	= "Мойка";
			PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontPrint);
			rect.X		= rect.X + 33;
			// Позиция 3
			graph.DrawRectangle(thinPen, rect.X, rect.Y, 5, 5);
			rect.X		= rect.X + 7;
			text	= "Тюнинг";
			PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontPrint);
			rect.X		= rect.X + 33;
			// Позиция 4
			graph.DrawRectangle(thinPen, rect.X, rect.Y, 5, 5);
			rect.X		= rect.X + 7;
			text	= "Другое";
			PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontPrint);
			rect.X		= rect.X + 33;
			// Подпись мастера
			rect.X		= xPos + 60;
			rect.Y		= rect.Y + rect.Height;
			text	= "Въезд разрешил мастер";
			PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontPrint);
			graph.DrawLine(thinPen, rect.X, rect.Y + rect.Height, rect.X + rect.Width - 60, rect.Y + rect.Height);
			rect.Y		= rect.Y + rect.Height;
			text	= "(Подпись)";
			rect.X		= 120;
			PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontPrint);
			text	= "(ФИО)";
			rect.X		= 160;
			PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontPrint);

			/*
			rect.Y		= rect.Y + rect.Height;
			text	= "Наряд на мойку";
			PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontPrint);
			*/

			/*
			//text	+= "Качество ремонта проверил, автомобиль технически исправен";
			text	= "Качество ремонта проверил, автомобиль технически исправен";
			PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontPrint);
			graph.DrawLine(thinPen, rect.X, rect.Y + rect.Height, rect.X + rect.Width, rect.Y + rect.Height);
			rect.Y		= rect.Y + rect.Height;
			text	= "(Подпись)";
			rect.X		= 120;
			if(!testPrint) PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontPrint);
			text	= "(ФИО)";
			rect.X		= 160;
			if(!testPrint) PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontPrint);

			rect.X = xPos;
			rect.Y		= rect.Y + rect.Height;
			text	= "Автомобиль из ремонта принял, претензий не имею";
			if(!testPrint) PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontPrint);
			if(!testPrint) graph.DrawLine(thinPen, rect.X, rect.Y + rect.Height, rect.X + rect.Width, rect.Y + rect.Height);
			rect.Y		= rect.Y + rect.Height;
			text	= "(Подпись клиента)";
			rect.X		= 120;
			if(!testPrint) PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontPrint);
			text	= "(Дата)";
			rect.X		= 160;
			if(!testPrint) PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontPrint);

			rect.Y = rect.Y + rect.Height;
			rect.X = xPos;
			rect.Height = rowIntervalTitle * 2;
			text	=  "Гарантия на слесарные, электрические, контрольно-осмотровые(диагностические), регулировочные работы - 30 дней или 1000 км пробега\n";
			if(!testPrint) PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontPrint);

			rect.Height = rowIntervalTitle;
			rect.Y = rect.Y + rect.Height;
			rect.X = xPos;

			graph.DrawLine(dotPen, rect.X, rect.Y + rect.Height, rect.X + rect.Width, rect.Y + rect.Height);
			rect.X	= xPos;
			rect.Y	= rect.Y + rect.Height;
			text	= "ЗАКАЗ-НАРЯД " + card.WarrantNumberTxt;
			if(!testPrint) PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontBoldTitle);
			rect.Y	= rect.Y + rect.Height;
			text	= "Автомобиль : VIN " + card.Auto.Vin + "            Рег. знак : " + card.Auto.SignNo;
			if(!testPrint) PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontBoldTitle);
			*/
			/*
			float summ = 0.0F;
			foreach(object o in cardDetails)
			{
				DbCardDetail dtl = (DbCardDetail)o;
				summ += dtl.Summ;
			}
			foreach(object o in cardWorks)
			{
				DbCardWork wrk = (DbCardWork)o;
				summ += wrk.Summ;
			}
			rect.Y	= rect.Y + rect.Height;
			text	= "Оплата произведена " + Db.CachToTxt(summ) + " руб";
			*/
			/*
			rect.Y	= rect.Y + rect.Height;
			text	= "Оплата произведена полностью";
			if(!testPrint) PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontTitle);
			rect.Y		= rect.Y + rect.Height;
			if(!testPrint) graph.DrawLine(thinPen, rect.X, rect.Y + rect.Height, rect.X + rect.Width, rect.Y + rect.Height);
			rect.Y		= rect.Y + rect.Height;
			text	= "(Дата)";
			rect.X		= 40;
			if(!testPrint) PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontPrint);
			text	= "(Подпись)";
			rect.X		= 160;
			if(!testPrint) PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontPrint);
			*/
			return rect.Y + rect.Height;
		}
		// Печать шапки
		public float PrintTitleDocument(Graphics graph, float xPos, float yPos)
		{
			string text;
			float x;
			float y;
			RectangleF rect = new RectangleF(0, 0, 0, 0);

			y = yPos;
			rect.X = xPos;
			rect.Y = yPos;
			rect.Height = 10;
			rect.Width = 60;

			y = PrintTitleCardDocument(graph, this.pageOffsetX, y);
			y += rowIntervalSection;
			PrintTitleClient(graph, this.pageOffsetX + autoRectX + 5, y);
			y = PrintTitleAuto(graph, this.pageOffsetX, y);

			// Допечатываем картинку
			rect.X = xPos;
			rect.Y = y + 3;
			rect.Width = 190;
			rect.Height = 100;
			Image carImage = Image.FromFile("act_face_01.jpg");
			if(!testPrint) graph.DrawImage(carImage, rect);

			return y;
		}
		
		// Шапка заказ наряда - Карточка
		public float PrintTitleCardDocument(Graphics graph, float xPos, float yPos)
		{
			RectangleF	rect = new RectangleF();
			string		text;
			float		y;

			rect.X		= xPos + innerOffsetTitleX;
			rect.Y		= yPos;
			rect.Height = rowIntervalTitle;
			rect.Width	= pageWidth;
			y			= yPos;

			//  НОМЕР И ДАТА ЗАЯВКИ + ТЕКУЩИЕ ДАТА И ВРЕМЯ
			text	= "ЗАЯВКА № " + card.NumberTxt;
			PrintBoxFixed(graph, text, rect, false, StringAlignment.Near, StringAlignment.Center, fontBoldTitle);

			rect.X = xPos;
			rect.Width = pageWidth;
			text = DateTime.Now.ToString();
			PrintBoxFixed(graph, text, rect, false, StringAlignment.Far, StringAlignment.Center, fontTitle);

			y = rect.Y + rowIntervalTitle;
			rect.Y = yPos;
			rect.X = xPos;
			rect.Height	= y - rect.Y;
			rect.Width	= pageWidth;
			if(!testPrint) graph.DrawRectangle(thinPen, rect.X, rect.Y, rect.Width, rect.Height);

			return y;
		}
		#endregion
	}
}
