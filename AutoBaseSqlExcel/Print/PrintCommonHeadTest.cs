using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using AutoBaseSql.Print;

namespace AutoBaseSql
{
    // Класс описывает общие элементы печати для всех вариантов карточки - заголовки футеры и т.д.
    class PrintCommonHeadTest:PrintDocumentBase
    {
        // НЕИЗМЕНЯЕМЫЕ ДАННЫЕ
        const float TEXT_OFFSET_X = 1; // Отсуп текста внутри оконтовки
        const float LOGO_BLOCK_X = PAGE_LEFT_X; // HEADER_TITLE_WIDTH + HEADER_DATA_WIDTH + HEADER_X + 5; // Начало для блока логотипа
        const float MAINHEAD_OFFSET = 3;

        const float LOGO_IMAGE_WIDTH = 39; // Ширина картинки логотипа
        const float LOGO_IMAGE_HEIGHT = 20; // Высота картинки логотипа
        const float OFFSET_AFTER_HEADER = 3; // Отступ после заголовка - всегда

        protected const float PAGE_RIGHT_X = 200;  // Граница страницы по ширине
        protected const float PAGE_LEFT_X = 10;    // Размер отступа по странице справа

        const float MAINHEAD_WIDTH_COLUMN1 = 25;
        const float MAINHEAD_WIDTH_COLUMN2 = 40;
        const float MAINHEAD_WIDTH_COLUMN3 = 35;
        const float MAINHEAD_WIDTH_COLUMN4 = PAGE_RIGHT_X - MAINHEAD_WIDTH_COLUMN1 - MAINHEAD_WIDTH_COLUMN2 - MAINHEAD_WIDTH_COLUMN3 - PAGE_LEFT_X;
        const float MAINHEAD_ROW_HEIGHT = 6;

        // Размеры колонок в таблице работ
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
        // Размеры колонок в таблице деталей
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

        private readonly PrintableTable _headerTable;

        public PrintCommonHeadTest(DtCard srcCard) : base()
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

            _printTools = new PrintDrawingBase();

            _headerTable = new PrintableTable(7, 4);
            _headerTable.SetColumnWidth(1, MAINHEAD_WIDTH_COLUMN1);
            _headerTable.SetColumnWidth(2, MAINHEAD_WIDTH_COLUMN2);
            _headerTable.SetColumnWidth(3, MAINHEAD_WIDTH_COLUMN3);
            _headerTable.SetColumnWidth(4, MAINHEAD_WIDTH_COLUMN4);
            _headerTable.RowsHeight = MAINHEAD_ROW_HEIGHT;

            _headerTable.AddMergedCell(1, 1, "Данные об автомобиле", 2);
            _headerTable.AddMergedCell(1, 3, "Данные об клиенте", 2);

            _headerTable.AddCell(2, 1, "Модель");
            _headerTable.AddCell(2, 2, _cardTxt.AutoModel);
            _headerTable.AddCell(2, 3, "Собственник");
            _headerTable.AddCell(2, 4, _cardTxt.OwnerTitle);
            _headerTable.AddCell(3, 1, "VIN");
            _headerTable.AddCell(3, 1, "Дата продажи");
            _headerTable.AddCell(3, 2, _cardTxt.AutoVin);
            _headerTable.AddCell(3, 2, _cardTxt.AutoSellDate);
            _headerTable.AddCell(3, 3, "Адрес собственника");
            _headerTable.AddCell(3, 4, _cardTxt.OwnerAddress);
            _headerTable.AddCell(4, 1, "Год выпуска");
            _headerTable.AddCell(4, 2, _cardTxt.AutoYear);
            _headerTable.AddCell(4, 3, "Заказчик / Доверенное лицо");
            _headerTable.AddCell(4, 4, _cardTxt.RepresentTitle);
            _headerTable.AddCell(5, 1, "Пробег");
            _headerTable.AddCell(5, 2, _cardTxt.Run);
            _headerTable.AddCell(5, 1, "Регистрационный знак");
            _headerTable.AddCell(5, 2, _cardTxt.AutoLicensePlate);
            _headerTable.AddCell(5, 3, "Адрес телефон e-mail заказчика / доверенного лица");
            _headerTable.AddCell(5, 4, _cardTxt.RepresentAddress + " / " + _cardTxt.RepresentContacts);
            _headerTable.AddCell(6, 1, "Номер двигателя");
            _headerTable.AddCell(6, 2, "-");
            _headerTable.AddCell(6, 3, "Плательщик");
            _headerTable.AddCell(6, 4, _cardTxt.PayerTitle);
            _headerTable.AddCell(7, 1, "");
            _headerTable.AddCell(7, 2, "");
            _headerTable.AddCell(7, 3, "Адрес плательщика");
            _headerTable.AddCell(7, 4, _cardTxt.PayerAddress);
        }

        public  float PrintStandartHead(float offset, object data = null) // Печать стандартного заголовка
        {
            // Настройка инструментов печати
            _printTools.SelectTextBoxOffsetX(TEXT_OFFSET_X);
            _printTools.SelectFont(_fontArial8);
            _printTools.SelectBrush(_brushStandart);
            _printTools.SelectPen(_penStandart03);
            _printTools.SetStringFormat(StringAlignment.Near, StringAlignment.Center);

            string file_name = LogoFileName(_card.DialerOfficial);
            RectangleF rectLogoImage = new RectangleF(LOGO_BLOCK_X, offset, LOGO_IMAGE_WIDTH, LOGO_IMAGE_HEIGHT);
            _printTools.SelectedPrintImage(file_name, rectLogoImage);
            rectLogoImage.X += rectLogoImage.Width + MAINHEAD_OFFSET;
            rectLogoImage.Width += PAGE_RIGHT_X - rectLogoImage.X;
            string txt = "Официальный дилер:  Общество с ограниченной ответсвенностью \"Авто - 1\"" + "\n";
            txt += "Адрес дилерского центра:  г. Новосибирск, ул. Русская, 48" + "\n";
            txt += "Тел.: (383)330-03-03" + "\n";
            txt += "Web: avto1.lada.ru, E-mail: sale@avto-1.ru" + "\n";
            _printTools.SelectedPrintTextNoBoxNoMeasure(txt, rectLogoImage);
            float y = offset + rectLogoImage.Height + MAINHEAD_OFFSET;

            y += PrintDocumentTitle(y);

            y = PrintDocumentHeader(PAGE_LEFT_X, y);
            return y + OFFSET_AFTER_HEADER;    
        }

        public float PrintDocumentHeader(float x, float y)
        {

            _headerTable.MesureTable(_printTools);
            y += _headerTable.Print(x, y, _printTools);
            return y;
        }

    }
}
