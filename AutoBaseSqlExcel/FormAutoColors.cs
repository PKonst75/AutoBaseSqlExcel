using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormAutoColors.
	/// </summary>
	public class FormAutoColors : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBoxProductYearStart;
		private System.Windows.Forms.TextBox textBoxProductYearEnd;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button buttonOk;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.TextBox textBoxCodeColorTxt;
		private System.Windows.Forms.TextBox textBoxColorDescription;
		private System.Windows.Forms.TextBox textBoxNameColorTxt;
		private System.Windows.Forms.Label label5;

		DbAutoColors autoColors;

		public FormAutoColors(DbAutoModel autoModel, DbAutoColors autoColorsSrc, string srcColorName)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			if(autoColorsSrc != null)
			{
				autoColors	= new DbAutoColors(autoColorsSrc);
			}
			else
			{
				autoColors				= new DbAutoColors(autoModel);
				autoColors.NameColorTxt	= srcColorName;
			}

			this.Text							= "Цвет для " + autoModel.Model;
			this.textBoxCodeColorTxt.Text		= autoColors.CodeColorTxt;
			this.textBoxNameColorTxt.Text		= autoColors.NameColorTxt;
			this.textBoxColorDescription.Text	= autoColors.ColorDescription;
			this.textBoxProductYearStart.Text	= autoColors.ProductYearStartTxt;
			this.textBoxProductYearEnd.Text		= autoColors.ProductYearEndTxt;
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
			this.textBoxCodeColorTxt = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.textBoxColorDescription = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textBoxProductYearStart = new System.Windows.Forms.TextBox();
			this.textBoxProductYearEnd = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.buttonOk = new System.Windows.Forms.Button();
			this.textBoxNameColorTxt = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// textBoxCodeColorTxt
			// 
			this.textBoxCodeColorTxt.Location = new System.Drawing.Point(136, 16);
			this.textBoxCodeColorTxt.Name = "textBoxCodeColorTxt";
			this.textBoxCodeColorTxt.Size = new System.Drawing.Size(304, 23);
			this.textBoxCodeColorTxt.TabIndex = 0;
			this.textBoxCodeColorTxt.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(80, 24);
			this.label1.TabIndex = 1;
			this.label1.Text = "Код цвета";
			// 
			// textBoxColorDescription
			// 
			this.textBoxColorDescription.Location = new System.Drawing.Point(136, 88);
			this.textBoxColorDescription.Name = "textBoxColorDescription";
			this.textBoxColorDescription.Size = new System.Drawing.Size(304, 23);
			this.textBoxColorDescription.TabIndex = 2;
			this.textBoxColorDescription.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 88);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(88, 23);
			this.label2.TabIndex = 3;
			this.label2.Text = "Описание";
			// 
			// textBoxProductYearStart
			// 
			this.textBoxProductYearStart.Location = new System.Drawing.Point(136, 128);
			this.textBoxProductYearStart.Name = "textBoxProductYearStart";
			this.textBoxProductYearStart.Size = new System.Drawing.Size(64, 23);
			this.textBoxProductYearStart.TabIndex = 4;
			this.textBoxProductYearStart.Text = "";
			// 
			// textBoxProductYearEnd
			// 
			this.textBoxProductYearEnd.Location = new System.Drawing.Point(240, 128);
			this.textBoxProductYearEnd.Name = "textBoxProductYearEnd";
			this.textBoxProductYearEnd.Size = new System.Drawing.Size(64, 23);
			this.textBoxProductYearEnd.TabIndex = 5;
			this.textBoxProductYearEnd.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 128);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(112, 23);
			this.label3.TabIndex = 6;
			this.label3.Text = "Года выпуска с";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(208, 128);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(32, 24);
			this.label4.TabIndex = 7;
			this.label4.Text = "по";
			// 
			// buttonOk
			// 
			this.buttonOk.Location = new System.Drawing.Point(184, 168);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.TabIndex = 8;
			this.buttonOk.Text = "ОК";
			this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
			// 
			// textBoxNameColorTxt
			// 
			this.textBoxNameColorTxt.Location = new System.Drawing.Point(136, 48);
			this.textBoxNameColorTxt.Name = "textBoxNameColorTxt";
			this.textBoxNameColorTxt.Size = new System.Drawing.Size(304, 23);
			this.textBoxNameColorTxt.TabIndex = 9;
			this.textBoxNameColorTxt.Text = "";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(8, 48);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(112, 24);
			this.label5.TabIndex = 10;
			this.label5.Text = "Наименование";
			// 
			// FormAutoColors
			// 
			this.AcceptButton = this.buttonOk;
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.ClientSize = new System.Drawing.Size(450, 207);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.label5,
																		  this.textBoxNameColorTxt,
																		  this.buttonOk,
																		  this.label4,
																		  this.label3,
																		  this.textBoxProductYearEnd,
																		  this.textBoxProductYearStart,
																		  this.label2,
																		  this.textBoxColorDescription,
																		  this.label1,
																		  this.textBoxCodeColorTxt});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "FormAutoColors";
			this.Text = "Цвет";
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonOk_Click(object sender, System.EventArgs e)
		{
			// Собираем данные и записываем новую комплектацию
			autoColors.CodeColorTxt			= this.textBoxCodeColorTxt.Text;
			autoColors.NameColorTxt			= this.textBoxNameColorTxt.Text;
			autoColors.ColorDescription		= this.textBoxColorDescription.Text;
			autoColors.ProductYearStartTxt	= this.textBoxProductYearStart.Text;
			autoColors.ProductYearEndTxt	= this.textBoxProductYearEnd.Text;

			if(Db.ShowFaults()) return;

			if(autoColors.Write() != true)
			{
				Db.ShowFaults();
				return;
			}
			this.DialogResult = DialogResult.OK;
			this.Close();
		}
		public DbAutoColors AutoColors
		{
			get
			{
				return autoColors;
			}
		}
	}
}
