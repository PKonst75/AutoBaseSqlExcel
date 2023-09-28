using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormAutoFactura.
	/// </summary>
	public class FormAutoFactura : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBoxNumber;
		private System.Windows.Forms.DateTimePicker dateTimePickerDate;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader8;
		private System.Windows.Forms.ColumnHeader columnHeader9;
		private System.Windows.Forms.ColumnHeader columnHeader10;

		private DbAutoFactura autoFactura = null;

		private System.Windows.Forms.Button buttonSelectByer;
		private System.Windows.Forms.TextBox textBoxBayer;
		private System.Windows.Forms.TextBox textBoxSeller;
		private System.Windows.Forms.Button buttonSelectSeller;
		private System.Windows.Forms.Button buttonOk;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		ArrayList autoModelArray;			// Список всех моделей автомобилей
		ArrayList sellerAutoArray;			// Список продавцов автомобилей
		ArrayList innerCustomerAutoArray;	// Список всех внутренних покупателей

		// Параметры для управления удобным вводом
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textBoxDocument;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textBoxComment;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.NumericUpDown numericUpDownYear;
		private System.Windows.Forms.CheckBox checkBoxShowAll;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button buttonUp;
		private System.Windows.Forms.Button buttonDown;			// Колонка, в которую вбивали последнее значение
		ListViewItem	activeItem;				// Элемент который вводится в данный момент

		public FormAutoFactura(DbAutoFactura sourceAutoFactura)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			// Для быстрого поиска - кэширование
			autoModelArray = new ArrayList();
			DbAutoModel.FillArray(autoModelArray);

			sellerAutoArray = new ArrayList();
			DbPartner.FillArray(sellerAutoArray, DbPartner.PartnerFunction.SellerAuto);

			innerCustomerAutoArray = new ArrayList();
			DbPartner.FillArray(innerCustomerAutoArray, DbPartner.PartnerFunction.InnerCustomerAuto);

			// Подготовка параметров
			if(sourceAutoFactura != null)
			{
				autoFactura		= new DbAutoFactura(sourceAutoFactura);
				// Заполнение возможного прихода по данному документу
				DbAutoIncom.FillList(listView1, autoFactura);
			}
			else
			{
				autoFactura		= new DbAutoFactura();
			}
			textBoxBayer.Text			= autoFactura.BuyerNameTxt;
			textBoxSeller.Text			= autoFactura.SellerNameTxt;
			textBoxDocument.Text		= autoFactura.Document;
			textBoxNumber.Text			= autoFactura.Number;
			dateTimePickerDate.Value	= autoFactura.Date;
			textBoxComment.Text			= autoFactura.Comment;

			// Год выпуска устанавливаем текущим годом
			numericUpDownYear.Value		= DateTime.Now.Year;

			/*if(sourceAutoFactura != null)
			{
				autoFactura = sourceAutoFactura;
				textBoxNumber.ReadOnly = true;
				dateTimePickerDate.Enabled = false;
				buttonSelectByer.Enabled = false;
				buttonSelectSeller.Enabled = false;
				DbAutoStorage.FillList(listView1, autoFactura);
			}
			else
			{
				autoFactura = new DbAutoFactura();
			}
			textBoxBayer.Text = autoFactura.BayerName;
			textBoxSeller.Text = autoFactura.SellerName;
			textBoxNumber.Text = autoFactura.Number;
			dateTimePickerDate.Value = autoFactura.Date;
			*/
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textBoxNumber = new System.Windows.Forms.TextBox();
			this.dateTimePickerDate = new System.Windows.Forms.DateTimePicker();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.textBoxBayer = new System.Windows.Forms.TextBox();
			this.textBoxSeller = new System.Windows.Forms.TextBox();
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader10 = new System.Windows.Forms.ColumnHeader();
			this.buttonSelectByer = new System.Windows.Forms.Button();
			this.buttonSelectSeller = new System.Windows.Forms.Button();
			this.buttonOk = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.textBoxDocument = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.textBoxComment = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.numericUpDownYear = new System.Windows.Forms.NumericUpDown();
			this.checkBoxShowAll = new System.Windows.Forms.CheckBox();
			this.button1 = new System.Windows.Forms.Button();
			this.buttonUp = new System.Windows.Forms.Button();
			this.buttonDown = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownYear)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(288, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(56, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "Номер";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(440, 8);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(48, 23);
			this.label2.TabIndex = 1;
			this.label2.Text = "дата";
			// 
			// textBoxNumber
			// 
			this.textBoxNumber.Location = new System.Drawing.Point(344, 8);
			this.textBoxNumber.Name = "textBoxNumber";
			this.textBoxNumber.Size = new System.Drawing.Size(96, 23);
			this.textBoxNumber.TabIndex = 2;
			this.textBoxNumber.Text = "";
			// 
			// dateTimePickerDate
			// 
			this.dateTimePickerDate.Location = new System.Drawing.Point(488, 8);
			this.dateTimePickerDate.Name = "dateTimePickerDate";
			this.dateTimePickerDate.TabIndex = 3;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 80);
			this.label3.Name = "label3";
			this.label3.TabIndex = 4;
			this.label3.Text = "Покупатель";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 112);
			this.label4.Name = "label4";
			this.label4.TabIndex = 5;
			this.label4.Text = "Продавец";
			// 
			// textBoxBayer
			// 
			this.textBoxBayer.Location = new System.Drawing.Point(120, 80);
			this.textBoxBayer.Name = "textBoxBayer";
			this.textBoxBayer.ReadOnly = true;
			this.textBoxBayer.Size = new System.Drawing.Size(488, 23);
			this.textBoxBayer.TabIndex = 5;
			this.textBoxBayer.Text = "textBox1";
			this.textBoxBayer.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxBayer_KeyPress);
			this.textBoxBayer.Click += new System.EventHandler(this.textBoxBayer_Click);
			// 
			// textBoxSeller
			// 
			this.textBoxSeller.Location = new System.Drawing.Point(120, 112);
			this.textBoxSeller.Name = "textBoxSeller";
			this.textBoxSeller.ReadOnly = true;
			this.textBoxSeller.Size = new System.Drawing.Size(488, 23);
			this.textBoxSeller.TabIndex = 6;
			this.textBoxSeller.Text = "textBox2";
			this.textBoxSeller.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxSeller_KeyPress);
			this.textBoxSeller.Click += new System.EventHandler(this.textBoxSeller_Click);
			// 
			// listView1
			// 
			this.listView1.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.columnHeader6,
																						this.columnHeader2,
																						this.columnHeader1,
																						this.columnHeader3,
																						this.columnHeader4,
																						this.columnHeader5,
																						this.columnHeader8,
																						this.columnHeader9,
																						this.columnHeader10});
			this.listView1.FullRowSelect = true;
			this.listView1.GridLines = true;
			this.listView1.HideSelection = false;
			this.listView1.Location = new System.Drawing.Point(8, 192);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(720, 224);
			this.listView1.TabIndex = 8;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listView1_KeyDown);
			this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "№";
			this.columnHeader6.Width = 30;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Марка";
			this.columnHeader2.Width = 180;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Вариант";
			this.columnHeader1.Width = 80;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Комплектация";
			this.columnHeader3.Width = 120;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "№ кузова";
			this.columnHeader4.Width = 100;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "цвет";
			this.columnHeader5.Width = 180;
			// 
			// columnHeader8
			// 
			this.columnHeader8.Text = "Цена с НДС";
			this.columnHeader8.Width = 100;
			// 
			// columnHeader9
			// 
			this.columnHeader9.Text = "Доставка";
			this.columnHeader9.Width = 100;
			// 
			// columnHeader10
			// 
			this.columnHeader10.Text = "Общая цена";
			this.columnHeader10.Width = 100;
			// 
			// buttonSelectByer
			// 
			this.buttonSelectByer.Location = new System.Drawing.Point(616, 80);
			this.buttonSelectByer.Name = "buttonSelectByer";
			this.buttonSelectByer.Size = new System.Drawing.Size(24, 23);
			this.buttonSelectByer.TabIndex = 9;
			this.buttonSelectByer.Text = "...";
			this.buttonSelectByer.Click += new System.EventHandler(this.buttonSelectByer_Click);
			// 
			// buttonSelectSeller
			// 
			this.buttonSelectSeller.Location = new System.Drawing.Point(616, 112);
			this.buttonSelectSeller.Name = "buttonSelectSeller";
			this.buttonSelectSeller.Size = new System.Drawing.Size(24, 23);
			this.buttonSelectSeller.TabIndex = 10;
			this.buttonSelectSeller.Text = "...";
			this.buttonSelectSeller.Click += new System.EventHandler(this.buttonSelectSeller_Click);
			// 
			// buttonOk
			// 
			this.buttonOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.buttonOk.Location = new System.Drawing.Point(320, 424);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.TabIndex = 11;
			this.buttonOk.Text = "ОК";
			this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(8, 8);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(72, 23);
			this.label5.TabIndex = 12;
			this.label5.Text = "Документ";
			// 
			// textBoxDocument
			// 
			this.textBoxDocument.Location = new System.Drawing.Point(80, 8);
			this.textBoxDocument.Name = "textBoxDocument";
			this.textBoxDocument.Size = new System.Drawing.Size(200, 23);
			this.textBoxDocument.TabIndex = 1;
			this.textBoxDocument.Text = "";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(8, 48);
			this.label6.Name = "label6";
			this.label6.TabIndex = 14;
			this.label6.Text = "Примечание";
			// 
			// textBoxComment
			// 
			this.textBoxComment.Location = new System.Drawing.Point(112, 48);
			this.textBoxComment.Name = "textBoxComment";
			this.textBoxComment.Size = new System.Drawing.Size(560, 23);
			this.textBoxComment.TabIndex = 4;
			this.textBoxComment.Text = "";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(16, 144);
			this.label8.Name = "label8";
			this.label8.TabIndex = 18;
			this.label8.Text = "Год выпуска";
			// 
			// numericUpDownYear
			// 
			this.numericUpDownYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.numericUpDownYear.Location = new System.Drawing.Point(120, 144);
			this.numericUpDownYear.Maximum = new System.Decimal(new int[] {
																			  3000,
																			  0,
																			  0,
																			  0});
			this.numericUpDownYear.Minimum = new System.Decimal(new int[] {
																			  1900,
																			  0,
																			  0,
																			  0});
			this.numericUpDownYear.Name = "numericUpDownYear";
			this.numericUpDownYear.Size = new System.Drawing.Size(64, 23);
			this.numericUpDownYear.TabIndex = 20;
			this.numericUpDownYear.Value = new System.Decimal(new int[] {
																			1900,
																			0,
																			0,
																			0});
			// 
			// checkBoxShowAll
			// 
			this.checkBoxShowAll.Appearance = System.Windows.Forms.Appearance.Button;
			this.checkBoxShowAll.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkBoxShowAll.Location = new System.Drawing.Point(216, 144);
			this.checkBoxShowAll.Name = "checkBoxShowAll";
			this.checkBoxShowAll.Size = new System.Drawing.Size(120, 24);
			this.checkBoxShowAll.TabIndex = 21;
			this.checkBoxShowAll.Text = "ПОКАЗАТЬ ВСЕ";
			this.checkBoxShowAll.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.checkBoxShowAll.CheckedChanged += new System.EventHandler(this.checkBoxShowAll_CheckedChanged);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(352, 160);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(176, 23);
			this.button1.TabIndex = 22;
			this.button1.Text = "Восстановить индексы";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// buttonUp
			// 
			this.buttonUp.Location = new System.Drawing.Point(528, 144);
			this.buttonUp.Name = "buttonUp";
			this.buttonUp.Size = new System.Drawing.Size(56, 24);
			this.buttonUp.TabIndex = 23;
			this.buttonUp.Text = "UP";
			this.buttonUp.Click += new System.EventHandler(this.buttonUp_Click);
			// 
			// buttonDown
			// 
			this.buttonDown.Location = new System.Drawing.Point(528, 168);
			this.buttonDown.Name = "buttonDown";
			this.buttonDown.Size = new System.Drawing.Size(56, 23);
			this.buttonDown.TabIndex = 24;
			this.buttonDown.Text = "DOWN";
			this.buttonDown.Click += new System.EventHandler(this.buttonDown_Click);
			// 
			// FormAutoFactura
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.ClientSize = new System.Drawing.Size(736, 453);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.buttonDown,
																		  this.buttonUp,
																		  this.button1,
																		  this.checkBoxShowAll,
																		  this.numericUpDownYear,
																		  this.label8,
																		  this.textBoxComment,
																		  this.label6,
																		  this.textBoxDocument,
																		  this.label5,
																		  this.buttonOk,
																		  this.buttonSelectSeller,
																		  this.buttonSelectByer,
																		  this.listView1,
																		  this.textBoxSeller,
																		  this.textBoxBayer,
																		  this.label4,
																		  this.label3,
																		  this.dateTimePickerDate,
																		  this.textBoxNumber,
																		  this.label2,
																		  this.label1});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.Name = "FormAutoFactura";
			this.Text = "Приходный документ";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownYear)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion
		
		protected void listView1_DoubleClick(object sender, System.EventArgs e)
		{
			FormSelectionType1 dialog1;
			FormSelectionType2 dialog2;
			string initText;
			// Необходимо определить элемент и колонку, на которой кликнули
			int column = Db.GetColumnPosition(listView1);
			ListViewItem item = Db.GetItemPosition(listView1);
			if(item == null) return;
			DbAutoIncom autoIncom = (DbAutoIncom)item.Tag;
			if(autoIncom == null) return;

			if(column == 2)
			{
				// Автоматический переход к следующему полю - Подмодель (вариант)
				if(autoIncom.Auto.AutoSubModel != null)
					initText		= autoIncom.Auto.AutoSubModel.DbTitle();
				else
					initText		= "";
				ArrayList autoSubModelArray = new ArrayList();
				DbAutoSubmodel.FillArray(autoSubModelArray, autoIncom.Auto.AutoModel, 0); 
				dialog1 = Db.MakeFormSelectionType1(this, item, 2, autoSubModelArray, initText, true);
				dialog1.ShowDialog(this);
				if(dialog1.DialogResult != DialogResult.OK) return;
				autoIncom.Auto.AutoSubModel = (DbAutoSubmodel)dialog1.SelectedElement;
				autoIncom.SetLVItem(item);
				column = 3;
			}
			if(column == 3)
			{
				// Автоматический переход к следующему полю - Комплектация
				if(autoIncom.Auto.AutoComplect != null)
					initText		= autoIncom.Auto.AutoComplect.DbTitle();
				else
					initText		= "";
				ArrayList autoComplectArray = new ArrayList();
				DbAutoComplect.FillArray(autoComplectArray, autoIncom.Auto.AutoModel, 0); 
				dialog1 = Db.MakeFormSelectionType1(this, item, 3, autoComplectArray, initText, true);
				dialog1.ShowDialog(this);
				if(dialog1.DialogResult != DialogResult.OK) return;
				autoIncom.Auto.AutoComplect = (DbAutoComplect)dialog1.SelectedElement;
				autoIncom.SetLVItem(item);
				column = 4;
			}
			if(column == 4)
			{
				// Автоматический переход к следующему полю	- Номер кузова
				dialog2 = Db.MakeFormSelectionType2(this, item, 4, autoIncom.BodyNoTxt);
				dialog2.ShowDialog(this);
				if(dialog2.DialogResult != DialogResult.OK) return;
				autoIncom.Auto.BodyNo = dialog2.SelectedTextUp;
				autoIncom.SetLVItem(item);
				column = 5;
			}
			if(column == 5)
			{
				// Автоматический переход к следующему полю - Цвет
				if(autoIncom.Auto.AutoColors != null)
					initText		= autoIncom.Auto.AutoColors.DbTitle();
				else
					initText		= "";
				ArrayList autoColorsArray = new ArrayList();
				DbAutoColors.FillArray(autoColorsArray, autoIncom.Auto.AutoModel, 0); 
				dialog1 = Db.MakeFormSelectionType1(this, item, 5, autoColorsArray, initText, true);
				dialog1.ShowDialog(this);
				if(dialog1.DialogResult != DialogResult.OK) return;
				autoIncom.Auto.AutoColors = (DbAutoColors)dialog1.SelectedElement;
				autoIncom.SetLVItem(item);
				column = 6;
			}
			if(column == 6)
			{
				// Автоматический переход к следующему полю	- Входная цена
				dialog2 = Db.MakeFormSelectionType2(this, item, 6, autoIncom.IncomPriceTxt);
				dialog2.ShowDialog(this);
				if(dialog2.DialogResult != DialogResult.OK) return;
				autoIncom.IncomPrice	 = dialog2.SelectedFloat;
				autoIncom.SetLVItem(item);
				column = 7;
			}
			if(column == 7)
			{
				// Автоматический переход к следующему полю - Цена доставки
				dialog2 = Db.MakeFormSelectionType2(this, item, 7, autoIncom.DeliveryPriceTxt);
				dialog2.ShowDialog(this);
				if(dialog2.DialogResult != DialogResult.OK) return;
				autoIncom.DeliveryPrice	 = dialog2.SelectedFloat;
				autoIncom.SetLVItem(item);
			}
		}

		protected void listView1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			DbAutoIncom		newAutoIncom;		// Новый вводимый элемент
			string			initText;
			long			codeModelOld;
			ListViewItem	item;

			if(e.KeyCode == Keys.Delete)
			{
				item = Db.GetItemSelected(listView1);
				if(item == null) return;
				DbAutoIncom element = (DbAutoIncom)item.Tag;
				if(element == null) return;
				// Удяляем запись
				if(element.Code == 0)
				{
					item.Remove();
					// Уменьшаем на 1 все последующие позиции
					foreach(ListViewItem itm in listView1.Items)
					{
						DbAutoIncom	elm	= (DbAutoIncom)itm.Tag;
						if(elm.Position > element.Position)
						{
							elm.Position = elm.Position - 1;
							elm.SetLVItem(itm);
						}
					}
					return;
				}
				// Запускаем миханизм удаления прихода!
				if(element.WriteDeleteIncom() != true) return;
				item.Remove();

				// Уменьшаем на 1 все последующие позиции с записью в базу
				foreach(ListViewItem itm in listView1.Items)
				{
					DbAutoIncom	elm	= (DbAutoIncom)itm.Tag;
					if(elm.Position > element.Position)
					{
						elm.Position = elm.Position - 1;
						elm.SetLVItem(itm);
						elm.Write(AutoFactura);
					}
				}
				Db.ShowFaults();
				return;
			}

			if(e.KeyCode == Keys.Enter)
			{
				// Нажата клавиша ENTER - активируется механизм добавления нового
				// или копирования выбранного элемента с последующим пошаговым введением
				if(listView1.SelectedItems.Count > 0)
					item	= listView1.SelectedItems[0];
				else
					item	= null;
				if(item != null)
				{
					// Начинаетяся копирование выбранной строчки
					newAutoIncom				= new DbAutoIncom((DbAutoIncom)item.Tag);
					newAutoIncom.Adding			= true;		// Хотя делали копирование - элемент новый!
					newAutoIncom.ResetAuto();

					initText					= newAutoIncom.Auto.ModelTxt;
					codeModelOld				= newAutoIncom.Auto.CodeModel;
					newAutoIncom.Position		= newAutoIncom.Position + 1;

					// Перебиваем номера у всех последующих элементов - делаем вставку
					activeItem = listView1.Items.Insert(item.Index + 1, newAutoIncom.LVItem);
				}
				else
				{
					// Начинаетяся ведение новой строчки
					newAutoIncom	= new DbAutoIncom();
					initText		= "";
					codeModelOld	= 0;
					newAutoIncom.Position = 1;

					// Вставка на первое место
					activeItem = listView1.Items.Insert(0, newAutoIncom.LVItem);
				}
				//activeItem = listView1.Items.Add(newAutoIncom.LVItem);
				FormSelectionType1 dialog1 = Db.MakeFormSelectionType1(this, activeItem, 1, autoModelArray, initText, false);
				dialog1.ShowDialog(this);
				if(dialog1.DialogResult != DialogResult.OK)
				{
					// Отказались от выбора? - удаляем введенный элемент
					activeItem.Remove();
					return;
				}
				// Если от выбора не отказались - увеличиваем на единичку позиции всех последующих элементов
				foreach(ListViewItem itm in listView1.Items)
				{
					DbAutoIncom	element	= (DbAutoIncom)itm.Tag;
					if(element.Position >= newAutoIncom.Position)
					{
						if(itm != activeItem)
						{
							element.Position = element.Position + 1;
							element.SetLVItem(itm);
						}
					}
				}

				// Если изменили модель - производим очистку всех данных, кроме позиции
				if (codeModelOld != 0)
				{
					if(codeModelOld != ((DbAutoModel)dialog1.SelectedElement).Code)
					{
						int pos = newAutoIncom.Position;
						newAutoIncom	= new DbAutoIncom();
						newAutoIncom.Position = pos;
					}
				}
				newAutoIncom.Auto.AutoModel = (DbAutoModel)dialog1.SelectedElement;
				newAutoIncom.SetLVItem(activeItem);
				// Автоматический переход к следующему полю - Подмодель (вариант)
				if(newAutoIncom.Auto.AutoSubModel != null)
					initText		= newAutoIncom.Auto.AutoSubModel.DbTitle();
				else
					initText		= "";
				ArrayList autoSubModelArray = new ArrayList();
				DbAutoSubmodel.FillArray(autoSubModelArray, newAutoIncom.Auto.AutoModel, 0); 
				dialog1 = Db.MakeFormSelectionType1(this, activeItem, 2, autoSubModelArray, initText, true);
				dialog1.ShowDialog(this);
				if(dialog1.DialogResult != DialogResult.OK) return;
				if(dialog1.SelectedElement == null)
				{
					// Пробуем ввести новый элемент в базу
					if(dialog1.SelectedText.Length > 0)
					{
						FormAutoSubmodel dialogTmp = new FormAutoSubmodel(newAutoIncom.Auto.AutoModel, null, dialog1.SelectedText);
						dialogTmp.ShowDialog();
						if(dialogTmp.DialogResult == DialogResult.OK)
						{
							newAutoIncom.Auto.AutoSubModel = dialogTmp.AutoSubmodel;
						}
						else
							newAutoIncom.Auto.AutoSubModel = (DbAutoSubmodel)dialog1.SelectedElement;
					}
					else
						newAutoIncom.Auto.AutoSubModel = (DbAutoSubmodel)dialog1.SelectedElement;
				}
				else
					newAutoIncom.Auto.AutoSubModel = (DbAutoSubmodel)dialog1.SelectedElement;
				newAutoIncom.SetLVItem(activeItem);
				// Автоматический переход к следующему полю - Комплектация
				if(newAutoIncom.Auto.AutoComplect != null)
					initText		= newAutoIncom.Auto.AutoComplect.DbTitle();
				else
					initText		= "";
				ArrayList autoComplectArray = new ArrayList();
				DbAutoComplect.FillArray(autoComplectArray, newAutoIncom.Auto.AutoModel, 0); 
				dialog1 = Db.MakeFormSelectionType1(this, activeItem, 3, autoComplectArray, initText, true);
				dialog1.ShowDialog(this);
				if(dialog1.DialogResult != DialogResult.OK) return;
				if(dialog1.SelectedElement == null)
				{
					// Пробуем ввести новый элемент в базу
					if(dialog1.SelectedText.Length > 0)
					{
						FormAutoComplect dialogTmp = new FormAutoComplect(newAutoIncom.Auto.AutoModel, null, dialog1.SelectedText);
						dialogTmp.ShowDialog();
						if(dialogTmp.DialogResult == DialogResult.OK)
						{
							newAutoIncom.Auto.AutoComplect = dialogTmp.AutoComplect;
						}
						else
							newAutoIncom.Auto.AutoComplect = (DbAutoComplect)dialog1.SelectedElement;
					}
					else
						newAutoIncom.Auto.AutoComplect = (DbAutoComplect)dialog1.SelectedElement;
				}
				else
					newAutoIncom.Auto.AutoComplect = (DbAutoComplect)dialog1.SelectedElement;
				newAutoIncom.SetLVItem(activeItem);
				// Автоматический переход к следующему полю	- Номер кузова
				FormSelectionType2 dialog2 = Db.MakeFormSelectionType2(this, activeItem, 4, "");
				dialog2.ShowDialog(this);
				if(dialog2.DialogResult != DialogResult.OK) return;
				newAutoIncom.Auto.BodyNo = dialog2.SelectedTextUp;
				newAutoIncom.SetLVItem(activeItem);
				// Автоматический переход к следующему полю - Цвет
				if(newAutoIncom.Auto.AutoColors != null)
					initText		= newAutoIncom.Auto.AutoColors.DbTitle();
				else
					initText		= "";
				ArrayList autoColorsArray = new ArrayList();
				DbAutoColors.FillArray(autoColorsArray, newAutoIncom.Auto.AutoModel, 0); 
				dialog1 = Db.MakeFormSelectionType1(this, activeItem, 5, autoColorsArray, initText, true);
				dialog1.ShowDialog(this);
				if(dialog1.DialogResult != DialogResult.OK) return;
				if(dialog1.SelectedElement == null)
				{
					// Пробуем ввести новый элемент в базу
					if(dialog1.SelectedText.Length > 0)
					{
						FormAutoColors dialogTmp = new FormAutoColors(newAutoIncom.Auto.AutoModel, null, dialog1.SelectedText);
						dialogTmp.ShowDialog();
						if(dialogTmp.DialogResult == DialogResult.OK)
						{
							newAutoIncom.Auto.AutoColors = dialogTmp.AutoColors;
						}
						else
							newAutoIncom.Auto.AutoColors = (DbAutoColors)dialog1.SelectedElement;
					}
					else
						newAutoIncom.Auto.AutoColors = (DbAutoColors)dialog1.SelectedElement;
				}
				else
					newAutoIncom.Auto.AutoColors = (DbAutoColors)dialog1.SelectedElement;
				newAutoIncom.SetLVItem(activeItem);
				// Автоматический переход к следующему полю	- Входная цена
				dialog2 = Db.MakeFormSelectionType2(this, activeItem, 6, newAutoIncom.IncomPriceTxt);
				dialog2.ShowDialog(this);
				if(dialog2.DialogResult != DialogResult.OK) return;
				newAutoIncom.IncomPrice	 = dialog2.SelectedFloat;
				newAutoIncom.SetLVItem(activeItem);
				// Автоматический переход к следующему полю - Цена доставки
				dialog2 = Db.MakeFormSelectionType2(this, activeItem, 7, newAutoIncom.DeliveryPriceTxt);
				dialog2.ShowDialog(this);
				if(dialog2.DialogResult != DialogResult.OK) return;
				newAutoIncom.DeliveryPrice	 = dialog2.SelectedFloat;
				newAutoIncom.SetLVItem(activeItem);

				// Выбираем последний введенный элемент
				listView1.SelectedItems.Clear();
				activeItem.Selected	= true;
				activeItem.Focused	= true;

				return;
			}
		}

		private void buttonSelectByer_Click(object sender, System.EventArgs e)
		{
			// Выбор контрагента покупателя
			FormPartnerList dialog = new FormPartnerList();
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			DbPartner partner = dialog.Partner;
			autoFactura.PartnerByer = partner;
			textBoxBayer.Text = autoFactura.BuyerNameTxt;
		}

		private void buttonSelectSeller_Click(object sender, System.EventArgs e)
		{
			// Выбор контрагента продавца
			FormPartnerList dialog = new FormPartnerList();
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			DbPartner partner = dialog.Partner;
			autoFactura.PartnerSeller = partner;
			textBoxSeller.Text = autoFactura.SellerNameTxt;
		}

		private void buttonOk_Click(object sender, System.EventArgs e)
		{

			// Проверяем сделанные изменения в шапке документа
			autoFactura.Document	= textBoxDocument.Text;
			autoFactura.Number		= textBoxNumber.Text;
			autoFactura.Date		= dateTimePickerDate.Value;
			autoFactura.Comment		= textBoxComment.Text;
			autoFactura.Valid();
			if(Db.ShowFaults() == true) return;

			// Сохраняем приходный документ
			if(autoFactura.Write() != true) return;
			
			// Сохраняем все введенные автомобили
			if(DbAutoIncom.WriteList(listView1, autoFactura) == false) return;
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		public DbAutoFactura AutoFactura
		{
			get
			{
				return autoFactura;
			}
		}

		public ListViewItem GetLastItem()
		{
			ListViewItem	lastItem;
			int				itemsCount;

			itemsCount = listView1.Items.Count;
			if(itemsCount > 0)
			{
				lastItem = listView1.Items[itemsCount - 1];
				return lastItem;
			}
			else
				return null;
		}

		protected void textBoxSeller_Click(object sender, EventArgs e)
		{
			FormSelectionType1 dialog = Db.MakeFormSelectionType1(this, textBoxSeller, sellerAutoArray, textBoxSeller.Text, false);
			dialog.ShowDialog();
			if(dialog.DialogResult != DialogResult.OK) return;
			if (dialog.SelectedElement == null) return;
			autoFactura.PartnerSeller = (DbPartner)dialog.SelectedElement;
			textBoxSeller.Text = autoFactura.SellerNameTxt;
		}

		protected void textBoxSeller_KeyPress(object sender, KeyPressEventArgs e)
		{
			string txt = new String(e.KeyChar, 1);
			FormSelectionType1 dialog = Db.MakeFormSelectionType1(this, textBoxSeller, sellerAutoArray, txt, false);
			dialog.ShowDialog();
			if(dialog.DialogResult != DialogResult.OK) return;
			if (dialog.SelectedElement == null) return;
			autoFactura.PartnerSeller = (DbPartner)dialog.SelectedElement;
			textBoxSeller.Text = autoFactura.SellerNameTxt;
		}

		protected void textBoxBayer_Click(object sender, EventArgs e)
		{
			
			FormSelectionType1 dialog = Db.MakeFormSelectionType1(this, textBoxBayer, innerCustomerAutoArray, textBoxBayer.Text, false);
			dialog.ShowDialog();
			if(dialog.DialogResult != DialogResult.OK) return;
			if (dialog.SelectedElement == null) return;
			autoFactura.PartnerByer = (DbPartner)dialog.SelectedElement;
			textBoxBayer.Text = autoFactura.BuyerNameTxt;
		}

		protected void textBoxBayer_KeyPress(object sender, KeyPressEventArgs e)
		{
			string txt = new String(e.KeyChar, 1);
			FormSelectionType1 dialog = Db.MakeFormSelectionType1(this, textBoxBayer, innerCustomerAutoArray, txt, false);
			dialog.ShowDialog();
			if(dialog.DialogResult != DialogResult.OK) return;
			if (dialog.SelectedElement == null) return;
			autoFactura.PartnerByer = (DbPartner)dialog.SelectedElement;
			textBoxBayer.Text = autoFactura.BuyerNameTxt;
		}

		private void checkBoxShowAll_CheckedChanged(object sender, System.EventArgs e)
		{
			if(checkBoxShowAll.Checked == true)
			{
				// Пеерезачесть полные массивы поставщиков/покупателей
				sellerAutoArray.Clear();
				DbPartner.FillArray(sellerAutoArray, DbPartner.PartnerFunction.Unknown);

				innerCustomerAutoArray.Clear();
				DbPartner.FillArray(innerCustomerAutoArray, DbPartner.PartnerFunction.Unknown);
			}
			else
			{
				// Перезачесть огранниченные массивы
				sellerAutoArray.Clear();
				DbPartner.FillArray(sellerAutoArray, DbPartner.PartnerFunction.SellerAuto);

				innerCustomerAutoArray.Clear();
				DbPartner.FillArray(innerCustomerAutoArray, DbPartner.PartnerFunction.InnerCustomerAuto);
			}
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			// Восстанавливаем индексы списка
			int pos = 1;
			foreach(ListViewItem itm in listView1.Items)
			{
				DbAutoIncom element = (DbAutoIncom)itm.Tag;
				if(element != null)
				{
					element.Position = pos;
					element.SetLVItem(itm);
					pos++;
				}
			}
		}

		private void buttonUp_Click(object sender, System.EventArgs e)
		{
			// Передвинуть  элемент на один вверх
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			if (item.Index == 0) return;
			ListViewItem itm = listView1.Items.Insert(item.Index-1, (ListViewItem)item.Clone());
			item.Remove();
			itm.Selected = true;
		}

		private void buttonDown_Click(object sender, System.EventArgs e)
		{
			// Передвинуть элемент на один вниз
			
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			ListViewItem itm = listView1.Items.Insert(item.Index+1, (ListViewItem)item.Clone());
			item.Remove();
			itm.Selected = true;
		}
	}
}
