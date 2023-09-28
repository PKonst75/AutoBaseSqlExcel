using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormListSell.
	/// </summary>
	public class FormListSell : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.Button button_new;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.DateTimePicker dateTimePicker_start;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.DateTimePicker dateTimePicker_stop;
		private System.Windows.Forms.GroupBox groupBox_search;
		private System.Windows.Forms.CheckBox checkBox_timeon;
		private System.Windows.Forms.TextBox textBox_model;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBox_variant;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textBox_color;
		private System.Windows.Forms.Button button_update;
		private System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;
        private MenuItem menuItem5;
        private MenuItem menuItem6;
        private MenuItem menuItem7;
        private MenuItem menuItem8;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FormListSell()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			// Первоначальная настройка параметров поиска
			DateTime now = DateTime.Now;
			dateTimePicker_start.Value = new DateTime(now.Year, now.Month, 1, 0, 0, 0, 0);
			dateTimePicker_stop.Value = dateTimePicker_start.Value.AddMonths(1);
			dateTimePicker_stop.Value = dateTimePicker_stop.Value.AddMilliseconds(-1);
			checkBox_timeon.Checked = true;
			textBox_model.Tag = (long)0;
			textBox_variant.Tag	= (long)0;
			textBox_color.Tag = (long)0;

			UpdateList();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormListSell));
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.button_new = new System.Windows.Forms.Button();
            this.dateTimePicker_start = new System.Windows.Forms.DateTimePicker();
            this.groupBox_search = new System.Windows.Forms.GroupBox();
            this.textBox_color = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_variant = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_model = new System.Windows.Forms.TextBox();
            this.checkBox_timeon = new System.Windows.Forms.CheckBox();
            this.dateTimePicker_stop = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button_update = new System.Windows.Forms.Button();
            this.contextMenu1 = new System.Windows.Forms.ContextMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.menuItem8 = new System.Windows.Forms.MenuItem();
            this.groupBox_search.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new System.Drawing.Point(8, 152);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(736, 168);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseUp);
            this.listView1.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView1_ColumnClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Дата";
            this.columnHeader1.Width = 92;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Покупатель";
            this.columnHeader2.Width = 181;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Модель";
            this.columnHeader3.Width = 117;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Цвет";
            this.columnHeader4.Width = 80;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Исполнение";
            this.columnHeader5.Width = 110;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "VIN";
            this.columnHeader6.Width = 113;
            // 
            // button_new
            // 
            this.button_new.Image = ((System.Drawing.Image)(resources.GetObject("button_new.Image")));
            this.button_new.Location = new System.Drawing.Point(8, 120);
            this.button_new.Name = "button_new";
            this.button_new.Size = new System.Drawing.Size(24, 23);
            this.button_new.TabIndex = 1;
            this.button_new.Click += new System.EventHandler(this.button_new_Click);
            // 
            // dateTimePicker_start
            // 
            this.dateTimePicker_start.Location = new System.Drawing.Point(112, 16);
            this.dateTimePicker_start.Name = "dateTimePicker_start";
            this.dateTimePicker_start.Size = new System.Drawing.Size(152, 23);
            this.dateTimePicker_start.TabIndex = 2;
            // 
            // groupBox_search
            // 
            this.groupBox_search.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox_search.Controls.Add(this.textBox_color);
            this.groupBox_search.Controls.Add(this.label5);
            this.groupBox_search.Controls.Add(this.textBox_variant);
            this.groupBox_search.Controls.Add(this.label4);
            this.groupBox_search.Controls.Add(this.label3);
            this.groupBox_search.Controls.Add(this.textBox_model);
            this.groupBox_search.Controls.Add(this.checkBox_timeon);
            this.groupBox_search.Controls.Add(this.dateTimePicker_stop);
            this.groupBox_search.Controls.Add(this.label2);
            this.groupBox_search.Controls.Add(this.label1);
            this.groupBox_search.Controls.Add(this.dateTimePicker_start);
            this.groupBox_search.Location = new System.Drawing.Point(136, 0);
            this.groupBox_search.Name = "groupBox_search";
            this.groupBox_search.Size = new System.Drawing.Size(608, 144);
            this.groupBox_search.TabIndex = 3;
            this.groupBox_search.TabStop = false;
            this.groupBox_search.Text = "Поиск";
            // 
            // textBox_color
            // 
            this.textBox_color.Location = new System.Drawing.Point(384, 64);
            this.textBox_color.Name = "textBox_color";
            this.textBox_color.Size = new System.Drawing.Size(208, 23);
            this.textBox_color.TabIndex = 12;
            this.textBox_color.DoubleClick += new System.EventHandler(this.textBox_color_DoubleClick);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(280, 64);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 23);
            this.label5.TabIndex = 11;
            this.label5.Text = "Цвет";
            // 
            // textBox_variant
            // 
            this.textBox_variant.Location = new System.Drawing.Point(384, 40);
            this.textBox_variant.Name = "textBox_variant";
            this.textBox_variant.Size = new System.Drawing.Size(208, 23);
            this.textBox_variant.TabIndex = 10;
            this.textBox_variant.DoubleClick += new System.EventHandler(this.textBox_variant_DoubleClick);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(280, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 23);
            this.label4.TabIndex = 9;
            this.label4.Text = "Исполнение";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(280, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 23);
            this.label3.TabIndex = 8;
            this.label3.Text = "Модель";
            // 
            // textBox_model
            // 
            this.textBox_model.Location = new System.Drawing.Point(384, 16);
            this.textBox_model.Name = "textBox_model";
            this.textBox_model.Size = new System.Drawing.Size(208, 23);
            this.textBox_model.TabIndex = 7;
            this.textBox_model.DoubleClick += new System.EventHandler(this.textBox_model_DoubleClick);
            // 
            // checkBox_timeon
            // 
            this.checkBox_timeon.Location = new System.Drawing.Point(8, 72);
            this.checkBox_timeon.Name = "checkBox_timeon";
            this.checkBox_timeon.Size = new System.Drawing.Size(160, 24);
            this.checkBox_timeon.TabIndex = 6;
            this.checkBox_timeon.Text = "Включить интервал";
            // 
            // dateTimePicker_stop
            // 
            this.dateTimePicker_stop.Location = new System.Drawing.Point(112, 40);
            this.dateTimePicker_stop.Name = "dateTimePicker_stop";
            this.dateTimePicker_stop.Size = new System.Drawing.Size(152, 23);
            this.dateTimePicker_stop.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 23);
            this.label2.TabIndex = 4;
            this.label2.Text = "Интервал до";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 3;
            this.label1.Text = "Интервал от";
            // 
            // button_update
            // 
            this.button_update.Image = ((System.Drawing.Image)(resources.GetObject("button_update.Image")));
            this.button_update.Location = new System.Drawing.Point(32, 120);
            this.button_update.Name = "button_update";
            this.button_update.Size = new System.Drawing.Size(24, 23);
            this.button_update.TabIndex = 4;
            this.button_update.Click += new System.EventHandler(this.button_update_Click);
            // 
            // contextMenu1
            // 
            this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.menuItem4,
            this.menuItem3,
            this.menuItem2,
            this.menuItem5,
            this.menuItem6});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.Text = "Дополнительная информация";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 1;
            this.menuItem4.Text = "Служебная информация";
            this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 2;
            this.menuItem3.Text = "-";
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 3;
            this.menuItem2.Text = "Смена даты продажи";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 4;
            this.menuItem5.Text = "-";
            // 
            // menuItem6
            // 
            this.menuItem6.Index = 5;
            this.menuItem6.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem7,
            this.menuItem8});
            this.menuItem6.Text = "Договора";
            // 
            // menuItem7
            // 
            this.menuItem7.Index = 0;
            this.menuItem7.Text = "ДКП Джи-Эм Утилизация";
            this.menuItem7.Click += new System.EventHandler(this.menuItem7_Click);
            // 
            // menuItem8
            // 
            this.menuItem8.Index = 1;
            this.menuItem8.Text = "ДКП Джи-Эм Трейд-ИН";
            this.menuItem8.Click += new System.EventHandler(this.menuItem8_Click);
            // 
            // FormListSell
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
            this.ClientSize = new System.Drawing.Size(752, 333);
            this.Controls.Add(this.button_update);
            this.Controls.Add(this.groupBox_search);
            this.Controls.Add(this.button_new);
            this.Controls.Add(this.listView1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "FormListSell";
            this.Text = "Продажи";
            this.groupBox_search.ResumeLayout(false);
            this.groupBox_search.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		private void button_new_Click(object sender, System.EventArgs e)
		{
			// Новая продажа
			FormAutoSell dialog = new FormAutoSell();
			dialog.SetConnection(listView1);
			dialog.ShowDialog();
		}

		private void textBox_model_DoubleClick(object sender, System.EventArgs e)
		{
			// Выбор модели для поиска
			ListView list = new ListView();
			DbSqlModel.SelectInListAll(list);
			FormSelectionList form = new FormSelectionList(list, "ВСЕ МОДЕЛИ");
			if(form.ShowDialog() != DialogResult.OK) return;
			textBox_model.Tag = (long)form.SelectedCode;
			textBox_model.Text	= form.SelectedText;
			// Очищаем исполнение и цвет
			textBox_variant.Tag		= (long)0;
			textBox_variant.Text	= "";
			textBox_color.Tag		= (long)0;
			textBox_color.Text		= "";
		}

		private void textBox_variant_DoubleClick(object sender, System.EventArgs e)
		{
			// Выбор исполнения модели
			// Проверяем выбрана ли модель
			if(textBox_model.Tag == null)
			{
				MessageBox.Show("Не выбрана модель");
				return;
			}
			long code_model = (long)textBox_model.Tag;
			if(code_model == 0)
			{
				MessageBox.Show("Не выбрана модель");
				return;
			}
			ListView list = new ListView();
			DbSqlVariant.SelectInListAll(list, code_model);
			FormSelectionList form = new FormSelectionList(list, "ВСЕ ИСПОЛНЕНИЯ");
			if(form.ShowDialog() != DialogResult.OK) return;
			textBox_variant.Tag = (long)form.SelectedCode;
			textBox_variant.Text	= form.SelectedText;
		}

		private void textBox_color_DoubleClick(object sender, System.EventArgs e)
		{
			// Выбор цвета модели
			// Проверяем выбрана ли модель
			if(textBox_model.Tag == null)
			{
				MessageBox.Show("Не выбрана модель");
				return;
			}
			long code_model = (long)textBox_model.Tag;
			if(code_model == 0)
			{
				MessageBox.Show("Не выбрана модель");
				return;
			}
			ListView list = new ListView();
			DbSqlColor.SelectInListAll(list, code_model);
			FormSelectionList form = new FormSelectionList(list, "ВСЕ ЦВЕТА");
			if(form.ShowDialog() != DialogResult.OK) return;
			textBox_color.Tag = (long)form.SelectedCode;
			textBox_color.Text	= form.SelectedText;
		}

		private void button_update_Click(object sender, System.EventArgs e)
		{
			UpdateList();
		}

		private void UpdateList()
		{
			// Обновляем список, используя параметры поиска
			DbSqlAutoSell.SearchMask mask;
			DateTime date;
			mask.timeon		= checkBox_timeon.Checked;
			date	= dateTimePicker_start.Value;
			date	= new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 0);
			mask.date_start	= date;
			date	= dateTimePicker_stop.Value;
			date	= new DateTime(date.Year, date.Month, date.Day, 23, 59, 59, 99);
			mask.date_stop	= date;
			mask.code_model		= (long)textBox_model.Tag;
			mask.code_variant	= (long)textBox_variant.Tag;
			mask.code_color		= (long)textBox_color.Tag;

			listView1.Items.Clear();
			DbSqlAutoSell.SelectInList(listView1, mask);
		}

		private void listView1_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
		{
			// Дополнительные поиски - по контрагенту и VIN у
			switch(e.Column)
			{
				case 1:
					FormListPartner dialog1 = new FormListPartner(0, null);
					if(dialog1.ShowDialog() != DialogResult.OK) return;
					long code	= dialog1.SelectedCode;
					if(code == 0) return;
					listView1.Items.Clear();
					DbSqlAutoSell.SelectInListPartner(listView1, code);
					break;
				case 5:
					// Запрашиваем VIN
					FormSelectString dialog = new FormSelectString("VIN или Номер кузова", "");
					if(dialog.ShowDialog() != DialogResult.OK) return;
					string pattern = dialog.SelectedTextMask;
					if(pattern.Length == 0) return;
					listView1.Items.Clear();
					DbSqlAutoSell.SelectInListVinBody(listView1, pattern);
					break;
				default:
					break;
			}
		}

		private void menuItem1_Click(object sender, System.EventArgs e)
		{
			// Вызов окна настройки дополнительной информации
			ListViewItem item;
			long code;
			if(listView1.SelectedItems == null) return;
			item = listView1.SelectedItems[0];
			if(item == null) return;
			code = (long)item.Tag;
			if(code == 0) return;
			// Пробуем найти информацию
			UIF_SellInfo dialog = new UIF_SellInfo(code);
			dialog.ShowDialog();
		}

		private void listView1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			// Вызов контекстного меню
			if(e.Button == MouseButtons.Right)
			{
				// Показ меню
				contextMenu1.Show(listView1, new Point(e.X, e.Y));
			}
		}

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			// Смена даты продажи
			string login = Form1.currentLogin.ToLower();
			if (login != "админ")
			{
				return;
			}
			ListViewItem item;
			long code;
			if(listView1.SelectedItems == null) return;
			item = listView1.SelectedItems[0];
			if(item == null) return;
			code = (long)item.Tag;
			if(code == 0) return;

			FormSelectDate form = new FormSelectDate();
			if (form.ShowDialog() != DialogResult.OK) return;
			DateTime date = form.SelectedDate;

			if (DbSqlAutoSell.SetSellDate(code, date) == false) return;
			MessageBox.Show("Дата продажи изменена. Проверьте результат");
		}

		private void menuItem4_Click(object sender, System.EventArgs e)
		{
			// Вызов диалога служебной информации
			ListViewItem item;
			long code;
			if(listView1.SelectedItems == null) return;
			item = listView1.SelectedItems[0];
			if(item == null) return;
			code = (long)item.Tag;
			if(code == 0) return;
			// Пробуем найти информацию
			UIF_AutoSellServ dialog = new UIF_AutoSellServ(code);
			dialog.ShowDialog();
		}

        private void menuItem7_Click(object sender, EventArgs e)
        {
            // Печать договора купли продажи по утилизации Джи-Эм АВТОВАЗ
            // Вызов диалога служебной информации
            ListViewItem item;
            long code;
            if (listView1.SelectedItems == null) return;
            item = listView1.SelectedItems[0];
            if (item == null) return;
            code = (long)item.Tag;
            if (code == 0) return;

            // Вызов процедуры открытия документа WORD
            DbWordUtil word = new DbWordUtil(code);
            word.OpenUtilDkp("C:\\temp\\ДКПУТИЛЬДЖИЭМ.doc");
        }

        private void menuItem8_Click(object sender, EventArgs e)
        {
            // Печать договора купли продажи по трейд-ин Джи-Эм АВТОВАЗ
            // Вызов диалога служебной информации
            ListViewItem item;
            long code;
            if (listView1.SelectedItems == null) return;
            item = listView1.SelectedItems[0];
            if (item == null) return;
            code = (long)item.Tag;
            if (code == 0) return;

            // Вызов процедуры открытия документа WORD
            DbWordUtil word = new DbWordUtil(code);
            word.OpenTinDkp("C:\\temp\\ДКПТИДЖИЭМ.doc");
        }
	}
}
