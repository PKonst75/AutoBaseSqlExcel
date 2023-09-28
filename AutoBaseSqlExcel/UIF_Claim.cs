using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for UIF_Claim.
	/// </summary>
	public class UIF_Claim : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ComboBox comboBox_claim_type;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button button_ok;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public UIF_Claim()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			// Заполнение типа заявок на ремонт
			comboBox_claim_type.Items.Insert(0, (object)"Заявка на ремонт");
			comboBox_claim_type.Items.Insert(1, (object)"Заявка о дефекте");
			comboBox_claim_type.Items.Insert(2, (object)"Заявка на диагностику");
			comboBox_claim_type.Items.Insert(3, (object)"Жалоба");
			comboBox_claim_type.Items.Insert(4, (object)"Дефект выявленный при осмотре");
			comboBox_claim_type.Items.Insert(5, (object)"Дефект выявленный при диагностике");
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
			this.comboBox_claim_type = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.button_ok = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// comboBox_claim_type
			// 
			this.comboBox_claim_type.Location = new System.Drawing.Point(152, 16);
			this.comboBox_claim_type.Name = "comboBox_claim_type";
			this.comboBox_claim_type.Size = new System.Drawing.Size(288, 21);
			this.comboBox_claim_type.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(128, 24);
			this.label1.TabIndex = 1;
			this.label1.Text = "Тип заявки";
			// 
			// button_ok
			// 
			this.button_ok.Location = new System.Drawing.Point(440, 240);
			this.button_ok.Name = "button_ok";
			this.button_ok.TabIndex = 2;
			this.button_ok.Text = "ОК";
			this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
			// 
			// UIF_Claim
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(536, 273);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.button_ok,
																		  this.label1,
																		  this.comboBox_claim_type});
			this.Name = "UIF_Claim";
			this.Text = "Заявка";
			this.ResumeLayout(false);

		}
		#endregion

		private void button_ok_Click(object sender, System.EventArgs e)
		{
		
		}
	}
}
