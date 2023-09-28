using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormManageAutoWork.
	/// </summary>
	public class FormManageAutoWork : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView listViewAutoType;
		private System.Windows.Forms.Button buttonUpdateList;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ListView listViewDirectoryWork;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.TextBox textBoxCategorySearch;
		private System.Windows.Forms.Label label1;

		private int	listType						= 0;
		DbCategorySearch categorySearchSelection	= null;
		DbAutoType	autoTypeSelection				= null;

		public FormManageAutoWork()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			// Заполняем лист типов автомобилей
			DbAutoType.FillList(listViewAutoType);
			listViewAutoType.Items.Insert(0, "ОБЩИЕ РАБОТЫ");
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormManageAutoWork));
			this.listViewDirectoryWork = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.listViewAutoType = new System.Windows.Forms.ListView();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.buttonUpdateList = new System.Windows.Forms.Button();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
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
																									this.columnHeader1,
																									this.columnHeader2,
																									this.columnHeader3});
			this.listViewDirectoryWork.FullRowSelect = true;
			this.listViewDirectoryWork.Location = new System.Drawing.Point(8, 168);
			this.listViewDirectoryWork.Name = "listViewDirectoryWork";
			this.listViewDirectoryWork.Size = new System.Drawing.Size(720, 208);
			this.listViewDirectoryWork.TabIndex = 0;
			this.listViewDirectoryWork.View = System.Windows.Forms.View.Details;
			this.listViewDirectoryWork.ColumnClick	+= new ColumnClickEventHandler(this.listViewDirectoryWork_ColumnClick);
			this.listViewDirectoryWork.DoubleClick	+= new EventHandler(this.listViewDirectoryWork_DoubleClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Работа";
			this.columnHeader1.Width = 240;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Трудоемкость";
			this.columnHeader2.Width = 260;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Н/Ч";
			this.columnHeader3.Width = 120;
			// 
			// listViewAutoType
			// 
			this.listViewAutoType.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																							   this.columnHeader4});
			this.listViewAutoType.FullRowSelect = true;
			this.listViewAutoType.HideSelection = false;
			this.listViewAutoType.Location = new System.Drawing.Point(8, 8);
			this.listViewAutoType.Name = "listViewAutoType";
			this.listViewAutoType.Size = new System.Drawing.Size(368, 152);
			this.listViewAutoType.TabIndex = 1;
			this.listViewAutoType.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Группа трудоемкостей";
			this.columnHeader4.Width = 300;
			// 
			// buttonUpdateList
			// 
			this.buttonUpdateList.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.buttonUpdateList.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonUpdateList.Image")));
			this.buttonUpdateList.Location = new System.Drawing.Point(704, 144);
			this.buttonUpdateList.Name = "buttonUpdateList";
			this.buttonUpdateList.Size = new System.Drawing.Size(24, 23);
			this.buttonUpdateList.TabIndex = 2;
			this.toolTip1.SetToolTip(this.buttonUpdateList, "Обновить окно списка работ");
			this.buttonUpdateList.Click += new System.EventHandler(this.buttonUpdateList_Click);
			// 
			// textBoxCategorySearch
			// 
			this.textBoxCategorySearch.Location = new System.Drawing.Point(384, 32);
			this.textBoxCategorySearch.Name = "textBoxCategorySearch";
			this.textBoxCategorySearch.ReadOnly = true;
			this.textBoxCategorySearch.Size = new System.Drawing.Size(344, 23);
			this.textBoxCategorySearch.TabIndex = 3;
			this.textBoxCategorySearch.Text = "";
			this.textBoxCategorySearch.Click += new System.EventHandler(this.textBoxCategorySearch_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(384, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(136, 23);
			this.label1.TabIndex = 4;
			this.label1.Text = "Категория поиска";
			// 
			// FormManageAutoWork
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.ClientSize = new System.Drawing.Size(736, 389);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.label1,
																		  this.textBoxCategorySearch,
																		  this.buttonUpdateList,
																		  this.listViewAutoType,
																		  this.listViewDirectoryWork});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.Name = "FormManageAutoWork";
			this.Text = "Управление трудоемкостями";
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonUpdateList_Click(object sender, System.EventArgs e)
		{
			// Обновляем список
			autoTypeSelection		= null;
			listViewDirectoryWork.Items.Clear();

			if(listViewAutoType.SelectedItems.Count <= 1)
			{
				listType = 1;
				// Прдготовка листа
				listViewDirectoryWork.Columns.Clear();
				listViewDirectoryWork.Columns.Add("Наименование работы", 260, System.Windows.Forms.HorizontalAlignment.Left);
				listViewDirectoryWork.Columns.Add("Трудоемкость", 260, System.Windows.Forms.HorizontalAlignment.Left);
				listViewDirectoryWork.Columns.Add("Н/Ч", 120, System.Windows.Forms.HorizontalAlignment.Center);
				// Подробное описание одного выбранного элемента
				ListViewItem item	= Db.GetItemSelected(listViewAutoType);
				if(item	== null) return;
				DbAutoType	tmpAutoTypeSelection = (DbAutoType)item.Tag;
				if(categorySearchSelection != null)
					DbDirectoryWork.FillList(listViewDirectoryWork, tmpAutoTypeSelection, "", categorySearchSelection.Code);
				else
					DbDirectoryWork.FillList(listViewDirectoryWork, tmpAutoTypeSelection, "", 0);

				if(autoTypeSelection != null)
					this.Text	= "Управление трудоемкостями - " + tmpAutoTypeSelection.Name;
				else
					this.Text	= "Управление трудоемкостями - общие работы";

				// Дополнительная обработка листа, для красоты
				DbDirectoryWork last_dw = null;
				foreach(ListViewItem itm in listViewDirectoryWork.Items)
				{
					DbDirectoryWork dw = (DbDirectoryWork)itm.Tag;
					if(dw != null)
					{
						if(last_dw != null && last_dw.Code == dw.Code)
						{
							dw.LevelUp();
							dw.SetLVItem(itm);
						}
						last_dw	= dw;
					}
				}

				autoTypeSelection = tmpAutoTypeSelection;
				return;
			}

			listType	= 2;
			// Построение таблицы
			ArrayList directoryWork	= new ArrayList();
			DbDirectoryWork.FillArray2(directoryWork, categorySearchSelection, "");
			int count = 0;
			foreach(ListViewItem item in listViewAutoType.SelectedItems)
			{
				DbAutoType at = (DbAutoType)item.Tag;
				count++;
			}
			// Прдготовка листа
			listViewDirectoryWork.Columns.Add("Наименование работы", 260, System.Windows.Forms.HorizontalAlignment.Left);
			foreach(ListViewItem item in listViewAutoType.SelectedItems)
			{
				DbAutoType at = (DbAutoType)item.Tag;
				if(at != null)
					listViewDirectoryWork.Columns.Add(at.Name, 120, System.Windows.Forms.HorizontalAlignment.Center);
				else
					listViewDirectoryWork.Columns.Add("ОБЩИЕ РАБОТЫ", 120, System.Windows.Forms.HorizontalAlignment.Center);
			}
			// Дозаполнение соновного списка
			int i;
			foreach(object o in directoryWork)
			{
				DbDirectoryWork dw = (DbDirectoryWork)o;
				i = 0;
				foreach(ListViewItem item in listViewAutoType.SelectedItems)
				{
					DbAutoType at = (DbAutoType)item.Tag;	
					dw.WorkArray.Add(new ArrayList());
					DbWork.FillArray((ArrayList)(dw.WorkArray[i]), at, dw);
					i++;
				}
			}
			// Обработка списка c заполнением листа
			listViewDirectoryWork.Items.Clear();
			foreach(object o in directoryWork)
			{
				DbDirectoryWork dw = (DbDirectoryWork)o;
				int maxCount = 0;
				foreach(object o1 in dw.WorkArray)
				{
					if(maxCount < ((ArrayList)o1).Count) maxCount = ((ArrayList)o1).Count;
				}
				listViewDirectoryWork.Items.Add(dw.LVItem);
				for(int j=1; j < maxCount; j++)
				{
					DbDirectoryWork dw1 = new DbDirectoryWork(dw, j);
					listViewDirectoryWork.Items.Add(dw1.LVItem);
				}
			}
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
		}

		private void listViewDirectoryWork_ColumnClick(object sender, ColumnClickEventArgs e)
		{
			// Запрос поисковой строки
			FormSelectString dialog = new FormSelectString("Наименование работы для поиска", "");
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			string pattern = dialog.SelectedTextMask;

			// Обновляем список
			autoTypeSelection		= null;
			listViewDirectoryWork.Items.Clear();

			if(listViewAutoType.SelectedItems.Count <= 1)
			{
				listType = 1;
				// Прдготовка листа
				listViewDirectoryWork.Columns.Clear();
				listViewDirectoryWork.Columns.Add("Наименование работы", 260, System.Windows.Forms.HorizontalAlignment.Left);
				listViewDirectoryWork.Columns.Add("Трудоемкость", 260, System.Windows.Forms.HorizontalAlignment.Left);
				listViewDirectoryWork.Columns.Add("Н/Ч", 120, System.Windows.Forms.HorizontalAlignment.Center);
				// Подробное описание одного выбранного элемента
				ListViewItem item	= Db.GetItemSelected(listViewAutoType);
				if(item	== null) return;
				DbAutoType	tmpAutoTypeSelection = (DbAutoType)item.Tag;
				if(categorySearchSelection != null)
					DbDirectoryWork.FillList(listViewDirectoryWork, tmpAutoTypeSelection, pattern, categorySearchSelection.Code);
				else
					DbDirectoryWork.FillList(listViewDirectoryWork, tmpAutoTypeSelection, pattern, 0);

				if(autoTypeSelection != null)
					this.Text	= "Управление трудоемкостями - " + tmpAutoTypeSelection.Name;
				else
					this.Text	= "Управление трудоемкостями - общие работы";

				// Дополнительная обработка листа, для красоты
				DbDirectoryWork last_dw = null;
				foreach(ListViewItem itm in listViewDirectoryWork.Items)
				{
					DbDirectoryWork dw = (DbDirectoryWork)itm.Tag;
					if(dw != null)
					{
						if(last_dw != null && last_dw.Code == dw.Code)
						{
							dw.LevelUp();
							dw.SetLVItem(itm);
						}
						last_dw	= dw;
					}
				}

				autoTypeSelection = tmpAutoTypeSelection;
				return;
			}

			listType	= 2;
			// Построение таблицы
			ArrayList directoryWork	= new ArrayList();
			DbDirectoryWork.FillArray2(directoryWork, categorySearchSelection, pattern);
			int count = 0;
			foreach(ListViewItem item in listViewAutoType.SelectedItems)
			{
				DbAutoType at = (DbAutoType)item.Tag;
				count++;
			}
			// Прдготовка листа
			listViewDirectoryWork.Columns.Clear();
			listViewDirectoryWork.Columns.Add("Наименование работы", 260, System.Windows.Forms.HorizontalAlignment.Left);
			foreach(ListViewItem item in listViewAutoType.SelectedItems)
			{
				DbAutoType at = (DbAutoType)item.Tag;
				if(at != null)
					listViewDirectoryWork.Columns.Add(at.Name, 120, System.Windows.Forms.HorizontalAlignment.Center);
				else
					listViewDirectoryWork.Columns.Add("ОБЩИЕ РАБОТЫ", 120, System.Windows.Forms.HorizontalAlignment.Center);
			}
			// Дозаполнение соновного списка
			int i;
			foreach(object o in directoryWork)
			{
				DbDirectoryWork dw = (DbDirectoryWork)o;
				i = 0;
				foreach(ListViewItem item in listViewAutoType.SelectedItems)
				{
					DbAutoType at = (DbAutoType)item.Tag;
					dw.WorkArray.Add(new ArrayList());
					DbWork.FillArray((ArrayList)(dw.WorkArray[i]), at, dw);
					i++;
					
				}
			}
			// Обработка списка c заполнением листа
			foreach(object o in directoryWork)
			{
				DbDirectoryWork dw = (DbDirectoryWork)o;
				int maxCount = 0;
				foreach(object o1 in dw.WorkArray)
				{
					if(maxCount < ((ArrayList)o1).Count) maxCount = ((ArrayList)o1).Count;
				}
				listViewDirectoryWork.Items.Add(dw.LVItem);
				for(int j=1; j < maxCount; j++)
				{
					DbDirectoryWork dw1 = new DbDirectoryWork(dw, j);
					listViewDirectoryWork.Items.Add(dw1.LVItem);
				}
			}
		}

		private void listViewDirectoryWork_DoubleClick(object sender, EventArgs e)
		{
			if(listType != 1) return;

			// Процедура изменения/добавления трудоемкости
			ListViewItem item = Db.GetItemPosition(listViewDirectoryWork);
			if(item == null) return;
			int column = Db.GetColumnPosition(listViewDirectoryWork);
			if(column != 1 && column != 0) return;
			DbDirectoryWork dw = (DbDirectoryWork)item.Tag;
			if(dw == null) return;
			DbWork wk = dw.Work;
			if(wk == null || wk.Code == 0 || column == 0)
			{
				// Новая трудоемкость
				FormWork dialog = new FormWork(autoTypeSelection, null, dw);
				dialog.ShowDialog(this);
				if(dialog.DialogResult == DialogResult.OK)
				{
					dw.Work = dialog.Work;
					dw.SetLVItem(item);
				}
			}
			else
			{
				// Изменение трудоемкости
				FormWork dialog = new FormWork(null, wk, null);
				dialog.ShowDialog(this);
				if(dialog.DialogResult == DialogResult.OK)
				{
					dw.Work = dialog.Work;
					dw.SetLVItem(item);
				}
			}
			
		}
	}
}
