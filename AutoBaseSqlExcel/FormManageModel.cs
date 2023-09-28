using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormManageModel.
	/// </summary>
	public class FormManageModel : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TreeView treeView1;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.Button button_tobrand;
		private System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.Button button_new_brand;
		private System.Windows.Forms.Button button_delete_brand;
		private System.Windows.Forms.Button button_update_brand;
		private System.Windows.Forms.ContextMenu contextMenu2;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.MenuItem menuItem8;
		private System.Windows.Forms.Button button_new_model;
		private System.Windows.Forms.MenuItem menuItem9;
		private System.Windows.Forms.MenuItem menuItem10;
		private System.Windows.Forms.MenuItem menuItem11;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FormManageModel()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			// Заполняем дерево брендами
			DbSqlBrand.SelectInTree(treeView1);
			foreach(TreeNode node in treeView1.Nodes)
			{
				DbSqlModel.SelectInTree(node, (long)node.Tag);
			}
			// Заполняем список нераспределенных по брендам моделей
			DbSqlModel.SelectInList(listView1, 0);
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormManageModel));
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.button_tobrand = new System.Windows.Forms.Button();
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.button_new_brand = new System.Windows.Forms.Button();
			this.button_delete_brand = new System.Windows.Forms.Button();
			this.button_update_brand = new System.Windows.Forms.Button();
			this.contextMenu2 = new System.Windows.Forms.ContextMenu();
			this.menuItem9 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.menuItem7 = new System.Windows.Forms.MenuItem();
			this.menuItem8 = new System.Windows.Forms.MenuItem();
			this.button_new_model = new System.Windows.Forms.Button();
			this.menuItem10 = new System.Windows.Forms.MenuItem();
			this.menuItem11 = new System.Windows.Forms.MenuItem();
			this.SuspendLayout();
			// 
			// treeView1
			// 
			this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left);
			this.treeView1.HideSelection = false;
			this.treeView1.ImageIndex = -1;
			this.treeView1.Location = new System.Drawing.Point(8, 32);
			this.treeView1.Name = "treeView1";
			this.treeView1.SelectedImageIndex = -1;
			this.treeView1.Size = new System.Drawing.Size(304, 296);
			this.treeView1.TabIndex = 0;
			this.treeView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseUp);
			// 
			// listView1
			// 
			this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.columnHeader1});
			this.listView1.HideSelection = false;
			this.listView1.Location = new System.Drawing.Point(352, 8);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(264, 320);
			this.listView1.TabIndex = 1;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseUp);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Модель";
			this.columnHeader1.Width = 200;
			// 
			// button_tobrand
			// 
			this.button_tobrand.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.button_tobrand.Location = new System.Drawing.Point(320, 40);
			this.button_tobrand.Name = "button_tobrand";
			this.button_tobrand.Size = new System.Drawing.Size(24, 23);
			this.button_tobrand.TabIndex = 2;
			this.button_tobrand.Text = "<";
			this.button_tobrand.Click += new System.EventHandler(this.button_tobrand_Click);
			// 
			// contextMenu1
			// 
			this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.menuItem1});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem2,
																					  this.menuItem3,
																					  this.menuItem5,
																					  this.menuItem6,
																					  this.menuItem10,
																					  this.menuItem11});
			this.menuItem1.Text = "Модель";
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 0;
			this.menuItem2.Text = "Свойства";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 1;
			this.menuItem3.Text = "Удалить из Бренда";
			this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
			// 
			// menuItem5
			// 
			this.menuItem5.Index = 2;
			this.menuItem5.Text = "Список автомобилей модели";
			this.menuItem5.Click += new System.EventHandler(this.menuItem5_Click);
			// 
			// menuItem6
			// 
			this.menuItem6.Index = 3;
			this.menuItem6.Text = "-";
			// 
			// button_new_brand
			// 
			this.button_new_brand.Image = ((System.Drawing.Bitmap)(resources.GetObject("button_new_brand.Image")));
			this.button_new_brand.Location = new System.Drawing.Point(8, 8);
			this.button_new_brand.Name = "button_new_brand";
			this.button_new_brand.Size = new System.Drawing.Size(24, 23);
			this.button_new_brand.TabIndex = 3;
			this.button_new_brand.Click += new System.EventHandler(this.button_new_brand_Click);
			// 
			// button_delete_brand
			// 
			this.button_delete_brand.Image = ((System.Drawing.Bitmap)(resources.GetObject("button_delete_brand.Image")));
			this.button_delete_brand.Location = new System.Drawing.Point(32, 8);
			this.button_delete_brand.Name = "button_delete_brand";
			this.button_delete_brand.Size = new System.Drawing.Size(24, 23);
			this.button_delete_brand.TabIndex = 4;
			this.button_delete_brand.Click += new System.EventHandler(this.button_delete_brand_Click);
			// 
			// button_update_brand
			// 
			this.button_update_brand.Image = ((System.Drawing.Bitmap)(resources.GetObject("button_update_brand.Image")));
			this.button_update_brand.Location = new System.Drawing.Point(56, 8);
			this.button_update_brand.Name = "button_update_brand";
			this.button_update_brand.Size = new System.Drawing.Size(24, 23);
			this.button_update_brand.TabIndex = 5;
			this.button_update_brand.Click += new System.EventHandler(this.button_update_brand_Click);
			// 
			// contextMenu2
			// 
			this.contextMenu2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.menuItem9,
																						 this.menuItem4,
																						 this.menuItem7,
																						 this.menuItem8});
			// 
			// menuItem9
			// 
			this.menuItem9.Index = 0;
			this.menuItem9.Text = "Свойства";
			this.menuItem9.Click += new System.EventHandler(this.menuItem9_Click);
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 1;
			this.menuItem4.Text = "Список автомобилей модели";
			this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
			// 
			// menuItem7
			// 
			this.menuItem7.Index = 2;
			this.menuItem7.Text = "-";
			// 
			// menuItem8
			// 
			this.menuItem8.Index = 3;
			this.menuItem8.Text = "Удалить модель";
			this.menuItem8.Click += new System.EventHandler(this.menuItem8_Click);
			// 
			// button_new_model
			// 
			this.button_new_model.Image = ((System.Drawing.Bitmap)(resources.GetObject("button_new_model.Image")));
			this.button_new_model.Location = new System.Drawing.Point(320, 16);
			this.button_new_model.Name = "button_new_model";
			this.button_new_model.Size = new System.Drawing.Size(24, 23);
			this.button_new_model.TabIndex = 6;
			this.button_new_model.Click += new System.EventHandler(this.button_new_model_Click);
			// 
			// menuItem10
			// 
			this.menuItem10.Index = 4;
			this.menuItem10.Text = "Добавить в продажи";
			this.menuItem10.Click += new System.EventHandler(this.menuItem10_Click);
			// 
			// menuItem11
			// 
			this.menuItem11.Index = 5;
			this.menuItem11.Text = "Убрать из продаж";
			this.menuItem11.Click += new System.EventHandler(this.menuItem11_Click);
			// 
			// FormManageModel
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(624, 333);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.button_new_model,
																		  this.button_update_brand,
																		  this.button_delete_brand,
																		  this.button_new_brand,
																		  this.button_tobrand,
																		  this.listView1,
																		  this.treeView1});
			this.MaximumSize = new System.Drawing.Size(632, 900);
			this.MinimumSize = new System.Drawing.Size(632, 0);
			this.Name = "FormManageModel";
			this.Text = "Управление списком моделей";
			this.ResumeLayout(false);

		}
		#endregion

		private void button_tobrand_Click(object sender, System.EventArgs e)
		{
			// Добавить выбранные модели в список моделей выбранного бренда
			// Находим выбранный бренд
			TreeNode node = treeView1.SelectedNode;
			if(node == null) return;
			if(node.Parent != null) return;		// Это не бренд а модель
			if(node.Tag == null) return;
			if((long)node.Tag == 0) return;
			// Запрос подтверждения
			if(MessageBox.Show("Добавить выбранные модели в сисок моделей для " + node.Text, "Запрос", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
			// Записываем все по очереди
			foreach(ListViewItem item in listView1.SelectedItems)
			{
				if((long)item.Tag != 0)
				{
					DbSqlModel.InsertBrandModel((long)node.Tag, (long)item.Tag);
				}
			}
			// Перезачитываем список невыбранных моделей и моделей изменяемого бренда
			listView1.Items.Clear();
			node.Nodes.Clear();
			DbSqlModel.SelectInTree(node, (long)node.Tag);
			DbSqlModel.SelectInList(listView1, 0);
			node.Expand();
		}

		private void menuItem3_Click(object sender, System.EventArgs e)
		{
			// Удалить модель из списка моделей Бренда
			// Ищем выбранный елемент дерева
			TreeNode node = Db.GetItemSelected(treeView1);
			if(node == null) return;
			if(node.Tag == null) return;
			if(node.Parent == null) return;	// Это Бренд
			if((long)node.Tag == 0) return;

			if(DbSqlModel.DeleteBrandModel((long)node.Tag)== true)
			{
				node.Parent.Nodes.Remove(node);
				listView1.Items.Clear();
				DbSqlModel.SelectInList(listView1, 0);
			}

		}

		private void treeView1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if(e.Button == MouseButtons.Right)
			{
				// Находим элемент на котором отпустили
				TreeNode node = Db.GetItemPosition(treeView1);
				treeView1.SelectedNode = node;

				// На отпускание правой кнопки мышки - всплывающее меню
				// Настройка меню
				// Права пользователя
				// Отключаем все запрещенное
				//menuItem6.Enabled = false;
				
				// Включаем по разрешению
				//string login = Form1.currentLogin.ToLower();
				//if (login == "админ")
				//{
				//	menuItem6.Enabled = true;
				//}
				// Настройка меню исходя из свойств выбранной карточки
				// Показ меню
				contextMenu1.Show(treeView1, new Point(e.X, e.Y));
			}
		}

		private void button_new_brand_Click(object sender, System.EventArgs e)
		{
			// Добавление нового Бренда
			FormSelectString dialog = new FormSelectString("Введине наименование нового бренда", "");
			if(dialog.ShowDialog() != DialogResult.OK) return;
			DtBrand brand = new DtBrand();
			brand.SetData("НАИМЕНОВАНИЕ_АВТОМОБИЛЬ_БРЕНД", dialog.SelectedText);
			if(brand.CheckData("НАИМЕНОВАНИЕ_АВТОМОБИЛЬ_БРЕНД") != true) return;
			brand = DbSqlBrand.Insert(brand);
			if(brand == null) return;

			// Добавляем в дерево
			TreeNode node_insert;
			if(treeView1.Nodes.Count == 0)
			{
				node_insert = treeView1.Nodes.Add("");
				brand.SetTNode(node_insert);
				return;
			}
			string name = (string)brand.GetData("НАИМЕНОВАНИЕ_АВТОМОБИЛЬ_БРЕНД");
			node_insert = new TreeNode("");
			brand.SetTNode(node_insert);
			foreach(TreeNode node in treeView1.Nodes)
			{
				if(name.CompareTo(node.Text) < 0)
				{
					treeView1.Nodes.Insert(node.Index, node_insert);
					return;
				}
			}
			node_insert = treeView1.Nodes.Add("");
			brand.SetTNode(node_insert);
			return;
		}

		private void button_delete_brand_Click(object sender, System.EventArgs e)
		{
			// Удалить выбранный Бренд
			// Удалить модель из списка моделей Бренда
			// Ищем выбранный елемент дерева
			TreeNode node = Db.GetItemSelected(treeView1);
			if(node == null) return;
			if(node.Tag == null) return;
			if(node.Parent != null) return;	// Это не Бренд
			if((long)node.Tag == 0) return;

			// Запрос подтверждения удаление
			if(MessageBox.Show("Удалить бренд " + node.Text + "?", "Запрос", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
			if(DbSqlBrand.Delete((long)node.Tag) == false) return;
			if(node.Nodes != null && node.Nodes.Count != 0)
			{
				listView1.Items.Clear();
				DbSqlModel.SelectInList(listView1, 0);
			}
			treeView1.Nodes.Remove(node);
		}

		private void button_update_brand_Click(object sender, System.EventArgs e)
		{
			// Изменить выбранный Бренд
			// Ищем выбранный елемент дерева
			TreeNode node = Db.GetItemSelected(treeView1);
			if(node == null) return;
			if(node.Tag == null) return;
			if(node.Parent != null) return;	// Это не Бренд
			if((long)node.Tag == 0) return;
			
			// Запрос подтверждение изменения
			if(MessageBox.Show("Изменить бренд " + node.Text + "?", "Запрос", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

			FormSelectString dialog = new FormSelectString("Введине новое наименование бренда", node.Text);
			if(dialog.ShowDialog() != DialogResult.OK) return;
			DtBrand brand = new DtBrand();
			brand.SetData("КОД_АВТОМОБИЛЬ_БРЕНД", (long)node.Tag);
			brand.SetData("НАИМЕНОВАНИЕ_АВТОМОБИЛЬ_БРЕНД", dialog.SelectedText);
			if(brand.CheckData("КОД_АВТОМОБИЛЬ_БРЕНД") != true) return;
			if(brand.CheckData("НАИМЕНОВАНИЕ_АВТОМОБИЛЬ_БРЕНД") != true) return;
			if(DbSqlBrand.Update(brand) == false) return;
			if(brand == null) return;
			brand.SetTNode(node);
		}

		private void menuItem4_Click(object sender, System.EventArgs e)
		{
			// Выдаем список автомобилей записанных на эту модель
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			if(item.Tag == null) return;
			if((long)item.Tag == 0) return;

			// Вызов списка автомобилей
			FormListAuto dialog = new FormListAuto(1, item.Tag);
			dialog.Show();
		}

		private void listView1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			// Вызываем менюшку
			if(e.Button == MouseButtons.Right)
			{
				// На отпускание правой кнопки мышки - всплывающее меню
				// Настройка меню
				// Права пользователя
				// Отключаем все запрещенное
				menuItem8.Enabled = false;
				
				// Включаем по разрешению
				string login = Form1.currentLogin.ToLower();
				if (login == "админ")
				{
					menuItem8.Enabled = true;
				}
				//Настройка меню исходя из свойств выбранной карточки
				// Показ меню
				contextMenu2.Show(listView1, new Point(e.X, e.Y));
			}
		}

		private void menuItem5_Click(object sender, System.EventArgs e)
		{
			// Ищем выбранный елемент дерева
			TreeNode node = Db.GetItemSelected(treeView1);
			if(node == null) return;
			if(node.Tag == null) return;
			if(node.Parent == null) return;	// Это не Модель
			if((long)node.Tag == 0) return;

			// Вызов списка автомобилей
			FormListAuto dialog = new FormListAuto(1, node.Tag);
			dialog.Show();
		}

		private void menuItem8_Click(object sender, System.EventArgs e)
		{
			// Удаление модели
			// Выдаем список автомобилей записанных на эту модель
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			if(item.Tag == null) return;
			if((long)item.Tag == 0) return;

			// Подтверждение удаления
			if(MessageBox.Show("Удалить модель " + item.Text + "?", "Запрос", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

			if(DbSqlModel.Delete((long)item.Tag) == false) return;
			listView1.Items.Remove(item);
		}

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			// Изменение свойств выбранной модели
			// Ищем выбранный елемент дерева
			TreeNode node = Db.GetItemSelected(treeView1);
			if(node == null) return;
			if(node.Tag == null) return;
			if(node.Parent == null) return;	// Это не Модель
			if((long)node.Tag == 0) return;

			FormUpdateModel dialog = new FormUpdateModel((long)node.Tag);
			dialog.SetExchange(2, node, null, null, null);
			dialog.ShowDialog();
		}

		private void menuItem9_Click(object sender, System.EventArgs e)
		{
			// Свойства модели из листа
			// Выдаем список автомобилей записанных на эту модель
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			if(item.Tag == null) return;
			if((long)item.Tag == 0) return;

			FormUpdateModel dialog = new FormUpdateModel((long)item.Tag);
			dialog.SetExchange(2, null, item, null, null);
			dialog.ShowDialog();
		}

		private void button_new_model_Click(object sender, System.EventArgs e)
		{
			// Добавление новой модели в лист
			FormUpdateModel dialog = new FormUpdateModel(0);
			dialog.SetExchange(1, null, null, null, listView1);
			dialog.ShowDialog();
		}

		private void menuItem10_Click(object sender, System.EventArgs e)
		{
			// Добавить модель в список продаж
			TreeNode node = Db.GetItemSelected(treeView1);
			if(node == null) return;
			if(node.Tag == null) return;
			if(node.Parent == null) return;	// Это не Модель
			if((long)node.Tag == 0) return;

			long code_model = (long)node.Tag;
			DbSqlModel.UpdateSetSell(code_model);
			DtModel model = DbSqlModel.Find(code_model);
			if (model == null) return;
			node.BackColor		= Color.LightGreen;
		}

		private void menuItem11_Click(object sender, System.EventArgs e)
		{
			// Убрать модель из списока продаж
			TreeNode node = Db.GetItemSelected(treeView1);
			if(node == null) return;
			if(node.Tag == null) return;
			if(node.Parent == null) return;	// Это не Модель
			if((long)node.Tag == 0) return;

			long code_model = (long)node.Tag;
			DbSqlModel.UpdateRemoveSell(code_model);
			DtModel model =DbSqlModel.Find(code_model);
			if (model == null) return;
			node.BackColor		= Color.White;
		}
	}
}
