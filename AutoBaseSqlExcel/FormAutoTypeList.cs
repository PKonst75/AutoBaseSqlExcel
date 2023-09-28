using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormAutoTypeList.
	/// </summary>
	public class FormAutoTypeList : System.Windows.Forms.Form
	{
		/* Элементы формы */
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.Button buttonUpdate;
		private System.Windows.Forms.Button buttonChange;
		private System.Windows.Forms.Button buttonNew;
		// Для функциональности окна
		private DbAutoType selectedAutoType = null;
		private System.Windows.Forms.Button buttonReplace;
		private System.Windows.Forms.Button buttonRemove;
		
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FormAutoTypeList()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormAutoTypeList));
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.buttonNew = new System.Windows.Forms.Button();
			this.buttonUpdate = new System.Windows.Forms.Button();
			this.buttonChange = new System.Windows.Forms.Button();
			this.buttonReplace = new System.Windows.Forms.Button();
			this.buttonRemove = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// listView1
			// 
			this.listView1.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.columnHeader1,
																						this.columnHeader3});
			this.listView1.FullRowSelect = true;
			this.listView1.GridLines = true;
			this.listView1.Location = new System.Drawing.Point(0, 24);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(526, 246);
			this.listView1.TabIndex = 0;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Наименование";
			this.columnHeader1.Width = 240;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Стоимость нормачаса";
			this.columnHeader3.Width = 120;
			// 
			// buttonNew
			// 
			this.buttonNew.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonNew.Image")));
			this.buttonNew.Name = "buttonNew";
			this.buttonNew.Size = new System.Drawing.Size(24, 24);
			this.buttonNew.TabIndex = 1;
			this.buttonNew.Click += new System.EventHandler(this.buttonNew_Click);
			// 
			// buttonUpdate
			// 
			this.buttonUpdate.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonUpdate.Image")));
			this.buttonUpdate.Location = new System.Drawing.Point(24, 0);
			this.buttonUpdate.Name = "buttonUpdate";
			this.buttonUpdate.Size = new System.Drawing.Size(24, 24);
			this.buttonUpdate.TabIndex = 2;
			this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
			// 
			// buttonChange
			// 
			this.buttonChange.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonChange.Image")));
			this.buttonChange.Location = new System.Drawing.Point(48, 0);
			this.buttonChange.Name = "buttonChange";
			this.buttonChange.Size = new System.Drawing.Size(24, 24);
			this.buttonChange.TabIndex = 3;
			this.buttonChange.Click += new System.EventHandler(this.buttonChange_Click);
			// 
			// buttonReplace
			// 
			this.buttonReplace.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonReplace.Image")));
			this.buttonReplace.Location = new System.Drawing.Point(176, 0);
			this.buttonReplace.Name = "buttonReplace";
			this.buttonReplace.Size = new System.Drawing.Size(24, 23);
			this.buttonReplace.TabIndex = 4;
			this.buttonReplace.Click += new System.EventHandler(this.buttonReplace_Click);
			// 
			// buttonRemove
			// 
			this.buttonRemove.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonRemove.Image")));
			this.buttonRemove.Location = new System.Drawing.Point(200, 0);
			this.buttonRemove.Name = "buttonRemove";
			this.buttonRemove.Size = new System.Drawing.Size(24, 23);
			this.buttonRemove.TabIndex = 5;
			this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
			// 
			// FormAutoTypeList
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(528, 273);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.buttonRemove,
																		  this.buttonReplace,
																		  this.buttonChange,
																		  this.buttonUpdate,
																		  this.buttonNew,
																		  this.listView1});
			this.MinimumSize = new System.Drawing.Size(400, 300);
			this.Name = "FormAutoTypeList";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Марки автомобилей (группы)";
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonNew_Click(object sender, System.EventArgs e)
		{
			// Вызов диалога добавления нового элемента
			FormAutoType dialog = new FormAutoType(null);
			if(dialog.ShowDialog(this) != DialogResult.OK) return;
			listView1.Items.Add(dialog.AutoType.LVItem);
		}

		private void buttonUpdate_Click(object sender, System.EventArgs e)
		{
			// Обновить лист
			listView1.Items.Clear();
			DbAutoType.FillList(listView1);
		}

		private void buttonChange_Click(object sender, System.EventArgs e)
		{
			// Изменение выбранного элемента
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			DbAutoType autoType = (DbAutoType)item.Tag;
			if(autoType == null) return;
			FormAutoType dialog = new FormAutoType(autoType);
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			dialog.AutoType.SetLVItem(item);
		}

		private void listView1_DoubleClick(object sender, System.EventArgs e)
		{
			// Определяем позицию элемента, но котором кликнули
			ListViewItem item = Db.GetItemPosition(listView1);
			if(item == null) return;
			DbAutoType autoType = (DbAutoType)item.Tag;
			if(autoType == null) return;
			// Далее идет различие диалога и не диалога
			if(this.Modal == true)
			{
				if(item.Tag != null)
				{
					selectedAutoType = autoType;
					this.DialogResult = DialogResult.OK;
					this.Close();
				}
			}
		}

		private void buttonReplace_Click(object sender, System.EventArgs e)
		{
			// Замена одной группы автомобилей на другую
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			DbAutoType autoType = (DbAutoType)item.Tag;
			if(autoType == null) return;

			// Просим выбрать новый тип автомобиля
			FormAutoTypeList dialog = new FormAutoTypeList();
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			
			autoType.Replace(dialog.SelectedAutoType);
		}

		private void buttonRemove_Click(object sender, System.EventArgs e)
		{
			// Удаление группы автомобилей
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			DbAutoType autoType = (DbAutoType)item.Tag;
			if(autoType == null) return;
			
			if(autoType.Remove() != true) return;
			item.Remove();
		}

		public DbAutoType SelectedAutoType
		{
			get
			{
				return selectedAutoType;
			}
		}
	}
}
