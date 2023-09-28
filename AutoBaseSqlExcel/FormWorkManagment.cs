using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormWorkManagment.
	/// </summary>
	public class FormWorkManagment : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button_work_collection;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.TextBox textBox_work_group;
		private System.Windows.Forms.TextBox textBox_description;
		private System.Windows.Forms.ListView listView_work;
		private System.Windows.Forms.CheckBox checkBox_show_common;
		private System.Windows.Forms.Button button_update_list;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.ColumnHeader columnHeader8;
		private System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.ColumnHeader columnHeader9;
		private System.Windows.Forms.ColumnHeader columnHeader10;
		private System.Windows.Forms.ColumnHeader columnHeader11;
		private System.Windows.Forms.ColumnHeader columnHeader12;
		private System.Windows.Forms.ColumnHeader columnHeader13;
		private System.Windows.Forms.ColumnHeader columnHeader14;
		private System.Windows.Forms.ListView listView_details;
		private System.Windows.Forms.CheckBox checkBox_null_quontity;


		long selected_code_work_group	= 0;
		FormCard form_card				= null;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton radioButton_work;
		private System.Windows.Forms.RadioButton radioButton_detail;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.ColumnHeader columnHeader15;
		private System.Windows.Forms.ContextMenu contextMenu2;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.MenuItem menuItem8;
		ListView list_works				= null;

		public FormWorkManagment(FormCard form)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			// Первичная настройка формы
			checkBox_show_common.Checked	= true;
			radioButton_work.Checked		= true;

			// Анализ в случае ненулевой формы карточки заказа
			if(form != null)
			{
				form_card		= form;
				DbCard	card	= form_card.Card;
				if(card.AutoType != null)
					selected_code_work_group	= card.AutoType.Code;
				if(selected_code_work_group != 0)
				{
					// Заполняем список
					textBox_work_group.Text = card.AutoTypeTxt;
					bool show_common = checkBox_show_common.Checked;
					listView_work.Items.Clear();
					DbSqlWork.SelectInList(listView_work, selected_code_work_group, show_common, 0, "");
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormWorkManagment));
            this.button_work_collection = new System.Windows.Forms.Button();
            this.listView_work = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader15 = new System.Windows.Forms.ColumnHeader();
            this.textBox_work_group = new System.Windows.Forms.TextBox();
            this.textBox_description = new System.Windows.Forms.TextBox();
            this.checkBox_show_common = new System.Windows.Forms.CheckBox();
            this.button_update_list = new System.Windows.Forms.Button();
            this.contextMenu1 = new System.Windows.Forms.ContextMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.listView_details = new System.Windows.Forms.ListView();
            this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader10 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader11 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader12 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader13 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader14 = new System.Windows.Forms.ColumnHeader();
            this.checkBox_null_quontity = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton_detail = new System.Windows.Forms.RadioButton();
            this.radioButton_work = new System.Windows.Forms.RadioButton();
            this.contextMenu2 = new System.Windows.Forms.ContextMenu();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.menuItem8 = new System.Windows.Forms.MenuItem();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_work_collection
            // 
            this.button_work_collection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_work_collection.Location = new System.Drawing.Point(904, 0);
            this.button_work_collection.Name = "button_work_collection";
            this.button_work_collection.Size = new System.Drawing.Size(75, 23);
            this.button_work_collection.TabIndex = 0;
            this.button_work_collection.Text = "Наборы";
            this.button_work_collection.Click += new System.EventHandler(this.button_work_collection_Click);
            // 
            // listView_work
            // 
            this.listView_work.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listView_work.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader15});
            this.listView_work.FullRowSelect = true;
            this.listView_work.HideSelection = false;
            this.listView_work.Location = new System.Drawing.Point(8, 88);
            this.listView_work.Name = "listView_work";
            this.listView_work.Size = new System.Drawing.Size(808, 248);
            this.listView_work.TabIndex = 0;
            this.listView_work.UseCompatibleStateImageBehavior = false;
            this.listView_work.View = System.Windows.Forms.View.Details;
            this.listView_work.SelectedIndexChanged += new System.EventHandler(this.listView_work_SelectedIndexChanged);
            this.listView_work.DoubleClick += new System.EventHandler(this.listView_work_DoubleClick);
            this.listView_work.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listView_work_MouseUp);
            this.listView_work.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView_work_ColumnClick);
            this.listView_work.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.listView_work_KeyPress);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Позиция";
            this.columnHeader1.Width = 80;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Код детали";
            this.columnHeader2.Width = 73;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Работа";
            this.columnHeader3.Width = 50;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Наименование";
            this.columnHeader4.Width = 259;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Труд.";
            this.columnHeader5.Width = 55;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Стоимость н/ч";
            this.columnHeader6.Width = 88;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Сумма";
            this.columnHeader7.Width = 82;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "";
            this.columnHeader8.Width = 20;
            // 
            // columnHeader15
            // 
            this.columnHeader15.Text = "НВ";
            // 
            // textBox_work_group
            // 
            this.textBox_work_group.Location = new System.Drawing.Point(8, 64);
            this.textBox_work_group.Name = "textBox_work_group";
            this.textBox_work_group.ReadOnly = true;
            this.textBox_work_group.Size = new System.Drawing.Size(240, 20);
            this.textBox_work_group.TabIndex = 2;
            this.textBox_work_group.DoubleClick += new System.EventHandler(this.textBox_work_group_DoubleClick);
            // 
            // textBox_description
            // 
            this.textBox_description.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_description.Location = new System.Drawing.Point(824, 88);
            this.textBox_description.Multiline = true;
            this.textBox_description.Name = "textBox_description";
            this.textBox_description.Size = new System.Drawing.Size(152, 184);
            this.textBox_description.TabIndex = 3;
            // 
            // checkBox_show_common
            // 
            this.checkBox_show_common.Location = new System.Drawing.Point(256, 64);
            this.checkBox_show_common.Name = "checkBox_show_common";
            this.checkBox_show_common.Size = new System.Drawing.Size(176, 24);
            this.checkBox_show_common.TabIndex = 4;
            this.checkBox_show_common.Text = "Показывать общие работы";
            this.checkBox_show_common.CheckedChanged += new System.EventHandler(this.checkBox_show_common_CheckedChanged);
            // 
            // button_update_list
            // 
            this.button_update_list.Image = ((System.Drawing.Image)(resources.GetObject("button_update_list.Image")));
            this.button_update_list.Location = new System.Drawing.Point(480, 64);
            this.button_update_list.Name = "button_update_list";
            this.button_update_list.Size = new System.Drawing.Size(24, 23);
            this.button_update_list.TabIndex = 5;
            this.button_update_list.Click += new System.EventHandler(this.button_update_list_Click);
            // 
            // contextMenu1
            // 
            this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.menuItem4});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem2,
            this.menuItem3});
            this.menuItem1.Text = "Наборы";
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 0;
            this.menuItem2.Text = "Добавить";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 1;
            this.menuItem3.Text = "Удалить";
            this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 1;
            this.menuItem4.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem5});
            this.menuItem4.Text = "НВ";
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 0;
            this.menuItem5.Text = "Установить фактическое НВ";
            this.menuItem5.Click += new System.EventHandler(this.menuItem5_Click);
            // 
            // listView_details
            // 
            this.listView_details.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listView_details.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11,
            this.columnHeader12,
            this.columnHeader13,
            this.columnHeader14});
            this.listView_details.FullRowSelect = true;
            this.listView_details.HideSelection = false;
            this.listView_details.Location = new System.Drawing.Point(8, 368);
            this.listView_details.Name = "listView_details";
            this.listView_details.Size = new System.Drawing.Size(808, 128);
            this.listView_details.TabIndex = 6;
            this.listView_details.UseCompatibleStateImageBehavior = false;
            this.listView_details.View = System.Windows.Forms.View.Details;
            this.listView_details.DoubleClick += new System.EventHandler(this.listView_details_DoubleClick);
            this.listView_details.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listView_details_MouseUp);
            this.listView_details.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView1_ColumnClick);
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Код";
            this.columnHeader9.Width = 90;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Наименование";
            this.columnHeader10.Width = 200;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "Количество";
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "Ед.Изм.";
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "Цена";
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "Применимость";
            this.columnHeader14.Width = 120;
            // 
            // checkBox_null_quontity
            // 
            this.checkBox_null_quontity.Location = new System.Drawing.Point(8, 344);
            this.checkBox_null_quontity.Name = "checkBox_null_quontity";
            this.checkBox_null_quontity.Size = new System.Drawing.Size(312, 24);
            this.checkBox_null_quontity.TabIndex = 7;
            this.checkBox_null_quontity.Text = "Не показывать детали отсутствующие на складе";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton_detail);
            this.groupBox1.Controls.Add(this.radioButton_work);
            this.groupBox1.Location = new System.Drawing.Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(240, 48);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Вид выбора";
            // 
            // radioButton_detail
            // 
            this.radioButton_detail.Location = new System.Drawing.Point(136, 16);
            this.radioButton_detail.Name = "radioButton_detail";
            this.radioButton_detail.Size = new System.Drawing.Size(88, 24);
            this.radioButton_detail.TabIndex = 1;
            this.radioButton_detail.Text = "По деталям";
            // 
            // radioButton_work
            // 
            this.radioButton_work.Location = new System.Drawing.Point(16, 16);
            this.radioButton_work.Name = "radioButton_work";
            this.radioButton_work.Size = new System.Drawing.Size(88, 24);
            this.radioButton_work.TabIndex = 0;
            this.radioButton_work.Text = "По работам";
            // 
            // contextMenu2
            // 
            this.contextMenu2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem6});
            // 
            // menuItem6
            // 
            this.menuItem6.Index = 0;
            this.menuItem6.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem7,
            this.menuItem8});
            this.menuItem6.Text = "Склад";
            // 
            // menuItem7
            // 
            this.menuItem7.Index = 0;
            this.menuItem7.Text = "Пересчет остатков";
            this.menuItem7.Click += new System.EventHandler(this.menuItem7_Click);
            // 
            // menuItem8
            // 
            this.menuItem8.Index = 1;
            this.menuItem8.Text = "Просмотр движения";
            this.menuItem8.Click += new System.EventHandler(this.menuItem8_Click);
            // 
            // FormWorkManagment
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(984, 509);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.checkBox_null_quontity);
            this.Controls.Add(this.listView_details);
            this.Controls.Add(this.button_update_list);
            this.Controls.Add(this.checkBox_show_common);
            this.Controls.Add(this.textBox_description);
            this.Controls.Add(this.textBox_work_group);
            this.Controls.Add(this.listView_work);
            this.Controls.Add(this.button_work_collection);
            this.Name = "FormWorkManagment";
            this.Text = "Управление работами";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void button_work_collection_Click(object sender, System.EventArgs e)
		{
			FormWorkCollectionManagment dialog = new FormWorkCollectionManagment();
			dialog.Show();
		}

		private void textBox_work_group_DoubleClick(object sender, System.EventArgs e)
		{
			// Инициируем выбор группы трудоемкостей
			ListView list = new ListView();
			DbAutoType.FillList(list);
			FormSelectionList dialog = new FormSelectionList(list);
			if(dialog.ShowDialog() != DialogResult.OK) return;
			DbAutoType auto_type = (DbAutoType)dialog.SelectedCodeObject;
			if(auto_type == null) return;
			long code_work_group = auto_type.Code;
			if(code_work_group == 0) return;
			
			// Отмечаем какая работа выбрана
			selected_code_work_group = code_work_group;
			textBox_work_group.Text = dialog.SelectedText;

			// Заполняем список работами
			bool show_common = checkBox_show_common.Checked;
			listView_work.Items.Clear();
			DbSqlWork.SelectInList(listView_work, selected_code_work_group, show_common, 0, "");
		}

		private void listView_work_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			// Проверяем выбранный элемент
			textBox_description.Text = "";
			if(listView_work.SelectedItems == null) return;
			if(listView_work.SelectedItems.Count == 0) return;
			ListViewItem item = listView_work.SelectedItems[0];
			long code = (long)item.Tag;
			if(code == 0) return;
			// Загружаем работу
			DtWork work = DbSqlWork.Find(code);
			if(work == null) return;

			// Выставляем подсказку
			textBox_description.Text = (string)work.GetData("ОПИСАНИЕ");
		}

		private void checkBox_show_common_CheckedChanged(object sender, System.EventArgs e)
		{
			// Перезачитываем в зависимости от выбора
		}

		private void button_update_list_Click(object sender, System.EventArgs e)
		{
			// Обновить список в соответствии с условиями
			// Заполняем список работами
			bool show_common = checkBox_show_common.Checked;
			listView_work.Items.Clear();
			DbSqlWork.SelectInList(listView_work, selected_code_work_group, show_common, 0, "");
		}

		private void listView_work_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
		{
			FormSelectString dialog;
			string pattern = "";
			bool show_common = checkBox_show_common.Checked;
			// Поиски по определенным параметрам
			switch(e.Column)
			{
				case 0:
					// Поиск по номеру позиции
					dialog = new FormSelectString("Номер позиции", "");
					if(dialog.ShowDialog() != DialogResult.OK) return;
					pattern = dialog.SelectedTextMask;
					listView_work.Items.Clear();
					DbSqlWork.SelectInList(listView_work, selected_code_work_group, show_common, 1, pattern);
					break;
				case 1:
					// Поиск по коду дутали
					dialog = new FormSelectString("Код детали", "");
					if(dialog.ShowDialog() != DialogResult.OK) return;
					pattern = dialog.SelectedTextMask;
					listView_work.Items.Clear();
					DbSqlWork.SelectInList(listView_work, selected_code_work_group, show_common, 2, pattern);
					break;
				case 3:
					// Поиск по наименованию
					dialog = new FormSelectString("Наименование", "");
					if(dialog.ShowDialog() != DialogResult.OK) return;
					pattern = dialog.SelectedTextMask;
					listView_work.Items.Clear();
					DbSqlWork.SelectInList(listView_work, selected_code_work_group, show_common, 3, pattern);
					break;
				default:
					break;
			}
		}

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			// Получаем выбранную работу
			ListViewItem item;
			long code;
			if(listView_work.SelectedItems == null) return;
			if(listView_work.SelectedItems.Count == 0) return;
			item = listView_work.SelectedItems[0];
			if(item == null) return;
			code = (long)item.Tag;
			if(code == 0) return;

			// Установить набор для выбранной работы
			FormWorkCollectionManagment dialog = new FormWorkCollectionManagment();
			if(dialog.ShowDialog() != DialogResult.OK) return;
			long code_collection = dialog.SelectedCodeCollecion;
			if(code_collection == 0)return;

			// Проводим изменение в базе данных
			if(DbSqlWork.SetCollection(code, code_collection) == false) return;
			DtWork work = DbSqlWork.Find(code);
			work.SetLVItem(item);
		}

		private void listView_work_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if(e.Button == MouseButtons.Right)
			{
				// На отпускание правой кнопки мышки - всплывающее меню
				// Настройка меню
				// Права пользователя
				// Отключаем все запрещенное
				//contextMenu1.MenuItems[0].Enabled = false;
				//contextMenu1.MenuItems[1].Enabled = true;
				//menuItem21.Enabled = false;
				// Включаем по разрешению
				string login = Form1.currentLogin.ToLower();
				if (login == "заякинм" || login == "админ" || login == "панкратьева")
				{
					//contextMenu1.MenuItems[0].Enabled = true;
				}
				if (login == "админ")
				{
					//menuItem21.Enabled = true;
					//menuItem22.Enabled = true;
					//menuItem23.Enabled = true;
					//menuItem24.Enabled = true;
					//menuItem31.Enabled = true;
				}
				if (login == "заякинм")
				{
					//menuItem31.Enabled = true;
					// На время отпуска
					//menuItem21.Enabled = true;
					//menuItem22.Enabled = true;
					//menuItem23.Enabled = true;
				}
				// Настройка меню исходя из свойств выбранной карточки
				// Показ меню
				contextMenu1.Show(listView_work, new Point(e.X, e.Y));
			}
		}

		private void menuItem3_Click(object sender, System.EventArgs e)
		{
			// Удалить набор
			// Получаем выбранную работу
			ListViewItem item;
			long code;
			if(listView_work.SelectedItems == null) return;
			if(listView_work.SelectedItems.Count == 0) return;
			item = listView_work.SelectedItems[0];
			if(item == null) return;
			code = (long)item.Tag;
			if(code == 0) return;

			// Проводим изменение в базе данных
			if(DbSqlWork.ClearCollection(code) == false) return;
			DtWork work = DbSqlWork.Find(code);
			work.SetLVItem(item);
		}

		private void listView1_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
		{
			string mask = "";
			FormSelectString dialog = null;
			// Поиски по колонкам
			switch(e.Column)
			{
				case 0:
					dialog = new FormSelectString("Код детали", "Код детали для поиска");
					if(dialog.ShowDialog(this) != DialogResult.OK) return;
					mask = dialog.SelectedTextMask;
					listView_details.Items.Clear();
					if(checkBox_null_quontity.Checked)
						DbSqlStorageDetail.SelectInListNumber(listView_details, mask, false);
					else
						DbSqlStorageDetail.SelectInListNumber(listView_details, mask, true);
					break;
				case 1:
					dialog = new FormSelectString("Наименование детали", "Наименование детали для поиска");
					if(dialog.ShowDialog(this) != DialogResult.OK) return;
					mask = dialog.SelectedTextMask;
					listView_details.Items.Clear();
					if(checkBox_null_quontity.Checked)
						DbSqlStorageDetail.SelectInListName(listView_details, mask, false);
					else
						DbSqlStorageDetail.SelectInListName(listView_details, mask, true);
					break;
				default:
					break;
			}
		}

		private void listView_work_DoubleClick(object sender, System.EventArgs e)
		{
			// Поиск выбранной работы
			ListViewItem item;
			long code;
			if(listView_work.SelectedItems == null) return;
			if(listView_work.SelectedItems.Count == 0) return;
			item = listView_work.SelectedItems[0];
			if(item == null) return;
			code = (long)item.Tag;
			if(code == 0) return;

			// Определяем доп. параметры поиска
			string number	= item.SubItems[1].Text;
			if(number.Length > 0)
				number = "%" + number + "%";
			else
				number = "";
			// Хитрый поиск по наименованию
			string name		= item.SubItems[3].Text;
			int pos = name.IndexOf(" ") - 1;
			int pos1 = name.IndexOf("-") - 1;
			if(pos1 > 0 && pos1 < pos) pos = pos1;
			if(pos > 0)
			{
				name = name.Substring(0, pos);
				if(name.Length > 0)
					name = "%" + name + "%";
				else
					name = "";
			}
			else
				name = "";

			// Осуществляем поиск по номеру детали
			if(radioButton_work.Checked	== true)
			{
				listView_details.Items.Clear();
				if(checkBox_null_quontity.Checked)
				{
					if(number.Length > 0)
						DbSqlStorageDetail.SelectInListNameNumber(listView_details, number, false);
					if(name.Length > 0)
						DbSqlStorageDetail.SelectInListName(listView_details, name, false);
				}
				else
				{
					if(number.Length > 0)
						DbSqlStorageDetail.SelectInListNameNumber(listView_details, number, true);
					if(name.Length > 0)
						DbSqlStorageDetail.SelectInListName(listView_details, name, true);
				}
			}

			// Отправляем в связанный заказ-наряд выбранную работу
			// Определяем, открыта ли еще карточка заказ-наряда
			if(form_card == null) return;	// Формы никогда не было
			if(form_card.IsDisposed == true)
			{
				// Форма была но закрыта, закрываемся
				this.Close();
				return;
			}
			ListView list = form_card.GetWorkList();
			DbWork work = DbWork.Find(code);
			DbCard card = form_card.Card;
			if(card == null || work == null) return;
			DbCardWork card_work = new DbCardWork(work, card);
			ListViewItem card_work_lvitem = list.Items.Add(card_work.LVItem);
			card_work.SetLVItem(card_work_lvitem);
			form_card.UpdateCardData();
		}

		private void listView_details_DoubleClick(object sender, System.EventArgs e)
		{
			// Осуществили выбор детали
			// Поиск выбранной детали
			ListViewItem item;
			long code;
			if(listView_details.SelectedItems == null) return;
			if(listView_details.SelectedItems.Count == 0) return;
			item = listView_details.SelectedItems[0];
			if(item == null) return;
			code = (long)item.Tag;
			if(code == 0) return;

			// Определяем доп. параметры поиска
			// Хитрый поиск по номеру детали
			string number	= item.Text;
			if(number.Length > 0)
			{
				int pos5 = -1;
				int pos6 = -1;
				pos5 = number.IndexOf("-");
				if(pos5 != -1) pos6 = number.IndexOf("-", pos5 + 1);
				if(pos6 != -1)
					number = number.Substring(pos5 + 1, pos6 - pos5);
				else
					if(pos5 != - 1)
						number = number.Substring(pos5 + 1);
				if(number.Length > 0)
					number = "%" + number + "%";
				else
					number = "";
			}
			else
				number = "";
			// Хитрый поиск по наименованию
			string name		= item.SubItems[1].Text;
			int pos = name.IndexOf(" ");
			if(pos > 1)
			{
				name = name.Substring(0, pos - 1);
				if(name.Length > 0)
					name = "%" + name + "%";
				else
					name = "";
			}
			else
				name = "";

			// Осуществляем поиск работ по номеру детали
			if(radioButton_detail.Checked	== true)
			{
				bool show_common = checkBox_show_common.Checked;
				listView_work.Items.Clear();
				// Поиск по коду детали
				if(number.Length > 0)
					DbSqlWork.SelectInList(listView_work, selected_code_work_group, show_common, 2, number);
				// Поиск по коду названию
				if(name.Length > 0)
					DbSqlWork.SelectInList(listView_work, selected_code_work_group, show_common, 3, name);
			}

			// Если есть связь отправляем деталь в заказ-наряд
			// Определяем, открыта ли еще карточка заказ-наряда
			if(form_card == null) return;	// Формы никогда не было
			if(form_card.IsDisposed == true)
			{
				// Форма была но закрыта, закрываемся
				this.Close();
				return;
			}
			ListView list = form_card.GetDetailList();
			DbDetailStorage detail = DbDetailStorage.Find(code);
			DbCard card = form_card.Card;
			if(card == null || detail == null) return;


			DbCardDetail card_detail = new DbCardDetail(card, detail);
			ListViewItem card_detail_lvitem = list.Items.Add(card_detail.LVItem);
			card_detail.SetLVItem(card_detail_lvitem);
			form_card.UpdateCardData();
		}

		private void menuItem5_Click(object sender, System.EventArgs e)
		{
			// Устанавливаем фактические НВ, для сервис пакета
			// Получаем выбранную работу
			ListViewItem item;
			long code;
			if(listView_work.SelectedItems == null) return;
			if(listView_work.SelectedItems.Count == 0) return;
			item = listView_work.SelectedItems[0];
			if(item == null) return;
			code = (long)item.Tag;
			if(code == 0) return;

			// Установить набор для выбранной работы
			FormSelectString dialog = new FormSelectString("Норма времени для сервис пакета", "0.0");
			if(dialog.ShowDialog() != DialogResult.OK) return;
			float nv = dialog.SelectedFloat;

			// Проводим изменение в базе данных
			if(DbSqlWork.SetNv(code, nv) == false) return;
			DtWork work = DbSqlWork.Find(code);
			work.SetLVItem(item);
		}

		private void listView_details_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if(e.Button == MouseButtons.Right)
			{
				// На отпускание правой кнопки мышки - всплывающее меню
				// Настройка меню
				// Права пользователя
				// Отключаем все запрещенное
				//contextMenu1.MenuItems[0].Enabled = false;
				//contextMenu1.MenuItems[1].Enabled = true;
				//menuItem21.Enabled = false;
				// Включаем по разрешению
				string login = Form1.currentLogin.ToLower();
				if (login == "заякинм" || login == "админ" || login == "панкратьева")
				{
					//contextMenu1.MenuItems[0].Enabled = true;
				}
				if (login == "админ")
				{
					//menuItem21.Enabled = true;
					//menuItem22.Enabled = true;
					//menuItem23.Enabled = true;
					//menuItem24.Enabled = true;
					//menuItem31.Enabled = true;
				}
				if (login == "заякинм")
				{
					//menuItem31.Enabled = true;
					// На время отпуска
					//menuItem21.Enabled = true;
					//menuItem22.Enabled = true;
					//menuItem23.Enabled = true;
				}
				// Настройка меню исходя из свойств выбранной карточки
				// Показ меню
				contextMenu2.Show(listView_details, new Point(e.X, e.Y));
			}
		}

		private void menuItem7_Click(object sender, System.EventArgs e)
		{
			// Пересчет остатков по складу по позиции
			// Получаем выбранную работу
			ListViewItem item;
			long code;
			item	= Db.GetItemSelected(listView_details);
			if(item == null) return;
			code = (long)item.Tag;
			if(code == 0) return;

			// Установить набор для выбранной работы
			DbSqlStorageDetail.CalculateStorage(code);
			DtStorageDetail detail = DbSqlStorageDetail.Find(code);
			if(detail == null) return;
			// Проводим изменение в базе данных
			detail.SetLVItem(item);
		}

		private void menuItem8_Click(object sender, System.EventArgs e)
		{
			// Просмотр движения по складу позиции
			// Получаем выбранную работу
			ListViewItem item;
			long code;
			item	= Db.GetItemSelected(listView_details);
			if(item == null) return;
			code = (long)item.Tag;
			if(code == 0) return;

			DtStorageDetail detail = DbSqlStorageDetail.Find(code);
			if(detail == null) return;
			
			FormStorageDetailMove dialog = new FormStorageDetailMove(detail, 1);
			dialog.Show();
		}

        private void listView_work_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormBarCodeGet form = null;
            string bc = "";
            long l = 0;
            string first = "";
            string other = "";
            
            switch(e.KeyChar)
            {
                case 'Z':
                // Если нажата клавиша Z то ждем штрих код!
                form = new FormBarCodeGet();
                if(form.ShowDialog() != DialogResult.OK) return;
                // Обрабатываем штрих код - работа или деталь!
                bc = form.BarCode;
                if (bc.Length == 0) return;
                first = bc.Substring(0, 1);
                other = bc.Substring(1);
                l = 0;
                try
                {
                    l = Convert.ToInt64(other);
                }
                catch (Exception e1)
                {
                    return;
                }
                if (l == 0) return;
                switch (first)
                {
                    case "D":
                        // Добавляем в список деталь
                        if (form_card == null) return;	// Формы никогда не было
                        if (form_card.IsDisposed == true)
                        {
                            // Форма была но закрыта, закрываемся
                            this.Close();
                            return;
                        }
                        ListView list = null;
                        DbCard card = null;
                        list = form_card.GetDetailList();
                        DbDetailStorage detail = DbDetailStorage.Find(l);
                        card = form_card.Card;
                        if (card == null || detail == null) return;
                        DbCardDetail card_detail = new DbCardDetail(card, detail);
                        ListViewItem card_detail_lvitem = list.Items.Add(card_detail.LVItem);
                        card_detail.SetLVItem(card_detail_lvitem);
                        form_card.UpdateCardData();
                        break;
                    case "W":
                        // Добавляем в список работу
                        // Отправляем в связанный заказ-наряд выбранную работу
                        // Определяем, открыта ли еще карточка заказ-наряда
                        if (form_card == null) return;	// Формы никогда не было
                        if (form_card.IsDisposed == true)
                        {
                            // Форма была но закрыта, закрываемся
                            this.Close();
                            return;
                        }
                        list = form_card.GetWorkList();
                        DbWork work = DbWork.Find(l);
                        card = form_card.Card;
                        if (card == null || work == null) return;
                        DbCardWork card_work = new DbCardWork(work, card);
                        ListViewItem card_work_lvitem = list.Items.Add(card_work.LVItem);
                        card_work.SetLVItem(card_work_lvitem);
                        form_card.UpdateCardData();
                        break;
                     default:
                        return;
                }
                break;
                case 'Я':
                // Если нажата клавиша Z то ждем штрих код!
                form = new FormBarCodeGet();
                if (form.ShowDialog() != DialogResult.OK) return;
                // Обрабатываем штрих код - работа или деталь!
                bc = form.BarCode;
                if (bc.Length == 0) return;
                first = bc.Substring(0, 1);
                other = bc.Substring(1);
                l = 0;
                try
                {
                    l = Convert.ToInt64(other);
                }
                catch (Exception e1)
                {
                    return;
                }
                if (l == 0) return;
               
                switch (first)
                {
                    case "D":
                        // Добавляем в список деталь
                        if (form_card == null) return;	// Формы никогда не было
                        if (form_card.IsDisposed == true)
                        {
                            // Форма была но закрыта, закрываемся
                            this.Close();
                            return;
                        }
                        ListView list = null;
                        DbCard card = null;
                        list = form_card.GetDetailList();
                        DbDetailStorage detail = DbDetailStorage.Find(l);
                        card = form_card.Card;
                        if (card == null || detail == null) return;
                        DbCardDetail card_detail = new DbCardDetail(card, detail);
                        ListViewItem card_detail_lvitem = list.Items.Add(card_detail.LVItem);
                        card_detail.SetLVItem(card_detail_lvitem);
                        form_card.UpdateCardData();
                        break;
                    case "W":
                        // Добавляем в список работу
                        // Отправляем в связанный заказ-наряд выбранную работу
                        // Определяем, открыта ли еще карточка заказ-наряда
                        if (form_card == null) return;	// Формы никогда не было
                        if (form_card.IsDisposed == true)
                        {
                            // Форма была но закрыта, закрываемся
                            this.Close();
                            return;
                        }
                        list = form_card.GetWorkList();
                        DbWork work = DbWork.Find(l);
                        card = form_card.Card;
                        if (card == null || work == null) return;
                        DbCardWork card_work = new DbCardWork(work, card);
                        ListViewItem card_work_lvitem = list.Items.Add(card_work.LVItem);
                        card_work.SetLVItem(card_work_lvitem);
                        form_card.UpdateCardData();
                        break;
                    default:
                        return;
                }

                break;
                default:
                break;
            }
        }
	}
}
