using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormCardExtend.
	/// </summary>
	public class FormCardExtend : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBox_card_number;
		private System.Windows.Forms.DateTimePicker dateTimePicker_card_date;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button button_create_card;
		private System.Windows.Forms.Button button_card_set_datetime;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private long	card_number		= 0L;
		private int		card_year		= 0;
		private long	warrant_number	= 0L;
		DateTime		warrant_date	= DateTime.Now;

		private System.Windows.Forms.TextBox textBox_warrant_number;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.DateTimePicker dateTimePicker_warrant_date;
		private System.Windows.Forms.Button button_warrant_open;
		private System.Windows.Forms.Button button_warrant_set_date;
		private System.Windows.Forms.Button button_set_run;
		private System.Windows.Forms.TextBox textBox_run;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button button_warrant_close;
		private System.Windows.Forms.Button button_warrant_close_set;
		private System.Windows.Forms.DateTimePicker dateTimePicker_warrant_close;
		private System.Windows.Forms.Button button_workend_set;
		private System.Windows.Forms.DateTimePicker dateTimePicker_workend;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button button_workend_update;
		private System.Windows.Forms.Button button_discount_set;
		private System.Windows.Forms.TextBox textBox_discount;
		private DtCard	card		= null;
		

		public FormCardExtend(long number, int year)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			card_number		= number;
			card_year		= year;
			card			= DbSqlCard.Find(card_number, card_year);

			// Настройка компонентов
			// Карточка
			if(number	== 0 || year == 0 || card == null)
			{
				textBox_card_number.Enabled			= true;
				button_create_card.Enabled			= true;
				button_card_set_datetime.Enabled	= false;
				
				button_warrant_set_date.Enabled		= false;
				button_warrant_open.Enabled			= false;
				textBox_warrant_number.Enabled		= false;
				dateTimePicker_warrant_date.Enabled	= false;

				button_set_run.Enabled				= false;
				textBox_run.Enabled					= false;

				button_warrant_close.Enabled		= false;
				button_warrant_close_set.Enabled	= false;
				dateTimePicker_warrant_close.Enabled= false;

				button_workend_set.Enabled			= false;
				button_workend_update.Enabled		= false;
				dateTimePicker_workend.Enabled		= false;

				textBox_discount.Enabled			= false;
				button_discount_set.Enabled			= false;
				return;
			}
			
			// Выставление данных и настройка карточки
			textBox_card_number.Text			= card.GetData("НОМЕР_КАРТОЧКА").ToString();
			dateTimePicker_card_date.Value		= (DateTime)card.GetData("ДАТА");
			textBox_card_number.Enabled			= false;
			button_create_card.Enabled			= false;
			button_card_set_datetime.Enabled	= true;

			// Выставление данных и настройка заказ-наряда
			warrant_number						= (long)card.GetData("НОМЕР_НАРЯД_КАРТОЧКА");
			warrant_date						= (DateTime)card.GetData("ДАТА_НАРЯД_ОТКРЫТ_КАРТОЧКА");
			if(warrant_number == 0)
			{
				button_warrant_open.Enabled		= true;
				button_warrant_set_date.Enabled	= false;
			}
			else
			{
				textBox_warrant_number.Text			= warrant_number.ToString();
				dateTimePicker_warrant_date.Value	= warrant_date;
				button_warrant_open.Enabled			= false;
				button_warrant_set_date.Enabled		= true;
			}

			// Пробег
			int run								= (int)card.GetData("ПРОБЕГ_КАРТОЧКА");
			textBox_run.Text					= run.ToString();

			// Дата закрытия
			short status	= (short)card.GetData("СТАТУС_КАРТОЧКА");
			if(status == 2)
			{
				DateTime close_date						= (DateTime)card.GetData("ДАТА_НАРЯД_ЗАКРЫТ_КАРТОЧКА");
				dateTimePicker_warrant_close.Value		= close_date;
				button_warrant_close.Enabled			= false;
				button_warrant_close_set.Enabled		= true;
			}
			else
			{
				button_warrant_close.Enabled			= true;
				button_warrant_close_set.Enabled		= false;
			}

			// Окончание ремонта
			DtCardMarkEndWork element = DbSqlCardMarkEndWork.Find(card_number, card_year);
			if(element == null)
			{
				button_workend_set.Enabled			= true;
				button_workend_update.Enabled		= false;
			}
			else
			{
				button_workend_set.Enabled			= false;
				button_workend_update.Enabled		= true;
				dateTimePicker_workend.Value		= (DateTime)element.GetData("ДАТА");
			}

			// Скидка
			float discount						= (float)card.GetData("СКИДКА_РАБОТА_КАРТОЧКА");
			textBox_discount.Text				= discount.ToString();
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
			this.label1 = new System.Windows.Forms.Label();
			this.textBox_card_number = new System.Windows.Forms.TextBox();
			this.dateTimePicker_card_date = new System.Windows.Forms.DateTimePicker();
			this.label2 = new System.Windows.Forms.Label();
			this.button_create_card = new System.Windows.Forms.Button();
			this.button_card_set_datetime = new System.Windows.Forms.Button();
			this.textBox_warrant_number = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.dateTimePicker_warrant_date = new System.Windows.Forms.DateTimePicker();
			this.button_warrant_open = new System.Windows.Forms.Button();
			this.button_warrant_set_date = new System.Windows.Forms.Button();
			this.textBox_run = new System.Windows.Forms.TextBox();
			this.button_set_run = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.dateTimePicker_warrant_close = new System.Windows.Forms.DateTimePicker();
			this.button_warrant_close = new System.Windows.Forms.Button();
			this.button_warrant_close_set = new System.Windows.Forms.Button();
			this.button_workend_set = new System.Windows.Forms.Button();
			this.dateTimePicker_workend = new System.Windows.Forms.DateTimePicker();
			this.label6 = new System.Windows.Forms.Label();
			this.button_workend_update = new System.Windows.Forms.Button();
			this.button_discount_set = new System.Windows.Forms.Button();
			this.textBox_discount = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 24);
			this.label1.TabIndex = 0;
			this.label1.Text = "Номер карточки";
			// 
			// textBox_card_number
			// 
			this.textBox_card_number.Location = new System.Drawing.Point(152, 16);
			this.textBox_card_number.Name = "textBox_card_number";
			this.textBox_card_number.Size = new System.Drawing.Size(88, 20);
			this.textBox_card_number.TabIndex = 1;
			this.textBox_card_number.Text = "";
			// 
			// dateTimePicker_card_date
			// 
			this.dateTimePicker_card_date.Format = System.Windows.Forms.DateTimePickerFormat.Time;
			this.dateTimePicker_card_date.Location = new System.Drawing.Point(152, 40);
			this.dateTimePicker_card_date.Name = "dateTimePicker_card_date";
			this.dateTimePicker_card_date.Size = new System.Drawing.Size(88, 20);
			this.dateTimePicker_card_date.TabIndex = 2;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(136, 23);
			this.label2.TabIndex = 3;
			this.label2.Text = "Дата и время карточки";
			// 
			// button_create_card
			// 
			this.button_create_card.Location = new System.Drawing.Point(8, 72);
			this.button_create_card.Name = "button_create_card";
			this.button_create_card.Size = new System.Drawing.Size(232, 23);
			this.button_create_card.TabIndex = 4;
			this.button_create_card.Text = "Создать карточку";
			this.button_create_card.Click += new System.EventHandler(this.button_create_card_Click);
			// 
			// button_card_set_datetime
			// 
			this.button_card_set_datetime.Location = new System.Drawing.Point(8, 104);
			this.button_card_set_datetime.Name = "button_card_set_datetime";
			this.button_card_set_datetime.Size = new System.Drawing.Size(232, 23);
			this.button_card_set_datetime.TabIndex = 5;
			this.button_card_set_datetime.Text = "Установить дату и время";
			this.button_card_set_datetime.Click += new System.EventHandler(this.button_card_set_datetime_Click);
			// 
			// textBox_warrant_number
			// 
			this.textBox_warrant_number.Location = new System.Drawing.Point(416, 16);
			this.textBox_warrant_number.Name = "textBox_warrant_number";
			this.textBox_warrant_number.Size = new System.Drawing.Size(104, 20);
			this.textBox_warrant_number.TabIndex = 6;
			this.textBox_warrant_number.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(256, 16);
			this.label3.Name = "label3";
			this.label3.TabIndex = 7;
			this.label3.Text = "Заказ-наряд";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(256, 40);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(152, 23);
			this.label4.TabIndex = 8;
			this.label4.Text = "Дата и время заказ-наряда";
			// 
			// dateTimePicker_warrant_date
			// 
			this.dateTimePicker_warrant_date.Format = System.Windows.Forms.DateTimePickerFormat.Time;
			this.dateTimePicker_warrant_date.Location = new System.Drawing.Point(416, 40);
			this.dateTimePicker_warrant_date.Name = "dateTimePicker_warrant_date";
			this.dateTimePicker_warrant_date.Size = new System.Drawing.Size(104, 20);
			this.dateTimePicker_warrant_date.TabIndex = 9;
			// 
			// button_warrant_open
			// 
			this.button_warrant_open.Location = new System.Drawing.Point(256, 72);
			this.button_warrant_open.Name = "button_warrant_open";
			this.button_warrant_open.Size = new System.Drawing.Size(264, 23);
			this.button_warrant_open.TabIndex = 10;
			this.button_warrant_open.Text = "Открыть заказ-наряд";
			this.button_warrant_open.Click += new System.EventHandler(this.button_warrant_open_Click);
			// 
			// button_warrant_set_date
			// 
			this.button_warrant_set_date.Location = new System.Drawing.Point(256, 104);
			this.button_warrant_set_date.Name = "button_warrant_set_date";
			this.button_warrant_set_date.Size = new System.Drawing.Size(264, 23);
			this.button_warrant_set_date.TabIndex = 11;
			this.button_warrant_set_date.Text = "Изменить заказ-наряд";
			this.button_warrant_set_date.Click += new System.EventHandler(this.button_warrant_set_date_Click);
			// 
			// textBox_run
			// 
			this.textBox_run.Location = new System.Drawing.Point(136, 144);
			this.textBox_run.Name = "textBox_run";
			this.textBox_run.Size = new System.Drawing.Size(104, 20);
			this.textBox_run.TabIndex = 13;
			this.textBox_run.Text = "";
			// 
			// button_set_run
			// 
			this.button_set_run.Location = new System.Drawing.Point(8, 144);
			this.button_set_run.Name = "button_set_run";
			this.button_set_run.Size = new System.Drawing.Size(120, 23);
			this.button_set_run.TabIndex = 14;
			this.button_set_run.Text = "Установить пробег";
			this.button_set_run.Click += new System.EventHandler(this.button_set_run_Click);
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(536, 40);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(168, 23);
			this.label5.TabIndex = 15;
			this.label5.Text = "Дата закрытия заказ-наряда";
			// 
			// dateTimePicker_warrant_close
			// 
			this.dateTimePicker_warrant_close.Format = System.Windows.Forms.DateTimePickerFormat.Time;
			this.dateTimePicker_warrant_close.Location = new System.Drawing.Point(704, 40);
			this.dateTimePicker_warrant_close.Name = "dateTimePicker_warrant_close";
			this.dateTimePicker_warrant_close.Size = new System.Drawing.Size(96, 20);
			this.dateTimePicker_warrant_close.TabIndex = 16;
			// 
			// button_warrant_close
			// 
			this.button_warrant_close.Location = new System.Drawing.Point(536, 72);
			this.button_warrant_close.Name = "button_warrant_close";
			this.button_warrant_close.Size = new System.Drawing.Size(264, 23);
			this.button_warrant_close.TabIndex = 17;
			this.button_warrant_close.Text = "Закрыть заказ-наряд";
			this.button_warrant_close.Click += new System.EventHandler(this.button_warrant_close_Click);
			// 
			// button_warrant_close_set
			// 
			this.button_warrant_close_set.Location = new System.Drawing.Point(536, 104);
			this.button_warrant_close_set.Name = "button_warrant_close_set";
			this.button_warrant_close_set.Size = new System.Drawing.Size(264, 23);
			this.button_warrant_close_set.TabIndex = 18;
			this.button_warrant_close_set.Text = "Установть дату закрытия";
			this.button_warrant_close_set.Click += new System.EventHandler(this.button_warrant_close_set_Click);
			// 
			// button_workend_set
			// 
			this.button_workend_set.Location = new System.Drawing.Point(536, 176);
			this.button_workend_set.Name = "button_workend_set";
			this.button_workend_set.Size = new System.Drawing.Size(264, 23);
			this.button_workend_set.TabIndex = 19;
			this.button_workend_set.Text = "Установть время окончания ремонта";
			this.button_workend_set.Click += new System.EventHandler(this.button_workend_set_Click);
			// 
			// dateTimePicker_workend
			// 
			this.dateTimePicker_workend.Format = System.Windows.Forms.DateTimePickerFormat.Time;
			this.dateTimePicker_workend.Location = new System.Drawing.Point(704, 144);
			this.dateTimePicker_workend.Name = "dateTimePicker_workend";
			this.dateTimePicker_workend.Size = new System.Drawing.Size(96, 20);
			this.dateTimePicker_workend.TabIndex = 20;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(536, 152);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(120, 23);
			this.label6.TabIndex = 21;
			this.label6.Text = "Окончание ремонта";
			// 
			// button_workend_update
			// 
			this.button_workend_update.Location = new System.Drawing.Point(536, 208);
			this.button_workend_update.Name = "button_workend_update";
			this.button_workend_update.Size = new System.Drawing.Size(264, 23);
			this.button_workend_update.TabIndex = 22;
			this.button_workend_update.Text = "Изменить время окончания ремонта";
			this.button_workend_update.Click += new System.EventHandler(this.button_workend_update_Click);
			// 
			// button_discount_set
			// 
			this.button_discount_set.Location = new System.Drawing.Point(8, 176);
			this.button_discount_set.Name = "button_discount_set";
			this.button_discount_set.Size = new System.Drawing.Size(120, 23);
			this.button_discount_set.TabIndex = 23;
			this.button_discount_set.Text = "Установить скидку";
			this.button_discount_set.Click += new System.EventHandler(this.button_discount_set_Click);
			// 
			// textBox_discount
			// 
			this.textBox_discount.Location = new System.Drawing.Point(136, 176);
			this.textBox_discount.Name = "textBox_discount";
			this.textBox_discount.Size = new System.Drawing.Size(104, 20);
			this.textBox_discount.TabIndex = 24;
			this.textBox_discount.Text = "";
			// 
			// FormCardExtend
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(832, 365);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.textBox_discount,
																		  this.button_discount_set,
																		  this.button_workend_update,
																		  this.label6,
																		  this.dateTimePicker_workend,
																		  this.button_workend_set,
																		  this.button_warrant_close_set,
																		  this.button_warrant_close,
																		  this.dateTimePicker_warrant_close,
																		  this.label5,
																		  this.button_set_run,
																		  this.textBox_run,
																		  this.button_warrant_set_date,
																		  this.button_warrant_open,
																		  this.dateTimePicker_warrant_date,
																		  this.label4,
																		  this.label3,
																		  this.textBox_warrant_number,
																		  this.button_card_set_datetime,
																		  this.button_create_card,
																		  this.label2,
																		  this.dateTimePicker_card_date,
																		  this.textBox_card_number,
																		  this.label1});
			this.Name = "FormCardExtend";
			this.Text = "Заказ-Наряд  - расширенное управление";
			this.ResumeLayout(false);

		}
		#endregion

		private void button_create_card_Click(object sender, System.EventArgs e)
		{
			// Создание карточки с заданными характеристиками
			string txt_number	= textBox_card_number.Text;
			long card_number = 0L;
			try
			{
				card_number	= Convert.ToInt64(txt_number);
			}
			catch(Exception E){}
			if(card_number == 0L) return;
			DateTime card_date = dateTimePicker_card_date.Value;
			if(DbSqlCard.AuxiliaryCardInsert(card_number, card_date) == false)
			{
				return;
			}
			MessageBox.Show("Карточка успешно создана");
		}

		private void button_card_set_datetime_Click(object sender, System.EventArgs e)
		{
			// Устанавливаем дату и время карточки
			DateTime card_date = dateTimePicker_card_date.Value;
			if(DbSqlCard.AuxiliaryCardSetDate(card_number, card_year, card_date) == false)
			{
				return;
			}
			MessageBox.Show("Изменили дату");
		}

		private void button_warrant_set_date_Click(object sender, System.EventArgs e)
		{
			// Изменяем данные о заказ-наряде
			DateTime date		= dateTimePicker_warrant_date.Value;
			string txt_number	= textBox_warrant_number.Text;
			long number = 0L;
			try
			{
				number	= Convert.ToInt64(txt_number);
			}
			catch(Exception E){return;}
			if(DbSqlCard.AuxiliaryWarrantSet(card_number, card_year, number, date) == false)
			{
				return;
			}
			MessageBox.Show("Изменили заказ-наряд");
		}

		private void button_set_run_Click(object sender, System.EventArgs e)
		{
			// Установть новый пробег
			string txt_run	= textBox_run.Text;
			int run = 0;
			try
			{
				run	= Convert.ToInt32(txt_run);
			}
			catch(Exception E){return;}
			if(DbSqlCard.AuxiliaryRunSet(card_number, card_year, run) == false)
			{
				return;
			}
			MessageBox.Show("Установили пробег");
		}

		private void button_warrant_open_Click(object sender, System.EventArgs e)
		{
			// Открываем новый заказ-наряд
			DateTime date		= dateTimePicker_warrant_date.Value;
			string txt_number	= textBox_warrant_number.Text;
			long number = 0L;
			try
			{
				number	= Convert.ToInt64(txt_number);
			}
			catch(Exception E){return;}
			if(DbSqlCard.AuxiliaryWarrantOpen(card_number, card_year, number, date) == false)
			{
				return;
			}
			MessageBox.Show("Открыли заказ-наряд");
		}

		private void button_warrant_close_Click(object sender, System.EventArgs e)
		{
			// Закрываем заказ-наряд
			DateTime date		= dateTimePicker_warrant_close.Value;
			if(DbSqlCard.AuxiliaryWarrantClose(card_number, card_year, date) == false)
			{
				return;
			}
			MessageBox.Show("Закрыли заказ-наряд");
		}

		private void button_warrant_close_set_Click(object sender, System.EventArgs e)
		{
			// Закрываем заказ-наряд
			DateTime date		= dateTimePicker_warrant_close.Value;
			if(DbSqlCard.AuxiliaryWarrantCloseSet(card_number, card_year, date) == false)
			{
				return;
			}
			MessageBox.Show("Изменили закрытие заказ-наряда");
		}

		private void button_workend_set_Click(object sender, System.EventArgs e)
		{
			// Устанавливаем время окончания ремонта
			DateTime date		= dateTimePicker_workend.Value;
			if(DbSqlCardMarkEndWork.AuxiliaryInsert(card_number, card_year, date) == false)
			{
				return;
			}
			MessageBox.Show("Добавили время окончания ремонта");
		}

		private void button_workend_update_Click(object sender, System.EventArgs e)
		{
			// Устанавливаем время окончания ремонта
			DateTime date		= dateTimePicker_workend.Value;
			if(DbSqlCardMarkEndWork.AuxiliaryUpdate(card_number, card_year, date) == false)
			{
				return;
			}
			MessageBox.Show("Bpvtybkb время окончания ремонта");
		}

		private void button_discount_set_Click(object sender, System.EventArgs e)
		{
			// Устанавливаем скидку
			string txt_discount	= textBox_discount.Text;
			float discount = 0.0F;
			try
			{
				discount	= Convert.ToSingle(txt_discount);
			}
			catch(Exception E){return;}
			if(DbSqlCard.SetDiscount(card_number, card_year, discount, 0) == false)
			{
				return;
			}
			MessageBox.Show("Установили скидку");
		}
	}
}
