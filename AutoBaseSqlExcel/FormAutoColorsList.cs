using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormAutoColorsList.
	/// </summary>
	public class FormAutoColorsList : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.Button buttonNew;
		private System.Windows.Forms.Button buttonUpdate;
		private System.Windows.Forms.Button buttonProperty;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		Db.ClickType		clickType;
		DbAutoModel			autoModel;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		DbAutoColors		autoColors;


		public FormAutoColorsList(Db.ClickType clickTypeSrc, DbAutoModel autoModelSrc)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			clickType		= clickTypeSrc;
			autoModel		= new DbAutoModel(autoModelSrc);

			// Установка названия окна
			this.Text		= "Цвета для модели " + autoModel.Model;

			DbAutoColors.FillList(listView1, autoModel, 0);
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormAutoColorsList));
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.buttonNew = new System.Windows.Forms.Button();
			this.buttonUpdate = new System.Windows.Forms.Button();
			this.buttonProperty = new System.Windows.Forms.Button();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
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
																						this.columnHeader4});
			this.listView1.FullRowSelect = true;
			this.listView1.GridLines = true;
			this.listView1.Location = new System.Drawing.Point(8, 32);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(684, 316);
			this.listView1.TabIndex = 0;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Код цвета";
			this.columnHeader1.Width = 120;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Наименование";
			this.columnHeader2.Width = 200;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Описание";
			this.columnHeader3.Width = 200;
			// 
			// buttonNew
			// 
			this.buttonNew.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonNew.Image")));
			this.buttonNew.Location = new System.Drawing.Point(8, 8);
			this.buttonNew.Name = "buttonNew";
			this.buttonNew.Size = new System.Drawing.Size(24, 23);
			this.buttonNew.TabIndex = 1;
			this.buttonNew.Click += new System.EventHandler(this.buttonNew_Click);
			// 
			// buttonUpdate
			// 
			this.buttonUpdate.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonUpdate.Image")));
			this.buttonUpdate.Location = new System.Drawing.Point(32, 8);
			this.buttonUpdate.Name = "buttonUpdate";
			this.buttonUpdate.Size = new System.Drawing.Size(24, 23);
			this.buttonUpdate.TabIndex = 2;
			this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
			// 
			// buttonProperty
			// 
			this.buttonProperty.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonProperty.Image")));
			this.buttonProperty.Location = new System.Drawing.Point(56, 8);
			this.buttonProperty.Name = "buttonProperty";
			this.buttonProperty.Size = new System.Drawing.Size(24, 23);
			this.buttonProperty.TabIndex = 3;
			this.buttonProperty.Click += new System.EventHandler(this.buttonProperty_Click);
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Года выпуска";
			this.columnHeader4.Width = 120;
			// 
			// FormAutoColorsList
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(696, 349);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.buttonProperty,
																		  this.buttonUpdate,
																		  this.buttonNew,
																		  this.listView1});
			this.Name = "FormAutoColorsList";
			this.Text = "Цвета";
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonNew_Click(object sender, System.EventArgs e)
		{
			FormAutoColors dialog = new FormAutoColors(autoModel, null, "");
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			listView1.Items.Add(dialog.AutoColors.LVItem);
		}

		private void buttonUpdate_Click(object sender, System.EventArgs e)
		{
			// Обновляем список моделей
			listView1.Items.Clear();
			DbAutoColors.FillList(listView1, autoModel, 0);
		}

		private void buttonProperty_Click(object sender, System.EventArgs e)
		{
			// Выбор изменяемого элемента
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			DbAutoColors element = (DbAutoColors)item.Tag;
			if(element == null) return;
			// Вызов диалога изменения существующего элемента
			FormAutoColors dialog = new FormAutoColors(autoModel, element, "");
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			dialog.AutoColors.SetLVItem(item);
		}

		private void listView1_DoubleClick(object sender, EventArgs e)
		{
			if(clickType == Db.ClickType.Select)
			{
				ListViewItem item = Db.GetItemPosition(listView1);
				if(item == null)
				{
					autoColors = null;
					return;
				}
				autoColors = (DbAutoColors)item.Tag;
				if(autoColors == null) return;
				this.DialogResult = DialogResult.OK;
				this.Close();
				return;
			}
		}
		public DbAutoColors AutoColor
		{
			get
			{
				return autoColors;
			}
		}
	}
}
