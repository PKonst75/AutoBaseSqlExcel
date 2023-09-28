using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormDetailOutcomList.
	/// </summary>
	public class FormDetailOutcomList : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.Button buttonNew;
		private System.Windows.Forms.Button buttonUpdate;
		private System.Windows.Forms.Button buttonPrint;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.Button buttonSetImplement;
		private System.Windows.Forms.Button buttonSetNotImplement;
		private System.Windows.Forms.Button buttonDelete;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FormDetailOutcomList()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormDetailOutcomList));
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.buttonNew = new System.Windows.Forms.Button();
			this.buttonUpdate = new System.Windows.Forms.Button();
			this.buttonPrint = new System.Windows.Forms.Button();
			this.buttonSetImplement = new System.Windows.Forms.Button();
			this.buttonSetNotImplement = new System.Windows.Forms.Button();
			this.buttonDelete = new System.Windows.Forms.Button();
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
			this.listView1.Size = new System.Drawing.Size(616, 292);
			this.listView1.TabIndex = 0;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Номер";
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Дата";
			this.columnHeader2.Width = 90;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Сумма";
			this.columnHeader3.Width = 80;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Основание";
			this.columnHeader4.Width = 240;
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
			// buttonPrint
			// 
			this.buttonPrint.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonPrint.Image")));
			this.buttonPrint.Location = new System.Drawing.Point(56, 8);
			this.buttonPrint.Name = "buttonPrint";
			this.buttonPrint.Size = new System.Drawing.Size(24, 23);
			this.buttonPrint.TabIndex = 3;
			this.buttonPrint.Click += new System.EventHandler(this.buttonPrint_Click);
			// 
			// buttonSetImplement
			// 
			this.buttonSetImplement.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonSetImplement.Image")));
			this.buttonSetImplement.Location = new System.Drawing.Point(160, 8);
			this.buttonSetImplement.Name = "buttonSetImplement";
			this.buttonSetImplement.Size = new System.Drawing.Size(24, 23);
			this.buttonSetImplement.TabIndex = 4;
			this.buttonSetImplement.Click += new System.EventHandler(this.buttonSetImplement_Click);
			// 
			// buttonSetNotImplement
			// 
			this.buttonSetNotImplement.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonSetNotImplement.Image")));
			this.buttonSetNotImplement.Location = new System.Drawing.Point(184, 8);
			this.buttonSetNotImplement.Name = "buttonSetNotImplement";
			this.buttonSetNotImplement.Size = new System.Drawing.Size(24, 23);
			this.buttonSetNotImplement.TabIndex = 5;
			this.buttonSetNotImplement.Click += new System.EventHandler(this.buttonSetNotImplement_Click);
			// 
			// buttonDelete
			// 
			this.buttonDelete.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonDelete.Image")));
			this.buttonDelete.Location = new System.Drawing.Point(136, 8);
			this.buttonDelete.Name = "buttonDelete";
			this.buttonDelete.Size = new System.Drawing.Size(24, 23);
			this.buttonDelete.TabIndex = 6;
			this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
			// 
			// FormDetailOutcomList
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(632, 333);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.buttonDelete,
																		  this.buttonSetNotImplement,
																		  this.buttonSetImplement,
																		  this.buttonPrint,
																		  this.buttonUpdate,
																		  this.buttonNew,
																		  this.listView1});
			this.Name = "FormDetailOutcomList";
			this.Text = "Требования";
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonNew_Click(object sender, System.EventArgs e)
		{
			// Новое требование-накладная
			FormDetailOutcomDoc dialog = new FormDetailOutcomDoc(null);
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			listView1.Items.Add(dialog.DetailOutcomDoc.LVItem);
		}

		private void buttonUpdate_Click(object sender, System.EventArgs e)
		{
			// Обновление списка
			listView1.Items.Clear();
			DbDetailOutcomDoc.FillList(listView1);
		}

		private void listView1_DoubleClick(object sender, EventArgs e)
		{
			ListViewItem item = Db.GetItemPosition(listView1);
			if(item == null) return;
			DbDetailOutcomDoc element = (DbDetailOutcomDoc)item.Tag;
			if(element == null) return;

			// Просмотр требования накладной
			FormDetailOutcomDoc dialog = new FormDetailOutcomDoc(element);
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			dialog.DetailOutcomDoc.SetLVItem(item);
		}

		private void buttonPrint_Click(object sender, System.EventArgs e)
		{
			// Печать выбранного расходника
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			DbDetailOutcomDoc element = (DbDetailOutcomDoc)item.Tag;
			if(element == null) return;

			DbRequestionPrint print = new DbRequestionPrint(element);
			print.Print();
		}

		private void buttonSetImplement_Click(object sender, System.EventArgs e)
		{
			// Проводка документов по списку
			foreach(ListViewItem item in listView1.SelectedItems)
			{
				DbDetailOutcomDoc element = (DbDetailOutcomDoc)item.Tag;
				if(element != null)
				{
					if(element.MakeImplement(true)) element.SetLVItem(item);
				}
			}
			Db.ShowFaults();
		}

		private void buttonSetNotImplement_Click(object sender, System.EventArgs e)
		{
			// Отмена проводки документов по списку
			foreach(ListViewItem item in listView1.SelectedItems)
			{
				DbDetailOutcomDoc element = (DbDetailOutcomDoc)item.Tag;
				if(element != null)
				{
					if(element.MakeImplement(false)) element.SetLVItem(item);
				}
			}
			Db.ShowFaults();
		}

		private void buttonDelete_Click(object sender, System.EventArgs e)
		{
			// Удаление выбранного расходника
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			DbDetailOutcomDoc element = (DbDetailOutcomDoc)item.Tag;
			if(element == null) return;

			if (element.Delete()) item.Remove();	
		}
	}
}
