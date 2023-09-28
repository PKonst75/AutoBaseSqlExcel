using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormAutoComplect.
	/// </summary>
	public class FormAutoComplect : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox textBoxCodeComplectTxt;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxDescription;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBoxProductYearStart;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBoxProductYearEnd;
		private System.Windows.Forms.Button buttonOk;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		DbAutoComplect autoComplect;

		public FormAutoComplect(DbAutoModel autoModel, DbAutoComplect autoComplectSrc, string srcCodeComplect)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			if(autoComplectSrc != null)
			{
				autoComplect	= new DbAutoComplect(autoComplectSrc);
			}
			else
			{
				autoComplect					= new DbAutoComplect(autoModel);
				autoComplect.CodeComplectTxt	= srcCodeComplect;
			}

			this.Text							= "Комплектация для " + autoModel.Model;
			this.textBoxCodeComplectTxt.Text	= autoComplect.CodeComplectTxt;
			this.textBoxDescription.Text		= autoComplect.ComplectDescription;
			this.textBoxProductYearStart.Text	= autoComplect.ProductYearStartTxt;
			this.textBoxProductYearEnd.Text		= autoComplect.ProductYearEndTxt;
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
			this.textBoxCodeComplectTxt = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.textBoxDescription = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textBoxProductYearStart = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.textBoxProductYearEnd = new System.Windows.Forms.TextBox();
			this.buttonOk = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// textBoxCodeComplectTxt
			// 
			this.textBoxCodeComplectTxt.Location = new System.Drawing.Point(160, 16);
			this.textBoxCodeComplectTxt.Name = "textBoxCodeComplectTxt";
			this.textBoxCodeComplectTxt.Size = new System.Drawing.Size(272, 23);
			this.textBoxCodeComplectTxt.TabIndex = 0;
			this.textBoxCodeComplectTxt.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(128, 23);
			this.label1.TabIndex = 1;
			this.label1.Text = "Код комплектации";
			// 
			// textBoxDescription
			// 
			this.textBoxDescription.Location = new System.Drawing.Point(160, 56);
			this.textBoxDescription.Name = "textBoxDescription";
			this.textBoxDescription.Size = new System.Drawing.Size(272, 23);
			this.textBoxDescription.TabIndex = 2;
			this.textBoxDescription.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 56);
			this.label2.Name = "label2";
			this.label2.TabIndex = 3;
			this.label2.Text = "Описание";
			// 
			// textBoxProductYearStart
			// 
			this.textBoxProductYearStart.Location = new System.Drawing.Point(160, 96);
			this.textBoxProductYearStart.Name = "textBoxProductYearStart";
			this.textBoxProductYearStart.Size = new System.Drawing.Size(64, 23);
			this.textBoxProductYearStart.TabIndex = 4;
			this.textBoxProductYearStart.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 96);
			this.label3.Name = "label3";
			this.label3.TabIndex = 5;
			this.label3.Text = "Выпускается с";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(232, 96);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(24, 23);
			this.label4.TabIndex = 6;
			this.label4.Text = "по";
			// 
			// textBoxProductYearEnd
			// 
			this.textBoxProductYearEnd.Location = new System.Drawing.Point(264, 96);
			this.textBoxProductYearEnd.Name = "textBoxProductYearEnd";
			this.textBoxProductYearEnd.Size = new System.Drawing.Size(64, 23);
			this.textBoxProductYearEnd.TabIndex = 7;
			this.textBoxProductYearEnd.Text = "";
			// 
			// buttonOk
			// 
			this.buttonOk.Location = new System.Drawing.Point(176, 136);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.TabIndex = 8;
			this.buttonOk.Text = "OK";
			this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
			// 
			// FormAutoComplect
			// 
			this.AcceptButton = this.buttonOk;
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.ClientSize = new System.Drawing.Size(440, 173);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.buttonOk,
																		  this.textBoxProductYearEnd,
																		  this.label4,
																		  this.label3,
																		  this.textBoxProductYearStart,
																		  this.label2,
																		  this.textBoxDescription,
																		  this.label1,
																		  this.textBoxCodeComplectTxt});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "FormAutoComplect";
			this.Text = "Комплектация";
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonOk_Click(object sender, System.EventArgs e)
		{
			// Собираем данные и записываем новую комплектацию
			autoComplect.CodeComplectTxt		= this.textBoxCodeComplectTxt.Text;
			autoComplect.ComplectDescription	= this.textBoxDescription.Text;
			autoComplect.ProductYearStartTxt	= this.textBoxProductYearStart.Text;
			autoComplect.ProductYearEndTxt		= this.textBoxProductYearEnd.Text;

			if(Db.ShowFaults()) return;

			if(autoComplect.Write() != true)
			{
				Db.ShowFaults();
				return;
			}
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		public DbAutoComplect AutoComplect
		{
			get
			{
				return autoComplect;
			}
		}
	}
}
