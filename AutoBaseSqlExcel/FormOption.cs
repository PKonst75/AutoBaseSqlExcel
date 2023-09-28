using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormOption.
	/// </summary>
	public class FormOption : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxName;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBoxPrice;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBoxValue;
		private System.Windows.Forms.Button buttonOk;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private DbOption	option;

		public FormOption(DbOption src)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			if(src != null)
			{
				option = new DbOption(src);
			}
			else
			{
				option = new DbOption();
			}

			textBoxName.Text	= option.Name;
			textBoxPrice.Text	= option.Price.ToString();
			textBoxValue.Text	= option.ValuePrice.ToString();
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
			this.textBoxName = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textBoxPrice = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textBoxValue = new System.Windows.Forms.TextBox();
			this.buttonOk = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(112, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "Наименование";
			// 
			// textBoxName
			// 
			this.textBoxName.Location = new System.Drawing.Point(8, 32);
			this.textBoxName.Name = "textBoxName";
			this.textBoxName.Size = new System.Drawing.Size(456, 23);
			this.textBoxName.TabIndex = 1;
			this.textBoxName.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(216, 64);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(136, 23);
			this.label2.TabIndex = 2;
			this.label2.Text = "Цена по прайсу";
			// 
			// textBoxPrice
			// 
			this.textBoxPrice.Location = new System.Drawing.Point(360, 64);
			this.textBoxPrice.Name = "textBoxPrice";
			this.textBoxPrice.Size = new System.Drawing.Size(104, 23);
			this.textBoxPrice.TabIndex = 3;
			this.textBoxPrice.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(216, 96);
			this.label3.Name = "label3";
			this.label3.TabIndex = 4;
			this.label3.Text = "Стоимость";
			// 
			// textBoxValue
			// 
			this.textBoxValue.Location = new System.Drawing.Point(360, 96);
			this.textBoxValue.Name = "textBoxValue";
			this.textBoxValue.Size = new System.Drawing.Size(104, 23);
			this.textBoxValue.TabIndex = 5;
			this.textBoxValue.Text = "";
			// 
			// buttonOk
			// 
			this.buttonOk.Location = new System.Drawing.Point(184, 128);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.TabIndex = 6;
			this.buttonOk.Text = "ОК";
			this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
			// 
			// FormOption
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.ClientSize = new System.Drawing.Size(480, 159);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.buttonOk,
																		  this.textBoxValue,
																		  this.label3,
																		  this.textBoxPrice,
																		  this.label2,
																		  this.textBoxName,
																		  this.label1});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "FormOption";
			this.Text = "Дополнительное оборудование";
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonOk_Click(object sender, System.EventArgs e)
		{
			// Добавление/Изменение доп оборудования
			option.Name				= textBoxName.Text;
			option.PriceTxt			= textBoxPrice.Text;
			option.ValuePriceTxt	= textBoxValue.Text;

			if(Db.ShowFaults()) return;

			if(option.Write() != true) return;
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		public DbOption Option
		{
			get
			{
				return option;
			}
		}
	}
}
