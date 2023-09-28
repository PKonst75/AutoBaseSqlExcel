using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormSelectDateInterval.
	/// </summary>
	public class FormSelectDateInterval : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.DateTimePicker dateTimePickerStart;
		private System.Windows.Forms.DateTimePicker dateTimePickerEnd;
		private System.Windows.Forms.Button buttonOk;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private DateTime startDate;
		private DateTime endDate;

		public FormSelectDateInterval()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			DateTime date = DateTime.Today;
			dateTimePickerStart.Value = date.AddMonths(-1);
			dateTimePickerEnd.Value = date;
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
			this.dateTimePickerStart = new System.Windows.Forms.DateTimePicker();
			this.dateTimePickerEnd = new System.Windows.Forms.DateTimePicker();
			this.buttonOk = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(56, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "От";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 56);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(56, 23);
			this.label2.TabIndex = 1;
			this.label2.Text = "До";
			// 
			// dateTimePickerStart
			// 
			this.dateTimePickerStart.Location = new System.Drawing.Point(112, 8);
			this.dateTimePickerStart.Name = "dateTimePickerStart";
			this.dateTimePickerStart.TabIndex = 2;
			// 
			// dateTimePickerEnd
			// 
			this.dateTimePickerEnd.Location = new System.Drawing.Point(112, 56);
			this.dateTimePickerEnd.Name = "dateTimePickerEnd";
			this.dateTimePickerEnd.TabIndex = 3;
			// 
			// buttonOk
			// 
			this.buttonOk.Location = new System.Drawing.Point(168, 96);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.TabIndex = 4;
			this.buttonOk.Text = "ОК";
			this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
			// 
			// FormSelectDateInterval
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(424, 133);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.buttonOk,
																		  this.dateTimePickerEnd,
																		  this.dateTimePickerStart,
																		  this.label2,
																		  this.label1});
			this.Name = "FormSelectDateInterval";
			this.Text = "Выбор временного интервала";
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonOk_Click(object sender, System.EventArgs e)
		{
			// Запоминаем выбранный временной интервал
			startDate = dateTimePickerStart.Value;
			endDate = dateTimePickerEnd.Value;
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		public DateTime StartDate
		{
			get{return startDate;}
		}

		public DateTime EndDate
		{
			get{return endDate;}
		}
	}
}
