using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormAutoList.
	/// </summary>
	public class FormAutoList : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader ColumnHeader1;
		private System.Windows.Forms.ColumnHeader ColumnHeader2;
		private System.Windows.Forms.Button buttonNew;
		private System.Windows.Forms.Button buttonUpdate;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.Button buttonChange;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		/// <summary>
		/// Required designer variable.
		/// </summary>

		private DbAuto auto = null;
		private System.ComponentModel.Container components = null;
		private Db.ClickType clickType;
		private System.Windows.Forms.Button buttonAutoList;
		private System.Windows.Forms.Button buttonAddPartnerAuto;
		private System.Windows.Forms.Button buttonDeletePartnerAuto;
		private System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private DbPartner owner;
		private System.Windows.Forms.MenuItem menuItem3;
		DbAuto	contextMenuElement	= null;

		public FormAutoList(Db.ClickType clickTypeSrc, DbPartner ownerSrc)
		{
			InitializeComponent();

			clickType	= clickTypeSrc;
			owner		= ownerSrc;
			if(owner != null)
			{
				this.Text += " / Владелец - " + owner.NameShort;
				DbAuto.FillList(listView1, owner);
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormAutoList));
			this.listView1 = new System.Windows.Forms.ListView();
			this.ColumnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.ColumnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.buttonNew = new System.Windows.Forms.Button();
			this.buttonUpdate = new System.Windows.Forms.Button();
			this.buttonChange = new System.Windows.Forms.Button();
			this.buttonAutoList = new System.Windows.Forms.Button();
			this.buttonAddPartnerAuto = new System.Windows.Forms.Button();
			this.buttonDeletePartnerAuto = new System.Windows.Forms.Button();
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.SuspendLayout();
			// 
			// listView1
			// 
			this.listView1.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.ColumnHeader1,
																						this.ColumnHeader2,
																						this.columnHeader4,
																						this.columnHeader5});
			this.listView1.FullRowSelect = true;
			this.listView1.GridLines = true;
			this.listView1.Location = new System.Drawing.Point(8, 40);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(608, 280);
			this.listView1.TabIndex = 0;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listView1_KeyDown);
			this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
			this.listView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseUp);
			this.listView1.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView1_ColumnClick);
			// 
			// ColumnHeader1
			// 
			this.ColumnHeader1.Text = "Модель";
			this.ColumnHeader1.Width = 120;
			// 
			// ColumnHeader2
			// 
			this.ColumnHeader2.Text = "Кузов №";
			this.ColumnHeader2.Width = 90;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Гос.Номер";
			this.columnHeader4.Width = 90;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "Дополнительно";
			this.columnHeader5.Width = 200;
			// 
			// buttonNew
			// 
			this.buttonNew.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonNew.Image")));
			this.buttonNew.Location = new System.Drawing.Point(8, 8);
			this.buttonNew.Name = "buttonNew";
			this.buttonNew.Size = new System.Drawing.Size(24, 23);
			this.buttonNew.TabIndex = 1;
			this.buttonNew.Click += new System.EventHandler(this.buttonNew_Click);
			// 
			// buttonUpdate
			// 
			this.buttonUpdate.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonUpdate.Image")));
			this.buttonUpdate.Location = new System.Drawing.Point(32, 8);
			this.buttonUpdate.Name = "buttonUpdate";
			this.buttonUpdate.Size = new System.Drawing.Size(24, 23);
			this.buttonUpdate.TabIndex = 2;
			this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
			// 
			// buttonChange
			// 
			this.buttonChange.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonChange.Image")));
			this.buttonChange.Location = new System.Drawing.Point(56, 8);
			this.buttonChange.Name = "buttonChange";
			this.buttonChange.Size = new System.Drawing.Size(24, 23);
			this.buttonChange.TabIndex = 3;
			this.buttonChange.Click += new System.EventHandler(this.buttonChange_Click);
			// 
			// buttonAutoList
			// 
			this.buttonAutoList.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonAutoList.Image")));
			this.buttonAutoList.Location = new System.Drawing.Point(184, 8);
			this.buttonAutoList.Name = "buttonAutoList";
			this.buttonAutoList.Size = new System.Drawing.Size(24, 23);
			this.buttonAutoList.TabIndex = 4;
			this.buttonAutoList.Click += new System.EventHandler(this.buttonAutoList_Click);
			// 
			// buttonAddPartnerAuto
			// 
			this.buttonAddPartnerAuto.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonAddPartnerAuto.Image")));
			this.buttonAddPartnerAuto.Location = new System.Drawing.Point(208, 8);
			this.buttonAddPartnerAuto.Name = "buttonAddPartnerAuto";
			this.buttonAddPartnerAuto.Size = new System.Drawing.Size(24, 23);
			this.buttonAddPartnerAuto.TabIndex = 5;
			this.buttonAddPartnerAuto.Click += new System.EventHandler(this.buttonAddPartnerAuto_Click);
			// 
			// buttonDeletePartnerAuto
			// 
			this.buttonDeletePartnerAuto.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonDeletePartnerAuto.Image")));
			this.buttonDeletePartnerAuto.Location = new System.Drawing.Point(232, 8);
			this.buttonDeletePartnerAuto.Name = "buttonDeletePartnerAuto";
			this.buttonDeletePartnerAuto.Size = new System.Drawing.Size(24, 23);
			this.buttonDeletePartnerAuto.TabIndex = 6;
			this.buttonDeletePartnerAuto.Click += new System.EventHandler(this.buttonDeletePartnerAuto_Click);
			// 
			// contextMenu1
			// 
			this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.menuItem1,
																						 this.menuItem3});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem2});
			this.menuItem1.Text = "Печать";
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 0;
			this.menuItem2.Text = "Карточка Автомобиля";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 1;
			this.menuItem3.Text = "Установть/Сменить регистрационный знак";
			this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
			// 
			// FormAutoList
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(624, 325);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.buttonDeletePartnerAuto,
																		  this.buttonAddPartnerAuto,
																		  this.buttonAutoList,
																		  this.buttonChange,
																		  this.buttonUpdate,
																		  this.buttonNew,
																		  this.listView1});
			this.Name = "FormAutoList";
			this.Text = "Список автомобилей";
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonNew_Click(object sender, System.EventArgs e)
		{
			// Диалог добавления новых автомобилей
			FormAuto dialog = new FormAuto(null);
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			listView1.Items.Add(dialog.Auto.LVItem);
		}

		private void buttonUpdate_Click(object sender, System.EventArgs e)
		{
			// Обновление списка
			listView1.Items.Clear();
			DbAuto.FillList(listView1, 0, "");
		}

		private void buttonChange_Click(object sender, System.EventArgs e)
		{
			// Изменение свойств выбранного элемента
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			DbAuto auto = (DbAuto)item.Tag;
			if(auto == null) return;
			FormAuto dialog = new FormAuto(auto);
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			dialog.Auto.SetLVItem(item);
		}

		protected void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
		{
			if(e.Column == 0)
			{
				// Помодельный поиск
				FormSelectString dialog = new FormSelectString("Модель автомобиля", "Марка автомобиля");
				dialog.ShowDialog(this);
				if(dialog.DialogResult != DialogResult.OK) return;
				string text = dialog.SelectedTextMask;
				listView1.Items.Clear();
				DbAuto.FillList(listView1, 1, text);
			}

			if(e.Column == 1)
			{
				// Поиск по номеру кузова
				FormSelectString dialog = new FormSelectString("Номер кузова автомобиля", "Номер кузова");
				dialog.ShowDialog(this);
				if(dialog.DialogResult != DialogResult.OK) return;
				string text = dialog.SelectedTextMask;
				listView1.Items.Clear();
				DbAuto.FillList(listView1, 2, text);
			}
			if(e.Column == 2)
			{
				// Поиск номерному знаку
				FormSelectString dialog = new FormSelectString("Регистрационный знак автомобиля", "Регистрационный знак");
				dialog.ShowDialog(this);
				if(dialog.DialogResult != DialogResult.OK) return;
				string text = dialog.SelectedTextMask;
				listView1.Items.Clear();
				DbAuto.FillList(listView1, 3, text);
			}
		}

		public DbAuto Auto
		{
			get
			{
				return auto;
			}
		}

		protected void listView1_DoubleClick(object sender, System.EventArgs e)
		{
			ListViewItem item = Db.GetItemPosition(listView1);
			if(item == null) return;
			auto = (DbAuto)item.Tag;
			if(auto == null) return;

			if(clickType == Db.ClickType.Select)
			{
				this.DialogResult = DialogResult.OK;
				this.Close();
				return;
			}
		}

		private void buttonAutoList_Click(object sender, System.EventArgs e)
		{
			// Показ списка автомобилей, принадлежащих владельцу
			if(owner == null) return;
			listView1.Items.Clear();
			DbAuto.FillList(listView1, owner);
		}

		private void buttonAddPartnerAuto_Click(object sender, System.EventArgs e)
		{
			// Добавление связи автомобиля с контрагентом
			if(owner == null) return;
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			DbAuto auto = (DbAuto)item.Tag;
			if(auto == null) return;
			auto.WritePartnerAuto(owner, true);
		}

		private void buttonDeletePartnerAuto_Click(object sender, System.EventArgs e)
		{
			// Удаление связи автомобиля с контрагентом
			if(owner == null) return;
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			DbAuto auto = (DbAuto)item.Tag;
			if(auto == null) return;
			auto.WritePartnerAuto(owner, false);
		}

		private void listView1_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.Control == true)
			{
				// Нажатие кнопки совместно с Control
				if(e.KeyCode == Keys.A)
				{
					// Вызов поиска автомобиля по штрих-коду
					if(Db.barcCodeType == Db.BarCodes.Unknown) return;	// Нет типа штрихкода
					FormBarCodeGet dialog = new FormBarCodeGet();
					dialog.ShowDialog();
					// Необходимо получить строку поиска автомобилей по штрих-коду
					ClassBarCode barCode = new ClassBarCode(dialog.BarCode);
					if(barCode.ValidAuto != true)
					{
						MessageBox.Show("Ошибка чтения штрих-кода");
						return;
					}
					listView1.Items.Clear();
					DbAuto.FillListBarCode(listView1, barCode.AutoModelMask, barCode.AutoBody);
				}
			}
		}

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			// Печать карточки выбранного автомобиля
			if(contextMenuElement== null) return;
			PrintAutoCoupon printAuto = new PrintAutoCoupon(contextMenuElement);
			printAuto.Print();
			contextMenuElement= null;
		}
		protected void listView1_MouseUp(object sender, MouseEventArgs e)
		{
			contextMenuElement = null;

			ListViewItem item = Db.GetItemPosition(listView1);
			if(item == null) return;
			DbAuto auto = (DbAuto)item.Tag;
			if(auto == null) return;

			if(e.Button == MouseButtons.Right)
			{
				contextMenuElement = auto;
				Point pos = new Point(e.X, e.Y);
				contextMenu1.Show(listView1, pos);
			}
		}

		private void menuItem3_Click(object sender, System.EventArgs e)
		{
			// Смена или установка регистрационного знака автомобиля
			if(contextMenuElement== null) return;
			long code_auto	= contextMenuElement.Code;
			string plate	= contextMenuElement.SignNo;
			UIF_LicencePlate dialog = new UIF_LicencePlate(code_auto, plate);
			dialog.ShowDialog();
			contextMenuElement= null;
		}
	}
}
