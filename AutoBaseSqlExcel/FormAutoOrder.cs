using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormAutoOrder.
	/// </summary>
	public class FormAutoOrder : System.Windows.Forms.Form
	{
		private System.Windows.Forms.CheckBox checkBoxAutoNew;
		private System.Windows.Forms.CheckBox checkBoxAutoUsed;
		private System.Windows.Forms.CheckBox checkBoxAutoRus;
		private System.Windows.Forms.CheckBox checkBoxAutoImport;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBoxBrandTxt;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBoxModelTxt;
		private System.Windows.Forms.Button buttonSelectBrand;
		private System.Windows.Forms.Button buttonSelectModel;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textBoxSubModelTxt;
		private System.Windows.Forms.Button buttonSelectSubModel;
		private System.Windows.Forms.TextBox textBoxColorTxt;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button buttonSelectColor;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox textBoxComplectTxt;
		private System.Windows.Forms.Button buttonSelectComplect;
		private System.Windows.Forms.TextBox textBoxBody;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox textBoxColor;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox textBoxExpand;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.CheckBox checkBoxTransmissionManual;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.CheckBox checkBoxTransmissionAuto;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.CheckBox checkBoxAbs;
		private System.Windows.Forms.CheckBox checkBoxAirBag;
		private System.Windows.Forms.CheckBox checkBoxConditioner;
		private System.Windows.Forms.TextBox textBoxPriceFrom;
		private System.Windows.Forms.TextBox textBoxPriceTo;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label14;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FormAutoOrder()
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
			this.checkBoxAutoNew = new System.Windows.Forms.CheckBox();
			this.checkBoxAutoUsed = new System.Windows.Forms.CheckBox();
			this.checkBoxAutoRus = new System.Windows.Forms.CheckBox();
			this.checkBoxAutoImport = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textBoxBrandTxt = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.textBoxModelTxt = new System.Windows.Forms.TextBox();
			this.buttonSelectBrand = new System.Windows.Forms.Button();
			this.buttonSelectModel = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.textBoxSubModelTxt = new System.Windows.Forms.TextBox();
			this.buttonSelectSubModel = new System.Windows.Forms.Button();
			this.textBoxColorTxt = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.buttonSelectColor = new System.Windows.Forms.Button();
			this.label7 = new System.Windows.Forms.Label();
			this.textBoxComplectTxt = new System.Windows.Forms.TextBox();
			this.buttonSelectComplect = new System.Windows.Forms.Button();
			this.textBoxBody = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.textBoxColor = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.textBoxExpand = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.checkBoxTransmissionManual = new System.Windows.Forms.CheckBox();
			this.label11 = new System.Windows.Forms.Label();
			this.checkBoxTransmissionAuto = new System.Windows.Forms.CheckBox();
			this.label12 = new System.Windows.Forms.Label();
			this.checkBoxAbs = new System.Windows.Forms.CheckBox();
			this.checkBoxAirBag = new System.Windows.Forms.CheckBox();
			this.checkBoxConditioner = new System.Windows.Forms.CheckBox();
			this.textBoxPriceFrom = new System.Windows.Forms.TextBox();
			this.textBoxPriceTo = new System.Windows.Forms.TextBox();
			this.label13 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// checkBoxAutoNew
			// 
			this.checkBoxAutoNew.Location = new System.Drawing.Point(16, 16);
			this.checkBoxAutoNew.Name = "checkBoxAutoNew";
			this.checkBoxAutoNew.Size = new System.Drawing.Size(80, 24);
			this.checkBoxAutoNew.TabIndex = 0;
			this.checkBoxAutoNew.Text = "ÕŒ¬€…";
			// 
			// checkBoxAutoUsed
			// 
			this.checkBoxAutoUsed.Location = new System.Drawing.Point(120, 16);
			this.checkBoxAutoUsed.Name = "checkBoxAutoUsed";
			this.checkBoxAutoUsed.Size = new System.Drawing.Size(144, 24);
			this.checkBoxAutoUsed.TabIndex = 1;
			this.checkBoxAutoUsed.Text = "œŒƒ≈–∆¿ÕÕ€…";
			// 
			// checkBoxAutoRus
			// 
			this.checkBoxAutoRus.Location = new System.Drawing.Point(280, 16);
			this.checkBoxAutoRus.Name = "checkBoxAutoRus";
			this.checkBoxAutoRus.Size = new System.Drawing.Size(152, 24);
			this.checkBoxAutoRus.TabIndex = 2;
			this.checkBoxAutoRus.Text = "Œ“≈◊≈—“¬≈ÕÕ€…";
			// 
			// checkBoxAutoImport
			// 
			this.checkBoxAutoImport.Location = new System.Drawing.Point(464, 16);
			this.checkBoxAutoImport.Name = "checkBoxAutoImport";
			this.checkBoxAutoImport.Size = new System.Drawing.Size(120, 24);
			this.checkBoxAutoImport.TabIndex = 3;
			this.checkBoxAutoImport.Text = "»ÕŒÃ¿– ¿";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(96, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(16, 23);
			this.label1.TabIndex = 4;
			this.label1.Text = "/";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(440, 16);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(16, 23);
			this.label2.TabIndex = 5;
			this.label2.Text = "/";
			// 
			// textBoxBrandTxt
			// 
			this.textBoxBrandTxt.Location = new System.Drawing.Point(152, 48);
			this.textBoxBrandTxt.Name = "textBoxBrandTxt";
			this.textBoxBrandTxt.ReadOnly = true;
			this.textBoxBrandTxt.Size = new System.Drawing.Size(352, 23);
			this.textBoxBrandTxt.TabIndex = 6;
			this.textBoxBrandTxt.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(16, 48);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(64, 23);
			this.label3.TabIndex = 7;
			this.label3.Text = "¡–≈Õƒ";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(16, 80);
			this.label4.Name = "label4";
			this.label4.TabIndex = 8;
			this.label4.Text = "ÃŒƒ≈À‹";
			// 
			// textBoxModelTxt
			// 
			this.textBoxModelTxt.Location = new System.Drawing.Point(152, 80);
			this.textBoxModelTxt.Name = "textBoxModelTxt";
			this.textBoxModelTxt.ReadOnly = true;
			this.textBoxModelTxt.Size = new System.Drawing.Size(352, 23);
			this.textBoxModelTxt.TabIndex = 9;
			this.textBoxModelTxt.Text = "";
			// 
			// buttonSelectBrand
			// 
			this.buttonSelectBrand.Location = new System.Drawing.Point(504, 48);
			this.buttonSelectBrand.Name = "buttonSelectBrand";
			this.buttonSelectBrand.Size = new System.Drawing.Size(24, 23);
			this.buttonSelectBrand.TabIndex = 10;
			this.buttonSelectBrand.Text = "...";
			// 
			// buttonSelectModel
			// 
			this.buttonSelectModel.Location = new System.Drawing.Point(504, 80);
			this.buttonSelectModel.Name = "buttonSelectModel";
			this.buttonSelectModel.Size = new System.Drawing.Size(24, 23);
			this.buttonSelectModel.TabIndex = 11;
			this.buttonSelectModel.Text = "...";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(16, 112);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(136, 23);
			this.label5.TabIndex = 12;
			this.label5.Text = "—œ≈÷»‘» ¿÷»ﬂ";
			// 
			// textBoxSubModelTxt
			// 
			this.textBoxSubModelTxt.Location = new System.Drawing.Point(152, 112);
			this.textBoxSubModelTxt.Name = "textBoxSubModelTxt";
			this.textBoxSubModelTxt.ReadOnly = true;
			this.textBoxSubModelTxt.Size = new System.Drawing.Size(352, 23);
			this.textBoxSubModelTxt.TabIndex = 13;
			this.textBoxSubModelTxt.Text = "";
			// 
			// buttonSelectSubModel
			// 
			this.buttonSelectSubModel.Location = new System.Drawing.Point(504, 112);
			this.buttonSelectSubModel.Name = "buttonSelectSubModel";
			this.buttonSelectSubModel.Size = new System.Drawing.Size(24, 23);
			this.buttonSelectSubModel.TabIndex = 14;
			this.buttonSelectSubModel.Text = "...";
			// 
			// textBoxColorTxt
			// 
			this.textBoxColorTxt.Location = new System.Drawing.Point(152, 144);
			this.textBoxColorTxt.Name = "textBoxColorTxt";
			this.textBoxColorTxt.ReadOnly = true;
			this.textBoxColorTxt.Size = new System.Drawing.Size(352, 23);
			this.textBoxColorTxt.TabIndex = 15;
			this.textBoxColorTxt.Text = "";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(16, 144);
			this.label6.Name = "label6";
			this.label6.TabIndex = 16;
			this.label6.Text = "÷¬≈“";
			// 
			// buttonSelectColor
			// 
			this.buttonSelectColor.Location = new System.Drawing.Point(504, 144);
			this.buttonSelectColor.Name = "buttonSelectColor";
			this.buttonSelectColor.Size = new System.Drawing.Size(24, 23);
			this.buttonSelectColor.TabIndex = 17;
			this.buttonSelectColor.Text = "...";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(16, 176);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(128, 23);
			this.label7.TabIndex = 18;
			this.label7.Text = " ŒÃœÀ≈ “¿÷»ﬂ";
			// 
			// textBoxComplectTxt
			// 
			this.textBoxComplectTxt.Location = new System.Drawing.Point(152, 176);
			this.textBoxComplectTxt.Name = "textBoxComplectTxt";
			this.textBoxComplectTxt.ReadOnly = true;
			this.textBoxComplectTxt.Size = new System.Drawing.Size(352, 23);
			this.textBoxComplectTxt.TabIndex = 19;
			this.textBoxComplectTxt.Text = "";
			// 
			// buttonSelectComplect
			// 
			this.buttonSelectComplect.Location = new System.Drawing.Point(504, 176);
			this.buttonSelectComplect.Name = "buttonSelectComplect";
			this.buttonSelectComplect.Size = new System.Drawing.Size(24, 24);
			this.buttonSelectComplect.TabIndex = 20;
			this.buttonSelectComplect.Text = "...";
			// 
			// textBoxBody
			// 
			this.textBoxBody.Location = new System.Drawing.Point(152, 224);
			this.textBoxBody.Name = "textBoxBody";
			this.textBoxBody.Size = new System.Drawing.Size(352, 23);
			this.textBoxBody.TabIndex = 21;
			this.textBoxBody.Text = "";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(16, 224);
			this.label8.Name = "label8";
			this.label8.TabIndex = 22;
			this.label8.Text = "“»œ  ”«Œ¬¿";
			// 
			// textBoxColor
			// 
			this.textBoxColor.Location = new System.Drawing.Point(152, 256);
			this.textBoxColor.Name = "textBoxColor";
			this.textBoxColor.Size = new System.Drawing.Size(352, 23);
			this.textBoxColor.TabIndex = 23;
			this.textBoxColor.Text = "";
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(16, 256);
			this.label9.Name = "label9";
			this.label9.TabIndex = 24;
			this.label9.Text = "÷¬≈“";
			// 
			// textBoxExpand
			// 
			this.textBoxExpand.Location = new System.Drawing.Point(152, 288);
			this.textBoxExpand.Name = "textBoxExpand";
			this.textBoxExpand.Size = new System.Drawing.Size(352, 23);
			this.textBoxExpand.TabIndex = 25;
			this.textBoxExpand.Text = "";
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(16, 288);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(136, 24);
			this.label10.TabIndex = 26;
			this.label10.Text = "ƒŒœŒÀÕ»“≈À‹ÕŒ";
			// 
			// checkBoxTransmissionManual
			// 
			this.checkBoxTransmissionManual.Location = new System.Drawing.Point(200, 328);
			this.checkBoxTransmissionManual.Name = "checkBoxTransmissionManual";
			this.checkBoxTransmissionManual.TabIndex = 27;
			this.checkBoxTransmissionManual.Text = "Ã≈’¿Õ» ¿";
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(312, 328);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(16, 23);
			this.label11.TabIndex = 28;
			this.label11.Text = "/";
			// 
			// checkBoxTransmissionAuto
			// 
			this.checkBoxTransmissionAuto.Location = new System.Drawing.Point(336, 328);
			this.checkBoxTransmissionAuto.Name = "checkBoxTransmissionAuto";
			this.checkBoxTransmissionAuto.TabIndex = 29;
			this.checkBoxTransmissionAuto.Text = "¿¬“ŒÃ¿“";
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(16, 328);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(152, 23);
			this.label12.TabIndex = 30;
			this.label12.Text = " Œ–Œ¡ ¿ œ≈–≈ƒ¿◊";
			// 
			// checkBoxAbs
			// 
			this.checkBoxAbs.Location = new System.Drawing.Point(16, 360);
			this.checkBoxAbs.Name = "checkBoxAbs";
			this.checkBoxAbs.Size = new System.Drawing.Size(64, 24);
			this.checkBoxAbs.TabIndex = 31;
			this.checkBoxAbs.Text = "¿¡—";
			// 
			// checkBoxAirBag
			// 
			this.checkBoxAirBag.Location = new System.Drawing.Point(128, 360);
			this.checkBoxAirBag.Name = "checkBoxAirBag";
			this.checkBoxAirBag.Size = new System.Drawing.Size(224, 24);
			this.checkBoxAirBag.TabIndex = 32;
			this.checkBoxAirBag.Text = "œŒƒ”ÿ » ¡≈«Œœ¿—ÕŒ—“»";
			// 
			// checkBoxConditioner
			// 
			this.checkBoxConditioner.Location = new System.Drawing.Point(368, 360);
			this.checkBoxConditioner.Name = "checkBoxConditioner";
			this.checkBoxConditioner.Size = new System.Drawing.Size(144, 24);
			this.checkBoxConditioner.TabIndex = 33;
			this.checkBoxConditioner.Text = " ŒÕƒ»÷»ŒÕ≈–";
			// 
			// textBoxPriceFrom
			// 
			this.textBoxPriceFrom.Location = new System.Drawing.Point(248, 392);
			this.textBoxPriceFrom.Name = "textBoxPriceFrom";
			this.textBoxPriceFrom.TabIndex = 34;
			this.textBoxPriceFrom.Text = "";
			// 
			// textBoxPriceTo
			// 
			this.textBoxPriceTo.Location = new System.Drawing.Point(408, 392);
			this.textBoxPriceTo.Name = "textBoxPriceTo";
			this.textBoxPriceTo.TabIndex = 35;
			this.textBoxPriceTo.Text = "";
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(16, 400);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(184, 23);
			this.label13.TabIndex = 36;
			this.label13.Text = "÷≈ÕŒ¬¿ﬂ  ¿“≈√Œ–»ﬂ Œ“";
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(368, 400);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(32, 23);
			this.label14.TabIndex = 37;
			this.label14.Text = "ƒŒ";
			// 
			// FormAutoOrder
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.ClientSize = new System.Drawing.Size(624, 437);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.label14,
																		  this.label13,
																		  this.textBoxPriceTo,
																		  this.textBoxPriceFrom,
																		  this.checkBoxConditioner,
																		  this.checkBoxAirBag,
																		  this.checkBoxAbs,
																		  this.label12,
																		  this.checkBoxTransmissionAuto,
																		  this.label11,
																		  this.checkBoxTransmissionManual,
																		  this.label10,
																		  this.textBoxExpand,
																		  this.label9,
																		  this.textBoxColor,
																		  this.label8,
																		  this.textBoxBody,
																		  this.buttonSelectComplect,
																		  this.textBoxComplectTxt,
																		  this.label7,
																		  this.buttonSelectColor,
																		  this.label6,
																		  this.textBoxColorTxt,
																		  this.buttonSelectSubModel,
																		  this.textBoxSubModelTxt,
																		  this.label5,
																		  this.buttonSelectModel,
																		  this.buttonSelectBrand,
																		  this.textBoxModelTxt,
																		  this.label4,
																		  this.label3,
																		  this.textBoxBrandTxt,
																		  this.label2,
																		  this.label1,
																		  this.checkBoxAutoImport,
																		  this.checkBoxAutoRus,
																		  this.checkBoxAutoUsed,
																		  this.checkBoxAutoNew});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.Name = "FormAutoOrder";
			this.Text = "FormAutoOrder";
			this.ResumeLayout(false);

		}
		#endregion

	}
}
