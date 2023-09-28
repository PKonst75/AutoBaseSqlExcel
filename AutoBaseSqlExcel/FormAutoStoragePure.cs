using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormAutoStoragePure.
	/// </summary>
	public class FormAutoStoragePure : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.Button button_have;
		private System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.Button button_print_storage;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox checkBox_add;
		private System.Windows.Forms.TextBox textBox_model;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBox_variant;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBox_color;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.TextBox textBox_vin;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.ColumnHeader columnHeader8;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.MenuItem menuItem8;
		private System.Windows.Forms.ListView listView_complect;
		private System.Windows.Forms.ColumnHeader columnHeader9;
		private System.Windows.Forms.Button button_set_option_find;
		private System.Windows.Forms.Button button_remove_option_find;
		private System.Windows.Forms.MenuItem menuItem9;
		private System.Windows.Forms.ColumnHeader columnHeader10;
		private System.Windows.Forms.TextBox textBox_year;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ColumnHeader columnHeader11;
        private ColumnHeader columnHeader12;
        private MenuItem menuItem10;
        private MenuItem menuItem11;
        private MenuItem menuItem12;
        private ColumnHeader columnHeader13;
        private MenuItem menuItem13;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FormAutoStoragePure()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			// Заполняем лист комплектаций
			DbSqlAutoOptions.SelectInListOptionFind(listView_complect);
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAutoStoragePure));
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader10 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader11 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader12 = new System.Windows.Forms.ColumnHeader();
            this.button_have = new System.Windows.Forms.Button();
            this.contextMenu1 = new System.Windows.Forms.ContextMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.menuItem8 = new System.Windows.Forms.MenuItem();
            this.menuItem9 = new System.Windows.Forms.MenuItem();
            this.menuItem10 = new System.Windows.Forms.MenuItem();
            this.menuItem11 = new System.Windows.Forms.MenuItem();
            this.menuItem12 = new System.Windows.Forms.MenuItem();
            this.button_print_storage = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_year = new System.Windows.Forms.TextBox();
            this.listView_complect = new System.Windows.Forms.ListView();
            this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_vin = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_color = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_variant = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_model = new System.Windows.Forms.TextBox();
            this.checkBox_add = new System.Windows.Forms.CheckBox();
            this.button_set_option_find = new System.Windows.Forms.Button();
            this.button_remove_option_find = new System.Windows.Forms.Button();
            this.columnHeader13 = new System.Windows.Forms.ColumnHeader();
            this.menuItem13 = new System.Windows.Forms.MenuItem();
            this.groupBox1.SuspendLayout();
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
            this.columnHeader6,
            this.columnHeader10,
            this.columnHeader7,
            this.columnHeader13,
            this.columnHeader8,
            this.columnHeader11,
            this.columnHeader12});
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(8, 201);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(1243, 170);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseUp);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Дата";
            this.columnHeader1.Width = 87;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Модель";
            this.columnHeader2.Width = 145;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Вариант";
            this.columnHeader3.Width = 80;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Резерв";
            this.columnHeader4.Width = 155;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Цвет";
            this.columnHeader5.Width = 135;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Кузов";
            this.columnHeader6.Width = 140;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Год";
            this.columnHeader10.Width = 80;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Цена";
            this.columnHeader7.Width = 100;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Допы";
            this.columnHeader8.Width = 85;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "Сумма";
            this.columnHeader11.Width = 85;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "ПТС";
            this.columnHeader12.Width = 40;

            // columnHeader13
            // 
            //this.columnHeader13.DisplayIndex = 11;
            this.columnHeader13.Text = "За Цвет";
            this.columnHeader13.Width = 72;
            // 
            // button_have
            // 
            this.button_have.Location = new System.Drawing.Point(8, 8);
            this.button_have.Name = "button_have";
            this.button_have.Size = new System.Drawing.Size(75, 23);
            this.button_have.TabIndex = 1;
            this.button_have.Text = "Наличие";
            this.button_have.Click += new System.EventHandler(this.button_have_Click);
            // 
            // contextMenu1
            // 
            this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.menuItem2,
            this.menuItem5,
            this.menuItem8,
            this.menuItem9,
            this.menuItem10});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.Text = "Отписать со склада";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 1;
            this.menuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem3,
            this.menuItem4});
            this.menuItem2.Text = "Резервы";
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 0;
            this.menuItem3.Text = "Добавить";
            this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 1;
            this.menuItem4.Text = "Снять";
            this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 2;
            this.menuItem5.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem6,
            this.menuItem7,
            this.menuItem13});
            this.menuItem5.Text = "Прайс-лист";
            // 
            // menuItem6
            // 
            this.menuItem6.Index = 0;
            this.menuItem6.Text = "Добавить Модель/Вариант";
            this.menuItem6.Click += new System.EventHandler(this.menuItem6_Click);
            // 
            // menuItem7
            // 
            this.menuItem7.Index = 1;
            this.menuItem7.Text = "Изменить цену";
            this.menuItem7.Click += new System.EventHandler(this.menuItem7_Click);
            // 
            // menuItem8
            // 
            this.menuItem8.Index = 3;
            this.menuItem8.Text = "Показать допы";
            this.menuItem8.Click += new System.EventHandler(this.menuItem8_Click);
            // 
            // menuItem9
            // 
            this.menuItem9.Index = 4;
            this.menuItem9.Text = "Показать комплектацию";
            this.menuItem9.Click += new System.EventHandler(this.menuItem9_Click);
            // 
            // menuItem10
            // 
            this.menuItem10.Index = 5;
            this.menuItem10.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem11,
            this.menuItem12});
            this.menuItem10.Text = "ПТС";
            // 
            // menuItem11
            // 
            this.menuItem11.Index = 0;
            this.menuItem11.Text = "Есть ПТС";
            this.menuItem11.Click += new System.EventHandler(this.menuItem11_Click);
            // 
            // menuItem12
            // 
            this.menuItem12.Index = 1;
            this.menuItem12.Text = "Нет ПТС";
            this.menuItem12.Click += new System.EventHandler(this.menuItem12_Click);
            // 
            // button_print_storage
            // 
            this.button_print_storage.Location = new System.Drawing.Point(824, 8);
            this.button_print_storage.Name = "button_print_storage";
            this.button_print_storage.Size = new System.Drawing.Size(75, 23);
            this.button_print_storage.TabIndex = 2;
            this.button_print_storage.Text = "Печать";
            this.button_print_storage.Click += new System.EventHandler(this.button_print_storage_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.textBox_year);
            this.groupBox1.Controls.Add(this.listView_complect);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textBox_vin);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBox_color);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBox_variant);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBox_model);
            this.groupBox1.Controls.Add(this.checkBox_add);
            this.groupBox1.Location = new System.Drawing.Point(96, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(680, 184);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Условия поиска";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(272, 144);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 23);
            this.label5.TabIndex = 11;
            this.label5.Text = "ГОД";
            // 
            // textBox_year
            // 
            this.textBox_year.Location = new System.Drawing.Point(144, 144);
            this.textBox_year.Name = "textBox_year";
            this.textBox_year.Size = new System.Drawing.Size(120, 23);
            this.textBox_year.TabIndex = 10;
            // 
            // listView_complect
            // 
            this.listView_complect.CheckBoxes = true;
            this.listView_complect.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader9});
            this.listView_complect.Location = new System.Drawing.Point(352, 16);
            this.listView_complect.Name = "listView_complect";
            this.listView_complect.Size = new System.Drawing.Size(320, 136);
            this.listView_complect.TabIndex = 9;
            this.listView_complect.UseCompatibleStateImageBehavior = false;
            this.listView_complect.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Комплектация";
            this.columnHeader9.Width = 307;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(272, 112);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 23);
            this.label4.TabIndex = 8;
            this.label4.Text = "VIN";
            // 
            // textBox_vin
            // 
            this.textBox_vin.Location = new System.Drawing.Point(144, 112);
            this.textBox_vin.Name = "textBox_vin";
            this.textBox_vin.Size = new System.Drawing.Size(120, 23);
            this.textBox_vin.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(272, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 24);
            this.label3.TabIndex = 6;
            this.label3.Text = "Цвет";
            // 
            // textBox_color
            // 
            this.textBox_color.Location = new System.Drawing.Point(144, 80);
            this.textBox_color.Name = "textBox_color";
            this.textBox_color.Size = new System.Drawing.Size(120, 23);
            this.textBox_color.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(272, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 23);
            this.label2.TabIndex = 4;
            this.label2.Text = "Вариант";
            // 
            // textBox_variant
            // 
            this.textBox_variant.Location = new System.Drawing.Point(144, 48);
            this.textBox_variant.Name = "textBox_variant";
            this.textBox_variant.Size = new System.Drawing.Size(120, 23);
            this.textBox_variant.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(272, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 23);
            this.label1.TabIndex = 2;
            this.label1.Text = "Модель";
            // 
            // textBox_model
            // 
            this.textBox_model.Location = new System.Drawing.Point(144, 16);
            this.textBox_model.Name = "textBox_model";
            this.textBox_model.Size = new System.Drawing.Size(120, 23);
            this.textBox_model.TabIndex = 1;
            // 
            // checkBox_add
            // 
            this.checkBox_add.Location = new System.Drawing.Point(8, 24);
            this.checkBox_add.Name = "checkBox_add";
            this.checkBox_add.Size = new System.Drawing.Size(120, 24);
            this.checkBox_add.TabIndex = 0;
            this.checkBox_add.Text = "Считать допы";
            // 
            // button_set_option_find
            // 
            this.button_set_option_find.Image = ((System.Drawing.Image)(resources.GetObject("button_set_option_find.Image")));
            this.button_set_option_find.Location = new System.Drawing.Point(784, 16);
            this.button_set_option_find.Name = "button_set_option_find";
            this.button_set_option_find.Size = new System.Drawing.Size(24, 23);
            this.button_set_option_find.TabIndex = 4;
            this.button_set_option_find.Click += new System.EventHandler(this.button_set_option_find_Click);
            // 
            // button_remove_option_find
            // 
            this.button_remove_option_find.Image = ((System.Drawing.Image)(resources.GetObject("button_remove_option_find.Image")));
            this.button_remove_option_find.Location = new System.Drawing.Point(784, 40);
            this.button_remove_option_find.Name = "button_remove_option_find";
            this.button_remove_option_find.Size = new System.Drawing.Size(24, 23);
            this.button_remove_option_find.TabIndex = 5;
            this.button_remove_option_find.Click += new System.EventHandler(this.button_remove_option_find_Click);
            // 
          
            // 
            // menuItem13
            // 
            this.menuItem13.Index = 2;
            this.menuItem13.Text = "Установить доплату за цвет";
            this.menuItem13.Click += new System.EventHandler(this.menuItem13_Click);
            // 
            // FormAutoStoragePure
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
            this.ClientSize = new System.Drawing.Size(1259, 383);
            this.Controls.Add(this.button_remove_option_find);
            this.Controls.Add(this.button_set_option_find);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button_print_storage);
            this.Controls.Add(this.button_have);
            this.Controls.Add(this.listView1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "FormAutoStoragePure";
            this.Text = "Склад автомобилей";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		private void button_have_Click(object sender, System.EventArgs e)
		{
			// Создаем список автомобилей в наличии
			listView1.Items.Clear();
			ArrayList autos = new ArrayList();
			string model_mask = textBox_model.Text.Trim();
			string variant_mask = textBox_variant.Text.Trim();
			string color_mask = textBox_color.Text.Trim();
			string vin_mask = textBox_vin.Text.Trim();
			string year_mask_tmp = textBox_year.Text.Trim();
			int year_mask = 0;
			// Обработка года
			if (year_mask_tmp != "")
			{
				try
				{
					year_mask = Convert.ToInt32(year_mask_tmp);
				}
				catch(Exception E)
				{
					year_mask = 0;
				}
			}
			bool mask = false;
			bool option = false;
			long[] options = new long[5];
			int count = 0;

			if (model_mask != "")
			{
				model_mask = "%" + model_mask + "%";
				mask = true;
			}
			if (variant_mask != "")
			{
				variant_mask = "%" + variant_mask + "%";
				mask = true;
			}
			if (color_mask != "")
			{
				color_mask = "%" + color_mask + "%";
				mask = true;
			}
			if (vin_mask != "")
			{
				vin_mask = "%" + vin_mask + "%";
				mask = true;
			}
			if (year_mask != 0)
			{
				mask = true;
			}
			// Параметры поиска по комплектации
			foreach(ListViewItem item in listView_complect.Items)
			{
				if(item.Checked == true)
				{
					mask = true;
					option = true;
					DtAutoOption opt = (DtAutoOption)item.Tag;
					if (opt != null)
					{
						if(count < 5)
						{
							options[count] = opt.code;
							count++;
						}
					}
				}
			}

			if(mask)
				if(option == false)
					DbSqlAuto.SelectInArrayStorageAvaliableMask(autos, model_mask, variant_mask, color_mask, vin_mask, year_mask);
				else
					DbSqlAuto.SelectInArrayStorageAvaliableMaskOptions(autos, model_mask, variant_mask, color_mask, vin_mask, year_mask, options[0], options[1], options[2], options[3], options[4]);
			else
				DbSqlAuto.SelectInArrayStorageAvaliable(autos);

			if(checkBox_add.Checked == false)
			{
				foreach (DtAuto auto in autos)
				{
					ListViewItem lvItem = new ListViewItem();
					auto.SetLVItemStorageV2(lvItem);
					listView1.Items.Add(lvItem);
				}
				return;
			}
			// Анализируем список доп оборудования
			foreach (DtAuto auto in autos)
			{
				float add_summ = DbSqlAuto.SelectAutoAdd((long)auto.GetData("КОД_АВТОМОБИЛЬ"));
				auto.SetData("СУММА_ДОПОВ", add_summ);
				ListViewItem lvItem = new ListViewItem();
				auto.SetLVItemStorageV2(lvItem);
				listView1.Items.Add(lvItem);
			}
		}

		private void menuItem1_Click(object sender, System.EventArgs e)
		{
			// Отписать выбранный автомобиль сос клада
			// Определяем, где щелкнули мышкой
			ListViewItem item = null;
			long code = 0;
			
			item = Db.GetItemSelected(listView1);
			if(item == null) return;
			code = (long)item.Tag;
			if(code == 0) return;
			// Отписываем
			if (DbSqlAuto.UpdateStorageAvaliable(code) == false) return;
			listView1.Items.Remove(item);
		}

		private void listView1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if(e.Button == MouseButtons.Right)
			{
				// На отпускание правой кнопки мышки - всплывающее меню
				// Настройка меню
				// Права пользователя
				// Отключаем все запрещенное
				menuItem1.Enabled = false;
				menuItem3.Enabled = false;
				menuItem4.Enabled = false;
				menuItem6.Enabled = false;
				// Включаем по разрешению
				string login = Form1.currentLogin.ToLower();
				if (login == "админ")
				{
					menuItem1.Enabled = true;
					menuItem3.Enabled = true;
					menuItem4.Enabled = true;
					menuItem6.Enabled = true;
					menuItem7.Enabled = true;
				}
				if (login == "ильиныхю")
				{
					menuItem3.Enabled = true;
					menuItem4.Enabled = true;
					menuItem6.Enabled = true;
					menuItem7.Enabled = true;
				}
				// Показ меню
				contextMenu1.Show(listView1, new Point(e.X, e.Y));
			}
		}

		private void button_print_storage_Click(object sender, System.EventArgs e)
		{
			DbPrintAutoStorageAvaliable print = new DbPrintAutoStorageAvaliable();
			print.Print();
		}

		private void menuItem3_Click(object sender, System.EventArgs e)
		{
			// Добавить резерв
			// Определяем, где щелкнули мышкой
			ListViewItem item = null;
			long code = 0;
			
			item = Db.GetItemSelected(listView1);
			if(item == null) return;
			code = (long)item.Tag;
			if(code == 0) return;

			// Запрашиваем примечание и дату окончания резерва
			FormSelectDate form = new FormSelectDate();
			if (form.ShowDialog() != DialogResult.OK) return;
			DateTime end_date = form.SelectedDate;
			FormSelectString form1 = new FormSelectString("Комментарий к резерву", "");
			if(form1.ShowDialog() != DialogResult.OK) return;
			string comment = form1.SelectedText;
			// Отписываем
			if (DbSqlAuto.ReserveInsert(code, end_date, comment) == false) return;
			// Перечитываем заново
			DtAuto auto = DbSqlAuto.SelectStorageAvaliableFind(code);
			auto.SetLVItemStorageV2(item);
		}

		private void menuItem4_Click(object sender, System.EventArgs e)
		{
			// Сниманем резерв
			// Определяем, где щелкнули мышкой
			ListViewItem item = null;
			long code = 0;
			
			item = Db.GetItemSelected(listView1);
			if(item == null) return;
			code = (long)item.Tag;
			if(code == 0) return;

			// Запрашиваем примечание к отмене резерва
			FormSelectString form1 = new FormSelectString("Комментарий к отмене резерва", "");
			if(form1.ShowDialog() != DialogResult.OK) return;
			string comment = form1.SelectedText;
			// Отписываем
			if (DbSqlAuto.ReserveRemove(code, comment) == false) return;
			// Перечитываем заново
			DtAuto auto = DbSqlAuto.SelectStorageAvaliableFind(code);
			auto.SetLVItemStorageV2(item);
		}

		private void menuItem6_Click(object sender, System.EventArgs e)
		{
			// Добавляем позицию в прайс-лист
			ListViewItem item = null;
			long code = 0;
			
			item = Db.GetItemSelected(listView1);
			if(item == null) return;
			code = (long)item.Tag;
			if(code == 0) return;

			// Находим автомобиль по коду
			DtAuto auto = DbSqlAuto.Find(code);
			if(auto == null) return;

			long code_model = (long)auto.GetData("ССЫЛКА_КОД_АВТОМОБИЛЬ_МОДЕЛЬ");
			long code_variant = (long)auto.GetData("ССЫЛКА_КОД_АВТОМОБИЛЬ_ИСПОЛНЕНИЕ");
			int year = (int)auto.GetData("ГОД_ВЫПУСК");
			if(code_model == 0 || code_variant == 0) return;

			// Запрашиваем стоимость
			// Запрашиваем примечание к отмене резерва
			FormSelectString form1 = new FormSelectString("Цена по прайсу", "");
			if(form1.ShowDialog() != DialogResult.OK) return;
			float price = form1.SelectedFloat;
			if (price == 0.0F) return;

			// Пробуем добавить позицию в прайс
			if (DbSqlAuto.PriceInsert(code_model, code_variant, year, price) == false) return;

			// Перезачитываем весь список!
			listView1.Items.Clear();
			ArrayList autos = new ArrayList();
			DbSqlAuto.SelectInArrayStorageAvaliable(autos);
			foreach (DtAuto theauto in autos)
			{
				ListViewItem lvItem = new ListViewItem();
				theauto.SetLVItemStorageV2(lvItem);
				listView1.Items.Add(lvItem);
			}
		}

		private void menuItem7_Click(object sender, System.EventArgs e)
		{
			// Изменяем позицию в прайс-листе
			ListViewItem item = null;
			long code = 0;
			
			item = Db.GetItemSelected(listView1);
			if(item == null) return;
			code = (long)item.Tag;
			if(code == 0) return;

			// Находим автомобиль по коду
			DtAuto auto = DbSqlAuto.Find(code);
			if(auto == null) return;

			long code_model = (long)auto.GetData("ССЫЛКА_КОД_АВТОМОБИЛЬ_МОДЕЛЬ");
			long code_variant = (long)auto.GetData("ССЫЛКА_КОД_АВТОМОБИЛЬ_ИСПОЛНЕНИЕ");
			int year = (int)auto.GetData("ГОД_ВЫПУСК");
			if(code_model == 0 || code_variant == 0) return;

			// Запрашиваем стоимость
			FormSelectString form1 = new FormSelectString("Новая цена по прайсу", "");
			if(form1.ShowDialog() != DialogResult.OK) return;
			float price = form1.SelectedFloat;
			if (price == 0.0F) return;

			// Пробуем добавить позицию в прайс
			if (DbSqlAuto.PriceUpdate(code_model, code_variant, year, price) == false) return;

			// Перезачитываем весь список!
			listView1.Items.Clear();
			ArrayList autos = new ArrayList();
			DbSqlAuto.SelectInArrayStorageAvaliable(autos);
			foreach (DtAuto theauto in autos)
			{
				ListViewItem lvItem = new ListViewItem();
				theauto.SetLVItemStorageV2(lvItem);
				listView1.Items.Add(lvItem);
			}
		}

		private void menuItem8_Click(object sender, System.EventArgs e)
		{
			// Показать список дополнительного оборудования на данном автомобиле
			ListViewItem item = null;
			long code = 0;
			item = Db.GetItemSelected(listView1);
			if(item == null) return;
			code = (long)item.Tag;
			if(code == 0) return;
			// Находим автомобиль по коду
			DtAuto auto = DbSqlAuto.Find(code);
			if(auto == null) return;

			// Запускаем процедуру печати
			DbPrintDopList print = new DbPrintDopList(code);
			print.Print();
		}

		private void button_set_option_find_Click(object sender, System.EventArgs e)
		{
			// Добавить комлектацию в список по поиску
			ListView list = new ListView();
			DbSqlAutoOptions.SelectInListOption(list);
			FormSelectionList form = new FormSelectionList(list, false);
			if(form.ShowDialog() != DialogResult.OK) return;
			object o = form.SelectedCodeObject;
			DtAutoOption option = (DtAutoOption)o;
			long code = option.code;
			if (DbSqlAutoOptions.SetOptionFind(code) == false) return;

			listView_complect.Items.Clear();
			DbSqlAutoOptions.SelectInListOptionFind(listView_complect);
		}

		private void button_remove_option_find_Click(object sender, System.EventArgs e)
		{
			// Убираем из списка!
			ListViewItem item = Db.GetItemSelected(listView_complect);
			if(item == null) return;
			if(item.Tag == null) return;
			object o = item.Tag;
			DtAutoOption option = (DtAutoOption)o;
			long code = option.code;
			if(code == 0) return;

			if (DbSqlAutoOptions.RemoveOptionFind(code) == false) return;
			listView_complect.Items.Clear();
			DbSqlAutoOptions.SelectInListOptionFind(listView_complect);
		}

		private void menuItem9_Click(object sender, System.EventArgs e)
		{
			// Печать комплектации
			ListViewItem item = null;
			long code = 0;
			item = Db.GetItemSelected(listView1);
			if(item == null) return;
			code = (long)item.Tag;
			if(code == 0) return;
			// Находим автомобиль по коду
			DtAuto auto = DbSqlAuto.Find(code);
			if(auto == null) return;

			// Запускаем процедуру печати
			DbPrintOptionsList print = new DbPrintOptionsList(code);
			print.Print();
		}

        private void menuItem11_Click(object sender, EventArgs e)
        {
            // Есть ПТС
            ListViewItem item = null;
            long code = 0;
            item = Db.GetItemSelected(listView1);
            if (item == null) return;
            code = (long)item.Tag;
            if (code == 0) return;
            // Находим автомобиль по коду
            DtAuto auto = DbSqlAuto.Find(code);
            if (auto == null) return;

            // А теперь собственно добавляем сам ПТС
            FormAutoPts form1 = new FormAutoPts(code, 1);
            if (form1.ShowDialog() != DialogResult.OK) return; // Не добавили сам ПТС

            DbSqlAuto.UpdatePts(code, true);
            auto = DbSqlAuto.SelectStorageAvaliableFind(code);
            auto.SetLVItemStorageV2(item);
        }

        private void menuItem12_Click(object sender, EventArgs e)
        {
            // Нет ПТС
            ListViewItem item = null;
            long code = 0;
            item = Db.GetItemSelected(listView1);
            if (item == null) return;
            code = (long)item.Tag;
            if (code == 0) return;
            // Находим автомобиль по коду
            DtAuto auto = DbSqlAuto.Find(code);
            if (auto == null) return;

            DbSqlAuto.UpdatePts(code, false);
            auto = DbSqlAuto.SelectStorageAvaliableFind(code);
            auto.SetLVItemStorageV2(item);
        }

        private void menuItem13_Click(object sender, EventArgs e)
        {
            // Ставим доплату за цвет для КОНКРЕТНОГО! автомобиля
            ListViewItem item = null;
            long code = 0;
            item = Db.GetItemSelected(listView1);
            if (item == null) return;
            code = (long)item.Tag;
            if (code == 0) return;
            // Находим автомобиль по коду
            DtAuto auto = DbSqlAuto.Find(code);
            if (auto == null) return;

            // Запрашиваем стоимость
            FormSelectString form1 = new FormSelectString("Доплата за цвет - установить", "");
            if (form1.ShowDialog() != DialogResult.OK) return;
            float price = form1.SelectedFloat;
            if (price == 0.0F) return;

            DbSqlAuto.UpdateColorPrice(code, price);
            auto = DbSqlAuto.SelectStorageAvaliableFind(code);
            auto.SetLVItemStorageV2(item);
        }
	}
}
