using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for UI_CallToClient.
	/// </summary>
	public class UI_CallToClient : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView listView_info;
		private System.Windows.Forms.DateTimePicker dateTimePicker_start;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.DateTimePicker dateTimePicker_end;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.Button button1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public UI_CallToClient()
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
			this.listView_info = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.dateTimePicker_start = new System.Windows.Forms.DateTimePicker();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.dateTimePicker_end = new System.Windows.Forms.DateTimePicker();
			this.button1 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// listView_info
			// 
			this.listView_info.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.listView_info.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																							this.columnHeader1,
																							this.columnHeader2,
																							this.columnHeader3});
			this.listView_info.Location = new System.Drawing.Point(8, 88);
			this.listView_info.Name = "listView_info";
			this.listView_info.Size = new System.Drawing.Size(648, 192);
			this.listView_info.TabIndex = 0;
			this.listView_info.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "ФИО/Наименование";
			this.columnHeader1.Width = 200;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Место";
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Автомобиль";
			this.columnHeader3.Width = 200;
			// 
			// dateTimePicker_start
			// 
			this.dateTimePicker_start.Location = new System.Drawing.Point(136, 8);
			this.dateTimePicker_start.Name = "dateTimePicker_start";
			this.dateTimePicker_start.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(0, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(128, 23);
			this.label1.TabIndex = 2;
			this.label1.Text = "Начало интервала";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(120, 23);
			this.label2.TabIndex = 3;
			this.label2.Text = "Конец интервала";
			// 
			// dateTimePicker_end
			// 
			this.dateTimePicker_end.Location = new System.Drawing.Point(136, 40);
			this.dateTimePicker_end.Name = "dateTimePicker_end";
			this.dateTimePicker_end.TabIndex = 4;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(344, 8);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(96, 23);
			this.button1.TabIndex = 5;
			this.button1.Text = "Выполнить";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// UI_CallToClient
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.ClientSize = new System.Drawing.Size(672, 293);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.button1,
																		  this.dateTimePicker_end,
																		  this.label2,
																		  this.label1,
																		  this.dateTimePicker_start,
																		  this.listView_info});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.Name = "UI_CallToClient";
			this.Text = "Обзвон клиентов";
			this.ResumeLayout(false);

		}
		#endregion

		private void button1_Click(object sender, System.EventArgs e)
		{
			// Обновить список
			DateTime start = dateTimePicker_start.Value;
			DateTime stop = dateTimePicker_end.Value;

			listView_info.Items.Clear();
			DbSqlAutoSell.SelectInListCallToClient(listView_info, start, stop);
		}
	}
}
