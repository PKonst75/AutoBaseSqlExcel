using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormProgress.
	/// </summary>
	public class FormProgress : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ProgressBar progressBar1;
		private System.Windows.Forms.TextBox textBox_count;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FormProgress()
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
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.textBox_count = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// progressBar1
			// 
			this.progressBar1.Location = new System.Drawing.Point(16, 40);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(576, 23);
			this.progressBar1.TabIndex = 0;
			// 
			// textBox_count
			// 
			this.textBox_count.Location = new System.Drawing.Point(16, 72);
			this.textBox_count.Name = "textBox_count";
			this.textBox_count.TabIndex = 1;
			this.textBox_count.Text = "";
			// 
			// FormProgress
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(8, 19);
			this.ClientSize = new System.Drawing.Size(608, 125);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.textBox_count,
																		  this.progressBar1});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.Name = "FormProgress";
			this.Text = "FormProgress";
			this.ResumeLayout(false);

		}
		#endregion

		public int Minimum
		{
			set
			{
				progressBar1.Minimum = value;
			}
		}
		public int Maximum
		{
			set
			{
				progressBar1.Maximum = value;
			}
		}
		public int Step
		{
			set
			{
				progressBar1.Step = value;
			}
		}
		public int Value
		{
			set
			{
				progressBar1.Value = value;
			}
		}
		public void PerformStep()
		{
			progressBar1.PerformStep();
			this.BringToFront();
			textBox_count.Text	= progressBar1.Value.ToString();
		}
	}
}
