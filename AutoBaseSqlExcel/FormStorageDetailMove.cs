using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormStorageDetailMove.
	/// </summary>
	public class FormStorageDetailMove : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.ColumnHeader columnHeader5;

		DtStorageDetail detail;

		public FormStorageDetailMove(DtStorageDetail the_detail, long type)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			if(type == 1)
			{
				// Формирование запроса по движению определенной детали
				detail	= the_detail;
				// Получаем все приходы
				DbSqlStorageIncom.SelectInListDetail(listView1, (long)detail.GetData("КОД_СКЛАД_ДЕТАЛЬ"));
			}
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
			this.listView1.Location = new System.Drawing.Point(0, 32);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(736, 272);
			this.listView1.TabIndex = 0;
			this.listView1.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Артикул";
			this.columnHeader1.Width = 104;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Наименование";
			this.columnHeader2.Width = 252;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Дата";
			this.columnHeader3.Width = 82;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Количество";
			this.columnHeader4.Width = 75;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "Основание";
			this.columnHeader5.Width = 185;
			// 
			// FormStorageDetailMove
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(736, 301);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.listView1});
			this.Name = "FormStorageDetailMove";
			this.Text = "Движение запчастей";
			this.ResumeLayout(false);

		}
		#endregion
	}
}
