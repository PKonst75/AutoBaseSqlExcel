using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormModelList.
	/// </summary>
	public class FormModelList : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.Button buttonNew;
		private System.Windows.Forms.Button buttonChange;
		private System.Windows.Forms.Button buttonUpdate;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private Db.ClickType clickType;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.Button buttonDel;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.Button button_add_sell;
		private System.Windows.Forms.Button button_remove_sell;
		private DbAutoModel autoModel;

		public FormModelList(Db.ClickType clickTypeSrc)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			clickType = clickTypeSrc;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormModelList));
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.buttonNew = new System.Windows.Forms.Button();
			this.buttonChange = new System.Windows.Forms.Button();
			this.buttonUpdate = new System.Windows.Forms.Button();
			this.buttonDel = new System.Windows.Forms.Button();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.button_add_sell = new System.Windows.Forms.Button();
			this.button_remove_sell = new System.Windows.Forms.Button();
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
			this.listView1.Location = new System.Drawing.Point(8, 24);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(768, 240);
			this.listView1.TabIndex = 0;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Модель";
			this.columnHeader1.Width = 120;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Двигатель";
			this.columnHeader2.Width = 120;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Вид Гарантии";
			this.columnHeader3.Width = 200;
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
			// buttonChange
			// 
			this.buttonChange.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonChange.Image")));
			this.buttonChange.Location = new System.Drawing.Point(32, 0);
			this.buttonChange.Name = "buttonChange";
			this.buttonChange.Size = new System.Drawing.Size(24, 23);
			this.buttonChange.TabIndex = 2;
			this.buttonChange.Click += new System.EventHandler(this.buttonChange_Click);
			// 
			// buttonUpdate
			// 
			this.buttonUpdate.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonUpdate.Image")));
			this.buttonUpdate.Location = new System.Drawing.Point(56, 0);
			this.buttonUpdate.Name = "buttonUpdate";
			this.buttonUpdate.Size = new System.Drawing.Size(24, 23);
			this.buttonUpdate.TabIndex = 3;
			this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
			// 
			// buttonDel
			// 
			this.buttonDel.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonDel.Image")));
			this.buttonDel.Location = new System.Drawing.Point(400, 0);
			this.buttonDel.Name = "buttonDel";
			this.buttonDel.Size = new System.Drawing.Size(24, 23);
			this.buttonDel.TabIndex = 4;
			this.buttonDel.Click += new System.EventHandler(this.buttonDel_Click);
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "В продаже";
			this.columnHeader4.Width = 90;
			// 
			// button_add_sell
			// 
			this.button_add_sell.Image = ((System.Drawing.Bitmap)(resources.GetObject("button_add_sell.Image")));
			this.button_add_sell.Location = new System.Drawing.Point(120, 0);
			this.button_add_sell.Name = "button_add_sell";
			this.button_add_sell.Size = new System.Drawing.Size(24, 23);
			this.button_add_sell.TabIndex = 5;
			this.button_add_sell.Click += new System.EventHandler(this.button_add_sell_Click);
			// 
			// button_remove_sell
			// 
			this.button_remove_sell.Image = ((System.Drawing.Bitmap)(resources.GetObject("button_remove_sell.Image")));
			this.button_remove_sell.Location = new System.Drawing.Point(144, 0);
			this.button_remove_sell.Name = "button_remove_sell";
			this.button_remove_sell.Size = new System.Drawing.Size(24, 23);
			this.button_remove_sell.TabIndex = 6;
			this.button_remove_sell.Click += new System.EventHandler(this.button_remove_sell_Click);
			// 
			// FormModelList
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(784, 273);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.button_remove_sell,
																		  this.button_add_sell,
																		  this.buttonDel,
																		  this.buttonUpdate,
																		  this.buttonChange,
																		  this.buttonNew,
																		  this.listView1});
			this.Name = "FormModelList";
			this.Text = "Модели автомобилей";
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonNew_Click(object sender, System.EventArgs e)
		{
			// Добавление нового элемента в список моделей
			FormAutoModel dialog = new FormAutoModel(null);
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			listView1.Items.Add(dialog.AutoModel.LVItem);
		}

		private void buttonUpdate_Click(object sender, System.EventArgs e)
		{
			// Обновление списка моделей
			listView1.Items.Clear();
			DbAutoModel.FillList(listView1);
			//DbSqlModel.SelectInListWide(listView1);
		}

		private void buttonChange_Click(object sender, System.EventArgs e)
		{
			// Изменение элемента в списке моделей
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			DbAutoModel element = (DbAutoModel)item.Tag;
			if(element == null) return;
			FormAutoModel dialog = new FormAutoModel(element);
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			dialog.AutoModel.SetLVItem(item);
		}

		private void listView1_DoubleClick(object sender, System.EventArgs e)
		{
			ListViewItem item = Db.GetItemPosition(listView1);
			if(item == null) return;
			DbAutoModel element = (DbAutoModel)item.Tag;
			if(element == null) return;

			if(clickType == Db.ClickType.Select)
			{
				autoModel = element;
				this.DialogResult = DialogResult.OK;
				this.Close();
				return;
			}
		}

		private void buttonDel_Click(object sender, System.EventArgs e)
		{
			// Удаление модели автомобиля
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			DbAutoModel autoModel = (DbAutoModel)item.Tag;
			if(autoModel == null) return;

			if(!autoModel.Delete()) return;
			listView1.Items.Remove(item);
		}

		private void button_add_sell_Click(object sender, System.EventArgs e)
		{
			// Добавляем модель в список действующих продаж
		}

		private void button_remove_sell_Click(object sender, System.EventArgs e)
		{
			// Убираем модель из списка действующих продаж
		}

		public DbAutoModel AutoModel
		{
			get
			{
				return autoModel;
			}
		}
	}
}
