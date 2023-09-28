using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormWorkCollectionManagment.
	/// </summary>
	public class FormWorkCollectionManagment : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView listView_work_collections;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.Button button_new_work_collection;
		private System.Windows.Forms.Button button_update_work_collection;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ListView listView_collection_items;
		private System.Windows.Forms.Button button_new_collection_item;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Button button_update_item;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.Button button_delete_item;
		private System.Windows.Forms.Button button_delete_collection;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.Button button_set_time;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.Button button_set_number;
		private System.Windows.Forms.Button button_set_number_group;

		long selected_code_collection = 0;

		public FormWorkCollectionManagment()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			// Показываем список существующих наборов
			DbSqlWorkCollection.SelectInList(listView_work_collections);
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormWorkCollectionManagment));
			this.listView_work_collections = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.button_new_work_collection = new System.Windows.Forms.Button();
			this.button_update_work_collection = new System.Windows.Forms.Button();
			this.listView_collection_items = new System.Windows.Forms.ListView();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.button_new_collection_item = new System.Windows.Forms.Button();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.button_set_time = new System.Windows.Forms.Button();
			this.button_set_number = new System.Windows.Forms.Button();
			this.button_set_number_group = new System.Windows.Forms.Button();
			this.button_update_item = new System.Windows.Forms.Button();
			this.button_delete_item = new System.Windows.Forms.Button();
			this.button_delete_collection = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// listView_work_collections
			// 
			this.listView_work_collections.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.listView_work_collections.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																										this.columnHeader1});
			this.listView_work_collections.HideSelection = false;
			this.listView_work_collections.Location = new System.Drawing.Point(8, 8);
			this.listView_work_collections.Name = "listView_work_collections";
			this.listView_work_collections.Size = new System.Drawing.Size(320, 136);
			this.listView_work_collections.TabIndex = 0;
			this.listView_work_collections.View = System.Windows.Forms.View.Details;
			this.listView_work_collections.DoubleClick += new System.EventHandler(this.listView_work_collections_DoubleClick);
			this.listView_work_collections.SelectedIndexChanged += new System.EventHandler(this.listView_work_collections_SelectedIndexChanged);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Наименование";
			this.columnHeader1.Width = 260;
			// 
			// button_new_work_collection
			// 
			this.button_new_work_collection.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.button_new_work_collection.Image = ((System.Drawing.Bitmap)(resources.GetObject("button_new_work_collection.Image")));
			this.button_new_work_collection.Location = new System.Drawing.Point(328, 8);
			this.button_new_work_collection.Name = "button_new_work_collection";
			this.button_new_work_collection.Size = new System.Drawing.Size(24, 23);
			this.button_new_work_collection.TabIndex = 1;
			this.toolTip1.SetToolTip(this.button_new_work_collection, "Новый набор");
			this.button_new_work_collection.Click += new System.EventHandler(this.button_new_work_collection_Click);
			// 
			// button_update_work_collection
			// 
			this.button_update_work_collection.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.button_update_work_collection.Image = ((System.Drawing.Bitmap)(resources.GetObject("button_update_work_collection.Image")));
			this.button_update_work_collection.Location = new System.Drawing.Point(328, 32);
			this.button_update_work_collection.Name = "button_update_work_collection";
			this.button_update_work_collection.Size = new System.Drawing.Size(24, 23);
			this.button_update_work_collection.TabIndex = 2;
			this.toolTip1.SetToolTip(this.button_update_work_collection, "Изменение набора");
			this.button_update_work_collection.Click += new System.EventHandler(this.button_update_work_collection_Click);
			// 
			// listView_collection_items
			// 
			this.listView_collection_items.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.listView_collection_items.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																										this.columnHeader4,
																										this.columnHeader5,
																										this.columnHeader2,
																										this.columnHeader3});
			this.listView_collection_items.FullRowSelect = true;
			this.listView_collection_items.Location = new System.Drawing.Point(8, 152);
			this.listView_collection_items.Name = "listView_collection_items";
			this.listView_collection_items.Size = new System.Drawing.Size(592, 208);
			this.listView_collection_items.TabIndex = 3;
			this.listView_collection_items.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "№";
			this.columnHeader4.Width = 30;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "Гр.";
			this.columnHeader5.Width = 30;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Описание";
			this.columnHeader2.Width = 400;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "НВ";
			// 
			// button_new_collection_item
			// 
			this.button_new_collection_item.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.button_new_collection_item.Image = ((System.Drawing.Bitmap)(resources.GetObject("button_new_collection_item.Image")));
			this.button_new_collection_item.Location = new System.Drawing.Point(600, 152);
			this.button_new_collection_item.Name = "button_new_collection_item";
			this.button_new_collection_item.Size = new System.Drawing.Size(24, 23);
			this.button_new_collection_item.TabIndex = 4;
			this.toolTip1.SetToolTip(this.button_new_collection_item, "Новый элемент набора");
			this.button_new_collection_item.Click += new System.EventHandler(this.button_new_collection_item_Click);
			// 
			// button_set_time
			// 
			this.button_set_time.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.button_set_time.Location = new System.Drawing.Point(600, 224);
			this.button_set_time.Name = "button_set_time";
			this.button_set_time.Size = new System.Drawing.Size(32, 23);
			this.button_set_time.TabIndex = 8;
			this.button_set_time.Text = "НВ";
			this.toolTip1.SetToolTip(this.button_set_time, "Установить количество НВ");
			this.button_set_time.Click += new System.EventHandler(this.button_set_time_Click);
			// 
			// button_set_number
			// 
			this.button_set_number.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.button_set_number.Location = new System.Drawing.Point(600, 248);
			this.button_set_number.Name = "button_set_number";
			this.button_set_number.Size = new System.Drawing.Size(32, 23);
			this.button_set_number.TabIndex = 9;
			this.button_set_number.Text = "№";
			this.toolTip1.SetToolTip(this.button_set_number, "Установить номер группы");
			this.button_set_number.Click += new System.EventHandler(this.button_set_number_Click);
			// 
			// button_set_number_group
			// 
			this.button_set_number_group.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.button_set_number_group.Location = new System.Drawing.Point(600, 272);
			this.button_set_number_group.Name = "button_set_number_group";
			this.button_set_number_group.Size = new System.Drawing.Size(32, 23);
			this.button_set_number_group.TabIndex = 10;
			this.button_set_number_group.Text = "Гр.";
			this.toolTip1.SetToolTip(this.button_set_number_group, "Установить принадлежность группе с номером");
			this.button_set_number_group.Click += new System.EventHandler(this.button_set_number_group_Click);
			// 
			// button_update_item
			// 
			this.button_update_item.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.button_update_item.Image = ((System.Drawing.Bitmap)(resources.GetObject("button_update_item.Image")));
			this.button_update_item.Location = new System.Drawing.Point(600, 176);
			this.button_update_item.Name = "button_update_item";
			this.button_update_item.Size = new System.Drawing.Size(24, 23);
			this.button_update_item.TabIndex = 5;
			this.button_update_item.Click += new System.EventHandler(this.button_update_item_Click);
			// 
			// button_delete_item
			// 
			this.button_delete_item.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.button_delete_item.Image = ((System.Drawing.Bitmap)(resources.GetObject("button_delete_item.Image")));
			this.button_delete_item.Location = new System.Drawing.Point(600, 200);
			this.button_delete_item.Name = "button_delete_item";
			this.button_delete_item.Size = new System.Drawing.Size(24, 23);
			this.button_delete_item.TabIndex = 6;
			this.button_delete_item.Click += new System.EventHandler(this.button_delete_item_Click);
			// 
			// button_delete_collection
			// 
			this.button_delete_collection.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.button_delete_collection.Image = ((System.Drawing.Bitmap)(resources.GetObject("button_delete_collection.Image")));
			this.button_delete_collection.Location = new System.Drawing.Point(328, 56);
			this.button_delete_collection.Name = "button_delete_collection";
			this.button_delete_collection.Size = new System.Drawing.Size(24, 23);
			this.button_delete_collection.TabIndex = 7;
			this.button_delete_collection.Click += new System.EventHandler(this.button_delete_collection_Click);
			// 
			// FormWorkCollectionManagment
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(632, 365);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.button_set_number_group,
																		  this.button_set_number,
																		  this.button_set_time,
																		  this.button_delete_collection,
																		  this.button_delete_item,
																		  this.button_update_item,
																		  this.button_new_collection_item,
																		  this.listView_collection_items,
																		  this.button_update_work_collection,
																		  this.button_new_work_collection,
																		  this.listView_work_collections});
			this.Name = "FormWorkCollectionManagment";
			this.Text = "Управление наборами";
			this.ResumeLayout(false);

		}
		#endregion

		private void button_new_work_collection_Click(object sender, System.EventArgs e)
		{
			// Создание нового набора
			FormSelectString dialog = new FormSelectString("Наименование нового набора", "");
			if(dialog.ShowDialog() != DialogResult.OK) return;
			// Попытка добавления нового набора в базу
			string name = dialog.SelectedText;
			name = name.Trim();
			if(name.Length == 0) return;
			DtWorkCollection collection = new DtWorkCollection(name);
			collection = DbSqlWorkCollection.Insert(collection);
			if(collection == null) return;
			// Добавляем созданный набор в список
			ListViewItem itm = listView_work_collections.Items.Add("");
			collection.SetLVItem(itm);
		}

		private void button_update_work_collection_Click(object sender, System.EventArgs e)
		{
			// Поиск выбранного элемента
			if(listView_work_collections.SelectedItems == null) return;
			if(listView_work_collections.SelectedItems.Count == 0) return;
			ListViewItem itm = listView_work_collections.SelectedItems[0];
			if(itm == null) return;
			long code = (long)itm.Tag;
			if(code == 0) return;
			// Изменение выбранного набора
			FormSelectString dialog = new FormSelectString("Наименование нового набора", itm.Text);
			if(dialog.ShowDialog() != DialogResult.OK) return;
			// Попытка изменения набора в базе
			string name = dialog.SelectedText;
			name = name.Trim();
			if(name.Length == 0) return;
			DtWorkCollection collection = new DtWorkCollection(name);
			collection.SetData("КОД_КОЛЛЕКЦИЯ", (long)code);
			if(DbSqlWorkCollection.Update(collection) == false) return;
			// Изменяем набор в списке
			collection.SetLVItem(itm);
		}

		private void button_new_collection_item_Click(object sender, System.EventArgs e)
		{
			// Добавление нового элемента в набор
			// Поиск выбранного набора
			if(listView_work_collections.SelectedItems == null) return;
			if(listView_work_collections.SelectedItems.Count == 0) return;
			ListViewItem itm = listView_work_collections.SelectedItems[0];
			if(itm == null) return;
			long code_collection = (long)itm.Tag;
			if(code_collection == 0) return;
			// Запрос имени элемента набора
			FormSelectString dialog = new FormSelectString("Наименование позиции", "");
			if(dialog.ShowDialog() != DialogResult.OK) return;
			// Попытка добавления нового набора в базу
			string name = dialog.SelectedText;
			name = name.Trim();
			if(name.Length == 0) return;
			DtWorkCollectionItem collection_item = new DtWorkCollectionItem(code_collection, name);
			collection_item = DbSqlWorkCollectionItem.Insert(collection_item);
			if(collection_item == null) return;
			// Добавляем созданный набор в список
			ListViewItem itm_1 = listView_collection_items.Items.Add("");
			collection_item.SetLVItem(itm_1);
		}

		private void listView_work_collections_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			// При изменении активного элемента - показать соответсвующий набор

			// Очищаем старый список
			listView_collection_items.Items.Clear();
			// Поиск выбранного набора
			if(listView_work_collections.SelectedItems == null) return;
			if(listView_work_collections.SelectedItems.Count == 0) return;
			ListViewItem itm = listView_work_collections.SelectedItems[0];
			if(itm == null) return;
			long code_collection = (long)itm.Tag;
			if(code_collection == 0) return;
			// Заполнение списком элементов
			DbSqlWorkCollectionItem.SelectInList(listView_collection_items, code_collection);
		}

		private void button_update_item_Click(object sender, System.EventArgs e)
		{
			// Изменение элемента списка
			// Поиск выбранного набора
			if(listView_work_collections.SelectedItems == null) return;
			if(listView_work_collections.SelectedItems.Count == 0) return;
			ListViewItem itm = listView_work_collections.SelectedItems[0];
			if(itm == null) return;
			long code_collection = (long)itm.Tag;
			if(code_collection == 0) return;

			// Поиск выбранного элемента набора
			if(listView_collection_items.SelectedItems == null) return;
			if(listView_collection_items.SelectedItems.Count == 0) return;
			ListViewItem itm_1 = listView_collection_items.SelectedItems[0];
			if(itm_1 == null) return;
			long code = (long)itm_1.Tag;
			if(code == 0) return;

			// Запрос имени элемента набора
			FormSelectString dialog = new FormSelectString("Наименование позиции", itm_1.SubItems[2].Text);
			if(dialog.ShowDialog() != DialogResult.OK) return;

			// Попытка изменения элемента в базе
			string name = dialog.SelectedText;
			name = name.Trim();
			if(name.Length == 0) return;
			DtWorkCollectionItem collection_item = new DtWorkCollectionItem(code_collection, name);
			collection_item.SetData("КОД_КОЛЛЕКЦИЯ_ЭЛЕМЕНТ", (long)code);
			if(DbSqlWorkCollectionItem.Update(collection_item) == false) return;
			if(collection_item == null) return;

			// Изменяем набор в списке
			collection_item.SetLVItem(itm_1);
		}

		private void listView_work_collections_DoubleClick(object sender, System.EventArgs e)
		{
			// Осуществляем закрытие окна с выбором соответствующего набора
			if(listView_work_collections.SelectedItems == null) return;
			if(listView_work_collections.SelectedItems.Count == 0) return;
			ListViewItem itm = listView_work_collections.SelectedItems[0];
			if(itm == null) return;
			long code_collection = (long)itm.Tag;
			if(code_collection == 0) return;

			selected_code_collection = code_collection;
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void button_delete_item_Click(object sender, System.EventArgs e)
		{
			// Удаляем элемент списка
			// Изменение элемента списка
			// Поиск выбранного набора
			if(listView_work_collections.SelectedItems == null) return;
			if(listView_work_collections.SelectedItems.Count == 0) return;
			ListViewItem itm = listView_work_collections.SelectedItems[0];
			if(itm == null) return;
			long code_collection = (long)itm.Tag;
			if(code_collection == 0) return;

			// Поиск выбранного элемента набора
			if(listView_collection_items.SelectedItems == null) return;
			if(listView_collection_items.SelectedItems.Count == 0) return;
			ListViewItem itm_1 = listView_collection_items.SelectedItems[0];
			if(itm_1 == null) return;
			long code = (long)itm_1.Tag;
			if(code == 0) return;

			// Запрос подтверждения
			if(MessageBox.Show("Удалить элемент набора?", "Запрос", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

			// Попытка изменения элемента в базе
			if(DbSqlWorkCollectionItem.Remove(code_collection, code) == false) return;

			// Изменяем набор в списке
			listView_collection_items.Items.Remove(itm_1);
		}

		private void button_delete_collection_Click(object sender, System.EventArgs e)
		{
			// Пытаемся удалить набор
			// Поиск выбранного элемента
			if(listView_work_collections.SelectedItems == null) return;
			if(listView_work_collections.SelectedItems.Count == 0) return;
			ListViewItem itm = listView_work_collections.SelectedItems[0];
			if(itm == null) return;
			long code = (long)itm.Tag;
			if(code == 0) return;

			// Запрос подтверждения
			if(MessageBox.Show("Удалить элемент набора?", "Запрос", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

			if(DbSqlWorkCollection.Remove(code) == false) return;
			// Изменяем набор в списке
			listView_work_collections.Items.Remove(itm);
		}

		private void button_set_time_Click(object sender, System.EventArgs e)
		{
			// Устанавливаем норму времени для выбранного элемента
			// Удаляем элемент списка
			// Изменение элемента списка
			// Поиск выбранного набора
			if(listView_work_collections.SelectedItems == null) return;
			if(listView_work_collections.SelectedItems.Count == 0) return;
			ListViewItem itm = listView_work_collections.SelectedItems[0];
			if(itm == null) return;
			long code_collection = (long)itm.Tag;
			if(code_collection == 0) return;

			// Поиск выбранного элемента набора
			if(listView_collection_items.SelectedItems == null) return;
			if(listView_collection_items.SelectedItems.Count == 0) return;
			ListViewItem itm_1 = listView_collection_items.SelectedItems[0];
			if(itm_1 == null) return;
			long code = (long)itm_1.Tag;
			if(code == 0) return;

			// Запрос нормы времени элемента набора
			FormSelectString dialog = new FormSelectString("Норма времени", itm_1.SubItems[3].Text);
			if(dialog.ShowDialog() != DialogResult.OK) return;

			// Попытка изменения элемента в базе
			float time = dialog.SelectedFloat;
			if(DbSqlWorkCollectionItem.UpdateTime(time, code_collection, code) == false) return;
			DtWorkCollectionItem collection_item	= new DtWorkCollectionItem();
			collection_item = DbSqlWorkCollectionItem.Find(code_collection, code);
			if(collection_item == null) return;
			// Изменяем набор в списке
			collection_item.SetLVItem(itm_1);
		}

		private void button_set_number_Click(object sender, System.EventArgs e)
		{
			// Устанавливаем норму времени для выбранного элемента
			// Удаляем элемент списка
			// Изменение элемента списка
			// Поиск выбранного набора
			if(listView_work_collections.SelectedItems == null) return;
			if(listView_work_collections.SelectedItems.Count == 0) return;
			ListViewItem itm = listView_work_collections.SelectedItems[0];
			if(itm == null) return;
			long code_collection = (long)itm.Tag;
			if(code_collection == 0) return;

			// Поиск выбранного элемента набора
			if(listView_collection_items.SelectedItems == null) return;
			if(listView_collection_items.SelectedItems.Count == 0) return;
			ListViewItem itm_1 = listView_collection_items.SelectedItems[0];
			if(itm_1 == null) return;
			long code = (long)itm_1.Tag;
			if(code == 0) return;

			// Запрос нормы времени элемента набора
			FormSelectString dialog = new FormSelectString("Номер группы", itm_1.Text);
			if(dialog.ShowDialog() != DialogResult.OK) return;

			// Попытка изменения элемента в базе
			int number = dialog.SelectedInt;
			if(DbSqlWorkCollectionItem.UpdateNumber(number, code_collection, code) == false) return;
			DtWorkCollectionItem collection_item	= new DtWorkCollectionItem();
			collection_item = DbSqlWorkCollectionItem.Find(code_collection, code);
			if(collection_item == null) return;
			// Изменяем набор в списке
			collection_item.SetLVItem(itm_1);
		}

		private void button_set_number_group_Click(object sender, System.EventArgs e)
		{
			// Устанавливаем норму времени для выбранного элемента
			// Удаляем элемент списка
			// Изменение элемента списка
			// Поиск выбранного набора
			if(listView_work_collections.SelectedItems == null) return;
			if(listView_work_collections.SelectedItems.Count == 0) return;
			ListViewItem itm = listView_work_collections.SelectedItems[0];
			if(itm == null) return;
			long code_collection = (long)itm.Tag;
			if(code_collection == 0) return;

			// Поиск выбранного элемента набора
			if(listView_collection_items.SelectedItems == null) return;
			if(listView_collection_items.SelectedItems.Count == 0) return;
			ListViewItem itm_1 = listView_collection_items.SelectedItems[0];
			if(itm_1 == null) return;
			long code = (long)itm_1.Tag;
			if(code == 0) return;

			// Запрос нормы времени элемента набора
			FormSelectString dialog = new FormSelectString("Номер подгруппы", itm_1.SubItems[1].Text);
			if(dialog.ShowDialog() != DialogResult.OK) return;

			// Попытка изменения элемента в базе
			int number_group = dialog.SelectedInt;
			if(DbSqlWorkCollectionItem.UpdateNumberGroup(number_group, code_collection, code) == false) return;
			DtWorkCollectionItem collection_item	= new DtWorkCollectionItem();
			collection_item = DbSqlWorkCollectionItem.Find(code_collection, code);
			if(collection_item == null) return;
			// Изменяем набор в списке
			collection_item.SetLVItem(itm_1);
		}

		public long SelectedCodeCollecion
		{
			get
			{
				return selected_code_collection;
			}
		}
	}
}
