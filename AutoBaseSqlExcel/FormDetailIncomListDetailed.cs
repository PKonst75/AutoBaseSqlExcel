using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormDetailIncomListDetailed.
	/// </summary>
	public class FormDetailIncomListDetailed : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.ColumnHeader columnHeader8;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		ListView outerList = null;
		DbDetailOutcom selectedDetailOutcom;

		public FormDetailIncomListDetailed(DbDetailStorage source, ListView list)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			this.Text = source.DetailName;
			DbDetailIncomDetailed.FillListIncom(listView1, source);
			outerList = list;
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
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
			this.SuspendLayout();
			// 
			// listView1
			// 
			this.listView1.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.columnHeader1,
																						this.columnHeader4,
																						this.columnHeader5,
																						this.columnHeader6,
																						this.columnHeader7,
																						this.columnHeader8});
			this.listView1.FullRowSelect = true;
			this.listView1.GridLines = true;
			this.listView1.Location = new System.Drawing.Point(8, 8);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(608, 256);
			this.listView1.TabIndex = 0;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.DoubleClick += new EventHandler(this.listView1_DoubleClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Дата";
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Цена";
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "НДС";
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "Цена с НДС";
			// 
			// columnHeader7
			// 
			this.columnHeader7.Text = "Количество";
			// 
			// columnHeader8
			// 
			this.columnHeader8.Text = "Основание";
			this.columnHeader8.Width = 200;
			// 
			// FormDetailIncomListDetailed
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(624, 273);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.listView1});
			this.Name = "FormDetailIncomListDetailed";
			this.Text = "Приход деталей на склад (подробно)";
			this.ResumeLayout(false);

		}
		#endregion

		private void listView1_DoubleClick(object sender, EventArgs e)
		{
			ListViewItem item = Db.GetItemPosition(listView1);
			if(item == null) return;
			DbDetailIncomDetailed detailIncomDetailed = (DbDetailIncomDetailed)item.Tag;
			if(detailIncomDetailed == null) return;

			// Запрос количества (по умолчанию единица)
			FormSelectString dialog = new FormSelectString("Количество", "1.00");
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			float quontity = dialog.SelectedFloat;
			if(quontity <= 0) return;
			detailIncomDetailed.Quontity = quontity;
			
			if(outerList == null)
			{
				selectedDetailOutcom = new DbDetailOutcom(detailIncomDetailed);
				this.DialogResult = DialogResult.OK;
				this.Close();
				return;
			}
			DbDetailOutcom detailOutcom = new DbDetailOutcom(detailIncomDetailed);
			outerList.Items.Add(detailOutcom.LVItem);
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		public DbDetailOutcom SelectedDeteilOutcom
		{
			get
			{
				return selectedDetailOutcom;
			}
		}
	}
}
