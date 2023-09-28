using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormSell.
	/// </summary>
	public class FormSell : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox textBoxModel;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBoxVin;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBoxPartner;
		private System.Windows.Forms.Button buttonSelectPartner;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.DateTimePicker dateTimePickerSell;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textBoxPriceAuto;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textBoxPriceOption;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox textBoxComment;
		private System.Windows.Forms.Button buttonSell;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		DbAutoStorage autoStorage;
		private System.Windows.Forms.TextBox textBoxDocument;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.CheckBox checkBoxCashless;
		private System.Windows.Forms.CheckBox checkBoxCredit;
		private System.Windows.Forms.TextBox textBoxCredit;
		private System.Windows.Forms.CheckBox checkBoxTradeIn;
		private System.Windows.Forms.TextBox textBoxTradein;
		DbAutoSell autoSell;

		DbAuto tradeinAuto			= null;
		DbCreditBank creditBank		= null;

		public FormSell(DbAutoStorage src)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			if(src == null)
			{ 
				buttonSell.Visible = false;
				return;
			}
			autoStorage		= new DbAutoStorage(src);
			autoSell		= new DbAutoSell(autoStorage);
			// Настройка вида
			if (autoSell.Partner != null)
			{
				// Покупатель уже выбран
				buttonSelectPartner.Visible = false;
				textBoxPartner.Text = autoSell.PartnerTxt;
			}
			textBoxModel.Text		= autoSell.AutoModelTxt;
			textBoxVin.Text			= autoSell.VinTxt;
			textBoxPriceAuto.Text	= autoSell.PriceAuto.ToString();;
			textBoxPriceOption.Text	= autoSell.PriceOption.ToString();
			textBoxComment.Text		= autoSell.Comment;
			textBoxDocument.Text	= autoSell.Document;

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
			this.textBoxModel = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.textBoxVin = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.buttonSelectPartner = new System.Windows.Forms.Button();
			this.textBoxPartner = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.label8 = new System.Windows.Forms.Label();
			this.textBoxDocument = new System.Windows.Forms.TextBox();
			this.textBoxComment = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.textBoxPriceOption = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.textBoxPriceAuto = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.dateTimePickerSell = new System.Windows.Forms.DateTimePicker();
			this.label4 = new System.Windows.Forms.Label();
			this.buttonSell = new System.Windows.Forms.Button();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.textBoxTradein = new System.Windows.Forms.TextBox();
			this.checkBoxTradeIn = new System.Windows.Forms.CheckBox();
			this.textBoxCredit = new System.Windows.Forms.TextBox();
			this.checkBoxCredit = new System.Windows.Forms.CheckBox();
			this.checkBoxCashless = new System.Windows.Forms.CheckBox();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.SuspendLayout();
			// 
			// textBoxModel
			// 
			this.textBoxModel.Location = new System.Drawing.Point(128, 24);
			this.textBoxModel.Name = "textBoxModel";
			this.textBoxModel.ReadOnly = true;
			this.textBoxModel.Size = new System.Drawing.Size(264, 23);
			this.textBoxModel.TabIndex = 0;
			this.textBoxModel.Text = "";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.textBoxVin,
																					this.label2,
																					this.label1,
																					this.textBoxModel});
			this.groupBox1.Location = new System.Drawing.Point(8, 16);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(592, 88);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Автомобиль";
			// 
			// textBoxVin
			// 
			this.textBoxVin.Location = new System.Drawing.Point(128, 48);
			this.textBoxVin.Name = "textBoxVin";
			this.textBoxVin.ReadOnly = true;
			this.textBoxVin.Size = new System.Drawing.Size(264, 23);
			this.textBoxVin.TabIndex = 3;
			this.textBoxVin.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 48);
			this.label2.Name = "label2";
			this.label2.TabIndex = 2;
			this.label2.Text = "VIN";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 24);
			this.label1.Name = "label1";
			this.label1.TabIndex = 1;
			this.label1.Text = "Модель";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.buttonSelectPartner,
																					this.textBoxPartner,
																					this.label3});
			this.groupBox2.Location = new System.Drawing.Point(8, 120);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(592, 64);
			this.groupBox2.TabIndex = 2;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Покупатель";
			// 
			// buttonSelectPartner
			// 
			this.buttonSelectPartner.Location = new System.Drawing.Point(560, 24);
			this.buttonSelectPartner.Name = "buttonSelectPartner";
			this.buttonSelectPartner.Size = new System.Drawing.Size(24, 23);
			this.buttonSelectPartner.TabIndex = 2;
			this.buttonSelectPartner.Text = "...";
			this.buttonSelectPartner.Click += new System.EventHandler(this.buttonSelectPartner_Click);
			// 
			// textBoxPartner
			// 
			this.textBoxPartner.Location = new System.Drawing.Point(128, 24);
			this.textBoxPartner.Name = "textBoxPartner";
			this.textBoxPartner.ReadOnly = true;
			this.textBoxPartner.Size = new System.Drawing.Size(432, 23);
			this.textBoxPartner.TabIndex = 1;
			this.textBoxPartner.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 24);
			this.label3.Name = "label3";
			this.label3.TabIndex = 0;
			this.label3.Text = "Покупатель";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.label8,
																					this.textBoxDocument,
																					this.textBoxComment,
																					this.label7,
																					this.textBoxPriceOption,
																					this.label6,
																					this.textBoxPriceAuto,
																					this.label5,
																					this.dateTimePickerSell,
																					this.label4});
			this.groupBox3.Location = new System.Drawing.Point(8, 200);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(592, 176);
			this.groupBox3.TabIndex = 3;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Продажа";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(16, 32);
			this.label8.Name = "label8";
			this.label8.TabIndex = 9;
			this.label8.Text = "Документ";
			// 
			// textBoxDocument
			// 
			this.textBoxDocument.Location = new System.Drawing.Point(128, 32);
			this.textBoxDocument.Name = "textBoxDocument";
			this.textBoxDocument.Size = new System.Drawing.Size(440, 23);
			this.textBoxDocument.TabIndex = 8;
			this.textBoxDocument.Text = "";
			// 
			// textBoxComment
			// 
			this.textBoxComment.Location = new System.Drawing.Point(128, 144);
			this.textBoxComment.Name = "textBoxComment";
			this.textBoxComment.Size = new System.Drawing.Size(456, 23);
			this.textBoxComment.TabIndex = 12;
			this.textBoxComment.Text = "";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(16, 144);
			this.label7.Name = "label7";
			this.label7.TabIndex = 6;
			this.label7.Text = "Примечание";
			// 
			// textBoxPriceOption
			// 
			this.textBoxPriceOption.Location = new System.Drawing.Point(432, 104);
			this.textBoxPriceOption.Name = "textBoxPriceOption";
			this.textBoxPriceOption.Size = new System.Drawing.Size(112, 23);
			this.textBoxPriceOption.TabIndex = 11;
			this.textBoxPriceOption.Text = "";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(288, 104);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(136, 23);
			this.label6.TabIndex = 4;
			this.label6.Text = "Доп. оборудования";
			// 
			// textBoxPriceAuto
			// 
			this.textBoxPriceAuto.Location = new System.Drawing.Point(152, 104);
			this.textBoxPriceAuto.Name = "textBoxPriceAuto";
			this.textBoxPriceAuto.Size = new System.Drawing.Size(120, 23);
			this.textBoxPriceAuto.TabIndex = 10;
			this.textBoxPriceAuto.Text = "";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(16, 112);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(136, 23);
			this.label5.TabIndex = 2;
			this.label5.Text = "Цена автомобиля";
			// 
			// dateTimePickerSell
			// 
			this.dateTimePickerSell.Location = new System.Drawing.Point(128, 64);
			this.dateTimePickerSell.Name = "dateTimePickerSell";
			this.dateTimePickerSell.Size = new System.Drawing.Size(160, 23);
			this.dateTimePickerSell.TabIndex = 9;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(16, 64);
			this.label4.Name = "label4";
			this.label4.TabIndex = 0;
			this.label4.Text = "Дата";
			// 
			// buttonSell
			// 
			this.buttonSell.Location = new System.Drawing.Point(264, 480);
			this.buttonSell.Name = "buttonSell";
			this.buttonSell.TabIndex = 4;
			this.buttonSell.Text = "Продать";
			this.buttonSell.Click += new System.EventHandler(this.buttonSell_Click);
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.textBoxTradein,
																					this.checkBoxTradeIn,
																					this.textBoxCredit,
																					this.checkBoxCredit,
																					this.checkBoxCashless});
			this.groupBox4.Location = new System.Drawing.Point(8, 384);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(592, 88);
			this.groupBox4.TabIndex = 5;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Свойства продажи";
			// 
			// textBoxTradein
			// 
			this.textBoxTradein.Location = new System.Drawing.Point(264, 56);
			this.textBoxTradein.Name = "textBoxTradein";
			this.textBoxTradein.ReadOnly = true;
			this.textBoxTradein.Size = new System.Drawing.Size(312, 23);
			this.textBoxTradein.TabIndex = 4;
			this.textBoxTradein.Text = "";
			// 
			// checkBoxTradeIn
			// 
			this.checkBoxTradeIn.Location = new System.Drawing.Point(152, 56);
			this.checkBoxTradeIn.Name = "checkBoxTradeIn";
			this.checkBoxTradeIn.TabIndex = 3;
			this.checkBoxTradeIn.Text = "Обмен";
			this.checkBoxTradeIn.CheckedChanged += new System.EventHandler(this.checkBoxTradeIn_CheckedChanged);
			// 
			// textBoxCredit
			// 
			this.textBoxCredit.Location = new System.Drawing.Point(264, 24);
			this.textBoxCredit.Name = "textBoxCredit";
			this.textBoxCredit.ReadOnly = true;
			this.textBoxCredit.Size = new System.Drawing.Size(312, 23);
			this.textBoxCredit.TabIndex = 2;
			this.textBoxCredit.Text = "";
			// 
			// checkBoxCredit
			// 
			this.checkBoxCredit.Location = new System.Drawing.Point(152, 24);
			this.checkBoxCredit.Name = "checkBoxCredit";
			this.checkBoxCredit.TabIndex = 1;
			this.checkBoxCredit.Text = "Кредит";
			this.checkBoxCredit.CheckedChanged += new System.EventHandler(this.checkBoxCredit_CheckedChanged);
			// 
			// checkBoxCashless
			// 
			this.checkBoxCashless.Location = new System.Drawing.Point(8, 24);
			this.checkBoxCashless.Name = "checkBoxCashless";
			this.checkBoxCashless.TabIndex = 0;
			this.checkBoxCashless.Text = "Безнал";
			this.checkBoxCashless.CheckedChanged += new System.EventHandler(this.checkBoxCashless_CheckedChanged);
			// 
			// FormSell
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.ClientSize = new System.Drawing.Size(608, 511);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.groupBox4,
																		  this.buttonSell,
																		  this.groupBox3,
																		  this.groupBox2,
																		  this.groupBox1});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "FormSell";
			this.Text = "Продажа автомобиля";
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonSell_Click(object sender, System.EventArgs e)
		{
			// Продать выбранный автомобиль
			autoSell.Comment		= textBoxComment.Text;
			autoSell.PriceAutoTxt	= textBoxPriceAuto.Text;
			autoSell.PriceOptionTxt	= textBoxPriceOption.Text;
			autoSell.DateSell		= dateTimePickerSell.Value;
			autoSell.Document		= textBoxDocument.Text;

			// Предупреждение о цене
			if(autoSell.Price <= 1000)
			{
				if(MessageBox.Show(this,"Низка цена автомобиля, уверены?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes) return;
			}

			// Свойства продажи
			autoSell.Cashless		= checkBoxCashless.Checked;
			if(tradeinAuto != null)
				autoSell.Tradein	= tradeinAuto.Code;
			else
				autoSell.Tradein	= 0;
			if(creditBank != null)
				autoSell.Credit	= creditBank.Code;
			else
				autoSell.Credit	= 0;

			if(Db.ShowFaults() == true) return;

			if(autoSell.Write() != true)
			{
				Db.ShowFaults();
				return;
			}

			this.DialogResult	= DialogResult.OK;
			this.Close();
		}

		private void buttonSelectPartner_Click(object sender, System.EventArgs e)
		{
			// Выбор покупателя, если можно
			FormPartnerList dialog = new FormPartnerList();
			dialog.ShowDialog();
			if(dialog.DialogResult != DialogResult.OK) return;

			autoSell.Partner	= dialog.Partner;
			textBoxPartner.Text	= autoSell.PartnerTxt;
		}

		private void checkBoxCredit_CheckedChanged(object sender, System.EventArgs e)
		{
			if (checkBoxCredit.Checked == true)
			{
				// Запрос кредитного банка
				FormCreditBankList dialog = new FormCreditBankList();
				dialog.ShowDialog();
				if(dialog.DialogResult != DialogResult.OK)
				{
					checkBoxCredit.Checked = false;
					creditBank				= null;
					textBoxCredit.Text		= "";
					return;
				}
				creditBank					= dialog.SelectedCreditBank;
				textBoxCredit.Text			= creditBank.DbTitle();
				checkBoxCashless.Checked	= true;
			}
			else
			{
				creditBank			= null;
				textBoxCredit.Text	= "";
			}
		}

		private void checkBoxTradeIn_CheckedChanged(object sender, System.EventArgs e)
		{
			if (checkBoxTradeIn.Checked == true)
			{
				// Запрос автомобиля по обмену
				FormAutoList dialog = new FormAutoList(Db.ClickType.Select, null);
				dialog.ShowDialog();
				if(dialog.DialogResult != DialogResult.OK)
				{
					checkBoxTradeIn.Checked = false;
					tradeinAuto				= null;
					textBoxTradein.Text		= "";
					return;
				}
				tradeinAuto				= dialog.Auto;
				textBoxTradein.Text		= tradeinAuto.Title;
			}
			else
			{
				tradeinAuto				= null;
				textBoxTradein.Text		= "";
			}
		}

		private void checkBoxCashless_CheckedChanged(object sender, System.EventArgs e)
		{
			if (checkBoxCashless.Checked == false)
			{
				// Убераем кредит если был
				checkBoxCredit.Checked  = false;
				creditBank				= null;
				textBoxCredit.Text		= "";
			}
		}

		public DbAutoSell AutoSell
		{
			get
			{
				return AutoSell;
			}
		}
	}
}
