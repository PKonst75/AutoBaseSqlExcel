using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormManageDiscount.
	/// </summary>
	public class FormManageDiscount : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBox_summwork;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBox_discountpercent;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBox_discountsumm;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textBox_summpaydetail;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textBox_summpay;
		private System.Windows.Forms.Button button_cancel;
		private System.Windows.Forms.TextBox textBox_summpaywork;
		private System.Windows.Forms.Button button_calc;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		float summwork;
		float summworkpay;
		float summdetailpay;
		float summpay;
		public float discountpercent;
		private System.Windows.Forms.Button button1;
		DtCard card;

		public FormManageDiscount(DtCard cardsrc)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			// Расчет первичных параметров диалога оформления скидки
			card = cardsrc;
			discountpercent = 0.0F;
			summwork = card.SummWorkPayNoDiscount();
			summworkpay = card.SummWorkPayNoDiscount() + card.SummDetailOilPay();
			summdetailpay = card.SummDetailPay();
			summpay = summworkpay + summdetailpay;

			textBox_summwork.Text = summwork.ToString();
			textBox_summpaywork.Text = summworkpay.ToString();
			textBox_summpaydetail.Text = summdetailpay.ToString();
			textBox_summpay.Text = summpay.ToString();

			textBox_discountpercent.Text = "0";
			textBox_discountsumm.Text = "0";
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
			this.textBox_summwork = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textBox_discountpercent = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textBox_discountsumm = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textBox_summpaywork = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.textBox_summpaydetail = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.textBox_summpay = new System.Windows.Forms.TextBox();
			this.button_cancel = new System.Windows.Forms.Button();
			this.button_calc = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(248, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "Сумма работ по заказ-няряду";
			// 
			// textBox_summwork
			// 
			this.textBox_summwork.Location = new System.Drawing.Point(256, 16);
			this.textBox_summwork.Name = "textBox_summwork";
			this.textBox_summwork.ReadOnly = true;
			this.textBox_summwork.Size = new System.Drawing.Size(120, 26);
			this.textBox_summwork.TabIndex = 1;
			this.textBox_summwork.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 64);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(144, 23);
			this.label2.TabIndex = 2;
			this.label2.Text = "Процент скидки";
			// 
			// textBox_discountpercent
			// 
			this.textBox_discountpercent.Location = new System.Drawing.Point(256, 56);
			this.textBox_discountpercent.Name = "textBox_discountpercent";
			this.textBox_discountpercent.Size = new System.Drawing.Size(120, 26);
			this.textBox_discountpercent.TabIndex = 3;
			this.textBox_discountpercent.Text = "";
			this.textBox_discountpercent.TextChanged += new System.EventHandler(this.textBox_discountpercent_TextChanged);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(408, 64);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(120, 24);
			this.label3.TabIndex = 4;
			this.label3.Text = "Сумма скидки";
			// 
			// textBox_discountsumm
			// 
			this.textBox_discountsumm.Location = new System.Drawing.Point(528, 56);
			this.textBox_discountsumm.Name = "textBox_discountsumm";
			this.textBox_discountsumm.ReadOnly = true;
			this.textBox_discountsumm.Size = new System.Drawing.Size(112, 26);
			this.textBox_discountsumm.TabIndex = 5;
			this.textBox_discountsumm.Text = "";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 128);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(200, 24);
			this.label4.TabIndex = 6;
			this.label4.Text = "Сумма оплаты (работы)";
			// 
			// textBox_summpaywork
			// 
			this.textBox_summpaywork.Location = new System.Drawing.Point(256, 128);
			this.textBox_summpaywork.Name = "textBox_summpaywork";
			this.textBox_summpaywork.ReadOnly = true;
			this.textBox_summpaywork.Size = new System.Drawing.Size(112, 26);
			this.textBox_summpaywork.TabIndex = 7;
			this.textBox_summpaywork.Text = "";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(8, 168);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(200, 23);
			this.label5.TabIndex = 8;
			this.label5.Text = "Сумма оплаты (детали)";
			// 
			// textBox_summpaydetail
			// 
			this.textBox_summpaydetail.Location = new System.Drawing.Point(256, 168);
			this.textBox_summpaydetail.Name = "textBox_summpaydetail";
			this.textBox_summpaydetail.ReadOnly = true;
			this.textBox_summpaydetail.Size = new System.Drawing.Size(112, 26);
			this.textBox_summpaydetail.TabIndex = 9;
			this.textBox_summpaydetail.Text = "";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(8, 208);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(176, 23);
			this.label6.TabIndex = 10;
			this.label6.Text = "ИТОГО К ОПЛАТЕ";
			// 
			// textBox_summpay
			// 
			this.textBox_summpay.Location = new System.Drawing.Point(256, 208);
			this.textBox_summpay.Name = "textBox_summpay";
			this.textBox_summpay.ReadOnly = true;
			this.textBox_summpay.Size = new System.Drawing.Size(112, 26);
			this.textBox_summpay.TabIndex = 11;
			this.textBox_summpay.Text = "";
			// 
			// button_cancel
			// 
			this.button_cancel.Location = new System.Drawing.Point(544, 248);
			this.button_cancel.Name = "button_cancel";
			this.button_cancel.Size = new System.Drawing.Size(104, 32);
			this.button_cancel.TabIndex = 12;
			this.button_cancel.Text = "ОТМЕНА";
			this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
			// 
			// button_calc
			// 
			this.button_calc.Location = new System.Drawing.Point(528, 88);
			this.button_calc.Name = "button_calc";
			this.button_calc.Size = new System.Drawing.Size(112, 23);
			this.button_calc.TabIndex = 13;
			this.button_calc.Text = "Рассчитать";
			this.button_calc.Click += new System.EventHandler(this.button_calc_Click);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(416, 248);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(120, 32);
			this.button1.TabIndex = 14;
			this.button1.Text = "ПРИМЕНИТЬ";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// FormManageDiscount
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(8, 19);
			this.ClientSize = new System.Drawing.Size(656, 287);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.button1,
																		  this.button_calc,
																		  this.button_cancel,
																		  this.textBox_summpay,
																		  this.label6,
																		  this.textBox_summpaydetail,
																		  this.label5,
																		  this.textBox_summpaywork,
																		  this.label4,
																		  this.textBox_discountsumm,
																		  this.label3,
																		  this.textBox_discountpercent,
																		  this.label2,
																		  this.textBox_summwork,
																		  this.label1});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "FormManageDiscount";
			this.Text = "Управление скидкой по заказ-няряду";
			this.ResumeLayout(false);

		}
		#endregion

		private void button_cancel_Click(object sender, System.EventArgs e)
		{
			// Отмена всех изменение/неприменяем скидку
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		private void button_calc_Click(object sender, System.EventArgs e)
		{
			// Запрос суммы скидки в рублях
			FormSelectString dialog = new FormSelectString("Запрос суммы скидки", "0.0");
			if (dialog.ShowDialog() !=DialogResult.OK) return;
			float discount = dialog.SelectedFloat;
			if (discount == 0.0F) return;
			if (discount > summwork)
			{
				MessageBox.Show("ОШИБКА!", "Скидка превышает стоимость работ!");
				return;
			}
			discountpercent = (discount/summwork)*100;
			textBox_discountpercent.Text = discountpercent.ToString();
			Recalc(discountpercent);
		}
		public void Recalc(float discount)
		{
			float summwork1 = card.SummWorkPayDiscount(discount);
			float summworkpay1 = card.SummWorkPayDiscount(discount) + card.SummDetailOilPay();
			float summdetailpay1 = card.SummDetailPay();
			float summpay1 = summworkpay1 + summdetailpay1;
			float summdiscount1 = summworkpay - summworkpay1;

			textBox_summwork.Text = summwork1.ToString();
			textBox_summpaywork.Text = summworkpay1.ToString();
			textBox_summpaydetail.Text = summdetailpay1.ToString();
			textBox_summpay.Text = summpay1.ToString();

			//textBox_discountpercent.Text = discount.ToString();
			textBox_discountsumm.Text = summdiscount1.ToString();
		}

		private void textBox_discountpercent_TextChanged(object sender, System.EventArgs e)
		{
			// Автоматически пересчитываем скидку
			string selectedText = textBox_discountpercent.Text;
			selectedText.Trim();
			selectedText = selectedText.Replace(".", ",");
			float data = 0.0F;
			try
			{
				data = (float)Convert.ToDouble(selectedText);
			}
			catch(Exception E)
			{
				MessageBox.Show(E.Message);
				data = 0;
				selectedText = "0";
			}
			discountpercent = data;
			if (discountpercent > 100)
			{
				discountpercent = 0;
				selectedText = "0";
			}
			if (selectedText != textBox_discountpercent.Text)
				textBox_discountpercent.Text = selectedText;
			Recalc(discountpercent);
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Close();
		}
	}
}
