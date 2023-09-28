using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormManageStorageIncomDoc.
	/// </summary>
	public class FormManageStorageIncomDoc : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ListView listView_docs;
		private System.Windows.Forms.Button button_updatelist;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Button button_reset_quontity;
		private System.ComponentModel.IContainer components;

		public FormManageStorageIncomDoc()
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormManageStorageIncomDoc));
			this.listView_docs = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.button_updatelist = new System.Windows.Forms.Button();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.button_reset_quontity = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// listView_docs
			// 
			this.listView_docs.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.listView_docs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																							this.columnHeader1,
																							this.columnHeader2});
			this.listView_docs.FullRowSelect = true;
			this.listView_docs.Location = new System.Drawing.Point(0, 32);
			this.listView_docs.Name = "listView_docs";
			this.listView_docs.Size = new System.Drawing.Size(576, 240);
			this.listView_docs.TabIndex = 0;
			this.listView_docs.View = System.Windows.Forms.View.Details;
			this.listView_docs.DoubleClick += new System.EventHandler(this.listView_docs_DoubleClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Номер";
			this.columnHeader1.Width = 69;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Дата";
			this.columnHeader2.Width = 121;
			// 
			// button_updatelist
			// 
			this.button_updatelist.Image = ((System.Drawing.Bitmap)(resources.GetObject("button_updatelist.Image")));
			this.button_updatelist.Location = new System.Drawing.Point(0, 8);
			this.button_updatelist.Name = "button_updatelist";
			this.button_updatelist.Size = new System.Drawing.Size(24, 23);
			this.button_updatelist.TabIndex = 1;
			this.toolTip1.SetToolTip(this.button_updatelist, "Обновление списка");
			this.button_updatelist.Click += new System.EventHandler(this.button_updatelist_Click);
			// 
			// button_reset_quontity
			// 
			this.button_reset_quontity.Image = ((System.Drawing.Bitmap)(resources.GetObject("button_reset_quontity.Image")));
			this.button_reset_quontity.Location = new System.Drawing.Point(264, 8);
			this.button_reset_quontity.Name = "button_reset_quontity";
			this.button_reset_quontity.Size = new System.Drawing.Size(24, 23);
			this.button_reset_quontity.TabIndex = 2;
			this.toolTip1.SetToolTip(this.button_reset_quontity, "Пересчет складских остатков");
			this.button_reset_quontity.Click += new System.EventHandler(this.button_reset_quontity_Click);
			// 
			// FormManageStorageIncomDoc
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(576, 273);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.button_reset_quontity,
																		  this.button_updatelist,
																		  this.listView_docs});
			this.Name = "FormManageStorageIncomDoc";
			this.Text = "Приход запчастей на склад";
			this.ResumeLayout(false);

		}
		#endregion

		private void button_updatelist_Click(object sender, System.EventArgs e)
		{
			listView_docs.Items.Clear();
			DbSqlStorageIncomDoc.SelectInListAll(listView_docs);
		}

		private void listView_docs_DoubleClick(object sender, System.EventArgs e)
		{
			// Открываем соответсвующий документ прихода
			ListViewItem item = Db.GetItemSelected(listView_docs);
			if(item == null) return;
			long code_doc = (long)item.Tag;
			if(code_doc == 0) return;
			DtStorageIncomDoc doc = DbSqlStorageIncomDoc.Find(code_doc);
			if(doc == null) return;
			FormViewSorageIncomDoc dialog = new FormViewSorageIncomDoc(doc);
			dialog.Show();
		}

		private void button_reset_quontity_Click(object sender, System.EventArgs e)
		{
			// Пересчет складских остатков, ВСЕХ
			// Получаем список все элементов склада
			ArrayList storage = new ArrayList();
			DbSqlStorageDetail.SelectInArray1C(storage);
			foreach(object o in storage)
			{
				DtStorageDetail element = (DtStorageDetail)o;
				if(element != null)
				{
					DbSqlStorageDetail.CalculateStorage((long)element.GetData("КОД_СКЛАД_ДЕТАЛЬ"));
				}
			}
			MessageBox.Show("Пересчет остатков завершен");
		}
	}
}
