using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormPartner.
	/// </summary>
	public class FormPartner : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxInn;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBoxAddressFact;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox textBoxFirstName;
		private System.Windows.Forms.TextBox textBoxName;
		private System.Windows.Forms.TextBox textBoxSecondName;
		private System.Windows.Forms.DateTimePicker dateTimePickerBirth;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Button buttonOk;
		private System.Windows.Forms.TextBox textBoxComment;

		private DbPartner partner;
		private System.Windows.Forms.TextBox textBoxKPP;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.TextBox textBoxAddressJuridical;
		private System.Windows.Forms.TextBox textBoxContactPhone;
		private System.Windows.Forms.TextBox textBoxNameJuridical;
		private System.Windows.Forms.TextBox textBoxContact;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.TextBox textBoxNameShort;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.TextBox textBoxAddressRegistration;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.TextBox textBoxAddressLiving;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.TextBox textBoxPhone;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox textBoxSettlementAccount;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		bool ourChange = false;


		public FormPartner(long code)
		{
			DbPartner val = (DbPartner)DbPartner.Find(code);
			if (val == null) return;
			FormPartner form = new FormPartner(val, true);
			if (form != null) form.ShowDialog();
		}
		public FormPartner(DbPartner sourcePartner, bool juridical)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			if(sourcePartner == null)
			{
				partner = new DbPartner(juridical);
			}
			else
			{
				partner = new DbPartner(sourcePartner);
			}
			textBoxComment.Text				= partner.Comment;
			// Физические лица
			textBoxFirstName.Text			= partner.FirstName;
			textBoxName.Text				= partner.Name;
			textBoxSecondName.Text			= partner.SecondName;
			dateTimePickerBirth.Value		= partner.Birth;
			textBoxAddressRegistration.Text	= partner.AddressRegistration;
			textBoxAddressLiving.Text		= partner.AddressLiving;
			textBoxPhone.Text				= partner.Phone;
			// Юридические лица
			textBoxNameJuridical.Text		= partner.NameJuridical;
			textBoxAddressJuridical.Text	= partner.AddressJuridical;
			textBoxAddressFact.Text			= partner.AddressFact;
			textBoxInn.Text					= partner.Inn;
			textBoxKPP.Text					= partner.Kpp;
			textBoxContact.Text				= partner.Contact;
			textBoxContactPhone.Text		= partner.ContactPhone;
			// Короткое имя выставляем самым последним(эффект автоматического короткого имени)
			textBoxNameShort.Text			= partner.NameShort;

			if(partner.Juridical)
			{
				textBoxFirstName.Enabled = false;
				textBoxName.Enabled = false;
				textBoxSecondName.Enabled = false;
				dateTimePickerBirth.Enabled = false;
				textBoxPhone.Enabled = false;
				textBoxAddressRegistration.Enabled = false;
				textBoxAddressLiving.Enabled = false;
				tabControl1.SelectedTab = tabPage2;
				tabControl1.TabPages.RemoveAt(0);
			}
			else
			{
				textBoxNameShort.Enabled = false;		// Для физических лиц - краткое наименование автоматом
				textBoxNameJuridical.Enabled = false;
				textBoxAddressJuridical.Enabled = false;
				textBoxAddressFact.Enabled = false;
				textBoxKPP.Enabled = false;
				textBoxInn.Enabled = false;
				textBoxContact.Enabled = false;
				textBoxContactPhone.Enabled = false;
				textBoxSettlementAccount.Enabled = false;
				tabControl1.SelectedTab = tabPage1;
				tabControl1.TabPages.RemoveAt(1);
			}
		}

		public FormPartner(DbPartner sourcePartner, bool juridical, string nameSrc)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			if(sourcePartner == null)
			{
				partner = new DbPartner(juridical);
				if(juridical)
				{
					partner.NameJuridical = nameSrc;
					textBoxNameJuridical.Focus();
				}
				else
				{
					partner.FirstName	= nameSrc;
					textBoxFirstName.Focus();
				}
			}
			else
			{
				partner = new DbPartner(sourcePartner);
			}
			textBoxComment.Text				= partner.Comment;
			// Физические лица
			textBoxFirstName.Text			= partner.FirstName;
			textBoxName.Text				= partner.Name;
			textBoxSecondName.Text			= partner.SecondName;
			dateTimePickerBirth.Value		= partner.Birth;
			textBoxAddressRegistration.Text	= partner.AddressRegistration;
			textBoxAddressLiving.Text		= partner.AddressLiving;
			textBoxPhone.Text				= partner.Phone;
			// Юридические лица
			textBoxNameJuridical.Text		= partner.NameJuridical;
			textBoxAddressJuridical.Text	= partner.AddressJuridical;
			textBoxAddressFact.Text			= partner.AddressFact;
			textBoxInn.Text					= partner.Inn;
			textBoxKPP.Text					= partner.Kpp;
			textBoxContact.Text				= partner.Contact;
			textBoxContactPhone.Text		= partner.ContactPhone;
			// Короткое имя выставляем самым последним(эффект автоматического короткого имени)
			textBoxNameShort.Text			= partner.NameShort;

			if(partner.Juridical)
			{
				textBoxFirstName.Enabled = false;
				textBoxName.Enabled = false;
				textBoxSecondName.Enabled = false;
				dateTimePickerBirth.Enabled = false;
				textBoxPhone.Enabled = false;
				textBoxAddressRegistration.Enabled = false;
				textBoxAddressLiving.Enabled = false;
				tabControl1.SelectedTab = tabPage2;
				tabControl1.TabPages.RemoveAt(0);
			}
			else
			{
				textBoxNameShort.Enabled = false;		// Для физических лиц - краткое наименование автоматом
				textBoxNameJuridical.Enabled = false;
				textBoxAddressJuridical.Enabled = false;
				textBoxAddressFact.Enabled = false;
				textBoxKPP.Enabled = false;
				textBoxInn.Enabled = false;
				textBoxContact.Enabled = false;
				textBoxContactPhone.Enabled = false;
				textBoxSettlementAccount.Enabled = false;
				tabControl1.SelectedTab = tabPage1;
				tabControl1.TabPages.RemoveAt(1);
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
			this.textBoxInn = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.textBoxAddressJuridical = new System.Windows.Forms.TextBox();
			this.textBoxAddressFact = new System.Windows.Forms.TextBox();
			this.textBoxContactPhone = new System.Windows.Forms.TextBox();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.textBoxPhone = new System.Windows.Forms.TextBox();
			this.label16 = new System.Windows.Forms.Label();
			this.textBoxAddressLiving = new System.Windows.Forms.TextBox();
			this.label15 = new System.Windows.Forms.Label();
			this.textBoxAddressRegistration = new System.Windows.Forms.TextBox();
			this.label14 = new System.Windows.Forms.Label();
			this.dateTimePickerBirth = new System.Windows.Forms.DateTimePicker();
			this.textBoxSecondName = new System.Windows.Forms.TextBox();
			this.textBoxName = new System.Windows.Forms.TextBox();
			this.textBoxFirstName = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.textBoxSettlementAccount = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.textBoxNameJuridical = new System.Windows.Forms.TextBox();
			this.label12 = new System.Windows.Forms.Label();
			this.textBoxContact = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.textBoxKPP = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.textBoxComment = new System.Windows.Forms.TextBox();
			this.buttonOk = new System.Windows.Forms.Button();
			this.label13 = new System.Windows.Forms.Label();
			this.textBoxNameShort = new System.Windows.Forms.TextBox();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 56);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(72, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "ИНН/КПП";
			// 
			// textBoxInn
			// 
			this.textBoxInn.Location = new System.Drawing.Point(144, 56);
			this.textBoxInn.Name = "textBoxInn";
			this.textBoxInn.Size = new System.Drawing.Size(128, 23);
			this.textBoxInn.TabIndex = 1;
			this.textBoxInn.Text = "textBox1";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(0, 120);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(144, 23);
			this.label2.TabIndex = 2;
			this.label2.Text = "Юридический адрес";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(0, 144);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(136, 23);
			this.label3.TabIndex = 3;
			this.label3.Text = "Фактический адрес";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 216);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(112, 23);
			this.label4.TabIndex = 4;
			this.label4.Text = "Телефоны";
			// 
			// textBoxAddressJuridical
			// 
			this.textBoxAddressJuridical.Location = new System.Drawing.Point(144, 120);
			this.textBoxAddressJuridical.Name = "textBoxAddressJuridical";
			this.textBoxAddressJuridical.Size = new System.Drawing.Size(472, 23);
			this.textBoxAddressJuridical.TabIndex = 5;
			this.textBoxAddressJuridical.Text = "textBox1";
			this.textBoxAddressJuridical.KeyDown += new KeyEventHandler(this.textBoxAddressJuridical_KeyDown);
			// 
			// textBoxAddressFact
			// 
			this.textBoxAddressFact.Location = new System.Drawing.Point(144, 144);
			this.textBoxAddressFact.Name = "textBoxAddressFact";
			this.textBoxAddressFact.Size = new System.Drawing.Size(472, 23);
			this.textBoxAddressFact.TabIndex = 6;
			this.textBoxAddressFact.Text = "textBox1";
			// 
			// textBoxContactPhone
			// 
			this.textBoxContactPhone.Location = new System.Drawing.Point(136, 216);
			this.textBoxContactPhone.Name = "textBoxContactPhone";
			this.textBoxContactPhone.Size = new System.Drawing.Size(464, 23);
			this.textBoxContactPhone.TabIndex = 7;
			this.textBoxContactPhone.Text = "textBox1";
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.AddRange(new System.Windows.Forms.Control[] {
																					  this.tabPage1,
																					  this.tabPage2});
			this.tabControl1.Location = new System.Drawing.Point(8, 64);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(640, 304);
			this.tabControl1.TabIndex = 8;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.AddRange(new System.Windows.Forms.Control[] {
																				   this.textBoxPhone,
																				   this.label16,
																				   this.textBoxAddressLiving,
																				   this.label15,
																				   this.textBoxAddressRegistration,
																				   this.label14,
																				   this.dateTimePickerBirth,
																				   this.textBoxSecondName,
																				   this.textBoxName,
																				   this.textBoxFirstName,
																				   this.label8,
																				   this.label7,
																				   this.label6,
																				   this.label5});
			this.tabPage1.Location = new System.Drawing.Point(4, 25);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new System.Drawing.Size(632, 275);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Физическое лицо";
			// 
			// textBoxPhone
			// 
			this.textBoxPhone.Location = new System.Drawing.Point(136, 160);
			this.textBoxPhone.Name = "textBoxPhone";
			this.textBoxPhone.Size = new System.Drawing.Size(480, 23);
			this.textBoxPhone.TabIndex = 13;
			this.textBoxPhone.Text = "";
			// 
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(8, 160);
			this.label16.Name = "label16";
			this.label16.TabIndex = 12;
			this.label16.Text = "Телефоны";
			// 
			// textBoxAddressLiving
			// 
			this.textBoxAddressLiving.Location = new System.Drawing.Point(136, 136);
			this.textBoxAddressLiving.Name = "textBoxAddressLiving";
			this.textBoxAddressLiving.Size = new System.Drawing.Size(480, 23);
			this.textBoxAddressLiving.TabIndex = 11;
			this.textBoxAddressLiving.Text = "";
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(8, 136);
			this.label15.Name = "label15";
			this.label15.TabIndex = 10;
			this.label15.Text = "Проживает";
			// 
			// textBoxAddressRegistration
			// 
			this.textBoxAddressRegistration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBoxAddressRegistration.Location = new System.Drawing.Point(136, 112);
			this.textBoxAddressRegistration.Name = "textBoxAddressRegistration";
			this.textBoxAddressRegistration.Size = new System.Drawing.Size(480, 23);
			this.textBoxAddressRegistration.TabIndex = 9;
			this.textBoxAddressRegistration.Text = "";
			this.textBoxAddressRegistration.KeyDown += new KeyEventHandler(this.textBoxAddressRegistration_KeyDown);
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(8, 112);
			this.label14.Name = "label14";
			this.label14.TabIndex = 8;
			this.label14.Text = "Прописка";
			// 
			// dateTimePickerBirth
			// 
			this.dateTimePickerBirth.Location = new System.Drawing.Point(136, 80);
			this.dateTimePickerBirth.Name = "dateTimePickerBirth";
			this.dateTimePickerBirth.TabIndex = 7;
			// 
			// textBoxSecondName
			// 
			this.textBoxSecondName.Location = new System.Drawing.Point(136, 56);
			this.textBoxSecondName.Name = "textBoxSecondName";
			this.textBoxSecondName.Size = new System.Drawing.Size(288, 23);
			this.textBoxSecondName.TabIndex = 6;
			this.textBoxSecondName.Text = "textBox1";
			this.textBoxSecondName.TextChanged += new System.EventHandler(this.textBoxSecondName_TextChanged);
			// 
			// textBoxName
			// 
			this.textBoxName.Location = new System.Drawing.Point(136, 32);
			this.textBoxName.Name = "textBoxName";
			this.textBoxName.Size = new System.Drawing.Size(288, 23);
			this.textBoxName.TabIndex = 5;
			this.textBoxName.Text = "textBox1";
			this.textBoxName.TextChanged += new System.EventHandler(this.textBoxName_TextChanged);
			// 
			// textBoxFirstName
			// 
			this.textBoxFirstName.Location = new System.Drawing.Point(136, 8);
			this.textBoxFirstName.Name = "textBoxFirstName";
			this.textBoxFirstName.Size = new System.Drawing.Size(288, 23);
			this.textBoxFirstName.TabIndex = 4;
			this.textBoxFirstName.Text = "textBox1";
			this.textBoxFirstName.TextChanged += new System.EventHandler(this.textBoxFirstName_TextChanged);
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(8, 80);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(120, 23);
			this.label8.TabIndex = 3;
			this.label8.Text = "Дата рождения";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(8, 56);
			this.label7.Name = "label7";
			this.label7.TabIndex = 2;
			this.label7.Text = "Отчество";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(8, 32);
			this.label6.Name = "label6";
			this.label6.TabIndex = 1;
			this.label6.Text = "Имя";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(8, 8);
			this.label5.Name = "label5";
			this.label5.TabIndex = 0;
			this.label5.Text = "Фамилия";
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.AddRange(new System.Windows.Forms.Control[] {
																				   this.textBoxSettlementAccount,
																				   this.label10,
																				   this.textBoxNameJuridical,
																				   this.label12,
																				   this.textBoxContact,
																				   this.label11,
																				   this.textBoxKPP,
																				   this.textBoxInn,
																				   this.label1,
																				   this.textBoxAddressJuridical,
																				   this.label2,
																				   this.textBoxAddressFact,
																				   this.label3,
																				   this.label4,
																				   this.textBoxContactPhone});
			this.tabPage2.Location = new System.Drawing.Point(4, 25);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Size = new System.Drawing.Size(632, 275);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Юридическое лицо";
			// 
			// textBoxSettlementAccount
			// 
			this.textBoxSettlementAccount.Location = new System.Drawing.Point(144, 88);
			this.textBoxSettlementAccount.Name = "textBoxSettlementAccount";
			this.textBoxSettlementAccount.Size = new System.Drawing.Size(472, 23);
			this.textBoxSettlementAccount.TabIndex = 9;
			this.textBoxSettlementAccount.Text = "";
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(8, 88);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(120, 23);
			this.label10.TabIndex = 8;
			this.label10.Text = "Расчетный счет";
			// 
			// textBoxNameJuridical
			// 
			this.textBoxNameJuridical.Location = new System.Drawing.Point(8, 24);
			this.textBoxNameJuridical.Name = "textBoxNameJuridical";
			this.textBoxNameJuridical.Size = new System.Drawing.Size(616, 23);
			this.textBoxNameJuridical.TabIndex = 5;
			this.textBoxNameJuridical.Text = "";
			this.textBoxNameJuridical.TextChanged += new System.EventHandler(this.textBoxNameJuridical_TextChanged);
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(8, 8);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(216, 23);
			this.label12.TabIndex = 4;
			this.label12.Text = "Юридическое наименование";
			// 
			// textBoxContact
			// 
			this.textBoxContact.Location = new System.Drawing.Point(136, 184);
			this.textBoxContact.Name = "textBoxContact";
			this.textBoxContact.Size = new System.Drawing.Size(464, 23);
			this.textBoxContact.TabIndex = 3;
			this.textBoxContact.Text = "textBox1";
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(8, 184);
			this.label11.Name = "label11";
			this.label11.TabIndex = 2;
			this.label11.Text = "Контакты";
			// 
			// textBoxKPP
			// 
			this.textBoxKPP.Location = new System.Drawing.Point(304, 56);
			this.textBoxKPP.Name = "textBoxKPP";
			this.textBoxKPP.TabIndex = 1;
			this.textBoxKPP.Text = "textBox1";
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(16, 32);
			this.label9.Name = "label9";
			this.label9.TabIndex = 9;
			this.label9.Text = "Примечания";
			// 
			// textBoxComment
			// 
			this.textBoxComment.Location = new System.Drawing.Point(184, 32);
			this.textBoxComment.Name = "textBoxComment";
			this.textBoxComment.Size = new System.Drawing.Size(376, 23);
			this.textBoxComment.TabIndex = 10;
			this.textBoxComment.Text = "textBox1";
			// 
			// buttonOk
			// 
			this.buttonOk.Location = new System.Drawing.Point(280, 376);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.TabIndex = 11;
			this.buttonOk.Text = "ОК";
			this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(16, 8);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(160, 23);
			this.label13.TabIndex = 12;
			this.label13.Text = "Краткое наименование";
			// 
			// textBoxNameShort
			// 
			this.textBoxNameShort.Location = new System.Drawing.Point(184, 8);
			this.textBoxNameShort.Name = "textBoxNameShort";
			this.textBoxNameShort.Size = new System.Drawing.Size(376, 23);
			this.textBoxNameShort.TabIndex = 13;
			this.textBoxNameShort.Text = "";
			// 
			// FormPartner
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.ClientSize = new System.Drawing.Size(658, 403);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.textBoxNameShort,
																		  this.label13,
																		  this.buttonOk,
																		  this.textBoxComment,
																		  this.label9,
																		  this.tabControl1});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Name = "FormPartner";
			this.Text = "Контрагент";
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonOk_Click(object sender, System.EventArgs e)
		{
			// Получение измененных данных
			partner.NameShort	= textBoxNameShort.Text;
			partner.Comment		= textBoxComment.Text;	
			if(partner.Juridical == false)
			{
				partner.FirstName = textBoxFirstName.Text;
				partner.Name = textBoxName.Text;
				partner.SecondName = textBoxSecondName.Text;
				partner.Birth = dateTimePickerBirth.Value;
				partner.Phone = textBoxPhone.Text;
				partner.AddressRegistration = textBoxAddressRegistration.Text;
				partner.AddressLiving = textBoxAddressLiving.Text;
			}
			else
			{
				partner.NameJuridical = textBoxNameJuridical.Text;
				partner.AddressJuridical = textBoxAddressJuridical.Text;
				partner.AddressFact = textBoxAddressFact.Text;
				partner.Inn = textBoxInn.Text;
				partner.Kpp = textBoxKPP.Text;
				partner.Contact = textBoxContact.Text;
				partner.ContactPhone = textBoxContactPhone.Text;
			}
			if(Db.ShowFaults()) return;

			// Только в случае добавления!!!!
			// Прежде чем записывать контрагента - проверим на наличие похожих
			if(partner.Adding == true)
			{
				ArrayList partners = new ArrayList();
				if(partner.Juridical)
					DbPartner.FillArray(partners, partner.Juridical, 2, partner.NameJuridical);
				else
					DbPartner.FillArray(partners, partner.Juridical, 2, partner.FirstName);
				if (partners.Count > 0)
				{
					// Запрос на добавление
					FormInfo dialog = new FormInfo(null, 0);
					foreach(object o in partners)
					{
						DbPartner element = (DbPartner)o;
						dialog.InsertInfo(element);
					}
					dialog.ShowDialog();
					DialogResult result = MessageBox.Show("В базе есть похожие элементы, добавить?", "Запрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
					if(result != DialogResult.Yes)
					{
						dialog.Close();
						return;
					}
				}
			}

			if(partner.Write() != true)
			{
				Db.ShowFaults();
				return;
			}
			this.DialogResult = DialogResult.OK;
			this.Close();
			return;
		}

		public DbPartner Partner
		{
			get
			{
				return partner;
			}
		}

		protected void textBoxFirstName_TextChanged(object sender, EventArgs e)
		{
			if (ourChange == true)
			{
				ourChange = false;
				return;
			}
			// Первым делом ставим заглавной первую букву
			string tmpText = Db.FirstUpper(textBoxFirstName.Text);
			if(tmpText != textBoxFirstName.Text)
			{
				// Если есть изменения - производим их
				ourChange = true;
				int selectionStart = textBoxFirstName.SelectionStart;
				if(selectionStart == -1)
				{
					selectionStart = textBoxFirstName.Text.Length;
				}
				textBoxFirstName.Text = tmpText;
				textBoxFirstName.SelectionStart = selectionStart;
			}
			// Устанавливаем новое значение для короткого имени контрагента
			SetShortName();
		}

		protected void textBoxSecondName_TextChanged(object sender, EventArgs e)
		{
			if (ourChange == true)
			{
				ourChange = false;
				return;
			}
			// Первым делом ставим заглавной первую букву
			string tmpText = Db.FirstUpper(textBoxSecondName.Text);
			if(tmpText != textBoxSecondName.Text)
			{
				// Если есть изменения - производим их
				ourChange = true;
				int selectionStart = textBoxSecondName.SelectionStart;
				textBoxSecondName.Text = tmpText;
				textBoxSecondName.SelectionStart = selectionStart;
			}
			// Устанавливаем новое значение для короткого имени контрагента
			SetShortName();
		}

		protected void textBoxName_TextChanged(object sender, EventArgs e)
		{
			if (ourChange == true)
			{
				ourChange = false;
				return;
			}
			// Первым делом ставим заглавной первую букву
			string tmpText = Db.FirstUpper(textBoxName.Text);
			if(tmpText != textBoxName.Text)
			{
				// Если есть изменения - производим их
				ourChange = true;
				int selectionStart = textBoxName.SelectionStart;
				textBoxName.Text = tmpText;
				if(selectionStart > 0)
					textBoxName.SelectionStart = selectionStart;
			}
			// Устанавливаем новое значение для короткого имени контрагента
			SetShortName();
		}

		protected void SetShortName()
		{
			string text = "";
			string tmpText;
			text = textBoxFirstName.Text;
			if(text.Trim().Length == 0)
			{
				textBoxNameShort.Text = "";
				return;
			}
			tmpText = textBoxName.Text.Trim();
			if(tmpText.Length != 0) text += " " + Db.Initial(textBoxName.Text) + Db.Initial(textBoxSecondName.Text);
			textBoxNameShort.Text = text;
		}

		protected void textBoxNameJuridical_TextChanged(object sender, EventArgs e)
		{
			if (partner.Adding == false) return;
			SetShortNameJuridical();
		}
		protected void SetShortNameJuridical()
		{
			textBoxNameShort.Text = textBoxNameJuridical.Text;
		}

		private void textBoxAddressRegistration_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Enter)
			{
				// Ввод адреса через кладр
				FormKladr dialog = new FormKladr();
				dialog.ShowDialog();
				if(dialog.DialogResult != DialogResult.OK) return;
				textBoxAddressRegistration.Text = dialog.Address;
			}
		}

		private void textBoxAddressJuridical_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Enter)
			{
				// Ввод адреса через кладр
				FormKladr dialog = new FormKladr();
				dialog.ShowDialog();
				if(dialog.DialogResult != DialogResult.OK) return;
				textBoxAddressJuridical.Text = dialog.Address;
			}
		}
	}
}
