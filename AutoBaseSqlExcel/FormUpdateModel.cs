using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormUpdateModel.
	/// </summary>
	public class FormUpdateModel : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBox_workgroup;
		private System.Windows.Forms.TextBox textBox_guarantytype;
		private System.Windows.Forms.ListView listView_variant;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ListView listView_color;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button button_save;
		private System.Windows.Forms.TextBox textBox_name;
		private System.Windows.Forms.Label label5;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Button button_null_workgroup;
		private System.Windows.Forms.Button button_null_guarantytype;

		DtModel			model;

		// Для отображения результатов действий
		private int				exchange_type	= 0;	// 1 - добавление, 2 - изменение
		private TreeNode		tree_node		= null;	// Узел дерева
		private ListViewItem	list_item		= null;	// Элемент листа
		private ListView		list_view		= null;
		private System.Windows.Forms.Button button_new_color;
		private System.Windows.Forms.Button button_update_color;
		private System.Windows.Forms.Button button_del_color;
		private System.Windows.Forms.Button button_cancel_color;
		private System.Windows.Forms.Button button_restore_color;
		private System.Windows.Forms.Button button_new_variant;
		private System.Windows.Forms.Button button_update_variant;
		private System.Windows.Forms.Button button_del_variant;
		private System.Windows.Forms.Button button_cancel_variant;
		private System.Windows.Forms.Button button_restore_variant;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private TextBox textBox_markmodel;
        private TextBox textBox_trans;
        private TextBox textBox_engine;
        private TextBox textBox_type;	// Листа
		private TreeView		tree_view		= null;	// Дерево

		public void SetExchange(int type, TreeNode node, ListViewItem item, TreeView tree, ListView list)
		{
			exchange_type	= type;
			tree_node		= node;
			list_item		= item;
			list_view		= list;
			tree_view		= tree;
		}

		public void MakeExchange()
		{
			switch(exchange_type)
			{
				case 1:
					if(list_view != null)
					{
						if(list_view.IsDisposed == true) return;
						ListViewItem	item = list_view.Items.Add("Ошибка");
						model.SetLVItem(item);
						list_item = item;
						exchange_type = 2;
						list_view = null;
						return;
					}
					break;
				case 2:
					if(list_item != null)
					{
						if(list_item.ListView == null) return;
						if(list_item.ListView.IsDisposed == true) return;
						model.SetLVItem(list_item);
						return;
					}
					if(tree_node != null)
					{
						if(tree_node.TreeView == null) return;
						if(tree_node.TreeView.IsDisposed == true) return;
						model.SetTNode(tree_node);
						return;
					}
					break;
				default:
					break;
			}
		}

		public FormUpdateModel(long model_code)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			// Загрузка данных
			if(model_code == 0)
				model	= new DtModel();
			else
			{
				model	= DbSqlModel.Find(model_code);
				if(model == null) model	= new DtModel();
			}

			// Отображение данных
			textBox_workgroup.Text		= (string)model.GetData("НАИМЕНОВАНИЕ");
			textBox_guarantytype.Text	= (string)model.GetData("ОПИСАНИЕ_ГАРАНТИЯ");
			textBox_name.Text			= (string)model.GetData("МОДЕЛЬ");

            textBox_engine.Text = (string)model.GetData("ДВИГАТЕЛЬ");
            textBox_markmodel.Text = (string)model.GetData("МАРКА_МОДЕЛЬ_ПТС");
            textBox_trans.Text = (string)model.GetData("ТРАНСМИССИЯ");
            textBox_type.Text = (string)model.GetData("ТИП_ТС");

			// Список цветов модели
			DbSqlColor.SelectInListAll(listView_color, (long)model.GetData("КОД_АВТОМОБИЛЬ_МОДЕЛЬ"));
			// Список исполнений модели
			DbSqlVariant.SelectInListAll(listView_variant, (long)model.GetData("КОД_АВТОМОБИЛЬ_МОДЕЛЬ"));
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormUpdateModel));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_workgroup = new System.Windows.Forms.TextBox();
            this.textBox_guarantytype = new System.Windows.Forms.TextBox();
            this.listView_variant = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.label3 = new System.Windows.Forms.Label();
            this.listView_color = new System.Windows.Forms.ListView();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.label4 = new System.Windows.Forms.Label();
            this.button_save = new System.Windows.Forms.Button();
            this.textBox_name = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button_null_workgroup = new System.Windows.Forms.Button();
            this.button_null_guarantytype = new System.Windows.Forms.Button();
            this.button_new_color = new System.Windows.Forms.Button();
            this.button_update_color = new System.Windows.Forms.Button();
            this.button_del_color = new System.Windows.Forms.Button();
            this.button_cancel_color = new System.Windows.Forms.Button();
            this.button_restore_color = new System.Windows.Forms.Button();
            this.button_new_variant = new System.Windows.Forms.Button();
            this.button_update_variant = new System.Windows.Forms.Button();
            this.button_del_variant = new System.Windows.Forms.Button();
            this.button_cancel_variant = new System.Windows.Forms.Button();
            this.button_restore_variant = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox_markmodel = new System.Windows.Forms.TextBox();
            this.textBox_trans = new System.Windows.Forms.TextBox();
            this.textBox_engine = new System.Windows.Forms.TextBox();
            this.textBox_type = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(208, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Трудоемкости по умолчанию";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(208, 23);
            this.label2.TabIndex = 1;
            this.label2.Text = "Вид гарантии по умолчанию";
            // 
            // textBox_workgroup
            // 
            this.textBox_workgroup.Location = new System.Drawing.Point(216, 32);
            this.textBox_workgroup.Name = "textBox_workgroup";
            this.textBox_workgroup.Size = new System.Drawing.Size(296, 23);
            this.textBox_workgroup.TabIndex = 1;
            this.textBox_workgroup.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_workgroup_KeyDown);
            // 
            // textBox_guarantytype
            // 
            this.textBox_guarantytype.Location = new System.Drawing.Point(216, 56);
            this.textBox_guarantytype.Name = "textBox_guarantytype";
            this.textBox_guarantytype.Size = new System.Drawing.Size(296, 23);
            this.textBox_guarantytype.TabIndex = 2;
            this.textBox_guarantytype.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_guarantytype_KeyDown);
            // 
            // listView_variant
            // 
            this.listView_variant.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.listView_variant.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listView_variant.FullRowSelect = true;
            this.listView_variant.Location = new System.Drawing.Point(8, 239);
            this.listView_variant.Name = "listView_variant";
            this.listView_variant.Size = new System.Drawing.Size(272, 121);
            this.listView_variant.TabIndex = 4;
            this.listView_variant.TabStop = false;
            this.listView_variant.UseCompatibleStateImageBehavior = false;
            this.listView_variant.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Наименование";
            this.columnHeader1.Width = 244;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(8, 183);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 23);
            this.label3.TabIndex = 5;
            this.label3.Text = "Исполнения";
            // 
            // listView_color
            // 
            this.listView_color.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listView_color.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.listView_color.FullRowSelect = true;
            this.listView_color.Location = new System.Drawing.Point(288, 239);
            this.listView_color.Name = "listView_color";
            this.listView_color.Size = new System.Drawing.Size(264, 121);
            this.listView_color.TabIndex = 6;
            this.listView_color.TabStop = false;
            this.listView_color.UseCompatibleStateImageBehavior = false;
            this.listView_color.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Наименование";
            this.columnHeader2.Width = 237;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(288, 183);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 23);
            this.label4.TabIndex = 7;
            this.label4.Text = "Цвета";
            // 
            // button_save
            // 
            this.button_save.Location = new System.Drawing.Point(464, 183);
            this.button_save.Name = "button_save";
            this.button_save.Size = new System.Drawing.Size(88, 23);
            this.button_save.TabIndex = 3;
            this.button_save.Text = "Сохранить";
            this.button_save.Click += new System.EventHandler(this.button_save_Click);
            // 
            // textBox_name
            // 
            this.textBox_name.Location = new System.Drawing.Point(216, 8);
            this.textBox_name.Name = "textBox_name";
            this.textBox_name.Size = new System.Drawing.Size(296, 23);
            this.textBox_name.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(8, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(168, 23);
            this.label5.TabIndex = 10;
            this.label5.Text = "Наименование модели";
            // 
            // button_null_workgroup
            // 
            this.button_null_workgroup.Image = ((System.Drawing.Image)(resources.GetObject("button_null_workgroup.Image")));
            this.button_null_workgroup.Location = new System.Drawing.Point(512, 32);
            this.button_null_workgroup.Name = "button_null_workgroup";
            this.button_null_workgroup.Size = new System.Drawing.Size(24, 23);
            this.button_null_workgroup.TabIndex = 11;
            this.button_null_workgroup.TabStop = false;
            this.button_null_workgroup.Click += new System.EventHandler(this.button_null_workgroup_Click);
            // 
            // button_null_guarantytype
            // 
            this.button_null_guarantytype.Image = ((System.Drawing.Image)(resources.GetObject("button_null_guarantytype.Image")));
            this.button_null_guarantytype.Location = new System.Drawing.Point(512, 56);
            this.button_null_guarantytype.Name = "button_null_guarantytype";
            this.button_null_guarantytype.Size = new System.Drawing.Size(24, 23);
            this.button_null_guarantytype.TabIndex = 12;
            this.button_null_guarantytype.TabStop = false;
            this.button_null_guarantytype.Click += new System.EventHandler(this.button_null_guarantytype_Click);
            // 
            // button_new_color
            // 
            this.button_new_color.Image = ((System.Drawing.Image)(resources.GetObject("button_new_color.Image")));
            this.button_new_color.Location = new System.Drawing.Point(288, 215);
            this.button_new_color.Name = "button_new_color";
            this.button_new_color.Size = new System.Drawing.Size(24, 23);
            this.button_new_color.TabIndex = 13;
            this.button_new_color.Click += new System.EventHandler(this.button_new_color_Click);
            // 
            // button_update_color
            // 
            this.button_update_color.Image = ((System.Drawing.Image)(resources.GetObject("button_update_color.Image")));
            this.button_update_color.Location = new System.Drawing.Point(312, 215);
            this.button_update_color.Name = "button_update_color";
            this.button_update_color.Size = new System.Drawing.Size(24, 23);
            this.button_update_color.TabIndex = 14;
            this.button_update_color.Click += new System.EventHandler(this.button_update_color_Click);
            // 
            // button_del_color
            // 
            this.button_del_color.Image = ((System.Drawing.Image)(resources.GetObject("button_del_color.Image")));
            this.button_del_color.Location = new System.Drawing.Point(336, 215);
            this.button_del_color.Name = "button_del_color";
            this.button_del_color.Size = new System.Drawing.Size(24, 23);
            this.button_del_color.TabIndex = 15;
            this.button_del_color.Click += new System.EventHandler(this.button_del_color_Click);
            // 
            // button_cancel_color
            // 
            this.button_cancel_color.Image = ((System.Drawing.Image)(resources.GetObject("button_cancel_color.Image")));
            this.button_cancel_color.Location = new System.Drawing.Point(504, 215);
            this.button_cancel_color.Name = "button_cancel_color";
            this.button_cancel_color.Size = new System.Drawing.Size(24, 23);
            this.button_cancel_color.TabIndex = 16;
            this.button_cancel_color.Click += new System.EventHandler(this.button_cancel_color_Click);
            // 
            // button_restore_color
            // 
            this.button_restore_color.Image = ((System.Drawing.Image)(resources.GetObject("button_restore_color.Image")));
            this.button_restore_color.Location = new System.Drawing.Point(528, 215);
            this.button_restore_color.Name = "button_restore_color";
            this.button_restore_color.Size = new System.Drawing.Size(24, 23);
            this.button_restore_color.TabIndex = 17;
            this.button_restore_color.Click += new System.EventHandler(this.button_restore_color_Click);
            // 
            // button_new_variant
            // 
            this.button_new_variant.Image = ((System.Drawing.Image)(resources.GetObject("button_new_variant.Image")));
            this.button_new_variant.Location = new System.Drawing.Point(8, 215);
            this.button_new_variant.Name = "button_new_variant";
            this.button_new_variant.Size = new System.Drawing.Size(24, 23);
            this.button_new_variant.TabIndex = 18;
            this.button_new_variant.Click += new System.EventHandler(this.button_new_variant_Click);
            // 
            // button_update_variant
            // 
            this.button_update_variant.Image = ((System.Drawing.Image)(resources.GetObject("button_update_variant.Image")));
            this.button_update_variant.Location = new System.Drawing.Point(32, 215);
            this.button_update_variant.Name = "button_update_variant";
            this.button_update_variant.Size = new System.Drawing.Size(24, 23);
            this.button_update_variant.TabIndex = 19;
            this.button_update_variant.Click += new System.EventHandler(this.button_update_variant_Click);
            // 
            // button_del_variant
            // 
            this.button_del_variant.Image = ((System.Drawing.Image)(resources.GetObject("button_del_variant.Image")));
            this.button_del_variant.Location = new System.Drawing.Point(56, 215);
            this.button_del_variant.Name = "button_del_variant";
            this.button_del_variant.Size = new System.Drawing.Size(24, 23);
            this.button_del_variant.TabIndex = 20;
            this.button_del_variant.Click += new System.EventHandler(this.button_del_variant_Click);
            // 
            // button_cancel_variant
            // 
            this.button_cancel_variant.Image = ((System.Drawing.Image)(resources.GetObject("button_cancel_variant.Image")));
            this.button_cancel_variant.Location = new System.Drawing.Point(232, 215);
            this.button_cancel_variant.Name = "button_cancel_variant";
            this.button_cancel_variant.Size = new System.Drawing.Size(24, 23);
            this.button_cancel_variant.TabIndex = 21;
            this.button_cancel_variant.Click += new System.EventHandler(this.button_cancel_variant_Click);
            // 
            // button_restore_variant
            // 
            this.button_restore_variant.Image = ((System.Drawing.Image)(resources.GetObject("button_restore_variant.Image")));
            this.button_restore_variant.Location = new System.Drawing.Point(256, 215);
            this.button_restore_variant.Name = "button_restore_variant";
            this.button_restore_variant.Size = new System.Drawing.Size(24, 23);
            this.button_restore_variant.TabIndex = 22;
            this.button_restore_variant.Click += new System.EventHandler(this.button_restore_variant_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 91);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(175, 17);
            this.label6.TabIndex = 23;
            this.label6.Text = "Марка, модель как в ПТС";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 112);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(95, 17);
            this.label7.TabIndex = 24;
            this.label7.Text = "Трансмиссия";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 154);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(199, 17);
            this.label8.TabIndex = 25;
            this.label8.Text = "Тип транспортного средства";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 133);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(129, 17);
            this.label9.TabIndex = 26;
            this.label9.Text = "Модель двигателя";
            // 
            // textBox_markmodel
            // 
            this.textBox_markmodel.Location = new System.Drawing.Point(216, 85);
            this.textBox_markmodel.Name = "textBox_markmodel";
            this.textBox_markmodel.Size = new System.Drawing.Size(296, 23);
            this.textBox_markmodel.TabIndex = 27;
            // 
            // textBox_trans
            // 
            this.textBox_trans.Location = new System.Drawing.Point(216, 108);
            this.textBox_trans.Name = "textBox_trans";
            this.textBox_trans.Size = new System.Drawing.Size(296, 23);
            this.textBox_trans.TabIndex = 28;
            // 
            // textBox_engine
            // 
            this.textBox_engine.Location = new System.Drawing.Point(216, 131);
            this.textBox_engine.Name = "textBox_engine";
            this.textBox_engine.Size = new System.Drawing.Size(296, 23);
            this.textBox_engine.TabIndex = 29;
            // 
            // textBox_type
            // 
            this.textBox_type.Location = new System.Drawing.Point(216, 154);
            this.textBox_type.Name = "textBox_type";
            this.textBox_type.Size = new System.Drawing.Size(296, 23);
            this.textBox_type.TabIndex = 30;
            // 
            // FormUpdateModel
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
            this.ClientSize = new System.Drawing.Size(560, 365);
            this.Controls.Add(this.textBox_type);
            this.Controls.Add(this.textBox_engine);
            this.Controls.Add(this.textBox_trans);
            this.Controls.Add(this.textBox_markmodel);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button_restore_variant);
            this.Controls.Add(this.button_cancel_variant);
            this.Controls.Add(this.button_del_variant);
            this.Controls.Add(this.button_update_variant);
            this.Controls.Add(this.button_new_variant);
            this.Controls.Add(this.button_restore_color);
            this.Controls.Add(this.button_cancel_color);
            this.Controls.Add(this.button_del_color);
            this.Controls.Add(this.button_update_color);
            this.Controls.Add(this.button_new_color);
            this.Controls.Add(this.button_null_guarantytype);
            this.Controls.Add(this.button_null_workgroup);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox_name);
            this.Controls.Add(this.button_save);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.listView_color);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.listView_variant);
            this.Controls.Add(this.textBox_guarantytype);
            this.Controls.Add(this.textBox_workgroup);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MaximumSize = new System.Drawing.Size(568, 768);
            this.MinimumSize = new System.Drawing.Size(568, 392);
            this.Name = "FormUpdateModel";
            this.Text = "Управление моделью";
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void button_save_Click(object sender, System.EventArgs e)
		{
			model.SetData("МОДЕЛЬ", textBox_name.Text);
            model.SetData("ДВИГАТЕЛЬ", textBox_engine.Text);
            model.SetData("МАРКА_МОДЕЛЬ_ПТС", textBox_markmodel.Text);
            model.SetData("ТРАНСМИССИЯ", textBox_trans.Text);
            model.SetData("ТИП_ТС", textBox_type.Text);

			if(model.CheckData("МОДЕЛЬ") != true) return;
			// Записываем данные модели
			if((long)model.GetData("КОД_АВТОМОБИЛЬ_МОДЕЛЬ") == 0)
			{
				// Новый
				DtModel new_model = DbSqlModel.Insert(model);
				if(new_model == null) return;
                new_model.SetData("ДВИГАТЕЛЬ", textBox_engine.Text);
                new_model.SetData("МАРКА_МОДЕЛЬ_ПТС", textBox_markmodel.Text);
                new_model.SetData("ТРАНСМИССИЯ", textBox_trans.Text);
                new_model.SetData("ТИП_ТС", textBox_type.Text);
				model	= new_model;
                DbSqlModel.UpdateEx(model);
				MessageBox.Show("Данные успешно добавлены");
			}
			else
			{
				// Изменение
				if(DbSqlModel.Update(model) != true) return;
                if (DbSqlModel.UpdateEx(model) != true) return;
				MessageBox.Show("Данные успешно изменены");
			}
			MakeExchange();
		}

		private void textBox_workgroup_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Enter)
			{
				// Инициируем выбор группы трудоемкостей
				ListView list = new ListView();
				DbSqlWorkGroup.SelectInList(list);
				FormSelectionList dialog = new FormSelectionList(list);
				if(dialog.ShowDialog() != DialogResult.OK) return;
				if(dialog.SelectedCode == 0) return;
				model.SetData("ССЫЛКА_КОД_АВТОМОБИЛЬ_ТИП", dialog.SelectedCode);
				textBox_workgroup.Text	= dialog.SelectedText;
				textBox_guarantytype.Focus();
			}
		}

		private void textBox_guarantytype_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Enter)
			{
				// Инициируем выбор вида гарантии
				ListView list = new ListView();
				DbSqlGuarantyType.SelectInList(list);
				FormSelectionList dialog = new FormSelectionList(list);
				if(dialog.ShowDialog() != DialogResult.OK) return;
				if(dialog.SelectedCode == 0) return;
				model.SetData("ССЫЛКА_КОД_ГАРАНТИЯ", dialog.SelectedCode);
				textBox_guarantytype.Text	= dialog.SelectedText;
			}
		}

		private void button_null_workgroup_Click(object sender, System.EventArgs e)
		{
			// Зануляем трудоемкость по умолчанию
			model.SetData("ССЫЛКА_КОД_АВТОМОБИЛЬ_ТИП", 0L);
			textBox_workgroup.Text	= "";
		}

		private void button_null_guarantytype_Click(object sender, System.EventArgs e)
		{
			// Занюляем вид гарантии по умолчанию
			model.SetData("ССЫЛКА_КОД_ГАРАНТИЯ", 0L);
			textBox_guarantytype.Text	= "";
		}

		private void button_new_color_Click(object sender, System.EventArgs e)
		{
			// Добавляем новый цвет для выбранной модели
			if((long)model.GetData("КОД_АВТОМОБИЛЬ_МОДЕЛЬ") == 0) return;
			FormUpdateColor dialog = new FormUpdateColor((long)model.GetData("КОД_АВТОМОБИЛЬ_МОДЕЛЬ"), 0);
			if(dialog.ShowDialog() != DialogResult.OK) return;
			ListViewItem item = listView_color.Items.Insert(0, "Ошибка");
			dialog.Color.SetLVItem(item);
		}

		private void button_update_color_Click(object sender, System.EventArgs e)
		{
			// Изменение свойств выбранного цвета
			ListViewItem item = Db.GetItemSelected(listView_color);
			if(item == null) return;
			if(item.Tag == null) return;
			if((long)item.Tag == 0) return;

			FormUpdateColor dialog = new FormUpdateColor(0, (long)item.Tag);
			if(dialog.ShowDialog() != DialogResult.OK) return;
			dialog.Color.SetLVItem(item);
		}

		private void button_del_color_Click(object sender, System.EventArgs e)
		{
			// Удаление выбранного цвета
			ListViewItem item = Db.GetItemSelected(listView_color);
			if(item == null) return;
			if(item.Tag == null) return;
			if((long)item.Tag == 0) return;

			if(DbSqlColor.Delete((long)item.Tag) == false) return;
			listView_color.Items.Remove(item);
		}

		private void button_cancel_color_Click(object sender, System.EventArgs e)
		{
			// Отменить выбранный цвет
			ListViewItem item = Db.GetItemSelected(listView_color);
			if(item == null) return;
			if(item.Tag == null) return;
			if((long)item.Tag == 0) return;

			if(DbSqlColor.Cancel((long)item.Tag) == false) return;
			DtColor.SetLVItemCancel(item);
		}

		private void button_restore_color_Click(object sender, System.EventArgs e)
		{
			// Восстановить выбранный цвет
			ListViewItem item = Db.GetItemSelected(listView_color);
			if(item == null) return;
			if(item.Tag == null) return;
			if((long)item.Tag == 0) return;

			if(DbSqlColor.Restore((long)item.Tag) == false) return;
			DtColor.SetLVItemRestore(item);
		}

		private void button_new_variant_Click(object sender, System.EventArgs e)
		{
			// Добавляем новое исполнение для выбранной модели
			if((long)model.GetData("КОД_АВТОМОБИЛЬ_МОДЕЛЬ") == 0) return;
			FormUpdateVariant dialog = new FormUpdateVariant((long)model.GetData("КОД_АВТОМОБИЛЬ_МОДЕЛЬ"), 0);
			if(dialog.ShowDialog() != DialogResult.OK) return;
			ListViewItem item = listView_variant.Items.Insert(0, "Ошибка");
			dialog.Variant.SetLVItem(item);
		}

		private void button_update_variant_Click(object sender, System.EventArgs e)
		{
			// Изменение свойств выбранного исполнения
			ListViewItem item = Db.GetItemSelected(listView_variant);
			if(item == null) return;
			if(item.Tag == null) return;
			if((long)item.Tag == 0) return;

			FormUpdateVariant dialog = new FormUpdateVariant(0, (long)item.Tag);
			if(dialog.ShowDialog() != DialogResult.OK) return;
			dialog.Variant.SetLVItem(item);
		}

		private void button_del_variant_Click(object sender, System.EventArgs e)
		{
			// Удаление выбранного исполнения
			ListViewItem item = Db.GetItemSelected(listView_variant);
			if(item == null) return;
			if(item.Tag == null) return;
			if((long)item.Tag == 0) return;

			if(DbSqlVariant.Delete((long)item.Tag) == false) return;
			listView_variant.Items.Remove(item);
		}

		private void button_cancel_variant_Click(object sender, System.EventArgs e)
		{
			// Отменить выбранное исполнение
			ListViewItem item = Db.GetItemSelected(listView_variant);
			if(item == null) return;
			if(item.Tag == null) return;
			if((long)item.Tag == 0) return;

			if(DbSqlVariant.Cancel((long)item.Tag) == false) return;
			DtVariant.SetLVItemCancel(item);
		}

		private void button_restore_variant_Click(object sender, System.EventArgs e)
		{
			// Восстановить выбранное исполнение
			ListViewItem item = Db.GetItemSelected(listView_variant);
			if(item == null) return;
			if(item.Tag == null) return;
			if((long)item.Tag == 0) return;

			if(DbSqlVariant.Restore((long)item.Tag) == false) return;
			DtVariant.SetLVItemRestore(item);
		}
	}
}
