using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormAutoSubmodel.
	/// </summary>
	public class FormAutoSubmodel : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox textBoxCodeModelTxt;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxCodeEngineTxt;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBoxEngineType;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button buttonSelectEngineType;
		private System.Windows.Forms.TextBox textBoxTransmissionType;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button buttonSelectTransmissionType;
		private System.Windows.Forms.ComboBox comboBoxBody;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.CheckBox checkBoxFourWeelDrive;
		private System.Windows.Forms.TextBox textBoxProductYearStart;
		private System.Windows.Forms.TextBox textBoxProductYearEnd;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Button buttonOk;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		DbAutoModel			autoModel;
		DbAutoSubmodel		autoSubmodel;

		public FormAutoSubmodel(DbAutoModel autoModelSrc, DbAutoSubmodel autoSubmodelSrc, string srcCodeSubModel)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			autoModel		= new DbAutoModel(autoModelSrc);
			if(autoSubmodelSrc != null)
				autoSubmodel	= new DbAutoSubmodel(autoSubmodelSrc);
			else
			{
				autoSubmodel				= new DbAutoSubmodel(autoModel);
				autoSubmodel.CodeModelTxt	= srcCodeSubModel;
			}

			// Установка значений
			this.Text			= "Подмодель для " + autoModel.Model;

			textBoxCodeModelTxt.Text			= autoSubmodel.CodeModelTxt;
			textBoxCodeEngineTxt.Text			= autoSubmodel.CodeEngineTxt;
			//int i = comboBoxBody.FindStringExact(autoSubmodel.Body);
			comboBoxBody.Text					= autoSubmodel.Body;
			checkBoxFourWeelDrive.Checked		= autoSubmodel.FourWeelDrive;
			textBoxProductYearStart.Text		= autoSubmodel.ProductYearStartTxt;
			textBoxProductYearEnd.Text			= autoSubmodel.ProductYearEndTxt;
			textBoxEngineType.Text				= autoSubmodel.EngineTypeTxt;
			textBoxTransmissionType.Text		= autoSubmodel.TransmissionTypeTxt;
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
			this.textBoxCodeModelTxt = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.textBoxCodeEngineTxt = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textBoxEngineType = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.buttonSelectEngineType = new System.Windows.Forms.Button();
			this.textBoxTransmissionType = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.buttonSelectTransmissionType = new System.Windows.Forms.Button();
			this.comboBoxBody = new System.Windows.Forms.ComboBox();
			this.label5 = new System.Windows.Forms.Label();
			this.checkBoxFourWeelDrive = new System.Windows.Forms.CheckBox();
			this.textBoxProductYearStart = new System.Windows.Forms.TextBox();
			this.textBoxProductYearEnd = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.buttonOk = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// textBoxCodeModelTxt
			// 
			this.textBoxCodeModelTxt.Location = new System.Drawing.Point(128, 16);
			this.textBoxCodeModelTxt.Name = "textBoxCodeModelTxt";
			this.textBoxCodeModelTxt.Size = new System.Drawing.Size(376, 23);
			this.textBoxCodeModelTxt.TabIndex = 0;
			this.textBoxCodeModelTxt.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 16);
			this.label1.Name = "label1";
			this.label1.TabIndex = 1;
			this.label1.Text = "Код модели";
			// 
			// textBoxCodeEngineTxt
			// 
			this.textBoxCodeEngineTxt.Location = new System.Drawing.Point(128, 48);
			this.textBoxCodeEngineTxt.Name = "textBoxCodeEngineTxt";
			this.textBoxCodeEngineTxt.Size = new System.Drawing.Size(376, 23);
			this.textBoxCodeEngineTxt.TabIndex = 2;
			this.textBoxCodeEngineTxt.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 48);
			this.label2.Name = "label2";
			this.label2.TabIndex = 3;
			this.label2.Text = "Код двигателя";
			// 
			// textBoxEngineType
			// 
			this.textBoxEngineType.Location = new System.Drawing.Point(128, 80);
			this.textBoxEngineType.Name = "textBoxEngineType";
			this.textBoxEngineType.ReadOnly = true;
			this.textBoxEngineType.Size = new System.Drawing.Size(376, 23);
			this.textBoxEngineType.TabIndex = 4;
			this.textBoxEngineType.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(16, 80);
			this.label3.Name = "label3";
			this.label3.TabIndex = 5;
			this.label3.Text = "Двигатель";
			// 
			// buttonSelectEngineType
			// 
			this.buttonSelectEngineType.Location = new System.Drawing.Point(504, 80);
			this.buttonSelectEngineType.Name = "buttonSelectEngineType";
			this.buttonSelectEngineType.Size = new System.Drawing.Size(24, 23);
			this.buttonSelectEngineType.TabIndex = 6;
			this.buttonSelectEngineType.Text = "...";
			this.buttonSelectEngineType.Click += new System.EventHandler(this.buttonSelectEngineType_Click);
			// 
			// textBoxTransmissionType
			// 
			this.textBoxTransmissionType.Location = new System.Drawing.Point(128, 112);
			this.textBoxTransmissionType.Name = "textBoxTransmissionType";
			this.textBoxTransmissionType.ReadOnly = true;
			this.textBoxTransmissionType.Size = new System.Drawing.Size(376, 23);
			this.textBoxTransmissionType.TabIndex = 7;
			this.textBoxTransmissionType.Text = "";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(16, 112);
			this.label4.Name = "label4";
			this.label4.TabIndex = 8;
			this.label4.Text = "КПП";
			// 
			// buttonSelectTransmissionType
			// 
			this.buttonSelectTransmissionType.Location = new System.Drawing.Point(504, 112);
			this.buttonSelectTransmissionType.Name = "buttonSelectTransmissionType";
			this.buttonSelectTransmissionType.Size = new System.Drawing.Size(24, 24);
			this.buttonSelectTransmissionType.TabIndex = 9;
			this.buttonSelectTransmissionType.Text = "...";
			this.buttonSelectTransmissionType.Click += new System.EventHandler(this.buttonSelectTransmissionType_Click);
			// 
			// comboBoxBody
			// 
			this.comboBoxBody.Items.AddRange(new object[] {
															  "седан",
															  "хетчбек 3 дверный",
															  "хетчбек 5 дверный",
															  "универсал",
															  "универсал 3 дверный",
															  "универсал 5 дверный",
															  "джип",
															  "минивен"});
			this.comboBoxBody.Location = new System.Drawing.Point(128, 144);
			this.comboBoxBody.Name = "comboBoxBody";
			this.comboBoxBody.Size = new System.Drawing.Size(184, 24);
			this.comboBoxBody.TabIndex = 10;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(16, 144);
			this.label5.Name = "label5";
			this.label5.TabIndex = 11;
			this.label5.Text = "Кузов";
			// 
			// checkBoxFourWeelDrive
			// 
			this.checkBoxFourWeelDrive.Location = new System.Drawing.Point(128, 176);
			this.checkBoxFourWeelDrive.Name = "checkBoxFourWeelDrive";
			this.checkBoxFourWeelDrive.Size = new System.Drawing.Size(200, 24);
			this.checkBoxFourWeelDrive.TabIndex = 12;
			this.checkBoxFourWeelDrive.Text = "Полный привод";
			// 
			// textBoxProductYearStart
			// 
			this.textBoxProductYearStart.Location = new System.Drawing.Point(128, 208);
			this.textBoxProductYearStart.Name = "textBoxProductYearStart";
			this.textBoxProductYearStart.Size = new System.Drawing.Size(88, 23);
			this.textBoxProductYearStart.TabIndex = 13;
			this.textBoxProductYearStart.Text = "";
			// 
			// textBoxProductYearEnd
			// 
			this.textBoxProductYearEnd.Location = new System.Drawing.Point(248, 208);
			this.textBoxProductYearEnd.Name = "textBoxProductYearEnd";
			this.textBoxProductYearEnd.Size = new System.Drawing.Size(88, 23);
			this.textBoxProductYearEnd.TabIndex = 14;
			this.textBoxProductYearEnd.Text = "";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(16, 208);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(112, 23);
			this.label6.TabIndex = 15;
			this.label6.Text = "Года выпуска с";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(224, 208);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(24, 24);
			this.label7.TabIndex = 16;
			this.label7.Text = "по";
			// 
			// buttonOk
			// 
			this.buttonOk.Location = new System.Drawing.Point(232, 240);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.TabIndex = 17;
			this.buttonOk.Text = "ОК";
			this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
			// 
			// FormAutoSubmodel
			// 
			this.AcceptButton = this.buttonOk;
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.ClientSize = new System.Drawing.Size(560, 277);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.buttonOk,
																		  this.label7,
																		  this.label6,
																		  this.textBoxProductYearEnd,
																		  this.textBoxProductYearStart,
																		  this.checkBoxFourWeelDrive,
																		  this.label5,
																		  this.comboBoxBody,
																		  this.buttonSelectTransmissionType,
																		  this.label4,
																		  this.textBoxTransmissionType,
																		  this.buttonSelectEngineType,
																		  this.label3,
																		  this.textBoxEngineType,
																		  this.label2,
																		  this.textBoxCodeEngineTxt,
																		  this.label1,
																		  this.textBoxCodeModelTxt});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.Name = "FormAutoSubmodel";
			this.Text = "Подмодель";
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonOk_Click(object sender, System.EventArgs e)
		{
			// Проверяем выбрали ли тип двигателя и тип коробки передач
			autoSubmodel.Check();
			// Пытаемся записать полученную подмодель
			autoSubmodel.CodeModelTxt			= textBoxCodeModelTxt.Text;
			autoSubmodel.CodeEngineTxt			= textBoxCodeEngineTxt.Text;
			if(comboBoxBody.SelectedItem != null)
				autoSubmodel.Body				= comboBoxBody.SelectedItem.ToString();
			else
				autoSubmodel.Body				= "";
			autoSubmodel.FourWeelDrive			= checkBoxFourWeelDrive.Checked;
			autoSubmodel.ProductYearStartTxt	= textBoxProductYearStart.Text;
			autoSubmodel.ProductYearEndTxt		= textBoxProductYearEnd.Text;

			if(Db.ShowFaults()) return;

			if(autoSubmodel.Write() != true)
			{
				Db.ShowFaults();
				return;
			}
			this.DialogResult = DialogResult.OK;
			this.Close();
			return;
		}

		private void buttonSelectEngineType_Click(object sender, System.EventArgs e)
		{
			// Вызов диалога получения типа двигателя
			FormEngineTypeList dialog = new FormEngineTypeList(Db.ClickType.Select);
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			autoSubmodel.EngineType	= dialog.EngineType;
			textBoxEngineType.Text	= autoSubmodel.EngineTypeTxt;
		}

		private void buttonSelectTransmissionType_Click(object sender, System.EventArgs e)
		{
			// Вызов диалога получения типа коробки передач
			FormTransmissionTypeList dialog = new FormTransmissionTypeList(Db.ClickType.Select);
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			autoSubmodel.TransmissionType	= dialog.TransmissionType;
			textBoxTransmissionType.Text	= autoSubmodel.TransmissionTypeTxt;
		}

		public DbAutoSubmodel AutoSubmodel
		{
			get
			{
				return autoSubmodel;
			}
		}
	}
}
