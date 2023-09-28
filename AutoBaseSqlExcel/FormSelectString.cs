using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormSelectString.
	/// </summary>
	public class FormSelectString : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox textBoxSelect;
		private System.Windows.Forms.Button buttonOk;
		private string selectedText= "";
		private System.Windows.Forms.Button buttonCancel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FormSelectString()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		public FormSelectString(string caption, string text)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			this.Text = caption;
			this.textBoxSelect.Text = text;
		}
		public FormSelectString(string caption, string text, bool password)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			this.Text = caption;
			this.textBoxSelect.Text = text;
			if(password == true)
			{
				this.textBoxSelect.PasswordChar = '*';
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
			this.textBoxSelect = new System.Windows.Forms.TextBox();
			this.buttonOk = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// textBoxSelect
			// 
			this.textBoxSelect.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.textBoxSelect.Location = new System.Drawing.Point(8, 8);
			this.textBoxSelect.Name = "textBoxSelect";
			this.textBoxSelect.Size = new System.Drawing.Size(584, 23);
			this.textBoxSelect.TabIndex = 0;
			this.textBoxSelect.Text = "Текст подсказки";
			// 
			// buttonOk
			// 
			this.buttonOk.Location = new System.Drawing.Point(216, 40);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.TabIndex = 1;
			this.buttonOk.Text = "Выбрать";
			this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(296, 40);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.TabIndex = 2;
			this.buttonCancel.Text = "Отмена";
			// 
			// FormSelectString
			// 
			this.AcceptButton = this.buttonOk;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(600, 71);
			this.ControlBox = false;
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.buttonCancel,
																		  this.buttonOk,
																		  this.textBoxSelect});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "FormSelectString";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Текст подсказки";
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonOk_Click(object sender, System.EventArgs e)
		{
			selectedText = textBoxSelect.Text;
			selectedText.Trim();
			if(selectedText.Length == 0) return;
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		public string SelectedText
		{
			get
			{
				return (string) selectedText;
			}
		}

		public string SelectedTextMask
		{
			get
			{
				return (string) "%" + selectedText + "%";
			}
		}

		public float SelectedFloat
		{
			get
			{
				selectedText.Trim();
				selectedText = selectedText.Replace(".", ",");
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
		public int SelectedInt
		{
			get
			{
				selectedText.Trim();
				int data = 0;
				try
				{
					data = (int)Convert.ToInt32(selectedText);
				}
				catch(Exception E)
				{
					MessageBox.Show(E.Message);
				}
				return data;
			}
		}
		public long SelectedLong
		{
			get
			{
				selectedText.Trim();
				int data = 0;
				try
				{
					data = (int)Convert.ToInt64(selectedText);
				}
				catch(Exception E)
				{
					MessageBox.Show(E.Message);
				}
				return data;
			}
		}

		protected override void OnCreateControl()
		{
			textBoxSelect.SelectAll();
		}
	}
}
