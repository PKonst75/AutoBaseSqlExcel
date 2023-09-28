using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormFaults.
	/// </summary>
	public class FormFaults : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox textBox1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private int linesCounter;

		private int MaxCount = 1000;

		public FormFaults(ArrayList faultsList)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			// Заполняем лист
			DateTime now = DateTime.Now;
			textBox1.Text = "";
			int count = faultsList.Count;
			if(count > MaxCount) count = MaxCount;
			string[] stringList = new string[count + 1];
			int index = 0;
			count = 0;
			stringList[count] = "* " + now.ToShortDateString() + " " + now.ToShortTimeString() + " *";
			count = 1;
			foreach(object o in faultsList)
			{
				if(count < MaxCount)
				{
					index++;
					stringList[count] = "\t" + (string)o;
					count += 1;
				}
			}
			textBox1.Lines = stringList;
			textBox1.Select(0, 0);
			linesCounter = count;
		}

		public void InsertFaults(ArrayList faultsList)
		{
			DateTime now = DateTime.Now;
			int count = faultsList.Count;
			if(textBox1.Lines.Length + count > MaxCount) return;
			string[] stringList = new string[linesCounter + count + 1];
			int index = 0;
			count = 0;
			textBox1.Lines.CopyTo(stringList, 0);
			stringList[linesCounter + count] = "* " + now.ToShortDateString() + " " + now.ToShortTimeString() + " *";
			count = 1;
			foreach(object o in faultsList)
			{
				index++;
				stringList[linesCounter + count] = "\t" + (string)o;
				count += 1;
			}
			textBox1.Lines = stringList;
			textBox1.Select(0, 0);
			linesCounter += count;
			textBox1.ScrollToCaret();
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
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// textBox1
			// 
			this.textBox1.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.textBox1.Location = new System.Drawing.Point(0, 8);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.textBox1.Size = new System.Drawing.Size(720, 256);
			this.textBox1.TabIndex = 0;
			this.textBox1.Text = "";
			this.textBox1.WordWrap = false;
			this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
			this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
			// 
			// FormFaults
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(720, 273);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.textBox1});
			this.Name = "FormFaults";
			this.Text = "Список ошибок";
			this.ResumeLayout(false);

		}
		#endregion

		protected void textBox1_KeyDown(object sender, KeyEventArgs e)
		{
			return;
		}
		protected void textBox1_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = true;
			return;
		}
		protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
		{
			Db.faultWindow = null;
		}
	}
}
