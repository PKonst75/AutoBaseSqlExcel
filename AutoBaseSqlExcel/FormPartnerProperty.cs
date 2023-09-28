using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormPartnerProperty.
	/// </summary>
	public class FormPartnerProperty : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox textBox_partner;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox checkBox_cashless;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBox_discount;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBox_comment;
		private System.Windows.Forms.Button button_ok;
		private System.Windows.Forms.Button button_partner_select;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		DtPartnerProperty	partner_property;
		DtPartnerProperty	source;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBox_card_number;
		bool				adding;

		public FormPartnerProperty(long code_partner)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			if(code_partner == 0)
			{
				// Это попытка добавления нового контрагента
				partner_property = new DtPartnerProperty();
				// Запрос на поиск
				FormPartnerList dialog = new FormPartnerList();
				if(dialog.ShowDialog() != DialogResult.OK) return;
				// Установка новых параметров
				partner_property.PartnerName	= dialog.Partner.NameShort;
				partner_property.CodePartner	= dialog.Partner.Code;
				adding = true;
			}
			else
			{
				// Чтение свойства контрагента
				source = DbSqlPartnerProperty.Find(code_partner);
				if(source == null)
				{
					partner_property = new DtPartnerProperty();
				}
				else
				{
					button_partner_select.Enabled = false;
					partner_property	= new DtPartnerProperty(source);
				}
			}
			// Заполнение диалога
			textBox_partner.Text		= partner_property.PartnerName;
			textBox_discount.Text		= partner_property.DiscountTxt;
			textBox_comment.Text		= partner_property.Comment;
			textBox_card_number.Text	= partner_property.CardNumberTxt;
			checkBox_cashless.Checked	= partner_property.Cashless;
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
			this.textBox_partner = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.checkBox_cashless = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textBox_discount = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textBox_comment = new System.Windows.Forms.TextBox();
			this.button_ok = new System.Windows.Forms.Button();
			this.button_partner_select = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.textBox_card_number = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// textBox_partner
			// 
			this.textBox_partner.Location = new System.Drawing.Point(8, 40);
			this.textBox_partner.Name = "textBox_partner";
			this.textBox_partner.ReadOnly = true;
			this.textBox_partner.Size = new System.Drawing.Size(496, 23);
			this.textBox_partner.TabIndex = 0;
			this.textBox_partner.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 16);
			this.label1.Name = "label1";
			this.label1.TabIndex = 1;
			this.label1.Text = "Контрагент";
			// 
			// checkBox_cashless
			// 
			this.checkBox_cashless.Location = new System.Drawing.Point(8, 88);
			this.checkBox_cashless.Name = "checkBox_cashless";
			this.checkBox_cashless.TabIndex = 2;
			this.checkBox_cashless.Text = "Безнал";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 120);
			this.label2.Name = "label2";
			this.label2.TabIndex = 3;
			this.label2.Text = "Скидка";
			// 
			// textBox_discount
			// 
			this.textBox_discount.Location = new System.Drawing.Point(8, 152);
			this.textBox_discount.Name = "textBox_discount";
			this.textBox_discount.Size = new System.Drawing.Size(144, 23);
			this.textBox_discount.TabIndex = 4;
			this.textBox_discount.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 184);
			this.label3.Name = "label3";
			this.label3.TabIndex = 5;
			this.label3.Text = "Примечание";
			// 
			// textBox_comment
			// 
			this.textBox_comment.Location = new System.Drawing.Point(8, 208);
			this.textBox_comment.Name = "textBox_comment";
			this.textBox_comment.Size = new System.Drawing.Size(520, 23);
			this.textBox_comment.TabIndex = 6;
			this.textBox_comment.Text = "";
			// 
			// button_ok
			// 
			this.button_ok.Location = new System.Drawing.Point(232, 248);
			this.button_ok.Name = "button_ok";
			this.button_ok.TabIndex = 7;
			this.button_ok.Text = "ОК";
			this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
			// 
			// button_partner_select
			// 
			this.button_partner_select.Location = new System.Drawing.Point(504, 40);
			this.button_partner_select.Name = "button_partner_select";
			this.button_partner_select.Size = new System.Drawing.Size(24, 23);
			this.button_partner_select.TabIndex = 8;
			this.button_partner_select.Text = "...";
			this.button_partner_select.Click += new System.EventHandler(this.button_partner_select_Click);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(168, 120);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(192, 23);
			this.label4.TabIndex = 9;
			this.label4.Text = "Идентификационная карта";
			// 
			// textBox_card_number
			// 
			this.textBox_card_number.Location = new System.Drawing.Point(168, 152);
			this.textBox_card_number.Name = "textBox_card_number";
			this.textBox_card_number.Size = new System.Drawing.Size(176, 23);
			this.textBox_card_number.TabIndex = 10;
			this.textBox_card_number.Text = "";
			// 
			// FormPartnerProperty
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.ClientSize = new System.Drawing.Size(544, 287);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.textBox_card_number,
																		  this.label4,
																		  this.button_partner_select,
																		  this.button_ok,
																		  this.textBox_comment,
																		  this.label3,
																		  this.textBox_discount,
																		  this.label2,
																		  this.checkBox_cashless,
																		  this.label1,
																		  this.textBox_partner});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "FormPartnerProperty";
			this.Text = "Свойства контрагента";
			this.ResumeLayout(false);

		}
		#endregion

		private void button_ok_Click(object sender, System.EventArgs e)
		{
			bool cancel = false;
			// Добавление/изменение свойства контрагента
			// Устанавливаем новые данные
			partner_property.Cashless	= checkBox_cashless.Checked;
			partner_property.Discount	= (float)Convert.ToDouble(textBox_discount.Text);
			partner_property.Comment	= textBox_comment.Text;
			partner_property.CardNumber	= (long)Convert.ToInt64(textBox_card_number.Text);

			// Исправляем явные ошибки
			if(partner_property.Update("СКИДКА") == true)
			{
				textBox_discount.BackColor	= Color.Yellow;
				textBox_discount.Text		= partner_property.DiscountTxt;
				cancel = true;
			}
			if(partner_property.Update("НОМЕР_КАРТА") == true)
			{
				textBox_card_number.BackColor	= Color.Yellow;
				textBox_card_number.Text		= partner_property.CardNumberTxt;
				cancel = true;
			}
			if(partner_property.Update("ПРИМЕЧАНИЕ") == true)
			{
				textBox_comment.BackColor = Color.Yellow;
				textBox_comment.Text		= partner_property.Comment;
				cancel = true;
			}
			// Проверяем правильность данных
			if(partner_property.Check("КОД_КОНТРАГЕНТ") == false)
			{
				cancel = true;
				textBox_partner.BackColor = Color.Red;
			}
			if(partner_property.Check("СКИДКА") == false)
			{
				cancel = true;
				textBox_discount.BackColor = Color.Red;
			}
			if(partner_property.Check("НОМЕР_КАРТА") == false)
			{
				cancel = true;
				textBox_card_number.BackColor = Color.Red;
			}
			if(partner_property.Check("ПРИМЕЧАНИЕ") == false)
			{
				cancel = true;
				textBox_comment.BackColor = Color.Red;
			}

			if(cancel == true) return;		// Отменяем действие
			if(adding == true)
			{
				// Добавление
				if(DbSqlPartnerProperty.Insert(partner_property) == false) return;
				this.DialogResult = DialogResult.OK;
				this.Close();
				return;
			}
			// А теперь идет изменение элемента
			if(DbSqlPartnerProperty.Update(partner_property) == false) return;
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void button_partner_select_Click(object sender, System.EventArgs e)
		{
		
		}

		public DtPartnerProperty Element
		{
			get
			{
				return partner_property;
			}
		}
	}
}
