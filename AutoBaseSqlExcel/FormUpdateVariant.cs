using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormUpdateVariant.
	/// </summary>
	public class FormUpdateVariant : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBox_name;
		private System.Windows.Forms.TextBox textBox_description;
		private System.Windows.Forms.Button button_save;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		DtVariant	variant;
		DtModel		model;

		public FormUpdateVariant(long code_model, long code)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			if(code == 0)
			{
				// Ìîäåëü äëÿ êîòîğîé èùåì öâåò
				model	= DbSqlModel.Find(code_model);
				if(model == null)
				{
					button_save.Enabled = false;
					return;
				}
				variant = new DtVariant();
				variant.SetData("ÑÑÛËÊÀ_ÊÎÄ_ÀÂÒÎÌÎÁÈËÜ_ÌÎÄÅËÜ", (long)model.GetData("ÊÎÄ_ÀÂÒÎÌÎÁÈËÜ_ÌÎÄÅËÜ"));
			}
			else
			{
				DtVariant element = DbSqlVariant.Find(code);
				if(element == null)
				{
					button_save.Enabled = false;
					return;
				}
				model	= DbSqlModel.Find((long)element.GetData("ÑÑÛËÊÀ_ÊÎÄ_ÀÂÒÎÌÎÁÈËÜ_ÌÎÄÅËÜ"));
				if(model == null)
				{
					button_save.Enabled = false;
					return;
				}
				variant = new DtVariant(element);
			}
			this.Text = this.Text + " ìîäåëü " + (string)model.GetData("ÌÎÄÅËÜ");
			textBox_name.Text			= (string)variant.GetData("ÈÑÏÎËÍÅÍÈÅ_ÍÀÈÌÅÍÎÂÀÍÈÅ");
			textBox_description.Text	= (string)variant.GetData("ÈÑÏÎËÍÅÍÈÅ_ÎÏÈÑÀÍÈÅ");
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
			this.textBox_name = new System.Windows.Forms.TextBox();
			this.textBox_description = new System.Windows.Forms.TextBox();
			this.button_save = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 16);
			this.label1.Name = "label1";
			this.label1.TabIndex = 0;
			this.label1.Text = "Èñïîëíåíèå";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 40);
			this.label2.Name = "label2";
			this.label2.TabIndex = 1;
			this.label2.Text = "Îïèñàíèå";
			// 
			// textBox_name
			// 
			this.textBox_name.Location = new System.Drawing.Point(112, 16);
			this.textBox_name.Name = "textBox_name";
			this.textBox_name.Size = new System.Drawing.Size(344, 23);
			this.textBox_name.TabIndex = 2;
			this.textBox_name.Text = "";
			// 
			// textBox_description
			// 
			this.textBox_description.Location = new System.Drawing.Point(112, 40);
			this.textBox_description.Name = "textBox_description";
			this.textBox_description.Size = new System.Drawing.Size(344, 23);
			this.textBox_description.TabIndex = 3;
			this.textBox_description.Text = "";
			// 
			// button_save
			// 
			this.button_save.Location = new System.Drawing.Point(368, 72);
			this.button_save.Name = "button_save";
			this.button_save.Size = new System.Drawing.Size(88, 23);
			this.button_save.TabIndex = 4;
			this.button_save.Text = "Ñîõğàíèòü";
			this.button_save.Click += new System.EventHandler(this.button_save_Click);
			// 
			// FormUpdateVariant
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.ClientSize = new System.Drawing.Size(464, 103);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.button_save,
																		  this.textBox_description,
																		  this.textBox_name,
																		  this.label2,
																		  this.label1});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "FormUpdateVariant";
			this.Text = "Èñïîëíåíèå:";
			this.ResumeLayout(false);

		}
		#endregion

		private void button_save_Click(object sender, System.EventArgs e)
		{
			// Ñîõğàíÿåì èçìåíåíèÿ
			// Ñîõğàíÿåì âàğèàíò
			variant.SetData("ÈÑÏÎËÍÅÍÈÅ_ÍÀÈÌÅÍÎÂÀÍÈÅ", textBox_name.Text);
			variant.SetData("ÈÑÏÎËÍÅÍÈÅ_ÎÏÈÑÀÍÈÅ", textBox_description.Text);

			if(variant.CheckData("ÈÑÏÎËÍÅÍÈÅ_ÍÀÈÌÅÍÎÂÀÍÈÅ") == false) return;
			if(variant.CheckData("ÑÑÛËÊÀ_ÊÎÄ_ÀÂÒÎÌÎÁÈËÜ_ÌÎÄÅËÜ") == false) return;

			if((long)variant.GetData("ÊÎÄ_ÀÂÒÎÌÎÁÈËÜ_ÈÑÏÎËÍÅÍÈÅ")==0)
			{
				DtVariant result = DbSqlVariant.Insert(variant);
				if(result == null) return;
				variant = result;
			}
			else
			{
				if(DbSqlVariant.Update(variant) == false) return;
			}
			DialogResult = DialogResult.OK;
			this.Close();
		}

		public DtVariant Variant
		{
			get
			{
				return variant;
			}
		}
	}
}
