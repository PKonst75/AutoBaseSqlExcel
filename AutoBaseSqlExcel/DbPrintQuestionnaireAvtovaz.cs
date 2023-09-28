using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DbPrintQuestionnaireAvtovaz.
	/// </summary>
	public class DbPrintQuestionnaireAvtovaz:DbPrint
	{
		// Инструменты для печати
		SolidBrush	brush_standart;
		SolidBrush	brush_lightgray;
		Font		font_small_bold;
		Font		font_middle_bold;
		Font		font_middle;
		Font		font_small;
		Pen			pen_thin;

		private DtCard		card;

		#region Данные для печати
		protected class HeaderData
		{
			public enum DIALER:long{unknown=0, chevrolet=1, lada=2, kia=3}
			public string txt_model		= "";
			public string txt_sign		= "";
			public string txt_vin		= "";
			public string txt_owner		= "";
			public string txt_address	= "";
			public string txt_phone		= "";
			public string txt_represent	= "";
			public string txt_sell		= "";
			public string txt_run		= "";
			public string txt_card		= "";
			public string txt_warrant	= "";
			public string txt_warrant_close		= "";
			public string txt_summ_work			= "";
			public string txt_summ_work_pay		= "";
			public string txt_discount_work		= "";
			public string txt_summ_detail		= "";
			public string txt_summ_detail_pay	= "";
			public string txt_summ_pay			= "";
			public string txt_recomendations	= "";
			public DIALER dialer		= DIALER.unknown;
	
			public ArrayList	works			= null;
			public ArrayList	details			= null;
			public ArrayList	recomendations	= null;
			public bool			cashless		= false;
			public bool			inner			= false;

			public float	count_nv	= 0.0F;
			public int		count_sp	= 0;

			public HeaderData(DtCard card)
			{
				// Данные карточки
				txt_card = card.GetData("НОМЕР_КАРТОЧКА").ToString() + "/" + card.GetData("ГОД_КАРТОЧКА").ToString();
				if((long)card.GetData("НОМЕР_НАРЯД_КАРТОЧКА") != 0)
				{
					//txt_warrant = card.GetData("НОМЕР_НАРЯД_КАРТОЧКА").ToString() + " от " + card.GetData("ДАТА_НАРЯД_ОТКРЫТ_КАРТОЧКА").ToString();
					txt_warrant = "       от " + card.GetData("ДАТА_НАРЯД_ОТКРЫТ_КАРТОЧКА").ToString();
				}
				if((short)card.GetData("СТАТУС_КАРТОЧКА") == 2)
				{
					txt_warrant_close = card.GetData("ДАТА_НАРЯД_ЗАКРЫТ_КАРТОЧКА").ToString();
				}
				if((bool)card.GetData("БЕЗНАЛИЧНЫЙ_КАРТОЧКА") == true)
				{
					cashless = true;
				}
				if((bool)card.GetData("ВНУТРЕННИЙ_КАРТОЧКА") == true)
				{
					inner = true;
				}
				int run = (int)card.GetData("ПРОБЕГ_КАРТОЧКА");
				if(run != 0)
				{
					// Пробег пустое поле
					txt_run = "";
					//txt_run		= run.ToString();
				}
				// Загрузка отображаемых данных заголовка
				DtAuto auto = DbSqlAuto.Find((long)card.CodeAuto/*GetData("АВТОМОБИЛЬ_КАРТОЧКА")*/);
				if(auto != null)
				{
					txt_sign	= (string)auto.GetData("НОМЕР_ЗНАК");
					txt_vin		= (string)auto.GetData("VIN");
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

				// Список работ в заказ-наряде
				works = new ArrayList();
				DbSqlCardWork.SelectInArray(card, works);
				float summ_work = 0.0F;
				foreach(DtCardWork element in works)
				{
					if((bool)element.GetData("ГАРАНТИЯ_КАРТОЧКА_РАБОТА") == false)
					{
						float nv = (float)element.GetData("ТРУДОЕМКОСТЬ_КАРТОЧКА_РАБОТА");
						if(nv != 0.0F)
						{
							summ_work += nv * (float)element.GetData("НОРМАЧАС_КАРТОЧКА_РАБОТА") * (float)element.GetData("КОЛИЧЕСТВО_КАРТОЧКА_РАБОТА");
						}
						else
						{
							summ_work += (float)element.GetData("НОРМАЧАС_КАРТОЧКА_РАБОТА") * (float)element.GetData("КОЛИЧЕСТВО_КАРТОЧКА_РАБОТА");
						}
					}
				}

				// Список деталей в заказ-наряде
				details = new ArrayList();
				DbSqlCardDetail.SelectInArray(card, details);
				float summ_oil = 0.0F;
				float summ_detail = 0.0F;
				foreach(DtCardDetail element in details)
				{
					if((bool)element.GetData("ЖИДКОСТЬ_КАРТОЧКА_ДЕТАЛЬ") == true)
					{
						if((bool)element.GetData("ГАРАНТИЯ_КАРТОЧКА_ДЕТАЛЬ") == false)
						{
							summ_oil += (float)element.GetData("КОЛИЧЕСТВО_КАРТОЧКА_ДЕТАЛЬ") * (float)element.GetData("ЦЕНА_КАРТОЧКА_ДЕТАЛЬ");
						}
					}
					else
					{
						if((bool)element.GetData("ГАРАНТИЯ_КАРТОЧКА_ДЕТАЛЬ") == false)
						{
							summ_detail += (float)element.GetData("КОЛИЧЕСТВО_КАРТОЧКА_ДЕТАЛЬ") * (float)element.GetData("ЦЕНА_КАРТОЧКА_ДЕТАЛЬ");
						}
					}
				}

				// Список рекомендаций
				recomendations = new ArrayList();
				DbSqlCardRecomendation.SelectInArray(recomendations, (long)card.GetData("НОМЕР_КАРТОЧКА"), (int)card.GetData("ГОД_КАРТОЧКА"));
				foreach(object o in recomendations)
				{
					DtCardRecomendation recomendation = (DtCardRecomendation)o;
					string s = (string)recomendation.GetData("РЕКОМЕНДАЦИЯ");
					txt_recomendations += s;
					txt_recomendations += "; ";
				}

				// Данные по сумме работ, с учетом скидки
				float discount_work		= 0.0F;
				float summ_work_pay		= 0.0F;
				float summ_work_oil		= 0.0F;
				float discount_val		= 0.0F;
				discount_val	= (float)card.GetData("СКИДКА_РАБОТА_КАРТОЧКА");
				if(discount_val != 0.0F)
				{
					discount_work = (float)Math.Round((float)(summ_work / 100 * discount_val), 0);
				}
				summ_work_pay		= summ_oil + summ_work - discount_work;
				summ_work_oil		= summ_oil + summ_work;
				txt_summ_work		= Db.CachToTxt(Math.Round(summ_work_oil, 2));
				txt_discount_work	= Db.CachToTxt(Math.Round(discount_work, 2));
				txt_summ_work_pay	= Db.CachToTxt(Math.Round(summ_work_pay, 2));
				txt_summ_detail		= Db.CachToTxt(Math.Round(summ_detail, 2));
				txt_summ_detail_pay	= Db.CachToTxt(Math.Round(summ_detail, 2));
				txt_summ_pay		= Db.CachToTxt(Math.Round(summ_detail + summ_work_pay, 2));
			}
		}
		HeaderData	header_data = null;
		#endregion

		public DbPrintQuestionnaireAvtovaz(long card_number, int card_year)
		{
			// Подготовка инструментов для печати
			brush_standart		= new SolidBrush(Color.Black);
			brush_lightgray		= new SolidBrush(Color.LightGray);
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
			if(test == true || print == false)
			{
				// Просто рассчитываем высоту
				y = offset;
				y += 6;
				y += 6;
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
			if(header_data.txt_warrant != "")
			{
				// Пустое место под заказ-нарядом
				//PrintText(graph, "ЗАКАЗ-НАРЯД" + header_data.txt_warrant, header_x, y, 150, 5, StringAlignment.Near, StringAlignment.Center, font_middle_bold, brush_standart, false);
			}
			else
			{
				//PrintText(graph, "ЗАКАЗ-НАРЯД (НЕ ОТКРЫТ)" + header_data.txt_warrant, header_x, y, 150, 5, StringAlignment.Near, StringAlignment.Center, font_middle_bold, brush_standart, false);
			}
			y += 5;
			if(header_data.txt_warrant_close != "")
			{
				//PrintText(graph, "ЗАКРЫТ : " + header_data.txt_warrant_close, header_x, y, 150, 5, StringAlignment.Near, StringAlignment.Center, font_middle_bold, brush_standart, false);
			}
			else
			{
				//PrintText(graph, "НЕ ЗАКРЫТ", header_x, y, 150, 5, StringAlignment.Near, StringAlignment.Center, font_middle_bold, brush_standart, false);
			}
			y += 5;
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
			txt = FileIni.GetParameter("print.ini", "#ADDRESS_BLOCK");
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
			SizeF size;
			int y		= offset;
			string txt	= "";
		
			// Настроечные параметры
			int offset_x_left			= 10;
			int offset_x_right			= 10;
			int page_width				= 210;

			int	rowheight	= 4;
			int w1			= 90;
			int w2			= 20;
			int w3			= 5;
			
			y += 3;

			if(test == true || print == false)
			{	
				txt = "Ваши впечатления связанные с сервисным обслуживанием автомобиля";
				size = this.MeasureText(graph, txt, w1, StringAlignment.Center, StringAlignment.Near, font_small_bold);
				y += (int)Math.Round(size.Height) + 1;

				txt = "1.Как Вы оцениваете понимание, доброжелательностьи внимательность персонала сервисной станции в решении ваших проблем, насколько бережно и аккуратно относился персонал станции к Вашему автомобилю во время приемки и обслуживания.";
				size = this.MeasureText(graph, txt, w1, StringAlignment.Near, StringAlignment.Near, font_small);
				y += (int)Math.Round(size.Height) + 1;

				txt = "2.Как Вы оцениваете чистоту и порядок в производственной зоне сервисного обслуживания, а так же опрятность одежды персонала сервисной станции.";
				size = this.MeasureText(graph, txt, w1, StringAlignment.Near, StringAlignment.Near, font_small);
				y += (int)Math.Round(size.Height) + 1;

				txt = "3.Как Вы оцениваете чистоту условия приемки автомобиля, предварительную запись, соблюдение сроков ремонта и предъявления автомобиля в процессе обслуживания.";
				size = this.MeasureText(graph, txt, w1, StringAlignment.Near, StringAlignment.Near, font_small);
				y += (int)Math.Round(size.Height) + 1;

				txt = "4.Отказывали ли Вам в ремонте автомобилей из-за отсутствия на сервисной станции запасных частей.";
				size = this.MeasureText(graph, txt, w1, StringAlignment.Near, StringAlignment.Near, font_small);
				y += (int)Math.Round(size.Height) + 1;

				txt = "5.Как Вы оцениваете удобства в зоне ожидания и отдыха клиентов во время ожидания приемки и обслуживания Вашего автомобиля.";
				size = this.MeasureText(graph, txt, w1, StringAlignment.Near, StringAlignment.Near, font_small);
				y += (int)Math.Round(size.Height) + 1;

				txt = "6.Как Вы оцениваете доступность и полноту информации о предоставляемых услугах по обслуживанию автомобиля.";
				size = this.MeasureText(graph, txt, w1, StringAlignment.Near, StringAlignment.Near, font_small);
				y += (int)Math.Round(size.Height) + 1;

				txt = "7.Как Вы оцениваете полноту и качество выполненных работ по результатам проверки и выдачи автомобиля.";
				size = this.MeasureText(graph, txt, w1, StringAlignment.Near, StringAlignment.Near, font_small);
				y += (int)Math.Round(size.Height) + 1;

				txt = "8.Ваши впечатления вцелом. С каким настроением Вы расстаетесь со станцией.";
				size = this.MeasureText(graph, txt, w1, StringAlignment.Near, StringAlignment.Near, font_small);
				y += (int)Math.Round(size.Height) + 1;

				return y;
			}
			
			// Первая строчка
			txt = "Ваши впечатления связанные с сервисным обслуживанием автомобиля";
			size = this.MeasureText(graph, txt, w1, StringAlignment.Center, StringAlignment.Near, font_small_bold);
			PrintText(graph, txt, offset_x_left, y, w1, (int)Math.Round(size.Height) + 1, StringAlignment.Center, StringAlignment.Near, font_small_bold, brush_standart, false);
			PrintText(graph, "Отлично", offset_x_left + w1, y, w2, (int)Math.Round(size.Height) + 1, StringAlignment.Center, StringAlignment.Near, font_small_bold, brush_standart, false);
			PrintText(graph, "Хорошо", offset_x_left + w1 + w2, y, w2, (int)Math.Round(size.Height) + 1, StringAlignment.Center, StringAlignment.Near, font_small_bold, brush_standart, false);
			PrintText(graph, "Удовлетворительно", offset_x_left + w1 + w2 + w2, y, w2, (int)Math.Round(size.Height) + 1, StringAlignment.Center, StringAlignment.Near, font_small_bold, brush_standart, false);
			PrintText(graph, "Неудовлетворительно", offset_x_left + w1 + w2 + w2 + w2, y, w2, (int)Math.Round(size.Height) + 1, StringAlignment.Center, StringAlignment.Near, font_small_bold, brush_standart, false);
			y += (int)Math.Round(size.Height) + 1;

			txt = "1.Как вы оцениваете понимание, доброжелательностьи внимательность персонала сервисной станции в решении ваших проблем, насколько бережно и аккуратно относился персонал станции к Вашему автомобилю во время приемки и обслуживания.";
			size = this.MeasureText(graph, txt, w1, StringAlignment.Near, StringAlignment.Near, font_small);
			PrintText(graph, txt, offset_x_left, y, w1, (int)Math.Round(size.Height) + 1, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			rect = new RectangleF(offset_x_left + w1 + (w2 - w3) / 2, y, w3, w3);
			PrintBox(graph, rect, pen_thin);
			rect.X += w2;
			PrintBox(graph, rect, pen_thin);
			rect.X += w2;
			PrintBox(graph, rect, pen_thin);
			rect.X += w2;
			PrintBox(graph, rect, pen_thin);
			y += (int)Math.Round(size.Height) + 1;

			txt = "2.Как вы оцениваете чистоту и порядок в производственной зоне сервисного обслуживания, а так же опрятность одежды персонала сервисной станции.";
			size = this.MeasureText(graph, txt, w1, StringAlignment.Near, StringAlignment.Near, font_small);
			PrintText(graph, txt, offset_x_left, y, w1, (int)Math.Round(size.Height) + 1, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			rect = new RectangleF(offset_x_left + w1 + (w2 - w3) / 2, y, w3, w3);
			PrintBox(graph, rect, pen_thin);
			rect.X += w2;
			PrintBox(graph, rect, pen_thin);
			rect.X += w2;
			PrintBox(graph, rect, pen_thin);
			rect.X += w2;
			PrintBox(graph, rect, pen_thin);
			y += (int)Math.Round(size.Height) + 1;

			txt = "3.Как вы оцениваете чистоту условия приемки автомобиля, предварительную запись, соблюдение сроков ремонта и предъявления автомобиля в процессе обслуживания.";
			size = this.MeasureText(graph, txt, w1, StringAlignment.Near, StringAlignment.Near, font_small);
			PrintText(graph, txt, offset_x_left, y, w1, (int)Math.Round(size.Height) + 1, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			rect = new RectangleF(offset_x_left + w1 + (w2 - w3) / 2, y, w3, w3);
			PrintBox(graph, rect, pen_thin);
			rect.X += w2;
			PrintBox(graph, rect, pen_thin);
			rect.X += w2;
			PrintBox(graph, rect, pen_thin);
			rect.X += w2;
			PrintBox(graph, rect, pen_thin);
			y += (int)Math.Round(size.Height) + 1;

			txt = "4.Отказывали ли Вам в ремонте автомобилей из-за отсутствия на сервисной станции запасных частей.";
			size = this.MeasureText(graph, txt, w1, StringAlignment.Near, StringAlignment.Near, font_small);
			PrintText(graph, txt, offset_x_left, y, w1, (int)Math.Round(size.Height) + 1, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			rect = new RectangleF(offset_x_left + w1 + (w2 - w3) / 2, y, w3, w3);
			this.PrintRectSigned(graph, "Да", rect, 1, 10, font_small, brush_standart, pen_thin, false);
			rect.X += w2;
			this.PrintRectSigned(graph, "Нет", rect, 1, 10, font_small, brush_standart, pen_thin, false);
			y += (int)Math.Round(size.Height) + 1;

			txt = "5.Как Вы оцениваете удобства в зоне ожидания и отдыха клиентов во время ожидания приемки и обслуживания Вашего автомобиля.";
			size = this.MeasureText(graph, txt, w1, StringAlignment.Near, StringAlignment.Near, font_small);
			PrintText(graph, txt, offset_x_left, y, w1, (int)Math.Round(size.Height) + 1, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			rect = new RectangleF(offset_x_left + w1 + (w2 - w3) / 2, y, w3, w3);
			PrintBox(graph, rect, pen_thin);
			rect.X += w2;
			PrintBox(graph, rect, pen_thin);
			rect.X += w2;
			PrintBox(graph, rect, pen_thin);
			rect.X += w2;
			PrintBox(graph, rect, pen_thin);
			y += (int)Math.Round(size.Height) + 1;

			txt = "6.Как Вы оцениваете доступность и полноту информации о предоставляемых услугах по обслуживанию автомобиля.";
			size = this.MeasureText(graph, txt, w1, StringAlignment.Near, StringAlignment.Near, font_small);
			PrintText(graph, txt, offset_x_left, y, w1, (int)Math.Round(size.Height) + 1, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			rect = new RectangleF(offset_x_left + w1 + (w2 - w3) / 2, y, w3, w3);
			PrintBox(graph, rect, pen_thin);
			rect.X += w2;
			PrintBox(graph, rect, pen_thin);
			rect.X += w2;
			PrintBox(graph, rect, pen_thin);
			rect.X += w2;
			PrintBox(graph, rect, pen_thin);
			y += (int)Math.Round(size.Height) + 1;

			txt = "7.Как Вы оцениваете полноту и качество выполненных работ по результатам проверки и выдачи автомобиля.";
			size = this.MeasureText(graph, txt, w1, StringAlignment.Near, StringAlignment.Near, font_small);
			PrintText(graph, txt, offset_x_left, y, w1, (int)Math.Round(size.Height) + 1, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			rect = new RectangleF(offset_x_left + w1 + (w2 - w3) / 2, y, w3, w3);
			PrintBox(graph, rect, pen_thin);
			rect.X += w2;
			PrintBox(graph, rect, pen_thin);
			rect.X += w2;
			PrintBox(graph, rect, pen_thin);
			rect.X += w2;
			PrintBox(graph, rect, pen_thin);
			y += (int)Math.Round(size.Height) + 1;

			txt = "8.Ваши впечатления вцелом. С каким настроением Вы расстаетесь со станцией.";
			size = this.MeasureText(graph, txt, w1, StringAlignment.Near, StringAlignment.Near, font_small);
			PrintText(graph, txt, offset_x_left, y, w1, (int)Math.Round(size.Height) + 1, StringAlignment.Near, StringAlignment.Near, font_small, brush_standart, false);
			rect = new RectangleF(offset_x_left + w1 + (w2 - w3) / 2, y, w3, w3);
			PrintBox(graph, rect, pen_thin);
			rect.X += w2;
			PrintBox(graph, rect, pen_thin);
			rect.X += w2;
			PrintBox(graph, rect, pen_thin);
			rect.X += w2;
			PrintBox(graph, rect, pen_thin);
			y += (int)Math.Round(size.Height) + 1;

			return y;
		}
		#endregion

		// Основная процедура печати
		public override void  PrintPage(Graphics graph, int page)
		{
			// Для ориентации на странице
			int offset = 0;

			offset = 10;
			offset = PrintBlock(graph, offset, new DelegatePrintBlock(PrintHead), null);
			offset = PrintBlock(graph, offset, new DelegatePrintBlock(PrintFooter), null);
		}
	}
}
