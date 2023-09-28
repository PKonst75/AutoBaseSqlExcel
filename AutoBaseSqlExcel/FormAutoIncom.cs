using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormAutoIncom.
	/// </summary>
	public class FormAutoIncom : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button buttonAuto;
		private System.Windows.Forms.Button buttonIncomeDate;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Button buttonSelectDocument;
		private System.ComponentModel.IContainer components;

		public FormAutoIncom()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormAutoIncom));
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.button1 = new System.Windows.Forms.Button();
			this.buttonAuto = new System.Windows.Forms.Button();
			this.buttonIncomeDate = new System.Windows.Forms.Button();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.buttonSelectDocument = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// listView1
			// 
			this.listView1.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.listView1.CheckBoxes = true;
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.columnHeader1,
																						this.columnHeader2,
																						this.columnHeader3,
																						this.columnHeader4,
																						this.columnHeader5,
																						this.columnHeader6});
			this.listView1.FullRowSelect = true;
			this.listView1.HideSelection = false;
			this.listView1.Location = new System.Drawing.Point(8, 32);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(728, 296);
			this.listView1.TabIndex = 0;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView1_ColumnClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Модель";
			this.columnHeader1.Width = 110;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "VIN";
			this.columnHeader2.Width = 126;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Спецификация";
			this.columnHeader3.Width = 110;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Комплектация";
			this.columnHeader4.Width = 108;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "Цвет";
			this.columnHeader5.Width = 134;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "Дата прихода";
			this.columnHeader6.Width = 104;
			// 
			// button1
			// 
			this.button1.Image = ((System.Drawing.Bitmap)(resources.GetObject("button1.Image")));
			this.button1.Location = new System.Drawing.Point(8, 8);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(24, 23);
			this.button1.TabIndex = 1;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// buttonAuto
			// 
			this.buttonAuto.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonAuto.Image")));
			this.buttonAuto.Location = new System.Drawing.Point(32, 8);
			this.buttonAuto.Name = "buttonAuto";
			this.buttonAuto.Size = new System.Drawing.Size(24, 23);
			this.buttonAuto.TabIndex = 2;
			this.buttonAuto.Click += new System.EventHandler(this.buttonAuto_Click);
			// 
			// buttonIncomeDate
			// 
			this.buttonIncomeDate.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonIncomeDate.Image")));
			this.buttonIncomeDate.Location = new System.Drawing.Point(200, 8);
			this.buttonIncomeDate.Name = "buttonIncomeDate";
			this.buttonIncomeDate.Size = new System.Drawing.Size(24, 23);
			this.buttonIncomeDate.TabIndex = 3;
			this.toolTip1.SetToolTip(this.buttonIncomeDate, "Отметить приход автомобилей");
			this.buttonIncomeDate.Click += new System.EventHandler(this.buttonIncomeDate_Click);
			// 
			// buttonSelectDocument
			// 
			this.buttonSelectDocument.Location = new System.Drawing.Point(296, 8);
			this.buttonSelectDocument.Name = "buttonSelectDocument";
			this.buttonSelectDocument.Size = new System.Drawing.Size(24, 24);
			this.buttonSelectDocument.TabIndex = 4;
			this.toolTip1.SetToolTip(this.buttonSelectDocument, "Выбор автомобилей по документу");
			this.buttonSelectDocument.Click += new System.EventHandler(this.buttonSelectDocument_Click);
			// 
			// FormAutoIncom
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.ClientSize = new System.Drawing.Size(744, 333);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.buttonSelectDocument,
																		  this.buttonIncomeDate,
																		  this.buttonAuto,
																		  this.button1,
																		  this.listView1});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.Name = "FormAutoIncom";
			this.Text = "Ожидаемый приход";
			this.ResumeLayout(false);

		}
		#endregion

		private void button1_Click(object sender, System.EventArgs e)
		{
			// Обновить список
			listView1.Items.Clear();
			DbAutoIncom.FillListNotIncom(listView1);
		}

		private void buttonAuto_Click(object sender, System.EventArgs e)
		{
			// Вызов диалога свойств автомобиля
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			DbAutoIncom element = (DbAutoIncom)item.Tag;
			if(element == null) return;
			if(element.Auto == null) return;

			FormAuto dialog = new FormAuto(element.Auto);
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			element.Auto = dialog.Auto;
			element.SetLVItem(item);
		}

		private void buttonIncomeDate_Click(object sender, System.EventArgs e)
		{
			// Отмечаем приход автомобиля на склад
			FormSelectDate dialog = new FormSelectDate();
			dialog.ShowDialog();
			if(dialog.DialogResult != DialogResult.OK) return;
			DateTime date = dialog.SelectedDate;

			foreach(ListViewItem item in listView1.Items)
			{
				if(item.Checked)
				{
					// Добавляем в склад
					DbAutoIncom autoIncom = (DbAutoIncom)item.Tag;
					if(autoIncom != null)
					{
						DbAutoStorage autoStorage = new DbAutoStorage(autoIncom, date);
						if(autoStorage.Write()) item.Remove();
					}
				}
			}
			Db.ShowFaults();
		}

		protected void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
		{
			if(e.Column == 1)
			{
				// Поиск по вину или кузову
				FormSelectString dialog = new FormSelectString("Поиск по кузову/VINу", "");
				dialog.ShowDialog();
				if(dialog.DialogResult != DialogResult.OK) return;
				listView1.Items.Clear();
				DbAutoIncom.FillListNotIncomSearchBody(listView1, dialog.SelectedTextMask);
			}
		}

		private void buttonSelectDocument_Click(object sender, System.EventArgs e)
		{
			// Выбор всех автомобилей по определенному документу
			FormAutoFacturaList dialog = new FormAutoFacturaList(Db.ClickType.Select);
			dialog.ShowDialog();
			if(dialog.DialogResult != DialogResult.OK) return;
			listView1.Items.Clear();
			DbAutoIncom.FillListNotIncomDocument(listView1, dialog.SelectedDocument);
		}
	}
}
