using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormManageCard.
	/// </summary>
	public class FormManageCard : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.ColumnHeader columnHeader8;
		private System.Windows.Forms.DateTimePicker dateTimePicker_start;
		private System.Windows.Forms.DateTimePicker dateTimePicker_end;
		private System.Windows.Forms.CheckBox checkBox_nodate;
		private System.Windows.Forms.Button button_refresh;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.CheckBox checkBox_showcancel;
		private System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.Button button_closed;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.MenuItem menuItem8;
		private System.Windows.Forms.MenuItem menuItem9;
		private System.Windows.Forms.MenuItem menuItem10;
		private System.Windows.Forms.MenuItem menuItem11;
		private System.Windows.Forms.MenuItem menuItem12;
		private System.Windows.Forms.MenuItem menuItem13;
		private System.Windows.Forms.MenuItem menuItem14;
		private System.Windows.Forms.MenuItem menuItem15;
		private System.Windows.Forms.MenuItem menuItem16;
		private System.Windows.Forms.MenuItem menuItem17;
		private System.Windows.Forms.MenuItem menuItem18;

		// Структура поиска
		SearchCard search_card = new SearchCard();
		Db.ClickType click_type;
		private System.Windows.Forms.MenuItem menuItem19;
		private System.Windows.Forms.MenuItem menuItem20;
		private System.Windows.Forms.MenuItem menuItem21;
		private System.Windows.Forms.MenuItem menuItem22;
		private System.Windows.Forms.MenuItem menuItem23;
		private System.Windows.Forms.MenuItem menuItem24;
		private System.Windows.Forms.MenuItem menuItem25;
		private System.Windows.Forms.MenuItem menuItem26;
		private System.Windows.Forms.MenuItem menuItem27;
		private System.Windows.Forms.MenuItem menuItem28;
		private System.Windows.Forms.MenuItem menuItem29;
		private System.Windows.Forms.MenuItem menuItem30;
		private System.Windows.Forms.MenuItem menuItem31;
		private System.Windows.Forms.MenuItem menuItem32;
		private System.Windows.Forms.ColumnHeader columnHeader9;
		private System.Windows.Forms.MenuItem menuItem33;
		private System.Windows.Forms.TextBox textBox_workshop;
		public DtCard	card_selected;
		private System.Windows.Forms.Button button_detail_get;
		private System.Windows.Forms.Button button_detail_return;
		private System.Windows.Forms.MenuItem menuItem34;
		private System.Windows.Forms.MenuItem menuItem35;
		private System.Windows.Forms.MenuItem menuItem36;
		private System.Windows.Forms.MenuItem menuItem37;
		private System.Windows.Forms.MenuItem menuItem38;
		private System.Windows.Forms.MenuItem menuItem39;
		private System.Windows.Forms.MenuItem menuItem40;
		private System.Windows.Forms.MenuItem menuItem41;
		private System.Windows.Forms.MenuItem menuItem42;
		private System.Windows.Forms.MenuItem menuItem43;
		private System.Windows.Forms.MenuItem menuItem44;
		private System.Windows.Forms.MenuItem menuItem45;
		private System.Windows.Forms.MenuItem menuItem46;
		private System.Windows.Forms.MenuItem menuItem47;
		private System.Windows.Forms.MenuItem menuItem48;
		private System.Windows.Forms.MenuItem menuItem49;
		private System.Windows.Forms.MenuItem menuItem50;
		private System.Windows.Forms.MenuItem menuItem51;
		private System.Windows.Forms.MenuItem menuItem52;
		private System.Windows.Forms.MenuItem menuItem53;
		private System.Windows.Forms.MenuItem menuItem54;
		private System.Windows.Forms.MenuItem menuItem55;
		private System.Windows.Forms.ColumnHeader columnHeader10;
		private System.Windows.Forms.MenuItem menuItem56;
		private System.Windows.Forms.MenuItem menuItem57;
		private System.Windows.Forms.MenuItem menuItem58;
		private System.Windows.Forms.MenuItem menuItem59;
		private System.Windows.Forms.MenuItem menuItem60;
		private System.Windows.Forms.MenuItem menuItem61;
		private System.Windows.Forms.MenuItem menuItem62;
		private System.Windows.Forms.MenuItem menuItem63;
        private MenuItem menuItem64;
        private MenuItem menuItem65;
        private MenuItem menuItem66;
        private MenuItem menuItem67;
        private MenuItem menuItem68;
        private MenuItem menuItem69;
        private MenuItem menuItem70;
        private MenuItem menuItem71;
        private MenuItem menuItem72;
        private MenuItem menuItem73;
        private MenuItem menuItem74;
        private MenuItem menuItem75;
        private MenuItem menuItem76;
        private MenuItem menuItem77;
        private MenuItem menuItem78;
        private MenuItem menuItem79;
        private MenuItem menuItem80;
        private MenuItem menuItem81;
        private MenuItem menuItem82;
        private long	selected_workshop_code = 0;

		public FormManageCard(Db.ClickType click, int object_code, object o)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			// Загрузка листа с изображениями
			imageList1.Images.Add(new Bitmap(".\\Icons\\crd000.bmp"));
			imageList1.Images.Add(new Bitmap(".\\Icons\\crd001.bmp"));
			imageList1.Images.Add(new Bitmap(".\\Icons\\crd002.bmp"));
			
			// Установка первого рабочего интервала
			DateTime date = DateTime.Now;
			date = date.AddMilliseconds(-date.Millisecond);
			date = date.AddSeconds(-date.Second);
			date = date.AddMinutes(-date.Minute);
			date = date.AddHours(-date.Hour);
			dateTimePicker_end.Value = date;
			date = date.AddDays(-3);
			dateTimePicker_start.Value = date;

			click_type	= click;

			if(object_code == 0)
				FillList();
			else
			{
				FillList(object_code, o);
			}
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormManageCard));
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.dateTimePicker_start = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker_end = new System.Windows.Forms.DateTimePicker();
            this.checkBox_nodate = new System.Windows.Forms.CheckBox();
            this.button_refresh = new System.Windows.Forms.Button();
            this.checkBox_showcancel = new System.Windows.Forms.CheckBox();
            this.contextMenu1 = new System.Windows.Forms.ContextMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItem58 = new System.Windows.Forms.MenuItem();
            this.menuItem81 = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.menuItem8 = new System.Windows.Forms.MenuItem();
            this.menuItem9 = new System.Windows.Forms.MenuItem();
            this.menuItem10 = new System.Windows.Forms.MenuItem();
            this.menuItem33 = new System.Windows.Forms.MenuItem();
            this.menuItem52 = new System.Windows.Forms.MenuItem();
            this.menuItem62 = new System.Windows.Forms.MenuItem();
            this.menuItem11 = new System.Windows.Forms.MenuItem();
            this.menuItem12 = new System.Windows.Forms.MenuItem();
            this.menuItem13 = new System.Windows.Forms.MenuItem();
            this.menuItem14 = new System.Windows.Forms.MenuItem();
            this.menuItem15 = new System.Windows.Forms.MenuItem();
            this.menuItem16 = new System.Windows.Forms.MenuItem();
            this.menuItem17 = new System.Windows.Forms.MenuItem();
            this.menuItem18 = new System.Windows.Forms.MenuItem();
            this.menuItem30 = new System.Windows.Forms.MenuItem();
            this.menuItem19 = new System.Windows.Forms.MenuItem();
            this.menuItem20 = new System.Windows.Forms.MenuItem();
            this.menuItem21 = new System.Windows.Forms.MenuItem();
            this.menuItem22 = new System.Windows.Forms.MenuItem();
            this.menuItem23 = new System.Windows.Forms.MenuItem();
            this.menuItem24 = new System.Windows.Forms.MenuItem();
            this.menuItem25 = new System.Windows.Forms.MenuItem();
            this.menuItem26 = new System.Windows.Forms.MenuItem();
            this.menuItem29 = new System.Windows.Forms.MenuItem();
            this.menuItem53 = new System.Windows.Forms.MenuItem();
            this.menuItem31 = new System.Windows.Forms.MenuItem();
            this.menuItem32 = new System.Windows.Forms.MenuItem();
            this.menuItem61 = new System.Windows.Forms.MenuItem();
            this.menuItem80 = new System.Windows.Forms.MenuItem();
            this.menuItem27 = new System.Windows.Forms.MenuItem();
            this.menuItem28 = new System.Windows.Forms.MenuItem();
            this.menuItem34 = new System.Windows.Forms.MenuItem();
            this.menuItem35 = new System.Windows.Forms.MenuItem();
            this.menuItem36 = new System.Windows.Forms.MenuItem();
            this.menuItem37 = new System.Windows.Forms.MenuItem();
            this.menuItem38 = new System.Windows.Forms.MenuItem();
            this.menuItem39 = new System.Windows.Forms.MenuItem();
            this.menuItem40 = new System.Windows.Forms.MenuItem();
            this.menuItem41 = new System.Windows.Forms.MenuItem();
            this.menuItem42 = new System.Windows.Forms.MenuItem();
            this.menuItem47 = new System.Windows.Forms.MenuItem();
            this.menuItem48 = new System.Windows.Forms.MenuItem();
            this.menuItem49 = new System.Windows.Forms.MenuItem();
            this.menuItem54 = new System.Windows.Forms.MenuItem();
            this.menuItem55 = new System.Windows.Forms.MenuItem();
            this.menuItem63 = new System.Windows.Forms.MenuItem();
            this.menuItem64 = new System.Windows.Forms.MenuItem();
            this.menuItem67 = new System.Windows.Forms.MenuItem();
            this.menuItem68 = new System.Windows.Forms.MenuItem();
            this.menuItem69 = new System.Windows.Forms.MenuItem();
            this.menuItem43 = new System.Windows.Forms.MenuItem();
            this.menuItem44 = new System.Windows.Forms.MenuItem();
            this.menuItem45 = new System.Windows.Forms.MenuItem();
            this.menuItem46 = new System.Windows.Forms.MenuItem();
            this.menuItem50 = new System.Windows.Forms.MenuItem();
            this.menuItem51 = new System.Windows.Forms.MenuItem();
            this.menuItem56 = new System.Windows.Forms.MenuItem();
            this.menuItem57 = new System.Windows.Forms.MenuItem();
            this.menuItem59 = new System.Windows.Forms.MenuItem();
            this.menuItem60 = new System.Windows.Forms.MenuItem();
            this.menuItem65 = new System.Windows.Forms.MenuItem();
            this.menuItem66 = new System.Windows.Forms.MenuItem();
            this.menuItem70 = new System.Windows.Forms.MenuItem();
            this.menuItem71 = new System.Windows.Forms.MenuItem();
            this.menuItem72 = new System.Windows.Forms.MenuItem();
            this.menuItem73 = new System.Windows.Forms.MenuItem();
            this.menuItem74 = new System.Windows.Forms.MenuItem();
            this.menuItem75 = new System.Windows.Forms.MenuItem();
            this.menuItem76 = new System.Windows.Forms.MenuItem();
            this.menuItem77 = new System.Windows.Forms.MenuItem();
            this.menuItem78 = new System.Windows.Forms.MenuItem();
            this.menuItem79 = new System.Windows.Forms.MenuItem();
            this.button_closed = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.button_detail_get = new System.Windows.Forms.Button();
            this.button_detail_return = new System.Windows.Forms.Button();
            this.textBox_workshop = new System.Windows.Forms.TextBox();
            this.menuItem82 = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
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
            this.columnHeader7,
            this.columnHeader9,
            this.columnHeader8,
            this.columnHeader10});
            this.listView1.FullRowSelect = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(11, 142);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(938, 193);
            this.listView1.StateImageList = this.imageList1;
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView1_ColumnClick);
            this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
            this.listView1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.listView1_KeyUp);
            this.listView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseUp);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "№";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Дата";
            this.columnHeader2.Width = 127;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "З/Н";
            this.columnHeader3.Width = 139;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Владелец";
            this.columnHeader4.Width = 200;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Модель";
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "VIN";
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Гос.Номер";
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Пробег";
            this.columnHeader9.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader9.Width = 90;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Мастер";
            this.columnHeader8.Width = 90;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Доп";
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // dateTimePicker_start
            // 
            this.dateTimePicker_start.Location = new System.Drawing.Point(11, 10);
            this.dateTimePicker_start.Name = "dateTimePicker_start";
            this.dateTimePicker_start.Size = new System.Drawing.Size(266, 26);
            this.dateTimePicker_start.TabIndex = 1;
            // 
            // dateTimePicker_end
            // 
            this.dateTimePicker_end.Location = new System.Drawing.Point(11, 48);
            this.dateTimePicker_end.Name = "dateTimePicker_end";
            this.dateTimePicker_end.Size = new System.Drawing.Size(266, 26);
            this.dateTimePicker_end.TabIndex = 2;
            // 
            // checkBox_nodate
            // 
            this.checkBox_nodate.Location = new System.Drawing.Point(11, 76);
            this.checkBox_nodate.Name = "checkBox_nodate";
            this.checkBox_nodate.Size = new System.Drawing.Size(266, 28);
            this.checkBox_nodate.TabIndex = 3;
            this.checkBox_nodate.Text = "Отменить интервал дат";
            // 
            // button_refresh
            // 
            this.button_refresh.Image = ((System.Drawing.Image)(resources.GetObject("button_refresh.Image")));
            this.button_refresh.Location = new System.Drawing.Point(11, 114);
            this.button_refresh.Name = "button_refresh";
            this.button_refresh.Size = new System.Drawing.Size(32, 27);
            this.button_refresh.TabIndex = 4;
            this.toolTip1.SetToolTip(this.button_refresh, "Обновить список в с соответсвии с условиями поиска");
            this.button_refresh.Click += new System.EventHandler(this.button_refresh_Click);
            // 
            // checkBox_showcancel
            // 
            this.checkBox_showcancel.Location = new System.Drawing.Point(331, 10);
            this.checkBox_showcancel.Name = "checkBox_showcancel";
            this.checkBox_showcancel.Size = new System.Drawing.Size(341, 28);
            this.checkBox_showcancel.TabIndex = 5;
            this.checkBox_showcancel.Text = "Показывать отмененные карточки";
            // 
            // contextMenu1
            // 
            this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.menuItem5,
            this.menuItem11,
            this.menuItem14,
            this.menuItem19,
            this.menuItem24,
            this.menuItem31,
            this.menuItem27,
            this.menuItem28,
            this.menuItem34,
            this.menuItem35,
            this.menuItem43,
            this.menuItem44,
            this.menuItem50,
            this.menuItem56,
            this.menuItem59,
            this.menuItem65,
            this.menuItem78,
            this.menuItem79});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem2,
            this.menuItem3,
            this.menuItem4,
            this.menuItem58,
            this.menuItem81});
            this.menuItem1.Text = "Карточка (Служебное)";
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 0;
            this.menuItem2.Text = "Проверить";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 1;
            this.menuItem3.Text = "Одобрить";
            this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 2;
            this.menuItem4.Text = "Отклонить";
            this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
            // 
            // menuItem58
            // 
            this.menuItem58.Index = 3;
            this.menuItem58.Text = "Исправить дату продажи автомобиля";
            this.menuItem58.Click += new System.EventHandler(this.menuItem58_Click);
            // 
            // menuItem81
            // 
            this.menuItem81.Index = 4;
            this.menuItem81.Text = "Тест печати";
            this.menuItem81.Click += new System.EventHandler(this.menuItem81_Click);
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 1;
            this.menuItem5.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem6,
            this.menuItem7,
            this.menuItem8,
            this.menuItem9,
            this.menuItem10,
            this.menuItem33,
            this.menuItem52,
            this.menuItem62});
            this.menuItem5.Text = "Заказ-наряд";
            // 
            // menuItem6
            // 
            this.menuItem6.Index = 0;
            this.menuItem6.Text = "Открыть";
            this.menuItem6.Click += new System.EventHandler(this.menuItem6_Click);
            // 
            // menuItem7
            // 
            this.menuItem7.Index = 1;
            this.menuItem7.Text = "Приостановить";
            this.menuItem7.Click += new System.EventHandler(this.menuItem7_Click);
            // 
            // menuItem8
            // 
            this.menuItem8.Index = 2;
            this.menuItem8.Text = "Возобновить";
            this.menuItem8.Click += new System.EventHandler(this.menuItem8_Click);
            // 
            // menuItem9
            // 
            this.menuItem9.Index = 3;
            this.menuItem9.Text = "Закрыть";
            this.menuItem9.Click += new System.EventHandler(this.menuItem9_Click);
            // 
            // menuItem10
            // 
            this.menuItem10.Index = 4;
            this.menuItem10.Text = "Отменить карточку";
            this.menuItem10.Click += new System.EventHandler(this.menuItem10_Click);
            // 
            // menuItem33
            // 
            this.menuItem33.Index = 5;
            this.menuItem33.Text = "Действия";
            this.menuItem33.Click += new System.EventHandler(this.menuItem33_Click);
            // 
            // menuItem52
            // 
            this.menuItem52.Index = 6;
            this.menuItem52.Text = "Окончание ремонта";
            this.menuItem52.Click += new System.EventHandler(this.menuItem52_Click);
            // 
            // menuItem62
            // 
            this.menuItem62.Index = 7;
            this.menuItem62.Text = "Установка скидки";
            this.menuItem62.Click += new System.EventHandler(this.menuItem62_Click);
            // 
            // menuItem11
            // 
            this.menuItem11.Index = 2;
            this.menuItem11.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem12,
            this.menuItem13});
            this.menuItem11.Text = "Карточка";
            // 
            // menuItem12
            // 
            this.menuItem12.Index = 0;
            this.menuItem12.Text = "Новая";
            this.menuItem12.Click += new System.EventHandler(this.menuItem12_Click);
            // 
            // menuItem13
            // 
            this.menuItem13.Index = 1;
            this.menuItem13.Text = "Исправить";
            this.menuItem13.Click += new System.EventHandler(this.menuItem13_Click);
            // 
            // menuItem14
            // 
            this.menuItem14.Index = 3;
            this.menuItem14.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem15,
            this.menuItem16,
            this.menuItem17,
            this.menuItem18,
            this.menuItem30});
            this.menuItem14.Text = "Печать";
            // 
            // menuItem15
            // 
            this.menuItem15.Index = 0;
            this.menuItem15.Text = "Шапка карточки";
            this.menuItem15.Click += new System.EventHandler(this.menuItem15_Click);
            // 
            // menuItem16
            // 
            this.menuItem16.Index = 1;
            this.menuItem16.Text = "Пропуск на въезд";
            this.menuItem16.Click += new System.EventHandler(this.menuItem16_Click);
            // 
            // menuItem17
            // 
            this.menuItem17.Index = 2;
            this.menuItem17.Text = "Карточка";
            this.menuItem17.Click += new System.EventHandler(this.menuItem17_Click);
            // 
            // menuItem18
            // 
            this.menuItem18.Index = 3;
            this.menuItem18.Text = "Бухгалтерская справка";
            this.menuItem18.Click += new System.EventHandler(this.menuItem18_Click);
            // 
            // menuItem30
            // 
            this.menuItem30.Index = 4;
            this.menuItem30.Text = "Гарантийный талон";
            this.menuItem30.Click += new System.EventHandler(this.menuItem30_Click);
            // 
            // menuItem19
            // 
            this.menuItem19.Index = 4;
            this.menuItem19.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem20,
            this.menuItem21,
            this.menuItem22,
            this.menuItem23});
            this.menuItem19.Text = "История действий";
            // 
            // menuItem20
            // 
            this.menuItem20.Index = 0;
            this.menuItem20.Text = "Показать";
            this.menuItem20.Click += new System.EventHandler(this.menuItem20_Click);
            // 
            // menuItem21
            // 
            this.menuItem21.Index = 1;
            this.menuItem21.Text = "Отменить закрытие";
            this.menuItem21.Click += new System.EventHandler(this.menuItem21_Click);
            // 
            // menuItem22
            // 
            this.menuItem22.Index = 2;
            this.menuItem22.Text = "Восстановить закрытие";
            this.menuItem22.Click += new System.EventHandler(this.menuItem22_Click);
            // 
            // menuItem23
            // 
            this.menuItem23.Index = 3;
            this.menuItem23.Text = "Удалить закрытие";
            this.menuItem23.Click += new System.EventHandler(this.menuItem23_Click);
            // 
            // menuItem24
            // 
            this.menuItem24.Index = 5;
            this.menuItem24.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem25,
            this.menuItem26,
            this.menuItem29,
            this.menuItem53});
            this.menuItem24.Text = "Служебные";
            // 
            // menuItem25
            // 
            this.menuItem25.Index = 0;
            this.menuItem25.Text = "Удалить автомобиль в карточке";
            this.menuItem25.Click += new System.EventHandler(this.menuItem25_Click);
            // 
            // menuItem26
            // 
            this.menuItem26.Index = 1;
            this.menuItem26.Text = "Заменить владельца в карточке";
            this.menuItem26.Click += new System.EventHandler(this.menuItem26_Click);
            // 
            // menuItem29
            // 
            this.menuItem29.Index = 2;
            this.menuItem29.Text = "Заменить автомобиль в карточке";
            this.menuItem29.Click += new System.EventHandler(this.menuItem29_Click);
            // 
            // menuItem53
            // 
            this.menuItem53.Index = 3;
            this.menuItem53.Text = "Управление карточкой";
            this.menuItem53.Click += new System.EventHandler(this.menuItem53_Click);
            // 
            // menuItem31
            // 
            this.menuItem31.Index = 6;
            this.menuItem31.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem32,
            this.menuItem61,
            this.menuItem80});
            this.menuItem31.Text = "Отчеты";
            // 
            // menuItem32
            // 
            this.menuItem32.Index = 0;
            this.menuItem32.Text = "Детали (период)";
            this.menuItem32.Click += new System.EventHandler(this.menuItem32_Click);
            // 
            // menuItem61
            // 
            this.menuItem61.Index = 1;
            this.menuItem61.Text = "Возвраты (период)";
            this.menuItem61.Click += new System.EventHandler(this.menuItem61_Click);
            // 
            // menuItem80
            // 
            this.menuItem80.Index = 2;
            this.menuItem80.Text = "Выгрузка БУХГАЛТЕРИЯ";
            this.menuItem80.Click += new System.EventHandler(this.menuItem80_Click);
            // 
            // menuItem27
            // 
            this.menuItem27.Index = 7;
            this.menuItem27.Text = "-";
            // 
            // menuItem28
            // 
            this.menuItem28.Index = 8;
            this.menuItem28.Text = "Владелец";
            this.menuItem28.Click += new System.EventHandler(this.menuItem28_Click);
            // 
            // menuItem34
            // 
            this.menuItem34.Index = 9;
            this.menuItem34.Text = "-";
            // 
            // menuItem35
            // 
            this.menuItem35.Index = 10;
            this.menuItem35.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem36,
            this.menuItem37,
            this.menuItem38,
            this.menuItem39,
            this.menuItem40,
            this.menuItem41,
            this.menuItem42,
            this.menuItem47,
            this.menuItem48,
            this.menuItem49,
            this.menuItem54,
            this.menuItem55,
            this.menuItem63,
            this.menuItem64,
            this.menuItem67,
            this.menuItem68,
            this.menuItem82});
            this.menuItem35.Text = "Новый документооборот";
            // 
            // menuItem36
            // 
            this.menuItem36.Index = 0;
            this.menuItem36.Text = "Заявка клиента";
            this.menuItem36.Click += new System.EventHandler(this.menuItem36_Click);
            // 
            // menuItem37
            // 
            this.menuItem37.Index = 1;
            this.menuItem37.Text = "Акт приема в ремонт";
            this.menuItem37.Click += new System.EventHandler(this.menuItem37_Click);
            // 
            // menuItem38
            // 
            this.menuItem38.Index = 2;
            this.menuItem38.Text = "Заказ-Наряд (Предварительный)";
            this.menuItem38.Click += new System.EventHandler(this.menuItem38_Click);
            // 
            // menuItem39
            // 
            this.menuItem39.Index = 3;
            this.menuItem39.Text = "Операционная карта";
            this.menuItem39.Click += new System.EventHandler(this.menuItem39_Click);
            // 
            // menuItem40
            // 
            this.menuItem40.Index = 4;
            this.menuItem40.Text = "Заказ-Наряд";
            this.menuItem40.Click += new System.EventHandler(this.menuItem40_Click);
            // 
            // menuItem41
            // 
            this.menuItem41.Index = 5;
            this.menuItem41.Text = "Акт выдачи автомобиля";
            this.menuItem41.Click += new System.EventHandler(this.menuItem41_Click);
            // 
            // menuItem42
            // 
            this.menuItem42.Index = 6;
            this.menuItem42.Text = "Печать заголовка";
            this.menuItem42.Click += new System.EventHandler(this.menuItem42_Click);
            // 
            // menuItem47
            // 
            this.menuItem47.Index = 7;
            this.menuItem47.Text = "Опросный лист ПССС";
            this.menuItem47.Click += new System.EventHandler(this.menuItem47_Click);
            // 
            // menuItem48
            // 
            this.menuItem48.Index = 8;
            this.menuItem48.Text = "Акт осмотра ходовой части";
            this.menuItem48.Click += new System.EventHandler(this.menuItem48_Click);
            // 
            // menuItem49
            // 
            this.menuItem49.Index = 9;
            this.menuItem49.Text = "Акт приема в ремонт (Пустой)";
            this.menuItem49.Click += new System.EventHandler(this.menuItem49_Click);
            // 
            // menuItem54
            // 
            this.menuItem54.Index = 10;
            this.menuItem54.Text = "-";
            // 
            // menuItem55
            // 
            this.menuItem55.Index = 11;
            this.menuItem55.Text = "Акт осмотра 2009";
            this.menuItem55.Click += new System.EventHandler(this.menuItem55_Click);
            // 
            // menuItem63
            // 
            this.menuItem63.Index = 12;
            this.menuItem63.Text = "Лист диагностики двигателя";
            this.menuItem63.Click += new System.EventHandler(this.menuItem63_Click);
            // 
            // menuItem64
            // 
            this.menuItem64.Index = 13;
            this.menuItem64.Text = "Заказ-Наряд (Без Округления)";
            this.menuItem64.Click += new System.EventHandler(this.menuItem64_Click);
            // 
            // menuItem67
            // 
            this.menuItem67.Index = 14;
            this.menuItem67.Text = "Акт приема новый";
            this.menuItem67.Click += new System.EventHandler(this.menuItem67_Click);
            // 
            // menuItem68
            // 
            this.menuItem68.Index = 15;
            this.menuItem68.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem69});
            this.menuItem68.Text = "Гарантия";
            // 
            // menuItem69
            // 
            this.menuItem69.Index = 0;
            this.menuItem69.Text = "Гарантияный ЗН";
            this.menuItem69.Click += new System.EventHandler(this.menuItem69_Click);
            // 
            // menuItem43
            // 
            this.menuItem43.Index = 11;
            this.menuItem43.Text = "-";
            // 
            // menuItem44
            // 
            this.menuItem44.Index = 12;
            this.menuItem44.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem45,
            this.menuItem46});
            this.menuItem44.Text = "Печать-Гарантия";
            // 
            // menuItem45
            // 
            this.menuItem45.Index = 0;
            this.menuItem45.Text = "Гарантия Джи-Эм АВТОВАЗ";
            this.menuItem45.Click += new System.EventHandler(this.menuItem45_Click);
            // 
            // menuItem46
            // 
            this.menuItem46.Index = 1;
            this.menuItem46.Text = "Гарантия KIA";
            this.menuItem46.Click += new System.EventHandler(this.menuItem46_Click);
            // 
            // menuItem50
            // 
            this.menuItem50.Index = 13;
            this.menuItem50.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem51});
            this.menuItem50.Text = "Время";
            // 
            // menuItem51
            // 
            this.menuItem51.Index = 0;
            this.menuItem51.Text = "Установка предполагаемого времени окончания ремонта";
            this.menuItem51.Click += new System.EventHandler(this.menuItem51_Click);
            // 
            // menuItem56
            // 
            this.menuItem56.Index = 14;
            this.menuItem56.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem57});
            this.menuItem56.Text = "Безналичный расчет";
            // 
            // menuItem57
            // 
            this.menuItem57.Index = 0;
            this.menuItem57.Text = "Выставить счет";
            this.menuItem57.Click += new System.EventHandler(this.menuItem57_Click);
            // 
            // menuItem59
            // 
            this.menuItem59.Index = 15;
            this.menuItem59.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem60});
            this.menuItem59.Text = "Установки";
            // 
            // menuItem60
            // 
            this.menuItem60.Index = 0;
            this.menuItem60.Text = "Сервис-Консультант";
            this.menuItem60.Click += new System.EventHandler(this.menuItem60_Click);
            // 
            // menuItem65
            // 
            this.menuItem65.Index = 16;
            this.menuItem65.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem66,
            this.menuItem70,
            this.menuItem71});
            this.menuItem65.Text = "EXCEL";
            // 
            // menuItem66
            // 
            this.menuItem66.Index = 0;
            this.menuItem66.Text = "Для СТРАХОВОЙ РЕСО";
            this.menuItem66.Click += new System.EventHandler(this.menuItem66_Click);
            // 
            // menuItem70
            // 
            this.menuItem70.Index = 1;
            this.menuItem70.Text = "ЗН НОВОЙ ФОРМЫ";
            this.menuItem70.Click += new System.EventHandler(this.menuItem70_Click);
            // 
            // menuItem71
            // 
            this.menuItem71.Index = 2;
            this.menuItem71.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem72,
            this.menuItem73,
            this.menuItem74,
            this.menuItem75,
            this.menuItem76,
            this.menuItem77});
            this.menuItem71.Text = "НОВЫЕ ФОРМЫ";
            // 
            // menuItem72
            // 
            this.menuItem72.Index = 0;
            this.menuItem72.Text = "Заявка-Договор-ЗН";
            this.menuItem72.Click += new System.EventHandler(this.menuItem72_Click);
            // 
            // menuItem73
            // 
            this.menuItem73.Index = 1;
            this.menuItem73.Text = "Наряд на работы";
            this.menuItem73.Click += new System.EventHandler(this.menuItem73_Click);
            // 
            // menuItem74
            // 
            this.menuItem74.Index = 2;
            this.menuItem74.Text = "Лист Контроля";
            this.menuItem74.Click += new System.EventHandler(this.menuItem74_Click);
            // 
            // menuItem75
            // 
            this.menuItem75.Index = 3;
            this.menuItem75.Text = "Акт приема-передачи";
            this.menuItem75.Click += new System.EventHandler(this.menuItem75_Click);
            // 
            // menuItem76
            // 
            this.menuItem76.Index = 4;
            this.menuItem76.Text = "Договор-ЗаказНаряд";
            this.menuItem76.Click += new System.EventHandler(this.menuItem76_Click);
            // 
            // menuItem77
            // 
            this.menuItem77.Index = 5;
            this.menuItem77.Text = "Общий";
            this.menuItem77.Click += new System.EventHandler(this.menuItem77_Click);
            // 
            // menuItem78
            // 
            this.menuItem78.Index = 17;
            this.menuItem78.Text = "НОВАЯ ФОРМА УПРАВЛЕНИЯ";
            this.menuItem78.Click += new System.EventHandler(this.menuItem78_Click);
            // 
            // menuItem79
            // 
            this.menuItem79.Index = 18;
            this.menuItem79.Text = "НОВЫЙ ЗН - НОВАЯ ФОРМА";
            this.menuItem79.Click += new System.EventHandler(this.menuItem79_Click);
            // 
            // button_closed
            // 
            this.button_closed.Image = ((System.Drawing.Image)(resources.GetObject("button_closed.Image")));
            this.button_closed.Location = new System.Drawing.Point(43, 114);
            this.button_closed.Name = "button_closed";
            this.button_closed.Size = new System.Drawing.Size(32, 27);
            this.button_closed.TabIndex = 6;
            this.toolTip1.SetToolTip(this.button_closed, "Показать закрытые еа заданную дату");
            this.button_closed.Click += new System.EventHandler(this.button_closed_Click);
            // 
            // button_detail_get
            // 
            this.button_detail_get.Image = ((System.Drawing.Image)(resources.GetObject("button_detail_get.Image")));
            this.button_detail_get.Location = new System.Drawing.Point(747, 114);
            this.button_detail_get.Name = "button_detail_get";
            this.button_detail_get.Size = new System.Drawing.Size(32, 27);
            this.button_detail_get.TabIndex = 8;
            this.toolTip1.SetToolTip(this.button_detail_get, "Полусить детали со склада по выбранному заказ-наряду");
            this.button_detail_get.Click += new System.EventHandler(this.button_detail_get_Click);
            // 
            // button_detail_return
            // 
            this.button_detail_return.Image = ((System.Drawing.Image)(resources.GetObject("button_detail_return.Image")));
            this.button_detail_return.Location = new System.Drawing.Point(779, 114);
            this.button_detail_return.Name = "button_detail_return";
            this.button_detail_return.Size = new System.Drawing.Size(32, 27);
            this.button_detail_return.TabIndex = 9;
            this.toolTip1.SetToolTip(this.button_detail_return, "Вернуть детали на склад по выбранному заказ-няряду");
            this.button_detail_return.Click += new System.EventHandler(this.button_detail_return_Click);
            // 
            // textBox_workshop
            // 
            this.textBox_workshop.Location = new System.Drawing.Point(331, 48);
            this.textBox_workshop.Name = "textBox_workshop";
            this.textBox_workshop.Size = new System.Drawing.Size(330, 26);
            this.textBox_workshop.TabIndex = 7;
            this.textBox_workshop.DoubleClick += new System.EventHandler(this.textBox_workshop_DoubleClick);
            // 
            // menuItem82
            // 
            this.menuItem82.Index = 16;
            this.menuItem82.Text = "Акт заводская форма";
            this.menuItem82.Click += new System.EventHandler(this.menuItem82_Click);
            // 
            // FormManageCard
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(8, 19);
            this.ClientSize = new System.Drawing.Size(960, 341);
            this.Controls.Add(this.button_detail_return);
            this.Controls.Add(this.button_detail_get);
            this.Controls.Add(this.textBox_workshop);
            this.Controls.Add(this.button_closed);
            this.Controls.Add(this.checkBox_showcancel);
            this.Controls.Add(this.button_refresh);
            this.Controls.Add(this.checkBox_nodate);
            this.Controls.Add(this.dateTimePicker_end);
            this.Controls.Add(this.dateTimePicker_start);
            this.Controls.Add(this.listView1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "FormManageCard";
            this.Text = "FormManageCard";
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		#region Обработчики событий
		private void button_refresh_Click(object sender, System.EventArgs e)
		{
			FillList();
		}
		private void listView1_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
		{
			// Запрос маски для поиска по различным параметрам

			FormSelectString dialog;
			switch(e.Column)
			{
				case 0:
					// Поиск карточки по номеру заказ-наряда
					dialog = new FormSelectString("Поиска по номеру карточки", "");
					if(dialog.ShowDialog() != DialogResult.OK) return;
					long number = dialog.SelectedLong;
					if (number == 0) return;
					listView1.Items.Clear();
					DbSqlCard.SelectNumber(listView1, number);
					return;
				case 2:
					// Поиск карточки по номеру заказ-наряда
					dialog = new FormSelectString("Поиска по номеру заказ-наряда", "");
					if(dialog.ShowDialog() != DialogResult.OK) return;
					long warrant_number = dialog.SelectedLong;
					if (warrant_number == 0) return;
					listView1.Items.Clear();
					DbSqlCard.SelectWarrantNumber(listView1, warrant_number);
					return;
				case 3:
					dialog = new FormSelectString("Поиска по владельцу", "");
					if(dialog.ShowDialog() != DialogResult.OK) return;
					search_card.SetOwnerMask(dialog.SelectedTextMask);
					break;
				case 5:
					dialog = new FormSelectString("Поиска по VINу", "");
					if(dialog.ShowDialog() != DialogResult.OK) return;
					search_card.SetVinMask(dialog.SelectedTextMask);
					break;
				case 6:
					dialog = new FormSelectString("Поиска по регистрационному знаку", "");
					if(dialog.ShowDialog() != DialogResult.OK) return;
					search_card.SetSignMask(dialog.SelectedTextMask);
					break;
				default:
					return;
			}
			FillList();
		}
		private void listView1_DoubleClick(object sender, System.EventArgs e)
		{
			// Определяем, где щелкнули мышкой
			ListViewItem item = null;
			DbCard card = null;
			DtCard card1 = null;
			long card_code = 0;
			
			item = Db.GetItemPosition(listView1);
			if(item == null) return;
			card_code = (long)item.Tag;
			if(card_code == 0) return;
			// Загружаем карточку
			card1 = DbSqlCard.Find(card_code);
			if(card1 == null) return;
			card = DbCard.Find((long)card1.GetData("НОМЕР_КАРТОЧКА"), (int)card1.GetData("ГОД_КАРТОЧКА"));
			if(card == null) return;
			if(click_type == Db.ClickType.Properties)
			{
				FormCard dialog = new FormCard(card);
				dialog.SetItemLink(item);
				dialog.Show();
				return;
			}
			if(click_type == Db.ClickType.Select)
			{
				card_selected = card1;
				this.DialogResult = DialogResult.OK;
				this.Close();
				return;
			}

			//if(dialog.DialogResult != DialogResult.OK) return;
			//dialog.Card.SetLVItem(item);
		}
		#endregion

		protected void SetSearch()
		{
			// Установка параметров поиска
			search_card.SetDates(dateTimePicker_start.Value, dateTimePicker_end.Value);
			search_card.SetNoDate(checkBox_nodate.Checked);
			search_card.SetShowCancel(checkBox_showcancel.Checked);
			search_card.SetWorkshop(selected_workshop_code);
		}

		protected void FillList()
		{
			// Обновить список заказ-нарядов
			// Установка параметров поиска
			SetSearch();
			// Очистка предыдущих данных
			listView1.Items.Clear();
			// Подготовка команды к поиску
			DbSqlCard.PrepareSelectCard(search_card);
			// Заполнение листа
			this.Cursor = Cursors.WaitCursor;
			if(search_card.Workshop > 0)
				DbSql.FillList(listView1, DbSqlCard.select_card_workshop, new DbSql.DelegateMakeLVItem(DbSqlCard.MakeLV_List));
			else
				DbSql.FillList(listView1, DbSqlCard.select_card, new DbSql.DelegateMakeLVItem(DbSqlCard.MakeLV_List));
			search_card.ClearTemp();
			this.Cursor = Cursors.Default;
		}
		protected void FillList(int code, object obj)
		{
			// Обновить список заказ-нарядов
			listView1.Items.Clear();
			// Подготовка команды к поиску

			switch(code)
			{
				case 1:
					DbSqlCard.SelectAuto(listView1, (long)obj);
					break;
				case 2:
					DbSqlCard.SelectPartner(listView1, (long)obj);
					break;
				case 3:
					DbSqlCard.SelectCardDetail(listView1, (long)obj);
					break;
				default:
					break;
			}
		}

		private void listView1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if(e.Button == MouseButtons.Right)
			{
				// На отпускание правой кнопки мышки - всплывающее меню
				// Настройка меню
				// Права пользователя
				// Отключаем все запрещенное
				contextMenu1.MenuItems[0].Enabled = false;
				contextMenu1.MenuItems[1].Enabled = true;
				menuItem21.Enabled = false;
				menuItem22.Enabled = false;
				menuItem23.Enabled = false;
				menuItem24.Enabled = false;
				menuItem31.Enabled = false;
				// Включаем по разрешению
				string login = Form1.currentLogin.ToLower();
				if (login == "заякинм" || login == "админ" || login == "панкратьева")
				{
					contextMenu1.MenuItems[0].Enabled = true;
				}
				if (login == "админ")
				{
					menuItem21.Enabled = true;
					menuItem22.Enabled = true;
					menuItem23.Enabled = true;
					menuItem24.Enabled = true;
					menuItem31.Enabled = true;
				}
				if (login == "заякинм")
				{
					menuItem31.Enabled = true;
					// На время отпуска
					//menuItem21.Enabled = true;
					//menuItem22.Enabled = true;
					//menuItem23.Enabled = true;
				}
				// Настройка меню исходя из свойств выбранной карточки
				// Показ меню
				contextMenu1.Show(listView1, new Point(e.X, e.Y));
			}
		}

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			if(listView1.SelectedItems == null || listView1.SelectedItems[0] == null) return;
			long code = (long)listView1.SelectedItems[0].Tag;
			if(code == 0) return;
			// Проверяем выбранную карточку
			FormCardControl dialog = new FormCardControl(code);
			dialog.Show();
		}

		private void menuItem3_Click(object sender, System.EventArgs e)
		{
			// Одобряем
			ListViewItem item;
			long code;
			if(listView1.SelectedItems == null) return;
			item = listView1.SelectedItems[0];
			if(item == null) return;
			code = (long)item.Tag;
			if(code == 0) return;
			if(DbSqlCard.SetStatusControl(code, 1) != true) return;
			item.StateImageIndex = 1;
		}

		private void menuItem4_Click(object sender, System.EventArgs e)
		{
			// Отклоняем
			ListViewItem item;
			long code;
			if(listView1.SelectedItems == null) return;
			item = listView1.SelectedItems[0];
			if(item == null) return;
			code = (long)item.Tag;
			if(code == 0) return;
			if(DbSqlCard.SetStatusControl(code, 2) != true) return;
			item.StateImageIndex = 2;
		}

		private void button_closed_Click(object sender, System.EventArgs e)
		{
			// Показать карточки закрытые на заданную дату
			FormSelectDate dialog = new FormSelectDate();
			if(dialog.ShowDialog() != DialogResult.OK) return;
			DateTime date = dialog.SelectedDate;
			listView1.Items.Clear();
			DbSqlCard.SelectCardClosed(listView1, date);
		}

		private void menuItem7_Click(object sender, System.EventArgs e)
		{
			// Приостановить заказ-наряд
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			long card_code = (long)item.Tag;
			if(card_code == 0) return;
			// Загружаем карточку
			DtCard card1 = DbSqlCard.Find(card_code);
			if(card1 == null) return;
			DbCard card = DbCard.Find(card1.Number, card1.Year);
			if(card == null) return;
			card.Action(DbCardAction.ActionCodes.Stop);

			// Зачитаем карточку
			card1 = DbSqlCard.FindList(card);
			card1.SetLVItem(item);
		}

		private void menuItem6_Click(object sender, System.EventArgs e)
		{
			// Открыть заказ-наряд
			// Пытаемся открыть выбранный заказ наряд
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			long card_code = (long)item.Tag;
			if(card_code == 0) return;
			// Загружаем карточку
			DtCard card1 = DbSqlCard.Find(card_code);
			if(card1 == null) return;
			DbCard card = DbCard.Find((long)card1.GetData("НОМЕР_КАРТОЧКА"), (int)card1.GetData("ГОД_КАРТОЧКА"));
			if(card == null) return;

			// Установка номера автомобиля для заказ-наряда
			if((long)card1.GetData("АВТОМОБИЛЬ_КАРТОЧКА") != 0)
			{
				DtAuto auto = DbSqlAuto.Find((long)card1.GetData("АВТОМОБИЛЬ_КАРТОЧКА"));
				DbSqlCard.SetLicencePlate(card, (string)auto.GetData("НОМЕР_ЗНАК_НОМЕР"), (string)auto.GetData("НОМЕР_ЗНАК_РЕГИОН"));
			}

			// Подтверждение открытия заказ наряда без установленной даты продажи

			card.Action(DbCardAction.ActionCodes.Open);

			// Зачитаем карточку
			card1 = DbSqlCard.FindList(card);
			card1.SetLVItem(item);
		}

		private void menuItem8_Click(object sender, System.EventArgs e)
		{
			// Возобновить заказ-наряд
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			long card_code = (long)item.Tag;
			if(card_code == 0) return;
			// Загружаем карточку
			DtCard card1 = DbSqlCard.Find(card_code);
			if(card1 == null) return;
			DbCard card = DbCard.Find((long)card1.GetData("НОМЕР_КАРТОЧКА"), (int)card1.GetData("ГОД_КАРТОЧКА"));
			if(card == null) return;
			card.Action(DbCardAction.ActionCodes.Start);

			// Зачитаем карточку
			card1 = DbSqlCard.FindList(card);
			card1.SetLVItem(item);
		}

		private void menuItem9_Click(object sender, System.EventArgs e)
		{
			// Закрыть заказ-наряд
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			long card_code = (long)item.Tag;
			if(card_code == 0) return;
			// Загружаем карточку
			DtCard card1 = DbSqlCard.Find(card_code);
			if(card1 == null) return;
			DbCard card = DbCard.Find(card1.Number, card1.Year);
			if(card == null) return;

			// Проверка корректности закрытия
			// Дополнительные проверки на разрешения
			if(CheckAutoPermision(card.Auto.Code) == false)
			{
				MessageBox.Show("У Вас нет прав закрытия заказ-наряда на этот автомобиль");
				return;
			}
			// Запрет закрытия карточки с работами и без заявки
			if(card.CodeWorkshop == 1)
			{
				ArrayList array_claim = new ArrayList();
				ArrayList array_work = new ArrayList();
				DbSqlCardClaim.SelectInArray(array_claim, card.Number, card.Year);
				DbSqlCardWork.SelectInArray(card1, array_work);
				if(array_work.Count > 0 && array_claim.Count == 0)
				{
					MessageBox.Show("Попытка закрыть заказ-наряд с работами без заполненной заявки");
				}
			}


			// Запрос об установке скидки
			if(card.CodeWorkshop == 1)
			{
				// Скидка только для сервиса
				if(card.Partner.Juridical == true)
				{
					// Для юридических лиц скидка по списку
					// Загружаем свойтсва контрагента
					DtPartnerProperty property = DbSqlPartnerProperty.Find(card.Partner.Code);
					if(property != null)
					{
						if(property.Discount > 0)
						{
							// Запрос на предоставление скидки
							if(MessageBox.Show("Предоставить корпоративную скидку " + property.Discount.ToString() + "%", "Запрос",  MessageBoxButtons.YesNo) == DialogResult.Yes)
							{
								// Предоставляем скидку корпоративную
								if(DbSqlCard.SetDiscount(card, property.Discount, 0) == true)
								{
									card.DiscountWork = (float)property.Discount;
								}
							}
						}
					}
				}
				else
				{
					// Ручная установка скидки!!!!!!
					FormManageDiscount dialogdiscount = new FormManageDiscount(card1);
					DialogResult resdiscount = dialogdiscount.ShowDialog();
					if (resdiscount == DialogResult.OK)
					{
						float discount = dialogdiscount.discountpercent;
						if(DbSqlCard.SetDiscount(card, discount, 0)== true)
						{
							card.DiscountWork = discount;
						}
					}

					// Запрос кода карточки
	//				if(MessageBox.Show("Есть ли дисконтная карта?", "Запрос",  MessageBoxButtons.YesNo) == DialogResult.Yes)
	//				{
	//					FormSelectString dialog = new FormSelectString("Введите код карточки", "");
	//					if(dialog.ShowDialog() == DialogResult.OK)
	//					{
	//						// Поиск дисконтной карточки
	//						DtDiscount discount = DbSqlDiscount.Find(dialog.SelectedLong);
	//						if(discount == null) return;
	//						// Предоставляем скидку дисконтную
	//						if(DbSqlCard.SetDiscount(card, (float)discount.GetData("СКИДКА_СЕРВИС_РАБОТА_ДИСКОНТ"), (long)discount.GetData("КОД_ДИСКОНТ"))== true)
	//						{
	//							card.DiscountWork = (float)discount.GetData("СКИДКА_СЕРВИС_РАБОТА_ДИСКОНТ");
	//						}
	//					}
	//				}
				}
			}

			//Запрашиваем мастера-контролера подписавшего закрытие заказ-наряда
			// MessageBox.Show("Выберете мастера-контролера подписавшего закрытие заказ-наряда");

			// ###################################### Старый вариант
			// FormStaffList staff = new FormStaffList();
			// if(staff.ShowDialog() != DialogResult.OK) return;
			// if(DbSqlCard.SetMaster(card, staff.SelectedStaff) != true) return;
			// ###################################### Конец старый вариант

			// ######################### Вариант с выбором по новой схеме
			// FormListStaff staff = new FormListStaff(2, 0);
			// if(staff.ShowDialog() != DialogResult.OK) return;
			// DtStaff master = staff.SelectedStaff;
			// if(DbSqlCard.SetMaster(card, master) != true) return;
			// ######################### Конец Вариант с выбором по новой схеме

			FormSelectString staff = new FormSelectString("Электронная подпись мастера закрывающего заказ-наряд", "", true);
			if(staff.ShowDialog() != DialogResult.OK) return;
			if(staff.SelectedLong == 0) return;
			DtStaff master = DbSqlStaff.FindSign(staff.SelectedLong);
			if(master == null) return;
			if(DbSqlCard.SetMaster(card, master) != true) return;
			
			

			card.Action(DbCardAction.ActionCodes.Close);
			// Зачитаем карточку
			card1 = DbSqlCard.FindList(card);
			card1.SetLVItem(item);
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

		private void menuItem10_Click(object sender, System.EventArgs e)
		{
			// Отменить карточку
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			long card_code = (long)item.Tag;
			if(card_code == 0) return;
			// Загружаем карточку
			DtCard card1 = DbSqlCard.Find(card_code);
			if(card1 == null) return;
			DbCard card = DbCard.Find((long)card1.GetData("НОМЕР_КАРТОЧКА"), (int)card1.GetData("ГОД_КАРТОЧКА"));
			if(card == null) return;
			card.Action(DbCardAction.ActionCodes.Cancel);

			// Зачитаем карточку
			card1 = DbSqlCard.FindList(card);
			card1.SetLVItem(item);
		}

		private void menuItem12_Click(object sender, System.EventArgs e)
		{
			// Новая карточка
			// Вызов диалога заполнения и добавления нового элемента
			FormCard dialog = new FormCard(null);
			dialog.SetListLink(listView1);
			dialog.Show();
			if(dialog.DialogResult != DialogResult.OK) return;
			

			// Зачитаем карточку
			//ListViewItem item = listView1.Items.Add("");
			//DtCard card1 = DbSqlCard.FindList(dialog.Card);
			//card1.SetLVItem(item);
		}

		private void menuItem13_Click(object sender, System.EventArgs e)
		{
			// Исправить карточку
			// Определяем, где щелкнули мышкой
			ListViewItem item = null;
			DbCard card = null;
			DtCard card1 = null;
			long card_code = 0;
			
			item = Db.GetItemSelected(listView1);
			if(item == null) return;
			card_code = (long)item.Tag;
			if(card_code == 0) return;
			// Загружаем карточку
			card1 = DbSqlCard.Find(card_code);
			if(card1 == null) return;
			card = DbCard.Find(card1.Number, card1.Year);
			if(card == null) return;
			FormCard dialog = new FormCard(card);
			dialog.Show();
			//if(dialog.DialogResult != DialogResult.OK) return;
			//dialog.Card.SetLVItem(item);
		}

		private void menuItem15_Click(object sender, System.EventArgs e)
		{
			// Печать пропуска на въезд
			// Определяем, где щелкнули мышкой
			ListViewItem item = null;
			DbCard card = null;
			DtCard card1 = null;
			long card_code = 0;
			
			item = Db.GetItemSelected(listView1);
			if(item == null) return;
			card_code = (long)item.Tag;
			if(card_code == 0) return;
			// Загружаем карточку
			card1 = DbSqlCard.Find(card_code);
			if(card1 == null) return;
			card = DbCard.Find((long)card1.GetData("НОМЕР_КАРТОЧКА"), (int)card1.GetData("ГОД_КАРТОЧКА"));

			DbCardPrint cardPrint = new DbCardPrint(card);
			cardPrint.PrintRequest();
		}

		private void menuItem16_Click(object sender, System.EventArgs e)
		{
			// Печать пропуска на въезд
			// Определяем, где щелкнули мышкой
			ListViewItem item = null;
			DbCard card = null;
			DtCard card1 = null;
			long card_code = 0;
			
			item = Db.GetItemSelected(listView1);
			if(item == null) return;
			card_code = (long)item.Tag;
			if(card_code == 0) return;
			// Загружаем карточку
			card1 = DbSqlCard.Find(card_code);
			if(card1 == null) return;
			card = DbCard.Find((long)card1.GetData("НОМЕР_КАРТОЧКА"), (int)card1.GetData("ГОД_КАРТОЧКА"));

			DbCardPrint cardPrint = new DbCardPrint(card);
			cardPrint.PrintDocument();
		}

		private void menuItem17_Click(object sender, System.EventArgs e)
		{
			// Печать пропуска на въезд
			// Определяем, где щелкнули мышкой
			ListViewItem item = null;
			DbCard card = null;
			DtCard card1 = null;
			long card_code = 0;
			
			item = Db.GetItemSelected(listView1);
			if(item == null) return;
			card_code = (long)item.Tag;
			if(card_code == 0) return;
			// Загружаем карточку
			card1 = DbSqlCard.Find(card_code);
			if(card1 == null) return;
			card = DbCard.Find((long)card1.GetData("НОМЕР_КАРТОЧКА"), (int)card1.GetData("ГОД_КАРТОЧКА"));

			DbCardPrint cardPrint = new DbCardPrint(card);
			cardPrint.Print();

			// При необходимости печатаем лист регламентных работ
			DbPrintReglament prn = new DbPrintReglament(card);
			if(prn.IsCollection && card.IsWarrantOpened && !card.IsWarrantClosed)
			{
				// Запрос необходимости печати
				if(MessageBox.Show("Печатать регламентные работы?", "Запрос", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
				prn.Print();
			}
		}

		private void menuItem18_Click(object sender, System.EventArgs e)
		{
			// Печать пропуска на въезд
			// Определяем, где щелкнули мышкой
			ListViewItem item = null;
			DbCard card = null;
			DtCard card1 = null;
			long card_code = 0;
			
			item = Db.GetItemSelected(listView1);
			if(item == null) return;
			card_code = (long)item.Tag;
			if(card_code == 0) return;
			// Загружаем карточку
			card1 = DbSqlCard.Find(card_code);
			if(card1 == null) return;
			card = DbCard.Find((long)card1.GetData("НОМЕР_КАРТОЧКА"), (int)card1.GetData("ГОД_КАРТОЧКА"));

			// Проверяем статус карточки
            // Разрешить печатать справку неоткрытого з/н
			if(card.IsWarrantOpened == false)
			{
                // Разрешить печатать справку неоткрытого з/н
				//MessageBox.Show(this, "Нельзя печатать справку для неоткрытого заказ-наряда");
				//return;
			}
			if(card.IsWarrantClosed == true)
			{
				string login = Form1.currentLogin.ToLower();
				if (login != "заякинм" && login != "админ")
				{
					MessageBox.Show(this, "Вам запрещено печатать бухгалтерскую справку закрытого заказ-наряда");
					return;
				}
			}
			DbCardPrint cardPrint = new DbCardPrint(card);
			cardPrint.PrintBuhg();
		}

		private void menuItem20_Click(object sender, System.EventArgs e)
		{
			// Показать историю действий произведенных с данной карточкой
			ListViewItem item = null;
			DtCard card = null;
			long card_code = 0;
			
			item = Db.GetItemSelected(listView1);
			if(item == null) return;
			card_code = (long)item.Tag;
			if(card_code == 0) return;
			// Загружаем карточку
			card = DbSqlCard.Find(card_code);
			if(card == null) return;

			FormListCardAction dialog = new FormListCardAction(card);
			dialog.ShowDialog();
		}

		private void menuItem21_Click(object sender, System.EventArgs e)
		{
			// Отменяем закрытие карточки
			// Показать историю действий произведенных с данной карточкой
			ListViewItem item = null;
			DtCard card = null;
			long card_code = 0;
			
			item = Db.GetItemSelected(listView1);
			if(item == null) return;
			card_code = (long)item.Tag;
			if(card_code == 0) return;
			// Загружаем карточку
			card = DbSqlCard.Find(card_code);
			if(card == null) return;

			// Запрос обязательного примечания
			string comment;
			FormSelectString dialog = new FormSelectString("Обязательное примечание", "");
			if(dialog.ShowDialog() != DialogResult.OK) return;
			comment = dialog.SelectedText;

			if(DbSqlCardAction.CancelClose(card, comment) == false) return;

			// Перезачитываем карточку
			card = DbSqlCard.FindList(card);
			card.SetLVItem(item);
		}

		private void menuItem22_Click(object sender, System.EventArgs e)
		{
			// Восстанавливаем закрытие карточки
			// Показать историю действий произведенных с данной карточкой
			ListViewItem item = null;
			DtCard card = null;
			long card_code = 0;
			
			item = Db.GetItemSelected(listView1);
			if(item == null) return;
			card_code = (long)item.Tag;
			if(card_code == 0) return;
			// Загружаем карточку
			card = DbSqlCard.Find(card_code);
			if(card == null) return;

			// Запрос обязательного примечания
			string comment;
			FormSelectString dialog = new FormSelectString("Обязательное примечание", "");
			if(dialog.ShowDialog() != DialogResult.OK) return;
			comment = dialog.SelectedText;

			if(DbSqlCardAction.ReClose(card, comment) == false) return;

			// Перезачитываем карточку
			card = DbSqlCard.FindList(card);
			card.SetLVItem(item);
		}

		private void menuItem23_Click(object sender, System.EventArgs e)
		{
			// Удаляем закрытие выбранной карточки
			ListViewItem item = null;
			DtCard card = null;
			long card_code = 0;
			
			item = Db.GetItemSelected(listView1);
			if(item == null) return;
			card_code = (long)item.Tag;
			if(card_code == 0) return;
			// Загружаем карточку
			card = DbSqlCard.Find(card_code);
			if(card == null) return;

			// Запрашиваем подтверждение
			if(MessageBox.Show("Удалить закрытие заказ-наряда?", "Запрос", MessageBoxButtons.YesNo) != DialogResult.Yes) return;


			if(DbSqlCardAction.DeleteClose(card) == false) return;
			// Перезачитываем карточку
			card = DbSqlCard.FindList(card);
			card.SetLVItem(item);
		}

		private void menuItem25_Click(object sender, System.EventArgs e)
		{
			// Удаляем владельца из выбранной карточки
			ListViewItem item = null;
			DtCard card = null;
			long card_code = 0;
			
			item = Db.GetItemSelected(listView1);
			if(item == null) return;
			card_code = (long)item.Tag;
			if(card_code == 0) return;
			// Загружаем карточку
			card = DbSqlCard.Find(card_code);
			if(card == null) return;

			// Запрашиваем подтверждение
			if(MessageBox.Show("Обнулить автомобиль в карточке?", "Запрос", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

			if(DbSqlCard.AuxiliaryAutoSetNull(card) == false) return;
			// Перезачитываем карточку
			card = DbSqlCard.FindList(card);
			card.SetLVItem(item);
		}

		private void menuItem26_Click(object sender, System.EventArgs e)
		{
			// Замена в карточке одного владельца на другого
			// Удаляем владельца из выбранной карточки
			ListViewItem item = null;
			DtCard card = null;
			long card_code = 0;
			
			item = Db.GetItemSelected(listView1);
			if(item == null) return;
			card_code = (long)item.Tag;
			if(card_code == 0) return;
			// Загружаем карточку
			card = DbSqlCard.Find(card_code);
			if(card == null) return;

			// Запрашиваем подтверждение
			FormPartnerList dialog = new FormPartnerList();
			if(dialog.ShowDialog() != DialogResult.OK) return;
			if(MessageBox.Show("Заменить владельца в карточке на " + dialog.Partner.TitleTxt + "?", "Запрос", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

			if(DbSqlCard.AuxiliaryPartnerReplace(card, dialog.Partner) == false) return;
			// Перезачитываем карточку
			card = DbSqlCard.FindList(card);
			card.SetLVItem(item);
		}

		private void menuItem28_Click(object sender, System.EventArgs e)
		{
			// Вызов свойств владельца выбранной карточки
			// Удаляем владельца из выбранной карточки
			ListViewItem item = null;
			DtCard card = null;
			long card_code = 0;
			
			item = Db.GetItemSelected(listView1);
			if(item == null) return;
			card_code = (long)item.Tag;
			if(card_code == 0) return;
			// Загружаем карточку
			card = DbSqlCard.Find(card_code);
			if(card == null) return;

			DbPartner partner = DbPartner.Find((long)card.GetData("ВЛАДЕЛЕЦ_КАРТОЧКА"));
			if(partner == null) return;
			FormPartner dialog = new FormPartner(partner, false);
			dialog.ShowDialog();
		}

		private void menuItem29_Click(object sender, System.EventArgs e)
		{
			// Замена автомобиля в карточке
			// Замена в карточке одного владельца на другого
			// Удаляем владельца из выбранной карточки
			ListViewItem item = null;
			DtCard card = null;
			long card_code = 0;
			
			item = Db.GetItemSelected(listView1);
			if(item == null) return;
			card_code = (long)item.Tag;
			if(card_code == 0) return;
			// Загружаем карточку
			card = DbSqlCard.Find(card_code);
			if(card == null) return;

			// Запрашиваем подтверждение
			FormListAuto dialog = new FormListAuto(0, null);
			if(dialog.ShowDialog() != DialogResult.OK) return;
			if(MessageBox.Show("Заменить автомобиль в карточке на " + dialog.Auto.GetData("VIN").ToString() + "?", "Запрос", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

			if(DbSqlCard.AuxiliaryAutoReplace(card, dialog.Auto) == false) return;
			// Перезачитываем карточку
			card = DbSqlCard.FindList(card);
			card.SetLVItem(item);
		}

		private void menuItem30_Click(object sender, System.EventArgs e)
		{
			// Запуск печати гарантийного талона
			ListViewItem item = null;
			DtCard card = null;
			long card_code = 0;
			
			item = Db.GetItemSelected(listView1);
			if(item == null) return;
			card_code = (long)item.Tag;
			if(card_code == 0) return;
			// Загружаем карточку
			card = DbSqlCard.Find(card_code);
			if(card == null) return;

			DbPrintGuarantee prn = new DbPrintGuarantee(card);
			prn.Print();
		}

		private void menuItem32_Click(object sender, System.EventArgs e)
		{
			// Отчет по деталям за период
			//DbExcelDetails report = new DbExcelDetails(1, false, false, false, false);
			DbExcelDetails report = new DbExcelDetails();
			report.DownloadData(false,1);
		}

		private void menuItem33_Click(object sender, System.EventArgs e)
		{
			// Более удобный контроль над действиями
			ListViewItem item = null;
			DtCard card = null;
			long card_code = 0;
			
			item = Db.GetItemSelected(listView1);
			if(item == null) return;
			card_code = (long)item.Tag;
			if(card_code == 0) return;
			// Загружаем карточку
			card = DbSqlCard.Find(card_code);
			if(card == null) return;
			long number = (long)card.GetData("НОМЕР_КАРТОЧКА");
			int year	= (int)card.GetData("ГОД_КАРТОЧКА");

			FormCardActionMake dialog = new FormCardActionMake(number, year);
			dialog.ShowDialog();
		}

		private void listView1_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.Control == true)
			{
				switch(e.KeyCode)
				{
					case Keys.A:
						ListViewItem item = null;
						DtCard card = null;
						long card_code = 0;
			
						item = Db.GetItemSelected(listView1);
						if(item == null) return;
						card_code = (long)item.Tag;
						if(card_code == 0) return;
						// Загружаем карточку
						card = DbSqlCard.Find(card_code);
						if(card == null) return;
						long number = (long)card.GetData("НОМЕР_КАРТОЧКА");
						int year	= (int)card.GetData("ГОД_КАРТОЧКА");

						FormCardActionMake dialog = new FormCardActionMake(number, year);
						dialog.SetConnection(item);
						dialog.ShowDialog();
						break;
                    case Keys.L:
                        break;
                    case Keys.N:
                        break;
					default:
						break;
				}
			}
		}

		private void textBox_workshop_DoubleClick(object sender, System.EventArgs e)
		{
			// Активизация выбора подразделения для фильтрации
			// Список подразделений
			ListView list = new ListView();
			DbSqlWorkshop.SelectInList(list);
			FormSelectionList form = new FormSelectionList(list, "ВСЕ ПОДРАЗДЕЛЕНИЯ");
			if(form.ShowDialog() != DialogResult.OK) return;
			selected_workshop_code	= form.SelectedCode;
			textBox_workshop.Text	= form.SelectedText;

			FillList();
		}

		private void button_detail_get_Click(object sender, System.EventArgs e)
		{
			// Получаем детали на складе
			ListViewItem item = null;
			DtCard card = null;
			long card_code = 0;
			
			item = Db.GetItemSelected(listView1);
			if(item == null) return;
			card_code = (long)item.Tag;
			if(card_code == 0) return;
			// Загружаем карточку
			card = DbSqlCard.Find(card_code);
			if(card == null) return;
			long number = (long)card.GetData("НОМЕР_КАРТОЧКА");
			int year	= (int)card.GetData("ГОД_КАРТОЧКА");

			DbCard card_old = DbCard.Find(number, year);
			if(card_old == null) return;

			FormCardDetailExchange dialog = new FormCardDetailExchange(true, card_old);
			dialog.ShowDialog();
		}

		private void button_detail_return_Click(object sender, System.EventArgs e)
		{
			// Возвращаем детали на склад
			ListViewItem item = null;
			DtCard card = null;
			long card_code = 0;
			
			item = Db.GetItemSelected(listView1);
			if(item == null) return;
			card_code = (long)item.Tag;
			if(card_code == 0) return;
			// Загружаем карточку
			card = DbSqlCard.Find(card_code);
			if(card == null) return;
			long number = (long)card.GetData("НОМЕР_КАРТОЧКА");
			int year	= (int)card.GetData("ГОД_КАРТОЧКА");

			DbCard card_old = DbCard.Find(number, year);
			if(card_old == null) return;

			FormCardDetailExchange dialog = new FormCardDetailExchange(false, card_old);
			dialog.ShowDialog();
		}

		#region Новый документооборот
		private void menuItem36_Click(object sender, System.EventArgs e)
		{
			// Печать заявки клиента
			// Возвращаем детали на склад
			ListViewItem item = null;
			DtCard card = null;
			long card_code = 0;
			
			item = Db.GetItemSelected(listView1);
			if(item == null) return;
			card_code = (long)item.Tag;
			if(card_code == 0) return;
			// Загружаем карточку
			card = DbSqlCard.Find(card_code);
			if(card == null) return;
			long number = (long)card.GetData("НОМЕР_КАРТОЧКА");
			int year	= (int)card.GetData("ГОД_КАРТОЧКА");

			DbPrintClaim print = new DbPrintClaim(number, year);
			print.Print();

            // Печать акта-приема передачи
            DbPrintAcceptanceReportV01 print_p2 = new DbPrintAcceptanceReportV01(number, year);
            print_p2.Print();

			// Запрос печати второй стороны
			//if(MessageBox.Show("Будет ли выполняться пробная поездка?", "Запрос", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
			//DbPrintClaimP2 print_p2 = new DbPrintClaimP2();
			//print_p2.Print();
		}
		private void menuItem37_Click(object sender, System.EventArgs e)
		{
			// Печать заявки клиента
			ListViewItem item = null;
			DtCard card = null;
			long card_code = 0;
			
			item = Db.GetItemSelected(listView1);
			if(item == null) return;
			card_code = (long)item.Tag;
			if(card_code == 0) return;
			// Загружаем карточку
			card = DbSqlCard.Find(card_code);
			if(card == null) return;
			long number = (long)card.GetData("НОМЕР_КАРТОЧКА");
			int year	= (int)card.GetData("ГОД_КАРТОЧКА");

			DbPrintAcceptanceReport print = new DbPrintAcceptanceReport(number, year);
			print.Print();
			// Запрос печати второй стороны
			if(MessageBox.Show("Печатать акт осмотра?", "Запрос", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
			DbPrintAcceptanceReportP2 print_p2 = new DbPrintAcceptanceReportP2();
			print_p2.Print();
		}
		private void menuItem38_Click(object sender, System.EventArgs e)
		{
			// Печать временного заказ-наряда
			ListViewItem item = null;
			DtCard card = null;
			long card_code = 0;
			
			item = Db.GetItemSelected(listView1);
			if(item == null) return;
			card_code = (long)item.Tag;
			if(card_code == 0) return;
			// Загружаем карточку
			card = DbSqlCard.Find(card_code);
			if(card == null) return;
			long number = (long)card.GetData("НОМЕР_КАРТОЧКА");
			int year	= (int)card.GetData("ГОД_КАРТОЧКА");

			DbPrintWarrantTmp print = new DbPrintWarrantTmp(number, year);
			print.Print();

			// Запрос печати операционной карты
			if(MessageBox.Show("Печатать операционную карту?", "Запрос", MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				DbPrintOperations print_p3 = new DbPrintOperations(number, year);
				print_p3.Print();
			}

			// Запрос печати регламентных работ
			if(MessageBox.Show("Печатать регламентные работы?", "Запрос", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
	//		DbCard db_card = DbCard.Find(number, year);
	//		DbPrintReglament print_p2 = new DbPrintReglament(db_card);
	//		print_p2.Print();
			DbPrintReglament_v1 print_p2 = new DbPrintReglament_v1(number, year);
			if(print_p2.print_data.is_reglament == false) return;
			print_p2.Print();
		}
		private void menuItem39_Click(object sender, System.EventArgs e)
		{
			// Печать операционной карты заказ-наряда
			ListViewItem item = null;
			DtCard card = null;
			long card_code = 0;
			
			item = Db.GetItemSelected(listView1);
			if(item == null) return;
			card_code = (long)item.Tag;
			if(card_code == 0) return;
			// Загружаем карточку
			card = DbSqlCard.Find(card_code);
			if(card == null) return;
			long number = (long)card.GetData("НОМЕР_КАРТОЧКА");
			int year	= (int)card.GetData("ГОД_КАРТОЧКА");

			DbPrintOperations print = new DbPrintOperations(number, year);
			print.Print();
		}
		private void menuItem40_Click(object sender, System.EventArgs e)
		{
			// Печать закрытого заказ-няряда
			ListViewItem item = null;
			DtCard card = null;
			long card_code = 0;
			
			item = Db.GetItemSelected(listView1);
			if(item == null) return;
			card_code = (long)item.Tag;
			if(card_code == 0) return;
			// Загружаем карточку
			card = DbSqlCard.Find(card_code);
			if(card == null) return;
			long number = (long)card.GetData("НОМЕР_КАРТОЧКА");
			int year	= (int)card.GetData("ГОД_КАРТОЧКА");

			DbPrintWarrant print = new DbPrintWarrant(number, year, 0);
			print.Print();

			// Анализ необходимости печати формы опросного листа АВТОВАЗ
			DtAuto auto = DbSqlAuto.Find((long)card.GetData("АВТОМОБИЛЬ_КАРТОЧКА"));
			if(auto == null) return;
			DtModel	model = DbSqlModel.Find((long)auto.GetData("ССЫЛКА_КОД_АВТОМОБИЛЬ_МОДЕЛЬ"));
			if(model == null) return;
			DtBrand brand = DbSqlBrand.FindModel((long)model.GetData("КОД_АВТОМОБИЛЬ_МОДЕЛЬ"));
			string txt_brand = "";
			if(brand != null)
				txt_brand = (string)brand.GetData("НАИМЕНОВАНИЕ_АВТОМОБИЛЬ_БРЕНД");
			if(txt_brand == "LADA")
			{
				// Только если есть гарантия
				bool flag = false;
				DaCard analiz = new DaCard(card);
				if(analiz.IsGuaranty() == true)
				{
					flag = true;
				}
				if(flag == false)
				{
					if(analiz.IsTo())
					{
						flag = true;
					}
				}
				if(flag == true)
				{
					DbPrintQuestionnaireAvtovaz print1 = new DbPrintQuestionnaireAvtovaz(number, year);
					print1.Print();
				}
			}
			
		}
		private void menuItem41_Click(object sender, System.EventArgs e)
		{
			// Печать акта выдачи автомобиля
			ListViewItem item = null;
			DtCard card = null;
			long card_code = 0;
			
			item = Db.GetItemSelected(listView1);
			if(item == null) return;
			card_code = (long)item.Tag;
			if(card_code == 0) return;
			// Загружаем карточку
			card = DbSqlCard.Find(card_code);
			if(card == null) return;
			long number = (long)card.GetData("НОМЕР_КАРТОЧКА");
			int year	= (int)card.GetData("ГОД_КАРТОЧКА");

			DbPrintGivingout print = new DbPrintGivingout(number, year);
			print.Print();
			// Запрос печати второй стороны
			if(MessageBox.Show("Печатать акт осмотра?", "Запрос", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
			DbPrintGivingoutP2 print_p2 = new DbPrintGivingoutP2();
			print_p2.Print();
		}
		#endregion

		private void menuItem42_Click(object sender, System.EventArgs e)
		{
			// Печатаем заголовок
			// Печать акта выдачи автомобиля
			ListViewItem item = null;
			DtCard card = null;
			long card_code = 0;
			
			item = Db.GetItemSelected(listView1);
			if(item == null) return;
			card_code = (long)item.Tag;
			if(card_code == 0) return;
			// Загружаем карточку
			card = DbSqlCard.Find(card_code);
			if(card == null) return;
			long number = (long)card.GetData("НОМЕР_КАРТОЧКА");
			int year	= (int)card.GetData("ГОД_КАРТОЧКА");

			DbPrintDocHead print = new DbPrintDocHead(number, year);
			print.Print();
		}

		private void menuItem45_Click(object sender, System.EventArgs e)
		{
			// Печать гарантийного заказ-наряда по ДЖИ-ЭИ АВТОВАЗ
			ListViewItem item = null;
			DtCard card = null;
			long card_code = 0;
			
			item = Db.GetItemSelected(listView1);
			if(item == null) return;
			card_code = (long)item.Tag;
			if(card_code == 0) return;
			// Загружаем карточку
			card = DbSqlCard.Find(card_code);
			if(card == null) return;
			long number = (long)card.GetData("НОМЕР_КАРТОЧКА");
			int year	= (int)card.GetData("ГОД_КАРТОЧКА");

			DbPrintGuarantyGM print = new DbPrintGuarantyGM (number, year);
			print.Print();
		}

		private void menuItem46_Click(object sender, System.EventArgs e)
		{
			// Печать гарантийного заказ-наряда по КИА
			DtCard card;
			if ((card = GetSelectedCard()) == null) return;
			long number = card.Number;
			int year = card.Year;
			DbPrintGuarantyKIA print = new DbPrintGuarantyKIA (number, year);
			print.Print();
		}

		private void menuItem47_Click(object sender, System.EventArgs e)
		{
			DtCard card;
			if ((card = GetSelectedCard()) == null) return;
			long number = card.Number;
			int year = card.Year;
			DbPrintQuestionnaireAvtovaz print = new DbPrintQuestionnaireAvtovaz(number, year);
			print.Print();
		}
		private void menuItem48_Click(object sender, System.EventArgs e)
		{
			// Печать акта осмотра ходовой части автомобиля
			DtCard card;
			if ((card = GetSelectedCard()) == null) return;
			long number = card.Number;
			int year = card.Year;
			DbPrintInspectionUndercarriage print = new DbPrintInspectionUndercarriage(number, year);
			print.Print();
			DbPrintUndercarriageP2 print2 = new DbPrintUndercarriageP2();
			print2.Print();
		}
		private void menuItem49_Click(object sender, System.EventArgs e)
		{
			// Печать пустого акта приема в ремонт
			DbPrintAcceptanceReportEmpty print = new DbPrintAcceptanceReportEmpty();
			print.Print();
			// Запрос печати второй стороны
			if(MessageBox.Show("Печатать акт осмотра?", "Запрос", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
			DbPrintAcceptanceReportP2 print_p2 = new DbPrintAcceptanceReportP2();
			print_p2.Print();
		}
		private void menuItem51_Click(object sender, System.EventArgs e)
		{
			/*
			// Для выбранного заказ няряда, устанавливаем время окончания ремонта
			DtCard card;
			if ((card = GetSelectedCard()) == null) return;
			long number = card.Number;
			int year = card.Year;
			if(number == 0 || year == 0) return;
			// Запрос времени окончания ремонта
			//DateTime date_workend;
			FormSelectDateTime dialog = new FormSelectDateTime();
			if (dialog.ShowDialog() != DialogResult.OK) return;
			DtCardWorkend workend = new DtCardWorkend(number, year, dialog.result);
			DateTime tmp = dialog.result;
			// Пытаемся установть время окончания работ по заказ-наряду
			workend = DbSqlCardWorkend.Insert(workend);
			if(workend == null) return;
			// Создаем текст
			string txt = "Время окончания ремонта для карточки №" + number.ToString();
			txt += "\n" + "установленно на " + tmp.ToString();
			MessageBox.Show(txt);
			*/
		}
		private void menuItem52_Click(object sender, System.EventArgs e)
		{
			// Отметить окончание ремонта по заказ-наряду
			DtCard card;
			if ((card = GetSelectedCard()) == null) return;
			long number = card.Number;
			int year = card.Year;
			if(number == 0 || year == 0) return;
			// Запрос подтверждения установки окончания ремонта
			if(MessageBox.Show("Отметить окончание ремонта заказ-наряда?","Запрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
			DtCardMarkEndWork end = DbSqlCardMarkEndWork.Insert(card);
			if(end == null) return;
			MessageBox.Show("Окончание ремонта произведено в " + end.GetData("ДАТА").ToString(), "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
		private void menuItem53_Click(object sender, System.EventArgs e)
		{
			DtCard card;
			if ((card = GetSelectedCard()) == null) return;
			long number = card.Number;
			int year	= card.Year;
			if(number == 0 || year == 0) return;
			FormCardExtend dialog = new FormCardExtend(number, year);
			dialog.ShowDialog();
		}
		private void menuItem55_Click(object sender, System.EventArgs e)
		{
			// Вызов формы печати акта осмотра автомобиля, НОВОГО
			// Печать акта осмотра ходовой части автомобиля
			DtCard card;
			if ((card = GetSelectedCard()) == null) return;
			long number = card.Number;
			int year = card.Year;
			DbPrintInspection2009 print = new DbPrintInspection2009(number, year);
			print.Print();
		}
		private void menuItem57_Click(object sender, System.EventArgs e)
		{
			// Выставляем счет для безнального заказ-наряда
			DtCard card;
			if ((card = GetSelectedCard()) == null) return;
			// Проверка безнальности карточки
			if((bool)card.GetData("БЕЗНАЛИЧНЫЙ_КАРТОЧКА") == false)
			{
				MessageBox.Show("Только для безналичного расчета!");
				return;
			}
			// Открываем диалог создания нового счета
			UI_Invoice form = new UI_Invoice(card);
			form.ShowDialog();
		}
		private void menuItem58_Click(object sender, System.EventArgs e)
		{
			// Исправляем дату продажи автомобиля по карточке
			ListViewItem item = null;
			DtCard card = null;
			long card_code = 0;
			
			item = Db.GetItemSelected(listView1);
			if(item == null) return;
			card_code = (long)item.Tag;
			if(card_code == 0) return;
			// Загружаем карточку
			card = DbSqlCard.Find(card_code);
			if(card == null) return;

			// Получаем автомобиль
			long code_auto = (long) card.GetData("АВТОМОБИЛЬ_КАРТОЧКА");
			if(code_auto == 0) return;

			// Запрашиваем дату продажи
			FormSelectDate fd = new FormSelectDate();
			if (fd.ShowDialog() != DialogResult.OK) return;
			DateTime sell_date = fd.SelectedDate;

			// Запрашиваем подтверждение
			if(MessageBox.Show("Установить дату продажи " + sell_date.ToShortDateString() + "?", "Запрос", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

			if(DbSqlAuto.AuxiliarySetSellDate(code_auto, sell_date) == false) return;
			// Перезачитываем карточку
			card = DbSqlCard.FindList(card);
			card.SetLVItem(item);
		}
		private void menuItem60_Click(object sender, System.EventArgs e)
		{
			// Исправляем сервис-консультанта
			ListViewItem item = null;
			DtCard card = null;
			long card_code = 0;
			
			item = Db.GetItemSelected(listView1);
			if(item == null) return;
			card_code = (long)item.Tag;
			if(card_code == 0) return;
			// Загружаем карточку
			card = DbSqlCard.Find(card_code);
			if(card == null) return;

			FormListStaff dialog;
			dialog = new FormListStaff(2, 0);
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			DtStaff staff = dialog.SelectedStaff;
			// Производим запись
			string login = Form1.currentLogin.ToLower();
			if(login == "админ")
			{
				if (DbSqlCard.SetServiceManagerEver(card, staff) == false) return;
			}
			else
			{
				if (DbSqlCard.SetServiceManager(card, staff) == false) return;
			}
			// Если удачно - пишем сервис консультанта
			// Перезачитываем карточку
			card = DbSqlCard.FindList(card);
			card.SetLVItem(item);
		}
		private void menuItem61_Click(object sender, System.EventArgs e)
		{
			FormSelectDateInterval form = new FormSelectDateInterval();
			if (form.ShowDialog() != DialogResult.OK) return;
			listView1.Items.Clear();
			DbSqlCard.SelectInListCardReturned(listView1, form.StartDate, form.EndDate);
		}
		private void menuItem62_Click(object sender, System.EventArgs e)
		{
			DtCard card;
			if ((card = GetSelectedCard()) == null) return;
			FormManageDiscount dialogdiscount = new FormManageDiscount(card);
			DialogResult resdiscount = dialogdiscount.ShowDialog();
		}
		private void menuItem63_Click(object sender, System.EventArgs e)
		{
			DtCard card;
			if ((card = GetSelectedCard()) == null) return;
			long number = card.Number;
			int year = card.Year;
			DbPrintDiagnosticList print = new DbPrintDiagnosticList(number, year);
			print.Print();
		}
        private void menuItem64_Click(object sender, EventArgs e)
        {
			// Печать закрытого заказ-няряда
			DtCard card;
			if ((card = GetSelectedCard()) == null) return;
			long number = card.Number;
            int year = card.Year;
            DbPrintWarrant print = new DbPrintWarrant(number, year, 1);
            print.Print();
        }
        private void menuItem66_Click(object sender, EventArgs e)
        {
			// Выгрузка в EXCEL заказ-наряда для страховой компании
			// Печать закрытого заказ-няряда
			DtCard card;
			if ((card = GetSelectedCard()) == null) return;
			DbExcelCardEnsurance excel = new DbExcelCardEnsurance(card);
            excel.DownloadDataMult(false, 1);
        }

        private void menuItem67_Click(object sender, EventArgs e)
        {
			// Печать заявки клиента
			DtCard card;
			if ((card = GetSelectedCard()) == null) return;
			long number = card.Number;
            int year = card.Year;

            DbPrintAcceptanceReportV01 print = new DbPrintAcceptanceReportV01(number, year);
            print.Print(); 
        }
        private void menuItem69_Click(object sender, EventArgs e)
        {
			DtCard card;
			if ((card = GetSelectedCard()) == null) return;
			long number = card.Number;
            int year = card.Year;
            DbPrintWarrantV02 print = new DbPrintWarrantV02(number, year, 1);
            print.Print();
        }
        private void menuItem70_Click(object sender, EventArgs e)
        {
			// Создание файла ЗН с актом новой формы
			DtCard card;
			if ((card = GetSelectedCard()) == null) return;
			DbExcelNewOrder excel = new DbExcelNewOrder(card);
			excel.DownloadDataMult(true, 1);
		}
        private void menuItem72_Click(object sender, EventArgs e)
        {
			// Создание файла ЗАЯВКА-ДОГОВОР-ЗН ПО РД LADA
			DtCard card;
			if ((card = GetSelectedCard()) == null) return;
			DbExcelRD372019Claim excel = new DbExcelRD372019Claim(card);
			excel.DownloadDataMult(true, 1);
		}
        private void menuItem73_Click(object sender, EventArgs e)
        {
			DtCard card;
			if ((card = GetSelectedCard()) == null) return;
			DbExcelRD372019WorkOrder excel = new DbExcelRD372019WorkOrder(card);
			excel.DownloadDataMult(true, 1);
		}
        private void menuItem74_Click(object sender, EventArgs e)
        {
			DtCard card;
			if ((card = GetSelectedCard()) == null) return;
			DbExcelRD372019CheckOutList excel = new DbExcelRD372019CheckOutList(card);
			excel.DownloadDataMult(true, 1);
		}
        private void menuItem75_Click(object sender, EventArgs e)
        {
			DtCard card;
			if ((card = GetSelectedCard()) == null) return;
			DbExcelRD372019Act excel = new DbExcelRD372019Act(card);
			excel.DownloadDataMult(true, 1);
		}
        private void menuItem76_Click(object sender, EventArgs e)
        {
			DtCard card;
			if ((card = GetSelectedCard()) == null) return;
			DbExcelRD372019ContractOrder excel = new DbExcelRD372019ContractOrder(card);
			excel.DownloadDataMult(true, 1);
		}
        private void menuItem77_Click(object sender, EventArgs e)
        {
			DtCard card;
			if ((card = GetSelectedCard()) == null) return;
			DbExcelRD372019 excel = new DbExcelRD372019(card);
			excel.DownloadDataMult(true, 6);
		}
        #region DRY код
		public ListView GetCardsList()
        {
			return listView1;
        }
		public DtCard GetSelectedCard()
        {
			long cardCode;
			if ((cardCode = WindowsFormCommon.GetListItemSelectedTagLong(GetCardsList())) == 0) return null;
			return DbSqlCard.Find(cardCode);
		}
        #endregion

        private void menuItem78_Click(object sender, EventArgs e) // Новая форма управления заказ нарядом - редактирования наряда
        {
			ListViewItem item;
			DtCard dtCard;
			long cardCode;
			if((item = WindowsFormCommon.GetListItemSelected(GetCardsList())) == null) return;
			if ((cardCode = WindowsFormCommon.GetListItemTagLong(item)) == 0) return;
			if ((dtCard = DbSqlCard.Find(cardCode)) == null) return;
			FormCard2 dialog = new FormCard2(dtCard);
			dialog.SetItemLink(item);
			dialog.Show();
		}
        private void menuItem79_Click(object sender, EventArgs e) // Новая форма управления заказ нарядом - новый наряд
        {
			FormCard2 dialog = new FormCard2(null);
			dialog.Show();
		}

        private void menuItem80_Click(object sender, EventArgs e)
        {
			FormSelectDate dlg = new FormSelectDate();
			dlg.ShowDialog();
			if (dlg.DialogResult != DialogResult.OK) return;
			DateTime date_start = dlg.SelectedDate;

			dlg.ShowDialog();
			if (dlg.DialogResult != DialogResult.OK) return;
			DateTime date_end = dlg.SelectedDate;


			// Новый способ получения списка карточке закрытых на указанную дату
			// Считаем, что работаем в одном году
			ArrayList number_array = new ArrayList();
			DbSqlCard.SelectCardClosedNumber(number_array, date_start, date_end);
			ArrayList array = new ArrayList();
			FormInfoTable info = new FormInfoTable("Начало отсчета");
			info.Show();
			foreach (DtCard element in number_array)
			{

				DbCard card = DbCard.Find((long)element.GetData("НОМЕР_КАРТОЧКА"), (int)element.GetData("ГОД_КАРТОЧКА"));
				array.Add(card);
				info.SetText(card.NumberTxt);
			}
			info.SetText("Конец отсчета");
			info.Close();

			ExcelCardReport.DownloadList(array);
			Db.ShowFaults();
			//DbExcelCardEnsurance excel = new DbExcelCardEnsurance(card);
			//excel.DownloadDataMult(false, 1);
		}

        private void menuItem81_Click(object sender, EventArgs e)
        {
			//тестируем разные классы для печати
			DtCard card;
			if ((card = GetSelectedCard()) == null) return;
			long number = card.Number;
			int year = card.Year;

			// DbPrint print = new DbPrint();	// Общие процедуры для организации печати (нужно превратить в интерфейс)
			// DbPrintAcceptanceReport print = new DbPrintAcceptanceReport(number, year); // Старый вариант акта приема передачи полный (заполнение данных карточки)
			// DbPrintAcceptanceReportEmpty print = new DbPrintAcceptanceReportEmpty(); // Старый вариант акта приема передачи (пустой) - лист 1
			// DbPrintAcceptanceReportP2 print = new DbPrintAcceptanceReportP2(); // Старый вариант акта приема передачи - лист 2
			// DbPrintAcceptanceReportV01 print = new DbPrintAcceptanceReportV01(number, year); // Акт приема передачи с фотофиксацией (заполнение данных)

			// DbPrintCard print = new DbPrintCard(); // Ничего не печатает ???
			// DbPrintCard1 print = new DbPrintCard1(); // Какая то очень старая версия карточки (только заголовок)

			// DbPrintWarrantTmp print = new DbPrintWarrantTmp(number, year); // Предварительный заказ-наряд !Рабочая версия
			// DbPrintWarrantV02 print = new DbPrintWarrantV02(number, year, 1); // Заказ-наряд обычный

			//print.Print();

			// Типа новая версия печати

			//PrintCard print = new PrintCard(card);	// Ничего не печатает
			//PrintCardWorkOrder print = new PrintCardWorkOrder(card, new CalculatorCard(CALCULATOR_TYPE.CALCULATOR_DATABASE, VAT_TYPE.VAT_NON, 0));	// Новая форма печати
			//print.PrintPreview();

			//PrintTest print = new PrintTest(card, new CalculatorCard(CALCULATOR_TYPE.CALCULATOR_DATABASE, VAT_TYPE.VAT_NON, 0));  // Новая форма печати
			//																													  //print.PrintPreview();
			//_Print printing = new _Print();
			////printing.Print(print.CreatePrintingSchema(), true);
			//printing.Print(print, true);

			PrintRD37Act print = new PrintRD37Act(card);  // Новая форма печати
			//											
			_Print printing = new _Print();
			printing.Print(print, true);

		}

        private void menuItem82_Click(object sender, EventArgs e)
        {
			DtCard card;
			if ((card = GetSelectedCard()) == null) return;
			PrintRD37Act print = new PrintRD37Act(card);  // Новая форма печати
														  //											
			_Print printing = new _Print();
			printing.Print(print, false);
		}
    }
}
