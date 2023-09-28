using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormUpdateStaff.
	/// </summary>
	public class FormUpdateStaff : System.Windows.Forms.Form
	{
		private System.Windows.Forms.CheckBox checkBox_avaliable;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.TextBox textBox_login;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBox_esign;
		private System.Windows.Forms.GroupBox groupBox_datasys;
		private System.Windows.Forms.GroupBox groupBox_datapersonal;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBox_code;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textBox_surname;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textBox_name;
		private System.Windows.Forms.TextBox textBox_patronymic;
		private System.Windows.Forms.Label label7;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.Button button_setpass;

		DtStaff staff = null;

		public FormUpdateStaff(long code)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			// Поиск персонала
			staff = DbSqlStaff.Find(code);

			// Заполнение полей с данными
			if(staff != null)
			{
				textBox_surname.Text	= (string)staff.GetData("ФАМИЛИЯ_ПЕРСОНАЛ");
				textBox_name.Text		= (string)staff.GetData("ИМЯ_ПЕРСОНАЛ");
				textBox_patronymic.Text = (string)staff.GetData("ОТЧЕСТВО_ПЕРСОНАЛ");

				textBox_login.Text	= (string)staff.GetData("ЛОГИН");
				long esign = (long)staff.GetData("ЭЛЕКТРОННАЯ_ПОДПИСЬ_ПЕРСОНАЛ");
				if(esign != 0)
					textBox_esign.Text	= esign.ToString();
				else
					textBox_esign.Text	= "";
				textBox_code.Text	= staff.GetData("КОД_ПЕРСОНАЛ").ToString();

				checkBox_avaliable.Checked	= (bool)staff.GetData("РАБОТАЕТ");
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
			this.components = new System.ComponentModel.Container();
			this.checkBox_avaliable = new System.Windows.Forms.CheckBox();
			this.groupBox_datasys = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.textBox_login = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textBox_esign = new System.Windows.Forms.TextBox();
			this.groupBox_datapersonal = new System.Windows.Forms.GroupBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textBox_code = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.textBox_surname = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.textBox_name = new System.Windows.Forms.TextBox();
			this.textBox_patronymic = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.button_setpass = new System.Windows.Forms.Button();
			this.groupBox_datasys.SuspendLayout();
			this.groupBox_datapersonal.SuspendLayout();
			this.SuspendLayout();
			// 
			// checkBox_avaliable
			// 
			this.checkBox_avaliable.Location = new System.Drawing.Point(488, 8);
			this.checkBox_avaliable.Name = "checkBox_avaliable";
			this.checkBox_avaliable.TabIndex = 0;
			this.checkBox_avaliable.Text = "РАБОТАЕТ";
			// 
			// groupBox_datasys
			// 
			this.groupBox_datasys.Controls.AddRange(new System.Windows.Forms.Control[] {
																						   this.button_setpass,
																						   this.textBox_code,
																						   this.label4,
																						   this.textBox_esign,
																						   this.label3,
																						   this.textBox_login,
																						   this.label1});
			this.groupBox_datasys.Location = new System.Drawing.Point(16, 152);
			this.groupBox_datasys.Name = "groupBox_datasys";
			this.groupBox_datasys.Size = new System.Drawing.Size(424, 160);
			this.groupBox_datasys.TabIndex = 1;
			this.groupBox_datasys.TabStop = false;
			this.groupBox_datasys.Text = "Системные данные";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(88, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "Логин";
			this.toolTip1.SetToolTip(this.label1, "Логин для доступа в систему");
			// 
			// textBox_login
			// 
			this.textBox_login.Location = new System.Drawing.Point(176, 24);
			this.textBox_login.Name = "textBox_login";
			this.textBox_login.Size = new System.Drawing.Size(232, 27);
			this.textBox_login.TabIndex = 1;
			this.textBox_login.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 88);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(160, 23);
			this.label3.TabIndex = 4;
			this.label3.Text = "Цифровая подпись";
			this.toolTip1.SetToolTip(this.label3, "Уникальная цифровая подпись");
			// 
			// textBox_esign
			// 
			this.textBox_esign.Location = new System.Drawing.Point(176, 88);
			this.textBox_esign.Name = "textBox_esign";
			this.textBox_esign.Size = new System.Drawing.Size(232, 27);
			this.textBox_esign.TabIndex = 5;
			this.textBox_esign.Text = "";
			// 
			// groupBox_datapersonal
			// 
			this.groupBox_datapersonal.Controls.AddRange(new System.Windows.Forms.Control[] {
																								this.label7,
																								this.textBox_patronymic,
																								this.textBox_name,
																								this.label6,
																								this.textBox_surname,
																								this.label5});
			this.groupBox_datapersonal.Location = new System.Drawing.Point(16, 8);
			this.groupBox_datapersonal.Name = "groupBox_datapersonal";
			this.groupBox_datapersonal.Size = new System.Drawing.Size(424, 136);
			this.groupBox_datapersonal.TabIndex = 2;
			this.groupBox_datapersonal.TabStop = false;
			this.groupBox_datapersonal.Text = "Персональные данные";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 120);
			this.label4.Name = "label4";
			this.label4.TabIndex = 6;
			this.label4.Text = "КОД";
			this.toolTip1.SetToolTip(this.label4, "Код в базе данных");
			// 
			// textBox_code
			// 
			this.textBox_code.BackColor = System.Drawing.Color.LightSkyBlue;
			this.textBox_code.Enabled = false;
			this.textBox_code.Location = new System.Drawing.Point(176, 120);
			this.textBox_code.Name = "textBox_code";
			this.textBox_code.Size = new System.Drawing.Size(232, 27);
			this.textBox_code.TabIndex = 7;
			this.textBox_code.Text = "";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(16, 24);
			this.label5.Name = "label5";
			this.label5.TabIndex = 0;
			this.label5.Text = "Фамилия";
			// 
			// textBox_surname
			// 
			this.textBox_surname.Location = new System.Drawing.Point(128, 24);
			this.textBox_surname.Name = "textBox_surname";
			this.textBox_surname.Size = new System.Drawing.Size(288, 27);
			this.textBox_surname.TabIndex = 1;
			this.textBox_surname.Text = "";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(16, 56);
			this.label6.Name = "label6";
			this.label6.TabIndex = 2;
			this.label6.Text = "Имя";
			// 
			// textBox_name
			// 
			this.textBox_name.Location = new System.Drawing.Point(128, 56);
			this.textBox_name.Name = "textBox_name";
			this.textBox_name.Size = new System.Drawing.Size(288, 27);
			this.textBox_name.TabIndex = 3;
			this.textBox_name.Text = "";
			// 
			// textBox_patronymic
			// 
			this.textBox_patronymic.Location = new System.Drawing.Point(128, 88);
			this.textBox_patronymic.Name = "textBox_patronymic";
			this.textBox_patronymic.Size = new System.Drawing.Size(288, 27);
			this.textBox_patronymic.TabIndex = 4;
			this.textBox_patronymic.Text = "";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(16, 88);
			this.label7.Name = "label7";
			this.label7.TabIndex = 5;
			this.label7.Text = "Отчество";
			// 
			// button_setpass
			// 
			this.button_setpass.Location = new System.Drawing.Point(176, 56);
			this.button_setpass.Name = "button_setpass";
			this.button_setpass.Size = new System.Drawing.Size(232, 23);
			this.button_setpass.TabIndex = 8;
			this.button_setpass.Text = "Задать пароль";
			this.button_setpass.Click += new System.EventHandler(this.button_setpass_Click);
			// 
			// FormUpdateStaff
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(8, 20);
			this.BackColor = System.Drawing.Color.DeepSkyBlue;
			this.ClientSize = new System.Drawing.Size(600, 335);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.groupBox_datapersonal,
																		  this.groupBox_datasys,
																		  this.checkBox_avaliable});
			this.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "FormUpdateStaff";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Работа с персоналом";
			this.groupBox_datasys.ResumeLayout(false);
			this.groupBox_datapersonal.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void button_setpass_Click(object sender, System.EventArgs e)
		{
			// Задать новый пароль для пользователя
		}
	}
}
