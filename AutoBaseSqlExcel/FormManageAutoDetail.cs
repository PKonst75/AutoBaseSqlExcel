using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormManageAutoDetail.
	/// </summary>
	public class FormManageAutoDetail : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ListView listViewStorage;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ColumnHeader columnHeader8;
		private System.Windows.Forms.ColumnHeader columnHeader9;
		private System.Windows.Forms.ListView listViewDetail;
		private System.Windows.Forms.Button buttonUpdate;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FormManageAutoDetail()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormManageAutoDetail));
			this.listViewDetail = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.listViewStorage = new System.Windows.Forms.ListView();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
			this.buttonUpdate = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// listViewDetail
			// 
			this.listViewDetail.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.listViewDetail.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																							 this.columnHeader1,
																							 this.columnHeader2,
																							 this.columnHeader9,
																							 this.columnHeader3,
																							 this.columnHeader4});
			this.listViewDetail.FullRowSelect = true;
			this.listViewDetail.Location = new System.Drawing.Point(8, 40);
			this.listViewDetail.Name = "listViewDetail";
			this.listViewDetail.Size = new System.Drawing.Size(792, 160);
			this.listViewDetail.TabIndex = 0;
			this.listViewDetail.View = System.Windows.Forms.View.Details;
			this.listViewDetail.DoubleClick += new System.EventHandler(this.listViewDetail_DoubleClick);
			this.listViewDetail.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listViewDetail_ColumnClick);
			this.listViewDetail.SelectedIndexChanged += new System.EventHandler(this.listViewDetail_SelectedIndexChanged);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Код";
			this.columnHeader1.Width = 98;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Наименование";
			this.columnHeader2.Width = 220;
			// 
			// columnHeader9
			// 
			this.columnHeader9.Text = "Примечание";
			this.columnHeader9.Width = 160;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Применимость (Код)";
			this.columnHeader3.Width = 122;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Применимость";
			this.columnHeader4.Width = 165;
			// 
			// listViewStorage
			// 
			this.listViewStorage.Anchor = ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.listViewStorage.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																							  this.columnHeader5,
																							  this.columnHeader6,
																							  this.columnHeader8});
			this.listViewStorage.FullRowSelect = true;
			this.listViewStorage.Location = new System.Drawing.Point(8, 224);
			this.listViewStorage.Name = "listViewStorage";
			this.listViewStorage.Size = new System.Drawing.Size(792, 136);
			this.listViewStorage.TabIndex = 1;
			this.listViewStorage.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "Код";
			this.columnHeader5.Width = 106;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "Наименование";
			this.columnHeader6.Width = 320;
			// 
			// columnHeader8
			// 
			this.columnHeader8.Text = "Описание";
			this.columnHeader8.Width = 190;
			// 
			// buttonUpdate
			// 
			this.buttonUpdate.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonUpdate.Image")));
			this.buttonUpdate.Location = new System.Drawing.Point(8, 16);
			this.buttonUpdate.Name = "buttonUpdate";
			this.buttonUpdate.Size = new System.Drawing.Size(24, 23);
			this.buttonUpdate.TabIndex = 2;
			this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
			// 
			// FormManageAutoDetail
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(816, 373);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.buttonUpdate,
																		  this.listViewStorage,
																		  this.listViewDetail});
			this.Name = "FormManageAutoDetail";
			this.Text = "Управление деталями склада";
			this.ResumeLayout(false);

		}
		#endregion

		private void listViewDetail_ColumnClick(object sender, ColumnClickEventArgs e)
		{
			// Запрос строки для поиска
			FormSelectString dialog = new FormSelectString("Запрос", "Введите строку для поиска");
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			string pattern = dialog.SelectedTextMask;
			listViewDetail.Items.Clear();
			switch(e.Column)
			{
				case 1:
					DbDetail.FillList(listViewDetail, pattern, DbDetail.SelectType.ByName);
					break;
				default:
					break;
			}
		}

		private void listViewDetail_SelectedIndexChanged(object sender, EventArgs e)
		{
			// Показываем складские позиции на данную деталь
			listViewStorage.Items.Clear();
			ListViewItem item = Db.GetItemSelected(listViewDetail);
			if(item == null) return;
			DbDetail detail = (DbDetail)item.Tag;
			if(detail == null) return;

			DbDetailStorage.FillList(listViewStorage, detail);
		}

		private void listViewDetail_DoubleClick(object sender, EventArgs e)
		{
			// Показываем складские позиции на данную деталь
			listViewStorage.Items.Clear();
			ListViewItem item = Db.GetItemSelected(listViewDetail);
			if(item == null) return;
			DbDetail detail = (DbDetail)item.Tag;
			if(detail == null) return;

			// Вызов диалога изменения детали
			FormDetail dialog = new FormDetail(detail);
			dialog.ShowDialog(this);
			DbDetailStorage.FillList(listViewStorage, detail);
			if(dialog.DialogResult != DialogResult.OK) return;
			dialog.Detail.SetLVItem(item);
		}

		private void buttonUpdate_Click(object sender, System.EventArgs e)
		{
			// Обновить список деталей
			listViewDetail.Items.Clear();
			DbDetail.FillList(listViewDetail, "", DbDetail.SelectType.ByNot);
		}
	}
}
