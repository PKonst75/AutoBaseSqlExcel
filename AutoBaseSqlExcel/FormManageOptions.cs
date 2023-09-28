using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormManageOptions.
	/// </summary>
	public class FormManageOptions : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.Button buttonNewOption;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.Button buttonOptionProperties;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public delegate void DelegateTransferToForm(ListViewItem item);
		public DelegateTransferToForm transferFunction = null;

		public FormManageOptions(DelegateTransferToForm srcTransferFunction)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			// Сразу заполним список
			DbOption.FillList(listView1);

			if(srcTransferFunction != null) transferFunction = srcTransferFunction;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormManageOptions));
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.buttonNewOption = new System.Windows.Forms.Button();
			this.buttonOptionProperties = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// listView1
			// 
			this.listView1.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.columnHeader1,
																						this.columnHeader2,
																						this.columnHeader3});
			this.listView1.Location = new System.Drawing.Point(8, 56);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(496, 312);
			this.listView1.TabIndex = 0;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.DoubleClick += new EventHandler(this.listView1_DoubleClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Наименование";
			this.columnHeader1.Width = 292;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Цена";
			this.columnHeader2.Width = 90;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Стоимость";
			this.columnHeader3.Width = 90;
			// 
			// buttonNewOption
			// 
			this.buttonNewOption.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonNewOption.Image")));
			this.buttonNewOption.Location = new System.Drawing.Point(8, 32);
			this.buttonNewOption.Name = "buttonNewOption";
			this.buttonNewOption.Size = new System.Drawing.Size(24, 23);
			this.buttonNewOption.TabIndex = 1;
			this.buttonNewOption.Click += new System.EventHandler(this.buttonNewOption_Click);
			// 
			// buttonOptionProperties
			// 
			this.buttonOptionProperties.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonOptionProperties.Image")));
			this.buttonOptionProperties.Location = new System.Drawing.Point(32, 32);
			this.buttonOptionProperties.Name = "buttonOptionProperties";
			this.buttonOptionProperties.Size = new System.Drawing.Size(24, 23);
			this.buttonOptionProperties.TabIndex = 2;
			this.buttonOptionProperties.Click += new System.EventHandler(this.buttonOptionProperties_Click);
			// 
			// FormManageOptions
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.ClientSize = new System.Drawing.Size(696, 381);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.buttonOptionProperties,
																		  this.buttonNewOption,
																		  this.listView1});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.Name = "FormManageOptions";
			this.Text = "Управление списком доп оборудования";
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonNewOption_Click(object sender, System.EventArgs e)
		{
			// Новая опция (доп оборудование)
			FormOption dialog = new FormOption(null);
			dialog.ShowDialog();
			if(dialog.DialogResult != DialogResult.OK) return;
			listView1.Items.Add(dialog.Option.LVItem);
		}

		private void buttonOptionProperties_Click(object sender, System.EventArgs e)
		{
			// Исправление выбранного элемента
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			DbOption element = (DbOption)item.Tag;
			if(element == null) return;

			FormOption dialog = new FormOption(element);
			dialog.ShowDialog();
			if(dialog.DialogResult != DialogResult.OK) return;
			dialog.Option.SetLVItem(item);
		}

		private void listView1_DoubleClick(object sender, EventArgs e)
		{
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			DbOption element = (DbOption)item.Tag;
			if(element == null) return;

			if(transferFunction == null)
			{
				return;
			}
			// Отправка на добавление в форму
			transferFunction(item);
		}
	}
}
