using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormSelectElement.
	/// </summary>
	public class FormSelectElement : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private Db selectedElement		= null;

		public FormSelectElement(ArrayList array)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			// Подгонка под размеры верхней формы
			listView1.Height	= this.ClientSize.Height;
			listView1.Width		= this.ClientSize.Width;
			listView1.Top		= 0;
			listView1.Left		= 0;
			// Заполнение списка
			// Специальный элемент
			ListViewItem itm	= new ListViewItem("Новый элемент");
			itm.Tag				= null;
			listView1.Items.Add(itm);
			foreach(object o in array)
			{
				Db element = (Db)o;
				itm					= new ListViewItem(element.DbTitleEx());
				itm.Tag				= element;
				listView1.Items.Add(itm);
			}
			listView1.Focus();
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
			this.listView1.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.columnHeader1});
			this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.listView1.HideSelection = false;
			this.listView1.Location = new System.Drawing.Point(8, 8);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(688, 232);
			this.listView1.TabIndex = 0;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listView1_KeyDown);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Элемент";
			this.columnHeader1.Width = 500;
			// 
			// FormSelectElement
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(8, 19);
			this.ClientSize = new System.Drawing.Size(704, 245);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.listView1});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "FormSelectElement";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Выбор";
			this.ResumeLayout(false);

		}
		#endregion

		private void listView1_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Escape)
			{
				// Завершение без выбора
				selectedElement	= null;
				this.Close();
				return;
			}
			if(e.KeyCode == Keys.Enter)
			{
				// Выбираем элемент
				if(listView1.SelectedItems == null) return;
				if(listView1.SelectedItems.Count == 0) return;
				selectedElement	= (Db)listView1.SelectedItems[0].Tag;
				this.Close();
				this.DialogResult	= DialogResult.OK;
				return;
			}
		}

		public Db SelectedElement
		{
			get
			{
				return selectedElement;
			}
		}

		protected override void OnCreateControl()
		{
			listView1.Focus();
			listView1.Items[0].Focused	= true;
		}
	}
}
