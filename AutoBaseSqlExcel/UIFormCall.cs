using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for UIFormCall.
	/// </summary>
	public class UIFormCall : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button_ok;
		private System.Windows.Forms.Button button_cancel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.DateTimePicker dateTimePicker_date;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox checkBox_repeat;
		private System.Windows.Forms.ComboBox comboBox_type;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBox_fio;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textBox_contact;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textBox_mark;
		private System.Windows.Forms.TextBox textBox_essence;
		private System.Windows.Forms.CheckBox checkBox_credit;
		private System.Windows.Forms.CheckBox checkBox_tradein;
		private System.Windows.Forms.CheckBox checkBox_discount;
		private System.Windows.Forms.ComboBox comboBox_source;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.ComboBox comboBox_share;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox textBox_comment;
		private System.Windows.Forms.TextBox textBox_result;

		DtIncomingCall call;

		public UIFormCall()
		{
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			call = new DtIncomingCall();

			// Заполнение первоначальных данных
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
			this.button_ok = new System.Windows.Forms.Button();
			this.button_cancel = new System.Windows.Forms.Button();
			this.dateTimePicker_date = new System.Windows.Forms.DateTimePicker();
			this.label2 = new System.Windows.Forms.Label();
			this.checkBox_repeat = new System.Windows.Forms.CheckBox();
			this.comboBox_type = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.textBox_fio = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.textBox_contact = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.textBox_mark = new System.Windows.Forms.TextBox();
			this.textBox_essence = new System.Windows.Forms.TextBox();
			this.checkBox_credit = new System.Windows.Forms.CheckBox();
			this.checkBox_tradein = new System.Windows.Forms.CheckBox();
			this.checkBox_discount = new System.Windows.Forms.CheckBox();
			this.comboBox_source = new System.Windows.Forms.ComboBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.comboBox_share = new System.Windows.Forms.ComboBox();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.textBox_comment = new System.Windows.Forms.TextBox();
			this.textBox_result = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// button_ok
			// 
			this.button_ok.Location = new System.Drawing.Point(552, 280);
			this.button_ok.Name = "button_ok";
			this.button_ok.TabIndex = 2;
			this.button_ok.Text = "ОК";
			this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
			// 
			// button_cancel
			// 
			this.button_cancel.Location = new System.Drawing.Point(472, 280);
			this.button_cancel.Name = "button_cancel";
			this.button_cancel.TabIndex = 3;
			this.button_cancel.Text = "Отмена";
			this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
			// 
			// dateTimePicker_date
			// 
			this.dateTimePicker_date.Location = new System.Drawing.Point(72, 8);
			this.dateTimePicker_date.Name = "dateTimePicker_date";
			this.dateTimePicker_date.Size = new System.Drawing.Size(128, 20);
			this.dateTimePicker_date.TabIndex = 7;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 8);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(56, 23);
			this.label2.TabIndex = 8;
			this.label2.Text = "Дата";
			// 
			// checkBox_repeat
			// 
			this.checkBox_repeat.Location = new System.Drawing.Point(216, 8);
			this.checkBox_repeat.Name = "checkBox_repeat";
			this.checkBox_repeat.Size = new System.Drawing.Size(96, 24);
			this.checkBox_repeat.TabIndex = 9;
			this.checkBox_repeat.Text = "Повторный";
			// 
			// comboBox_type
			// 
			this.comboBox_type.Location = new System.Drawing.Point(488, 8);
			this.comboBox_type.Name = "comboBox_type";
			this.comboBox_type.Size = new System.Drawing.Size(136, 21);
			this.comboBox_type.TabIndex = 10;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(384, 8);
			this.label1.Name = "label1";
			this.label1.TabIndex = 11;
			this.label1.Text = "Вид контакта";
			// 
			// textBox_fio
			// 
			this.textBox_fio.Location = new System.Drawing.Point(104, 96);
			this.textBox_fio.Name = "textBox_fio";
			this.textBox_fio.Size = new System.Drawing.Size(520, 20);
			this.textBox_fio.TabIndex = 12;
			this.textBox_fio.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(0, 96);
			this.label3.Name = "label3";
			this.label3.TabIndex = 13;
			this.label3.Text = "ФИО";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(0, 128);
			this.label4.Name = "label4";
			this.label4.TabIndex = 14;
			this.label4.Text = "Контакт";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(0, 152);
			this.label5.Name = "label5";
			this.label5.TabIndex = 15;
			this.label5.Text = "Зацепка";
			// 
			// textBox_contact
			// 
			this.textBox_contact.Location = new System.Drawing.Point(104, 120);
			this.textBox_contact.Name = "textBox_contact";
			this.textBox_contact.Size = new System.Drawing.Size(520, 20);
			this.textBox_contact.TabIndex = 16;
			this.textBox_contact.Text = "";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(0, 176);
			this.label6.Name = "label6";
			this.label6.TabIndex = 17;
			this.label6.Text = "Суть";
			// 
			// textBox_mark
			// 
			this.textBox_mark.Location = new System.Drawing.Point(104, 144);
			this.textBox_mark.Name = "textBox_mark";
			this.textBox_mark.Size = new System.Drawing.Size(520, 20);
			this.textBox_mark.TabIndex = 18;
			this.textBox_mark.Text = "";
			// 
			// textBox_essence
			// 
			this.textBox_essence.Location = new System.Drawing.Point(104, 168);
			this.textBox_essence.Name = "textBox_essence";
			this.textBox_essence.Size = new System.Drawing.Size(520, 20);
			this.textBox_essence.TabIndex = 19;
			this.textBox_essence.Text = "";
			// 
			// checkBox_credit
			// 
			this.checkBox_credit.Location = new System.Drawing.Point(8, 40);
			this.checkBox_credit.Name = "checkBox_credit";
			this.checkBox_credit.TabIndex = 20;
			this.checkBox_credit.Text = "Кредит";
			// 
			// checkBox_tradein
			// 
			this.checkBox_tradein.Location = new System.Drawing.Point(8, 64);
			this.checkBox_tradein.Name = "checkBox_tradein";
			this.checkBox_tradein.TabIndex = 21;
			this.checkBox_tradein.Text = "Трейдин";
			// 
			// checkBox_discount
			// 
			this.checkBox_discount.Location = new System.Drawing.Point(112, 40);
			this.checkBox_discount.Name = "checkBox_discount";
			this.checkBox_discount.TabIndex = 22;
			this.checkBox_discount.Text = "Бонус";
			// 
			// comboBox_source
			// 
			this.comboBox_source.Location = new System.Drawing.Point(488, 32);
			this.comboBox_source.Name = "comboBox_source";
			this.comboBox_source.Size = new System.Drawing.Size(136, 21);
			this.comboBox_source.TabIndex = 23;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(336, 32);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(136, 23);
			this.label7.TabIndex = 24;
			this.label7.Text = "Источник информации";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(376, 64);
			this.label8.Name = "label8";
			this.label8.TabIndex = 25;
			this.label8.Text = "Акция";
			// 
			// comboBox_share
			// 
			this.comboBox_share.Location = new System.Drawing.Point(488, 56);
			this.comboBox_share.Name = "comboBox_share";
			this.comboBox_share.Size = new System.Drawing.Size(136, 21);
			this.comboBox_share.TabIndex = 26;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(0, 208);
			this.label9.Name = "label9";
			this.label9.TabIndex = 27;
			this.label9.Text = "Комментарий";
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(0, 232);
			this.label10.Name = "label10";
			this.label10.TabIndex = 28;
			this.label10.Text = "Результат";
			// 
			// textBox_comment
			// 
			this.textBox_comment.Location = new System.Drawing.Point(104, 208);
			this.textBox_comment.Name = "textBox_comment";
			this.textBox_comment.Size = new System.Drawing.Size(520, 20);
			this.textBox_comment.TabIndex = 29;
			this.textBox_comment.Text = "";
			// 
			// textBox_result
			// 
			this.textBox_result.Location = new System.Drawing.Point(104, 232);
			this.textBox_result.Name = "textBox_result";
			this.textBox_result.Size = new System.Drawing.Size(520, 20);
			this.textBox_result.TabIndex = 30;
			this.textBox_result.Text = "";
			// 
			// UIFormCall
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(640, 315);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.textBox_result,
																		  this.textBox_comment,
																		  this.label10,
																		  this.label9,
																		  this.comboBox_share,
																		  this.label8,
																		  this.label7,
																		  this.comboBox_source,
																		  this.checkBox_discount,
																		  this.checkBox_tradein,
																		  this.checkBox_credit,
																		  this.textBox_essence,
																		  this.textBox_mark,
																		  this.label6,
																		  this.textBox_contact,
																		  this.label5,
																		  this.label4,
																		  this.label3,
																		  this.textBox_fio,
																		  this.label1,
																		  this.comboBox_type,
																		  this.checkBox_repeat,
																		  this.label2,
																		  this.dateTimePicker_date,
																		  this.button_cancel,
																		  this.button_ok});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Name = "UIFormCall";
			this.Text = "Входящий контакт";
			this.ResumeLayout(false);

		}
		#endregion

		private void button_cancel_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}


		private void button_ok_Click(object sender, System.EventArgs e)
		{
		
		}
	}
}
