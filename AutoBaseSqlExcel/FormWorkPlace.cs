using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormWorkPlace.
	/// </summary>
	public class FormWorkPlace : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;

		private DbWorkPlace workPlace;
		private System.Windows.Forms.TextBox textBoxName;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button buttonOk;

		private int offsetX = 70;
		private int offsetY = 50;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FormWorkPlace(DbWorkPlace sourceWorkPlace)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			if(sourceWorkPlace != null)
			{
				workPlace = sourceWorkPlace;
				workPlace.MakeHour();
			}
			else
			{
				workPlace = new DbWorkPlace();
			}
			textBoxName.Text = workPlace.Name;
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
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.label1 = new System.Windows.Forms.Label();
			this.textBoxName = new System.Windows.Forms.TextBox();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.buttonOk = new System.Windows.Forms.Button();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.tabControl1.Controls.AddRange(new System.Windows.Forms.Control[] {
																					  this.tabPage1,
																					  this.tabPage2});
			this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.tabControl1.Location = new System.Drawing.Point(8, 32);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(664, 284);
			this.tabControl1.TabIndex = 0;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.AddRange(new System.Windows.Forms.Control[] {
																				   this.label1,
																				   this.textBoxName});
			this.tabPage1.Location = new System.Drawing.Point(4, 25);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new System.Drawing.Size(656, 255);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Рабочее место";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(128, 23);
			this.label1.TabIndex = 1;
			this.label1.Text = "Наименование";
			// 
			// textBoxName
			// 
			this.textBoxName.Location = new System.Drawing.Point(16, 48);
			this.textBoxName.Name = "textBoxName";
			this.textBoxName.Size = new System.Drawing.Size(344, 23);
			this.textBoxName.TabIndex = 0;
			this.textBoxName.Text = "textBox1";
			// 
			// tabPage2
			// 
			this.tabPage2.Location = new System.Drawing.Point(4, 25);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Size = new System.Drawing.Size(464, 171);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Расписание";
			this.tabPage2.Click += new System.EventHandler(this.tabPage2_Click);
			this.tabPage2.Paint += new System.Windows.Forms.PaintEventHandler(this.tabPage2_Paint);
			// 
			// buttonOk
			// 
			this.buttonOk.Location = new System.Drawing.Point(280, 328);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.TabIndex = 1;
			this.buttonOk.Text = "ОК";
			this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
			// 
			// FormWorkPlace
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(680, 357);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.buttonOk,
																		  this.tabControl1});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "FormWorkPlace";
			this.Text = "Рабочее место";
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		protected void tabPage2_Paint(object sender, PaintEventArgs e)
		{
			workPlace.DrawTitle(e.Graphics, offsetX, offsetY);
			workPlace.DrawHours(e.Graphics, offsetX, offsetY);
		}

		protected void tabPage2_Click(object sender, EventArgs e)
		{
			Point pnt = Cursor.Position;
			pnt = tabPage2.PointToClient(pnt);
			int  i = workPlace.ChangeStatus(pnt, offsetX, offsetY);
			if(workPlace.IsValidIndex(i) == false) return;
			Rectangle rect = workPlace.GetCellRect(i, offsetX, offsetY);
			tabPage2.Invalidate(rect);
		}

		private void buttonOk_Click(object sender, System.EventArgs e)
		{
			// Применяем сделанные изменения
			workPlace.Name = textBoxName.Text;
			workPlace.MakeHours();
			
			if(workPlace.Write() == false) return;

			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		public DbWorkPlace WorkPlace
		{
			get
			{
				return workPlace;
			}
		}
	}
}
