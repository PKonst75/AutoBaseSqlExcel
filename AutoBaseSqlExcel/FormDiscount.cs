using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormDiscount.
	/// </summary>
	public class FormDiscount : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox textBox_code;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBox_discount_service_work;
		private System.Windows.Forms.TextBox textBox_partner;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button button_select_partner;
		private System.Windows.Forms.Button button_ok;
		private System.Windows.Forms.TextBox textBox_comment;
		private System.Windows.Forms.Label label4;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		DtDiscount discount_old;
		DtDiscount discount_new;

		public FormDiscount(DtDiscount discount)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			discount_old = new DtDiscount(discount);
			discount_new = new DtDiscount(discount);

			// Установка параметров
			textBox_code.Text = discount_old.GetData("КОД_ДИСКОНТ").ToString();
			textBox_discount_service_work.Text = discount_old.GetData("СКИДКА_СЕРВИС_РАБОТА_ДИСКОНТ").ToString();
			if((long)discount_old.GetData("КОД_КОНТРАГЕНТ_ДИСКОНТ") > 0)
				textBox_partner.Text = (string)discount_old.GetData("КОНТРАГЕНТ_НАИМЕНОВАНИЕ");
			else
				textBox_partner.Text = "НЕ ВЫБРАН";
			textBox_comment.Text	= (string)discount_old.GetData("ПРИМЕЧАНИЕ_ДИСКОНТ");
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
			this.textBox_code = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textBox_discount_service_work = new System.Windows.Forms.TextBox();
			this.textBox_partner = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.button_select_partner = new System.Windows.Forms.Button();
			this.button_ok = new System.Windows.Forms.Button();
			this.textBox_comment = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// textBox_code
			// 
			this.textBox_code.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox_code.Location = new System.Drawing.Point(128, 16);
			this.textBox_code.Name = "textBox_code";
			this.textBox_code.ReadOnly = true;
			this.textBox_code.TabIndex = 0;
			this.textBox_code.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 16);
			this.label1.Name = "label1";
			this.label1.TabIndex = 1;
			this.label1.Text = "Номер карты";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(200, 23);
			this.label2.TabIndex = 2;
			this.label2.Text = "Скидка на работы сервиса";
			// 
			// textBox_discount_service_work
			// 
			this.textBox_discount_service_work.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox_discount_service_work.Location = new System.Drawing.Point(216, 48);
			this.textBox_discount_service_work.Name = "textBox_discount_service_work";
			this.textBox_discount_service_work.TabIndex = 3;
			this.textBox_discount_service_work.Text = "";
			// 
			// textBox_partner
			// 
			this.textBox_partner.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox_partner.Location = new System.Drawing.Point(8, 184);
			this.textBox_partner.Name = "textBox_partner";
			this.textBox_partner.Size = new System.Drawing.Size(408, 23);
			this.textBox_partner.TabIndex = 4;
			this.textBox_partner.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 152);
			this.label3.Name = "label3";
			this.label3.TabIndex = 5;
			this.label3.Text = "Владелец";
			// 
			// button_select_partner
			// 
			this.button_select_partner.Location = new System.Drawing.Point(416, 184);
			this.button_select_partner.Name = "button_select_partner";
			this.button_select_partner.Size = new System.Drawing.Size(24, 23);
			this.button_select_partner.TabIndex = 6;
			this.button_select_partner.Text = "...";
			this.button_select_partner.Click += new System.EventHandler(this.button_select_partner_Click);
			// 
			// button_ok
			// 
			this.button_ok.Location = new System.Drawing.Point(192, 216);
			this.button_ok.Name = "button_ok";
			this.button_ok.Size = new System.Drawing.Size(80, 24);
			this.button_ok.TabIndex = 7;
			this.button_ok.Text = "ОК";
			this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
			// 
			// textBox_comment
			// 
			this.textBox_comment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox_comment.Location = new System.Drawing.Point(8, 112);
			this.textBox_comment.Name = "textBox_comment";
			this.textBox_comment.Size = new System.Drawing.Size(432, 23);
			this.textBox_comment.TabIndex = 8;
			this.textBox_comment.Text = "";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 88);
			this.label4.Name = "label4";
			this.label4.TabIndex = 9;
			this.label4.Text = "Примечание";
			// 
			// FormDiscount
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.ClientSize = new System.Drawing.Size(456, 253);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.label4,
																		  this.textBox_comment,
																		  this.button_ok,
																		  this.button_select_partner,
																		  this.label3,
																		  this.textBox_partner,
																		  this.textBox_discount_service_work,
																		  this.label2,
																		  this.label1,
																		  this.textBox_code});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.Name = "FormDiscount";
			this.Text = "Дисконтная карта";
			this.ResumeLayout(false);

		}
		#endregion

		private void button_ok_Click(object sender, System.EventArgs e)
		{
			// Получаем данные
			float discount_service_work = (float)Convert.ToDouble(textBox_discount_service_work.Text);
			discount_new.SetData("СКИДКА_СЕРВИС_РАБОТА_ДИСКОНТ", discount_service_work);
			discount_new.SetData("ПРИМЕЧАНИЕ_ДИСКОНТ", textBox_comment.Text);
			// Проверка соответсвия
			if(discount_new.IsEqual(discount_old)) return;
			if(DbSqlDiscount.Update(discount_new) != true) return;

			this.DialogResult = DialogResult.OK;
			this.Close();
			return;
		}

		private void button_select_partner_Click(object sender, System.EventArgs e)
		{
			// Выбор владельца
			FormPartnerList dialog = new FormPartnerList();
			dialog.ShowDialog();
			if(dialog.DialogResult != DialogResult.OK) return;
			DbPartner partner = dialog.Partner;
			if(partner == null) return;
			discount_new.SetData("КОД_КОНТРАГЕНТ_ДИСКОНТ", partner.Code);
			discount_new.SetData("КОНТРАГЕНТ_НАИМЕНОВАНИЕ", partner.Name);
			textBox_partner.Text	= partner.Title;
		}
	}
}
