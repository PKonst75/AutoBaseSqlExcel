using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for UI_InvoiceList.
	/// </summary>
	public class UI_InvoiceList : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView listView_invoice;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.Button button_update;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.Button button_delete;
		private System.Windows.Forms.Button button1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		Db.ClickType click_type;
		public DtInvoice selected_invoice = null;

		public UI_InvoiceList(Db.ClickType type)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			click_type = type;
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
			this.listView_invoice = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.button_update = new System.Windows.Forms.Button();
			this.button_delete = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// listView_invoice
			// 
			this.listView_invoice.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.listView_invoice.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																							   this.columnHeader1,
																							   this.columnHeader2,
																							   this.columnHeader3,
																							   this.columnHeader4,
																							   this.columnHeader5,
																							   this.columnHeader6});
			this.listView_invoice.FullRowSelect = true;
			this.listView_invoice.Name = "listView_invoice";
			this.listView_invoice.Size = new System.Drawing.Size(712, 332);
			this.listView_invoice.TabIndex = 0;
			this.listView_invoice.View = System.Windows.Forms.View.Details;
			this.listView_invoice.DoubleClick += new System.EventHandler(this.listView_invoice_DoubleClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Номер";
			this.columnHeader1.Width = 50;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Дата";
			this.columnHeader2.Width = 65;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Контрагент";
			this.columnHeader3.Width = 166;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Сумма";
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "Ждем";
			this.columnHeader5.Width = 65;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "Примечание";
			this.columnHeader6.Width = 224;
			// 
			// button_update
			// 
			this.button_update.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.button_update.Location = new System.Drawing.Point(728, 8);
			this.button_update.Name = "button_update";
			this.button_update.TabIndex = 1;
			this.button_update.Text = "Обновить";
			this.button_update.Click += new System.EventHandler(this.button_update_Click);
			// 
			// button_delete
			// 
			this.button_delete.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.button_delete.Location = new System.Drawing.Point(728, 88);
			this.button_delete.Name = "button_delete";
			this.button_delete.TabIndex = 2;
			this.button_delete.Text = "Удалить";
			this.button_delete.Click += new System.EventHandler(this.button_delete_Click);
			// 
			// button1
			// 
			this.button1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			this.button1.Location = new System.Drawing.Point(728, 296);
			this.button1.Name = "button1";
			this.button1.TabIndex = 3;
			this.button1.Text = "Платежи";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// UI_InvoiceList
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(808, 333);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.button1,
																		  this.button_delete,
																		  this.button_update,
																		  this.listView_invoice});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "UI_InvoiceList";
			this.Text = "Счета";
			this.ResumeLayout(false);

		}
		#endregion

		private void button_update_Click(object sender, System.EventArgs e)
		{
			// Обновить список счетов
			ArrayList array = new ArrayList();
			DbSqlInvoice.SelectInArray(array);
			FillList(array);

		}

		public void FillList(ArrayList array)
		{
			listView_invoice.Items.Clear();
			ListViewItem item;

			foreach(object o in array)
			{
				DtInvoice invoice = (DtInvoice)o;
				item = new ListViewItem();
				invoice.SetLVItem(item);
				listView_invoice.Items.Add(item);
			}
		}

		private void listView_invoice_DoubleClick(object sender, System.EventArgs e)
		{
			// Определяем, где щелкнули мышкой
			ListViewItem item = null;
			DtInvoice invoice = null;
			DtInvoice.Pair pair;
			
			item = Db.GetItemPosition(listView_invoice);
			if(item == null) return;
			pair = (DtInvoice.Pair)item.Tag;
			if(pair.code == 0) return;
			// Загружаем карточку
			invoice = DbSqlInvoice.Find(pair.code, pair.year);
			if(invoice == null) return;

			if(click_type == Db.ClickType.Select)
			{
				this.DialogResult = DialogResult.OK;
				selected_invoice = invoice;
				this.Close();
				return;
			}
			
			UI_Invoice form = new UI_Invoice(invoice);
			form.ShowDialog();
		}

		private void button_delete_Click(object sender, System.EventArgs e)
		{
			// Проверка прав пользователя
			string login = Form1.currentLogin.ToLower();
			if (login != "админ")
			{
				return;	// Ограничение доступа
			}
			ListViewItem item = null;
			DtInvoice invoice = null;
			DtInvoice.Pair pair;

			// Удалить выбранный счет (если выбрано несколько - удаляем только первый)
			item = Db.GetItemSelected(listView_invoice);
			if(item == null) return;
			pair = (DtInvoice.Pair)item.Tag;
			if(pair.code == 0) return;
			// Загружаем карточку
			invoice = DbSqlInvoice.Find(pair.code, pair.year);
			if(invoice == null) return;

			// Непосредственно удаляем
			if (DbSqlInvoice.Delete(pair.code, pair.year) == false) return;
			listView_invoice.Items.Remove(item);
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			UI_InvoicePayList form = new UI_InvoicePayList();
			form.Show();
		}
	}
}
