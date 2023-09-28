using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormDetailIncomList.
	/// </summary>
	public class FormDetailIncomList : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.Button buttonNew;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.Button buttonUpdate;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.Button buttonDocNull;
		private System.Windows.Forms.Button buttonImplement;
		private System.Windows.Forms.Button buttonImplementBack;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FormDetailIncomList()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormDetailIncomList));
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.buttonNew = new System.Windows.Forms.Button();
			this.buttonUpdate = new System.Windows.Forms.Button();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.buttonDocNull = new System.Windows.Forms.Button();
			this.buttonImplement = new System.Windows.Forms.Button();
			this.buttonImplementBack = new System.Windows.Forms.Button();
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
																						this.columnHeader5});
			this.listView1.FullRowSelect = true;
			this.listView1.GridLines = true;
			this.listView1.Location = new System.Drawing.Point(8, 24);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(712, 300);
			this.listView1.TabIndex = 0;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Номер";
			this.columnHeader1.Width = 70;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Дата";
			this.columnHeader2.Width = 100;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Сумма";
			this.columnHeader3.Width = 80;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Поставщик";
			this.columnHeader4.Width = 200;
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
			// columnHeader5
			// 
			this.columnHeader5.Text = "По документу";
			this.columnHeader5.Width = 200;
			// 
			// buttonDocNull
			// 
			this.buttonDocNull.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonDocNull.Image")));
			this.buttonDocNull.Location = new System.Drawing.Point(264, 0);
			this.buttonDocNull.Name = "buttonDocNull";
			this.buttonDocNull.Size = new System.Drawing.Size(24, 23);
			this.buttonDocNull.TabIndex = 3;
			this.buttonDocNull.Click += new System.EventHandler(this.buttonDocNull_Click);
			// 
			// buttonImplement
			// 
			this.buttonImplement.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonImplement.Image")));
			this.buttonImplement.Location = new System.Drawing.Point(288, 0);
			this.buttonImplement.Name = "buttonImplement";
			this.buttonImplement.Size = new System.Drawing.Size(24, 23);
			this.buttonImplement.TabIndex = 4;
			this.buttonImplement.Click += new System.EventHandler(this.buttonImplement_Click);
			// 
			// buttonImplementBack
			// 
			this.buttonImplementBack.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonImplementBack.Image")));
			this.buttonImplementBack.Location = new System.Drawing.Point(312, 0);
			this.buttonImplementBack.Name = "buttonImplementBack";
			this.buttonImplementBack.Size = new System.Drawing.Size(24, 23);
			this.buttonImplementBack.TabIndex = 5;
			this.buttonImplementBack.Click += new System.EventHandler(this.buttonImplementBack_Click);
			// 
			// FormDetailIncomList
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(728, 333);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.buttonImplementBack,
																		  this.buttonImplement,
																		  this.buttonDocNull,
																		  this.buttonUpdate,
																		  this.buttonNew,
																		  this.listView1});
			this.Name = "FormDetailIncomList";
			this.Text = "Приходные ордера";
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonNew_Click(object sender, System.EventArgs e)
		{
			// Заведение нового приходного ордера
			FormDetailIncom dialog = new FormDetailIncom(null);
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			listView1.Items.Add(dialog.DetailIncomDoc.LVItem);
		}

		private void buttonUpdate_Click(object sender, System.EventArgs e)
		{
			listView1.Items.Clear();
			DbDetailIncomDoc.FillList(listView1, null);
		}

		private void listView1_DoubleClick(object sender, EventArgs e)
		{
			ListViewItem item = Db.GetItemPosition(listView1);
			if(item == null) return;
			DbDetailIncomDoc detailIncomDoc = (DbDetailIncomDoc)item.Tag;
			if(detailIncomDoc == null) return;
			FormDetailIncom dialog = new FormDetailIncom(detailIncomDoc);
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			dialog.DetailIncomDoc.SetLVItem(item);
		}

		private void buttonDocNull_Click(object sender, System.EventArgs e)
		{
			// Анулировать выбранные документы
			// То есть после определенных проверок - удалить
			
			foreach(ListViewItem item in listView1.SelectedItems)
			{
				DbDetailIncomDoc element = (DbDetailIncomDoc)item.Tag;
				if(element != null)
				{
					if(element.Delete()) item.Remove();
				}
			}
			Db.ShowFaults();
		}

		private void buttonImplement_Click(object sender, System.EventArgs e)
		{
			// Проводка документов по списку
			foreach(ListViewItem item in listView1.SelectedItems)
			{
				DbDetailIncomDoc element = (DbDetailIncomDoc)item.Tag;
				if(element != null)
				{
					if(element.MakeImplement(true)) element.SetLVItem(item);
				}
			}
			Db.ShowFaults();
		}

		private void buttonImplementBack_Click(object sender, System.EventArgs e)
		{
			// Отмена проводки документов по списку
			foreach(ListViewItem item in listView1.SelectedItems)
			{
				DbDetailIncomDoc element = (DbDetailIncomDoc)item.Tag;
				if(element != null)
				{
					if(element.MakeImplement(false)) element.SetLVItem(item);
				}
			}
			Db.ShowFaults();
		}
	}
}
