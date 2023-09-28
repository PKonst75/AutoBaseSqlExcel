using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormAutoSell.
	/// </summary>
	public class FormAutoSell : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox textBox_customer;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.TextBox textBox_model;
		private System.Windows.Forms.Button button_select_customer;
		private System.Windows.Forms.Button button_select_auto;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.DateTimePicker dateTimePicker_date;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBox_comment;
		private System.Windows.Forms.Button button_sell;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBox_variant;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textBox_color;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textBox_vin;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox textBox_body;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox textBox_surname;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox textBox_name;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox textBox_patronymic;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.TextBox textBox_name_juridical;

		DtAutoSell sell			= null;
		ListView connected_list	= null;

		public FormAutoSell()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			// Первоначальная настройка
			// Для физического лица
			textBox_surname.Visible			= false;
			textBox_name.Visible			= false;
			textBox_patronymic.Visible		= false;
			label8.Visible					= false;
			label9.Visible					= false;
			label10.Visible					= false;
			//Для юридического лица
			textBox_name_juridical.Visible	= false;
			label11.Visible					= false;

			// Все продажи новые
			sell = new DtAutoSell();
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.button_select_customer = new System.Windows.Forms.Button();
			this.textBox_customer = new System.Windows.Forms.TextBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this.button_select_auto = new System.Windows.Forms.Button();
			this.textBox_model = new System.Windows.Forms.TextBox();
			this.dateTimePicker_date = new System.Windows.Forms.DateTimePicker();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.textBox_comment = new System.Windows.Forms.TextBox();
			this.button_sell = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.textBox_variant = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.textBox_color = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.textBox_vin = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.textBox_body = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.textBox_surname = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.textBox_name = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.textBox_patronymic = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.textBox_name_juridical = new System.Windows.Forms.TextBox();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.textBox_name_juridical,
																					this.label11,
																					this.textBox_patronymic,
																					this.label10,
																					this.textBox_name,
																					this.label9,
																					this.textBox_surname,
																					this.label8,
																					this.button_select_customer,
																					this.textBox_customer});
			this.groupBox1.Location = new System.Drawing.Point(8, 16);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(592, 144);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Покупатель";
			// 
			// button_select_customer
			// 
			this.button_select_customer.Location = new System.Drawing.Point(504, 112);
			this.button_select_customer.Name = "button_select_customer";
			this.button_select_customer.TabIndex = 1;
			this.button_select_customer.Text = "Выбрать";
			this.button_select_customer.Click += new System.EventHandler(this.button_select_customer_Click);
			// 
			// textBox_customer
			// 
			this.textBox_customer.Location = new System.Drawing.Point(8, 24);
			this.textBox_customer.Name = "textBox_customer";
			this.textBox_customer.ReadOnly = true;
			this.textBox_customer.Size = new System.Drawing.Size(576, 23);
			this.textBox_customer.TabIndex = 0;
			this.textBox_customer.Text = "";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.textBox_body,
																					this.label7,
																					this.textBox_vin,
																					this.label6,
																					this.textBox_color,
																					this.label5,
																					this.textBox_variant,
																					this.label4,
																					this.label1,
																					this.button_select_auto,
																					this.textBox_model});
			this.groupBox2.Location = new System.Drawing.Point(8, 168);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(592, 136);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Автомобиль";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(80, 23);
			this.label1.TabIndex = 2;
			this.label1.Text = "Модель";
			// 
			// button_select_auto
			// 
			this.button_select_auto.Location = new System.Drawing.Point(504, 104);
			this.button_select_auto.Name = "button_select_auto";
			this.button_select_auto.TabIndex = 1;
			this.button_select_auto.Text = "Выбрать";
			this.button_select_auto.Click += new System.EventHandler(this.button_select_auto_Click);
			// 
			// textBox_model
			// 
			this.textBox_model.Location = new System.Drawing.Point(136, 24);
			this.textBox_model.Name = "textBox_model";
			this.textBox_model.ReadOnly = true;
			this.textBox_model.Size = new System.Drawing.Size(152, 23);
			this.textBox_model.TabIndex = 0;
			this.textBox_model.Text = "";
			// 
			// dateTimePicker_date
			// 
			this.dateTimePicker_date.Location = new System.Drawing.Point(240, 312);
			this.dateTimePicker_date.Name = "dateTimePicker_date";
			this.dateTimePicker_date.TabIndex = 2;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 312);
			this.label2.Name = "label2";
			this.label2.TabIndex = 3;
			this.label2.Text = "Дата продажи";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 336);
			this.label3.Name = "label3";
			this.label3.TabIndex = 4;
			this.label3.Text = "Примечание";
			// 
			// textBox_comment
			// 
			this.textBox_comment.Location = new System.Drawing.Point(8, 360);
			this.textBox_comment.Multiline = true;
			this.textBox_comment.Name = "textBox_comment";
			this.textBox_comment.Size = new System.Drawing.Size(592, 80);
			this.textBox_comment.TabIndex = 5;
			this.textBox_comment.Text = "";
			// 
			// button_sell
			// 
			this.button_sell.Location = new System.Drawing.Point(272, 448);
			this.button_sell.Name = "button_sell";
			this.button_sell.TabIndex = 6;
			this.button_sell.Text = "Продать";
			this.button_sell.Click += new System.EventHandler(this.button_sell_Click);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 48);
			this.label4.Name = "label4";
			this.label4.TabIndex = 3;
			this.label4.Text = "Исполнение";
			// 
			// textBox_variant
			// 
			this.textBox_variant.Location = new System.Drawing.Point(136, 48);
			this.textBox_variant.Name = "textBox_variant";
			this.textBox_variant.ReadOnly = true;
			this.textBox_variant.Size = new System.Drawing.Size(152, 23);
			this.textBox_variant.TabIndex = 4;
			this.textBox_variant.Text = "";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(8, 72);
			this.label5.Name = "label5";
			this.label5.TabIndex = 5;
			this.label5.Text = "Цвет";
			// 
			// textBox_color
			// 
			this.textBox_color.Location = new System.Drawing.Point(136, 72);
			this.textBox_color.Name = "textBox_color";
			this.textBox_color.ReadOnly = true;
			this.textBox_color.Size = new System.Drawing.Size(152, 23);
			this.textBox_color.TabIndex = 6;
			this.textBox_color.Text = "";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(296, 24);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(80, 23);
			this.label6.TabIndex = 7;
			this.label6.Text = "VIN";
			// 
			// textBox_vin
			// 
			this.textBox_vin.Location = new System.Drawing.Point(400, 24);
			this.textBox_vin.Name = "textBox_vin";
			this.textBox_vin.ReadOnly = true;
			this.textBox_vin.Size = new System.Drawing.Size(184, 23);
			this.textBox_vin.TabIndex = 8;
			this.textBox_vin.Text = "";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(296, 48);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(72, 23);
			this.label7.TabIndex = 9;
			this.label7.Text = "Кузов";
			// 
			// textBox_body
			// 
			this.textBox_body.Location = new System.Drawing.Point(400, 48);
			this.textBox_body.Name = "textBox_body";
			this.textBox_body.ReadOnly = true;
			this.textBox_body.Size = new System.Drawing.Size(184, 23);
			this.textBox_body.TabIndex = 10;
			this.textBox_body.Text = "";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(8, 48);
			this.label8.Name = "label8";
			this.label8.TabIndex = 2;
			this.label8.Text = "Фамилия";
			// 
			// textBox_surname
			// 
			this.textBox_surname.Location = new System.Drawing.Point(120, 48);
			this.textBox_surname.Name = "textBox_surname";
			this.textBox_surname.ReadOnly = true;
			this.textBox_surname.Size = new System.Drawing.Size(176, 23);
			this.textBox_surname.TabIndex = 3;
			this.textBox_surname.Text = "";
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(8, 72);
			this.label9.Name = "label9";
			this.label9.TabIndex = 4;
			this.label9.Text = "Имя";
			// 
			// textBox_name
			// 
			this.textBox_name.Location = new System.Drawing.Point(120, 72);
			this.textBox_name.Name = "textBox_name";
			this.textBox_name.ReadOnly = true;
			this.textBox_name.Size = new System.Drawing.Size(176, 23);
			this.textBox_name.TabIndex = 5;
			this.textBox_name.Text = "";
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(8, 96);
			this.label10.Name = "label10";
			this.label10.TabIndex = 6;
			this.label10.Text = "Отчество";
			// 
			// textBox_patronymic
			// 
			this.textBox_patronymic.Location = new System.Drawing.Point(120, 96);
			this.textBox_patronymic.Name = "textBox_patronymic";
			this.textBox_patronymic.ReadOnly = true;
			this.textBox_patronymic.Size = new System.Drawing.Size(176, 23);
			this.textBox_patronymic.TabIndex = 7;
			this.textBox_patronymic.Text = "";
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(8, 56);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(192, 23);
			this.label11.TabIndex = 8;
			this.label11.Text = "Юридическое наименование";
			// 
			// textBox_name_juridical
			// 
			this.textBox_name_juridical.Location = new System.Drawing.Point(8, 72);
			this.textBox_name_juridical.Name = "textBox_name_juridical";
			this.textBox_name_juridical.ReadOnly = true;
			this.textBox_name_juridical.Size = new System.Drawing.Size(576, 23);
			this.textBox_name_juridical.TabIndex = 9;
			this.textBox_name_juridical.Text = "";
			// 
			// FormAutoSell
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.ClientSize = new System.Drawing.Size(610, 477);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.button_sell,
																		  this.textBox_comment,
																		  this.label3,
																		  this.label2,
																		  this.dateTimePicker_date,
																		  this.groupBox2,
																		  this.groupBox1});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "FormAutoSell";
			this.Text = "Продажа";
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void button_select_customer_Click(object sender, System.EventArgs e)
		{
			// Осуществляем выбор покупателя
			FormListPartner dialog = new FormListPartner(0, null);
			if(dialog.ShowDialog() != DialogResult.OK) return;
			// Удачно выбрали покупателя
			long	code_partner	= dialog.SelectedCode;
			DtPartner partner	= DbSqlPartner.Find(code_partner);
			if(partner	== null) return;

			textBox_customer.Text	= (string)partner.GetData("НАИМЕНОВАНИЕ_КРАТКОЕ");
			sell.SetData("ПОКУПАТЕЛЬ", partner);

			// Настройка внешнего вида в зависимости от типа покупателя
			if((bool)partner.GetData("ЮРИДИЧЕСКОЕ_ЛИЦО") == false)
			{
				// Для физического лица
				textBox_surname.Visible			= true;
				textBox_name.Visible			= true;
				textBox_patronymic.Visible		= true;
				label8.Visible					= true;
				label9.Visible					= true;
				label10.Visible					= true;
				//Для юридического лица
				textBox_name_juridical.Visible	= false;
				label11.Visible					= false;

				DtPartnerPerson person		= (DtPartnerPerson)partner.GetData("ФИЗИЧЕСКОЕ");
				textBox_surname.Text		= (string)person.GetData("ФАМИЛИЯ");
				textBox_name.Text			= (string)person.GetData("ИМЯ");
				textBox_patronymic.Text		= (string)person.GetData("ОТЧЕСТВО");
			}
			else
			{
				// Для физического лица
				textBox_surname.Visible			= false;
				textBox_name.Visible			= false;
				textBox_patronymic.Visible		= false;
				label8.Visible					= false;
				label9.Visible					= false;
				label10.Visible					= false;
				//Для юридического лица
				textBox_name_juridical.Visible	= true;
				label11.Visible					= true;

				DtPartnerJuridical juridical	= (DtPartnerJuridical)partner.GetData("ЮРИДИЧЕСКОЕ");
				textBox_name_juridical.Text		= (string)juridical.GetData("НАИМЕНОВАНИЕ_ЮРИДИЧЕСКОЕ");
			}
		}

		private void button_select_auto_Click(object sender, System.EventArgs e)
		{
			// Выбор автомобиля
			FormListAuto dialog = new FormListAuto(0, null);
			if(dialog.ShowDialog() != DialogResult.OK) return;
			// Удачно выбрали автомобиль
			DtAuto auto	= (DtAuto)dialog.Auto;
			if(auto	== null) return;

			textBox_model.Text		= (string)auto.GetData("МОДЕЛЬ");
			textBox_vin.Text		= (string)auto.GetData("VIN");
			textBox_body.Text		= (string)auto.GetData("НОМЕР_КУЗОВ");
			textBox_color.Text		= (string)auto.GetData("АВТОМОБИЛЬ_ЦВЕТ");
			textBox_variant.Text	= (string)auto.GetData("АВТОМОБИЛЬ_ИСПОЛНЕНИЕ");
			sell.SetData("АВТОМОБИЛЬ", auto);
		}

		private void button_sell_Click(object sender, System.EventArgs e)
		{
			// Осуществление продажи автомобиля
			sell.SetData("ПРИМЕЧАНИЕ_АВТОМОБИЛЬ_ПРОДАЖА", textBox_comment.Text);
			sell.SetData("ДАТА_АВТОМОБИЛЬ_ПРОДАЖА", dateTimePicker_date.Value);
			// Проверка введенных данных
			if(sell.CheckData("ССЫЛКА_КОД_АВТОМОБИЛЬ") == false)
			{
				MessageBox.Show("Не выбран автомобиль");
				return;
			}
			if(sell.CheckData("ССЫЛКА_КОД_ПОКУПАТЕЛЬ") == false)
			{
				MessageBox.Show("Не выбран покупатель");
				return;
			}

			DtAutoSell new_sell = DbSqlAutoSell.Insert(sell);
			if(new_sell == null) return;
			MessageBox.Show("Автомобиль продан");
			this.DialogResult = DialogResult.OK;
			this.Close();

			// Отображение данных во внешнем листе
			if(connected_list != null && connected_list.IsDisposed== false)
			{
				ListViewItem item = connected_list.Items.Add("");
				new_sell.SetLVItem(item);
			}
		}
		
		public void SetConnection(ListView list)
		{
			connected_list		= list;
		}
	}
}
