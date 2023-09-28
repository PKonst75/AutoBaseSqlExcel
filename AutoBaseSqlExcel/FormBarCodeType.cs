using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormBarCodeType.
	/// </summary>
	public class FormBarCodeType : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FormBarCodeType()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			// Заполянем список предустановленными типами Штрих-Кодов
			ListViewItem item;
			item = listView1.Items.Add("ВАЗ");
			item.Tag = Db.BarCodes.Vaz;

			listView1.Items[0].Selected = true;
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
			this.SuspendLayout();
			// 
			// listView1
			// 
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.columnHeader1});
			this.listView1.FullRowSelect = true;
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(312, 112);
			this.listView1.TabIndex = 0;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listView1_KeyDown);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Штрих-Код";
			this.columnHeader1.Width = 293;
			// 
			// FormBarCodeType
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(312, 109);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.listView1});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "FormBarCodeType";
			this.Text = "Выберете тип Штрих-Кода";
			this.ResumeLayout(false);

		}
		#endregion

		protected void listView1_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Enter)
			{
				// Выбор
				ListViewItem item = Db.GetItemSelected(listView1);
				if(item == null) return;
				Db.barcCodeType = (Db.BarCodes)item.Tag;
				this.Close();
				return;
			}
			if(e.KeyCode == Keys.Escape)
			{
				this.Close();
				return;
			}
		}
	}
}
