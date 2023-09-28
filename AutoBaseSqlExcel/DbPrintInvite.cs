using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbPrintInvite.
	/// </summary>
	/// 
	
	public class DbPrintInvite:PrintCommon
	{
		const int PAGE_RIGHT_X = 200;
		const int PAGE_LEFT_X = 10;
		const int WORK_WIDTH_COLUMN1 = 8;
		const int WORK_WIDTH_COLUMN2 = 20;
		const int WORK_WIDTH_COLUMN4 = 10;
		const int WORK_WIDTH_COLUMN5 = 15;
		const int WORK_WIDTH_COLUMN6 = 10;
		const int WORK_WIDTH_COLUMN7 = 18;
		const int WORK_WIDTH_COLUMN8 = 18;
		const int WORK_WIDTH_COLUMN9 = 18;
		const int WORK_WIDTH_COLUMN10 = 18;
		const int WORK_WIDTH_COLUMN11 = 18;
		const int WORK_WIDTH_COLUMN3 = PAGE_RIGHT_X - WORK_WIDTH_COLUMN1 - WORK_WIDTH_COLUMN2 - WORK_WIDTH_COLUMN4 - WORK_WIDTH_COLUMN5 - WORK_WIDTH_COLUMN6 - WORK_WIDTH_COLUMN7 - WORK_WIDTH_COLUMN8 - WORK_WIDTH_COLUMN9 - WORK_WIDTH_COLUMN10 - WORK_WIDTH_COLUMN11 - PAGE_LEFT_X;

		const int DETAIL_WIDTH_COLUMN1 = 8;
		const int DETAIL_WIDTH_COLUMN2 = 20;
		const int DETAIL_WIDTH_COLUMN4 = 10;
		const int DETAIL_WIDTH_COLUMN5 = 15;
		const int DETAIL_WIDTH_COLUMN6 = 18;
		const int DETAIL_WIDTH_COLUMN7 = 18;
		const int DETAIL_WIDTH_COLUMN8 = 18;
		const int DETAIL_WIDTH_COLUMN9 = 18;
		const int DETAIL_WIDTH_COLUMN10 = 18;
		const int DETAIL_WIDTH_COLUMN3 = PAGE_RIGHT_X - DETAIL_WIDTH_COLUMN1 - DETAIL_WIDTH_COLUMN2 - DETAIL_WIDTH_COLUMN4 - DETAIL_WIDTH_COLUMN5 - DETAIL_WIDTH_COLUMN6 - DETAIL_WIDTH_COLUMN7 - DETAIL_WIDTH_COLUMN8 - DETAIL_WIDTH_COLUMN9 - DETAIL_WIDTH_COLUMN10 - PAGE_LEFT_X;

		// Инструменты для печати
		readonly SolidBrush	brush_standart;
		readonly SolidBrush brush_lightgray;
		readonly Font font_small_bold;
		readonly Font font_middle_bold;
		//readonly Font font_middle;
		readonly Font font_small;
		readonly Font font_small_cur;
		readonly Pen pen_thin;

		private readonly DtCard	_card;
		private readonly DtTxtCard _cardTxt;
		private readonly CalculatorCard _calculatorCard;

		//public DbPrintInvite(DtCard srcCard, ArrayList srcWorks, ArrayList srcDetails, CalculatorCard srcCalculatorCard):base()
		public DbPrintInvite(DtCard srcCard, CalculatorCard srcCalculatorCard) : base()
		{
			// Подготовка инструментов для печати
			brush_standart		= new SolidBrush(Color.Black);
			brush_lightgray		= new SolidBrush(Color.LightGray);
			font_small_bold		= new Font("Arial", 8, FontStyle.Bold);
			font_small			= new Font("Arial", 8);
			font_small_cur		= new Font("Arial", 8, FontStyle.Italic);
			font_middle_bold	= new Font("Arial", 10, FontStyle.Bold);
			pen_thin			= new Pen(brush_standart, 0.3F);

			_card = srcCard;
			//_card.LoadCardWorksForce();
			//_card.LoadCardDetailsForce();
			_cardTxt = new DtTxtCard(srcCard);
			_calculatorCard = srcCalculatorCard;
			_calculatorCard.Calculate(_card);
		}

        #region Новая печать
        public override void PrintDocument()
		{
			bool first = true;
			int count = 1;
			if (_card.CardWorks.Count > 0)
			{
				foreach (DtCardWork element in _card.CardWorks)
				{
					PrintBlockHead(new DelegatePrintBlock(PrintWorkBlock), element, new DelegatePrintBlock(PrintWorkBlockHead2), first, count);
					count++;
					first = false;
				}
				PrintBlockHead(new DelegatePrintBlock(PrintWorkBlockTotal), null);
			}
			first = true;
			count = 1;
			if (_card.CardDetails.Count > 0)
			{
				foreach (DtCardDetail element in _card.CardDetails)
				{
					PrintBlockHead(new DelegatePrintBlock(PrintDetailBlock2), element, new DelegatePrintBlock(PrintDetailBlockHead2), first, count);
					count++;
					first = false;
				}
				PrintBlockHead(new DelegatePrintBlock(PrintDetailBlockTotal), null);
			}
			PrintBlockHead(new DelegatePrintBlock(PrintInviteTotal), null); 
		}
		
		public override float PrintStandartHead(float offset)
		{
			// НЕИЗМЕНЯЕМЫЕ ДАННЫЕ
			const int HEADER_TITLE_WIDTH = 40; // Ширина блока заголовка для наименований
			const int HEADER_DATA_WIDTH = 75; // Ширина блока заголовка для данных
			const int TEXT_OFFSET_X = 2; // Отсуп текста внутри оконтовки
			const int HEADER_X = 10; // Оступ заголовка от края страницы
			const int ROUNDCORNER_RADIUS = 4; // Радиус закругления углов табличной части
			const int HEADER_LINE_HEIGHT = 4;     // Высота одной строки в заголовке
			const int LOGO_BLOCK_X = HEADER_TITLE_WIDTH + HEADER_DATA_WIDTH + HEADER_X + 5; // Начало для блока логотипа
			const int PAGE_RIGHT_X = 200; // Граница страницы по ширине
			const int LOGO_IMAGE_WIDTH = 39; // Ширина картинки логотипа
			const int LOGO_IMAGE_HEIGHT = 20; // Высота картинки логотипа
			const int OFFSET_AFTER_HEADER = 3;

			float y = offset;
			// Учстанавливаем используемые для печати имнтрументы

			SelectTextBoxOffsetX(TEXT_OFFSET_X);
			SelectFont(font_middle_bold);
			SelectBrush(brush_standart);
			SelectPen(pen_thin);
			SetStringFormat(StringAlignment.Near, StringAlignment.Center);

			// Печатем данные заготовка
			y += SelectedPrintTextNoMesure("РАССЧЕТ СТОИМОСТИ ОБСЛУЖИВАНИЯ НА " + DateTime.Now.ToShortDateString(), HEADER_X, y, HEADER_TITLE_WIDTH + HEADER_DATA_WIDTH, 5);
			SelectFont(font_small_bold);
			RectangleF rect_header = new RectangleF(HEADER_X, y, HEADER_DATA_WIDTH + HEADER_TITLE_WIDTH, 0);
			y += SelectedPrintTextNoBoxPair("Модель", _cardTxt.AutoModel, HEADER_X, y, HEADER_TITLE_WIDTH, HEADER_DATA_WIDTH, HEADER_LINE_HEIGHT);
			y += SelectedPrintTextBoxPair("Гос. номер:", _cardTxt.AutoLicensePlate, HEADER_X, y, HEADER_TITLE_WIDTH, HEADER_DATA_WIDTH, HEADER_LINE_HEIGHT);
			y += SelectedPrintTextBoxPair("VIN:", _cardTxt.AutoVin, HEADER_X, y, HEADER_TITLE_WIDTH, HEADER_DATA_WIDTH, HEADER_LINE_HEIGHT);
			y += SelectedPrintTextBoxPair("Дата продажи:", _cardTxt.AutoSellDate, HEADER_X, y, HEADER_TITLE_WIDTH, HEADER_DATA_WIDTH, HEADER_LINE_HEIGHT);
			y += SelectedPrintTextBoxPair("Пробег:", _cardTxt.Run, HEADER_X, y, HEADER_TITLE_WIDTH, HEADER_DATA_WIDTH, HEADER_LINE_HEIGHT);
			y += SelectedPrintTextBoxPair("Владелец:", _cardTxt.OwnerTitle, HEADER_X, y, HEADER_TITLE_WIDTH, HEADER_DATA_WIDTH, HEADER_LINE_HEIGHT);
			y += SelectedPrintTextBoxPair("Адрес:", _cardTxt.OwnerAddress, HEADER_X, y, HEADER_TITLE_WIDTH, HEADER_DATA_WIDTH, HEADER_LINE_HEIGHT);
			y += SelectedPrintTextBoxPair("Контакты:", _cardTxt.OwnerContacts, HEADER_X, y, HEADER_TITLE_WIDTH, HEADER_DATA_WIDTH, HEADER_LINE_HEIGHT);
			y += SelectedPrintTextNoBoxPair("Доверенное лицо:", _cardTxt.RepresentTitle + " " + _cardTxt.RepresentContacts, HEADER_X, y, HEADER_TITLE_WIDTH, HEADER_DATA_WIDTH, HEADER_LINE_HEIGHT);
			rect_header.Height = y - rect_header.Y;
			SelectedPrintRoundBox(rect_header, ROUNDCORNER_RADIUS);
			SelectedPrintLineVrt(rect_header.X + HEADER_TITLE_WIDTH, rect_header.Y, rect_header.Height);

			string file_name = LogoFileName(_card.DialerOfficial);

			RectangleF rectLogoImage = new RectangleF((LOGO_BLOCK_X + PAGE_RIGHT_X - LOGO_IMAGE_WIDTH) / 2.0F, offset, LOGO_IMAGE_WIDTH, LOGO_IMAGE_HEIGHT);
			SelectedPrintImage(file_name, rectLogoImage);
			rectLogoImage.Y += LOGO_IMAGE_HEIGHT;
			if (OfficialMark(_card.DialerOfficial))
			{
				rectLogoImage.Height = HEADER_LINE_HEIGHT;
				SelectedPrintTextNoBoxNoMeasure("ОФИЦИАЛЬНЫЙ ДИЛЕР", rectLogoImage);
				rectLogoImage.Y += HEADER_LINE_HEIGHT;
			}
			rectLogoImage.X = LOGO_BLOCK_X;
			rectLogoImage.Width = PAGE_RIGHT_X - LOGO_BLOCK_X;
			rectLogoImage.Height = y - rectLogoImage.Y;
			SelectedPrintRoundBox(rectLogoImage, ROUNDCORNER_RADIUS);

			SelectFont(font_small);
			SetStringFormat(StringAlignment.Center, StringAlignment.Center);
			string txt = FileIni.GetParameter("print.ini", "#ADDRESS_BLOCK");
			SelectedPrintTextNoBoxNoMeasure(txt, rectLogoImage);

			return y + OFFSET_AFTER_HEADER;
		}
		private float PrintWorkBlockHead2(float offset, object o = null, int count = 0)
		{			
			const int ROW_HEIGHT = 6;
			const int TOP_OFFSET = 0;

			object[] line_data = new object[11];
			line_data[0] = new object[] { "№ПП", WORK_WIDTH_COLUMN1 };
			line_data[1] = new object[] { "Код работы", WORK_WIDTH_COLUMN2 };
			line_data[2] = new object[] { "Наименование работы", WORK_WIDTH_COLUMN3 };
			line_data[3] = new object[] { "К.оп.", WORK_WIDTH_COLUMN4 };
			line_data[4] = new object[] { "К-во", WORK_WIDTH_COLUMN5 };
			line_data[5] = new object[] { "Ед.Изм.", WORK_WIDTH_COLUMN6 };
			line_data[6] = new object[] { "Цена", WORK_WIDTH_COLUMN7 };
			line_data[7] = new object[] { "Скидка", WORK_WIDTH_COLUMN8 };
			line_data[8] = new object[] { "Сумма НДС", WORK_WIDTH_COLUMN9 };
			line_data[9] = new object[] { "Всего без НДС", WORK_WIDTH_COLUMN10 };
			line_data[10] = new object[] {"Всего с НДС", WORK_WIDTH_COLUMN11 };

			float y = offset + TOP_OFFSET;
			SetStringFormat(StringAlignment.Center, StringAlignment.Center);
			SelectFont(font_small_bold);
			y += SelectedPrintTableGroup(line_data, PAGE_LEFT_X, y, ROW_HEIGHT);
			return y;
		}
		private float PrintWorkBlock(float offset, object o, int count)
		{
			const int ROW_HEIGHT = 4;
			const int TOP_OFFSET = 0;

			DtCardWork work = (DtCardWork)o;		
			CalculatorResult res = _calculatorCard.WorkCalculator.Calculate(work);
			DtTxtCardWork txtCardWork = new DtTxtCardWork(work);

			object[] line_data;
			if (work.GuaranteeFlag())
			{
				line_data = new object[7];
				line_data[0] = new object[] { count.ToString(), WORK_WIDTH_COLUMN1 };
				line_data[1] = new object[] { txtCardWork.CatalogueNumber, WORK_WIDTH_COLUMN2 };
				line_data[2] = new object[] { txtCardWork.WorkName, WORK_WIDTH_COLUMN3, ALIGN_X.LEFT };
				line_data[3] = new object[] { txtCardWork.OperationAmount, WORK_WIDTH_COLUMN4 };
				line_data[4] = new object[] { txtCardWork.Amount, WORK_WIDTH_COLUMN5 };
				line_data[5] = new object[] { txtCardWork.Unit, WORK_WIDTH_COLUMN6 };
				line_data[6] = new object[] { "Гарантия", WORK_WIDTH_COLUMN7 + WORK_WIDTH_COLUMN8 + WORK_WIDTH_COLUMN9 + WORK_WIDTH_COLUMN10 + WORK_WIDTH_COLUMN11 };
			}
			else
			{
				line_data = new object[11];
				line_data[0] = new object[] { count.ToString(), WORK_WIDTH_COLUMN1 };
				line_data[1] = new object[] { txtCardWork.CatalogueNumber, WORK_WIDTH_COLUMN2 };
				line_data[2] = new object[] { txtCardWork.WorkName, WORK_WIDTH_COLUMN3, ALIGN_X.LEFT };
				line_data[3] = new object[] { txtCardWork.OperationAmount, WORK_WIDTH_COLUMN4 };
				line_data[4] = new object[] { txtCardWork.Amount, WORK_WIDTH_COLUMN5 };
				line_data[5] = new object[] { txtCardWork.Unit, WORK_WIDTH_COLUMN6 };
				line_data[6] = new object[] { txtCardWork.Price, WORK_WIDTH_COLUMN7 };
				line_data[7] = new object[] { res.SummTotalDiscountBonus.ToString(), WORK_WIDTH_COLUMN8 };
				line_data[8] = new object[] { res.SummVAT.ToString(), WORK_WIDTH_COLUMN9 };
				line_data[9] = new object[] { res.SummTotalNoVAT.ToString(), WORK_WIDTH_COLUMN10 };
				line_data[10] = new object[] { res.SummTotal.ToString(), WORK_WIDTH_COLUMN11 };
			}

			float y = offset + TOP_OFFSET;
			SetStringFormat(StringAlignment.Center, StringAlignment.Center);
			SelectFont(font_small);
			y += SelectedPrintTableGroup(line_data, PAGE_LEFT_X, y, ROW_HEIGHT);
			return y;
		}
		private float PrintWorkBlockTotal(float offset, object o = null, int count = 0)
		{			
			const int ROW_HEIGHT = 6;
			const int TOP_OFFSET = 0;

			CalculatorResult res = _calculatorCard.WorksTotal;

			object[] line_data = new object[9];
			line_data[0] = new object[] { "ИТОГО", WORK_WIDTH_COLUMN1 + WORK_WIDTH_COLUMN2 + WORK_WIDTH_COLUMN3 };
			line_data[1] = new object[] { res.SimpleAmount.ToString(), WORK_WIDTH_COLUMN4 };
			line_data[2] = new object[] { res.SimpleAmountUnit.ToString(), WORK_WIDTH_COLUMN5 };
			line_data[3] = new object[] { "", WORK_WIDTH_COLUMN6 };
			line_data[4] = new object[] { "", WORK_WIDTH_COLUMN7 };
			line_data[5] = new object[] { res.SummTotalDiscountBonus.ToString(), WORK_WIDTH_COLUMN8 };
			line_data[6] = new object[] { res.SummVAT.ToString(), WORK_WIDTH_COLUMN9 };
			line_data[7] = new object[] { res.SummTotalNoVAT.ToString(), WORK_WIDTH_COLUMN10 };
			line_data[8] = new object[] { res.SummTotal.ToString(), WORK_WIDTH_COLUMN11 };


			float y = offset + TOP_OFFSET;
			SetStringFormat(StringAlignment.Center, StringAlignment.Center);
			SelectFont(font_small_bold);
			y += SelectedPrintTableGroup(line_data, PAGE_LEFT_X, y, ROW_HEIGHT);
			return y;
		}
		private float PrintDetailBlockHead2(float offset, object o = null, int count = 0)
		{
			const int ROW_HEIGHT = 6;
			const int TOP_OFFSET = 3;

			object[] line_data = new object[10];
			line_data[0] = new object[] { "% ПП", DETAIL_WIDTH_COLUMN1 };
			line_data[1] = new object[] { "№ по каталогу", DETAIL_WIDTH_COLUMN2 };
			line_data[2] = new object[] { "Наименование детали", DETAIL_WIDTH_COLUMN3 };
			line_data[3] = new object[] { "Кол-во", DETAIL_WIDTH_COLUMN4 };
			line_data[4] = new object[] { "Ед. изм.", DETAIL_WIDTH_COLUMN5 };
			line_data[5] = new object[] { "Скидка", DETAIL_WIDTH_COLUMN6 };
			line_data[6] = new object[] { "Цена", DETAIL_WIDTH_COLUMN7 };
			line_data[7] = new object[] { "Сумма НДС", DETAIL_WIDTH_COLUMN8 };
			line_data[8] = new object[] { "Всего без НДС", DETAIL_WIDTH_COLUMN9 };
			line_data[9] = new object[] { "Всего с НДС", DETAIL_WIDTH_COLUMN10 };

			float y = offset + TOP_OFFSET;
			SetStringFormat(StringAlignment.Center, StringAlignment.Center);
			SelectFont(font_small_bold);
			y += SelectedPrintTableGroup(line_data, PAGE_LEFT_X, y, ROW_HEIGHT);
			return y;
		}
		private float PrintDetailBlock2(float offset, object o, int count)
		{
			const int ROW_HEIGHT = 4;
			const int TOP_OFFSET = 0;

			DtCardDetail detail = (DtCardDetail)o;
			CalculatorResult res = _calculatorCard.DetailCalculator.Calculate(detail);
			DtTxtCardDetail txtCardDetail = new DtTxtCardDetail(detail);

			object[] line_data;
			if (detail.GuaranteeFlag())
			{
				line_data = new object[10];
				line_data[0] = new object[] { count.ToString(), DETAIL_WIDTH_COLUMN1 };
				line_data[1] = new object[] { txtCardDetail.CatalogueNumber, DETAIL_WIDTH_COLUMN2 };
				line_data[2] = new object[] { txtCardDetail.WorkName, DETAIL_WIDTH_COLUMN3, ALIGN_X.LEFT };
				line_data[3] = new object[] { res.SimpleAmount.ToString(), DETAIL_WIDTH_COLUMN4 };
				line_data[4] = new object[] { txtCardDetail.Unit, DETAIL_WIDTH_COLUMN5 };
				line_data[5] = new object[] { "Гарантия", DETAIL_WIDTH_COLUMN6 + DETAIL_WIDTH_COLUMN7 + DETAIL_WIDTH_COLUMN8 + DETAIL_WIDTH_COLUMN9 + DETAIL_WIDTH_COLUMN10};
			}
			else
			{
				line_data = new object[10];
				line_data[0] = new object[] { count.ToString(), DETAIL_WIDTH_COLUMN1 };
				line_data[1] = new object[] { txtCardDetail.CatalogueNumber, DETAIL_WIDTH_COLUMN2 };
				line_data[2] = new object[] { txtCardDetail.WorkName, DETAIL_WIDTH_COLUMN3, ALIGN_X.LEFT };
				line_data[3] = new object[] { res.SimpleAmount.ToString(), DETAIL_WIDTH_COLUMN4 };
				line_data[4] = new object[] { txtCardDetail.Unit, DETAIL_WIDTH_COLUMN5 };
				line_data[5] = new object[] { res.SummTotalDiscountBonus.ToString(), DETAIL_WIDTH_COLUMN6 };
				line_data[6] = new object[] { txtCardDetail.Price, DETAIL_WIDTH_COLUMN7 };
				line_data[7] = new object[] { res.SummVAT.ToString(), DETAIL_WIDTH_COLUMN8 };
				line_data[8] = new object[] { res.SummTotalNoVAT.ToString(), DETAIL_WIDTH_COLUMN9 };
				line_data[9] = new object[] { res.SummTotal.ToString(), DETAIL_WIDTH_COLUMN10 };
			}
			
			float y = offset + TOP_OFFSET;
			SetStringFormat(StringAlignment.Center, StringAlignment.Center);
			SelectFont(font_small);
			y += SelectedPrintTableGroup(line_data, PAGE_LEFT_X, y, ROW_HEIGHT);
			return y;
		}
		private float PrintDetailBlockTotal(float offset, object o = null, int count = 0)
		{
			const int ROW_HEIGHT = 6;
			const int TOP_OFFSET = 0;

			CalculatorResult res = _calculatorCard.DetailsTotal;

			object[] line_data = new object[8];
			line_data[0] = new object[] { "ИТОГО", DETAIL_WIDTH_COLUMN1 + DETAIL_WIDTH_COLUMN2 + DETAIL_WIDTH_COLUMN3, ALIGN_X.RIGHT };
			line_data[1] = new object[] { res.SimpleAmount.ToString(), DETAIL_WIDTH_COLUMN4 };
			line_data[2] = new object[] { "", DETAIL_WIDTH_COLUMN5 };
			line_data[3] = new object[] { res.SummTotalDiscountBonus.ToString(), DETAIL_WIDTH_COLUMN6 };
			line_data[4] = new object[] { "", DETAIL_WIDTH_COLUMN7 };
			line_data[5] = new object[] { res.SummVAT.ToString(), DETAIL_WIDTH_COLUMN8 };
			line_data[6] = new object[] { res.SummTotalNoVAT.ToString(), DETAIL_WIDTH_COLUMN9 };
			line_data[7] = new object[] { res.SummTotal.ToString(), DETAIL_WIDTH_COLUMN10 };

			float y = offset + TOP_OFFSET;
			SetStringFormat(StringAlignment.Center, StringAlignment.Center);
			SelectFont(font_small_bold);
			y += SelectedPrintTableGroup(line_data, PAGE_LEFT_X, y, ROW_HEIGHT);
			return y;
		}
		private float PrintInviteTotal(float offset, object o = null, int count = 0)
		{
			const int TOP_OFFSET = 4;
			float y = offset + TOP_OFFSET;

			string txt = _cardTxt.CardPersonTitle;
			if (txt != "")
				txt += ", " + "приглашаем Вас посетить наш автосалон.\n";
			else
				txt = "Приглашаем Вас посетить наш автосалон.\n";
			txt += "-Мы предоставляем гарантию на выполненые работы и установленные запасные части;\n";
			txt += "-Работы выполняются специалистами, регулярно проходящими обучения в центрах заводов-изготовителей;\n";
			txt += "-При ремонте и техническом обслуживании используются оригинальные запасные части и расходные материалы;\n";
			txt += "-Работает система скидок;\n";
			txt += "-Запись производиться по телефону (383)330-03-03;\n";
			txt += "-Пожалуйста, при обращении по приглашению, уточняйте наличие запчастей;\n";

			SelectFont(font_small_bold);
			SetStringFormat(StringAlignment.Near, StringAlignment.Near);
			
			y += SelectedPrintTextBox(txt, PAGE_LEFT_X, y, PAGE_RIGHT_X - PAGE_LEFT_X, 0);
			return y;
		}
		#endregion
	}
}
