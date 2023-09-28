using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for UIF_AutoAlarm.
	/// </summary>
	public class UIF_AutoAlarm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox textBox_auto;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.DateTimePicker dateTimePicker_date;
		private System.Windows.Forms.CheckBox checkBox_outer;
		private System.Windows.Forms.TextBox textBox_alarm;
		private System.Windows.Forms.Button button_select_alarm;
		private System.Windows.Forms.Button button_cancel_alarm;
		private System.Windows.Forms.Button button_cancel_service;
		private System.Windows.Forms.Button button_select_service;
		private System.Windows.Forms.TextBox textBox_service;
		private System.Windows.Forms.Button button_cancel_reason;
		private System.Windows.Forms.Button button_select_reason;
		private System.Windows.Forms.TextBox textBox_reason;
		private System.Windows.Forms.Button button_set;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		long code_auto = 0;
		long code_alarm = 0;
		long code_service = 0;
		long code_reason = 0;
		long flag = 0;

		public UIF_AutoAlarm(long code)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			DtAuto auto = DbSqlAuto.Find(code);
			if(auto == null)
				textBox_auto.Text = "Автомобиль не обнаружен";
			else
			{
				textBox_auto.Text = (string)auto.GetData("VIN");
				code_auto = (long)auto.GetData("КОД_АВТОМОБИЛЬ");
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
			this.textBox_auto = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.dateTimePicker_date = new System.Windows.Forms.DateTimePicker();
			this.checkBox_outer = new System.Windows.Forms.CheckBox();
			this.textBox_alarm = new System.Windows.Forms.TextBox();
			this.button_select_alarm = new System.Windows.Forms.Button();
			this.button_cancel_alarm = new System.Windows.Forms.Button();
			this.button_cancel_service = new System.Windows.Forms.Button();
			this.button_select_service = new System.Windows.Forms.Button();
			this.textBox_service = new System.Windows.Forms.TextBox();
			this.button_cancel_reason = new System.Windows.Forms.Button();
			this.button_select_reason = new System.Windows.Forms.Button();
			this.textBox_reason = new System.Windows.Forms.TextBox();
			this.button_set = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// textBox_auto
			// 
			this.textBox_auto.Location = new System.Drawing.Point(8, 40);
			this.textBox_auto.Name = "textBox_auto";
			this.textBox_auto.ReadOnly = true;
			this.textBox_auto.Size = new System.Drawing.Size(552, 23);
			this.textBox_auto.TabIndex = 0;
			this.textBox_auto.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 16);
			this.label1.Name = "label1";
			this.label1.TabIndex = 1;
			this.label1.Text = "Автомобиль";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 136);
			this.label2.Name = "label2";
			this.label2.TabIndex = 2;
			this.label2.Text = "Сигнализация";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 104);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(216, 23);
			this.label3.TabIndex = 3;
			this.label3.Text = "Сторонняя установка (ДА/НЕТ)";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 184);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(136, 23);
			this.label4.TabIndex = 4;
			this.label4.Text = "Место установки";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(8, 232);
			this.label5.Name = "label5";
			this.label5.TabIndex = 5;
			this.label5.Text = "Причина";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(8, 72);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(240, 23);
			this.label6.TabIndex = 6;
			this.label6.Text = "Дата установки (можно примерно)";
			// 
			// dateTimePicker_date
			// 
			this.dateTimePicker_date.Location = new System.Drawing.Point(248, 72);
			this.dateTimePicker_date.Name = "dateTimePicker_date";
			this.dateTimePicker_date.TabIndex = 7;
			// 
			// checkBox_outer
			// 
			this.checkBox_outer.Location = new System.Drawing.Point(248, 104);
			this.checkBox_outer.Name = "checkBox_outer";
			this.checkBox_outer.TabIndex = 8;
			// 
			// textBox_alarm
			// 
			this.textBox_alarm.Location = new System.Drawing.Point(8, 152);
			this.textBox_alarm.Name = "textBox_alarm";
			this.textBox_alarm.ReadOnly = true;
			this.textBox_alarm.Size = new System.Drawing.Size(488, 23);
			this.textBox_alarm.TabIndex = 9;
			this.textBox_alarm.Text = "";
			// 
			// button_select_alarm
			// 
			this.button_select_alarm.Location = new System.Drawing.Point(496, 152);
			this.button_select_alarm.Name = "button_select_alarm";
			this.button_select_alarm.Size = new System.Drawing.Size(24, 23);
			this.button_select_alarm.TabIndex = 10;
			this.button_select_alarm.Text = "...";
			this.button_select_alarm.Click += new System.EventHandler(this.button_select_alarm_Click);
			// 
			// button_cancel_alarm
			// 
			this.button_cancel_alarm.Location = new System.Drawing.Point(520, 152);
			this.button_cancel_alarm.Name = "button_cancel_alarm";
			this.button_cancel_alarm.Size = new System.Drawing.Size(24, 23);
			this.button_cancel_alarm.TabIndex = 11;
			this.button_cancel_alarm.Text = "X";
			this.button_cancel_alarm.Click += new System.EventHandler(this.button_cancel_alarm_Click);
			// 
			// button_cancel_service
			// 
			this.button_cancel_service.Location = new System.Drawing.Point(520, 200);
			this.button_cancel_service.Name = "button_cancel_service";
			this.button_cancel_service.Size = new System.Drawing.Size(24, 23);
			this.button_cancel_service.TabIndex = 14;
			this.button_cancel_service.Text = "X";
			this.button_cancel_service.Click += new System.EventHandler(this.button_cancel_service_Click);
			// 
			// button_select_service
			// 
			this.button_select_service.Location = new System.Drawing.Point(496, 200);
			this.button_select_service.Name = "button_select_service";
			this.button_select_service.Size = new System.Drawing.Size(24, 23);
			this.button_select_service.TabIndex = 13;
			this.button_select_service.Text = "...";
			this.button_select_service.Click += new System.EventHandler(this.button_select_service_Click);
			// 
			// textBox_service
			// 
			this.textBox_service.Location = new System.Drawing.Point(8, 200);
			this.textBox_service.Name = "textBox_service";
			this.textBox_service.ReadOnly = true;
			this.textBox_service.Size = new System.Drawing.Size(488, 23);
			this.textBox_service.TabIndex = 12;
			this.textBox_service.Text = "";
			// 
			// button_cancel_reason
			// 
			this.button_cancel_reason.Location = new System.Drawing.Point(520, 256);
			this.button_cancel_reason.Name = "button_cancel_reason";
			this.button_cancel_reason.Size = new System.Drawing.Size(24, 23);
			this.button_cancel_reason.TabIndex = 17;
			this.button_cancel_reason.Text = "X";
			this.button_cancel_reason.Click += new System.EventHandler(this.button_cancel_reason_Click);
			// 
			// button_select_reason
			// 
			this.button_select_reason.Location = new System.Drawing.Point(496, 256);
			this.button_select_reason.Name = "button_select_reason";
			this.button_select_reason.Size = new System.Drawing.Size(24, 23);
			this.button_select_reason.TabIndex = 16;
			this.button_select_reason.Text = "...";
			this.button_select_reason.Click += new System.EventHandler(this.button_select_reason_Click);
			// 
			// textBox_reason
			// 
			this.textBox_reason.Location = new System.Drawing.Point(8, 256);
			this.textBox_reason.Name = "textBox_reason";
			this.textBox_reason.ReadOnly = true;
			this.textBox_reason.Size = new System.Drawing.Size(488, 23);
			this.textBox_reason.TabIndex = 15;
			this.textBox_reason.Text = "";
			// 
			// button_set
			// 
			this.button_set.Location = new System.Drawing.Point(448, 296);
			this.button_set.Name = "button_set";
			this.button_set.Size = new System.Drawing.Size(96, 23);
			this.button_set.TabIndex = 18;
			this.button_set.Text = "Установить";
			this.button_set.Click += new System.EventHandler(this.button_set_Click);
			// 
			// UIF_AutoAlarm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.ClientSize = new System.Drawing.Size(568, 333);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.button_set,
																		  this.button_cancel_reason,
																		  this.button_select_reason,
																		  this.textBox_reason,
																		  this.button_cancel_service,
																		  this.button_select_service,
																		  this.textBox_service,
																		  this.button_cancel_alarm,
																		  this.button_select_alarm,
																		  this.textBox_alarm,
																		  this.checkBox_outer,
																		  this.dateTimePicker_date,
																		  this.label6,
																		  this.label5,
																		  this.label4,
																		  this.label3,
																		  this.label2,
																		  this.label1,
																		  this.textBox_auto});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.Name = "UIF_AutoAlarm";
			this.Text = "Отметка установки сигнализации на автомобиль";
			this.ResumeLayout(false);

		}
		#endregion

		private void button_set_Click(object sender, System.EventArgs e)
		{
			// Пробуем записать данные
			if(code_auto == 0) return;
			if(checkBox_outer.Checked == true)
			{
				flag = 1;
			}
			else
			{
				flag = 0;
				code_service = 0;
			}
			DateTime date = dateTimePicker_date.Value;

			DtAutoAlarm alarm = new DtAutoAlarm();
			alarm.SetData("КОД_АВТОМОБИЛЬ", code_auto);
			alarm.SetData("ФЛАГ_УСТАНОВКА", flag);
			alarm.SetData("КОД_СИГНАЛИЗАЦИЯ", code_alarm);
			alarm.SetData("КОД_СЕРВИС", code_service);
			alarm.SetData("КОД_ПРИЧИНА", code_reason);
			alarm.SetData("ДАТА_УСТАНОВКА", date);

			if(DbSqlAutoAlarm.Insert(alarm) == false) return;
			MessageBox.Show("Отметка установлена");
			this.Close();
		}

		private void button_select_alarm_Click(object sender, System.EventArgs e)
		{
			// Запуск выбора сигнализации
			object o = UserInterface.ListAlarm(0, null, 0, UserInterface.ClickType.Select);
			if(o == null) return;
			DtListAlarm alarm = (DtListAlarm)o;
			code_alarm = (long)alarm.GetData("КОД_СИГНАЛИЗАЦИЯ");
			textBox_alarm.Text = (string)alarm.GetData("НАИМЕНОВАНИЕ");
		}

		private void button_cancel_alarm_Click(object sender, System.EventArgs e)
		{
			// Отмена сигнализации
			code_alarm = 0;
			textBox_alarm.Text = "";
		}

		private void button_select_service_Click(object sender, System.EventArgs e)
		{
			// Запуск установщика
			object o = UserInterface.ListServiceOuter(0, null, 0, UserInterface.ClickType.Select);
			if(o == null) return;
			DtServiceOuter service = (DtServiceOuter)o;
			code_service = (long)service.GetData("КОД_СЕРВИС");
			textBox_service.Text = (string)service.GetData("НАИМЕНОВАНИЕ");
		}

		private void button_cancel_service_Click(object sender, System.EventArgs e)
		{
			// Отмена установщика
			code_service = 0;
			textBox_service.Text = "";
		}

		private void button_select_reason_Click(object sender, System.EventArgs e)
		{
			// Запуск выбора причины
			object o = UserInterface.ListCommonReason(0, null, 0, UserInterface.ClickType.Select);
			if(o == null) return;
			DtCommonReason reason = (DtCommonReason)o;
			code_reason = (long)reason.GetData("КОД_ПРИЧИНА");
			textBox_reason.Text = (string)reason.GetData("ОПИСАНИЕ");
		}

		private void button_cancel_reason_Click(object sender, System.EventArgs e)
		{
			// Отмена причины
			code_reason = 0;
			textBox_reason.Text = "";
		}
	}
}
