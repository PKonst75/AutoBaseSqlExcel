using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormDetailStorageList.
	/// </summary>
	public class FormDetailStorageList : System.Windows.Forms.Form
	{
		public enum Type:short{Card=5};
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.Button buttonNew;
		private System.Windows.Forms.Button buttonUpdate;
		private System.ComponentModel.IContainer components;

		private DbDetailStorage detailStorage;
		private DbCardDetail cardDetail;
		private DbCard card;
		private ListView outerList;
		private System.Windows.Forms.Button buttonDetailStorageCheck;
		private System.Windows.Forms.Button buttonReportMove;
		private System.Windows.Forms.Button buttonChange;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.Button buttonNewPrice;
		private System.Windows.Forms.ToolTip toolTip1;
		private int outerType;

		private DbDetailOutcom selectedDetailOutcom;
		private System.Windows.Forms.Button buttonSetPrice;
		private System.Windows.Forms.Button buttonDelete;
		private System.Windows.Forms.Button buttonCheckIncomExpens;
		private System.Windows.Forms.Button buttonChangeDetail;
		private System.Windows.Forms.Button buttonTurnOff;
		private System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.CheckBox checkBox_null_quontity;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private DbDetail detail;

		public FormDetailStorageList(ListView list, int type, DbDetail detailSrc, DbCard cardSrc)
		{
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			outerList = list;
			outerType = type;
			detail		= detailSrc;
			card		= cardSrc;

			// Первоначальное заполнение по детали
			if(detailSrc != null)
			{
				DbDetailStorage.FillList(listView1, detail);
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormDetailStorageList));
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.buttonNew = new System.Windows.Forms.Button();
			this.buttonUpdate = new System.Windows.Forms.Button();
			this.buttonDetailStorageCheck = new System.Windows.Forms.Button();
			this.buttonReportMove = new System.Windows.Forms.Button();
			this.buttonChange = new System.Windows.Forms.Button();
			this.buttonNewPrice = new System.Windows.Forms.Button();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.buttonDelete = new System.Windows.Forms.Button();
			this.buttonCheckIncomExpens = new System.Windows.Forms.Button();
			this.buttonChangeDetail = new System.Windows.Forms.Button();
			this.buttonTurnOff = new System.Windows.Forms.Button();
			this.checkBox_null_quontity = new System.Windows.Forms.CheckBox();
			this.buttonSetPrice = new System.Windows.Forms.Button();
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
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
																						this.columnHeader4,
																						this.columnHeader5,
																						this.columnHeader6,
																						this.columnHeader3});
			this.listView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.listView1.FullRowSelect = true;
			this.listView1.GridLines = true;
			this.listView1.Location = new System.Drawing.Point(8, 64);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(768, 200);
			this.listView1.TabIndex = 0;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listView1_KeyDown);
			this.listView1.Resize += new System.EventHandler(this.listView1_Resize);
			this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
			this.listView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseUp);
			this.listView1.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView1_ColumnClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Код";
			this.columnHeader1.Width = 120;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Наименование";
			this.columnHeader2.Width = 200;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Количество";
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "Ед.Изм.";
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "Цена";
			this.columnHeader6.Width = 80;
			// 
			// buttonNew
			// 
			this.buttonNew.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonNew.Image")));
			this.buttonNew.Location = new System.Drawing.Point(8, 0);
			this.buttonNew.Name = "buttonNew";
			this.buttonNew.Size = new System.Drawing.Size(24, 23);
			this.buttonNew.TabIndex = 1;
			this.toolTip1.SetToolTip(this.buttonNew, "Новая позиция");
			this.buttonNew.Click += new System.EventHandler(this.buttonNew_Click);
			// 
			// buttonUpdate
			// 
			this.buttonUpdate.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonUpdate.Image")));
			this.buttonUpdate.Location = new System.Drawing.Point(32, 0);
			this.buttonUpdate.Name = "buttonUpdate";
			this.buttonUpdate.Size = new System.Drawing.Size(24, 23);
			this.buttonUpdate.TabIndex = 2;
			this.toolTip1.SetToolTip(this.buttonUpdate, "Обновить список");
			this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
			// 
			// buttonDetailStorageCheck
			// 
			this.buttonDetailStorageCheck.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonDetailStorageCheck.Image")));
			this.buttonDetailStorageCheck.Location = new System.Drawing.Point(520, 0);
			this.buttonDetailStorageCheck.Name = "buttonDetailStorageCheck";
			this.buttonDetailStorageCheck.Size = new System.Drawing.Size(24, 23);
			this.buttonDetailStorageCheck.TabIndex = 3;
			this.toolTip1.SetToolTip(this.buttonDetailStorageCheck, "Проверка складских остатков");
			this.buttonDetailStorageCheck.Click += new System.EventHandler(this.buttonDetailStorageCheck_Click);
			// 
			// buttonReportMove
			// 
			this.buttonReportMove.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonReportMove.Image")));
			this.buttonReportMove.Location = new System.Drawing.Point(272, 0);
			this.buttonReportMove.Name = "buttonReportMove";
			this.buttonReportMove.Size = new System.Drawing.Size(24, 23);
			this.buttonReportMove.TabIndex = 4;
			this.toolTip1.SetToolTip(this.buttonReportMove, "Просмотр движения товаров по складу");
			this.buttonReportMove.Click += new System.EventHandler(this.buttonReportMove_Click);
			// 
			// buttonChange
			// 
			this.buttonChange.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonChange.Image")));
			this.buttonChange.Location = new System.Drawing.Point(56, 0);
			this.buttonChange.Name = "buttonChange";
			this.buttonChange.Size = new System.Drawing.Size(24, 23);
			this.buttonChange.TabIndex = 5;
			this.toolTip1.SetToolTip(this.buttonChange, "Редактировать позицию");
			this.buttonChange.Click += new System.EventHandler(this.buttonChange_Click);
			// 
			// buttonNewPrice
			// 
			this.buttonNewPrice.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonNewPrice.Image")));
			this.buttonNewPrice.Location = new System.Drawing.Point(224, 0);
			this.buttonNewPrice.Name = "buttonNewPrice";
			this.buttonNewPrice.Size = new System.Drawing.Size(24, 23);
			this.buttonNewPrice.TabIndex = 6;
			this.toolTip1.SetToolTip(this.buttonNewPrice, "Установка автоматических цен");
			this.buttonNewPrice.Click += new System.EventHandler(this.buttonNewPrice_Click);
			// 
			// buttonDelete
			// 
			this.buttonDelete.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonDelete.Image")));
			this.buttonDelete.Location = new System.Drawing.Point(424, 0);
			this.buttonDelete.Name = "buttonDelete";
			this.buttonDelete.Size = new System.Drawing.Size(24, 23);
			this.buttonDelete.TabIndex = 8;
			this.toolTip1.SetToolTip(this.buttonDelete, "Удалить складскую позицию");
			this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
			// 
			// buttonCheckIncomExpens
			// 
			this.buttonCheckIncomExpens.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonCheckIncomExpens.Image")));
			this.buttonCheckIncomExpens.Location = new System.Drawing.Point(544, 0);
			this.buttonCheckIncomExpens.Name = "buttonCheckIncomExpens";
			this.buttonCheckIncomExpens.Size = new System.Drawing.Size(24, 23);
			this.buttonCheckIncomExpens.TabIndex = 9;
			this.toolTip1.SetToolTip(this.buttonCheckIncomExpens, "Проверка расходов по приходам");
			this.buttonCheckIncomExpens.Click += new System.EventHandler(this.buttonCheckIncomExpens_Click);
			// 
			// buttonChangeDetail
			// 
			this.buttonChangeDetail.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonChangeDetail.Image")));
			this.buttonChangeDetail.Location = new System.Drawing.Point(400, 0);
			this.buttonChangeDetail.Name = "buttonChangeDetail";
			this.buttonChangeDetail.Size = new System.Drawing.Size(24, 23);
			this.buttonChangeDetail.TabIndex = 10;
			this.toolTip1.SetToolTip(this.buttonChangeDetail, "Смена исходной детали");
			this.buttonChangeDetail.Click += new System.EventHandler(this.buttonChangeDetail_Click);
			// 
			// buttonTurnOff
			// 
			this.buttonTurnOff.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonTurnOff.Image")));
			this.buttonTurnOff.Location = new System.Drawing.Point(448, 0);
			this.buttonTurnOff.Name = "buttonTurnOff";
			this.buttonTurnOff.Size = new System.Drawing.Size(24, 23);
			this.buttonTurnOff.TabIndex = 11;
			this.toolTip1.SetToolTip(this.buttonTurnOff, "Вкл/Выкл складскую позицию");
			this.buttonTurnOff.Click += new System.EventHandler(this.buttonTurnOff_Click);
			// 
			// checkBox_null_quontity
			// 
			this.checkBox_null_quontity.Location = new System.Drawing.Point(8, 40);
			this.checkBox_null_quontity.Name = "checkBox_null_quontity";
			this.checkBox_null_quontity.Size = new System.Drawing.Size(312, 24);
			this.checkBox_null_quontity.TabIndex = 12;
			this.checkBox_null_quontity.Text = "Не показывать детали отсутствующие на складе";
			this.toolTip1.SetToolTip(this.checkBox_null_quontity, "Не показывать позиции с нулевым или отрицательным остатком");
			// 
			// buttonSetPrice
			// 
			this.buttonSetPrice.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonSetPrice.Image")));
			this.buttonSetPrice.Location = new System.Drawing.Point(336, 0);
			this.buttonSetPrice.Name = "buttonSetPrice";
			this.buttonSetPrice.Size = new System.Drawing.Size(24, 23);
			this.buttonSetPrice.TabIndex = 7;
			this.buttonSetPrice.Click += new System.EventHandler(this.buttonSetPrice_Click);
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
																					  this.menuItem3});
			this.menuItem1.Text = "Деталь";
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 0;
			this.menuItem2.Text = "Подать заявку";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 1;
			this.menuItem3.Text = "Список заказ-нарядов";
			this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Применимость";
			this.columnHeader3.Width = 200;
			// 
			// FormDetailStorageList
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(784, 273);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.checkBox_null_quontity,
																		  this.buttonTurnOff,
																		  this.buttonChangeDetail,
																		  this.buttonCheckIncomExpens,
																		  this.buttonDelete,
																		  this.buttonSetPrice,
																		  this.buttonNewPrice,
																		  this.buttonChange,
																		  this.buttonReportMove,
																		  this.buttonDetailStorageCheck,
																		  this.buttonUpdate,
																		  this.buttonNew,
																		  this.listView1});
			this.Name = "FormDetailStorageList";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Склад деталей";
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonNew_Click(object sender, System.EventArgs e)
		{
			// Добавление новой позиции на склад
			FormDetailStorage dialog = new FormDetailStorage(null);
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			listView1.Items.Add(dialog.DetailStorage.LVItem);
		}

		private void buttonUpdate_Click(object sender, System.EventArgs e)
		{
			// Обновляем список
			listView1.Items.Clear();
			// Проверяем, нужно ли отображать нулевые детали
			if(checkBox_null_quontity.Checked)
				DbDetailStorage.FillList(listView1, 4, "");
			else
				DbDetailStorage.FillList(listView1, 0, "");
		}

		public DbDetailStorage DetailStorage
		{
			get
			{
				return detailStorage;
			}
		}

		private void listView1_DoubleClick(object sender, EventArgs e)
		{
			ListViewItem item = Db.GetItemPosition(listView1);
			if(item == null) return;
			DbDetailStorage tmpDetailStorage = (DbDetailStorage)item.Tag;
			if(tmpDetailStorage == null) return;
			
			switch(outerType)
			{
				case 1:
					if(outerList == null)
					{
						detailStorage = tmpDetailStorage;
						this.DialogResult = DialogResult.OK;
						this.Close();
						return;
					}
					DbDetailIncom detailIncom = new DbDetailIncom(tmpDetailStorage);
					outerList.Items.Add(detailIncom.LVItem);
					listView1.Items.Remove(item);
					return;
				case 2:
					// Выбор элемента для записи в требование
					FormDetailIncomListDetailed dialog = new FormDetailIncomListDetailed(tmpDetailStorage, outerList);
					dialog.ShowDialog(this);
					if(dialog.DialogResult != DialogResult.OK)
					{
						// Мы ничего не выбрали в приходе
						if(outerList != null)
						{
							DbDetailOutcom	detailOutcom = new DbDetailOutcom(tmpDetailStorage);
							outerList.Items.Add(detailOutcom.LVItem);
							this.DialogResult = DialogResult.OK;
							this.Close();
						}
						return;
					}
					if(outerList == null)
					{
						if(dialog.DialogResult != DialogResult.OK)
						{
							return;
						}
						selectedDetailOutcom = dialog.SelectedDeteilOutcom;
						this.DialogResult = DialogResult.OK;
						this.Close();
						return;
					}
					return;
				case 3:
					if(outerList == null)
					{
						detailStorage = tmpDetailStorage;
						this.DialogResult = DialogResult.OK;
						this.Close();
						return;
					}
					DbAccountDetailItem element = new DbAccountDetailItem(tmpDetailStorage);
					outerList.Items.Add(element.LVItem);
					listView1.Items.Remove(item);
					return;
				case 4:
					if(card == null) return;
					if(outerList == null)
					{
						cardDetail		= new DbCardDetail(card, tmpDetailStorage);
						this.DialogResult = DialogResult.OK;
						this.Close();
						return;
					}
					DbCardDetail cardDetailTmp = new DbCardDetail(card, tmpDetailStorage);
					outerList.Items.Add(cardDetailTmp.LVItem);
					return;
			}
			if(outerList == null)
			{
				if(tmpDetailStorage == null) return;
				FormDetailStorage dialog = new FormDetailStorage(tmpDetailStorage);
				dialog.ShowDialog(this);
				if(dialog.DialogResult != DialogResult.OK) return;
				dialog.DetailStorage.SetLVItem(item);
				return;
			}
		}

		private void listView1_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Enter)
			{
				ListViewItem item = Db.GetItemSelected(listView1);
				if(item == null) return;
				DbDetailStorage tmpDetailStorage = (DbDetailStorage)item.Tag;
				if(tmpDetailStorage == null) return;
				if(outerList == null)
				{
					detailStorage = tmpDetailStorage;
					this.DialogResult = DialogResult.OK;
					this.Close();
					return;
				}
				switch(outerType)
				{
					case 1:
						DbDetailIncom detailIncom = new DbDetailIncom(tmpDetailStorage);
						outerList.Items.Add(detailIncom.LVItem);
						listView1.Items.Remove(item);
						return;
					case 3:
						DbAccountDetailItem element = new DbAccountDetailItem(tmpDetailStorage);
						outerList.Items.Add(element.LVItem);
						listView1.Items.Remove(item);
						return;
					case 4:
						if(outerList == null) return;
						if(card == null) return;
						foreach(ListViewItem itemLV in listView1.SelectedItems)
						{
							DbDetailStorage tmpDetailStorage1 = (DbDetailStorage)itemLV.Tag;
							if(tmpDetailStorage1 != null)
							{
								DbCardDetail cardDetailTmp = new DbCardDetail(card, tmpDetailStorage1);
								outerList.Items.Add(cardDetailTmp.LVItem);
							}
						}
						return;
				}
			}
		}

		private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
		{
			string mask = "";
			FormSelectString dialog = null;
			switch(e.Column)
			{
				case 0:
					// Щелчек на колонке с кодом детали
					dialog = new FormSelectString("Код детали", "Код детали для поиска");
					if(dialog.ShowDialog(this) != DialogResult.OK) return;
					mask = dialog.SelectedTextMask;
					listView1.Items.Clear();
					if(checkBox_null_quontity.Checked)
						DbDetailStorage.FillList(listView1, 5, mask);
					else
						DbDetailStorage.FillList(listView1, 1, mask);
					return;
				case 1:
					dialog = new FormSelectString("Наименование детали", "Наименование детали для поиска");
					if(dialog.ShowDialog(this) != DialogResult.OK) return;
					mask = dialog.SelectedTextMask;
					listView1.Items.Clear();
					if(checkBox_null_quontity.Checked)
						DbDetailStorage.FillList(listView1, 6, mask);
					else
						DbDetailStorage.FillList(listView1, 2, mask);
					return;
			}
		}

		private void buttonDetailStorageCheck_Click(object sender, System.EventArgs e)
		{	
			// Проверка количества деталей на складе
			DbDetailStorage.DetailStorageCheck();
			listView1.Items.Clear();
		}

		private void listView1_Resize(object sender, EventArgs e)
		{
			Rectangle rect = listView1.ClientRectangle;
			int newWidth = rect.Width - 40
				- listView1.Columns[0].Width
				- listView1.Columns[2].Width
				- listView1.Columns[3].Width
				- listView1.Columns[4].Width
				- listView1.Columns[5].Width;
			//	- listView1.Columns[5].Width;
			//int col1 = newWidth * 2 / 3;
			//int col2 = newWidth * 1 / 3;
			//if(col1 > 100) listView1.Columns[1].Width = col1;
			//if(col2 > 100) listView1.Columns[2].Width = col2;
			if(newWidth > 100) listView1.Columns[1].Width = newWidth;
		}

		private void buttonReportMove_Click(object sender, System.EventArgs e)
		{
			// Определяем выбранную складскую позицию
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			DbDetailStorage element = (DbDetailStorage)item.Tag;
			if(element == null) return;
			FormSelectDateInterval dialog = new FormSelectDateInterval();
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;

			FormReportStorageMove form = new FormReportStorageMove(element, dialog.StartDate, dialog.EndDate);
			form.Show();
		}

		private void buttonChange_Click(object sender, System.EventArgs e)
		{
			// Изменение существующего элемента (Единицы измерения и складские группы)
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			DbDetailStorage element = (DbDetailStorage)item.Tag;
			if(element == null) return;

			FormDetailStorage dialog = new FormDetailStorage(element);
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			dialog.DetailStorage.SetLVItem(item);
		}

		private void buttonNewPrice_Click(object sender, System.EventArgs e)
		{
			// Пересчет цен для все выбранных элементов
			foreach(ListViewItem item in listView1.SelectedItems)
			{
				DbDetailStorage element = (DbDetailStorage)item.Tag;
				if(element.AutoPrice())
				{
					element.SetLVItem(item);
				}
			}
			listView1.SelectedItems.Clear();
			Db.ShowFaults();
		}

		private void buttonSetPrice_Click(object sender, System.EventArgs e)
		{
			// Установка цены вручную
			// Изменение существующего элемента (Единицы измерения и складские группы)
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			DbDetailStorage element = (DbDetailStorage)item.Tag;
			if(element == null) return;
			if(element.Code_1C > 0) return;

			FormSelectString dialog = new FormSelectString("Новая цена", "0.0");
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;

			DbDetailStorage elementNew = new DbDetailStorage(element);
			elementNew.Price = dialog.SelectedFloat;
			if(elementNew.WritePrice() != true) return;
			elementNew.SetLVItem(item);
		}

		private void buttonDelete_Click(object sender, System.EventArgs e)
		{
			// Вызов системной функции - удаление складской позиции
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			DbDetailStorage element = (DbDetailStorage)item.Tag;
			if(element == null) return;
			if(element.DetailStorageDelete() == true) item.Remove();
		}

		private void buttonCheckIncomExpens_Click(object sender, System.EventArgs e)
		{
			// Проверка расходов по приходам
			DbDetailIncom.ExpensCheck();
		}

		private void buttonChangeDetail_Click(object sender, System.EventArgs e)
		{
			// Вызов системной функции - смена исходной детали для складской позиции
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			DbDetailStorage element = (DbDetailStorage)item.Tag;
			if(element == null) return;

			// Запрос новой детали
			FormDetailList dialog = new FormDetailList();
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			if(!element.DetailStorageChangeDetail(dialog.SelectedDetail)) return;
			element.SetLVItem(item);
		}

		private void buttonTurnOff_Click(object sender, System.EventArgs e)
		{
			// Изменяем состояние складской позиции Включена/Выключена
			// Вызов системной функции - смена исходной детали для складской позиции
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			DbDetailStorage element = (DbDetailStorage)item.Tag;
			if(element == null) return;
			if(!element.DetailStorageState()) return;
			element.SetLVItem(item);
		}

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			// Подать заявку на деталь
			// Оформляем заявку на выбранную деталь
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			if(item.Tag == null) return;
			DbDetailStorage storage_detail = (DbDetailStorage)item.Tag;
			DtStorageRequest request = new DtStorageRequest();
			request.SetData("ССЫЛКА_КОД_СКЛАД_ДЕТАЛЬ", storage_detail.Code);
			request.SetData("НАИМЕНОВАНИЕ_СКЛАД_ДЕТАЛЬ", storage_detail.DetailName);

			FormUpdateStorageRequest dialog = new FormUpdateStorageRequest(request);
			dialog.Show();
		}

		private void listView1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			// Показ менюшки
			// На поднятие кнопки мышки - меню
			if(e.Button == MouseButtons.Right)
			{
				// На отпускание правой кнопки мышки - всплывающее меню
				// Настройка меню
				// Показ меню
				contextMenu1.Show(listView1, new Point(e.X, e.Y));
			}
		}

		private void menuItem3_Click(object sender, System.EventArgs e)
		{
			// Получаем список заказ-нарядов содержащих эту деталь
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			if(item.Tag == null) return;
			DbDetailStorage storage_detail = (DbDetailStorage)item.Tag;
			long code_storage = storage_detail.Code;
			
			FormManageCard dialog = new FormManageCard(Db.ClickType.Properties, 3, (object)code_storage);
			dialog.Show();
		}

		public DbDetailOutcom SelectedDetailOutcom
		{
			get
			{
				return selectedDetailOutcom;
			}
		}

		public DbDetailStorage SelectedDetailStorage
		{
			get
			{
				return detailStorage;
			}
		}
	}
}
