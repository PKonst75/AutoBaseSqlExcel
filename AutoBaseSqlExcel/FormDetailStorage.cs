using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormDetailStorage.
	/// </summary>
	public class FormDetailStorage : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox textBoxDetail;
		private System.Windows.Forms.Button buttonSelectDetail;
		private System.Windows.Forms.Label label1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button buttonOk;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBoxUnits;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBoxStorageGroup;
		private System.Windows.Forms.Button buttonSelectStorageGroup;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textBoxMinQuontity;
		private System.Windows.Forms.TextBox textBoxDescription;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textBoxNds;
		private System.Windows.Forms.CheckBox checkBox_liquid;
		private System.Windows.Forms.TextBox textBox_name;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox textBox_number;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox textBox_1C;

		DbDetailStorage detailStorage;

		public FormDetailStorage(DbDetailStorage sourceDetailStorage)
		{
			InitializeComponent();

			if(sourceDetailStorage == null)
			{
				detailStorage = new DbDetailStorage();
				detailStorage.Units = "шт";
			}
			else
			{
				detailStorage = new DbDetailStorage(sourceDetailStorage);
				// Отключаем возможнось повторного выбора детали
				buttonSelectDetail.Enabled = false;
			}

			textBoxDetail.Text			= detailStorage.DetailName;
			textBoxDescription.Text		= detailStorage.Description;
			textBoxUnits.Text			= detailStorage.Units;
			textBoxNds.Text				= detailStorage.NdsTxt;
			textBoxStorageGroup.Text	= detailStorage.StorageGroupName;
			textBoxMinQuontity.Text		= detailStorage.MinQuontityTxt;

			// Добавление/Исправление
			textBox_name.Text			= detailStorage.Name;
			textBox_number.Text			= detailStorage.Number;
			checkBox_liquid.Checked		= detailStorage.Liquid;
			textBox_1C.Text				= detailStorage.Code_1C_Txt;

			// Отключаем возможность выбора детали и установки/изменения наименования
			buttonSelectDetail.Enabled	= false;
			textBoxDescription.Enabled	= true;		// Используем для описания применимости
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
			this.textBoxDetail = new System.Windows.Forms.TextBox();
			this.buttonSelectDetail = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.buttonOk = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.textBoxUnits = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textBoxStorageGroup = new System.Windows.Forms.TextBox();
			this.buttonSelectStorageGroup = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.textBoxMinQuontity = new System.Windows.Forms.TextBox();
			this.textBoxDescription = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.textBoxNds = new System.Windows.Forms.TextBox();
			this.checkBox_liquid = new System.Windows.Forms.CheckBox();
			this.textBox_name = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.textBox_number = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.textBox_1C = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// textBoxDetail
			// 
			this.textBoxDetail.Location = new System.Drawing.Point(8, 24);
			this.textBoxDetail.Name = "textBoxDetail";
			this.textBoxDetail.ReadOnly = true;
			this.textBoxDetail.Size = new System.Drawing.Size(600, 23);
			this.textBoxDetail.TabIndex = 0;
			this.textBoxDetail.Text = "textBox1";
			// 
			// buttonSelectDetail
			// 
			this.buttonSelectDetail.Location = new System.Drawing.Point(608, 24);
			this.buttonSelectDetail.Name = "buttonSelectDetail";
			this.buttonSelectDetail.Size = new System.Drawing.Size(24, 23);
			this.buttonSelectDetail.TabIndex = 1;
			this.buttonSelectDetail.Text = "...";
			this.buttonSelectDetail.Click += new System.EventHandler(this.buttonSelectDetail_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(224, 23);
			this.label1.TabIndex = 2;
			this.label1.Text = "Деталь";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 56);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(112, 23);
			this.label2.TabIndex = 3;
			this.label2.Text = "Описание";
			// 
			// buttonOk
			// 
			this.buttonOk.Location = new System.Drawing.Point(304, 368);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.TabIndex = 6;
			this.buttonOk.Text = "Добавить";
			this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 256);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(144, 24);
			this.label3.TabIndex = 7;
			this.label3.Text = "Единицы измерения";
			// 
			// textBoxUnits
			// 
			this.textBoxUnits.Location = new System.Drawing.Point(8, 280);
			this.textBoxUnits.Name = "textBoxUnits";
			this.textBoxUnits.TabIndex = 8;
			this.textBoxUnits.Text = "textBox1";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 312);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(160, 23);
			this.label4.TabIndex = 9;
			this.label4.Text = "Складская группа";
			// 
			// textBoxStorageGroup
			// 
			this.textBoxStorageGroup.Location = new System.Drawing.Point(8, 336);
			this.textBoxStorageGroup.Name = "textBoxStorageGroup";
			this.textBoxStorageGroup.ReadOnly = true;
			this.textBoxStorageGroup.Size = new System.Drawing.Size(416, 23);
			this.textBoxStorageGroup.TabIndex = 10;
			this.textBoxStorageGroup.Text = "textBox1";
			// 
			// buttonSelectStorageGroup
			// 
			this.buttonSelectStorageGroup.Location = new System.Drawing.Point(424, 336);
			this.buttonSelectStorageGroup.Name = "buttonSelectStorageGroup";
			this.buttonSelectStorageGroup.Size = new System.Drawing.Size(24, 23);
			this.buttonSelectStorageGroup.TabIndex = 11;
			this.buttonSelectStorageGroup.Text = "...";
			this.buttonSelectStorageGroup.Click += new System.EventHandler(this.buttonSelectStorageGroup_Click);
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(200, 256);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(168, 23);
			this.label5.TabIndex = 12;
			this.label5.Text = "Минимальный остаток";
			// 
			// textBoxMinQuontity
			// 
			this.textBoxMinQuontity.Location = new System.Drawing.Point(200, 280);
			this.textBoxMinQuontity.Name = "textBoxMinQuontity";
			this.textBoxMinQuontity.TabIndex = 13;
			this.textBoxMinQuontity.Text = "textBox1";
			// 
			// textBoxDescription
			// 
			this.textBoxDescription.Location = new System.Drawing.Point(8, 80);
			this.textBoxDescription.Name = "textBoxDescription";
			this.textBoxDescription.Size = new System.Drawing.Size(600, 23);
			this.textBoxDescription.TabIndex = 14;
			this.textBoxDescription.Text = "textBoxDescription";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(392, 256);
			this.label6.Name = "label6";
			this.label6.TabIndex = 15;
			this.label6.Text = "НДС";
			// 
			// textBoxNds
			// 
			this.textBoxNds.Location = new System.Drawing.Point(392, 280);
			this.textBoxNds.Name = "textBoxNds";
			this.textBoxNds.TabIndex = 16;
			this.textBoxNds.Text = "textBox2";
			// 
			// checkBox_liquid
			// 
			this.checkBox_liquid.Location = new System.Drawing.Point(8, 112);
			this.checkBox_liquid.Name = "checkBox_liquid";
			this.checkBox_liquid.TabIndex = 17;
			this.checkBox_liquid.Text = "Жидкости";
			// 
			// textBox_name
			// 
			this.textBox_name.Location = new System.Drawing.Point(8, 160);
			this.textBox_name.MaxLength = 256;
			this.textBox_name.Name = "textBox_name";
			this.textBox_name.Size = new System.Drawing.Size(624, 23);
			this.textBox_name.TabIndex = 18;
			this.textBox_name.Text = "";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(8, 136);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(224, 23);
			this.label7.TabIndex = 19;
			this.label7.Text = "Наименование позиции";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(8, 192);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(232, 24);
			this.label8.TabIndex = 20;
			this.label8.Text = "Код/Номер детали в справочнике";
			// 
			// textBox_number
			// 
			this.textBox_number.Location = new System.Drawing.Point(8, 216);
			this.textBox_number.MaxLength = 64;
			this.textBox_number.Name = "textBox_number";
			this.textBox_number.Size = new System.Drawing.Size(216, 23);
			this.textBox_number.TabIndex = 21;
			this.textBox_number.Text = "";
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(376, 192);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(184, 23);
			this.label9.TabIndex = 22;
			this.label9.Text = "Код синхронизации 1С";
			// 
			// textBox_1C
			// 
			this.textBox_1C.Enabled = false;
			this.textBox_1C.Location = new System.Drawing.Point(376, 216);
			this.textBox_1C.Name = "textBox_1C";
			this.textBox_1C.Size = new System.Drawing.Size(152, 23);
			this.textBox_1C.TabIndex = 23;
			this.textBox_1C.Text = "";
			// 
			// FormDetailStorage
			// 
			this.AcceptButton = this.buttonOk;
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.ClientSize = new System.Drawing.Size(664, 395);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.textBox_1C,
																		  this.label9,
																		  this.textBox_number,
																		  this.label8,
																		  this.label7,
																		  this.textBox_name,
																		  this.checkBox_liquid,
																		  this.textBoxNds,
																		  this.label6,
																		  this.textBoxDescription,
																		  this.textBoxMinQuontity,
																		  this.label5,
																		  this.buttonSelectStorageGroup,
																		  this.textBoxStorageGroup,
																		  this.label4,
																		  this.textBoxUnits,
																		  this.label3,
																		  this.buttonOk,
																		  this.label2,
																		  this.label1,
																		  this.buttonSelectDetail,
																		  this.textBoxDetail});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Name = "FormDetailStorage";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Складская позиция";
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonSelectDetail_Click(object sender, System.EventArgs e)
		{
			// Выбираем деталь
			FormDetailList dialog = new FormDetailList();
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			detailStorage.Detail = dialog.SelectedDetail;
			textBoxDetail.Text = detailStorage.DetailName;
		}

		private void buttonOk_Click(object sender, System.EventArgs e)
		{
			// Добавление новой складской позиции
			detailStorage.Units				= textBoxUnits.Text;
			detailStorage.NdsTxt			= textBoxNds.Text;
			detailStorage.MinQuontityTxt	= textBoxMinQuontity.Text;
			detailStorage.Description		= textBoxDescription.Text;
			detailStorage.Liquid			= checkBox_liquid.Checked;
			detailStorage.Name				= textBox_name.Text;
			detailStorage.Number			= textBox_number.Text;
			if(Db.ShowFaults()) return;

			if(detailStorage.IsValid() == false)
			{
				MessageBox.Show("Заполнены не все данные");
				return;
			}
			detailStorage.Write();
			if(Db.ShowFaults() == true) return;
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void buttonSelectStorageGroup_Click(object sender, System.EventArgs e)
		{
			// Выбор складской группы
			FormStorageGroupTree dialog = new FormStorageGroupTree();
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			detailStorage.StorageGroup = dialog.SelectedStorageGroup;
			textBoxStorageGroup.Text = detailStorage.StorageGroupName;
		}

		public DbDetailStorage DetailStorage
		{
			get
			{
				return detailStorage;
			}
		}
	}
}
