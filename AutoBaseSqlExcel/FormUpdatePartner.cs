using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormUpdatePartner.
	/// </summary>
	public class FormUpdatePartner : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBox_title;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TextBox textBox_comment;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox textBox_surname;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textBox_name;
		private System.Windows.Forms.TextBox textBox_patronymic;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.DateTimePicker dateTimePicker1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox textBox_address_living;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.RadioButton radioButton_person;
		private System.Windows.Forms.RadioButton radioButton_juridical;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.Button button_save;
		private System.Windows.Forms.CheckBox checkBox_birthday;
		private System.Windows.Forms.TextBox textBox_registration;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private System.Windows.Forms.Button button_select_registration;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.TextBox textBox_phone;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.TextBox textBox_inn;
		private System.Windows.Forms.TextBox textBox_contactphone;
		private System.Windows.Forms.Button button_new_phone;
		private System.Windows.Forms.Button button_remove_phone;
		private System.Windows.Forms.Button button_close;

		DtPartner			partner;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TextBox textBox_name_juridical;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.TextBox textBox_address_juridical;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Button button_address_juridical;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.TextBox textBox_address_fact;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.TextBox textBox_contacts;

		DtPartnerPerson			partner_person;
		DtPartnerJuridical		partner_juridical;

		ListView				connected_list	= null;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		ListViewItem			connected_item	= null;

		public FormUpdatePartner(long code)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			if(code == 0)
			{
				partner = new DtPartner();
				partner.SetData("ЮРИДИЧЕСКОЕ_ЛИЦО", false);
				radioButton_person.Checked = true;
				radioButton_juridical.Checked = false;
				tabControl1.SelectedTab = tabPage1;
				partner_person = new DtPartnerPerson();
				partner_juridical = new DtPartnerJuridical();
				partner.SetData("ФИЗИЧЕСКОЕ", partner_person);
			}
			else
			{
				partner = DbSqlPartner.Find(code);
				// Закрываем возможность некоторых изменений
				radioButton_person.Enabled		= false;
				radioButton_juridical.Enabled	= false;

				// Список телефонов
				// Для начала пробуем загрузить список в новом варианте
				DbSqlPartnerContact.SelectInList(listView1, code);
				if(listView1.Items.Count ==0)
				{
					if(partner.GetData("ТЕЛЕФОН").ToString().Length != 0 || partner.GetData("КОНТАКТ_ТЕЛЕФОН").ToString().Length != 0)
						MakeOldContacts();
				}

				if((bool)partner.GetData("ЮРИДИЧЕСКОЕ_ЛИЦО") == false)
				{
					// Специфические обработки для физического лица
					textBox_title.Enabled			= false;
					radioButton_person.Checked		= true;
					radioButton_juridical.Checked	= false;
					tabControl1.SelectedTab			= tabPage1;
					tabControl1.TabPages.RemoveAt(1);
				}
				else
				{
					textBox_title.Enabled			= true;
					radioButton_person.Checked		= false;
					radioButton_juridical.Checked	= true;
					tabControl1.SelectedTab			= tabPage2;
					tabControl1.TabPages.RemoveAt(0);
				}
			}
			textBox_phone.Text			= (string)partner.GetData("ТЕЛЕФОН");
			textBox_contactphone.Text	= (string)partner.GetData("КОНТАКТ_ТЕЛЕФОН");
			textBox_title.Text			= (string)partner.GetData("НАИМЕНОВАНИЕ_КРАТКОЕ");
			textBox_comment.Text		= (string)partner.GetData("КОМЕНТАРИЙ");
			textBox_inn.Text			= (string)partner.GetData("ИНН");

			if((bool)partner.GetData("ЮРИДИЧЕСКОЕ_ЛИЦО") == false)
			{
				partner_person = (DtPartnerPerson)partner.GetData("ФИЗИЧЕСКОЕ");
				textBox_surname.Text		= (string)partner_person.GetData("ФАМИЛИЯ");
				textBox_name.Text			= (string)partner_person.GetData("ИМЯ");
				textBox_patronymic.Text		= (string)partner_person.GetData("ОТЧЕСТВО");
				// НАЧАЛО - проверка на правильность краткого наименования
				string name = (string)partner_person.GetData("ИМЯ");
				if(name.Length > 0) name = name.Substring(0, 1).ToUpper() + ".";
				string patronymic = (string)partner_person.GetData("ОТЧЕСТВО");
				if(patronymic.Length > 0) patronymic = patronymic.Substring(0, 1).ToUpper() + ".";
				string right_title = (string)partner_person.GetData("ФАМИЛИЯ") + " " +  name + patronymic;
				if(right_title != (string)partner.GetData("НАИМЕНОВАНИЕ_КРАТКОЕ"))
				{
					partner.SetData("НАИМЕНОВАНИЕ_КРАТКОЕ", right_title);
				}
				textBox_title.Text			= (string)partner.GetData("НАИМЕНОВАНИЕ_КРАТКОЕ");
				// КОНЕЦ - проверка правильности краткого наименования
				textBox_registration.Text	= (string)partner_person.GetData("АДРЕС_ПРОПИСКА");
				textBox_address_living.Text	= (string)partner_person.GetData("АДРЕС_ПРОЖИВАНИЕ");
				dateTimePicker1.Value		= (DateTime)partner_person.GetData("ДАТА_РОЖДЕНИЯ");
				checkBox_birthday.Checked	= (bool)partner_person.GetData("ЕСТЬ_ДАТА_РОЖДЕНИЯ");
			}
			else
			{			
				partner_juridical = (DtPartnerJuridical)partner.GetData("ЮРИДИЧЕСКОЕ");
				textBox_name_juridical.Text		= (string)partner_juridical.GetData("НАИМЕНОВАНИЕ_ЮРИДИЧЕСКОЕ");
				textBox_address_juridical.Text	= (string)partner_juridical.GetData("АДРЕС_ЮРИДИЧЕСКИЙ");
				textBox_address_fact.Text		= (string)partner_juridical.GetData("АДРЕС_ФАКТИЧЕСКИЙ");
				textBox_contacts.Text			= (string)partner_juridical.GetData("КОНТАКТ");
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormUpdatePartner));
			this.label1 = new System.Windows.Forms.Label();
			this.textBox_title = new System.Windows.Forms.TextBox();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.textBox_address_living = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.button_select_registration = new System.Windows.Forms.Button();
			this.checkBox_birthday = new System.Windows.Forms.CheckBox();
			this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.textBox_registration = new System.Windows.Forms.TextBox();
			this.textBox_patronymic = new System.Windows.Forms.TextBox();
			this.textBox_name = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.textBox_surname = new System.Windows.Forms.TextBox();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.textBox_contacts = new System.Windows.Forms.TextBox();
			this.label15 = new System.Windows.Forms.Label();
			this.textBox_address_fact = new System.Windows.Forms.TextBox();
			this.label14 = new System.Windows.Forms.Label();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.button_address_juridical = new System.Windows.Forms.Button();
			this.label13 = new System.Windows.Forms.Label();
			this.textBox_address_juridical = new System.Windows.Forms.TextBox();
			this.textBox_name_juridical = new System.Windows.Forms.TextBox();
			this.label12 = new System.Windows.Forms.Label();
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.label9 = new System.Windows.Forms.Label();
			this.textBox_comment = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.radioButton_person = new System.Windows.Forms.RadioButton();
			this.radioButton_juridical = new System.Windows.Forms.RadioButton();
			this.button_save = new System.Windows.Forms.Button();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.label10 = new System.Windows.Forms.Label();
			this.textBox_contactphone = new System.Windows.Forms.TextBox();
			this.textBox_phone = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.textBox_inn = new System.Windows.Forms.TextBox();
			this.button_new_phone = new System.Windows.Forms.Button();
			this.button_remove_phone = new System.Windows.Forms.Button();
			this.button_close = new System.Windows.Forms.Button();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.groupBox5.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(128, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "Наименование";
			// 
			// textBox_title
			// 
			this.textBox_title.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.textBox_title.Location = new System.Drawing.Point(144, 8);
			this.textBox_title.Name = "textBox_title";
			this.textBox_title.Size = new System.Drawing.Size(432, 26);
			this.textBox_title.TabIndex = 1;
			this.textBox_title.Text = "";
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.AddRange(new System.Windows.Forms.Control[] {
																					  this.tabPage1,
																					  this.tabPage2});
			this.tabControl1.Location = new System.Drawing.Point(8, 104);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(640, 296);
			this.tabControl1.TabIndex = 2;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.AddRange(new System.Windows.Forms.Control[] {
																				   this.groupBox2,
																				   this.groupBox1});
			this.tabPage1.Location = new System.Drawing.Point(4, 25);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new System.Drawing.Size(632, 267);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Физическое Лицо";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.textBox_address_living,
																					this.label8});
			this.groupBox2.Location = new System.Drawing.Point(8, 192);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(616, 64);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Контактные данные";
			// 
			// textBox_address_living
			// 
			this.textBox_address_living.Location = new System.Drawing.Point(8, 32);
			this.textBox_address_living.Name = "textBox_address_living";
			this.textBox_address_living.Size = new System.Drawing.Size(576, 23);
			this.textBox_address_living.TabIndex = 1;
			this.textBox_address_living.Text = "";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(8, 16);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(136, 23);
			this.label8.TabIndex = 0;
			this.label8.Text = "Место проживания";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.button_select_registration,
																					this.checkBox_birthday,
																					this.dateTimePicker1,
																					this.label7,
																					this.label6,
																					this.textBox_registration,
																					this.textBox_patronymic,
																					this.textBox_name,
																					this.label5,
																					this.label4,
																					this.label3,
																					this.textBox_surname});
			this.groupBox1.Location = new System.Drawing.Point(8, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(616, 176);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Идентификационные данные";
			// 
			// button_select_registration
			// 
			this.button_select_registration.Location = new System.Drawing.Point(584, 120);
			this.button_select_registration.Name = "button_select_registration";
			this.button_select_registration.Size = new System.Drawing.Size(24, 23);
			this.button_select_registration.TabIndex = 11;
			this.button_select_registration.Text = "...";
			this.button_select_registration.Click += new System.EventHandler(this.button_select_registration_Click);
			// 
			// checkBox_birthday
			// 
			this.checkBox_birthday.Location = new System.Drawing.Point(344, 144);
			this.checkBox_birthday.Name = "checkBox_birthday";
			this.checkBox_birthday.Size = new System.Drawing.Size(128, 24);
			this.checkBox_birthday.TabIndex = 10;
			this.checkBox_birthday.Text = "Установлена";
			// 
			// dateTimePicker1
			// 
			this.dateTimePicker1.Location = new System.Drawing.Point(144, 144);
			this.dateTimePicker1.Name = "dateTimePicker1";
			this.dateTimePicker1.TabIndex = 9;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(8, 144);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(120, 23);
			this.label7.TabIndex = 8;
			this.label7.Text = "Дата рождения";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(8, 96);
			this.label6.Name = "label6";
			this.label6.TabIndex = 7;
			this.label6.Text = "Прописка";
			// 
			// textBox_registration
			// 
			this.textBox_registration.Location = new System.Drawing.Point(8, 120);
			this.textBox_registration.Name = "textBox_registration";
			this.textBox_registration.Size = new System.Drawing.Size(576, 23);
			this.textBox_registration.TabIndex = 6;
			this.textBox_registration.Text = "";
			// 
			// textBox_patronymic
			// 
			this.textBox_patronymic.Location = new System.Drawing.Point(128, 72);
			this.textBox_patronymic.Name = "textBox_patronymic";
			this.textBox_patronymic.Size = new System.Drawing.Size(400, 23);
			this.textBox_patronymic.TabIndex = 5;
			this.textBox_patronymic.Text = "";
			this.textBox_patronymic.TextChanged += new System.EventHandler(this.textBox_patronymic_TextChanged);
			// 
			// textBox_name
			// 
			this.textBox_name.Location = new System.Drawing.Point(128, 48);
			this.textBox_name.Name = "textBox_name";
			this.textBox_name.Size = new System.Drawing.Size(400, 23);
			this.textBox_name.TabIndex = 4;
			this.textBox_name.Text = "";
			this.textBox_name.TextChanged += new System.EventHandler(this.textBox_name_TextChanged);
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(8, 72);
			this.label5.Name = "label5";
			this.label5.TabIndex = 3;
			this.label5.Text = "Отчество";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 48);
			this.label4.Name = "label4";
			this.label4.TabIndex = 2;
			this.label4.Text = "Имя";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 24);
			this.label3.Name = "label3";
			this.label3.TabIndex = 1;
			this.label3.Text = "Фамилия";
			// 
			// textBox_surname
			// 
			this.textBox_surname.Location = new System.Drawing.Point(128, 24);
			this.textBox_surname.Name = "textBox_surname";
			this.textBox_surname.Size = new System.Drawing.Size(400, 23);
			this.textBox_surname.TabIndex = 0;
			this.textBox_surname.Text = "";
			this.textBox_surname.TextChanged += new System.EventHandler(this.textBox_surname_TextChanged);
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.AddRange(new System.Windows.Forms.Control[] {
																				   this.groupBox5,
																				   this.groupBox4});
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Size = new System.Drawing.Size(632, 270);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Юридическое лицо";
			// 
			// groupBox5
			// 
			this.groupBox5.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.textBox_contacts,
																					this.label15,
																					this.textBox_address_fact,
																					this.label14});
			this.groupBox5.Location = new System.Drawing.Point(8, 128);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(616, 120);
			this.groupBox5.TabIndex = 3;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "Контактные данные";
			// 
			// textBox_contacts
			// 
			this.textBox_contacts.Location = new System.Drawing.Point(8, 80);
			this.textBox_contacts.Name = "textBox_contacts";
			this.textBox_contacts.Size = new System.Drawing.Size(600, 23);
			this.textBox_contacts.TabIndex = 3;
			this.textBox_contacts.Text = "";
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(8, 56);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(144, 23);
			this.label15.TabIndex = 2;
			this.label15.Text = "Контактные лица";
			// 
			// textBox_address_fact
			// 
			this.textBox_address_fact.Location = new System.Drawing.Point(8, 32);
			this.textBox_address_fact.Name = "textBox_address_fact";
			this.textBox_address_fact.Size = new System.Drawing.Size(600, 23);
			this.textBox_address_fact.TabIndex = 1;
			this.textBox_address_fact.Text = "";
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(8, 16);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(144, 23);
			this.label14.TabIndex = 0;
			this.label14.Text = "Фактический адрес";
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.button_address_juridical,
																					this.label13,
																					this.textBox_address_juridical,
																					this.textBox_name_juridical,
																					this.label12});
			this.groupBox4.Location = new System.Drawing.Point(8, 8);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(616, 120);
			this.groupBox4.TabIndex = 2;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Идинтификационные данные";
			// 
			// button_address_juridical
			// 
			this.button_address_juridical.Location = new System.Drawing.Point(584, 88);
			this.button_address_juridical.Name = "button_address_juridical";
			this.button_address_juridical.Size = new System.Drawing.Size(24, 23);
			this.button_address_juridical.TabIndex = 4;
			this.button_address_juridical.Text = "...";
			this.button_address_juridical.Click += new System.EventHandler(this.button_address_juridical_Click);
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(8, 64);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(144, 23);
			this.label13.TabIndex = 3;
			this.label13.Text = "Юридический адрес";
			// 
			// textBox_address_juridical
			// 
			this.textBox_address_juridical.Location = new System.Drawing.Point(8, 88);
			this.textBox_address_juridical.Name = "textBox_address_juridical";
			this.textBox_address_juridical.Size = new System.Drawing.Size(576, 23);
			this.textBox_address_juridical.TabIndex = 2;
			this.textBox_address_juridical.Text = "";
			// 
			// textBox_name_juridical
			// 
			this.textBox_name_juridical.Location = new System.Drawing.Point(8, 40);
			this.textBox_name_juridical.Name = "textBox_name_juridical";
			this.textBox_name_juridical.Size = new System.Drawing.Size(600, 23);
			this.textBox_name_juridical.TabIndex = 0;
			this.textBox_name_juridical.Text = "";
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(8, 16);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(208, 23);
			this.label12.TabIndex = 1;
			this.label12.Text = "Юридическое наименование";
			// 
			// listView1
			// 
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.columnHeader1,
																						this.columnHeader2,
																						this.columnHeader3,
																						this.columnHeader4});
			this.listView1.FullRowSelect = true;
			this.listView1.Location = new System.Drawing.Point(16, 432);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(600, 112);
			this.listView1.TabIndex = 3;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Контакт";
			this.columnHeader1.Width = 213;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Тип";
			this.columnHeader2.Width = 120;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Вид";
			this.columnHeader3.Width = 120;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(16, 408);
			this.label9.Name = "label9";
			this.label9.TabIndex = 2;
			this.label9.Text = "Телефоны";
			// 
			// textBox_comment
			// 
			this.textBox_comment.Location = new System.Drawing.Point(144, 40);
			this.textBox_comment.Name = "textBox_comment";
			this.textBox_comment.Size = new System.Drawing.Size(432, 23);
			this.textBox_comment.TabIndex = 3;
			this.textBox_comment.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(104, 23);
			this.label2.TabIndex = 4;
			this.label2.Text = "Примечание";
			// 
			// radioButton_person
			// 
			this.radioButton_person.Location = new System.Drawing.Point(8, 72);
			this.radioButton_person.Name = "radioButton_person";
			this.radioButton_person.Size = new System.Drawing.Size(144, 24);
			this.radioButton_person.TabIndex = 5;
			this.radioButton_person.Text = "Физическое лицо";
			this.radioButton_person.CheckedChanged += new System.EventHandler(this.radioButton_person_CheckedChanged);
			// 
			// radioButton_juridical
			// 
			this.radioButton_juridical.Location = new System.Drawing.Point(152, 72);
			this.radioButton_juridical.Name = "radioButton_juridical";
			this.radioButton_juridical.Size = new System.Drawing.Size(160, 24);
			this.radioButton_juridical.TabIndex = 6;
			this.radioButton_juridical.Text = "Юридическое лицо";
			this.radioButton_juridical.CheckedChanged += new System.EventHandler(this.radioButton_juridical_CheckedChanged);
			// 
			// button_save
			// 
			this.button_save.Location = new System.Drawing.Point(440, 656);
			this.button_save.Name = "button_save";
			this.button_save.Size = new System.Drawing.Size(104, 23);
			this.button_save.TabIndex = 7;
			this.button_save.Text = "Сохранить";
			this.button_save.Click += new System.EventHandler(this.button_save_Click);
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.label10,
																					this.textBox_contactphone,
																					this.textBox_phone});
			this.groupBox3.Location = new System.Drawing.Point(8, 552);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(640, 80);
			this.groupBox3.TabIndex = 8;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Для старой версии";
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(8, 24);
			this.label10.Name = "label10";
			this.label10.TabIndex = 2;
			this.label10.Text = "Телефоны";
			// 
			// textBox_contactphone
			// 
			this.textBox_contactphone.Location = new System.Drawing.Point(120, 48);
			this.textBox_contactphone.Name = "textBox_contactphone";
			this.textBox_contactphone.ReadOnly = true;
			this.textBox_contactphone.Size = new System.Drawing.Size(512, 23);
			this.textBox_contactphone.TabIndex = 1;
			this.textBox_contactphone.Text = "";
			// 
			// textBox_phone
			// 
			this.textBox_phone.Location = new System.Drawing.Point(120, 24);
			this.textBox_phone.Name = "textBox_phone";
			this.textBox_phone.ReadOnly = true;
			this.textBox_phone.Size = new System.Drawing.Size(512, 23);
			this.textBox_phone.TabIndex = 0;
			this.textBox_phone.Text = "";
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(360, 72);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(48, 23);
			this.label11.TabIndex = 9;
			this.label11.Text = "ИНН";
			// 
			// textBox_inn
			// 
			this.textBox_inn.Location = new System.Drawing.Point(408, 64);
			this.textBox_inn.Name = "textBox_inn";
			this.textBox_inn.Size = new System.Drawing.Size(168, 23);
			this.textBox_inn.TabIndex = 10;
			this.textBox_inn.Text = "";
			// 
			// button_new_phone
			// 
			this.button_new_phone.Image = ((System.Drawing.Bitmap)(resources.GetObject("button_new_phone.Image")));
			this.button_new_phone.Location = new System.Drawing.Point(120, 408);
			this.button_new_phone.Name = "button_new_phone";
			this.button_new_phone.Size = new System.Drawing.Size(24, 23);
			this.button_new_phone.TabIndex = 4;
			this.button_new_phone.Click += new System.EventHandler(this.button_new_phone_Click);
			// 
			// button_remove_phone
			// 
			this.button_remove_phone.Image = ((System.Drawing.Bitmap)(resources.GetObject("button_remove_phone.Image")));
			this.button_remove_phone.Location = new System.Drawing.Point(144, 408);
			this.button_remove_phone.Name = "button_remove_phone";
			this.button_remove_phone.Size = new System.Drawing.Size(24, 23);
			this.button_remove_phone.TabIndex = 5;
			this.button_remove_phone.Click += new System.EventHandler(this.button_remove_phone_Click);
			// 
			// button_close
			// 
			this.button_close.Location = new System.Drawing.Point(568, 656);
			this.button_close.Name = "button_close";
			this.button_close.Size = new System.Drawing.Size(80, 23);
			this.button_close.TabIndex = 11;
			this.button_close.Text = "Закрыть";
			this.button_close.Click += new System.EventHandler(this.button_close_Click);
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Примечание";
			this.columnHeader4.Width = 100;
			// 
			// FormUpdatePartner
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.ClientSize = new System.Drawing.Size(664, 685);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.button_close,
																		  this.textBox_inn,
																		  this.label11,
																		  this.groupBox3,
																		  this.button_save,
																		  this.radioButton_juridical,
																		  this.radioButton_person,
																		  this.label2,
																		  this.textBox_comment,
																		  this.tabControl1,
																		  this.textBox_title,
																		  this.label1,
																		  this.listView1,
																		  this.button_new_phone,
																		  this.label9,
																		  this.button_remove_phone});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.Name = "FormUpdatePartner";
			this.Text = "Контрагент";
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.groupBox5.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void button_select_registration_Click(object sender, System.EventArgs e)
		{
			// Подбор адреса прописки
			if(Form1.kladr_form == null)
				Form1.kladr_form = new FormSelectionKladr();
			if(Form1.kladr_form.ShowDialog() != DialogResult.OK) return;
			textBox_registration.Text = Form1.kladr_form.SelectedAddress;
			
		}

		private void MakeOldContacts()
		{
			// Загрузка старых данных о контактах
			ArrayList array = DtPartnerContact.MakeContacts((string)partner.GetData("ТЕЛЕФОН"));
			if(array != null)
			{
				foreach(object o in array)
				{
					DtPartnerContact element = (DtPartnerContact)o;
					ListViewItem item = listView1.Items.Add("");
					element.SetLVItem(item);
				}
			}
				
			array = DtPartnerContact.MakeContacts((string)partner.GetData("КОНТАКТ_ТЕЛЕФОН"));
			if(array != null)
			{
				foreach(object o in array)
				{
					DtPartnerContact element = (DtPartnerContact)o;
					ListViewItem item = listView1.Items.Add("");
					element.SetLVItem(item);
				}
			}

			// И создаем список телефонов
			foreach(ListViewItem itm in listView1.Items)
			{
				if(itm.Tag != null)
				{
					DtPartnerContact element = new DtPartnerContact();
					element.SetData("КОД_КОНТАКТ", (long)itm.Tag);
					element.SetData("ССЫЛКА_КОД_КОНТРАГЕНТ_КОНТАКТ", partner.GetData("КОД_КОНТРАГЕНТ"));
					element.SetData("ТИП_КОНТАКТ", "ТЕЛЕФОН");
					element.SetData("КОНТАКТ", itm.Text);
					element = DbSqlPartnerContact.Insert(element);
					if(element == null) listView1.Items.Remove(itm);
				}
			}
		}

		private void listView1_DoubleClick(object sender, System.EventArgs e)
		{
			// Изменение существующего контакта
			long code_partner = (long)partner.GetData("КОД_КОНТРАГЕНТ");
			if(code_partner == 0) return;

			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			if(item.Tag == null) return;
			long code = (long)item.Tag;
			if(code == 0) return;
	
			FormUpdatePartnerContact dialog = new FormUpdatePartnerContact(code_partner, code);
			if(dialog.ShowDialog() != DialogResult.OK) return;
			dialog.Contact.SetLVItem(item);
		}

		private void button_new_phone_Click(object sender, System.EventArgs e)
		{
			// Новый телефон
			long code_partner = (long)partner.GetData("КОД_КОНТРАГЕНТ");
			if(code_partner == 0) return;
		
			FormUpdatePartnerContact dialog = new FormUpdatePartnerContact(code_partner, 0);
			if(dialog.ShowDialog() != DialogResult.OK) return;

			ListViewItem item = listView1.Items.Add("");
			if(item == null) return;
			dialog.Contact.SetLVItem(item);
		}

		private void button_remove_phone_Click(object sender, System.EventArgs e)
		{
			// Удаление телефона
			long code_partner = (long)partner.GetData("КОД_КОНТРАГЕНТ");
			if(code_partner == 0) return;

			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			if(item.Tag == null) return;
			long code = (long)item.Tag;
			if(code == 0) return;
	
			if(DbSqlPartnerContact.Delete(code_partner, code) == false) return;
			listView1.Items.Remove(item);
		}

		private void button_close_Click(object sender, System.EventArgs e)
		{
			// Закрываем без внесения изменений
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void textBox_surname_TextChanged(object sender, System.EventArgs e)
		{
			// Устанавливаем первую заглавную букву, если нужно и изменяем краткое наименование
			string surname	= textBox_surname.Text;
			string tmp		= DtService.FirstUpper(surname);
			if(tmp != surname)
			{
				textBox_surname.Text = tmp;
				textBox_surname.SelectionStart = tmp.Length;
				textBox_surname.SelectionLength = 0;
				return;
			}
			partner_person.SetData("ФАМИЛИЯ", textBox_surname.Text);
			textBox_title.Text = (string)partner_person.GetData("ПАРВИЛЬНОЕ_НАИМЕНОВАНИЕ_КРАТКОЕ");
		}

		private void textBox_name_TextChanged(object sender, System.EventArgs e)
		{
			// Устанавливаем первую заглавную букву, если нужно и изменяем краткое наименование
			string name	= textBox_name.Text;
			string tmp		= DtService.FirstUpper(name);
			if(tmp != name)
			{
				textBox_name.Text = tmp;
				textBox_name.SelectionStart = tmp.Length;
				textBox_name.SelectionLength = 0;
				return;
			}
			partner_person.SetData("ИМЯ", textBox_name.Text);
			textBox_title.Text = (string)partner_person.GetData("ПАРВИЛЬНОЕ_НАИМЕНОВАНИЕ_КРАТКОЕ");
		}

		private void textBox_patronymic_TextChanged(object sender, System.EventArgs e)
		{
			// Устанавливаем первую заглавную букву, если нужно и изменяем краткое наименование
			string patronymic	= textBox_patronymic.Text;
			string tmp		= DtService.FirstUpper(patronymic);
			if(tmp != patronymic)
			{
				textBox_patronymic.Text = tmp;
				textBox_patronymic.SelectionStart = tmp.Length;
				textBox_patronymic.SelectionLength = 0;
				return;
			}
			partner_person.SetData("ОТЧЕСТВО", textBox_patronymic.Text);
			textBox_title.Text = (string)partner_person.GetData("ПАРВИЛЬНОЕ_НАИМЕНОВАНИЕ_КРАТКОЕ");
		}

		private void button_save_Click(object sender, System.EventArgs e)
		{
			// Выполнение сохраннения данных
			// Общие данные
			partner.SetData("НАИМЕНОВАНИЕ_КРАТКОЕ", textBox_title.Text);
			partner.SetData("КОМЕНТАРИЙ", textBox_comment.Text);
			partner.SetData("ИНН", textBox_inn.Text);

			//Проверка общих данных
			if(partner.CheckData("НАИМЕНОВАНИЕ_КРАТКОЕ") == false) return;

			if((bool)partner.GetData("ЮРИДИЧЕСКОЕ_ЛИЦО") == false)
			{
				// Если физическое лицо - получаем данные физического лица
				partner_person.SetData("ФАМИЛИЯ", textBox_surname.Text);
				partner_person.SetData("ИМЯ", textBox_name.Text);
				partner_person.SetData("ОТЧЕСТВО", textBox_patronymic.Text);
				partner_person.SetData("ДАТА_РОЖДЕНИЯ", dateTimePicker1.Value);
				partner_person.SetData("ЕСТЬ_ДАТА_РОЖДЕНИЯ", checkBox_birthday.Checked);
				partner_person.SetData("АДРЕС_ПРОПИСКА", textBox_registration.Text);
				partner_person.SetData("АДРЕС_ПРОЖИВАНИЕ", textBox_address_living.Text);

				//Проверка данных физического лица
				if(partner_person.CheckData("ФАМИЛИЯ") == false) return;
				if(partner_person.CheckData("ИМЯ") == false) return;
				if(partner_person.CheckData("ОТЧЕСТВО") == false) return;

				partner.SetData("ФИЗИЧЕСКОЕ", partner_person);
			}
			else
			{
				// Если юридическое лицо - получаем данные юридического лица
				partner_juridical.SetData("НАИМЕНОВАНИЕ_ЮРИДИЧЕСКОЕ", textBox_name_juridical.Text);
				partner_juridical.SetData("АДРЕС_ЮРИДИЧЕСКИЙ", textBox_address_juridical.Text);
				partner_juridical.SetData("АДРЕС_ФАКТИЧЕСКИЙ", textBox_address_fact.Text);
				partner_juridical.SetData("КОНТАКТ", textBox_contacts.Text);

				//Проверка данных юридического лица
				if(partner_juridical.CheckData("НАИМЕНОВАНИЕ_ЮРИДИЧЕСКОЕ") == false) return;
				
				partner.SetData("ЮРИДИЧЕСКОЕ", partner_juridical);
			}

			if((long)partner.GetData("КОД_КОНТРАГЕНТ") != 0)
			{
				// Записываем изменения
				if(DbSqlPartner.Update(partner) != true) return;
				MessageBox.Show("Данные контрагента изменены");
				//this.DialogResult = DialogResult.OK;
			}
			else
			{
				// Поиск сходных записей, с показом найденного списка
				string pattern = "";
				if((bool)partner.GetData("ЮРИДИЧЕСКОЕ_ЛИЦО") == false)
				{
					pattern = "%" + (string)partner_person.GetData("ФАМИЛИЯ") + "%";
				}
				else
				{
					pattern = "%" + (string)partner_juridical.GetData("НАИМЕНОВАНИЕ_ЮРИДИЧЕСКОЕ") + "%";
				}
				ArrayList array = new ArrayList();
				DbSqlPartner.SelectInArray(array, pattern);
				if(array.Count > 0)
				{
					// Предлагаем возможность выбора!
					FormListPartner dialog = new FormListPartner(1, array);
					if(dialog.ShowDialog() == DialogResult.OK)
					{
						// Мы нашли то что искали!!!!
						if(dialog.SelectedCode == 0) return;
						LoadPartner(dialog.SelectedCode);
						return;
					}
				}

				DtPartner partner_new = DbSqlPartner.Insert(partner);
				if(partner_new == null) return;
				MessageBox.Show("Контрагент добавлен");
				partner = partner_new;
				// Если запись успешна... настраиваем диалоговое окно
				if((bool)partner.GetData("ЮРИДИЧЕСКОЕ_ЛИЦО") == false)
				{
					// Специфические обработки для физического лица
					textBox_title.Enabled			= false;
					radioButton_person.Checked		= true;
					radioButton_juridical.Checked	= false;
					radioButton_person.Enabled		= false;
					radioButton_juridical.Enabled	= false;
					tabControl1.SelectedTab			= tabPage1;
					tabControl1.TabPages.RemoveAt(1);
				}
				else
				{
					textBox_title.Enabled			= true;
					radioButton_person.Checked		= false;
					radioButton_juridical.Checked	= true;
					radioButton_person.Enabled		= false;
					radioButton_juridical.Enabled	= false;
					tabControl1.SelectedTab			= tabPage2;
					tabControl1.TabPages.RemoveAt(0);
				}
			}

			// Отображение изменений во внешнем листе
			if(connected_item != null)
			{
				if(connected_item.ListView != null && connected_item.ListView.IsDisposed != true)
				{
					partner.SetLVItem(connected_item);
				}
			}
			if(connected_list != null)
			{
				ListViewItem item = connected_list.Items.Add("");
				partner.SetLVItem(item);
				connected_item	= item;
				connected_list	= null;
			}
		}

		private void button_address_juridical_Click(object sender, System.EventArgs e)
		{
			// Подбор юридического адреса
			if(Form1.kladr_form == null)
				Form1.kladr_form = new FormSelectionKladr();
			if(Form1.kladr_form.ShowDialog() != DialogResult.OK) return;
			textBox_address_juridical.Text = Form1.kladr_form.SelectedAddress;
		}

		private void radioButton_person_CheckedChanged(object sender, System.EventArgs e)
		{
			if(radioButton_person.Checked == false) return;
			radioButton_juridical.Checked = false;
			partner.SetData("ЮРИДИЧЕСКОЕ_ЛИЦО", false);
			tabControl1.SelectedTab = tabPage1;
			textBox_title.Enabled			= false;
		}

		private void ClearJuridical()
		{
		}

		private void radioButton_juridical_CheckedChanged(object sender, System.EventArgs e)
		{
			if(radioButton_juridical.Checked == false) return;
			radioButton_person.Checked = false;
			partner.SetData("ЮРИДИЧЕСКОЕ_ЛИЦО", true);
			tabControl1.SelectedTab = tabPage2;
			textBox_title.Enabled			= true;
		}

		private void LoadPartner(long code)
		{
			partner = DbSqlPartner.Find(code);
			// Закрываем возможность некоторых изменений
			radioButton_person.Enabled		= false;
			radioButton_juridical.Enabled	= false;

			// Список телефонов
			// Для начала пробуем загрузить список в новом варианте
			DbSqlPartnerContact.SelectInList(listView1, code);
			if(listView1.Items.Count ==0)
			{
				if(partner.GetData("ТЕЛЕФОН").ToString().Length != 0 || partner.GetData("КОНТАКТ_ТЕЛЕФОН").ToString().Length != 0)
					MakeOldContacts();
			}

			if((bool)partner.GetData("ЮРИДИЧЕСКОЕ_ЛИЦО") == false)
			{
				// Специфические обработки для физического лица
				textBox_title.Enabled			= false;
				radioButton_person.Checked		= true;
				radioButton_juridical.Checked	= false;
				tabControl1.SelectedTab			= tabPage1;
				tabControl1.TabPages.RemoveAt(1);
			}
			else
			{
				textBox_title.Enabled			= true;
				radioButton_person.Checked		= false;
				radioButton_juridical.Checked	= true;
				tabControl1.SelectedTab			= tabPage2;
				tabControl1.TabPages.RemoveAt(0);
			}

			textBox_phone.Text			= (string)partner.GetData("ТЕЛЕФОН");
			textBox_contactphone.Text	= (string)partner.GetData("КОНТАКТ_ТЕЛЕФОН");
			textBox_title.Text			= (string)partner.GetData("НАИМЕНОВАНИЕ_КРАТКОЕ");
			textBox_comment.Text		= (string)partner.GetData("КОМЕНТАРИЙ");
			textBox_inn.Text			= (string)partner.GetData("ИНН");

			if((bool)partner.GetData("ЮРИДИЧЕСКОЕ_ЛИЦО") == false)
			{
				partner_person = (DtPartnerPerson)partner.GetData("ФИЗИЧЕСКОЕ");
				textBox_surname.Text		= (string)partner_person.GetData("ФАМИЛИЯ");
				textBox_name.Text			= (string)partner_person.GetData("ИМЯ");
				textBox_patronymic.Text		= (string)partner_person.GetData("ОТЧЕСТВО");
				// НАЧАЛО - проверка на правильность краткого наименования
				string name = (string)partner_person.GetData("ИМЯ");
				if(name.Length > 0) name = name.Substring(0, 1).ToUpper() + ".";
				string patronymic = (string)partner_person.GetData("ОТЧЕСТВО");
				if(patronymic.Length > 0) patronymic = patronymic.Substring(0, 1).ToUpper() + ".";
				string right_title = (string)partner_person.GetData("ФАМИЛИЯ") + " " +  name + patronymic;
				if(right_title != (string)partner.GetData("НАИМЕНОВАНИЕ_КРАТКОЕ"))
				{
					partner.SetData("НАИМЕНОВАНИЕ_КРАТКОЕ", right_title);
				}
				textBox_title.Text			= (string)partner.GetData("НАИМЕНОВАНИЕ_КРАТКОЕ");
				// КОНЕЦ - проверка правильности краткого наименования
				textBox_registration.Text	= (string)partner_person.GetData("АДРЕС_ПРОПИСКА");
				textBox_address_living.Text	= (string)partner_person.GetData("АДРЕС_ПРОЖИВАНИЕ");
				dateTimePicker1.Value		= (DateTime)partner_person.GetData("ДАТА_РОЖДЕНИЯ");
				checkBox_birthday.Checked	= (bool)partner_person.GetData("ЕСТЬ_ДАТА_РОЖДЕНИЯ");
			}
			else
			{			
				partner_juridical = (DtPartnerJuridical)partner.GetData("ЮРИДИЧЕСКОЕ");
				textBox_name_juridical.Text		= (string)partner_juridical.GetData("НАИМЕНОВАНИЕ_ЮРИДИЧЕСКОЕ");
				textBox_address_juridical.Text	= (string)partner_juridical.GetData("АДРЕС_ЮРИДИЧЕСКИЙ");
				textBox_address_fact.Text		= (string)partner_juridical.GetData("АДРЕС_ФАКТИЧЕСКИЙ");
				textBox_contacts.Text			= (string)partner_juridical.GetData("КОНТАКТ");
			}
		}

		public void SetConnection(ListView list)
		{
			connected_list = list;
			connected_item = null;
		}
		public void SetConnection(ListViewItem item)
		{
			connected_item = item;
			connected_list = null;
		}
	}
}
