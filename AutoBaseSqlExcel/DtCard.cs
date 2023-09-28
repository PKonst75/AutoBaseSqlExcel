using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for DtCard.
	/// </summary>
	/// 
	
	public class DtCard : Dt
	{
		const float DISCOUNT_MAX_LIMIT_WITHOUT_PENALTY = 30.0F;

		// Система событий для отображения изменений
		public delegate void DisplayValueChange(object sender, string valueName = "");
		public event DisplayValueChange DisplayChanges;

		public enum PAY_TYPE : short { NONE = 0, CREDIT_CARD = 1, CASHLESS = 2, CASH = 3, NO_PAY = 4 };
		public enum NodeGroup : long { NONE = 0, ROOT = 1, GUARANTY_ROOT = 2, GUARANTY = 3, PAY_ROOT = 4, PAY = 5 };
		public enum NodeType : long { NONE = 0, WORK = 1, DETAIL = 2 };
		public enum CardState : short { NONE = 0, OPEND = 1, CLOSED = 2, REOPEND = 4, CANCELED = 5, STOPPED = 3, CLOSECANCEL = 10, MARKWORKEND = 20 };
		public struct Data
		{
			public NodeGroup group;
			public NodeType type;
			public object data;
		}
		public struct Pair
		{
			public long number;
			public int year;
		}

		// Прямые данные соответсвующие базе данных
		private long _code; // Код карточки
		private long _number; // Номер карточки
		private int _year; // Год карточки
		private DateTime _date; // Дата создания карточки
		private long _warrantNumber; // Номер заказ-наряда
		private int _run; // Пробег карточки
		private DateTime _agreedPickupTime;  // Согласованное время выдачи
		private DtCard.CardState _state; // Основной статус карточка
		private short _stateControl; // Статус контроля карточки
		private long _masterCode; // Тот кто закрыл заказ-наряд

		// Агрегированные ссылочные данные
		private DtLicensePlate _licensePlate; // Регистрационный знак автомобиля в карточке

		// Вспомогательные данные
		private bool _is_agreedPickupTime;   // Установленно ли согласованное время выдачи


		// Агрегированные связанные списки
		private DtCardActionCollection _cardActions; // Список действий карточки
		private DtCardClaimCollection _cardClaims; // Список заявок карточки
		private DtCardWorkCollection _cardWorks; // Список работ карточки
													 

		private long code_partner;
		private long code_represent;
		private long code_auto;
		private DtAuto _dtAuto; // Автомобиль в карточке
		private string represent_document;

		// Расширенные действия


		// Под удаление
		private string tmp_auto_plate;

		private DateTime warrant_date_open;
		private DateTime warrant_date_close;
		
		private bool cashless;
		private bool credit_card;
		private bool inner;
		private string comment;
		private long code_workshop;
		private long code_guaranty;
		private long code_work;     // Код типа трудоемкостей поумолчанию
		
		
		private float discount;
		private float discount_parts; //Скидка на запасные части

		private long code_licence_vehicle;
		private bool returned;
		private long service_manager_code;
		// Одобрения
		private long supervisor_guaranty;
		private long supervisor_payment;
		private long supervisor_whole;

		private short card_rate;
		private bool card_rate_change;

		private short card_call_rate;
		private bool card_call_work_done;

		

		private string tmp_owner_txt;       // Владелец автомобиля, текстовое представление
		private string tmp_auto_model;      // Модель автомобиля, текстовое представление
		private string tmp_auto_vin;        // VIN автомобиля
											//private string tmp_auto_plate;      // Регистрационный знак
		private string tmp_master;          // Сервис-консультант закрывший наряд
		
		private bool tmp_guaranty;      // Отметка о наличии гарантии в карточке
		private bool tmp_to;                // Отметка о наличии ТО в карточке

		private float tmpHourPrice; // Стоимость нормачаса по умолчанию
		private float tmpHouePriceGuaranty; // Стоимость гарантийного нормачаса по умолчанию
		private DbAutoType tmpAutoType; // Трудоемкости по умолчанию
		private DtWorkshop tmpWorkshop; // Подразделение карточки


		public bool FLAG_ERROR;

		// Улучшаем код май 2022
		private ArrayList cardWorks = null; // Список работ карточки
		private ArrayList cardDetails = null; // Список работ карточки
		private DtPartner tmp_owner = null; // Временная переменная - владелец в карточке
		private DtPartner tmp_representative = null; // Временная переменная - представитель в карточке


		//private string tmp_service_manager; // Сервис консультант ведущий заказ-наряд
		private DtStaff _tmpServiceManager = null; // Временная переменная - сервис консультант карточки
		private string tmp_service_manager_txt; // Сервис консультант ведущий заказ-наряд

		#region Методы установки значений со сложними проверками
		public void SetCardRun(int srcNewRun)
		{
			if (srcNewRun < 0) { MessageBox.Show("Пробег не может быть мньше нуля"); return; }
			if (srcNewRun == 0) { MessageBox.Show("Пробег не может быть нулевым"); return; }
			DateValuePair runAtDate = DtAuto.ReadDbLastRun(DtAuto); // Находим в базе данных предыдущий пробег
			int lastRun = runAtDate.GetInt();
			if (srcNewRun < lastRun) { MessageBox.Show("Пробег меньше предыдущего"); return; }
			CardRun = srcNewRun;
		}
		#endregion
		#region Геттеры и сеттеры правильные
		public DtCardActionCollection Actions
        {
            get
            {
				if(_cardActions == null)
                {
					_cardActions = new DtCardActionCollection(this);
                }
				return _cardActions;
            }
        }
		public DtCardClaimCollection Claims
		{
			get
			{
				if (_cardClaims == null)
				{
					_cardClaims = new DtCardClaimCollection(this);
				}
				return _cardClaims;
			}
		}
		public DtLicensePlate LicensePlate
		{
			get { return _licensePlate; }
			set
			{
				if (value == null) return;
				_licensePlate = value;
				IsChg = true;
			}
		}
        public int CardRun
		{
			get { return _run; }
            set
            {
				if (value < 0) { MessageBox.Show("Пробег не может быть мньше нуля"); return; }
				if (_run == value) return;
				_run = value;
				IsChg = true;
			}
		}
		public long Number
		{
			get { return _number; }
			set { _number = value; }
		}
		public int Year
		{
			get { return _year; }
			set { _year = value; }
		}
		public DateTime Date
		{
			get { return _date; }
			set { _date = value; }
		}

		public long CodeLicenseVihicle
        {
            get { return code_licence_vehicle; }
            set { code_licence_vehicle = value; }
        }
		public DtCardWorkCollection WorkCollection
        {
			get
			{
				if (_cardWorks == null)
					LoadCardWorksForce(); // Загружаем список работ карточки
				return _cardWorks;
			}
        }
		#endregion

		public DtCard(long srcCardNumber, int srcCardYear):this()
        {
			Number = srcCardNumber;
			Year = srcCardYear;
			IsNew = false; // Это не новый элемент
			IsChg = false; // Это не измененный элемент
        }
		public DtCard(DtCard src)
		{
			_code = src._code;
			_number = src._number;
			_year = src._year;
			code_partner = src.code_partner;
			code_represent = src.code_represent;
			code_auto = src.code_auto;
			_run = src._run;
			represent_document = src.represent_document;
			_warrantNumber = src._warrantNumber;
			_date = src._date;
			warrant_date_open = src.warrant_date_open;
			warrant_date_close = src.warrant_date_close;
			_state = src._state;
			cashless = src.cashless;
			credit_card = src.credit_card;
			inner = src.inner;
			comment = src.comment;
			code_guaranty = src.code_guaranty;
			code_workshop = src.code_workshop;
			code_work = src.code_work;
			_stateControl = src.StateControl;
			_masterCode = src.MasterCode;
			discount = src.discount;
			discount_parts = src.discount_parts;
			service_manager_code = src.service_manager_code;

			code_licence_vehicle = src.code_licence_vehicle;
			returned = src.returned;

			// Одобрения
			supervisor_guaranty = src.supervisor_guaranty;
			supervisor_payment = src.supervisor_payment;
			supervisor_whole = src.supervisor_whole;

			// Представления
			tmp_owner = src.tmp_owner;
			tmp_auto_model = src.tmp_auto_model;

			tmp_auto_vin = src.tmp_auto_vin;

			tmp_auto_plate = src.tmp_auto_plate;
			tmp_master = src.tmp_master;
		//	tmp_service_manager = src.tmp_service_manager;

			tmp_guaranty = src.tmp_guaranty;
			tmp_to = src.tmp_to;

			// Агрегированные связанные списки
			_cardActions = src._cardActions;
			_cardClaims = src._cardClaims;
			_cardWorks = src._cardWorks;
		}

		public DtCard()
		{
			// Правильные переменные
			_number = 0;
			_year = 0;
			_date = DateTime.MinValue;
			_run = 0;
			_licensePlate = new DtLicensePlate("", "");
			_agreedPickupTime = DateTime.MinValue;
			_is_agreedPickupTime = false;
			_state = 0;
			_warrantNumber = 0;

			_code = 0;
			
			code_partner = 0;
			code_represent = 0;
			CodeAuto = 0;
			DtAuto = null;
			
			represent_document = "";
			
			
			warrant_date_open = DateTime.Now;
			warrant_date_close = DateTime.Now;
			
			cashless = false;
			credit_card = false;
			inner = false;
			comment = "";
			code_guaranty = 0;
			code_workshop = 0;
			code_work = 0;
			_stateControl = 0;
			_masterCode = 0;
			discount = 0.0F;
			discount_parts = 0.0F;

			code_licence_vehicle = 0L;
			returned = false;
			service_manager_code = 0L;

			// Одобрения
			supervisor_guaranty = 0;
			supervisor_payment = 0;
			supervisor_whole = 0;

			card_rate = 0;
			card_rate_change = false;

			card_call_rate = 0;
			card_call_work_done = false;

			// Представления
			tmp_owner_txt = "";
			tmp_auto_model = "";

			tmp_auto_vin = "";

			//tmp_auto_plate = "";
			tmp_master = "";
			tmp_service_manager_txt = "";

			tmp_guaranty = false;
			tmp_to = false;

			tmp_owner = null;	
			tmpAutoType = null;
			tmpWorkshop = null;

			IsChg = false;
			IsNew = true;

			// Агрегированные связанные списки
			_cardActions = null;
			_cardClaims = null;
			_cardWorks = null;
		}

		public void SetData(string data, object val)
		{
			switch (data)
			{
				case "КОД_КАРТОЧКА":
					_code = (long)val;
					break;
				case "НОМЕР_КАРТОЧКА":
					Number = (long)val;
					break;
				case "ГОД_КАРТОЧКА":
					Year = (int)val;
					break;
				case "ВЛАДЕЛЕЦ_КАРТОЧКА":
					code_partner = (long)val;
					break;
				case "ПРЕДСТАВИТЕЛЬ_КАРТОЧКА":
					code_represent = (long)val;
					break;
				case "АВТОМОБИЛЬ_КАРТОЧКА":
					CodeAuto = (long)val;
					break;
				case "ДОКУМЕНТ_ПРЕДСТАВИТЕЛЬ_КАРТОЧКА":
					represent_document = (string)val;
					break;
				case "ПРОБЕГ_КАРТОЧКА":
					CardRun = (int)val;
					break;
				case "НОМЕР_НАРЯД_КАРТОЧКА":
					WarrantNumber = (long)val;
					break;
				case "ДАТА":
					Date = (DateTime)val;
					break;
				case "ДАТА_НАРЯД_ОТКРЫТ_КАРТОЧКА":
					warrant_date_open = (DateTime)val;
					break;
				case "ДАТА_НАРЯД_ЗАКРЫТ_КАРТОЧКА":
					warrant_date_close = (DateTime)val;
					break;
				case "СТАТУС_КАРТОЧКА":
					_state = (DtCard.CardState)(short)val;
					break;
				case "КРЕДИТНАЯ_КАРТА_КАРТОЧКА":
					credit_card = (bool)val;
					break;
				case "ВНУТРЕННИЙ_КАРТОЧКА":
					inner = (bool)val;
					break;
				case "БЕЗНАЛИЧНЫЙ_КАРТОЧКА":
					cashless = (bool)val;
					break;
				case "ПРИМЕЧАНИЕ_КАРТОЧКА":
					comment = (string)val;
					break;
				case "ВИД_ГАРАНТИЯ_КАРТОЧКА":
					code_guaranty = (long)val;
					break;
				case "ПОДРАЗДЕЛЕНИЕ_КАРТОЧКА":
					code_workshop = (long)val;
					break;
				case "ВИД_ТРУДОЕМКОСТЬ_КАРТОЧКА":
					code_work = (long)val;
					break;
				case "СТАТУС_КОНТРОЛЬ_КАРТОЧКА":
					StateControl = (short)val;
					break;
				case "МАСТЕР_КОНТРОЛЕР_КАРТОЧКА":
					MasterCode = (long)val;
					break;
				case "ВЛАДЕЛЕЦ":
					tmp_owner_txt = (string)val;
					break;
				case "АВТОМОБИЛЬ_МОДЕЛЬ":
					tmp_auto_model = (string)val;
					break;
				case "АВТОМОБИЛЬ_VIN":
					tmp_auto_vin = (string)val;
					break;
				case "АВТОМОБИЛЬ_РЕГИСТРАЦИОННЫЙ_ЗНАК":
					tmp_auto_plate = (string)val;
					break;
				case "СКИДКА_РАБОТА_КАРТОЧКА":
					discount = (float)val;
					break;
				case "СКИДКА_ДЕТАЛЬ_КАРТОЧКА":
					discount_parts = (float)val;
					break;
				case "ОДОБРЕНИЕ_ГАРАНТИЯ":
					supervisor_guaranty = (long)val;
					break;
				case "ОДОБРЕНИЕ_ОПЛАТА":
					supervisor_payment = (long)val;
					break;
				case "ОДОБРЕНИЕ_ПОЛНОЕ":
					supervisor_whole = (long)val;
					break;
				case "СВИДЕТЕЛЬСТВО_ТС":
					code_licence_vehicle = (long)val;
					break;
				case "ВОЗВРАТ":
					returned = (bool)val;
					break;
				case "ЗАКРЫЛ_НАРЯД":
					tmp_master = (string)val;
					break;
				case "СЕРВИС_КОНСУЛЬТАНТ":
					service_manager_code = (long)val;
					break;
				case "СЕРВИС_КОНСУЛЬТАНТ_ФАМИЛИЯ":
					tmp_service_manager_txt = (string)val;
					break;
				case "РЕКОМЕНДАЦИЯ":
					card_rate = (short)val;
					break;
				case "РЕКОМЕНДАЦИЯ_ИЗМЕНЕНА":
					card_rate_change = (bool)val;
					break;
				case "НЕИСПРАВНОСТЬ_УСТРАНЕНА":
					card_call_work_done = (bool)val;
					break;
				case "ОБЩАЯ_ОЦЕНКА":
					card_call_rate = (short)val;
					break;
				case "ВРЕМЯ_ВЫДАЧИ_СОГЛАСОВАННОЕ":
					_agreedPickupTime = (DateTime)val;
					break;
				case "ЕСТЬ_ВРЕМЯ_ВЫДАЧИ_СОГЛАСОВАННОЕ":
					_is_agreedPickupTime = (bool)val;
					break;
				default:
					break;
			}
		}
		public object GetData(string data)
		{
			DtTxtCard txtCard = new DtTxtCard(this);
			switch (data)
			{
				case "КОД_КАРТОЧКА":
					return (object)CardCode;
				case "НОМЕР_КАРТОЧКА":
					return (object)(long)Number;
				case "ДАТА":
					return (object)(DateTime)Date;
				case "НОМЕР_НАРЯД_КАРТОЧКА":
					return (object)(long)WarrantNumber;
				case "ГОД_КАРТОЧКА":
					return (object)(int)Year;
				case "ВЛАДЕЛЕЦ_КАРТОЧКА":
					return (object)(long)code_partner;
				case "ПРЕДСТАВИТЕЛЬ_КАРТОЧКА":
					return (object)(long)code_represent;
				case "АВТОМОБИЛЬ_КАРТОЧКА":
					return (object)(long)CodeAuto;
				case "ДОКУМЕНТ_ПРЕДСТАВИТЕЛЬ_КАРТОЧКА":
					return (object)(string)represent_document;
				case "ПРОБЕГ_КАРТОЧКА":
					return (object)(int)CardRun;
				case "СТАТУС_КАРТОЧКА":
					return (object)(short)_state;
				case "КРЕДИТНАЯ_КАРТА_КАРТОЧКА":
					return (object)(bool)credit_card;
				case "БЕЗНАЛИЧНЫЙ_КАРТОЧКА":
					return (object)(bool)cashless;
				case "ВНУТРЕННИЙ_КАРТОЧКА":
					return (object)(bool)inner;
				case "ПРИМЕЧАНИЕ_КАРТОЧКА":
					return (object)(string)comment;
				case "ПОДРАЗДЕЛЕНИЕ_КАРТОЧКА":
					return (object)(long)code_workshop;
				case "ВИД_ГАРАНТИЯ_КАРТОЧКА":
					return (object)(long)code_guaranty;
				case "ВИД_ТРУДОЕМКОСТЬ_КАРТОЧКА":
					return (object)(long)code_work;
				case "СТАТУС_КОНТРОЛЬ_КАРТОЧКА":
					return (object)StateControl;
				case "МАСТЕР_КОНТРОЛЕР_КАРТОЧКА":
					return (object)(long)MasterCode;
				case "ВЛАДЕЛЕЦ":
					return (object)(string)tmp_owner_txt;
				case "АВТОМОБИЛЬ_МОДЕЛЬ":
					return (object)(string)tmp_auto_model;
				case "АВТОМОБИЛЬ_VIN":
					return txtCard.AutoVin;//(object)(string)tmp_auto_vin;
				case "АВТОМОБИЛЬ_РЕГИСТРАЦИОННЫЙ_ЗНАК":
					return (object)(string)tmp_auto_plate;
				case "ДАТА_НАРЯД_ОТКРЫТ_КАРТОЧКА":
					return (object)(DateTime)warrant_date_open;
				case "ДАТА_НАРЯД_ЗАКРЫТ_КАРТОЧКА":
					return (object)(DateTime)warrant_date_close;
				case "СКИДКА_РАБОТА_КАРТОЧКА":
					return (object)(float)discount;
				case "СКИДКА_ДЕТАЛЬ_КАРТОЧКА":
					return (object)(float)discount_parts;
				case "ОДОБРЕНИЕ_ГАРАНТИЯ":
					return (object)(long)supervisor_guaranty;
				case "ОДОБРЕНИЕ_ОПЛАТА":
					return (object)(long)supervisor_payment;
				case "ОДОБРЕНИЕ_ПОЛНОЕ":
					return (object)(long)supervisor_whole;
				case "ВОЗВРАТ":
					return (object)(bool)returned;
				case "ЗАКРЫЛ_НАРЯД":
					return (object)(string)tmp_master;
				case "СЕРВИС_КОНСУЛЬТАНТ":
					return (object)(long)service_manager_code;
				case "СЕРВИС_КОНСУЛЬТАНТ_ФАМИЛИЯ":
					return (object)(string)tmp_service_manager_txt;
				default:
					return (object)null;
			}
		}
		public void SetLVItem(ListViewItem item)
		{
			DtTxtCard txtCard = new DtTxtCard(this);
			item.SubItems.Clear();
			item.Tag = CardCode;
			item.Text = Number.ToString();
			item.SubItems.Add(Date.ToShortDateString());
			item.SubItems.Add(WarrantNumber.ToString());
			item.SubItems.Add(tmp_owner_txt);
			item.SubItems.Add(tmp_auto_model);
			item.SubItems.Add(tmp_auto_vin);//item.SubItems.Add(txtCard.AutoVin);
			item.SubItems.Add(tmp_auto_plate);
			item.SubItems.Add(this.CardRun.ToString());
			//	item.SubItems.Add(tmp_master);
			item.SubItems.Add(tmp_service_manager_txt + "/" + tmp_master);
			item.SubItems.Add(comment);
			switch (_state)
			{
				case CardState.OPEND:
					item.BackColor = Color.Yellow;
					break;
				case CardState.CLOSED:
					item.BackColor = Color.LightGreen;
					break;
				case CardState.STOPPED:
					item.BackColor = Color.Red;
					break;
				case CardState.REOPEND:
					item.BackColor = Color.Yellow;
					break;
				case CardState.CANCELED:
					item.BackColor = Color.Gray;
					break;
				default:
					break;
			}
		}
		public void SetLVItemPOSTimer(ListViewItem item)
		{
			DtTxtCard txtCard = new DtTxtCard(this);
			item.SubItems.Clear();
			DtCard.Pair pair = new DtCard.Pair();
			pair.number = (long)Number;
			pair.year = (int)Year;
			item.Tag = (DtCard.Pair)pair;
			item.Text = Number.ToString();
			item.SubItems.Add(tmp_auto_plate);
			item.SubItems.Add(tmp_auto_model);
			item.SubItems.Add(txtCard.AutoVin);
			item.StateImageIndex = 0;
		}
		public void SetLVItemCardRate(ListViewItem item)
		{
			DtTxtCard txtCard = new DtTxtCard(this);
			item.SubItems.Clear();
			DtCard.Pair pair = new DtCard.Pair();
			pair.number = (long)Number;
			pair.year = (int)Year;
			item.Tag = (DtCard.Pair)pair;
			item.Text = this.WarrantNumber.ToString() + "/" + Year.ToString();
			item.SubItems.Add(this.Date.ToShortDateString());
			item.SubItems.Add(this.tmp_auto_model);
			item.SubItems.Add(txtCard.AutoVin);
			if (this.tmp_to == true)
				item.SubItems.Add("ДА");
			else
				item.SubItems.Add("");
			if (this.tmp_guaranty == true)
				item.SubItems.Add("ДА");
			else
				item.SubItems.Add("");
			item.SubItems.Add(this.tmp_master + "/" + this.tmp_service_manager_txt);
			if (this.card_rate == 0)
				item.SubItems.Add("");
			else
			{
				string txt = "ОШИБКА";
				if (card_rate == 1) txt = "ДА КОНЕЧНО";
				if (card_rate == 2) txt = "МОЖЕТ БЫТЬ";
				if (card_rate == 3) txt = "ТОЧНО НЕТ";
				if (card_rate_change == true) txt += "/ДА";
				item.SubItems.Add(txt);
			}

			if (this.card_call_work_done == true)
				item.SubItems.Add("ДА");
			else
				item.SubItems.Add("");

			if (this.card_call_rate == 0)
				item.SubItems.Add("");
			else
			{
				string txt = "ОШИБКА";
				if (card_call_rate == 1) txt = "ОТЛИЧНО.";
				if (card_call_rate == 2) txt = "ХОРОШО.";
				if (card_call_rate == 3) txt = "МОГЛО БЫТЬ И ЛУЧШЕ.";
				if (card_call_rate == 4) txt = "УДОВЛЕТВОРИТЕЛЬНО.";
				if (card_call_rate == 5) txt = "ПЛОХО.";
				if (card_call_rate == 6) txt = "УЖАСНО. БОЛЬШЕ НЕ ПРИЕДУ.";
				if (card_call_rate == 7) txt = "ВЕЧНО НЕДОВОЛЕН. НЕ УГОДИТЬ.";
				if (card_call_rate == 8) txt = "МЗАТРУДНИТЕЛЬНО ОЦЕНИТЬ.";
				item.SubItems.Add(txt);
			}

			// Расцвечиваем
			if (this.tmp_guaranty || this.tmp_to)
			{
				if (this.card_call_rate == 0 && this.card_rate == 0) item.BackColor = Color.Red;
			}
		}

		public float SummWorkPay()
		{
			float summ = 0.0F;
			ArrayList works = new ArrayList();
			DbSqlCardWork.SelectInArray(this, works);
			foreach (object o in works)
			{
				DtCardWork work = (DtCardWork)o;
				summ += work.WorkSummCash;
			}
			summ = (float)summ - (float)Math.Round((float)(summ / 100 * discount), 0);
			return summ;
		}

		public float SummWorkPayNoDiscount()
		{
			float summ = 0.0F;
			ArrayList works = new ArrayList();
			DbSqlCardWork.SelectInArray(this, works);
			foreach (object o in works)
			{
				DtCardWork work = (DtCardWork)o;
				summ += work.WorkSummCash;
			}
			return summ;
		}

		public float SummWorkPayDiscount(float discount_value)
		{
			float summ = 0.0F;
			ArrayList works = new ArrayList();
			DbSqlCardWork.SelectInArray(this, works);
			foreach (object o in works)
			{
				DtCardWork work = (DtCardWork)o;
				summ += work.WorkSummCash;
			}
			summ = (float)summ - (float)Math.Round((float)(summ / 100 * discount_value), 0);
			return summ;
		}

		public float SummDetailPay()
		{
			float summ = 0.0F;
			ArrayList details = new ArrayList();
			DbSqlCardDetail.SelectInArray(this, details);
			foreach (object o in details)
			{
				DtCardDetail detail = (DtCardDetail)o;
				// Добавляем округление в большую сторону
				summ += (float)Math.Ceiling(detail.DetailSummCash);
			}
			return summ;
		}
		public float SummDetailOilPay()
		{
			float summ = 0.0F;
			ArrayList details = new ArrayList();
			DbSqlCardDetail.SelectInArray(this, details);
			foreach (object o in details)
			{
				DtCardDetail detail = (DtCardDetail)o;
				summ += detail.DetailSummOilCash;
			}
			return summ;
		}
		public void SetTNode_Supervisor(TreeNode node)
		{
			// Подразделение
			DtWorkshop workshop = DbSqlWorkshop.Find(code_workshop);
			string workshop_txt = "";
			if (workshop != null) workshop_txt = (string)workshop.GetData("НАИМЕНОВАНИЕ_ЦЕХ");
			workshop_txt = workshop_txt.ToUpper();

			node.Text = workshop_txt + " " + Number.ToString() + "/" + Year.ToString() + " (" + WarrantNumber.ToString() + "/" + Year.ToString() + ")";
			Data data = new Data();
			Pair pair = new Pair();
			pair.number = Number;
			pair.year = Year;
			data.group = NodeGroup.ROOT;
			data.type = NodeType.NONE;
			data.data = (object)pair;
			node.Tag = data;

			if (supervisor_whole > 0)
			{
				node.ImageIndex = 2;
				node.SelectedImageIndex = 2;
				DtStaff staff = DbSqlStaff.Find(supervisor_whole);
				string staff_txt = "ОШИБКА";
				if (staff != null) staff_txt = staff.Title;
				node.Text = node.Text + " " + staff_txt;
			}
			else
			{
				node.ImageIndex = 1;
				node.SelectedImageIndex = 1;
			}

			#region ОПЛАТА
			{
				// Подветка, отвечающая за ОПЛАТЫ
				string pay_type = "";
				if (inner == false && cashless == false) pay_type = "НАЛИЧНЫЙ";
				if (cashless == true) pay_type = "БЕЗНАЛИЧНЫЙ";
				if (inner == true) pay_type = "ВНУТРЕННИЙ";
				TreeNode nd = new TreeNode("ОПЛАТА " + pay_type);
				Data dt = new Data();
				dt.group = NodeGroup.PAY_ROOT;
				dt.type = NodeType.NONE;
				nd.Tag = dt;

				if (supervisor_payment == 0)
				{
					nd.ImageIndex = 1;              // Платеж не одобрен
					nd.SelectedImageIndex = 1;
				}
				else
				{
					DtStaff staff = DbSqlStaff.Find(supervisor_payment);
					string staff_txt = "ОШИБКА";
					if (staff != null) staff_txt = staff.Title;
					nd.Text = "ОПЛАТА " + pay_type + " (" + staff_txt + ")";
					nd.ImageIndex = 2;
					nd.SelectedImageIndex = 2;
				}
				float summ_1 = SummWorkPay() + SummDetailOilPay();  // К оплате - ТО, Работы, Масла - КАССА 1
				float summ_2 = SummDetailPay();                     // К оплате - Запчасти - КАССА 2
				if (summ_1 + summ_2 > 0.0F)
				{
					// Случай наличной оплаты
					node.Nodes.Add(nd);

					if (inner == false && cashless == false)
					{
						if (summ_1 > 0)
						{
							// Добавляем подветку оплаты по кассе 1
							TreeNode nd1 = new TreeNode("К оплате ТО, Работы, Масла " + summ_1.ToString());
							nd.Nodes.Add(nd1);
							// Платежи по первой кассе
							ArrayList payments = new ArrayList();
							DbSqlPayment.SelectInArrayCardWorkshopDepartment(payments, Number, Year, code_workshop, 1);
							float summ = 0.0F;
							foreach (object o in payments)
							{
								CS_Payment pay = (CS_Payment)o;
								TreeNode nd2 = new TreeNode();
								pay.SetTNode_Supervisor(nd2);
								nd1.Nodes.Add(nd2);
								summ += pay.summ;
							}
							if (summ == summ_1)
							{
								nd1.ImageIndex = 2;
								nd1.SelectedImageIndex = 2;
							}
							else
							{
								nd1.ImageIndex = 1;
								nd1.SelectedImageIndex = 1;
							}
						}
						if (summ_2 > 0)
						{
							// Добавляем подветку оплаты по кассе 2
							TreeNode nd1 = new TreeNode("К оплате Запасные части " + summ_2.ToString());
							nd.Nodes.Add(nd1);
							// Платежи по первой кассе
							ArrayList payments = new ArrayList();
							DbSqlPayment.SelectInArrayCardWorkshopDepartment(payments, Number, Year, code_workshop, 2);
							float summ = 0.0F;
							foreach (object o in payments)
							{
								CS_Payment pay = (CS_Payment)o;
								TreeNode nd2 = new TreeNode();
								pay.SetTNode_Supervisor(nd2);
								nd1.Nodes.Add(nd2);
								summ += pay.summ;
							}
							if (Math.Round(summ, 2) == Math.Round(summ_2, 2))
							{
								nd1.ImageIndex = 2;
								nd1.SelectedImageIndex = 2;
							}
							else
							{
								nd1.ImageIndex = 1;
								nd1.SelectedImageIndex = 1;
							}
						}
					}
				}

			}
			#endregion

			#region ГАРАНТИЯ
			{
				// Подветка, отвечающая за ГАРАНТИЮ
				TreeNode nd = new TreeNode("ГАРАНТИЯ");
				if (supervisor_guaranty == 0)
				{
					nd.ImageIndex = 1;              // Гарантия не одобрена
					nd.SelectedImageIndex = 1;
				}
				else
				{
					DtStaff staff = DbSqlStaff.Find(supervisor_guaranty);
					string staff_txt = "ОШИБКА";
					if (staff != null) staff_txt = staff.Title;
					nd.Text = "ГАРАНТИЯ (" + staff_txt + ")";
					nd.ImageIndex = 2;
					nd.SelectedImageIndex = 2;
				}
				bool exist_flag = false;
				// Перебираем все гарантийные запчасти
				ArrayList details = new ArrayList();
				DbSqlCardDetail.SelectInArray(this, details);
				foreach (object o in details)
				{
					DtCardDetail detail = (DtCardDetail)o;
					if ((bool)detail.GetData("ГАРАНТИЯ_КАРТОЧКА_ДЕТАЛЬ") == true)
					{
						if (exist_flag == false)
						{
							node.Nodes.Add(nd);
							exist_flag = true;
						}
						detail = DbSqlCardDetail.Find(this, (long)detail.GetData("ПОЗИЦИЯ_КАРТОЧКА_ДЕТАЛЬ"));
						TreeNode nd1 = new TreeNode("ОШИБКА");
						Data dt = new Data();
						dt.group = NodeGroup.GUARANTY;
						dt.type = NodeType.DETAIL;
						dt.data = (object)(long)detail.GetData("ПОЗИЦИЯ_КАРТОЧКА_ДЕТАЛЬ");
						nd1.Tag = dt;
						detail.SetTNode_Supervisor(nd1);
						nd.Nodes.Add(nd1);
					}
				}
				// Перебираем все гарантийные работы
				ArrayList works = new ArrayList();
				DbSqlCardWork.SelectInArray(this, works);
				foreach (object o in works)
				{
					DtCardWork work = (DtCardWork)o;
					if ((bool)work.GetData("ГАРАНТИЯ_КАРТОЧКА_РАБОТА") == true)
					{
						if (exist_flag == false)
						{
							node.Nodes.Add(nd);
							exist_flag = true;
						}
						work = DbSqlCardWork.Find(this, (int)work.GetData("ПОЗИЦИЯ_КАРТОЧКА_РАБОТА"));
						TreeNode nd1 = new TreeNode("ОШИБКА");
						Data dt = new Data();
						dt.group = NodeGroup.GUARANTY;
						dt.type = NodeType.WORK;
						dt.data = (object)(int)work.GetData("ПОЗИЦИЯ_КАРТОЧКА_РАБОТА");
						nd1.Tag = dt;
						work.SetTNode_Supervisor(nd1);
						nd.Nodes.Add(nd1);
					}
				}
			}
			#endregion

		}

		// Определяем, есть ли у карточки гарантийные работы
		public bool IsGuarantyWork()
		{
			// Получаем все работы карточки
			ArrayList card_works = new ArrayList();
			DbSqlCardWork.SelectInArray(this, card_works);
			foreach (DtCardWork work in card_works)
			{
				if (work.GuaranteeFlag() == true)
				{
					tmp_guaranty = true;
					return true;
				}
			}
			return false;
		}

		public bool IsGuarantyDetail()
		{
			// Получаем все работы карточки
			ArrayList card_details = new ArrayList();
			DbSqlCardDetail.SelectInArray(this, card_details);
			foreach (DtCardDetail detail in card_details)
			{
				if (detail.IsGuaranty == true)
				{
					tmp_guaranty = true;
					return true;
				}
			}
			return false;
		}

		// Определяем, есть ли у карточки ТО
		public bool IsToWork()
		{
			// Получаем все работы карточки
			ArrayList card_works = new ArrayList();
			DbSqlCardWork.SelectInArray(this, card_works);
			foreach (DtCardWork work in card_works)
			{
				// Проверяем гарантию
				DtCardWork fwork = DbSqlCardWork.Find(this, (int)work.GetData("ПОЗИЦИЯ_КАРТОЧКА_РАБОТА"));
				if (fwork != null)
				{
					if (fwork.IsTo() == true)
					{
						tmp_to = true;
						return true;
					}
				}
			}
			return false;
		}

		public string NameToWork()
		{
			// Получаем все работы карточки
			ArrayList card_works = new ArrayList();
			DbSqlCardWork.SelectInArray(this, card_works);
			foreach (DtCardWork work in card_works)
			{
				// Проверяем гарантию
				DtCardWork fwork = DbSqlCardWork.Find(this, (int)work.GetData("ПОЗИЦИЯ_КАРТОЧКА_РАБОТА"));
				if (fwork != null)
				{
					if (fwork.IsTo() == true)
					{
						tmp_to = true;
						return (string)fwork.GetData("НАИМЕНОВАНИЕ_КАРТОЧКА_РАБОТА");
					}
				}
			}
			return "";
		}

		#region Доступ к данным Гетеры и сеттеры простые
		public ArrayList CardDetails
		{
			get
			{
				return cardDetails;
			}
		}
		public ArrayList CardWorks
		{
			get
			{
				return _cardWorks.Collection;
				//return cardWorks;
			}
		}
		public long CardCode
		{
			set { _code = value; }
			get { return _code; }
		}
		public long MasterCode
        {
            get { return _masterCode; }
			set { _masterCode = value; }
        }
		public short StateControl
        {
            get { return _stateControl; }
			set { _stateControl = value; }
        }
		
		public long LicenceVihicleCode
		{
			get { return code_licence_vehicle; }
		}
		public long ServiceManagerCode
		{
            get { return service_manager_code; }
			set { service_manager_code = value; }
		}
		public long CodeAuto
		{
			get { return code_auto; }
			set { code_auto = value; }
		}
	
		public string Comment
		{
			get { return comment; }
			set
			{
				if (comment == value) return;
				comment = value;
				IsChg = true;
			}
		}
		public long CodeOwner
        {
            get { return code_partner; }
            set { code_partner = value; }
        }
		public float Discount
        {
            get { return discount; }
            set {
				if (value < 0) return;
				if (value == discount) return;
				discount = value;
				IsChg = true;
			}
        }
		public long WarrantNumber
        {
            get { return _warrantNumber; }
			set { _warrantNumber = value; }
		}
		public long CodeWorkshop
		{
			get { return code_workshop; }
			set { code_workshop = value; }
		}
		public long CodeRepresentative
		{
			get { return code_represent; }
			set { code_represent = value; }
		}
		public string RepresentativeDocs
		{
			get { return represent_document; }
			set
			{
				if (represent_document == value) return;
				represent_document = value;
				IsChg = true;
			}
		}
		public DtCard.CardState State
		{
			get { return _state; }
			set { _state = value; }
		}
		public bool Returned
		{
			get
			{
				return returned;
			}
			set
			{
				if (returned != value)
				{
					returned = value;
					IsChg = true;
				}
			}
		}
		public DtCard.PAY_TYPE PayType
		{
			get
			{
				if (CreditCard) return PAY_TYPE.CREDIT_CARD;
				if (Cashless) return PAY_TYPE.CASHLESS;
				if (Inner) return PAY_TYPE.NO_PAY;
				return PAY_TYPE.CASH;
			}
			set
			{
				switch (value)
				{
					case PAY_TYPE.CREDIT_CARD:
						CreditCard = true;
						break;
					case PAY_TYPE.NO_PAY:
						Inner = true;
						break;
					case PAY_TYPE.CASHLESS:
						Cashless = true;
						break;
					case PAY_TYPE.CASH:
						CreditCard = false;
						Inner = false;
						Cashless = false;
						break;
					default:
						break;
				}
			}
		}
		public bool Cashless
		{
			get { return cashless; }
			set
			{
				if (cashless != value)
				{
					cashless = value;
					if (cashless == true)
					{
						inner = false;
						credit_card = false;
					}
					IsChg = true;
				}
			}
		}
		public bool Inner
		{
			get { return inner; }
			set
			{
				if (inner != value)
				{
					inner = value;
					if (inner == true)
					{
						cashless = false;
						credit_card = false;
					}
					IsChg = true;
				}
			}
		}
		public bool CreditCard
		{
			get { return credit_card; }
			set
			{
				if (credit_card != value)
				{
					credit_card = value;
					if (credit_card == true)
					{
						inner = false;
						cashless = false;
					}
					IsChg = true;
				}
			}
		}
		public float DiscountDetail
		{
			get
			{
				return discount_parts;
			}
			set
			{
				if (discount_parts != value)
				{
					discount_parts = value;
					IsChg = true;
				}
			}
		}
		public bool IsAgreedPickUpTime
		{
			get
			{
				if (_agreedPickupTime == DateTime.MinValue) return false;
				return true;
			}
		}
		public DateTime AgreedPickUpTime
		{
			get { return _agreedPickupTime; }
			set
			{
				if (value == _agreedPickupTime) return;
				_agreedPickupTime = value;
				IsChg = true;
				_is_agreedPickupTime = true;
			}
		}

		public long CodeAutoType
		{
			get { return code_work; }
			set { code_work = value; }
		}
		#endregion
		#region Сложные геттеры и сеттеры
		public DtAuto DtAuto
		{
			get
			{
				if (CodeAuto == 0) return null;
				if (_dtAuto != null) return _dtAuto;
				else
				{
					_dtAuto = DbSqlAuto.Find(CodeAuto);
					return _dtAuto;
				}
			}
			set
			{
				if (value == null && CodeAuto == 0) return;
				if (value == null) CodeAuto = 0;
				else CodeAuto = value.Code;
				_dtAuto = value;
				_dtAuto.IsChg = true;
				//IsChg = true;
				ChangeMember("AUTO");
				// Так же меняем набор трудоемкостей если не установлен
				if (CodeAutoType == 0)
					CodeAutoType = value.CodeAutoType;
				// В обязательном порядке меняем регистрационный номер карточки
				LicensePlate = DtAuto.LicensePlate;

				// Вызываем событие - отобразить изменение автомобиля
				//DisplayChanges?.Invoke(_dtAuto, "Авто" );
				
			}
		}
		public DtAuto Auto
		{
			get
			{
				if (CodeAuto == 0) return null;
				if (_dtAuto != null) return _dtAuto;
				else
				{
					_dtAuto = DbSqlAuto.Find(CodeAuto);
					return _dtAuto;
				}
			}
			set
			{
				if (value == null && CodeAuto == 0) return;
				if (value == null) CodeAuto = 0;
				else CodeAuto = value.Code;
				_dtAuto = value;
				_dtAuto.IsChg = true;
				IsChg = true;
				// Так же меняем набор трудоемкостей если не установлен
				if (CodeAutoType == 0)
					CodeAutoType = value.CodeAutoType;
				// В обязательном порядке меняем регистрационный номер карточки
				LicensePlate = DtAuto.LicensePlate;
			}
		}
		public DtPartner Owner
		{
			set
			{
				if (value == null && CodeOwner == 0) return;
				if (value == null)
				{
					CodeOwner = 0;
					Discount = 0;
				}
				else
				{
					CodeOwner = value.Code;
					// Так же, при выборе нового владельца, меняем размеры скидок по умолчанию
					DtPartnerProperty tmpProperties = value.Properties;
					if (tmpProperties != null)
						Discount = tmpProperties.Discount;
					else
						Discount = 0;
				}
				tmp_owner = value;
				IsChg = true;
			}
			get
			{
				if (CodeOwner == 0) return null;
				if (tmp_owner != null) return tmp_owner;
				else
				{
					tmp_owner = DbSqlPartner.Find(CodeOwner);
					return tmp_owner;
				}
			}
		}
		public DtWorkshop Workshop
		{
			set
			{
				if (value == null && CodeWorkshop == 0) return;
				if (value == null)
				{
					CodeWorkshop = 0;
				}
				else
				{
					CodeWorkshop = value.CodeWorkshop;
				}
				tmpWorkshop = value;
				IsChg = true;
			}
			get
			{
				if (CodeWorkshop == 0) return null;
				if (tmpWorkshop != null) return tmpWorkshop;
				else
				{
					tmpWorkshop = DbSqlWorkshop.Find(CodeWorkshop);
					return tmpWorkshop;
				}
			}
		}
		
		public DtPartner Representative
		{
			set
			{
				if (value == null && CodeRepresentative == 0) return;
				if (value == null)
				{
					// Так же, при обнулении представителя убираем докмуент представителя
					CodeRepresentative = 0;
					RepresentativeDocs = "";
				}
				else
				{
					if (CodeRepresentative == value.Code) return;
					if (value.IsJuridical()) return; // Представитель может быть только физицеским лицом
					// Так же, при выборе нового представителя, ставим документ представителя по умолчанию
					CodeRepresentative = value.Code;
					RepresentativeDocs = "Доверенность";
				}
				tmp_representative = value;
				IsChg = true;
			}
			get
			{
				if (CodeRepresentative == 0) return null;
				if (tmp_representative != null) return tmp_representative;
				else
				{
					tmp_representative = DbSqlPartner.Find(CodeRepresentative);
					return tmp_representative;
				}
			}
		}
		public DtStaff ServiceManager
		{
			set
			{
				if (value == null) 
                {
					if(service_manager_code == 0) return;
                    else
                    {
						service_manager_code = 0;
						_tmpServiceManager = value;
						IsChg = true;
						return;
					}
                }
				if (service_manager_code == value.Code) return;
				service_manager_code = value.Code;
				_tmpServiceManager = value;
				IsChg = true;
			}
			get
			{
				if (service_manager_code == 0) return null;
				if (_tmpServiceManager == null)
					return DbSqlStaff.Find(service_manager_code);
				else
					return _tmpServiceManager;
			}
		}	
		public DbAutoType AutoType
        {
            get
            {
				if (tmpAutoType == null)
					tmpAutoType = DbAutoType.Find(code_work);
				return tmpAutoType;
            }
            set
            {
				if (value == null) return;
				if (code_work == value.Code) return;
				code_work = value.Code;
				tmpAutoType = value;
				IsChg = true;
				tmpHourPrice = value.Price;
            }
        }	
		public float HourPrice
        {
            get { 
				if(tmpHourPrice == 0.0F)
				{
					DbAutoType dbAutoType = AutoType;
					if(dbAutoType != null)
						tmpHourPrice = dbAutoType.Price;
                }
				return tmpHourPrice;
			}
            set { tmpHourPrice = value; }
        }
		#endregion

		public void LoadCardWorksForce()
		{
			_cardWorks = new DtCardWorkCollection(this);
			cardWorks = _cardWorks.Collection;
			SetWorkDiscount();

			//if (cardWorks != null) cardWorks.Clear();
			//else cardWorks = new ArrayList();
			//DbSqlCardWork.SelectInArray(this, cardWorks);
		}

		public void LoadCardDetailsForce()
		{
			if (cardDetails != null) cardDetails.Clear();
			else cardDetails = new ArrayList();
			DbSqlCardDetail.SelectInArray(this, cardDetails);
		}

		public static void SetWorkCalc(DbCardWork wrk, float srcPrice, float srcDiscount)
		{
			if (wrk.Val != 0.0F)
			{
				if (srcPrice != 0.0F)
					wrk.Price = srcPrice;
			}
			if(srcDiscount != 0.0F)
				wrk.Discount = srcDiscount;
        }

		public void SetWorkDiscount()
		{
			if (Discount == 0.0F) return; // Нет общей скидки
			foreach(DtCardWork work in CardWorks)
            {
				if (work.Discount == 0) work.Discount = Discount;
            }
		}

		#region Крупные логические методы работы с ккарточкой - АТД
		public bool Close()	// Закрыть карточку заказ-наряда текущей временем и датой
        {
			if (IsNew) return Error.ErroMessageFalse("КАРТОЧКА - НОВАЯ"); // Карточка новая
			if (IsChg) return Error.ErroMessageFalse("ЗАКАЗ-НАРЯД ИЗМЕНЕН"); // Нельзя закрывать измененный заказ-наряд
			if (!(State == DtCard.CardState.OPEND || State == DtCard.CardState.REOPEND)) return Error.ErroMessageFalse("ЗАКАЗ-НАРЯД НЕ ОТКРЫТ"); // Закрыть можно только открытый заказ-наряд
			DtCardAction closeAction = new DtCardAction(this);
			closeAction.ActionCode = DtCard.CardState.CLOSED;
			closeAction = DbSqlCardAction.Insert(closeAction);
			if (closeAction == null) return false;
			_cardActions.AddAction(closeAction);
			State = DtCard.CardState.CLOSED;
			return true;
        }
		public bool Open() // Открыть карточку заказ-наряда текущей временем и датой
		{
			if (IsNew) return Error.ErroMessageFalse("КАРТОЧКА - НОВАЯ"); // Карточка новая
			if (IsChg) return Error.ErroMessageFalse("ЗАКАЗ-НАРЯД ИЗМЕНЕН"); // Нельзя открывать измененный заказ-наряд
			if (State != DtCard.CardState.NONE) return Error.ErroMessageFalse("ЗАКАЗ-НАРЯД УЖЕ ИМЕЛ ДВИЖЕНИЯ"); // Открыть можно только карточку без движений
			DtCardAction closeAction = new DtCardAction(this);
			closeAction.ActionCode = DtCard.CardState.OPEND;
			closeAction = DbSqlCardAction.Insert(closeAction);
			if (closeAction == null) return false;
			_cardActions.AddAction(closeAction);
			State = DtCard.CardState.OPEND;
			WarrantNumber = Number;
			return true;
		}
		public bool Create() // Создать новую карточку в базе данных
		{
			if (!IsNew) return Error.ErroMessageFalse("КАРТОЧКА - НЕ НОВАЯ"); // Карточка новая
			if (!DbSqlCard.Insert(this)) return Error.ErroMessageFalse("Не согли записать в базу данных");
			return true;
		}
        #endregion

        #region Логические геттеры для удобства чтения
		public bool Closed
        {
			get
            {
				if (State == DtCard.CardState.CLOSED) return true;
				return false;
            }
        }
        #endregion

		public DtBrand.DIALER DialerOfficial
        {
            get
            {
				if (DtAuto == null) return DtBrand.DIALER.unknown;
				if(DtAuto.AutoBrand == null) return DtBrand.DIALER.unknown;
				return DtAuto.AutoBrand.DialerOfficial;
            }
        }

        #region Калькуляторы
		public float ComputePayedHoursMinusDiscout()
        {
			float hours_no_discount = 0.0F;
			float hours_with_discount = 0.0F;
			LoadCardWorksForce();
			foreach(object o in CardWorks)
            {
				DtCardWork card_work = (DtCardWork)o;
                if (card_work.GuaranteeFlag())
                {
					continue;
                }
				if( (card_work.Discount > 0) && (card_work.Discount < DISCOUNT_MAX_LIMIT_WITHOUT_PENALTY))
                {
					hours_with_discount += card_work.WorkNV;
				}
                else if (card_work.Discount >= DISCOUNT_MAX_LIMIT_WITHOUT_PENALTY)
				{
					hours_with_discount += card_work.WorkNV * (100.0F - card_work.Discount) / 100.0F;
				}
                else
                {
					hours_no_discount += card_work.WorkNV;
				}

				if(Discount > DISCOUNT_MAX_LIMIT_WITHOUT_PENALTY)
                {
					hours_no_discount = hours_no_discount * (100.0F - Discount) / 100.0F;
				}
			}
			return hours_no_discount + hours_with_discount;
        }
		public float ComputePayedHours()
		{
			float hours = 0.0F;
			LoadCardWorksForce();
			foreach (object o in CardWorks)
			{
				DtCardWork card_work = (DtCardWork)o;
				if (card_work.GuaranteeFlag())
				{
					continue;
				}
				hours += card_work.WorkNV;
			}
			return hours;
		}
		#endregion

		public string LoadPreviousRecomendations()
		{
			DateTime date_end = this.Date.AddDays(-1);
			DateTime date_start = date_end;
			date_start = date_start.AddYears(-3);

			ArrayList array = new ArrayList();
			DbSqlCard.SelectCardClosedNumberWorkshopAuto(array, date_start, date_end, CodeWorkshop, CodeAuto);
			int count;
			if((count = array.Count) == 0){
				return "";
            }
			DtCard lastCard = (DtCard)array[0];
			foreach (object o in array)
			{ 
				if(lastCard.Year < ((DtCard)o).Year)
                {
					lastCard = (DtCard)o;
                }
				else if( (lastCard.Year == ((DtCard)o).Year) && (lastCard.Number < ((DtCard)o).Number))
                {
					lastCard = (DtCard)o;
				}
			}
			ArrayList recomends = new ArrayList();
			DbSqlCardRecomendation.SelectInArray(recomends, lastCard.Number, lastCard.Year);
			string recomendsTxt = "";
			foreach (DtCardRecomendation recomend in recomends)
			{
				recomendsTxt += recomend.RecomendationTxt + ";";
			}
			return recomendsTxt;
		}
	}

    public class DtCardDatabaseConnector
    {
		private DtCard _card;

		public DtCardDatabaseConnector(DtCard dtCard)
        {
			_card = dtCard;
        }

		public bool SaveWorks()
        {
			// Записываем карточку в базу данных
			for(int index = 0; index < _card.WorkCollection.Count; index++)
			{
				DtCardWorkDatabaseConnector databaseConnector = new DtCardWorkDatabaseConnector(_card.WorkCollection.GetWork(index));
				databaseConnector.Save();
			};			
			return true;
        }
    }

	public class DtCardDisplay
    {
		private DtCard _card;

		public DtCardDisplay(DtCard card)
        {
			_card = card;
        }

		public void DisplayCardWorks(ListView list)
        {
			list.Items.Clear();
			foreach(DtCardWork work in _card.WorkCollection.Collection)
            {
				DtCardWorkDisplay display = new DtCardWorkDisplay(work);
				ListViewItem item = list.Items.Add("Автоматическое создание");
				display.ListViewItem(item);
            }
        }

		public DtCardWorkDisplay GetSelectedCardWorkDisplay(ListView list)
        {
			ListViewItem item = Db.GetItemSelected(list);
			DtCardWork cardWork = _card.WorkCollection.FindWork(item);
			if (cardWork == null) return null;
			DtCardWorkDisplay displey =  new DtCardWorkDisplay(cardWork);
			displey.SetConnectedLVItem(item);
			return displey;
		}
    }

	
}
