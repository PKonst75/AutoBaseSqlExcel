using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormFirm.
	/// </summary>
	public class FormFirm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button buttonOk;
		private System.Windows.Forms.Button buttonCancel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.TextBox textBox_name;
		private System.Windows.Forms.TextBox textBox_prefix;
		private System.Windows.Forms.Label label2;

		private DtFactory factory;
		private DtFactory source;
		bool adding	= false;

		public FormFirm(long code)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			if(code == 0)
			{
				// Добавление нового элемента
				factory = new DtFactory();
				adding = true;
			}
			else
			{
				source = DbSqlFactory.Find(code);
				if(source == null)
				{
					buttonOk.Enabled = false;
					MessageBox.Show("Ошибка");
					return;
				}
				factory = new DtFactory(source);
			}

			textBox_name.Text = (string)factory.GetData("НАИМЕНОВАНИЕ_АВТОМОБИЛЬ_ПРОИЗВОДИТЕЛЬ");
			textBox_prefix.Text = (string)factory.GetData("ПРЕФИКС_АВТОМОБИЛЬ_ПРОИЗВОДИТЕЛЬ");
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
			this.textBox_name = new System.Windows.Forms.TextBox();
			this.buttonOk = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.textBox_prefix = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(128, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "Наименование";
			// 
			// textBox_name
			// 
			this.textBox_name.Location = new System.Drawing.Point(8, 32);
			this.textBox_name.Name = "textBox_name";
			this.textBox_name.Size = new System.Drawing.Size(296, 23);
			this.textBox_name.TabIndex = 1;
			this.textBox_name.Text = "";
			// 
			// buttonOk
			// 
			this.buttonOk.Location = new System.Drawing.Point(88, 128);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.TabIndex = 2;
			this.buttonOk.Text = "Ок";
			this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(168, 128);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.TabIndex = 3;
			this.buttonCancel.Text = "Cancel";
			// 
			// textBox_prefix
			// 
			this.textBox_prefix.Location = new System.Drawing.Point(8, 96);
			this.textBox_prefix.Name = "textBox_prefix";
			this.textBox_prefix.Size = new System.Drawing.Size(80, 23);
			this.textBox_prefix.TabIndex = 4;
			this.textBox_prefix.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 72);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(104, 24);
			this.label2.TabIndex = 5;
			this.label2.Text = "Префикс VIN";
			// 
			// FormFirm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(320, 157);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.label2,
																		  this.textBox_prefix,
																		  this.buttonCancel,
																		  this.buttonOk,
																		  this.textBox_name,
																		  this.label1});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.Name = "FormFirm";
			this.Text = "Производитель";
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonOk_Click(object sender, System.EventArgs e)
		{
			// Попытка добавить в базу нового производителя
			factory.SetData("НАИМЕНОВАНИЕ_АВТОМОБИЛЬ_ПРОИЗВОДИТЕЛЬ",textBox_name.Text);
			factory.SetData("ПРЕФИКС_АВТОМОБИЛЬ_ПРОИЗВОДИТЕЛЬ",textBox_prefix.Text);

			if(adding == true)
			{
				if(DbSqlFactory.Insert(factory) == false) return;
			}
			else
			{
				if(DbSqlFactory.Update(factory) == false) return;
			}

			this.DialogResult = DialogResult.OK;
			this.Close();
			return;
		}

		public DtFactory Factory
		{
			get
			{
				return factory;
			}
		}
	}
}
