using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormAutoModel.
	/// </summary>
	public class FormAutoModel : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBoxModel;
		private System.Windows.Forms.TextBox textBoxEngine;
		private System.Windows.Forms.Button buttonOk;
		private System.Windows.Forms.Button buttonCancel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		/// 
		private DbAutoModel	autoModel;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBoxAutoType;
		private System.Windows.Forms.Button buttonSelectAutoType;
		private System.Windows.Forms.TextBox textBoxGuarantyType;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button buttonSelectGuarantyType;

		private System.ComponentModel.Container components = null;

		public FormAutoModel(DbAutoModel source)
		{
			InitializeComponent();

			if(source == null)
			{
				autoModel = new DbAutoModel();
			}
			else
			{
				autoModel = new DbAutoModel(source);
			}
			textBoxModel.Text = autoModel.Model;
			textBoxEngine.Text = autoModel.Engine;
			textBoxAutoType.Text = autoModel.AutoTypeTxt;
			textBoxGuarantyType.Text = autoModel.GuarantyTypeTxt;
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
			this.textBoxModel = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textBoxEngine = new System.Windows.Forms.TextBox();
			this.buttonOk = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.textBoxAutoType = new System.Windows.Forms.TextBox();
			this.buttonSelectAutoType = new System.Windows.Forms.Button();
			this.textBoxGuarantyType = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.buttonSelectGuarantyType = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 16);
			this.label1.Name = "label1";
			this.label1.TabIndex = 0;
			this.label1.Text = "Модель";
			// 
			// textBoxModel
			// 
			this.textBoxModel.Location = new System.Drawing.Point(136, 16);
			this.textBoxModel.Name = "textBoxModel";
			this.textBoxModel.Size = new System.Drawing.Size(400, 23);
			this.textBoxModel.TabIndex = 1;
			this.textBoxModel.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 48);
			this.label2.Name = "label2";
			this.label2.TabIndex = 2;
			this.label2.Text = "Двигатель";
			// 
			// textBoxEngine
			// 
			this.textBoxEngine.Location = new System.Drawing.Point(136, 48);
			this.textBoxEngine.Name = "textBoxEngine";
			this.textBoxEngine.ReadOnly = true;
			this.textBoxEngine.Size = new System.Drawing.Size(400, 23);
			this.textBoxEngine.TabIndex = 3;
			this.textBoxEngine.Text = "";
			// 
			// buttonOk
			// 
			this.buttonOk.Location = new System.Drawing.Point(184, 160);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.TabIndex = 4;
			this.buttonOk.Text = "ОК";
			this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(264, 160);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.TabIndex = 5;
			this.buttonCancel.Text = "Cancel";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 80);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(112, 23);
			this.label3.TabIndex = 6;
			this.label3.Text = "Тип автомобиля";
			// 
			// textBoxAutoType
			// 
			this.textBoxAutoType.Location = new System.Drawing.Point(136, 80);
			this.textBoxAutoType.Name = "textBoxAutoType";
			this.textBoxAutoType.ReadOnly = true;
			this.textBoxAutoType.Size = new System.Drawing.Size(352, 23);
			this.textBoxAutoType.TabIndex = 7;
			this.textBoxAutoType.Text = "";
			// 
			// buttonSelectAutoType
			// 
			this.buttonSelectAutoType.Location = new System.Drawing.Point(488, 80);
			this.buttonSelectAutoType.Name = "buttonSelectAutoType";
			this.buttonSelectAutoType.Size = new System.Drawing.Size(24, 23);
			this.buttonSelectAutoType.TabIndex = 8;
			this.buttonSelectAutoType.Text = "...";
			this.buttonSelectAutoType.Click += new System.EventHandler(this.buttonSelectAutoType_Click);
			// 
			// textBoxGuarantyType
			// 
			this.textBoxGuarantyType.Location = new System.Drawing.Point(136, 112);
			this.textBoxGuarantyType.Name = "textBoxGuarantyType";
			this.textBoxGuarantyType.ReadOnly = true;
			this.textBoxGuarantyType.Size = new System.Drawing.Size(352, 23);
			this.textBoxGuarantyType.TabIndex = 9;
			this.textBoxGuarantyType.Text = "";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 112);
			this.label4.Name = "label4";
			this.label4.TabIndex = 10;
			this.label4.Text = "Тип гарантии";
			// 
			// buttonSelectGuarantyType
			// 
			this.buttonSelectGuarantyType.Location = new System.Drawing.Point(488, 112);
			this.buttonSelectGuarantyType.Name = "buttonSelectGuarantyType";
			this.buttonSelectGuarantyType.Size = new System.Drawing.Size(24, 23);
			this.buttonSelectGuarantyType.TabIndex = 11;
			this.buttonSelectGuarantyType.Text = "...";
			this.buttonSelectGuarantyType.Click += new System.EventHandler(this.buttonSelectGuarantyType_Click);
			// 
			// FormAutoModel
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(552, 187);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.buttonSelectGuarantyType,
																		  this.label4,
																		  this.textBoxGuarantyType,
																		  this.buttonSelectAutoType,
																		  this.textBoxAutoType,
																		  this.label3,
																		  this.buttonCancel,
																		  this.buttonOk,
																		  this.textBoxEngine,
																		  this.label2,
																		  this.textBoxModel,
																		  this.label1});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Name = "FormAutoModel";
			this.Text = "Модель автомобиля";
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonOk_Click(object sender, System.EventArgs e)
		{
			// Получение данных
			autoModel.Model = textBoxModel.Text;
			autoModel.Engine = textBoxEngine.Text;
			if(Db.ShowFaults()) return;
			if(autoModel.Write() != true) return;
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void buttonSelectAutoType_Click(object sender, System.EventArgs e)
		{
			// Выбор нового типа автомобиля для модели
			FormAutoTypeList dialog = new FormAutoTypeList();
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			autoModel.AutoType = dialog.SelectedAutoType;
			textBoxAutoType.Text = autoModel.AutoTypeTxt;
		}

		private void buttonSelectGuarantyType_Click(object sender, System.EventArgs e)
		{
			// Выбор типа гарантии автомобиля
			FormGuarantyTypeList dialog = new FormGuarantyTypeList(Db.ClickType.Select);
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			autoModel.GuarantyType		= dialog.GuarantyType;
			textBoxGuarantyType.Text	= autoModel.GuarantyTypeTxt;
		}

		public DbAutoModel AutoModel
		{
			get
			{
				return autoModel;
			}
		}
	}
}
