using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for UIF_Payment_List.
	/// </summary>
	public class UIF_Payment_List : System.Windows.Forms.Form
	{
		private System.Windows.Forms.DateTimePicker dateTimePicker_date;
		private System.Windows.Forms.Button button_select_date;
		private System.Windows.Forms.ListView listView_payments;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.ImageList imageList_status;
		private System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.Button button_search_vin;
		private System.ComponentModel.IContainer components;

		public UIF_Payment_List()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(UIF_Payment_List));
			this.dateTimePicker_date = new System.Windows.Forms.DateTimePicker();
			this.button_select_date = new System.Windows.Forms.Button();
			this.listView_payments = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
			this.imageList_status = new System.Windows.Forms.ImageList(this.components);
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.button_search_vin = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// dateTimePicker_date
			// 
			this.dateTimePicker_date.Location = new System.Drawing.Point(8, 16);
			this.dateTimePicker_date.Name = "dateTimePicker_date";
			this.dateTimePicker_date.Size = new System.Drawing.Size(128, 20);
			this.dateTimePicker_date.TabIndex = 0;
			// 
			// button_select_date
			// 
			this.button_select_date.Location = new System.Drawing.Point(136, 16);
			this.button_select_date.Name = "button_select_date";
			this.button_select_date.Size = new System.Drawing.Size(88, 23);
			this.button_select_date.TabIndex = 1;
			this.button_select_date.Text = "Выбрать";
			this.button_select_date.Click += new System.EventHandler(this.button_select_date_Click);
			// 
			// listView_payments
			// 
			this.listView_payments.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.listView_payments.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																								this.columnHeader1,
																								this.columnHeader2,
																								this.columnHeader3,
																								this.columnHeader4,
																								this.columnHeader5,
																								this.columnHeader6,
																								this.columnHeader7});
			this.listView_payments.FullRowSelect = true;
			this.listView_payments.Location = new System.Drawing.Point(8, 48);
			this.listView_payments.Name = "listView_payments";
			this.listView_payments.Size = new System.Drawing.Size(920, 340);
			this.listView_payments.StateImageList = this.imageList_status;
			this.listView_payments.TabIndex = 2;
			this.listView_payments.View = System.Windows.Forms.View.Details;
			this.listView_payments.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listView_payments_MouseUp);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Дата и время";
			this.columnHeader1.Width = 121;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Касса";
			this.columnHeader2.Width = 48;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Сумма";
			this.columnHeader3.Width = 72;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Подразделение";
			this.columnHeader4.Width = 118;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "Заказ-наряд";
			this.columnHeader5.Width = 149;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "Автомобиль";
			this.columnHeader6.Width = 261;
			// 
			// columnHeader7
			// 
			this.columnHeader7.Text = "Клиент";
			this.columnHeader7.Width = 118;
			// 
			// imageList_status
			// 
			this.imageList_status.ColorDepth = System.Windows.Forms.ColorDepth.Depth16Bit;
			this.imageList_status.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList_status.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList_status.ImageStream")));
			this.imageList_status.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// contextMenu1
			// 
			this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.menuItem1,
																						 this.menuItem3});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem2});
			this.menuItem1.Text = "Изменения";
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 0;
			this.menuItem2.Text = "Установить заказ-наряд";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 1;
			this.menuItem3.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem4});
			this.menuItem3.Text = "Удаление";
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 0;
			this.menuItem4.Text = "Удалить платеж";
			this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
			// 
			// button_search_vin
			// 
			this.button_search_vin.Location = new System.Drawing.Point(264, 16);
			this.button_search_vin.Name = "button_search_vin";
			this.button_search_vin.Size = new System.Drawing.Size(88, 23);
			this.button_search_vin.TabIndex = 3;
			this.button_search_vin.Text = "Поиск по VIN";
			this.button_search_vin.Click += new System.EventHandler(this.button_search_vin_Click);
			// 
			// UIF_Payment_List
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(936, 397);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.button_search_vin,
																		  this.listView_payments,
																		  this.button_select_date,
																		  this.dateTimePicker_date});
			this.Name = "UIF_Payment_List";
			this.Text = "Список платежей";
			this.ResumeLayout(false);

		}
		#endregion

		private void button_select_date_Click(object sender, System.EventArgs e)
		{
			// Заполнение списка платежей на указанную дату
			// Выбираем заказ-наряды закрытые на дату
			listView_payments.Items.Clear();
			DateTime date = dateTimePicker_date.Value;
			ArrayList payments = new ArrayList();
			DbSqlPayment.SelectInArray(payments, date);

			// Составляем список
			foreach(object o in payments)
			{
				CS_Payment pay = (CS_Payment)o;
				ListViewItem item = new ListViewItem("ОШИБКА");
				pay = DbSqlPayment.Find(pay.code, pay.year);
				if(pay != null)
				{
					pay.SetLVItem(item);
					listView_payments.Items.Add(item);
				}
			}
		}

		private void listView_payments_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			// Запускаем контекстное меню
			if(e.Button == MouseButtons.Right)
			{
				// Находим элемент на котором отпустили
				ListViewItem item = Db.GetItemSelected(listView_payments);
				// На отпускание правой кнопки мышки - всплывающее меню
				// Настройка меню
				// Права пользователя
				// Отключаем все запрещенное
				//menuItem6.Enabled = false;
				
				// Включаем по разрешению
				//string login = Form1.currentLogin.ToLower();
				//if (login == "админ")
				//{
				//	menuItem6.Enabled = true;
				//}
				// Настройка меню исходя из свойств выбранной карточки
				// Показ меню
				contextMenu1.Show(listView_payments, new Point(e.X, e.Y));
			}
		}

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			// Привязать к платежу заказ-наряд
			// Устанавливаем вид гарантии для выбранного элемента
			// Ищем выбранный элемент
			ListViewItem item = Db.GetItemSelected(listView_payments);
			if(item == null) return;
			if(item.Tag == null) return;
			CS_Payment.Pair pair = (CS_Payment.Pair)item.Tag;
			long code = pair.code;
			int year = pair.year;
			if(year == 0 || code == 0L) return;
			CS_Payment pay = DbSqlPayment.Find(code, year);
			if(pay == null) return;
			// Запрашиваем карточку для привязки
			FormManageCard form = new FormManageCard(Db.ClickType.Select, 0, null);
			if(form.ShowDialog() != DialogResult.OK) return;
			DtCard card = form.card_selected;
			card = DbSqlCard.Find((long)card.GetData("КОД_КАРТОЧКА"));
			if(card == null) return;
			// Ппроверяем справедливость присваивания
			if((long)card.GetData("ПОДРАЗДЕЛЕНИЕ_КАРТОЧКА") != pay.code_workshop)
			{
				MessageBox.Show("Не совпадают подразделения");
				return;
			}
			// Пытаемся установть карточку
			long card_number = (long)card.GetData("НОМЕР_КАРТОЧКА");
			int card_year = (int)card.GetData("ГОД_КАРТОЧКА");
			if(DbSqlPayment.SetPaymentCard(card_number, card_year, pay) == false) return;
			pay = DbSqlPayment.Find(code, year);
			pay.SetLVItem(item);
		}

		private void menuItem4_Click(object sender, System.EventArgs e)
		{
			// Удаление выбранного платежа
			// Привязать к платежу заказ-наряд
			// Устанавливаем вид гарантии для выбранного элемента
			// Ищем выбранный элемент
			ListViewItem item = Db.GetItemSelected(listView_payments);
			if(item == null) return;
			if(item.Tag == null) return;
			CS_Payment.Pair pair = (CS_Payment.Pair)item.Tag;
			long code = pair.code;
			int year = pair.year;
			if(year == 0 || code == 0L) return;
			CS_Payment pay = DbSqlPayment.Find(code, year);
			if(pay == null) return;
			
			if(DbSqlPayment.Delete(code, year) == false) return;
			listView_payments.Items.Remove(item);
		}

		private void button_search_vin_Click(object sender, System.EventArgs e)
		{
			// Поиск платежа по автомобилю
			listView_payments.Items.Clear();

			FormAutoList dlg = new FormAutoList(Db.ClickType.Select, null);
			if(dlg.ShowDialog() != DialogResult.OK) return;
			if (dlg.Auto == null) return;
			DbAuto auto = dlg.Auto;
			if(auto.Code == 0) return;
			long code_auto = auto.Code;

			
			ArrayList payments = new ArrayList();
			DbSqlPayment.SelectInArrayAuto(payments, code_auto);

			// Составляем список
			foreach(object o in payments)
			{
				CS_Payment pay = (CS_Payment)o;
				ListViewItem item = new ListViewItem("ОШИБКА");
				pay = DbSqlPayment.Find(pay.code, pay.year);
				if(pay != null)
				{
					pay.SetLVItem(item);
					listView_payments.Items.Add(item);
				}
			}
		}
	}
}
