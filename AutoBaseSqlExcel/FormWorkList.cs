using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormWorkList.
	/// </summary>
	public class FormWorkList : System.Windows.Forms.Form
	{
		// Элементы формы
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.TextBox textBoxSelect;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Button buttonUpdate;
		private System.Windows.Forms.Button buttonChange;	
		private System.Windows.Forms.Button buttonSetPrice;
		private System.Windows.Forms.Button buttonSetPriceGuaranty;
		private System.Windows.Forms.Button buttonSelect;
		
		private ListViewItem currentItem = null;
		private DbAutoType autoType = null;
		private ListView externalList = null;
		private System.Windows.Forms.Button buttonNew;
		private System.Windows.Forms.Button buttonReplace;
		private System.Windows.Forms.Button buttonRemove;
		private System.Windows.Forms.Button buttonUsage;
		private System.Windows.Forms.Button buttonNullAutoType;
		private System.Windows.Forms.ColumnHeader columnHeader8;
		private DbWork selectedWork = null;

		public FormWorkList(DbAutoType autoTypeStatic, ListView list)
		{
			InitializeComponent();

			toolTip1.SetToolTip(this.listView1, "");
			externalList = list;
			if(autoTypeStatic != null)
			{
				//buttonSelect.Visible = false;		// Нужно позволить выбирать общие (кузовные, окраска) работы
				autoType = autoTypeStatic;
				textBoxSelect.Text = autoType.Name;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormWorkList));
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
			this.textBoxSelect = new System.Windows.Forms.TextBox();
			this.buttonSelect = new System.Windows.Forms.Button();
			this.buttonUpdate = new System.Windows.Forms.Button();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.buttonChange = new System.Windows.Forms.Button();
			this.buttonSetPrice = new System.Windows.Forms.Button();
			this.buttonSetPriceGuaranty = new System.Windows.Forms.Button();
			this.buttonNew = new System.Windows.Forms.Button();
			this.buttonReplace = new System.Windows.Forms.Button();
			this.buttonRemove = new System.Windows.Forms.Button();
			this.buttonUsage = new System.Windows.Forms.Button();
			this.buttonNullAutoType = new System.Windows.Forms.Button();
			this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
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
																						this.columnHeader6,
																						this.columnHeader7,
																						this.columnHeader8});
			this.listView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.listView1.FullRowSelect = true;
			this.listView1.GridLines = true;
			this.listView1.Location = new System.Drawing.Point(0, 80);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(590, 240);
			this.listView1.TabIndex = 0;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
			this.listView1.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView1_ColumnClick);
			this.listView1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseMove);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "№ позиции";
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Код детали";
			this.columnHeader2.Width = 80;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Код работы";
			this.columnHeader3.Width = 40;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Наименование";
			this.columnHeader4.Width = 160;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "Трудоемкость";
			this.columnHeader5.Width = 40;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "Нормачас";
			// 
			// columnHeader7
			// 
			this.columnHeader7.Text = "";
			this.columnHeader7.Width = 20;
			// 
			// textBoxSelect
			// 
			this.textBoxSelect.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.textBoxSelect.Location = new System.Drawing.Point(0, 56);
			this.textBoxSelect.Name = "textBoxSelect";
			this.textBoxSelect.ReadOnly = true;
			this.textBoxSelect.Size = new System.Drawing.Size(384, 23);
			this.textBoxSelect.TabIndex = 1;
			this.textBoxSelect.Text = "Марка Автомобиля";
			this.toolTip1.SetToolTip(this.textBoxSelect, "Выбранная марка автомобиля");
			// 
			// buttonSelect
			// 
			this.buttonSelect.Location = new System.Drawing.Point(384, 56);
			this.buttonSelect.Name = "buttonSelect";
			this.buttonSelect.TabIndex = 2;
			this.buttonSelect.Text = "Выбрать";
			this.toolTip1.SetToolTip(this.buttonSelect, "Выбрать марку автомобиля");
			this.buttonSelect.Click += new System.EventHandler(this.buttonSelect_Click);
			// 
			// buttonUpdate
			// 
			this.buttonUpdate.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonUpdate.Image")));
			this.buttonUpdate.Name = "buttonUpdate";
			this.buttonUpdate.Size = new System.Drawing.Size(24, 24);
			this.buttonUpdate.TabIndex = 3;
			this.toolTip1.SetToolTip(this.buttonUpdate, "Обновить список");
			this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
			// 
			// toolTip1
			// 
			this.toolTip1.AutoPopDelay = 20000;
			this.toolTip1.InitialDelay = 100;
			this.toolTip1.ReshowDelay = 100;
			this.toolTip1.ShowAlways = true;
			// 
			// buttonChange
			// 
			this.buttonChange.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonChange.Image")));
			this.buttonChange.Location = new System.Drawing.Point(24, 0);
			this.buttonChange.Name = "buttonChange";
			this.buttonChange.Size = new System.Drawing.Size(24, 24);
			this.buttonChange.TabIndex = 4;
			this.toolTip1.SetToolTip(this.buttonChange, "Свойсва трудоемкости");
			this.buttonChange.Click += new System.EventHandler(this.buttonChange_Click);
			// 
			// buttonSetPrice
			// 
			this.buttonSetPrice.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonSetPrice.Image")));
			this.buttonSetPrice.Location = new System.Drawing.Point(48, 0);
			this.buttonSetPrice.Name = "buttonSetPrice";
			this.buttonSetPrice.Size = new System.Drawing.Size(24, 24);
			this.buttonSetPrice.TabIndex = 5;
			this.toolTip1.SetToolTip(this.buttonSetPrice, "Стоимость нормачаса");
			this.buttonSetPrice.Click += new System.EventHandler(this.buttonSetPrice_Click);
			// 
			// buttonSetPriceGuaranty
			// 
			this.buttonSetPriceGuaranty.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonSetPriceGuaranty.Image")));
			this.buttonSetPriceGuaranty.Location = new System.Drawing.Point(72, 0);
			this.buttonSetPriceGuaranty.Name = "buttonSetPriceGuaranty";
			this.buttonSetPriceGuaranty.Size = new System.Drawing.Size(24, 24);
			this.buttonSetPriceGuaranty.TabIndex = 6;
			this.toolTip1.SetToolTip(this.buttonSetPriceGuaranty, "Стоимость гарантийного нормачаса");
			this.buttonSetPriceGuaranty.Click += new System.EventHandler(this.buttonSetPriceGuaranty_Click);
			// 
			// buttonNew
			// 
			this.buttonNew.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonNew.Image")));
			this.buttonNew.Location = new System.Drawing.Point(96, 0);
			this.buttonNew.Name = "buttonNew";
			this.buttonNew.Size = new System.Drawing.Size(24, 24);
			this.buttonNew.TabIndex = 7;
			this.toolTip1.SetToolTip(this.buttonNew, "Новая трудоемкость");
			this.buttonNew.Click += new System.EventHandler(this.buttonNew_Click);
			// 
			// buttonReplace
			// 
			this.buttonReplace.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonReplace.Image")));
			this.buttonReplace.Location = new System.Drawing.Point(368, 0);
			this.buttonReplace.Name = "buttonReplace";
			this.buttonReplace.Size = new System.Drawing.Size(24, 24);
			this.buttonReplace.TabIndex = 8;
			this.toolTip1.SetToolTip(this.buttonReplace, "Замена трудоемкости на другую");
			this.buttonReplace.Click += new System.EventHandler(this.buttonReplace_Click);
			// 
			// buttonRemove
			// 
			this.buttonRemove.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonRemove.Image")));
			this.buttonRemove.Location = new System.Drawing.Point(392, 0);
			this.buttonRemove.Name = "buttonRemove";
			this.buttonRemove.Size = new System.Drawing.Size(24, 24);
			this.buttonRemove.TabIndex = 9;
			this.toolTip1.SetToolTip(this.buttonRemove, "Удаление трудоемкости");
			this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
			// 
			// buttonUsage
			// 
			this.buttonUsage.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonUsage.Image")));
			this.buttonUsage.Location = new System.Drawing.Point(280, 0);
			this.buttonUsage.Name = "buttonUsage";
			this.buttonUsage.Size = new System.Drawing.Size(24, 23);
			this.buttonUsage.TabIndex = 10;
			this.toolTip1.SetToolTip(this.buttonUsage, "Информация о присутствии выбранной работы в карточках");
			this.buttonUsage.Click += new System.EventHandler(this.buttonUsage_Click);
			// 
			// buttonNullAutoType
			// 
			this.buttonNullAutoType.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonNullAutoType.Image")));
			this.buttonNullAutoType.Location = new System.Drawing.Point(416, 0);
			this.buttonNullAutoType.Name = "buttonNullAutoType";
			this.buttonNullAutoType.Size = new System.Drawing.Size(24, 24);
			this.buttonNullAutoType.TabIndex = 11;
			this.toolTip1.SetToolTip(this.buttonNullAutoType, "Обнулить тип автомобиля выбранной работы");
			this.buttonNullAutoType.Click += new System.EventHandler(this.buttonNullAutoType_Click);
			// 
			// columnHeader8
			// 
			this.columnHeader8.Text = "Сумма";
			this.columnHeader8.Width = 90;
			// 
			// FormWorkList
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(592, 325);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.buttonNullAutoType,
																		  this.buttonUsage,
																		  this.buttonRemove,
																		  this.buttonReplace,
																		  this.buttonNew,
																		  this.buttonSetPriceGuaranty,
																		  this.buttonSetPrice,
																		  this.buttonChange,
																		  this.buttonUpdate,
																		  this.buttonSelect,
																		  this.textBoxSelect,
																		  this.listView1});
			this.MinimumSize = new System.Drawing.Size(500, 300);
			this.Name = "FormWorkList";
			this.Text = "Трудоемкости";
			this.Resize += new System.EventHandler(this.this_Resize);
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonSelect_Click(object sender, System.EventArgs e)
		{
			// Вызов диалога выбора марки автомобиля, с последующим выбором
			FormAutoTypeList dialog = new FormAutoTypeList();
			if(dialog.ShowDialog(this) != DialogResult.OK) return;
			autoType = dialog.SelectedAutoType;
			dialog.Dispose();
			textBoxSelect.Text = autoType.Name;
		}

		private void buttonUpdate_Click(object sender, System.EventArgs e)
		{
			// Обновить лист
			listView1.Items.Clear();
			//DbWork.FillList(listView1, autoType, null);
			DbWork.FillList(listView1, autoType, 0, "");
		}

		private void listView1_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
		{
			if(e.Column == 1)
			{
				// Щелчек на колонке с кодом детали
				FormSelectString dialog = new FormSelectString("Код детали", "Код детали для поиска");
				if(dialog.ShowDialog(this) != DialogResult.OK) return;
				string mask = dialog.SelectedTextMask;
				listView1.Items.Clear();
				//DbWork.FillListDetail(listView1, autoType, mask);
				DbWork.FillList(listView1, autoType, 1, mask);
			}
			if(e.Column == 3)
			{
				// Щелчек на колонке с наименованием
				FormSelectString dialog = new FormSelectString("Наименование", "Искомая часть наименования");
				if(dialog.ShowDialog(this) != DialogResult.OK) return;
				string mask = dialog.SelectedTextMask;
				listView1.Items.Clear();
				//DbWork.FillListName(listView1, autoType, mask);
				DbWork.FillList(listView1, autoType, 2, mask);
			}
		}

		private void this_Resize(object sender, System.EventArgs e)
		{
			listView1.Columns[3].Width = this.Width - 10 - 30 - listView1.Columns[0].Width
				- listView1.Columns[1].Width
				- listView1.Columns[2].Width
				- listView1.Columns[4].Width
				- listView1.Columns[5].Width
				- listView1.Columns[6].Width
				- listView1.Columns[7].Width;
		}

		private void listView1_MouseMove(object sender, MouseEventArgs e)
		{
			// Поддержка всплывающих подсказок
			Point pnt = new Point(e.X, e.Y);
			if(pnt.X > listView1.Columns[0].Width)
			{
				toolTip1.SetToolTip(listView1, "");
				currentItem = null;
				return;
			}
			ListViewItem item = listView1.GetItemAt(pnt.X, pnt.Y);
			if(item == null)
			{
				toolTip1.SetToolTip(listView1, "");
				currentItem = null;
				return;
			}
			DbWork work = (DbWork)item.Tag;
			if(work == null)
			{
				toolTip1.SetToolTip(listView1, "");
				currentItem = null;
				return;
			}
			if(currentItem != item)
			{	
				toolTip1.SetToolTip(listView1, work.ToolTipText);
				currentItem = item;
			}	
		}

		private void buttonChange_Click(object sender, System.EventArgs e)
		{
			// Делаем попытку изменить выбранный элемент
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			DbWork work = (DbWork)item.Tag;
			if(work == null) return;
			if(work.Exist != true) return;
			FormWork dialog = new FormWork(autoType, work, null);
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			RemoveExpand(item);
			dialog.Work.SetLVItem(item);
		}

		private void buttonNew_Click(object sender, System.EventArgs e)
		{
			// Добавить новый элемент
			if(autoType == null) return;
			FormWork dialog = new FormWork(autoType, null, null);
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			listView1.Items.Add(dialog.Work.LVItem);
		}

		private void listView1_DoubleClick(object sender, EventArgs e)
		{
			Point pnt = Cursor.Position;
			pnt = listView1.PointToClient(pnt);
			ListViewItem item = listView1.GetItemAt(pnt.X, pnt.Y);
			if(item == null) return;
			DbWork work = (DbWork)item.Tag;
			if(work == null) return;
			if(work.Exist != true) return;

			// Если щелкнули на + (последняя колонка - раскрытие списка сопутсвующих работ)
			if(pnt.X > listView1.Columns[0].Width +
				listView1.Columns[1].Width +
				listView1.Columns[2].Width +
				listView1.Columns[3].Width +
				listView1.Columns[4].Width)
			{
				if(work.Expand == false)
				{
					work.Expand = true;
					work.FillListLinksTmp(listView1, item.Index + 1);
				}
				else
				{
					RemoveExpand(item);
				}
				work.SetLVItem(item);

				return;
			}
			if(externalList != null)
			{
				// Если добавляем во внешний лист - добавляем как незаписанное в базу
				work.Exist = false;
				externalList.Items.Add(work.LVItem);
				listView1.Items.Remove(item);
			}
			else
			{
				selectedWork = work;
				this.DialogResult = DialogResult.OK;
				this.Close();
			}
		}

		private void buttonSetPrice_Click(object sender, System.EventArgs e)
		{
			// Установить значение нормачаса для всех выбранных элементов
			FormSelectString dialog = new FormSelectString("Значение нормачаса", "0.0");
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			float newPrice = dialog.SelectedFloat;
			if(newPrice < 0)
			{
				MessageBox.Show("Неверное значение стоимости");
				return;
			}
			foreach(ListViewItem item in listView1.SelectedItems)
			{
				DbWork work = (DbWork)item.Tag;
				if(work != null && work.Exist)
				{ 
					DbWork wrk = new DbWork(work);
					if(wrk.WritePrice(newPrice, wrk.PriceGuaranty))
					{
						wrk.SetLVItem(item);
					}
				}
			}
			Db.ShowFaults();
		}

		private void buttonSetPriceGuaranty_Click(object sender, System.EventArgs e)
		{
			// Установить значение гарантийного нормачаса
			FormSelectString dialog = new FormSelectString("Стоимость гарантийного нормачаса", "0.0");
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			float newPrice = dialog.SelectedFloat;
			if(newPrice < 0)
			{
				MessageBox.Show("Неверное значение стоимости");
				return;
			}
			foreach(ListViewItem item in listView1.SelectedItems)
			{
				DbWork work = (DbWork)item.Tag;
				if(work != null && work.Exist)
				{ 
					DbWork wrk = new DbWork(work);
					if(wrk.WritePrice(wrk.Price, newPrice))
					{
						wrk.SetLVItem(item);
					}
				}
			}
			Db.ShowFaults();
		}

		public DbWork SelectedWork
		{
			get
			{
				return selectedWork;
			}
		}

		// Убираем раскрытие заданного элемента
		public void RemoveExpand(ListViewItem item)
		{
			ListView list = item.ListView;
			if(list == null) return;
			if(item == null) return;
			DbWork work = (DbWork)item.Tag;
			if(work == null) return;
			if(work.Expand != true) return;
			work.Expand = false;
			int i = item.Index + 1;
			bool flag = true;
			if(i >= list.Items.Count) flag = false;
			while(flag)
			{
				DbWork wrk = (DbWork)list.Items[i].Tag;
				if(wrk == null) flag = false;
				if(wrk.Exist)
				{
					flag = false;
				}
				else
				{
					list.Items.RemoveAt(i);
					i--;
				}
				i++;
				if(i >= list.Items.Count) flag = false;
			}
		}

		private void buttonReplace_Click(object sender, System.EventArgs e)
		{
			// Процедура замены одной трудоемкости на другую
			// Админовская привилегия
			//ListViewItem item = Db.GetItemSelected(listView1);
			//if(item == null) return;
			//DbWork work = (DbWork)item.Tag;
			//if(work == null) return;

			// Запрос новой работы
			//FormWorkList dialog = new FormWorkList(this.autoType, null);
			//dialog.ShowDialog();
			//if(dialog.DialogResult != DialogResult.OK) return;
			//work.Replace(dialog.SelectedWork);
			
			// Вариант группового изменения
			// Запрос новой работы
			FormWorkList dialog = new FormWorkList(this.autoType, null);
			dialog.ShowDialog();
			if(dialog.DialogResult != DialogResult.OK) return;
			string question = "Вы уверены что хотите заменить выбранные работы на " + dialog.SelectedWork.Name + "?";
			if(Db.CheckSysAction(question) == false) return;

			foreach(ListViewItem item in listView1.SelectedItems)
			{
				DbWork work = (DbWork)item.Tag;
				if(work != null)
				{
					work.Replace(dialog.SelectedWork);
				}
			}
		}

		private void buttonRemove_Click(object sender, System.EventArgs e)
		{
			// Процедура удаления трудоемкости
			// Админовская привилегия
			//ListViewItem item = Db.GetItemSelected(listView1);
			//if(item == null) return;
			//DbWork work = (DbWork)item.Tag;
			//if(work == null) return;
			//if(!work.Delete()) return;
			//listView1.Items.Remove(item);

			// Вариант группового удаления
			if(Db.CheckSysAction("Вы уверены что хотите удалить трудоемкость?") == false) return;

			foreach(ListViewItem item in listView1.SelectedItems)
			{
				DbWork work = (DbWork)item.Tag;
				if(work != null)
				{
					if(work.Delete())
						listView1.Items.Remove(item);
				}
			}
		}

		private void buttonUsage_Click(object sender, System.EventArgs e)
		{
			// Информация о использовании выбранной работы в карточках заказов
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			DbWork work = (DbWork)item.Tag;
			if(work == null) return;

			// Проверка на похожие данные
			ArrayList cardWorks = new ArrayList();
			DbCardWork.FindWork(cardWorks, work);
			if(cardWorks.Count != 0)
			{
				// Вывод списка похожих и запрос на добавление нового
				// Запрос на добавление
				FormInfo dialog = new FormInfo(null, 0);
				foreach(object o in cardWorks)
				{
					DbCardWork element = (DbCardWork)o;
					dialog.InsertInfo(element);
				}
				dialog.ShowDialog();
			}
			else
				MessageBox.Show("Работа в карточках не присутствует");
		}

		private void buttonNullAutoType_Click(object sender, System.EventArgs e)
		{
			// Обнуление типа вытомобиля для выбранной работы
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			DbWork work = (DbWork)item.Tag;
			if(work == null) return;

			if(work.WriteNullAutoType() == false) return;
			work.SetLVItem(item);
		}
	}
}
