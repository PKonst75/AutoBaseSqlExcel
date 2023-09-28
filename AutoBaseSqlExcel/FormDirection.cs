using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormDirection.
	/// </summary>
	public class FormDirection : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox textBox_factory;
		private System.Windows.Forms.Button button_select_factory;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBox_number;
		private System.Windows.Forms.DateTimePicker dateTimePicker_date;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textBox_model;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textBox_interval_start;
		private System.Windows.Forms.TextBox textBox_interval_end;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox textBox_description;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Button button_ok;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		DtDirection source		= null;
		DtDirection direction	= null;
		bool adding				= false;

		public FormDirection(long code)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			if(code == 0)
			{
				direction = new DtDirection();
				adding = true;
			}
			else
			{
				source	= DbSqlDirection.Find(code);
				if(source == null)
				{
					button_ok.Enabled = false;
					return;
				}
				direction = new DtDirection(source);
			}
			textBox_factory.Text		= (string)direction.GetData("мюхлемнбюмхе_опнхгбндхрекэ_опедохяюмхе");
			textBox_number.Text			= (string)direction.GetData("мнлеп_опедохяюмхе");
			dateTimePicker_date.Value	= (DateTime)direction.GetData("дюрю_опедохяюмхе");
			textBox_model.Text			= (string)direction.GetData("лндекэ_опедохяюмхе");
			textBox_interval_start.Text	= ((long)direction.GetData("мювюкн_хмрепбюк_опедохяюмхе")).ToString();
			textBox_interval_end.Text	= ((long)direction.GetData("нйнмвюмхе_хмрепбюк_опедохяюмхе")).ToString();
			textBox_description.Text	= (string)direction.GetData("нохяюмхе_опедохяюмхе");
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
			this.textBox_factory = new System.Windows.Forms.TextBox();
			this.button_select_factory = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.textBox_number = new System.Windows.Forms.TextBox();
			this.dateTimePicker_date = new System.Windows.Forms.DateTimePicker();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.textBox_model = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.textBox_interval_start = new System.Windows.Forms.TextBox();
			this.textBox_interval_end = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.textBox_description = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.button_ok = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// textBox_factory
			// 
			this.textBox_factory.Location = new System.Drawing.Point(8, 32);
			this.textBox_factory.Name = "textBox_factory";
			this.textBox_factory.ReadOnly = true;
			this.textBox_factory.Size = new System.Drawing.Size(552, 23);
			this.textBox_factory.TabIndex = 0;
			this.textBox_factory.Text = "";
			// 
			// button_select_factory
			// 
			this.button_select_factory.Location = new System.Drawing.Point(560, 32);
			this.button_select_factory.Name = "button_select_factory";
			this.button_select_factory.Size = new System.Drawing.Size(24, 23);
			this.button_select_factory.TabIndex = 1;
			this.button_select_factory.Text = "...";
			this.button_select_factory.Click += new System.EventHandler(this.button_select_factory_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(136, 23);
			this.label1.TabIndex = 2;
			this.label1.Text = "оПНХГБНДХРЕКЭ";
			// 
			// textBox_number
			// 
			this.textBox_number.Location = new System.Drawing.Point(96, 88);
			this.textBox_number.Name = "textBox_number";
			this.textBox_number.Size = new System.Drawing.Size(144, 23);
			this.textBox_number.TabIndex = 3;
			this.textBox_number.Text = "";
			// 
			// dateTimePicker_date
			// 
			this.dateTimePicker_date.Location = new System.Drawing.Point(304, 88);
			this.dateTimePicker_date.Name = "dateTimePicker_date";
			this.dateTimePicker_date.TabIndex = 4;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 64);
			this.label2.Name = "label2";
			this.label2.TabIndex = 5;
			this.label2.Text = "оПЕДОХЯЮМХЕ";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 88);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(80, 23);
			this.label3.TabIndex = 6;
			this.label3.Text = "мНЛЕП";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(256, 88);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(40, 23);
			this.label4.TabIndex = 7;
			this.label4.Text = "НР";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(8, 120);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(80, 23);
			this.label5.TabIndex = 8;
			this.label5.Text = "лНДЕКЭ";
			// 
			// textBox_model
			// 
			this.textBox_model.Location = new System.Drawing.Point(96, 120);
			this.textBox_model.Name = "textBox_model";
			this.textBox_model.Size = new System.Drawing.Size(144, 23);
			this.textBox_model.TabIndex = 9;
			this.textBox_model.Text = "";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(8, 152);
			this.label6.Name = "label6";
			this.label6.TabIndex = 10;
			this.label6.Text = "хМРЕПБЮК";
			// 
			// textBox_interval_start
			// 
			this.textBox_interval_start.Location = new System.Drawing.Point(120, 152);
			this.textBox_interval_start.Name = "textBox_interval_start";
			this.textBox_interval_start.Size = new System.Drawing.Size(120, 23);
			this.textBox_interval_start.TabIndex = 11;
			this.textBox_interval_start.Text = "";
			// 
			// textBox_interval_end
			// 
			this.textBox_interval_end.Location = new System.Drawing.Point(280, 152);
			this.textBox_interval_end.Name = "textBox_interval_end";
			this.textBox_interval_end.Size = new System.Drawing.Size(120, 23);
			this.textBox_interval_end.TabIndex = 12;
			this.textBox_interval_end.Text = "";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(256, 152);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(24, 23);
			this.label7.TabIndex = 13;
			this.label7.Text = "-";
			// 
			// textBox_description
			// 
			this.textBox_description.Location = new System.Drawing.Point(16, 208);
			this.textBox_description.Multiline = true;
			this.textBox_description.Name = "textBox_description";
			this.textBox_description.Size = new System.Drawing.Size(584, 104);
			this.textBox_description.TabIndex = 14;
			this.textBox_description.Text = "";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(16, 184);
			this.label8.Name = "label8";
			this.label8.TabIndex = 15;
			this.label8.Text = "нОХЯЮМХЕ";
			// 
			// button_ok
			// 
			this.button_ok.Location = new System.Drawing.Point(240, 328);
			this.button_ok.Name = "button_ok";
			this.button_ok.TabIndex = 16;
			this.button_ok.Text = "нй";
			this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
			// 
			// FormDirection
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.ClientSize = new System.Drawing.Size(608, 357);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.button_ok,
																		  this.label8,
																		  this.textBox_description,
																		  this.label7,
																		  this.textBox_interval_end,
																		  this.textBox_interval_start,
																		  this.label6,
																		  this.textBox_model,
																		  this.label5,
																		  this.label4,
																		  this.label3,
																		  this.label2,
																		  this.dateTimePicker_date,
																		  this.textBox_number,
																		  this.label1,
																		  this.button_select_factory,
																		  this.textBox_factory});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.Name = "FormDirection";
			this.Text = "оПЕДОХЯЮМХЕ";
			this.ResumeLayout(false);

		}
		#endregion

		private void button_ok_Click(object sender, System.EventArgs e)
		{
			// оНКСВЕМХЕ МНБШУ ДЮММШУ
			direction.SetData("мнлеп_опедохяюмхе", textBox_number.Text);
			direction.SetData("дюрю_опедохяюмхе", dateTimePicker_date.Value);
			direction.SetData("лндекэ_опедохяюмхе", textBox_model.Text);
			direction.SetData("мювюкн_хмрепбюк_опедохяюмхе", Convert.ToInt64(textBox_interval_start.Text));
			direction.SetData("нйнмвюмхе_хмрепбюк_опедохяюмхе", Convert.ToInt64(textBox_interval_end.Text));
			direction.SetData("нохяюмхе_опедохяюмхе", textBox_description.Text);
			
			if((long)direction.GetData("опнхгбндхрекэ_опедохяюмхе") == 0) return;

			if(adding == true)
			{
				if(DbSqlDirection.Insert(direction) != true) return;
			}
			else
			{
				if(DbSqlDirection.Update(direction) != true) return;
			}
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		public DtDirection Direction
		{
			get
			{
				return direction;
			}
		}

		private void button_select_factory_Click(object sender, System.EventArgs e)
		{
			// бШАНП ОПНХГБНДХРЕКЪ
			FormFirmList dialog = new FormFirmList();
			if(dialog.ShowDialog() != DialogResult.OK) return;
			long code = dialog.SelectedCode;
			if(code == 0) return;
			DtFactory factory = DbSqlFactory.Find(code);
			if(factory == null) return;
			textBox_factory.Text	= (string)factory.GetData("мюхлемнбюмхе_юбрнлнахкэ_опнхгбндхрекэ");
			direction.SetData("опнхгбндхрекэ_опедохяюмхе", factory.GetData("йнд_юбрнлнахкэ_опнхгбндхрекэ"));
			direction.SetData("мюхлемнбюмхе_опнхгбндхрекэ_опедохяюмхе", factory.GetData("мюхлемнбюмхе_юбрнлнахкэ_опнхгбндхрекэ"));
		}
	}
}
