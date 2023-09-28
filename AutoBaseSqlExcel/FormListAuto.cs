using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormListAuto.
	/// </summary>
	public class FormListAuto : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader head_model;
		private System.Windows.Forms.ColumnHeader head_vin;
		private System.Windows.Forms.ColumnHeader head_body;
		private System.Windows.Forms.ColumnHeader head_sign;
		private System.Windows.Forms.ColumnHeader head_factory;
		private System.Windows.Forms.ColumnHeader head_comment;
		private System.Windows.Forms.Button button_update;
		private System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.MenuItem menuItem8;
		private System.Windows.Forms.MenuItem menuItem9;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.MenuItem menuItem10;
		private System.Windows.Forms.MenuItem menuItem11;
		private System.Windows.Forms.MenuItem menuItem12;
        private MenuItem menuItem13;

		private DtAuto auto;

		public FormListAuto(int type, object o)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			head_model = new System.Windows.Forms.ColumnHeader();
			head_model.Width = 120;
			head_model.Text = "Модель";
			head_vin = new System.Windows.Forms.ColumnHeader();
			head_vin.Width = 120;
			head_vin.Text = "VIN";
			head_body = new System.Windows.Forms.ColumnHeader();
			head_body.Width = 80;
			head_body.Text = "№ кузова";
			head_factory = new System.Windows.Forms.ColumnHeader();
			head_factory.Width = 120;
			head_factory.Text = "Производитель";
			head_comment = new System.Windows.Forms.ColumnHeader();
			head_comment.Width = 256;
			head_comment.Text = "Примечание";
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.head_model,
																						this.head_vin,
																						this.head_body,
																						this.head_factory,
																						this.head_comment
																					});

			// Заполнение по требованию
			switch(type)
			{
				case 1:
					DbSqlAuto.SelectInListModel(listView1, (long)o);
					break;
				case 2:
					// Поиск по VIN или кузову
					string mask = "%" + (string)o + "%";
					DbSqlAuto.SelectInListVin(listView1, (string)mask);
					break;
				default:
					break;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormListAuto));
            this.listView1 = new System.Windows.Forms.ListView();
            this.button_update = new System.Windows.Forms.Button();
            this.contextMenu1 = new System.Windows.Forms.ContextMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.menuItem8 = new System.Windows.Forms.MenuItem();
            this.menuItem9 = new System.Windows.Forms.MenuItem();
            this.menuItem10 = new System.Windows.Forms.MenuItem();
            this.menuItem11 = new System.Windows.Forms.MenuItem();
            this.menuItem12 = new System.Windows.Forms.MenuItem();
            this.menuItem13 = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new System.Drawing.Point(8, 40);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(648, 296);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
            this.listView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseUp);
            this.listView1.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView1_ColumnClick);
            // 
            // button_update
            // 
            this.button_update.Image = ((System.Drawing.Image)(resources.GetObject("button_update.Image")));
            this.button_update.Location = new System.Drawing.Point(8, 16);
            this.button_update.Name = "button_update";
            this.button_update.Size = new System.Drawing.Size(24, 23);
            this.button_update.TabIndex = 1;
            this.button_update.Click += new System.EventHandler(this.button_update_Click);
            // 
            // contextMenu1
            // 
            this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.menuItem3,
            this.menuItem7,
            this.menuItem10,
            this.menuItem13});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem2});
            this.menuItem1.Text = "Поиск";
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 0;
            this.menuItem2.Text = "Заказ-Наряды";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 1;
            this.menuItem3.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem4,
            this.menuItem5,
            this.menuItem6});
            this.menuItem3.Text = "Автомобиль";
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 0;
            this.menuItem4.Text = "Новый";
            this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 1;
            this.menuItem5.Text = "Свойства";
            this.menuItem5.Click += new System.EventHandler(this.menuItem5_Click);
            // 
            // menuItem6
            // 
            this.menuItem6.Index = 2;
            this.menuItem6.Text = "Удалить";
            this.menuItem6.Click += new System.EventHandler(this.menuItem6_Click);
            // 
            // menuItem7
            // 
            this.menuItem7.Index = 2;
            this.menuItem7.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem8,
            this.menuItem9});
            this.menuItem7.Text = "Служебные";
            // 
            // menuItem8
            // 
            this.menuItem8.Index = 0;
            this.menuItem8.Text = "Показать дубли";
            this.menuItem8.Click += new System.EventHandler(this.menuItem8_Click);
            // 
            // menuItem9
            // 
            this.menuItem9.Index = 1;
            this.menuItem9.Text = "Заменить автомобиль";
            this.menuItem9.Click += new System.EventHandler(this.menuItem9_Click);
            // 
            // menuItem10
            // 
            this.menuItem10.Index = 3;
            this.menuItem10.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem11,
            this.menuItem12});
            this.menuItem10.Text = "Отметки";
            // 
            // menuItem11
            // 
            this.menuItem11.Index = 0;
            this.menuItem11.Text = "Установка Сигнализации";
            this.menuItem11.Click += new System.EventHandler(this.menuItem11_Click);
            // 
            // menuItem12
            // 
            this.menuItem12.Index = 1;
            this.menuItem12.Text = "Управление Примечаниями";
            this.menuItem12.Click += new System.EventHandler(this.menuItem12_Click);
            // 
            // menuItem13
            // 
            this.menuItem13.Index = 4;
            this.menuItem13.Text = "Установить ПТС";
            this.menuItem13.Click += new System.EventHandler(this.menuItem13_Click);
            // 
            // FormListAuto
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(664, 341);
            this.Controls.Add(this.button_update);
            this.Controls.Add(this.listView1);
            this.Name = "FormListAuto";
            this.Text = "Список автомобилей";
            this.ResumeLayout(false);

		}
		#endregion

		private void button_update_Click(object sender, System.EventArgs e)
		{
			// Обновить список
			listView1.Items.Clear();
			DbSqlAuto.SelectInList(listView1);
		}

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			// Показываем все заказ-няряды зарегестрированные на автомобиль
			// Отменить карточку
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			long code_auto = (long)item.Tag;
			if(code_auto == 0) return;

			FormManageCard dialog = new FormManageCard(Db.ClickType.Properties, 1, (object)code_auto);
			dialog.Show();
		}

		private void listView1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if(e.Button == MouseButtons.Right)
			{
				// На отпускание правой кнопки мышки - всплывающее меню
				// Настройка меню
				// Права пользователя
				// Отключаем все запрещенное
				menuItem6.Enabled = false;
				menuItem7.Enabled = false;
				
				// Включаем по разрешению
				string login = Form1.currentLogin.ToLower();
				if (login == "админ")
				{
					menuItem6.Enabled = true;
					menuItem7.Enabled = true;
				}
				// Настройка меню исходя из свойств выбранной карточки
				// Показ меню
				contextMenu1.Show(listView1, new Point(e.X, e.Y));
			}
		}

		private void menuItem4_Click(object sender, System.EventArgs e)
		{
			// Добавление нового автомобиля
			FormUpdateAuto dialog = new FormUpdateAuto(0, "");
			dialog.ShowDialog();
		}

		private void menuItem6_Click(object sender, System.EventArgs e)
		{
			// Удалить выбранный автомобиль
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			long code_auto = (long)item.Tag;
			if(code_auto == 0) return;

			// Запрос подтверждения
			if(MessageBox.Show("Вы уверены что хотите удалить автомобиль?", "Запрос", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
			if(DbSqlAuto.Delete(code_auto) == false) return;

			listView1.Items.Remove(item);

		}

		private void listView1_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
		{
			FormSelectString dialog;
			// Поиски по критериям
			switch(e.Column)
			{
				case 1:
					// Поиск по VIN номеру
					dialog = new FormSelectString("Поиск по VIN", "");
					if(dialog.ShowDialog() != DialogResult.OK) return;
					listView1.Items.Clear();
					DbSqlAuto.SelectInListVin(listView1, dialog.SelectedTextMask);
					break;
				case 2:
					// Поиск по номеру кузова
					dialog = new FormSelectString("Поиск по номеру кузова", "");
					if(dialog.ShowDialog() != DialogResult.OK) return;
					listView1.Items.Clear();
					DbSqlAuto.SelectInListBody(listView1, dialog.SelectedTextMask);
					break;
				default:
					break;
			}
		}

		private void menuItem5_Click(object sender, System.EventArgs e)
		{
			// Изменение свойств выбранного автомобиля
			// Удалить выбранный автомобиль
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			long code_auto = (long)item.Tag;
			if(code_auto == 0) return;

			FormUpdateAuto dialog = new FormUpdateAuto(code_auto, "");
			dialog.Show();
		}

		private void menuItem8_Click(object sender, System.EventArgs e)
		{
			// Показать список дублей
			listView1.Items.Clear();
			DbSqlAuto.SelectInListDouble(listView1);
		}

		private void menuItem9_Click(object sender, System.EventArgs e)
		{
			// Выбор автомобиля
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			long code_auto = (long)item.Tag;
			if(code_auto == 0) return;

			// Запрос нового автомобиля
			FormListAuto dialog = new FormListAuto(0, null);
			if(dialog.ShowDialog() != DialogResult.OK) return;

			// Производим замену
			if(DbSqlAuto.AuxiliaryAutoReplace(code_auto, (long)dialog.Auto.GetData("КОД_АВТОМОБИЛЬ")) == false) return;
			MessageBox.Show("Успешно");
		}

		private void listView1_DoubleClick(object sender, System.EventArgs e)
		{
			// Выбор автомобиля
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			long code_auto = (long)item.Tag;
			if(code_auto == 0) return;

			auto = DbSqlAuto.Find(code_auto);
			if(auto == null) return;
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void menuItem11_Click(object sender, System.EventArgs e)
		{
			// Отметка об установке сигнализации
			// Выбор автомобиля
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			long code_auto = (long)item.Tag;
			if(code_auto == 0) return;

			UserInterface.AutoAlarm(0, (object)code_auto, 0, UserInterface.ClickType.Modify);
		}

		private void menuItem12_Click(object sender, System.EventArgs e)
		{
			// Управление списком примечаний
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			long code_auto = (long)item.Tag;
			if(code_auto == 0) return;

			UserInterface.ListAutoComment(0, (object)code_auto, 0, UserInterface.ClickType.Modify);
		}
	
		public DtAuto Auto
		{
			get
			{
				return auto;
			}
		}

        private void menuItem13_Click(object sender, EventArgs e)
        {
            // Управление списком примечаний
            ListViewItem item = Db.GetItemSelected(listView1);
            if (item == null) return;
            long code_auto = (long)item.Tag;
            if (code_auto == 0) return;

            FormAutoPts form = new FormAutoPts(code_auto, 1);
            form.ShowDialog();
        }
	}
}
