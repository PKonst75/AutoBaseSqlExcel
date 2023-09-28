using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormAutoFactory.
	/// </summary>
	public class FormAutoFactory : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox textBoxName;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button buttonOk;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		DbAutoFactory autoFactory;

		public FormAutoFactory(DbAutoFactory source)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			if(source == null)
			{
				autoFactory = new DbAutoFactory();
			}
			else
			{
				autoFactory = new DbAutoFactory(source);
			}
			textBoxName.Text = autoFactory.Name;
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
			this.textBoxName = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.buttonOk = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// textBoxName
			// 
			this.textBoxName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.textBoxName.Location = new System.Drawing.Point(8, 40);
			this.textBoxName.Name = "textBoxName";
			this.textBoxName.Size = new System.Drawing.Size(432, 23);
			this.textBoxName.TabIndex = 0;
			this.textBoxName.Text = "textBox1";
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(128, 23);
			this.label1.TabIndex = 1;
			this.label1.Text = "������������";
			// 
			// buttonOk
			// 
			this.buttonOk.Location = new System.Drawing.Point(184, 72);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.TabIndex = 2;
			this.buttonOk.Text = "��";
			this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
			// 
			// FormAutoFactory
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.ClientSize = new System.Drawing.Size(448, 109);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.buttonOk,
																		  this.label1,
																		  this.textBoxName});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "FormAutoFactory";
			this.Text = "�����-������������";
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonOk_Click(object sender, System.EventArgs e)
		{
			// ��������� �����-������������
			autoFactory.Name = textBoxName.Text;
			if(Db.ShowFaults()) return;

			if(autoFactory.Write() != true) return;
			this.DialogResult = DialogResult.OK;
			this.Close();
			return;
		}

		public DbAutoFactory AutoFactory
		{
			get
			{
				return autoFactory;
			}
		}
	}
}
