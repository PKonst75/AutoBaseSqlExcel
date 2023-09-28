using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormViewSorageIncomDoc.
	/// </summary>
	public class FormViewSorageIncomDoc : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ListView listView_details;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FormViewSorageIncomDoc(DtStorageIncomDoc doc)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			// Заполняем данные
			this.Text = (string)doc.GetData("НОМЕР") + " " + doc.GetData("ДАТА").ToString();
			DbSqlStorageIncom.SelectInListDoc(listView_details, doc);
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
			this.listView_details = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.SuspendLayout();
			// 
			// listView_details
			// 
			this.listView_details.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.listView_details.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																							   this.columnHeader1,
																							   this.columnHeader2,
																							   this.columnHeader3,
																							   this.columnHeader4,
																							   this.columnHeader5});
			this.listView_details.FullRowSelect = true;
			this.listView_details.Location = new System.Drawing.Point(0, 32);
			this.listView_details.Name = "listView_details";
			this.listView_details.Size = new System.Drawing.Size(576, 280);
			this.listView_details.TabIndex = 0;
			this.listView_details.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "№";
			this.columnHeader1.Width = 33;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Артикул";
			this.columnHeader2.Width = 113;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Наименование";
			this.columnHeader3.Width = 250;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Количество";
			this.columnHeader4.Width = 77;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "Цена";
			this.columnHeader5.Width = 75;
			// 
			// FormViewSorageIncomDoc
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(576, 309);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.listView_details});
			this.Name = "FormViewSorageIncomDoc";
			this.Text = "Приход деталей";
			this.ResumeLayout(false);

		}
		#endregion
	}
}
