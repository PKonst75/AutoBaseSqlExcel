using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormSubmodelList.
	/// </summary>
	public class FormSubmodelList : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.Button buttonNew;
		private System.Windows.Forms.Button buttonUpdate;
		private System.Windows.Forms.Button buttonProperty;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		Db.ClickType		clickType;
		DbAutoModel			autoModel;
		DbAutoSubmodel		autoSubmodel;

		public FormSubmodelList(Db.ClickType clickTypeSrc, DbAutoModel autoModelSrc)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			clickType		= clickTypeSrc;
			autoModel		= new DbAutoModel(autoModelSrc);

			// Установка названия окна
			this.Text		= "Подмодели для " + autoModel.Model;

			DbAutoSubmodel.FillList(listView1, autoModel, 0);
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormSubmodelList));
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
			this.buttonNew = new System.Windows.Forms.Button();
			this.buttonUpdate = new System.Windows.Forms.Button();
			this.buttonProperty = new System.Windows.Forms.Button();
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
																						this.columnHeader3,
																						this.columnHeader4,
																						this.columnHeader5,
																						this.columnHeader6,
																						this.columnHeader7});
			this.listView1.FullRowSelect = true;
			this.listView1.GridLines = true;
			this.listView1.Location = new System.Drawing.Point(8, 24);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(680, 320);
			this.listView1.TabIndex = 0;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.DoubleClick += new EventHandler(listView1_DoubleClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Модель";
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Двигатель";
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Двигатель - описание";
			this.columnHeader3.Width = 200;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "КПП";
			this.columnHeader4.Width = 120;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "4х4";
			this.columnHeader5.Width = 30;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "Кузов";
			this.columnHeader6.Width = 80;
			// 
			// columnHeader7
			// 
			this.columnHeader7.Text = "Года выпуска";
			this.columnHeader7.Width = 90;
			// 
			// buttonNew
			// 
			this.buttonNew.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonNew.Image")));
			this.buttonNew.Location = new System.Drawing.Point(8, 0);
			this.buttonNew.Name = "buttonNew";
			this.buttonNew.Size = new System.Drawing.Size(24, 23);
			this.buttonNew.TabIndex = 1;
			this.buttonNew.Click += new System.EventHandler(this.buttonNew_Click);
			// 
			// buttonUpdate
			// 
			this.buttonUpdate.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonUpdate.Image")));
			this.buttonUpdate.Location = new System.Drawing.Point(32, 0);
			this.buttonUpdate.Name = "buttonUpdate";
			this.buttonUpdate.Size = new System.Drawing.Size(24, 23);
			this.buttonUpdate.TabIndex = 2;
			this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
			// 
			// buttonProperty
			// 
			this.buttonProperty.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonProperty.Image")));
			this.buttonProperty.Location = new System.Drawing.Point(56, 0);
			this.buttonProperty.Name = "buttonProperty";
			this.buttonProperty.Size = new System.Drawing.Size(24, 23);
			this.buttonProperty.TabIndex = 3;
			this.buttonProperty.Click += new System.EventHandler(this.buttonProperty_Click);
			// 
			// FormSubmodelList
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(696, 349);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.buttonProperty,
																		  this.buttonUpdate,
																		  this.buttonNew,
																		  this.listView1});
			this.Name = "FormSubmodelList";
			this.Text = "Подмодели";
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonNew_Click(object sender, System.EventArgs e)
		{
			// Вызов диалога создания нового элемента
			FormAutoSubmodel dialog = new FormAutoSubmodel(autoModel, null, "");
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			listView1.Items.Add(dialog.AutoSubmodel.LVItem);
		}

		private void buttonUpdate_Click(object sender, System.EventArgs e)
		{
			// Обновляем список моделей
			listView1.Items.Clear();
			DbAutoSubmodel.FillList(listView1, autoModel, 0);
		}

		private void buttonProperty_Click(object sender, System.EventArgs e)
		{
			// Выбор изменяемого элемента
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			DbAutoSubmodel element = (DbAutoSubmodel)item.Tag;
			if(element == null) return;
			// Вызов диалога изменения существующего элемента
			FormAutoSubmodel dialog = new FormAutoSubmodel(autoModel, element, "");
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			dialog.AutoSubmodel.SetLVItem(item);
		}

		private void listView1_DoubleClick(object sender, EventArgs e)
		{
			if(clickType == Db.ClickType.Select)
			{
				ListViewItem item = Db.GetItemPosition(listView1);
				if(item == null)
				{
					autoSubmodel = null;
					return;
				}
				autoSubmodel = (DbAutoSubmodel)item.Tag;
				if(autoSubmodel == null) return;
				this.DialogResult = DialogResult.OK;
				this.Close();
				return;
			}
		}
		public DbAutoSubmodel AutoSubModel
		{
			get
			{
				return autoSubmodel;
			}
		}
	}
}
