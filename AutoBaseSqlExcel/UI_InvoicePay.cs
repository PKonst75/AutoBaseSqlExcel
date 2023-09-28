using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for UI_InvoicePay.
	/// </summary>
	public class UI_InvoicePay : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBox_partner;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBox_numberpp;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.DateTimePicker dateTimePicker_date;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBox_summ;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textBox_comment;
		private System.Windows.Forms.ComboBox comboBox_type;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button button_pay;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ListView listView_invoice;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.Button button1;

		DtInvoicePay invoice_pay;

		public UI_InvoicePay()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		public UI_InvoicePay(DtInvoice invoice)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			comboBox_type.Items.Add("Не определено");				// 0
			comboBox_type.Items.Add("Оплата по счету");				// 1
			comboBox_type.Items.Add("Групповая оплата");			// 2
			comboBox_type.Items.Add("Частичная оплата");			// 3
			comboBox_type.SelectedIndex = 1;

			invoice_pay = new DtInvoicePay(invoice);

			DtPartner partner = DbSqlPartner.Find(invoice_pay.code_partner);
			if(partner != null)
				textBox_partner.Text = partner.GetTitle();

			dateTimePicker_date.Value = DateTime.Now;
			textBox_summ.Text			= invoice_pay.summ.ToString();
			dateTimePicker_date.Value	= DateTime.Now;

			// Заполняем список счетов
			ListViewItem item = new ListViewItem("");
			SetListViewItem(invoice, item);
			listView_invoice.Items.Add(item);
		}

		public UI_InvoicePay(DtInvoicePay src)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			
			button_pay.Enabled	= false;

			invoice_pay = new DtInvoicePay(src);

			comboBox_type.Items.Add("Не определено");				// 0
			comboBox_type.Items.Add("Оплата по счету");				// 1
			comboBox_type.Items.Add("Групповая оплата");			// 2
			comboBox_type.Items.Add("Частичная оплата");			// 3
			comboBox_type.SelectedIndex = invoice_pay.type;

			DtPartner partner = DbSqlPartner.Find(invoice_pay.code_partner);
			if(partner != null)
				textBox_partner.Text = partner.GetTitle();

			textBox_summ.Text			= invoice_pay.summ.ToString();
			dateTimePicker_date.Value	= invoice_pay.date;

			textBox_numberpp.Text		= invoice_pay.number_pp;
			textBox_comment.Text		= invoice_pay.comment;

			// Заполняем список счетов
			ArrayList array = new ArrayList();
			DbSqlInvoice.SelectInArrayPay(src.code, src.year, array);
			foreach(object o in array)
			{
				DtInvoice inv = (DtInvoice)o;
				if(inv != null)
				{
					DtInvoice invoice = DbSqlInvoice.Find(inv.code, inv.year);
					if(invoice != null)
					{
						ListViewItem item = new ListViewItem("");
						SetListViewItem(invoice, item);
						listView_invoice.Items.Add(item);
					}
				}
			}
		}

		public void SetListViewItem(DtInvoice invoice, ListViewItem item)
		{
			DtInvoice.Pair pair = new DtInvoice.Pair();
			pair.code		= (long)invoice.code;
			pair.year		= (int)invoice.year;
			item.Tag = pair;
			item.Text = invoice.number_buhg;
			item.SubItems.Add(invoice.date_buhg.ToShortDateString());
			float summ = invoice.summ;
			item.SubItems.Add(summ.ToString());
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
			this.textBox_numberpp = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.dateTimePicker_date = new System.Windows.Forms.DateTimePicker();
			this.label4 = new System.Windows.Forms.Label();
			this.textBox_summ = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.textBox_comment = new System.Windows.Forms.TextBox();
			this.comboBox_type = new System.Windows.Forms.ComboBox();
			this.label6 = new System.Windows.Forms.Label();
			this.button_pay = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.listView_invoice = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.button1 = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
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
			this.textBox_partner.Size = new System.Drawing.Size(304, 23);
			this.textBox_partner.TabIndex = 1;
			this.textBox_partner.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 40);
			this.label2.Name = "label2";
			this.label2.TabIndex = 2;
			this.label2.Text = "№ П/П";
			// 
			// textBox_numberpp
			// 
			this.textBox_numberpp.Location = new System.Drawing.Point(112, 40);
			this.textBox_numberpp.Name = "textBox_numberpp";
			this.textBox_numberpp.TabIndex = 3;
			this.textBox_numberpp.Text = "";
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
			this.dateTimePicker_date.Location = new System.Drawing.Point(272, 40);
			this.dateTimePicker_date.Name = "dateTimePicker_date";
			this.dateTimePicker_date.TabIndex = 5;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 80);
			this.label4.Name = "label4";
			this.label4.TabIndex = 6;
			this.label4.Text = "Сумма";
			// 
			// textBox_summ
			// 
			this.textBox_summ.Location = new System.Drawing.Point(112, 80);
			this.textBox_summ.Name = "textBox_summ";
			this.textBox_summ.TabIndex = 7;
			this.textBox_summ.Text = "";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(8, 120);
			this.label5.Name = "label5";
			this.label5.TabIndex = 8;
			this.label5.Text = "Примечание";
			// 
			// textBox_comment
			// 
			this.textBox_comment.Location = new System.Drawing.Point(112, 120);
			this.textBox_comment.Name = "textBox_comment";
			this.textBox_comment.Size = new System.Drawing.Size(464, 23);
			this.textBox_comment.TabIndex = 9;
			this.textBox_comment.Text = "";
			// 
			// comboBox_type
			// 
			this.comboBox_type.Location = new System.Drawing.Point(120, 160);
			this.comboBox_type.Name = "comboBox_type";
			this.comboBox_type.Size = new System.Drawing.Size(456, 24);
			this.comboBox_type.TabIndex = 10;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(8, 160);
			this.label6.Name = "label6";
			this.label6.TabIndex = 11;
			this.label6.Text = "Тип";
			// 
			// button_pay
			// 
			this.button_pay.Location = new System.Drawing.Point(432, 384);
			this.button_pay.Name = "button_pay";
			this.button_pay.Size = new System.Drawing.Size(152, 23);
			this.button_pay.TabIndex = 12;
			this.button_pay.Text = "Подтвердить оплату";
			this.button_pay.Click += new System.EventHandler(this.button_pay_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.button1,
																					this.listView_invoice});
			this.groupBox1.Location = new System.Drawing.Point(8, 200);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(576, 176);
			this.groupBox1.TabIndex = 13;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Счета";
			// 
			// listView_invoice
			// 
			this.listView_invoice.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																							   this.columnHeader1,
																							   this.columnHeader2,
																							   this.columnHeader3});
			this.listView_invoice.Location = new System.Drawing.Point(8, 24);
			this.listView_invoice.Name = "listView_invoice";
			this.listView_invoice.Size = new System.Drawing.Size(448, 144);
			this.listView_invoice.TabIndex = 0;
			this.listView_invoice.View = System.Windows.Forms.View.Details;
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
			this.columnHeader3.Width = 87;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(480, 24);
			this.button1.Name = "button1";
			this.button1.TabIndex = 1;
			this.button1.Text = "Добавить";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// UI_InvoicePay
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.ClientSize = new System.Drawing.Size(592, 415);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.groupBox1,
																		  this.button_pay,
																		  this.label6,
																		  this.comboBox_type,
																		  this.textBox_comment,
																		  this.label5,
																		  this.textBox_summ,
																		  this.label4,
																		  this.dateTimePicker_date,
																		  this.label3,
																		  this.textBox_numberpp,
																		  this.label2,
																		  this.textBox_partner,
																		  this.label1});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "UI_InvoicePay";
			this.Text = "Платеж по счету";
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void button_pay_Click(object sender, System.EventArgs e)
		{
			// Провести оплату
			invoice_pay.summ		= (float)Convert.ToDouble(textBox_summ.Text);
			invoice_pay.comment		= textBox_comment.Text;
			invoice_pay.date		= dateTimePicker_date.Value;
			invoice_pay.type		= comboBox_type.SelectedIndex;
			invoice_pay.number_pp	= textBox_numberpp.Text;

			invoice_pay = DbSqlInvoicePay.Insert(invoice_pay);
			if(invoice_pay == null) return;
			DialogResult = DialogResult.OK;

			// Добавляем соответсвия платеж-счет
			foreach(ListViewItem itm in listView_invoice.Items)
			{
				if(itm.Tag != null)
				{
					DtInvoice.Pair pair = (DtInvoice.Pair)itm.Tag;
					DbSqlInvoice.InsertPay(invoice_pay, pair.code, pair.year);
				}
			}

			MessageBox.Show("Оплачено");
			this.Close();
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			// Добавляем счет в оплату
			UI_InvoiceList form = new UI_InvoiceList(Db.ClickType.Select);
			if(form.ShowDialog() != DialogResult.OK) return;

			DtInvoice inv = form.selected_invoice;
			ListViewItem item = new ListViewItem();
			SetListViewItem(inv, item);
			listView_invoice.Items.Add(item);

			// Увеличиваем сумму
			invoice_pay.summ += inv.summ;
			textBox_summ.Text = invoice_pay.summ.ToString();
		}
	}
}
