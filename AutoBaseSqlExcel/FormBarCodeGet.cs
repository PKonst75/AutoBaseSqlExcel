using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormBarCodeGet.
	/// </summary>
	public class FormBarCodeGet : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox textBox1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        InputLanguage lang = null;
        InputLanguage lang_eng = null;
		private string barCode;

		public FormBarCodeGet()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
            lang = InputLanguage.CurrentInputLanguage; // Получаем текущий язык ввода

            // Устанавливаем язык ввода на английский
            InputLanguageCollection coll = InputLanguage.InstalledInputLanguages;
            foreach (InputLanguage l in coll)
            {
                if (l.Culture.ToString() == "en-US") lang_eng = l;
            }
            if (lang_eng != null) InputLanguage.CurrentInputLanguage = lang_eng;
            else
            {
                MessageBox.Show("Отсутствует язык ввода");
                this.Close();
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
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// textBox1
			// 
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(344, 26);
			this.textBox1.TabIndex = 0;
			this.textBox1.Text = "";
			this.textBox1.KeyDown += new KeyEventHandler(this.textBox1_KeyDown);
			// 
			// FormBarCodeGet
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(8, 19);
			this.ClientSize = new System.Drawing.Size(344, 29);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.textBox1});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "FormBarCodeGet";
			this.Text = "Ожидаю штрих-код";
			this.ResumeLayout(false);

		}
		#endregion

		protected void textBox1_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Escape)
			{
                if (lang != null) InputLanguage.CurrentInputLanguage = lang;    // Возвращаем обратно язык ввода
				this.Close();
				return;
			}
			if(e.KeyCode == Keys.Enter)
			{
                if (lang != null) InputLanguage.CurrentInputLanguage = lang;    // Возвращаем обратно язык ввода
				barCode	= textBox1.Text;
				this.DialogResult = DialogResult.OK;
				this.Close();
				return;
			}
		}

		public string BarCode
		{
			get
			{
				return barCode;
			}
		}
	}
}
