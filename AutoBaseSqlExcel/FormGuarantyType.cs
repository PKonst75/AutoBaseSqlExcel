using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormGuarantyType.
	/// </summary>
	public class FormGuarantyType : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button buttonOk;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.TextBox textBoxDescription;

		private DbGuarantyType guarantyType;

		public FormGuarantyType(DbGuarantyType guarantyTypeSrc)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			if(guarantyTypeSrc == null)
			{
				guarantyType = new DbGuarantyType();
			}
			else
			{
				guarantyType = new DbGuarantyType(guarantyTypeSrc);
			}
			textBoxDescription.Text = guarantyType.Description;
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
			this.textBoxDescription = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.buttonOk = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// textBoxDescription
			// 
			this.textBoxDescription.Location = new System.Drawing.Point(8, 32);
			this.textBoxDescription.Name = "textBoxDescription";
			this.textBoxDescription.Size = new System.Drawing.Size(368, 23);
			this.textBoxDescription.TabIndex = 0;
			this.textBoxDescription.Text = "Описание";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(184, 23);
			this.label1.TabIndex = 1;
			this.label1.Text = "Описание вида гарантии";
			// 
			// buttonOk
			// 
			this.buttonOk.Location = new System.Drawing.Point(160, 64);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.TabIndex = 2;
			this.buttonOk.Text = "OK";
			this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
			// 
			// FormGuarantyType
			// 
			this.AcceptButton = this.buttonOk;
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.ClientSize = new System.Drawing.Size(392, 95);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.buttonOk,
																		  this.label1,
																		  this.textBoxDescription});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "FormGuarantyType";
			this.Text = "Вид гарантии";
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonOk_Click(object sender, System.EventArgs e)
		{
			// Пытаемся произвести запись элемента, если необходимо
			guarantyType.Description = textBoxDescription.Text;
			
			if(guarantyType.Write() != true) return;
			this.DialogResult = DialogResult.OK;
			this.Close();
			return;
		}
		public DbGuarantyType GuarantyType
		{
			get
			{
				return guarantyType;
			}
		}
	}
}
