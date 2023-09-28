using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AutoBaseSql
{
    // Класс описывает общие элементы печати для всех вариантов карточки - заголовки футеры и т.д.
    class PrintCard:PrintCommon
    {

		protected const float PAGE_RIGHT_X = 200; // Граница страницы по ширине
		protected const float PAGE_LEFT_X = 10;

		protected const float WORK_WIDTH_COLUMN1 = 8;
		protected const float WORK_WIDTH_COLUMN2 = 15;
		protected const float WORK_WIDTH_COLUMN4 = 7;
		protected const float WORK_WIDTH_COLUMN5 = 12;
		protected const float WORK_WIDTH_COLUMN6 = 10;
		protected const float WORK_WIDTH_COLUMN7 = 12;
		protected const float WORK_WIDTH_COLUMN8 = 14;
		protected const float WORK_WIDTH_COLUMN9 = 15;
		protected const float WORK_WIDTH_COLUMN10 = 15;
		protected const float WORK_WIDTH_COLUMN11 = 14;
		protected const float WORK_WIDTH_COLUMN12 = 18;
		protected const float WORK_WIDTH_COLUMN3 = PAGE_RIGHT_X - WORK_WIDTH_COLUMN1 - WORK_WIDTH_COLUMN2 - WORK_WIDTH_COLUMN4 - WORK_WIDTH_COLUMN5 - WORK_WIDTH_COLUMN6 - WORK_WIDTH_COLUMN7 - WORK_WIDTH_COLUMN8 - WORK_WIDTH_COLUMN9 - WORK_WIDTH_COLUMN10 - WORK_WIDTH_COLUMN11 - WORK_WIDTH_COLUMN12 - PAGE_LEFT_X;

		protected const float DETAIL_WIDTH_COLUMN1 = 8;
		protected const float DETAIL_WIDTH_COLUMN2 = 20;
		protected const float DETAIL_WIDTH_COLUMN4 = 10;
		protected const float DETAIL_WIDTH_COLUMN5 = 15;
		protected const float DETAIL_WIDTH_COLUMN6 = 18;
		protected const float DETAIL_WIDTH_COLUMN7 = 18;
		protected const float DETAIL_WIDTH_COLUMN8 = 18;
		protected const float DETAIL_WIDTH_COLUMN9 = 18;
		protected const float DETAIL_WIDTH_COLUMN10 = 18;
		protected const float DETAIL_WIDTH_COLUMN3 = PAGE_RIGHT_X - DETAIL_WIDTH_COLUMN1 - DETAIL_WIDTH_COLUMN2 - DETAIL_WIDTH_COLUMN4 - DETAIL_WIDTH_COLUMN5 - DETAIL_WIDTH_COLUMN6 - DETAIL_WIDTH_COLUMN7 - DETAIL_WIDTH_COLUMN8 - DETAIL_WIDTH_COLUMN9 - DETAIL_WIDTH_COLUMN10 - PAGE_LEFT_X;

		protected readonly SolidBrush _brushStandart;
		protected readonly Font _fontArial8Bold;
		protected readonly Font _fontArial6Bold;
		protected readonly Font _fontArial8;
		protected readonly Font _fontArial6;
		protected readonly Font _fontArial8Italic;
		protected readonly Font _fontArial10Bold;
		protected readonly Pen _penStandart03;

        protected readonly DtCard _card;
        protected readonly DtTxtCard _cardTxt;
        public PrintCard(DtCard srcCard) : base()
        {
            _brushStandart = new SolidBrush(Color.Black);
            _fontArial8Bold = new Font("Arial", 8, FontStyle.Bold);
			_fontArial6Bold = new Font("Arial", 6, FontStyle.Bold);
			_fontArial8 = new Font("Arial", 8);
			_fontArial6 = new Font("Arial", 6);
			_fontArial8Italic = new Font("Arial", 8, FontStyle.Italic);
            _fontArial10Bold = new Font("Arial", 10, FontStyle.Bold);
            _penStandart03 = new Pen(_brushStandart, 0.3F);

            _card = srcCard;
            _cardTxt = new DtTxtCard(srcCard);
        }

		public override float PrintStandartHead(float offset) // Печать стандартного заголовка
		{
			// НЕИЗМЕНЯЕМЫЕ ДАННЫЕ
			const float TEXT_OFFSET_X = 1; // Отсуп текста внутри оконтовки
			const float LOGO_BLOCK_X = PAGE_LEFT_X; // HEADER_TITLE_WIDTH + HEADER_DATA_WIDTH + HEADER_X + 5; // Начало для блока логотипа
			const float MAINHEAD_OFFSET = 3;
			
			const float LOGO_IMAGE_WIDTH = 39; // Ширина картинки логотипа
			const float LOGO_IMAGE_HEIGHT = 20; // Высота картинки логотипа
			const float OFFSET_AFTER_HEADER = 3; // Отступ после заголовка - всегда

			const float MAINHEAD_WIDTH_COLUMN1 = 25;
			const float MAINHEAD_WIDTH_COLUMN2 = 40;
			const float MAINHEAD_WIDTH_COLUMN3 = 35;
			const float MAINHEAD_WIDTH_COLUMN4 = PAGE_RIGHT_X - MAINHEAD_WIDTH_COLUMN1 - MAINHEAD_WIDTH_COLUMN2 - MAINHEAD_WIDTH_COLUMN3 - PAGE_LEFT_X;
			const float MAINHEAD_ROW_HEIGHT = 6;

			// Учстанавливаем используемые для печати имнтрументы

			SelectTextBoxOffsetX(TEXT_OFFSET_X);
			SelectFont(_fontArial8);
			SelectBrush(_brushStandart);
			SelectPen(_penStandart03);
			SetStringFormat(StringAlignment.Near, StringAlignment.Center);

			string file_name = LogoFileName(_card.DialerOfficial);
			RectangleF rectLogoImage = new RectangleF(LOGO_BLOCK_X, offset, LOGO_IMAGE_WIDTH, LOGO_IMAGE_HEIGHT);
			SelectedPrintImage(file_name, rectLogoImage);
			rectLogoImage.X += rectLogoImage.Width + MAINHEAD_OFFSET;
			rectLogoImage.Width += PAGE_RIGHT_X - rectLogoImage.X;
			string txt = "Официальный дилер:  Общество с ограниченной ответсвенностью \"Авто - 1\"" + "\n";
			txt += "Адрес дилерского центра:  г. Новосибирск, ул. Русская, 48" + "\n";
			txt += "Тел.: (383)330-03-03" + "\n";
			txt += "Web: avto1.lada.ru, E-mail: sale@avto-1.ru" + "\n";
			SelectedPrintTextNoBoxNoMeasure(txt, rectLogoImage);
			float y = offset + rectLogoImage.Height + MAINHEAD_OFFSET;
			
			y += PrintDocumentTitle(y);

			SetStringFormat(StringAlignment.Near, StringAlignment.Center);
			// Новый заголовок карточки
			object[] line_data = new object[4];
			object[] line_data2 = new object[2];
			line_data2[0] = new object[] { "Данные об автомобиле", MAINHEAD_WIDTH_COLUMN1 + MAINHEAD_WIDTH_COLUMN2 };
			line_data2[1] = new object[] { "Данные о клиенте", MAINHEAD_WIDTH_COLUMN3 + MAINHEAD_WIDTH_COLUMN4 };
			y += SelectedPrintTableGroup(line_data2, PAGE_LEFT_X, y, MAINHEAD_ROW_HEIGHT);
			line_data[0] = new object[] { "Модель", MAINHEAD_WIDTH_COLUMN1 };
			line_data[1] = new object[] { _cardTxt.AutoModel, MAINHEAD_WIDTH_COLUMN2 };
			line_data[2] = new object[] { "Собственник", MAINHEAD_WIDTH_COLUMN3 };
			line_data[3] = new object[] { _cardTxt.OwnerTitle, MAINHEAD_WIDTH_COLUMN4 };
			y += SelectedPrintTableGroup(line_data, PAGE_LEFT_X, y, MAINHEAD_ROW_HEIGHT);
			line_data[0] = new object[] { "VIN", MAINHEAD_WIDTH_COLUMN1 };
			line_data[1] = new object[] { _cardTxt.AutoVin, MAINHEAD_WIDTH_COLUMN2 };
			line_data[2] = new object[] { "Адрес собственника", MAINHEAD_WIDTH_COLUMN3 };
			line_data[3] = new object[] { _cardTxt.OwnerAddress, MAINHEAD_WIDTH_COLUMN4 };		
			line_data2[0] = new object[] { "Дата продажи", MAINHEAD_WIDTH_COLUMN1 };
			line_data2[1] = new object[] { _cardTxt.AutoSellDate, MAINHEAD_WIDTH_COLUMN2 };
			y += SelectedPrintTableGroupMerge(line_data, line_data2, PAGE_LEFT_X, y, MAINHEAD_ROW_HEIGHT);
			line_data[0] = new object[] { "Год выпуска", MAINHEAD_WIDTH_COLUMN1 };
			line_data[1] = new object[] { _cardTxt.AutoYear, MAINHEAD_WIDTH_COLUMN2 };
			line_data[2] = new object[] { "Заказчик / Доверенное лицо", MAINHEAD_WIDTH_COLUMN3 };
			line_data[3] = new object[] { _cardTxt.RepresentTitle, MAINHEAD_WIDTH_COLUMN4 };
			y += SelectedPrintTableGroup(line_data, PAGE_LEFT_X, y, MAINHEAD_ROW_HEIGHT);
			line_data[0] = new object[] { "Пробег", MAINHEAD_WIDTH_COLUMN1 };
			line_data[1] = new object[] { _cardTxt.Run, MAINHEAD_WIDTH_COLUMN2 };
			line_data[2] = new object[] { "Адрес телефон e-mail заказчика / доверенного лица", MAINHEAD_WIDTH_COLUMN3 };
			line_data[3] = new object[] { _cardTxt.RepresentAddress + " / " + _cardTxt.RepresentContacts, MAINHEAD_WIDTH_COLUMN4 };
			line_data2[0] = new object[] { "Регистрационный знак", MAINHEAD_WIDTH_COLUMN1 };
			line_data2[1] = new object[] { _cardTxt.AutoLicensePlate, MAINHEAD_WIDTH_COLUMN2 };
			y += SelectedPrintTableGroupMerge(line_data, line_data2, PAGE_LEFT_X, y, MAINHEAD_ROW_HEIGHT);
			line_data[0] = new object[] { "Номер двигателя", MAINHEAD_WIDTH_COLUMN1 };
			line_data[1] = new object[] { "-", MAINHEAD_WIDTH_COLUMN2 };
			line_data[2] = new object[] { "Плательщик", MAINHEAD_WIDTH_COLUMN3 };
			line_data[3] = new object[] { _cardTxt.PayerTitle, MAINHEAD_WIDTH_COLUMN4 };
			y += SelectedPrintTableGroup(line_data, PAGE_LEFT_X, y, MAINHEAD_ROW_HEIGHT);
			line_data[0] = new object[] { "", MAINHEAD_WIDTH_COLUMN1 };
			line_data[1] = new object[] { "", MAINHEAD_WIDTH_COLUMN2 };
			line_data[2] = new object[] { "Адрес плательщика", MAINHEAD_WIDTH_COLUMN3 };
			line_data[3] = new object[] { _cardTxt.PayerAddress, MAINHEAD_WIDTH_COLUMN4 };
			y += SelectedPrintTableGroup(line_data, PAGE_LEFT_X, y, MAINHEAD_ROW_HEIGHT);

			return y + OFFSET_AFTER_HEADER;
		}
	}
}
