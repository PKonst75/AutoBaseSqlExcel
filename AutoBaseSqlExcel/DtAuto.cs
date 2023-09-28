using System;
using System.Windows.Forms;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Описание автомобиля.
	/// </summary>
	public class DtAuto : Dt
	{

		// Текстовое отображение полей автомобиля
		public class DtAutoTxt
		{
			// Прямые
			public readonly string vin;
			public readonly string year;
			public readonly string licence_plate;
			public readonly string sell_date;
			// Получаемые
			public readonly string model;
			public DtAutoTxt(DtAuto auto)
			{
				if (auto == null) return;
				// Прямые
				if (auto.is_id_vin == false)
					vin = auto.id_vin;
				else
					vin = "ОТСУТСВУЕТ";
				year = auto.year.ToString();
				if (auto.licence_plate_number != "")
					licence_plate = auto.licence_plate_number + " " + auto.licence_plate_region;
				else
					licence_plate = auto.id_sign;

				if (auto.ext_is_sell_date)
					sell_date = auto.ext_sell_date.ToShortDateString();
				else
					sell_date = "-";
				// Получаемые
				DtModel m = DbSqlModel.Find(auto.code_model);
				if (m == null) model = "NO MODEL";
				else model = m.Txt();
			}
		}

		DtLicensePlate _licensePlate = null;
		DtModel _tmpModel = null;

		long code;          // Уникальный код автомобиля - БД Индекс
							// Идинтификационные данные
		private long code_model;        // Код модели автомобиля
		private string id_vin;          // VIN - уникальный VIN автомобиля
		private bool is_id_vin;     // Есть ли VIN у автомобиля
		private string id_vin_origin;   // Исходный VIN производителя автомобиля
		private string id_body;     // Номер кузова автомобиля
		private string id_frame;        // Номер шасси автомобиля
		private string id_engine;       // Номер двигателя автомобиля
		private long id_parts;      // Номер запчастей автомобиля, если есть
		private bool is_id_parts;   // Есть ли номер запчастей автомобиля


		// Дополнительные данные
		int year;           // Год выпуска
		long code_producer; // Код производителя автомобиля
		long code_color;        // Код цвета автомобиля
		long code_variant;  // Код исполнения автомобиля
							// Регистрационные данные
		string id_sign;     // Регистрационный знак автомобиля
							// Дополнительные
		string comment;             // Примечание, дополнительные данные
		string licence_plate_number;    // Номер гос-знака
		string licence_plate_region;    // Регион знака
		bool is_pts;                    // Есть ли ПТС
		float color_price;          // Доплата за цвет

		string tmp_model_name;      // Наименование модели
		string tmp_factory_name;    // Наименование производителя автомобиля
		string tmp_color_name;      // Наименование цвета
		string tmp_variant_name;    // Наименование исполнения


		string ext_receive_comment; // Расширение для получения, примечание
		DateTime ext_receive_date;      // Расширение, дата получения
		bool ext_is_receive_date;   // Расширение, есть ли дата получения
		bool ext_is_sell_date;      // Расширение, есть ли дата продажи
		DateTime ext_sell_date;         // Расширение, дата продажи
		string ext_sell_customer;       // Расширение, покупатель
		string ext_sell_comment;        // Расширение, продажа примечание

		string ext_reserve_info;        // Примечание к резервированию
		float ext_summ_add;         // Сумма допов
		float ext_price;              // Цена по прайс-листу

		// Расширенне данные
		private DtBrand _autoBrand; // Бренд автомобиля


		public DtAuto()
		{
			code = 0;
			code_model = 0;
			id_vin = "";
			is_id_vin = false;
			id_vin_origin = "";
			id_body = "";
			id_frame = "";
			id_engine = "";
			id_sign = "";
			year = 0;
			code_producer = 0;
			id_parts = 0;
			is_id_parts = false;
			code_color = 0;
			code_variant = 0;
			comment = "";
			is_pts = false;
			color_price = 0.0F;

			licence_plate_number = "";
			licence_plate_region = "";

			tmp_model_name = "";
			tmp_factory_name = "";

			ext_receive_comment = "";
			ext_receive_date = DateTime.Now;
			ext_is_receive_date = false;
			ext_sell_comment = "";
			ext_sell_customer = "";
			ext_sell_date = DateTime.Now;
			ext_is_sell_date = false;

			ext_reserve_info = "";
			ext_summ_add = 0.0F;
			ext_price = 0.0F;

			_autoBrand = null;
		}

		public DtBrand AutoBrand
		{
            get
            {
				if (_autoBrand != null) return _autoBrand;
				if (code_model == 0) return null;
				return _autoBrand = DbSqlBrand.FindModel(code_model);
			}
		}

		public DtLicensePlate LicensePlate
        {
            get
			{ 
				if(_licensePlate == null)
					_licensePlate = new DtLicensePlate(licence_plate_number, licence_plate_region);
				return _licensePlate;
			}
            set
            {
				licence_plate_number = value.Number;
				licence_plate_region = value.Region;
				_licensePlate = value;
            }
        }

		public DtModel AutoModel
        {
            get
            {
				if (code_model == 0) return null;
				if(_tmpModel == null)
					_tmpModel = DbSqlModel.Find(code_model);
				return _tmpModel;
            }
        }
		public object GetData(string data)
		{
			switch(data)
			{
				case "КОД_АВТОМОБИЛЬ":
					return (object)(long)code;
				// Идинтификационные данные
				case "ССЫЛКА_КОД_АВТОМОБИЛЬ_МОДЕЛЬ":
					return (object)(long)code_model;
				case "VIN":
					return (object)(string)id_vin;
				case "VIN_ОТСУТСТВУЕТ":
					return (object)(bool)is_id_vin;
				case "VIN_ПРОИЗВОДИТЕЛЬ":
					return (object)(string)id_vin_origin;
				case "НОМЕР_КУЗОВ":
					return (object)(string)id_body;
				case "НОМЕР_ШАССИ":
					return (object)(string)id_frame;
				case "НОМЕР_ДВИГАТЕЛЬ":
					return (object)(string)id_engine;
				case "НОМЕР_ЗАПЧАСТЕЙ_АВТОМОБИЛЬ":
					return (object)(long)id_parts;
				case "НОМЕР_ЗАПЧАСТЕЙ_ОТСУТСТВУЕТ_АВТОМОБИЛЬ":
					return (object)(bool)is_id_parts;
				// Описание автомобиля
				case "ГОД_ВЫПУСК":
					return (object)(int)year;
				case "КОД_ПРОИЗВОДИТЕЛЬ_АВТОМОБИЛЬ":
					return (object)(long)code_producer;
				case "ССЫЛКА_КОД_АВТОМОБИЛЬ_ЦВЕТ":
					return (object)(long)code_color;
				case "ССЫЛКА_КОД_АВТОМОБИЛЬ_ИСПОЛНЕНИЕ":
					return (object)(long)code_variant;
				// Регистрационные
				case "НОМЕР_ЗНАК":
					return (object)(string)id_sign;
				// Дополнительные
				case "ПРИМЕЧАНИЕ":
					return (object)(string)comment;
                case "ПТС":
                    return (object)(bool)is_pts;
                case "ДОПЛАТА_ЦВЕТ":
                    return (object)(float)color_price;
				// Регистрационные
                case "НОМЕР_ЗНАК_НОМЕР":
					return (object)(string)licence_plate_number;
				// Регистрационные
				case "НОМЕР_ЗНАК_РЕГИОН":
					return (object)(string)licence_plate_region;
				// Вспомагательные
				case "МОДЕЛЬ":
					return (object)(string)tmp_model_name;
				case "ПРОИЗВОДИТЕЛЬ":
					return (object)(string)tmp_factory_name;
				case "АВТОМОБИЛЬ_ЦВЕТ":
					return (object)(string)tmp_color_name;
				case "АВТОМОБИЛЬ_ИСПОЛНЕНИЕ":
					return (object)(string)tmp_variant_name;

				// Расширенные
				case "ПОЛУЧЕНИЕ_ПРИМЕЧАНИЕ":
					return (object)(string)ext_receive_comment;
				case "ПОЛУЧЕНИЕ_ДАТА":
					return (object)(DateTime)ext_receive_date;
				case "ЕСТЬ_ПОЛУЧЕНИЕ_ДАТА":
					return (object)(bool)ext_is_receive_date;
				case "ПРОДАЖА_ПРИМЕЧАНИЕ":
					return (object)(string)ext_sell_comment;
				case "ПРОДАЖА_ПОКУПАТЕЛЬ":
					return (object)(string)ext_sell_customer;
				case "ПРОДАЖА_ДАТА":
					return (object)(DateTime)ext_sell_date;
				case "ЕСТЬ_ПРОДАЖА_ДАТА":
					return (object)(bool)ext_is_sell_date;

				default:
					return (object)null;
			}
		}

		public void SetData(string data, object val)
		{
			switch(data)
			{
				case "КОД_АВТОМОБИЛЬ":
					code = (long)val;
					break;
				// Идинтификационные данные
				case "ССЫЛКА_КОД_АВТОМОБИЛЬ_МОДЕЛЬ":
					code_model = (long)val;
					break;
				case "VIN":
					id_vin = (string)val;
					break;
				case "VIN_ОТСУТСТВУЕТ":
					is_id_vin = (bool)val;
					break;
				case "VIN_ПРОИЗВОДИТЕЛЬ":
					id_vin_origin = (string)val;
					break;
				case "НОМЕР_КУЗОВ":
					id_body = (string)val;
					break;
				case "НОМЕР_ШАССИ":
					id_frame = (string)val;
					break;
				case "НОМЕР_ДВИГАТЕЛЬ":
					id_engine = (string)val;
					break;
				case "НОМЕР_ЗАПЧАСТЕЙ_АВТОМОБИЛЬ":
					id_parts = (long)val;
					break;
				case "НОМЕР_ЗАПЧАСТЕЙ_ОТСУТСТВУЕТ_АВТОМОБИЛЬ":
					is_id_parts = (bool)val;
					break;
				// Описание автомобиля
				case "ГОД_ВЫПУСК":
					year = (int)val;
					break;
				case "КОД_ПРОИЗВОДИТЕЛЬ_АВТОМОБИЛЬ":
					code_producer = (long)val;
					break;
				case "ССЫЛКА_КОД_АВТОМОБИЛЬ_ЦВЕТ":
					code_color = (long)val;
					break;
				case "ССЫЛКА_КОД_АВТОМОБИЛЬ_ИСПОЛНЕНИЕ":
					code_variant = (long)val;
					break;
				// Регистрационные
				case "НОМЕР_ЗНАК":
					id_sign = (string)val;
					break;
					// Регистрационные
				case "ПРИМЕЧАНИЕ":
					comment = (string)val;
					break;
                case "ПТС":
                    is_pts = (bool)val;
                    break;
                case "ДОПЛАТА_ЦВЕТ":
                    color_price = (float)val;
                    break;
					// Регистрационные
				case "НОМЕР_ЗНАК_НОМЕР":
					licence_plate_number = (string)val;
					break;
				case "НОМЕР_ЗНАК_РЕГИОН":
					licence_plate_region = (string)val;
					break;
				// Вспомагательные
				case "МОДЕЛЬ":
					tmp_model_name = (string)val;
					break;
				case "ПРОИЗВОДИТЕЛЬ":
					tmp_factory_name = (string)val;
					break;
				case "АВТОМОБИЛЬ_ЦВЕТ":
					tmp_color_name = (string)val;
					break;
				case "АВТОМОБИЛЬ_ИСПОЛНЕНИЕ":
					tmp_variant_name = (string)val;
					break;
				// Расширенные
				case "ПОЛУЧЕНИЕ_ПРИМЕЧАНИЕ":
					ext_receive_comment = (string)val;
					break;
				case "ПОЛУЧЕНИЕ_ДАТА":
					ext_receive_date = (DateTime)val;
					break;
				case "ЕСТЬ_ПОЛУЧЕНИЕ_ДАТА":
					ext_is_receive_date = (bool)val;
					break;
				case "ПРОДАЖА_ПРИМЕЧАНИЕ":
					ext_sell_comment = (string)val;
					break;
				case "ПРОДАЖА_ПОКУПАТЕЛЬ":
					ext_sell_customer = (string)val;
					break;
				case "ПРОДАЖА_ДАТА":
					ext_sell_date = (DateTime)val;
					break;
				case "ЕСТЬ_ПРОДАЖА_ДАТА":
					ext_is_sell_date = (bool)val;
					break;
				case "РЕЗЕРВ":
					ext_reserve_info = (string)val;
					break;
				case "СУММА_ДОПОВ":
					ext_summ_add = (float)val;
					break;
				case "ЦЕНА_ПРАЙС":
					ext_price = (float)val;
					break;
				default:
					break;
			}
		}

		public long CodeModel
        {
            get { return code_model; }
        }
		public long CodeAutoType
        {
            get
            {
				DtModel dtAutoModel = DbSqlModel.Find(CodeModel);
				return dtAutoModel.CodeAutoType;
            }
        }
		public void SetLVItem(ListViewItem item)
		{
			item.SubItems.Clear();		// Чтобы сделать однотипным добавление и изменение

			item.Tag				= code;
			item.Text				= this.tmp_model_name;
			item.SubItems.Add(this.id_vin);
			item.SubItems.Add(this.id_body);
			item.SubItems.Add(this.tmp_factory_name);
			item.SubItems.Add(this.comment);
		}

		public void SetLVItemReceive(ListViewItem item)
		{
			item.SubItems.Clear();		// Чтобы сделать однотипным добавление и изменение

			item.Tag				= code;
			item.Text				= (item.Index + 1).ToString();
			item.SubItems.Add(this.tmp_model_name);
			item.SubItems.Add(this.tmp_variant_name);
			item.SubItems.Add(this.tmp_color_name);
			item.SubItems.Add(this.id_vin);
			item.SubItems.Add(this.ext_receive_comment);
		}

		public void SetLVItemStorageV1(ListViewItem item)
		{
			item.SubItems.Clear();		// Чтобы сделать однотипным добавление и изменение

			item.Tag	= code;
			item.Text	= this.tmp_model_name;
			item.SubItems.Add(this.tmp_variant_name);
			item.SubItems.Add(this.tmp_color_name);
			item.SubItems.Add(this.id_vin);
			item.SubItems.Add(this.id_body);
			if(this.ext_is_receive_date)
				item.SubItems.Add(this.ext_receive_date.ToShortDateString());
			else
				item.SubItems.Add("");
			item.SubItems.Add(this.ext_receive_comment);
			if(this.ext_is_sell_date)
				item.SubItems.Add(this.ext_sell_date.ToShortDateString());
			else
				item.SubItems.Add("");
			item.SubItems.Add(this.ext_sell_customer);
			item.SubItems.Add(this.ext_sell_comment);
		}

		public void SetLVItemStorageV2(ListViewItem item)
		{
			item.SubItems.Clear();		// Чтобы сделать однотипным добавление и изменение

			item.Tag	= code;
			if(this.ext_is_receive_date)
				item.Text = this.ext_receive_date.ToShortDateString();
			else
				item.Text = "Неопределено";
			item.SubItems.Add(this.tmp_model_name);
			item.SubItems.Add(this.tmp_variant_name);
			item.SubItems.Add(this.ext_reserve_info);
			item.SubItems.Add(this.tmp_color_name);
			item.SubItems.Add(this.id_vin);
			item.SubItems.Add(this.year.ToString());
			if(this.ext_price != 0.0F)
				item.SubItems.Add(Db.CachToTxt(this.ext_price));
			else
				item.SubItems.Add("");
            // НОВОЕ! Доплата за цвет
            if (this.color_price != 0.0F)
                item.SubItems.Add(Db.CachToTxt(this.color_price));
            else
                item.SubItems.Add("");

			string summ_add_txt = "";
			if(this.ext_summ_add == -1.0F)
				summ_add_txt = "ОШИБКА";
			if(this.ext_summ_add > 0.0F)
				summ_add_txt = ext_summ_add.ToString();
			item.SubItems.Add(summ_add_txt);

			string summ_whole_txt = "";
            if ((this.ext_price > 0.0F) && ((this.ext_summ_add > 0.0F) || (this.color_price > 0.0F)))
			{
				summ_whole_txt = (this.ext_price + this.ext_summ_add + this.color_price).ToString();
			}
			item.SubItems.Add(summ_whole_txt);

            string pts_txt = "";
            if (this.is_pts == true)
                pts_txt = "+";
            item.SubItems.Add(pts_txt);
		}

		public string Txt()
		{
			// Текстовое отображение элемента
			string txt;//		= "";
			DtModel model = DbSqlModel.Find(code_model);
			if(model == null) txt = "NO MODEL, ";
			else txt = model.Txt() + ", ";
			if(is_id_vin == false) txt += id_vin + " (" + id_body + ")";
			else txt += id_body;
			return txt;
		}

		public long Code
        {
            get { return code; }
        }
		public string TitleTxt
        {
            get
            {
				//string title = "";
				//title = tmp_model_name + " ";
				string title = tmp_model_name + " ";
				if (!is_id_vin) title += this.id_vin;
				else title +=  "VIN ОТСУТСВУЕТ";
				return title;
            }
        }

		public DateTime SellDate
        {
            get
            {
				if (ext_is_sell_date) return ext_sell_date;
				return new DateTime(1, 1, 1);
            }
        }
		public string SellDateTxt
		{
			get
			{
				if (SellDate.Year == 1) return "НЕТ";
				return SellDate.ToShortDateString();
			}
		}

		public static DateValuePair ReadDbLastRun(DtAuto auto)
        {
			DateValuePair pair = new DateValuePair(DateValuePair.OBJECTTYPE.OTYPE_INT);
			if (auto == null) return pair;
			DtCard card = DbSqlCard.FindCardAutoRun(auto);
			if (card == null) return pair;
			pair.SetInt(card.Date, card.CardRun);
			return pair;
        }

		public string VIN
        {
            get { return id_vin; }
			set
			{
				//string str = value;
				//str = str.Trim().ToUpper();
				//str = str.ToUpper();
				if (value == "") return;
				if (id_vin == value) return;
				id_vin = value;
				is_id_vin = true;
				IsChg = true;
			}
        }

		public string IdSign
		{
			get
			{
				return id_sign;
			}
		}

        #region Простые геттеры и сеттеры
		public int Year
        {
			get { return year; }
        }
		#endregion

		#region Аналитика характеристик автомобиля

		public bool IsBrandLada()
        {
			if (id_vin.StartsWith("XTA")) return true;
			if (id_vin.StartsWith("Z9Z")) return true;
			if (id_vin.StartsWith("XWK")) return true;
			if (id_vin.StartsWith("X9L")) return true;
			if (id_vin.StartsWith("X6D")) return true;
			return false;
		}

		#endregion
	}
}
