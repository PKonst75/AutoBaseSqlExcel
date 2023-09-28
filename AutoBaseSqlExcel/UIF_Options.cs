using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for UIF_Options.
	/// </summary>
	public class UIF_Options : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.ListView listView_options;
		private System.Windows.Forms.ListView listView_variants;
		private System.Windows.Forms.Button button_add_variant;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.Button button_options_update;
		private System.Windows.Forms.ComboBox comboBox_model;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox comboBox_model_variant;
		private System.Windows.Forms.Button button_save_complect;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public UIF_Options()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			// Инициируем список моделей
			ArrayList array = new ArrayList();
			DbSqlModel.SelectInArrayInSell(array);
			foreach(object o in array)
			{
				DtModel model = (DtModel)o;
				comboBox_model.Items.Add(model);
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(UIF_Options));
			this.listView_options = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.button1 = new System.Windows.Forms.Button();
			this.listView_variants = new System.Windows.Forms.ListView();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.button_add_variant = new System.Windows.Forms.Button();
			this.button_options_update = new System.Windows.Forms.Button();
			this.comboBox_model = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.comboBox_model_variant = new System.Windows.Forms.ComboBox();
			this.button_save_complect = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// listView_options
			// 
			this.listView_options.CheckBoxes = true;
			this.listView_options.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																							   this.columnHeader1,
																							   this.columnHeader2});
			this.listView_options.Location = new System.Drawing.Point(16, 56);
			this.listView_options.Name = "listView_options";
			this.listView_options.Size = new System.Drawing.Size(480, 248);
			this.listView_options.TabIndex = 0;
			this.listView_options.View = System.Windows.Forms.View.Details;
			this.listView_options.SelectedIndexChanged += new System.EventHandler(this.listView_options_SelectedIndexChanged);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Опция";
			this.columnHeader1.Width = 240;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Вариант";
			this.columnHeader2.Width = 226;
			// 
			// button1
			// 
			this.button1.Image = ((System.Drawing.Bitmap)(resources.GetObject("button1.Image")));
			this.button1.Location = new System.Drawing.Point(16, 32);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(24, 23);
			this.button1.TabIndex = 1;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// listView_variants
			// 
			this.listView_variants.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																								this.columnHeader3});
			this.listView_variants.Location = new System.Drawing.Point(504, 56);
			this.listView_variants.Name = "listView_variants";
			this.listView_variants.Size = new System.Drawing.Size(352, 248);
			this.listView_variants.TabIndex = 2;
			this.listView_variants.View = System.Windows.Forms.View.Details;
			this.listView_variants.DoubleClick += new System.EventHandler(this.listView_variants_DoubleClick);
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Наименование";
			this.columnHeader3.Width = 331;
			// 
			// button_add_variant
			// 
			this.button_add_variant.Image = ((System.Drawing.Bitmap)(resources.GetObject("button_add_variant.Image")));
			this.button_add_variant.Location = new System.Drawing.Point(504, 32);
			this.button_add_variant.Name = "button_add_variant";
			this.button_add_variant.Size = new System.Drawing.Size(24, 23);
			this.button_add_variant.TabIndex = 3;
			this.button_add_variant.Click += new System.EventHandler(this.button_add_variant_Click);
			// 
			// button_options_update
			// 
			this.button_options_update.Image = ((System.Drawing.Bitmap)(resources.GetObject("button_options_update.Image")));
			this.button_options_update.Location = new System.Drawing.Point(40, 32);
			this.button_options_update.Name = "button_options_update";
			this.button_options_update.Size = new System.Drawing.Size(24, 23);
			this.button_options_update.TabIndex = 4;
			this.button_options_update.Click += new System.EventHandler(this.button_options_update_Click);
			// 
			// comboBox_model
			// 
			this.comboBox_model.Location = new System.Drawing.Point(128, 8);
			this.comboBox_model.Name = "comboBox_model";
			this.comboBox_model.Size = new System.Drawing.Size(176, 21);
			this.comboBox_model.TabIndex = 5;
			this.comboBox_model.SelectedValueChanged += new System.EventHandler(this.comboBox_model_SelectedValueChanged);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 8);
			this.label1.Name = "label1";
			this.label1.TabIndex = 6;
			this.label1.Text = "Модель";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(320, 8);
			this.label2.Name = "label2";
			this.label2.TabIndex = 7;
			this.label2.Text = "Исполнение";
			// 
			// comboBox_model_variant
			// 
			this.comboBox_model_variant.Location = new System.Drawing.Point(424, 8);
			this.comboBox_model_variant.Name = "comboBox_model_variant";
			this.comboBox_model_variant.Size = new System.Drawing.Size(208, 21);
			this.comboBox_model_variant.TabIndex = 8;
			this.comboBox_model_variant.SelectedIndexChanged += new System.EventHandler(this.comboBox_model_variant_SelectedIndexChanged);
			// 
			// button_save_complect
			// 
			this.button_save_complect.Location = new System.Drawing.Point(720, 8);
			this.button_save_complect.Name = "button_save_complect";
			this.button_save_complect.Size = new System.Drawing.Size(120, 23);
			this.button_save_complect.TabIndex = 9;
			this.button_save_complect.Text = "Сохранить";
			this.button_save_complect.Click += new System.EventHandler(this.button_save_complect_Click);
			// 
			// UIF_Options
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(864, 317);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.button_save_complect,
																		  this.comboBox_model_variant,
																		  this.label2,
																		  this.label1,
																		  this.comboBox_model,
																		  this.button_options_update,
																		  this.button_add_variant,
																		  this.listView_variants,
																		  this.button1,
																		  this.listView_options});
			this.Name = "UIF_Options";
			this.Text = "Комплектации автомобиля";
			this.ResumeLayout(false);

		}
		#endregion

		private void button1_Click(object sender, System.EventArgs e)
		{
			// Добавление новой опции в список опций

			UIF_AutoOption dialog = new UIF_AutoOption();
			if(dialog.ShowDialog() != DialogResult.OK) return;
			DtAutoOption auto_option = dialog.new_option;
			if(auto_option == null) return;
			ListViewItem item = new ListViewItem();
			auto_option.SetLVItem(item);
			listView_options.Items.Add(item);
		}

		private void button_add_variant_Click(object sender, System.EventArgs e)
		{
			// Добавить вариант к выбранной опции
			ListViewItem item = Db.GetItemSelected(listView_options);
			if (item == null) return;
			string caption_text = item.Text;
			DtAutoOption option = (DtAutoOption)item.Tag;
			long code = (long)option.code;
			if (code == 0) return;

			FormSelectString dialog = new FormSelectString("Добавить вариант для опции: " + caption_text, "");
			if(dialog.ShowDialog() != DialogResult.OK) return;
			string variant_name = dialog.SelectedText;

			DtAutoOptionVariant variant = new DtAutoOptionVariant();
			variant.name = variant_name;
			variant.code_option = code;

			variant = DbSqlAutoOptions.InsertOptionVariant(variant);
			if(variant == null) return;
			ListViewItem new_item = new ListViewItem();
			variant.SetLVItem(new_item);
			listView_variants.Items.Add(new_item);
		}

		private void button_options_update_Click(object sender, System.EventArgs e)
		{
			// Обновление списка опций
			listView_options.Items.Clear();
			DbSqlAutoOptions.SelectInListOption(listView_options);
		}

		private void listView_options_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			// Обнвление списка вариантов опций
			listView_variants.Items.Clear();
			ListViewItem item = Db.GetItemSelected(listView_options);
			if (item == null) return;
			string caption_text = item.Text;
			DtAutoOption option = (DtAutoOption)item.Tag;
			long code = (long)option.code;
			if (code == 0) return;

			DbSqlAutoOptions.SelectInListVariant(listView_variants, code);
		}

		private void comboBox_model_SelectedValueChanged(object sender, System.EventArgs e)
		{
			// После выбора модели, заполняем список активных вариантов/комплектаций
			DtModel model = (DtModel)comboBox_model.SelectedItem;
			if (model == null) return;
			listView_options.Items.Clear();
			comboBox_model_variant.Items.Clear();
			comboBox_model_variant.SelectedItem = null;
			comboBox_model_variant.SelectedText = "";
			comboBox_model_variant.SelectedIndex = -1;
			comboBox_model_variant.Text = "";
			ArrayList array = new ArrayList();
			DbSqlVariant.SelectInArray(array, (long)model.GetData("КОД_АВТОМОБИЛЬ_МОДЕЛЬ"));
			foreach(object o in array)
			{
				DtVariant variant = (DtVariant)o;
				comboBox_model_variant.Items.Add(variant);
			}
		}

		private void listView_variants_DoubleClick(object sender, System.EventArgs e)
		{
			// Двойным счелчком - выбор активного варианта комплектации
			ListViewItem item = Db.GetItemSelected(listView_options);
			if (item == null) return;
			if(item.Tag == null) return;
			DtAutoOption option = (DtAutoOption)item.Tag;
			
			ListViewItem item1 = Db.GetItemSelected(listView_variants);
			if (item1 == null) return;
			string text = item1.Text;
			long code = (long)item1.Tag;
			if(code == 0) return;

			// Установка значения
			option.tmp_option_variant_name	= text;
			option.tmp_option_variant		= code;
			option.tmp_change				= true;

			option.SetLVItem(item);
			item.Checked = true;
			
		}

		private void button_save_complect_Click(object sender, System.EventArgs e)
		{
			// Сохранить комплектацию автомобиля
			DtModel		model = (DtModel)comboBox_model.SelectedItem;
			DtVariant	variant = (DtVariant)comboBox_model_variant.SelectedItem;
			if(model == null) return;
			if(variant == null) return;
			long code_model = (long)model.GetData("КОД_АВТОМОБИЛЬ_МОДЕЛЬ");
			long code_model_variant	= (long)variant.GetData("КОД_АВТОМОБИЛЬ_ИСПОЛНЕНИЕ");
			if(code_model == 0) return;
			if(code_model_variant == 0) return;

			foreach(ListViewItem item in listView_options.Items)
			{
				DtAutoOption option = (DtAutoOption)item.Tag;
				if(item.Checked)
				{
					// Производим запись/удаление
					if(option.tmp_active == false)
						DbSqlAutoOptions.InsertComplect(code_model, code_model_variant, option.code, option.tmp_option_variant);
					else
					{
						if(option.tmp_change == true)
							DbSqlAutoOptions.InsertComplect(code_model, code_model_variant, option.code, option.tmp_option_variant);
					}
				}
				else
				{
					if(option.tmp_active == true)
						DbSqlAutoOptions.RemoveComplect(code_model, code_model_variant, option.code);
				}
			}
			// Отмечаем что комплектация сохранена
			listView_options.Items.Clear();
			DbSqlAutoOptions.SelectInListOtionComplect(listView_options, code_model, code_model_variant);
			MessageBox.Show("Комплектация сохранена");
		}

		private void comboBox_model_variant_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			// Заполняем данные о выбранной комплектации
			listView_options.Items.Clear();
			DtModel		model = (DtModel)comboBox_model.SelectedItem;
			DtVariant	variant = (DtVariant)comboBox_model_variant.SelectedItem;
			if(model == null) return;
			if(variant == null) return;
			long code_model = (long)model.GetData("КОД_АВТОМОБИЛЬ_МОДЕЛЬ");
			long code_model_variant	= (long)variant.GetData("КОД_АВТОМОБИЛЬ_ИСПОЛНЕНИЕ");
			if(code_model == 0) return;
			if(code_model_variant == 0) return;

			DbSqlAutoOptions.SelectInListOtionComplect(listView_options, code_model, code_model_variant);
		}
	}
}
