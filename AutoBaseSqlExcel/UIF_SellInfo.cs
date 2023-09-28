using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for UIF_SellInfo.
	/// </summary>
	public class UIF_SellInfo : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ComboBox comboBox_position;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox checkBox_credit_inner;
		private System.Windows.Forms.CheckBox checkBox_credit_outer;
		private System.Windows.Forms.CheckBox checkBox_lising;
		private System.Windows.Forms.CheckBox checkBox_cashless;
		private System.Windows.Forms.ComboBox comboBox_reklama;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button button_save;
		private System.Windows.Forms.CheckBox checkBox_partner;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.CheckBox checkBox_util;
        private CheckBox checkBox_tin;
        private TextBox textBox_tinprice;
        private Label label3;

		CS_SellInfo info = null;

		public UIF_SellInfo(long code_sell)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			// Первоначальная настройка компонентов
			comboBox_position.Items.Add("Не выбрано");
			comboBox_position.Items.Add("Местный (Новосибирск, Бердск)");
			comboBox_position.Items.Add("Область (Новосибирская область)");
			comboBox_position.Items.Add("Иногородний");

			comboBox_reklama.Items.Add("Не выбрано");
			comboBox_reklama.Items.Add("Приобретали автомобиль");
			comboBox_reklama.Items.Add("Знкомые, Родственники");
			comboBox_reklama.Items.Add("Радио");
			comboBox_reklama.Items.Add("Телевидение");
			comboBox_reklama.Items.Add("Наружняя реклама");
			comboBox_reklama.Items.Add("Газеты");
			comboBox_reklama.Items.Add("Журналы");
			comboBox_reklama.Items.Add("Интернет");
			comboBox_reklama.Items.Add("Дубль-ГИС");

			// Поиск существующей информации
			info = DbSqlSellInfo.Find(code_sell);
			if(info == null) info = new CS_SellInfo(code_sell);
			// Настройка отображения
			string txt = "";
			comboBox_position.SelectedIndex = (int)info.code_position;
			txt = comboBox_position.GetItemText(comboBox_position.Items[comboBox_position.SelectedIndex]);
			comboBox_position.Text = txt;

			txt = "";
			comboBox_reklama.SelectedIndex = (int)info.code_reklama;
			txt = comboBox_reklama.GetItemText(comboBox_reklama.Items[comboBox_reklama.SelectedIndex]);
			comboBox_reklama.Text = txt;

			if(info.flag_credit_inner == true) checkBox_credit_inner.Checked = true;
			if(info.flag_credit_outer == true) checkBox_credit_outer.Checked = true;
			if(info.flag_lising == true) checkBox_lising.Checked = true;
			if(info.flag_cashless == true) checkBox_cashless.Checked = true;
			if(info.flag_partner == true) checkBox_partner.Checked = true;
			if(info.flag_util == true) checkBox_util.Checked = true;

            if (info.flag_tin == true) checkBox_tin.Checked = true;
            textBox_tinprice.Text = info.tinprice.ToString();
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
            this.comboBox_position = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox_credit_inner = new System.Windows.Forms.CheckBox();
            this.checkBox_credit_outer = new System.Windows.Forms.CheckBox();
            this.checkBox_lising = new System.Windows.Forms.CheckBox();
            this.checkBox_cashless = new System.Windows.Forms.CheckBox();
            this.checkBox_partner = new System.Windows.Forms.CheckBox();
            this.comboBox_reklama = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button_save = new System.Windows.Forms.Button();
            this.checkBox_util = new System.Windows.Forms.CheckBox();
            this.checkBox_tin = new System.Windows.Forms.CheckBox();
            this.textBox_tinprice = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboBox_position
            // 
            this.comboBox_position.Location = new System.Drawing.Point(16, 40);
            this.comboBox_position.Name = "comboBox_position";
            this.comboBox_position.Size = new System.Drawing.Size(256, 21);
            this.comboBox_position.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(16, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "Местоположение";
            // 
            // checkBox_credit_inner
            // 
            this.checkBox_credit_inner.Location = new System.Drawing.Point(320, 8);
            this.checkBox_credit_inner.Name = "checkBox_credit_inner";
            this.checkBox_credit_inner.Size = new System.Drawing.Size(160, 24);
            this.checkBox_credit_inner.TabIndex = 2;
            this.checkBox_credit_inner.Text = "Кредит - В автосалоне";
            // 
            // checkBox_credit_outer
            // 
            this.checkBox_credit_outer.Location = new System.Drawing.Point(320, 32);
            this.checkBox_credit_outer.Name = "checkBox_credit_outer";
            this.checkBox_credit_outer.Size = new System.Drawing.Size(136, 24);
            this.checkBox_credit_outer.TabIndex = 3;
            this.checkBox_credit_outer.Text = "Кредит - Внешний";
            // 
            // checkBox_lising
            // 
            this.checkBox_lising.Location = new System.Drawing.Point(320, 56);
            this.checkBox_lising.Name = "checkBox_lising";
            this.checkBox_lising.Size = new System.Drawing.Size(104, 24);
            this.checkBox_lising.TabIndex = 4;
            this.checkBox_lising.Text = "Лизинг";
            // 
            // checkBox_cashless
            // 
            this.checkBox_cashless.Location = new System.Drawing.Point(320, 112);
            this.checkBox_cashless.Name = "checkBox_cashless";
            this.checkBox_cashless.Size = new System.Drawing.Size(152, 24);
            this.checkBox_cashless.TabIndex = 5;
            this.checkBox_cashless.Text = "Оплата безналом";
            // 
            // checkBox_partner
            // 
            this.checkBox_partner.Location = new System.Drawing.Point(320, 136);
            this.checkBox_partner.Name = "checkBox_partner";
            this.checkBox_partner.Size = new System.Drawing.Size(144, 24);
            this.checkBox_partner.TabIndex = 6;
            this.checkBox_partner.Text = "Партерская продажа";
            // 
            // comboBox_reklama
            // 
            this.comboBox_reklama.Location = new System.Drawing.Point(16, 104);
            this.comboBox_reklama.Name = "comboBox_reklama";
            this.comboBox_reklama.Size = new System.Drawing.Size(256, 21);
            this.comboBox_reklama.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(16, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 23);
            this.label2.TabIndex = 8;
            this.label2.Text = "Рекламный источник";
            // 
            // button_save
            // 
            this.button_save.Location = new System.Drawing.Point(520, 327);
            this.button_save.Name = "button_save";
            this.button_save.Size = new System.Drawing.Size(75, 23);
            this.button_save.TabIndex = 9;
            this.button_save.Text = "Сохранить";
            this.button_save.Click += new System.EventHandler(this.button_save_Click);
            // 
            // checkBox_util
            // 
            this.checkBox_util.Location = new System.Drawing.Point(320, 184);
            this.checkBox_util.Name = "checkBox_util";
            this.checkBox_util.Size = new System.Drawing.Size(136, 24);
            this.checkBox_util.TabIndex = 10;
            this.checkBox_util.Text = "Утилизация";
            // 
            // checkBox_tin
            // 
            this.checkBox_tin.AutoSize = true;
            this.checkBox_tin.Location = new System.Drawing.Point(320, 214);
            this.checkBox_tin.Name = "checkBox_tin";
            this.checkBox_tin.Size = new System.Drawing.Size(76, 17);
            this.checkBox_tin.TabIndex = 11;
            this.checkBox_tin.Text = "Трейд-ИН";
            this.checkBox_tin.UseVisualStyleBackColor = true;
            // 
            // textBox_tinprice
            // 
            this.textBox_tinprice.Location = new System.Drawing.Point(411, 214);
            this.textBox_tinprice.Name = "textBox_tinprice";
            this.textBox_tinprice.Size = new System.Drawing.Size(184, 20);
            this.textBox_tinprice.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(414, 237);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(177, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Стоимость Трейд-Ин автомобиля";
            // 
            // UIF_SellInfo
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(607, 362);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_tinprice);
            this.Controls.Add(this.checkBox_tin);
            this.Controls.Add(this.checkBox_util);
            this.Controls.Add(this.button_save);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBox_reklama);
            this.Controls.Add(this.checkBox_partner);
            this.Controls.Add(this.checkBox_cashless);
            this.Controls.Add(this.checkBox_lising);
            this.Controls.Add(this.checkBox_credit_outer);
            this.Controls.Add(this.checkBox_credit_inner);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox_position);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "UIF_SellInfo";
            this.Text = "Дополнительная информация о продаже";
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void button_save_Click(object sender, System.EventArgs e)
		{
			// Сохраняем настроенные данные о продаже
			info.code_position = (long)comboBox_position.SelectedIndex;
			info.code_reklama = (long)comboBox_reklama.SelectedIndex;
			
			if(checkBox_credit_inner.Checked == true)
				info.flag_credit_inner = true;
			else
				info.flag_credit_inner = false;
			if(checkBox_credit_outer.Checked == true)
				info.flag_credit_outer = true;
			else
				info.flag_credit_outer = false;
			if(checkBox_lising.Checked == true)
				info.flag_lising = true;
			else
				info.flag_lising = false;
			if(checkBox_cashless.Checked == true)
				info.flag_cashless  = true;
			else
				info.flag_cashless  = false;
			if(checkBox_partner.Checked == true)
				info.flag_partner = true;
			else
				info.flag_partner = false;
			if(checkBox_util.Checked == true)
				info.flag_util= true;
			else
				info.flag_util = false;

            if (checkBox_tin.Checked == true)
                info.flag_tin = true;
            else
                info.flag_tin = false;

            info.tinprice = (long)Convert.ToInt64(textBox_tinprice.Text); ;

			if(DbSqlSellInfo.Insert(info) == true) this.Close();
		}
	}
}
