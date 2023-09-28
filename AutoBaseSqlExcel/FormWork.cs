using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormWork.
	/// </summary>
	public class FormWork : System.Windows.Forms.Form
	{
		// Элементы формы
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.TabPage tabPageMain;
		private System.Windows.Forms.TabPage tabPageLinks;
		private System.Windows.Forms.TextBox textBoxPosition;
		private System.Windows.Forms.TextBox textBoxCodeDetail;
		private System.Windows.Forms.TextBox textBoxCodeWork;
		private System.Windows.Forms.TextBox textBoxName;
		private System.Windows.Forms.TextBox textBoxDescript;
		private System.Windows.Forms.TextBox textBoxVal;
		private System.Windows.Forms.TextBox textBoxPrice;
		private System.Windows.Forms.TextBox textBoxPriceGuaranty;
		private System.Windows.Forms.Button buttonOk;		
		private System.Windows.Forms.Button buttonAddLink;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		// Для функциональности диалога
		private DbAutoType autoType = null;
		private DbWork work = null;
		private System.Windows.Forms.TextBox textBoxDirectoryWork;
		private System.Windows.Forms.Button buttonSelectDirectoryWork;
			
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FormWork(DbAutoType type, DbWork workSource, DbDirectoryWork directoryWork)
		{
			InitializeComponent();

			autoType = type;
			if(workSource != null)
			{
				work = new DbWork(workSource);
				textBoxPosition.ReadOnly = false;
			}
			else
			{
				if(directoryWork == null)
				{
					work = new DbWork(autoType);
					textBoxPosition.ReadOnly = false;
				}
				else
				{
					work = new DbWork(autoType, directoryWork);
				}
			}
			textBoxPosition.Text		= work.Position;
			textBoxCodeDetail.Text		= work.CodeDetail;
			textBoxCodeWork.Text		= work.CodeWork;
			textBoxName.Text			= work.Name;
			textBoxDescript.Text		= work.Description;
			textBoxVal.Text				= work.ValTxt;
			textBoxPrice.Text			= work.PriceTxt;
			textBoxPriceGuaranty.Text	= work.PriceGuarantyTxt;

			textBoxDirectoryWork.Text	= work.DirectoryWorkTxt;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormWork));
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPageMain = new System.Windows.Forms.TabPage();
			this.label8 = new System.Windows.Forms.Label();
			this.textBoxPriceGuaranty = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.textBoxPrice = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.textBoxVal = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.textBoxDescript = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textBoxName = new System.Windows.Forms.TextBox();
			this.textBoxCodeWork = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textBoxCodeDetail = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.textBoxPosition = new System.Windows.Forms.TextBox();
			this.tabPageLinks = new System.Windows.Forms.TabPage();
			this.buttonAddLink = new System.Windows.Forms.Button();
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.buttonOk = new System.Windows.Forms.Button();
			this.textBoxDirectoryWork = new System.Windows.Forms.TextBox();
			this.buttonSelectDirectoryWork = new System.Windows.Forms.Button();
			this.tabControl1.SuspendLayout();
			this.tabPageMain.SuspendLayout();
			this.tabPageLinks.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.AddRange(new System.Windows.Forms.Control[] {
																					  this.tabPageMain,
																					  this.tabPageLinks});
			this.tabControl1.Location = new System.Drawing.Point(0, 24);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(520, 320);
			this.tabControl1.TabIndex = 0;
			// 
			// tabPageMain
			// 
			this.tabPageMain.BackColor = System.Drawing.SystemColors.Control;
			this.tabPageMain.Controls.AddRange(new System.Windows.Forms.Control[] {
																					  this.buttonSelectDirectoryWork,
																					  this.textBoxDirectoryWork,
																					  this.label8,
																					  this.textBoxPriceGuaranty,
																					  this.label7,
																					  this.textBoxPrice,
																					  this.label6,
																					  this.textBoxVal,
																					  this.label5,
																					  this.textBoxDescript,
																					  this.label4,
																					  this.textBoxName,
																					  this.textBoxCodeWork,
																					  this.label3,
																					  this.label2,
																					  this.textBoxCodeDetail,
																					  this.label1,
																					  this.textBoxPosition});
			this.tabPageMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.tabPageMain.Location = new System.Drawing.Point(4, 22);
			this.tabPageMain.Name = "tabPageMain";
			this.tabPageMain.Size = new System.Drawing.Size(512, 294);
			this.tabPageMain.TabIndex = 0;
			this.tabPageMain.Text = "Свойства";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(264, 64);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(100, 32);
			this.label8.TabIndex = 15;
			this.label8.Text = "Гарантийный нормачас";
			// 
			// textBoxPriceGuaranty
			// 
			this.textBoxPriceGuaranty.Location = new System.Drawing.Point(392, 72);
			this.textBoxPriceGuaranty.Name = "textBoxPriceGuaranty";
			this.textBoxPriceGuaranty.TabIndex = 14;
			this.textBoxPriceGuaranty.Text = "textBox1";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(264, 40);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(100, 32);
			this.label7.TabIndex = 13;
			this.label7.Text = "Нормачас";
			// 
			// textBoxPrice
			// 
			this.textBoxPrice.Location = new System.Drawing.Point(392, 40);
			this.textBoxPrice.Name = "textBoxPrice";
			this.textBoxPrice.TabIndex = 12;
			this.textBoxPrice.Text = "textBox1";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(264, 8);
			this.label6.Name = "label6";
			this.label6.TabIndex = 11;
			this.label6.Text = "Трудоемкость";
			// 
			// textBoxVal
			// 
			this.textBoxVal.Location = new System.Drawing.Point(392, 8);
			this.textBoxVal.Name = "textBoxVal";
			this.textBoxVal.TabIndex = 10;
			this.textBoxVal.Text = "textBox1";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(8, 168);
			this.label5.Name = "label5";
			this.label5.TabIndex = 9;
			this.label5.Text = "Описание";
			// 
			// textBoxDescript
			// 
			this.textBoxDescript.Location = new System.Drawing.Point(120, 168);
			this.textBoxDescript.Multiline = true;
			this.textBoxDescript.Name = "textBoxDescript";
			this.textBoxDescript.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBoxDescript.Size = new System.Drawing.Size(384, 80);
			this.textBoxDescript.TabIndex = 8;
			this.textBoxDescript.Text = "textBox1";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 104);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(104, 23);
			this.label4.TabIndex = 7;
			this.label4.Text = "Наименование";
			// 
			// textBoxName
			// 
			this.textBoxName.Location = new System.Drawing.Point(120, 104);
			this.textBoxName.Multiline = true;
			this.textBoxName.Name = "textBoxName";
			this.textBoxName.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBoxName.Size = new System.Drawing.Size(384, 56);
			this.textBoxName.TabIndex = 6;
			this.textBoxName.Text = "textBox1";
			// 
			// textBoxCodeWork
			// 
			this.textBoxCodeWork.Location = new System.Drawing.Point(120, 72);
			this.textBoxCodeWork.Name = "textBoxCodeWork";
			this.textBoxCodeWork.TabIndex = 5;
			this.textBoxCodeWork.Text = "textBox1";
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.label3.Location = new System.Drawing.Point(8, 72);
			this.label3.Name = "label3";
			this.label3.TabIndex = 4;
			this.label3.Text = "Код работы";
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.label2.Location = new System.Drawing.Point(8, 40);
			this.label2.Name = "label2";
			this.label2.TabIndex = 3;
			this.label2.Text = "Код детали";
			// 
			// textBoxCodeDetail
			// 
			this.textBoxCodeDetail.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.textBoxCodeDetail.Location = new System.Drawing.Point(120, 40);
			this.textBoxCodeDetail.Name = "textBoxCodeDetail";
			this.textBoxCodeDetail.TabIndex = 2;
			this.textBoxCodeDetail.Text = "textBox1";
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.TabIndex = 1;
			this.label1.Text = "№ позиции";
			// 
			// textBoxPosition
			// 
			this.textBoxPosition.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.textBoxPosition.Location = new System.Drawing.Point(120, 8);
			this.textBoxPosition.Name = "textBoxPosition";
			this.textBoxPosition.TabIndex = 0;
			this.textBoxPosition.Text = "textBox1";
			// 
			// tabPageLinks
			// 
			this.tabPageLinks.Controls.AddRange(new System.Windows.Forms.Control[] {
																					   this.buttonAddLink,
																					   this.listView1});
			this.tabPageLinks.Location = new System.Drawing.Point(4, 22);
			this.tabPageLinks.Name = "tabPageLinks";
			this.tabPageLinks.Size = new System.Drawing.Size(512, 294);
			this.tabPageLinks.TabIndex = 1;
			this.tabPageLinks.Text = "Сопутсвующие";
			// 
			// buttonAddLink
			// 
			this.buttonAddLink.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonAddLink.Image")));
			this.buttonAddLink.Location = new System.Drawing.Point(8, 8);
			this.buttonAddLink.Name = "buttonAddLink";
			this.buttonAddLink.Size = new System.Drawing.Size(24, 23);
			this.buttonAddLink.TabIndex = 1;
			this.buttonAddLink.Click += new System.EventHandler(this.buttonAddLink_Click);
			// 
			// listView1
			// 
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.columnHeader1,
																						this.columnHeader2,
																						this.columnHeader3,
																						this.columnHeader4,
																						this.columnHeader5});
			this.listView1.FullRowSelect = true;
			this.listView1.GridLines = true;
			this.listView1.Location = new System.Drawing.Point(8, 32);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(496, 216);
			this.listView1.TabIndex = 0;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listView1_KeyDown);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "№ позиции";
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Код детали";
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Код работы";
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Ноименование";
			this.columnHeader4.Width = 180;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "Трудоемкость";
			// 
			// buttonOk
			// 
			this.buttonOk.Location = new System.Drawing.Point(224, 360);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.TabIndex = 1;
			this.buttonOk.Text = "ОК";
			this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
			// 
			// textBoxDirectoryWork
			// 
			this.textBoxDirectoryWork.Location = new System.Drawing.Point(16, 264);
			this.textBoxDirectoryWork.Name = "textBoxDirectoryWork";
			this.textBoxDirectoryWork.ReadOnly = true;
			this.textBoxDirectoryWork.Size = new System.Drawing.Size(448, 23);
			this.textBoxDirectoryWork.TabIndex = 16;
			this.textBoxDirectoryWork.Text = "";
			// 
			// buttonSelectDirectoryWork
			// 
			this.buttonSelectDirectoryWork.Location = new System.Drawing.Point(464, 264);
			this.buttonSelectDirectoryWork.Name = "buttonSelectDirectoryWork";
			this.buttonSelectDirectoryWork.Size = new System.Drawing.Size(24, 23);
			this.buttonSelectDirectoryWork.TabIndex = 17;
			this.buttonSelectDirectoryWork.Text = "...";
			this.buttonSelectDirectoryWork.Click += new System.EventHandler(this.buttonSelectDirectoryWork_Click);
			// 
			// FormWork
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(528, 383);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.buttonOk,
																		  this.tabControl1});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "FormWork";
			this.Text = "Трудоемкость";
			this.tabControl1.ResumeLayout(false);
			this.tabPageMain.ResumeLayout(false);
			this.tabPageLinks.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		protected override void OnCreateControl()
		{
			// Заполняем список сопутсвующих работ
			DbWork.FillListLinks(listView1, work);
		}

		private void buttonOk_Click(object sender, System.EventArgs e)
		{
			// Попытка изменить данные
			work.Position			= textBoxPosition.Text;
			work.CodeDetail			= textBoxCodeDetail.Text;
			work.CodeWork			= textBoxCodeWork.Text;
			work.Name				= textBoxName.Text;
			work.Description		= textBoxDescript.Text;
			work.ValTxt				= textBoxVal.Text;
			work.PriceTxt			= textBoxPrice.Text;
			work.PriceGuarantyTxt	= textBoxPriceGuaranty.Text;

			if(Db.ShowFaults()) return; // Проверка на сбои
			if(work.Write() != true) return;


			// Пробуем добавить-удалить сопутсвующие работы по списку
			int linkCounter = 0;
			foreach(ListViewItem item in listView1.Items)
			{
				DbWork wrk = (DbWork) item.Tag;
				if(wrk != null)
				{
					if(wrk.Exist == false)
					{
						DbWorkLink workLink = new DbWorkLink(work, wrk, true);
						if(workLink.Write() == true) linkCounter++;
					}
					else
					{
						if(wrk.Deleted == true)
						{
							DbWorkLink workLink = new DbWorkLink(work, wrk, false);
							if(workLink.Write() == false) linkCounter++;
						}
						else
						{
							linkCounter++;
						}
					}
				}
			}
			work.Links			= linkCounter;
			this.DialogResult	= DialogResult.OK;
			this.Close();
		}

		private void buttonAddLink_Click(object sender, System.EventArgs e)
		{
			// Добавка сопутсвующей работы в список
			FormWorkList dialog = new FormWorkList(autoType, listView1);
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
		}

		public DbWork Work
		{
			set
			{
				work = value;
			}
			get
			{
				return work;
			}
		}

		protected void listView1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			// Получаем выбранный элемент
			if(e.KeyCode == Keys.Delete)
			{
				DbWork wrk = null;
				if(listView1.SelectedItems == null) return;
				foreach(ListViewItem item in listView1.SelectedItems)
				{	
					wrk = (DbWork)item.Tag;
					if(wrk != null)
					{
						if(wrk.Exist)
						{
							if(wrk.Deleted == false)
							{
								wrk.Deleted = true;
							}
							else
							{
								wrk.Deleted = false;
							}
							wrk.SetLVItem(item);
						}
						else
						{
							listView1.Items.Remove(item);
						}
					}
				}
				listView1.SelectedItems.Clear();
			}
		}

		private void buttonSelectDirectoryWork_Click(object sender, System.EventArgs e)
		{
			// Выбираем новую работу из центрального справочника трудоемкостей
			FormManageDirectoryWork dialog = new FormManageDirectoryWork();
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			work.DirectoryWork = dialog.DirectoryWork;
			textBoxDirectoryWork.Text	= work.DirectoryWorkTxt;
		}
	}
}
