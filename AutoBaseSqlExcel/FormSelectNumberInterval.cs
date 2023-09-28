using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormSelectNumberInterval.
	/// </summary>
	public class FormSelectNumberInterval : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox textBox_from;
		private System.Windows.Forms.TextBox textBox_to;
		private System.Windows.Forms.Button button_ok;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;

		private string selectedText_from = "";
		private string selectedText_to = "";
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FormSelectNumberInterval()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
			this.textBox_from = new System.Windows.Forms.TextBox();
			this.textBox_to = new System.Windows.Forms.TextBox();
			this.button_ok = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// textBox_from
			// 
			this.textBox_from.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox_from.Location = new System.Drawing.Point(48, 40);
			this.textBox_from.Name = "textBox_from";
			this.textBox_from.Size = new System.Drawing.Size(104, 23);
			this.textBox_from.TabIndex = 0;
			this.textBox_from.Text = "";
			// 
			// textBox_to
			// 
			this.textBox_to.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox_to.Location = new System.Drawing.Point(192, 40);
			this.textBox_to.Name = "textBox_to";
			this.textBox_to.TabIndex = 1;
			this.textBox_to.Text = "";
			// 
			// button_ok
			// 
			this.button_ok.Location = new System.Drawing.Point(104, 88);
			this.button_ok.Name = "button_ok";
			this.button_ok.TabIndex = 2;
			this.button_ok.Text = "OK";
			this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(88, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(144, 23);
			this.label1.TabIndex = 3;
			this.label1.Text = "Интервал поиска";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(24, 23);
			this.label2.TabIndex = 4;
			this.label2.Text = "от";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(160, 40);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(32, 23);
			this.label3.TabIndex = 5;
			this.label3.Text = "до";
			// 
			// FormSelectNumberInterval
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.ClientSize = new System.Drawing.Size(304, 117);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.label3,
																		  this.label2,
																		  this.label1,
																		  this.button_ok,
																		  this.textBox_to,
																		  this.textBox_from});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.Name = "FormSelectNumberInterval";
			this.Text = "Поиск в интервале";
			this.ResumeLayout(false);

		}
		#endregion

		private void button_ok_Click(object sender, System.EventArgs e)
		{
			// Запись данных
			selectedText_from = textBox_from.Text;
			selectedText_from.Trim();
			if(selectedText_from.Length == 0) return;
			selectedText_to= textBox_to.Text;
			selectedText_to.Trim();
			if(selectedText_to.Length == 0) return;
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		public long SelectedLong_from
		{
			get
			{
				selectedText_from.Trim();
				long data = 0;
				try
				{
					data = (long)Convert.ToInt64(selectedText_from);
				}
				catch(Exception E)
				{
					MessageBox.Show(E.Message);
				}
				return data;
			}
		}
		public long SelectedLong_to
		{
			get
			{
				selectedText_to.Trim();
				long data = 0;
				try
				{
					data = (long)Convert.ToInt64(selectedText_to);
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
