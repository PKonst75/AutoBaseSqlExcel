using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormManageDirectoryWork.
	/// </summary>
	public class FormManageDirectoryWork : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView listViewDirectoryWork;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.Button buttonDeleteDirectoryWork;
		private System.Windows.Forms.Button buttonPropertiesDirectoryWork;
		private System.Windows.Forms.TreeView treeViewWorkGroup;
		private System.Windows.Forms.ImageList imageListWorkGroup;
		private System.Windows.Forms.Button buttonNewWorkGroup;
		private System.Windows.Forms.Button buttonPropertieWorkGroup;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.ListView listViewWorkGroupItems;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.TextBox textBoxCategorySearch;

		object dragdropData = null;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button buttonNewDirecoryWork;
		DbCategorySearch categorySearchSelection	= null;
		private DbDirectoryWork		directoryWorkSelection;

		public FormManageDirectoryWork()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			// Тестово - загружаем полный список справочника работ
			DbDirectoryWork.FillList(listViewDirectoryWork);
			// Загружаем дерево каталога работ
			DbWorkGroup.FillTree(treeViewWorkGroup);
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormManageDirectoryWork));
			this.listViewDirectoryWork = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.buttonDeleteDirectoryWork = new System.Windows.Forms.Button();
			this.buttonPropertiesDirectoryWork = new System.Windows.Forms.Button();
			this.treeViewWorkGroup = new System.Windows.Forms.TreeView();
			this.imageListWorkGroup = new System.Windows.Forms.ImageList(this.components);
			this.buttonNewWorkGroup = new System.Windows.Forms.Button();
			this.buttonPropertieWorkGroup = new System.Windows.Forms.Button();
			this.listViewWorkGroupItems = new System.Windows.Forms.ListView();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.buttonNewDirecoryWork = new System.Windows.Forms.Button();
			this.textBoxCategorySearch = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// listViewDirectoryWork
			// 
			this.listViewDirectoryWork.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.listViewDirectoryWork.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																									this.columnHeader1});
			this.listViewDirectoryWork.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.listViewDirectoryWork.FullRowSelect = true;
			this.listViewDirectoryWork.Location = new System.Drawing.Point(8, 248);
			this.listViewDirectoryWork.Name = "listViewDirectoryWork";
			this.listViewDirectoryWork.Size = new System.Drawing.Size(672, 80);
			this.listViewDirectoryWork.TabIndex = 0;
			this.listViewDirectoryWork.View = System.Windows.Forms.View.Details;
			this.listViewDirectoryWork.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listViewDirectoryWork_KeyDown);
			this.listViewDirectoryWork.DoubleClick += new System.EventHandler(this.listViewDirectoryWork_DoubleClick);
			this.listViewDirectoryWork.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listViewDirectoryWork_ColumnClick);
			this.listViewDirectoryWork.MouseMove += new System.Windows.Forms.MouseEventHandler(this.listViewDirectoryWork_MouseMove);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Наименование";
			this.columnHeader1.Width = 535;
			// 
			// buttonDeleteDirectoryWork
			// 
			this.buttonDeleteDirectoryWork.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.buttonDeleteDirectoryWork.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonDeleteDirectoryWork.Image")));
			this.buttonDeleteDirectoryWork.Location = new System.Drawing.Point(656, 224);
			this.buttonDeleteDirectoryWork.Name = "buttonDeleteDirectoryWork";
			this.buttonDeleteDirectoryWork.Size = new System.Drawing.Size(24, 23);
			this.buttonDeleteDirectoryWork.TabIndex = 1;
			this.toolTip1.SetToolTip(this.buttonDeleteDirectoryWork, "Удаление из справочника выбранных работ");
			this.buttonDeleteDirectoryWork.Click += new System.EventHandler(this.buttonDeleteDirectoryWork_Click);
			// 
			// buttonPropertiesDirectoryWork
			// 
			this.buttonPropertiesDirectoryWork.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.buttonPropertiesDirectoryWork.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonPropertiesDirectoryWork.Image")));
			this.buttonPropertiesDirectoryWork.Location = new System.Drawing.Point(632, 224);
			this.buttonPropertiesDirectoryWork.Name = "buttonPropertiesDirectoryWork";
			this.buttonPropertiesDirectoryWork.Size = new System.Drawing.Size(24, 23);
			this.buttonPropertiesDirectoryWork.TabIndex = 2;
			this.toolTip1.SetToolTip(this.buttonPropertiesDirectoryWork, "Изменение параметров работы");
			this.buttonPropertiesDirectoryWork.Click += new System.EventHandler(this.buttonPropertiesDirectoryWork_Click);
			// 
			// treeViewWorkGroup
			// 
			this.treeViewWorkGroup.AllowDrop = true;
			this.treeViewWorkGroup.HideSelection = false;
			this.treeViewWorkGroup.ImageList = this.imageListWorkGroup;
			this.treeViewWorkGroup.Location = new System.Drawing.Point(8, 8);
			this.treeViewWorkGroup.Name = "treeViewWorkGroup";
			this.treeViewWorkGroup.Size = new System.Drawing.Size(272, 232);
			this.treeViewWorkGroup.TabIndex = 3;
			this.treeViewWorkGroup.KeyDown += new System.Windows.Forms.KeyEventHandler(this.treeViewWorkGroup_KeyDown);
			this.treeViewWorkGroup.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewWorkGroup_AfterSelect);
			this.treeViewWorkGroup.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeViewWorkGroup_BeforeExpand);
			this.treeViewWorkGroup.DragEnter += new System.Windows.Forms.DragEventHandler(this.treeViewWorkGroup_DragEnter);
			this.treeViewWorkGroup.MouseMove += new System.Windows.Forms.MouseEventHandler(this.treeViewWorkGroup_MouseMove);
			this.treeViewWorkGroup.DragDrop += new System.Windows.Forms.DragEventHandler(this.treeViewWorkGroup_DragDrop);
			// 
			// imageListWorkGroup
			// 
			this.imageListWorkGroup.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.imageListWorkGroup.ImageSize = new System.Drawing.Size(16, 16);
			this.imageListWorkGroup.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListWorkGroup.ImageStream")));
			this.imageListWorkGroup.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// buttonNewWorkGroup
			// 
			this.buttonNewWorkGroup.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonNewWorkGroup.Image")));
			this.buttonNewWorkGroup.Location = new System.Drawing.Point(280, 8);
			this.buttonNewWorkGroup.Name = "buttonNewWorkGroup";
			this.buttonNewWorkGroup.Size = new System.Drawing.Size(24, 23);
			this.buttonNewWorkGroup.TabIndex = 4;
			this.toolTip1.SetToolTip(this.buttonNewWorkGroup, "Новая группа работ");
			this.buttonNewWorkGroup.Click += new System.EventHandler(this.buttonNewWorkGroup_Click);
			// 
			// buttonPropertieWorkGroup
			// 
			this.buttonPropertieWorkGroup.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonPropertieWorkGroup.Image")));
			this.buttonPropertieWorkGroup.Location = new System.Drawing.Point(304, 8);
			this.buttonPropertieWorkGroup.Name = "buttonPropertieWorkGroup";
			this.buttonPropertieWorkGroup.Size = new System.Drawing.Size(24, 23);
			this.buttonPropertieWorkGroup.TabIndex = 5;
			this.toolTip1.SetToolTip(this.buttonPropertieWorkGroup, "Изменение выбранной группы работ");
			this.buttonPropertieWorkGroup.Click += new System.EventHandler(this.buttonPropertieWorkGroup_Click);
			// 
			// listViewWorkGroupItems
			// 
			this.listViewWorkGroupItems.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.listViewWorkGroupItems.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																									 this.columnHeader2});
			this.listViewWorkGroupItems.Location = new System.Drawing.Point(336, 8);
			this.listViewWorkGroupItems.Name = "listViewWorkGroupItems";
			this.listViewWorkGroupItems.Size = new System.Drawing.Size(344, 208);
			this.listViewWorkGroupItems.TabIndex = 6;
			this.listViewWorkGroupItems.View = System.Windows.Forms.View.Details;
			this.listViewWorkGroupItems.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listViewWorkGroupItems_KeyDown);
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Наименование";
			this.columnHeader2.Width = 323;
			// 
			// buttonNewDirecoryWork
			// 
			this.buttonNewDirecoryWork.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.buttonNewDirecoryWork.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonNewDirecoryWork.Image")));
			this.buttonNewDirecoryWork.Location = new System.Drawing.Point(608, 224);
			this.buttonNewDirecoryWork.Name = "buttonNewDirecoryWork";
			this.buttonNewDirecoryWork.Size = new System.Drawing.Size(24, 23);
			this.buttonNewDirecoryWork.TabIndex = 9;
			this.toolTip1.SetToolTip(this.buttonNewDirecoryWork, "Добавление новой работы в справочник");
			this.buttonNewDirecoryWork.Click += new System.EventHandler(this.buttonNewDirecoryWork_Click);
			// 
			// textBoxCategorySearch
			// 
			this.textBoxCategorySearch.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.textBoxCategorySearch.Location = new System.Drawing.Point(400, 224);
			this.textBoxCategorySearch.Name = "textBoxCategorySearch";
			this.textBoxCategorySearch.ReadOnly = true;
			this.textBoxCategorySearch.Size = new System.Drawing.Size(208, 23);
			this.textBoxCategorySearch.TabIndex = 7;
			this.textBoxCategorySearch.Text = "";
			this.textBoxCategorySearch.Click += new System.EventHandler(this.textBoxCategorySearch_Click);
			// 
			// label1
			// 
			this.label1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.label1.Location = new System.Drawing.Point(336, 224);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(56, 23);
			this.label1.TabIndex = 8;
			this.label1.Text = "Поиск";
			// 
			// FormManageDirectoryWork
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.ClientSize = new System.Drawing.Size(688, 357);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.buttonNewDirecoryWork,
																		  this.label1,
																		  this.textBoxCategorySearch,
																		  this.listViewWorkGroupItems,
																		  this.buttonPropertieWorkGroup,
																		  this.buttonNewWorkGroup,
																		  this.treeViewWorkGroup,
																		  this.buttonPropertiesDirectoryWork,
																		  this.buttonDeleteDirectoryWork,
																		  this.listViewDirectoryWork});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.Name = "FormManageDirectoryWork";
			this.Text = "Управление справочником работ";
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonDeleteDirectoryWork_Click(object sender, System.EventArgs e)
		{
			// Удаляем работы из справочника по выбранному списку
			foreach(ListViewItem item in listViewDirectoryWork.SelectedItems)
			{
				DbDirectoryWork	directoryWork	= (DbDirectoryWork)item.Tag;
				if(directoryWork != null)
				{
					if(directoryWork.Delete() == true)
					{
						listViewDirectoryWork.Items.Remove(item);
					}
					else
					{
						Db.ShowFaults();
					}
				}
			}
		}

		private void buttonPropertiesDirectoryWork_Click(object sender, System.EventArgs e)
		{
			// Определение исправляемого элемента
			ListViewItem item = Db.GetItemSelected(listViewDirectoryWork);
			if(item == null) return;
			DbDirectoryWork directoryWork = (DbDirectoryWork)item.Tag;
			if(directoryWork == null) return;
			// Вызов диалога исправления элемента справочника
			FormDirectoryWork	dialog	= new FormDirectoryWork(directoryWork);
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			// Изменяем элемент в списке
			dialog.DirectoryWork.SetLVItem(item);
		}

		private void buttonNewWorkGroup_Click(object sender, System.EventArgs e)
		{
			// Добавление нового группового элемента
			DbWorkGroup workGroup = null;
			TreeNode node = Db.GetItemSelected(treeViewWorkGroup);
			if(node != null)
			{
				workGroup = (DbWorkGroup)node.Tag;
				if(workGroup == null) node = null;
			}
			// Вызов диалога добавления
			FormWorkGroup dialog	= new FormWorkGroup(null, workGroup);
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			// Добавляем в дерево новый элемент
			if(node != null) node.Nodes.Add(dialog.WorkGroup.TVItem);
			else treeViewWorkGroup.Nodes.Add(dialog.WorkGroup.TVItem);
		}

		private void buttonPropertieWorkGroup_Click(object sender, System.EventArgs e)
		{
			// Свойства выбранной группы трудоемкостей
			DbWorkGroup workGroup = null;
			TreeNode node = Db.GetItemSelected(treeViewWorkGroup);
			if(node == null)	return;
			workGroup = (DbWorkGroup)node.Tag;
			if(workGroup == null) return;
			// Вызов диалога изменения
			FormWorkGroup dialog	= new FormWorkGroup(workGroup, null);
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			// Изменяем элемент в дереве
			dialog.WorkGroup.SetTVItem(node);
		}

		protected void treeViewWorkGroup_BeforeExpand(object sender, TreeViewCancelEventArgs e)
		{
			DbWorkGroup element = (DbWorkGroup)e.Node.Tag;
			if(element.Expand) return;
			foreach(TreeNode node in e.Node.Nodes)
			{
				DbWorkGroup.FillTree(node);
			}
			element.Expand = true;
			element.SetTVItem(e.Node);
		}

		// Перетаскивание элементов каталога мышкой
		protected void treeViewWorkGroup_MouseMove(object sender, MouseEventArgs e)
		{
			if(e.Button != MouseButtons.Left) return;
			TreeNode dragNode	= Db.GetItemPosition(treeViewWorkGroup);
			if(dragNode	== null) return;
			if(dragNode.Tag == null) return;

			// Начинаем операцию по перетаскиванию
			dragdropData = (TreeNode)dragNode;
			this.DoDragDrop("Внутри каталога", DragDropEffects.Move);
		}
		protected void treeViewWorkGroup_DragEnter(object sender, DragEventArgs e)
		{
			e.Effect = DragDropEffects.Move;
		}
		protected void treeViewWorkGroup_DragDrop(object sender, DragEventArgs e)
		{
			// Проверяем на правильность перетаскиваемого объекта
			if(e.Data.GetDataPresent(DataFormats.Text) != true) return;
			if((string)e.Data.GetData(DataFormats.Text) == "Внутри каталога")
			{
				// Объект который
				TreeNode node			= (TreeNode)dragdropData;
				dragdropData			= null;
				DbWorkGroup workGroup	= (DbWorkGroup)node.Tag;
				// Объект куда
				TreeNode node2Move		= Db.GetItemPosition(treeViewWorkGroup);
				// Неьзя перетаскивать на самого себя!
				if(node2Move == node) return;
				DbWorkGroup workGroup2Move;
				if(node2Move == null)
					workGroup2Move = null;
				else
					workGroup2Move = (DbWorkGroup)node2Move.Tag;
				DbWorkGroup workGroupNew = new DbWorkGroup(workGroup);
				if(workGroup2Move == null) workGroupNew.CodeParent = 0;
				else workGroupNew.CodeParent = workGroup2Move.Code;

				// Пытаемся записать новый элемент
				if(workGroupNew.Write() == false)
				{
					Db.ShowFaults();
					return;
				}
				workGroupNew.SetTVItem(node);
				if(node2Move != null)
				{
					treeViewWorkGroup.Nodes.Remove(node);
					node2Move.Nodes.Insert(1, node);
				}
				else
				{
					treeViewWorkGroup.Nodes.Remove(node);
					treeViewWorkGroup.Nodes.Insert(1, node);
				}
				return;
			}
			if((string)e.Data.GetData(DataFormats.Text) == "Трудоемкость в каталог")
			{
				// Объект куда
				TreeNode node2Move		= Db.GetItemPosition(treeViewWorkGroup);
				// Перетаскивать можно только на объект
				if(node2Move == null) return;
				DbWorkGroup workGroup2Move = (DbWorkGroup)node2Move.Tag;
				if(workGroup2Move == null) return;
				if(workGroup2Move.FlagElement == false) return;

				// Осуществляем выбор элемента, если он еще не выбран
				if(treeViewWorkGroup.SelectedNode != node2Move) treeViewWorkGroup.SelectedNode = node2Move;

				ListView.SelectedListViewItemCollection selection = (ListView.SelectedListViewItemCollection)dragdropData;
				if(selection.Count == 0) return;
				foreach(ListViewItem item in selection)
				{
					DbDirectoryWork directoryWork = (DbDirectoryWork)item.Tag;
					DbWorkGroupDirectoryWork link = new DbWorkGroupDirectoryWork(workGroup2Move, directoryWork, true);
					if(link.Write() == true)
					{
						listViewWorkGroupItems.Items.Add(directoryWork.LVItem);
					}
				}
			}
		}
		protected void treeViewWorkGroup_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Escape)
			{
				// Отменяем выбор существующего элемента
				treeViewWorkGroup.SelectedNode = null;
				return;
			}
			if(e.KeyCode == Keys.Delete)
			{
				TreeNode node = treeViewWorkGroup.SelectedNode;
				if(node	== null) return;
				DbWorkGroup	workGroup = (DbWorkGroup)node.Tag;
				if(workGroup == null) return;

				if(workGroup.Delete())
				{
					treeViewWorkGroup.Nodes.Remove(node);
				}
				else
				{
					Db.ShowFaults();
				}
				return;
			}
		}
		// Перетаскивание элементов списка трудоемкостей мышкой
		protected void listViewDirectoryWork_MouseMove(object sender, MouseEventArgs e)
		{
			if(e.Button != MouseButtons.Left) return;
			if(listViewDirectoryWork.SelectedItems.Count == 0) return;

			// Начинаем операцию по перетаскиванию
			dragdropData = (ListView.SelectedListViewItemCollection)listViewDirectoryWork.SelectedItems;
			this.DoDragDrop("Трудоемкость в каталог", DragDropEffects.Move);
		}

		private void treeViewWorkGroup_AfterSelect(object sender, TreeViewEventArgs e)
		{
			listViewWorkGroupItems.Items.Clear();
			// Перезаполняием лист
			DbWorkGroup workGroup	= (DbWorkGroup)e.Node.Tag;
			if(workGroup == null) return;
			if(workGroup.FlagElement	== false) return;

			DbDirectoryWork.FillList(listViewWorkGroupItems, workGroup);
		}
		private void listViewWorkGroupItems_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Delete)
			{
				// Удаляем связку
				TreeNode node = treeViewWorkGroup.SelectedNode;
				if(node == null) return;
				DbWorkGroup workGroup = (DbWorkGroup)node.Tag;
				if(workGroup == null) return;
				if(listViewWorkGroupItems.SelectedItems.Count == 0) return;
				foreach(ListViewItem item in listViewWorkGroupItems.SelectedItems)
				{
					DbDirectoryWork element = (DbDirectoryWork)item.Tag;
					if(element != null)
					{
						DbWorkGroupDirectoryWork link = new DbWorkGroupDirectoryWork(workGroup, element, false);
						if(link.Write() == true) listViewWorkGroupItems.Items.Remove(item);
					}
				}
				Db.ShowFaults();
				return;
			}
		}
		private void listViewDirectoryWork_ColumnClick(object sender, ColumnClickEventArgs e)
		{
			if(e.Column == 0)
			{
				// Зпарос маски для поиска
				FormSelectString dialog = new FormSelectString("Поиск работы в справочнике", "");
				dialog.ShowDialog(this);
				if(dialog.DialogResult != DialogResult.OK) return;

				listViewDirectoryWork.Items.Clear();
				//DbDirectoryWork.FillList(listViewDirectoryWork, dialog.SelectedTextMask);
				DbDirectoryWork.FillList(listViewDirectoryWork, categorySearchSelection, dialog.SelectedTextMask);
			}
		}
		private void listViewDirectoryWork_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Delete)
			{
				return;
			}
			if(e.KeyCode == Keys.Enter)
			{
				// Выбор элемента
				ListViewItem item		= Db.GetItemSelected(listViewDirectoryWork);
				if(item == null) return;
				directoryWorkSelection	= (DbDirectoryWork)item.Tag;
				if(directoryWorkSelection == null) return;

				this.DialogResult	= DialogResult.OK;
				this.Close();
				return;
			}
			if(e.KeyCode == Keys.Escape)
			{
				return;
			}
			if(e.KeyCode == Keys.Up)
			{
				return;
			}
			if(e.KeyCode == Keys.Down)
			{
				return;
			}
			if(e.KeyCode == Keys.Right)
			{
				return;
			}
			if(e.KeyCode == Keys.Left)
			{
				return;
			}
			if(e.KeyCode == Keys.PageUp)
			{
				return;
			}
			if(e.KeyCode == Keys.PageDown)
			{
				return;
			}
			// Любое доугое нажатие - выход в поиск
			listViewDirectoryWork.HideSelection = false;
			FormSelectionType3 dialog = Db.MakeFormSelectionType3(this, listViewDirectoryWork.Items[0], 0, listViewDirectoryWork);
			dialog.ShowDialog(null);
			listViewDirectoryWork.HideSelection = true;
		}

		private void textBoxCategorySearch_Click(object sender, System.EventArgs e)
		{
			// Инициализируем форму выбора категории поиска
			ArrayList	categorySearch = new ArrayList();
			DbCategorySearch.FillArray(categorySearch);
			FormSelectionType1 dialog = Db.MakeFormSelectionType1(this, textBoxCategorySearch, categorySearch, textBoxCategorySearch.Text, true);
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			categorySearchSelection = (DbCategorySearch)dialog.SelectedElement;
			if(categorySearchSelection != null)
				textBoxCategorySearch.Text	= categorySearchSelection.DbTitle();
			else
				textBoxCategorySearch.Text = "";
			// Обновляем список
			listViewDirectoryWork.Items.Clear();
			DbDirectoryWork.FillList(listViewDirectoryWork, categorySearchSelection);
		}

		private void buttonNewDirecoryWork_Click(object sender, System.EventArgs e)
		{
			// Вызов диалога добавления элемента справочника
			FormDirectoryWork	dialog	= new FormDirectoryWork(null);
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			// Добавляем элемент в список
			listViewDirectoryWork.Items.Add(dialog.DirectoryWork.LVItem);
		}

		private void listViewDirectoryWork_DoubleClick(object sender, EventArgs e)
		{
			// Выбор элемента
			ListViewItem item		= Db.GetItemSelected(listViewDirectoryWork);
			if(item == null) return;
			directoryWorkSelection	= (DbDirectoryWork)item.Tag;
			if(directoryWorkSelection == null) return;

			this.DialogResult	= DialogResult.OK;
			this.Close();
		}

		public DbDirectoryWork DirectoryWork
		{
			get
			{
				return directoryWorkSelection;
			}
		}
	}
}
