using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for UI_InvoicePayList.
	/// </summary>
	public class UI_InvoicePayList : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView listView_pay;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.Button button_update;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public UI_InvoicePayList()
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
			this.listView_pay = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.button_update = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// listView_pay
			// 
			this.listView_pay.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						   this.columnHeader1,
																						   this.columnHeader2,
																						   this.columnHeader3,
																						   this.columnHeader4});
			this.listView_pay.FullRowSelect = true;
			this.listView_pay.Location = new System.Drawing.Point(8, 8);
			this.listView_pay.Name = "listView_pay";
			this.listView_pay.Size = new System.Drawing.Size(504, 256);
			this.listView_pay.TabIndex = 0;
			this.listView_pay.View = System.Windows.Forms.View.Details;
			this.listView_pay.DoubleClick += new System.EventHandler(this.listView_pay_DoubleClick);
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
			// columnHeader4
			// 
			this.columnHeader4.Text = "Плательщик";
			this.columnHeader4.Width = 240;
			// 
			// button_update
			// 
			this.button_update.Location = new System.Drawing.Point(536, 8);
			this.button_update.Name = "button_update";
			this.button_update.TabIndex = 1;
			this.button_update.Text = "Обновить";
			this.button_update.Click += new System.EventHandler(this.button_update_Click);
			// 
			// UI_InvoicePayList
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(632, 273);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.button_update,
																		  this.listView_pay});
			this.Name = "UI_InvoicePayList";
			this.Text = "Список платежей";
			this.ResumeLayout(false);

		}
		#endregion

		private void button_update_Click(object sender, System.EventArgs e)
		{
			// Обновить список платежей
			ArrayList array = new ArrayList();
			DbSqlInvoicePay.SelectInArray(array);
			FillList(array);
		}

		public void FillList(ArrayList array)
		{
			listView_pay.Items.Clear();
			ListViewItem item;

			foreach(object o in array)
			{
				DtInvoicePay invoicepay = (DtInvoicePay)o;
				item = new ListViewItem();
				invoicepay.SetLVItem(item);
				listView_pay.Items.Add(item);
			}
		}

		private void listView_pay_DoubleClick(object sender, System.EventArgs e)
		{
			// Выбор свойств платежа
			// Определяем, где щелкнули мышкой
			ListViewItem item = null;
			DtInvoicePay pay = null;
			DtInvoicePay.Pair pair;
			
			item = Db.GetItemPosition(listView_pay);
			if(item == null) return;
			pair = (DtInvoicePay.Pair)item.Tag;
			if(pair.code == 0) return;
			// Загружаем карточку
			pay = DbSqlInvoicePay.Find(pair.code, pair.year);
			if(pay == null) return;
			
			UI_InvoicePay form = new UI_InvoicePay(pay);
			form.ShowDialog();
		}
	}
}
