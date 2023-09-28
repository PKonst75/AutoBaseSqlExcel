using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormDetailIncom.
	/// </summary>
	public class FormDetailIncom : System.Windows.Forms.Form
	{
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.DateTimePicker dateTimePicker1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.ColumnHeader columnHeader8;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.CheckBox checkBoxAutoNumber;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TabPage tabPageFactura;
		private System.Windows.Forms.TextBox textBoxPartner;
		private System.Windows.Forms.TextBox textBoxDocument;
		private System.Windows.Forms.TextBox textBoxStaff;
		private System.Windows.Forms.TextBox textBoxSumm;
		private System.Windows.Forms.TextBox textBoxNumber;
		private System.Windows.Forms.Button buttonSelectPartner;
		private System.Windows.Forms.Button buttonOk;
		private System.Windows.Forms.Button buttonNew;
		private System.Windows.Forms.Button buttonDelete;
		private System.Windows.Forms.Button buttonSelectStaff;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		
		private DbDetailIncomDoc detailIncomDoc			= null;
		private System.Windows.Forms.TextBox textBox	= null;
		private ListViewItem textItem					= null;
		private int textBoxColumn;
		
		#region Конструкторы/Деструкторы
		public FormDetailIncom(DbDetailIncomDoc sourceDetailIncomDoc)
		{
			InitializeComponent();

			// Подготовка документа-основания
			textBoxDocument.ReadOnly = false;

			if(sourceDetailIncomDoc == null)
			{
				detailIncomDoc = new DbDetailIncomDoc();
				textBoxSumm.Text = "0.00";
				// Предполагаем автоматическую нумерацию
				checkBoxAutoNumber.Checked = true;
				textBoxNumber.Hide();
				dateTimePicker1.Hide();
				label4.Hide();
				label5.Hide();
			}
			else
			{
				detailIncomDoc = new DbDetailIncomDoc(sourceDetailIncomDoc);
				this.Text +=  " № " + detailIncomDoc.FullNumber;
				DbDetailIncom.FillList(listView1, detailIncomDoc);
				SetListIndexes(listView1);
				SetSumm(listView1);
				// Номер уже определен
				checkBoxAutoNumber.Checked = false;
				checkBoxAutoNumber.Hide();
				textBoxNumber.Show();
				textBoxNumber.ReadOnly = true;
				dateTimePicker1.Show();
				dateTimePicker1.Enabled = false;
				label4.Show();
				label5.Show();
				buttonSelectPartner.Hide();		// Нельзя менять поставщика
				buttonSelectStaff.Hide();		// Нельзя менять принявшего товар
			}
			textBoxNumber.Text		= detailIncomDoc.NumberTxt;
			textBoxPartner.Text		= detailIncomDoc.PartnerName;
			textBoxDocument.Text	= detailIncomDoc.Document;
			textBoxStaff.Text		= detailIncomDoc.StaffTxt;
			dateTimePicker1.Value	= detailIncomDoc.Date;
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
		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormDetailIncom));
			this.textBoxPartner = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.buttonSelectPartner = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.textBoxDocument = new System.Windows.Forms.TextBox();
			this.buttonOk = new System.Windows.Forms.Button();
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
			this.buttonNew = new System.Windows.Forms.Button();
			this.buttonDelete = new System.Windows.Forms.Button();
			this.textBoxSumm = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPageFactura = new System.Windows.Forms.TabPage();
			this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
			this.label5 = new System.Windows.Forms.Label();
			this.buttonSelectStaff = new System.Windows.Forms.Button();
			this.textBoxStaff = new System.Windows.Forms.TextBox();
			this.label13 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.textBoxNumber = new System.Windows.Forms.TextBox();
			this.checkBoxAutoNumber = new System.Windows.Forms.CheckBox();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.tabControl1.SuspendLayout();
			this.tabPageFactura.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.SuspendLayout();
			// 
			// textBoxPartner
			// 
			this.textBoxPartner.Location = new System.Drawing.Point(96, 48);
			this.textBoxPartner.Name = "textBoxPartner";
			this.textBoxPartner.ReadOnly = true;
			this.textBoxPartner.Size = new System.Drawing.Size(536, 23);
			this.textBoxPartner.TabIndex = 0;
			this.textBoxPartner.Text = "textBox1";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 48);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(80, 23);
			this.label1.TabIndex = 1;
			this.label1.Text = "Поставщик";
			// 
			// buttonSelectPartner
			// 
			this.buttonSelectPartner.Location = new System.Drawing.Point(632, 48);
			this.buttonSelectPartner.Name = "buttonSelectPartner";
			this.buttonSelectPartner.Size = new System.Drawing.Size(24, 23);
			this.buttonSelectPartner.TabIndex = 2;
			this.buttonSelectPartner.Text = "...";
			this.buttonSelectPartner.Click += new System.EventHandler(this.buttonSelectPartner_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 80);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(160, 23);
			this.label2.TabIndex = 3;
			this.label2.Text = "Документ - основание";
			// 
			// textBoxDocument
			// 
			this.textBoxDocument.Location = new System.Drawing.Point(8, 104);
			this.textBoxDocument.Name = "textBoxDocument";
			this.textBoxDocument.Size = new System.Drawing.Size(648, 23);
			this.textBoxDocument.TabIndex = 4;
			this.textBoxDocument.Text = "";
			// 
			// buttonOk
			// 
			this.buttonOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.buttonOk.Location = new System.Drawing.Point(304, 400);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.TabIndex = 5;
			this.buttonOk.Text = "ОК";
			this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
			// 
			// listView1
			// 
			this.listView1.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.columnHeader1,
																						this.columnHeader8,
																						this.columnHeader2,
																						this.columnHeader3,
																						this.columnHeader4,
																						this.columnHeader5,
																						this.columnHeader6,
																						this.columnHeader7});
			this.listView1.FullRowSelect = true;
			this.listView1.GridLines = true;
			this.listView1.Location = new System.Drawing.Point(8, 32);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(656, 280);
			this.listView1.TabIndex = 6;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
			this.listView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listView1_KeyDown);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "№ п/п";
			this.columnHeader1.Width = 40;
			// 
			// columnHeader8
			// 
			this.columnHeader8.Text = "Код";
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Наименование";
			this.columnHeader2.Width = 180;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Количество";
			this.columnHeader3.Width = 50;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Цена";
			this.columnHeader4.Width = 82;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "НДС";
			this.columnHeader5.Width = 50;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "Цена с НДС";
			this.columnHeader6.Width = 85;
			// 
			// columnHeader7
			// 
			this.columnHeader7.Text = "Сумма";
			this.columnHeader7.Width = 80;
			// 
			// buttonNew
			// 
			this.buttonNew.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonNew.Image")));
			this.buttonNew.Location = new System.Drawing.Point(8, 8);
			this.buttonNew.Name = "buttonNew";
			this.buttonNew.Size = new System.Drawing.Size(24, 23);
			this.buttonNew.TabIndex = 7;
			this.toolTip1.SetToolTip(this.buttonNew, "Новая позиция в приходе");
			this.buttonNew.Click += new System.EventHandler(this.buttonNew_Click);
			// 
			// buttonDelete
			// 
			this.buttonDelete.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonDelete.Image")));
			this.buttonDelete.Location = new System.Drawing.Point(32, 8);
			this.buttonDelete.Name = "buttonDelete";
			this.buttonDelete.Size = new System.Drawing.Size(24, 23);
			this.buttonDelete.TabIndex = 8;
			this.toolTip1.SetToolTip(this.buttonDelete, "Удаление выбранных позиций");
			this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
			// 
			// textBoxSumm
			// 
			this.textBoxSumm.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			this.textBoxSumm.Location = new System.Drawing.Point(544, 320);
			this.textBoxSumm.Name = "textBoxSumm";
			this.textBoxSumm.Size = new System.Drawing.Size(112, 23);
			this.textBoxSumm.TabIndex = 9;
			this.textBoxSumm.Text = "textBox1";
			// 
			// label3
			// 
			this.label3.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			this.label3.Location = new System.Drawing.Point(440, 320);
			this.label3.Name = "label3";
			this.label3.TabIndex = 10;
			this.label3.Text = "Общая сумма";
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.tabControl1.Controls.AddRange(new System.Windows.Forms.Control[] {
																					  this.tabPageFactura,
																					  this.tabPage2});
			this.tabControl1.Location = new System.Drawing.Point(8, 8);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(680, 384);
			this.tabControl1.TabIndex = 11;
			// 
			// tabPageFactura
			// 
			this.tabPageFactura.Controls.AddRange(new System.Windows.Forms.Control[] {
																						 this.dateTimePicker1,
																						 this.label5,
																						 this.buttonSelectStaff,
																						 this.textBoxStaff,
																						 this.label13,
																						 this.label4,
																						 this.textBoxNumber,
																						 this.checkBoxAutoNumber,
																						 this.textBoxDocument,
																						 this.label1,
																						 this.textBoxPartner,
																						 this.buttonSelectPartner,
																						 this.label2});
			this.tabPageFactura.Location = new System.Drawing.Point(4, 25);
			this.tabPageFactura.Name = "tabPageFactura";
			this.tabPageFactura.Size = new System.Drawing.Size(672, 355);
			this.tabPageFactura.TabIndex = 0;
			this.tabPageFactura.Text = "Заголовок";
			// 
			// dateTimePicker1
			// 
			this.dateTimePicker1.Location = new System.Drawing.Point(440, 16);
			this.dateTimePicker1.Name = "dateTimePicker1";
			this.dateTimePicker1.TabIndex = 34;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(392, 16);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(48, 23);
			this.label5.TabIndex = 33;
			this.label5.Text = "Дата";
			// 
			// buttonSelectStaff
			// 
			this.buttonSelectStaff.Location = new System.Drawing.Point(632, 144);
			this.buttonSelectStaff.Name = "buttonSelectStaff";
			this.buttonSelectStaff.Size = new System.Drawing.Size(24, 23);
			this.buttonSelectStaff.TabIndex = 32;
			this.buttonSelectStaff.Text = "...";
			this.buttonSelectStaff.Click += new System.EventHandler(this.buttonSelectStaff_Click);
			// 
			// textBoxStaff
			// 
			this.textBoxStaff.Location = new System.Drawing.Point(384, 144);
			this.textBoxStaff.Name = "textBoxStaff";
			this.textBoxStaff.ReadOnly = true;
			this.textBoxStaff.Size = new System.Drawing.Size(216, 23);
			this.textBoxStaff.TabIndex = 30;
			this.textBoxStaff.Text = "";
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(320, 144);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(56, 23);
			this.label13.TabIndex = 29;
			this.label13.Text = "Принял";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(232, 16);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(56, 23);
			this.label4.TabIndex = 7;
			this.label4.Text = "Номер";
			// 
			// textBoxNumber
			// 
			this.textBoxNumber.Location = new System.Drawing.Point(288, 16);
			this.textBoxNumber.MaxLength = 16;
			this.textBoxNumber.Name = "textBoxNumber";
			this.textBoxNumber.TabIndex = 6;
			this.textBoxNumber.Text = "";
			// 
			// checkBoxAutoNumber
			// 
			this.checkBoxAutoNumber.Location = new System.Drawing.Point(8, 8);
			this.checkBoxAutoNumber.Name = "checkBoxAutoNumber";
			this.checkBoxAutoNumber.Size = new System.Drawing.Size(216, 24);
			this.checkBoxAutoNumber.TabIndex = 5;
			this.checkBoxAutoNumber.Text = "Автоматическая нумерация";
			this.checkBoxAutoNumber.CheckedChanged += new System.EventHandler(this.checkBoxAutoNumber_CheckedChanged);
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.AddRange(new System.Windows.Forms.Control[] {
																				   this.buttonNew,
																				   this.buttonDelete,
																				   this.listView1,
																				   this.textBoxSumm,
																				   this.label3});
			this.tabPage2.Location = new System.Drawing.Point(4, 25);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Size = new System.Drawing.Size(672, 355);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Позиции";
			// 
			// FormDetailIncom
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.ClientSize = new System.Drawing.Size(688, 429);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.tabControl1,
																		  this.buttonOk});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.Name = "FormDetailIncom";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Приходный ордер";
			this.tabControl1.ResumeLayout(false);
			this.tabPageFactura.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#region Отработка событий - нажатия кнопок
		private void buttonSelectStaff_Click(object sender, System.EventArgs e)
		{
			// Выбор принявшего товар
			FormStaffList dialog = new FormStaffList();
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			detailIncomDoc.Staff = dialog.SelectedStaff;
			textBoxStaff.Text = detailIncomDoc.StaffTxt;
		}
		private void checkBoxAutoNumber_CheckedChanged(object sender, System.EventArgs e)
		{
			// Изменение варианта нумерации
			if(detailIncomDoc.Adding == false) return; // Номер уже определен, менять нельзя.
			if(checkBoxAutoNumber.Checked)
			{
				textBoxNumber.Hide();
				dateTimePicker1.Hide();
				label4.Hide();
				label5.Hide();
				// Исправление данных
				detailIncomDoc.Number	= 0;
				textBoxNumber.Text		= detailIncomDoc.NumberTxt;
				dateTimePicker1.Value	= DateTime.Now;
			}
			else
			{
				textBoxNumber.Show();
				dateTimePicker1.Show();
				label4.Show();
				label5.Show();
				// Исправление данных
				detailIncomDoc.Number	= 0;
				textBoxNumber.Text		= detailIncomDoc.NumberTxt;
				dateTimePicker1.Value	= DateTime.Now;
			}
			Db.ShowFaults();
		}
		private void buttonDelete_Click(object sender, System.EventArgs e)
		{
			// Удаляем выбранные элементы
			foreach(ListViewItem item in listView1.SelectedItems)
			{
				DbDetailIncom detailIncom = (DbDetailIncom)item.Tag;
				if(detailIncom != null)
				{
					if(detailIncom.Exists == true)
					{
						if(detailIncom.Del == true) detailIncom.Del = false;
						else detailIncom.Del = true;
						detailIncom.SetLVItem(item);
					}
					else
					{
						listView1.Items.Remove(item);
					}
				}
			}
			SetListIndexes(listView1);
			SetSumm(listView1);
		}
		private void buttonNew_Click(object sender, System.EventArgs e)
		{
			// Забиваем новую запчасть в приходник
			FormDetailStorageList dialog = new FormDetailStorageList(listView1, 1, null, null);
			dialog.ShowDialog(this);
			SetListIndexes(listView1);
			if(dialog.DialogResult != DialogResult.OK) return;
			return;
		}
		private void buttonSelectPartner_Click(object sender, System.EventArgs e)
		{
			// Выбор поставщика по приходному ордеру
			FormPartnerList dialog = new FormPartnerList();
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			detailIncomDoc.Partner = dialog.Partner;
			textBoxPartner.Text = detailIncomDoc.PartnerName;
		}

		private void buttonOk_Click(object sender, System.EventArgs e)
		{
			// Получаем необходимые данные
			detailIncomDoc.Document		= textBoxDocument.Text;
			detailIncomDoc.NumberTxt	= textBoxNumber.Text;
			detailIncomDoc.Date			= dateTimePicker1.Value;
			detailIncomDoc.IsValid();

			// Проверяем все данные из листа
			foreach(ListViewItem item in listView1.Items)
			{
				DbDetailIncom element = (DbDetailIncom)item.Tag;
				if(element != null) element.IsValid();
			}
			if(Db.ShowFaults()) return;

			if(IsChanged() == false)
			{
				this.Close();
				return;	// Изменений не было
			}

			// Если все в порядке - инициируем процедуру добавления
			// Все сразу в одной транзакции
			if(detailIncomDoc.Update(listView1) != true) return;

			// Можно запускать диалог изменения цен склада!
			FormDetailStoragePrice dialog = new FormDetailStoragePrice(listView1);
			dialog.Show();
			dialog.BringToFront();
			// Все было в порядке, пересчитаем сумму
			detailIncomDoc.Summ = SetSumm(listView1);
			this.DialogResult = DialogResult.OK;
			this.Close();
			return;
		}
		#endregion

		#region переопереленные методы
		protected override void OnClosing (System.ComponentModel.CancelEventArgs e)
		{
			// Перед тем как закрывать, проверяем, сохранили ли мы сделанные изменения
			if(this.DialogResult != DialogResult.OK)
			{
				if(IsChanged())
				{
					DialogResult res = MessageBox.Show("Все сделанные изменения будут потеряны. Вы уверены?", "Предупреждение", MessageBoxButtons.YesNo);
					if(res == DialogResult.No)
					{
						e.Cancel = true;
						return;
					}
				}
			}
		}
		#endregion

		#region Доступ к основным параметрам
		public DbDetailIncomDoc DetailIncomDoc
		{
			get
			{
				return detailIncomDoc;
			}
		}
		#endregion

		#region Отработка событий - кнопка мыши
		private void listView1_DoubleClick(object sender, EventArgs e)
		{
			// Необходимо определить элемент и колонку, на которой кликнули
			int column = Db.GetColumnPosition(listView1);
			ListViewItem item = Db.GetItemPosition(listView1);
			if(item == null) return;
			DbDetailIncom detailIncom = (DbDetailIncom)item.Tag;
			if(detailIncom == null) return;

			switch(column)
			{
				case 5:
					textBox = Db.MakeBox(this, item, column);
					TextBoxInit(detailIncom.NdsTxt);
					textItem = item;
					textBoxColumn = 5;
					return;
				case 4:
					textBox = Db.MakeBox(this, item, column);
					TextBoxInit(detailIncom.PriceNoNdsTxt);
					textItem = item;
					textBoxColumn = 4;
					return;
				case 6:
					textBox = Db.MakeBox(this, item, column);
					TextBoxInit(detailIncom.PriceTxt);
					textItem = item;
					textBoxColumn = 6;
					return;
				case 3:
					textBox = Db.MakeBox(this, item, column);
					TextBoxInit(detailIncom.QuontityTxt);
					textItem = item;
					textBoxColumn = 3;
					return;
			}
		}
		#endregion

		#region Отроботка событий - нажатие клавиши
		protected void listView1_KeyDown(object sender, KeyEventArgs e)
		{
			// Ищем выбранный элемент
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			DbDetailIncom element = (DbDetailIncom)item.Tag;
			if(element == null) return;

			if(e.KeyCode == Keys.Add)
			{
				element.Quontity = element.Quontity + 1;
				element.SetLVItem(item);
				SetSumm(listView1);
				return;
			}
			if(e.KeyCode == Keys.Subtract)
			{
				element.Quontity = element.Quontity - 1;
				element.SetLVItem(item);
				SetSumm(listView1);
				return;
			}
			if(e.KeyCode == Keys.Enter)
			{
				textBox = Db.MakeBox(this, item, 4);
				TextBoxInit(element.PriceNoNdsTxt);
				textItem = item;
				textBoxColumn = 4;
				return;
			}
			if(e.KeyCode == Keys.Delete)
			{
				if(element.Exists == false)
				{
					listView1.Items.Remove(item);
					SetListIndexes(listView1);
				}
				else
				{
					element.Del = !element.Del;
					element.SetLVItem(item);
				}
				SetSumm(listView1);
				return;
			}
		}
		#endregion

		#region Для функциональности изменений данных в листе
		public void TextBoxInit(string text)
		{
			textBox.MouseDown += new MouseEventHandler(this.textBox_MouseDown);
			textBox.KeyDown += new KeyEventHandler(this.textBox_KeyDown);
			textBox.Text = text;
			textBox.SelectAll();
		}
		public void TextBoxDispose()
		{
			this.Controls.Remove(textBox);
			textBox.Dispose();
			textBox = null;
			listView1.Focus();
			textItem = null;
			return;
		}
		protected void textBox_MouseDown(object sender, MouseEventArgs e)
		{
			TextBoxDispose();
			return;
		}
		protected void textBox_KeyDown(object sender, KeyEventArgs e)
		{
			
			string text = textBox.Text;
			ListViewItem item = textItem;
			DbDetailIncom detailIncom = (DbDetailIncom)textItem.Tag;
			text = text.Trim();
			if(e.KeyCode == Keys.Enter)
			{
				switch(textBoxColumn)
				{
					case 4:
						detailIncom.PriceNoNdsTxt = text;
						if(Db.ShowFaults())
						{
							TextBoxDisposeNotFocus();
							return;
						}
						detailIncom.SetLVItem(textItem);
						TextBoxDispose();
						SetSumm(listView1);
						return;
					case 5:
						detailIncom.NdsTxt = text;
						if(Db.ShowFaults())
						{
							TextBoxDisposeNotFocus();
							return;
						}
						detailIncom.SetLVItem(textItem);
						TextBoxDispose();
						SetSumm(listView1);
						return;
					case 6:
						detailIncom.PriceTxt = text;
						if(Db.ShowFaults())
						{
							TextBoxDisposeNotFocus();
							return;
						}
						detailIncom.SetLVItem(textItem);
						TextBoxDispose();
						SetSumm(listView1);
						return;
					case 3:
						detailIncom.QuontityTxt = text;
						if(Db.ShowFaults())
						{
							TextBoxDisposeNotFocus();
							return;
						}
						detailIncom.SetLVItem(textItem);
						TextBoxDispose();
						SetSumm(listView1);
						return;
				}
			}
			if(e.KeyCode == Keys.Escape)
			{
				TextBoxDispose();
				return;
			}
		}
		public void TextBoxDisposeNotFocus()
		{
			this.Controls.Remove(textBox);
			textBox.Dispose();
			textBox = null;
			textItem = null;
			return;
		}
		#endregion

		#region Основные методы
		protected void SetListIndexes(ListView list)
		{
			foreach(ListViewItem item in list.Items)
			{
				item.Text = (item.Index + 1).ToString();
			}
		}
		protected float SetSumm(ListView list)
		{
			float summ = 0;
			foreach(ListViewItem item in list.Items)
			{
				DbDetailIncom detailIncom = (DbDetailIncom)item.Tag;
				if(detailIncom != null)
				{
					if(detailIncom.Del != true)
						summ += detailIncom.Summ;
				}
			}
			textBoxSumm.Text = Db.CachToTxt(summ);
			return summ;
		}
		public bool IsChanged()
		{
			if(detailIncomDoc.Adding == true) return true;
			if(detailIncomDoc.Changed == true) return true;
			foreach(ListViewItem item in listView1.Items)
			{
				DbDetailIncom element = (DbDetailIncom)item.Tag;
				if(element != null)
				{
					if(element.Changed == true) return true;
					if(element.Adding == true) return true;
				}
			}
			return false;
		}
		#endregion
	}
}
