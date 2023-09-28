using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for UIF_PTS.
	/// </summary>
	public class UIF_PTS : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox textBox_series;
		private System.Windows.Forms.TextBox textBox_number;
		private System.Windows.Forms.Button button_save;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CS_PTS selected_pts = null;

		public UIF_PTS()
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
			this.textBox_series = new System.Windows.Forms.TextBox();
			this.textBox_number = new System.Windows.Forms.TextBox();
			this.button_save = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// textBox_series
			// 
			this.textBox_series.Location = new System.Drawing.Point(80, 40);
			this.textBox_series.Name = "textBox_series";
			this.textBox_series.Size = new System.Drawing.Size(56, 23);
			this.textBox_series.TabIndex = 0;
			this.textBox_series.Text = "";
			// 
			// textBox_number
			// 
			this.textBox_number.Location = new System.Drawing.Point(152, 40);
			this.textBox_number.Name = "textBox_number";
			this.textBox_number.TabIndex = 1;
			this.textBox_number.Text = "";
			// 
			// button_save
			// 
			this.button_save.Location = new System.Drawing.Point(280, 304);
			this.button_save.Name = "button_save";
			this.button_save.Size = new System.Drawing.Size(96, 24);
			this.button_save.TabIndex = 2;
			this.button_save.Text = "Сохранить";
			this.button_save.Click += new System.EventHandler(this.button_save_Click);
			// 
			// UIF_PTS
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.ClientSize = new System.Drawing.Size(402, 343);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.button_save,
																		  this.textBox_number,
																		  this.textBox_series});
			this.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "UIF_PTS";
			this.Text = "Паспорт технического средства ПТС";
			this.ResumeLayout(false);

		}
		#endregion

		private void button_save_Click(object sender, System.EventArgs e)
		{
			CS_PTS pts = new CS_PTS();
			// Пробуем сохранить данные
			pts.code	= 0;
			pts.series	= textBox_series.Text;
			pts.number	= textBox_number.Text;

			Data_SQL_PTS data_pts = new Data_SQL_PTS(pts.SaveStruct());
			

			// Возвращаем сохраненный ПТС при удачном сохранении
			selected_pts = pts;
		}
	}
}
