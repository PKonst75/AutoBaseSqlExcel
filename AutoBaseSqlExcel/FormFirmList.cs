using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormFirmList.
	/// </summary>
	public class FormFirmList : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.Button buttonNew;
		private System.Windows.Forms.Button buttonUpdate;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Button buttonChange;
		private System.Windows.Forms.ColumnHeader columnHeader2;

		private long selected_code;

		public FormFirmList()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			// Обновить список
			listView1.Items.Clear();
			DbSql.FillList(listView1, DbSqlFactory.select, new DbSql.DelegateMakeLVItem(DbSqlFactory.MakeLVItem));
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormFirmList));
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.buttonNew = new System.Windows.Forms.Button();
			this.buttonUpdate = new System.Windows.Forms.Button();
			this.buttonChange = new System.Windows.Forms.Button();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.SuspendLayout();
			// 
			// listView1
			// 
			this.listView1.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.columnHeader1,
																						this.columnHeader2});
			this.listView1.FullRowSelect = true;
			this.listView1.GridLines = true;
			this.listView1.Location = new System.Drawing.Point(8, 24);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(488, 240);
			this.listView1.TabIndex = 0;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Наименование";
			this.columnHeader1.Width = 300;
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
			// buttonChange
			// 
			this.buttonChange.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonChange.Image")));
			this.buttonChange.Location = new System.Drawing.Point(56, 0);
			this.buttonChange.Name = "buttonChange";
			this.buttonChange.Size = new System.Drawing.Size(24, 23);
			this.buttonChange.TabIndex = 3;
			this.buttonChange.Click += new System.EventHandler(this.buttonChange_Click);
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Префикс VIN";
			this.columnHeader2.Width = 120;
			// 
			// FormFirmList
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(504, 273);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.buttonChange,
																		  this.buttonUpdate,
																		  this.buttonNew,
																		  this.listView1});
			this.Name = "FormFirmList";
			this.Text = "Производители";
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonNew_Click(object sender, System.EventArgs e)
		{
			// Новый производитель
			FormFirm dialog = new FormFirm(0);
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			ListViewItem itm = listView1.Items.Add("");
			dialog.Factory.SetLVItem(itm);
		}

		private void buttonUpdate_Click(object sender, System.EventArgs e)
		{
			// Обновить список
			listView1.Items.Clear();
			DbSql.FillList(listView1, DbSqlFactory.select, new DbSql.DelegateMakeLVItem(DbSqlFactory.MakeLVItem));
		}

		protected void listView1_DoubleClick(object sender, EventArgs e)
		{
			ListViewItem item = Db.GetItemPosition(listView1);
			if(item == null) return;
			long code = (long)item.Tag;
			if(code == 0) return;
			selected_code = code;
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void buttonChange_Click(object sender, System.EventArgs e)
		{
			// Изменение свойств производителя
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			long code = (long)item.Tag;
			if(code == 0) return;
			FormFirm dialog = new FormFirm(code);
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			dialog.Factory.SetLVItem(item);
		}

		public long SelectedCode
		{
			get
			{
				return selected_code;
			}
		}
	}
}
