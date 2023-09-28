using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormUpdateColor.
	/// </summary>
	public class FormUpdateColor : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBox_description;
		private System.Windows.Forms.TextBox textBox_code;
		private System.Windows.Forms.Button button_save;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		DtColor	color;
		private System.Windows.Forms.TextBox textBox_name;
		DtModel	model;

		public FormUpdateColor(long code_model, long code)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			if(code == 0)
			{
				// лНДЕКЭ ДКЪ ЙНРНПНИ ХЫЕЛ ЖБЕР
				model	= DbSqlModel.Find(code_model);
				if(model == null)
				{
					button_save.Enabled = false;
					return;
				}
				color = new DtColor();
				color.SetData("яяшкйю_йнд_юбрнлнахкэ_лндекэ", (long)model.GetData("йнд_юбрнлнахкэ_лндекэ"));
			}
			else
			{
				DtColor element = DbSqlColor.Find(code);
				if(element == null)
				{
					button_save.Enabled = false;
					return;
				}
				model	= DbSqlModel.Find((long)element.GetData("яяшкйю_йнд_юбрнлнахкэ_лндекэ"));
				if(model == null)
				{
					button_save.Enabled = false;
					return;
				}
				color = new DtColor(element);
			}
			this.Text = this.Text + " ЛНДЕКЭ " + (string)model.GetData("лндекэ");
			textBox_code.Text			= (string)color.GetData("жбер_йнд");
			textBox_name.Text			= (string)color.GetData("жбер_мюхлемнбюмхе");
			textBox_description.Text	= (string)color.GetData("жбер_нохяюмхе");

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
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.textBox_name = new System.Windows.Forms.TextBox();
			this.textBox_description = new System.Windows.Forms.TextBox();
			this.textBox_code = new System.Windows.Forms.TextBox();
			this.button_save = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 16);
			this.label1.Name = "label1";
			this.label1.TabIndex = 0;
			this.label1.Text = "йНД ЖБЕРЮ";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(112, 23);
			this.label2.TabIndex = 1;
			this.label2.Text = "мЮХЛЕМНБЮМХЕ";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 64);
			this.label3.Name = "label3";
			this.label3.TabIndex = 2;
			this.label3.Text = "нОХЯЮМХЕ";
			// 
			// textBox_name
			// 
			this.textBox_name.Location = new System.Drawing.Point(120, 32);
			this.textBox_name.Name = "textBox_name";
			this.textBox_name.Size = new System.Drawing.Size(232, 23);
			this.textBox_name.TabIndex = 2;
			this.textBox_name.Text = "";
			// 
			// textBox_description
			// 
			this.textBox_description.Location = new System.Drawing.Point(120, 56);
			this.textBox_description.Name = "textBox_description";
			this.textBox_description.Size = new System.Drawing.Size(232, 23);
			this.textBox_description.TabIndex = 3;
			this.textBox_description.Text = "";
			// 
			// textBox_code
			// 
			this.textBox_code.Location = new System.Drawing.Point(120, 8);
			this.textBox_code.Name = "textBox_code";
			this.textBox_code.Size = new System.Drawing.Size(232, 23);
			this.textBox_code.TabIndex = 1;
			this.textBox_code.Text = "";
			// 
			// button_save
			// 
			this.button_save.Location = new System.Drawing.Point(264, 88);
			this.button_save.Name = "button_save";
			this.button_save.Size = new System.Drawing.Size(88, 23);
			this.button_save.TabIndex = 4;
			this.button_save.Text = "яНУПЮМХРЭ";
			this.button_save.Click += new System.EventHandler(this.button_save_Click);
			// 
			// FormUpdateColor
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.ClientSize = new System.Drawing.Size(360, 117);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.button_save,
																		  this.textBox_code,
																		  this.textBox_description,
																		  this.textBox_name,
																		  this.label3,
																		  this.label2,
																		  this.label1});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "FormUpdateColor";
			this.Text = "жБЕР:";
			this.ResumeLayout(false);

		}
		#endregion

		private void button_save_Click(object sender, System.EventArgs e)
		{
			// яНУПЮМЪЕЛ ЖБЕР
			color.SetData("жбер_йнд", textBox_code.Text);
			color.SetData("жбер_мюхлемнбюмхе", textBox_name.Text);
			color.SetData("жбер_нохяюмхе", textBox_description.Text);

			if(color.CheckData("жбер_мюхлемнбюмхе") == false) return;
			if(color.CheckData("яяшкйю_йнд_юбрнлнахкэ_лндекэ") == false) return;

			if((long)color.GetData("йнд_юбрнлнахкэ_жбер")==0)
			{
				DtColor result = DbSqlColor.Insert(color);
				if(result == null) return;
				color = result;
			}
			else
			{
				if(DbSqlColor.Update(color) == false) return;
			}
			DialogResult = DialogResult.OK;
			this.Close();
		}

		public DtColor Color
		{
			get
			{
				return color;
			}
		}
	}
}
