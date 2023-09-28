using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for UIF_ServiceOuter.
	/// </summary>
	public class UIF_ServiceOuter : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBox_name;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBox_address;
		private System.Windows.Forms.Button button_new;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public DtServiceOuter service_selected = null;

		public UIF_ServiceOuter()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			
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
			this.textBox_name = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textBox_address = new System.Windows.Forms.TextBox();
			this.button_new = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(120, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "Наименование";
			// 
			// textBox_name
			// 
			this.textBox_name.Location = new System.Drawing.Point(8, 32);
			this.textBox_name.Name = "textBox_name";
			this.textBox_name.Size = new System.Drawing.Size(488, 23);
			this.textBox_name.TabIndex = 1;
			this.textBox_name.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 64);
			this.label2.Name = "label2";
			this.label2.TabIndex = 2;
			this.label2.Text = "Адрес";
			// 
			// textBox_address
			// 
			this.textBox_address.Location = new System.Drawing.Point(8, 88);
			this.textBox_address.Name = "textBox_address";
			this.textBox_address.Size = new System.Drawing.Size(488, 23);
			this.textBox_address.TabIndex = 3;
			this.textBox_address.Text = "";
			// 
			// button_new
			// 
			this.button_new.Location = new System.Drawing.Point(400, 120);
			this.button_new.Name = "button_new";
			this.button_new.Size = new System.Drawing.Size(96, 23);
			this.button_new.TabIndex = 4;
			this.button_new.Text = "Добавить";
			this.button_new.Click += new System.EventHandler(this.button_new_Click);
			// 
			// UIF_ServiceOuter
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.ClientSize = new System.Drawing.Size(512, 149);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.button_new,
																		  this.textBox_address,
																		  this.label2,
																		  this.textBox_name,
																		  this.label1});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.Name = "UIF_ServiceOuter";
			this.Text = "Внешний сервис";
			this.ResumeLayout(false);

		}
		#endregion

		private void button_new_Click(object sender, System.EventArgs e)
		{
			// Добавление нового сервиса в список
			string name = textBox_name.Text;
			string address = textBox_address.Text;

			name.Trim();
			address.Trim();

			if(name == "") return;

			DtServiceOuter service = new DtServiceOuter();
			service.SetData("НАИМЕНОВАНИЕ", (object) name);
			service.SetData("АДРЕС", (object) address);

			service = DbSqlServiceOuter.Insert(service);
			if(service == null) return;

			service_selected = service;
			DialogResult = DialogResult.OK;
			this.Close();
		}

		
	}
}
