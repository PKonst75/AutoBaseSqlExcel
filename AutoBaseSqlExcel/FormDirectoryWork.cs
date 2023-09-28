using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormDirectoryWork.
	/// </summary>
	public class FormDirectoryWork : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button buttonOk;

		private	bool ourChange						= false;
		private DbDirectoryWork	directoryWork		= null;
		private System.Windows.Forms.CheckBox checkBoxGroupFlag;
		private System.Windows.Forms.TextBox textBoxCategorySearch;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBoxCategoryPrice;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.CheckBox checkBoxDiscount;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FormDirectoryWork(DbDirectoryWork directoryWorkSrc)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			if(directoryWorkSrc == null)
			{
				directoryWork	= new DbDirectoryWork();
			}
			else
			{
				directoryWork	= new DbDirectoryWork(directoryWorkSrc);
				directoryWork.LoadCategorySearch();
				directoryWork.LoadCategoryPrice();
			}
			textBox1.Text				= directoryWork.Name;
			checkBoxGroupFlag.Checked	= directoryWork.GroupFlag;
			checkBoxDiscount.Checked	= directoryWork.Discount;
			textBoxCategorySearch.Text	= directoryWork.CategorySearchTxt;
			textBoxCategoryPrice.Text	= directoryWork.CategoryPriceTxt;
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
			this.label1 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.buttonOk = new System.Windows.Forms.Button();
			this.checkBoxGroupFlag = new System.Windows.Forms.CheckBox();
			this.textBoxCategorySearch = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textBoxCategoryPrice = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.checkBoxDiscount = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(176, 24);
			this.label1.TabIndex = 0;
			this.label1.Text = "Наименование элемента";
			// 
			// textBox1
			// 
			this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.textBox1.Location = new System.Drawing.Point(8, 40);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(624, 23);
			this.textBox1.TabIndex = 1;
			this.textBox1.Text = "textBox1";
			this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
			// 
			// buttonOk
			// 
			this.buttonOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.buttonOk.Location = new System.Drawing.Point(280, 184);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.TabIndex = 2;
			this.buttonOk.Text = "ОК";
			this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
			// 
			// checkBoxGroupFlag
			// 
			this.checkBoxGroupFlag.Location = new System.Drawing.Point(8, 72);
			this.checkBoxGroupFlag.Name = "checkBoxGroupFlag";
			this.checkBoxGroupFlag.Size = new System.Drawing.Size(176, 24);
			this.checkBoxGroupFlag.TabIndex = 3;
			this.checkBoxGroupFlag.Text = "Группируемая работа";
			// 
			// textBoxCategorySearch
			// 
			this.textBoxCategorySearch.Location = new System.Drawing.Point(184, 112);
			this.textBoxCategorySearch.Name = "textBoxCategorySearch";
			this.textBoxCategorySearch.ReadOnly = true;
			this.textBoxCategorySearch.Size = new System.Drawing.Size(448, 23);
			this.textBoxCategorySearch.TabIndex = 4;
			this.textBoxCategorySearch.Text = "";
			this.textBoxCategorySearch.Click += new System.EventHandler(this.textBoxCategorySearch_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 112);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(144, 23);
			this.label2.TabIndex = 5;
			this.label2.Text = "Категория поиска";
			// 
			// textBoxCategoryPrice
			// 
			this.textBoxCategoryPrice.Location = new System.Drawing.Point(184, 144);
			this.textBoxCategoryPrice.Name = "textBoxCategoryPrice";
			this.textBoxCategoryPrice.ReadOnly = true;
			this.textBoxCategoryPrice.Size = new System.Drawing.Size(448, 23);
			this.textBoxCategoryPrice.TabIndex = 6;
			this.textBoxCategoryPrice.Text = "";
			this.textBoxCategoryPrice.Click += new System.EventHandler(this.textBoxCategoryPrice_Click);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 144);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(128, 23);
			this.label3.TabIndex = 7;
			this.label3.Text = "Ценовая категория";
			// 
			// checkBoxDiscount
			// 
			this.checkBoxDiscount.Location = new System.Drawing.Point(216, 72);
			this.checkBoxDiscount.Name = "checkBoxDiscount";
			this.checkBoxDiscount.TabIndex = 8;
			this.checkBoxDiscount.Text = "Скидка";
			// 
			// FormDirectoryWork
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.ClientSize = new System.Drawing.Size(640, 223);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.checkBoxDiscount,
																		  this.label3,
																		  this.textBoxCategoryPrice,
																		  this.label2,
																		  this.textBoxCategorySearch,
																		  this.checkBoxGroupFlag,
																		  this.buttonOk,
																		  this.textBox1,
																		  this.label1});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "FormDirectoryWork";
			this.Text = "Справочник трудоемкостей";
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonOk_Click(object sender, System.EventArgs e)
		{
			// Запись/редактирование элемента справочника трудоемкостей
			directoryWork.Name			= textBox1.Text;
			directoryWork.GroupFlag		= checkBoxGroupFlag.Checked;
			directoryWork.Discount		= checkBoxDiscount.Checked;
			if(Db.ShowFaults() == true) return;

			if(directoryWork.Write() == false) return;
			this.DialogResult	= DialogResult.OK;
			this.Close();
		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{
			// Постоянный контроль вводимого текста
			if (ourChange == true)
			{
				ourChange = false;
				return;
			}
			// Первым делом ставим заглавной первую букву
			string tmpText = Db.FirstUpperSpace(textBox1.Text);
			if(tmpText != textBox1.Text)
			{
				// Если есть изменения - производим их
				ourChange = true;
				int selectionStart = textBox1.SelectionStart;
				textBox1.Text = tmpText;
				if(selectionStart > 0)
					textBox1.SelectionStart = selectionStart;
			}
		}

		private void textBoxCategorySearch_Click(object sender, System.EventArgs e)
		{
			// Инициализируем форму выбора категории поиска
			ArrayList	categorySearch = new ArrayList();
			DbCategorySearch.FillArray(categorySearch);
			FormSelectionType1 dialog = Db.MakeFormSelectionType1(this, textBoxCategorySearch, categorySearch, directoryWork.CategorySearchTxt, true);
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			directoryWork.CategorySearch = (DbCategorySearch)dialog.SelectedElement;
			textBoxCategorySearch.Text	= directoryWork.CategorySearchTxt;
		}

		private void textBoxCategoryPrice_Click(object sender, System.EventArgs e)
		{
			// Инициализируем форму выбора категории цены
			ArrayList	categoryPrice = new ArrayList();
			DbCategoryPrice.FillArray(categoryPrice);
			FormSelectionType1 dialog = Db.MakeFormSelectionType1(this, textBoxCategoryPrice, categoryPrice, directoryWork.CategoryPriceTxt, true);
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			directoryWork.CategoryPrice = (DbCategoryPrice)dialog.SelectedElement;
			textBoxCategoryPrice.Text	= directoryWork.CategoryPriceTxt;
		}

		public DbDirectoryWork DirectoryWork
		{
			get
			{
				return directoryWork;
			}
		}
	}
}
