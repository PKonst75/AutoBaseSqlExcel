using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormTakeDocument.
	/// </summary>
	public class FormTakeDocument : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ComboBox comboBoxDocumentName;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxDocumentNumber;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.DateTimePicker dateTimePickerDocumentDate;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBoxPartner;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button buttonSelectPartner;
		private System.Windows.Forms.Button buttonApply;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.ColumnHeader columnHeader5;

		private DbTakeDocument document = null;
		private ArrayList autoModelArray = null;
		ListViewItem	activeItem;				// Элемент который вводится в данный момент

		public FormTakeDocument(DbTakeDocument src)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			// Для быстрого поиска - кэширование
			autoModelArray = new ArrayList();
			DbAutoModel.FillArray(autoModelArray);

			this.comboBoxDocumentName.Items.Add("ПСА");
			this.comboBoxDocumentName.Items.Add("Товар-накладная");
			this.comboBoxDocumentName.Items.Add("Догвор приема передачи");

			if(src == null)
				document = new DbTakeDocument();
			else
			{
				document = new DbTakeDocument(src);
				// Заполнение возможного прихода по данному документу
				DbAutoIncom.FillList(listView1, document);
			}

			this.textBoxPartner.Text = document.PartnerNameTxt;
			this.textBoxDocumentNumber.Text = document.Number;
			this.comboBoxDocumentName.Text = document.Document;
			this.dateTimePickerDocumentDate.Value = document.Date;
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
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.comboBoxDocumentName = new System.Windows.Forms.ComboBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.buttonSelectPartner = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.textBoxPartner = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.dateTimePickerDocumentDate = new System.Windows.Forms.DateTimePicker();
			this.label2 = new System.Windows.Forms.Label();
			this.textBoxDocumentNumber = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.buttonApply = new System.Windows.Forms.Button();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.groupBox1.SuspendLayout();
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
																						this.columnHeader5});
			this.listView1.FullRowSelect = true;
			this.listView1.Location = new System.Drawing.Point(8, 112);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(712, 232);
			this.listView1.TabIndex = 0;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.DoubleClick += new EventHandler(this.listView1_DoubleClick);
			this.listView1.KeyDown += new KeyEventHandler(this.listView1_KeyDown);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Модель";
			this.columnHeader1.Width = 140;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "VIN";
			this.columnHeader2.Width = 140;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Спецификация";
			this.columnHeader3.Width = 100;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Комплектация";
			this.columnHeader4.Width = 100;
			// 
			// comboBoxDocumentName
			// 
			this.comboBoxDocumentName.Location = new System.Drawing.Point(8, 40);
			this.comboBoxDocumentName.Name = "comboBoxDocumentName";
			this.comboBoxDocumentName.Size = new System.Drawing.Size(216, 24);
			this.comboBoxDocumentName.TabIndex = 1;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.buttonSelectPartner,
																					this.label4,
																					this.textBoxPartner,
																					this.label3,
																					this.dateTimePickerDocumentDate,
																					this.label2,
																					this.textBoxDocumentNumber,
																					this.label1,
																					this.comboBoxDocumentName});
			this.groupBox1.Location = new System.Drawing.Point(8, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(608, 104);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Документ";
			// 
			// buttonSelectPartner
			// 
			this.buttonSelectPartner.Location = new System.Drawing.Point(568, 72);
			this.buttonSelectPartner.Name = "buttonSelectPartner";
			this.buttonSelectPartner.Size = new System.Drawing.Size(24, 23);
			this.buttonSelectPartner.TabIndex = 9;
			this.buttonSelectPartner.Text = "...";
			this.buttonSelectPartner.Click += new System.EventHandler(this.buttonSelectPartner_Click);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 72);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(96, 24);
			this.label4.TabIndex = 8;
			this.label4.Text = "Поставщик";
			// 
			// textBoxPartner
			// 
			this.textBoxPartner.Location = new System.Drawing.Point(104, 72);
			this.textBoxPartner.Name = "textBoxPartner";
			this.textBoxPartner.ReadOnly = true;
			this.textBoxPartner.Size = new System.Drawing.Size(464, 23);
			this.textBoxPartner.TabIndex = 7;
			this.textBoxPartner.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(368, 16);
			this.label3.Name = "label3";
			this.label3.TabIndex = 6;
			this.label3.Text = "Дата";
			// 
			// dateTimePickerDocumentDate
			// 
			this.dateTimePickerDocumentDate.Location = new System.Drawing.Point(368, 40);
			this.dateTimePickerDocumentDate.Name = "dateTimePickerDocumentDate";
			this.dateTimePickerDocumentDate.TabIndex = 5;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(232, 16);
			this.label2.Name = "label2";
			this.label2.TabIndex = 4;
			this.label2.Text = "Номер";
			// 
			// textBoxDocumentNumber
			// 
			this.textBoxDocumentNumber.Location = new System.Drawing.Point(232, 40);
			this.textBoxDocumentNumber.Name = "textBoxDocumentNumber";
			this.textBoxDocumentNumber.Size = new System.Drawing.Size(128, 23);
			this.textBoxDocumentNumber.TabIndex = 3;
			this.textBoxDocumentNumber.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(120, 23);
			this.label1.TabIndex = 2;
			this.label1.Text = "Наименование";
			// 
			// buttonApply
			// 
			this.buttonApply.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			this.buttonApply.Location = new System.Drawing.Point(624, 352);
			this.buttonApply.Name = "buttonApply";
			this.buttonApply.Size = new System.Drawing.Size(96, 24);
			this.buttonApply.TabIndex = 3;
			this.buttonApply.Text = "Применить";
			this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "Цвет";
			this.columnHeader5.Width = 200;
			// 
			// FormTakeDocument
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.ClientSize = new System.Drawing.Size(728, 381);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.buttonApply,
																		  this.groupBox1,
																		  this.listView1});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.Name = "FormTakeDocument";
			this.Text = "Документ";
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonApply_Click(object sender, System.EventArgs e)
		{
			// Применяем сделанные изменения
			document.Number = this.textBoxDocumentNumber.Text;
			document.Document = this.comboBoxDocumentName.Text;
			document.Date = this.dateTimePickerDocumentDate.Value;

			document.Valid();

			if(Db.ShowFaults() == true) return;

			// Запись/изменение документа
			if(document.Write() != true) return;

			// Сохраняем все введенные автомобили
			if(DbAutoIncom.WriteList(listView1, document) == false) return;
			this.DialogResult = DialogResult.OK;
			this.Close();

			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void buttonSelectPartner_Click(object sender, System.EventArgs e)
		{
			// Выбираем поставщика автомобилей
			FormPartnerList dialog = new FormPartnerList();
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			document.Partner = dialog.Partner;
			this.textBoxPartner.Text = document.PartnerNameTxt;
		}

		public DbTakeDocument Document
		{
			get
			{
				return document;
			}
		}

		protected void listView1_DoubleClick(object sender, EventArgs e)
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

			if(autoIncom.Code != 0 && column == 1) return;	// Если элемент уже писали в базу - нельзя менять его VIN

			if(column == 1)
			{
				// Автоматический переход к следующему полю	- VIN
				dialog2 = Db.MakeFormSelectionType2(this, item, 1, autoIncom.VinNoTxt);
				dialog2.ShowDialog(this);
				if(dialog2.DialogResult != DialogResult.OK) return;
				autoIncom.Auto.Vin = dialog2.SelectedTextUp;
				autoIncom.SetLVItem(item);
				column = 2;
			}
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
				// Автоматический переход к следующему полю - Цвет
				if(autoIncom.Auto.AutoColors != null)
					initText		= autoIncom.Auto.AutoColors.DbTitle();
				else
					initText		= "";
				ArrayList autoColorsArray = new ArrayList();
				DbAutoColors.FillArray(autoColorsArray, autoIncom.Auto.AutoModel, 0); 
				dialog1 = Db.MakeFormSelectionType1(this, item, 4, autoColorsArray, initText, true);
				dialog1.ShowDialog(this);
				if(dialog1.DialogResult != DialogResult.OK) return;
				autoIncom.Auto.AutoColors = (DbAutoColors)dialog1.SelectedElement;
				autoIncom.SetLVItem(item);
				column = 5;
			}
		}

		protected void listView1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			DbAutoIncom		newAutoIncom;		// Новый вводимый элемент
			string			initText;
			long			codeModelOld;
			ListViewItem	item;

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
					newAutoIncom.SetViewType(1);
					newAutoIncom.Adding			= true;		// Хотя делали копирование - элемент новый!
					newAutoIncom.ResetAuto();

					initText					= newAutoIncom.Auto.ModelTxt;
					codeModelOld				= newAutoIncom.Auto.CodeModel;
				}
				else
				{
					// Начинаетяся ведение новой строчки
					newAutoIncom	= new DbAutoIncom();
					newAutoIncom.SetViewType(1);
					initText		= "";
					codeModelOld	= 0;
				}
				activeItem = listView1.Items.Add(newAutoIncom.LVItem);
				FormSelectionType1 dialog1 = Db.MakeFormSelectionType1(this, activeItem, 0, autoModelArray, initText, false);
				dialog1.ShowDialog(this);
				if(dialog1.DialogResult != DialogResult.OK) return;
				// Если изменили модель - производим очистку всех данных
				if (codeModelOld != 0)
				{
					if(codeModelOld != ((DbAutoModel)dialog1.SelectedElement).Code)
					{
						newAutoIncom	= new DbAutoIncom();
						newAutoIncom.SetViewType(1);
					}
				}
				newAutoIncom.Auto.AutoModel = (DbAutoModel)dialog1.SelectedElement;
				newAutoIncom.SetLVItem(activeItem);
				// Автоматический переход к следующему полю	- VIN
				FormSelectionType2 dialog2 = Db.MakeFormSelectionType2(this, activeItem, 1, "");
				dialog2.ShowDialog(this);
				if(dialog2.DialogResult != DialogResult.OK) return;
				newAutoIncom.Auto.Vin = dialog2.SelectedTextUp;
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
				newAutoIncom.Auto.AutoComplect = (DbAutoComplect)dialog1.SelectedElement;
				newAutoIncom.SetLVItem(activeItem);
				// Автоматический переход к следующему полю - Цвет
				if(newAutoIncom.Auto.AutoColors != null)
					initText		= newAutoIncom.Auto.AutoColors.DbTitle();
				else
					initText		= "";
				ArrayList autoColorsArray = new ArrayList();
				DbAutoColors.FillArray(autoColorsArray, newAutoIncom.Auto.AutoModel, 0); 
				dialog1 = Db.MakeFormSelectionType1(this, activeItem, 4, autoColorsArray, initText, true);
				dialog1.ShowDialog(this);
				if(dialog1.DialogResult != DialogResult.OK) return;
				newAutoIncom.Auto.AutoColors = (DbAutoColors)dialog1.SelectedElement;
				newAutoIncom.SetLVItem(activeItem);
				
				// Выбираем последний введенный элемент
				listView1.SelectedItems.Clear();
				activeItem.Selected	= true;
				activeItem.Focused	= true;

				return;
			}
			if(e.KeyCode == Keys.Delete)
			{
				// Запуск механизма удаления косячного элемента
				item = Db.GetItemSelected(listView1);
				if(item == null) return;
				DbAutoIncom element = (DbAutoIncom)item.Tag;
				if(element == null) return;

				if(element.Code == 0)
				{
					// Елемент еще не введен в базу
					// Просто удаляем его из списка
					item.Remove();
					return;
				}
				// Запускаем миханизм удаления прихода!
				if(element.WriteDeleteIncom() != true) return;
				item.Remove();
			}
		}
	}
}
