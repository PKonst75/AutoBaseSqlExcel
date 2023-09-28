using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormListPartner.
	/// </summary>
	public class FormListPartner : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.Button button_new;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.MenuItem menuItem5;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.MenuItem menuItem8;
		private System.Windows.Forms.MenuItem menuItem9;
		private System.Windows.Forms.MenuItem menuItem10;
        private MenuItem menuItem11;

		long selected_code;

		public FormListPartner(int type, object obj)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			switch(type)
			{
				case 1:
					foreach(object o in (ArrayList)obj)
					{
						DtPartner element = (DtPartner)o;
						ListViewItem item = listView1.Items.Add("");
						element.SetLVItem(item);
					}
					break;
				case 2:
					// Осуществляем поиск по введенной части наименования
					// Ищем в фамилии, кратком наименовании, юридическом наименованиии
					string mask = (string)obj;
					// Дополняем маску
					mask = "%" + mask + "%";
					listView1.Items.Clear();
					DbSqlPartner.SelectInList(listView1, mask);
					break;
				default:
					// Заполняем всеми контрагентами
					// DbSqlPartner.SelectInList(listView1);
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormListPartner));
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
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
            this.button_new = new System.Windows.Forms.Button();
            this.menuItem11 = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView1.Location = new System.Drawing.Point(16, 48);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(624, 272);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
            this.listView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseUp);
            this.listView1.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView1_ColumnClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Краткое наименование";
            this.columnHeader1.Width = 200;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "ФИО / Юр. Наименование";
            this.columnHeader2.Width = 200;
            // 
            // contextMenu1
            // 
            this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.menuItem3,
            this.menuItem5,
            this.menuItem6,
            this.menuItem7,
            this.menuItem8,
            this.menuItem9,
            this.menuItem11});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem2});
            this.menuItem1.Text = "Служебное";
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 0;
            this.menuItem2.Text = "Удалить";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 1;
            this.menuItem3.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem4});
            this.menuItem3.Text = "Поиск";
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 0;
            this.menuItem4.Text = "Заказ-наряды";
            this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 2;
            this.menuItem5.Text = "Свойства";
            this.menuItem5.Click += new System.EventHandler(this.menuItem5_Click);
            // 
            // menuItem6
            // 
            this.menuItem6.Index = 3;
            this.menuItem6.Text = "Контакты";
            this.menuItem6.Click += new System.EventHandler(this.menuItem6_Click);
            // 
            // menuItem7
            // 
            this.menuItem7.Index = 4;
            this.menuItem7.Text = "Зарегистрировать звонок";
            this.menuItem7.Click += new System.EventHandler(this.menuItem7_Click);
            // 
            // menuItem8
            // 
            this.menuItem8.Index = 5;
            this.menuItem8.Text = "-";
            // 
            // menuItem9
            // 
            this.menuItem9.Index = 6;
            this.menuItem9.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem10});
            this.menuItem9.Text = "Выборка";
            // 
            // menuItem10
            // 
            this.menuItem10.Index = 0;
            this.menuItem10.Text = "Дни рождения";
            this.menuItem10.Click += new System.EventHandler(this.menuItem10_Click);
            // 
            // button_new
            // 
            this.button_new.Image = ((System.Drawing.Image)(resources.GetObject("button_new.Image")));
            this.button_new.Location = new System.Drawing.Point(16, 24);
            this.button_new.Name = "button_new";
            this.button_new.Size = new System.Drawing.Size(24, 23);
            this.button_new.TabIndex = 1;
            this.button_new.Click += new System.EventHandler(this.button_new_Click);
            // 
            // menuItem11
            // 
            this.menuItem11.Index = 7;
            this.menuItem11.Text = "Ввести данные паспорта";
            this.menuItem11.Click += new System.EventHandler(this.menuItem11_Click);
            // 
            // FormListPartner
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
            this.ClientSize = new System.Drawing.Size(648, 333);
            this.Controls.Add(this.button_new);
            this.Controls.Add(this.listView1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "FormListPartner";
            this.Text = "Контрагенты";
            this.ResumeLayout(false);

		}
		#endregion

		private void menuItem4_Click(object sender, System.EventArgs e)
		{
			// Список заказ-нарядов данного контрагента
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			if(item.Tag == null) return;
			long code_partner = (long)item.Tag;
			if(code_partner == 0) return;

			FormManageCard dialog = new FormManageCard(Db.ClickType.Properties, 2, (object)code_partner);
			dialog.Show();
		}

		private void listView1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			// Вызов меню
			if(e.Button == MouseButtons.Right)
			{
				// На отпускание правой кнопки мышки - всплывающее меню
				// Настройка меню
				// Права пользователя
				// Отключаем все запрещенное
				menuItem1.Enabled = false;
				
				// Включаем по разрешению
				string login = Form1.currentLogin.ToLower();
				if (login == "админ")
				{
					menuItem1.Enabled = true;
				}
				// Настройка меню исходя из свойств выбранной карточки
				// Показ меню
				contextMenu1.Show(listView1, new Point(e.X, e.Y));
			}
		}

		private void button_new_Click(object sender, System.EventArgs e)
		{
			// Новый контрагент
			FormUpdatePartner dialog = new FormUpdatePartner(0);
			dialog.SetConnection(listView1);
			if(dialog.ShowDialog() != DialogResult.OK) return;
		}

		private void menuItem5_Click(object sender, System.EventArgs e)
		{
			// Свойства контрагента
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			if(item.Tag == null) return;
			long code_partner = (long)item.Tag;
			if(code_partner == 0) return;

			FormUpdatePartner dialog = new FormUpdatePartner(code_partner);
			dialog.SetConnection(item);
			dialog.Show();
		}

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			// Удаляем контрагента
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			if(item.Tag == null) return;
			long code_partner = (long)item.Tag;
			if(code_partner == 0) return;

			if(MessageBox.Show("Уверенны что хотите удалить контаргента? " + item.Text, "Запрос", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
			if(DbSqlPartner.Delete(code_partner) != true) return;
			listView1.Items.Remove(item);
		}

		private void listView1_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
		{
			// Поиск по колонкам
			switch(e.Column)
			{
				case 0:
					FormSelectString dialog = new FormSelectString("Поиск по наименованию / фамилии", "");
					if(dialog.ShowDialog() != DialogResult.OK) return;
					listView1.Items.Clear();
					DbSqlPartner.SelectInList(listView1, dialog.SelectedTextMask);
					break;
				default:
					break;
			}

		}

		private void listView1_DoubleClick(object sender, System.EventArgs e)
		{
			// Инициируем выбор
			// Удаляем контрагента
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			if(item.Tag == null) return;
			long code_partner = (long)item.Tag;
			if(code_partner == 0) return;

			selected_code = code_partner;
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void menuItem6_Click(object sender, System.EventArgs e)
		{
			// Список контактов контрагента
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			if(item.Tag == null) return;
			long code_partner = (long)item.Tag;
			if(code_partner == 0) return;

			FormListPartnerContact dialog = new FormListPartnerContact(code_partner);
			dialog.Show();
		}

		private void menuItem7_Click(object sender, System.EventArgs e)
		{
			// Запуск функции регистрации звонка контрагенту
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			if(item.Tag == null) return;
			long code_partner = (long)item.Tag;
			if(code_partner == 0) return;

			FormListPartnerContact dialog = new FormListPartnerContact(code_partner);
			if(dialog.ShowDialog() != DialogResult.OK) return;
		}

		private void menuItem10_Click(object sender, System.EventArgs e)
		{
			// Показ списка всех контрагентов с днем рождения на выбранную дату
			FormSelectDate form = new FormSelectDate();
			if (form.ShowDialog() != DialogResult.OK) return;
			DateTime date = form.SelectedDate;
			listView1.Items.Clear();
			DbSqlPartner.SelectInList(listView1, date);
		}

		public long SelectedCode
		{
			get
			{
				return selected_code;
			}
		}

        private void menuItem11_Click(object sender, EventArgs e)
        {
            // Вводим данные паспорта выбранного контрагента
            // Запуск функции регистрации звонка контрагенту
            ListViewItem item = Db.GetItemSelected(listView1);
            if (item == null) return;
            if (item.Tag == null) return;
            long code_partner = (long)item.Tag;
            if (code_partner == 0) return;

            FormPassport dialog = new FormPassport(code_partner, 1);
            if (dialog.ShowDialog() != DialogResult.OK) return;
        }
	}
}
