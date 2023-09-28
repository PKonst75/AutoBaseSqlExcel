using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormDefectList.
	/// </summary>
	public class FormDefectList : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.Button buttonNew;
		private System.Windows.Forms.TextBox textBoxAutoType;
		private System.Windows.Forms.Button buttonSelectAutoType;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private DbAutoType	autoType = null;

		public FormDefectList()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormDefectList));
			this.listView1 = new System.Windows.Forms.ListView();
			this.buttonNew = new System.Windows.Forms.Button();
			this.textBoxAutoType = new System.Windows.Forms.TextBox();
			this.buttonSelectAutoType = new System.Windows.Forms.Button();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.SuspendLayout();
			// 
			// listView1
			// 
			this.listView1.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.columnHeader1,
																						this.columnHeader2,
																						this.columnHeader3,
																						this.columnHeader4});
			this.listView1.FullRowSelect = true;
			this.listView1.GridLines = true;
			this.listView1.Location = new System.Drawing.Point(8, 24);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(592, 240);
			this.listView1.TabIndex = 0;
			this.listView1.View = System.Windows.Forms.View.Details;
			// 
			// buttonNew
			// 
			this.buttonNew.Image = ((System.Drawing.Bitmap)(resources.GetObject("buttonNew.Image")));
			this.buttonNew.Location = new System.Drawing.Point(8, 0);
			this.buttonNew.Name = "buttonNew";
			this.buttonNew.Size = new System.Drawing.Size(24, 23);
			this.buttonNew.TabIndex = 1;
			// 
			// textBoxAutoType
			// 
			this.textBoxAutoType.Location = new System.Drawing.Point(288, 0);
			this.textBoxAutoType.Name = "textBoxAutoType";
			this.textBoxAutoType.ReadOnly = true;
			this.textBoxAutoType.Size = new System.Drawing.Size(192, 23);
			this.textBoxAutoType.TabIndex = 2;
			this.textBoxAutoType.Text = "textBox1";
			// 
			// buttonSelectAutoType
			// 
			this.buttonSelectAutoType.Location = new System.Drawing.Point(480, 0);
			this.buttonSelectAutoType.Name = "buttonSelectAutoType";
			this.buttonSelectAutoType.Size = new System.Drawing.Size(24, 23);
			this.buttonSelectAutoType.TabIndex = 3;
			this.buttonSelectAutoType.Text = "...";
			this.buttonSelectAutoType.Click += new System.EventHandler(this.buttonSelectAutoType_Click);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Деталь";
			this.columnHeader1.Width = 100;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Код дефекта";
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Место дефекта";
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Наименование дефекта";
			this.columnHeader4.Width = 280;
			// 
			// FormDefectList
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.ClientSize = new System.Drawing.Size(608, 273);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.buttonSelectAutoType,
																		  this.textBoxAutoType,
																		  this.buttonNew,
																		  this.listView1});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.Name = "FormDefectList";
			this.Text = "Дефекты";
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonSelectAutoType_Click(object sender, System.EventArgs e)
		{
			// Выбор группы автомобилей, для определения принадлежности дефекта
			FormAutoTypeList dialog = new FormAutoTypeList();
			dialog.ShowDialog(this);
			if(dialog.DialogResult != DialogResult.OK) return;
			autoType = dialog.SelectedAutoType;
			textBoxAutoType.Text = autoType.NameTxt;
		}
	}
}
