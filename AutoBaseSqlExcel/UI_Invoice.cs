using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for UI_Invoice.
	/// </summary>
	public class UI_Invoice : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBox_number;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.DateTimePicker dateTimePicker_date;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBox_numberbuhg;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.DateTimePicker dateTimePicker_datebuhg;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textBox_comment;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox textBox_summ;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.CheckBox checkBox_pay;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.DateTimePicker dateTimePicker_datepay;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.TextBox textBox_commentnopay;
		private System.Windows.Forms.Button button_save;
		private System.Windows.Forms.Button button_add;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.TextBox textBox_partner;
		private System.Windows.Forms.DateTimePicker dateTimePicker_datecontrollgreen;
		private System.Windows.Forms.DateTimePicker dateTimePicker_datecontrollyellow;
		private System.Windows.Forms.DateTimePicker dateTimePicker_datecontrollred;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.ComboBox comboBox_type;
		private System.Windows.Forms.Button button_pay;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.Button button_addcard;
		private System.Windows.Forms.ListView listView_cards;

		DtInvoice invoice = null;

		public UI_Invoice()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		public UI_Invoice(DtCard card)
		{
			InitializeComponent();

			// Настйка визуальных параметров
			button_save.Enabled = false;
			button_pay.Enabled  = false;
			
			// Настройка данных
			invoice = new DtInvoice(card);
			DtPartner partner = DbSqlPartner.Find(invoice.code_partner);
			if(partner != null)
				textBox_partner.Text = partner.GetTitle();
			dateTimePicker_date.Value = DateTime.Now;

			dateTimePicker_datebuhg.Value			= invoice.date_buhg;
			textBox_summ.Text						= invoice.summ.ToString();
			dateTimePicker_datecontrollgreen.Value	= invoice.date_controll_green;
			dateTimePicker_datecontrollyellow.Value = invoice.date_controll_yellow;
			dateTimePicker_datecontrollred.Value	= invoice.date_controll_red;

			comboBox_type.Items.Add("Не определено");				// 0
			comboBox_type.Items.Add("Счет на оплату заказ-наряда"); // 1
			comboBox_type.Items.Add("Счет на предоплату");			// 2
			comboBox_type.Items.Add("Счет на частичную оплату");	// 3
			comboBox_type.Items.Add("Счет на групповую оплату");	// 4
			comboBox_type.SelectedIndex = 1;

			ListViewItem item = new ListViewItem("");
			SetListViewItem(card, item);
			listView_cards.Items.Add(item);
		}

		public void SetListViewItem(DtCard card, ListViewItem item)
		{
			DtCard.Pair pair = new DtCard.Pair();
			pair.number		= (long)card.GetData("НОМЕР_КАРТОЧКА");
			pair.year		= (int)card.GetData("ГОД_КАРТОЧКА");
			item.Tag = pair;
			item.Text = card.GetData("НОМЕР_КАРТОЧКА").ToString();
			item.SubItems.Add(((DateTime)card.GetData("ДАТА")).ToShortDateString());
			float summ = card.SummWorkPay() + card.SummDetailOilPay() + card.SummDetailPay();
			item.SubItems.Add(summ.ToString());
		}

		public UI_Invoice(DtInvoice src)
		{
			InitializeComponent();

			// Настйка визуальных параметров
			button_save.Enabled = true;
			button_add.Enabled = false;
			button_addcard.Enabled = false;
			
			// Настройка данных
			invoice = new DtInvoice(src);
			DtPartner partner = DbSqlPartner.Find(invoice.code_partner);
			if(partner != null)
				textBox_partner.Text = partner.GetTitle();
			textBox_number.Text						= invoice.code.ToString();
			dateTimePicker_date.Value				= invoice.date;
			textBox_comment.Text					= invoice.comment;
			dateTimePicker_datebuhg.Value			= invoice.date_buhg;
			textBox_summ.Text						= invoice.summ.ToString();
			textBox_numberbuhg.Text					= invoice.number_buhg.ToString();
			dateTimePicker_datecontrollgreen.Value	= invoice.date_controll_green;
			dateTimePicker_datecontrollyellow.Value = invoice.date_controll_yellow;
			dateTimePicker_datecontrollred.Value	= invoice.date_controll_red;

			comboBox_type.Items.Add("Не определено");				// 0
			comboBox_type.Items.Add("Счет на оплату заказ-наряда"); // 1
			comboBox_type.Items.Add("Счет на предоплату");			// 2
			comboBox_type.Items.Add("Счет на частичную оплату");	// 3
			comboBox_type.Items.Add("Счет на групповую оплату");	// 4
			comboBox_type.SelectedIndex = invoice.type;

			textBox_commentnopay.Text	= invoice.comment_unpay;
			checkBox_pay.Checked		= invoice.pay;
			dateTimePicker_datepay.Value	= invoice.date_pay;

			// Список  карточек счета
			ArrayList array = new ArrayList();
			DbSqlInvoice.SelectInArrayCards(src.code, src.year, array);
			foreach(object o in array)
			{
				DtCard.Pair pair = (DtCard.Pair)o;
				if(pair.number != 0)
				{
					DtCard card = DbSqlCard.Find(pair.number, pair.year);
					if(card != null)
					{
						ListViewItem item = new ListViewItem("");
						SetListViewItem(card, item);
						listView_cards.Items.Add(item);
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
			this.textBox_partner = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textBox_number = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.dateTimePicker_date = new System.Windows.Forms.DateTimePicker();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.dateTimePicker_datecontrollred = new System.Windows.Forms.DateTimePicker();
			this.label10 = new System.Windows.Forms.Label();
			this.dateTimePicker_datecontrollyellow = new System.Windows.Forms.DateTimePicker();
			this.label9 = new System.Windows.Forms.Label();
			this.dateTimePicker_datecontrollgreen = new System.Windows.Forms.DateTimePicker();
			this.label8 = new System.Windows.Forms.Label();
			this.textBox_summ = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.dateTimePicker_datebuhg = new System.Windows.Forms.DateTimePicker();
			this.label5 = new System.Windows.Forms.Label();
			this.textBox_numberbuhg = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.textBox_comment = new System.Windows.Forms.TextBox();
			this.checkBox_pay = new System.Windows.Forms.CheckBox();
			this.label11 = new System.Windows.Forms.Label();
			this.dateTimePicker_datepay = new System.Windows.Forms.DateTimePicker();
			this.label12 = new System.Windows.Forms.Label();
			this.textBox_commentnopay = new System.Windows.Forms.TextBox();
			this.button_save = new System.Windows.Forms.Button();
			this.button_add = new System.Windows.Forms.Button();
			this.comboBox_type = new System.Windows.Forms.ComboBox();
			this.label13 = new System.Windows.Forms.Label();
			this.button_pay = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.button_addcard = new System.Windows.Forms.Button();
			this.listView_cards = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.TabIndex = 0;
			this.label1.Text = "Контрагент";
			// 
			// textBox_partner
			// 
			this.textBox_partner.Location = new System.Drawing.Point(112, 8);
			this.textBox_partner.Name = "textBox_partner";
			this.textBox_partner.ReadOnly = true;
			this.textBox_partner.Size = new System.Drawing.Size(512, 23);
			this.textBox_partner.TabIndex = 1;
			this.textBox_partner.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 40);
			this.label2.Name = "label2";
			this.label2.TabIndex = 2;
			this.label2.Text = "Номер";
			// 
			// textBox_number
			// 
			this.textBox_number.Location = new System.Drawing.Point(112, 40);
			this.textBox_number.Name = "textBox_number";
			this.textBox_number.ReadOnly = true;
			this.textBox_number.TabIndex = 3;
			this.textBox_number.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(224, 40);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(48, 23);
			this.label3.TabIndex = 4;
			this.label3.Text = "Дата";
			// 
			// dateTimePicker_date
			// 
			this.dateTimePicker_date.Enabled = false;
			this.dateTimePicker_date.Location = new System.Drawing.Point(272, 40);
			this.dateTimePicker_date.Name = "dateTimePicker_date";
			this.dateTimePicker_date.TabIndex = 5;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.dateTimePicker_datecontrollred,
																					this.label10,
																					this.dateTimePicker_datecontrollyellow,
																					this.label9,
																					this.dateTimePicker_datecontrollgreen,
																					this.label8,
																					this.textBox_summ,
																					this.label7,
																					this.dateTimePicker_datebuhg,
																					this.label5,
																					this.textBox_numberbuhg,
																					this.label4});
			this.groupBox1.Location = new System.Drawing.Point(8, 152);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(616, 192);
			this.groupBox1.TabIndex = 6;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Бухгалтерские данные";
			// 
			// dateTimePicker_datecontrollred
			// 
			this.dateTimePicker_datecontrollred.Location = new System.Drawing.Point(216, 152);
			this.dateTimePicker_datecontrollred.Name = "dateTimePicker_datecontrollred";
			this.dateTimePicker_datecontrollred.TabIndex = 11;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(8, 152);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(216, 23);
			this.label10.TabIndex = 10;
			this.label10.Text = "Контрольная дата (Красная)";
			// 
			// dateTimePicker_datecontrollyellow
			// 
			this.dateTimePicker_datecontrollyellow.Location = new System.Drawing.Point(216, 120);
			this.dateTimePicker_datecontrollyellow.Name = "dateTimePicker_datecontrollyellow";
			this.dateTimePicker_datecontrollyellow.TabIndex = 9;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(8, 120);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(200, 23);
			this.label9.TabIndex = 8;
			this.label9.Text = "Контрольная дата (Желтая)";
			// 
			// dateTimePicker_datecontrollgreen
			// 
			this.dateTimePicker_datecontrollgreen.Location = new System.Drawing.Point(216, 88);
			this.dateTimePicker_datecontrollgreen.Name = "dateTimePicker_datecontrollgreen";
			this.dateTimePicker_datecontrollgreen.TabIndex = 7;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(8, 88);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(200, 23);
			this.label8.TabIndex = 6;
			this.label8.Text = "Контрольная дата (Зеленая)";
			// 
			// textBox_summ
			// 
			this.textBox_summ.Location = new System.Drawing.Point(112, 56);
			this.textBox_summ.Name = "textBox_summ";
			this.textBox_summ.Size = new System.Drawing.Size(120, 23);
			this.textBox_summ.TabIndex = 5;
			this.textBox_summ.Text = "";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(8, 56);
			this.label7.Name = "label7";
			this.label7.TabIndex = 4;
			this.label7.Text = "Сумма";
			// 
			// dateTimePicker_datebuhg
			// 
			this.dateTimePicker_datebuhg.Location = new System.Drawing.Point(328, 24);
			this.dateTimePicker_datebuhg.Name = "dateTimePicker_datebuhg";
			this.dateTimePicker_datebuhg.TabIndex = 3;
			this.dateTimePicker_datebuhg.ValueChanged += new System.EventHandler(this.dateTimePicker_datebuhg_ValueChanged);
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(256, 24);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(56, 23);
			this.label5.TabIndex = 2;
			this.label5.Text = "Дата";
			// 
			// textBox_numberbuhg
			// 
			this.textBox_numberbuhg.Location = new System.Drawing.Point(112, 24);
			this.textBox_numberbuhg.Name = "textBox_numberbuhg";
			this.textBox_numberbuhg.Size = new System.Drawing.Size(120, 23);
			this.textBox_numberbuhg.TabIndex = 1;
			this.textBox_numberbuhg.Text = "";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 24);
			this.label4.Name = "label4";
			this.label4.TabIndex = 0;
			this.label4.Text = "Номер";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(8, 72);
			this.label6.Name = "label6";
			this.label6.TabIndex = 7;
			this.label6.Text = "Примечание";
			// 
			// textBox_comment
			// 
			this.textBox_comment.Location = new System.Drawing.Point(112, 72);
			this.textBox_comment.Name = "textBox_comment";
			this.textBox_comment.Size = new System.Drawing.Size(512, 23);
			this.textBox_comment.TabIndex = 8;
			this.textBox_comment.Text = "";
			// 
			// checkBox_pay
			// 
			this.checkBox_pay.Enabled = false;
			this.checkBox_pay.Location = new System.Drawing.Point(16, 592);
			this.checkBox_pay.Name = "checkBox_pay";
			this.checkBox_pay.Size = new System.Drawing.Size(88, 24);
			this.checkBox_pay.TabIndex = 9;
			this.checkBox_pay.Text = "Оплачен";
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(120, 592);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(96, 23);
			this.label11.TabIndex = 10;
			this.label11.Text = "Дата оплаты";
			// 
			// dateTimePicker_datepay
			// 
			this.dateTimePicker_datepay.Enabled = false;
			this.dateTimePicker_datepay.Location = new System.Drawing.Point(224, 592);
			this.dateTimePicker_datepay.Name = "dateTimePicker_datepay";
			this.dateTimePicker_datepay.TabIndex = 11;
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(8, 352);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(144, 23);
			this.label12.TabIndex = 12;
			this.label12.Text = "Причина неплатежа";
			// 
			// textBox_commentnopay
			// 
			this.textBox_commentnopay.Location = new System.Drawing.Point(160, 352);
			this.textBox_commentnopay.Name = "textBox_commentnopay";
			this.textBox_commentnopay.Size = new System.Drawing.Size(464, 23);
			this.textBox_commentnopay.TabIndex = 13;
			this.textBox_commentnopay.Text = "";
			// 
			// button_save
			// 
			this.button_save.Location = new System.Drawing.Point(408, 640);
			this.button_save.Name = "button_save";
			this.button_save.Size = new System.Drawing.Size(96, 23);
			this.button_save.TabIndex = 14;
			this.button_save.Text = "Сохранить";
			// 
			// button_add
			// 
			this.button_add.Location = new System.Drawing.Point(528, 640);
			this.button_add.Name = "button_add";
			this.button_add.Size = new System.Drawing.Size(88, 23);
			this.button_add.TabIndex = 15;
			this.button_add.Text = "Добавить";
			this.button_add.Click += new System.EventHandler(this.button_add_Click);
			// 
			// comboBox_type
			// 
			this.comboBox_type.Location = new System.Drawing.Point(112, 104);
			this.comboBox_type.Name = "comboBox_type";
			this.comboBox_type.Size = new System.Drawing.Size(384, 24);
			this.comboBox_type.TabIndex = 16;
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(16, 104);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(80, 23);
			this.label13.TabIndex = 17;
			this.label13.Text = "Тип счета";
			// 
			// button_pay
			// 
			this.button_pay.Location = new System.Drawing.Point(440, 592);
			this.button_pay.Name = "button_pay";
			this.button_pay.Size = new System.Drawing.Size(128, 23);
			this.button_pay.TabIndex = 18;
			this.button_pay.Text = "Оплатить";
			this.button_pay.Click += new System.EventHandler(this.button_pay_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.button_addcard,
																					this.listView_cards});
			this.groupBox2.Location = new System.Drawing.Point(16, 392);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(608, 184);
			this.groupBox2.TabIndex = 19;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Заказ-наряды";
			// 
			// button_addcard
			// 
			this.button_addcard.Location = new System.Drawing.Point(512, 24);
			this.button_addcard.Name = "button_addcard";
			this.button_addcard.Size = new System.Drawing.Size(88, 23);
			this.button_addcard.TabIndex = 1;
			this.button_addcard.Text = "Добавить";
			this.button_addcard.Click += new System.EventHandler(this.button_addcard_Click);
			// 
			// listView_cards
			// 
			this.listView_cards.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																							 this.columnHeader1,
																							 this.columnHeader2,
																							 this.columnHeader3});
			this.listView_cards.Location = new System.Drawing.Point(8, 24);
			this.listView_cards.Name = "listView_cards";
			this.listView_cards.Size = new System.Drawing.Size(496, 152);
			this.listView_cards.TabIndex = 0;
			this.listView_cards.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Номер";
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Дата";
			this.columnHeader2.Width = 90;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Сумма";
			this.columnHeader3.Width = 90;
			// 
			// UI_Invoice
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.ClientSize = new System.Drawing.Size(632, 671);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.groupBox2,
																		  this.button_pay,
																		  this.label13,
																		  this.comboBox_type,
																		  this.button_add,
																		  this.button_save,
																		  this.textBox_commentnopay,
																		  this.label12,
																		  this.dateTimePicker_datepay,
																		  this.label11,
																		  this.checkBox_pay,
																		  this.textBox_comment,
																		  this.label6,
																		  this.groupBox1,
																		  this.dateTimePicker_date,
																		  this.label3,
																		  this.textBox_number,
																		  this.label2,
																		  this.textBox_partner,
																		  this.label1});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "UI_Invoice";
			this.Text = "СЧЕТ";
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void button_add_Click(object sender, System.EventArgs e)
		{
			// Добавить новый счет
			// Получаем данные счета
			invoice.comment					= textBox_comment.Text;
			invoice.number_buhg				= textBox_numberbuhg.Text;
			invoice.date_buhg				= dateTimePicker_datebuhg.Value;
			invoice.summ					= (float)Convert.ToDouble(textBox_summ.Text);
			invoice.date_controll_green		= dateTimePicker_datecontrollgreen.Value;
			invoice.date_controll_yellow	= dateTimePicker_datecontrollyellow.Value;
			invoice.date_controll_red		= dateTimePicker_datecontrollred.Value;

			invoice.type					= (int)comboBox_type.SelectedIndex;

			if(DbSqlInvoice.Insert(invoice) == null)
			{
				MessageBox.Show("Ошибка добавления");
				return;
			}

			// Добавляем список карточек счета
			foreach(ListViewItem itm in listView_cards.Items)
			{
				if(itm.Tag != null)
				{
					DtCard.Pair pair = (DtCard.Pair)itm.Tag;
					DbSqlInvoice.InsertCard(invoice, pair.number, pair.year);
				}
			}

			MessageBox.Show("Счет добавлен");
			this.Close();
		}

		private void dateTimePicker_datebuhg_ValueChanged(object sender, System.EventArgs e)
		{
			// При смене значения, меняем и все остальные
			invoice.date_buhg						= dateTimePicker_datebuhg.Value;
			invoice.SetControlDate();
			dateTimePicker_datecontrollgreen.Value	= invoice.date_controll_green;
			dateTimePicker_datecontrollyellow.Value = invoice.date_controll_yellow;
			dateTimePicker_datecontrollred.Value	= invoice.date_controll_red;
		}

		private void button_pay_Click(object sender, System.EventArgs e)
		{
			// Отметить оплату счета
			UI_InvoicePay form = new UI_InvoicePay(invoice);
			if(form.ShowDialog() != DialogResult.OK) return;
			DialogResult = DialogResult.OK;
			Close();
		}

		private void button_addcard_Click(object sender, System.EventArgs e)
		{
			// Добавить заказ-наряд в список
			FormManageCard form = new FormManageCard(Db.ClickType.Select, 0, null);
			if(form.ShowDialog() != DialogResult.OK) return;
			DtCard card = form.card_selected;
			if (card == null) return;
			ListViewItem item = new ListViewItem("");
			SetListViewItem(card, item);
			listView_cards.Items.Add(item);
			float summ_card = invoice.summ;
			summ_card += card.SummDetailOilPay() + card.SummDetailPay() + card.SummWorkPay();
			textBox_summ.Text = summ_card.ToString();
		}
	}
}
