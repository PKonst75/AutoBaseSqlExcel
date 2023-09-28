using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Выбираем из списка работу, при определенных условиях - передаем выбранный элемент на обработку внешней форме.
	/// </summary>
	public class FormWorkSelect : System.Windows.Forms.Form
	{
		public delegate void DelegateTransferToForm(ListViewItem item);

		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.ComponentModel.IContainer components;

		DbAutoType				autoTypeSelection;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.TreeView treeView1;
		private System.Windows.Forms.TextBox textBoxCategorySearch;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBoxAutoType;
		private System.Windows.Forms.CheckBox checkBoxShowCommon;
		private System.Windows.Forms.Button buttonUpdate;
		private System.Windows.Forms.ToolTip toolTip1;
		DelegateTransferToForm	transferFunction;
		private System.Windows.Forms.Button buttonToOuterList;
		private System.Windows.Forms.Button buttonNewWork;
		private System.Windows.Forms.ColumnHeader columnHeader6;

		DbCategorySearch	categorySearchSelection = null;

		public FormWorkSelect(DbAutoType srcAutoType, DelegateTransferToForm srcTransferFunction)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			autoTypeSelection	= srcAutoType;
			transferFunction	= srcTransferFunction;

			// Заполняем лист работами
			DbWork.FillList1(listView1, autoTypeSelection, categorySearchSelection, true, 0, "");
			// Загружаем дерево каталога работ
			DbWorkGroup.FillTree(treeView1);
			checkBoxShowCommon.Checked = true;

			if(autoTypeSelection != null)
			{
				textBoxAutoType.Text	= autoTypeSelection.DbTitle();
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormWorkSelect));
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.textBoxCategorySearch = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textBoxAutoType = new System.Windows.Forms.TextBox();
			this.checkBoxShowCommon = new System.Windows.Forms.CheckBox();
			this.buttonUpdate = new System.Windows.Forms.Button();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.buttonToOuterList = new System.Windows.Forms.Button();
			this.buttonNewWork = new System.Windows.Forms.Button();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
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
																						this.columnHeader6});
			this.listView1.FullRowSelect = true;
			this.listView1.HideSelection = false;
			this.listView1.Location = new System.Drawing.Point(8, 152);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(872, 192);
			this.listView1.TabIndex = 0;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listView1_KeyDown);
			this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
			this.listView1.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView1_ColumnClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Код детали";
			this.columnHeader1.Width = 98;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Позиция";
			this.columnHeader2.Width = 85;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Наименование справочник";
			this.columnHeader3.Width = 220;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Наименование";
			this.columnHeader4.Width = 220;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "Расценка";
			this.columnHeader5.Width = 120;
			// 
			// treeView1
			// 
			this.treeView1.ImageIndex = -1;
			this.treeView1.Location = new System.Drawing.Point(8, 0);
			this.treeView1.Name = "treeView1";
			this.treeView1.SelectedImageIndex = -1;
			this.treeView1.Size = new System.Drawing.Size(320, 144);
			this.treeView1.TabIndex = 1;
			this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
			this.treeView1.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView1_BeforeExpand);
			// 
			// textBoxCategorySearch
			// 
			this.textBoxCategorySearch.Location = new System.Drawing.Point(392, 120);
			this.textBoxCategorySearch.Name = "textBoxCategorySearch";
			this.textBoxCategorySearch.ReadOnly = true;
			this.textBoxCategorySearch.Size = new System.Drawing.Size(280, 23);
			this.textBoxCategorySearch.TabIndex = 2;
			this.textBoxCategorySearch.Text = "";
			this.textBoxCategorySearch.Click += new System.EventHandler(this.textBoxCategorySearch_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(392, 96);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(144, 23);
			this.label1.TabIndex = 3;
			this.label1.Text = "Категория поиска";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(392, 8);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(160, 23);
			this.label2.TabIndex = 4;
			this.label2.Text = "Группа трудоемкостей";
			// 
			// textBoxAutoType
			// 
			this.textBoxAutoType.Location = new System.Drawing.Point(392, 32);
			this.textBoxAutoType.Name = "textBoxAutoType";
			this.textBoxAutoType.ReadOnly = true;
			this.textBoxAutoType.Size = new System.Drawing.Size(280, 23);
			this.textBoxAutoType.TabIndex = 5;
			this.textBoxAutoType.Text = "";
			this.textBoxAutoType.Click += new System.EventHandler(this.textBoxAutoType_Click);
			// 
			// checkBoxShowCommon
			// 
			this.checkBoxShowCommon.Location = new System.Drawing.Point(392, 64);
			this.checkBoxShowCommon.Name = "checkBoxShowCommon";
			this.checkBoxShowCommon.Size = new System.Drawing.Size(224, 24);
			this.checkBoxShowCommon.TabIndex = 6;
			this.checkBoxShowCommon.Text = "Показывать общие работы";
			// 
			// buttonUpdate
			// 
			this.buttonUpdate.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.buttonUpdate.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonUpdate.Image")));
			this.buttonUpdate.Location = new System.Drawing.Point(856, 128);
			this.buttonUpdate.Name = "buttonUpdate";
			this.buttonUpdate.Size = new System.Drawing.Size(24, 23);
			this.buttonUpdate.TabIndex = 7;
			this.toolTip1.SetToolTip(this.buttonUpdate, "Обновление списка");
			this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
			// 
			// buttonToOuterList
			// 
			this.buttonToOuterList.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonToOuterList.Image")));
			this.buttonToOuterList.Location = new System.Drawing.Point(328, 0);
			this.buttonToOuterList.Name = "buttonToOuterList";
			this.buttonToOuterList.Size = new System.Drawing.Size(24, 23);
			this.buttonToOuterList.TabIndex = 8;
			this.toolTip1.SetToolTip(this.buttonToOuterList, "Выбранные работы в заказ");
			this.buttonToOuterList.Click += new System.EventHandler(this.buttonToOuterList_Click);
			// 
			// buttonNewWork
			// 
			this.buttonNewWork.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonNewWork.Image")));
			this.buttonNewWork.Location = new System.Drawing.Point(768, 104);
			this.buttonNewWork.Name = "buttonNewWork";
			this.buttonNewWork.Size = new System.Drawing.Size(24, 23);
			this.buttonNewWork.TabIndex = 9;
			this.buttonNewWork.Click += new System.EventHandler(this.buttonNewWork_Click);
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "Сумма";
			this.columnHeader6.Width = 90;
			// 
			// FormWorkSelect
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.ClientSize = new System.Drawing.Size(888, 357);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.buttonNewWork,
																		  this.buttonToOuterList,
																		  this.buttonUpdate,
																		  this.checkBoxShowCommon,
																		  this.textBoxAutoType,
																		  this.label2,
																		  this.label1,
																		  this.textBoxCategorySearch,
																		  this.treeView1,
																		  this.listView1});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.Name = "FormWorkSelect";
			this.Text = "Выбор трудоемкости";
			this.ResumeLayout(false);

		}
		#endregion

		protected void listView1_DoubleClick(object sender, EventArgs e)
		{
			// Определяем колонку
			// Добавляем новый элемент во внешний лист
			if(transferFunction == null) return;
			ListViewItem item	= Db.GetItemSelected(listView1);
			transferFunction(item);
		}

		protected void listView1_KeyDown(object sender, KeyEventArgs e)
		{
			switch(e.KeyCode)
			{
				case Keys.Enter:
				{
					// Добавляем новый элемент во внешний лист
					if(transferFunction == null) return;
					ListViewItem item	= Db.GetItemSelected(listView1);
					transferFunction(item);
				}
				break;
				case Keys.Up:
					break;
				case Keys.Down:
					break;
				default:
				{
					// Побуквенный поиск
					FormSelectionType3 dialog = Db.MakeFormSelectionType3(this, listView1.Items[0], 2, listView1);
					dialog.ShowDialog(null);
				}
				break;
			}
		}

		private void textBoxCategorySearch_Click(object sender, System.EventArgs e)
		{
			// Выбор категории поиска
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
			listView1.Items.Clear();
			DbWork.FillList1(listView1, autoTypeSelection, categorySearchSelection, checkBoxShowCommon.Checked, 0, "");
		}

		private void textBoxAutoType_Click(object sender, System.EventArgs e)
		{
			// Выбор группы трудоемкостей
			// Выбор категории поиска
			// Инициализируем форму выбора категории поиска
			ArrayList	autoType = new ArrayList();
			DbAutoType.FillArray(autoType);
			FormSelectionType1 dialog = Db.MakeFormSelectionType1(this, textBoxAutoType, autoType, textBoxAutoType.Text, true);
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			autoTypeSelection = (DbAutoType)dialog.SelectedElement;
			if(autoTypeSelection != null)
				textBoxAutoType.Text	= autoTypeSelection.DbTitle();
			else
				textBoxAutoType.Text = "";

			// Обновляем список
			listView1.Items.Clear();
			DbWork.FillList1(listView1, autoTypeSelection, categorySearchSelection, checkBoxShowCommon.Checked, 0, "");
		}

		private void treeView1_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			DbWorkGroup workGroup	= (DbWorkGroup)e.Node.Tag;
			if(workGroup == null) return;
			if(workGroup.FlagElement	== false) return;

			// Перезаполняием лист
			listView1.Items.Clear();
			DbWork.FillList2(listView1, autoTypeSelection, workGroup, 4);
			foreach(ListViewItem itm in listView1.Items)
			{
				itm.Selected	= true;
			}
		}

		protected void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
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

		protected void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
		{
			if (e.Column > 3) return;
			FormSelectString dialog = new FormSelectString("Запрос", "Введите строку для поиска");
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			string pattern = dialog.SelectedTextMask;
			listView1.Items.Clear();

			switch (e.Column)
			{
				case 0:
					DbWork.FillList1(listView1, autoTypeSelection, categorySearchSelection, checkBoxShowCommon.Checked, 1, pattern);
					break;
				case 1:
					DbWork.FillList1(listView1, autoTypeSelection, categorySearchSelection, checkBoxShowCommon.Checked, 3, pattern);
					break;
				case 2:
					DbWork.FillList1(listView1, autoTypeSelection, categorySearchSelection, checkBoxShowCommon.Checked, 5, pattern);
					break;
				case 3:
					DbWork.FillList1(listView1, autoTypeSelection, categorySearchSelection, checkBoxShowCommon.Checked, 2, pattern);
					break;
				default:
					break;
			}
		}

		private void buttonUpdate_Click(object sender, System.EventArgs e)
		{
			// Обновляем список
			listView1.Items.Clear();
			DbWork.FillList1(listView1, autoTypeSelection, categorySearchSelection, checkBoxShowCommon.Checked, 0, "");
		}

		private void buttonToOuterList_Click(object sender, System.EventArgs e)
		{
			// Пепенос выбранных работ в заказ-наряд
			if(transferFunction == null) return;

			foreach(ListViewItem itm in listView1.SelectedItems)
			{
				transferFunction(itm);
				itm.Selected	= false;
			}
		}

		private void buttonNewWork_Click(object sender, System.EventArgs e)
		{
		
		}

	}
}
