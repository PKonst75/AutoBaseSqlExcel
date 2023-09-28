using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormDetailList.
	/// </summary>
	public class FormDetailList : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.Button buttonUpdate;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Button buttonNew;
		private System.Windows.Forms.Button buttonChange;
		private System.Windows.Forms.Button buttonChangeCode;
		private System.Windows.Forms.Button buttonDelete;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;

		DbDetail selectedDetail = null;

		public FormDetailList()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormDetailList));
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.buttonUpdate = new System.Windows.Forms.Button();
			this.buttonNew = new System.Windows.Forms.Button();
			this.buttonChange = new System.Windows.Forms.Button();
			this.buttonChangeCode = new System.Windows.Forms.Button();
			this.buttonDelete = new System.Windows.Forms.Button();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
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
			this.listView1.Location = new System.Drawing.Point(8, 32);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(720, 280);
			this.listView1.TabIndex = 0;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
			this.listView1.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView1_ColumnClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Код";
			this.columnHeader1.Width = 119;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Наименование";
			this.columnHeader2.Width = 273;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Примечание";
			this.columnHeader3.Width = 98;
			// 
			// buttonUpdate
			// 
			this.buttonUpdate.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonUpdate.Image")));
			this.buttonUpdate.Location = new System.Drawing.Point(32, 8);
			this.buttonUpdate.Name = "buttonUpdate";
			this.buttonUpdate.Size = new System.Drawing.Size(24, 23);
			this.buttonUpdate.TabIndex = 1;
			this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
			// 
			// buttonNew
			// 
			this.buttonNew.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonNew.Image")));
			this.buttonNew.Location = new System.Drawing.Point(8, 8);
			this.buttonNew.Name = "buttonNew";
			this.buttonNew.Size = new System.Drawing.Size(24, 23);
			this.buttonNew.TabIndex = 2;
			this.buttonNew.Click += new System.EventHandler(this.buttonNew_Click);
			// 
			// buttonChange
			// 
			this.buttonChange.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonChange.Image")));
			this.buttonChange.Location = new System.Drawing.Point(56, 8);
			this.buttonChange.Name = "buttonChange";
			this.buttonChange.Size = new System.Drawing.Size(24, 23);
			this.buttonChange.TabIndex = 3;
			this.buttonChange.Click += new System.EventHandler(this.buttonChange_Click);
			// 
			// buttonChangeCode
			// 
			this.buttonChangeCode.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonChangeCode.Image")));
			this.buttonChangeCode.Location = new System.Drawing.Point(384, 8);
			this.buttonChangeCode.Name = "buttonChangeCode";
			this.buttonChangeCode.Size = new System.Drawing.Size(24, 23);
			this.buttonChangeCode.TabIndex = 4;
			this.buttonChangeCode.Click += new System.EventHandler(this.buttonChangeCode_Click);
			// 
			// buttonDelete
			// 
			this.buttonDelete.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonDelete.Image")));
			this.buttonDelete.Location = new System.Drawing.Point(416, 8);
			this.buttonDelete.Name = "buttonDelete";
			this.buttonDelete.Size = new System.Drawing.Size(24, 23);
			this.buttonDelete.TabIndex = 5;
			this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Применимость (код)";
			this.columnHeader4.Width = 90;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "Применимость";
			this.columnHeader5.Width = 120;
			// 
			// FormDetailList
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(736, 317);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.buttonDelete,
																		  this.buttonChangeCode,
																		  this.buttonChange,
																		  this.buttonNew,
																		  this.buttonUpdate,
																		  this.listView1});
			this.Name = "FormDetailList";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Справочник деталей";
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonUpdate_Click(object sender, System.EventArgs e)
		{
			// Обновляем список в листе
			listView1.Items.Clear();
			DbDetail.FillList(listView1, "", DbDetail.SelectType.ByNot);
		}

		private void listView1_DoubleClick(object sender, EventArgs e)
		{
			ListViewItem item = Db.GetItemPosition(listView1);
			if(item == null) return;
			DbDetail detail = (DbDetail)item.Tag;
			if(detail == null) return;
			selectedDetail = detail;
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		public DbDetail SelectedDetail
		{
			get
			{
				return selectedDetail;
			}
		}

		private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
		{
			string mask = "";
			FormSelectString dialog = null;
			switch(e.Column)
			{
				case 0:
					// Щелчек на колонке с кодом детали
					dialog = new FormSelectString("Код детали", "Код детали для поиска");
					if(dialog.ShowDialog(this) != DialogResult.OK) return;
					mask = dialog.SelectedTextMask;
					listView1.Items.Clear();
					DbDetail.FillList(listView1, mask, DbDetail.SelectType.ByCode);
					return;
				case 1:
					dialog = new FormSelectString("Наименование детали", "Наименование детали для поиска");
					if(dialog.ShowDialog(this) != DialogResult.OK) return;
					mask = dialog.SelectedTextMask;
					listView1.Items.Clear();
					DbDetail.FillList(listView1, mask, DbDetail.SelectType.ByName);
					return;
			}
		}

		private void buttonNew_Click(object sender, System.EventArgs e)
		{
			// Добавление новой детали
			FormDetail dialog = new FormDetail(null);
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			listView1.Items.Add(dialog.Detail.LVItem);
		}

		private void buttonChange_Click(object sender, System.EventArgs e)
		{
			// Свойства вбранной детали
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			DbDetail detail = (DbDetail)item.Tag;
			if(detail == null) return;
			// Изменение детали
			FormDetail dialog = new FormDetail(detail);
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			dialog.Detail.SetLVItem(item);
		}

		private void buttonChangeCode_Click(object sender, System.EventArgs e)
		{
			// Смена кода детали!
			// Админовская привилегия
			// Свойства вбранной детали
			string newCode;
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			DbDetail detail = (DbDetail)item.Tag;
			if(detail == null) return;

			FormSelectString dialog = new FormSelectString("Новый код детали", "Введите новый код детали");
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK)
			{
				if(DialogResult.Yes != MessageBox.Show("Ввести пустой код?", "Вопрос", MessageBoxButtons.YesNo)) return;				
				newCode = "";
			}
			else
				newCode = dialog.SelectedText;
			if(detail.WriteCode(newCode) != true) return;
			detail.SetLVItem(item);
		}

		private void buttonDelete_Click(object sender, System.EventArgs e)
		{
			// Удаление позиции
			// Смена кода детали!
			// Админовская привилегия
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			DbDetail detail = (DbDetail)item.Tag;
			if(detail == null) return;

			if(!detail.DetailDelete()) return;
			listView1.Items.Remove(item);
		}
	}
}
