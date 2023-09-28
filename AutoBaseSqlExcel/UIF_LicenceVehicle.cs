using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for UIF_LicenceVehicle.
	/// </summary>
	public class UIF_LicenceVehicle : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBox_number;
		private System.Windows.Forms.TextBox textBox_region;
		private System.Windows.Forms.Button button_select_auto;
		private System.Windows.Forms.TextBox textBox_vin;
		private System.Windows.Forms.TextBox textBox_licence_series;
		private System.Windows.Forms.TextBox textBox_licence_number;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox textBox_owner_firstname;
		private System.Windows.Forms.TextBox textBox_owner_name;
		private System.Windows.Forms.DateTimePicker dateTimePicker_date;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBox_model;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textBox_year;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox textBox_engine_no;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox textBox_frame_no;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox textBox_body_no;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Button button_select_person;
		private System.Windows.Forms.TextBox textBox_owner_patronymic;
		private System.Windows.Forms.TextBox textBox_licence_series_cpy1;
		private System.Windows.Forms.TextBox textBox_licence_number_cpy1;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.Label label22;
		private System.Windows.Forms.Label label23;
		private System.Windows.Forms.Label label24;
		private System.Windows.Forms.Label label25;
		private System.Windows.Forms.TextBox textBox_licence_number_cpy2;
		private System.Windows.Forms.TextBox textBox_licence_series_cpy2;
		private System.Windows.Forms.Button button_save;
		private System.Windows.Forms.Button button_cancel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		bool ignore = false;
		CS_LicenceVehicle licence = null;
		DtAuto selected_auto = null;
		private System.Windows.Forms.Button button_apply;
		DtPartner selected_partner = null;

		public UIF_LicenceVehicle(CS_LicenceVehicle licence_src, string licence_number)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			// Дополнительная настройка вида
			dateTimePicker_date.Format	= DateTimePickerFormat.Custom;
			dateTimePicker_date.CustomFormat  = "dd.MM.yyyy";

			textBox_vin.ReadOnly		= true;
			textBox_model.ReadOnly		= true;
			textBox_year.ReadOnly		= true;
			textBox_engine_no.ReadOnly	= true;
			textBox_frame_no.ReadOnly	= true;
			textBox_body_no.ReadOnly	= true;

			textBox_owner_firstname.ReadOnly	= true;
			textBox_owner_name.ReadOnly			= true;
			textBox_owner_patronymic.ReadOnly	= true;

			textBox_licence_series_cpy1.ReadOnly	= true;
			textBox_licence_series_cpy2.ReadOnly	= true;
			textBox_licence_number_cpy1.ReadOnly	= true;
			textBox_licence_number_cpy2.ReadOnly	= true;

			textBox_region.Text			= "54";		// Сделать умолчание доступным из файла
			textBox_licence_number.Text			= licence_number;
			textBox_licence_number_cpy1.Text	= textBox_licence_number.Text;
			textBox_licence_number_cpy2.Text	= textBox_licence_number.Text;

			button_apply.Enabled				= false;

			if(licence_src != null)
			{
				// Запрет изменений
				textBox_region.ReadOnly			= true;
				textBox_number.ReadOnly			= true;
				textBox_licence_series.ReadOnly	= true;
				textBox_licence_number.ReadOnly	= true;
				button_select_auto.Enabled		= false;
				button_select_person.Enabled	= false;
				dateTimePicker_date.Enabled		= false;
				button_save.Enabled				= false;
				button_apply.Enabled			= true;

				// Заполнение в случае показа
				textBox_region.Text					= licence_src.vehicle_region;
				textBox_number.Text					= licence_src.vehicle_number;

				textBox_licence_number.Text			= licence_src.licence_number;
				textBox_licence_number_cpy1.Text	= textBox_licence_number.Text;
				textBox_licence_number_cpy2.Text	= textBox_licence_number.Text;
				textBox_licence_series.Text			= licence_src.licence_series;
				textBox_licence_series_cpy1.Text	= textBox_licence_series.Text;
				textBox_licence_series_cpy2.Text	= textBox_licence_series.Text;

				dateTimePicker_date.Value			= licence_src.date;

				// Данные автомобиля
				DtAuto auto = (DtAuto)DbSqlAuto.Find(licence_src.code_auto);
				DtModel model = (DtModel)DbSqlModel.Find((long)auto.GetData("ССЫЛКА_КОД_АВТОМОБИЛЬ_МОДЕЛЬ"));
				if(auto != null && model != null)
				{
					// Успешно выбрали автомобиль, отображение для пользователся
					selected_auto			= auto;
					textBox_vin.Text		= (string)auto.GetData("VIN");
					textBox_model.Text		= (string)model.GetData("МОДЕЛЬ");
					textBox_year.Text		= (string)auto.GetData("ГОД_ВЫПУСК").ToString();
					textBox_engine_no.Text	= (string)auto.GetData("НОМЕР_ДВИГАТЕЛЬ");
					textBox_frame_no.Text	= (string)auto.GetData("НОМЕР_ШАССИ");
					textBox_body_no.Text	= (string)auto.GetData("НОМЕР_КУЗОВ");
				}

				// Данные владельца
				DtPartner partner = (DtPartner)DbSqlPartner.Find(licence_src.code_owner);
				if(partner != null)
				{
					// Успешно загрузили владельца
					selected_partner = partner;
					if((bool)partner.GetData("ЮРИДИЧЕСКОЕ_ЛИЦО") == false)
					{
						DtPartnerPerson person = (DtPartnerPerson)partner.GetData("ФИЗИЧЕСКОЕ");
						textBox_owner_firstname.Text	= (string)person.GetData("ФАМИЛИЯ");
						textBox_owner_name.Text			= (string)person.GetData("ИМЯ");
						textBox_owner_patronymic.Text	= (string)person.GetData("ОТЧЕСТВО");
					}
					else
					{
						DtPartnerJuridical juridical	= (DtPartnerJuridical)partner.GetData("ЮРИДИЧЕСКОЕ");
						textBox_owner_firstname.Text	= (string)juridical.GetData("НАИМЕНОВАНИЕ_ЮРИДИЧЕСКОЕ");
					}
				}
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
			this.label1 = new System.Windows.Forms.Label();
			this.textBox_number = new System.Windows.Forms.TextBox();
			this.textBox_region = new System.Windows.Forms.TextBox();
			this.button_select_auto = new System.Windows.Forms.Button();
			this.textBox_vin = new System.Windows.Forms.TextBox();
			this.textBox_licence_series = new System.Windows.Forms.TextBox();
			this.textBox_licence_number = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.textBox_licence_number_cpy2 = new System.Windows.Forms.TextBox();
			this.textBox_licence_series_cpy2 = new System.Windows.Forms.TextBox();
			this.label25 = new System.Windows.Forms.Label();
			this.label24 = new System.Windows.Forms.Label();
			this.label23 = new System.Windows.Forms.Label();
			this.label22 = new System.Windows.Forms.Label();
			this.label21 = new System.Windows.Forms.Label();
			this.label20 = new System.Windows.Forms.Label();
			this.label19 = new System.Windows.Forms.Label();
			this.label18 = new System.Windows.Forms.Label();
			this.label17 = new System.Windows.Forms.Label();
			this.textBox_licence_number_cpy1 = new System.Windows.Forms.TextBox();
			this.textBox_licence_series_cpy1 = new System.Windows.Forms.TextBox();
			this.dateTimePicker_date = new System.Windows.Forms.DateTimePicker();
			this.textBox_owner_patronymic = new System.Windows.Forms.TextBox();
			this.textBox_owner_name = new System.Windows.Forms.TextBox();
			this.textBox_owner_firstname = new System.Windows.Forms.TextBox();
			this.button_select_person = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label16 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.textBox_body_no = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.textBox_frame_no = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.textBox_engine_no = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.textBox_year = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.textBox_model = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.button_save = new System.Windows.Forms.Button();
			this.button_cancel = new System.Windows.Forms.Button();
			this.button_apply = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.label1.Location = new System.Drawing.Point(8, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(168, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "Регистрационный знак";
			// 
			// textBox_number
			// 
			this.textBox_number.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox_number.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.textBox_number.Location = new System.Drawing.Point(176, 24);
			this.textBox_number.MaxLength = 6;
			this.textBox_number.Name = "textBox_number";
			this.textBox_number.Size = new System.Drawing.Size(72, 19);
			this.textBox_number.TabIndex = 1;
			this.textBox_number.Text = "";
			this.textBox_number.TextChanged += new System.EventHandler(this.textBox_number_TextChanged);
			// 
			// textBox_region
			// 
			this.textBox_region.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox_region.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.textBox_region.Location = new System.Drawing.Point(256, 24);
			this.textBox_region.MaxLength = 3;
			this.textBox_region.Name = "textBox_region";
			this.textBox_region.Size = new System.Drawing.Size(48, 19);
			this.textBox_region.TabIndex = 2;
			this.textBox_region.Text = "";
			// 
			// button_select_auto
			// 
			this.button_select_auto.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.button_select_auto.Location = new System.Drawing.Point(8, 56);
			this.button_select_auto.Name = "button_select_auto";
			this.button_select_auto.Size = new System.Drawing.Size(304, 23);
			this.button_select_auto.TabIndex = 3;
			this.button_select_auto.Text = "Идентификационный номер (VIN)";
			this.button_select_auto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.button_select_auto.Click += new System.EventHandler(this.button_select_auto_Click);
			// 
			// textBox_vin
			// 
			this.textBox_vin.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox_vin.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.textBox_vin.Location = new System.Drawing.Point(8, 88);
			this.textBox_vin.Name = "textBox_vin";
			this.textBox_vin.Size = new System.Drawing.Size(304, 19);
			this.textBox_vin.TabIndex = 4;
			this.textBox_vin.TabStop = false;
			this.textBox_vin.Text = "";
			// 
			// textBox_licence_series
			// 
			this.textBox_licence_series.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox_licence_series.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.textBox_licence_series.Location = new System.Drawing.Point(104, 496);
			this.textBox_licence_series.MaxLength = 4;
			this.textBox_licence_series.Name = "textBox_licence_series";
			this.textBox_licence_series.Size = new System.Drawing.Size(48, 19);
			this.textBox_licence_series.TabIndex = 4;
			this.textBox_licence_series.Text = "";
			this.textBox_licence_series.TextChanged += new System.EventHandler(this.textBox_licence_series_TextChanged);
			// 
			// textBox_licence_number
			// 
			this.textBox_licence_number.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox_licence_number.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.textBox_licence_number.Location = new System.Drawing.Point(160, 496);
			this.textBox_licence_number.MaxLength = 6;
			this.textBox_licence_number.Name = "textBox_licence_number";
			this.textBox_licence_number.Size = new System.Drawing.Size(72, 19);
			this.textBox_licence_number.TabIndex = 5;
			this.textBox_licence_number.Text = "";
			this.textBox_licence_number.TextChanged += new System.EventHandler(this.textBox_licence_number_TextChanged);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.textBox_licence_number_cpy2,
																					this.textBox_licence_series_cpy2,
																					this.label25,
																					this.label24,
																					this.label23,
																					this.label22,
																					this.label21,
																					this.label20,
																					this.label19,
																					this.label18,
																					this.label17,
																					this.textBox_licence_number_cpy1,
																					this.textBox_licence_series_cpy1,
																					this.dateTimePicker_date,
																					this.textBox_owner_patronymic,
																					this.textBox_owner_name,
																					this.textBox_owner_firstname,
																					this.button_select_person});
			this.groupBox1.Location = new System.Drawing.Point(336, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(320, 528);
			this.groupBox1.TabIndex = 9;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Оборотная сторона";
			// 
			// textBox_licence_number_cpy2
			// 
			this.textBox_licence_number_cpy2.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox_licence_number_cpy2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.textBox_licence_number_cpy2.Location = new System.Drawing.Point(128, 496);
			this.textBox_licence_number_cpy2.Name = "textBox_licence_number_cpy2";
			this.textBox_licence_number_cpy2.Size = new System.Drawing.Size(80, 19);
			this.textBox_licence_number_cpy2.TabIndex = 17;
			this.textBox_licence_number_cpy2.TabStop = false;
			this.textBox_licence_number_cpy2.Text = "";
			// 
			// textBox_licence_series_cpy2
			// 
			this.textBox_licence_series_cpy2.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox_licence_series_cpy2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.textBox_licence_series_cpy2.Location = new System.Drawing.Point(64, 496);
			this.textBox_licence_series_cpy2.Name = "textBox_licence_series_cpy2";
			this.textBox_licence_series_cpy2.Size = new System.Drawing.Size(48, 19);
			this.textBox_licence_series_cpy2.TabIndex = 16;
			this.textBox_licence_series_cpy2.TabStop = false;
			this.textBox_licence_series_cpy2.Text = "";
			// 
			// label25
			// 
			this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.label25.Location = new System.Drawing.Point(8, 432);
			this.label25.Name = "label25";
			this.label25.Size = new System.Drawing.Size(120, 23);
			this.label25.TabIndex = 15;
			this.label25.Text = "Выдано ГИБДД";
			// 
			// label24
			// 
			this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.label24.Location = new System.Drawing.Point(16, 384);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(136, 23);
			this.label24.TabIndex = 14;
			this.label24.Text = "Особые отметки";
			// 
			// label23
			// 
			this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.label23.Location = new System.Drawing.Point(16, 336);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(64, 23);
			this.label23.TabIndex = 13;
			this.label23.Text = "Улица";
			// 
			// label22
			// 
			this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.label22.Location = new System.Drawing.Point(208, 360);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(32, 23);
			this.label22.TabIndex = 12;
			this.label22.Text = "кв.";
			// 
			// label21
			// 
			this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.label21.Location = new System.Drawing.Point(104, 360);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(48, 23);
			this.label21.TabIndex = 11;
			this.label21.Text = "кор.";
			// 
			// label20
			// 
			this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.label20.Location = new System.Drawing.Point(16, 360);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(40, 23);
			this.label20.TabIndex = 10;
			this.label20.Text = "Дом";
			// 
			// label19
			// 
			this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.label19.Location = new System.Drawing.Point(16, 312);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(96, 23);
			this.label19.TabIndex = 9;
			this.label19.Text = "Нас. пункт";
			// 
			// label18
			// 
			this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.label18.Location = new System.Drawing.Point(16, 288);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(56, 23);
			this.label18.TabIndex = 8;
			this.label18.Text = "Район";
			// 
			// label17
			// 
			this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.label17.Location = new System.Drawing.Point(16, 232);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(192, 23);
			this.label17.TabIndex = 7;
			this.label17.Text = "Республика, край, область";
			// 
			// textBox_licence_number_cpy1
			// 
			this.textBox_licence_number_cpy1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox_licence_number_cpy1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.textBox_licence_number_cpy1.Location = new System.Drawing.Point(96, 24);
			this.textBox_licence_number_cpy1.Name = "textBox_licence_number_cpy1";
			this.textBox_licence_number_cpy1.Size = new System.Drawing.Size(80, 19);
			this.textBox_licence_number_cpy1.TabIndex = 6;
			this.textBox_licence_number_cpy1.TabStop = false;
			this.textBox_licence_number_cpy1.Text = "";
			// 
			// textBox_licence_series_cpy1
			// 
			this.textBox_licence_series_cpy1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox_licence_series_cpy1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.textBox_licence_series_cpy1.Location = new System.Drawing.Point(32, 24);
			this.textBox_licence_series_cpy1.Name = "textBox_licence_series_cpy1";
			this.textBox_licence_series_cpy1.Size = new System.Drawing.Size(48, 19);
			this.textBox_licence_series_cpy1.TabIndex = 5;
			this.textBox_licence_series_cpy1.TabStop = false;
			this.textBox_licence_series_cpy1.Text = "";
			// 
			// dateTimePicker_date
			// 
			this.dateTimePicker_date.CustomFormat = "";
			this.dateTimePicker_date.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.dateTimePicker_date.Location = new System.Drawing.Point(160, 456);
			this.dateTimePicker_date.Name = "dateTimePicker_date";
			this.dateTimePicker_date.Size = new System.Drawing.Size(128, 26);
			this.dateTimePicker_date.TabIndex = 4;
			// 
			// textBox_owner_patronymic
			// 
			this.textBox_owner_patronymic.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox_owner_patronymic.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.textBox_owner_patronymic.Location = new System.Drawing.Point(8, 184);
			this.textBox_owner_patronymic.Name = "textBox_owner_patronymic";
			this.textBox_owner_patronymic.Size = new System.Drawing.Size(200, 19);
			this.textBox_owner_patronymic.TabIndex = 3;
			this.textBox_owner_patronymic.TabStop = false;
			this.textBox_owner_patronymic.Text = "";
			// 
			// textBox_owner_name
			// 
			this.textBox_owner_name.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox_owner_name.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.textBox_owner_name.Location = new System.Drawing.Point(8, 136);
			this.textBox_owner_name.Name = "textBox_owner_name";
			this.textBox_owner_name.Size = new System.Drawing.Size(200, 19);
			this.textBox_owner_name.TabIndex = 2;
			this.textBox_owner_name.TabStop = false;
			this.textBox_owner_name.Text = "";
			// 
			// textBox_owner_firstname
			// 
			this.textBox_owner_firstname.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox_owner_firstname.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.textBox_owner_firstname.Location = new System.Drawing.Point(8, 88);
			this.textBox_owner_firstname.Name = "textBox_owner_firstname";
			this.textBox_owner_firstname.Size = new System.Drawing.Size(200, 19);
			this.textBox_owner_firstname.TabIndex = 1;
			this.textBox_owner_firstname.TabStop = false;
			this.textBox_owner_firstname.Text = "";
			// 
			// button_select_person
			// 
			this.button_select_person.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.button_select_person.Location = new System.Drawing.Point(8, 56);
			this.button_select_person.Name = "button_select_person";
			this.button_select_person.Size = new System.Drawing.Size(200, 23);
			this.button_select_person.TabIndex = 0;
			this.button_select_person.Text = "СОБСТВЕННИК (владелец)";
			this.button_select_person.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.button_select_person.Click += new System.EventHandler(this.button_select_person_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.label16,
																					this.label15,
																					this.label14,
																					this.label13,
																					this.label12,
																					this.label11,
																					this.label10,
																					this.textBox_body_no,
																					this.label9,
																					this.textBox_frame_no,
																					this.label8,
																					this.textBox_engine_no,
																					this.label7,
																					this.label6,
																					this.textBox_year,
																					this.label5,
																					this.label4,
																					this.label3,
																					this.textBox_model,
																					this.label2,
																					this.textBox_vin,
																					this.label1,
																					this.button_select_auto,
																					this.textBox_number,
																					this.textBox_region,
																					this.textBox_licence_series,
																					this.textBox_licence_number});
			this.groupBox2.Location = new System.Drawing.Point(8, 0);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(320, 528);
			this.groupBox2.TabIndex = 8;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Лицевая сторона";
			this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
			// 
			// label16
			// 
			this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.label16.Location = new System.Drawing.Point(8, 440);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(176, 23);
			this.label16.TabIndex = 26;
			this.label16.Text = "Масса без нагрузки, кг";
			// 
			// label15
			// 
			this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.label15.Location = new System.Drawing.Point(8, 416);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(208, 23);
			this.label15.TabIndex = 25;
			this.label15.Text = "Разрешенная max масса, кг";
			// 
			// label14
			// 
			this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.label14.Location = new System.Drawing.Point(176, 392);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(32, 23);
			this.label14.TabIndex = 24;
			this.label14.Text = "№";
			// 
			// label13
			// 
			this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.label13.Location = new System.Drawing.Point(8, 392);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(112, 23);
			this.label13.TabIndex = 23;
			this.label13.Text = "Паспорт серия";
			// 
			// label12
			// 
			this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.label12.Location = new System.Drawing.Point(8, 368);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(232, 23);
			this.label12.TabIndex = 22;
			this.label12.Text = "Рабочий объем двигателя, см3";
			// 
			// label11
			// 
			this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.label11.Location = new System.Drawing.Point(8, 344);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(208, 23);
			this.label11.TabIndex = 21;
			this.label11.Text = "Мощность двигателя, кВт/л.с.";
			// 
			// label10
			// 
			this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.label10.Location = new System.Drawing.Point(8, 320);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(56, 23);
			this.label10.TabIndex = 20;
			this.label10.Text = "Цвет";
			// 
			// textBox_body_no
			// 
			this.textBox_body_no.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox_body_no.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.textBox_body_no.Location = new System.Drawing.Point(144, 296);
			this.textBox_body_no.Name = "textBox_body_no";
			this.textBox_body_no.Size = new System.Drawing.Size(168, 16);
			this.textBox_body_no.TabIndex = 19;
			this.textBox_body_no.TabStop = false;
			this.textBox_body_no.Text = "";
			// 
			// label9
			// 
			this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.label9.Location = new System.Drawing.Point(8, 296);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(136, 23);
			this.label9.TabIndex = 18;
			this.label9.Text = "Кузов (коляска) №";
			// 
			// textBox_frame_no
			// 
			this.textBox_frame_no.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox_frame_no.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.textBox_frame_no.Location = new System.Drawing.Point(136, 272);
			this.textBox_frame_no.Name = "textBox_frame_no";
			this.textBox_frame_no.Size = new System.Drawing.Size(168, 16);
			this.textBox_frame_no.TabIndex = 17;
			this.textBox_frame_no.TabStop = false;
			this.textBox_frame_no.Text = "";
			// 
			// label8
			// 
			this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.label8.Location = new System.Drawing.Point(8, 272);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(128, 23);
			this.label8.TabIndex = 16;
			this.label8.Text = "Шасси (рама) №";
			// 
			// textBox_engine_no
			// 
			this.textBox_engine_no.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox_engine_no.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.textBox_engine_no.Location = new System.Drawing.Point(128, 248);
			this.textBox_engine_no.Name = "textBox_engine_no";
			this.textBox_engine_no.Size = new System.Drawing.Size(160, 15);
			this.textBox_engine_no.TabIndex = 15;
			this.textBox_engine_no.TabStop = false;
			this.textBox_engine_no.Text = "";
			// 
			// label7
			// 
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.label7.Location = new System.Drawing.Point(8, 248);
			this.label7.Name = "label7";
			this.label7.TabIndex = 14;
			this.label7.Text = "Двигатель №";
			// 
			// label6
			// 
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.label6.Location = new System.Drawing.Point(8, 224);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(152, 23);
			this.label6.TabIndex = 13;
			this.label6.Text = "Модель двигателя";
			// 
			// textBox_year
			// 
			this.textBox_year.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox_year.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.textBox_year.Location = new System.Drawing.Point(144, 200);
			this.textBox_year.Name = "textBox_year";
			this.textBox_year.Size = new System.Drawing.Size(72, 19);
			this.textBox_year.TabIndex = 12;
			this.textBox_year.TabStop = false;
			this.textBox_year.Text = "";
			// 
			// label5
			// 
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.label5.Location = new System.Drawing.Point(8, 200);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(128, 23);
			this.label5.TabIndex = 11;
			this.label5.Text = "Год выпуска ТС";
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.label4.Location = new System.Drawing.Point(8, 176);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(208, 23);
			this.label4.TabIndex = 10;
			this.label4.Text = "Категория ТС (ABCD, прицеп)";
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.label3.Location = new System.Drawing.Point(8, 152);
			this.label3.Name = "label3";
			this.label3.TabIndex = 9;
			this.label3.Text = "Тип ТС";
			// 
			// textBox_model
			// 
			this.textBox_model.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox_model.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.textBox_model.Location = new System.Drawing.Point(128, 128);
			this.textBox_model.Name = "textBox_model";
			this.textBox_model.Size = new System.Drawing.Size(184, 19);
			this.textBox_model.TabIndex = 8;
			this.textBox_model.TabStop = false;
			this.textBox_model.Text = "";
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.label2.Location = new System.Drawing.Point(8, 128);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(112, 23);
			this.label2.TabIndex = 7;
			this.label2.Text = "Марка, модель";
			// 
			// button_save
			// 
			this.button_save.Location = new System.Drawing.Point(504, 536);
			this.button_save.Name = "button_save";
			this.button_save.TabIndex = 9;
			this.button_save.Text = "Сохранить";
			this.button_save.Click += new System.EventHandler(this.button_save_Click);
			// 
			// button_cancel
			// 
			this.button_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button_cancel.Location = new System.Drawing.Point(584, 536);
			this.button_cancel.Name = "button_cancel";
			this.button_cancel.TabIndex = 10;
			this.button_cancel.Text = "Отменить";
			this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
			// 
			// button_apply
			// 
			this.button_apply.Location = new System.Drawing.Point(424, 536);
			this.button_apply.Name = "button_apply";
			this.button_apply.TabIndex = 11;
			this.button_apply.Text = "Принять";
			this.button_apply.Click += new System.EventHandler(this.button_apply_Click);
			// 
			// UIF_LicenceVehicle
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.button_cancel;
			this.ClientSize = new System.Drawing.Size(664, 565);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.button_apply,
																		  this.button_cancel,
																		  this.button_save,
																		  this.groupBox2,
																		  this.groupBox1});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "UIF_LicenceVehicle";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Свидетельство о регистрации ТС";
			this.Load += new System.EventHandler(this.UIF_LicenceVehicle_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void textBox_licence_series_TextChanged(object sender, System.EventArgs e)
		{
			if (ignore == true)
			{
				ignore = false;
				return;
			}
			string txt	= textBox_licence_series.Text;
			txt = txt.ToUpper();
			if(txt != textBox_licence_series.Text)
			{
				textBox_licence_series.Text = txt;
				textBox_licence_series.Select(txt.Length, 0);
			//	ignore = true;
			}
			if(txt.Length == 4)
			{
				textBox_licence_number.Focus();
			}
			textBox_licence_series_cpy1.Text	= txt;
			textBox_licence_series_cpy2.Text	= txt;
		}

		private void textBox_licence_number_TextChanged(object sender, System.EventArgs e)
		{
			string txt	= textBox_licence_number.Text;
			if(txt.Length == 6)
			{
				button_select_person.Focus();
			}
			textBox_licence_number_cpy1.Text	= txt;
			textBox_licence_number_cpy2.Text	= txt;
		}

		private void button_select_auto_Click(object sender, System.EventArgs e)
		{
			// Выбор автомобиля
			string mask = UserInterface.Selector_String("Часть VIN или кузова автомобиля", "");
			if (mask == "") return;
			DtAuto auto = (DtAuto)UserInterface.AutoList(0, mask, 2, UserInterface.ClickType.Select);
			if(auto == null) return;
			DtModel model = (DtModel)DbSqlModel.Find((long)auto.GetData("ССЫЛКА_КОД_АВТОМОБИЛЬ_МОДЕЛЬ"));
			if (model == null) return;

			// Успешно выбрали автомобиль, отображение для пользователся
			selected_auto			= auto;
			textBox_vin.Text		= (string)auto.GetData("VIN");
			textBox_model.Text		= (string)model.GetData("МОДЕЛЬ");
			textBox_year.Text		= (string)auto.GetData("ГОД_ВЫПУСК").ToString();
			textBox_engine_no.Text	= (string)auto.GetData("НОМЕР_ДВИГАТЕЛЬ");
			textBox_frame_no.Text	= (string)auto.GetData("НОМЕР_ШАССИ");
			textBox_body_no.Text	= (string)auto.GetData("НОМЕР_КУЗОВ");

			textBox_licence_series.Focus();
		}

		private void groupBox2_Enter(object sender, System.EventArgs e)
		{
		
		}

		private void textBox_number_TextChanged(object sender, System.EventArgs e)
		{
			// При достижении максимальной длины - автомотический переход на регион
			if (ignore == true)
			{
				ignore = false;
				return;
			}
			string txt	= textBox_number.Text;
			txt = txt.ToUpper();
			if(txt != textBox_number.Text)
			{
				textBox_number.Text = txt;
				textBox_number.Select(txt.Length, 0);
		//		ignore = true;
			}
			if(txt.Length == 6)
			{
				textBox_region.Focus();
			}
		}

		private void UIF_LicenceVehicle_Load(object sender, System.EventArgs e)
		{
			textBox_number.Focus();
		}

		private void button_select_person_Click(object sender, System.EventArgs e)
		{
			// Выбираем владельца автомобиля
			string mask = UserInterface.Selector_String("Часть фамилии владельца", "");
			if (mask == "") return;
			DtPartner partner = (DtPartner)UserInterface.PartnerList(0, mask, 2, UserInterface.ClickType.Select);
			if(partner == null) return;

			selected_partner = partner;
			if((bool)partner.GetData("ЮРИДИЧЕСКОЕ_ЛИЦО") == false)
			{
				DtPartnerPerson person = (DtPartnerPerson)partner.GetData("ФИЗИЧЕСКОЕ");
				textBox_owner_firstname.Text	= (string)person.GetData("ФАМИЛИЯ");
				textBox_owner_name.Text			= (string)person.GetData("ИМЯ");
				textBox_owner_patronymic.Text	= (string)person.GetData("ОТЧЕСТВО");
			}
			else
			{
				DtPartnerJuridical juridical	= (DtPartnerJuridical)partner.GetData("ЮРИДИЧЕСКОЕ");
				textBox_owner_firstname.Text	= (string)juridical.GetData("НАИМЕНОВАНИЕ_ЮРИДИЧЕСКОЕ");
			}

			dateTimePicker_date.Focus();
		}

		protected override void OnCreateControl()
		{
			textBox_number.Focus();
		}

		private void button_cancel_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void button_save_Click(object sender, System.EventArgs e)
		{
			// Проводим попытку проверить и сохранить введенные данные
			bool flag_new = false;
			if (licence == null)
			{
				flag_new = true;
				licence = new CS_LicenceVehicle();
			}

			if(selected_auto != null) licence.code_auto		= (long)selected_auto.GetData("КОД_АВТОМОБИЛЬ");
			if(selected_partner != null) licence.code_owner		= (long)selected_partner.GetData("КОД_КОНТРАГЕНТ");
			licence.licence_series	= textBox_licence_series.Text;
			licence.licence_number	= textBox_licence_number.Text;
			licence.date			= dateTimePicker_date.Value;
			licence.vehicle_number	= textBox_number.Text;
			licence.vehicle_region	= textBox_region.Text;

			licence.CheckElement();
			if(licence.CheckError() == false)
			{
				licence = null;
				return;
			}

			if(flag_new == true)
			{
				CS_LicenceVehicle new_licence = DbSqlLicenceVehicle.Insert(licence);
				if(new_licence == null) return;
				licence = new_licence;
				DialogResult = DialogResult.OK;
				this.Close();
			}
		}

		private void button_apply_Click(object sender, System.EventArgs e)
		{
			// Принимаем данные
			DialogResult = DialogResult.OK;
			this.Close();
		}

		public CS_LicenceVehicle Licence
		{
			get
			{
				return licence;
			}
		}

	}
}
