using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormDetailOutcomDoc.
	/// </summary>
	public class FormDetailOutcomDoc : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.Button buttonOk;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.Button buttonNew;
		private System.Windows.Forms.TextBox textBoxNumber;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox checkBoxAutoNumber;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBoxGive;
		private System.Windows.Forms.TextBox textBoxGet;
		private System.Windows.Forms.Button buttonSelectGive;
		private System.Windows.Forms.Button buttonSelectGet;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBoxBased;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		DbDetailOutcomDoc detailOutcomDoc = null;
		private System.Windows.Forms.TextBox textBox = null;
		private ListViewItem textItem = null;
		private int textBoxColumn;

		private System.Windows.Forms.Button buttonReturn;
		private System.Windows.Forms.ColumnHeader columnHeader8;
		private System.Windows.Forms.DateTimePicker dateTimePicker1;
		private System.Windows.Forms.Button buttonSetDetailIncom;
		private System.Windows.Forms.CheckBox checkBoxGuaranty;
		private System.Windows.Forms.Label label5;

		public FormDetailOutcomDoc(DbDetailOutcomDoc source)
		{
			InitializeComponent();

			if(source == null)
			{
				detailOutcomDoc = new DbDetailOutcomDoc();
				// По умолчанию - автоматическая нумерация
				label1.Hide();
				label5.Hide();
				textBoxNumber.Hide();
				dateTimePicker1.Hide();
				checkBoxAutoNumber.Checked = true;
			}
			else
			{
				detailOutcomDoc = new DbDetailOutcomDoc(source);
				DbDetailOutcom.FillList(listView1, detailOutcomDoc);
				SetListIndexes(listView1);
				checkBoxAutoNumber.Checked	= false;	// Номер уже есть
				checkBoxAutoNumber.Hide();				// ..
				textBoxNumber.Enabled		= false;
				dateTimePicker1.Enabled		= false;
				checkBoxGuaranty.Checked	= detailOutcomDoc.Guaranty;
				checkBoxGuaranty.Enabled	= false;
			}
			textBoxNumber.Text		= detailOutcomDoc.NumberTxt;
			textBoxGive.Text		= detailOutcomDoc.GiveTxt;
			textBoxGet.Text			= detailOutcomDoc.GetTxt;
			textBoxBased.Text		= detailOutcomDoc.Based;
			dateTimePicker1.Value	= detailOutcomDoc.Date;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormDetailOutcomDoc));
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.checkBoxGuaranty = new System.Windows.Forms.CheckBox();
			this.label5 = new System.Windows.Forms.Label();
			this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
			this.label4 = new System.Windows.Forms.Label();
			this.textBoxBased = new System.Windows.Forms.TextBox();
			this.buttonSelectGet = new System.Windows.Forms.Button();
			this.buttonSelectGive = new System.Windows.Forms.Button();
			this.textBoxGet = new System.Windows.Forms.TextBox();
			this.textBoxGive = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.checkBoxAutoNumber = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.textBoxNumber = new System.Windows.Forms.TextBox();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.buttonSetDetailIncom = new System.Windows.Forms.Button();
			this.buttonReturn = new System.Windows.Forms.Button();
			this.buttonNew = new System.Windows.Forms.Button();
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
			this.buttonOk = new System.Windows.Forms.Button();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.tabControl1.Controls.AddRange(new System.Windows.Forms.Control[] {
																					  this.tabPage1,
																					  this.tabPage2});
			this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.tabControl1.Location = new System.Drawing.Point(8, 8);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(600, 272);
			this.tabControl1.TabIndex = 2;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.AddRange(new System.Windows.Forms.Control[] {
																				   this.checkBoxGuaranty,
																				   this.label5,
																				   this.dateTimePicker1,
																				   this.label4,
																				   this.textBoxBased,
																				   this.buttonSelectGet,
																				   this.buttonSelectGive,
																				   this.textBoxGet,
																				   this.textBoxGive,
																				   this.label3,
																				   this.label2,
																				   this.checkBoxAutoNumber,
																				   this.label1,
																				   this.textBoxNumber});
			this.tabPage1.Location = new System.Drawing.Point(4, 25);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new System.Drawing.Size(568, 243);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Шапка";
			// 
			// checkBoxGuaranty
			// 
			this.checkBoxGuaranty.Location = new System.Drawing.Point(448, 8);
			this.checkBoxGuaranty.Name = "checkBoxGuaranty";
			this.checkBoxGuaranty.TabIndex = 13;
			this.checkBoxGuaranty.Text = "Гарантия";
			this.checkBoxGuaranty.CheckedChanged += new System.EventHandler(this.checkBoxGuaranty_CheckedChanged);
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(152, 48);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(24, 23);
			this.label5.TabIndex = 12;
			this.label5.Text = "от";
			// 
			// dateTimePicker1
			// 
			this.dateTimePicker1.Location = new System.Drawing.Point(200, 48);
			this.dateTimePicker1.Name = "dateTimePicker1";
			this.dateTimePicker1.TabIndex = 11;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 152);
			this.label4.Name = "label4";
			this.label4.TabIndex = 10;
			this.label4.Text = "Основание";
			// 
			// textBoxBased
			// 
			this.textBoxBased.Location = new System.Drawing.Point(8, 192);
			this.textBoxBased.Name = "textBoxBased";
			this.textBoxBased.Size = new System.Drawing.Size(552, 23);
			this.textBoxBased.TabIndex = 9;
			this.textBoxBased.Text = "textBox1";
			// 
			// buttonSelectGet
			// 
			this.buttonSelectGet.Location = new System.Drawing.Point(424, 120);
			this.buttonSelectGet.Name = "buttonSelectGet";
			this.buttonSelectGet.Size = new System.Drawing.Size(24, 23);
			this.buttonSelectGet.TabIndex = 8;
			this.buttonSelectGet.Text = "...";
			this.buttonSelectGet.Click += new System.EventHandler(this.buttonSelectGet_Click);
			// 
			// buttonSelectGive
			// 
			this.buttonSelectGive.Location = new System.Drawing.Point(424, 88);
			this.buttonSelectGive.Name = "buttonSelectGive";
			this.buttonSelectGive.Size = new System.Drawing.Size(24, 23);
			this.buttonSelectGive.TabIndex = 7;
			this.buttonSelectGive.Text = "...";
			this.buttonSelectGive.Click += new System.EventHandler(this.buttonSelectGive_Click);
			// 
			// textBoxGet
			// 
			this.textBoxGet.Location = new System.Drawing.Point(120, 120);
			this.textBoxGet.Name = "textBoxGet";
			this.textBoxGet.ReadOnly = true;
			this.textBoxGet.Size = new System.Drawing.Size(304, 23);
			this.textBoxGet.TabIndex = 6;
			this.textBoxGet.Text = "textBox1";
			// 
			// textBoxGive
			// 
			this.textBoxGive.Location = new System.Drawing.Point(120, 88);
			this.textBoxGive.Name = "textBoxGive";
			this.textBoxGive.ReadOnly = true;
			this.textBoxGive.Size = new System.Drawing.Size(304, 23);
			this.textBoxGive.TabIndex = 5;
			this.textBoxGive.Text = "textBox1";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 120);
			this.label3.Name = "label3";
			this.label3.TabIndex = 4;
			this.label3.Text = "Получил";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 88);
			this.label2.Name = "label2";
			this.label2.TabIndex = 3;
			this.label2.Text = "Отпустил";
			// 
			// checkBoxAutoNumber
			// 
			this.checkBoxAutoNumber.Location = new System.Drawing.Point(8, 8);
			this.checkBoxAutoNumber.Name = "checkBoxAutoNumber";
			this.checkBoxAutoNumber.Size = new System.Drawing.Size(232, 24);
			this.checkBoxAutoNumber.TabIndex = 2;
			this.checkBoxAutoNumber.Text = "Автоматическкая нумерация";
			this.checkBoxAutoNumber.CheckedChanged += new System.EventHandler(this.checkBoxAutoNumber_CheckedChanged);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 48);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 23);
			this.label1.TabIndex = 1;
			this.label1.Text = "Номер";
			// 
			// textBoxNumber
			// 
			this.textBoxNumber.Location = new System.Drawing.Point(64, 48);
			this.textBoxNumber.Name = "textBoxNumber";
			this.textBoxNumber.Size = new System.Drawing.Size(72, 23);
			this.textBoxNumber.TabIndex = 0;
			this.textBoxNumber.Text = "textBox1";
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.AddRange(new System.Windows.Forms.Control[] {
																				   this.buttonSetDetailIncom,
																				   this.buttonReturn,
																				   this.buttonNew,
																				   this.listView1});
			this.tabPage2.Location = new System.Drawing.Point(4, 25);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Size = new System.Drawing.Size(592, 243);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Позиции";
			// 
			// buttonSetDetailIncom
			// 
			this.buttonSetDetailIncom.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonSetDetailIncom.Image")));
			this.buttonSetDetailIncom.Location = new System.Drawing.Point(56, 8);
			this.buttonSetDetailIncom.Name = "buttonSetDetailIncom";
			this.buttonSetDetailIncom.Size = new System.Drawing.Size(24, 23);
			this.buttonSetDetailIncom.TabIndex = 3;
			this.buttonSetDetailIncom.Click += new System.EventHandler(this.buttonSetDetailIncom_Click);
			// 
			// buttonReturn
			// 
			this.buttonReturn.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonReturn.Image")));
			this.buttonReturn.Location = new System.Drawing.Point(32, 8);
			this.buttonReturn.Name = "buttonReturn";
			this.buttonReturn.Size = new System.Drawing.Size(24, 23);
			this.buttonReturn.TabIndex = 2;
			this.buttonReturn.Click += new System.EventHandler(this.buttonReturn_Click);
			// 
			// buttonNew
			// 
			this.buttonNew.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonNew.Image")));
			this.buttonNew.Location = new System.Drawing.Point(8, 8);
			this.buttonNew.Name = "buttonNew";
			this.buttonNew.Size = new System.Drawing.Size(24, 23);
			this.buttonNew.TabIndex = 1;
			this.buttonNew.Click += new System.EventHandler(this.buttonNew_Click);
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
			this.listView1.FullRowSelect = true;
			this.listView1.GridLines = true;
			this.listView1.Location = new System.Drawing.Point(8, 32);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(576, 205);
			this.listView1.TabIndex = 0;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "№ п/п";
			this.columnHeader1.Width = 40;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Наименование";
			this.columnHeader2.Width = 120;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Количество";
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Цена";
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "НДС";
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "Цена с НДС";
			// 
			// columnHeader7
			// 
			this.columnHeader7.Text = "Сумма";
			// 
			// columnHeader8
			// 
			this.columnHeader8.Text = "Цена входа";
			this.columnHeader8.Width = 80;
			// 
			// buttonOk
			// 
			this.buttonOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.buttonOk.Location = new System.Drawing.Point(284, 288);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.TabIndex = 1;
			this.buttonOk.Text = "ОК";
			this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
			// 
			// FormDetailOutcomDoc
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(616, 317);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.tabControl1,
																		  this.buttonOk});
			this.Name = "FormDetailOutcomDoc";
			this.Text = "Требование";
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonNew_Click(object sender, System.EventArgs e)
		{
			FormDetailStorageList dialog;
			// Новый элемент в расходном ордере
			dialog = new FormDetailStorageList(listView1, 2, null, null);
			dialog.ShowDialog(this);
			// Расстановка цен, в зависимости от типа расходника
			SetPrices(listView1);
			SetListIndexes(listView1);
		}

		protected void SetListIndexes(ListView list)
		{
			foreach(ListViewItem item in list.Items)
			{
				item.Text = (item.Index + 1).ToString();
			}
		}

		protected void SetPrices(ListView list)
		{
			foreach(ListViewItem item in list.Items)
			{
				DbDetailOutcom element = (DbDetailOutcom)item.Tag;
				if(element != null)
				{
					element.SetPrice(detailOutcomDoc.Guaranty);
					element.SetLVItem(item);
				}
			}
		}

		protected float GetSumm(ListView list)
		{
			float summ = 0.0F;
			foreach(ListViewItem item in list.Items)
			{
				DbDetailOutcom element = (DbDetailOutcom)item.Tag;
				if(element != null)
				{
					if(element.Deleted == false)
						summ += element.Summ;
				}
			}
			return summ;
		}

		protected int GetNotImplemented(ListView list)
		{
			int notImplemented = 0;
			foreach(ListViewItem item in list.Items)
			{
				DbDetailOutcom element = (DbDetailOutcom)item.Tag;
				if(element != null)
				{
					if(element.Deleted == false)
						if(element.CodeDetailIncom == 0) notImplemented++;
				}
			}
			return notImplemented;
		}

		private void buttonSelectGive_Click(object sender, System.EventArgs e)
		{
			// Выбор того кто отпустил товар
			FormStaffList dialog = new FormStaffList();
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			detailOutcomDoc.Give = dialog.SelectedStaff;
			textBoxGive.Text = detailOutcomDoc.GiveTxt;
		}

		private void buttonSelectGet_Click(object sender, System.EventArgs e)
		{
			// Выбор того кто получил товар
			FormStaffList dialog = new FormStaffList();
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			detailOutcomDoc.Get = dialog.SelectedStaff;
			textBoxGet.Text = detailOutcomDoc.GetTxt;
		}

		private void buttonOk_Click(object sender, System.EventArgs e)
		{
			// При нажатии кнопки ОК
			if(checkBoxAutoNumber.Checked == true)
			{
				// Автоматическая нумерация
				detailOutcomDoc.Number	= 0;
			}
			else
			{
				// Ручная нумерация
				detailOutcomDoc.NumberTxt	= textBoxNumber.Text;
				detailOutcomDoc.Date		= dateTimePicker1.Value;
			}
			detailOutcomDoc.Based			= textBoxBased.Text;
			detailOutcomDoc.Guaranty		= checkBoxGuaranty.Checked;
			detailOutcomDoc.Summ			= GetSumm(listView1);
			detailOutcomDoc.NotImplemented	= GetNotImplemented(listView1);
			detailOutcomDoc.IsValid();
			if(Db.ShowFaults() == true) return;

			// Добавляем новый документ
			if(detailOutcomDoc.Update(listView1) != true) return;
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		public DbDetailOutcomDoc DetailOutcomDoc
		{
			get
			{
				return detailOutcomDoc;
			}
		}

		private void listView1_DoubleClick(object sender, EventArgs e)
		{
			// Необходимо определить элемент и колонку, на которой кликнули
			int column = Db.GetColumnPosition(listView1);
			ListViewItem item = Db.GetItemPosition(listView1);
			if(item == null) return;
			DbDetailOutcom detailOutcom = (DbDetailOutcom)item.Tag;
			if(detailOutcom == null) return;

			switch(column)
			{
				case 4:
					textBox = Db.MakeBox(this, item, column);
					TextBoxInit(detailOutcom.NdsTxt);
					textItem = item;
					textBoxColumn = 4;
					return;
				case 3:
					if(detailOutcomDoc.Guaranty == true) return;	// В гарантийном документе цену изменять нельзя
					textBox = Db.MakeBox(this, item, column);
					TextBoxInit(detailOutcom.PriceNoNdsTxt);
					textItem = item;
					textBoxColumn = 3;
					return;
				case 5:
					if(detailOutcomDoc.Guaranty == true) return;	// В гарантийном документе цену изменять нельзя
					textBox = Db.MakeBox(this, item, column);
					TextBoxInit(detailOutcom.PriceTxt);
					textItem = item;
					textBoxColumn = 5;
					return;
				case 2:
					textBox = Db.MakeBox(this, item, column);
					TextBoxInit(detailOutcom.QuontityTxt);
					textItem = item;
					textBoxColumn = 2;
					return;
			}
		}

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
			DbDetailOutcom detailOutcom = (DbDetailOutcom)textItem.Tag;
			text = text.Trim();
			if(e.KeyCode == Keys.Enter)
			{
				switch(textBoxColumn)
				{
					case 3:
						detailOutcom.PriceNoNdsTxt = text;
						if(Db.ShowFaults())
						{
							TextBoxDisposeNotFocus();
							return;
						}
						detailOutcom.SetLVItem(textItem);
						TextBoxDispose();
						//SetSumm(listView1);
						return;
					case 4:
						detailOutcom.NdsTxt = text;
						if(Db.ShowFaults())
						{
							TextBoxDisposeNotFocus();
							return;
						}
						detailOutcom.SetLVItem(textItem);
						TextBoxDispose();
						//SetSumm(listView1);
						return;
					case 5:
						detailOutcom.PriceTxt = text;
						if(Db.ShowFaults())
						{
							TextBoxDisposeNotFocus();
							return;
						}
						detailOutcom.SetLVItem(textItem);
						TextBoxDispose();
						//SetSumm(listView1);
						return;
					case 2:
						detailOutcom.QuontityTxt = text;
						if(Db.ShowFaults())
						{
							TextBoxDisposeNotFocus();
							return;
						}
						detailOutcom.SetLVItem(textItem);
						TextBoxDispose();
						//SetSumm(listView1);
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

		private void buttonReturn_Click(object sender, System.EventArgs e)
		{
			// Возврат полученой ранее детали - удаление из требования
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			DbDetailOutcom element = (DbDetailOutcom)item.Tag;
			if(element == null) return;

			if(element.Exists)
			{
				element.Deleted = true;
				element.SetLVItem(item);

			}
			else
			{
				listView1.Items.Remove(item);
				SetListIndexes(listView1);
			}
		}

		private void checkBoxAutoNumber_CheckedChanged(object sender, System.EventArgs e)
		{
			// Переключение Автоматическая/Ручная нумерация
			if(checkBoxAutoNumber.Checked == true)
			{
				// Автоматическая нумерация
				label1.Hide();
				label5.Hide();
				textBoxNumber.Hide();
				dateTimePicker1.Hide();
			}
			else
			{
				// Ручная нумерация
				dateTimePicker1.Value	= DateTime.Now;
				textBoxNumber.Text		= "0";
				label1.Show();
				label5.Show();
				textBoxNumber.Show();
				dateTimePicker1.Show();
			}
		}

		private void buttonSetDetailIncom_Click(object sender, System.EventArgs e)
		{
			// Устанавливаем приход для позиции без приходника
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			DbDetailOutcom element = (DbDetailOutcom)item.Tag;
			if(element == null) return;
			DbDetailStorage detailStorage = element.DetailStorage;
			if(detailStorage == null) return;
			FormDetailIncomListDetailed dialog = new FormDetailIncomListDetailed(detailStorage, null);
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			DbDetailOutcom newDetailOutcom = dialog.SelectedDeteilOutcom;
			element.DetailIncom = newDetailOutcom.DetailIncom;
			element.SetPrice(detailOutcomDoc.Guaranty);
			element.SetLVItem(item);
		}

		private void checkBoxGuaranty_CheckedChanged(object sender, System.EventArgs e)
		{
			// Изменение гарантийного статуса требования
			detailOutcomDoc.Guaranty = checkBoxGuaranty.Checked;
			SetPrices(listView1);
		}
	}
}
