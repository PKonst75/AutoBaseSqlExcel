using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Windows.Forms;


namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbPrintGuarantyKIA.
	/// </summary>
	public class DbPrintGuarantyKIA:DbPrint
	{
		SolidBrush	brush_standart;
		Font		font_small_bold;
		Font		font_middle_bold;
		Font		font_small;
		Font		font_middle;
		Pen			pen_thin;

		private DtCard		card;

		#region Данные для печати
		protected class HeaderData
		{
			public enum DIALER:long{unknown=0, chevrolet=1, lada=2, kia=3}
			public string txt_model		= "";
			public string txt_sign		= "";
			public string txt_vin		= "";
			public string txt_engine	= "";
			public string txt_owner		= "";
			public string txt_address	= "";
			public string txt_phone		= "";
			public string txt_represent	= "";
			public string txt_sell		= "";
			public string txt_run		= "";
			public string txt_card		= "";
			public string txt_workshop	= "";
			public string txt_card_number		= "";
			public string txt_warrant_number	= "";
			public string txt_warrant_close		= "";
			public string txt_warrant_open		= "";
			public string txt_card_open			= "";
			public DIALER dialer		= DIALER.unknown;

			public ArrayList	claims	= null;

			public HeaderData(DtCard card)
			{
				// Данные карточки
				txt_card = card.GetData("НОМЕР_КАРТОЧКА").ToString() + "/" + card.GetData("ГОД_КАРТОЧКА").ToString();
				txt_card_number = card.GetData("НОМЕР_КАРТОЧКА").ToString();

				// Данные карточки
				txt_card_open			= card.GetData("ДАТА").ToString();
				if((long)card.GetData("НОМЕР_НАРЯД_КАРТОЧКА") != 0)
				{
					txt_warrant_open	= card.GetData("ДАТА_НАРЯД_ОТКРЫТ_КАРТОЧКА").ToString();
					txt_warrant_number	= card.GetData("НОМЕР_НАРЯД_КАРТОЧКА").ToString();
				}
				if((short)card.GetData("СТАТУС_КАРТОЧКА") == 2)
				{
					txt_warrant_close = card.GetData("ДАТА_НАРЯД_ЗАКРЫТ_КАРТОЧКА").ToString();
				}
				int run = (int)card.GetData("ПРОБЕГ_КАРТОЧКА");
				if(run != 0)
				{
					txt_run		= run.ToString();
				}
				// Запрос об изменении данных карточки
				if(MessageBox.Show("Ввести данные вручную?", "Запрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					// Изменение данных
					FormSelectDataGuaranty dialog = new FormSelectDataGuaranty();
					// Настройка элементов диалога
					dialog.number		= txt_card_number;
					dialog.time_request	= (DateTime)card.GetData("ДАТА");
					dialog.time_begin	= (DateTime)card.GetData("ДАТА_НАРЯД_ОТКРЫТ_КАРТОЧКА");
					dialog.time_end		= (DateTime)card.GetData("ДАТА_НАРЯД_ЗАКРЫТ_КАРТОЧКА");
					if(dialog.ShowDialog() == DialogResult.OK)
					{
						txt_card_number			= dialog.number;
						txt_card_open			= dialog.TimeRequest.ToString();
						txt_warrant_open		= dialog.TimeBegin.ToString();
						txt_warrant_close		= dialog.TimeEnd.ToString();
					}
				}
				// Загрузка отображаемых данных заголовка
				DtAuto auto = DbSqlAuto.Find((long)card.CodeAuto/*GetData("АВТОМОБИЛЬ_КАРТОЧКА")*/);
				if(auto != null)
				{
					txt_sign	= (string)auto.GetData("НОМЕР_ЗНАК");
					txt_vin		= (string)auto.GetData("VIN");

					// Если есть VIN завода-изготовителя, печатаем его в скобках
					string txt_vin_origin = (string)auto.GetData("VIN_ПРОИЗВОДИТЕЛЬ");
					if (txt_vin_origin != "")
						txt_vin	+= " (" + txt_vin_origin + ")";

					txt_engine	= (string)auto.GetData("НОМЕР_ДВИГАТЕЛЬ");
					// Данные по автомобилю
					// Модель
					DtModel	model = DbSqlModel.Find((long)auto.GetData("ССЫЛКА_КОД_АВТОМОБИЛЬ_МОДЕЛЬ"));
					if(model != null)
						txt_model	= (string)model.GetData("МОДЕЛЬ");
					// Дата продажи
					if((bool)auto.GetData("ЕСТЬ_ПРОДАЖА_ДАТА") == true)
						txt_sell = ((DateTime)auto.GetData("ПРОДАЖА_ДАТА")).ToShortDateString();
					else
						txt_sell = "---------";

					// Тип официального дилера
					DtBrand brand = DbSqlBrand.FindModel((long)model.GetData("КОД_АВТОМОБИЛЬ_МОДЕЛЬ"));
					string txt_brand = "";
					if(brand != null)
						txt_brand = (string)brand.GetData("НАИМЕНОВАНИЕ_АВТОМОБИЛЬ_БРЕНД");
					if(txt_brand == "ШЕВРОЛЕ") dialer = DIALER.chevrolet;
					if(txt_brand == "LADA") dialer = DIALER.lada;
					if(txt_brand == "KIA") dialer = DIALER.kia;

					// АНАЛИЗ ПРОФИЛЬНОГО ДИЛЕРА
					string txt_dealer		= FileIni.GetParameter("print.ini", "#DEALER_BLOCK");
					char[] separators		= new Char[] {'\t'};
					string[] txt_dealers	= txt_dealer.Split(separators);
					bool flag				= false;
					foreach(string s in txt_dealers)
					{
						if(s == ((long)dialer).ToString()) flag = true;
					}
					if(!flag) dialer = DIALER.unknown;
				}
				else
				{

				}
				// Владелец
				DtPartner owner = DbSqlPartner.Find((long)card.GetData("ВЛАДЕЛЕЦ_КАРТОЧКА"));
				if(owner != null)
				{
					txt_owner		= owner.GetTitle();
					txt_address		= owner.GetAddress();
					txt_phone		= owner.GetPhone();
				}
				// Представитель
				DtPartner represent = DbSqlPartner.Find((long)card.GetData("ПРЕДСТАВИТЕЛЬ_КАРТОЧКА"));
				if(represent != null)
					txt_represent	= represent.GetTitle();

				// Подразделение
				DtWorkshop workshop =  DbSqlWorkshop.Find((long)card.GetData("ПОДРАЗДЕЛЕНИЕ_КАРТОЧКА"));
				if(workshop != null)
					txt_workshop = (string)workshop.GetData("НАИМЕНОВАНИЕ_ЦЕХ");

				// Список заявленных неисправностей/работ
				claims = new ArrayList();
				DbSqlCardClaim.SelectInArray(claims, (long)card.GetData("НОМЕР_КАРТОЧКА"), (int)card.GetData("ГОД_КАРТОЧКА"));
			}
		}
		HeaderData	header_data = null;
		#endregion

		public DbPrintGuarantyKIA(long card_number, int card_year)
		{
			// Подготовка инструментов для печати
			brush_standart		= new SolidBrush(Color.Black);
			font_small_bold		= new Font("Arial", 8, FontStyle.Bold);
			font_small			= new Font("Arial", 8);
			font_middle_bold	= new Font("Arial", 10, FontStyle.Bold);
			font_middle			= new Font("Arial", 10);
			pen_thin			= new Pen(brush_standart, 0.3F);

			// Заявка клиента печатается по карточке (даже по нрулевой)
			if(card_number != 0 && card_year != 0)
				// Поиск карточки по входным данным
				card =	DbSqlCard.Find(card_number, card_year);
			else
				// Карточка нулевая
				card = null;

			if(card != null)
			{
				// Сумели найти карточку, получаем данные для печати
				header_data = new HeaderData(card);
			}
		}

		#region Печать заголовка
		private int PrintHead(Graphics graph, int offset,  bool test, bool print, object o)
		{
			// Настройка параметров печати заголовка
			SizeF size;
			int	height;
			int	y;
			string txt	= "";

			int	header_height_real	= 0;		// Реально получившаяся высота заголовка
			int header_height		= 50;		// Фиксированная высота заголовка
			int text_offset			= 2;		// Отсуп текста внутри оконтовки
			int header_line_height	= 4;		// Высота линии
			int	header_x			= 10;		// Смещение заголовка по х
			int	header_title_width	= 40;		// Ширина блока заголовка для наименований
			int	header_data_width	= 75;		// Ширина блока заголовка для данных
			int x1					= 50;
			int x2					= 30;
			if(test == true || print == false)
			{
				// Просто рассчитываем высоту
				y = offset;
				y += 10;
				y += header_line_height;
				y += header_line_height;
				y += header_line_height;
				y += header_line_height;
				y += header_line_height;
				y += header_line_height;
				size	= MeasureTextBox(graph, header_data.txt_owner, header_data_width, text_offset, StringAlignment.Near, StringAlignment.Near, font_small_bold);
				height	= (int)Math.Ceiling(size.Height);
				if(height < header_line_height) height = header_line_height;
				y += height;
				size	= MeasureTextBox(graph, header_data.txt_address, header_data_width, text_offset, StringAlignment.Near, StringAlignment.Near, font_small_bold);
				height	= (int)Math.Ceiling(size.Height);
				if(height < header_line_height) height = header_line_height;
				y += height;
				y += header_line_height;
				y += header_line_height;
				return y + 3;	// Отсуп от заголовка
			}

			y	= offset;
			// НАЗВАНИЕ ДОКУМЕНТА
			PrintText(graph, "ЗАКАЗ-НАРЯД №" + header_data.txt_card_number, header_x, y, 150, 5, StringAlignment.Near, StringAlignment.Center, font_middle_bold, brush_standart, false);
			PrintText(graph, "Дата обращения:", header_x + x1 - 50, y, 70, 5, StringAlignment.Far, StringAlignment.Center, font_small, brush_standart, false);
			PrintText(graph, header_data.txt_card_open, header_x + x1 + x2, y, 150, 5, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, false);
			y += 3;
			PrintText(graph, "Открыт/Дата передачи автомобиля в сервис:", header_x + x1 - 50, y, 70, 5, StringAlignment.Far, StringAlignment.Center, font_small, brush_standart, false);
			PrintText(graph, header_data.txt_warrant_open, header_x + x1 + x2, y, 150, 5, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, false);
			y += 3;
			PrintText(graph, "Закрыт/Дата устранения дефекта:", header_x + x1 - 50, y, 70, 5, StringAlignment.Far, StringAlignment.Center, font_small, brush_standart, false);
			PrintText(graph, header_data.txt_warrant_close, header_x + x1 + x2, y, 150, 5, StringAlignment.Near, StringAlignment.Center, font_small, brush_standart, false);
			y += 4;
			// ДАННЫЕ ЗАГОЛОВКА
			// Модель
			PrintTextNoBox(graph, "Модель:", header_x, y, header_title_width, header_line_height, text_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextNoBox(graph, header_data.txt_model, header_x + header_title_width, y, header_data_width, header_line_height, text_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			y += header_line_height;
			// Регистрационный знак
			PrintTextBox(graph, "Гос. номер:", header_x, y, header_title_width, header_line_height, text_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, header_data.txt_sign, header_x + header_title_width, y, header_data_width, header_line_height, text_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			y += header_line_height;
			// VIN
			PrintTextBox(graph, "VIN:", header_x, y, header_title_width, header_line_height, text_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, header_data.txt_vin, header_x + header_title_width, y, header_data_width, header_line_height, text_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			y += header_line_height;
			// Номер двигателя
			PrintTextBox(graph, "Двигатель №:", header_x, y, header_title_width, header_line_height, text_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, header_data.txt_engine, header_x + header_title_width, y, header_data_width, header_line_height, text_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			y += header_line_height;
			// Дата продажи
			PrintTextBox(graph, "Дата продажи:", header_x, y, header_title_width, header_line_height, text_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, header_data.txt_sell, header_x + header_title_width, y, header_data_width, header_line_height, text_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			y += header_line_height;
			// Пробег
			PrintTextBox(graph, "Пробег:", header_x, y, header_title_width, header_line_height, text_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, header_data.txt_run, header_x + header_title_width, y, header_data_width, header_line_height, text_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			y += header_line_height;
			// Владелец
			// Сначала измеряем строчку, чтобы определить истинный размер
			size	= MeasureTextBox(graph, header_data.txt_owner, header_data_width, text_offset, StringAlignment.Near, StringAlignment.Near, font_small_bold);
			height	= (int)Math.Ceiling(size.Height);
			if(height < header_line_height) height = header_line_height;
			PrintTextBox(graph, "Владелец:", header_x, y, header_title_width, height, text_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, header_data.txt_owner, header_x + header_title_width, y, header_data_width, height, text_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			y += height;
			// Адрес
			size	= MeasureTextBox(graph, header_data.txt_address, header_data_width, text_offset, StringAlignment.Near, StringAlignment.Near, font_small_bold);
			height	= (int)Math.Ceiling(size.Height);
			if(height < header_line_height) height = header_line_height;
			PrintTextBox(graph, "Адрес:", header_x, y, header_title_width, height, text_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, header_data.txt_address, header_x + header_title_width, y, header_data_width, height, text_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			y += height;
			// Телефон
			PrintTextBox(graph, "Телефон:", header_x, y, header_title_width, header_line_height, text_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextBox(graph, header_data.txt_phone, header_x + header_title_width, y, header_data_width, header_line_height, text_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			y += header_line_height;
			// Доверенное лицо
			PrintTextNoBox(graph, "Доверенное лицо:", header_x, y, header_title_width, header_line_height, text_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			PrintTextNoBox(graph, header_data.txt_represent, header_x + header_title_width, y, header_data_width, header_line_height, text_offset, StringAlignment.Near, StringAlignment.Center, font_small_bold, brush_standart, pen_thin, false);
			y += header_line_height;
			// Внешний закругленный прямоугольник и вертикальное дополнение
			RectangleF rect1	= new RectangleF(header_x, offset + 10, header_data_width + header_title_width, y - (offset + 10));
			PrintRoundBox(graph, rect1, pen_thin, 4);
			PrintLineVrt(graph, header_x + header_title_width, offset + 10, y - (offset + 10), pen_thin, false);


			header_height_real = y;
			// ЛОГОТИП ОФИЦИАЛЬНОГО ДИЛЕРА (ДЛЯ ПРОФИЛЬНЫХ АВТОМОБИЛЕЙ)
			string file_name	= "";
			bool official		= false;
			switch(header_data.dialer)
			{
				case HeaderData.DIALER.kia:
					file_name	= "logo_kia.bmp";
					official	= true;
					break;
				case HeaderData.DIALER.lada:
					file_name	= "logo_lada.bmp";
					official	= true;
					break;
				case HeaderData.DIALER.chevrolet:
					file_name	= "logo_chevrolet.bmp";
					official	= true;
					break;
				case HeaderData.DIALER.unknown:
					file_name	= "logo_avto.bmp";
					break;
				default:
					file_name	= "logo_avto.bmp";
					break;
			}
			int image_x = header_title_width + header_data_width + header_x + 5;
			image_x = image_x + (200 - image_x) / 2;
			image_x = image_x - 39 / 2;
			PrintImage(graph, file_name, image_x, offset, 39, 20, false);
			if(official)
				PrintText(graph, "ОФИЦИАЛЬНЫЙ ДИЛЕР", image_x, offset + 20, 39, 4, StringAlignment.Center, StringAlignment.Center, font_small_bold, brush_standart, false);
			// БЛОК РЕКВИЗИТОВ ТЕХНИЧЕСКОГО ЦЕНТРА
			int radius					= 3;
			int addr_block_x			= header_title_width + header_data_width + header_x + 5;
			int addr_block_y			= offset + 20 + 4;
			int addr_block_width		= 200 - addr_block_x;
			int addr_block_height		= header_height_real - addr_block_y;
			int addr_block_line_height	= 4;
			RectangleF rect = new RectangleF(addr_block_x, addr_block_y , addr_block_width, addr_block_height);
			PrintRoundBox(graph, rect, pen_thin, radius);

			// Зачитываем данные из настроечного файла
			txt = FileIni.GetParameter("print.ini", "#ADDRESS_BLOCK_KIA");
			this.PrintTextNoBox(graph, txt, rect, 1, StringAlignment.Center, StringAlignment.Center, font_small, brush_standart, false);
			y += addr_block_height;

			//		y = addr_block_y + (addr_block_height - addr_block_line_height * 5) / 2;
			//		PrintText(graph, "Сервисный центр \"Авто-1\"", addr_block_x + radius, y, addr_block_width - radius * 2, addr_block_line_height, StringAlignment.Center, StringAlignment.Center, font_small, brush_standart, false);
			//		y += addr_block_line_height;
			//		PrintText(graph, "Тел. (383) 332-02-92 Факс (383) 333-87-08", addr_block_x + radius, y, addr_block_width - radius * 2, addr_block_line_height, StringAlignment.Center, StringAlignment.Center, font_small, brush_standart, false);
			//		y += addr_block_line_height;
			//		PrintText(graph, "630058, г.Новосибирск", addr_block_x + radius, y, addr_block_width - radius * 2, addr_block_line_height, StringAlignment.Center, StringAlignment.Center, font_small, brush_standart, false);
			//		y += addr_block_line_height;
			//		PrintText(graph, "Академгородок, ул. Русская, 48", addr_block_x + radius, y, addr_block_width - radius * 2, addr_block_line_height, StringAlignment.Center, StringAlignment.Center, font_small, brush_standart, false);
			//		y += addr_block_line_height;
			//		PrintText(graph, "www.avto-1.ru", addr_block_x + radius, y, addr_block_width - radius * 2, addr_block_line_height, StringAlignment.Center, StringAlignment.Center, font_small, brush_standart, false);


			return header_height_real + 3;
		}
		#endregion

		#region Печать завершающего блока
		private int PrintFooter(Graphics graph, int offset,  bool test, bool print, object o)
		{
			// Вспомогательные инструменты
			RectangleF rect;
			int y		= offset;
			string text	= "";
			string txt	= "";
			SizeF size;
			// Настроечные параметры
			int offset_x_left			= 10;
			int offset_x_right			= 10;
			int page_width				= 210;
			int title_height			= 4;
			int page_height				= 297 - 10;
			int recomendations_height	= 0;

			int	rowheight	= 4;
			int w1			= 190;
			int w2			= 46;
			int w3			= 15;
			int w4			= 25;
			int w5			= 15;
			int w6			= 105;
			int w7			= 37;
			int w8			= 15;
			int w9			= 25;

			y += 3;

			txt = "На запасные части использованные при гарантийном ремонте Автомобиля распространяется гарантия завода-изготовителя, которая истекает одновременно с окончанием гарантийного срока на Автомобиль. Сроки и условия гарантии на Автомобиль прописаны в гарантийном талоне.";
			size = this.MeasureText(graph, txt, w1 - 4, StringAlignment.Center, StringAlignment.Center, font_small);

			if(test == true || print == false)
			{
				y += 2;
				y += rowheight;
				y += rowheight;
				y += rowheight;
				y += rowheight;
				y += rowheight;
				y += 1;
				y += (int)Math.Ceiling(size.Height);
				y += 1;
				y += rowheight * 2;
				y += rowheight;
				y += rowheight;
				y += rowheight * 2;
				y += rowheight;

				y += 10;
				return y;
			}

			// Согласование сроков ремонта
			rect = new RectangleF(offset_x_left, y, w1, rowheight * 5 + 2);
			y += 2;
			txt = "Срок ремонта продлен на _________ (кол-во дней) по причине ___________________________________";
			PrintText(graph, txt, offset_x_left, y, w1, rowheight, StringAlignment.Near, StringAlignment.Center, font_middle, brush_standart, false);
			y += rowheight;
			PrintText(graph, "Максимальный срок ремонта составляет 45 дней", offset_x_left, y, w1, rowheight, StringAlignment.Near, StringAlignment.Center, font_middle, brush_standart, false);
			y += rowheight * 2;
			PrintText(graph, "Владелец/Представитель", offset_x_left, y, w2, rowheight, StringAlignment.Near, StringAlignment.Center, font_middle, brush_standart, false);
			PrintText(graph, "Сервис-Консультант", offset_x_left + w6, y, w7, rowheight, StringAlignment.Near, StringAlignment.Center, font_middle, brush_standart, false);
			y += rowheight;
			PrintLineHor(graph, offset_x_left + w2, y, w3 + w4 + w5, pen_thin, false);
			PrintText(graph, "Подпись", offset_x_left + w2, y, w3, rowheight, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
			PrintText(graph, "Расшифровка", offset_x_left + w2 + w3, y, w4, rowheight, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
			PrintText(graph, "Дата", offset_x_left + w2 + w3 + w4, y, w5, rowheight, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
			PrintLineHor(graph, offset_x_left + w6 + w7, y, w8 + w9, pen_thin, false);
			PrintText(graph, "Подпись", offset_x_left + w6 + w7, y, w8, rowheight, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
			PrintText(graph, "Расшифровка", offset_x_left + w6 + w7 + w8, y, w9, rowheight, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
			PrintRoundBox(graph, rect, pen_thin, 2);
			y += rowheight;
			y += 1;
			txt = "На запасные части использованные при гарантийном ремонте Автомобиля распространяется гарантия завода-изготовителя, которая истекает одновременно с окончанием гарантийного срока на Автомобиль. Сроки и условия гарантии на Автомобиль прописаны в гарантийном талоне.";
			rect = new RectangleF(offset_x_left, y, w1, (int)Math.Ceiling(size.Height));
			PrintTextNoBox(graph, txt, rect.X, rect.Y, rect.Width, rect.Height, 2, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, pen_thin, false);
			PrintRoundBox(graph, rect, pen_thin, 2);
			y += (int)Math.Ceiling(size.Height);
			y += 1;
			txt = "Автомобиль передан Владельцу/Представителю, объем и качество выполненных работ проверено.";
			PrintText(graph, txt, offset_x_left, y, w1, rowheight, StringAlignment.Near, StringAlignment.Center, font_middle, brush_standart, false);
			y += rowheight * 2;
			PrintText(graph, "Сервис-Консультант", offset_x_left, y, w7, rowheight, StringAlignment.Near, StringAlignment.Center, font_middle, brush_standart, false);
			PrintText(graph, "Инженер по гарантии", offset_x_left + w6 - 10, y, w2, rowheight, StringAlignment.Near, StringAlignment.Center, font_middle, brush_standart, false);
			y += rowheight;
			PrintLineHor(graph, offset_x_left + w7, y, w3 + w4, pen_thin, false);
			PrintText(graph, "Подпись", offset_x_left + w7, y, w3, rowheight, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
			PrintText(graph, "Расшифровка", offset_x_left + w7 + w3, y, w4, rowheight, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
			PrintLineHor(graph, offset_x_left + w6 - 10 + w7, y, w3 + w4, pen_thin, false);
			PrintText(graph, "Подпись", offset_x_left + w6 - 10 + w7, y, w3, rowheight, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
			PrintText(graph, "Расшифровка", offset_x_left + w6 - 10 + w7 + w3, y, w4, rowheight, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
			y += rowheight;
			txt = "Автомобиль после ремонта получил в технически исправном состоянии, претензий не имею.";
			PrintText(graph, txt, offset_x_left, y, w1, rowheight, StringAlignment.Near, StringAlignment.Center, font_middle, brush_standart, false);
			y += rowheight * 2;
			PrintText(graph, "Владелец/Представитель", offset_x_left, y, w2, rowheight, StringAlignment.Near, StringAlignment.Center, font_middle, brush_standart, false);
			y += rowheight;
			PrintLineHor(graph, offset_x_left + w2, y, w3 + w4 + w5, pen_thin, false);
			PrintText(graph, "Подпись", offset_x_left + w2, y, w3, rowheight, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
			PrintText(graph, "Расшифровка", offset_x_left + w2 + w3, y, w4, rowheight, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);
			PrintText(graph, "Дата", offset_x_left + w2 + w3 + w4, y, w5, rowheight, StringAlignment.Center, StringAlignment.Near, font_small, brush_standart, false);

			y += 10;
			return y;
		}
		#endregion

		public override void  PrintPage(Graphics graph, int page)
		{
			// Для ориентации на странице
			int offset = 0;

			offset = page_min_y;
			offset = PrintBlock(graph, offset, new DelegatePrintBlock(PrintHead), null);
			offset = PrintFooter(graph, offset, new DelegatePrintBlock(PrintFooter), null);
		}
	}
}
