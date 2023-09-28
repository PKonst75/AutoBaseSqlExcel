using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormTransmissionType.
	/// </summary>
	public class FormTransmissionType : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox textBoxDescription;
		private System.Windows.Forms.Button ButtonOk;
		private System.Windows.Forms.Label label1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private DbTransmissionType transmissionType;

		public FormTransmissionType(DbTransmissionType transmissionTypeSrc)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			if(transmissionTypeSrc == null)
			{
				transmissionType = new DbTransmissionType();
			}
			else
			{
				transmissionType = new DbTransmissionType(transmissionTypeSrc);
			}
			textBoxDescription.Text = transmissionType.Description;
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
			this.ButtonOk = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// textBoxDescription
			// 
			this.textBoxDescription.Location = new System.Drawing.Point(16, 40);
			this.textBoxDescription.Name = "textBoxDescription";
			this.textBoxDescription.Size = new System.Drawing.Size(344, 23);
			this.textBoxDescription.TabIndex = 0;
			this.textBoxDescription.Text = "";
			// 
			// ButtonOk
			// 
			this.ButtonOk.Location = new System.Drawing.Point(152, 72);
			this.ButtonOk.Name = "ButtonOk";
			this.ButtonOk.TabIndex = 1;
			this.ButtonOk.Text = "OK";
			this.ButtonOk.Click += new System.EventHandler(this.ButtonOk_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 16);
			this.label1.Name = "label1";
			this.label1.TabIndex = 2;
			this.label1.Text = "Описание";
			// 
			// FormTransmissionType
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.ClientSize = new System.Drawing.Size(378, 103);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.label1,
																		  this.ButtonOk,
																		  this.textBoxDescription});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "FormTransmissionType";
			this.Text = "Тип коробки передач";
			this.ResumeLayout(false);

		}
		#endregion

		private void ButtonOk_Click(object sender, System.EventArgs e)
		{
			// Попытка заведения/исправления типа КПП
			transmissionType.Description = textBoxDescription.Text;
			
			if(transmissionType.Write() != true) return;
			this.DialogResult = DialogResult.OK;
			this.Close();
			return;
		}
		public DbTransmissionType TransmissionType
		{
			get
			{
				return transmissionType;
			}
		}
	}
}
