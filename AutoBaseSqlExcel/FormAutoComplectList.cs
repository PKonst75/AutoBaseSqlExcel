using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormAutoComplectList.
	/// </summary>
	public class FormAutoComplectList : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.Button buttonNew;
		private System.Windows.Forms.Button buttonUpdate;
		private System.Windows.Forms.Button buttonPropery;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		Db.ClickType		clickType;
		DbAutoModel			autoModel;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		DbAutoComplect		autoComplect;

		public FormAutoComplectList(Db.ClickType clickTypeSrc, DbAutoModel autoModelSrc)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			clickType		= clickTypeSrc;
			autoModel		= new DbAutoModel(autoModelSrc);

			// Установка названия окна
			this.Text		= "Комплектации для модели " + autoModel.Model;

			DbAutoComplect.FillList(listView1, autoModel, 0);
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormAutoComplectList));
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.buttonNew = new System.Windows.Forms.Button();
			this.buttonUpdate = new System.Windows.Forms.Button();
			this.buttonPropery = new System.Windows.Forms.Button();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
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
			this.listView1.FullRowSelect = true;
			this.listView1.GridLines = true;
			this.listView1.Location = new System.Drawing.Point(8, 24);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(504, 296);
			this.listView1.TabIndex = 0;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.DoubleClick += new EventHandler(this.listView1_DoubleClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Код комплектации";
			this.columnHeader1.Width = 120;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Описание";
			this.columnHeader2.Width = 250;
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
			// buttonPropery
			// 
			this.buttonPropery.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonPropery.Image")));
			this.buttonPropery.Location = new System.Drawing.Point(56, 0);
			this.buttonPropery.Name = "buttonPropery";
			this.buttonPropery.Size = new System.Drawing.Size(24, 23);
			this.buttonPropery.TabIndex = 3;
			this.buttonPropery.Click += new System.EventHandler(this.buttonPropery_Click);
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Года выпуска";
			this.columnHeader3.Width = 90;
			// 
			// FormAutoComplectList
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(520, 325);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.buttonPropery,
																		  this.buttonUpdate,
																		  this.buttonNew,
																		  this.listView1});
			this.Name = "FormAutoComplectList";
			this.Text = "Комплектации";
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonNew_Click(object sender, System.EventArgs e)
		{
			FormAutoComplect dialog = new FormAutoComplect(autoModel, null, "");
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			listView1.Items.Add(dialog.AutoComplect.LVItem);
		}

		private void buttonUpdate_Click(object sender, System.EventArgs e)
		{
			// Обновляем список моделей
			listView1.Items.Clear();
			DbAutoComplect.FillList(listView1, autoModel, 0);
		}

		private void buttonPropery_Click(object sender, System.EventArgs e)
		{
			// Выбор изменяемого элемента
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			DbAutoComplect element = (DbAutoComplect)item.Tag;
			if(element == null) return;
			// Вызов диалога изменения существующего элемента
			FormAutoComplect dialog = new FormAutoComplect(autoModel, element, "");
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			dialog.AutoComplect.SetLVItem(item);
		}

		public DbAutoComplect AutoComplect
		{
			get
			{
				return autoComplect;
			}
		}

		protected void listView1_DoubleClick(object sender, EventArgs e)
		{
			if(clickType == Db.ClickType.Select)
			{
				ListViewItem item = Db.GetItemPosition(listView1);
				if(item == null)
				{
					autoComplect = null;
					return;
				}
				autoComplect = (DbAutoComplect)item.Tag;
				if(autoComplect == null) return;
				this.DialogResult = DialogResult.OK;
				this.Close();
				return;
			}
		}
	}
}
