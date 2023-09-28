using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Выбор из простого текст-бокса
	/// </summary>
	public class FormSelectionType2 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox textBoxSelect;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private string selectedText;
		private string initialText;

		public FormSelectionType2(string initialTextSrc)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			initialText			= initialTextSrc;
			textBoxSelect.Text	= initialText;
			textBoxSelect.SelectAll();
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
			this.textBoxSelect = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// textBoxSelect
			// 
			this.textBoxSelect.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBoxSelect.Name = "textBoxSelect";
			this.textBoxSelect.Size = new System.Drawing.Size(240, 23);
			this.textBoxSelect.TabIndex = 0;
			this.textBoxSelect.Text = "";
			// 
			// FormSelectionType2
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.ClientSize = new System.Drawing.Size(233, 21);
			this.ControlBox = false;
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.textBoxSelect});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.KeyPreview = true;
			this.Name = "FormSelectionType2";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "FormSelectionType2";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.form_KeyDown);
			this.ResumeLayout(false);

		}
		#endregion

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
				selectedText	= textBoxSelect.Text;
				selectedText	= selectedText.Trim();
				if(selectedText.Length == 0) return;
				e.Handled = true;
				this.DialogResult = DialogResult.OK;
				this.Close();
				return;
			}
		}

		public string SelectedTextUp
		{
			get
			{
				string tmpText;
				tmpText = selectedText.Trim();
				tmpText	= tmpText.ToUpper();
				return tmpText;
			}
		}
		public float SelectedFloat
		{
			get
			{
				selectedText = selectedText.Trim();
				selectedText = selectedText.Replace(".", ",");
				selectedText = selectedText.Replace("-", ",");
				float data = 0.0F;
				try
				{
					data = (float)Convert.ToDouble(selectedText);
				}
				catch(Exception E)
				{
					MessageBox.Show(E.Message);
				}
				return data;
			}
		}
	}
}
