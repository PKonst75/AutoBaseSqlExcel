using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormSelectionType1.
	/// </summary>
	public class FormSelectionType1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.TextBox textBoxSelect;
		private System.Windows.Forms.Button buttonSelect;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		Db selectedElement;
		private string selectedText	= "";

		public FormSelectionType1(ArrayList src, string initialText, bool emptyElement)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			// Подгонка размер компонент под размер окна

			// Вод пустого элемента
			if(emptyElement)
			{
				ListViewItem item = listView1.Items.Add("");
			}

			if(src != null)
			{
				ArrayList array = new ArrayList(src);
				foreach(object o in array)
				{
					Db element = (Db)o;
					ListViewItem item = listView1.Items.Add(element.DbTitle());
					item.Tag = o;
				}
			}
			textBoxSelect.Text	= initialText;

			string text				= textBoxSelect.Text;
			ListViewItem firstMatch	= null;
			listView1.HideSelection	= false;
			listView1.SelectedItems.Clear();
			// Пробуем найти ближайший  элемент, совпадающий по слогам
			foreach(ListViewItem item in listView1.Items)
			{
				if((item.Text.ToUpper().StartsWith(text.ToUpper()) == true)&&(firstMatch == null))
				{
					firstMatch = item;
				}
			}
			if(firstMatch != null)
			{
				listView1.EnsureVisible(firstMatch.Index);
				firstMatch.Selected		= true;
				firstMatch.Focused		= true;
				ListViewItem topItem	= listView1.TopItem;
				int diff				= topItem.Index - firstMatch.Index;
				if(diff != 0)
				{
					int elements		= listView1.Height / (firstMatch.Bounds.Bottom - firstMatch.Bounds.Top) - 1;
					int visibleItem		= elements - 1 + firstMatch.Index;
					if (visibleItem > listView1.Items.Count - 1)
						listView1.EnsureVisible(listView1.Items.Count - 1);
					else
						listView1.EnsureVisible(visibleItem);
				}
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
			this.textBoxSelect = new System.Windows.Forms.TextBox();
			this.buttonSelect = new System.Windows.Forms.Button();
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
			this.listView1.FullRowSelect = true;
			this.listView1.Location = new System.Drawing.Point(0, 24);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(292, 249);
			this.listView1.TabIndex = 3;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.DoubleClick += new EventHandler(this.listView1_DoubleClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Наименование";
			this.columnHeader1.Width = 260;
			// 
			// textBoxSelect
			// 
			this.textBoxSelect.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.textBoxSelect.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBoxSelect.Name = "textBoxSelect";
			this.textBoxSelect.Size = new System.Drawing.Size(268, 23);
			this.textBoxSelect.TabIndex = 1;
			this.textBoxSelect.Text = "";
			this.textBoxSelect.TextChanged += new System.EventHandler(this.textBoxSelect_TextChanged);
			// 
			// buttonSelect
			// 
			this.buttonSelect.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.buttonSelect.Location = new System.Drawing.Point(269, 1);
			this.buttonSelect.Name = "buttonSelect";
			this.buttonSelect.Size = new System.Drawing.Size(24, 23);
			this.buttonSelect.TabIndex = 2;
			this.buttonSelect.TabStop = false;
			this.buttonSelect.Text = "...";
			this.buttonSelect.Click += new System.EventHandler(this.buttonSelect_Click);
			// 
			// FormSelectionType1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.ClientSize = new System.Drawing.Size(292, 273);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.buttonSelect,
																		  this.textBoxSelect,
																		  this.listView1});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.KeyPreview = true;
			this.Name = "FormSelectionType1";
			this.ShowInTaskbar = false;
			this.Text = "FormSelectionType1";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.form_KeyDown);
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonSelect_Click(object sender, System.EventArgs e)
		{
		
		}

		private void textBoxSelect_TextChanged(object sender, EventArgs e)
		{
			string text				= textBoxSelect.Text;
			ListViewItem firstMatch	= null;
			listView1.HideSelection	= false;
			listView1.SelectedItems.Clear();
			// Пробуем найти ближайший  элемент, совпадающий по слогам
			foreach(ListViewItem item in listView1.Items)
			{
				if((item.Text.ToUpper().StartsWith(text.ToUpper()) == true)&&(firstMatch == null))
				{
					firstMatch = item;
				}
			}
			if(firstMatch != null)
			{
				listView1.EnsureVisible(firstMatch.Index);
				firstMatch.Selected		= true;
				firstMatch.Focused		= true;
				ListViewItem topItem	= listView1.TopItem;
				int diff				= topItem.Index - firstMatch.Index;
				if(diff != 0)
				{
					int elements		= listView1.Height / (firstMatch.Bounds.Bottom - firstMatch.Bounds.Top) - 1;
					int visibleItem		= elements - 1 + firstMatch.Index;
					if (visibleItem > listView1.Items.Count - 1)
						listView1.EnsureVisible(listView1.Items.Count - 1);
					else
						listView1.EnsureVisible(visibleItem);
				}
			}
		}

		private void form_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Escape)
			{
				e.Handled = true;
				this.Close();
				return;
			}
			if(e.KeyCode == Keys.Enter)
			{
				if((listView1.SelectedItems != null)&&(listView1.SelectedItems.Count > 0))
					selectedElement		= (Db)listView1.SelectedItems[0].Tag;
				else
				{
					// Отработка набранного текста
					selectedText		= textBoxSelect.Text;
					selectedElement		= null;
				}
				e.Handled = true;
				this.DialogResult = DialogResult.OK;
				this.Close();
				return;
			}
			if((e.KeyCode == Keys.Up)||(e.KeyCode == Keys.Down))
			{
				if(textBoxSelect.Focused == true)
				{
					listView1.Focus();
					// Если не один из элементов не имеет фокуса - ставим фокус на первый элемен
					if(listView1.FocusedItem == null)
					{
						if(listView1.Items[0] != null)
						{
							listView1.Items[0].Focused = true;
						}
					}
					e.Handled = true;
					return;
				}
			}
		}

		private void listView1_DoubleClick(object sender, EventArgs e)
		{
			// Выбор элемента по щелчку
			if((listView1.SelectedItems != null)&&(listView1.SelectedItems.Count > 0))
				selectedElement		= (Db)listView1.SelectedItems[0].Tag;
			else
			{
				return;
			}
			this.DialogResult = DialogResult.OK;
			this.Close();
			return;
		}

		public Db SelectedElement
		{
			get
			{
				return selectedElement;
			}
		}

		public string SelectedText
		{
			get
			{
				return selectedText;
			}
		}
	}
}
