using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormTakeDocumentList.
	/// </summary>
	public class FormTakeDocumentList : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.Button buttonNew;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.Button buttonUpdate;
		private System.Windows.Forms.Button buttonProperties;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FormTakeDocumentList()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormTakeDocumentList));
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.buttonNew = new System.Windows.Forms.Button();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.buttonUpdate = new System.Windows.Forms.Button();
			this.buttonProperties = new System.Windows.Forms.Button();
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
			this.listView1.Location = new System.Drawing.Point(8, 32);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(736, 280);
			this.listView1.TabIndex = 0;
			this.listView1.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Дата";
			this.columnHeader1.Width = 90;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Номер";
			this.columnHeader2.Width = 100;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Документ";
			this.columnHeader3.Width = 120;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Поставщик";
			this.columnHeader4.Width = 200;
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
			// columnHeader5
			// 
			this.columnHeader5.Text = "Примечание";
			this.columnHeader5.Width = 200;
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
			// buttonProperties
			// 
			this.buttonProperties.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonProperties.Image")));
			this.buttonProperties.Location = new System.Drawing.Point(56, 8);
			this.buttonProperties.Name = "buttonProperties";
			this.buttonProperties.Size = new System.Drawing.Size(24, 23);
			this.buttonProperties.TabIndex = 3;
			this.buttonProperties.Click += new System.EventHandler(this.buttonProperties_Click);
			// 
			// FormTakeDocumentList
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.ClientSize = new System.Drawing.Size(752, 317);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.buttonProperties,
																		  this.buttonUpdate,
																		  this.buttonNew,
																		  this.listView1});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.Name = "FormTakeDocumentList";
			this.Text = "Документы принятия автомобилей";
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonNew_Click(object sender, System.EventArgs e)
		{
			// Вызов диалога добавления нового документа
			FormTakeDocument dialog = new FormTakeDocument(null);
			dialog.ShowDialog();

			if(dialog.DialogResult != DialogResult.OK)
			{
				if (dialog.Document.Code > 0)
					listView1.Items.Add(dialog.Document.LVItem);
				return;
			}
			listView1.Items.Add(dialog.Document.LVItem);
		}

		private void buttonUpdate_Click(object sender, System.EventArgs e)
		{
			// Обновление списка документов принятия автомобилей
			listView1.Items.Clear();
			DbTakeDocument.FillList(listView1);
		}

		private void buttonProperties_Click(object sender, System.EventArgs e)
		{
			// Свойства выбранного документа
			ListViewItem item = Db.GetItemSelected(listView1);
			if(item == null) return;
			DbTakeDocument element = (DbTakeDocument)item.Tag;
			if(element == null) return;

			FormTakeDocument dialog = new FormTakeDocument(element);
			dialog.ShowDialog();
			if(dialog.DialogResult != DialogResult.OK) return;
			dialog.Document.SetLVItem(item);
		}
	}
}
