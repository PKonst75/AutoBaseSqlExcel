using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for UIInquiryPrompt.
	/// </summary>
	public class UIInquiryPrompt : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox textBox_inquiry;
		private System.Windows.Forms.Label label_prompt;
		private System.Windows.Forms.Label label_prompt_add;
		private System.Windows.Forms.Button button_ok;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		string the_text;

		public UIInquiryPrompt(string prompt, string prompt_add)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			// Начальные параметры
			the_text = "";

			// Устанавливаем текст основной подсказки
			label_prompt.Text = prompt;
			// Устанавливаем текст дополнительной подсказки
			label_prompt_add.Text = prompt_add;
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
			this.textBox_inquiry = new System.Windows.Forms.TextBox();
			this.label_prompt = new System.Windows.Forms.Label();
			this.label_prompt_add = new System.Windows.Forms.Label();
			this.button_ok = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// textBox_inquiry
			// 
			this.textBox_inquiry.Location = new System.Drawing.Point(8, 192);
			this.textBox_inquiry.Name = "textBox_inquiry";
			this.textBox_inquiry.Size = new System.Drawing.Size(544, 29);
			this.textBox_inquiry.TabIndex = 0;
			this.textBox_inquiry.Text = "";
			// 
			// label_prompt
			// 
			this.label_prompt.Location = new System.Drawing.Point(16, 8);
			this.label_prompt.Name = "label_prompt";
			this.label_prompt.Size = new System.Drawing.Size(536, 88);
			this.label_prompt.TabIndex = 1;
			this.label_prompt.Text = "Подсказка к запросу";
			this.label_prompt.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// label_prompt_add
			// 
			this.label_prompt_add.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.label_prompt_add.Location = new System.Drawing.Point(8, 120);
			this.label_prompt_add.Name = "label_prompt_add";
			this.label_prompt_add.Size = new System.Drawing.Size(544, 48);
			this.label_prompt_add.TabIndex = 2;
			this.label_prompt_add.Text = "Дополнительная подсказка к запросу";
			this.label_prompt_add.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// button_ok
			// 
			this.button_ok.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.button_ok.Location = new System.Drawing.Point(240, 240);
			this.button_ok.Name = "button_ok";
			this.button_ok.TabIndex = 3;
			this.button_ok.Text = "Принять";
			this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
			// 
			// UIInquiryPrompt
			// 
			this.AcceptButton = this.button_ok;
			this.AutoScaleBaseSize = new System.Drawing.Size(9, 22);
			this.ClientSize = new System.Drawing.Size(560, 273);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.button_ok,
																		  this.label_prompt_add,
																		  this.label_prompt,
																		  this.textBox_inquiry});
			this.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "UIInquiryPrompt";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Запрос с подсказкой";
			this.ResumeLayout(false);

		}
		#endregion

		private void button_ok_Click(object sender, System.EventArgs e)
		{
			// Закрываем окно в случае непустого поля запроса
			string text = textBox_inquiry.Text;
			text = text.Trim();
			if(text.Length == 0) return;
			the_text = text;
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		public string Inquiry
		{
			get
			{
				return the_text;
			}
		}
	}
}
