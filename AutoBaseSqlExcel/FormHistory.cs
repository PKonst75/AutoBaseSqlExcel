using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormHistory.
	/// </summary>
	public class FormHistory : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.DateTimePicker dateTimePicker1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button buttonUpdate;
		private System.Windows.Forms.DateTimePicker dateTimePicker2;
		private System.Windows.Forms.TextBox textBoxStaff;
		private System.Windows.Forms.Label label3;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FormHistory()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormHistory));
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
			this.label1 = new System.Windows.Forms.Label();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.buttonUpdate = new System.Windows.Forms.Button();
			this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
			this.textBoxStaff = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// listView1
			// 
			this.listView1.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.columnHeader6,
																						this.columnHeader1,
																						this.columnHeader2,
																						this.columnHeader3,
																						this.columnHeader4,
																						this.columnHeader5});
			this.listView1.FullRowSelect = true;
			this.listView1.Location = new System.Drawing.Point(8, 56);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(840, 208);
			this.listView1.TabIndex = 0;
			this.listView1.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "Время";
			this.columnHeader6.Width = 77;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Таблица";
			this.columnHeader1.Width = 120;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Действие";
			this.columnHeader2.Width = 120;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Наименование";
			this.columnHeader3.Width = 230;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Дополнение";
			this.columnHeader4.Width = 168;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "Пользователь";
			this.columnHeader5.Width = 106;
			// 
			// dateTimePicker1
			// 
			this.dateTimePicker1.Location = new System.Drawing.Point(584, 0);
			this.dateTimePicker1.Name = "dateTimePicker1";
			this.dateTimePicker1.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(440, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(144, 23);
			this.label1.TabIndex = 2;
			this.label1.Text = "Интервал контроля";
			// 
			// comboBox1
			// 
			this.comboBox1.Location = new System.Drawing.Point(112, 0);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(248, 24);
			this.comboBox1.TabIndex = 3;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 0);
			this.label2.Name = "label2";
			this.label2.TabIndex = 4;
			this.label2.Text = "Объект";
			// 
			// buttonUpdate
			// 
			this.buttonUpdate.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.buttonUpdate.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonUpdate.Image")));
			this.buttonUpdate.Location = new System.Drawing.Point(824, 32);
			this.buttonUpdate.Name = "buttonUpdate";
			this.buttonUpdate.Size = new System.Drawing.Size(24, 23);
			this.buttonUpdate.TabIndex = 5;
			this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
			// 
			// dateTimePicker2
			// 
			this.dateTimePicker2.Location = new System.Drawing.Point(584, 24);
			this.dateTimePicker2.Name = "dateTimePicker2";
			this.dateTimePicker2.TabIndex = 6;
			// 
			// textBoxStaff
			// 
			this.textBoxStaff.Location = new System.Drawing.Point(112, 24);
			this.textBoxStaff.Name = "textBoxStaff";
			this.textBoxStaff.ReadOnly = true;
			this.textBoxStaff.Size = new System.Drawing.Size(248, 23);
			this.textBoxStaff.TabIndex = 7;
			this.textBoxStaff.Text = "";
			this.textBoxStaff.Click += new System.EventHandler(this.textBoxStaff_Click);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 24);
			this.label3.Name = "label3";
			this.label3.TabIndex = 8;
			this.label3.Text = "Персонал";
			// 
			// FormHistory
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.ClientSize = new System.Drawing.Size(856, 273);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.label3,
																		  this.textBoxStaff,
																		  this.dateTimePicker2,
																		  this.buttonUpdate,
																		  this.label2,
																		  this.comboBox1,
																		  this.label1,
																		  this.dateTimePicker1,
																		  this.listView1});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.Name = "FormHistory";
			this.Text = "История";
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonUpdate_Click(object sender, System.EventArgs e)
		{
			// Обновляем список истории
			listView1.Items.Clear();
			DbHistory.FillList(listView1, "", DateTime.Now, DateTime.Now, "");
		}

		private void textBoxStaff_Click(object sender, System.EventArgs e)
		{
			// Вызов диалога выбора искомого персонала
			ArrayList array = new ArrayList();
			DbStaff.FillArray(array);
			FormSelectionType1 dialog = Db.MakeFormSelectionType1(this, textBoxStaff, array, "", true);
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			DbStaff staff = (DbStaff)dialog.SelectedElement;
			textBoxStaff.Text = staff.DbTitle();
		}
	}
}
