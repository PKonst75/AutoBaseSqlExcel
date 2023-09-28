using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Позволяет осуществлять текстовый поиск в определенной колонке заданного листа
	/// </summary>
	public class FormSelectionType3 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox textBox1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		ListView	listView;
		int			column;

		public FormSelectionType3(ListView srcListView, int srcColumn)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			listView	= srcListView;
			column		= srcColumn;
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
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// textBox1
			// 
			this.textBox1.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(392, 23);
			this.textBox1.TabIndex = 0;
			this.textBox1.Text = "";
			this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
			// 
			// FormSelectionType3
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.ClientSize = new System.Drawing.Size(392, 24);
			this.ControlBox = false;
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.textBox1});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.KeyPreview = true;
			this.Name = "FormSelectionType3";
			this.Text = "FormSelectionType3";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.form_KeyDown);
			this.ResumeLayout(false);

		}
		#endregion

		protected void textBox1_TextChanged(object sender, EventArgs e)
		{
			// При изменении искомого текста, производим поиск и прокрутку

			string text				= textBox1.Text;
			ListViewItem firstMatch	= null;
			listView.HideSelection	= false;
			listView.SelectedItems.Clear();
			// Пробуем найти ближайший  элемент, совпадающий по слогам
			foreach(ListViewItem item in listView.Items)
			{
				if((item.SubItems[column].Text.ToUpper().StartsWith(text.ToUpper()) == true)&&(firstMatch == null))
				{
					firstMatch = item;
				}
			}
			if(firstMatch != null)
			{
				listView.EnsureVisible(firstMatch.Index);
				firstMatch.Selected		= true;
				firstMatch.Focused		= true;
				ListViewItem topItem	= listView.TopItem;
				int diff				= topItem.Index - firstMatch.Index;
				if(diff != 0)
				{
					int elements		= listView.Height / (firstMatch.Bounds.Bottom - firstMatch.Bounds.Top) - 1;
					int visibleItem		= elements - 1 + firstMatch.Index;
					if (visibleItem > listView.Items.Count - 1)
						listView.EnsureVisible(listView.Items.Count - 1);
					else
						listView.EnsureVisible(visibleItem);
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
			//	if((listView.SelectedItems != null)&&(listView.SelectedItems.Count > 0))
			//		selectedElement		= (Db)listView.SelectedItems[0].Tag;
			//	else
			//		selectedElement		= null;
				e.Handled = true;
				this.DialogResult = DialogResult.OK;
				this.Close();
				return;
			}
			if((e.KeyCode == Keys.Up)||(e.KeyCode == Keys.Down))
			{
			//	if(textBoxSelect.Focused == true)
			//	{
					listView.Focus();
					// Если не один из элементов не имеет фокуса - ставим фокус на первый элемен
					if(listView.FocusedItem == null)
					{
						if(listView.Items[0] != null)
						{
							listView.Items[0].Focused = true;
						}
					}
					e.Handled = true;
					return;
			//	}
			}
		}
	}
}
