using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for FormInfo.
	/// </summary>
	public class FormInfo : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox textBox1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		protected int infoLevel;
		protected int linesCount;

		public FormInfo(Db element, int infoLevelSrc)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			// Первоночально лист у нас пуст
			linesCount			= 0;
			// Устанавливаем уровень информотивности окна
			infoLevel			= infoLevelSrc;
			// Создаем новое окно с информацией
			// Заполняем информацию на элемент element
			if (element != null)
			{
				string[] strings = element.Inform(infoLevel);
				if(strings != null)
				{
					string[] stringList = new string[strings.Length + 4];
					stringList[0] = "***************************************************";
					stringList[1] = "";
					for(int i = 0; i < strings.Length; i++)
					{
						stringList[2 + i] = strings[i];
					}
					stringList[2 + strings.Length] = "";
					stringList[2 + strings.Length + 1] = "***************************************************";
					linesCount	= strings.Length + 4;

					// Переносим получившуюся информацию в лист
					textBox1.Lines = stringList;
					textBox1.Select(0, 0);
					textBox1.ScrollToCaret();
				}
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
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// textBox1
			// 
			this.textBox1.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.textBox1.Location = new System.Drawing.Point(8, 8);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.textBox1.Size = new System.Drawing.Size(280, 256);
			this.textBox1.TabIndex = 0;
			this.textBox1.Text = "";
			this.textBox1.WordWrap = false;
			// 
			// FormInfo
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.ClientSize = new System.Drawing.Size(292, 273);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.textBox1});
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.KeyPreview = true;
			this.Name = "FormInfo";
			this.Text = "Информация";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.ResumeLayout(false);
			this.KeyDown += new KeyEventHandler(this.formInfo_KeyDown);
			this.KeyPress += new KeyPressEventHandler(this.formInfo_KeyPress);

		}
		#endregion

		protected void formInfo_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.Alt == true)
			{
				if(e.KeyCode == Keys.Q)
				{
					this.Close();
					e.Handled = true;
					return;
				}
			}
			e.Handled = true;
			return;
		}
		protected void formInfo_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = true;
			return;
		}

		public void InsertInfo(Db element)
		{
			// Заполняем информацию на элемент element
			if (element != null)
			{
				string[] strings = element.Inform(infoLevel);
				if(strings != null)
				{
					string[] stringList = new string[linesCount + strings.Length + 4];
					textBox1.Lines.CopyTo(stringList, 0);
					stringList[0 + linesCount] = "***************************************************";
					stringList[1 + linesCount] = "";
					for(int i = 0; i < strings.Length; i++)
					{
						stringList[2 + i + linesCount] = strings[i];
					}
					stringList[2 + strings.Length + linesCount] = "";
					stringList[2 + strings.Length + 1 + linesCount] = "***************************************************";
					linesCount	= strings.Length + 4 + linesCount;

					// Переносим получившуюся информацию в лист
					textBox1.Lines = stringList;
					textBox1.Select(0, 0);
					textBox1.ScrollToCaret();
				}
			}
		}
	}
}
