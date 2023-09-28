using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormSelectCardDetail.
	/// </summary>
	public class FormSelectCardDetail : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ListView listView2;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.ColumnHeader columnHeader8;
		private System.Windows.Forms.ColumnHeader columnHeader9;
		private System.Windows.Forms.ColumnHeader columnHeader10;
		private System.Windows.Forms.ColumnHeader columnHeader11;
		private System.Windows.Forms.Button buttonOK;
		DbCard card;
		ListView outerList;

		public FormSelectCardDetail(DbCard sourceCard, ListView outerListSrc)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			card = sourceCard;
			outerList = outerListSrc;
			// Заполнение верхнего списка деталями из заказ/наряда, которые еще не получены
			DbCardDetail.FillListNotIncom(listView1, card);

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
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
			this.listView2 = new System.Windows.Forms.ListView();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader10 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader11 = new System.Windows.Forms.ColumnHeader();
			this.buttonOK = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// listView1
			// 
			this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.columnHeader1,
																						this.columnHeader2,
																						this.columnHeader3,
																						this.columnHeader7});
			this.listView1.FullRowSelect = true;
			this.listView1.GridLines = true;
			this.listView1.Location = new System.Drawing.Point(8, 8);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(728, 136);
			this.listView1.TabIndex = 0;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Код";
			this.columnHeader1.Width = 90;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Наименование";
			this.columnHeader2.Width = 200;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Производитель";
			this.columnHeader3.Width = 200;
			// 
			// columnHeader7
			// 
			this.columnHeader7.Text = "Количество";
			// 
			// listView2
			// 
			this.listView2.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.listView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.columnHeader4,
																						this.columnHeader5,
																						this.columnHeader6,
																						this.columnHeader8,
																						this.columnHeader9,
																						this.columnHeader10,
																						this.columnHeader11});
			this.listView2.FullRowSelect = true;
			this.listView2.GridLines = true;
			this.listView2.Location = new System.Drawing.Point(8, 168);
			this.listView2.Name = "listView2";
			this.listView2.Size = new System.Drawing.Size(728, 136);
			this.listView2.TabIndex = 1;
			this.listView2.View = System.Windows.Forms.View.Details;
			this.listView2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listView2_KeyDown);
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "№";
			this.columnHeader4.Width = 30;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "Наименование";
			this.columnHeader5.Width = 200;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "Количество";
			// 
			// columnHeader8
			// 
			this.columnHeader8.Text = "Цена";
			this.columnHeader8.Width = 90;
			// 
			// columnHeader9
			// 
			this.columnHeader9.Text = "НДС";
			// 
			// columnHeader10
			// 
			this.columnHeader10.Text = "Цена с НДС";
			this.columnHeader10.Width = 90;
			// 
			// columnHeader11
			// 
			this.columnHeader11.Text = "Скмма";
			this.columnHeader11.Width = 90;
			// 
			// buttonOK
			// 
			this.buttonOK.Location = new System.Drawing.Point(344, 312);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.TabIndex = 2;
			this.buttonOK.Text = "ОК";
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// FormSelectCardDetail
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(752, 341);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.buttonOK,
																		  this.listView2,
																		  this.listView1});
			this.Name = "FormSelectCardDetail";
			this.Text = "Подбор деталей для заказ-наряда";
			this.ResumeLayout(false);

		}
		#endregion

		protected void listView1_DoubleClick(object sender, EventArgs e)
		{
			// Получаем выбранный элемент
			ListViewItem item = Db.GetItemPosition(listView1);
			if(item == null) return;
			DbCardDetail detail = (DbCardDetail)item.Tag;
			if(detail == null) return;
			if(detail.Quontity <= 0) return;

			// Запуск выбора сладской позиции для выбранной детали
			FormDetailStorageList dialog = new FormDetailStorageList(null, 2, null, null);
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			// Списываем выбранную деталь с верхнего листа
			DbDetailOutcom detailOutcom = dialog.SelectedDetailOutcom;
			if(detailOutcom.Quontity > detail.Quontity)
			{
				detailOutcom.Quontity = detail.Quontity;
			}
			detail.Quontity -= detailOutcom.Quontity;
			detail.SetLVItem(item);
			detailOutcom.SetConnectedPosition(detail);
			listView2.Items.Add(detailOutcom.LVItem);
		}

		protected void listView2_KeyDown(object sender, KeyEventArgs e)
		{
			ListViewItem item = Db.GetItemSelected(listView2);
			if(item == null) return;
			DbDetailOutcom detailOutcom = (DbDetailOutcom)item.Tag;
			if(detailOutcom == null) return;
			DbCardDetail detail = null;
			ListViewItem detailItem = null;

			switch(e.KeyCode)
			{
				case Keys.Delete:
					// Удаление из списка элемента
					// Найдем в верхнем списке элемент, соответсвующий выбранному
					foreach(ListViewItem upItem in listView1.Items)
					{
						DbCardDetail detailTmp = (DbCardDetail)upItem.Tag;
						if(detailTmp != null)
						{
						//	if(detailTmp.Number == detailOutcom.ConnectedNumber)
						//	{
						//		detail = detailTmp;
						//		detailItem = upItem;
						//	}
						}
					}
					// Исправляем найденый элемент
					if(detail != null)
					{
						detail.Quontity += detailOutcom.Quontity;
						detail.SetLVItem(detailItem);
						listView2.Items.Remove(item);
					}
					break;
				default:
					return;
			}
		}

		private void buttonOK_Click(object sender, System.EventArgs e)
		{
			// Переносим список, в лист!
			foreach(ListViewItem item in listView2.Items)
			{
				DbDetailOutcom detailOutcom = (DbDetailOutcom)item.Tag;
				if(detailOutcom != null)
				{
					outerList.Items.Add(detailOutcom.LVItem);
				}
			}
			this.DialogResult = DialogResult.OK;
			this.Close();
		}
	}
}
