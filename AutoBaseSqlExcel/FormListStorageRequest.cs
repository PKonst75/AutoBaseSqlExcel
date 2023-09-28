using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormListStorageRequest.
	/// </summary>
	public class FormListStorageRequest : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader head_number;
		private System.Windows.Forms.ColumnHeader head_year;
		private System.Windows.Forms.ColumnHeader head_code_storage;
		private System.Windows.Forms.ColumnHeader head_quontity;
		private System.Windows.Forms.ColumnHeader head_date_perfomance;
		private System.Windows.Forms.ColumnHeader head_partner;
		private System.Windows.Forms.ColumnHeader head_requester;
		private System.Windows.Forms.ColumnHeader head_date_supply;
		private System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.Button button_update;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.ComponentModel.IContainer components;

		public FormListStorageRequest()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			// Организация заголовков
			head_number = new System.Windows.Forms.ColumnHeader();
			head_number.Width = 60;
			head_number.Text = "Номер";
			head_year = new System.Windows.Forms.ColumnHeader();
			head_year.Width = 40;
			head_year.Text = "Дата и Время";
			head_code_storage = new System.Windows.Forms.ColumnHeader();
			head_code_storage.Width = 200;
			head_code_storage.Text = "Код складской позиции";
			head_quontity = new System.Windows.Forms.ColumnHeader();
			head_quontity.Width = 80;
			head_quontity.Text = "Количество";
			head_date_perfomance = new System.Windows.Forms.ColumnHeader();
			head_date_perfomance.Width = 80;
			head_date_perfomance.Text = "Дата исполнения";
			head_date_supply = new System.Windows.Forms.ColumnHeader();
			head_date_supply.Width = 80;
			head_date_supply.Text = "Дата поставки";
			head_requester = new System.Windows.Forms.ColumnHeader();
			head_requester.Width = 200;
			head_requester.Text = "Подписал заявку";
			head_partner = new System.Windows.Forms.ColumnHeader();
			head_partner.Width = 200;
			head_partner.Text = "Для кого";
			// Установка заголовков
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.head_number,
																						this.head_year,
																						this.head_code_storage,
																						this.head_quontity,
																						this.head_date_perfomance,
																						this.head_partner,
																						this.head_date_supply,
																						this.head_requester
			});
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormListStorageRequest));
			this.listView1 = new System.Windows.Forms.ListView();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.button_update = new System.Windows.Forms.Button();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.SuspendLayout();
			// 
			// listView1
			// 
			this.listView1.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.listView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.listView1.FullRowSelect = true;
			this.listView1.Location = new System.Drawing.Point(8, 32);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(576, 304);
			this.listView1.StateImageList = this.imageList1;
			this.listView1.TabIndex = 0;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
			this.listView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseUp);
			this.listView1.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView1_ColumnClick);
			// 
			// imageList1
			// 
			this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// contextMenu1
			// 
			this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.menuItem1,
																						 this.menuItem6});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem2,
																					  this.menuItem3,
																					  this.menuItem4,
																					  this.menuItem5});
			this.menuItem1.Text = "Заявка";
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 0;
			this.menuItem2.Text = "Новая";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 1;
			this.menuItem3.Text = "Отметить подачу";
			this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 2;
			this.menuItem4.Text = "Отметить выполнение";
			this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
			// 
			// menuItem5
			// 
			this.menuItem5.Index = 3;
			this.menuItem5.Text = "Архивация";
			this.menuItem5.Click += new System.EventHandler(this.menuItem5_Click);
			// 
			// button_update
			// 
			this.button_update.Image = ((System.Drawing.Bitmap)(resources.GetObject("button_update.Image")));
			this.button_update.Location = new System.Drawing.Point(8, 8);
			this.button_update.Name = "button_update";
			this.button_update.Size = new System.Drawing.Size(24, 23);
			this.button_update.TabIndex = 1;
			this.button_update.Click += new System.EventHandler(this.button_update_Click);
			// 
			// menuItem6
			// 
			this.menuItem6.Index = 1;
			this.menuItem6.Text = "Печать заявок";
			this.menuItem6.Click += new System.EventHandler(this.menuItem6_Click);
			// 
			// FormListStorageRequest
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(592, 341);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.button_update,
																		  this.listView1});
			this.Name = "FormListStorageRequest";
			this.Text = "Заявки на склад";
			this.ResumeLayout(false);

		}
		#endregion

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			// Запускаем процедуру оформления новой заявки
			FormUpdateStorageRequest dialog = new FormUpdateStorageRequest(null);
			dialog.ShowDialog();
		}

		private void listView1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if(e.Button == MouseButtons.Right)
			{
				// На отпускание правой кнопки мышки - всплывающее меню
				// Настройка меню
				// Показ меню
				contextMenu1.Show(listView1, new Point(e.X, e.Y));
			}
		}

		private void button_update_Click(object sender, System.EventArgs e)
		{
			// Обновить список
			listView1.Items.Clear();
			DbSqlStorageRequest.SelectInList(listView1);
		}

		private void listView1_DoubleClick(object sender, System.EventArgs e)
		{
			// Показ всех характеристик заданного элемента
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			if(item.Tag == null) return;
			DtStorageRequest.CodeYear code_year = (DtStorageRequest.CodeYear)item.Tag;
			DtStorageRequest element = DbSqlStorageRequest.Find(code_year.code, code_year.year);
			if(element == null) return;
			// Запускаем процедуру показа данных заявки
			FormUpdateStorageRequest dialog = new FormUpdateStorageRequest(element);
			dialog.ShowDialog();

		}

		private void menuItem3_Click(object sender, System.EventArgs e)
		{
			// Отметить подачу выбранной заявки
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			if(item.Tag == null) return;
			DtStorageRequest.CodeYear code_year = (DtStorageRequest.CodeYear)item.Tag;
			DtStorageRequest request = DbSqlStorageRequest.Find(code_year.code, code_year.year);
			if(request == null) return;
			// Запрос предполагаемой даты поставки
			FormSelectDate dialog = new FormSelectDate();
			if(dialog.ShowDialog() != DialogResult.OK) return;
			request.SetData("ДАТА_ПОСТАВКИ", dialog.SelectedDate);
			// Запрос кода подписавшего заявку
			DbStaff staff = DbStaff.GetByESign("Электронная подпись");
			if(staff == null) return;
			request.SetData("КОД_ПОДПИСАЛ_ПОДАЧА_ЗАЯВКА", staff.Code);
			// Отмечаем подачу заявки
			request = DbSqlStorageRequest.Give(request);
			if(request != null)
			{
				request.SetLVItem(item);
			}
		}

		private void menuItem4_Click(object sender, System.EventArgs e)
		{
			// Отметить выполнение заявки (по  поступлению запчасти на склад)
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			if(item.Tag == null) return;
			DtStorageRequest.CodeYear code_year = (DtStorageRequest.CodeYear)item.Tag;
			DtStorageRequest request = DbSqlStorageRequest.Find(code_year.code, code_year.year);
			if(request == null) return;
			// Запрос кода подписавшего заявку
			DbStaff staff = DbStaff.GetByESign("Электронная подпись");
			if(staff == null) return;
			request.SetData("КОД_ПОДПИСАЛ_ВЫПОЛНЕНИЕ_ЗАЯВКА", staff.Code);
			// Отмечаем подачу заявки
			request = DbSqlStorageRequest.Execute(request);
			if(request != null)
			{
				request.SetLVItem(item);
			}
		}

		private void menuItem5_Click(object sender, System.EventArgs e)
		{
			// Архивация выбранной заявки
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			if(item.Tag == null) return;
			DtStorageRequest.CodeYear code_year = (DtStorageRequest.CodeYear)item.Tag;
			DtStorageRequest request = DbSqlStorageRequest.Find(code_year.code, code_year.year);
			if(request == null) return;
			// Запрос кода подписавшего заявку
			DbStaff staff = DbStaff.GetByESign("Электронная подпись");
			if(staff == null) return;
			request.SetData("КОД_ПОДПИСАЛ_АРХИВАЦИЯ", staff.Code);

			// Отмечаем подачу заявки
			if(DbSqlStorageRequest.Archive(request) == false) return;
			listView1.Items.Remove(item);
		}

		private void listView1_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
		{
			if(e.Column == 5)
			{
				// Запрос маски для поиска по имени контрагента
				FormSelectString dialog = new FormSelectString("Маска для поиска заказчика", "");
				if(dialog.ShowDialog() != DialogResult.OK) return;
				listView1.Items.Clear();
				DbSqlStorageRequest.SelectInList(listView1, dialog.SelectedTextMask);
			}
		}

		private void menuItem6_Click(object sender, System.EventArgs e)
		{
			// Запуск печати заявок по списку
			if(listView1.SelectedItems.Count == 0) return;
			DbPrintStorageRequest print = new DbPrintStorageRequest(listView1);
			print.Print();
		}
	}
}
