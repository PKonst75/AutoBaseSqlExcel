using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormSelectDataGuaranty.
	/// </summary>
	public class FormSelectDataGuaranty : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox textBox_number;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.DateTimePicker dateTime_request;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button button_ok;
		private System.Windows.Forms.DateTimePicker dateTime_begin;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.DateTimePicker dateTime_end;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public string		number			= "";
		public DateTime		time_request	= DateTime.Now;
		public DateTime		time_begin		= DateTime.Now;
		public DateTime		time_end		= DateTime.Now;

		public FormSelectDataGuaranty()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
			this.textBox_number = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.dateTime_request = new System.Windows.Forms.DateTimePicker();
			this.label2 = new System.Windows.Forms.Label();
			this.button_ok = new System.Windows.Forms.Button();
			this.dateTime_begin = new System.Windows.Forms.DateTimePicker();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.dateTime_end = new System.Windows.Forms.DateTimePicker();
			this.SuspendLayout();
			// 
			// textBox_number
			// 
			this.textBox_number.Location = new System.Drawing.Point(256, 16);
			this.textBox_number.Name = "textBox_number";
			this.textBox_number.Size = new System.Drawing.Size(104, 23);
			this.textBox_number.TabIndex = 0;
			this.textBox_number.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(160, 23);
			this.label1.TabIndex = 1;
			this.label1.Text = "Номер заказ-наряда";
			// 
			// dateTime_request
			// 
			this.dateTime_request.Format = System.Windows.Forms.DateTimePickerFormat.Time;
			this.dateTime_request.Location = new System.Drawing.Point(256, 48);
			this.dateTime_request.Name = "dateTime_request";
			this.dateTime_request.Size = new System.Drawing.Size(104, 23);
			this.dateTime_request.TabIndex = 2;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(176, 23);
			this.label2.TabIndex = 3;
			this.label2.Text = "Дата и Время обращения";
			// 
			// button_ok
			// 
			this.button_ok.Location = new System.Drawing.Point(200, 176);
			this.button_ok.Name = "button_ok";
			this.button_ok.TabIndex = 4;
			this.button_ok.Text = "OK";
			this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
			// 
			// dateTime_begin
			// 
			this.dateTime_begin.Format = System.Windows.Forms.DateTimePickerFormat.Time;
			this.dateTime_begin.Location = new System.Drawing.Point(256, 80);
			this.dateTime_begin.Name = "dateTime_begin";
			this.dateTime_begin.Size = new System.Drawing.Size(104, 23);
			this.dateTime_begin.TabIndex = 5;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 80);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(208, 23);
			this.label3.TabIndex = 6;
			this.label3.Text = "Дата и Время начала ремонта";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 112);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(232, 23);
			this.label4.TabIndex = 7;
			this.label4.Text = "Дата и Время окончания ремонта";
			// 
			// dateTime_end
			// 
			this.dateTime_end.Format = System.Windows.Forms.DateTimePickerFormat.Time;
			this.dateTime_end.Location = new System.Drawing.Point(256, 112);
			this.dateTime_end.Name = "dateTime_end";
			this.dateTime_end.Size = new System.Drawing.Size(104, 23);
			this.dateTime_end.TabIndex = 8;
			// 
			// FormSelectDataGuaranty
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.ClientSize = new System.Drawing.Size(482, 213);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.dateTime_end,
																		  this.label4,
																		  this.label3,
																		  this.dateTime_begin,
																		  this.button_ok,
																		  this.label2,
																		  this.dateTime_request,
																		  this.label1,
																		  this.textBox_number});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "FormSelectDataGuaranty";
			this.Text = "Настройка гарантийного заказ-наряда";
			this.ResumeLayout(false);

		}
		#endregion

		protected override void OnCreateControl()
		{
			// Устанавливаем начальные значения
			textBox_number.Text		= number;
			dateTime_request.Value	= time_request;
			dateTime_begin.Value	= time_begin;
			dateTime_end.Value		= time_end;
		}

		private void button_ok_Click(object sender, System.EventArgs e)
		{
			number				= textBox_number.Text;
			time_request		= dateTime_request.Value;
			time_begin			= dateTime_begin.Value;
			time_end			= dateTime_end.Value;
			this.DialogResult	= DialogResult.OK;
		}

		public DateTime TimeRequest
		{
			get
			{
				return time_request;
			}
		}
		public DateTime TimeBegin
		{
			get
			{
				return time_begin;
			}
		}
		public DateTime TimeEnd
		{
			get
			{
				return time_end;
			}
		}
	}
}
