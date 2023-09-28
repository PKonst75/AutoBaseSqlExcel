using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for UIF_AutoOption.
	/// </summary>
	public class UIF_AutoOption : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox textBox_name;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox comboBox_group;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button button_add;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public DtAutoOption new_option = null;

		public UIF_AutoOption()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			// Заполняем список групп опций
			ArrayList array = new ArrayList();
			DbSqlAutoOptions.SelectInArrayGroup(array);
			foreach(object o in array)
			{
				DtAutoOptionGroup group = (DtAutoOptionGroup)o;
				comboBox_group.Items.Add(group);
			}
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
			this.textBox_name = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.comboBox_group = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.button_add = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// textBox_name
			// 
			this.textBox_name.Location = new System.Drawing.Point(104, 16);
			this.textBox_name.Name = "textBox_name";
			this.textBox_name.Size = new System.Drawing.Size(288, 20);
			this.textBox_name.TabIndex = 0;
			this.textBox_name.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(88, 23);
			this.label1.TabIndex = 1;
			this.label1.Text = "Наименование";
			// 
			// comboBox_group
			// 
			this.comboBox_group.Location = new System.Drawing.Point(104, 48);
			this.comboBox_group.Name = "comboBox_group";
			this.comboBox_group.Size = new System.Drawing.Size(288, 21);
			this.comboBox_group.TabIndex = 2;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(80, 24);
			this.label2.TabIndex = 3;
			this.label2.Text = "Группа";
			// 
			// button_add
			// 
			this.button_add.Location = new System.Drawing.Point(312, 80);
			this.button_add.Name = "button_add";
			this.button_add.TabIndex = 4;
			this.button_add.Text = "Добавить";
			this.button_add.Click += new System.EventHandler(this.button_add_Click);
			// 
			// UIF_AutoOption
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(400, 117);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.button_add,
																		  this.label2,
																		  this.comboBox_group,
																		  this.label1,
																		  this.textBox_name});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "UIF_AutoOption";
			this.Text = "Опция";
			this.ResumeLayout(false);

		}
		#endregion

		private void button_add_Click(object sender, System.EventArgs e)
		{
			// Добавить новую опцию в базу
			DtAutoOptionGroup group = (DtAutoOptionGroup)comboBox_group.SelectedItem;
			if(group == null) return;

			DtAutoOption option = new DtAutoOption();
			option.name			= textBox_name.Text;
			option.code_group	= group.code;
			option = DbSqlAutoOptions.InsertOption(option);
			if(option == null) return;
			if (option.code == 0) return;
			new_option = option;
			this.DialogResult = DialogResult.OK;
			this.Close();
		}
	}
}
