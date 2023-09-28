using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormQuestionnaire.
	/// </summary>
	public class FormQuestionnaire : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxPartnerTxt;
		private System.Windows.Forms.Button buttonSelectPartner;
		private System.Windows.Forms.TextBox textBoxPartner;
		private System.Windows.Forms.CheckBox checkBoxCash;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox checkBoxCredit;
		private System.Windows.Forms.CheckBox checkBoxTradeIn;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBoxComment;
		private System.Windows.Forms.Button buttonOk;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FormQuestionnaire()
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
			this.label1 = new System.Windows.Forms.Label();
			this.textBoxPartnerTxt = new System.Windows.Forms.TextBox();
			this.buttonSelectPartner = new System.Windows.Forms.Button();
			this.textBoxPartner = new System.Windows.Forms.TextBox();
			this.checkBoxCash = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this.checkBoxCredit = new System.Windows.Forms.CheckBox();
			this.checkBoxTradeIn = new System.Windows.Forms.CheckBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textBoxComment = new System.Windows.Forms.TextBox();
			this.buttonOk = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(24, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(72, 24);
			this.label1.TabIndex = 0;
			this.label1.Text = " À»≈Õ“";
			// 
			// textBoxPartnerTxt
			// 
			this.textBoxPartnerTxt.Location = new System.Drawing.Point(24, 48);
			this.textBoxPartnerTxt.Name = "textBoxPartnerTxt";
			this.textBoxPartnerTxt.ReadOnly = true;
			this.textBoxPartnerTxt.Size = new System.Drawing.Size(504, 23);
			this.textBoxPartnerTxt.TabIndex = 1;
			this.textBoxPartnerTxt.Text = "";
			// 
			// buttonSelectPartner
			// 
			this.buttonSelectPartner.Location = new System.Drawing.Point(528, 48);
			this.buttonSelectPartner.Name = "buttonSelectPartner";
			this.buttonSelectPartner.Size = new System.Drawing.Size(24, 23);
			this.buttonSelectPartner.TabIndex = 2;
			this.buttonSelectPartner.Text = "...";
			// 
			// textBoxPartner
			// 
			this.textBoxPartner.Location = new System.Drawing.Point(24, 88);
			this.textBoxPartner.Name = "textBoxPartner";
			this.textBoxPartner.Size = new System.Drawing.Size(528, 23);
			this.textBoxPartner.TabIndex = 3;
			this.textBoxPartner.Text = "";
			// 
			// checkBoxCash
			// 
			this.checkBoxCash.Location = new System.Drawing.Point(32, 160);
			this.checkBoxCash.Name = "checkBoxCash";
			this.checkBoxCash.Size = new System.Drawing.Size(120, 24);
			this.checkBoxCash.TabIndex = 4;
			this.checkBoxCash.Text = "100% ŒœÀ¿“¿";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(32, 128);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(232, 23);
			this.label2.TabIndex = 5;
			this.label2.Text = "‘Œ–Ã¿ œŒ ”œ » ¿¬“ŒÃŒ¡»Àﬂ";
			// 
			// checkBoxCredit
			// 
			this.checkBoxCredit.Location = new System.Drawing.Point(168, 160);
			this.checkBoxCredit.Name = "checkBoxCredit";
			this.checkBoxCredit.TabIndex = 6;
			this.checkBoxCredit.Text = " –≈ƒ»“";
			// 
			// checkBoxTradeIn
			// 
			this.checkBoxTradeIn.Location = new System.Drawing.Point(288, 160);
			this.checkBoxTradeIn.Name = "checkBoxTradeIn";
			this.checkBoxTradeIn.TabIndex = 7;
			this.checkBoxTradeIn.Text = "TRADE-IN";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(24, 192);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(112, 23);
			this.label3.TabIndex = 8;
			this.label3.Text = "œ–»Ã≈◊¿Õ»ﬂ";
			// 
			// textBoxComment
			// 
			this.textBoxComment.Location = new System.Drawing.Point(24, 216);
			this.textBoxComment.Name = "textBoxComment";
			this.textBoxComment.Size = new System.Drawing.Size(528, 23);
			this.textBoxComment.TabIndex = 9;
			this.textBoxComment.Text = "";
			// 
			// buttonOk
			// 
			this.buttonOk.Location = new System.Drawing.Point(248, 264);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.TabIndex = 10;
			this.buttonOk.Text = "OK";
			// 
			// FormQuestionnaire
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.ClientSize = new System.Drawing.Size(584, 301);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.buttonOk,
																		  this.textBoxComment,
																		  this.label3,
																		  this.checkBoxTradeIn,
																		  this.checkBoxCredit,
																		  this.label2,
																		  this.checkBoxCash,
																		  this.textBoxPartner,
																		  this.buttonSelectPartner,
																		  this.textBoxPartnerTxt,
																		  this.label1});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.Name = "FormQuestionnaire";
			this.Text = "¿ÌÍÂÚ‡ ÍÎËÂÌÚ‡";
			this.ResumeLayout(false);

		}
		#endregion
	}
}
