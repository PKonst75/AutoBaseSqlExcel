using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for UIF_ServiceOuter_List.
	/// </summary>
	public class UIF_ServiceOuter_List : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.Button button1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public DtServiceOuter service_selected = null;

		public UIF_ServiceOuter_List()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			FillList();
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(UIF_ServiceOuter_List));
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.button1 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// listView1
			// 
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.columnHeader1,
																						this.columnHeader2});
			this.listView1.Location = new System.Drawing.Point(8, 32);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(640, 232);
			this.listView1.TabIndex = 0;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Наименование";
			this.columnHeader1.Width = 204;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Адрес";
			this.columnHeader2.Width = 401;
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
			// UIF_ServiceOuter_List
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(656, 273);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.button1,
																		  this.listView1});
			this.Name = "UIF_ServiceOuter_List";
			this.Text = "Сторонние сервисы";
			this.ResumeLayout(false);

		}
		#endregion

		private void button1_Click(object sender, System.EventArgs e)
		{
			// Новый сервис
			UIF_ServiceOuter dialog = new UIF_ServiceOuter();
			if(dialog.ShowDialog() != DialogResult.OK) return;

			service_selected = dialog.service_selected;
			DialogResult = DialogResult.OK;
			this.Close();
		}

		private void listView1_DoubleClick(object sender, System.EventArgs e)
		{
			// Двойной счелчек - выбор элемента во внешний список
			ListViewItem item = Db.GetItemPosition(listView1);
			if(item == null) return;
			long code = (long)item.Tag;
			if(code == 0) return;
			DtServiceOuter service = DbSqlServiceOuter.Find(code);
			if(service == null) return;
			service_selected = service;
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		void FillList()
		{
			listView1.Items.Clear();
			DbSqlServiceOuter.SelectInList(listView1);
		}
	}
}
