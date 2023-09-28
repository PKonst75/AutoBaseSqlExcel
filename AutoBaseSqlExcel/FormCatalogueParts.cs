using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormCatalogueParts.
	/// </summary>
	public class FormCatalogueParts : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button_new_group;
		private System.Windows.Forms.TreeView treeView_groups;
		private System.Windows.Forms.ContextMenu contextMenu_groups;
		private System.Windows.Forms.MenuItem menuItem1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.ListView listView_details;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.CheckBox checkBox_use_groups;

		TreeNode context_menu_node = null;
		Form outer_form				= null;
		bool outer_connection		= false;
		int form_type				= 0;

		public FormCatalogueParts(Form form, int type)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			if(type == 1 && form != null)
			{
				outer_form			= form; 
				outer_connection	= true;
				form_type			= type;
			}

			checkBox_use_groups.Checked = true;
			// Первичное заполнение дерева групп
			DbSqlCatalogueParts.SelectInTree(treeView_groups);
			// Первичное заполнение листа деталей
			DbSqlCatalogueParts.SelectInList(listView_details, 0);
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormCatalogueParts));
			this.treeView_groups = new System.Windows.Forms.TreeView();
			this.button_new_group = new System.Windows.Forms.Button();
			this.contextMenu_groups = new System.Windows.Forms.ContextMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.listView_details = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.checkBox_use_groups = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// treeView_groups
			// 
			this.treeView_groups.HideSelection = false;
			this.treeView_groups.ImageIndex = -1;
			this.treeView_groups.Location = new System.Drawing.Point(8, 40);
			this.treeView_groups.Name = "treeView_groups";
			this.treeView_groups.SelectedImageIndex = -1;
			this.treeView_groups.Size = new System.Drawing.Size(168, 272);
			this.treeView_groups.TabIndex = 0;
			this.treeView_groups.MouseUp += new System.Windows.Forms.MouseEventHandler(this.treeView_groups_MouseUp);
			this.treeView_groups.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_groups_AfterSelect);
			// 
			// button_new_group
			// 
			this.button_new_group.Image = ((System.Drawing.Bitmap)(resources.GetObject("button_new_group.Image")));
			this.button_new_group.Location = new System.Drawing.Point(8, 16);
			this.button_new_group.Name = "button_new_group";
			this.button_new_group.Size = new System.Drawing.Size(24, 23);
			this.button_new_group.TabIndex = 1;
			this.button_new_group.Click += new System.EventHandler(this.button_new_group_Click);
			// 
			// contextMenu_groups
			// 
			this.contextMenu_groups.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																							   this.menuItem1,
																							   this.menuItem2,
																							   this.menuItem3});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.Text = "Добавить группу";
			this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 1;
			this.menuItem2.Text = "Изменить наименование";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 2;
			this.menuItem3.Text = "Добавить деталь в группу";
			this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
			// 
			// listView_details
			// 
			this.listView_details.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																							   this.columnHeader1});
			this.listView_details.Location = new System.Drawing.Point(192, 40);
			this.listView_details.Name = "listView_details";
			this.listView_details.Size = new System.Drawing.Size(424, 272);
			this.listView_details.TabIndex = 2;
			this.listView_details.View = System.Windows.Forms.View.Details;
			this.listView_details.DoubleClick += new System.EventHandler(this.listView_details_DoubleClick);
			this.listView_details.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView_details_ColumnClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Наименование";
			this.columnHeader1.Width = 348;
			// 
			// checkBox_use_groups
			// 
			this.checkBox_use_groups.Location = new System.Drawing.Point(192, 0);
			this.checkBox_use_groups.Name = "checkBox_use_groups";
			this.checkBox_use_groups.Size = new System.Drawing.Size(144, 24);
			this.checkBox_use_groups.TabIndex = 3;
			this.checkBox_use_groups.Text = "Сортировка по дереву";
			this.checkBox_use_groups.CheckedChanged += new System.EventHandler(this.checkBox_use_groups_CheckedChanged);
			// 
			// FormCatalogueParts
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(624, 325);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.checkBox_use_groups,
																		  this.listView_details,
																		  this.button_new_group,
																		  this.treeView_groups});
			this.Name = "FormCatalogueParts";
			this.Text = "Каталог - Запасные части";
			this.ResumeLayout(false);

		}
		#endregion

		private void button_new_group_Click(object sender, System.EventArgs e)
		{
			// Создаем новую группу в каталоге запасных частей
			// Определяем элемент дерева, который сейчас выбран
			TreeNode node = Db.GetItemSelected(treeView_groups);
			long code_group = 0L;
			if (node != null)
			{
				object o = node.Tag;
				if( o != null)
				{
					code_group = (long)o;
				}
			}
			// Запрашиваем наименование нового элемента
			FormSelectString dialog = new FormSelectString("Наименование группы", "");
			if(dialog.ShowDialog() != DialogResult.OK) return;
			string name = dialog.SelectedText;
			name = name.Trim();
			if(name.Length == 0) return;
			// Подготавливаем новый элемент
			DtCatalogueParts element = new DtCatalogueParts();
			element.SetData("НАИМЕНОВАНИЕ",(object)name);
			element.SetData("КОД_ГРУППА",(object)code_group);
			element.SetData("ФЛАГ_ГРУППА",(object)true);
			// Пытаемся записать новый элемент
			element = DbSqlCatalogueParts.Insert(element);
			if(element == null) return;
			MessageBox.Show("Добавили новую группу");
			TreeNode new_node = null;
			if(node == null)
			{
				new_node = treeView_groups.Nodes.Add("Новый элемент");
			}
			else
			{
				new_node = node.Nodes.Add("Новый элемент");
			}
			if(new_node != null)
				element.SetTNode(new_node);
		}

		private void menuItem1_Click(object sender, System.EventArgs e)
		{
			// Добавление новой группы
			// Создаем новую группу в каталоге запасных частей
			// Определяем элемент дерева, который сейчас выбран
			TreeNode node = context_menu_node;
			context_menu_node	= null;
			long code_group = 0L;
			if (node != null)
			{
				object o = node.Tag;
				if( o != null)
				{
					code_group = (long)o;
				}
			}
			// Запрашиваем наименование нового элемента
			FormSelectString dialog = new FormSelectString("Наименование группы", "");
			if(dialog.ShowDialog() != DialogResult.OK) return;
			string name = dialog.SelectedText;
			name = name.Trim();
			if(name.Length == 0) return;
			// Подготавливаем новый элемент
			DtCatalogueParts element = new DtCatalogueParts();
			element.SetData("НАИМЕНОВАНИЕ",(object)name);
			element.SetData("КОД_ГРУППА",(object)code_group);
			element.SetData("ФЛАГ_ГРУППА",(object)true);
			// Пытаемся записать новый элемент
			element = DbSqlCatalogueParts.Insert(element);
			if(element == null) return;
			MessageBox.Show("Добавили новую группу");
			TreeNode new_node = null;
			if(node == null)
			{
				new_node = treeView_groups.Nodes.Add("Новый элемент");
			}
			else
			{
				new_node = node.Nodes.Add("Новый элемент");
			}
			if(new_node != null)
				element.SetTNode(new_node);
		}

		private void treeView_groups_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if(e.Button == MouseButtons.Right)
			{
				// Отменяем предыдущий элемент на котором сработало контекстное меню
				context_menu_node	= null;
				// На отпускание правой кнопки мышки - всплывающее меню
				// Настройка меню
				// Права пользователя
				// Отключаем все запрещенное
				// Включаем по разрешению
				string login = Form1.currentLogin.ToLower();
				if (login == "заякинм" || login == "админ" || login == "панкратьева"){}
				if (login == "админ"){}
				if (login == "заякинм"){}
				// Запоминание узла, на котором сработало контекстное меню
				context_menu_node	= Db.GetItemPosition(treeView_groups);
				// Показ меню
				contextMenu_groups.Show(treeView_groups, new Point(e.X, e.Y));
			}
		}

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			// Добавление новой группы
			// Создаем новую группу в каталоге запасных частей
			// Определяем элемент дерева, который сейчас выбран
			TreeNode node = context_menu_node;
			context_menu_node	= null;
			long code = 0L;
			if (node != null)
			{
				object o = node.Tag;
				if( o != null)
				{
					code = (long)o;
				}
			}
			if(code == 0) return;
			DtCatalogueParts element = DbSqlCatalogueParts.Find(code);
			if(element == null) return;
			// Запрашиваем наименование нового элемента
			FormSelectString dialog = new FormSelectString("Наименование группы", (string)element.GetData("НАИМЕНОВАНИЕ"));
			if(dialog.ShowDialog() != DialogResult.OK) return;
			string name = dialog.SelectedText;
			name = name.Trim();
			if(name.Length == 0) return;
			// Устанавливавем новое наименование
			element.SetData("НАИМЕНОВАНИЕ",(object)name);
			// Пытаемся записать новый элемент
			if(DbSqlCatalogueParts.Update(element) == false) return;
			MessageBox.Show("Изменили наименование группы");
			element.SetTNode(node);
		}

		private void menuItem3_Click(object sender, System.EventArgs e)
		{
			// Добавление в выбранную группу новой детали
			// Добавление новой группы
			// Создаем новую группу в каталоге запасных частей
			// Определяем элемент дерева, который сейчас выбран
			TreeNode node = context_menu_node;
			context_menu_node	= null;
			long code_group = 0L;
			if (node != null)
			{
				object o = node.Tag;
				if( o != null)
				{
					code_group = (long)o;
				}
			}
			// Запрашиваем наименование новой детали
			FormSelectString dialog = new FormSelectString("Наименование детали", "");
			if(dialog.ShowDialog() != DialogResult.OK) return;
			string name = dialog.SelectedText;
			name = name.Trim();
			if(name.Length == 0) return;
			// Подготавливаем новый элемент
			DtCatalogueParts element = new DtCatalogueParts();
			element.SetData("НАИМЕНОВАНИЕ",(object)name);
			element.SetData("КОД_ГРУППА",(object)code_group);
			element.SetData("ФЛАГ_ГРУППА",(object)false);
			// Пытаемся записать новый элемент
			element = DbSqlCatalogueParts.Insert(element);
			if(element == null) return;
			MessageBox.Show("Добавили новую деталь");
			ListViewItem new_item = null;
			if(node != treeView_groups.SelectedNode)
			{
				return; // Группа в которую добавили элемент сейчас не отображается
			}
			new_item = listView_details.Items.Add("Новый элемент");
			if(new_item != null)
				element.SetLVItem(new_item);
		}

		private void treeView_groups_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			listView_details.Items.Clear();
			// Определяем элемент дерева, который сейчас выбран
			TreeNode node = Db.GetItemSelected(treeView_groups);
			context_menu_node	= null;
			long code_group = 0L;
			if (node != null)
			{
				object o = node.Tag;
				if( o != null)
				{
					code_group = (long)o;
				}
			}
			// После выбора элемента дерева заполняем лист принадлежащими ему элементами
			DbSqlCatalogueParts.SelectInList(listView_details, code_group);
		}

		private void checkBox_use_groups_CheckedChanged(object sender, System.EventArgs e)
		{
			// Переключение режима использовать/неиспользовать группы
			if(checkBox_use_groups.Checked)
			{
				// Включаем режим использования групп
				treeView_groups.CollapseAll();
				treeView_groups.Enabled		= true;
				button_new_group.Enabled	= true;
				// Заполняем все дерево списком, независимо от групп
				listView_details.Items.Clear();
				DbSqlCatalogueParts.SelectInList(listView_details, 0);
			}
			else
			{
				// Выключаем режим использования групп
				treeView_groups.CollapseAll();
				treeView_groups.Enabled		= false;
				button_new_group.Enabled	= false;
				// Заполняем все дерево списком, независимо от групп
				listView_details.Items.Clear();
				DbSqlCatalogueParts.SelectInList(listView_details);
			}
		}

		private void listView_details_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
		{
			// Запуск поиска по имени, если щелкнули по колонке
			FormSelectString dialog;
			switch(e.Column)
			{
				case 0:
					// Поиск по имени
					dialog = new FormSelectString("Наименование детали", "");
					if(dialog.ShowDialog() != DialogResult.OK) return;
					string pattern = dialog.SelectedTextMask;
					if (pattern == "") return;
					listView_details.Items.Clear();
					DbSqlCatalogueParts.SelectInList(listView_details, pattern);
					return;
				default:
					return;
			}
		}

		private void listView_details_DoubleClick(object sender, System.EventArgs e)
		{
			// Определяем элемент листа, который сейчас выбран
			ListViewItem item = Db.GetItemSelected(listView_details);
			long code = 0L;
			if (item != null)
			{
				object o = item.Tag;
				if( o != null)
				{
					code = (long)o;
				}
			}
			if(code == 0) return;
			// Поиск элемента в базе данных
			DtCatalogueParts catalogue_parts = DbSqlCatalogueParts.Find(code);
			if(catalogue_parts == null) return;

			// Дальнейшее зависит от типа внешнего соединения
			if(outer_connection == false) return;
			if(outer_form == null) return;
			if(outer_form.IsDisposed == true) return;
			if(form_type == 1)
			{
				// Заявка на каталог в карточку
				FormCard form = (FormCard)outer_form;
				form.NewCatalogueParts(catalogue_parts);
				return;
			}
		}
	}
}
