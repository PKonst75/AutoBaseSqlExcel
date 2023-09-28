using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormAutoStorageList.
	/// </summary>
	public class FormAutoStorageList : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Button buttonMakePreparation;
		private System.Windows.Forms.Button buttonAuto;
		private System.Windows.Forms.ColumnHeader columnHeader8;
		private System.Windows.Forms.ColumnHeader columnHeader9;
		private System.Windows.Forms.Button buttonReserv;
		private System.Windows.Forms.Button buttonPrice;
		private System.Windows.Forms.Button buttonUnReserv;
		private System.Windows.Forms.Button buttonComment;
		private System.Windows.Forms.Button buttonSell;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button buttonCheckPreparation;
		private System.ComponentModel.IContainer components;

		public FormAutoStorageList()
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormAutoStorageList));
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
			this.button1 = new System.Windows.Forms.Button();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.buttonMakePreparation = new System.Windows.Forms.Button();
			this.buttonReserv = new System.Windows.Forms.Button();
			this.buttonPrice = new System.Windows.Forms.Button();
			this.buttonUnReserv = new System.Windows.Forms.Button();
			this.buttonComment = new System.Windows.Forms.Button();
			this.buttonSell = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.buttonAuto = new System.Windows.Forms.Button();
			this.buttonCheckPreparation = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// listView1
			// 
			this.listView1.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.columnHeader1,
																						this.columnHeader2,
																						this.columnHeader3,
																						this.columnHeader4,
																						this.columnHeader5,
																						this.columnHeader6,
																						this.columnHeader7,
																						this.columnHeader8,
																						this.columnHeader9});
			this.listView1.FullRowSelect = true;
			this.listView1.HideSelection = false;
			this.listView1.Location = new System.Drawing.Point(8, 32);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(968, 336);
			this.listView1.TabIndex = 0;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listView1_KeyDown);
			this.listView1.ColumnClick += new ColumnClickEventHandler(this.listView1_ColumnClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Дата";
			this.columnHeader1.Width = 88;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Модель";
			this.columnHeader2.Width = 125;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Спецификация";
			this.columnHeader3.Width = 81;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Комплектация";
			this.columnHeader4.Width = 86;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "Цвет";
			this.columnHeader5.Width = 105;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "VIN";
			this.columnHeader6.Width = 157;
			// 
			// columnHeader7
			// 
			this.columnHeader7.Text = "Цена";
			this.columnHeader7.Width = 90;
			// 
			// columnHeader8
			// 
			this.columnHeader8.Text = "Доп Оборудование";
			this.columnHeader8.Width = 90;
			// 
			// columnHeader9
			// 
			this.columnHeader9.Text = "Примечание";
			this.columnHeader9.Width = 120;
			// 
			// button1
			// 
			this.button1.Image = ((System.Drawing.Bitmap)(resources.GetObject("button1.Image")));
			this.button1.Location = new System.Drawing.Point(8, 8);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(24, 23);
			this.button1.TabIndex = 1;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// buttonMakePreparation
			// 
			this.buttonMakePreparation.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonMakePreparation.Image")));
			this.buttonMakePreparation.Location = new System.Drawing.Point(184, 8);
			this.buttonMakePreparation.Name = "buttonMakePreparation";
			this.buttonMakePreparation.Size = new System.Drawing.Size(24, 23);
			this.buttonMakePreparation.TabIndex = 2;
			this.toolTip1.SetToolTip(this.buttonMakePreparation, "Отметить выполнение предпрадажной подготовки");
			this.buttonMakePreparation.Click += new System.EventHandler(this.buttonMakePreparation_Click);
			// 
			// buttonReserv
			// 
			this.buttonReserv.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonReserv.Image")));
			this.buttonReserv.Location = new System.Drawing.Point(232, 8);
			this.buttonReserv.Name = "buttonReserv";
			this.buttonReserv.Size = new System.Drawing.Size(24, 23);
			this.buttonReserv.TabIndex = 4;
			this.toolTip1.SetToolTip(this.buttonReserv, "Резервирование автомобиля");
			this.buttonReserv.Click += new System.EventHandler(this.buttonReserv_Click);
			// 
			// buttonPrice
			// 
			this.buttonPrice.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonPrice.Image")));
			this.buttonPrice.Location = new System.Drawing.Point(208, 8);
			this.buttonPrice.Name = "buttonPrice";
			this.buttonPrice.Size = new System.Drawing.Size(24, 23);
			this.buttonPrice.TabIndex = 5;
			this.toolTip1.SetToolTip(this.buttonPrice, "Изменение цены");
			this.buttonPrice.Click += new System.EventHandler(this.buttonPrice_Click);
			// 
			// buttonUnReserv
			// 
			this.buttonUnReserv.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonUnReserv.Image")));
			this.buttonUnReserv.Location = new System.Drawing.Point(256, 8);
			this.buttonUnReserv.Name = "buttonUnReserv";
			this.buttonUnReserv.Size = new System.Drawing.Size(24, 23);
			this.buttonUnReserv.TabIndex = 6;
			this.toolTip1.SetToolTip(this.buttonUnReserv, "Снятие  резервирования");
			this.buttonUnReserv.Click += new System.EventHandler(this.buttonUnReserv_Click);
			// 
			// buttonComment
			// 
			this.buttonComment.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonComment.Image")));
			this.buttonComment.Location = new System.Drawing.Point(280, 8);
			this.buttonComment.Name = "buttonComment";
			this.buttonComment.Size = new System.Drawing.Size(24, 23);
			this.buttonComment.TabIndex = 7;
			this.toolTip1.SetToolTip(this.buttonComment, "Изменение примечание");
			this.buttonComment.Click += new System.EventHandler(this.buttonComment_Click);
			// 
			// buttonSell
			// 
			this.buttonSell.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonSell.Image")));
			this.buttonSell.Location = new System.Drawing.Point(384, 8);
			this.buttonSell.Name = "buttonSell";
			this.buttonSell.Size = new System.Drawing.Size(24, 23);
			this.buttonSell.TabIndex = 8;
			this.toolTip1.SetToolTip(this.buttonSell, "Осуществить продажу");
			this.buttonSell.Click += new System.EventHandler(this.buttonSell_Click);
			// 
			// button2
			// 
			this.button2.Image = ((System.Drawing.Bitmap)(resources.GetObject("button2.Image")));
			this.button2.Location = new System.Drawing.Point(408, 8);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(24, 23);
			this.button2.TabIndex = 9;
			this.toolTip1.SetToolTip(this.button2, "Отметка о выходе проданного автомобиля");
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// buttonAuto
			// 
			this.buttonAuto.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonAuto.Image")));
			this.buttonAuto.Location = new System.Drawing.Point(32, 8);
			this.buttonAuto.Name = "buttonAuto";
			this.buttonAuto.Size = new System.Drawing.Size(24, 23);
			this.buttonAuto.TabIndex = 3;
			this.buttonAuto.Click += new System.EventHandler(this.buttonAuto_Click);
			// 
			// buttonCheckPreparation
			// 
			this.buttonCheckPreparation.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonCheckPreparation.Image")));
			this.buttonCheckPreparation.Location = new System.Drawing.Point(528, 8);
			this.buttonCheckPreparation.Name = "buttonCheckPreparation";
			this.buttonCheckPreparation.Size = new System.Drawing.Size(24, 23);
			this.buttonCheckPreparation.TabIndex = 10;
			this.toolTip1.SetToolTip(this.buttonCheckPreparation, "Проверить ВЕСЬ склад на ППП");
			this.buttonCheckPreparation.Click += new System.EventHandler(this.buttonCheckPreparation_Click);
			// 
			// FormAutoStorageList
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.ClientSize = new System.Drawing.Size(984, 373);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.buttonCheckPreparation,
																		  this.button2,
																		  this.buttonSell,
																		  this.buttonComment,
																		  this.buttonUnReserv,
																		  this.buttonPrice,
																		  this.buttonReserv,
																		  this.buttonAuto,
																		  this.buttonMakePreparation,
																		  this.button1,
																		  this.listView1});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.Name = "FormAutoStorageList";
			this.Text = "Склад Автомобилей";
			this.ResumeLayout(false);

		}
		#endregion

		private void button1_Click(object sender, System.EventArgs e)
		{
			// Обновление списка автомобилей на складе
			listView1.Items.Clear();
			DbAutoStorage.FillList(listView1);
		}

		private void buttonMakePreparation_Click(object sender, System.EventArgs e)
		{
			// Отмечаем выполнение ППП для выбранного автомобиля
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			DbAutoStorage element = (DbAutoStorage)item.Tag;
			if(element == null) return;
			// Запуск выбора заказ-наряда
			FormCardList dialog = new FormCardList(Db.ClickType.Select, 1, element.Auto);
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			// Запуск процедуры отметки о ППП
			if(element.WritePreparation(dialog.SelectedCard))
				element.SetLVItem(item);	
		}

		private void buttonAuto_Click(object sender, System.EventArgs e)
		{
			// Исправление свойств автомобиля
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			DbAutoStorage element = (DbAutoStorage)item.Tag;
			if(element == null) return;
			if(element.Auto == null) return;

			FormAuto dialog = new FormAuto(element.Auto);
			dialog.ShowDialog();

			// Теперь необходимо перезачесть выбранный элемент
			DbAutoStorage find = DbAutoStorage.Find(element.Code);
			if(find != null) find.SetLVItem(item);
		}

		private void buttonReserv_Click(object sender, System.EventArgs e)
		{
			// Резервирование автомобиля
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			DbAutoStorage element = (DbAutoStorage)item.Tag;
			if(element == null) return;
			
			// Запрос клиента, под которого резервируем автомобиль
			FormPartnerList dialog = new FormPartnerList();
			dialog.ShowDialog();
			if(dialog.DialogResult != DialogResult.OK) return;
			if(element.WriteReserve(dialog.Partner) != true) return;
			element.SetLVItem(item);
		}

		private void buttonPrice_Click(object sender, System.EventArgs e)
		{
			// Изменение цены автомобиля
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			DbAutoStorage element = (DbAutoStorage)item.Tag;
			if(element == null) return;
			// Запрос новой цены
			FormSelectString dialog = new FormSelectString("Новая цена", element.Price.ToString());
			dialog.ShowDialog();
			if(dialog.DialogResult != DialogResult.OK) return;
			if(element.WritePrice(dialog.SelectedFloat) != true) return;

			// Теперь необходимо перезачесть выбранный элемент
			DbAutoStorage find = DbAutoStorage.Find(element.Code);
			if(find != null) find.SetLVItem(item);
		}

		private void buttonUnReserv_Click(object sender, System.EventArgs e)
		{
			// Снятие резервировани
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			DbAutoStorage element = (DbAutoStorage)item.Tag;
			if(element == null) return;
			
			// Запрос клиента, под которого резервируем автомобиль
			if(element.WriteReserve(null) != true) return;
			element.SetLVItem(item);
		}

		private void listView1_KeyDown(object sender, KeyEventArgs e)
		{
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			DbAutoStorage element = (DbAutoStorage)item.Tag;
			if(element == null) return;

			if(e.KeyCode == Keys.I && e.Control == true)
			{
				// Выдача информации о складской позиции
				FormInfo dialog = new FormInfo(element, 0);
				dialog.ShowDialog();
			}
		}

		private void buttonComment_Click(object sender, System.EventArgs e)
		{
			// Изменение примечания
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			DbAutoStorage element = (DbAutoStorage)item.Tag;
			if(element == null) return;
			// Запрос новой цены
			FormSelectString dialog = new FormSelectString("Примечание", element.Comment);
			dialog.ShowDialog();
			element.Comment = dialog.SelectedText;
			if(element.Write() != true) return;

			// Теперь необходимо перезачесть выбранный элемент
			DbAutoStorage find = DbAutoStorage.Find(element.Code);
			if(find != null) find.SetLVItem(item);
		}

		private void buttonSell_Click(object sender, System.EventArgs e)
		{
			// Осуществить продажу
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			DbAutoStorage element = (DbAutoStorage)item.Tag;
			if(element == null) return;

			FormSell dialog = new FormSell(element);
			dialog.ShowDialog();
			if(dialog.DialogResult != DialogResult.OK) return;

			// Отмечаем успешную продажу автомобиля
			// Теперь необходимо перезачесть выбранный элемент
			DbAutoStorage find = DbAutoStorage.Find(element.Code);
			if(find != null) find.SetLVItem(item);
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			// Отмчаем, что автомобиль ушел за территорию
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			DbAutoStorage element = (DbAutoStorage)item.Tag;
			if(element == null) return;

			FormSelectDate dialog = new FormSelectDate();
			dialog.ShowDialog();
			if(dialog.DialogResult != DialogResult.OK) return;
			if(element.WriteOutcom(dialog.SelectedDate) != true) return;

			item.Remove();
		}

		private void buttonCheckPreparation_Click(object sender, System.EventArgs e)
		{
			// Проверяем все складские позиции на предмет предпродажной подготовки
			if(MessageBox.Show(this, "Вы уверены?", "Запрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question)== DialogResult.No) return;
			DbAutoStorage.ActionPraparation();
			listView1.Items.Clear();
			DbAutoStorage.FillList(listView1);
		}

		protected void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
		{
			if(e.Column == 5)
			{
				// Поиск по номеру кузова
				FormSelectString dialog = new FormSelectString("Номер кузова", "");
				dialog.ShowDialog();
				if(dialog.DialogResult != DialogResult.OK) return;
				listView1.Items.Clear();
				DbAutoStorage.FillListSearchBody(listView1, dialog.SelectedTextMask);
				return;
			}
		}
	}
}
