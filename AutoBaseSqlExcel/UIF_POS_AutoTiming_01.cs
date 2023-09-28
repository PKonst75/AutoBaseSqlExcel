using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for UIF_POS_AutoTiming_01.
	/// </summary>
	public class UIF_POS_AutoTiming_01 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button_action;
		private System.Windows.Forms.Button button_notime;
		private System.Windows.Forms.Button button_cancel;
		private System.Windows.Forms.Label label1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		int stage = 0;
		int pause_stage = 0;
		long card_number = 0;
		private System.Windows.Forms.Button button_pause;
		int card_year = 0;

		public UIF_POS_AutoTiming_01(long number, int year, string info)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			label1.Text = info;
			card_number = number;
			card_year = year;

			// Изначально все кнопки неактивны, часть спрятаны
			button_action.Enabled = false;
			button_notime.Enabled = false;
			button_pause.Enabled = false;
			button_pause.Visible = false;
			button_notime.Visible = false;

			if(card_number == 0 || card_year == 0)
			{
				// Закрываем все возможности
				label1.Text = "ОШИБКА";
				button_action.Enabled = false;
				button_notime.Enabled = false;
				return;
			}

			DtCardTime time = DbSqlCardTime.Find(number, year);
			if(time == null)
			{
				// Первичная обработка
				stage = 1;
				button_action.Text = "Установить время заезда";
				button_notime.Enabled = true;
				button_action.Enabled = true;
				button_notime.Visible = true;
				return;
			}
			if((bool)time.GetData("НЕЗАЕЗД")==true || (bool)time.GetData("ЕСТЬ_ВРЕМЯ_ЗАЕЗД")==false)
			{
				label1.Text = "ОШИБКА";
				button_action.Enabled = false;
				button_notime.Enabled = false;
				stage = 0;
				return;
			}
			if((bool)time.GetData("ЕСТЬ_ВРЕМЯ_НАЧАЛО")==false)
			{
				button_action.Text = "Отметить начало ремонта";
				button_action.Enabled = true;
				stage = 2;
				return;
			}
			if((bool)time.GetData("ЕСТЬ_ВРЕМЯ_ОКОНЧАНИЕ")==false)
			{
				button_action.Text = "Отметить окончание ремонта";
				if((bool)time.GetData("ПАУЗА") == false)
				{
					button_pause.Text = "Отметить приостановку ремонта";
					button_pause.Enabled = true;
					button_pause.Visible = true;
					button_action.Enabled = true;
					pause_stage = 1;
					stage = 3;
				}
				else
				{
					button_pause.Text = "Отметить возобновление ремонта";
					button_pause.Enabled = true;
					button_pause.Visible = true;
					button_action.Enabled = false;
					stage = 3;
					pause_stage = 2;
				}
				return;
			}
			if((bool)time.GetData("ЕСТЬ_ВРЕМЯ_ВЫЕЗД")==false)
			{
				button_action.Text = "Отметить выезд";
				button_action.Enabled = true;
				button_notime.Enabled = false;
				stage = 4;
				return;
			}
			label1.Text = "ОШИБКА";
			button_action.Enabled = false;
			button_notime.Enabled = false;
			stage = 0;
			return;
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
			this.button_action = new System.Windows.Forms.Button();
			this.button_notime = new System.Windows.Forms.Button();
			this.button_cancel = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.button_pause = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// button_action
			// 
			this.button_action.Location = new System.Drawing.Point(16, 152);
			this.button_action.Name = "button_action";
			this.button_action.Size = new System.Drawing.Size(160, 128);
			this.button_action.TabIndex = 0;
			this.button_action.Text = "Первичное действие";
			this.button_action.Click += new System.EventHandler(this.button_action_Click);
			// 
			// button_notime
			// 
			this.button_notime.Location = new System.Drawing.Point(272, 152);
			this.button_notime.Name = "button_notime";
			this.button_notime.Size = new System.Drawing.Size(160, 128);
			this.button_notime.TabIndex = 1;
			this.button_notime.Text = "Автомобиль не заезжал в сервс";
			this.button_notime.Click += new System.EventHandler(this.button_notime_Click);
			// 
			// button_cancel
			// 
			this.button_cancel.Location = new System.Drawing.Point(152, 296);
			this.button_cancel.Name = "button_cancel";
			this.button_cancel.Size = new System.Drawing.Size(160, 152);
			this.button_cancel.TabIndex = 2;
			this.button_cancel.Text = "Отмена действия";
			this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(424, 136);
			this.label1.TabIndex = 3;
			this.label1.Text = "Информационная панель";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// button_pause
			// 
			this.button_pause.Location = new System.Drawing.Point(272, 152);
			this.button_pause.Name = "button_pause";
			this.button_pause.Size = new System.Drawing.Size(160, 128);
			this.button_pause.TabIndex = 4;
			this.button_pause.Text = "Остановка/возобновление ремонта";
			this.button_pause.Click += new System.EventHandler(this.button_pause_Click);
			// 
			// UIF_POS_AutoTiming_01
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(12, 28);
			this.ClientSize = new System.Drawing.Size(450, 463);
			this.ControlBox = false;
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.button_pause,
																		  this.label1,
																		  this.button_cancel,
																		  this.button_notime,
																		  this.button_action});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "UIF_POS_AutoTiming_01";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Запрос";
			this.ResumeLayout(false);

		}
		#endregion

		private void button_cancel_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		private void button_notime_Click(object sender, System.EventArgs e)
		{
			// Отметить незаезд
			if (DbSqlCardTime.SetNoTime(card_number, card_year) == false) return;
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void button_action_Click(object sender, System.EventArgs e)
		{
			// Действия постадийно
			if (stage == 1)
			{
				// Устанавливаем время заезда
				if (DbSqlCardTime.SetGoinTime(card_number, card_year) == false) return;
				this.DialogResult = DialogResult.OK;
				this.Close();
				return;
			}

			if (stage == 2)
			{
				// Устанавливаем время начала
				if (DbSqlCardTime.SetBeginTime(card_number, card_year) == false) return;
				this.DialogResult = DialogResult.OK;
				this.Close();
				return;
			}

			if (stage == 3)
			{
				// Устанавливаем время окончания
				if (DbSqlCardTime.SetEndTime(card_number, card_year) == false) return;
				this.DialogResult = DialogResult.OK;
				this.Close();
				return;
			}

			if (stage == 4)
			{
				// Устанавливаем время выезда
				if (DbSqlCardTime.SetGooutTime(card_number, card_year) == false) return;
				this.DialogResult = DialogResult.OK;
				this.Close();
				return;
			}
		}

		private void button_pause_Click(object sender, System.EventArgs e)
		{
			// Отметка остановки/возобновления ремонта
			if(pause_stage == 1)
			{
				UIF_POS_AutoTiming_02 dialog = new UIF_POS_AutoTiming_02(card_number, card_year);
				if(dialog.ShowDialog() != DialogResult.OK) return;
				this.DialogResult = DialogResult.OK;
				this.Close();
				return;
			}

			if(pause_stage == 2)
			{
				if(DbSqlCardTime.PauseSetEndTime(card_number, card_year) == false) return;
				this.DialogResult = DialogResult.OK;
				this.Close();
				return;
			}
		}
	}
}
