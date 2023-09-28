using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormCard.
	/// </summary>
	public class FormCard2 : System.Windows.Forms.Form
	{
		const string NO_VAT = "БЕЗ НДС";
		const string VAT_INCLUDED_20 = "НДС ВКЛЮЧЕН 20%";

		#region Объявление переменнных
		private TextBox textBoxTmp;
		private ListViewItem textBoxItem;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.ContextMenu contextMenu2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.Button button_print_invite;
        private MenuItem menuItem4;
		private Label label_cardWorksSumm;
		private Label label_cardWorkSummChange;
		private TabPage tabPage3;
		private ListView listView2;
		private ColumnHeader columnHeader7;
		private ColumnHeader columnHeader8;
		private ColumnHeader columnHeader9;
		private ColumnHeader columnHeader10;
		private ColumnHeader columnHeader11;
		private TabPage tabPage7;
		private Button button_remove_claim;
		private Button button_new_request;
		private ListView listView_request;
		private ColumnHeader columnHeader21;
		private ColumnHeader columnHeader22;
		private ColumnHeader columnHeader23;
		private ColumnHeader columnHeader24;
		private ColumnHeader columnHeader25;
		private ColumnHeader columnHeader26;
		private TabPage tabPage8;
		private Button button_new_catalogue_detail;
		private ListView listView5;
		private ColumnHeader columnHeader27;
		private ColumnHeader columnHeader28;
		private TabPage tabPage6;
		private Button buttonPropertyRecomend;
		private Button buttonNewRecomend;
		private ListView listView4;
		private ColumnHeader columnHeader20;
		private TabPage tabPage2;
		private Button button_worknopresent;
		private Button button_workpresent;
		private Button button_discount;
		private Button button_comment;
		private Button button_selection1;
		private Button buttonChangePrice;
		private TextBox textBoxSumWork;
		private Label label6;
		private Button button1;
		private Button buttonSetNoOil;
		private Button buttonSetOil;
		private Button buttonUnDone;
		private Button buttonDone;
		private Button buttonGuarantyOf;
		private Button buttonGarantyOn;
		private ListView listView1;
		private ColumnHeader columnHeader1;
		private ColumnHeader columnHeader2;
		private ColumnHeader columnHeader3;
		private ColumnHeader columnHeader4;
		private ColumnHeader columnHeader5;
		private ColumnHeader columnHeader6;
		private ColumnHeader columnHeader19;
		private Button buttonNewWork;
		private TabPage tabPage4;
		private Button button_discount_detail;
		private Button button_to;
		private Button button_unset_present;
		private Button button_set_present;
		private Button button_selection;
		private Button button3;
		private Button button2;
		private Label label11;
		private TextBox textBoxDetailSumm;
		private Button buttonSetNoCheck;
		private Button buttonSetNoOuter;
		private Button buttonSetCheck;
		private Button buttonSetOuter;
		private Button buttonSetPrice;
		private Button buttonGuarantyOff;
		private Button buttonGuarantyOn;
		private Button buttonUnreserve;
		private Button buttonReserve;
		private Button buttonDelDetail;
		private Button buttonNewStorage;
		private ListView listView3;
		private ColumnHeader columnHeader12;
		private ColumnHeader columnHeader13;
		private ColumnHeader columnHeader14;
		private ColumnHeader columnHeader15;
		private ColumnHeader columnHeader16;
		private ColumnHeader columnHeader17;
		private ColumnHeader columnHeader18;
		private TabPage tabPage1;
		private GroupBox groupBox_cardStateTime;
		private LinkLabel linkLabel_cardActionOpen;
		private Label label_cardState;
		private LinkLabel linkLabel_AgreedPickUpTime;
		private Button buttonWrite;
		private LinkLabel linkLabel_cardServiceManager;
		private Label lable_cardPayType;
		private Button button_close;
		private ComboBox comboBox_cardPayType;
		private LinkLabel linkLabel_cardComment;
		private LinkLabel linkLabel_cardWorkshop;
		private GroupBox groupBox1;
		private LinkLabel linkLabel_LicensePlate;
		private LinkLabel linkLabel_auto;
		private LinkLabel linkLabel_cardAutoRunHistory;
		private Button button_vinToClipboard;
		private LinkLabel linkLabel_cardRun;
		private LinkLabel linkLabel_autoType;
		private LinkLabel linkLabel_sellDate;
		private CheckBox checkBox_cardReturn;
		private GroupBox groupBox4;
		private LinkLabel linkLabel_defaultCalcDiscountDetail;
		private LinkLabel linkLabel_representativeContacts;
		private LinkLabel linkLabel_hourPrice;
		private LinkLabel linkLabel_representative;
		private LinkLabel linkLabel_representativeDocs;
		private LinkLabel linkLabel_ownerContacts;
		private LinkLabel linkLabel_defaultCalcDiscountWork;
		private Label label_defaultCalcLabel;
		private LinkLabel linkLabel_owner;
		private TabControl tabControl1;
		private LinkLabel linkLabel_cardActionClose;
		private LinkLabel linkLabel_cardActionWorkend;
		private ComboBox comboBox_typeOfVAT;
		private Label label_cardWorksDiscount;
		private Label label_cardWorksPay;
		private Label label_cardWorksDiscountChange;
		private Label label_cardWorksPayChange;
		private Label label_cardWorks;
		private Label label_cardWorksChange;
		private Label label_cardDetails;
		private Label label_cardDetailsSumm;
		private Label label_cardDetailsDiscount;
		private Label label_cardDetailsPay;
		#endregion

		private DbCard card = null;
		public DtCard the_card;
		// Связь со внешним миром.....
		ListView outer_list = null;
		ListViewItem outer_item = null;
		FormWorkSelect workSelect = null;
		private CalculatorCard _calculatorCard; // Калькулятор сумм работ и деталей
        private Button button_printWarrant;
        private readonly DisplayDtCard _displayerCard; // Для отображения - всех данных картоки в компоненты формы

		public FormCard2(DtCard cardSource) // Конструктор
		{
			InitializeComponent();
			comboBox_typeOfVAT.Items.Add(VAT_INCLUDED_20);
			comboBox_typeOfVAT.SelectedIndex = comboBox_typeOfVAT.Items.Add(NO_VAT);

			_calculatorCard = new CalculatorCard(CALCULATOR_TYPE.CALCULATOR_PAY, VAT_TYPE.VAT_NON, 0);    // Рассчетные параметры калькулятора

			if (cardSource == null)
			{
				the_card = new DtCard();
				card = new DbCard();
			}
			else
			{
				the_card = DbSqlCard.Find(cardSource);
				the_card.LoadCardWorksForce();
				the_card.LoadCardDetailsForce();
				card = DbCard.Find(the_card.Number, the_card.Year);
			}

			_calculatorCard.Calculate(the_card);
			_displayerCard = new DisplayDtCard(the_card);

			// Инициация значений элементов выбора
			FormDisplay.InitCombobox(comboBox_cardPayType, _displayerCard.CardPayTypeInit());
			FormDisplay.DisplayComponent(comboBox_cardPayType, _displayerCard.CardPayType());


			SetComponentAccessibility(); // Установка доступа к различным компонентам формы
			LoadCardWorks();    // Загрузка списка работ карточки
			LoadCardDetails();  // Загрузка списка деталей карточки
			LoadCardClaims();   // Загрузка списка заявок карточки
			LoadCardRecomends();   // Загрузка списка заявок карточки
			LoadCardDetailOrders(); // Загрузка заявок на запасные части

			//	ShowCardInfo();	// Отображение данных карточки
			//	ShowOwnerInfo(); // Отображение в карточке данных владельца
			//	ShowAutoInfo(); // Отображение в карточке данных автомобиля
			//	ShowRepresentativeInfo(); // Отображение в карточке данных представителя
			//	ShowServiceManagerInfo(); // Отображение в карточке данных сервис-консультанта
		}

		private void SetComponentAccessibility() // Установка доступности компонент формы для изменений
        {
			// Установка доступности компонентов в зависимости от статуса карточки и заказ наряда
			if(the_card.Closed)
            {
				// Заказ-няряд закрыт - все поля только для отображение
				linkLabel_auto.Enabled = false;
				linkLabel_owner.Enabled = false;
				linkLabel_ownerContacts.Enabled = false;
				linkLabel_defaultCalcDiscountWork.Enabled = false;
				linkLabel_cardRun.Enabled = false;
				linkLabel_sellDate.Enabled = false;
				linkLabel_representative.Enabled = false;
				linkLabel_hourPrice.Enabled = false;
				linkLabel_defaultCalcDiscountWork.Enabled = false;
				linkLabel_representativeContacts.Enabled = false;
				linkLabel_autoType.Enabled = false;
				linkLabel_representativeDocs.Enabled = false;
				linkLabel_cardWorkshop.Enabled = false;
				linkLabel_cardComment.Enabled = false;
				linkLabel_cardRun.Enabled = false;
				linkLabel_cardServiceManager.Enabled = false;
				linkLabel_defaultCalcDiscountDetail.Enabled = false;
				comboBox_cardPayType.Enabled = false;
				checkBox_cardReturn.Enabled = false;
				linkLabel_cardAutoRunHistory.Enabled = false;
				buttonWrite.Enabled = false;
				linkLabel_LicensePlate.Enabled = false;
				linkLabel_AgreedPickUpTime.Enabled = false;
				linkLabel_cardActionOpen.Enabled = false;
				linkLabel_cardActionClose.Enabled = false;
				linkLabel_cardActionWorkend.Enabled = false;
			}
			// Настройка разрешений для изменений
			
			if(the_card.Closed)//if (!card.CanUpdateWarrant)
			{
				buttonNewWork.Enabled = false;  // Запрет 
				buttonGarantyOn.Enabled = false;    //	нажатия
				buttonGuarantyOf.Enabled = false;   //		кнопок
				buttonDone.Enabled = false;
				buttonUnDone.Enabled = false;
				buttonNewStorage.Enabled = false;
				buttonDelDetail.Enabled = false;
				buttonReserve.Enabled = false;
				buttonUnreserve.Enabled = false;
				buttonGuarantyOn.Enabled = false;
				buttonGuarantyOff.Enabled = false;
				buttonSetPrice.Enabled = false;
				buttonSetOuter.Enabled = false;
				buttonSetCheck.Enabled = false;
				buttonSetNoOuter.Enabled = false;
				buttonSetNoCheck.Enabled = false;
				buttonNewRecomend.Enabled = false;
				buttonPropertyRecomend.Enabled = false;
				button_selection.Enabled = false;
				button_set_present.Enabled = false;
				button_unset_present.Enabled = false;
				button_discount_detail.Enabled = false;
				button_to.Enabled = false;
			}
		}
        #region Отображения данных карточки в компоненты формы
        private void ShowRepresentativeInfo() // Отображение данных Представителя форму
		{
			FormDisplay.DisplayComponent(linkLabel_representative, _displayerCard.Representative());
			FormDisplay.DisplayComponent(linkLabel_representativeContacts, _displayerCard.RepresentativeContact());
			FormDisplay.DisplayComponent(linkLabel_representativeDocs, _displayerCard.RepresentativeDocuments());		
		}
		private void ShowOwnerInfo() // Отображение данных Владельца в форму
		{
			FormDisplay.DisplayComponent(linkLabel_owner, _displayerCard.Owner());
			FormDisplay.DisplayComponent(linkLabel_ownerContacts, _displayerCard.OwnerContact());
		}
		private void ShowCalcDefault() // Отображение значений по умолчанию - СКИДКИ, СТОИМОСТЬ РАБОТ
		{
			// Расчетные параметры - скидки и значения по умолчанию
			FormDisplay.DisplayComponent(linkLabel_defaultCalcDiscountWork, _displayerCard.CardWorkDiscount());
			FormDisplay.DisplayComponent(linkLabel_defaultCalcDiscountDetail, _displayerCard.CardDetailDiscount());
			FormDisplay.DisplayComponent(linkLabel_hourPrice, _displayerCard.CardWorkHourPrice());
		}
		private void ShowServiceManagerInfo() // Отображение сервис-консультанта карточки
		{
			FormDisplay.DisplayComponent(linkLabel_cardServiceManager, _displayerCard.ServiceManager());
		}
		private void ShowCardInfo() // Отображение данных карточки
		{
			if (GetCardOrCloseForm() == null) return;	
			FormDisplay.DisplayComponent(this, _displayerCard.CardTitle());
			FormDisplay.DisplayComponent(linkLabel_autoType, _displayerCard.AutoType());
			FormDisplay.DisplayComponent(linkLabel_cardWorkshop, _displayerCard.CardWorkshop());
			FormDisplay.DisplayComponent(linkLabel_cardComment, _displayerCard.CardComment());
			FormDisplay.DisplayComponent(linkLabel_cardRun, _displayerCard.CardRun());
			FormDisplay.DisplayComponent(checkBox_cardReturn, _displayerCard.CardReturn());
			FormDisplay.DisplayComponent(linkLabel_LicensePlate, _displayerCard.CardLicensePlate());
			FormDisplay.DisplayComponent(linkLabel_AgreedPickUpTime, _displayerCard.CardAgreedPickupTime());
			FormDisplay.DisplayComponent(label_cardState, _displayerCard.CardState());
			FormDisplay.DisplayComponent(linkLabel_cardActionOpen, _displayerCard.CardActionOpen());
			FormDisplay.DisplayComponent(linkLabel_cardActionClose, _displayerCard.CardActionClose());
			FormDisplay.DisplayComponent(linkLabel_cardActionWorkend, _displayerCard.CardActionEndwork());
			ShowCalcDefault();	
		}
		#endregion
		#region Загрузчики
		private void FillWorkList()
		{
			GetWorkList().Items.Clear();
			if (the_card == null) return;
			if (the_card.CardWorks == null) return;
			if (the_card.CardWorks.Count == 0) return;
			ArrayList elements = new ArrayList();
			foreach (DtCardWork cardWork in the_card.CardWorks)
				elements.Add(new WfListViewFormCardWorkT01(cardWork, _calculatorCard.WorkCalculator));
			WindowsFormCommon.FillListView(GetWorkList(), elements);
		}
		private void LoadCardWorks() // Загрузка списка работ карточки
		{
			if (card == null) return;
			if (the_card == null) return;
			if (the_card.IsNew) return;

			the_card.LoadCardWorksForce();
			FillWorkList();
			return;

			listView1.Items.Clear();
			DbCardWork.FillList(listView1, the_card.CardWorks);
			//DbCardWork.FillList(listView1, card);
			UserInterface.SetListIndexes(listView1);
			MakeSumWork();
		}
		private void LoadCardDetails() // Загрузка списка деталей карточки
        {
			if (the_card == null) return;
			if (card == null) return;
			if (the_card.IsNew) return;
			// Пробуем загрузить список деталей карточки
			listView3.Items.Clear();
			DbCardDetail.FillList(listView3, card);
			UserInterface.SetListIndexes(listView3);
			//Выставляем сумму полученных деталей
			textBoxDetailSumm.Text = Db.CachToTxt(SetDetailSumm());
		}
		private void LoadCardClaims() // Загрузка списка заявок карточки
		{
			if (the_card == null) return;
			if (the_card.IsNew) return;
			GetClaimList().Items.Clear();
			// Пробуем загрузить список деталей карточки
			DbSqlCardClaim.SelectInListNoDatabase(GetClaimList(), the_card);
			//DbSqlCardClaim.SelectInList(listView_request, the_card.Number, the_card.Year);
		}
		private void LoadCardDetailOrders() // Загрузка списка заказа запчастей карточки
		{
			if (the_card == null) return;
			if (the_card.IsNew) return;
			// Пробуем загрузить список деталей карточки
			listView5.Items.Clear();
			DbSqlCardDetailOrder.SelectInList(listView5, the_card.Number, the_card.Year);
		}
		private void LoadCardRecomends() // Загрузка списка рекомендаций карточки
		{
			if (the_card == null) return;
			if (card == null) return;
			//if (the_card.IsNew) return;
			// Пробуем загрузить список деталей карточки
			listView4.Items.Clear();
			DbCardRecomend.FillList(listView4, card);
		}
        #endregion

        #region Работа с автомобилем
        private bool ExtendedCheckAuto(DtAuto dtAuto) // Показ дополнительной информации и проверок на автомобиле по коду
        {
			if (dtAuto == null) return false;
			long code_auto = dtAuto.Code;
			if (code_auto == 0) return false;
			DbAuto auto = DbAuto.Find(code_auto);

			DtAuto the_auto = dtAuto;
			if (the_auto == null) return false;


			if (DbSqlAutoComment.IsUnexe(code_auto))
				UserInterface.ListAutoComment(0, (object)code_auto, 0, UserInterface.ClickType.Modify);

			if (the_card.Workshop != null && the_card.Workshop.CodeWorkshop == 1)
			{
				ServiceHistory(code_auto, 1);
				// Проверка наличия ОБЯЗАТЕЛЬНЫХ примечаний
				if (DbSqlAutoComment.IsComments(code_auto) == true)
				{
					UserInterface.ListAutoComment(0, (object)code_auto, 0, UserInterface.ClickType.Modify);
				}
			}

			// Проверка гос-номера
			//string plate_number = (string)the_auto.GetData("НОМЕР_ЗНАК_НОМЕР");
			//string plate_region = (string)the_auto.GetData("НОМЕР_ЗНАК_РЕГИОН");
			DtLicensePlate licensePlate = the_auto.LicensePlate;
			string plate_number = licensePlate.Number;
			string plate_region = licensePlate.Region; ;
			if (plate_number == "" || plate_region == "")
			{
				UIF_LicencePlate dialog = new UIF_LicencePlate(auto.Code, auto.SignNo);
				if(dialog.ShowDialog() == DialogResult.OK)
                {
					// Сменили гос. номер автомобиля - нужно его установить в автомобиль
					dtAuto.LicensePlate = dialog.LicensePlate;
                }
			}
			// Будет проверка на предписания
			auto.DirectionReport();

			return true;
		}
		private void SetAuto(DtAuto srcAuto, DtCard wdtCard)    // Установка автомобиля в данные карточки
		{		
			wdtCard.DtAuto = srcAuto;
		}
		private void ShowAutoInfo() // Отображение данных автомобиля в форму
		{
			FormDisplay.DisplayComponent(linkLabel_auto, _displayerCard.Auto());
			FormDisplay.DisplayComponent(linkLabel_sellDate, _displayerCard.AutoSellDate());
			FormDisplay.DisplayComponent(linkLabel_cardAutoRunHistory, _displayerCard.AutoLastRun());
		}

		private void AutoSelectCheckSetShow(DtCard wdtCard) // Выбрать, проверить, установить и отобразить автомобиль в карточке
        {
			// Выбор автомобиля
			DtAuto dtAuto = (DtAuto)UserInterface.SelectAuto(); // Вызов процедуры интерфейса - выбор автомобиля
			if (dtAuto == null) return; // Если диалог вернул null - значит это отказ от обработки
			AutoCheckSetShow(dtAuto, wdtCard);
		}
		private void AutoCheckSetShow(DtAuto srcAuto, DtCard wdtCard) // Проверить, установить и отобразить автомобиль в карточке
		{
			if (!ExtendedCheckAuto(srcAuto)) return;// Сделать проверки
			SetAuto(srcAuto, wdtCard); // Сохранить значение
			ShowAutoInfo(); // Отобразить значение
			ShowCardInfo();
		}

		private void LinkLabel_auto_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) // Обработчик ссылки выбора автомобиля
		{
			AutoSelectCheckSetShow(the_card); // Выбор автомобиля
		}
		#endregion

	

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
		protected override void OnCreateControl()
		{
			// ПОСЛЕДОВАТЕЛЬНОСТЬ ДЕЙСТВИЙ ПРИ ОТКРЫТИИ НОВОЙ КАРТОЧКИ
			if (the_card.IsNew)
			{
				SelectWorkshop(); // Выбор подразделения новой карточки
			}
			//if(card.Number == 0)
			//{     
			//        // Выбор подразделения
			//        SelectWorkshop();	
			//}
			ShowCardInfo();	// Отображение данных карточки
			ShowOwnerInfo(); // Отображение в карточке данных владельца
			ShowAutoInfo(); // Отображение в карточке данных автомобиля
			ShowRepresentativeInfo(); // Отображение в карточке данных представителя
			ShowServiceManagerInfo(); // Отображение в карточке данных сервис-консультанта
							
			groupBox4.Focus();
			groupBox4.Select();
			groupBox4.SelectNextControl(groupBox4, true, true, true, true);
		}


		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCard2));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.button_new_request = new System.Windows.Forms.Button();
            this.button_new_catalogue_detail = new System.Windows.Forms.Button();
            this.buttonNewWork = new System.Windows.Forms.Button();
            this.buttonGarantyOn = new System.Windows.Forms.Button();
            this.buttonGuarantyOf = new System.Windows.Forms.Button();
            this.buttonDone = new System.Windows.Forms.Button();
            this.buttonUnDone = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button_selection1 = new System.Windows.Forms.Button();
            this.button_workpresent = new System.Windows.Forms.Button();
            this.buttonNewStorage = new System.Windows.Forms.Button();
            this.buttonDelDetail = new System.Windows.Forms.Button();
            this.buttonGuarantyOn = new System.Windows.Forms.Button();
            this.buttonGuarantyOff = new System.Windows.Forms.Button();
            this.buttonSetPrice = new System.Windows.Forms.Button();
            this.buttonSetOuter = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button_selection = new System.Windows.Forms.Button();
            this.button_set_present = new System.Windows.Forms.Button();
            this.button_unset_present = new System.Windows.Forms.Button();
            this.contextMenu1 = new System.Windows.Forms.ContextMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.contextMenu2 = new System.Windows.Forms.ContextMenu();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.button_print_invite = new System.Windows.Forms.Button();
            this.label_cardWorksSumm = new System.Windows.Forms.Label();
            this.label_cardWorkSummChange = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.listView2 = new System.Windows.Forms.ListView();
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.button_remove_claim = new System.Windows.Forms.Button();
            this.listView_request = new System.Windows.Forms.ListView();
            this.columnHeader21 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader22 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader23 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader24 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader25 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader26 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage8 = new System.Windows.Forms.TabPage();
            this.listView5 = new System.Windows.Forms.ListView();
            this.columnHeader27 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader28 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.buttonPropertyRecomend = new System.Windows.Forms.Button();
            this.buttonNewRecomend = new System.Windows.Forms.Button();
            this.listView4 = new System.Windows.Forms.ListView();
            this.columnHeader20 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button_worknopresent = new System.Windows.Forms.Button();
            this.button_discount = new System.Windows.Forms.Button();
            this.button_comment = new System.Windows.Forms.Button();
            this.buttonChangePrice = new System.Windows.Forms.Button();
            this.textBoxSumWork = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.buttonSetNoOil = new System.Windows.Forms.Button();
            this.buttonSetOil = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader19 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.button_discount_detail = new System.Windows.Forms.Button();
            this.button_to = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.textBoxDetailSumm = new System.Windows.Forms.TextBox();
            this.buttonSetNoCheck = new System.Windows.Forms.Button();
            this.buttonSetNoOuter = new System.Windows.Forms.Button();
            this.buttonSetCheck = new System.Windows.Forms.Button();
            this.buttonUnreserve = new System.Windows.Forms.Button();
            this.buttonReserve = new System.Windows.Forms.Button();
            this.listView3 = new System.Windows.Forms.ListView();
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader15 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader16 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader17 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader18 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox_cardStateTime = new System.Windows.Forms.GroupBox();
            this.linkLabel_cardActionWorkend = new System.Windows.Forms.LinkLabel();
            this.linkLabel_cardActionClose = new System.Windows.Forms.LinkLabel();
            this.linkLabel_cardActionOpen = new System.Windows.Forms.LinkLabel();
            this.label_cardState = new System.Windows.Forms.Label();
            this.linkLabel_AgreedPickUpTime = new System.Windows.Forms.LinkLabel();
            this.linkLabel_cardServiceManager = new System.Windows.Forms.LinkLabel();
            this.lable_cardPayType = new System.Windows.Forms.Label();
            this.comboBox_cardPayType = new System.Windows.Forms.ComboBox();
            this.linkLabel_cardComment = new System.Windows.Forms.LinkLabel();
            this.linkLabel_cardWorkshop = new System.Windows.Forms.LinkLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.linkLabel_LicensePlate = new System.Windows.Forms.LinkLabel();
            this.linkLabel_auto = new System.Windows.Forms.LinkLabel();
            this.linkLabel_cardAutoRunHistory = new System.Windows.Forms.LinkLabel();
            this.button_vinToClipboard = new System.Windows.Forms.Button();
            this.linkLabel_cardRun = new System.Windows.Forms.LinkLabel();
            this.linkLabel_autoType = new System.Windows.Forms.LinkLabel();
            this.linkLabel_sellDate = new System.Windows.Forms.LinkLabel();
            this.checkBox_cardReturn = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.comboBox_typeOfVAT = new System.Windows.Forms.ComboBox();
            this.linkLabel_defaultCalcDiscountDetail = new System.Windows.Forms.LinkLabel();
            this.linkLabel_representativeContacts = new System.Windows.Forms.LinkLabel();
            this.linkLabel_hourPrice = new System.Windows.Forms.LinkLabel();
            this.linkLabel_representative = new System.Windows.Forms.LinkLabel();
            this.linkLabel_representativeDocs = new System.Windows.Forms.LinkLabel();
            this.linkLabel_ownerContacts = new System.Windows.Forms.LinkLabel();
            this.linkLabel_defaultCalcDiscountWork = new System.Windows.Forms.LinkLabel();
            this.label_defaultCalcLabel = new System.Windows.Forms.Label();
            this.linkLabel_owner = new System.Windows.Forms.LinkLabel();
            this.button_close = new System.Windows.Forms.Button();
            this.buttonWrite = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.label_cardWorksDiscount = new System.Windows.Forms.Label();
            this.label_cardWorksPay = new System.Windows.Forms.Label();
            this.label_cardWorksDiscountChange = new System.Windows.Forms.Label();
            this.label_cardWorksPayChange = new System.Windows.Forms.Label();
            this.label_cardWorks = new System.Windows.Forms.Label();
            this.label_cardWorksChange = new System.Windows.Forms.Label();
            this.label_cardDetails = new System.Windows.Forms.Label();
            this.label_cardDetailsSumm = new System.Windows.Forms.Label();
            this.label_cardDetailsDiscount = new System.Windows.Forms.Label();
            this.label_cardDetailsPay = new System.Windows.Forms.Label();
            this.button_printWarrant = new System.Windows.Forms.Button();
            this.tabPage3.SuspendLayout();
            this.tabPage7.SuspendLayout();
            this.tabPage8.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox_cardStateTime.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_new_request
            // 
            this.button_new_request.Image = ((System.Drawing.Image)(resources.GetObject("button_new_request.Image")));
            this.button_new_request.Location = new System.Drawing.Point(10, 9);
            this.button_new_request.Name = "button_new_request";
            this.button_new_request.Size = new System.Drawing.Size(28, 27);
            this.button_new_request.TabIndex = 1;
            this.toolTip1.SetToolTip(this.button_new_request, "Управление заявками клиента");
            this.button_new_request.Click += new System.EventHandler(this.Button_new_request_Click);
            // 
            // button_new_catalogue_detail
            // 
            this.button_new_catalogue_detail.Image = ((System.Drawing.Image)(resources.GetObject("button_new_catalogue_detail.Image")));
            this.button_new_catalogue_detail.Location = new System.Drawing.Point(10, 9);
            this.button_new_catalogue_detail.Name = "button_new_catalogue_detail";
            this.button_new_catalogue_detail.Size = new System.Drawing.Size(28, 28);
            this.button_new_catalogue_detail.TabIndex = 1;
            this.toolTip1.SetToolTip(this.button_new_catalogue_detail, "Новая деталь из каталога");
            this.button_new_catalogue_detail.Click += new System.EventHandler(this.button_new_catalogue_detail_Click);
            // 
            // buttonNewWork
            // 
            this.buttonNewWork.Image = ((System.Drawing.Image)(resources.GetObject("buttonNewWork.Image")));
            this.buttonNewWork.Location = new System.Drawing.Point(10, 9);
            this.buttonNewWork.Name = "buttonNewWork";
            this.buttonNewWork.Size = new System.Drawing.Size(28, 27);
            this.buttonNewWork.TabIndex = 0;
            this.toolTip1.SetToolTip(this.buttonNewWork, "Новая работа");
            this.buttonNewWork.Click += new System.EventHandler(this.buttonNewWork_Click);
            // 
            // buttonGarantyOn
            // 
            this.buttonGarantyOn.Image = ((System.Drawing.Image)(resources.GetObject("buttonGarantyOn.Image")));
            this.buttonGarantyOn.Location = new System.Drawing.Point(38, 9);
            this.buttonGarantyOn.Name = "buttonGarantyOn";
            this.buttonGarantyOn.Size = new System.Drawing.Size(29, 27);
            this.buttonGarantyOn.TabIndex = 2;
            this.toolTip1.SetToolTip(this.buttonGarantyOn, "Отметить работы как гаранийные");
            this.buttonGarantyOn.Click += new System.EventHandler(this.ButtonGarantyOn_Click);
            // 
            // buttonGuarantyOf
            // 
            this.buttonGuarantyOf.Image = ((System.Drawing.Image)(resources.GetObject("buttonGuarantyOf.Image")));
            this.buttonGuarantyOf.Location = new System.Drawing.Point(67, 9);
            this.buttonGuarantyOf.Name = "buttonGuarantyOf";
            this.buttonGuarantyOf.Size = new System.Drawing.Size(29, 27);
            this.buttonGuarantyOf.TabIndex = 3;
            this.toolTip1.SetToolTip(this.buttonGuarantyOf, "Отметить работы как негарантийные");
            this.buttonGuarantyOf.Click += new System.EventHandler(this.ButtonGuarantyOf_Click);
            // 
            // buttonDone
            // 
            this.buttonDone.Image = ((System.Drawing.Image)(resources.GetObject("buttonDone.Image")));
            this.buttonDone.Location = new System.Drawing.Point(96, 9);
            this.buttonDone.Name = "buttonDone";
            this.buttonDone.Size = new System.Drawing.Size(29, 27);
            this.buttonDone.TabIndex = 4;
            this.toolTip1.SetToolTip(this.buttonDone, "Отметить выполнение работ");
            this.buttonDone.Click += new System.EventHandler(this.buttonDone_Click);
            // 
            // buttonUnDone
            // 
            this.buttonUnDone.Image = ((System.Drawing.Image)(resources.GetObject("buttonUnDone.Image")));
            this.buttonUnDone.Location = new System.Drawing.Point(125, 9);
            this.buttonUnDone.Name = "buttonUnDone";
            this.buttonUnDone.Size = new System.Drawing.Size(29, 27);
            this.buttonUnDone.TabIndex = 5;
            this.toolTip1.SetToolTip(this.buttonUnDone, "Отметить работы как невыполненные");
            this.buttonUnDone.Click += new System.EventHandler(this.buttonUnDone_Click);
            // 
            // button1
            // 
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(384, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(29, 27);
            this.button1.TabIndex = 8;
            this.toolTip1.SetToolTip(this.button1, "Новая форма добавления работ");
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // button_selection1
            // 
            this.button_selection1.Image = ((System.Drawing.Image)(resources.GetObject("button_selection1.Image")));
            this.button_selection1.Location = new System.Drawing.Point(749, 9);
            this.button_selection1.Name = "button_selection1";
            this.button_selection1.Size = new System.Drawing.Size(29, 27);
            this.button_selection1.TabIndex = 12;
            this.toolTip1.SetToolTip(this.button_selection1, "Подбор работ/деталей");
            this.button_selection1.Click += new System.EventHandler(this.button_selection1_Click);
            // 
            // button_workpresent
            // 
            this.button_workpresent.Location = new System.Drawing.Point(785, 9);
            this.button_workpresent.Name = "button_workpresent";
            this.button_workpresent.Size = new System.Drawing.Size(29, 27);
            this.button_workpresent.TabIndex = 15;
            this.button_workpresent.Text = "0";
            this.toolTip1.SetToolTip(this.button_workpresent, "Бонус");
            this.button_workpresent.UseVisualStyleBackColor = true;
            this.button_workpresent.Click += new System.EventHandler(this.Button_workpresent_Click);
            // 
            // buttonNewStorage
            // 
            this.buttonNewStorage.Image = ((System.Drawing.Image)(resources.GetObject("buttonNewStorage.Image")));
            this.buttonNewStorage.Location = new System.Drawing.Point(38, 9);
            this.buttonNewStorage.Name = "buttonNewStorage";
            this.buttonNewStorage.Size = new System.Drawing.Size(29, 27);
            this.buttonNewStorage.TabIndex = 2;
            this.toolTip1.SetToolTip(this.buttonNewStorage, "Новая складская позиция");
            this.buttonNewStorage.Click += new System.EventHandler(this.buttonNewStorage_Click);
            // 
            // buttonDelDetail
            // 
            this.buttonDelDetail.Image = ((System.Drawing.Image)(resources.GetObject("buttonDelDetail.Image")));
            this.buttonDelDetail.Location = new System.Drawing.Point(67, 9);
            this.buttonDelDetail.Name = "buttonDelDetail";
            this.buttonDelDetail.Size = new System.Drawing.Size(29, 27);
            this.buttonDelDetail.TabIndex = 3;
            this.toolTip1.SetToolTip(this.buttonDelDetail, "Удалить деталь");
            this.buttonDelDetail.Click += new System.EventHandler(this.buttonDelDetail_Click);
            // 
            // buttonGuarantyOn
            // 
            this.buttonGuarantyOn.Image = ((System.Drawing.Image)(resources.GetObject("buttonGuarantyOn.Image")));
            this.buttonGuarantyOn.Location = new System.Drawing.Point(134, 9);
            this.buttonGuarantyOn.Name = "buttonGuarantyOn";
            this.buttonGuarantyOn.Size = new System.Drawing.Size(29, 27);
            this.buttonGuarantyOn.TabIndex = 6;
            this.toolTip1.SetToolTip(this.buttonGuarantyOn, "Отметить детали как гаранийные");
            this.buttonGuarantyOn.Click += new System.EventHandler(this.buttonGuarantyOn_Click);
            // 
            // buttonGuarantyOff
            // 
            this.buttonGuarantyOff.Image = ((System.Drawing.Image)(resources.GetObject("buttonGuarantyOff.Image")));
            this.buttonGuarantyOff.Location = new System.Drawing.Point(163, 9);
            this.buttonGuarantyOff.Name = "buttonGuarantyOff";
            this.buttonGuarantyOff.Size = new System.Drawing.Size(29, 27);
            this.buttonGuarantyOff.TabIndex = 7;
            this.toolTip1.SetToolTip(this.buttonGuarantyOff, "Отметить детали как негарантийные");
            this.buttonGuarantyOff.Click += new System.EventHandler(this.buttonGuarantyOff_Click);
            // 
            // buttonSetPrice
            // 
            this.buttonSetPrice.Image = ((System.Drawing.Image)(resources.GetObject("buttonSetPrice.Image")));
            this.buttonSetPrice.Location = new System.Drawing.Point(365, 9);
            this.buttonSetPrice.Name = "buttonSetPrice";
            this.buttonSetPrice.Size = new System.Drawing.Size(29, 27);
            this.buttonSetPrice.TabIndex = 8;
            this.toolTip1.SetToolTip(this.buttonSetPrice, "Установить цену детали");
            this.buttonSetPrice.Click += new System.EventHandler(this.buttonSetPrice_Click);
            // 
            // buttonSetOuter
            // 
            this.buttonSetOuter.Image = ((System.Drawing.Image)(resources.GetObject("buttonSetOuter.Image")));
            this.buttonSetOuter.Location = new System.Drawing.Point(394, 9);
            this.buttonSetOuter.Name = "buttonSetOuter";
            this.buttonSetOuter.Size = new System.Drawing.Size(28, 27);
            this.buttonSetOuter.TabIndex = 9;
            this.toolTip1.SetToolTip(this.buttonSetOuter, "Отметить внешнюю деталь");
            this.buttonSetOuter.Click += new System.EventHandler(this.buttonSetOuter_Click);
            // 
            // button2
            // 
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.Location = new System.Drawing.Point(566, 9);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(29, 27);
            this.button2.TabIndex = 15;
            this.toolTip1.SetToolTip(this.button2, "Получить детали по списку");
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
            this.button3.Location = new System.Drawing.Point(595, 9);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(29, 27);
            this.button3.TabIndex = 16;
            this.toolTip1.SetToolTip(this.button3, "Возврат деталей по списку");
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button_selection
            // 
            this.button_selection.Image = ((System.Drawing.Image)(resources.GetObject("button_selection.Image")));
            this.button_selection.Location = new System.Drawing.Point(662, 9);
            this.button_selection.Name = "button_selection";
            this.button_selection.Size = new System.Drawing.Size(29, 27);
            this.button_selection.TabIndex = 17;
            this.toolTip1.SetToolTip(this.button_selection, "Подбор работ/деталей");
            this.button_selection.Click += new System.EventHandler(this.button_selection_Click);
            // 
            // button_set_present
            // 
            this.button_set_present.Location = new System.Drawing.Point(739, 9);
            this.button_set_present.Name = "button_set_present";
            this.button_set_present.Size = new System.Drawing.Size(29, 27);
            this.button_set_present.TabIndex = 18;
            this.button_set_present.Text = "0";
            this.toolTip1.SetToolTip(this.button_set_present, "Отметить как подарок");
            this.button_set_present.Click += new System.EventHandler(this.button_set_present_Click);
            // 
            // button_unset_present
            // 
            this.button_unset_present.Location = new System.Drawing.Point(768, 9);
            this.button_unset_present.Name = "button_unset_present";
            this.button_unset_present.Size = new System.Drawing.Size(29, 27);
            this.button_unset_present.TabIndex = 19;
            this.button_unset_present.Text = "$";
            this.toolTip1.SetToolTip(this.button_unset_present, "Отметить как деньги");
            this.button_unset_present.Click += new System.EventHandler(this.button_unset_present_Click);
            // 
            // contextMenu1
            // 
            this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem2});
            this.menuItem1.Text = "Деталь";
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 0;
            this.menuItem2.Text = "Заказать на склад";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // contextMenu2
            // 
            this.contextMenu2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem3,
            this.menuItem4});
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 0;
            this.menuItem3.Text = "Примечания";
            this.menuItem3.Click += new System.EventHandler(this.MenuItem3_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 1;
            this.menuItem4.Text = "Запчасти предоставленны клиентом";
            this.menuItem4.Click += new System.EventHandler(this.MenuItem4_Click);
            // 
            // button_print_invite
            // 
            this.button_print_invite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_print_invite.Location = new System.Drawing.Point(10, 776);
            this.button_print_invite.Name = "button_print_invite";
            this.button_print_invite.Size = new System.Drawing.Size(163, 26);
            this.button_print_invite.TabIndex = 4;
            this.button_print_invite.Text = "Печать приглашения";
            this.button_print_invite.Click += new System.EventHandler(this.Button_print_invite_Click);
            // 
            // label_cardWorksSumm
            // 
            this.label_cardWorksSumm.AutoSize = true;
            this.label_cardWorksSumm.Location = new System.Drawing.Point(195, 732);
            this.label_cardWorksSumm.Name = "label_cardWorksSumm";
            this.label_cardWorksSumm.Size = new System.Drawing.Size(120, 17);
            this.label_cardWorksSumm.TabIndex = 5;
            this.label_cardWorksSumm.Text = "РАБОТЫ - сумма";
            // 
            // label_cardWorkSummChange
            // 
            this.label_cardWorkSummChange.AutoSize = true;
            this.label_cardWorkSummChange.Location = new System.Drawing.Point(829, 732);
            this.label_cardWorkSummChange.Name = "label_cardWorkSummChange";
            this.label_cardWorkSummChange.Size = new System.Drawing.Size(204, 17);
            this.label_cardWorkSummChange.TabIndex = 6;
            this.label_cardWorkSummChange.Text = "РАБОТЫ - сумма измененная";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.listView2);
            this.tabPage3.Location = new System.Drawing.Point(4, 29);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1169, 653);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Дефекты";
            // 
            // listView2
            // 
            this.listView2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11});
            this.listView2.FullRowSelect = true;
            this.listView2.GridLines = true;
            this.listView2.HideSelection = false;
            this.listView2.Location = new System.Drawing.Point(10, 28);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(1148, 607);
            this.listView2.TabIndex = 0;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "№ п/п";
            this.columnHeader7.Width = 30;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Наименование дефекта";
            this.columnHeader8.Width = 200;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Код дефекта";
            this.columnHeader9.Width = 30;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Место Дефекта";
            this.columnHeader10.Width = 30;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "Описание дефекта";
            this.columnHeader11.Width = 200;
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.button_remove_claim);
            this.tabPage7.Controls.Add(this.button_new_request);
            this.tabPage7.Controls.Add(this.listView_request);
            this.tabPage7.Location = new System.Drawing.Point(4, 29);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Size = new System.Drawing.Size(1169, 653);
            this.tabPage7.TabIndex = 6;
            this.tabPage7.Text = "Заявка";
            // 
            // button_remove_claim
            // 
            this.button_remove_claim.Image = ((System.Drawing.Image)(resources.GetObject("button_remove_claim.Image")));
            this.button_remove_claim.Location = new System.Drawing.Point(38, 9);
            this.button_remove_claim.Name = "button_remove_claim";
            this.button_remove_claim.Size = new System.Drawing.Size(29, 27);
            this.button_remove_claim.TabIndex = 2;
            this.button_remove_claim.Click += new System.EventHandler(this.Button_remove_claim_Click);
            // 
            // listView_request
            // 
            this.listView_request.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView_request.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader21,
            this.columnHeader22,
            this.columnHeader23,
            this.columnHeader24,
            this.columnHeader25,
            this.columnHeader26});
            this.listView_request.FullRowSelect = true;
            this.listView_request.HideSelection = false;
            this.listView_request.Location = new System.Drawing.Point(10, 37);
            this.listView_request.Name = "listView_request";
            this.listView_request.Size = new System.Drawing.Size(1148, 321);
            this.listView_request.TabIndex = 0;
            this.listView_request.UseCompatibleStateImageBehavior = false;
            this.listView_request.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader21
            // 
            this.columnHeader21.Text = "Заявка";
            this.columnHeader21.Width = 250;
            // 
            // columnHeader22
            // 
            this.columnHeader22.Text = "Примечание";
            this.columnHeader22.Width = 200;
            // 
            // columnHeader23
            // 
            this.columnHeader23.Text = "Д";
            this.columnHeader23.Width = 30;
            // 
            // columnHeader24
            // 
            this.columnHeader24.Text = "П";
            this.columnHeader24.Width = 30;
            // 
            // columnHeader25
            // 
            this.columnHeader25.Text = "Г";
            this.columnHeader25.Width = 30;
            // 
            // columnHeader26
            // 
            this.columnHeader26.Text = "Ссогласование";
            this.columnHeader26.Width = 120;
            // 
            // tabPage8
            // 
            this.tabPage8.Controls.Add(this.button_new_catalogue_detail);
            this.tabPage8.Controls.Add(this.listView5);
            this.tabPage8.Location = new System.Drawing.Point(4, 29);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Size = new System.Drawing.Size(1169, 653);
            this.tabPage8.TabIndex = 7;
            this.tabPage8.Text = "Заявка на запчасти";
            // 
            // listView5
            // 
            this.listView5.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader27,
            this.columnHeader28});
            this.listView5.FullRowSelect = true;
            this.listView5.GridLines = true;
            this.listView5.HideSelection = false;
            this.listView5.Location = new System.Drawing.Point(10, 37);
            this.listView5.Name = "listView5";
            this.listView5.Size = new System.Drawing.Size(835, 535);
            this.listView5.TabIndex = 0;
            this.listView5.UseCompatibleStateImageBehavior = false;
            this.listView5.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader27
            // 
            this.columnHeader27.Text = "Заказанная деталь / складская позиция";
            this.columnHeader27.Width = 563;
            // 
            // columnHeader28
            // 
            this.columnHeader28.Text = "Гарантия";
            this.columnHeader28.Width = 78;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.buttonPropertyRecomend);
            this.tabPage6.Controls.Add(this.buttonNewRecomend);
            this.tabPage6.Controls.Add(this.listView4);
            this.tabPage6.Location = new System.Drawing.Point(4, 29);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new System.Drawing.Size(1169, 653);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "Рекомендации";
            // 
            // buttonPropertyRecomend
            // 
            this.buttonPropertyRecomend.Image = ((System.Drawing.Image)(resources.GetObject("buttonPropertyRecomend.Image")));
            this.buttonPropertyRecomend.Location = new System.Drawing.Point(38, 9);
            this.buttonPropertyRecomend.Name = "buttonPropertyRecomend";
            this.buttonPropertyRecomend.Size = new System.Drawing.Size(29, 27);
            this.buttonPropertyRecomend.TabIndex = 2;
            this.buttonPropertyRecomend.Click += new System.EventHandler(this.buttonPropertyRecomend_Click);
            // 
            // buttonNewRecomend
            // 
            this.buttonNewRecomend.Image = ((System.Drawing.Image)(resources.GetObject("buttonNewRecomend.Image")));
            this.buttonNewRecomend.Location = new System.Drawing.Point(10, 9);
            this.buttonNewRecomend.Name = "buttonNewRecomend";
            this.buttonNewRecomend.Size = new System.Drawing.Size(28, 27);
            this.buttonNewRecomend.TabIndex = 1;
            this.buttonNewRecomend.Click += new System.EventHandler(this.buttonNewRecomend_Click);
            // 
            // listView4
            // 
            this.listView4.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader20});
            this.listView4.FullRowSelect = true;
            this.listView4.GridLines = true;
            this.listView4.HideSelection = false;
            this.listView4.Location = new System.Drawing.Point(10, 37);
            this.listView4.Name = "listView4";
            this.listView4.Size = new System.Drawing.Size(768, 314);
            this.listView4.TabIndex = 0;
            this.listView4.UseCompatibleStateImageBehavior = false;
            this.listView4.View = System.Windows.Forms.View.Details;
            this.listView4.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listView4_KeyDown);
            // 
            // columnHeader20
            // 
            this.columnHeader20.Text = "Рекомендация";
            this.columnHeader20.Width = 300;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.button_worknopresent);
            this.tabPage2.Controls.Add(this.button_workpresent);
            this.tabPage2.Controls.Add(this.button_discount);
            this.tabPage2.Controls.Add(this.button_comment);
            this.tabPage2.Controls.Add(this.button_selection1);
            this.tabPage2.Controls.Add(this.buttonChangePrice);
            this.tabPage2.Controls.Add(this.textBoxSumWork);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Controls.Add(this.buttonSetNoOil);
            this.tabPage2.Controls.Add(this.buttonSetOil);
            this.tabPage2.Controls.Add(this.buttonUnDone);
            this.tabPage2.Controls.Add(this.buttonDone);
            this.tabPage2.Controls.Add(this.buttonGuarantyOf);
            this.tabPage2.Controls.Add(this.buttonGarantyOn);
            this.tabPage2.Controls.Add(this.listView1);
            this.tabPage2.Controls.Add(this.buttonNewWork);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(1169, 653);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Работы";
            // 
            // button_worknopresent
            // 
            this.button_worknopresent.Location = new System.Drawing.Point(821, 9);
            this.button_worknopresent.Name = "button_worknopresent";
            this.button_worknopresent.Size = new System.Drawing.Size(30, 27);
            this.button_worknopresent.TabIndex = 16;
            this.button_worknopresent.Text = "$";
            this.button_worknopresent.UseVisualStyleBackColor = true;
            this.button_worknopresent.Click += new System.EventHandler(this.Button_worknopresent_Click);
            // 
            // button_discount
            // 
            this.button_discount.Location = new System.Drawing.Point(694, 9);
            this.button_discount.Name = "button_discount";
            this.button_discount.Size = new System.Drawing.Size(33, 27);
            this.button_discount.TabIndex = 14;
            this.button_discount.Text = "%";
            this.button_discount.UseVisualStyleBackColor = true;
            this.button_discount.Click += new System.EventHandler(this.button_discount_Click);
            // 
            // button_comment
            // 
            this.button_comment.Location = new System.Drawing.Point(528, 9);
            this.button_comment.Name = "button_comment";
            this.button_comment.Size = new System.Drawing.Size(125, 27);
            this.button_comment.TabIndex = 13;
            this.button_comment.Text = "Примечание";
            this.button_comment.Click += new System.EventHandler(this.Button_comment_Click);
            // 
            // buttonChangePrice
            // 
            this.buttonChangePrice.Image = ((System.Drawing.Image)(resources.GetObject("buttonChangePrice.Image")));
            this.buttonChangePrice.Location = new System.Drawing.Point(413, 9);
            this.buttonChangePrice.Name = "buttonChangePrice";
            this.buttonChangePrice.Size = new System.Drawing.Size(29, 27);
            this.buttonChangePrice.TabIndex = 11;
            this.buttonChangePrice.Click += new System.EventHandler(this.buttonChangePrice_Click);
            // 
            // textBoxSumWork
            // 
            this.textBoxSumWork.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSumWork.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxSumWork.Location = new System.Drawing.Point(995, 615);
            this.textBoxSumWork.Name = "textBoxSumWork";
            this.textBoxSumWork.ReadOnly = true;
            this.textBoxSumWork.Size = new System.Drawing.Size(153, 26);
            this.textBoxSumWork.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(889, 615);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 28);
            this.label6.TabIndex = 9;
            this.label6.Text = "ИТОГО:";
            // 
            // buttonSetNoOil
            // 
            this.buttonSetNoOil.Image = ((System.Drawing.Image)(resources.GetObject("buttonSetNoOil.Image")));
            this.buttonSetNoOil.Location = new System.Drawing.Point(230, 9);
            this.buttonSetNoOil.Name = "buttonSetNoOil";
            this.buttonSetNoOil.Size = new System.Drawing.Size(29, 27);
            this.buttonSetNoOil.TabIndex = 7;
            this.buttonSetNoOil.Click += new System.EventHandler(this.buttonSetNoOil_Click);
            // 
            // buttonSetOil
            // 
            this.buttonSetOil.Image = ((System.Drawing.Image)(resources.GetObject("buttonSetOil.Image")));
            this.buttonSetOil.Location = new System.Drawing.Point(202, 9);
            this.buttonSetOil.Name = "buttonSetOil";
            this.buttonSetOil.Size = new System.Drawing.Size(28, 27);
            this.buttonSetOil.TabIndex = 6;
            this.buttonSetOil.Click += new System.EventHandler(this.buttonSetOil_Click);
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader19});
            this.listView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(10, 46);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(1148, 560);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SizeChanged += new System.EventHandler(this.ListView1_SizeChanged);
            this.listView1.DoubleClick += new System.EventHandler(this.ListView1_DoubleClick);
            this.listView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listView1_KeyDown);
            this.listView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseUp);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "№ п/п";
            this.columnHeader1.Width = 25;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Наименование";
            this.columnHeader2.Width = 180;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Поз. по прейск.";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Количество";
            this.columnHeader4.Width = 25;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Расценка";
            this.columnHeader5.Width = 80;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Сумма";
            this.columnHeader6.Width = 80;
            // 
            // columnHeader19
            // 
            this.columnHeader19.Text = "Выполнил";
            this.columnHeader19.Width = 120;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.button_discount_detail);
            this.tabPage4.Controls.Add(this.button_to);
            this.tabPage4.Controls.Add(this.button_unset_present);
            this.tabPage4.Controls.Add(this.button_set_present);
            this.tabPage4.Controls.Add(this.button_selection);
            this.tabPage4.Controls.Add(this.button3);
            this.tabPage4.Controls.Add(this.button2);
            this.tabPage4.Controls.Add(this.label11);
            this.tabPage4.Controls.Add(this.textBoxDetailSumm);
            this.tabPage4.Controls.Add(this.buttonSetNoCheck);
            this.tabPage4.Controls.Add(this.buttonSetNoOuter);
            this.tabPage4.Controls.Add(this.buttonSetCheck);
            this.tabPage4.Controls.Add(this.buttonSetOuter);
            this.tabPage4.Controls.Add(this.buttonSetPrice);
            this.tabPage4.Controls.Add(this.buttonGuarantyOff);
            this.tabPage4.Controls.Add(this.buttonGuarantyOn);
            this.tabPage4.Controls.Add(this.buttonUnreserve);
            this.tabPage4.Controls.Add(this.buttonReserve);
            this.tabPage4.Controls.Add(this.buttonDelDetail);
            this.tabPage4.Controls.Add(this.buttonNewStorage);
            this.tabPage4.Controls.Add(this.listView3);
            this.tabPage4.Location = new System.Drawing.Point(4, 29);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(1169, 653);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Детали";
            // 
            // button_discount_detail
            // 
            this.button_discount_detail.Location = new System.Drawing.Point(698, 9);
            this.button_discount_detail.Name = "button_discount_detail";
            this.button_discount_detail.Size = new System.Drawing.Size(34, 27);
            this.button_discount_detail.TabIndex = 21;
            this.button_discount_detail.Text = "%";
            this.button_discount_detail.UseVisualStyleBackColor = true;
            this.button_discount_detail.Click += new System.EventHandler(this.button_discount_detail_Click);
            // 
            // button_to
            // 
            this.button_to.Location = new System.Drawing.Point(792, 9);
            this.button_to.Name = "button_to";
            this.button_to.Size = new System.Drawing.Size(53, 27);
            this.button_to.TabIndex = 20;
            this.button_to.Text = "ТО";
            this.button_to.UseVisualStyleBackColor = true;
            this.button_to.Click += new System.EventHandler(this.button_to_Click);
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label11.Location = new System.Drawing.Point(899, 606);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(86, 28);
            this.label11.TabIndex = 14;
            this.label11.Text = "ИТОГО:";
            // 
            // textBoxDetailSumm
            // 
            this.textBoxDetailSumm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDetailSumm.Location = new System.Drawing.Point(1004, 606);
            this.textBoxDetailSumm.Name = "textBoxDetailSumm";
            this.textBoxDetailSumm.ReadOnly = true;
            this.textBoxDetailSumm.Size = new System.Drawing.Size(154, 26);
            this.textBoxDetailSumm.TabIndex = 13;
            // 
            // buttonSetNoCheck
            // 
            this.buttonSetNoCheck.Image = ((System.Drawing.Image)(resources.GetObject("buttonSetNoCheck.Image")));
            this.buttonSetNoCheck.Location = new System.Drawing.Point(480, 9);
            this.buttonSetNoCheck.Name = "buttonSetNoCheck";
            this.buttonSetNoCheck.Size = new System.Drawing.Size(29, 27);
            this.buttonSetNoCheck.TabIndex = 12;
            this.buttonSetNoCheck.Click += new System.EventHandler(this.buttonSetNoCheck_Click);
            // 
            // buttonSetNoOuter
            // 
            this.buttonSetNoOuter.Image = ((System.Drawing.Image)(resources.GetObject("buttonSetNoOuter.Image")));
            this.buttonSetNoOuter.Location = new System.Drawing.Point(451, 9);
            this.buttonSetNoOuter.Name = "buttonSetNoOuter";
            this.buttonSetNoOuter.Size = new System.Drawing.Size(29, 27);
            this.buttonSetNoOuter.TabIndex = 11;
            this.buttonSetNoOuter.Click += new System.EventHandler(this.buttonSetNoOuter_Click);
            // 
            // buttonSetCheck
            // 
            this.buttonSetCheck.Image = ((System.Drawing.Image)(resources.GetObject("buttonSetCheck.Image")));
            this.buttonSetCheck.Location = new System.Drawing.Point(422, 9);
            this.buttonSetCheck.Name = "buttonSetCheck";
            this.buttonSetCheck.Size = new System.Drawing.Size(29, 27);
            this.buttonSetCheck.TabIndex = 10;
            this.buttonSetCheck.Click += new System.EventHandler(this.buttonSetCheck_Click);
            // 
            // buttonUnreserve
            // 
            this.buttonUnreserve.Image = ((System.Drawing.Image)(resources.GetObject("buttonUnreserve.Image")));
            this.buttonUnreserve.Location = new System.Drawing.Point(269, 9);
            this.buttonUnreserve.Name = "buttonUnreserve";
            this.buttonUnreserve.Size = new System.Drawing.Size(29, 27);
            this.buttonUnreserve.TabIndex = 5;
            this.buttonUnreserve.Visible = false;
            this.buttonUnreserve.Click += new System.EventHandler(this.buttonUnreserve_Click);
            // 
            // buttonReserve
            // 
            this.buttonReserve.Image = ((System.Drawing.Image)(resources.GetObject("buttonReserve.Image")));
            this.buttonReserve.Location = new System.Drawing.Point(240, 9);
            this.buttonReserve.Name = "buttonReserve";
            this.buttonReserve.Size = new System.Drawing.Size(29, 27);
            this.buttonReserve.TabIndex = 4;
            this.buttonReserve.Visible = false;
            this.buttonReserve.Click += new System.EventHandler(this.buttonReserve_Click);
            // 
            // listView3
            // 
            this.listView3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView3.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader12,
            this.columnHeader13,
            this.columnHeader14,
            this.columnHeader15,
            this.columnHeader16,
            this.columnHeader17,
            this.columnHeader18});
            this.listView3.FullRowSelect = true;
            this.listView3.GridLines = true;
            this.listView3.HideSelection = false;
            this.listView3.Location = new System.Drawing.Point(10, 37);
            this.listView3.Name = "listView3";
            this.listView3.Size = new System.Drawing.Size(1148, 561);
            this.listView3.TabIndex = 0;
            this.listView3.UseCompatibleStateImageBehavior = false;
            this.listView3.View = System.Windows.Forms.View.Details;
            this.listView3.DoubleClick += new System.EventHandler(this.listView3_DoubleClick);
            this.listView3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listView3_KeyDown);
            this.listView3.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listView3_MouseUp);
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "№ п/п";
            this.columnHeader12.Width = 30;
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "Код";
            this.columnHeader13.Width = 90;
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "Наименование";
            this.columnHeader14.Width = 120;
            // 
            // columnHeader15
            // 
            this.columnHeader15.Text = "Количество";
            // 
            // columnHeader16
            // 
            this.columnHeader16.Text = "Цена";
            this.columnHeader16.Width = 90;
            // 
            // columnHeader17
            // 
            this.columnHeader17.Text = "Сумма";
            this.columnHeader17.Width = 90;
            // 
            // columnHeader18
            // 
            this.columnHeader18.Text = "$";
            this.columnHeader18.Width = 90;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox_cardStateTime);
            this.tabPage1.Controls.Add(this.linkLabel_cardServiceManager);
            this.tabPage1.Controls.Add(this.lable_cardPayType);
            this.tabPage1.Controls.Add(this.comboBox_cardPayType);
            this.tabPage1.Controls.Add(this.linkLabel_cardComment);
            this.tabPage1.Controls.Add(this.linkLabel_cardWorkshop);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.checkBox_cardReturn);
            this.tabPage1.Controls.Add(this.groupBox4);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(1169, 653);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Шапка";
            // 
            // groupBox_cardStateTime
            // 
            this.groupBox_cardStateTime.Controls.Add(this.linkLabel_cardActionWorkend);
            this.groupBox_cardStateTime.Controls.Add(this.linkLabel_cardActionClose);
            this.groupBox_cardStateTime.Controls.Add(this.linkLabel_cardActionOpen);
            this.groupBox_cardStateTime.Controls.Add(this.label_cardState);
            this.groupBox_cardStateTime.Controls.Add(this.linkLabel_AgreedPickUpTime);
            this.groupBox_cardStateTime.Location = new System.Drawing.Point(25, 516);
            this.groupBox_cardStateTime.Name = "groupBox_cardStateTime";
            this.groupBox_cardStateTime.Size = new System.Drawing.Size(905, 123);
            this.groupBox_cardStateTime.TabIndex = 55;
            this.groupBox_cardStateTime.TabStop = false;
            this.groupBox_cardStateTime.Text = "Статусы карточки";
            // 
            // linkLabel_cardActionWorkend
            // 
            this.linkLabel_cardActionWorkend.AutoSize = true;
            this.linkLabel_cardActionWorkend.Location = new System.Drawing.Point(519, 22);
            this.linkLabel_cardActionWorkend.Name = "linkLabel_cardActionWorkend";
            this.linkLabel_cardActionWorkend.Size = new System.Drawing.Size(287, 20);
            this.linkLabel_cardActionWorkend.TabIndex = 4;
            this.linkLabel_cardActionWorkend.TabStop = true;
            this.linkLabel_cardActionWorkend.Text = "КАРТОЧКА - Окончание ремонта";
            // 
            // linkLabel_cardActionClose
            // 
            this.linkLabel_cardActionClose.AutoSize = true;
            this.linkLabel_cardActionClose.Location = new System.Drawing.Point(10, 88);
            this.linkLabel_cardActionClose.Name = "linkLabel_cardActionClose";
            this.linkLabel_cardActionClose.Size = new System.Drawing.Size(201, 20);
            this.linkLabel_cardActionClose.TabIndex = 3;
            this.linkLabel_cardActionClose.TabStop = true;
            this.linkLabel_cardActionClose.Text = "КАРТОЧКА - Закрытие";
            this.linkLabel_cardActionClose.Click += new System.EventHandler(this.LinkLabel_cardActionClose_Click);
            // 
            // linkLabel_cardActionOpen
            // 
            this.linkLabel_cardActionOpen.AutoSize = true;
            this.linkLabel_cardActionOpen.Location = new System.Drawing.Point(10, 56);
            this.linkLabel_cardActionOpen.Name = "linkLabel_cardActionOpen";
            this.linkLabel_cardActionOpen.Size = new System.Drawing.Size(202, 20);
            this.linkLabel_cardActionOpen.TabIndex = 2;
            this.linkLabel_cardActionOpen.TabStop = true;
            this.linkLabel_cardActionOpen.Text = "КАРТОЧКА - Открытие";
            this.linkLabel_cardActionOpen.Click += new System.EventHandler(this.LinkLabel_cardActionOpen_Click);
            // 
            // label_cardState
            // 
            this.label_cardState.AutoSize = true;
            this.label_cardState.Location = new System.Drawing.Point(6, 22);
            this.label_cardState.Name = "label_cardState";
            this.label_cardState.Size = new System.Drawing.Size(174, 20);
            this.label_cardState.TabIndex = 1;
            this.label_cardState.Text = "КАРТОЧКА - статус";
            // 
            // linkLabel_AgreedPickUpTime
            // 
            this.linkLabel_AgreedPickUpTime.AutoSize = true;
            this.linkLabel_AgreedPickUpTime.Location = new System.Drawing.Point(519, 88);
            this.linkLabel_AgreedPickUpTime.Name = "linkLabel_AgreedPickUpTime";
            this.linkLabel_AgreedPickUpTime.Size = new System.Drawing.Size(370, 20);
            this.linkLabel_AgreedPickUpTime.TabIndex = 0;
            this.linkLabel_AgreedPickUpTime.TabStop = true;
            this.linkLabel_AgreedPickUpTime.Text = "КАРТОЧКА - согласованное время выдачи";
            this.linkLabel_AgreedPickUpTime.Click += new System.EventHandler(this.LinkLabel_AgreedPickUpTime_Click);
            // 
            // linkLabel_cardServiceManager
            // 
            this.linkLabel_cardServiceManager.AutoSize = true;
            this.linkLabel_cardServiceManager.Location = new System.Drawing.Point(869, 404);
            this.linkLabel_cardServiceManager.Name = "linkLabel_cardServiceManager";
            this.linkLabel_cardServiceManager.Size = new System.Drawing.Size(290, 20);
            this.linkLabel_cardServiceManager.TabIndex = 53;
            this.linkLabel_cardServiceManager.TabStop = true;
            this.linkLabel_cardServiceManager.Text = "КАРТОЧКА - Сервис консультант";
            this.linkLabel_cardServiceManager.Click += new System.EventHandler(this.LinkLabel_cardServiceManager_Click);
            // 
            // lable_cardPayType
            // 
            this.lable_cardPayType.AutoSize = true;
            this.lable_cardPayType.Location = new System.Drawing.Point(24, 414);
            this.lable_cardPayType.Name = "lable_cardPayType";
            this.lable_cardPayType.Size = new System.Drawing.Size(110, 20);
            this.lable_cardPayType.TabIndex = 52;
            this.lable_cardPayType.Text = "Вид оплаты";
            // 
            // comboBox_cardPayType
            // 
            this.comboBox_cardPayType.FormattingEnabled = true;
            this.comboBox_cardPayType.Location = new System.Drawing.Point(28, 437);
            this.comboBox_cardPayType.Name = "comboBox_cardPayType";
            this.comboBox_cardPayType.Size = new System.Drawing.Size(365, 28);
            this.comboBox_cardPayType.TabIndex = 51;
            this.comboBox_cardPayType.SelectedValueChanged += new System.EventHandler(this.ComboBox_cardPayType_SelectedValueChanged);
            // 
            // linkLabel_cardComment
            // 
            this.linkLabel_cardComment.AutoSize = true;
            this.linkLabel_cardComment.Location = new System.Drawing.Point(20, 374);
            this.linkLabel_cardComment.Name = "linkLabel_cardComment";
            this.linkLabel_cardComment.Size = new System.Drawing.Size(223, 20);
            this.linkLabel_cardComment.TabIndex = 50;
            this.linkLabel_cardComment.TabStop = true;
            this.linkLabel_cardComment.Text = "КАРТОЧКА - Примечание";
            this.linkLabel_cardComment.Click += new System.EventHandler(this.LinkLabel_cardComment_Click);
            // 
            // linkLabel_cardWorkshop
            // 
            this.linkLabel_cardWorkshop.AutoSize = true;
            this.linkLabel_cardWorkshop.Location = new System.Drawing.Point(868, 374);
            this.linkLabel_cardWorkshop.Name = "linkLabel_cardWorkshop";
            this.linkLabel_cardWorkshop.Size = new System.Drawing.Size(254, 20);
            this.linkLabel_cardWorkshop.TabIndex = 49;
            this.linkLabel_cardWorkshop.TabStop = true;
            this.linkLabel_cardWorkshop.Text = "КАРТОЧКА - Подразделение";
            this.linkLabel_cardWorkshop.Click += new System.EventHandler(this.LinkLabel_cardWorkshop_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.linkLabel_LicensePlate);
            this.groupBox1.Controls.Add(this.linkLabel_auto);
            this.groupBox1.Controls.Add(this.linkLabel_cardAutoRunHistory);
            this.groupBox1.Controls.Add(this.button_vinToClipboard);
            this.groupBox1.Controls.Add(this.linkLabel_cardRun);
            this.groupBox1.Controls.Add(this.linkLabel_autoType);
            this.groupBox1.Controls.Add(this.linkLabel_sellDate);
            this.groupBox1.Location = new System.Drawing.Point(10, 201);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1149, 162);
            this.groupBox1.TabIndex = 48;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Автомобиль";
            // 
            // linkLabel_LicensePlate
            // 
            this.linkLabel_LicensePlate.AutoSize = true;
            this.linkLabel_LicensePlate.Location = new System.Drawing.Point(438, 22);
            this.linkLabel_LicensePlate.Name = "linkLabel_LicensePlate";
            this.linkLabel_LicensePlate.Size = new System.Drawing.Size(161, 20);
            this.linkLabel_LicensePlate.TabIndex = 48;
            this.linkLabel_LicensePlate.TabStop = true;
            this.linkLabel_LicensePlate.Text = "АВТО - Гос Номер";
            this.linkLabel_LicensePlate.Click += new System.EventHandler(this.LinkLabel_LicensePlate_Click);
            // 
            // linkLabel_auto
            // 
            this.linkLabel_auto.AutoSize = true;
            this.linkLabel_auto.Location = new System.Drawing.Point(11, 22);
            this.linkLabel_auto.Name = "linkLabel_auto";
            this.linkLabel_auto.Size = new System.Drawing.Size(170, 20);
            this.linkLabel_auto.TabIndex = 42;
            this.linkLabel_auto.TabStop = true;
            this.linkLabel_auto.Text = "АВТО - Модель VIN";
            this.linkLabel_auto.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel_auto_LinkClicked);
            // 
            // linkLabel_cardAutoRunHistory
            // 
            this.linkLabel_cardAutoRunHistory.AutoSize = true;
            this.linkLabel_cardAutoRunHistory.Location = new System.Drawing.Point(858, 115);
            this.linkLabel_cardAutoRunHistory.Name = "linkLabel_cardAutoRunHistory";
            this.linkLabel_cardAutoRunHistory.Size = new System.Drawing.Size(214, 20);
            this.linkLabel_cardAutoRunHistory.TabIndex = 47;
            this.linkLabel_cardAutoRunHistory.TabStop = true;
            this.linkLabel_cardAutoRunHistory.Text = "АВТОМОБИЛЬ - история";
            // 
            // button_vinToClipboard
            // 
            this.button_vinToClipboard.Location = new System.Drawing.Point(15, 99);
            this.button_vinToClipboard.Name = "button_vinToClipboard";
            this.button_vinToClipboard.Size = new System.Drawing.Size(134, 57);
            this.button_vinToClipboard.TabIndex = 46;
            this.button_vinToClipboard.Text = "Копировать VIN";
            this.button_vinToClipboard.UseVisualStyleBackColor = true;
            this.button_vinToClipboard.Click += new System.EventHandler(this.Button_vinToClipboard_Click);
            // 
            // linkLabel_cardRun
            // 
            this.linkLabel_cardRun.AutoSize = true;
            this.linkLabel_cardRun.Location = new System.Drawing.Point(859, 22);
            this.linkLabel_cardRun.Name = "linkLabel_cardRun";
            this.linkLabel_cardRun.Size = new System.Drawing.Size(168, 20);
            this.linkLabel_cardRun.TabIndex = 44;
            this.linkLabel_cardRun.TabStop = true;
            this.linkLabel_cardRun.Text = "КАРТОЧКА Пробег";
            this.linkLabel_cardRun.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.linkLabel_cardRun.Click += new System.EventHandler(this.LinkLabel_cardRun_Click);
            // 
            // linkLabel_autoType
            // 
            this.linkLabel_autoType.AutoSize = true;
            this.linkLabel_autoType.Location = new System.Drawing.Point(444, 53);
            this.linkLabel_autoType.Name = "linkLabel_autoType";
            this.linkLabel_autoType.Size = new System.Drawing.Size(388, 20);
            this.linkLabel_autoType.TabIndex = 45;
            this.linkLabel_autoType.TabStop = true;
            this.linkLabel_autoType.Text = "АВТОМОБИЛЬ - трудоемкость по умолчанию";
            this.linkLabel_autoType.Click += new System.EventHandler(this.LinkLabel_autoType_Click);
            // 
            // linkLabel_sellDate
            // 
            this.linkLabel_sellDate.AutoSize = true;
            this.linkLabel_sellDate.Location = new System.Drawing.Point(14, 53);
            this.linkLabel_sellDate.Name = "linkLabel_sellDate";
            this.linkLabel_sellDate.Size = new System.Drawing.Size(194, 20);
            this.linkLabel_sellDate.TabIndex = 43;
            this.linkLabel_sellDate.TabStop = true;
            this.linkLabel_sellDate.Text = "АВТО - Дата продажи";
            // 
            // checkBox_cardReturn
            // 
            this.checkBox_cardReturn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBox_cardReturn.ForeColor = System.Drawing.Color.OrangeRed;
            this.checkBox_cardReturn.Location = new System.Drawing.Point(28, 482);
            this.checkBox_cardReturn.Name = "checkBox_cardReturn";
            this.checkBox_cardReturn.Size = new System.Drawing.Size(134, 28);
            this.checkBox_cardReturn.TabIndex = 30;
            this.checkBox_cardReturn.Text = "ВОЗВРАТ!!!!";
            this.checkBox_cardReturn.CheckedChanged += new System.EventHandler(this.CheckBox_cardReturn_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.comboBox_typeOfVAT);
            this.groupBox4.Controls.Add(this.linkLabel_defaultCalcDiscountDetail);
            this.groupBox4.Controls.Add(this.linkLabel_representativeContacts);
            this.groupBox4.Controls.Add(this.linkLabel_hourPrice);
            this.groupBox4.Controls.Add(this.linkLabel_representative);
            this.groupBox4.Controls.Add(this.linkLabel_representativeDocs);
            this.groupBox4.Controls.Add(this.linkLabel_ownerContacts);
            this.groupBox4.Controls.Add(this.linkLabel_defaultCalcDiscountWork);
            this.groupBox4.Controls.Add(this.label_defaultCalcLabel);
            this.groupBox4.Controls.Add(this.linkLabel_owner);
            this.groupBox4.Location = new System.Drawing.Point(10, 9);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(1149, 186);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Владелец";
            // 
            // comboBox_typeOfVAT
            // 
            this.comboBox_typeOfVAT.FormattingEnabled = true;
            this.comboBox_typeOfVAT.Location = new System.Drawing.Point(863, 137);
            this.comboBox_typeOfVAT.Name = "comboBox_typeOfVAT";
            this.comboBox_typeOfVAT.Size = new System.Drawing.Size(266, 28);
            this.comboBox_typeOfVAT.TabIndex = 39;
            this.comboBox_typeOfVAT.SelectedValueChanged += new System.EventHandler(this.ComboBox_typeOfVAT_SelectedValueChanged);
            // 
            // linkLabel_defaultCalcDiscountDetail
            // 
            this.linkLabel_defaultCalcDiscountDetail.AutoSize = true;
            this.linkLabel_defaultCalcDiscountDetail.Location = new System.Drawing.Point(953, 71);
            this.linkLabel_defaultCalcDiscountDetail.Name = "linkLabel_defaultCalcDiscountDetail";
            this.linkLabel_defaultCalcDiscountDetail.Size = new System.Drawing.Size(176, 20);
            this.linkLabel_defaultCalcDiscountDetail.TabIndex = 38;
            this.linkLabel_defaultCalcDiscountDetail.TabStop = true;
            this.linkLabel_defaultCalcDiscountDetail.Text = "СКИДКА - Запчасти";
            this.linkLabel_defaultCalcDiscountDetail.Click += new System.EventHandler(this.LinkLabel_defaultCardDiscountDetail_Click);
            // 
            // linkLabel_representativeContacts
            // 
            this.linkLabel_representativeContacts.AutoSize = true;
            this.linkLabel_representativeContacts.Location = new System.Drawing.Point(6, 114);
            this.linkLabel_representativeContacts.Name = "linkLabel_representativeContacts";
            this.linkLabel_representativeContacts.Size = new System.Drawing.Size(258, 20);
            this.linkLabel_representativeContacts.TabIndex = 36;
            this.linkLabel_representativeContacts.TabStop = true;
            this.linkLabel_representativeContacts.Text = "ПРЕДСТАВИТЕЛЬ - Контакты";
            this.linkLabel_representativeContacts.Click += new System.EventHandler(this.LinkLabel_representativeContacts_Click);
            // 
            // linkLabel_hourPrice
            // 
            this.linkLabel_hourPrice.AutoSize = true;
            this.linkLabel_hourPrice.Location = new System.Drawing.Point(953, 101);
            this.linkLabel_hourPrice.Name = "linkLabel_hourPrice";
            this.linkLabel_hourPrice.Size = new System.Drawing.Size(151, 20);
            this.linkLabel_hourPrice.TabIndex = 35;
            this.linkLabel_hourPrice.TabStop = true;
            this.linkLabel_hourPrice.Text = "СТОИМОСТЬ Н/Ч";
            this.linkLabel_hourPrice.Click += new System.EventHandler(this.LinkLabel_hourPrice_Click);
            // 
            // linkLabel_representative
            // 
            this.linkLabel_representative.AutoSize = true;
            this.linkLabel_representative.Location = new System.Drawing.Point(6, 82);
            this.linkLabel_representative.Name = "linkLabel_representative";
            this.linkLabel_representative.Size = new System.Drawing.Size(301, 20);
            this.linkLabel_representative.TabIndex = 34;
            this.linkLabel_representative.TabStop = true;
            this.linkLabel_representative.Text = "ПРЕДСТАВИТЕЛЬ - Наименование";
            this.linkLabel_representative.Click += new System.EventHandler(this.LinkLabel_representative_Click);
            // 
            // linkLabel_representativeDocs
            // 
            this.linkLabel_representativeDocs.AutoSize = true;
            this.linkLabel_representativeDocs.Location = new System.Drawing.Point(10, 148);
            this.linkLabel_representativeDocs.Name = "linkLabel_representativeDocs";
            this.linkLabel_representativeDocs.Size = new System.Drawing.Size(258, 20);
            this.linkLabel_representativeDocs.TabIndex = 37;
            this.linkLabel_representativeDocs.TabStop = true;
            this.linkLabel_representativeDocs.Text = "ПРЕДСТАВИТЕЛЬ - Документ";
            this.linkLabel_representativeDocs.Click += new System.EventHandler(this.LinkLabel_representativeDocs_Click);
            // 
            // linkLabel_ownerContacts
            // 
            this.linkLabel_ownerContacts.AutoSize = true;
            this.linkLabel_ownerContacts.Location = new System.Drawing.Point(6, 53);
            this.linkLabel_ownerContacts.Name = "linkLabel_ownerContacts";
            this.linkLabel_ownerContacts.Size = new System.Drawing.Size(205, 20);
            this.linkLabel_ownerContacts.TabIndex = 33;
            this.linkLabel_ownerContacts.TabStop = true;
            this.linkLabel_ownerContacts.Text = "ВЛАДЕЛЕЦ - Контакты";
            this.linkLabel_ownerContacts.Click += new System.EventHandler(this.LinkLabel_ownerContacts_Click);
            // 
            // linkLabel_defaultCalcDiscountWork
            // 
            this.linkLabel_defaultCalcDiscountWork.AutoSize = true;
            this.linkLabel_defaultCalcDiscountWork.Location = new System.Drawing.Point(953, 39);
            this.linkLabel_defaultCalcDiscountWork.Name = "linkLabel_defaultCalcDiscountWork";
            this.linkLabel_defaultCalcDiscountWork.Size = new System.Drawing.Size(158, 20);
            this.linkLabel_defaultCalcDiscountWork.TabIndex = 32;
            this.linkLabel_defaultCalcDiscountWork.TabStop = true;
            this.linkLabel_defaultCalcDiscountWork.Text = "СКИДКА - Работы";
            this.linkLabel_defaultCalcDiscountWork.Click += new System.EventHandler(this.LinkLabel_defaultCalcDiscountWork_Click);
            // 
            // label_defaultCalcLabel
            // 
            this.label_defaultCalcLabel.AutoSize = true;
            this.label_defaultCalcLabel.Location = new System.Drawing.Point(953, 10);
            this.label_defaultCalcLabel.Name = "label_defaultCalcLabel";
            this.label_defaultCalcLabel.Size = new System.Drawing.Size(130, 20);
            this.label_defaultCalcLabel.TabIndex = 31;
            this.label_defaultCalcLabel.Text = "По умолчанию";
            // 
            // linkLabel_owner
            // 
            this.linkLabel_owner.AutoSize = true;
            this.linkLabel_owner.DisabledLinkColor = System.Drawing.Color.DarkGray;
            this.linkLabel_owner.Location = new System.Drawing.Point(6, 22);
            this.linkLabel_owner.Name = "linkLabel_owner";
            this.linkLabel_owner.Size = new System.Drawing.Size(248, 20);
            this.linkLabel_owner.TabIndex = 30;
            this.linkLabel_owner.TabStop = true;
            this.linkLabel_owner.Text = "ВЛАДЕЛЕЦ - Наименование";
            this.linkLabel_owner.Click += new System.EventHandler(this.LinkLabel_owner_Click);
            // 
            // button_close
            // 
            this.button_close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_close.Location = new System.Drawing.Point(1189, 111);
            this.button_close.Name = "button_close";
            this.button_close.Size = new System.Drawing.Size(111, 43);
            this.button_close.TabIndex = 2;
            this.button_close.Text = "Закрыть";
            this.button_close.Click += new System.EventHandler(this.Button_close_Click);
            // 
            // buttonWrite
            // 
            this.buttonWrite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonWrite.Location = new System.Drawing.Point(1189, 164);
            this.buttonWrite.Name = "buttonWrite";
            this.buttonWrite.Size = new System.Drawing.Size(111, 49);
            this.buttonWrite.TabIndex = 54;
            this.buttonWrite.Text = "Записать";
            this.buttonWrite.UseVisualStyleBackColor = true;
            this.buttonWrite.Click += new System.EventHandler(this.ButtonWrite_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Controls.Add(this.tabPage8);
            this.tabControl1.Controls.Add(this.tabPage7);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tabControl1.Location = new System.Drawing.Point(10, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1177, 686);
            this.tabControl1.TabIndex = 0;
            // 
            // label_cardWorksDiscount
            // 
            this.label_cardWorksDiscount.AutoSize = true;
            this.label_cardWorksDiscount.Location = new System.Drawing.Point(195, 754);
            this.label_cardWorksDiscount.Name = "label_cardWorksDiscount";
            this.label_cardWorksDiscount.Size = new System.Drawing.Size(125, 17);
            this.label_cardWorksDiscount.TabIndex = 55;
            this.label_cardWorksDiscount.Text = "РАБОТЫ - скидка";
            // 
            // label_cardWorksPay
            // 
            this.label_cardWorksPay.AutoSize = true;
            this.label_cardWorksPay.Location = new System.Drawing.Point(195, 776);
            this.label_cardWorksPay.Name = "label_cardWorksPay";
            this.label_cardWorksPay.Size = new System.Drawing.Size(138, 17);
            this.label_cardWorksPay.TabIndex = 56;
            this.label_cardWorksPay.Text = "РАБОТЫ - к оплате";
            // 
            // label_cardWorksDiscountChange
            // 
            this.label_cardWorksDiscountChange.AutoSize = true;
            this.label_cardWorksDiscountChange.Location = new System.Drawing.Point(829, 754);
            this.label_cardWorksDiscountChange.Name = "label_cardWorksDiscountChange";
            this.label_cardWorksDiscountChange.Size = new System.Drawing.Size(209, 17);
            this.label_cardWorksDiscountChange.TabIndex = 57;
            this.label_cardWorksDiscountChange.Text = "РАБОТЫ - скидка измененная";
            // 
            // label_cardWorksPayChange
            // 
            this.label_cardWorksPayChange.AutoSize = true;
            this.label_cardWorksPayChange.Location = new System.Drawing.Point(829, 776);
            this.label_cardWorksPayChange.Name = "label_cardWorksPayChange";
            this.label_cardWorksPayChange.Size = new System.Drawing.Size(222, 17);
            this.label_cardWorksPayChange.TabIndex = 58;
            this.label_cardWorksPayChange.Text = "РАБОТЫ - к оплате измененная";
            // 
            // label_cardWorks
            // 
            this.label_cardWorks.AutoSize = true;
            this.label_cardWorks.Location = new System.Drawing.Point(195, 701);
            this.label_cardWorks.Name = "label_cardWorks";
            this.label_cardWorks.Size = new System.Drawing.Size(67, 17);
            this.label_cardWorks.TabIndex = 59;
            this.label_cardWorks.Text = "РАБОТЫ";
            // 
            // label_cardWorksChange
            // 
            this.label_cardWorksChange.AutoSize = true;
            this.label_cardWorksChange.Location = new System.Drawing.Point(829, 701);
            this.label_cardWorksChange.Name = "label_cardWorksChange";
            this.label_cardWorksChange.Size = new System.Drawing.Size(159, 17);
            this.label_cardWorksChange.TabIndex = 60;
            this.label_cardWorksChange.Text = "РАБОТЫ ИЗМЕНЕНИЯ";
            // 
            // label_cardDetails
            // 
            this.label_cardDetails.AutoSize = true;
            this.label_cardDetails.Location = new System.Drawing.Point(399, 701);
            this.label_cardDetails.Name = "label_cardDetails";
            this.label_cardDetails.Size = new System.Drawing.Size(189, 17);
            this.label_cardDetails.TabIndex = 61;
            this.label_cardDetails.Text = "ЗАПЧАСТИ И МАТЕРИАЛЫ";
            // 
            // label_cardDetailsSumm
            // 
            this.label_cardDetailsSumm.AutoSize = true;
            this.label_cardDetailsSumm.Location = new System.Drawing.Point(402, 732);
            this.label_cardDetailsSumm.Name = "label_cardDetailsSumm";
            this.label_cardDetailsSumm.Size = new System.Drawing.Size(136, 17);
            this.label_cardDetailsSumm.TabIndex = 62;
            this.label_cardDetailsSumm.Text = "ЗАПЧАСТИ - сумма";
            // 
            // label_cardDetailsDiscount
            // 
            this.label_cardDetailsDiscount.AutoSize = true;
            this.label_cardDetailsDiscount.Location = new System.Drawing.Point(402, 754);
            this.label_cardDetailsDiscount.Name = "label_cardDetailsDiscount";
            this.label_cardDetailsDiscount.Size = new System.Drawing.Size(141, 17);
            this.label_cardDetailsDiscount.TabIndex = 63;
            this.label_cardDetailsDiscount.Text = "ЗАПЧАСТИ - скидка";
            // 
            // label_cardDetailsPay
            // 
            this.label_cardDetailsPay.AutoSize = true;
            this.label_cardDetailsPay.Location = new System.Drawing.Point(402, 776);
            this.label_cardDetailsPay.Name = "label_cardDetailsPay";
            this.label_cardDetailsPay.Size = new System.Drawing.Size(154, 17);
            this.label_cardDetailsPay.TabIndex = 64;
            this.label_cardDetailsPay.Text = "ЗАПЧАСТИ - к оплате";
            // 
            // button_printWarrant
            // 
            this.button_printWarrant.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_printWarrant.Location = new System.Drawing.Point(1193, 320);
            this.button_printWarrant.Name = "button_printWarrant";
            this.button_printWarrant.Size = new System.Drawing.Size(107, 57);
            this.button_printWarrant.TabIndex = 65;
            this.button_printWarrant.Text = "Печать ЗН";
            this.button_printWarrant.UseVisualStyleBackColor = true;
            this.button_printWarrant.Click += new System.EventHandler(this.Button_printWarrant_Click);
            // 
            // FormCard2
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.ClientSize = new System.Drawing.Size(1306, 809);
            this.Controls.Add(this.button_printWarrant);
            this.Controls.Add(this.label_cardDetailsPay);
            this.Controls.Add(this.label_cardDetailsDiscount);
            this.Controls.Add(this.label_cardDetailsSumm);
            this.Controls.Add(this.label_cardDetails);
            this.Controls.Add(this.label_cardWorksChange);
            this.Controls.Add(this.label_cardWorks);
            this.Controls.Add(this.label_cardWorksPayChange);
            this.Controls.Add(this.label_cardWorksDiscountChange);
            this.Controls.Add(this.label_cardWorksPay);
            this.Controls.Add(this.label_cardWorksDiscount);
            this.Controls.Add(this.label_cardWorkSummChange);
            this.Controls.Add(this.buttonWrite);
            this.Controls.Add(this.label_cardWorksSumm);
            this.Controls.Add(this.button_print_invite);
            this.Controls.Add(this.button_close);
            this.Controls.Add(this.tabControl1);
            this.Name = "FormCard2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormCard";
            this.Closed += new System.EventHandler(this.FormCard_Closed);
            this.tabPage3.ResumeLayout(false);
            this.tabPage7.ResumeLayout(false);
            this.tabPage8.ResumeLayout(false);
            this.tabPage6.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox_cardStateTime.ResumeLayout(false);
            this.groupBox_cardStateTime.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		#region Главные кнопки
		
		private void Button_close_Click(object sender, System.EventArgs e)
		{
			// Закрыть окно без сохранения сделанных изменений
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}
		#endregion

		#region Действия трудоемкостей
		private void MenuItem3_Click(object sender, System.EventArgs e)
		{
			// Вызов формы корректировки/добавления/исправления примечаний работы.
			ListViewItem item = Db.GetItemSelected(listView1);
			if (item == null) return;
			if (item.Tag == null) return;
			DbCardWork card_work = (DbCardWork)item.Tag;
			long number = card_work.CardNumber;
			int year = card_work.CardYear;
			int position = card_work.Number;
			if (number == 0L) return;
			if (year == 0) return;
			if (position == 0) return;
			DtCardWork card_work_dt = DbSqlCardWork.Find(number, year, position);
			if (card_work_dt == null) return;

			UserInterface.CardWorkCommentList(0, card_work_dt, 0, UserInterface.ClickType.Modify);
		}
		private void MenuItem4_Click(object sender, EventArgs e)
		{
			// Всем выбранным работам по саиску устанавливается видимое примечание по отсутствию гарантии
			DbCardWork wrk;
			foreach (ListViewItem item in listView1.SelectedItems)
			{
				wrk = (DbCardWork)item.Tag;
				if (wrk != null)
				{
					if (wrk.Number > 0)
					{
						DbSqlCardWorkComment.InsertConnection(the_card.Number, the_card.Year, wrk.Number, 7754, true);
					}
				}
			}
		}
		private void Button_workpresent_Click(object sender, EventArgs e)
		{
			// Установить работу как бонус
		}
		private void Button_worknopresent_Click(object sender, EventArgs e)
		{
		}


		private void Button1_Click(object sender, System.EventArgs e)
		{
			// Запускаем окно выбора новой трудоемкости
			if (workSelect == null || workSelect.IsDisposed)
			{
				workSelect = new FormWorkSelect(card.AutoType, new FormWorkSelect.DelegateTransferToForm(TransferToForm));
				workSelect.Show();
			}
			else
			{
				workSelect.WindowState = FormWindowState.Normal;
				workSelect.BringToFront();
			}
		}

	
		private void Button_comment_Click(object sender, System.EventArgs e)
		{
			// Добавляем примечание к выбранной работе
			// Изменение цены выбранной трудоемкости
			ListViewItem item = Db.GetItemSelected(listView1);
			if (item == null) return;
			DbCardWork element = (DbCardWork)item.Tag;
			if (element == null) return;
			long card_number = the_card.Number;
			int card_year = the_card.Year;
			int position = element.Number;
			long code;
			bool show_flag = false;


			if (position == 0)
			{
				MessageBox.Show("Сначала нужно СОХРАНИТЬ заказ-наряд");
				return;
			}

			// Зпрос примечания
			ArrayList array = new ArrayList();
			DbSqlCardWorkComment.SelectInArray(array);
			UIF_Selector_Array form = new UIF_Selector_Array(array);
			if (form.ShowDialog() == DialogResult.OK)
			{
				// Произвели выбор из списка
				code = form.SelectedCode;
			}
			else
			{
				// Зпрос нового примечания
				FormSelectString form1 = new FormSelectString("Текст примечания", "");
				if (form1.ShowDialog() != DialogResult.OK) return;
				string text = form1.SelectedText;
				if (text == "") return;
				DtCardWorkComment comment = new DtCardWorkComment(text);
				comment = DbSqlCardWorkComment.Insert(comment);
				if (comment == null) return;
				code = comment.code;
			}
			if (code == 0) return;
			DialogResult result = MessageBox.Show("Сделать примечание видимым?", "Запрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (result == DialogResult.Yes) show_flag = true;

			// Проверка на возможные ошибки!!!
			bool flg = true;
			if (card_number == 0) flg = false;
			if (card_year == 0) flg = false;
			if (position == 0) flg = false;
			if (code == 0) flg = false;
			if (flg == false)
			{
				MessageBox.Show("Ошибка");
				return;
			}

			bool res = DbSqlCardWorkComment.InsertConnection(card_number, card_year, position, code, show_flag);
			if (res == true) MessageBox.Show("ОК");
		}
		private void buttonChangePrice_Click(object sender, System.EventArgs e)
		{
			// Изменение цены выбранной трудоемкости
			ListViewItem item = Db.GetItemSelected(listView1);
			if (item == null) return;
			DbCardWork element = (DbCardWork)item.Tag;
			if (element == null) return;
			// Запрос новой цены
			FormSelectString dialog = new FormSelectString("Новая стоимость работы", element.PriceFullTxt);
			dialog.ShowDialog();
			if (dialog.DialogResult != DialogResult.OK) return;
			float price = dialog.SelectedFloat;
			element.Price = price;
			element.SetLVItem(item);
		}
		private void MakeSumWork()
		{
			float summ = 0;
			foreach (ListViewItem item in listView1.Items)
			{
				DbCardWork cardWork = (DbCardWork)item.Tag;
				if (cardWork != null)
				{
					if (cardWork.Deleted != true)
					{
						summ += cardWork.Summ;
					}
				}
			}

			textBoxSumWork.Text = Db.CachToTxt(summ);

			// А это НОВОЕ
			//CalculatorCardWorkToPayVATIncluded calculator = new CalculatorCardWorkToPayVATIncluded(20);
			//CalculatorResult res = calculator.Calculate(the_card);
			_calculatorCard.Calculate(the_card);
			CalculatorResult currentWorks = _calculatorCard.WorksTotal;
			label_cardWorksSumm.Text = currentWorks.SummDatabase.ToString();
			label_cardWorksDiscount.Text = currentWorks.SummTotalDiscountBonus.ToString();
			label_cardWorksPay.Text = currentWorks.SummTotal.ToString();

			CalculatorResult currentDetails = _calculatorCard.DetailsTotal;
			label_cardDetailsSumm.Text = currentDetails.SummDatabase.ToString();
			label_cardDetailsDiscount.Text = currentDetails.SummTotalDiscountBonus.ToString();
			label_cardDetailsPay.Text = currentDetails.SummTotal.ToString();

			_calculatorCard.Calculate(listView1);
			CalculatorResult predictWorks = _calculatorCard.WorksTotal;
			label_cardWorkSummChange.Text = predictWorks.SummDatabase.ToString();
			label_cardWorksDiscountChange.Text = predictWorks.SummTotalDiscountBonus.ToString();
			label_cardWorksPayChange.Text = predictWorks.SummTotal.ToString();
		}
		private void buttonNewWork_Click(object sender, System.EventArgs e)
		{
			if (card.AutoType == null) return;
			// Добавление в список выполняемой работы
			FormWorkList dialog = new FormWorkList(card.AutoType, null);
			dialog.ShowDialog();
			if (dialog.DialogResult != DialogResult.OK) return;
			DbCardWork work = new DbCardWork(dialog.SelectedWork, card);

			// Для новой работы устанавливаем стоимость и скидку по умолчанию
			DtCard.SetWorkCalc(work, the_card.HourPrice, the_card.Discount);
			work.AddToList(listView1, true);
			UserInterface.SetListIndexes(listView1);
			MakeSumWork();
		}
		private void ButtonGarantyOn_Click(object sender, System.EventArgs e)
		{
			// Отметить все выбранные элементы как гарантийные
			foreach (ListViewItem item in listView1.SelectedItems)
			{
				DbCardWork work = (DbCardWork)item.Tag;
				if (work == null) return;
				work.Guaranty = true;
				work.SetLVItem(item);
			}
			listView1.SelectedItems.Clear();
			MakeSumWork();
		}
		protected void ListView1_SizeChanged(object sender, EventArgs e)
		{
			listView1.Columns[1].Width = listView1.Width - 82 - listView1.Columns[0].Width
				- listView1.Columns[2].Width
				- listView1.Columns[3].Width
				- listView1.Columns[4].Width
				- listView1.Columns[5].Width;
		}

		protected void ListView1_DoubleClick(object sender, EventArgs e)
		{
			Point pnt = Cursor.Position;
			pnt = listView1.PointToClient(pnt);
			ListViewItem item = Db.GetItemPosition(listView1);
			if (item == null) return;
			DbCardWork wrk = (DbCardWork)item.Tag;
			if (wrk == null) return;
			// Проверяем место клика
			int start = listView1.Columns[0].Width
				+ listView1.Columns[1].Width
				+ listView1.Columns[2].Width;
			int end = start + listView1.Columns[3].Width;
			if ((pnt.X > start) && (pnt.X < end))
			{
				int ypos = item.Bounds.Top;
				int yhght = item.Bounds.Height;
				int xpos = start;
				int xwdth = end - start;
				Point pnt0 = new Point(xpos, ypos);
				pnt0 = listView1.PointToScreen(pnt0);
				pnt0 = this.PointToClient(pnt0);
				Size sz0 = new Size(xwdth, yhght);
				TextBox box = new TextBox();
				this.Controls.Add(box);
				box.Location = pnt0;
				box.Size = sz0;
				box.Visible = true;
				box.Capture = true;
				box.Text = wrk.QuontityTxt;
				box.Show();
				box.BringToFront();
				box.Focus();
				box.MouseDown += new MouseEventHandler(this.box_MouseDown);
				box.KeyDown += new KeyEventHandler(this.box_KeyDown);
				textBoxTmp = box;
				textBoxItem = item;
			}
		}
		private void ButtonGuarantyOf_Click(object sender, System.EventArgs e)
		{
			// Отметить все выбранные элементы как гарантийные
			foreach(ListViewItem item in listView1.SelectedItems)
			{
				DbCardWork work = (DbCardWork)item.Tag;
				if(work == null) return;
				work.Guaranty = false;
				work.SetLVItem(item);
			}
			listView1.SelectedItems.Clear();
			MakeSumWork();
		}
		protected void listView1_KeyDown(object sender, KeyEventArgs e)
		{
			if(!card.CanUpdateWarrant) return;	// Нельзя изменять содержимое заказ-наряда

			if(e.KeyCode == Keys.Delete)
			{
				foreach(ListViewItem item in listView1.SelectedItems)
				{
					DbCardWork wrk = (DbCardWork)item.Tag;
					if(wrk == null) return;
					if(wrk.Exist)
					{
						if(wrk.Deleted)
							wrk.Deleted = false;
						else
							wrk.Deleted = true;
						wrk.SetLVItem(item);
					}
					else
					{
						listView1.Items.Remove(item);
						UserInterface.SetListIndexes(listView1);
					}
				}
				listView1.SelectedItems.Clear();
				MakeSumWork();
			}
			if(e.KeyCode == Keys.Add)
			{
				// Увеличиваем на единицу количество выбранных работ
				foreach(ListViewItem item in listView1.SelectedItems)
				{
					DbCardWork wrk = (DbCardWork)item.Tag;
					if(wrk != null)
					{
						wrk.Quontity++;
						wrk.SetLVItem(item);
					}
				}
				MakeSumWork();
			}
			if(e.KeyCode == Keys.Subtract)
			{
				// Уменьшаем на единицу количество выбранных работ
				foreach(ListViewItem item in listView1.SelectedItems)
				{
					DbCardWork wrk = (DbCardWork)item.Tag;
					if(wrk != null)
					{
						wrk.Quontity--;
						if(wrk.Quontity == 0) wrk.Quontity = 1;
						wrk.SetLVItem(item);
					}
				}
				MakeSumWork();
			}
		}
		private void buttonDone_Click(object sender, System.EventArgs e)
		{
			DbCardWork wrk;

			if(!card.IsWarrantOpened)
			{
				MessageBox.Show("Нельзя выполнять работы в неоткрытом заказ-наряде");
				return;
			}

			// #### Новый блок!
			// Проверяем работы на пренадлежность одному типу (пока только Мойка/Не мойка)
			bool wash	 = false;
			bool other	 = false;
			foreach(ListViewItem item in listView1.SelectedItems)
			{
				wrk = (DbCardWork)item.Tag;
				if(wrk != null)
				{
					// КОД МОЙКИ
					if(wrk.CodeDirectoryWork == 722)
						wash = true;
					else
						other = true;
				}
			}
			if(other == true && wash == true)
			{
				MessageBox.Show("Нельзя объеденять слесарные работы с мойкой!");
				return;
			}
			// #### Конец новый блок

			// #### НОВЫЙ ВАРИАНТ Сразбивкой по типам исполнителей
			FormListStaff dialog1;
			if(wash == true)
				dialog1 = new FormListStaff(3, 0);
			else
			{
				if(the_card.CodeWorkshop == 1)
					dialog1 = new FormListStaff(1, 0);
				else
					dialog1 = new FormListStaff(0, the_card.CodeWorkshop);
			}
			dialog1.ShowDialog(this);
			if(dialog1.DialogResult != DialogResult.OK) return;
			ListView list = dialog1.List;
			foreach(ListViewItem item in listView1.SelectedItems)
			{
				wrk = (DbCardWork)item.Tag;
				if(wrk != null)
				{
					if(wrk.Number > 0)
					{
						DbCardWorkPersonal.WriteListSelectionCode(list, wrk);
						wrk.SetLVItem(item);
					}
				}
			}
			// #### КОНЕЦ НОВЫЙ ВАРИАНТ
		}
		private void button_discount_Click(object sender, EventArgs e)
		{
			// Устанавливаем скидку на определенную группу работ
			// Сначала запрашиваем размер скидки
			FormSelectString dialog = new FormSelectString("Введите размер скидки на выбранные работы", "", false);
			if (dialog.ShowDialog() != DialogResult.OK) { return; }
			float data = dialog.SelectedFloat;
			//    if (data == 0.0F) return;
			DbCardWork wrk = null;
			foreach (ListViewItem item in listView1.SelectedItems)
			{
				wrk = (DbCardWork)item.Tag;
				if (wrk != null)
				{
					if (wrk.Number > 0)
					{
						if (DbSqlCardWork.SetDiscount(the_card.Number, the_card.Year, wrk.Number, data) == true)
						{
							wrk.Discount = data;
							wrk.SetLVItem(item);
						}
					}
				}
			}
		}
		protected void box_KeyDown(object sender, KeyEventArgs e)
		{
			string text = textBoxTmp.Text;
			if (e.KeyCode == Keys.Enter)
			{
				DbCardWork wrk = (DbCardWork)textBoxItem.Tag;
				if (wrk == null) return;
				wrk.QuontityTxt = text;
				wrk.SetLVItem(textBoxItem);
				this.Controls.Remove(textBoxTmp);
				textBoxTmp.Dispose();
				return;
			}
			if (e.KeyCode == Keys.Escape)
			{
				this.Controls.Remove(textBoxTmp);
				textBoxTmp.Dispose();
				return;
			}
		}

		private void buttonUnDone_Click(object sender, System.EventArgs e)
		{
			// Второй вариант - отмена выполнения выбранных работ
			foreach (ListViewItem item in listView1.SelectedItems)
			{
				DbCardWorkPersonal cardWorkPersonal = null;
				MessageBox.Show(item.Tag.GetType().ToString());
				DbCardWork wrk = (DbCardWork)item.Tag;
				if (wrk != null)
				{
					cardWorkPersonal = new DbCardWorkPersonal(wrk, null);
					if (cardWorkPersonal.Write())
					{
						wrk.DonePersonal = null;
						wrk.SetLVItem(item);
					}
				}
			}
		}

		protected void box_MouseDown(object sender, MouseEventArgs e)
		{
			this.Controls.Remove(textBoxTmp);
			textBoxTmp.Dispose();
		}
		#endregion

		#region Действия деталей
		private void button_to_Click(object sender, EventArgs e)
		{
			// Отметить выбранные детали и масла как относящиеся к ТО
			// Снимаем гарантию с элементов
			foreach (ListViewItem item in listView3.SelectedItems)
			{
				DbCardDetail detail = (DbCardDetail)item.Tag;
				if (detail != null)
				{
					if (detail.To == true)
						detail.To = false;
					else
						detail.To = true;

					long pos = detail.Number;
					if (detail.To)
					{
						if (DbSqlCardDetail.SetTo(the_card, pos) == false)
						{
							detail.To = false;
						}
					}
					else
					{
						if (DbSqlCardDetail.UnsetTo(the_card, pos) == false)
						{
							detail.To = true;
						}
					}
					detail.SetLVItem(item);
				}
			}
		}
		private void button_discount_detail_Click(object sender, EventArgs e)
		{
			// Устанавливаем скидку на определенную группу запасных частей
			// Сначала запрашиваем размер скидки
			FormSelectString dialog = new FormSelectString("Введите размер скидки на выбранные запчасти", "", true);
			if (dialog.ShowDialog() != DialogResult.OK) { return; }
			float data = dialog.SelectedFloat;
			// if (data == 0.0F) return;
			foreach (ListViewItem item in listView3.SelectedItems)
			{
				DbCardDetail detail = (DbCardDetail)item.Tag;
				if (detail != null)
				{
					long pos = detail.Number;
					if (DbSqlCardDetail.SetDetailDiscount(the_card, pos, data) == true)
					{
						detail.Discount = data;
						detail.SetLVItem(item);
					}
				}
			}
		}
		private void button_set_present_Click(object sender, System.EventArgs e)
		{
			// Установить как подарок
			ListViewItem item = Db.GetItemSelected(listView3);
			if (item == null) return;
			DbCardDetail element = (DbCardDetail)item.Tag;
			if (element == null) return;

			if (element.Guaranty == true)
			{
				MessageBox.Show("Деталь гарантийная!");
				return;
			}

			long pos = element.Number;
			if (DbSqlCardDetail.SetPresent(the_card, pos) == false) return;
			element.Present = true;
			element.SetLVItem(item);
		}
		private void button_unset_present_Click(object sender, System.EventArgs e)
		{
			// Снять отметку подарка
			ListViewItem item = Db.GetItemSelected(listView3);
			if (item == null) return;
			DbCardDetail element = (DbCardDetail)item.Tag;
			if (element == null) return;

			if (element.Present == false)
			{
				MessageBox.Show("Деталь не подарок!");
				return;
			}

			long pos = element.Number;
			if (DbSqlCardDetail.UnsetPresent(the_card, pos) == false) return;
			element.Present = false;
			element.SetLVItem(item);
		}
		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			// Оформляем заявку на выбранную деталь
			ListViewItem item = Db.GetItemSelected(listView3);
			if (item == null) return;
			if (item.Tag == null) return;
			DbCardDetail card_detail = (DbCardDetail)item.Tag;
			DtStorageRequest request = new DtStorageRequest();
			request.SetData("ССЫЛКА_КОД_СКЛАД_ДЕТАЛЬ", card_detail.CodeDetailStorage);
			request.SetData("НАИМЕНОВАНИЕ_СКЛАД_ДЕТАЛЬ", card_detail.DetailNameTxt);
			request.SetData("ССЫЛКА_НОМЕР_КАРТОЧКА", the_card.Number);
			request.SetData("ССЫЛКА_ГОД_КАРТОЧКА", the_card.Year);
			request.SetData("ССЫЛКА_КОД_КОНТРАГЕНТ", the_card.CodeOwner);
			request.SetData("КОЛИЧЕСТВО_СКЛАД_ДЕТАЛЬ", card_detail.Quontity);
			request.SetData("ГАРАНТИЯ_ЗАЯВКА", card_detail.Guaranty);

			FormUpdateStorageRequest dialog = new FormUpdateStorageRequest(request);
			dialog.Show();
		}
		private void listView3_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			// На поднятие кнопки мышки - меню
			if (e.Button == MouseButtons.Right)
			{
				// На отпускание правой кнопки мышки - всплывающее меню
				// Настройка меню
				// Показ меню
				contextMenu1.Show(listView3, new Point(e.X, e.Y));
			}
		}
		protected float SetDetailSumm()
		{
			float summ = 0.0f;
			foreach (ListViewItem itm in listView3.Items)
			{
				if (itm != null)
				{
					DbCardDetail element = (DbCardDetail)itm.Tag;
					if (element != null)
					{
						summ += element.Summ;
					}
				}
			}
			return summ;
		}
		private void button2_Click(object sender, System.EventArgs e)
		{
			bool recive = true;
			// Отметка получения деталей по выбранному списку
			DbStaff reciver = DbStaff.GetByESign("Электронная подпись получателя");
			if (reciver == null) return;
			foreach (ListViewItem itm in this.listView3.SelectedItems)
			{
				DbCardDetail dtl = (DbCardDetail)itm.Tag;
				recive = true;
				if (dtl.Check == true) recive = false;
				if (dtl.Outer == true) recive = false;
				if (dtl.Recived == true) recive = false;
				if (recive)
				{
					if (dtl.WriteRecive(reciver.Code) == true)
					{
						dtl.SetLVItem(itm);
					}
				}
			}
		}
		private void button3_Click(object sender, System.EventArgs e)
		{
			bool ret;
			// Возврат деталей по списку
			DbStaff returner = DbStaff.GetByESign("Электронная подпись принимающего деталь на склад");
			if (returner == null) return;
			foreach (ListViewItem itm in this.listView3.SelectedItems)
			{
				ret = true;
				DbCardDetail dtl = (DbCardDetail)itm.Tag;
				if (dtl.Recived == false) ret = false;
				if (ret)
				{
					if (dtl.WriteReturn(returner.Code) == true)
					{
					}
				}
			}
			// Обновляем список
			// Пробуем загрузить список деталей карточки
			listView3.Items.Clear();
			DbCardDetail.FillList(listView3, card);
			UserInterface.SetListIndexes(listView3);
			//Выставляем сумму полученных деталей
			textBoxDetailSumm.Text = Db.CachToTxt(SetDetailSumm());
		}
		protected void listView3_DoubleClick(object sender, EventArgs e)
		{
			// Определяем столбец, на ктором щелкнули мышкой
			// Необходимо определить элемент и колонку, на которой кликнули
			int column = Db.GetColumnPosition(listView3);
			ListViewItem item = Db.GetItemPosition(listView3);
			if (item == null) return;
			DbCardDetail cardDetail = (DbCardDetail)item.Tag;
			if (cardDetail == null) return;

			if (column != 3) return;

			// Запуск нового окна ввода текста
			FormSelectionType2 dialog = Db.MakeFormSelectionType2(this, item, column, "1.00");
			dialog.ShowDialog(this);
			if (dialog.DialogResult != DialogResult.OK) return;
			cardDetail.Quontity = dialog.SelectedFloat;
			cardDetail.SetLVItem(item);
		}
		protected void listView3_KeyDown(object sender, KeyEventArgs e)
		{
			if (the_card.Closed) return;

			if(e.KeyCode == Keys.Delete)
			{
				foreach(ListViewItem item in listView3.SelectedItems)
				{
					DbCardDetail dtl = (DbCardDetail)item.Tag;
					if(dtl == null) return;
					if(dtl.Exists)
					{
						if(dtl.Delete)
							dtl.Delete = false;
						else
							dtl.Delete = true;
						dtl.SetLVItem(item);
					}
					else
					{
						listView3.Items.Remove(item);
						UserInterface.SetListIndexes(listView3);
					}
				}
				listView3.SelectedItems.Clear();
				//Выставляем сумму полученных работ
				textBoxDetailSumm.Text	= Db.CachToTxt(SetDetailSumm());
			}
			if(e.KeyCode == Keys.Add)
			{
				// Увеличиваем на единицу количество выбранных работ
				foreach(ListViewItem item in listView3.SelectedItems)
				{
					DbCardDetail dtl = (DbCardDetail)item.Tag;
					if(dtl != null)
					{
						dtl.Quontity += 1;
						dtl.SetLVItem(item);
					}
				}
				//Выставляем сумму полученных работ
				textBoxDetailSumm.Text	= Db.CachToTxt(SetDetailSumm());
			}
			if(e.KeyCode == Keys.Subtract)
			{
				// Уменьшаем на единицу количество выбранных работ
				foreach(ListViewItem item in listView3.SelectedItems)
				{
					DbCardDetail dtl = (DbCardDetail)item.Tag;
					if(dtl != null)
					{
						dtl.Quontity--;
						if(dtl.Quontity == 0) dtl.Quontity = 1;
						dtl.SetLVItem(item);
					}
				}
				//Выставляем сумму полученных работ
				textBoxDetailSumm.Text	= Db.CachToTxt(SetDetailSumm());
			}
		}
		private void buttonNewStorage_Click(object sender, System.EventArgs e)
		{
			// Добавление новой детали в лист деталей, через склад

			FormDetailStorageList dialog = new FormDetailStorageList(listView3, 4, null, card);
			dialog.ShowDialog(this);
			//if(dialog.DialogResult != DialogResult.OK) return;
			//DbCardDetail element = new DbCardDetail(card, dialog.DetailStorage);
			//listView3.Items.Add(element.LVItem);
			UserInterface.SetListIndexes(listView3);
			//Выставляем сумму полученных работ
			textBoxDetailSumm.Text = Db.CachToTxt(SetDetailSumm());

		}
		private void buttonDelDetail_Click(object sender, System.EventArgs e)
		{
			// Удаляем из списка выбранную деталь
			ListViewItem item = Db.GetItemSelected(listView3);
			if (item == null) return;
			DbCardDetail element = (DbCardDetail)item.Tag;
			if (element == null) return;

			if (element.Exists)
			{
				// Изменяем флаг удаления
				element.Delete = !element.Delete;
				element.SetLVItem(item);
			}
			else
			{
				listView3.Items.Remove(item);
				UserInterface.SetListIndexes(listView3);
			}
			//Выставляем сумму полученных работ
			textBoxDetailSumm.Text = Db.CachToTxt(SetDetailSumm());
		}
		private void buttonReserve_Click(object sender, System.EventArgs e)
		{
			// Резервирование детали на складе под заданную позицию
			foreach (ListViewItem item in listView3.SelectedItems)
			{
				DbCardDetail detail = (DbCardDetail)item.Tag;
				if (detail != null)
				{
					DbReserve reserve = new DbReserve(detail);
					reserve.Add();
					if (reserve.Write())
					{
						detail.Reserve = reserve.Reserve;
						detail.SetLVItem(item);
					}
				}
			}
			Db.ShowFaults();
		}
		private void buttonUnreserve_Click(object sender, System.EventArgs e)
		{
			// Отмена резервирование детали на складе под заданную позицию
			foreach (ListViewItem item in listView3.SelectedItems)
			{
				DbCardDetail detail = (DbCardDetail)item.Tag;
				if (detail != null)
				{
					DbReserve reserve = new DbReserve(detail);
					reserve.Delete();
					if (reserve.Write())
					{
						detail.Reserve = reserve.Reserve;
						detail.SetLVItem(item);
					}
				}
			}
			Db.ShowFaults();
		}
		private void buttonGuarantyOn_Click(object sender, System.EventArgs e)
		{
			// Устанавливаем гарантию на элементы
			foreach (ListViewItem item in listView3.SelectedItems)
			{
				DbCardDetail detail = (DbCardDetail)item.Tag;
				if (detail != null)
				{
					detail.Guaranty = true;
					// Пересчет цены
					if (detail.Connect1C() == true)
					{
						detail.Price += detail.Price / 100 * card.GuarantyTypeVal;
					}
					detail.SetLVItem(item);
				}
			}
			//Выставляем сумму полученных работ
			textBoxDetailSumm.Text = Db.CachToTxt(SetDetailSumm());
		}
		private void buttonGuarantyOff_Click(object sender, System.EventArgs e)
		{
			// Снимаем гарантию с элементов
			foreach (ListViewItem item in listView3.SelectedItems)
			{
				DbCardDetail detail = (DbCardDetail)item.Tag;
				if (detail != null)
				{
					detail.Guaranty = false;
					detail.SetLVItem(item);
				}
			}
			//Выставляем сумму полученных работ
			textBoxDetailSumm.Text = Db.CachToTxt(SetDetailSumm());
		}
		private void buttonSetPrice_Click(object sender, System.EventArgs e)
		{
			// Установка новой цены детали
			ListViewItem item = Db.GetItemSelected(listView3);
			if (item == null) return;
			DbCardDetail element = (DbCardDetail)item.Tag;
			if (element == null) return;

			// Если позиция связанна с 1С - новую цену установить нельзя!
			if (element.Connect1C() == true) return;

			FormSelectString dialog = new FormSelectString("Новая цена (с НДС)", "0.0");
			dialog.ShowDialog(this);
			if (dialog.DialogResult != DialogResult.OK) return;
			element.Price = dialog.SelectedFloat;
			element.SetLVItem(item);

			//Выставляем сумму полученных работ
			textBoxDetailSumm.Text = Db.CachToTxt(SetDetailSumm());
		}
		private void buttonSetOuter_Click(object sender, System.EventArgs e)
		{
			// Пометить деталь как внешнюю
			foreach (ListViewItem item in listView3.SelectedItems)
			{
				DbCardDetail detail = (DbCardDetail)item.Tag;
				if (detail != null)
				{
					detail.Outer = true;
					detail.SetLVItem(item);
				}
			}
			//Выставляем сумму полученных работ
			textBoxDetailSumm.Text = Db.CachToTxt(SetDetailSumm());
		}
		private void buttonSetCheck_Click(object sender, System.EventArgs e)
		{
			// Пометить детали как по копии чека
			foreach (ListViewItem item in listView3.SelectedItems)
			{
				DbCardDetail detail = (DbCardDetail)item.Tag;
				if (detail != null)
				{
					detail.Check = true;
					detail.SetLVItem(item);
				}
			}
		}
		private void buttonSetNoOuter_Click(object sender, System.EventArgs e)
		{
			// Отменить пометку детали как внешней
			foreach (ListViewItem item in listView3.SelectedItems)
			{
				DbCardDetail detail = (DbCardDetail)item.Tag;
				if (detail != null)
				{
					detail.Outer = false;
					detail.SetLVItem(item);
				}
			}
			//Выставляем сумму полученных работ
			textBoxDetailSumm.Text = Db.CachToTxt(SetDetailSumm());
		}
		private void buttonSetNoCheck_Click(object sender, System.EventArgs e)
		{
			// Отменить пометку детали как по копии чека
			foreach (ListViewItem item in listView3.SelectedItems)
			{
				DbCardDetail detail = (DbCardDetail)item.Tag;
				if (detail != null)
				{
					detail.Check = false;
					detail.SetLVItem(item);
				}
			}
		}
		private void buttonSetOil_Click(object sender, System.EventArgs e)
		{
			// Установить выбранные работы - входят в группу масла
			foreach (ListViewItem item in listView1.SelectedItems)
			{
				DbCardWork work = (DbCardWork)item.Tag;
				if (work != null)
				{
					work.Oil = true;
					work.SetLVItem(item);
				}
			}
		}
		private void buttonSetNoOil_Click(object sender, System.EventArgs e)
		{
			// Отменить установку выбранных работ - как входящих в группу масла
			foreach (ListViewItem item in listView1.SelectedItems)
			{
				DbCardWork work = (DbCardWork)item.Tag;
				if (work != null)
				{
					work.Oil = false;
					work.SetLVItem(item);
				}
			}
		}
		#endregion

        #region Рекомендации к карточке
        private void buttonNewRecomend_Click(object sender, System.EventArgs e)
		{
			// Добавление в список новой рекомендации
			FormSelectString dialog = new FormSelectString("Введите текст рекомендации", "");
			dialog.ShowDialog();
			if(dialog.DialogResult != DialogResult.OK) return;
			string text = dialog.SelectedText;
			text = text.Trim();
			if(DbCardRecomend.CheckText(text) == false) return;
			DbCardRecomend recomend = new DbCardRecomend(card, text);
			listView4.Items.Add(recomend.LVItem);
		}
		private void buttonPropertyRecomend_Click(object sender, System.EventArgs e)
		{
			// Изменение существующей рекомендации
			// Установка новой цены детали
			ListViewItem item = Db.GetItemSelected(listView4);
			if(item == null) return;
			DbCardRecomend element = (DbCardRecomend)item.Tag;
			if(element == null) return;
			FormSelectString dialog = new FormSelectString("Новый текст рекомендации", element.Recomendation);
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			string text = dialog.SelectedText;
			text = text.Trim();
			if(DbCardRecomend.CheckText(text) == false) return;
			element.Recomendation = text;
			element.SetLVItem(item);
		}
		protected void listView4_KeyDown(object sender, KeyEventArgs e)
		{
			if(!card.CanUpdateWarrant) return;	// Нельзя менять заказ-наряд

			if(e.KeyCode == Keys.Delete)
			{
				foreach(ListViewItem item in listView4.SelectedItems)
				{
					DbCardRecomend rcm = (DbCardRecomend)item.Tag;
					if(rcm == null) return;
					if(rcm.Exists)
					{
						if(rcm.Delete)
							rcm.Delete = false;
						else
							rcm.Delete = true;
						rcm.SetLVItem(item);
					}
					else
					{
						listView4.Items.Remove(item);
					}
				}
				listView4.SelectedItems.Clear();
			}
		}
        #endregion
		public void UpdateCardData()
		{
			UserInterface.SetListIndexes(listView1);
			MakeSumWork();

			UserInterface.SetListIndexes(listView3);
			textBoxDetailSumm.Text	= Db.CachToTxt(SetDetailSumm());
		}

		private void FormCard_Closed(object sender, EventArgs e)
		{
			if(workSelect != null)
			{
				workSelect.Close();
				workSelect	= null;
			}
		}

		#region Огранизация связи со внешним миром
		public void TransferToForm(ListViewItem item)
		{
			if (item == null) return;
			DbWork work = (DbWork)item.Tag;
			if (work == null) return;
			DbCardWork cardWork = new DbCardWork(work, card);
			DtCard.SetWorkCalc(cardWork, the_card.HourPrice, the_card.Discount);
			ListViewItem itm = listView1.Items.Add(cardWork.LVItem);
			cardWork.SetLVItem(itm);
			UserInterface.SetListIndexes(listView1);
	
			MakeSumWork();
		}
		public void SetListLink(ListView list)
		{
			outer_list = list;
		}
		public void SetItemLink(ListViewItem item)
		{
			outer_item = item;
		}
		public void ShowChanges()
		{
			if(outer_list != null)
			{
				if(outer_list.IsDisposed == true) return;
				ListViewItem item = outer_list.Items.Add("");
				//DtCard card1 = DbSqlCard.FindList(this.card);

				DtCard card1 = DbSqlCard.Find(the_card);
				WfListViewForm cardLW = new WfListViewForm(card1);
				cardLW.SetListViewItemT01(item);

				//card1.SetLVItem(item);
				outer_item	= item;
				outer_list	= null;
				return;
			}
			else
			{
				if(outer_item == null) return;
				if(outer_item.ListView == null) return;
				if(outer_item.ListView.IsDisposed == true) return;
				//DtCard card1 = DbSqlCard.FindList(this.card);

				DtCard card1 = DbSqlCard.Find(the_card);
				WfListViewForm cardLW = new WfListViewForm(card1);
				cardLW.SetListViewItemT01(outer_item);

				//card1.SetLVItem(outer_item);
			}
		}
		#endregion

		#region Работа с заявками
		private void Button_new_request_Click(object sender, System.EventArgs e) // Добавляем заявку в списко заявок
		{
			// Создание новой строки заявки
			FormManageClaim form = new FormManageClaim(this, the_card, GetClaimList());
			form.ShowDialog();
		}
		private void Button_remove_claim_Click(object sender, System.EventArgs e) // Удаляем заявку из списка заявок - сразу
		{
			DtCard dtCard;
			long code;
			ListViewItem item;
			if ((dtCard = GetCardOrCloseForm()) == null) return;
			if ((item = WindowsFormCommon.GetListItemSelected(GetClaimList())) == null) return;
			if ((code = WindowsFormCommon.GetListItemTagLong(item)) == 0) return;
			if (DbSqlCardClaim.Remove(dtCard, code) == false) return;
			GetClaimList().Items.Remove(item);
		}
		#endregion
		private void button_selection_Click(object sender, System.EventArgs e)
		{
	//		FormWorkManagment dialog = new FormWorkManagment(this);
	//		dialog.Show();
		}
		public ListView GetWorkList() // Возвращает объект - список работ карточи
		{
			return listView1;
		}
		public ListView GetDetailList()
		{
			return listView3;
		}
		public ListView GetClaimList()
		{
			return listView_request;
		}

		private void button_selection1_Click(object sender, System.EventArgs e)
		{
		//	FormWorkManagment dialog = new FormWorkManagment(this);
		//	dialog.Show();
		}

		private void button_new_catalogue_detail_Click(object sender, System.EventArgs e)
		{
			// Добавление в заказ новой позиции из каталога
			// Вызов окна управления каталогом деталей
			FormCatalogueParts dialog = new FormCatalogueParts(this, 1);
			dialog.Show();
		}

		// Работа с внешними формами
		public bool NewCatalogueParts(DtCatalogueParts part)
		{
			// Можно запускать, только если карточка сохранена в базе
			if(the_card.Number == 0)return false;
			DtCardDetailOrder order = new DtCardDetailOrder();
			order.SetData("НОМЕР_КАРТОЧКА", the_card.Number);
			order.SetData("ГОД_КАРТОЧКА", the_card.Year);
			order.SetData("КОД_КАТАЛОГ_ДЕТАЛЬ", (long)part.GetData("КОД_КАТАЛОГ_ДЕТАЛЬ"));
			order.SetData("КОД_СКЛАД_ДЕТАЛЬ", (long)0);
			order.SetData("ГАРАНТИЯ", (bool)false);
			order.SetData("НАИМЕНОВАНИЕ", (string)part.GetData("НАИМЕНОВАНИЕ"));
			DtCardDetailOrder new_order = DbSqlCardDetailOrder.Insert(order);
			if(new_order == null) return false;
			ListViewItem item = listView5.Items.Add("Новый элемент");
			if(item == null) return false;
			new_order.SetLVItem(item);
			return true;
		}

		private void ServiceHistory(long auto, long workshop)
		{
			DateTime date_end	= DateTime.Now;
			DateTime date_start = date_end;
			date_start			= date_start.AddYears(-1);

			ArrayList array = new ArrayList();
			DbSqlCard.SelectCardClosedNumberWorkshopAuto(array, date_start, date_end, workshop, auto);

			DtCardClaimCollectionAnalitic claimsAnalize = new DtCardClaimCollectionAnalitic();

			int day_count = 0;

			// Подсчитываем количество затраченного времени и анализируем заявки
			foreach(object o in array)
			{
				DtCard card = (DtCard)o;
				long number = (long)card.GetData("НОМЕР_КАРТОЧКА");
				int year = (int)card.GetData("ГОД_КАРТОЧКА");
				card = DbSqlCard.Find(number, year);
				DateTime date = (DateTime)card.GetData("ДАТА");

				// Подсчет времени нахождения в ремонте (с гарантийными случаями)
				ArrayList works = new ArrayList();
				DbSqlCardWork.SelectInArray(card, works);
				bool guaranty = false;
				foreach(object o3 in works)
				{
					DtCardWork work = (DtCardWork)o3;
					if(work.GuaranteeFlag() == true) guaranty = true;
				}
				if(guaranty)
				{
					DateTime work_begin = (DateTime)card.GetData("ДАТА_НАРЯД_ОТКРЫТ_КАРТОЧКА");
					DateTime work_end;
					DtCardMarkEndWork mark_workend = DbSqlCardMarkEndWork.Find(card);
					if(mark_workend != null)
						work_end = (DateTime)mark_workend.GetData("ДАТА");
					else
						work_end = (DateTime)card.GetData("ДАТА_НАРЯД_ЗАКРЫТ_КАРТОЧКА");

					TimeSpan span = new TimeSpan(work_begin.Ticks);
					DateTime diff = work_end.Subtract(span);
					int count = diff.DayOfYear;
					day_count += count;
				}

				claimsAnalize.Add(card);
			
			}
			// Выдаем результат
			string message = claimsAnalize.ClaimsStringList();
			message += "Время в гарантийном ремонте " + day_count.ToString();
			MessageBox.Show(message);
		}

		private void listView1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			// На поднятие кнопки мышки - меню
			if(e.Button == MouseButtons.Right)
			{
				// На отпускание правой кнопки мышки - всплывающее меню
				// Настройка меню
				// Показ меню
				contextMenu2.Show(listView1, new Point(e.X, e.Y));
			}
		}

		
		public ArrayList WorkList
		{
			get
			{
				ArrayList works = new ArrayList();
				foreach (ListViewItem itm in listView1.Items)
				{
					DbCardWork wrk = (DbCardWork)itm.Tag;
					if (wrk != null)
					{
						DtCardWork wrk_dt = new DtCardWork(wrk);
						works.Add(wrk_dt);
					}
				}
				return works;
			}
		}
		public ArrayList DetailList
		{
			get
			{
				ArrayList details = new ArrayList();
				foreach (ListViewItem itm in listView3.Items)
				{
					DbCardDetail dtl = (DbCardDetail)itm.Tag;
					if (dtl != null)
					{
						DtCardDetail dtl_dt = new DtCardDetail(dtl);
						details.Add(dtl_dt);
					}
				}
				return details;
			}
		}

		#region Обработчики основных событий
		private void LinkLabel_AgreedPickUpTime_Click(object sender, EventArgs e) // Установка согласованного времени выдачи
		{
			// Изменить согласованное время выдачи
			FormSelectDateTime dlg = new FormSelectDateTime();
			if (dlg.ShowDialog() != DialogResult.OK) return;
			DateTime dt = dlg.result;
			the_card.AgreedPickUpTime = dt;
			ShowCardInfo();
		}
		private void ComboBox_cardPayType_SelectedValueChanged(object sender, EventArgs e) // Выбор типа оплаты
		{
			FormDisplay.FormDisplayStruct displayStract = (FormDisplay.FormDisplayStruct)comboBox_cardPayType.SelectedItem;
			DtCard dtCard;
			if ((dtCard = GetCardOrCloseForm()) == null) return;
			if ((DtCard.PAY_TYPE)displayStract.EnumData == (DtCard.PAY_TYPE)_displayerCard.CardPayType().EnumData)
			{
				return;
			}
			if ((DtCard.PAY_TYPE) displayStract.EnumData == DtCard.PAY_TYPE.NONE)
			{
				FormDisplay.DisplayComponent(comboBox_cardPayType, _displayerCard.CardPayType());
				return;
			}
			dtCard.PayType = (DtCard.PAY_TYPE)displayStract.EnumData;
			ShowCardInfo();
		}
		private void LinkLabel_cardWorkshop_Click(object sender, EventArgs e) // Выбор подразделения карточи
		{
			SelectWorkshop();
			ShowCardInfo();
		}
		private void LinkLabel_cardRun_Click(object sender, EventArgs e)    // Запуск изменение пробега карточки
		{
			int run = UserInterface.Selector_Int("Введите значение пробега, км", "0");
			the_card.SetCardRun(run);
			ShowCardInfo();
		}
		private void LinkLabel_autoType_Click(object sender, EventArgs e) // Выбор трудоемкостей по умолчанию
        {
			DbAutoType dbAutoType = (DbAutoType)UserInterface.SelectAutoType();
			the_card.AutoType = dbAutoType;
			ShowCardInfo();
		}
		private void LinkLabel_defaultCalcDiscountWork_Click(object sender, EventArgs e) // Изменить Размер скидки на работы
		{
			float dsk = UserInterface.Selector_Float("Скидка на работы", "0.0");
			if (dsk < 0.0F) return;
			the_card.Discount = dsk;
			ShowCalcDefault();
		}
		private void LinkLabel_representativeDocs_Click(object sender, EventArgs e) // Смена документа представителя
		{
			string docs = UserInterface.Selector_String("Документ представителя", "Доверенность без номера");
			if (docs == "") return;
			the_card.RepresentativeDocs = docs;
			card.RepresentDocument = docs;
			ShowRepresentativeInfo();
		}
		private void LinkLabel_hourPrice_Click(object sender, EventArgs e) // Изменить временную стоимость нормачаса
		{
			float hp = UserInterface.Selector_Float("Стоимость нормачаса", "0.0");
			if (hp == 0.0F) return;
			the_card.HourPrice = hp;
			ShowCalcDefault();
		}
		private void LinkLabel_representative_Click(object sender, EventArgs e) // Выбрать нового представителя
		{
			DtCard dtCard;
			if ((dtCard = GetCardOrCloseForm()) == null) return;
			DtPartner dtRepresentative = (DtPartner)UserInterface.SelectPartner();
			if (dtRepresentative == null) return;
			dtCard.Representative = dtRepresentative;
			ShowRepresentativeInfo();
		}	
		private void LinkLabel_owner_Click(object sender, EventArgs e) // Обработка события - выбор владельца автомобиля
		{
			SetOwner();
		}
		private void LinkLabel_cardComment_Click(object sender, EventArgs e) // Установка примечания к карточке
		{
			DtCard dtCard;
			if ((dtCard = GetCardOrCloseForm()) == null) return;
			string comment = UserInterface.Selector_String("Примечание", dtCard.Comment);
			dtCard.Comment = comment;
			ShowCardInfo();
		}
		private void LinkLabel_defaultCardDiscountDetail_Click(object sender, EventArgs e) // Установка скидки на запасные части
		{
			float dsk = UserInterface.Selector_Float("Скидка на запчасти", "0.0");
			if (dsk == 0.0F) return;
			the_card.DiscountDetail = dsk;
			ShowCalcDefault();
		}
		private void LinkLabel_cardServiceManager_Click(object sender, EventArgs e) // Выбор сервис консультанта
		{
			DtCard dtCard;
			if ((dtCard = GetCardOrCloseForm()) == null) return;
			DtStaff staff = (DtStaff)UserInterface.SelectStaff();
			if (staff == null) return;
			dtCard.ServiceManager = staff;
			ShowServiceManagerInfo();
		}
		private void CheckBox_cardReturn_CheckedChanged(object sender, EventArgs e) // Установка снятие флага возврата
		{
			DtCard dtCard;
			if ((dtCard = GetCardOrCloseForm()) == null) return;
			dtCard.Returned = checkBox_cardReturn.Checked;
		}
		private void LinkLabel_cardActionClose_Click(object sender, EventArgs e) // Закрытие карточки заказа
		{
			if (the_card.Close() == false) { Error.InfoMessageVoid("Не смогли закрыть заказ-наряд"); return; }
			SetComponentAccessibility();
			ShowCardInfo();
		}
		private void LinkLabel_cardActionOpen_Click(object sender, EventArgs e)
		{
			if (the_card.Open() == false) { Error.InfoMessageVoid("Не смогли открыть заказ-наряд"); return; }
			SetComponentAccessibility();
			ShowCardInfo();
		}
		#endregion

		#region Обработчики вспомогательных событий
		private void LinkLabel_ownerContacts_Click(object sender, EventArgs e) // Клик на контактах - вызов диалога редактирования свойств владельца
		{
			DtCard dtCard;
			if ((dtCard = GetCardOrCloseForm()) == null) return;
			DtPartner owner;
			if ((owner = dtCard.Owner) == null) return;
			UserInterface.PartnerEditForm(owner);
			owner.ReloadDataFromDatabase();
			ShowOwnerInfo();
		}
		private void LinkLabel_representativeContacts_Click(object sender, EventArgs e) // Клик на контактах - вызов диалога редактирования свойств представителя
		{
			DtCard dtCard;
			if ((dtCard = GetCardOrCloseForm()) == null) return;
			DtPartner representative;
			if ((representative = dtCard.Representative) == null) return;
			UserInterface.PartnerEditForm(representative);
			representative.ReloadDataFromDatabase();
			ShowRepresentativeInfo();
		}
		#endregion

		#region Вспомагательные кнопки интерфейся для удобства работы
		private void Button_vinToClipboard_Click(object sender, EventArgs e) // Сохранение VIN в буфере обмена
        {
			DtAuto auto;
			if ((auto = GetAuto()) == null) return;
			Clipboard.SetData(DataFormats.Text, (Object)auto.VIN);
		}
        #endregion

        #region DRY код
		private DtCard GetCardOrCloseForm() // Возвращает картучку в форме, если определены все данные или закрывает форму с ошибкой
        {
			if (the_card == null || card == null)
			{
				Db.SetErrorMessage("NULL - карточка в форме");
				Db.ShowFaults();
				this.Close();
				return null;
			}
			return the_card;
        }
		private DtAuto GetAuto() // Возвращает автомобиль привязанный к карточке в форме
        {
			DtCard card;
			if ((card = GetCardOrCloseForm()) == null) return null;	
			return card.DtAuto;
        }
		private void SetOwner()    // Установка Владельца
		{
			// Контроль ошибок
			DtCard dtCard;
			if ((dtCard = GetCardOrCloseForm()) == null) return;
			DtPartner dtPartner = (DtPartner)UserInterface.SelectPartner();
			if (dtPartner == null) return;
			dtCard.Owner = dtPartner;

			ShowOwnerInfo(); // Отображаем данные владельца
			ShowCalcDefault(); // Отображаем значения по умолчанию для данного владельца
			ShowRepresentativeInfo(); // Отображаем представителя (в том числе и нулевого)
									  // Пока все по старому
									  // Тут же предлагаем выбрать автомобиль данного клиента
			if (the_card.DtAuto != null) return; // Если автомобиль уже есть - то не предлагаем автовыбор
			DbPartner dbOwner = DbPartner.Find(dtCard.CodeOwner);
			FormAutoList dialogAuto = new FormAutoList(Db.ClickType.Select, dbOwner);
			dialogAuto.ShowDialog(this);
			if (dialogAuto.DialogResult != DialogResult.OK) return;

			DtAuto dtAuto = DbSqlAuto.Find(dialogAuto.Auto.Code);

			AutoCheckSetShow(dtAuto, the_card);
			return;
		}
		private bool SelectWorkshop() // Выбор подразделения
		{
			DtWorkshop workshop = (DtWorkshop)UserInterface.SelectWorkshop();
			if (workshop == null) return false;
			the_card.Workshop = workshop;
			return true;
		}
        #endregion

        private void ButtonWrite_Click(object sender, EventArgs e)
        {
			if (DbSqlCard.Write(the_card))
			{
				MessageBox.Show("Записали успешно");
				the_card.IsChg = false;
				the_card.IsNew = false;
			}
			else
				MessageBox.Show("Что то не записалось");

			// Запись трудоемкостей
			DbCardWork.WriteList(listView1, card);
			// Запись деталей
			DbCardDetail.WriteList(listView3, card);
			// Запись рекомендаций
			DbCardRecomend.WriteList(listView4, card);


			// Перезачитываем заново сохраненные данные
			the_card = DbSqlCard.Find(the_card.Number, the_card.Year); // Чтение из базы данных
			card = DbCard.Find(the_card.Number, the_card.Year);
			if (card == null || the_card == null) return;
			the_card.IsNew = false;

			float tmpCardHourPrice = the_card.HourPrice;
			// Костыль
			if (tmpCardHourPrice != 0.0F)
				the_card.HourPrice = tmpCardHourPrice;

			LoadCardWorks();    // Загрузка списка работ карточки
			LoadCardDetails();  // Загрузка списка деталей карточки
			LoadCardClaims();   // Загрузка списка заявок карточки
			LoadCardDetailOrders(); // Загрузка заявок на запасные части
			LoadCardRecomends(); // Загрузка рекомендаций карточки

			//	ShowCardInfo();
			//	ShowOwnerInfo();
			//	ShowAutoInfo();
			//	ShowRepresentativeInfo();
			//	ShowServiceManagerInfo();
			ShowChanges();
			if (Db.ShowFaults() == true) return;
		}
        private void LinkLabel_LicensePlate_Click(object sender, EventArgs e) // Исправление регистрационного знака в заказ-наряде
        {
			// Пока не предусмотрено
        }

        private void ComboBox_typeOfVAT_SelectedValueChanged(object sender, EventArgs e) // Выбор типа калькулятора для рассчета
        {
			// При смене значения пересчитываем калькулятор
            switch (comboBox_typeOfVAT.SelectedItem.ToString())
            {
				case NO_VAT:
					_calculatorCard = new CalculatorCard(CALCULATOR_TYPE.CALCULATOR_PAY, VAT_TYPE.VAT_NON, 0);    // Рассчетные параметры калькулятора
					break;
				case VAT_INCLUDED_20:
					_calculatorCard = new CalculatorCard(CALCULATOR_TYPE.CALCULATOR_PAY, VAT_TYPE.VAT_INCLUDED, 20);    // Рассчетные параметры калькулятора
					break;
				default:
					_calculatorCard = new CalculatorCard(CALCULATOR_TYPE.CALCULATOR_PAY, VAT_TYPE.VAT_NON, 0);    // Рассчетные параметры калькулятора
					break;
            }
			ShowChanges();
		}

		#region Обработчики кнопок печати
		private void Button_print_invite_Click(object sender, System.EventArgs e)
		{
			//DbPrintInvite print = new DbPrintInvite(the_card, WorkList, DetailList, _calculatorCard);
			DbPrintInvite print = new DbPrintInvite(the_card, _calculatorCard);
			print.PrintPreview();
		}
		private void Button_printWarrant_Click(object sender, EventArgs e) //  Печать заказ-наряда
		{
			PrintCardWorkOrder print = new PrintCardWorkOrder(the_card, _calculatorCard);
			print.PrintPreview();
		}
        #endregion
    }
}
