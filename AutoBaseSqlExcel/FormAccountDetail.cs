using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormAccountDetail.
	/// </summary>
	public class FormAccountDetail : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBoxClient;
		private System.Windows.Forms.TextBox textBoxSeller;
		private System.Windows.Forms.Button buttonSelectClient;
		private System.Windows.Forms.Button buttonSelectSeller;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBoxComment;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBoxStaff;
		private System.Windows.Forms.Button buttonSelectStaff;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.TextBox textBoxSumm;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button buttonOk;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		DbAccountDetail accountDetail;

		private System.Windows.Forms.TextBox textBox = null;
		private ListViewItem textItem = null;
		private int textBoxColumn;

		public FormAccountDetail(DbAccountDetail source)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			if(source == null)
			{
				accountDetail = new DbAccountDetail();
			}
			else
			{
				accountDetail = new DbAccountDetail(source);
				DbAccountDetailItem.FillList(listView1, accountDetail);
			}
			textBoxStaff.Text = accountDetail.StaffTitle;
			textBoxSeller.Text = accountDetail.SellerTitle;
			textBoxClient.Text = accountDetail.ClientTitle;
			textBoxComment.Text = accountDetail.Comment;
			SetSumm(listView1);
			SetListIndexes(listView1);
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormAccountDetail));
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.buttonSelectStaff = new System.Windows.Forms.Button();
			this.textBoxStaff = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textBoxComment = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.buttonSelectSeller = new System.Windows.Forms.Button();
			this.buttonSelectClient = new System.Windows.Forms.Button();
			this.textBoxSeller = new System.Windows.Forms.TextBox();
			this.textBoxClient = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.button1 = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.textBoxSumm = new System.Windows.Forms.TextBox();
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
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
			this.tabControl1.Location = new System.Drawing.Point(8, 8);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(520, 232);
			this.tabControl1.TabIndex = 0;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.AddRange(new System.Windows.Forms.Control[] {
																				   this.buttonSelectStaff,
																				   this.textBoxStaff,
																				   this.label4,
																				   this.textBoxComment,
																				   this.label3,
																				   this.buttonSelectSeller,
																				   this.buttonSelectClient,
																				   this.textBoxSeller,
																				   this.textBoxClient,
																				   this.label2,
																				   this.label1});
			this.tabPage1.Location = new System.Drawing.Point(4, 25);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new System.Drawing.Size(512, 203);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Шапка";
			// 
			// buttonSelectStaff
			// 
			this.buttonSelectStaff.Location = new System.Drawing.Point(448, 96);
			this.buttonSelectStaff.Name = "buttonSelectStaff";
			this.buttonSelectStaff.Size = new System.Drawing.Size(24, 23);
			this.buttonSelectStaff.TabIndex = 10;
			this.buttonSelectStaff.Text = "...";
			this.buttonSelectStaff.Click += new System.EventHandler(this.buttonSelectStaff_Click);
			// 
			// textBoxStaff
			// 
			this.textBoxStaff.Location = new System.Drawing.Point(104, 96);
			this.textBoxStaff.Name = "textBoxStaff";
			this.textBoxStaff.ReadOnly = true;
			this.textBoxStaff.Size = new System.Drawing.Size(344, 23);
			this.textBoxStaff.TabIndex = 9;
			this.textBoxStaff.Text = "textBox1";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 96);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(88, 23);
			this.label4.TabIndex = 8;
			this.label4.Text = "Выписал";
			// 
			// textBoxComment
			// 
			this.textBoxComment.Location = new System.Drawing.Point(16, 168);
			this.textBoxComment.Name = "textBoxComment";
			this.textBoxComment.Size = new System.Drawing.Size(464, 23);
			this.textBoxComment.TabIndex = 7;
			this.textBoxComment.Text = "textBox1";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(16, 144);
			this.label3.Name = "label3";
			this.label3.TabIndex = 6;
			this.label3.Text = "Примечание";
			// 
			// buttonSelectSeller
			// 
			this.buttonSelectSeller.Location = new System.Drawing.Point(448, 56);
			this.buttonSelectSeller.Name = "buttonSelectSeller";
			this.buttonSelectSeller.Size = new System.Drawing.Size(24, 23);
			this.buttonSelectSeller.TabIndex = 5;
			this.buttonSelectSeller.Text = "...";
			this.buttonSelectSeller.Click += new System.EventHandler(this.buttonSelectSeller_Click);
			// 
			// buttonSelectClient
			// 
			this.buttonSelectClient.Location = new System.Drawing.Point(448, 32);
			this.buttonSelectClient.Name = "buttonSelectClient";
			this.buttonSelectClient.Size = new System.Drawing.Size(24, 23);
			this.buttonSelectClient.TabIndex = 4;
			this.buttonSelectClient.Text = "...";
			this.buttonSelectClient.Click += new System.EventHandler(this.buttonSelectClient_Click);
			// 
			// textBoxSeller
			// 
			this.textBoxSeller.Location = new System.Drawing.Point(104, 56);
			this.textBoxSeller.Name = "textBoxSeller";
			this.textBoxSeller.ReadOnly = true;
			this.textBoxSeller.Size = new System.Drawing.Size(344, 23);
			this.textBoxSeller.TabIndex = 3;
			this.textBoxSeller.Text = "textBox2";
			// 
			// textBoxClient
			// 
			this.textBoxClient.Location = new System.Drawing.Point(104, 32);
			this.textBoxClient.Name = "textBoxClient";
			this.textBoxClient.ReadOnly = true;
			this.textBoxClient.Size = new System.Drawing.Size(344, 23);
			this.textBoxClient.TabIndex = 2;
			this.textBoxClient.Text = "textBox1";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 56);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(96, 23);
			this.label2.TabIndex = 1;
			this.label2.Text = "Продавец";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 32);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(96, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "Покупатель";
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.AddRange(new System.Windows.Forms.Control[] {
																				   this.button1,
																				   this.label5,
																				   this.textBoxSumm,
																				   this.listView1});
			this.tabPage2.Location = new System.Drawing.Point(4, 25);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Size = new System.Drawing.Size(512, 203);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Позиции";
			// 
			// button1
			// 
			this.button1.Image = ((System.Drawing.Bitmap)(resources.GetObject("button1.Image")));
			this.button1.Location = new System.Drawing.Point(8, 8);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(24, 23);
			this.button1.TabIndex = 3;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// label5
			// 
			this.label5.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			this.label5.Location = new System.Drawing.Point(312, 176);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(56, 23);
			this.label5.TabIndex = 2;
			this.label5.Text = "ИТОГО:";
			// 
			// textBoxSumm
			// 
			this.textBoxSumm.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			this.textBoxSumm.Location = new System.Drawing.Point(376, 176);
			this.textBoxSumm.Name = "textBoxSumm";
			this.textBoxSumm.TabIndex = 1;
			this.textBoxSumm.Text = "textBox1";
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
			this.listView1.GridLines = true;
			this.listView1.Location = new System.Drawing.Point(8, 32);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(496, 136);
			this.listView1.TabIndex = 0;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.DoubleClick += new EventHandler(this.listView1_DoubleClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "№ п/п";
			this.columnHeader1.Width = 20;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Наименование";
			this.columnHeader2.Width = 200;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Количество";
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Цена (С НДС)";
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "НДС";
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "Сумма";
			// 
			// buttonOk
			// 
			this.buttonOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.buttonOk.Location = new System.Drawing.Point(192, 248);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.TabIndex = 1;
			this.buttonOk.Text = "ОК";
			this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
			// 
			// FormAccountDetail
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.ClientSize = new System.Drawing.Size(536, 273);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.buttonOk,
																		  this.tabControl1});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.Name = "FormAccountDetail";
			this.Text = "Счет";
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonSelectClient_Click(object sender, System.EventArgs e)
		{
			// Установка покупателя
			FormPartnerList dialog = new FormPartnerList();
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			accountDetail.Client = dialog.Partner;
			textBoxClient.Text = accountDetail.ClientTitle;
		}

		private void buttonSelectSeller_Click(object sender, System.EventArgs e)
		{
			// Установка продавца
			FormPartnerList dialog = new FormPartnerList();
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			accountDetail.Seller = dialog.Partner;
			textBoxSeller.Text = accountDetail.SellerTitle;
		}

		private void buttonSelectStaff_Click(object sender, System.EventArgs e)
		{
			// Установка менеджера
			FormStaffList dialog = new FormStaffList();
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			accountDetail.Staff = dialog.SelectedStaff;
			textBoxStaff.Text = accountDetail.StaffTitle;
		}

		private void buttonOk_Click(object sender, System.EventArgs e)
		{
			// Установка оставшихся данных
			accountDetail.Comment = textBoxComment.Text;
			accountDetail.IsValid();

			// Проверяем, не произошло ли ошибки
			if(Db.ShowFaults()) return;

			// Добавляем в базу
			if(accountDetail.Update(listView1) != true) return;
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			// Добавление новой позиции в счет
			FormDetailStorageList dialog = new FormDetailStorageList(listView1, 3, null, null);
			dialog.ShowDialog(this);
			SetListIndexes(listView1);
			SetSumm(listView1);
		}

		public DbAccountDetail Account
		{
			get{return accountDetail;}
		}

		public void SetSumm(ListView list)
		{
			double summ = 0.0;
			foreach(ListViewItem item in list.Items)
			{
				DbAccountDetailItem element = (DbAccountDetailItem)item.Tag;
				if(element != null)
				{
					summ += element.Summ;
				}
			}
			textBoxSumm.Text = Db.CachToTxt(summ);
			accountDetail.Summ = (float)summ;
		}

		protected void SetListIndexes(ListView list)
		{
			foreach(ListViewItem item in list.Items)
			{
				item.Text = (item.Index + 1).ToString();
			}
		}

		#region Организация изменения прямо в листе

		private void listView1_DoubleClick(object sender, EventArgs e)
		{
			// Необходимо определить элемент и колонку, на которой кликнули
			int column = Db.GetColumnPosition(listView1);
			ListViewItem item = Db.GetItemPosition(listView1);
			if(item == null) return;
			DbAccountDetailItem element = (DbAccountDetailItem)item.Tag;
			if(element == null) return;

			switch(column)
			{
				case 2:
					textBox = Db.MakeBox(this, item, column);
					TextBoxInit(element.QuontityTxt);
					textItem = item;
					textBoxColumn = 2;
					return;
				case 3:
					textBox = Db.MakeBox(this, item, column);
					TextBoxInit(element.PriceTxt);
					textItem = item;
					textBoxColumn = 3;
					return;
				case 4:
					textBox = Db.MakeBox(this, item, column);
					TextBoxInit(element.NdsTxt);
					textItem = item;
					textBoxColumn = 4;
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
			DbAccountDetailItem element = (DbAccountDetailItem)textItem.Tag;
			text = text.Trim();
			if(e.KeyCode == Keys.Enter)
			{
				switch(textBoxColumn)
				{
					case 2:
						element.QuontityTxt= text;
						if(Db.ShowFaults())
						{
							TextBoxDisposeNotFocus();
							return;
						}
						element.SetLVItem(textItem);
						TextBoxDispose();
						SetSumm(listView1);
						return;
					case 3:
						element.PriceTxt = text;
						if(Db.ShowFaults())
						{
							TextBoxDisposeNotFocus();
							return;
						}
						element.SetLVItem(textItem);
						TextBoxDispose();
						SetSumm(listView1);
						return;
					case 4:
						element.NdsTxt = text;
						if(Db.ShowFaults())
						{
							TextBoxDisposeNotFocus();
							return;
						}
						element.SetLVItem(textItem);
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

	}
}
