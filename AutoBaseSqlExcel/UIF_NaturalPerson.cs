using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for UIF_NaturalPerson.
	/// </summary>
	public class UIF_NaturalPerson : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox tb_name_first;
		private System.Windows.Forms.TextBox tb_name_last;
		private System.Windows.Forms.TextBox tb_name_patronymic;
		private System.Windows.Forms.TextBox tb_service_code;
		private System.Windows.Forms.DateTimePicker dtp_info_birthday;
		private System.Windows.Forms.TextBox tb_info_birthplace;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public UIF_NaturalPerson()
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
			this.tb_name_first = new System.Windows.Forms.TextBox();
			this.tb_name_last = new System.Windows.Forms.TextBox();
			this.tb_name_patronymic = new System.Windows.Forms.TextBox();
			this.tb_service_code = new System.Windows.Forms.TextBox();
			this.dtp_info_birthday = new System.Windows.Forms.DateTimePicker();
			this.tb_info_birthplace = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// tb_name_first
			// 
			this.tb_name_first.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tb_name_first.Location = new System.Drawing.Point(88, 32);
			this.tb_name_first.Name = "tb_name_first";
			this.tb_name_first.Size = new System.Drawing.Size(224, 20);
			this.tb_name_first.TabIndex = 0;
			this.tb_name_first.Text = "";
			// 
			// tb_name_last
			// 
			this.tb_name_last.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tb_name_last.Location = new System.Drawing.Point(88, 56);
			this.tb_name_last.Name = "tb_name_last";
			this.tb_name_last.Size = new System.Drawing.Size(224, 20);
			this.tb_name_last.TabIndex = 1;
			this.tb_name_last.Text = "";
			// 
			// tb_name_patronymic
			// 
			this.tb_name_patronymic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tb_name_patronymic.Location = new System.Drawing.Point(88, 80);
			this.tb_name_patronymic.Name = "tb_name_patronymic";
			this.tb_name_patronymic.Size = new System.Drawing.Size(224, 20);
			this.tb_name_patronymic.TabIndex = 2;
			this.tb_name_patronymic.Text = "";
			// 
			// tb_service_code
			// 
			this.tb_service_code.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tb_service_code.Location = new System.Drawing.Point(88, 8);
			this.tb_service_code.Name = "tb_service_code";
			this.tb_service_code.TabIndex = 3;
			this.tb_service_code.Text = "";
			// 
			// dtp_info_birthday
			// 
			this.dtp_info_birthday.Location = new System.Drawing.Point(88, 112);
			this.dtp_info_birthday.Name = "dtp_info_birthday";
			this.dtp_info_birthday.Size = new System.Drawing.Size(224, 20);
			this.dtp_info_birthday.TabIndex = 4;
			// 
			// tb_info_birthplace
			// 
			this.tb_info_birthplace.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tb_info_birthplace.Location = new System.Drawing.Point(88, 144);
			this.tb_info_birthplace.Name = "tb_info_birthplace";
			this.tb_info_birthplace.Size = new System.Drawing.Size(224, 20);
			this.tb_info_birthplace.TabIndex = 5;
			this.tb_info_birthplace.Text = "";
			// 
			// UIF_NaturalPerson
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(552, 273);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.tb_info_birthplace,
																		  this.dtp_info_birthday,
																		  this.tb_service_code,
																		  this.tb_name_patronymic,
																		  this.tb_name_last,
																		  this.tb_name_first});
			this.Name = "UIF_NaturalPerson";
			this.Text = "UIF_NaturalPerson";
			this.ResumeLayout(false);

		}
		#endregion
	}
}
