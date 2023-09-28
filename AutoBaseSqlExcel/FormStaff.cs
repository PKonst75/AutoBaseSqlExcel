using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormStaff.
	/// </summary>
	public class FormStaff : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBoxFirstName;
		private System.Windows.Forms.TextBox textBoxName;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBoxSecondName;
		private System.Windows.Forms.Button buttonOk;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private DbStaff staff;

		public FormStaff(DbStaff sourceStaff)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			if(sourceStaff == null)
			{
				staff = new DbStaff();
			}
			else
			{
				staff = new DbStaff(sourceStaff);
			}
			textBoxFirstName.Text = staff.FirstName;
			textBoxName.Text = staff.Name;
			textBoxSecondName.Text = staff.SecondName;
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
			this.textBoxFirstName = new System.Windows.Forms.TextBox();
			this.textBoxName = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textBoxSecondName = new System.Windows.Forms.TextBox();
			this.buttonOk = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.TabIndex = 0;
			this.label1.Text = "Фамилия";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 40);
			this.label2.Name = "label2";
			this.label2.TabIndex = 1;
			this.label2.Text = "Имя";
			// 
			// textBoxFirstName
			// 
			this.textBoxFirstName.Location = new System.Drawing.Point(120, 8);
			this.textBoxFirstName.Name = "textBoxFirstName";
			this.textBoxFirstName.Size = new System.Drawing.Size(248, 23);
			this.textBoxFirstName.TabIndex = 2;
			this.textBoxFirstName.Text = "textBox1";
			// 
			// textBoxName
			// 
			this.textBoxName.Location = new System.Drawing.Point(120, 40);
			this.textBoxName.Name = "textBoxName";
			this.textBoxName.Size = new System.Drawing.Size(248, 23);
			this.textBoxName.TabIndex = 3;
			this.textBoxName.Text = "textBox1";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 72);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(104, 24);
			this.label3.TabIndex = 4;
			this.label3.Text = "Отчество";
			// 
			// textBoxSecondName
			// 
			this.textBoxSecondName.Location = new System.Drawing.Point(120, 72);
			this.textBoxSecondName.Name = "textBoxSecondName";
			this.textBoxSecondName.Size = new System.Drawing.Size(248, 23);
			this.textBoxSecondName.TabIndex = 5;
			this.textBoxSecondName.Text = "textBox1";
			// 
			// buttonOk
			// 
			this.buttonOk.Location = new System.Drawing.Point(160, 216);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.TabIndex = 6;
			this.buttonOk.Text = "ОК";
			this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
			// 
			// FormStaff
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.ClientSize = new System.Drawing.Size(424, 245);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.buttonOk,
																		  this.textBoxSecondName,
																		  this.label3,
																		  this.textBoxName,
																		  this.textBoxFirstName,
																		  this.label2,
																		  this.label1});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "FormStaff";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Сотрудник";
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonOk_Click(object sender, System.EventArgs e)
		{
			// Нажатие кнопки ОК
			staff.Name			= textBoxName.Text;
			staff.FirstName		= textBoxFirstName.Text;
			staff.SecondName	= textBoxSecondName.Text;

			if(Db.ShowFaults() == true) return;			// Проверка на выполнение условий

			// Проверка на похожие данные
			ArrayList sameStaff = new ArrayList();
			DbStaff.FindFirstName(sameStaff, staff.FirstName);
			if(sameStaff.Count != 0)
			{
				// Вывод списка похожих и запрос на добавление нового
				// Запрос на добавление
				FormInfo dialog = new FormInfo(null, 0);
				foreach(object o in sameStaff)
				{
					DbStaff element = (DbStaff)o;
					dialog.InsertInfo(element);
				}
				dialog.ShowDialog();
				DialogResult result = MessageBox.Show("В базе есть похожие элементы, добавить?", "Запрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if(result != DialogResult.Yes)
				{
					dialog.Close();
					return;
				}
			}

			if(staff.Write() != true) return;
			this.DialogResult = DialogResult.OK;
			this.Close();
			return;
		}

		public DbStaff Staff
		{
			get
			{
				return staff;
			}
		}
	}
}
