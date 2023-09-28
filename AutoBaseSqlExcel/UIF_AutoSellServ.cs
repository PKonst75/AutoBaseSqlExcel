using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for UIF_AutoSellServ.
	/// </summary>
	public class UIF_AutoSellServ : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox textBox_manager;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button button_select_manager;
		private System.Windows.Forms.CheckBox checkBox_music;
		private System.Windows.Forms.CheckBox checkBox_alarm;
		private System.Windows.Forms.CheckBox checkBox_tune;
		private System.Windows.Forms.CheckBox checkBox_other;
		private System.Windows.Forms.CheckBox checkBox_anti;
		private System.Windows.Forms.CheckBox checkBox_gibdd;
		private System.Windows.Forms.CheckBox checkBox_kasko;
		private System.Windows.Forms.CheckBox checkBox_osago;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBox_summ_whole;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBox_auto_summ;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBox_auto_discount_money;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textBox_auto_discount_other;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textBox_summ_anti;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox textBox_auto_discount_anti;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox textBox_auto_discount_tunemus;
		private System.Windows.Forms.CheckBox checkBox_anti1;
		private System.Windows.Forms.CheckBox checkBox_anti2;
		private System.Windows.Forms.CheckBox checkBox_sprav;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox textBox_summ_sprav;

		private DtAutoSellServ sell_serv = null;

		public UIF_AutoSellServ(long code)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			DtAutoSellServ ss = DbSqlAutoSellServ.Find(code);
			if(ss == null)
			{
				sell_serv = new DtAutoSellServ();
				sell_serv.code_sell = code;
			}
			else
			{
				sell_serv = new DtAutoSellServ(ss);
				// Установка данных
				checkBox_music.Checked = sell_serv.flag_music;
				checkBox_alarm.Checked = sell_serv.flag_alarm;
				checkBox_tune.Checked = sell_serv.flag_tune;
				checkBox_anti.Checked = sell_serv.flag_anti;
				checkBox_anti1.Checked = sell_serv.flag_anti1;
				checkBox_anti2.Checked = sell_serv.flag_anti2;
				checkBox_other.Checked = sell_serv.flag_other;
				checkBox_gibdd.Checked = sell_serv.flag_gibdd;
				checkBox_sprav.Checked = sell_serv.flag_sprav;
				checkBox_kasko.Checked = sell_serv.flag_kasko;
				checkBox_osago.Checked = sell_serv.flag_osago;

				textBox_summ_whole.Text = Db.FloatToTxt(sell_serv.summ_whole);
				textBox_summ_anti.Text = Db.FloatToTxt(sell_serv.summ_anti);
				textBox_summ_sprav.Text = Db.FloatToTxt(sell_serv.summ_sprav);
				textBox_auto_summ.Text = Db.FloatToTxt(sell_serv.auto_summ);
				textBox_auto_discount_money.Text = Db.FloatToTxt(sell_serv.auto_discount_money);
				textBox_auto_discount_other.Text = Db.FloatToTxt(sell_serv.auto_discount_other);
				textBox_auto_discount_anti.Text = Db.FloatToTxt(sell_serv.auto_discount_anti);
				textBox_auto_discount_tunemus.Text = Db.FloatToTxt(sell_serv.auto_discount_tunemus);

				DtStaff staff = DbSqlStaff.Find(sell_serv.code_manager);
				string text = "ОШИБКА";
				if(staff != null)
					text = staff.Title;
				textBox_manager.Text = text;
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
			this.button1 = new System.Windows.Forms.Button();
			this.textBox_manager = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.button_select_manager = new System.Windows.Forms.Button();
			this.checkBox_music = new System.Windows.Forms.CheckBox();
			this.checkBox_alarm = new System.Windows.Forms.CheckBox();
			this.checkBox_tune = new System.Windows.Forms.CheckBox();
			this.checkBox_other = new System.Windows.Forms.CheckBox();
			this.checkBox_anti = new System.Windows.Forms.CheckBox();
			this.checkBox_gibdd = new System.Windows.Forms.CheckBox();
			this.checkBox_kasko = new System.Windows.Forms.CheckBox();
			this.checkBox_osago = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textBox_summ_whole = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textBox_auto_summ = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textBox_auto_discount_money = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.textBox_auto_discount_other = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.textBox_summ_anti = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.textBox_auto_discount_anti = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.textBox_auto_discount_tunemus = new System.Windows.Forms.TextBox();
			this.checkBox_anti1 = new System.Windows.Forms.CheckBox();
			this.checkBox_anti2 = new System.Windows.Forms.CheckBox();
			this.checkBox_sprav = new System.Windows.Forms.CheckBox();
			this.textBox_summ_sprav = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(512, 280);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(80, 23);
			this.button1.TabIndex = 0;
			this.button1.Text = "Сохранить";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// textBox_manager
			// 
			this.textBox_manager.Location = new System.Drawing.Point(112, 24);
			this.textBox_manager.Name = "textBox_manager";
			this.textBox_manager.Size = new System.Drawing.Size(256, 22);
			this.textBox_manager.TabIndex = 1;
			this.textBox_manager.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 24);
			this.label1.Name = "label1";
			this.label1.TabIndex = 2;
			this.label1.Text = "Менеджер";
			// 
			// button_select_manager
			// 
			this.button_select_manager.Location = new System.Drawing.Point(376, 24);
			this.button_select_manager.Name = "button_select_manager";
			this.button_select_manager.Size = new System.Drawing.Size(24, 23);
			this.button_select_manager.TabIndex = 3;
			this.button_select_manager.Text = "...";
			this.button_select_manager.Click += new System.EventHandler(this.button_select_manager_Click);
			// 
			// checkBox_music
			// 
			this.checkBox_music.Location = new System.Drawing.Point(24, 64);
			this.checkBox_music.Name = "checkBox_music";
			this.checkBox_music.TabIndex = 4;
			this.checkBox_music.Text = "Музыка";
			// 
			// checkBox_alarm
			// 
			this.checkBox_alarm.Location = new System.Drawing.Point(24, 88);
			this.checkBox_alarm.Name = "checkBox_alarm";
			this.checkBox_alarm.Size = new System.Drawing.Size(144, 24);
			this.checkBox_alarm.TabIndex = 5;
			this.checkBox_alarm.Text = "Сигнализация";
			// 
			// checkBox_tune
			// 
			this.checkBox_tune.Location = new System.Drawing.Point(24, 112);
			this.checkBox_tune.Name = "checkBox_tune";
			this.checkBox_tune.TabIndex = 6;
			this.checkBox_tune.Text = "Тюнинг";
			// 
			// checkBox_other
			// 
			this.checkBox_other.Location = new System.Drawing.Point(24, 136);
			this.checkBox_other.Name = "checkBox_other";
			this.checkBox_other.TabIndex = 7;
			this.checkBox_other.Text = "Аксессуары";
			// 
			// checkBox_anti
			// 
			this.checkBox_anti.Location = new System.Drawing.Point(184, 64);
			this.checkBox_anti.Name = "checkBox_anti";
			this.checkBox_anti.TabIndex = 8;
			this.checkBox_anti.Text = "Антикор";
			// 
			// checkBox_gibdd
			// 
			this.checkBox_gibdd.Location = new System.Drawing.Point(304, 64);
			this.checkBox_gibdd.Name = "checkBox_gibdd";
			this.checkBox_gibdd.TabIndex = 9;
			this.checkBox_gibdd.Text = "ГИБДД";
			// 
			// checkBox_kasko
			// 
			this.checkBox_kasko.Location = new System.Drawing.Point(464, 64);
			this.checkBox_kasko.Name = "checkBox_kasko";
			this.checkBox_kasko.TabIndex = 10;
			this.checkBox_kasko.Text = "КАСКО";
			// 
			// checkBox_osago
			// 
			this.checkBox_osago.Location = new System.Drawing.Point(464, 88);
			this.checkBox_osago.Name = "checkBox_osago";
			this.checkBox_osago.TabIndex = 11;
			this.checkBox_osago.Text = "ОСАГО";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(24, 168);
			this.label2.Name = "label2";
			this.label2.TabIndex = 12;
			this.label2.Text = "Сумма допов";
			// 
			// textBox_summ_whole
			// 
			this.textBox_summ_whole.Location = new System.Drawing.Point(192, 168);
			this.textBox_summ_whole.Name = "textBox_summ_whole";
			this.textBox_summ_whole.TabIndex = 13;
			this.textBox_summ_whole.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(24, 216);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(136, 23);
			this.label3.TabIndex = 14;
			this.label3.Text = "Цена автомобиля";
			// 
			// textBox_auto_summ
			// 
			this.textBox_auto_summ.Location = new System.Drawing.Point(192, 216);
			this.textBox_auto_summ.Name = "textBox_auto_summ";
			this.textBox_auto_summ.TabIndex = 15;
			this.textBox_auto_summ.Text = "";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(304, 216);
			this.label4.Name = "label4";
			this.label4.TabIndex = 16;
			this.label4.Text = "Скидка деньги";
			// 
			// textBox_auto_discount_money
			// 
			this.textBox_auto_discount_money.Location = new System.Drawing.Point(424, 216);
			this.textBox_auto_discount_money.Name = "textBox_auto_discount_money";
			this.textBox_auto_discount_money.TabIndex = 17;
			this.textBox_auto_discount_money.Text = "";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(24, 240);
			this.label5.Name = "label5";
			this.label5.TabIndex = 18;
			this.label5.Text = "Подарок";
			// 
			// textBox_auto_discount_other
			// 
			this.textBox_auto_discount_other.Location = new System.Drawing.Point(192, 240);
			this.textBox_auto_discount_other.Name = "textBox_auto_discount_other";
			this.textBox_auto_discount_other.TabIndex = 19;
			this.textBox_auto_discount_other.Text = "";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(24, 192);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(120, 23);
			this.label6.TabIndex = 20;
			this.label6.Text = "Сумма антикор";
			// 
			// textBox_summ_anti
			// 
			this.textBox_summ_anti.Location = new System.Drawing.Point(192, 192);
			this.textBox_summ_anti.Name = "textBox_summ_anti";
			this.textBox_summ_anti.TabIndex = 21;
			this.textBox_summ_anti.Text = "";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(304, 192);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(120, 23);
			this.label7.TabIndex = 22;
			this.label7.Text = "Скидка антикор";
			// 
			// textBox_auto_discount_anti
			// 
			this.textBox_auto_discount_anti.Location = new System.Drawing.Point(424, 192);
			this.textBox_auto_discount_anti.Name = "textBox_auto_discount_anti";
			this.textBox_auto_discount_anti.TabIndex = 23;
			this.textBox_auto_discount_anti.Text = "";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(304, 168);
			this.label8.Name = "label8";
			this.label8.TabIndex = 24;
			this.label8.Text = "Скидка допы";
			// 
			// textBox_auto_discount_tunemus
			// 
			this.textBox_auto_discount_tunemus.Location = new System.Drawing.Point(424, 168);
			this.textBox_auto_discount_tunemus.Name = "textBox_auto_discount_tunemus";
			this.textBox_auto_discount_tunemus.TabIndex = 25;
			this.textBox_auto_discount_tunemus.Text = "";
			// 
			// checkBox_anti1
			// 
			this.checkBox_anti1.Location = new System.Drawing.Point(184, 88);
			this.checkBox_anti1.Name = "checkBox_anti1";
			this.checkBox_anti1.TabIndex = 26;
			this.checkBox_anti1.Text = "Подкрылки";
			// 
			// checkBox_anti2
			// 
			this.checkBox_anti2.Location = new System.Drawing.Point(184, 112);
			this.checkBox_anti2.Name = "checkBox_anti2";
			this.checkBox_anti2.TabIndex = 27;
			this.checkBox_anti2.Text = "Защита";
			// 
			// checkBox_sprav
			// 
			this.checkBox_sprav.Location = new System.Drawing.Point(304, 88);
			this.checkBox_sprav.Name = "checkBox_sprav";
			this.checkBox_sprav.Size = new System.Drawing.Size(120, 24);
			this.checkBox_sprav.TabIndex = 28;
			this.checkBox_sprav.Text = "ДКП";
			// 
			// textBox_summ_sprav
			// 
			this.textBox_summ_sprav.Location = new System.Drawing.Point(192, 264);
			this.textBox_summ_sprav.Name = "textBox_summ_sprav";
			this.textBox_summ_sprav.TabIndex = 29;
			this.textBox_summ_sprav.Text = "";
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(24, 264);
			this.label9.Name = "label9";
			this.label9.TabIndex = 30;
			this.label9.Text = "ДКП";
			// 
			// UIF_AutoSellServ
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
			this.ClientSize = new System.Drawing.Size(600, 309);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.label9,
																		  this.textBox_summ_sprav,
																		  this.checkBox_sprav,
																		  this.checkBox_anti2,
																		  this.checkBox_anti1,
																		  this.textBox_auto_discount_tunemus,
																		  this.label8,
																		  this.textBox_auto_discount_anti,
																		  this.label7,
																		  this.textBox_summ_anti,
																		  this.label6,
																		  this.textBox_auto_discount_other,
																		  this.label5,
																		  this.textBox_auto_discount_money,
																		  this.label4,
																		  this.textBox_auto_summ,
																		  this.label3,
																		  this.textBox_summ_whole,
																		  this.label2,
																		  this.checkBox_osago,
																		  this.checkBox_kasko,
																		  this.checkBox_gibdd,
																		  this.checkBox_anti,
																		  this.checkBox_other,
																		  this.checkBox_tune,
																		  this.checkBox_alarm,
																		  this.checkBox_music,
																		  this.button_select_manager,
																		  this.label1,
																		  this.textBox_manager,
																		  this.button1});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "UIF_AutoSellServ";
			this.Text = "Служебная информация по продаже";
			this.ResumeLayout(false);

		}
		#endregion

		private void button_select_manager_Click(object sender, System.EventArgs e)
		{
			// Выбор менеджера продавшего автомобиль
			FormStaffList dialog = new FormStaffList();
			if (dialog.ShowDialog() != DialogResult.OK) return;
			textBox_manager.Text = dialog.SelectedStaff.Title;
			sell_serv.code_manager = dialog.SelectedStaff.Code;
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			// Применяе сделанные изменения

			// Допы
			if(checkBox_music.Checked == true)
				sell_serv.flag_music = true;
			else
				sell_serv.flag_music = false;
			if(checkBox_alarm.Checked == true)
				sell_serv.flag_alarm = true;
			else
				sell_serv.flag_alarm = false;
			if(checkBox_tune.Checked == true)
				sell_serv.flag_tune = true;
			else
				sell_serv.flag_tune = false;
			if(checkBox_anti.Checked == true)
				sell_serv.flag_anti = true;
			else
				sell_serv.flag_anti = false;
			if(checkBox_anti1.Checked == true)
				sell_serv.flag_anti1 = true;
			else
				sell_serv.flag_anti1 = false;
			if(checkBox_anti2.Checked == true)
				sell_serv.flag_anti2 = true;
			else
				sell_serv.flag_anti2 = false;
			if(checkBox_other.Checked == true)
				sell_serv.flag_other = true;
			else
				sell_serv.flag_other = false;
			if(checkBox_gibdd.Checked == true)
				sell_serv.flag_gibdd = true;
			else
				sell_serv.flag_gibdd = false;
			if(checkBox_sprav.Checked == true)
				sell_serv.flag_sprav = true;
			else
				sell_serv.flag_sprav = false;
			if(checkBox_kasko.Checked == true)
				sell_serv.flag_kasko = true;
			else
				sell_serv.flag_kasko = false;
			if(checkBox_osago.Checked == true)
				sell_serv.flag_osago = true;
			else
				sell_serv.flag_osago = false;

			// Суммы
			sell_serv.summ_whole = (float)Convert.ToDouble(textBox_summ_whole.Text);
			sell_serv.summ_anti = (float)Convert.ToDouble(textBox_summ_anti.Text);
			sell_serv.summ_sprav = (float)Convert.ToDouble(textBox_summ_sprav.Text);
			sell_serv.auto_summ = (float)Convert.ToDouble(textBox_auto_summ.Text);
			sell_serv.auto_discount_money = (float)Convert.ToDouble(textBox_auto_discount_money.Text);
			sell_serv.auto_discount_other = (float)Convert.ToDouble(textBox_auto_discount_other.Text);
			sell_serv.auto_discount_anti = (float)Convert.ToDouble(textBox_auto_discount_anti.Text);
			sell_serv.auto_discount_tunemus = (float)Convert.ToDouble(textBox_auto_discount_tunemus.Text);

			// Пытаемся записать
			if(DbSqlAutoSellServ.Insert(sell_serv) == true) this.Close();
		}
	}
}
