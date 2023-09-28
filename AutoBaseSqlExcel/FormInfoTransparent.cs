using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormInfoTransparent.
	/// </summary>
	public class FormInfoTransparent : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button_close;
		private System.Windows.Forms.TextBox textBox1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FormInfoTransparent(string caption, string text, ArrayList text_lines)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			this.Text			= caption;
			if(text != "")
				this.textBox1.Text	= text;
			else
			{
				string[] temp_array = new string [text_lines.Count];
				int i = 0;
				foreach (object o in text_lines)
				{
					string txt		= (string)o;
					temp_array[i]	= txt;
					i++;
				}
				this.textBox1.Lines = temp_array;
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
			this.button_close = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// button_close
			// 
			this.button_close.Location = new System.Drawing.Point(248, 280);
			this.button_close.Name = "button_close";
			this.button_close.Size = new System.Drawing.Size(88, 23);
			this.button_close.TabIndex = 0;
			this.button_close.Text = "Закрыть";
			this.button_close.Click += new System.EventHandler(this.button_close_Click);
			// 
			// textBox1
			// 
			this.textBox1.BackColor = System.Drawing.Color.PaleGreen;
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(600, 272);
			this.textBox1.TabIndex = 1;
			this.textBox1.Text = "";
			// 
			// FormInfoTransparent
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(8, 20);
			this.BackColor = System.Drawing.Color.PaleGreen;
			this.ClientSize = new System.Drawing.Size(600, 309);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.button_close,
																		  this.textBox1});
			this.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormInfoTransparent";
			this.Opacity = 0.800000011920929;
			this.Text = "FormInfoTransparent";
			this.ResumeLayout(false);

		}
		#endregion

		private void button_close_Click(object sender, System.EventArgs e)
		{
			// Закрыть
			this.Close();
		}
	}
}
