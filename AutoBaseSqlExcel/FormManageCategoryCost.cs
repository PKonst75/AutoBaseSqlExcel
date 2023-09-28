using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormManageCategoryCost.
	/// </summary>
	public class FormManageCategoryCost : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.TextBox textBoxCategoryCost;
		private System.Windows.Forms.Label label1;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.TextBox textBoxMark;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button buttonNewCategoryCost;
		private System.Windows.Forms.Button buttonPropertiesCategoryCost;
		private System.Windows.Forms.Button buttonNewCategoryPrice;
		private System.Windows.Forms.Button buttonPropertiesCategoryPrice;
		private System.Windows.Forms.ToolTip toolTip1;

		DbCategoryCost	categoryCostSelection = null;

		public FormManageCategoryCost()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			if(categoryCostSelection != null)
			{
				textBoxCategoryCost.Text	= categoryCostSelection.DbTitle();
				textBoxMark.Text			= categoryCostSelection.Mark;
			}
			else
			{
				textBoxCategoryCost.Text	= "";
				textBoxMark.Text			= "";
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormManageCategoryCost));
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.textBoxCategoryCost = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.textBoxMark = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.buttonNewCategoryCost = new System.Windows.Forms.Button();
			this.buttonPropertiesCategoryCost = new System.Windows.Forms.Button();
			this.buttonNewCategoryPrice = new System.Windows.Forms.Button();
			this.buttonPropertiesCategoryPrice = new System.Windows.Forms.Button();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.SuspendLayout();
			// 
			// listView1
			// 
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.columnHeader1,
																						this.columnHeader2,
																						this.columnHeader3});
			this.listView1.FullRowSelect = true;
			this.listView1.Location = new System.Drawing.Point(8, 72);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(680, 240);
			this.listView1.TabIndex = 0;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Категория работ";
			this.columnHeader1.Width = 300;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Стоимость Н/Ч";
			this.columnHeader2.Width = 180;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Стоимость Н/Ч гарантия";
			this.columnHeader3.Width = 180;
			// 
			// textBoxCategoryCost
			// 
			this.textBoxCategoryCost.Location = new System.Drawing.Point(368, 8);
			this.textBoxCategoryCost.Name = "textBoxCategoryCost";
			this.textBoxCategoryCost.ReadOnly = true;
			this.textBoxCategoryCost.Size = new System.Drawing.Size(320, 23);
			this.textBoxCategoryCost.TabIndex = 1;
			this.textBoxCategoryCost.Text = "";
			this.textBoxCategoryCost.Click += new System.EventHandler(this.textBoxCategoryCost_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(224, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(136, 23);
			this.label1.TabIndex = 2;
			this.label1.Text = "Ценовая категория";
			// 
			// textBoxMark
			// 
			this.textBoxMark.Location = new System.Drawing.Point(584, 40);
			this.textBoxMark.Name = "textBoxMark";
			this.textBoxMark.ReadOnly = true;
			this.textBoxMark.TabIndex = 3;
			this.textBoxMark.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(408, 40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(160, 23);
			this.label2.TabIndex = 4;
			this.label2.Text = "Условное обозначение";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(296, 320);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(88, 23);
			this.button1.TabIndex = 5;
			this.button1.Text = "Применить";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// buttonNewCategoryCost
			// 
			this.buttonNewCategoryCost.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonNewCategoryCost.Image")));
			this.buttonNewCategoryCost.Location = new System.Drawing.Point(8, 8);
			this.buttonNewCategoryCost.Name = "buttonNewCategoryCost";
			this.buttonNewCategoryCost.Size = new System.Drawing.Size(24, 23);
			this.buttonNewCategoryCost.TabIndex = 6;
			this.toolTip1.SetToolTip(this.buttonNewCategoryCost, "Добавление новой ценовой категории");
			this.buttonNewCategoryCost.Click += new System.EventHandler(this.buttonNewCategoryCost_Click);
			// 
			// buttonPropertiesCategoryCost
			// 
			this.buttonPropertiesCategoryCost.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonPropertiesCategoryCost.Image")));
			this.buttonPropertiesCategoryCost.Location = new System.Drawing.Point(32, 8);
			this.buttonPropertiesCategoryCost.Name = "buttonPropertiesCategoryCost";
			this.buttonPropertiesCategoryCost.Size = new System.Drawing.Size(24, 23);
			this.buttonPropertiesCategoryCost.TabIndex = 7;
			this.toolTip1.SetToolTip(this.buttonPropertiesCategoryCost, "Изменение выбранной ценовой категории");
			this.buttonPropertiesCategoryCost.Click += new System.EventHandler(this.buttonPropertiesCategoryCost_Click);
			// 
			// buttonNewCategoryPrice
			// 
			this.buttonNewCategoryPrice.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonNewCategoryPrice.Image")));
			this.buttonNewCategoryPrice.Location = new System.Drawing.Point(8, 48);
			this.buttonNewCategoryPrice.Name = "buttonNewCategoryPrice";
			this.buttonNewCategoryPrice.Size = new System.Drawing.Size(24, 23);
			this.buttonNewCategoryPrice.TabIndex = 8;
			this.toolTip1.SetToolTip(this.buttonNewCategoryPrice, "Добавление новой категории работ");
			this.buttonNewCategoryPrice.Click += new System.EventHandler(this.buttonNewCategoryPrice_Click);
			// 
			// buttonPropertiesCategoryPrice
			// 
			this.buttonPropertiesCategoryPrice.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonPropertiesCategoryPrice.Image")));
			this.buttonPropertiesCategoryPrice.Location = new System.Drawing.Point(32, 48);
			this.buttonPropertiesCategoryPrice.Name = "buttonPropertiesCategoryPrice";
			this.buttonPropertiesCategoryPrice.Size = new System.Drawing.Size(24, 23);
			this.buttonPropertiesCategoryPrice.TabIndex = 9;
			this.toolTip1.SetToolTip(this.buttonPropertiesCategoryPrice, "Изменение выбранной категории работ");
			this.buttonPropertiesCategoryPrice.Click += new System.EventHandler(this.buttonPropertiesCategoryPrice_Click);
			// 
			// FormManageCategoryCost
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.ClientSize = new System.Drawing.Size(696, 349);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.buttonPropertiesCategoryPrice,
																		  this.buttonNewCategoryPrice,
																		  this.buttonPropertiesCategoryCost,
																		  this.buttonNewCategoryCost,
																		  this.button1,
																		  this.label2,
																		  this.textBoxMark,
																		  this.label1,
																		  this.textBoxCategoryCost,
																		  this.listView1});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "FormManageCategoryCost";
			this.Text = "Ценовые категории";
			this.ResumeLayout(false);

		}
		#endregion

		private void textBoxCategoryCost_Click(object sender, System.EventArgs e)
		{
			// Вызов диалога выбора ценовой категории
			// Инициализируем форму выбора категории цены
			ArrayList	categoryCost = new ArrayList();
			DbCategoryCost.FillArray(categoryCost);
			FormSelectionType1 dialog = Db.MakeFormSelectionType1(this, textBoxCategoryCost, categoryCost, textBoxCategoryCost.Text, true);
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			if((DbCategoryCost)dialog.SelectedElement != null)
			{
				categoryCostSelection		= (DbCategoryCost)dialog.SelectedElement;
				textBoxCategoryCost.Text	= categoryCostSelection.DbTitle();
				textBoxMark.Text			= categoryCostSelection.Mark;
				// Инициируем процедуру загрузки таблицы стоимостей
				listView1.Items.Clear();
				DbCategoryPrice.FillList(listView1, categoryCostSelection);
				return;
			}
			// Инициируем ввод нового элемента!
			DialogResult res = MessageBox.Show("Уверены что хотите добавить новый элемент?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (res != DialogResult.Yes) return;
			FormCategoryCost dialog1 = new FormCategoryCost(null);
			dialog1.ShowDialog(this);
			if(dialog1.DialogResult != DialogResult.OK) return;
			// Выбираем вновь добавленный элемент
			categoryCostSelection		= dialog1.CategoryCost;
			textBoxCategoryCost.Text	= categoryCostSelection.DbTitle();
			textBoxMark.Text			= categoryCostSelection.Mark;
			// Инициируем процедуру загрузки таблицы стоимостей
			listView1.Items.Clear();
			DbCategoryPrice.FillList(listView1, categoryCostSelection);
		}

		private void listView1_DoubleClick(object sender, System.EventArgs e)
		{
			// Двойной щелчек на листе - предполагаем попытку изменения цены
			ListViewItem item = Db.GetItemPosition(listView1);
			if(item == null) return;
			DbCategoryPrice	categoryPrice = (DbCategoryPrice)item.Tag;
			if(categoryPrice == null) return;
			int column	= Db.GetColumnPosition(listView1);
			FormSelectionType2 dialog;
			switch(column)
			{
				case 1:
					dialog = Db.MakeFormSelectionType2(this, item, column, categoryPrice.PriceTxt);
					dialog.ShowDialog(this);
					if(dialog.DialogResult != DialogResult.OK) return;
					categoryPrice.Price	 = dialog.SelectedFloat;
					categoryPrice.SetLVItem(item);
					break;
				case 2:
					dialog = Db.MakeFormSelectionType2(this, item, column, categoryPrice.PriceGuarantyTxt);
					dialog.ShowDialog(this);
					if(dialog.DialogResult != DialogResult.OK) return;
					categoryPrice.PriceGuaranty	 = dialog.SelectedFloat;
					categoryPrice.SetLVItem(item);
					break;
				default:
					return;
			}
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			// Принимаем зделанные изменения, если есть
			foreach(ListViewItem item in listView1.Items)
			{
				DbCategoryPrice categoryPrice = (DbCategoryPrice)item.Tag;
				if(categoryPrice != null)
				{
					categoryPrice.Write(categoryCostSelection);
					categoryPrice.SetLVItem(item);
				}
			}
		}

		private void buttonNewCategoryCost_Click(object sender, System.EventArgs e)
		{
			DialogResult res = MessageBox.Show("Уверены что хотите добавить новый элемент?", "Подтверждение",  MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (res != DialogResult.Yes) return;
			FormCategoryCost dialog = new FormCategoryCost(null);
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			// Выбираем вновь добавленный элемент
			categoryCostSelection		= dialog.CategoryCost;
			textBoxCategoryCost.Text	= categoryCostSelection.DbTitle();
			textBoxMark.Text			= categoryCostSelection.Mark;
			// Инициируем процедуру загрузки таблицы стоимостей
			listView1.Items.Clear();
			DbCategoryPrice.FillList(listView1, categoryCostSelection);
			return;
		}

		private void buttonPropertiesCategoryCost_Click(object sender, System.EventArgs e)
		{
			if(categoryCostSelection == null) return;
			DialogResult res = MessageBox.Show("Уверены что хотите изменить элемент?", "Подтверждение",  MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (res != DialogResult.Yes) return;
			FormCategoryCost dialog = new FormCategoryCost(categoryCostSelection);
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			// Выбираем вновь добавленный элемент
			categoryCostSelection		= dialog.CategoryCost;
			textBoxCategoryCost.Text	= categoryCostSelection.DbTitle();
			textBoxMark.Text			= categoryCostSelection.Mark;
			return;
		}

		private void buttonNewCategoryPrice_Click(object sender, System.EventArgs e)
		{
			// Добавление новой категории работы
			if(DbCategoryPrice.Write((DbCategoryPrice)null) == null) return;
			// Инициируем процедуру загрузки таблицы стоимостей
			listView1.Items.Clear();
			DbCategoryPrice.FillList(listView1, categoryCostSelection);
		}

		private void buttonPropertiesCategoryPrice_Click(object sender, System.EventArgs e)
		{
			// Изменение категории работы
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			DbCategoryPrice categoryPrice = (DbCategoryPrice)item.Tag;
			if(categoryPrice == null) return;
			if(DbCategoryPrice.Write(categoryPrice) == null) return;
			// Инициируем процедуру загрузки таблицы стоимостей
			listView1.Items.Clear();
			DbCategoryPrice.FillList(listView1, categoryCostSelection);
		}
	}
}
