using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using AutoBaseSql.TxtClasses;
using AutoBaseSql.Displaer;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormCard.
	/// </summary>
	public class FormCard : System.Windows.Forms.Form
	{
		// Общие
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.ColumnHeader columnHeader8;
		private System.Windows.Forms.ColumnHeader columnHeader9;
		private System.Windows.Forms.ColumnHeader columnHeader10;
		private System.Windows.Forms.ColumnHeader columnHeader11;
		private System.Windows.Forms.ColumnHeader columnHeader12;
		private System.Windows.Forms.ColumnHeader columnHeader13;
		private System.Windows.Forms.ColumnHeader columnHeader14;
		private System.Windows.Forms.ColumnHeader columnHeader15;
		private System.Windows.Forms.ColumnHeader columnHeader16;
		private System.Windows.Forms.ColumnHeader columnHeader17;
		private System.Windows.Forms.ColumnHeader columnHeader18;
		private System.Windows.Forms.ColumnHeader columnHeader19;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ListView listView2;	
		private System.Windows.Forms.ListView listView3;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.TabPage tabPage4;
		private System.Windows.Forms.Button buttonSave;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;

		// Основные параметры
		private System.Windows.Forms.TextBox textBoxAutoType;
		private System.Windows.Forms.Button buttonSelect;
		private System.Windows.Forms.TextBox textBoxClient;
		private System.Windows.Forms.Button buttonSelectClient;
		private System.Windows.Forms.TextBox textBoxAuto;
		private System.Windows.Forms.Button buttonSelectAuto;
		
		// Работы
		private System.Windows.Forms.Button buttonNewWork;
		private System.Windows.Forms.Button buttonGarantyOn;
		private System.Windows.Forms.Button buttonGuarantyOf;
		private System.Windows.Forms.Button buttonDone;
		private System.Windows.Forms.Button buttonUnDone;

		// Детали
		private System.Windows.Forms.Button buttonNewDetail;
		private System.Windows.Forms.Button buttonNewStorage;
		private System.Windows.Forms.Button buttonDelDetail;
		private System.Windows.Forms.Button buttonReserve;
		private System.Windows.Forms.Button buttonUnreserve;
		private System.Windows.Forms.Button buttonGuarantyOn;
		private System.Windows.Forms.Button buttonGuarantyOff;
		private System.Windows.Forms.Button buttonSetPrice;
		private System.Windows.Forms.Button buttonSetOuter;
		
		private DbCard card = null;
		private TextBox textBoxTmp;
		private ListViewItem textBoxItem;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBoxComment;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Button buttonSetCheck;
		private System.Windows.Forms.Button buttonSetNoOuter;
		private System.Windows.Forms.Button buttonSetNoCheck;
		private System.Windows.Forms.Button buttonSetOil;
		private System.Windows.Forms.Button buttonSetNoOil;
		private System.Windows.Forms.TabPage tabPage5;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox checkBoxTm1;
		private System.Windows.Forms.CheckBox checkBoxTm2;
		private System.Windows.Forms.CheckBox checkBoxTm3;
		private System.Windows.Forms.CheckBox checkBoxTm4;
		private System.Windows.Forms.CheckBox checkBoxTm5;
		private System.Windows.Forms.CheckBox checkBoxTm6;
		private System.Windows.Forms.CheckBox checkBoxTm7;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.CheckBox checkBoxTm8;
		private System.Windows.Forms.CheckBox checkBoxTm9;
		private System.Windows.Forms.CheckBox checkBoxTm10;
		private System.Windows.Forms.CheckBox checkBoxTm11;
		private System.Windows.Forms.CheckBox checkBoxTm12;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.CheckBox checkBoxTm13;
		private System.Windows.Forms.CheckBox checkBoxTm14;
		private System.Windows.Forms.CheckBox checkBoxTm15;
		private System.Windows.Forms.CheckBox checkBoxTm16;
		private System.Windows.Forms.CheckBox checkBoxTm17;
		private System.Windows.Forms.CheckBox checkBoxTm18;
		private System.Windows.Forms.CheckBox checkBoxTm19;
		private System.Windows.Forms.CheckBox checkBoxTm20;
		private System.Windows.Forms.CheckBox checkBoxTm21;
		private System.Windows.Forms.CheckBox checkBoxTm22;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBoxTime;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.CheckBox checkBoxCashless;
		private System.Windows.Forms.TabPage tabPage6;
		private System.Windows.Forms.ListView listView4;
		private System.Windows.Forms.ColumnHeader columnHeader20;
		private System.Windows.Forms.Button buttonNewRecomend;
		private System.Windows.Forms.Button buttonPropertyRecomend;
		private System.Windows.Forms.TextBox textBoxGuarantyTypeTxt;
		private System.Windows.Forms.Button buttonSelectGuarantyType;
		private System.Windows.Forms.Button button1;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textBoxSumWork;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox textBoxRepresent;
		private System.Windows.Forms.Button buttonSelectRepresent;
		private System.Windows.Forms.TextBox textBoxRepresentDocument;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox textBoxWorkshop;
		private System.Windows.Forms.Button buttonSelectWorkshop;
		private System.Windows.Forms.TextBox textBoxDetailSumm;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Button buttonChangePrice;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.TextBox textBoxPartner;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.TextBox textBoxPartnerAddress;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.TextBox textBoxPartnerPhone;
		private System.Windows.Forms.Button buttonSelectPartner;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;

		FormWorkSelect workSelect	= null;
		private System.Windows.Forms.CheckBox checkBox_inner_warranty;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.TextBox textBox_cashless;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.TextBox textBox_discount;
		private System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;

		DtPartnerProperty			partner_property = null;

		// Связь со внешним миром.....
		ListView		outer_list		= null;
		private System.Windows.Forms.Button button_close;
		private System.Windows.Forms.Button button_save_close;
		private System.Windows.Forms.TabPage tabPage7;
		private System.Windows.Forms.ColumnHeader columnHeader21;
		private System.Windows.Forms.ColumnHeader columnHeader22;
		private System.Windows.Forms.ColumnHeader columnHeader23;
		private System.Windows.Forms.ColumnHeader columnHeader24;
		private System.Windows.Forms.ColumnHeader columnHeader25;
		private System.Windows.Forms.Button button_new_request;
		private System.Windows.Forms.ListView listView_request;
		private System.Windows.Forms.Button button_selection;
		private System.Windows.Forms.Button button_selection1;
		private System.Windows.Forms.ColumnHeader columnHeader26;
		ListViewItem	outer_item		= null;
		private System.Windows.Forms.Button button_remove_claim;
		private System.Windows.Forms.TabPage tabPage8;
		private System.Windows.Forms.ListView listView5;
		private System.Windows.Forms.ColumnHeader columnHeader27;
		private System.Windows.Forms.ColumnHeader columnHeader28;
		private System.Windows.Forms.Button button_new_catalogue_detail;
		private System.Windows.Forms.Button button_comment;

		
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.TextBox textBox_licence_vehicle;
		private System.Windows.Forms.ContextMenu contextMenu2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.Button button_print_invite;
		private System.Windows.Forms.TextBox textBox_dsknt;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.TextBox textBox_workval;
		private System.Windows.Forms.CheckBox checkBox_return;
		private System.Windows.Forms.TextBox textBox_consultant;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.Button button_select_consultant;
		private System.Windows.Forms.CheckBox checkBox_creditcard;
		private System.Windows.Forms.Button button_set_present;
		private System.Windows.Forms.Button button_unset_present;
        private MenuItem menuItem4;
        private Label label21;
        private TextBox textBox_discountparts;
        private Button button_setdiscountparts;
        private Button button_set_discount_work;
        private Button button_to;
		public DtCard	the_card_change;
        private Button button_discount_detail;
        private Button button_discount;
        private Button button_worknopresent;
        private Button button_workpresent;

        private bool autocreate;

		public DtCard the_card;
        private TabPage tabPage9;
        private Label label_auto;
        private DtCardDisplay _display;
		private TxtCard _cardTxt;

		private DisplayCard _displayCard;
		
		public struct ClaimData
		{
			public long code;
			public string txt;
			public int count;
			public DateTime last;
		};

		// Создание привязок данных для формы отображения заказ-наряда
		private void AutoVinConvert(object sender, ConvertEventArgs e)
        {
			if (e.Value == null) e.Value = "Автомобиль не выбран внутри";
			else
			 e.Value = "VIN автомобиля - " + ((DtAuto)e.Value).VIN; 
        }
		private void CreateDataBindings()
        {
			//Binding autoBinding = new Binding("Text", _cardTxt, "AutoVIN");
			//Binding autoBinding = new Binding("Text", the_card, "DtAuto");
			//Binding autoBinding = new Binding("Text", the_card, "CodeAuto");
			//Binding autoBinding = new Binding("Text", card, "AutoTxt");

			//Binding autoBinding = new Binding("Text", the_card_change, "CodeAuto");
			//autoBinding.Format += AutoVinConvert;
			//autoBinding.NullValue = "Автомобиль не выбран";
			//autoBinding.DataSourceNullValue = "Ссылка на автомобиль = NULL";
			//label_auto.DataBindings.Add(autoBinding);		

			the_card.DisplayChanges += DisplayCardAuto;
			the_card.DisplayMemberChanges += DisplayChnages;
			
        }

		public void DisplayChnages(string name)
        {
			switch (name){
				case "AUTO":
					DisplayAuto();
					break;
				default:
					break;
            }
        }
		private void DisplayAuto()
        {
			Display.DisplayComponent(label_auto, _displayCard.AutoVIN);
        }

		private void DisplayCardAuto(object sender, string valueName)
        {
			if (valueName == "Auto")
			{
	//			DtAuto auto = (DtAuto)sender;
	//			if (sender == null)
	//				label_auto.Text = "АВТОМОБИЛЬ НЕ ВЫБРАН";
	//			else
	//				label_auto.Text = "VIN автомобиля - " + auto.VIN;
			}
        }

        #region Рефакторинг

		private DtCard LoadCard(DbCard source)
        {
			DtCard card;
			// НОВЫЙ ТИП ДАННЫХ
			if (source == null)
			{
				card = new DtCard();
			}
			else
			{
				card = DbSqlCard.Find(source.Number, source.Year);
				if (card != null)
				{
					// Проверка наличия ОБЯЗАТЕЛЬНЫХ примечаний
					long codeauto = (long)card.GetData("АВТОМОБИЛЬ_КАРТОЧКА");

					card.DtAuto = DbSqlAuto.Find(codeauto);

					if (codeauto > 0 && DbSqlAutoComment.IsUnexe(codeauto) == true)
					{
						UserInterface.ListAutoComment(0, (object)codeauto, 0, UserInterface.ClickType.Modify);
					}
					// Проверка предписаний по VIN автомобиля

		//			DbSqlCardClaim.SelectInList(listView_request, (long)card.GetData("НОМЕР_КАРТОЧКА"), (int)card.GetData("ГОД_КАРТОЧКА"));
		//			DbSqlCardDetailOrder.SelectInList(listView5, (long)card.GetData("НОМЕР_КАРТОЧКА"), (int)card.GetData("ГОД_КАРТОЧКА"));
					// Свидетельство о регистрации ТС
					if (card.CodeLicenseVihicle != 0)
					{
						CS_LicenceVehicle licence = DbSqlLicenceVehicle.Find(card.CodeLicenseVihicle);
						if (licence != null)
						{
							textBox_licence_vehicle.Text = licence.licence_series + " " + licence.licence_number + "        " + licence.vehicle_number + " " + licence.vehicle_region;
						}
						else
						{
							textBox_licence_vehicle.Text = "Ошибка";
						}
					}
					else
					{
						textBox_licence_vehicle.Text = "Не установленно";
					}
				}

				// Установка сервис-консультанта, если есть
				long service_manager_code = (long)card.GetData("СЕРВИС_КОНСУЛЬТАНТ");
				if (service_manager_code != 0)
				{
					DtStaff service_manager = DbSqlStaff.Find(service_manager_code);
					if (service_manager != null)
					{
						textBox_consultant.Text = service_manager.Title;
					}
					else
					{
						textBox_consultant.Text = "ОШИБКА";
					}
				}
			}
			return card;
		}

		private void DisplayCard(DtCard card)
        {
			DbSqlCardClaim.SelectInList(listView_request, card.Number, card.Year);
			DbSqlCardDetailOrder.SelectInList(listView5, card.Number, card.Year);

			checkBox_return.Checked = card.Returned;
			checkBox_creditcard.Checked = card.CreditCard; //checkBox_creditcard.Checked = (bool)card.GetData("КРЕДИТНАЯ_КАРТА_КАРТОЧКА");
			textBox_discountparts.Text = card.DiscountDetail.ToString(); //textBox_discountparts.Text = card.GetData("СКИДКА_ДЕТАЛЬ_КАРТОЧКА").ToString();
		}
		#endregion

		public FormCard(DbCard cardSource)
		{
            autocreate = false;
			InitializeComponent();

			the_card = LoadCard(cardSource);
			DisplayCard(the_card);

			_displayCard = new DisplayCard(the_card);
			CreateDataBindings();


			//// НОВЫЙ ТИП ДАННЫХ
			//if(cardSource == null)
			//{
			//	the_card = new DtCard();
			//	CreateDataBindings();
			//}
			//else
			//{
			//	the_card = DbSqlCard.Find(cardSource.Number, cardSource.Year);
			//	if(the_card != null)
			//	{
			//		CreateDataBindings();
			//		// Проверка наличия ОБЯЗАТЕЛЬНЫХ примечаний
			//		long codeauto = (long)the_card.GetData("АВТОМОБИЛЬ_КАРТОЧКА");

			//		the_card.DtAuto = DbSqlAuto.Find(codeauto);

			//		if(codeauto > 0 && DbSqlAutoComment.IsUnexe(codeauto) == true)
			//		{
			//			UserInterface.ListAutoComment(0, (object)codeauto, 0, UserInterface.ClickType.Modify);
			//		}
			//		// Проверка предписаний по VIN автомобиля

			//		DbSqlCardClaim.SelectInList(listView_request, (long)the_card.GetData("НОМЕР_КАРТОЧКА"), (int)the_card.GetData("ГОД_КАРТОЧКА"));
			//		DbSqlCardDetailOrder.SelectInList(listView5, (long)the_card.GetData("НОМЕР_КАРТОЧКА"), (int)the_card.GetData("ГОД_КАРТОЧКА"));
			//		// Свидетельство о регистрации ТС
			//		if(the_card.CodeLicenseVihicle != 0)
			//		{
			//			CS_LicenceVehicle licence = DbSqlLicenceVehicle.Find(the_card.CodeLicenseVihicle);
			//			if(licence != null)
			//			{
			//				textBox_licence_vehicle.Text = licence.licence_series + " " + licence.licence_number + "        " + licence.vehicle_number + " " + licence.vehicle_region;
			//			}
			//			else
			//			{
			//				textBox_licence_vehicle.Text = "Ошибка";
			//			}
			//		}
			//		else
			//		{
			//			textBox_licence_vehicle.Text = "Не установленно";
			//		}
			//	}

			//	// Установка сервис-консультанта, если есть
			//	long service_manager_code = (long)the_card.GetData("СЕРВИС_КОНСУЛЬТАНТ");
			//	if(service_manager_code != 0)
			//	{
			//		DtStaff service_manager = DbSqlStaff.Find(service_manager_code);
			//		if(service_manager != null)
			//		{
			//			textBox_consultant.Text = service_manager.Title;
			//		}
			//		else
			//		{
			//			textBox_consultant.Text = "ОШИБКА";
			//		}
			//	}
			//}



			//checkBox_return.Checked	= the_card.Returned;
			//checkBox_creditcard.Checked	= (bool)the_card.GetData("КРЕДИТНАЯ_КАРТА_КАРТОЧКА");
   //         textBox_discountparts.Text = the_card.GetData("СКИДКА_ДЕТАЛЬ_КАРТОЧКА").ToString();


			the_card_change = new DtCard(the_card);
			// КОНЕЦ НОВЫЙ ТИП ДАННЫХ

			if (cardSource == null)
			{
				card		= new DbCard();
				this.Text	= "Новая карточка заказа";

				
				textBoxRepresentDocument.Enabled	= false;	// Запрет изменения
				comboBox1.Visible					= false;	//		документа представителя
			}
			else
			{
				card = new DbCard(cardSource);
				this.Text = "Карточка заказа № " + card.NumberTxt;
				if(card.WarrantNumberTxt.Length != 0)
				{
					this.Text += " , Заказ-наряд № " + card.WarrantNumberTxt;
				}

				// Пробуем загрузить список работ карточки
				DbCardWork.FillList(WorkList, card);
				SetListIndexes(WorkList);
				MakeSumWork();

				// Новый способ
				//DtCardDisplay display = new DtCardDisplay(the_card);
				//display.DisplayCardWorks(WorkList);


				// Пробуем загрузить список деталей карточки
				DbCardDetail.FillList(listView3, card);
				SetListIndexes(listView3);
				//Выставляем сумму полученных деталей
				textBoxDetailSumm.Text	= Db.CachToTxt(SetDetailSumm());

				// Пробуем загрузить список рекомендаций
				DbCardRecomend.FillList(listView4, card);

				// Догружаем представителя
				if(card.CodeRepresent != 0)
					card.Represent	= DbPartner.Find(card.CodeRepresent);

				// Догружаем цех
				if(card.CodeWorkshop != 0)
					card.Workshop	= DbWorkshop.Find(card.CodeWorkshop);

				// Догружаем полный вид гарантии
				if(card.CodeGuarantyType != 0)
					card.GuarantyTypeFull	= DbGuarantyType.Find(card.CodeGuarantyType);

				if(card.CodeRepresent == 0)
				{
					textBoxRepresentDocument.Enabled	= false;	// Запрет изменения
					comboBox1.Visible					= false;	//		документа представителя
				}

				// Настройка разрешений для изменений
				if(!card.CanUpdateWarrantTitle)
				{
					buttonSelect.Visible				= false;	// Запрет изменения
					buttonSelectAuto.Visible			= false;	//		параметров
					buttonSelectClient.Visible			= false;	//			шапки
					buttonSelectRepresent.Visible		= false;	//
					textBoxRepresentDocument.Enabled	= false;	// Запрет изменения
					comboBox1.Visible					= false;	//		документа представителя

					textBoxPartner.Enabled				= false;
					buttonSelectPartner.Visible			= false;
				}
				if(!card.CanUpdateWarrant)
				{
					button_save_close.Visible	= false;	// Запрет записи карточки!
					buttonSave.Visible			= false;	// Запрет записи карточки!
					buttonNewWork.Enabled		= false;	// Запрет 
					buttonGarantyOn.Enabled		= false;	//	нажатия
					buttonGuarantyOf.Enabled	= false;	//		кнопок
					buttonDone.Enabled			= false;
					buttonUnDone.Enabled		= false;
					buttonNewDetail.Enabled		= false;
					buttonNewStorage.Enabled	= false;
					buttonDelDetail.Enabled		= false;
					buttonReserve.Enabled		= false;
					buttonUnreserve.Enabled		= false;
					buttonGuarantyOn.Enabled	= false;
					buttonGuarantyOff.Enabled	= false;
					buttonSetPrice.Enabled		= false;
					buttonSetOuter.Enabled		= false;
					buttonSetCheck.Enabled		= false;
					buttonSetNoOuter.Enabled	= false;
					buttonSetNoCheck.Enabled	= false;
					textBoxComment.Enabled		= false;	// Запрет измененея коментария
					checkBoxCashless.Enabled	= false;	// Запрет изменения типа расчета
					checkBox_creditcard.Enabled	= false;	// Запрет изменения типа расчета
					textBoxRepresentDocument.Enabled	= false;	// Запрет изменения
					comboBox1.Visible					= false;	//		документа представителя
					checkBox_inner_warranty.Enabled		= false;	// Запрет изменения типа заказ-наряда

					buttonNewRecomend.Enabled		= false;
					buttonPropertyRecomend.Enabled	= false;
					button_selection.Enabled		= false;
					checkBox_return.Enabled			= false;

					button_select_consultant.Enabled	= false;	// Запрет изменения сервис-консультанта
					textBox_consultant.Enabled			= false;	// Запрет изменения сервис-консультанта

					button_set_present.Enabled = false;
					button_unset_present.Enabled	= false;
                    button_setdiscountparts.Enabled = false;        // Нельзя установить скидку на запчасти карточки
                    button_discount_detail.Enabled = false;
                    button_to.Enabled = false;
				}

				// Установка сервис-консультанта
			
				// Если выбран владелец (контрагент) догружаем для него свойтсва
				if(card.CodePartner != 0) 
					partner_property = DbSqlPartnerProperty.Find(card.CodePartner);
				if(partner_property != null)
				{
					if(partner_property.Cashless == true)
						textBox_cashless.Text = "Безналичный";
					else
						textBox_cashless.Text = "Наличный";
				}
				else
				{
					textBox_cashless.Text = "Наличный";
				}
			}

			// Установка Владельца
			RestorePartner();
			textBoxAutoType.Text		= card.AutoTypeTxt;
			textBoxAuto.Text			= card.AutoTxt;
			textBoxClient.Text			= card.PartnerNameTxt;
			textBoxRepresent.Text		= card.RepresentNameTxt;
			textBoxRepresentDocument.Text		= card.RepresentDocument;
			textBoxComment.Text			= card.Comment;
			checkBoxCashless.Checked	= card.Cashless;
			checkBox_inner_warranty.Checked	= card.InnerWarranty;
			textBoxGuarantyTypeTxt.Text	= card.GuarantyTypeTxt;
			textBoxWorkshop.Text		= card.WorkshopTxt;
			textBox_discount.Text		= card.DiscountWorkTxt;
            textBox_dsknt.Text = card.DiscountWorkTxt;
           

			// Установка выполняемых работ
			if((card.Works & (1)) > 0) checkBoxTm1.Checked = true;
			if((card.Works & (1 << 1)) > 0) checkBoxTm2.Checked = true;
			if((card.Works & (1 << 2)) > 0) checkBoxTm3.Checked = true;
			if((card.Works & (1 << 3)) > 0) checkBoxTm4.Checked = true;
			if((card.Works & (1 << 4)) > 0) checkBoxTm5.Checked = true;
			if((card.Works & (1 << 5)) > 0) checkBoxTm6.Checked = true;
			if((card.Works & (1 << 6)) > 0) checkBoxTm7.Checked = true;
			if((card.Works & (1 << 7)) > 0) checkBoxTm8.Checked = true;
			if((card.Works & (1 << 8)) > 0) checkBoxTm9.Checked = true;
			if((card.Works & (1 << 9)) > 0) checkBoxTm10.Checked = true;
			if((card.Works & (1 << 10)) > 0) checkBoxTm11.Checked = true;
			if((card.Works & (1 << 11)) > 0) checkBoxTm12.Checked = true;
			if((card.Works & (1 << 12)) > 0) checkBoxTm13.Checked = true;
			if((card.Works & (1 << 13)) > 0) checkBoxTm14.Checked = true;
			if((card.Works & (1 << 14)) > 0) checkBoxTm15.Checked = true;
			if((card.Works & (1 << 15)) > 0) checkBoxTm16.Checked = true;
			if((card.Works & (1 << 16)) > 0) checkBoxTm17.Checked = true;
			if((card.Works & (1 << 17)) > 0) checkBoxTm18.Checked = true;
			if((card.Works & (1 << 18)) > 0) checkBoxTm19.Checked = true;
			if((card.Works & (1 << 19)) > 0) checkBoxTm20.Checked = true;
			if((card.Works & (1 << 20)) > 0) checkBoxTm21.Checked = true;
			if((card.Works & (1 << 21)) > 0) checkBoxTm22.Checked = true;
			textBoxTime.Text = card.TimeTxt;

			_display = new DtCardDisplay(the_card);

			// Работа через DATA BINDINGS
			// Запускаем создание привязок данных
			_cardTxt = new TxtCard(the_card);
			

		}

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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCard));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.button_set_discount_work = new System.Windows.Forms.Button();
            this.button_setdiscountparts = new System.Windows.Forms.Button();
            this.label21 = new System.Windows.Forms.Label();
            this.textBox_discountparts = new System.Windows.Forms.TextBox();
            this.checkBox_creditcard = new System.Windows.Forms.CheckBox();
            this.button_select_consultant = new System.Windows.Forms.Button();
            this.label20 = new System.Windows.Forms.Label();
            this.textBox_consultant = new System.Windows.Forms.TextBox();
            this.checkBox_return = new System.Windows.Forms.CheckBox();
            this.textBox_workval = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.textBox_dsknt = new System.Windows.Forms.TextBox();
            this.textBox_licence_vehicle = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.checkBox_inner_warranty = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.textBox_discount = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.textBox_cashless = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.textBoxPartnerPhone = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.textBoxPartnerAddress = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.textBoxPartner = new System.Windows.Forms.TextBox();
            this.buttonSelectPartner = new System.Windows.Forms.Button();
            this.buttonSelectWorkshop = new System.Windows.Forms.Button();
            this.textBoxWorkshop = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxRepresentDocument = new System.Windows.Forms.TextBox();
            this.buttonSelectRepresent = new System.Windows.Forms.Button();
            this.textBoxRepresent = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.buttonSelectGuarantyType = new System.Windows.Forms.Button();
            this.textBoxGuarantyTypeTxt = new System.Windows.Forms.TextBox();
            this.checkBoxCashless = new System.Windows.Forms.CheckBox();
            this.textBoxComment = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonSelectAuto = new System.Windows.Forms.Button();
            this.textBoxAuto = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxClient = new System.Windows.Forms.TextBox();
            this.buttonSelect = new System.Windows.Forms.Button();
            this.textBoxAutoType = new System.Windows.Forms.TextBox();
            this.buttonSelectClient = new System.Windows.Forms.Button();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.button_discount_detail = new System.Windows.Forms.Button();
            this.button_to = new System.Windows.Forms.Button();
            this.button_unset_present = new System.Windows.Forms.Button();
            this.button_set_present = new System.Windows.Forms.Button();
            this.button_selection = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.textBoxDetailSumm = new System.Windows.Forms.TextBox();
            this.buttonSetNoCheck = new System.Windows.Forms.Button();
            this.buttonSetNoOuter = new System.Windows.Forms.Button();
            this.buttonSetCheck = new System.Windows.Forms.Button();
            this.buttonSetOuter = new System.Windows.Forms.Button();
            this.buttonSetPrice = new System.Windows.Forms.Button();
            this.buttonGuarantyOff = new System.Windows.Forms.Button();
            this.buttonGuarantyOn = new System.Windows.Forms.Button();
            this.buttonUnreserve = new System.Windows.Forms.Button();
            this.buttonReserve = new System.Windows.Forms.Button();
            this.buttonDelDetail = new System.Windows.Forms.Button();
            this.buttonNewStorage = new System.Windows.Forms.Button();
            this.buttonNewDetail = new System.Windows.Forms.Button();
            this.listView3 = new System.Windows.Forms.ListView();
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader15 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader16 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader17 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader18 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button_worknopresent = new System.Windows.Forms.Button();
            this.button_workpresent = new System.Windows.Forms.Button();
            this.button_discount = new System.Windows.Forms.Button();
            this.button_comment = new System.Windows.Forms.Button();
            this.button_selection1 = new System.Windows.Forms.Button();
            this.buttonChangePrice = new System.Windows.Forms.Button();
            this.textBoxSumWork = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonSetNoOil = new System.Windows.Forms.Button();
            this.buttonSetOil = new System.Windows.Forms.Button();
            this.buttonUnDone = new System.Windows.Forms.Button();
            this.buttonDone = new System.Windows.Forms.Button();
            this.buttonGuarantyOf = new System.Windows.Forms.Button();
            this.buttonGarantyOn = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader19 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonNewWork = new System.Windows.Forms.Button();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.buttonPropertyRecomend = new System.Windows.Forms.Button();
            this.buttonNewRecomend = new System.Windows.Forms.Button();
            this.listView4 = new System.Windows.Forms.ListView();
            this.columnHeader20 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage8 = new System.Windows.Forms.TabPage();
            this.button_new_catalogue_detail = new System.Windows.Forms.Button();
            this.listView5 = new System.Windows.Forms.ListView();
            this.columnHeader27 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader28 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.button_remove_claim = new System.Windows.Forms.Button();
            this.button_new_request = new System.Windows.Forms.Button();
            this.listView_request = new System.Windows.Forms.ListView();
            this.columnHeader21 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader22 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader23 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader24 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader25 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader26 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxTime = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBoxTm22 = new System.Windows.Forms.CheckBox();
            this.checkBoxTm21 = new System.Windows.Forms.CheckBox();
            this.checkBoxTm20 = new System.Windows.Forms.CheckBox();
            this.checkBoxTm19 = new System.Windows.Forms.CheckBox();
            this.checkBoxTm18 = new System.Windows.Forms.CheckBox();
            this.checkBoxTm17 = new System.Windows.Forms.CheckBox();
            this.checkBoxTm16 = new System.Windows.Forms.CheckBox();
            this.checkBoxTm15 = new System.Windows.Forms.CheckBox();
            this.checkBoxTm14 = new System.Windows.Forms.CheckBox();
            this.checkBoxTm13 = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBoxTm12 = new System.Windows.Forms.CheckBox();
            this.checkBoxTm11 = new System.Windows.Forms.CheckBox();
            this.checkBoxTm10 = new System.Windows.Forms.CheckBox();
            this.checkBoxTm9 = new System.Windows.Forms.CheckBox();
            this.checkBoxTm8 = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxTm1 = new System.Windows.Forms.CheckBox();
            this.checkBoxTm2 = new System.Windows.Forms.CheckBox();
            this.checkBoxTm3 = new System.Windows.Forms.CheckBox();
            this.checkBoxTm4 = new System.Windows.Forms.CheckBox();
            this.checkBoxTm5 = new System.Windows.Forms.CheckBox();
            this.checkBoxTm6 = new System.Windows.Forms.CheckBox();
            this.checkBoxTm7 = new System.Windows.Forms.CheckBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.listView2 = new System.Windows.Forms.ListView();
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonSave = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenu1 = new System.Windows.Forms.ContextMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.button_close = new System.Windows.Forms.Button();
            this.button_save_close = new System.Windows.Forms.Button();
            this.contextMenu2 = new System.Windows.Forms.ContextMenu();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.button_print_invite = new System.Windows.Forms.Button();
            this.tabPage9 = new System.Windows.Forms.TabPage();
            this.label_auto = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.tabPage8.SuspendLayout();
            this.tabPage7.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage9.SuspendLayout();
            this.SuspendLayout();
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
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage9);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tabControl1.Location = new System.Drawing.Point(10, 9);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(918, 744);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.button_set_discount_work);
            this.tabPage1.Controls.Add(this.button_setdiscountparts);
            this.tabPage1.Controls.Add(this.label21);
            this.tabPage1.Controls.Add(this.textBox_discountparts);
            this.tabPage1.Controls.Add(this.checkBox_creditcard);
            this.tabPage1.Controls.Add(this.button_select_consultant);
            this.tabPage1.Controls.Add(this.label20);
            this.tabPage1.Controls.Add(this.textBox_consultant);
            this.tabPage1.Controls.Add(this.checkBox_return);
            this.tabPage1.Controls.Add(this.textBox_workval);
            this.tabPage1.Controls.Add(this.label19);
            this.tabPage1.Controls.Add(this.label18);
            this.tabPage1.Controls.Add(this.textBox_dsknt);
            this.tabPage1.Controls.Add(this.textBox_licence_vehicle);
            this.tabPage1.Controls.Add(this.label17);
            this.tabPage1.Controls.Add(this.checkBox_inner_warranty);
            this.tabPage1.Controls.Add(this.groupBox4);
            this.tabPage1.Controls.Add(this.buttonSelectWorkshop);
            this.tabPage1.Controls.Add(this.textBoxWorkshop);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.comboBox1);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.textBoxRepresentDocument);
            this.tabPage1.Controls.Add(this.buttonSelectRepresent);
            this.tabPage1.Controls.Add(this.textBoxRepresent);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.buttonSelectGuarantyType);
            this.tabPage1.Controls.Add(this.textBoxGuarantyTypeTxt);
            this.tabPage1.Controls.Add(this.checkBoxCashless);
            this.tabPage1.Controls.Add(this.textBoxComment);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.buttonSelectAuto);
            this.tabPage1.Controls.Add(this.textBoxAuto);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.textBoxClient);
            this.tabPage1.Controls.Add(this.buttonSelect);
            this.tabPage1.Controls.Add(this.textBoxAutoType);
            this.tabPage1.Controls.Add(this.buttonSelectClient);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(910, 711);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Шапка";
            // 
            // button_set_discount_work
            // 
            this.button_set_discount_work.Location = new System.Drawing.Point(13, 648);
            this.button_set_discount_work.Name = "button_set_discount_work";
            this.button_set_discount_work.Size = new System.Drawing.Size(160, 27);
            this.button_set_discount_work.TabIndex = 38;
            this.button_set_discount_work.Text = "Установить";
            this.button_set_discount_work.UseVisualStyleBackColor = true;
            this.button_set_discount_work.Click += new System.EventHandler(this.button_set_discount_work_Click);
            // 
            // button_setdiscountparts
            // 
            this.button_setdiscountparts.Location = new System.Drawing.Point(715, 652);
            this.button_setdiscountparts.Name = "button_setdiscountparts";
            this.button_setdiscountparts.Size = new System.Drawing.Size(120, 26);
            this.button_setdiscountparts.TabIndex = 37;
            this.button_setdiscountparts.Text = "Установить";
            this.button_setdiscountparts.UseVisualStyleBackColor = true;
            this.button_setdiscountparts.Click += new System.EventHandler(this.button_setdiscountparts_Click);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(602, 623);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(106, 20);
            this.label21.TabIndex = 36;
            this.label21.Text = "Скидка З/Ч";
            // 
            // textBox_discountparts
            // 
            this.textBox_discountparts.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox_discountparts.Location = new System.Drawing.Point(715, 618);
            this.textBox_discountparts.Name = "textBox_discountparts";
            this.textBox_discountparts.Size = new System.Drawing.Size(120, 26);
            this.textBox_discountparts.TabIndex = 35;
            // 
            // checkBox_creditcard
            // 
            this.checkBox_creditcard.Location = new System.Drawing.Point(461, 489);
            this.checkBox_creditcard.Name = "checkBox_creditcard";
            this.checkBox_creditcard.Size = new System.Drawing.Size(163, 28);
            this.checkBox_creditcard.TabIndex = 34;
            this.checkBox_creditcard.Text = "Кредитная карта";
            // 
            // button_select_consultant
            // 
            this.button_select_consultant.Location = new System.Drawing.Point(595, 655);
            this.button_select_consultant.Name = "button_select_consultant";
            this.button_select_consultant.Size = new System.Drawing.Size(29, 27);
            this.button_select_consultant.TabIndex = 33;
            this.button_select_consultant.Text = "...";
            this.button_select_consultant.Click += new System.EventHandler(this.button_select_consultant_Click);
            // 
            // label20
            // 
            this.label20.Location = new System.Drawing.Point(193, 655);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(192, 27);
            this.label20.TabIndex = 32;
            this.label20.Text = "Сервис-консультант";
            // 
            // textBox_consultant
            // 
            this.textBox_consultant.Location = new System.Drawing.Point(392, 655);
            this.textBox_consultant.Name = "textBox_consultant";
            this.textBox_consultant.Size = new System.Drawing.Size(198, 26);
            this.textBox_consultant.TabIndex = 31;
            // 
            // checkBox_return
            // 
            this.checkBox_return.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBox_return.ForeColor = System.Drawing.Color.OrangeRed;
            this.checkBox_return.Location = new System.Drawing.Point(653, 489);
            this.checkBox_return.Name = "checkBox_return";
            this.checkBox_return.Size = new System.Drawing.Size(134, 28);
            this.checkBox_return.TabIndex = 30;
            this.checkBox_return.Text = "ВОЗВРАТ!!!!";
            // 
            // textBox_workval
            // 
            this.textBox_workval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox_workval.Location = new System.Drawing.Point(461, 619);
            this.textBox_workval.Name = "textBox_workval";
            this.textBox_workval.Size = new System.Drawing.Size(120, 26);
            this.textBox_workval.TabIndex = 29;
            // 
            // label19
            // 
            this.label19.Location = new System.Drawing.Point(317, 618);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(134, 27);
            this.label19.TabIndex = 28;
            this.label19.Text = "Стоимость Н/Ч";
            // 
            // label18
            // 
            this.label18.Location = new System.Drawing.Point(10, 618);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(144, 27);
            this.label18.TabIndex = 27;
            this.label18.Text = "Скидка (работы)";
            // 
            // textBox_dsknt
            // 
            this.textBox_dsknt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox_dsknt.Location = new System.Drawing.Point(173, 619);
            this.textBox_dsknt.Name = "textBox_dsknt";
            this.textBox_dsknt.Size = new System.Drawing.Size(120, 26);
            this.textBox_dsknt.TabIndex = 26;
            // 
            // textBox_licence_vehicle
            // 
            this.textBox_licence_vehicle.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_licence_vehicle.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_licence_vehicle.Location = new System.Drawing.Point(298, 591);
            this.textBox_licence_vehicle.Name = "textBox_licence_vehicle";
            this.textBox_licence_vehicle.ReadOnly = true;
            this.textBox_licence_vehicle.Size = new System.Drawing.Size(470, 23);
            this.textBox_licence_vehicle.TabIndex = 25;
            // 
            // label17
            // 
            this.label17.Location = new System.Drawing.Point(10, 591);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(278, 26);
            this.label17.TabIndex = 24;
            this.label17.Text = "Свидетельство о регистрации ТС";
            // 
            // checkBox_inner_warranty
            // 
            this.checkBox_inner_warranty.Location = new System.Drawing.Point(211, 489);
            this.checkBox_inner_warranty.Name = "checkBox_inner_warranty";
            this.checkBox_inner_warranty.Size = new System.Drawing.Size(231, 28);
            this.checkBox_inner_warranty.TabIndex = 23;
            this.checkBox_inner_warranty.Text = "Внутренний заказ/наряд";
            this.checkBox_inner_warranty.CheckedChanged += new System.EventHandler(this.checkBox_inner_warranty_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.textBox_discount);
            this.groupBox4.Controls.Add(this.label16);
            this.groupBox4.Controls.Add(this.textBox_cashless);
            this.groupBox4.Controls.Add(this.label15);
            this.groupBox4.Controls.Add(this.textBoxPartnerPhone);
            this.groupBox4.Controls.Add(this.label14);
            this.groupBox4.Controls.Add(this.textBoxPartnerAddress);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.textBoxPartner);
            this.groupBox4.Controls.Add(this.buttonSelectPartner);
            this.groupBox4.Location = new System.Drawing.Point(10, 9);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(879, 185);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Владелец";
            // 
            // textBox_discount
            // 
            this.textBox_discount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_discount.Enabled = false;
            this.textBox_discount.Location = new System.Drawing.Point(557, 138);
            this.textBox_discount.Name = "textBox_discount";
            this.textBox_discount.ReadOnly = true;
            this.textBox_discount.Size = new System.Drawing.Size(144, 26);
            this.textBox_discount.TabIndex = 28;
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(365, 138);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(182, 27);
            this.label16.TabIndex = 27;
            this.label16.Text = "Скидка на работы";
            // 
            // textBox_cashless
            // 
            this.textBox_cashless.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_cashless.Enabled = false;
            this.textBox_cashless.Location = new System.Drawing.Point(211, 138);
            this.textBox_cashless.Name = "textBox_cashless";
            this.textBox_cashless.ReadOnly = true;
            this.textBox_cashless.Size = new System.Drawing.Size(125, 26);
            this.textBox_cashless.TabIndex = 26;
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(10, 138);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(192, 27);
            this.label15.TabIndex = 25;
            this.label15.Text = "Основной вид расчета";
            // 
            // textBoxPartnerPhone
            // 
            this.textBoxPartnerPhone.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPartnerPhone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxPartnerPhone.Location = new System.Drawing.Point(182, 113);
            this.textBoxPartnerPhone.Name = "textBoxPartnerPhone";
            this.textBoxPartnerPhone.ReadOnly = true;
            this.textBoxPartnerPhone.Size = new System.Drawing.Size(659, 26);
            this.textBoxPartnerPhone.TabIndex = 5;
            this.textBoxPartnerPhone.TabStop = false;
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(10, 92);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(120, 27);
            this.label14.TabIndex = 4;
            this.label14.Text = "Телефоны";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxPartnerAddress
            // 
            this.textBoxPartnerAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPartnerAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxPartnerAddress.Location = new System.Drawing.Point(182, 76);
            this.textBoxPartnerAddress.Name = "textBoxPartnerAddress";
            this.textBoxPartnerAddress.ReadOnly = true;
            this.textBoxPartnerAddress.Size = new System.Drawing.Size(659, 26);
            this.textBoxPartnerAddress.TabIndex = 3;
            this.textBoxPartnerAddress.TabStop = false;
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(10, 55);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(76, 27);
            this.label13.TabIndex = 2;
            this.label13.Text = "Адрес";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(10, 18);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(172, 27);
            this.label12.TabIndex = 1;
            this.label12.Text = "ФИО/Наименование";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxPartner
            // 
            this.textBoxPartner.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPartner.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxPartner.Location = new System.Drawing.Point(182, 39);
            this.textBoxPartner.Name = "textBoxPartner";
            this.textBoxPartner.Size = new System.Drawing.Size(659, 26);
            this.textBoxPartner.TabIndex = 0;
            this.textBoxPartner.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxPartner_KeyDown);
            this.textBoxPartner.LostFocus += new System.EventHandler(this.textBoxPartner_LostFocus);
            // 
            // buttonSelectPartner
            // 
            this.buttonSelectPartner.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSelectPartner.Location = new System.Drawing.Point(841, 39);
            this.buttonSelectPartner.Name = "buttonSelectPartner";
            this.buttonSelectPartner.Size = new System.Drawing.Size(29, 27);
            this.buttonSelectPartner.TabIndex = 24;
            this.buttonSelectPartner.TabStop = false;
            this.buttonSelectPartner.Text = "...";
            this.buttonSelectPartner.Click += new System.EventHandler(this.buttonSelectPartner_Click);
            // 
            // buttonSelectWorkshop
            // 
            this.buttonSelectWorkshop.Location = new System.Drawing.Point(730, 554);
            this.buttonSelectWorkshop.Name = "buttonSelectWorkshop";
            this.buttonSelectWorkshop.Size = new System.Drawing.Size(28, 26);
            this.buttonSelectWorkshop.TabIndex = 22;
            this.buttonSelectWorkshop.Text = "...";
            this.buttonSelectWorkshop.Click += new System.EventHandler(this.buttonSelectWorkshop_Click);
            // 
            // textBoxWorkshop
            // 
            this.textBoxWorkshop.Location = new System.Drawing.Point(355, 554);
            this.textBoxWorkshop.Name = "textBoxWorkshop";
            this.textBoxWorkshop.ReadOnly = true;
            this.textBoxWorkshop.Size = new System.Drawing.Size(375, 26);
            this.textBoxWorkshop.TabIndex = 21;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(355, 517);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(154, 26);
            this.label10.TabIndex = 20;
            this.label10.Text = "Подразделение";
            // 
            // comboBox1
            // 
            this.comboBox1.Items.AddRange(new object[] {
            "Доверенность №",
            "Доверенность без номера",
            "Рукописная доверенность",
            "Лизинговый договор №",
            "Путевой лист №"});
            this.comboBox1.Location = new System.Drawing.Point(509, 323);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(192, 28);
            this.comboBox1.TabIndex = 19;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(10, 295);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(211, 27);
            this.label9.TabIndex = 18;
            this.label9.Text = "Документ представителя";
            // 
            // textBoxRepresentDocument
            // 
            this.textBoxRepresentDocument.Location = new System.Drawing.Point(10, 323);
            this.textBoxRepresentDocument.Name = "textBoxRepresentDocument";
            this.textBoxRepresentDocument.Size = new System.Drawing.Size(499, 26);
            this.textBoxRepresentDocument.TabIndex = 17;
            // 
            // buttonSelectRepresent
            // 
            this.buttonSelectRepresent.Location = new System.Drawing.Point(730, 268);
            this.buttonSelectRepresent.Name = "buttonSelectRepresent";
            this.buttonSelectRepresent.Size = new System.Drawing.Size(28, 27);
            this.buttonSelectRepresent.TabIndex = 16;
            this.buttonSelectRepresent.Text = "...";
            this.buttonSelectRepresent.Click += new System.EventHandler(this.buttonSelectRepresent_Click);
            // 
            // textBoxRepresent
            // 
            this.textBoxRepresent.Location = new System.Drawing.Point(154, 268);
            this.textBoxRepresent.Name = "textBoxRepresent";
            this.textBoxRepresent.ReadOnly = true;
            this.textBoxRepresent.Size = new System.Drawing.Size(576, 26);
            this.textBoxRepresent.TabIndex = 15;
            this.textBoxRepresent.Text = "textBox1";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(10, 268);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(144, 26);
            this.label8.TabIndex = 14;
            this.label8.Text = "Представитель";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(10, 526);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(120, 27);
            this.label7.TabIndex = 13;
            this.label7.Text = "Вид гарантии";
            // 
            // buttonSelectGuarantyType
            // 
            this.buttonSelectGuarantyType.Location = new System.Drawing.Point(298, 554);
            this.buttonSelectGuarantyType.Name = "buttonSelectGuarantyType";
            this.buttonSelectGuarantyType.Size = new System.Drawing.Size(28, 26);
            this.buttonSelectGuarantyType.TabIndex = 12;
            this.buttonSelectGuarantyType.Text = "...";
            this.buttonSelectGuarantyType.Click += new System.EventHandler(this.buttonSelectGuarantyType_Click);
            // 
            // textBoxGuarantyTypeTxt
            // 
            this.textBoxGuarantyTypeTxt.Location = new System.Drawing.Point(10, 554);
            this.textBoxGuarantyTypeTxt.Name = "textBoxGuarantyTypeTxt";
            this.textBoxGuarantyTypeTxt.ReadOnly = true;
            this.textBoxGuarantyTypeTxt.Size = new System.Drawing.Size(288, 26);
            this.textBoxGuarantyTypeTxt.TabIndex = 11;
            // 
            // checkBoxCashless
            // 
            this.checkBoxCashless.Location = new System.Drawing.Point(10, 489);
            this.checkBoxCashless.Name = "checkBoxCashless";
            this.checkBoxCashless.Size = new System.Drawing.Size(201, 28);
            this.checkBoxCashless.TabIndex = 10;
            this.checkBoxCashless.Text = "Безналичный расчет";
            this.checkBoxCashless.CheckedChanged += new System.EventHandler(this.checkBoxCashless_CheckedChanged);
            // 
            // textBoxComment
            // 
            this.textBoxComment.Location = new System.Drawing.Point(10, 443);
            this.textBoxComment.Name = "textBoxComment";
            this.textBoxComment.Size = new System.Drawing.Size(758, 26);
            this.textBoxComment.TabIndex = 9;
            this.textBoxComment.Text = "textBox1";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(10, 415);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 27);
            this.label3.TabIndex = 8;
            this.label3.Text = "Примечание";
            // 
            // buttonSelectAuto
            // 
            this.buttonSelectAuto.Location = new System.Drawing.Point(730, 240);
            this.buttonSelectAuto.Name = "buttonSelectAuto";
            this.buttonSelectAuto.Size = new System.Drawing.Size(28, 27);
            this.buttonSelectAuto.TabIndex = 7;
            this.buttonSelectAuto.Text = "...";
            this.buttonSelectAuto.Click += new System.EventHandler(this.buttonSelectAuto_Click);
            // 
            // textBoxAuto
            // 
            this.textBoxAuto.Location = new System.Drawing.Point(154, 240);
            this.textBoxAuto.Name = "textBoxAuto";
            this.textBoxAuto.ReadOnly = true;
            this.textBoxAuto.Size = new System.Drawing.Size(576, 26);
            this.textBoxAuto.TabIndex = 6;
            this.textBoxAuto.Text = "textBox1";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(10, 240);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 27);
            this.label2.TabIndex = 5;
            this.label2.Text = "Автомобиль";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(10, 212);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 27);
            this.label1.TabIndex = 3;
            this.label1.Text = "Владелец";
            this.label1.Visible = false;
            // 
            // textBoxClient
            // 
            this.textBoxClient.Location = new System.Drawing.Point(154, 212);
            this.textBoxClient.Name = "textBoxClient";
            this.textBoxClient.ReadOnly = true;
            this.textBoxClient.Size = new System.Drawing.Size(576, 26);
            this.textBoxClient.TabIndex = 2;
            this.textBoxClient.Text = "textBox1";
            this.textBoxClient.Visible = false;
            // 
            // buttonSelect
            // 
            this.buttonSelect.Location = new System.Drawing.Point(470, 378);
            this.buttonSelect.Name = "buttonSelect";
            this.buttonSelect.Size = new System.Drawing.Size(90, 27);
            this.buttonSelect.TabIndex = 1;
            this.buttonSelect.Text = "Выбор";
            this.buttonSelect.Click += new System.EventHandler(this.buttonSelect_Click);
            // 
            // textBoxAutoType
            // 
            this.textBoxAutoType.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxAutoType.Location = new System.Drawing.Point(10, 378);
            this.textBoxAutoType.Name = "textBoxAutoType";
            this.textBoxAutoType.ReadOnly = true;
            this.textBoxAutoType.Size = new System.Drawing.Size(441, 26);
            this.textBoxAutoType.TabIndex = 0;
            this.textBoxAutoType.Text = "Марка автомобиля";
            // 
            // buttonSelectClient
            // 
            this.buttonSelectClient.Location = new System.Drawing.Point(730, 212);
            this.buttonSelectClient.Name = "buttonSelectClient";
            this.buttonSelectClient.Size = new System.Drawing.Size(28, 27);
            this.buttonSelectClient.TabIndex = 4;
            this.buttonSelectClient.Text = "...";
            this.buttonSelectClient.Visible = false;
            this.buttonSelectClient.Click += new System.EventHandler(this.buttonSelectClient_Click);
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
            this.tabPage4.Controls.Add(this.buttonNewDetail);
            this.tabPage4.Controls.Add(this.listView3);
            this.tabPage4.Location = new System.Drawing.Point(4, 29);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(910, 711);
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
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label11.Location = new System.Drawing.Point(640, 665);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(86, 27);
            this.label11.TabIndex = 14;
            this.label11.Text = "ИТОГО:";
            // 
            // textBoxDetailSumm
            // 
            this.textBoxDetailSumm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDetailSumm.Location = new System.Drawing.Point(745, 665);
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
            // buttonNewDetail
            // 
            this.buttonNewDetail.Image = ((System.Drawing.Image)(resources.GetObject("buttonNewDetail.Image")));
            this.buttonNewDetail.Location = new System.Drawing.Point(10, 9);
            this.buttonNewDetail.Name = "buttonNewDetail";
            this.buttonNewDetail.Size = new System.Drawing.Size(28, 27);
            this.buttonNewDetail.TabIndex = 1;
            this.toolTip1.SetToolTip(this.buttonNewDetail, "Новая деталь");
            this.buttonNewDetail.Click += new System.EventHandler(this.buttonNewDetail_Click);
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
            this.listView3.Size = new System.Drawing.Size(889, 619);
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
            this.tabPage2.Size = new System.Drawing.Size(910, 711);
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
            this.button_worknopresent.Click += new System.EventHandler(this.button_worknopresent_Click);
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
            this.button_workpresent.Click += new System.EventHandler(this.button_workpresent_Click);
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
            this.button_comment.Click += new System.EventHandler(this.button_comment_Click);
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
            this.textBoxSumWork.Location = new System.Drawing.Point(736, 674);
            this.textBoxSumWork.Name = "textBoxSumWork";
            this.textBoxSumWork.ReadOnly = true;
            this.textBoxSumWork.Size = new System.Drawing.Size(153, 26);
            this.textBoxSumWork.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(630, 674);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 27);
            this.label6.TabIndex = 9;
            this.label6.Text = "ИТОГО:";
            // 
            // button1
            // 
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(384, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(29, 27);
            this.button1.TabIndex = 8;
            this.toolTip1.SetToolTip(this.button1, "Новая форма добавления работ");
            this.button1.Click += new System.EventHandler(this.button1_Click);
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
            // buttonGuarantyOf
            // 
            this.buttonGuarantyOf.Image = ((System.Drawing.Image)(resources.GetObject("buttonGuarantyOf.Image")));
            this.buttonGuarantyOf.Location = new System.Drawing.Point(67, 9);
            this.buttonGuarantyOf.Name = "buttonGuarantyOf";
            this.buttonGuarantyOf.Size = new System.Drawing.Size(29, 27);
            this.buttonGuarantyOf.TabIndex = 3;
            this.toolTip1.SetToolTip(this.buttonGuarantyOf, "Отметить работы как негарантийные");
            this.buttonGuarantyOf.Click += new System.EventHandler(this.buttonGuarantyOf_Click);
            // 
            // buttonGarantyOn
            // 
            this.buttonGarantyOn.Image = ((System.Drawing.Image)(resources.GetObject("buttonGarantyOn.Image")));
            this.buttonGarantyOn.Location = new System.Drawing.Point(38, 9);
            this.buttonGarantyOn.Name = "buttonGarantyOn";
            this.buttonGarantyOn.Size = new System.Drawing.Size(29, 27);
            this.buttonGarantyOn.TabIndex = 2;
            this.toolTip1.SetToolTip(this.buttonGarantyOn, "Отметить работы как гаранийные");
            this.buttonGarantyOn.Click += new System.EventHandler(this.buttonGarantyOn_Click);
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
            this.listView1.Size = new System.Drawing.Size(889, 619);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SizeChanged += new System.EventHandler(this.listView1_SizeChanged);
            this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
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
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.buttonPropertyRecomend);
            this.tabPage6.Controls.Add(this.buttonNewRecomend);
            this.tabPage6.Controls.Add(this.listView4);
            this.tabPage6.Location = new System.Drawing.Point(4, 29);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new System.Drawing.Size(910, 711);
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
            // tabPage8
            // 
            this.tabPage8.Controls.Add(this.button_new_catalogue_detail);
            this.tabPage8.Controls.Add(this.listView5);
            this.tabPage8.Location = new System.Drawing.Point(4, 29);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Size = new System.Drawing.Size(910, 711);
            this.tabPage8.TabIndex = 7;
            this.tabPage8.Text = "Заявка на запчасти";
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
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.button_remove_claim);
            this.tabPage7.Controls.Add(this.button_new_request);
            this.tabPage7.Controls.Add(this.listView_request);
            this.tabPage7.Location = new System.Drawing.Point(4, 29);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Size = new System.Drawing.Size(910, 711);
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
            this.button_remove_claim.Click += new System.EventHandler(this.button_remove_claim_Click);
            // 
            // button_new_request
            // 
            this.button_new_request.Image = ((System.Drawing.Image)(resources.GetObject("button_new_request.Image")));
            this.button_new_request.Location = new System.Drawing.Point(10, 9);
            this.button_new_request.Name = "button_new_request";
            this.button_new_request.Size = new System.Drawing.Size(28, 27);
            this.button_new_request.TabIndex = 1;
            this.toolTip1.SetToolTip(this.button_new_request, "Управление заявками клиента");
            this.button_new_request.Click += new System.EventHandler(this.button_new_request_Click);
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
            this.listView_request.Size = new System.Drawing.Size(889, 379);
            this.listView_request.TabIndex = 0;
            this.listView_request.UseCompatibleStateImageBehavior = false;
            this.listView_request.View = System.Windows.Forms.View.Details;
            this.listView_request.Layout += new System.Windows.Forms.LayoutEventHandler(this.listView_requset_layount);
            this.listView_request.Validating += new System.ComponentModel.CancelEventHandler(this.listView_requset_invalidate);
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
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.label5);
            this.tabPage5.Controls.Add(this.textBoxTime);
            this.tabPage5.Controls.Add(this.label4);
            this.tabPage5.Controls.Add(this.groupBox3);
            this.tabPage5.Controls.Add(this.groupBox2);
            this.tabPage5.Controls.Add(this.groupBox1);
            this.tabPage5.Location = new System.Drawing.Point(4, 29);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(910, 711);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Выполняемые работы";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(605, 332);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 27);
            this.label5.TabIndex = 5;
            this.label5.Text = "час.";
            // 
            // textBoxTime
            // 
            this.textBoxTime.Location = new System.Drawing.Point(451, 332);
            this.textBoxTime.Name = "textBoxTime";
            this.textBoxTime.Size = new System.Drawing.Size(144, 26);
            this.textBoxTime.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(221, 332);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(230, 27);
            this.label4.TabIndex = 3;
            this.label4.Text = "Предположительное время";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.checkBoxTm22);
            this.groupBox3.Controls.Add(this.checkBoxTm21);
            this.groupBox3.Controls.Add(this.checkBoxTm20);
            this.groupBox3.Controls.Add(this.checkBoxTm19);
            this.groupBox3.Controls.Add(this.checkBoxTm18);
            this.groupBox3.Controls.Add(this.checkBoxTm17);
            this.groupBox3.Controls.Add(this.checkBoxTm16);
            this.groupBox3.Controls.Add(this.checkBoxTm15);
            this.groupBox3.Controls.Add(this.checkBoxTm14);
            this.groupBox3.Controls.Add(this.checkBoxTm13);
            this.groupBox3.Location = new System.Drawing.Point(403, 9);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(375, 314);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Неисправность";
            // 
            // checkBoxTm22
            // 
            this.checkBoxTm22.Location = new System.Drawing.Point(19, 277);
            this.checkBoxTm22.Name = "checkBoxTm22";
            this.checkBoxTm22.Size = new System.Drawing.Size(307, 28);
            this.checkBoxTm22.TabIndex = 9;
            this.checkBoxTm22.Text = "Отопление и кондиционирование";
            this.checkBoxTm22.Click += new System.EventHandler(this.checkBoxTm_Click);
            // 
            // checkBoxTm21
            // 
            this.checkBoxTm21.Location = new System.Drawing.Point(19, 249);
            this.checkBoxTm21.Name = "checkBoxTm21";
            this.checkBoxTm21.Size = new System.Drawing.Size(125, 28);
            this.checkBoxTm21.TabIndex = 8;
            this.checkBoxTm21.Text = "Кузов";
            this.checkBoxTm21.Click += new System.EventHandler(this.checkBoxTm_Click);
            // 
            // checkBoxTm20
            // 
            this.checkBoxTm20.Location = new System.Drawing.Point(19, 222);
            this.checkBoxTm20.Name = "checkBoxTm20";
            this.checkBoxTm20.Size = new System.Drawing.Size(211, 27);
            this.checkBoxTm20.TabIndex = 7;
            this.checkBoxTm20.Text = "Электрооборудование";
            this.checkBoxTm20.Click += new System.EventHandler(this.checkBoxTm_Click);
            // 
            // checkBoxTm19
            // 
            this.checkBoxTm19.Location = new System.Drawing.Point(19, 194);
            this.checkBoxTm19.Name = "checkBoxTm19";
            this.checkBoxTm19.Size = new System.Drawing.Size(192, 28);
            this.checkBoxTm19.TabIndex = 6;
            this.checkBoxTm19.Text = "Система смазки";
            this.checkBoxTm19.Click += new System.EventHandler(this.checkBoxTm_Click);
            // 
            // checkBoxTm18
            // 
            this.checkBoxTm18.Location = new System.Drawing.Point(19, 166);
            this.checkBoxTm18.Name = "checkBoxTm18";
            this.checkBoxTm18.Size = new System.Drawing.Size(202, 28);
            this.checkBoxTm18.TabIndex = 5;
            this.checkBoxTm18.Text = "Система охлаждения";
            this.checkBoxTm18.Click += new System.EventHandler(this.checkBoxTm_Click);
            // 
            // checkBoxTm17
            // 
            this.checkBoxTm17.Location = new System.Drawing.Point(19, 138);
            this.checkBoxTm17.Name = "checkBoxTm17";
            this.checkBoxTm17.Size = new System.Drawing.Size(183, 28);
            this.checkBoxTm17.TabIndex = 4;
            this.checkBoxTm17.Text = "Тормозная система";
            this.checkBoxTm17.Click += new System.EventHandler(this.checkBoxTm_Click);
            // 
            // checkBoxTm16
            // 
            this.checkBoxTm16.Location = new System.Drawing.Point(19, 111);
            this.checkBoxTm16.Name = "checkBoxTm16";
            this.checkBoxTm16.Size = new System.Drawing.Size(135, 27);
            this.checkBoxTm16.TabIndex = 3;
            this.checkBoxTm16.Text = "Рулевое упр.";
            this.checkBoxTm16.Click += new System.EventHandler(this.checkBoxTm_Click);
            // 
            // checkBoxTm15
            // 
            this.checkBoxTm15.Location = new System.Drawing.Point(19, 83);
            this.checkBoxTm15.Name = "checkBoxTm15";
            this.checkBoxTm15.Size = new System.Drawing.Size(144, 28);
            this.checkBoxTm15.TabIndex = 2;
            this.checkBoxTm15.Text = "Ходовая часть";
            this.checkBoxTm15.Click += new System.EventHandler(this.checkBoxTm_Click);
            // 
            // checkBoxTm14
            // 
            this.checkBoxTm14.Location = new System.Drawing.Point(19, 55);
            this.checkBoxTm14.Name = "checkBoxTm14";
            this.checkBoxTm14.Size = new System.Drawing.Size(77, 28);
            this.checkBoxTm14.TabIndex = 1;
            this.checkBoxTm14.Text = "ЭСУД";
            this.checkBoxTm14.Click += new System.EventHandler(this.checkBoxTm_Click);
            // 
            // checkBoxTm13
            // 
            this.checkBoxTm13.Location = new System.Drawing.Point(19, 28);
            this.checkBoxTm13.Name = "checkBoxTm13";
            this.checkBoxTm13.Size = new System.Drawing.Size(67, 27);
            this.checkBoxTm13.TabIndex = 0;
            this.checkBoxTm13.Text = "ДВС";
            this.checkBoxTm13.Click += new System.EventHandler(this.checkBoxTm_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBoxTm12);
            this.groupBox2.Controls.Add(this.checkBoxTm11);
            this.groupBox2.Controls.Add(this.checkBoxTm10);
            this.groupBox2.Controls.Add(this.checkBoxTm9);
            this.groupBox2.Controls.Add(this.checkBoxTm8);
            this.groupBox2.Location = new System.Drawing.Point(182, 9);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(212, 314);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Диагностика";
            // 
            // checkBoxTm12
            // 
            this.checkBoxTm12.Location = new System.Drawing.Point(19, 138);
            this.checkBoxTm12.Name = "checkBoxTm12";
            this.checkBoxTm12.Size = new System.Drawing.Size(183, 28);
            this.checkBoxTm12.TabIndex = 4;
            this.checkBoxTm12.Text = "Тормозная система";
            this.checkBoxTm12.Click += new System.EventHandler(this.checkBoxTm_Click);
            // 
            // checkBoxTm11
            // 
            this.checkBoxTm11.Location = new System.Drawing.Point(19, 111);
            this.checkBoxTm11.Name = "checkBoxTm11";
            this.checkBoxTm11.Size = new System.Drawing.Size(135, 27);
            this.checkBoxTm11.TabIndex = 3;
            this.checkBoxTm11.Text = "Рулевое упр.";
            this.checkBoxTm11.Click += new System.EventHandler(this.checkBoxTm_Click);
            // 
            // checkBoxTm10
            // 
            this.checkBoxTm10.Location = new System.Drawing.Point(19, 83);
            this.checkBoxTm10.Name = "checkBoxTm10";
            this.checkBoxTm10.Size = new System.Drawing.Size(144, 28);
            this.checkBoxTm10.TabIndex = 2;
            this.checkBoxTm10.Text = "Ходовая часть";
            this.checkBoxTm10.Click += new System.EventHandler(this.checkBoxTm_Click);
            // 
            // checkBoxTm9
            // 
            this.checkBoxTm9.Location = new System.Drawing.Point(19, 55);
            this.checkBoxTm9.Name = "checkBoxTm9";
            this.checkBoxTm9.Size = new System.Drawing.Size(125, 28);
            this.checkBoxTm9.TabIndex = 1;
            this.checkBoxTm9.Text = "ЭСУД";
            this.checkBoxTm9.Click += new System.EventHandler(this.checkBoxTm_Click);
            // 
            // checkBoxTm8
            // 
            this.checkBoxTm8.Location = new System.Drawing.Point(19, 28);
            this.checkBoxTm8.Name = "checkBoxTm8";
            this.checkBoxTm8.Size = new System.Drawing.Size(125, 27);
            this.checkBoxTm8.TabIndex = 0;
            this.checkBoxTm8.Text = "ДВС";
            this.checkBoxTm8.Click += new System.EventHandler(this.checkBoxTm_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxTm1);
            this.groupBox1.Controls.Add(this.checkBoxTm2);
            this.groupBox1.Controls.Add(this.checkBoxTm3);
            this.groupBox1.Controls.Add(this.checkBoxTm4);
            this.groupBox1.Controls.Add(this.checkBoxTm5);
            this.groupBox1.Controls.Add(this.checkBoxTm6);
            this.groupBox1.Controls.Add(this.checkBoxTm7);
            this.groupBox1.Location = new System.Drawing.Point(10, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(163, 314);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "TO";
            // 
            // checkBoxTm1
            // 
            this.checkBoxTm1.Location = new System.Drawing.Point(19, 28);
            this.checkBoxTm1.Name = "checkBoxTm1";
            this.checkBoxTm1.Size = new System.Drawing.Size(67, 27);
            this.checkBoxTm1.TabIndex = 1;
            this.checkBoxTm1.Text = "TO-1";
            this.checkBoxTm1.Click += new System.EventHandler(this.checkBoxTm_Click);
            // 
            // checkBoxTm2
            // 
            this.checkBoxTm2.Location = new System.Drawing.Point(19, 55);
            this.checkBoxTm2.Name = "checkBoxTm2";
            this.checkBoxTm2.Size = new System.Drawing.Size(67, 28);
            this.checkBoxTm2.TabIndex = 1;
            this.checkBoxTm2.Text = "ТО-2";
            this.checkBoxTm2.Click += new System.EventHandler(this.checkBoxTm_Click);
            // 
            // checkBoxTm3
            // 
            this.checkBoxTm3.Location = new System.Drawing.Point(19, 83);
            this.checkBoxTm3.Name = "checkBoxTm3";
            this.checkBoxTm3.Size = new System.Drawing.Size(67, 28);
            this.checkBoxTm3.TabIndex = 1;
            this.checkBoxTm3.Text = "ТО-3";
            this.checkBoxTm3.Click += new System.EventHandler(this.checkBoxTm_Click);
            // 
            // checkBoxTm4
            // 
            this.checkBoxTm4.Location = new System.Drawing.Point(19, 111);
            this.checkBoxTm4.Name = "checkBoxTm4";
            this.checkBoxTm4.Size = new System.Drawing.Size(67, 27);
            this.checkBoxTm4.TabIndex = 1;
            this.checkBoxTm4.Text = "ТО-4";
            this.checkBoxTm4.Click += new System.EventHandler(this.checkBoxTm_Click);
            // 
            // checkBoxTm5
            // 
            this.checkBoxTm5.Location = new System.Drawing.Point(19, 138);
            this.checkBoxTm5.Name = "checkBoxTm5";
            this.checkBoxTm5.Size = new System.Drawing.Size(67, 28);
            this.checkBoxTm5.TabIndex = 1;
            this.checkBoxTm5.Text = "ТО-5";
            this.checkBoxTm5.Click += new System.EventHandler(this.checkBoxTm_Click);
            // 
            // checkBoxTm6
            // 
            this.checkBoxTm6.Location = new System.Drawing.Point(19, 166);
            this.checkBoxTm6.Name = "checkBoxTm6";
            this.checkBoxTm6.Size = new System.Drawing.Size(67, 28);
            this.checkBoxTm6.TabIndex = 1;
            this.checkBoxTm6.Text = "ТО-6";
            this.checkBoxTm6.Click += new System.EventHandler(this.checkBoxTm_Click);
            // 
            // checkBoxTm7
            // 
            this.checkBoxTm7.Location = new System.Drawing.Point(19, 194);
            this.checkBoxTm7.Name = "checkBoxTm7";
            this.checkBoxTm7.Size = new System.Drawing.Size(135, 28);
            this.checkBoxTm7.TabIndex = 1;
            this.checkBoxTm7.Text = "Сход/Развал";
            this.checkBoxTm7.Click += new System.EventHandler(this.checkBoxTm_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.listView2);
            this.tabPage3.Location = new System.Drawing.Point(4, 29);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(910, 711);
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
            this.listView2.Size = new System.Drawing.Size(889, 665);
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
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.Location = new System.Drawing.Point(371, 762);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(96, 26);
            this.buttonSave.TabIndex = 1;
            this.buttonSave.Text = "Запомнить";
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
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
            // button_close
            // 
            this.button_close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_close.Location = new System.Drawing.Point(822, 762);
            this.button_close.Name = "button_close";
            this.button_close.Size = new System.Drawing.Size(106, 26);
            this.button_close.TabIndex = 2;
            this.button_close.Text = "Закрыть";
            this.button_close.Click += new System.EventHandler(this.button_close_Click);
            // 
            // button_save_close
            // 
            this.button_save_close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_save_close.Location = new System.Drawing.Point(476, 762);
            this.button_save_close.Name = "button_save_close";
            this.button_save_close.Size = new System.Drawing.Size(164, 26);
            this.button_save_close.TabIndex = 3;
            this.button_save_close.Text = "Запомнить и закрыть";
            this.button_save_close.Click += new System.EventHandler(this.button_save_close_Click);
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
            this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 1;
            this.menuItem4.Text = "Запчасти предоставленны клиентом";
            this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
            // 
            // button_print_invite
            // 
            this.button_print_invite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_print_invite.Location = new System.Drawing.Point(10, 762);
            this.button_print_invite.Name = "button_print_invite";
            this.button_print_invite.Size = new System.Drawing.Size(163, 26);
            this.button_print_invite.TabIndex = 4;
            this.button_print_invite.Text = "Печать приглашения";
            this.button_print_invite.Click += new System.EventHandler(this.button_print_invite_Click);
            // 
            // tabPage9
            // 
            this.tabPage9.Controls.Add(this.label_auto);
            this.tabPage9.Location = new System.Drawing.Point(4, 29);
            this.tabPage9.Name = "tabPage9";
            this.tabPage9.Size = new System.Drawing.Size(910, 711);
            this.tabPage9.TabIndex = 8;
            this.tabPage9.Text = "Основные данные";
            this.tabPage9.UseVisualStyleBackColor = true;
            // 
            // label_auto
            // 
            this.label_auto.AutoSize = true;
            this.label_auto.Location = new System.Drawing.Point(36, 31);
            this.label_auto.Name = "label_auto";
            this.label_auto.Size = new System.Drawing.Size(205, 20);
            this.label_auto.TabIndex = 0;
            this.label_auto.Text = "Автомобиль не выбран";
            // 
            // FormCard
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.ClientSize = new System.Drawing.Size(937, 795);
            this.Controls.Add(this.button_print_invite);
            this.Controls.Add(this.button_save_close);
            this.Controls.Add(this.button_close);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.tabControl1);
            this.Name = "FormCard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormCard";
            this.Closed += new System.EventHandler(this.formCard_Closed);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage6.ResumeLayout(false);
            this.tabPage8.ResumeLayout(false);
            this.tabPage7.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage9.ResumeLayout(false);
            this.tabPage9.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		private void buttonSelect_Click(object sender, System.EventArgs e)
		{
			// Выбор марки автомобиля для данного заказ наряда
			FormAutoTypeList dialog = new FormAutoTypeList();
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			Card.AutoType = dialog.SelectedAutoType;
			if(Db.ShowFaults())return;
			textBoxAutoType.Text = card.AutoTypeTxt;

			// Выбор времен для данной марки  автомобиля осуществляется автоматом
			// При установке типа автомобиля
			textBoxTime.Text = card.TimeTxt;
		}

		

		#region Основные действия
		private bool Save()
		{
			card.Comment			= textBoxComment.Text;
			card.Cashless			= checkBoxCashless.Checked;
			card.InnerWarranty		= checkBox_inner_warranty.Checked;
			card.RepresentDocument	= textBoxRepresentDocument.Text;
			the_card_change.SetData("ВОЗВРАТ", checkBox_return.Checked);
			the_card_change.SetData("КРЕДИТНАЯ_КАРТА_КАРТОЧКА", checkBox_creditcard.Checked);

			// Дополнительные проверки на правильность заполнения заголовка
			if(card.CodeWorkshop == 0) Db.SetErrorMessage("НЕ ВЫБРАНО ПОДРАЗДЕЛЕНИЕ");

			if(Db.ShowFaults()) return false;
			// Запоминаем изменения или заводим новую карточку!
			if(card.Write() != true)
			{
				Db.ShowFaults();
				return false;
			}

			// При необходимости, записывает данные свидетельства о регистрации
			if(the_card.CodeLicenseVihicle != the_card_change.CodeLicenseVihicle)
			{
				DbSqlCard.SetLicenceVehicle(card, the_card_change.CodeLicenseVihicle);
			}
			// При необходимости записываем признак возвартности карточки
			if(the_card.Returned != the_card_change.Returned)
			{
				DbSqlCard.SetReturnFlag(card, the_card_change.Returned);
			}
			// При необходимости записываем признак безналичного расчета
			if(((bool)the_card.GetData("КРЕДИТНАЯ_КАРТА_КАРТОЧКА")) != ((bool)the_card_change.GetData("КРЕДИТНАЯ_КАРТА_КАРТОЧКА")))
			{
				DbSqlCard.SetCreditcardFlag(card, (bool)the_card_change.GetData("КРЕДИТНАЯ_КАРТА_КАРТОЧКА"));
			}

			// Запись трудоемкостей
			DbCardWork.WriteList(WorkList, card);
			// Новая схема!!!!!
			//DtCardDatabaseConnector dbConnector = new DtCardDatabaseConnector(the_card);
			//dbConnector.SaveWorks();

			// Запись деталей
			DbCardDetail.WriteList(listView3, card);
			// Запись рекомендаций
			DbCardRecomend.WriteList(listView4, card);
			if(Db.ShowFaults() == true) return false;

			ShowChanges();
			return true;
		}
		private bool LoadData()
		{
			// Очистка предыдущего
			listView3.Items.Clear();
			listView2.Items.Clear();
			WorkList.Items.Clear();
			listView4.Items.Clear();
			// Конец очистки


			card = DbCard.Find(card.Number, card.Year);
			this.Text = "Карточка заказа № " + card.NumberTxt;
			if(card.WarrantNumberTxt.Length != 0)
			{
				this.Text += " , Заказ-наряд № " + card.WarrantNumberTxt;
			}

			// Пробуем загрузить список работ карточки
			DbCardWork.FillList(WorkList, card);
			SetListIndexes(WorkList);
			MakeSumWork();
			// Новый способ!!!!!
			//DtCardDisplay displey = new DtCardDisplay(the_card);
			//displey.DisplayCardWorks(WorkList);

			// Пробуем загрузить список деталей карточки
			DbCardDetail.FillList(listView3, card);
			SetListIndexes(listView3);
			//Выставляем сумму полученных деталей
			textBoxDetailSumm.Text	= Db.CachToTxt(SetDetailSumm());

			// Пробуем загрузить список рекомендаций
			DbCardRecomend.FillList(listView4, card);

			// Догружаем представителя
			if(card.CodeRepresent != 0)
				card.Represent	= DbPartner.Find(card.CodeRepresent);

			// Догружаем цех
			if(card.CodeWorkshop != 0)
				card.Workshop	= DbWorkshop.Find(card.CodeWorkshop);

			// Догружаем полный вид гарантии
			if(card.CodeGuarantyType != 0)
				card.GuarantyTypeFull	= DbGuarantyType.Find(card.CodeGuarantyType);

			if(card.CodeRepresent == 0)
			{
				textBoxRepresentDocument.Enabled	= false;	// Запрет изменения
				comboBox1.Visible					= false;	//		документа представителя
			}

			// Настройка разрешений для изменений
			if(!card.CanUpdateWarrantTitle)
			{
				buttonSelect.Visible				= false;	// Запрет изменения
				buttonSelectAuto.Visible			= false;	//		параметров
				buttonSelectClient.Visible			= false;	//			шапки
				buttonSelectRepresent.Visible		= false;	//
				textBoxRepresentDocument.Enabled	= false;	// Запрет изменения
				comboBox1.Visible					= false;	//		документа представителя

				textBoxPartner.Enabled				= false;
				buttonSelectPartner.Visible			= false;
			}
			if(!card.CanUpdateWarrant)
			{
				buttonSave.Visible			= false;	// Запрет записи карточки!
				buttonNewWork.Enabled		= false;	// Запрет 
				buttonGarantyOn.Enabled		= false;	//	нажатия
				buttonGuarantyOf.Enabled	= false;	//		кнопок
				buttonDone.Enabled			= false;
				buttonUnDone.Enabled		= false;
				buttonNewDetail.Enabled		= false;
				buttonNewStorage.Enabled	= false;
				buttonDelDetail.Enabled		= false;
				buttonReserve.Enabled		= false;
				buttonUnreserve.Enabled		= false;
				buttonGuarantyOn.Enabled	= false;
				buttonGuarantyOff.Enabled	= false;
				buttonSetPrice.Enabled		= false;
				buttonSetOuter.Enabled		= false;
				buttonSetCheck.Enabled		= false;
				buttonSetNoOuter.Enabled	= false;
				buttonSetNoCheck.Enabled	= false;
				textBoxComment.Enabled		= false;	// Запрет измененея коментария
				checkBoxCashless.Enabled	= false;	// Запрет изменения типа расчета
				textBoxRepresentDocument.Enabled	= false;	// Запрет изменения
				comboBox1.Visible					= false;	//		документа представителя
				checkBox_inner_warranty.Enabled		= false;	// Запрет изменения типа заказ-наряда

				buttonNewRecomend.Enabled		= false;
				buttonPropertyRecomend.Enabled	= false;
				checkBox_return.Enabled			= false;
			}

			// Если выбран владелец (контрагент) догружаем для него свойтсва
			if(card.CodePartner != 0) 
				partner_property = DbSqlPartnerProperty.Find(card.CodePartner);
			if(partner_property != null)
			{
				if(partner_property.Cashless == true)
					textBox_cashless.Text = "Безналичный";
				else
					textBox_cashless.Text = "Наличный";
			}
			else
			{
				textBox_cashless.Text = "Наличный";
			}

			// Установка Владельца
			RestorePartner();
			textBoxAutoType.Text		= card.AutoTypeTxt;
			textBoxAuto.Text			= card.AutoTxt;
			textBoxClient.Text			= card.PartnerNameTxt;
			textBoxRepresent.Text		= card.RepresentNameTxt;
			textBoxRepresentDocument.Text		= card.RepresentDocument;
			textBoxComment.Text			= card.Comment;
			checkBoxCashless.Checked	= card.Cashless;
			checkBox_inner_warranty.Checked	= card.InnerWarranty;
			textBoxGuarantyTypeTxt.Text	= card.GuarantyTypeTxt;
			textBoxWorkshop.Text		= card.WorkshopTxt;
			textBox_discount.Text		= card.DiscountWorkTxt;

			// Установка выполняемых работ
			if((card.Works & (1)) > 0) checkBoxTm1.Checked = true;
			if((card.Works & (1 << 1)) > 0) checkBoxTm2.Checked = true;
			if((card.Works & (1 << 2)) > 0) checkBoxTm3.Checked = true;
			if((card.Works & (1 << 3)) > 0) checkBoxTm4.Checked = true;
			if((card.Works & (1 << 4)) > 0) checkBoxTm5.Checked = true;
			if((card.Works & (1 << 5)) > 0) checkBoxTm6.Checked = true;
			if((card.Works & (1 << 6)) > 0) checkBoxTm7.Checked = true;
			if((card.Works & (1 << 7)) > 0) checkBoxTm8.Checked = true;
			if((card.Works & (1 << 8)) > 0) checkBoxTm9.Checked = true;
			if((card.Works & (1 << 9)) > 0) checkBoxTm10.Checked = true;
			if((card.Works & (1 << 10)) > 0) checkBoxTm11.Checked = true;
			if((card.Works & (1 << 11)) > 0) checkBoxTm12.Checked = true;
			if((card.Works & (1 << 12)) > 0) checkBoxTm13.Checked = true;
			if((card.Works & (1 << 13)) > 0) checkBoxTm14.Checked = true;
			if((card.Works & (1 << 14)) > 0) checkBoxTm15.Checked = true;
			if((card.Works & (1 << 15)) > 0) checkBoxTm16.Checked = true;
			if((card.Works & (1 << 16)) > 0) checkBoxTm17.Checked = true;
			if((card.Works & (1 << 17)) > 0) checkBoxTm18.Checked = true;
			if((card.Works & (1 << 18)) > 0) checkBoxTm19.Checked = true;
			if((card.Works & (1 << 19)) > 0) checkBoxTm20.Checked = true;
			if((card.Works & (1 << 20)) > 0) checkBoxTm21.Checked = true;
			if((card.Works & (1 << 21)) > 0) checkBoxTm22.Checked = true;
			textBoxTime.Text = card.TimeTxt;

			return true;
		}
		#endregion
		#region Главные кнопки
		private void buttonSave_Click(object sender, System.EventArgs e)
		{
			if(Save() != true) return;
			LoadData();

			// НОВЫЙ ТИП ДАННЫХ
			the_card = DbSqlCard.Find(card.Number, card.Year);
			// КОНЕЦ НОВЫЙ ТИП ДАННЫХ

			// Пока все остается
			//this.DialogResult = DialogResult.OK;
			//this.Close();
		}
		private void button_close_Click(object sender, System.EventArgs e)
		{
			// Закрыть окно без сохранения сделанных изменений
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		private void button_save_close_Click(object sender, System.EventArgs e)
		{
			// Закрыть окно после сохранения сделанных изменений
			if(Save() != true) return;
			this.DialogResult = DialogResult.OK;
			this.Close();
		}
	
		#endregion

		#region Действия деталей
		protected void listView3_KeyDown(object sender, KeyEventArgs e)
		{
			if(!card.CanUpdateWarrant) return;	// Нельзя менять заказ-наряд

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
						SetListIndexes(listView3);
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
						dtl.Quontity = dtl.Quontity + 1;
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
						dtl.Quontity = dtl.Quontity - 1;
						if(dtl.Quontity == 0) dtl.Quontity = 1;
						dtl.SetLVItem(item);
					}
				}
				//Выставляем сумму полученных работ
				textBoxDetailSumm.Text	= Db.CachToTxt(SetDetailSumm());
			}
		}
		#endregion

		public DbCard Card
		{
			get
			{
				return card;
			}
		}

		protected void listView1_SizeChanged(object sender, EventArgs e)
		{
			listView1.Columns[1].Width = listView1.Width - 82 - listView1.Columns[0].Width
				- listView1.Columns[2].Width
				- listView1.Columns[3].Width
				- listView1.Columns[4].Width
				- listView1.Columns[5].Width;
		}

		

		protected void box_MouseDown(object sender, MouseEventArgs e)
		{
			this.Controls.Remove(textBoxTmp);
			textBoxTmp.Dispose();
		}

		

		protected void SetListIndexes(ListView list)
		{
			foreach(ListViewItem item in list.Items)
			{
				item.Text = (item.Index + 1).ToString();
			}
		}

	

		private void buttonSelectClient_Click(object sender, System.EventArgs e)
		{
			// Выбор клиента по данной карточке
			FormPartnerList dialog = new FormPartnerList();
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			card.Partner = dialog.Partner;
			textBoxClient.Text = card.PartnerNameTxt;

			// Работа со свойствами контрагентов
			if(card.CodePartner != 0)
			{
				partner_property = DbSqlPartnerProperty.Find(card.CodePartner);
				if(partner_property != null)
				{
					checkBoxCashless.Checked	= partner_property.Cashless;
				}
				else
				{
					checkBoxCashless.Checked	= false;
				}
			}

			// Высвечиваем примечание!
			if(card.Partner.Comment.Length != 0)
				MessageBox.Show(card.Partner.Comment, "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			// Тут же предлагаем выбрать автомобиль данного клиента
			FormAutoList dialogAuto = new FormAutoList(Db.ClickType.Select, dialog.Partner);
			dialogAuto.ShowDialog(this);
			if(dialogAuto.DialogResult != DialogResult.OK) return;

			SetAuto(dialogAuto.Auto);
			return;

			// Установка автомобиля
			card.Auto = dialogAuto.Auto;
			textBoxAuto.Text = card.AutoTxt;
			textBoxAutoType.Text = card.AutoTypeTxt;

			// И опять контроль свойств автомобиля
			// Проверяем наличие номера для запчастей
			if(card.Auto.NoSparePartNumber == false && card.Auto.SparePartNumber == 0)
			{
				MessageBox.Show("Установите пожалуйста НОМЕР ДЛЯ ЗАПЧАСТЕЙ автомобиля");
				FormAuto dialog1 = new FormAuto(card.Auto);
				dialog1.ShowDialog();
				if(dialog1.DialogResult != DialogResult.OK) return;
				card.Auto = dialog1.Auto;
			}
			// Будет проверка на предписания
			card.Auto.DirectionReport();
		}

		private void buttonSelectAuto_Click(object sender, System.EventArgs e)
		{
			// Выбор автомобиля клиента по карточке
			FormAutoList dialog = new FormAutoList(Db.ClickType.Select, null);
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;

			SetAuto(dialog.Auto);
			return;

			// Установка автомобиля
			card.Auto = dialog.Auto;
			textBoxAuto.Text = card.AutoTxt;
			textBoxAutoType.Text = card.AutoTypeTxt;

			// Проверяем наличие номера для запчастей
			if(card.Auto.NoSparePartNumber == false && card.Auto.SparePartNumber == 0)
			{
				MessageBox.Show("Установите пожалуйста НОМЕР ДЛЯ ЗАПЧАСТЕЙ автомобиля");
				FormAuto dialog1 = new FormAuto(card.Auto);
				dialog1.ShowDialog();
				if(dialog1.DialogResult != DialogResult.OK) return;
				card.Auto = dialog1.Auto;
			}
			// Будет проверка на предписания
			card.Auto.DirectionReport();
		}

		private void SetAuto(DbAuto auto)
		{
			// Выдача справочных данных, для сервиса
			DtAuto the_auto = DbSqlAuto.Find(auto.Code);
			if(the_auto != null)
			{
				// Проверка гос-номера
				string plate_number = (string)the_auto.GetData("НОМЕР_ЗНАК_НОМЕР");
				string plate_region = (string)the_auto.GetData("НОМЕР_ЗНАК_РЕГИОН");
				if(plate_number == "" || plate_region == "")
				{
					UIF_LicencePlate dialog = new UIF_LicencePlate(auto.Code, auto.SignNo);
					dialog.ShowDialog();
				}
			}
			if(card.CodeWorkshop == 1)
			{
				ServiceHistory(auto.Code, 1);
				AlarmHistory(auto.Code);
				// Проверка наличия ОБЯЗАТЕЛЬНЫХ примечаний
				if(DbSqlAutoComment.IsComments(auto.Code) == true)
				{
					UserInterface.ListAutoComment(0, (object)auto.Code, 0, UserInterface.ClickType.Modify);
				}
			}

			// Дополнительные проверки на разрешения
			if(CheckAutoPermision(auto.Code) == false)
			{
				MessageBox.Show("У Вас нет прав для выбора этого автомобиля");
				return;
			}
			card.Auto = auto;
			textBoxAuto.Text = card.AutoTxt;
			textBoxAutoType.Text = card.AutoTypeTxt;

			the_card.DtAuto = the_auto; // Проверка фикскации изменений

			// Проверяем наличие номера для запчастей
			if (card.Auto.NoSparePartNumber == false && card.Auto.SparePartNumber == 0)
			{
				MessageBox.Show("Установите пожалуйста НОМЕР ДЛЯ ЗАПЧАСТЕЙ автомобиля");
				FormAuto dialog1 = new FormAuto(card.Auto);
				dialog1.ShowDialog();
				if(dialog1.DialogResult != DialogResult.OK) return;
				card.Auto = dialog1.Auto;
			}
			// Будет проверка на предписания
			card.Auto.DirectionReport();

			
		}

		private void buttonNewDetail_Click(object sender, System.EventArgs e)
		{
			// Подбор таким образом отключается
			return;
			// Добавление детали - через выбор детали для замены
			FormDetailList dialog = new FormDetailList();
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			// Запрос складской позиции
			FormDetailStorageList dialogStorage = new FormDetailStorageList(null, 0, dialog.SelectedDetail, null);
			dialogStorage.ShowDialog(this);
			DbCardDetail	element;
			if(dialogStorage.DialogResult != DialogResult.OK)
			{
				element = new DbCardDetail(card, dialog.SelectedDetail);
			}
			else
			{
				element = new DbCardDetail(card, dialog.SelectedDetail, dialogStorage.DetailStorage);
			}
			listView3.Items.Add(element.LVItem);
			SetListIndexes(listView3);
			//Выставляем сумму полученных работ
			textBoxDetailSumm.Text	= Db.CachToTxt(SetDetailSumm());
		}

		private void buttonNewStorage_Click(object sender, System.EventArgs e)
		{
			// Добавление новой детали в лист деталей, через склад
			
			FormDetailStorageList dialog = new FormDetailStorageList(listView3, 4, null, card);
			dialog.ShowDialog(this);
			//if(dialog.DialogResult != DialogResult.OK) return;
			//DbCardDetail element = new DbCardDetail(card, dialog.DetailStorage);
			//listView3.Items.Add(element.LVItem);
			SetListIndexes(listView3);
			//Выставляем сумму полученных работ
			textBoxDetailSumm.Text	= Db.CachToTxt(SetDetailSumm());
			
		}

		private void buttonDelDetail_Click(object sender, System.EventArgs e)
		{
			// Удаляем из списка выбранную деталь
			ListViewItem item = Db.GetItemSelected(listView3);
			if(item == null) return;
			DbCardDetail element = (DbCardDetail)item.Tag;
			if(element == null) return;

			if(element.Exists)
			{
				// Изменяем флаг удаления
				element.Delete = !element.Delete;
				element.SetLVItem(item);
			}
			else
			{
				listView3.Items.Remove(item);
				SetListIndexes(listView3);
			}
			//Выставляем сумму полученных работ
			textBoxDetailSumm.Text	= Db.CachToTxt(SetDetailSumm());
		}

		private void buttonReserve_Click(object sender, System.EventArgs e)
		{
			// Резервирование детали на складе под заданную позицию
			foreach(ListViewItem item in listView3.SelectedItems)
			{
				DbCardDetail detail = (DbCardDetail)item.Tag;
				if(detail != null)
				{
					DbReserve reserve = new DbReserve(detail);
					reserve.Add();
					if(reserve.Write())
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
			foreach(ListViewItem item in listView3.SelectedItems)
			{
				DbCardDetail detail = (DbCardDetail)item.Tag;
				if(detail != null)
				{
					DbReserve reserve = new DbReserve(detail);
					reserve.Delete();
					if(reserve.Write())
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
			foreach(ListViewItem item in listView3.SelectedItems)
			{
				DbCardDetail detail = (DbCardDetail)item.Tag;
				if(detail != null)
				{
					detail.Guaranty = true;
					// Пересчет цены
					if(detail.Connect1C() == true)
					{
						detail.Price = detail.Price + detail.Price / 100 * card.GuarantyTypeVal;
					}
					detail.SetLVItem(item);
				}
			}
			//Выставляем сумму полученных работ
			textBoxDetailSumm.Text	= Db.CachToTxt(SetDetailSumm());
		}

		private void buttonGuarantyOff_Click(object sender, System.EventArgs e)
		{
			// Снимаем гарантию с элементов
			foreach(ListViewItem item in listView3.SelectedItems)
			{
				DbCardDetail detail = (DbCardDetail)item.Tag;
				if(detail != null)
				{
					detail.Guaranty = false;
					detail.SetLVItem(item);
				}
			}
			//Выставляем сумму полученных работ
			textBoxDetailSumm.Text	= Db.CachToTxt(SetDetailSumm());
		}

		private void buttonSetPrice_Click(object sender, System.EventArgs e)
		{
			// Установка новой цены детали
			ListViewItem item = Db.GetItemSelected(listView3);
			if(item == null) return;
			DbCardDetail element = (DbCardDetail)item.Tag;
			if(element == null) return;

			// Если позиция связанна с 1С - новую цену установить нельзя!
			if(element.Connect1C() == true) return;

			FormSelectString dialog = new FormSelectString("Новая цена (с НДС)", "0.0");
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			element.Price	= dialog.SelectedFloat;
			element.SetLVItem(item);

			//Выставляем сумму полученных работ
			textBoxDetailSumm.Text	= Db.CachToTxt(SetDetailSumm());
		}

		private void buttonSetOuter_Click(object sender, System.EventArgs e)
		{
			// Пометить деталь как внешнюю
			foreach(ListViewItem item in listView3.SelectedItems)
			{
				DbCardDetail detail = (DbCardDetail)item.Tag;
				if(detail != null)
				{
					detail.Outer = true;
					detail.SetLVItem(item);
				}
			}
			//Выставляем сумму полученных работ
			textBoxDetailSumm.Text	= Db.CachToTxt(SetDetailSumm());
		}

		private void buttonSetCheck_Click(object sender, System.EventArgs e)
		{
			// Пометить детали как по копии чека
			foreach(ListViewItem item in listView3.SelectedItems)
			{
				DbCardDetail detail = (DbCardDetail)item.Tag;
				if(detail != null)
				{
					detail.Check = true;
					detail.SetLVItem(item);
				}
			}
		}

		private void buttonSetNoOuter_Click(object sender, System.EventArgs e)
		{
			// Отменить пометку детали как внешней
			foreach(ListViewItem item in listView3.SelectedItems)
			{
				DbCardDetail detail = (DbCardDetail)item.Tag;
				if(detail != null)
				{
					detail.Outer = false;
					detail.SetLVItem(item);
				}
			}
			//Выставляем сумму полученных работ
			textBoxDetailSumm.Text	= Db.CachToTxt(SetDetailSumm());
		}

		private void buttonSetNoCheck_Click(object sender, System.EventArgs e)
		{
			// Отменить пометку детали как по копии чека
			foreach(ListViewItem item in listView3.SelectedItems)
			{
				DbCardDetail detail = (DbCardDetail)item.Tag;
				if(detail != null)
				{
					detail.Check = false;
					detail.SetLVItem(item);
				}
			}
		}

		

		private void checkBoxTm_Click(object sender, System.EventArgs e)
		{
			int		works = 0;
			if(checkBoxTm1.Checked) works	+=  1;
			if(checkBoxTm2.Checked) works	+=  1 << 1;
			if(checkBoxTm3.Checked) works	+=  1 << 2;
			if(checkBoxTm4.Checked) works	+=  1 << 3;
			if(checkBoxTm5.Checked) works	+=  1 << 4;
			if(checkBoxTm6.Checked) works	+=  1 << 5;
			if(checkBoxTm7.Checked) works	+=  1 << 6;
			if(checkBoxTm8.Checked) works	+=  1 << 7;
			if(checkBoxTm9.Checked) works	+=  1 << 8;
			if(checkBoxTm10.Checked) works	+=  1 << 9;
			if(checkBoxTm11.Checked) works	+=  1 << 10;
			if(checkBoxTm12.Checked) works	+=  1 << 11;
			if(checkBoxTm13.Checked) works	+=  1 << 12;
			if(checkBoxTm14.Checked) works	+=  1 << 13;
			if(checkBoxTm15.Checked) works	+=  1 << 14;
			if(checkBoxTm16.Checked) works	+=  1 << 15;
			if(checkBoxTm17.Checked) works	+=  1 << 16;
			if(checkBoxTm18.Checked) works	+=  1 << 17;
			if(checkBoxTm19.Checked) works	+=  1 << 18;
			if(checkBoxTm20.Checked) works	+=  1 << 19;
			if(checkBoxTm21.Checked) works	+=  1 << 20;
			if(checkBoxTm22.Checked) works	+=  1 << 21;
			card.Works = works;
			textBoxTime.Text = card.TimeTxt;
		}

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

		protected void listView3_DoubleClick(object sender, EventArgs e)
		{
			// Определяем столбец, на ктором щелкнули мышкой
			// Необходимо определить элемент и колонку, на которой кликнули
			int column = Db.GetColumnPosition(listView3);
			ListViewItem item = Db.GetItemPosition(listView3);
			if(item == null) return;
			DbCardDetail cardDetail = (DbCardDetail)item.Tag;
			if(cardDetail == null) return;

			if(column != 3) return;

			// Запуск нового окна ввода текста
			FormSelectionType2 dialog = Db.MakeFormSelectionType2(this, item, column, "1.00");
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			cardDetail.Quontity		= dialog.SelectedFloat;
			cardDetail.SetLVItem(item);
		}

		private void buttonSelectGuarantyType_Click(object sender, System.EventArgs e)
		{
			// СЕРВИС КОНСУЛЬТАНТ МОЖЕТ ВЫБРАТЬ ТОЛЬКО ОДИН ВИД ГАРАНТИИ
			DtGuarantyType guaranty = DbSqlGuarantyType.FindDefault();
			if(guaranty == null) return;
			DbGuarantyType guaranty_type = DbGuarantyType.Find((long)guaranty.GetData("КОД_ГАРАНТИЯ"));
			if(guaranty_type == null) return;
			card.GuarantyType = guaranty_type;
			textBoxGuarantyTypeTxt.Text	= card.GuarantyTypeTxt;
			return;

			// Инициируем процедуру выбора типа гарантии
			FormGuarantyTypeList dialog = new FormGuarantyTypeList(Db.ClickType.Select);
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			card.GuarantyType			= dialog.GuarantyType;
			textBoxGuarantyTypeTxt.Text	= card.GuarantyTypeTxt;
		}

		

		public void UpdateCardData()
		{
			SetListIndexes(WorkList);
			MakeSumWork();

			SetListIndexes(listView3);
			textBoxDetailSumm.Text	= Db.CachToTxt(SetDetailSumm());
		}

		private void formCard_Closed(object sender, EventArgs e)
		{
			if(workSelect != null)
			{
				workSelect.Close();
				workSelect	= null;
			}
		}

		

		private void buttonSelectRepresent_Click(object sender, System.EventArgs e)
		{
			// Выбор нового представителя
			FormPartnerList dialog = new FormPartnerList();
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			card.Represent = dialog.Partner;
			textBoxRepresent.Text = card.RepresentNameTxt;

			// Включение окна выбора документа представителя
			textBoxRepresentDocument.Enabled = true;
			comboBox1.Visible = true;
		}

		private void comboBox1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			// Очищаем поле документа представителя
			if(comboBox1.SelectedIndex < 0) return;
			string text = comboBox1.Items[comboBox1.SelectedIndex].ToString();
			textBoxRepresentDocument.Text = text;
		}

		private void buttonSelectWorkshop_Click(object sender, System.EventArgs e)
		{
			// Вызываем диалог выбора подразделения
			FormWorkshopList dialog = new FormWorkshopList();
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			card.Workshop = dialog.SelectedWorkshop;
			this.textBoxWorkshop.Text = card.WorkshopTxt;
		}

		protected float SetDetailSumm()
		{
			float summ = 0.0f;
			foreach(ListViewItem itm in listView3.Items)
			{
				if(itm != null)
				{
					DbCardDetail element = (DbCardDetail)itm.Tag;
					if(element != null)
					{
						summ += element.Summ;
					}
				}
			}
			return summ;
		}

		

		#region Выбор владельца автомобиля
		private void SetPartner(DbPartner element)
		{
            // Перед выбором клиента необходимо выбрать подразделение
			if(card.CodeWorkshop == 0)
			{
				MessageBox.Show("Необходмо выбрать подразделение");
				return;
			}
			if(card.Workshop == null)
			{
				MessageBox.Show("Необходмо выбрать подразделение");
				return;
			}
			// Контроль ошибок
			if(element == null) return;	
            // Проверка на наличие электронной почты
            DtPartner prt = DbSqlPartner.Find(element.Code);
            if (prt != null)
            {
                if (prt.GetMail() == "")
                {
                    MessageBox.Show("Уточните e-mail клиента!");
                }
            }

			// Устанавливаем владельца автомобиля по карточке
			card.Partner				= element;
			textBoxPartner.Text			= element.Title;
			textBoxPartnerAddress.Text	= element.AddressTxt;
			textBoxPartnerPhone.Text	= element.PhoneTxt;
			// Контроль примечания
			if(card.Partner.Comment.Length != 0)
				MessageBox.Show(card.Partner.Comment, "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

			// Работа со свойствами контрагентов
			if(card.CodePartner != 0)
			{
				// Проверка на выдачу дисконтной карты
				DtDiscount discount = DbSqlDiscount.FindPartner(card.CodePartner);
				if(discount != null)
				{
					if((bool)discount.GetData("ФЛАГ_ВЫДАНО_ДИСКОНТ")==false)
					{
						MessageBox.Show("Выдайте карточку №" + discount.GetData("КОД_ДИСКОНТ").ToString());
						if(MessageBox.Show("Карточка выдана?", "Запрос", MessageBoxButtons.YesNo)==DialogResult.Yes)
						{
							DbSqlDiscount.Give((long)discount.GetData("КОД_ДИСКОНТ"));
						}
					}
				}

				partner_property = DbSqlPartnerProperty.Find(card.CodePartner);
				if(partner_property != null)
				{
					checkBoxCashless.Checked	= partner_property.Cashless;
					if(partner_property.Cashless)
						textBox_cashless.Text		= "Безналичный";
					else
						textBox_cashless.Text		= "Наличный";
					// Выставляем скидку по данному клиенту
					//if (MessageBox.Show("Подтверждаете скидку " + partner_property.Discount.ToString() + "% ?", "Запрос", MessageBoxButtons.YesNo) == DialogResult.Yes)
					// Скидка предоставляется только для сервиса

					// СКИДКА
					card.DiscountWork		= 0;
					//if(card.Workshop.Code == 1)
					//	card.DiscountWork		= partner_property.Discount;
					//else
					//	card.DiscountWork		= 0;
				}
				else
				{
					card.DiscountWork		= 0;
					checkBoxCashless.Checked	= false;
					textBox_cashless.Text		= "Наличный";
				}
			}
			textBox_discount.Text	= card.DiscountWorkTxt;
			// Пока все по старому
			// Тут же предлагаем выбрать автомобиль данного клиента
			FormAutoList dialogAuto = new FormAutoList(Db.ClickType.Select, element);
			dialogAuto.ShowDialog(this);
			if(dialogAuto.DialogResult != DialogResult.OK) return;

			SetAuto(dialogAuto.Auto);
			return;

			card.Auto = dialogAuto.Auto;
			textBoxAuto.Text = card.AutoTxt;
			textBoxAutoType.Text = card.AutoTypeTxt;

			// И опять контроль свойств автомобиля
			// Проверяем наличие номера для запчастей
			if(card.Auto.NoSparePartNumber == false && card.Auto.SparePartNumber == 0)
			{
				MessageBox.Show("Установите пожалуйста НОМЕР ДЛЯ ЗАПЧАСТЕЙ автомобиля");
				FormAuto dialog1 = new FormAuto(card.Auto);
				dialog1.ShowDialog();
				if(dialog1.DialogResult != DialogResult.OK) return;
				card.Auto = dialog1.Auto;
			}
			// Будет проверка на предписания
			card.Auto.DirectionReport();
		}
		private void RestorePartner()
		{
			// Контроль ошибок
			// Устанавливаем владельца автомобиля по карточке
			DbPartner element			= card.Partner;
			if(element == null)
			{
				textBoxPartner.Text			= "";
				textBoxPartnerAddress.Text	= "";
				textBoxPartnerPhone.Text	= "";
				return;
			}
			textBoxPartner.Text			= element.Title;
			textBoxPartnerAddress.Text	= element.AddressTxt;
			textBoxPartnerPhone.Text	= element.PhoneTxt;
		}
		private void buttonSelectPartner_Click(object sender, System.EventArgs e)
		{
			// Выбор клиента по данной карточке
			FormPartnerList dialog = new FormPartnerList();
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			SetPartner(dialog.Partner);
		}
		private void textBoxPartner_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Enter)
			{
				// Инициализация поиска контрагента по наименованию
				string name		= textBoxPartner.Text;
				name			= name.Trim();
				if(name.Length	== 0) return;
				string pattern	= "%" + name + "%";
				ArrayList array = new ArrayList();
				DbPartner.FillArray(array, false, 3, pattern);
				if(array.Count == 1)
				{
					// В точности один контрагент попадающий под условия поиска
					DbPartner element = (DbPartner)array[0];
					SetPartner(element);
					return;
				}
				if(array.Count > 1)
				{
					// Инициация выбора из существующего массива
					FormSelectElement dialog = new FormSelectElement(array);
					dialog.ShowDialog();
					if(dialog.DialogResult != DialogResult.OK)
					{
						// Отказ
						RestorePartner();
						return;	
					}
					if(dialog.SelectedElement != null)
					{
						// Выбор сделан
						DbPartner element = (DbPartner)dialog.SelectedElement;
						SetPartner(element);
						return;
					}

				}
				// Если оказались здесь - предполагается ввод нового контрагента
				// Запрос введения физического лица
				bool	juridical;
				DialogResult res = MessageBox.Show(this, "Ввод нового физического лица?", "Запрос", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
				if(res == DialogResult.Cancel)
				{
					// Отказ
					RestorePartner();
					return;	
				}
				if(res == DialogResult.Yes) juridical	= false;
				else juridical	= true;	
				// Диалог ввода нового контрагента
				FormPartner dialog1 = new FormPartner(null, juridical, name);
				dialog1.ShowDialog(this);
				if(dialog1.DialogResult != DialogResult.OK)
				{
					// Отказ
					RestorePartner();
					return;	
				}
				SetPartner(dialog1.Partner);
			}
		}
		private void textBoxPartner_LostFocus(object sender, EventArgs e)
		{
			RestorePartner();
		}
		#endregion

		protected override void OnCreateControl()
		{
            if (autocreate == true) return;   // Для нормальной работы автоматического создателя
			// ПОСЛЕДОВАТЕЛЬНОСТЬ ДЕЙСТВИЙ ПРИ ОТКРЫТИИ НОВОЙ КАРТОЧКИ
			if(card.Number == 0)
			{
               
               
                    // Выбор подразделения
                    SelectWorkshop();
                    // Запрос свидетельства о регистрации транспортного средства
                    SelectLicenceVehicle();
              
				
			}
			// Передаем управление выбору владельца автомобиля
			groupBox4.Focus();
			groupBox4.Select();
			groupBox4.SelectNextControl(groupBox4, true, true, true, true);
		}

		private bool SelectLicenceVehicle()
		{
			// Запрос свидетельства о регистрации транспортного средства
			string number = UserInterface.Selector_String("Номер свидетельства о регистрации ТС", "");
			if(number == "") return false;		
			// Ввод или выбор свидетельсва о регистрации
			string mask = "%" + number + "%";
			ArrayList array = new ArrayList();
			DbSqlLicenceVehicle.SelectInArrayLicenceNumber(array, mask); 
			CS_LicenceVehicle licence = null;
			if(array.Count == 0)
			{
				// Не нашли ничего похожего, вводим новое свидетельство о регистрации
				UIF_LicenceVehicle dialog = new UIF_LicenceVehicle(null, number);
				if(dialog.ShowDialog() != DialogResult.OK) return false;
				licence = dialog.Licence;
			}
			if(array.Count > 1)
			{
				UIF_LicenceVehicleList dialog1 = new UIF_LicenceVehicleList(array);
				if(dialog1.ShowDialog() != DialogResult.OK)
				{
					UIF_LicenceVehicle dialog2 = new UIF_LicenceVehicle(null, number);
					if(dialog2.ShowDialog() != DialogResult.OK) return false;
					licence	= dialog2.Licence;
				}
				else
				{
					licence = dialog1.licence_selected;
				}
			}
			if(array.Count == 1)
			{
				CS_LicenceVehicle lic = (CS_LicenceVehicle)array[0];
				UIF_LicenceVehicle dialog = new UIF_LicenceVehicle(lic, "");
				if(dialog.ShowDialog() != DialogResult.OK)
				{
					// Это не то свидетельство о регистрации, вводим новое
					UIF_LicenceVehicle dialog1 = new UIF_LicenceVehicle(null, number);
					if(dialog1.ShowDialog() != DialogResult.OK) return false;
					licence	= dialog1.Licence;
				}
				else
				{
					licence = lic;
				}
			}
			// Проверка на програмные сбои
			if(licence == null) return false;
			if(licence.code_auto == 0) return false;
			if(licence.code_owner == 0) return false;
			// Производим автоматическое заполнение данных карточки
			bool flag = SetLicenceVehicle(licence);
			if(flag)
			{
				the_card_change.CodeLicenseVihicle = licence.code;
			}
			
			return true;
		}

		private bool SetLicenceVehicle(CS_LicenceVehicle licence)
		{
			bool flag = true;
			flag = flag && SetOwner(licence.code_owner);
			flag = flag && SetAuto(licence.code_auto);
			return flag;
		}
		private bool SetOwner(long code_owner)
		{
			DbPartner element = DbPartner.Find(code_owner);
			if(element == null) return false;
			// Перед выбором клиента необходимо выбрать подразделение
			if(card.CodeWorkshop == 0)
			{
				MessageBox.Show("Необходмо выбрать подразделение");
				return false;
			}
			if(card.Workshop == null)
			{
				MessageBox.Show("Необходмо выбрать подразделение");
				return false;
			}
			// Контроль ошибок
		
			// Устанавливаем владельца автомобиля по карточке
			card.Partner				= element;
			textBoxPartner.Text			= element.Title;
			textBoxPartnerAddress.Text	= element.AddressTxt;
			textBoxPartnerPhone.Text	= element.PhoneTxt;
			// Контроль примечания
			if(card.Partner.Comment.Length != 0)
				MessageBox.Show(card.Partner.Comment, "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

			// Работа со свойствами контрагентов
			if(card.CodePartner != 0)
			{
				// Проверка на выдачу дисконтной карты
				DtDiscount discount = DbSqlDiscount.FindPartner(card.CodePartner);
				if(discount != null)
				{
					if((bool)discount.GetData("ФЛАГ_ВЫДАНО_ДИСКОНТ")==false)
					{
						MessageBox.Show("Выдайте карточку №" + discount.GetData("КОД_ДИСКОНТ").ToString());
						if(MessageBox.Show("Карточка выдана?", "Запрос", MessageBoxButtons.YesNo)==DialogResult.Yes)
						{
							DbSqlDiscount.Give((long)discount.GetData("КОД_ДИСКОНТ"));
						}
					}
				}

				partner_property = DbSqlPartnerProperty.Find(card.CodePartner);
				if(partner_property != null)
				{
					checkBoxCashless.Checked	= partner_property.Cashless;
					if(partner_property.Cashless)
						textBox_cashless.Text		= "Безналичный";
					else
						textBox_cashless.Text		= "Наличный";
					// Выставляем скидку по данному клиенту
					//if (MessageBox.Show("Подтверждаете скидку " + partner_property.Discount.ToString() + "% ?", "Запрос", MessageBoxButtons.YesNo) == DialogResult.Yes)
					// Скидка предоставляется только для сервиса

					// СКИДКА
					card.DiscountWork		= 0;
					//if(card.Workshop.Code == 1)
					//	card.DiscountWork		= partner_property.Discount;
					//else
					//	card.DiscountWork		= 0;
				}
				else
				{
					card.DiscountWork		= 0;
					checkBoxCashless.Checked	= false;
					textBox_cashless.Text		= "Наличный";
				}
			}
			textBox_discount.Text	= card.DiscountWorkTxt;
			return true;
		}
		private bool SetAuto(long code_auto)
		{
			// Пока все по старому
			// Тут же предлагаем выбрать автомобиль данного клиента
			DbAuto auto = DbAuto.Find(code_auto);
			if(auto == null) return false;
	
			SetAuto(auto);
			return true;

			//	card.Auto = dialogAuto.Auto;
			//	textBoxAuto.Text = card.AutoTxt;
			//	textBoxAutoType.Text = card.AutoTypeTxt;

			// И опять контроль свойств автомобиля
			// Проверяем наличие номера для запчастей
			if(card.Auto.NoSparePartNumber == false && card.Auto.SparePartNumber == 0)
			{
				MessageBox.Show("Установите пожалуйста НОМЕР ДЛЯ ЗАПЧАСТЕЙ автомобиля");
				FormAuto dialog1 = new FormAuto(card.Auto);
				dialog1.ShowDialog();
				if(dialog1.DialogResult != DialogResult.OK) return true;
				card.Auto = dialog1.Auto;
			}
			// Будет проверка на предписания
			card.Auto.DirectionReport();
		}
		private bool SelectWorkshop()
		{
			ArrayList array = new ArrayList();
			DbSqlWorkshop.SelectInArray(array);
			UIF_Selector_Array dialog = new UIF_Selector_Array(array);
			if(dialog.ShowDialog() != DialogResult.OK) return false;

			long code = dialog.SelectedCode;
			DtWorkshop workshop = DbSqlWorkshop.Find(code);
			if(workshop == null) return false;
			card.CodeWorkshop = code;
			textBoxWorkshop.Text	= (string)workshop.GetData("НАИМЕНОВАНИЕ_ЦЕХ");
			// Для совместимости со старой версией
			DbWorkshop wsh = DbWorkshop.Find(code);
			card.Workshop = wsh;
			return true;
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			bool recive = true;
			// Отметка получения деталей по выбранному списку
			DbStaff reciver = DbStaff.GetByESign("Электронная подпись получателя");
			if(reciver == null) return;
			foreach(ListViewItem itm in this.listView3.SelectedItems)
			{
				DbCardDetail dtl = (DbCardDetail)itm.Tag;
				recive = true;
				if(dtl.Check == true) recive = false;
				if(dtl.Outer == true) recive = false;
				if(dtl.Recived == true) recive = false;
				if(recive)
				{
					if(dtl.WriteRecive(reciver.Code) == true)
					{
						dtl.SetLVItem(itm);
					}
				}
			}
		}

		private void button3_Click(object sender, System.EventArgs e)
		{
			bool ret = true;
			// Возврат деталей по списку
			DbStaff returner = DbStaff.GetByESign("Электронная подпись принимающего деталь на склад");
			if(returner == null) return;
			foreach(ListViewItem itm in this.listView3.SelectedItems)
			{
				ret = true;
				DbCardDetail dtl = (DbCardDetail)itm.Tag;
				if(dtl.Recived == false) ret = false;
				if(ret)
				{
					if(dtl.WriteReturn(returner.Code) == true)
					{
					}
				}
			}
			// Обновляем список
			// Пробуем загрузить список деталей карточки
			listView3.Items.Clear();
			DbCardDetail.FillList(listView3, card);
			SetListIndexes(listView3);
			//Выставляем сумму полученных деталей
			textBoxDetailSumm.Text	= Db.CachToTxt(SetDetailSumm());
		}

		private void checkBoxCashless_CheckedChanged(object sender, System.EventArgs e)
		{
			// Проверяем на возможность установки выбранного типа
			if(checkBoxCashless.Enabled == false) return;	// Предустановленно
			if(checkBoxCashless.Checked == false) return;

			if(partner_property == null || partner_property.Cashless == false)
			{
				MessageBox.Show("Для данного контрагента не разрешен безналичный расчет");
				return;
			}
		}

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			// Оформляем заявку на выбранную деталь
			ListViewItem item = Db.GetItemSelected(listView3);
			if(item == null) return;
			if(item.Tag == null) return;
			DbCardDetail card_detail = (DbCardDetail)item.Tag;
			DtStorageRequest request = new DtStorageRequest();
			request.SetData("ССЫЛКА_КОД_СКЛАД_ДЕТАЛЬ", card_detail.CodeDetailStorage);
			request.SetData("НАИМЕНОВАНИЕ_СКЛАД_ДЕТАЛЬ", card_detail.DetailNameTxt);
			request.SetData("ССЫЛКА_НОМЕР_КАРТОЧКА", card.Number);
			request.SetData("ССЫЛКА_ГОД_КАРТОЧКА", card.Year);
			request.SetData("ССЫЛКА_КОД_КОНТРАГЕНТ", card.CodePartner);
			request.SetData("КОЛИЧЕСТВО_СКЛАД_ДЕТАЛЬ", card_detail.Quontity);
			request.SetData("ГАРАНТИЯ_ЗАЯВКА", card_detail.Guaranty);

			FormUpdateStorageRequest dialog = new FormUpdateStorageRequest(request);
			dialog.Show();
		}

		private void listView3_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			// На поднятие кнопки мышки - меню
			if(e.Button == MouseButtons.Right)
			{
				// На отпускание правой кнопки мышки - всплывающее меню
				// Настройка меню
				// Показ меню
				contextMenu1.Show(listView3, new Point(e.X, e.Y));
			}
		}

		// Огранизация связи со внешним миром
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
				DtCard card1 = DbSqlCard.FindList(this.card);
				card1.SetLVItem(item);
				outer_item	= item;
				outer_list	= null;
				return;
			}
			else
			{
				if(outer_item == null) return;
				if(outer_item.ListView == null) return;
				if(outer_item.ListView.IsDisposed == true) return;
				DtCard card1 = DbSqlCard.FindList(this.card);
				//DtCard card1 = DbSqlCard.Find(this.Card.Number, this.Card.Year);
				card1.SetLVItem(outer_item);
			}
		}
		// Конец организации связи
		// Работа с заявками
		private void button_new_request_Click(object sender, System.EventArgs e)
		{
			// Создание новой строки заявки
			FormManageClaim form = new FormManageClaim(this, the_card, GetClaimList());
			form.ShowDialog();
		}
		private void listView_requset_layount(object sender, LayoutEventArgs e)
		{
			
		}
		private void listView_requset_invalidate(object sender, CancelEventArgs e)
		{
			
		}
		// КОНЕЦ - Работа с заявками
		private void button_selection_Click(object sender, System.EventArgs e)
		{
			FormWorkManagment dialog = new FormWorkManagment(this);
			dialog.Show();
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
			FormWorkManagment dialog = new FormWorkManagment(this);
			dialog.Show();
		}

		private void button_remove_claim_Click(object sender, System.EventArgs e)
		{
			// Удаляем выбранную заявку
			// Удаляем выбранный элемент списка
			ListViewItem item = null;
			long code = 0;
			
			item = Db.GetItemSelected(listView_request);
			if(item == null) return;
			code = (long)item.Tag;
			if(code == 0) return;

			// Удаляем заявку
			if(the_card == null) return;
			if(DbSqlCardClaim.Remove(the_card, code) == false) return;
			listView_request.Items.Remove(item);
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
			if(card == null)return false;
			if((long)card.Number == 0)return false;
			DtCardDetailOrder order = new DtCardDetailOrder();
			order.SetData("НОМЕР_КАРТОЧКА", (long)card.Number);
			order.SetData("ГОД_КАРТОЧКА", (int)card.Year);
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



		private bool CheckAutoPermision(long code_auto)
		{
			ArrayList array = new ArrayList();
			DbSqlStaff.SelectInArrayAutoRestriction(array, code_auto);
			if(array.Count == 0) return true;
			long sign = 0;
			FormSelectString dialog = new FormSelectString("Введите электронную подпись", "", true);
			if(dialog.ShowDialog() != DialogResult.OK) return false;
			sign = dialog.SelectedLong;
			DtStaff staff = DbSqlStaff.FindSign(sign);
			if(staff == null) return false;
			foreach(object o in array)
			{
				DtStaff stf = (DtStaff)o;
				if ((long)stf.GetData("КОД_ПЕРСОНАЛ") == (long)staff.GetData("КОД_ПЕРСОНАЛ")) return true;
			}
			return false;
		}

		private void ServiceHistory(long auto, long workshop)
		{
			DateTime date_end	= DateTime.Now;
			DateTime date_start = date_end;
			date_start			= date_start.AddYears(-1);

			ArrayList array = new ArrayList();
			DbSqlCard.SelectCardClosedNumberWorkshopAuto(array, date_start, date_end, workshop, auto);

			ArrayList claims = new ArrayList();

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
				if (guaranty)
				{
					DateTime work_begin = (DateTime)card.GetData("ДАТА_НАРЯД_ОТКРЫТ_КАРТОЧКА");
					DateTime work_end;
					DtCardMarkEndWork mark_workend = DbSqlCardMarkEndWork.Find(card);
					if (mark_workend != null)
						work_end = (DateTime)mark_workend.GetData("ДАТА");
					else
						work_end = (DateTime)card.GetData("ДАТА_НАРЯД_ЗАКРЫТ_КАРТОЧКА");

					if (work_begin > work_end)
					{
						MessageBox.Show("В карточке №" + number.ToString() + " / " + year.ToString() + " ошибочные даты");
					}
					else
					{
						TimeSpan span = new TimeSpan(work_begin.Ticks);
						DateTime diff = work_end.Subtract(span);
						int count = diff.DayOfYear;
						day_count = day_count + count;
					}
				}

				// Анализируем заявку
				ArrayList claim = new ArrayList();
				DbSqlCardClaim.SelectInArray(claim, number, year);

				foreach(object o1 in claim)
				{
					DtCardClaim claim_data = (DtCardClaim)o1;
					bool find = false;
					foreach(object o2 in claims)
					{
						ClaimData data = (ClaimData)o2;
						long claim_code = (long)claim_data.GetData("ССЫЛКА_КОД_ЗАЯВКА");
						if(claim_code == data.code)
						{
							data.count++;
							data.last	= date;
							find = true;
						}
					}
					if(find == false)
					{
						ClaimData data = new ClaimData();
						data.count	= 1;
						data.code	= (long)claim_data.GetData("ССЫЛКА_КОД_ЗАЯВКА");
						data.txt	= (string)claim_data.GetData("НАИМЕНОВАНИЕ_ЗАЯВКА");
						data.last	= date;
						claims.Add(data);
					}
				}
			}

			// Выдаем результат
			string message = "";
			foreach(object o in claims)
			{
				ClaimData data = (ClaimData)o;
				message = message + data.last.ToShortDateString() + " "  + data.txt + " " + data.count.ToString() + "\n";
			}
			message += "Время в гарантийном ремонте " + day_count.ToString();
			MessageBox.Show(message);
		}

		private void AlarmHistory(long auto)
		{
			DtAutoAlarm alarm = DbSqlAutoAlarm.FindLast(auto);
			if(alarm == null)
			{
				MessageBox.Show("Проверьте установку сигнализации");
				return;
			}
			DateTime now = DateTime.Now;
			DateTime last = (DateTime)alarm.GetData("ДАТА_УСТАНОВКА");

			TimeSpan span = now - last;
			string txt;
			string alarm_txt;
			string set_txt;
			if(span.Days > 1000)
			{
				alarm_txt = " (";
				DtListAlarm tmp_alarm = DbSqlListAlarm.Find((long)alarm.GetData("КОД_СИГНАЛИЗАЦИЯ"));
				if(tmp_alarm != null)
				{
					txt = (string)tmp_alarm.GetData("НАИМЕНОВАНИЕ");
					alarm_txt = alarm_txt + txt + ", ";
				}
				DtServiceOuter tmp_service = DbSqlServiceOuter.Find((long)alarm.GetData("КОД_СЕРВИС"));
				if(tmp_service != null)
				{
					txt = (string)tmp_service.GetData("НАИМЕНОВАНИЕ");
					alarm_txt = alarm_txt + txt + ", ";
				}
				DtCommonReason tmp_reason = DbSqlCommonReason.Find((long)alarm.GetData("КОД_ПРИЧИНА"));
				if(tmp_reason != null)
				{
					txt = (string)tmp_reason.GetData("ОПИСАНИЕ");
					alarm_txt = alarm_txt + txt;
				}
				alarm_txt = alarm_txt + ")";
				long flag = (long)alarm.GetData("ФЛАГ_УСТАНОВКА");
				if(flag == 0)
					set_txt = "Сигнализация установлена у нас: ";
				else
					set_txt = "Сигнализация установлена на стороне: ";
				MessageBox.Show(set_txt + last.ToShortDateString() + alarm_txt + " (Предложить новую)");
			}
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



		private void button_print_invite_Click(object sender, System.EventArgs e)
		{
		//	DbPrintInvite print = new DbPrintInvite(this);
		//	print.Print();
		}

		private void button_select_consultant_Click(object sender, System.EventArgs e)
		{
			if (the_card == null) return;
			if ((long)the_card.GetData("НОМЕР_КАРТОЧКА") == 0) return;
			// Выбрать ведущего сервис-консультанта
			FormListStaff dialog;
			dialog = new FormListStaff(2, 0);
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			DtStaff staff = dialog.SelectedStaff;
			// Производим запись
			if (DbSqlCard.SetServiceManagerEver(the_card, staff) == false) return;
			// Если удачно - пишем сервис консультанта
			textBox_consultant.Text = staff.Title;
			ShowChanges();
		}

		private void checkBox_inner_warranty_CheckedChanged(object sender, System.EventArgs e)
		{
		
		}

		private void button_set_present_Click(object sender, System.EventArgs e)
		{
			// Установить как подарок
			ListViewItem item = Db.GetItemSelected(listView3);
			if(item == null) return;
			DbCardDetail element = (DbCardDetail)item.Tag;
			if(element == null) return;

			if (element.Guaranty == true)
			{
				MessageBox.Show("Деталь гарантийная!");
				return;
			}
			
			long pos = element.Number;
			if(DbSqlCardDetail.SetPresent(the_card, pos) == false) return;
			element.Present = true;
			element.SetLVItem(item);
		}

		private void button_unset_present_Click(object sender, System.EventArgs e)
		{
			// Снять отметку подарка
			ListViewItem item = Db.GetItemSelected(listView3);
			if(item == null) return;
			DbCardDetail element = (DbCardDetail)item.Tag;
			if(element == null) return;

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

		
		public ListView DetailList
		{
			get
			{
				return listView3;
			}
		}


        private void button_setdiscountparts_Click(object sender, EventArgs e)
        {
            // Установить скидку на З/Ч
            float discount = 0.0F;
            string text = textBox_discountparts.Text;
            text.Trim();
            text = text.Replace(".", ",");
            float data = 0.0F;
            try
            {
                data = (float)Convert.ToDouble(text);
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
                return;
            }
            discount = data;
            DbSqlCard.SetDiscountParts(card, discount);
            
        }

       

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
            if(dialog.ShowDialog() != DialogResult.OK){return;}
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

     

		#region работа со списком работ
		private void button_workpresent_Click(object sender, EventArgs e)
		{
			// Установить работу как бонус
		}

		private void button_worknopresent_Click(object sender, EventArgs e)
		{
			// снять отметку бонуса
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
			foreach (ListViewItem item in WorkList.SelectedItems)
			{
				wrk = (DbCardWork)item.Tag;
				if (wrk != null)
				{
					if (wrk.Number > 0)
					{
						if (DbSqlCardWork.SetDiscount(card.Number, card.Year, wrk.Number, data) == true)
						{
							wrk.Discount = data;
							wrk.SetLVItem(item);
						}
					}
				}
			}
		}

		private void button_set_discount_work_Click(object sender, EventArgs e)
		{
			// Установить скидку на работы
			string discount_text = textBox_dsknt.Text;
			discount_text.Trim();
			discount_text = discount_text.Replace(".", ",");
			float data = 0.0F;
			try
			{
				data = (float)Convert.ToDouble(discount_text);
			}
			catch (Exception E)
			{
				MessageBox.Show(E.Message);
			}
			float discount_work_val = data;
			if (DbSqlCard.SetDiscount(card, discount_work_val, 0) == true)
			{
				textBox_dsknt.Text = discount_work_val.ToString();
			}
			else
			{
				textBox_dsknt.Text = "";
			}
		}
		private void menuItem4_Click(object sender, EventArgs e)
		{
			// Всем выбранным работам по саиску устанавливается видимое примечание по отсутствию гарантии
			DbCardWork wrk = null;
			foreach (ListViewItem item in listView1.SelectedItems)
			{
				wrk = (DbCardWork)item.Tag;
				if (wrk != null)
				{
					if (wrk.Number > 0)
					{
						DbSqlCardWorkComment.InsertConnection(card.Number, card.Year, wrk.Number, 7754, true);
					}
				}
			}
		}
		public ListView WorkList
		{
			get
			{
				return listView1;
			}
		}
		private void buttonNewWork_Click(object sender, System.EventArgs e)
		{
			if (card.AutoType == null) return;
			// Добавление в список выполняемой работы
			FormWorkList dialog = new FormWorkList(card.AutoType, null);
			dialog.ShowDialog();
			if (dialog.DialogResult != DialogResult.OK) return;
			DbCardWork work = new DbCardWork(dialog.SelectedWork, card);
			work.AddToList(listView1, true);
			SetListIndexes(listView1);
			MakeSumWork();
		}

		private void buttonGarantyOn_Click(object sender, System.EventArgs e)
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

		private void buttonGuarantyOf_Click(object sender, System.EventArgs e)
		{
			// Отметить все выбранные элементы как гарантийные
			foreach (ListViewItem item in listView1.SelectedItems)
			{
				DbCardWork work = (DbCardWork)item.Tag;
				if (work == null) return;
				work.Guaranty = false;
				work.SetLVItem(item);
			}
			listView1.SelectedItems.Clear();
			MakeSumWork();
		}
		protected void listView1_KeyDown(object sender, KeyEventArgs e)
		{
			if (!card.CanUpdateWarrant) return; // Нельзя изменять содержимое заказ-наряда

			if (e.KeyCode == Keys.Delete)
			{
				foreach (ListViewItem item in listView1.SelectedItems)
				{
					DbCardWork wrk = (DbCardWork)item.Tag;
					if (wrk == null) return;
					if (wrk.Exist)
					{
						if (wrk.Deleted)
							wrk.Deleted = false;
						else
							wrk.Deleted = true;
						wrk.SetLVItem(item);
					}
					else
					{
						listView1.Items.Remove(item);
						SetListIndexes(listView1);
					}
				}
				listView1.SelectedItems.Clear();
				MakeSumWork();
			}
			if (e.KeyCode == Keys.Add)
			{
				// Увеличиваем на единицу количество выбранных работ
				foreach (ListViewItem item in listView1.SelectedItems)
				{
					DbCardWork wrk = (DbCardWork)item.Tag;
					if (wrk != null)
					{
						wrk.Quontity = wrk.Quontity + 1;
						wrk.SetLVItem(item);
					}
				}
				MakeSumWork();
			}
			if (e.KeyCode == Keys.Subtract)
			{
				// Уменьшаем на единицу количество выбранных работ
				foreach (ListViewItem item in listView1.SelectedItems)
				{
					DbCardWork wrk = (DbCardWork)item.Tag;
					if (wrk != null)
					{
						wrk.Quontity = wrk.Quontity - 1;
						if (wrk.Quontity == 0) wrk.Quontity = 1;
						wrk.SetLVItem(item);
					}
				}
				MakeSumWork();
			}
		}
		private void buttonDone_Click(object sender, System.EventArgs e)
		{
			DbCardWork wrk;

			if (!card.IsWarrantOpened)
			{
				MessageBox.Show("Нельзя выполнять работы в неоткрытом заказ-наряде");
				return;
			}

			// #### Новый блок!
			// Проверяем работы на пренадлежность одному типу (пока только Мойка/Не мойка)
			bool wash = false;
			bool other = false;
			foreach (ListViewItem item in listView1.SelectedItems)
			{
				wrk = (DbCardWork)item.Tag;
				if (wrk != null)
				{
					// КОД МОЙКИ
					if (wrk.CodeDirectoryWork == 722)
						wash = true;
					else
						other = true;
				}
			}
			if (other == true && wash == true)
			{
				MessageBox.Show("Нельзя объеденять слесарные работы с мойкой!");
				return;
			}
			// #### Конец новый блок

			// #### НАЧАЛО Старая форма диалога
			// FormStaffList dialog1 = new FormStaffList();
			// dialog1.ShowDialog(this);
			// if(dialog1.DialogResult != DialogResult.OK) return;
			// ListView list = dialog1.List;
			// foreach(ListViewItem item in listView1.SelectedItems)
			// {
			// 	wrk = (DbCardWork)item.Tag;
			// 	if(wrk != null)
			// 	{
			// 		if(wrk.Number > 0)
			// 		{
			// 			DbCardWorkPersonal.WriteListSelection(list, wrk);
			// 			wrk.SetLVItem(item);
			// 		}
			// 	}
			// }
			// #### КОНЕЦ Старая форма диалога

			// #### НОВЫЙ ВАРИАНТ Сразбивкой по типам исполнителей
			FormListStaff dialog1;
			if (wash == true)
				dialog1 = new FormListStaff(3, 0);
			else
			{
				if (card.Workshop.Code == 1)
					dialog1 = new FormListStaff(1, 0);
				else
					dialog1 = new FormListStaff(0, card.Workshop.Code);
			}
			dialog1.ShowDialog(this);
			if (dialog1.DialogResult != DialogResult.OK) return;
			ListView list = dialog1.List;
			foreach (ListViewItem item in listView1.SelectedItems)
			{
				wrk = (DbCardWork)item.Tag;
				if (wrk != null)
				{
					if (wrk.Number > 0)
					{
						DbCardWorkPersonal.WriteListSelectionCode(list, wrk);
						wrk.SetLVItem(item);
					}
				}
			}
			// #### КОНЕЦ НОВЫЙ ВАРИАНТ
		}
		private void menuItem3_Click(object sender, System.EventArgs e)
		{
			// Вызов формы корректировки/добавления/исправления примечаний работы.
			// Оформляем заявку на выбранную деталь
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
		private void button_comment_Click(object sender, System.EventArgs e)
		{
			// Добавляем примечание к выбранной работе
			// Изменение цены выбранной трудоемкости
			ListViewItem item = Db.GetItemSelected(listView1);
			if (item == null) return;
			DbCardWork element = (DbCardWork)item.Tag;
			if (element == null) return;
			long card_number = card.Number;
			int card_year = card.Year;
			int position = element.Number;
			long code = 0;
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
				string text = "";
				FormSelectString form1 = new FormSelectString("Текст примечания", "");
				if (form1.ShowDialog() != DialogResult.OK) return;
				text = form1.SelectedText;
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
		protected void listView1_DoubleClick(object sender, EventArgs e)
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
		private void buttonUnDone_Click(object sender, System.EventArgs e)
		{
			// Отметить выбранные работы как не сделанные
			/*foreach(ListViewItem item in listView1.SelectedItems)
			{
				DbCardWork wrk = (DbCardWork)item.Tag;
				if(wrk != null)
				{
					wrk.DonePersonal = null;
					wrk.SetLVItem(item);
				}
			}*/

			// Второй вариант - отмена выполнения выбранных работ
			foreach (ListViewItem item in listView1.SelectedItems)
			{
				DbCardWorkPersonal cardWorkPersonal = null;
				DbCardWork wrk = (DbCardWork)item.Tag;
				// DbCardWork wrk;
							   //var wrkContainer = item.Tag;
							   //if (wrkContainer.GetType() == typeof(DbCardWork))
							   //	wrk = (DbCardWork)wrkContainer;
							   //else
							   //	wrk = new DbCardWork(DbSqlCardWork.Find(the_card, (int)wrkContainer));

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
		public ListView GetWorkList()
		{
			return WorkList;
		}
		private void buttonSetOil_Click(object sender, System.EventArgs e)
		{
			// Установить выбранные работы - входят в группу масла
			foreach (ListViewItem item in WorkList.SelectedItems)
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
			foreach (ListViewItem item in WorkList.SelectedItems)
			{
				DbCardWork work = (DbCardWork)item.Tag;
				if (work != null)
				{
					work.Oil = false;
					work.SetLVItem(item);
				}
			}
		}
		private void button1_Click(object sender, System.EventArgs e)
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

		public void TransferToForm(ListViewItem item)
		{
			if (item == null) return;
			DbWork work = (DbWork)item.Tag;
			if (work == null) return;
			DbCardWork cardWork = new DbCardWork(work, card);
			ListViewItem itm = WorkList.Items.Add(cardWork.LVItem);
			cardWork.SetLVItem(itm);
			this.SetListIndexes(WorkList);
			MakeSumWork();
		}
		private void MakeSumWork()
		{
			float summ = 0;
			foreach (ListViewItem item in WorkList.Items)
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
		}
		private void buttonChangePrice_Click(object sender, System.EventArgs e)
		{
			// Изменение цены выбранной трудоемкости
			
			ListViewItem item = Db.GetItemSelected(WorkList);
			if (item == null) return;
			//DbCardWork element = (DbCardWork)item.Tag;
			DbCardWork element = GetWorkListTagObject(item);
			if (element == null) return;
			// Запрос новой цены
			FormSelectString dialog = new FormSelectString("Новая стоимость работы", element.PriceFullTxt);
			dialog.ShowDialog();
			if (dialog.DialogResult != DialogResult.OK) return;
			float price = dialog.SelectedFloat;
			element.Price = price;
			element.SetLVItem(item);

			return;

			// А теперь работаем с колекцией работ карточки
			//ListViewItem item = Db.GetItemSelected(WorkList);
			//DtCardWork cardWork = the_card.WorkCollection.FindWork(item);
			//if (cardWork == null) return;
			//DtCardWorkDisplay displey = new DtCardWorkDisplay(cardWork);

			DtCardWorkDisplay workDispley = _display.GetSelectedCardWorkDisplay(WorkList);

			//FormSelectString dialog = new FormSelectString("Новая стоимость работы", cardWorkTxt.OperationCoast());
			//dialog.ShowDialog();
			//if (dialog.DialogResult != DialogResult.OK) return;
			object res;
			if ( (res = UserInterface.SelectFloat("Новая стоимость работы", workDispley.CardWork.OperationCost)) == null) return;
			workDispley.CardWork.OperationCost = (float)res;			

			workDispley.ListViewItem();			
		}
		private DbCardWork GetWorkListTagObject(ListViewItem item)
        {
			object o = item.Tag;
			if(o.GetType() != typeof(DbCardWork))
            {
                MessageBox.Show("Класс не соответсвует DbCardWork!");
                return null;
            }
			return (DbCardWork)item.Tag;
        }
		#endregion
	}
}
