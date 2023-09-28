using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for UIYesNoPrompt.
	/// </summary>
	public class UIYesNoPrompt : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label_prompt;
		private System.Windows.Forms.Label label_prompt_add;
		private System.Windows.Forms.Button button_yes;
		private System.Windows.Forms.Button button_no;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		UserInterface.Buttons result;

		public UIYesNoPrompt(string prompt, string prompt_add)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			label_prompt.Text		= prompt;
			label_prompt_add.Text	= prompt_add;
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
			this.label_prompt = new System.Windows.Forms.Label();
			this.label_prompt_add = new System.Windows.Forms.Label();
			this.button_yes = new System.Windows.Forms.Button();
			this.button_no = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label_prompt
			// 
			this.label_prompt.Location = new System.Drawing.Point(16, 16);
			this.label_prompt.Name = "label_prompt";
			this.label_prompt.Size = new System.Drawing.Size(536, 72);
			this.label_prompt.TabIndex = 0;
			this.label_prompt.Text = "Основная подсказка";
			this.label_prompt.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// label_prompt_add
			// 
			this.label_prompt_add.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.label_prompt_add.Location = new System.Drawing.Point(8, 96);
			this.label_prompt_add.Name = "label_prompt_add";
			this.label_prompt_add.Size = new System.Drawing.Size(544, 64);
			this.label_prompt_add.TabIndex = 1;
			this.label_prompt_add.Text = "Дополнительная подсказка";
			this.label_prompt_add.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// button_yes
			// 
			this.button_yes.Location = new System.Drawing.Point(192, 176);
			this.button_yes.Name = "button_yes";
			this.button_yes.Size = new System.Drawing.Size(80, 80);
			this.button_yes.TabIndex = 2;
			this.button_yes.Text = "ДА";
			this.button_yes.Click += new System.EventHandler(this.button_yes_Click);
			// 
			// button_no
			// 
			this.button_no.Location = new System.Drawing.Point(296, 176);
			this.button_no.Name = "button_no";
			this.button_no.Size = new System.Drawing.Size(80, 80);
			this.button_no.TabIndex = 3;
			this.button_no.Text = "НЕТ";
			this.button_no.Click += new System.EventHandler(this.button_no_Click);
			// 
			// UIYesNoPrompt
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(9, 22);
			this.ClientSize = new System.Drawing.Size(568, 273);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.button_no,
																		  this.button_yes,
																		  this.label_prompt_add,
																		  this.label_prompt});
			this.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "UIYesNoPrompt";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Запрос с подсказкой";
			this.ResumeLayout(false);

		}
		#endregion

		private void button_yes_Click(object sender, System.EventArgs e)
		{
			result = UserInterface.Buttons.Yes;
			DialogResult = DialogResult.OK;
			this.Close();
		}

		private void button_no_Click(object sender, System.EventArgs e)
		{
			result = UserInterface.Buttons.No;
			DialogResult = DialogResult.OK;
			this.Close();
		}
		public UserInterface.Buttons Result
		{
			get
			{
				return result;
			}
		}
	}
}
