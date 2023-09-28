using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormWorkshop.
	/// </summary>
	public class FormWorkshop : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxName;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBoxImplement;
		private System.Windows.Forms.Button buttonOk;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private DbWorkshop workshop;

		public FormWorkshop(DbWorkshop src)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			if(src == null)
				workshop = new DbWorkshop();
			else
				workshop = new DbWorkshop(src);

			textBoxName.Text		= workshop.Name;
			textBoxImplement.Text	= workshop.Implement;
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
			this.label1 = new System.Windows.Forms.Label();
			this.textBoxName = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textBoxImplement = new System.Windows.Forms.TextBox();
			this.buttonOk = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(128, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "Наименование";
			// 
			// textBoxName
			// 
			this.textBoxName.Location = new System.Drawing.Point(128, 8);
			this.textBoxName.Name = "textBoxName";
			this.textBoxName.Size = new System.Drawing.Size(360, 23);
			this.textBoxName.TabIndex = 1;
			this.textBoxName.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(104, 23);
			this.label2.TabIndex = 2;
			this.label2.Text = "Применимость";
			// 
			// textBoxImplement
			// 
			this.textBoxImplement.Location = new System.Drawing.Point(128, 40);
			this.textBoxImplement.Name = "textBoxImplement";
			this.textBoxImplement.Size = new System.Drawing.Size(360, 23);
			this.textBoxImplement.TabIndex = 3;
			this.textBoxImplement.Text = "";
			// 
			// buttonOk
			// 
			this.buttonOk.Location = new System.Drawing.Point(216, 72);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.TabIndex = 4;
			this.buttonOk.Text = "OK";
			this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
			// 
			// FormWorkshop
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.ClientSize = new System.Drawing.Size(504, 101);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.buttonOk,
																		  this.textBoxImplement,
																		  this.label2,
																		  this.textBoxName,
																		  this.label1});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.Name = "FormWorkshop";
			this.Text = "Подразделение";
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonOk_Click(object sender, System.EventArgs e)
		{
			// Применение изменений
			workshop.Name		= textBoxName.Text;
			workshop.Implement	= textBoxImplement.Text;
			if(Db.ShowFaults() == true) return;

			if(workshop.Write() == false) return;
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		public DbWorkshop Workshop
		{
			get
			{
				return workshop;
			}
		}
	}
}
