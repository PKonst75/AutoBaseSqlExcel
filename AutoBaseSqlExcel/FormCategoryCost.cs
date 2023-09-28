using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormCategoryCost.
	/// </summary>
	public class FormCategoryCost : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox textBoxName;
		private System.Windows.Forms.TextBox textBoxMark;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button buttonOk;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private DbCategoryCost	categoryCost;

		public FormCategoryCost(DbCategoryCost src)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			if(src == null)
			{
				categoryCost	= new DbCategoryCost();
				this.Text		= "Новая ценовая категория";
			}
			else
			{
				categoryCost	= new DbCategoryCost(src);
				this.Text		= "Ценовая категория - " + categoryCost.Name;
			}
			textBoxName.Text	= categoryCost.Name;
			textBoxMark.Text	= categoryCost.Mark;
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
			this.textBoxName = new System.Windows.Forms.TextBox();
			this.textBoxMark = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.buttonOk = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// textBoxName
			// 
			this.textBoxName.Location = new System.Drawing.Point(152, 16);
			this.textBoxName.Name = "textBoxName";
			this.textBoxName.Size = new System.Drawing.Size(264, 23);
			this.textBoxName.TabIndex = 0;
			this.textBoxName.Text = "";
			// 
			// textBoxMark
			// 
			this.textBoxMark.Location = new System.Drawing.Point(152, 48);
			this.textBoxMark.Name = "textBoxMark";
			this.textBoxMark.Size = new System.Drawing.Size(264, 23);
			this.textBoxMark.TabIndex = 1;
			this.textBoxMark.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(112, 23);
			this.label1.TabIndex = 2;
			this.label1.Text = "Наименование";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 48);
			this.label2.Name = "label2";
			this.label2.TabIndex = 3;
			this.label2.Text = "Обозначение";
			// 
			// buttonOk
			// 
			this.buttonOk.Location = new System.Drawing.Point(184, 80);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.TabIndex = 4;
			this.buttonOk.Text = "ОК";
			this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
			// 
			// FormCategoryCost
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.ClientSize = new System.Drawing.Size(480, 117);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.buttonOk,
																		  this.label2,
																		  this.label1,
																		  this.textBoxMark,
																		  this.textBoxName});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.Name = "FormCategoryCost";
			this.Text = "Ценовая категория";
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonOk_Click(object sender, System.EventArgs e)
		{
			// Добавление/Изменение ценовой категории
			categoryCost.Name		= textBoxName.Text;
			categoryCost.Mark		= textBoxMark.Text;

			if(Db.ShowFaults() == true) return;		//  Накосячили с данными

			if(categoryCost.Write() == false) return;
			this.DialogResult	= DialogResult.OK;
			this.Close();
		}

		public DbCategoryCost CategoryCost
		{
			get
			{
				return categoryCost;
			}
		}
	}
}
