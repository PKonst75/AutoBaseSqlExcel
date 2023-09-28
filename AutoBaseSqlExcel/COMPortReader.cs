using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoBaseSql
{
	/// <summary>
	/// Summary description for COMPortReader.
	/// </summary>
	public class COMPortReader : System.Windows.Forms.Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public COMPortReader()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			// Открываем COM порт 7
			//HANDLE handle = CreateFile("COM7", GENERIC_READ | GENERIC_WRITE, NULL, NULL, OPEN_EXISTING, 0, NULL);
			//HANDLE handle = CreateFile("COM1", GENERIC_READ | GENERIC_WRITE, NULL, NULL, OPEN_EXISTING, FILE_FLAG_OVERLAPPED, NULL);

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
			// 
			// COMPortReader
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(608, 273);
			this.Name = "COMPortReader";
			this.Text = "COMPortReader";

		}
		#endregion
	}
}
