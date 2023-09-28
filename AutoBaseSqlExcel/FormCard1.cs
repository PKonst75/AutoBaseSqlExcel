using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormCard1.
	/// </summary>
	public class FormCard1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.ListView listViewCardWork;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxWorkSum;
		private System.Windows.Forms.Button buttonNewWork;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBoxAutoGroup;
		private System.Windows.Forms.Button buttonSelectAutoGroup;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBoxCategoryCost;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBoxPartner;
		private System.Windows.Forms.Button buttonSelectPartner;

		// Используемые параметры
		DbCard			card;					// Основная карточка заказа
		ArrayList		arrayCategoryPrice;		// Матрица цен на виды работ

		public FormCard1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			card		= new DbCard();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCard1));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonSelectPartner = new System.Windows.Forms.Button();
            this.textBoxPartner = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxCategoryCost = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonSelectAutoGroup = new System.Windows.Forms.Button();
            this.textBoxAutoGroup = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.buttonNewWork = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxWorkSum = new System.Windows.Forms.TextBox();
            this.listViewCardWork = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(682, 350);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(674, 317);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Свойства";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonSelectPartner);
            this.groupBox2.Controls.Add(this.textBoxPartner);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(11, 10);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(885, 152);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Клиент";
            // 
            // buttonSelectPartner
            // 
            this.buttonSelectPartner.Location = new System.Drawing.Point(843, 28);
            this.buttonSelectPartner.Name = "buttonSelectPartner";
            this.buttonSelectPartner.Size = new System.Drawing.Size(32, 28);
            this.buttonSelectPartner.TabIndex = 2;
            this.buttonSelectPartner.Text = "...";
            this.buttonSelectPartner.Click += new System.EventHandler(this.buttonSelectPartner_Click);
            // 
            // textBoxPartner
            // 
            this.textBoxPartner.Location = new System.Drawing.Point(107, 28);
            this.textBoxPartner.Name = "textBoxPartner";
            this.textBoxPartner.ReadOnly = true;
            this.textBoxPartner.Size = new System.Drawing.Size(736, 26);
            this.textBoxPartner.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(21, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 29);
            this.label4.TabIndex = 0;
            this.label4.Text = "Клиент";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxCategoryCost);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.buttonSelectAutoGroup);
            this.groupBox1.Controls.Add(this.textBoxAutoGroup);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(11, 171);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(885, 199);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Настройка";
            // 
            // textBoxCategoryCost
            // 
            this.textBoxCategoryCost.Location = new System.Drawing.Point(235, 76);
            this.textBoxCategoryCost.Name = "textBoxCategoryCost";
            this.textBoxCategoryCost.ReadOnly = true;
            this.textBoxCategoryCost.Size = new System.Drawing.Size(597, 26);
            this.textBoxCategoryCost.TabIndex = 4;
            this.textBoxCategoryCost.Click += new System.EventHandler(this.textBoxCategoryCost_Click);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(21, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(182, 27);
            this.label3.TabIndex = 3;
            this.label3.Text = "Ценовая категория";
            // 
            // buttonSelectAutoGroup
            // 
            this.buttonSelectAutoGroup.Location = new System.Drawing.Point(832, 28);
            this.buttonSelectAutoGroup.Name = "buttonSelectAutoGroup";
            this.buttonSelectAutoGroup.Size = new System.Drawing.Size(32, 28);
            this.buttonSelectAutoGroup.TabIndex = 2;
            this.buttonSelectAutoGroup.Text = "...";
            this.buttonSelectAutoGroup.Click += new System.EventHandler(this.buttonSelectAutoGroup_Click);
            // 
            // textBoxAutoGroup
            // 
            this.textBoxAutoGroup.Location = new System.Drawing.Point(235, 28);
            this.textBoxAutoGroup.Name = "textBoxAutoGroup";
            this.textBoxAutoGroup.ReadOnly = true;
            this.textBoxAutoGroup.Size = new System.Drawing.Size(597, 26);
            this.textBoxAutoGroup.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(21, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(203, 28);
            this.label2.TabIndex = 0;
            this.label2.Text = "Группа трудоемкостей";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.buttonNewWork);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.textBoxWorkSum);
            this.tabPage1.Controls.Add(this.listViewCardWork);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(909, 385);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Работы/Детали";
            // 
            // buttonNewWork
            // 
            this.buttonNewWork.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonNewWork.Location = new System.Drawing.Point(875, 10);
            this.buttonNewWork.Name = "buttonNewWork";
            this.buttonNewWork.Size = new System.Drawing.Size(32, 27);
            this.buttonNewWork.TabIndex = 3;
            this.buttonNewWork.Text = "button1";
            this.buttonNewWork.Click += new System.EventHandler(this.buttonNewWork_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(629, 194);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 27);
            this.label1.TabIndex = 2;
            this.label1.Text = "ИТОГО:";
            // 
            // textBoxWorkSum
            // 
            this.textBoxWorkSum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxWorkSum.Location = new System.Drawing.Point(725, 194);
            this.textBoxWorkSum.Name = "textBoxWorkSum";
            this.textBoxWorkSum.Size = new System.Drawing.Size(139, 26);
            this.textBoxWorkSum.TabIndex = 1;
            // 
            // listViewCardWork
            // 
            this.listViewCardWork.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewCardWork.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.listViewCardWork.FullRowSelect = true;
            this.listViewCardWork.HideSelection = false;
            this.listViewCardWork.Location = new System.Drawing.Point(11, 10);
            this.listViewCardWork.Name = "listViewCardWork";
            this.listViewCardWork.Size = new System.Drawing.Size(853, 180);
            this.listViewCardWork.SmallImageList = this.imageList1;
            this.listViewCardWork.TabIndex = 0;
            this.listViewCardWork.UseCompatibleStateImageBehavior = false;
            this.listViewCardWork.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "№";
            this.columnHeader1.Width = 39;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Позиция";
            this.columnHeader2.Width = 90;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Наименование";
            this.columnHeader3.Width = 273;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Количество";
            this.columnHeader4.Width = 54;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Расценка";
            this.columnHeader5.Width = 79;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Сумма";
            this.columnHeader6.Width = 88;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            // 
            // FormCard1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(8, 19);
            this.ClientSize = new System.Drawing.Size(704, 365);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "FormCard1";
            this.Text = "Заказ-няряд";
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		private void buttonNewWork_Click(object sender, System.EventArgs e)
		{
			// Запускаем окно выбора новой трудоемкости
			FormWorkSelect dialog = new FormWorkSelect(card.AutoType, new FormWorkSelect.DelegateTransferToForm(TransferToForm));
			dialog.ShowDialog();
		}

		private void buttonSelectAutoGroup_Click(object sender, System.EventArgs e)
		{
			// Выбор группы автомобиля (для расчета трудоемкостей)
			FormAutoTypeList dialog = new FormAutoTypeList();
			dialog.ShowDialog();
			if(dialog.DialogResult != DialogResult.OK) return;
			card.AutoType = dialog.SelectedAutoType;

			// Отображение сделанного выбора
			textBoxAutoGroup.Text = card.AutoTypeTxt;
		}

		public void WorkSumm()
		{
			float summ = 0;
			foreach(ListViewItem item in listViewCardWork.Items)
			{
				DbCardWork cardWork = (DbCardWork)item.Tag;
				if(cardWork != null)
				{
					if(cardWork.Deleted != true)
					{
						summ += cardWork.Summ;
					}
				}
			}

			textBoxWorkSum.Text = Db.CachToTxt(summ);
		}

		public void TransferToForm(ListViewItem item)
		{
			if(item == null) return;
			DbWork work			= (DbWork)item.Tag;
			if(work == null) return;
			DbCardWork cardWork	= new DbCardWork(work, card);
			cardWork.SetViewType(1);
			ListViewItem itm = listViewCardWork.Items.Add(cardWork.LVItem);
			cardWork.SetLVItem(itm);

			WorkSumm();
		}

		private void textBoxCategoryCost_Click(object sender, System.EventArgs e)
		{
			// Выбор ценовой категории данной карточки заказа
			ArrayList arrayCategoryCost	= new ArrayList();
			DbCategoryCost.FillArray(arrayCategoryCost);
			FormSelectionType1 dialog = Db.MakeFormSelectionType1(this.groupBox1, textBoxCategoryCost, arrayCategoryCost, "", false);
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			card.CategoryCost			= (DbCategoryCost)dialog.SelectedElement;
			textBoxCategoryCost.Text	= card.CategoryCostTxt;

			// Загружаем матрицу цен
			if(arrayCategoryPrice != null)
				arrayCategoryPrice.Clear();
			else
				arrayCategoryPrice	= new ArrayList();
			DbCategoryPrice.FillArray(arrayCategoryPrice, card.CategoryCost);
		}

		private void buttonSelectPartner_Click(object sender, System.EventArgs e)
		{
			// Инициируем выбор клиента
		}

		private void tabControl1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			string	message = "";
			bool	failed	= false;
			if(tabControl1.SelectedIndex == 1)
			{
				// Проверяем на возможность перехода
				if(card.AutoType == null)
				{
					message += "Необходимо выбрать группу трудоемкостей\n";
					failed	= true;
				}
				if(card.CategoryCost == null)
				{
					message += "Необходимо выбрать ценовую категорию\n";
					failed	= true;
				}
			}

			if(failed)
			{
				MessageBox.Show(message, "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				tabControl1.SelectedTab = tabPage2;
				return;
			}
		}
	}
}
