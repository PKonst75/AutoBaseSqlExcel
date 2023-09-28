using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormSelectionList.
	/// </summary>
	public class FormSelectionList : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView listView1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private long	selected_code			= 0;
		private string	selected_code_string	= "";
		private object	selected_code_object	= "";
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private string	selected_text;

		public FormSelectionList(ListView list)
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
			itm.Tag				= 0;
			listView1.Items.Add(itm);

			if(list != null && list.Items != null && list.Items.Count != 0)
			{
				foreach(ListViewItem item in list.Items)
				{
					itm					= new ListViewItem(item.Text);
					itm.Tag				= item.Tag;
					listView1.Items.Add(itm);
				}
			}
			listView1.Focus();
			listView1.Items[0].Selected = true;
		}

		public FormSelectionList(ListView list, bool is_new)
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
			ListViewItem itm;
			if(is_new)
			{
				itm	= new ListViewItem("Новый элемент");
				itm.Tag				= 0;
				listView1.Items.Add(itm);
			}

			if(list != null && list.Items != null && list.Items.Count != 0)
			{
				foreach(ListViewItem item in list.Items)
				{
					itm					= new ListViewItem(item.Text);
					itm.Tag				= item.Tag;
					listView1.Items.Add(itm);
				}
			}			
			listView1.Focus();
			if(listView1.Items != null && listView1.Items.Count != 0)
				listView1.Items[0].Selected = true;
		}

		public FormSelectionList(ListView list, string new_string)
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
			ListViewItem itm;
			itm	= new ListViewItem(new_string);
			itm.Tag				= 0;
			listView1.Items.Add(itm);

			if(list != null && list.Items != null && list.Items.Count != 0)
			{
				foreach(ListViewItem item in list.Items)
				{
					itm					= new ListViewItem(item.Text);
					itm.Tag				= item.Tag;
					listView1.Items.Add(itm);
				}
			}			
			listView1.Focus();
			if(listView1.Items != null && listView1.Items.Count != 0)
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
			this.listView1.Location = new System.Drawing.Point(8, 8);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(520, 256);
			this.listView1.TabIndex = 0;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listView1_KeyDown);
			this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Элемент";
			this.columnHeader1.Width = 500;
			// 
			// FormSelectionList
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(8, 19);
			this.ClientSize = new System.Drawing.Size(536, 273);
			this.ControlBox = false;
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.listView1});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "FormSelectionList";
			this.Text = "FormSelectionList";
			this.ResumeLayout(false);

		}
		#endregion

		private void listView1_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Escape)
			{
				// Завершение без выбора
				selected_code	= 0;
				selected_text	= "";
				this.Close();
				return;
			}
			if(e.KeyCode == Keys.Enter)
			{
				// Выбираем элемент
				if(listView1.SelectedItems == null) return;
				if(listView1.SelectedItems.Count == 0) return;
				if(listView1.SelectedItems[0].Tag == null) return;

				switch(listView1.SelectedItems[0].Tag.GetType().ToString())
				{
					case "System.String":
						selected_code_string = (string)listView1.SelectedItems[0].Tag;
						break;
					case "System.Int64":
						selected_code = (long)listView1.SelectedItems[0].Tag;
						break;
					default:
						selected_code_object = (object)listView1.SelectedItems[0].Tag;
						break;
				}
				selected_text	= listView1.SelectedItems[0].Text;
				this.Close();
				this.DialogResult	= DialogResult.OK;
				return;
			}
		}

		public string SelectedText
		{
			get
			{
				return selected_text;
			}
		}

		public long SelectedCode
		{
			get
			{
				return selected_code;
			}
		}
		public string SelectedCodeString
		{
			get
			{
				return selected_code_string;
			}
		}
		public object SelectedCodeObject
		{
			get
			{
				return selected_code_object;
			}
		}

		protected override void OnCreateControl()
		{
			listView1.Focus();
			if(listView1.Items != null && listView1.Items.Count != 0)
				listView1.Items[0].Focused	= true;
		}

		private void listView1_DoubleClick(object sender, System.EventArgs e)
		{
			// Выбираем элемент
			if(listView1.SelectedItems == null) return;
			if(listView1.SelectedItems.Count == 0) return;
			if(listView1.SelectedItems[0].Tag == null) return;

			switch(listView1.SelectedItems[0].Tag.GetType().ToString())
			{
				case "System.String":
					selected_code_string = (string)listView1.SelectedItems[0].Tag;
					break;
				case "System.Int64":
					selected_code = (long)listView1.SelectedItems[0].Tag;
					break;
				default:
					selected_code_object = (object)listView1.SelectedItems[0].Tag;
					break;
			}

			selected_text	= listView1.SelectedItems[0].Text;
			this.Close();
			this.DialogResult	= DialogResult.OK;
			return;
		}
	}
}
