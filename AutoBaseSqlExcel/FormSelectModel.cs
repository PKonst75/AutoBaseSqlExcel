using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormSelectModel.
	/// </summary>
	public class FormSelectModel : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton radioButton_tree;
		private System.Windows.Forms.RadioButton radioButton_list;
		private System.Windows.Forms.TreeView treeView1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		
		private	long	selected_code		= 0;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.TextBox textBox_brand;
		private System.Windows.Forms.Label label1;
		private	string	selected_text		= "";
		private System.Windows.Forms.Button button_cancel_brand;


		public FormSelectModel()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			radioButton_tree.Checked	= true;
			radioButton_list.Checked	= false;
			textBox_brand.Enabled		= false;
			button_cancel_brand.Enabled	= false;
			listView1.Visible			= false;

			// Заполняем дерево брендами
		//	DbSqlBrand.SelectInTree(treeView1);
		//	foreach(TreeNode node in treeView1.Nodes)
		//	{
		//		DbSqlModel.SelectInTree(node, (long)node.Tag);
		//	}
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormSelectModel));
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.button_cancel_brand = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.textBox_brand = new System.Windows.Forms.TextBox();
			this.radioButton_list = new System.Windows.Forms.RadioButton();
			this.radioButton_tree = new System.Windows.Forms.RadioButton();
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.button_cancel_brand,
																					this.label1,
																					this.textBox_brand,
																					this.radioButton_list,
																					this.radioButton_tree});
			this.groupBox1.Location = new System.Drawing.Point(8, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(544, 72);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Стиль выбора";
			// 
			// button_cancel_brand
			// 
			this.button_cancel_brand.Image = ((System.Drawing.Bitmap)(resources.GetObject("button_cancel_brand.Image")));
			this.button_cancel_brand.Location = new System.Drawing.Point(344, 40);
			this.button_cancel_brand.Name = "button_cancel_brand";
			this.button_cancel_brand.Size = new System.Drawing.Size(24, 23);
			this.button_cancel_brand.TabIndex = 4;
			this.button_cancel_brand.Click += new System.EventHandler(this.button_cancel_brand_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(168, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(184, 23);
			this.label1.TabIndex = 3;
			this.label1.Text = "Бренд для выбора в листе";
			// 
			// textBox_brand
			// 
			this.textBox_brand.Location = new System.Drawing.Point(168, 40);
			this.textBox_brand.Name = "textBox_brand";
			this.textBox_brand.Size = new System.Drawing.Size(176, 23);
			this.textBox_brand.TabIndex = 2;
			this.textBox_brand.Text = "";
			this.textBox_brand.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_brand_KeyDown);
			// 
			// radioButton_list
			// 
			this.radioButton_list.Location = new System.Drawing.Point(8, 40);
			this.radioButton_list.Name = "radioButton_list";
			this.radioButton_list.TabIndex = 1;
			this.radioButton_list.Text = "Лист";
			this.radioButton_list.CheckedChanged += new System.EventHandler(this.radioButton_list_CheckedChanged);
			// 
			// radioButton_tree
			// 
			this.radioButton_tree.Location = new System.Drawing.Point(8, 16);
			this.radioButton_tree.Name = "radioButton_tree";
			this.radioButton_tree.Size = new System.Drawing.Size(120, 24);
			this.radioButton_tree.TabIndex = 0;
			this.radioButton_tree.Text = "Дерево";
			this.radioButton_tree.CheckedChanged += new System.EventHandler(this.radioButton_tree_CheckedChanged);
			// 
			// treeView1
			// 
			this.treeView1.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.treeView1.ImageIndex = -1;
			this.treeView1.Location = new System.Drawing.Point(8, 88);
			this.treeView1.Name = "treeView1";
			this.treeView1.SelectedImageIndex = -1;
			this.treeView1.Size = new System.Drawing.Size(544, 240);
			this.treeView1.TabIndex = 1;
			this.treeView1.DoubleClick += new System.EventHandler(this.treeView1_DoubleClick);
			// 
			// listView1
			// 
			this.listView1.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.columnHeader1});
			this.listView1.Location = new System.Drawing.Point(8, 88);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(544, 240);
			this.listView1.TabIndex = 2;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Наименование";
			this.columnHeader1.Width = 300;
			// 
			// FormSelectModel
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.ClientSize = new System.Drawing.Size(568, 333);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.listView1,
																		  this.treeView1,
																		  this.groupBox1});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.Name = "FormSelectModel";
			this.Text = "Выбор модели";
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void treeView1_DoubleClick(object sender, System.EventArgs e)
		{
			// Выбор модели!
			// Ищем выбранный елемент дерева
			TreeNode node = Db.GetItemSelected(treeView1);
			if(node == null) return;
			if(node.Tag == null) return;
			if(node.Parent == null) return;	// Это не Модель
			if((long)node.Tag == 0) return;

			selected_code			= (long)node.Tag;
			selected_text			= node.Text;
			this.Close();
			this.DialogResult = DialogResult.OK;
		}

		private void textBox_brand_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			// Выбор бренда для сортировки
			if(e.KeyCode == Keys.Enter)
			{
				ListView list = new ListView();
				DbSqlBrand.SelectInList(list);
				FormSelectionList dialog = new FormSelectionList(list);
				if(dialog.ShowDialog() != DialogResult.OK) return;
				textBox_brand.Text	= dialog.SelectedText;
				listView1.Items.Clear();
				DbSqlModel.SelectInList(listView1, dialog.SelectedCode);
			}
		}

		private void listView1_DoubleClick(object sender, System.EventArgs e)
		{
			// Выбор модели из листа
			// Удаление модели
			// Выдаем список автомобилей записанных на эту модель
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			if(item.Tag == null) return;
			if((long)item.Tag == 0) return;

			selected_code			= (long)item.Tag;
			selected_text			= item.Text;
			this.Close();
			this.DialogResult = DialogResult.OK;
		}

		private void button_cancel_brand_Click(object sender, System.EventArgs e)
		{
			textBox_brand.Text		= "";
			listView1.Items.Clear();
			DbSqlModel.SelectInListAll(listView1);
		}

		private void radioButton_list_CheckedChanged(object sender, System.EventArgs e)
		{
			// При смене на включено!
			if(radioButton_list.Checked == true)
			{
				radioButton_tree.Checked = false;
				listView1.Visible = true;
				textBox_brand.Enabled = true;
				button_cancel_brand.Enabled = true;
				DbSqlModel.SelectInListAll(listView1);
				treeView1.Nodes.Clear();
				treeView1.Visible = false;
			}
		}

		private void radioButton_tree_CheckedChanged(object sender, System.EventArgs e)
		{
			// При смене на включено!
			if(radioButton_tree.Checked == true)
			{
				radioButton_list.Checked = false;
				listView1.Visible = false;
				textBox_brand.Enabled = false;
				button_cancel_brand.Enabled = false;

				// Заполняем дерево брендами
				DbSqlBrand.SelectInTree(treeView1);
				foreach(TreeNode node in treeView1.Nodes)
				{
					DbSqlModel.SelectInTree(node, (long)node.Tag);
				}

				listView1.Items.Clear();
				treeView1.Visible = true;
			}
		}

		public long SelectedCode
		{
			get
			{
				return selected_code;
			}
		}
		public string SelectedText
		{
			get
			{
				return selected_text;
			}
		}
	}
}
