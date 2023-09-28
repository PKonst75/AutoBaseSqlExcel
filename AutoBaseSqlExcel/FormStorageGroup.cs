using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormStorageGroup.
	/// </summary>
	public class FormStorageGroup : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxName;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBoxCharge;
		private System.Windows.Forms.Button buttonOk;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label label3;
		private DbStorageGroup storageGroup;

		public FormStorageGroup(DbStorageGroup source, DbStorageGroup parent)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			if(source == null)
			{
				if(parent == null)
					storageGroup = new DbStorageGroup(0);
				else
					storageGroup = new DbStorageGroup(parent.Code);
			}
			else
			{
				storageGroup = new DbStorageGroup(source);
			}
			textBoxName.Text = storageGroup.Name;
			textBoxCharge.Text = storageGroup.ChargeTxt;
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
			this.textBoxName = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textBoxCharge = new System.Windows.Forms.TextBox();
			this.buttonOk = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(112, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "Наименование";
			// 
			// textBoxName
			// 
			this.textBoxName.Location = new System.Drawing.Point(8, 40);
			this.textBoxName.Name = "textBoxName";
			this.textBoxName.Size = new System.Drawing.Size(440, 23);
			this.textBoxName.TabIndex = 1;
			this.textBoxName.Text = "textBox1";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 80);
			this.label2.Name = "label2";
			this.label2.TabIndex = 2;
			this.label2.Text = "Наценка";
			// 
			// textBoxCharge
			// 
			this.textBoxCharge.Location = new System.Drawing.Point(104, 80);
			this.textBoxCharge.Name = "textBoxCharge";
			this.textBoxCharge.TabIndex = 3;
			this.textBoxCharge.Text = "textBox1";
			// 
			// buttonOk
			// 
			this.buttonOk.Location = new System.Drawing.Point(176, 120);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.TabIndex = 5;
			this.buttonOk.Text = "ОК";
			this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(208, 80);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(24, 23);
			this.label3.TabIndex = 6;
			this.label3.Text = "%";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// FormStorageGroup
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.ClientSize = new System.Drawing.Size(464, 157);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.label3,
																		  this.buttonOk,
																		  this.textBoxCharge,
																		  this.label2,
																		  this.textBoxName,
																		  this.label1});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.Name = "FormStorageGroup";
			this.Text = "Складская группа";
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonOk_Click(object sender, System.EventArgs e)
		{
			// Добавление/Изменение складской группы
			storageGroup.Name = textBoxName.Text;
			storageGroup.ChargeTxt = textBoxCharge.Text;
			if(Db.ShowFaults()) return;

			if(storageGroup.Write() != true)
			{
				Db.ShowFaults();
				return;
			}
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		public DbStorageGroup StorageGroup
		{
			get
			{
				return storageGroup;
			}
		}
	}
}
