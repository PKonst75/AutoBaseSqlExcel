using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for UIF_POS_AutoTiming_02.
	/// </summary>
	public class UIF_POS_AutoTiming_02 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.CheckBox checkBox_goout;
		private System.Windows.Forms.Label label_info;
		private System.Windows.Forms.RadioButton radioButton_parts;
		private System.Windows.Forms.RadioButton radioButton_person;
		private System.Windows.Forms.RadioButton radioButton_diner;
		private System.Windows.Forms.RadioButton radioButton_dayend;
		private System.Windows.Forms.RadioButton radioButton_smoke;
		private System.Windows.Forms.Button button_ok;
		private System.Windows.Forms.Button button_cancel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;


		long card_number = 0;
		int card_year = 0;

		public UIF_POS_AutoTiming_02(long number, int year)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			radioButton_parts.Checked = true;

			card_number = number;
			card_year = year;
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
			this.checkBox_goout = new System.Windows.Forms.CheckBox();
			this.label_info = new System.Windows.Forms.Label();
			this.radioButton_parts = new System.Windows.Forms.RadioButton();
			this.radioButton_person = new System.Windows.Forms.RadioButton();
			this.radioButton_diner = new System.Windows.Forms.RadioButton();
			this.radioButton_dayend = new System.Windows.Forms.RadioButton();
			this.radioButton_smoke = new System.Windows.Forms.RadioButton();
			this.button_ok = new System.Windows.Forms.Button();
			this.button_cancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// checkBox_goout
			// 
			this.checkBox_goout.Location = new System.Drawing.Point(16, 104);
			this.checkBox_goout.Name = "checkBox_goout";
			this.checkBox_goout.Size = new System.Drawing.Size(536, 24);
			this.checkBox_goout.TabIndex = 0;
			this.checkBox_goout.Text = "ОТМЕТИТЬ ВЫЕЗД ИЗ СЕРВИСА";
			// 
			// label_info
			// 
			this.label_info.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.label_info.Location = new System.Drawing.Point(8, 8);
			this.label_info.Name = "label_info";
			this.label_info.Size = new System.Drawing.Size(568, 88);
			this.label_info.TabIndex = 1;
			this.label_info.Text = "Информационная панель";
			this.label_info.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// radioButton_parts
			// 
			this.radioButton_parts.Location = new System.Drawing.Point(16, 160);
			this.radioButton_parts.Name = "radioButton_parts";
			this.radioButton_parts.Size = new System.Drawing.Size(336, 24);
			this.radioButton_parts.TabIndex = 2;
			this.radioButton_parts.Text = "Ожидание ЗАПЧАСТЕЙ";
			// 
			// radioButton_person
			// 
			this.radioButton_person.Location = new System.Drawing.Point(16, 208);
			this.radioButton_person.Name = "radioButton_person";
			this.radioButton_person.Size = new System.Drawing.Size(384, 24);
			this.radioButton_person.TabIndex = 3;
			this.radioButton_person.Text = "Ожидание СПЕЦИАЛИСТА";
			// 
			// radioButton_diner
			// 
			this.radioButton_diner.Location = new System.Drawing.Point(16, 256);
			this.radioButton_diner.Name = "radioButton_diner";
			this.radioButton_diner.Size = new System.Drawing.Size(400, 24);
			this.radioButton_diner.TabIndex = 4;
			this.radioButton_diner.Text = "ОБЕД";
			// 
			// radioButton_dayend
			// 
			this.radioButton_dayend.Location = new System.Drawing.Point(16, 304);
			this.radioButton_dayend.Name = "radioButton_dayend";
			this.radioButton_dayend.Size = new System.Drawing.Size(400, 24);
			this.radioButton_dayend.TabIndex = 5;
			this.radioButton_dayend.Text = "Окончание РАБОЧЕГО ДНЯ";
			// 
			// radioButton_smoke
			// 
			this.radioButton_smoke.Location = new System.Drawing.Point(16, 352);
			this.radioButton_smoke.Name = "radioButton_smoke";
			this.radioButton_smoke.Size = new System.Drawing.Size(400, 24);
			this.radioButton_smoke.TabIndex = 6;
			this.radioButton_smoke.Text = "ПЕРЕКУР";
			// 
			// button_ok
			// 
			this.button_ok.Location = new System.Drawing.Point(16, 416);
			this.button_ok.Name = "button_ok";
			this.button_ok.Size = new System.Drawing.Size(136, 120);
			this.button_ok.TabIndex = 7;
			this.button_ok.Text = "Принять";
			this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
			// 
			// button_cancel
			// 
			this.button_cancel.Location = new System.Drawing.Point(432, 416);
			this.button_cancel.Name = "button_cancel";
			this.button_cancel.Size = new System.Drawing.Size(136, 120);
			this.button_cancel.TabIndex = 8;
			this.button_cancel.Text = "Отменить";
			this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
			// 
			// UIF_POS_AutoTiming_02
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(12, 28);
			this.ClientSize = new System.Drawing.Size(586, 551);
			this.ControlBox = false;
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.button_cancel,
																		  this.button_ok,
																		  this.radioButton_smoke,
																		  this.radioButton_dayend,
																		  this.radioButton_diner,
																		  this.radioButton_person,
																		  this.radioButton_parts,
																		  this.label_info,
																		  this.checkBox_goout});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "UIF_POS_AutoTiming_02";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Приостановка/Возобновление ремонта";
			this.ResumeLayout(false);

		}
		#endregion

		private void button_cancel_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		private void button_ok_Click(object sender, System.EventArgs e)
		{
			//Анализирует условия паузы и устанавливаем ее
			long reason = 0;
			bool goout = false;
			if(radioButton_smoke.Checked == true) reason = 5;
			if(radioButton_dayend.Checked == true) reason = 4;
			if(radioButton_diner.Checked == true) reason = 3;
			if(radioButton_person.Checked == true) reason = 2;
			if(radioButton_parts.Checked == true) reason = 1;

			if(checkBox_goout.Checked == true) goout = true;

			// Записываем паузу в базу данных
			if(DbSqlCardTime.PauseSetBeginTime(card_number, card_year, reason, goout) == false) return;
			
			this.DialogResult = DialogResult.OK;
			this.Close();

		}
	}
}
