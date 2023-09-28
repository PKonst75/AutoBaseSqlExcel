using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for UIF_Payment.
	/// </summary>
	public class UIF_Payment : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBox_card;
		private System.Windows.Forms.Button button_pay;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBox_auto;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBox_partner;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBox_department;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textBox_workshop;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textBox_summ;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox textBox_comment;

		CS_Payment pay;

		public UIF_Payment(CS_Payment payment)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			textBox_card.Enabled		= false;
			textBox_auto.Enabled		= false;
			textBox_partner.Enabled		= false;
			textBox_workshop.Enabled	= false;
			textBox_department.Enabled	= false;

			pay	= payment;
			// Заполняем шаблон на основе платежа
			textBox_card.Text		= payment.str_warrant;
			textBox_auto.Text		= payment.str_auto;
			textBox_partner.Text	= payment.str_partner;
			textBox_workshop.Text	= payment.str_workshop;
			textBox_department.Text	= payment.str_department;

			textBox_summ.Text		= payment.summ.ToString();
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
			this.textBox_card = new System.Windows.Forms.TextBox();
			this.button_pay = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.textBox_auto = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textBox_partner = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textBox_department = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.textBox_workshop = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.textBox_summ = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.textBox_comment = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(72, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "Заказ-наряд";
			// 
			// textBox_card
			// 
			this.textBox_card.Location = new System.Drawing.Point(88, 16);
			this.textBox_card.Name = "textBox_card";
			this.textBox_card.Size = new System.Drawing.Size(288, 20);
			this.textBox_card.TabIndex = 1;
			this.textBox_card.Text = "";
			// 
			// button_pay
			// 
			this.button_pay.Location = new System.Drawing.Point(448, 248);
			this.button_pay.Name = "button_pay";
			this.button_pay.Size = new System.Drawing.Size(75, 64);
			this.button_pay.TabIndex = 2;
			this.button_pay.Text = "Оплатить";
			this.button_pay.Click += new System.EventHandler(this.button_pay_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(72, 23);
			this.label2.TabIndex = 3;
			this.label2.Text = "Автомобиль";
			// 
			// textBox_auto
			// 
			this.textBox_auto.Location = new System.Drawing.Point(88, 40);
			this.textBox_auto.Name = "textBox_auto";
			this.textBox_auto.Size = new System.Drawing.Size(288, 20);
			this.textBox_auto.TabIndex = 4;
			this.textBox_auto.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 64);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(72, 23);
			this.label3.TabIndex = 5;
			this.label3.Text = "Контрагент";
			// 
			// textBox_partner
			// 
			this.textBox_partner.Location = new System.Drawing.Point(88, 64);
			this.textBox_partner.Name = "textBox_partner";
			this.textBox_partner.Size = new System.Drawing.Size(288, 20);
			this.textBox_partner.TabIndex = 6;
			this.textBox_partner.Text = "";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(32, 160);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(56, 23);
			this.label4.TabIndex = 7;
			this.label4.Text = "КАССА";
			// 
			// textBox_department
			// 
			this.textBox_department.Location = new System.Drawing.Point(144, 152);
			this.textBox_department.Name = "textBox_department";
			this.textBox_department.Size = new System.Drawing.Size(272, 20);
			this.textBox_department.TabIndex = 8;
			this.textBox_department.Text = "";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(32, 128);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(112, 23);
			this.label5.TabIndex = 9;
			this.label5.Text = "ПОДРАЗДЕЛЕНИЕ";
			// 
			// textBox_workshop
			// 
			this.textBox_workshop.Location = new System.Drawing.Point(144, 128);
			this.textBox_workshop.Name = "textBox_workshop";
			this.textBox_workshop.Size = new System.Drawing.Size(272, 20);
			this.textBox_workshop.TabIndex = 10;
			this.textBox_workshop.Text = "";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(32, 192);
			this.label6.Name = "label6";
			this.label6.TabIndex = 11;
			this.label6.Text = "СУММА";
			// 
			// textBox_summ
			// 
			this.textBox_summ.Location = new System.Drawing.Point(144, 184);
			this.textBox_summ.Name = "textBox_summ";
			this.textBox_summ.Size = new System.Drawing.Size(272, 20);
			this.textBox_summ.TabIndex = 12;
			this.textBox_summ.Text = "";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(32, 224);
			this.label7.Name = "label7";
			this.label7.TabIndex = 13;
			this.label7.Text = "ПРИМЕЧАНИЕ";
			// 
			// textBox_comment
			// 
			this.textBox_comment.Location = new System.Drawing.Point(144, 216);
			this.textBox_comment.Name = "textBox_comment";
			this.textBox_comment.Size = new System.Drawing.Size(272, 20);
			this.textBox_comment.TabIndex = 14;
			this.textBox_comment.Text = "";
			// 
			// UIF_Payment
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(528, 319);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.textBox_comment,
																		  this.label7,
																		  this.textBox_summ,
																		  this.label6,
																		  this.textBox_workshop,
																		  this.label5,
																		  this.textBox_department,
																		  this.label4,
																		  this.textBox_partner,
																		  this.label3,
																		  this.textBox_auto,
																		  this.label2,
																		  this.button_pay,
																		  this.textBox_card,
																		  this.label1});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "UIF_Payment";
			this.Text = "Платеж";
			this.ResumeLayout(false);

		}
		#endregion

		private void button_pay_Click(object sender, System.EventArgs e)
		{
			// Получаем данные
			// Сумма платежа
			float summ = 0.0F;
			try
			{
				summ = (float)Convert.ToDouble(textBox_summ.Text);
			}
			catch{}
			// Примечание
			string comment = textBox_comment.Text;
			comment = comment.Trim();

			pay.summ		= summ;
			pay.comment		= comment;
			// Проверяем платеж
			pay.CheckElement();
			if(pay.CheckError() == false)
			{
				return;
			}
			// Совершаем платеж
			if(DbSqlPayment.Insert(pay) == null) return;
			MessageBox.Show("Добавили платеж");
			this.Close();
		}
	}
}
